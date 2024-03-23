''Modify by Balwinder on 13-10-2011.
''Add Assessible and MRP in Grid
''18/06/2012---Updation by --[Pankaj kumar]-- Commented Grid Formating Event Code So that Grid Cell's Color Could Not be change on clicking 
'' Added By Abhishek as on 30 Nov 2012 3:56 Pm For Location Lock
'by vipin for some feild hidden on 07/03/2013
'-----BM00000003441
Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO
Imports System.Data.SqlClient


Public Class frmSaleOrderProductSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dblOutstandingAmount As Double = 0
    Dim AllowDifferentStateofChildCustomerOnPS As Integer = 0
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim EnableTCSRateValidityFrom01July2021 As Boolean = False
    Dim GSTStatus As Boolean = False
    Const colIHSN As String = "colIHSN"
    Dim IsLocationProductType As Integer
    Dim AllowWo_Outstanding As Boolean
    Dim vaddnew As String = "Y"
    Dim attachqry As String = ""
    Private StrSql As String
    Public StrDocNo As String
    Public strExcise As Boolean
    Private PurchaseOneItemOneVendor As Boolean = False
    Private blnBackCalculation As Boolean = False
    Private IsBatchMFDEXDmandatory As Boolean = False
    Private AutoScheme As Boolean = False
    Private ItemRateEditable As Boolean = False
    Private IsRemarksMandatory As Boolean = False
    Private ItemMRPEditable As Boolean = False
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"
    Const colConvQty As String = "colConvQty"
    Const colRateUnitQty As String = "colRateUnitQty"
    Const colOrgUnit As String = "COLORGUNIT"
    Const colOrgUnitRate As String = "colOrgUnitRate"
    Public intMRPwithabatement As Integer
    Private intApprovel_Required As Integer = 0
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colUnitALter As String = "colUnitALter"
    Const colUnitRate As String = "colUnitRate"
    Const colBOOK_QTY_UOM As String = "colBOOK_QTY_UOM"
    Const colTAX_PAID As String = "colTAX_PAID"
    Const colBOOK_Rate As String = "colBOOK_Rate"
    Const colBOOK_RATE_UOM As String = "colBOOK_RATE_UOM"
    Const colRTRate As String = "colRTRate"
    Const ColCommParty As String = "ColCommParty"
    Const ColCommPartyName As String = "ColCommPartyName"
    Const colCommRate As String = "colCommRate"
    Const ColCommAmt As String = "ColCommAmt"
    Const ColAmtAfterCOmm As String = "ColAmtAfterCOmm"

    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colICode As String = "COLICODE"
    Const colICodeGrp As String = "colICodeGrp"
    Const colIName As String = "COLINAME"
    Const colPriceDateColumn As String = "pricedate"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colOrgRequitionQty As String = "COLORIGPEQQTY"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colManualRate As String = "colManualRate"
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

    Const colBinNo As String = "colBinNo"
    Const colPricipleCode As String = "colPricipleCode"
    Const colPricipleDesc As String = "colPricipleDesc"
    Const colvendorCode As String = "colvendorCode"
    Const colvendorDesc As String = "colvendorDesc"
    Const colBatchNo As String = "BATCHNO"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colItemWeight As String = "colItemWeight"

    Const colConvF As String = "colConvF"
    Const colPurCost As String = "colPurCost"
    Const colOrgCost As String = "colOrgCost"
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
    Const colHeaDDisPer As String = "colHeaDDisPer"
    Const colHeadDisPerAmt As String = "colHeadDisPerAmt"
    Const colShipParty As String = "colShipParty"
    Const colShipPartyName As String = "colShipPartyName"
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
    Const colIsTaxOnBaseAmount As String = "colIsTaxOnBaseAmount"
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
    Const colReqistionNo As String = "REQNO"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colItemUsedINGRN As String = "USEDINGRN"
    Const colMRP As String = "MRP"
    Const ColActualBalQty As String = "ColActualBalQty"
    'Const colAssessableRate As String = "ASSESSABLERATE"
    'Const colAssessableAmount As String = "ASSESSABLEAMT"

    Const colLocationCode As String = "LOCATIONCODE"
    Const colLocationName As String = "LOCATIONNAME"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"
    'Const colTIsSurTax As String = "ISSURTAX"
    'Const colTSurTaxCode As String = "SURTAXCODE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
#End Region
    Public Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalesOrderProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub


    Private Sub frmPurchaseOrder_Invalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.InvalidateEventArgs)

    End Sub

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
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


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'coll.Add("Manual_Item_Cost", "decimal(18, 6) NULL")
        btnCopy.Enabled = False
        btnHistory.Enabled = False
        'gv1.Enabled = False
        AllowDifferentStateofChildCustomerOnPS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDifferentStateofChildCustomerOnPS, clsFixedParameterCode.AllowDifferentStateofChildCustomerOnPS, Nothing))
        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingPS & "'")) = 0, False, True)
        PurchaseOneItemOneVendor = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='PurchaseOneItemOneVendor'")) = 0, False, True)
        AutoScheme = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AutoSchemeOn & "'")) = 0, False, True)
        ItemRateEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemRateEditableOnSales & "'")) = 0, False, True)
        ItemMRPEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemMRPEditableOnSales & "'")) = 0, False, True)
        IsRemarksMandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsRemarksMandatoryOnCloseSaleOrder & "'")) = 0, False, True)
        IsRemarksMandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsRemarksMandatoryOnCloseSaleOrder & "'")) = 0, False, True)
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        EnableTCSRateValidityFrom01July2021 = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTCSRateValidityFrom01July2021, clsFixedParameterCode.EnableTCSRateValidityFrom01July2021, Nothing)) = 0, False, True)
        'SendSMSandEmail()
        SetMailRight()
        SetUserMgmtNew()

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        txtVendorNo.MendatroryField = True
        LoadModeOfTrasport()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")

        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()
        LoadBlankGridTax()
        LoadPOType()
        LoadItemType()
        LoadBlankGridAC()
        AddNew()
        SetLength()
        LoadDispatchTerms()
        LoadPaymentTerms()
        chkRateDefaultSetting.ToggleState = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, Nothing)) = 1, ToggleState.On, ToggleState.Off)

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
        intMRPwithabatement = clsDBFuncationality.getSingleValue("select IsMRPwithAbatement from TSPL_INV_PARAMETERS")
        IsBatchMFDEXDmandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsBatchNo_MFD_EXD_Mandatory from TSPL_inv_parameters")) = 0, False, True)
        blnBackCalculation = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsRateBackCalculation from TSPL_inv_parameters")) = 0, False, True)
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        IsLocationProductType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Location_Code) from TSPL_LOCATION_MASTER where Location_Code='" & txtBillToLocation.Value & "' and Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' and (GIT_Type='' or GIT_Type='N') and MCC_Type='N'  "))
        If IsLocationProductType = 0 Then
            txtBillToLocation.Value = ""
            lblBillToLocation.Text = ""
        End If
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
        'RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        'RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        'RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
        If clsCommon.myLen(StrDocNo) > 0 Then
            LoadData(StrDocNo, NavigatorType.Current)
        End If
        If AllowDifferentStateofChildCustomerOnPS = 1 Then
            chkSameBillShip.Visible = True
        Else
            chkSameBillShip.Visible = False
        End If
        RadMenuItem3.Visibility = ElementVisibility.Collapsed
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
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200
        cboModeOfTransport.MaxLength = 12
        cboPOType.MaxLength = 1
        cboItemType.MaxLength = 1
        txtPONo.MaxLength = 200

    End Sub

    Sub LoadPOType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Local"
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
        dr("Name") = "Open"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Specific"
        dt.Rows.Add(dr)

        cboPOType.DataSource = dt
        cboPOType.ValueMember = "Code"
        cboPOType.DisplayMember = "Name"
    End Sub

    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 1
        'cboItemType.Visible = True
        'RadLabel21.Visible = True
    End Sub

    Sub LoadModeOfTrasport()
        cboModeOfTransport.Items.Add("By Road")
        cboModeOfTransport.Items.Add("By Air")
        cboModeOfTransport.Items.Add("By Sea")
    End Sub

    Sub BlankAllControls()
        chkIsTaxable.Checked = False
        chkCommApply.Checked = False
        lblCommAmt.Text = "0"
        txtrate.ReadOnly = False
        ddlPaymentTerms.Enabled = False
        txtAdvance.Enabled = False
        txtrate.Value = 0
        'chkItemwise.Visible = False
        chkItemwise.Checked = False
        chkItemwise.ReadOnly = False
        txtAdvance.Value = 0
        txtCustPODate.Value = clsCommon.GETSERVERDATE()
        txtCustPODate.Checked = False
        txtCreditLimit.Text = ""
        txtVehicleCode.Value = ""
        lblVhicleNo.Text = ""
        txtVehicleCapacity.Text = 0
        ddlPaymentTerms.SelectedValue = ""
        ddlDispatchTerms.SelectedValue = ""
        txtDispatchPeriod.Value = 0
        txtRoadPermitNo.Text = ""
        txtCloseRemarks.Value = ""
        lblCloseRemarksdesc.Text = ""
        txtDiscPer.Text = 0
        txtDiscAmt.Value = 0
        lblInvoiceDiscAmt.Text = ""
        vaddnew = "N"
        chkclose.Checked = False
        vaddnew = "Y"
        txtPONo.Text = ""
        txtPriceGroupCode.Text = ""
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
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
        lblTotRAmt.Text = ""
        lblTotRAmt1.Text = ""

        cboModeOfTransport.Text = "By Road"
        cboPOType.SelectedValue = "L"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDeliveryDate.Value = txtDate.Value
        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 2
        txtReqNo.Value = ""
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        lblAmbendmentNoCaption.Visible = False
        lblAbandonmentNo.Text = ""
        btnAmendment.Enabled = False

        lblAddCharges1.Text = ""
        lblAddCharges1.Text = ""

        txtSalesman.Value = ""
        lblSalesman.Text = ""

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

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn
        Dim BOOK_Rate As New GridViewDecimalColumn
        Dim BOOK_QTY_UOM As New GridViewTextBoxColumn

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
        repoComplete.VisibleInColumnChooser = False
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
        repoRowType.IsVisible = True
        repoRowType.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoRowType)

        Dim repoICodeGrp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICodeGrp.FormatString = ""
        repoICodeGrp.HeaderText = "Item Group"
        repoICodeGrp.Name = colICodeGrp
        repoICodeGrp.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICodeGrp.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICodeGrp.Width = 100
        If chkItemwise.Checked Then
            repoICodeGrp.ReadOnly = True
        Else
            repoICodeGrp.ReadOnly = False
        End If
        gv1.MasterTemplate.Columns.Add(repoICodeGrp)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        'If chkItemwise.Checked Then
        '    repoICode.IsVisible = True
        'Else
        '    repoICode.IsVisible = False
        'End If
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
        repoIHSN.HeaderText = "HSN No/SAC Code"
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
        repoPriceDate.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoPriceDate)

        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCOde
        repoPriceCode.IsVisible = True
        repoPriceCode.ReadOnly = True
        repoPriceCode.IsVisible = False
        repoPriceCode.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Booking Qty"
        repoPendingQty.Name = colPendingQty
        'repoPendingQty.IsVisible = False
        'repoPendingQty.VisibleInColumnChooser = False
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
        repoBalQty.VisibleInColumnChooser = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoOrgPeqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgPeqQty.WrapText = True
        repoOrgPeqQty.ReadOnly = True
        repoOrgPeqQty.FormatString = ""
        repoOrgPeqQty.HeaderText = "Quotation Quantity"
        repoOrgPeqQty.Name = colOrgRequitionQty
        repoOrgPeqQty.Width = 80
        repoOrgPeqQty.Minimum = 0
        repoOrgPeqQty.IsVisible = False
        repoOrgPeqQty.VisibleInColumnChooser = False
        repoOrgPeqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPeqQty)


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
        BOOK_QTY_UOM.FormatString = ""
        BOOK_QTY_UOM.HeaderText = "Booking UOM"
        BOOK_QTY_UOM.Name = colBOOK_QTY_UOM
        BOOK_QTY_UOM.Width = 100
        BOOK_QTY_UOM.ReadOnly = True
        BOOK_QTY_UOM.HeaderImage = Global.ERP.My.Resources.Resources.search4
        BOOK_QTY_UOM.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.Columns.Add(BOOK_QTY_UOM)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Main UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
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

        Dim repoOrgUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrgUnit.FormatString = ""
        repoOrgUnit.HeaderText = "ORG UOM"
        repoOrgUnit.Name = colOrgUnit
        repoOrgUnit.Width = 80
        repoOrgUnit.ReadOnly = False
        repoOrgUnit.IsVisible = False
        repoOrgUnit.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoOrgUnit)

        Dim repoOrgUnitRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrgUnitRate.FormatString = ""
        repoOrgUnitRate.HeaderText = "ORG Rate UOM"
        repoOrgUnitRate.Name = colOrgUnitRate
        repoOrgUnitRate.Width = 80
        repoOrgUnitRate.ReadOnly = False
        repoOrgUnitRate.IsVisible = False
        repoOrgUnitRate.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoOrgUnitRate)


        Dim repoRTRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRTRate = New GridViewDecimalColumn()
        repoRTRate.FormatString = ""
        repoRTRate.HeaderText = "RT Rate"
        repoRTRate.Name = colRTRate
        repoRTRate.Width = 80
        repoRTRate.Minimum = 0
        repoRTRate.IsVisible = False
        repoRTRate.VisibleInColumnChooser = False
        repoRTRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRTRate)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "SO Qty(Main UOM)"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoConvQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConvQty = New GridViewDecimalColumn()
        repoConvQty.FormatString = ""
        repoConvQty.HeaderText = "Converted Qty"
        repoConvQty.Name = colConvQty
        repoConvQty.Width = 80
        repoConvQty.Minimum = 0
        repoConvQty.ShowUpDownButtons = False
        repoConvQty.Step = 0
        repoConvQty.IsVisible = False
        repoConvQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConvQty)

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
        repoMainUnitQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMainUnitQty)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = "{0:n2}"
        repoRate.HeaderText = "Basic Rate(Rate UOM)"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)


        Dim repoManualRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoManualRate = New GridViewDecimalColumn()
        repoManualRate.FormatString = ""
        repoManualRate.HeaderText = "Maual Rate"
        repoManualRate.Name = colManualRate
        repoManualRate.Width = 80
        repoManualRate.Minimum = 0
        repoManualRate.ReadOnly = False
        repoManualRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoManualRate)

        Dim repoCommparty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCommparty.FormatString = ""
        repoCommparty.HeaderText = "Comm. Party"
        repoCommparty.Name = ColCommParty
        repoCommparty.HeaderImage = Global.ERP.My.Resources.Resources.search4
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


        Dim repoShipparty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShipparty.FormatString = ""
        repoShipparty.HeaderText = "Ship Party"
        repoShipparty.Name = colShipParty
        repoShipparty.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoShipparty.TextImageRelation = TextImageRelation.TextBeforeImage
        repoShipparty.Width = 50
        repoShipparty.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoShipparty)

        Dim repoShippartyName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShippartyName.FormatString = ""
        repoShippartyName.HeaderText = "Ship Party Name"
        repoShippartyName.Name = colShipPartyName
        'repoShippartyName.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoShippartyName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoShippartyName.Width = 100
        repoShippartyName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoShippartyName)
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
        repoSchemeApp.IsVisible = False
        repoSchemeApp.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoSchemeApp)

        Dim repoItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "Item Weight"
        repoItemWt.Name = colItemWeight
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.IsVisible = False
        repoItemWt.ReadOnly = False
        repoItemWt.IsVisible = False
        repoItemWt.VisibleInColumnChooser = False
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
        repoConv.VisibleInColumnChooser = False
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
        repoTotItemWt.VisibleInColumnChooser = False
        repoTotItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotItemWt)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.IsVisible = True
        repoMRP.VisibleInColumnChooser = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)


        TAX_PAID.FormatString = ""
        TAX_PAID.HeaderText = "Tax Paid"
        TAX_PAID.Name = colTAX_PAID
        TAX_PAID.Width = 100
        TAX_PAID.ReadOnly = False
        TAX_PAID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        TAX_PAID.DataSource = clsCSABooking.GetTaxPaid()
        TAX_PAID.ValueMember = "Code"
        TAX_PAID.DisplayMember = "Name"
        TAX_PAID.ReadOnly = True
        gv1.Columns.Add(TAX_PAID)

        BOOK_Rate.FormatString = ""
        BOOK_Rate.HeaderText = "Booking Rate"
        BOOK_Rate.Name = colBOOK_Rate
        BOOK_Rate.Width = 100
        BOOK_Rate.ReadOnly = True
        BOOK_Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BOOK_Rate)



        BOOK_RATE_UOM.FormatString = ""
        BOOK_RATE_UOM.HeaderText = "Rate UOM"
        BOOK_RATE_UOM.Name = colBOOK_RATE_UOM
        BOOK_RATE_UOM.Width = 100
        BOOK_RATE_UOM.ReadOnly = True
        BOOK_RATE_UOM.IsVisible = False
        BOOK_RATE_UOM.HeaderImage = Global.ERP.My.Resources.Resources.search4
        BOOK_RATE_UOM.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.Columns.Add(BOOK_RATE_UOM)

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

        Dim repoQtySchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeItem.AllowSort = False
        repoQtySchemeItem.HeaderText = "Qty Scheme Item"
        repoQtySchemeItem.Name = colSchemeItem
        repoQtySchemeItem.ReadOnly = True
        repoQtySchemeItem.Width = 96
        repoQtySchemeItem.IsVisible = False
        repoQtySchemeItem.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoQtySchemeItem)

        Dim repoFromSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromSchemeCode.HeaderText = "Scheme Code"
        repoFromSchemeCode.Name = colFromSchemeCode
        repoFromSchemeCode.Width = 80
        repoFromSchemeCode.ReadOnly = True
        repoFromSchemeCode.IsVisible = False
        repoFromSchemeCode.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoFromSchemeCode)

        Dim repoDiscountAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountAmt.AllowSort = False
        repoDiscountAmt.HeaderText = "Discount"
        repoDiscountAmt.Name = colDiscountAmount
        repoDiscountAmt.ReadOnly = True
        repoDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDiscountAmt.Width = 52
        repoDiscountAmt.IsVisible = False
        'repoDiscountAmt.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoDiscountAmt)

        Dim repoCustDiscountQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscountQty.HeaderText = "Cash Dis Qty."
        repoCustDiscountQty.MinWidth = 4
        repoCustDiscountQty.Name = ColCustDiscountQty
        repoCustDiscountQty.ReadOnly = True
        repoCustDiscountQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscountQty.Width = 54
        repoCustDiscountQty.IsVisible = False
        repoCustDiscountQty.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscountQty)

        Dim repoCustDiscountPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscountPer.HeaderText = "Cash Dis %."
        repoCustDiscountPer.MinWidth = 4
        repoCustDiscountPer.Name = colCustDiscPercentage
        repoCustDiscountPer.ReadOnly = True
        repoCustDiscountPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscountPer.Width = 54
        repoCustDiscountPer.IsVisible = False
        repoCustDiscountPer.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscountPer)

        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.HeaderText = "Cash Dis Amt."
        repoCustDiscount.MinWidth = 4
        repoCustDiscount.Name = colcustDiscount
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscount.Width = 54
        repoCustDiscount.IsVisible = False
        repoCustDiscount.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)

        Dim repoCashSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCashSchemeCode.HeaderText = "Cash Scheme Code"
        repoCashSchemeCode.Name = colCashDiscSchemeCode
        repoCashSchemeCode.Width = 80
        repoCashSchemeCode.ReadOnly = True
        repoCashSchemeCode.IsVisible = False
        repoCashSchemeCode.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoCashSchemeCode)

        Dim repoAcualCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAcualCost.AllowSort = False
        repoAcualCost.HeaderText = "Net Price"
        repoAcualCost.Name = colActualCost
        repoAcualCost.ReadOnly = True
        repoAcualCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAcualCost.Width = 55
        repoAcualCost.IsVisible = False
        repoAcualCost.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoAcualCost)


        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.IsVisible = True
        repoAmt.VisibleInColumnChooser = True
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
        'repoAbatementRate.IsVisible = False
        'repoAbatementRate.VisibleInColumnChooser = False
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
        'repoAbatementamount.IsVisible = False
        'repoAbatementamount.VisibleInColumnChooser = False
        repoAbatementamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementamount)

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
        repoDisPer.IsVisible = False
        'repoDisPer.VisibleInColumnChooser = False
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.IsVisible = False
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repoDisAmt.VisibleInColumnChooser = False
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
        repoHDisPer.IsVisible = False
        'repoHDisPer.VisibleInColumnChooser = False
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
        repoHDisAmt.IsVisible = False
        'repoHDisAmt.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoHDisAmt)


        Dim repoHeadDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadDisAmt = New GridViewDecimalColumn()
        repoHeadDisAmt.FormatString = ""
        repoHeadDisAmt.HeaderText = "Head Disc Amt"
        repoHeadDisAmt.WrapText = True
        repoHeadDisAmt.Name = colHeadDiscamt
        repoHeadDisAmt.Width = 80
        repoHeadDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHeadDisAmt.IsVisible = False
        'repoHeadDisAmt.VisibleInColumnChooser = False
        repoHeadDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHeadDisAmt)

        Dim repoTotalMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalMRP.AllowSort = False
        repoTotalMRP.HeaderText = "Total MRP"
        repoTotalMRP.Name = colTotalMRP
        repoTotalMRP.ReadOnly = True
        repoTotalMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalMRP.Width = 62
        repoTotalMRP.IsVisible = False
        repoTotalMRP.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoTotalMRP)

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Basic Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
        repoTotalBasicAmt.IsVisible = False
        repoTotalBasicAmt.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoTotalBasicAmt)

        Dim repoTotalDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalDiscount.AllowSort = False
        repoTotalDiscount.HeaderText = "Total Discount"
        repoTotalDiscount.Name = colTotalDiscountAmount
        repoTotalDiscount.ReadOnly = True
        repoTotalDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalDiscount.Width = 81
        repoTotalDiscount.IsVisible = False
        'repoTotalDiscount.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoTotalDiscount)

        Dim repoTotalCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCustDiscount.HeaderText = "Total Cash Dis"
        repoTotalCustDiscount.Name = colTotalCustDiscount
        repoTotalCustDiscount.ReadOnly = True
        repoTotalCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCustDiscount.Width = 79
        repoTotalCustDiscount.IsVisible = False
        repoTotalCustDiscount.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoTotalCustDiscount)


        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 80
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.IsVisible = False
        ' repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)

        Dim repoPrincipleCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleCOde.FormatString = ""
        repoPrincipleCOde.HeaderText = "Principle Code"
        repoPrincipleCOde.Name = colPricipleCode
        repoPrincipleCOde.Width = 150
        repoPrincipleCOde.ReadOnly = True
        repoPrincipleCOde.IsVisible = False
        repoPrincipleCOde.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoPrincipleCOde)

        Dim repoPrincipleDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrincipleDesc.FormatString = ""
        repoPrincipleDesc.HeaderText = "Principle Desc"
        repoPrincipleDesc.Name = colPricipleDesc
        repoPrincipleDesc.Width = 150
        repoPrincipleDesc.ReadOnly = True
        repoPrincipleDesc.IsVisible = False
        repoPrincipleDesc.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoPrincipleDesc)

        Dim repoVCOde As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCOde.FormatString = ""
        repoVCOde.HeaderText = "Vendor Code"
        repoVCOde.Name = colvendorCode
        repoVCOde.Width = 150
        repoVCOde.ReadOnly = True
        repoVCOde.IsVisible = False
        repoVCOde.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoVCOde)

        Dim repoVDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVDesc.FormatString = ""
        repoVDesc.HeaderText = "Vendor Desc"
        repoVDesc.Name = colvendorDesc
        repoVDesc.Width = 150
        repoVDesc.ReadOnly = True
        repoVDesc.IsVisible = False
        repoVDesc.VisibleInColumnChooser = False
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


        Dim repoPurCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPurCost = New GridViewDecimalColumn()
        repoPurCost.FormatString = ""
        repoPurCost.HeaderText = "purchase Cost"
        repoPurCost.Name = colPurCost
        repoPurCost.WrapText = True
        repoPurCost.Width = 80
        repoPurCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPurCost.IsVisible = False
        repoPurCost.VisibleInColumnChooser = False
        repoPurCost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPurCost)

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


        Dim repoMarkupOn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMarkupOn.FormatString = ""
        repoMarkupOn.HeaderText = "MarkUp On"
        repoMarkupOn.Name = colMarkupOn
        repoMarkupOn.ReadOnly = True
        repoMarkupOn.Width = 100
        repoMarkupOn.IsVisible = False
        repoMarkupOn.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoMarkupOn)

        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colBinNo
        repoBinNo.ReadOnly = False
        repoBinNo.Width = 100
        repoBinNo.IsVisible = False
        repoBinNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = False
        repoBatchNo.Width = 100
        repoBatchNo.IsVisible = False
        repoBatchNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colManufactureDate
        repoManDate.ReadOnly = False
        repoManDate.Width = 80
        repoManDate.IsVisible = False
        repoManDate.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colExpiry
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 80
        repoExpiry.IsVisible = False
        repoExpiry.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoExpiry)

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
        'repoRequition.HeaderText = "Requition No"
        repoRequition.HeaderText = "Booking Number"
        repoRequition.Name = colReqistionNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)


        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable.WrapText = True
        ''repoAssessable.ReadOnly = True
        ''repoAssessable.FormatString = ""
        ''repoAssessable.HeaderText = "Assessable"
        ''repoAssessable.Name = colAssessableRate
        ''repoAssessable.Width = 80
        ''repoAssessable.Minimum = 0
        ''repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
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

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoIsUsedInGRN As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsUsedInGRN.HeaderText = "Is Item Used IN GRN "
        repoIsUsedInGRN.Name = colItemUsedINGRN
        repoIsUsedInGRN.IsVisible = False
        repoIsUsedInGRN.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsUsedInGRN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsUsedInGRN)

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

        Dim repoFOC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFOC.FormatString = ""
        repoFOC.HeaderText = "FOC"
        repoFOC.Name = ColFOC
        repoFOC.ReadOnly = True
        repoFOC.IsVisible = False
        repoFOC.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoFOC)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
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
            'gv1.CurrentRow.Cells(colDisPer).Value
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colTotTaxAmt) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                    End If
                    'gv1.CurrentRow.Cells(colDisPer).Value 

                    If e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colUnitALter) OrElse e.Column Is gv1.Columns(colUnitRate) OrElse e.Column Is gv1.Columns(ColCommParty) OrElse e.Column Is gv1.Columns(colCommRate) OrElse e.Column Is gv1.Columns(colRTRate) OrElse e.Column Is gv1.Columns(colICodeGrp) OrElse e.Column Is gv1.Columns(colTAX_PAID) OrElse e.Column Is gv1.Columns(colShipParty) OrElse e.Column Is gv1.Columns(colLocationCode) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse e.Column Is gv1.Columns(colTotTaxAmt) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colManualRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal Then

                        If (e.Column Is gv1.Columns(colQty) OrElse (e.Column Is gv1.Columns(colHeadDiscamt)) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colHeaDDisPer) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                            If (e.Column Is gv1.Columns(colQty) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0) Then
                                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                'If dblEnteredQty > dblPendingQty Then
                                '    common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                '    gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                                'End If
                            ElseIf (e.Column Is gv1.Columns(colQty) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) = 0) Then
                                If AutoScheme Then
                                    gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                                End If

                            End If
                            'findQtyandPromoSchemeCode(False)
                            'CalDiffrate(CInt(gv1.CurrentRow.Index), True)
                            gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colManualRate).Value
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()
                            'CalculateDiscountAmount()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            'If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                            '    common.clsCommon.MyMessageBoxShow("Please select Customer First")
                            '    isCellValueChangedOpen = False
                            '    Exit Sub
                            'End If
                            'If blnBackCalculation = True Then
                            '    OpenICodeList(False)
                            'Else
                            '    OpenICodeListCurrentCalaculation(False)
                            'End If
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            OpenItemList(False)
                        ElseIf e.Column Is gv1.Columns(colICodeGrp) Then
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            OpenItemGroupList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            'If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                            '    CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnit)
                            'End If
                            'UpdateCurrentRow(gv1.CurrentRow.Index)
                            'UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colUnitALter) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) = 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select Main Unit First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            OpenUOMAlterList(False)
                        ElseIf e.Column Is gv1.Columns(colUnitRate) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) = 0 Then
                                common.clsCommon.MyMessageBoxShow("Please select Main Unit First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                                'ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colUnitALter).Value) = 0 Then
                                '    common.clsCommon.MyMessageBoxShow("Please select Main Unit First")
                                '    isCellValueChangedOpen = False
                                '    Exit Sub
                            End If
                            OpenUOMRateList(False)
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                                CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnitRate)
                            End If
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colTAX_PAID) Then
                            'CalDiffrate(CInt(gv1.CurrentRow.Index), True)
                        ElseIf e.Column Is gv1.Columns(colRTRate) Then
                            'CalDiffrate(CInt(gv1.CurrentRow.Index), True)
                        ElseIf e.Column Is gv1.Columns(colLocationCode) Then
                            OpenLocationList(False)
                        ElseIf e.Column Is gv1.Columns(colShipParty) Then
                            OpenShipParty(False)
                        ElseIf e.Column Is gv1.Columns(ColCommParty) Then
                            OpenCommParty(False)
                        ElseIf e.Column Is gv1.Columns(colCommRate) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colSchemeApplicable) Then
                            findQtyandPromoSchemeCode(False)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colManualRate).Value
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colManualRate) Then
                            gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colManualRate).Value
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
                'If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 Then
                '    gv1.CurrentRow.Cells(colICode).ReadOnly = True
                'Else
                '    gv1.CurrentRow.Cells(colICode).ReadOnly = False
                'End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub findQtyandPromoSchemeCode(ByVal isButtonClick As Boolean)
        Dim dr1 As DataTable
        Dim schemeCodeCol As String
        Dim intRow As Integer
        Dim strOrderCode As String = ""
        Try
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) = 0 Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value)) = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(gv1.Rows(schemeRow).Cells(colQty).Value) >= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal Then
                                gv1.Rows.RemoveAt(schemeRow)
                            End If
                        End If
                    End If
                Next
                gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
                gv1.CurrentRow.Cells(colFromSchemeCode).Value = Nothing

            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "Yes") = CompairStringResult.Equal Then

                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value)) = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(gv1.Rows(schemeRow).Cells(colQty).Value) >= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal Then
                                gv1.Rows.RemoveAt(schemeRow)
                            End If
                        End If
                    End If
                Next

                StrSql = "SELECT top 1 TSPL_SCHEME_MASTER_NEW.Scheme_Code,TSPL_SCHEME_MASTER_NEW.Item_Qty,TSPL_SCHEME_MASTER_NEW.Unit_Code " & _
             "FROM TSPL_SCHEME_MASTER_NEW  left outer JOIN TSPL_SCHEME_DETAIL_NEW  ON  " & _
             "TSPL_SCHEME_MASTER_NEW.Scheme_Code = TSPL_SCHEME_DETAIL_NEW.Scheme_Code  left outer join " & _
             "TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code " & _
             "WHERE  Scheme_Type='Quantitive' and  (TSPL_SCHEME_MASTER_NEW.Item_Code = '" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "') AND TSPL_SCHEME_MASTER_NEW.Start_Date <='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "' " & _
                  " AND (TSPL_SCHEME_MASTER_NEW.End_Date >='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") + "' OR TSPL_SCHEME_MASTER_NEW.End_Date is NULL )   and  " & _
             "TSPL_SCHEME_MASTER_NEW.Item_Qty <= '" & clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) & "'  AND " & _
             "TSPL_SCHEME_MASTER_NEW.Unit_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) & "' AND " & _
             "TSPL_SCHEME_MASTER_NEW.MRP='" & clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value) & "' AND TSPL_SCHEME_BENEFICIARY.Cust_Code  = '" & txtVendorNo.Value & "' order by Start_Date desc"

                dr1 = clsDBFuncationality.GetDataTable(StrSql)
                Dim discountRatio As Integer = 0
                If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
                    Dim intOldRow As Integer = gv1.CurrentRow.Index
                    schemeCodeCol = dr1.Rows(0)(0).ToString
                    Dim strPriceCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceCOde).Value)

                    Dim mainItemQty As Decimal = clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    Dim mainItemCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    Dim mode As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Mod clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    discountRatio = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) - mode) / clsCommon.myCdbl(dr1.Rows(0)(1).ToString())

                    If discountRatio > 0 Then

                        'For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        '    If Not schemeCodeCol = String.Empty Then
                        '        If gv1.CurrentRow.Cells(colFromSchemeCode).Value = schemeCodeCol Then
                        '            gv1.Rows.RemoveAt(schemeRow)
                        '        End If
                        '    End If
                        'Next
                        Dim dblSchemeItemActualQTy
                        'StrSql = "SELECT TSPL_SCHEME_DETAIL_NEW.Item_Code, TSPL_SCHEME_DETAIL_NEW.Qty,TSPL_SCHEME_DETAIL_NEW.Unit_Code, " & _
                        '"TSPL_SCHEME_DETAIL_NEW.MRP FROM TSPL_SCHEME_MASTER_NEW left outer JOIN TSPL_SCHEME_DETAIL_NEW    ON  " & _
                        '"TSPL_SCHEME_MASTER_NEW.Scheme_Code = TSPL_SCHEME_DETAIL_NEW.Scheme_Code WHERE  " & _
                        StrSql = "SELECT TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_PRICE_MASTER.Price_Code,TSPL_SCHEME_DETAIL_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc, " & _
                           "TSPL_SCHEME_DETAIL_NEW.Qty,TSPL_SCHEME_DETAIL_NEW.Unit_Code, TSPL_SCHEME_DETAIL_NEW.MRP,TSPL_SCHEME_DETAIL_NEW.Price_Date, " & _
                           "TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, TSPL_ITEM_PRICE_MASTER.Tax_group  , TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.show,  " & _
                           "TSPL_ITEM_MASTER.Sku_Seq,TAX1_Rate as Tax1Rate,TAX2_Rate as  Tax2Rate,TAX3_Rate as Tax3Rate,TAX4_Rate as Tax4Rate, " & _
                           "TAX5_Rate as Tax5Rate,TAX6_Rate as Tax6Rate,TAX7_Rate as Tax7Rate,TAX8_Rate as Tax8Rate, TAX9_Rate as Tax9Rate, " & _
                           "TAX10_Rate as Tax10Rate,TSPL_ITEM_PRICE_MASTER.TAX1 ,  " & _
                           "TSPL_ITEM_PRICE_MASTER.TAX2,TSPL_ITEM_PRICE_MASTER.TAX3, TSPL_ITEM_PRICE_MASTER.TAX4,TSPL_ITEM_PRICE_MASTER.TAX5,  " & _
                           "TSPL_ITEM_PRICE_MASTER.TAX6,TSPL_ITEM_PRICE_MASTER.TAX7, TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,  " & _
                           "TSPL_ITEM_PRICE_MASTER.TAX10,abatement_rate FROM TSPL_SCHEME_MASTER_NEW left outer JOIN TSPL_SCHEME_DETAIL_NEW  " & _
                           "ON  TSPL_SCHEME_MASTER_NEW.Scheme_Code = TSPL_SCHEME_DETAIL_NEW.Scheme_Code left outer join  " & _
                           "TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCHEME_DETAIL_NEW.Item_Code left outer  join tspl_item_price_master on " & _
                           "tspl_item_price_master.Item_Code=TSPL_SCHEME_DETAIL_NEW.Item_Code and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_SCHEME_DETAIL_NEW.Unit_Code and " & _
                           "TSPL_ITEM_PRICE_MASTER.Start_Date=TSPL_SCHEME_DETAIL_NEW.Price_Date and " & _
                           "TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=TSPL_SCHEME_DETAIL_NEW.MRP " & _
                           "WHERE Scheme_Type='Quantitive' and (TSPL_SCHEME_MASTER_NEW.Scheme_Code =  '" & schemeCodeCol & "') and TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "'"

                        dr1 = clsDBFuncationality.GetDataTable(StrSql)
                        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
                            Dim intParentRowIndexNo As Integer = gv1.CurrentRow.Index
                            gv1.Rows(intParentRowIndexNo).Cells(colFromSchemeCode).Value = schemeCodeCol
                            For Each tdr As DataRow In dr1.Rows
                                dblSchemeItemActualQTy = clsItemLocationDetails.getBalance(clsCommon.myCstr(tdr("Item_Code")), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(tdr("Unit_Code")), clsCommon.myCstr(tdr("MRP")))
                                intOldRow += 1

                                If clsCommon.myLen(gv1.Rows(intParentRowIndexNo).Cells(colReqistionNo).Value) = 0 Then
                                    gv1.Rows.AddNew()
                                    intRow = gv1.Rows.Count - 1
                                    'If Not clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                                    '    gv1.CurrentRow.Cells(colFromSchemeCode).Value = schemeCodeCol
                                    '    gv1.Rows.AddNew()
                                    '    intRow = gv1.Rows.Count - 1
                                    'ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 And clsCommon.CompairString(gv1.CurrentRow.Cells(colSchemeItem).Value, "Yes") = CompairStringResult.Equal Then
                                    '    intRow = gv1.CurrentRow.Index + 1
                                    '    gv1.Rows.AddNew()
                                    'Else
                                    '    intRow = gv1.CurrentRow.Index + 1
                                    'End If
                                Else
                                    strOrderCode = gv1.Rows(intParentRowIndexNo).Cells(colReqistionNo).Value
                                    If clsCommon.CompairString(gv1.Rows(intParentRowIndexNo).Cells(colSchemeApplicable).Value, "Yes") = CompairStringResult.Equal Then
                                        gv1.Rows.AddNew()
                                        intRow = gv1.Rows.Count - 1

                                    End If
                                End If


                                gv1.Rows(intRow).Cells(colRowType).Value = RowTypeItem
                                gv1.Rows(intRow).Cells(colLineNo).Value = intRow + 1
                                gv1.Rows(intRow).Cells(colICode).Value = clsCommon.myCstr(tdr("Item_Code"))
                                gv1.Rows(intRow).Cells(colIName).Value = clsCommon.myCstr(tdr("Item_Desc"))
                                gv1.Rows(intRow).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(tdr("Item_Code")), Nothing)
                                gv1.Rows(intRow).Cells(colPriceCOde).Value = strPriceCode
                                gv1.Rows(intRow).Cells(colPriceDateColumn).Value = clsCommon.myCstr(tdr("Start_Date"))
                                gv1.Rows(intRow).Cells(colUnit).Value = clsCommon.myCstr(tdr("Unit_Code"))
                                gv1.Rows(intRow).Cells(colMRP).Value = clsCommon.myCdbl(tdr("MRP"))
                                gv1.Rows(intRow).Cells(colRate).Value = clsCommon.myCdbl(tdr("Item_Basic_Price"))
                                gv1.Rows(intRow).Cells(colQty).Value = clsCommon.myCdbl(tdr("Qty")) * discountRatio
                                gv1.Rows(intRow).Cells(colItemWeight).Value = clsCommon.myCdbl(tdr("Weight_Value"))
                                gv1.Rows(intRow).Cells(colSchemeApplicable).Value = "Yes"
                                gv1.Rows(intRow).Cells(colSchemeItem).Value = "Yes"
                                gv1.Rows(intRow).Cells(colFromSchemeCode).Value = schemeCodeCol
                                gv1.Rows(intRow).Cells(ColFOC).Value = 1
                                gv1.Rows(intRow).Cells(colAbatementPer).Value = clsCommon.myCdbl(tdr("abatement_rate"))
                                gv1.Rows(intRow).Cells(colActualCost).Value = clsCommon.myCdbl(tdr("Item_Basic_Price"))
                                gv1.Rows(intRow).Cells(colTaxRate1).Value = clsCommon.myCdbl(tdr("Tax1Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate2).Value = clsCommon.myCdbl(tdr("Tax2Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate3).Value = clsCommon.myCdbl(tdr("Tax3Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate4).Value = clsCommon.myCdbl(tdr("Tax4Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate5).Value = clsCommon.myCdbl(tdr("Tax5Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate6).Value = clsCommon.myCdbl(tdr("Tax6Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate7).Value = clsCommon.myCdbl(tdr("Tax7Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate8).Value = clsCommon.myCdbl(tdr("Tax8Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate9).Value = clsCommon.myCdbl(tdr("Tax9Rate"))
                                gv1.Rows(intRow).Cells(colTaxRate10).Value = clsCommon.myCdbl(tdr("Tax10Rate"))

                                gv1.Rows(intRow).Cells(colTax1).Value = clsCommon.myCstr(tdr("Tax1"))
                                gv1.Rows(intRow).Cells(colTax2).Value = clsCommon.myCstr(tdr("Tax2"))
                                gv1.Rows(intRow).Cells(colTax3).Value = clsCommon.myCstr(tdr("Tax3"))
                                gv1.Rows(intRow).Cells(colTax4).Value = clsCommon.myCstr(tdr("Tax4"))
                                gv1.Rows(intRow).Cells(colTax5).Value = clsCommon.myCstr(tdr("Tax5"))
                                gv1.Rows(intRow).Cells(colTax6).Value = clsCommon.myCstr(tdr("Tax6"))
                                gv1.Rows(intRow).Cells(colTax7).Value = clsCommon.myCstr(tdr("Tax7"))
                                gv1.Rows(intRow).Cells(colTax8).Value = clsCommon.myCstr(tdr("Tax8"))
                                gv1.Rows(intRow).Cells(colTax9).Value = clsCommon.myCstr(tdr("Tax9"))
                                gv1.Rows(intRow).Cells(colTax10).Value = clsCommon.myCstr(tdr("Tax10"))
                                Dim dblConvF As Double
                                dblConvF = GetConvFactor(gv1.Rows(intRow).Cells(colUnit).Value, gv1.Rows(intRow).Cells(colICode).Value)
                                gv1.Rows(intRow).Cells(colConvF).Value = dblConvF
                                gv1.Rows(intRow).Cells(colTotItemWt).Value = dblConvF * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colItemWeight).Value) * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colQty).Value)

                                gv1.Rows(intRow).Cells(ColActualBalQty).Value = dblSchemeItemActualQTy
                                'If gv1.Rows(intRow).Cells(ColActualBalQty).Value = 0 Then
                                '    Throw New Exception("Qty is not avaliable for item " & gv1.Rows(intRow).Cells(colICode).Value & " MRP = " & gv1.Rows(intRow).Cells(colMRP).Value & " at location " & txtBillToLocation.Value & " ")
                                '    SetBlankOfItemColumns()
                                'End If

                                If intRow <> intOldRow Then
                                    gv1.Rows.Move(intRow, intOldRow)
                                End If

                                UpdateCurrentRow(gv1.Rows(intRow).Index)
                                UpdateCurrentRow(gv1.Rows(intRow).Index)
                                UpdateAllTotals()
                                'End If
                            Next

                        End If
                        If dr1.Rows.Count = 0 Then
                            gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                            gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
                            gv1.CurrentRow.Cells(colFromSchemeCode).Value = Nothing
                        End If
                    End If
                Else
                    If AutoScheme = False Then
                        'common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                    End If
                    For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value)) = CompairStringResult.Equal Then
                                If clsCommon.myCdbl(gv1.Rows(schemeRow).Cells(colQty).Value) >= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal Then
                                    gv1.Rows.RemoveAt(schemeRow)
                                End If
                            End If
                        End If
                    Next
                    'common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                    gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                    gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
                    gv1.CurrentRow.Cells(colFromSchemeCode).Value = Nothing
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


    Sub AddCustomerDisc(ByVal isButtonClick As Boolean)

    End Sub



    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"

            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("OrderItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colOrgUnitRate).Value = gv1.CurrentRow.Cells(colUnit).Value
            Dim intUOMCount As Integer = clsDBFuncationality.getSingleValue("select COUNT(Item_Code) from TSPL_ITEM_UOM_DETAIL  where Item_Code='" & strICode & "'")
            If intUOMCount = 1 Then
                gv1.CurrentRow.Cells(colUnitRate).Value = gv1.CurrentRow.Cells(colUnit).Value
                gv1.CurrentRow.Cells(colUnitALter).Value = gv1.CurrentRow.Cells(colUnit).Value
            Else
                gv1.CurrentRow.Cells(colUnitRate).Value = Nothing
                gv1.CurrentRow.Cells(colUnitALter).Value = Nothing
            End If
            Dim dblConvF As Double
            dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
            gv1.CurrentRow.Cells(colConvF).Value = dblConvF
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                'txtrate.Value = gv1.CurrentRow.Cells(colBOOK_Rate).Value

                CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnit)
            Else
                'If gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colRate).Value Then
                '    gv1.CurrentRow.Cells(colRate).Value = GetConvRate(gv1.CurrentRow.Index)

                '    'gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                'Else
                '    gv1.CurrentRow.Cells(colRate).Value = GetConvRate(gv1.CurrentRow.Index)
                '    gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                'End If

            End If
            'If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value)) = CompairStringResult.Equal Then
            '    common.clsCommon.MyMessageBoxShow("Main Unit and Alternate unit should not be same.")
            '    gv1.CurrentRow.Cells(colUnit).Value = Nothing
            '    isCellValueChangedOpen = False
            'End If

            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
            UpdateAllTotals()
            If AutoScheme Then
                gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
            End If

            findQtyandPromoSchemeCode(False)

        End If
    End Sub
    Sub OpenUOMAlterList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim qry As String = ""
        Dim whrCls As String = ""
        Dim strMainUnit As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        Dim strRateUnit As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value)

        If clsCommon.myLen(strICode) > 0 Then
            qry = "select distinct UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            'If clsCommon.myLen(strRateUnit) > 0 Then
            '    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value)) = CompairStringResult.Equal Then
            '        whrCls = "Item_Code='" + strICode + "' and  UOM_Code not in ('" & clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) & "')"
            '    Else
            '        whrCls = "UOM_Code in ('" & strMainUnit & "','" & strRateUnit & "') and Item_Code='" + strICode + "'"
            '    End If
            'Else
            '    whrCls = "Item_Code='" + strICode + "' "
            'End If
            whrCls = "Item_Code='" + strICode + "' "
            gv1.CurrentRow.Cells(colUnitALter).Value = clsCommon.ShowSelectForm("OrderItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value), "Code", isButtonClick)
            Dim intUOMCount As Integer = clsDBFuncationality.getSingleValue("select COUNT(Item_Code) from TSPL_ITEM_UOM_DETAIL  where Item_Code='" & strICode & "'")
            If intUOMCount = 1 Then
                gv1.CurrentRow.Cells(colUnitRate).Value = gv1.CurrentRow.Cells(colUnitALter).Value
            Else
                'gv1.CurrentRow.Cells(colUnitRate).Value = Nothing
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value)) = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow("Main Unit and Alternate unit should not be same.", Me.Text)
                    gv1.CurrentRow.Cells(colUnitALter).Value = Nothing
                    isCellValueChangedOpen = False
                End If
                If Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value)) = CompairStringResult.Equal Then
                    If Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value)) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colUnitRate).Value = Nothing
                    End If
                End If

            End If
        End If
    End Sub
    Sub OpenUOMRateList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim strMainUnit As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        Dim strAlter As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select Unit_Code as Code,Unit_Desc as [Description] from TSPL_UNIT_MASTER"
            Dim whrCls As String = "Unit_Code in ('" & strMainUnit & "','" & strAlter & "')"
            gv1.CurrentRow.Cells(colUnitRate).Value = clsCommon.ShowSelectForm("OrderItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colOrgUnit).Value = gv1.CurrentRow.Cells(colUnitRate).Value
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                'txtrate.Value = gv1.CurrentRow.Cells(colBOOK_Rate).Value
                CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnitRate)
            Else
                If gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colManualRate).Value Then

                    If clsCommon.CompairString(gv1.CurrentRow.Cells(colTAX_PAID).Value, "Yes") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colRate).Value = GetConvRateUOM(gv1.CurrentRow.Index)
                        gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                    Else
                        gv1.CurrentRow.Cells(colRate).Value = GetConvRateUOM(gv1.CurrentRow.Index)
                        gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                    End If

                    'gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                Else
                    gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colManualRate).Value
                    gv1.CurrentRow.Cells(colRate).Value = GetConvRateUOM(gv1.CurrentRow.Index)
                    gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colRate).Value
                End If

            End If
        End If
    End Sub
    Sub OpenLocationList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            'Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colLocationCode).Value = clsLocation.getFinder("", gv1.CurrentRow.Cells(colLocationCode).Value, isButtonClick)
            gv1.CurrentRow.Cells(colLocationName).Value = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & gv1.CurrentRow.Cells(colLocationCode).Value & "'")
        End If
    End Sub
    Sub OpenShipParty(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then

            Dim whrCls As String = "Parent_Customer_No='" + txtVendorNo.Value + "' and TSPL_CUSTOMER_MASTER.Status='N'"
            gv1.CurrentRow.Cells(colShipParty).Value = clsCustomerMaster.getFinder(whrCls, gv1.CurrentRow.Cells(colShipParty).Value, isButtonClick)
            gv1.CurrentRow.Cells(colShipPartyName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.CurrentRow.Cells(colShipParty).Value & "'")

            'findQtyandPromoSchemeCode(False)
        End If
    End Sub
    Sub OpenCommParty(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim whrCls As String = " Vendor_Type='A'"
            gv1.CurrentRow.Cells(ColCommParty).Value = clsVendorMaster.getFinder(whrCls, gv1.CurrentRow.Cells(ColCommParty).Value, isButtonClick)
            gv1.CurrentRow.Cells(ColCommPartyName).Value = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" & gv1.CurrentRow.Cells(ColCommParty).Value & "'")
        End If
    End Sub
    Sub OpenGetbalance(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
    End Sub
    Sub OpenItemGroupList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtReqNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Booking First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        ElseIf clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        ElseIf clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        End If
        Dim Qry As String
        Dim whrCls As String
        If clsCommon.myLen(txtReqNo.Value) = 0 Then
            Qry = "select  distinct CSA_TYPE as  Code from TSPL_ITEM_MASTER "
            whrCls = "(CSA_TYPE <> '' and CSA_TYPE <> 'None')"
        Else
            Qry = "select Item_Group as Code from TSPL_BOOKING_DETAIL_PRODUCTSALE"
            whrCls = "Document_Code='" & txtReqNo.Value & "'"
        End If
        gv1.CurrentRow.Cells(colICodeGrp).Value = clsCommon.ShowSelectForm("OrderItemGrp", Qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICodeGrp).Value), "Code", isButtonClick)
        SetBlankOfItemColumns()
        If chkItemwise.Checked = False Then
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colIHSN).Value = ""
        End If

        Qry = "select BOOK_RATE_UOM,Item_Cost,Unit_code,TAX_PAID from TSPL_BOOKING_DETAIL_PRODUCTSALE where Item_Group='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICodeGrp).Value) & "'  and Document_Code='" & txtReqNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value = clsCommon.myCstr(dt.Rows(0)("BOOK_RATE_UOM"))
            gv1.CurrentRow.Cells(colBOOK_Rate).Value = clsCommon.myCdbl(dt.Rows(0)("Item_Cost"))
            gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
            gv1.CurrentRow.Cells(colUnitRate).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
            gv1.CurrentRow.Cells(colOrgUnitRate).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
            gv1.CurrentRow.Cells(colTAX_PAID).Value = clsCommon.myCstr(dt.Rows(0)("TAX_PAID"))
            gv1.CurrentRow.Cells(colReqistionNo).Value = txtReqNo.Value
            gv1.CurrentRow.Cells(colPendingQty).Value = GetBalanceBookingQty(txtReqNo.Value, gv1.CurrentRow.Cells(colICodeGrp).Value)
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                txtrate.Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colBOOK_Rate).Value)
                'CalDiffrate(CInt(gv1.CurrentRow.Index), True)
            End If
        End If
    End Sub
    Sub OpenItemList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtReqNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Booking First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        ElseIf clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        ElseIf clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location First", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colRowType).Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Row Type", Me.Text)
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            Dim strTax As String = Nothing
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus Then
                If chkIsTaxable.Checked Then
                    strTax = " and Is_Taxable=1"
                Else
                    strTax = " and Is_Taxable=0"
                End If
            End If
            Dim Qry As String
            Dim whrCls As String
            If clsCommon.myLen(txtReqNo.Value) = 0 Then
                whrCls = "  Active =1 and Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 " & strTax
                gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
            Else
                If chkItemwise.Checked Then
                    Qry = "select TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Code as Code,tspl_item_master.Item_Desc as [Item Desc] ,ISNULL(tspl_item_master.Short_Description ,'') As  Short_Description ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,TSPL_BOOKING_DETAIL_PRODUCTSALE.Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price]  from TSPL_BOOKING_DETAIL_PRODUCTSALE left outer join tspl_item_master on TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Code=TSPL_ITEM_MASTER.Item_Code"
                    whrCls = "Document_Code='" & txtReqNo.Value & "'" & strTax
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("OrderItefndnder", Qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "Code", isButtonClick)
                Else
                    Dim strIcodegrp As String = gv1.CurrentRow.Cells(colICodeGrp).Value
                    If clsCommon.myLen(strIcodegrp) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Item Group First", Me.Text)
                        isCellValueChangedOpen = False
                        Exit Sub
                    End If
                    ' whrCls = "csa_type='" & gv1.CurrentRow.Cells(colICodeGrp).Value & "'"
                    ''richa agarwal 8 july 2016
                    If chkIsTaxable.Checked Then
                        strTax = " and IsTaxable=1"
                    Else
                        strTax = " and IsTaxable=0"
                    End If
                    whrCls = "csa_type='" & gv1.CurrentRow.Cells(colICodeGrp).Value & "' and Active =1 and Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 " & strTax


                    Dim strItemCodeforlocation As String = String.Empty
                    Dim strItemCategory As String = String.Empty
                    Dim itemcount As Integer = 0
                    Dim StrCustomerState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(state,'') AS STATE from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "'"))
                    Dim StrLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State,'') AS STATE from TSPL_LOCATION_MASTER WHERE LOCATION_CODE='" & clsCommon.myCstr(txtBillToLocation.Value) & "'"))
                    For ii As Integer = 0 To gv1.CurrentRow.Index
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "") <> CompairStringResult.Equal Then
                            strItemCodeforlocation = strItemCodeforlocation + "'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "',"
                            itemcount = itemcount + 1
                        End If
                    Next
                    If clsCommon.myLen(strItemCodeforlocation) > 0 Then
                        strItemCodeforlocation = strItemCodeforlocation.Substring(0, strItemCodeforlocation.Length - 1)
                    End If
                    If clsCommon.CompairString(StrCustomerState, StrLocationState) = CompairStringResult.Equal Then
                        strItemCategory = "L"
                    Else
                        strItemCategory = "I"
                    End If

                    Dim strcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(Item_Code) FROM TSPL_LOCATION_WISE_ITEM_MASTER where Location_Code='" & txtBillToLocation.Value & "' and Item_Category='" & strItemCategory & "' and Item_Code in (" & strItemCodeforlocation & ")"))
                    If strcount > 0 Then
                        whrCls += " and TSPL_ITEM_MASTER.Item_Code in (sELECT TSPL_LOCATION_WISE_ITEM_MASTER.Item_Code FROM TSPL_LOCATION_WISE_ITEM_MASTER where isnull(TSPL_LOCATION_WISE_ITEM_MASTER.Item_Category,'')='" & strItemCategory & "' and TSPL_LOCATION_WISE_ITEM_MASTER.Location_Code='" & clsCommon.myCstr(txtBillToLocation.Value) & "') "
                        gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
                    Else
                        gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
                    End If

                    ''---------- changes end

                    'gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)

                End If
            End If
            If GSTStatus = False Then
                If CheckItemtaxType() = False Then
                    Exit Sub
                End If
            End If
            SetBlankOfItemColumns()
            gv1.CurrentRow.Cells(colIName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            gv1.CurrentRow.Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(gv1.CurrentRow.Cells(colICode).Value, Nothing)
            gv1.CurrentRow.Cells(colICodeGrp).Value = clsDBFuncationality.getSingleValue("select CSA_TYPE from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            gv1.CurrentRow.Cells(colUnit).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
            gv1.CurrentRow.Cells(colOrgUnit).Value = gv1.CurrentRow.Cells(colUnit).Value
            'gv1.CurrentRow.Cells(colOrgUnitRate).Value = gv1.CurrentRow.Cells(colUnit).Value
            gv1.CurrentRow.Cells(colUnitALter).Value = Nothing
            gv1.CurrentRow.Cells(colUnitRate).Value = gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value

            Qry = "select BOOK_RATE_UOM,Item_Cost,Unit_code,TAX_PAID from TSPL_BOOKING_DETAIL_PRODUCTSALE where Item_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "' and   Document_Code='" & txtReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value = clsCommon.myCstr(dt.Rows(0)("BOOK_RATE_UOM"))
                gv1.CurrentRow.Cells(colBOOK_Rate).Value = clsCommon.myCdbl(dt.Rows(0)("Item_Cost"))
                gv1.CurrentRow.Cells(colBOOK_QTY_UOM).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
                gv1.CurrentRow.Cells(colUnitRate).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
                gv1.CurrentRow.Cells(colOrgUnitRate).Value = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
                gv1.CurrentRow.Cells(colTAX_PAID).Value = clsCommon.myCstr(dt.Rows(0)("TAX_PAID"))
                gv1.CurrentRow.Cells(colReqistionNo).Value = txtReqNo.Value
                gv1.CurrentRow.Cells(colPendingQty).Value = GetBalanceBookingQty(txtReqNo.Value, gv1.CurrentRow.Cells(colICode).Value)

            End If
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                txtrate.Value = gv1.CurrentRow.Cells(colBOOK_Rate).Value
                CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnitRate)
            Else
                gv1.CurrentRow.Cells(colRate).Value = gv1.CurrentRow.Cells(colBOOK_Rate).Value
                gv1.CurrentRow.Cells(colManualRate).Value = gv1.CurrentRow.Cells(colBOOK_Rate).Value
            End If
            SetTaxDetails()
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
                SetitemWiseTaxSetting(True, True)
            Else
                SetBlankOfItemColumns()
            End If
        End If



    End Sub
    Function CheckItemtaxType() As Boolean
        Dim arrICode As New List(Of String)()
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            If clsCommon.myLen(strICode) > 0 Then
                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            End If

        Next
        Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrICode)
        Dim Item_TaxType As Integer = 0
        If Not (intx = arrICode.Count OrElse intx = 0) Then
            common.clsCommon.MyMessageBoxShow("All item should be of Excisable or NonExcisable", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colIHSN).Value = ""
            Return False
            Exit Function
        End If

        Return True
    End Function
    Function GetBalanceBookingQty(ByVal strBookingCode As String, ByVal strICode As String) As Double
        Dim strItem As String
        If chkItemwise.Checked Then
            strItem = "Item_Code='" & strICode & "'"
        Else
            strItem = "Item_Group='" & strICode & "'"
        End If
        Dim qry As String = "select sum (qty) from (" & _
        "select Qty from TSPL_BOOKING_DETAIL_PRODUCTSALE where Document_Code='" & strBookingCode & "' and " & strItem & " union all " & _
        "select -1 * Converted_Qty from TSPL_SD_SALES_ORDER_DETAIL where Against_Booking_No='" & strBookingCode & "' and " & strItem & " and Document_Code not in ('" & txtDocNo.Value & "'))a"

        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Sub OpenItemUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
        Dim whrCls As String = "Item_Code='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("OrderItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
    End Sub
    Sub OpenItemGrpUOMList(ByVal isButtonClick As Boolean)
        Dim whrCls As String = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where csa_type='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICodeGrp).Value) + "'))"
        gv1.CurrentRow.Cells(colUnit).Value = clsUOMInfo.GetFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), False)

    End Sub
    Sub OpenRateUOMList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select csa_uom from TSPL_CSA_PRICE_HEAD where csa_type ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICodeGrp).Value) + "'"
        Dim whrCls = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If whrCls IsNot Nothing AndAlso clsCommon.myLen(whrCls) > 0 AndAlso clsCommon.CompairString(whrCls, """") <> CompairStringResult.Equal Then
            whrCls = " unit_code='" + whrCls + "'"
        Else
            whrCls = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where csa_type='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICodeGrp).Value) + "'))"
        End If
        gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value = clsUOMInfo.GetFinder(whrCls, gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value, False)
    End Sub
    Private Sub txtrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrate.TextChanged
        Try
            If isInsideLoadData Then
                Return
            End If

            If clsCommon.myCdbl(txtrate.Value) > 0 Then
                CalDiffrate(0, False, "")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CalDiffrate(ByVal XR As Integer, ByVal CellChanged As Boolean, ByVal strUnit As String)


        Try


            Dim diffrate As Decimal = 0
            Dim orgrate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim uom As String = ""
            Dim cnvrsn As Decimal = 1
            Dim csauom As String = ""
            Dim qry As String
            Dim CSA_State As String = clsCSAPriceMaster.GetCSAState(txtVendorNo.Value)

            If CellChanged Then

                uom = clsCommon.myCstr(gv1.Rows(XR).Cells(strUnit).Value)
                If clsCommon.myLen(uom) = 0 Then
                    If clsCommon.myLen(gv1.Rows(XR).Cells(colUnitRate).Value) = 0 Then
                        uom = clsCommon.myCstr(gv1.Rows(XR).Cells(colUnit).Value)
                    Else
                        uom = clsCommon.myCstr(gv1.Rows(XR).Cells(colUnitRate).Value)
                    End If
                End If
                qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                'If clsCommon.CompairString(clsCommon.myCstr(cmbCSAType.SelectedValue), clsCommon.myCstr(gv1.Rows(XR).Cells(colCSAType).Value)) <> CompairStringResult.Equal Then
                '    Exit Sub
                'End If

                qry = "select TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no "
                qry += "where tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colTAX_PAID).Value) + "'"
                qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICodeGrp).Value) + "'"
                qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtBillToLocation.Value + "')"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    csauom = clsCommon.myCstr(dt.Rows(0)("uom"))

                    Dim csaconversion As Decimal = 0
                    csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and uom_code='" + csauom + "'"))
                    If csaconversion <= 0 Then
                        csaconversion = 1
                    End If


                    diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                    If chkItemwise.Checked Then
                        orgrate = (clsCommon.myCdbl(gv1.Rows(XR).Cells(colBOOK_Rate).Value) + clsCommon.myCdbl(diffrate))
                    Else
                        orgrate = (clsCommon.myCdbl(txtrate.Value) + clsCommon.myCdbl(diffrate))
                    End If

                    orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                    gv1.Rows(XR).Cells(colRate).Value = orgrate
                    gv1.Rows(XR).Cells(colManualRate).Value = orgrate
                Else
                    clsCommon.MyMessageBoxShow("Please create Price chart or check location mapped with price chart.", Me.Text)
                    If clsCommon.myCdbl(gv1.Rows(XR).Cells(colRate).Value) <= 0 Then
                        gv1.Rows(XR).Cells(colRate).Value = 0
                        gv1.Rows(XR).Cells(colManualRate).Value = 0
                    End If
                End If

                gv1.Rows(XR).Cells(colAmt).Value = clsCommon.myCdbl(gv1.Rows(XR).Cells(colRate).Value) * clsCommon.myCdbl(gv1.Rows(XR).Cells(colQty).Value)
            Else

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.CompairString(grow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                        If clsCommon.myLen(grow.Cells(colUnitRate).Value) > 0 Then
                            strUnit = colUnitRate
                        Else
                            strUnit = colUnit
                        End If
                        uom = clsCommon.myCstr(grow.Cells(strUnit).Value)
                        qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and uom_code='" + uom + "'"
                        cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                        If cnvrsn <= 0 Then
                            cnvrsn = 1
                        End If

                        If clsCommon.myLen(uom) <= 0 Then
                            Continue For
                        End If
                        'If clsCommon.CompairString(clsCommon.myCstr(cmbCSAType.SelectedValue), clsCommon.myCstr(grow.Cells(colCSAType).Value)) <> CompairStringResult.Equal Then
                        '    Continue For
                        'End If

                        qry = "select TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                        qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no "
                        qry += "where tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(grow.Cells(colTAX_PAID).Value) + "'"
                        qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(grow.Cells(colICodeGrp).Value) + "'"
                        qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtBillToLocation.Value + "')"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            csauom = clsCommon.myCstr(dt.Rows(0)("uom"))

                            Dim csaconversion As Decimal = 0
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and uom_code='" + csauom + "'"))
                            If csaconversion <= 0 Then
                                csaconversion = 1
                            End If

                            diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))

                            If chkItemwise.Checked Then
                                If clsCommon.myCdbl(txtrate.Value) > 0 Then
                                    orgrate = (clsCommon.myCdbl(txtrate.Value) + clsCommon.myCdbl(diffrate))
                                Else
                                    orgrate = (clsCommon.myCdbl(grow.Cells(colBOOK_Rate).Value) + clsCommon.myCdbl(diffrate))
                                End If

                            Else
                                orgrate = (clsCommon.myCdbl(txtrate.Value) + clsCommon.myCdbl(diffrate))
                            End If
                            'orgrate = (clsCommon.myCdbl(txtrate.Value) + clsCommon.myCdbl(diffrate))
                            orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                            grow.Cells(colRate).Value = orgrate
                            grow.Cells(colManualRate).Value = orgrate
                        Else
                            'clsCommon.MyMessageBoxShow("Please create Price chart or check location mapped with price chart.")
                            If clsCommon.myCdbl(grow.Cells(colRate).Value) <= 0 Then
                                grow.Cells(colManualRate).Value = 0
                                grow.Cells(colRate).Value = 0
                            End If
                        End If
                        grow.Cells(colAmt).Value = clsCommon.myCdbl(grow.Cells(colRate).Value) * clsCommon.myCdbl(grow.Cells(colQty).Value)
                    End If
                Next
            End If


            CalDocumentAmt()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub CalDocumentAmt()
        Try
            lblTotRAmt.Text = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                lblTotRAmt.Text = clsCommon.myCdbl(lblTotRAmt.Text) + clsCommon.myCdbl(grow.Cells(colAmt).Value)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Row Type", Me.Text)
            Exit Sub
        End If
        Dim strPriceGrp, strPriceCondition, strPriceGrpStatus As String
        strPriceGrp = "''"
        strPriceGrpStatus = "''"
        strPriceCondition = ""
        If clsCommon.myLen(txtPriceCode.Text) > 0 Then
            strPriceGrp = "''"
            strPriceGrpStatus = "''"
            strPriceCondition = " and Price_Code='" & txtPriceCode.Text & "'"
        ElseIf clsCommon.myLen(txtPriceGroupCode.Text) > 0 Then
            strPriceGrp = "TSPL_PRICE_GROUP_MAPPING.price_group_code "
            strPriceCondition = " and (priceGroup ='" & txtPriceGroupCode.Text & "' and PriceGroupStatus='Y')"
            strPriceGrpStatus = "TSPL_PRICE_GROUP_MAPPING.status"
        End If

        '----------------------------------07/08/2014----------n-level pivot------------------
        Dim pivotheader As String = ""
        pivotheader = clsItemMaster.GetNLevelPivotHeader()
        '-------------------------------------------------------------------------------

        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then
            Dim repID As String = ""
            Dim qry As String = ""
            Dim strTaxRate As String = ""
            Dim dr As DataRow = Nothing
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

            If clsCommon.myLen(pivotheader) <= 0 Then
                If PurchaseOneItemOneVendor = True Then
                    repID = "OrderPurOneItm1"
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
             "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND (End_Date  >='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' OR End_Date IS NULL) GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
             ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
             "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
             "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             "left outer join TSPL_VENDOR_ITEM_DETAIL on TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code left  outer join " & _
             "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
             "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
             "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
             "where TSPL_ITEM_MASTER.Active=1  and Is_FreshItem=0 and Product_Type not in ('MI'))xxx Where  Tax_group='" & txtTaxGroup.Value & "'   " & strPriceCondition & " " & _
             "" & strTaxRate & " " & _
             "Order By Item_Code,Start_Date,UOM desc"
                    dr = clsCommon.ShowSelectFormForRow(repID, qry)
                Else
                    repID = "OrderPurOneItmNo2"
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
             "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND (End_Date  >='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' OR End_Date IS NULL) GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
             ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
             "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
             "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             "left  outer join " & _
             "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
             "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
             "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
             "where TSPL_ITEM_MASTER.Active=1  and Is_FreshItem=0 and Product_Type not in ('MI'))xxx Where   Tax_group='" & txtTaxGroup.Value & "' " & strPriceCondition & " " & _
             "" & strTaxRate & " " & _
             "Order By Item_Code,Start_Date,UOM desc"

                    dr = clsCommon.ShowSelectFormForRow(repID, qry)
                End If
            End If

            '-----------07/08/2014-----------n-level cat in pivot--------------------------------
            If clsCommon.myLen(pivotheader) > 0 Then
                dr = Nothing
                If PurchaseOneItemOneVendor = True Then
                    repID = "OrderPurOneItm3"
                    qry = "select * from (select a.DESCRIPTION,a.cat_value,b.* from (SELECT Item_Code as Item,Item_Desc as ItemDesc,Customer_item_no as [Customer Item Code],PrincipleCode,PrincipleDesc,vendor_code as [Vendor code],vendor_desc as [Vendor Desc],Price_Code,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate, " & _
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
             "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND (End_Date  >='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' OR End_Date IS NULL) GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
             ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
             "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
             "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             "left outer join TSPL_VENDOR_ITEM_DETAIL on TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_ITEM_MASTER.Item_Code left  outer join " & _
             "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
             "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
             "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
             "where TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI') )xxx Where Tax_group='" & txtTaxGroup.Value & "' " & strPriceCondition & " " & _
             "" & strTaxRate & " " & _
             ")b left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=b.Item) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
                    dr = clsCommon.ShowSelectFormForRow(repID, qry)
                Else
                    repID = "OrderPurOneItmNo4"
                    qry = "select * from (select a.DESCRIPTION,a.cat_value,b.* from (SELECT Item_Code as Item,Item_Desc as ItemDesc,Customer_item_no as [Customer Item Code],PrincipleCode,PrincipleDesc,Price_Code,Start_Date AS Start_Date,UOM as Unit,Item_Basic_Net as MRP,abatement_rate,Item_Basic_Price as BasicRate,ITF_CODE as [ITF CODE], " & _
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
             "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND (End_Date  >='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' OR End_Date IS NULL) GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group   " & _
             ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
             "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND  " & _
             "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  left outer join TSPL_PRICE_GROUP_MAPPING on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_PRICE_GROUP_MAPPING.price_code " & _
             "INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             "left  outer join " & _
             "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
             "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
             "left outer join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no and TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code='" & txtVendorNo.Value & "' " & _
             "where TSPL_ITEM_MASTER.Active=1  and Is_FreshItem=0 and Product_Type not in ('MI'))xxx Where  Tax_group='" & txtTaxGroup.Value & "' " & strPriceCondition & " " & _
             "" & strTaxRate & " " & _
             ")b left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=b.Item) as s pivot(max(cat_value) for description in (" + pivotheader + "))t"
                    '"Order By Item_Code,Start_Date,UOM desc"

                    dr = clsCommon.ShowSelectFormForRow(repID, qry)
                End If
            End If
            '------------------------------------------------------------


            If Not dr Is Nothing Then
                If clsCommon.myLen(clsCommon.myCstr(dr("Item"))) > 0 Then
                    SetitemWiseTaxSetting(True, True)
                    gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(dr("Item"))
                    gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dr("Unit")), clsCommon.myCdbl(dr("MRP")))
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

                    gv1.CurrentRow.Cells(colMarkupOn).Value = clsCommon.myCstr(dr("markup_on"))
                    gv1.CurrentRow.Cells(colMarkUpPercentage).Value = clsCommon.myCdbl(dr("markup_percent"))
                    gv1.CurrentRow.Cells(colLandingCost).Value = clsCommon.myCdbl(dr("landing_cost"))

                    gv1.CurrentRow.Cells(ColFOC).Value = 0
                    gv1.CurrentRow.Cells(colItemWeight).Value = clsCommon.myCdbl(dr("Weight_Value"))
                    gv1.CurrentRow.Cells(colPurCost).Value = clsCommon.myCdbl(dr("Purchase_Cost"))
                    gv1.CurrentRow.Cells(colOrgCost).Value = clsCommon.myCdbl(dr("BasicRate"))
                    Dim dblConvF As Double
                    dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colConvF).Value = dblConvF
                End If
            Else
                SetBlankOfItemColumns()
            End If
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
                SetitemWiseTaxSetting(True, True)
            Else
                SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 
        End If


        setBalance()

    End Sub

    Sub OpenICodeListCurrentCalaculation(ByVal isButtonClick As Boolean)
        'gv1.CurrentRow.Cells(colRowType).Value = RowTypeItem
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Row Type", Me.Text)
            Exit Sub
        End If

        '------------07/08/2014--------pivot category---------------------------
        Dim pivotheader As String = ""
        pivotheader = clsItemMaster.GetNLevelPivotHeader()
        '---------------------------------------------------------------------

        If clsCommon.CompairString(strItemType, RowTypeItem) = CompairStringResult.Equal Then
            Dim qry As String = ""
            If clsCommon.myLen(pivotheader) <= 0 Then
                If PurchaseOneItemOneVendor = True Then
                    qry = "select TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date],vendor_code as [Vendor code],vendor_desc as [Vendor Desc]  , " & _
                    "TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc, " & _
                    "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                    "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no left outer join " & _
                    "TSPL_VENDOR_ITEM_DETAIL on TSPL_CUSTOMER_ITEM_DETAIL.item_no=TSPL_VENDOR_ITEM_DETAIL.item_no  left  outer join " & _
                    "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and " & _
                    "TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
                    "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                    "where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI') "
                Else
                    qry = "select TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date], " & _
                    "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                    "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no  " & _
                    "where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI') "
                End If
            End If

            '---------------------07/08/2014---------------n-level pivot------------------
            If clsCommon.myLen(pivotheader) > 0 Then
                If PurchaseOneItemOneVendor = True Then
                    qry = "select * from (select a.DESCRIPTION,a.cat_value, TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date],vendor_code as [Vendor code],vendor_desc as [Vendor Desc]  , " & _
                    "TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as PrincipleCode,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc, " & _
                    "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                    "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no left outer join " & _
                    "TSPL_VENDOR_ITEM_DETAIL on TSPL_CUSTOMER_ITEM_DETAIL.item_no=TSPL_VENDOR_ITEM_DETAIL.item_no  left  outer join " & _
                    "TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code and " & _
                    "TSPL_ITEM_MASTER_CATEGORY.SNO=1 left outer join " & _
                    "TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                    " left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_CUSTOMER_ITEM_DETAIL.item_no=a.item_code" & _
                    " where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI')) as s pivot(max(cat_value) for description in (" + pivotheader + "))t  "
                Else

                    qry = "select * from (select a.DESCRIPTION,a.cat_value, TSPL_CUSTOMER_ITEM_DETAIL.item_no as Item,TSPL_CUSTOMER_ITEM_DETAIL.item_desc as [ItemDesc],TSPL_CUSTOMER_ITEM_DETAIL.uom as Unit , " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.item_rate as BasicRate,TSPL_CUSTOMER_ITEM_DETAIL.Item_MRP as MRP, TSPL_ITEM_MASTER.ITF_CODE as [ITF Code], " & _
                    "TSPL_CUSTOMER_ITEM_DETAIL.Start_Date as [Start Date], " & _
                    "Weight_Value as [Weight Value] from TSPL_CUSTOMER_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on " & _
                    "TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_ITEM_DETAIL.item_no  " & _
                    " left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_CUSTOMER_ITEM_DETAIL.item_no=a.item_code" & _
                    " where Customer_Code='" & txtVendorNo.Value & "' and TSPL_ITEM_MASTER.Active=1 and Is_FreshItem=0 and Product_Type not in ('MI')) as s pivot(max(cat_value) for description in (" + pivotheader + "))t  "

                End If
            End If
            '------------------------------------------------------------------------------


            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("SOPSDE@y", qry)
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
                    gv1.CurrentRow.Cells(colOrgUnit).Value = clsCommon.myCstr(dr("Unit"))
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
                    Dim dblConvF As Double
                    dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colConvF).Value = dblConvF
                End If
            Else
                SetBlankOfItemColumns()
            End If
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
                SetitemWiseTaxSetting(True, True)
            Else
                SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 
        End If
        setBalance()
    End Sub
    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function

    Private Sub SetBlankOfItemColumns()
        'gv1.CurrentRow.Cells(colBOOK_RATE_UOM).Value = ""
        gv1.CurrentRow.Cells(colRate).Value = 0
        gv1.CurrentRow.Cells(colOrgCost).Value = 0
        gv1.CurrentRow.Cells(colManualRate).Value = 0
        gv1.CurrentRow.Cells(colQty).Value = 0
        gv1.CurrentRow.Cells(colUnit).Value = ""
        'gv1.CurrentRow.Cells(colTAX_PAID).Value = ""

        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        'gv1.CurrentRow.Cells(colPriceDateColumn).Value = 0
        gv1.CurrentRow.Cells(colPriceCOde).Value = 0
        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
        gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
        'gv1.CurrentRow.Cells(colRate).Value = 0
        gv1.CurrentRow.Cells(colMRP).Value = 0
        ''gv1.CurrentRow.Cells(colAssessableRate).Value = 0
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Function GetConvQty(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitRate).Value)
        Dim dblQTy As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            If dblCurrentConvF > 0 Then
                dblConvQty = Math.Round((dblQTy / dblCurrentConvF) * dblStockingUnitConvF * dblOrgConvF, 6)
            End If
        End If
        gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strCurrentUnit
        gv1.Rows(IntRowNo).Cells(colOrgUnitRate).Value = strCurrentUnit
        Return dblConvQty
    End Function
    Private Function GetConvBookingQty(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colBOOK_QTY_UOM).Value)
        Dim dblQTy As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            If dblCurrentConvF > 0 Then
                dblConvQty = Math.Round((dblQTy / dblCurrentConvF) * dblStockingUnitConvF * dblOrgConvF, 2)
            End If
        End If
        gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strCurrentUnit
        gv1.Rows(IntRowNo).Cells(colOrgUnitRate).Value = strCurrentUnit
        Return dblConvQty
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
            If dblOrgConvF > 0 Then
                dblConvRate = Math.Round((dblRate / dblOrgConvF) * dblStockingUnitConvF * dblCurrentConvF, 2)
            End If
        End If
        gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strCurrentUnit
        gv1.Rows(IntRowNo).Cells(colOrgUnitRate).Value = strCurrentUnit
        Return dblConvRate
    End Function
    Private Function GetConvRateUOM(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colBOOK_QTY_UOM).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitRate).Value)
        Dim dblCurrentConvF As Double = clsItemMaster.GetConvertionFactor(strItem, strCurrentUnit, Nothing)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBOOK_Rate).Value)
        Dim dblConvRate As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            If dblOrgConvF > 0 Then
                dblConvRate = Math.Round((dblRate / dblOrgConvF) * dblStockingUnitConvF * dblCurrentConvF, 2)
            End If
        End If
        gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strCurrentUnit
        gv1.Rows(IntRowNo).Cells(colOrgUnitRate).Value = strCurrentUnit
        Return dblConvRate
    End Function
    Private Function GetConvMRP(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colOrgUnit).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
        Dim dblConvRate As Double = 0
        Dim dblConvMRP As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            If dblOrgConvF > 0 Then
                dblConvMRP = (dblMRP / dblOrgConvF) * dblStockingUnitConvF * dblCurrentConvF
            End If
        End If
        Return dblConvMRP
    End Function

    Private Function abatement() As Decimal
        Dim abat As Decimal = 0
        Dim sql As String = "select abatement_percent from tspl_abatement_master where start_date <='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and end_date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' "
        abat = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
        If abat > 0 Then
            abat = abat
        Else
            abat = 60
        End If
        Return abat
    End Function
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
       
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If GSTStatus = False Then
            strExcise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) + "'")) = 2, True, False)
        End If
        Dim arrTaxableAuth As New List(Of String)
        Dim dblConvBookingQty As Double = 0
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblManualRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colManualRate).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
        Dim dblBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
        'Dim dblItemWeight As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemWeight).Value)
        Dim dblheadDiscamt As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeadDiscamt).Value)
        Dim dblOrgBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgCost).Value)
        Dim wt_unit As String = 0
        Dim wt_qty As Double = 0
        Dim Item_Weight As Double = 0
        Dim TotalItem_Weight As Double = 0
        Dim TotalItem_WeightMetric As Double = 0
        Dim dblInnerQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        'Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        'Dim strOrgUnitRate As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitRate).Value)
        'If clsCommon.myLen(strOrgUnitRate) = 0 Then
        '    strOrgUnitRate = strOrgUnit
        'End If
        dblQty = GetConvQty(IntRowNo)
        dblConvBookingQty = GetConvBookingQty(IntRowNo)
        Dim dblBasicAmt As Double = dblQty * dblRate
        Dim dblAmt As Double = dblQty * dblRate
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            dblAmt = gv1.Rows(IntRowNo).Cells(colAmt).Value
        End If


        Dim dblMRPAmt As Double = dblQty * dblMRP

        '''' to calculate customer disc
        Dim dt As New DataTable
        Dim dblOrderQty As Double = 0
        Dim dblCustDiscQty As Double = 0
        Dim dblCustDiscAmt As Double = 0
        Dim dblCustDiscPercentage As Double = 0
        Dim dblApplyCustDisc As Double = 0
        Dim dblTotCustDisc As Double = 0


        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
       
        If clsCommon.myLen(strICode) > 0 And clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColFOC).Value) = 0 Then
            StrSql = "select top 1 Item_Qty,Amount,percentage,TSPL_SCHEME_MASTER_NEW.scheme_code from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_BENEFICIARY on  " & _
        "TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where Scheme_Type='Cash' and Cust_Code='" & txtVendorNo.Value & "' and " & _
        "Start_Date < = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and Item_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "' and " & _
        "Unit_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' and " & _
        "MRP='" & clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value) & "' " & _
        "and Basic_Price='" & clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value) & "'  order by TSPL_SCHEME_MASTER_NEW.scheme_code desc "
            dt = clsDBFuncationality.GetDataTable(StrSql)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                dblOrderQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
                dblCustDiscQty = clsCommon.myCdbl(dt.Rows(0)("Item_Qty"))
                dblCustDiscAmt = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                dblCustDiscPercentage = clsCommon.myCdbl(dt.Rows(0)("percentage"))
                If dblCustDiscPercentage = 0 Then
                    gv1.Rows(IntRowNo).Cells(ColCustDiscountQty).Value = dblCustDiscQty
                    gv1.Rows(IntRowNo).Cells(colcustDiscount).Value = dblCustDiscAmt
                    dblApplyCustDisc = Math.Truncate(dblOrderQty / dblCustDiscQty)
                    dblTotCustDisc = dblApplyCustDisc * dblCustDiscAmt
                Else
                    gv1.Rows(IntRowNo).Cells(ColCustDiscountQty).Value = dblCustDiscQty
                    gv1.Rows(IntRowNo).Cells(colCustDiscPercentage).Value = dblCustDiscPercentage
                    dblApplyCustDisc = (dblAmt * dblCustDiscPercentage) / 100
                    dblTotCustDisc = dblApplyCustDisc
                End If
                If dblTotCustDisc > 0 Then
                    gv1.Rows(IntRowNo).Cells(colCashDiscSchemeCode).Value = clsCommon.myCstr(dt.Rows(0)("scheme_code"))
                End If
                gv1.Rows(IntRowNo).Cells(colTotalCustDiscount).Value = dblTotCustDisc
            End If
        End If
        '''' end 

        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        End If




        'gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblHeadDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaDDisPer).Value)
        Dim dblHeadPerDisAmt As Double = (dblAmt * dblHeadDisPer) / 100
        Dim dblTotDiscAmt = dblheadDiscamt + dblHeadPerDisAmt + dblDisAmt
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt - dblTotCustDisc - dblheadDiscamt - dblHeadPerDisAmt

        Dim dblAbatementRate As Double = abatement()
        'Dim dblAbatementAmt As Double = ((dblMRP * dblAbatementRate) / 100) * dblQty
        Dim dblAbatementAmt As Double = ((dblMRP * dblAbatementRate) / 100) * dblQty

        Dim dblCommPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCommRate).Value)
        Dim dblCommAmt As Double = (dblAmtAfterDis * dblCommPer) / 100
        Dim dblAmtAfterComm As Double = dblAmtAfterDis - dblCommAmt
        Dim dblTotTaxAmt As Double = 0
        Dim dblTotTaxAmtTaxInclusive As Double = 0
        Dim dblLastTaxAmt As Double = 0
        Dim dblAmtAfterTax As Double = 0
        If clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(colTAX_PAID).Value, "Yes") = CompairStringResult.Equal And (dblManualRate = dblRate OrElse dblRate = dblOrgBasicRate) Then
            If strExcise = True Then
                ''''' back calculation for excisable entry start here
                For ii As Integer = 1 To 10
                    Dim Strii As String = clsCommon.myCstr(ii)
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        dblAmtAfterComm = 0
                        dblAmtAfterTax = 0
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        'If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                        '    dblTaxRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        'Else
                        '    If clsCommon.myLen(strICode) > 0 Then
                        '        Dim objTM As clsItemWiseTaxAuthority
                        '        objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value), txtDate.Value, "S")
                        '        If objTM IsNot Nothing Then
                        '            dblTaxRate = objTM.TAX_Rate
                        '            gv1.Rows(IntRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                        '            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                        '        End If
                        '    End If
                        'End If
                            Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                            Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                            Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                            Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                            Dim dblBaseAmt As Double = 0
                            Dim dblTaxAmt As Double = 0


                            If IsSurTax Then
                                Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                                dblBaseAmt = dblSurTaxAmt
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100)
                            Else
                                Dim dblOtherTaxAmt As Double = 0
                                ''If IsTaxable Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmtIncusiveTax(IntRowNo, Strii, arrTaxableAuth)
                                ''End If
                                If strExcise = True AndAlso IsExcisable = True Then
                                    dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100)
                                Else
                                    dblBaseAmt = dblAmtAfterDis
                                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTaxRate)
                                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                                    dblBaseAmt = dblAmt - (GetCurrentRowTotalTaxAmt(IntRowNo))
                                End If

                            End If
                            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                            If dblBaseAmt > 0 Then
                                dblRate = dblBaseAmt / dblQty
                            End If
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
                Next
                '''' back calculation excisable entry ends here
            Else
                '''' back calculaion for non excisable start here

                ''  for non excisable check surcharge entry yes or no
                Dim blnSurcharge As Boolean = False
                For ii As Integer = 1 To 10
                    Dim Strii As String = clsCommon.myCstr(ii)
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                        If IsSurTax Then
                            blnSurcharge = True
                        End If
                    End If
                Next
                If blnSurcharge Then

                    '''' non excisable surcharge entry only in case if one authority is non surcharge and other is surtax and taxable start here

                    For ii As Integer = 1 To 10
                        Dim Strii As String = clsCommon.myCstr(ii)
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                        If clsCommon.myLen(strTaxCode) > 0 Then
                            dblAmtAfterComm = 0
                            dblAmtAfterTax = 0
                            Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            'If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                            '    dblTaxRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            'Else
                            '    If clsCommon.myLen(strICode) > 0 Then
                            '        Dim objTM As clsItemWiseTaxAuthority
                            '        objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value), txtDate.Value, "S")
                            '        If objTM IsNot Nothing Then
                            '            dblTaxRate = objTM.TAX_Rate
                            '            gv1.Rows(IntRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                            '            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                            '        End If
                            '    End If
                            'End If
                            Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                            Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                            Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                            Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                            Dim dblBaseAmt As Double = 0
                            Dim dblTaxAmt As Double = 0
                            Dim dblTotTaxRate = 0
                            For jj As Integer = 1 To 10
                                Dim Strjj As String = clsCommon.myCstr(jj)
                                Dim dblTaxRateinner As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strjj)).Value)
                                Dim IsTaxableinner As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strjj)).Value)
                                Dim IsSurTaxinner As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strjj)).Value)
                                If IsTaxableinner = True AndAlso IsSurTaxinner = True Then
                                    dblTotTaxRate += dblTaxRateinner
                                End If
                            Next
                            'If dblTotTaxRate = 0 Then
                            '    dblTotTaxRate = dblTaxRate
                            'End If
                            If IsSurTax Then
                                Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                                dblBaseAmt = dblSurTaxAmt
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100)

                            Else
                                Dim dblOtherTaxAmt As Double = 0
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmtIncusiveTax(IntRowNo, Strii + 1, arrTaxableAuth)

                                If strExcise = True AndAlso intMRPwithabatement = 1 AndAlso IsExcisable = True Then
                                    dblBaseAmt = (dblAbatementAmt - dblOtherTaxAmt)
                                Else
                                    dblBaseAmt = (dblAmt - dblTotTaxAmtTaxInclusive)
                                End If
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTaxRate + ((dblTotTaxRate * dblTaxRate) / 100))
                                dblTotTaxAmtTaxInclusive += dblTaxAmt
                                'dblBaseAmt = dblBaseAmt - dblTaxAmt
                                'If dblBaseAmt = dblAmt And dblTotTaxAmtTaxInclusive > 0 Then
                                '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                                'ElseIf IsTaxable Then
                                '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                                'End If

                            End If
                            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                            If IsSurTax Then
                                dblBaseAmt = dblAmt - (GetCurrentRowTotalTaxAmt(IntRowNo))
                            End If
                            If dblBaseAmt > 0 Then
                                dblRate = dblBaseAmt / dblQty
                            End If
                            'dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100

                            If Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
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

                    '''' non excisable surcharge entry ends here
                Else
                    '''' non excisable without surcharge entry start here
                    For ii As Integer = 10 To 1 Step -1
                        Dim Strii As String = clsCommon.myCstr(ii)
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                        If clsCommon.myLen(strTaxCode) > 0 Then
                            dblAmtAfterComm = 0
                            dblAmtAfterTax = 0
                            Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            'If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                            '    dblTaxRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            'Else
                            '    If clsCommon.myLen(strICode) > 0 Then
                            '        Dim objTM As clsItemWiseTaxAuthority
                            '        objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value), txtDate.Value, "S")
                            '        If objTM IsNot Nothing Then
                            '            dblTaxRate = objTM.TAX_Rate
                            '            gv1.Rows(IntRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                            '            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                            '        End If
                            '    End If
                            'End If
                            Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                            Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                            Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                            Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                            Dim IsTaxonBaseAmount As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsTaxOnBaseAmount + clsCommon.myCstr(ii)).Value)

                            Dim dblBaseAmt As Double = 0
                            Dim dblTaxAmt As Double = 0
                            Dim dblTotTaxRate = 0
                            For jj As Integer = ii To 1 Step -1

                                Dim Strjj As String = clsCommon.myCstr(jj)
                                Dim dblTaxRateinner As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strjj)).Value)
                                Dim IsTaxableinner As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strjj)).Value)
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code IN (select Tax_Code  from TSPL_TAX_GROUP_DETAILS WHERE TAX_GROUP_CODE='" & txtTaxGroup.Value & "') AND Is_TCS ='Y'")) > 0 Then
                                    If IsTaxableinner = True Then
                                        dblTotTaxRate += dblTaxRateinner
                                    End If
                                Else
                                    If IsTaxableinner = False Then
                                        dblTotTaxRate += dblTaxRateinner
                                    End If
                                End If



                            Next
                            If dblTotTaxRate = 0 Then
                                dblTotTaxRate = dblTaxRate
                            End If
                            If IsSurTax Then
                                Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                                dblBaseAmt = dblSurTaxAmt
                            Else
                                Dim dblOtherTaxAmt As Double = 0
                                ''richa 21 Oct
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & strTaxCode & "' AND Is_TCS ='Y'")) <= 0 Then
                                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmtIncusiveTax(IntRowNo, Strii + 1, arrTaxableAuth)
                                End If

                                If strExcise = True AndAlso intMRPwithabatement = 1 AndAlso IsExcisable = True Then
                                        dblBaseAmt = (dblAbatementAmt - dblOtherTaxAmt)
                                    Else
                                        dblBaseAmt = (dblAmt - dblTotTaxAmtTaxInclusive)
                                    End If


                                ''richa 21 Oct
                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & strTaxCode & "' AND Is_TCS ='Y'")) > 0 Then
                                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                                    dblTotTaxAmtTaxInclusive += 0
                                    dblBaseAmt = dblBaseAmt
                                Else
                                    ''richa 21 oct
                                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTotTaxRate)
                                    dblTotTaxAmtTaxInclusive += dblTaxAmt
                                    dblBaseAmt = dblBaseAmt - dblTaxAmt
                                    End If

                                    'If dblBaseAmt = dblAmt And dblTotTaxAmtTaxInclusive > 0 Then
                                    '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                                    'ElseIf IsTaxable Then
                                    '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                                    'End If

                                End If
                            'gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = dblBaseAmt
                            If dblBaseAmt > 0 Then
                                dblRate = dblBaseAmt / dblQty
                            End If
                            'dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                            'gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblTaxAmt
                            If Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
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
                    '''' non excisable without surcharge entry ends here
                End If

                dblTotTaxAmt = GetCurrentRowTotalTaxAmt(IntRowNo)
                dblAmtAfterTax = dblAmt
                dblAmt = dblAmt - dblTotTaxAmt
                dblAmtAfterDis = dblAmtAfterDis - dblTotTaxAmt

            End If
            ' ends here


        Else
            '''' for forward calculation start here
            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    dblAmtAfterComm = 0
                    dblAmtAfterTax = 0
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    'If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                    '    dblTaxRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    'Else
                    '    If clsCommon.myLen(strICode) > 0 Then
                    '        Dim objTM As clsItemWiseTaxAuthority
                    '        objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value), txtDate.Value, "S")
                    '        If objTM IsNot Nothing Then
                    '            dblTaxRate = objTM.TAX_Rate
                    '            gv1.Rows(IntRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                    '            gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                    '        End If
                    '    End If
                    'End If
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

                        ''richa 15 OCT 2020 changes according to tax
                        If Not IsTaxonBaseAmount Then
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        End If

                        If strExcise = True AndAlso intMRPwithabatement = 1 AndAlso IsExcisable = True Then
                            dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                        Else
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
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + Strii)).Value = Nothing
                End If
            Next
            dblTotTaxAmt = GetCurrentRowTotalTaxAmt(IntRowNo)
            dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt

            '''' forward calculation ends here

        End If


        If dblQty > 0 Then
            Dim dblNetPrice As Double = dblAmtAfterDis / dblQty
            gv1.Rows(IntRowNo).Cells(colActualCost).Value = dblNetPrice
        End If
        gv1.Rows(IntRowNo).Cells(colConvQty).Value = Math.Round(dblConvBookingQty, 2)
        gv1.Rows(IntRowNo).Cells(colRateUnitQty).Value = Math.Round(dblQty, 2)
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
        gv1.Rows(IntRowNo).Cells(colAbatementPer).Value = Math.Round(dblAbatementRate, 2)
        gv1.Rows(IntRowNo).Cells(colAbatementAmount).Value = Math.Round(dblAbatementAmt, 2)
        gv1.Rows(IntRowNo).Cells(colTotalMRP).Value = Math.Round(dblMRPAmt, 2)
        gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value = Math.Round(dblBasicAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemWeight).Value = Math.Round(TotalItem_Weight, 2)
        gv1.Rows(IntRowNo).Cells(colTotItemWt).Value = Math.Round(TotalItem_WeightMetric, 2)
        gv1.Rows(IntRowNo).Cells(colTotalCustDiscount).Value = Math.Round(dblTotCustDisc, 2)
        gv1.Rows(IntRowNo).Cells(colRate).Value = Math.Round(dblRate, 6)
        'gv1.Rows(IntRowNo).Cells(colHeadDiscamt).Value = Math.Round(dblheadDiscamt, 2)
        gv1.Rows(IntRowNo).Cells(colHeadDisPerAmt).Value = Math.Round(dblHeadPerDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colTotalDiscountAmount).Value = Math.Round(dblTotDiscAmt, 2)
        'gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strOrgUnit
        'gv1.Rows(IntRowNo).Cells(colOrgUnitRate).Value = strOrgUnitRate
        gv1.Rows(IntRowNo).Cells(colMRP).Value = Math.Round(dblMRP, 2)
        gv1.Rows(IntRowNo).Cells(ColCommAmt).Value = Math.Round(dblCommAmt, 2)
        gv1.Rows(IntRowNo).Cells(ColAmtAfterCOmm).Value = Math.Round(dblAmtAfterComm, 2)

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
    Private Function GetCurrentRowOtherTaxAmtIncusiveTax(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = intEndCol To 1 Step -1
                'For ii As Integer = 1 To intEndCol
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
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
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
        Dim dblCashDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblHeadDisAmt As Double = 0
        Dim dblHeadDisPerAmt As Double = 0

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
        Dim dblCommAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) And clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 0 Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblCashDisAmt = dblCashDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalCustDiscount).Value)
                dblHeadDisAmt = dblHeadDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDiscamt).Value)
                dblHeadDisPerAmt = dblHeadDisPerAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colHeadDisPerAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
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
            End If
        Next

        ''If (gv1.CurrentRow.Index < 0) Then
        ''    If (clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0) Then
        ''        dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value)
        ''        dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.CurrentRow.Cells(colDisAmt).Value)
        ''        dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
        ''        dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt1).Value)
        ''        dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt2).Value)
        ''        dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt3).Value)
        ''        dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt4).Value)
        ''        dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt5).Value)
        ''        dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt6).Value)
        ''        dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt7).Value)
        ''        dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt8).Value)
        ''        dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt9).Value)
        ''        dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxAmt10).Value)

        ''        dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt1).Value)
        ''        dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt2).Value)
        ''        dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt3).Value)
        ''        dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt4).Value)
        ''        dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt5).Value)
        ''        dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt6).Value)
        ''        dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt7).Value)
        ''        dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt8).Value)
        ''        dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt9).Value)
        ''        dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTaxBaseAmt10).Value)

        ''        dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
        ''        dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterTax).Value)
        ''    End If
        ''End If

        For ii As Integer = 1 To gv2.Rows.Count
            Select Case (ii)
                Case 1
                    If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                        lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                        dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                        dblTaxAmt1 = (dblTaxBaseAmt1 * txtTCSTaxRate.Value) / 100
                    End If
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
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 3)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 7
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                    If dblTaxBaseAmt7 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 3)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 8
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                    If dblTaxBaseAmt8 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 3)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 9
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                    If dblTaxBaseAmt9 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 3)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 10
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                    If dblTaxBaseAmt10 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 3)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
            End Select
        Next


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
        'lblInvoiceDiscAmt.Text = dblHeadDisAmt
        'txtDiscAmt.Text = lblInvoiceDiscAmt.Text
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblTotRAmt1.Text = lblTotRAmt.Text
        lblCommAmt.Text = clsCommon.myFormat(dblCommAmt)
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
        chkSameBillShip.Enabled = True
        chkSameBillShip.Checked = True
        txtVendorNo.Enabled = False
        txtCreditLimit.Enabled = False
        BlankAllControls()
        fndProject.Enabled = True
        lblProject.Enabled = True
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadBlankGridAC()
        isNewEntry = True
        btnCopy.Enabled = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        btnHistory.Enabled = False

        txtDate.Enabled = True


        If AllowtoChangeTCSBaseAmount = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        txtTCSTaxRate.Value = 0

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave() As Boolean
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
        If CalculateTaxRatefromItemwsieTaxOnSale = 1 Then
            SetitemWiseTaxSetting(True, False)
        End If
        For ii As Integer = 0 To gv1.Rows.Count - 1
           
            UpdateCurrentRow(ii)
            UpdateCurrentRow(ii)
        Next
            UpdateAllTotals()
            '' check for the minimum order level including tolerance 
            Dim proceed As Boolean = False
            Dim dt As DataTable
            For Each dr As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(dr.Cells(colICode).Value) > 0 Then
                    If proceed = True Then
                        Exit For
                    End If
                    Dim balQty As Decimal = clsItemLocationDetails.getBalance(dr.Cells(colICode).Value, clsCommon.myCstr(Me.txtBillToLocation.Value), Me.txtDocNo.Value, txtDate.Value, Nothing, dr.Cells(colUnit).Value, dr.Cells(colMRP).Value)
                    Dim strq As String
                    strq = "select Min_Level,(Min_Level+Min_Level * Min_Level_Tollerence/100) tol_Plus,(Min_Level-Min_Level * Min_Level_Tollerence/100) tol_Minus " & _
                           " from TSPL_ITEM_REORDER_LEVEL_NEW where Item_Code='" & dr.Cells(colICode).Value & "'"

                    dt = clsDBFuncationality.GetDataTable(strq)
                    'If dt.Rows.Count > 0 Then
                    '    If balQty <= clsCommon.myCdbl(dt.Rows(0).Item("tol_Minus")) + clsCommon.myCdbl(dr.Cells(colQty).Value) Then
                    '        If clsCommon.MyMessageBoxShow("Balance of item " & dr.Cells(colICode).Value & "  on Location " & clsCommon.myCstr(Me.txtBillToLocation.Value) & " is reached minimum level of (" & balQty & "," & dt.Rows(0).Item("tol_Plus") & "). Still do you want to proceed ?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    '            proceed = False
                    '            Return False
                    '        Else
                    '            proceed = True
                    '        End If
                    '    End If
                    'End If
                End If
            Next


            'CalculateDiscountAmount()
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Customer", Me.Text)
                txtVendorNo.Focus()
                Return False
            End If

            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus = False OrElse (chkIsTaxable.Checked AndAlso GSTStatus = True) Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Tax Group", Me.Text)
                    txtTaxGroup.Focus()
                    Return False
                End If
            End If

            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Bill to Location", Me.Text)
                txtBillToLocation.Focus()
                Return False
            End If


            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Pucahse Order code Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If
            'If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtBillToLocation.Value)) Then
            '    common.clsCommon.MyMessageBoxShow("Location should be Excisable for Raw Material")
            '    txtBillToLocation.Focus()
            '    Return False
            'End If
            'If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Item Type")
            '    cboItemType.Focus()
            '    Return False
            'End If
            If chkclose.Checked = True AndAlso IsRemarksMandatory = True AndAlso clsCommon.myLen(txtCloseRemarks.Value) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Remarks for Closing Order", Me.Text)
                txtCloseRemarks.Focus()
                Return False
            End If
            If chkCommApply.Checked = True AndAlso clsCommon.myCdbl(lblCommAmt.Text) = 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Commission amount.", Me.Text)
                Return False
            End If
            If txtCustPODate.Checked Then
                If clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtCustPODate.Value)) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtDeliveryDate.Value)) Then
                common.clsCommon.MyMessageBoxShow("Please enter Delivery Date greater or equal  than Customer Po Date.", Me.Text)
                    Return False
                End If

                If clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtCustPODate.Value)) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtDate.Value)) Then
                common.clsCommon.MyMessageBoxShow("Please enter Document Date greater or equal  than Customer Po Date.", Me.Text)
                    Return False
                End If
            Else
                If clsCommon.myLen(txtPONo.Text) > 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Customer Po Date.", Me.Text)
                    Return False
                End If
                'If clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtCustPODate.Value)) > clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtDate.Value)) Then
                '    common.clsCommon.MyMessageBoxShow("Please enter Document Date greater or equal  than Customer Po Date.")
                '    Return False
                'End If

            End If
            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Document_Date,103) from TSPL_BOOKING_MASTER_PRODUCTSALE where Document_Code ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from Booking Date")
            End If
        Dim strCust As String = ""
        For jj As Integer = 0 To gv1.Rows.Count - 1
            If AllowDifferentStateofChildCustomerOnPS = 0 Then
                strCust = clsCommon.myCstr(gv1.Rows(jj).Cells(colShipParty).Value)
            Else
                If chkSameBillShip.Checked Then
                    strCust = clsCommon.myCstr(gv1.Rows(jj).Cells(colShipParty).Value)
                Else
                    strCust = ""
                End If
            End If

            If clsCommon.myLen(strCust) = 0 Then
                strCust = clsCommon.myCstr(txtVendorNo.Value)
            Else
                Exit For
            End If
        Next
            If GSTStatus = True AndAlso chkIsTaxable.Checked Then
            clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, strCust, "S", txtDate.Value, Nothing)
            End If
            Dim arrProjNo As New List(Of String)
            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            Dim dblTotalQty As Double = 0
            Dim dblPendingQty As Double = 0
            Dim arrCust As New List(Of String)

            ''richa agarwal 7 july
            '  If chkItemwise.Checked Then
            Dim strItemCodeforlocation As String = String.Empty
            Dim strItemCategory As String = String.Empty
            Dim itemcount As Integer = 0
            Dim StrCustomerState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(state,'') AS STATE from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "'"))
            Dim StrLocationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State,'') AS STATE from TSPL_LOCATION_MASTER WHERE LOCATION_CODE='" & clsCommon.myCstr(txtBillToLocation.Value) & "'"))
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "") <> CompairStringResult.Equal Then
                    strItemCodeforlocation = strItemCodeforlocation + "'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "',"
                    itemcount = itemcount + 1
                End If
            Next
            If clsCommon.myLen(strItemCodeforlocation) > 0 Then
                strItemCodeforlocation = strItemCodeforlocation.Substring(0, strItemCodeforlocation.Length - 1)
            End If
            If clsCommon.CompairString(StrCustomerState, StrLocationState) = CompairStringResult.Equal Then
                strItemCategory = "L"
            Else
                strItemCategory = "I"
            End If

            Dim strcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT count(Item_Code) FROM TSPL_LOCATION_WISE_ITEM_MASTER where Location_Code='" & txtBillToLocation.Value & "' and Item_Category='" & strItemCategory & "' and Item_Code in (" & strItemCodeforlocation & ")"))
            If strcount <> 0 Then
                If strcount <> itemcount Then
                    Throw New Exception(" You must have to select only those items which are mapped with location " & txtBillToLocation.Value & " in location master/ or which are not mapped with this location.")
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select count(Tax_Group_Code) from TSPL_TAX_GROUP_MASTER where Tax_Group_Code ='" & clsCommon.myCstr(txtTaxGroup.Value) & "' and Tax_Group_Type ='S' and Is_Tax_Exempted =1")), "0") = CompairStringResult.Equal Then
                    Throw New Exception(" Please select Tax type EXEMPTED for sale.")
                End If

            End If

            '  End If

            ''-----------------

            For ii As Integer = 0 To gv1.Rows.Count - 1
                dblTotalQty = 0
                Dim strRowType As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)
                Dim strCommParty As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColCommParty).Value)
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
                Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strICodeGrp As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICodeGrp).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)

                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                ''Dim dblAssessableAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strAlterUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitALter).Value)
                Dim strRateUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitRate).Value)
                'Dim strProject As String
                Dim strCustCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colShipParty).Value)
                If clsCommon.myLen(strCustCode) = 0 Then
                    strCustCode = txtVendorNo.Value
                ElseIf Not arrCust.Contains(strCustCode) Then
                    arrCust.Add(strCustCode)
                End If
                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strRowType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(strAlterUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter Alter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(strRateUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter Rate UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If chkCommApply.Checked = True AndAlso clsCommon.myLen(strCommParty) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter Commission party for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strReqNo) > 0 Then
                    If chkItemwise.Checked Then
                        dblPendingQty = GetBalanceBookingQty(txtReqNo.Value, strICode)
                        If dblQty > dblPendingQty Then
                            common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If
                    Else
                        dblPendingQty = GetBalanceBookingQty(txtReqNo.Value, strICodeGrp)
                    End If
                End If
                If clsCommon.myLen(strICode) > 0 Then
                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                End If


                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(strSchemeItem, "No") = CompairStringResult.Equal Then

                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        Dim wt_unit As String = 0
                        Dim wt_qty As Double = 0
                        Dim Item_Weight As Double = 0
                        Dim TotalItem_Weight As Double = 0
                        Dim TotalItem_WeightMetric As Double = 0
                        Dim dblInnerQty As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        Dim dblConvertedQty As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colConvQty).Value)

                        If jj = ii Then
                            dblTotalQty += dblConvertedQty
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        'Dim dblInnerQty As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colQty).Value)
                        Dim strInnerCustCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colShipParty).Value)
                        Dim strInnerICodeGrp As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICodeGrp).Value)
                        If clsCommon.myLen(strInnerCustCode) = 0 Then
                            strInnerCustCode = txtVendorNo.Value
                        End If

                        Dim dblInnerMRP As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)
                        Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCustCode, strInnerCustCode) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)

                            Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                            Msg = Msg + Environment.NewLine + "UOM: " + strUOM
                            If dblMRP > 0 Then
                                Msg = Msg + Environment.NewLine + "MRP: " + clsCommon.myCstr(dblMRP)
                            End If
                        common.clsCommon.MyMessageBoxShow(Msg, Me.Text)
                            Return False
                        End If
                        If chkItemwise.Checked Then
                            'If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                            '    dblTotalQty += dblInnerQty
                            'End If
                        Else
                            If clsCommon.CompairString(strICodeGrp, strInnerICodeGrp) = CompairStringResult.Equal Then
                                dblTotalQty += dblConvertedQty
                            End If
                        End If
                        If dblTotalQty > dblPendingQty Then
                            common.clsCommon.MyMessageBoxShow("Item Group " + strICodeGrp + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblTotalQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If
                    Next
                End If
        Next
        ' In case of Bill and ship party different 
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If AllowDifferentStateofChildCustomerOnPS = 1 AndAlso chkSameBillShip.Checked = False Then
                Dim IsparentCustomer As String = clsDBFuncationality.getSingleValue("select isnull(Parent_Customer_YN,'N') from tspl_customer_master where Cust_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "'")
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colShipParty).Value), clsCommon.myCstr(txtVendorNo.Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(IsparentCustomer, "Y") = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow("Bill to party and ship to party should be different At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    Return False
                End If
            End If
        Next
            Dim strLocationStatus As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
            If clsCommon.myLen(strLocationStatus) <= 0 Then
                Throw New Exception("From location's states not found")
            End If
            If GSTStatus = False OrElse (chkIsTaxable.Checked AndAlso GSTStatus = True) Then
                Dim qry As String = "select  State from TSPL_CUSTOMER_MASTER where Cust_Code in (" + clsCommon.GetMulcallString(arrCust) + ") group by State"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strFirstChildstate = clsCommon.myCstr(dt.Rows(0)("State"))
                If dt.Rows.Count = 1 Then
                    If AllowDifferentStateofChildCustomerOnPS = 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("State")), strLocationStatus) = CompairStringResult.Equal Then
                            qry = "select Tax_Category from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" + txtBillToLocation.Value + "' and Tax_Type='S' and Tax_Group_Code='" + txtTaxGroup.Value + "'"
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "L") = CompairStringResult.Equal Then
                                Throw New Exception("Tax Group type should be local")
                            End If
                        Else
                            qry = "select Tax_Category from TSPL_LOCATION_WISE_TAX_MASTER where Location_Code='" + txtBillToLocation.Value + "' and Tax_Type='S' and Tax_Group_Code='" + txtTaxGroup.Value + "'"
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "I") = CompairStringResult.Equal Then
                                Throw New Exception("Tax Group type should be Inter State")
                            End If
                        End If
                    End If
                Else
                    Dim firstStateSame As Boolean = False
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii = 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("State")), strLocationStatus) = CompairStringResult.Equal Then
                                firstStateSame = True
                            Else
                                firstStateSame = False
                            End If
                        Else
                            If AllowDifferentStateofChildCustomerOnPS = 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("State")), strLocationStatus) = CompairStringResult.Equal Then
                                    If Not firstStateSame Then
                                        Throw New Exception("State should be same for all customer")
                                    End If
                                Else
                                    If firstStateSame Then
                                        Throw New Exception("State should be different for all customer")
                                    End If
                                End If
                            Else
                                ' Allow different state 
                                If chkSameBillShip.Checked = True Then
                                    If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("State")), strFirstChildstate) = CompairStringResult.Equal Then
                                        Throw New Exception("State should be same for all child customer")
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
                    'Else
                    '    Throw New Exception("Customer states not found")
                End If
            End If
            'GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus = False Then
                Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrICode)
                Dim Item_TaxType As Integer = 0
                If Not (intx = arrICode.Count OrElse intx = 0) Then
                    Throw New Exception("All item should be of Excisable or NonExcisable")
                End If
                If intx > 0 Then
                    Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")"))
                Else
                    Item_TaxType = 0
                End If

                'If clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
                If Item_TaxType = 2 AndAlso clsLocation.isLocatinExcisable(txtBillToLocation.Value) = True Then
                    For Each grow As GridViewRowInfo In gv2.Rows
                        If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colTTaxAutCode).Value + "'")), "Y") = CompairStringResult.Equal Then
                            Throw New Exception("Atleast One tax should be excisable.")
                        Else
                            Exit For
                        End If
                    Next

                End If
            End If
        If AllowtoChangeTCSBaseAmount Then
            If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
            End If
        End If
        UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                If CFormFunction() = False Then
                    Return False
                End If
            End If

            Return True
       
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub
    Function CFormFunction()
        Try
            Dim intQuarter As Integer
            Dim strYear As Integer
            Dim strFromDate As String
            Dim strToDate As String
            ' Dim strSql As String
            Dim StrValidation As String
            Dim intCForm As Integer

            If (Month(txtDate.Value) = 1 OrElse Month(txtDate.Value) = 2 OrElse Month(txtDate.Value) = 3) Then
                intQuarter = 4
            ElseIf (Month(txtDate.Value) = 4 OrElse Month(txtDate.Value) = 5 OrElse Month(txtDate.Value) = 6) Then
                intQuarter = 1
            ElseIf (Month(txtDate.Value) = 7 OrElse Month(txtDate.Value) = 8 OrElse Month(txtDate.Value) = 9) Then
                intQuarter = 2
            ElseIf (Month(txtDate.Value) = 10 OrElse Month(txtDate.Value) = 11 OrElse Month(txtDate.Value) = 12) Then
                intQuarter = 3
            End If

            If intQuarter = 1 Then

                strYear = Year(txtDate.Value) - 1
                '' for 3st quarter
                strFromDate = "01/Jul/" & strYear
                strToDate = "30/Sep/" & strYear
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ", Me.Text)
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 2nd quarter
                strFromDate = "01/Oct/" & strYear
                strToDate = "31/Dec/" & strYear
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 1rd quarter
                strFromDate = "01/Jan/" & Year(txtDate.Value)
                strToDate = "31/Mar/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

            ElseIf intQuarter = 2 Then

                strYear = Year(txtDate.Value) - 1
                '' for 3st quarter
                strFromDate = "01/Oct/" & strYear
                strToDate = "31/Dec/" & strYear
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 2nd quarter
                strFromDate = "01/Jan/" & Year(txtDate.Value)
                strToDate = "31/Mar/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 1rd quarter
                strFromDate = "01/Apr/" & Year(txtDate.Value)
                strToDate = "30/Jun/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If


            ElseIf intQuarter = 3 Then

                strYear = Year(txtDate.Value) - 1
                '' for 3rd quarter
                strFromDate = "01/Jan/" & Year(txtDate.Value)
                strToDate = "31/Mar/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 2nd quarter
                strFromDate = "01/Apr/" & Year(txtDate.Value)
                strToDate = "30/Jun/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 1rd quarter
                strFromDate = "01/Jul/" & Year(txtDate.Value)
                strToDate = "30/Sep/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

            ElseIf intQuarter = 4 Then

                strYear = Year(txtDate.Value) - 1
                '' for 3st quarter
                strFromDate = "01/Apr/" & strYear
                strToDate = "30/Jun/" & strYear
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 2nd quarter
                strFromDate = "01/Jul/" & strYear
                strToDate = "30/Sep/" & strYear
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

                '' for 1rd quarter
                strFromDate = "01/Oct/" & Year(txtDate.Value)
                strToDate = "31/Dec/" & Year(txtDate.Value)
                intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(Document_Code) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Document_Date > ='" & strFromDate & " ' and Document_Date < ='" & strToDate & "' "))
                If intCForm > 0 Then
                    StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
                    If StrValidation = "Warning" Then
                        clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
                    ElseIf StrValidation = "Required Approval" Then
                        intApprovel_Required = 1
                        clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
                    ElseIf StrValidation = "Full Stop" Then
                        clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
                        Return False
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return Nothing
        End Try
    End Function
    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnCopy.Enabled = False
            End If

        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean) As Boolean
        Try
            If (AllowToSave()) Then
                'If AllowWo_Outstanding = False Then
                '    CustomerOutstandingAmount(txtVendorNo.Value, True)
                'End If

                Dim obj As New clsPSSalesOrder()
                obj.IsSameBillShipParty = IIf(chkSameBillShip.Checked, 1, 0)
                obj.RT_RATE = txtrate.Value
                obj.Is_Taxable = IIf(chkIsTaxable.Checked, 1, 0)
                obj.Commission_Apply = IIf(chkCommApply.Checked, 1, 0)
                obj.Total_Comm_Amt = clsCommon.myCdbl(lblCommAmt.Text)
                If txtCustPODate.Checked Then
                    obj.Cust_PODate = txtCustPODate.Value
                End If
                obj.Itemwise = IIf(chkItemwise.Checked, 1, 0)
                obj.Advance_Percentage = txtAdvance.Value
                'obj.Credit_Limit = txtCreditLimit.Value
                obj.CloseRemarks = txtCloseRemarks.Value
                obj.Cust_PO_No = txtPONo.Text
                obj.HeadDisc_Per = txtDiscPer.Value
                If obj.HeadDisc_Per > 0 Then
                    obj.HeadDisc_PerAmt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_Amt = 0
                Else
                    obj.HeadDisc_Amt = lblInvoiceDiscAmt.Text
                    obj.HeadDisc_PerAmt = 0
                End If
                obj.Road_Permit_No = txtRoadPermitNo.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_No = lblVhicleNo.Text
                obj.Vehicle_Capacity = txtVehicleCapacity.Text
                obj.Payment_Terms = ddlPaymentTerms.SelectedValue
                obj.Dispatch_Terms = ddlDispatchTerms.SelectedValue
                obj.Dispatch_Period = txtDispatchPeriod.Value

                obj.Price_Group_Code = txtPriceGroupCode.Text
                obj.Route_No = txtRouteNo.Value
                obj.Route_Desc = lblRouteDesc.Text
                obj.Price_Code = txtPriceCode.Text
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value

                obj.CloseSO = "N"
                If chkclose.Checked = True Then
                    obj.CloseSO = "Y"
                End If

                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                obj.Delivery_date = txtDeliveryDate.Value
                obj.Customer_Code = txtVendorNo.Value
                obj.Customer_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Mode_Of_Transport = cboModeOfTransport.Text
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.SalesOrder_Type = clsCommon.myCstr(cboPOType.SelectedValue)
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text
                obj.PROJECT_ID = fndProject.Value
                obj.Approvel_Required = intApprovel_Required
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
                obj.Abandonment_No = clsCommon.myCdbl(lblAbandonmentNo.Text)
                'obj.Against_Quotation_No = txtReqNo.Value
                obj.Against_Booking_No = txtReqNo.Value




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

                obj.Salesman_Code = txtSalesman.Value
                obj.Salesman_Name = lblSalesman.Text

                obj.Arr = New List(Of clsPSSalesOrderDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsPSSalesOrderDetail()
                    objTr.Converted_Qty = clsCommon.myCdbl(grow.Cells(colConvQty).Value)
                    objTr.Rate_UnitQty = clsCommon.myCdbl(grow.Cells(colRateUnitQty).Value)
                    objTr.Item_Group = clsCommon.myCstr(grow.Cells(colICodeGrp).Value)
                    objTr.RATE_UOM = clsCommon.myCstr(grow.Cells(colUnitRate).Value)
                    objTr.Alternate_UOM = clsCommon.myCstr(grow.Cells(colUnitALter).Value)
                    objTr.TAX_PAID = clsCommon.myCstr(grow.Cells(colTAX_PAID).Value)
                    objTr.TAX_PAID = clsCommon.myCstr(grow.Cells(colTAX_PAID).Value)
                    objTr.BOOK_RATE_UOM = clsCommon.myCstr(grow.Cells(colBOOK_RATE_UOM).Value)
                    objTr.BOOK_QTY_UOM = clsCommon.myCstr(grow.Cells(colBOOK_QTY_UOM).Value)
                    objTr.BOOK_Rate = clsCommon.myCdbl(grow.Cells(colBOOK_Rate).Value)
                    'objTr.RT_Rate = clsCommon.myCdbl(grow.Cells(colRTRate).Value)

                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnit).Value)
                    objTr.OrgRateUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnitRate).Value)
                    'objTr.Quotation_Code = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
                    objTr.Against_Booking_No = clsCommon.myCstr(grow.Cells(colReqistionNo).Value)
                    objTr.Ship_Party = clsCommon.myCstr(grow.Cells(colShipParty).Value)
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    If objTr.Ship_Party = Nothing Then
                        objTr.Ship_Party = clsCommon.myCstr(txtVendorNo.Value)
                    Else
                        objTr.Ship_Party = clsCommon.myCstr(grow.Cells(colShipParty).Value)
                    End If
                    'If objTr.Location = Nothing Then
                    '    objTr.Location = txtBillToLocation.Value
                    'Else
                    '    objTr.Location = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    'End If

                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Manual_Item_Cost = clsCommon.myCdbl(grow.Cells(colManualRate).Value)
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
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
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
                    objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                    objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                    objTr.HeadDiscPer = clsCommon.myCdbl(grow.Cells(colHeaDDisPer).Value)
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(grow.Cells(colHeadDisPerAmt).Value)
                    If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                        objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                        objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                    End If
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
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item", Me.Text)
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
                If clsPSSalesOrder.checkSaveNotification(obj, Nothing) Then
                    Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, isDoAbandomentNo)
                    UcAttachment1.SaveData(obj.Document_Code)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                    Return isSaved
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            Dim obj As New clsPSSalesOrder()
            obj = clsPSSalesOrder.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                btnCopy.Enabled = True
                isNewEntry = False
                btnSave.Text = "Update"
                BlankAllControls()
                chkItemwise.Checked = IIf(obj.Itemwise = 1, True, False)
                chkItemwise.ReadOnly = True
                txtrate.ReadOnly = True
                chkSameBillShip.Enabled = False
                txtAdvance.Value = obj.Advance_Percentage
                If obj.Cust_PODate IsNot Nothing Then
                    txtCustPODate.Value = obj.Cust_PODate
                    txtCustPODate.Checked = True
                End If

                LoadBlankGrid()
                LoadBlankGridTax()
                LoadBlankGridAC()
                cboItemType.Enabled = False
                txtBillToLocation.Enabled = False

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False

                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True


                End If
                chkSameBillShip.Checked = IIf(obj.IsSameBillShipParty = 1, True, False)
                chkIsTaxable.Checked = IIf(obj.Is_Taxable = 1, True, False)
                txtrate.Value = obj.RT_RATE
                chkCommApply.Checked = IIf(obj.Commission_Apply = 1, True, False)
                lblCommAmt.Text = obj.Total_Comm_Amt
                txtCloseRemarks.Value = obj.CloseRemarks
                lblCloseRemarksdesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_remark_master where code='" + txtCloseRemarks.Value + "'")
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                If clsCommon.myLen(obj.Delivery_date) > 0 Then
                    txtDeliveryDate.Value = obj.Delivery_date
                End If
                Dim strNewExpiryDate = clsDBFuncationality.getSingleValue("select New_Expiry_Date from TSPL_EXPIRY_DATE where  Program_Code='" & clsUserMgtCode.frmDeliveryPrderProductSale & "' and Document_No='" & txtDocNo.Value & "'")
                If clsCommon.myLen(strNewExpiryDate) > 0 Then
                    txtDeliveryDate.Value = strNewExpiryDate
                End If
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVhicleNo.Text = obj.Vehicle_No
                ddlDispatchTerms.SelectedValue = obj.Dispatch_Terms
                ddlPaymentTerms.SelectedValue = obj.Payment_Terms
                txtDispatchPeriod.Value = obj.Dispatch_Period
                txtRoadPermitNo.Text = obj.Road_Permit_No
                txtPONo.Text = obj.Cust_PO_No
                txtVendorNo.Value = obj.Customer_Code
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtPriceCode.Text = obj.Price_Code
                txtPriceGroupCode.Text = obj.Price_Group_Code
                txtDate.Enabled = False
                txtVendorNo.Enabled = False
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
                lblVendorName.Text = obj.Customer_Name
                txtRefNo.Text = obj.Ref_No
                cboPOType.SelectedValue = obj.SalesOrder_Type
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                txtVehicleCapacity.Text = obj.Vehicle_Capacity

                '------------------------------------------------
                vaddnew = "N"
                If obj.CloseSO = "Y" Then
                    chkclose.Checked = True
                Else
                    chkclose.Checked = False
                End If
                vaddnew = "Y"
                '-----------------------------------------------

                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtRemarks.Text = obj.Remarks
                cboModeOfTransport.Text = obj.Mode_Of_Transport
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)
                cboItemType.SelectedValue = obj.Item_Type
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                txtDueDate.Value = obj.Due_Date
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

                If obj.Abandonment_No > 0 Then
                    lblAbandonmentNo.Text = clsCommon.myCstr(obj.Abandonment_No)
                    lblAmbendmentNoCaption.Visible = True
                End If
                'txtReqNo.Value = obj.Against_Quotation_No
                txtReqNo.Value = obj.Against_Booking_No
                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    lblProject.Enabled = False
                    fndProject.Enabled = False
                End If
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


                'If clsCommon.myLen(txtDiscAmt.Text) = 0 OrElse clsCommon.myLen(txtDiscPer.Text) <> 0 Then
                '    chkDiscountOnRate.IsChecked = True
                'Else
                '    chkDiscountOnAmt.IsChecked = True
                'End If

                'If clsCommon.CompairString(obj.CreditApproval_Reqd, "Y") = CompairStringResult.Equal And obj.Is_Credit_Approved = 0 Then
                '    clsCommon.MyMessageBoxShow("Approval is required for this order")
                'End If
                'If clsCommon.CompairString(obj.CreditApproval_Reqd, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Credit_Approved, "Y") = CompairStringResult.Equal And obj.Status = 1 Then
                '    txtCreditLimit.Enabled = True
                'End If
                LoadBlankGridTax()
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


                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = obj.Salesman_Name


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsPSSalesOrderDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvQty).Value = objTr.Converted_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRateUnitQty).Value = objTr.Rate_UnitQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = objTr.RATE_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitALter).Value = objTr.Alternate_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommRate).Value = objTr.Commission_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value = objTr.Commission_Party
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommPartyName).Value = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommParty).Value & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCommAmt).Value = objTr.Commission_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmtAfterCOmm).Value = objTr.Amt_Less_Commission
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_PAID).Value = objTr.TAX_PAID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = objTr.Item_Group
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_RATE_UOM).Value = objTr.BOOK_RATE_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_QTY_UOM).Value = objTr.BOOK_QTY_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_Rate).Value = objTr.BOOK_Rate
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRTRate).Value = objTr.RT_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code

                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRequitionQty).Value = objTr.OriginalReqQty
                        If chkItemwise.Checked Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = GetBalanceBookingQty(txtReqNo.Value, objTr.Item_Code)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = GetBalanceBookingQty(txtReqNo.Value, objTr.Item_Group)
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.OrgUnit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnitRate).Value = objTr.OrgRateUnit_code
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = objTr.Quotation_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = objTr.Against_Booking_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colManualRate).Value = objTr.Manual_Item_Cost
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemUsedINGRN).Value = objTr.IsUsedInGRN
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = objTr.Assessable
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = objTr.Item_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = objTr.TotalItem_Weight
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShipParty).Value = objTr.Ship_Party
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShipPartyName).Value = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colShipParty).Value & "'")

                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If

                        'If clsCommon.myLen(objTr.Quotation_Code) > 0 Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsSalesQuotationsDetail.GetBalanceRequitionQty(objTr.Quotation_Code, objTr.Item_Code, obj.Document_Code)
                        'End If

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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objTr.ItemwiseTaxCode
                    Next

                    If obj.Status = ERPTransactionStatus.Approved Then
                        btnAmendment.Enabled = True
                    End If
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                        gvAC.Rows.AddNew()
                    End If

                    'gv1.Rows.AddNew()
                End If
                SetitemWiseTaxOnlySetting()
                ''RefreshReqNo()

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_Code)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Document_Code, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' currencyconversion
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  currencyconversion
                UcAttachment1.LoadData(obj.Document_Code)
            End If

            If chkclose.Checked Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnCopy.Enabled = False
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If True Then
                Dim strCustomer As String = ""
                Try
                    strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
                Catch ex As Exception

                End Try
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
                        gv1.Rows(ii).Cells(colRate).Value = gv1.Rows(ii).Cells(colManualRate).Value
                        UpdateCurrentRow(ii)
                        UpdateCurrentRow(ii)
                    Next
                    SetitemWiseTaxSetting(False, False)
                    If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                        txttcstaxbaseamount.Value = 1
                        txttcstaxbaseamount.Value = 0
                    End If
                    UpdateAllTotals()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()

    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim ReceiptAmount As Double = 0
            Dim AmountLessDiscount As Double = 0
            Dim desc As String = ""
            If (myMessages.postConfirm()) Then
                SavingData(True)
                'If AllowWo_Outstanding = False Then
                '    If CustomerOutstandingAmount(txtVendorNo.Value, False) = False Then Exit Sub
                'End If
                ''richa 28/08/2014 Against Ticket no.BM00000003667
                desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AdvanceAgainstSO, clsFixedParameterCode.AdvanceAgainstSO, Nothing))
                If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then

                    dt = clsDBFuncationality.GetDataTable("Select isnull(Receipt_Amount,0) as Receipt_Amount from TSPL_RECEIPT_HEADER  where SaleOrderNo='" + txtDocNo.Value + "'")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        ReceiptAmount = clsCommon.myCdbl(dt.Rows(0)("Receipt_Amount"))
                        AmountLessDiscount = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                        If ReceiptAmount < AmountLessDiscount Then
                            Throw New Exception("Advance has not been received against this Sale Order No " + txtDocNo.Value + " cannot be post")
                        End If
                    Else
                        Throw New Exception("Please create Receipt Advance against this Sale Order No " + txtDocNo.Value + " ")
                    End If
                End If

                ''=========================================
                If (clsPSSalesOrder.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
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
                common.clsCommon.MyMessageBoxShow(msg, Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)

                If clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                    SMSSENDONLY(True)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
                If (clsPSSalesOrder.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
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

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-ODR"
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
            ''-------richa 30/07/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) = 0 Then
                qst = "select count(*) from TSPL_SD_SALES_ORDER_HEAD where Document_Code='" + txtDocNo.Value + "'"
            Else
                qst = "select count(*) from TSPL_SD_SALES_ORDER_HEAD where Document_Code='" + txtDocNo.Value + "' and TSPL_SD_SALES_ORDER_HEAD.Customer_Code in (" + strwherecls + ")"

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
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        'Dim frm As New frmMandatoryFieldChecker
        'gv1.Enabled = Not frm.CheckmandatoryField(Me)


    End Sub




    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim qry As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------

        qry = "select Document_Code as Code,convert(varchar(10),Document_Date,103)  as Date,Customer_Code as [Customer Code], Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Against_Booking_No as [Booking No],Total_Amt as Amount,case when TSPL_SD_SALES_ORDER_HEAD.status='0' then 'Pending' else 'Approved' end as [Status],Bill_To_Location as [Location],(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =Bill_To_Location ) as [Location Name] from TSPL_SD_SALES_ORDER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALES_ORDER_HEAD.Customer_Code "
        Dim whrClas As String = ""
        '-------richa 30/07/2014 Ticket No. BM00000003242---------

        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALES_ORDER_HEAD.Trans_Type='PS' and  TSPL_SD_SALES_ORDER_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALES_ORDER_HEAD.Trans_Type='PS' "
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_SD_SALES_ORDER_HEAD.Customer_Code in (" + strwherecls + ") and TSPL_SD_SALES_ORDER_HEAD.Trans_Type='PS' "
        Else
            whrClas = " TSPL_SD_SALES_ORDER_HEAD.Trans_Type='PS'"
        End If
        '-----------------------------------------------------
        LoadData(clsCommon.ShowSelectForm("DeliveryCodeFnd", qry, "Code", whrClas, txtDocNo.Value, "Document_Date desc", isButtonClicked), NavigatorType.Current)
        btnCopy.Enabled = False

    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            If gv1.CurrentColumn Is gv1.Columns(colICodeGrp) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
                    isCellValueChangedOpen = False
                    Exit Sub
                End If
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenItemGroupList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(colICode) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select Customer First", Me.Text)
                    isCellValueChangedOpen = False
                    Exit Sub
                End If
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenItemList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenUOMList(True)
                gv1.CurrentColumn = gv1.Columns(colUnit)
                setGridFocus()
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnitALter) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select Main Unit First", Me.Text)
                    isCellValueChangedOpen = False
                    Exit Sub
                End If
                OpenUOMAlterList(True)
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnitRate) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select Main Unit First", Me.Text)
                    isCellValueChangedOpen = False
                    Exit Sub
                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colUnitALter).Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select Main Unit First", Me.Text)
                    isCellValueChangedOpen = False
                    Exit Sub
                End If
                OpenUOMRateList(True)
                If clsCommon.CompairString(gv1.CurrentRow.Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                    CalDiffrate(CInt(gv1.CurrentRow.Index), True, colUnitRate)
                End If
                UpdateCurrentRow(gv1.CurrentRow.Index)
                UpdateCurrentRow(gv1.CurrentRow.Index)
                UpdateAllTotals()
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(colShipParty) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                gv1.CurrentColumn = gv1.Columns(colShipPartyName)
                OpenShipParty(True)
                gv1.CurrentColumn = gv1.Columns(colShipParty)
                isCellValueChangedOpen = False
            ElseIf gv1.CurrentColumn Is gv1.Columns(ColCommParty) AndAlso gv1.CurrentColumn.ReadOnly = False Then
                isCellValueChangedOpen = True
                gv1.CurrentColumn = gv1.Columns(ColCommPartyName)
                OpenCommParty(True)
                gv1.CurrentColumn = gv1.Columns(ColCommParty)
                isCellValueChangedOpen = False
            End If
        End If
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) <= 0 Then
            isCellValueChangedOpen = True
            'If gv1.CurrentColumn Is gv1.Columns(colICode) Then
            '    gv1.CurrentColumn = gv1.Columns(colIName)
            '    If blnBackCalculation = True Then
            '        OpenICodeList(True)
            '    Else
            '        OpenICodeListCurrentCalaculation(True)
            '    End If
            '    gv1.CurrentColumn = gv1.Columns(colICode)
            'End If
            ''setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F7 Then
            SelectRequistionItems()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.T Then
            chkRateDefaultSetting.Visible = Not chkRateDefaultSetting.Visible
            chkRateUserCustomer.Visible = Not chkRateUserCustomer.Visible
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                              "TSPL_SD_SALES_ORDER_HEAD " + Environment.NewLine + _
                              "TSPL_SD_SALES_ORDER_DETAIL " + Environment.NewLine + _
                              "TSPL_EXPIRY_DATE (For update Expiry Date) ")
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        txtTermCode.Value = ClsReceivablePaymentTerms.getFinderWithSaleType(txtTermCode.Value, "S", isButtonClicked)
        lblTermName.Text = ClsReceivablePaymentTerms.GetName(txtTermCode.Value)
        SetTermDetails()


    End Sub
    Private Sub fndVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
        txtVehicleCode.Value = clsCommon.ShowSelectForm("ShipVehicaleFinder", qry, "Code", "", txtVehicleCode.Value, "", isButtonClicked)

        qry = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
        lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = lblVhicleNo.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicleCode.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
            lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "



            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVhicleNo.Text) <= 0 Then
                    lblVhicleNo.Focus()
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

                'lblVhicleNo.Text = txtVehicleCode.Text + "-Hired"
                txtVehicleCode.Text = txtVehicleCode.Value
                Return True
            Else
                txtVehicleCode.Value = String.Empty
                txtVehicleCode.Text = txtVehicleCode.Value
                Return False
            End If
        End If
    End Function
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
        Try
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                Dim strCustomer As String = ""
                Try
                    If AllowDifferentStateofChildCustomerOnPS = 0 Then
                        strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
                    Else
                        If chkSameBillShip.Checked = True Then
                            strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
                        Else
                            strCustomer = ""
                        End If
                    End If

                Catch ex As Exception

                End Try
                If clsCommon.myLen(strCustomer) <= 0 Then
                    strCustomer = txtVendorNo.Value
                End If

                Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
                txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtBillToLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.CompairString(gv1.Rows(ii).Cells(colICodeGrp).Value, "CPD-DESI GHEE") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(ii).Cells(colTAX_PAID).Value, "Yes") = CompairStringResult.Equal Then
                        gv1.Rows(ii).Cells(colRate).Value = gv1.Rows(ii).Cells(colManualRate).Value
                    End If
                Next
                SetTaxDetails()
                If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                    txttcstaxbaseamount.Value = 1
                    txttcstaxbaseamount.Value = 0
                End If
                SetTaxDetails()
            Else
                Throw New Exception("Please select Location First")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub SetTax()
        Dim strCustomer As String = ""
        Try
            If AllowDifferentStateofChildCustomerOnPS = 0 Then
                strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
            Else
                If chkSameBillShip.Checked = True Then
                    strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
                Else
                    strCustomer = ""
                End If
            End If

        Catch ex As Exception

        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtVendorNo.Value
        End If
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If GSTStatus = False OrElse (chkIsTaxable.Checked AndAlso GSTStatus) Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
            If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                txttcstaxbaseamount.Value = 1
                txttcstaxbaseamount.Value = 0
            End If
        End If
    End Sub
    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim strCustomer As String = ""
        Try
            If AllowDifferentStateofChildCustomerOnPS = 0 Then
                strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
            Else
                If chkSameBillShip.Checked = True Then
                    strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)
                Else
                    strCustomer = ""
                End If
            End If
        Catch ex As Exception

        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtVendorNo.Value
        End If
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", strCustomer, txtBillToLocation.Value)
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
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                        If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                            dblOutstandingAmount = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))

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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
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
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"

        Dim strCustomer As String = ""
        Try
            strCustomer = clsCommon.myCstr(gv1.Rows(0).Cells(colShipParty).Value)

        Catch ex As Exception

        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtVendorNo.Value
        End If
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", strCustomer, txtBillToLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If (CalculateTaxRatefromItemwsieTaxOnSale = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal) Then
                            If isChangeRate Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
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
                        gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo, isChangeRate)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
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
                            ''tcs tax rate
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & txtVendorNo.Value & "'")), "0") = CompairStringResult.Equal Then
                                    If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                        dblOutstandingAmount = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(txtVendorNo.Value, txtDate.Value))

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
                            If isChangeRate = True Then
                                If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                                    txttcstaxbaseamount.Value = 1
                                    txttcstaxbaseamount.Value = 0
                                End If

                            End If


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
    Private Sub BlankControlOnCustomer()

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
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtPriceCode.Text = ""
        txtPriceGroupCode.Text = ""
        LoadBlankGrid()
        LoadBlankGridTax()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        btnHistory.Enabled = True
        'If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Location first")
        '    Exit Sub
        'End If
        BlankControlOnCustomer()
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , " & _
        "TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description], " & _
        "Salesman_Code as [Salesman Code],Salesman_Desc as Salesman,TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Number "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        qry += " left outer join TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER on TSPL_RECEIVABLE_PAYMENT_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " & _
        "left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
        "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id "

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        If clsCommon.myLen(strwherecls) = 0 Then
            txtVendorNo.Value = clsCommon.ShowSelectForm("PS-SOCustFndr", qry, "Code", "TSPL_CUSTOMER_MASTER.Status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        Else
            txtVendorNo.Value = clsCommon.ShowSelectForm("PS-SOCustFndr", qry, "Code", "TSPL_CUSTOMER_MASTER.Status='N' and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtVendorNo.Value, "Code", isButtonClicked)

        End If
        '-----------------------------------------------------



        qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Term Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Term Description"))
            'txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
            'lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group Description"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman"))
            txtRouteNo.Value = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            lblRouteDesc.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            txtVehicleCode.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
            lblVhicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))

            txtDate.Enabled = False
            'txtVendorNo.Enabled = False
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
            txtRouteNo.Value = ""
            lblRouteDesc.Text = ""
            txtVehicleCode.Value = ""
            lblVhicleNo.Text = ""
        End If
        '' priti change start here
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
        "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(txtBillToLocation.Value) + "'"
        Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        'If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal Then
        '    strExcise = True
        'Else
        '    strExcise = False
        'End If
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
            'txtVendorNo.Enabled = False

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
        'Dim frm As New frmMandatoryFieldChecker

        'gv1.Enabled = frm.CheckmandatoryField(Me)

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

        'AddNew()
        'LoadBlankGrid()
        'LoadBlankGridTax()
        'gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' and (GIT_Type='' or GIT_Type='N') and MCC_Type='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtBillToLocation.Value = clsCommon.ShowSelectForm("PS-SOLocFndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
        'strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'")) = "T", True, False)
        CalDiffrate(0, False, "")
        Try
            SetTax()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Location first", Me.Text)
            txtBillToLocation.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtVendorNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Customer first", Me.Text)
            txtVendorNo.Focus()
            Exit Sub
        End If


        Dim qry As String = " select TSPL_SHIP_TO_LOCATION.Ship_To_Code as [Code],TSPL_SHIP_TO_LOCATION.Ship_To_Desc as [Description],TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code as[Customer Code] ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_SHIP_TO_LOCATION.Add1,'')='' then '' else TSPL_SHIP_TO_LOCATION.add1 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add2,'')='' then '' else TSPL_SHIP_TO_LOCATION.add2 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add3,'')='' then '' else TSPL_SHIP_TO_LOCATION.add3 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add4,'')='' then '' else TSPL_SHIP_TO_LOCATION.add4 +',' end ,',,',',') as [Ship to Address],TSPL_SHIP_TO_LOCATION.CST_No as [CST NO],TSPL_SHIP_TO_LOCATION.Tin_No as [TIN No]  from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code "
        txtShipToLocation.Value = clsCommon.ShowSelectForm("ShipmentShindrlter", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and loc_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'", txtShipToLocation.Value, "Code", isButtonClicked)
        'txtShipToLocation.Value = clsShipToLocation.getFinder("Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and loc_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'", txtShipToLocation.Value, isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))
    End Sub

    Private Sub btnRequistionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Sub SelectRequistionItems()
        'If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Location first")
        '    Exit Sub
        'End If

        isInsideLoadData = True
        Dim frm As New frmPendingBookingOrderPS()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.ShowDialog()
        LoadBlankGrid()
        Dim objOrderHead As clsBookingMasterProductSale = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).Document_Code) > 0 Then
                objOrderHead = clsBookingMasterProductSale.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)
                If objOrderHead IsNot Nothing AndAlso clsCommon.myLen(objOrderHead.Document_Code) > 0 Then
                    txtBillToLocation.Value = ""
                    lblBillToLocation.Text = ""
                    '' currency details
                    chkIsTaxable.Checked = IIf(objOrderHead.Is_Taxable = 1, True, False)
                    txtCurrencyCode.Value = objOrderHead.CURRENCY_CODE
                    Me.txtConversionRate.Text = objOrderHead.ConvRate
                    If objOrderHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objOrderHead.ApplicableFrom
                    End If
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objOrderHead.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objOrderHead.Description
                    End If
                    If clsCommon.myLen(txtPONo.Text) <= 0 Then
                        txtPONo.Text = objOrderHead.Cust_PO_No
                    End If

                    If (clsCommon.myLen(txtShipToLocation.Value)) <= 0 Then
                        txtShipToLocation.Value = objOrderHead.ShipToLocationName
                    End If
                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objOrderHead.Item_Type
                    End If
                    If (clsCommon.myLen(txtDept.Value) <= 0) Then
                        txtDept.Value = objOrderHead.Dept
                        lblDept.Text = objOrderHead.Dept_Desc
                    End If
                    If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                        txtBillToLocation.Value = objOrderHead.Bill_To_Location
                        lblBillToLocation.Text = objOrderHead.BillToLocationName
                    End If
                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                    End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objOrderHead.Terms_Code
                        lblTermName.Text = objOrderHead.TermsName
                        txtDueDate.Value = objOrderHead.Due_Date
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
                    If txtDispatchPeriod.Value = 0 Then
                        txtDispatchPeriod.Value = objOrderHead.Dispatch_Period
                    End If
                    txtDeliveryDate.Value = objOrderHead.BookValidity_date
                    txtAdvance.Value = objOrderHead.Advance_Percentage
                    If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                        txtTaxGroup.Value = objOrderHead.Tax_Group
                        SetTaxDetails()
                    End If

                    If clsCommon.myLen(txtSalesman.Value) <= 0 Then
                        txtSalesman.Value = objOrderHead.Salesman_Code
                        lblSalesman.Text = objOrderHead.Salesman_Name
                    End If
                    If (clsCommon.myLen(fndProject.Value) <= 0) Then
                        fndProject.Value = objOrderHead.PROJECT_ID
                        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                        fndProject.Enabled = False
                        lblProject.Enabled = False
                    End If

                    Dim qry As String = "select TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Number " & _
                    " from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
                    "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                    " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        txtRouteNo.Value = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                        lblRouteDesc.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                        txtVehicleCode.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                        lblVhicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
                    End If
                    LoadBlankGridAC()

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

            'If GSTStatus = True AndAlso chkIsTaxable.Checked Then
            '    clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "S", txtDate.Value, Nothing)
            'End If

            Dim mrnno As String = ""

            Dim arr As New List(Of String)
            For ii As Integer = 0 To frm.ArrReturn.Count - 1
                If clsCommon.myLen(frm.ArrReturn(ii).Document_Code) > 0 Then
                    Dim strCode As String = frm.ArrReturn(ii).Document_Code
                    'If Not arr.Contains(strCode) Then
                    '    arr.Add(strCode)
                    objOrderHead = clsBookingMasterProductSale.GetData(frm.ArrReturn(ii).Document_Code, NavigatorType.Current)
                    For Each obj As clsBookingDetailProductSale In objOrderHead.Arr
                        If (obj.Item_Code = frm.ArrReturn(ii).Item_Code AndAlso obj.Unit_code = frm.ArrReturn(ii).Unit_code AndAlso obj.Scheme_Item = "N") Then '' OrElse (obj.Scheme_Code = frm.ArrReturn(ii).Scheme_Code AndAlso clsCommon.myLen(obj.Scheme_Code) > 0) Then
                            If IsValidItem(obj) Then
                                gv1.Rows.AddNew()
                                txtReqNo.Value = strCode
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colShipParty).Value = txtVendorNo.Value
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colShipPartyName).Value = lblVendorName.Text
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_RATE_UOM).Value = obj.BOOK_RATE_UOM
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_Rate).Value = obj.Item_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_QTY_UOM).Value = obj.Unit_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_PAID).Value = obj.TAX_PAID
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOOK_RATE_UOM).Value = obj.BOOK_RATE_UOM

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = obj.Document_Code

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = clsDBFuncationality.getSingleValue("select CSA_TYPE from TSPL_ITEM_MASTER where Item_Code='" & obj.Item_Code & "'")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colManualRate).Value = obj.Item_Cost
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = obj.Unit_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnitRate).Value = obj.Unit_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value = obj.Unit_code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnitRate).Value = obj.Unit_code

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = frm.ArrReturn(ii).Balance_Qty

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = GetBalanceBookingQty(txtReqNo.Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value)
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
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                                'Dim dt As DataTable = clsSNShipmentHead.GetOriginalQty(obj.Document_Code, obj.Item_Code, obj.Unit_code, obj.Assessable, obj.MRP, Nothing)
                                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
                                'End If
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

                                findQtyandPromoSchemeCode(False)
                            End If
                        End If

                    Next
                    'End If
                End If
                'mrnno = obj.Document_Code
            Next


            If objOrderHead.Arr IsNot Nothing AndAlso objOrderHead.Arr.Count > 0 Then
                For Each objTr As clsBookingDetailProductSale In objOrderHead.Arr
                    If objTr.Row_Type = "Misc" Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = mrnno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
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
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
    End Sub

    Function IsValidItem(ByVal obj As clsBookingDetailProductSale)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.SOTax_Group
            SetTaxDetails()
        End If
        ''If Not clsCommon.CompairString(txtTaxGroup.Value, obj.MRNTax_Group) = CompairStringResult.Equal Then
        ''    common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " MRN No: " + obj.MRN_No + "  contain Tax Group :" + obj.MRNTax_Group + Environment.NewLine)
        ''    Return False
        ''End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.Document_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Order No : " + obj.Document_Code + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg, Me.Text)
                Return False
            End If
        Next
        Return True
    End Function


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
                    ''New Column for location wise
                    frm.strTaxGroup = txtTaxGroup.Value
                    frm.strTransLocation = txtBillToLocation.Value
                    frm.strTaxType = "S"
                    frm.strVendorCustomerCode = txtVendorNo.Value
                    ''End of New Column for location wise
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
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        End If
                    End If
                ElseIf (gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso (UsLock1.Status = ERPTransactionStatus.Approved)) Then
                    Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                    Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                    If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                        If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            If clsPSSalesOrderDetail.CompletePO(txtDocNo.Value, strICode, intSNo) Then
                                common.clsCommon.MyMessageBoxShow("Successfully Completed", Me.Text)
                                LoadData(txtDocNo.Value, NavigatorType.Current)
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal ChekPostBtn As Boolean) As Boolean
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblAmt As Double = 0
            Dim qry As String = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
           "select SUM(isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE " & _
            "where TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posting_Date is not  null and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
           "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "' ) xxx "

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'"))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType not in ('C')  and SecurityDepositType='S' and Posted='Y' and Cust_Code='" & strCustomer & "'"))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where  Posting_Date is  null and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code <> '" & txtDocNo.Value & "' and Customer_Code='" & strCustomer & "'"))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt


            If dblAmt < lblTotRAmt1.Text AndAlso UsLock1.Status = ERPTransactionStatus.Open Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit), Me.Text)
                Return False
            End If
            If dblAmt < lblTotRAmt1.Text And UsLock1.Status = ERPTransactionStatus.Pending Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow("Please increase credit limit " + clsCommon.myCstr(dblNewCredtitLimit) + " for customer " + txtVendorNo.Value, Me.Text)
                Return False
            End If
            If ChekPostBtn = True Then
                Return True
            End If
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Sale Order No not found to Print", Me.Text)
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()

        Try
            Dim dtBarCode As New DataTable

            dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
            Dim bytes() As Byte
            Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False).[GetType]())
            bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False), GetType(Byte())), Byte())

            '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
            Dim QryShowStatus As String = ""
            Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
            If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
                QryShowStatus = " ,(case when TSPL_SD_SALES_ORDER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as SOStatus "
            Else
                QryShowStatus = ""
            End If

            Dim Qry As String
            Dim FooterText As String
            Dim frm As New frmPurchaseOrder
            frm.strFormId = MyBase.Form_ID
            Qry = ""
            'Qry = "select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "' "
            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'FooterText = dt1.Rows(0).Item("Footer_Text")
            FooterText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "'"))
            Qry = " select TSPL_ITEM_MASTER.HSN_Code,TSPL_LOCATION_MASTER.GSTNO AS Loc_GSTIN_NO,LOC_STATE_MASTER.GST_STATE_Code AS LOC_GST_STATE,TSPL_CUSTOMER_MASTER.GSTNO AS Custoemr_gstin_No,TSPL_STATE_MASTER.GST_STATE_Code as Cust_GST_State, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code as varchar)  else ' ' end + case when len(TSPL_LOCATION_MASTER.Email    )>0 then TSPL_LOCATION_MASTER.Email else '' end +Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End    as lOC_Address,   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address, TSPL_CUSTOMER_MASTER.Cust_Code, (case when TSPL_SD_SALES_ORDER_HEAD.HeadDisc_Per>0 then TSPL_SD_SALES_ORDER_HEAD.HeadDisc_PerAmt else TSPL_SD_SALES_ORDER_HEAD.HeadDisc_Amt end) as Total_Disc_Amt,  convert(varchar,TSPL_SD_SALES_ORDER_HEAD.Document_Date ,103) as SaleOrder_Date,convert(varchar,TSPL_SD_SALES_ORDER_HEAD.Delivery_date,103) as Delivery_date ,TSPL_SD_SALES_ORDER_HEAD.Discount_Base,TSPL_SD_SALES_ORDER_HEAD.Total_Tax_Amt , case when coalesce(TSPL_CUSTOMER_MASTER.Customer_Name ,'')<>'' then TSPL_CUSTOMER_MASTER.Customer_Name else  p_cust.P_cust_name end as CustName, case when coalesce(TSPL_CUSTOMER_MASTER.Add1  ,'')<>'' then TSPL_CUSTOMER_MASTER.Add1  else  p_cust.P_cust_add1 end   as Customer_Add1, case when coalesce(TSPL_CUSTOMER_MASTER.Add2  ,'')<>'' then TSPL_CUSTOMER_MASTER.Add2  else  p_cust.P_cust_add2 end  as   customer_Add2 ,case when coalesce(TSPL_CUSTOMER_MASTER.Add3  ,'')<>'' then TSPL_CUSTOMER_MASTER.Add3  else  p_cust.P_cust_add3 end   as customer_Add3  , case when coalesce(TSPL_CUSTOMER_MASTER.State   ,'')<>'' then TSPL_CUSTOMER_MASTER.State   else  p_cust.P_state  end as customer_State  , case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No    end as P_CSTNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Lst_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_City_Name    end as P_City_Name, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_state_Master.state_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_state_Name    end as P_State_Name, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER.GSTNO       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_gstNo    end as P_GST_No, case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,TSPL_STATE_MASTER .STATE_NAME  as Cust_State_Name , TSPL_LOCATION_MASTER.Location_Desc , TSPL_LOCATION_MASTER.cst_no as Loc_CST,TSPL_LOCATION_MASTER.Add1 as loc_add1 ,TSPL_LOCATION_MASTER.add4 as loc_add4,TSPL_LOCATION_MASTER.Add2 as loc_add2,TSPL_LOCATION_MASTER.Add3 as loc_add3,TSPL_LOCATION_MASTER.Email as loc_email,TSPL_LOCATION_MASTER.TAN_No as loc_TAN_No,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Cust_address,TSPL_CUSTOMER_MASTER.Lst_No as cust_Lst_No,TSPL_CUSTOMER_MASTER.Tin_No as Cust_Tin_No , case when ISNULL(TSPL_LOCATION_MASTER .Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn ,TSPL_SD_SALES_ORDER_HEAD.Document_Code  as Doc_Code,convert(varchar,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103)  as Doc_Date,TSPL_SD_SALES_ORDER_HEAD.Cust_PO_No ,tspl_item_master.Item_Code ,tspl_item_master.Item_Desc  ,TSPL_SD_SALES_ORDER_detail.Qty ,TSPL_SD_SALES_ORDER_detail.RATE_UOM  ,TSPL_SD_SALES_ORDER_detail.Amount ,TSPL_SD_SALES_ORDER_detail.item_cost as Basic_Price,TSPL_SD_SALES_ORDER_HEAD.Created_By ," & _
                 " case when TSPL_SD_SALES_ORDER_HEAD.status=1 then TSPL_SD_SALES_ORDER_HEAD.Modify_By else '' end as Modify_By,TSPL_SD_SALES_ORDER_HEAD.Created_Date ,case when TSPL_SD_SALES_ORDER_HEAD.Payment_Terms ='A' then 'Advanced' else '' end  as Payment_Terms,TSPL_SD_SALES_ORDER_HEAD.Total_Amt , TSPL_SD_SALES_ORDER_HEAD.Discount_Amt as Disc_Amt ,TSPL_SD_SALES_ORDER_HEAD.Amount_Less_Discount as Amt_Less_Discount ,TSPL_SD_SALES_ORDER_detail.Total_Basic_Amt  ,tspl_company_master.Comp_Code ,tspl_company_master.Comp_Name , 	TAX1 .Tax_Code_Desc as tax1name,TSPL_SD_SALES_ORDER_HEAD.TAX1_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax1_amt,0) as txt1amt,        tax2.Tax_Code_Desc as tax2name,TSPL_SD_SALES_ORDER_HEAD.TAX2_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax2_amt,0) as txt2amt,      tax3.Tax_Code_Desc as tax3name,TSPL_SD_SALES_ORDER_HEAD.TAX3_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax3_amt,0) as txt3amt,  tax4.Tax_Code_Desc as tax4name,TSPL_SD_SALES_ORDER_HEAD.TAX4_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name,TSPL_SD_SALES_ORDER_HEAD.TAX5_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax5_amt,0) as txt5amt,  tax6.Tax_Code_Desc as tax6name,TSPL_SD_SALES_ORDER_HEAD.TAX6_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax6_amt,0) as txt6amt,  tax7.Tax_Code_Desc as tax7name,TSPL_SD_SALES_ORDER_HEAD.TAX7_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax7_amt,0) as txt7amt,  tax8.Tax_Code_Desc as tax8name,TSPL_SD_SALES_ORDER_HEAD.TAX8_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax8_amt,0) as txt8amt,   tax9.Tax_Code_Desc as tax9name,TSPL_SD_SALES_ORDER_HEAD.TAX9_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax9_amt,0) as txt9amt,  tax10.Tax_Code_Desc as tax10name,TSPL_SD_SALES_ORDER_HEAD.TAX10_Rate,isnull (TSPL_SD_SALES_ORDER_HEAD.tax10_amt,0) as txt10amt , TSPL_SD_SALES_ORDER_HEAD.Total_Add_Charge,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name1,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name2,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name3,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name4,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name5,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name6,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name7,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name8,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name9,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Name10,isnull (TSPL_SD_SALES_ORDER_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10  ,Consignee_Detail.GSTNO as Consignee_gst_state  ,Consignee_Detail.Customer_Name as Consignee_Name ,Consignee_Detail.Add1 AS Consignee_Add1,Consignee_Detail.Add2 as Consignee_Add2,Consignee_Detail.Add3 as Consignee_Add3,Consignee_Detail.PIN_Code as Consignee_PinCode,Consignee_Detail.Email as Consignee_Email, Consignee_Detail.Fax as Consignee_Fax,case when ISNULL(Consignee_Detail.Phone1,'')='(+__)__________' then '' else Consignee_Detail.Phone1 end +  Case When   ISNULL(Consignee_Detail.Phone2,'')<>'(+__)__________' Then ', '+ Consignee_Detail.Phone2 Else'' End as  Consignee_Phone,TSPL_STATE_MASTER.GST_STATE_Code as Cust_GST_State_Code,TSPL_STATE_MASTER_Consignee.GST_STATE_Code as Consignee_GST_State_Code,TSPL_STATE_MASTER_Loc.GST_STATE_Code as  Loc_GST_Code   from TSPL_SD_SALES_ORDER_detail left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code =TSPL_SD_SALES_ORDER_detail.Document_Code  left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SALES_ORDER_HEAD.Comp_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALES_ORDER_HEAD.Customer_Code left outer join TSPL_CUSTOMER_MASTER as Consignee_Detail on Consignee_Detail.Cust_Code=TSPL_SD_SALES_ORDER_detail.Ship_Party   LEFT join (select Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name,TSPL_STATE_MASTER.STATE_NAME as P_State_Name  ,case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No ,GSTNO as p_gstNo,TSPL_STATE_MASTER.GST_STATE_Code  from TSPL_CUSTOMER_MASTER   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code  " & _
                " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State =TSPL_STATE_MASTER.STATE_CODE    ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_CUSTOMER_MASTER .State  " & _
                " left outer join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location " & _
                " LEFT outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_SD_SALES_ORDER_detail.Item_Code  " & _
                " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                  " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALES_ORDER_HEAD.tax1  " & _
                 " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALES_ORDER_HEAD.tax2  " & _
                "  left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALES_ORDER_HEAD .TAX3 " & _
              " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALES_ORDER_HEAD .tax4   " & _
             " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALES_ORDER_HEAD .tax5  " & _
             " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX6  " & _
             " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX7  " & _
             " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX8 " & _
             " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX9 " & _
                " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX10  " & _
            " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  LEFT OUTER JOIN TSPL_STATE_MASTER AS LOC_STATE_MASTER ON TSPL_LOCATION_MASTER.State=LOC_STATE_MASTER.STATE_CODE  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Loc  ON TSPL_STATE_MASTER_Loc.STATE_CODE  =TSPL_LOCATION_MASTER.State  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_Consignee  ON TSPL_STATE_MASTER_Consignee.STATE_CODE  =Consignee_Detail.state  where 2=2   and  TSPL_SD_SALES_ORDER_HEAD.document_code = '" & txtDocNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dt.Columns.Add("BarCodeImage", GetType(Byte()))
            For Each dr As DataRow In dt.Rows
                dr("BarCodeImage") = bytes
            Next
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptProductSaleOrderReport", "Sale Order product Sale", clsCommon.myCDate(dt.Rows(0)("Doc_Date")))
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_SD_SALES_ORDER_DETAIL where Document_Code='" + strShipFrm + "'   "
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

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then

            e.Cancel = True
        ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item is Used In GRN", Me.Text)
            e.Cancel = True
        End If
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal AndAlso Not (e.Column Is gv1.Columns(colRowType)) Then
                If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAmt) Then
                    gv1.CurrentRow.Cells(e.Column.Name).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(e.Column.Name).ReadOnly = True
                End If
                Exit Sub
            End If
            If e.Column Is gv1.Columns(colICode) Then
                If chkItemwise.Checked = True Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                    End If
                Else
                    gv1.CurrentRow.Cells(colICode).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colRowType) Then
                If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 Then
                    gv1.CurrentRow.Cells(colRowType).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colRowType).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colICodeGrp) Then
                If chkItemwise.Checked Then
                    gv1.CurrentRow.Cells(colICodeGrp).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colICodeGrp).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colUnit) Then
                'If clsCommon.myLen(gv1.CurrentRow.Cells(colReqistionNo).Value) > 0 OrElse clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
                '    gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                'Else
                '    gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                'End If
            ElseIf e.Column Is gv1.Columns(colQty) Then
                gv1.CurrentRow.Cells(colQty).ReadOnly = False
            ElseIf e.Column Is gv1.Columns(colRate) Then
                'If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                '    If chkRateUserCustomer.ToggleState = ToggleState.Indeterminate Then
                '        gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateDefaultSetting.Checked
                '    Else
                '        gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateUserCustomer.Checked
                '    End If
                '    If ItemRateEditable Then
                '        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                '    Else
                '        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                '    End If
                'Else
                '    gv1.CurrentRow.Cells(colRate).ReadOnly = True
                'End If
                If chkItemwise.Checked Then
                    gv1.CurrentRow.Cells(colRate).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(colRate).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colMRP) Then
                'If ItemMRPEditable Then
                '    gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                'Else
                '    gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                'End If
            ElseIf e.Column Is gv1.Columns(colAmt) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(ColCommParty) Then
                If chkCommApply.Checked Then
                    gv1.CurrentRow.Cells(ColCommParty).ReadOnly = False
                    gv1.CurrentRow.Cells(colCommRate).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(ColCommParty).ReadOnly = True
                    gv1.CurrentRow.Cells(colCommRate).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisPer)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colSpecification)

                ''ElseIf gv1.CurrentColumn Is gv1.Columns(colMRP) Then
                ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                ''    gv1.CurrentColumn = gv1.Columns(colAssessableAmt)
                ''ElseIf gv1.CurrentColumn Is gv1.Columns(colAssessableAmt) Then
                ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                ''    gv1.CurrentColumn = gv1.Columns(colSpecification)

            ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRemarks)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        If chkItemwise.Checked = True Then
            SelectRequistionItems()
        Else
            Dim qry = "select  Document_Code as Code,convert(varchar(10),Document_Date,103) as Date,convert(varchar(10),bookvalidity_date,103) as BookingValiditydate,Customer_Code as [Customer Code], Customer_Name as Name, " & _
            "case when Itemwise=0 then 'Groupwise' else 'Itemwise' end as [Item/Grp wise],Payment_Terms as [Payment Term],Dispatch_Terms as [Dispatch Term], " & _
            "Itemwise,Advance_Percentage as [Advance%],TSPL_BOOKING_MASTER_PRODUCTSALE.Salesman_Code as Salesman,TSPL_BOOKING_MASTER_PRODUCTSALE.Salesman_Name as [Salesman Name],Is_Taxable from TSPL_BOOKING_MASTER_PRODUCTSALE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code "
            Dim strwherecls As String = "TSPL_BOOKING_MASTER_PRODUCTSALE.Status=1 and Itemwise=0   and bookvalidity_date >= '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' and TSPL_BOOKING_MASTER_PRODUCTSALE.close_yn='N'  "
            txtReqNo.Value = clsCommon.ShowSelectForm("PS-SOBookFnd", qry, "Code", strwherecls, txtReqNo.Value, "Code", isButtonClicked)
            qry += " where 2=2 and Document_Code ='" + txtReqNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'txtDate.Value = clsCommon.myCstr(dt.Rows(0)("Date"))
                txtVendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Customer Code"))
                lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
                ddlPaymentTerms.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Payment Term"))
                ddlDispatchTerms.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Dispatch Term"))
                chkItemwise.Checked = IIf(clsCommon.myCstr(dt.Rows(0)("ItemWise")) = 1, True, False)
                txtAdvance.Value = clsCommon.myCstr(dt.Rows(0)("Advance%"))
                txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman"))
                lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman Name"))
                txtDeliveryDate.Value = clsCommon.myCstr(dt.Rows(0)("BookingValiditydate"))
                chkIsTaxable.Checked = IIf(clsCommon.myCstr(dt.Rows(0)("Is_Taxable")) = 1, True, False)
                LoadBlankGrid()
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
            End If
        End If
      
        'SelectRequistionItems()
    End Sub

    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = strReqNo
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub PrintAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Sale Order No not found to Print", Me.Text)
        Else
            FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
            '' ''clsCommon.ProgressBarShow()
            '' ''For index As Integer = 1 To Integer.MaxValue - 1

            '' ''Next
            '' ''clsCommon.ProgressBarHide()
        End If
    End Sub

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub

    Private Sub btnAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmendment.Click
        'Try
        '    Dim isDoAbandomentNo As Boolean = False
        '    If UsLock1.Status = ERPTransactionStatus.Approved Then
        '        If common.clsCommon.MyMessageBoxShow("Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '            isDoAbandomentNo = True
        '        End If
        '    End If
        '    Dim IsSavedData As Boolean = SaveData(isDoAbandomentNo)
        '    IsSavedData = IsSavedData AndAlso clsPSSalesOrder.PostData(txtDocNo.Value)

        '    If IsSavedData Then
        '        common.clsCommon.MyMessageBoxShow("Successfully Amendmented")
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub




    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''Printing the amendment
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Purchase Order No not found to Print")
        Else
            FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
        End If
    End Sub

    Dim i As Integer


    Private Function GetItemType() As DataTable
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

    Private Sub gvAC_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
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

    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("PS-SOSalesmanFndr", qry, "Code", whrcls, txtSalesman.Value, "Code", isButtonClicked)
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
    Private Sub chkDiscountOnAmt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDiscountOnAmt.ToggleStateChanged, chkDiscountOnRate.ToggleStateChanged
        If chkDiscountOnAmt.IsChecked Then
            txtDiscAmt.Enabled = True
            txtDiscPer.Enabled = False
            txtDiscPer.Text = 0
            For Each gro As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
                    gro.Cells(colHeaDDisPer).Value = 0
                    gro.Cells(colHeadDisPerAmt).Value = 0
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

    Private Sub txtDiscAmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Click

    End Sub

    Private Sub txtDiscAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Leave
        CalculateDiscountAmount()

    End Sub

    Private Sub txtDiscPer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Click

    End Sub

    Private Sub txtDiscPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave
        CalculateDiscountAmount()
    End Sub
    'Private Sub CalculateDiscountAmount()
    '    Dim discountrate As Decimal
    '    If clsCommon.myCdbl(txtDiscAmt.Text) > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
    '        isCellValueChangedOpen = False
    '        Throw New Exception("Discount amount cannot be greater than Doc amount")

    '    End If
    '    If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
    '        discountrate = Decimal.Parse(txtDiscPer.Text)
    '    Else
    '        txtDiscPer.Text = 0
    '    End If
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
    '        txtDiscPer.Text = 0
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
    '            If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then

    '                dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colAmt).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
    '                gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
    '                gro.Cells(colHeaDDisPer).Value = 0
    '                'discountrate = clsCommon.myCdbl(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(lblAmtWithDiscount.Text)), 2, MidpointRounding.ToEven))
    '                'txtDiscPer.Value = discountrate
    '            Else
    '                'gro.Cells(colHeadDiscamt).Value = 0

    '            End If

    '        Next
    '    Else
    '        txtDiscAmt.Text = 0
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
    '            If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
    '                gro.Cells(colHeaDDisPer).Value = Math.Round((discountrate), 2)
    '                gro.Cells(colHeadDiscamt).Value = 0
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
                'gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 Then
                    If clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then

                        dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colAmt).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
                        gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
                    Else
                        gro.Cells(colHeadDiscamt).Value = 0

                    End If
                End If

            Next
        ElseIf chkDiscountOnRate.IsChecked Then
            For Each gro As GridViewRowInfo In gv1.Rows
                gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colICode).Value) > 0 And clsCommon.myCdbl(gro.Cells(ColFOC).Value) = 0 Then
                    gro.Cells(colHeaDDisPer).Value = Math.Round((discountrate), 2)
                Else

                End If
            Next
        End If

    End Sub

    'Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

    '    LoadBlankGrid()
    '    isInsideLoadData = True
    '    Dim frm As New FrmCopySO()
    '    frm.ShowDialog()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        Dim objReq As clsPSSalesOrder = clsPSSalesOrder.GetData(frm.strFirstSO, NavigatorType.Current)
    '        If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.Document_Code) > 0 Then
    '            If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
    '                txtVendorNo.Value = objReq.Customer_Code
    '                lblVendorName.Text = objReq.Customer_Name
    '            End If
    '            If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
    '                txtBillToLocation.Value = objReq.Bill_To_Location
    '                lblBillToLocation.Text = objReq.BillToLocationName
    '            End If
    '            If (clsCommon.myLen(txtShipToLocation.Value) <= 0) Then
    '                txtShipToLocation.Value = objReq.Ship_To_Location
    '                lblShipToLocation.Text = objReq.ShipToLocationName
    '            End If

    '            If (clsCommon.myLen(txtSalesman.Value) <= 0) Then
    '                txtSalesman.Value = objReq.Salesman_Code
    '                lblSalesman.Text = objReq.Salesman_Name
    '            End If
    '            If (clsCommon.myLen(txtRefNo.Text) <= 0) Then
    '                txtRefNo.Text = objReq.Ref_No

    '            End If

    '            If (clsCommon.myLen(txtRouteNo.Value) <= 0) Then
    '                txtRouteNo.Value = objReq.Route_No
    '                lblRouteNo.Text = objReq.Route_Desc
    '            End If

    '            If (clsCommon.myLen(txtPriceCode.Text) <= 0) Then
    '                txtPriceCode.Text = objReq.Price_Code

    '            End If

    '            If (clsCommon.myLen(txtPriceGroupCode.Text) <= 0) Then
    '                txtPriceGroupCode.Text = objReq.Price_Group_Code

    '            End If
    '            If (clsCommon.myLen(txtReqNo.Value) <= 0) Then
    '                txtReqNo.Value = objReq.Against_Quotation_No

    '            End If
    '            If (clsCommon.myLen(txtDesc.Text) <= 0) Then
    '                txtDesc.Text = objReq.Description

    '            End If
    '            If (clsCommon.myLen(txtPONo.Text) <= 0) Then
    '                txtPONo.Text = objReq.Cust_PO_No

    '            End If
    '            If (clsCommon.myLen(cboModeOfTransport.SelectedValue) <= 0) Then
    '                cboModeOfTransport.SelectedValue = objReq.Mode_Of_Transport

    '            End If

    '            If (clsCommon.myLen(txtDeliveryDate.Value) <= 0) Then
    '                txtDeliveryDate.Value = objReq.Delivery_date

    '            End If
    '            If (clsCommon.myLen(cboPOType.SelectedValue) <= 0) Then
    '                cboPOType.SelectedValue = objReq.SalesOrder_Type

    '            End If
    '            If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
    '                cboItemType.SelectedValue = objReq.Item_Type

    '            End If
    '            If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
    '                cboItemType.SelectedValue = objReq.Item_Type

    '            End If

    '            If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
    '                txtRemarks.Text = objReq.Remarks
    '            End If
    '            If (clsCommon.myLen(txtTaxGroup.Value) <= 0) Then

    '                txtTaxGroup.Value = objReq.Tax_Group
    '                lblTaxGrpName.Text = objReq.TaxGroupName
    '                SetTaxDetails()
    '            End If
    '        End If
    '        If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
    '            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
    '        End If
    '        For Each obj As clsPSSalesOrderDetail In frm.ArrReturn
    '            gv1.Rows.AddNew()
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = frmGRN.RowTypeItem
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
    '            'gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(obj.Item_Code)
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsVendorItemDetail.GetRate(txtVendorNo.Value, obj.Item_Code, obj.Unit_code, obj.MRP)
    '            SetitemWiseTaxSetting(True, True)
    '            UpdateCurrentRow(gv1.Rows.Count - 1)
    '        Next
    '    End If
    '    isInsideLoadData = False
    '    UpdateAllTotals()
    '    RefreshReqNo()
    'End Sub
    'Dim qry As String = "select Document_Code as Code,Document_Date as Date,Customer_Code as Vendor,Total_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status],Bill_To_Location as [Location],(select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code =Bill_To_Location ) as [Location Name] from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE"

    'Dim whrClas As String = ""
    'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '    whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
    'End If
    'txtDocNo.Value = clsCommon.ShowSelectForm("SNSOOrderNoFndd", qry, "Code", whrClas, txtDocNo.Value, "Code", True)
    'LoadData(txtDocNo.Value, NavigatorType.Current)
    'txtDocNo.Value = ""
    'btnSave.Enabled = True
    'btnPost.Enabled = True
    'btnDelete.Enabled = True
    'UsLock1.Status = ERPTransactionStatus.Pending
    'btnSave.Text = "Save"
    'isNewEntry = True
    'End Sub

    'Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
    '    If clsCommon.myLen(txtReqNo.Value) > 0 Then
    '        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.SaleQuotation, txtReqNo.Value)


    '    Else
    '        common.clsCommon.MyMessageBoxShow("No data found")
    '    End If
    'End Sub

    Private Sub RadLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel1.Click

    End Sub

    '--------------------------------------BM00000002443 Done By Monika 30/04/2014----------------------------------'
#Region "New Mail System"
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmDeliveryPrderProductSale
        frm.ShowDialog()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            Return
        End If

        attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            System.Diagnostics.Process.Start(frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order"))
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnsend_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Delivery Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
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
    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
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
    Private Sub btnApproveCreditLimit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveCreditLimit.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS for approval Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_CREDIT_LIMIT_APPROVAL_Detail where Module_Name='FreshSale' "
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
            qry = "update TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE set CreditApproval_Reqd='Y' where Document_No='" & txtDocNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNSalesOrder)

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
            '    attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)
            '    If dt1.Rows.Count > 0 Then
            '        SetItemWiseTax(dt1, txtDocNo.Value)
            '        Dim frmCRV As New frmCrystalReportViewer()
            '        strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order")
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
            Dim strContactperson As String = ""
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDeliveryPrderProductSale + "'", Nothing)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, txtVendorNo.Value)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblVendorName.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, lblTotRAmt.Text)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.frmDeliveryPrderProductSale)

                '------------------------code for attchament-------------------------------------

                Dim strRptPath As String = ""
                attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)
                If dt1.Rows.Count > 0 Then
                    SetItemWiseTax(dt1, txtDocNo.Value)
                    Dim frmCRV As New frmCrystalReportViewer()
                    strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order")
                    frmCRV = Nothing
                    objEmailH.Attachment_1_Path = strRptPath
                End If

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

                objEmailH.SaveData(clsUserMgtCode.frmDeliveryPrderProductSale, objEmailH, Nothing)
                objEmailH = Nothing
                clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
                If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                    SMSSENDONLY(False)
                End If
            Else
                clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            End If
            'sanjay

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub SMSSENDONLY(ByVal isPost As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNSalesOrder)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'If clsCommon.myLen(obj.smsbody) <= 0 Then
            '    Return
            'End If

            'Dim strContactPerson As String = ""

            'Dim strMes As String = obj.smsbody
            'strMes = strMes.Replace("'", " ").Replace("`", "/")

            'If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'End If
            'If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.CustomerNo, txtVendorNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.VendorName) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.CustomerName, lblVendorName.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'End If


            'Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

            'If clsSMSSend.SendSMS(clsUserMgtCode.frmSNSalesOrder, strMes, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")
            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDeliveryPrderProductSale + "'", Nothing)
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerNo, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.frmDeliveryPrderProductSale)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)

                    objSMSH.SaveData(clsUserMgtCode.frmDeliveryPrderProductSale, objSMSH, Nothing)
                    objSMSH = Nothing
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
                    End If
                End If
            End If
            'Sanjay
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
        End If

    End Sub

    Public Function GetAtchmentPrintQuery(ByVal DocNo As String)

        attachqry = " select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate, TSPL_SD_SALES_ORDER_HEAD.Abandonment_No, TSPL_SD_SALES_ORDER_HEAD.Dept_Desc ,TSPL_SD_SALES_ORDER_HEAD.Delivery_date ,TSPL_SD_SALES_ORDER_HEAD.Remarks ,(case when TSPL_SD_SALES_ORDER_HEAD .status =1 then TSPL_SD_SALES_ORDER_HEAD.Modify_By else '' end) as Modify_By ,TSPL_SD_SALES_ORDER_DETAIL.Landing_Cost,TSPL_SD_SALES_ORDER_HEAD.Created_By , "
        attachqry += " TSPL_SD_SALES_ORDER_HEAD.Terms_Code, (TSPL_SD_SALES_ORDER_DETAIL.Total_Tax_Amt /(case when TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount<=0 then 1 else TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount end)) *100 as Tax,((((TSPL_SD_SALES_ORDER_DETAIL.Total_Tax_Amt /(case when TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount<=0 then 1 else TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount end)) *100)*TSPL_SD_SALES_ORDER_DETAIL.Item_Cost/100) +TSPL_SD_SALES_ORDER_DETAIL.Item_Cost) as landing_Rate,(TSPL_SD_SALES_ORDER_DETAIL.Total_Tax_Amt /(case when TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount<=0 then 1 else TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount end) *100) as Tax_Lan,TSPL_SD_SALES_ORDER_HEAD.Mode_Of_Transport as  ModeofTransport,TSPL_SD_SALES_ORDER_DETAIL .Specification as  specification,  "
        attachqry += " TSPL_SD_SALES_ORDER_HEAD.Document_Code as DocNo , convert(varchar(10),TSPL_SD_SALES_ORDER_HEAD .Document_Date,103)  as po_date ,case TSPL_SD_SALES_ORDER_HEAD .SalesOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type , "
        attachqry += " TSPL_SD_SALES_ORDER_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALES_ORDER_DETAIL.Item_Net_Amt as Total_Item_Net_amt,TSPL_SD_SALES_ORDER_DETAIL.MRP ,TSPL_SD_SALES_ORDER_DETAIL.Item_Cost, TSPL_SD_SALES_ORDER_HEAD .Terms_Code as termscode ,TSPL_SD_SALES_ORDER_HEAD .Ref_No as ref_no ,TSPL_SD_SALES_ORDER_HEAD .Comments as comments , "
        attachqry += " TSPL_SD_SALES_ORDER_HEAD .Discount_Amt as dis_amt,TSPL_SD_SALES_ORDER_DETAIL .Disc_Amt  as dis_amt1,TSPL_SD_SALES_ORDER_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_SD_SALES_ORDER_HEAD .Total_Amt as Total_amount,TSPL_SD_SALES_ORDER_HEAD.Discount_Base as bfrdisc_amount, "
        attachqry += " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax1_amt,0) as txt1amt, "
        attachqry += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax2_amt,0) as txt2amt, "
        attachqry += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax3_amt,0) as txt3amt, "
        attachqry += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax4_amt,0) as txt4amt, "
        attachqry += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax5_amt,0) as txt5amt, "
        attachqry += " tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax6_amt,0) as txt6amt, "
        attachqry += " tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax7_amt,0) as txt7amt, "
        attachqry += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax8_amt,0) as txt8amt,  "
        attachqry += " tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax9_amt,0) as txt9amt, "
        attachqry += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SALES_ORDER_HEAD.tax10_amt,0) as txt10amt, "
        attachqry += " isnull(TSPL_SD_SALES_ORDER_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_SD_SALES_ORDER_HEAD.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,TSPL_SD_SALES_ORDER_DETAIL.item_code as item_code, TSPL_ITEM_MASTER.Item_Desc   as itemdesc, TSPL_SD_SALES_ORDER_DETAIL.Row_Type,TSPL_SD_SALES_ORDER_DETAIL.Qty as qty,TSPL_SD_SALES_ORDER_DETAIL.scheme_item as freeitem,TSPL_SD_SALES_ORDER_DETAIL.unit_code as uom,TSPL_SD_SALES_ORDER_DETAIL.item_cost as itemcost,TSPL_SD_SALES_ORDER_DETAIL.amount as amount ,"
        attachqry += " TSPL_LOCATION_MASTER.TIN_No ,ISNULL(TSPL_LOCATION_MASTER.Phone1,'')+ Case When ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Location_Phone ,(Select Add1+Case When ISNULL(Add2,'')='' Then '' else ', '+Add2+ Case When ISNULL(Add3,'')='' Then '' Else ', '+Add3+ Case When ISNULL(Pin_Code,'')='' Then '' else '-'+CONVERT(varchar, Pin_Code) End End End from TSPL_LOCATION_MASTER Where TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location ) as [BillToAddress],TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Cust_address,TSPL_CUSTOMER_MASTER.Lst_No as cust_Lst_No,TSPL_CUSTOMER_MASTER.Tin_No as Cust_Tin_No  ,TSPL_CUSTOMER_MASTER.CST as Cust_Cst,"
        attachqry += " (Select Add1+Case When ISNULL(Add2,'')='' Then '' else ', '+Add2+ Case When ISNULL(Add3,'')='' Then '' Else ', '+Add3+ Case When ISNULL(Pin_Code,'')='' Then '' else '-'+CONVERT(varchar, Pin_Code) End End End from TSPL_LOCATION_MASTER Where TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALES_ORDER_HEAD.Ship_To_Location ) as [ShipToAddress], TSPL_SD_SALES_ORDER_HEAD.TAX1,TSPL_SD_SALES_ORDER_HEAD.TAX2,TSPL_SD_SALES_ORDER_HEAD.TAX3,TSPL_SD_SALES_ORDER_HEAD.TAX4,TSPL_SD_SALES_ORDER_HEAD.TAX5,TSPL_SD_SALES_ORDER_HEAD.Total_Add_Charge "
        attachqry += " from TSPL_SD_SALES_ORDER_DETAIL "
        attachqry += " left outer join TSPL_SD_SALES_ORDER_HEAD  on TSPL_SD_SALES_ORDER_HEAD.Document_Code  =TSPL_SD_SALES_ORDER_DETAIL.Document_Code  "
        attachqry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALES_ORDER_HEAD.tax1 "
        attachqry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALES_ORDER_HEAD.tax2 "
        attachqry += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALES_ORDER_HEAD .TAX3 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALES_ORDER_HEAD .tax4 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALES_ORDER_HEAD .tax5 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX6 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX7 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX8 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX9 "
        attachqry += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALES_ORDER_HEAD .TAX10    "
        attachqry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALES_ORDER_HEAD.comp_code "
        attachqry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALES_ORDER_HEAD.Customer_Code  "
        attachqry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location"
        attachqry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code  where 2=2 "
        attachqry += "  and  TSPL_SD_SALES_ORDER_HEAD.Document_Code = '" + DocNo + "'"


        SetItemWiseTax(clsDBFuncationality.GetDataTable(attachqry), txtDocNo.Value)
        Return attachqry
    End Function


#End Region



    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
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
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim frm As New FrmCrptFooter
        frm.strFormId = MyBase.Form_ID
        frm.ShowDialog()
    End Sub


    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub chkclose_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkclose.ToggleStateChanged
        If vaddnew = "N" Then
            Return
        End If
        If chkclose.Checked Then
            If chkclose.Checked = True AndAlso IsRemarksMandatory = True AndAlso clsCommon.myLen(txtCloseRemarks.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Remarks for Closing Order", Me.Text)
                chkclose.Checked = False
                txtCloseRemarks.Focus()
                Return
            End If
            If Not (common.clsCommon.MyMessageBoxShow("Want To Close Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
        End If

        If Not chkclose.Checked And btnSave.Enabled = False Then
            If Not (common.clsCommon.MyMessageBoxShow("Want To Open Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
        End If

        Dim qry As String = "select count(*) from TSPL_SD_SALES_ORDER_HEAD where document_code='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check <= 0 Then
            clsCommon.MyMessageBoxShow("There is no data found for closed", Me.Text)
            Return
        End If

        Dim obj As New clsPSSalesOrder()
        obj.CloseSO = "N"
        If chkclose.Checked = True Then
            obj.CloseSO = "Y"
        End If

        If clsPSSalesOrder.ClosedData(obj, txtDocNo.Value, txtCloseRemarks.Value) Then
            If chkclose.Checked Then
                clsCommon.MyMessageBoxShow("Sale Order No. " + txtDocNo.Value + " Is Closed Successfully", Me.Text)
                vaddnew = "N"
                chkclose.Checked = True
                vaddnew = "Y"
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                btnCopy.Enabled = False
            End If
            If Not chkclose.Checked And btnSave.Enabled = False Then
                clsCommon.MyMessageBoxShow("Sale Order No. " + txtDocNo.Value + " Is Opened Successfully", Me.Text)
                vaddnew = "N"
                chkclose.Checked = False
                vaddnew = "Y"
                If UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnCopy.Enabled = False
                ElseIf Not UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnCopy.Enabled = True
                End If
            End If
        End If

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


    'Private Sub cboPOType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPOType.SelectedValueChanged
    '    Dim frm As New frmMandatoryFieldChecker

    '    gv1.Enabled = Not frm.CheckmandatoryField(Me)

    'End Sub

    'Private Sub cboItemType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboItemType.SelectedValueChanged
    '    Dim frm As New frmMandatoryFieldChecker
    '    gv1.Enabled = Not frm.CheckmandatoryField(Me)

    'End Sub

    Private Sub txtCloseRemarks__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCloseRemarks._MYValidating
        Dim qry As String = "select Code as Code,description from tspl_remark_master"
        txtCloseRemarks.Value = clsCommon.ShowSelectForm("RemarkCode", qry, "Code", "", txtCloseRemarks.Value, "", isButtonClicked)
        lblCloseRemarksdesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_remark_master where code='" + txtCloseRemarks.Value + "'")
    End Sub



    Private Sub txtDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        SETGSTControl()
    End Sub
    Sub SETGSTControl()
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If (GSTStatus AndAlso chkIsTaxable.Checked = True) OrElse (GSTStatus = False) Then
            txtTaxGroup.Enabled = True
        Else
            txtTaxGroup.Enabled = False
        End If
    End Sub
    Private Sub chkIsTaxable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIsTaxable.ToggleStateChanged
        SETGSTControl()
    End Sub

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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


End Class
