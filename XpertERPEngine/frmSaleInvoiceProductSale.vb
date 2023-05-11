
'by vipin for some feild hidden on 07/03/2013 
'---preeti Gupta---Ticket No.-BM00000003015--
'-Updation By [Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000003165 14/07/2014],BHA/19/06/18-000058
'-BM00000003441




Imports common
Imports System
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations

Public Class frmSaleInvoiceProductSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim FORMTYPE As String = Nothing
    Dim IsDairyModule As Boolean = False
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim EnableTCSRateValidityFrom01July2021 As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Dim ShowShipToPartyInDairyDispatch As Integer = 0
    Const colVS_CashSchemeCode As String = "colVS_CashSchemeCode"
    Const colVS_Cash_Amt As String = "colVS_Cash_Amt"
    Const colVS_ltrInCrate As String = "colVS_ltrInCrate"
    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"
    Dim atchqry As String = ""
    Private StrSql As String
    Public strrptpath As String = Nothing
    Private AllowChangeInvoiceType As Boolean = False
    Public strExcise As Boolean
    Public intMRPwithabatement As Integer
    Public strSaleInvoice As String = Nothing
    Private isPO_GRN_MRN_Editable As Boolean = False
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"

    Const ReportID As String = "IPSShipmentSNItemGrid"
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
    Const colHSNNo As String = "COLHSNNo"
    Const colPendingQty As String = "COLPENDINGQTY"

    Const colUnitALter As String = "colUnitALter"
    Const colUnitRate As String = "colUnitRate"
    'Const colOrgSOQty As String = "COLORGSOQTY"
    Const colQty As String = "COLQTY"
    Const colHeaDDisPer As String = "colHeaDDisPer"
    Const colHeadDisPerAmt As String = "colHeadDisPerAmt"
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
    Const colIsTaxOnBaseAmount As String = "colIsTaxOnBaseAmount"
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


    Const colLocationCode As String = "LOCATIONCODE"
    Const colLocationName As String = "LOCATIONNAME"


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
    Public DocumentNo As String = Nothing
    Const ColCommParty As String = "ColCommParty"
    Const ColCommPartyName As String = "ColCommPartyName"
    Const colCommRate As String = "colCommRate"
    Const ColCommAmt As String = "ColCommAmt"
    Const ColAmtAfterCOmm As String = "ColAmtAfterCOmm"
    Dim Item_TaxType As Integer = 0
    Public ShowDocumentCancel As Boolean = False
    Public AllowSeperateSchemeItemOnPrint As Boolean = False
    Public AllowManualVehicleOnDairyDispatch As Boolean = False
    Dim SaleInvoiceDate As DateTime
    Dim FlagDocumentIsTaxable As Integer = 0
    Dim EInvoiceType As String = ""
#End Region

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Public Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleInvoiceProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        RadButton1.Visible = MyBase.isCancel_Flag_After_Posting
        If MyBase.isReverse Then
            btnReverseAndUnpost.Enabled = True
        Else
            btnReverseAndUnpost.Enabled = False
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnHistory.Enabled = False
        SetUserMgmtNew()
        SetMailRight()
        IsDairyModule = IIf(FORMTYPE = "INVOICE-DS", True, False)
        ShowShipToPartyInDairyDispatch = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ShowShipToPartyInDairyDispatch & "'")) = 0, 0, 1)
        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        isPO_GRN_MRN_Editable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isMRNQtyEdiatableOnSRN from TSPL_inv_parameters")) = 0, False, True)
        AllowSeperateSchemeItemOnPrint = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSeprateSchemeItemPrintDairySaleInvoice, clsFixedParameterCode.AllowSeprateSchemeItemPrintDairySaleInvoice, Nothing)) = 0, False, True)
        AllowManualVehicleOnDairyDispatch = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualvehicleOnDairyBooking, clsFixedParameterCode.AllowManualvehicleOnDairyBooking, Nothing)) = 1, True, False)
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        EnableTCSRateValidityFrom01July2021 = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTCSRateValidityFrom01July2021, clsFixedParameterCode.EnableTCSRateValidityFrom01July2021, Nothing)) = 0, False, True)
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
        LoadSupplementaryType()
        LoadInvoiceType()
        LoadBlankGridAC()
        AddNew()
        LoadPaymentTerms()
        LoadDispatchTerms()
        SetLength()
        If clsCommon.myLen(strSRNno) > 0 Then
            LoadData(strSRNno, NavigatorType.Current)
        End If
        chkRateDefaultSetting.ToggleState = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, Nothing)) = 1, ToggleState.On, ToggleState.Off)
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        If clsCommon.myLen(strSaleInvoice) > 0 Then
            LoadData(strSaleInvoice, NavigatorType.Current)
        End If
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
        ShowDocumentCancel = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DocumentCancel, clsFixedParameterCode.DocumentCancel, Nothing)) = 1, True, False)

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        fndProject.Enabled = False
        lblProject.Enabled = False
        intMRPwithabatement = clsDBFuncationality.getSingleValue("select IsMRPwithAbatement from TSPL_INV_PARAMETERS")

        If clsCommon.CompairString(Me.Form_ID, "INVOICE-DS") = CompairStringResult.Equal Then
            btn_Depo_Print.Visibility = ElementVisibility.Collapsed
        End If

        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        btnDeliveredTo.Enabled = False
        RadMenuItem5.Visibility = ElementVisibility.Collapsed
        Try
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            End If
        Catch ex As Exception
        End Try
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
    'Public Shared Function GetInvoiceType() As DataTable
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = ""
    '    dr("Name") = "Select"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "R"
    '    dr("Name") = "Retail"
    '    dt.Rows.Add(dr)


    '    dr = dt.NewRow()
    '    dr("Code") = "T"
    '    dr("Name") = "Tax"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "E"
    '    dr("Name") = "Excise"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "A"
    '    dr("Name") = "Tax Exempted"
    '    dt.Rows.Add(dr)


    '    Return dt
    'End Function
    Sub LoadInvoiceType()
        ddlInvoiceType.DataSource = clsPSShipmentHead.GetInvoiceType(1)
        ddlInvoiceType.ValueMember = "Code"
        ddlInvoiceType.DisplayMember = "Name"
    End Sub
    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 2
        cboItemType.Visible = False
        RadLabel29.Visible = False
    End Sub

    Sub LoadSupplementaryType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Supplementrary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Credit note"
        dt.Rows.Add(dr)

        cboSupplementaryType.DataSource = dt
        cboSupplementaryType.ValueMember = "Code"
        cboSupplementaryType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        EInvoiceType = ""
        FlagDocumentIsTaxable = 0
        TxtRoundoff.Text = ""
        txtRoadPermitNo.Text = ""
        txtVehicleCapacity.Value = 0
        ddlPaymentTerms.SelectedValue = ""
        ddlDispatchTerms.SelectedValue = ""
        txtDispatchPeriod.Value = 0
        txtSOvalidity.Value = 0
        txtDispatchDate.Value = clsCommon.GETSERVERDATE
        chkCommApply.Checked = False
        txtForm38.Text = ""
        txtPONo.Text = ""
        txtpodate.Text = ""
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
        txtVehicleNo.Text = ""
        txtVehcileCode.Text = ""
        txtGRNo.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value
        txtInvNoForSupplementary.Value = ""
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
        ddlInvoiceType.SelectedValue = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Text = ""
        lblProject.Text = ""
        txtPriceCode.Text = ""
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        txtMannaulInvoiceNo.Value = 0
        '-----------------richa 26/06/2014 Ticket No .BM00000002982------------------------
        TxtInvoiceManualNoWithPrefix.Text = ""
        '----------------------------------------------------------------------------------
        cboSupplementaryType.SelectedValue = ""
        RadButton1.Enabled = False
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
        gv1.MasterTemplate.Columns.Add(repoRowType)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = My.Resources.search4
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


        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "HSN No/SAC Code"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


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

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoAlterUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAlterUnit.FormatString = ""
        repoAlterUnit.HeaderText = "Alter UOM"
        repoAlterUnit.Name = colUnitALter
        repoAlterUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoAlterUnit)

        Dim reporateUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporateUnit.FormatString = ""
        reporateUnit.HeaderText = "Rate UOM"
        reporateUnit.Name = colUnitRate
        reporateUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(reporateUnit)


        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = False
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Actual Balance"
        repoActualBalQty.Name = ColActualBalQty
        repoActualBalQty.Width = 80
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        repoActualBalQty.IsVisible = False
        repoActualBalQty.VisibleInColumnChooser = False
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
        repoItemWt.ReadOnly = True
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemWt)

        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Conv. Factor"
        repoConv.Name = colConvF
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = True
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)

        Dim repoTotItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemWt = New GridViewDecimalColumn()
        repoTotItemWt.FormatString = ""
        repoTotItemWt.HeaderText = "Tot Item Weight"
        repoTotItemWt.Name = colTotItemWt
        repoTotItemWt.Width = 80
        repoTotItemWt.Minimum = 0
        repoTotItemWt.ReadOnly = True
        repoTotItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotItemWt)





        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRP)




        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty.FormatString = ""
        repoFreeQty.HeaderText = "Free Quantity"
        repoFreeQty.Name = colFreeQty
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.IsVisible = False
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFreeQty)

        Dim repoLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationCode.FormatString = ""
        repoLocationCode.HeaderText = "Location Code"
        repoLocationCode.Name = colLocationCode
        repoLocationCode.HeaderImage = My.Resources.search4
        repoLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLocationCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoLocationCode)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Location"
        repoLocationName.Name = colLocationName
        repoLocationName.ReadOnly = True
        repoLocationName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoLocationName)



        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Basic Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrgRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgRate = New GridViewDecimalColumn()
        repoOrgRate.FormatString = ""
        repoOrgRate.HeaderText = "Org Basic Rate"
        repoOrgRate.Name = colOrgCost
        repoOrgRate.Width = 80
        repoOrgRate.Minimum = 0
        repoOrgRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOrgRate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoOrgRate)


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

        Dim repoVS_CashSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVS_CashSchemeCode.HeaderText = "Volume Slab Cash Scheme Code"
        repoVS_CashSchemeCode.Name = colVS_CashSchemeCode
        repoVS_CashSchemeCode.Width = 80
        repoVS_CashSchemeCode.ReadOnly = True
        repoVS_CashSchemeCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVS_CashSchemeCode)

        Dim repoVS_totalLtr As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVS_totalLtr.HeaderText = "Total Ltr in Crate"
        repoVS_totalLtr.MinWidth = 4
        repoVS_totalLtr.Name = colVS_ltrInCrate
        repoVS_totalLtr.ReadOnly = True
        repoVS_totalLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoVS_totalLtr.Width = 54
        repoVS_totalLtr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVS_totalLtr)

        Dim repoVS_CashDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoVS_CashDis.HeaderText = "Volume Slab Cash Dis Amt."
        repoVS_CashDis.MinWidth = 4
        repoVS_CashDis.Name = colVS_Cash_Amt
        repoVS_CashDis.ReadOnly = True
        repoVS_CashDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoVS_CashDis.Width = 54
        repoVS_CashDis.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVS_CashDis)


        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate = New GridViewDecimalColumn()
        repoAbatementRate.FormatString = ""
        repoAbatementRate.HeaderText = "Abatement %"
        repoAbatementRate.Name = colAbatementPer
        repoAbatementRate.Width = 80
        repoAbatementRate.Minimum = 0
        repoAbatementRate.ReadOnly = False
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
        repoAbatementamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementamount)

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

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
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

        Dim repoTotalMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalMRP.AllowSort = False
        repoTotalMRP.HeaderText = "Total MRP"
        repoTotalMRP.Name = colTotalMRP
        repoTotalMRP.ReadOnly = True
        repoTotalMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalMRP.Width = 62
        gv1.MasterTemplate.Columns.Add(repoTotalMRP)

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Basic Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
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

        Dim repoCommparty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCommparty.FormatString = ""
        repoCommparty.HeaderText = "Comm. Party"
        repoCommparty.Name = ColCommParty
        repoCommparty.HeaderImage = My.Resources.search4
        repoCommparty.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCommparty.Width = 50
        gv1.MasterTemplate.Columns.Add(repoCommparty)

        Dim repoCommPartyName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCommPartyName.FormatString = ""
        repoCommPartyName.HeaderText = "Comm. Party Name"
        repoCommPartyName.Name = ColCommPartyName
        repoCommPartyName.Width = 100
        gv1.MasterTemplate.Columns.Add(repoCommPartyName)


        Dim repoCommPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommPer = New GridViewDecimalColumn()
        repoCommPer.FormatString = ""
        repoCommPer.HeaderText = "Comm %"
        repoCommPer.Minimum = 0
        repoCommPer.Maximum = 100
        repoCommPer.Name = colCommRate
        repoCommPer.Width = 100
        repoCommPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCommPer)

        Dim repoCommAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommAmt = New GridViewDecimalColumn()
        repoCommAmt.FormatString = ""
        repoCommAmt.HeaderText = "Comm Amount"
        repoCommAmt.WrapText = True
        repoCommAmt.Name = ColCommAmt
        repoCommAmt.Width = 80
        repoCommAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommAmt.VisibleInColumnChooser = False
        repoCommAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCommAmt)

        Dim repoCommAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommAmtAfterDis = New GridViewDecimalColumn()
        repoCommAmtAfterDis.FormatString = ""
        repoCommAmtAfterDis.HeaderText = "Amount After Comm."
        repoCommAmtAfterDis.Name = ColAmtAfterCOmm
        repoCommAmtAfterDis.WrapText = True
        repoCommAmtAfterDis.Width = 80
        repoCommAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommAmtAfterDis.VisibleInColumnChooser = False
        repoCommAmtAfterDis.ReadOnly = True
        repoCommAmtAfterDis.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCommAmtAfterDis)


        Dim repoPrincipleCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleCOde.FormatString = ""
        repoPrincipleCOde.HeaderText = "Principle Code"
        repoPrincipleCOde.Name = colPricipleCode
        repoPrincipleCOde.Width = 150
        repoPrincipleCOde.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPrincipleCOde)

        Dim repoPrincipleDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleDesc.FormatString = ""
        repoPrincipleDesc.HeaderText = "Principle Desc"
        repoPrincipleDesc.Name = colPricipleDesc
        repoPrincipleDesc.Width = 150
        repoPrincipleDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPrincipleDesc)

        Dim repoVCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCOde.FormatString = ""
        repoVCOde.HeaderText = "Vendor Code"
        repoVCOde.Name = colvendorCode
        repoVCOde.Width = 150
        repoVCOde.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVCOde)

        Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVDesc.FormatString = ""
        repoVDesc.HeaderText = "Vendor Desc"
        repoVDesc.Name = colvendorDesc
        repoVDesc.Width = 150
        repoVDesc.ReadOnly = True
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
        gv1.MasterTemplate.Columns.Add(repoPurCost)

        Dim repoMarkupOn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMarkupOn.FormatString = ""
        repoMarkupOn.HeaderText = "MarkUp On"
        repoMarkupOn.Name = colMarkupOn
        repoMarkupOn.ReadOnly = True
        repoMarkupOn.Width = 100
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

        For ii As Integer = 1 To 10
            Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoCheckBox.HeaderText = "Is Tax On Base Amount " + clsCommon.myCstr(ii)
            repoCheckBox.Name = colIsTaxOnBaseAmount + clsCommon.myCstr(ii)
            repoCheckBox.ReadOnly = True
            repoCheckBox.IsVisible = False
            repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            repoCheckBox.WrapText = True
            gv1.MasterTemplate.Columns.Add(repoCheckBox)
        Next

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
        repoRequition.HeaderText = "Shipment No"
        repoRequition.Name = colOrderNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)


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

        Dim repoItemTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTaxCode = New GridViewTextBoxColumn()
        repoItemTaxCode.FormatString = ""
        repoItemTaxCode.HeaderText = "Item Tax Code"
        repoItemTaxCode.Name = colItemwiseTaxCode
        repoItemTaxCode.Width = 100
        repoItemTaxCode.IsVisible = False
        repoItemTaxCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTaxCode)

        Dim repoFOC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFOC.FormatString = ""
        repoFOC.HeaderText = "FOC"
        repoFOC.Name = ColFOC
        repoFOC.ReadOnly = True
        repoFOC.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoFOC)

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
    Sub LoadDispatchTerms()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CIF"
        dr("Name") = "CIF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FOB"
        dr("Name") = "FOB"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CF"
        dr("Name") = "C&F"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FE"
        dr("Name") = "Freight extra"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Others"
        dt.Rows.Add(dr)

        ddlDispatchTerms.DataSource = dt
        ddlDispatchTerms.ValueMember = "Code"
        ddlDispatchTerms.DisplayMember = "Name"
    End Sub

    Sub LoadPaymentTerms()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Advance"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Credit Period"
        dt.Rows.Add(dr)

        ddlPaymentTerms.DataSource = dt
        ddlPaymentTerms.ValueMember = "Code"
        ddlPaymentTerms.DisplayMember = "Name"
    End Sub
    Sub LoadBlankGridAC()
        gvAC.Rows.Clear()
        gvAC.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colACCode
        repoACCode.HeaderImage = My.Resources.search4
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
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
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

    Private Sub txtDiscPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave
        CalculateDiscountAmount()
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
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse e.Column Is gv1.Columns(colHeadDiscamt) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse e.Column Is gv1.Columns(colUnit) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                        If (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colHeadDiscamt) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse e.Column Is gv1.Columns(colDisPer) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal)) Then
                            If ((e.Column Is gv1.Columns(colQty))) Then
                                Dim dblPendingQty As Double = 0
                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0) Then
                                    dblPendingQty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                End If
                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0) Then
                                    Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    Dim dblDamageQty As Double = 0 'clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)
                                    If (dblEnteredQty + dblDamageQty) > dblPendingQty Then
                                        common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty) + ". Damage Quantity : " + clsCommon.myCstr(dblDamageQty))
                                        gv1.CurrentCell.Value = 0
                                    End If
                                End If
                            End If
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colUnitRate).Value) > 0 Then
                                findQtyandPromoSchemeCode(False)
                            End If
                            If clsCommon.myLen(txtInvNoForSupplementary.Value) > 0 Then
                                calculateTCSAmount()
                                If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colAmt) Then
                                    UpdateCurrentRow(gv1.CurrentRow.Index)
                                End If
                            End If


                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            Dim Rateqry As String = "select Item_Rate,MRP from TSPL_VENDOR_ITEM_DETAIL where Customer_Code='" + txtVendorNo.Value + "' and item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM='FC' and MRP='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value) + "' "
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
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            '' Added By abhishek as on 29 june 2012  when we change UOM type at run time then according to UOM Item rate will be change ----
            'SetVendorItemDetails()
        End If

    End Sub
    Private Sub findQtyandPromoSchemeCode(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtInvNoForSupplementary.Value) > 0 Then
            Exit Sub
        End If

        ' Dim dr1 As DataTable
        'Dim schemeCodeCol As String
        Dim LocCodeCol As String
        Dim LocNameCol As String
        Dim intRow As Integer
        Dim strOrderCode As String = ""
        LocCodeCol = clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value)
        LocNameCol = clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationName).Value)
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
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then

                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "Yes") = CompairStringResult.Equal Then
                    For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        For Each grow As GridViewRowInfo In gv1.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                                gv1.Rows.RemoveAt(grow.Index)
                            End If
                        Next
                    Next
                    '-------------fill cash scheme---------------------------------------
                    'Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(Index).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(Index).Cells(colQty).Value), txtVendorNo.Value)
                    'If obj_Cash IsNot Nothing Then
                    '    gv1.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    '    gv1.Rows(Index).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                    '    gv1.Rows(Index).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                    '    If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                    '        gv1.Rows(Index).Cells(colCashSchemeType).Value = "P"
                    '        gv1.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                    '    ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) > 0 Then
                    '        gv1.Rows(Index).Cells(colCashSchemeType).Value = "A"
                    '    End If
                    '    'gv1.Rows(Index).Cells(colSchemeItem).Value = "Yes"
                    'Else
                    '    gv1.Rows(Index).Cells(colCash_Amt).Value = Nothing
                    '    gv1.Rows(Index).Cells(colCash_Pers).Value = Nothing
                    '    gv1.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                    '    gv1.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                    '    gv1.Rows(Index).Cells(colSchemeItem).Value = "No"

                    '    '==========if cash scheme is there but no quantitive or volumn then also scheme item set to Y
                    '    'If clsCommon.myLen(gv1.Rows(Index).Cells(colMainIcode).Value) > 0 Then
                    '    '    gv1.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    '    'End If
                    'End If
                    '------------------------------------------------------------------


                    Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value))
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
                            Dim MainItemUnit = gv1.Rows(Index).Cells(colUnitRate).Value
                            Dim MainItemQty As Double = gv1.Rows(Index).Cells(colQty).Value
                            Dim MainSaleOrderCode As String = clsCommon.myCstr(gv1.Rows(Index).Cells(colOrderNo).Value)
                            '-------------------------------------------------------------

                            '-------------------------------------------------------------

                            gv1.Rows.AddNew()

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intRow + 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objtr.Schm_Icode, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = LocCodeCol
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = LocNameCol
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = ""
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = ""
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Schm_Item_Uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = objtr.Schm_Item_Uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = "Yes"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objtr.Schm_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value), 0), 2)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = MainItemCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = MainItemQty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = MainItemUnit
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = MainSaleOrderCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = clsDBFuncationality.getSingleValue("select CSA_TYPE from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "' ")

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True

                            Dim dblConvF As Double = clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value, Nothing)
                            gv1.Rows(intRow).Cells(colConvF).Value = dblConvF
                            gv1.Rows(intRow).Cells(colTotItemWt).Value = dblConvF * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colItemWeight).Value) * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colQty).Value)
                            Dim qry As String = ""
                            Dim Weight_UOM As String = ""
                            Dim SKU_VALUE As Decimal = 0

                            qry = "select Item_Code,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'"
                            Dim dtWt As New DataTable()
                            dtWt = clsDBFuncationality.GetDataTable(qry)
                            If dtWt.Rows.Count > 0 Then
                                SKU_VALUE = clsCommon.myCdbl(dtWt.Rows(0).Item("Weight_Value"))
                                Weight_UOM = clsCommon.myCstr(dtWt.Rows(0).Item("Weight_UOM"))
                            End If

                            'If clsCommon.myLen(Weight_UOM) <= 0 Then
                            '    clsCommon.MyMessageBoxShow("Weight UOM not defined for Item  " & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "")
                            '    Exit Sub
                            'End If

                            gv1.Rows.Move(gv1.Rows.Count - 1, Index + 1)


                            'End If
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
    Private Sub setGridFocus()
        Try
            'Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If

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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Row Type")
            Exit Sub
        End If

        '---------------------07/08/2014-----n-level pivot cat.-----
        Dim pivotheader As String = ""
        pivotheader = clsItemMaster.GetNLevelPivotHeader()
        '------------------------------------------------------------------

        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then
            'If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Item Type")
            '    SetBlankOfItemColumns()
            '    cboItemType.Focus()
            '    Exit Sub
            'End If
            Dim qry As String = "SELECT Item_Code as Item,Item_Desc as ItemDesc,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate,Item_Basic_Price as BasicRate, " & _
            "Weight_Value,markup_on,markup_percent,landing_cost,Purchase_Cost,TAX1_Rate as Tax1Rate,TAX2_Rate as  Tax2Rate,TAX3_Rate as Tax3Rate,TAX4_Rate as Tax4Rate, " & _
            "TAX5_Rate as Tax5Rate,TAX6_Rate as Tax6Rate,TAX7_Rate as Tax7Rate,TAX8_Rate as Tax8Rate, " & _
            "TAX9_Rate as Tax9Rate,TAX10_Rate as Tax10Rate,TAX1 ,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7, TAX8,TAX9,TAX10  " & _
            "FROM ( SELECT TSPL_ITEM_PRICE_MASTER.Purchase_Cost,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_PRICE_MASTER.abatement_rate,TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc,  " & _
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
            "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  " & _
            "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            ")xxx Where  Price_Code='" & txtPriceCode.Text & "' and  Tax_group='" & txtTaxGroup.Value & "'  AND " & _
            "Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" & txtBillToLocation.Value & "' AND Item_Qty <> 0) " & _
            "Order By Sku_Seq,Start_Date,UOM desc"

            If clsCommon.myLen(pivotheader) > 0 Then
                qry = "select * from (select a.DESCRIPTION,a.cat_value,b.* from (SELECT Item_Code as Item,Item_Desc as ItemDesc,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate,Item_Basic_Price as BasicRate, " & _
           "Weight_Value,markup_on,markup_percent,landing_cost,Purchase_Cost,TAX1_Rate as Tax1Rate,TAX2_Rate as  Tax2Rate,TAX3_Rate as Tax3Rate,TAX4_Rate as Tax4Rate, " & _
           "TAX5_Rate as Tax5Rate,TAX6_Rate as Tax6Rate,TAX7_Rate as Tax7Rate,TAX8_Rate as Tax8Rate, " & _
           "TAX9_Rate as Tax9Rate,TAX10_Rate as Tax10Rate,TAX1 ,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7, TAX8,TAX9,TAX10  " & _
           "FROM ( SELECT TSPL_ITEM_PRICE_MASTER.Purchase_Cost,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_PRICE_MASTER.abatement_rate,TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc,  " & _
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
           "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  " & _
           "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
           ")xxx Where  Price_Code='" & txtPriceCode.Text & "' and  Tax_group='" & txtTaxGroup.Value & "'  AND " & _
           "Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" & txtBillToLocation.Value & "' AND Item_Qty <> 0) " & _
           ")b left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=b.Item) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
                '"Order By Sku_Seq,Start_Date,UOM desc"
            End If


            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ICode", qry)
            If Not dr Is Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                    SetitemWiseTaxSetting(True, True)
                    gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item"))
                    gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dr("Unit")), clsCommon.myCstr(dr("MRP")))
                    'gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, clsCommon.myCdbl(dr("MRP")), clsCommon.myCstr(dr("Unit")), txtDocNo.Value, txtDate.Value)
                    If gv1.CurrentRow.Cells(ColActualBalQty).Value = 0 Then
                        isCellValueChangedOpen = False

                        Throw New Exception("Qty is not avaliable for item " & gv1.CurrentRow.Cells(colICode).Value & " at location " & txtBillToLocation.Value & " ")
                    End If
                    gv1.CurrentRow.Cells(colIName).Value = clsCommon.myCstr(dr("ItemDesc"))
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item")), Nothing)
                    gv1.CurrentRow.Cells(colPriceDateColumn).Value = clsCommon.myCstr(dr("Start_Date"))
                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
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

                    gv1.CurrentRow.Cells(colMarkupOn).Value = clsCommon.myCstr(dr("markup_on"))
                    gv1.CurrentRow.Cells(colMarkUpPercentage).Value = clsCommon.myCdbl(dr("markup_percent"))
                    gv1.CurrentRow.Cells(colLandingCost).Value = clsCommon.myCdbl(dr("landing_cost"))
                    gv1.CurrentRow.Cells(colPurCost).Value = clsCommon.myCdbl(dr("Purchase_Cost"))
                    gv1.CurrentRow.Cells(colOrgCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                    Dim dblConvF As Double
                    dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colConvF).Value = dblConvF

                End If
            Else
                SetBlankOfItemColumns()
            End If
        End If
        SetitemWiseTaxSetting(True, True)
    End Sub


    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
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

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotLandedCost As Double = 0
        Dim dblCashDisAmt As Double = 0
        Dim dblHeadDisAmt As Double = 0
        Dim dblCommAmt As Double = 0

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
        Dim dblHeadDisPerAmt As Double = 0


        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 0 Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblCashDisAmt = dblCashDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalCustDiscount).Value)
                dblHeadDisAmt = dblHeadDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDiscamt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                dblHeadDisPerAmt = dblHeadDisPerAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDisPerAmt).Value)
                dblCommAmt = dblCommAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCommAmt).Value)


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
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                            dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt1 = (dblTaxBaseAmt1 * txtTCSTaxRate.Value) / 100
                        End If
                        ''richa 11 Dec,2020
                        ''If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                        ''    If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                        ''        dblTaxAmt1 = 0
                        ''    End If
                        ''End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 2
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1)
                            dblTaxBaseAmt2 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt2 = (dblTaxBaseAmt2 * txtTCSTaxRate.Value) / 100
                        End If
                        ''richa 11 Dec,2020
                        'If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                        '    If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                        '        dblTaxAmt2 = 0
                        '    End If
                        'End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 3
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2)
                            dblTaxBaseAmt3 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt3 = (dblTaxBaseAmt3 * txtTCSTaxRate.Value) / 100
                        End If
                        ''richa 11 Dec,2020
                        'If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                        '    If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                        '        dblTaxAmt3 = 0
                        '        If gv1.RowCount > 0 Then
                        '            For m As Integer = 0 To gv1.Rows.Count - 1
                        '                gv1.Rows(m).Cells("COLTAXRATE3").Value = 0
                        '            Next
                        '        End If
                        '    End If
                        'End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 4
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3)
                            dblTaxBaseAmt4 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt4 = (dblTaxBaseAmt4 * txtTCSTaxRate.Value) / 100
                        End If
                        ''richa 11 Dec,2020
                        'If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                        '    If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                        '        dblTaxAmt4 = 0
                        '    End If
                        'End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
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
        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
        Next


        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)

        dblNetAmt = dblNetAmt + dblACAmount
        lblInvoiceDiscAmt.Text = dblHeadDisAmt + dblHeadDisPerAmt
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        'Dim dblROAmt As Decimal = Math.Round(Math.Round(dblNetAmt, 0) - dblNetAmt, 2)

        If Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) > clsCommon.myCdbl(lblTotRAmt.Text) Then
            TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
        Else
            TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text)) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
        End If
        lblTotRAmt1.Text = lblTotRAmt.Text
        lblCommAmt.Text = clsCommon.myFormat(dblCommAmt)
    End Sub

    'Private Function GetBaseOtherTaxableAmount(ByVal intEndCol As Integer) As Double
    ''Dim dblRetVal As Double = 0
    ''For ii As Integer = 0 To intEndCol - 1
    ''    If clsCommon.myCBool(gv2.Rows(ii).Cells(colTIsTaxable).Value) Then
    ''        dblRetVal = dblRetVal + clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
    ''    End If
    ''Next
    ''Return dblRetVal
    'End Function

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
        LoadBlankGridTax()
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        'gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        ''gv1.Rows.AddNew()
        chkInternal.Checked = False
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        btnHistory.Enabled = False

        txtDate.Enabled = True
        txtVendorNo.Enabled = True
        chkAgainstCForm.Checked = False

        If AllowtoChangeTCSBaseAmount = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        txtTCSTaxRate.Value = 0
        SaleInvoiceDate = Nothing
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave() As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            'If CalculateTaxRatefromItemwsieTaxOnSale = 1 Then
            '    SetitemWiseTaxSetting(True, False)
            'End If
            RefreshReqNo()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Customer")
                txtVendorNo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Tax Group")
                txtTaxGroup.Focus()
                Return False
            End If
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Bill to Location")
                txtBillToLocation.Focus()
                Return False
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Invoice No Not found to save")
                txtDocNo.Focus()
                Return False
            End If
            'If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Item Type")
            '    cboItemType.Focus()
            '    Return False
            'End If

            If AllowChangeInvoiceType Then
                If clsCommon.myLen(ddlInvoiceType.SelectedValue) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select invoice  Type for creating invoice")
                    cboItemType.Focus()
                    Return False
                End If
            Else
                InvoiceType()
            End If

            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colOrderNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
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
                        If dblQty > dblPendingQty Then
                            common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If
                    End If

                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If

                    'If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                    '    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                    '        Dim strInICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                    '        Dim strInUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)


                    '        If clsCommon.CompairString(strICode, strInICode) = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal Then
                    '            common.clsCommon.MyMessageBoxShow("Item Code " + strICode + " and Unit " + strUOM + " is repeted at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                    '            Return False
                    '        End If
                    '    Next
                    'End If




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

            'If clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
            '    Dim qry As String = "select Type  from TSPL_TAX_MASTER where Tax_Code ='" + clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value) + "' "
            '    If Not clsCommon.CompairString("E", clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) = CompairStringResult.Equal Then
            '        common.clsCommon.MyMessageBoxShow("Tax should be excisable for excisable location")
            '        Return False
            '    End If
            'End If

            'clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            clsPSShipmentHead.IsValidCustomer(arrReqNo, txtVendorNo.Value)
            ''UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrICode)
            If Not (intx = arrICode.Count OrElse intx = 0) Then
                'Throw New Exception("All item should be of Excisable or NonExcisable")
                'Else
                '    If intx > 0 AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                '        Qry = "select 1 from TSPL_Tax_Group_Master where Tax_Group_Code='" + txtTaxGroup.Value + "' and Is_Tax_Exempted=1"
                '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                '            Throw New Exception("Please select of Excisable or not")
                '        End If
                '    End If
            End If
            If intx > 0 Then
                Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")"))
            Else
                Item_TaxType = 0
            End If
            If clsCommon.myLen(txtInvNoForSupplementary.Value) > 0 Then
                If clsCommon.myLen(cboSupplementaryType.SelectedValue) <= 0 Then
                    cboSupplementaryType.Focus()
                    Throw New Exception("Please select Supplementary Type")
                End If
            End If
            If clsCommon.myCdbl(txtDiscAmt.Text) > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
                isCellValueChangedOpen = False
                Throw New Exception("Discount amount cannot be greater than Doc amount")

            End If

            If AllowtoChangeTCSBaseAmount Then
                If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                    Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsPSInvoiceHead()
                If IsDairyModule = False Then
                    obj.Screen_Type = ""
                Else
                    obj.Screen_Type = "DS"
                End If
                obj.SO_Validity = txtSOvalidity.Value
                obj.Commission_Apply = IIf(chkCommApply.Checked, 1, 0)
                obj.Total_Comm_Amt = lblCommAmt.Text
                obj.Dispatch_date = txtDispatchDate.Value
                obj.Vehicle_Capacity = txtVehicleCapacity.Value
                obj.Payment_Terms = ddlPaymentTerms.SelectedValue
                obj.Dispatch_Terms = ddlDispatchTerms.SelectedValue
                obj.Dispatch_Period = txtDispatchPeriod.Value
                obj.Road_Permit_No = txtRoadPermitNo.Text
                obj.Invoice_No_For_Supplementary = txtInvNoForSupplementary.Value
                obj.Supplementary_Type = clsCommon.myCstr(cboSupplementaryType.SelectedValue)
                obj.Form_38_No = txtForm38.Text
                obj.Cust_PO_No = txtPONo.Text
                obj.Route_No = txtRouteNo.Value
                obj.Route_Desc = lblRouteDesc.Text
                obj.Price_Code = txtPriceCode.Text
                obj.Price_Group_Code = txtPriceGroupCode.Text
                obj.HeadDisc_Per = txtDiscPer.Text
                obj.HeadDisc_Per = txtDiscPer.Text
                If obj.HeadDisc_Per > 0 Then
                    obj.HeadDisc_PerAmt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_Amt = 0
                Else
                    obj.HeadDisc_Amt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_PerAmt = 0
                End If
                obj.Invoice_Type = ddlInvoiceType.SelectedValue
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Customer_Code = txtVendorNo.Value
                obj.Customer_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Inv_Date = clsCommon.GetPrintDate(dtpInvoice.Value, "dd/MMM/yyyy")
                obj.podate = txtpodate.Text
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallan.Value, "dd/MMM/yyyy")
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Inv_No = txtInvNo.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sub_Location_code = txtSubLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Salesman_Code = txtSalesman.Value
                obj.Salesman_Name = lblSalesman.Text
                obj.Is_Internal = chkInternal.Checked
                obj.Against_C_Form = chkAgainstCForm.Checked
                obj.PROJECT_ID = fndProject.Text
                '-----------------richa 26/06/2014 Ticket No .BM00000002982-------  
                If clsCommon.myCdbl(txtMannaulInvoiceNo.Value) > 0 Then
                    obj.Mannual_Document_Code = txtMannaulInvoiceNo.Value
                Else

                    obj.InvoiceManualNowithPrefix = TxtInvoiceManualNoWithPrefix.Text
                End If

                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                '----------------------------------------------------------------
                'obj.Mannual_Document_Code = txtMannaulInvoiceNo.Value
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
                obj.RoundOffAmount = clsCommon.myCdbl(TxtRoundoff.Text)
                obj.Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                obj.Carrier = txtCarrier.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.Vehicle_Code = txtVehcileCode.Text
                obj.GRNo = txtGRNo.Text
                obj.GENo = txtGENo.Text

                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text

                obj.Against_Shipment_No = txtReqNo.Value
                obj.Item_Tax_Type = Item_TaxType

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

                obj.Is_Create_Auto_Receipt = chkCreateAutoReceipt.Checked
                obj.Arr = New List(Of clsPSInvoiceHeadDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsPSInvoiceHeadDetail()
                    objTr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                    objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                    objTr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
                    objTr.VS_CashSchemeCode = clsCommon.myCstr(grow.Cells(colVS_CashSchemeCode).Value)
                    objTr.VS_Cash_Amt = clsCommon.myCdbl(grow.Cells(colVS_Cash_Amt).Value)
                    objTr.VS_ltrInCrate = clsCommon.myCdbl(grow.Cells(colVS_ltrInCrate).Value)

                    objTr.RATE_UOM = clsCommon.myCstr(grow.Cells(colUnitRate).Value)
                    objTr.Alternate_UOM = clsCommon.myCstr(grow.Cells(colUnitALter).Value)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

                    objTr.Free_Qty = clsCommon.myCdbl(grow.Cells(colFreeQty).Value)

                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Shipment_Code = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
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
                    objTr.Location = clsCommon.myCstr(grow.Cells(colLocationCode).Value)


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
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(grow.Cells(colIsMannualAmt).Value)

                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

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
                    objTr.Commission_Rate = clsCommon.myCdbl(grow.Cells(colCommRate).Value)
                    objTr.Commission_Party = clsCommon.myCstr(grow.Cells(ColCommParty).Value)
                    objTr.Commission_Amt = clsCommon.myCdbl(grow.Cells(ColCommAmt).Value)
                    objTr.Amt_Less_Commission = clsCommon.myCdbl(grow.Cells(ColAmtAfterCOmm).Value)
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(grow.Cells(colItemwiseTaxCode).Value)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
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
                '' end CurrencyConversion
                If clsPSInvoiceHead.checkSaveNotification(obj, Nothing) Then
                    If (obj.SaveData(obj, isNewEntry)) Then
                        UcAttachment1.SaveData(obj.Document_Code)
                        If ChekPostBtn = False Then
                            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                        End If
                        LoadData(obj.Document_Code, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            Dim obj As New clsPSInvoiceHead()
            obj = clsPSInvoiceHead.GetData(strCode, NavTyep, "'T','R','E','I','A','N'", Nothing, IsDairyModule)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
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
                txtReqNo.Value = obj.Against_Shipment_No
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    If obj.Is_Delivered = 1 Then
                        btnDeliveredTo.Enabled = False
                    Else
                        btnDeliveredTo.Enabled = True
                    End If
                    RadButton1.Enabled = True
                Else
                    Dim dblPost As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Status from TSPL_SD_SHIPMENT_HEAD where Document_Code='" & txtReqNo.Value & "'"))
                    If dblPost = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Post this Shipment No " & txtReqNo.Value & " before posting invoice ")
                        btnPost.Enabled = False
                        btnDelete.Enabled = False
                    End If
                    RadButton1.Enabled = False
                End If

                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Customer_Code)
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtpodate.Text = obj.podate
                txtVendorNo.Value = obj.Customer_Code
                txtPONo.Text = obj.Cust_PO_No
                txtForm38.Text = obj.Form_38_No
                txtDate.Enabled = False
                txtVendorNo.Enabled = False
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
                txtRoadPermitNo.Text = obj.Road_Permit_No
                FlagDocumentIsTaxable = obj.Is_Taxable
                EInvoiceType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", obj.Document_Code, Nothing)
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
                txtSubLocation.Value = obj.Sub_Location_code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocation.Text = ""
                End If

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

                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)

                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                lblTotRAmt1.Text = lblTotRAmt.Text
                TxtRoundoff.Text = clsCommon.myFormat(obj.RoundOffAmount)
                lblBillToLocation.Text = obj.BillToLocationName
                lblShipToLocation.Text = obj.ShipToLocationName
                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName
                'richa Ticket No.BM00000002982
                txtMannaulInvoiceNo.Value = obj.Mannual_Document_Code
                TxtInvoiceManualNoWithPrefix.Text = obj.InvoiceManualNowithPrefix
                '--------------------------------
                txtCarrier.Text = obj.Carrier
                txtVehicleNo.Text = obj.VehicleNo
                txtVehcileCode.Text = obj.Vehicle_Code
                txtGRNo.Text = obj.GRNo
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If

                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = obj.Salesman_Name
                chkAgainstCForm.Checked = obj.Against_C_Form
                txtInvNoForSupplementary.Value = obj.Invoice_No_For_Supplementary
                cboSupplementaryType.SelectedValue = obj.Supplementary_Type
                If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 Then
                    SaleInvoiceDate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Document_date from tspl_sd_sale_invoice_head where document_code='" & txtInvNoForSupplementary.Value & "'"))
                End If
                fndProject.Text = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtPriceCode.Text = obj.Price_Code
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
                txtPriceGroupCode.Text = obj.Price_Group_Code
                ddlInvoiceType.SelectedValue = obj.Invoice_Type

                txtSOvalidity.Value = obj.SO_Validity
                chkCommApply.Checked = IIf(obj.Commission_Apply = 1, True, False)
                If clsCommon.myLen(obj.Dispatch_date) > 0 Then
                    txtDispatchDate.Value = obj.Dispatch_date
                End If
                ddlDispatchTerms.SelectedValue = obj.Dispatch_Terms
                ddlPaymentTerms.SelectedValue = obj.Payment_Terms
                txtDispatchPeriod.Value = obj.Dispatch_Period
                txtVehicleCapacity.Value = obj.Vehicle_Capacity
                lblCommAmt.Text = obj.Total_Comm_Amt
                Item_TaxType = obj.Item_Tax_Type
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
                chkCreateAutoReceipt.Checked = obj.Is_Create_Auto_Receipt
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsPSInvoiceHeadDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = objTr.Cash_Scheme_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = objTr.Cash_Scheme_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeCode).Value = objTr.Cash_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = objTr.Cash_Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = objTr.Scheme_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = objTr.Scheme_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = objTr.Scheme_Item_UOM

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = objTr.RATE_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitALter).Value = objTr.Alternate_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objTr.Item_Code)

                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = objTr.so_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreeQty).Value = objTr.Free_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Shipment_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurCost).Value = objTr.Purchase_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.OrgRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If
                        'If clsCommon.myLen(objTr.Shipment_Code) > 0 Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPSShipmentHeadDetail.GetBalanceSRNQty(objTr.Shipment_Code, objTr.Item_Code, obj.Document_Code, objTr.Unit_code, objTr.MRP, objTr.Assessable)
                        'End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objTr.Balance_Qty
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = objTr.Abatement_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmount).Value = objTr.Abatement_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value))
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaDDisPer).Value = objTr.HeadDiscPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDisPerAmt).Value = objTr.HeadDiscPerAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVS_CashSchemeCode).Value = objTr.VS_CashSchemeCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVS_Cash_Amt).Value = objTr.VS_Cash_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVS_ltrInCrate).Value = objTr.VS_ltrInCrate


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommRate).Value = objTr.Commission_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value = objTr.Commission_Party
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommPartyName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommAmt).Value = objTr.Commission_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmtAfterCOmm).Value = objTr.Amt_Less_Commission
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objTr.ItemwiseTaxCode
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
                    'If obj.Status = ERPTransactionStatus.Pending Then
                    '    gv1.Rows.AddNew()
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    '    gvAC.Rows.AddNew()
                    'End If
                End If
                SetitemWiseTaxOnlySetting()
                ' ''RefreshReqNo()
                ' ''RefreshGRPNo()
                ''For Custom Fields

                ''========= added by Parteek 31-1-2017 Cancel Document
                If ShowDocumentCancel = True Then
                    Dim QryPost As String = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select status from TSPL_SD_SALE_INVOICE_HEAD where document_Code='" & txtDocNo.Value & "'"))

                    If clsCommon.myCBool(QryPost) = True Then
                        If ShowDocumentCancel = True Then
                            btnCancel.Visible = True
                            btnReverseAndUnpost.Enabled = False
                        Else
                            btnReverseAndUnpost.Enabled = True
                        End If
                    End If
                    Dim QryCancel As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CancelFlag from TSPL_SD_SALE_INVOICE_HEAD where document_Code='" & txtDocNo.Value & "'"))
                    If clsCommon.CompairString(QryCancel, "1") = CompairStringResult.Equal Then
                        btnCancel.Enabled = False
                    Else
                        btnCancel.Enabled = True
                    End If
                    If clsCommon.CompairString(obj.CancelFlag, "1") = CompairStringResult.Equal Then
                        UsLock1.Status = ERPTransactionStatus.Cancel
                    End If
                End If


                '=========End ============

                'Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", txtDocNo.Value, Nothing)
                If FlagDocumentIsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(txtDate.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                    btnReverseAndUnpost.Enabled = False
                    If obj.Status = ERPTransactionStatus.Approved Then
                        RadButton1.Enabled = True
                    ElseIf obj.Status = ERPTransactionStatus.Pending Then
                        RadButton1.Enabled = False
                    End If
                End If

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
                UcAttachment1.LoadData(obj.Document_Code)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
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
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try

            Dim isSaved As Boolean = True
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strCode = "PI Cancel"
                frm.strType = "PI Cancel"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    Dim iscancel As Boolean = False
                    If common.clsCommon.MyMessageBoxShow("Do you want to cancel the Product Invice?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                        Dim qrySaveCancel = "Update TSPL_SD_SALE_INVOICE_HEAD set CancelFlag=1 where Document_Code='" & txtDocNo.Value & "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qrySaveCancel)
                        If isSaved = True Then
                            clsCommon.MyMessageBoxShow("PI cancelled successfully!")
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If

                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        'Task No-TEC/23/07/19-000951
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNSaleInvoice)

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

            ''------------------------code for attchament-------------------------------------
            'Dim strRptPath As String = ""
            'If obj.atchmnt = "Y" Then
            '    atchqry = GetAtachmntPrint(txtDocNo.Value)
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            '    If dt1.Rows.Count > 0 Then
            '        SetItemWiseTax(dt1, txtDocNo.Value)
            '        Dim frmCRV As New frmCrystalReportViewer()
            '        strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shipment Detail")
            '        frmCRV = Nothing
            '    End If
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
            '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    lstReceiptents.Add(emailId)

            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
            'Next
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)


            'If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
            '    SMSSENDONLY(False)
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where cust_code='" + txtVendorNo.Value + "'"))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + FORMTYPE + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Code, txtVendorNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Cust_Name, lblVendorName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))

                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.SalePerson_Code, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.SalePerson_Name, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ShippedCrate, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ShippedCan, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.OutStandingAmt, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CRT, "")
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CAN, "")

                End If


                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Code, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Code, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Name, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ShippedCrate, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ShippedCan, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.OutStandingAmt, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CRT, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CAN, "")
                End If

            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                Dim strRptPath As String = ""
                atchqry = GetAtachmntPrint(txtDocNo.Value)
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
                If dt1.Rows.Count > 0 Then
                    SetItemWiseTax(dt1, txtDocNo.Value)
                    Dim frmCRV As New frmCrystalReportViewer()
                    'strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shipment Detail")
                    strRptPath = frmCRV.funreport(True, CrystalReportFolder.NewSalesReports, dt1, "crptShipment", "Shipment Detail")
                    frmCRV = Nothing
                    objEmailH.Attachment_1_Path = strRptPath
                End If
            End If

            For Each strUser As String In lstUsers
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
                objEmailH.SaveData(FORMTYPE, objEmailH, Nothing)
                objEmailH = Nothing
            End If
            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                objSMSH.SaveData(FORMTYPE, objSMSH, Nothing)
                objSMSH = Nothing
            End If
            clsCommon.MyMessageBoxShow("E-Mail/SMS Send Successfully", Me.Text)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub SMSSENDONLY(ByVal isPost As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNSaleInvoice)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'If clsCommon.myLen(obj.smsbody) <= 0 Then
            '    Return
            'End If

            'Dim strMes As String = obj.smsbody
            'If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'End If
            'If strMes.Contains(clsEmailSMSConstants.CustomerNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.CustomerNo, txtVendorNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.CustomerName) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.CustomerName, lblVendorName.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, "")
            'End If
            'If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'End If

            'Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

            'If clsSMSSend.SendSMS(clsUserMgtCode.frmSNSaleInvoice, strMes, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where cust_code='" + txtVendorNo.Value + "'"))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + FORMTYPE + "'", Nothing)

            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Code, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Cust_Name, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Code, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.SalePerson_Name, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ShippedCrate, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ShippedCan, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.OutStandingAmt, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CRT, "")
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CAN, "")
                End If

            End If


            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.UserCode, txtVendorNo.Value)
            End If


            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                objSMSH.SaveData(FORMTYPE, objSMSH, Nothing)
                objSMSH = Nothing
                clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsPSInvoiceHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
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

                If clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                    SMSSENDONLY(True)
                End If

                If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    funPrint(txtDocNo.Value)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsPSInvoiceHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-SRN"
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            '-------richa 30/07/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            Dim qst As String = ""
            Dim strcondition As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                strcondition = "and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ")   and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'"
            End If
            qst = "select count(*) from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" + txtDocNo.Value + "'"
            If IsDairyModule = False Then
                qst += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and Screen_Type='' "
            Else
                qst += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') and Screen_Type='DS' "
            End If

            qst += " and Invoice_Type in ('T','R','E','A','I') " + strcondition + " "

            '-----------------------------------------------------
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
                OpenICodeList(True)
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
        ElseIf isNewEntry AndAlso e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                '-----------------richa 26/06/2014 Ticket No .BM00000002982------------
                Dim desc As String = ""
                Dim trans As SqlTransaction
                trans = clsDBFuncationality.GetTransactin()
                'desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select IsNull(Description,0) as Description  from TSPL_FIXED_PARAMETER where type='" + clsFixedParameterType.InvoiceManualNoWithPrefix + "'"))
                desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InvoiceManualNoWithPrefix, clsFixedParameterCode.InvoiceManualNoWithPrefix, trans))
                'If desc = "0" Then
                If clsCommon.CompairString(desc, "0") = CompairStringResult.Equal Then
                    txtMannaulInvoiceNo.Visible = True
                    TxtInvoiceManualNoWithPrefix.Visible = False
                Else
                    txtMannaulInvoiceNo.Visible = False
                    TxtInvoiceManualNoWithPrefix.Visible = True
                End If
                '-----------------------------------------------------------
                pnlMannualInvoiceNo.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                       "TSPL_SD_SALE_INVOICE_HEAD (In case of Auto Sale Invoice) " + Environment.NewLine +
                                       "TSPL_SD_SALE_INVOICE_DETAIL " + Environment.NewLine +
                                       "TSPL_Customer_Invoice_Head (For AR Invoice Entry  - After Posting)  " + Environment.NewLine +
                                       "TSPL_Customer_Invoice_Detail (After Posting)  " + Environment.NewLine +
                                       "TSPL_JOURNAL_MASTER (Journal Voucher Entry - For invoice  - After Posting )  " + Environment.NewLine +
                                       "TSPL_JOURNAL_DETAILS (After Posting ")
            'done by priti TEC/15/03/18-000092
            'Dim frm As New FrmPWD(Nothing)
            'frm.strType = clsFixedParameterType.SIRC
            'frm.strCode = clsFixedParameterCode.SIReversAndCreate
            'frm.ShowDialog()
            'If frm.isPasswordCorrect Then
            '    If ShowDocumentCancel = True Then
            '        clsCommon.MyMessageBoxShow("No Reverse Document Apply", Me.Text)
            '    Else
            '        btnReverseAndUnpost.Visible = True
            '    End If

            'End If
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        txtTermCode.Value = ClsReceivablePaymentTerms.getFinderWithSaleType(txtTermCode.Value, "P", isButtonClicked)
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
        If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
            txttcstaxbaseamount.Value = 1
            txttcstaxbaseamount.Value = 0
        End If

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
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))
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
                                        Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & txtVendorNo.Value & "'")) = 1, True, False)
                                        If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
                                    Else
                                        Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & txtVendorNo.Value & "'"))
                                        If clsCommon.myLen(panno) > 0 Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If

                                    End If

                                    ''richa 30 Nov,2020
                                    If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                    End If
                                Else
                                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                End If
                            Else
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                                ''richa 30 Nov,2020
                                If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                End If
                            End If
                        Else
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                        End If
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                    Else
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
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

        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If (CalculateTaxRatefromItemwsieTaxOnSale = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                            If isChangeRate Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                        Else
                            Dim objTM As clsItemWiseTaxAuthority
                            objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "S")
                            If objTM IsNot Nothing Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = objTM.HCODE
                            End If
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
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
                            If (CalculateTaxRatefromItemwsieTaxOnSale = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                                If isChangeRate Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                Dim objTM As clsItemWiseTaxAuthority
                                objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "S")
                                If objTM IsNot Nothing Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                    gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                End If
                            End If
                            ''tcs tax rate
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                                    If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))
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
                                                Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & txtVendorNo.Value & "'")) = 1, True, False)
                                                If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
                                            Else
                                                Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & txtVendorNo.Value & "'"))
                                                If clsCommon.myLen(panno) > 0 Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
                                            End If

                                            ''richa 30 Nov,2020
                                            If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                            End If
                                        Else
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                        End If
                                    Else
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                                        ''richa 30 Nov,2020
                                        If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                        End If
                                    End If
                                Else
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                End If

                            End If


                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
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
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        btnHistory.Enabled = True
        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Location first")
            Exit Sub
        End If
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman   "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        If clsCommon.myLen(strwherecls) = 0 Then
            txtVendorNo.Value = clsCommon.ShowSelectForm("ShipmentVendorFndr", qry, "Code", "", txtVendorNo.Value, "Code", isButtonClicked)
        Else
            txtVendorNo.Value = clsCommon.ShowSelectForm("ShipmentVendorFndr", qry, "Code", " TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtVendorNo.Value, "Code", isButtonClicked)

        End If
        '-----------------------------------------------------
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

        '''' priti change start here
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
            qry = "select Price_Code,price_CodeNon,State from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"
            dt = clsDBFuncationality.GetDataTable(qry)

            If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(loc, "Y") = CompairStringResult.Equal Then
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            Else
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
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


        '''' priti change ends here
        SetTaxDetails()
        SetTermDetails()
    End Sub
    Sub InvoiceType()
        If AllowChangeInvoiceType = False Then
            Dim dt As DataTable
            Dim qry As String
            Dim strloc As String
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
            strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'")) = "T", True, False)
            If strExcise = True Then
                ddlInvoiceType.SelectedValue = "E"
            End If
        Else
            ddlInvoiceType.SelectedValue = ""
        End If
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
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))




    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Location first")
            txtBillToLocation.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtVendorNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Customer first")
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
        Dim frm As New frmPendingShipmentPS()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.ShowDialog()
        LoadBlankGrid()
        Dim objOrderHead As clsPSShipmentHead = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).Document_Code) > 0 Then
                objOrderHead = clsPSShipmentHead.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)
                If objOrderHead IsNot Nothing AndAlso clsCommon.myLen(objOrderHead.Document_Code) > 0 Then
                    '' currency details
                    txtCurrencyCode.Value = objOrderHead.CURRENCY_CODE
                    Me.txtConversionRate.Text = objOrderHead.ConvRate
                    If objOrderHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objOrderHead.ApplicableFrom
                    End If
                    If clsCommon.myLen(txtRoadPermitNo.Text) <= 0 Then
                        txtRoadPermitNo.Text = objOrderHead.Road_Permit_No
                    End If
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objOrderHead.Ref_No
                    End If
                    If clsCommon.myLen(txtRoadPermitNo.Text) <= 0 Then
                        txtRoadPermitNo.Text = objOrderHead.Road_Permit_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objOrderHead.Description
                    End If
                    If clsCommon.myLen(txtPONo.Text) <= 0 Then
                        txtPONo.Text = objOrderHead.Cust_PO_No
                    End If
                    If clsCommon.myLen(objOrderHead.Podate) > 0 Then
                        txtpodate.Text = objOrderHead.Podate
                    End If
                    If clsCommon.myLen(txtInvNo.Text) <= 0 Then
                        'txtRemarks.Text = objOrderHead.Remarks
                    End If
                    If (clsCommon.myLen(txtShipToLocation.Value)) <= 0 Then
                        txtShipToLocation.Value = objOrderHead.Ship_To_Location
                    End If
                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objOrderHead.Item_Type
                    End If
                    If (clsCommon.myLen(txtDept.Value) <= 0) Then
                        txtDept.Value = objOrderHead.Dept
                        lblDept.Text = objOrderHead.Dept_Desc
                    End If
                    If (clsCommon.myLen(txtVehcileCode.Text) <= 0) Then
                        txtVehcileCode.Text = objOrderHead.Vehicle_Code
                        txtVehicleNo.Text = objOrderHead.VehicleNo
                    End If
                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objOrderHead.Bill_To_Location
                        lblBillToLocation.Text = objOrderHead.BillToLocationName
                    End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)


                        txtDate.Enabled = False
                        txtVendorNo.Enabled = False
                        chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
                    End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objOrderHead.Terms_Code
                        lblTermName.Text = objOrderHead.TermsName

                        If objOrderHead.Due_Date IsNot Nothing Then
                            txtDueDate.Value = objOrderHead.Due_Date
                        End If


                    End If


                    If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                        txtTaxGroup.Value = objOrderHead.Tax_Group
                        SetTaxDetails()
                    End If

                    If clsCommon.myLen(txtSalesman.Value) <= 0 Then
                        txtSalesman.Value = objOrderHead.Salesman_Code
                        lblSalesman.Text = objOrderHead.Salesman_Name
                    End If
                    If (clsCommon.myLen(lblProject.Text) <= 0) Then
                        fndProject.Text = objOrderHead.PROJECT_ID
                        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")
                    End If
                    If (clsCommon.myLen(txtRouteNo.Value)) <= 0 Then
                        txtRouteNo.Value = objOrderHead.Route_No
                        lblRouteDesc.Text = objOrderHead.Route_Desc
                    End If
                    If (clsCommon.myLen(txtPriceCode.Text)) <= 0 Then
                        txtPriceCode.Text = objOrderHead.Price_Code
                    End If
                    If (clsCommon.myLen(txtPriceGroupCode.Text)) <= 0 Then
                        txtPriceGroupCode.Text = objOrderHead.Price_Group_Code
                    End If
                    If (clsCommon.myLen(ddlPaymentTerms.SelectedValue)) <= 0 Then
                        ddlPaymentTerms.SelectedValue = objOrderHead.Payment_Terms
                    End If
                    If (clsCommon.myLen(ddlDispatchTerms.SelectedValue)) <= 0 Then
                        ddlDispatchTerms.SelectedValue = objOrderHead.Dispatch_Terms
                    End If
                    If txtVehicleCapacity.Value = 0 Then
                        txtVehicleCapacity.Value = objOrderHead.Vehicle_Capacity
                    End If
                    If txtDispatchPeriod.Value = 0 Then
                        txtDispatchPeriod.Value = objOrderHead.Dispatch_Period
                        txtDispatchDate.Value = objOrderHead.Dispatch_date
                    End If
                    If (clsCommon.myLen(txtVehcileCode.Text)) <= 0 Then
                        txtVehcileCode.Text = objOrderHead.Vehicle_Code
                        txtVehicleNo.Text = objOrderHead.VehicleNo
                    End If
                    If clsCommon.myLen(txtDiscAmt.Text) <= 0 OrElse clsCommon.myLen(txtDiscPer.Text) <= 0 OrElse clsCommon.myCdbl(txtDiscAmt.Text) = 0 OrElse clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                        If clsCommon.myLen(txtDiscAmt.Text) <= 0 OrElse clsCommon.myLen(txtDiscPer.Text) <= 0 OrElse clsCommon.myCdbl(txtDiscAmt.Text) = 0 OrElse clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                            txtDiscPer.Text = objOrderHead.HeadDisc_Per
                            If clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                                txtDiscAmt.Text = objOrderHead.HeadDisc_Amt
                                chkDiscountOnAmt.IsChecked = True
                                lblInvoiceDiscAmt.Text = objOrderHead.HeadDisc_Amt
                            Else
                                chkDiscountOnRate.IsChecked = True
                                lblInvoiceDiscAmt.Text = objOrderHead.HeadDisc_PerAmt
                            End If

                        End If

                    End If
                    LoadBlankGridAC()
                    InvoiceType()
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code1) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt1
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code2) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt2
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code3) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt3
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code4) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt4
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code5) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt5
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code6) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt6
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code7) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt7
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code8) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt8
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code9) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt9
                    End If
                    If (clsCommon.myLen(objOrderHead.Add_Charge_Code10) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt10
                    End If
                    gvAC.Rows.AddNew()


                End If

            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If


            Dim mrnno As String = ""
            Dim arr As New List(Of String)
            For ii As Integer = 0 To frm.ArrReturn.Count - 1
                If clsCommon.myLen(frm.ArrReturn(ii).Document_Code) > 0 Then
                    Dim strCode As String = frm.ArrReturn(ii).Document_Code
                    'If Not arr.Contains(strCode) Then
                    '    arr.Add(strCode)
                    objOrderHead = clsPSShipmentHead.GetData(frm.ArrReturn(ii).Document_Code, NavigatorType.Current)
                    For Each obj As clsPSShipmentHeadDetail In objOrderHead.Arr
                        If (obj.Item_Code = frm.ArrReturn(ii).Item_Code AndAlso obj.Scheme_Item = "N") OrElse (obj.Scheme_Code = frm.ArrReturn(ii).Scheme_Code AndAlso clsCommon.myLen(obj.Scheme_Code) > 0) Then

                            If IsValidItem(obj) Then
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = obj.RATE_UOM
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitALter).Value = obj.Alternate_UOM
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = obj.Document_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = obj.Row_Type
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                                If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                                Else
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Item_Code, Nothing)
                                End If
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = frm.ArrReturn(ii).Balance_Qty

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = frm.ArrReturn(ii).Balance_Qty
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = frm.ArrReturn(ii).Balance_Qty
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
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                                ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = obj.Assessable
                                'Dim dt As DataTable = clsPSInvoiceHead.GetOriginalQty(obj.Document_Code, obj.Item_Code, obj.Unit_code, obj.Assessable, obj.MRP, Nothing)
                                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
                                'End If
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                                If obj.MFG_Date.HasValue Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = obj.MFG_Date
                                End If
                                If obj.Expiry_Date.HasValue Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = obj.Expiry_Date
                                End If
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = IIf(obj.Scheme_Applicable = "Y", "Yes", "No")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = obj.Scheme_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = IIf(obj.Scheme_Item = "Y", "Yes", "No")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = obj.Item_Tax
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRP).Value = obj.Total_MRP_Amt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = obj.Total_Basic_Amt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscountAmount).Value = obj.Total_Disc_Amt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colcustDiscount).Value = obj.Cust_Discount
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = obj.Total_Cust_Discount
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = obj.ActualRate
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColCustDiscountQty).Value = obj.Cust_DiscountQty
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = obj.Price_Date
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = obj.Price_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = obj.Abatement_Per
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmount).Value = obj.Abatement_Amt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = obj.FOC_Item
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = obj.Item_Weight
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = obj.Conv_Factor
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = obj.TotalItem_Weight
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkupOn).Value = obj.Markup_On
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkUpPercentage).Value = obj.Markup_Percent
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLandingCost).Value = obj.Landing_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscPercentage).Value = obj.CustDiscPer
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDiscamt).Value = obj.HeadDiscAmt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCashDiscSchemeCode).Value = obj.CasdDiscScheme_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPurCost).Value = obj.Purchase_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = obj.OrgRate
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleCode).Value = obj.PrincipleCode
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleDesc).Value = obj.PrincipleDesc
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorCode).Value = obj.vendor_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colvendorDesc).Value = obj.vendor_desc
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaDDisPer).Value = obj.HeadDiscPer
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDisPerAmt).Value = obj.HeadDiscPerAmt

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCommRate).Value = obj.Commission_Rate
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value = obj.Commission_Party
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommPartyName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value & "'")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommAmt).Value = obj.Commission_Amt
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmtAfterCOmm).Value = obj.Amt_Less_Commission

                            End If
                        End If
                    Next
                    'End If
                End If
                'mrnno = obj.Document_Code
            Next


            If objOrderHead.Arr IsNot Nothing AndAlso objOrderHead.Arr.Count > 0 Then
                For Each objTr As clsPSShipmentHeadDetail In objOrderHead.Arr
                    If objTr.Row_Type = "Misc" Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = mrnno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                        End If
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

                    End If
                Next
            End If

            'gv1.Rows.AddNew()
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem


            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
    End Sub

    Function IsValidItem(ByVal obj As clsPSShipmentHeadDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.SRNTax_Group
            SetTaxDetails()
        End If
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
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.Document_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Order No : " + obj.Document_Code + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
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
                If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
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
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        End If
                    End If
                End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If clsPSInvoiceHeadDetail.CompleteSRN(txtDocNo.Value, strICode, intSNo) Then
                            common.clsCommon.MyMessageBoxShow("Successfully Completed")
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Function GetQueryVIJAYA(Optional ByVal strDocNo As String = Nothing) As String
        Dim Qry As String = ""

        Dim Bank_Name As String = ""
        Dim IFSC_Code As String = ""
        Dim BANKACCNUMBER As String = ""
        Qry = "select isnull(DESCRIPTION,'') as Bank_Name,isnull(IFSC_Code,'') as IFSC_Code,isnull(BANKACCNUMBER,'') as BANKACCNUMBER from TSPL_BANK_MASTER" & _
               " left outer join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_BRANCH_MASTER.Bank_CODE " & _
               " where Default_Bank = 1 "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then
            Bank_Name = clsCommon.myCstr(dt.Rows(0).Item("Bank_Name"))
            IFSC_Code = clsCommon.myCstr(dt.Rows(0).Item("IFSC_Code"))
            BANKACCNUMBER = clsCommon.myCstr(dt.Rows(0).Item("BANKACCNUMBER"))
        End If
        Qry = "select " &
            "Case when dtax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate " &
            " when dtax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate when dtax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  when dtax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate when dtax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate when dtax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate when dtax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate end as TCS_Rate " &
            " ,Case when dtax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt  when dtax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt when dtax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt  when dtax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt when dtax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt when dtax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt when dtax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt end  as TCS_Amount," &
            " TBL_FOR_SHIP_TO_LOCTION_GSTSTATE.state_Name as Ship_State_Name,  Convert (decimal(18,2), (isnull (SourceUOM.Conversion_Factor,0)/nullif (TargetUOM.Conversion_Factor,0) ) * convert (decimal(18,2),isnull(TSPL_SD_sale_invoice_DETAIL.Qty,0))) as QtyInPCS ,case when coalesce(InLtr.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InLtr.Conversion_factor,1)) end as QtyInLtr, case when coalesce(InCrate.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InCrate.Conversion_factor,1)) end as QtyInCrate " &
            " ,'" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER " &
            "  ,  TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary,convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item , case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No, Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable, case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo,  case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate, COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SALE_INVOICE_HEAD.Remarks, case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then TSPL_SD_SALE_INVOICE_head.RoundOffAmount else TSPL_SD_SHIPMENT_HEAD.RoundOffAmount end  as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.shippedCan " &
            " , cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img " &
            " ,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo " &
            " , TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty, TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No,case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Description,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Description else   TSPL_SD_SHIPMENT_HEAD.Description end as Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery, TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate  ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate, TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition, TSPL_LOCATION_MASTER.Location_Desc " &
            ", TSPL_COMPANY_MASTER.Comp_Name as CompName " &
            "  ,  case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1 " &
            ", case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2 " &
            ", case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3  " &
            " , case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name" &
            " , case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn " &
        " ,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn " &
        " ,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan" &
        ",  TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost" &
        " , TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10 " &
        "   , TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,  isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount" &
        ", TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as SalePerson     " &
       " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code   left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" &
        " LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE   ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' " &
        " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State   Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code " &
     " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code" &
        " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 " &
    " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2 " &
    " left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3  " &
    " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4 " &
    " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5 " &
    " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6 " &
    " left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7 " &
    " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8 " &
    " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9 " &
    " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " &
    " left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
    " left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM  " &
    " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code  " &
    " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
    " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" &
    " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code" &
    " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" &
    " Left outer join TSPL_STATE_MASTER as TBL_FOR_SHIP_TO_LOCTION_GSTSTATE on TBL_FOR_SHIP_TO_LOCTION_GSTSTATE.STATE_CODE = TSPL_SHIP_TO_LOCATION.State " &
    "  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.LTR_TYPE='Y') as InLtr on InLtr.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL 	  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.Crate_type='Y') as InCrate on InCrate.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left outer join tspl_item_uom_detail as SourceUOM  on SourceUOM.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code  AND SourceUOM.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.unit_code  left Outer Join tspl_item_uom_detail as TargetUOM on TargetUOM.Item_Code = TSPL_SD_sale_invoice_DETAIL.Item_Code and TargetUOM.UOM_Code = 'PCS' "

        Return Qry
    End Function

    Public Function GetQuery(Optional ByVal strDocNo As String = Nothing) As String
        '' Dim Supp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_No_For_Supplementary from TSPL_SD_SALE_INVOICE_head where Document_Code ='" & strDocNo & "' "))
        Dim ShowShipToPartyInDairyDispatch As Integer = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ShowShipToPartyInDairyDispatch & "'")) = 0, 0, 1)
        Dim Qry As String = ""

        Dim Bank_Name As String = ""
        Dim IFSC_Code As String = ""
        Dim BANKACCNUMBER As String = ""
        Qry = "select isnull(DESCRIPTION,'') as Bank_Name,isnull(IFSC_Code,'') as IFSC_Code,isnull(BANKACCNUMBER,'') as BANKACCNUMBER from TSPL_BANK_MASTER" & _
               " left outer join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_BRANCH_MASTER.Bank_CODE " & _
               " where Default_Bank = 1 "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then
            Bank_Name = clsCommon.myCstr(dt.Rows(0).Item("Bank_Name"))
            IFSC_Code = clsCommon.myCstr(dt.Rows(0).Item("IFSC_Code"))
            BANKACCNUMBER = clsCommon.myCstr(dt.Rows(0).Item("BANKACCNUMBER"))
        End If
        Dim DateOfEInvoiceImplementation As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, Nothing))
        '=======update by preeti Gupta against ticket no[ERO/31/05/18-000332]
        'Ticket No-MIL/24/04/19-000068,  Client -Milan, Sanjay
        ''RICHA WRITE SEPARATE CODE FOR MILAN DAIRY gmd MIL/28/06/19-000102
        'Ticket No-MIL/02/08/19-000117 Sanjay,Add Bank Detail
        If ShowShipToPartyInDairyDispatch = 1 OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
            '            Qry = " select cast(TSPL_SD_SALE_INVOICE_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_SD_SALE_INVOICE_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Ack_Date,103) else ''  end as Ack_Date, case when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=1 and isnull(TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type,'')='BB' AND convert(date ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
            '                  "  Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt end  as TCS_Amount, TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress,  TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_vehicle_master.vehicle_id,tspl_vehicle_master.Description as Vehicle_Name,tspl_company_master.Access_Officer,tspl_ship_to_location.ship_to_code,case when ISNULL(TSPL_SHIP_TO_LOCATION.Telphone,'')='(+__)__________' then '' else TSPL_SHIP_TO_LOCATION.Telphone end   as Ship_to_Phn ,SHip_to_State.state_code  as Ship_To_State_Code," &
            '" SHip_to_State.state_Name as Ship_To_State_Name,SHip_to_State.gst_state_code as  Ship_To_GSt_Sate_Code,TSPL_SHIP_TO_LOCATION.pin_code as Ship_PIN_Code,tspl_ship_to_location.gstNo as Ship_to_GSTNO," &
            '" TSPL_SHIP_TO_LOCATION.PAN as Ship_To_PAN, TSPL_CUSTOMER_MASTER.pin_no,TSPL_CUSTOMER_MASTER_Ship_To_Location.pin_no as Ship_PIN_No,TSPL_SD_SALE_INVOICE_head.salesMan_Name,tspl_payment_code.payment_desc,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO ,TSPL_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode," &
            '                 " case when ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2 Else'' End   as Ship_to_Party_Phn ,isnull(TSPL_CUSTOMER_MASTER_Ship_To_Location.FSSAI_NO,'') as Ship_To_Cust_FSSAI_LIC_NO ,TSPL_CITY_MASTER.city_name as Cust_City,TSPL_TERMS_MASTER.terms_desc, TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,      case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc, '" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER , TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
            '                  "convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Debit Note' WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='C' then 'Credit Note' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ," &
            '                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No," &
            '                  " Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable," &
            '                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo, " &
            '                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate," &
            '                  " COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SALE_INVOICE_HEAD.Remarks, TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," &
            '          " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," &
            '          "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
            '           "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
            '           "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
            '           "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
            '           " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
            '          "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" &
            '          "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
            '           "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
            '           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
            '  " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," &
            '           " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,"
            '            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, " &
            '"TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " &
            '          " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," &
            '           " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," &
            '           " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " &
            '           " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," &
            '           " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," &
            '          "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," &
            '           " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " &
            '           "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," &
            '           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CURRENCY_MASTER.CURRENCY_SIGN  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_Cust_CurrencySign end as Currency_Sign, " &
            '           "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name," &
            '           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
            '           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
            '  " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan ,case when coalesce(p_cust.Contact_Person_Phone,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone      when coalesce(p_cust.Contact_Person_Phone,'')<>'' then p_cust.Contact_Person_Phone   end as P_Contact_Person_Phone,case when coalesce(p_cust.Contact_Person_Name,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name      when coalesce(p_cust.Contact_Person_Name,'')<>'' then p_cust.Contact_Person_Name   end as P_Contact_Person_Name,case when coalesce(p_cust.Contact_Person_Email,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Email      when coalesce(p_cust.Contact_Person_Email,'')<>'' then p_cust.Contact_Person_Email   end as P_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone, " &
            '   " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty" &
            '           " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" &
            '           " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," &
            '           "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt, " &
            '          "   tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt, " &
            '          " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt," &
            '          " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt, " &
            '          " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt, " &
            '          " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  " &
            '          " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, " &
            '          " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  " &
            '          " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, " &
            '          " TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ," &
            '         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt, " &
            '         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, " &
            '          " isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount " &
            '            " ,TCM.Customer_Name Party,TCM.Add1 as Partycust_add1 ,TCM. Add2 as Partycust_add2 ,TCM.Add3 Partycust_add3,PARTY_STATE_MASTER.GST_STATE_Code AS Party_Gst_StateCode, TCM.gstno as PartyGSTNo,TCM.pan as Party_Pan " &
            '            "  ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount   from TSPL_SD_SALE_INVOICE_HEAD" &
            '           " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
            '          " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" &
            '          " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " &
            '           "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " &
            '           " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select isnull(contact_person_phone,'') as contact_person_phone ,isnull(Contact_Person_Name,'') AS Contact_Person_Name,isnull(Contact_Person_Website,'') as Contact_Person_Website,isnull(Contact_Person_Email,'') as Contact_Person_Email,Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No,TSPL_CURRENCY_MASTER.CURRENCY_SIGN as P_Cust_CurrencySign  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
            '         " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
            '           " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'" &
            '           " left outer join TSPL_CUSTOMER_MASTER TCM on tcm.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Party left outer join TSPL_STATE_MASTER as PARTY_STATE_MASTER on TCM.State= PARTY_STATE_MASTER.STATE_CODE " &
            '           "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " &
            '           " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
            '           " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code" &
            '           " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
            '          " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" &
            '          " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " &
            '          "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " &
            '           " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " &
            '           " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " &
            '           " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " &
            '          "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " &
            '         "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " &
            '           " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " &
            '         "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " &
            '          " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " &
            '           "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
            '           "left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " &
            '          "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
            '          " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
            '           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
            '           "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " &
            '          " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " &
            '          " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" &
            '           " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" &
            '           " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " &
            '            " left outer join TSPL_CURRENCY_MASTER  on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
            '             " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" &
            '            "  left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State   " &
            '            "left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.terms_code=tspl_customer_master.terms_code" &
            '            " left join tspl_payment_code on tspl_payment_code.payment_code=tspl_customer_master.payment_code " &
            '            " left join TSpl_state_master SHip_to_State on  SHip_to_State.state_code=tspl_ship_to_location.state" &
            '           " left join tspl_route_master on tspl_route_master.route_no= tspl_customer_master.route_no " &
            '        " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_SD_SHIPMENT_HEAD.vehicle_code" &
            '        " left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id"
            ''          "left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no=TSPL_SD_SHIPMENT_HEAD.against_delivery_code " & _
            ''" left join tspl_route_master on tspl_route_master.route_no= TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.route_no"

            'Batch wise
            Qry = " select  isnull (TSPL_ITEM_PRICE_PLAN_DETAIL .Item_Basic_Price,0) as Item_Selling_Price,  COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '' when Scheme_Item='N' then '' end   as itemdesc2, TSPL_ITEM_MASTER.Structure_Code ,case when TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Crate' and len (Scheme_Item_Code) <= 0 then TSPL_SD_SALE_INVOICE_DETAIL.Qty   when TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Box' and len (Scheme_Item_Code) <= 0 then TSPL_SD_SALE_INVOICE_DETAIL.Qty  when TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Can' and len (Scheme_Item_Code) <= 0 then TSPL_SD_SALE_INVOICE_DETAIL.Qty else 0 end as QtyCrateBoxCan_F
,case when (TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Pouch' or TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Pcs') and Scheme_Item='N' then isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) else (isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,'' ))>0 and Scheme_Item='N'  then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty * TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target_Pouch.Conversion_Factor,0)) else 0 end),0) ) end as QtyInPSC_F,
                    case when (TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Pouch' or  TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = 'Pcs') and Scheme_Item='Y' then isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0) else 0 end as SchemeQtyInPSC_F ,                       
                    case when  Scheme_Item='Y' then (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.amount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.amount end) else 0 end  as amountScheme,

                    case when  Scheme_Item='N' then (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.amount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.amount end) else 0 end  as amountWithoutScheme,

case when isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target_LTR.Conversion_Factor,0)) else 0 end),0) >=  TBL_TurnoverDis.From_Range and isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0  then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target_LTR.Conversion_Factor,0)) else 0 end),0)  <= TBL_TurnoverDis.To_Range then TBL_TurnoverDis.Incentive else 0 end as ItemTurnoverDis_Old, ( isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target_LTR.Conversion_Factor,0)) else 0 end),0))  *  TBL_TurnoverDis.INCENTIVE as ItemTurnoverDis , isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty *TSPL_ITEM_UOM_DETAIL_Scheme.Conversion_Factor)/Uom_Detail_Scheme.Conversion_Factor) else 0 end),0)  as Scheme_Qty_PSC, isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Scheme.Conversion_Factor)/Uom_Detail_Scheme_LTR.Conversion_Factor) else 0 end),0)  as Scheme_Qty_LTR,isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target.Conversion_Factor,0)) else 0 end),0)  as QtyInPSC,isnull((case when len(isnull(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL_Source.Conversion_Factor)/ nullif(Uom_Detail_Target_LTR.Conversion_Factor,0)) else 0 end),0)  as QtyInLTR,isnull (TSPL_SD_SALE_INVOICE_DETAIL.HeadDiscAmt,0) as InvoiceItemDiscount, cast(TSPL_SD_SALE_INVOICE_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_SD_SALE_INVOICE_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Ack_Date,103) else ''  end as Ack_Date, case when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=1 and isnull(TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type,'')='BB' AND convert(date ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
      "  Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt end  as TCS_Amount, TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress,  TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_vehicle_master.vehicle_id,tspl_vehicle_master.Description as Vehicle_Name,tspl_company_master.Access_Officer,tspl_ship_to_location.ship_to_code,case when ISNULL(TSPL_SHIP_TO_LOCATION.Telphone,'')='(+__)__________' then '' else TSPL_SHIP_TO_LOCATION.Telphone end   as Ship_to_Phn ,SHip_to_State.state_code  as Ship_To_State_Code," &
" SHip_to_State.state_Name as Ship_To_State_Name,SHip_to_State.gst_state_code as  Ship_To_GSt_Sate_Code,TSPL_SHIP_TO_LOCATION.pin_code as Ship_PIN_Code,tspl_ship_to_location.gstNo as Ship_to_GSTNO," &
" TSPL_SHIP_TO_LOCATION.PAN as Ship_To_PAN, TSPL_CUSTOMER_MASTER.pin_no,TSPL_CUSTOMER_MASTER_Ship_To_Location.pin_no as Ship_PIN_No,TSPL_SD_SALE_INVOICE_head.salesMan_Name,tspl_payment_code.payment_desc,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO ,TSPL_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode," &
     " case when ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2 Else'' End   as Ship_to_Party_Phn ,isnull(TSPL_CUSTOMER_MASTER_Ship_To_Location.FSSAI_NO,'') as Ship_To_Cust_FSSAI_LIC_NO ,TSPL_CITY_MASTER.city_name as Cust_City,TSPL_TERMS_MASTER.terms_desc, TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,      case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc, '" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER , TSPL_SD_SALE_INVOICE_DETAIL.Line_No,(case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
     " else TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt end) as Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
      "convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Debit Note' WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='C' then 'Credit Note' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,(case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt end) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ," &
      " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No," &
      " Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable," &
      " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo, " &
      " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate," &
      " COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SALE_INVOICE_HEAD.Remarks, TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," &
" TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," &
"   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
"  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
"  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
"  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
" + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
"  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" &
"+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
"+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
"  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
" as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," &
" TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, " &
"TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " &
          " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," &
           " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," &
           " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " &
           " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," &
           " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," &
          "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," &
           " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " &
           "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CURRENCY_MASTER.CURRENCY_SIGN  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_Cust_CurrencySign end as Currency_Sign, " &
           "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
  " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan ,case when coalesce(p_cust.Contact_Person_Phone,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone      when coalesce(p_cust.Contact_Person_Phone,'')<>'' then p_cust.Contact_Person_Phone   end as P_Contact_Person_Phone,case when coalesce(p_cust.Contact_Person_Name,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name      when coalesce(p_cust.Contact_Person_Name,'')<>'' then p_cust.Contact_Person_Name   end as P_Contact_Person_Name,case when coalesce(p_cust.Contact_Person_Email,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Email      when coalesce(p_cust.Contact_Person_Email,'')<>'' then p_cust.Contact_Person_Email   end as P_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone, " &
   " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,COALESCE(BI.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Qty) as qty" &
           " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.amount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.amount end)  as amount,(case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.amt_less_discount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.amt_less_discount end) as Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" &
           " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," &
           "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt, " &
          "   tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt, " &
          " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt," &
          " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt, " &
          " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt, " &
          " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  " &
          " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, " &
          " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  " &
          " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, " &
          " TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ," &
         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10" &
         " ,(case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt end) as TAX1_Amt " &
         ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt end) as TAX2_Amt " &
         " , (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
         " else TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt end) as TAX3_Amt " &
         ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt end) as TAX4_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt end) as TAX5_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt end) as TAX6_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        "  else TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt end) as TAX7_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt end) as TAX8_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt end) as TAX9_Amt " &
        ", (case when BI.Qty is not null then convert(Decimal(18,2),(TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*BI.Qty) " &
        " else TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt end) as TAX10_Amt" &
         " ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, " &
          " isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount " &
            " ,TCM.Customer_Name Party,TCM.Add1 as Partycust_add1 ,TCM. Add2 as Partycust_add2 ,TCM.Add3 Partycust_add3,PARTY_STATE_MASTER.GST_STATE_Code AS Party_Gst_StateCode, TCM.gstno as PartyGSTNo,TCM.pan as Party_Pan " &
            "  ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount,upper(SUBSTRING(BI.Batch_No,1,7)) as Batch_No from TSPL_SD_SALE_INVOICE_HEAD" &
           " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
          " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" &
          "  LEFT outer JOIN TSPL_BATCH_ITEM as BI ON BI.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.shipment_code AND BI.Parent_Line_No=TSPL_SD_SALE_INVOICE_DETAIL.Line_No AND BI.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
          " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " &
           "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " &
           " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select isnull(contact_person_phone,'') as contact_person_phone ,isnull(Contact_Person_Name,'') AS Contact_Person_Name,isnull(Contact_Person_Website,'') as Contact_Person_Website,isnull(Contact_Person_Email,'') as Contact_Person_Email,Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No,TSPL_CURRENCY_MASTER.CURRENCY_SIGN as P_Cust_CurrencySign  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
         " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
           " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'" &
           " left outer join TSPL_CUSTOMER_MASTER TCM on tcm.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Party left outer join TSPL_STATE_MASTER as PARTY_STATE_MASTER on TCM.State= PARTY_STATE_MASTER.STATE_CODE " &
           "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " &
           " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
           " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code" &
           " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
          " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" &
          " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " &
          "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " &
           " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " &
           " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " &
           " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " &
          "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " &
         "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " &
           " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " &
         "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " &
          " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " &
           "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
           "left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " &
           " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Scheme  on TSPL_ITEM_UOM_DETAIL_Scheme.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL_Scheme.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code 

             left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Scheme  on Uom_Detail_Scheme.Item_Code =TSPL_ITEM_MASTER.Item_Code   
             and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' and ( Uom_Detail_Scheme.UOM_Code = 'PSC'  )

             left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Scheme_Pouch  on Uom_Detail_Scheme.Item_Code =TSPL_ITEM_MASTER.Item_Code   
             and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' and  Uom_Detail_Scheme_Pouch.UOM_Code = 'Pouch' 
			  
            left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Scheme_LTR  on Uom_Detail_Scheme_LTR.Item_Code =TSPL_ITEM_MASTER.Item_Code   
            and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' and Uom_Detail_Scheme_LTR.UOM_Code = 'LTR'  
			 
            left outer join TSPL_ITEM_UOM_DETAIL  as TSPL_ITEM_UOM_DETAIL_Source on TSPL_ITEM_UOM_DETAIL_Source.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL_Source.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code    

            left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Target  on Uom_Detail_Target.Item_Code =TSPL_ITEM_MASTER.Item_Code   
            and  Uom_Detail_Target.UOM_Code = 'PSC'  

            left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Target_Pouch  on Uom_Detail_Target_Pouch.Item_Code =TSPL_ITEM_MASTER.Item_Code   
            and  Uom_Detail_Target_Pouch.UOM_Code = 'Pouch' 

            left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail_Target_LTR  on Uom_Detail_Target_LTR.Item_Code =TSPL_ITEM_MASTER.Item_Code   
            and Uom_Detail_Target_LTR.UOM_Code = 'LTR'   and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' 
           
           left outer join (select Document_Code as DocumentCode , sum(Qty) as TotalQty from TSPL_SD_SALE_INVOICE_DETAIL where Scheme_Item='N' group by Document_Code)	as 	TBL_TotalInvoiceQty	on   TBL_TotalInvoiceQty.DocumentCode = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
           
            " &
          "     left outer join (  select TOP 1  TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE, TSPL_SALES_INCENTIVE_SLAB.FROM_RANGE  as FROM_RANGE,TSPL_SALES_INCENTIVE_SLAB.TO_RANGE  as TO_RANGE ,TSPL_SALES_INCENTIVE_SLAB.INCENTIVE, TSPL_SALES_INCENTIVE_HEADER.FROM_DATE, TSPL_SALES_INCENTIVE_HEADER.TO_DATE, TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code from TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING 
                left outer join TSPL_SALES_INCENTIVE_HEADER on TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE
                left outer join TSPL_SALES_INCENTIVE_SLAB on TSPL_SALES_INCENTIVE_SLAB.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE
                left outer join TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING on TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.INCENTIVE_CODE = TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE
                where  TSPL_SALES_INCENTIVE_HEADER.Status = 1 and TSPL_SALES_INCENTIVE_HEADER.In_Active = 0 and INCENTIVE_UOM = 'LTR'   ) as TBL_TurnoverDis on TBL_TurnoverDis.CUSTOMER_CODE = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code and convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) between convert(date, TBL_TurnoverDis.FROM_DATE,103) and convert (date,TBL_TurnoverDis.TO_DATE,103) and TBL_TurnoverDis.Structure_Code = TSPL_ITEM_MASTER.Structure_Code  and     TBL_TotalInvoiceQty.TotalQty  between TBL_TurnoverDis.FROM_RANGE and TBL_TurnoverDis.TO_RANGE  " &
                " left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code = TSPL_SD_SALE_INVOICE_DETAIL.Price_code and TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_PRICE_MASTER.Item_MRP = TSPL_SD_SALE_INVOICE_DETAIL.MRP and  convert (varchar,TSPL_ITEM_PRICE_MASTER.Start_Date,103) = convert (varchar,TSPL_SD_SALE_INVOICE_DETAIL.Price_Date ,103) and TSPL_ITEM_PRICE_MASTER.UOM = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                  left outer join TSPL_ITEM_PRICE_PLAN_DETAIL on TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_TR_Code = TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code " &
          "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
          " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
           "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " &
          " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " &
          " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" &
           " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" &
           " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " &
            " left outer join TSPL_CURRENCY_MASTER  on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
             " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" &
            "  left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State   " &
            "left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.terms_code=tspl_customer_master.terms_code" &
            " left join tspl_payment_code on tspl_payment_code.payment_code=tspl_customer_master.payment_code " &
            " left join TSpl_state_master SHip_to_State on  SHip_to_State.state_code=tspl_ship_to_location.state" &
           " left join tspl_route_master on tspl_route_master.route_no= tspl_customer_master.route_no " &
        " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_SD_SHIPMENT_HEAD.vehicle_code" &
        " left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id"

        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal Then
            Qry = " select TSPL_SD_SHIPMENT_HEAD.Form_38_No AS Vehicle,TSPL_SD_SHIPMENT_HEAD.SHIP_TO_DELIVERY_AT
                    ,TSPL_SD_SHIPMENT_HEAD.Beejak_No,TSPL_SD_SHIPMENT_HEAD.Comments,(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor) as TotalItem_Weight,
                    TSPL_SD_SHIPMENT_HEAD.LR_GR_NO as GP_No ,convert(varchar,TSPL_SD_SHIPMENT_HEAD.LR_GR_Date,103) as GP_Date
                 ,TSPL_LOCATION_MASTER.Bank as Loc_Bank,TSPL_LOCATION_MASTER.Branch as Loc_Branch,TSPL_LOCATION_MASTER.ACType as Loc_ACType
                 ,TSPL_LOCATION_MASTER.bankaccno as Loc_bankaccno,TSPL_LOCATION_MASTER.bankifsccode AS Loc_bankifsccode,TSPL_LOCATION_MASTER.accountholdername as Loc_accountholdername
                 ,((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) /BagUOM.Conversion_Factor) AS BagCount
                 ,BagUOM.Conversion_Factor AS BagWt,
                    TSPL_SD_SALE_INVOICE_HEAD.Dept_Desc+' Department' as Department,TSPL_LOCATION_MASTER.Service_Tax_Reg_No,cast(TSPL_SD_SALE_INVOICE_HEAD.BarCode_Img as image) As BarCode_Img,isnull (TSPL_SD_SALE_INVOICE_HEAD.IRN_No,'') as IRN_No,isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'') as Ack_No,case when len(isnull (TSPL_SD_SALE_INVOICE_HEAD.Ack_No,'')) > 0 then convert (varchar, TSPL_SD_SALE_INVOICE_HEAD.Ack_Date,103) else ''  end as Ack_Date, case when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=1 and isnull(TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type,'')='BB' AND convert(date ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
                  "  Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt end  as TCS_Amount, TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress,  TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_vehicle_master.vehicle_id,tspl_vehicle_master.Description as Vehicle_Name,tspl_company_master.Access_Officer,tspl_ship_to_location.ship_to_code,case when ISNULL(TSPL_SHIP_TO_LOCATION.Telphone,'')='(+__)__________' then '' else TSPL_SHIP_TO_LOCATION.Telphone end   as Ship_to_Phn ,SHip_to_State.state_code  as Ship_To_State_Code," &
                " SHip_to_State.state_Name as Ship_To_State_Name,SHip_to_State.gst_state_code as  Ship_To_GSt_Sate_Code,TSPL_SHIP_TO_LOCATION.pin_code as Ship_PIN_Code,tspl_ship_to_location.gstNo as Ship_to_GSTNO," &
                " TSPL_SHIP_TO_LOCATION.PAN as Ship_To_PAN, TSPL_CUSTOMER_MASTER.pin_no,TSPL_CUSTOMER_MASTER_Ship_To_Location.pin_no as Ship_PIN_No,TSPL_SD_SALE_INVOICE_head.salesMan_Name,tspl_payment_code.payment_desc,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO ,TSPL_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode," &
                 " case when ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER_Ship_To_Location.Phone2 Else'' End   as Ship_to_Party_Phn ,isnull(TSPL_CUSTOMER_MASTER_Ship_To_Location.FSSAI_NO,'') as Ship_To_Cust_FSSAI_LIC_NO ,TSPL_CITY_MASTER.city_name as Cust_City,TSPL_TERMS_MASTER.terms_desc, TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,      case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc, '" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER , TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
                  "convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Debit Note' WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='C' then 'Credit Note' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ," &
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No," &
                  " Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable," &
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo, " &
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate," &
                  " COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SALE_INVOICE_HEAD.Remarks, TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," &
          " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," &
          "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
           "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
           "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
           "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
           " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" &
          "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
           "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
  " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," &
           " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, " &
"TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " &
          " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," &
           " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," &
           " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " &
           " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," &
           " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," &
          "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," &
           " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " &
           "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CURRENCY_MASTER.CURRENCY_SIGN  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_Cust_CurrencySign end as Currency_Sign, " &
           "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
  " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan ,case when coalesce(p_cust.Contact_Person_Phone,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone      when coalesce(p_cust.Contact_Person_Phone,'')<>'' then p_cust.Contact_Person_Phone   end as P_Contact_Person_Phone,case when coalesce(p_cust.Contact_Person_Name,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name      when coalesce(p_cust.Contact_Person_Name,'')<>'' then p_cust.Contact_Person_Name   end as P_Contact_Person_Name,case when coalesce(p_cust.Contact_Person_Email,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Email      when coalesce(p_cust.Contact_Person_Email,'')<>'' then p_cust.Contact_Person_Email   end as P_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone, " &
   " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty" &
           " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" &
           " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," &
           "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt, " &
          "   tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt, " &
          " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt," &
          " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt, " &
          " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt, " &
          " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  " &
          " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, " &
          " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  " &
          " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, " &
          " TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ," &
         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt, " &
         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, " &
          " isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount " &
            " ,TCM.Customer_Name Party,TCM.Add1 as Partycust_add1 ,TCM. Add2 as Partycust_add2 ,TCM.Add3 Partycust_add3,PARTY_STATE_MASTER.GST_STATE_Code AS Party_Gst_StateCode, TCM.gstno as PartyGSTNo,TCM.pan as Party_Pan " &
            "  ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount   from TSPL_SD_SALE_INVOICE_HEAD" &
           " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
          " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" &
          " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " &
           "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " &
           " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select isnull(contact_person_phone,'') as contact_person_phone ,isnull(Contact_Person_Name,'') AS Contact_Person_Name,isnull(Contact_Person_Website,'') as Contact_Person_Website,isnull(Contact_Person_Email,'') as Contact_Person_Email,Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No,TSPL_CURRENCY_MASTER.CURRENCY_SIGN as P_Cust_CurrencySign  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
         " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
           " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'" &
           " left outer join TSPL_CUSTOMER_MASTER TCM on tcm.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Party left outer join TSPL_STATE_MASTER as PARTY_STATE_MASTER on TCM.State= PARTY_STATE_MASTER.STATE_CODE " &
           "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " &
           " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
           " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code" &
           " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
          " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" &
          " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " &
          "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " &
           " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " &
           " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " &
           " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " &
          "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " &
         "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " &
           " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " &
         "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " &
          " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " &
           "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
           "left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " &
          "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
          " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
           "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " &
          " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " &
          " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" &
           " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" &
           " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " &
            " left outer join TSPL_CURRENCY_MASTER  on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " &
             " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" &
            "  left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State   " &
            "left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.terms_code=tspl_customer_master.terms_code" &
            " left join tspl_payment_code on tspl_payment_code.payment_code=tspl_customer_master.payment_code " &
            " left join TSpl_state_master SHip_to_State on  SHip_to_State.state_code=tspl_ship_to_location.state" &
           " left join tspl_route_master on tspl_route_master.route_no= tspl_customer_master.route_no " &
        " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_SD_SHIPMENT_HEAD.vehicle_code" &
        " left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id
          left outer join TSPL_ITEM_UOM_DETAIL as BagUOM ON BagUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and BagUOM.UOM_Code='Bag'
          left outer join TSPL_ITEM_UOM_DETAIL as KgUOM ON KgUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and BagUOM.UOM_Code='Kg'"

        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
            Qry = "select   TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress," &
            " isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,'1' as CopyType ,TSPL_SD_SALE_INVOICE_DETAIL.Line_No, " & Environment.NewLine &
            " TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary,convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item , case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No, " & Environment.NewLine &
            " Location_State.gst_state_code as Loc_GST_StateCode, Tspl_customer_master.gstno as CustGSTNo,tspl_location_master.gstno as LocGstNo, TSPL_STATE_MASTER.gst_state_code, " & Environment.NewLine &
            " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo,  case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate, COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE,TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.Email as Loc_Email, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,  " & Environment.NewLine &
            " TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate  ,TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition, TSPL_COMPANY_MASTER.Comp_Name as CompName, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1, " & Environment.NewLine &
            " TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty ,TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom, " & Environment.NewLine &
            " TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost,  isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  " & Environment.NewLine &
            " from TSPL_SD_SALE_INVOICE_HEAD " & Environment.NewLine &
            " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  " & Environment.NewLine &
            " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  " & Environment.NewLine &
            " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code    " & Environment.NewLine &
            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code   " & Environment.NewLine &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     " & Environment.NewLine &
            " LEFT join (select isnull(contact_person_phone,'') as contact_person_phone ,isnull(Contact_Person_Name,'') AS Contact_Person_Name,isnull(Contact_Person_Website,'') as Contact_Person_Website,isnull(Contact_Person_Email,'') as Contact_Person_Email,Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No,TSPL_CURRENCY_MASTER.CURRENCY_SIGN as P_Cust_CurrencySign  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE  ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'  " & Environment.NewLine &
            " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " & Environment.NewLine &
            " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & Environment.NewLine &
            " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code " & Environment.NewLine &
            " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 " & Environment.NewLine &
            " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2 " & Environment.NewLine &
            " left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9 " & Environment.NewLine &
            " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " & Environment.NewLine &
            " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & Environment.NewLine &
            " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state  " & Environment.NewLine &
            " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " & Environment.NewLine

        Else
            '" left outer join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.delivery_no=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code " & _
            '" left outer join (select delivery_no,performance_invoice_no from TSPL_BOOKING_DETAIL group by delivery_no,performance_invoice_no)BOOKING on BOOKING.delivery_no=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code " & _
            '",BOOKING.performance_invoice_no" & _
            '===update by preeti gupta Against ticket no[SWA/14/12/18-000062,BHA/10/04/01-000856,BHA/10/04/01-000855,BHA/10/04/01-000854,BHA/10/04/01-000856,BHA/10/04/01-000851]

            Qry = " select Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate end as TCS_Rate,Case when tax1.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX1_Amt when  tax2.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt when tax3.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX3_Amt when tax4.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX4_Amt  when tax5.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX5_Amt when tax6.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX6_Amt  when tax7.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX7_Amt when tax8.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX8_Amt when tax9.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX9_Amt when tax10.Is_TCS = 'Y' then TSPL_SD_SALE_INVOICE_HEAD.TAX10_Amt end  as TCS_Amount, TBL_FOR_SHIP_TO_LOCTION_GSTSTATE.state_Name as Ship_State_Name, case when coalesce(InLtr.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InLtr.Conversion_factor,1)) end as QtyInLtr, case when coalesce(InCrate.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InCrate.Conversion_factor,1)) end as QtyInCrate,tspl_sd_sale_return_head.is_cancelled,isnull (TSPL_SHIP_TO_LOCATION.GSTNO,'') as Ship_TO_Location_GSTIN, isnull(TSPL_SHIP_TO_LOCATION.PAN,'')  as Ship_To_Location_PAN,TBL_FOR_SHIP_TO_LOCTION_GSTSTATE.GST_STATE_Code as GST_STATE_CODE_FOR_SHIP_TO_LOCATION , TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,  case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc,'" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER ,  TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
                    "convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ," &
                    " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No," &
                    " Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable," &
                    " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo, " &
                    " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate," &
                    " COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SALE_INVOICE_HEAD.Remarks, case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then TSPL_SD_SALE_INVOICE_head.RoundOffAmount else TSPL_SD_SHIPMENT_HEAD.RoundOffAmount end  as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.shippedCan,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.Mode_of_Trans,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," &
            " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," &
            "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
             "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
             "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
             "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
             " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
            "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" &
            "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
             "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
             "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
    " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," &
             " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No,case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Description,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Description else   TSPL_SD_SHIPMENT_HEAD.Description end as Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, " &
"TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " &
            " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," &
             " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," &
             " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " &
             " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," &
             " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," &
            "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," &
             " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " &
             "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," &
             "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name," &
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
             " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
    " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone      when coalesce(p_cust.Contact_Person_Phone,'')<>'' then p_cust.Contact_Person_Phone   end as P_Contact_Person_Phone,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name      when coalesce(p_cust.Contact_Person_Name,'')<>'' then p_cust.Contact_Person_Name   end as P_Contact_Person_Name,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Email      when coalesce(p_cust.Contact_Person_Email,'')<>'' then p_cust.Contact_Person_Email   end as P_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone, " &
     " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty" &
             " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" &
             " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," &
             "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt, " &
            "   tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt, " &
            " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt," &
            " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt, " &
            " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt, " &
            " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  " &
            " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, " &
            " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  " &
            " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, " &
            " TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ," &
           " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt, " &
           " TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, " &
            " isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount " &
            ",(select top 1 performance_invoice_no from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.delivery_no=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code) AS performance_invoice_no" &
            ", TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as SalePerson  " &
             "     from TSPL_SD_SALE_INVOICE_HEAD" &
            " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " &
            " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" &
            " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " &
             "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " &
             " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,Contact_Person_Email,Contact_Person_Name,Contact_Person_Phone from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
           " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  " &
             " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'" &
             "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " &
             " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " &
             " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code" &
             " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" &
            " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " &
            "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " &
             " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " &
             " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " &
             " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " &
            "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " &
           "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " &
             " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " &
           "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " &
            " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " &
             "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " &
             "left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " &
            "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " &
            " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
             "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " &
            " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " &
            " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" &
             " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" &
             " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " &
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location " &
            "  left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State left join tspl_sd_sale_return_head on tspl_sd_sale_return_head.against_invoice_no=tspl_sd_sale_invoice_head.document_code   Left outer join TSPL_STATE_MASTER as TBL_FOR_SHIP_TO_LOCTION_GSTSTATE on TBL_FOR_SHIP_TO_LOCTION_GSTSTATE.STATE_CODE = TSPL_SHIP_TO_LOCATION.State  " &
            " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.LTR_TYPE='Y') as InLtr on InLtr.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code " &
            " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL 	  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.Crate_type='Y') as InCrate on InCrate.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code"
        End If
        Return Qry
    End Function
    Public Function GetQueryALPHA(Optional ByVal strDocNo As String = Nothing, Optional ByVal AllowManualVehicle As Boolean = False) As String
        'Ticket No-  ALF/15/10/18-000085 Round(amount,2,1) for invoice print rounding amount
        Dim ShowShipToPartyInDairyDispatch As Integer = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ShowShipToPartyInDairyDispatch & "'")) = 0, 0, 1)
        Dim Qry As String = ""

        Dim Bank_Name As String = ""
        Dim IFSC_Code As String = ""
        Dim BANKACCNUMBER As String = ""
        Qry = "select isnull(DESCRIPTION,'') as Bank_Name,isnull(IFSC_Code,'') as IFSC_Code,isnull(BANKACCNUMBER,'') as BANKACCNUMBER from TSPL_BANK_MASTER" & _
               " left outer join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_BRANCH_MASTER.Bank_CODE " & _
               " where Default_Bank = 1 "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


        Dim strDispatchNo As String = clsDBFuncationality.getSingleValue("select Against_Shipment_No from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & strDocNo & "'")
        Dim strDeliveryNo As String = clsDBFuncationality.getSingleValue("select top 1 performance_invoice_no from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.delivery_no in (select Against_Delivery_Code from tspl_sd_shipment_head where document_code='" & strDispatchNo & "')")

        If dt.Rows.Count > 0 Then
            Bank_Name = clsCommon.myCstr(dt.Rows(0).Item("Bank_Name"))
            IFSC_Code = clsCommon.myCstr(dt.Rows(0).Item("IFSC_Code"))
            BANKACCNUMBER = clsCommon.myCstr(dt.Rows(0).Item("BANKACCNUMBER"))
        End If


        If ShowShipToPartyInDairyDispatch = 1 Then

            Qry = " select isnull(TSPL_SD_SALE_INVOICE_HEAD.CancelFlag,'') as Is_Cancelled,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,      case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc, '" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER , TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," & _
                  "convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ," & _
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No," & _
                  " Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable," & _
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo, " & _
                  " case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate," & _
                  " COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, tspl_sd_shipment_head.comments as Remarks, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ," & _
          " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date," & _
          "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" & _
           "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" & _
           "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" & _
           "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" & _
           " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" & _
          "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end" & _
          "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end " & _
           "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " & _
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " & _
  " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty," & _
           " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, " & _
"TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate " & _
          " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate," & _
           " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition," & _
           " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ " & _
           " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone," & _
           " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo," & _
          "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode," & _
           " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') " & _
           "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3," & _
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CURRENCY_MASTER.CURRENCY_SIGN  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_Cust_CurrencySign end as Currency_Sign, " & _
           "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name," & _
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," & _
           " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," & _
  " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then TSPL_CUSTOMER_MASTER  .PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan ,case when coalesce(p_cust.Contact_Person_Phone,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone      when coalesce(p_cust.Contact_Person_Phone,'')<>'' then p_cust.Contact_Person_Phone   end as P_Contact_Person_Phone,case when coalesce(p_cust.Contact_Person_Name,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name      when coalesce(p_cust.Contact_Person_Name,'')<>'' then p_cust.Contact_Person_Name   end as P_Contact_Person_Name,case when coalesce(p_cust.Contact_Person_Email,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Email      when coalesce(p_cust.Contact_Person_Email,'')<>'' then p_cust.Contact_Person_Email   end as P_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone, " & _
   " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty" & _
           " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp, TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM" & _
           " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt," & _
           "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt, " & _
          "   tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt, " & _
          " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt," & _
          " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt, " & _
          " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt, " & _
          " tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  " & _
          " tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, " & _
          " tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  " & _
          " tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt, " & _
          " TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ," & _
         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt, " & _
         " TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type, " & _
          " isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount " & _
            " ,TCM.Customer_Name Party,TCM.Add1 as Partycust_add1 ,TCM. Add2 as Partycust_add2 ,TCM.Add3 Partycust_add3,PARTY_STATE_MASTER.GST_STATE_Code AS Party_Gst_StateCode, TCM.gstno as PartyGSTNo,TCM.pan as Party_Pan " & _
            "  ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount   from TSPL_SD_SALE_INVOICE_HEAD" & _
           " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
          " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No" & _
          " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " & _
           "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code " & _
           " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No,TSPL_CURRENCY_MASTER.CURRENCY_SIGN as P_Cust_CurrencySign  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
         " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " & _
           " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'" & _
           " left outer join TSPL_CUSTOMER_MASTER TCM on tcm.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Party left outer join TSPL_STATE_MASTER as PARTY_STATE_MASTER on TCM.State= PARTY_STATE_MASTER.STATE_CODE " & _
           "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " & _
           " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
           " left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code" & _
           " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " & _
          " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1" & _
          " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  " & _
          "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  " & _
           " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  " & _
           " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  " & _
           " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  " & _
          "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 " & _
         "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 " & _
           " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 " & _
         "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  " & _
          " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " & _
           "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code " & _
           "left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM " & _
          "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
          " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " & _
           "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " & _
          " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state " & _
          " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" & _
           " left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state" & _
           " left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " & _
            " left outer join TSPL_CURRENCY_MASTER  on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_CUSTOMER_MASTER.CURRENCY_CODE " & _
             " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" & _
            "  left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State   "

        Else
            ''richa MIL/28/06/19-000102
            'Ticket No-ALF/05/08/19-000109,Sanjay
            Qry = " select TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate ,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_detail.tax2_amt,0) as txt2amt,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,isnull(TSPL_SD_SALE_INVOICE_DETAIL .Total_Tax_Amt,0) as Detail_Total_Tax_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate ,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_detail.tax1_amt,0) as txt1amt," & IIf(AllowManualVehicle = True, "TSPL_SD_SHIPMENT_HEAD.ManualVehicle as VehicleNo", "TSPL_SD_SALE_INVOICE_HEAD.VehicleNo") & ",TSPL_SD_SALE_INVOICE_DETAIL.Line_No,'" & strDeliveryNo & "' AS performance_invoice_no,TSPL_USER_MASTER.User_Name as UserName ,  isnull(TSPL_SD_SALE_INVOICE_HEAD.CancelFlag,'') as Is_Cancelled,case when TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt=0 then 0 else (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then  ((isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_SD_SALE_INVOICE_DETAIL.item_cost)/TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt)* isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) else isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) end) end as Disc_Item_Amt, case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_STATE_MASTER.gst_state_code  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_GstStateCode end as ShipTo_GstStateCode , TSPL_CITY_MASTER_For_Location .City_Name as Loc_City_Name ,case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.PAN  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Pan end as ShipTo_Pan ,  isnull(TSPL_BATCH_ITEM.Batch_No,'') as Batch_No, case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Name   when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Person    end as ShipTo_Contact_Person  , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Phone  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Per_Phone end as ShipTo_Contact_Per_Phone  ,  ShimentDetail.Item_Selling_Price as PriceRate , ShimentDetail.Conversion_Factor,TSPL_SD_SHIPMENT_HEAD.Insurance as InsuranceNo,ShimentDetail.kG_Conv_Rate,TSPL_COMPANY_MASTER.Mode_of_Trans, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as SalePerson,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone,case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.GSTNO end as ShipTo_GSTNO,isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary,convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ,Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo, TSPL_STATE_MASTER.gst_state_code,tspl_location_master.gstno as LocGstNo,case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo,  case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate,COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, " & Environment.NewLine & _
" TSPL_SD_SHIPMENT_HEAD.Comments as Remarks, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img, " & Environment.NewLine & _
" TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date  " & Environment.NewLine & _
" , TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,Case when ISNULL(TSPL_LOCATION_MASTER.Telphone,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Telphone end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone1,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone1 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate  , TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition, TSPL_COMPANY_MASTER.Comp_Name as CompName,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Add1 end as P_Add1  , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add2 end as P_Add2  , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add3 end as P_Add3 ,case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Email  end as P_Email , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Customer_Name    end as P_Cust_Name  ,  case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_state_Master.STATE_NAME  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.STATE_NAME    end as P_State_Name,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3, TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end   as itemdesc,  " & Environment.NewLine & _
" TSPL_ITEM_MASTER.Is_batch_Item, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then isnull(TSPL_BATCH_ITEM.Qty,0) else TSPL_SD_SALE_INVOICE_DETAIL.Qty end ) as qty   ,convert(decimal(18,2),case when TSPL_ITEM_MASTER.Is_batch_Item=1 then (TSPL_BATCH_ITEM.Qty*TSPL_SD_SALE_INVOICE_DETAIL.item_cost) ELSE TSPL_SD_SALE_INVOICE_DETAIL.Amount end) as amount  ,case when TSPL_ITEM_MASTER.Is_batch_Item=1 then ((TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*TSPL_BATCH_ITEM.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount end as Amt_Less_Discount, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then TSPL_BATCH_ITEM.UOM else TSPL_SD_SALE_INVOICE_DETAIL.unit_code END) as uom,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10   " & Environment.NewLine & _
" from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE   " & Environment.NewLine & _
" left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  " & Environment.NewLine & _
" left outer join (select * from (select  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.document_code,max(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as Item_Selling_Price  ,max(case when TSPL_ITEM_UOM_DETAIL.Default_UOM=1 then TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Conversion_Factor,max(case when TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' then Conversion_Factor end) kG_Conv_Rate ,TSPL_ITEM_PRICE_MASTER.start_date ,max(TSPL_ITEM_PRICE_MASTER.Price_Code ) as Price_Code from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_PRICE_MASTER on  TSPL_ITEM_PRICE_MASTER.Price_code=TSPL_SD_SHIPMENT_DETAIL.Price_code and TSPL_ITEM_PRICE_MASTER.Start_Date=TSPL_SD_SHIPMENT_DETAIL.Price_Date and tspl_sd_shipment_detail.item_code=tspl_item_price_master.item_code   and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  where TSPL_ITEM_PRICE_MASTER.UOM='KG' and Start_Date<='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and TSPL_ITEM_PRICE_MASTER.UOM='KG' and DOCUMENT_CODE='" & strDispatchNo & "' group by DOCUMENT_CODE,TSPL_ITEM_PRICE_MASTER.Item_Code,Start_Date,TSPL_SD_SHIPMENT_DETAIL.Item_Code )xx )ShimentDetail on ShimentDetail.document_code=TSPL_SD_SHIPMENT_HEAD.document_code and ShimentDetail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and ShimentDetail.start_date=  TSPL_SD_SALE_INVOICE_DETAIL.Price_Date and ShimentDetail.Price_Code=TSPL_SD_SALE_INVOICE_DETAIL.Price_code " & Environment.NewLine & _
" left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code    " & Environment.NewLine & _
" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code   " & Environment.NewLine & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & Environment.NewLine & _
" left join (select Ship_To_Code,Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1,TSPL_SHIP_TO_LOCATION.Add2,TSPL_SHIP_TO_LOCATION.Add3,TSPL_SHIP_TO_LOCATION.Add4,TSPL_SHIP_TO_LOCATION.City_Code,Ship_To_Type_Code,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_SHIP_TO_LOCATION.CST_No,TSPL_SHIP_TO_LOCATION.Email,TSPL_SHIP_TO_LOCATION.Telphone,TSPL_STATE_MASTER.STATE_NAME as STATE_NAME,TSPL_CITY_MASTER.City_Name,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SHIP_TO_LOCATION.Contact_Person_Name as ShipTo_Contact_Person, TSPL_SHIP_TO_LOCATION.Contact_Person_Phone as ShipTo_Contact_Per_Phone,TSPL_SHIP_TO_LOCATION.PAN AS ShipTo_Pan,TSPL_STATE_MASTER.GST_STATE_CODE AS ShipTo_GstStateCode,TSPL_SHIP_TO_LOCATION.GSTNO from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code left outer join TSPL_STATE_MASTER on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER.STATE_CODE) ship_cust on ship_cust.Ship_To_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & Environment.NewLine & _
" and ship_cust.Ship_To_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location  " & Environment.NewLine & _
" left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State " & Environment.NewLine & _
" Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & Environment.NewLine & _
" left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code  " & Environment.NewLine & _
" LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1 " & Environment.NewLine & _
" left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2 " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 " & Environment.NewLine & _
" left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2 " & Environment.NewLine & _
" left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3 " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4 " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5 " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6 " & Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7 " & Environment.NewLine & _
"   left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  " & Environment.NewLine & _
  " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9 " & Environment.NewLine & _
 " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 " & Environment.NewLine & _
" left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code " & Environment.NewLine & _
" left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state " & Environment.NewLine & _
" left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code " & Environment.NewLine & _
" left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_SD_SALE_INVOICE_HEAD.Created_By " & Environment.NewLine & _
" LEFT OUTER JOIN TSPL_BATCH_ITEM ON TSPL_BATCH_ITEM.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code and TSPL_BATCH_ITEM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_BATCH_ITEM.Parent_Line_No =TSPL_SD_SALE_INVOICE_DETAIL.Line_No AND TSPL_BATCH_ITEM.Document_Type='PS-SH'  " & Environment.NewLine




            '   Qry = "  select isnull(TSPL_SD_SALE_INVOICE_HEAD.CancelFlag,'') as Is_Cancelled,isnull(TSPL_SD_SALE_INVOICE_DETAIL .Total_Tax_Amt,0) as Detail_Total_Tax_Amt,TSPL_CITY_MASTER_For_Location .City_Name as Loc_City_Name , TSPL_SD_SHIPMENT_HEAD.Customer_Code as Bill_To_Party_Code "

            '   If AllowManualVehicle = True Then
            '       Qry += " ,TSPL_SD_SHIPMENT_HEAD.ManualVehicle as VehicleNo "
            '   Else
            '       Qry += " ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo "
            '   End If

            '   ''richa ALF/19/09/18-000084
            '   '=update by preeti gupta Against ticket no[ALF/17/04/19-000101]
            '   ''TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address, 
            '   Qry += " ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_SD_SHIPMENT_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,  case when  TSPL_SD_SHIPMENT_HEAD.HeadDisc_Per >0 then TSPL_SD_SHIPMENT_HEAD.HeadDisc_PerAmt else TSPL_SD_SHIPMENT_HEAD.HeadDisc_Amt end as Invoice_Disc,'' as Bank_Name,'' as IFSC_Code,'' as BANKACCNUMBER ,  TSPL_SD_SALE_INVOICE_DETAIL.Line_No, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt, isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary,convert(varchar,Supplimentry_Head.Document_Date,103) as supp_Date,case WHEN ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'')='S' then 'Supplementrary' ELSE ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type,'') END AS Supplementary_Type,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item , case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.Electronic_Ref_No  else TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No end as Electronic_Ref_No, Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_SD_SALE_INVOICE_HEAD.is_taxable, case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillNo  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo end as EWayBillNo,  case when isnull(TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary,'') <>'' then Supplimentry_Head.EWayBillDate  else TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate end as EWayBillDate, COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_SD_SALE_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_SD_SHIPMENT_HEAD.Comments as Remarks, TSPL_SD_SHIPMENT_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name = '' THEN TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual ELSE  TSPL_SD_SHIPMENT_HEAD.Transporter_Name END AS Transporter_Name,TSPL_SD_SHIPMENT_HEAD.crate,TSPL_SD_SHIPMENT_HEAD.jaali,TSPL_SD_SHIPMENT_HEAD.box, TSPL_SD_SALE_INVOICE_head.Invoice_Type AS GP_Invoice_Type, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.Mode_of_Trans,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.GRNo,case when TSPL_SD_SHIPMENT_HEAD.GRNo <> '' then convert(varchar,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) else '' end as GR_Date " & Environment.NewLine & _
            '   " ,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date , TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,case when TSPL_SD_SALE_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date,   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Telphone,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Telphone end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone1,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone1 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' when TSPL_SD_SALE_INVOICE_head.Invoice_Type='A' then 'Tax Exempted' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then ((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/Uom_Detail.Conversion_Factor) else null end  as Alternet_Qty, TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No,case when isnull(TSPL_SD_SALE_INVOICE_HEAD.Description,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Description else   TSPL_SD_SHIPMENT_HEAD.Description end as Description ,isnull(TSPL_SD_SHIPMENT_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery, TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate  ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate, TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition, TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone, TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode, TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3 " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Add1 end as P_Add1 " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add2 end as P_Add2 " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add3 end as P_Add3 " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Pin_Code  end as P_PinNo " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.CST_No   end as P_CstNo " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .STATE_NAME   end as p_State " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Email  end as P_Email " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Contact_Name , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Contact_Phone  " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Ship_To_Type_Code   end as P_CustCode  " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Customer_Name    end as P_Cust_Name " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.City_Name    end as P_City_Name  " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_state_Master.STATE_NAME  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.STATE_NAME    end as P_State_Name  " & Environment.NewLine & _
            '" , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .PAN	  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Pan_No,  " & Environment.NewLine & _
            '" case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Name   when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Person    end as ShipTo_Contact_Person  ,  " & Environment.NewLine & _
            '" case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Phone  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Per_Phone end as ShipTo_Contact_Per_Phone  ,  " & Environment.NewLine & _
            '" case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.PAN  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Pan end as ShipTo_Pan ,  " & Environment.NewLine & _
            '" case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.GSTNO end as ShipTo_GSTNO,  " & Environment.NewLine & _
            '" case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_STATE_MASTER.gst_state_code  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_GstStateCode end as ShipTo_GstStateCode ,  " & Environment.NewLine & _
            '"  TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone,  TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' then '' end   as itemdesc, " & Environment.NewLine & _
            '" TSPL_ITEM_MASTER.Is_batch_Item, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then isnull(TSPL_BATCH_ITEM.Qty,0) else TSPL_SD_SALE_INVOICE_DETAIL.Qty end ) as qty   ,TSPL_SD_SALE_INVOICE_DETAIL.mrp "
            '   '", round(case when TSPL_ITEM_MASTER.Is_batch_Item=1 then  isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_SD_SALE_INVOICE_DETAIL.item_cost ELSE TSPL_SD_SALE_INVOICE_DETAIL.Amount end,2,1) as amount " & Environment.NewLine & _
            '   'Qry += " ,case when TSPL_ITEM_MASTER.Is_batch_Item=1 then TSPL_SD_SALE_INVOICE_DETAIL.Amount+(case when cast((select right(substring(cast(isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_SD_SALE_INVOICE_DETAIL.item_cost as varchar),CHARINDEX ( '.',cast(isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_SD_SALE_INVOICE_DETAIL.item_cost as varchar))+1 ,3) ,1))as int)> 5 then .01 else 0 end) ELSE TSPL_SD_SALE_INVOICE_DETAIL.Amount end as amount"
            '   Qry += " ,convert(decimal(18,2),case when TSPL_ITEM_MASTER.Is_batch_Item=1 then (TSPL_BATCH_ITEM.Qty*TSPL_SD_SALE_INVOICE_DETAIL.item_cost) ELSE TSPL_SD_SALE_INVOICE_DETAIL.Amount end) as amount "
            '   Qry += " ,case when TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt=0 then 0 else (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then  ((isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_SD_SALE_INVOICE_DETAIL.item_cost)/TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt)* isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) else isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) end) end as Disc_Item_Amt" & _
            '       ",case when TSPL_ITEM_MASTER.Is_batch_Item=1 then ((TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount/TSPL_SD_SALE_INVOICE_DETAIL.Qty)*TSPL_BATCH_ITEM.Qty) else TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount end as Amt_Less_Discount" & _
            '       ", (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then TSPL_BATCH_ITEM.UOM else TSPL_SD_SALE_INVOICE_DETAIL.unit_code END) as uom,isnull(TSPL_BATCH_ITEM.Batch_No,'') as Batch_No, " & Environment.NewLine & _
            '" TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_detail.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_detail.tax2_amt,0) as txt2amt,    tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_SD_SALE_INVOICE_detail.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name, isnull (TSPL_SD_SALE_INVOICE_detail.tax4_amt,0) as txt4amt, tax5.Tax_Code_Desc as tax5name, isnull (TSPL_SD_SALE_INVOICE_detail.tax5_amt,0) as txt5amt,  tax6.Tax_Code_Desc as tax6name, isnull (TSPL_SD_SALE_INVOICE_detail.tax6_amt,0) as txt6amt,  tax7.Tax_Code_Desc as tax7name, isnull (TSPL_SD_SALE_INVOICE_detail.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_SD_SALE_INVOICE_detail.tax8_amt,0) as txt8amt,  tax9.Tax_Code_Desc as tax9name, isnull (TSPL_SD_SALE_INVOICE_detail.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_SD_SALE_INVOICE_Detail.tax10_amt,0) as txt10amt,  TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  , TSPL_SD_SALE_INVOICE_DETAIL.TAX1 as dTAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2 as dTAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3 as  dTAX3, TSPL_SD_SALE_INVOICE_DETAIL.TAX4 as  dTAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5 as  dTAX5, TSPL_SD_SALE_INVOICE_DETAIL.TAX6 as  dTAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7 as  dTAX7, TSPL_SD_SALE_INVOICE_DETAIL.TAX8 as dTAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9 as dTAX9, TSPL_SD_SALE_INVOICE_DETAIL.TAX10 as  dTAX10, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt,  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate as dTAX1_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate as dTAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate as dTAX3_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate as dTAX4_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate as dTAX5_Rate  ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate as dTAX6_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate as dTAX7_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate as dTAX8_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate as dTAX9_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,  isnull(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount ,isnull(TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount " & Environment.NewLine
            '   ''" ,(select top 1 performance_invoice_no from TSPL_BOOKING_DETAIL where TSPL_BOOKING_DETAIL.delivery_no=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code) AS performance_invoice_no " & Environment.NewLine & _
            '   Qry += " ,'" & strDeliveryNo & "' AS performance_invoice_no " & Environment.NewLine & _
            '       " , TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as SalePerson,TSPL_SD_SHIPMENT_HEAD.Insurance as InsuranceNo,ShimentDetail.Item_Selling_Price as PriceRate " & Environment.NewLine & _
            '" , ShimentDetail.Conversion_Factor,ShimentDetail.kG_Conv_Rate,TSPL_USER_MASTER.User_Name as UserName from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join (select * from (select  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.document_code,max(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as Item_Selling_Price  ,max(case when TSPL_ITEM_UOM_DETAIL.Default_UOM=1 then TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Conversion_Factor,max(case when TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' then Conversion_Factor end) kG_Conv_Rate ,TSPL_ITEM_PRICE_MASTER.start_date ,max(TSPL_ITEM_PRICE_MASTER.Price_Code ) as Price_Code from TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_PRICE_MASTER on  TSPL_ITEM_PRICE_MASTER.Price_code=TSPL_SD_SHIPMENT_DETAIL.Price_code and TSPL_ITEM_PRICE_MASTER.Start_Date=TSPL_SD_SHIPMENT_DETAIL.Price_Date and tspl_sd_shipment_detail.item_code=tspl_item_price_master.item_code   and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  where TSPL_ITEM_PRICE_MASTER.UOM='KG' and Start_Date<='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and TSPL_ITEM_PRICE_MASTER.UOM='KG' and DOCUMENT_CODE='" & strDispatchNo & "' group by DOCUMENT_CODE,TSPL_ITEM_PRICE_MASTER.Item_Code,Start_Date,TSPL_SD_SHIPMENT_DETAIL.Item_Code )xx )ShimentDetail on ShimentDetail.document_code=TSPL_SD_SHIPMENT_HEAD.document_code and ShimentDetail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and ShimentDetail.start_date=  TSPL_SD_SALE_INVOICE_DETAIL.Price_Date and ShimentDetail.Price_Code=TSPL_SD_SALE_INVOICE_DETAIL.Price_code " & Environment.NewLine & _
            '" left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code   left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & Environment.NewLine
            '   '' " LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,Contact_Person_Email,Contact_Person_Name,Contact_Person_Phone from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE   ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' " & Environment.NewLine & _
            '   Qry += " left join (select Ship_To_Code,Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1,TSPL_SHIP_TO_LOCATION.Add2,TSPL_SHIP_TO_LOCATION.Add3,TSPL_SHIP_TO_LOCATION.Add4,TSPL_SHIP_TO_LOCATION.City_Code,Ship_To_Type_Code,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_SHIP_TO_LOCATION.CST_No,TSPL_SHIP_TO_LOCATION.Email,TSPL_SHIP_TO_LOCATION.Telphone,TSPL_STATE_MASTER.STATE_NAME as STATE_NAME,TSPL_CITY_MASTER.City_Name,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SHIP_TO_LOCATION.Contact_Person_Name as ShipTo_Contact_Person," & _
            '    " TSPL_SHIP_TO_LOCATION.Contact_Person_Phone as ShipTo_Contact_Per_Phone,TSPL_SHIP_TO_LOCATION.PAN AS ShipTo_Pan,TSPL_STATE_MASTER.GST_STATE_CODE AS ShipTo_GstStateCode,TSPL_SHIP_TO_LOCATION.GSTNO from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code left outer join TSPL_STATE_MASTER on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER.STATE_CODE) ship_cust on ship_cust.Ship_To_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & Environment.NewLine & _
            '    " and ship_cust.Ship_To_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location " & Environment.NewLine & _
            '    " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State   Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left join TSPL_Additional_Charges TAC ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TAC.Code LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2    left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7    left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9    left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10   left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10 left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   Uom_Detail.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Alternate_UOM   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state  left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state left join TSPL_SD_SALE_INVOICE_HEAD as Supplimentry_Head on TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=Supplimentry_Head.Document_Code  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location   left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State  left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_SD_SALE_INVOICE_HEAD.Created_By " & Environment.NewLine & _
            '    " LEFT OUTER JOIN TSPL_BATCH_ITEM ON TSPL_BATCH_ITEM.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code and TSPL_BATCH_ITEM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_BATCH_ITEM.Parent_Line_No =TSPL_SD_SALE_INVOICE_DETAIL.Line_No AND TSPL_BATCH_ITEM.Document_Type='PS-SH' " & Environment.NewLine


        End If
        Return Qry
    End Function

    '=============preeti Gupta======Ticket No[BM00000005041]
    Public Sub funPrint(ByVal strDocNo As String, Optional ByVal ForMailAttachment As Boolean = False, Optional ByVal strinvoicetype As String = Nothing, Optional ByVal ExcisableType As String = Nothing, Optional ByVal AllowManualVehicle As Boolean = False, Optional ByVal isPdf As Boolean = False)
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowToPrintInvoiceAfterPosting & "'")) = 1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct status from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')")) = 0 Then
                frmCRV.ShowCystalReportToolbar = False
            End If
        End If
        Dim strReportType As String = Nothing
        Dim strLocState As String = Nothing
        Dim strCustState As String = Nothing
        Dim strLocCode As String = Nothing
        Dim strCustCode As String = Nothing
        Dim IsTaxable As Double = 0
        Dim dtDocdate As Date?
        dtDocdate = Nothing
        Dim IsMandiTax As Double = 0
        Dim strTaxGroup As String = Nothing
        Dim StrSql = "Select Document_Date,Customer_Code,Bill_To_Location,is_taxable,Tax_Group from TSPL_SD_SALE_INVOICE_HEAD where Document_Code in ('" & strDocNo & "')"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
        If dt1.Rows.Count > 0 Then
            strLocCode = clsCommon.myCstr(dt1.Rows(0)("Bill_To_Location"))
            strCustCode = clsCommon.myCstr(dt1.Rows(0)("Customer_Code"))
            IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
            dtDocdate = clsCommon.myCDate(dt1.Rows(0)("Document_Date"))
            strTaxGroup = clsCommon.myCstr(dt1.Rows(0)("Tax_Group"))
        End If
        IsMandiTax = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & strTaxGroup & "' and Tax_Code in(select Tax_Code from TSPL_TAX_MASTER where Is_Mandi_Tax='Y')"))
        strLocState = clsDBFuncationality.getSingleValue("Select TSPL_LOCATION_MASTER.State from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" + strLocCode + "'")
        strCustState = clsDBFuncationality.getSingleValue("Select State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'")
        Dim ShowShipToPartyInDairyDispatch As Integer = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ShowShipToPartyInDairyDispatch & "'")) = 0, 0, 1)
        If IsTaxable = 1 Then
            If clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                strReportType = "L"
            Else
                strReportType = "I"
            End If
        Else
            strReportType = "NT"
        End If
        Try

            Dim query As String = ""
            ''RICHA UDL/31/03/19-000282 SHOW SHIPPING DETAILS FROM SHIP TO LOCATION WHEN IT IS SHOWN OTHERWISE CUSTOMER DETAILS SHOWN
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                'Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", strDocNo, Nothing)
                'Dim IsEInvoiceApply As Integer = 0
                'If IsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(dtDocdate) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                '    IsEInvoiceApply = 1
                'End If
                Dim DateOfEInvoiceImplementation As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DateOfEInvoiceImplementation, clsFixedParameterCode.DateOfEInvoiceImplementation, Nothing))
                Dim Supp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Invoice_No_For_Supplementary from TSPL_SD_SALE_INVOICE_head where Document_Code in ('" & strDocNo & "')"))
                If clsCommon.myLen(Supp) > 0 Then
                    query = "SELECT cast(I.BarCode_Img as image) As BarCode_Img,isnull (I.IRN_No,'') as IRN_No,isnull (I.Ack_No,'') as Ack_No,case when len(isnull (I.Ack_No,'')) > 0 then convert (varchar, I.Ack_Date,103) else ''  end as Ack_Date" &
                    ", case when I.Is_Taxable=1 and isnull(I.EInvoice_Type,'')='BB' AND convert(date ,I.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
                 " I.Status,(CASE WHEN ISNULL(I.STATUS,0)=1 THEN 'Authorized' else 'Un Authorized' end )as Doc_Status, D.Scheme_Item as Is_Scheme_Item, isnull(I.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
                        " (select top 1 convert(varchar,Document_Date,103)   from TSPL_SD_SALE_INVOICE_head where TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary='" & Supp & "') as supp_Date, isnull(IT.Is_Batch_Item,0) as Is_Batch_Item, I.EWayBillNo,CONVERT(VARCHAR,I.EWayBillDate,103) AS EWayBillDate,I.Electronic_Ref_No,COALESCE(IT.HSN_CODE, TAC.SAC_Code) AS HSN_CODE,case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.GSTNO ELSE C.GSTNO END AS C_GSTIN_NO,C.State as Cust_State,case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_STATE_MASTER_SHIPTO_lOCATION.STATE_CODE  ELSE cust_state_master.GST_STATE_Code END AS Cust_GST_StateCode , I.is_taxable,I.EWayBillNo,I.EWayBillDate,It.HSN_Code,T1.type as T1Type,T2.type as T2Type,T3.type as T3Type,T4.type as T4Type,T5.type as T5Type,T6.type as T6Type, T7.type as T7Type,CM.Comp_Name,L.Loc_Short_Name,COMP_ADDRESS=(CM.Add1+' '+CM.Add2+' '+CM.Add3+' '+CM.State),Loc_Address=(L.Add1+' '+L.Add2+' '+L.Add3+' '+L.Add4+' '+L.State), " &
                "L.City_Code,L.State,L.Pin_Code AS Location_Pincode ,L.TIN_No AS Loc_Tin_No,L.CST_No as Loc_CST_No,case when ISNULL(L.Phone1,'')='(+__)__________' then '' else L.Phone1 end +  Case When   ISNULL(L.Phone2,'')<>'(+__)__________' Then ', '+ L.Phone2 Else'' End   as Location_Telphone "
                    ''========== Change by Parteek=======
                    Dim Qry = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc from TSPL_USER_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_USER_MASTER.Default_Location where TSPL_USER_MASTER.User_Code='" & objCommonVar.CurrentUserCode & "'")
                    If clsCommon.CompairString(Qry, "Guwahati") = CompairStringResult.Equal Then
                        query += ",(L.Pin_Code) as COMP_PIN,(case when len(L.TIN_No)>0 then L.Tin_No ELSE L.Tin_No END )AS Tin_No ,L.Phone1,(case when len(CM.PanNo_Issue_Date)>0 then CM.Pan_No + ' DT ' +CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) ELSE CM.Pan_No END )AS Pan_No, Location_State_master.GST_STATE_Code AS Loc_GST_StateCode,L.State AS Loc_State "
                    Else
                        query += ",COMP_PIN=('PIN '+CM.Pincode+' CORP ID NO :-'+CM.CINNo),(case when len(CM.TinNo_Issue_Date)>0 then CM.Tin_No + ' DT ' +CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103) ELSE CM.Tin_No END )AS Tin_No ,CM.Phone1,(case when len(CM.PanNo_Issue_Date)>0 then CM.Pan_No + ' DT ' +CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) ELSE CM.Pan_No END )AS Pan_No,Location_State_master.GST_STATE_Code AS Loc_GST_StateCode,L.State AS Loc_State "
                    End If
                    query += " ,L.GSTNO AS Loc_GSTIN_NO,CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103)AS TinNo_Issue_Date ,CM.CST_LST,CM.CINNo, " &
       "CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) AS PanNo_Issue_Date ,CM.Pincode,CM.Email,CM.Tcan_No AS WebSite," &
       "TRAN_SACTION=(CASE WHEN SH.Including_Insurance =1 THEN 'GOODS ARE COVERED UNDER TRANSIT POLICY COVER/NOTE NO ' + (case when len(CM.Insurance_No)>0 then CONVERT(NVARCHAR(50),CM.Insurance_No) else '----------' end ) +' OF ' +CONVERT(NVARCHAR(200),CM.Insurance_Comp_Name) + ' VALID UPTO ' + CONVERT(NVARCHAR(15),CM.Insurance_Valid_Date,103) ELSE '' END),SH.Including_Insurance, " &
       "CM.CE_Division,CM.CE_Commissionerate,CM.Circle_No as Tariff,CM.Access_Officer as FSSAI,I.Description,I.Remarks, " &
       "  case when coalesce(p_cust.P_cust_code,'')='' then C.Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then C  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then C  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then C  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then C  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then C  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then C  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then C  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then C  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then C.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then C.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then C  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then C.Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_cust_name    end as P_Cust_Name," &
         "  case when coalesce(p_cust.P_cust_code,'')='' then CT.City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
        " case when coalesce(p_cust.P_cust_code,'')='' then cust_state_master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
      " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(C.Phone1,'')='(+__)__________' then '' else C.Phone1 end +  Case When   ISNULL(C.Phone2,'')<>'(+__)__________' Then ', '+ C.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then C.PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan, case when coalesce(p_cust.P_CustGstCode,'')='' then cust_state_master.GST_STATE_Code    when coalesce(p_cust.P_CustGstCode,'')<>'' then p_cust.P_CustGstCode   end as P_CustGstCode, " &
       "case when coalesce(C.parent_customer_no,'')<>'' then C2.Customer_Name else C.Customer_Name end as PCust_Name " &
    ",case when coalesce(C2.parent_customer_no,'')=coalesce(c2.Cust_Code,'') then 'Same As Buyer' when coalesce(C2.parent_customer_no,'')='' then 'Same As Buyer' else C2.Customer_Name end as C_Cust_Name," &
       "case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.Ship_To_Desc ELSE C.Customer_Name END AS Customer_Name,case when isnull(SH.Ship_To_Location,'')<>'' THEN (TSPL_SHIP_TO_LOCATION.Add1+' '+TSPL_SHIP_TO_LOCATION.Add2+' '+TSPL_SHIP_TO_LOCATION.Add3+' '+TSPL_SHIP_TO_LOCATION.STATE) ELSE (C.Add1+' '+C.Add2+' '+C.Add3+' '+C.STATE) END AS CUST_ADD, " &
       "case when coalesce(C.parent_customer_no,'')<>'' then (C2.Add1+' '+C2.Add2+' '+C2.Add3+' '+C2.STATE) else (C.Add1+' '+C.Add2+' '+C.Add3+' '+C.STATE) end as Pcust_Address " &
       ",CT.City_Name,C.CST,C.Cust_Code,Cust_Tin=(C.Tin_No),Cust_cst=c.CST,I.Document_Type,I.Document_Code,I.VAT_InvoiceNo,I.Excisable,I.Invoice_Type,I.VatInvoice_Type, " &
            "(CASE WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='T' THEN 'TAX INVOICE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='R' " &
        "THEN 'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='I' THEN 'SALE INVOICE-INTER STATE' END) AS VAT_HEADER, " &
         " (CASE WHEN LEN(ISNULL(I.Invoice_No_For_Supplementary,''))>0 and ISNULL(I.Supplementary_Type,'')='S' THEN 'Debit Note' WHEN LEN(ISNULL(I.Invoice_No_For_Supplementary,''))>0 and ISNULL(I.Supplementary_Type,'')='C' THEN 'Credit Note' WHEN I.Invoice_Type='T'  THEN 'TAX INVOICE' WHEN I.Invoice_Type='R' THEN 'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='I' and ( coalesce(C2.parent_customer_no,'')=coalesce(c.Cust_Code,'') or coalesce(C2.parent_customer_no,'')='') THEN   'SALE INVOICE-INTER STATE' WHEN I.Invoice_Type='I' and ( coalesce(C2.parent_customer_no,'')<>coalesce(c.Cust_Code,'') or coalesce(C2.parent_customer_no,'')<>'') THEN 'TAX INVOICE'   END) AS SALE_HEADER " &
         ",D.FOC_Item, " &
             "Document_Date=CONVERT(VARCHAR(100),I.Document_Date,103),CONVERT(VARCHAR(5),I.Modify_Date,108) AS DOCUMENT_TIME,(case when LEN(SH.Cust_PO_Date)>0 THEN SH.Cust_PO_No END) AS Cust_PO_No,(case when LEN(SH.Cust_PO_Date)>0 THEN CONVERT(VARCHAR(100),I.Cust_PO_Date,103)END ) AS Cust_PO_Date, " &
       "GE_DATE=CONVERT(VARCHAR(100),I.GEDate,103),I.GENo,(CASE WHEN LEN(SH.Transporter_Name)>0 THEN SH.Transporter_Name WHEN LEN(SH.Transporter_Name_Manual)>0 THEN SH.Transporter_Name_Manual END ) AS Transporter_Name ,I.Freight_Amount,Vehicle_Code=(case when len(VM.Number)>0 and VM.Number is not null then VM.Number else SH.Vehicle_Manual_No END) ,VM.Vehicle_Name,i.Final_Destination,SH.Road_Permit_no,SH.Document_Date AS Time_of_Prepration,SH.Removal_Date AS Time_of_Removal, " &
    "C_FORM=(CASE WHEN I.TAX1!='' and T1.Type='C' THEN 'FORM '+T1.Type + ' DUE' WHEN I.TAX2!='' and T2.Type='C' THEN 'FORM '+T2.Type + ' DUE' WHEN I.TAX3!='' and T3.Type='C' THEN 'FORM '+T3.Type + ' DUE' WHEN I.TAX4!='' and T4.Type='C' THEN 'FORM '+T4.Type + ' DUE' WHEN I.TAX5!='' and T5.Type='C' THEN 'FORM '+T5.Type + ' DUE' WHEN I.TAX6!='' and T6.Type='C' THEN 'FORM '+T6.Type + ' DUE' WHEN I.TAX7!='' and T7.Type='C' THEN 'FORM '+T7.Type + ' DUE' WHEN I.TAX8!='' and T8.Type='C' THEN 'FORM '+T8.Type + ' DUE' WHEN I.TAX9!='' and T9.Type='C' THEN 'FORM '+T9.Type + ' DUE' WHEN I.TAX10!='' and T10.Type='C' THEN 'FORM '+T10.Type + ' DUE' END ), " &
    "TAX1=REPLACE((CASE WHEN I.TAX1!='' AND T1.Type='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+CHAR(13)+ " &
             "(CASE WHEN I.TAX2!='' AND T2.Type='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX3!='' AND T3.Type='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX4!='' AND T4.Type='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX5!='' AND T5.Type='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX6!='' AND T6.Type='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX7!='' AND T7.Type='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX8!='' AND T8.Type='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX9!='' AND T9.Type='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX10!='' AND T10.Type='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''), " &
               "TAX2= " &
           "REPLACE((CASE WHEN I.TAX1!='' AND T1.Type!='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX2!='' AND T2.Type!='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX3!='' AND T3.Type!='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX4!='' AND T4.Type!='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX5!='' AND T5.Type!='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX6!='' AND T6.Type!='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX7!='' AND T7.Type!='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX8!='' AND T8.Type!='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX9!='' AND T9.Type!='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX10!='' AND T10.Type!='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''), " &
             " ISNULL(D.TAX1_RATE,0) AS DTAX1_RATE, ISNULL(D.TAX2_RATE,0) AS DTAX2_RATE, ISNULL(D.TAX3_RATE,0) AS DTAX3_RATE," &
          " ISNULL(D.TAX4_RATE,0) AS DTAX4_RATE, ISNULL(D.TAX5_RATE,0) AS DTAX5_RATE, ISNULL(D.TAX6_RATE,0) AS DTAX6_RATE," &
           " ISNULL(D.TAX7_RATE,0) AS DTAX7_RATE, ISNULL(D.TAX8_RATE,0) AS DTAX8_RATE, ISNULL(D.TAX9_RATE,0) AS DTAX9_RATE," &
            " ISNULL(D.TAX10_RATE,0) AS DTAX10_RATE," &
             " ISNULL(D.TAX1_Amt,0) AS DTAX1_Amt, ISNULL(D.TAX2_Amt,0) AS DTAX2_Amt, ISNULL(D.TAX3_Amt,0) AS DTAX3_Amt," &
          " ISNULL(D.TAX4_Amt,0) AS DTAX4_Amt, ISNULL(D.TAX5_Amt,0) AS DTAX5_Amt, ISNULL(D.TAX6_Amt,0) AS DTAX6_Amt," &
           " ISNULL(D.TAX7_Amt,0) AS DTAX7_Amt, ISNULL(D.TAX8_Amt,0) AS DTAX8_Amt, ISNULL(D.TAX9_Amt,0) AS DTAX9_Amt," &
            " ISNULL(D.TAX10_Amt,0) AS DTAX10_Amt," &
             " DTAX1.TYPE AS Tax1Type,DTAX2.TYPE AS Tax2Type,DTAX3.TYPE AS Tax3Type,DTAX4.TYPE AS Tax4Type,DTAX5.TYPE AS Tax5Type,DTAX6.TYPE AS Tax6Type," &
            " DTAX7.TYPE AS Tax7Type,DTAX8.TYPE AS Tax8Type,DTAX9.TYPE AS Tax9Type,DTAX10.TYPE AS Tax10Type," &
             "Add_Charge_Name1=(CASE WHEN I.Add_Charge_Name1!='' THEN (I.Add_Charge_Name1+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt1)) ELSE '' END)+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name2!='' THEN (I.Add_Charge_Name2+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt2)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name3!='' THEN (I.Add_Charge_Name3+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt3)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name4!='' THEN (I.Add_Charge_Name4+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt4)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name5!='' THEN (I.Add_Charge_Name5+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt5)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name6!='' THEN (I.Add_Charge_Name6+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt6)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name7!='' THEN (I.Add_Charge_Name7+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt7)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name8!='' THEN (I.Add_Charge_Name8+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt8)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name9!='' THEN (I.Add_Charge_Name9+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt9)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name10!='' THEN (I.Add_Charge_Name10+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt10)) ELSE '' END), " &
             " case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.State  ELSE  C.State  END AS CustomerState,L.State as LocationState, " &
             "i.Total_Add_Charge,NULL AS Gross_Wt,NULL AS Final_Gross_Wt,I.Discount_Amt,I.HeadDisc_Amt AS Head_DiscAmt,I.Discount_Amt as CasH_DiscountAmt,C.ECC,C.Form_Type,CM.Ecc_No,L.Location_Desc,I.RoundOffAmount,I.Total_Tax_Amt," &
             "D.Abatement_Amt, D.Abatement_Per as Abatement_Percent,COALESCE(IT.Item_Desc,TAC.DESCRIPTION) as Item_Desc,  D.Qty  as Qty , BI.Batch_No,D.Unit_Code, " &
             "(CASE WHEN IT.Is_Tax_Exempted=2 THEN D.MRP ELSE 0.00 END)AS MRP,I.Tax_Group," &
             "D.Item_Cost ,D.amount as Amount,I.Total_Amt,'' AS Packaging_Details,'' as Book_No,SH.GRNo,I.GR_Date,'' AS Mandi_Receipt_No ,'' AS Mandi_Receipt_Date,'' AS EXC_PLA,'' AS MODVAT " &
             "FROM TSPL_SD_SALE_INVOICE_HEAD I  " &
            "LEFT JOIN TSPL_SD_SHIPMENT_HEAD AS SH ON SH.Document_Code=I.Against_Shipment_No " &
             "JOIN TSPL_SD_SALE_INVOICE_DETAIL D ON D.DOCUMENT_CODE=I.Document_Code   " &
             " left join TSPL_SD_SHIPMENT_DETAIL SHD ON SHD.Document_Code=D.Shipment_Code AND SHD.Item_Code=D.Item_Code AND SHD.Line_No=D.Line_No " &
             " left JOIN TSPL_ITEM_MASTER IT ON D.Item_Code=IT.Item_Code " &
             " left join TSPL_Additional_Charges TAC ON D.Item_Code=TAC.Code" &
             " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
            "LEFT JOIN TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE ON TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code=SH.Delivery_Code_PS and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code=SHD.Item_Code AND TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Line_No=SHD.Line_No " &
             "JOIN TSPL_CUSTOMER_MASTER C ON C.Cust_Code=I.Customer_Code " &
             "LEFT outer JOIN TSPL_CUSTOMER_MASTER C2 ON C2.Parent_Customer_No=C.Cust_Code AND C2.Cust_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Ship_Party " + Environment.NewLine +
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =SH.Ship_To_Location " & Environment.NewLine &
            " left outer join TSPL_STATE_MASTER TSPL_STATE_MASTER_SHIPTO_lOCATION on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER_SHIPTO_lOCATION.STATE_CODE " & Environment.NewLine &
             "  left outer join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No," + Environment.NewLine +
             " Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,GST_STATE_Code as P_CustGstCode from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " + Environment.NewLine +
             " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE ) as P_Cust on P_Cust.P_cust_code=c.Parent_Customer_No " + Environment.NewLine +
             "LEFT JOIN TSPL_CITY_MASTER CT ON CT.City_Code=C.City_Code " &
             "JOIN TSPL_COMPANY_MASTER CM ON CM.Comp_Code=I.Comp_Code " &
            " JOIN TSPL_LOCATION_MASTER L ON L.Location_Code=I.Bill_To_Location " &
             "LEFT JOIN TSPL_TAX_MASTER T1 ON T1.Tax_Code=I.TAX1 " &
              "LEFT JOIN TSPL_TAX_MASTER T2 ON T2.Tax_Code=I.TAX2 " &
               "LEFT JOIN TSPL_TAX_MASTER T3 ON T3.Tax_Code=I.TAX3 " &
               "LEFT JOIN TSPL_TAX_MASTER T4 ON T4.Tax_Code=I.TAX4 " &
               "LEFT JOIN TSPL_TAX_MASTER T5 ON T5.Tax_Code=I.TAX5 " &
               "LEFT JOIN TSPL_TAX_MASTER T6 ON T6.Tax_Code=I.TAX6 " &
               "LEFT JOIN TSPL_TAX_MASTER T7 ON T7.Tax_Code=I.TAX7 " &
               "LEFT JOIN TSPL_TAX_MASTER T8 ON T8.Tax_Code=I.TAX8 " &
               "LEFT JOIN TSPL_TAX_MASTER T9 ON T9.Tax_Code=I.TAX9 " &
               "LEFT JOIN TSPL_TAX_MASTER T10 ON T10.Tax_Code=I.TAX10 " &
             " LEFT JOIN TSPL_TAX_MASTER DTAX1 ON DTAX1.Tax_Code=D.TAX1 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX2 ON DTAX2.Tax_Code=D.TAX2 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX3 ON DTAX3.Tax_Code=D.TAX3 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX4 ON DTAX4.Tax_Code=D.TAX4 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX5 ON DTAX5.Tax_Code=D.TAX5 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX6 ON DTAX6.Tax_Code=D.TAX6 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX7 ON DTAX7.Tax_Code=D.TAX7" &
             " LEFT JOIN TSPL_TAX_MASTER DTAX8 ON DTAX8.Tax_Code=D.TAX8" &
              " LEFT JOIN TSPL_TAX_MASTER DTAX9 ON DTAX9.Tax_Code=D.TAX9 " &
              " LEFT JOIN TSPL_TAX_MASTER DTAX10 ON DTAX10.Tax_Code=D.TAX10 " &
              " LEFT JOIN TSPL_STATE_MASTER Location_State_master on Location_State_master.STATE_CODE =L.State " &
         " left join TSPL_STATE_MASTER cust_state_master on cust_state_master.STATE_CODE=C.State " &
           "left join TSPL_STATE_MASTER Comp_state_master on Comp_state_master.STATE_CODE =CM.State " &
           "LEFT JOIN TSPL_VEHICLE_MASTER AS VM ON VM.Vehicle_Id=SH.Vehicle_Code " &
           "LEFT JOIN TSPL_BATCH_ITEM as BI ON BI.Document_Code=SHD.Document_Code AND BI.Parent_Line_No=SHD.Line_No AND BI.Item_Code=SHD.Item_Code AND BI.Document_Type='PS-SH' " &
             "WHERE I.Document_Code in ('" + strDocNo + "')"
                Else
                    query = "SELECT cast(I.BarCode_Img as image) As BarCode_Img,isnull (I.IRN_No,'') as IRN_No,isnull (I.Ack_No,'') as Ack_No,case when len(isnull (I.Ack_No,'')) > 0 then convert (varchar, I.Ack_Date,103) else ''  end as Ack_Date" &
                                        ", case when I.Is_Taxable=1 and isnull(I.EInvoice_Type,'')='BB' AND convert(date ,I.Document_Date,103)>=convert(date ,'" + clsCommon.myCstr(DateOfEInvoiceImplementation) + "',103) then 1 else 0 end as  IsEInvoiceApply," &
                                     " I.Status,(CASE WHEN ISNULL(I.STATUS,0)=1 THEN 'Authorized' else 'Un Authorized' end )as Doc_Status, D.Scheme_Item as Is_Scheme_Item, isnull(I.Invoice_No_For_Supplementary,'') as Invoice_No_For_Supplementary," &
                                            " (select top 1 convert(varchar,Document_Date,103)   from TSPL_SD_SALE_INVOICE_head where TSPL_SD_SALE_INVOICE_head.Invoice_No_For_Supplementary='" & Supp & "') as supp_Date, isnull(IT.Is_Batch_Item,0) as Is_Batch_Item, I.EWayBillNo,CONVERT(VARCHAR,I.EWayBillDate,103) AS EWayBillDate,I.Electronic_Ref_No,COALESCE(IT.HSN_CODE, TAC.SAC_Code) AS HSN_CODE,case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.GSTNO ELSE C.GSTNO END AS C_GSTIN_NO,C.State as Cust_State,case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_STATE_MASTER_SHIPTO_lOCATION.STATE_CODE  ELSE cust_state_master.GST_STATE_Code END AS Cust_GST_StateCode , I.is_taxable,I.EWayBillNo,I.EWayBillDate,It.HSN_Code,T1.type as T1Type,T2.type as T2Type,T3.type as T3Type,T4.type as T4Type,T5.type as T5Type,T6.type as T6Type, T7.type as T7Type,CM.Comp_Name,L.Loc_Short_Name,COMP_ADDRESS=(CM.Add1+' '+CM.Add2+' '+CM.Add3+' '+CM.State),Loc_Address=(L.Add1+' '+L.Add2+' '+L.Add3+' '+L.Add4+' '+L.State), " &
                                    "L.City_Code,L.State,L.Pin_Code AS Location_Pincode ,L.TIN_No AS Loc_Tin_No,L.CST_No as Loc_CST_No,case when ISNULL(L.Phone1,'')='(+__)__________' then '' else L.Phone1 end +  Case When   ISNULL(L.Phone2,'')<>'(+__)__________' Then ', '+ L.Phone2 Else'' End   as Location_Telphone "
                    ''========== Change by Parteek=======
                    Dim Qry = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc from TSPL_USER_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_USER_MASTER.Default_Location where TSPL_USER_MASTER.User_Code='" & objCommonVar.CurrentUserCode & "'")
                    If clsCommon.CompairString(Qry, "Guwahati") = CompairStringResult.Equal Then
                        query += ",(L.Pin_Code) as COMP_PIN,(case when len(L.TIN_No)>0 then L.Tin_No ELSE L.Tin_No END )AS Tin_No ,L.Phone1,(case when len(CM.PanNo_Issue_Date)>0 then CM.Pan_No + ' DT ' +CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) ELSE CM.Pan_No END )AS Pan_No, Location_State_master.GST_STATE_Code AS Loc_GST_StateCode,L.State AS Loc_State "
                    Else
                        query += ",COMP_PIN=('PIN '+CM.Pincode+' CORP ID NO :-'+CM.CINNo),(case when len(CM.TinNo_Issue_Date)>0 then CM.Tin_No + ' DT ' +CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103) ELSE CM.Tin_No END )AS Tin_No ,CM.Phone1,(case when len(CM.PanNo_Issue_Date)>0 then CM.Pan_No + ' DT ' +CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) ELSE CM.Pan_No END )AS Pan_No,Location_State_master.GST_STATE_Code AS Loc_GST_StateCode,L.State AS Loc_State "
                    End If
                    query += " ,L.GSTNO AS Loc_GSTIN_NO,CONVERT(VARCHAR(15),CM.TinNo_Issue_Date,103)AS TinNo_Issue_Date ,CM.CST_LST,CM.CINNo, " &
       "CONVERT(VARCHAR(15),CM.PanNo_Issue_Date,103) AS PanNo_Issue_Date ,CM.Pincode,CM.Email,CM.Tcan_No AS WebSite," &
       "TRAN_SACTION=(CASE WHEN SH.Including_Insurance =1 THEN 'GOODS ARE COVERED UNDER TRANSIT POLICY COVER/NOTE NO ' + (case when len(CM.Insurance_No)>0 then CONVERT(NVARCHAR(50),CM.Insurance_No) else '----------' end ) +' OF ' +CONVERT(NVARCHAR(200),CM.Insurance_Comp_Name) + ' VALID UPTO ' + CONVERT(NVARCHAR(15),CM.Insurance_Valid_Date,103) ELSE '' END),SH.Including_Insurance, " &
       "CM.CE_Division,CM.CE_Commissionerate,CM.Circle_No as Tariff,CM.Access_Officer as FSSAI,SH.Description,SH.Remarks, " &
       "  case when coalesce(p_cust.P_cust_code,'')='' then C.Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then C  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then C  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then C  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then C  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then C  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,case when coalesce(p_cust.P_cust_code,'')='' then C  .State     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_State   end as p_State, case when coalesce(p_cust.P_cust_code,'')='' then C  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then C  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then C.Lst_No when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then C.GSTNO when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_No    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then C  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then C.Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust.P_cust_name    end as P_Cust_Name," &
         "  case when coalesce(p_cust.P_cust_code,'')='' then CT.City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name," &
        " case when coalesce(p_cust.P_cust_code,'')='' then cust_state_master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name," &
      " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(C.Phone1,'')='(+__)__________' then '' else C.Phone1 end +  Case When   ISNULL(C.Phone2,'')<>'(+__)__________' Then ', '+ C.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,case when coalesce(p_cust.P_Pan,'')='' then C.PAN      when coalesce(p_cust.P_Pan,'')<>'' then p_cust.P_Pan   end as P_Pan, case when coalesce(p_cust.P_CustGstCode,'')='' then cust_state_master.GST_STATE_Code    when coalesce(p_cust.P_CustGstCode,'')<>'' then p_cust.P_CustGstCode   end as P_CustGstCode, " &
       "case when coalesce(C.parent_customer_no,'')<>'' then C2.Customer_Name else C.Customer_Name end as PCust_Name " &
    ",case when coalesce(C2.parent_customer_no,'')=coalesce(c2.Cust_Code,'') then 'Same As Buyer' when coalesce(C2.parent_customer_no,'')='' then 'Same As Buyer' else C2.Customer_Name end as C_Cust_Name," &
       "case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.Ship_To_Desc ELSE C.Customer_Name END AS Customer_Name,case when isnull(SH.Ship_To_Location,'')<>'' THEN (TSPL_SHIP_TO_LOCATION.Add1+' '+TSPL_SHIP_TO_LOCATION.Add2+' '+TSPL_SHIP_TO_LOCATION.Add3+' '+TSPL_SHIP_TO_LOCATION.STATE) ELSE (C.Add1+' '+C.Add2+' '+C.Add3+' '+C.STATE) END AS CUST_ADD, " &
       "case when coalesce(C.parent_customer_no,'')<>'' then (C2.Add1+' '+C2.Add2+' '+C2.Add3+' '+C2.STATE) else (C.Add1+' '+C.Add2+' '+C.Add3+' '+C.STATE) end as Pcust_Address " &
       ",CT.City_Name,C.CST,C.Cust_Code,Cust_Tin=(C.Tin_No),Cust_cst=c.CST,I.Document_Type,I.Document_Code,I.VAT_InvoiceNo,I.Excisable,I.Invoice_Type,I.VatInvoice_Type, " &
            "(CASE WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='T' THEN 'TAX INVOICE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='R' " &
        "THEN 'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='E' AND I.VatInvoice_Type='I' THEN 'SALE INVOICE-INTER STATE' END) AS VAT_HEADER, " &
         " (CASE WHEN I.Invoice_Type='T'  THEN 'TAX INVOICE' WHEN I.Invoice_Type='R' THEN 'SALE INVOICE-WITHIN STATE' WHEN I.Invoice_Type='I' and ( coalesce(C2.parent_customer_no,'')=coalesce(c.Cust_Code,'') or coalesce(C2.parent_customer_no,'')='') THEN   'SALE INVOICE-INTER STATE' WHEN I.Invoice_Type='I' and ( coalesce(C2.parent_customer_no,'')<>coalesce(c.Cust_Code,'') or coalesce(C2.parent_customer_no,'')<>'') THEN 'TAX INVOICE'   END) AS SALE_HEADER " &
         ",SHD.FOC_Item, " &
             "Document_Date=CONVERT(VARCHAR(100),I.Document_Date,103),CONVERT(VARCHAR(5),I.Modify_Date,108) AS DOCUMENT_TIME,(case when LEN(SH.Cust_PO_Date)>0 THEN SH.Cust_PO_No END) AS Cust_PO_No,(case when LEN(SH.Cust_PO_Date)>0 THEN CONVERT(VARCHAR(100),I.Cust_PO_Date,103)END ) AS Cust_PO_Date, " &
       "GE_DATE=CONVERT(VARCHAR(100),I.GEDate,103),I.GENo,(CASE WHEN LEN(SH.Transporter_Name)>0 THEN SH.Transporter_Name WHEN LEN(SH.Transporter_Name_Manual)>0 THEN SH.Transporter_Name_Manual END ) AS Transporter_Name ,I.Freight_Amount,Vehicle_Code=(case when len(VM.Number)>0 and VM.Number is not null then VM.Number else SH.Vehicle_Manual_No END) ,VM.Vehicle_Name,i.Final_Destination,SH.Road_Permit_no,SH.Document_Date AS Time_of_Prepration,SH.Removal_Date AS Time_of_Removal, " &
    "C_FORM=(CASE WHEN I.TAX1!='' and T1.Type='C' THEN 'FORM '+T1.Type + ' DUE' WHEN I.TAX2!='' and T2.Type='C' THEN 'FORM '+T2.Type + ' DUE' WHEN I.TAX3!='' and T3.Type='C' THEN 'FORM '+T3.Type + ' DUE' WHEN I.TAX4!='' and T4.Type='C' THEN 'FORM '+T4.Type + ' DUE' WHEN I.TAX5!='' and T5.Type='C' THEN 'FORM '+T5.Type + ' DUE' WHEN I.TAX6!='' and T6.Type='C' THEN 'FORM '+T6.Type + ' DUE' WHEN I.TAX7!='' and T7.Type='C' THEN 'FORM '+T7.Type + ' DUE' WHEN I.TAX8!='' and T8.Type='C' THEN 'FORM '+T8.Type + ' DUE' WHEN I.TAX9!='' and T9.Type='C' THEN 'FORM '+T9.Type + ' DUE' WHEN I.TAX10!='' and T10.Type='C' THEN 'FORM '+T10.Type + ' DUE' END ), " &
    "TAX1=REPLACE((CASE WHEN I.TAX1!='' AND T1.Type='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+CHAR(13)+ " &
             "(CASE WHEN I.TAX2!='' AND T2.Type='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX3!='' AND T3.Type='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX4!='' AND T4.Type='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX5!='' AND T5.Type='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX6!='' AND T6.Type='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX7!='' AND T7.Type='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX8!='' AND T8.Type='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX9!='' AND T9.Type='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.TAX10!='' AND T10.Type='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''), " &
               "TAX2= " &
           "REPLACE((CASE WHEN I.TAX1!='' AND T1.Type!='E' THEN(T1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX1_Amt)) ELSE '' END)+' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX2!='' AND T2.Type!='E' THEN(T2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX2_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX3!='' AND T3.Type!='E' THEN(T3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX3_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX4!='' AND T4.Type!='E' THEN(T4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX4_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX5!='' AND T5.Type!='E' THEN(T5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX5_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX6!='' AND T6.Type!='E' THEN(T6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX6_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX7!='' AND T7.Type!='E' THEN(T7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX7_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX8!='' AND T8.Type!='E' THEN(T8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX8_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX9!='' AND T9.Type!='E' THEN(T9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX9_Amt))ELSE '' END) +' '+CHAR(13)+ " &
           "(CASE WHEN I.TAX10!='' AND T10.Type!='E' THEN(T10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),I.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),I.TAX10_Amt))ELSE '' END),CHAR(13),''), " &
             " ISNULL(D.TAX1_RATE,0) AS DTAX1_RATE, ISNULL(D.TAX2_RATE,0) AS DTAX2_RATE, ISNULL(D.TAX3_RATE,0) AS DTAX3_RATE," &
          " ISNULL(D.TAX4_RATE,0) AS DTAX4_RATE, ISNULL(D.TAX5_RATE,0) AS DTAX5_RATE, ISNULL(D.TAX6_RATE,0) AS DTAX6_RATE," &
           " ISNULL(D.TAX7_RATE,0) AS DTAX7_RATE, ISNULL(D.TAX8_RATE,0) AS DTAX8_RATE, ISNULL(D.TAX9_RATE,0) AS DTAX9_RATE," &
            " ISNULL(D.TAX10_RATE,0) AS DTAX10_RATE," &
             " ISNULL(D.TAX1_Amt,0) AS DTAX1_Amt, ISNULL(D.TAX2_Amt,0) AS DTAX2_Amt, ISNULL(D.TAX3_Amt,0) AS DTAX3_Amt," &
          " ISNULL(D.TAX4_Amt,0) AS DTAX4_Amt, ISNULL(D.TAX5_Amt,0) AS DTAX5_Amt, ISNULL(D.TAX6_Amt,0) AS DTAX6_Amt," &
           " ISNULL(D.TAX7_Amt,0) AS DTAX7_Amt, ISNULL(D.TAX8_Amt,0) AS DTAX8_Amt, ISNULL(D.TAX9_Amt,0) AS DTAX9_Amt," &
            " ISNULL(D.TAX10_Amt,0) AS DTAX10_Amt," &
             " DTAX1.TYPE AS Tax1Type,DTAX2.TYPE AS Tax2Type,DTAX3.TYPE AS Tax3Type,DTAX4.TYPE AS Tax4Type,DTAX5.TYPE AS Tax5Type,DTAX6.TYPE AS Tax6Type," &
            " DTAX7.TYPE AS Tax7Type,DTAX8.TYPE AS Tax8Type,DTAX9.TYPE AS Tax9Type,DTAX10.TYPE AS Tax10Type," &
             "Add_Charge_Name1=(CASE WHEN I.Add_Charge_Name1!='' THEN (I.Add_Charge_Name1+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt1)) ELSE '' END)+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name2!='' THEN (I.Add_Charge_Name2+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt2)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name3!='' THEN (I.Add_Charge_Name3+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt3)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name4!='' THEN (I.Add_Charge_Name4+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt4)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name5!='' THEN (I.Add_Charge_Name5+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt5)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name6!='' THEN (I.Add_Charge_Name6+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt6)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name7!='' THEN (I.Add_Charge_Name7+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt7)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name8!='' THEN (I.Add_Charge_Name8+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt8)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name9!='' THEN (I.Add_Charge_Name9+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt9)) ELSE '' END) +' '+CHAR(13)+ " &
             "(CASE WHEN I.Add_Charge_Name10!='' THEN (I.Add_Charge_Name10+' : '+CONVERT(VARCHAR(10),I.Add_Charge_Amt10)) ELSE '' END), " &
             " case when isnull(SH.Ship_To_Location,'')<>'' THEN TSPL_SHIP_TO_LOCATION.State  ELSE  C.State  END AS CustomerState,L.State as LocationState, " &
             "i.Total_Add_Charge,SH.Gross_Item_Wt AS Gross_Wt,SH.Total_item_weightMetric AS Final_Gross_Wt,I.Discount_Amt,SH.HeadDisc_Amt AS Head_DiscAmt,SH.Discount_Amt as CasH_DiscountAmt,C.ECC,C.Form_Type,CM.Ecc_No,L.Location_Desc,I.RoundOffAmount,I.Total_Tax_Amt," &
             "D.Abatement_Amt, D.Abatement_Per as Abatement_Percent,COALESCE(IT.Item_Desc,TAC.DESCRIPTION) as Item_Desc, (case when isnull(IT.Is_Batch_Item,0)=1 then BI.Qty else D.Qty end) as Qty , BI.Batch_No,SHD.Unit_Code, " &
             "(CASE WHEN IT.Is_Tax_Exempted=2 THEN SHD.MRP ELSE 0.00 END)AS MRP,I.Tax_Group," &
             "D.Item_Cost ,coalesce(BI.Qty*SHD.Item_Cost,D.amount) as Amount,I.Total_Amt,'' AS Packaging_Details,'' as Book_No,SH.GRNo,I.GR_Date,'' AS Mandi_Receipt_No ,'' AS Mandi_Receipt_Date,'' AS EXC_PLA,'' AS MODVAT " &
             "FROM TSPL_SD_SALE_INVOICE_HEAD I  " &
            "LEFT JOIN TSPL_SD_SHIPMENT_HEAD AS SH ON SH.Document_Code=I.Against_Shipment_No " &
             "JOIN TSPL_SD_SALE_INVOICE_DETAIL D ON D.DOCUMENT_CODE=I.Document_Code   " &
             " left join TSPL_SD_SHIPMENT_DETAIL SHD ON SHD.Document_Code=D.Shipment_Code AND SHD.Item_Code=D.Item_Code AND SHD.Line_No=D.Line_No " &
             " left JOIN TSPL_ITEM_MASTER IT ON D.Item_Code=IT.Item_Code " &
             " left join TSPL_Additional_Charges TAC ON D.Item_Code=TAC.Code" &
             " LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code " &
            "LEFT JOIN TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE ON TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code=SH.Delivery_Code_PS and TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Item_Code=SHD.Item_Code AND TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Line_No=SHD.Line_No " &
             "JOIN TSPL_CUSTOMER_MASTER C ON C.Cust_Code=I.Customer_Code " &
             "LEFT outer JOIN TSPL_CUSTOMER_MASTER C2 ON C2.Parent_Customer_No=C.Cust_Code AND C2.Cust_Code=TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Ship_Party " + Environment.NewLine +
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =SH.Ship_To_Location " & Environment.NewLine &
            " left outer join TSPL_STATE_MASTER TSPL_STATE_MASTER_SHIPTO_lOCATION on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER_SHIPTO_lOCATION.STATE_CODE " & Environment.NewLine &
             "  left outer join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No," + Environment.NewLine +
             " Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,GST_STATE_Code as P_CustGstCode from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " + Environment.NewLine +
             " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE ) as P_Cust on P_Cust.P_cust_code=c.Parent_Customer_No " + Environment.NewLine +
             "LEFT JOIN TSPL_CITY_MASTER CT ON CT.City_Code=C.City_Code " &
             "JOIN TSPL_COMPANY_MASTER CM ON CM.Comp_Code=I.Comp_Code " &
            " JOIN TSPL_LOCATION_MASTER L ON L.Location_Code=I.Bill_To_Location " &
             "LEFT JOIN TSPL_TAX_MASTER T1 ON T1.Tax_Code=I.TAX1 " &
              "LEFT JOIN TSPL_TAX_MASTER T2 ON T2.Tax_Code=I.TAX2 " &
               "LEFT JOIN TSPL_TAX_MASTER T3 ON T3.Tax_Code=I.TAX3 " &
               "LEFT JOIN TSPL_TAX_MASTER T4 ON T4.Tax_Code=I.TAX4 " &
               "LEFT JOIN TSPL_TAX_MASTER T5 ON T5.Tax_Code=I.TAX5 " &
               "LEFT JOIN TSPL_TAX_MASTER T6 ON T6.Tax_Code=I.TAX6 " &
               "LEFT JOIN TSPL_TAX_MASTER T7 ON T7.Tax_Code=I.TAX7 " &
               "LEFT JOIN TSPL_TAX_MASTER T8 ON T8.Tax_Code=I.TAX8 " &
               "LEFT JOIN TSPL_TAX_MASTER T9 ON T9.Tax_Code=I.TAX9 " &
               "LEFT JOIN TSPL_TAX_MASTER T10 ON T10.Tax_Code=I.TAX10 " &
             " LEFT JOIN TSPL_TAX_MASTER DTAX1 ON DTAX1.Tax_Code=D.TAX1 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX2 ON DTAX2.Tax_Code=D.TAX2 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX3 ON DTAX3.Tax_Code=D.TAX3 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX4 ON DTAX4.Tax_Code=D.TAX4 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX5 ON DTAX5.Tax_Code=D.TAX5 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX6 ON DTAX6.Tax_Code=D.TAX6 " &
            " LEFT JOIN TSPL_TAX_MASTER DTAX7 ON DTAX7.Tax_Code=D.TAX7" &
             " LEFT JOIN TSPL_TAX_MASTER DTAX8 ON DTAX8.Tax_Code=D.TAX8" &
              " LEFT JOIN TSPL_TAX_MASTER DTAX9 ON DTAX9.Tax_Code=D.TAX9 " &
              " LEFT JOIN TSPL_TAX_MASTER DTAX10 ON DTAX10.Tax_Code=D.TAX10 " &
              " LEFT JOIN TSPL_STATE_MASTER Location_State_master on Location_State_master.STATE_CODE =L.State " &
         " left join TSPL_STATE_MASTER cust_state_master on cust_state_master.STATE_CODE=C.State " &
           "left join TSPL_STATE_MASTER Comp_state_master on Comp_state_master.STATE_CODE =CM.State " &
           "LEFT JOIN TSPL_VEHICLE_MASTER AS VM ON VM.Vehicle_Id=SH.Vehicle_Code " &
           "LEFT JOIN TSPL_BATCH_ITEM as BI ON BI.Document_Code=SHD.Document_Code AND BI.Parent_Line_No=SHD.Line_No AND BI.Item_Code=SHD.Item_Code AND BI.Document_Type='PS-SH' " &
             "WHERE I.Document_Code in ('" + strDocNo + "')"
                End If


                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(query)
                If dt3.Rows.Count > 0 Then
                    '=======================GST(20/06/2017)==========================
                    If clsERPFuncationality.GetGSTStatus(dtDocdate) Then
                        If IsTaxable = 1 Then
                            If clsCommon.CompairString(strTaxGroup, "EXEMPTED") = CompairStringResult.Equal Then
                                strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_NonTaxable", "Bill Of Supply", dtDocdate)
                            Else

                                If clsCommon.CompairString(dt3.Rows(0)("Loc_State"), dt3.Rows(0)("Cust_State")) = CompairStringResult.Equal Then
                                    If IsMandiTax > 0 Then
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_LocalWithMandiTax", "Tax Invoice", dtDocdate)
                                    Else
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_Local", "Tax Invoice", dtDocdate)
                                    End If
                                Else
                                    If IsMandiTax > 0 Then
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_InterStateWithMandiTax", "Tax Invoice", dtDocdate)
                                    Else
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_InterState", "Tax Invoice", dtDocdate)
                                    End If
                                End If
                            End If
                        Else
                            strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleInvoice_NonTaxable", "Bill Of Supply", dtDocdate)
                        End If
                    Else

                        If clsCommon.CompairString(strinvoicetype, "DepoPrint") = CompairStringResult.Equal Then
                            strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptDepoCentralTaxInvoice", "Depo Print")
                        Else

                            If clsCommon.CompairString(ExcisableType, "Excise Invoice") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "T") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "I") = CompairStringResult.Equal Then
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                                        ''frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                                    Else

                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                                    End If
                                End If
                            ElseIf clsCommon.CompairString(ExcisableType, "Vat Invoice") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "E") = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "T") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "R") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("VatInvoice_Type")), "I") = CompairStringResult.Equal Then
                                        '' frmCrystalReportViewer.funreport(CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")
                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSalesTaxInvoice", "Vat Invoice")
                                    Else

                                        strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptProductSaleExciseInvoice", "Excise Invoice")

                                    End If
                                End If
                            End If

                            If clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "T") = CompairStringResult.Equal Then
                                ''========== Change by Parteek=======
                                Dim qry1 As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc from TSPL_USER_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_USER_MASTER.Default_Location where TSPL_USER_MASTER.User_Code='" & objCommonVar.CurrentUserCode & "'")
                                If clsCommon.CompairString(qry1, "Guwahati") = CompairStringResult.Equal Then
                                    strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoiceLoc", "Tax Invoice")
                                Else
                                    strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Tax Invoice")
                                End If


                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "R") = CompairStringResult.Equal Then
                                strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Retail Invoice")
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt3.Rows(0)("Invoice_Type")), "I") = CompairStringResult.Equal Then
                                ''========== Change by Parteek=======
                                Dim qry1 As String = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Location_Desc from TSPL_USER_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_USER_MASTER.Default_Location where TSPL_USER_MASTER.User_Code='" & objCommonVar.CurrentUserCode & "'")
                                If clsCommon.CompairString(qry1, "Guwahati") = CompairStringResult.Equal Then
                                    strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoiceInter", "Interstate Invoice")
                                Else
                                    strrptpath = frmCRV.funreport(isPdf, CrystalReportFolder.NewSalesReports, dt3, "rptCentralTaxInvoice", "Interstate Invoice")
                                End If
                            End If
                        End If

                    End If
                    '====================End(GST)==========
                End If

            Else
                Dim Qry As String = Nothing
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ALPHA") = CompairStringResult.Equal Then
                    Qry = GetQueryALPHA(strDocNo, AllowManualVehicle)
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                    Qry = GetQueryVIJAYA(strDocNo)
                Else
                    Qry = GetQuery(strDocNo)
                End If

                Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strDocNo + "')"
                'Qry += " where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' order by TSPL_SD_SALE_INVOICE_DETAIL.line_no" 


                ''richa ALF/19/09/18-000084
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ALPHA") = CompairStringResult.Equal Then
                    If isPdf = True Then
                        Qry = " Select * from (" & Qry & ") XXX , (Select '1' as COL1, 1 as COL2,  '' as CopyType1 "
                    Else
                        Qry = " Select * from (" & Qry & ") XXX , (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 "
                        If clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SinglePrintCopyDairyInvoice, clsFixedParameterCode.SinglePrintCopyDairyInvoice, Nothing))) = False Then
                            Qry = Qry + " UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1"
                        End If
                    End If
                    Qry = Qry + " ) YYY ORDER BY YYY.COL2,XXX.Line_No "
                Else
                    'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") <> CompairStringResult.Equal Then
                    If isPdf = True Then
                            Qry = " Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  '' as CopyType1 "
                        Else
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then ' not need copy word for EROD
                            Qry = " Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1 "
                        Else
                            Qry = " Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 "
                            End If

                            If clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SinglePrintCopyDairyInvoice, clsFixedParameterCode.SinglePrintCopyDairyInvoice, Nothing))) = False Then
                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                Qry = Qry + " UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE' as CopyType1 "
                            Else
                                Qry = Qry + " UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1"
                                End If

                            End If
                        End If
                        Qry = Qry + " ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2,XXX.Line_No "
                        'End If
                    End If
                ''---------------

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                '===================detail taxes'''''''''''''''''''''''''
                Dim dt3_qry As String = "select  final.DOCUMENT_CODE ,TSPL_TAX_MASTER.Tax_Code_Desc as TAX1 ,final.TAX1_Rate ,final.tax_amt  from (select Document_Code,TAX1,TAX1_Rate,sum(tax_amt) as tax_amt from ( " & _
                " select Document_Code,tax1,TAX1_Rate,sum(tax1_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax1,TAX1_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax2,TAX2_Rate,sum(tax2_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax2,TAX2_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax3,TAX3_Rate,sum(tax3_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax3,TAX3_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax4,TAX4_Rate,sum(tax4_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax4,TAX4_Rate,Document_Code " & _
                " union all  " & _
                " select Document_Code,tax5,TAX5_Rate,sum(tax5_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax5,TAX5_Rate,Document_Code " & _
                " union all" & _
                " select Document_Code,tax6,TAX6_Rate,sum(tax6_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax6,TAX6_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax7,TAX7_Rate,sum(tax7_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax7,TAX7_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax8,TAX8_Rate,sum(tax8_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax8,TAX8_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax9,TAX9_Rate,sum(tax9_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax9,TAX9_Rate,Document_Code " & _
                " union all " & _
                " select Document_Code,tax10,TAX10_Rate,sum(tax10_amt) as tax_amt from TSPL_SD_SALE_INVOICE_DETAIL group by tax10,TAX10_Rate,Document_Code " & _
                " )a where len(isnull(tax1,''))>0 and Document_Code in ('" + strDocNo + "')  group by Document_Code,TAX1,TAX1_Rate )final" & _
                " left outer join (select * from (select 1 as sno,Document_Code,tax1 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax1,'')<>'' union all select 2 as sno,Document_Code,tax2 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax2,'')<>'' union all select 3 as sno,Document_Code,tax3 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax3,'')<>'' union all select 4 as sno,Document_Code,tax4 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax4,'')<>'' union all select 5 as sno,Document_Code,tax5 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax5,'')<>'' union all select 6 as sno,Document_Code,tax6 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax6,'')<>'' union all select 7 as sno,Document_Code,tax7 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax7,'')<>'' union all select 8 as sno,Document_Code,tax8 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax8,'')<>'' union all select 9 as sno,Document_Code,tax9 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax9,'')<>'' union all select 10 as sno,Document_Code,tax10 from TSPL_SD_SALE_INVOICE_HEAD where isnull(tax10,'')<>'')s where s.Document_Code in ('" + strDocNo + "'))ax on ax.Document_Code=final.Document_Code and ax.tax1=final.tax1  left join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =ax.TAX1  order by ax.sno,final.tax1_rate "
                Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(dt3_qry)
                '' --------------------shivani 30/03/2015
                ' Ticket No : BHA/21/05/18-000029, BHA/06/06/18-000042 By Prabhakar 
                '=============================================== Hash code Summary Start =========================================
                Dim dtHashCodeSummary_qry As String = "  select (final.InvoiceNo) as InvoiceNo, final.HSN_Code, sum (Final.Amount- Final.Disc_Amt) as  Amount, max(TAX1_Rate) as TAX1_Rate, sum (Final.TAX1_Amt) as TAX1_Amt ,max( Final.tax1name) as tax1name , max (TAX2_Rate ) as  TAX2_Rate,sum( TAX2_Amt) as TAX2_Amt  , max( tax2name) as tax2name " &
                                                      "  ,  max (TAX3_Rate ) as  TAX3_Rate,sum( TAX3_Amt) as TAX3_Amt  , max( isnull(tax3name,'')) as tax3name " &
                                                      "  ,  max (TAX4_Rate ) as  TAX4_Rate,sum( TAX4_Amt) as TAX4_Amt  , max( isnull( tax4name,'')) as tax4name " &
                                                      "  ,  max (TAX5_Rate ) as  TAX5_Rate,sum( TAX5_Amt) as TAX5_Amt  , max( isnull(tax5name,'')) as tax5name " &
                                                      "  ,  max (TAX6_Rate ) as  TAX6_Rate,sum( TAX6_Amt) as TAX6_Amt  , max( isnull(tax6name,'')) as tax6name " &
                                                      "  ,  max (TAX7_Rate ) as  TAX7_Rate,sum( TAX7_Amt) as TAX7_Amt  , max( isnull(tax7name,'')) as tax7name " &
                                                      "  ,  max (TAX8_Rate ) as  TAX8_Rate,sum( TAX8_Amt) as TAX8_Amt  , max( isnull(tax8name,'')) as tax8name " &
                                                      "  ,  max (TAX9_Rate ) as  TAX9_Rate,sum( TAX9_Amt) as TAX9_Amt  , max( isnull(tax9name,'')) as tax9name " &
                                                      "  ,  max (TAX10_Rate ) as  TAX10_Rate,sum( TAX10_Amt) as TAX10_Amt  , max( isnull(tax10name,'')) as tax10name " &
                                                      "  from (select TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE  as InvoiceNo, TSPL_ITEM_MASTER.HSN_Code ,TSPL_SD_SALE_INVOICE_DETAIL.amount, isnull(TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,0) as Disc_Amt, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate , " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate , " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX6 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX7 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX8 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX9 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate, " &
                                                      "  TSPL_SD_SALE_INVOICE_DETAIL.TAX10 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate, " &
                                                      "  tax1.Type as tax1name,tax2.Type as tax2name,tax3.Type as tax3name " &
                                                      "  ,tax4.Type as tax4name ,tax5.Type as tax5name, tax6.Type as tax6name, tax7.Type as tax7name, tax8.Type as tax8name, tax9.Type as tax9name, tax10.Type as tax10name " &
                                                      "  from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE        where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strDocNo + "') and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item ='N' ) final group by final.InvoiceNo, final.HSN_Code "
                Dim dtHashCodeSummary As DataTable = clsDBFuncationality.GetDataTable(dtHashCodeSummary_qry)

                '=============================================== Hash code Summary End =========================================
                Dim count As Integer = 0
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    Dim UOMKG As String = String.Empty

                    For i As Integer = 0 To dt.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("UOM")), clsCommon.myCstr(dt.Rows(0)("UOM"))) = CompairStringResult.Equal Then
                            count = count + 1

                        End If
                    Next
                End If
                dt.Columns.Add("UOMKG")
                If dt.Rows.Count = count Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("UOMKG") = "T"
                    Next
                Else
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("UOMKG") = "F"
                    Next
                End If
                ''--------------------------------------------
                Dim Qry2 As String = "select TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE  as InvoiceNo,Abatement_Amt,TSPL_ITEM_MASTER.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Batch_No,TSPL_ITEM_MASTER.Item_Desc + '( MRP : ' +   convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.MRP) + '  Abatement : ' + convert(varchar,convert(int,100- TSPL_SD_SALE_INVOICE_DETAIL.Abatement_Per)) + '%)'  as Item_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate,TSPL_ITEM_MASTER.Cheapter_Heads  ,tax1.Tax_Code_Desc as tax1name,tax2.Tax_Code_Desc as tax2name,tax3.Tax_Code_Desc as tax3name   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE       "
                Qry2 += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strDocNo + "')"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                '===added by preeti gupta against ticket no [BM00000009965] 10/10/2016
                Dim InvoiceType As String = clsDBFuncationality.getSingleValue(" select top 1 TSPL_SD_SALE_INVOICE_head.Invoice_Type from TSPL_SD_SALE_INVOICE_head where TSPL_SD_SALE_INVOICE_HEAD.Document_Code in ('" + strDocNo + "')")
                '======================================ERODE===================
                ''''Done Work by prabahakar for Same invoice of Taxable /Non Taxable Item --Comment By Balwinder on 12/04/2023
                ''If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                ''    If dt.Rows.Count > 0 Then
                ''        Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                ''        Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                ''        Dim CustomerCode As String = ""
                ''        If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                ''            For Each dr As DataRow In dt10.Rows
                ''                CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                ''            Next
                ''            If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                ''                CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                ''            End If
                ''        End If
                ''        Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                ''        strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoiceAll", "Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding, "rptItemHashCodeSummayGST.rpt", dtHashCodeSummary)

                ''    Else

                ''    End If
                ''    Exit Sub
                ''End If
                ''==============================================================

                '---------------RCDFCF----------------------------------------
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDFCF") = CompairStringResult.Equal Then
                    If dt.Rows.Count > 0 Then
                        Dim QryItemSummary As String = "select max(TSPL_ITEM_MASTER.Item_Desc) AS Item_Name 
                                                        ,SUM(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor) as WeightInKg
                                                        ,SUM((TSPL_SD_SALE_INVOICE_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) /BagUOM.Conversion_Factor) AS BagCount
                                                       ,sum(TSPL_SD_SALE_INVOICE_DETAIL.Amount) as Amt
                                                        from TSPL_SD_SALE_INVOICE_HEAD 
                                                        left outer join TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
                                                        LEFT OUTER JOIN TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                                        left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code 
                                                        left outer join TSPL_ITEM_UOM_DETAIL as BagUOM ON BagUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and BagUOM.UOM_Code='Bag'
                                                        where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + strDocNo + "'
                                                        group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code"
                        Dim dtItemSummary As DataTable = clsDBFuncationality.GetDataTable(QryItemSummary)
                        strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoiceParty", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptSubItemSummary.rpt", dtItemSummary)
                    End If
                    Exit Sub
                End If
                '---------------RCDFCF----------------------------------------

                If InvoiceType = "E" Then
                            If dt.Rows.Count > 0 Then
                                ''richa agarwal call report with its subreport data'''
                                '  KwalitySalesReportViewer.funsubreportWithdt(dt, dt2, "rptProductExciseTransferSaleInvoice", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt")
                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GDFPL") <> CompairStringResult.Equal Then
                                    If Not ForMailAttachment Then
                                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptProductExciseTransferSaleInvoice", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
                                    Else
                                        strrptpath = frmCRV.EmailSubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptProductExciseTransferSaleInvoice", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
                                    End If
                                Else
                                    If Not ForMailAttachment Then
                                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptSaleInvoiceTaxExcise", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter(), "MMM.rpt", dt3)
                                    Else
                                        strrptpath = frmCRV.EmailSubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptSaleInvoiceTaxExcise", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter(), "MMM.rpt", dt3)
                                    End If
                                End If
                                ''----------------
                            End If
                        Else
                            If dt.Rows.Count > 0 Then
                                'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Retail Invoice", "rptCompanyAddress.rpt")
                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GDFPL") <> CompairStringResult.Equal Then
                                    If Not ForMailAttachment Then
                                        '================Sanjeet==================
                                        If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("InvoiceDate"))) Then
                                            If clsCommon.CompairString(strReportType, "L") = CompairStringResult.Equal Then
                                                If clsCommon.CompairString(strTaxGroup, "EXEMPTED") = CompairStringResult.Equal Then
                                                    If ShowShipToPartyInDairyDispatch = 1 Then
                                                        If clsCommon.CompairString(Me.Form_ID, "INVOICE-DS") = CompairStringResult.Equal Then
                                                            Dim Qry1 As String = Nothing
                                                            Dim objMultPrintInvoice As New FrmPrintFreshInvoice

                                                            Qry1 = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                            'Qry = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                                                            If dt5.Rows.Count > 0 Then
                                                                If ShowShipToPartyInDairyDispatch = 1 Then
                                                                    strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                                Else
                                                                    strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                                End If
                                                            End If
                                                        End If
                                                    Else
                                                        ' done by priti BHA/17/08/18-000452 for bharat to correct customer dashboard problem
                                                        Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                        Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                        Dim CustomerCode As String = ""
                                                        If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                            For Each dr As DataRow In dt10.Rows
                                                                CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                            Next
                                                            If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                            End If
                                                        End If

                                                        Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                                                        strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice _NonTaxable", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                                                    End If
                                                Else
                                                    If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Is_GST_UT")), 1) = CompairStringResult.Equal Then
                                                        If ShowShipToPartyInDairyDispatch = 1 Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_UTParty", "Tax Invoice For UT", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                        Else
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_UT", "Tax Invoice For UT", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptItemHashCodeSummaryCGST_UGST.rpt", dtHashCodeSummary)
                                                        End If
                                                    Else
                                                        If IsMandiTax > 0 Then
                                                            If ShowShipToPartyInDairyDispatch = 1 Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                                strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_WithMandiTaxParty", "Tax Invoice with MandiTax", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                            Else
                                                                strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_WithMandiTax", "Tax Invoice with MandiTax", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                            End If
                                                        Else
                                                    If ShowShipToPartyInDairyDispatch = 1 Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                            ' done by richa ERO/23/11/18-000418 for customer dashboard  ERO/30/11/18-000424 30 Nov,2018 richa 
                                                            Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                            Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                            Dim CustomerCode As String = ""
                                                            If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                                For Each dr As DataRow In dt10.Rows
                                                                    CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                                Next
                                                                If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                    CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                                End If
                                                            End If

                                                            Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoiceParty", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                                        Else
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoiceParty", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                        End If
                                                    Else
                                                        ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                        'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptItemHashCodeSummary.rpt", dtHashCodeSummary)

                                                        ' done by priti BHA/17/08/18-000452 for bharat to correct customer dashboard problem
                                                        Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                                Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                                Dim CustomerCode As String = ""
                                                                If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                                    For Each dr As DataRow In dt10.Rows
                                                                        CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                                    Next
                                                                    If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                        CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                                    End If
                                                                End If

                                                                Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))

                                                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                                                                    dt.Columns.Add("OpngBal", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("OpngBal"))
                                                                    dt.Columns.Add("DrAmt", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("DrAmt"))
                                                                    dt.Columns.Add("CrAmt", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("CrAmt"))
                                                                End If

                                                                strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptItemHashCodeSummary.rpt", dtHashCodeSummary, "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                                                                'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dtHashCodeSummary, "rptProductSaleInvoice", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptItemHashCodeSummary")
                                                            End If
                                                        End If
                                                    End If

                                                End If
                                            ElseIf clsCommon.CompairString(strReportType, "I") = CompairStringResult.Equal Then
                                                If clsCommon.CompairString(strTaxGroup, "EXEMPTED") = CompairStringResult.Equal Then
                                                    If ShowShipToPartyInDairyDispatch = 1 Then
                                                        If clsCommon.CompairString(Me.Form_ID, "INVOICE-DS") = CompairStringResult.Equal Then
                                                            Dim Qry1 As String = Nothing
                                                            Dim objMultPrintInvoice As New FrmPrintFreshInvoice

                                                            Qry1 = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                            'Qry = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                                                            If dt.Rows.Count > 0 Then
                                                                If ShowShipToPartyInDairyDispatch = 1 Then
                                                                    strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                                Else
                                                                    strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                                End If
                                                            End If
                                                        End If
                                                    Else
                                                        ' done by priti BHA/17/08/18-000452 for bharat to correct customer dashboard problem
                                                        Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                        Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                        Dim CustomerCode As String = ""
                                                        If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                            For Each dr As DataRow In dt10.Rows
                                                                CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                            Next
                                                            If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                            End If
                                                        End If
                                                        ' done by priti BHA/17/08/18-000457 for bharat to correct customer dashboard problem for all type reports
                                                        Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                                                        strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice _NonTaxable", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                                                    End If
                                                Else
                                                    If IsMandiTax > 0 Then
                                                        If ShowShipToPartyInDairyDispatch = 1 OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_InterstateWithMandiTaxParty", "Tax Invoice With Mandi Tax", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                        Else
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_InterstateWithMandiTax", "Tax Invoice With Mandi Tax", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                        End If
                                                    Else
                                                        If ShowShipToPartyInDairyDispatch = 1 OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                                                ' done by ERO/23/11/18-000418 for erode richa agarwal ERO/30/11/18-000424 30 Nov,2018 richa 
                                                                Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                                Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                                Dim CustomerCode As String = ""
                                                                If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                                    For Each dr As DataRow In dt10.Rows
                                                                        CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                                    Next
                                                                    If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                        CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                                    End If
                                                                End If
                                                                Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                                                                strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_InterstateParty", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding, "rptItemHashCodeSummayGST.rpt", dtHashCodeSummary)
                                                            Else
                                                                strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_InterstateParty", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                            End If
                                                        Else
                                                            ' done by priti BHA/17/08/18-000452 for bharat to correct customer dashboard problem
                                                            Qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & strDocNo & "')"
                                                            Dim dt10 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                                            Dim CustomerCode As String = ""
                                                            If (dt10 IsNot Nothing AndAlso dt10.Rows.Count > 0) Then
                                                                For Each dr As DataRow In dt10.Rows
                                                                    CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                                                                Next
                                                                If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                                                    CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                                                                End If
                                                            End If
                                                            Dim dtCustomerOutstanding As DataTable = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "dd/MMM/yyyy"))
                                                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                                                                dt.Columns.Add("OpngBal", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("OpngBal"))
                                                                dt.Columns.Add("DrAmt", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("DrAmt"))
                                                                dt.Columns.Add("CrAmt", GetType(Decimal), dtCustomerOutstanding.Rows(0).Item("CrAmt"))
                                                            End If
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_Interstate", "Tax Invoice", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3, "rptItemHashCodeSummary_IGST.rpt", dtHashCodeSummary, "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                                                        End If
                                                    End If
                                                End If
                                            ElseIf clsCommon.CompairString(strReportType, "NT") = CompairStringResult.Equal Then
                                                If clsCommon.CompairString(Me.Form_ID, "INVOICE-DS") = CompairStringResult.Equal Then
                                                    Dim Qry1 As String = Nothing
                                                    Dim objMultPrintInvoice As New FrmPrintFreshInvoice

                                                    Qry1 = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                    'Qry = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                                                    Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                                                    If dt5.Rows.Count > 0 Then
                                                        If ShowShipToPartyInDairyDispatch = 1 Then
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                        Else
                                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt5, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                                        End If
                                                    End If
                                                Else
                                                    strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice _NonTaxable", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                                End If
                                            End If
                                        Else
                                            strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Retail Invoice", "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                        End If
                                    Else
                                        strrptpath = frmCRV.EmailSubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice", "Retail Invoice", "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                    End If
                                Else
                                    If Not ForMailAttachment Then
                                        strrptpath = frmCRV.funsubreportWithdt(isPdf, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptSaleInvoiceTax", "Retail Invoice", "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                    Else
                                        strrptpath = frmCRV.EmailSubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptSaleInvoiceTax", "Retail Invoice", "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                                    End If
                                End If
                            End If
                        End If
                    End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        frmCRV = Nothing
    End Sub

    '=============Rohit Gupta======Ticket No[BM00000005988]
    Public Sub funPrintChallan(ByVal strDocNo As String, Optional ByVal AllowManualVehicle As Boolean = False)
        Dim frmCRV As New frmCrystalReportViewer()
        Dim Qry As String = ""
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ALPHA") = CompairStringResult.Equal Then
                Qry = GetQueryALPHA(strDocNo, AllowManualVehicle)
            Else
                Qry = GetQuery(strDocNo)
            End If

            'Dim Qry As String = " select TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name,TSPL_SD_SHIPMENT_HEAD.GRNo,convert(date,TSPL_SD_SHIPMENT_HEAD.GR_Date,103) as GR_Date ,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_DETAIL.Alter_UnitQty,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt ,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate , TSPL_CHAPTER_HEAD.Description as Chap_Desc,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, case when TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms='A' then 'Advance' else TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms end as Payment_Terms, TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_SD_SHIPMENT_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_SD_SHIPMENT_HEAD.RoadPermit_Date,103) as RoadPermit_Date ,"
            'Qry += " TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate, TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date,103) as Challan_Date,TSPL_SD_SHIPMENT_HEAD.Removal_Date,"
            'Qry += "   TSPL_SD_SALE_INVOICE_HEAD.total_add_charge ,TSPL_SD_SALE_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end"

            'Qry += "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end"
            'Qry += "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end"
            'Qry += "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end"
            'Qry += " + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end"
            'Qry += "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end"

            'Qry += "+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end "
            'Qry += "+  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End "
            'Qry += "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end "


            'Qry += " as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email,( case when TSPL_SD_SALE_INVOICE_head.Invoice_Type='R' then 'Retail Invoice' else 'Tax Invoice' end) as Invoice_Type,case when len(isnull(Alternate_UOM ,''))>0 then Qty *Conversion_Factor else null end  as Alternet_Qty,"
            'Qry += " TSPL_SD_SALE_INVOICE_DETAIL .Alternate_UOM,    TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item_UOM + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then ''end as Scheme_Item_UOM  , TSPL_SD_SALE_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No else TSPL_SD_SALE_INVOICE_HEAD.GRNo end AS Dis_Doc_No, TSPL_SD_SHIPMENT_HEAD.Description ,TSPL_SD_SALE_INVOICE_HEAD.Transporter_Name , TSPL_COMPANY_MASTER .State as Comp_State,coalesce(TSPL_SD_SALE_INVOICE_HEAD.Road_Permit_No,TSPL_SD_SALE_INVOICE_HEAD.GRNo) as Dis_doc_no, TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_Date,103) as Buyer_order_date,case when (TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_SD_SHIPMENT_HEAD.Dispatch_Terms  end  as Terms_of_delivery,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date_Time_Invoice,convert(varchar ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as InvoiceDate "
            'Qry += " ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as ShipmentNo,TSPL_SD_SALE_INVOICE_DETAIL.Alt_Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Alt_UOM,convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as ShipmentDate,"
            'Qry += " TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code as DeliveryOrderNo,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as TermCondition,"
            'Qry += " TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ "
            'Qry += " Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone,"
            'Qry += " TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,"

            'Qry += "  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode,"
            'Qry += " TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'') "
            'Qry += "  as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3,"

            'Qry += "   case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No    end as P_CSTNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Lst_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name,"
            'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name,"
            'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name,"


            'Qry += " case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name, "


            'Qry += " TSPL_SD_SALE_INVOICE_DETAIL.item_code + case when Scheme_Item='Y' then ' (Free Scheme)' when Scheme_Item='N' then '' end as item_code,TSPL_ITEM_MASTER.Item_Desc + case when Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when Scheme_Item='N' then '' end   as itemdesc,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty"
            'Qry += " ,TSPL_SD_SALE_INVOICE_DETAIL.mrp,TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM as RATE_UOM"
            'Qry += " ,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt,"
            'Qry += "   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name,"
            'Qry += "   isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name,"
            'Qry += "   isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name,"
            'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,"
            'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,"
            'Qry += " isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name,"
            'Qry += " isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,"
            'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name,"
            'Qry += "  isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,TSPL_SD_SALE_INVOICE_HEAD.TAX1_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate ,TSPL_SD_SALE_INVOICE_HEAD.TAX3_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX4_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX5_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX6_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX7_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX8_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX9_Rate,TSPL_SD_SALE_INVOICE_HEAD.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per, isnull(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Total_Amt "
            'Qry += "     from TSPL_SD_SALE_INVOICE_HEAD"
            'Qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
            'Qry += " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No"
            'Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
            'Qry += "  left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code "
            'Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code    LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code "
            'Qry += " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE  "
            'Qry += " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'"
            'Qry += "  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  "
            'Qry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            'Qry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1"
            'Qry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  "
            'Qry += "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  "
            'Qry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  "
            'Qry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  "
            'Qry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  "
            'Qry += "  left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7 "
            'Qry += "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8 "
            'Qry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 "
            'Qry += "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10  "
            'Qry += "left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_SD_SALE_INVOICE_DETAIL.Unit_code "
            'Qry += "  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code "
            'Qry += " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code "
            'Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
            'Qry += "  left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_CITY_MASTER.City_Code "
            'Qry += " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_STATE_MASTER.STATE_CODE "
            'Qry += " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads"
            Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code ='" + strDocNo + "'"
            'Qry += " where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' order by TSPL_SD_SALE_INVOICE_DETAIL.line_no"
            Qry = "Select * from (" & Qry & ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim Qry2 As String = "select TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE  as InvoiceNo,Abatement_Amt,TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc + '( MRP : ' +   convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.MRP) + '  Abatement : ' + convert(varchar,convert(int,100- TSPL_SD_SALE_INVOICE_DETAIL.Abatement_Per)) + '%)'  as Item_Desc ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate,TSPL_ITEM_MASTER.Cheapter_Heads  ,tax1.Tax_Code_Desc as tax1name,tax2.Tax_Code_Desc as tax2name,tax3.Tax_Code_Desc as tax3name   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_DETAIL.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_DETAIL.tax2  left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code= TSPL_SD_SALE_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX9   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_DETAIL .TAX10  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE       "
            Qry2 += " where TSPL_SD_SALE_INVOICE_HEAD.Document_Code ='" + strDocNo + "'"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)

            Dim InvoiceType As String = clsDBFuncationality.getSingleValue(" select TSPL_SD_SALE_INVOICE_head.Invoice_Type from TSPL_SD_SALE_INVOICE_head where TSPL_SD_SALE_INVOICE_HEAD.Document_Code ='" + strDocNo + "'")
            If InvoiceType = "E" Then
                If dt.Rows.Count > 0 Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptProductExciseTransferSaleInvoice_Challan", "Challan", "rptSubReportExciseTransferSaleInvoice.rpt")
                End If
            Else
                If dt.Rows.Count > 0 Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptProductSaleInvoice_Challan", "Challan", clsCommon.myCDate(dt.Rows(0)("InvoiceDate")), "rptCompanyAddress.rpt")
                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        frmCRV = Nothing
    End Sub

    Function GetTaxRateTypeDT(ByVal DocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = ""
        qry = " select distinct * from (" & _
              " select distinct TAX1 as Tax_RateType_Name,TAX1_Rate as Tax_RateType_Rate,sum(TAX1_Amt) as Tax_RateType_Amount  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX1,TAX1_Rate " & _
              " union all " & _
              " select distinct TAX2,TAX2_Rate,sum(TAX2_Amt) as TAX2_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX2,TAX2_Rate " & _
              " union all " & _
              " select distinct TAX3,TAX3_Rate,sum(TAX3_Amt) as TAX3_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX3,TAX3_Rate " & _
              " union all " & _
              " select distinct TAX4,TAX4_Rate,sum(TAX4_Amt) as TAX4_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX4,TAX4_Rate " & _
              " union all " & _
              " select distinct TAX5,TAX5_Rate,sum(TAX5_Amt) as TAX5_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX5,TAX5_Rate " & _
              " union all " & _
              " select distinct TAX6,TAX6_Rate,sum(TAX6_Amt) as TAX6_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX6,TAX6_Rate " & _
              " union all " & _
              " select distinct TAX7,TAX7_Rate,sum(TAX7_Amt) as TAX7_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX7,TAX7_Rate " & _
              " union all " & _
              " select distinct TAX8,TAX8_Rate,sum(TAX8_Amt) as TAX8_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX8,TAX8_Rate " & _
              " union all " & _
              " select distinct TAX9,TAX9_Rate,sum(TAX9_Amt) as TAX9_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX9,TAX9_Rate " & _
              " union all " & _
              " select distinct TAX10,TAX10_Rate,sum(TAX10_Amt) as TAX1_Amt  from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" & DocNo & "' group by TAX10,TAX10_Rate " & _
              " ) as tax where Tax_RateType_Name is not null and Tax_RateType_Amount>0"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function
    Function GetColumnsForTaxRateType(ByVal dt As DataTable)
        Dim cols As String = ""
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                'If (dt.Rows.IndexOf(dr) + 1) = dt.Rows.Count Then
                '    cols = cols & "'" & dr.Item("Tax_RateType_Name") & "'" & " as Tax_RateType_Name" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Rate") & " as Tax_RateType_Rate" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Amount") & " as Tax_RateType_Amount" & (dt.Rows.IndexOf(dr) + 1)
                'Else
                '    cols = cols & "'" & dr.Item("Tax_RateType_Name") & "'" & " as Tax_RateType_Name" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Rate") & " as Tax_RateType_Rate" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Amount") & " as Tax_RateType_Amount" & (dt.Rows.IndexOf(dr) + 1) & ","
                'End If
                cols = cols & "'" & dr.Item("Tax_RateType_Name") & "'" & " as Tax_RateType_Name" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Rate") & " as Tax_RateType_Rate" & (dt.Rows.IndexOf(dr) + 1) & "," & dr.Item("Tax_RateType_Amount") & " as Tax_RateType_Amount" & (dt.Rows.IndexOf(dr) + 1) & ","
            Next
        End If
        For i As Integer = (dt.Rows.Count + 1) To 7
            If i = 7 Then
                cols = cols & "''" & " as Tax_RateType_Name" & (i) & "," & "null" & " as Tax_RateType_Rate" & (i) & "," & "null" & " as Tax_RateType_Amount" & (i)
            Else
                cols = cols & "''" & " as Tax_RateType_Name" & (i) & "," & "null" & " as Tax_RateType_Rate" & (i) & "," & "null" & " as Tax_RateType_Amount" & (i) & ","
            End If
        Next

        If clsCommon.myLen(cols) > 0 Then
            Return "," & cols
        Else
            Return ""
        End If
    End Function


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
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + strShipFrm + "'   "
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
                If clsCommon.myLen(txtInvNoForSupplementary.Value) <= 0 Then
                    If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                        gv1.Columns(colExpiry).FormatString = "{0:d}"
                    ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0 Then
                            gv1.CurrentRow.Cells(colICode).ReadOnly = True
                            gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colICode).ReadOnly = False
                            gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                        End If
                    ElseIf e.Column Is gv1.Columns(colQty) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colQty).ReadOnly = False
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colQty).ReadOnly = True
                            gv1.CurrentRow.Cells(colFreeQty).ReadOnly = True
                        End If
                    ElseIf e.Column Is gv1.Columns(colRate) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                            If chkRateUserCustomer.ToggleState = ToggleState.Indeterminate Then
                                gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateDefaultSetting.Checked
                            Else
                                gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateUserCustomer.Checked
                            End If
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = True
                        End If
                    ElseIf e.Column Is gv1.Columns(colDisPer) Then
                        If chkRateUserCustomer.ToggleState = ToggleState.Indeterminate Then
                            gv1.CurrentRow.Cells(colDisPer).ReadOnly = Not chkRateDefaultSetting.Checked
                        Else
                            gv1.CurrentRow.Cells(colDisPer).ReadOnly = Not chkRateUserCustomer.Checked
                        End If
                    ElseIf e.Column Is gv1.Columns(colAmt) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1 Then
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                        End If
                    End If
                Else
                    If e.Column Is gv1.Columns(colRate) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colRate).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = True
                        End If
                    ElseIf e.Column Is gv1.Columns(colQty) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colQty).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colQty).ReadOnly = True
                        End If
                    ElseIf e.Column Is gv1.Columns(colAmt) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                        End If
                    Else
                        gv1.CurrentRow.Cells(e.Column.Name).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            Dim qry As String = "SELECT     TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location, TSPL_SD_SALE_INVOICE_HEAD.RMDA_No, TSPL_SD_SALE_INVOICE_HEAD.RMDA_Date,TSPL_SD_SALE_INVOICE_HEAD.Remarks,TSPL_SD_SALE_INVOICE_HEAD.Description, TSPL_SRN_DETAIL.Item_Code,   TSPL_SRN_DETAIL.Item_Desc, TSPL_SRN_DETAIL.Rejected_Qty, TSPL_SRN_DETAIL.Item_Cost,TSPL_SRN_DETAIL.Unit_code,TSPL_SRN_DETAIL.Rejected_Qty*TSPL_SRN_DETAIL.Item_Cost as Amount,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2  FROM         TSPL_SD_SALE_INVOICE_HEAD INNER JOIN    TSPL_SRN_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SRN_DETAIL.Document_Code LEFT OUTER JOIN     TSPL_COMPANY_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + txtDocNo.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptMRDA", "MRDA Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    Private Sub CalculateDiscountAmount()

        Dim discountrate As Decimal
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
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()

        'If chkDiscountOnAmt.IsChecked Then
        '    For Each gro As GridViewRowInfo In gv1.Rows
        '        gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
        '        If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
        '            dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colAmt).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
        '            gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
        '        Else
        '            gro.Cells(colHeadDiscamt).Value = 0
        '        End If
        '    Next
        'Else
        '    For Each gro As GridViewRowInfo In gv1.Rows
        '        gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
        '        If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
        '            gro.Cells(colHeaDDisPer).Value = Math.Round((discountrate), 2)
        '        End If
        '    Next
        'End If
    End Sub

    Sub calculateHeaderDiscountAmt()
        If (chkDiscountOnRate.IsChecked) OrElse (chkDiscountOnAmt.IsChecked) Then
            Dim dblTotAmt As Decimal = 0
            Dim dblTotAmtUsed As Decimal = 0
            For ii As Integer = 0 To gv1.RowCount - 1
                If chkDiscountOnRate.IsChecked Then
                    gv1.Rows(ii).Cells(colHeaDDisPer).Value = txtDiscPer.Value
                ElseIf chkDiscountOnAmt.IsChecked Then
                    Dim dblQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Dim dblRate As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                    Dim dblAmt As Decimal = (dblQty * dblRate) ''+ dblFAmt
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colIsMannualAmt).Value) = 0 Then
                        gv1.Rows(ii).Cells(colAmt).Value = Math.Round(dblAmt, 2)
                    Else
                        dblAmt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    End If
                    dblAmt = Math.Round(dblAmt, 2, MidpointRounding.ToEven)
                    dblTotAmt += dblAmt
                    gv1.Rows(ii).Cells(colHeaDDisPer).Value = 0
                    gv1.Rows(ii).Cells(colHeadDiscamt).Value = 0
                End If
            Next
            If chkDiscountOnAmt.IsChecked AndAlso txtDiscAmt.Value > 0 AndAlso dblTotAmt > 0 Then
                For ii As Integer = 0 To gv1.RowCount - 1
                    Dim dblQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Dim dblRate As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                    Dim dblAmt As Decimal = (dblQty * dblRate)
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colIsMannualAmt).Value) = 0 Then
                        gv1.Rows(ii).Cells(colAmt).Value = Math.Round(dblAmt, 2)
                    Else
                        dblAmt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    End If
                    dblAmt = Math.Round(dblAmt, 2, MidpointRounding.ToEven)
                    Dim part As Decimal = Math.Round(txtDiscAmt.Value * dblAmt / dblTotAmt, 2, MidpointRounding.ToEven)
                    dblTotAmtUsed += part
                    gv1.Rows(ii).Cells(colHeadDiscamt).Value = part
                Next
                Dim diffAmt As Decimal = Math.Round(txtDiscAmt.Value - dblTotAmtUsed, 2, MidpointRounding.ToEven)
                If Math.Abs(diffAmt) <> 0 Then
                    If gv1.Rows.Count > 0 Then
                        gv1.Rows(0).Cells(colHeadDiscamt).Value = gv1.Rows(0).Cells(colHeadDiscamt).Value + diffAmt
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            calculateHeaderDiscountAmt()
            Dim arrTaxableAuth As New List(Of String)
            Dim dblFAmt As Double = 0
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate) ''+ dblFAmt
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0 Then
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            Else
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            End If
            Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
            Dim dblHeadPerDisAmt As Double = 0
            If chkDiscountOnRate.IsChecked Then
                Dim dblHeadDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaDDisPer).Value)
                dblHeadPerDisAmt = (dblAmt * dblHeadDisPer) / 100
            Else
                dblHeadPerDisAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeadDiscamt).Value)
            End If
            Dim dblTotDiscAmt = dblHeadPerDisAmt + dblDisAmt
            Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt - dblHeadPerDisAmt

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
                        Dim IsTaxonBaseAmount As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsTaxOnBaseAmount + clsCommon.myCstr(ii)).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                            Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            ''richa 15 Sep 2020 changes according to tax
                            If Not IsTaxonBaseAmount Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            End If

                            If Not IsTaxonBaseAmount AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                Dim dblTotalBasicPrice As Double = 0
                                For n As Integer = 0 To gv1.Rows.Count - 1
                                    If clsCommon.myLen(gv1.Rows(n).Cells(colICode).Value) > 0 Then
                                        dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colTotalBasicAmount).Value)
                                    End If
                                Next
                                dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
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
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + Strii)).Value = Nothing
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
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt

            gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            If chkDiscountOnRate.IsChecked Then
                gv1.Rows(IntRowNo).Cells(colHeadDisPerAmt).Value = Math.Round(dblHeadPerDisAmt, 2)
            Else
                gv1.Rows(IntRowNo).Cells(colHeadDisPerAmt).Value = 0
            End If


            gv1.Rows(IntRowNo).Cells(colTotalDiscountAmount).Value = Math.Round(dblTotDiscAmt, 2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            If clsCommon.CompairString(clsCommon.myCstr(e.RowElement.RowInfo.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(e.RowElement.RowInfo.Cells(colIsMannualAmt).Value) > 0 Then
                e.RowElement.ForeColor = Color.Blue
            Else
                e.RowElement.ForeColor = Color.Black
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
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
    Private Sub fndRouteNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating

        Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        txtRouteNo.Value = clsCommon.ShowSelectForm("ShipRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
        fndRouteNo_TextChanged()
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

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        If clsCommon.myLen(txtReqNo.Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, txtReqNo.Value)
        Else
            common.clsCommon.MyMessageBoxShow("No data found")
        End If

    End Sub

    Private Sub txtDept_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDept.Load

    End Sub

    Private Sub btnReverseAndUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsPSInvoiceHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    '----------------------Done By Monika 30/04/2014-------BM00000002443----------
#Region "SMS Email Setting"
    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsetting.Enabled = True
        Else
            btnsetting.Enabled = False
        End If

    End Sub

    Public Function GetAtachmntPrint(ByVal strDocNo As String)
        Dim qry1 As String = "select distinct TSPL_SD_SHIPMENT_DETAIL.Order_Code "
        qry1 += " from TSPL_SD_SALE_INVOICE_DETAIL "
        qry1 += "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code and TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
        qry1 += " where TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE='" + strDocNo + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
        Dim strSoNo As String = ""
        For Each dr As DataRow In dt1.Rows
            If clsCommon.myLen(strSoNo) > 0 Then
                strSoNo += ","
            End If
            strSoNo += clsCommon.myCstr(dr("Order_Code"))
        Next
        '' code for TaxRateType  done by Panch Raj
        Dim colsTaxRateType As String = GetColumnsForTaxRateType(GetTaxRateTypeDT(strDocNo))
        '' end 

        atchqry = "  select '" + strSoNo + "' as SONo, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate,TSPL_LOCATION_MASTER.Add1 as Location_Add1,TSPL_LOCATION_MASTER.Add2 as Location_Add2,TSPL_LOCATION_MASTER.Add3 as Location_Add3,TSPL_LOCATION_MASTER.TIN_No ,ISNULL(TSPL_LOCATION_MASTER.Phone1,'')+ Case When ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Location_Phone, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as shipName, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as shipName, TSPL_SHIP_TO_LOCATION.add1 as ship_Add1, TSPL_SHIP_TO_LOCATION.Add2 as ship_add2 ,TSPL_SHIP_TO_LOCATION.Add3 as ship_add3  ,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_CITY_MASTER.STATE_CODE  ,City_Name,"
        atchqry += "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SD_SALE_INVOICE_HEAD.Inv_No, TSPL_SD_SALE_INVOICE_HEAD.Dept_Desc , TSPL_SD_SALE_INVOICE_HEAD.Remarks ,  TSPL_SD_SALE_INVOICE_HEAD.Terms_Code,TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as Term_Desc,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as Vehicle_Desc , "
        atchqry += " TSPL_SD_SALE_INVOICE_DETAIL .Specification as  specification,   TSPL_SD_SALE_INVOICE_HEAD.Document_Code as DocNo , TSPL_SD_SALE_INVOICE_HEAD.Description, "
        atchqry += " TSPL_SD_SALE_INVOICE_HEAD.Description,convert(varchar, TSPL_SD_SALE_INVOICE_HEAD .Document_Date,103) as Document_Date , TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No, TSPL_SD_SALE_INVOICE_HEAD.Item_Type ,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, "
        atchqry += " TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.add2 as customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as customer_Add3 ,TSPL_CUSTOMER_MASTER.State as customer_city_State ,TSPL_CUSTOMER_MASTER.PIN_Code as Customer_Pin_Code,TSPL_CUSTOMER_MASTER.CST as Cust_CST_No,TSPL_CUSTOMER_MASTER.Tin_No as Cust_Tin_No,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Number , TSPL_SD_SALE_INVOICE_HEAD .Terms_Code as termscode ,TSPL_SD_SALE_INVOICE_HEAD .Ref_No as ref_no ,"
        atchqry += " TSPL_SD_SALE_INVOICE_HEAD .Comments as comments ,TSPL_SD_SALE_INVOICE_HEAD.Status ,TSPL_SD_SALE_INVOICE_HEAD.On_Hold ,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code ,TSPL_SD_SALE_INVOICE_HEAD.Due_Date ,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date ,TSPL_SD_SALE_INVOICE_HEAD.Carrier ,TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.GENo ,TSPL_SD_SALE_INVOICE_HEAD.GEDate ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code1 ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1 ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date ,TSPL_SD_SALE_INVOICE_HEAD.Inv_Date ,TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE ,TSPL_SD_SALE_INVOICE_HEAD.ConvRate ,TSPL_SD_SALE_INVOICE_HEAD.ApplicableFrom ,TSPL_SD_SALE_INVOICE_HEAD.Against_C_Form ,TSPL_SD_SALE_INVOICE_HEAD.CFormApplied ,TSPL_SD_SALE_INVOICE_HEAD.CFormRecd ,TSPL_SD_SALE_INVOICE_HEAD.PROJECT_ID ,TSPL_SD_SALE_INVOICE_HEAD.Price_code ,TSPL_SD_SALE_INVOICE_HEAD.Route_No ,TSPL_SD_SALE_INVOICE_HEAD.Route_Desc ,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Per ,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt ,TSPL_SD_SALE_INVOICE_HEAD.TotCashDiscAmt ,  TSPL_SD_SALE_INVOICE_HEAD .Discount_Amt as dis_amt,(case when Scheme_Item='Y' then (TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt+TSPL_SD_SALE_INVOICE_DETAIL.Amount) else (TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt) end) as dis_amt1,"
        atchqry += " TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_SD_SALE_INVOICE_HEAD .Total_Amt as Total_amount,"
        atchqry += " TSPL_SD_SALE_INVOICE_HEAD.Discount_Base as bfrdisc_amount,TSPL_LOCATION_MASTER.City_Code  as Location_City_Code,TSPL_LOCATION_MASTER.State as Location_State,TSPL_LOCATION_MASTER.Pin_Code as Location_Pin_Code,TSPL_LOCATION_MASTER.Country as Location_Country,TSPL_LOCATION_MASTER.Email as Location_Email,Location_Type ,Loc_Status ,Status_Date   as Location_Status_Date,TSPL_LOCATION_MASTER.Excisable as Location_Excisable,Loc_Segment_Code ,TSPL_LOCATION_MASTER.Type as Location_Type,Purchase_Tax_Group as Location_Purchase_Tax_Group,Sales_Tax_Group as Location_Sales_Tax_Group,Ecc_Number  as Location_Ecc_Number,Registration_Number as Location_Registration_Number ,Commissionerate as Location_Commissionerate ,Range_Code as Location_Range_Code ,Range_Name as Location_Range_Name ,Range_Address as Location_Range_Address,Division_Code as Location_Division_Code,Division_Name as Location_Division_Name,Division_Address as Location_Division_Address,TSPL_LOCATION_MASTER.TAN_No as Location_TAN_No,TSPL_LOCATION_MASTER.TCAN_No as Location_TCAN_No,Service_Tax_Reg_No as Location_Service_Tax_Reg_No,DutyPaid as Location_DutyPaid,Purchase_Tax_GroupIS as Location_Purchase_Tax_GroupIS,Sales_Tax_GroupIS as Location_Sales_Tax_GroupIS,Stock_Transfer_Filled_Ac as Location_Stock_Transfer_Filled_Ac,GIT_Location as Location_GIT_Location,GIT_Type as Location_GIT_Type,TSPL_LOCATION_MASTER.CST_No as Location_CST_No,TSPL_LOCATION_MASTER.Telphone as Location_PhoneNo,TSPL_LOCATION_MASTER.TAN_No as Location_FaxNo , "
        atchqry += " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt,  "
        atchqry += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,  "
        atchqry += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,  "
        atchqry += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,  "
        atchqry += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,  "
        atchqry += " tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,  "
        atchqry += " tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  "
        atchqry += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt,   "
        atchqry += " tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  "
        atchqry += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,  "
        atchqry += " isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Phone,TSPL_COMPANY_MASTER.Fax ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,"
        atchqry += " TSPL_SD_SALE_INVOICE_DETAIL.item_code as item_code, TSPL_ITEM_MASTER.Item_Desc   as itemdesc, TSPL_SD_SALE_INVOICE_DETAIL.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost,TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_HEAD.TAX1,TSPL_SD_SALE_INVOICE_HEAD.TAX2,TSPL_SD_SALE_INVOICE_HEAD.TAX3,TSPL_SD_SALE_INVOICE_HEAD.TAX4,TSPL_SD_SALE_INVOICE_HEAD.TAX5,TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge,TSPL_SD_SALE_INVOICE_DETAIL.Batch_No,cast(datepart(MONTH,TSPL_SD_SALE_INVOICE_DETAIL.MFG_Date) as varchar(2)) + '/' + "
        atchqry += " SUBSTRING(cast(datepart(YYYY,TSPL_SD_SALE_INVOICE_DETAIL.MFG_Date) as varchar(4)),3,2) as MFG_Date,"
        atchqry += " cast(datepart(MONTH,TSPL_SD_SALE_INVOICE_DETAIL.Expiry_Date) as varchar(2)) + '/' + "
        atchqry += " SUBSTRING(cast(datepart(YYYY,TSPL_SD_SALE_INVOICE_DETAIL.Expiry_Date) as varchar(4)),3,2) as Exp_Date,TSPL_SD_SALE_INVOICE_DETAIL.mrp,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_SD_SALE_INVOICE_DETAIL.Item_Weight,(case when COALESCE(TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type,'R')='T' then 'Tax Invoice' else 'Retail Invoice' end) as Invoice_Type " & colsTaxRateType & " from TSPL_SD_SALE_INVOICE_DETAIL  "
        atchqry += " left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code   "
        atchqry += " left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD .Ship_To_Location "
        atchqry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code "
        atchqry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code "
        atchqry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1  "
        atchqry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  "
        atchqry += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8  "
        atchqry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 "
        atchqry += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10     "
        atchqry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code  "
        atchqry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   "
        atchqry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
        atchqry += " left join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Terms_Code=TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code "
        atchqry += " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id"
        atchqry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  where 2=2 "
        atchqry += "  and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "'"

        SetItemWiseTax(clsDBFuncationality.GetDataTable(atchqry), txtDocNo.Value)

        Return atchqry
    End Function

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmSaleInvoiceProductSale
        frm.ShowDialog()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select First Sale Invoice No. For Mailing", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            Return
        End If

        atchqry = GetAtachmntPrint(txtDocNo.Value)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            System.Diagnostics.Process.Start(frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSaleInvoice", "Sales Invoice"))
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnsend_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            SendSMSandEmail(lstUsers, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '----------------------Done By Preeti 29/05/2014-------BM00000002659----------

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim frm As New FrmSaleHistory
        frm.strFormId = MyBase.Form_ID
        frm.strCustId = txtVendorNo.Value
        frm.strCustName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnDeliveredTo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeliveredTo.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strDatabaseName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 DataBase_Name from TSPL_COMPANY_MASTER  where Cust_Code='" & txtVendorNo.Value & "'", trans))
            If clsCommon.myLen(strDatabaseName) = 0 Then
                Throw New Exception("Please set Customer for child company ")
            End If
            If common.clsCommon.MyMessageBoxShow("Are you sure to deliver Shipment ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim qry = "Update TSPL_SD_SALE_INVOICE_HEAD set Is_Delivered=1  where Document_Code='" + txtDocNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "Update " + strDatabaseName + ".dbo.TSPL_PURCHASE_ORDER_HEAD set SaleInvoiceNo='" & txtDocNo.Value & "' where PurchaseOrder_No='" & txtPONo.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommon.MyMessageBoxShow("Data Delivered successfully")


            End If
            trans.Commit()
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSendEmailSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmailSMS.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Invoice No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtVendorNo.Value)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btn_printNormal_Click(sender As Object, e As EventArgs) Handles btn_printNormal.Click
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowToPrintInvoiceAfterPosting & "'")) = 1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct status from tspl_sd_sale_invoice_head where document_code in ('" & txtDocNo.Value & "')")) = 0 Then
                frmCRV.ShowCystalReportToolbar = False
            End If
        End If
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue("Invoice not found to Print")
        Else
            If clsCommon.CompairString(Me.Form_ID, "INVOICE-DS") = CompairStringResult.Equal Then
                Dim IsTaxable As Double = 0
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                Dim StrSql = "Select Document_Date,Customer_Code,Bill_To_Location,is_taxable,Tax_Group from TSPL_SD_SALE_INVOICE_HEAD where Document_Code='" & txtDocNo.Value & "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(StrSql)
                If dt1.Rows.Count > 0 Then
                    IsTaxable = clsCommon.myCdbl(dt1.Rows(0)("is_taxable"))
                    dtDocdate = clsCommon.myCDate(dt1.Rows(0)("Document_Date"))
                End If
                If IsTaxable = 1 Then
                    'Dim objInvoice As New frmSaleInvoiceProductSale
                    'objInvoice.funPrint(txtDocNo.Value)
                    funPrint(txtDocNo.Value)
                Else
                    Dim Qry As String = Nothing
                    Dim dt As DataTable = Nothing
                    Dim CreateFreshInvoiceOnDispatchSave As Integer = 0
                    Dim objMultPrintInvoice As New FrmPrintFreshInvoice
                    CreateFreshInvoiceOnDispatchSave = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateFreshInvoiceOnDispatchSave, clsFixedParameterCode.CreateFreshInvoiceOnDispatchSave, Nothing))
                    If AllowSeperateSchemeItemOnPrint Then
                        GetSeperateSchemeItemPrintQry(txtDocNo.Value)
                    Else
                        If CreateFreshInvoiceOnDispatchSave = 0 Then
                            Qry = objMultPrintInvoice.LoadPrintQuery(txtReqNo.Value)
                        Else
                            Qry = objMultPrintInvoice.LoadPrintQuery(txtDocNo.Value)
                        End If
                        dt = clsDBFuncationality.GetDataTable(Qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If ShowShipToPartyInDairyDispatch = 1 OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                ''richa agarwal 23 Nov,2018  ERO/30/11/18-000424 30 Nov,2018 richa 
                                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                                    Dim dtCustomerOutstanding As DataTable = Nothing
                                    dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & clsCommon.myCstr(dt.Rows(0)("Cust_code")) & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Document_date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Document_date")), "dd/MMM/yyyy"))
                                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                                Else
                                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                                End If
                            Else
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice(New)", "Bill of Supply", dtDocdate, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            End If
                        End If
                    End If
                End If
            Else
                funPrint(txtDocNo.Value)
            End If
        End If
        frmCRV = Nothing
    End Sub
    Public Sub GetSeperateSchemeItemPrintQry(StrDocNo As String)
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowToPrintInvoiceAfterPosting & "'")) = 1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct status from tspl_sd_sale_invoice_head where document_code in ('" & StrDocNo & "')")) = 0 Then
                frmCRV.ShowCystalReportToolbar = False
            End If
        End If
        '========UPDATE BY PREETI GUPTA AGAINST TICKET NO[BHA/25/09/18-000566]
        Dim qry As String = Nothing
        qry = "select Main_Final.*,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress from ( select DISTINCT final.*,tbl_Brand.Brand,tbl_Brand.BRANDDESC from (select tspl_ship_to_location.Ship_To_Desc,tspl_ship_to_location.add1 as Ship_Add1,tspl_ship_to_location.add2 as Ship_Add2,tspl_ship_to_location.Add3 as Ship_to_Add3,tspl_ship_to_location.add4 as Ship_Add4,tspl_ship_to_location.ship_to_code,case when ISNULL(TSPL_SHIP_TO_LOCATION.Telphone,'')='(+__)__________' then '' else TSPL_SHIP_TO_LOCATION.Telphone end   as Ship_to_Phn ,SHip_to_State.state_code  as Ship_To_State_Code, SHip_to_State.state_Name as Ship_To_State_Name,SHip_to_State.gst_state_code as  Ship_To_GSt_Sate_Code,TSPL_SHIP_TO_LOCATION.pin_code as Ship_PIN_Code,tspl_ship_to_location.gstNo as Ship_to_GSTNO," & _
 " TSPL_SHIP_TO_LOCATION.PAN as Ship_To_PAN,case when coalesce(InLtr.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InLtr.Conversion_factor,1)) end as QtyInLtr," & _
  " case when coalesce(InCrate.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2),  TSPL_SD_sale_invoice_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(InCrate.Conversion_factor,1)) end as QtyInCrate,TSPL_SD_SHIPMENT_HEAD.CRATE,TSPL_SD_SHIPMENT_HEAD.Box ,TSPL_SD_SHIPMENT_HEAD.jaali, case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='SCM' then " & _
              " isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as SCM,  case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='DIS-MARGIN' then " & _
              " isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as DIS_MARGIN,  TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Loc_Short_Name ,  TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin, (case when isnull(TSPL_LOCATION_MASTER.Phone1,'')<>'' then TSPL_LOCATION_MASTER.Phone1  when  isnull(TSPL_LOCATION_MASTER.Phone2,'')<>'' then + ', '+ TSPL_LOCATION_MASTER.Phone2 end) as Loc_Phone , TSPL_LOCATION_MASTER.Email as Loc_Eamil,'' as Loc_Website, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No,convert(varchar(12),TSPL_SD_SALE_INVOICE_HEAD.Document_date,103) as Invoice_Date," & _
              " TSPL_DELIVERY_NOTE_master_FRESHSALE.Document_No as DO_No,convert(varchar(12),TSPL_DELIVERY_NOTE_master_FRESHSALE.Document_Date,103) as Do_Date, customer_city_master.city_name as Cust_City,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No ,CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode, TSPL_SD_SALE_INVOICE_HEAD.Electronic_Ref_No, Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,tspl_location_master.gstno as LocGstNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillNo,TSPL_SD_SALE_INVOICE_HEAD.EWayBillDate,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_HEAD.Remarks as InvRemarks,TSPL_SD_sale_invoice_DETAIL.Delivery_Code ,ITEMDETAIL.Conversion_factor, case when TSPL_SD_sale_invoice_DETAIL.Qty > 0 then case when coalesce(ITEMDETAIL.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2), TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(ITEMDETAIL.Conversion_factor,1)) end else 0 end as QTY_Box, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Sale_Invoice_No, Case When ISNULL(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')<>'' Then TSPL_SD_SHIPMENT_HEAD.ManualVehicle WHEN ISNULL(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' Then TSPL_VEHICLE_MASTER.Number Else TSPL_SD_SALE_INVOICE_HEAD.vehicleNo End as vehicleNo," & _
              " Convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date, TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount, TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO, TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date , TSPL_SD_SALE_INVOICE_HEAD.Lorry_No,TSPL_ITEM_MASTER.Sku_Seq ,TSPL_SD_sale_invoice_DETAIL.Item_Code,TSPL_SD_sale_invoice_DETAIL.Line_No,TSPL_ITEM_MASTER.Item_Desc as Particulars,TSPL_SD_sale_invoice_DETAIL.Scheme_Item,TSPL_SD_sale_invoice_DETAIL.Qty as UOM_QTY," & IIf(clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal, "TSPL_SD_sale_invoice_DETAIL.Item_net_Amt as Total_Amt", "TSPL_SD_sale_invoice_DETAIL.Item_net_Amt") & ",TSPL_SD_sale_invoice_DETAIL.Amount, TSPL_SD_sale_invoice_DETAIL.Crate as QtyCrates ,TSPL_SD_sale_invoice_DETAIL.Unit_code , convert(Decimal(18,2), TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as QtyPCS , coalesce( case when TSPL_SD_sale_invoice_DETAIL.Sampling=1 then 0 else TSPL_SD_sale_invoice_DETAIL.QTY end,0) as free_qty," & _
              " TSPL_SD_sale_invoice_DETAIL.Scheme_Item as FreeSchemeInLitres , case when TSPL_SD_sale_invoice_DETAIL.Qty > 0 then convert(DECIMAL(18,5),(case when TSPL_SD_sale_invoice_DETAIL.Sampling=1then 0 else  TSPL_SD_sale_invoice_DETAIL.Amount end)/ (TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) else 0 end as RatePerPcs, (case when TSPL_SD_sale_invoice_DETAIL.Sampling=1then 0 else  TSPL_SD_sale_invoice_DETAIL.Amount end)  as valueInRs, coalesce(TSPL_SD_sale_invoice_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount,isnull(TSPL_SD_sale_invoice_DETAIL.Crate,0)as schemeInCrates, '' GrandTotalCrates ," & _
              " TSPL_SD_SALE_INVOICE_DETAIL.ITEM_COST , TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo , TSPL_SD_SALE_INVOICE_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone ,TSPL_CUSTOMER_MASTER.Fax as cust_fax ,TSPL_CUSTOMER_MASTER.State as Cust_state,CUSTOMER_STATE_MASTER.STATE_NAME as cust_Statename,TSPL_CUSTOMER_MASTER.Email as  cust_Email,TSPL_CUSTOMER_MASTER.WebSite as cust_website,TSPL_CUSTOMER_MASTER.pan as Customer_Pan ,isnull(TSPL_SD_SHIPMENT_HEAD.discount_amt,0)+isnull(TSPL_SD_SHIPMENT_HEAD.headDisc_Amt,0)+isnull(TSPL_SD_SHIPMENT_HEAD.HeadDisc_perAmt,0) as HeadTotalDisc " & _
              " from TSPL_SD_sale_invoice_DETAIL  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " & _
              " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code and  TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_sale_invoice_DETAIL.Unit_code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_sale_invoice_DETAIL.Item_Code" & _
              " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='BOX') as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code " & _
              " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SALE_INVOICE_HEAD .Customer_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE  left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code " & _
              " LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_SD_sale_invoice_DETAIL.Price_code  and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_SD_sale_invoice_DETAIL.Location  and TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left outer join TSPL_DELIVERY_NOTE_master_FRESHSALE on TSPL_DELIVERY_NOTE_master_FRESHSALE.Document_No=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code " & _
              " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location " & _
              " left join TSpl_state_master SHip_to_State on  SHip_to_State.state_code=tspl_ship_to_location.state" & _
              " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.LTR_TYPE='Y') as InLtr on InLtr.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code " & _
              " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL 	  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.UNIT_CODE= TSPL_ITEM_UOM_DETAIL.UOM_CODE where TSPL_UNIT_MaSTER.Crate_type='Y') as InCrate on InCrate.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code " & _
              " where 2=2 and TSPL_SD_SALE_INVOICE_HEAD.Document_Code in   ('" + StrDocNo + "') " & _
              " ) AS FINAL left outer join  ( select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND],max([DESCRP]) as [DESCRP],max([PACK]) as [PACK], max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA],max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW], max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC]) as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC], max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC],max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC], max([SCRAPDESC]) as [SCRAPDESC]  from ( select * from (   select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   , TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER    left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values   where 2=2 )xx  Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE],[CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP])  ) Pivt   Pivot  ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC], [BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC],[CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC])  ) Pivt1 ) xxx  group by Item_Code )  as tbl_Brand on tbl_Brand.Item_Code=final.item_Code  ) AS Main_Final left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.comp_code=Main_Final.comp_code  order  by Main_Final.Sku_Seq ,Main_Final.Line_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '' BHA/22/05/18-000030 richa agarwal 14 june,2018
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                ' done by priti BHA/17/08/18-000452 for bharat to correct customer dashboard problem
                qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & StrDocNo & "')"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim CustomerCode As String = ""
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                    Next
                    If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                        CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                    End If
                End If

                Dim dtCustomerOutstanding As DataTable = Nothing
                If dt.Rows(0)("Do_Date") IsNot DBNull.Value Then
                    dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Do_Date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Do_Date")), "dd/MMM/yyyy"))
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptSaleInvoiceDairyWithCustomerOutstanding", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Do_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                Else
                    dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Invoice_Date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Invoice_Date")), "dd/MMM/yyyy"))
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptSaleInvoiceDairyWithCustomerOutstanding", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstanding.rpt", dtCustomerOutstanding)
                End If
            Else
                If dt.Rows(0)("Do_Date") IsNot DBNull.Value Then
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                        ' done by richa agarwal 23 Nov,2018 add customer dashbord into existing report for Erode client ERO/01/04/19-000534
                        qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & StrDocNo & "')"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        Dim CustomerCode As String = ""
                        If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                            For Each dr As DataRow In dt2.Rows
                                CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                            Next
                            If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                            End If
                        End If
                        Dim dtCustomerOutstanding As DataTable = Nothing
                        dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Do_Date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Do_Date")), "dd/MMM/yyyy"))
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Do_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                    Else
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice_ForSeperateScheme", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Do_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                    End If

                    ''--

                Else
                    ''richa 
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal Then
                        ' done by richa agarwal 23 Nov,2018 add customer dashbord into existing report for Erode client ERO/01/04/19-000534
                        qry = "select distinct customer_code from tspl_sd_sale_invoice_head where document_code in ('" & StrDocNo & "')"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                        Dim CustomerCode As String = ""
                        If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                            For Each dr As DataRow In dt2.Rows
                                CustomerCode = CustomerCode + "','" + clsCommon.myCstr(dr("customer_code"))
                            Next
                            If clsCommon.myLen(CustomerCode) > 0 AndAlso clsCommon.myCstr(CustomerCode).Substring(0, 3) = "','" Then
                                CustomerCode = CustomerCode.Substring(3, CustomerCode.Length - 3)

                            End If
                        End If
                        Dim dtCustomerOutstanding As DataTable = Nothing
                        dtCustomerOutstanding = clsCustomerMaster.getCustomerOutstandingOfAmt_Can_Crate("'" & CustomerCode & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Invoice_Date")).AddDays(-1), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("Invoice_Date")), "dd/MMM/yyyy"))
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoiceParty", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader(), "rptCustomerOutstandingErode.rpt", dtCustomerOutstanding)
                    Else
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFreshSaleInvoice_ForSeperateScheme", "Bill of Supply", clsCommon.myCDate(dt.Rows(0)("Invoice_Date")), "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                    End If

                    ''--

                End If
            End If
        End If
        frmCRV = Nothing
    End Sub

    Private Sub btn_Depo_Print_Click(sender As Object, e As EventArgs) Handles btn_Depo_Print.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue("Invoice not found to Print")
        Else
            funPrint(txtDocNo.Value, False, "DepoPrint")
        End If
    End Sub


    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating, txtInvNoForSupplementary._MYValidating
        Try
            '-------richa 30/07/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            '-----------------------------------------------------
            'Ticket No-ALF/18/09/18-000082 show cancel status
            'Ticket No- KDI/28/09/18-000434 Column ambigious error resolved
            Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Code,CONVERT(varchar(10), TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)+' '+ CONVERT(varchar(5), TSPL_SD_SALE_INVOICE_HEAD.Document_Date,114) as Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SALE_INVOICE_HEAD.Comments,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as Amount,Invoice_No_For_Supplementary as [Supplementary Invoice No], "
            If ShowDocumentCancel = True Then
                qry += " case when TSPL_SD_SALE_INVOICE_HEAD.CancelFlag=1 then 'Cancel' else case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end end as [Status],Against_Shipment_No as [Shipment No],case when isnull(TSPL_SD_SALE_RETURN_HEAD.is_cancelled,0) = 0 then 'No' else 'Yes' end as [Is Cancelled] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.document_code "
            Else
                qry += " case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Against_Shipment_No as [Shipment No],case when isnull(TSPL_SD_SALE_RETURN_HEAD.is_cancelled,0) = 0 then 'No' else 'Yes' end as [Is Cancelled] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.document_code "
            End If

            Dim whrClas As String = " 2=2 "
            'If clsCommon.myLen(txtVendorNo.Value) > 0 Then
            '    whrClas += " AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ='" + txtVendorNo.Value + "'"
            'End If
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type in ('T','R','E','A','I','N') and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "  'and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'

                If IsDairyModule = False Then
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' "
                Else
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' "
                End If
            ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type in ('T','R','E','A','I','N')  " 'and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'

                If IsDairyModule = False Then
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' "
                Else
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' "
                End If
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type in ('T','R','E','A','I','N')  " 'and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'

                If IsDairyModule = False Then
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' "
                Else
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' "
                End If
            Else
                whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type in ('T','R','E','A','I','N') " 'and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'

                If IsDairyModule = False Then
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='' "
                Else
                    whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' "
                End If
            End If
            Dim controlName As String = DirectCast(sender, Control).Name

            '--For supplementary Posted document should pick.
            If clsCommon.CompairString(controlName, txtInvNoForSupplementary.Name) = CompairStringResult.Equal Then
                If Not clsERPFuncationality.GetGSTStatus(clsCommon.GETSERVERDATE()) Then
                    Throw New Exception("GST Should be application For supplementary invoice.")
                End If
                whrClas += " and len(isnull(Invoice_No_For_Supplementary,''))=0 and TSPL_SD_SALE_INVOICE_HEAD.Status=1 and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(objCommonVar.GSTApplicableDate), "dd/MMM/yyyy hh:mm:ss tt") + "'"
            End If
            '-----------------------------------------------------
            LoadData(clsCommon.ShowSelectForm("ShipmentCofndInvoice", qry, "Code", whrClas, txtDocNo.Value, "TSPL_SD_SALE_INVOICE_HEAD.Document_Date desc", isButtonClicked, "TSPL_SD_SALE_INVOICE_HEAD.Document_Date"), NavigatorType.Current)

            Try
                isInsideLoadData = True
                If clsCommon.CompairString(controlName, txtInvNoForSupplementary.Name) = CompairStringResult.Equal Then
                    isNewEntry = True
                    btnSave.Text = "Save"
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    SaleInvoiceDate = txtDate.Value
                    txtDate.Value = clsCommon.GETSERVERDATE()
                    txtInvNoForSupplementary.Value = txtDocNo.Value
                    txtDocNo.Value = ""
                    txttcstaxbaseamount.Value = 0
                    For ii As Integer = gv1.RowCount - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal Then
                            gv1.Rows(ii).Delete()
                            Continue For
                        End If
                        gv1.Rows(ii).Cells(colCashSchemeCode).Value = ""
                        gv1.Rows(ii).Cells(colCashSchemeType).Value = ""
                        gv1.Rows(ii).Cells(colCash_Pers).Value = 0
                        gv1.Rows(ii).Cells(colCash_Amt).Value = 0
                        gv1.Rows(ii).Cells(colDisPer).Value = 0
                        gv1.Rows(ii).Cells(colHeaDDisPer).Value = 0
                    Next
                    For ii As Integer = 0 To gv1.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells(colAmt).Value = 0
                        End If
                        gv1.Rows(ii).Cells(colRate).Value = 0
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Finally
                isInsideLoadData = False
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txttcstaxbaseamount_TextChanged(sender As Object, e As EventArgs) Handles txttcstaxbaseamount.TextChanged
        Try
            If AllowtoChangeTCSBaseAmount Then
                ' SetTaxDetails()
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()

            Else
                txttcstaxbaseamount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function GetQuery() As String
        Throw New NotImplementedException
    End Function
    '' created by Richa Agarwal against ticket No-ERO/09/09/19-001022  on date 09-09-2019
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        CancelData()
    End Sub
    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Return False
            End If
            Dim strSaleReturnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Code from TSPL_SD_SALE_RETURN_HEAD where Against_Invoice_No='" & txtDocNo.Value & "' "))
            If clsCommon.myLen(strSaleReturnNo) > 0 Then
                Throw New Exception("You cannot cancelled this document because its Sale Return (" + clsCommon.myCstr(strSaleReturnNo) + ") has been created.")
            End If

            Dim strDairyGAtePassCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select gpcode from TSPL_SD_SHIPMENT_HEAD where document_code='" & txtReqNo.Value & "' "))
            If clsCommon.myLen(strDairyGAtePassCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because Dairy GAte Pass (" + clsCommon.myCstr(strDairyGAtePassCount) + ") has been created against its Dispatch.")
            End If

            Dim strReceiptCount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select receipt_no from TSPL_RECEIPT_DETAIL where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where against_Sale_no='" & txtDocNo.Value & "') "))
            If clsCommon.myLen(strReceiptCount) > 0 Then
                Throw New Exception("You cannot cancelled this document because receiving (" + clsCommon.myCstr(strReceiptCount) + ") has been done against its AR Invoice.")
            End If

            If FlagDocumentIsTaxable = 1 AndAlso clsERPFuncationality.GetEInvoiceStatus(txtDate.Value) = True AndAlso clsCommon.CompairString(EInvoiceType, "BB") = CompairStringResult.Equal Then
                Dim EInvoiceCancelTimeValid As Int64 = 0
                EInvoiceCancelTimeValid = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select  isnull (DATEDIFF(hour,EInvoice_Posting_Date,GETDATE()),0) as PostedHours from tspl_sd_sale_invoice_head where  document_code = '" + txtDocNo.Value + "'"))
                If EInvoiceCancelTimeValid >= 24 Then
                    Throw New Exception("Invoice can not be cancelled.It has been more than 24 hours.")
                End If
            End If

            clsPSShipmentHead.CancelData(Me.Form_ID, txtReqNo.Value, txtDocNo.Value, NavigatorType.Current)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    ''richa 14 Dec,2020
    Private Sub calculateTCSAmount()
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                For ii As Integer = 0 To gv2.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAutCode).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCommon.myCstr(txtInvNoForSupplementary.Value)) > 0 AndAlso SaleInvoiceDate.Month() <> txtDate.Value.Month() AndAlso SaleInvoiceDate.Year() = txtDate.Value.Year() Then
                            If gv1.RowCount > 0 Then
                                For m As Integer = 0 To gv1.Rows.Count - 1
                                    Dim strII As String = clsCommon.myCstr(ii + 1)
                                    gv1.Rows(m).Cells("COLTAXRATE" + strII).Value = 0
                                    UpdateCurrentRow(m)
                                Next

                                UpdateAllTotals()
                            End If
                        End If
                    End If

                Next


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
