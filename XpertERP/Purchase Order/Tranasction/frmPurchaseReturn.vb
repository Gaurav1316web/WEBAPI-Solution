Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports XpertERPEngine
''Check in by prabhakar on 22/06/2020
Public Class frmPurchaseReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ShowCapexCodeandSubCode As Boolean = False
    Public ShowItemAllStructureWise As Boolean = False
    Public atchqry As String = ""
    Private isCellValueChangedOpen As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colOrgSRNQty As String = "ORGINALSRNQTY"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colQty As String = "COLQTY"
    ''Const colRejectedQty As String = "COLREJECTEDQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colIsInsurance As String = "colIsInsurance"
    Const colInsuranceBaseAmt As String = "colInsuranceBaseAmt"
    Const colInsurancePer As String = "colInsurancePer"
    Const colAmt As String = "COLAMT"
    Const colHeaderDiscountPer As String = "colHeaderDiscountPer"
    Const colHeaderDiscountAmt As String = "colHeaderDiscountAmt"
    Const colDisPer As String = "COLDISPER"
    Const colDetailDisAmt As String = "colDetailDisAmt"

    Const colDisPerUnit As String = "colDisPerUnit"
    Const colDisAmtPerUnit As String = "colDisAmtPerUnit"

    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
    Const colTaxableAmount As String = "colTaxableAmount"
    Const colTaxableAmountPer As String = "colTaxableAmountPer"
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
    Const colLandedRate As String = "LANDEDRATE"
    Const colLandedAmt As String = "LANDEDAMT"
    Const colTaxRecoverable1 As String = "RECOVERTABLETAX1"
    Const colTaxRecoverable2 As String = "RECOVERTABLETAX2"
    Const colTaxRecoverable3 As String = "RECOVERTABLETAX3"
    Const colTaxRecoverable4 As String = "RECOVERTABLETAX4"
    Const colTaxRecoverable5 As String = "RECOVERTABLETAX5"
    Const colTaxRecoverable6 As String = "RECOVERTABLETAX6"
    Const colTaxRecoverable7 As String = "RECOVERTABLETAX7"
    Const colTaxRecoverable8 As String = "RECOVERTABLETAX8"
    Const colTaxRecoverable9 As String = "RECOVERTABLETAX9"
    Const colTaxRecoverable10 As String = "RECOVERTABLETAX10"
    Const colTaxOnBaseAmt1 As String = "colTaxOnBaseAmt1"
    Const colTaxOnBaseAmt2 As String = "colTaxOnBaseAmt2"
    Const colTaxOnBaseAmt3 As String = "colTaxOnBaseAmt3"
    Const colTaxOnBaseAmt4 As String = "colTaxOnBaseAmt4"
    Const colTaxOnBaseAmt5 As String = "colTaxOnBaseAmt5"
    Const colTaxOnBaseAmt6 As String = "colTaxOnBaseAmt6"
    Const colTaxOnBaseAmt7 As String = "colTaxOnBaseAmt7"
    Const colTaxOnBaseAmt8 As String = "colTaxOnBaseAmt8"
    Const colTaxOnBaseAmt9 As String = "colTaxOnBaseAmt9"
    Const colTaxOnBaseAmt10 As String = "colTaxOnBaseAmt10"
    Const colTCSTax1 As String = "colTCSTax1"
    Const colTCSTax2 As String = "colTCSTax2"
    Const colTCSTax3 As String = "colTCSTax3"
    Const colTCSTax4 As String = "colTCSTax4"
    Const colTCSTax5 As String = "colTCSTax5"
    Const colTCSTax6 As String = "colTCSTax6"
    Const colTCSTax7 As String = "colTCSTax7"
    Const colTCSTax8 As String = "colTCSTax8"
    Const colTCSTax9 As String = "colTCSTax9"
    Const colTCSTax10 As String = "colTCSTax10"


    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colPINo As String = "colPINo"

    Const colPO_No As String = "colPO_No"
    Const colGRN_NO As String = "colGRN_NO"
    Const colMRN_NO As String = "colMRN_NO"
    Const colSRN_NO As String = "colSRN_NO"


    Const colUnitTotRecTax As String = "colUnitTotRecTax"
    Const colUnitTotNonRecTax As String = "colUnitTotNonRecTax"
    Const colUnitTotAddCost As String = "colUnitTotAddCost"

    ''Const colLocationCode As String = "LOCATIONCODE"
    ''Const colLocationName As String = "LOCATIONNAME"

    Const colMRP As String = "MRP"
    ''Const colAssessableRate As String = "ASSESSABLERATE"
    Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colBatchNo As String = "BATCHNO"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"
    Const colBinNo As String = "colBinNo"




    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"
    'Const colTIsSurTax As String = "ISSURTAX"
    'Const colTSurTaxCode As String = "SURTAXCODE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Public strDocumentNo As String = ""
    Private isCellValueChangedTaxOpen As Boolean = False
    Dim Return_Without_Invoice As Boolean = False

    ''=================BM00000009405======19/10/2016==========================
    Const colItemACCode1 As String = "COLItemACCODE1"
    Const colItemACAmount1 As String = "COLItemACAMOUNT1"
    Const colItemACCalcAmount1 As String = "COLItemACCalcAMOUNT1"

    Const colItemACCode2 As String = "COLItemACCODE2"
    Const colItemACAmount2 As String = "COLItemACAMOUNT2"
    Const colItemACCalcAmount2 As String = "COLItemACCalcAMOUNT2"

    Const colItemACCode3 As String = "COLItemACCODE3"
    Const colItemACAmount3 As String = "COLItemACAMOUNT3"
    Const colItemACCalcAmount3 As String = "COLItemACCalcAMOUNT3"

    Const colItemACCode4 As String = "COLItemACCODE4"
    Const colItemACAmount4 As String = "COLItemACAMOUNT4"
    Const colItemACCalcAmount4 As String = "COLItemACCalcAMOUNT4"

    Const colItemACCode5 As String = "COLItemACCODE5"
    Const colItemACAmount5 As String = "COLItemACAMOUNT5"
    Const colItemACCalcAmount5 As String = "COLItemACCalcAMOUNT5"

    Const colItemACCode6 As String = "COLItemACCODE6"
    Const colItemACAmount6 As String = "COLItemACAMOUNT6"
    Const colItemACCalcAmount6 As String = "COLItemACCalcAMOUNT6"

    Const colItemACCode7 As String = "COLItemACCODE7"
    Const colItemACAmount7 As String = "COLItemACAMOUNT7"
    Const colItemACCalcAmount7 As String = "COLItemACCalcAMOUNT7"

    Const colItemACCode8 As String = "COLItemACCODE8"
    Const colItemACAmount8 As String = "COLItemACAMOUNT8"
    Const colItemACCalcAmount8 As String = "COLItemACCalcAMOUNT8"

    Const colItemACCode9 As String = "COLItemACCODE9"
    Const colItemACAmount9 As String = "COLItemACAMOUNT9"
    Const colItemACCalcAmount9 As String = "COLItemACCalcAMOUNT9"

    Const colItemACCode10 As String = "COLItemACCODE10"
    Const colItemACAmount10 As String = "COLItemACAMOUNT10"
    Const colItemACCalcAmount10 As String = "COLItemACCalcAMOUNT10"
    Const colItemTotalAdditionalCharge As String = "ColItemAdditionalCHarge"


    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"
    ''==================================================================
    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"

    Const colItemInsuranceBaseAmt As String = "colItemInsuranceBaseAmt"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"
    Const colItemAmtAfterInsurance As String = "colItemAmtAfterInsurance"

    Const colACInsuranceCode As String = "colACInsuranceCode"
    Const colACInsuranceName As String = "colACInsuranceName"
    Const colACInsuranceAmount As String = "colACInsuranceAmount"

    Private PurchaseModulePickFixTaxRate As Boolean = False
    Private SettingAutoRoundOffSeprateAccountOnVendorTransaction As Boolean = False
    Dim SaleInvoiceDate As DateTime
#End Region
    '===update by preeti gupta Against ticket no[ERO/08/04/19-000550]'
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnPurchaseReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False

        'End If
        btncancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub




    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetMailRight()
        btncancel.Enabled = False
        SettingAutoRoundOffSeprateAccountOnVendorTransaction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, Nothing)) = 1)
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        PurchaseModulePickFixTaxRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseModulePickFixTaxRate, clsFixedParameterCode.PurchaseModulePickFixTaxRate, Nothing)) = 1, True, False)
        ShowItemAllStructureWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 1, True, False)
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")

        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()
        LoadBlankGridTax()
        LoadItemType()
        LoadNoteType()
        LoadTransactionType()
        LoadBlankGridAC()
        AddNew()
        SetLength()


        Return_Without_Invoice = IIf(clsCommon.myCdbl(clsPurchaseOrderHead.GetPurchaseSetting().Rows(0).Item("Return_Without_Invoice")) = 1, True, False)
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

        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                txtSubLocation.Enabled = True
            Else
                txtSubLocation.Enabled = False
            End If
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
        End If

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If Not objCommonVar.IsDemoERP Then
            pnlPCJ.Visible = False
        Else
            fndProject.Enabled = False
            lblProject.Enabled = False
        End If
        '' make editable/non editable Term Code
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        btnHistory.Enabled = False
        '==
        RadMenuItem1.Visibility = ElementVisibility.Collapsed
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
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
                strq = "select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(Me.txtVendorNo.Value) & "'"
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
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200
        txtRefNo.MaxLength = 50
        txtCarrier.MaxLength = 50
        txtVehicleNo.MaxLength = 50
        txtGRNo.MaxLength = 50
        txtGENo.MaxLength = 50
        txtVendorInvoiceNo.MaxLength = 30
    End Sub

    Sub LoadItemType()
        If ShowItemAllStructureWise = True Then
            cboItemType.DataSource = GetItemall()
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
            cboItemType.Enabled = False
        Else
            ' cboItemType.DataSource = clsItemMaster.GetItemType()
            Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
        cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
            cboItemType.Enabled = True
        End If
    End Sub

    Public Shared Function GetItemall() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Z"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        Return dt
    End Function
    Public Sub LoadNoteType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Debit Note"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Credit Note"
        dt.Rows.Add(dr)

        CboNoteType.DataSource = dt
        CboNoteType.ValueMember = "Code"
        CboNoteType.DisplayMember = "Name"
    End Sub

    Public Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = If(CboNoteType.SelectedValue = "C", "E", "R")
        dr("Name") = If(CboNoteType.SelectedValue = "C", "Received", "Return")
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Price Adjustment"
        dt.Rows.Add(dr)

        cboTrType.DataSource = dt
        cboTrType.ValueMember = "Code"
        cboTrType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
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
        lblRoundOff.Text = ""
        lblTotRAmt.Text = ""
        lblDocAmount.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtVendorInvoiceNo.Text = ""
        txtCarrier.Text = ""
        txtVehicleNo.Text = ""
        txtGRNo.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value
        lblTaxableAmount.Text = ""
        cboItemType.SelectedIndex = 0
        CboNoteType.SelectedIndex = 0
        cboTrType.SelectedIndex = 0
        If ShowItemAllStructureWise Then
            cboItemType.Enabled = False
        Else
            cboItemType.Enabled = True

        End If
        txtReqNo.Value = ""
        txtBillToLocation.Enabled = True
        rbtnTaxCalAutomatic.IsChecked = True
        chkExciseOnQty.Checked = False
        chkExciseOnQty.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        fndProject.Text = ""
        lblProject.Text = ""
        cboTrType.SelectedIndex = 0
        CboNoteType.SelectedIndex = 0
        chkJobWorkOutward.Checked = False

        ''RICHA AGARWAL AGAINST TICKET NO. BM00000006091 ON 04/05/2015
        txtCurrencyCode.Enabled = True
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 1
        txtApplicableFrom.Text = ""
        ''--------------------------
        txtSubLocation.Enabled = False
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""

        lblAddChargesForInsurance.Text = ""
        lblAddChargesForInsurance1.Text = ""
        lblTotalInsuranceAmt.Text = ""
        btncancel.Enabled = False
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
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)

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
        repoIName.HeaderText = "HSN No/SAC Code"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Item Taxable"
        repoIsSurTax1.Name = colItemTaxable
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Against Item Wise Tax Code"
        repoIName.Name = colAgainstItemWiseTaxCode
        repoIName.IsVisible = False
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

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

        Dim repoOrgSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgSRNQty.FormatString = ""
        repoOrgSRNQty.WrapText = True
        'repoOrgSRNQty.HeaderText = "Original SRN Quantity"
        repoOrgSRNQty.HeaderText = "Invoice Quantity"
        repoOrgSRNQty.Name = colOrgSRNQty
        repoOrgSRNQty.Width = 80
        repoOrgSRNQty.Minimum = 0
        repoOrgSRNQty.ReadOnly = True
        repoOrgSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgSRNQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N3}"
        repoQty.WrapText = True
        repoQty.HeaderText = "Return Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.DecimalPlaces = 10
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        ' repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        gv1.MasterTemplate.Columns.Add(repoRate)

        repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Insurance"
        repoIsSurTax1.Name = colIsInsurance
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1) '30

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Insurance Base Amt"
        repoAmt.Name = colInsuranceBaseAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt) '21

        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Insurance %"
        repoAmt.Name = colInsurancePer
        repoAmt.Width = 100
        repoAmt.Minimum = 0
        repoAmt.Maximum = 100
        repoAmt.ReadOnly = False
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

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
        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisPerUnit As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoDisAmtPerUnit As GridViewDecimalColumn = New GridViewDecimalColumn()

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = "{0:N2}"
        repoDisPer.HeaderText = "Header Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colHeaderDiscountPer
        repoDisPer.IsVisible = False
        repoDisPer.ReadOnly = True
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Header Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colHeaderDiscountAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 80
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDetailDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        repoDisPerUnit = New GridViewDecimalColumn()
        repoDisPerUnit.FormatString = "{0:n2}"
        repoDisPerUnit.HeaderText = "Discount Per Unit"
        repoDisPerUnit.Minimum = 0
        repoDisPerUnit.Maximum = 100
        repoDisPerUnit.Name = colDisPerUnit
        repoDisPerUnit.Width = 80
        repoDisPerUnit.DecimalPlaces = 2
        repoDisPerUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPerUnit)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = "{0:n2}"
        repoDisAmt.HeaderText = "Discount Amt UnitWise"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmtPerUnit
        repoDisAmt.Width = 80
        repoDisAmt.DecimalPlaces = 2
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Total Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)


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


        Dim DecimalCol As New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance Base Amount"
        DecimalCol.Name = colItemInsuranceBaseAmt
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        repoRowType = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Item Insurance Apply On"
        repoRowType.Name = colItemInsuranceApplyOn
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = clsCalculationlApplyON.GetApplyOnType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        repoRowType.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoRowType)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance %"
        DecimalCol.Name = colItemInsurancePer
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Insurance Amount"
        DecimalCol.Name = colItemInsuranceAmt
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(DecimalCol)

        DecimalCol = New GridViewDecimalColumn()
        DecimalCol.FormatString = "{0:N2}"
        DecimalCol.HeaderText = "Item Amount After Insurance"
        DecimalCol.Name = colItemAmtAfterInsurance
        DecimalCol.WrapText = True
        DecimalCol.Width = 80
        DecimalCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        DecimalCol.VisibleInColumnChooser = False
        DecimalCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DecimalCol)


        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Taxable Amount %"
        repoAmtAfterDis.Name = colTaxableAmountPer
        repoAmtAfterDis.WrapText = False
        repoAmtAfterDis.IsVisible = True
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)


        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Taxable Amount"
        repoAmtAfterDis.Name = colTaxableAmount
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 150
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)

        Dim repoAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt.WrapText = True
        repoAssessableAmt.ReadOnly = True
        repoAssessableAmt.FormatString = ""
        repoAssessableAmt.HeaderText = "Assessable Amount"
        repoAssessableAmt.Name = colAssessableAmount
        repoAssessableAmt.IsVisible = False
        repoAssessableAmt.Minimum = 0
        repoAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt)

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

        repoIsSurTax1 = New GridViewCheckBoxColumn()
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 1"
        repoIsTaxable1.Name = colTaxOnBaseAmt1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax1"
        repoIsTaxable1.Name = colTCSTax1
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 2"
        repoIsTaxable1.Name = colTaxOnBaseAmt2
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax2"
        repoIsTaxable1.Name = colTCSTax2
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 3"
        repoIsTaxable1.Name = colTaxOnBaseAmt3
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax3"
        repoIsTaxable1.Name = colTCSTax3
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 4"
        repoIsTaxable1.Name = colTaxOnBaseAmt4
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax4"
        repoIsTaxable1.Name = colTCSTax4
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 5"
        repoIsTaxable1.Name = colTaxOnBaseAmt5
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax5"
        repoIsTaxable1.Name = colTCSTax5
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 6"
        repoIsTaxable1.Name = colTaxOnBaseAmt6
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax6"
        repoIsTaxable1.Name = colTCSTax6
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 7"
        repoIsTaxable1.Name = colTaxOnBaseAmt7
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax7"
        repoIsTaxable1.Name = colTCSTax7
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 8"
        repoIsTaxable1.Name = colTaxOnBaseAmt8
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax8"
        repoIsTaxable1.Name = colTCSTax8
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 9"
        repoIsTaxable1.Name = colTaxOnBaseAmt9
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax9"
        repoIsTaxable1.Name = colTCSTax9
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

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 10"
        repoIsTaxable1.Name = colTaxOnBaseAmt10
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "TCS Tax10"
        repoIsTaxable1.Name = colTCSTax10
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
        repoRequition.HeaderText = "PI No"
        repoRequition.Name = colPINo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)

        repoRequition = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "PO No"
        repoRequition.Name = colPO_No
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)
        repoRequition = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "GRN No"
        repoRequition.Name = colGRN_NO
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)
        repoRequition = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "MRN No"
        repoRequition.Name = colMRN_NO
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)
        repoRequition = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "SRN No"
        repoRequition.Name = colSRN_NO
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoMRP)

        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable = New GridViewDecimalColumn()
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

        Dim repoLandedRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedRate.FormatString = ""
        repoLandedRate.HeaderText = "Landed Rate"
        repoLandedRate.Name = colLandedRate
        repoLandedRate.WrapText = True
        repoLandedRate.Width = 80
        repoLandedRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedRate)

        Dim repoLandedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedAmt.FormatString = ""
        repoLandedAmt.HeaderText = "Landed Amount"
        repoLandedAmt.Name = colLandedAmt
        repoLandedAmt.WrapText = True
        repoLandedAmt.Width = 80
        repoLandedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedAmt)


        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable1)


        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable2)

        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable3)

        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable4)

        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable5)

        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable6)

        Dim repoUnitTotNonRecTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotNonRecTax.FormatString = ""
        repoUnitTotNonRecTax.HeaderText = "Total Non-Recovered Tax Per Unit"
        repoUnitTotNonRecTax.Name = colUnitTotNonRecTax
        repoUnitTotNonRecTax.IsVisible = False
        repoUnitTotNonRecTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotNonRecTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotNonRecTax)


        Dim repoUnitTotRecTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotRecTax.FormatString = ""
        repoUnitTotRecTax.HeaderText = "Total Recovered Tax Per Unit"
        repoUnitTotRecTax.Name = colUnitTotRecTax
        repoUnitTotRecTax.IsVisible = False
        repoUnitTotRecTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotRecTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotRecTax)


        Dim repoUnitTotAddCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitTotAddCost.FormatString = ""
        repoUnitTotAddCost.HeaderText = "Total Addtional Cost Per Unit"
        repoUnitTotAddCost.Name = colUnitTotAddCost
        repoUnitTotAddCost.IsVisible = False
        repoUnitTotAddCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitTotAddCost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitTotAddCost)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        ''============19/10/2016--------------additional charge columns============================
        Dim repoWeightUOMMT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code1"
        repoWeightUOMMT.Name = colItemACCode1
        repoWeightUOMMT.Width = 150
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        Dim repoItemWeightMT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Org Amt1"
        repoItemWeightMT.Name = colItemACAmount1
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt1"
        repoItemWeightMT.Name = colItemACCalcAmount1
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code2"
        repoWeightUOMMT.Name = colItemACCode2
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt2"
        repoItemWeightMT.Name = colItemACAmount2
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt2"
        repoItemWeightMT.Name = colItemACCalcAmount2
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code3"
        repoWeightUOMMT.Name = colItemACCode3
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt3"
        repoItemWeightMT.Name = colItemACAmount3
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt3"
        repoItemWeightMT.Name = colItemACCalcAmount3
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code4"
        repoWeightUOMMT.Name = colItemACCode4
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt4"
        repoItemWeightMT.Name = colItemACAmount4
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt4"
        repoItemWeightMT.Name = colItemACCalcAmount4
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code5"
        repoWeightUOMMT.Name = colItemACCode5
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt5"
        repoItemWeightMT.Name = colItemACAmount5
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt5"
        repoItemWeightMT.Name = colItemACCalcAmount5
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code6"
        repoWeightUOMMT.Name = colItemACCode6
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt6"
        repoItemWeightMT.Name = colItemACAmount6
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt6"
        repoItemWeightMT.Name = colItemACCalcAmount6
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code7"
        repoWeightUOMMT.Name = colItemACCode7
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt7"
        repoItemWeightMT.Name = colItemACAmount7
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt7"
        repoItemWeightMT.Name = colItemACCalcAmount7
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code8"
        repoWeightUOMMT.Name = colItemACCode8
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt8"
        repoItemWeightMT.Name = colItemACAmount8
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt8"
        repoItemWeightMT.Name = colItemACCalcAmount8
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code9"
        repoWeightUOMMT.Name = colItemACCode9
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt9"
        repoItemWeightMT.Name = colItemACAmount9
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt9"
        repoItemWeightMT.Name = colItemACCalcAmount9
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoWeightUOMMT = New GridViewTextBoxColumn()
        repoWeightUOMMT.FormatString = ""
        repoWeightUOMMT.HeaderText = "Additional Charge Code10"
        repoWeightUOMMT.Name = colItemACCode10
        repoWeightUOMMT.Width = 50
        repoWeightUOMMT.ReadOnly = True
        repoWeightUOMMT.WrapText = True
        repoWeightUOMMT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoWeightUOMMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Additional Org Amt10"
        repoItemWeightMT.Name = colItemACAmount10
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 150
        repoItemWeightMT.HeaderText = "Additional Calc Amt10"
        repoItemWeightMT.Name = colItemACCalcAmount10
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        repoItemWeightMT = New GridViewDecimalColumn()
        repoItemWeightMT.WrapText = True
        repoItemWeightMT.ReadOnly = True
        repoItemWeightMT.FormatString = "{0:n3}"
        repoItemWeightMT.Width = 50
        repoItemWeightMT.HeaderText = "Total ItemAdditional Amt"
        repoItemWeightMT.Name = colItemTotalAdditionalCharge
        repoItemWeightMT.IsVisible = False
        repoItemWeightMT.Minimum = 0
        gv1.MasterTemplate.Columns.Add(repoItemWeightMT)

        ''done by stuti on 20/10/2016 against purchase points
        Dim repoCategoryType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        repoCategoryType.DataSource = Xtra.GetCapexCombo()
        repoCategoryType.ValueMember = "Code"
        repoCategoryType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoCategoryType)

        Dim repoEmergency As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoEmergency.Checked = ToggleState.Off
        repoEmergency.HeaderText = "Emergency"
        repoEmergency.Name = colEmergency
        repoEmergency.Width = 50
        repoEmergency.IsVisible = ShowCapexCodeandSubCode
        repoEmergency.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoEmergency)

        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        repoCapexSubCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexSubCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        repoCapexCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCapexCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexCode)
        ''==============================================================================================

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
    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeItem
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeMisc
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
            frm.strCurrDocType = "Purchase Return"
            frm.strAgaintsDocNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colPINo).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
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


        Dim repoTaxAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAssessableAmt.FormatString = ""
        repoTaxAssessableAmt.HeaderText = "Assessable Amount"
        repoTaxAssessableAmt.Name = colTTaxAssessableAmt
        repoTaxAssessableAmt.Width = 100
        repoTaxAssessableAmt.ReadOnly = True
        repoTaxAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAssessableAmt)

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
        repoTaxRate.ReadOnly = False
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
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colCategoryType) OrElse e.Column Is gv1.Columns(colCapexSubCode) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colDisPerUnit) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) Then
                        If (e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse (clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colRate) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPerUnit) = CompairStringResult.Equal)) Then
                            If (clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) > 0) Then
                                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                If dblEnteredQty > dblPendingQty Then
                                    common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                    gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                                End If
                            End If

                            If e.Column Is gv1.Columns(colQty) Then
                                OpenSerialItem()
                            End If
                            'Asked By Ranjana mam(12-Nov-2020)
                            If (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) > 0) Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                                    Dim TempCost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Amount from TSPL_PI_DETAIL where  pi_no='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colPINo).Value) & "' and Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'"))
                                    If gv1.CurrentRow.Cells(colAmt).Value > TempCost Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Extended Cost can not be greater than amount define in PI", Me.Text)
                                        gv1.CurrentRow.Cells(colAmt).Value = TempCost
                                    End If
                                End If
                            End If

                            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf (clsCommon.CompairString(e.Column.Name, colICode) = CompairStringResult.Equal) Then
                            ''OpenICodeList(False)
                        End If

                        'If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                        '    Dim stockqty As Double = 0
                        '    If clsCommon.myLen(txtBillToLocation.Value) > 0 And clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                        '        Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + txtBillToLocation.Value + "' "
                        '        stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))


                        '        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > stockqty Then
                        '            common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed.Availabe Qty : " + clsCommon.myCstr(stockqty))
                        '            gv1.CurrentRow.Cells(colQty).Value = 0
                        '        End If

                        '    Else
                        '        common.clsCommon.MyMessageBoxShow("Select the Location")
                        '        gv1.CurrentRow.Cells(colQty).Value = 0
                        '    End If
                        'End If


                        If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                            If txtReqNo.Value = "" Then
                                OpenICodeList(False)
                            End If
                        ElseIf e.Column Is gv1.Columns(colCategoryType) Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colCategoryType).Value, "Capex") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colCapexSubCode).ReadOnly = False
                                gv1.CurrentRow.Cells(colCapexCode).ReadOnly = False
                            Else
                                gv1.CurrentRow.Cells(colCapexSubCode).ReadOnly = True
                                gv1.CurrentRow.Cells(colCapexCode).ReadOnly = True
                                gv1.CurrentRow.Cells(colCapexSubCode).Value = ""
                                gv1.CurrentRow.Cells(colCapexCode).Value = ""
                            End If
                        ElseIf e.Column Is gv1.Columns(colCapexSubCode) Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colCategoryType).Value, "Capex") = CompairStringResult.Equal Then
                                OpenCapexSubCodeList()
                            End If
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                            If txtReqNo.Value = "" Then
                                openuom(False)
                                UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                                If rbtnTaxCalManual.IsChecked Then
                                    For ii As Integer = 0 To gv1.Rows.Count - 1
                                        UpdateCurrentRow(ii)
                                    Next
                                End If
                                UpdateAllTotals()
                            End If

                        End If


                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub openuom(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            '' Added By abhishek as on 29 june 2012  when we change UOM type at run time then according to UOM Item rate will be change ----
            If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then

                Dim Itemrate As Double
                Dim VendrItemRate As Double
                Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
                If objVItem IsNot Nothing Then
                    gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
                    gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
                Else
                    Dim Rateqry As String = "select Item_Rate,MRP from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + txtVendorNo.Value + "' and item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM='FC' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Rateqry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        VendrItemRate = clsCommon.myCdbl(dt.Rows(0)("Item_Rate"))
                        Dim conversionFact As Double = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(strICode, gv1.CurrentRow.Cells(colUnit).Value, Nothing))
                        If VendrItemRate <> 0 Then
                            Itemrate = clsCommon.myCdbl(clsCommon.myCdbl(VendrItemRate) / clsCommon.myCdbl(conversionFact))
                            gv1.CurrentRow.Cells(colRate).Value = Math.Round(Itemrate, 2)
                        End If
                    End If

                End If

            End If
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim itype As String = String.Empty
        '' Anubhooti 13-Mar-2015 (Item type validation should consider all fields of cboitemtype)
        'If cboItemType.Text = "Finished Goods" Then
        '    itype = "F"
        'ElseIf cboItemType.Text = "RM Other" Then
        '    itype = "RO"
        'Else
        '    gv1.CurrentRow.Cells(colICode).Value = ""
        '    common.clsCommon.MyMessageBoxShow("Select the Item Type")
        '    Exit Sub
        'End If
        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            gv1.CurrentRow.Cells(colICode).Value = ""
            common.clsCommon.MyMessageBoxShow(Me, "Select the Item Type", Me.Text)
            Exit Sub
        End If

        Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemBoth(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), itype, isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
            'gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            'gv1.CurrentRow.Cells(colRate).Value = obj.price
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colHSNNo).Value = ""
            gv1.CurrentRow.Cells(colItemTaxable).Value = False
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0
        End If
        SetitemWiseTaxSetting(True, True)
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

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
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
                    gv1.CurrentColumn = gv1.Columns(colRate)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colDisPer)

                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colMRP)
                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colMRP) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colBatchNo)
                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colBatchNo) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colExpiry) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colManufactureDate) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
                    ''ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                    ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    ''    gv1.CurrentColumn = gv1.Columns(colRemarks)

                ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                    If Not (intCurrRow = gv1.Rows.Count - 1) Then
                        gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    End If
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''Sub OpenICodeList(ByVal isButtonClick As Boolean)
    ''    If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
    ''        common.clsCommon.MyMessageBoxShow("Please select Item Type")
    ''        gv1.CurrentRow.Cells(colICode).Value = ""
    ''        gv1.CurrentRow.Cells(colIName).Value = ""
    ''        gv1.CurrentRow.Cells(colUnit).Value = ""
    ''        cboItemType.Focus()
    ''        Exit Sub
    ''    End If

    ''    Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), isButtonClick)
    ''    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
    ''        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
    ''        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
    ''        gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
    ''    Else
    ''        gv1.CurrentRow.Cells(colICode).Value = ""
    ''        gv1.CurrentRow.Cells(colIName).Value = ""
    ''        gv1.CurrentRow.Cells(colUnit).Value = ""
    ''    End If
    ''    SetitemWiseTaxSetting(True, True)

    ''End Sub
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
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
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
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            End If
        Next
    End Sub

    Private Sub UpdateAllTotals()
        Dim isInsuranceExists As Boolean = False
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colPINo).Value) <= 0 Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                            isInsuranceExists = True
                            Exit For
                        End If
                    End If
                End If
            End If
        Next

        If isInsuranceExists Then
            Dim dblTotalInsuranceBaseAmt As Decimal = 0
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                        dblTotalInsuranceBaseAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    End If
                End If
            Next
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsInsurance).Value) Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colPINo).Value) <= 0 Then
                                gv1.Rows(ii).Cells(colInsuranceBaseAmt).Value = dblTotalInsuranceBaseAmt
                                UpdateCurrentRow(ii)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotalQty As Double = Nothing
        Dim dblTaxAssessableAmt As Double = 0
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
        Dim dblItemInsuranceAmt As Decimal = 0
        Dim dblTaxableAmount As Decimal = 0

        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTaxableAmount = dblTaxableAmount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxableAmount).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

                dblTotalQty = dblTotalQty + clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)

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

                dblTaxAssessableAmt = dblTaxAssessableAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableAmount).Value)
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
                dblItemInsuranceAmt = dblItemInsuranceAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsuranceAmt).Value)
                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
            End If
        Next


        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If chkExciseOnQty.Checked Then
                            If dblTaxAssessableAmt <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxAssessableAmt, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        ElseIf dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = Math.Round(dblTaxAssessableAmt, 2)
                    Case 2
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 3
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 4
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                End Select
            Next
        Else
            For ii As Integer = 1 To gv2.Rows.Count
                gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblAmtAfterDis, 2)
            Next
        End If

        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
        Next

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTotalInsuranceAmt.Text = clsCommon.myFormat(dblItemInsuranceAmt)
        lblTaxableAmount.Text = clsCommon.myFormat(dblTaxableAmount)
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        dblNetAmt = dblNetAmt + dblACAmount
        Dim dclROAmt As Decimal = 0
        If SettingAutoRoundOffSeprateAccountOnVendorTransaction Then
            dclROAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero) - dblNetAmt
            dblNetAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero)
        End If
        lblRoundOff.Text = clsCommon.myFormat(dclROAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblDocAmount.Text = lblTotRAmt.Text
        Calc_AddtionalCharge_Itemwise(dblTotalQty)
    End Sub

#Region "item level additional charge calculation"
    Private Sub Calc_AddtionalCharge_Itemwise(ByVal TotalQty As Double)
        Try
            TotalQty = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                    TotalQty += clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)
                End If
            Next


            Dim add_code1 As String = ""
            Dim add_amt1 As Double = Nothing
            Dim add_code2 As String = ""
            Dim add_amt2 As Double = Nothing
            Dim add_code3 As String = ""
            Dim add_amt3 As Double = Nothing
            Dim add_code4 As String = ""
            Dim add_amt4 As Double = Nothing
            Dim add_code5 As String = ""
            Dim add_amt5 As Double = Nothing
            Dim add_code6 As String = ""
            Dim add_amt6 As Double = Nothing
            Dim add_code7 As String = ""
            Dim add_amt7 As Double = Nothing
            Dim add_code8 As String = ""
            Dim add_amt8 As Double = Nothing
            Dim add_code9 As String = ""
            Dim add_amt9 As Double = Nothing
            Dim add_code10 As String = ""
            Dim add_amt10 As Double = Nothing
            ''==========================================================================================
            If gvAC.Rows.Count > 0 Then
                If gvAC.Rows.Count > 0 AndAlso clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                    add_code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                    add_amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 1 AndAlso clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                    add_code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                    add_amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 2 AndAlso clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                    add_code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                    add_amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 3 AndAlso clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                    add_code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                    add_amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 4 AndAlso clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                    add_code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                    add_amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 5 AndAlso clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                    add_code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                    add_amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 6 AndAlso clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                    add_code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                    add_amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 7 AndAlso clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                    add_code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                    add_amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 8 AndAlso clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                    add_code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                    add_amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                End If
                If gvAC.Rows.Count > 9 AndAlso clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                    add_code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                    add_amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                End If
            End If ''additional head level grid
            ''==========================================================================================
            Dim LastIndex As Integer = 0
            Dim TotalAmt1 As Double = Nothing
            Dim TotalAmt2 As Double = Nothing
            Dim TotalAmt3 As Double = Nothing
            Dim TotalAmt4 As Double = Nothing
            Dim TotalAmt5 As Double = Nothing
            Dim TotalAmt6 As Double = Nothing
            Dim TotalAmt7 As Double = Nothing
            Dim TotalAmt8 As Double = Nothing
            Dim TotalAmt9 As Double = Nothing
            Dim TotalAmt10 As Double = Nothing
            Dim qty As Double = Nothing

            For Each grow As GridViewRowInfo In gv1.Rows
                qty = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value) 'clsCommon.myCdbl(grow.Cells(colQty).Value)
                ''=======================code=============================
                grow.Cells(colItemACCode1).Value = add_code1
                grow.Cells(colItemACCode2).Value = add_code2
                grow.Cells(colItemACCode3).Value = add_code3
                grow.Cells(colItemACCode4).Value = add_code4
                grow.Cells(colItemACCode5).Value = add_code5
                grow.Cells(colItemACCode6).Value = add_code6
                grow.Cells(colItemACCode7).Value = add_code7
                grow.Cells(colItemACCode8).Value = add_code8
                grow.Cells(colItemACCode9).Value = add_code9
                grow.Cells(colItemACCode10).Value = add_code10

                grow.Cells(colItemACAmount1).Value = System.Math.Round(add_amt1, 3)
                grow.Cells(colItemACAmount2).Value = System.Math.Round(add_amt2, 3)
                grow.Cells(colItemACAmount3).Value = System.Math.Round(add_amt3, 3)
                grow.Cells(colItemACAmount4).Value = System.Math.Round(add_amt4, 3)
                grow.Cells(colItemACAmount5).Value = System.Math.Round(add_amt5, 3)
                grow.Cells(colItemACAmount6).Value = System.Math.Round(add_amt6, 3)
                grow.Cells(colItemACAmount7).Value = System.Math.Round(add_amt7, 3)
                grow.Cells(colItemACAmount8).Value = System.Math.Round(add_amt8, 3)
                grow.Cells(colItemACAmount9).Value = System.Math.Round(add_amt9, 3)
                grow.Cells(colItemACAmount10).Value = System.Math.Round(add_amt10, 3)
                ''=============amount=========================================
                If TotalQty > 0 Then
                    grow.Cells(colItemACCalcAmount1).Value = System.Math.Round((qty * add_amt1) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount2).Value = System.Math.Round((qty * add_amt2) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount3).Value = System.Math.Round((qty * add_amt3) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount4).Value = System.Math.Round((qty * add_amt4) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount5).Value = System.Math.Round((qty * add_amt5) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount6).Value = System.Math.Round((qty * add_amt6) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount7).Value = System.Math.Round((qty * add_amt7) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount8).Value = System.Math.Round((qty * add_amt8) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount9).Value = System.Math.Round((qty * add_amt9) / TotalQty, 3)
                    grow.Cells(colItemACCalcAmount10).Value = System.Math.Round((qty * add_amt10) / TotalQty, 3)

                    TotalAmt1 = System.Math.Round(TotalAmt1 + System.Math.Round((qty * add_amt1) / TotalQty, 3), 3)
                    TotalAmt2 = System.Math.Round(TotalAmt2 + System.Math.Round((qty * add_amt2) / TotalQty, 3), 3)
                    TotalAmt3 = System.Math.Round(TotalAmt3 + System.Math.Round((qty * add_amt3) / TotalQty, 3), 3)
                    TotalAmt4 = System.Math.Round(TotalAmt4 + System.Math.Round((qty * add_amt4) / TotalQty, 3), 3)
                    TotalAmt5 = System.Math.Round(TotalAmt5 + System.Math.Round((qty * add_amt5) / TotalQty, 3), 3)
                    TotalAmt6 = System.Math.Round(TotalAmt6 + System.Math.Round((qty * add_amt6) / TotalQty, 3), 3)
                    TotalAmt7 = System.Math.Round(TotalAmt7 + System.Math.Round((qty * add_amt7) / TotalQty, 3), 3)
                    TotalAmt8 = System.Math.Round(TotalAmt8 + System.Math.Round((qty * add_amt8) / TotalQty, 3), 3)
                    TotalAmt9 = System.Math.Round(TotalAmt9 + System.Math.Round((qty * add_amt9) / TotalQty, 3), 3)
                    TotalAmt10 = System.Math.Round(TotalAmt10 + System.Math.Round((qty * add_amt10) / TotalQty, 3), 3)
                End If

                grow.Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
            Next

            ''================check if grid amount not equal to header amount then adjust it on last item row==============
            If gv1.Rows.Count > 0 AndAlso TotalAmt1 > add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) - (TotalAmt1 - add_amt1), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt1 < add_amt1 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount1).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount1).Value) + (add_amt1 - TotalAmt1), 3)
            End If
            ''2.
            If gv1.Rows.Count > 0 AndAlso TotalAmt2 > add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) - (TotalAmt2 - add_amt2), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt2 < add_amt2 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount2).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount2).Value) + (add_amt2 - TotalAmt2), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt3 > add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) - (TotalAmt3 - add_amt3), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt3 < add_amt3 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount3).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount3).Value) + (add_amt3 - TotalAmt3), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt4 > add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) - (TotalAmt4 - add_amt4), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt4 < add_amt4 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount4).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount4).Value) + (add_amt4 - TotalAmt4), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt5 > add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) - (TotalAmt5 - add_amt5), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt5 < add_amt5 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount5).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount5).Value) + (add_amt5 - TotalAmt5), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt6 > add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) - (TotalAmt6 - add_amt6), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt6 < add_amt6 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount6).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount6).Value) + (add_amt6 - TotalAmt6), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt7 > add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) - (TotalAmt7 - add_amt7), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt7 < add_amt7 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount7).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount7).Value) + (add_amt7 - TotalAmt7), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt8 > add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) - (TotalAmt8 - add_amt8), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt8 < add_amt8 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount8).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount8).Value) + (add_amt8 - TotalAmt8), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt9 > add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) - (TotalAmt9 - add_amt9), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt9 < add_amt9 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount9).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount9).Value) + (add_amt9 - TotalAmt9), 3)
            End If
            If gv1.Rows.Count > 0 AndAlso TotalAmt10 > add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) - (TotalAmt10 - add_amt10), 3)
            ElseIf gv1.Rows.Count > 0 AndAlso TotalAmt10 < add_amt10 Then
                gv1.Rows(LastIndex).Cells(colItemACAmount10).Value = System.Math.Round(clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACAmount10).Value) + (add_amt10 - TotalAmt10), 3)
            End If

            If gv1.Columns(colItemTotalAdditionalCharge) IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                gv1.Rows(LastIndex).Cells(colItemTotalAdditionalCharge).Value = clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount1).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount2).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount3).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount4).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount5).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount6).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount7).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount8).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount9).Value) + clsCommon.myCdbl(gv1.Rows(LastIndex).Cells(colItemACCalcAmount10).Value)
            End If
            ''==========================================================================================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

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
        btnShowJE.Visible = False
        BlankAllControls()
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
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        gv1.Rows(0).Cells(colRowType).Value = clsItemRowType.RowTypeItem
        gvAC.Rows.AddNew()
        txtTaxGroup.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        chkRejected.Enabled = True
        chkRejected.Checked = False
        SaleInvoiceDate = Nothing
    End Sub

    Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009580 ========
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If


            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_PR_HEAD where PR_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                    Return False
                End If
            End If

            CalculateInsuranceTotal(False)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    If PurchaseModulePickFixTaxRate AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colPINo).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        SetitemWiseTaxSetting(True, True)
                    End If
                    UpdateCurrentRow(ii)
                End If
            Next
            UpdateAllTotals()
            UpdateTDSAmount()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
                txtVendorNo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
                txtTaxGroup.Focus()
                Return False
            End If
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                txtBillToLocation.Focus()
                Return False
            Else
                Dim LocSegmentCode As String = txtBillToLocation.Value
                Dim locDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + LocSegmentCode + "'")
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtSubLocation.Value) <= 0 And chkJobWorkOutward.Checked = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                        txtSubLocation.Focus()
                        Return False
                    End If
                End If
            End If
            If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select PI No", Me.Text)
                txtReqNo.Focus()
                Return False
            End If

            'If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsLocation.isLocatinExcisable(txtBillToLocation.Value) Then
            '    common.clsCommon.MyMessageBoxShow("Location Can't be excisable of finished goods")
            '    Return False
            'End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "GRN No Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If


            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Item Type")
                cboItemType.Focus()
                Return False
            End If

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PI_Date,103) from tspl_pi_head where PI_No ='" + txtReqNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from Purchase Invoice Date")
            End If


            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strMRP As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value)
                Dim dblAmtAfterDis As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                ''richa agarwal BM00000008001
                Dim strRowType As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)

                If clsCommon.myLen(strRowType) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter Row Type of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    Return False
                End If
                ''-----------------------
                If clsCommon.myLen(strReqNo) > 0 Then
                    If Not (arrReqNo.Contains(strReqNo)) Then
                        arrReqNo.Add(strReqNo)
                    End If
                    If dblQty > dblPendingQty Then
                        common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Can't be more Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If

                If dblAmtAfterDis < 0 Then
                    clsCommon.MyMessageBoxShow(Me, " Amount After discount Cannot be in Negative. ")
                    Return False
                End If

                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
                If clsCommon.myLen(strICode) > 0 Then
                    If ShowCapexCodeandSubCode Then
                        Dim Category As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                        Dim Emergency As String = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
                        Dim CapexCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)
                        Dim CapexSubCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)
                        If clsCommon.CompairString(Category, "") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Fill category at row no. " + clsCommon.myCstr(ii + 1) + "")
                            Return False
                        ElseIf clsCommon.CompairString(Category, "Capex") = CompairStringResult.Equal Then
                            If clsCommon.myLen(CapexSubCode) <= 0 Then
                                clsCommon.MyMessageBoxShow("Fill capex sub code at row no. " + clsCommon.myCstr(ii + 1) + "")
                                Return False
                            End If
                        End If
                    End If
                End If
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        If clsCommon.myLen(strUOM) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If

                        Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                        Dim strLoc As String = txtBillToLocation.Value
                        If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
                            strLoc = txtShipToLocation.Value
                        End If
                        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                            strLoc = txtSubLocation.Value
                        End If
                        If chkRejected.Checked Then
                            strLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Rejected_Location  from tspl_location_master where Location_Code='" + txtBillToLocation.Value + "'"))
                            If clsCommon.myLen(strLoc) <= 0 Then
                                Throw New Exception("Please set rejected location for location " + txtBillToLocation.Value)
                            End If
                        End If
                        ''richa agarwal 06/05/2015 check only in case of Debit and transaction type=return BM00000006474
                        If CboNoteType.SelectedValue = "D" AndAlso cboTrType.SelectedValue = "R" Then
                            ''richa agarwal 06/10/2015 BM00000008025
                            Dim dblBalQty As Double = 0
                            Dim IsMRPWiseBalance As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsMRPWiseBalance, clsFixedParameterCode.IsMRPWiseBalance, Nothing)) > 0, True, False)
                            If IsMRPWiseBalance Then
                                dblBalQty = clsItemLocationDetails.getBalance(strICode, strLoc, txtDocNo.Value, txtDate.Value, Nothing, strUOM, strMRP)
                            Else
                                dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, strLoc, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
                            End If
                            ''---------------------------
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
                                common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                                Return False
                            End If
                        End If
                        ''--------------------------
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
            If ShowItemAllStructureWise = False Then
                clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            End If
            clsPurchaseInvoiceHead.IsValidVendorForPI(arrReqNo, txtVendorNo.Value)
            clsPurchaseInvoiceHead.IsValidJobWorkOutwordForPR(arrReqNo, chkJobWorkOutward.Checked)

            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            If (CboNoteType.SelectedValue = "" And cboTrType.SelectedValue = "") Or (CboNoteType.SelectedValue <> "" And cboTrType.SelectedValue = "") Or (CboNoteType.SelectedValue = "" And cboTrType.SelectedValue <> "") Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Fill Transaction Type and Note Type ??", Me.Text)
                Return False
            End If
            'clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            ''For GST Skip
            Dim isSkipGST As Boolean = False
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select sum(case when isnull( Skip_GST,0)=1 then 1 else 0 end) as NoOfSkipGSTItem,sum(case when isnull( Skip_GST,0)=0 then 1 else 0 end) as NoOfNonSkipGSTItem from tspl_item_master where item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("NoOfSkipGSTItem")) > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("NoOfNonSkipGSTItem")) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "All Item should be of Skip GST or Not", Me.Text)
                        Return False
                    End If
                    isSkipGST = True
                End If
            End If
            dt = Nothing
            If Not isSkipGST Then
                clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
            End If
            ''End of For GST Skip
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", IIf(clsCommon.myLen(txtShipToLocation.Value) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtDate.Value, Nothing)
            SaveData(False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If ChekPostBtn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Return", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''
            If (AllowToSave()) Then
                Dim obj As New clsPurchasReturnHead()
                obj.PR_No = txtDocNo.Value
                obj.PR_Date = txtDate.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sub_Location_code = txtSubLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.is_Reject_Item = chkRejected.Checked
                obj.Against_PI = txtReqNo.Value
                obj.Project_Id = fndProject.Text
                If clsCommon.myLen(obj.Against_PI) > 0 Then
                    obj.Against_SRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + obj.Against_PI + "'"))
                    obj.GSTRegistered = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select GSTRegistered  from TSPL_PI_HEAD where PI_No ='" + obj.Against_PI + "' "))
                End If
                If clsCommon.myLen(obj.Against_SRN) > 0 Then
                    obj.Against_MRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + obj.Against_SRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_MRN) > 0 Then
                    obj.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + obj.Against_MRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_GRN) > 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'"))
                End If
                If clsCommon.myLen(obj.Against_PO) > 0 Then
                    obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "'"))
                End If
                obj.isJobWorkOutward = chkJobWorkOutward.Checked
                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                    obj.AssessableAmt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAssessableAmt).Value)

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
                obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(lblAddChargesForInsurance.Text)
                obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(lblTotalInsuranceAmt.Text)
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
                If objRemittance IsNot Nothing Then
                    obj.objPIRemittance = New clsPIRemittance()
                    obj.objPIRemittance.Vendor_Code = objRemittance.Vendor_Code
                    obj.objPIRemittance.Vendor_Name = objRemittance.Vendor_Name
                    obj.objPIRemittance.Document_No = objRemittance.Document_No
                    obj.objPIRemittance.Document_Date = objRemittance.Document_Date
                    ''richa agarwal
                    If clsCommon.CompairString(CboNoteType.SelectedValue, "D") = CompairStringResult.Equal Then
                        obj.objPIRemittance.Document_Type = "D"
                    Else
                        obj.objPIRemittance.Document_Type = objRemittance.Document_Type
                    End If
                    ''--------
                    obj.objPIRemittance.Document_Amount = objRemittance.Document_Amount
                    obj.objPIRemittance.Service_Type = objRemittance.Service_Type
                    obj.objPIRemittance.Actual_TDS_Base = objRemittance.Actual_TDS_Base
                    obj.objPIRemittance.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
                    obj.objPIRemittance.Actual_TDS = objRemittance.Actual_TDS
                    obj.objPIRemittance.Calculated_TDS = objRemittance.Calculated_TDS
                    obj.objPIRemittance.Actual_Surcharge = objRemittance.Actual_Surcharge
                    obj.objPIRemittance.Calculated_Surcharge = objRemittance.Calculated_Surcharge
                    obj.objPIRemittance.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
                    obj.objPIRemittance.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
                    obj.objPIRemittance.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
                    obj.objPIRemittance.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
                    obj.objPIRemittance.Actual_Total_TDS = objRemittance.Actual_Total_TDS
                    obj.objPIRemittance.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
                    obj.objPIRemittance.Fiscal_Year = objRemittance.Fiscal_Year
                    obj.objPIRemittance.Quarter = objRemittance.Quarter
                    obj.objPIRemittance.Section_Code = objRemittance.Section_Code
                    obj.objPIRemittance.Section_Description = objRemittance.Section_Description
                    obj.objPIRemittance.Branch_Code = objRemittance.Branch_Code
                    obj.objPIRemittance.Deduction_Code = objRemittance.Deduction_Code
                    obj.objPIRemittance.TDS_Per = objRemittance.TDS_Per
                    obj.objPIRemittance.Surcharge_Per = objRemittance.Surcharge_Per
                    obj.objPIRemittance.Edu_Cess_Per = objRemittance.Edu_Cess_Per
                    obj.objPIRemittance.Sec_Educess_Per = objRemittance.Sec_Educess_Per
                    obj.objPIRemittance.Select_By = objRemittance.Select_By
                    obj.objPIRemittance.IsTDSOverride = objRemittance.IsTDSOverride
                    obj.objPIRemittance.IsApplyTDS = objRemittance.IsApplyTDS
                End If

                obj.Terms_Code = txtTermCode.Value
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.RoundOffAmt = clsCommon.myCdbl(lblRoundOff.Text)
                obj.PR_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                obj.Carrier = txtCarrier.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GRNo = txtGRNo.Text
                obj.GENo = txtGENo.Text
                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If


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
                obj.is_Excise_On_Qty = chkExciseOnQty.Checked

                obj.Arr = New List(Of clsPurchasReturnDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsPurchasReturnDetail()

                    'done by stuti n 20/10/2016 against purchase points
                    objTr.Category = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    objTr.Emergency = CInt(clsCommon.myCdbl(grow.Cells(colEmergency).Value))
                    objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)

                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.PR_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    ''objTr.Rejected_Qty = clsCommon.myCdbl(grow.Cells(colRejectedQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.PI_Id = clsCommon.myCstr(grow.Cells(colPINo).Value)

                    objTr.PO_ID = clsCommon.myCstr(grow.Cells(colPO_No).Value)
                    objTr.GRN_ID = clsCommon.myCstr(grow.Cells(colGRN_NO).Value)
                    objTr.MRN_ID = clsCommon.myCstr(grow.Cells(colMRN_NO).Value)
                    objTr.SRN_Id = clsCommon.myCstr(grow.Cells(colSRN_NO).Value)


                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)

                    objTr.Header_Discount_Per = clsCommon.myCdbl(grow.Cells(colHeaderDiscountPer).Value)
                    objTr.Header_Discount_Amount = clsCommon.myCdbl(grow.Cells(colHeaderDiscountAmt).Value)
                    objTr.Detail_Discount_Amount = clsCommon.myCdbl(grow.Cells(colDetailDisAmt).Value)

                    objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)

                    objTr.Disc_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisPerUnit).Value)
                    objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisAmtPerUnit).Value)

                    objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colItemInsuranceBaseAmt).Value)
                    objTr.Item_Insurance_Apply_On = clsCommon.myCstr(grow.Cells(colItemInsuranceApplyOn).Value)
                    objTr.Item_Insurance_Rate = clsCommon.myCdbl(grow.Cells(colItemInsurancePer).Value)
                    objTr.Item_Insurance_Amt = clsCommon.myCdbl(grow.Cells(colItemInsuranceAmt).Value)
                    objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(grow.Cells(colItemAmtAfterInsurance).Value)

                    objTr.Taxable_Amount = clsCommon.myCdbl(grow.Cells(colTaxableAmount).Value)
                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(grow.Cells(colTaxableAmountPer).Value)
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
                    ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                    objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
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


                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                    CalLandAmt()
                    CalNonRectax()
                    CalRectax()
                    CalAddtionalAmt()

                    objTr.Landed_Cost_Rate = clsCommon.myCdbl(grow.Cells(colLandedRate).Value)
                    objTr.Landed_Cost_Amount = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)

                    objTr.Total_AddtionalCost_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotAddCost).Value)
                    objTr.Total_NonRecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotNonRecTax).Value)
                    objTr.Total_RecTax_PerUnit = clsCommon.myCdbl(grow.Cells(colUnitTotRecTax).Value)

                    If clsItemMaster.IsItemTypeEmpty(objTr.Item_Code, objTr.Unit_code, Nothing) Then
                        Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objTr.Unit_code, Nothing))
                        objTr.Empty_Amount = dblVal * objTr.PR_Qty
                        obj.Tot_Empty_Amount += objTr.Empty_Amount
                    End If

                    ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                    objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(grow.Cells(colItemACCode1).Value)
                    objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(grow.Cells(colItemACCode2).Value)
                    objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(grow.Cells(colItemACCode3).Value)
                    objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(grow.Cells(colItemACCode4).Value)
                    objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(grow.Cells(colItemACCode5).Value)
                    objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(grow.Cells(colItemACCode6).Value)
                    objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(grow.Cells(colItemACCode7).Value)
                    objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(grow.Cells(colItemACCode8).Value)
                    objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(grow.Cells(colItemACCode9).Value)
                    objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(grow.Cells(colItemACCode10).Value)
                    objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount1).Value)
                    objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount2).Value)
                    objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount3).Value)
                    objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount4).Value)
                    objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount5).Value)
                    objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount6).Value)
                    objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount7).Value)
                    objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount8).Value)
                    objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount9).Value)
                    objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACCalcAmount10).Value)
                    objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(grow.Cells(colItemACAmount1).Value)
                    objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(grow.Cells(colItemACAmount2).Value)
                    objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(grow.Cells(colItemACAmount3).Value)
                    objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(grow.Cells(colItemACAmount4).Value)
                    objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(grow.Cells(colItemACAmount5).Value)
                    objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(grow.Cells(colItemACAmount6).Value)
                    objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(grow.Cells(colItemACAmount7).Value)
                    objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(grow.Cells(colItemACAmount8).Value)
                    objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(grow.Cells(colItemACAmount9).Value)
                    objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(grow.Cells(colItemACAmount10).Value)
                    objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(grow.Cells(colItemTotalAdditionalCharge).Value)
                    ''=======================================================================================
                    objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(grow.Cells(colAgainstItemWiseTaxCode).Value)
                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colInsuranceBaseAmt).Value)
                    objTr.Insurance_Per = clsCommon.myCdbl(grow.Cells(colInsurancePer).Value)
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
                'If CboNoteType.SelectedValue = "D" Then
                'Else
                '    obj.TrType = ""
                'End If
                obj.NoteType = CboNoteType.SelectedValue
                obj.TrType = cboTrType.SelectedValue
                '' end CurrencyConversion
                obj.Arr_ACInsurance = New List(Of clsPRAdditionChargeInsurance)
                For Each grow As GridViewRowInfo In gvACInsurance.Rows
                    Dim objtr As New clsPRAdditionChargeInsurance()
                    objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                    If clsCommon.myLen(objtr.AC_Code) > 0 Then
                        obj.Arr_ACInsurance.Add(objtr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.PR_No)
                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.PR_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalLandAmt()
        Dim dblLandedCost As Double = 0
        Dim dblLandedRate As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotAmtAfterDiscount As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                    dblAdditionalAmt += GetNonRecoverableTax(ii)
                End If
            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(lblAddCharges.Text)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    If dblQty > 0 Then
                        Dim dblAmtAfterDiscount As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value) + GetNonRecoverableTax(ii)
                        Dim dblAmtAfterDiscountRatio As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

                        dblLandedCost = dblAmtAfterDiscount + IIf(dblTotAmtAfterDiscount > 0, ((dblAdditionalAmt * dblAmtAfterDiscountRatio) / dblTotAmtAfterDiscount), 0)
                        dblLandedRate = IIf(dblQty = 0, 0, dblLandedCost / dblQty)
                        gv1.Rows(ii).Cells(colLandedAmt).Value = Math.Round(dblLandedCost, 2)
                        gv1.Rows(ii).Cells(colLandedRate).Value = Math.Round(dblLandedRate, 4)
                    End If
                End If
            End If
        Next
        Calc_AddtionalCharge_Itemwise(Nothing)

    End Sub



    'Sub CalLandAmt()
    '    Dim dblLandedCost As Double = 0
    '    Dim dblAdditionalAmt As Double = 0
    '    Dim dblTotAmtAfterDiscount As Double = 0

    '    For ii As Integer = 0 To gv1.Rows.Count - 1
    '        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
    '            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
    '                dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
    '            End If


    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
    '            End If
    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
    '            End If
    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
    '            End If
    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
    '            End If
    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
    '            End If
    '            If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) Then
    '                dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
    '            End If

    '        End If
    '    Next
    '    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(lblAddCharges.Text)

    '    For ii As Integer = 0 To gv1.Rows.Count - 1
    '        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
    '            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
    '                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
    '                If dblQty > 0 Then
    '                    Dim dblAmtAfterDiscount As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmtAfterInsurance).Value)
    '                    dblLandedCost = dblAmtAfterDiscount + IIf(dblTotAmtAfterDiscount > 0, ((dblAdditionalAmt * dblAmtAfterDiscount) / dblTotAmtAfterDiscount), 0)

    '                    gv1.Rows(ii).Cells(colLandedAmt).Value = Math.Round(dblLandedCost, 2)
    '                    gv1.Rows(ii).Cells(colLandedRate).Value = Math.Round(dblLandedCost / dblQty, 4)
    '                End If
    '            End If
    '        End If
    '    Next

    '    Calc_AddtionalCharge_Itemwise(Nothing)
    'End Sub

    Function GetNonRecoverableTax(ByVal rowNo As Integer) As Double
        Dim dblAdditionalAmt As Double = 0
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable1).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt1).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable2).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt2).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable3).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt3).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable4).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt4).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable5).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt5).Value)
        End If
        If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable6).Value) Then
            dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt6).Value)
        End If
        'If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable7).Value) Then
        '    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt7).Value)
        'End If
        'If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable8).Value) Then
        '    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt8).Value)
        'End If
        'If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable9).Value) Then
        '    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt9).Value)
        'End If
        'If Not clsCommon.myCBool(gv1.Rows(rowNo).Cells(colTaxRecoverable10).Value) Then
        '    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(rowNo).Cells(colTaxAmt10).Value)
        'End If
        Return dblAdditionalAmt
    End Function

    Sub CalNonRectax()
        Dim dblAdditionalAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblAdditionalAmt = 0
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt1).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt2).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt3).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt4).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt5).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) = False Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt6).Value)
            End If
            If dblAdditionalAmt > 0 Then
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                If dblTotQty = 0 Then
                    dblTotQty = 1
                End If
                gv1.Rows(ii).Cells(colUnitTotNonRecTax).Value = dblAdditionalAmt / dblTotQty
            Else
                gv1.Rows(ii).Cells(colUnitTotNonRecTax).Value = 0
            End If

        Next
    End Sub

    Sub CalRectax()
        Dim dblAdditionalAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblAdditionalAmt = 0
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt1).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt2).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt3).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt4).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt5).Value)
            End If
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) = True Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt6).Value)
            End If
            If dblAdditionalAmt > 0 Then
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                If dblTotQty = 0 Then
                    dblTotQty = 1
                End If
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = dblAdditionalAmt / dblTotQty
            Else
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = 0
            End If
        Next
    End Sub

    Sub CalAddtionalAmt()
        Dim dblLandedCost As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotItemCost As String = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                dblTotItemCost = dblTotItemCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If

            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeMisc Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + CDec(lblAddCharges.Text)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = clsItemRowType.RowTypeItem Then
                dblLandedCost = IIf(dblTotItemCost = 0, 0, gv1.Rows(ii).Cells(colAmt).Value / dblTotItemCost * dblAdditionalAmt)
                If dblAdditionalAmt = 0 Then
                    gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                Else
                    If dblLandedCost <> 0 Then
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = Math.Round(dblLandedCost / (CDec(gv1.Rows(ii).Cells(colQty).Value)), 6)
                    Else
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                    End If
                End If
            End If
        Next
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True

            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            cboItemType.Enabled = False
            txtBillToLocation.Enabled = False
            Dim obj As New clsPurchasReturnHead()
            obj = clsPurchasReturnHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PR_No) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    If ShowItemAllStructureWise = False Then
                        cboItemType.SelectedValue = obj.Item_Type
                    Else
                        LoadItemType()
                    End If
                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    btncancel.Enabled = True
                Else
                    btncancel.Enabled = False
                End If

                chkJobWorkOutward.Checked = obj.isJobWorkOutward
                txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.PR_No
                txtDate.Value = obj.PR_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                lblRoundOff.Text = clsCommon.myFormat(obj.RoundOffAmt)
                lblDocAmount.Text = lblTotRAmt.Text = clsCommon.myFormat(obj.PR_Total_Amt)
                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                txtBillToLocation.Value = obj.Bill_To_Location
                txtRemarks.Text = obj.Remarks
                txtSubLocation.Value = obj.Sub_Location_code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocation.Text = ""
                End If
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                txtDueDate.Value = obj.Due_Date
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.PR_Total_Amt)
                lblDocAmount.Text = lblTotRAmt.Text
                lblTaxableAmount.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
                lblBillToLocation.Text = obj.BillToLocationName
                lblShipToLocation.Text = clsLocation.GetName(obj.Ship_To_Location, Nothing)
                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                txtReqNo.Value = obj.Against_PI
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    SaleInvoiceDate = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select PI_Date from TSPL_PI_HEAD where PI_NO='" + txtReqNo.Value + "'"))
                    txtTaxGroup.Enabled = False
                End If
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                fndProject.Text = obj.Project_Id
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")

                txtCarrier.Text = obj.Carrier
                txtVehicleNo.Text = obj.VehicleNo
                txtGRNo.Text = obj.GRNo
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If
                cboItemType.SelectedValue = obj.Item_Type
                cboItemType.Enabled = False

                objRemittance = Nothing
                If obj.objPIRemittance IsNot Nothing Then
                    'If objRemittance IsNot Nothing Then
                    objRemittance = New clsRemittance()
                    objRemittance.Vendor_Code = obj.objPIRemittance.Vendor_Code
                    objRemittance.Vendor_Name = obj.objPIRemittance.Vendor_Name
                    objRemittance.Document_No = obj.objPIRemittance.Document_No
                    objRemittance.Document_Date = obj.objPIRemittance.Document_Date
                    objRemittance.Document_Type = obj.objPIRemittance.Document_Type
                    objRemittance.Document_Amount = obj.objPIRemittance.Document_Amount
                    objRemittance.Service_Type = obj.objPIRemittance.Service_Type
                    objRemittance.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                    objRemittance.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                    objRemittance.Actual_TDS = obj.objPIRemittance.Actual_TDS
                    objRemittance.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                    objRemittance.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                    objRemittance.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                    objRemittance.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                    objRemittance.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                    objRemittance.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                    objRemittance.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                    objRemittance.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                    objRemittance.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                    objRemittance.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                    objRemittance.Quarter = obj.objPIRemittance.Quarter
                    objRemittance.Section_Code = obj.objPIRemittance.Section_Code
                    objRemittance.Section_Description = obj.objPIRemittance.Section_Description
                    objRemittance.Branch_Code = obj.objPIRemittance.Branch_Code
                    objRemittance.Deduction_Code = obj.objPIRemittance.Deduction_Code
                    objRemittance.TDS_Per = obj.objPIRemittance.TDS_Per
                    objRemittance.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                    objRemittance.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                    objRemittance.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                    objRemittance.Select_By = obj.objPIRemittance.Select_By
                    objRemittance.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                    objRemittance.IsApplyTDS = obj.objPIRemittance.IsApplyTDS
                    'End If
                End If
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.AssessableAmt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX2_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX3_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX4_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX5_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX6_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX7_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX8_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX9_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX10_Base_Amt
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

                gvAC.Rows.Clear()

                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = obj.Add_Charge_Amt1
                Else
                    gvAC.Rows.AddNew()
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



                chkRejected.Checked = obj.is_Reject_Item
                chkRejected.Enabled = False

                lblAddChargesForInsurance.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblAddChargesForInsurance1.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblTotalInsuranceAmt.Text = clsCommon.myFormat(obj.Total_Item_Insurance_Amt)

                If obj.Arr_ACInsurance IsNot Nothing AndAlso obj.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsPRAdditionChargeInsurance In obj.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If

                If obj.Status = ERPTransactionStatus.Pending Then
                    gvAC.Rows.AddNew()
                End If
                chkExciseOnQty.Checked = obj.is_Excise_On_Qty
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsPurchasReturnDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objTr.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode

                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = objTr.OrgPIQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.PR_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = objTr.PI_Id

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_No).Value = objTr.PO_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRN_NO).Value = objTr.GRN_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = objTr.MRN_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_NO).Value = objTr.SRN_Id

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountAmt).Value = objTr.Header_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = objTr.Disc_Per_Unit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmtPerUnit).Value = objTr.Disc_Amt_Per_Unit

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceBaseAmt).Value = objTr.Item_Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = objTr.Item_Insurance_Apply_On
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = objTr.Item_Insurance_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = objTr.Item_Insurance_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmtAfterInsurance).Value = objTr.Item_Amt_After_Insurance

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmount).Value = objTr.Taxable_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = objTr.Taxable_Amount_Per
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedRate).Value = objTr.Landed_Cost_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Cost_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotNonRecTax).Value = objTr.Total_NonRecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotRecTax).Value = objTr.Total_RecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotAddCost).Value = objTr.Total_AddtionalCost_PerUnit

                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If

                        If clsCommon.myLen(objTr.PI_Id) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPurchaseInvoiceDetail.GetBalancePIQty(objTr.PI_Id, objTr.Item_Code, obj.PR_No, objTr.Unit_code, objTr.MRP, objTr.Assessable, chkRejected.Checked)
                        End If

                        ''-----------------19/10/2016---------additional charge itemwise------------------------------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode1).Value = objTr.ItemAdd_Charge_Code1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode2).Value = objTr.ItemAdd_Charge_Code2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode3).Value = objTr.ItemAdd_Charge_Code3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode4).Value = objTr.ItemAdd_Charge_Code4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode5).Value = objTr.ItemAdd_Charge_Code5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode6).Value = objTr.ItemAdd_Charge_Code6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode7).Value = objTr.ItemAdd_Charge_Code7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode8).Value = objTr.ItemAdd_Charge_Code8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode9).Value = objTr.ItemAdd_Charge_Code9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCode10).Value = objTr.ItemAdd_Charge_Code10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount1).Value = objTr.ItemAdd_Calc_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount2).Value = objTr.ItemAdd_Calc_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount3).Value = objTr.ItemAdd_Calc_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount4).Value = objTr.ItemAdd_Calc_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount5).Value = objTr.ItemAdd_Calc_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount6).Value = objTr.ItemAdd_Calc_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount7).Value = objTr.ItemAdd_Calc_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount8).Value = objTr.ItemAdd_Calc_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount9).Value = objTr.ItemAdd_Calc_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACCalcAmount10).Value = objTr.ItemAdd_Calc_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount1).Value = objTr.ItemAdd_Org_Charge_Amt1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount2).Value = objTr.ItemAdd_Org_Charge_Amt2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount3).Value = objTr.ItemAdd_Org_Charge_Amt3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount4).Value = objTr.ItemAdd_Org_Charge_Amt4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount5).Value = objTr.ItemAdd_Org_Charge_Amt5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount6).Value = objTr.ItemAdd_Org_Charge_Amt6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount7).Value = objTr.ItemAdd_Org_Charge_Amt7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount8).Value = objTr.ItemAdd_Org_Charge_Amt8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount9).Value = objTr.ItemAdd_Org_Charge_Amt9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemACAmount10).Value = objTr.ItemAdd_Org_Charge_Amt10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTotalAdditionalCharge).Value = objTr.Total_ItemAdd_Charge
                        ''=======================================================================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = objTr.Against_Item_Wise_Tax_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsuranceBaseAmt).Value = objTr.Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsurancePer).Value = objTr.Insurance_Per

                    Next
                End If

                SetitemWiseTaxOnlySetting()
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PR_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.PR_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    Me.txtCurrencyCode.Enabled = False
                    MakeColumnReadOnly(True)
                Else
                    Me.txtCurrencyCode.Enabled = True
                    MakeColumnReadOnly(False)
                End If
                Me.CboNoteType.SelectedValue = obj.NoteType
                Me.cboTrType.SelectedValue = obj.TrType
                If clsCommon.CompairString(clsCommon.myCstr(cboTrType.SelectedValue), "R") = CompairStringResult.Equal Then
                    Me.CboNoteType.SelectedValue = "D"
                    Me.CboNoteType.Enabled = False
                Else
                    Me.CboNoteType.Enabled = True
                End If
                '' end  MULTICURRENCY
                UcAttachment1.LoadData(obj.PR_No)
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnShowJE.Visible = True
                Else
                    btnShowJE.Visible = False
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

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
            gvrow.Cells(colEmergency).ReadOnly = Read
        Next

    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If clsCommon.myLen(txtReqNo.Value) = 0 OrElse (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Is_TCS from tspl_tax_master where Tax_Code='" & clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) & "'")), "Y") = CompairStringResult.Equal) Then
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FdVendFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtBillToLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendorNo.Value, "P")
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", IIf(clsCommon.myLen(txtShipToLocation.Value) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtDate.Value, Nothing)
            PostData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                '' Anubhooti 13-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Return", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''
                If (clsPurchasReturnHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
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

                If clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                    SMSSENDONLY(True)
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
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", IIf(clsCommon.myLen(txtShipToLocation.Value) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtDate.Value, Nothing)
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
                If (clsPurchasReturnHead.DeleteData(txtDocNo.Value)) Then
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
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_PR_HEAD where PR_No='" + txtDocNo.Value + "'"
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
        '===================update by preeti gupta [Add column created by for Jakson Clinet]
        Dim qry As String = "select PR_No as Code,convert(varchar,PR_Date,103) as Date,TSPL_PR_HEAD.Vendor_Code as [Vendor Code], TSPL_PR_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_invoice_No as [Vendor Invoice Number],Against_SRN as [SRN Num],PR_Total_Amt as Amount,case when TSPL_PR_HEAD.Status='0' then 'Pending' else 'Approved' end as Status ,tspl_user_master.User_Name as [User Name] from TSPL_PR_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PR_HEAD.Vendor_Code "
        qry += " left join tspl_user_master on TSPL_USER_MASTER .User_Code =TSPL_PR_HEAD.Created_By"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("PRCodeFNDID", qry, "Code", whrClas, txtDocNo.Value, "TSPL_PR_HEAD.PR_Date desc", isButtonClicked, "TSPL_PR_HEAD.PR_Date"), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ''If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) <= 0 Then
        ''    isCellValueChangedOpen = True
        ''    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
        ''        gv1.CurrentColumn = gv1.Columns(colIName)
        ''        OpenICodeList(True)
        ''        gv1.CurrentColumn = gv1.Columns(colICode)
        ''    End If
        ''    setGridFocus()
        ''    isCellValueChangedOpen = False
        ''Else
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.X Then
            'AmbujPrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
            'ElseIf e.Control AndAlso e.KeyCode = Keys.F7 Then
            '    SelectPIItems()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                "TSPL_PR_HEAD " + Environment.NewLine +
                                                "TSPL_PR_DETAIL " + Environment.NewLine +
                                                "TSPL_PI_REMITTANCE " + Environment.NewLine +
                                                "Press Alt+P for Post Trasnaction  " + Environment.NewLine +
                                                "TSPL_PI_DETAIL(Update Balance Qty) " + Environment.NewLine +
                                                "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                                "TSPL_REMITTANCE " + Environment.NewLine +
                                                "TSPL_VENDOR_INVOICE_HEAD " + Environment.NewLine +
                                                "TSPL_VENDOR_INVOICE_DETAIL " + Environment.NewLine +
                                                "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                                "TSPL_PI_HEAD " + Environment.NewLine +
                                                "TSPL_MILK_PURCHASE_INVOICE_HEAD(Against Milk purchase invoice) " + Environment.NewLine +
                                                "TSPL_Bulk_MILK_PURCHASE_INVOICE_HEAD(Against Bulk Milk purchase invoice) ")
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

    Private Sub AmbujPrintData()

        Try
            Dim qry As String = "select a.*, b.* from gl_deviation_head a, gl_deviation_detail b where a.DEVIATION_CODE='DVN00003'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptDeviation", "Deviation Report")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("FDTFNDe", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        'Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("TaFNDr", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)

        SetTaxDetails()

    End Sub

    Private Sub SetTax()
        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value)
        lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtBillToLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        If rbtnTaxCalAutomatic.IsChecked Then
                            If isChangeRate Then
                                If clsCommon.myCBool(gv1.CurrentRow.Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
                                    Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr(colICode)).Value), txtTaxGroup.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "P")
                                    If objTAXRate IsNot Nothing Then
                                        gv1.CurrentRow.Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTAXRate.TAX_Rate
                                    End If
                                Else
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            End If
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTCSTax" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("IS_TCS")), "Y") = CompairStringResult.Equal)
                        End If
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
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            If rbtnTaxCalAutomatic.IsChecked Then
                                If isChangeRate Then
                                    If clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colItemTaxable)).Value) AndAlso PurchaseModulePickFixTaxRate Then
                                        Dim objTAXRate As clsItemWiseTaxAuthority = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colICode)).Value), txtTaxGroup.Value, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "P")
                                        If objTAXRate IsNot Nothing Then
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colAgainstItemWiseTaxCode)).Value = objTAXRate.HCODE
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTAXRate.TAX_Rate
                                        End If
                                    Else
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                    End If
                                End If
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTCSTax" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("IS_TCS")), "Y") = CompairStringResult.Equal)
                            End If
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
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
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTCSTax" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("IS_TCS")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
        txtVendorNo.Value = clsCommon.ShowSelectForm("VendorNoFNDD", qry, "Code", " TSPL_VENDOR_MASTER.Status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""

            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If
        SetTax()

        SetVendorTDSDetails()
        btnHistory.Enabled = True
    End Sub

    Sub SetVendorTDSDetails()
        btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(txtVendorNo.Value)
        If objVendor IsNot Nothing Then
            btnViewTDSDetails.Enabled = True
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, clsCommon.myCdbl(lblTotRAmt.Text), Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                objRemittance = New clsRemittance()
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                objRemittance.IsTDSOverride = False
                'objRemittance.IsApplyTDS = True
                If isNewEntry Then
                    objRemittance.IsApplyTDS = True
                Else
                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                End If
                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"
            End If
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

        txtBillToLocation.Value = clsCommon.ShowSelectForm("BillToLocFNDD", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))


        SetTax()
        If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                txtSubLocation.Enabled = True
            Else
                txtSubLocation.Enabled = False
            End If
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
        End If

    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        'Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description from TSPL_SHIP_TO_LOCATION"
        'txtShipToLocation.Value = clsCommon.ShowSelectForm("ShipToLocIDFND", qry, "Code", "", txtShipToLocation.Value, "Code", isButtonClicked)
        'lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtShipToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))

    End Sub

    Sub SelectPIItems()
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            If clsCommon.myLen(clsCommon.myCstr(txtSubLocation.Value)) <= 0 AndAlso chkJobWorkOutward.Checked = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                Exit Sub
            End If
        End If
        isInsideLoadData = True
        Dim frm As New frmPendingPI()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.isRejectedItem = chkRejected.Checked
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
            If clsCommon.myLen(clsCommon.myCstr(txtSubLocation.Value)) >= 0 AndAlso chkJobWorkOutward.Checked = False Then
                frm.SubLocationCode = txtSubLocation.Value
            End If
        End If
        frm.ShowDialog()

        Dim objPurInvoice As clsPurchaseInvoiceHead = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).PI_No) > 0 Then
                chkRejected.Enabled = False
                objPurInvoice = clsPurchaseInvoiceHead.GetData(frm.ArrReturn(0).PI_No, NavigatorType.Current, "")
                If objPurInvoice IsNot Nothing AndAlso clsCommon.myLen(objPurInvoice.PI_No) > 0 Then
                    SaleInvoiceDate = objPurInvoice.PI_Date
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtCarrier.Text = objPurInvoice.Carrier
                    End If
                    If clsCommon.myLen(txtVehicleNo.Text) <= 0 Then
                        txtVehicleNo.Text = objPurInvoice.VehicleNo
                    End If
                    If clsCommon.myLen(txtGRNo.Text) <= 0 Then
                        txtGRNo.Text = objPurInvoice.GRNo
                    End If
                    If clsCommon.myLen(txtGENo.Text) <= 0 Then
                        txtGENo.Text = objPurInvoice.GENo
                    End If
                    If txtGEDate.Checked = False AndAlso objPurInvoice.GEDate.HasValue Then
                        txtGEDate.Checked = True
                        txtGEDate.Value = clsCommon.GetPrintDate(objPurInvoice.GEDate.Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(txtCarrier.Text) <= 0 Then
                        txtVehicleNo.Text = objPurInvoice.VehicleNo
                    End If
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objPurInvoice.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objPurInvoice.Description
                    End If
                    If clsCommon.myLen(txtRemarks.Text) <= 0 Then
                        txtRemarks.Text = objPurInvoice.Remarks
                    End If

                    '' multicurrency
                    Me.txtCurrencyCode.Value = objPurInvoice.CURRENCY_CODE
                    txtConversionRate.Text = objPurInvoice.ConvRate
                    Me.txtCurrencyCode.Enabled = False
                    If objPurInvoice.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objPurInvoice.ApplicableFrom
                    End If
                    chkJobWorkOutward.Checked = objPurInvoice.isJobWorkOutward
                    txtBillToLocation.Value = objPurInvoice.Bill_To_Location
                    lblBillToLocation.Text = objPurInvoice.BillToLocationName
                    txtSubLocation.Value = objPurInvoice.Sublocation_Code
                    lblSubLocation.Text = objPurInvoice.Sublocation_Code
                    txtShipToLocation.Value = objPurInvoice.Ship_To_Location
                    lblShipToLocation.Text = clsLocation.GetName(objPurInvoice.Ship_To_Location, Nothing)

                    If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                        txtVendorNo.Value = frm.VendorCode
                        lblVendorName.Text = frm.VendorName
                    End If
                    If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                        cboItemType.SelectedValue = objPurInvoice.Item_Type
                    End If

                    If clsCommon.myLen(objPurInvoice.Vendor_Invoice_No) > 0 Then
                        txtVendorInvoiceNo.Text = objPurInvoice.Vendor_Invoice_No
                        txtVendorInvoiceNo.ReadOnly = True
                    Else
                        txtVendorInvoiceNo.Text = ""
                        txtVendorInvoiceNo.ReadOnly = False
                    End If

                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objPurInvoice.Terms_Code
                        lblTermName.Text = objPurInvoice.TermsName
                        txtDueDate.Value = objPurInvoice.Due_Date
                    End If
                    If (clsCommon.myLen(fndProject.Text) <= 0) Then
                        fndProject.Text = objPurInvoice.PROJECT_ID
                        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Text + "'")
                    End If
                    If objPurInvoice.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                        rbtnTaxCalAutomatic.IsChecked = True
                        chkExciseOnQty.Checked = objPurInvoice.is_Excise_On_Qty
                    ElseIf objPurInvoice.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                        rbtnTaxCalManual.IsChecked = True
                    End If
                    txtTaxGroup.Enabled = False

                    LoadBlankGridACInsurance()
                    If objPurInvoice.Arr_ACInsurance IsNot Nothing AndAlso objPurInvoice.Arr_ACInsurance.Count > 0 Then
                        For Each objtr As clsPIAdditionChargeInsurance In objPurInvoice.Arr_ACInsurance
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                            gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                            gvACInsurance.Rows.AddNew()
                        Next
                    End If

                    gvAC.Rows.Clear()
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code1) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name1
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt1
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code2) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name2
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt2
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code3) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name3
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt3
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code4) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name4
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt4
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code5) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name5
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt5
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code6) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name6
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt6
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code7) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name7
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt7
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code8) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name8
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt8
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code9) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name9
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt9
                    End If
                    If (clsCommon.myLen(objPurInvoice.Add_Charge_Code10) > 0) Then
                        gvAC.Rows.AddNew()
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPurInvoice.Add_Charge_Code10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPurInvoice.Add_Charge_Name10
                        gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPurInvoice.Add_Charge_Amt10
                    End If
                End If

            End If
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            For Each obj As clsPurchaseInvoiceDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = obj.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(obj.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = obj.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = obj.Capex_SubCode
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_No).Value = obj.PO_ID
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGRN_NO).Value = obj.GRN_ID
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRN_NO).Value = obj.MRN_ID
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_NO).Value = obj.SRN_Id

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = obj.PI_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = obj.PI_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = obj.Header_Discount_Per
                    If rbtnTaxCalManual.IsChecked Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = obj.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = obj.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = obj.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = obj.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = obj.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = obj.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = obj.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = obj.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = obj.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = obj.TAX10_Amt
                    End If
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = frmPendingPI.Load_discount_per_unit_for_PR(obj.PI_No, obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPI.Load_discount_for_PR(obj.PI_No, obj.Item_Code)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = obj.Assessable
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
                    If obj.MFG_Date.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = obj.MFG_Date
                    End If
                    If obj.Expiry_Date.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = obj.Expiry_Date
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = obj.Against_Item_Wise_Tax_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = obj.Item_Insurance_Apply_On
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = obj.Item_Insurance_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = obj.Item_Insurance_Amt
                End If
            Next

            If objPurInvoice IsNot Nothing Then
                For Each obj As clsPurchaseInvoiceDetail In objPurInvoice.Arr
                    ''If IsValidItem(obj) Then
                    If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = obj.PI_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = False
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Amount
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = obj.Header_Discount_Per
                        If ShowCapexCodeandSubCode Then
                            MakeColumnReadOnly(True)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = obj.Category
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = obj.Emergency
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = obj.Capex_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = obj.Capex_SubCode
                        End If
                        'If rbtnTaxCalManual.IsChecked Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = obj.TAX1_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = obj.TAX2_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = obj.TAX3_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = obj.TAX4_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = obj.TAX5_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = obj.TAX6_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = obj.TAX7_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = obj.TAX8_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = obj.TAX9_Amt
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = obj.TAX10_Amt
                        'End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
                    End If
                Next
            End If

            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 1 To 10
                    If gv2.Rows.Count >= ii Then
                        Dim dblTotTaxAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotTaxAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells("COLTAXAMT" + clsCommon.myCstr(ii)).Value)
                        Next
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = dblTotTaxAmt
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(gv1.Rows(0).Cells("COLTAXRATE" + clsCommon.myCstr(ii)).Value)
                    End If
                Next
            End If

            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        CalculateInsuranceTotal(True)
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()
        SetVendorTDSDetails()
    End Sub

    Function IsValidItem(ByVal obj As clsPurchaseInvoiceDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.PITax_Group
            SetTaxDetails()
        End If
        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.PITax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " SRN No: " + obj.PI_No + "  contain Tax Group :" + obj.PITax_Group + Environment.NewLine)
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)

            Dim strPOCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPO_No).Value)
            Dim strGRNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colGRN_NO).Value)
            Dim strMRNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRN_NO).Value)
            Dim strSRNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRN_NO).Value)


            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)

            ''Dim dblAssessable As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strPOCode, obj.PO_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strGRNCode, obj.GRN_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMRNCode, obj.MRN_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strSRNCode, obj.SRN_Id) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.PI_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Purchase Invoice No : " + obj.PI_No + "  Item : " + obj.Item_Desc + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                ''If dblAssessable > 0 Then
                ''    strMsg = strMsg + Environment.NewLine + "Assessable : " + clsCommon.myCstr(dblAssessable)
                ''End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                If clsCommon.myLen(txtReqNo.Value) = 0 Then
                    Dim frm As New FrmPOItemTaxDetails()
                    frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                    ''New Column for location wise
                    frm.strTaxGroup = txtTaxGroup.Value
                    frm.strTransLocation = txtBillToLocation.Value
                    frm.strTaxType = "P"
                    frm.strVendorCustomerCode = txtVendorNo.Value
                    ''End of New Column for location wise
                    frm.PurchaseModulePickFixTaxRate = PurchaseModulePickFixTaxRate
                    frm.IsTaxableItem = clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value)
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
                                obj.TaxOnBaseAmount = clsCommon.myCBool(gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value)
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
                                gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value = frm.ArrOut(ii).TaxOnBaseAmount
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
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If clsPurchasReturnDetail.CompletePI(txtDocNo.Value, strICode, intSNo) Then
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
                        ''gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colRowType) Then
                    If clsCommon.myLen(txtReqNo.Value) = 0 Then
                        gv1.CurrentRow.Cells(colRowType).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colRowType).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) Then
                    Dim isbatchNoEdiatable As Boolean = IIf(clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) > 0, True, False)
                    gv1.CurrentRow.Cells(colMRP).ReadOnly = isbatchNoEdiatable
                    gv1.CurrentRow.Cells(colBatchNo).ReadOnly = isbatchNoEdiatable
                    gv1.CurrentRow.Cells(colExpiry).ReadOnly = isbatchNoEdiatable
                    gv1.CurrentRow.Cells(colManufactureDate).ReadOnly = isbatchNoEdiatable
                ElseIf Return_Without_Invoice Then
                    If txtReqNo.Value = "" Then
                        gv1.Columns(colQty).ReadOnly = False
                    Else
                        gv1.Columns(colQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) <= 0 Then
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                        End If
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colInsurancePer) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPINo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    End If
                End If
                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub btnViewTDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub

    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            objRemittance = Nothing
            If (frm.ObjReturn IsNot Nothing) Then
                If frm.ObjReturn.IsApplyTDS Then
                    objRemittance = frm.ObjReturn
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        Else
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(lblTotRAmt.Text), Nothing, False, txtVendorNo.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If
        End If
        If (objRemittance IsNot Nothing) Then

            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + txtVendorNo.Value + "'")
            Dim applicableAmt As Double = 0
            If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
                applicableAmt = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            Else
                applicableAmt = clsCommon.myCdbl(lblTotRAmt.Text)
            End If


            objRemittance.Vendor_Code = txtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            '' objRemittance.Document_No = txtDocNo.SelectedValue   Should pass when saveing the data
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = "I" 'Purchase Invoice Type
            objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
            'objRemittance.Service_Type = txtDate.Value

            objRemittance.Calculated_TDS_Base = applicableAmt
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = applicableAmt
            End If
            If objRemittance.IsApplyTDS = True Then
                objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
                objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

                objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
                objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

                objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

                objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

                objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
            End If

        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
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

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        SelectPIItems()
    End Sub

    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
            If clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colPINo).Value) <= 0 Then
                dblAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsuranceBaseAmt).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsurancePer).Value)) / 100
                gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            Else
                dblAmt = gv1.Rows(IntRowNo).Cells(colAmt).Value
            End If
        End If
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
       

        Dim dblHeaderDisAmt As Decimal = Math.Round(dblAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaderDiscountPer).Value) / 100, 2, MidpointRounding.AwayFromZero)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dbldisperunit As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPerUnit).Value)
        Dim dbldisamtperunit As Decimal = (dblQty * dbldisperunit)
        Dim dblDetailDisAmt As Decimal = (dblAmt * dblDisPer) / 100
        Dim dblDisAmt As Double = dblHeaderDisAmt + dblDetailDisAmt + dbldisamtperunit

        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt

        Dim dblTotAmt As Decimal = 0
        For jj As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                If jj = IntRowNo Then
                    dblTotAmt += dblAmt
                Else
                    dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmt).Value)
                End If
            End If
        Next
        Dim dclItemInsuranceAdditionalChargePart As Decimal = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            dclItemInsuranceAdditionalChargePart = Math.Round(clsCommon.myCDivide((clsCommon.myCdbl(lblAddChargesForInsurance.Text)) * dblAmt, dblTotAmt), 2, MidpointRounding.AwayFromZero)
        Else
            gv1.Rows(IntRowNo).Cells(colItemInsurancePer).Value = 0
            gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = 0
        End If
        Dim dclItemInsuranceBaseAmt As Decimal = dblAmtAfterDis + dclItemInsuranceAdditionalChargePart
        Dim dclItemInsuranceAmt As Decimal = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemInsuranceApplyOn).Value), clsCalculationlApplyON.RowTypeApplyOnPercent) = CompairStringResult.Equal Then
            dclItemInsuranceAmt = dclItemInsuranceBaseAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemInsurancePer).Value) / 100
        Else
            dclItemInsuranceAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value)
        End If
        Dim dclItemAmtAfterInsurance As Decimal = dblAmtAfterDis + dclItemInsuranceAmt + dclItemInsuranceAdditionalChargePart


        Dim dblCurrentTaxablePer As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTaxableAmountPer).Value)
        Dim dblCurrentTaxableAmount As Decimal = dclItemAmtAfterInsurance * dblCurrentTaxablePer / 100
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If (clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTCSTax" + Strii)).Value)) AndAlso Not (SaleInvoiceDate.Month() = txtDate.Value.Month() And SaleInvoiceDate.Year() = txtDate.Value.Year()) Then
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = 0 ''Because Tax Rate will be zero for TCS Type Tax.
            End If
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                    Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsTaxOnBaseAmt Then
                        dblBaseAmt = dblCurrentTaxableAmount
                    ElseIf IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        dblBaseAmt = (dblCurrentTaxableAmount + dblOtherTaxAmt)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    If ii = 1 Then
                        If chkExciseOnQty.Checked Then
                            gv1.Rows(IntRowNo).Cells(colAssessableAmount).Value = Math.Round(dblQty, 2)
                            dblTaxAmt = (dblQty * dblTaxRate) / 100
                        Else
                            gv1.Rows(IntRowNo).Cells(colAssessableAmount).Value = Math.Round(dblBaseAmt, 2)
                        End If
                    End If
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
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTCSTax" + Strii)).Value = Nothing
                End If
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colAmtAfterDis).Value)
                    dblTotAmt = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmtAfterDis).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = dblCurrRowAmt
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If
            End If
        Next

        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dclItemInsuranceAdditionalChargePart + dclItemInsuranceAmt + dblTotTaxAmt
        gv1.Rows(IntRowNo).Cells(colHeaderDiscountAmt).Value = Math.Round(dblHeaderDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colDetailDisAmt).Value = Math.Round(dblDetailDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
        gv1.Rows(IntRowNo).Cells(colDisAmtPerUnit).Value = Math.Round(dbldisamtperunit, 2)

        gv1.Rows(IntRowNo).Cells(colItemInsuranceBaseAmt).Value = Math.Round(dclItemInsuranceBaseAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemInsuranceAmt).Value = Math.Round(dclItemInsuranceAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemAmtAfterInsurance).Value = Math.Round(dclItemAmtAfterInsurance, 2)

        gv1.Rows(IntRowNo).Cells(colTaxableAmount).Value = Math.Round(dblCurrentTaxableAmount, 2)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            If txtReqNo.Value = "" Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document number not found", Me.Text)
        Else
            printReport1(txtDocNo.Value.Trim())
        End If
    End Sub
    ' Ticket No : KDI/10/09/19-000457 By Prabhakar
    Public Sub printReport1(ByVal StrDocNo As String)
        Try
            Dim qry As String


            If txtDocNo.Value = "" AndAlso clsCommon.myLen(StrDocNo) <= 0 Then
                MessageBox.Show("Please Select Document No.")
                Exit Sub
            End If

            qry = "select TSPL_PR_HEAD.Note_type ,TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, case when isnull(TSPL_SHIP_TO_LOCATION1.Add2, '') <> '' then TSPL_SHIP_TO_LOCATION1.Add2 ELSE  '' END   + case when isnull(TSPL_SHIP_TO_LOCATION1.City_Code , '')<>'' then  ', '+TSPL_SHIP_TO_LOCATION1.City_Code ELSE '' END + case when isnull(TSPL_SHIP_TO_LOCATION1.State , '')<>'' then  ', '+ (sELECT STATE_NAME  FROM TSPL_STATE_MASTER WHERE State_Code =TSPL_SHIP_TO_LOCATION1.State)  ELSE '' END   AS BILL_To_Location, TSPL_PR_HEAD.PR_No,TSPL_PR_HEAD.Description,	convert(varchar(15),TSPL_PR_HEAD.PR_Date,106)PR_Date, "
            If clsCommon.CompairString(cboTrType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.CompairString(CboNoteType.SelectedValue, "D") = CompairStringResult.Equal Then
                qry += "substring(TSPL_PR_HEAD.Description,27,len(TSPL_PR_HEAD.Description)) as 'PR_Head_Remarks',"
            Else
                qry += " TSPL_PR_HEAD.Remarks as 'PR_Head_Remarks',"
            End If
            qry += "TSPL_PR_HEAD.Total_Add_Charge, TSPL_PR_HEAD.PR_Total_Amt, TSPL_PR_DETAIL.TAX1, TSPL_PR_HEAD.discount_base, TSPL_PR_HEAD.TAX1, TSPL_PR_HEAD.TAX2, TSPL_PR_HEAD.TAX3, TSPL_PR_HEAD.TAX4, TSPL_PR_HEAD.TAX5, TSPL_PR_HEAD.TAX6, TSPL_PR_HEAD.TAX7, TSPL_PR_HEAD.TAX8, TSPL_PR_HEAD.TAX9, TSPL_PR_HEAD.TAX10, TSPL_PR_HEAD.Discount_Amt, TSPL_PR_HEAD.Amount_Less_Discount, TSPL_PR_HEAD.Total_Tax_Amt, TSPL_PR_HEAD.TAX1_Amt, TSPL_PR_HEAD.TAX2_Amt, TSPL_PR_HEAD.TAX3_Amt, TSPL_PR_HEAD.TAX4_Amt, TSPL_PR_HEAD.TAX5_Amt, TSPL_PR_HEAD.TAX6_Amt, TSPL_PR_HEAD.TAX7_Amt, TSPL_PR_HEAD.TAX8_Amt, TSPL_PR_HEAD.TAX9_Amt, TSPL_PR_HEAD.TAX10_Amt, TSPL_ITEM_BARCODE.Bar_Code, TSPL_COMPANY_MASTER.Comp_Name, " &
            "(case when isnull(TSPL_COMPANY_MASTER.Add1, '') <> '' then TSPL_COMPANY_MASTER.Add1 ELSE  '' END + " &
            "case when isnull(TSPL_COMPANY_MASTER.Add2, '')<>'' then ', ' + TSPL_COMPANY_MASTER.Add2 ELSE '' END  + " &
            "case when isnull(TSPL_COMPANY_MASTER.Add3, '')<>'' then ', ' + TSPL_COMPANY_MASTER.Add3 ELSE '' END ) AS Company_Address, " &
            " case when isnull(TSPL_PR_HEAD.Ship_To_Location ,'')<>'' then  (case when isnull(TSPL_SHIP_TO_LOCATION.Add1, '') <> '' then TSPL_SHIP_TO_LOCATION.Add1 ELSE  '' END + case when isnull(TSPL_SHIP_TO_LOCATION.Add2, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION.Add2 ELSE '' END  + case when isnull(TSPL_SHIP_TO_LOCATION.Add3, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION.Add3 ELSE '' END ) else  (case when isnull(TSPL_SHIP_TO_LOCATION1.Add1, '') <> '' then TSPL_SHIP_TO_LOCATION1.Add1 ELSE  '' END + case when isnull(TSPL_SHIP_TO_LOCATION1.Add2, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION1.Add2 ELSE '' END  + case when isnull(TSPL_SHIP_TO_LOCATION1.Add3, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION1.Add3 ELSE '' END ) end AS Ship_To_Location," &
            "case when isnull(TSPL_VENDOR_MASTER.Add1, '')<>'' then TSPL_VENDOR_MASTER.Add1 ELSE '' END  + " &
            "case when isnull(TSPL_VENDOR_MASTER.Add2, '')<>'' then ', ' + TSPL_VENDOR_MASTER.Add2 ELSE '' END  + " &
            "case when isnull(TSPL_VENDOR_MASTER.Add3, '')<>'' then ', ' + TSPL_VENDOR_MASTER.Add3 ELSE '' END  AS Supplier_Address,	  " &
            "TSPL_PR_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date, case when TSPL_PI_HEAD.InvoiceDate is null then '' else  Convert (varchar,TSPL_PI_HEAD.InvoiceDate,103) end as InvoiceDate,TSPL_SRN_HEAD.Against_PO, " &
            "TSPL_PR_HEAD.Against_SRN, convert(varchar(15),TSPL_SRN_HEAD.SRN_Date,106)SRN_Date, convert(varchar(15),TSPL_PR_HEAD.Due_Date,106)Due_Date, TSPL_PR_HEAD.Terms_Code, " &
            "TSPL_VENDOR_MASTER.Tin_No, TSPL_VENDOR_MASTER.CST, TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, " &
            "isnull(TSPL_PR_DETAIL.MRP,0)MRP, isnull(TSPL_PR_DETAIL.Item_Cost,0)Item_Cost, TSPL_PR_DETAIL.Unit_code, isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)PurchaseOrder_Qty, " &
            "isnull(TSPL_PR_DETAIL.PR_Qty,0)PR_Qty, isnull(TSPL_PI_DETAIL.PI_Qty,0)Balance_Qty, (isnull(TSPL_PI_DETAIL.PI_Qty,0)-isnull(TSPL_PR_DETAIL.PR_Qty,0))Difference, TSPL_PR_DETAIL.Remarks, TSPL_VENDOR_MASTER.Vendor_Name, TSPL_PR_DETAIL.Landed_Cost_Amount, " &
            " isnull(Tspl_Tax1.Tax_Code_Desc,'') as Taxdesc1,isnull(Tspl_Tax2.Tax_Code_Desc,'') as Taxdesc2,isnull(Tspl_Tax3.Tax_Code_Desc,'') as Taxdesc3,isnull(Tspl_Tax4.Tax_Code_Desc,'') as Taxdesc4,isnull(Tspl_Tax5.Tax_Code_Desc,'') as Taxdesc5,isnull(Tspl_Tax6.Tax_Code_Desc,'') as Taxdesc6,isnull(Tspl_Tax7.Tax_Code_Desc,'') as Taxdesc7,isnull(Tspl_Tax8.Tax_Code_Desc,'') as Taxdesc8,isnull(Tspl_Tax9.Tax_Code_Desc,'') as Taxdesc9,isnull(Tspl_Tax10.Tax_Code_Desc,'') as Taxdesc10 " &
            " ,TSPL_PR_HEAD.tax1_rate,TSPL_PR_HEAD.TAX2_Rate,TSPL_PR_HEAD.TAX3_Rate,TSPL_PR_HEAD.TAX4_Rate,TSPL_PR_HEAD.TAX5_Rate,TSPL_PR_HEAD.TAX6_Rate,TSPL_PR_HEAD.tax7_rate,TSPL_PR_HEAD.tax8_rate,TSPL_PR_HEAD.tax9_rate,TSPL_PR_HEAD.tax10_rate " &
            " ,TSPL_PR_HEAD.Add_Charge_code1, TSPL_PR_HEAD.Add_Charge_Name1, TSPL_PR_HEAD.Add_Charge_Amt1 ,TSPL_PR_HEAD.Add_Charge_code2, TSPL_PR_HEAD.Add_Charge_Name2, TSPL_PR_HEAD.Add_Charge_Amt2 ,TSPL_PR_HEAD.Add_Charge_code3, TSPL_PR_HEAD.Add_Charge_Name3, TSPL_PR_HEAD.Add_Charge_Amt3 ,TSPL_PR_HEAD.Add_Charge_code4, TSPL_PR_HEAD.Add_Charge_Name4, TSPL_PR_HEAD.Add_Charge_Amt4 ,TSPL_PR_HEAD.Add_Charge_code5, TSPL_PR_HEAD.Add_Charge_Name5, TSPL_PR_HEAD.Add_Charge_Amt5 ,TSPL_PR_HEAD.Add_Charge_code6, TSPL_PR_HEAD.Add_Charge_Name6, TSPL_PR_HEAD.Add_Charge_Amt6 ,TSPL_PR_HEAD.Add_Charge_code7, TSPL_PR_HEAD.Add_Charge_Name7, TSPL_PR_HEAD.Add_Charge_Amt7 ,TSPL_PR_HEAD.Add_Charge_code8, TSPL_PR_HEAD.Add_Charge_Name8, TSPL_PR_HEAD.Add_Charge_Amt8 ,TSPL_PR_HEAD.Add_Charge_code9, TSPL_PR_HEAD.Add_Charge_Name9, TSPL_PR_HEAD.Add_Charge_Amt9 ,TSPL_PR_HEAD.Add_Charge_code10, TSPL_PR_HEAD.Add_Charge_Name10, TSPL_PR_HEAD.Add_Charge_Amt10 , isnull (TSPL_PR_HEAD.RoundOffAmt,0) as RoundOffAmt,TSPL_PR_HEAD.Discount_Base,TSPL_PR_HEAD.Discount_Amt,TSPL_PR_HEAD.Amount_Less_Discount " &
            " from TSPL_PR_HEAD " &
            "LEFT OUTER JOIN TSPL_PR_DETAIL ON TSPL_PR_DETAIL.PR_No = TSPL_PR_HEAD.PR_No " &
            "LEFT OUTER JOIN TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_PR_HEAD.Vendor_Code " &
            "LEFT OUTER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code= TSPL_PR_HEAD.Comp_Code " &
            "LEFT OUTER JOIN TSPL_LOCATION_MASTER  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Location_Code  = TSPL_PR_HEAD.Ship_To_Location  LEFT OUTER JOIN TSPL_LOCATION_MASTER TSPL_SHIP_TO_LOCATION1 on TSPL_SHIP_TO_LOCATION1.Location_Code  = TSPL_PR_HEAD.Bill_To_Location " &
            "LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=TSPL_PR_HEAD.PR_No " &
            "LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No= TSPL_PR_HEAD.Against_PI " &
            "LEFT OUTER JOIN TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN " &
            "LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_SRN_HEAD.Against_PO " &
            "LEFT OUTER JOIN TSPL_PURCHASE_ORDER_DETAIL ON TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code =  TSPL_PR_DETAIL.Item_Code " &
            "LEFT OUTER JOIN TSPL_ITEM_BARCODE on TSPL_ITEM_BARCODE.Item_Code= TSPL_PR_DETAIL.Item_Code and TSPL_ITEM_BARCODE.Item_MRP = TSPL_PR_DETAIL.MRP " &
            " LEFT OUTER JOIN TSPL_PI_DETAIL ON  TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD.PI_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code  and TSPL_PI_DETAIL.SRN_Id =TSPL_PR_DETAIL .SRN_Id and  TSPL_PI_DETAIL.Line_No =TSPL_PR_DETAIL.Line_No " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax1 on Tspl_Tax1.Tax_Code =TSPL_PR_HEAD.TAX1 " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax2 on Tspl_Tax2.Tax_Code =TSPL_PR_HEAD.TAX2  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax3 on Tspl_Tax3.Tax_Code =TSPL_PR_HEAD.TAX3  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax4 on Tspl_Tax4.Tax_Code =TSPL_PR_HEAD.TAX4  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax5 on Tspl_Tax5.Tax_Code =TSPL_PR_HEAD.TAX5  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax6 on Tspl_Tax6.Tax_Code =TSPL_PR_HEAD.TAX6  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax7 on Tspl_Tax7.Tax_Code =TSPL_PR_HEAD.TAX7  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax8 on Tspl_Tax8.Tax_Code =TSPL_PR_HEAD.TAX8  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax9 on Tspl_Tax9.Tax_Code =TSPL_PR_HEAD.TAX9  " &
            " left outer join TSPL_TAX_MASTER Tspl_Tax10 on Tspl_Tax10.Tax_Code =TSPL_PR_HEAD.TAX10 " &
            " left outer join tspl_location_master on tspl_location_master.location_code= TSPL_PR_HEAD.Bill_To_Location " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_PR_DETAIL.Item_Code " &
            " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  " &
            " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code " &
            " WHERE 1 = 1 " &
            " AND TSPL_PR_HEAD.PR_No='" + StrDocNo + "' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim qtySubReport As String = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Source_Code,   TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Doc_Date,   TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_MASTER.Source_Narration,    TSPL_JOURNAL_MASTER.Remarks, TSPL_JOURNAL_MASTER.Comments, TSPL_JOURNAL_MASTER.Auto_Reverse,ISNULL(TSPL_JOURNAL_MASTER.Reverse_Date,'') AS Reverse_Date,    TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code, CASE WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='C' THEN TSPL_CUSTOMER_MASTER.CUSTOMER_NAME  WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='V' THEN TSPL_VENDOR_MASTER.VENDOR_NAME ELSE TSPL_JOURNAL_MASTER.CustVend_Name end AS CustVend_Name,    TSPL_JOURNAL_MASTER.Transaction_Type,    TSPL_JOURNAL_MASTER.Provisional_Post, TSPL_JOURNAL_MASTER.Authorized, TSPL_JOURNAL_MASTER.Total_Debit_Amt,    TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code],    TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description as [Desc],    TSPL_JOURNAL_DETAILS.Reference as [Ref], convert(varchar(11),TSPL_JOURNAL_DETAILS.Posting_Date,103) AS [Date],   TSPL_JOURNAL_MASTER.SendToTally,TSPL_JOURNAL_MASTER.Segment_code,TSPL_JOURNAL_MASTER.MonthlyReverse,TSPL_JOURNAL_DETAILS.Hirerachy_code as [Hierarchy Code],  TSPL_JOURNAL_DETAILS.Cost_Centre_Code as [Cost Centre],TSPL_JOURNAL_MASTER.Ind_As , TSPL_JOURNAL_MASTER.AgainstVoucherNoReverseEntry,TSPL_JOURNAL_MASTER.TapalNo,TSPL_JOURNAL_MASTER.DateAndTime   FROM TSPL_JOURNAL_MASTER INNER JOIN   TSPL_JOURNAL_DETAILS ON     TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No   left outer join  tspl_customer_master on tspl_customer_master.cust_code  =TSPL_JOURNAL_MASTER.CustVend_Code left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_CODE  =TSPL_JOURNAL_MASTER.CustVend_Code   WHERE " & _
                                         " TSPL_JOURNAL_MASTER.Source_doc_No = ( select TSPL_Vendor_Invoice_Head.Document_No from TSPL_Vendor_Invoice_Head where Against_PurchaseReturn_No ='" + txtDocNo.Value + "') " & _
                                         " order by [Line No]  "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qtySubReport)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                ''richa agarwal create new report against ticket no BM00000008838 for price adjsutment 04 March,2016 ,31 May 2019 open Price Adjustment report in both cases i.e Credit Note and Debit Note if Trasaction type is Price Adjustment KDI/31/05/19-000455
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(cboTrType.SelectedValue, "P") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptPriceAdjustment", "Price Adjustment Report", clsCommon.myCDate(dt.Rows(0)("PR_Date")))
                Else
                    'frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptPurchaseReturn", "Purchase Return Report",  clsCommon.myCDate(dt.Rows(0)("PR_Date")))
                    frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, dt2, "crptPurchaseReturn", "Purchase Return Report", clsCommon.myCDate(dt.Rows(0)("PR_Date")), "rptSubReportPurchaseReturnVoucherDetails.rpt", "SubRptCmpnyMasterForERODE.rpt", clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE())
                End If
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub printReport()
        Try
            Dim qry As String


            If txtDocNo.Value = "" Then
                MessageBox.Show("Please Select Document No.")
                Exit Sub
            End If

            qry = " SELECT TSPL_PR_HEAD.Created_By,TSPL_PR_HEAD.Modify_By ,  case when TSPL_PR_HEAD.Against_SRN='' OR TSPL_PR_HEAD.Against_SRN Is null then 'We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you  . Material were rejected as per following details. ' else ('We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you vide billNo.  '+TSPL_PR_HEAD.Vendor_Invoice_No+'  dt  '+ case when  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) IS NULL then'' else  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103)  end +'  and received through  GIN No.  '+TSPL_PR_HEAD.Against_SRN+'  dt  '+CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103)+' . Material were rejected as per following details. ') end as detail, " & _
                " TSPL_PR_HEAD.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_PR_HEAD.Vendor_Code, TSPL_PR_HEAD.Vendor_Name,  " & _
                   "  TSPL_PR_HEAD.Vendor_Invoice_No, CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) AS InDate, TSPL_PR_HEAD.Against_SRN, " & _
                   "  CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103) AS SRNDate, TSPL_PR_HEAD.Against_SRN AS srn, TSPL_PR_HEAD.PR_Total_Amt, " & _
                   "  TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, TSPL_PR_DETAIL.Unit_code, TSPL_PR_DETAIL.PR_Qty, TSPL_PR_DETAIL.Item_Cost, " & _
                   "  TSPL_PR_DETAIL.Amount, TSPL_PR_DETAIL.Amt_Less_Discount, TSPL_PR_DETAIL.Total_Tax_Amt, TSPL_PR_DETAIL.Item_Net_Amt, " & _
                   "  TSPL_VENDOR_MASTER.Add1, TSPL_VENDOR_MASTER.Add2, TSPL_VENDOR_MASTER.Add3, TSPL_COMPANY_MASTER.Comp_Name, " & _
                    " TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Add1 AS ComAdd, TSPL_PR_HEAD.TAX1, TSPL_PR_HEAD.TAX1_Amt, " & _
                    " TSPL_PR_HEAD.TAX2, TSPL_PR_HEAD.TAX2_Amt, TSPL_PR_HEAD.TAX3, TSPL_PR_HEAD.TAX3_Amt, TSPL_PR_HEAD.TAX4, " & _
                    " TSPL_PR_HEAD.TAX4_Amt, TSPL_PR_HEAD.TAX5, TSPL_PR_HEAD.TAX5_Amt, TSPL_PR_HEAD.TAX6, TSPL_PR_HEAD.TAX6_Amt, " & _
                    " TSPL_PR_HEAD.TAX7, TSPL_PR_HEAD.TAX7_Amt, TSPL_PR_HEAD.TAX8, TSPL_PR_HEAD.TAX8_Amt, TSPL_PR_HEAD.TAX9, " & _
         "  TSPL_PR_HEAD.TAX9_Amt, TSPL_PR_HEAD.TAX10, TSPL_PR_HEAD.TAX10_Amt " & _
           "     FROM         TSPL_PR_HEAD LEFT OUTER JOIN " & _
                   "  TSPL_PR_DETAIL ON TSPL_PR_HEAD.PR_No = TSPL_PR_DETAIL.PR_No LEFT OUTER JOIN " & _
                   "  TSPL_PI_HEAD ON TSPL_PI_HEAD.Vendor_Invoice_No = TSPL_PR_HEAD.Vendor_Invoice_No LEFT OUTER JOIN " & _
                   "  TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN LEFT OUTER JOIN " & _
                   "  TSPL_COMPANY_MASTER ON TSPL_PR_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                    " TSPL_VENDOR_MASTER ON TSPL_PR_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code where 2=2 and    TSPL_PR_HEAD.PR_No  ='" + txtDocNo.Value + "' "


            '   qry = " SELECT  case when TSPL_PR_HEAD.Against_SRN='' OR TSPL_PR_HEAD.Against_SRN Is null then 'We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you  . Material were rejected as per following details. ' else ('We have debited your A/c by  Rs ' +convert(varchar(20),TSPL_PR_HEAD.PR_Total_Amt)+ '  on A/c of QC Rejection of material supplied by you vide billNo.  '+TSPL_PR_HEAD.Vendor_Invoice_No+'  dt  '+ case when  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) IS NULL then'' else  CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103)  end +'  and received through  GIN No.  '+TSPL_PR_HEAD.Against_SRN+'  dt  '+CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103)+' . Material were rejected as per following details. ') end as detail, " & _
            '       " TSPL_PR_HEAD.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_PR_HEAD.Vendor_Code, TSPL_PR_HEAD.Vendor_Name,  " & _
            '          "  TSPL_PR_HEAD.Vendor_Invoice_No, CONVERT(varchar(12), TSPL_PI_HEAD.InvoiceDate, 103) AS InDate, TSPL_PR_HEAD.Against_SRN, " & _
            '          "  CONVERT(varchar(12), TSPL_SRN_HEAD.SRN_Date, 103) AS SRNDate, TSPL_PR_HEAD.Against_SRN AS srn, TSPL_PR_HEAD.PR_Total_Amt, " & _
            '          "  TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, TSPL_PR_DETAIL.Unit_code, TSPL_PR_DETAIL.PR_Qty, TSPL_PR_DETAIL.Item_Cost, " & _
            '          "  TSPL_PR_DETAIL.Amount, TSPL_PR_DETAIL.Amt_Less_Discount, TSPL_PR_DETAIL.Total_Tax_Amt, TSPL_PR_DETAIL.Item_Net_Amt, " & _
            '          "  TSPL_VENDOR_MASTER.Add1, TSPL_VENDOR_MASTER.Add2, TSPL_VENDOR_MASTER.Add3, TSPL_COMPANY_MASTER.Comp_Name, " & _
            '           " TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Add1 AS ComAdd, TSPL_PR_HEAD.TAX1, TSPL_PR_HEAD.TAX1_Amt, " & _
            '           " TSPL_PR_HEAD.TAX2, TSPL_PR_HEAD.TAX2_Amt, TSPL_PR_HEAD.TAX3, TSPL_PR_HEAD.TAX3_Amt, TSPL_PR_HEAD.TAX4, " & _
            '           " TSPL_PR_HEAD.TAX4_Amt, TSPL_PR_HEAD.TAX5, TSPL_PR_HEAD.TAX5_Amt, TSPL_PR_HEAD.TAX6, TSPL_PR_HEAD.TAX6_Amt, " & _
            '           " TSPL_PR_HEAD.TAX7, TSPL_PR_HEAD.TAX7_Amt, TSPL_PR_HEAD.TAX8, TSPL_PR_HEAD.TAX8_Amt, TSPL_PR_HEAD.TAX9, " & _
            '"  TSPL_PR_HEAD.TAX9_Amt, TSPL_PR_HEAD.TAX10, TSPL_PR_HEAD.TAX10_Amt " & _
            '  "     FROM         TSPL_PR_HEAD LEFT OUTER JOIN " & _
            '          "  TSPL_PR_DETAIL ON TSPL_PR_HEAD.PR_No = TSPL_PR_DETAIL.PR_No LEFT OUTER JOIN " & _
            '          "  TSPL_PI_HEAD ON TSPL_PI_HEAD.Vendor_Invoice_No = TSPL_PR_HEAD.Vendor_Invoice_No LEFT OUTER JOIN " & _
            '          "  TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN LEFT OUTER JOIN " & _
            '          "  TSPL_COMPANY_MASTER ON TSPL_PR_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
            '           " TSPL_VENDOR_MASTER ON TSPL_PR_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code where 2=2   and not(TSPL_PI_HEAD.InvoiceDate  is null) and    TSPL_PR_HEAD.PR_No  ='" + txtDocNo.Value + "' "



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptDebitAdivse", "Debit Advise Report")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
                chkExciseOnQty.Enabled = True
            ElseIf rbtnTaxCalManual.IsChecked Then
                chkExciseOnQty.Checked = False
                chkExciseOnQty.Enabled = False
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

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub chkExciseOnQty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkExciseOnQty.ToggleStateChanged
        If Not isInsideLoadData Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim qry As String = "select TSPL_PR_HEAD.PR_No,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_JOURNAL_MASTER.Voucher_No from TSPL_PR_HEAD "
            qry += "  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=TSPL_PR_HEAD.PR_No"
            qry += "  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code='AP-CN'"
            qry += "  left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD.Invoice_No=TSPL_PR_HEAD.PR_No "
            qry += "  where PR_No='" + txtDocNo.Value + "' and TSPL_PR_HEAD.Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '' reason for reverse
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                    If clsPurchasReturnHead.ReverseAndUnpost(txtDocNo.Value) Then
                        saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                        clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        Dim strLoc As String = txtBillToLocation.Value
        If chkRejected.Checked Then
            UcItemBalance1.LocationCode = clsLocation.GetRejectedLocation(txtBillToLocation.Value, Nothing)
            UcItemBalance1.LocationName = clsLocation.GetName(UcItemBalance1.LocationCode, Nothing)
        Else
            If clsCommon.myLen(txtShipToLocation.Value) > 0 Then
                UcItemBalance1.LocationCode = txtShipToLocation.Value
                UcItemBalance1.LocationName = lblShipToLocation.Text
            Else
                UcItemBalance1.LocationCode = txtBillToLocation.Value
                UcItemBalance1.LocationName = lblBillToLocation.Text
            End If
        End If

        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = False
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

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub

    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutbtn.Click
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

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub



    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    '----------------------Done By Monika For Mailing Process----------------------------------------------
#Region "Mail SMS Setting"
    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select First Purchase Return No.", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            Return
        End If

        atchqry = GetMailPrintPreview(txtDocNo.Value)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found")
        Else
            Dim frmCRV As New frmCrystalReportViewer()
            System.Diagnostics.Process.Start(frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt, "crptPurchaseReturn", "Purchase Return Report"))
            frmCRV = Nothing
        End If
    End Sub

    Public Function GetMailPrintPreview(ByVal strcode As String)
        atchqry = "select	TSPL_PR_HEAD.PR_No,	convert(varchar(15),TSPL_PR_HEAD.PR_Date,106)PR_Date, TSPL_PR_HEAD.Remarks as 'PR_Head_Remarks',TSPL_ITEM_BARCODE.Bar_Code, " & _
            "(case when isnull(TSPL_COMPANY_MASTER.Add1, '') <> '' then TSPL_COMPANY_MASTER.Add1 ELSE  '' END + " & _
            "case when isnull(TSPL_COMPANY_MASTER.Add2, '')<>'' then ', ' + TSPL_COMPANY_MASTER.Add2 ELSE '' END  + " & _
            "case when isnull(TSPL_COMPANY_MASTER.Add3, '')<>'' then ', ' + TSPL_COMPANY_MASTER.Add3 ELSE '' END ) AS Company_Address, " & _
            "(case when isnull(TSPL_SHIP_TO_LOCATION.Add1, '') <> '' then TSPL_SHIP_TO_LOCATION.Add1 ELSE  '' END + " & _
            "case when isnull(TSPL_SHIP_TO_LOCATION.Add2, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION.Add2 ELSE '' END  + " & _
            "case when isnull(TSPL_SHIP_TO_LOCATION.Add3, '')<>'' then ', ' + TSPL_SHIP_TO_LOCATION.Add3 ELSE '' END ) AS Ship_To_Location,	" & _
            "(case when isnull(TSPL_VENDOR_MASTER.Add1, '') <> '' then TSPL_VENDOR_MASTER.Add1 ELSE  '' END + " & _
            "case when isnull(TSPL_VENDOR_MASTER.Add2, '')<>'' then ', ' + TSPL_VENDOR_MASTER.Add2 ELSE '' END  + " & _
            "case when isnull(TSPL_VENDOR_MASTER.Add3, '')<>'' then ', ' + TSPL_VENDOR_MASTER.Add3 ELSE '' END ) AS Supplier_Address,	  " & _
            "TSPL_PR_HEAD.Vendor_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date, TSPL_SRN_HEAD.Against_PO, " & _
            "TSPL_PR_HEAD.Against_SRN, convert(varchar(15),TSPL_SRN_HEAD.SRN_Date,106)SRN_Date, convert(varchar(15),TSPL_PR_HEAD.Due_Date,106)Due_Date, TSPL_PR_HEAD.Terms_Code, " & _
            "TSPL_VENDOR_MASTER.Tin_No, TSPL_VENDOR_MASTER.CST, TSPL_PR_DETAIL.Item_Code, TSPL_PR_DETAIL.Item_Desc, " & _
            "isnull(TSPL_PR_DETAIL.MRP,0)MRP, isnull(TSPL_PR_DETAIL.Item_Cost,0)Item_Cost, TSPL_PR_DETAIL.Unit_code, isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0)PurchaseOrder_Qty, " & _
            "isnull(TSPL_PR_DETAIL.PR_Qty,0)PR_Qty, isnull(TSPL_PI_DETAIL.PI_Qty,0)Balance_Qty, (isnull(TSPL_PI_DETAIL.PI_Qty,0)-isnull(TSPL_PR_DETAIL.PR_Qty,0))Difference, TSPL_PR_DETAIL.Remarks, TSPL_VENDOR_MASTER.Vendor_Name " & _
            "from TSPL_PR_HEAD " & _
            "LEFT OUTER JOIN TSPL_PR_DETAIL ON TSPL_PR_DETAIL.PR_No = TSPL_PR_HEAD.PR_No " & _
            "LEFT OUTER JOIN TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_PR_HEAD.Vendor_Code " & _
            "LEFT OUTER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code= TSPL_PR_HEAD.Comp_Code " & _
            "LEFT OUTER JOIN TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code = TSPL_PR_HEAD.Ship_To_Location " & _
            "LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=TSPL_PR_HEAD.PR_No " & _
            "LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No= TSPL_PR_HEAD.Against_PI " & _
            "LEFT OUTER JOIN TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No = TSPL_PR_HEAD.Against_SRN " & _
            "LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_SRN_HEAD.Against_PO " & _
            "LEFT OUTER JOIN TSPL_PURCHASE_ORDER_DETAIL ON TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code =  TSPL_PR_DETAIL.Item_Code " & _
            "LEFT OUTER JOIN TSPL_ITEM_BARCODE on TSPL_ITEM_BARCODE.Item_Code= TSPL_PR_DETAIL.Item_Code and TSPL_ITEM_BARCODE.Item_MRP = TSPL_PR_DETAIL.MRP " & _
            "LEFT OUTER JOIN TSPL_PI_DETAIL ON  TSPL_PI_DETAIL.PI_No = TSPL_PI_HEAD.PI_No AND TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code " & _
            "WHERE 1 = 1 " & _
            "AND TSPL_PR_HEAD.PR_No='" + strcode + "' "
        Return atchqry
    End Function

    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseReturn)

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
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            ''------------------------code for attchament-------------------------------------
            'Dim strRptPath As String = ""
            'If obj.atchmnt = "Y" Then
            '    atchqry = GetMailPrintPreview(txtDocNo.Value)
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            '    If dt1.Rows.Count > 0 Then
            '        Dim frmCRV As New frmCrystalReportViewer()
            '        strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "crptPurchaseReturn", "Purchase Return Report")
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
            '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
            '        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    lstReceiptents.Add(emailId)

            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)

            'Next
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)

            'Ticket No-TEC/30/07/19-000972 sanjay
            Dim strContactPerson As String = ""

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseReturn + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()

            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, txtVendorNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.mbtnPurchaseReturn)


                    ''------------------------code for attchament-------------------------------------
                    Dim strRptPath As String = ""
                    'If obj.atchmnt = "Y" Then
                    atchqry = GetMailPrintPreview(txtDocNo.Value)
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
                    If dt1.Rows.Count > 0 Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        strRptPath = frmCRV.funreport(True, CrystalReportFolder.PurchaseOrder, dt1, "crptPurchaseReturn", "Purchase Return Report")
                        frmCRV = Nothing
                        objEmailH.Attachment_1_Path = strRptPath
                    End If
                    'End If
                    ''---------------------------------------------------------------------------

                    For Each strUser As String In lstUsers
                        'lstUsers.Add(dr("User_Code").ToString())
                        'Dim lstReceiptents As New List(Of String)
                        Dim qry As String = ""
                        Dim emailId As String = ""
                        If isSendForApproval Then
                            strContactPerson = strUser
                            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                            emailId = clsDBFuncationality.getSingleValue(qry)
                        Else
                            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")
                            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")
                        End If

                        'lstReceiptents.Add(emailId)
                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))


                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)

                    Next


                    objEmailH.SaveData(clsUserMgtCode.mbtnPurchaseReturn, objEmailH, Nothing)
                    objEmailH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "First do email and sms setting", Me.Text)
            End If

            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                SMSSENDONLY(False)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub SMSSENDONLY(ByVal isPost As Boolean)
        Try

            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseReturn)

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
            'If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.CustomerName) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.InvoiceNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)
            'End If

            'Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' ")

            'If clsSMSSend.SendSMS(clsUserMgtCode.mbtnPurchaseReturn, strMes, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' "))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseReturn + "'", Nothing)
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, txtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.mbtnPurchaseReturn)
                End If



                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SaveData(clsUserMgtCode.mbtnPurchaseReturn, objSMSH, Nothing)
                    objSMSH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "SMS Send Successfully", Me.Text)
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


    Private Sub btnsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.mbtnPurchaseReturn
        frm.ShowDialog()
    End Sub
#End Region
    '----------------------------------------------------------

    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim frm As New FrmPurchaseHistory
        frm.SetUserMgmt(clsUserMgtCode.FrmPurchaseHistory)
        frm.strFormId = MyBase.Form_ID
        frm.strVendorCode = txtVendorNo.Value
        frm.strVendorName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub CboNoteType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CboNoteType.SelectedIndexChanged
        Try
            If CboNoteType.SelectedValue = "C" Then
                'lbltrtype.Visible = False
                'cboTrType.Visible = False
                gv1.Columns(colQty).HeaderText = gv1.Columns(colQty).HeaderText.Replace("Return", "Received")
            Else
                ' lbltrtype.Visible = True
                'cboTrType.Visible = True
                gv1.Columns(colQty).HeaderText = gv1.Columns(colQty).HeaderText.Replace("Received", "Return")
            End If
            LoadTransactionType()
            'If CboNoteType.SelectedValue = "D" Then
            '    cboTrType.SelectedValue = "R"
            'End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub cboTrType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTrType.SelectedIndexChanged
        If clsCommon.CompairString(clsCommon.myCstr(Me.cboTrType.SelectedValue), "R") = CompairStringResult.Equal Then
            Me.CboNoteType.SelectedValue = "D"
            Me.CboNoteType.Enabled = False
        Else
            Me.CboNoteType.Enabled = True
        End If
    End Sub



    Private Sub btnShowJE_Click(sender As Object, e As EventArgs) Handles btnShowJE.Click
        clsOpenJEAgainstInvoice.ShowJEForPurchaseReturn(txtDocNo.Value)
    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtBillToLocation.Value)) > 0 Then
                txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" + txtBillToLocation.Value + "'", txtSubLocation.Value, isButtonClicked)
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocation.Text = ""
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridACInsurance()
        gvACInsurance.Rows.Clear()
        gvACInsurance.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colACInsuranceCode
        repoACCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 100
        repoACCode.ReadOnly = False
        gvACInsurance.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACInsuranceName
        repoACName.Width = 150
        repoACName.ReadOnly = True
        gvACInsurance.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colACInsuranceAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvACInsurance.MasterTemplate.Columns.Add(repoACAmt)

        gvACInsurance.AllowAddNewRow = False
        gvACInsurance.ShowGroupPanel = False
        gvACInsurance.AllowColumnReorder = True
        gvACInsurance.AllowRowReorder = False
        gvACInsurance.EnableSorting = False
        gvACInsurance.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvACInsurance.MasterTemplate.ShowRowHeaderColumn = False
        gvACInsurance.TableElement.TableHeaderHeight = 40
        gvACInsurance.Rows.AddNew()
    End Sub
    Private Sub gvACInsurance_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvACInsurance.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvACInsurance.Columns(colACInsuranceCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value = obj.Code
                            gvACInsurance.CurrentRow.Cells(colACInsuranceName).Value = obj.desc
                        Else
                            gvACInsurance.CurrentRow.Cells(colACInsuranceCode).Value = ""
                            gvACInsurance.CurrentRow.Cells(colACInsuranceName).Value = ""
                            gvACInsurance.CurrentRow.Cells(colACInsuranceAmount).Value = 0
                        End If
                    ElseIf e.Column Is gvACInsurance.Columns(colACInsuranceAmount) Then
                        CalculateInsuranceTotal(True)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CalculateInsuranceTotal(ByVal CalculateItemRow As Boolean)
        Dim dblACAmount As Decimal = 0
        For ii As Integer = 0 To gvACInsurance.Rows.Count - 1
            If (clsCommon.myLen(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value) > 0) Then
                dblACAmount += clsCommon.myCdbl(gvACInsurance.Rows(ii).Cells(colACInsuranceAmount).Value)
            End If
        Next
        lblAddChargesForInsurance.Text = clsCommon.myFormat(dblACAmount)
        lblAddChargesForInsurance1.Text = clsCommon.myFormat(dblACAmount)
        If CalculateItemRow Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
    End Sub
    Private Sub gvACInsurance_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvACInsurance.UserDeletedRow
        UpdateAllTotals()
    End Sub
    Private Sub gvACInsurance_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvACInsurance.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gvACInsurance_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvACInsurance.CurrentColumnChanged
        If gvACInsurance.RowCount > 0 Then
            Dim intCurrRow As Integer = gvACInsurance.CurrentRow.Index
            If intCurrRow = gvACInsurance.Rows.Count - 1 Then
                gvACInsurance.Rows.AddNew()
                gvACInsurance.CurrentRow = gvACInsurance.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub Btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", IIf(clsCommon.myLen(txtShipToLocation.Value) <= 0, txtBillToLocation.Value, txtShipToLocation.Value), txtDate.Value, Nothing)
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If

            Dim dtAcquisition As DataTable = clsDBFuncationality.GetDataTable("SELECT Acquisition_Code FROM TSPL_ACQUISITION_HEAD WHERE PI_No='" + txtDocNo.Value + "'")
            Dim TempAcqNO As String = ""
            If dtAcquisition IsNot Nothing AndAlso dtAcquisition.Rows.Count > 0 Then
                TempAcqNO = "Purchase Invoice - " + txtDocNo.Value + " is used in following Acquisition Entry -"
                For Each drAcq As DataRow In dtAcquisition.Rows
                    TempAcqNO += Environment.NewLine + clsCommon.myCstr(drAcq("Acquisition_Code"))
                Next
                clsCommon.MyMessageBoxShow(Me, TempAcqNO, Me.Text)
                Exit Sub
            End If

            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            If clsPurchasReturnHead.CancelData(Me.Form_ID, txtDocNo.Value) Then
                clsCommon.MyMessageBoxShow("Purchase Return cancelled successfully!")
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNewHistory_Click(sender As Object, e As EventArgs) Handles btnNewHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtDocNo.Value, "PR_No", "TSPL_PR_HEAD", "TSPL_PR_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
End Class
