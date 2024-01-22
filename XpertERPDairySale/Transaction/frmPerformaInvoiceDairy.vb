'' Print Related work done against ticket No. ALF/04/05/18-000059
'' work done on transaction level agaist ticket no. ALF/14/05/18-000063,ALF/16/05/18-000065
'' work done on transaction and report for against ticket no . ALF/29/05/18-000069 
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports XpertERPEngine
Public Class frmPerformaInvoiceDairy
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim blnReverse As Boolean = False
    Dim strMessages As String = Nothing
    Const colURoundOff As String = "colURoundOff"
    Const col_Is_Taxable As String = "col_Is_Taxable"
    Const colTransType As String = "colTransType"
    Const colcustomer As String = "colcustomer"
    Const colUDocCode As String = "colUDocCode"
    Const colLinenno As String = "colLinenno"
    Const colUDocDate As String = "colUDocDate"
    Const colUDesc As String = "colUDesc"
    Const colUDocAmount As String = "colUDocAmount"
    Const colUTaxGroup1 As String = "colUTaxGroup1"
    Const colUTTaxAutCode1 As String = "colUTTaxAutCode1"
    Const colUTBaseAmt1 As String = "colUTBaseAmt1"
    Const colUTTaxRate1 As String = "colUTTaxRate1"
    Const colUTTaxAmt1 As String = "colUTTaxAmt1"
    Const colheadTaxAmt1 As String = "colheadTaxAmt1"
    Const colUTTaxAutCode2 As String = "colUTTaxAutCode2"
    Const colUTBaseAmt2 As String = "colUTBaseAmt2"
    Const colUTTaxRate2 As String = "colUTTaxRate2"
    Const colUTTaxAmt2 As String = "colUTTaxAmt2"
    Const colUTTaxAutCode3 As String = "colUTTaxAutCode3"
    Const colUTBaseAmt3 As String = "colUTBaseAmt3"
    Const colUTTaxRate3 As String = "colUTTaxRate3"
    Const colUTTaxAmt3 As String = "colUTTaxAmt3"
    Const colUTTaxAutCode4 As String = "colUTTaxAutCode4"
    Const colUTBaseAmt4 As String = "colUTBaseAmt4"
    Const colUTTaxRate4 As String = "colUTTaxRate4"
    Const colUTTaxAmt4 As String = "colUTTaxAmt4"
    Const colUDoc_Amt_WO_Disc As String = "colUDoc_Amt_WO_Disc"
    Const colUAmt_After_Disc As String = "colUAmt_After_Disc"
    Const colUTaxAmt As String = "colUTaxAmt"
    Const colitemcode As String = "colitemcode"
    Const colAbatementPers As String = "colAbatementPers"
    Const colAbatementAmt As String = "colAbatementAmt"
    Const colConversionFactor As String = "colConversionFactor"
    Const colItemType As String = "colItemType"
    Const colItemUOM As String = "colItemUOM"
    Const Quantity As String = "Quantity"
    Const colUValidateRemark As String = "colUValidateRemark"
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Dim AllowManualVehicleOnDairyDispatch As Boolean = False
    Dim ParentCode As String = ""
    Dim GSTStatus As Boolean = False
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colBatchNo As String = "BATCHNO"
    Dim SeparateDairyDispatchTaxableNonTaxable As Integer = 0
    Dim RunBatchFifowise As Integer = 0
    Dim ShowSchemeItemRate As Integer = 0
    Dim AutoCalculateCrate As Integer = 0
    Dim ShowShipToPartyInDairyDispatch As Integer = 0
    Const colCrate As String = "colCrate"
    Dim intDispatchfromDelivery As Integer = 0
    Dim strOrginalCust As String = Nothing
    Private StrSql As String
    Private blnChangeCustomer As Boolean = False
    Private AutoScheme As Boolean = False
    Private strVehicleCapacityUnit As String
    Private attachQry As String = ""
    Private blnBackCalculation As Boolean = False
    Private AllowChangeInvoiceType As Boolean = False
    Private IsBatchMFDEXDmandatory As Boolean = False
    Private PurchaseOneItemOneVendor As Boolean = False
    Private ItemRateEditable As Boolean = False
    Private ItemMRPEditable As Boolean = False
    Dim IsLocationProductType As Integer
    Public strExcise As Boolean
    Public intMRPwithabatement As Integer
    Private isPO_GRN_MRN_Editable As Boolean = False
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"
    Const Disc_Scheme_Amount As String = "Disc_Scheme_Amount"
    Const Disc_Scheme_Code As String = "Disc_Scheme_Code"
    Const Disc_Scheme_Pers As String = "Disc_Scheme_Pers"
    Const Disc_Scheme_Type As String = "Disc_Scheme_Type"
    Const colBooking_User_Code As String = "colBooking_User_Code"
    Const colDistributor_Retailer_Code As String = "colDistributor_Retailer_Code"
    Const colDistributor_Retailer_Name As String = "colDistributor_Retailer_Name"

    Const colUnitALter As String = "colUnitALter"
    Const colUnitRate As String = "colUnitRate"
    Const colOrgUnit As String = "COLORGUNIT"
    Const ReportID As String = "PSShipmentSNItemGrid"
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
    Const colRateUnitQty As String = "colRateUnitQty"
    Const colAlterUnitQty As String = "colAlterUnitQty"

    Const colTAX_PAID As String = "colTAX_PAID"
    Const colICodeGrp As String = "colICodeGrp"

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
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colOrderNo As String = "ORDERNO"
    Const colHeaDDisPer As String = "colHeaDDisPer"
    Const colHeadDisPerAmt As String = "colHeadDisPerAmt"


    Const colLocationCode As String = "LOCATIONCODE"
    Const colLocationName As String = "LOCATIONNAME"


    Const colMRP As String = "MRP"
    '' ''Const colAssessableRate As String = "ASSESSABLERATE"
    '' ''Const colAssessableAmount As String = "ASSESSABLEAMT"

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
    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"

    Const colItemWeightMetric As String = "colItemWeightMetric"
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
    Const ColCommParty As String = "ColCommParty"
    Const ColCommPartyName As String = "ColCommPartyName"
    Const colCommRate As String = "colCommRate"
    Const ColCommAmt As String = "ColCommAmt"
    Const ColAmtAfterCOmm As String = "ColAmtAfterCOmm"

    Public IsFormLoad As Boolean = False

    Dim atchqry As String = ""
    Public IsDataImported As Boolean = False
    Public gvExcel As New RadGridView
    Public row_index As Integer
    Public DtExcel As DataTable
    Dim Item_TaxType As Integer = 0
    Public ShowPrintDisAmt As Boolean = False

#End Region


    Private Sub frmPerformaInvoiceDairy_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False, Nothing)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData(Nothing)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
            'Add Tool tip Task No- TEC/18/05/18-000237
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "tspl_dairy_proforma_invoice_head " + Environment.NewLine + _
                                         "tspl_dairy_proforma_invoice_detail " + Environment.NewLine + _
                                         "TSPL_BATCH_ITEM ( If Item is batch type) " + Environment.NewLine + _
                                         "TSPL_SERIAL_ITEM ( If Item is Serial type)")
            'Add Tool tip Task No- TEC/18/05/18-000237
        End If
    End Sub

    Private Sub frmPerformaInvoiceDairy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsFormLoad = True
        txtDate.Value = clsCommon.GETSERVERDATE
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        SetUserMgmtNew()

        LoadBlankGrid()
        LoadBlankGridTax()
        LoadItemType()
        LoadBlankGridAC()
        LoadDispatchTerms()
        LoadDOItemType()
        AddNew()
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '    UcCustomFields1.Report_ID = MyBase.Form_ID
        '    UcCustomFields1.LoadCustomControls()
        'End If
        IsFormLoad = False

    End Sub
    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
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
    Sub AddNew()

        txtPrintDiscountAmt.Text = "0"
        If clsCommon.myCBool(ShowPrintDisAmt) = True Then
            txtPrintDiscountAmt.Visible = True
            lblPrintDisAmt.Visible = True
        Else
            txtPrintDiscountAmt.Visible = False
            lblPrintDisAmt.Visible = False
        End If


        rbtn_Ambient.Enabled = True
        rbtn_Fresh.Enabled = True

        txtManualCustomer.Text = ""
        BlankAllControls()
        LoadBlankGrid()
        LoadBlankGridAC()
        LoadBlankGridTax()
        LoadDOItemType()

        txtPaymentTerms.Text = ""

        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
     
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        txtVendorNo.Enabled = True
        txtSchemeTaxGroup.Visible = False
        txtComment.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        ''For Custom Fields
        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '    UcCustomFields1.SetDefaultValues()
        'End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))

        End If
        txtTaxGroup.Enabled = True
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
   
    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 2
        cboItemType.Visible = False
        RadLabel29.Visible = False
    End Sub
    Public Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
       
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

        Dim repoICodeGrp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICodeGrp.FormatString = ""
        repoICodeGrp.HeaderText = "Item Group"
        repoICodeGrp.Name = colICodeGrp
        repoICodeGrp.HeaderImage = My.Resources.search4
        repoICodeGrp.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICodeGrp.Width = 100
        repoICodeGrp.IsVisible = False
        repoICodeGrp.VisibleInColumnChooser = False
        repoICodeGrp.ReadOnly = True

        gv1.MasterTemplate.Columns.Add(repoICodeGrp)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = False
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
        repoPriceDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPriceDate)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCOde
        repoPriceCode.IsVisible = False
        repoPriceCode.ReadOnly = True
        repoPriceCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoBarcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBarcode.FormatString = ""
        repoBarcode.HeaderText = "BAR Code"
        repoBarcode.Name = colBarCode
        repoBarcode.IsVisible = False
        repoBarcode.ReadOnly = True
        repoBarcode.VisibleInColumnChooser = False
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
        repoUnit.HeaderText = "Main UOM"
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
        repoAlterUnit.ReadOnly = True
        repoAlterUnit.IsVisible = False
        repoAlterUnit.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoAlterUnit)

        Dim reporateUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporateUnit.FormatString = ""
        reporateUnit.HeaderText = "Rate UOM"
        reporateUnit.Name = colUnitRate
        reporateUnit.Width = 80
        reporateUnit.ReadOnly = True
        reporateUnit.IsVisible = False
        reporateUnit.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(reporateUnit)

        Dim repoOrgUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrgUnit.FormatString = ""
        repoOrgUnit.HeaderText = "ORG UOM"
        repoOrgUnit.Name = colOrgUnit
        repoOrgUnit.Width = 80
        repoOrgUnit.ReadOnly = False
        repoOrgUnit.IsVisible = False
        repoOrgUnit.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoOrgUnit)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Dispatch Qty(as per UOM)"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = False
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoMainUnitQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMainUnitQty = New GridViewDecimalColumn()
        repoMainUnitQty.FormatString = ""
        repoMainUnitQty.HeaderText = "Rate Unit Qty"
        repoMainUnitQty.Name = colRateUnitQty
        repoMainUnitQty.Width = 80
        repoMainUnitQty.Minimum = 0
        repoMainUnitQty.ShowUpDownButtons = False
        repoMainUnitQty.Step = 0
        repoMainUnitQty.IsVisible = False
        repoMainUnitQty.VisibleInColumnChooser = False
        repoMainUnitQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMainUnitQty)

        Dim repoAlterUnitQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAlterUnitQty = New GridViewDecimalColumn()
        repoAlterUnitQty.FormatString = ""
        repoAlterUnitQty.HeaderText = "Alter Unit Qty"
        repoAlterUnitQty.Name = colAlterUnitQty
        repoAlterUnitQty.Width = 80
        repoAlterUnitQty.Minimum = 0
        repoAlterUnitQty.ShowUpDownButtons = False
        repoAlterUnitQty.Step = 0
        repoAlterUnitQty.IsVisible = False
        repoAlterUnitQty.VisibleInColumnChooser = False
        repoAlterUnitQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAlterUnitQty)

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
        repoSchemeApp.IsVisible = False
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
        repoConv.IsVisible = False
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

        Dim repoTotItemWtMetric As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemWtMetric = New GridViewDecimalColumn()
        repoTotItemWtMetric.FormatString = ""
        repoTotItemWtMetric.HeaderText = "Tot Item Weight"
        repoTotItemWtMetric.Name = colItemWeightMetric
        repoTotItemWtMetric.Width = 80
        repoTotItemWtMetric.Minimum = 0
        repoTotItemWtMetric.ReadOnly = False
        repoTotItemWtMetric.IsVisible = False
        repoTotItemWtMetric.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotItemWtMetric)



        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = "{0:n2}"
        repoRate.HeaderText = "Basic Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoCrate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCrate.FormatString = ""
        repoCrate.HeaderText = "Crate"
        repoCrate.Name = colCrate
        If AutoCalculateCrate = 1 Then
            repoCrate.IsVisible = True
        Else
            repoCrate.IsVisible = False
        End If
        repoCrate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCrate)
        ''

        Dim TAX_PAID As New GridViewComboBoxColumn
        TAX_PAID.FormatString = ""
        TAX_PAID.HeaderText = "Tax Paid"
        TAX_PAID.Name = colTAX_PAID
        TAX_PAID.Width = 100
        TAX_PAID.ReadOnly = False
        TAX_PAID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        TAX_PAID.DataSource = clsCSABooking.GetTaxPaid()
        TAX_PAID.ValueMember = "Code"
        TAX_PAID.DisplayMember = "Name"
        TAX_PAID.IsVisible = False
        TAX_PAID.VisibleInColumnChooser = False
        gv1.Columns.Add(TAX_PAID)


        Dim repoOrgRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgRate = New GridViewDecimalColumn()
        repoOrgRate.FormatString = ""
        repoOrgRate.HeaderText = "Org Basic Rate"
        repoOrgRate.Name = colOrgCost
        repoOrgRate.Width = 80
        repoOrgRate.Minimum = 0
        repoOrgRate.ReadOnly = True
        repoOrgRate.IsVisible = False
        repoOrgRate.VisibleInColumnChooser = False
        repoOrgRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgRate)

        Dim repoIsSchmItem13 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem13.FormatString = ""
        repoIsSchmItem13.HeaderText = "Scheme Type"
        repoIsSchmItem13.Name = colSchmCodeType
        repoIsSchmItem13.Width = 50
        repoIsSchmItem13.ReadOnly = True
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
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFreeQty)

        Dim repoLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationCode.FormatString = ""
        repoLocationCode.HeaderText = "Location Code"
        repoLocationCode.Name = colLocationCode
        repoLocationCode.HeaderImage = My.Resources.search4
        repoLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLocationCode.Width = 100
        repoLocationCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLocationCode)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Location"
        repoLocationName.Name = colLocationName
        repoLocationName.ReadOnly = True
        repoLocationName.Width = 150
        repoLocationName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLocationName)


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
        repoCustDiscountQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscountQty)

        Dim repoCustDiscountPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscountPer.HeaderText = "Cash Dis %."
        repoCustDiscountPer.MinWidth = 4
        repoCustDiscountPer.Name = colCustDiscPercentage
        repoCustDiscountPer.ReadOnly = True
        repoCustDiscountPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscountPer.Width = 54
        repoCustDiscountPer.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscountPer)


        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.HeaderText = "Cash Dis Amt."
        repoCustDiscount.MinWidth = 4
        repoCustDiscount.Name = colcustDiscount
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscount.Width = 54
        repoCustDiscount.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)

        Dim repoCashSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCashSchemeCode.HeaderText = "Cash Scheme Code"
        repoCashSchemeCode.Name = colCashDiscSchemeCode
        repoCashSchemeCode.Width = 80
        repoCashSchemeCode.ReadOnly = True
        repoCashSchemeCode.IsVisible = False
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
        repoMarkupPer.IsVisible = False
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
        repoLandingCost.IsVisible = False
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
        repoPurCost.IsVisible = False
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

        Dim repoItemTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTaxCode = New GridViewTextBoxColumn()
        repoItemTaxCode.FormatString = ""
        repoItemTaxCode.HeaderText = "Item Tax Code"
        repoItemTaxCode.Name = colItemwiseTaxCode
        repoItemTaxCode.Width = 100
        repoItemTaxCode.IsVisible = False
        repoItemTaxCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTaxCode)

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
        If intDispatchfromDelivery = 0 Then
            repoRequition.HeaderText = "Ref No"
        Else
            repoRequition.HeaderText = "Ref No"
        End If
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

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsBatchItem)
        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        Dim repoDisc_Scheme_Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisc_Scheme_Amount.FormatString = ""
        repoDisc_Scheme_Amount.HeaderText = "Disc_Scheme_Amount"
        repoDisc_Scheme_Amount.Name = Disc_Scheme_Amount
        repoDisc_Scheme_Amount.Width = 200
        repoDisc_Scheme_Amount.ReadOnly = True
        repoDisc_Scheme_Amount.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Amount)

        Dim RepoDisc_Scheme_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoDisc_Scheme_Code.FormatString = ""
        RepoDisc_Scheme_Code.HeaderText = "Disc_Scheme_Code"
        RepoDisc_Scheme_Code.Name = Disc_Scheme_Code
        RepoDisc_Scheme_Code.Width = 100
        RepoDisc_Scheme_Code.ReadOnly = True
        RepoDisc_Scheme_Code.IsVisible = False
        RepoDisc_Scheme_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(RepoDisc_Scheme_Code)

        Dim RepoDisc_Scheme_Pers As GridViewDecimalColumn = New GridViewDecimalColumn()
        RepoDisc_Scheme_Pers.FormatString = ""
        RepoDisc_Scheme_Pers.HeaderText = "Disc_Scheme_Pers"
        RepoDisc_Scheme_Pers.Name = Disc_Scheme_Pers
        RepoDisc_Scheme_Pers.Width = 100
        RepoDisc_Scheme_Pers.ReadOnly = True
        RepoDisc_Scheme_Pers.IsVisible = False
        RepoDisc_Scheme_Pers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(RepoDisc_Scheme_Pers)

        Dim repoDisc_Scheme_Type As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDisc_Scheme_Type.FormatString = ""
        repoDisc_Scheme_Type.HeaderText = "Disc_Scheme_Type"
        repoDisc_Scheme_Type.Name = Disc_Scheme_Type
        repoDisc_Scheme_Type.Width = 200
        repoDisc_Scheme_Type.ReadOnly = True
        repoDisc_Scheme_Type.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Type)


        '' added by Panch Raj for whollyCodw
        Dim Booking_User_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Booking_User_Code.FormatString = ""
        Booking_User_Code.HeaderText = "Booking User Code"
        Booking_User_Code.Name = colBooking_User_Code
        Booking_User_Code.Width = 100
        Booking_User_Code.ReadOnly = True
        Booking_User_Code.IsVisible = True
        Booking_User_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Booking_User_Code)

        Dim Distributor_Retailer_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Distributor_Retailer_Code.FormatString = ""
        Distributor_Retailer_Code.HeaderText = "Distributer Code"
        Distributor_Retailer_Code.Name = colDistributor_Retailer_Code
        Distributor_Retailer_Code.Width = 100
        Distributor_Retailer_Code.ReadOnly = True
        Distributor_Retailer_Code.IsVisible = True
        Distributor_Retailer_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Distributor_Retailer_Code)

        Dim Distributor_Retailer_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Distributor_Retailer_Name.FormatString = ""
        Distributor_Retailer_Name.HeaderText = "Distributer Name"
        Distributor_Retailer_Name.Name = colDistributor_Retailer_Name
        Distributor_Retailer_Name.Width = 100
        Distributor_Retailer_Name.ReadOnly = True
        Distributor_Retailer_Name.IsVisible = True
        Distributor_Retailer_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(Distributor_Retailer_Name)

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
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                    End If
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(ColCommParty) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse e.Column Is gv1.Columns(colUnit) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                        If (e.Column Is gv1.Columns(colQty) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse e.Column Is gv1.Columns(colDisPer) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal)) Then
                            If ((e.Column Is gv1.Columns(colQty))) Then
                                Dim dblPendingQty As Double = 0
                                If (clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) > 0) Then
                                    dblPendingQty = GetBalanceDeliveryQty(gv1.CurrentRow.Cells(colOrderNo).Value, gv1.CurrentRow.Cells(colICode).Value)

                                    Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)

                                    Dim dblDamageQty As Double = 0 'clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)
                                    If (dblEnteredQty + dblDamageQty) > dblPendingQty Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                        gv1.CurrentCell.Value = dblPendingQty
                                    End If
                                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colOrderNo).Value) = 0 Then
                                    If AutoScheme Then
                                        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                                    End If
                                End If
                                If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
                                    findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)
                                End If
                                OpenSerialItem()
                                OpenBatchItem()
                            End If

                            UpdateCurrentRow(gv1.CurrentRow.Index)

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
                            If AutoScheme Then
                                gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                            End If
                            findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)
                         
                        ElseIf e.Column Is gv1.Columns(colSchemeApplicable) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colUnitRate).Value) > 0 Then
                                findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Please select Rate Unit", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            'OpenGetbalance(False)
                        ElseIf e.Column Is gv1.Columns(colRate) Then

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                      
                        ElseIf e.Column Is gv1.Columns(colAmt) Then

                       
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
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
            isCellValueChangedOpen = False
        End Try
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If (myMessages.postConfirm()) Then
            If (AllowToSave(True)) Then
                If PostData(Nothing) Then

                End If
            End If
        End If
    End Sub
    Function PostData(ByVal trans As SqlTransaction) As Boolean
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If trans Is Nothing Then
                If SaveData(True, Nothing) Then
                    If (clsBookingProformaInvoice.PostData(MyBase.Form_ID, txtDocNo.Value, True)) Then
                        msg = "Successfully Posted"
                        common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
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


                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)

            End If
            Return True
        Catch ex As Exception
            If blnReverse = False Then
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Return False
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Function
    Private Sub findQtyandPromoSchemeCode(ByVal isButtonClick As Boolean, Optional ByVal SchemeCode As String = Nothing)
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

            If gv1.CurrentRow.Cells(colFromSchemeCode).Value = "" AndAlso intDispatchfromDelivery = 0 Then
                Dim Qry As String = "select isnull(Scheme_Code,'') as Scheme_Code from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE on " & _
                " TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No" & _
                " where TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + txtReqNo.Value + "' and TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code='" + gv1.CurrentRow.Cells(colUnit).Value + "' and isnull(TSPL_GATEPASS_DETAIL_DAIRYSALE.Scheme_Item,'N')='N'"
                Dim SchemeCodeValue As String = clsDBFuncationality.getSingleValue(Qry)
                SchemeCode = SchemeCodeValue
                If SchemeCode <> "" Then
                    If AutoScheme = True Then
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                    End If
                End If
            Else
                Dim Qry As String = "select isnull(Scheme_Code,'') as Scheme_Code from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE on " & _
                        " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No" & _
                        " where TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No='" + txtReqNo.Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Unit_code='" + gv1.CurrentRow.Cells(colUnit).Value + "' and isnull(TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Scheme_Item,'N')='N'"
                Dim SchemeCodeValue As String = clsDBFuncationality.getSingleValue(Qry)
                SchemeCode = SchemeCodeValue
                If SchemeCode <> "" Then
                    If AutoScheme = True Then
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                    End If
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) = 0 Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colOrderNo).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colOrderNo).Value)) = CompairStringResult.Equal Then
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
                gv1.Rows(Index).Cells(colSchemeItem).Value = "No"

                gv1.Rows(Index).Cells(ColFOC).Value = 0

            End If
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then

                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "Yes") = CompairStringResult.Equal Then
                    'For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    '    For Each grow As GridViewRowInfo In gv1.Rows
                    '        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                    '            gv1.Rows.RemoveAt(grow.Index)
                    '        End If
                    '    Next
                    'Next

                    For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colOrderNo).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colOrderNo).Value)) = CompairStringResult.Equal Then
                            gv1.Rows.RemoveAt(schemeRow)
                        End If
                    Next


                    'Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value))
                    Dim objD As clsSchemeApplyOnDairy = Nothing

                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value), clsCommon.myCstr(SchemeCode))
                    'If objD.Arr.Count = 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colUnitALter).Value) > 0 Then
                    '    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value))
                    'End If
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
                            Dim MainSaleOrderCode As String = gv1.Rows(Index).Cells(colOrderNo).Value
                            '-------------------------------------------------------------

                            '-------------------------------------------------------------

                            gv1.Rows.AddNew()

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intRow + 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objtr.Schm_Icode, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
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
                         
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = MainItemCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = MainItemQty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = MainItemUnit
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = MainSaleOrderCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = clsDBFuncationality.getSingleValue("select CSA_TYPE from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "' ")
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Schm_Icode)
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

                          
                            gv1.Rows.Move(gv1.Rows.Count - 1, Index + 1)

                            UpdateCurrentRow(gv1.Rows(Index + 1).Index)
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
                        gv1.Rows(Index).Cells(colSchemeItem).Value = "No"
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
    Private Sub UpdateAllTotals()
        Dim dblCashDisAmt As Double = 0
        Dim dblHeadDisPerAmt As Double = 0
        Dim dblCommAmt As Double = 0

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
        Dim dblTotalWtMetric As Double = 0
        Dim dblTotalWt As Double = 0


        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTotalWtMetric = dblTotalWtMetric + clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemWeightMetric).Value)
            dblTotalWt = dblTotalWt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotItemWt).Value)

            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                If clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 1 Then
                    dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                Else
                    'dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                End If

                'sanjay Discount Amt
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                'sanjay Discount Amt

                'dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                'dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                dblCashDisAmt = dblCashDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCash_Amt).Value)
                dblHeadDisAmt = dblHeadDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDiscamt).Value)
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

                'dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
                dblNetAmt = dblAmtAfterDis + dblTaxTotAmt
                dblTotLandedCost = dblTotLandedCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLandedAmt).Value)

            End If
        Next

        If rbtnTaxCalAutomatic.IsChecked Then

            For ii As Integer = 1 To gv2.Rows.Count
                If Not clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
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
                End If

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
        lblTotRAmt1.Text = lblTotRAmt.Text
        lblCommAmt.Text = clsCommon.myFormat(dblCommAmt)
        lblTotalWtMetric.Text = dblTotalWtMetric
        If Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) > clsCommon.myCdbl(lblTotRAmt.Text) Then
            'TxtRoundoff.Text = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(clsCommon.myCdbl(lblTotRAmt1.Text)) - Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text), 0)), 2)
            TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
        Else
            'TxtRoundoff.Text = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(lblTotRAmt1.Text) - Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text))), 2)
            TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text)) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
        End If
        'If clsCommon.myLen(txtTransporterCode.Value) > 0 Then
        '    lblFreightCharges.Text = Math.Round(clsCSATransfer.GetProvisionCharge(txtBillToLocation.Value, txtVendorNo.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicleCapacity.Value), clsCommon.myCstr(txtTransporterCode.Value)), 2)
        'Else
        '    lblFreightCharges.Text = 0
        'End If
        FillVehicleCharges()
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)

            Dim dblFAmt As Double = 0
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus = False Then
                strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'")) = "T", True, False)
            End If
            Dim dblAlterQty As Double = 0
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
            Dim strUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
            Dim dblBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
            Dim dblItemWeight As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemWeight).Value)

            Dim dblheadDiscamt As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeadDiscamt).Value)

            Dim wt_unit As String = 0
            Dim wt_qty As Double = 0
            Dim Item_Weight As Double = 0
            Dim TotalItem_Weight As Double = 0
            Dim TotalItem_WeightMetric As Double = 0
            If clsCommon.myLen(strICode) > 0 Then
                wt_unit = clsItemMaster.GetItemWeightUnit(strICode, Nothing)
                TotalItem_Weight = clsItemMaster.getTotalItemWeight(strICode, strUnit, dblQty, Nothing)
            End If
            gv1.Rows(IntRowNo).Cells(colItemWeight).Value = Item_Weight
            gv1.Rows(IntRowNo).Cells(colTotItemWt).Value = TotalItem_Weight
            gv1.Rows(IntRowNo).Cells(colItemWeightMetric).Value = TotalItem_WeightMetric
           
            Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
            Dim dblBasicAmt As Double = dblQty * dblRate
            Dim dblMRPAmt As Double = dblQty * dblMRP
            Dim dblAmt As Double = (dblQty * dblRate) ''+ dblFAmt

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then 'AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            Else
                dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
            End If

            ''''' to calculate customer disc
            Dim dt As New DataTable
            Dim dblOrderQty As Double = 0
            Dim dblCustDiscQty As Double = 0
            Dim dblCustDiscAmt As Double = 0
            Dim dblCustDiscPercentage As Double = 0
            Dim dblApplyCustDisc As Double = 0
            Dim dblTotCustDisc As Double = 0



            If clsCommon.myLen(strICode) > 0 And clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then

                Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
                obj_Cash = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), txtVendorNo.Value)
                'If clsCommon.myLen(obj_Cash.Schm_Code) = 0 AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colUnitALter).Value) > 0 Then
                '    obj_Cash = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitALter).Value), clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), txtVendorNo.Value)
                'End If
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

            End If

            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCash_Amt).Value) > dblAmt Then
                gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = 0
                gv1.Rows(IntRowNo).Cells(colCash_Pers).Value = 0
                gv1.Rows(IntRowNo).Cells(colCashSchemeCode).Value = Nothing
                gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = Nothing
            End If

            ''''' end 

            Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
            Dim dblHeadDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaDDisPer).Value)
            Dim dblHeadPerDisAmt As Double = (dblAmt * dblHeadDisPer) / 100
            Dim dblCashAmt As Double = gv1.Rows(IntRowNo).Cells(colCash_Amt).Value
            Dim dblTotDiscAmt = dblheadDiscamt + dblHeadPerDisAmt + dblDisAmt + dblCashAmt
            Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt - dblCashAmt - dblheadDiscamt - dblHeadPerDisAmt
            Dim dblAbatementRate As Double = abatement()
            Dim dblAbatementAmt As Double = ((dblMRP * dblAbatementRate) / 100) * dblQty

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

                            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
                                If Not clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
                                    If strExcise = True AndAlso IsExcisable = True Then
                                        dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                                    Else
                                        dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                                    End If
                                End If

                            Else
                                If strExcise = True AndAlso IsExcisable = True Then
                                    dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                                Else
                                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                                End If
                            End If

                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
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

            If dblQty > 0 Then
                Dim dblNetPrice As Double = dblAmtAfterDis / dblQty
                gv1.Rows(IntRowNo).Cells(colActualCost).Value = dblNetPrice
            End If
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = 0
            If clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
                'dblAmtAfterTax = dblAmtAfterDis
                dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt
            Else
                dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt
            End If
            gv1.Rows(IntRowNo).Cells(colAlterUnitQty).Value = Math.Round(dblAlterQty, 2)
            gv1.Rows(IntRowNo).Cells(colRateUnitQty).Value = Math.Round(dblQty, 2)
            gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            gv1.Rows(IntRowNo).Cells(colAbatementAmount).Value = Math.Round(dblAbatementAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalMRP).Value = Math.Round(dblMRPAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value = Math.Round(dblBasicAmt, 2)
            'gv1.Rows(IntRowNo).Cells(colTotItemWt).Value = Math.Round(dblConvF * dblItemWeight * dblQty, 2)
            gv1.Rows(IntRowNo).Cells(colTotalCustDiscount).Value = Math.Round(dblTotCustDisc, 2)
            gv1.Rows(IntRowNo).Cells(colRate).Value = dblRate
            gv1.Rows(IntRowNo).Cells(colHeadDisPerAmt).Value = Math.Round(dblHeadPerDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colTotalDiscountAmount).Value = Math.Round(dblTotDiscAmt, 2)
            gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strOrgUnit
            gv1.Rows(IntRowNo).Cells(colMRP).Value = Math.Round(dblMRP, 2)
            'If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCash_Amt).Value) > dblAmt Then
            '    gv1.Rows(IntRowNo).Cells(colCash_Amt).Value = 0
            '    gv1.Rows(IntRowNo).Cells(colCash_Pers).Value = 0
            '    gv1.Rows(IntRowNo).Cells(colCashSchemeCode).Value = Nothing
            '    gv1.Rows(IntRowNo).Cells(colCashSchemeType).Value = Nothing
            'End If

            If AutoCalculateCrate = 1 Then
                If clsCommon.myLen(strICode) > 0 Then 'AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0
                    '' Anubhooti 11-Sep-2014 BM00000003847
                    Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'"))
                    If ItemCrateType = 1 Then
                        Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "'"))
                        'If IsStockingUnit = "Y" Then
                        Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and tspl_unit_master.Crate_Type ='Y' "))
                        Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' "))

                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                            Dim DispatchQty As Double = gv1.Rows(IntRowNo).Cells(colQty).Value * ItemConvFactor

                            If DispatchQty >= CrateConvFactor Then
                                'If IncreaseCrateQtyOnFiftyPercent = True Then
                                '    Dim IntegerPart As Integer = Math.Floor(DispatchQty / CrateConvFactor)
                                '    Dim fractionPart As Integer = ((DispatchQty / CrateConvFactor) - IntegerPart) * 100
                                '    If fractionPart >= 50 Then
                                '        gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Ceiling(DispatchQty / CrateConvFactor)
                                '    Else
                                '        gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Floor(DispatchQty / CrateConvFactor)
                                '    End If
                                'Else
                                '    gv1.Rows(IntRowNo).Cells(colCrate).Value = Math.Floor(DispatchQty / CrateConvFactor)
                                'End If
                            Else
                                gv1.Rows(IntRowNo).Cells(colCrate).Value = 0
                            End If
                        Else

                            clsCommon.MyMessageBoxShow(Me, "Please fill conversion factor for this unit at line no." & IntRowNo + 1 & "")

                        End If
                    End If
                End If

                Dim TotalCrate As Integer = 0
                For i As Integer = 0 To gv1.Rows.Count - 1
                    TotalCrate = TotalCrate + gv1.Rows(i).Cells(colCrate).Value
                Next
             
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
    Private Sub FillVehicleCharges()
        Dim dt As New DataTable()
        dt = clsCSATransfer.GetProvisionCharge(txtBillToLocation.Value, txtVendorNo.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicleCapacity.Text), txtTransporterCode.Value)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblFreightCharges.Text = clsCommon.myCdbl(dt.Rows(0)("FreightCharge"))
            lblFreightCharges.Tag = dt
        Else
            lblFreightCharges.Text = "0"
            lblFreightCharges.Tag = Nothing
        End If
    End Sub
    Function GetBalanceDeliveryQty(ByVal strDeliveryCode As String, ByVal strICode As String) As Double
        Dim strItem As String = ""
        Dim qry As String = ""
        strItem = "Item_Code='" & strICode & "'"
      
        qry = "select sum (qty) from (" & _
 "select Booking_Qty as qty from TSPL_BOOKING_DETAIL where Document_No='" & strDeliveryCode & "' and " & strItem & " union all " & _
 " select -1 * Qty from TSPL_DAIRY_PROFORMA_INVOICE_detail inner join TSPL_DAIRY_PROFORMA_INVOICE_HEAD on TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.DOCUMENT_CODE where TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No='" & strDeliveryCode & "' and " & strItem & " and TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No not in ('" & txtDocNo.Value & "'))a"


        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
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
    Private Function abatement() As Decimal
        Dim abat As Decimal = 0
        Dim sql As String = "select top 1 abatement_percent from tspl_abatement_master where start_date <='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and end_date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' "
        abat = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
        If abat > 0 Then
            abat = abat
        Else
            abat = 60
        End If
        Return abat
    End Function
    Sub OpenBatchItem()

        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then

            Dim TransType_Str As String = ""
            TransType_Str = IIf(rbtn_Fresh.IsChecked = True, "FS", "PS")
            TransType_Str = TransType_Str & "-SH"

            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = txtBillToLocation.Value
            frm.strCurrDocNo = txtDocNo.Value
            frm.strCurrDocType = TransType_Str
            '"PS-SH"
            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            'frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value) comment by balwinder on UDL on 02/12/2016
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
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("DSShipmentItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

            Dim dblConvF As Double
            dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
            gv1.CurrentRow.Cells(colConvF).Value = dblConvF


        End If

    End Sub
    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtVendorNo.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Customer First", Me.Text)
            Exit Sub
        ElseIf clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow(Me,"Please select Location First", Me.Text)
            Exit Sub
            'ElseIf clsCommon.myLen(txtTaxGroup.Value) = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Either Map taxgroup with location or Select tax group First")
            '    Exit Sub
        End If
        'gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Row Type", Me.Text)
            Exit Sub
        End If

        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then

            Dim ItemTypeForBooking = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.ItemTypeForDairyBooking & "'"))
            Dim strItem As String = ""

            If rbtn_Fresh.IsChecked = True Then
                strItem = "Is_FreshItem =1 "
            Else
                strItem = "Is_Ambient=1"
            End If
            If cmbDisItemType.SelectedValue = "NT" Then
                strItem += " and IsTaxable=0 "
            Else
                strItem += " and IsTaxable=1 "
            End If
            gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(strItem, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), False)
            gv1.CurrentRow.Cells(colUnit).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            gv1.CurrentRow.Cells(colIName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            gv1.CurrentRow.Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
            gv1.CurrentRow.Cells(colIsBatchItem).Value = clsDBFuncationality.getSingleValue("select Is_Batch_Item from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            ItemPrice(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Index)
        Else

            ''For Open Misc Charges 
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Code
                gv1.CurrentRow.Cells(colIName).Value = obj.desc
                gv1.CurrentRow.Cells(colIHSN).Value = obj.SACCode
                gv1.CurrentRow.Cells(colUnit).Value = Nothing
                gv1.CurrentRow.Cells(colQty).Value = Nothing
                gv1.CurrentRow.Cells(colRate).Value = Nothing
                'gv1.CurrentRow.Cells(colisMRPMandatory).Value=False
            Else
                '  SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 
        End If
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                    Dim frm As New FrmPOItemTaxDetails()
                    frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                    frm.strTaxGroup = clsCommon.myCstr(txtTaxGroup.Value)
                    frm.strTaxType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Type from TSPL_TAX_group_MASTER where Tax_Group_Code='" & txtTaxGroup.Value & "'"))
                    frm.strTransLocation = txtBillToLocation.Value
                    frm.strVendorCustomerCode = txtVendorNo.Value
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
                ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            Dim strCustomer As String = ""
            If rbtnTaxCalAutomatic.IsChecked Then
                If clsCommon.myLen(strCustomer) <= 0 Then
                    strCustomer = txtVendorNo.Value
                End If
                If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                    Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtBillToLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), strCustomer, "S")
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function ItemPrice(ByVal strItem As String, ByVal strUnit As String, ByVal introw As Integer) As Double
        Dim strTaxType As String = ""
        Dim strTax As String = ""
        Dim IsTaxable As Integer = 0

        If gv1.Rows(introw).Cells(colRowType).Value = "Misc" Then
            'Exit Function
            Return 0
        End If

        If gv1.Rows(introw).Cells(ColFOC).Value = 1 Then
            IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & strItem & "'"))
            If clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
                If IsTaxable = 1 Then
                    strTaxType = clsLocationWiseTax.TaxType(txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value, Nothing)
                    If strTaxType = "L" Then
                        strTax = "  and tax_group='" & txtSchemeTaxGroup.Value & "'"
                    Else
                        strTax = "  and tax_group ='" & txtSchemeTaxGroup.Value & "'"
                    End If
                End If
            Else
                If IsTaxable = 1 Then
                    strTax = "  and tax_group='" & txtTaxGroup.Value & "'"
                Else
                    strTax = ""
                End If
            End If
        End If


        Dim qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " & _
            " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " & _
            "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " & _
            " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " & _
            " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " & _
            " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,Tax_group   from ( " & _
       "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
       "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
       "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " & _
       "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " & _
       " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " & _
       " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " & _
       " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " & _
       " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10 ,TSPL_ITEM_PRICE_MASTER.Tax_group  from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
       "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
       "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and  " & _
       "TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "'    and UOM='" & strUnit & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & strItem & "' " & _
       "AND Location_Code='" & txtBillToLocation.Value & "' " & strTax & " " & _
       ") XXXE WHERE RowNo=1  "
        'and Tax_group='" & txtTaxGroup.Value & "'
        Dim dt As New DataTable()
        Dim dblRate As Double = 0
        Dim dblTotal As Double = 0
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then

            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Then
            '    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
            '    gv1.Rows(introw).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
            'Else
            '    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            '    gv1.Rows(introw).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            'End If
            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            gv1.Rows(introw).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))

            gv1.Rows(introw).Cells(colMRP).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Net"))
            gv1.Rows(introw).Cells(colTaxRate1).Value = clsCommon.myCdbl(dt.Rows(0)("Tax1_Rate"))
            gv1.Rows(introw).Cells(colTaxRate2).Value = clsCommon.myCdbl(dt.Rows(0)("Tax2_Rate"))
            gv1.Rows(introw).Cells(colTaxRate3).Value = clsCommon.myCdbl(dt.Rows(0)("Tax3_Rate"))
            gv1.Rows(introw).Cells(colTaxRate4).Value = clsCommon.myCdbl(dt.Rows(0)("Tax4_Rate"))
            gv1.Rows(introw).Cells(colTaxRate5).Value = clsCommon.myCdbl(dt.Rows(0)("Tax5_Rate"))
            gv1.Rows(introw).Cells(colTaxRate6).Value = clsCommon.myCdbl(dt.Rows(0)("Tax6_Rate"))
            gv1.Rows(introw).Cells(colTaxRate7).Value = clsCommon.myCdbl(dt.Rows(0)("Tax7_Rate"))
            gv1.Rows(introw).Cells(colTaxRate8).Value = clsCommon.myCdbl(dt.Rows(0)("Tax8_Rate"))
            gv1.Rows(introw).Cells(colTaxRate9).Value = clsCommon.myCdbl(dt.Rows(0)("Tax9_Rate"))
            gv1.Rows(introw).Cells(colTaxRate10).Value = clsCommon.myCdbl(dt.Rows(0)("Tax10_Rate"))

            gv1.Rows(introw).Cells(colTax1).Value = clsCommon.myCstr(dt.Rows(0)("Tax1"))
            gv1.Rows(introw).Cells(colTax2).Value = clsCommon.myCstr(dt.Rows(0)("Tax2"))
            gv1.Rows(introw).Cells(colTax3).Value = clsCommon.myCstr(dt.Rows(0)("Tax3"))
            gv1.Rows(introw).Cells(colTax4).Value = clsCommon.myCstr(dt.Rows(0)("Tax4"))
            gv1.Rows(introw).Cells(colTax5).Value = clsCommon.myCstr(dt.Rows(0)("Tax5"))
            gv1.Rows(introw).Cells(colTax6).Value = clsCommon.myCstr(dt.Rows(0)("Tax6"))
            gv1.Rows(introw).Cells(colTax7).Value = clsCommon.myCstr(dt.Rows(0)("Tax7"))
            gv1.Rows(introw).Cells(colTax8).Value = clsCommon.myCstr(dt.Rows(0)("Tax8"))
            gv1.Rows(introw).Cells(colTax9).Value = clsCommon.myCstr(dt.Rows(0)("Tax9"))
            gv1.Rows(introw).Cells(colTax10).Value = clsCommon.myCstr(dt.Rows(0)("Tax10"))
        Else

            If gv1.Rows(introw).Cells(ColFOC).Value = 1 AndAlso clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal And IsTaxable = 1 Then
                clsCommon.MyMessageBoxShow("Please Create Price Chart For  " + Environment.NewLine + _
                                                 "Location    --  " & clsCommon.myCstr(txtBillToLocation.Value) & " " + Environment.NewLine + _
                                                 "Scheme Tax Group  --   " & clsCommon.myCstr(txtSchemeTaxGroup.Value) & " " + Environment.NewLine + _
                                                 "Price Code --   " & clsCommon.myCstr(txtPriceCode.Text) & " " + Environment.NewLine + _
                                                 "State Date < =  " & clsCommon.myCstr(txtDate.Value) & " " + Environment.NewLine + _
                                                 "Item         --   " & clsCommon.myCstr(gv1.Rows(introw).Cells(colICode).Value) & " " + Environment.NewLine + _
                                                 "Unit         --   " & clsCommon.myCstr(strUnit) & ".", Me.Text)            'gv1.Rows(introw).Cells(colICode).Value = Nothing
            Else
                If IsTaxable = 1 Then
                    clsCommon.MyMessageBoxShow("Please Create Price Chart For  " + Environment.NewLine + _
                                               "Location    --  " & clsCommon.myCstr(txtBillToLocation.Value) & " " + Environment.NewLine + _
                                               "Tax Group  --   " & clsCommon.myCstr(txtTaxGroup.Value) & " " + Environment.NewLine + _
                                               "Price Code --   " & clsCommon.myCstr(txtPriceCode.Text) & " " + Environment.NewLine + _
                                               "State Date < =  " & clsCommon.myCstr(txtDate.Value) & " " + Environment.NewLine + _
                                               "Item         --   " & clsCommon.myCstr(gv1.Rows(introw).Cells(colICode).Value) & " " + Environment.NewLine + _
                                               "Unit         --   " & clsCommon.myCstr(strUnit) & ".", Me.Text)            'gv1.Rows(introw).Cells(colICode).Value = Nothing
                Else
                    clsCommon.MyMessageBoxShow("Please Create Price Chart For  " + Environment.NewLine + _
                                               "Location    --  " & clsCommon.myCstr(txtBillToLocation.Value) & " " + Environment.NewLine + _
                                               "Price Code --   " & clsCommon.myCstr(txtPriceCode.Text) & " " + Environment.NewLine + _
                                               "State Date < =  " & clsCommon.myCstr(txtDate.Value) & " " + Environment.NewLine + _
                                               "Item         --   " & clsCommon.myCstr(gv1.Rows(introw).Cells(colICode).Value) & " " + Environment.NewLine + _
                                               "Unit         --   " & clsCommon.myCstr(strUnit) & ".", Me.Text)            'gv1.Rows(introw).Cells(colICode).Value = Nothing
                End If

            End If

            'gv1.Rows(introw).Cells(colUnit).Value = Nothing
            'gv1.Rows(introw).Cells(colIName).Value = Nothing
            'Exit Function
            Return 0
        End If
        Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
        obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(gv1.Rows(introw).Cells(colICode).Value, gv1.Rows(introw).Cells(colICode).Value, 1, clsCommon.myCstr(txtVendorNo.Value))
        If obj_Cash IsNot Nothing Then
            gv1.Rows(introw).Cells(Disc_Scheme_Amount).Value = obj_Cash.Cash_Amt
            gv1.Rows(introw).Cells(Disc_Scheme_Pers).Value = obj_Cash.Cash_Pers
            gv1.Rows(introw).Cells(Disc_Scheme_Code).Value = obj_Cash.Schm_Code
            If clsCommon.myCdbl(gv1.Rows(introw).Cells(Disc_Scheme_Pers).Value) <> 0 Then
                gv1.Rows(introw).Cells(Disc_Scheme_Type).Value = "P"
                gv1.Rows(introw).Cells(Disc_Scheme_Amount).Value = System.Math.Round((dblRate * clsCommon.myCdbl(gv1.Rows(introw).Cells(Disc_Scheme_Pers).Value)) / 100, 2)
            ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                gv1.Rows(introw).Cells(Disc_Scheme_Type).Value = "A"
            End If
            dblRate = dblRate - clsCommon.myCdbl(gv1.Rows(introw).Cells(Disc_Scheme_Amount).Value)
        End If
        Return dblRate
    End Function
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
    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtBillToLocation.Value = clsCommon.ShowSelectForm("DS-SHLocFndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
        SetTax()
        intDispatchfromDelivery = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select DairyDispatchFromDO from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")) = 0, 0, 1)
       
    End Sub
    Private Sub SetTax()
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If GSTStatus = False OrElse (clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") <> CompairStringResult.Equal AndAlso GSTStatus) Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "S")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
        Else
            If clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal Then
                txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value)
                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            End If
        End If

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
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim strCustomer As String = ""
        Try
            strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colvendorCode).Value)

        Catch ex As Exception

        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtVendorNo.Value
        End If
        Dim IsTaxable As Integer = 0
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "'"))
                If ((clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 And IsTaxable = 1) OrElse (gv1.CurrentRow.Cells(colRowType).Value = "Misc")) Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        '' Changes by Parteek 21/09/2017
                        If (CalculateTaxRatefromItemwsieTaxOnSale = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                            If isChangeRate Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                        Else
                            If isChangeRate Then
                                Dim objTM As clsItemWiseTaxAuthority
                                objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "S")
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
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo, isChangeRate)
                    IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value) & "'"))
                    If ((clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 And IsTaxable = 1) OrElse (gv1.Rows(intRowNo).Cells(colRowType).Value = "Misc")) Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            '' == Changes by Parteek 21/09/2017
                            If (CalculateTaxRatefromItemwsieTaxOnSale = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                                If isChangeRate Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                If isChangeRate Then
                                    Dim objTM As clsItemWiseTaxAuthority
                                    objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "S")
                                    If objTM IsNot Nothing Then
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                        gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                    End If
                                End If
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
            Dim IsTaxable As Integer = 0
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value) & "'"))
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 AndAlso IsTaxable = 1 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        If blnReverse Then
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
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
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
  
        If SeparateDairyDispatchTaxableNonTaxable = 1 Then
            If clsCommon.CompairString(cmbDisItemType.SelectedValue, "") = CompairStringResult.Equal Then
                cmbDisItemType.Focusable = True
                clsCommon.MyMessageBoxShow(Me, "Please Select Item Type.", Me.Text)
                Exit Sub
            End If
        End If

        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------
        Dim qry As String = ""
        Dim strWhrClause As String = ""


        qry = "select Cust_Code as Code,Customer_Name as Name,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman  " & _
        ",TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Number,TSPL_VEHICLE_MASTER.Capacity "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'" & _
        "left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
        "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id "
        If clsCommon.myLen(txtDocNo.Value) > 0 And blnChangeCustomer = True Then
            Dim intCount As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(grow.Cells(colSchemeItem).Value, "Yes") = CompairStringResult.Equal Then
                    intCount = 1
                End If
            Next
            If intCount = 0 Then
                Dim strState = clsDBFuncationality.getSingleValue("select State from TSPL_LOCATION_MASTER where Location_Code='" & txtBillToLocation.Value & "'")

               
            Else
                strWhrClause = ""
            End If

        Else
            strWhrClause = ""
        End If



        If clsCommon.myLen(strwherecls) = 0 Then
            txtVendorNo.Value = clsCommon.ShowSelectForm("DSShipmentVendorFndr", qry, "Code", "TSPL_CUSTOMER_MASTER.Status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        Else
            txtVendorNo.Value = clsCommon.ShowSelectForm("DSShipmentVendorFndr", qry, "Code", strWhrClause + " TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtVendorNo.Value, "Code", isButtonClicked)
        End If
        '-----------------------------------------------------
        qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"

        Dim qrycheck As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Manual_Customer from TSPL_Customer_Master where Cust_Code='" + txtVendorNo.Value + "'"))
        If (clsCommon.CompairString(qrycheck, "Y")) = CompairStringResult.Equal Then
            txtManualCustomer.Enabled = True
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Term Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Term Description"))
            'txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
            'lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group Description"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman"))
           
            txtVehicleCode.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
            lblVhicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
            txtVehicleCapacity.Value = clsCommon.myCdbl(dt.Rows(0)("Capacity"))
            txtDate.Enabled = False

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
        End If

        SetTax()
        If clsCommon.CompairString(cmbDisItemType.SelectedValue, "NT") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) = 0 Then
            txtVendorNo.Value = ""
            clsCommon.MyMessageBoxShow(Me, "Please Map exempted Tax Group on Location " & txtBillToLocation.Value)
            Exit Sub
        End If

        SetTermDetails()
        strOrginalCust = clsDBFuncationality.getSingleValue("select Customer_Code from  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE  where Document_Code='" & txtReqNo.Value & "'")
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
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

        If ShowShipToPartyInDairyDispatch = 1 Then
            Dim qry As String
            qry = "select * from( select distinct Cust_Code as [Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code='" + txtVendorNo.Value + "' union all select distinct Cust_Code as [Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Parent_Customer_No='" + txtVendorNo.Value + "') as tt"
            txtShipToLocation.Value = clsCommon.ShowSelectForm("DS-SHPTY", qry, "Code", "1=1", txtShipToLocation.Value, "Code", isButtonClicked)
            lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtShipToLocation.Value + "'"))
            ParentCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Parent_Customer_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtShipToLocation.Value & "'"))
           
        Else
            Dim qry As String = " select distinct TSPL_SHIP_TO_LOCATION.Ship_To_Code as [Code],TSPL_SHIP_TO_LOCATION.Ship_To_Desc as [Description],TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code as[Customer Code] ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_SHIP_TO_LOCATION.Add1,'')='' then '' else TSPL_SHIP_TO_LOCATION.add1 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add2,'')='' then '' else TSPL_SHIP_TO_LOCATION.add2 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add3,'')='' then '' else TSPL_SHIP_TO_LOCATION.add3 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add4,'')='' then '' else TSPL_SHIP_TO_LOCATION.add4 +',' end ,',,',',') as [Ship to Address],TSPL_SHIP_TO_LOCATION.CST_No as [CST NO],TSPL_SHIP_TO_LOCATION.Tin_No as [TIN No]  from TSPL_SHIP_TO_LOCATION left outer join TSPL_SHIP_TO_LOCATION_LOCATIONS on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SHIP_TO_LOCATION_LOCATIONS.Ship_To_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code "
            txtShipToLocation.Value = clsCommon.ShowSelectForm("DS-ShipShindrlter", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and (TSPL_SHIP_TO_LOCATION_LOCATIONS.Loc_Code ='" & txtBillToLocation.Value & "' or  TSPL_SHIP_TO_LOCATION.Loc_Code='" & txtShipToLocation.Value & "' )", txtShipToLocation.Value, "Code", isButtonClicked)
            lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))
          
        End If
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
    Sub LoadDOItemType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        If Not clsERPFuncationality.GetGSTStatus(txtDate.Value) Then
            dr = dt.NewRow()
            dr("Code") = "B"
            dr("Name") = "Both"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Taxable"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "NT"
        dr("Name") = "Non Taxable"
        dt.Rows.Add(dr)

        cmbDisItemType.DataSource = dt
        cmbDisItemType.ValueMember = "Code"
        cmbDisItemType.DisplayMember = "Name"
    End Sub
    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                Dim strCustomer As String = ""
                Try
                    strCustomer = txtVendorNo.Value

                Catch ex As Exception

                End Try
                If clsCommon.myLen(strCustomer) <= 0 Then
                    strCustomer = txtVendorNo.Value
                End If

                Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
                txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtBillToLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.CompairString(gv1.Rows(ii).Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(ii).Cells(colTAX_PAID).Value, "Yes") = CompairStringResult.Equal Then

                    End If
                Next
                SetTaxDetails()

            Else
                Throw New Exception("Please select Location First")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (AllowToSave(False)) Then
            SaveData(False, Nothing)
        End If
    End Sub
    Function AllowToSave(ByVal ChekPostBtn As Boolean) As Boolean
        Try
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            RefreshReqNo()

            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) = 0 AndAlso (clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 0 OrElse ShowSchemeItemRate = 1) Then
                        gv1.Rows(ii).Cells(colRate).Value = ItemPrice(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), ii)
                        If clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value) = "Item" And clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) = 0 Then
                            'Exit Function
                            Return False
                        End If
                    End If
                End If
                UpdateCurrentRow(ii)
            Next

            UpdateAllTotals()

            If CalculateTaxRatefromItemwsieTaxOnSale = 1 Then
                SetitemWiseTaxSetting(True, False)
            End If

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Customer")
                txtVendorNo.Focus()
                Return False
            End If


            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                Throw New Exception("Please select Tax Group")
                txtTaxGroup.Focus()
                Return False
            End If


            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Bill to Location")
                txtBillToLocation.Focus()
                Return False
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Shipment No Not found to save")
                txtDocNo.Focus()
                Return False
            End If

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Document_Date,103) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from Delivery Date")
            End If
            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            Dim arrProjNo As New List(Of String)


            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strCommParty As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColCommParty).Value)
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colOrderNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strUOMRate As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitRate).Value)
                Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
                Dim strProject As String = Nothing
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If

                Dim objCustItem As clsCustomeritemDetails = clsCustomeritemDetails.GetItemRateAndDiscount(txtVendorNo.Value, strICode, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDate.Value)
                If objCustItem IsNot Nothing Then
                    If objCustItem.Min_Rate > (clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value) / dblQty) Then
                        Throw New Exception("Minimum Rate Can't be Less Then " + clsCommon.myCstr(objCustItem.Min_Rate) + " of Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)))
                        Return False
                    End If
                End If

                If clsCommon.myLen(strReqNo) > 0 Then
                    If Not (arrReqNo.Contains(strReqNo)) Then
                        arrReqNo.Add(strReqNo)
                    End If
                    dblPendingQty = GetBalanceDeliveryQty(strReqNo, strICode)

                End If

                If IsBatchMFDEXDmandatory Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                        Throw New Exception("Please enter Batch No for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colManufactureDate).Value) <= 0 Then
                        Throw New Exception("Please enter Manufacture Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colExpiry).Value) <= 0 Then
                        Throw New Exception("Please enter Expiry Date for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If clsCommon.GetDateWithStartTime(clsCommon.myCDate(gv1.Rows(ii).Cells(colManufactureDate).Value)) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(gv1.Rows(ii).Cells(colExpiry).Value)) Then
                        Throw New Exception("Please enter Expiry Date greater than Manufacturing Date in " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If


                If clsCommon.myLen(strICode) > 0 AndAlso Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                        Dim strInICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strInUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        Dim strInnerReqNo As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colOrderNo).Value)

                        If clsCommon.CompairString(strICode, strInICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqNo, strInnerReqNo) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInUOM) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal Then
                            Throw New Exception("Item Code " + strICode + " and Unit " + strUOM + " is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1))
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
                    If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOMInner, strUOM) = CompairStringResult.Equal Then
                        dblEnteredQty += dblQtyInner
                    End If
                Next
                dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                If dblEnteredQty > dblBalQty Then ' AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then
                    Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    Return False
                End If

                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        Throw New Exception("Please provice serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If


                If dblQty > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                    If RunBatchFifowise = 1 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        OpenBatchItem()
                    End If
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
            Next

            For ii As Integer = 0 To gvAC.Rows.Count - 1
                If clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0 Then
                    For jj As Integer = 0 To gvAC.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colACCode).Value)) = CompairStringResult.Equal Then
                            Throw New Exception("Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If
                    Next
                End If
            Next



            'UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            Dim intx As Integer = 0

            'Return True
        Catch ex As Exception
            If blnReverse = False Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Return False
            Else
                Throw New Exception(ex.Message)
                Return False
            End If

        End Try
        Return True
    End Function
    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        Try

            If clsCommon.CompairString(cmbDisItemType.SelectedValue, "") = CompairStringResult.Equal Then
                cmbDisItemType.Focusable = True
                Throw New Exception("Please Select Item Type.")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select Customer")
            End If
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                txtBillToLocation.Focus()
            Else
                SelectBookingItems()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub BlankAllControls()
        txtOPKM.Value = 0
        txtCLKM.Value = 0
        lblTaxGroupScheme.Text = ""
        txtSchemeTaxGroup.Value = ""
        cmbDisItemType.SelectedValue = ""
        cmbDisItemType.Enabled = True
        txtBillToLocation.Enabled = True

        strOrginalCust = ""

        blnChangeCustomer = False
        txtGross_Wt.Text = Nothing
 
        lblFreightCharges.Text = 0
        lblFreightCharges.Tag = Nothing
        lblTotalWtMetric.Text = 0
        txtTransporterCode.Value = ""
        lblTransporterName.Text = ""

    
        txtRoadPermitDate.Checked = False
        txtRoadPermitDate.Value = clsCommon.GETSERVERDATE()

        txtGRDate.Checked = False
        txtGRDate.Value = clsCommon.GETSERVERDATE()
    
        txtEWayBillNo.Text = ""
        txtEWayBillDate.Value = clsCommon.GETSERVERDATE()
        txtRoadPermitNo.Text = ""
        txtVehicleCapacity.Value = 0


      
        txtDiscAmt.Text = 0
        txtDiscPer.Text = 0
        lblDiscountAmt.Text = 0
        lblInvoiceDiscAmt.Text = 0
        chkDiscountOnRate.IsChecked = True
       
        txtDocNo.Value = ""
        txtDesc.Text = ""

        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
    
        'txtBillToLocation.Value = ""
        'lblBillToLocation.Text = ""
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))

        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""

        ParentCode = ""
        txtDesc.Text = ""
   
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value

        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lblTotRAmt1.Text = ""
        TxtRoundoff.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending

        txtVehicleCode.Value = ""
        lblVhicleNo.Text = ""
        txtGRNo.Text = ""
     
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
        txtEWayBillNo.Text = ""
        txtElecttefNo.Text = ""
        txtEWayBillDate.Checked = False
        txtEWayBillDate.Value = clsCommon.GETSERVERDATE()

        txtBokingProformaInvoice.Text = ""
        txtMannaulInvoiceNo.Value = 0
        TxtInvoiceManualNoWithPrefix.Text = ""
     
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub
    Function SaveData(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsBookingProformaInvoice()
      
            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                Throw New Exception("Please select Tax Group")
                txtTaxGroup.Focus()
                Return False
            End If
            obj.DO_Item_Type = cmbDisItemType.SelectedValue
        
            If txtGRDate.Checked Then
                obj.GR_Date = txtGRDate.Value
            End If
            If txtRoadPermitDate.Checked Then
                obj.RoadPermit_Date = txtRoadPermitDate.Value
            End If
          
            obj.Freight_Charges = lblFreightCharges.Text
            obj.Total_Item_WeightMetric = lblTotalWtMetric.Text       
            obj.Item_Tax_Type = Item_TaxType

            obj.BookingProformaNo = txtBokingProformaInvoice.Text
            obj.Booking_No = txtReqNo.Value

            obj.WayBillNo = txtEWayBillNo.Text
            obj.WayBillDate = txtEWayBillDate.Value
            obj.Road_Permit_No = txtRoadPermitNo.Text
            obj.RoundOffAmount = TxtRoundoff.Text
            obj.Vehicle_Capacity = txtVehicleCapacity.Value
            obj.Dispatch_Terms = ddlDispatchTerms.SelectedValue

            obj.Transport_Id = txtTransporterCode.Value
            obj.Transporter_Name = lblTransporterName.Text

            obj.Payment_Terms = txtPaymentTerms.Text

            If obj.HeadDisc_Per > 0 Then
                obj.HeadDisc_PerAmt = lblInvoiceDiscAmt.Text
                obj.HeadDisc_Amt = 0
            Else
                obj.HeadDisc_Amt = lblInvoiceDiscAmt.Text
                obj.HeadDisc_PerAmt = 0
            End If
            obj.Comments = txtComment.Text
            '----------------------------------------------------------------
            obj.Document_Code = txtDocNo.Value
            obj.Document_Date = txtDate.Value

            obj.Customer_Code = txtVendorNo.Value
            obj.Customer_Name = lblVendorName.Text

            obj.CashCustomer = txtManualCustomer.Text

            obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)

            obj.Bill_To_Location = txtBillToLocation.Value

            If ShowShipToPartyInDairyDispatch = 1 Then
                obj.Ship_To_Party = txtShipToLocation.Value
                obj.Ship_To_Location = ""
                obj.Ship_To_Party_Parent = clsCommon.myCstr(ParentCode)
            Else
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Ship_To_Party = ""
                obj.Ship_To_Party_Parent = ""
            End If

            If rbtn_Fresh.IsChecked = True Then
                obj.Trans_Type = "FS"
            Else
                obj.Trans_Type = "PS"
            End If
    
            obj.Description = txtDesc.Text
            obj.Tax_Group = txtTaxGroup.Value
            obj.Salesman_Code = txtSalesman.Value
            obj.Salesman_Name = lblSalesman.Text
       
            obj.Print_Discount_Amt = txtPrintDiscountAmt.Text
            'obj.Price_Group_Code = txtPriceGroupCode.Text
            Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtVendorNo.Value & "'", trans)
            obj.Price_Code = Price_code

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


            obj.Vehicle_Code = txtVehicleCode.Value
            obj.VehicleNo = lblVhicleNo.Text
            obj.GRNo = txtGRNo.Text


        
            obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
           
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


            obj.Arr = New List(Of clsBookingProformaInvoiceDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsBookingProformaInvoiceDetail()
                objTr.Is_CustomerChanged = IIf(blnChangeCustomer = True, 1, 0)
                objTr.OrgCustCOde = strOrginalCust
                objTr.Disc_Scheme_Amount = clsCommon.myCdbl(grow.Cells(Disc_Scheme_Amount).Value)
                objTr.Disc_Scheme_Code = clsCommon.myCstr(grow.Cells(Disc_Scheme_Code).Value)
                objTr.Disc_Scheme_Pers = clsCommon.myCdbl(grow.Cells(Disc_Scheme_Pers).Value)
                objTr.Disc_Scheme_Type = clsCommon.myCstr(grow.Cells(Disc_Scheme_Type).Value)
                objTr.Alter_UnitQty = clsCommon.myCdbl(grow.Cells(colAlterUnitQty).Value)
                objTr.Rate_UnitQty = clsCommon.myCdbl(grow.Cells(colRateUnitQty).Value)
                objTr.Customer_Code = txtVendorNo.Value
                objTr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                objTr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                objTr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                objTr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                objTr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                objTr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                objTr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)

                objTr.Total_Item_WeightMetric = clsCommon.myCdbl(grow.Cells(colItemWeightMetric).Value)
                objTr.RATE_UOM = clsCommon.myCstr(grow.Cells(colUnitRate).Value)
                objTr.Alternate_UOM = clsCommon.myCstr(grow.Cells(colUnitALter).Value)
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                objTr.Item_Group = clsCommon.myCstr(grow.Cells(colICodeGrp).Value)
                objTr.TAX_PAID = clsCommon.myCstr(grow.Cells(colTAX_PAID).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                'objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

                objTr.Free_Qty = clsCommon.myCdbl(grow.Cells(colFreeQty).Value)
                objTr.Crate = clsCommon.myCdbl(grow.Cells(colCrate).Value)
                objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnit).Value)

                If intDispatchfromDelivery = 0 Then
                    objTr.GatePass_No = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                Else
                    objTr.Delivery_Code = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                End If

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

                objTr.ItemwiseTaxCode = clsCommon.myCstr(grow.Cells(colItemwiseTaxCode).Value)
                objTr.Location = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                If objTr.Location Is Nothing OrElse clsCommon.myLen(objTr.Location) = 0 Then
                    objTr.Location = txtBillToLocation.Value
                End If

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



                Dim dblRate As Double = 0
                Dim dt As New DataTable()
                Dim strCustName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER  where Cust_Code='" & txtVendorNo.Value & "'", trans))

                Dim qry As String = "Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,Item_MRP,XXXE.TAX1_Rate, XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " & _
"XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7,XXXE.TAX8,XXXE.TAX9,XXXE.TAX10 from ( " & _
"Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
"Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
"Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,Item_MRP ,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " & _
 "TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " & _
 "TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " & _
 "TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7,TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10 from TSPL_ITEM_PRICE_MASTER  left  outer join  " & _
"TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
"TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code    where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " & _
"TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & clsCommon.myCstr(grow.Cells(colUnit).Value) & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & clsCommon.myCstr(grow.Cells(colICode).Value) & "' AND Location_Code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'  " & _
") XXXE WHERE RowNo=1  "


                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                    If dt.Rows.Count > 0 Then
                        dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                        If dblRate = 0 Then
                            Throw New Exception("Please Fill Selling Price for Location " & txtBillToLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                            Exit Function
                        End If
                        'End If
                        objTr.MRP = clsCommon.myCdbl(dt.Rows(0).Item("Item_MRP"))
                        objTr.Price_Date = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                    Else
                        Throw New Exception("Please create Price chart for customer " & txtVendorNo.Value & " for Location " & txtBillToLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                        Exit Function
                    End If
                End If
                



                'objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value)
                objTr.Price_code = Price_code
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

                objTr.Commission_Rate = clsCommon.myCdbl(grow.Cells(colCommRate).Value)
                objTr.Commission_Party = clsCommon.myCstr(grow.Cells(ColCommParty).Value)
                objTr.Commission_Amt = clsCommon.myCdbl(grow.Cells(ColCommAmt).Value)
                objTr.Amt_Less_Commission = clsCommon.myCdbl(grow.Cells(ColAmtAfterCOmm).Value)

                objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
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

            '' CurrencConversion
            If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code, trans) = True Then
                obj.CURRENCY_CODE = Me.txtCurrencyCode.Value
                ''''obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
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
            If trans Is Nothing Then
                If (clsBookingProformaInvoice.SaveData(obj, isNewEntry, True)) Then
                    UcAttachment1.SaveData(obj.Document_Code)
                    If ChekPostBtn = False And IsDataImported = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If

            End If
            'Return True
        Catch ex As Exception
            If blnReverse = False Then
                If ChekPostBtn = False Then
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Else
                    Throw New Exception(ex.Message)
                End If
            Else
                Throw New Exception(ex.Message)
            End If
            Return False
        End Try
        Return True
    End Function
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsBookingProformaInvoice
                obj.Document_Code = clsCommon.myCstr(txtDocNo.Value)
                obj.GRNo = txtGRNo.Text
                obj.GR_Date = txtGRDate.Value
                obj.Road_Permit_No = txtRoadPermitNo.Text
                obj.RoadPermit_Date = txtRoadPermitDate.Value
                If txtGRDate.Checked Then
                    obj.GR_Date = txtGRDate.Value
                End If
                If txtRoadPermitDate.Checked Then
                    obj.RoadPermit_Date = txtRoadPermitDate.Value
                End If

                obj.EWayBillNo = txtEWayBillNo.Text
                obj.Electronic_Ref_No = txtElecttefNo.Text
                If txtEWayBillDate.Checked Then
                    obj.EWayBillDate = txtEWayBillDate.Value
                End If

                If clsBookingProformaInvoice.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            Dim obj As New clsBookingProformaInvoice()
            obj = clsBookingProformaInvoice.GetData(strCode, NavTyep, Nothing, True)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                
                cboItemType.Enabled = False
                txtBillToLocation.Enabled = False

                'rbtn_Ambient.Enabled = False
                'rbtn_Fresh.Enabled = False
               
                cmbDisItemType.Enabled = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                   
                End If

                cmbDisItemType.SelectedValue = obj.DO_Item_Type
                If obj.Trans_Type = "FS" Then
                    rbtn_Fresh.IsChecked = True
                Else
                    rbtn_Ambient.IsChecked = True
                End If
               
                TxtRoundoff.Text = obj.RoundOffAmount

                lblFreightCharges.Text = obj.Freight_Charges

                txtBokingProformaInvoice.Text = obj.BookingProformaNo
                txtReqNo.Value = obj.Booking_No

                txtPriceCode.Text = obj.Price_Code
                'txtPriceGroupCode.Text = obj.Price_Group_Code

                If clsCommon.myCdbl(obj.Freight_Charges) > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("FixedCharge", GetType(Decimal))
                    dt.Columns.Add("EmptyCharge", GetType(Decimal))
                    dt.Columns.Add("FreightCharge", GetType(Decimal))
                    dt.Columns.Add("FreightType", GetType(String))
                    Dim dr As DataRow = dt.NewRow()
                    dr("FreightType") = obj.Freight_Type
                    dr("FixedCharge") = obj.FixedCharge
                    dr("FreightCharge") = obj.Freight_Charges
                    dr("EmptyCharge") = obj.EmptyCharge

                    lblFreightCharges.Tag = dt
                Else
                    lblFreightCharges.Tag = Nothing
                End If
                ''======================================================================
                txtPaymentTerms.Text = obj.Payment_Terms

                lblTotalWtMetric.Text = obj.Total_Item_WeightMetric
                txtTransporterCode.Value = obj.Transport_Id
                lblTransporterName.Text = obj.Transporter_Name
                txtEWayBillNo.Text = obj.WayBillNo
                txtComment.Text = obj.Comments

                If (obj.Print_Discount_Amt <> 0) Then
                    lblPrintDisAmt.Visible = True
                    txtPrintDiscountAmt.Visible = True
                    txtPrintDiscountAmt.Text = clsCommon.myCdbl(obj.Print_Discount_Amt)
                Else

                    lblPrintDisAmt.Visible = False
                    txtPrintDiscountAmt.Visible = False
                    txtPrintDiscountAmt.Text = clsCommon.myCdbl(obj.Print_Discount_Amt)
                End If
                If clsCommon.myLen(obj.WayBillDate) > 0 Then
                    txtEWayBillDate.Value = obj.WayBillDate
                End If


                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Customer_Code)
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code

                txtDate.Value = obj.Document_Date
                txtVendorNo.Value = obj.Customer_Code
             
                txtDate.Enabled = False
                txtVendorNo.Enabled = False
                txtRoadPermitNo.Text = obj.Road_Permit_No

                lblVendorName.Text = obj.Customer_Name

                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group

                txtShipToLocation.Value = obj.Ship_To_Location
                lblShipToLocation.Text = obj.ShipToLocationName
              
            txtBillToLocation.Value = obj.Bill_To_Location

            Dim objTaxGrpMaster As New clsTaxGroupMaster()
            objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
            If (objTaxGrpMaster IsNot Nothing) Then
                lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
            End If
            If clsCommon.myLen(txtSchemeTaxGroup.Value) > 0 Then
                lblTaxGroupScheme.Text = clsTaxGroupMaster.GetNameOfSaleType(txtSchemeTaxGroup.Value, Nothing)
            End If


            cboItemType.SelectedValue = obj.Item_Type
                txtTermCode.Value = obj.Terms_Code

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

            lblTaxGrpName.Text = obj.TaxGroupName
            lblTermName.Text = obj.TermsName


            lblVhicleNo.Text = obj.VehicleNo
            txtVehicleCode.Value = obj.Vehicle_Code
            txtGRNo.Text = obj.GRNo
          

            txtEWayBillNo.Text = obj.EWayBillNo
                txtElecttefNo.Text = obj.Electronic_Ref_No

            If obj.EWayBillDate IsNot Nothing Then
                txtEWayBillDate.Value = obj.EWayBillDate
                txtEWayBillDate.Checked = True
            End If
     
                txtGRNo.Text = obj.GRNo
                txtEWayBillNo.Text = obj.EWayBillNo
                txtElecttefNo.Text = obj.Electronic_Ref_No
                If obj.EWayBillDate IsNot Nothing Then
                    txtEWayBillDate.Value = obj.EWayBillDate
                    txtEWayBillDate.Checked = True
                End If
                If obj.GR_Date IsNot Nothing Then
                    txtGRDate.Value = obj.GR_Date
                    txtGRDate.Checked = True
                End If
                If obj.RoadPermit_Date IsNot Nothing Then
                    txtRoadPermitDate.Value = obj.RoadPermit_Date
                    txtRoadPermitDate.Checked = True
                End If

            txtSalesman.Value = obj.Salesman_Code
            lblSalesman.Text = obj.Salesman_Name

            txtMannaulInvoiceNo.Value = obj.Mannual_Invoice_No
            TxtInvoiceManualNoWithPrefix.Text = obj.InvoiceManualNowithPrefix
            '
            intDispatchfromDelivery = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select DairyDispatchFromDO from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")) = 0, 0, 1)
           



            
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
          
            ddlDispatchTerms.SelectedValue = obj.Dispatch_Terms
          
            txtVehicleCapacity.Value = obj.Vehicle_Capacity

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
                    For Each objTr As clsBookingProformaInvoiceDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Disc_Scheme_Amount).Value = objTr.Disc_Scheme_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Disc_Scheme_Code).Value = objTr.Disc_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Disc_Scheme_Pers).Value = objTr.Disc_Scheme_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Disc_Scheme_Type).Value = objTr.Disc_Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAlterUnitQty).Value = objTr.Alter_UnitQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRateUnitQty).Value = objTr.Rate_UnitQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = objTr.Cash_Scheme_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = objTr.Cash_Scheme_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeCode).Value = objTr.Cash_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = objTr.Cash_Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = objTr.Scheme_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = objTr.Scheme_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = objTr.Scheme_Item_UOM


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeightMetric).Value = objTr.Total_Item_WeightMetric

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = objTr.RATE_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitALter).Value = objTr.Alternate_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type 'RowTypeItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = objTr.Item_Group
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_PAID).Value = objTr.TAX_PAID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)

                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = objTr.so_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCrate).Value = objTr.Crate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objTr.Balance_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreeQty).Value = objTr.Free_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.OrgUnit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        If intDispatchfromDelivery = 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = obj.Booking_No
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = obj.Booking_No
                        End If
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaDDisPer).Value = objTr.HeadDiscPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDisPerAmt).Value = objTr.HeadDiscPerAmt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objTr.ItemwiseTaxCode

                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If

                        If intDispatchfromDelivery = 0 Then
                            If clsCommon.myLen(objTr.GatePass_No) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = GetBalanceDeliveryQty(objTr.GatePass_No, objTr.Item_Code)
                            End If
                        Else
                            If clsCommon.myLen(objTr.Delivery_Code) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = GetBalanceDeliveryQty(objTr.Delivery_Code, objTr.Item_Code)
                            End If
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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommRate).Value = objTr.Commission_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value = objTr.Commission_Party
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommPartyName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommAmt).Value = objTr.Commission_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmtAfterCOmm).Value = objTr.Amt_Less_Commission
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBooking_User_Code).Value = objTr.Booking_User_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDistributor_Retailer_Code).Value = objTr.Distributor_Retailer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDistributor_Retailer_Name).Value = objTr.Distributor_Retailer_Name

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
                'LoadParentSHipCode()
            End If
            SetitemWiseTaxOnlySetting()

                RefreshReqNo()
            ' ''RefreshGRPNo()
                UcAttachment1.LoadData(obj.Document_Code)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (myMessages.deleteConfirm()) Then
            DeleteData()
        End If
    End Sub
    Private Sub txtTransporterCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporterCode._MYValidating
        Dim qry As String = "select Transport_Id,Transporter_Name from TSPL_TRANSPORT_MASTER"
        txtTransporterCode.Value = clsCommon.ShowSelectForm("DSTransport No", qry, "Transport_Id", "", txtTransporterCode.Value, "Transport_Id", isButtonClicked)
        lblTransporterName.Text = connectSql.RunScalar("Select Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id = '" + Convert.ToString(txtTransporterCode.Value) + "'")

    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue("Proforma Invoice not found to Print")
        Else
            ' Dim Qry As String = ""
            Dim dtDocdate As Date?
            Dim Qry As String = "Select  * from (  select  isnull(TSPL_DAIRY_PROFORMA_INVOICE_detail .Total_Tax_Amt,0) as Detail_Total_Tax_Amt,TSPL_CITY_MASTER_For_Location .City_Name as Loc_City_Name , TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Customer_Code as Bill_To_Party_Code  ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.VehicleNo  ,TSPL_CUSTOMER_MASTER_Ship_To_Location.PAN as Ship_To_Party_Pan, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Party , TSPL_CUSTOMER_MASTER_Ship_To_Location.Customer_Name as Ship_to_Party_Cust_Name , TSPL_CUSTOMER_MASTER_Ship_To_Location.Add1  as Ship_to_Party_Cust_Add1,TSPL_CUSTOMER_MASTER_Ship_To_Location.Add2 as Ship_to_Party_Cust_Add2, TSPL_CUSTOMER_MASTER_Ship_To_Location.Add3 as Ship_to_Party_Cust_Add3,TSPL_STATE_MASTER_Ship_To_Location.GST_STATE_Code as Ship_To_Party_GSNIN_State_Code, TSPL_CUSTOMER_MASTER_Ship_To_Location.GSTNO as Ship_to_Party_GSTIN ,  case when  TSPL_DAIRY_PROFORMA_INVOICE_HEAD.HeadDisc_Per >0 then TSPL_DAIRY_PROFORMA_INVOICE_HEAD.HeadDisc_PerAmt else TSPL_DAIRY_PROFORMA_INVOICE_HEAD.HeadDisc_Amt end as Invoice_Disc,'' as Bank_Name,'' as IFSC_Code,'' as BANKACCNUMBER ,  TSPL_DAIRY_PROFORMA_INVOICE_detail.Line_No, TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Net_Amt,isnull(TSPL_DAIRY_PROFORMA_INVOICE_detail.Total_Disc_Amt,0) as Disc_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.Scheme_Item , Location_State.gst_state_code as Loc_GST_StateCode,Tspl_customer_master.gstno as CustGSTNo,TSPL_STATE_MASTER.gst_state_code,isnull(TSPL_STATE_MASTER.Is_GST_UT,0) as Is_GST_UT ,tspl_location_master.gstno as LocGstNo, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.is_taxable, COALESCE(TSPL_ITEM_MASTER.HSN_CODE, TAC.SAC_Code) AS HSN_CODE, isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Nine_NR_No,'') as Nine_NR_No, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Comments as Remarks, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.RoundOffAmount as Roundoff,CASE WHEN TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Transporter_Name = '' THEN TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Transporter_Name_Manual ELSE  TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Transporter_Name END AS Transporter_Name, TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end    as Location_Address_GP, cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.Mode_of_Trans,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range,   TSPL_ITEM_master.ITF_CODE,Location_State.STATE_NAME as Loc_State_Name,TSPL_CITY_MASTER.City_Name,Location_State.state_code as Loc_state_code, tspl_location_master.HOAdd1 ,TSPL_LOCATION_MASTER .HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Transport_Id,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.GRNo,case when TSPL_DAIRY_PROFORMA_INVOICE_HEAD.GRNo <> '' then convert(varchar,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.GR_Date,103) else '' end as GR_Date  ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.HeadDisc_Amt  , TSPL_ITEM_master.ITF_CODE as Chap_Desc,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.HeadDisc_PerAmt,TSPL_LOCATION_MASTER.Registration_Number, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.PaymentTerm as Payment_Terms, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Modify_By,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Road_Permit_No,TSPL_ITEM_MASTER.Cheapter_Heads,convert(date,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.RoadPermit_Date,103) as RoadPermit_Date , TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_For_Location.City_Name)>0 then ', '+TSPL_CITY_MASTER_For_Location .City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_State.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_State.STATE_NAME else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code  as varchar)  else ' ' end  + case when len(TSPL_LOCATION_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_LOCATION_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_LOCATION_MASTER.Email    )>0 then ',Email - '+ TSPL_LOCATION_MASTER.Email else '' end  as Location_Address,TSPL_LOCATION_MASTER.CST_No as Loc_CSTNo,TSPL_LOCATION_MASTER.Excisable as loc_Excisable,TSPL_LOCATION_MASTER.Range_Address as Loc_range_Add,TSPL_LOCATION_MASTER.Division_Address  as loc_Division_Address,TSPL_LOCATION_MASTER.Commissionerate  as Loc_Commissionerate ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Challan_No ,case when TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Challan_No <> '' then  convert(varchar,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Challan_Date,103) else '' end as Challan_Date,   TSPL_DAIRY_PROFORMA_INVOICE_HEAD.total_add_charge ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD. WayBillNo,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address, TSPL_LOCATION_MASTER .Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_ADd2,TSPL_LOCATION_MASTER.Add3  as Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,Case when ISNULL(TSPL_LOCATION_MASTER.Telphone,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Telphone end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone1,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone1 Else'' End as  Loc_Phn,TSPL_LOCATION_MASTER.Email as Loc_Email  , TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Discount_Base,case when len(isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Road_Permit_No,''))>0 then TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Road_Permit_No else TSPL_DAIRY_PROFORMA_INVOICE_HEAD.GRNo end AS Dis_Doc_No,case when isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Description,'')<>'' then TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Description else   TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Description end as Description ,isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Print_Discount_Amt,0) as 'Print_Discount_Amt', TSPL_COMPANY_MASTER .State as Comp_State, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Cust_PO_No as Buyer_order_no,case when TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Cust_PO_No <> '' then convert(varchar,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Cust_PO_Date,103) else '' end  as Buyer_order_date,case when (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Dispatch_Terms )='FE' then 'Freight Extra' else  TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Dispatch_Terms  end  as Terms_of_delivery, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Location ,TSPL_SHIP_TO_LOCATION.State as Con_statecode, TSPL_SHIP_TO_LOCATION.Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1 as STL_Add1, TSPL_SHIP_TO_LOCATION.Add2 as STL_Add2, TSPL_SHIP_TO_LOCATION.Add3 as STL_Add3,  TSPL_SHIP_TO_LOCATION.Telphone as STL_Phone, TSPL_SHIP_TO_LOCATION.Email  as STL_email, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Code as InvoiceNo,convert(varchar,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Date,103) as Date_Time_Invoice,convert(varchar ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Date,103) as InvoiceDate  ,convert(varchar,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Date,103) as ShipmentDate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Terms_Code as TermCondition, TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone, TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode, TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3 ,   case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Add1 end as P_Add1 , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add2 end as P_Add2 , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Add3 end as P_Add3 , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Pin_Code  end as P_PinNo , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.CST_No   end as P_CstNo ,case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .State     when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .STATE_NAME   end as p_State , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust .Email  end as P_Email ,   case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Name  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Contact_Name , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Contact_Person_Phone  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Contact_Phone  , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Ship_To_Type_Code   end as P_CustCode , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.Customer_Name    end as P_Cust_Name , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.City_Name    end as P_City_Name , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_state_Master.STATE_NAME  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.STATE_NAME    end as P_State_Name , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER  .PAN	  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then '' end as P_Pan_No,  case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Name   when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Person    end as ShipTo_Contact_Person  ,  case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.Contact_Person_Phone  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Contact_Per_Phone end as ShipTo_Contact_Per_Phone  , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.PAN  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_Pan end as ShipTo_Pan , case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.GSTNO end as ShipTo_GSTNO, case when coalesce(ship_cust.Ship_To_Type_Code,'')='' then TSPL_STATE_MASTER.gst_state_code  when coalesce(ship_cust.Ship_To_Type_Code,'')<>'' then ship_cust.ShipTo_GstStateCode end as ShipTo_GstStateCode ,  TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name,TSPL_CUSTOMER_MASTER.PAN  as Customer_Pan,TSPL_CUSTOMER_MASTER.Contact_Person_Email as Cust_Contact_Person_Email,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Person_Phone,  TSPL_DAIRY_PROFORMA_INVOICE_detail.item_code + case when TSPL_DAIRY_PROFORMA_INVOICE_detail.Scheme_Item='Y' then ' (Free Scheme)' when TSPL_DAIRY_PROFORMA_INVOICE_detail.Scheme_Item='N' then '' end as item_code,COALESCE(TSPL_ITEM_MASTER.Item_Desc,TAC.DESCRIPTION) + case when TSPL_DAIRY_PROFORMA_INVOICE_detail.Scheme_Item='Y' then '  ( Free Scheme Not For Sale )' when TSPL_DAIRY_PROFORMA_INVOICE_detail.Scheme_Item='N' then '' end   as itemdesc, TSPL_ITEM_MASTER.Is_batch_Item, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then isnull(TSPL_BATCH_ITEM.Qty,0) else TSPL_DAIRY_PROFORMA_INVOICE_detail.Qty end ) as qty   ,TSPL_DAIRY_PROFORMA_INVOICE_detail.mrp, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then  isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_DAIRY_PROFORMA_INVOICE_detail.item_cost ELSE TSPL_DAIRY_PROFORMA_INVOICE_detail.Amount end) as amount,(case when TSPL_ITEM_MASTER.Is_batch_Item=1 then  ((isnull(TSPL_BATCH_ITEM.Qty,0)*TSPL_DAIRY_PROFORMA_INVOICE_detail.item_cost)/TSPL_DAIRY_PROFORMA_INVOICE_detail.Total_Basic_Amt)* isnull(TSPL_DAIRY_PROFORMA_INVOICE_detail.Total_Disc_Amt,0) else isnull(TSPL_DAIRY_PROFORMA_INVOICE_detail.Total_Disc_Amt,0) end) as Disc_Item_Amt,TSPL_DAIRY_PROFORMA_INVOICE_detail.Amt_Less_Discount, (case when TSPL_ITEM_MASTER.Is_batch_Item=1 then TSPL_BATCH_ITEM.UOM else TSPL_DAIRY_PROFORMA_INVOICE_detail.unit_code END) as uom,isnull(TSPL_BATCH_ITEM.Batch_No,'') as Batch_No ,TSPL_DAIRY_PROFORMA_INVOICE_detail.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax2_amt,0) as txt2amt,    tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax4_amt,0) as txt4amt, tax5.Tax_Code_Desc as tax5name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax5_amt,0) as txt5amt,  tax6.Tax_Code_Desc as tax6name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax6_amt,0) as txt6amt,  tax7.Tax_Code_Desc as tax7name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax8_amt,0) as txt8amt,  tax9.Tax_Code_Desc as tax9name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax10_amt,0) as txt10amt,  TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX1_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX2_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX3_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX4_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX5_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX6_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX7_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX8_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX9_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX10_Rate,TSPL_DAIRY_PROFORMA_INVOICE_detail.Disc_Per, isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  , TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1 as dTAX1, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2 as dTAX2, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3 as  dTAX3, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4 as  dTAX4, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5 as  dTAX5, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6 as  dTAX6, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7 as  dTAX7, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8 as dTAX8, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9 as dTAX9, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10 as  dTAX10, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10_Amt,  TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1_Rate as dTAX1_Rate, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2_Rate as dTAX2_Rate, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3_Rate as dTAX3_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4_Rate as dTAX4_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5_Rate as dTAX5_Rate  ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6_Rate as dTAX6_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7_Rate as dTAX7_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8_Rate as dTAX8_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9_Rate as dTAX9_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,  isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.BookingProformaNo AS performance_invoice_no , TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Salesman_Name as SalePerson,ShimentDetail.Item_Selling_Price as PriceRate  , ShimentDetail.Conversion_Factor,ShimentDetail.kG_Conv_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No "
            Qry += " from TSPL_DAIRY_PROFORMA_INVOICE_HEAD left outer join TSPL_DAIRY_PROFORMA_INVOICE_detail on TSPL_DAIRY_PROFORMA_INVOICE_head.Document_Code =TSPL_DAIRY_PROFORMA_INVOICE_detail.DOCUMENT_CODE  "
            Qry += " left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No  = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No "
            Qry += " left outer join (select * from (select  TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code,TSPL_DAIRY_PROFORMA_INVOICE_detail.document_code,max(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as Item_Selling_Price  ,max(case when TSPL_ITEM_UOM_DETAIL.Default_UOM=1 then TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Conversion_Factor,max(case when TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' then Conversion_Factor end) kG_Conv_Rate ,TSPL_ITEM_PRICE_MASTER.start_date ,max(TSPL_ITEM_PRICE_MASTER.Price_Code ) as Price_Code "
            Qry += " from TSPL_DAIRY_PROFORMA_INVOICE_detail left outer join TSPL_ITEM_PRICE_MASTER on  TSPL_ITEM_PRICE_MASTER.Price_code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_code and TSPL_ITEM_PRICE_MASTER.Start_Date=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_Date and TSPL_DAIRY_PROFORMA_INVOICE_detail.item_code=tspl_item_price_master.item_code   and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Location "
            Qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  where TSPL_ITEM_PRICE_MASTER.UOM='KG' and Start_Date<='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and TSPL_ITEM_PRICE_MASTER.UOM='KG' and DOCUMENT_CODE='" & txtDocNo.Value & "' group by DOCUMENT_CODE,TSPL_ITEM_PRICE_MASTER.Item_Code,Start_Date,TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code )xx )ShimentDetail "
            Qry += " on ShimentDetail.document_code=TSPL_DAIRY_PROFORMA_INVOICE_HEAD.document_code and ShimentDetail.Item_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code and ShimentDetail.start_date=  TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_Date and ShimentDetail.Price_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_code  "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code  "
            Qry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.comp_code  "
            Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Customer_Code   "
            Qry += " LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,Contact_Person_Email,Contact_Person_Name,Contact_Person_Phone from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE   ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'  left join (select Ship_To_Code,Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1,TSPL_SHIP_TO_LOCATION.Add2,TSPL_SHIP_TO_LOCATION.Add3,TSPL_SHIP_TO_LOCATION.Add4,TSPL_SHIP_TO_LOCATION.City_Code,Ship_To_Type_Code,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_SHIP_TO_LOCATION.CST_No,TSPL_SHIP_TO_LOCATION.Email,TSPL_SHIP_TO_LOCATION.Telphone,TSPL_STATE_MASTER.STATE_NAME as STATE_NAME,TSPL_CITY_MASTER.City_Name,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SHIP_TO_LOCATION.Contact_Person_Name as ShipTo_Contact_Person, TSPL_SHIP_TO_LOCATION.Contact_Person_Phone as ShipTo_Contact_Per_Phone,TSPL_SHIP_TO_LOCATION.PAN AS ShipTo_Pan,TSPL_STATE_MASTER.GST_STATE_CODE AS ShipTo_GstStateCode,TSPL_SHIP_TO_LOCATION.GSTNO from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code left outer join TSPL_STATE_MASTER on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER.STATE_CODE) ship_cust on ship_cust.Ship_To_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Code     "
            Qry += " and ship_cust.Ship_To_Code=TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Location "
            Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State   "
            Qry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code  left join TSPL_Additional_Charges TAC ON TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code=TAC.Code LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_DAIRY_PROFORMA_INVOICE_head.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_DAIRY_PROFORMA_INVOICE_head.tax2    left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_head .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_DAIRY_PROFORMA_INVOICE_head .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_head .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX7    left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX9    left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX10   left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX10 "
            Qry += " left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.Unit_code "
            Qry += " left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code  "
            Qry += " and   Uom_Detail.UOM_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.unit_code "
            Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state  left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state "
            Qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Location   "
            Qry += " left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State    LEFT OUTER JOIN TSPL_BATCH_ITEM ON TSPL_BATCH_ITEM.Document_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.DOCUMENT_CODE and TSPL_BATCH_ITEM.Item_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.Item_Code and TSPL_BATCH_ITEM.Parent_Line_No =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.Line_No"
            Qry += " AND TSPL_BATCH_ITEM.Document_Type='PI-SH'  where TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Code in ('" & txtDocNo.Value & "')) XXX "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)


            '''' work done on multiple level batch system on print for against ticket no . ALF/29/05/18-000069 

            Dim qry1 As String = "Select  * from (  select  TSPL_ITEM_MASTER.hsn_code,TSPL_DAIRY_PROFORMA_INVOICE_detail.Amt_Less_Discount,isnull(TSPL_DAIRY_PROFORMA_INVOICE_detail .Total_Tax_Amt,0) as Detail_Total_Tax_Amt ,TSPL_DAIRY_PROFORMA_INVOICE_detail.item_cost as itemcost, tax1.Tax_Code_Desc as tax1name,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax2_amt,0) as txt2amt,    tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax4_amt,0) as txt4amt, tax5.Tax_Code_Desc as tax5name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax5_amt,0) as txt5amt,  tax6.Tax_Code_Desc as tax6name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax6_amt,0) as txt6amt,  tax7.Tax_Code_Desc as tax7name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax8_amt,0) as txt8amt,  tax9.Tax_Code_Desc as tax9name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.tax10_amt,0) as txt10amt,  TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX1_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX2_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX3_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX4_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX5_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX6_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX7_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX8_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX9_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.TAX10_Rate,TSPL_DAIRY_PROFORMA_INVOICE_detail.Disc_Per, isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Total_Amt as Total_Amt, '1' as CopyType ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name1,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name2,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name3,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name4,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name5,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name6,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name7,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name8,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name9,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Name10,isnull (TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  , TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1 as dTAX1, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2 as dTAX2, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3 as  dTAX3, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4 as  dTAX4, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5 as  dTAX5, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6 as  dTAX6, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7 as  dTAX7, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8 as dTAX8, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9 as dTAX9, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10 as  dTAX10, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9_Amt, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10_Amt,  TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX1_Rate as dTAX1_Rate, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX2_Rate as dTAX2_Rate, TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX3_Rate as dTAX3_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX4_Rate as dTAX4_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX5_Rate as dTAX5_Rate  ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX6_Rate as dTAX6_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX7_Rate as dTAX7_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX8_Rate as dTAX8_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX9_Rate as dTAX9_Rate ,TSPL_DAIRY_PROFORMA_INVOICE_detail.TAX10_Rate as dTAX10_Rate,  dtax1.Type as tax1Type,dtax2.Type as tax2Type,dtax3.Type as tax3Type,dtax4.Type as tax4Type,dtax5.Type as tax5Type,dtax6.Type as tax6Type,dtax7.Type as tax7Type,dtax8.Type as tax8Type,dtax9.Type as tax9Type,dtax10.Type as tax10Type,  isnull(TSPL_DAIRY_PROFORMA_INVOICE_HEAD.RoundOffAmount,0) as RoundOffAmount ,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.BookingProformaNo AS performance_invoice_no , TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Salesman_Name as SalePerson,ShimentDetail.Item_Selling_Price as PriceRate  , ShimentDetail.Conversion_Factor,ShimentDetail.kG_Conv_Rate,TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No  "
            qry1 += " from TSPL_DAIRY_PROFORMA_INVOICE_HEAD left outer join TSPL_DAIRY_PROFORMA_INVOICE_detail on TSPL_DAIRY_PROFORMA_INVOICE_head.Document_Code =TSPL_DAIRY_PROFORMA_INVOICE_detail.DOCUMENT_CODE   left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No  = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Booking_No  left outer join (select * from (select  TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code,TSPL_DAIRY_PROFORMA_INVOICE_detail.document_code,max(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as Item_Selling_Price  ,max(case when TSPL_ITEM_UOM_DETAIL.Default_UOM=1 then TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as Conversion_Factor,max(case when TSPL_ITEM_UOM_DETAIL.UOM_Code='KG' then Conversion_Factor end) kG_Conv_Rate ,TSPL_ITEM_PRICE_MASTER.start_date ,max(TSPL_ITEM_PRICE_MASTER.Price_Code ) as Price_Code  from TSPL_DAIRY_PROFORMA_INVOICE_detail left outer join TSPL_ITEM_PRICE_MASTER on  TSPL_ITEM_PRICE_MASTER.Price_code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_code and TSPL_ITEM_PRICE_MASTER.Start_Date=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_Date and TSPL_DAIRY_PROFORMA_INVOICE_detail.item_code=tspl_item_price_master.item_code   and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Location  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code  where TSPL_ITEM_PRICE_MASTER.UOM='KG' and Start_Date<='31/May/2018' and TSPL_ITEM_PRICE_MASTER.UOM='KG' and DOCUMENT_CODE='" & txtDocNo.Value & "' group by DOCUMENT_CODE,TSPL_ITEM_PRICE_MASTER.Item_Code,Start_Date,TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code )xx )ShimentDetail  on ShimentDetail.document_code=TSPL_DAIRY_PROFORMA_INVOICE_HEAD.document_code and ShimentDetail.Item_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code and ShimentDetail.start_date=  TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_Date and ShimentDetail.Price_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Price_code   left outer join TSPL_LOCATION_MASTER on TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code   left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.comp_code   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Customer_Code    LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No,PAN as P_Pan,GSTNO as P_GST_No ,Contact_Person_Email,Contact_Person_Name,Contact_Person_Phone from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE   ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'  left join (select Ship_To_Code,Ship_To_Desc,TSPL_SHIP_TO_LOCATION.Add1,TSPL_SHIP_TO_LOCATION.Add2,TSPL_SHIP_TO_LOCATION.Add3,TSPL_SHIP_TO_LOCATION.Add4,TSPL_SHIP_TO_LOCATION.City_Code,Ship_To_Type_Code,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_SHIP_TO_LOCATION.CST_No,TSPL_SHIP_TO_LOCATION.Email,TSPL_SHIP_TO_LOCATION.Telphone,TSPL_STATE_MASTER.STATE_NAME as STATE_NAME,TSPL_CITY_MASTER.City_Name,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SHIP_TO_LOCATION.Contact_Person_Name as ShipTo_Contact_Person, TSPL_SHIP_TO_LOCATION.Contact_Person_Phone as ShipTo_Contact_Per_Phone,TSPL_SHIP_TO_LOCATION.PAN AS ShipTo_Pan,TSPL_STATE_MASTER.GST_STATE_CODE AS ShipTo_GstStateCode,TSPL_SHIP_TO_LOCATION.GSTNO from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code left outer join TSPL_STATE_MASTER on TSPL_SHIP_TO_LOCATION.state =TSPL_STATE_MASTER.STATE_CODE) ship_cust on ship_cust.Ship_To_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Code      and ship_cust.Ship_To_Code=TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Location  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State    Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code  left join TSPL_Additional_Charges TAC ON TSPL_DAIRY_PROFORMA_INVOICE_detail.Item_Code=TAC.Code LEFT JOIN TSPL_SAC_MASTER SAC ON TAC.SAC_Code=SAC.Code  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_DAIRY_PROFORMA_INVOICE_head.tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_DAIRY_PROFORMA_INVOICE_head.tax2    left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_head .TAX3   left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_DAIRY_PROFORMA_INVOICE_head .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_head .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX6    left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX7    left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX8  left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX9    left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_head .TAX10   left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax1 left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.tax2    left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX3   left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax4   left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .tax5   left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX6    left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX7    left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX8  left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX9    left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL .TAX10  left outer join TSPL_ITEM_UOM_DETAIL  on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code  and   TSPL_ITEM_UOM_DETAIL.UOM_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.Unit_code  left outer join TSPL_ITEM_UOM_DETAIL as Uom_Detail  on Uom_Detail.Item_Code =TSPL_ITEM_MASTER.Item_Code   and   Uom_Detail.UOM_Code =TSPL_DAIRY_PROFORMA_INVOICE_DETAIL.unit_code  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Location on  TSPL_CITY_MASTER_For_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_State on TSPL_STATE_MASTER_For_State.STATE_CODE =TSPL_LOCATION_MASTER.state  left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads left outer join tspl_state_master as Location_State on Location_State.state_code=tspl_location_master.state  left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Location    left outer join TSPL_CUSTOMER_MASTER as  TSPL_CUSTOMER_MASTER_Ship_To_Location on  TSPL_CUSTOMER_MASTER_Ship_To_Location.Cust_Code = TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Ship_To_Party   left outer join TSPL_CITY_MASTER   as TSPL_CITY_MASTER_For_Ship_To_Location on  TSPL_CITY_MASTER_For_Ship_To_Location.City_Code =TSPL_LOCATION_MASTER.City_Code  left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_For_Ship_To_Location on TSPL_STATE_MASTER_For_Ship_To_Location.STATE_CODE =TSPL_LOCATION_MASTER.state   LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Ship_To_Location  ON TSPL_STATE_MASTER_Ship_To_Location.STATE_CODE  =TSPL_CUSTOMER_MASTER_Ship_To_Location.State   "
            qry1 += "  where TSPL_DAIRY_PROFORMA_INVOICE_HEAD.Document_Code in ('" & txtDocNo.Value & "')) XXX "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
            dtDocdate = clsCommon.myCDate(dt.Rows(0)("Date_Time_Invoice"))
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(dt.Rows(0)("Tax1name"), "IGST") = CompairStringResult.Equal Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt1, "rptProformaInvoiceIntrastate", "Proforma Invoice IGST", dtDocdate, "rptChildProductSaleInvoice_Interstate.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
            Else
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt1, "rptProformaInvoiceLocal", "Proforma Invoice", dtDocdate, "RptChlidProductInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
            End If
            frmCRV = Nothing
        End If

    End Sub
    
    Sub DeleteData()
        Try
            Dim Reason As String = ""
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
            If (clsBookingProformaInvoice.DeleteData(txtDocNo.Value)) Then
                saveCancelLog(Reason, "Delete", Nothing)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
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
    Sub SelectBookingItems()
        isInsideLoadData = True
        Dim frm As New frmPendingBookingDairySale()
        'frm. = cmbDisItemType.SelectedValue
        frm.TaxCode = IIf(cmbDisItemType.SelectedValue = "T", 1, 0)
        frm.FreshAndAmbiet = IIf(rbtn_Fresh.IsChecked = True, "FS", "PS")
        frm.VendorCode = txtVendorNo.Value
        frm.LocCode = txtBillToLocation.Value
        frm.strCurrCode = txtDocNo.Value
        frm.DairyProformaInvoice = True
        frm.ShowDialog()
        LoadBlankGrid()

        Dim objOrderHead As clsBookingEntryDairySale = Nothing
        If frm.ArrProformaReturn IsNot Nothing AndAlso frm.ArrProformaReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrProformaReturn(0).Document_Code) > 0 Then
                objOrderHead = clsBookingEntryDairySale.GetData(frm.ArrProformaReturn(0).Document_Code, NavigatorType.Current)
                If objOrderHead IsNot Nothing AndAlso clsCommon.myLen(objOrderHead.Document_No) > 0 Then
                    txtVendorNo.Enabled = False
                    txtBillToLocation.Enabled = False
                    lblBillToLocation.Text = ""




                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objOrderHead.location_code
                    End If

                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                        chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                        strOrginalCust = txtVendorNo.Value = ""
                    End If
                    If objOrderHead.TRANSACTION_TYPE = "FS" Then
                        rbtn_Fresh.IsChecked = True
                        rbtn_Fresh.Enabled = False
                        rbtn_Ambient.Enabled = False
                    ElseIf objOrderHead.TRANSACTION_TYPE = "PS" Then
                        rbtn_Ambient.IsChecked = True
                        rbtn_Ambient.Enabled = False
                        rbtn_Fresh.Enabled = False
                    End If


                    strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'")) = "T", True, False)

                    LoadBlankGridAC()
                    SetTax()
                End If

            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            For Each obj As clsBookingProformaInvoiceDetail In frm.ArrProformaReturn
                gv1.Rows.AddNew()


                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = obj.Document_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_PAID).Value = "No"
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsEmptyValue).Value = clsItemMaster.IsItemHaveEmptyValue(obj.Item_Code)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.OrgRate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = txtBillToLocation.Value
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = lblBillToLocation.Text
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = obj.Balance_Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = obj.OrgUnit_code

                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = obj.Price_Date
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = obj.Price_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = obj.Conv_Factor
                If AutoScheme = True Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                End If
                findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)
                txtBokingProformaInvoice.Text = frm.ProformaInvoiceNo

            Next
        End If
        SetitemWiseTaxSetting(False, False)
        For ii As Integer = 0 To gv1.RowCount - 1
            UpdateCurrentRow(ii)
        Next
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
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
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) = 0 Then
                qst = "select count(*) from tspl_dairy_proforma_invoice_head where Document_Code='" + txtDocNo.Value + "'"
            Else
                qst = "select count(*) from tspl_dairy_proforma_invoice_head where Document_Code='" + txtDocNo.Value + "' and tspl_dairy_proforma_invoice_head.Customer_Code in (" + strwherecls + ")"

            End If
        
            '-----------------------------------------------------
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

        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------
        Dim strDONo As String = Nothing

        Dim qry As String = "select tspl_dairy_proforma_invoice_head.Document_Code as Code, tspl_dairy_proforma_invoice_head.Booking_No, CONVERT(varchar(10)"
        qry += " , tspl_dairy_proforma_invoice_head.Document_Date,103)+' '+ CONVERT(varchar(5), tspl_dairy_proforma_invoice_head.Document_Date,114) as Date"
        qry += " , tspl_dairy_proforma_invoice_head.Customer_Code as [Customer Code], Customer_Name as Customer,tspl_dairy_proforma_invoice_head.Bill_To_Location as [Location Code], Location_Desc as [Location Name],tspl_dairy_proforma_invoice_head.Comments,tspl_dairy_proforma_invoice_head.Total_Amt as Amount, case when tspl_dairy_proforma_invoice_head.Status=0 then 'Pending' else 'Approved' end as [Status],Direct_Dispatch as [Direct Dispatch] from tspl_dairy_proforma_invoice_head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_dairy_proforma_invoice_head.Customer_Code left outer join  TSPL_LOCATION_MASTER on tspl_dairy_proforma_invoice_head.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code  "

        Dim whrClas As String = ""
        
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " tspl_dairy_proforma_invoice_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and tspl_dairy_proforma_invoice_head.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " tspl_dairy_proforma_invoice_head.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " tspl_dairy_proforma_invoice_head.Customer_Code in (" + strwherecls + ") "
        
        End If
    
        If intDispatchfromDelivery = 0 Then
            LoadData(clsCommon.ShowSelectForm("ProformaFind", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        Else
            LoadData(clsCommon.ShowSelectForm("ProformaFind", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        End If


    End Sub

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        ElseIf e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub
    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("DS-SHipSNOSaleman", qry, "Code", whrcls, txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtSalesman.Value + "' and Emp_type='Salesman'"))
    End Sub

End Class

