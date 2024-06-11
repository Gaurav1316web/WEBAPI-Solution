Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class frmGRN
    Inherits FrmMainTranScreen

#Region "Variables"
    Private PurchaseModulePickFixTaxRate As Boolean = False
    Dim AllowPurchaseModulewithUniqueItem As Integer = 0
    Dim Tolerence_Qty As Double = 0
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim Is_RGP_After_PO As Boolean = False
    Dim Schedule_ON As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colComplete As String = "COMPLETE"
    Const colRowType As String = "COLTYPE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colOrgPOQty As String = "COLORIGINALPOQTY"
    Const colQty As String = "COLQTY"
    Const colLeakQty As String = "COLEAKQTY"
    Const colBurstQty As String = "COLBURSTQTY"
    Const colShortQty As String = "COLSHORTQTY"

    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colIsInsurance As String = "colIsInsurance"
    Const colInsuranceBaseAmt As String = "colInsuranceBaseAmt"
    Const colInsurancePer As String = "colInsurancePer"
    Const colItemInsuranceBaseAmt As String = "colItemInsuranceBaseAmt"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"
    Const colItemAmtAfterInsurance As String = "colItemAmtAfterInsurance"
    Const colHeaderDiscountPer As String = "colHeaderDiscountPer"
    Const colHeaderDiscountAmt As String = "colHeaderDiscountAmt"
    Const colDisPer As String = "COLDISPER"
    Const colDetailDisAmt As String = "colDetailDisAmt"
    Const colDisPerUnit As String = "colDisPerUnit"
    Const colDisAmtPerUnit As String = "colDisAmtPerUnit"

    Const colAmt As String = "COLAMT"


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
    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colPONo As String = "PONO"
    Const colReqNo As String = "ReqNO"
    Const colMRP As String = "MRP"
    Const colBatchNo As String = "BATCHNO"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
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

    Const colBlanketPO As String = "colBlanketPO"
    Const colPOAmt As String = "colPOAmt"
    Const colPOControlOnReceive As String = "colPOControlOnReceive"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Const colScheduleNo As String = "ScheduleNo"
    Const colRGPNo As String = "RGPNo"

    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Const colACInsuranceCode As String = "colACInsuranceCode"
    Const colACInsuranceName As String = "colACInsuranceName"
    Const colACInsuranceAmount As String = "colACInsuranceAmount"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn

    '===================rgp job work grid-------------------------
    Const colRGPIcode As String = "RGPIcode"
    Const colRGPIname As String = "RGPIname"
    Const colRGPUnit As String = "RGPIunit"
    Const colRGPUnitcost As String = "RGPIRate"
    Const colRGPQty As String = "RGPIqty"
    Const colRGPSpecification As String = "RGPspecification"
    Const colRGPDocNo As String = "RGPdocno"
    Const colRGPPONo As String = "RGPpono"
    Const colRGPSchNo As String = "RGPSchno"
    Public strGRN As String = ""
    Dim isApplyBrachAccounting As Boolean = False
    Dim AutoClosePO As Boolean = False
    Dim AutoClosePOBasedOnSRNQtyWithTolerance As Boolean = False
    Const colItemTaxable As String = "colItemTaxable"
    Const colAgainstItemWiseTaxCode As String = "colAgainstItemWiseTaxCode"

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
    ''==================================================================
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Public ShowItemAllStructureWise As Boolean = False
    Dim SettPurchaseSlabApplyRange As Boolean = False
    Dim SettPurchaseSlabRangeNotApplicableFrom As Decimal
    Dim SettPurchaseSlabRangeNotApplicableTo As Decimal
    Dim SettPurchaseSlabRangePOFrom As Decimal
    Dim SettPurchaseSlabRangePOTo As Decimal
    Dim SettPurchaseSlabRangeRALFrom As Decimal
    Dim SettPurchaseSlabRangeRALTo As Decimal
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btncancel.Visible = MyBase.isCancel_Flag_After_Posting
        btnUnpost.Visible = False
        'If MyBase.isReverse Then
        '    btnUnpost.Enabled = True
        'Else
        '    btnUnpost.Enabled = False
        'End If
    End Sub

    Private Sub LoadRGPType()
        isInsideLoadData = True
        Dim qry As String = "select '' as Code,'Select' as Name union all select 'AR' as Code,'Against RGP' as Name union all select 'AB' as Code,'Against BOM' as Name union all select 'AI' as Code,'As It Is' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbRGPType.DataSource = Nothing
        cmbRGPType.DataSource = dt
        cmbRGPType.DisplayMember = "Name"
        cmbRGPType.ValueMember = "Code"
        isInsideLoadData = False
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadButton1.Visible = True
        PurchaseModulePickFixTaxRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseModulePickFixTaxRate, clsFixedParameterCode.PurchaseModulePickFixTaxRate, Nothing)) = 1, True, False)
        AllowPurchaseModulewithUniqueItem = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseModulewithUniqueItem, clsFixedParameterCode.AllowPurchaseModulewithUniqueItem, Nothing))
        ShowItemAllStructureWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 1, True, False)

        SetUserMgmtNew()
        '=============================================================
        'btncancel.Visible = False
        btncancel.Enabled = False
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        txt_invdate.Value = clsCommon.GETSERVERDATE
        Is_RGP_After_PO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsRGPAfterPurchaseOrder, clsFixedParameterCode.IsRGPAfterPurchaseOrder, Nothing)) = "1", True, False))
        AutoClosePO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePO, clsFixedParameterCode.AutoClosePO, Nothing)) = "1", True, False))
        AutoClosePOBasedOnSRNQtyWithTolerance = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePOBasedOnSRNQtyWithTolerance, clsFixedParameterCode.AutoClosePOBasedOnSRNQtyWithTolerance, Nothing)) = "1", True, False))
        If AutoClosePO Or AutoClosePOBasedOnSRNQtyWithTolerance Then
            txtinvoiceno.Visible = True
            txt_invdate.Visible = True
            txt_transporterdocbility.Visible = True
            MyLabel11.Visible = True
            MyLabel10.Visible = True
            MyLabel9.Visible = True
        Else
            txtinvoiceno.Visible = False
            txt_invdate.Visible = False
            txt_transporterdocbility.Visible = False
            MyLabel11.Visible = False
            MyLabel10.Visible = False
            MyLabel9.Visible = False
        End If

        SettPurchaseSlabApplyRange = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.ApplyRange, Nothing)) = 1)
        Dim str As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeNotApplicable, Nothing))
        If str.Contains("-") Then
            Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
            If strBreak.Length > 1 Then
                SettPurchaseSlabRangeNotApplicableFrom = clsCommon.myCDecimal(strBreak(0))
                SettPurchaseSlabRangeNotApplicableTo = clsCommon.myCDecimal(strBreak(1))
            End If
        End If
        str = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangePO, Nothing))
        If str.Contains("-") Then
            Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
            If strBreak.Length > 1 Then
                SettPurchaseSlabRangePOFrom = clsCommon.myCDecimal(strBreak(0))
                SettPurchaseSlabRangePOTo = clsCommon.myCDecimal(strBreak(1))
            End If
        End If
        str = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeRAL, Nothing))
        If str.Contains("-") Then
            Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
            If strBreak.Length > 1 Then
                SettPurchaseSlabRangeRALFrom = clsCommon.myCDecimal(strBreak(0))
                SettPurchaseSlabRangeRALTo = clsCommon.myCDecimal(strBreak(1))
            End If
        End If


        MyLabel4.Visible = False
        txtRgp_no.Visible = False
        chkRGPNonInventory.Visible = False
        MyLabel6.Visible = False
        cmbRGPType.Visible = False
        If Is_RGP_After_PO Then
            MyLabel4.Visible = True
            txtRgp_no.Visible = True
            'chkRGPNonInventory.Visible = True
            MyLabel6.Visible = True
            cmbRGPType.Visible = True
        End If

        Schedule_ON = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPOScheduling, clsFixedParameterCode.AllowPOScheduling, Nothing)) = "1", True, False))
        If Schedule_ON Then
            MyLabel3.Visible = True
            txtSch_No.Visible = True
        Else
            MyLabel3.Visible = False
            txtSch_No.Visible = False
        End If
        '===================================================================

        chkVendorGrossReceipt.Visible = False
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
        ''For Attachment
        UcAttachment1.Form_ID = MyBase.Form_ID
        'RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        ''End of For Attachment
        LoadRGPType()
        LoadGRNType()
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadItemType()
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        AddNew()
        SetLength()
        ''End of For Custom Fields
        '==========Added by Preeti Gupta==
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        If clsCommon.myLen(strGRN) > 0 Then
            LoadData(strGRN, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        '============End ====================

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency
        isApplyBrachAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 1, True, False)
        If objCommonVar.RCDFCFP = True Then
            RadPageViewPage5.Item.Visibility = ElementVisibility.Visible
        Else
            RadPageViewPage5.Item.Visibility = ElementVisibility.Collapsed
        End If

    End Sub

    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDept.Enabled = False
            txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDept.Value + "'"))
        Else
            txtDept.Enabled = True
        End If
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
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
        txtRefNo.MaxLength = 50
        txtCarrier.MaxLength = 50
        txtVehicleNo.MaxLength = 50
        txtGRNo.MaxLength = 50
        txtGENo.MaxLength = 50
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

    Sub LoadItemType()
        'If ShowItemAllStructureWise = True Then
        '    cboItemType.DataSource = GetItemall()
        '    cboItemType.ValueMember = "Code"
        '    cboItemType.DisplayMember = "Name"
        'Else
        'cboItemType.DataSource = clsItemMaster.GetItemType()
        Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
            cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
        ' End If
    End Sub

    Sub LoadGRNType()
        isInsideLoadData = True
        cmbGRNType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cmbGRNType.DisplayMember = "Name"
        cmbGRNType.ValueMember = "Code"
        isInsideLoadData = False
    End Sub

    Sub BlankAllControls()
        chkSkipPurchaseQc.Enabled = False
        chkSkipPurchaseQc.Checked = False
        'txtRgp_no.Enabled = True
        chkJobWorkOutward.Checked = False
        'btncancel.Visible = False
        btncancel.Enabled = False
        txt_invdate.Value = clsCommon.GETSERVERDATE()
        txt_RoadPermitNo.Text = ""
        txt_RoadPermitDate.Text = clsCommon.GETSERVERDATE()
        txtinvoiceno.Text = ""
        txt_transporterdocbility.Text = ""
        cmbGRNType.SelectedValue = ""
        cmbGRNType.Enabled = True
        cmbRGPType.SelectedValue = ""
        cmbRGPType.Enabled = False
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnTaxCalAutomatic.IsChecked = True
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
        txtBillToLocation.Enabled = True
        txtShipToLocation.Enabled = True
        txtSubLocation.Enabled = True
        txtDesc.Text = ""
        txtRemarks.Text = ""
        txtComment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        TxtRetention.Text = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value
        txtRefNo.Text = ""
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        lblTaxableAmount.Text = ""
        txtCarrier.Text = ""
        txtVehicleNo.Text = ""
        txtGRNo.Text = ""
        txtGENo.Text = ""
        txtGEDate.Checked = False
        txtGEDate.Value = txtDate.Value
        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 0
        cboVisualQCStatus.SelectedIndex = 1
        txtVisualQCRemarks.Text = ""
        dtpVisualQCStatus.Value = clsCommon.GETSERVERDATE()
        dtpVisualQCStatusSecond.Value = clsCommon.GETSERVERDATE()
        cboVisualQCStatusSecond.SelectedIndex = 1
        txtVisualQCRemarksSecond.Text = ""
        GBVisualQC.Enabled = False
        GBVisualQCSecond.Enabled = False
        txtReqNo.Value = ""
        cboItemType.Enabled = True
        txtBillToLocation.Enabled = True
        chkVendorGrossReceipt.Checked = False
        lblAddCharges1.Text = ""
        lblAddCharges1.Text = ""
        txtLRNo.Text = ""
        dtpLRDate.Value = clsCommon.GETSERVERDATE()
        UcAttachment1.BlankAllControls()
        txtSch_No.Value = ""
        txtRgp_no.Value = ""
        txtReqNo.Enabled = True
        chkRGPNonInventory.Checked = False
        txtRgp_no.Enabled = True
        txtSch_No.Enabled = True
        chkRGPNonInventory.Enabled = True

        ''RICHA AGARWAL AGAINST TICKET NO. BM00000006091 ON 04/05/2015
        txtCurrencyCode.Enabled = True
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 1
        txtApplicableFrom.Text = ""

        lblAddChargesForInsurance.Text = ""
        lblAddChargesForInsurance1.Text = ""
        lblTotalInsuranceAmt.Text = ""
        TxtRetention.Text = ""
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
        repoComplete.ReadOnly = True
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

        Dim repoRequition1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequition1.FormatString = ""
        repoRequition1.HeaderText = "RGP No"
        repoRequition1.Name = colRGPNo
        repoRequition1.ReadOnly = True
        repoRequition1.IsVisible = True
        repoRequition1.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition1)

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
        repoIName.ReadOnly = False
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
        repoPendingQty.VisibleInColumnChooser = False
        repoPendingQty.Minimum = 0
        repoPendingQty.Width = 150
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

        Dim repoOrgPOQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgPOQty.FormatString = ""
        repoOrgPOQty.WrapText = True
        'repoOrgPOQty.HeaderText = "Original PO Quantity"
        repoOrgPOQty.HeaderText = "PO Quantity"
        repoOrgPOQty.Name = colOrgPOQty
        repoOrgPOQty.Width = 80
        repoOrgPOQty.Minimum = 0
        repoOrgPOQty.ReadOnly = True
        repoOrgPOQty.IsVisible = False
        repoOrgPOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgPOQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N3}"
        'repoQty.HeaderText = "GRN Quantity"
        repoQty.HeaderText = "Challan Qty"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.DecimalPlaces = 3
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoLeadQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeadQty.FormatString = ""
        repoLeadQty.HeaderText = "Leakage"
        repoLeadQty.Name = colLeakQty
        repoLeadQty.IsVisible = False
        repoLeadQty.ReadOnly = True
        repoLeadQty.Minimum = 0
        repoLeadQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeadQty)

        Dim repoBurstQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurstQty.FormatString = ""
        repoBurstQty.HeaderText = "Burst"
        repoBurstQty.Name = colBurstQty
        repoBurstQty.Width = 80
        repoBurstQty.IsVisible = False
        repoBurstQty.ReadOnly = True
        repoBurstQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurstQty)

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.FormatString = ""
        repoShortQty.HeaderText = "Shortage"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 80
        repoShortQty.ReadOnly = True
        repoShortQty.Minimum = 0
        repoShortQty.IsVisible = False
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShortQty)

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

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
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

        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = "{0:N2}"
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 80
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)


        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = "{0:N2}"
        repoDisAmt.HeaderText = "Discount Amt"
        repoDisAmt.Minimum = 0
        repoDisAmt.Maximum = 100
        repoDisAmt.Name = colDetailDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.ReadOnly = True
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisAmt)


        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Total Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
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
        repoRequition.HeaderText = "PO No"
        repoRequition.Name = colPONo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)

        Dim repoRequitionId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRequitionId.FormatString = ""
        repoRequitionId.HeaderText = "Req No"
        repoRequitionId.Name = colReqNo
        repoRequitionId.ReadOnly = True
        repoRequitionId.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequitionId)

        repoRequition = New GridViewTextBoxColumn()
        repoRequition.FormatString = ""
        repoRequition.HeaderText = "Schedule No"
        repoRequition.Name = colScheduleNo
        repoRequition.ReadOnly = True
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)



        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Minimum = 0
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True 'False
        gv1.MasterTemplate.Columns.Add(repoMRP)

        ''Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        ''repoAssessable = New GridViewDecimalColumn()
        ''repoAssessable.FormatString = ""
        ''repoAssessable.HeaderText = "Assessable Rate"
        ''repoAssessable.Minimum = 0
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

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = True 'False
        repoBatchNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colExpiry
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colManufactureDate
        repoManDate.ReadOnly = True
        repoManDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoManDate)

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

        repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "PO Control On Receive"
        repoIsSurTax1.Name = colPOControlOnReceive
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "PO Amount"
        repoTaxAmt1.Name = colPOAmt
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)


        repoIsSurTax4 = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Blanket PO"
        repoIsSurTax4.Name = colBlanketPO
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4)

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
        'gv1.AutoSizeRows = True
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
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = False
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

    Private Function GetItemType() As DataTable
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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colCategoryType) OrElse e.Column Is gv1.Columns(colCapexSubCode) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colDisPerUnit) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        If (e.Column Is gv1.Columns(colItemInsuranceAmt) OrElse e.Column Is gv1.Columns(colItemInsurancePer) OrElse e.Column Is gv1.Columns(colInsurancePer) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colDisPerUnit) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty) OrElse (e.Column Is gv1.Columns(colAmt)) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) Then
                            If ((e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colLeakQty) OrElse e.Column Is gv1.Columns(colBurstQty) OrElse e.Column Is gv1.Columns(colShortQty)) AndAlso (clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0)) Then
                                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                Dim dblDamageQty As Double = 0 ' clsCommon.myCdbl(gv1.CurrentRow.Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colShortQty).Value)
                                If clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) <= 0 Then
                                    dblPendingQty = clsPurchaseScheduleDetail.GetBalanceScheduleQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colScheduleNo).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtDocNo.Value, txtDate.Text, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), True)
                                End If
                                If clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso Is_RGP_After_PO Then
                                    Dim strMsg As String = clsRGPHead.GetRGPTypeItemBalance(cmbRGPType.SelectedValue, dblEnteredQty, txtVendorNo.Value, txtDocNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), txtDate.Value, Nothing, False)
                                    If clsCommon.myLen(strMsg) > 0 Then
                                        gv1.CurrentCell.Value = 0
                                        Throw New Exception(strMsg)
                                    End If
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                End If
                                ''=========preeti gupta [Add tolerence with item wise]
                                If AutoClosePO Then
                                    dblPendingQty = dblPendingQty + ((clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgPOQty).Value) * (clsGRNHead.ToleranceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing))) / 100)
                                End If
                                If (dblEnteredQty + dblDamageQty) > dblPendingQty Then
                                    If Not clsCommon.myCBool(gv1.CurrentRow.Cells(colBlanketPO).Value) Then
                                        common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty) + ". Damage Quantity : " + clsCommon.myCstr(dblDamageQty))
                                        gv1.CurrentCell.Value = 0
                                    End If
                                End If
                            End If
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then ''BHA/17/08/18-000443 by balwinder on 21/08/2018
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
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
                        End If
                        setGridFocus()
                    ElseIf e.Column Is gv1.Columns(colAmt) Then
                        If clsCommon.myCBool(gv1.CurrentRow.Cells(colPOControlOnReceive).Value) Then
                            gv1.CurrentRow.Cells(colQty).Value = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colOrgPOQty).Value)) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colPOAmt).Value), 10, MidpointRounding.ToEven)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    gv1.Columns(colDisAmt).ReadOnly = True
                    gv1.Columns(colDisPer).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value)
        If clsCommon.myLen(strItemType) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Row Type", Me.Text)
            isCellValueChangedOpen = False
            Exit Sub
        End If

        '================================================================================================================================
        If Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Select rgp type for job-work transaction.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            cmbRGPType.Select()
            cmbRGPType.Focus()
            Exit Sub
        End If

        '==============================End here==================================================================================================


        If clsCommon.CompairString(strItemType, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then

            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                SetBlankOfItemColumns()
                cboItemType.Focus()
                Exit Sub
            End If

            'If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select Item Type")
            '    SetBlankOfItemColumns()
            '    cboItemType.Focus()
            '    Exit Sub
            'End If
            ' ''If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
            ' ''    Dim objItemPM As clsItemPriceMaster = clsItemPriceMaster.FinderForItemPrices()
            ' ''    If objItemPM IsNot Nothing Then
            ' ''        gv1.CurrentRow.Cells(colICode).Value = objItemPM.Item_Code
            ' ''        gv1.CurrentRow.Cells(colIName).Value = objItemPM.item_Description
            ' ''        gv1.CurrentRow.Cells(colUnit).Value = objItemPM.UOM
            ' ''        gv1.CurrentRow.Cells(colMRP).Value = objItemPM.Item_MRP
            ' ''        ''gv1.CurrentRow.Cells(colAssessableRate).Value = objItemPM.Abatement
            ' ''    Else
            ' ''        SetBlankOfItemColumns()
            ' ''    End If
            ' ''Else
            If Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal Then
                Dim obj As clsRGPDetail = clsRGPHead.GetRGPTypeItemFInder(cmbRGPType.SelectedValue, txtVendorNo.Value, txtDocNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue))
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)

                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_code
                    gv1.CurrentRow.Cells(colRGPNo).Value = obj.RGP_No
                    gv1.CurrentRow.Cells(colQty).Value = obj.RGP_Qty
                    gv1.CurrentRow.Cells(colPendingQty).Value = obj.RGP_Qty
                Else
                    SetBlankOfItemColumns()
                End If
            Else
                Dim obj As clsItemMaster = clsItemMaster.FinderForItemALL(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, ShowItemAllStructureWise, isButtonClick, txtVendorNo.Value, "", "")
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)

                    gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
                Else
                    SetBlankOfItemColumns()
                End If
                ''End If

            End If

            Dim objVItem As clsVendorItemDetail = clsVendorItemDetail.GetItemRateAndMRP(txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
            If objVItem IsNot Nothing Then
                gv1.CurrentRow.Cells(colRate).Value = objVItem.item_rate
                gv1.CurrentRow.Cells(colMRP).Value = objVItem.MRP
            End If
        Else

            ''For Open Misc Charges 
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Code
                gv1.CurrentRow.Cells(colIName).Value = obj.desc
                gv1.CurrentRow.Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Code, Nothing)
                gv1.CurrentRow.Cells(colIsInsurance).Value = obj.Is_Insurance
                gv1.CurrentRow.Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Code, Nothing)
                gv1.CurrentRow.Cells(colUnit).Value = Nothing
                gv1.CurrentRow.Cells(colQty).Value = Nothing
                gv1.CurrentRow.Cells(colRate).Value = Nothing
            Else
                SetBlankOfItemColumns()
            End If
            ''End of Misc Charges 

        End If


        SetitemWiseTaxSetting(True, True)
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colHSNNo).Value = ""
        gv1.CurrentRow.Cells(colItemTaxable).Value = False
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colBalanceQty).Value = 0
        gv1.CurrentRow.Cells(colPendingQty).Value = 0
        gv1.CurrentRow.Cells(colRGPNo).Value = ""
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

#Region "item level additional charge calculation"
    Private Sub Calc_AddtionalCharge_Itemwise(ByVal TotalQty As Double)
        Try

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
                qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
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

    Private Sub UpdateAllTotals()
        Dim isInsuranceExists As Boolean = False
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colPONo).Value) <= 0 Then
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
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colPONo).Value) <= 0 Then
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
        Dim dblTotalQuantity As Double = 0

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
        Dim dblTaxableAmount As Double = Nothing

        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTaxableAmount = dblTaxableAmount + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxableAmount).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

                dblTotalQuantity = dblTotalQuantity + clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)


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
                        If dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 2
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 3
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 4
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
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        dblNetAmt = dblNetAmt + dblACAmount
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        Calc_AddtionalCharge_Itemwise(dblTotalQuantity)
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
        LoadBlankGridAC()
        LoadBlankGridACInsurance()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btn_Amendment.Enabled = False
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        btnUnpost.Visible = False
        MakeColumnReadOnly(False)
        AllowDepartmentMandatoryOnPurchaseCycle()
        txtSubLocation.Enabled = True
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        UcAttachment1.BlankAllControls()
    End Sub
    Private Sub frmGRN_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id As String = Form_ID
        Try
            'If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmGRN) = CompairStringResult.Equal Then
            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal Then
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.mbtnGRN) = CompairStringResult.Equal Then
                RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Hidden
                RadPageView1.Pages("RadPageViewPage7").Item.Visibility = ElementVisibility.Hidden
                btnRejected.Visible = False
                'RadPageViewPage5.Visible = False
                'RadPageViewPage7.Visible = False

            ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.VisualRandomQC) = CompairStringResult.Equal Then
                txtDocNo.Value = ""
                'txtDocNo.Value = clsCommon.GETSERVERDATE()
                'txtDCSDate.Value = txtShiftDate.Value
                'cboShift.SelectedValue = "M"
                RadGroupBox1.Enabled = False
                RadPageViewPage1.Text = "Visual Random QC"
                RadGroupBox1.HeaderText = "Visual Random QC"
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage6").Item.Visibility = ElementVisibility.Collapsed
                'RadPageView1.Pages("UcAttachment1").Item.Visibility = ElementVisibility.Collapsed
                'RadPageView1.Pages("SplitContainer1.Panel2").Item.Visibility = ElementVisibility.Collapsed
                'SplitContainer1.Panel2Collapsed = True
                btnClose.Visible = True
                btnPrint.Visible = True
                btnPrint.Location = New Point(5, 4)
                btnRejected.Visible = True
                btnRejected.Location = New Point(78, 4)
                btnSave.Visible = False
                btnPost.Visible = False
                btnDelete.Visible = False
                RadButton1.Visible = False
                btn_Amendment.Visible = False
                btnUnpost.Visible = False
                'End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Function AllowToSave() As Boolean
        Dim dt As DataTable
        If AutoClosePO Or AutoClosePOBasedOnSRNQtyWithTolerance Then
            If clsCommon.myLen(txtinvoiceno.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please enter invoice no.", Me.Text)
                txtinvoiceno.Focus()
                txtinvoiceno.Select()
                Return False
            End If
            ''RICHA AGARWAL DONE ON 19 APR,2018 AGAINST TICKET NO UDL/13/04/18-000098
            ''Add by balwinder becuase it give msg in KDIL by Ranjan Mam.It should be in AutoClosePO Setting.
            If clsCommon.GetDateWithStartTime(txt_invdate.Value) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Invoice Date can't be greater than Document Date", Me.Text)
                Return False
            End If
            ''--------
        End If
        '= KUNAL > TICKET : BM00000009580 ===========
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If

        CalculateInsuranceTotal(False)
        Dim intChange As Integer = gv1.CurrentRow.Index
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If PurchaseModulePickFixTaxRate AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colPONo).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                gv1.CurrentRow = gv1.Rows(ii)
                SetitemWiseTaxSetting(True, True)
            End If
            UpdateCurrentRow(ii)
        Next

        UpdateAllTotals()
        If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
            txtVendorNo.Focus()
            txtVendorNo.Select()
            Return False
        End If
        UcAttachment1.AllowToSave()
        'CLEINT : UDL > DATE : 27-01-2017 > ASKED BY BALWINDER SIR
        If clsCommon.myLen(txtGENo.Text) > 0 Then
            If txtGEDate.Checked = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Gate Entry Date.", Me.Text)
                txtGEDate.Focus()
                Return False
            End If
        End If

        If clsCommon.CompairString(cmbGRNType.SelectedValue, "") = CompairStringResult.Equal Then
            RadPageView1.SelectedPage = RadPageViewPage1
            cmbGRNType.Select()
            clsCommon.MyMessageBoxShow(Me, "Select GRN Type.", Me.Text)
            Return False
        End If
        ''richa agarwal BM00000006850 add condition in below clsCommon.myLen(txtReqNo.Value) <= 0
        If Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtReqNo.Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            cmbRGPType.Focus()
            cmbRGPType.Select()
            clsCommon.MyMessageBoxShow(Me, "Select RGP Type.", Me.Text)
            Return False
        End If

        If clsCommon.CompairString("O", cmbGRNType.SelectedValue) = CompairStringResult.Equal Then
            If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Sub Location", Me.Text)
                Return False
            End If
        End If

        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage2
            common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
            txtTaxGroup.Focus()
            txtTaxGroup.Select()
            Return False
        End If
        If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
            txtBillToLocation.Focus()
            txtBillToLocation.Select()
            Return False
        End If

        '===========Added By Rohit on Aug 12,2015=======
        If clsCommon.myLen(txtShipToLocation.Value) > 0 And Not isApplyBrachAccounting Then
            If Not clsCommon.CompairString(txtShipToLocation.Value, txtBillToLocation.Value) = CompairStringResult.Equal Then
                Dim qry As String = "select [State] from TSPL_LOCATION_MASTER where Location_Code in ('" + txtShipToLocation.Value + "','" + txtBillToLocation.Value + "') group by State"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please define State Location of bill to location and ship to location")
                End If
                If dt.Rows.Count > 1 Then
                    Throw New Exception("State should be same of bill to location and ship to location")
                End If

            End If

        End If
        '==================================================
        If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            common.clsCommon.MyMessageBoxShow(Me, "GRN No Not found to save", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            Return False
        End If
        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
            cboItemType.Focus()
            Return False
        End If

        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PurchaseOrder_Date,103) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + txtReqNo.Value + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0")) > clsCommon.myCDate(txtDate.Value) Then
            txtDate.Focus()
            Throw New Exception("Date cannot be less than from PO Date")
        End If
        Dim arrICode As New List(Of String)
        Dim arrReqNo As New List(Of String)
        Dim arrRGPNo As New List(Of String)
        Dim arrSchNo As New List(Of String)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            gv1.Focus()
            gv1.Select()
            RadPageView1.SelectedPage = RadPageViewPage1

            Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)
            Dim strrgpNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
            Dim strScheduleNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
            Dim dblAmtAfterDis As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
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
                ' added by priti BHA/26/07/18-000194 to validate cost for PO blanket
                If dblRate = 0 And clsCommon.myLen(strReqNo) > 0 Then
                    Dim dblIsblanket As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isblanket from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" & strReqNo & "'"))
                    If dblIsblanket = 1 Then
                        clsCommon.MyMessageBoxShow("Fill Rate at row no." + clsCommon.myCstr(ii + 1) + " Rate id mandatory for open PO. ", Me.Text)
                        Return False
                    End If
                End If
            End If


            'because user can issue more qty if available in selected month and schedule is of daily or weekly based.
            If clsCommon.myLen(strScheduleNo) > 0 AndAlso clsCommon.myLen(strrgpNo) <= 0 Then
                dblPendingQty = clsPurchaseScheduleDetail.GetBalanceScheduleQty(strScheduleNo, strICode, txtDocNo.Value, txtDate.Text, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), True)
            End If

            If dblAmtAfterDis < 0 Then
                clsCommon.MyMessageBoxShow(Me, " Amount After discount Cannot be in Negative. ")
                Return False
            End If

            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)

            If clsCommon.myLen(strICode) > 0 AndAlso dblQty <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv1.CurrentRow = gv1.Rows(ii)
                gv1.CurrentColumn = gv1.Columns(colQty)
                clsCommon.MyMessageBoxShow("Fill quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                Return False
            End If
            '=======================added by shivani check item stock sent by vendor in case of against Bom when isRGPAfterPO setting is on  ==========================================================
            If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colRGPNo).Value) > 0 AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso Is_RGP_After_PO Then
                Dim strMsg As String = clsRGPHead.GetRGPTypeItemBalance(cmbRGPType.SelectedValue, dblQty, txtVendorNo.Value, txtDocNo.Value, clsCommon.myCstr(strICode), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDate.Value, Nothing, False)
                If clsCommon.myLen(strMsg) > 0 Then
                    gv1.Rows(ii).Cells(colQty).Value = 0
                    clsCommon.MyMessageBoxShow(strMsg)
                    Return False
                End If
            End If
            '===============================================================================================
            If clsCommon.myLen(strReqNo) > 0 OrElse clsCommon.myLen(strrgpNo) > 0 OrElse clsCommon.myLen(strScheduleNo) > 0 Then
                If clsCommon.myLen(strReqNo) > 0 Then
                    If Not (arrReqNo.Contains(strReqNo)) Then
                        arrReqNo.Add(strReqNo)
                    End If
                End If
                If clsCommon.myLen(strrgpNo) > 0 AndAlso Not arrRGPNo.Contains(strrgpNo) Then
                    arrRGPNo.Add(strrgpNo)
                End If
                If clsCommon.myLen(strScheduleNo) > 0 AndAlso Not arrSchNo.Contains(strScheduleNo) Then
                    arrSchNo.Add(strScheduleNo)
                End If
                If AutoClosePO Then
                    dblPendingQty = dblPendingQty + ((clsCommon.myCdbl(gv1.Rows(ii).Cells(colOrgPOQty).Value) * (clsGRNHead.ToleranceQty(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), Nothing))) / 100)
                End If
                If clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal Then

                ElseIf dblQty > dblPendingQty Then
                    If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colBlanketPO).Value) Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colQty)
                        Dim strTemp As String = ""
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colPOControlOnReceive).Value) Then
                            strTemp = "Amount : " + clsCommon.myCstr(Math.Round(dblPendingQty * clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value), 2, MidpointRounding.AwayFromZero))
                        End If
                        common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity with Damage(" + clsCommon.myCstr(dblQty) + ") Cannot be more than Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ") " + strTemp + " .At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If
            End If
            If Not arrICode.Contains(strICode) Then
                arrICode.Add(strICode)
            End If
            If AllowPurchaseModulewithUniqueItem = 1 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        If jj = ii Then
                            Continue For
                        End If
                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                            RadPageView1.SelectedPage = RadPageViewPage1
                            common.clsCommon.MyMessageBoxShow(Msg)
                            Return False
                        End If
                    Next
                End If
            End If
            'If Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.myLen(strICode) > 0 Then
            '    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
            '        If clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strrgpNo, clsCommon.myCstr(gv1.Rows(jj).Cells(colRGPNo).Value)) = CompairStringResult.Equal Then
            '            gv1.CurrentRow = gv1.Rows(jj)
            '            gv1.CurrentColumn = gv1.Columns(colICode)
            '            Throw New Exception("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
            '        End If
            '    Next
            'End If

            '' added code by parteek HSN Code related
            Dim IsSkip As Boolean = False
            IsSkip = clsDBFuncationality.getSingleValue("select case when isnull( Skip_GST,0)=1 then 1 else 0 end as Skip_GST from tspl_item_master where item_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) & "'")
            If clsERPFuncationality.GetGSTStatus(txtDate.Value) AndAlso IsSkip = False Then
                'If ShowItemAllStructureWise = False Then
                If clsCommon.CompairString(cboItemType.SelectedValue, "N") <> CompairStringResult.Equal Then
                        Dim taxamt As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                        Dim HSNCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHSNNo).Value)

                    If clsCommon.myCdbl(taxamt) > 0 AndAlso clsCommon.myLen(HSNCode) <= 0 Then
                        clsCommon.MyMessageBoxShow("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If

                End If
                ' End If
            End If
            '' ===== ENd of code===

        Next
        If arrICode.Count <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            gv1.Focus()
            gv1.Select()
            If gv1.Rows.Count > 0 Then
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            clsCommon.MyMessageBoxShow(Me, "Fill atleast one item in grid.", Me.Text)
            Return False
        End If

        If clsCommon.CompairString(cmbGRNType.SelectedValue, "I") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select multi-currency for import entry.", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage4
            txtCurrencyCode.Focus()
            txtCurrencyCode.Select()

            Return False
        End If
        '=======================added by shivani in case of job work (RGP Type - Against RGP,As it is)
        If Is_RGP_After_PO AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtRgp_no.Value) = 0 AndAlso (clsCommon.CompairString(cmbRGPType.SelectedValue, "AI") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbRGPType.SelectedValue, "AR") = CompairStringResult.Equal) Then
            clsCommon.MyMessageBoxShow(Me, "Please select RGP No,it is mandatory to select", Me.Text)
            Return False
        End If

        ' Add By Prabhakar 24/11/2016
        '*****************************************
        If clsCommon.myLen(txtReqNo.Value) <= 0 AndAlso clsCommon.myLen(txtRgp_no.Value) <= 0 Then
            Dim GRNLim As Double = 0
            Dim GRNAmt As Double = 0
            GRNLim = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(GRN_Limit,0) AS GRN_Limit From TSPL_PURCHASE_SETTINGS"))
            If GRNLim > 0 Then
                GRNAmt = clsCommon.myCdbl(lblTotRAmt.Text)
                If GRNAmt > GRNLim Then
                    common.clsCommon.MyMessageBoxShow(" Document amount (" & clsCommon.myCstr(GRNAmt) & ") is more than GRN limit (" & clsCommon.myCstr(GRNLim) & ")", Me.Text)
                    Return False
                End If
            End If
        End If



        If ShowItemAllStructureWise = False Then
            clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
        Else
            Dim itemtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 item_type from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrICode) + ") "))
            cboItemType.SelectedValue = itemtype

        End If

        clsPurchaseOrderHead.IsValidVendorForPO(arrReqNo, txtVendorNo.Value)
        clsPurchaseOrderHead.IsValidTaxGroupForPO(arrReqNo, txtTaxGroup.Value)

        clsPurchaseSchedule.IsValidVendorForSchedule(arrSchNo, txtVendorNo.Value)
        clsRGPHead.IsValidVendorForRGP(arrRGPNo, txtVendorNo.Value)

        'clsLocationWiseTax.IsValidTaxGroup(txtTaxGroup.Value, txtBillToLocation.Value, txtVendorNo.Value, "P", txtDate.Value, Nothing)
        ''For GST Skip
        Dim isSkipGST As Boolean = False
        dt = clsDBFuncationality.GetDataTable("Select sum(Case When isnull( Skip_GST, 0) = 1 Then 1 Else 0 End) As NoOfSkipGSTItem, sum(case when isnull( Skip_GST, 0) = 0 Then 1 Else 0 End) As NoOfNonSkipGSTItem from tspl_item_master where item_Code In (" + clsCommon.GetMulcallString(arrICode) + ")")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If clsCommon.myCdbl(dt.Rows(0)("NoOfSkipGSTItem")) > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("NoOfNonSkipGSTItem")) > 0 Then
                    clsCommon.MyMessageBoxShow("All Item should be Of Skip GST Or Not", Me.Text)
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

        'Check Slab
        If SettPurchaseSlabApplyRange Then
            Dim qry As String = "Select 1 from TSPL_VENDOR_MASTER where vendor_code='" + txtVendorNo.Value + "' and isnull(OEM,0)=1"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                If clsCommon.myCDecimal(lblTotRAmt.Text) >= SettPurchaseSlabRangeNotApplicableFrom AndAlso clsCommon.myCDecimal(lblTotRAmt.Text) <= SettPurchaseSlabRangeNotApplicableTo Then
                    ''No Need To Check
                ElseIf clsCommon.myCDecimal(lblTotRAmt.Text) >= SettPurchaseSlabRangePOFrom AndAlso clsCommon.myCDecimal(lblTotRAmt.Text) <= SettPurchaseSlabRangePOTo Then
                    If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                        Throw New Exception("Purchase Order is Required for PO Amount [" + lblTotRAmt.Text + "]")
                    End If
                ElseIf clsCommon.myCDecimal(lblTotRAmt.Text) >= SettPurchaseSlabRangeRALFrom AndAlso clsCommon.myCDecimal(lblTotRAmt.Text) <= SettPurchaseSlabRangeRALTo Then
                    If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                        Throw New Exception("RAL is Mandatory")
                    Else
                        qry = "select 1 from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtReqNo.Value + "' and  Against_Tender='Y' and len(isnull( RefTendorNo,''))>0"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("RAL is Mandatory")
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekPostBtn As Boolean, Optional ByVal isamendment As Boolean = False)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsGRNHead()
                obj.IsSkipPurchaseQC = IIf(chkSkipPurchaseQc.Checked = True, 1, 0)
                obj.isJobWorkOutward = IIf(chkJobWorkOutward.Checked = True, 1, 0)
                obj.GRN_No = txtDocNo.Value
                obj.GRN_Date = txtDate.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Bill_To_Location = txtBillToLocation.Value
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Sublocation_Code = txtSubLocation.Value
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.PurchaseOrder_Type = clsCommon.myCstr(cmbGRNType.SelectedValue)
                obj.RGP_Type = clsCommon.myCstr(cmbRGPType.SelectedValue)
                obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
                'stuti

                If txt_RoadPermitDate.Text IsNot Nothing AndAlso clsCommon.myLen(txt_RoadPermitDate.Text) > 0 AndAlso IsDate(txt_RoadPermitDate.Text) Then
                    obj.RoadPermit_Date = clsCommon.myCDate(txt_RoadPermitDate.Text)
                Else
                    obj.RoadPermit_Date = clsCommon.GETSERVERDATE()
                End If

                obj.RoadPermit_No = clsCommon.myCstr(txt_RoadPermitNo.Text)
                obj.Invoiceno = clsCommon.myCstr(txtinvoiceno.Text)
                obj.InvoiceDate = clsCommon.GetPrintDate(txt_invdate.Value, "dd/MMM/yyyy")
                obj.TransporterDocumentBility = clsCommon.myCstr(txt_transporterdocbility.Text)
                '====end here===
                If ShowItemAllStructureWise = True Then
                    'obj.Item_Type = "A"
                    ' Assuming dgvGrid is your DataGridView control
                    If gv1.Rows.Count > 0 Then
                        Dim itemcode As String = clsCommon.myCstr(gv1.Rows(0).Cells(colICode).Value)
                        Dim itemtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 item_type from TSPL_ITEM_MASTER where Item_Code ='" + itemcode + "'"))
                        obj.Item_Type = itemtype
                    End If
                Else
                    obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                End If
                obj.Dept = txtDept.Value
                obj.Dept_Desc = lblDept.Text

                obj.IsCancel = 0
                If objCommonVar.RCDFCFP = True Then
                    If chkSkipPurchaseQc.Checked = True Then
                        obj.VisualQCStatus = 5
                    ElseIf RequiredVisualQC() = False Then
                        obj.VisualQCStatus = 5
                    End If
                Else
                    obj.VisualQCStatus = 5
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
                obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(lblAddChargesForInsurance.Text)
                obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(lblTotalInsuranceAmt.Text)
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                obj.Terms_Code = txtTermCode.Value
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
                obj.GRN_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

                obj.Carrier = txtCarrier.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GRNo = txtGRNo.Text
                obj.GENo = txtGENo.Text
                If txtGEDate.Checked Then
                    obj.GEDate = txtGEDate.Value
                End If

                '====================
                obj.Against_PO = txtReqNo.Value
                obj.Against_Schedule_Code = clsCommon.myCstr(txtSch_No.Value)
                obj.Against_RGP_No = clsCommon.myCstr(txtRgp_no.Value)
                obj.RGP_Non_Inventory_Item = clsCommon.myCstr(IIf(chkRGPNonInventory.Checked = True, "1", "0"))
                If clsCommon.myLen(obj.Against_RGP_No) > 0 AndAlso clsCommon.myLen(obj.Against_Schedule_Code) <= 0 Then
                    obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Schedule_Code from TSPL_RGP_HEAD where RGP_NO='" + obj.Against_PO + "'"))
                End If

                If clsCommon.myLen(obj.Against_Schedule_Code) > 0 AndAlso clsCommon.myLen(obj.Against_PO) <= 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_code from TSPL_PO_SCH_HEAD where document_code='" + obj.Against_PO + "'"))
                End If

                If clsCommon.myLen(obj.Against_RGP_No) > 0 AndAlso clsCommon.myLen(obj.Against_Schedule_Code) <= 0 AndAlso clsCommon.myLen(obj.Against_PO) <= 0 Then
                    obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_id from TSPL_RGP_HEAD where RGP_NO='" + obj.Against_PO + "'"))
                End If
                '=============================================

                If clsCommon.myLen(obj.Against_PO) > 0 AndAlso clsCommon.myLen(obj.Against_Requisition) <= 0 Then
                    obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0"))
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





                obj.Arr = New List(Of clsGRNDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsGRNDetail()
                    Dim objhsn As New ClsHSNMaster()
                    'done by stuti n 20/10/2016 against purchase points
                    objTr.Category = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                    objTr.Emergency = CInt(clsCommon.myCdbl(grow.Cells(colEmergency).Value))
                    objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                    objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)
                    Dim hsn As String = Nothing
                    Dim hsncode As String = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(grow.Cells(colICode).Value), Nothing)
                    Dim hsncode1 As String = clsItemMaster.checkHSNCode(clsCommon.myCstr(grow.Cells(colHSNNo).Value), Nothing)
                    If clsCommon.myLen(hsncode) <= 0 Then
                        hsn = clsCommon.myCstr(grow.Cells(colHSNNo).Value)
                        clsItemMaster.UpdateHSNCode(hsn, clsCommon.myCstr(grow.Cells(colICode).Value), Nothing)
                    End If
                    If clsCommon.myLen(hsncode1) <= 0 Then
                        Dim isnew As Boolean = True
                        hsn = clsCommon.myCstr(grow.Cells(colHSNNo).Value)
                        If clsCommon.myLen(hsn) > 0 Then
                            objhsn.Code = hsn
                            ClsHSNMaster.SaveData(objhsn, isnew)
                        End If
                    End If
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.GRN_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    If clsCommon.myCdbl(grow.Cells(colQty).Value) > clsCommon.myCdbl(grow.Cells(colPendingQty).Value) Then
                        objTr.Tolerence_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value) - clsCommon.myCdbl(grow.Cells(colPendingQty).Value)
                    End If
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.PO_Id = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    objTr.Requisition_Id = clsCommon.myCstr(grow.Cells(colReqNo).Value)
                    objTr.Against_RGP_No = clsCommon.myCstr(grow.Cells(colRGPNo).Value)
                    objTr.Against_Schedule_Code = clsCommon.myCstr(grow.Cells(colScheduleNo).Value)
                    objTr.RGP_Item_Code = Nothing
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtReqNo.Value) > 0 Then
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Tag)
                    Else
                        objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    End If

                    objTr.Header_Discount_Per = clsCommon.myCdbl(grow.Cells(colHeaderDiscountPer).Value)
                    objTr.Header_Discount_Amount = clsCommon.myCdbl(grow.Cells(colHeaderDiscountAmt).Value)
                    objTr.Detail_Discount_Amount = clsCommon.myCdbl(grow.Cells(colDetailDisAmt).Value)

                    objTr.Disc_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisPerUnit).Value)
                    objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(grow.Cells(colDisAmtPerUnit).Value)

                    objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)

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
                    ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                    objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)

                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                    If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                        objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                    End If
                    If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                        objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                    End If
                    objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value) + clsCommon.myCdbl(grow.Cells(colLeakQty).Value) + clsCommon.myCdbl(grow.Cells(colBurstQty).Value) + clsCommon.myCdbl(grow.Cells(colShortQty).Value)


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
                    If clsCommon.myLen(grow.Cells(colRGPNo).Value) > 0 Then
                        obj.Against_RGP_No = clsCommon.myCstr(grow.Cells(colRGPNo).Value)
                    End If
                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colInsuranceBaseAmt).Value)
                    objTr.Insurance_Per = clsCommon.myCdbl(grow.Cells(colInsurancePer).Value)

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item", Me.Text)
                    Return
                End If

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
                '' Anubhooti 03-Sep-2014 BM00000003657
                obj.LR_No = txtLRNo.Text
                obj.LR_Date = dtpLRDate.Value
                ''
                obj.Arr_ACInsurance = New List(Of clsGRNAdditionChargeInsurance)
                For Each grow As GridViewRowInfo In gvACInsurance.Rows
                    Dim objtr As New clsGRNAdditionChargeInsurance()
                    objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                    If clsCommon.myLen(objtr.AC_Code) > 0 Then
                        obj.Arr_ACInsurance.Add(objtr)
                    End If
                Next

                If (obj.SaveData(obj, isNewEntry, isamendment)) Then
                    UcAttachment1.SaveData(obj.GRNo)
                    If ChekPostBtn = True Then
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.GRN_No, NavigatorType.Current)
                End If
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

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btn_Amendment.Enabled = False
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            txtReqNo.Enabled = True

            txtRgp_no.Enabled = True
            txtSch_No.Enabled = True
            chkRGPNonInventory.Enabled = True
            cmbGRNType.Enabled = True

            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            LoadBlankGridACInsurance()
            cboItemType.Enabled = False
            txtBillToLocation.Enabled = False
            txtSubLocation.Enabled = False
            Dim obj As New clsGRNHead()
            obj = clsGRNHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GRN_No) > 0) Then
                If btn_Amendment.Visible = False Then
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        txtReqNo.Enabled = True
                    Else
                        txtReqNo.Enabled = False
                    End If

                End If
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btn_Amendment.Enabled = True
                    btnDelete.Enabled = False
                    repoComplete.IsVisible = True
                    'If Not clsGRNHead.CheckGRNUsedInSRNorMRN(clsCommon.myCstr(obj.GRN_No), Nothing) Then
                    '    btncancel.Visible = True
                    'Else
                    '    btncancel.Visible = False
                    'End If
                    '' repoBalQty.IsVisible = True
                    btncancel.Enabled = True
                Else
                    btncancel.Enabled = False
                End If
                'If Not clsGRNHead.CheckGRNUsedInSRNorMRN(clsCommon.myCstr(obj.GRN_No), Nothing) Then
                '    btncancel.Visible = True
                'Else
                '    btncancel.Visible = False
                'End If

                If CInt(obj.IsCancel) = CInt(1) Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btn_Amendment.Enabled = False
                    btnDelete.Enabled = False
                    btncancel.Visible = False
                End If
                chkSkipPurchaseQc.Checked = IIf(obj.IsSkipPurchaseQC = 1, True, False)
                chkJobWorkOutward.Checked = IIf(obj.isJobWorkOutward = 1, True, False)
                chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(obj.Vendor_Code)
                chkSkipPurchaseQc.Enabled = IIf(clsVendorMaster.IsAllowSkipPurchaseQC(obj.Vendor_Code) = True, True, False)
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.GRN_No
                txtDate.Value = obj.GRN_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                cmbGRNType.SelectedValue = obj.PurchaseOrder_Type
                'stuti
                If obj.RoadPermit_Date IsNot Nothing AndAlso clsCommon.myLen(obj.RoadPermit_Date) > 0 AndAlso IsDate(obj.RoadPermit_Date) Then
                    txt_RoadPermitDate.Text = obj.RoadPermit_Date
                End If
                txt_RoadPermitNo.Text = obj.RoadPermit_No
                txtinvoiceno.Text = obj.Invoiceno
                If obj.InvoiceDate IsNot Nothing AndAlso clsCommon.myLen(obj.InvoiceDate) > 0 AndAlso IsDate(obj.InvoiceDate) Then
                    txt_invdate.Value = obj.InvoiceDate
                End If
                txt_transporterdocbility.Text = obj.TransporterDocumentBility
                '=======end here=====
                If clsCommon.myLen(obj.PurchaseOrder_Type) > 0 Then
                    cmbGRNType.Enabled = False
                End If

                cmbRGPType.SelectedValue = obj.RGP_Type
                If clsCommon.myLen(obj.RGP_Type) <= 0 AndAlso clsCommon.myLen(txtRgp_no.Value) > 0 Then 'only when against rgp
                    cmbRGPType.SelectedValue = "AR"
                End If

                txtComment.Text = obj.Comments
                txtShipToLocation.Value = obj.Ship_To_Location
                lblShipToLocation.Text = clsLocation.GetName(obj.Ship_To_Location, Nothing)
                txtBillToLocation.Value = obj.Bill_To_Location
                txtSubLocation.Value = obj.Sublocation_Code
                lblShipToLocation.Text = obj.SubLocationName
                txtRemarks.Text = obj.Remarks
                TxtRetention.Text = obj.Retention
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                If ShowItemAllStructureWise = False Then
                    cboItemType.SelectedValue = obj.Item_Type
                Else
                    LoadItemType()
                End If

                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                '' richa agarwal condition to check due date is in object or not
                If clsCommon.myLen(obj.Due_Date) > 0 Then
                    txtDueDate.Value = obj.Due_Date
                End If

                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.GRN_Total_Amt)
                lblTaxableAmount.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
                lblBillToLocation.Text = obj.BillToLocationName
                lblTaxGrpName.Text = obj.TaxGroupName
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                lblTermName.Text = obj.TermsName

                txtCarrier.Text = obj.Carrier
                txtVehicleNo.Text = obj.VehicleNo
                txtGRNo.Text = obj.GRNo
                txtGENo.Text = obj.GENo
                If obj.GEDate.HasValue Then
                    txtGEDate.Value = obj.GEDate
                    txtGEDate.Checked = True
                End If

                txtSch_No.Value = obj.Against_Schedule_Code
                txtRgp_no.Value = obj.Against_RGP_No
                chkRGPNonInventory.Checked = clsCommon.myCBool(IIf(obj.RGP_Non_Inventory_Item = "1", True, False))
                txtRgp_no.Enabled = False
                txtSch_No.Enabled = False
                chkRGPNonInventory.Enabled = False
                cboVisualQCStatus.SelectedIndex = obj.VisualQCStatus
                txtVisualQCRemarks.Text = obj.VisualQCRemarks
                If obj.VisualQCUpdatedDate.HasValue Then
                    dtpVisualQCStatus.Value = obj.VisualQCUpdatedDate
                Else
                    dtpVisualQCStatus.Value = obj.GRN_Date
                End If
                If obj.VisualQCUpdatedDateSecond.HasValue Then
                    dtpVisualQCStatusSecond.Value = obj.VisualQCUpdatedDateSecond
                Else
                    dtpVisualQCStatusSecond.Value = obj.GRN_Date
                End If
                cboVisualQCStatusSecond.SelectedIndex = obj.VisualQCStatusSecond
                txtVisualQCRemarksSecond.Text = obj.VisualQCRemarksSecond
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

                lblAddChargesForInsurance.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblAddChargesForInsurance1.Text = clsCommon.myFormat(obj.Total_Add_Charge_Insurance)
                lblTotalInsuranceAmt.Text = clsCommon.myFormat(obj.Total_Item_Insurance_Amt)


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsGRNDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objTr.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objTr.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objTr.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objTr.Capex_SubCode

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
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBlanketPO).Value = clsPurchaseOrderHead.BlanketPO(objTr.PO_Id, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.GRN_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PO_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqNo).Value = objTr.Requisition_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Against_Schedule_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objTr.Against_RGP_No


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        If clsCommon.myLen(txtReqNo.Value) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                        End If
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = objTr.OriginalROQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount

                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Tag = objTr.Disc_Per
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Disc_Per
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountAmt).Value = objTr.Header_Discount_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = objTr.Disc_Per_Unit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmtPerUnit).Value = objTr.Disc_Amt_Per_Unit

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount
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
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leak_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = objTr.Burst_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Short_Qty

                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If

                        If clsCommon.myLen(objTr.PO_Id) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPurchaseOrderDetail.GetBalancePOQtyByGRN(objTr.PO_Id, objTr.Item_Code, obj.GRN_No, objTr.Unit_code, objTr.MRP, objTr.Assessable)
                        End If
                        If clsCommon.myLen(objTr.Against_Schedule_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsPurchaseScheduleDetail.GetBalanceScheduleQty(objTr.Against_Schedule_Code, objTr.Item_Code, obj.GRN_No, obj.GRN_Date, objTr.Unit_code)
                        End If
                        If clsCommon.myLen(objTr.Against_RGP_No) > 0 AndAlso clsCommon.CompairString(obj.RGP_Type, "AB") <> CompairStringResult.Equal Then ''when against bom then not cal. pending
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsRGPDetail.GetBalanceRGPQty(objTr.Against_RGP_No, objTr.Item_Code, obj.GRN_No, objTr.Unit_code, clsCommon.myCBool(IIf(clsCommon.myCstr(cmbGRNType.SelectedValue).ToUpper() = "J", True, False)))
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
                        LoadPOReceiveControl(gv1.Rows.Count - 1)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsuranceBaseAmt).Value = objTr.Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInsurancePer).Value = objTr.Insurance_Per

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceBaseAmt).Value = objTr.Item_Insurance_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = objTr.Item_Insurance_Apply_On
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = objTr.Item_Insurance_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = objTr.Item_Insurance_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmtAfterInsurance).Value = objTr.Item_Amt_After_Insurance
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gvAC.Rows.AddNew()
                    End If
                End If
                If objCommonVar.RCDFCFP = True AndAlso (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Cancel) AndAlso obj.VisualQCStatus <> 5 Then
                    RequiredVisualQC()
                End If
                If objCommonVar.RCDFCFP = True AndAlso (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Cancel) AndAlso obj.VisualQCStatus <> 5 AndAlso obj.VisualQCStatus <> 0 Then
                    RequiredVisualQCSecond()
                End If
                SetitemWiseTaxOnlySetting()
                RefreshReqNo()
                RefreshRGPNo()
                RefreshSCHDNo()

                '' MULTICURRENCY

                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                    Me.txtApplicableFrom.Text = clsCommon.myCstr(clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
                End If

                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    Me.txtCurrencyCode.Enabled = False
                    MakeColumnReadOnly(True)
                Else
                    Me.txtCurrencyCode.Enabled = True
                    MakeColumnReadOnly(False)
                End If
                '' end  MULTICURRENCY
                '' Anubhooti 03-Sep-2014 BM00000003657
                txtLRNo.Text = obj.LR_No
                '' richa agarwal condition to check LR date is in object or not
                If obj.LR_Date.HasValue Then
                    dtpLRDate.Value = obj.LR_Date
                End If
                If clsCommon.myLen(txtReqNo.Value) > 0 Then
                    txtBillToLocation.Enabled = False
                    txtShipToLocation.Enabled = False
                End If
                ''
                If obj.Arr_ACInsurance IsNot Nothing AndAlso obj.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsGRNAdditionChargeInsurance In obj.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If
                UcAttachment1.LoadData(obj.GRN_No)
            Else
                AddNew()
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
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked AndAlso Not PurchaseModulePickFixTaxRate Then
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVTaxRateFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
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
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsGRNHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    '//stuti ticket no - BM00000009616 
                    If AutoClosePOBasedOnSRNQtyWithTolerance Then
                    ElseIf AutoClosePO AndAlso clsCommon.myLen(txtReqNo.Value) > 0 Then
                        If clsGRNHead.IsPOQtyRecv(txtReqNo.Value, Nothing) Then
                            clsPurchaseOrderHead.closepodata(txtReqNo.Value, True, "Y")
                        End If
                    End If
                    '=====end here======
                    LoadData(txtDocNo.Value, NavigatorType.Current)

                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        print(txtDocNo.Value)
                    End If

                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GenerateMRN()
        Try
            isNewEntry = True
            Dim obj As New clsMRNHead()
            obj.isJobWorkOutward = IIf(chkJobWorkOutward.Checked = True, 1, 0)
            obj.MRN_Date = txtDate.Value
            obj.Vendor_Code = txtVendorNo.Value
            obj.Vendor_Name = lblVendorName.Text
            obj.Ref_No = txtRefNo.Text
            obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
            obj.Remarks = txtRemarks.Text
            obj.Bill_To_Location = txtBillToLocation.Value
            obj.Ship_To_Location = txtShipToLocation.Value
            obj.Sublocation_Code = txtSubLocation.Value
            obj.Comments = txtComment.Text
            obj.On_Hold = chkOnHold.Checked
            obj.Description = txtDesc.Text
            obj.Tax_Group = txtTaxGroup.Value
            obj.PurchaseOrder_Type = clsCommon.myCstr(cmbGRNType.SelectedValue)
            obj.RGP_Type = clsCommon.myCstr(cmbRGPType.SelectedValue)
            obj.Retention = clsCommon.myCdbl(TxtRetention.Text)
            'stuti

            If txt_RoadPermitDate.Text IsNot Nothing AndAlso clsCommon.myLen(txt_RoadPermitDate.Text) > 0 AndAlso IsDate(txt_RoadPermitDate.Text) Then
                obj.RoadPermit_Date = clsCommon.myCDate(txt_RoadPermitDate.Text)
            Else
                obj.RoadPermit_Date = clsCommon.GETSERVERDATE()
            End If

            obj.RoadPermit_No = clsCommon.myCstr(txt_RoadPermitNo.Text)
            obj.InvoiceNo = clsCommon.myCstr(txtinvoiceno.Text)
            obj.InvoiceDate = clsCommon.GetPrintDate(txt_invdate.Value, "dd/MMM/yyyy")
            obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
            obj.Dept = txtDept.Value
            obj.Dept_Desc = lblDept.Text
            obj.IsCancel = 0

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
            obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(lblAddChargesForInsurance.Text)
            obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(lblTotalInsuranceAmt.Text)
            If rbtnTaxCalAutomatic.IsChecked Then
                obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
            ElseIf rbtnTaxCalManual.IsChecked Then
                obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
            End If
            obj.Terms_Code = txtTermCode.Value
            obj.Due_Date = txtDueDate.Value
            obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
            obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
            obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            obj.Total_Taxable_Amount = clsCommon.myCdbl(lblTaxableAmount.Text)
            obj.MRN_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)

            obj.Carrier = txtCarrier.Text
            obj.VehicleNo = txtVehicleNo.Text
            obj.GRNo = txtGRNo.Text
            obj.GENo = txtGENo.Text
            If txtGEDate.Checked Then
                obj.GEDate = txtGEDate.Value
            End If

            '====================
            obj.Against_PO = txtReqNo.Value
            obj.Against_GRN = txtDocNo.Value
            obj.Against_Schedule_Code = clsCommon.myCstr(txtSch_No.Value)
            obj.Against_RGP_No = clsCommon.myCstr(txtRgp_no.Value)
            If clsCommon.myLen(obj.Against_RGP_No) > 0 AndAlso clsCommon.myLen(obj.Against_Schedule_Code) <= 0 Then
                obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_Schedule_Code from TSPL_RGP_HEAD where RGP_NO='" + obj.Against_PO + "'"))
            End If

            If clsCommon.myLen(obj.Against_Schedule_Code) > 0 AndAlso clsCommon.myLen(obj.Against_PO) <= 0 Then
                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_code from TSPL_PO_SCH_HEAD where document_code='" + obj.Against_PO + "'"))
            End If

            If clsCommon.myLen(obj.Against_RGP_No) > 0 AndAlso clsCommon.myLen(obj.Against_Schedule_Code) <= 0 AndAlso clsCommon.myLen(obj.Against_PO) <= 0 Then
                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_id from TSPL_RGP_HEAD where RGP_NO='" + obj.Against_PO + "'"))
            End If
            '=============================================

            If clsCommon.myLen(obj.Against_PO) > 0 AndAlso clsCommon.myLen(obj.Against_Requisition) <= 0 Then
                obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "' and isnull(TSPL_PURCHASE_ORDER_HEAD.ISCANCEL,0)=0"))
            End If


            obj.Against_Schedule_Code = clsCommon.myCstr(clsCommon.myCstr(txtSch_No.Value))
            obj.Against_RGP_No = clsCommon.myCstr(gv1.Rows(0).Cells(colRGPNo).Value)
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

            obj.Arr = New List(Of clsMRNDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsMRNDetail()
                'done by stuti n 20/10/2016 against purchase points
                objTr.Category = clsCommon.myCstr(grow.Cells(colCategoryType).Value)
                objTr.Emergency = CInt(clsCommon.myCdbl(grow.Cells(colEmergency).Value))
                objTr.Capex_Code = clsCommon.myCstr(grow.Cells(colCapexCode).Value)
                objTr.Capex_SubCode = clsCommon.myCstr(grow.Cells(colCapexSubCode).Value)

                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                objTr.MRN_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.GRN_Id = clsCommon.myCstr(txtDocNo.Value)
                objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                objTr.PO_ID = clsCommon.myCstr(grow.Cells(colPONo).Value)
                objTr.Requisition_Id = clsCommon.myCstr(grow.Cells(colReqNo).Value)
                objTr.RGP_No = clsCommon.myCstr(grow.Cells(colRGPNo).Value)

                'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtReqNo.Value) > 0 Then
                    objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Tag)
                Else
                    objTr.Disc_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                End If

                objTr.Header_Discount_Per = clsCommon.myCdbl(grow.Cells(colHeaderDiscountPer).Value)
                objTr.Header_Discount_Amount = clsCommon.myCdbl(grow.Cells(colHeaderDiscountAmt).Value)
                objTr.Detail_Discount_Amount = clsCommon.myCdbl(grow.Cells(colDetailDisAmt).Value)

                objTr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)

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
                ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)

                objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                    objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                End If
                If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                    objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                End If
                objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value) + clsCommon.myCdbl(grow.Cells(colLeakQty).Value) + clsCommon.myCdbl(grow.Cells(colBurstQty).Value) + clsCommon.myCdbl(grow.Cells(colShortQty).Value)


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
                If clsCommon.myLen(grow.Cells(colRGPNo).Value) > 0 Then
                    obj.Against_RGP_No = clsCommon.myCstr(grow.Cells(colRGPNo).Value)
                End If
                objTr.Insurance_Base_Amt = clsCommon.myCdbl(grow.Cells(colInsuranceBaseAmt).Value)
                objTr.Insurance_Per = clsCommon.myCdbl(grow.Cells(colInsurancePer).Value)

                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item", Me.Text)
                Return
            End If

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
            ''
            obj.Arr_ACInsurance = New List(Of clsMRNAdditionChargeInsurance)
            For Each grow As GridViewRowInfo In gvACInsurance.Rows
                Dim objtr As New clsMRNAdditionChargeInsurance()
                objtr.AC_Code = clsCommon.myCstr(grow.Cells(colACInsuranceCode).Value)
                objtr.Amount = clsCommon.myCdbl(grow.Cells(colACInsuranceAmount).Value)
                If clsCommon.myLen(objtr.AC_Code) > 0 Then
                    obj.Arr_ACInsurance.Add(objtr)
                End If
            Next
            Dim isamendment As Boolean = False
            If (obj.SaveData(obj, isNewEntry, isamendment)) Then
                clsMRNHead.PostData(obj.MRN_No)
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
                If (clsGRNHead.DeleteData(txtDocNo.Value)) Then
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

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-GRN"
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
            'Dim qst As String = "select count(*) from TSPL_GRN_HEAD where GRN_No='" + txtDocNo.Value + "' and  GRN_Total_Amt>0"
            Dim qst As String = "select count(*) from TSPL_GRN_HEAD where GRN_No='" + txtDocNo.Value + "' "
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
    '============BM00000008167
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_GRN_HEAD.GRN_No as Code,FORMAT(CAST(GRN_Date AS DATETIME),'dd/MM/yyyy hh:mm tt') AS Date,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code], TSPL_GRN_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],GRN_Total_Amt as Amount,case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancel' when TSPL_GRN_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_GRN_HEAD.Against_PO as [Against PO Code] "
        If Is_RGP_After_PO Then
            qry += ",TSPL_GRN_HEAD.Against_RGP_No as [Against RGP Code] "
        End If
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as [Tendor No]
                 ,TSPL_GRN_HEAD.VehicleNo as [Vehicle No]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end as [Visual QC Status]
                 ,case when VisualQCRequired.Is_Visual_QC=0 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else 'Pending' end as [Visual QC Second Status]
                 from TSPL_GRN_HEAD LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code 
                 left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO 
                left outer join (
                SELECT TSPL_GRN_DETAIL.GRN_No as GRN_No,MAX(TSPL_ITEM_MASTER.Visual_QC) AS Is_Visual_QC FROM TSPL_GRN_DETAIL
                LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_GRN_DETAIL.ITEM_CODE
                GROUP BY TSPL_GRN_DETAIL.GRN_No) as VisualQCRequired on TSPL_GRN_HEAD.grn_no=VisualQCRequired.GRN_No"
        Dim whrClas As String = "  2=2  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("GRNFND", qry, "Code", whrClas, txtDocNo.Value, "GRN_Date desc", isButtonClicked, "GRN_Date"), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) <= 0 Then
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
                SaveData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                CloseForm()
            ElseIf Not e.Alt AndAlso Not e.Shift AndAlso (e.Control AndAlso e.KeyCode = Keys.F7) Then ''because setting is open at Alt+Shift+Cntl+F7, and after this short-cut works automatically creates problem ,so do change
                SelectPOItems()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
                If AllowAmendmentWithPasssword(MyBase.Form_ID, Nothing) Then
                    btn_Amendment.Visible = True
                Else
                    btn_Amendment.Visible = False
                End If
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                If MyBase.isReverse Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "sirc"
                    frm.strCode = "sireversandcreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnUnpost.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                    'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("POTeFNDde", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("POTaxGroFND", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, txtBillToLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
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
                            If rbtnTaxCalAutomatic.IsChecked Then
                                If isChangeRate Then
                                    If clsCommon.myCBool(gv1.Rows(intRowNo).Cells(colItemTaxable).Value) AndAlso PurchaseModulePickFixTaxRate Then
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
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating

        Dim whrCls As String = " "
        If clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal Then
            whrCls = " tspl_vendor_master.vendor_code in (select vendor_code from tspl_rgp_head where Against_BOM='1' and Against_JobWork='1' and Status='1') and tspl_vendor_master.Status='N' "
        ElseIf clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AI") = CompairStringResult.Equal Then
            whrCls = " tspl_vendor_master.vendor_code in (select vendor_code from tspl_rgp_head where TSPL_RGP_HEAD .Against_As_It_Is = 1 and TSPL_RGP_HEAD .Against_JobWork = 1  and Status='1') and tspl_vendor_master.Status='N' "

        ElseIf clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AR") = CompairStringResult.Equal Then
            whrCls = " tspl_vendor_master.vendor_code in (select vendor_code from tspl_rgp_head where TSPL_RGP_HEAD .Against_As_It_Is = 0 and TSPL_RGP_HEAD .Against_JobWork = 1 and TSPL_RGP_HEAD .Against_BOM = 0  and Status='1')  and tspl_vendor_master.Status='N' "
        Else
            whrCls = " tspl_vendor_master.Status='N'  and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        End If
        If objCommonVar.RCDFCFP = True Then
            whrCls += " and TSPL_VENDOR_MASTER.in_active_cf IS NULL OR TSPL_VENDOR_MASTER.in_active_cf = 'N'"
        End If
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVendorrFNDD", qry, "Code", whrCls, txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc,IsAllowSkipPurchaseQC from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'and TSPL_VENDOR_MASTER.Form_Type<>'VSP'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
            txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(txtVendorNo.Value)
            chkSkipPurchaseQc.Enabled = IIf(clsCommon.myCstr(dt.Rows(0)("IsAllowSkipPurchaseQC")) = "1", True, False)
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtTermCode.Value = ""
            lblTermName.Text = ""
            txtTaxGroup.Value = ""
            lblTaxGrpName.Text = ""
            chkVendorGrossReceipt.Checked = False

            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If
        SetTax()
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
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

        SetTax()



    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        'Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description from TSPL_SHIP_TO_LOCATION"
        'txtShipToLocation.Value = clsCommon.ShowSelectForm("POShfndr", qry, "Code", "", txtShipToLocation.Value, "Code", isButtonClicked)
        'lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtShipToLocation.Value = clsCommon.ShowSelectForm("BILLTOLOCPO", qry, "Code", WhrCls, txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtShipToLocation.Value + "'"))

    End Sub

    Private Sub btnRequistionItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Sub SelectPOItems()
        isInsideLoadData = True
        Dim frm As New frmPendingPO()
        If objCommonVar.RCDFCFP = True Then
            Dim objItemMaster As clsItemMaster
            objItemMaster = clsItemMaster.FinderForItem("", "", True)
            If objItemMaster IsNot Nothing AndAlso clsCommon.myLen(objItemMaster.Item_Code) > 0 Then
                frm.ItemForDocumentFilter = objItemMaster.Item_Code
            Else
                frm.ItemForDocumentFilter = Nothing
            End If
        End If
        frm.VendorCode = txtVendorNo.Value
        frm.VendorName = lblVendorName.Text
        frm.strCurrCode = txtDocNo.Value
        frm.PurchaseOrder_Type = cmbGRNType.SelectedValue
        frm.Is_From_RGP = True
        frm.dtGRNDate = txtDate.Value
        frm.IsItemInsuranceColumn = True
        frm.ShowDialog()
        Dim objPO As clsPurchaseOrderHead = Nothing
        LoadBlankGrid()
        Dim strtaxGroupOLD As String = txtTaxGroup.Value
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            objPO = clsPurchaseOrderHead.GetData(frm.ArrReturn(0).PurchaseOrder_No, NavigatorType.Current)
            If objPO IsNot Nothing AndAlso clsCommon.myLen(objPO.PurchaseOrder_No) > 0 Then
                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = objPO.Vendor_Code
                    lblVendorName.Text = objPO.Vendor_Name
                    chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(objPO.Vendor_Code)
                End If
                'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                chkJobWorkOutward.Checked = objPO.isJobWorkOutward
                txtBillToLocation.Value = objPO.Bill_To_Location
                lblBillToLocation.Text = objPO.BillToLocationName
                txtBillToLocation.Enabled = False
                txtShipToLocation.Value = objPO.Ship_To_Location
                lblShipToLocation.Text = clsLocation.GetName(objPO.Ship_To_Location, Nothing)
                txtShipToLocation.Enabled = False
                txtSubLocation.Value = objPO.Sublocation_Code
                lblSubLocation.Text = clsLocation.GetName(objPO.Sublocation_Code, Nothing)
                txtSubLocation.Enabled = False
                TxtRetention.Text = objPO.Retention
                'End If
                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtDesc.Text = objPO.Description
                End If
                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtRemarks.Text = objPO.Remarks
                End If
                If (clsCommon.myLen(txtRefNo.Text) <= 0) Then
                    txtRefNo.Text = objPO.Ref_No
                End If

                cmbGRNType.SelectedValue = objPO.PurchaseOrder_Type
                'If (clsCommon.myLen(txtReqNo.Value) <= 0) Then
                '    txtReqNo.Value = objReq.PurchaseOrder_No
                'End If
                ''RICHA AGARWAL AGAINST TICKET NO. BM00000006091 ON 04/05/2015
                txtCurrencyCode.Value = objPO.CURRENCY_CODE
                txtConversionRate.Value = objPO.ConvRate
                txtCurrencyCode.Enabled = False
                ''-------------------------------------------
                If (clsCommon.myLen(txtDept.Value) <= 0) Then
                    txtDept.Value = objPO.Dept
                    lblDept.Text = objPO.Dept_Desc
                End If
                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                    cboItemType.SelectedValue = objPO.Item_Type
                End If
                If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                    txtRemarks.Text = objPO.Remarks
                End If
                If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                    txtTermCode.Value = objPO.Terms_Code
                    lblTermName.Text = objPO.TermsName
                    If clsCommon.myLen(objPO.Due_Date) > 0 Then
                        txtDueDate.Value = objPO.Due_Date
                    End If
                End If
                If objPO.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf objPO.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                LoadBlankGridAC()
                LoadBlankGridACInsurance()
                If objPO.Arr_ACInsurance IsNot Nothing AndAlso objPO.Arr_ACInsurance.Count > 0 Then
                    For Each objtr As clsPurchaseOrderAdditionChargeInsurance In objPO.Arr_ACInsurance
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceCode).Value = objtr.AC_Code
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceName).Value = objtr.AC_Name
                        gvACInsurance.Rows(gvACInsurance.Rows.Count - 1).Cells(colACInsuranceAmount).Value = objtr.Amount
                        gvACInsurance.Rows.AddNew()
                    Next
                End If


                If (clsCommon.myLen(objPO.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt1
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(objPO.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objPO.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objPO.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objPO.Add_Charge_Amt10
                End If
                gvAC.Rows.AddNew()
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            Dim reqno As String = ""
            For Each obj As clsPurchaseOrderDetail In frm.ArrReturn
                'Dim strTaxGrp As String=
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = obj.PurchaseOrder_No 'obj.Requisition_Id
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqNo).Value = obj.Requisition_Id
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = obj.Row_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(obj.Item_Code, Nothing)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBlanketPO).Value = clsPurchaseOrderHead.BlanketPO(obj.PurchaseOrder_No, Nothing)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = obj.Item_Insurance_Apply_On
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = obj.Item_Insurance_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = obj.Item_Insurance_Amt


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Tag = frmPendingPO.Load_discount_for_GRN(obj.PurchaseOrder_No, obj.Item_Code) 'obj.Disc_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPO.Load_discount_for_GRN(obj.PurchaseOrder_No, obj.Item_Code) 'obj.Disc_Per
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPO.Load_discount_for_GRN(obj.PurchaseOrder_No, obj.Item_Code) 'obj.Disc_Per
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = obj.Detail_Discount_Amount

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = frmPendingPO.Load_discount_per_unit_for_GRN(obj.PurchaseOrder_No, obj.Item_Code) 'obj.Disc_Per'obj.Disc_Per_Unit
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPerUnit).Value = obj.Disc_Per_Unit

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = obj.Taxable_Amount_Per
                    If clsCommon.myCBool(gv1.Rows(gv1.Rows.Count - 1).Cells(colBlanketPO).Value) Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = obj.PurchaseOrder_Qty
                    End If

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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objPO.Against_RGP_NO
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAgainstItemWiseTaxCode).Value = obj.Against_Item_Wise_Tax_Rate
                    reqno = obj.PurchaseOrder_No
                    If ShowCapexCodeandSubCode Then
                        MakeColumnReadOnly(True)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objPO.Category
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objPO.Emergency)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objPO.Capex_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objPO.Capex_SubCode
                    End If
                    LoadPOReceiveControl(gv1.Rows.Count - 1)
                End If
            Next

            If objPO.Arr IsNot Nothing AndAlso objPO.Arr.Count > 0 Then
                For Each objTr As clsPurchaseOrderDetail In objPO.Arr
                    If objTr.Row_Type = "Misc" Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = reqno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqNo).Value = objTr.Requisition_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDetailDisAmt).Value = objTr.Detail_Discount_Amount

                        If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsAdditionalCharge.GetSACCode(objTr.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsInsurance).Value = clsAdditionalCharge.GetIsInsurance(objTr.Item_Code, Nothing)
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPO.Load_discount_for_GRN(reqno, objTr.Item_Code) 'objTr.Disc_Per
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
                        If rbtnTaxCalManual.IsChecked Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = objTr.Taxable_Amount_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHeaderDiscountPer).Value = objTr.Header_Discount_Per

                        If ShowCapexCodeandSubCode Then ''VIJ/11/12/19-000116 by balwinder on 18/12/2019
                            MakeColumnReadOnly(True)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = objPO.Category
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = CInt(objPO.Emergency)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = objPO.Capex_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = objPO.Capex_SubCode
                        End If
                    End If
                Next
            End If
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem

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

        If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If

        CalculateInsuranceTotal(True)
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshReqNo()

        gv1.Columns(colDisPer).ReadOnly = True
        gv1.Columns(colDisAmt).ReadOnly = True

        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(txtReqNo.Value) > 0 Then
                gv1.Rows(ii).Cells(colRate).ReadOnly = True
            End If
        Next

    End Sub



    Function IsValidItem(ByVal obj As clsPurchaseOrderDetail)
        If clsCommon.myLen(txtTaxGroup.Value) >= 0 Then
            txtTaxGroup.Value = obj.POTax_Group
            SetTaxDetails()
        End If

        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.POTax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " Purchase Order: " + obj.Requisition_Id + "  contain Tax Group :" + obj.POTax_Group + Environment.NewLine)
            Return False
        End If
        If clsCommon.CompairString(txtBillToLocation.Value, clsPurchaseOrderHead.GetLocationForPO(obj.PurchaseOrder_No)) = CompairStringResult.Equal Then
        Else
            common.clsCommon.MyMessageBoxShow("All PO's must be of same location.", Me.Text)
            Return False
        End If

        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            ''Dim dblAssessable As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Requisition_Id) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " Requition No : " + obj.Requisition_Id + "  Item : " + obj.Item_Desc + Environment.NewLine + " UOM : " + obj.Unit_code + ""
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
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                'If Not PurchaseModulePickFixTaxRate OrElse Not clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value) Then
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
                'End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If clsGRNDetail.CompleteGRN(txtDocNo.Value, strICode, intSNo) Then
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
            MessageBox.Show("Select the GRN No.")
        Else
            print(txtDocNo.Value)
        End If

    End Sub
    Public Sub print(ByVal StrDocNo As String)
        Dim strquery As String = Nothing
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 AndAlso clsCommon.myLen(StrDocNo) <= 0 Then
                MessageBox.Show("Select the GRN No.")
                Exit Sub

            End If
            If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal) Then
                strquery = "select TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,  detail.Unit_code,head.VehicleNo, head.GRN_No as 'GRN No', convert(varchar,head.GRN_Date,103) as 'GRN Date', head.Vendor_Code as 'Vendor Code', head.Vendor_Name as 'Vendor Name',head.Amount_Less_Discount as 'Amount After Discount',head.Comments as 'Comment', detail.Item_Code as 'Item Code', detail.Item_Desc as 'Descripton',detail.GRN_Qty as 'Quantity', detail.Item_Cost as 'Item Cost', detail.Disc_Amt as 'Discount', detail.Amount as 'Amount', HEAD.Discount_Base as 'Total Amount',HEAD.Discount_Amt as 'Discount Amount', HEAD.GRN_Total_Amt as 'Net Amount', tax1.Tax_Code_Desc as tax1name,isnull (HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (HEAD.tax10_amt,0) as txt10amt,isnull(HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, " &
                                   " CONCAT(ISNULL(TSPL_LOCATION_MASTER.ADD1, ''), ',', ISNULL(TSPL_LOCATION_MASTER.ADD2, ''), ',', ISNULL(TSPL_LOCATION_MASTER.ADD3, ''), ',', ISNULL(TSPL_LOCATION_MASTER.Add4, ''), ISNULL(TSPL_LOCATION_MASTER.State, '')) AS address1,head.Ref_No,head.Carrier,head.Against_PO,head.Posting_Date " &
                                   " from TSPL_GRN_HEAD as head " &
                                   " left outer join TSPL_GRN_DETAIL as detail on head.GRN_No=detail.grn_no " &
                                   " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =HEAD.tax1 " &
                                   " left outer join tspl_tax_master as tax2 on tax2.tax_code = HEAD.tax2 " &
                                   " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=HEAD .TAX3" &
                                   " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= HEAD .tax4" &
                                   " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=HEAD .tax5 " &
                                   " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =HEAD .TAX6 " &
                                   " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =HEAD .TAX7 " &
                                   " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =HEAD .TAX8 " &
                                   " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =HEAD .TAX9" &
                                   " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =HEAD .TAX10 " &
                                   " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = HEAD.comp_code " &
                                   " left outer join TSPL_LOCATION_MASTER ON HEAD.Bill_To_Location = TSPL_LOCATION_MASTER.Location_Code  AND tspl_location_master.Location_Code =  tspl_location_master.Loc_Segment_Code " &
                                   " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= detail.Item_Code " &
                                   " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = HEAD.Vendor_Code " &
                                   " left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state " &
                                   " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code " &
                                   " where 1=1 "
                If clsCommon.myLen(StrDocNo) Then
                    strquery += " and head.grn_no='" + StrDocNo + "'"
                End If
            Else
                strquery = "select TSPL_LOCATION_MASTER.Location_Desc, TSPL_LOCATION_MASTER.Add1 as Location_Add1, TSPL_LOCATION_MASTER.Add2 as Location_Add2 , TSPL_LOCATION_MASTER.Add3 as Location_Add3 , TSPL_LOCATION_MASTER.Add4 as Location_Add4 , TSPL_LOCATION_MASTER.Telphone as Location_Telphone , TSPL_LOCATION_MASTER.Email as Location_Email,TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant , TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,  detail.Unit_code,head.VehicleNo, head.GRN_No as 'GRN No', convert(varchar,head.GRN_Date,103) as 'GRN Date', head.Vendor_Code as 'Vendor Code', head.Vendor_Name as 'Vendor Name',head.Amount_Less_Discount as 'Amount After Discount',head.Comments as 'Comment', detail.Item_Code as 'Item Code', detail.Item_Desc as 'Descripton',detail.GRN_Qty as 'Quantity', detail.Item_Cost as 'Item Cost', detail.Disc_Amt as 'Discount', detail.Amount as 'Amount', HEAD.Discount_Base as 'Total Amount',HEAD.Discount_Amt as 'Discount Amount', HEAD.GRN_Total_Amt as 'Net Amount', tax1.Tax_Code_Desc as tax1name,isnull (HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (HEAD.tax10_amt,0) as txt10amt,isnull(HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,head.Ref_No,head.Carrier,head.Against_PO,head.Posting_Date  from TSPL_GRN_HEAD as head " &
                            " left outer join TSPL_GRN_DETAIL as detail on head.GRN_No=detail.grn_no " &
                            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code = HEAD.tax1 " &
                            " left outer join tspl_tax_master as tax2 on tax2.tax_code = HEAD.tax2 " &
                            " left outer join tspl_tax_master as tax3 on tax3.Tax_Code = HEAD .TAX3" &
                            " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code = HEAD .tax4" &
                            " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code = HEAD .tax5 " &
                            " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code = HEAD .TAX6 " &
                            " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code = HEAD .TAX7 " &
                            " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code = HEAD .TAX8 " &
                            " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code = HEAD .TAX9" &
                            " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code = HEAD .TAX10 " &
                            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = HEAD.comp_code " &
                            " left outer join TSPL_LOCATION_MASTER ON  ( HEAD.Bill_To_Location = TSPL_LOCATION_MASTER.Location_Code  and  HEAD.Bill_To_Location = tspl_location_master.Loc_Segment_Code ) " &
                            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= detail.Item_Code  " &
                            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = HEAD.Vendor_Code  " &
                            " left outer join tspl_state_master as tspl_state_master_for_location_state on tspl_state_master_for_location_state.state_code=tspl_location_master.state  " &
                            " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code " &
                            " where head.grn_no='" + StrDocNo + "'"

            End If
            If strquery IsNot Nothing AndAlso clsCommon.myLen(strquery) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    '==update by preeti gupta Against ticket no[ERO/30/04/19-000579]
                    'frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptGRNReport", "GRN Report", clsCommon.myCDate(dt.Rows(0)("GRN Date")))
                    frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinHeaderPartForERODE(), "crptGRNReport", "GRN Report", clsCommon.myCDate(dt.Rows(0)("GRN Date")), "SubRptCmpnyMasterForERODE.rpt")
                    frmCRV = Nothing
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) > 0 OrElse (clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 AndAlso clsCommon.myCstr(gv1.CurrentRow.Cells(colRGPNo).Value) <> "0") OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colRGPNo).Value) > 0 Then
                            gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                        End If
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
                    End If

                ElseIf e.Column Is gv1.Columns(colQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso Not clsCommon.myCBool(gv1.CurrentRow.Cells(colPOControlOnReceive).Value) Then
                        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colLeakQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colLeakQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colLeakQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colBurstQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colBurstQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colBurstQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colShortQty) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colShortQty).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colShortQty).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colRate) Then
                    If clsCommon.myCBool(gv1.CurrentRow.Cells(colPOControlOnReceive).Value) Then
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colBlanketPO).Value) Then
                        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        If clsCommon.myLen(txtReqNo.Value) > 0 Then
                            gv1.CurrentRow.Cells(colRate).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colRate).ReadOnly = False
                        End If
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.myCBool(gv1.CurrentRow.Cells(colPOControlOnReceive).Value) Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colInsurancePer) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colPONo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colInsurancePer).ReadOnly = True
                    End If
                ElseIf (e.Column Is gv1.Columns(colItemInsurancePer)) Then

                    gv1.CurrentRow.Cells(colItemInsurancePer).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnPercent, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemInsuranceApplyOn).Value)) = CompairStringResult.Equal, False, True)
                ElseIf (e.Column Is gv1.Columns(colItemInsuranceAmt)) Then
                    gv1.CurrentRow.Cells(colItemInsuranceAmt).ReadOnly = IIf(clsCommon.CompairString(clsCalculationlApplyON.RowTypeApplyOnAmount, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemInsuranceApplyOn).Value)) = CompairStringResult.Equal, False, True)

                ElseIf gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colHSNNo).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHSNNo).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colHSNNo).ReadOnly = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown

    End Sub

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
        End If

    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        SelectPOItems()
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

    Sub RefreshReqNo()
        txtReqNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtReqNo.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colLeakQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colLeakQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colBurstQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colBurstQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colShortQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colShortQty) Then
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
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If





            'If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            '    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colQty)
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colLeakQty)
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colLeakQty) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colBurstQty)
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colBurstQty) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colShortQty)
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colShortQty) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colRate)
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
            '        gv1.CurrentRow = gv1.Rows(intCurrRow)
            '        gv1.CurrentColumn = gv1.Columns(colDisPer)
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
            '    ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
            'gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
            'gv1.CurrentColumn = gv1.Columns(colICode)
            '    End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReceiveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceiveAll.Click
        ReceiveALL()
    End Sub

    Sub ReceiveALL()
        Dim arrPO As New List(Of String)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colPONo).Value) > 0 Then
                If Not arrPO.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value)) Then
                    arrPO.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value))
                End If
            End If
        Next
        Dim flag As Boolean = True
        If arrPO IsNot Nothing AndAlso arrPO.Count > 0 Then
            Dim qry As String = "select Code from TSPL_SET_PO_SCHEDULE where Status=1 and PO_No in (" + clsCommon.GetMulcallString(arrPO) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                flag = False
                For Each strPO As String In arrPO
                    qry = "select Code as DocumentNo,DDate as DocumentDate,Schedule_Date as ScheduleDate,PO_No as [PO No] from TSPL_SET_PO_SCHEDULE  "
                    Dim WhrCls As String = "Status=1 and PO_No in ('" + strPO + "')"
                    qry = clsCommon.ShowSelectForm("POSch@GRN", qry, "DocumentNo", WhrCls, "", "DocumentNo", True)
                    If clsCommon.myLen(qry) > 0 Then
                        qry = "select Item_Code,Qty,UOM from TSPL_SET_PO_SCHEDULE_DETAIL where Code='" + qry + "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        For Each dr As DataRow In dt.Rows
                            For ii As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value), strPO) = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(dr("Item_Code"))) = CompairStringResult.Equal Then
                                        gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCDecimal(dr("Qty"))
                                        UpdateCurrentRow(ii)
                                    End If
                                End If
                            Next
                        Next
                    End If
                Next
            End If
        End If

        If flag Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colPONo).Value) > 0 Then
                    If Not clsCommon.myCBool(gv1.CurrentRow.Cells(colPOControlOnReceive).Value) Then
                        gv1.Rows(ii).Cells(colQty).Value = gv1.Rows(ii).Cells(colPendingQty).Value
                        UpdateCurrentRow(ii)
                    End If
                End If
            Next
        End If

        For ii As Integer = 0 To gv1.RowCount - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblLeak As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value)
        Dim dblBurst As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value)
        Dim dblShort As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        ElseIf clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsInsurance).Value) AndAlso clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colPONo).Value) <= 0 Then
            dblAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsuranceBaseAmt).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInsurancePer).Value)) / 100
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
        End If

        Dim dblHeaderDisAmt As Decimal = Math.Round(dblAmt * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colHeaderDiscountPer).Value) / 100, 2, MidpointRounding.AwayFromZero)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dbldisperunit As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPerUnit).Value)
        Dim dbldisamtperunit As Decimal = (dblQty * dbldisperunit)
        Dim dblDetailDisAmt As Decimal = (dblAmt * dblDisPer) / 100
        Dim dblDisAmt As Double = dblDetailDisAmt + dblHeaderDisAmt + dbldisamtperunit


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
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value = Nothing
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

    Private Sub gvAC_CellValueChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAC.Columns(colACAmount) Then

                        UpdateAllTotals()
                    ElseIf e.Column Is gvAC.Columns(colACCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colACCode).Value), False)
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

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME As Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboItemType.SelectedIndexChanged

    End Sub

    Private Sub btnUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsGRNHead.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSch_No__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSch_No._MYValidating
        SelectScheduleItems()
    End Sub

    Sub SelectScheduleItems()
        isInsideLoadData = True
        Dim frm As New FrmPendingPOSchedule()
        frm.VendorCode = txtVendorNo.Value
        frm.VendorName = lblVendorName.Text
        frm.strCurrCode = txtDocNo.Value
        frm.strCurrDate = clsCommon.myCDate(txtDate.Text)
        frm.PurchaseOrder_Type = cmbGRNType.SelectedValue
        frm.Is_From_RGP = False
        frm.ShowDialog()

        txtReqNo.Enabled = True
        LoadBlankGrid()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objReq As clsPurchaseSchedule = clsPurchaseSchedule.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)
            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.Document_Code) > 0 Then

                txtReqNo.Enabled = False
                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                    chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                End If
                txtBillToLocation.Value = clsPurchaseSchedule.GetBillToLocation(objReq.PO_Code)
                lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)

                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtDesc.Text = objReq.Description
                End If

                If (clsCommon.myLen(txtSch_No.Text) <= 0) Then
                    txtSch_No.Value = objReq.Document_Code
                    txtReqNo.Value = objReq.PO_Code
                End If

                If clsCommon.myLen(cmbGRNType.SelectedValue) <= 0 Then
                    cmbGRNType.SelectedValue = objReq.PO_Type
                End If

                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                    cboItemType.SelectedValue = objReq.Arr(0).Item_Type
                End If
                If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                    txtRemarks.Text = objReq.Arr(0).Remarks
                End If
                If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                    txtTermCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Terms_Code from tspl_vendor_master where vendor_code='" + txtVendorNo.Value + "'"))
                    lblTermName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code_Desc from tspl_vendor_master where vendor_code='" + txtVendorNo.Value + "'"))
                End If
                LoadBlankGridAC()
                LoadBlankGridACInsurance()
                gvAC.Rows.AddNew()
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            Dim reqno As String = ""
            For Each obj As clsPurchaseScheduleDetail In frm.ArrReturn
                If IsValidItemForSchedule(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = obj.PO_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = obj.Document_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Schedule_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.balance_qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = frmPendingPO.Load_discount_for_GRN(obj.PO_Code, obj.Item_Code) 'obj.Disc_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = obj.Schedule_Qty
                End If
            Next

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshSCHDNo()
    End Sub

    Private Sub txtRgp_no__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRgp_no._MYValidating
        'BHA/11/12/18-000748 by balwinder on 12/12/2018
        If clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal Then
            Exit Sub
        End If
        SelectRGPItems()
    End Sub

    Sub SelectRGPItems()


        Dim isJobWork As String = ""
        isInsideLoadData = True
        Dim frm As New frmPendingRGP()
        frm.VendorCode = txtVendorNo.Value
        frm.VendorName = lblVendorName.Text
        frm.strCurrCode = txtDocNo.Value
        frm.strRGPType = cmbGRNType.SelectedValue
        frm.strJobWorkType = cmbRGPType.SelectedValue
        If clsCommon.CompairString(cmbGRNType.SelectedValue, "J") <> CompairStringResult.Equal Then
            frm.strCurrCode = "J"
        End If
        frm.ShowDialog()


        LoadBlankGrid()
        If (frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0) OrElse (frm.ArrReturn_Job IsNot Nothing AndAlso frm.ArrReturn_Job.Count > 0) Then
            Dim objReq As New clsRGPHead()
            If (frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 AndAlso clsCommon.myLen(frm.ArrReturn(0).RGP_No) > 0) Then
                objReq = clsRGPHead.GetData(frm.ArrReturn(0).RGP_No, NavigatorType.Current)
            ElseIf frm.ArrReturn_Job IsNot Nothing AndAlso frm.ArrReturn_Job.Count > 0 AndAlso clsCommon.myLen(frm.ArrReturn_Job(0).RGP_No) > 0 Then
                objReq = clsRGPHead.GetData(frm.ArrReturn_Job(0).RGP_No, NavigatorType.Current)
            End If

            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.RGP_No) > 0 Then
                isJobWork = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Against_JobWork,0) AS Against_JobWork From TSPL_RGP_HEAD  Where RGP_No='" & clsCommon.myCstr(objReq.RGP_No) & "'"))

                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                    chkVendorGrossReceipt.Checked = clsVendorMaster.isGrossReceipt(frm.VendorCode)
                End If
                'If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
                txtBillToLocation.Value = objReq.Location
                lblBillToLocation.Text = objReq.LocationName
                'End If
                If (clsCommon.myLen(txtDesc.Text) <= 0) Then
                    txtDesc.Text = objReq.Remarks
                End If
                If (clsCommon.myLen(txtRemarks.Text) <= 0) Then
                    txtRemarks.Text = objReq.Remarks
                End If

                txtRgp_no.Text = objReq.RGP_No
                chkRGPNonInventory.Checked = IIf(objReq.Is_Non_Inventory = 1, True, False)
                cmbGRNType.SelectedValue = "J"
                If objReq.Against_JobWork = 1 AndAlso objReq.Against_BOM = 0 AndAlso objReq.Against_As_It_Is = 0 Then
                    cmbRGPType.SelectedValue = "AR"
                ElseIf objReq.Against_JobWork = 1 AndAlso objReq.Against_BOM = 1 AndAlso objReq.Against_As_It_Is = 0 Then
                    cmbRGPType.SelectedValue = "AB"
                ElseIf objReq.Against_JobWork = 1 AndAlso objReq.Against_BOM = 0 AndAlso objReq.Against_As_It_Is = 1 Then
                    cmbRGPType.SelectedValue = "AI"
                End If

                If (clsCommon.myLen(txtDept.Value) <= 0) Then
                    txtDept.Value = objReq.Department
                    lblDept.Text = objReq.Department
                End If
                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
                    cboItemType.SelectedValue = objReq.ItemType
                End If

                If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                    txtTermCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code from tspl_vendor_master where vendor_code='" + txtVendorNo.Value + "'"))
                    lblTermName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code_Desc from tspl_vendor_master where vendor_code='" + txtVendorNo.Value + "'"))
                    'txtDueDate.Value = objReq.Due_Date
                End If

                txtTaxGroup.Value = clsDBFuncationality.getSingleValue("select top 1 Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Tax_Group_Type='P' and Is_Tax_Exempted=1")
                If clsCommon.myLen(txtTaxGroup.Value) = 0 Then
                    clsCommon.MyMessageBoxShow("Please create Exempted Type tax group for purchase", Me.Text)
                    Exit Sub
                Else
                    lblTaxGrpName.Text = clsDBFuncationality.getSingleValue("select top 1 Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & txtTaxGroup.Value & "' and Tax_Group_Type='P'")
                    SetTaxDetails()
                End If


                LoadBlankGridAC()
                LoadBlankGridACInsurance()
                gvAC.Rows.AddNew()
            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            If clsCommon.CompairString(isJobWork, "1") = CompairStringResult.Equal Then 'if rgp of job work type then fill rgp grid with issue material at rgp
                If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                End If


                For Each objReqDetail As clsRGPBOMItem In frm.ArrReturn_Job
                    If IsValidItemForJOBWORKRGP(objReqDetail) Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                            cboItemType.SelectedValue = clsCommon.myCstr(clsItemMaster.GetItemType(objReqDetail.Item_Code, Nothing))
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objReqDetail.RGP_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objReqDetail.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objReqDetail.Iname
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objReqDetail.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objReqDetail.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objReqDetail.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objReqDetail.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objReqDetail.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objReqDetail.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = objReqDetail.RGP_Qty
                    End If
                Next
            Else

                If clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "") = CompairStringResult.Equal Then
                    cmbRGPType.SelectedValue = "AR"
                End If

                For Each objReqDetail As clsRGPDetail In frm.ArrReturn
                    If IsValidItemForRGP(objReqDetail) Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
                        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                            cboItemType.SelectedValue = clsCommon.myCstr(clsItemMaster.GetItemType(objReqDetail.Item_Code, Nothing))
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = ""
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objReqDetail.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objReqDetail.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objReqDetail.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemTaxable).Value = clsItemMaster.IsTaxableItem(objReqDetail.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objReqDetail.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objReqDetail.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRGPNo).Value = objReqDetail.RGP_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = objReqDetail.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgPOQty).Value = objReqDetail.RGP_Qty
                    End If
                Next
            End If

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
            SetitemWiseTaxSetting(False, False)
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshRGPNo()
    End Sub

    Sub RefreshRGPNo()
        txtRgp_no.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtRgp_no.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Sub RefreshSCHDNo()
        txtSch_No.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    txtSch_No.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Function IsValidItemForSchedule(ByVal obj As clsPurchaseScheduleDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Document_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " RGP No : " + obj.Document_Code + "  Item : " + obj.Item_Name + Environment.NewLine + " UOM : " + obj.Unit_Code + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Function IsValidItemForRGP(ByVal obj As clsRGPDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.RGP_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " RGP No : " + obj.RGP_No + "  Item : " + obj.Item_Desc + Environment.NewLine + " UOM : " + obj.Unit_code + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Function IsValidItemForJOBWORKRGP(ByVal obj As clsRGPBOMItem)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRGPNo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.RGP_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " RGP No : " + obj.RGP_No + "  Item : " + obj.Iname + Environment.NewLine + " UOM : " + obj.Unit_Code + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If

            Dim JobWorkType As String = ""
            If clsCommon.CompairString(cmbRGPType.SelectedValue, "AR") = CompairStringResult.Equal Then
                JobWorkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_JobWork from tspl_rgp_head where rgp_no='" + strReqCode + "'"))
            End If
            If clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal Then
                JobWorkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_BOM from tspl_rgp_head where rgp_no='" + strReqCode + "'"))
            End If
            If clsCommon.CompairString(cmbRGPType.SelectedValue, "AI") = CompairStringResult.Equal Then
                JobWorkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_As_It_Is from tspl_rgp_head where rgp_no='" + strReqCode + "'"))
            End If
            If clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso Is_RGP_After_PO AndAlso clsCommon.CompairString(JobWorkType, "1") <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Document is not of same RGP Type ie. (" + cmbRGPType.SelectedValue + ")")
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub cmbGRNType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbGRNType.SelectedValueChanged
        If isInsideLoadData Then
            Exit Sub
        End If
        If cmbGRNType.DataSource IsNot Nothing AndAlso clsCommon.CompairString(cmbGRNType.SelectedValue, "J") = CompairStringResult.Equal AndAlso Is_RGP_After_PO Then
            cmbRGPType.Enabled = True
            cmbRGPType.SelectedValue = ""
        Else
            cmbRGPType.Enabled = False
            cmbRGPType.SelectedValue = ""
        End If
        If clsCommon.CompairString(cmbGRNType.SelectedValue, "O") = CompairStringResult.Equal Then
            txtSubLocation.Enabled = True
        Else
            txtSubLocation.Enabled = False
            txtSubLocation.Value = ""
            lblShipToLocation.Text = ""
        End If
    End Sub

    Private Sub cmbRGPType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbRGPType.SelectedValueChanged
        If isInsideLoadData Then
            Exit Sub
        End If
        If cmbRGPType.DataSource IsNot Nothing AndAlso clsCommon.CompairString(cmbRGPType.SelectedValue, "AB") = CompairStringResult.Equal AndAlso Is_RGP_After_PO Then
            txtVendorNo.Value = ""
            'txtRgp_no.Enabled = False
        End If
        gv1.Rows.Clear()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmountPer).Value = 100
        txtRgp_no.Value = ""
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
    Private Sub mnuSaveLayout_Click(sender As Object, e As EventArgs) Handles mnuSaveLayout.Click
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

    Private Sub mnuDeleteLayout_Click(sender As Object, e As EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Sub LoadPOReceiveControl(ByVal intRowNo As Integer)
        Dim qry As String = "select TSPL_PURCHASE_ORDER_HEAD.Apply_Receive_Control,TSPL_PURCHASE_ORDER_DETAIL.Amount from TSPL_PURCHASE_ORDER_DETAIL " + Environment.NewLine +
        " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " + Environment.NewLine +
        " where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colPONo).Value) + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.Rows(intRowNo).Cells(colPOControlOnReceive).Value = IIf(clsCommon.myCdbl(dt.Rows(0)("Apply_Receive_Control")) = 1, True, False)
            gv1.Rows(intRowNo).Cells(colPOAmt).Value = clsCommon.myCdbl(dt.Rows(0)("Amount"))
        End If
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                'Dim frm As New FrmPWD(Nothing)
                'frm.strType = "PO Cancel"
                'frm.strCode = "PO Cancel"
                'frm.ShowDialog()
                'If frm.isPasswordCorrect Then
                If common.clsCommon.MyMessageBoxShow("Do you want to cancel the GRN?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    'If clsGRNHead.CheckGRNUsedInSRNorMRN(clsCommon.myCstr(txtDocNo.Value), Nothing) Then
                    '    Throw New Exception("GRN can not be cancelled because it is used in SRN/MRN.")
                    'Else

                    Dim qry1 As String = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        qry1 = "GRN is used in following MRN"
                        For Each dr As DataRow In dt.Rows
                            qry1 += Environment.NewLine + clsCommon.myCstr(dr("MRN_No"))
                        Next
                        qry1 += Environment.NewLine + "Can't cancel it"
                        clsCommon.MyMessageBoxShow(qry1)
                        Exit Sub
                    End If
                    If clsGRNHead.GRNCancel(Me.Form_ID, clsCommon.myCstr(txtDocNo.Value)) Then
                        Dim qry As String = ""
                        qry = "update TSPL_PURCHASE_ORDER_HEAD set close_yn='N' WHERE PurchaseOrder_No= '" + txtReqNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        clsCommon.MyMessageBoxShow("GRN cancelled successfully!", Me.Text)
                    End If
                    'End If
                End If
                'End If
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAmendment_Click(sender As Object, e As EventArgs) Handles btn_Amendment.Click
        Try
            Dim Reason As String = ""
            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                Throw New Exception("Please Select GRN No for update.")
            ElseIf btnPost.Enabled = True Then
                Throw New Exception("This entry is already unposted.")
            End If
            Dim strMRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TOP 1 TSPL_MRN_DETAIL.MRN_No from TSPL_MRN_DETAIL LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No WHERE (Against_GRN='" + clsCommon.myCstr(txtDocNo.Value) + "' OR TSPL_MRN_DETAIL.GRN_Id='" + clsCommon.myCstr(txtDocNo.Value) + "')"))
            If clsCommon.myLen(strMRNNo) = 0 Then
                If clsCancelLog.CheckForReasonOnUpdateAfterPost() Then
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Update"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                SaveData(False, True)
                saveCancelLog(Reason, "GE Update", Nothing)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                If clsCommon.myLen(strMRNNo) > 0 Then
                    Throw New Exception("Gate Entry is Used in MRN No - " & strMRNNo)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Dim strLocations = String.Empty

        If clsCommon.myLen(txtBillToLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Bill To location code before sub location", Me.Text)
            Exit Sub
        End If
        txtSubLocation.Value = clsLocation.getFinder("(Main_Location_Code='" & txtBillToLocation.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtSubLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
        Else
            lblSubLocation.Text = ""
        End If
        strLocations = Nothing
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

                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If (clsCommon.myLen(txtDocNo.Value) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Please select Document to View.", Me.Text)
            Return
        End If

        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try

            ds_attachment = clsAttachDocument.GetGRNQCDocumentByte(txtDocNo.Value)
            If ds_attachment Is Nothing OrElse ds_attachment.Rows.Count <= 0 Then
                Throw New Exception("No QC Video available")
            End If
            filename = "QCFile.mp4"
            Dim blob As Byte() = ds_attachment.Rows(0)("FileData")
            file_path = "C:\ERPTempFolder"
            Dim dir As DirectoryInfo = New DirectoryInfo(file_path)
            If dir.Exists = False Then
                dir.Create()
            End If
            Dim index As Int16 = filename.LastIndexOf(".")
            file_extn = filename.Substring(index)
            filename = filename.Remove(index)
            filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
            filename = filename.Replace(" ", "")
            filename = filename.Replace("/", "_")
            filename = filename.Replace(":", "_")
            Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
            fs.Write(blob, 0, blob.Length)
            fs.Close()
            System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Error in Downloading Documnet.", Me.Text)
        End Try
    End Sub

    Private Function RequiredVisualQC() As Boolean
        Dim ContainVisualQCItem As Boolean = False
        Try
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsItemMaster.IsRequiredItemVisualQC(clsCommon.myCstr(grow.Cells(colICode).Value), Nothing) = True Then
                    ContainVisualQCItem = True
                    Exit For
                End If
            Next

            If ContainVisualQCItem = True Then
                GBVisualQC.Enabled = True
            Else
                GBVisualQC.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ContainVisualQCItem
    End Function

    Private Function RequiredVisualQCSecond() As Boolean
        Dim ContainVisualQCItem As Boolean = False
        Try
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsItemMaster.IsRequiredItemVisualQC(clsCommon.myCstr(grow.Cells(colICode).Value), Nothing) = True Then
                    ContainVisualQCItem = True
                    Exit For
                End If
            Next

            If ContainVisualQCItem = True Then
                GBVisualQCSecond.Enabled = True
            Else
                GBVisualQCSecond.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ContainVisualQCItem
    End Function

    Private Sub btnQCUpdate_Click(sender As Object, e As EventArgs) Handles btnQCUpdate.Click

        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.myLen(cboVisualQCStatus.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Select Visual QC Status", Me.Text)
                    cboVisualQCStatus.Focus()
                    Exit Sub
                End If

                Dim qry1 As String = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry1 = "GRN is used in following MRN"
                    For Each dr As DataRow In dt.Rows
                        qry1 += Environment.NewLine + clsCommon.myCstr(dr("MRN_No"))
                    Next
                    qry1 += Environment.NewLine + "Can't update it"
                    Throw New Exception(qry1)
                End If

                qry1 = "select distinct Weighment_Code from TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                dt = clsDBFuncationality.GetDataTable(qry1)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry1 = "GRN is used in following Weighment"
                    For Each dr As DataRow In dt.Rows
                        qry1 += Environment.NewLine + clsCommon.myCstr(dr("Weighment_Code"))
                    Next
                    qry1 += Environment.NewLine + "Can't update it"
                    Throw New Exception(qry1)
                End If

                clsGRNHead.UpdateVisualQCStatus(txtDocNo.Value, cboVisualQCStatus.SelectedIndex, txtVisualQCRemarks.Text, dtpVisualQCStatus.Value)

                If clsCommon.MyMessageBoxShow(Me, "You are saving data with" + " " + clsCommon.myCstr(cboVisualQCStatus.SelectedItem) + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then

                    Dim trans As SqlTransaction = Nothing
                    Try
                        trans = clsDBFuncationality.GetTransactin()

                        If CInt(cboVisualQCStatus.SelectedIndex) = 2 Then
                            Dim qry As String = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=1 where TSPL_GRN_HEAD.GRN_No='" + txtDocNo.Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Else
                            If UsLock1.Status = ERPTransactionStatus.Cancel Then
                                Dim qry As String = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=0 where TSPL_GRN_HEAD.GRN_No='" + txtDocNo.Value + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                        AddNew()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Select a document to Update Visual QC Status.", Me.Text)
                txtDocNo.Focus()
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnQCUpdateSecond_Click(sender As Object, e As EventArgs) Handles btnQCUpdateSecond.Click

        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.myLen(cboVisualQCStatusSecond.Text) <= 0 Then
                    cboVisualQCStatusSecond.Focus()
                    Throw New Exception("Select Visual QC Status")
                End If
            Else
                txtDocNo.Focus()
                Throw New Exception("Select a document to Update Visual QC Status Second.")

            End If

            ''
            'Dim qry1 As String = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    qry1 = "GRN is used in following MRN"
            '    For Each dr As DataRow In dt.Rows
            '        qry1 += Environment.NewLine + clsCommon.myCstr(dr("MRN_No"))
            '    Next
            '    qry1 += Environment.NewLine + "Can't update it"
            '    Throw New Exception(qry1)
            'End If

            Dim qry1 As String = "select distinct TSPL_NIR_QC.Document_No from TSPL_NIR_QC left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_NIR_QC.MRN_No  where TSPL_MRN_DETAIL.GRN_Id= '" + clsCommon.myCstr(txtDocNo.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry1 = "GRN is used in following NIR QC"
                For Each dr As DataRow In dt.Rows
                    qry1 += Environment.NewLine + clsCommon.myCstr(dr("Document_No"))
                Next
                qry1 += Environment.NewLine + "Can't update it"
                Throw New Exception(qry1)
            End If


            Dim qry2 As String = "select distinct Document_Code from TSPL_QC_CHECK_HEAD where Gate_Entry_No ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                qry2 = "GRN is used in following WAT Qc"
                For Each dr As DataRow In dt.Rows
                    qry2 += Environment.NewLine + clsCommon.myCstr(dr("Document_Code"))
                Next
                qry2 += Environment.NewLine + "Can't update it"
                Throw New Exception(qry2)
            End If


            Dim qry3 As String = "select distinct srn_no from TSPl_SRN_detail where GRN_ID ='" + clsCommon.myCstr(txtDocNo.Value) + "'"
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry3)
            If dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0 Then
                qry3 = "GRN is used in following SRN"
                For Each dr As DataRow In dt.Rows
                    qry3 += Environment.NewLine + clsCommon.myCstr(dr("srn_no"))
                Next
                qry3 += Environment.NewLine + "Can't update it"
                Throw New Exception(qry3)
            End If

            clsGRNHead.UpdateVisualQCStatusSecond(txtDocNo.Value, cboVisualQCStatusSecond.SelectedIndex, txtVisualQCRemarksSecond.Text, dtpVisualQCStatusSecond.Value)
            ''
            If clsCommon.MyMessageBoxShow(Me, "You are saving data with" + " " + clsCommon.myCstr(cboVisualQCStatusSecond.SelectedItem) + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    If CInt(cboVisualQCStatusSecond.SelectedIndex) = 2 Then
                        Dim qry As String = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=1 where TSPL_GRN_HEAD.GRN_No='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        '
                    Else
                        'If CInt(cboVisualQCStatusSecond.SelectedIndex) = 1 Then
                        '    Dim qry As String = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=0 where TSPL_GRN_HEAD.GRN_No='" + txtDocNo.Value + "'"
                        '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        'End If
                        Dim qry As String = "update TSPL_GRN_HEAD SET TSPL_GRN_HEAD.IsCancel=0 where TSPL_GRN_HEAD.GRN_No='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                    '
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                    AddNew()
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try


            End If

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtFinderVendorPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderVendorPrint._MYValidating
        Dim qry As String = "select distinct Vendor_Code,Vendor_Name from TSPL_GRN_HEAD "
        TxtFinderVendorPrint.Value = clsCommon.ShowSelectForm("VnrPrt", qry, "Vendor_Code", "", TxtFinderVendorPrint.Value, "Vendor_Code", isButtonClicked)
        lblVendorPrint.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from tspl_grn_head WHERE Vendor_Code ='" + TxtFinderVendorPrint.Value + "'"))
    End Sub

    Private Sub TxtFinderItemPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderItemPrint._MYValidating
        Dim qry As String = "select DISTINCT TSPL_GRN_DETAIL.item_code,TSPL_GRN_DETAIL.item_desc from TSPL_GRN_DETAIL 
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No 
inner join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code"
        Dim WHRCLS As String = " TSPL_ITEM_MASTER.Structure_Code='RM' "
        TxtFinderItemPrint.Value = clsCommon.ShowSelectForm("itmPrt", qry, "item_code", WHRCLS, TxtFinderVendorPrint.Value, "item_code", isButtonClicked)
        lblItemPrint.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from TSPL_GRN_DETAIL WHERE item_code ='" + TxtFinderItemPrint.Value + "'"))
    End Sub

    Private Sub TxtFinderRalPrint__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinderRalPrint._MYValidating
        Dim qry As String = "select distinct Ref_No,convert(date,tspl_tender_header.DocumentDate,103) as RalDate from TSPL_GRN_HEAD 
inner join tspl_tender_header on tspl_tender_header.DocumentCode=TSPL_GRN_HEAD.Ref_No"
        TxtFinderRalPrint.Value = clsCommon.ShowSelectForm("itmPrt", qry, "Ref_No", "", TxtFinderRalPrint.Value, "Ref_No", isButtonClicked)
        lblRalPrint.Text = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select DocumentDate from tspl_tender_header WHERE DocumentCode ='" + TxtFinderRalPrint.Value + "'"))
    End Sub
    Private Sub rmiEnglish_Click(sender As Object, e As EventArgs) Handles rmiEnglish.Click
        PrintRejected(rmiEnglish.Text)
    End Sub

    Private Sub rmiHindi_Click(sender As Object, e As EventArgs) Handles rmiHindi.Click
        PrintRejected(rmiHindi.Text)
    End Sub

    Private Sub PrintRejected(strBtnText As String)
        Try
            Dim StrWhere As String = ""
            If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage1.Name) = CompairStringResult.Equal Then
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Document number not found", Me.Text)
                    txtDocNo.Focus()
                    Exit Sub
                End If
                StrWhere += " AND TSPL_GRN_head.GRN_No = '" + txtDocNo.Value + "'"
            ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage7.Name) = CompairStringResult.Equal Then
                If fromDate.Value > ToDate.Value Then
                    common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                    fromDate.Focus()
                    Exit Sub
                End If

                If clsCommon.myLen(TxtFinderVendorPrint.Value) > 0 Then
                    StrWhere += " and TSPL_GRN_head.Vendor_Code = '" + TxtFinderVendorPrint.Value + "'"

                ElseIf clsCommon.myLen(TxtFinderItemPrint.Value) > 0 Then
                    StrWhere += " and TSPL_GRN_DETAIL.Item_Code = '" + TxtFinderItemPrint.Value + "'"

                ElseIf clsCommon.myLen(TxtFinderRalPrint.Value) > 0 Then
                    StrWhere += " and TSPL_GRN_head.Ref_No = '" + TxtFinderRalPrint.Value + "'"

                End If
                StrWhere += " 
                            and convert(date,tspl_grn_head.GRN_Date,103)>= convert(date,('" & fromDate.Value & "'),103) and convert(date,tspl_grn_head.GRN_Date,103)<= convert(date,('" & ToDate.Value & "'),103)"
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    StrWhere += " and tspl_grn_head.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " Select TSPL_COMPANY_MASTER.Comp_Code   , TSPL_COMPANY_MASTER.Comp_Name , TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2 , TSPL_COMPANY_MASTER.Add3 as Comp_Add3 , TSPL_COMPANY_MASTER.City_Code as Comp_City_Code, TSPL_COMPANY_MASTER.Email as Comp_Email , TSPL_COMPANY_MASTER.Pincode as Comp_Pincode , TSPL_COMPANY_MASTER.State as Comp_State , TSPL_COMPANY_MASTER.Tin_No as Comp_Tin_No , TSPL_COMPANY_MASTER.Logo_Img as Comp_Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 as Comp_Logo_Img2, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTReg_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNo, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1 , TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2, TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant,  TSPL_LOCATION_MASTER.Location_Desc,tspl_grn_head.Ref_No,tspl_grn_head.GRN_Date,TSPL_GRN_head.Vendor_Code,TSPL_GRN_head.Vendor_Name,TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.Item_Desc,tspl_grn_head.VisualQCUpdatedDate,tspl_grn_head.VisualQCStatus,tspl_grn_head.VisualQCUpdatedDateSecond,tspl_grn_head.VisualQCStatusSecond,tspl_grn_head.VisualQCRemarks,tspl_grn_head.VisualQCRemarksSecond, 
                                    tspl_grn_head.VehicleNo,tspl_grn_head.LR_No,
                                    isnull(case when tspl_grn_head.VisualQCStatus=2  then tspl_grn_head.VisualQCUpdatedDate end,
                                    case when tspl_grn_head.VisualQCStatusSecond=2 then tspl_grn_head.VisualQCUpdatedDateSecond end) as QCDate,
                                    isnull(case when tspl_grn_head.VisualQCStatus=2 then tspl_grn_head.VisualQCRemarks end,case when tspl_grn_head.VisualQCStatusSecond=2 then tspl_grn_head.VisualQCRemarksSecond end) as Remarks
 
  

                                from tspl_grn_head 
                                left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=tspl_grn_head.GRN_No
                                inner join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
                                left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ITEM_MASTER.Comp_code
								left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_COMPANY_MASTER.Comp_Code
                                where tspl_grn_head.VisualQCStatus=2 or tspl_grn_head.VisualQCStatusSecond=2" + StrWhere
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Exit Sub
            Else
                If clsCommon.CompairString(strBtnText, "English") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCRALWiseRMReportEnglish", "Rejected Report")
                ElseIf clsCommon.CompairString(strBtnText, "Hindi") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptQCAnalysisReportRejectionHindi", "Rejected Report")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub GBVisualQCSecond_Enter(sender As Object, e As EventArgs) Handles GBVisualQCSecond.Enter

    End Sub

    Private Sub btnhistory_Click(sender As Object, e As EventArgs) Handles btnhistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "GRN_No", "TSPL_GRN_HEAD", "TSPL_GRN_DETAIL")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
