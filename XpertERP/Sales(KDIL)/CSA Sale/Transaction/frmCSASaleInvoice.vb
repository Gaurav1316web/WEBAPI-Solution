'============BM00000003604========created by Monika----BM00000004383--------BM00000004164-------BM00000004017
''updation by richa agarwal against ticket no BM00000004204-------BM00000004499--BM00000004883
''BM00000007969 ,remove effect of booking on setting base
'BM00000008068,BM00000008069,BM00000008067 hide alt. unit and qty, and check knock-off
Imports common
Imports System.Data.SqlClient

Imports System.IO
Imports System.ComponentModel


Public Class FrmCSASaleInvoice
    Inherits FrmMainTranScreen

#Region "variables"
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim RunBatchFifowise As Integer = 0
    Dim AllowNLevel As Boolean = False
    Dim ForUDLOnly As Boolean = False
    Dim AllowRate_Readonly As Boolean = False
    Dim AllowDistibutorSale As Boolean = False
    Dim OpenALLTaxes As Boolean = False
    Dim AllowRoundOff_onInvoice As Boolean = False
    Dim Apply_PriceChat_On_OtherItems As Boolean = False
    Dim ApplyFreight_Cmmsn_Charge_Itemwise As Boolean = False
    Dim ExciseentryOnSale As Boolean = False
    Dim SetZeroValue As Boolean = False
    Public StrDocNo As String = ""
    Dim ReportID As String = "CSASALEPATTI"
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
    Dim ButtonToolTip As New ToolTip()
    Dim arrLoc As String = ""
    Dim BookingEffectOnSale As Boolean = True
    Dim TransferManual_KnockOFF As Boolean = False
    Dim Item_TaxType As Integer = 0


    '=============transfer grid======================
    Const colSetZeroValue As String = "colSetZeroValue"
    Const coltransAltQty As String = "TransAltQty"
    Const coltranslineno As String = "lineno"
    Const colTranscode As String = "transcode"
    Const coltransitemcode As String = "itemcode"
    Const coltransiname As String = "iname"
    Const coltransuom As String = "transuom"
    Const coltrans_actual_qty As String = "transqty"
    Const coltransqty As String = "qty"
    Const coltransbal_qty As String = "TransBal_Qty"
    Const coltrans_sale_uom As String = "transaleuom"
    Const coltransrate As String = "transrate"
    Const colTransConvFactor As String = "Trans_Conv_Fatcor"
    Const colTransFOC As String = "TransFOC"
    Const colIsBatchItem As String = "colIsBatchItem"
    '===============================================
    '==tax grid---------------------
    Const colTBaseAmt As String = "BASEAMT"
    Const colTTaxAutName As String = "AuthName"
    Const colTTaxAutCode As String = "AuthCode"
    Const colTTaxRate As String = "TaxRate"
    Const colTTaxAmt As String = "taxamt"
    '--------Additional grid--------------------------------
    Const colACCode As String = "Addcode"
    Const colACName As String = "Addname"
    Const colACAmount As String = "ACamt"

    '------------main grid-------------------
    Const colComm_Type_RS_Pers As String = "Comm_Per_RS"
    Const colSchmCode As String = "SchmCode"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"
    Const colMainLineNo As String = "MainLineNo"
    Const colFOC As String = "FOC"
    Const colIsSchmItem As String = "SchmItem"
    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    Const colMasterPackSize As String = "MasterPackSize"
    Const colPackSize As String = "PackSize"
    Const colOtherCharge As String = "OtherChrage"
    Const colCommision As String = "Commision"
    Const colCommisionValue As String = "CommisionValue"
    Const colConversionFactor As String = "ConvrsnFactor"
    Const coldoubleclick As String = "DoubleClick"
    Const colLinenno As String = "Lineno"
    Const coldate As String = "date"
    Const colBookingno As String = "bookingno"
    Const colbookingtype As String = "bookingtype"
    Const colitemcode As String = "icode"
    Const colHSNCode As String = "HSNCode"
    Const coliname As String = "iname"
    Const colItemType As String = "Itemtype"
    Const colCSAType As String = "CSAType"
    Const colItemUOM As String = "itemuom"
    Const colqty As String = "qty"
    Const colbalqty As String = "balqty"
    Const colAltUOM As String = "AltUOM"
    Const colAltQty As String = "AltQty"
    Const colBookQty As String = "BookQty"
    Const colbookingrate As String = "bookrate"
    Const colUnitRate As String = "UnitRate"
    Const colincludingtax As String = "Tax"
    Const colTaxBasis As String = "TaxBasis"
    Const colSaleRate As String = "SaleRate"
    Const colSaleValue As String = "Salevalue"
    Const colStckTransferrate As String = "Stckrate"
    Const colstckratevalue As String = "stckratevalue"
    Const colGainLoss As String = "GainLoss"
    Const colRemarks As String = "Remarks"
    Const colDisPer As String = "discpers"
    Const colDisAmt As String = "discamt"
    Const colAmtAfterDis As String = "aftrediscamt"
    Const colTax1 As String = "tax1"
    Const colTaxBaseAmt1 As String = "taxbaseamt1"
    Const colTaxRate1 As String = "taxrate1"
    Const colTaxAmt1 As String = "tacamt1"
    Const colIsSurTax1 As String = "surtax1"
    Const colSurTaxCode1 As String = "surtaxcode1"
    Const colIsTaxable1 As String = "taxable1"
    Const colTaxRecoverable1 As String = "recvrabletax1"

    Const colTax2 As String = "tax2"
    Const colTaxBaseAmt2 As String = "taxbaseamt2"
    Const colTaxRate2 As String = "taxrate2"
    Const colTaxAmt2 As String = "tacamt2"
    Const colIsSurTax2 As String = "surtax2"
    Const colSurTaxCode2 As String = "surtaxcode2"
    Const colIsTaxable2 As String = "taxable2"
    Const colTaxRecoverable2 As String = "recvrabletax2"

    Const colTax3 As String = "tax3"
    Const colTaxBaseAmt3 As String = "taxbaseamt3"
    Const colTaxRate3 As String = "taxrate3"
    Const colTaxAmt3 As String = "tacamt3"
    Const colIsSurTax3 As String = "surtax3"
    Const colSurTaxCode3 As String = "surtaxcode3"
    Const colIsTaxable3 As String = "taxable3"
    Const colTaxRecoverable3 As String = "recvrabletax3"

    Const colTax4 As String = "tax4"
    Const colTaxBaseAmt4 As String = "taxbaseamt4"
    Const colTaxRate4 As String = "taxrate4"
    Const colTaxAmt4 As String = "tacamt4"
    Const colIsSurTax4 As String = "surtax4"
    Const colSurTaxCode4 As String = "surtaxcode4"
    Const colIsTaxable4 As String = "taxable4"
    Const colTaxRecoverable4 As String = "recvrabletax4"

    Const colTax5 As String = "tax5"
    Const colTaxBaseAmt5 As String = "taxbaseamt5"
    Const colTaxRate5 As String = "taxrate5"
    Const colTaxAmt5 As String = "tacamt5"
    Const colIsSurTax5 As String = "surtax5"
    Const colSurTaxCode5 As String = "surtaxcode5"
    Const colIsTaxable5 As String = "taxable5"
    Const colTaxRecoverable5 As String = "recvrabletax5"

    Const colTax6 As String = "tax6"
    Const colTaxBaseAmt6 As String = "taxbaseamt6"
    Const colTaxRate6 As String = "taxrate6"
    Const colTaxAmt6 As String = "tacamt6"
    Const colIsSurTax6 As String = "surtax6"
    Const colSurTaxCode6 As String = "surtaxcode6"
    Const colIsTaxable6 As String = "taxable6"
    Const colTaxRecoverable6 As String = "recvrabletax6"

    Const colTax7 As String = "tax7"
    Const colTaxBaseAmt7 As String = "taxbaseamt7"
    Const colTaxRate7 As String = "taxrate7"
    Const colTaxAmt7 As String = "tacamt7"
    Const colIsSurTax7 As String = "surtax7"
    Const colSurTaxCode7 As String = "surtaxcode7"
    Const colIsTaxable7 As String = "taxable7"
    Const colTaxRecoverable7 As String = "recvrabletax7"

    Const colTax8 As String = "tax8"
    Const colTaxBaseAmt8 As String = "taxbaseamt8"
    Const colTaxRate8 As String = "taxrate8"
    Const colTaxAmt8 As String = "tacamt8"
    Const colIsSurTax8 As String = "surtax8"
    Const colSurTaxCode8 As String = "surtaxcode8"
    Const colIsTaxable8 As String = "taxable8"
    Const colTaxRecoverable8 As String = "recvrabletax8"

    Const colTax9 As String = "tax9"
    Const colTaxBaseAmt9 As String = "taxbaseamt9"
    Const colTaxRate9 As String = "taxrate9"
    Const colTaxAmt9 As String = "tacamt9"
    Const colIsSurTax9 As String = "surtax9"
    Const colSurTaxCode9 As String = "surtaxcode9"
    Const colIsTaxable9 As String = "taxable9"
    Const colTaxRecoverable9 As String = "recvrabletax9"

    Const colTax10 As String = "tax10"
    Const colTaxBaseAmt10 As String = "taxbaseamt10"
    Const colTaxRate10 As String = "taxrate10"
    Const colTaxAmt10 As String = "tacamt10"
    Const colIsSurTax10 As String = "surtax10"
    Const colSurTaxCode10 As String = "surtaxcode10"
    Const colIsTaxable10 As String = "taxable10"
    Const colTaxRecoverable10 As String = "recvrabletax10"

    Const colAmtAfterTax As String = "taxincludeamt"
    Const colTotTaxAmt As String = "totaltaxamt"
    Const colIsExcisable1 As String = "ex1"
    Const colIsExcisable2 As String = "ex2"
    Const colIsExcisable3 As String = "ex3"
    Const colIsExcisable4 As String = "ex4"
    Const colIsExcisable5 As String = "ex5"
    Const colIsExcisable6 As String = "ex6"
    Const colIsExcisable7 As String = "ex7"
    Const colIsExcisable8 As String = "ex8"
    Const colIsExcisable9 As String = "ex9"
    Const colIsExcisable10 As String = "ex10"

    ''=============================
    Const colFrghtType As String = "FrghtType" 'percentage,amount
    Const colFrghtRate As String = "FrghtRate"
    Const colFrghtAmt As String = "FrghtAmt"
    ''===============================


    ''=============columns for uploader================================
    Const colUDocCode As String = "UDocCode"
    Const colUDocDate As String = "UDocDate"
    Const colUDesc As String = "UDesc"
    Const colUCSACode As String = "UCSACode"
    Const colUDistributorCode As String = "UDistributorCode"
    Const colUBillToLocation As String = "UBilltoLocation"
    Const ColUToLocation As String = "UToLocation"
    Const colUDocAmount As String = "UDocAmount"
    Const colUTaxGroup1 As String = "UTaxGroup"
    ''==================header taxes=======================
    Const colUTBaseAmt1 As String = "UBASEAMT1"
    Const colUTTaxAutCode1 As String = "UAuthCode1"
    Const colUTTaxRate1 As String = "UTaxRate1"
    Const colUTTaxAmt1 As String = "Utaxamt1"

    Const colUTBaseAmt2 As String = "UBASEAMT2"
    Const colUTTaxAutCode2 As String = "UAuthCode2"
    Const colUTTaxRate2 As String = "UTaxRate2"
    Const colUTTaxAmt2 As String = "Utaxamt2"

    Const colUTBaseAmt3 As String = "UBASEAMT3"
    Const colUTTaxAutCode3 As String = "UAuthCode3"
    Const colUTTaxRate3 As String = "UTaxRate3"
    Const colUTTaxAmt3 As String = "Utaxamt3"

    Const colUTBaseAmt4 As String = "UBASEAMT4"
    Const colUTTaxAutCode4 As String = "UAuthCode4"
    Const colUTTaxRate4 As String = "UTaxRate4"
    Const colUTTaxAmt4 As String = "Utaxamt4"

    Const colUTBaseAmt5 As String = "UBASEAMT5"
    Const colUTTaxAutCode5 As String = "UAuthCode5"
    Const colUTTaxRate5 As String = "UTaxRate5"
    Const colUTTaxAmt5 As String = "Utaxamt5"

    Const colUTBaseAmt6 As String = "UBASEAMT6"
    Const colUTTaxAutCode6 As String = "UAuthCode6"
    Const colUTTaxRate6 As String = "UTaxRate6"
    Const colUTTaxAmt6 As String = "Utaxamt6"

    Const colUTBaseAmt7 As String = "UBASEAMT7"
    Const colUTTaxAutCode7 As String = "UAuthCode7"
    Const colUTTaxRate7 As String = "UTaxRate7"
    Const colUTTaxAmt7 As String = "Utaxamt7"

    Const colUTBaseAmt8 As String = "UBASEAMT8"
    Const colUTTaxAutCode8 As String = "UAuthCode8"
    Const colUTTaxRate8 As String = "UTaxRate8"
    Const colUTTaxAmt8 As String = "Utaxamt8"

    Const colUTBaseAmt9 As String = "UBASEAMT9"
    Const colUTTaxAutCode9 As String = "UAuthCode9"
    Const colUTTaxRate9 As String = "UTaxRate9"
    Const colUTTaxAmt9 As String = "Utaxamt9"

    Const colUTBaseAmt10 As String = "UBASEAMT10"
    Const colUTTaxAutCode10 As String = "UAuthCode10"
    Const colUTTaxRate10 As String = "UTaxRate10"
    Const colUTTaxAmt10 As String = "Utaxamt10"
    ''=================end taxes=============================
    Const colUTermCode As String = "UTermCode"
    Const colUDueDate As String = "UDueDate"
    Const colUDoc_Amt_WO_Disc As String = "colUDoc_Amt_WO_Disc"
    Const colUAmt_After_Disc As String = "colUAmt_After_Disc"
    Const colUTaxAmt As String = "colUTaxAmt"
    Const colUTCommsnAmt As String = "colUTCommsnAmt"
    Const colUTFreightAmt As String = "colUTFreightAmt"
    Const colUTOtherAmt As String = "colUTOtherAmt"
    Const colURoundOff As String = "colURoundOff"
    Const colUAddtnlAmt As String = "UAdditinalAmt"
    Const colUInvDiscAmt As String = "UInvDiscAMt"
    Const colUTDiscAmt As String = "UTdiscAmt"
    Const colUExciseType As String = "UExciseType"

    Const colUValidateRemark As String = "colUValidateRemark"
    Const colUResetScheme_LineNo As String = "colUResetScheme_LineNo"
    ''==============end here========================================================


    Const colMRP As String = "MRP"
    Const colIsMRPMandatory As String = "IsMRPMandatory"
    Const colAbatementPers As String = "AbatementPers"
    Const colAbatementAmt As String = "AbatementAmt"

    Dim CSAPricePostedData As Boolean
    Dim showBatchSkipAtCSAReturn As Boolean = False
    Dim CalculateCommOnCSATransWOConversion As Integer = 0
#End Region

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then
                    txt_loc_code.Value = obj.Default_LocCode
                    txt_loc_name.Text = obj.Default_LocName
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSASaleInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag

        RadMenuItem3.Visibility = IIf(MyBase.isExport = True, ElementVisibility.Visible, ElementVisibility.Collapsed)
        RadMenuItem4.Visibility = IIf(MyBase.isExport = True, ElementVisibility.Visible, ElementVisibility.Collapsed)
    End Sub

    Private Sub FunReset()
        If AllowNLevel Then
            btnpost.Visible = MyBase.isPostFlag
        End If
        btnRev_Unpost.Visible = False
        RadPageViewPage6.Item.Visibility = ElementVisibility.Collapsed
        RadPageViewPage7.Item.Visibility = ElementVisibility.Collapsed

        btnexcel.Visible = False
        ChkFOC.Enabled = True
        ChkFOC.Checked = False
        txtCode.Value = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)

        txtDesc.Text = ""
        txtDesc.Text = clsCSASaleInvoice.GetCSASALEDescrptn()

        txtDistributor_Name.Text = ""
        fndDistributorCode.Value = ""
        txtcustcode.Value = ""
        txtcustName.Text = ""
        txtCSAloc_code.Value = ""
        txtCSAloc_name.Text = ""
        txt_loc_code.Value = ""
        txt_loc_name.Text = ""
        txt_loc_code.Enabled = True
        txtPONo.Text = ""
        lblTotRAmt1.Text = ""
        txtRoundOff.Text = ""

        LOCATIONRIGTHS()

        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv2.Rows.Clear()
        gvAC.Rows.Clear()

        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtCurrencyCode.Value = ""
        txtConversionRate.Text = 1
        lblAmtWithDiscount.Text = ""
        chkDiscountOnRate.IsChecked = True
        txtDiscPer.Text = 0
        txtDiscAmt.Text = 0
        lblInvoiceDiscAmt.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblAddCharges1.Text = ""
        lblTotRAmt.Text = ""
        lblAddCharges.Text = ""
        txtApplicableFrom.Text = ""

        txtCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
        txtCSAloc_code.Enabled = True
        fndDistributorCode.Enabled = True
        txtcustcode.Enabled = True


        cmbEXType.SelectedValue = "N"
        cmbEXType.Enabled = True

        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            qry = "delete from CSA_SALE_TRANSFER"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        SetMailRight()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtDesc.Focus()
        txtDesc.Select()
    End Sub

    Private Sub txtcustcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcustcode._MYValidating
        txtcustcode.Value = clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' ", txtcustcode.Value, isButtonClicked)

        If clsCommon.myLen(txtcustcode.Value) > 0 Then
            txtcustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtcustcode.Value + "'"))
            txtCSAloc_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where cust_code='" + txtcustcode.Value + "' "))
            txtCSAloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txtCSAloc_code.Value + "'"))
            txtTermCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code from tspl_customer_master where cust_code='" + txtcustcode.Value + "'"))
        Else
            txtcustName.Text = ""
            txtCSAloc_code.Value = ""
            txtCSAloc_name.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtDistributor_Name.Text = ""
            fndDistributorCode.Value = ""
        End If

        SetTax()
        SetTermDetails()
    End Sub

    Private Sub SetTax()
        If Not OpenALLTaxes Then ''when not state condition then default tax fill
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txt_loc_code.Value, txtcustcode.Value, "S")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        End If
    End Sub

    Private Sub FrmCSASaleInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                btnpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            End If

            If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 AndAlso MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnRev_Unpost.Visible = True
                End If
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colitemcode) Then
                isCellValueChanged = True
                OpenIcode(True)
                CalAltQty(gv.CurrentRow.Index)
                CalConversionFactor_GV(gv.CurrentRow.Index)
                CalUnitPrice(gv.CurrentRow.Index, True)
                CommisionValue(gv.CurrentRow.Index)
                UpdateCurrentRow(gv.CurrentRow.Index)
                UpdateAllTotals()
                isCellValueChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colBookingno) Then ' AndAlso Not ChkFOC.Checked
                isCellValueChanged = True
                OpenBooking(True)
                isCellValueChanged = False
            End If

            If (e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colItemUOM)) Then
                isCellValueChanged = True
                OpenAltUOM(True)
                FillTransferStockData(False)
                FillFreeItemsInGrid()
                CalAltQty(gv.CurrentRow.Index)
                CalConversionFactor_GV(gv.CurrentRow.Index)
                CalUnitPrice(gv.CurrentRow.Index, True)
                CommisionValue(gv.CurrentRow.Index)
                gv.CurrentRow.Cells(colSaleRate).Value = GetTransferRate(gv.CurrentRow.Index)
                UpdateCurrentRow(gv.CurrentRow.Index)
                UpdateAllTotals()
                'gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                'gv.CurrentRow.Cells(colstckratevalue).Value = 0
                isCellValueChanged = False
            End If
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F6 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SetCSATransferwithZeroOnSalePatti"
                frm.strCode = "SetCSATransferwithZeroOnSalePatti"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    SetZeroValue = True
                    gv.Columns(colSetZeroValue).IsVisible = True
                    gv.Columns(colSetZeroValue).Width = 100
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTaxType()
        cmbEXType.DataSource = Nothing
        cmbEXType.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'N' as Code,'Non-Taxable' as Name union all select 'E' as Code,'Taxable' as Name")
        cmbEXType.DisplayMember = "Name"
        cmbEXType.ValueMember = "Code"
    End Sub

    Private Sub FrmCSASaleInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmCSASaleInvoice)
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        SetUserMgmtNew()
        CalculateCommOnCSATransWOConversion = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateCommOnCSATransWOConversion, clsFixedParameterCode.CalculateCommOnCSATransWOConversion, Nothing)) = 1, True, False)
        CSAPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCSAPriceMasterPostedData, clsFixedParameterCode.AllowCSAPriceMasterPostedData, Nothing)) = 1, True, False)
        AllowDistibutorSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDistributorSaleAtCSA_SaleInvoice, clsFixedParameterCode.AllowDistributorSaleAtCSA_SaleInvoice, Nothing)) = "1", True, False))
        AllowRate_Readonly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoReadonly_UnitRate_AtCSASale, clsFixedParameterCode.DoReadonly_UnitRate_AtCSASale, Nothing)) = 1, True, False)

        ApplyFreight_Cmmsn_Charge_Itemwise = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightChargeOnCSASaleInvoice, clsFixedParameterCode.FreightChargeOnCSASaleInvoice, Nothing)) = "1", True, False))
        Apply_PriceChat_On_OtherItems = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False))
        AllowRoundOff_onInvoice = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundOff_OnCSASalePatti, clsFixedParameterCode.AllowRoundOff_OnCSASalePatti, Nothing)) = "1", True, False))
        ForUDLOnly = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)) = "1", True, False))
        ExciseentryOnSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnableExciseONCSASalePatti, clsFixedParameterCode.EnableExciseONCSASalePatti, Nothing)) = "1", True, False))
        showBatchSkipAtCSAReturn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BatchSkipCSAReturn, clsFixedParameterCode.BatchSkipCSAReturn, Nothing)) = 1, True, False)
        MyLabel8.Visible = AllowRoundOff_onInvoice
        txtRoundOff.Visible = AllowRoundOff_onInvoice
        MyLabel14.Visible = AllowRoundOff_onInvoice
        txtTotalFreightAmt.Visible = AllowRoundOff_onInvoice

        cmbEXType.Visible = ExciseentryOnSale
        MyLabel21.Visible = ExciseentryOnSale

        If Not AllowDistibutorSale Then
            SplitContainer3.Panel2Collapsed = True
        ElseIf AllowDistibutorSale Then
            SplitContainer3.Panel2Collapsed = False
        End If

        OpenALLTaxes = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CSATransfer_SalePatti_All_Tax_Open, clsFixedParameterCode.CSATransfer_SalePatti_All_Tax_Open, Nothing)) = "1", True, False))
        ''if setting is ON then booking is mandatory  
        BookingEffectOnSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BOOKINGFINDER_ON_CSASALEPATTI, clsFixedParameterCode.BOOKINGFINDER_ON_CSASALEPATTI, Nothing)) = "1", True, False))

        ''Knock-off transfer on sale patti manually, or auto on below setting based
        '' against ticket :BM00000009263
        TransferManual_KnockOFF = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickManual_CSATransfer_OnCSASalePatti, clsFixedParameterCode.PickManual_CSATransfer_OnCSASalePatti, Nothing)) = "1", True, False))
        ''====================================================

        LoadBlankGrid()
        LoadBlankGridTax()
        LoadBlankGridAC()
        LoadTaxType()



        '=============create demo table for datasave of transfer=====================
        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        ElseIf check > 0 Then
            qry = "drop table CSA_SALE_TRANSFER"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If
        '================================================================================
        FunReset()

        SetMultiCurrencyVisibility()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+N for reset window.")

        RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
        RadPageViewPage6.Item.Visibility = ElementVisibility.Collapsed
        btnexcel.Visible = False
        If clsCommon.myLen(StrDocNo) > 0 Then
            LoadData(StrDocNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        ChkFOC.Checked = False
        ChkFOC.Visible = False

        btnRev_Unpost.Visible = False

        RadPageViewPage7.Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Function LoadComboBox() As DataTable
        Dim qry As String = "select '' as Code,'None' as Name union all select 'Back Calculation' as Code,'Back Calculation' as Name union all select 'Forward Calculation' as Code,'Forward Calculation' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function

    Private Function LoadType_Item_Group() As DataTable

        Dim qry As String = "select 'Item-Wise' as Code,'Item-Wise' as Name union all select 'Group-Wise' as Code,'Group-Wise' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLinenno
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoLineNo)
        repoLineNo = Nothing

        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd/MM/yyyy"
        repoPriceDate.HeaderText = "Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = coldate
        repoPriceDate.Width = 80
        repoPriceDate.IsVisible = BookingEffectOnSale 'if true then visible
        repoPriceDate.VisibleInColumnChooser = BookingEffectOnSale
        gv.MasterTemplate.Columns.Add(repoPriceDate)
        repoPriceDate = Nothing

        Dim repoItemwise As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoItemwise.FormatString = ""
        repoItemwise.HeaderText = "Row Type"
        repoItemwise.WrapText = True
        repoItemwise.Name = colbookingtype
        repoItemwise.Width = 80
        repoItemwise.DataSource = LoadType_Item_Group()
        repoItemwise.DisplayMember = "Name"
        repoItemwise.ValueMember = "Code"
        repoItemwise.IsVisible = BookingEffectOnSale
        repoItemwise.VisibleInColumnChooser = BookingEffectOnSale
        gv.MasterTemplate.Columns.Add(repoItemwise)
        repoItemwise = Nothing

        Dim repoBookingCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBookingCode.FormatString = ""
        repoBookingCode.HeaderText = "Booking No"
        repoBookingCode.Name = colBookingno
        repoBookingCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoBookingCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBookingCode.Width = 100
        repoBookingCode.IsVisible = BookingEffectOnSale
        repoBookingCode.VisibleInColumnChooser = BookingEffectOnSale
        gv.MasterTemplate.Columns.Add(repoBookingCode)
        repoBookingCode = Nothing

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colitemcode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        'repoICode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoICode)
        repoICode = Nothing

        Dim HSNCode As New GridViewTextBoxColumn()
        HSNCode.FormatString = ""
        HSNCode.HeaderText = "HSN Code"
        HSNCode.Name = colHSNCode
        HSNCode.Width = 100
        HSNCode.ReadOnly = True
        HSNCode.WrapText = True
        HSNCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(HSNCode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Description"
        repoIName.Name = coliname
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)
        repoIName = Nothing

        Dim repoIName12 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIName12.FormatString = ""
        repoIName12.HeaderText = "Conversion Factor"
        repoIName12.Name = colConversionFactor
        repoIName12.Width = 150
        repoIName12.IsVisible = False
        repoIName12.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName12)
        repoIName12 = Nothing

        Dim repoIName1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIName1.FormatString = ""
        repoIName1.HeaderText = "Pack Size"
        repoIName1.Name = colPackSize
        repoIName1.Width = 60
        repoIName1.IsVisible = False
        repoIName1.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoIName1)
        repoIName1 = Nothing

        Dim repoIName11 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIName11.FormatString = ""
        repoIName11.HeaderText = "MasterPack Size"
        repoIName11.Name = colMasterPackSize
        repoIName11.Width = 60
        repoIName11.IsVisible = False
        repoIName11.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoIName11)
        repoIName11 = Nothing


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Item Type"
        repoPriceCode.Name = colItemType
        repoPriceCode.Width = 80
        repoPriceCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPriceCode)
        repoPriceCode = Nothing

        Dim repoPriceCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode1.FormatString = ""
        repoPriceCode1.HeaderText = "CSA Item Type"
        repoPriceCode1.Name = colCSAType
        repoPriceCode1.Width = 80
        repoPriceCode1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPriceCode1)
        repoPriceCode1 = Nothing

        Dim repoPriceCode11 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode11.FormatString = ""
        repoPriceCode11.HeaderText = "Item UOM"
        repoPriceCode11.Name = colItemUOM
        repoPriceCode11.Width = 80
        repoPriceCode11.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoPriceCode11.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoPriceCode11)
        repoPriceCode11 = Nothing

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Quantity"
        repoPendingQty.Name = colqty
        repoPendingQty.DecimalPlaces = 2
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoPendingQty)
        repoPendingQty = Nothing

        Dim repoPendingQty1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty1 = New GridViewDecimalColumn()
        repoPendingQty1.FormatString = ""
        repoPendingQty1.HeaderText = "Balance Quantity"
        repoPendingQty1.Name = colbalqty
        repoPendingQty1.DecimalPlaces = 2
        repoPendingQty1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQty1.IsVisible = True
        repoPendingQty1.ReadOnly = True
        repoPendingQty1.IsVisible = BookingEffectOnSale
        repoPendingQty1.VisibleInColumnChooser = BookingEffectOnSale
        gv.MasterTemplate.Columns.Add(repoPendingQty1)
        repoPendingQty1 = Nothing

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Alt. UOM"
        repoUnit.Name = colAltUOM
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        repoUnit.IsVisible = BookingEffectOnSale
        repoUnit.VisibleInColumnChooser = BookingEffectOnSale
        gv.MasterTemplate.Columns.Add(repoUnit)
        repoUnit = Nothing

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Alt. Quantity"
        repoQty.Name = colAltQty
        repoQty.Width = 80
        repoQty.ReadOnly = True
        repoQty.DecimalPlaces = 2
        repoQty.IsVisible = BookingEffectOnSale
        repoQty.VisibleInColumnChooser = BookingEffectOnSale
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoQty)
        repoQty = Nothing

        If BookingEffectOnSale Then
            Dim repotax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repotax.FormatString = ""
            repotax.Width = 60
            repotax.Name = colincludingtax
            repotax.HeaderText = "Including Tax"
            repotax.ReadOnly = True
            repotax.IsVisible = Not ForUDLOnly
            repotax.VisibleInColumnChooser = Not ForUDLOnly
            gv.MasterTemplate.Columns.Add(repotax)
            repotax = Nothing
        Else
            Dim repotax As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repotax.FormatString = ""
            repotax.Width = 60
            repotax.Name = colincludingtax
            repotax.HeaderText = "Including Tax"
            repotax.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'Yes' as Code,'Yes' as Name union all select 'No' as Code,'No' as Name")
            repotax.DisplayMember = "Name"
            repotax.ValueMember = "Code"
            repotax.IsVisible = Not ForUDLOnly
            repotax.VisibleInColumnChooser = Not ForUDLOnly
            gv.MasterTemplate.Columns.Add(repotax)
            repotax = Nothing
        End If

        Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Booking Rate"
        repoActualBalQty.Name = colbookingrate
        repoActualBalQty.Width = 80
        repoActualBalQty.ReadOnly = BookingEffectOnSale  'True''if setting is off then rate fill manual,otherwise auto come from booking
        repoActualBalQty.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoActualBalQty)
        repoActualBalQty = Nothing

        Dim repoItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "Unit Price"
        repoItemWt.Name = colUnitRate
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.ReadOnly = AllowRate_Readonly
        repoItemWt.DecimalPlaces = 2
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoItemWt)
        repoItemWt = Nothing

        ''======================for excise column------------------------
        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "MRP"
        repoItemWt.Name = colMRP
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.ReadOnly = True
        repoItemWt.WrapText = True
        repoItemWt.IsVisible = ExciseentryOnSale
        repoItemWt.VisibleInColumnChooser = ExciseentryOnSale
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoItemWt)

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colIsMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        repoIsMRPMandatory.WrapText = True
        repoIsMRPMandatory.VisibleInColumnChooser = ExciseentryOnSale
        repoIsMRPMandatory.IsVisible = ExciseentryOnSale
        gv.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "Abatement%"
        repoItemWt.Name = colAbatementPers
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.ReadOnly = True
        repoItemWt.WrapText = True
        repoItemWt.VisibleInColumnChooser = ExciseentryOnSale
        repoItemWt.IsVisible = ExciseentryOnSale
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoItemWt)

        repoItemWt = New GridViewDecimalColumn()
        repoItemWt.FormatString = ""
        repoItemWt.HeaderText = "Abatement Amount"
        repoItemWt.Name = colAbatementAmt
        repoItemWt.Width = 80
        repoItemWt.Minimum = 0
        repoItemWt.ReadOnly = True
        repoItemWt.WrapText = True
        repoItemWt.VisibleInColumnChooser = ExciseentryOnSale
        repoItemWt.IsVisible = ExciseentryOnSale
        repoItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoItemWt)
        ''=============================================================================================

        Dim repotaxbasis As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repotaxbasis.FormatString = ""
        repotaxbasis.Name = colTaxBasis
        repotaxbasis.HeaderText = "Tax Basis"
        repotaxbasis.DataSource = LoadComboBox()
        repotaxbasis.DisplayMember = "Name"
        repotaxbasis.ValueMember = "Code"
        repotaxbasis.Width = 80
        repotaxbasis.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repotaxbasis)
        repotaxbasis = Nothing

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Scheme Columns>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Dim repoIsSchmItem As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem.FormatString = ""
        repoIsSchmItem.HeaderText = "Scheme Applicable(Y/N)"
        repoIsSchmItem.Name = colIsSchmItem
        repoIsSchmItem.Width = 50
        repoIsSchmItem.DataSource = clsDBFuncationality.GetDataTable("select 'Y' as Code,'Y' as Name union all select 'N' as Code,'N' as Name")
        repoIsSchmItem.DisplayMember = "Name"
        repoIsSchmItem.ValueMember = "Code"
        'repoIsSchmItem.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem)
        repoIsSchmItem = Nothing

        Dim repoIsSchmItem13 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem13.FormatString = ""
        repoIsSchmItem13.HeaderText = "Scheme Type"
        repoIsSchmItem13.Name = colSchmCodeType
        repoIsSchmItem13.Width = 50
        repoIsSchmItem13.ReadOnly = False
        repoIsSchmItem13.DataSource = clsSchemeApplyOnDairy.GetSchemeTypes()
        repoIsSchmItem13.DisplayMember = "Code"
        repoIsSchmItem13.ValueMember = "Name"
        'repoIsSchmItem13.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem13)
        repoIsSchmItem13 = Nothing

        Dim repoIsSchmItem1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1.FormatString = ""
        repoIsSchmItem1.HeaderText = "Scheme Code"
        repoIsSchmItem1.Name = colSchmCode
        repoIsSchmItem1.Width = 50
        repoIsSchmItem1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1)
        repoIsSchmItem1 = Nothing

        Dim repoIsSchmItem2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem2.FormatString = ""
        repoIsSchmItem2.HeaderText = "Is FOC"
        repoIsSchmItem2.Name = colFOC
        repoIsSchmItem2.Width = 50
        repoIsSchmItem2.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem2)
        repoIsSchmItem2 = Nothing

        Dim repoIsSchmItem3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem3.FormatString = ""
        repoIsSchmItem3.HeaderText = "Main Item Code"
        repoIsSchmItem3.Name = colMainIcode
        repoIsSchmItem3.Width = 50
        repoIsSchmItem3.IsVisible = False
        repoIsSchmItem3.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem3)
        repoIsSchmItem3 = Nothing

        Dim repoIsSchmItem3LI As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem3LI.FormatString = ""
        repoIsSchmItem3LI.HeaderText = "Main Item Line No"
        repoIsSchmItem3LI.Name = colMainLineNo
        repoIsSchmItem3LI.Width = 50
        repoIsSchmItem3LI.IsVisible = False
        repoIsSchmItem3LI.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem3LI)
        repoIsSchmItem3LI = Nothing

        Dim repoIsSchmItem4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem4.FormatString = ""
        repoIsSchmItem4.HeaderText = "Main Item UOM"
        repoIsSchmItem4.Name = colMainIUOM
        repoIsSchmItem4.Width = 50
        repoIsSchmItem4.IsVisible = False
        repoIsSchmItem4.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem4)
        repoIsSchmItem4 = Nothing

        Dim repoIsSchmItem5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem5.FormatString = ""
        repoIsSchmItem5.HeaderText = "Main Item Qty"
        repoIsSchmItem5.Name = colMainIQty
        repoIsSchmItem5.Width = 50
        repoIsSchmItem5.IsVisible = False
        repoIsSchmItem5.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem5)
        repoIsSchmItem5 = Nothing

        Dim repoIsSchmItem1c As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c.FormatString = ""
        repoIsSchmItem1c.HeaderText = "Cash Scheme Code"
        repoIsSchmItem1c.Name = colCashSchemeCode
        repoIsSchmItem1c.Width = 50
        repoIsSchmItem1c.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c)
        repoIsSchmItem1c = Nothing

        Dim repoIsSchmItem1c1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c1.FormatString = ""
        repoIsSchmItem1c1.HeaderText = "Cash Scheme Type"
        repoIsSchmItem1c1.Name = colCashSchemeType
        repoIsSchmItem1c1.Width = 50
        repoIsSchmItem1c1.ReadOnly = True
        repoIsSchmItem1c1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c1)
        repoIsSchmItem1c1 = Nothing

        Dim repoIsSchmItem1c2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c2.FormatString = ""
        repoIsSchmItem1c2.HeaderText = "Cash %"
        repoIsSchmItem1c2.Name = colCash_Pers
        repoIsSchmItem1c2.Width = 50
        repoIsSchmItem1c2.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c2)
        repoIsSchmItem1c2 = Nothing

        Dim repoIsSchmItem1c3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c3.FormatString = ""
        repoIsSchmItem1c3.HeaderText = "Cash Amount"
        repoIsSchmItem1c3.Name = colCash_Amt
        repoIsSchmItem1c3.Width = 50
        repoIsSchmItem1c3.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c3)
        repoIsSchmItem1c3 = Nothing
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
        repoDisPer.DecimalPlaces = 2
        repoDisPer.IsVisible = False
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer)
        repoDisPer = Nothing

        Dim repoDisPer1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer1 = New GridViewDecimalColumn()
        repoDisPer1.FormatString = ""
        repoDisPer1.HeaderText = "Commission Rate"
        repoDisPer1.Minimum = 0
        repoDisPer1.Name = colCommision
        repoDisPer1.Width = 100
        repoDisPer1.DecimalPlaces = 2
        repoDisPer1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer1)
        repoDisPer1 = Nothing

        Dim repoDisPer1RS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDisPer1RS.FormatString = ""
        repoDisPer1RS.HeaderText = "Commission Type"
        repoDisPer1RS.Name = colComm_Type_RS_Pers
        repoDisPer1RS.Width = 100
        repoDisPer1RS.IsVisible = True
        repoDisPer1RS.ReadOnly = True
        repoDisPer1RS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer1RS)
        repoDisPer1RS = Nothing

        Dim repoDisPer2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer2.FormatString = ""
        repoDisPer2.HeaderText = "Commission Charges"
        repoDisPer2.Minimum = 0
        repoDisPer2.Name = colCommisionValue
        repoDisPer2.Width = 100
        repoDisPer2.DecimalPlaces = 2
        repoDisPer2.ReadOnly = True
        repoDisPer2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer2)
        repoDisPer2 = Nothing

        ''=======================freight=============================================
        repoDisPer1 = New GridViewDecimalColumn()
        repoDisPer1.FormatString = ""
        repoDisPer1.HeaderText = "Freight Rate"
        repoDisPer1.Minimum = 0
        repoDisPer1.Name = colFrghtRate
        repoDisPer1.Width = 100
        repoDisPer1.DecimalPlaces = 2
        repoDisPer1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisPer1.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDisPer1.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        gv.MasterTemplate.Columns.Add(repoDisPer1)
        repoDisPer1 = Nothing

        repoDisPer1RS = New GridViewTextBoxColumn()
        repoDisPer1RS.FormatString = ""
        repoDisPer1RS.HeaderText = "Freight Type"
        repoDisPer1RS.Name = colFrghtType
        repoDisPer1RS.Width = 100
        repoDisPer1RS.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDisPer1RS.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDisPer1RS.ReadOnly = True
        repoDisPer1RS.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer1RS)
        repoDisPer1RS = Nothing

        repoDisPer2 = New GridViewDecimalColumn()
        repoDisPer2.FormatString = ""
        repoDisPer2.HeaderText = "Freight Charges"
        repoDisPer2.Minimum = 0
        repoDisPer2.Name = colFrghtAmt
        repoDisPer2.Width = 100
        repoDisPer2.DecimalPlaces = 2
        repoDisPer2.ReadOnly = True
        repoDisPer2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisPer2.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDisPer2.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        gv.MasterTemplate.Columns.Add(repoDisPer2)
        repoDisPer2 = Nothing
        ''==============================================================================

        Dim repoDisPer11 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer11 = New GridViewDecimalColumn()
        repoDisPer11.FormatString = ""
        repoDisPer11.HeaderText = "Other Charges"
        repoDisPer11.Minimum = 0
        repoDisPer11.Name = colOtherCharge
        repoDisPer11.Width = 100
        repoDisPer11.DecimalPlaces = 2
        repoDisPer11.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDisPer11)
        repoDisPer11 = Nothing

        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax1)
        repoTax1 = Nothing

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt1)
        repoTaxBaseAmt1 = Nothing

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate1)
        repoTaxRate1 = Nothing

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt1)
        repoTaxAmt1 = Nothing

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax1)
        repoIsSurTax1 = Nothing

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode1)
        repoSurTaxCode1 = Nothing

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable1)
        repoIsTaxable1 = Nothing


        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable1)
        repoTaxRecoverable1 = Nothing

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax2)
        repoTax2 = Nothing

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt2)
        repoTaxBaseAmt2 = Nothing

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate2)
        repoTaxRate2 = Nothing

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt2)
        repoTaxAmt2 = Nothing

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax2)
        repoIsSurTax2 = Nothing

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode2)
        repoSurTaxCode2 = Nothing

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable2)
        repoIsTaxable2 = Nothing

        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable2)
        repoTaxRecoverable2 = Nothing

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax3)
        repoTax3 = Nothing

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt3)
        repoTaxBaseAmt3 = Nothing

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate3)
        repoTaxRate3 = Nothing

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt3)
        repoTaxAmt3 = Nothing

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax3)
        repoIsSurTax3 = Nothing

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode3)
        repoSurTaxCode3 = Nothing

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable3)
        repoIsTaxable3 = Nothing

        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable3)
        repoTaxRecoverable3 = Nothing

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax4)
        repoTax4 = Nothing

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt4)
        repoTaxBaseAmt4 = Nothing

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate4)
        repoTaxRate4 = Nothing

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt4)
        repoTaxAmt4 = Nothing

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax4)
        repoIsSurTax4 = Nothing

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode4)
        repoSurTaxCode4 = Nothing

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable4)
        repoIsTaxable4 = Nothing

        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable4)
        repoTaxRecoverable4 = Nothing

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax5)
        repoTax5 = Nothing

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt5)
        repoTaxBaseAmt5 = Nothing

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate5)
        repoTaxRate5 = Nothing

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt5.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt5)
        repoTaxAmt5 = Nothing

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax5)
        repoIsSurTax5 = Nothing

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode5)
        repoSurTaxCode5 = Nothing

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable5)
        repoIsTaxable5 = Nothing

        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable5)
        repoTaxRecoverable5 = Nothing

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax6)
        repoTax6 = Nothing

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt6)
        repoTaxBaseAmt6 = Nothing

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate6)
        repoTaxRate6 = Nothing

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt6)
        repoTaxAmt6 = Nothing

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax6)
        repoIsSurTax6 = Nothing

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode6)
        repoSurTaxCode6 = Nothing

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable6)
        repoIsTaxable6 = Nothing

        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable6)
        repoTaxRecoverable6 = Nothing

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax7)
        repoTax7 = Nothing

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt7)
        repoTaxBaseAmt7 = Nothing

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate7)
        repoTaxRate7 = Nothing

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt7)
        repoTaxAmt7 = Nothing

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax7)
        repoIsSurTax7 = Nothing

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode7)
        repoSurTaxCode7 = Nothing

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable7)
        repoIsTaxable7 = Nothing

        Dim repoTaxRecoverable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable7.HeaderText = "Recoverable Tax 7"
        repoTaxRecoverable7.Name = colTaxRecoverable7
        repoTaxRecoverable7.ReadOnly = True
        repoTaxRecoverable7.IsVisible = False
        repoTaxRecoverable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable7)
        repoTaxRecoverable7 = Nothing

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax8)
        repoTax8 = Nothing

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt8)
        repoTaxBaseAmt8 = Nothing

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate8)
        repoTaxRate8 = Nothing

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt8)
        repoTaxAmt8 = Nothing

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax8)
        repoIsSurTax8 = Nothing

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode8)
        repoSurTaxCode8 = Nothing

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable8)
        repoIsTaxable8 = Nothing

        Dim repoTaxRecoverable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable8.HeaderText = "Recoverable Tax 8"
        repoTaxRecoverable8.Name = colTaxRecoverable8
        repoTaxRecoverable8.ReadOnly = True
        repoTaxRecoverable8.IsVisible = False
        repoTaxRecoverable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable8)
        repoTaxRecoverable8 = Nothing

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax9)
        repoTax9 = Nothing

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt9)
        repoTaxBaseAmt9 = Nothing

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate9)
        repoTaxRate9 = Nothing

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt9)
        repoTaxAmt9 = Nothing

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax9)
        repoIsSurTax9 = Nothing

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode9)
        repoSurTaxCode9 = Nothing

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable9)
        repoIsTaxable9 = Nothing

        Dim repoTaxRecoverable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable9.HeaderText = "Recoverable Tax 9"
        repoTaxRecoverable9.Name = colTaxRecoverable9
        repoTaxRecoverable9.ReadOnly = True
        repoTaxRecoverable9.IsVisible = False
        repoTaxRecoverable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable9)
        repoTaxRecoverable9 = Nothing

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax10)
        repoTax10 = Nothing

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt10)
        repoTaxBaseAmt10 = Nothing

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate10)
        repoTaxRate10 = Nothing

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt10)
        repoTaxAmt10 = Nothing

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax10)
        repoIsSurTax10 = Nothing

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode10)
        repoSurTaxCode10 = Nothing

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable10)
        repoIsTaxable10 = Nothing

        Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable10.HeaderText = "Recoverable Tax 10"
        repoTaxRecoverable10.Name = colTaxRecoverable10
        repoTaxRecoverable10.ReadOnly = True
        repoTaxRecoverable10.IsVisible = False
        repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoTaxRecoverable10)
        repoTaxRecoverable10 = Nothing

        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.DecimalPlaces = 2
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        repoDisAmt.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoDisAmt)
        repoDisAmt = Nothing

        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Sale Rate"
        repoConv.Name = colSaleRate
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.DecimalPlaces = 2
        repoConv.ReadOnly = 2
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoConv)
        repoConv = Nothing

        Dim repoTotItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemWt = New GridViewDecimalColumn()
        repoTotItemWt.FormatString = ""
        repoTotItemWt.HeaderText = "Sale Value"
        repoTotItemWt.Name = colSaleValue
        repoTotItemWt.Width = 80
        repoTotItemWt.Minimum = 0
        repoTotItemWt.DecimalPlaces = 2
        repoTotItemWt.ReadOnly = True
        repoTotItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTotItemWt)
        repoTotItemWt = Nothing

        Dim repoMRP1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRP1.FormatString = ""
        repoMRP1.HeaderText = "Transfer Option"
        repoMRP1.Name = coldoubleclick
        repoMRP1.Width = 80
        repoMRP1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoMRP1)
        repoMRP1 = Nothing

        Dim repoKnockwithZeroValue As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoKnockwithZeroValue.HeaderText = "Set Transfer with zero value"
        repoKnockwithZeroValue.Name = colSetZeroValue
        repoKnockwithZeroValue.IsVisible = False
        repoKnockwithZeroValue.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoKnockwithZeroValue.ReadOnly = False
        repoKnockwithZeroValue.WrapText = True
        repoKnockwithZeroValue.VisibleInColumnChooser = SetZeroValue
        repoKnockwithZeroValue.IsVisible = SetZeroValue
        gv.MasterTemplate.Columns.Add(repoKnockwithZeroValue)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "Stock Transfer Rate"
        repoMRP.Name = colStckTransferrate
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        repoMRP.Minimum = 0
        repoMRP.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoMRP)
        repoMRP = Nothing

        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty.FormatString = ""
        repoFreeQty.HeaderText = "Stock Value"
        repoFreeQty.Name = colstckratevalue
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.ReadOnly = True
        repoFreeQty.DecimalPlaces = 2
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFreeQty)
        repoFreeQty = Nothing

        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 80
        repoAmtAfterDis.DecimalPlaces = 2
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.ReadOnly = True
        repoAmtAfterDis.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoAmtAfterDis)
        repoAmtAfterDis = Nothing

        Dim repoLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationCode.FormatString = ""
        repoLocationCode.HeaderText = "Remarks"
        repoLocationCode.Name = colRemarks
        repoLocationCode.MaxLength = 200
        repoLocationCode.Width = 100
        gv.MasterTemplate.Columns.Add(repoLocationCode)
        repoLocationCode = Nothing

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable1)
        repoIsExcisable1 = Nothing

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable2)
        repoIsExcisable2 = Nothing

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable3)
        repoIsExcisable3 = Nothing

        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable4)
        repoIsExcisable4 = Nothing

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable5)
        repoIsExcisable5 = Nothing

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable6)
        repoIsExcisable6 = Nothing

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable7)
        repoIsExcisable7 = Nothing

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable8)
        repoIsExcisable8 = Nothing

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable9)
        repoIsExcisable9 = Nothing

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable10)
        repoIsExcisable10 = Nothing

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.DecimalPlaces = 2
        repoTotTaxAmt.VisibleInColumnChooser = Not ForUDLOnly
        repoTotTaxAmt.IsVisible = Not ForUDLOnly
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTotTaxAmt)
        repoTotTaxAmt = Nothing

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        repoAmtAfterTax.DecimalPlaces = 2
        repoAmtAfterTax.IsVisible = Not ForUDLOnly
        repoAmtAfterTax.VisibleInColumnChooser = Not ForUDLOnly
        gv.MasterTemplate.Columns.Add(repoAmtAfterTax)
        repoAmtAfterTax = Nothing

        Dim repoAmtAfterTax1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax1.FormatString = ""
        repoAmtAfterTax1.HeaderText = "Gain/Loss Amount"
        repoAmtAfterTax1.Name = colGainLoss
        repoAmtAfterTax1.WrapText = True
        repoAmtAfterTax1.Width = 80
        repoAmtAfterTax1.DecimalPlaces = 2
        repoAmtAfterTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoAmtAfterTax1)
        repoAmtAfterTax1 = Nothing

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsBatchItem)



        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.AllowDeleteRow = True

        ReStoreGridLayout()
    End Sub

    Sub SetMultiCurrencyVisibility()

        Dim strq As String = ""
        '=======shivani
        Dim Currency As Integer = clsModuleCurrencyMapping.GetmulticurrencyDecimalPlaces()
        txtConversionRate.DecimalPlaces = clsCommon.myCdbl(Currency)
        '================
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True

            If clsCommon.myLen(Me.txtCode.Value) > 0 Then
                strq = "select currency_code from TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(Me.txtCode.Value) & "'"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
                ShowCurrencyDetail()
            End If
            ShowCurrencyDetail()
        Else
            pnlCurrConv.Visible = False
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
        repoTaxAuthCode = Nothing

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)
        repoTaxAuthName = Nothing

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)
        repoTaxBaseAmt = Nothing

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.ReadOnly = True
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)
        repoTaxRate = Nothing

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)
        repoTaxAmt = Nothing

        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = True
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
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
        repoACCode = Nothing

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colACName
        repoACName.Width = 300
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)
        repoACName = Nothing

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colACAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACAmt)
        repoACAmt = Nothing

        gvAC.AllowAddNewRow = False
        gvAC.ShowGroupPanel = False
        gvAC.AllowColumnReorder = True
        gvAC.AllowRowReorder = False
        gvAC.EnableSorting = False
        gvAC.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAC.MasterTemplate.ShowRowHeaderColumn = False
        gvAC.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub txtloc_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCSAloc_code._MYValidating
        Dim whrCls As String = ""

        whrCls = " tspl_location_master.location_code in (select To_Location_Code from TSPL_CSA_TRANSFER_HEAD where [status]=1 union all select CSA_Loc_Code from TSPL_SD_SALE_RETURN_HEAD where Trans_Type='CPR' and [status]=1)"
        txtCSAloc_code.Value = clsLocation.getFinder(whrCls, txtCSAloc_code.Value, isButtonClicked)

        If clsCommon.myLen(txtCSAloc_code.Value) > 0 Then
            txtCSAloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txtCSAloc_code.Value + "'"))
        Else
            txtCSAloc_code.Value = ""
            txtCSAloc_name.Text = ""
        End If

        SetTax()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gv.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
            frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(coliname).Value)
            frm.strLocationCode = clsCommon.myCstr(txt_loc_code.Value)
            frm.strCurrDocNo = clsCommon.myCstr(txtCode.Value)
            frm.strSplTransaction = "CSATransfer"
            frm.strCurrDocType = "CSA-SALE"
            frm.ArrTransferNo = TryCast(gv.CurrentRow.Cells(coldoubleclick).Tag, ArrayList)
            frm.strUOM = clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value)
            frm.dblMRP = 0 'clsCommon.myCdbl(gv.CurrentRow.Cells(colMRP).Value)
            frm.dblqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)

            frm.arr = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
            If RunBatchFifowise = 0 Then
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
                End If
            Else
                frm.OpenSerialList(0, "")
                gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
            End If
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try

            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(dtpdate.Value, Nothing) = False Then
                dtpdate.Select()
                Return False
            End If

            UpdateAllTotals()
            If clsCommon.myLen(txtcustcode.Value) <= 0 Then
                txtcustcode.Focus()
                txtcustcode.Select()
                Errorcontrol.SetError(txtcustName, "Select CSA Detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select CSA Detail.")
            Else
                Errorcontrol.ResetError(txtcustName)
            End If

            If clsCommon.myLen(txtCSAloc_code.Value) <= 0 Then
                txtCSAloc_code.Focus()
                txtCSAloc_code.Select()
                Errorcontrol.SetError(txtCSAloc_name, "Select CSA Location Detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select CSA Location Detail.")
            Else
                Errorcontrol.ResetError(txtCSAloc_name)
            End If

            If clsCommon.myLen(txt_loc_code.Value) <= 0 Then
                txt_loc_code.Focus()
                txt_loc_code.Select()
                Errorcontrol.SetError(txt_loc_name, "Select Location Detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select Location Detail.")
            Else
                Errorcontrol.ResetError(txt_loc_name)
            End If

            If AllowDistibutorSale AndAlso clsCommon.myLen(fndDistributorCode.Value) <= 0 Then
                fndDistributorCode.Focus()
                fndDistributorCode.Select()
                Errorcontrol.SetError(fndDistributorCode, "Select distributor detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select distributor detail.")
            Else
                Errorcontrol.ResetError(fndDistributorCode)
            End If

            If ExciseentryOnSale AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Invoice type.", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                cmbEXType.Select()
                cmbEXType.Focus()
                Errorcontrol.SetError(cmbEXType, "Select Invoice type.")
                Return False
            Else
                Errorcontrol.ResetError(cmbEXType)
            End If

            '==============================================================
            Dim excisableLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select excisable from TSPL_LOCATION_MASTER where Location_Code='" + clsCommon.myCstr(txt_loc_code.Value) + "'"))
            If ExciseentryOnSale AndAlso (clsCommon.CompairString(excisableLoc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal) Then
                Dim count As Integer = 0

                count += clsDBFuncationality.getSingleValue("select COUNT(*) from tspl_tax_group_master where Tax_group_Code='" + clsCommon.myCstr(txtTaxGroup.Value) + "' and isnull(Excisable,'N')='Y' and tax_group_type in ('S','T')")

                'If count <= 0 Then
                '    RadPageView1.SelectedPage = RadPageViewPage2
                '    txtTaxGroup.Focus()
                '    txtTaxGroup.Select()
                '    ErrorControl.SetError(lblTaxGrpName, "Select excisable tax autority.")
                '    Throw New Exception("Select excisable tax autority.")
                'Else
                '    ErrorControl.ResetError(lblTaxGrpName)
                'End If
            End If
            '===================================================================

            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                txtTaxGroup.Focus()
                txtTaxGroup.Select()
                Errorcontrol.SetError(lblTaxGrpName, "Select Tax Detail.")
                RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Select Tax Detail.")
            Else
                Errorcontrol.ResetError(lblTaxGrpName)
            End If

            Dim icode As String = ""
            Dim oldicode As String = ""

            Dim arrIcode As New List(Of String)
            arrIcode = New List(Of String)
            Dim MRP_ofItem As Decimal = 0

            For ii As Integer = 0 To gv.Rows.Count - 1
                icode = clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)
                MRP_ofItem = clsCommon.myCdbl(gv.Rows(ii).Cells(colMRP).Value)

                If clsCommon.myLen(icode) > 0 AndAlso Not arrIcode.Contains(icode) Then
                    arrIcode.Add(icode)
                End If

                If BookingEffectOnSale AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colBookingno).Value) > 0 AndAlso clsCommon.myLen(icode) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception("Select item detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                'If clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colStckTransferrate).Value) <= 0 Then 'clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "Y") <> CompairStringResult.Equal
                '    RadPageView1.SelectedPage = RadPageViewPage1
                '    Throw New Exception("Select transfer detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                'End If

                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Throw New Exception("Fill quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                ''===============MRP cond=================================
                If ExciseentryOnSale AndAlso clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colIsMRPMandatory).Value) AndAlso MRP_ofItem <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Fill MRP of item " + icode + "( " + clsCommon.myCstr(gv.Rows(ii).Cells(coliname).Value).Trim() + " ) at line no: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colMRP)
                    Return False
                End If
                ''============================================================

                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then
                    If BookingEffectOnSale AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colBookingno).Value) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Select booking detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If Not BookingEffectOnSale AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colbookingrate).Value) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colbookingrate)
                        Throw New Exception("Fill booking rate at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If ForUDLOnly AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colincludingtax).Value) <= 0 Then
                        gv.Rows(ii).Cells(colincludingtax).Value = "No"
                        gv.Rows(ii).Cells(colTaxBasis).Value = "Forward Calculation"
                    End If

                    If Not BookingEffectOnSale AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colincludingtax).Value) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colincludingtax)
                        Throw New Exception("Select including tax at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colSaleRate).Value) <= 0 And SetZeroValue = False Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Fill sale rate at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If (clsCommon.myCdbl(gv.Rows(ii).Cells(colStckTransferrate).Value) <= 0) And SetZeroValue = False Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Select transfer detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If


                    '===================check that qty exceed balance available of booking?
                    Dim balqty As Double = 0
                    Dim altqty As Double = 0
                    balqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colbalqty).Value)
                    altqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colAltQty).Value)

                    For jj As Integer = ii + 1 To gv.Rows.Count - 1
                        oldicode = clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)

                        If clsCommon.CompairString(oldicode, icode) = CompairStringResult.Equal Then 'AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colBookingno).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colBookingno).Value)) = CompairStringResult.Equal
                            'RadPageView1.SelectedPage = RadPageViewPage1
                            'Throw New Exception("Duplicate item not allowed,see row no. " + clsCommon.myCstr(jj + 1) + "")
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colBookingno).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colBookingno).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colCSAType).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colCSAType).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(oldicode, icode) = CompairStringResult.Equal Then
                            altqty = altqty + clsCommon.myCdbl(gv.Rows(jj).Cells(colAltQty).Value)
                        End If
                    Next

                    If BookingEffectOnSale AndAlso altqty > balqty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Total available balance of Booking no. [" + clsCommon.myCstr(gv.Rows(ii).Cells(colBookingno).Value) + "] is [" + clsCommon.myCstr(balqty) + "] and sale qty is [" + clsCommon.myCstr(altqty) + "]" + Environment.NewLine + " are invalid entry.")
                    End If
                End If

                If Not showBatchSkipAtCSAReturn Then '=======[If Batch skip then Batch dosen't work]
                 
                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value) > 0 AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colIsBatchItem).Value) Then
                        If RunBatchFifowise = 1 Then
                            gv.CurrentRow = gv.Rows(ii)
                            OpenBatchItem()
                        End If
                        Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv.Rows(ii).Cells(colitemcode).Tag, List(Of clsBatchInventory))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventory In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If tQty <> clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value) > 0 Then
                                Throw New Exception("Item : " + clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value) + " Entered Qty " + clsCommon.myCstr(clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    End If
                End If
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv.Focus()
                gv.Select()
                Throw New Exception("Fill atleast one row in item detail grid.")
            End If

            For ii As Integer = 0 To gvAC.Rows.Count - 1
                If clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0 Then
                    For jj As Integer = ii + 1 To gvAC.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colACCode).Value)) = CompairStringResult.Equal Then
                            RadPageView1.SelectedPage = RadPageViewPage4
                            common.clsCommon.MyMessageBoxShow(Me, "Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colACCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If
                    Next
                End If
            Next

            'If clsLocation.isLocatinExcisable(txt_loc_code.Value) Then
            '    Dim qry As String = "select Type  from TSPL_TAX_MASTER where Tax_Code ='" + clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value) + "' "
            '    If Not clsCommon.CompairString("E", clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) = CompairStringResult.Equal Then
            '        RadPageView1.SelectedPage = RadPageViewPage3
            '        common.clsCommon.MyMessageBoxShow("Tax should be excisable for excisable location")
            '        Return False
            '    End If
            'End If

            '--------------------Check Excisable Transfer-----------
            Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrIcode)
            If Not (intx = arrIcode.Count OrElse intx = 0) Then
                Throw New Exception("All item should be of Excisable or NonExcisable")
            End If
            If intx > 0 Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                    Throw New Exception("Please select tax group.")
                Else
                    If clsLocation.isLocatinExcisable(txt_loc_code.Value) = True Then
                        For Each grow As GridViewRowInfo In gv2.Rows
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colTTaxAutCode).Value + "'")), "Y") = CompairStringResult.Equal Then
                                Throw New Exception("Atleast One tax should be excisable.")
                            Else
                                Exit For
                            End If
                        Next
                    End If
                End If
                Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrIcode) + ")"))
            Else
                Item_TaxType = 0
            End If
            '------------------------------------------------------------------------------------------------------

            'Dim GSTStatus As Boolean = False
            'GSTStatus = clsERPFuncationality.GetGSTStatus(dtpdate.Value)
            'If GSTStatus Then
            '    If clsLocationWiseTax.IsValidTaxGroupForCSATransferSalePatti(txtTaxGroup.Value, txt_loc_code.Value, txtcustcode.Value, "S", dtpdate.Value, False, Nothing) = False Then
            '        Return False
            '    End If
            'End If

            RefreshSerialNo()

            For Each grow As GridViewRowInfo In gv.Rows
                UpdateCurrentRow(grow.Index)
            Next
            UpdateAllTotals()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsCSASaleInvoice()
        Dim objtr As New clsCSASaleInvoiceItem()
        Dim totalqty As Decimal = 0
        Dim objApproval As New clsApply_Approval()
        If AllowNLevel Then
            If Not AllowModifcationByApprovalUser Then
                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(txtCode.Value))
            End If
        End If
        Try
            'clsCommon.ProgressBarShow()

            obj.Arr_Item = New List(Of clsCSASaleInvoiceItem)()
            objtr.GV_TAG_ARR = New List(Of clsCSAStockTransferDetail)

            obj.docno = clsCommon.myCstr(txtCode.Value)
            obj.docdate = clsCommon.myCDate(dtpdate.Text)
            obj.Is_Approved = 0
            obj.descrptn = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")

            If AllowDistibutorSale Then
                obj.CSA_Distributor_Code = clsCommon.myCstr(txtcustcode.Value)
                obj.cust_code = clsCommon.myCstr(fndDistributorCode.Value)
            Else
                obj.cust_code = clsCommon.myCstr(txtcustcode.Value)
                obj.CSA_Distributor_Code = ""
            End If

            obj.loc_code = clsCommon.myCstr(txtCSAloc_code.Value)
            obj.po_no = clsCommon.myCstr(txtPONo.Text)
            obj.document_amt = clsCommon.myCdbl(lblTotRAmt1.Text)
            obj.plant_loc_code = clsCommon.myCstr(txt_loc_code.Value)
            obj.total_commision = clsCommon.myCdbl(txttotal_comm_amt.Text)
            obj.Total_Freight_Amt = clsCommon.myCdbl(txtTotalFreightAmt.Text)
            'obj.CSA_FOC_STATUS = IIf(ChkFOC.Checked = True, 1, 0)

            obj.amt_with_disc = clsCommon.myCstr(lblAmtWithDiscount.Text)
            obj.disc_on_rate = clsCommon.myCstr(IIf(chkDiscountOnRate.IsChecked = True, "1", "0"))
            obj.disc_on_amt = clsCommon.myCstr(IIf(chkDiscountOnAmt.IsChecked = True, "1", "0"))
            obj.disc_pers = clsCommon.myCdbl(txtDiscPer.Text)

            obj.Excisable = clsCommon.myCstr(cmbEXType.SelectedValue)

            If obj.disc_pers > 0 Then
                obj.inv_disc_amt = clsCommon.myCdbl(lblInvoiceDiscAmt.Text)
                obj.disc_amt = 0
            Else
                obj.inv_disc_amt = 0
                obj.disc_amt = clsCommon.myCdbl(lblInvoiceDiscAmt.Text)
                obj.inv_disc_amt = 0
            End If

            obj.lbldisc_amt = clsCommon.myCstr(lblDiscountAmt.Text)
            obj.amt_after_disc = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            obj.lbltaxamt = clsCommon.myCdbl(lblTaxAmt.Text)
            obj.total_add_chrg = clsCommon.myCdbl(lblAddCharges1.Text)
            obj.tax_group_code = clsCommon.myCstr(txtTaxGroup.Value)
            If rbtnTaxCalAutomatic.IsChecked Then
                obj.Tax_Calculation_Type = "0"
            Else
                obj.Tax_Calculation_Type = "1"
            End If

            obj.term_code = clsCommon.myCstr(txtTermCode.Value)
            obj.due_date = clsCommon.myCDate(txtDueDate.Text)

            obj.RoundOffAmount = clsCommon.myCdbl(txtRoundOff.Text)
            '=================Tax--======================================
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
            '======================Additional=======================================

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

            If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
                obj.currency_code = clsCommon.myCstr(txtCurrencyCode.Value)
                obj.cnvrsn_rate = clsCommon.myCdbl(txtConversionRate.Text)
                obj.applicable_from = clsCommon.myCstr(txtApplicableFrom.Text)
            Else
                obj.currency_code = Nothing
                obj.cnvrsn_rate = 1
                obj.applicable_from = Nothing
            End If

            For Each grow As GridViewRowInfo In gv.Rows
                objtr = New clsCSASaleInvoiceItem()
                objtr.GV_TAG_ARR = New List(Of clsCSAStockTransferDetail)
                objtr.arrBatchItem = New List(Of clsBatchInventory)

                objtr.Line_No = clsCommon.myCdbl(grow.Cells(colLinenno).Value)
                objtr.commision = clsCommon.myCdbl(grow.Cells(colCommision).Value)
                objtr.CSA_Commission_RS_PERS = clsCommon.myCstr(grow.Cells(colComm_Type_RS_Pers).Value)
                objtr.Other_Chrage = clsCommon.myCdbl(grow.Cells(colOtherCharge).Value)
                objtr.Pack_Size = clsCommon.myCdbl(grow.Cells(colPackSize).Value)
                objtr.Master_Pack_Size = clsCommon.myCdbl(grow.Cells(colMasterPackSize).Value)

                objtr.Freight_Amt = clsCommon.myCdbl(grow.Cells(colFrghtAmt).Value)
                objtr.Freight_Rate = clsCommon.myCdbl(grow.Cells(colFrghtRate).Value)
                objtr.Freight_Type = clsCommon.myCstr(grow.Cells(colFrghtType).Value)


                If clsCommon.myLen(grow.Cells(coldate).Value) <= 0 Then
                    objtr.Grid_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(Nothing))
                Else
                    objtr.Grid_Date = clsCommon.myCDate(grow.Cells(coldate).Value)
                End If

                objtr.Booking_no = clsCommon.myCstr(grow.Cells(colBookingno).Value)
                objtr.Booking_type = clsCommon.myCstr(grow.Cells(colbookingtype).Value)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.Unit_code = clsCommon.myCstr(grow.Cells(colItemUOM).Value)
                objtr.Qty = clsCommon.myCdbl(grow.Cells(colqty).Value)
                objtr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colbalqty).Value)
                objtr.alt_qty = clsCommon.myCdbl(grow.Cells(colAltQty).Value)
                objtr.colcommsn_amt = clsCommon.myCdbl(grow.Cells(colCommisionValue).Value)
                objtr.Conv_Factor = clsCommon.myCdbl(grow.Cells(colConversionFactor).Value)
                objtr.alt_uom = clsCommon.myCstr(grow.Cells(colAltUOM).Value)
                objtr.booking_rate = clsCommon.myCdbl(grow.Cells(colbookingrate).Value)
                objtr.Item_Cost = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                objtr.including_tax = clsCommon.myCstr(grow.Cells(colincludingtax).Value)

                objtr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                objtr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                objtr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                objtr.sale_rate = clsCommon.myCdbl(grow.Cells(colSaleRate).Value)
                objtr.Amount = clsCommon.myCdbl(grow.Cells(colSaleValue).Value)

                objtr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                objtr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                objtr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                objtr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                objtr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                objtr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                objtr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                objtr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                objtr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                objtr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                objtr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                objtr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                objtr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                objtr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                objtr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                objtr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                objtr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                objtr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                objtr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                objtr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                objtr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                objtr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                objtr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                objtr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                objtr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                objtr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                objtr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                objtr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                objtr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                objtr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                objtr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                objtr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                objtr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                objtr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                objtr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                objtr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                objtr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                objtr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                objtr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                objtr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)

                objtr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                objtr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                objtr.Location = clsCommon.myCstr(txtCSAloc_code.Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                objtr.Item_Tax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                objtr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                objtr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                objtr.ActualRate = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)

                objtr.tax_basis = clsCommon.myCstr(grow.Cells(colTaxBasis).Value) 'back cal./forward cal.
                objtr.stck_trans_rate = clsCommon.myCdbl(grow.Cells(colStckTransferrate).Value)
                objtr.stck_trans_value = clsCommon.myCdbl(grow.Cells(colstckratevalue).Value)
                objtr.GainLoss = clsCommon.myCdbl(grow.Cells(colGainLoss).Value)

                If objtr.Disc_Per > 0 Then
                    objtr.HeadDiscPerAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objtr.HeadDiscAmt = 0
                Else
                    obj.inv_disc_amt = 0
                    objtr.HeadDiscAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objtr.HeadDiscPerAmt = 0
                End If

                objtr.Scheme_Item = clsCommon.myCstr(grow.Cells(colFOC).Value)
                objtr.Scheme_Applicable = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
                objtr.Scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCode).Value)
                objtr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                objtr.Scheme_Item_Line_No = clsCommon.myCstr(grow.Cells(colMainLineNo).Value)
                If clsCommon.myLen(objtr.Scheme_Item_Line_No) <= 0 Then
                    objtr.Scheme_Item_Line_No = "0"
                End If
                objtr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                objtr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                objtr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                objtr.FOC_Item = clsCommon.myCstr(IIf(clsCommon.myCstr(grow.Cells(colFOC).Value) = "N", "0", "1"))
                objtr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                objtr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                objtr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                objtr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)


                ''=============================================
                objtr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                objtr.Is_MRP_Mandatory = CInt(clsCommon.myCdbl(IIf(clsCommon.myCBool(grow.Cells(colIsMRPMandatory).Value) = True, 1, 0)))
                objtr.SetTransferQtytoZero = CInt(clsCommon.myCdbl(IIf(clsCommon.myCBool(grow.Cells(colSetZeroValue).Value) = True, 1, 0)))
                objtr.Abatement_Per = clsCommon.myCdbl(grow.Cells(colAbatementPers).Value)
                objtr.Abatement_Amt = clsCommon.myCdbl(grow.Cells(colAbatementAmt).Value)
                ''=============================================

                objtr.GV_TAG_ARR = TryCast(grow.Tag, List(Of clsCSAStockTransferDetail))
                objtr.arrBatchItem = TryCast(grow.Cells(colitemcode).Tag, List(Of clsBatchInventory))

                'done by stuti against approval work
                If clsCommon.myCdbl(grow.Cells(colbookingrate).Value) <> clsCommon.myCdbl(grow.Cells(colUnitRate).Value) Then
                    objApproval.Item_Rate = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                    objApproval.Comparision_Rate = clsCommon.myCdbl(grow.Cells(colbookingrate).Value)
                End If
                '=========end here===============

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr_Item.Add(objtr)
                    If AllowNLevel Then
                        totalqty += clsCommon.myCdbl(grow.Cells(colqty).Value)
                    End If
                End If
            Next


            If clsCSASaleInvoice.SaveData(isNewEntry, obj, isPost) Then
                'clsCommon.ProgressBarHide()
                If Not isPost Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully.", Me.Text)
                    End If
                End If

                txtCode.Value = obj.docno
                UcAttachment1.SaveData(txtCode.Value)

                ''done by stuti approval work 06/12/2016
                If AllowNLevel Then
                    clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(obj.docno), dtpdate.Text, clsCommon.myCstr(txtDesc.Text), "", clsCommon.myCdbl(lblTotRAmt1.Text), clsCommon.myCdbl(totalqty), "", objApproval)
                End If

                LoadData(txtCode.Value, NavigatorType.Current)
            End If
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim objApproval As New clsApply_Approval()
        Dim obj As clsCSASaleInvoice = New clsCSASaleInvoice()
        Try
            'clsCommon.ProgressBarShow()
            Dim schemeAlready_Applied As Boolean = False ''use in case of UDL,sheet imported and scheme applied when loaddata

            ChkFOC.Enabled = True
            ChkFOC.Checked = False
            RadPageViewPage6.Item.Visibility = ElementVisibility.Collapsed
            btnexcel.Visible = False
            gv.Rows.Clear()
            gv.Rows.AddNew()
            gv2.Rows.Clear()
            gvAC.Rows.Clear()

            isNewEntry = True
            isInsideLoadData = True

            Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
                clsDBFuncationality.ExecuteNonQuery(qry)
            Else
                qry = "delete from CSA_SALE_TRANSFER"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If


            obj = clsCSASaleInvoice.GetData(strCode, arrLoc, NavType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                isNewEntry = False

                'If obj.CSA_FOC_STATUS > 0 Then
                '    ChkFOC.Enabled = False
                'Else
                '    ChkFOC.Enabled = True
                'End If
                'ChkFOC.Checked = clsCommon.myCBool(IIf(obj.CSA_FOC_STATUS = 1, True, False))
                txtCode.Value = obj.docno
                dtpdate.Text = obj.docdate

                If AllowDistibutorSale Then
                    txtcustcode.Value = obj.CSA_Distributor_Code
                    txtcustName.Text = obj.Distributor_Name
                    fndDistributorCode.Value = obj.cust_code
                    txtDistributor_Name.Text = obj.cust_name
                Else
                    txtcustcode.Value = obj.cust_code
                    txtcustName.Text = obj.cust_name
                    fndDistributorCode.Value = ""
                    txtDistributor_Name.Text = ""
                End If

                cmbEXType.SelectedValue = obj.Excisable
                If clsCommon.myLen(obj.Excisable) > 0 Then
                    cmbEXType.Enabled = False
                Else
                    cmbEXType.SelectedValue = "N"
                    cmbEXType.Enabled = False
                End If

                txtCSAloc_code.Value = obj.loc_code
                txtCSAloc_name.Text = obj.loc_name
                txtPONo.Text = obj.po_no
                lblTotRAmt1.Text = obj.document_amt
                txtDesc.Text = obj.descrptn
                txtTaxGroup.Value = obj.tax_group_code
                lblTaxGrpName.Text = obj.tax_group_name
                txtTermCode.Value = obj.term_code
                lblTermName.Text = obj.term_desc
                txtDueDate.Text = obj.due_date
                txt_loc_code.Value = obj.plant_loc_code
                txt_loc_name.Text = obj.plant_loc_name
                txttotal_comm_amt.Text = obj.total_commision
                txtTotalFreightAmt.Text = obj.Total_Freight_Amt

                lblAmtWithDiscount.Text = obj.amt_with_disc
                lblDiscountAmt.Text = obj.lbldisc_amt
                lblAmtAfterDiscount.Text = obj.amt_after_disc
                lblTaxAmt.Text = obj.lbltaxamt
                lblTotRAmt.Text = obj.document_amt

                txtRoundOff.Text = obj.RoundOffAmount

                txtDiscPer.Text = obj.disc_pers
                txtDiscAmt.Text = obj.disc_amt
                If clsCommon.myLen(txtDiscAmt.Text) <= 0 OrElse clsCommon.myLen(txtDiscPer.Text) <= 0 OrElse clsCommon.myCdbl(txtDiscAmt.Text) = 0 OrElse clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                    txtDiscPer.Text = obj.disc_pers
                    If clsCommon.myCdbl(txtDiscPer.Text) = 0 Then
                        txtDiscAmt.Text = obj.disc_amt
                        chkDiscountOnAmt.IsChecked = True
                        lblInvoiceDiscAmt.Text = obj.disc_amt
                    Else
                        chkDiscountOnRate.IsChecked = True
                        lblInvoiceDiscAmt.Text = obj.inv_disc_amt
                    End If
                End If

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.tax_group_code)

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

                lblAddCharges.Text = obj.total_add_chrg
                lblAddCharges1.Text = obj.total_add_chrg
                If obj.Tax_Calculation_Type = "1" Then
                    rbtnTaxCalManual.IsChecked = True
                Else
                    rbtnTaxCalAutomatic.IsChecked = True
                End If

                If obj.Arr_Item IsNot Nothing AndAlso obj.Arr_Item.Count > 0 Then
                    ''=================================================================
                    schemeAlready_Applied = False
                    If ForUDLOnly Then
                        For Each objtr As clsCSASaleInvoiceItem In obj.Arr_Item
                            If clsCommon.CompairString(objtr.Scheme_Applicable, "Y") = CompairStringResult.Equal Then
                                schemeAlready_Applied = True
                                Exit For
                            End If
                        Next
                    End If
                    ''========================================================================

                    For Each objtr As clsCSASaleInvoiceItem In obj.Arr_Item
                        gv.Rows(gv.Rows.Count - 1).Cells(colLinenno).Value = clsCommon.myCstr(objtr.Line_No)
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommision).Value = objtr.commision
                        gv.Rows(gv.Rows.Count - 1).Cells(colComm_Type_RS_Pers).Value = objtr.CSA_Commission_RS_PERS
                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherCharge).Value = objtr.Other_Chrage
                        gv.Rows(gv.Rows.Count - 1).Cells(colPackSize).Value = objtr.Pack_Size
                        gv.Rows(gv.Rows.Count - 1).Cells(colMasterPackSize).Value = objtr.Master_Pack_Size
                        gv.Rows(gv.Rows.Count - 1).Cells(coldate).Value = objtr.Grid_Date
                        gv.Rows(gv.Rows.Count - 1).Cells(colBookingno).Value = objtr.Booking_no
                        gv.Rows(gv.Rows.Count - 1).Cells(colbookingtype).Value = objtr.Booking_type
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.Item_Code
                        ' gv.Rows(gv.Rows.Count - 1).Cells(colMasterPackSize).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.description from (select TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Master_Value='1' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + objtr.Item_Code + "')a"))
                        gv.Rows(gv.Rows.Count - 1).Cells(coliname).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objtr.Item_Code, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = LoadItemType(objtr.Item_type)
                        gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr.csa_type
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemUOM).Value = objtr.Unit_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colqty).Value = objtr.Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colbalqty).Value = objtr.Balance_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colAltQty).Value = objtr.alt_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colAltUOM).Value = objtr.alt_uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colbookingrate).Value = objtr.booking_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnitRate).Value = objtr.Item_Cost
                        gv.Rows(gv.Rows.Count - 1).Cells(colincludingtax).Value = objtr.including_tax
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommisionValue).Value = objtr.colcommsn_amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colConversionFactor).Value = objtr.Conv_Factor

                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtAmt).Value = objtr.Freight_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtRate).Value = objtr.Freight_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtType).Value = objtr.Freight_Type

                        gv.Rows(gv.Rows.Count - 1).Cells(colDisPer).Value = objtr.Disc_Per
                        gv.Rows(gv.Rows.Count - 1).Cells(colDisAmt).Value = objtr.Disc_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmtAfterDis).Value = objtr.Amt_Less_Discount
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleRate).Value = objtr.sale_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleValue).Value = objtr.Amount

                        gv.Rows(gv.Rows.Count - 1).Cells(colTax1).Value = objtr.TAX1
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objtr.TAX1_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate1).Value = objtr.TAX1_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt1).Value = objtr.TAX1_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax2).Value = objtr.TAX2
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objtr.TAX2_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate2).Value = objtr.TAX2_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt2).Value = objtr.TAX2_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax3).Value = objtr.TAX3
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objtr.TAX3_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate3).Value = objtr.TAX3_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt3).Value = objtr.TAX3_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax4).Value = objtr.TAX4
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objtr.TAX4_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate4).Value = objtr.TAX4_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt4).Value = objtr.TAX4_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax5).Value = objtr.TAX5
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objtr.TAX5_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate5).Value = objtr.TAX5_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt5).Value = objtr.TAX5_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax6).Value = objtr.TAX6
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objtr.TAX6_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate6).Value = objtr.TAX6_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt6).Value = objtr.TAX6_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax7).Value = objtr.TAX7
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objtr.TAX7_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate7).Value = objtr.TAX7_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt7).Value = objtr.TAX7_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax8).Value = objtr.TAX8
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objtr.TAX8_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate8).Value = objtr.TAX8_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt8).Value = objtr.TAX8_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax9).Value = objtr.TAX9
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objtr.TAX9_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate9).Value = objtr.TAX9_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt9).Value = objtr.TAX9_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax10).Value = objtr.TAX10
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objtr.TAX10_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate10).Value = objtr.TAX10_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt10).Value = objtr.TAX10_Amt

                        gv.Rows(gv.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Total_Tax_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmtAfterTax).Value = objtr.Item_Net_Amt

                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Item_Tax
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmtAfterTax).Value = objtr.Total_Basic_Amt


                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBasis).Value = objtr.tax_basis
                        gv.Rows(gv.Rows.Count - 1).Cells(colStckTransferrate).Value = objtr.stck_trans_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colstckratevalue).Value = objtr.stck_trans_value
                        gv.Rows(gv.Rows.Count - 1).Cells(colGainLoss).Value = objtr.GainLoss


                        If obj.isPost = 0 Then
                            If clsCommon.myLen(obj.TAX1) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable1).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX1)
                            End If
                            If clsCommon.myLen(obj.TAX2) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable2).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX2)
                            End If
                            If clsCommon.myLen(obj.TAX3) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable3).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX3)
                            End If
                            If clsCommon.myLen(obj.TAX4) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable4).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX4)
                            End If
                            If clsCommon.myLen(obj.TAX5) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable5).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX5)
                            End If
                            If clsCommon.myLen(obj.TAX6) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable6).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX6)
                            End If
                            If clsCommon.myLen(obj.TAX7) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable7).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX7)
                            End If
                            If clsCommon.myLen(obj.TAX8) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable8).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX8)
                            End If
                            If clsCommon.myLen(obj.TAX9) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable9).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX9)
                            End If
                            If clsCommon.myLen(obj.TAX10) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(colTaxRecoverable10).Value = clsTaxMaster.IsTaxRecoverableAC(obj.TAX10)
                            End If
                        End If

                        gv.Rows(gv.Rows.Count - 1).Cells(coldoubleclick).Value = "Double Click"

                        If clsCommon.CompairString(objtr.Booking_type, "Item-Wise") = CompairStringResult.Equal Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).ReadOnly = True
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).ReadOnly = False
                        End If

                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.Scheme_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = objtr.Scheme_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = objtr.Scheme_Item_UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = objtr.Scheme_Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainLineNo).Value = objtr.Scheme_Item_Line_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCode).Value = objtr.Scheme_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = objtr.Scheme_Applicable
                        gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = objtr.Scheme_Item
                        gv.Rows(gv.Rows.Count - 1).Cells(colCash_Amt).Value = objtr.Cash_Scheme_Amount
                        gv.Rows(gv.Rows.Count - 1).Cells(colCash_Pers).Value = objtr.Cash_Scheme_Pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeCode).Value = objtr.Cash_Scheme_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeType).Value = objtr.Cash_Scheme_Type

                        If clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                            gv.Rows(gv.Rows.Count - 1).Cells(coldate).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colbookingtype).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colBookingno).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colMasterPackSize).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colPackSize).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colItemUOM).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colqty).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colincludingtax).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colUnitRate).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colCommision).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colCommisionValue).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colFrghtRate).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colFrghtAmt).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colOtherCharge).ReadOnly = True
                        End If

                        gv.Rows(gv.Rows.Count - 1).Cells(colMRP).Value = objtr.MRP
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value), Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colSetZeroValue).Value = IIf(objtr.SetTransferQtytoZero = 1, True, False)
                        gv.Rows(gv.Rows.Count - 1).Cells(colAbatementPers).Value = objtr.Abatement_Per
                        gv.Rows(gv.Rows.Count - 1).Cells(colAbatementAmt).Value = objtr.Abatement_Amt


                        gv.Rows(gv.Rows.Count - 1).Tag = objtr.GV_TAG_ARR

                        ''=====================================================================
                        Dim ArrTransferNo_For_Batch As New ArrayList()
                        If objtr.GV_TAG_ARR IsNot Nothing AndAlso TryCast(objtr.GV_TAG_ARR, List(Of clsCSAStockTransferDetail)) IsNot Nothing Then
                            For Each objN As clsCSAStockTransferDetail In objtr.GV_TAG_ARR
                                If Not ArrTransferNo_For_Batch.Contains(objN.transcode) Then
                                    ArrTransferNo_For_Batch.Add(objN.transcode)
                                End If
                            Next
                        End If
                        gv.CurrentRow.Cells(coldoubleclick).Tag = ArrTransferNo_For_Batch
                        ''=====================================================================

                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Tag = objtr.arrBatchItem

                        ''=============================================================================
                        If ForUDLOnly AndAlso schemeAlready_Applied = False Then
                            gv.CurrentRow = gv.Rows(gv.Rows.Count - 1)
                            FillFreeItemsInGrid()
                            isInsideLoadData = True ''becuase it get reset at stocktransferknock-off
                        End If
                        ''======================================================================
                        'done by stuti against approval work n 06/12/2016
                        If objtr.Item_Cost <> objtr.booking_rate Then
                            objApproval.Item_Rate = objtr.Item_Cost
                            objApproval.Comparision_Rate = objtr.booking_rate
                        End If
                        '===========end here=============================

                        gv.Rows.AddNew()
                    Next
                End If

                '===============transfer data=======================
                clsDBFuncationality.ExecuteNonQuery("delete from csa_sale_transfer")
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + txtCode.Value + "'")) > 0 Then
                    qry = "insert into CSA_SALE_TRANSFER (DOCUMENT_CODE,Line_No,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Conv_Factor,Alt_Qty,FOC,Transfer_Line_No) select DOCUMENT_CODE,Line_No,Against_Transfer_Code,item_code,Item_Pack_Size,qty,transfer_rate,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Conv_Factor,Alt_Qty,FOC,Transfer_Line_No from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
                '================================================================

                SetitemWiseTaxOnlySetting()

                txtCurrencyCode.Value = obj.currency_code
                txtConversionRate.Text = obj.cnvrsn_rate
                txtApplicableFrom.Text = obj.applicable_from

                UcAttachment1.LoadData(txtCode.Value)

                txtCode.MyReadOnly = True
                txtCSAloc_code.Enabled = False
                txtcustcode.Enabled = False
                txt_loc_code.Enabled = False
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True
                btnsave.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending

                'If obj.Is_Approved = 0 Then
                '    btnpost.Enabled = False
                'End If
                If obj.isPost = 1 Then
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    btnsave.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
            Else
                FunReset()
            End If
            '=====================if document go for approval then no post button visible or if document contain related setting
            If AllowNLevel Then
                btnpost.Visible = MyBase.isPostFlag
                If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(txtCode.Value), clsCommon.myCdbl(lblTotRAmt1.Text), 0, "", objApproval) Then
                    btnpost.Visible = False
                    If UsLock1.Status = ERPTransactionStatus.Pending Then
                        UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(txtCode.Value), Nothing)
                    End If
                End If
            End If
            '============================================
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try

    End Sub

    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtcustcode.Value, txt_loc_code.Value, OpenALLTaxes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(intRowNo).Cells(colitemcode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        If ii = 1 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode1).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 2 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode2).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 3 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode3).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 4 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode4).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 5 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode5).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 6 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode6).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 7 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode7).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 8 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode8).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 9 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode9).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 10 Then
                            gv.Rows(intRowNo).Cells(colIsTaxable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colIsSurTax10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.Rows(intRowNo).Cells(colSurTaxCode10).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.Rows(intRowNo).Cells(colIsExcisable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        End If

                        ii = ii + 1
                    Next
                End If
            Next
        End If

        dt = Nothing
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document code for deletion.")
                Throw New Exception("Select document code for deletion.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not (myMessages.deleteConfirm()) Then
                Return
            End If

            'done by stuti on 1/12/2016 for N-LevelApproval
            If AllowNLevel Then
                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(txtCode.Value))
            End If
            '===========================================================

            'clsCommon.ProgressBarShow()
            If clsCSASaleInvoice.DeleteData(txtCode.Value) Then
                'clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                FunReset()
            End If
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document code for posting.")
                Throw New Exception("Select document code for posting.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not (myMessages.postConfirm()) Then
                Return
            End If

            '============check for approval status--=================
            If Not AllowNLevel Then
                Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where document_no='" + txtCode.Value + "' and isnull(Approve,0)<>1")
                If check > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Document required Approval,before post." + Environment.NewLine + "(for this goto --> Transaction Approval)")
                    FunReset() ''because on transaction,double click window open that cause on closing of screen child table droped,and here on current screen document error on posting
                    Exit Sub
                End If
            End If

            If AllowToSave() Then
                SaveData(True)
                'clsCommon.ProgressBarShow()
                If clsCSASaleInvoice.PostData(Me.Form_ID, arrLoc, txtCode.Value) Then
                    'clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Post Successfully", Me.Text)

                    LoadData(txtCode.Value, NavigatorType.Current)
                    'If clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                    '    SMSSENDONLY(True)
                    'End If
                End If
                'clsCommon.ProgressBarHide()
            End If

        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub DropTable()
        Dim qry As String = "select count(*) from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='CSA_SALE_TRANSFER'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "drop table CSA_SALE_TRANSFER"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        DropTable()
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select count(*) from TSPL_SD_SALE_INVOICE_HEAD where document_code='" + txtCode.Value + "' and trans_type='CSA'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
            End If

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                txtCode.Value = clsCSASaleInvoice.GetFinder("", txtCode.Value, isButtonClicked, arrLoc)

                If clsCommon.myLen(txtCode.Value) > 0 Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                Else
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Sub ShowCurrencyDetail()
        ' Dim strq As String
        Dim dt As DataTable
        If clsCommon.myLen(clsCommon.myCstr(Me.txtcustcode.Value)) = 0 Then
            Exit Sub
        End If

        If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
            dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.dtpdate.Value, txtCurrencyCode.Value)
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

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        'Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("Shipmentfndid", qry, "Code", "Tax_Group_Type='S'", txtTaxGroup.Value, "Code", isButtonClicked)
        Try
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txt_loc_code.Value, txtcustcode.Value, "S", txtTaxGroup.Value, isButtonClicked, OpenALLTaxes)
            lblTaxGrpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + txtTaxGroup.Value + "' and Tax_Group_Type='S'"))
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtcustcode.Value, txt_loc_code.Value, OpenALLTaxes)
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
            For ii As Integer = 0 To gv.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        isInsideLoadData = True
        For ii As Integer = 0 To gv.Rows.Count - 1
            UpdateCurrentRow(ii)
            CommisionValue(ii)
        Next

        isInsideLoadData = False
        UpdateAllTotals()
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotalFreightAmt As Double = 0
        Dim dblTotLandedCost As Double = 0
        Dim dblCashDisAmt As Double = 0
        Dim dblHeadDisAmt As Double = 0
        Dim total_commision As Decimal = Nothing
        Dim dblACAmount As Double = 0

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

        Dim is_manditax As String = ""



        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv.Rows.Count - 1
            If (clsCommon.myLen(gv.Rows(ii).Cells(colitemcode).Value) > 0) Then ' AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(ColFOC).Value) = 0

                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colSaleValue).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv.Rows(ii).Cells(colAmtAfterDis).Value)
                total_commision = total_commision + clsCommon.myCdbl(gv.Rows(ii).Cells(colCommisionValue).Value)
                dblTotalFreightAmt = dblTotalFreightAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colFrghtAmt).Value)
                dblACAmount = dblACAmount + clsCommon.myCdbl(gv.Rows(ii).Cells(colOtherCharge).Value)


                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colAmtAfterTax).Value)

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

        'For ii As Integer = 0 To gvAC.Rows.Count - 1
        '    If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
        '        dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
        '    End If
        'Next


        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        txttotal_comm_amt.Text = total_commision
        txtTotalFreightAmt.Text = dblTotalFreightAmt

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)

        dblNetAmt = dblTotAmt
        lblInvoiceDiscAmt.Text = dblHeadDisAmt + dblHeadDisPerAmt
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)

        If AllowRoundOff_onInvoice Then
            Dim lstDecml As New List(Of Decimal)
            lstDecml = clsCSASaleInvoice.Calculate_RoundOffAmt(clsCommon.myCdbl(lblTotRAmt.Text), Nothing)

            If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                lblTotRAmt.Text = clsCommon.myCdbl(lstDecml(0))
                txtRoundOff.Text = clsCommon.myCdbl(lstDecml(1))
            End If
        Else
            txtRoundOff.Text = 0
        End If

        lblTotRAmt1.Text = lblTotRAmt.Text
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)

            Dim dblFAmt As Double = 0
            gv.Rows(IntRowNo).Cells(colSaleRate).Value = GetTransferRate(IntRowNo)


            Dim dblQty As Double = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colqty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colUnitRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate) ''+ dblFAmt

            ''======================excise=====================================
            If ExciseentryOnSale AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
                gv.Rows(IntRowNo).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colitemcode).Value), Nothing)
                gv.Rows(IntRowNo).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
                gv.Rows(IntRowNo).Cells(colAbatementAmt).Value = ((clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colMRP).Value) * clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colAbatementPers).Value)) / 100) * clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colqty).Value)
                dblAmt = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colAbatementAmt).Value)
                'gv.CurrentRow.Cells(colTBaseAmt).Value = dblAmt
            End If
            ''===================================================


            Dim dblDisPer As Double = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = (dblQty * dblRate * dblDisPer) / 100

            Dim dblTotDiscAmt = dblDisAmt
            Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt

            '------------------cash amount minus---------------
            Dim dblCash_Amt As Double = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colCash_Amt).Value)
            dblAmtAfterDis = dblAmtAfterDis - dblCash_Amt

            Dim isManditax As String = ""

            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then

                    Dim strTaxCode As String = ""
                    If ii = 1 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax1).Value)
                    ElseIf ii = 2 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax2).Value)
                    ElseIf ii = 3 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax3).Value)
                    ElseIf ii = 4 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax4).Value)
                    ElseIf ii = 5 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax5).Value)
                    ElseIf ii = 6 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax6).Value)
                    ElseIf ii = 7 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax7).Value)
                    ElseIf ii = 8 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax8).Value)
                    ElseIf ii = 9 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax9).Value)
                    ElseIf ii = 10 Then
                        strTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax10).Value)
                    End If

                    isManditax = clsCSASaleInvoiceItem.MandiTax(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colCSAType).Value), strTaxCode)

                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dblTaxRate As Double = 0
                        Dim IsSurTax As Boolean
                        Dim strSurTaxCode As String = ""
                        Dim IsTaxable As Boolean
                        Dim IsExcisable As Boolean
                        If ii = 1 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate1).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax1).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode1).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable1).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable1).Value)
                        ElseIf ii = 2 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate2).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax2).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode2).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable2).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable2).Value)
                        ElseIf ii = 3 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate3).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax3).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode3).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable3).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable3).Value)
                        ElseIf ii = 4 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate4).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax4).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode4).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable4).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable4).Value)
                        ElseIf ii = 5 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate5).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax5).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode5).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable5).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable5).Value)
                        ElseIf ii = 6 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate6).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax6).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode6).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable6).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable6).Value)
                        ElseIf ii = 7 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate7).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax7).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode7).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable7).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable7).Value)
                        ElseIf ii = 8 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate8).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax8).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode8).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable8).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable8).Value)
                        ElseIf ii = 9 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate9).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax9).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode9).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable9).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable9).Value)
                        ElseIf ii = 10 Then
                            dblTaxRate = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxRate10).Value)
                            IsSurTax = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsSurTax10).Value)
                            strSurTaxCode = clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colSurTaxCode10).Value)
                            IsTaxable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsTaxable10).Value)
                            IsExcisable = clsCommon.myCBool(gv.Rows(IntRowNo).Cells(colIsExcisable10).Value)
                        End If

                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0


                        If IsSurTax Then
                            Dim dblSurTaxAmt As Double = 0
                            dblBaseAmt = 0

                            dblSurTaxAmt = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            dblBaseAmt = 0

                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)

                            ''====================excise work===================
                            If ExciseentryOnSale AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                                    dblBaseAmt = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colAbatementAmt).Value) - dblOtherTaxAmt
                                Else
                                    dblBaseAmt = clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colAbatementAmt).Value) + dblOtherTaxAmt
                                End If
                            Else
                                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                                    dblBaseAmt = (dblAmtAfterDis - dblOtherTaxAmt)
                                Else
                                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                                End If
                            End If
                            ''==================================================
                        End If

                        'gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxBaseAmt" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        If ii = 1 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt1).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 2 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt2).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 3 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt3).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 4 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt4).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 5 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt5).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 6 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt6).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 7 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt7).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 8 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt8).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 9 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt9).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 10 Then
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt10).Value = Math.Round(dblBaseAmt, 2)
                        End If

                        'If isManditax = "Y" Then
                        '    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        'Else
                        '    dblTaxAmt = 0
                        'End If

                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                            If isManditax = "Y" Then
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTaxRate)
                            Else
                                dblTaxAmt = 0
                            End If
                        Else
                            If isManditax = "Y" Then
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                            Else
                                dblTaxAmt = 0
                            End If
                        End If

                        'gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
                        If ii = 1 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt1).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 2 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt2).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 3 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt3).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 4 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt4).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 5 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt5).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 6 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt6).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 7 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt7).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 8 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt8).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 9 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt9).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 10 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt10).Value = Math.Round(dblTaxAmt, 2)
                        End If
                        If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If
                    Else
                        If ii = 1 Then
                            gv.Rows(IntRowNo).Cells(colTax1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable1).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable1).Value = Nothing
                        ElseIf ii = 2 Then
                            gv.Rows(IntRowNo).Cells(colTax2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable2).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable2).Value = Nothing
                        ElseIf ii = 3 Then
                            gv.Rows(IntRowNo).Cells(colTax3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable3).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable3).Value = Nothing
                        ElseIf ii = 4 Then
                            gv.Rows(IntRowNo).Cells(colTax4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable4).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable4).Value = Nothing
                        ElseIf ii = 5 Then
                            gv.Rows(IntRowNo).Cells(colTax5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable5).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable5).Value = Nothing
                        ElseIf ii = 6 Then
                            gv.Rows(IntRowNo).Cells(colTax6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable6).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable6).Value = Nothing
                        ElseIf ii = 7 Then
                            gv.Rows(IntRowNo).Cells(colTax7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable7).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable7).Value = Nothing
                        ElseIf ii = 8 Then
                            gv.Rows(IntRowNo).Cells(colTax8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable8).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable8).Value = Nothing
                        ElseIf ii = 9 Then
                            gv.Rows(IntRowNo).Cells(colTax9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable9).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable9).Value = Nothing
                        ElseIf ii = 10 Then
                            gv.Rows(IntRowNo).Cells(colTax10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxBaseAmt10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxRate10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colTaxAmt10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsSurTax10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colSurTaxCode10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsTaxable10).Value = Nothing
                            gv.Rows(IntRowNo).Cells(colIsExcisable10).Value = Nothing
                        End If
                    End If
                ElseIf rbtnTaxCalManual.IsChecked Then
                    If gv2.Rows.Count >= ii Then
                        Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                        Dim dblsalevalue As Double = clsCommon.myCdbl(gv.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colUnitRate).Value) * clsCommon.myCdbl(gv.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colqty).Value)
                        Dim dblCurrRowAmt As Double = dblsalevalue
                        Dim dblTotAmt As Double = 0
                        For jj As Integer = 0 To gv.Rows.Count - 1
                            dblTotAmt += dblsalevalue
                        Next
                        Dim dblCurrCalTax As Double = 0
                        If dblTotAmt <> 0 Then
                            dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                        End If

                        If ii = 1 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt1).Value = dblCurrCalTax
                        ElseIf ii = 2 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt2).Value = dblCurrCalTax
                        ElseIf ii = 3 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt3).Value = dblCurrCalTax
                        ElseIf ii = 4 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt4).Value = dblCurrCalTax
                        ElseIf ii = 5 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt5).Value = dblCurrCalTax
                        ElseIf ii = 6 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt6).Value = dblCurrCalTax
                        ElseIf ii = 7 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt7).Value = dblCurrCalTax
                        ElseIf ii = 8 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt8).Value = dblCurrCalTax
                        ElseIf ii = 9 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt9).Value = dblCurrCalTax
                        ElseIf ii = 10 Then
                            gv.Rows(IntRowNo).Cells(colTaxAmt10).Value = dblCurrCalTax
                        End If

                    End If
                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                dblAmtAfterTax = dblAmtAfterDis
            Else
                dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt
            End If


            gv.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            gv.Rows(IntRowNo).Cells(colSaleRate).Value = GetTransferRate(IntRowNo)

            gv.Rows(IntRowNo).Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colSaleValue).Value) - clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colstckratevalue).Value), 2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If ii = 1 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt1).Value)
                ElseIf ii = 2 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt2).Value)
                ElseIf ii = 3 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt3).Value)
                ElseIf ii = 4 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt4).Value)
                ElseIf ii = 5 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt5).Value)
                ElseIf ii = 6 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt6).Value)
                ElseIf ii = 7 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt7).Value)
                ElseIf ii = 8 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt8).Value)
                ElseIf ii = 9 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt9).Value)
                ElseIf ii = 10 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt10).Value)
                End If

            Else
                If ii = 1 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt1).Value)
                ElseIf ii = 2 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt2).Value)
                ElseIf ii = 3 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt3).Value)
                ElseIf ii = 4 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt4).Value)
                ElseIf ii = 5 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt5).Value)
                ElseIf ii = 6 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt6).Value)
                ElseIf ii = 7 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt7).Value)
                ElseIf ii = 8 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt8).Value)
                ElseIf ii = 9 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt9).Value)
                ElseIf ii = 10 Then
                    dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt10).Value)
                End If

            End If
        Next
        Return dblTotTax
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If ii = 1 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax1).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt1).Value)
                        End If
                    ElseIf ii = 2 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax2).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt2).Value)
                        End If
                    ElseIf ii = 3 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax3).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt3).Value)
                        End If
                    ElseIf ii = 4 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax4).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt4).Value)
                        End If
                    ElseIf ii = 5 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax5).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt5).Value)
                        End If
                    ElseIf ii = 6 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax6).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt6).Value)
                        End If
                    ElseIf ii = 7 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax7).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt7).Value)
                        End If
                    ElseIf ii = 8 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax8).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt8).Value)
                        End If
                    ElseIf ii = 9 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax9).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt9).Value)
                        End If
                    ElseIf ii = 10 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(colTax10).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt10).Value)
                        End If
                    End If
                Else
                    If ii = 1 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax1).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt1).Value)
                        End If
                    ElseIf ii = 2 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax2).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt2).Value)
                        End If
                    ElseIf ii = 3 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax3).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt3).Value)
                        End If
                    ElseIf ii = 4 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax4).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt4).Value)
                        End If
                    ElseIf ii = 5 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax5).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt5).Value)
                        End If
                    ElseIf ii = 6 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax6).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt6).Value)
                        End If
                    ElseIf ii = 7 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax7).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt7).Value)
                        End If
                    ElseIf ii = 8 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax8).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt8).Value)
                        End If
                    ElseIf ii = 9 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax9).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt9).Value)
                        End If
                    ElseIf ii = 10 Then
                        If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax10).Value)) = CompairStringResult.Equal Then
                            dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt10).Value)
                        End If
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
                If ii = 1 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax1).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt1).Value)
                    End If
                ElseIf ii = 2 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax2).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt2).Value)
                    End If
                ElseIf ii = 3 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax3).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt3).Value)
                    End If
                ElseIf ii = 4 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax4).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt4).Value)
                    End If
                ElseIf ii = 5 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax5).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt5).Value)
                    End If
                ElseIf ii = 6 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax6).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt6).Value)
                    End If
                ElseIf ii = 7 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax7).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt7).Value)
                    End If
                ElseIf ii = 8 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax8).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt8).Value)
                    End If
                ElseIf ii = 9 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax9).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt9).Value)
                    End If
                ElseIf ii = 10 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(colTax10).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.CurrentRow.Cells(colTaxAmt10).Value)
                    End If
                End If

            Else
                If ii = 1 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax1).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt1).Value)
                    End If
                ElseIf ii = 2 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax2).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt2).Value)
                    End If
                ElseIf ii = 3 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax3).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt3).Value)
                    End If
                ElseIf ii = 4 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax4).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt4).Value)
                    End If
                ElseIf ii = 5 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax5).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt5).Value)
                    End If
                ElseIf ii = 6 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax6).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt6).Value)
                    End If
                ElseIf ii = 7 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax7).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt7).Value)
                    End If
                ElseIf ii = 8 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax8).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt8).Value)
                    End If
                ElseIf ii = 9 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax9).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt9).Value)
                    End If
                ElseIf ii = 10 Then
                    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(colTax10).Value)) = CompairStringResult.Equal Then
                        Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(colTaxAmt10).Value)
                    End If
                End If

            End If
        Next
        Return 0
    End Function

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        'For ii As Integer = 1 To 10
        'Dim strII As String = clsCommon.myCstr(ii)
        If intRowNo < 0 Then
            gv.CurrentRow.Cells(colTax1).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt1).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate1).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt1).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable1).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax1).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode1).Value = Nothing

            gv.CurrentRow.Cells(colTax2).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt2).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate2).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt2).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable2).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax2).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode2).Value = Nothing

            gv.CurrentRow.Cells(colTax3).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt3).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate3).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt3).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable3).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax3).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode3).Value = Nothing

            gv.CurrentRow.Cells(colTax4).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt4).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate4).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt4).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable4).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax4).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode4).Value = Nothing

            gv.CurrentRow.Cells(colTax5).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt5).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate5).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt5).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable5).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax5).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode5).Value = Nothing

            gv.CurrentRow.Cells(colTax6).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt6).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate6).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt6).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable6).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax6).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode6).Value = Nothing

            gv.CurrentRow.Cells(colTax7).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt7).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate7).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt7).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable7).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax7).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode7).Value = Nothing

            gv.CurrentRow.Cells(colTax8).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt8).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate8).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt8).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable8).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax8).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode8).Value = Nothing

            gv.CurrentRow.Cells(colTax9).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt9).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate9).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt9).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable9).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax9).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode9).Value = Nothing

            gv.CurrentRow.Cells(colTax10).Value = Nothing
            gv.CurrentRow.Cells(colTaxBaseAmt10).Value = Nothing
            If isBlankRate Then
                gv.CurrentRow.Cells(colTaxRate10).Value = Nothing
            End If
            gv.CurrentRow.Cells(colTaxAmt10).Value = Nothing
            gv.CurrentRow.Cells(colIsTaxable10).Value = Nothing
            gv.CurrentRow.Cells(colIsSurTax10).Value = Nothing
            gv.CurrentRow.Cells(colSurTaxCode10).Value = Nothing
        Else
            gv.Rows(intRowNo).Cells(colTax1).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt1).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate1).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt1).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable1).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax1).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode1).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax2).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt2).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate2).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt2).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable2).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax2).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode2).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax3).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt3).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate3).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt3).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable3).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax3).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode3).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax4).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt4).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate4).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt4).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable4).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax4).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode4).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax5).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt5).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate5).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt5).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable5).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax5).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode5).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax6).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt6).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate6).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt6).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable6).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax6).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode6).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax7).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt7).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate7).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt7).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable7).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax7).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode7).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax8).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt8).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate8).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt8).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable8).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax8).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode8).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax9).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt9).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate9).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt9).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable9).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax9).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode9).Value = Nothing

            gv.Rows(intRowNo).Cells(colTax10).Value = Nothing
            gv.Rows(intRowNo).Cells(colTaxBaseAmt10).Value = Nothing
            If isBlankRate Then
                gv.Rows(intRowNo).Cells(colTaxRate10).Value = Nothing
            End If
            gv.Rows(intRowNo).Cells(colTaxAmt10).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsTaxable10).Value = Nothing
            gv.Rows(intRowNo).Cells(colIsSurTax10).Value = Nothing
            gv.Rows(intRowNo).Cells(colSurTaxCode10).Value = Nothing
        End If
        ' Next
    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtcustcode.Value, txt_loc_code.Value, OpenALLTaxes)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv.CurrentRow.Cells(colitemcode).Value) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        If ii = 1 Then
                            gv.CurrentRow.Cells(colTax1).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode1).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 2 Then
                            gv.CurrentRow.Cells(colTax2).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode2).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 3 Then
                            gv.CurrentRow.Cells(colTax3).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode3).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 4 Then
                            gv.CurrentRow.Cells(colTax4).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode4).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 5 Then
                            gv.CurrentRow.Cells(colTax5).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode5).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 6 Then
                            gv.CurrentRow.Cells(colTax6).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode6).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 7 Then
                            gv.CurrentRow.Cells(colTax7).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode7).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 8 Then
                            gv.CurrentRow.Cells(colTax8).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode8).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 9 Then
                            gv.CurrentRow.Cells(colTax9).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode9).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        ElseIf ii = 10 Then
                            gv.CurrentRow.Cells(colTax10).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv.CurrentRow.Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv.CurrentRow.Cells(colIsTaxable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colIsSurTax10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colSurTaxCode10).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv.CurrentRow.Cells(colIsExcisable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv.CurrentRow.Cells(colTaxRecoverable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        End If

                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv.Rows.Count - 1

                    If clsCommon.myLen(gv.Rows(intRowNo).Cells(colitemcode).Value) > 0 Then
                        BlankTaxDetails(intRowNo, isChangeRate)
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            If ii = 1 Then
                                gv.Rows(intRowNo).Cells(colTax1).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate1).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode1).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable1).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 2 Then
                                gv.Rows(intRowNo).Cells(colTax2).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate2).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode2).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable2).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 3 Then
                                gv.Rows(intRowNo).Cells(colTax3).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate3).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode3).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable3).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 4 Then
                                gv.Rows(intRowNo).Cells(colTax4).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate4).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode4).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable4).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 5 Then
                                gv.Rows(intRowNo).Cells(colTax5).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate5).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode5).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable5).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 6 Then
                                gv.Rows(intRowNo).Cells(colTax6).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate6).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode6).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable6).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 7 Then
                                gv.Rows(intRowNo).Cells(colTax7).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate7).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode7).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable7).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 8 Then
                                gv.Rows(intRowNo).Cells(colTax8).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate8).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode8).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable8).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 9 Then
                                gv.Rows(intRowNo).Cells(colTax9).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate9).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode9).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable9).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ElseIf ii = 10 Then
                                gv.Rows(intRowNo).Cells(colTax10).Value = clsCommon.myCstr(dr("Tax_Code"))
                                If isChangeRate Then
                                    gv.Rows(intRowNo).Cells(colTaxRate10).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv.Rows(intRowNo).Cells(colIsTaxable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colIsSurTax10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colSurTaxCode10).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv.Rows(intRowNo).Cells(colIsExcisable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                                gv.Rows(intRowNo).Cells(colTaxRecoverable10).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            End If
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            'If isChangeRate Then
                            '    gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            'End If
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            'gv.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
        dt = Nothing
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        txtTermCode.Value = ClsReceivablePaymentTerms.getFinderWithSaleType(txtTermCode.Value, "C", isButtonClicked)
        SetTermDetails()
    End Sub

    Sub SetTermDetails()
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER where Terms_Code='" + txtTermCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtDueDate.Value = dtpdate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No Of Days")))
        Else
            lblTermName.Text = ""
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column Is gv.Columns(coldoubleclick) Then
                FillTransferStockData(True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FillTransferStockData(ByVal gvDoubleClick As Boolean)
        Try
            isInsideLoadData = True

            ''==============================BM00000009263
            If Not gvDoubleClick AndAlso TransferManual_KnockOFF Then
                ''when transfer manual knock-off then it is not auto fill transfer detail, it fill only when double click on cell
                gv.CurrentRow.Tag = Nothing
                gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing
                gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                gv.CurrentRow.Cells(colstckratevalue).Value = 0
                gv.CurrentRow.Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colSaleValue).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colstckratevalue).Value), 2)

                CalAltQty(gv.CurrentRow.Index)
                CalConversionFactor_GV(gv.CurrentRow.Index)
                CalUnitPrice(gv.CurrentRow.Index, True)
                CommisionValue(gv.CurrentRow.Index)
                UpdateCurrentRow(gv.CurrentRow.Index)
                If rbtnTaxCalManual.IsChecked Then
                    For ii As Integer = 0 To gv.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                End If
                UpdateAllTotals()
                isInsideLoadData = False
                Exit Sub
            End If


            '=============create demo table for datasave of transfer=====================
            Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
            '================================================================================

            Dim frm As New FrmCSATrans_KnockOffScreen()
            frm.TransferManual_KnockOFF = TransferManual_KnockOFF
            frm.strDocCode = clsCommon.myCstr(txtCode.Value)
            frm.strDocDate = clsCommon.myCDate(dtpdate.Text)
            frm.strCustCode = clsCommon.myCstr(txtcustcode.Value)
            frm.strPlantLoc_Code = clsCommon.myCstr(txt_loc_code.Value)
            frm.strCSAloc_code = clsCommon.myCstr(txtCSAloc_code.Value)
            frm.colPackSize = clsCommon.myCdbl(gv.CurrentRow.Cells(colPackSize).Value)
            frm.colStckTransferrate = 0 ' clsCommon.myCdbl(gv.CurrentRow.Cells(colStckTransferrate).Value)
            frm.colitemcode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
            frm.colqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)
            frm.colFOC = clsCommon.myCstr(gv.CurrentRow.Cells(colFOC).Value)
            frm.colItemUOM = clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value)
            frm.LoadTransGrid()
            frm.ComeFromImport = False
            frm.FillTransfergrid(clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value), TryCast(gv.CurrentRow.Tag, List(Of clsCSAStockTransferDetail)))
            If gvDoubleClick = True Then
                frm.ShowDialog()
            Else
                frm.btnsave.PerformClick()
            End If



            If clsCommon.myCdbl(frm.colStckTransferrate) <= 0 AndAlso clsCommon.CompairString(frm.colFOC, "Y") <> CompairStringResult.Equal Then
                gv.CurrentRow.Tag = Nothing
                gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing
                gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                gv.CurrentRow.Cells(colstckratevalue).Value = 0
                gv.CurrentRow.Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colSaleValue).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colstckratevalue).Value), 2)

                CalAltQty(gv.CurrentRow.Index)
                CalConversionFactor_GV(gv.CurrentRow.Index)
                CalUnitPrice(gv.CurrentRow.Index, True)
                CommisionValue(gv.CurrentRow.Index)
                UpdateCurrentRow(gv.CurrentRow.Index)
                If rbtnTaxCalManual.IsChecked Then
                    For ii As Integer = 0 To gv.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                End If
                UpdateAllTotals()
                isInsideLoadData = False
                Exit Sub
            End If

            gv.CurrentRow.Tag = Nothing
            gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing

            ''=====================================================================
            Dim ArrTransferNo_For_Batch As New ArrayList()
            If frm.GV_ARR IsNot Nothing AndAlso TryCast(frm.GV_ARR, List(Of clsCSAStockTransferDetail)) IsNot Nothing Then
                For Each obj As clsCSAStockTransferDetail In frm.GV_ARR
                    If Not ArrTransferNo_For_Batch.Contains(obj.transcode) Then
                        ArrTransferNo_For_Batch.Add(obj.transcode)
                    End If
                Next
            End If
            gv.CurrentRow.Cells(coldoubleclick).Tag = ArrTransferNo_For_Batch
            ''=====================================================================

            gv.CurrentRow.Tag = frm.GV_ARR

            If gv.CurrentRow.Cells(colSetZeroValue).Value = True Then
                gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                gv.CurrentRow.Cells(colstckratevalue).Value = 0 '' System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colStckTransferrate).Value), 2)
            Else
                gv.CurrentRow.Cells(colStckTransferrate).Value = frm.colStckTransferrate
                gv.CurrentRow.Cells(colstckratevalue).Value = frm.colStckTransferAmount '' System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colStckTransferrate).Value), 2)
            End If
            gv.CurrentRow.Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colSaleValue).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colstckratevalue).Value), 2)

            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                gv.CurrentRow.Cells(colStckTransferrate).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index - 1).Cells(colStckTransferrate).Value)
            End If

            CalAltQty(gv.CurrentRow.Index)
            CalConversionFactor_GV(gv.CurrentRow.Index)
            CalUnitPrice(gv.CurrentRow.Index, True)
            CommisionValue(gv.CurrentRow.Index)
            UpdateCurrentRow(gv.CurrentRow.Index)
            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 0 To gv.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
            End If
            UpdateAllTotals()

            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try

            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv.Columns(colbookingtype) Then
                        isCellValueChanged = True
                        If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value), "Item-Wise") = CompairStringResult.Equal Then
                            gv.CurrentRow.Cells(colitemcode).ReadOnly = True
                        Else
                            gv.CurrentRow.Cells(colitemcode).ReadOnly = False
                        End If
                        isCellValueChanged = False
                    End If

                    If Not BookingEffectOnSale AndAlso e.Column Is gv.Columns(colincludingtax) Then
                        isCellValueChanged = True
                        If ForUDLOnly AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colincludingtax).Value) <= 0 Then
                            gv.CurrentRow.Cells(colincludingtax).Value = "No"
                            gv.CurrentRow.Cells(colTaxBasis).Value = "Forward Calculation"
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colincludingtax).Value), "Yes") = CompairStringResult.Equal Then
                            gv.CurrentRow.Cells(colTaxBasis).Value = "Back Calculation"
                        Else
                            gv.CurrentRow.Cells(colTaxBasis).Value = "Forward Calculation"
                        End If
                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        UpdateAllTotals()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colTotTaxAmt) OrElse (Not BookingEffectOnSale AndAlso e.Column Is gv.Columns(colbookingrate)) Then
                        isCellValueChanged = True
                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        UpdateAllTotals()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gv.Columns(colOtherCharge) OrElse (e.Column Is gv.Columns(colqty)) OrElse (e.Column Is gv.Columns(colSaleRate)) OrElse (e.Column Is gv.Columns(colDisPer)) OrElse (e.Column Is gv.Columns(colUnitRate)) OrElse e.Column Is gv.Columns(colMRP) Then
                        isCellValueChanged = True

                        If e.Column Is gv.Columns(colqty) Then
                            gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing ''transferno reset
                            gv.CurrentRow.Cells(colitemcode).Tag = Nothing ''batch array reset
                        End If

                        If ExciseentryOnSale Then
                            '==================calculate abatement%========================================================================
                            gv.CurrentRow.Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
                            gv.CurrentRow.Cells(colAbatementAmt).Value = ((clsCommon.myCdbl(gv.CurrentRow.Cells(colMRP).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colAbatementPers).Value)) / 100) * clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)
                            '=============================================================================================================
                        End If


                        gv.CurrentRow.Cells(colCash_Amt).Value = (clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colUnitRate).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCash_Pers).Value)) / 100
                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        If rbtnTaxCalManual.IsChecked Then
                            For ii As Integer = 0 To gv.Rows.Count - 1
                                UpdateCurrentRow(ii)
                            Next
                        End If
                        UpdateAllTotals()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gv.Columns(colitemcode) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing ''transferno reset
                        gv.CurrentRow.Cells(colitemcode).Tag = Nothing ''batch array reset
                        OpenIcode(False)
                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        UpdateAllTotals()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gv.Columns(colBookingno) Then ' AndAlso Not ChkFOC.Checked
                        isCellValueChanged = True
                        For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value)) = CompairStringResult.Equal Then
                                gv.Rows.RemoveAt(ii)
                            End If
                        Next
                        gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing ''transferno reset
                        gv.CurrentRow.Cells(colitemcode).Tag = Nothing ''batch array reset
                        OpenBooking(False)
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colqty)) Then
                        isCellValueChanged = True
                        If Not showBatchSkipAtCSAReturn Then '=======[If Batch skip then Batch dosen't work]
                            If TryCast(gv.CurrentRow.Cells(coldoubleclick).Tag, ArrayList) IsNot Nothing AndAlso TryCast(gv.CurrentRow.Cells(coldoubleclick).Tag, ArrayList).Count > 0 Then
                                OpenBatchItem()
                            End If
                        End If
                        FillTransferStockData(False)

                        Dim index As Integer = gv.CurrentRow.Index

                        FillFreeItemsInGrid()
                        isValid_CashScheme()
                        CalAltQty(index)
                        CalConversionFactor_GV(index)
                        CalUnitPrice(index, True)
                        CommisionValue(index)
                        gv.Rows(index).Cells(colSaleRate).Value = GetTransferRate(index)
                        UpdateCurrentRow(index)
                        UpdateAllTotals()
                        'gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                        'gv.CurrentRow.Cells(colstckratevalue).Value = 0
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colItemUOM)) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(coldoubleclick).Tag = Nothing ''transferno reset
                        gv.CurrentRow.Cells(colitemcode).Tag = Nothing ''batch array reset
                        OpenAltUOM(False)

                        Dim index As Integer = gv.CurrentRow.Index

                        FillTransferStockData(False)
                        FillFreeItemsInGrid()
                        isValid_CashScheme()

                        CalAltQty(index)
                        CalConversionFactor_GV(index)
                        CalUnitPrice(index, True)
                        CommisionValue(index)
                        gv.Rows(index).Cells(colSaleRate).Value = GetTransferRate(index)
                        UpdateCurrentRow(index)
                        UpdateAllTotals()
                        'gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                        'gv.CurrentRow.Cells(colstckratevalue).Value = 0
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colCommision)) OrElse (e.Column Is gv.Columns(colFrghtRate)) OrElse (e.Column Is gv.Columns(colPackSize) OrElse e.Column Is gv.Columns(colOtherCharge)) Then
                        isCellValueChanged = True
                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        gv.CurrentRow.Cells(colSaleRate).Value = GetTransferRate(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        If rbtnTaxCalManual.IsChecked Then
                            For ii As Integer = 0 To gv.Rows.Count - 1
                                UpdateCurrentRow(ii)
                            Next
                        End If
                        UpdateAllTotals()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gv.Columns(colIsSchmItem)) OrElse (e.Column Is gv.Columns(colSchmCodeType)) Then
                        isCellValueChanged = True
                        Dim index As Integer = gv.CurrentRow.Index

                        FillFreeItemsInGrid()
                        isValid_CashScheme()

                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(index).Cells(colIsSchmItem).Value), "Y") <> CompairStringResult.Equal Then
                            gv.Rows(index).Cells(colCashSchemeCode).Value = ""
                            gv.Rows(index).Cells(colCashSchemeType).Value = ""
                            gv.Rows(index).Cells(colCash_Amt).Value = 0
                            gv.Rows(index).Cells(colCash_Pers).Value = 0
                            gv.Rows(index).Cells(colIsSchmItem).Value = "N"
                        End If

                        CalAltQty(index)
                        CalConversionFactor_GV(index)
                        CalUnitPrice(index, True)
                        CommisionValue(index)
                        gv.Rows(index).Cells(colSaleRate).Value = GetTransferRate(index)
                        UpdateCurrentRow(index)
                        UpdateAllTotals()
                        'gv.CurrentRow.Cells(colStckTransferrate).Value = 0
                        'gv.CurrentRow.Cells(colstckratevalue).Value = 0
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CommisionValue(ByVal CurrentRow As Integer)
        Try
            Dim ItemCode As String = clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value)

            If clsCommon.myLen(ItemCode) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                Exit Sub
            End If
            Dim commsnrate As Decimal = clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colCommision).Value)
            Dim commsnuom As String = ""
            Dim FreightUOM As String = ""
            Dim Master_Sku As Decimal = 0
            Dim Master_Sku_Freight As Decimal = 0
            Dim qry As String = ""

            If ApplyFreight_Cmmsn_Charge_Itemwise Then
                ''if freight and commission itemwise then below code run
                commsnuom = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_UOM from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(txtcustcode.Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code='" + ItemCode + "' "))
                FreightUOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_UOM from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(txtcustcode.Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code='" + ItemCode + "' "))

                If clsCommon.myLen(commsnuom) <= 0 AndAlso clsCommon.myLen(FreightUOM) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill either Commission or Freight UOM not defined, for Item " & clsCommon.myCstr(gv.Rows(CurrentRow).Cells(coliname).Value) & "")
                    Exit Sub
                End If

                If clsCommon.myLen(commsnuom) <= 0 AndAlso clsCommon.myLen(FreightUOM) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill either Commission or Freight UOM not defined for location " & clsCommon.myCstr(gv.Rows(CurrentRow).Cells(coliname).Value) & "")
                    Exit Sub
                End If
            Else
                commsnuom = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CSA_Commision_Type from TSPL_LOCATION_MASTER where Cust_Code='" + clsCommon.myCstr(txtcustcode.Value) + "' and location_code='" + clsCommon.myCstr(txtCSAloc_code.Value) + "'"))
                If clsCommon.myLen(commsnuom) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Commission Rate UOM not defined for location " & txtCSAloc_code.Value & "")
                    Exit Sub
                End If
            End If


            Dim Weight_value As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Weight_Value from tspl_item_master where item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "' "))
            Dim weight_uom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_uom from tspl_item_master where item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "' "))
            If clsCommon.myLen(weight_uom) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Weight UOM not defined for Item  " & gv.Rows(CurrentRow).Cells(colitemcode).Value & "")
                Exit Sub
            End If

            Dim convFacter As Decimal = clsItemMaster.GetConvertionFactor(gv.Rows(CurrentRow).Cells(colitemcode).Value, gv.Rows(CurrentRow).Cells(colItemUOM).Value, Nothing)
            Weight_value = Weight_value * convFacter
            gv.Rows(CurrentRow).Cells(colPackSize).Value = Weight_value

            If clsCommon.myLen(commsnuom) > 0 Then
                qry = "select top 1 CF from (select (case when (Container_UOM='" & commsnuom & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & commsnuom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value), Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                Master_Sku = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            End If

            If Master_Sku = 0 Then
                Master_Sku = 1
            End If

            gv.Rows(CurrentRow).Cells(colMasterPackSize).Value = Master_Sku

            Try
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colComm_Type_RS_Pers).Value), "P") = CompairStringResult.Equal Then
                    If CalculateCommOnCSATransWOConversion = 0 Then
                        gv.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) * (clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colMasterPackSize).Value) = 0, 1, clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colMasterPackSize).Value))) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colCommision).Value)) / 100, 2)
                    Else
                        gv.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colCommision).Value)) / 100, 2)
                    End If
                Else
                    gv.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round(clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colqty).Value) * (clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colMasterPackSize).Value) = 0, 1, clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colMasterPackSize).Value))) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colCommision).Value), 2) '* clsCommon.myCdbl(gv.CurrentRow.Cells(colConversionFactor).Value)
                End If

            Catch exx As Exception
                gv.Rows(CurrentRow).Cells(colCommisionValue).Value = 0
            End Try

            If clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) < clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colCommisionValue).Value) Then
                gv.Rows(CurrentRow).Cells(colCommisionValue).Value = 0
            End If

            ''=========================================================================
            If ApplyFreight_Cmmsn_Charge_Itemwise Then

                If clsCommon.myLen(FreightUOM) > 0 Then
                    qry = "select top 1 CF from (select (case when (Container_UOM='" & FreightUOM & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & FreightUOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value), Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                    Master_Sku_Freight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                End If

                If Master_Sku_Freight = 0 Then
                    Master_Sku_Freight = 1
                End If

                Try
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colFrghtType).Value), "P") = CompairStringResult.Equal Then
                        If CalculateCommOnCSATransWOConversion = 0 Then
                            gv.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) * (clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(Master_Sku_Freight) = 0, 1, clsCommon.myCdbl(Master_Sku_Freight))) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colFrghtRate).Value)) / 100, 2)
                        Else
                            gv.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colFrghtRate).Value)) / 100, 2)
                        End If
                    Else
                        gv.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round(clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colqty).Value) * (clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(Master_Sku_Freight) = 0, 1, clsCommon.myCdbl(Master_Sku_Freight))) * clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colFrghtRate).Value), 2) '* clsCommon.myCdbl(gv.CurrentRow.Cells(colConversionFactor).Value)
                    End If

                Catch exx As Exception
                    gv.Rows(CurrentRow).Cells(colFrghtAmt).Value = 0
                End Try

                If clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colSaleValue).Value) < clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colFrghtAmt).Value) Then
                    gv.Rows(CurrentRow).Cells(colFrghtAmt).Value = 0
                End If
            End If
            ''=========================================================================
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function GetTransferRate(ByVal intRow As Integer)
        Dim totalTax As Double = 0
        Dim OtherCharges As Double = 0
        Dim commisionrate As Double = 0
        Dim totAmount As Double = 0
        Dim TransferRate As Double = 0
        totalTax = clsCommon.myCdbl(gv.Rows(intRow).Cells(colTotTaxAmt).Value)
        OtherCharges = clsCommon.myCdbl(gv.Rows(intRow).Cells(colOtherCharge).Value)
        commisionrate = 0 ' clsCommon.myCdbl(gv.Rows(intRow).Cells(colCommisionValue).Value)
        totAmount = clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value) * clsCommon.myCdbl(gv.Rows(intRow).Cells(colUnitRate).Value) - clsCommon.myCdbl(gv.Rows(intRow).Cells(colCash_Amt).Value)
        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(intRow).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
            TransferRate = System.Math.Round((totAmount - totalTax - OtherCharges - commisionrate) / IIf(clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value) = 0, 1, clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value)), 2)
        Else
            TransferRate = System.Math.Round((totAmount) / IIf(clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value) = 0, 1, clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value)), 2)
        End If

        If gv.Rows(intRow).Cells(colSetZeroValue).Value = True Then
            TransferRate = 0
            gv.Rows(intRow).Cells(colStckTransferrate).Value = 0
            gv.Rows(intRow).Cells(colstckratevalue).Value = 0
        End If

        gv.Rows(intRow).Cells(colSaleValue).Value = Math.Round(TransferRate * clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value), 2)

        ''for UDL only=====================
        If Apply_PriceChat_On_OtherItems Then
            gv.Rows(intRow).Cells(colSaleValue).Value = clsCommon.myCdbl(gv.Rows(intRow).Cells(colstckratevalue).Value)
            If clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value) > 0 AndAlso gv.Rows(intRow).Cells(colSetZeroValue).Value = False Then
                TransferRate = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colstckratevalue).Value) / clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value), 2)
            Else
                TransferRate = 0
            End If
        End If
        ''====================================================



        CommisionValue(intRow)
        Return TransferRate
    End Function

    Private Sub CalAltQty(ByVal CurrentRow As Integer)
        Try
            Dim qty As Decimal = clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colqty).Value)
            Dim uom As String = clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colItemUOM).Value)
            Dim altuom As String = clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colAltUOM).Value)
            Dim bookingrate As String = clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colbookingrate).Value)

            Dim altconversion As Decimal = 0
            altconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "' and uom_code='" + altuom + "'"))
            'If altconversion <= 0 Then
            '    altconversion = 1
            'End If

            Dim conversion As Decimal = 0
            conversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"))
            'If conversion <= 0 Then
            '    conversion = 1
            'End If

            Dim altqty As Decimal = 0

            If altconversion > 0 Then
                altqty = System.Math.Round((qty * conversion) / altconversion, 2)
            Else
                altqty = 0
            End If
            gv.Rows(CurrentRow).Cells(colAltQty).Value = altqty

            'Dim qry As String = "select *,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no"
            'qry += " where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv.CurrentRow.Cells(colincludingtax).Value) + "'"
            'qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Dim ltr_per_case As Decimal = clsCommon.myCdbl(dt.Rows(0)("Ltr_Per_Case"))
            '    Dim pcs_per_case As Decimal = clsCommon.myCdbl(dt.Rows(0)("Pcs_Per_Case"))
            '    Dim basic_uom As String = clsCommon.myCstr(dt.Rows(0)("UOM"))
            '    Dim actualqty As Decimal = 0

            '    If uom.ToUpper().Contains("PCS") AndAlso altuom.ToUpper().Contains("CASE") Then
            '        If pcs_per_case > 0 Then
            '            qty = qty * pcs_per_case
            '            gv.CurrentRow.Cells(colAltQty).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) / clsCommon.myCdbl(pcs_per_case), 2)
            '        End If
            '    ElseIf uom.ToUpper().Contains("CASE") AndAlso altuom.ToUpper().Contains("PCS") Then
            '        If pcs_per_case > 0 Then
            '            qty = qty / pcs_per_case
            '            gv.CurrentRow.Cells(colAltQty).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(pcs_per_case), 2)
            '        End If
            '    ElseIf Not uom.ToUpper().Contains("LTR") AndAlso altuom.ToUpper().Contains("CASE") Then
            '        If ltr_per_case > 0 Then
            '            qty = qty * ltr_per_case
            '            gv.CurrentRow.Cells(colAltQty).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) / clsCommon.myCdbl(ltr_per_case), 2)
            '        End If
            '    ElseIf Not uom.ToUpper().Contains("CASE") AndAlso altuom.ToUpper().Contains("LTR") Then
            '        If ltr_per_case > 0 Then
            '            qty = qty / ltr_per_case
            '            gv.CurrentRow.Cells(colAltQty).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(ltr_per_case), 2)
            '        End If
            '    End If

            If BookingEffectOnSale AndAlso clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colbalqty).Value) < altqty AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then ' AndAlso ChkFOC.Checked = False
                gv.Rows(CurrentRow).Cells(colqty).Value = 0
                Throw New Exception("Alt quantity can not be more than balance quantity i.e (" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colbalqty).Value) + ")")
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenIcode(ByVal isButtonClicked As Boolean)
        Try
            If (BookingEffectOnSale AndAlso clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value)) > 0) OrElse (Not BookingEffectOnSale) Then ' OrElse ChkFOC.Checked
                '=only that item comes whose csa type is of selected booking and of post transfer

                Dim whrcls As String = " tspl_item_master.Active=1 and isnull(Is_FreshItem,0)<>1 "
                If BookingEffectOnSale Then
                    whrcls += " and tspl_item_master.csa_type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "' "
                End If
                whrcls += " and (tspl_item_master.item_code in (select item_code from TSPL_CSA_TRANSFER_DETAIL where FOC <> 'Y' and doc_code in (select doc_code from TSPL_CSA_TRANSFER_HEAD where isnull(status,0)=1 and cust_code='" + txtcustcode.Value + "')) "
                whrcls += " or tspl_item_master.item_code in (select item_code from TSPL_ADJUSTMENT_DETAIL where adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where isnull(posted,'N')='Y')) or tspl_item_master.item_code in (select item_code from tspl_sd_sale_return_detail where document_code in (select document_code from tspl_sd_sale_return_head where return_type not in ('D','S') and [status]=1 and trans_type='CPR' )) )"
                '=============open free items in grid------------------
                'If ChkFOC.Checked Then
                '    whrcls = " tspl_item_master.Active=1 "
                '    whrcls += " and tspl_item_master.item_code in (select item_code from TSPL_CSA_TRANSFER_DETAIL where FOC = 'Y' and doc_code in (select doc_code from TSPL_CSA_TRANSFER_HEAD where isnull(status,0)=1)) "
                'End If
                gv.CurrentRow.Cells(colitemcode).Value = clsCommon.myCstr(clsItemMaster.getFinder(whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), isButtonClicked))

                If clsCommon.myLen(gv.CurrentRow.Cells(colitemcode).Value) > 0 Then
                    gv.CurrentRow.Cells(coliname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"))
                    gv.CurrentRow.Cells(colItemType).Value = LoadItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'")))
                    gv.CurrentRow.Cells(colItemUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"))
                    gv.CurrentRow.Cells(colCSAType).Value = clsItemMaster.GetItemCSAType(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), Nothing)

                    gv.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))
                    gv.CurrentRow.Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), Nothing)
                    ''=====================MRP--------------------------------
                    If ExciseentryOnSale Then
                        gv.CurrentRow.Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), Nothing)
                        gv.CurrentRow.Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
                        ''==========================================
                        Dim Item_TaxType As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in ('" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "')"))
                        If clsLocation.isLocatinExcisable(txt_loc_code.Value) = True AndAlso Item_TaxType = 2 Then
                            cmbEXType.SelectedValue = "E"
                        Else
                            cmbEXType.SelectedValue = "N"
                        End If
                        cmbEXType.Enabled = False
                        ''=============================
                    End If
                    ''=============================================================

                    If ForUDLOnly AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colincludingtax).Value) <= 0 Then
                        gv.CurrentRow.Cells(colincludingtax).Value = "No"
                        gv.CurrentRow.Cells(colTaxBasis).Value = "Forward Calculation"
                    End If

                    CalUnitPrice(CInt(gv.CurrentRow.Index), True)

                    If Not BookingEffectOnSale Then ''no booking select
                        gv.CurrentRow.Cells(colAltUOM).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value)
                    End If

                    gv.CurrentRow.Cells(coldoubleclick).Value = "Double Click"
                    gv.CurrentRow.Cells(colqty).Value = "0"
                    gv.CurrentRow.Cells(colPackSize).Value = 1 ' clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Weight_Value from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' ")) ' clsItemMaster.GetMasterOrSKUCategory(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), False, True)
                    gv.CurrentRow.Cells(colMasterPackSize).Value = 1 ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_UOM from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' ")) 'clsItemMaster.GetMasterOrSKUCategory(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), True, False)
                    If clsCommon.myCdbl(gv.CurrentRow.Cells(colPackSize).Value) > 0 Then
                        gv.CurrentRow.Cells(colPackSize).ReadOnly = True
                    Else
                        gv.CurrentRow.Cells(colPackSize).ReadOnly = False
                    End If
                    If clsCommon.myCdbl(gv.CurrentRow.Cells(colMasterPackSize).Value) <= 0 Then
                        'gv.CurrentRow.Cells(colMasterPackSize).Value = 1000
                    End If

                    ''======================================================
                    If ApplyFreight_Cmmsn_Charge_Itemwise Then
                        Dim qry As String = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Type,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Type " & _
                            " from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No " & _
                            " where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(txtcustcode.Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            gv.CurrentRow.Cells(colCommision).Value = clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
                            gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(dt.Rows(0)("Commission_Type"))

                            gv.CurrentRow.Cells(colFrghtRate).Value = clsCommon.myCdbl(dt.Rows(0)("Freight_Rate"))
                            gv.CurrentRow.Cells(colFrghtType).Value = clsCommon.myCstr(dt.Rows(0)("Freight_Type"))
                        End If

                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) > 0 OrElse clsCommon.myCdbl(gv.CurrentRow.Cells(colFrghtRate).Value) > 0 Then
                            'gv.CurrentRow.Cells(colCommision).ReadOnly = True
                            CommisionValue(gv.CurrentRow.Index)
                        End If
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) <= 0 Then
                            gv.CurrentRow.Cells(colCommision).ReadOnly = False
                        End If
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colFrghtRate).Value) <= 0 Then
                            gv.CurrentRow.Cells(colFrghtRate).ReadOnly = False
                        End If

                    Else
                        gv.CurrentRow.Cells(colCommision).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select CSA_Commision_Rate from TSPL_LOCATION_MASTER where Cust_Code='" + txtcustcode.Value + "' and location_code='" + txtCSAloc_code.Value + "'"))
                        gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(CSA_Commission_RS_PERS,'R') from TSPL_LOCATION_MASTER where Cust_Code='" + txtcustcode.Value + "' and location_code='" + txtCSAloc_code.Value + "'"))

                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) > 0 Then
                            'gv.CurrentRow.Cells(colCommision).ReadOnly = True
                            CommisionValue(gv.CurrentRow.Index)
                        Else
                            gv.CurrentRow.Cells(colCommision).ReadOnly = False
                        End If
                    End If
                    ''===================================================================

                    gv.CurrentRow.Cells(colConversionFactor).Value = 1
                    SetitemWiseTaxSetting(True, True)
                    UpdateCurrentRow(gv.CurrentRow.Index)
                    CommisionValue(gv.CurrentRow.Index)
                    UpdateAllTotals()

                    gv.CurrentRow.Cells(colSchmCode).Value = Nothing
                    gv.CurrentRow.Cells(colSchmCodeType).Value = Nothing
                    gv.CurrentRow.Cells(colMainIcode).Value = ""
                    gv.CurrentRow.Cells(colMainLineNo).Value = "0"
                    gv.CurrentRow.Cells(colMainIQty).Value = "0"
                    gv.CurrentRow.Cells(colMainIUOM).Value = ""
                    gv.CurrentRow.Cells(colFOC).Value = "N"
                    gv.CurrentRow.Cells(colIsSchmItem).Value = ""

                    'If ChkFOC.Checked Then
                    '    gv.CurrentRow.Cells(colFOC).Value = "Y"
                    'End If
                    FillFreeItemsInGrid()
                Else
                    gv.CurrentRow.Cells(colitemcode).Value = ""
                    gv.CurrentRow.Cells(coliname).Value = ""
                    gv.CurrentRow.Cells(colHSNCode).Value = ""
                    gv.CurrentRow.Cells(colConversionFactor).Value = Nothing
                    gv.CurrentRow.Cells(colItemType).Value = ""
                    gv.CurrentRow.Cells(colItemUOM).Value = ""
                    gv.CurrentRow.Cells(colUnitRate).Value = Nothing
                    gv.CurrentRow.Cells(coldoubleclick).Value = "Double Click"
                    gv.CurrentRow.Cells(colPackSize).Value = Nothing
                    gv.CurrentRow.Cells(colMasterPackSize).Value = Nothing
                    gv.CurrentRow.Cells(colCommision).Value = Nothing
                    gv.CurrentRow.Cells(colCommision).ReadOnly = False
                    gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = Nothing
                    gv.CurrentRow.Cells(colFrghtAmt).Value = Nothing
                    gv.CurrentRow.Cells(colFrghtRate).Value = Nothing
                    gv.CurrentRow.Cells(colFrghtType).Value = Nothing
                    gv.CurrentRow.Cells(colFrghtRate).ReadOnly = False
                    gv.CurrentRow.Cells(colIsBatchItem).Value = False
                    gv.CurrentRow.Cells(colPackSize).ReadOnly = False
                    gv.CurrentRow.Cells(colSchmCode).Value = Nothing
                    gv.CurrentRow.Cells(colSchmCodeType).Value = Nothing
                    gv.CurrentRow.Cells(colMainIcode).Value = ""
                    gv.CurrentRow.Cells(colMainLineNo).Value = "0"
                    gv.CurrentRow.Cells(colMainIQty).Value = "0"
                    gv.CurrentRow.Cells(colMainIUOM).Value = ""
                    gv.CurrentRow.Cells(colFOC).Value = "N"
                    gv.CurrentRow.Cells(colIsSchmItem).Value = ""
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CalUnitPrice(ByVal XR As Integer, ByVal CellChanged As Boolean)
        Try
            Dim diffrate As Decimal = 0
            Dim orgrate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim uom As String = ""
            Dim cnvrsn As Decimal = 1
            Dim csauom As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable()
            Dim MRP As Double = Nothing
            Dim CurrntCPDType As String = clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) 'CPD-DESI GHEE
            Dim CSA_State As String = clsCSAPriceMaster.GetCSAState(txtcustcode.Value)

            If CellChanged Then
                'If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(XR).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                '    gv.Rows(XR).Cells(colUnitRate).Value = 0
                '    Exit Sub
                'End If
                uom = clsCommon.myCstr(gv.Rows(XR).Cells(colItemUOM).Value)
                qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                '========(unit price=chart rate*base unit conversion of chart/calc.unit conversion)------------

                qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colitemcode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv.Rows(XR).Cells(colincludingtax).Value) + "' and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txt_loc_code.Value + "') "
                If CSAPricePostedData = True Then
                    qry += " and Tspl_CSA_Price_Head.Posted='1' "
                End If

                ''============when setting ON and item is not CPD then other price chat apply
                If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                    qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtcustcode.Value) + "' "
                Else
                    qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) + "' and tspl_csa_price_state_detail.state_code='" + CSA_State + "' "
                End If

                If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                    qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpdate.Text), "dd/MMM/yyyy") + "' "
                End If
                ''end here=============================
                ''===============end here============================================
                'done by stuti on 08/12/2016
                qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                '==============end here=========================
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                    diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                    MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                    Dim csaconversion As Decimal = 0

                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                        orgrate = clsCommon.myCdbl(diffrate)
                    Else
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + csauom + "'"))

                        If Apply_PriceChat_On_OtherItems Then
                            gv.Rows(XR).Cells(colbookingrate).Value = 0
                        End If
                        orgrate = (clsCommon.myCdbl(gv.Rows(XR).Cells(colbookingrate).Value) + clsCommon.myCdbl(diffrate))
                        If ForUDLOnly Then
                            orgrate = clsCommon.myCdbl(dt.Rows(0)("csa_rate")) + clsCommon.myCdbl(diffrate)
                        End If
                    End If
                    If csaconversion <= 0 Then
                        csaconversion = 1
                    End If


                    orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(XR).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                        gv.Rows(XR).Cells(colUnitRate).Value = 0
                    Else
                        gv.Rows(XR).Cells(colUnitRate).Value = orgrate
                    End If

                    gv.Rows(XR).Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)

                Else
                    If clsCommon.myCdbl(gv.Rows(XR).Cells(colUnitRate).Value) <= 0 Then
                        gv.Rows(XR).Cells(colUnitRate).Value = 0
                    End If
                End If

                If Apply_PriceChat_On_OtherItems AndAlso Not BookingEffectOnSale Then
                    gv.Rows(XR).Cells(colbookingrate).Value = clsCommon.myCdbl(gv.Rows(XR).Cells(colUnitRate).Value)
                End If

            Else

                For Each grow As GridViewRowInfo In gv.Rows
                    'If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                    '    grow.Cells(colUnitRate).Value = 0
                    '    Continue For
                    'End If

                    uom = clsCommon.myCstr(grow.Cells(colItemUOM).Value)
                    qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"
                    cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If

                    If clsCommon.myLen(uom) <= 0 Then
                        Continue For
                    End If

                    qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                    qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                    qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(grow.Cells(colincludingtax).Value) + "'"
                    qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txt_loc_code.Value + "')"
                    If CSAPricePostedData = True Then
                        qry += " and Tspl_CSA_Price_Head.Posted='1' "
                    End If

                    ''============when setting ON and item is not CPD then other price chat apply
                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtcustcode.Value) + "' "
                    Else
                        qry += " and tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) + "' "
                    End If

                    If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                        qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpdate.Text), "dd/MMM/yyyy") + "' "
                    End If
                    ''===============end here============================================
                    'done by stuti on 08/12/2016
                    qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                    '==============end here=========================

                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                        diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                        MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                        Dim csaconversion As Decimal = 0

                        If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                            orgrate = clsCommon.myCdbl(diffrate)
                        Else
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + csauom + "'"))
                            If Apply_PriceChat_On_OtherItems Then
                                gv.Rows(XR).Cells(colbookingrate).Value = 0
                            End If
                            orgrate = (clsCommon.myCdbl(gv.Rows(XR).Cells(colbookingrate).Value) + clsCommon.myCdbl(diffrate))

                            If ForUDLOnly Then
                                orgrate = (clsCommon.myCdbl(dt.Rows(0)("csa_rate")) + clsCommon.myCdbl(diffrate))
                            End If
                        End If

                        If csaconversion <= 0 Then
                            csaconversion = 1
                        End If

                        orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                            grow.Cells(colUnitRate).Value = 0
                        Else
                            grow.Cells(colUnitRate).Value = orgrate
                        End If

                        grow.Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)
                    Else
                        If clsCommon.myCdbl(grow.Cells(colUnitRate).Value) <= 0 Then
                            grow.Cells(colUnitRate).Value = 0
                        End If
                    End If

                    If Apply_PriceChat_On_OtherItems AndAlso Not BookingEffectOnSale Then
                        grow.Cells(colbookingrate).Value = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                    End If
                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function LoadItemType(ByVal itemtype As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(itemtype, "R") = CompairStringResult.Equal Then
                str = "Raw Material"
            ElseIf clsCommon.CompairString(itemtype, "F") = CompairStringResult.Equal Then
                str = "Finished Good"
            ElseIf clsCommon.CompairString(itemtype, "S") = CompairStringResult.Equal Then
                str = "Semi Finished Good"
            ElseIf clsCommon.CompairString(itemtype, "A") = CompairStringResult.Equal Then
                str = "Asset"
            ElseIf clsCommon.CompairString(itemtype, "O") = CompairStringResult.Equal Then
                str = "Other"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub OpenAltUOM(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as Description,TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL "
        gv.CurrentRow.Cells(colItemUOM).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("UOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value), "Code", isButtonClicked))

        If Not BookingEffectOnSale Then
            gv.CurrentRow.Cells(colAltUOM).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value)
        End If

        Dim stockunit As String = ""
        stockunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select stocking_unit from TSPL_ITEM_UOM_DETAIL where uom_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemUOM).Value) + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"))

        CalConversionFactor_GV(gv.CurrentRow.Index)
        CalAltQty(gv.CurrentRow.Index)
        CommisionValue(gv.CurrentRow.Index)
        CalUnitPrice(CInt(gv.CurrentRow.Index), True)
    End Sub

    Private Sub CalConversionFactor_GV(ByVal CurrentRow As Integer)
        gv.Rows(CurrentRow).Cells(colConversionFactor).Value = 1

        If clsCommon.myLen(gv.Rows(CurrentRow).Cells(colItemUOM).Value) > 0 Then
            Dim altuomcnvrsn As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where uom_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colAltUOM).Value) + "' and item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "'"))
            Dim cnvrsn As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where uom_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colItemUOM).Value) + "' and item_code='" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colitemcode).Value) + "'"))

            If cnvrsn > 0 Then
                gv.Rows(CurrentRow).Cells(colConversionFactor).Value = System.Math.Round(altuomcnvrsn / cnvrsn, 2)
            Else
                gv.Rows(CurrentRow).Cells(colConversionFactor).Value = 0
            End If
        End If
        If clsCommon.myCdbl(gv.Rows(CurrentRow).Cells(colConversionFactor).Value) <= 0 Then
            gv.Rows(CurrentRow).Cells(colConversionFactor).Value = 1
        End If
    End Sub

    Private Sub OpenBooking(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gv.CurrentRow.Cells(coldate).Value) <= 0 Then
            Throw New Exception("Fill date first.")
        End If
        If clsCommon.myLen(gv.CurrentRow.Cells(colbookingtype).Value) <= 0 Then
            Throw New Exception("Select Row Type first.")
        End If

        Dim qry As String = ""
        Dim whrCls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value), "Group-Wise") = CompairStringResult.Equal Then
            qry = "select ROW_NUMBER() over(order by final.code) as sno,final.* from (select axa.Code,axa.DESCRIPTION,axa.[CSA Type],axa.Date,axa.[CSA Code],axa.CSA,(ISNULL(axa.book_qty,0)-ISNULL(axa.bal_qty,0)) as bal_qty,axa.BOOK_QTY_UOM from ("
            qry += " select TSPL_CSA_BOOKING_HEAD.booking_no as Code,TSPL_CSA_BOOKING_HEAD.Description,TSPL_CSA_BOOKING_DETAIL.CSA_ITEM_TYPE as [CSA Type],TSPL_CSA_BOOKING_HEAD.booking_date as [Date],TSPL_CSA_BOOKING_HEAD.csa_code as [CSA Code],tspl_customer_master.customer_name as [CSA],TSPL_CSA_BOOKING_DETAIL.book_qty,(aa.alt_qty) as Bal_Qty,TSPL_CSA_BOOKING_DETAIL.book_qty_uom"
            qry += " from TSPL_CSA_BOOKING_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_BOOKING_HEAD.csa_code"
            qry += " left outer join TSPL_CSA_BOOKING_DETAIL on TSPL_CSA_BOOKING_DETAIL.booking_no=TSPL_CSA_BOOKING_HEAD.booking_no"
            qry += " left outer join (select TSPL_SD_SALE_INVOICE_DETAIL.CSA_Booking_no,TSPL_SD_SALE_INVOICE_DETAIL.alt_uom,tspl_item_master.csa_type,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.alt_qty,0)) as alt_qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where csa_booking_type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value) + "' and DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + txtcustcode.Value + "' and document_code <>'" + clsCommon.myCstr(txtCode.Value) + "') and isnull(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,'0')<>'1' group by TSPL_SD_SALE_INVOICE_DETAIL.alt_uom,tspl_item_master.csa_type,TSPL_SD_SALE_INVOICE_DETAIL.CSA_Booking_no)aa"
            qry += " on TSPL_CSA_BOOKING_DETAIL.CSA_ITEM_TYPE= aa.csa_type and TSPL_CSA_BOOKING_DETAIL.book_qty_uom=aa.alt_uom and TSPL_CSA_BOOKING_DETAIL.BOOKING_NO=aa.CSA_Booking_no"
        Else
            qry = "select ROW_NUMBER() over(order by final.code) as sno,final.* from (select axa.Code,axa.item_code as Icode,axa.DESCRIPTION,axa.[CSA Type],axa.Date,axa.[CSA Code],axa.CSA,(ISNULL(axa.book_qty,0)-ISNULL(axa.bal_qty,0)) as bal_qty,axa.BOOK_QTY_UOM from ("
            qry += " select TSPL_CSA_BOOKING_HEAD.booking_no as Code,TSPL_CSA_BOOKING_HEAD.Description,TSPL_CSA_BOOKING_DETAIL.item_code,TSPL_CSA_BOOKING_DETAIL.CSA_ITEM_TYPE as [CSA Type],TSPL_CSA_BOOKING_HEAD.booking_date as [Date],TSPL_CSA_BOOKING_HEAD.csa_code as [CSA Code],tspl_customer_master.customer_name as [CSA],TSPL_CSA_BOOKING_DETAIL.book_qty,(aa.alt_qty) as Bal_Qty,TSPL_CSA_BOOKING_DETAIL.book_qty_uom"
            qry += " from TSPL_CSA_BOOKING_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_BOOKING_HEAD.csa_code"
            qry += " left outer join TSPL_CSA_BOOKING_DETAIL on TSPL_CSA_BOOKING_DETAIL.booking_no=TSPL_CSA_BOOKING_HEAD.booking_no"
            qry += " left outer join (select TSPL_SD_SALE_INVOICE_DETAIL.CSA_Booking_no,TSPL_SD_SALE_INVOICE_DETAIL.alt_uom,tspl_item_master.csa_type,sum(isnull(TSPL_SD_SALE_INVOICE_DETAIL.alt_qty,0)) as alt_qty,TSPL_SD_SALE_INVOICE_DETAIL.item_code from TSPL_SD_SALE_INVOICE_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code where csa_booking_type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value) + "' and DOCUMENT_CODE in (select DOCUMENT_CODE from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + txtcustcode.Value + "' and document_code <>'" + clsCommon.myCstr(txtCode.Value) + "') and isnull(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,'0')<>'1' group by TSPL_SD_SALE_INVOICE_DETAIL.item_code,TSPL_SD_SALE_INVOICE_DETAIL.alt_uom,tspl_item_master.csa_type,TSPL_SD_SALE_INVOICE_DETAIL.CSA_Booking_no)aa"
            qry += " on aa.item_code=TSPL_CSA_BOOKING_DETAIL.item_code and TSPL_CSA_BOOKING_DETAIL.CSA_ITEM_TYPE= aa.csa_type and TSPL_CSA_BOOKING_DETAIL.book_qty_uom=aa.alt_uom and TSPL_CSA_BOOKING_DETAIL.BOOKING_NO=aa.CSA_Booking_no"
        End If

        whrCls = " where TSPL_CSA_BOOKING_HEAD.booking_type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value) + "' and TSPL_CSA_BOOKING_HEAD.posted=1 and TSPL_CSA_BOOKING_HEAD.BOOKING_DATE <= '" + clsCommon.GetPrintDate(clsCommon.myCDate(gv.CurrentRow.Cells(coldate).Value), "dd/MMM/yyyy") + "' and TSPL_CSA_BOOKING_HEAD.csa_code='" + txtcustcode.Value + "')axa"
        whrCls += ") final " 'where final.bal_qty>0

        qry = qry + whrCls
        Dim sno As String = ""
        'sno = clsCommon.ShowSelectForm("CSABOOKFND", qry, "sno", " final.bal_qty>0", clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value), "sno", isButtonClicked)
        sno = customFinder.getFinder("CSABOOKFND", qry, " final.bal_qty>0", "Code", clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value), "sno")

        qry = "select * from  (" + qry + " where final.bal_qty>0)abb where sno='" + clsCommon.myCstr(sno) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        'Dim dr As DataRow = clsCommon.ShowSelectFormForRow("BOKFND", qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then 'If dr IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value), "Group-Wise") = CompairStringResult.Equal Then
                    gv.CurrentRow.Cells(colBookingno).Value = clsCommon.myCstr(dr("Code"))
                    gv.CurrentRow.Cells(colCSAType).Value = clsCommon.myCstr(dr("CSA Type"))
                    gv.CurrentRow.Cells(colbookingrate).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_Rate from TSPL_CSA_BOOKING_DETAIL where CSA_ITEM_TYPE='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colAltUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_QTY_UOM from TSPL_CSA_BOOKING_DETAIL where CSA_ITEM_TYPE='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colAltQty).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_QTY from TSPL_CSA_BOOKING_DETAIL where CSA_ITEM_TYPE='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colincludingtax).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TAX_PAID from TSPL_CSA_BOOKING_DETAIL where CSA_ITEM_TYPE='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colbalqty).Value = clsCommon.myCdbl(dr("Bal_Qty"))
                    gv.CurrentRow.Cells(colqty).Value = "0"
                    If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colincludingtax).Value), "Yes") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colTaxBasis).Value = "Back Calculation"
                    Else
                        gv.CurrentRow.Cells(colTaxBasis).Value = "Forward Calculation"
                    End If
                    gv.CurrentRow.Cells(coldoubleclick).Value = "Double Click"
                Else
                    gv.CurrentRow.Cells(colBookingno).Value = clsCommon.myCstr(dr("Code"))
                    gv.CurrentRow.Cells(colCSAType).Value = clsCommon.myCstr(dr("CSA Type"))
                    gv.CurrentRow.Cells(colitemcode).Value = clsCommon.myCstr(dr("Icode"))
                    gv.CurrentRow.Cells(colqty).Value = "0"
                    gv.CurrentRow.Cells(colbookingrate).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_Rate from TSPL_CSA_BOOKING_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colAltUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_QTY_UOM from TSPL_CSA_BOOKING_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colAltQty).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BOOK_QTY from TSPL_CSA_BOOKING_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colincludingtax).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TAX_PAID from TSPL_CSA_BOOKING_DETAIL where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and BOOKING_NO='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "'"))
                    gv.CurrentRow.Cells(colbalqty).Value = clsCommon.myCdbl(dr("Bal_Qty"))
                    If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colincludingtax).Value), "Yes") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colTaxBasis).Value = "Back Calculation"
                    Else
                        gv.CurrentRow.Cells(colTaxBasis).Value = "Forward Calculation"
                    End If

                    gv.CurrentRow.Cells(coliname).Value = clsCommon.myCstr(clsItemMaster.GetItemName(dr("Icode"), Nothing))
                    gv.CurrentRow.Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Icode")), Nothing)
                    gv.CurrentRow.Cells(colItemType).Value = LoadItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'")))
                    gv.CurrentRow.Cells(colItemUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"))
                    gv.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(clsCommon.myCstr(dr("icode")))

                    ''==========================================
                    If ExciseentryOnSale Then
                        Dim Item_TaxType As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in ('" + clsCommon.myCstr(dr("icode")) + "')"))
                        If clsLocation.isLocatinExcisable(txt_loc_code.Value) = True AndAlso Item_TaxType = 2 Then
                            cmbEXType.SelectedValue = "E"
                        Else
                            cmbEXType.SelectedValue = "N"
                        End If
                        cmbEXType.Enabled = False
                    End If
                    ''=============================

                    CalUnitPrice(CInt(gv.CurrentRow.Index), True)
                    gv.CurrentRow.Cells(coldoubleclick).Value = "Double Click"
                    gv.CurrentRow.Cells(colPackSize).Value = 1
                    gv.CurrentRow.Cells(colMasterPackSize).Value = 1
                    If clsCommon.myCdbl(gv.CurrentRow.Cells(colPackSize).Value) > 0 Then
                        gv.CurrentRow.Cells(colPackSize).ReadOnly = True
                    Else
                        gv.CurrentRow.Cells(colPackSize).ReadOnly = False
                    End If

                    If ApplyFreight_Cmmsn_Charge_Itemwise Then
                        qry = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Type,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Type " & _
                            " from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No " & _
                            " where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(txtcustcode.Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"
                        Dim dt_CM As DataTable = clsDBFuncationality.GetDataTable(qry)

                        If dt_CM IsNot Nothing AndAlso dt_CM.Rows.Count > 0 Then
                            gv.CurrentRow.Cells(colCommision).Value = clsCommon.myCdbl(dt_CM.Rows(0)("Commission_Rate"))
                            gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(dt_CM.Rows(0)("Commission_Type"))
                            gv.CurrentRow.Cells(colFrghtRate).Value = clsCommon.myCdbl(dt_CM.Rows(0)("Freight_Rate"))
                            gv.CurrentRow.Cells(colFrghtType).Value = clsCommon.myCstr(dt_CM.Rows(0)("Freight_Type"))
                        End If

                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) > 0 OrElse clsCommon.myCdbl(gv.CurrentRow.Cells(colFrghtRate).Value) > 0 Then
                            CommisionValue(gv.CurrentRow.Index)
                        End If
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) <= 0 Then
                            gv.CurrentRow.Cells(colCommision).ReadOnly = False
                        End If

                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colFrghtRate).Value) <= 0 Then
                            gv.CurrentRow.Cells(colFrghtRate).ReadOnly = False
                        End If
                    Else
                        gv.CurrentRow.Cells(colCommision).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select CSA_Commision_Rate from TSPL_LOCATION_MASTER where Cust_Code='" + txtcustcode.Value + "' and location_code='" + txtCSAloc_code.Value + "'"))
                        gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(CSA_Commission_RS_PERS,'R') from TSPL_LOCATION_MASTER where Cust_Code='" + txtcustcode.Value + "' and location_code='" + txtCSAloc_code.Value + "'"))
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colCommision).Value) > 0 Then
                            CommisionValue(gv.CurrentRow.Index)
                        Else
                            gv.CurrentRow.Cells(colCommision).ReadOnly = False
                        End If
                    End If
                    ''===========================================

                    gv.CurrentRow.Cells(colConversionFactor).Value = 1
                    SetitemWiseTaxSetting(True, True)
                    UpdateCurrentRow(gv.CurrentRow.Index)
                    CommisionValue(gv.CurrentRow.Index)
                    UpdateAllTotals()

                    gv.CurrentRow.Cells(colSchmCode).Value = Nothing
                    gv.CurrentRow.Cells(colSchmCodeType).Value = Nothing
                    gv.CurrentRow.Cells(colMainIcode).Value = ""
                    gv.CurrentRow.Cells(colMainLineNo).Value = "0"
                    gv.CurrentRow.Cells(colMainIQty).Value = "0"
                    gv.CurrentRow.Cells(colMainIUOM).Value = ""
                    gv.CurrentRow.Cells(colFOC).Value = "N"
                    gv.CurrentRow.Cells(colIsSchmItem).Value = ""

                    '>>>>>>>>>>>>>>>scheme================================
                    FillFreeItemsInGrid()
                End If
            Next

        Else
            For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value)) = CompairStringResult.Equal Then
                    gv.Rows.RemoveAt(ii)
                End If
            Next
            '=before deleting main row first have to delete scheme items rows.(above)

            gv.CurrentRow.Cells(colBookingno).Value = ""
            gv.CurrentRow.Cells(colbookingtype).Value = ""
            gv.CurrentRow.Cells(colCSAType).Value = ""
            gv.CurrentRow.Cells(colincludingtax).Value = ""
            gv.CurrentRow.Cells(colbookingrate).Value = Nothing
            gv.CurrentRow.Cells(colAltUOM).Value = ""
            gv.CurrentRow.Cells(colAltQty).Value = Nothing
            gv.CurrentRow.Cells(colTaxBasis).Value = ""
            gv.CurrentRow.Cells(colbalqty).Value = Nothing
            gv.CurrentRow.Cells(coldoubleclick).Value = "Double Click"
            gv.CurrentRow.Cells(colitemcode).Value = ""
            gv.CurrentRow.Cells(coliname).Value = ""
            gv.CurrentRow.Cells(colHSNCode).Value = ""
            gv.CurrentRow.Cells(colConversionFactor).Value = Nothing
            gv.CurrentRow.Cells(colItemType).Value = ""
            gv.CurrentRow.Cells(colItemUOM).Value = ""
            gv.CurrentRow.Cells(colUnitRate).Value = Nothing
            gv.CurrentRow.Cells(colPackSize).Value = Nothing
            gv.CurrentRow.Cells(colMasterPackSize).Value = Nothing
            gv.CurrentRow.Cells(colCommision).Value = Nothing
            gv.CurrentRow.Cells(colCommision).ReadOnly = False
            gv.CurrentRow.Cells(colComm_Type_RS_Pers).Value = Nothing
            gv.CurrentRow.Cells(colFrghtAmt).Value = Nothing
            gv.CurrentRow.Cells(colFrghtRate).ReadOnly = False
            gv.CurrentRow.Cells(colFrghtType).Value = Nothing
            gv.CurrentRow.Cells(colFrghtRate).Value = Nothing
            gv.CurrentRow.Cells(colIsBatchItem).Value = False
            gv.CurrentRow.Cells(colPackSize).ReadOnly = False
            gv.CurrentRow.Cells(colSchmCode).Value = Nothing
            gv.CurrentRow.Cells(colSchmCodeType).Value = Nothing
            gv.CurrentRow.Cells(colMainIcode).Value = ""
            gv.CurrentRow.Cells(colMainLineNo).Value = "0"
            gv.CurrentRow.Cells(colMainIQty).Value = "0"
            gv.CurrentRow.Cells(colMainIUOM).Value = ""
            gv.CurrentRow.Cells(colFOC).Value = "N"
            gv.CurrentRow.Cells(colIsSchmItem).Value = ""
            gv.CurrentRow.Cells(colCashSchemeCode).Value = Nothing
            gv.CurrentRow.Cells(colCashSchemeType).Value = Nothing
            gv.CurrentRow.Cells(colCash_Amt).Value = "0"
            gv.CurrentRow.Cells(colCash_Pers).Value = "0"
        End If
    End Sub

#Region "Scheme Items"
    Private Sub FillFreeItemsInGrid()
        Dim Index As Integer = gv.CurrentRow.Index
        Try
            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then

                For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colLinenno).Value)) = CompairStringResult.Equal Then
                        gv.Rows.RemoveAt(ii)
                    End If
                Next

                gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                gv.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv.Rows(Index).Cells(colMainIcode).Value = Nothing
                gv.Rows(Index).Cells(colMainLineNo).Value = "0"
                gv.Rows(Index).Cells(colMainIQty).Value = Nothing
                gv.Rows(Index).Cells(colMainIUOM).Value = Nothing
                gv.Rows(Index).Cells(colFOC).Value = "N"
                gv.Rows(Index).Cells(colIsSchmItem).Value = "N"
                gv.CurrentRow.Cells(colCashSchemeCode).Value = Nothing
                gv.CurrentRow.Cells(colCashSchemeType).Value = Nothing
                gv.CurrentRow.Cells(colCash_Amt).Value = "0"
                gv.CurrentRow.Cells(colCash_Pers).Value = "0"

                RefreshSerialNo()
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then
                    For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colLinenno).Value)) = CompairStringResult.Equal Then
                            gv.Rows.RemoveAt(ii)
                        End If
                    Next
                End If
            End If

            If clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value) <= 0 Then
                Exit Sub
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "N") <> CompairStringResult.Equal Then
                Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv.Rows(Index).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colItemUOM).Value), clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value), txtcustcode.Value)
                If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then
                    gv.Rows(Index).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                    gv.Rows(Index).Cells(colCashSchemeType).Value = obj_Cash.schm_Type
                    gv.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    gv.Rows(Index).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers

                    If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                        gv.Rows(Index).Cells(colCashSchemeType).Value = "P"
                        gv.Rows(Index).Cells(colCash_Amt).Value = (clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value) * clsCommon.myCdbl(gv.Rows(Index).Cells(colUnitRate).Value) * obj_Cash.Cash_Pers) / 100
                    Else
                        gv.Rows(Index).Cells(colCashSchemeType).Value = "A"
                        gv.Rows(Index).Cells(colCash_Pers).Value = (100 * clsCommon.myCdbl(obj_Cash.Cash_Amt)) / (clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value) * clsCommon.myCdbl(gv.Rows(Index).Cells(colUnitRate).Value))
                        gv.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    End If

                    gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                Else
                    gv.Rows(Index).Cells(colCashSchemeCode).Value = ""
                    gv.Rows(Index).Cells(colCashSchemeType).Value = ""
                    gv.Rows(Index).Cells(colCash_Amt).Value = 0
                    gv.Rows(Index).Cells(colCash_Pers).Value = 0
                    If clsCommon.myLen(gv.Rows(Index).Cells(colSchmCode).Value) > 0 Then
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    Else
                        gv.Rows(Index).Cells(colIsSchmItem).Value = ""
                    End If
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "N") <> CompairStringResult.Equal Then

                Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv.Rows(Index).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colItemUOM).Value), clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value), txtcustcode.Value, clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value))
                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                    For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                        '--------------update free itemcode in main item row------------------
                        gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                        gv.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                        gv.Rows(Index).Cells(colMainIcode).Value = objtr.Schm_Icode
                        gv.Rows(Index).Cells(colMainIQty).Value = objtr.Schm_Qty
                        gv.Rows(Index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                        gv.Rows(Index).Cells(colFOC).Value = "N"
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        '-------------------------------------------------------------

                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colBookingno).Value = ""
                        gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr.Schm_Item_CSA_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colbookingrate).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colAltUOM).Value = ""
                        gv.Rows(gv.Rows.Count - 1).Cells(colAltQty).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colbalqty).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(coldoubleclick).Value = "Double Click"
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.Schm_Icode
                        gv.Rows(gv.Rows.Count - 1).Cells(coliname).Value = objtr.Schm_Iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objtr.Schm_Icode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = LoadItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.Schm_Icode + "'")))
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemUOM).Value = objtr.Schm_Item_Uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnitRate).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colPackSize).Value = 1
                        gv.Rows(gv.Rows.Count - 1).Cells(colMasterPackSize).Value = 1
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommision).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommisionValue).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtRate).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtAmt).Value = "0"
                        gv.Rows(gv.Rows.Count - 1).Cells(colqty).Value = objtr.Schm_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colConversionFactor).Value = 1
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colitemcode).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainLineNo).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colLinenno).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = clsCommon.myCdbl(gv.Rows(Index).Cells(colqty).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colItemUOM).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = "Y"
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Schm_Icode)
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBasis).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colTaxBasis).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colincludingtax).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colincludingtax).Value)

                        gv.Rows(gv.Rows.Count - 1).Cells(colBookingno).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(coldate).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colbookingtype).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colMasterPackSize).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colPackSize).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemUOM).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colqty).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colincludingtax).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnitRate).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommision).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colCommisionValue).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtRate).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrghtAmt).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherCharge).ReadOnly = True

                        gv.Rows.Move(gv.Rows.Count - 1, Index + 1)

                        ''==========for knocking free item===================
                        RefreshSerialNo()
                        gv.CurrentRow = gv.Rows(gv.Rows.Count - 1) ''free row index
                        FillTransferStockData(False)
                        gv.CurrentRow = gv.Rows(Index)
                        ''========================================================
                        SetitemWiseTaxSetting(True, True)
                        UpdateCurrentRow(gv.Rows.Count - 1)
                        CommisionValue(gv.Rows.Count - 1)
                        UpdateAllTotals()
                    Next
                Else
                    gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                    gv.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                    gv.Rows(Index).Cells(colMainIcode).Value = Nothing
                    gv.Rows(Index).Cells(colMainLineNo).Value = "0"
                    gv.Rows(Index).Cells(colMainIQty).Value = Nothing
                    gv.Rows(Index).Cells(colMainIUOM).Value = Nothing
                    gv.Rows(Index).Cells(colFOC).Value = "N"
                    gv.Rows(Index).Cells(colIsSchmItem).Value = "N"
                    If clsCommon.myLen(gv.Rows(Index).Cells(colCashSchemeCode).Value) > 0 Then
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    End If
                End If
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        gv.CurrentRow = gv.Rows(Index)
        RefreshSerialNo()
    End Sub

    Sub RefreshSerialNo()
        Dim intSerialNo As Integer = 0
        For intCount As Integer = 0 To gv.Rows.Count - 1
            intSerialNo += 1
            gv.Rows(intCount).Cells(colLinenno).Value = clsCommon.myCstr(intSerialNo)
        Next
    End Sub
#End Region

    Private Sub gvAC_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
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
                isCellValueChanged = False
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

    Private Sub gv2_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    'gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = True ' rbtnTaxCalAutomatic.IsChecked
                End If

                Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                cell.GradientStyle = GradientStyles.Solid
                cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChanged = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                isInsideLoadData = True

                Dim tax_category As String = clsCSATransfer.GetState_Inter_Local(txtcustcode.Value, txt_loc_code.Value)
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim qry As String = "select TSPL_LOCATION_WISE_TAX_MASTER.tax_code as [Rate Code],TSPL_TAX_MASTER.Tax_code_Desc as [Rate Description],TSPL_LOCATION_WISE_TAX_MASTER.Tax_Rate as [Rate] from TSPL_LOCATION_WISE_TAX_MASTER left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.tax_code=TSPL_LOCATION_WISE_TAX_MASTER.tax_code "
                'Dim whrcls As String = " TSPL_LOCATION_WISE_TAX_MASTER.tax_category='" + tax_category + "' and TSPL_LOCATION_WISE_TAX_MASTER.tax_group_code='" + txtTaxGroup.Value + "' and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Type='S' and TSPL_LOCATION_WISE_TAX_MASTER.location_code='" + txtCSAloc_code.Value + "'"
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txt_loc_code.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtcustcode.Value, "S", OpenALLTaxes) 'clsCommon.myCdbl(clsCommon.ShowSelectForm("CSATRANSFND", qry, "Rate", whrcls, "", "", True)) 'clsCommon.myCdbl(clsCommon.ShowSelectForm("CSASALETAXFND", qry, "Rate", whrcls, "", "", True))
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv.Rows.Count - 1
                        If clsCommon.CompairString(strII, "1") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate1).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "2") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate2).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "3") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate3).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "4") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate4).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "5") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate5).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "6") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate6).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "7") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate7).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "8") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate8).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "9") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate9).Value = dblNewRate
                        End If
                        If clsCommon.CompairString(strII, "10") = CompairStringResult.Equal Then
                            gv.Rows(ii).Cells(colTaxRate10).Value = dblNewRate
                        End If
                    Next
                End If

                For ii As Integer = 0 To gv.Rows.Count - 1
                    CalAltQty(ii)
                    CalConversionFactor_GV(ii)
                    CalUnitPrice(ii, True)
                    CommisionValue(ii)
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkDiscountOnAmt_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDiscountOnAmt.ToggleStateChanged
        If chkDiscountOnAmt.IsChecked Then
            txtDiscAmt.Enabled = True
            txtDiscPer.Enabled = False
            txtDiscPer.Text = 0
            lblInvoiceDiscAmt.Text = 0
        Else
            txtDiscAmt.Enabled = False
            txtDiscPer.Enabled = True
            txtDiscAmt.Text = 0
            lblInvoiceDiscAmt.Text = 0
        End If
    End Sub

    Private Sub CalculateDiscountAmount()
        If clsCommon.myCdbl(txtDiscAmt.Text) > clsCommon.myCdbl(lblAmtWithDiscount.Text) Then
            isCellValueChanged = False
            Throw New Exception("Discount amount cannot be greater than Doc amount")
        End If
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

        If chkDiscountOnAmt.IsChecked Then
            For Each gro As GridViewRowInfo In gv.Rows
                'gv.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colitemcode).Value) > 0 Then

                    If clsCommon.myCdbl(lblAmtWithDiscount.Text) > 0 Then
                        dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colSaleValue).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(lblAmtWithDiscount.Text), 2)
                    Else
                        dblDiscountAmt = 0
                    End If

                    'gro.Cells(colHeadDiscamt).Value = Math.Round((dblDiscountAmt), 2)
                Else
                    'gro.Cells(colHeadDiscamt).Value = 0
                End If

            Next
        Else
            For Each gro As GridViewRowInfo In gv.Rows
                'gv1.CurrentRow = gro.Cells(colHeadDiscamt).RowInfo
                If clsCommon.myLen(gro.Cells(colitemcode).Value) > 0 Then
                    'gro.Cells(colHeaDDisPer).Value = Math.Round((discountrate), 2)
                Else

                End If
            Next
        End If
    End Sub

    Private Function GetBaseTaxAmount(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 0 To intEndCol
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAutCode).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
            End If
        Next
        Return 0
    End Function

    Private Sub txtDiscAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Leave
        CalculateDiscountAmount()
    End Sub

    Private Sub txtDiscPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave
        CalculateDiscountAmount()
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If clsCommon.myCdbl(gv.CurrentRow.Cells(colLinenno).Value) <= 0 Then
                If gv.Rows.Count > 0 AndAlso gv.CurrentRow.Index > 0 Then
                    intCurrRow = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index - 1).Cells(colLinenno).Value)
                End If
                gv.CurrentRow.Cells(colLinenno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            End If

            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub FrmCSASaleInvoice_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DropTable()
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub isValid_CashScheme()

        Dim scheme_Code As String = ""
        Dim isSchemeApply As String = ""
        Dim cash_scheme_code As String = ""
        Dim cash_amt As Decimal = 0
        Dim amount As Decimal = 0

        For Each grow As GridViewRowInfo In gv.Rows
            isSchemeApply = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
            scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
            cash_scheme_code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
            cash_amt = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
            amount = clsCommon.myCdbl(grow.Cells(colUnitRate).Value) * clsCommon.myCdbl(grow.Cells(colqty).Value)

            '================if cash scheme amount exceed total basic amount than scheme not applicable.
            If cash_amt > amount Then
                grow.Cells(colCash_Amt).Value = 0
                grow.Cells(colCash_Pers).Value = 0
                grow.Cells(colCashSchemeCode).Value = Nothing
                grow.Cells(colCashSchemeType).Value = Nothing

                If clsCommon.myLen(scheme_Code) <= 0 Then
                    grow.Cells(colIsSchmItem).Value = Nothing
                    grow.Cells(colSchmCodeType).Value = Nothing
                End If
            End If
        Next
    End Sub

    Private Sub txttrans_loc_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txt_loc_code._MYValidating
        Dim whrCls As String = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N' "

        If clsCommon.myLen(arrLoc) > 0 Then
            whrCls += " and tspl_location_master.location_code in (" + arrLoc + ")"
        End If

        txt_loc_code.Value = clsLocation.getFinder(whrCls, txt_loc_code.Value, isButtonClicked)

        If clsCommon.myLen(txt_loc_code.Value) > 0 Then
            txt_loc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txt_loc_code.Value + "'"))
            isInsideLoadData = True
            CalUnitPrice(0, False)
            For ii As Integer = 0 To gv.Rows.Count - 1
                GetTransferRate(ii)
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
            isInsideLoadData = False
        Else
            txt_loc_code.Value = ""
            txt_loc_name.Text = ""
        End If
    End Sub

    Private Sub btnsavelayout1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavelayout1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
        End If
    End Sub

    Private Sub btndeletelayout1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletelayout1.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Delete layout successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub gv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv.DoubleClick
        Try
            If gv.CurrentColumn Is gv.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value)
                frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
                frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(coliname).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv.CurrentRow.Cells(colAmtAfterDis).Value)

                ''===================================================================
                If ExciseentryOnSale AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colTaxBasis).Value), "Backward Calculation") = CompairStringResult.Equal Then
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv.CurrentRow.Cells(colAbatementAmt).Value) - (clsCommon.myCdbl(gv.CurrentRow.Cells(colOtherCharge).Value) * 2) ''because in back calc. other charge is minus and in common form other charge is always '+' ,so minus twice from here and other charge add from po form get equal result
                ElseIf ExciseentryOnSale AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv.CurrentRow.Cells(colAbatementAmt).Value)
                End If
                ''===================================================================

                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = txt_loc_code.Value
                frm.strTaxType = "S"
                frm.Without_State_Condition = OpenALLTaxes
                frm.strVendorCustomerCode = txtcustcode.Value

                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv.CurrentRow.Cells("tax" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gv.CurrentRow.Cells("taxrate" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv.CurrentRow.Cells("taxbaseamt" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv.CurrentRow.Cells("tacamt" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gv.CurrentRow.Cells("surtax" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gv.CurrentRow.Cells("surtaxcode" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gv.CurrentRow.Cells("taxable" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gv.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv.CurrentRow.Cells("tax" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv.CurrentRow.Cells("taxrate" + strii).Value = frm.ArrOut(ii).Rate
                            gv.CurrentRow.Cells("taxbaseamt" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gv.CurrentRow.Cells("tacamt" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gv.CurrentRow.Cells("surtax" + strii).Value = frm.ArrOut(ii).isSurTax
                            gv.CurrentRow.Cells("surtaxcode" + strii).Value = frm.ArrOut(ii).SurTax
                            gv.CurrentRow.Cells("taxable" + strii).Value = frm.ArrOut(ii).IsTaxable
                        Next
                        gv.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax

                        isValid_CashScheme()
                        CalAltQty(gv.CurrentRow.Index)
                        CalConversionFactor_GV(gv.CurrentRow.Index)
                        CalUnitPrice(gv.CurrentRow.Index, True)
                        CommisionValue(gv.CurrentRow.Index)
                        UpdateCurrentRow(gv.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_KeyDown(sender As Object, e As KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.F5 Then
            If TryCast(gv.CurrentRow.Cells(coldoubleclick).Tag, ArrayList) Is Nothing OrElse TryCast(gv.CurrentRow.Cells(coldoubleclick).Tag, ArrayList).Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select transfer detail first.", Me.Text)
                Exit Sub
            End If
            '======update by preeti gupta 17/10/2018
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
        If clsCommon.myLen(gv.CurrentRow.Cells(colitemcode).Value) > 0 Then
            arr = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(Me, strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If Not btnpost.Enabled AndAlso Not btnsave.Enabled AndAlso Not btndelete.Enabled Then
                    e.Cancel = True
                    Throw New Exception("No row deleted because data is posted")
                End If
                If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                    e.Cancel = True
                    Throw New Exception("Free item cannot deleted.")
                End If
                e.Cancel = False

                'Dim qry As String = "delete from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code='" + txtCode.Value + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and line_no='" + clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value) + "' "
                'If BookingEffectOnSale Then
                '    qry += " and csa_booking_no='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBookingno).Value) + "' and CSA_Booking_Type='" + clsCommon.myCstr(gv.CurrentRow.Cells(colbookingtype).Value) + "' "
                'End If
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'qry = "delete from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code='" + txtCode.Value + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and line_no='" + clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value) + "'"
                'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'", trans)) > 0 Then
                '    clsDBFuncationality.ExecuteNonQuery("delete from CSA_SALE_TRANSFER where document_code='" + txtCode.Value + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "' and line_no='" + clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value) + "'", trans)
                'End If

                For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colLinenno).Value)) = CompairStringResult.Equal Then
                        gv.Rows.RemoveAt(ii)
                    End If
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Invoice not found to Print", Me.Text)
            txtCode.Focus()
            txtCode.Select()
        Else
            'clsCommon.ProgressBarShow()
            Dim dt As DataTable = New DataTable()
            dt = funPrint(txtCode.Value)
            'Dim dt1 As DataTable = New DataTable()
            'dt1 = funPrintTransfer()

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    NewSalesReportViewer.funsubreportWithdt(dt, dt1, "crptSaleInvoiceCSA", "CSA Sales Invoice", "CsaTransfer")
            'End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptSaleInvoiceCSA", "Sale Invoice CSA", clsCommon.myCstr(dt.Rows(0)("Document_Date")))
                frmCRV = Nothing
            End If
            'clsCommon.ProgressBarHide()
        End If
    End Sub
    Public Function funPrint(ByVal StrCode As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            ' Dim strDocNo As String = txtCode.Value
            Dim strDocNo As String = StrCode
            Dim dtBarCode As New DataTable
            dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
            Dim bytes() As Byte
            Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(StrCode, 1, False).[GetType]())
            bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(StrCode, 1, False), GetType(Byte())), Byte())

            Dim QryShowStatus As String = ""
            Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
            If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
                QryShowStatus = " ,(case when TSPL_SD_SALE_INVOICE_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as InvStatus "
            Else
                QryShowStatus = ""
            End If

            Dim SerialNo As String = ""
            Dim SerialNoColumn As String = ""
            Dim ShowSerialNoForSales As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowSerialNoForSales' And Type ='ShowSerialNoForSales'")
            If clsCommon.CompairString(clsCommon.myCstr(ShowSerialNoForSales), "1") = CompairStringResult.Equal Then
                SerialNoColumn = ",1 As SerialNoText , aa.Serial_No As [SerialNo] "
                'SerialNo = " left outer join TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL  on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  =TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code And TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.IS_Principle=1 ANd TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Location_Code =TSPL_SD_SALE_INVOICE_DETAIL.Location   "
                SerialNo = " left outer join (select distinct Doc_No,Serial_No,Main_Item_Code,Location_Code from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL WHERE Is_principle='1' AND ISNULL(Serial_No,'')<>'' and Doc_No in (select Doc_No from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where Status='1'))aa  on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  =AA.Main_Item_Code  ANd aa.Location_Code =TSPL_SD_SALE_INVOICE_DETAIL.Location  "
            Else
                SerialNoColumn = " ,0 As SerialNoText "
                SerialNo = ""
            End If

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

            'clsFixedParameter()
            Dim Qry As String = "  select isnull(TSPL_ITEM_MASTER.HSN_Code,'') as HSN_Code,axa .Scheme_Item_Code ,axa.Unit_code as Scheme_Unit_Code,axa.qty as Scheme_Qty,TSPL_COMPANY_MASTER .Tin_No as Comp_TinNo,TSPL_COMPANY_MASTER.CST_LST as comp_Cst_Lst,TSPL_COMPANY_MASTER.Pan_No as Comp_PANNo,TSPL_LOCATION_MASTER .Location_Desc,TSPL_SD_SALE_INVOICE_HEAD.Created_By ,TSPL_SD_SALE_INVOICE_HEAD.Modify_By ,"
            Qry += "   case when coalesce(p_cust.P_cust_code,'')='' then Cust_State  .GST_STATE_Code    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_STATE_Code  end as P_Cust_GST_State_code,Cust_State  .GST_STATE_Code as Cust_GST_State_code ,    case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .GSTNO    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Cust_GST_IN   end as P_Cust_GST_IN_no,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GST_In_No,"
            Qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No    end as P_CSTNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Lst_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode, case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_city_name,'')<>'' then p_cust .P_City_Name    end as P_City_Name, case when coalesce(p_cust.P_cust_code,'')='' then     case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,TSPL_CUSTOMER_MASTER.Cust_Code ,  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End  as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER_For_Customer.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,"
            Qry += " Total_Add_Charge,TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt,'" + strSoNo + "' as SONo, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate,   Loc_State_Master.GST_STATE_Code as Loc_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GST_INNO,TSPL_LOCATION_MASTER.Add1 as Location_Add1,TSPL_LOCATION_MASTER.Add1 as [Location Address],TSPL_LOCATION_MASTER.Add2 as Location_Add2,TSPL_LOCATION_MASTER.Add3 as Location_Add3,TSPL_LOCATION_MASTER.Add4 as Location_Add4,TSPL_LOCATION_MASTER.TIN_No ,ISNULL(TSPL_LOCATION_MASTER.Phone1,'')+ Case When ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Location_Phone, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as shipName, TSPL_SHIP_TO_LOCATION.Ship_To_Desc as shipName, TSPL_SHIP_TO_LOCATION.add1 as ship_Add1, TSPL_SHIP_TO_LOCATION.Add2 as ship_add2 ,TSPL_SHIP_TO_LOCATION.Add3 as ship_add3  ,TSPL_SHIP_TO_LOCATION.Pin_Code,TSPL_CITY_MASTER.STATE_CODE  ,TSPL_CITY_MASTER.City_Name,"
            Qry += "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SD_SALE_INVOICE_HEAD.Inv_No, TSPL_SD_SALE_INVOICE_HEAD.Dept_Desc , TSPL_SD_SALE_INVOICE_HEAD.Remarks ,  TSPL_SD_SALE_INVOICE_HEAD.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc as Term_Desc,TSPL_SD_SALE_INVOICE_HEAD.VehicleNo,TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as Vehicle_Desc , "
            Qry += " TSPL_SD_SALE_INVOICE_DETAIL .Specification as  specification,   TSPL_SD_SALE_INVOICE_HEAD.Document_Code as DocNo , TSPL_SD_SALE_INVOICE_HEAD.Description,TSPL_SD_SALE_INVOICE_HEAD.CUST_PO_NO,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.CUST_PO_DATE,103) as CUST_PO_DATE, "
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Description,convert(varchar, TSPL_SD_SALE_INVOICE_HEAD .Document_Date,103) as Document_Date , TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No, TSPL_SD_SALE_INVOICE_HEAD.Item_Type ,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, "
            Qry += " TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.add2 as customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as customer_Add3 ,TSPL_CUSTOMER_MASTER.State as customer_city_State ,TSPL_CUSTOMER_MASTER.PIN_Code as Customer_Pin_Code,COALESCE(TSPL_SHIP_TO_LOCATION.CST_No,TSPL_CUSTOMER_MASTER.CST) as Cust_CST_No,COALESCE(TSPL_SHIP_TO_LOCATION.Tin_No,TSPL_CUSTOMER_MASTER.Tin_No)as Cust_Tin_No,TSPL_CUSTOMER_MASTER.Contact_Person_Name as Cust_Contact_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as Cust_Contact_Number , TSPL_SD_SALE_INVOICE_HEAD .Terms_Code as termscode ,TSPL_SD_SALE_INVOICE_HEAD .Ref_No as ref_no ,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD .Comments as comments ,TSPL_SD_SALE_INVOICE_HEAD.Status ,TSPL_SD_SALE_INVOICE_HEAD.On_Hold ,TSPL_SD_SALE_INVOICE_HEAD.Comp_Code ,TSPL_SD_SALE_INVOICE_HEAD.Due_Date ,TSPL_SD_SALE_INVOICE_HEAD.Posting_Date ,TSPL_SD_SALE_INVOICE_HEAD.Carrier ,TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.GENo ,TSPL_SD_SALE_INVOICE_HEAD.GEDate ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Code1 ,TSPL_SD_SALE_INVOICE_HEAD.Add_Charge_Name1 ,TSPL_SD_SALE_INVOICE_HEAD.Challan_No ,TSPL_SD_SALE_INVOICE_HEAD.Challan_Date ,TSPL_SD_SALE_INVOICE_HEAD.Inv_Date ,TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE ,TSPL_SD_SALE_INVOICE_HEAD.ConvRate ,TSPL_SD_SALE_INVOICE_HEAD.ApplicableFrom ,TSPL_SD_SALE_INVOICE_HEAD.Against_C_Form ,TSPL_SD_SALE_INVOICE_HEAD.CFormApplied ,TSPL_SD_SALE_INVOICE_HEAD.CFormRecd ,TSPL_SD_SALE_INVOICE_HEAD.PROJECT_ID ,TSPL_SD_SALE_INVOICE_HEAD.Price_code ,TSPL_SD_SALE_INVOICE_HEAD.Route_No ,TSPL_SD_SALE_INVOICE_HEAD.Route_Desc ,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Per ,TSPL_SD_SALE_INVOICE_HEAD.HeadDisc_Amt ,TSPL_SD_SALE_INVOICE_HEAD.TotCashDiscAmt ,  TSPL_SD_SALE_INVOICE_HEAD .Discount_Amt as dis_amt,(case when Scheme_Item='Y' then (TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt+TSPL_SD_SALE_INVOICE_DETAIL.Amount) else (TSPL_SD_SALE_INVOICE_DETAIL.Disc_Amt) end) as dis_amt1,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_SD_SALE_INVOICE_HEAD .Total_Amt as Total_amount,"
            Qry += " TSPL_SD_SALE_INVOICE_HEAD.Discount_Base as bfrdisc_amount,TSPL_LOCATION_MASTER.City_Code  as Location_City_Code,TSPL_LOCATION_MASTER.State as Location_State,TSPL_LOCATION_MASTER.Pin_Code as Location_Pin_Code,TSPL_LOCATION_MASTER.Country as Location_Country,TSPL_LOCATION_MASTER.Email as Location_Email,Location_Type ,Loc_Status ,Status_Date   as Location_Status_Date,TSPL_LOCATION_MASTER.Excisable as Location_Excisable,Loc_Segment_Code ,TSPL_LOCATION_MASTER.Type as Location_Type,Purchase_Tax_Group as Location_Purchase_Tax_Group,Sales_Tax_Group as Location_Sales_Tax_Group,Ecc_Number  as Location_Ecc_Number,Registration_Number as Location_Registration_Number ,Commissionerate as Location_Commissionerate ,Range_Code as Location_Range_Code ,Range_Name as Location_Range_Name ,Range_Address as Location_Range_Address,Division_Code as Location_Division_Code,Division_Name as Location_Division_Name,Division_Address as Location_Division_Address,TSPL_LOCATION_MASTER.TAN_No as Location_TAN_No,TSPL_LOCATION_MASTER.TCAN_No as Location_TCAN_No,Service_Tax_Reg_No as Location_Service_Tax_Reg_No,DutyPaid as Location_DutyPaid,Purchase_Tax_GroupIS as Location_Purchase_Tax_GroupIS,Sales_Tax_GroupIS as Location_Sales_Tax_GroupIS,Stock_Transfer_Filled_Ac as Location_Stock_Transfer_Filled_Ac,GIT_Location as Location_GIT_Location,GIT_Type as Location_GIT_Type,TSPL_LOCATION_MASTER.CST_No as Location_CST_No,TSPL_LOCATION_MASTER.Telphone as Location_PhoneNo,TSPL_LOCATION_MASTER.TAN_No as Location_FaxNo , "
            Qry += " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax1_amt,0) as txt1amt,  "
            Qry += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax2_amt,0) as txt2amt,  "
            Qry += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax3_amt,0) as txt3amt,  "
            Qry += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax4_amt,0) as txt4amt,  "
            Qry += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax5_amt,0) as txt5amt,  "
            Qry += " tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax6_amt,0) as txt6amt,  "
            Qry += " tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax7_amt,0) as txt7amt,  "
            Qry += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax8_amt,0) as txt8amt,   "
            Qry += " tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax9_amt,0) as txt9amt,  "
            Qry += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SALE_INVOICE_HEAD.tax10_amt,0) as txt10amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,  "
            Qry += " isnull(TSPL_SD_SALE_INVOICE_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Phone,TSPL_COMPANY_MASTER.Fax,TSPL_COMPANY_MASTER.Email ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,ISNULL(tspl_company_Master.ADD2,'') as address2,ISNULL(tspl_company_Master.ADD3,'') as address3,"
            Qry += " TSPL_SD_SALE_INVOICE_DETAIL.item_code as item_code,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as PrincipleDesc,'' as CUSTOMER_ITEM_NO, TSPL_ITEM_MASTER.Item_Desc+ case when FOC_Item ='1' then ' (Free Scheme)' when FOC_Item='0' then '' end   as itemdesc"
            Qry += " , TSPL_SD_SALE_INVOICE_DETAIL.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL.Qty as qty,TSPL_SD_SALE_INVOICE_DETAIL.unit_code as uom,TSPL_SD_SALE_INVOICE_DETAIL.item_cost as itemcost,TSPL_SD_SALE_INVOICE_DETAIL.amount as amount,TSPL_SD_SALE_INVOICE_HEAD.TAX1,TSPL_SD_SALE_INVOICE_HEAD.TAX2,TSPL_SD_SALE_INVOICE_HEAD.TAX3,TSPL_SD_SALE_INVOICE_HEAD.TAX4,TSPL_SD_SALE_INVOICE_HEAD.TAX5,TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge,TSPL_SD_SALE_INVOICE_DETAIL.Batch_No,cast(datepart(MONTH,TSPL_SD_SALE_INVOICE_DETAIL.MFG_Date) as varchar(2)) + '/' + "
            Qry += " SUBSTRING(cast(datepart(YYYY,TSPL_SD_SALE_INVOICE_DETAIL.MFG_Date) as varchar(4)),3,2) as MFG_Date,"
            Qry += " cast(datepart(MONTH,TSPL_SD_SALE_INVOICE_DETAIL.Expiry_Date) as varchar(2)) + '/' + "
            Qry += " SUBSTRING(cast(datepart(YYYY,TSPL_SD_SALE_INVOICE_DETAIL.Expiry_Date) as varchar(4)),3,2) as Exp_Date,TSPL_SD_SALE_INVOICE_DETAIL.mrp,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,TSPL_SD_SALE_INVOICE_DETAIL.Item_Weight,(case when  coalesce(TSPL_SD_SALE_INVOICE_DETAIL.WEIGHT_UOM,tspl_item_master.Weight_UOM)=TSPL_WEIGHT_CONVERSION.Container_UOM then TSPL_SD_SALE_INVOICE_DETAIL.TotalItem_Weight else (TSPL_SD_SALE_INVOICE_DETAIL.TotalItem_Weight*TSPL_WEIGHT_CONVERSION1.Container_Qty/TSPL_WEIGHT_CONVERSION1.Contained_Qty) end) as TotalItem_Weight,(case when COALESCE(TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type,'R')='T' then 'Tax Invoice' else 'Retail Invoice' end) as Invoice_Type " & colsTaxRateType & " "
            Qry += " " & QryShowStatus & " "
            Qry += " " & SerialNoColumn & " "
            Qry += " from TSPL_SD_SALE_INVOICE_DETAIL  "
            Qry += " " & SerialNo & " "
            Qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD  on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code and TSPL_SD_SALE_INVOICE_HEAD.trans_type='CSA'  "
            Qry += " left outer join  TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code =TSPL_SD_SALE_INVOICE_HEAD .Ship_To_Location "
            Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_SHIP_TO_LOCATION.City_Code "
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code "
            Qry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_INVOICE_HEAD.tax1  "
            Qry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_INVOICE_HEAD.tax2  "
            Qry += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .TAX3  "
            Qry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_INVOICE_HEAD .tax4  "
            Qry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_INVOICE_HEAD .tax5  "
            Qry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX6  "
            Qry += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX7  "
            Qry += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX8  "
            Qry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX9 "
            Qry += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_INVOICE_HEAD .TAX10     "
            Qry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_INVOICE_HEAD.comp_code  "
            Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   "
            Qry += "  left outer join TSPL_STATE_MASTER as Cust_State on Cust_State.STATE_CODE  =TSPL_CUSTOMER_MASTER.State   "
            Qry += " LEFT join (select P_Cust_State.GST_STATE_Code as P_GST_STATE_Code,TSPL_CUSTOMER_MASTER.GSTNO as P_Cust_GST_IN, Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,TSPL_CITY_MASTER.City_Name  as P_City_Name,Email as P_Email,fax as P_Fax,"

            Qry += " case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No"

            Qry += "  from TSPL_CUSTOMER_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code    left join TSPL_STATE_MASTER as P_Cust_State on P_Cust_State.STATE_CODE =TSPL_CUSTOMER_MASTER.State  ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' "

            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SD_SALE_INVOICE_HEAD.CSA_PLANT_LOCATION "
            Qry += "  left join TSPL_STATE_MASTER as Loc_State_Master on Loc_State_Master.STATE_CODE =TSPL_LOCATION_MASTER.State "
            Qry += " left join TSPL_TERMS_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Terms_Code=TSPL_TERMS_MASTER.Terms_Code "
            Qry += " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id"
            'Qry += " left join TSPL_CUSTOMER_ITEM_MAPPING on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_CUSTOMER_ITEM_MAPPING.item_no "
            'Qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code "
            Qry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code "
            Qry += "  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_For_Customer on TSPL_CITY_MASTER_For_Customer.City_Code =TSPL_CUSTOMER_MASTER.City_Code  left join TSPL_STATE_MASTER as P_Cust_State on P_Cust_State.STATE_CODE =TSPL_CUSTOMER_MASTER.State"
            Qry += " left join TSPL_WEIGHT_CONVERSION on coalesce(TSPL_SD_SALE_INVOICE_DETAIL.WEIGHT_UOM,TSPL_ITEM_MASTER.WEIGHT_UOM)=TSPL_WEIGHT_CONVERSION.Container_UOM and TSPL_WEIGHT_CONVERSION.product_type=tspl_item_master.product_type "
            Qry += " left join TSPL_WEIGHT_CONVERSION AS TSPL_WEIGHT_CONVERSION1 on coalesce(TSPL_SD_SALE_INVOICE_DETAIL.WEIGHT_UOM,TSPL_ITEM_MASTER.WEIGHT_UOM)=TSPL_WEIGHT_CONVERSION1.Contained_UOM and TSPL_WEIGHT_CONVERSION.product_type=tspl_item_master.product_type "
            Qry += " left join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_SD_SALE_INVOICE_DETAIL.PrincipleCode=TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE "
            Qry += "  left outer join"

            Qry += "(select Scheme_Item_Line_No,Scheme_Item_Code,SUM(qty) as qty,Unit_code from TSPL_SD_SALE_INVOICE_DETAIL where Scheme_Item_Code in (select Item_Code from TSPL_SD_SALE_INVOICE_DETAIL where FOC_Item=0) and Scheme_Item_Line_No in (select Line_No from TSPL_SD_SALE_INVOICE_DETAIL where FOC_Item=0) and Unit_code in (select Unit_code from TSPL_SD_SALE_INVOICE_DETAIL where FOC_Item=0) group by Scheme_Item_Line_No,Scheme_Item_Code,Unit_code)axa on axa.Scheme_Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and axa.Scheme_Item_Line_No=TSPL_SD_SALE_INVOICE_DETAIL.Line_No and axa.Unit_code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code and TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item=0"

            Qry += " where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' and TSPL_SD_SALE_INVOICE_HEAD.trans_type='CSA' order by TSPL_SD_SALE_INVOICE_DETAIL.line_no"

            'Dim Qry2 As String = "   Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CSA_SALE_TRANSFER_DETAIL.qty   from TSPL_SD_SALE_INVOICE_HEAD Left Outer Join TSPL_CSA_SALE_TRANSFER_DETAIL on TSPL_CSA_SALE_TRANSFER_DETAIL .DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left Outer Join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD .DOC_CODE =TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' "

            dt = clsDBFuncationality.GetDataTable(Qry)
            'Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)
            dt.Columns.Add("BarCodeImage", GetType(Byte()))
            For Each dr As DataRow In dt.Rows
                dr("BarCodeImage") = bytes
            Next


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt
    End Function
    Private Function funPrintTransfer() As DataTable
        Dim strDocNo As String = txtCode.Value
        Dim Qry2 As String = "   Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CSA_SALE_TRANSFER_DETAIL.qty   from TSPL_SD_SALE_INVOICE_HEAD Left Outer Join TSPL_CSA_SALE_TRANSFER_DETAIL on TSPL_CSA_SALE_TRANSFER_DETAIL .DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code left Outer Join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD .DOC_CODE =TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + strDocNo + "' "

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)

        Return dt2
    End Function

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

#Region "Mail Event"
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS Of Respective Sale Patti No. " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            'LoadData(txtCode.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtcustcode.Value)
            'SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSASaleInvoice)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.Cust_Name, txtcustName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.From_Location, txt_loc_code.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Doc_Amount, lblTotRAmt.Text)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        If obj.atchmnt = "Y" Then
    '            Dim dt As DataTable = New DataTable()
    '            dt = funPrint(txtCode.Value)
    '            Dim dt1 As DataTable = New DataTable()
    '            dt1 = funPrintTransfer()
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                strRptPath = frmCRV.EmailSubreportWithdt(CrystalReportFolder.NewSalesReports, dt, dt1, "crptSaleInvoiceCSA", "CSA Sales Invoice", "CsaTransfer")
    '                frmCRV = Nothing
    '            End If
    '        End If
    '        '---------------------------------------------------------------------------


    '        For Each strUser As String In lstUsers

    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '            End If

    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '        Next

    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)



    '        If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
    '            SMSSENDONLY(False)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SMSSENDONLY(ByVal isPost As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSASaleInvoice)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do sms setting", Me.Text)
    '            Return
    '        End If


    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        Dim strMes As String = obj.smsbody
    '        strMes = strMes.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))
    '        strMes = strMes.Replace(clsEmailSMSConstants.Cust_Name, txtcustName.Text)
    '        strMes = strMes.Replace(clsEmailSMSConstants.From_Location, txt_loc_code.Value)
    '        'strMes = strMes.Replace(clsEmailSMSConstants.RT_Detail, ("RT Rate: " + clsCommon.myCstr(txtrate.Text) + " And UOM: " + txtRt_UOM.Text))
    '        ' strMes = strMes.Replace(clsEmailSMSConstants.CSA_Item_Type, cmbCSAType.SelectedValue)
    '        strMes = strMes.Replace(clsEmailSMSConstants.Doc_Amount, lblTotRAmt.Text)

    '        Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_customer_master where cust_code ='" & txtcustcode.Value & "' ")

    '        If clsSMSSend.SendSMS(clsUserMgtCode.frmCSASaleInvoice, strMes, strphone) Then
    '            If Not isPost Then
    '                clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnSend_Approval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend_Approval.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send for Approval Of Respective Sale Patti No. " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
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
            'SendSMSandEmail(lstUsers, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub SetMailRight()
        If objCommonVar.IsMailSend Then
            btnsetting.Enabled = True
        Else
            btnsetting.Enabled = False
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmCSASaleInvoice
        frm.ShowDialog()
    End Sub
#End Region


    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        If gv_Summary.Rows.Count > 0 Then
            Dim Arrheader As New List(Of String)()
            Arrheader.Add("Detail of Transfer Knock-Off")

            clsCommon.MyExportToExcelGrid("Transfer Summary", gv_Summary, Arrheader, "Transfer Summary")
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Dim qry As String = ""
        qry = "select a.DOC_CODE,a.invoice_no,a.invoice_Date,a.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,a.Item_Code,a.Item_Desc,a.Qty,a.Unit_code,a.Alt_qty,a.sale_qty,a.sale_uom from ("
        qry += "select '' as invoice_no,'01/01/1900' as invoice_Date,tspl_csa_transfer_head.DOC_CODE,tspl_csa_transfer_head.Cust_Code,TSPL_CSA_TRANSFER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_CSA_TRANSFER_DETAIL.unit_code,TSPL_CSA_TRANSFER_DETAIL.Qty,NULL as sale_uom,0 as sale_qty,0 as Alt_qty from TSPL_CSA_TRANSFER_DETAIL left outer join tspl_csa_transfer_head on tspl_csa_transfer_head.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code "
        qry += "union all "
        qry += "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as invoice_no,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as invoice_Date,TSPL_CSA_SALE_TRANSFER_DETAIL.Against_Transfer_Code as doc_code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as cust_code,TSPL_CSA_SALE_TRANSFER_DETAIL.item_code,TSPL_ITEM_MASTER.Item_Desc,TSPL_CSA_SALE_TRANSFER_DETAIL.Transfer_UOM as unit_code,TSPL_CSA_SALE_TRANSFER_DETAIL.Transfer_Qty as qty,TSPL_CSA_SALE_TRANSFER_DETAIL.Sale_UOM,TSPL_CSA_SALE_TRANSFER_DETAIL.qty as sale_qty,TSPL_CSA_SALE_TRANSFER_DETAIL.Alt_Qty from TSPL_CSA_SALE_TRANSFER_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_CSA_SALE_TRANSFER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_SALE_TRANSFER_DETAIL.item_code "
        qry += ")a left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=a.Cust_Code order by a.DOC_CODE,a.invoice_date,a.Item_Code"
        Dim dt_summ As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_Summary.SummaryRowsBottom.Clear()
        gv_Summary.GroupDescriptors.Clear()
        gv_Summary.DataSource = Nothing
        gv_Summary.Rows.Clear()
        gv_Summary.Columns.Clear()

        If dt_summ IsNot Nothing AndAlso dt_summ.Rows.Count > 0 Then
            gv_Summary.DataSource = dt_summ

            gv_Summary.TableElement.TableHeaderHeight = 40
            gv_Summary.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv_Summary.Columns.Count - 1
                gv_Summary.Columns(ii).ReadOnly = True
                gv_Summary.Columns(ii).IsVisible = False
            Next

            gv_Summary.Columns("invoice_no").IsVisible = True
            gv_Summary.Columns("invoice_no").Width = 100
            gv_Summary.Columns("invoice_no").HeaderText = "Invoice No."

            gv_Summary.Columns("invoice_date").IsVisible = True
            gv_Summary.Columns("invoice_date").Width = 100
            gv_Summary.Columns("invoice_date").HeaderText = "Invoice Date"

            gv_Summary.Columns("DOC_CODE").IsVisible = True
            gv_Summary.Columns("DOC_CODE").Width = 100
            gv_Summary.Columns("DOC_CODE").HeaderText = "Transfer Code"

            gv_Summary.Columns("cust_code").IsVisible = True
            gv_Summary.Columns("cust_code").Width = 100
            gv_Summary.Columns("cust_code").HeaderText = "Customer Code"

            gv_Summary.Columns("Customer_Name").IsVisible = True
            gv_Summary.Columns("Customer_Name").Width = 160
            gv_Summary.Columns("Customer_Name").HeaderText = "Customer Name"

            gv_Summary.Columns("Item_code").IsVisible = True
            gv_Summary.Columns("Item_code").Width = 100
            gv_Summary.Columns("Item_code").HeaderText = "Item code"

            gv_Summary.Columns("Item_desc").IsVisible = True
            gv_Summary.Columns("Item_desc").Width = 180
            gv_Summary.Columns("Item_desc").HeaderText = "Description"

            gv_Summary.Columns("unit_code").IsVisible = True
            gv_Summary.Columns("unit_code").Width = 80
            gv_Summary.Columns("unit_code").HeaderText = "Transfer Unit"

            gv_Summary.Columns("qty").IsVisible = True
            gv_Summary.Columns("qty").Width = 80
            gv_Summary.Columns("qty").HeaderText = "Transfer Qty"

            gv_Summary.Columns("sale_uom").IsVisible = True
            gv_Summary.Columns("sale_uom").Width = 80
            gv_Summary.Columns("sale_uom").HeaderText = "Sale Unit"

            gv_Summary.Columns("sale_qty").IsVisible = True
            gv_Summary.Columns("sale_qty").Width = 80
            gv_Summary.Columns("sale_qty").HeaderText = "Sale Qty"

            gv_Summary.Columns("alt_qty").IsVisible = True
            gv_Summary.Columns("alt_qty").Width = 100
            gv_Summary.Columns("alt_qty").HeaderText = "Alt. Sale Qty"

            gv_Summary.GroupDescriptors.Clear()


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("sale_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Alt_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv_Summary.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv_Summary.GroupDescriptors.Add(New GridGroupByExpression("DOC_CODE as Doc Format ""{0}: {1}"" Group By DOC_CODE"))
            gv_Summary.GroupDescriptors.Add(New GridGroupByExpression("Item_code as Item Format ""{0}: {1}"" Group By Item_code"))

            gv_Summary.ShowGroupPanel = False
            gv_Summary.MasterTemplate.AutoExpandGroups = True
            RadPageViewPage6.Item.Visibility = ElementVisibility.Visible
            btnexcel.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage6
        Else
            RadPageView1.SelectedPage = RadPageViewPage1
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If
    End Sub

    Private Sub ChkFOC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkFOC.ToggleStateChanged
        If isInsideLoadData = True Then
            Exit Sub
        End If

        'If ChkFOC.Checked Then
        '    LoadBlankGrid()
        '    LoadBlankGridAC()
        '    LoadBlankGridTax()
        '    UpdateAllTotals()

        '    gv.Rows.Clear()
        '    gv.Rows.AddNew()
        '    gvAC.Rows.Clear()
        '    gvAC.Rows.AddNew()

        '    gv.Columns(colUnitRate).ReadOnly = True
        '    gv.Columns(coldate).ReadOnly = True
        '    gv.Columns(colBookingno).ReadOnly = True
        '    gv.Columns(colbookingtype).ReadOnly = True

        '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'")) > 0 Then
        '        clsDBFuncationality.ExecuteNonQuery("delete from CSA_SALE_TRANSFER")
        '    End If
        'ElseIf Not ChkFOC.Checked Then
        '    LoadBlankGrid()
        '    LoadBlankGridAC()
        '    LoadBlankGridTax()
        '    UpdateAllTotals()
        '    gv.Rows.Clear()
        '    gv.Rows.AddNew()
        '    gvAC.Rows.Clear()
        '    gvAC.Rows.AddNew()

        '    gv.Columns(colUnitRate).ReadOnly = False
        '    gv.Columns(coldate).ReadOnly = False
        '    gv.Columns(colBookingno).ReadOnly = False
        '    gv.Columns(colbookingtype).ReadOnly = False

        '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'")) > 0 Then
        '        clsDBFuncationality.ExecuteNonQuery("delete from CSA_SALE_TRANSFER")
        '    End If
        'End If

    End Sub

    Private Sub btnRev_Unpost_Click(sender As Object, e As EventArgs) Handles btnRev_Unpost.Click
        Try
            ''BM00000009170
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document code for Unposting.")
                Throw New Exception("Select document code for Unposting.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the current document?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                btnRev_Unpost.Visible = False
                Exit Sub
            End If

            If clsCSASaleInvoice.UnPostData(Me.Form_ID, arrLoc, txtCode.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Record UnPosted Successfully", Me.Text)

                LoadData(txtCode.Value, NavigatorType.Current)
                btnRev_Unpost.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDistributorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDistributorCode._MYValidating
        fndDistributorCode.Value = clsCommon.myCstr(clsCustomerMaster.getFinder(" isnull(tspl_customer_master.Parent_Customer_YN,'N')='N' and Parent_Customer_No='" + clsCommon.myCstr(txtcustcode.Value) + "' ", fndDistributorCode.Value, isButtonClicked))
        txtDistributor_Name.Text = clsCommon.myCstr(clsCustomerMaster.GetName(fndDistributorCode.Value, Nothing))
    End Sub


#Region "Excel Uploader(transaction)"
    Private Sub LoadBlankUploaderGrid()
        Dim repoLineNo As New GridViewTextBoxColumn()
        Dim repoDatetime As New GridViewDateTimeColumn()
        Dim repoDecimal As New GridViewDecimalColumn()

        ''===============head=======================================
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Doc Code"
        repoLineNo.Name = colUDocCode
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLinenno
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "ResetSchemeLineNo"
        repoLineNo.Name = colUResetScheme_LineNo
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        gv_Uploader.Columns("Distributor Code").HeaderText = "Distributor Code"
        gv_Uploader.Columns("Distributor Code").Width = 50
        gv_Uploader.Columns("Distributor Code").WrapText = True
        gv_Uploader.Columns("Distributor Code").Name = colUDistributorCode

        gv_Uploader.Columns("Excisable").HeaderText = "Excisable(E/N)"
        gv_Uploader.Columns("Excisable").Width = 50
        gv_Uploader.Columns("Excisable").WrapText = True
        gv_Uploader.Columns("Excisable").Name = colUExciseType

        ''0
        gv_Uploader.Columns("Doc Date").HeaderText = "Doc Date"
        gv_Uploader.Columns("Doc Date").Width = 50
        gv_Uploader.Columns("Doc Date").WrapText = True
        gv_Uploader.Columns("Doc Date").Name = colUDocDate

        ''1
        gv_Uploader.Columns("Description").HeaderText = "Description"
        gv_Uploader.Columns("Description").Width = 50
        gv_Uploader.Columns("Description").WrapText = True
        gv_Uploader.Columns("Description").Name = colUDesc


        ''2
        gv_Uploader.Columns("Cust Code").HeaderText = "Customer"
        gv_Uploader.Columns("Cust Code").Width = 50
        gv_Uploader.Columns("Cust Code").WrapText = True
        gv_Uploader.Columns("Cust Code").Name = colUCSACode

        ''3
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Bill to Location"
        repoLineNo.Name = colUBillToLocation
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''4
        gv_Uploader.Columns("Plant Location").HeaderText = "To Location"
        gv_Uploader.Columns("Plant Location").Width = 50
        gv_Uploader.Columns("Plant Location").WrapText = True
        gv_Uploader.Columns("Plant Location").Name = ColUToLocation

        ''5
        'gv_Uploader.Columns("Doc Amount").HeaderText = "Document Amount"
        'gv_Uploader.Columns("Doc Amount").Width = 50
        'gv_Uploader.Columns("Doc Amount").WrapText = True
        'gv_Uploader.Columns("Doc Amount").Name = colUDocAmount
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUDocAmount
        repoDecimal.HeaderText = "Document Amount"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''6
        gv_Uploader.Columns("Tax Group").HeaderText = "Tax Group"
        gv_Uploader.Columns("Tax Group").Width = 50
        gv_Uploader.Columns("Tax Group").WrapText = True
        gv_Uploader.Columns("Tax Group").Name = colUTaxGroup1


        ''7 main
        ''1.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code1"
        repoLineNo.Name = colUTTaxAutCode1
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''8
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt1
        repoDecimal.HeaderText = "TaxBase Amt1"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''9
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate1
        repoDecimal.HeaderText = "TaxBase Rate1"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''10
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt1
        repoDecimal.HeaderText = "Tax Amt1"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''11 main
        ''2.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code2"
        repoLineNo.Name = colUTTaxAutCode2
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''12
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt2
        repoDecimal.HeaderText = "TaxBase Amt2"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''13
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate2
        repoDecimal.HeaderText = "TaxBase Rate2"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''14
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt2
        repoDecimal.HeaderText = "Tax Amt2"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''15 main
        ''3.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code3"
        repoLineNo.Name = colUTTaxAutCode3
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''16
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt3
        repoDecimal.HeaderText = "TaxBase Amt3"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''17
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate3
        repoDecimal.HeaderText = "TaxBase Rate3"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''18
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt3
        repoDecimal.HeaderText = "Tax Amt3"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''19 main
        ''4.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code4"
        repoLineNo.Name = colUTTaxAutCode4
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''20
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt4
        repoDecimal.HeaderText = "TaxBase Amt4"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''21
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate4
        repoDecimal.HeaderText = "TaxBase Rate4"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''22
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt4
        repoDecimal.HeaderText = "Tax Amt4"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''23 main
        ''5.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code5"
        repoLineNo.Name = colUTTaxAutCode5
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''24
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt5
        repoDecimal.HeaderText = "TaxBase Amt5"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''25
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate5
        repoDecimal.HeaderText = "TaxBase Rate5"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''26
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt5
        repoDecimal.HeaderText = "Tax Amt5"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''28 main
        ''6.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code6"
        repoLineNo.Name = colUTTaxAutCode6
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''29
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt6
        repoDecimal.HeaderText = "TaxBase Amt6"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''30
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate6
        repoDecimal.HeaderText = "TaxBase Rate6"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''31
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt6
        repoDecimal.HeaderText = "Tax Amt6"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''32 main
        ''7.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code7"
        repoLineNo.Name = colUTTaxAutCode7
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''33
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt7
        repoDecimal.HeaderText = "TaxBase Amt7"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''34
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate7
        repoDecimal.HeaderText = "TaxBase Rate7"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''35
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt7
        repoDecimal.HeaderText = "Tax Amt7"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''36 main
        ''8.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code8"
        repoLineNo.Name = colUTTaxAutCode8
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''37
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt8
        repoDecimal.HeaderText = "TaxBase Amt8"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''38
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate8
        repoDecimal.HeaderText = "TaxBase Rate8"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''39
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt8
        repoDecimal.HeaderText = "Tax Amt8"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''40 main
        ''9.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code9"
        repoLineNo.Name = colUTTaxAutCode9
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''50
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt9
        repoDecimal.HeaderText = "TaxBase Amt9"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''51
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate9
        repoDecimal.HeaderText = "TaxBase Rate9"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''52
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt9
        repoDecimal.HeaderText = "Tax Amt9"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''53 main
        ''10.
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Tax Code10"
        repoLineNo.Name = colUTTaxAutCode10
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''54
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTBaseAmt10
        repoDecimal.HeaderText = "TaxBase Amt10"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''55
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxRate10
        repoDecimal.HeaderText = "TaxBase Rate10"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''56
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTTaxAmt10
        repoDecimal.HeaderText = "Tax Amt10"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''57
        gv_Uploader.Columns("Term Code").HeaderText = "Term Code"
        gv_Uploader.Columns("Term Code").Width = 50
        gv_Uploader.Columns("Term Code").WrapText = True
        gv_Uploader.Columns("Term Code").Name = colUTermCode


        ''58
        repoDatetime = New GridViewDateTimeColumn()
        repoDatetime.FormatString = ""
        repoDatetime.Format = DateTimePickerFormat.Custom
        repoDatetime.CustomFormat = "dd/MM/yyyy"
        repoDatetime.Name = colUDueDate
        repoDatetime.HeaderText = "Due Date"
        repoDatetime.WrapText = True
        repoDatetime.Width = 50
        repoDatetime.ReadOnly = True
        repoDatetime.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDatetime)

        ''59
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUDoc_Amt_WO_Disc
        repoDecimal.HeaderText = "Doc Amt WO Disc."
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''60
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUAmt_After_Disc
        repoDecimal.HeaderText = "Amt After Disc."
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUAddtnlAmt
        repoDecimal.HeaderText = "Total Add. Amt"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUInvDiscAmt
        repoDecimal.HeaderText = "Inv. Disc. Amt"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTDiscAmt
        repoDecimal.HeaderText = "Total Disc. Amt"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''61
        'gv_Uploader.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"
        'gv_Uploader.Columns("Total_Tax_Amt").Width = 50
        'gv_Uploader.Columns("Total_Tax_Amt").WrapText = True
        'gv_Uploader.Columns("Total_Tax_Amt").IsVisible = False
        'gv_Uploader.Columns("Total_Tax_Amt").ReadOnly = True
        'gv_Uploader.Columns("Total_Tax_Amt").Name = colUTaxAmt
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTaxAmt
        repoDecimal.HeaderText = "Total Tax Amt"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''62
        'gv_Uploader.Columns("Total_Commision_Amt").HeaderText = "Total Comm. Amt."
        'gv_Uploader.Columns("Total_Commision_Amt").Width = 50
        'gv_Uploader.Columns("Total_Commision_Amt").WrapText = True
        'gv_Uploader.Columns("Total_Commision_Amt").IsVisible = False
        'gv_Uploader.Columns("Total_Commision_Amt").ReadOnly = True
        'gv_Uploader.Columns("Total_Commision_Amt").Name = colUTCommsnAmt
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTCommsnAmt
        repoDecimal.HeaderText = "Total Comm. Amt."
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''63
        'gv_Uploader.Columns("Total_Freight_Amt").HeaderText = "Total Freight Amt"
        'gv_Uploader.Columns("Total_Freight_Amt").Width = 50
        'gv_Uploader.Columns("Total_Freight_Amt").WrapText = True
        'gv_Uploader.Columns("Total_Freight_Amt").IsVisible = False
        'gv_Uploader.Columns("Total_Freight_Amt").ReadOnly = True
        'gv_Uploader.Columns("Total_Freight_Amt").Name = colUTFreightAmt
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colUTFreightAmt
        repoDecimal.HeaderText = "Total Freight Amt"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''64
        'gv_Uploader.Columns("RoundOffAmount").HeaderText = "Bill RoundOff"
        'gv_Uploader.Columns("RoundOffAmount").Width = 50
        'gv_Uploader.Columns("RoundOffAmount").WrapText = True
        'gv_Uploader.Columns("RoundOffAmount").Name = colURoundOff
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = 0
        repoDecimal.Name = colURoundOff
        repoDecimal.HeaderText = "Bill RoundOff"
        repoDecimal.Width = 50
        repoDecimal.WrapText = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)
        ''================end here=======================================================

        ''===============detail===================================
        ''65
        repoDatetime = New GridViewDateTimeColumn()
        repoDatetime.Format = DateTimePickerFormat.Custom
        repoDatetime.CustomFormat = "dd/MM/yyyy"
        repoDatetime.HeaderText = "Date"
        repoDatetime.WrapText = True
        repoDatetime.FormatString = "{0:d}"
        repoDatetime.Name = coldate
        repoDatetime.Width = 50
        repoDatetime.IsVisible = BookingEffectOnSale 'if true then visible
        repoDatetime.VisibleInColumnChooser = BookingEffectOnSale
        gv_Uploader.MasterTemplate.Columns.Add(repoDatetime)

        ''66
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Booking No"
        repoLineNo.Name = colBookingno
        repoLineNo.Width = 50
        repoLineNo.IsVisible = BookingEffectOnSale
        repoLineNo.VisibleInColumnChooser = BookingEffectOnSale
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''67
        gv_Uploader.Columns("Item_Code").HeaderText = "Item Code"
        gv_Uploader.Columns("Item_Code").WrapText = True
        gv_Uploader.Columns("Item_Code").Width = 50
        gv_Uploader.Columns("Item_Code").Name = colitemcode

        Dim chkbox As New GridViewCheckBoxColumn()
        chkbox.FormatString = ""
        chkbox.HeaderText = "Is MRP Item"
        chkbox.Name = colIsMRPMandatory
        chkbox.Width = 50
        chkbox.IsVisible = False
        chkbox.ThreeState = False
        chkbox.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(chkbox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "MRP"
        repoDecimal.Name = colMRP
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Abatement%"
        repoDecimal.Name = colAbatementPers
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Abatement Amount"
        repoDecimal.Name = colAbatementAmt
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''68
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Conversion Factor"
        repoDecimal.Name = colConversionFactor
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''69
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Pack Size"
        repoDecimal.Name = colPackSize
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''70
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "MasterPack Size"
        repoDecimal.Name = colMasterPackSize
        repoDecimal.Width = 50
        repoDecimal.IsVisible = False
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''71
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Type"
        repoLineNo.Name = colItemType
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''72
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "CSA Item Type"
        repoLineNo.Name = colCSAType
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''73
        gv_Uploader.Columns("Unit Code").HeaderText = "Item UOM"
        gv_Uploader.Columns("Unit Code").Width = 50
        gv_Uploader.Columns("Unit Code").WrapText = True
        gv_Uploader.Columns("Unit Code").Name = colItemUOM

        ''74
        gv_Uploader.Columns("Qty").HeaderText = "Quantity"
        gv_Uploader.Columns("Qty").WrapText = True
        gv_Uploader.Columns("Qty").Width = 50
        'gv_Uploader.Columns("Qty").Name = colqty

        ''75
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Balance Quantity"
        repoDecimal.Name = colbalqty
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''76
        gv_Uploader.Columns("Alt Unit").HeaderText = "Alt. UOM"
        gv_Uploader.Columns("Alt Unit").Width = 50
        gv_Uploader.Columns("Alt Unit").ReadOnly = True
        gv_Uploader.Columns("Alt Unit").IsVisible = BookingEffectOnSale
        gv_Uploader.Columns("Alt Unit").VisibleInColumnChooser = BookingEffectOnSale
        gv_Uploader.Columns("Alt Unit").WrapText = True
        gv_Uploader.Columns("Alt Unit").Name = colAltUOM

        ''77
        'gv_Uploader.Columns("Alt Qty").HeaderText = "Alt. Quantity"
        'gv_Uploader.Columns("Alt Qty").Width = 50
        'gv_Uploader.Columns("Alt Qty").ReadOnly = True
        'gv_Uploader.Columns("Alt Qty").IsVisible = BookingEffectOnSale
        'gv_Uploader.Columns("Alt Qty").VisibleInColumnChooser = BookingEffectOnSale
        'gv_Uploader.Columns("Alt Qty").Name = colAltQty
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Alt. Quantity"
        repoDecimal.Name = colAltQty
        repoDecimal.Width = 50
        repoDecimal.IsVisible = BookingEffectOnSale
        repoDecimal.VisibleInColumnChooser = BookingEffectOnSale
        repoDecimal.ReadOnly = True
        repoDecimal.DecimalPlaces = 2
        repoDecimal.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''78
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Booking Rate"
        repoDecimal.Name = colbookingrate
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = BookingEffectOnSale  'True''if setting is off then rate fill manual,otherwise auto come from booking
        repoDecimal.DecimalPlaces = 2
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''79
        'gv_Uploader.Columns("Including Tax").Width = 50
        'gv_Uploader.Columns("Including Tax").HeaderText = "Including Tax"
        'gv_Uploader.Columns("Including Tax").WrapText = True
        'gv_Uploader.Columns("Including Tax").Name = colincludingtax
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Including Tax"
        repoLineNo.Name = colincludingtax
        repoLineNo.Width = 50
        repoLineNo.IsVisible = False
        repoLineNo.VisibleInColumnChooser = False
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''80
        gv_Uploader.Columns("Unit Rate").HeaderText = "Unit Price"
        gv_Uploader.Columns("Unit Rate").Width = 50
        gv_Uploader.Columns("Unit Rate").WrapText = True
        gv_Uploader.Columns("Unit Rate").Name = colUnitRate

        ''81
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.Name = colTaxBasis
        repoLineNo.HeaderText = "Tax Basis"
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo) '.add(81, repoLineNo)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Scheme Columns>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ''82
        Dim repoIsSchmItem As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem.FormatString = ""
        repoIsSchmItem.HeaderText = "Scheme Applicable(Y/N)"
        repoIsSchmItem.Name = colIsSchmItem
        repoIsSchmItem.Width = 50
        repoIsSchmItem.DataSource = clsDBFuncationality.GetDataTable("select 'Y' as Code,'Y' as Name union all select 'N' as Code,'N' as Name")
        repoIsSchmItem.DisplayMember = "Name"
        repoIsSchmItem.ValueMember = "Code"
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSchmItem)

        ''83
        repoIsSchmItem = New GridViewComboBoxColumn()
        repoIsSchmItem.FormatString = ""
        repoIsSchmItem.HeaderText = "Scheme Type"
        repoIsSchmItem.Name = colSchmCodeType
        repoIsSchmItem.Width = 50
        repoIsSchmItem.ReadOnly = False
        repoIsSchmItem.DataSource = clsSchemeApplyOnDairy.GetSchemeTypes()
        repoIsSchmItem.DisplayMember = "Code"
        repoIsSchmItem.ValueMember = "Name"
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSchmItem)

        ''84
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Scheme Code"
        repoLineNo.Name = colSchmCode
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''85
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Is FOC"
        repoLineNo.Name = colFOC
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''86
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Main Item Code"
        repoLineNo.Name = colMainIcode
        repoLineNo.Width = 50
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''87
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Main Item Line No"
        repoLineNo.Name = colMainLineNo
        repoLineNo.Width = 50
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''88
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Main Item UOM"
        repoLineNo.Name = colMainIUOM
        repoLineNo.Width = 50
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''89
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Main Item Qty"
        repoLineNo.Name = colMainIQty
        repoLineNo.Width = 50
        repoLineNo.IsVisible = False
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''90
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Cash Scheme Code"
        repoLineNo.Name = colCashSchemeCode
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''91
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Cash Scheme Type"
        repoLineNo.Name = colCashSchemeType
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = False
        repoLineNo.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''92
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Cash %"
        repoDecimal.Name = colCash_Pers
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = True
        repoDecimal.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''93
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Cash Amount"
        repoDecimal.Name = colCash_Amt
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = True
        repoDecimal.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        ''94
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Discount %"
        repoDecimal.Minimum = 0
        repoDecimal.Name = colDisPer
        repoDecimal.Width = 50
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''95
        'gv_Uploader.Columns("Commission Rate").HeaderText = "Commission Rate"
        'gv_Uploader.Columns("Commission Rate").WrapText = True
        'gv_Uploader.Columns("Commission Rate").Width = 50
        'gv_Uploader.Columns("Commission Rate").Name = colCommision
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Commission Rate"
        repoDecimal.Minimum = 0
        repoDecimal.Name = colCommision
        repoDecimal.Width = 50
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.WrapText = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''96
        'gv_Uploader.Columns("Commission Type").HeaderText = "Commission Type"
        'gv_Uploader.Columns("Commission Type").Width = 50
        'gv_Uploader.Columns("Commission Type").IsVisible = True
        'gv_Uploader.Columns("Commission Type").ReadOnly = True
        'gv_Uploader.Columns("Commission Type").WrapText = True
        'gv_Uploader.Columns("Commission Type").Name = colComm_Type_RS_Pers
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Commission Type"
        repoLineNo.Name = colComm_Type_RS_Pers
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''97
        'gv_Uploader.Columns("Commission Amount").HeaderText = "Commission Charges"
        'gv_Uploader.Columns("Commission Amount").WrapText = True
        'gv_Uploader.Columns("Commission Amount").Width = 50
        'gv_Uploader.Columns("Commission Amount").ReadOnly = True
        'gv_Uploader.Columns("Commission Amount").Name = colCommisionValue
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Commission Charges"
        repoDecimal.Minimum = 0
        repoDecimal.Name = colCommisionValue
        repoDecimal.Width = 50
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)
        ''=======================freight=============================================

        ''98
        'gv_Uploader.Columns("Freight Rate").HeaderText = "Freight Rate"
        'gv_Uploader.Columns("Freight Rate").WrapText = True
        'gv_Uploader.Columns("Freight Rate").Width = 50
        'gv_Uploader.Columns("Freight Rate").VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Rate").IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Rate").Name = colFrghtRate
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Freight Rate"
        repoDecimal.Minimum = 0
        repoDecimal.Name = colFrghtRate
        repoDecimal.Width = 50
        repoDecimal.DecimalPlaces = 2
        repoDecimal.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDecimal.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)

        ''99
        'gv_Uploader.Columns("Freight Type").HeaderText = "Freight Type"
        'gv_Uploader.Columns("Freight Type").Width = 50
        'gv_Uploader.Columns("Freight Type").VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Type").IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Type").ReadOnly = True
        'gv_Uploader.Columns("Freight Type").WrapText = True
        'gv_Uploader.Columns("Freight Type").Name = colFrghtType
        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Freight Type"
        repoLineNo.Name = colFrghtType
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        repoLineNo.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        repoLineNo.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)

        ''100
        'gv_Uploader.Columns("Freight Charges").HeaderText = "Freight Charges"
        'gv_Uploader.Columns("Freight Charges").Width = 50
        'gv_Uploader.Columns("Freight Charges").WrapText = True
        'gv_Uploader.Columns("Freight Charges").ReadOnly = True
        'gv_Uploader.Columns("Freight Charges").VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Charges").IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        'gv_Uploader.Columns("Freight Charges").Name = colFrghtAmt
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Freight Charges"
        repoDecimal.Minimum = 0
        repoDecimal.Name = colFrghtAmt
        repoDecimal.Width = 50
        repoDecimal.DecimalPlaces = 2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = ApplyFreight_Cmmsn_Charge_Itemwise
        repoDecimal.VisibleInColumnChooser = ApplyFreight_Cmmsn_Charge_Itemwise
        gv_Uploader.MasterTemplate.Columns.Add(repoDecimal)
        ''==============================================================================

        ''101
        gv_Uploader.Columns("Other Charges").HeaderText = "Other Charges"
        gv_Uploader.Columns("Other Charges").WrapText = True
        gv_Uploader.Columns("Other Charges").Width = 50
        gv_Uploader.Columns("Other Charges").Name = colOtherCharge

        ''102
        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax1)
        repoTax1 = Nothing

        ''103
        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt1)
        repoTaxBaseAmt1 = Nothing

        ''104
        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate1)
        repoTaxRate1 = Nothing

        ''105
        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt1)
        repoTaxAmt1 = Nothing

        '106
        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax1)
        repoIsSurTax1 = Nothing

        ''107
        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode1)
        repoSurTaxCode1 = Nothing

        ''108
        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable1)
        repoIsTaxable1 = Nothing

        ''109
        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable1)
        repoTaxRecoverable1 = Nothing

        ''110
        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax2)
        repoTax2 = Nothing

        ''111
        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt2)
        repoTaxBaseAmt2 = Nothing

        ''112
        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate2)
        repoTaxRate2 = Nothing

        ''113
        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt2)
        repoTaxAmt2 = Nothing

        ''114
        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax2)
        repoIsSurTax2 = Nothing

        ''115
        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode2)
        repoSurTaxCode2 = Nothing

        ''116
        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable2)
        repoIsTaxable2 = Nothing

        ''117
        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable2)
        repoTaxRecoverable2 = Nothing

        ''118
        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax3)
        repoTax3 = Nothing

        ''119
        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt3)
        repoTaxBaseAmt3 = Nothing

        ''120
        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate3)
        repoTaxRate3 = Nothing

        ''121
        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt3)
        repoTaxAmt3 = Nothing

        ''122
        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax3)
        repoIsSurTax3 = Nothing

        ''123
        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode3)
        repoSurTaxCode3 = Nothing

        ''124
        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable3)
        repoIsTaxable3 = Nothing

        ''125
        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable3)
        repoTaxRecoverable3 = Nothing

        ''126
        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax4)
        repoTax4 = Nothing

        ''127
        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt4)
        repoTaxBaseAmt4 = Nothing

        ''128
        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate4)
        repoTaxRate4 = Nothing

        ''136
        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt4)
        repoTaxAmt4 = Nothing

        ''137
        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax4)
        repoIsSurTax4 = Nothing

        ''138
        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode4)
        repoSurTaxCode4 = Nothing

        ''139
        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable4)
        repoIsTaxable4 = Nothing

        ''140
        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable4)
        repoTaxRecoverable4 = Nothing

        ''141
        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax5)
        repoTax5 = Nothing

        ''142
        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt5)
        repoTaxBaseAmt5 = Nothing

        ''143
        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate5)
        repoTaxRate5 = Nothing

        ''144
        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt5.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt5)
        repoTaxAmt5 = Nothing

        ''145
        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax5)
        repoIsSurTax5 = Nothing

        ''146
        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode5)
        repoSurTaxCode5 = Nothing

        ''147
        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable5)
        repoIsTaxable5 = Nothing

        ''148
        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable5)
        repoTaxRecoverable5 = Nothing

        ''149
        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax6)
        repoTax6 = Nothing

        ''150
        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt6)
        repoTaxBaseAmt6 = Nothing

        ''151
        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate6)
        repoTaxRate6 = Nothing

        ''152
        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt6)
        repoTaxAmt6 = Nothing

        ''153
        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax6)
        repoIsSurTax6 = Nothing

        ''154
        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode6)
        repoSurTaxCode6 = Nothing

        ''155
        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable6)
        repoIsTaxable6 = Nothing

        ''156
        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable6)
        repoTaxRecoverable6 = Nothing

        ''157
        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax7)
        repoTax7 = Nothing

        ''158
        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt7)
        repoTaxBaseAmt7 = Nothing

        ''159
        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate7)
        repoTaxRate7 = Nothing

        ''160
        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt7)
        repoTaxAmt7 = Nothing

        ''161
        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax7)
        repoIsSurTax7 = Nothing

        ''162
        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode7)
        repoSurTaxCode7 = Nothing

        ''163
        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable7)
        repoIsTaxable7 = Nothing

        ''164
        Dim repoTaxRecoverable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable7.HeaderText = "Recoverable Tax 7"
        repoTaxRecoverable7.Name = colTaxRecoverable7
        repoTaxRecoverable7.ReadOnly = True
        repoTaxRecoverable7.IsVisible = False
        repoTaxRecoverable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable7)
        repoTaxRecoverable7 = Nothing

        ''165
        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax8)
        repoTax8 = Nothing

        ''166
        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt8)
        repoTaxBaseAmt8 = Nothing

        ''167
        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate8)
        repoTaxRate8 = Nothing

        ''168
        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt8)
        repoTaxAmt8 = Nothing

        ''169
        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax8)
        repoIsSurTax8 = Nothing

        ''170
        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode8)
        repoSurTaxCode8 = Nothing

        ''171
        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable8)
        repoIsTaxable8 = Nothing

        ''172
        Dim repoTaxRecoverable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable8.HeaderText = "Recoverable Tax 8"
        repoTaxRecoverable8.Name = colTaxRecoverable8
        repoTaxRecoverable8.ReadOnly = True
        repoTaxRecoverable8.IsVisible = False
        repoTaxRecoverable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable8)
        repoTaxRecoverable8 = Nothing

        ''173
        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax9)
        repoTax9 = Nothing

        ''174
        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt9)
        repoTaxBaseAmt9 = Nothing

        ''175
        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate9)
        repoTaxRate9 = Nothing

        ''176
        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt9)
        repoTaxAmt9 = Nothing

        ''177
        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax9)
        repoIsSurTax9 = Nothing

        ''178
        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode9)
        repoSurTaxCode9 = Nothing

        ''179
        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable9)
        repoIsTaxable9 = Nothing

        ''180
        Dim repoTaxRecoverable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable9.HeaderText = "Recoverable Tax 9"
        repoTaxRecoverable9.Name = colTaxRecoverable9
        repoTaxRecoverable9.ReadOnly = True
        repoTaxRecoverable9.IsVisible = False
        repoTaxRecoverable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable9)
        repoTaxRecoverable9 = Nothing

        ''181
        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTax10)
        repoTax10 = Nothing

        ''182
        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxBaseAmt10)
        repoTaxBaseAmt10 = Nothing

        ''183
        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRate10)
        repoTaxRate10 = Nothing

        ''184
        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxAmt10)
        repoTaxAmt10 = Nothing

        ''185
        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsSurTax10)
        repoIsSurTax10 = Nothing

        ''186
        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoSurTaxCode10)
        repoSurTaxCode10 = Nothing

        ''187
        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsTaxable10)
        repoIsTaxable10 = Nothing

        ''188
        Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable10.HeaderText = "Recoverable Tax 10"
        repoTaxRecoverable10.Name = colTaxRecoverable10
        repoTaxRecoverable10.ReadOnly = True
        repoTaxRecoverable10.IsVisible = False
        repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoTaxRecoverable10)
        repoTaxRecoverable10 = Nothing

        ''189
        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.DecimalPlaces = 2
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        repoDisAmt.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoDisAmt)
        repoDisAmt = Nothing

        ''190
        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Sale Rate"
        repoConv.Name = colSaleRate
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.DecimalPlaces = 2
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoConv)
        repoConv = Nothing

        ''191
        Dim repoTotItemWt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotItemWt = New GridViewDecimalColumn()
        repoTotItemWt.FormatString = ""
        repoTotItemWt.HeaderText = "Sale Value"
        repoTotItemWt.Name = colSaleValue
        repoTotItemWt.Width = 80
        repoTotItemWt.Minimum = 0
        repoTotItemWt.DecimalPlaces = 2
        repoTotItemWt.ReadOnly = True
        repoTotItemWt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTotItemWt)
        repoTotItemWt = Nothing

        ''192
        Dim repoMRP1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRP1.FormatString = ""
        repoMRP1.HeaderText = "Transfer Option"
        repoMRP1.Name = coldoubleclick
        repoMRP1.Width = 80
        repoMRP1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP1.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoMRP1)
        repoMRP1 = Nothing

        ''193
        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "Stock Transfer Rate"
        repoMRP.Name = colStckTransferrate
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        repoMRP.Minimum = 0
        repoMRP.DecimalPlaces = 2
        gv_Uploader.MasterTemplate.Columns.Add(repoMRP)
        repoMRP = Nothing

        ''194
        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty.FormatString = ""
        repoFreeQty.HeaderText = "Stock Value"
        repoFreeQty.Name = colstckratevalue
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
        repoFreeQty.ReadOnly = True
        repoFreeQty.DecimalPlaces = 2
        repoFreeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoFreeQty)
        repoFreeQty = Nothing

        ''195
        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 80
        repoAmtAfterDis.DecimalPlaces = 2
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.ReadOnly = True
        repoAmtAfterDis.IsVisible = False
        gv_Uploader.MasterTemplate.Columns.Add(repoAmtAfterDis)
        repoAmtAfterDis = Nothing

        ''196
        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable1)
        repoIsExcisable1 = Nothing

        ''197
        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable2)
        repoIsExcisable2 = Nothing

        ''198
        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable3)
        repoIsExcisable3 = Nothing

        ''199
        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable4)
        repoIsExcisable4 = Nothing

        ''200
        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable5)
        repoIsExcisable5 = Nothing

        ''201
        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable6)
        repoIsExcisable6 = Nothing

        ''202
        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable7)
        repoIsExcisable7 = Nothing

        ''203
        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable8)
        repoIsExcisable8 = Nothing

        ''204
        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable9)
        repoIsExcisable9 = Nothing

        ''205
        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv_Uploader.MasterTemplate.Columns.Add(repoIsExcisable10)
        repoIsExcisable10 = Nothing

        ''206
        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.DecimalPlaces = 2
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Uploader.MasterTemplate.Columns.Add(repoTotTaxAmt)
        repoTotTaxAmt = Nothing

        ''207
        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        repoAmtAfterTax.DecimalPlaces = 2
        gv_Uploader.MasterTemplate.Columns.Add(repoAmtAfterTax)
        repoAmtAfterTax = Nothing

        ''208
        Dim repoAmtAfterTax1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax1.FormatString = ""
        repoAmtAfterTax1.HeaderText = "Gain/Loss Amount"
        repoAmtAfterTax1.Name = colGainLoss
        repoAmtAfterTax1.WrapText = True
        repoAmtAfterTax1.Width = 80
        repoAmtAfterTax1.DecimalPlaces = 2
        repoAmtAfterTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax1.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoAmtAfterTax1)
        repoAmtAfterTax1 = Nothing

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Import Remarks"
        repoLineNo.Name = colUValidateRemark
        repoLineNo.Width = 200
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        gv_Uploader.MasterTemplate.Columns.Add(repoLineNo)


        gv_Uploader.AllowMultiColumnSorting = True
        gv_Uploader.EnableSorting = True
        gv_Uploader.AllowAddNewRow = False
        gv_Uploader.ShowGroupPanel = False
        gv_Uploader.AllowColumnReorder = False
        gv_Uploader.AllowRowReorder = False
        gv_Uploader.EnableSorting = False
        gv_Uploader.EnableFiltering = True
        gv_Uploader.ShowFilteringRow = True
        gv_Uploader.MasterTemplate.ShowRowHeaderColumn = False
        gv_Uploader.TableElement.TableHeaderHeight = 40
        gv_Uploader.AllowDeleteRow = False

        UpdateDocCode_For_Uploader_Forsametypedata()
        repoLineNo = Nothing
        repoDecimal = Nothing
        repoDatetime = Nothing
    End Sub

    Private Sub UpdateDocCode_For_Uploader_Forsametypedata()
        Dim arr As New ArrayList()
        Dim Unique_Identity As String = ""
        Dim strDocCode As String = ""
        Dim xcount As Integer = 1
        For Each grow As GridViewRowInfo In gv_Uploader.Rows
            Unique_Identity = clsCommon.myCstr(grow.Cells(colUExciseType).Value) + "&" + clsCommon.myCstr(grow.Cells(colUDocDate).Value) + "&" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "&" + clsCommon.myCstr(grow.Cells(ColUToLocation).Value)

            If clsCommon.myLen(Unique_Identity) > 0 AndAlso Not arr.Contains(Unique_Identity) Then
                xcount = 1
                arr.Add(Unique_Identity)
                If clsCommon.myLen(strDocCode) <= 0 Then
                    strDocCode = "CSIUP00000000001"
                Else
                    strDocCode = clsCommon.incval(strDocCode)
                End If
            End If
            grow.Cells(colUDocCode).Value = strDocCode
            grow.Cells(colLinenno).Value = xcount
            xcount += 1
        Next
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        ''export
        Dim qry As String = "select TSPL_SD_SALE_INVOICE_HEAD.Excisable,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Doc Date],TSPL_SD_SALE_INVOICE_HEAD.[Description],TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Cust Code],TSPL_SD_SALE_INVOICE_HEAD.CSA_PLANT_LOCATION  as [Plant Location],TSPL_SD_SALE_INVOICE_HEAD.Tax_Group as [Tax Group],TSPL_SD_SALE_INVOICE_HEAD.Terms_Code as [Term Code] " & _
                            " ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code],TSPL_SD_SALE_INVOICE_DETAIL.alt_uom as [Alt Unit],TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost as [Unit Rate],TSPL_SD_SALE_INVOICE_DETAIL.CSA_Other_Chrage as [Other Charges] "
        qry += " ,TSPL_SD_SALE_INVOICE_HEAD.CSA_Distributor_Code as [Distributor Code] "
        qry += " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "


        transportSql.ExporttoExcel(qry, " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='CSA' ", Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        ''import
        txtUploaderTotal.Text = "Total Rows: 0"
        Try
            gv_Uploader.DataSource = Nothing
            gv_Uploader.Rows.Clear()
            gv_Uploader.Columns.Clear()

            If transportSql.importExcel(gv_Uploader, "Excisable", "Doc Date", "Description", "Cust Code", "Plant Location", "Tax Group", "Term Code", "Item_Code", "Qty", "Unit Code", "Unit Rate", "Alt Unit", "Other Charges", "Distributor Code") Then
                clsCommon.ProgressBarPercentShow()

                ''do sorting of records for easy saving purpose.
                Dim dt As New DataTable()
                dt = gv_Uploader.DataSource()
                dt.DefaultView.Sort = "[Excisable],[Doc Date],[Cust Code],[Plant Location]"
                gv_Uploader.DataSource = Nothing
                gv_Uploader.Rows.Clear()
                gv_Uploader.Columns.Clear()

                gv_Uploader.DataSource = dt.DefaultView.ToTable()
                ''======================end here========================

                LoadBlankUploaderGrid()

                RefreshLineNo_Uploader()

                txtUploaderTotal.Text = "Total Rows: " + clsCommon.myCstr(gv_Uploader.MasterView.Rows.Count())

                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfered Successfully.", Me.Text)

                ImportTransactionButtonEvent()
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Private Sub ImportTransactionButtonEvent()
        RadPageViewPage7.Item.Visibility = ElementVisibility.Visible
        RadPageView1.SelectedPage = RadPageViewPage7
        SplitContainer1.Panel2.Enabled = False
        RadPageViewPage1.Item.Enabled = False
        RadPageViewPage2.Item.Enabled = False
        RadPageViewPage3.Item.Enabled = False
        RadPageViewPage4.Item.Enabled = False
        RadPageViewPage5.Item.Enabled = False
        RadPageViewPage6.Item.Enabled = False
        RadPageViewPage7.Item.Enabled = True

        btnValidate.Enabled = True
        btnApplyScheme.Enabled = False
        btnTransferKnockOff.Enabled = False
        btnCalculation.Enabled = False
        btnUploaderSave.Enabled = False
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gv_Uploader.DataSource = Nothing
        gv_Uploader.Rows.Clear()
        gv_Uploader.Columns.Clear()

        RadPageViewPage7.Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1
        SplitContainer1.Panel2.Enabled = True
        RadPageViewPage1.Item.Enabled = True
        RadPageViewPage2.Item.Enabled = True
        RadPageViewPage3.Item.Enabled = True
        RadPageViewPage4.Item.Enabled = True
        RadPageViewPage5.Item.Enabled = True
        RadPageViewPage6.Item.Enabled = True
        RadPageViewPage7.Item.Enabled = False
    End Sub

    Private Function LoadData_InUploaderGrid(ByVal IntRow As Integer) As String
        Dim dt As New DataTable
        Dim validateremarks As String = Nothing
        Try
            gv_Uploader.Rows(IntRow).Cells(colUBillToLocation).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where cust_code='" + clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value) + "'"))
            gv_Uploader.Rows(IntRow).Cells(colUTermCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code from tspl_customer_master where cust_code='" + clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value) + "'"))

            Dim noofdays As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select no_days from TSPL_TERMS_MASTER where terms_code='" + clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUTermCode).Value) + "'"))

            If gv_Uploader.Rows(IntRow).Cells(colUDocDate).Value IsNot Nothing AndAlso clsCommon.myLen(gv_Uploader.Rows(IntRow).Cells(colUDocDate).Value) > 0 AndAlso IsDate(gv_Uploader.Rows(IntRow).Cells(colUDocDate).Value) Then
                gv_Uploader.Rows(IntRow).Cells(colUDueDate).Value = clsCommon.myCDate(gv_Uploader.Rows(IntRow).Cells(colUDocDate).Value).AddDays(noofdays)
            End If

            If clsCommon.myLen(gv_Uploader.Rows(IntRow).Cells(colUExciseType).Value) <= 0 Then
                validateremarks = validateremarks + "Please Fill excisable type(E->Excisable,N->NonExcisable)." + Environment.NewLine
            End If
            If clsCommon.myLen(gv_Uploader.Rows(IntRow).Cells(colUExciseType).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUExciseType).Value), "E") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUExciseType).Value), "N") <> CompairStringResult.Equal Then
                validateremarks = validateremarks + "Please Fill excisable type(E->Excisable,N->NonExcisable)." + Environment.NewLine
            End If

            ''==========tax group detail=========================
            dt = New DataTable()
            dt = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUTaxGroup1).Value), "S", clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(ColUToLocation).Value), OpenALLTaxes)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If (dt.Rows.Count <= 10) Then
                    For i As Integer = 0 To 9
                        If dt.Rows.Count > i Then
                            gv_Uploader.Rows(IntRow).Cells("UAuthCode" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCstr(dt.Rows(i)("Tax_Code"))
                            gv_Uploader.Rows(IntRow).Cells("UTaxRate" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCdbl(dt.Rows(i)("TaxRate"))
                        End If
                    Next

                    SetitemWiseTaxSetting_For_Uploader(True, IntRow)
                End If
            Else
                For i As Integer = 0 To 9
                    gv_Uploader.Rows(IntRow).Cells("UAuthCode" & clsCommon.myCstr(i + 1)).Value = Nothing
                    gv_Uploader.Rows(IntRow).Cells("UTaxRate" & clsCommon.myCstr(i + 1)).Value = Nothing
                Next
            End If
            ''==================end tax group detail==============================================================

            'gv_Uploader.Rows(IntRow).Cells(colbookingtype).Value = "Item-Wise"
            ''=====conversion
            CalConversionFactor_Uploader(IntRow)
            ''============================

            gv_Uploader.Rows(IntRow).Cells(colItemType).Value = clsCommon.myCstr(LoadItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colitemcode).Value) + "'"))))
            gv_Uploader.Rows(IntRow).Cells(colCSAType).Value = clsCommon.myCstr(clsItemMaster.GetItemCSAType(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colitemcode).Value), Nothing))
            gv_Uploader.Rows(IntRow).Cells(colbookingrate).Value = clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colUnitRate).Value)

            ''=====================MRP--------------------------------
            If ExciseentryOnSale Then
                gv_Uploader.Rows(IntRow).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colitemcode).Value), Nothing)
                gv_Uploader.Rows(IntRow).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
            End If
            ''=============================================================

            ''alt qty
            CalAltQty_for_Uploader(IntRow)
            ''===========================================

            If clsCommon.myLen(gv_Uploader.Rows(IntRow).Cells(colTaxBasis).Value) <= 0 Then
                gv_Uploader.Rows(IntRow).Cells(colTaxBasis).Value = "No"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colincludingtax).Value), "Yes") = CompairStringResult.Equal Then
                gv_Uploader.Rows(IntRow).Cells(colTaxBasis).Value = "Back Calculation"
            Else
                gv_Uploader.Rows(IntRow).Cells(colTaxBasis).Value = "Forward Calculation"
            End If

            gv_Uploader.Rows(IntRow).Cells(colbalqty).Value = 0
            gv_Uploader.Rows(IntRow).Cells(colDisPer).Value = 0
            gv_Uploader.Rows(IntRow).Cells(colDisAmt).Value = 0

            validateremarks = validateremarks & CommisionValue_For_Uploader(IntRow)


            Return validateremarks
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            Dim validcount As Integer = 0
            Dim qry As String = ""
            Dim csacode As String = Nothing
            Dim dist_code As String = Nothing
            Dim csaToLoc As String = Nothing
            Dim TaxGroup As String = Nothing
            Dim ItemCode As String = Nothing
            Dim UOM As String = Nothing
            Dim qty As Decimal = Nothing
            Dim UnitRate As Decimal = Nothing
            Dim altuom As String = Nothing
            Dim includingTax As String = Nothing
            Dim commissiontype As String = Nothing
            Dim freighttype As String = Nothing

            Dim ValidateRemark As String = Nothing

            clsCommon.ProgressBarPercentShow()
            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                clsCommon.ProgressBarPercentUpdate((grow.Index + 1) / gv_Uploader.Rows.Count * 100, " Read & Validate Record(s) " & (grow.Index + 1) & " of Total " & gv_Uploader.Rows.Count)

                ValidateRemark = ""
                If grow.Cells(colUDocDate).Value Is Nothing OrElse clsCommon.myLen(grow.Cells(colUDocDate).Value) <= 0 OrElse Not IsDate(grow.Cells(colUDocDate).Value) Then
                    ValidateRemark = ValidateRemark & " Document date should not blank." + Environment.NewLine
                End If

                If clsCommon.myLen(grow.Cells(colUExciseType).Value) > 0 AndAlso clsCommon.CompairString(grow.Cells(colUExciseType).Value, "E") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells(colUExciseType).Value, "N") <> CompairStringResult.Equal Then
                    ValidateRemark = ValidateRemark & " Put E(Excisable) or N(Non-Excisable) in Excisable column." + Environment.NewLine
                ElseIf clsCommon.myLen(grow.Cells(colUExciseType).Value) <= 0 Then
                    grow.Cells(colUExciseType).Value = "N"
                End If
                csacode = clsCommon.myCstr(grow.Cells(colUCSACode).Value)
                csaToLoc = clsCommon.myCstr(grow.Cells(ColUToLocation).Value)
                TaxGroup = clsCommon.myCstr(grow.Cells(colUTaxGroup1).Value)
                dist_code = clsCommon.myCstr(grow.Cells(colUDistributorCode).Value)

                If clsCommon.myLen(csacode) <= 0 Then
                    ValidateRemark = ValidateRemark & " CSA Code should not blank." + Environment.NewLine
                End If
                If clsCommon.myLen(csacode) > 0 Then
                    qry = "select 1 from tspl_customer_master where csa_type='Y' and cust_code='" + csacode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " CSA Code not exist in Customer Master." + Environment.NewLine
                    End If
                End If

                If AllowDistibutorSale Then
                    If clsCommon.myLen(dist_code) <= 0 Then
                        ValidateRemark = ValidateRemark & " Distributor Code should not blank." + Environment.NewLine
                    End If
                    If clsCommon.myLen(dist_code) > 0 Then
                        qry = "select 1 from tspl_customer_master where isnull(Parent_Customer_YN,'N')='N' and Parent_Customer_No='" + csacode + "' and cust_code='" + dist_code + "'"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                            ValidateRemark = ValidateRemark & " Distributor Code not exist in Customer Master or not mapped with selected CSA." + Environment.NewLine
                        End If
                    End If
                End If

                If clsCommon.myLen(csaToLoc) <= 0 Then
                    ValidateRemark = ValidateRemark & " To Location Code should not blank." + Environment.NewLine
                End If
                If clsCommon.myLen(csaToLoc) > 0 Then
                    qry = "select 1 from tspl_location_master where location_code='" + csaToLoc + "' " 'and type='Plant'
                    qry += " and coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N' "
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " To Location not exist in Location Master or set as Sub-Location,Section,GIT." + Environment.NewLine
                    End If
                End If

                If clsCommon.myLen(TaxGroup) <= 0 Then
                    ValidateRemark = ValidateRemark & " Tax Group should not blank." + Environment.NewLine
                End If
                If clsCommon.myLen(TaxGroup) > 0 Then
                    qry = "select 1 from ("
                    qry += " Select Tax_Group_Code"
                    qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
                    qry += " where Location_Code = '" + csaToLoc + "' and Tax_Type='S' "
                    If Not OpenALLTaxes Then ''when false then without state check condition tax finder open all taxes
                        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + csaToLoc + "' union all   "
                        qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + csacode + "')x) "
                    End If
                    qry += " group by Tax_Group_Code"
                    qry += " )xxx"
                    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xxx.Tax_Group_Code and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' where xxx.Tax_Group_Code='" + TaxGroup + "' "

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " Tax Group is not mapped with To Location for CSA " & csacode & "" + Environment.NewLine
                    End If
                    SetTaxDetails_For_Uploader(grow.Index)
                    SetitemWiseTaxSetting_For_Uploader(True, grow.Index)
                End If

                Dim GSTStatus As Boolean = False
                GSTStatus = clsERPFuncationality.GetGSTStatus(dtpdate.Value)
                If GSTStatus Then
                    If clsLocationWiseTax.IsValidTaxGroupForCSATransferSalePatti(TaxGroup, csaToLoc, csacode, "S", clsCommon.myCDate(grow.Cells(colUDocDate).Value, "dd-MMM-yyyy"), False, Nothing) = False Then
                        ValidateRemark = ValidateRemark & " Check tax Group" + Environment.NewLine
                    End If
                End If


                ItemCode = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                If clsCommon.myLen(ItemCode) <= 0 Then
                    ValidateRemark = ValidateRemark & " Item Code should not blank" + Environment.NewLine
                End If
                If clsCommon.myLen(ItemCode) > 0 Then
                    qry = "select 1 from tspl_item_master where item_code='" + ItemCode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " Item code not found in Item Master" + Environment.NewLine
                    End If
                End If

                UOM = clsCommon.myCstr(grow.Cells(colItemUOM).Value)
                If clsCommon.myLen(UOM) <= 0 Then
                    ValidateRemark = ValidateRemark & " Unit Code should not blank" + Environment.NewLine
                End If
                If clsCommon.myLen(UOM) > 0 Then
                    qry = "select 1 from tspl_item_uom_detail where item_code='" + ItemCode + "' and uom_code='" + UOM + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " Unit Code is not mapped with Item code " & ItemCode & "" + Environment.NewLine
                    End If
                End If

                qty = clsCommon.myCdbl(grow.Cells(colqty).Value)
                If qty <= 0 Then
                    ValidateRemark = ValidateRemark & " Please fill quantity for item." + Environment.NewLine
                End If

                includingTax = clsCommon.myCstr(grow.Cells(colincludingtax).Value)
                If clsCommon.myLen(includingTax) <= 0 OrElse (clsCommon.myLen(includingTax) > 0 AndAlso clsCommon.CompairString(includingTax, "No") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(includingTax, "Yes") <> CompairStringResult.Equal) Then
                    grow.Cells(colincludingtax).Value = "No"
                    grow.Cells(colTaxBasis).Value = "Forward Calculation"
                    'ValidateRemark = ValidateRemark & " Please fill 'Yes/No' in including tax for Item code " & ItemCode & "" + Environment.NewLine
                End If

                UnitRate = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                If UnitRate <= 0 Then
                    CalUnitPrice_For_Uploader(grow.Index, True)
                    UnitRate = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                    If UnitRate <= 0 Then
                        ValidateRemark = ValidateRemark & " Please fill unit rate for item." + Environment.NewLine
                    End If
                End If

                altuom = clsCommon.myCstr(grow.Cells(colAltUOM).Value)
                If BookingEffectOnSale AndAlso clsCommon.myLen(altuom) <= 0 Then
                    ValidateRemark = ValidateRemark & " Alt Unit Code should not blank" + Environment.NewLine
                End If
                If clsCommon.myLen(altuom) > 0 Then
                    qry = "select 1 from tspl_item_uom_detail where item_code='" + ItemCode + "' and uom_code='" + altuom + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                        ValidateRemark = ValidateRemark & " Alt Unit Code is not mapped with Item code " & ItemCode & "" + Environment.NewLine
                    End If
                End If

                If ApplyFreight_Cmmsn_Charge_Itemwise Then
                    qry = "select TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Rate,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_Type,TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_Type " & _
                        " from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No " & _
                        " where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        grow.Cells(colCommision).Value = clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
                        grow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(dt.Rows(0)("Commission_Type"))

                        grow.Cells(colFrghtRate).Value = clsCommon.myCdbl(dt.Rows(0)("Freight_Rate"))
                        grow.Cells(colFrghtType).Value = clsCommon.myCstr(dt.Rows(0)("Freight_Type"))
                    End If

                    If clsCommon.myCdbl(grow.Cells(colCommision).Value) > 0 OrElse clsCommon.myCdbl(grow.Cells(colFrghtRate).Value) > 0 Then
                        'gv_uploader.CurrentRow.Cells(colCommision).ReadOnly = True
                        CommisionValue_For_Uploader(grow.Index)
                    End If
                    If clsCommon.myCdbl(grow.Cells(colCommision).Value) <= 0 Then
                        grow.Cells(colCommision).ReadOnly = False
                    End If
                    If clsCommon.myCdbl(grow.Cells(colFrghtRate).Value) <= 0 Then
                        grow.Cells(colFrghtRate).ReadOnly = False
                    End If

                Else
                    grow.Cells(colCommision).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select CSA_Commision_Rate from TSPL_LOCATION_MASTER where Cust_Code='" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "' and location_code='" + clsCommon.myCstr(grow.Cells(colUBillToLocation).Value) + "'"))
                    grow.Cells(colComm_Type_RS_Pers).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(CSA_Commission_RS_PERS,'R') from TSPL_LOCATION_MASTER where Cust_Code='" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "' and location_code='" + clsCommon.myCstr(grow.Cells(colUBillToLocation).Value) + "'"))

                    If clsCommon.myCdbl(grow.Cells(colCommision).Value) > 0 Then
                        'gv_uploader.CurrentRow.Cells(colCommision).ReadOnly = True
                        CommisionValue(grow.Index)
                    Else
                        grow.Cells(colCommision).ReadOnly = False
                    End If
                End If
                ''===================================================================
                'commissiontype = clsCommon.myCstr(grow.Cells(colComm_Type_RS_Pers).Value)
                'If clsCommon.myLen(commissiontype) <= 0 OrElse (clsCommon.myLen(commissiontype) > 0 AndAlso clsCommon.CompairString(commissiontype, "R") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(commissiontype, "P") <> CompairStringResult.Equal) Then
                '    ValidateRemark = ValidateRemark & " Please fill 'R(Rupees) or P(Percentage)' in commission type for Item code " & ItemCode & "" + Environment.NewLine
                'End If

                'freighttype = clsCommon.myCstr(grow.Cells(colFrghtType).Value)
                'If ApplyFreight_Cmmsn_Charge_Itemwise AndAlso clsCommon.myLen(freighttype) <= 0 OrElse (clsCommon.myLen(freighttype) > 0 AndAlso clsCommon.CompairString(freighttype, "R") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(freighttype, "P") <> CompairStringResult.Equal) Then
                '    ValidateRemark = ValidateRemark & " Please fill 'R(Rupees) or P(Percentage)' in freight type for Item code " & ItemCode & "" + Environment.NewLine
                'End If

                'ValidateRemark = CheckExciseableAccount_For_Uploader()

                ''======================check excisable===================
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colUExciseType).Value), "E") = CompairStringResult.Equal AndAlso clsCommon.myLen(grow.Cells(colitemcode).Value) > 0 Then
                    Dim arrIcode As New List(Of String)
                    arrIcode.Add(clsCommon.myCstr(grow.Cells(colitemcode).Value))
                    Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrIcode)
                    If Not (intx = arrIcode.Count OrElse intx = 0) Then
                        ValidateRemark = ValidateRemark & "Item should be of Excisable." + Environment.NewLine
                    End If
                    If clsLocation.isLocatinExcisable(clsCommon.myCstr(grow.Cells(ColUToLocation).Value)) = True Then
                        If clsCommon.myLen(grow.Cells(colUTaxGroup1).Value) > 0 Then
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colUTTaxAutCode1).Value + "'")), "Y") = CompairStringResult.Equal Then
                                ValidateRemark = ValidateRemark & "Tax should be excisable." + Environment.NewLine
                            End If
                        End If
                    End If
                End If
                ''===========================================================

                If clsCommon.myLen(ValidateRemark) > 0 Then
                    grow.Cells(colUValidateRemark).Value = ValidateRemark
                    grow.Cells(colUValidateRemark).Style.DrawFill = True
                    grow.Cells(colUValidateRemark).Style.CustomizeFill = True
                    grow.Cells(colUValidateRemark).Style.BackColor = Color.Red

                    validcount += 1
                Else
                    grow.Cells(colUValidateRemark).Value = Nothing
                    grow.Cells(colUValidateRemark).Style.DrawFill = True
                    grow.Cells(colUValidateRemark).Style.CustomizeFill = True
                    grow.Cells(colUValidateRemark).Style.BackColor = Nothing

                    LoadData_InUploaderGrid(grow.Index) ''if no validation fails then update other data in grid.
                End If
            Next ''loop end

            gv_Uploader.Columns(colUValidateRemark).IsPinned = True
            gv_Uploader.Columns(colUValidateRemark).PinPosition = PinnedColumnPosition.Left


            If validcount > 0 Then
                btnValidate.Enabled = True
                btnApplyScheme.Enabled = False
                btnTransferKnockOff.Enabled = False
            Else
                btnValidate.Enabled = False
                btnApplyScheme.Enabled = True
                btnTransferKnockOff.Enabled = True
            End If
            'btnTransferKnockOff.Enabled = False
            btnCalculation.Enabled = False
            btnUploaderSave.Enabled = False

            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Private Function CheckExciseableAccount_For_Uploader() As String
        Dim ValidRemark As String = ""
        Try
            Dim arrIcode As New List(Of String)
            Dim Icode As String = ""
            Dim arr As New ArrayList()
            Dim Unique_Identity As String = ""
            Dim strDocCode As String = ""
            Dim LastIndex As Integer = 0

            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                LastIndex = LastIndex + 1
                Unique_Identity = clsCommon.myCstr(grow.Cells(colUExciseType).Value) + "&" + clsCommon.myCstr(grow.Cells(colUDocDate).Value) + "&" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "&" + clsCommon.myCstr(grow.Cells(ColUToLocation).Value)
                If clsCommon.myLen(Unique_Identity) > 0 AndAlso Not arr.Contains(Unique_Identity) Then
                    arrIcode = New List(Of String)
                    arr.Add(Unique_Identity)

                    ''===============================================================================================
                    Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrIcode)
                    If Not (intx = arrIcode.Count OrElse intx = 0) Then
                        ValidRemark = ValidRemark & "All item should be of Excisable or NonExcisable."
                    End If
                    If intx > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colUTaxGroup1).Value)) <= 0 Then
                            Throw New Exception("Please select tax group.")
                        Else
                            If clsLocation.isLocatinExcisable(clsCommon.myCstr(grow.Cells(ColUToLocation).Value)) = True Then
                                If clsCommon.myLen(grow.Cells(colUTTaxAutCode1).Value) > 0 Then
                                    If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colUTTaxAutCode1).Value + "'")), "Y") = CompairStringResult.Equal Then
                                        Throw New Exception("Atleast One tax should be excisable.")
                                    Else
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                        Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrIcode) + ")"))
                    Else
                        Item_TaxType = 0
                    End If
                    ''===============================================================================================
                End If
                Icode = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                If clsCommon.myLen(Icode) > 0 Then
                    arrIcode.Add(Icode)
                End If

                ''==================================cond. check
                If gv_Uploader.Rows.Count = 1 OrElse gv_Uploader.Rows.Count = LastIndex Then
                    Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrIcode)
                    If Not (intx = arrIcode.Count OrElse intx = 0) Then
                        ValidRemark = ValidRemark & "All item should be of Excisable or NonExcisable."
                    End If
                    If intx > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colUTaxGroup1).Value)) <= 0 Then
                            Throw New Exception("Please select tax group.")
                        Else
                            If clsLocation.isLocatinExcisable(clsCommon.myCstr(grow.Cells(ColUToLocation).Value)) = True Then
                                If clsCommon.myLen(grow.Cells(colUTTaxAutCode1).Value) > 0 Then
                                    If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colUTTaxAutCode1).Value + "'")), "Y") = CompairStringResult.Equal Then
                                        Throw New Exception("Atleast One tax should be excisable.")
                                    Else
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                        Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrIcode) + ")"))
                    Else
                        Item_TaxType = 0
                    End If
                End If
                '------------------------------------------------------------------------------------------------------
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return ValidRemark
    End Function

    Private Sub btnApplyScheme_Click(sender As Object, e As EventArgs) Handles btnApplyScheme.Click
        Try
            gv_Uploader_Temp.Rows.Clear()
            gv_Uploader_Temp.Columns.Clear()
            gv_Uploader_Temp.DataSource = gv_Uploader.DataSource

            For Each grow As GridViewRowInfo In gv_Uploader_Temp.Rows
                FillFreeItemsInGrid_For_Uploader(grow.Index)
            Next

            ''============change positioning of free item==
            For i As Integer = gv_Uploader.Rows.Count - 1 To 0 Step -1
                If clsCommon.myCdbl(gv_Uploader.Rows(i).Cells(colUResetScheme_LineNo).Value) > 0 Then
                    gv_Uploader.Rows.Move(i, clsCommon.myCdbl(gv_Uploader.Rows(i).Cells(colUResetScheme_LineNo).Value))
                End If
            Next
            ''============================================

            RefreshLineNo_Uploader()

            btnValidate.Enabled = False
            btnApplyScheme.Enabled = False
            btnTransferKnockOff.Enabled = True
            btnCalculation.Enabled = False
            btnUploaderSave.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "Scheme for uploader"
    Private Sub isValid_CashScheme_For_Uploader(Optional ByVal IntRow As Integer = 0, Optional ByVal isRowChanges As Boolean = False)
        Dim scheme_Code As String = ""
        Dim isSchemeApply As String = ""
        Dim cash_scheme_code As String = ""
        Dim cash_amt As Decimal = 0
        Dim amount As Decimal = 0

        If Not isRowChanges Then
            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                isSchemeApply = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
                scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                cash_scheme_code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                cash_amt = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
                amount = clsCommon.myCdbl(grow.Cells(colUnitRate).Value) * clsCommon.myCdbl(grow.Cells(colqty).Value)

                '================if cash scheme amount exceed total basic amount than scheme not applicable.
                If cash_amt > amount Then
                    grow.Cells(colCash_Amt).Value = 0
                    grow.Cells(colCash_Pers).Value = 0
                    grow.Cells(colCashSchemeCode).Value = Nothing
                    grow.Cells(colCashSchemeType).Value = Nothing

                    If clsCommon.myLen(scheme_Code) <= 0 Then
                        grow.Cells(colIsSchmItem).Value = Nothing
                        grow.Cells(colSchmCodeType).Value = Nothing
                    End If
                End If
            Next
        Else
            isSchemeApply = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colIsSchmItem).Value)
            scheme_Code = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colSchmCodeType).Value)
            cash_scheme_code = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colCashSchemeCode).Value)
            cash_amt = clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colCash_Amt).Value)
            amount = clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colUnitRate).Value) * clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colqty).Value)

            '================if cash scheme amount exceed total basic amount than scheme not applicable.
            If cash_amt > amount Then
                gv_Uploader.Rows(IntRow).Cells(colCash_Amt).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colCash_Pers).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colCashSchemeCode).Value = Nothing
                gv_Uploader.Rows(IntRow).Cells(colCashSchemeType).Value = Nothing

                If clsCommon.myLen(scheme_Code) <= 0 Then
                    gv_Uploader.Rows(IntRow).Cells(colIsSchmItem).Value = Nothing
                    gv_Uploader.Rows(IntRow).Cells(colSchmCodeType).Value = Nothing
                End If
            End If
        End If ''end row cond
    End Sub

    Private Sub FillFreeItemsInGrid_For_Uploader(ByVal Index As Integer)
        Try
            ''document made on combination of csa,to location and doc date basis
            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then

                For ii As Integer = gv_Uploader.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colLinenno).Value)) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUCSACode).Value)) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colUDocDate).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUDocDate).Value)) = CompairStringResult.Equal _
                        AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(ColUToLocation).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(ColUToLocation).Value)) = CompairStringResult.Equal Then
                        gv_Uploader.Rows.RemoveAt(ii)
                    End If
                Next

                gv_Uploader.Rows(Index).Cells(colSchmCode).Value = Nothing
                gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv_Uploader.Rows(Index).Cells(colMainIcode).Value = Nothing
                gv_Uploader.Rows(Index).Cells(colMainLineNo).Value = "0"
                gv_Uploader.Rows(Index).Cells(colMainIQty).Value = Nothing
                gv_Uploader.Rows(Index).Cells(colMainIUOM).Value = Nothing
                gv_Uploader.Rows(Index).Cells(colFOC).Value = "N"
                gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "N"
                gv_Uploader.CurrentRow.Cells(colCashSchemeCode).Value = Nothing
                gv_Uploader.CurrentRow.Cells(colCashSchemeType).Value = Nothing
                gv_Uploader.CurrentRow.Cells(colCash_Amt).Value = "0"
                gv_Uploader.CurrentRow.Cells(colCash_Pers).Value = "0"

            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then
                    For ii As Integer = gv_Uploader.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colitemcode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colMainLineNo).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colLinenno).Value)) = CompairStringResult.Equal _
                            AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUCSACode).Value)) = CompairStringResult.Equal _
                            AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colUDocDate).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUDocDate).Value)) = CompairStringResult.Equal _
                            AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(ColUToLocation).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(ColUToLocation).Value)) = CompairStringResult.Equal Then
                            gv_Uploader.Rows.RemoveAt(ii)
                        End If
                    Next
                End If
            End If

            If clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value) <= 0 Then
                Exit Sub
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value), "N") <> CompairStringResult.Equal Then
                Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colitemcode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colItemUOM).Value), clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUCSACode).Value), Nothing, clsCommon.myCDate(gv_Uploader.Rows(Index).Cells(colUDocDate).Value))
                If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then
                    gv_Uploader.Rows(Index).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                    gv_Uploader.Rows(Index).Cells(colCashSchemeType).Value = obj_Cash.schm_Type
                    gv_Uploader.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    gv_Uploader.Rows(Index).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers

                    If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                        gv_Uploader.Rows(Index).Cells(colCashSchemeType).Value = "P"
                        gv_Uploader.Rows(Index).Cells(colCash_Amt).Value = (clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value) * clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colUnitRate).Value) * obj_Cash.Cash_Pers) / 100
                    Else
                        gv_Uploader.Rows(Index).Cells(colCashSchemeType).Value = "A"
                        gv_Uploader.Rows(Index).Cells(colCash_Pers).Value = (100 * clsCommon.myCdbl(obj_Cash.Cash_Amt)) / (clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value) * clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colUnitRate).Value))
                        gv_Uploader.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                    End If

                    gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                Else
                    gv_Uploader.Rows(Index).Cells(colCashSchemeCode).Value = ""
                    gv_Uploader.Rows(Index).Cells(colCashSchemeType).Value = ""
                    gv_Uploader.Rows(Index).Cells(colCash_Amt).Value = 0
                    gv_Uploader.Rows(Index).Cells(colCash_Pers).Value = 0
                    If clsCommon.myLen(gv_Uploader.Rows(Index).Cells(colSchmCode).Value) > 0 Then
                        gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    Else
                        gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = ""
                    End If
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value), "N") <> CompairStringResult.Equal Then

                Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colitemcode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colItemUOM).Value), clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value), Nothing, clsCommon.myCDate(gv_Uploader.Rows(Index).Cells(colUDocDate).Value))
                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                    For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                        '--------------update free itemcode in main item row------------------
                        gv_Uploader.Rows(Index).Cells(colSchmCode).Value = Nothing
                        gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                        gv_Uploader.Rows(Index).Cells(colMainIcode).Value = objtr.Schm_Icode
                        gv_Uploader.Rows(Index).Cells(colMainIQty).Value = objtr.Schm_Qty
                        gv_Uploader.Rows(Index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                        gv_Uploader.Rows(Index).Cells(colFOC).Value = "N"
                        gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        '-------------------------------------------------------------

                        gv_Uploader.Rows.AddNew()

                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUExciseType).Value = gv_Uploader.Rows(Index).Cells(colUExciseType).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDocCode).Value = gv_Uploader.Rows(Index).Cells(colUDocCode).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDocDate).Value = gv_Uploader.Rows(Index).Cells(colUDocDate).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDesc).Value = gv_Uploader.Rows(Index).Cells(colUDesc).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUCSACode).Value = gv_Uploader.Rows(Index).Cells(colUCSACode).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUBillToLocation).Value = gv_Uploader.Rows(Index).Cells(colUBillToLocation).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(ColUToLocation).Value = gv_Uploader.Rows(Index).Cells(ColUToLocation).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDocAmount).Value = gv_Uploader.Rows(Index).Cells(colUDocAmount).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTaxGroup1).Value = gv_Uploader.Rows(Index).Cells(colUTaxGroup1).Value

                        For ij As Integer = 0 To 9
                            gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells("UBASEAMT" & clsCommon.myCstr(ij + 1)).Value = gv_Uploader.Rows(Index).Cells("UBASEAMT" & clsCommon.myCstr(ij + 1)).Value
                            gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells("UAuthCode" & clsCommon.myCstr(ij + 1)).Value = gv_Uploader.Rows(Index).Cells("UAuthCode" & clsCommon.myCstr(ij + 1)).Value
                            gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells("UAuthCode" & clsCommon.myCstr(ij + 1)).Value = gv_Uploader.Rows(Index).Cells("UAuthCode" & clsCommon.myCstr(ij + 1)).Value
                            gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells("Utaxamt" & clsCommon.myCstr(ij + 1)).Value = gv_Uploader.Rows(Index).Cells("Utaxamt" & clsCommon.myCstr(ij + 1)).Value
                        Next

                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTermCode).Value = gv_Uploader.Rows(Index).Cells(colUTermCode).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDueDate).Value = gv_Uploader.Rows(Index).Cells(colUDueDate).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDoc_Amt_WO_Disc).Value = gv_Uploader.Rows(Index).Cells(colUDoc_Amt_WO_Disc).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUAmt_After_Disc).Value = gv_Uploader.Rows(Index).Cells(colUAmt_After_Disc).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTaxAmt).Value = gv_Uploader.Rows(Index).Cells(colUTaxAmt).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTCommsnAmt).Value = gv_Uploader.Rows(Index).Cells(colUTCommsnAmt).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTFreightAmt).Value = gv_Uploader.Rows(Index).Cells(colUTFreightAmt).Value
                        'gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUTOtherAmt).Value = gv_Uploader.Rows(Index).Cells(colUTOtherAmt).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUDocAmount).Value = gv_Uploader.Rows(Index).Cells(colUDocAmount).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colURoundOff).Value = gv_Uploader.Rows(Index).Cells(colURoundOff).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(coldate).Value = gv_Uploader.Rows(Index).Cells(coldate).Value
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colBookingno).Value = ""
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colitemcode).Value = objtr.Schm_Icode
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colConversionFactor).Value = 1
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colPackSize).Value = 1
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMasterPackSize).Value = 1
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colItemType).Value = LoadItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.Schm_Icode + "'")))
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colCSAType).Value = objtr.Schm_Item_CSA_Type
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colItemUOM).Value = objtr.Schm_Item_Uom
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colqty).Value = objtr.Schm_Qty
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colAltUOM).Value = ""
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colAltQty).Value = "0"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colbalqty).Value = "0"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colbookingrate).Value = "0"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colincludingtax).Value = clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colincludingtax).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUnitRate).Value = "0"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colTaxBasis).Value = clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colTaxBasis).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMainIcode).Value = clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colitemcode).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMainLineNo).Value = clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colLinenno).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMainIQty).Value = clsCommon.myCdbl(gv_Uploader.Rows(Index).Cells(colqty).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMainIUOM).Value = clsCommon.myCstr(gv_Uploader.Rows(Index).Cells(colItemUOM).Value)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colFOC).Value = "Y"
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colDisPer).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colDisAmt).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colCommision).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colCommisionValue).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colFrghtRate).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colFrghtAmt).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colOtherCharge).Value = 0
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(coldoubleclick).Value = "Double Click"

                        CalUnitPrice_For_Uploader(gv_Uploader.Rows.Count - 1, True)
                        SetitemWiseTaxSetting_For_Uploader(True, gv_Uploader.Rows.Count - 1)
                        'UpdateCurrentRow(gv_Uploader.Rows.Count - 1)
                        CommisionValue_For_Uploader(gv_Uploader.Rows.Count - 1)
                        'UpdateAllTotals()

                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colBookingno).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(coldate).ReadOnly = True
                        'gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colbookingtype).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colitemcode).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colMasterPackSize).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colPackSize).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colItemUOM).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colqty).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colincludingtax).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUnitRate).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colCommision).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colCommisionValue).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colFrghtRate).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colFrghtAmt).ReadOnly = True
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colOtherCharge).ReadOnly = True

                        'gv_Uploader.Rows.Move(gv_Uploader.Rows.Count - 1, Index + 1)
                        gv_Uploader.Rows(gv_Uploader.Rows.Count - 1).Cells(colUResetScheme_LineNo).Value = Index + 1
                    Next
                Else
                    gv_Uploader.Rows(Index).Cells(colSchmCode).Value = Nothing
                    gv_Uploader.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                    gv_Uploader.Rows(Index).Cells(colMainIcode).Value = Nothing
                    gv_Uploader.Rows(Index).Cells(colMainLineNo).Value = "0"
                    gv_Uploader.Rows(Index).Cells(colMainIQty).Value = Nothing
                    gv_Uploader.Rows(Index).Cells(colMainIUOM).Value = Nothing
                    gv_Uploader.Rows(Index).Cells(colFOC).Value = "N"
                    gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "N"
                    If clsCommon.myLen(gv_Uploader.Rows(Index).Cells(colCashSchemeCode).Value) > 0 Then
                        gv_Uploader.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#End Region

#Region "Calculation Function"
    Private Sub CalUnitPrice_For_Uploader(ByVal XR As Integer, ByVal CellChanged As Boolean)
        Try
            Dim diffrate As Decimal = 0
            Dim orgrate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim uom As String = ""
            Dim cnvrsn As Decimal = 1
            Dim csauom As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable()
            Dim MRP As Double = Nothing
            gv_Uploader.Rows(XR).Cells(colCSAType).Value = clsCommon.myCstr(clsItemMaster.GetItemCSAType(clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colitemcode).Value), Nothing))
            Dim CurrntCPDType As String = clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colCSAType).Value) 'CPD-DESI GHEE
            Dim CSA_State As String = clsCSAPriceMaster.GetCSAState(gv_Uploader.Rows(XR).Cells(colUCSACode).Value)

            If CellChanged Then
                'If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                '    gv_Uploader.Rows(XR).Cells(colUnitRate).Value = 0
                '    Exit Sub
                'End If

                If clsCommon.myLen(gv_Uploader.Rows(XR).Cells(colincludingtax).Value) <= 0 Then
                    gv_Uploader.Rows(XR).Cells(colincludingtax).Value = "No"
                    gv_Uploader.Rows(XR).Cells(colTaxBasis).Value = "Forward Calculation"
                End If

                uom = clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colItemUOM).Value)
                qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                '========(unit price=chart rate*base unit conversion of chart/calc.unit conversion)------------

                qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colitemcode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colincludingtax).Value) + "' and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(ColUToLocation).Value) + "') "
                If CSAPricePostedData = True Then
                    qry += " and Tspl_CSA_Price_Head.Posted='1' "
                End If

                ''============when setting ON and item is not CPD then other price chat apply
                If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                    qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colUCSACode).Value) + "' "
                Else
                    qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colCSAType).Value) + "' and tspl_csa_price_state_detail.state_code='" + CSA_State + "' "
                End If

                If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                    qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(gv_Uploader.Rows(XR).Cells(colUDocDate).Value), "dd/MMM/yyyy") + "' "
                End If
                ''end here=============================
                'done by stuti on 08/12/2016
                qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                '==============end here=========================
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                    diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                    MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                    Dim csaconversion As Decimal = 0

                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                        orgrate = clsCommon.myCdbl(diffrate)
                    Else
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colitemcode).Value) + "' and uom_code='" + csauom + "'"))

                        If Apply_PriceChat_On_OtherItems Then
                            gv_Uploader.Rows(XR).Cells(colbookingrate).Value = 0
                        End If
                        orgrate = (clsCommon.myCdbl(gv_Uploader.Rows(XR).Cells(colbookingrate).Value) + clsCommon.myCdbl(diffrate))

                        If ForUDLOnly Then
                            orgrate = (clsCommon.myCdbl(dt.Rows(0)("csa_rate")) + clsCommon.myCdbl(diffrate))
                        End If
                    End If
                    If csaconversion <= 0 Then
                        csaconversion = 1
                    End If


                    orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                    If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                        gv_Uploader.Rows(XR).Cells(colUnitRate).Value = 0
                    Else
                        gv_Uploader.Rows(XR).Cells(colUnitRate).Value = orgrate
                    End If

                    gv_Uploader.Rows(XR).Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)
                Else
                    If clsCommon.myCdbl(gv_Uploader.Rows(XR).Cells(colUnitRate).Value) <= 0 Then
                        gv_Uploader.Rows(XR).Cells(colUnitRate).Value = 0
                    End If
                End If

                If Apply_PriceChat_On_OtherItems AndAlso Not BookingEffectOnSale Then
                    gv_Uploader.Rows(XR).Cells(colbookingrate).Value = clsCommon.myCdbl(gv_Uploader.Rows(XR).Cells(colUnitRate).Value)
                End If

            Else

                For Each grow As GridViewRowInfo In gv_Uploader.Rows
                    'If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                    '    grow.Cells(colUnitRate).Value = 0
                    '    Continue For
                    'End If

                    If clsCommon.myLen(grow.Cells(colincludingtax).Value) <= 0 Then
                        grow.Cells(colincludingtax).Value = "No"
                        grow.Cells(colTaxBasis).Value = "Forward Calculation"
                    End If

                    uom = clsCommon.myCstr(grow.Cells(colItemUOM).Value)
                    qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"
                    cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If

                    If clsCommon.myLen(uom) <= 0 Then
                        Continue For
                    End If

                    qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                    qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                    qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(grow.Cells(colincludingtax).Value) + "'"
                    qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + clsCommon.myCstr(grow.Cells(ColUToLocation).Value) + "')"
                    If CSAPricePostedData = True Then
                        qry += " and Tspl_CSA_Price_Head.Posted='1' "
                    End If

                    ''============when setting ON and item is not CPD then other price chat apply
                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "' "
                    Else
                        qry += " and tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv_Uploader.Rows(XR).Cells(colCSAType).Value) + "' "
                    End If

                    If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                        qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colUDocDate).Value), "dd/MMM/yyyy") + "' "
                    End If
                    ''===============end here============================================
                    'done by stuti on 08/12/2016
                    qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                    '==============end here=========================

                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                        diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                        MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                        Dim csaconversion As Decimal = 0

                        If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                            orgrate = clsCommon.myCdbl(diffrate)
                        Else
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "' and uom_code='" + csauom + "'"))
                            If Apply_PriceChat_On_OtherItems Then
                                gv_Uploader.Rows(XR).Cells(colbookingrate).Value = 0
                            End If
                            orgrate = (clsCommon.myCdbl(gv_Uploader.Rows(XR).Cells(colbookingrate).Value) + clsCommon.myCdbl(diffrate))

                            If ForUDLOnly Then
                                orgrate = (clsCommon.myCdbl(dt.Rows(0)("csa_rate")) + clsCommon.myCdbl(diffrate))
                            End If
                        End If

                        If csaconversion <= 0 Then
                            csaconversion = 1
                        End If

                        orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                            grow.Cells(colUnitRate).Value = 0
                        Else
                            grow.Cells(colUnitRate).Value = orgrate
                        End If

                        grow.Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)
                    Else
                        If clsCommon.myCdbl(grow.Cells(colUnitRate).Value) <= 0 Then
                            grow.Cells(colUnitRate).Value = 0
                        End If
                    End If

                    If Apply_PriceChat_On_OtherItems AndAlso Not BookingEffectOnSale Then
                        grow.Cells(colbookingrate).Value = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                    End If
                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RefreshLineNo_Uploader()
        If gv_Uploader.Rows.Count > 0 Then
            Dim arr As New ArrayList()
            Dim Unique_Identity As String = ""
            Dim xcount As Integer = 1

            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                Unique_Identity = clsCommon.myCstr(grow.Cells(colUExciseType).Value) + "&" + clsCommon.myCstr(grow.Cells(colUDocDate).Value) + "&" + clsCommon.myCstr(grow.Cells(colUCSACode).Value) + "&" + clsCommon.myCstr(grow.Cells(ColUToLocation).Value)

                If clsCommon.myLen(Unique_Identity) > 0 AndAlso Not arr.Contains(Unique_Identity) Then
                    xcount = 1
                    arr.Add(Unique_Identity)
                End If
                grow.Cells(colLinenno).Value = xcount
                xcount += 1
            Next
        End If
    End Sub

    Private Sub CalAltQty_for_Uploader(ByVal CurrentRow As Integer)
        Try
            Dim qty As Decimal = clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colqty).Value)
            Dim uom As String = clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colItemUOM).Value)
            Dim altuom As String = clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colAltUOM).Value)
            Dim bookingrate As String = clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colbookingrate).Value)

            Dim altconversion As Decimal = 0
            altconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "' and uom_code='" + altuom + "'"))

            Dim conversion As Decimal = 0
            conversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "' and uom_code='" + uom + "'"))

            Dim altqty As Decimal = 0

            If altconversion > 0 Then
                altqty = System.Math.Round((qty * conversion) / altconversion, 2)
            Else
                altqty = 0
            End If
            gv_Uploader.Rows(CurrentRow).Cells(colAltQty).Value = altqty

            If BookingEffectOnSale AndAlso clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colbalqty).Value) < altqty AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then ' AndAlso ChkFOC.Checked = False
                gv_Uploader.Rows(CurrentRow).Cells(colqty).Value = 0
                'Throw New Exception("Alt quantity can not be more than balance quantity i.e (" + clsCommon.myCstr(gv.Rows(CurrentRow).Cells(colbalqty).Value) + ")")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CalConversionFactor_Uploader(ByVal CurrentRow As Integer)
        gv_Uploader.Rows(CurrentRow).Cells(colConversionFactor).Value = 1

        If clsCommon.myLen(gv_Uploader.Rows(CurrentRow).Cells(colItemUOM).Value) > 0 Then
            Dim altuomcnvrsn As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where uom_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colAltUOM).Value) + "' and item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "'"))
            Dim cnvrsn As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where uom_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colItemUOM).Value) + "' and item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "'"))

            If cnvrsn > 0 Then
                gv_Uploader.Rows(CurrentRow).Cells(colConversionFactor).Value = System.Math.Round(altuomcnvrsn / cnvrsn, 2)
            Else
                gv_Uploader.Rows(CurrentRow).Cells(colConversionFactor).Value = 0
            End If
        End If
        If clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colConversionFactor).Value) <= 0 Then
            gv_Uploader.Rows(CurrentRow).Cells(colConversionFactor).Value = 1
        End If
    End Sub

    Private Function CommisionValue_For_Uploader(ByVal CurrentRow As Integer) As String
        Dim validremarks As String = Nothing
        Try
            Dim ItemCode As String = clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value)

            If clsCommon.myLen(ItemCode) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                Return validremarks
            End If
            Dim commsnrate As Decimal = clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colCommision).Value)
            Dim commsnuom As String = ""
            Dim FreightUOM As String = ""
            Dim Master_Sku As Decimal = 0
            Dim Master_Sku_Freight As Decimal = 0
            Dim qry As String = ""

            If ApplyFreight_Cmmsn_Charge_Itemwise Then
                ''if freight and commission itemwise then below code run
                commsnuom = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Commission_uom from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colUCSACode).Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code='" + ItemCode + "' "))
                FreightUOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Freight_uom from TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL left outer join TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD on TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Doc_No=TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Doc_No where TSPL_ITEM_COMMISSION_FREIGHT_AC_HEAD.Cust_Code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colUCSACode).Value) + "' and TSPL_ITEM_COMMISSION_FREIGHT_AC_DETAIL.Item_Code='" + ItemCode + "' "))

                If clsCommon.myLen(FreightUOM) <= 0 AndAlso clsCommon.myLen(commsnuom) <= 0 Then
                    validremarks = validremarks & "Fill Either Commission or Freight UOM not defined, for Item " & clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) & ""
                End If

                If clsCommon.myLen(FreightUOM) <= 0 AndAlso clsCommon.myLen(commsnuom) <= 0 Then
                    validremarks = validremarks & "Fill Either Commission or Freight UOM not defined for location " & clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) & ""
                End If
            Else
                commsnuom = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CSA_Commision_Type from TSPL_LOCATION_MASTER where Cust_Code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colUCSACode).Value) + "' and location_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colUBillToLocation).Value) + "'"))
                If clsCommon.myLen(commsnuom) <= 0 Then
                    validremarks = validremarks & "Commission Rate UOM not defined for location " & txtCSAloc_code.Value & ""
                End If
            End If


            Dim Weight_value As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Weight_Value from tspl_item_master where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "' "))
            Dim weight_uom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_uom from tspl_item_master where item_code='" + clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value) + "' "))
            If clsCommon.myLen(weight_uom) <= 0 Then
                validremarks = validremarks & "Weight UOM not defined for Item  " & gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value & ""
            End If

            Dim convFacter As Decimal = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value), clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colItemUOM).Value), Nothing))
            Weight_value = Weight_value * convFacter
            gv_Uploader.Rows(CurrentRow).Cells(colPackSize).Value = Weight_value

            If clsCommon.myLen(commsnuom) > 0 Then
                qry = "select top 1 CF from (select (case when (Container_UOM='" & commsnuom & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & commsnuom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value), Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                Master_Sku = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            End If

            If Master_Sku = 0 Then
                Master_Sku = 1
            End If

            gv_Uploader.Rows(CurrentRow).Cells(colMasterPackSize).Value = Master_Sku

            Try
                If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colComm_Type_RS_Pers).Value), "P") = CompairStringResult.Equal Then
                    If CalculateCommOnCSATransWOConversion = 0 Then
                        gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round((clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) * (clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colMasterPackSize).Value) = 0, 1, clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colMasterPackSize).Value))) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colCommision).Value)) / 100, 2)
                    Else
                        gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round((clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colCommision).Value)) / 100, 2)
                    End If
                Else
                    gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colqty).Value) * (clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colMasterPackSize).Value) = 0, 1, clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colMasterPackSize).Value))) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colCommision).Value), 2) '* clsCommon.myCdbl(gv_uploader.CurrentRow.Cells(colConversionFactor).Value)
                End If

            Catch exx As Exception
                gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value = 0
            End Try

            If clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) < clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value) Then
                gv_Uploader.Rows(CurrentRow).Cells(colCommisionValue).Value = 0
            End If

            ''=========================================================================
            If ApplyFreight_Cmmsn_Charge_Itemwise Then
                If clsCommon.myLen(FreightUOM) > 0 Then
                    qry = "select top 1 CF from (select (case when (Container_UOM='" & FreightUOM & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & FreightUOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colitemcode).Value), Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                    Master_Sku_Freight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                End If

                If Master_Sku_Freight = 0 Then
                    Master_Sku_Freight = 1
                End If

                Try
                    If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(CurrentRow).Cells(colFrghtType).Value), "P") = CompairStringResult.Equal Then
                        If CalculateCommOnCSATransWOConversion = 0 Then
                            gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round((clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) * (clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(Master_Sku_Freight) = 0, 1, clsCommon.myCdbl(Master_Sku_Freight))) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colFrghtRate).Value)) / 100, 2)
                        Else
                            gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round((clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colFrghtRate).Value)) / 100, 2)
                        End If
                    Else
                        gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colqty).Value) * (clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colPackSize).Value) / IIf(clsCommon.myCdbl(Master_Sku_Freight) = 0, 1, clsCommon.myCdbl(Master_Sku_Freight))) * clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colFrghtRate).Value), 2) '* clsCommon.myCdbl(gv_uploader.CurrentRow.Cells(colConversionFactor).Value)
                    End If

                Catch exx As Exception
                    gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value = 0
                End Try

                If clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colSaleValue).Value) < clsCommon.myCdbl(gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value) Then
                    gv_Uploader.Rows(CurrentRow).Cells(colFrghtAmt).Value = 0
                End If
            End If
            ''=========================================================================

            Return validremarks
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub BlankTaxDetails_For_Uploader(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        gv_Uploader.Rows(intRowNo).Cells(colTax1).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt1).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate1).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt1).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable1).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax1).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode1).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax2).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt2).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate2).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt2).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable2).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax2).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode2).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax3).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt3).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate3).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt3).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable3).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax3).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode3).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax4).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt4).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate4).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt4).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable4).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax4).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode4).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax5).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt5).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate5).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt5).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable5).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax5).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode5).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax6).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt6).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate6).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt6).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable6).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax6).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode6).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax7).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt7).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate7).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt7).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable7).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax7).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode7).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax8).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt8).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate8).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt8).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable8).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax8).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode8).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax9).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt9).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate9).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt9).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable9).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax9).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode9).Value = Nothing

        gv_Uploader.Rows(intRowNo).Cells(colTax10).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colTaxBaseAmt10).Value = Nothing
        If isBlankRate Then
            gv_Uploader.Rows(intRowNo).Cells(colTaxRate10).Value = Nothing
        End If
        gv_Uploader.Rows(intRowNo).Cells(colTaxAmt10).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsTaxable10).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colIsSurTax10).Value = Nothing
        gv_Uploader.Rows(intRowNo).Cells(colSurTaxCode10).Value = Nothing
    End Sub

    Private Function SetTaxDetails_For_Uploader(ByVal IntRow As Integer) As String
        Dim ValidRemark As String = ""
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUTaxGroup1).Value), "S", clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(ColUToLocation).Value), OpenALLTaxes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                ValidRemark = ValidRemark & "Can't Handle More than 10 Tax Types in a Group" + Environment.NewLine
                Return ValidRemark
            End If
            Dim i As Integer = 1
            For Each dr As DataRow In dt.Rows
                gv_Uploader.Rows(IntRow).Cells("UAuthCode" & clsCommon.myCstr(i)).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv_Uploader.Rows(IntRow).Cells("UTaxRate" & clsCommon.myCstr(i)).Value = clsCommon.myCdbl(dr("TaxRate"))
                i = i + 1
            Next
            SetitemWiseTaxSetting_For_Uploader(True, IntRow)
        Else
            For ii As Integer = 0 To gv_Uploader.Rows.Count - 1
                BlankTaxDetails_For_Uploader(ii, True)
            Next
        End If

        Return ValidRemark
    End Function

    Sub SetitemWiseTaxSetting_For_Uploader(ByVal isChangeRate As Boolean, ByVal IntRow As Integer)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUTaxGroup1).Value), "S", clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value), clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(ColUToLocation).Value), OpenALLTaxes)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If clsCommon.myLen(gv_Uploader.Rows(IntRow).Cells(colitemcode).Value) > 0 Then
                BlankTaxDetails_For_Uploader(IntRow, isChangeRate)
                For i As Integer = 0 To 9
                    ''1
                    If dt.Rows.Count > i Then
                        gv_Uploader.Rows(IntRow).Cells("tax" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCstr(dt.Rows(i)("Tax_Code"))
                        If isChangeRate Then
                            gv_Uploader.Rows(IntRow).Cells("taxRate" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCdbl(dt.Rows(i)("TaxRate"))
                        End If
                        gv_Uploader.Rows(IntRow).Cells("taxable" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("Taxable")), "Y") = CompairStringResult.Equal)
                        gv_Uploader.Rows(IntRow).Cells("surTax" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("Surtax")), "Y") = CompairStringResult.Equal)
                        gv_Uploader.Rows(IntRow).Cells("surTaxCode" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCstr(dt.Rows(i)("Surtax_Tax_Code"))
                        gv_Uploader.Rows(IntRow).Cells("ex" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("Excisable")), "Y") = CompairStringResult.Equal)
                        gv_Uploader.Rows(IntRow).Cells("recvrabletax" & clsCommon.myCstr(i + 1)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i)("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                    End If
                Next
            End If
        End If
        dt = Nothing
    End Sub

    Private Sub UpdateCurrentRow_for_Uploader(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)

            Dim dblFAmt As Double = 0
            gv_Uploader.Rows(IntRowNo).Cells(colSaleRate).Value = GetTransferRate_for_Uploader(IntRowNo)

            Dim dblQty As Double = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colqty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colUnitRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate) ''+ dblFAmt


            ''======================excise=====================================
            If ExciseentryOnSale AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colUExciseType).Value), "E") = CompairStringResult.Equal Then ''if excisable then
                gv_Uploader.Rows(IntRowNo).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colitemcode).Value), Nothing)
                gv_Uploader.Rows(IntRowNo).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
                gv_Uploader.Rows(IntRowNo).Cells(colAbatementAmt).Value = ((clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colMRP).Value) * clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colAbatementPers).Value)) / 100) * clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colqty).Value)
                dblAmt = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colAbatementAmt).Value)
            End If
            ''===================================================


            Dim dblDisPer As Double = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = (dblQty * dblRate * dblDisPer) / 100

            Dim dblTotDiscAmt = dblDisAmt
            Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt

            '------------------cash amount minus---------------
            Dim dblCash_Amt As Double = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colCash_Amt).Value)
            dblAmtAfterDis = dblAmtAfterDis - dblCash_Amt

            Dim isManditax As String = ""

            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then

                    Dim strTaxCode As String = ""
                    If ii = 1 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax1).Value)
                    ElseIf ii = 2 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax2).Value)
                    ElseIf ii = 3 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax3).Value)
                    ElseIf ii = 4 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax4).Value)
                    ElseIf ii = 5 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax5).Value)
                    ElseIf ii = 6 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax6).Value)
                    ElseIf ii = 7 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax7).Value)
                    ElseIf ii = 8 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax8).Value)
                    ElseIf ii = 9 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax9).Value)
                    ElseIf ii = 10 Then
                        strTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax10).Value)
                    End If

                    isManditax = clsCSASaleInvoiceItem.MandiTax(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colCSAType).Value), strTaxCode)

                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dblTaxRate As Double = 0
                        Dim IsSurTax As Boolean
                        Dim strSurTaxCode As String = ""
                        Dim IsTaxable As Boolean
                        Dim IsExcisable As Boolean
                        If ii = 1 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate1).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax1).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode1).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable1).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable1).Value)
                        ElseIf ii = 2 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate2).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax2).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode2).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable2).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable2).Value)
                        ElseIf ii = 3 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate3).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax3).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode3).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable3).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable3).Value)
                        ElseIf ii = 4 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate4).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax4).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode4).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable4).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable4).Value)
                        ElseIf ii = 5 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate5).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax5).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode5).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable5).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable5).Value)
                        ElseIf ii = 6 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate6).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax6).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode6).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable6).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable6).Value)
                        ElseIf ii = 7 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate7).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax7).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode7).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable7).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable7).Value)
                        ElseIf ii = 8 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate8).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax8).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode8).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable8).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable8).Value)
                        ElseIf ii = 9 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate9).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax9).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode9).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable9).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable9).Value)
                        ElseIf ii = 10 Then
                            dblTaxRate = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxRate10).Value)
                            IsSurTax = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax10).Value)
                            strSurTaxCode = clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode10).Value)
                            IsTaxable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable10).Value)
                            IsExcisable = clsCommon.myCBool(gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable10).Value)
                        End If

                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0


                        If IsSurTax Then
                            Dim dblSurTaxAmt As Double = 0
                            dblBaseAmt = 0

                            dblSurTaxAmt = GetCurrentRowSurTaxAmt_For_Uploader(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            dblBaseAmt = 0

                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt_For_Uploader(IntRowNo, Strii, arrTaxableAuth)

                            ''====================excise work===================
                            If ExciseentryOnSale AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colUExciseType).Value), "E") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                                    dblBaseAmt = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colAbatementAmt).Value) - dblOtherTaxAmt
                                Else
                                    dblBaseAmt = clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colAbatementAmt).Value) + dblOtherTaxAmt
                                End If
                            Else
                                If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                                    dblBaseAmt = (dblAmtAfterDis - dblOtherTaxAmt)
                                Else
                                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                                End If
                            End If
                            ''==================================================
                        End If

                        'gv_uploader.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxBaseAmt" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        If ii = 1 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt1).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 2 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt2).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 3 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt3).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 4 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt4).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 5 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt5).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 6 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt6).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 7 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt7).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 8 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt8).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 9 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt9).Value = Math.Round(dblBaseAmt, 2)
                        ElseIf ii = 10 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt10).Value = Math.Round(dblBaseAmt, 2)
                        End If

                        'If isManditax = "Y" Then
                        '    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        'Else
                        '    dblTaxAmt = 0
                        'End If

                        If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                            If isManditax = "Y" Then
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTaxRate)
                            Else
                                dblTaxAmt = 0
                            End If
                        Else
                            If isManditax = "Y" Then
                                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                            Else
                                dblTaxAmt = 0
                            End If
                        End If

                        'gv_uploader.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
                        If ii = 1 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 2 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 3 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 4 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 5 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 6 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 7 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 8 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 9 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value = Math.Round(dblTaxAmt, 2)
                        ElseIf ii = 10 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value = Math.Round(dblTaxAmt, 2)
                        End If
                        If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If
                    Else
                        If ii = 1 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable1).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable1).Value = Nothing
                        ElseIf ii = 2 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable2).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable2).Value = Nothing
                        ElseIf ii = 3 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable3).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable3).Value = Nothing
                        ElseIf ii = 4 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable4).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable4).Value = Nothing
                        ElseIf ii = 5 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable5).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable5).Value = Nothing
                        ElseIf ii = 6 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable6).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable6).Value = Nothing
                        ElseIf ii = 7 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable7).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable7).Value = Nothing
                        ElseIf ii = 8 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable8).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable8).Value = Nothing
                        ElseIf ii = 9 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable9).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable9).Value = Nothing
                        ElseIf ii = 10 Then
                            gv_Uploader.Rows(IntRowNo).Cells(colTax10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxBaseAmt10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxRate10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsSurTax10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colSurTaxCode10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsTaxable10).Value = Nothing
                            gv_Uploader.Rows(IntRowNo).Cells(colIsExcisable10).Value = Nothing
                        End If
                    End If
                ElseIf rbtnTaxCalManual.IsChecked Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv_Uploader.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblsalevalue As Double = clsCommon.myCdbl(gv_Uploader.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colUnitRate).Value) * clsCommon.myCdbl(gv_Uploader.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colqty).Value)
                    Dim dblCurrRowAmt As Double = dblsalevalue
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv_Uploader.Rows.Count - 1
                        dblTotAmt += dblsalevalue
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If

                    If ii = 1 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value = dblCurrCalTax
                    ElseIf ii = 2 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value = dblCurrCalTax
                    ElseIf ii = 3 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value = dblCurrCalTax
                    ElseIf ii = 4 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value = dblCurrCalTax
                    ElseIf ii = 5 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value = dblCurrCalTax
                    ElseIf ii = 6 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value = dblCurrCalTax
                    ElseIf ii = 7 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value = dblCurrCalTax
                    ElseIf ii = 8 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value = dblCurrCalTax
                    ElseIf ii = 9 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value = dblCurrCalTax
                    ElseIf ii = 10 Then
                        gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value = dblCurrCalTax
                    End If

                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt_for_Uploader(IntRowNo)
            Dim dblAmtAfterTax As Double = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
                dblAmtAfterTax = dblAmtAfterDis
            Else
                dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt
            End If


            gv_Uploader.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv_Uploader.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv_Uploader.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv_Uploader.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
            gv_Uploader.Rows(IntRowNo).Cells(colSaleRate).Value = GetTransferRate_for_Uploader(IntRowNo)

            gv_Uploader.Rows(IntRowNo).Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colSaleValue).Value) - clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colstckratevalue).Value), 2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetTransferRate_for_Uploader(ByVal intRow As Integer)
        Dim totalTax As Double = 0
        Dim OtherCharges As Double = 0
        Dim commisionrate As Double = 0
        Dim totAmount As Double = 0
        Dim TransferRate As Double = 0
        totalTax = clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colTotTaxAmt).Value)
        OtherCharges = clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colOtherCharge).Value)
        commisionrate = 0 ' clsCommon.myCdbl(gv.Rows(intRow).Cells(colCommisionValue).Value)
        totAmount = clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value) * clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colUnitRate).Value) - clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colCash_Amt).Value)
        If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(intRow).Cells(colTaxBasis).Value), "Back Calculation") = CompairStringResult.Equal Then
            TransferRate = System.Math.Round((totAmount - totalTax - OtherCharges - commisionrate) / IIf(clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value) = 0, 1, clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value)), 2)
        Else
            TransferRate = System.Math.Round((totAmount) / IIf(clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value) = 0, 1, clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value)), 2)
        End If
        gv_Uploader.Rows(intRow).Cells(colSaleValue).Value = Math.Round(TransferRate * clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value), 2)

        ''for UDL only=====================
        If Apply_PriceChat_On_OtherItems Then
            gv_Uploader.Rows(intRow).Cells(colSaleValue).Value = Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colstckratevalue).Value), 2)
            If clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value) > 0 Then
                TransferRate = Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colstckratevalue).Value) / clsCommon.myCdbl(gv_Uploader.Rows(intRow).Cells(colqty).Value), 2)
            Else
                TransferRate = 0
            End If
        End If
        ''====================================================

        CommisionValue_For_Uploader(intRow)
        Return TransferRate
    End Function

    Private Function GetCurrentRowTotalTaxAmt_for_Uploader(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If ii = 1 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value)
            ElseIf ii = 2 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value)
            ElseIf ii = 3 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value)
            ElseIf ii = 4 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value)
            ElseIf ii = 5 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value)
            ElseIf ii = 6 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value)
            ElseIf ii = 7 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value)
            ElseIf ii = 8 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value)
            ElseIf ii = 9 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value)
            ElseIf ii = 10 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value)
            End If
        Next
        Return dblTotTax
    End Function

    Private Function GetCurrentRowOtherTaxAmt_For_Uploader(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If ii = 1 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax1).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value)
                    End If
                ElseIf ii = 2 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax2).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value)
                    End If
                ElseIf ii = 3 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax3).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value)
                    End If
                ElseIf ii = 4 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax4).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value)
                    End If
                ElseIf ii = 5 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax5).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value)
                    End If
                ElseIf ii = 6 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax6).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value)
                    End If
                ElseIf ii = 7 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax7).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value)
                    End If
                ElseIf ii = 8 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax8).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value)
                    End If
                ElseIf ii = 9 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax9).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value)
                    End If
                ElseIf ii = 10 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax10).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Function GetCurrentRowSurTaxAmt_For_Uploader(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If ii = 1 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax1).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt1).Value)
                End If
            ElseIf ii = 2 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax2).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt2).Value)
                End If
            ElseIf ii = 3 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax3).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt3).Value)
                End If
            ElseIf ii = 4 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax4).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt4).Value)
                End If
            ElseIf ii = 5 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax5).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt5).Value)
                End If
            ElseIf ii = 6 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax6).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt6).Value)
                End If
            ElseIf ii = 7 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax7).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt7).Value)
                End If
            ElseIf ii = 8 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax8).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt8).Value)
                End If
            ElseIf ii = 9 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax9).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt9).Value)
                End If
            ElseIf ii = 10 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv_Uploader.Rows(IntRowNo).Cells(colTax10).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv_Uploader.Rows(IntRowNo).Cells(colTaxAmt10).Value)
                End If
            End If

        Next
        Return 0
    End Function

    Private Sub UpdateAllTotals_For_Uploader()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0
        Dim dblTotalFreightAmt As Double = 0
        Dim dblTotLandedCost As Double = 0
        Dim dblCashDisAmt As Double = 0
        Dim dblHeadDisAmt As Double = 0
        Dim total_commision As Decimal = Nothing
        Dim dblACAmount As Double = 0
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
        Dim is_manditax As String = ""
        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0

        Dim strDocCode As String = ""
        Dim arr As New ArrayList()
        Dim LastIndex As Integer = 0

        For ii As Integer = 0 To gv_Uploader.Rows.Count - 1
            strDocCode = clsCommon.myCstr(gv_Uploader.Rows(ii).Cells(colUDocCode).Value)

            If clsCommon.myLen(strDocCode) > 0 AndAlso Not arr.Contains(strDocCode) Then
                If ii > 0 Then
                    For jj As Integer = LastIndex To ii - 1
                        ''tax grid value==================================
                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt1).Value = Math.Round(dblTaxAmt1, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt1).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If dblTaxBaseAmt1 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate1).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate1).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt2).Value = Math.Round(dblTaxAmt2, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt2).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate2).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate2).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt3).Value = Math.Round(dblTaxAmt3, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt3).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate3).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate3).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt4).Value = Math.Round(dblTaxAmt4, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt4).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate4).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate4).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt5).Value = Math.Round(dblTaxAmt5, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt5).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate5).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate5).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt6).Value = Math.Round(dblTaxAmt6, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt6).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate6).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate6).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt7).Value = Math.Round(dblTaxAmt7, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt7).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate7).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate7).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt8).Value = Math.Round(dblTaxAmt8, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt8).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate8).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate8).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt9).Value = Math.Round(dblTaxAmt9, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt9).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate9).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate9).Value = 0
                        End If

                        gv_Uploader.Rows(jj).Cells(colUTTaxAmt10).Value = Math.Round(dblTaxAmt10, 2)
                        gv_Uploader.Rows(jj).Cells(colUTBaseAmt10).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate10).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                        Else
                            gv_Uploader.Rows(jj).Cells(colUTTaxRate10).Value = 0
                        End If
                        ''=======================end tax grid=========================================================
                        gv_Uploader.Rows(jj).Cells(colUDoc_Amt_WO_Disc).Value = clsCommon.myFormat(dblTotAmt)
                        gv_Uploader.Rows(jj).Cells(colUAmt_After_Disc).Value = clsCommon.myFormat(dblTotAmt)
                        gv_Uploader.Rows(jj).Cells(colUTaxAmt).Value = clsCommon.myFormat(dblTaxTotAmt)
                        gv_Uploader.Rows(jj).Cells(colUTCommsnAmt).Value = clsCommon.myFormat(total_commision)
                        gv_Uploader.Rows(jj).Cells(colUTFreightAmt).Value = clsCommon.myFormat(dblTotalFreightAmt)

                        gv_Uploader.Rows(jj).Cells(colUTDiscAmt).Value = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
                        gv_Uploader.Rows(jj).Cells(colUAddtnlAmt).Value = clsCommon.myFormat(dblACAmount)
                        gv_Uploader.Rows(jj).Cells(colUInvDiscAmt).Value = clsCommon.myCdbl(dblHeadDisAmt + dblHeadDisPerAmt)

                        gv_Uploader.Rows(jj).Cells(colUDocAmount).Value = clsCommon.myFormat(dblTotAmt)
                        dblNetAmt = dblTotAmt
                        gv_Uploader.Rows(jj).Cells(colUDocAmount).Value = clsCommon.myFormat(dblNetAmt)

                        If AllowRoundOff_onInvoice Then
                            Dim lstDecml As New List(Of Decimal)
                            lstDecml = clsCSASaleInvoice.Calculate_RoundOffAmt(clsCommon.myCdbl(gv_Uploader.Rows(jj).Cells(colUDocAmount).Value), Nothing)

                            If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                                gv_Uploader.Rows(jj).Cells(colUDocAmount).Value = clsCommon.myCdbl(lstDecml(0))
                                gv_Uploader.Rows(jj).Cells(colURoundOff).Value = clsCommon.myCdbl(lstDecml(1))
                            End If
                        Else
                            gv_Uploader.Rows(jj).Cells(colURoundOff).Value = Nothing
                        End If
                    Next ''fill total document detail to all last grid
                    LastIndex = ii
                End If ''ii check cond
                arr.Add(strDocCode)

                ''=========blank
                dblTotAmt = 0
                dblTotDisAmt = 0
                dblAmtAfterDis = 0
                dblTotalFreightAmt = 0
                dblTotLandedCost = 0
                dblCashDisAmt = 0
                dblHeadDisAmt = 0
                total_commision = Nothing
                dblACAmount = 0
                dblTaxBaseAmt1 = 0
                dblTaxBaseAmt2 = 0
                dblTaxBaseAmt3 = 0
                dblTaxBaseAmt4 = 0
                dblTaxBaseAmt5 = 0
                dblTaxBaseAmt6 = 0
                dblTaxBaseAmt7 = 0
                dblTaxBaseAmt8 = 0
                dblTaxBaseAmt9 = 0
                dblTaxBaseAmt10 = 0
                dblTaxAmt1 = 0
                dblTaxAmt2 = 0
                dblTaxAmt3 = 0
                dblTaxAmt4 = 0
                dblTaxAmt5 = 0
                dblTaxAmt6 = 0
                dblTaxAmt7 = 0
                dblTaxAmt8 = 0
                dblTaxAmt9 = 0
                dblTaxAmt10 = 0
                dblHeadDisPerAmt = 0
                is_manditax = ""
                dblTaxTotAmt = 0
                dblNetAmt = 0
                ''========================
            End If ''end arr cond.

            If (clsCommon.myLen(gv_Uploader.Rows(ii).Cells(colitemcode).Value) > 0) Then ' AndAlso clsCommon.myCdbl(gv_uploader.Rows(ii).Cells(ColFOC).Value) = 0

                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colSaleValue).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colAmtAfterDis).Value)
                total_commision = total_commision + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colCommisionValue).Value)
                dblTotalFreightAmt = dblTotalFreightAmt + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colFrghtAmt).Value)
                dblACAmount = dblACAmount + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colOtherCharge).Value)


                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colAmtAfterTax).Value)
            End If ''item cond.


            ''----------------if only 1 row ingrid then or for last row=
            'If (gv_Uploader.Rows.Count = 1) OrElse ((gv_Uploader.Rows.Count - 1) = ii) OrElse ii = 0 Then
            ''tax grid value==================================
            gv_Uploader.Rows(ii).Cells(colUTTaxAmt1).Value = Math.Round(dblTaxAmt1, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt1).Value = Math.Round(dblTaxBaseAmt1, 2)
            If dblTaxBaseAmt1 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate1).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate1).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt2).Value = Math.Round(dblTaxAmt2, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt2).Value = Math.Round(dblTaxBaseAmt2, 2)
            If dblTaxBaseAmt2 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate2).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate2).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt3).Value = Math.Round(dblTaxAmt3, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt3).Value = Math.Round(dblTaxBaseAmt3, 2)
            If dblTaxBaseAmt3 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate3).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate3).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt4).Value = Math.Round(dblTaxAmt4, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt4).Value = Math.Round(dblTaxBaseAmt4, 2)
            If dblTaxBaseAmt4 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate4).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate4).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt5).Value = Math.Round(dblTaxAmt5, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt5).Value = Math.Round(dblTaxBaseAmt5, 2)
            If dblTaxBaseAmt5 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate5).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate5).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt6).Value = Math.Round(dblTaxAmt6, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt6).Value = Math.Round(dblTaxBaseAmt6, 2)
            If dblTaxBaseAmt6 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate6).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate6).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt7).Value = Math.Round(dblTaxAmt7, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt7).Value = Math.Round(dblTaxBaseAmt7, 2)
            If dblTaxBaseAmt7 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate7).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate7).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt8).Value = Math.Round(dblTaxAmt8, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt8).Value = Math.Round(dblTaxBaseAmt8, 2)
            If dblTaxBaseAmt8 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate8).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate8).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt9).Value = Math.Round(dblTaxAmt9, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt9).Value = Math.Round(dblTaxBaseAmt9, 2)
            If dblTaxBaseAmt9 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate9).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate9).Value = 0
            End If

            gv_Uploader.Rows(ii).Cells(colUTTaxAmt10).Value = Math.Round(dblTaxAmt10, 2)
            gv_Uploader.Rows(ii).Cells(colUTBaseAmt10).Value = Math.Round(dblTaxBaseAmt10, 2)
            If dblTaxBaseAmt10 <> 0 Then
                gv_Uploader.Rows(ii).Cells(colUTTaxRate10).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
            Else
                gv_Uploader.Rows(ii).Cells(colUTTaxRate10).Value = 0
            End If
            ''=======================end tax grid=========================================================
            gv_Uploader.Rows(ii).Cells(colUDoc_Amt_WO_Disc).Value = clsCommon.myFormat(dblTotAmt)
            gv_Uploader.Rows(ii).Cells(colUAmt_After_Disc).Value = clsCommon.myFormat(dblTotAmt)
            gv_Uploader.Rows(ii).Cells(colUTaxAmt).Value = clsCommon.myFormat(dblTaxTotAmt)
            gv_Uploader.Rows(ii).Cells(colUTCommsnAmt).Value = clsCommon.myFormat(total_commision)
            gv_Uploader.Rows(ii).Cells(colUTFreightAmt).Value = clsCommon.myFormat(dblTotalFreightAmt)

            gv_Uploader.Rows(ii).Cells(colUTDiscAmt).Value = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
            gv_Uploader.Rows(ii).Cells(colUAddtnlAmt).Value = clsCommon.myFormat(dblACAmount)
            gv_Uploader.Rows(ii).Cells(colUInvDiscAmt).Value = clsCommon.myCdbl(dblHeadDisAmt + dblHeadDisPerAmt)

            gv_Uploader.Rows(ii).Cells(colUDocAmount).Value = clsCommon.myFormat(dblTotAmt)
            dblNetAmt = dblTotAmt
            gv_Uploader.Rows(ii).Cells(colUDocAmount).Value = clsCommon.myFormat(dblNetAmt)

            If AllowRoundOff_onInvoice Then
                Dim lstDecml As New List(Of Decimal)
                lstDecml = clsCSASaleInvoice.Calculate_RoundOffAmt(clsCommon.myCdbl(gv_Uploader.Rows(ii).Cells(colUDocAmount).Value), Nothing)

                If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                    gv_Uploader.Rows(ii).Cells(colUDocAmount).Value = clsCommon.myCdbl(lstDecml(0))
                    gv_Uploader.Rows(ii).Cells(colURoundOff).Value = clsCommon.myCdbl(lstDecml(1))
                End If
            Else
                gv_Uploader.Rows(ii).Cells(colURoundOff).Value = Nothing
            End If
            'End If
            ''=================================


        Next ''grid loop
    End Sub
#End Region

    Private Sub btnTransferKnockOff_Click(sender As Object, e As EventArgs) Handles btnTransferKnockOff.Click
        Try
            Dim i As Integer = 0
            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                If FillTransferStockData_For_Uploader(False, grow.Index) = False Then
                    i += 1
                End If
            Next

            btnValidate.Enabled = False
            btnApplyScheme.Enabled = False
            btnUploaderSave.Enabled = False
            If i > 0 Then ''validation breaked
                btnTransferKnockOff.Enabled = True
                btnCalculation.Enabled = False
            Else
                btnTransferKnockOff.Enabled = False
                btnCalculation.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function FillTransferStockData_For_Uploader(ByVal gvDoubleClick As Boolean, ByVal IntRow As Integer) As Boolean
        Try
            isInsideLoadData = True

            '=============create demo table for datasave of transfer=====================
            Dim qry As String = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='CSA_SALE_TRANSFER'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                qry = "create table CSA_SALE_TRANSFER (document_code varchar(30),line_no integer,Against_Transfer_Code varchar(30),item_code varchar(50),Item_Pack_Size float,qty float,transfer_rate float,Conv_Factor float null,Transfer_Qty float null,Transfer_UOM varchar(12) null,Balance_Qty float null,Sale_UOM varchar(12) null,Alt_Qty float null,FOC char(1) not null default 'N',Transfer_Line_No integer)"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
            '================================================================================

            Dim validRmk As String = ""
            Dim frm As New FrmCSATrans_KnockOffScreen()
            Try
                frm = New FrmCSATrans_KnockOffScreen()
                frm.TransferManual_KnockOFF = TransferManual_KnockOFF
                frm.strDocCode = "" 'clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells("").Value)
                frm.strDocDate = clsCommon.myCDate(gv_Uploader.Rows(IntRow).Cells(colUDocDate).Value)
                frm.strCustCode = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUCSACode).Value)
                frm.strPlantLoc_Code = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(ColUToLocation).Value)
                frm.strCSAloc_code = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colUBillToLocation).Value)
                frm.colPackSize = clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colPackSize).Value)
                frm.colStckTransferrate = 0 ' clsCommon.myCdbl(gv_uploader.CurrentRow.Cells(colStckTransferrate).Value)
                frm.colitemcode = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colitemcode).Value)
                frm.colqty = clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colqty).Value)
                frm.colFOC = IIf(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colFOC).Value) <> "Y", "N", "Y")
                frm.colItemUOM = clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colItemUOM).Value)
                frm.LoadTransGrid()
                frm.ComeFromImport = True
                frm.FillTransfergrid(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colLinenno).Value), TryCast(gv_Uploader.Rows(IntRow).Tag, List(Of clsCSAStockTransferDetail)))
                frm.btnsave.PerformClick()
            Catch ex As Exception
                validRmk = clsCommon.myCstr(ex.Message)
            End Try

            If clsCommon.myLen(validRmk) > 0 Then
                gv_Uploader.Rows(IntRow).Tag = Nothing
                gv_Uploader.Rows(IntRow).Cells(colStckTransferrate).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colSaleValue).Value) - clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value), 2)

                CalAltQty_for_Uploader(IntRow)
                CalConversionFactor_Uploader(IntRow)
                CalUnitPrice_For_Uploader(gv_Uploader.CurrentRow.Index, True)
                CommisionValue_For_Uploader(IntRow)

                UpdateCurrentRow_for_Uploader(IntRow)
                If rbtnTaxCalManual.IsChecked Then
                    For ii As Integer = 0 To gv_Uploader.Rows.Count - 1
                        UpdateCurrentRow_for_Uploader(ii)
                    Next
                End If

                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Value = validRmk
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.DrawFill = True
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.CustomizeFill = True
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.BackColor = Color.Red
                Return False
            Else
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Value = Nothing
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.DrawFill = True
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.CustomizeFill = True
                gv_Uploader.Rows(IntRow).Cells(colUValidateRemark).Style.BackColor = Nothing
            End If

            If clsCommon.myCdbl(frm.colStckTransferrate) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then ''if stock rate is 0 for non scheme item then
                gv_Uploader.Rows(IntRow).Tag = Nothing
                gv_Uploader.Rows(IntRow).Cells(colStckTransferrate).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value = 0
                gv_Uploader.Rows(IntRow).Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colSaleValue).Value) - clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value), 2)

                CalAltQty_for_Uploader(IntRow)
                CalConversionFactor_Uploader(IntRow)
                CalUnitPrice_For_Uploader(gv_Uploader.CurrentRow.Index, True)
                CommisionValue_For_Uploader(IntRow)

                UpdateCurrentRow_for_Uploader(IntRow)
                If rbtnTaxCalManual.IsChecked Then
                    For ii As Integer = 0 To gv_Uploader.Rows.Count - 1
                        UpdateCurrentRow_for_Uploader(ii)
                    Next
                End If
                'UpdateAllTotals()
                isInsideLoadData = False
                Return False
            End If

            gv_Uploader.Rows(IntRow).Tag = Nothing
            gv_Uploader.Rows(IntRow).Tag = frm.GV_ARR
            gv_Uploader.Rows(IntRow).Cells(colStckTransferrate).Value = frm.colStckTransferrate
            gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value = frm.colStckTransferAmount '' System.Math.Round(clsCommon.myCdbl(gv_uploader.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(gv_uploader.CurrentRow.Cells(colStckTransferrate).Value), 2)
            gv_Uploader.Rows(IntRow).Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colSaleValue).Value) - clsCommon.myCdbl(gv_Uploader.Rows(IntRow).Cells(colstckratevalue).Value), 2)

            If clsCommon.CompairString(clsCommon.myCstr(gv_Uploader.Rows(IntRow).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                gv_Uploader.Rows(IntRow).Cells(colStckTransferrate).Value = clsCommon.myCdbl(gv_Uploader.Rows(IntRow - 1).Cells(colStckTransferrate).Value)
            End If

            CalAltQty_for_Uploader(IntRow)
            CalConversionFactor_Uploader(IntRow)
            'CalUnitPrice(gv_Uploader.CurrentRow.Index, True)
            CommisionValue_For_Uploader(IntRow)

            UpdateCurrentRow_for_Uploader(IntRow)
            If rbtnTaxCalManual.IsChecked Then
                For ii As Integer = 0 To gv_Uploader.Rows.Count - 1
                    UpdateCurrentRow_for_Uploader(ii)
                Next
            End If
            'UpdateAllTotals()

            isInsideLoadData = False

            Return True
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Function

    Private Sub btnCalculation_Click(sender As Object, e As EventArgs) Handles btnCalculation.Click
        Try
            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                isValid_CashScheme_For_Uploader(grow.Index, True)
                CalUnitPrice_For_Uploader(grow.Index, True)
                CalAltQty_for_Uploader(grow.Index)
                CalConversionFactor_Uploader(grow.Index)
                CommisionValue_For_Uploader(grow.Index)
                UpdateCurrentRow_for_Uploader(grow.Index)
            Next

            UpdateAllTotals_For_Uploader()

            btnValidate.Enabled = False
            btnApplyScheme.Enabled = False
            btnTransferKnockOff.Enabled = False
            btnCalculation.Enabled = False
            btnUploaderSave.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUploaderSave_Click(sender As Object, e As EventArgs) Handles btnUploaderSave.Click
        Dim obj As New clsCSASaleInvoice()
        Dim objtr As New clsCSASaleInvoiceItem()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arr As New ArrayList()
            Dim uniqno As String = Nothing
            Dim Doc_Count As Integer = 0
            Dim LastIndex As Integer = 0

            For Each grow As GridViewRowInfo In gv_Uploader.Rows
                uniqno = clsCommon.myCstr(grow.Cells(colUDocCode).Value)
                LastIndex = LastIndex + 1

                If clsCommon.myLen(uniqno) > 0 AndAlso Not arr.Contains(uniqno) Then
                    ''when different document found then old document is saved and obj is refresh for new document.
                    If obj IsNot Nothing AndAlso obj.Arr_Item IsNot Nothing AndAlso obj.Arr_Item.Count > 0 AndAlso clsCSASaleInvoice.SaveData(True, obj, False, trans) Then
                        Doc_Count += 1
                    End If
                    arr.Add(uniqno)
                    ''==============================================================================================

                    ''=========unique no identification of new document for invoice
                    obj = New clsCSASaleInvoice()
                    obj.Arr_Item = New List(Of clsCSASaleInvoiceItem)

                    obj.docno = Nothing
                    obj.docdate = clsCommon.myCDate(grow.Cells(colUDocDate).Value)
                    If AllowDistibutorSale Then
                        obj.CSA_Distributor_Code = clsCommon.myCstr(grow.Cells(colUCSACode).Value)
                        obj.cust_code = clsCommon.myCstr(grow.Cells(colUDistributorCode).Value)
                    Else
                        obj.cust_code = clsCommon.myCstr(grow.Cells(colUCSACode).Value)
                        obj.CSA_Distributor_Code = ""
                    End If
                    obj.isPost = 0
                    obj.descrptn = clsCommon.myCstr(grow.Cells(colUDesc).Value)
                    obj.tax_group_code = clsCommon.myCstr(grow.Cells(colUTaxGroup1).Value)
                    obj.loc_code = clsCommon.myCstr(grow.Cells(colUBillToLocation).Value)
                    obj.plant_loc_code = clsCommon.myCstr(grow.Cells(ColUToLocation).Value)
                    obj.TAX1 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode1).Value)
                    obj.Excisable = clsCommon.myCstr(grow.Cells(colUExciseType).Value)
                    obj.term_code = clsCommon.myCstr(grow.Cells(colUTermCode).Value)
                    obj.due_date = clsCommon.myCDate(grow.Cells(colUDueDate).Value)

                    'If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
                    '    obj.currency_code = clsCommon.myCstr(txtCurrencyCode.Value)
                    '    obj.cnvrsn_rate = clsCommon.myCdbl(txtConversionRate.Text)
                    '    obj.applicable_from = clsCommon.myCstr(txtApplicableFrom.Text)
                    'Else
                    obj.currency_code = Nothing
                    obj.cnvrsn_rate = 1
                    obj.applicable_from = Nothing
                    'End If

                End If
                obj.document_amt = clsCommon.myCdbl(grow.Cells(colUDocAmount).Value)
                obj.total_commision = clsCommon.myCdbl(grow.Cells(colUTCommsnAmt).Value)
                obj.Total_Freight_Amt = clsCommon.myCdbl(grow.Cells(colUTFreightAmt).Value)

                obj.amt_with_disc = clsCommon.myCdbl(grow.Cells(colUDoc_Amt_WO_Disc).Value)
                obj.disc_on_rate = "0"
                obj.disc_on_amt = "0"
                obj.disc_pers = 0

                If obj.disc_pers > 0 Then
                    obj.inv_disc_amt = clsCommon.myCdbl(lblInvoiceDiscAmt.Text)
                    obj.disc_amt = 0
                Else
                    obj.inv_disc_amt = 0
                    obj.disc_amt = clsCommon.myCdbl(lblInvoiceDiscAmt.Text)
                    obj.inv_disc_amt = 0
                End If

                obj.lbldisc_amt = 0
                obj.amt_after_disc = clsCommon.myCdbl(grow.Cells(colUAmt_After_Disc).Value)
                obj.lbltaxamt = clsCommon.myCdbl(grow.Cells(colUTaxAmt).Value)
                obj.total_add_chrg = 0
                'If rbtnTaxCalAutomatic.IsChecked Then
                obj.Tax_Calculation_Type = "0"
                'Else
                '    obj.Tax_Calculation_Type = "1"
                'End If
                obj.RoundOffAmount = clsCommon.myCdbl(grow.Cells(colURoundOff).Value)
                '=================Tax--======================================
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode1).Value) > 0 Then
                    obj.TAX1 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode1).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate1).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt1).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt1).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode2).Value) > 0 Then
                    obj.TAX2 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode2).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate2).Value)
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt2).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt2).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode3).Value) > 0 Then
                    obj.TAX3 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode3).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate3).Value)
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt3).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt3).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode4).Value) > 0 Then
                    obj.TAX4 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode4).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate4).Value)
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt4).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt4).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode5).Value) > 0 Then
                    obj.TAX5 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode5).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate5).Value)
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt5).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt5).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode6).Value) > 0 Then
                    obj.TAX6 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode6).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate6).Value)
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt6).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt6).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode7).Value) > 0 Then
                    obj.TAX7 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode7).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate7).Value)
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt7).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt7).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode8).Value) > 0 Then
                    obj.TAX8 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode8).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate8).Value)
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt8).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt8).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode9).Value) > 0 Then
                    obj.TAX9 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode9).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate9).Value)
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt9).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt9).Value)
                End If
                If clsCommon.myLen(grow.Cells(colUTTaxAutCode10).Value) > 0 Then
                    obj.TAX10 = clsCommon.myCstr(grow.Cells(colUTTaxAutCode10).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colUTTaxRate10).Value)
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colUTBaseAmt10).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colUTTaxAmt10).Value)
                End If
                'End If ''arr cond. for head part

                objtr = New clsCSASaleInvoiceItem()
                objtr.GV_TAG_ARR = New List(Of clsCSAStockTransferDetail)()

                objtr.Line_No = clsCommon.myCdbl(grow.Cells(colLinenno).Value)
                objtr.commision = clsCommon.myCdbl(grow.Cells(colCommision).Value)
                objtr.CSA_Commission_RS_PERS = clsCommon.myCstr(grow.Cells(colComm_Type_RS_Pers).Value)
                objtr.Other_Chrage = clsCommon.myCdbl(grow.Cells(colOtherCharge).Value)
                objtr.Pack_Size = clsCommon.myCdbl(grow.Cells(colPackSize).Value)
                objtr.Master_Pack_Size = clsCommon.myCdbl(grow.Cells(colMasterPackSize).Value)

                objtr.Freight_Amt = clsCommon.myCdbl(grow.Cells(colFrghtAmt).Value)
                objtr.Freight_Rate = clsCommon.myCdbl(grow.Cells(colFrghtRate).Value)
                objtr.Freight_Type = clsCommon.myCstr(grow.Cells(colFrghtType).Value)

                If clsCommon.myLen(grow.Cells(coldate).Value) <= 0 Then
                    objtr.Grid_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(trans))
                Else
                    objtr.Grid_Date = clsCommon.myCDate(grow.Cells(coldate).Value)
                End If

                ''=============================================
                objtr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                objtr.Is_MRP_Mandatory = CInt(clsCommon.myCdbl(IIf(clsCommon.myCBool(grow.Cells(colIsMRPMandatory).Value) = True, 1, 0)))
                objtr.Abatement_Per = clsCommon.myCdbl(grow.Cells(colAbatementPers).Value)
                objtr.Abatement_Amt = clsCommon.myCdbl(grow.Cells(colAbatementAmt).Value)
                ''=============================================

                objtr.Booking_no = clsCommon.myCstr(grow.Cells(colBookingno).Value)
                objtr.Booking_type = "Item-Wise"
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.Unit_code = clsCommon.myCstr(grow.Cells(colItemUOM).Value)
                objtr.Qty = clsCommon.myCdbl(grow.Cells(colqty).Value)
                objtr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colbalqty).Value)
                objtr.alt_qty = clsCommon.myCdbl(grow.Cells(colAltQty).Value)
                objtr.colcommsn_amt = clsCommon.myCdbl(grow.Cells(colCommisionValue).Value)
                objtr.Conv_Factor = clsCommon.myCdbl(grow.Cells(colConversionFactor).Value)
                objtr.alt_uom = clsCommon.myCstr(grow.Cells(colAltUOM).Value)
                objtr.booking_rate = clsCommon.myCdbl(grow.Cells(colbookingrate).Value)
                objtr.Item_Cost = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)
                objtr.including_tax = clsCommon.myCstr(grow.Cells(colincludingtax).Value)

                objtr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                objtr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                objtr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                objtr.sale_rate = clsCommon.myCdbl(grow.Cells(colSaleRate).Value)
                objtr.Amount = clsCommon.myCdbl(grow.Cells(colSaleValue).Value)

                objtr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                objtr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                objtr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                objtr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                objtr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                objtr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                objtr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                objtr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                objtr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                objtr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                objtr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                objtr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                objtr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                objtr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                objtr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                objtr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                objtr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                objtr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                objtr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                objtr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                objtr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                objtr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                objtr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                objtr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                objtr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                objtr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                objtr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                objtr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                objtr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                objtr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                objtr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                objtr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                objtr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                objtr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                objtr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                objtr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                objtr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                objtr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                objtr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                objtr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)

                objtr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                objtr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                objtr.Location = clsCommon.myCstr(grow.Cells(colUBillToLocation).Value)
                objtr.remarks = "Import sheet"
                objtr.Item_Tax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                objtr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                objtr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                objtr.ActualRate = clsCommon.myCdbl(grow.Cells(colUnitRate).Value)

                objtr.tax_basis = clsCommon.myCstr(grow.Cells(colTaxBasis).Value) 'back cal./forward cal.
                objtr.stck_trans_rate = clsCommon.myCdbl(grow.Cells(colStckTransferrate).Value)
                objtr.stck_trans_value = clsCommon.myCdbl(grow.Cells(colstckratevalue).Value)
                objtr.GainLoss = clsCommon.myCdbl(grow.Cells(colGainLoss).Value)

                If objtr.Disc_Per > 0 Then
                    objtr.HeadDiscPerAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objtr.HeadDiscAmt = 0
                Else
                    obj.inv_disc_amt = 0
                    objtr.HeadDiscAmt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objtr.HeadDiscPerAmt = 0
                End If

                objtr.Scheme_Item = clsCommon.myCstr(grow.Cells(colFOC).Value)
                objtr.Scheme_Applicable = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
                objtr.Scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCode).Value)
                objtr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                objtr.Scheme_Item_Line_No = clsCommon.myCstr(grow.Cells(colMainLineNo).Value)
                If clsCommon.myLen(objtr.Scheme_Item_Line_No) <= 0 Then
                    objtr.Scheme_Item_Line_No = "0"
                End If
                objtr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                objtr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                objtr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                objtr.FOC_Item = clsCommon.myCstr(IIf(clsCommon.myCstr(grow.Cells(colFOC).Value) = "N", "0", "1"))
                objtr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                objtr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                objtr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                objtr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)

                objtr.GV_TAG_ARR = TryCast(grow.Tag, List(Of clsCSAStockTransferDetail))

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr_Item.Add(objtr)
                End If

                If gv_Uploader.Rows.Count = 1 OrElse gv_Uploader.Rows.Count = LastIndex Then
                    clsCSASaleInvoice.SaveData(True, obj, False, trans)
                End If
                ''===============detail grid is filled
            Next ''grid loop

            trans.Commit()
            If arr IsNot Nothing AndAlso arr.Count > 0 AndAlso Doc_Count = arr.Count - 1 Then
                clsCommon.MyMessageBoxShow("Record(s) saved successfully." + Environment.NewLine + "Total " + clsCommon.myCstr(Doc_Count + 1) + " document created.")

                RadPageViewPage7.Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.SelectedPage = RadPageViewPage1
                SplitContainer1.Panel2.Enabled = True
                RadPageViewPage1.Item.Enabled = True
                RadPageViewPage2.Item.Enabled = True
                RadPageViewPage3.Item.Enabled = True
                RadPageViewPage4.Item.Enabled = True
                RadPageViewPage5.Item.Enabled = True
                RadPageViewPage6.Item.Enabled = True
                RadPageViewPage7.Item.Enabled = False

                FunReset()
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

End Class
