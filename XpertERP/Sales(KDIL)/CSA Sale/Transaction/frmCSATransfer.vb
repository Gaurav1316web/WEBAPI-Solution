''create by panch raj  on 12/09/2014
'' updation by richa agarwal against ticket no BM00000005230
'============BM00000004333========================
'=============update by preeti gupta against ticket no[BM00000008749]-=================
Imports common
Imports System
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO


Public Class frmCSATransfer
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ForUDLOnly As Boolean = False
    Dim AllowRate_Readonly As Boolean = False
    Dim OpenALLTaxes As Boolean = False
    Dim DisableCommissionColumn As Boolean = False
    Dim GrossWtfromItemMaster As Boolean = False
    Dim DifferentSeriesINExciseCase As Boolean = False

    Dim Weight_MT_Unit As String = Nothing
    Dim Gross_Weight_Unit As String = Nothing
    Dim isbtnDOClick As Boolean = False
    Public InsideUpdateCurrentRow As Boolean = False
    Dim arrLoc As String = ""
    Dim vaddnew As String = "Y"
    Dim attachqry As String = ""
    Private StrSql As String
    Public StrDocNo As String
    Public strExcise As Boolean
    Dim ErrorControl As New clsErrorControl()
    Dim ShowDocumentCancel As Boolean = Nothing
    Dim Item_TaxType As Integer = 0
    Private blnBackCalculation As Boolean = False
    Private ItemRateEditable As Boolean = False
    Private ItemMRPEditable As Boolean = False
    Private intApprovel_Required As Integer = 0
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colAltUnitCOde As String = "AltUnit"
    Const colSchmCode As String = "SchmCode"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"
    Const colFOC As String = "FOC"
    Const colIsSchmItem As String = "SchmItem"
    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    Const colCSA_Commission_RS_PERS As String = "CSA_Commission_RS_PERS"
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNCode As String = "HSNCode"
    Const colIUnitWt As String = "IUnitWt"
    Const colINetWt As String = "INetWt"
    Const colINetMTWt As String = "INetMTWt"
    Const colCSAType As String = "colCSAType"
    Const colQty As String = "COLQTY"

    Const colIsBatchItem As String = "colIsBatchItem"

    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colTaxBasis As String = "colTaxBasis"
    Const colTaxCalcType As String = "colTaxCalcType"
    'Const colAmt As String = "COLAMT"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colTotalBasicAmount As String = "totalBasicAmount"

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

    Const colItem_Pack_Size As String = "colItem_Pack_Size"
    Const colMaster_Pack_Size As String = "colMaster_Pack_Size"
    Const colCommision_Rate As String = "colCommision_Rate"
    Const colCommisionCharges As String = "colCommisionCharges"
    Const colOther_Chrage As String = "colOther_Chrage"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"

    Const colDOQty As String = "DOQTY"
    Const colDOBalanceQty As String = "DOBALQTY"
    Const colDOCode As String = "DOCode"
    Const ColActualBalQty As String = "ColActualBalQty"
    Const colTransferRate As String = "colTransferRate"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"

    Const colTTaxRate As String = "TAXRATE"

    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"
    Const colMRP As String = "MRP"
    Const colIsMRPMandatory As String = "IsMRPMandatory"
    Const colAbatementPers As String = "AbatementPers"
    Const colAbatementAmt As String = "AbatementAmt"
    Private isALlowVehicleGateOutValidation As Boolean = False

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn


#End Region


    Public Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSATransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        BtnPrintChallan.Visible = MyBase.isPrintFlag
        btnPrintMandi.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub
    Public csaTransferFinderValue As String = Nothing
    Public Sub OpenForDrillDown()
        Try

            If csaTransferFinderValue IsNot Nothing AndAlso csaTransferFinderValue.Length > 0 Then
                LoadData(csaTransferFinderValue, NavigatorType.Current)
            Else
                If clsCommon.myLen(Me.Tag) > 0 Then
                    csaTransferFinderValue = Me.Tag.ToString()
                    LoadData(csaTransferFinderValue, NavigatorType.Current)
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'btnReverse.Enabled = False
            btnHistory.Enabled = False
            btnupdate.Visible = True

            'SendSMSandEmail()
            'SetMailRight()
            SetUserMgmtNew()

            txtCustCode.MendatroryField = True

            Weight_MT_Unit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.VehicleCapacityUnit + "' and type='" + clsFixedParameterType.VehicleCapacityUnit + "'"))
            Gross_Weight_Unit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_FIXED_PARAMETER where code='" + clsFixedParameterCode.GrossWeightUnit + "' and type='" + clsFixedParameterType.GrossWeightUnit + "'"))
            OpenALLTaxes = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CSATransfer_SalePatti_All_Tax_Open, clsFixedParameterCode.CSATransfer_SalePatti_All_Tax_Open, Nothing)) = "1", True, False))
            AllowRate_Readonly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoReadonly_UnitRate_AtCSASale, clsFixedParameterCode.DoReadonly_UnitRate_AtCSASale, Nothing)) = 1, True, False)
            GrossWtfromItemMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GrossWtFromItemMasterONCSATransfer, clsFixedParameterCode.GrossWtFromItemMasterONCSATransfer, Nothing)) = 1, True, False)
            ForUDLOnly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)) = 1, True, False)
            ForUDLOnly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)) = 1, True, False)

            txtGross_Wt.ReadOnly = GrossWtfromItemMaster

            MyLabel7.Visible = Not ForUDLOnly
            txtCSARate.Visible = Not ForUDLOnly
            MyLabel24.Visible = ForUDLOnly
            txtSecondary_Doc_Code.Visible = ForUDLOnly

            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing), "1") = CompairStringResult.Equal Then
                cmbTax.SelectedValue = "No"
                cmbTax.Visible = False
                MyLabel9.Visible = False
            End If


            DisableCommissionColumn = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDisabledCommissionOnCSATransfer, clsFixedParameterCode.AllowDisabledCommissionOnCSATransfer, Nothing)) = "1", True, False))

            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

            RadPageView1.SelectedPage = RadPageViewPage1
            ShowDocumentCancel = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CSADocumentCancel, clsFixedParameterCode.CSADocumentCancel, Nothing)) = 1, True, False)
            LoadType()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadItemType()
            LoadBlankGridAC()
            AddNew()
            SetLength()
            ''For Custom Fields
            RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed

            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.Report_ID = MyBase.Form_ID
                UcCustomFields1.LoadCustomControls()
            End If
            ''End of For Custom Fields

            ' '' MultiCurrency
            'SetMultiCurrencyVisibility()
            ' '' End of MultiCurrency
            'intMRPwithabatement = clsDBFuncationality.getSingleValue("select IsMRPwithAbatement from TSPL_INV_PARAMETERS")
            'IsBatchMFDEXDmandatory = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsBatchNo_MFD_EXD_Mandatory from TSPL_inv_parameters")) = 0, False, True)
            'blnBackCalculation = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsRateBackCalculation from TSPL_inv_parameters")) = 0, False, True)
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
            Me.cmbTax.DataSource = clsCSABooking.GetTaxPaid()
            Me.cmbTax.ValueMember = "Code"
            Me.cmbTax.DisplayMember = "Name"



            If clsCommon.myLen(StrDocNo) > 0 Then
                LoadData(StrDocNo, NavigatorType.Current)
            End If
            isALlowVehicleGateOutValidation = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowVehicleGateOutValidationCSATransfer, clsFixedParameterCode.AllowVehicleGateOutValidationCSATransfer, Nothing)) = "1", True, False)
            ''richa agarwal 29 nov ,2016
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)), "1") = CompairStringResult.Equal Then
                RadSplitButton1.Visible = False
            Else
                RadSplitButton1.Visible = False
            End If
            btnPrint.Visible = False
            ''---------------------
            OpenForDrillDown()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            Dim CurrntCPDType As String = clsCommon.myCstr(gv1.Rows(XR).Cells(colCSAType).Value) 'CPD-DESI GHEE
            Dim CSA_State As String = clsCSAPriceMaster.GetCSAState(txtCustCode.Value)

            If CellChanged Then
                'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(XR).Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                '    gv1.Rows(XR).Cells(colRate).Value = 0
                '    Exit Sub
                'End If
                uom = clsCommon.myCstr(gv1.Rows(XR).Cells(colUnit).Value)
                qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                '========(unit price=chart rate*base unit conversion of chart/calc.unit conversion)------------

                qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colTaxBasis).Value) + "' and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtFromLocation.Value + "') "
                If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCSAPriceMasterPostedData, clsFixedParameterCode.AllowCSAPriceMasterPostedData, Nothing)) = 1, True, False) = True Then
                    qry += " and Tspl_CSA_Price_Head.Posted='1' "
                End If

                ''============when setting ON and item is not CPD then other price chat apply
                If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                    qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtCustCode.Value) + "' "
                Else
                    qry += " and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colCSAType).Value) + "' and tspl_csa_price_state_detail.state_code='" + CSA_State + "' "
                End If

                If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True Then ''if setting is ON then expiry check in all cases
                    qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "' "
                End If
                ''end here=============================
                qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "

                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                    diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                    MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                    Dim csaconversion As Decimal = 0

                    If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                        orgrate = clsCommon.myCdbl(diffrate)
                    Else
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colICode).Value) + "' and uom_code='" + csauom + "'"))


                        If ForUDLOnly Then
                            txtCSARate.Text = clsCommon.myCdbl(dt.Rows(0)("csa_rate"))
                        End If

                        orgrate = (clsCommon.myCdbl(txtCSARate.Text) + clsCommon.myCdbl(diffrate))
                    End If
                    If csaconversion <= 0 Then
                        csaconversion = 1
                    End If


                    orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                    'gv1.Rows(XR).Cells(colUnitRate).Value = orgrate
                    gv1.Rows(XR).Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)

                Else
                    'If clsCommon.myCdbl(gv1.Rows(XR).Cells(colUnitRate).Value) <= 0 Then
                    '    gv1.Rows(XR).Cells(colUnitRate).Value = 0
                    'End If
                End If

            Else

                For Each grow As GridViewRowInfo In gv1.Rows
                    'If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                    '    grow.Cells(colRate).Value = 0
                    '    Continue For
                    'End If

                    uom = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and uom_code='" + uom + "'"
                    cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If

                    If clsCommon.myLen(uom) <= 0 Then
                        Continue For
                    End If

                    qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.csa_rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                    qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                    qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(grow.Cells(colTaxBasis).Value) + "'"
                    qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtFromLocation.Value + "')"
                    If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowCSAPriceMasterPostedData, clsFixedParameterCode.AllowCSAPriceMasterPostedData, Nothing)) = "1", True, False)) = True Then
                        qry += " and Tspl_CSA_Price_Head.Posted='1' "
                    End If

                    ''============when setting ON and item is not CPD then other price chat apply
                    If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtCustCode.Value) + "' "
                    Else
                        qry += " and tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv1.Rows(XR).Cells(colCSAType).Value) + "' "
                    End If

                    If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True Then ''if setting is ON then expiry check in all cases
                        qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "' "
                    End If
                    ''===============end here============================================
                    qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "

                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                        diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                        MRP = clsCommon.myCdbl(dt.Rows(0)("MRP"))

                        Dim csaconversion As Decimal = 0

                        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = "1", True, False)) = True AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                            orgrate = clsCommon.myCdbl(diffrate)
                        Else
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and uom_code='" + csauom + "'"))

                            If ForUDLOnly Then
                                txtCSARate.Text = clsCommon.myCdbl(dt.Rows(0)("csa_rate"))
                            End If

                            orgrate = (clsCommon.myCdbl(txtCSARate.Text) + clsCommon.myCdbl(diffrate))
                        End If

                        If csaconversion <= 0 Then
                            csaconversion = 1
                        End If

                        orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                        'grow.Cells(colUnitRate).Value = orgrate
                        grow.Cells(colMRP).Value = System.Math.Round((MRP * cnvrsn) / csaconversion, 2)
                    Else
                        'If clsCommon.myCdbl(grow.Cells(colUnitRate).Value) <= 0 Then
                        '    grow.Cells(colUnitRate).Value = 0
                        'End If
                    End If
                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
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

            If clsCommon.myLen(Me.txtCustCode.Value) > 0 Then
                strq = "select currency_code from TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(Me.txtCustCode.Value) & "'"
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
        If clsCommon.myLen(clsCommon.myCstr(Me.txtCustCode.Value)) = 0 Then
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

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200

        txtComment.MaxLength = 200


        cboItemType.MaxLength = 1


    End Sub

    Sub LoadType()
        cmbEXType.DataSource = Nothing
        cmbEXType.DataSource = clsDBFuncationality.GetDataTable("select '' as Code,'None' as Name union all select 'N' as Code,'Non-Excisable' as Name union all select 'E' as Code,'Excisable' as Name")
        cmbEXType.DisplayMember = "Name"
        cmbEXType.ValueMember = "Code"
    End Sub

    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 1
        'cboItemType.Visible = True
        'RadLabel21.Visible = True
    End Sub

    Sub BlankAllControls()
        cmbEXType.SelectedValue = ""
        cmbEXType.Enabled = True
        txtship_to_loc_code.Value = ""
        txtship_to_loc_name.Text = ""
        txtDate.Enabled = True
        txtExcisable.Text = ""
        txtvehicle_Charge.Text = Nothing
        txtvehicle_Charge.Tag = Nothing
        txtVehicle_Capacity.Text = Nothing
        txttotal_Wt.Text = ""
        txtGross_Wt.Text = Nothing
        txtGR_No.Text = ""
        dtpGR.Text = clsCommon.GETSERVERDATE(Nothing)
        txtRemovalDate.Value = clsCommon.GETSERVERDATE()
        txtRemovalDate.Checked = False
        txtCSARate.Text = Nothing
        lblTotalAmount.Text = Nothing
        lblCommissionCharges.Text = Nothing
        chk_F_Form.Checked = True

        txtvehicle_code.Text = ""
        txtTransporter_Code.Value = ""
        txtTransporter_desc.Text = ""
        txtWayBill_No.Text = ""
        ttxway_bill_date.Text = clsCommon.GETSERVERDATE(Nothing)
        TxtEWayBillNo.Text = ""
        txtElectronicRefNo.Text = ""
        TxtEWayBillDate.Value = clsCommon.GETSERVERDATE(Nothing)
        btnupdate.Enabled = False
        txtDiscPer.Text = 0
        txtDiscAmt.Value = 0
        lblInvoiceDiscAmt.Text = ""
        vaddnew = "N"

        vaddnew = "Y"

        txtDocNo.Value = ""
        txtDesc.Text = ""

        txtCustCode.Value = ""
        txtCustDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromLocation.Value = ""
        txtFromLocationDesc.Text = ""
        txtToLocation.Value = ""
        txtToLocationDesc.Text = ""

        txtDesc.Text = ""
        txtDesc.Text = clsCSATransfer.GetTransferDescrptn()

        fndDONo.Value = Nothing
        txtComment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value

        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTotalTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        txtDocumentTotal.Text = ""

        UsLock1.Status = ERPTransactionStatus.Pending

        txtDept.Value = ""
        lblDept.Text = ""
        cboItemType.SelectedIndex = 2

        cboItemType.Enabled = True

        lblAmbendmentNoCaption.Visible = False
        lblAbandonmentNo.Text = ""
        btnAmendment.Enabled = False

        lblTotalOtherCharges.Text = ""
        lblTotalOtherCharges.Text = ""

        txtFromLocation.Enabled = False
        txtToLocation.Enabled = False
        txtCustCode.Enabled = False
        txtState.Enabled = False
        txtCSARate.Enabled = False

        MyLabel24.Visible = False
        txtSecondary_Doc_Code.Visible = False
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        chkownvehicle.Checked = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewDecimalColumn()
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
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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


        Dim HSNCode As New GridViewTextBoxColumn()
        HSNCode.FormatString = ""
        HSNCode.HeaderText = "HSN Code"
        HSNCode.Name = colHSNCode
        HSNCode.Width = 100
        HSNCode.ReadOnly = True
        HSNCode.WrapText = True
        HSNCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(HSNCode)

        Dim repoCSAType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCSAType.FormatString = ""
        repoCSAType.HeaderText = "CSA Type"
        repoCSAType.Name = colCSAType
        repoCSAType.Width = 150
        repoCSAType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCSAType)

        Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Balance Quantity"
        repoActualBalQty.Name = ColActualBalQty
        repoActualBalQty.Width = 80
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        'repoActualBalQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoActualBalQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        repoUnit = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Alt. UOM"
        repoUnit.Name = colAltUnitCOde
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        '=====================================
        Dim repounitwt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repounitwt.FormatString = ""
        repounitwt.HeaderText = "Unit Weight"
        repounitwt.Name = colIUnitWt
        repounitwt.Width = 80
        repounitwt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repounitwt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repounitwt)

        Dim reponetwt As GridViewDecimalColumn = New GridViewDecimalColumn()
        reponetwt.FormatString = ""
        reponetwt.HeaderText = "Net Weight"
        reponetwt.Name = colINetWt
        reponetwt.Width = 80
        reponetwt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        reponetwt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reponetwt)

        Dim repoMTwt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMTwt.FormatString = ""
        repoMTwt.HeaderText = "Net MT Weight"
        repoMTwt.Name = colINetMTWt
        repoMTwt.Width = 80
        repoMTwt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMTwt.ReadOnly = True
        repoMTwt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMTwt)
        '--------------------------------------------------------------

        Dim repoInclTax As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoInclTax.Name = colTaxBasis
        repoInclTax.FormatString = ""
        repoInclTax.Width = 80
        repoInclTax.HeaderText = "Including Tax"
        repoInclTax.DataSource = clsCSABooking.GetTaxPaid()
        repoInclTax.DisplayMember = "Name"
        repoInclTax.ValueMember = "Code"
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing), "1") = CompairStringResult.Equal Then
            repoInclTax.IsVisible = False
            repoInclTax.VisibleInColumnChooser = False
        Else
            repoInclTax.IsVisible = True
            repoInclTax.VisibleInColumnChooser = True
        End If
        gv1.MasterTemplate.Columns.Add(repoInclTax)

        Dim repoTaxCalcType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoTaxCalcType.Name = colTaxCalcType
        repoTaxCalcType.FormatString = ""
        repoTaxCalcType.Width = 80
        repoTaxCalcType.HeaderText = "Tax Calculation Type"
        repoTaxCalcType.DataSource = clsCSABooking.GetTaxCalcType
        repoTaxCalcType.DisplayMember = "Name"
        repoTaxCalcType.ValueMember = "Code"
        repoTaxCalcType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTaxCalcType)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        '================================================
        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Pending Qty"
        repoConv.Name = colDOBalanceQty
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = True
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)

        Dim repoConv1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoConv1.FormatString = ""
        repoConv1.HeaderText = "DO Code"
        repoConv1.Name = colDOCode
        repoConv1.Width = 100
        repoConv1.ReadOnly = True
        repoConv1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv1)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = True
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "DO Qty"
        repoMRP.Name = colDOQty
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.IsVisible = False
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)
        '======================================================================

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Price"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.ReadOnly = AllowRate_Readonly
        gv1.MasterTemplate.Columns.Add(repoRate)

        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "MRP"
        repoRate.Name = colMRP
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = ForUDLOnly
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colIsMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Abatement%"
        repoRate.Name = colAbatementPers
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Abatement Amount"
        repoRate.Name = colAbatementAmt
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Scheme Columns>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Dim repoIsSchmItem As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem.FormatString = ""
        repoIsSchmItem.HeaderText = "Scheme Applicable(Y/N)"
        repoIsSchmItem.Name = colIsSchmItem
        repoIsSchmItem.Width = 50
        repoIsSchmItem.DataSource = clsDBFuncationality.GetDataTable("select 'Y' as Code,'Y' as Name union all select 'N' as Code,'N' as Name")
        repoIsSchmItem.DisplayMember = "Name"
        repoIsSchmItem.ValueMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem)

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

        Dim repoIsSchmItem1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1.FormatString = ""
        repoIsSchmItem1.HeaderText = "Scheme Code"
        repoIsSchmItem1.Name = colSchmCode
        repoIsSchmItem1.Width = 50
        repoIsSchmItem1.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem1)

        Dim repoIsSchmItem2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem2.FormatString = ""
        repoIsSchmItem2.HeaderText = "Is FOC"
        repoIsSchmItem2.Name = colFOC
        repoIsSchmItem2.Width = 50
        repoIsSchmItem2.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsSchmItem2)

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
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
        repoTotalBasicAmt.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(repoTotalBasicAmt)


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
        repoAmtAfterTax.HeaderText = "Incl/Excl Tax Amount"
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

        Dim repoItemPackSize As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemPackSize.AllowSort = False
        repoItemPackSize.HeaderText = "Item Pack Size"
        repoItemPackSize.Name = colItem_Pack_Size
        repoItemPackSize.ReadOnly = True
        repoItemPackSize.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemPackSize.Width = 100
        gv1.MasterTemplate.Columns.Add(repoItemPackSize)

        Dim repoMasterPackSize As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMasterPackSize.AllowSort = False
        repoMasterPackSize.HeaderText = "Master Pack Size"
        repoMasterPackSize.Name = colMaster_Pack_Size
        repoMasterPackSize.ReadOnly = True
        repoMasterPackSize.IsVisible = False
        repoMasterPackSize.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMasterPackSize.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMasterPackSize)

        Dim repoCommisionRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommisionRate.AllowSort = False
        repoCommisionRate.HeaderText = "Commission Rate"
        repoCommisionRate.Name = colCommision_Rate
        repoCommisionRate.ReadOnly = False
        repoCommisionRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommisionRate.Width = 100
        repoCommisionRate.DecimalPlaces = 2
        repoCommisionRate.VisibleInColumnChooser = Not DisableCommissionColumn
        repoCommisionRate.IsVisible = Not DisableCommissionColumn
        gv1.MasterTemplate.Columns.Add(repoCommisionRate)

        Dim repoCommisionRatePer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCommisionRatePer.HeaderText = "Commission Type"
        repoCommisionRatePer.Name = colCSA_Commission_RS_PERS
        repoCommisionRatePer.ReadOnly = True
        repoCommisionRatePer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommisionRatePer.Width = 100
        repoCommisionRatePer.IsVisible = Not DisableCommissionColumn
        repoCommisionRatePer.VisibleInColumnChooser = Not DisableCommissionColumn
        gv1.MasterTemplate.Columns.Add(repoCommisionRatePer)

        Dim repoCommCharges As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommCharges.AllowSort = False
        repoCommCharges.HeaderText = "Commission Charges"
        repoCommCharges.Name = colCommisionCharges
        repoCommCharges.ReadOnly = True
        repoCommCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommCharges.Width = 100
        repoCommCharges.DecimalPlaces = 2
        repoCommCharges.VisibleInColumnChooser = Not DisableCommissionColumn
        repoCommCharges.IsVisible = Not DisableCommissionColumn
        gv1.MasterTemplate.Columns.Add(repoCommCharges)

        Dim repoOtherCharges As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOtherCharges.AllowSort = False
        repoOtherCharges.HeaderText = "Other Charges"
        repoOtherCharges.Name = colOther_Chrage
        repoOtherCharges.ReadOnly = False
        repoOtherCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOtherCharges.Width = 100
        repoOtherCharges.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(repoOtherCharges)

        Dim repoTransferRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTransferRate.AllowSort = False
        repoTransferRate.HeaderText = "Transfer Rate"
        repoTransferRate.Name = colTransferRate
        repoTransferRate.ReadOnly = True
        repoTransferRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTransferRate.Width = 100
        repoTransferRate.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(repoTransferRate)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsBatchItem)

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

    Sub OpenBatchItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = clsCommon.myCstr(txtFromLocation.Value)
            frm.strCurrDocNo = clsCommon.myCstr(txtDocNo.Value)
            frm.strCurrDocType = "SD-CSATRANS"
            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)


            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
            End If
        End If
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

    Private Sub FillFreeItemsInGrid()
        Dim Index As Integer = gv1.CurrentRow.Index
        Try
            If isbtnDOClick Then
                Exit Sub
            End If


            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then

                For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(ii)
                    End If
                Next


                gv1.Rows(Index).Cells(colSchmCode).Value = Nothing
                gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv1.Rows(Index).Cells(colCash_Amt).Value = Nothing
                gv1.Rows(Index).Cells(colCash_Pers).Value = Nothing
                gv1.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                gv1.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                gv1.Rows(Index).Cells(colMainIcode).Value = Nothing
                gv1.Rows(Index).Cells(colMainIQty).Value = Nothing
                gv1.Rows(Index).Cells(colMainIUOM).Value = Nothing
                gv1.Rows(Index).Cells(colFOC).Value = "N"
                gv1.Rows(Index).Cells(colIsSchmItem).Value = "N"

                RefreshSerialNo()
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colIsSchmItem).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colFOC).Value), "N") = CompairStringResult.Equal Then
                    For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value)) = CompairStringResult.Equal Then
                            gv1.Rows.RemoveAt(ii)
                        End If
                    Next

                    '-------------fill cash scheme---------------------------------------
                    Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(Index).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(Index).Cells(colQty).Value), txtCustCode.Value, Nothing, clsCommon.myCDate(txtDate.Text))
                    If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then

                        gv1.Rows(Index).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                        gv1.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                        gv1.Rows(Index).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                        If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                            gv1.Rows(Index).Cells(colCashSchemeType).Value = "P"
                            gv1.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                        ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) > 0 AndAlso clsCommon.myCdbl(obj_Cash.Cash_Pers) <= 0 Then
                            gv1.Rows(Index).Cells(colCashSchemeType).Value = "A"
                            gv1.Rows(Index).Cells(colCash_Pers).Value = (clsCommon.myCdbl(obj_Cash.Cash_Amt) * 100) / (clsCommon.myCdbl(gv1.Rows(Index).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(Index).Cells(colRate).Value))
                            gv1.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                        End If
                        gv1.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    Else
                        gv1.Rows(Index).Cells(colCash_Amt).Value = Nothing
                        gv1.Rows(Index).Cells(colCash_Pers).Value = Nothing
                        gv1.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                        gv1.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                        gv1.Rows(Index).Cells(colIsSchmItem).Value = "N"

                        '==========if cash scheme is there but no quantitive or volumn then also scheme item set to Y
                        If clsCommon.myLen(gv1.Rows(Index).Cells(colMainIcode).Value) > 0 Then
                            gv1.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        End If
                    End If
                    '------------------------------------------------------------------

                    Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(Index).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(Index).Cells(colQty).Value), txtCustCode.Value, clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), Nothing, clsCommon.myCDate(txtDate.Text))
                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                            '--------------update free itemcode in main item row------------------
                            gv1.Rows(Index).Cells(colSchmCode).Value = Nothing
                            gv1.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(Index).Cells(colMainIcode).Value = objtr.Schm_Icode
                            gv1.Rows(Index).Cells(colMainIQty).Value = objtr.Schm_Qty
                            gv1.Rows(Index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                            gv1.Rows(Index).Cells(colFOC).Value = "N"
                            gv1.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                            '-------------------------------------------------------------

                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(gv1.Rows.Count)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCSAType).Value = objtr.Schm_Item_CSA_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Schm_Item_Uom
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAltUnitCOde).Value = objtr.Schm_Item_Uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Schm_IUnit_Rate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Schm_Icode)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0), 2)

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

                            If clsCommon.myLen(Weight_UOM) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Weight UOM not defined for Item  " & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "")
                                Exit Sub
                            End If

                            Dim CommRate As Decimal = 0
                            Dim CommRateUOM As String = ""
                            Dim CommRateType As String = ""

                            If Not DisableCommissionColumn Then ''when not diabled then commission cal. done
                                Dim objLoc As clsLocation = clsLocation.GetData(txtToLocation.Value)
                                If Not objLoc Is Nothing Then
                                    CommRate = clsCommon.myCdbl(objLoc.csa_commision_rate)
                                    CommRateUOM = clsCommon.myCstr(objLoc.csa_commision_type)
                                    CommRateType = clsCommon.myCstr(objLoc.CSA_Commission_RS_PERS)
                                End If
                                If clsCommon.myLen(CommRateUOM) <= 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Commission Rate UOM not defined for location " & txtToLocation.Value & "")
                                    Exit Sub
                                End If
                            End If


                            Dim convFacter As Double = clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value, Nothing)
                            SKU_VALUE = SKU_VALUE * convFacter
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Pack_Size).Value = SKU_VALUE
                            qry = "select top 1 CF from (select (case when (Container_UOM='" & CommRateUOM & "' and Contained_UOM='" & Weight_UOM & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & CommRateUOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type  from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"

                            Dim Master_Sku As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If Master_Sku = 0 Then
                                Master_Sku = 1
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMaster_Pack_Size).Value = Master_Sku
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCSA_Commission_RS_PERS).Value = CommRateType
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Value = "0"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).Value = "0"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).Value = "0"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Tag = CommRateUOM

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIUnitWt).Value = SKU_VALUE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colINetWt).Value = SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty)
                            qry = "select top 1 CF from (select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                            Dim wt_uom_cnvrsn As Decimal = 1 ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_UOM, Nothing)
                            Dim mt_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_MT_Unit, Nothing)

                            If clsCommon.CompairString(Weight_UOM, Weight_MT_Unit) = CompairStringResult.Equal Then
                                mt_uom_cnvrsn = 1
                            End If

                            qry = "select top 1 CF from (select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Gross_Weight_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Gross_Weight_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                            Dim gross_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.CompairString(Weight_UOM, Gross_Weight_Unit) = CompairStringResult.Equal Then
                                gross_uom_cnvrsn = 1
                            End If

                            If gross_uom_cnvrsn > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty) * wt_uom_cnvrsn) * gross_uom_cnvrsn, 2) ' mt_uom_convrsn replaced by gross_uom_cnvrsn done by stuti on 04/01/2017
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = 0
                                If clsCommon.CompairString(Gross_Weight_Unit, Weight_UOM) = CompairStringResult.Equal Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty)), 2)
                                End If
                            End If


                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCode).Value = objtr.Schm_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = clsCommon.myCstr(gv1.Rows(Index).Cells(colICode).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = clsCommon.myCdbl(gv1.Rows(Index).Cells(colQty).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = clsCommon.myCstr(gv1.Rows(Index).Cells(colUnit).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFOC).Value = "Y"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Schm_Icode)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = clsCommon.myCstr(gv1.Rows(Index).Cells(colTaxBasis).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = clsCommon.myCstr(gv1.Rows(Index).Cells(colTaxCalcType).Value)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).ReadOnly = True

                            gv1.Rows.Move(gv1.Rows.Count - 1, Index + 1)
                        Next
                    Else
                        gv1.Rows(Index).Cells(colSchmCode).Value = Nothing
                        gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIcode).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIQty).Value = Nothing
                        gv1.Rows(Index).Cells(colMainIUOM).Value = Nothing
                        gv1.Rows(Index).Cells(colFOC).Value = "N"
                        gv1.Rows(Index).Cells(colIsSchmItem).Value = "N"
                        '==========if cash scheme is there but no quantitive or volumn then also scheme item set to Y
                        If clsCommon.myLen(gv1.Rows(Index).Cells(colCashSchemeCode).Value) > 0 Then
                            gv1.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        gv1.CurrentRow = gv1.Rows(Index)
        RefreshSerialNo()
    End Sub

    Private Sub TotalGrossWt_FromItemMaster()
        Try
            Dim itemCode As String = Nothing
            Dim Qty As Double = Nothing
            Dim Unit As String = Nothing
            Dim unit_gross_wt As Double = Nothing
            Dim wt_uom As String = Nothing
            Dim qry As String = Nothing
            Dim MT_CF As Double = Nothing
            txtGross_Wt.Text = 0
            Dim unit_Net_wt As Double = 0

            For Each grow As GridViewRowInfo In gv1.Rows
                itemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                Unit = clsCommon.myCstr(grow.Cells(colUnit).Value)

                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select gross_weight,Net_Weight from tspl_item_uom_detail where item_code='" + itemCode + "' and uom_code='" + Unit + "'")
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dtTemp.Rows(0)("gross_weight")) <= 0 Then
                        Throw New Exception("Please set gross weight for item:" + itemCode + " and UOM:" + Unit)
                    End If
                    If clsCommon.myCdbl(dtTemp.Rows(0)("Net_Weight")) <= 0 Then
                        Throw New Exception("Please set net weight for item:" + itemCode + " and UOM:" + Unit)
                    End If
                    unit_gross_wt += clsCommon.myCdbl(dtTemp.Rows(0)("gross_weight")) * Qty
                    unit_Net_wt += clsCommon.myCdbl(dtTemp.Rows(0)("Net_Weight")) * Qty
                End If




                'unit_gross_wt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select gross_weight from tspl_item_uom_detail where item_code='" + itemCode + "' and uom_code='" + Unit + "'"))

                ' ''====================================================================
                'wt_uom = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select weight_uom from tspl_item_master where item_code='" + itemCode + "'"))
                'qry = "select top 1 CF from (select (case when (Container_UOM='" & wt_uom & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & wt_uom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(itemCode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                'MT_CF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                'If MT_CF = 0 Then
                '    MT_CF = 1
                'End If
                ' ''====================================================================

                'unit_gross_wt = Qty * unit_gross_wt * MT_CF

                'txtGross_Wt.Text = clsCommon.myCdbl(txtGross_Wt.Text) + unit_gross_wt
            Next
            txtGross_Wt.Text = clsCommon.myCstr(Math.Round(unit_gross_wt, 3))
            txttotal_Wt.Text = clsCommon.myCstr(Math.Round(unit_Net_wt, 3))

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            'gv1.CurrentRow.Cells(colDisPer).Value
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        isCellValueChangedOpen = True
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                        isCellValueChangedOpen = False
                    End If
                    If e.Column Is gv1.Columns(colTotTaxAmt) And InsideUpdateCurrentRow = False Then
                        isCellValueChangedOpen = True
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv1.Columns(colAltUnitCOde) OrElse e.Column Is gv1.Columns(colTaxBasis) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colCommision_Rate) OrElse e.Column Is gv1.Columns(colOther_Chrage) OrElse e.Column Is gv1.Columns(colCash_Amt) OrElse e.Column Is gv1.Columns(colMRP) Then
                        isCellValueChangedOpen = True

                        If e.Column Is gv1.Columns(colQty) Then
                            'CalUnitPrice(gv1.CurrentRow.Index, True)
                            OpenBatchItem() ''only for qty
                            If GrossWtfromItemMaster Then
                                TotalGrossWt_FromItemMaster()
                            End If
                        End If

                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colTaxBasis).Value) <= 0 Then
                            gv1.CurrentRow.Cells(colTaxBasis).Value = "No"
                        End If
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colTaxBasis).Value, "Yes") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colTaxCalcType).Value = "Backward"
                        Else
                            gv1.CurrentRow.Cells(colTaxCalcType).Value = "Forward"
                        End If

                        '==================calculate abatement%========================================================================
                        gv1.CurrentRow.Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
                        gv1.CurrentRow.Cells(colAbatementAmt).Value = ((clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colAbatementPers).Value)) / 100) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                        '=============================================================================================================


                        'SetTax()
                        'SetTaxDetails()
                        '================================
                        gv1.CurrentRow.Cells(colIUnitWt).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value), 2)
                        gv1.CurrentRow.Cells(colINetWt).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2)
                        Dim qry As String = "select top 1 CF from (select (case when (Container_UOM='" & clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) + "'))aa where isnull(Cast(CF as varchar),'')<>'' order by Product_Type desc"
                        Dim wt_uom_cnvrsn As Decimal = 1 ' clsItemMaster.GetConvertionFactor(gv1.CurrentRow.Cells(colICode).Value, clsItemMaster.GetItemWeightUnit(gv1.CurrentRow.Cells(colICode).Value, Nothing), Nothing)
                        Dim mt_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) ' clsItemMaster.GetConvertionFactor(gv1.CurrentRow.Cells(colICode).Value, Weight_MT_Unit, Nothing)

                        If clsCommon.CompairString(clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing), Weight_MT_Unit) = CompairStringResult.Equal Then
                            mt_uom_cnvrsn = 1
                        End If

                        qry = "select top 1 CF from (select (case when (Container_UOM='" & clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) & "' and Contained_UOM='" & Gross_Weight_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Gross_Weight_Unit & "' and Contained_UOM='" & clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing) + "'))aa where isnull(Cast(CF as varchar),'')<>'' order by Product_Type desc"
                        Dim gross_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.CompairString(clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing), Gross_Weight_Unit) = CompairStringResult.Equal Then
                            gross_uom_cnvrsn = 1
                        End If

                        If gross_uom_cnvrsn > 0 Then
                            gv1.CurrentRow.Cells(colINetMTWt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * wt_uom_cnvrsn) * gross_uom_cnvrsn, 2) ' mt_uom_convrsn replaced by gross_uom_cnvrsn done by stuti on 04/01/2017
                        Else
                            gv1.CurrentRow.Cells(colINetMTWt).Value = 0
                            If clsCommon.CompairString(Gross_Weight_Unit, clsCommon.myCstr(clsItemMaster.GetItemWeightUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing))) = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colINetMTWt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)), 2)
                            End If
                        End If
                        '======================

                        gv1.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                        isValid_CashScheme()
                        gv1.CurrentRow.Cells(colTotalBasicAmount).Value = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Amt).Value), 2)

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_Commission_RS_PERS).Value), "P") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colCommisionCharges).Value = Math.Round(((clsCommon.myCdbl(gv1.CurrentRow.Cells(colCommision_Rate).Value) / IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)) / 100, 2)
                        Else
                            gv1.CurrentRow.Cells(colCommisionCharges).Value = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colCommision_Rate).Value) / IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2)
                        End If

                        gv1.CurrentRow.Cells(colTransferRate).Value = GetTransferRate(gv1.CurrentRow.Index)
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                        isCellValueChangedOpen = False
                    End If
                    If e.Column Is gv1.Columns(colIsSchmItem) OrElse e.Column Is gv1.Columns(colSchmCodeType) OrElse e.Column Is gv1.Columns(colQty) Then
                        isCellValueChangedOpen = True
                        Dim index As Integer = gv1.CurrentRow.Index

                        'CalUnitPrice(gv1.CurrentRow.Index, True)
                        FillFreeItemsInGrid()
                        isValid_CashScheme()
                        gv1.Rows(index).Cells(colTotalBasicAmount).Value = Math.Round((clsCommon.myCdbl(gv1.Rows(index).Cells(colRate).Value) * clsCommon.myCdbl(gv1.Rows(index).Cells(colQty).Value)) - clsCommon.myCdbl(gv1.Rows(index).Cells(colCash_Amt).Value), 2)

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(index).Cells(colCSA_Commission_RS_PERS).Value), "P") = CompairStringResult.Equal Then
                            gv1.Rows(index).Cells(colCommisionCharges).Value = Math.Round(((gv1.Rows(index).Cells(colCommision_Rate).Value / IIf(clsCommon.myCdbl(gv1.Rows(index).Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.Rows(index).Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.Rows(index).Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.Rows(index).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(index).Cells(colRate).Value)) / 100, 2)
                        Else
                            gv1.Rows(index).Cells(colCommisionCharges).Value = Math.Round((gv1.Rows(index).Cells(colCommision_Rate).Value / IIf(clsCommon.myCdbl(gv1.Rows(index).Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.Rows(index).Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.Rows(index).Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.Rows(index).Cells(colQty).Value), 2)
                        End If

                        gv1.Rows(index).Cells(colTransferRate).Value = GetTransferRate(index)
                        UpdateCurrentRow(index) ''-1 is for current row
                        UpdateAllTotals()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv1.Columns(colCash_Pers) Then
                        isCellValueChangedOpen = True
                        gv1.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                        isValid_CashScheme()
                        gv1.CurrentRow.Cells(colTotalBasicAmount).Value = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colCash_Amt).Value), 2)

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colCSA_Commission_RS_PERS).Value), "P") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colCommisionCharges).Value = Math.Round(((gv1.CurrentRow.Cells(colCommision_Rate).Value / IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)) / 100, 2)
                        Else
                            gv1.CurrentRow.Cells(colCommisionCharges).Value = Math.Round((gv1.CurrentRow.Cells(colCommision_Rate).Value / IIf(clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2)
                        End If

                        gv1.CurrentRow.Cells(colTransferRate).Value = GetTransferRate(gv1.CurrentRow.Index)
                        UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        UpdateAllTotals()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv1.Columns(colAltUnitCOde) Then
                        isCellValueChangedOpen = True
                        OpenUOMList(False)
                        isCellValueChangedOpen = False
                    End If
                End If
            End If

        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Function GetTransferRate(ByVal intRow As Integer)
        Dim totalTax As Double = 0
        Dim OtherCharges As Double = 0
        Dim CommisionCharges As Double = 0
        Dim totAmount As Double = 0
        Dim TransferRate As Double = 0
        totalTax = gv1.Rows(intRow).Cells(colTotTaxAmt).Value
        OtherCharges = gv1.Rows(intRow).Cells(colOther_Chrage).Value
        CommisionCharges = gv1.Rows(intRow).Cells(colCommisionCharges).Value

        totAmount = Math.Round((clsCommon.myCdbl(gv1.Rows(intRow).Cells(colRate).Value) * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colQty).Value)) - clsCommon.myCdbl(gv1.Rows(intRow).Cells(colCash_Amt).Value), 2)
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
            TransferRate = (totAmount - totalTax - OtherCharges - CommisionCharges) / IIf(gv1.Rows(intRow).Cells(colQty).Value = 0, 1, gv1.Rows(intRow).Cells(colQty).Value)
        Else
            TransferRate = (totAmount) / IIf(gv1.Rows(intRow).Cells(colQty).Value = 0, 1, gv1.Rows(intRow).Cells(colQty).Value)
        End If

        Return Math.Round(TransferRate, 2)
    End Function

    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = clsCommon.myCstr(intSerialNo)
        Next
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,unit_desc as [Description] from TSPL_ITEM_UOM_DETAIL"
            qry += " left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
            Dim whrCls As String = "Item_Code='" + strICode + "' and uom_code<>'" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            gv1.CurrentRow.Cells(colAltUnitCOde).Value = clsCommon.ShowSelectForm("OrderItefndnderaa", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colAltUnitCOde).Value), "Code", isButtonClick)
        End If
    End Sub

    Sub OpenGetbalance(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value))
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
        gv1.CurrentRow.Cells(colAltUnitCOde).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colSchmCode).Value = Nothing
        gv1.CurrentRow.Cells(colSchmCodeType).Value = Nothing
        gv1.CurrentRow.Cells(colIsSchmItem).Value = Nothing
        gv1.CurrentRow.Cells(colCash_Amt).Value = Nothing
        gv1.CurrentRow.Cells(colCash_Pers).Value = Nothing
        gv1.CurrentRow.Cells(colCashSchemeCode).Value = Nothing
        gv1.CurrentRow.Cells(colCashSchemeType).Value = Nothing
        gv1.CurrentRow.Cells(colFOC).Value = Nothing
        gv1.CurrentRow.Cells(colRate).Value = 0
        gv1.CurrentRow.Cells(colAbatementAmt).Value = 0
        gv1.CurrentRow.Cells(colAbatementPers).Value = 0
        gv1.CurrentRow.Cells(colIsMRPMandatory).Value = Nothing
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        'For i As Integer = 0 To gv1.Rows.Count - 1
        '    gv1.Rows(0).Cells(0).Value = 1
        '    If i <> 0 Then
        '        gv1.Rows(i).Cells(colLineNo).Value = i + 1
        '    End If
        'Next
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        InsideUpdateCurrentRow = True
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblAmt As Double = 0
        Dim TotalAmount As Double = 0
        Dim CSA_TYPE As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCSAType).Value)

        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)


        '==================calculate abatement%========================================================================
        gv1.Rows(IntRowNo).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))
        gv1.Rows(IntRowNo).Cells(colAbatementAmt).Value = ((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAbatementPers).Value)) / 100) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        '=============================================================================================================
        dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value)
        If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
            dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAbatementAmt).Value)
            gv1.CurrentRow.Cells(colTotalBasicAmount).Value = dblAmt
        End If

        TotalAmount = dblAmt
        gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Tag = TotalAmount
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
            dblAmt = dblAmt - clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOther_Chrage).Value)
        End If

        For ii As Integer = 1 To 10

            Dim Strii As String = clsCommon.myCstr(ii)
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


                    'If strExcise = True AndAlso IsExcisable = True Then ''AndAlso intMRPwithabatement = 1 
                    '    dblBaseAmt = (dblAbatementAmt + dblOtherTaxAmt)
                    'Else
                    '    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                    'End If
                    If clsCommon.CompairString(clsCSASaleInvoiceItem.MandiTax(CSA_TYPE, strTaxCode), "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
                                dblBaseAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAbatementAmt).Value) - dblOtherTaxAmt
                            Else
                                dblBaseAmt = dblAmt - dblOtherTaxAmt
                            End If

                        Else
                            If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
                                dblBaseAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAbatementAmt).Value) + dblOtherTaxAmt
                            Else
                                dblBaseAmt = dblAmt + dblOtherTaxAmt
                            End If

                        End If
                    Else
                        dblBaseAmt = 0
                    End If

                End If
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTaxRate)
                Else
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                End If

                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
                If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                    arrTaxableAuth.Add(strTaxCode.ToUpper())
                End If
                'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
                '    dblAmt = dblAmt - dblTaxAmt
                'End If

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
            'End If
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = 0

        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
            dblAmtAfterTax = TotalAmount - dblTotTaxAmt
        Else
            dblAmtAfterTax = TotalAmount + dblTotTaxAmt
        End If

        If dblQty > 0 Then
            Dim dblNetPrice As Double = dblAmt / dblQty
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblNetPrice, 2)
        End If

        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)

        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCSA_Commission_RS_PERS).Value), "P") = CompairStringResult.Equal Then
            gv1.Rows(IntRowNo).Cells(colCommisionCharges).Value = Math.Round(((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCommision_Rate).Value) / IIf(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)) / 100, 2)
        Else
            gv1.Rows(IntRowNo).Cells(colCommisionCharges).Value = Math.Round((clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCommision_Rate).Value) / IIf(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMaster_Pack_Size).Value) = 0, 1, clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMaster_Pack_Size).Value))) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItem_Pack_Size).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 2)
        End If
        gv1.Rows(IntRowNo).Cells(colTransferRate).Value = GetTransferRate(IntRowNo)

        If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTotalBasicAmount).Value) < clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCommisionCharges).Value) Then
            gv1.Rows(IntRowNo).Cells(colCommisionCharges).Value = 0
            gv1.Rows(IntRowNo).Cells(colCommision_Rate).Value = 0
        End If
        InsideUpdateCurrentRow = False
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
        Dim dblTotalWt As Double = 0

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

        Dim TotalCommitionCharges As Double = 0
        Dim TotalOtherCharges As Double = 0
        Dim TotalTransferCharges As Double = 0

        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            'And clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value) = 0
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalBasicAmount).Value)

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
                TotalOtherCharges = TotalOtherCharges + clsCommon.myCdbl(gv1.Rows(ii).Cells(colOther_Chrage).Value)
                TotalCommitionCharges = TotalCommitionCharges + clsCommon.myCdbl(gv1.Rows(ii).Cells(colCommisionCharges).Value)
                TotalTransferCharges = TotalTransferCharges + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTransferRate).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
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


        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
        Next

        For Each grow As GridViewRowInfo In gv1.Rows
            dblTotalWt += clsCommon.myCdbl(grow.Cells(colINetMTWt).Value)
        Next

        lblAmtWithDiscount.Text = Math.Round(dblTotAmt, 2)
        'lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt)
        'lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTotalTaxAmt.Text = Math.Round(dblTaxTotAmt, 2)

        lblAddCharges.Text = Math.Round(dblACAmount, 2)
        lblTotalOtherCharges.Text = Math.Round(dblACAmount, 2)

        dblNetAmt = dblNetAmt + dblACAmount

        'lblInvoiceDiscAmt.Text = dblHeadDisAmt + dblHeadDisPerAmt
        'lblInvoiceDiscAmt.Text = dblHeadDisAmt
        'txtDiscAmt.Text = lblInvoiceDiscAmt.Text
        lblTotRAmt.Text = Math.Round(dblNetAmt, 2)
        txtDocumentTotal.Text = lblTotRAmt.Text

        '' update totals
        Me.lblTotalAmount.Text = Math.Round(dblTotAmt, 2)
        Me.lblTotalOtherCharges.Text = Math.Round(TotalOtherCharges, 2)
        Me.lblTotalTaxAmt.Text = Math.Round(dblTaxTotAmt, 2)
        Me.lblCommissionCharges.Text = Math.Round(TotalCommitionCharges, 2)
        Me.lblTotRAmt.Text = Math.Round(TotalTransferCharges, 2)
        Me.txtDocumentTotal.Text = Math.Round(TotalTransferCharges, 2)

        '------------------provision calculation---------------------------------------------------
        If Not GrossWtfromItemMaster Then
            txttotal_Wt.Text = Math.Round(dblTotalWt, 2)
        End If


        'txtvehicle_Charge.Text = Math.Round(clsCSATransfer.GetProvisionCharge(txtFromLocation.Value, txtCustCode.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicle_Capacity.Text), txtTransporter_Code.Value), 2)
        FillVehicleCharges()
    End Sub

    Private Function GetBaseOtherTaxableAmount(ByVal intEndCol As Integer) As Double
        'Dim dblRetVal As Double = 0
        'For ii As Integer = 0 To intEndCol - 1
        '    If clsCommon.myCBool(gv2.Rows(ii).Cells(colTIsTaxable).Value) Then
        '        dblRetVal = dblRetVal + clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
        '    End If
        'Next
        'Return dblRetVal
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
        TxtTransportorMName.MendatroryField = True
        TxtTransportorMName.Visible = False
        BlankAllControls()
        'fndProject.Enabled = True
        'lblProject.Enabled = True
        LoadBlankGrid()
        LoadBlankGridTax()
        LoadBlankGridAC()
        isNewEntry = True
        'btnReverse.Enabled = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        'gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
        gvAC.Rows.AddNew()
        gvAC.Rows.AddNew()
        btnHistory.Enabled = False
        txtDesc.Text = ""
        txtDate.Enabled = True


        LOCATIONRIGTHS()

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave(Optional ByVal IsPost As Boolean = False) As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If

            '' check for the minimum order level including tolerance 
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(cmbTax.SelectedValue) <= 0 Then
                cmbTax.SelectedValue = "No"
            End If
            Dim proceed As Boolean = False
            For Each dr As GridViewRowInfo In gv1.Rows
                RadPageView1.SelectedPage = RadPageViewPage1
                gv1.Focus()
                gv1.Select()

                If clsCommon.myLen(dr.Cells(colICode).Value) > 0 Then
                    If proceed = True Then
                        Exit For
                    End If
                    Dim balQty As Decimal = clsItemLocationDetails.getBalance(dr.Cells(colICode).Value, clsCommon.myCstr(Me.txtFromLocation.Value), Me.txtDocNo.Value, txtDate.Value, Nothing, dr.Cells(colUnit).Value, 0)

                    If IsPost Then
                        If balQty < clsCommon.myCdbl(dr.Cells(colQty).Value) Then
                            clsCommon.MyMessageBoxShow(Me, "Balance of item " & dr.Cells(colICode).Value & "  on Location " & clsCommon.myCstr(Me.txtFromLocation.Value) & " " & balQty & " and required quantity " & clsCommon.myCdbl(dr.Cells(colQty).Value) & "")
                            gv1.CurrentRow = gv1.Rows(dr.Index)
                            gv1.CurrentColumn = gv1.Columns(colQty)
                            Return False
                        End If
                    End If
                End If

            Next

            If GrossWtfromItemMaster Then
                TotalGrossWt_FromItemMaster()
            End If

            UpdateAllTotals()
            'CalculateDiscountAmount()
            If clsCommon.myLen(txtCustCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCustCode.Select()
                txtCustCode.Focus()
                ErrorControl.SetError(txtCustDesc, "Select customer for transfer.")
                Return False
            Else
                ErrorControl.ResetError(txtCustDesc)
            End If

            If clsCommon.CompairString(cmbEXType.SelectedValue, "") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Transfer type.", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                cmbEXType.Select()
                cmbEXType.Focus()
                ErrorControl.SetError(cmbEXType, "Select Transfer type.")
                Return False
            Else
                ErrorControl.ResetError(cmbEXType)
            End If

            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage2
                txtTaxGroup.Select()
                txtTaxGroup.Focus()
                ErrorControl.SetError(lblTaxGrpName, "Select tax group.")
                Return False
            Else
                ErrorControl.ResetError(lblTaxGrpName)
            End If



            '========================================================
            Dim counter_F As Integer = 0
            If chk_F_Form.Checked Then
                'For Each grow As GridViewRowInfo In gv2.Rows
                '    Dim taxcode As String = ""
                '    taxcode = clsCommon.myCstr(grow.Cells(colTTaxAutCode).Value)
                '    counter_F += clsDBFuncationality.getSingleValue("select count(*) from TSPL_TAX_RATES where tax_code='" + taxcode + "' and tax_type='S' and _Type='F'")
                'Next

                'If counter_F <= 0 Then
                '    common.clsCommon.MyMessageBoxShow("Please select Tax Group,which is mapped with F-Form.")
                '    txtTaxGroup.Focus()
                '    Return False
                'End If
            End If
            '=======================================================================

            If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                txtFromLocation.Select()
                txtFromLocation.Focus()
                ErrorControl.SetError(txtFromLocationDesc, "Select bill to location.")
                Return False
            Else
                ErrorControl.ResetError(txtFromLocationDesc)
            End If
            '=============================================================
            'If isALlowVehicleGateOutValidation = True Then
            '    If clsCommon.myLen(txtvehicle_code.Text) > 0 Then
            '        Dim qry As String = String.Empty
            '        qry = "select count(*) from TSPL_CSATransfer_Gate_Out where  CSATransfer_No= ( select Top 1 DOC_CODE from  TSPL_CSA_TRANSFER_HEAD where Vehicle_Id='" & txtvehicle_code.Text & "' order by Transfer_Date desc)"
            '        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
            '            qry = "select Top 1 DOC_CODE from  TSPL_CSA_TRANSFER_HEAD where Vehicle_Id='" & txtvehicle_code.Text & "' order by Transfer_Date desc"
            '            Dim TransferNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '            common.clsCommon.MyMessageBoxShow("Vehicle No ('" & txtvehicle_code.Text & "') used in Transfer No '" & TransferNo & "'. After Gate Out Transfer No '" & TransferNo & "' ,You can use this Vehicle No.  ")
            '            Return False

            '        End If
            '    End If

            'End If

            If isALlowVehicleGateOutValidation = True Then
                If clsCommon.myLen(txtvehicle_code.Text) > 0 Then
                    Dim qry As String = String.Empty
                    qry = " SELECT Stuff((SELECT N', ' + TSPL_CSA_TRANSFER_HEAD.DOC_CODE FROM TSPL_CSA_TRANSFER_HEAD left join TSPL_CSATransfer_Gate_Out on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSATransfer_Gate_Out.CSATransfer_No  where TSPL_CSA_TRANSFER_HEAD.Vehicle_Id='" & txtvehicle_code.Text & "' and TSPL_CSATransfer_Gate_Out.CSATransfer_No is null FOR XML PATH(''),TYPE).value('text()[1]','nvarchar(max)'),1,2,N'') "
                    Dim result As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(result) > 0 Then
                        common.clsCommon.MyMessageBoxShow("Vehicle No ('" & txtvehicle_code.Text & "') used in other Transfer No. You can create new Transfer with Vehicle No ('" & txtvehicle_code.Text & "')  After  Gate Out following Transfer No : '" & result & "'")

                        Return False

                    End If
                End If

            End If


            '==============================================================
            Dim excisableLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
            If clsCommon.CompairString(excisableLoc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then
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

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Transfer No not found to save", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Select()
                txtDocNo.Focus()
                ErrorControl.SetError(txtDocNo, "Transfer No not found to save.")
                Return False
            Else
                ErrorControl.ResetError(txtDocNo)
            End If

            If clsCommon.myLen(txtGR_No.Text) > 0 AndAlso clsCommon.myLen(dtpGR.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpGR.Focus()
                dtpGR.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill GR Date", Me.Text)
                ErrorControl.SetError(dtpGR, "Fill GR Date")
                Return False
            Else
                ErrorControl.ResetError(dtpGR)
            End If
            '' If Own Vehicle is checked then manual transporter name will be mandatory
            If chkownvehicle.Checked = True AndAlso clsCommon.myLen(clsCommon.myCstr(TxtTransportorMName.Text)) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                TxtTransportorMName.Focus()
                'Throw New Exception("Please fill transpoter name")
                clsCommon.MyMessageBoxShow(Me, "Please fill transpoter name", Me.Text)
                ErrorControl.SetError(TxtTransportorMName, "Please fill transpoter name")
                Return False
            Else
                ErrorControl.ResetError(TxtTransportorMName)
            End If
            If chkownvehicle.Checked = False AndAlso clsCommon.myLen(clsCommon.myCstr(txtTransporter_Code.Value)) > 0 Then
                If clsCommon.myLen(txtvehicle_code.Text) = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Pls enter vehicle no", Me.Text)
                    txtvehicle_code.Focus()
                    ErrorControl.SetError(TxtTransportorMName, "Pls enter vehicle no")
                    Return False
                End If
                If clsCommon.myCdbl(txtVehicle_Capacity.Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Pls enter vehicle capacity", Me.Text)
                    txtVehicle_Capacity.Focus()
                    ErrorControl.SetError(TxtTransportorMName, "Pls enter vehicle capacity")
                    Return False
                End If
                If clsCommon.myCdbl(txtvehicle_Charge.Text) = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Pls enter Freight in Route Freight Details.", Me.Text)
                    Return False
                End If
            End If
            Dim arrProjNo As New List(Of String)
            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                RadPageView1.SelectedPage = RadPageViewPage1
                gv1.Focus()
                gv1.Select()

                Dim strDONo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDOCode).Value)
                'Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOBalanceQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                Dim IsMandatoryMRP As Boolean = clsCommon.myCBool(gv1.Rows(ii).Cells(colIsMRPMandatory).Value)
                ''Dim dblAssessableAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strAltUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAltUnitCOde).Value)


                If clsCommon.myLen(strDONo) > 0 AndAlso clsCommon.myLen(strICode) > 0 Then
                    If (Not clsCSADeliveryOrder.IsValidCustomerForDOItem(strDONo, strICode, txtCustCode.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Customer :" + txtCustDesc.Text + " is not valid for DO No:" + strDONo + " and Item : " + strIName + " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colICode)
                        Return False
                    End If
                    If dblQty > dblPendingQty Then
                        common.clsCommon.MyMessageBoxShow(Me, "Item " + strICode + "( " + strIName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(dblQty) + ") Cannot be more than Pending Quantity(" + clsCommon.myCstr(dblPendingQty) + ").At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colQty)
                        Return False
                    End If
                End If
                If clsCommon.myLen(strICode) > 0 AndAlso Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colTaxBasis).Value) <= 0 Then
                    gv1.Rows(ii).Cells(colTaxBasis).Value = "No"
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strAltUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Select Alternate UOM of item " + strICode + "( " + strIName.Trim() + " ) at line no: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colAltUnitCOde)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal AndAlso IsMandatoryMRP AndAlso dblMRP <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Fill MRP of item " + strICode + "( " + strIName.Trim() + " ) at line no: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                    gv1.CurrentRow = gv1.Rows(ii)
                    gv1.CurrentColumn = gv1.Columns(colMRP)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                            Throw New Exception("Item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " Entered Qty " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If

                If clsCommon.myLen(strICode) > 0 Then
                    For j As Integer = ii + 1 To gv1.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(j).Cells(colICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colFOC).Value), "Y") <> CompairStringResult.Equal Then ''free item can be duplicate
                                gv1.Focus()
                                gv1.Select()
                                gv1.CurrentRow = gv1.Rows(j)
                                gv1.CurrentColumn = gv1.Columns(colICode)
                                clsCommon.MyMessageBoxShow(Me, "Duplicate item at row no. " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(j + 1) + ".", Me.Text)
                                Return False
                            End If
                        End If
                    Next
                End If

                ''---------------------------------------------
            Next

            If arrICode Is Nothing OrElse arrICode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv1.Focus()
                gv1.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill item detail in grid.", Me.Text)
                Return False
            End If

            'clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                'If CFormFunction() = False Then
                '    Return False
                'End If
            End If
            '--------------------Check Excisable Transfer------------By PANKAJ KUMAR CHAUDHARY-17/APR/2015----------
            Dim intx As Integer = clsItemMaster.isItemOfSameExcisable(arrICode)
            If Not (intx = arrICode.Count OrElse intx = 0) Then
                Throw New Exception("All item should be of Excisable or NonExcisable")
            End If
            If intx > 0 Then
                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                    Throw New Exception("Please select tax group.")
                Else
                    If clsLocation.isLocatinExcisable(txtFromLocation.Value) = True Then
                        For Each grow As GridViewRowInfo In gv2.Rows
                            If Not clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_TAX_MASTER WHERE Tax_Code='" + grow.Cells(colTTaxAutCode).Value + "'")), "Y") = CompairStringResult.Equal Then
                                Throw New Exception("Atleast One tax should be excisable.")
                            Else
                                Exit For
                            End If
                        Next
                    End If
                End If
                Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in (" + clsCommon.GetMulcallString(arrICode) + ")"))
            Else
                Item_TaxType = 0
            End If
            '------------------------------------------------------------------------------------------------------
            'Dim GSTStatus As Boolean = False
            'GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            'If GSTStatus Then
            '    If clsLocationWiseTax.IsValidTaxGroupForTransfer(txtTaxGroup.Value, txtFromLocation.Value, txtCustCode.Value, "S", txtDate.Value, False, Nothing) = False Then
            '        Return False
            '    End If
            'End If


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub
    'Function CFormFunction()
    '    Try
    '        Dim intQuarter As Integer
    '        Dim strYear As Integer
    '        Dim strFromDate As String
    '        Dim strToDate As String
    '        Dim strSql As String
    '        Dim StrValidation As String
    '        Dim intCForm As Integer

    '        If (Month(txtDate.Value) = 1 OrElse Month(txtDate.Value) = 2 OrElse Month(txtDate.Value) = 3) Then
    '            intQuarter = 4
    '        ElseIf (Month(txtDate.Value) = 4 OrElse Month(txtDate.Value) = 5 OrElse Month(txtDate.Value) = 6) Then
    '            intQuarter = 1
    '        ElseIf (Month(txtDate.Value) = 7 OrElse Month(txtDate.Value) = 8 OrElse Month(txtDate.Value) = 9) Then
    '            intQuarter = 2
    '        ElseIf (Month(txtDate.Value) = 10 OrElse Month(txtDate.Value) = 11 OrElse Month(txtDate.Value) = 12) Then
    '            intQuarter = 3
    '        End If

    '        If intQuarter = 1 Then

    '            strYear = Year(txtDate.Value) - 1
    '            ''' for 3st quarter
    '            strFromDate = "01/Jul/" & strYear
    '            strToDate = "30/Sep/" & strYear
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 2nd quarter
    '            strFromDate = "01/Oct/" & strYear
    '            strToDate = "31/Dec/" & strYear
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 1rd quarter
    '            strFromDate = "01/Jan/" & Year(txtDate.Value)
    '            strToDate = "31/Mar/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '        ElseIf intQuarter = 2 Then

    '            strYear = Year(txtDate.Value) - 1
    '            ''' for 3st quarter
    '            strFromDate = "01/Oct/" & strYear
    '            strToDate = "31/Dec/" & strYear
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 2nd quarter
    '            strFromDate = "01/Jan/" & Year(txtDate.Value)
    '            strToDate = "31/Mar/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 1rd quarter
    '            strFromDate = "01/Apr/" & Year(txtDate.Value)
    '            strToDate = "30/Jun/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If


    '        ElseIf intQuarter = 3 Then

    '            strYear = Year(txtDate.Value) - 1
    '            ''' for 3rd quarter
    '            strFromDate = "01/Jan/" & Year(txtDate.Value)
    '            strToDate = "31/Mar/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 2nd quarter
    '            strFromDate = "01/Apr/" & Year(txtDate.Value)
    '            strToDate = "30/Jun/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 1rd quarter
    '            strFromDate = "01/Jul/" & Year(txtDate.Value)
    '            strToDate = "30/Sep/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '        ElseIf intQuarter = 4 Then

    '            strYear = Year(txtDate.Value) - 1
    '            ''' for 3st quarter
    '            strFromDate = "01/Apr/" & strYear
    '            strToDate = "30/Jun/" & strYear
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=3")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 2nd quarter
    '            strFromDate = "01/Jul/" & strYear
    '            strToDate = "30/Sep/" & strYear
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=2")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '            ''' for 1rd quarter
    '            strFromDate = "01/Oct/" & Year(txtDate.Value)
    '            strToDate = "31/Dec/" & Year(txtDate.Value)
    '            intCForm = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(DOC_CODE) from TSPL_SD_SALE_INVOICE_HEAD where CFormApplied=0 and CFormRecd=0 and Against_C_Form=1 AND Transfer_Date > ='" & strFromDate & " ' and Transfer_Date < ='" & strToDate & "' "))
    '            If intCForm > 0 Then
    '                StrValidation = clsDBFuncationality.getSingleValue("select Validation from TSPL_SCREEN_NOTIFICATION_SETTING where Quarter=1")
    '                If StrValidation = "Warning" Then
    '                    clsCommon.MyMessageBoxShow("Please Fill CForm For this customer " & txtVendorNo.Value & " ")
    '                ElseIf StrValidation = "Required Approval" Then
    '                    intApprovel_Required = 1
    '                    clsCommon.MyMessageBoxShow("Please Approve this order to create without CForm ")
    '                ElseIf StrValidation = "Full Stop" Then
    '                    clsCommon.MyMessageBoxShow("You have no permission to create order for this customer " & txtVendorNo.Value & ".Your CForm are pending For this customer " & txtVendorNo.Value & " ")
    '                    Return False
    '                End If
    '            End If

    '        End If
    '        Return True
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function

    Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (SaveData(False, ChekBtnPost)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                'btnReverse.Enabled = False
            End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean, Optional ByVal IsPost As Boolean = False) As Boolean
        Dim obj As New clsCSATransfer()
        Try
            If (AllowToSave(IsPost)) Then
                'clsCommon.ProgressBarShow()

                obj.DOC_CODE = txtDocNo.Value
                obj.Secondary_Doc_Code = clsCommon.myCstr(txtSecondary_Doc_Code.Text)

                obj.DELEVERY_ORDER_NO = fndDONo.Value
                obj.Transfer_Date = txtDate.Value
                obj.Waybill_No = clsCommon.myCstr(txtWayBill_No.Text)
                obj.Waybill_Date = clsCommon.myCDate(ttxway_bill_date.Text)
                obj.Vehicle_code = clsCommon.myCstr(txtvehicle_code.Text)
                obj.Vehicle_Capacity = clsCommon.myCdbl(txtVehicle_Capacity.Text)
                obj.Vehicle_Charge = clsCommon.myCdbl(txtvehicle_Charge.Text)

                ''======================================================================
                If txtvehicle_Charge.Tag IsNot Nothing AndAlso TryCast(txtvehicle_Charge.Tag, DataTable) IsNot Nothing AndAlso TryCast(txtvehicle_Charge.Tag, DataTable).Rows.Count > 0 Then
                    Dim dt As DataTable = TryCast(txtvehicle_Charge.Tag, DataTable)
                    obj.Freight_Type = clsCommon.myCstr(dt.Rows(0)("FreightType"))
                    obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("FixedCharge"))
                    obj.EmptyCharge = clsCommon.myCdbl(dt.Rows(0)("EmptyCharge"))
                End If
                ''======================================================================
                If txtRemovalDate.Checked Then
                    obj.Removal_Date = txtRemovalDate.Value
                End If
                obj.Total_Item_Wt = clsCommon.myCdbl(txttotal_Wt.Text)
                obj.Gross_Item_Wt = clsCommon.myCdbl(txtGross_Wt.Text)
                obj.GR_No = clsCommon.myCstr(txtGR_No.Text)
                If clsCommon.myLen(obj.GR_No) > 0 Then
                    obj.GR_Date = clsCommon.myCDate(dtpGR.Text)
                Else
                    obj.GR_Date = Nothing
                End If

                obj.Ship_To_Location = clsCommon.myCstr(txtship_to_loc_code.Value)
                obj.Excisable = clsCommon.myCstr(IIf(cmbEXType.SelectedValue = "E", "1", "0"))
                'obj.Transport_Id = clsCommon.myCstr(txtTransporter_Code.Value)
                obj.Cust_Code = txtCustCode.Value
                obj.Customer_Name = txtCustDesc.Text
                obj.Against_F = clsCommon.myCstr(IIf(chk_F_Form.Checked = True, "F", ""))

                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTotalTaxAmt.Text)

                obj.From_Location_Code = txtFromLocation.Value
                obj.To_Location_Code = txtToLocation.Value

                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.CSA_Rate = clsCommon.myCdbl(Me.txtCSARate.Text)
                obj.State_Code = Me.txtState.Value
                obj.Inculding_Tax = Me.cmbTax.SelectedValue
                obj.Document_Amount = txtDocumentTotal.Text

                obj.Approvel_Required = intApprovel_Required
                If chkownvehicle.Checked = True Then
                    obj.Transport_Id = ""
                    obj.Transporter_Name_Manual = clsCommon.myCstr(TxtTransportorMName.Text)
                Else
                    obj.Transporter_Name_Manual = ""
                    obj.Transport_Id = clsCommon.myCstr(txtTransporter_Code.Value)
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
                obj.Total_Add_Charge = clsCommon.myCdbl(lblTotalOtherCharges.Text)
                obj.Total_Commission_Chrage = clsCommon.myCdbl(lblCommissionCharges.Text)
                obj.Item_Tax_Type = Item_TaxType
                'obj.Salesman_Code = txtSalesman.Value
                'obj.Salesman_Name = lblSalesman.Text

                obj.Arr = New List(Of clsCSATransferDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsCSATransferDetail()
                    objTr.arrBatchItem = New List(Of clsBatchInventory)

                    objTr.Line_No = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(colLineNo).Value))
                    objTr.Including_Tax = clsCommon.myCstr(grow.Cells(colTaxBasis).Value)
                    objTr.Calc_Type = clsCommon.myCstr(grow.Cells(colTaxCalcType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Item_Unit_Wt = clsCommon.myCdbl(grow.Cells(colIUnitWt).Value)
                    objTr.Item_Net_Wt = clsCommon.myCdbl(grow.Cells(colINetWt).Value)
                    objTr.Item_Net_MT_Wt = clsCommon.myCdbl(grow.Cells(colINetMTWt).Value)
                    objTr.Alt_Unit_Code = clsCommon.myCstr(grow.Cells(colAltUnitCOde).Value)

                    objTr.DELEVERY_ORDER_NO = clsCommon.myCstr(grow.Cells(colDOCode).Value)
                    objTr.DO_Pending_Qty = clsCommon.myCdbl(grow.Cells(colDOBalanceQty).Value)
                    objTr.DO_Qty = clsCommon.myCdbl(grow.Cells(colDOQty).Value)

                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)

                    objTr.Unit_Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)

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

                    'objTr.Location = txtFromLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colTotalBasicAmount).Value)

                    objTr.Item_Pack_Size = clsCommon.myCdbl(grow.Cells(colItem_Pack_Size).Value)
                    objTr.Item_Master_Pack_Size = clsCommon.myCdbl(grow.Cells(colMaster_Pack_Size).Value)
                    'objTr. = clsCommon.myCdbl(grow.Cells(colItem_Pack_Size).Value)
                    objTr.Commision_Rate = clsCommon.myCdbl(grow.Cells(colCommision_Rate).Value)
                    objTr.Commission_Chrage = clsCommon.myCdbl(grow.Cells(colCommisionCharges).Value)
                    objTr.CSA_Commission_RS_PERS = clsCommon.myCstr(grow.Cells(colCSA_Commission_RS_PERS).Value)
                    objTr.Other_Chrage = clsCommon.myCdbl(grow.Cells(colOther_Chrage).Value)

                    objTr.Transfer_Rate = clsCommon.myCdbl(grow.Cells(colTransferRate).Value)

                    objTr.FOC = clsCommon.myCstr(grow.Cells(colFOC).Value)
                    objTr.Scheme_Applicable = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
                    objTr.Scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCode).Value)
                    objTr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                    objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                    objTr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)

                    objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    objTr.Is_MRP_Mandatory = CInt(clsCommon.myCdbl(IIf(clsCommon.myCBool(grow.Cells(colIsMRPMandatory).Value) = True, 1, 0)))
                    objTr.Abatement_Pers = clsCommon.myCdbl(grow.Cells(colAbatementPers).Value)
                    objTr.Abatement_Amt = clsCommon.myCdbl(grow.Cells(colAbatementAmt).Value)
                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))


                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill atleast one Item", Me.Text)
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

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                If isSaved Then
                    UcAttachment1.SaveData(obj.DOC_CODE)

                    LoadData(obj.DOC_CODE, NavigatorType.Current)
                End If

                Return isSaved
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsCSATransfer()
        Try

            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            'btnReverse.Enabled = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            LoadBlankGridTax()
            LoadBlankGridAC()
            cboItemType.Enabled = False
            'txtFromLocation.Enabled = False

            obj = clsCSATransfer.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_CODE) > 0) Then
                chkownvehicle.Checked = False
                If clsCommon.myLen(obj.Transport_Id) <= 0 Then
                    chkownvehicle.Checked = True
                End If

                txtTransporter_Code.Value = obj.Transport_Id
                txtTransporter_desc.Text = obj.Transport_Desc

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnupdate.Enabled = True
                    If ShowDocumentCancel = True Then
                        btnCancel.Visible = True
                    End If
                End If

                txtSecondary_Doc_Code.Text = obj.Secondary_Doc_Code
                txtvehicle_code.Text = obj.Vehicle_code
                UsLock1.Status = obj.Status
                Me.txtCustCode.Value = obj.Cust_Code
                txtCustDesc.Text = obj.Customer_Name
                txtWayBill_No.Text = obj.Waybill_No
                ttxway_bill_date.Text = obj.Waybill_Date
                TxtEWayBillNo.Text = obj.EWayBillNo
                TxtEWayBillDate.Value = obj.EWayBillDate
                txtElectronicRefNo.Text = obj.Electronic_Ref_No
                fndDONo.Value = obj.DELEVERY_ORDER_NO
                txtDocNo.Value = obj.DOC_CODE
                txtDate.Value = obj.Transfer_Date
                txtState.Value = obj.State_Code
                txtVehicle_Capacity.Text = obj.Vehicle_Capacity
                txtvehicle_Charge.Text = obj.Vehicle_Charge
                ''richa agarwal 
                If obj.Removal_Date IsNot Nothing Then
                    txtRemovalDate.Value = obj.Removal_Date
                    txtRemovalDate.Checked = True
                End If
                ''---------------
                ''======================================================================
                If clsCommon.myCdbl(obj.Vehicle_Charge) > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("FixedCharge", GetType(Decimal))
                    dt.Columns.Add("EmptyCharge", GetType(Decimal))
                    dt.Columns.Add("FreightCharge", GetType(Decimal))
                    dt.Columns.Add("FreightType", GetType(String))
                    Dim dr As DataRow = dt.NewRow()
                    dr("FreightType") = obj.Freight_Type
                    dr("FixedCharge") = obj.FixedCharge
                    dr("FreightCharge") = obj.Vehicle_Charge
                    dr("EmptyCharge") = obj.EmptyCharge

                    txtvehicle_Charge.Tag = dt
                Else
                    txtvehicle_Charge.Tag = Nothing
                End If
                ''======================================================================

                txttotal_Wt.Text = obj.Total_Item_Wt
                txtGross_Wt.Text = obj.Gross_Item_Wt
                txtGR_No.Text = obj.GR_No
                dtpGR.Text = obj.GR_Date

                txtExcisable.Text = obj.Excisable
                cmbEXType.SelectedValue = IIf(obj.Excisable = "1", "E", "N")
                cmbEXType.Enabled = False

                If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal AndAlso ForUDLOnly Then
                    MyLabel24.Visible = True
                    txtSecondary_Doc_Code.Visible = True
                Else
                    MyLabel24.Visible = False
                    txtSecondary_Doc_Code.Visible = False
                End If

                txtStateDesc.Text = obj.State_Code
                txtDate.Enabled = False
                txtCustCode.Enabled = False
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtCustCode.Value)
                txtCustDesc.Text = obj.Customer_Name

                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                txtCSARate.Text = obj.CSA_Rate

                txtship_to_loc_code.Value = obj.Ship_To_Location
                txtship_to_loc_name.Text = obj.Ship_To_Location_Desc
                '------------------------------------------------
                vaddnew = "N"

                txtToLocation.Value = obj.To_Location_Code
                txtFromLocation.Value = obj.From_Location_Code
                txtFromLocationDesc.Text = obj.From_Location_Name
                txtToLocationDesc.Text = obj.To_Location_Name
                txtDocumentTotal.Text = obj.Document_Amount
                cmbTax.SelectedValue = obj.Inculding_Tax
                chk_F_Form.Checked = clsCommon.myCBool(IIf(obj.Against_F = "F", True, False))

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                lblTotalTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = obj.Document_Amount
                txtFromLocationDesc.Text = obj.From_Location_Name
                txtToLocationDesc.Text = obj.To_Location_Name
                lblTaxGrpName.Text = obj.TaxGroupName
                TxtTransportorMName.Text = clsCommon.myCstr(obj.Transporter_Name_Manual)
                If obj.Approvel_Required = 1 And obj.Is_Approved = 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Approval is required for this order", Me.Text)
                    btnPost.Enabled = False
                    'Else
                    '    btnPost.Enabled = True
                End If
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
                lblTotalOtherCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                lblCommissionCharges.Text = clsCommon.myFormat(obj.Total_Commission_Chrage)

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCSATransferDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objTr.Line_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = objTr.Including_Tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = objTr.Calc_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSAType).Value = objTr.CSA_Type

                        If clsCommon.CompairString(objTr.FOC, "Y") <> CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDOBalanceQty).Value = clsCommon.myCdbl(clsCSADeliveryOrder.GetBalanceDOQtyByTransfer(objTr.DELEVERY_ORDER_NO, objTr.Item_Code, obj.DOC_CODE, objTr.Unit_code)) 'objTr.DO_Pending_Qty
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDOBalanceQty).Value = "0"
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDOCode).Value = objTr.DELEVERY_ORDER_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDOQty).Value = objTr.DO_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIUnitWt).Value = objTr.Item_Unit_Wt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINetWt).Value = objTr.Item_Net_Wt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = objTr.Item_Net_MT_Wt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAltUnitCOde).Value = objTr.Alt_Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Unit_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSAType).Value = objTr.CSA_Type
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

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = objTr.Total_Basic_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Item_Net_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Pack_Size).Value = objTr.Item_Pack_Size
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMaster_Pack_Size).Value = objTr.Item_Master_Pack_Size
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colMaster_Pack_Size).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.description from (select TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Master_Value='1' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + clsCommon.myCstr(objTr.Item_Code) + "')a"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Value = objTr.Commision_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).Value = objTr.Commission_Chrage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSA_Commission_RS_PERS).Value = objTr.CSA_Commission_RS_PERS
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).Value = objTr.Other_Chrage

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTransferRate).Value = objTr.Transfer_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), 0), 2)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMRPMandatory).Value = objTr.Is_MRP_Mandatory
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPers).Value = objTr.Abatement_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmt).Value = objTr.Abatement_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFOC).Value = objTr.FOC
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCode).Value = objTr.Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = objTr.Scheme_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = objTr.Scheme_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = objTr.Scheme_Item_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).Value = objTr.Scheme_Applicable
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = objTr.Cash_Scheme_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = objTr.Cash_Scheme_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeCode).Value = objTr.Cash_Scheme_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = objTr.Cash_Scheme_Type

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem

                        If clsCommon.CompairString(objTr.FOC, "Y") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).ReadOnly = True
                        End If
                    Next

                    If obj.Status = ERPTransactionStatus.Approved Then
                        btnAmendment.Enabled = True
                    End If
                    If obj.Status = ERPTransactionStatus.Pending Then
                        'gv1.Rows.AddNew()

                        gvAC.Rows.AddNew()
                    End If


                End If
                SetitemWiseTaxOnlySetting()
                UpdateAllTotals()


                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.DOC_CODE)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.DOC_CODE, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                btnReverse.Enabled = True

                ''========= added by Parteek 1-02-2017 Cancel Document
                If ShowDocumentCancel = True Then
                    Dim QryPost As String = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select status from TSPL_CSA_TRANSFER_HEAD where doc_Code='" & txtDocNo.Value & "'"))

                    If clsCommon.myCBool(QryPost) = True Then
                        If ShowDocumentCancel = True Then
                            btnCancel.Visible = True
                            If obj.CancelFlag = True Then
                                btnReverse.Enabled = False
                            Else
                                btnReverse.Enabled = True
                            End If
                        End If
                    End If
                    Dim QryCancel As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CancelFlag from TSPL_CSA_TRANSFER_HEAD where doc_Code='" & txtDocNo.Value & "'"))
                    If clsCommon.CompairString(QryCancel, "1") = CompairStringResult.Equal Then
                        btnCancel.Enabled = False
                    Else
                        btnCancel.Enabled = True
                    End If
                    If clsCommon.CompairString(QryCancel, "1") = CompairStringResult.Equal Then
                        UsLock1.Status = ERPTransactionStatus.Cancel
                    End If
                End If


                '=========End ============
                '' currencyconversion
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  currencyconversion
                UcAttachment1.LoadData(obj.DOC_CODE)
            Else
                AddNew()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
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
            If True Then
                Dim tax_category As String = clsCSATransfer.GetState_Inter_Local(txtCustCode.Value, txtFromLocation.Value)

                'Dim qry As String = "select TSPL_LOCATION_WISE_TAX_MASTER.tax_code as [Rate Code],TSPL_TAX_MASTER.Tax_code_Desc as [Rate Description],TSPL_LOCATION_WISE_TAX_MASTER.Tax_Rate as [Rate] from TSPL_LOCATION_WISE_TAX_MASTER left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.tax_code=TSPL_LOCATION_WISE_TAX_MASTER.tax_code "
                'Dim whrcls As String = " TSPL_LOCATION_WISE_TAX_MASTER.tax_category='" + tax_category + "' and TSPL_LOCATION_WISE_TAX_MASTER.tax_group_code='" + txtTaxGroup.Value + "' and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Type='S' and TSPL_LOCATION_WISE_TAX_MASTER.location_code='" + txtFromLocation.Value + "'"
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(txtFromLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtCustCode.Value, "S", OpenALLTaxes) 'clsCommon.myCdbl(clsCommon.ShowSelectForm("CSATRANSFND", qry, "Rate", whrcls, "", "", True))
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
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing

            Dim desc As String = ""

            If clsCommon.myLen(txtTransporter_Code.Value) <= 0 AndAlso chkownvehicle.Checked = False Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtTransporter_Code.Focus()
                txtTransporter_Code.Select()
                clsCommon.MyMessageBoxShow(Me, "Select transporter detail.", Me.Text)
                ErrorControl.SetError(txtTransporter_desc, "Select transporter detail.")
                Exit Sub
            Else
                ErrorControl.ResetError(txtTransporter_desc)
            End If

            If clsCommon.myLen(txtTransporter_Code.Value) > 0 AndAlso clsCommon.myLen(txtvehicle_code.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtvehicle_code.Focus()
                txtvehicle_code.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill vehicle no. for provision booking.", Me.Text)
                ErrorControl.SetError(txtvehicle_code, "Fill vehicle no. for provision booking.")
                Exit Sub
            Else
                ErrorControl.ResetError(txtvehicle_code)
            End If

            If clsCommon.myLen(txtTransporter_Code.Value) > 0 AndAlso clsCommon.myCdbl(txtVehicle_Capacity.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtVehicle_Capacity.Focus()
                txtVehicle_Capacity.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill vehicle capacity for provision booking.", Me.Text)
                ErrorControl.SetError(txtVehicle_Capacity, "Fill vehicle capacity for provision booking.")
                Exit Sub
            Else
                ErrorControl.ResetError(txtVehicle_Capacity)
            End If

            If clsCommon.myLen(txtTransporter_Code.Value) > 0 AndAlso clsCommon.myCdbl(txtGross_Wt.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtGross_Wt.Focus()
                txtGross_Wt.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill Gross weight for provision booking.", Me.Text)
                ErrorControl.SetError(txtGross_Wt, "Fill Gross weight for provision booking.")
                Exit Sub
            Else
                ErrorControl.ResetError(txtGross_Wt)
            End If

            If (myMessages.postConfirm()) Then
                If SavingData(True) = False Then
                    Exit Sub
                End If

                ''=========================================
                If (clsCSATransfer.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    msg = "Successfully Posted"

                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
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
                    common.clsCommon.MyMessageBoxShow(Me, msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If


                'If clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                '    SMSSENDONLY(True)
                'End If
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

                'clsCommon.ProgressBarShow()
                If (clsCSATransfer.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            ''-------richa 30/07/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            Dim qst As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) = 0 Then
                qst = "select count(*) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + txtDocNo.Value + "'"
            Else
                qst = "select count(*) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + txtDocNo.Value + "' and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ")"

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
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim qry As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------

        qry = "select DOC_CODE as Code,convert(varchar(10),Transfer_Date,103)  as Date,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.Cust_Code as [Customer Code], " & _
        " Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CSA_TRANSFER_HEAD.Document_Amount as Amount, "

        If ShowDocumentCancel = True Then
            qry += " case when TSPL_CSA_TRANSFER_HEAD.CancelFlag=1 then 'Cancel' else case when TSPL_CSA_TRANSFER_HEAD.Status=0 then 'Pending' else 'Approved' end end as [Status],TSPL_CSA_TRANSFER_HEAD.From_Location_Code as [From Location Code],TSPL_CSA_TRANSFER_HEAD.To_Location_Code as [To Location Code], " & _
      " Loc1.Location_Desc as [From Location Name],Loc2.Location_Desc as [To Location Name] from TSPL_CSA_TRANSFER_HEAD  " & _
      " left join TSPL_LOCATION_MASTER as Loc1 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc1.Location_Code" & _
      " left join TSPL_LOCATION_MASTER as Loc2 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc2.Location_Code " & _
      " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code "
        Else
            qry += " case when TSPL_CSA_TRANSFER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_CSA_TRANSFER_HEAD.From_Location_Code as [From Location Code],TSPL_CSA_TRANSFER_HEAD.To_Location_Code as [To Location Code], " & _
      " Loc1.Location_Desc as [From Location Name],Loc2.Location_Desc as [To Location Name] from TSPL_CSA_TRANSFER_HEAD  " & _
      " left join TSPL_LOCATION_MASTER as Loc1 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc1.Location_Code" & _
      " left join TSPL_LOCATION_MASTER as Loc2 on TSPL_CSA_TRANSFER_HEAD.From_Location_Code=Loc2.Location_Code " & _
      " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code "
        End If

        Dim whrClas As String = ""
        '-------richa 30/07/2014 Ticket No. BM00000003242---------

        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas = " From_Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If clsCommon.myLen(arrLoc) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " From_Location_Code in (" + arrLoc + ") and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " From_Location_Code in (" + arrLoc + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        LoadData(clsCommon.ShowSelectForm("TrnsferCSAFndd", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        'btnReverse.Enabled = False
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
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
                'SelectRequistionItems()
            ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.T Then
                chkRateDefaultSetting.Visible = Not chkRateDefaultSetting.Visible
                chkRateUserCustomer.Visible = Not chkRateUserCustomer.Visible
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    If ShowDocumentCancel = True AndAlso UsLock1.Status = ERPTransactionStatus.Cancel Then
                        btnReverse.Visible = False
                        clsCommon.MyMessageBoxShow(Me, "No Reverse Document Apply", Me.Text)
                    Else
                        btnReverse.Visible = True
                    End If

                End If
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.E Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    txtCustCode.Enabled = True
                    btnAmendment.Enabled = True
                    btnAmendment.Visible = True

                End If
            End If
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colAltUnitCOde) Then
                isCellValueChangedOpen = True
                OpenUOMList(True)
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        txtTermCode.Value = ClsReceivablePaymentTerms.getFinderWithSaleType(txtTermCode.Value, "C", isButtonClicked)
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
        'Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("SNSOTaxGroupfndd", qry, "Code", "Tax_Group_Type='S'", txtTaxGroup.Value, "Code", isButtonClicked)
        Try
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtFromLocation.Value, txtCustCode.Value, "S", txtTaxGroup.Value, isButtonClicked, OpenALLTaxes)
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustCode.Value, txtFromLocation.Value, OpenALLTaxes)
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
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        'isInsideLoadData = True
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustCode.Value, txtFromLocation.Value, OpenALLTaxes)
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

    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustCode.Value, txtFromLocation.Value, OpenALLTaxes)
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

    Private Sub BlankControlOnCustomer()

        txtCustDesc.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        'txtSalesman.Value = ""
        'lblSalesman.Text = ""
        Me.txtCurrencyCode.Value = ""
        Me.txtConversionRate.Text = 1
        Me.txtApplicableFrom.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        'txtPriceCode.Text = ""
        'txtPriceGroupCode.Text = ""
        LoadBlankGrid()
        LoadBlankGridTax()
        'gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustCode._MYValidating
        txtCustCode.Value = clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' and State='" & txtState.Value & "'", txtCustCode.Value, isButtonClicked)

        If clsCommon.myLen(txtCustCode.Value) > 0 Then
            txtCustDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtCustCode.Value + "'"))
        Else
            txtCustDesc.Text = ""
        End If
        'btnHistory.Enabled = True
        'If clsCommon.myLen(txtFromLocation.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Location first")
        '    Exit Sub
        'End If
        'BlankControlOnCustomer()
        ''-------richa 30/07/2014 Ticket No. BM00000003242---------
        'Dim strwherecls As String = ""
        'strwherecls = Xtra.CustomerPermission()
        ''-----------------------------------------------------
        'Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
        'qry += " from TSPL_CUSTOMER_MASTER "
        'qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        'qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        'qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        'qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
        ''-------richa 30/07/2014 Ticket No. BM00000003242---------
        'If clsCommon.myLen(strwherecls) = 0 Then
        '    txtCustCode.Value = clsCommon.ShowSelectForm("SNSOVendorFndr", qry, "Code", "", txtCustCode.Value, "Code", isButtonClicked)
        'Else
        '    txtCustCode.Value = clsCommon.ShowSelectForm("SNSOVendorFndr", qry, "Code", " TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")", txtCustCode.Value, "Code", isButtonClicked)

        'End If
        ''-----------------------------------------------------



        'qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtCustCode.Value + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    txtCustDesc.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
        '    txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Term Code"))
        '    lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Term Description"))
        '    'txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
        '    'lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group Description"))
        '    'txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman Code"))
        '    'lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman"))

        '    txtDate.Enabled = False
        '    'txtVendorNo.Enabled = False
        '    chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtCustCode.Value)
        '    SetMultiCurrencyVisibility()
        'Else
        '    txtCustDesc.Text = ""
        '    txtTermCode.Value = ""
        '    lblTermName.Text = ""
        '    txtTaxGroup.Value = ""
        '    lblTaxGrpName.Text = ""
        '    'txtSalesman.Value = ""
        '    'lblSalesman.Text = ""
        '    Me.txtCurrencyCode.Value = ""
        '    Me.txtConversionRate.Text = 1
        '    Me.txtApplicableFrom.Text = ""
        'End If
        ' '' priti change start here
        'qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
        '"TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
        '"TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
        '"FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(txtFromLocation.Value) + "'"
        'Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        'Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        'Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        'If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal Then
        '    strExcise = True
        'Else
        '    strExcise = False
        'End If
        'If clsCommon.myLen(txtCustCode.Value) > 0 Then
        '    qry = "select Price_Code,price_CodeNon,State,price_group_code from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustCode.Value + "'"
        '    dt = clsDBFuncationality.GetDataTable(qry)

        '    'If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(loc, "Y") = CompairStringResult.Equal Then
        '    '    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
        '    'Else
        '    '    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
        '    'End If

        '    'If clsCommon.myLen(txtPriceCode.Text) = 0 Then
        '    '    txtPriceGroupCode.Text = clsCommon.myCstr(dt.Rows(0)("price_group_code"))
        '    'End If
        '    'txtVendorNo.Enabled = False

        '    If clsCommon.CompairString(clsCommon.myCstr(dtLocation.Rows(0)("State")), clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
        '        txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("LocalTaxGroup"))
        '        lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Local_Tax_GroupName"))
        '    Else
        '        txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("InterstateTaxGroup"))
        '        lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Interstate_Tax_GroupName"))
        '    End If

        'End If


        ' '' priti change ends here

        'SetTax()
        'SetTaxDetails()
        'SetTermDetails()
        ''Dim frm As New frmMandatoryFieldChecker

        ''gv1.Enabled = frm.CheckmandatoryField(Me)

    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating

        Dim WhrCls As String = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then
            WhrCls += " and Excisable ='T' "
        Else
            WhrCls += " and Excisable <> 'T' "
        End If

        txtFromLocation.Value = clsLocation.getFinder(WhrCls, txtFromLocation.Value, isButtonClicked) 'clsCommon.ShowSelectForm("SNSOBILLTOLOCPO", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
        txtFromLocationDesc.Text = clsLocation.GetName(txtFromLocation.Value, Nothing) 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
        'Dim frm As New frmMandatoryFieldChecker
        'gv1.Enabled = frm.CheckmandatoryField(Me)
        txtExcisable.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when excisable='T' then '1' else '0' end) as exciable from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
        cmbEXType.SelectedValue = IIf(txtExcisable.Text = "1", "E", "N")

        SetTax()
    End Sub

    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtToLocation._MYValidating
        If clsCommon.myLen(txtFromLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
            txtFromLocation.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtCustCode.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer first", Me.Text)
            txtCustCode.Focus()
            Exit Sub
        End If


        Dim qry As String = " select TSPL_CSA_TRANSFER_HEAD.Ship_To_Code as [Code],TSPL_CSA_TRANSFER_HEAD.Ship_To_Desc as [Description],TSPL_CSA_TRANSFER_HEAD.Ship_To_Type_Code as[Customer Code] ,TSPL_CSA_TRANSFER_HEAD.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_CSA_TRANSFER_HEAD.Add1,'')='' then '' else TSPL_CSA_TRANSFER_HEAD.add1 +',' end + case when ISNULL (TSPL_CSA_TRANSFER_HEAD.Add2,'')='' then '' else TSPL_CSA_TRANSFER_HEAD.add2 +',' end + case when ISNULL (TSPL_CSA_TRANSFER_HEAD.Add3,'')='' then '' else TSPL_CSA_TRANSFER_HEAD.add3 +',' end + case when ISNULL (TSPL_CSA_TRANSFER_HEAD.Add4,'')='' then '' else TSPL_CSA_TRANSFER_HEAD.add4 +',' end ,',,',',') as [Ship to Address],TSPL_CSA_TRANSFER_HEAD.CST_No as [CST NO],TSPL_CSA_TRANSFER_HEAD.Tin_No as [TIN No]  from TSPL_CSA_TRANSFER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Ship_To_Type_Code "
        txtToLocation.Value = clsCommon.ShowSelectForm("ShipmentShindrlter", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtCustCode.Value) & "' and loc_code='" & clsCommon.myCstr(txtFromLocation.Value) & "'", txtToLocation.Value, "Code", isButtonClicked)
        'txtShipToLocation.Value = clsShipToLocation.getFinder("Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and loc_code='" & clsCommon.myCstr(txtBillToLocation.Value) & "'", txtShipToLocation.Value, isButtonClicked)
        txtToLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_CSA_TRANSFER_HEAD where Ship_To_Code='" + txtToLocation.Value + "'"))
    End Sub

    'Sub SelectRequistionItems()
    '    isInsideLoadData = True
    '    Dim frm As New frmPendingSaleQuotation()
    '    frm.VendorCode = txtVendorNo.Value
    '    frm.strCurrCode = txtDocNo.Value
    '    frm.ShowDialog()
    '    LoadBlankGrid()
    '    Dim objOrderHead As clsSNSalesQuotationHead
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            objOrderHead = clsSNSalesQuotationHead.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current)
    '            If objOrderHead IsNot Nothing AndAlso clsCommon.myLen(objOrderHead.DOC_CODE) > 0 Then
    '                '' currency details
    '                txtCurrencyCode.Value = objOrderHead.CURRENCY_CODE
    '                Me.txtConversionRate.Text = objOrderHead.ConvRate
    '                If objOrderHead.ApplicableFrom IsNot Nothing Then
    '                    Me.txtApplicableFrom.Text = objOrderHead.ApplicableFrom
    '                End If
    '                'If clsCommon.myLen(txtRefNo.Text) <= 0 Then
    '                '    txtRefNo.Text = objOrderHead.Ref_No
    '                'End If
    '                If clsCommon.myLen(txtDesc.Text) <= 0 Then
    '                    txtDesc.Text = objOrderHead.Description
    '                End If
    '                'If clsCommon.myLen(txtPONo.Text) <= 0 Then
    '                '    txtPONo.Text = objOrderHead.Cust_PO_No
    '                'End If

    '                If (clsCommon.myLen(txtShipToLocation.Value)) <= 0 Then
    '                    txtShipToLocation.Value = objOrderHead.ShipToLocationName
    '                End If
    '                If (clsCommon.myLen(cboItemType.SelectedValue) <= 0) Then
    '                    cboItemType.SelectedValue = objOrderHead.Item_Type
    '                End If
    '                If (clsCommon.myLen(txtDept.Value) <= 0) Then
    '                    txtDept.Value = objOrderHead.Dept
    '                    lblDept.Text = objOrderHead.Dept_Desc
    '                End If
    '                If (clsCommon.myLen(txtBillToLocation.Value) <= 0) Then
    '                    txtBillToLocation.Value = objOrderHead.From_Location_Code
    '                    lblBillToLocation.Text = objOrderHead.BillToLocationName
    '                End If
    '                If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
    '                    txtVendorNo.Value = frm.VendorCode
    '                    lblVendorName.Text = frm.VendorName
    '                End If
    '                If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
    '                    txtTermCode.Value = objOrderHead.Terms_Code
    '                    lblTermName.Text = objOrderHead.TermsName
    '                    txtDueDate.Value = objOrderHead.Due_Date
    '                End If

    '                'If (clsCommon.myLen(txtRouteNo.Value)) <= 0 Then
    '                '    txtRouteNo.Value = objOrderHead.Route_No
    '                '    lblRouteDesc.Text = objOrderHead.Route_Desc
    '                'End If
    '                'If (clsCommon.myLen(txtPriceCode.Text)) <= 0 Then
    '                '    txtPriceCode.Text = objOrderHead.Price_Code
    '                'End If
    '                'If (clsCommon.myLen(txtPriceGroupCode.Text)) <= 0 Then
    '                '    txtPriceGroupCode.Text = objOrderHead.Price_Group_Code
    '                'End If
    '                If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
    '                    txtTaxGroup.Value = objOrderHead.Tax_Group
    '                    SetTaxDetails()
    '                End If

    '                'If clsCommon.myLen(txtSalesman.Value) <= 0 Then
    '                '    txtSalesman.Value = objOrderHead.Salesman_Code
    '                '    lblSalesman.Text = objOrderHead.Salesman_Name
    '                'End If
    '                'If (clsCommon.myLen(fndProject.Value) <= 0) Then
    '                '    fndProject.Value = objOrderHead.PROJECT_ID
    '                '    lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    '                '    fndProject.Enabled = False
    '                '    lblProject.Enabled = False
    '                'End If
    '                LoadBlankGridAC()

    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code1) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code1
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name1
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt1
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code2) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code2
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name2
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt2
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code3) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code3
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name3
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt3
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code4) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code4
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name4
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt4
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code5) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code5
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name5
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt5
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code6) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code6
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name6
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt6
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code7) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code7
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name7
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt7
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code8) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code8
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name8
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt8
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code9) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code9
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name9
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt9
    '                End If
    '                If (clsCommon.myLen(objOrderHead.Add_Charge_Code10) > 0) Then
    '                    gvAC.Rows.AddNew()
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objOrderHead.Add_Charge_Code10
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objOrderHead.Add_Charge_Name10
    '                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objOrderHead.Add_Charge_Amt10
    '                End If
    '                gvAC.Rows.AddNew()


    '            End If

    '        End If
    '        If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
    '            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
    '        End If


    '        Dim mrnno As String

    '        Dim arr As New List(Of String)
    '        For ii As Integer = 0 To frm.ArrReturn.Count - 1
    '            If clsCommon.myLen(frm.ArrReturn(ii).DOC_CODE) > 0 Then
    '                Dim strCode As String = frm.ArrReturn(ii).DOC_CODE
    '                'If Not arr.Contains(strCode) Then
    '                '    arr.Add(strCode)
    '                objOrderHead = clsSNSalesQuotationHead.GetData(frm.ArrReturn(ii).DOC_CODE, NavigatorType.Current)
    '                For Each obj As clsSNSalesQuotationDetail In objOrderHead.Arr
    '                    If (obj.Item_Code = frm.ArrReturn(ii).Item_Code AndAlso obj.Scheme_Item = "N") OrElse (obj.Scheme_Code = frm.ArrReturn(ii).Scheme_Code AndAlso clsCommon.myLen(obj.Scheme_Code) > 0) Then
    '                        If IsValidItem(obj) Then
    '                            gv1.Rows.AddNew()
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = obj.DOC_CODE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = obj.Calc_Type
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc

    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Unit_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code


    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty


    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = obj.TAX1_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = obj.TAX2_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = obj.TAX3_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = obj.TAX4_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = obj.TAX5_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = obj.TAX6_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = obj.TAX7_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = obj.TAX8_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = obj.TAX9_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = obj.TAX10_Rate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
    '                            ''gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableRate).Value = obj.Assessable
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = obj.Bin_No
    '                            'Dim dt As DataTable = clsSNShipmentHead.GetOriginalQty(obj.DOC_CODE, obj.Item_Code, obj.Unit_code, obj.Assessable, obj.MRP, Nothing)
    '                            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSOQty).Value = clsCommon.myCdbl(dt.Rows(0)("MRN_Qty"))
    '                            'End If
    '                            'If obj.MFG_Date.HasValue Then
    '                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = obj.MFG_Date
    '                            'End If
    '                            'If obj.Expiry_Date.HasValue Then
    '                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = obj.Expiry_Date
    '                            'End If
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = IIf(obj.Scheme_Applicable = "Y", "Yes", "No")
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = obj.Scheme_Code
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = IIf(obj.Scheme_Item = "Y", "Yes", "No")
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = obj.Item_Tax
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalMRP).Value = obj.Total_MRP_Amt
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = obj.Total_Basic_Amt
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalDiscountAmount).Value = obj.Total_Disc_Amt
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colcustDiscount).Value = obj.Cust_Discount
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCustDiscount).Value = obj.Total_Cust_Discount
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = obj.ActualRate
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColCustDiscountQty).Value = obj.Cust_DiscountQty
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = obj.Price_Date
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = obj.Price_code
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = obj.Abatement_Per
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementAmount).Value = obj.Abatement_Amt
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = obj.FOC_Item
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value), clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value))
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = obj.Item_Weight
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = obj.Conv_Factor
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotItemWt).Value = obj.TotalItem_Weight

    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkupOn).Value = obj.Markup_On
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colMarkUpPercentage).Value = obj.Markup_Percent
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLandingCost).Value = obj.Landing_Cost
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDiscPercentage).Value = obj.CustDiscPer
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHeadDiscamt).Value = obj.HeadDiscAmt
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colCashDiscSchemeCode).Value = obj.CasdDiscScheme_Code
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colPurCost).Value = obj.Purchase_Cost
    '                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = obj.OrgRate
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleCode).Value = obj.PrincipleCode
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPricipleDesc).Value = obj.PrincipleDesc
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCust_Code).Value = obj.vendor_code
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCust_Desc).Value = obj.vendor_desc


    '                        End If
    '                    End If

    '                Next
    '                'End If
    '            End If
    '            'mrnno = obj.DOC_CODE
    '        Next


    '        If objOrderHead.Arr IsNot Nothing AndAlso objOrderHead.Arr.Count > 0 Then
    '            For Each objTr As clsSNSalesQuotationDetail In objOrderHead.Arr
    '                If objTr.Calc_Type = "Misc" Then
    '                    gv1.Rows.AddNew()
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colReqistionNo).Value = mrnno
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Calc_Type
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Unit_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate

    '                End If
    '            Next
    '        End If

    '        'gv1.Rows.AddNew()
    '        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem


    '        SetitemWiseTaxSetting(False, False)
    '        For ii As Integer = 0 To gv1.RowCount - 1
    '            UpdateCurrentRow(ii)
    '        Next
    '    End If
    '    isInsideLoadData = False
    '    UpdateAllTotals()
    '    RefreshReqNo()
    'End Sub

    Function IsValidItem(ByVal obj As clsSNSalesQuotationDetail)
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
            'Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            'Dim strSchemeItem As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value)
            'If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.DOC_CODE) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
            '    Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Order No : " + obj.DOC_CODE + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
            '    If dblMRP > 0 Then
            '        strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
            '    End If
            '    common.clsCommon.MyMessageBoxShow(strMsg)
            '    Return False
            'End If
        Next
        Return True
    End Function

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                Dim dblOtherTaxAmt As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colOther_Chrage).Value)
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colTaxCalcType).Value), "Backward") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
                        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAbatementAmt).Value) - (dblOtherTaxAmt * 2) ''because in back calc. other charge is minus and in common form other charge is always '+' ,so minus twice from here and other charge add from po form get equal result
                    Else
                        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalBasicAmount).Value) - (dblOtherTaxAmt * 2)
                    End If

                Else
                    If clsCommon.CompairString(cmbEXType.SelectedValue, "E") = CompairStringResult.Equal Then ''if excisable then
                        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAbatementAmt).Value)
                    Else
                        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalBasicAmount).Value)
                    End If

                End If

                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = txtFromLocation.Value
                frm.strTaxType = "S"
                frm.Without_State_Condition = OpenALLTaxes
                frm.strVendorCustomerCode = txtCustCode.Value

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
                'ElseIf (gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso (UsLock1.Status = ERPTransactionStatus.Approved)) Then
                '    Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                '    Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                '    Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                '    If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                '        If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '            If clsCSATransferDetail.CompletePO(txtDocNo.Value, strICode, intSNo) Then
                '                common.clsCommon.MyMessageBoxShow("Successfully Completed")
                '                LoadData(txtDocNo.Value, NavigatorType.Current)
                '            End If
                '        End If
                '    End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "CSA Transfer No not found to Print", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funPrint(txtDocNo.Value)
        End If
    End Sub

    Public Sub funPrint(ByVal StrCode As String, Optional ByVal strDepPrint As String = Nothing)

        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.myLen(StrCode) > 0 Then
                Dim PrintType As String = ""
                Dim strQuery As String
                Dim dt As DataTable
                PrintType = "Select Item_Tax_Type from TSPL_CSA_TRANSFER_HEAD WHERE DOC_CODE='" + StrCode + "'"
                PrintType = clsDBFuncationality.getSingleValue(PrintType)
                If Not PrintType = "2" Then
                    Dim dtBarCode As New DataTable

                    dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
                    Dim bytes() As Byte
                    Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(StrCode, 1, False).[GetType]())
                    bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(StrCode, 1, False), GetType(Byte())), Byte())

                    '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
                    Dim QryShowStatus As String = ""
                    Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
                    If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
                        QryShowStatus = " ,(case when TSPL_CSA_TRANSFER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as SOStatus "
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
                    Qry = " select TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_CSA_TRANSFER_HEAD.EwayBillNo,Convert(varchar(15),TSPL_CSA_TRANSFER_HEAD.EwayBillDate,103) as EwaBillDate,TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_ITEM_MASTER.HSN_Code,isnull(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0) as Disc_Amt," & _
                        " TSPL_LOCATION_MASTER.GSTNO as GSTIN_NO,from_location_state.GST_STATE_CODE AS From_GST_StateCode,tspl_customer_master.GSTNO as To_Loc_GstinNo,StateMaster_Customer.GST_STATE_Code as To_Loc_GSTStateCode,  STUFF((SELECT DISTINCT(',' + cst.DELEVERY_ORDER_NO) FROM TSPL_CSA_TRANSFER_DETAIL as cst WHERE cst.DOC_CODE  =TSPL_CSA_TRANSFER_HEAD .DOC_CODE FOR XML PATH('')), 1, 1, '') AS csaDespatchAdviceNo," & _
                    " STUFF((SELECT DISTINCT(',' + convert(varchar,dah.Doc_Date,103)) FROM TSPL_CSA_DO_HEAD as dah WHERE dah.Doc_No in ( select ct.DELEVERY_ORDER_NO from TSPL_CSA_TRANSFER_DETAIL  ct where ct.DOC_CODE  = TSPL_CSA_TRANSFER_HEAD .DOC_CODE)   FOR XML PATH('')), 1, 1, '') AS csaDespatchAdviceDate,LEFT(CAST(TSPL_CSA_TRANSFER_HEAD.Modify_Date AS TIME ),5) AS TIMEOFPREPARATION, " & _
                        " TSPL_COMPANY_MASTER.Insurance_Comp_Name ,TSPL_COMPANY_MASTER.Insurance_No ,convert(varchar,TSPL_COMPANY_MASTER.Insurance_Valid_Date ,103) AS Insurance_Valid_Date,TSPL_CSA_TRANSFER_HEAD.Total_Item_Wt ,TSPL_CSA_TRANSFER_HEAD.Gross_Item_Wt ,"
                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += "  TSPL_BATCH_ITEM.Batch_No, "
                    End If
                    ''--------------
                    Qry += " convert(varchar,TSPL_CSA_TRANSFER_HEAD.Removal_Date ,103) AS Removal_Date,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_COMPANY_MASTER.CINNo ,TSPL_COMPANY_MASTER.Tcan_No as website,TSPL_COMPANY_MASTER.Pan_No ,TSPL_COMPANY_MASTER.Access_Officer ,convert(varchar,TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) as TinNo_Issue_Date,convert(varchar,TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) PanNo_Issue_Date , " & _
                        " tspl_customer_master.PAN as To_Location_Pan,from_location_state.state_name as from_state_Name,from_location_state.State_code as Loc_state_code, TSPL_LOCATION_MASTER.hoAdd1,TSPL_LOCATION_MASTER.hoadd2 , TSPL_CSA_TRANSFER_HEAD.Vehicle_Id,tspl_customer_master.City_Code,case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as Transporter_Name,TSPL_CSA_TRANSFER_HEAD.WayBill_No ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.WayBill_Date,103)as WayBill_Date, cast(convert(decimal(18,0),(tspl_csa_transfer_detail.qty * tspl_item_uom_detail.conversion_factor) / alt_convrsn.conversion_factor) as varchar)+' '+TSPL_CSA_TRANSFER_DETAIL.alt_unit_code as alt_unit_code,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name as comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax ,TSPL_COMPANY_MASTER.Email as comp_email ,TSPL_COMPANY_MASTER.Pincode as copm_pincode ,TSPL_COMPANY_MASTER.State as comp_state ,TSPL_COMPANY_MASTER.Tin_No  as copm_TinNo, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_CSA_TRANSFER_DETAIL.Item_Code , case when isnull(TSPL_CSA_TRANSFER_DETAIL.FOC ,'')='Y' then TSPL_ITEM_MASTER.Item_Desc+'(Free Scheme)' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc  ,tspl_item_master.Is_Batch_Item,"

                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += " case when isnull(TSPL_BATCH_ITEM.Batch_No,'')<>'' then TSPL_BATCH_ITEM.qty else TSPL_CSA_TRANSFER_DETAIL.Qty end as Qty,TSPL_CSA_TRANSFER_DETAIL.FOC, "
                    Else
                        Qry += " TSPL_CSA_TRANSFER_DETAIL.Qty, "
                    End If
                    ''--------------
                    Qry += " TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Unit_Rate,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as 'STN_No' ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as [Date_N_Time_issue],TSPL_CSA_TRANSFER_HEAD.Document_Amount,tspl_customer_master.cust_code AS To_Location_Code,TSPL_LOCATION_MASTER.add1 as transpotor,tspl_customer_master.Add1 AS To_Location_Add1, tspl_customer_master.Add2 as To_Location_Add2 ,tspl_customer_master.Add3 as To_Location_Add3, tspl_customer_master.customer_name as To_Location_Desc ,tspl_customer_master.City_Code as To_Location_City_Code , StateMaster_Customer.State_Name as To_Location_State, tspl_customer_master.pin_no as To_Location_Pin_Code,  tspl_customer_master.Country as To_Location_Country, case when ISNULL(tspl_customer_master.Phone1,'')='(+__)__________' then '' else tspl_customer_master.Phone1 end +  Case When   ISNULL(tspl_customer_master.Phone2,'')<>'(+__)__________' Then ', '+ tspl_customer_master.Phone2 Else'' End  as To_Location_Telphone, tspl_customer_master.Email as To_Location_Email ,  tspl_customer_master .TIN_No as to_location_tin_no, tspl_customer_master .CST as to_location_cstno , TSPL_LOCATION_MASTER.Location_Code as From_Location_Code,  TSPL_LOCATION_MASTER.Location_Desc as From_Location_Dec,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 , TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 , TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,TSPL_LOCATION_MASTER.Add4  as From_Location_Add4,TSPL_LOCATION_MASTER.City_Code as From_Location_City_Code ,  TSPL_LOCATION_MASTER.State as From_Location_State,TSPL_LOCATION_MASTER.Pin_Code as From_Location_Pin_Code ,TSPL_LOCATION_MASTER.Country as From_Location_Country,  case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End   as From_Location_Telphone,TSPL_LOCATION_MASTER.Email as From_Location_Email,TSPL_LOCATION_MASTER.TIN_No AS From_Location_tin_no, TSPL_LOCATION_MASTER.CST_No AS From_Location_cstNo,TSPL_ITEM_MASTER.Weight_UOM as UOM2,"
                    Qry += " TSPL_ITEM_MASTER.Weight_Value as Weight, "
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
                        Qry += "   case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From' end  as Against_Form ,"
                    Else
                        Qry += "   case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From Due' end  as Against_Form ,"
                    End If


                    Qry += "  TSPL_CSA_TRANSFER_DETAIL.Commission_Chrage, TSPL_CSA_TRANSFER_DETAIL.Other_Chrage, Transfer_Rate, "
                    ''added by richa agarwal
                    Qry += " TSPL_CSA_TRANSFER_HEAD.GR_No as GRNO ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as grdate,case when isnull( TSPL_CSA_TRANSFER_HEAD.Against_Form,'')='F' then 'F' else '' end as Termsofdelivery, "
                    Qry += " tspl_customer_master.City_Code as consigneeaddress,tspl_city_master.city_name as ToLocCityName,TSPL_CSA_TRANSFER_HEAD.document_type,TSPL_CSA_TRANSFER_HEAD.Description "

                    '============Sanjeet(GST 09/06/2017)======================
                    Qry += ", tax1.Tax_Code_Desc as tax1name,isnull (TSPL_CSA_TRANSFER_HEAD.TAX1_Amt,0) as txt1amt,tax1.Type as tax1Type , " & _
                        " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_CSA_TRANSFER_HEAD.tax2_amt,0) as txt2amt, tax2.Type as tax2Type, " & _
                      " tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_CSA_TRANSFER_HEAD.tax3_amt,0) as txt3amt,tax3.Type as tax3Type,  " & _
                     " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_CSA_TRANSFER_HEAD.tax4_amt,0) as txt4amt,tax4.Type as tax4Type,  " & _
                     " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_CSA_TRANSFER_HEAD.tax5_amt,0) as txt5amt, tax5.Type as tax5Type,  " & _
                      " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_CSA_TRANSFER_HEAD.tax6_amt,0) as txt6amt,tax6.Type as tax6Type,  " & _
                     "tax7.Tax_Code_Desc as tax7name, isnull (TSPL_CSA_TRANSFER_HEAD.tax7_amt,0) as txt7amt, tax7.Type as tax7Type,  " & _
                     "tax8.Tax_Code_Desc as tax8name, isnull (TSPL_CSA_TRANSFER_HEAD.tax8_amt,0) as txt8amt, tax8.Type as tax8Type, " & _
                     "tax9.Tax_Code_Desc as tax9name, isnull (TSPL_CSA_TRANSFER_HEAD.tax9_amt,0) as txt9amt, tax9.Type as tax9Type,  " & _
                     "tax10.Tax_Code_Desc as tax10name, isnull (TSPL_CSA_TRANSFER_HEAD.tax10_amt,0) as txt10amt, tax10.Type as tax10Type, " & _
                     " isnull(tspl_csa_transfer_detail.TAX1_Amt,0) as DTax1_Amt, isnull(tspl_csa_transfer_detail.TAX2_Amt,0) as DTax2_Amt," & _
              "isnull(tspl_csa_transfer_detail.TAX3_Amt,0) as DTax3_Amt, isnull(tspl_csa_transfer_detail.TAX4_Amt,0) as DTax4_Amt," & _
              " isnull(tspl_csa_transfer_detail.TAX5_Amt,0) as DTax5_Amt, isnull(tspl_csa_transfer_detail.TAX6_Amt,0) as DTax6_Amt," & _
               " isnull(tspl_csa_transfer_detail.TAX7_Amt,0) as DTax7_Amt, isnull(tspl_csa_transfer_detail.TAX8_Amt,0) as DTax8_Amt," & _
                " isnull(tspl_csa_transfer_detail.TAX9_Amt,0) as DTax9_Amt, isnull(tspl_csa_transfer_detail.TAX10_Amt,0) as DTax10_Amt," & _
            " tspl_csa_transfer_detail.TAX1_Rate as DTax1_Rate,tspl_csa_transfer_detail.TAX2_Rate as DTax2_Rate," & _
                " tspl_csa_transfer_detail.TAX3_Rate as DTax3_Rate,tspl_csa_transfer_detail.TAX4_Rate as DTax4_Rate," & _
                    " tspl_csa_transfer_detail.TAX5_Rate as DTax5_Rate,tspl_csa_transfer_detail.TAX6_Rate as DTax6_Rate," & _
                        " tspl_csa_transfer_detail.TAX7_Rate as DTax7_Rate,tspl_csa_transfer_detail.TAX8_Rate as DTax8_Rate," & _
                            " tspl_csa_transfer_detail.TAX9_Rate as DTax9_Rate,tspl_csa_transfer_detail.TAX10_Rate as DTax10_Rate," & _
                     "TSPL_CSA_TRANSFER_HEAD.TAX1_Rate ,TSPL_CSA_TRANSFER_HEAD.TAX2_Rate ,TSPL_CSA_TRANSFER_HEAD.TAX3_Rate,TSPL_CSA_TRANSFER_HEAD.TAX4_Rate,TSPL_CSA_TRANSFER_HEAD.TAX5_Rate,TSPL_CSA_TRANSFER_HEAD.TAX6_Rate,TSPL_CSA_TRANSFER_HEAD.TAX7_Rate,TSPL_CSA_TRANSFER_HEAD.TAX8_Rate,TSPL_CSA_TRANSFER_HEAD.TAX9_Rate,TSPL_CSA_TRANSFER_HEAD.TAX10_Rate, " & _
                     "TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name1,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name2,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name3,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name4,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name5,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name6,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name7,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name8,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name9,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name10,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10   "
                    '========================================================

                    Qry += " from TSPL_CSA_TRANSFER_DETAIL"
                    ''-------------------------------
                    Qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE "
                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += " left outer join TSPL_BATCH_ITEM on TSPL_BATCH_ITEM.Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code and TSPL_CSA_TRANSFER_DETAIL.Line_No =TSPL_BATCH_ITEM.Parent_Line_No and TSPL_BATCH_ITEM.Document_Code =TSPL_CSA_TRANSFER_HEAD.DOC_CODE and TSPL_BATCH_ITEM .In_Out_Type ='O' "
                    End If
                    ''--------------
                    Qry += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code "
                    Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code "
                    Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
                        Qry += "  left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.cust_code "
                    Else
                        Qry += "  left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.To_Location_Code "
                    End If
                    Qry += " left outer join tspl_state_master as from_location_state on from_location_state.state_code=TSPL_LOCATION_MASTER.state"
                    Qry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_csa_transfer_detail.item_code and tspl_item_uom_detail.uom_code=tspl_csa_transfer_detail.unit_code "
                    Qry += " left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=tspl_csa_transfer_detail.item_code and alt_convrsn.uom_code=tspl_csa_transfer_detail.alt_unit_code "
                    Qry += " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSA_TRANSFER_HEAD.Transport_Id "
                    Qry += " LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "
                    Qry += " LEFT OUTER JOIN tspl_city_master   ON tspl_customer_master.City_Code =tspl_city_master.City_Code "
                    Qry += " left outer join tspl_state_master as From_Loc_State_Name on From_Loc_State_Name.STATE_CODE =TSPL_CSA_TRANSFER_HEAD.STATE_CODE" & _
                        " LEFT OUTER JOIN TSPL_STATE_MASTER StateMaster_Customer on StateMaster_Customer.State_Code=TSPL_CUSTOMER_MASTER.State"
                    '============Sanjeet(GST 09/06/2017)======================
                    Qry += " LEFT JOIN TSPL_TAX_MASTER tax1 ON tax1.Tax_Code=tspl_csa_transfer_detail.TAX1 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax2 ON tax2.Tax_Code=tspl_csa_transfer_detail.TAX2 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax3 ON tax3.Tax_Code=tspl_csa_transfer_detail.TAX3 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax4 ON tax4.Tax_Code=tspl_csa_transfer_detail.TAX4 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax5 ON tax5.Tax_Code=tspl_csa_transfer_detail.TAX5 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax6 ON tax6.Tax_Code=tspl_csa_transfer_detail.TAX6 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax7 ON tax7.Tax_Code=tspl_csa_transfer_detail.TAX7 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax8 ON tax8.Tax_Code=tspl_csa_transfer_detail.TAX8 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax9 ON tax9.Tax_Code=tspl_csa_transfer_detail.TAX9 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax10 ON tax10.Tax_Code=tspl_csa_transfer_detail.TAX10 "
                    '======================================================================
                    ''added by richa agarwal
                    'Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code"
                    'Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State"
                    ''------------------------
                    Qry += " where 1=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" + StrCode + "'"

                    dt = clsDBFuncationality.GetDataTable(Qry)
                    dt.Columns.Add("BarCodeImage", GetType(Byte()))
                    For Each dr As DataRow In dt.Rows
                        dr("BarCodeImage") = bytes
                    Next
                    If dt.Rows.Count > 0 Then
                        'SetItemWiseTax(dt, txtDocNo.Value)
                        'KwalitySalesReportViewer.funreport(dt, "CSATransfer", "CSA Transfer")
                        If clsCommon.CompairString(strDepPrint, "Y") = CompairStringResult.Equal Then
                            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSA_DepoTransfer_Challan", "Depo Transfer", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                        Else
                            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue"))) Then
                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("from_state_Name")), clsCommon.myCstr(dt.Rows(0)("To_Location_State"))) = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dt.Rows(0)("txt1amt")) <> 0 Then
                                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Local_WithMandiTax", "CSA Transfer Local With MandiTax", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                                    Else
                                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Local", "CSA Transfer Local", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                                    End If
                                Else
                                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_InterState", "CSA Transfer Interstate", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                                End If

                            Else
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer", "CSA Transfer", "rptCompanyAddress.rpt")
                            End If

                        End If

                    End If
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
                    strQuery = "Select * from ( select CMTo.City_Name as City_Name,SMFrom.STATE_NAME as Loc_State_Name, SMFrom.state_code as frm_State_code, LMFrom.HOAdd1, LMFrom.HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_HEAD.Transport_Id, TSPL_TRANSPORT_MASTER.Transporter_Name, TSPL_CSA_TRANSFER_HEAD.GR_No, case when coalesce(TSPL_CSA_TRANSFER_HEAD.GR_No,'')='' then null else convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) end as GR_Date," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_No, 0 as Alter_UnitQty, TSPL_CSA_TRANSFER_HEAD.Discount_Amt  as HeadDisc_Amt, 0 as HeadDisc_PerAmt, TSPL_ITEM_MASTER.Cheapter_Heads," + Environment.NewLine & _
" TSPL_CHAPTER_HEAD.Description as Chap_Desc, LMFrom.Registration_Number, '' as Payment_Terms, TSPL_CSA_TRANSFER_HEAD.Modify_By, TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO," + Environment.NewLine & _
" LMFrom.add1 +case when len(LMFrom.add2)>0 then ', '+LMFrom.add2 else '' end +case when LEN(isnull(LMFrom.Add3,''))>0 then ', '+isnull(LMFrom.Add3,'') else ' ' end + case when LEN(CMFrom.City_Name)>0 then ', '+CMFrom .City_Name else ' ' end + case when len(SMFrom.STATE_NAME  )>0 then ', '+ SMFrom.STATE_NAME else ' ' end  + case when len(LMFrom.Pin_Code   )>0 then ', Pin Code - '+ cast(LMFrom.Pin_Code  as varchar)  else ' ' end  + case when len(LMFrom.Tin_No     )>0 then ', Tin No - '+ cast(LMFrom.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(LMFrom.Phone1,''))>0 and LMFrom.Phone1='(+__)__________' then '' else ', Phone'+LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(LMFrom.Email    )>0 then ', Email - '+ LMFrom.Email else '' end  as Location_Address," + Environment.NewLine & _
" LMFrom.CST_No as Loc_CSTNo, LMFrom.Excisable as loc_Excisable,LMFrom.Range_Address as Loc_range_Add,LMFrom.Division_Address  as loc_Division_Address,LMFrom.Commissionerate  as Loc_Commissionerate, '' as Challan_No, '' as Challan_Date, '' as Removal_Date, TSPL_CSA_TRANSFER_HEAD.WayBill_No," + Environment.NewLine & _
" TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(CM_Company.City_Name)>0 then ', '+CM_Company.City_Name else ' ' end + case when len(SMCompany.STATE_NAME  )>0 then ', '+ SMCompany.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No)>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end + case when len(TSPL_COMPANY_MASTER.Pan_No)>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address," + Environment.NewLine & _
" LMFrom .Add1 as Loc_Add1, LMFrom.Add2 as Loc_ADd2, LMFrom.Add3 as Loc_Add3, LMFrom.Pin_Code as Loc_Pin_Code, LMFrom.TIN_No as Loc_TinNo, Case when ISNULL(LMFrom.Phone1,'')='(+__)__________' then '' else LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ LMFrom.Phone2 Else'' End as  Loc_Phn, LMFrom.Email as Loc_Email, 'Excise Invoice' as Invoice_Type," + Environment.NewLine & _
" (case when len(isnull(TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code,''))>0 then round(TSPL_ITEM_UOM_DETAIL .Conversion_Factor * (TSPL_CSA_TRANSFER_DETAIL.Qty)/case when alt_convrsn.Conversion_Factor<=0 then 1 else alt_convrsn.Conversion_Factor end,2) else null end) as Alternet_Qty," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code as Alternate_UOM, 0 as Scheme_Qty, '' as Scheme_Item_UOM, TSPL_CSA_TRANSFER_HEAD.Discount_Base, TSPL_CSA_TRANSFER_HEAD.GR_No as Dis_Doc_No, TSPL_CSA_TRANSFER_HEAD.Description, TSPL_COMPANY_MASTER .State as Comp_State, '' as Buyer_order_no, '' as Buyer_order_date, '' as Terms_of_delivery, TSPL_CSA_TRANSFER_HEAD.DOC_CODE as InvoiceNo, TSPL_CSA_TRANSFER_HEAD.GR_No as GRNo, CONVERT(VARCHAR,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as Date_Time_Invoice, convert(varchar ,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as InvoiceDate, '' as ShipmentNo, 0 as Alt_Qty, TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code as Alt_UOM, '' as ShipmentDate, '' as DeliveryOrderNo, '' as TermCondition, LMFrom.Location_Desc," + Environment.NewLine & _
" TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone, TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode, TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3, '' as P_Add1, '' as P_Add2, '' as P_Add3, '' as P_PinNo, '' as P_CstNo, '' as P_TinNo, '' as P_Email, '' as P_Fax, '' as P_LstNo, '' as P_CustCode, '' as P_Cust_Name, '' as P_City_Name, '' as P_State_Name, '' as P_Cust_Phn,LMTo .PAN as Customer_PAN, LMTo.Cust_Code as Cust_Code, LMTo.Customer_Name as Customer_Name, LMTo.Add1 as Cust_Add1, LMTo.Add2 as Cust_add2, LMTo.Add3 as cust_add3, case when ISNULL(LMTo.Phone1,'')='(+__)__________' then '' else LMTo.Phone1 end +  Case When   ISNULL(LMTo.Phone2,'')<>'(+__)__________' Then ', '+ LMTo.Phone2 Else'' End as Cust_Phn,LMTo.Tin_No  as Cust_TinNo, LMTo.CST as Cust_CSTNo, '' Cust_LSTNo, LMTo.Email as Cust_Email, LMTo.PIN_Code as Cust_PinCode, CMTo.City_Name as Cust_City_Name, '' as Cust_Fax, SMTo.STATE_NAME as Cust_State_Name, TSPL_CSA_TRANSFER_DETAIL.item_code, TSPL_ITEM_MASTER.Item_Desc as itemdesc, TSPL_CSA_TRANSFER_DETAIL.Qty, TSPL_CSA_TRANSFER_DETAIL.mrp, (TSPL_CSA_TRANSFER_DETAIL.Unit_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty) -TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Amount as amount, TSPL_CSA_TRANSFER_DETAIL.unit_code as uom, '' as RATE_UOM, TSPL_CSA_TRANSFER_DETAIl.Unit_Rate as itemcost," + Environment.NewLine & _
" tax1.Tax_Code_Desc as tax1name,isnull (TSPL_CSA_TRANSFER_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name, isnull (TSPL_CSA_TRANSFER_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name, isnull (TSPL_CSA_TRANSFER_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name, isnull (TSPL_CSA_TRANSFER_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name, isnull (TSPL_CSA_TRANSFER_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name, isnull (TSPL_CSA_TRANSFER_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name, isnull (TSPL_CSA_TRANSFER_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_CSA_TRANSFER_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name, isnull (TSPL_CSA_TRANSFER_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_CSA_TRANSFER_HEAD.tax10_amt,0) as txt10amt," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.Disc_Per, isnull(TSPL_CSA_TRANSFER_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_CSA_TRANSFER_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_CSA_TRANSFER_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_CSA_TRANSFER_HEAD.Document_Amount as Total_Amt, '1' as CopyType," + Environment.NewLine & _
" '' as Add_Charge_Name1, 0 as Add_Charge_Amt1, '' as Add_Charge_Name2, 0 as Add_Charge_Amt2, '' as Add_Charge_Name3, 0 as Add_Charge_Amt3, '' as Add_Charge_Name4, 0 as Add_Charge_Amt4, '' as Add_Charge_Name5, 0 as Add_Charge_Amt5, '' as Add_Charge_Name6, 0 as Add_Charge_Amt6, '' as Add_Charge_Name7, 0 as Add_Charge_Amt7, '' as Add_Charge_Name8, 0 as Add_Charge_Amt8, '' as Add_Charge_Name9, 0 as Add_Charge_Amt9, '' as Add_Charge_Name10, 0 as Add_Charge_Amt10" + Environment.NewLine & _
                " from TSPL_CSA_TRANSFER_DETAIL" + Environment.NewLine & _
" join TSPL_CSA_TRANSFER_HEAD  on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_CSA_TRANSFER_DETAIL.iTEM_CODE" + Environment.NewLine & _
" left outer join TSPL_LOCATION_MASTER LMFrom on LMFrom.Location_Code=  TSPL_CSA_TRANSFER_HEAD.From_Location_Code" + Environment.NewLine & _
" left outer join TSPL_CUSTOMER_MASTER LMTo on LMTo.Cust_Code =  TSPL_CSA_TRANSFER_HEAD.Cust_Code" + Environment.NewLine & _
" Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CM_Company ON CM_Company.City_Code =TSPL_COMPANY_MASTER.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CMFrom ON CMFrom.City_Code =LMFrom.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CMTo ON CMTo.City_Code =LMTo.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER AS SMCompany  ON SMCompany.STATE_CODE  =TSPL_COMPANY_MASTER.State" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER SMFrom on SMFrom.STATE_CODE=LMFrom.State" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER SMTo ON SMTo.State_Code=LMTo.State" + Environment.NewLine & _
" left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code and tspl_item_uom_detail.uom_code=TSPL_CSA_TRANSFER_DETAIL.unit_code" + Environment.NewLine & _
" left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code and alt_convrsn.uom_code=TSPL_CSA_TRANSFER_DETAIL.alt_unit_code" + Environment.NewLine & _
" left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.transport_id=TSPL_CSA_TRANSFER_HEAD.transport_id" + Environment.NewLine & _
" left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_CSA_TRANSFER_HEAD.tax1" + Environment.NewLine & _
" left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_CSA_TRANSFER_HEAD.tax2" + Environment.NewLine & _
" left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_CSA_TRANSFER_HEAD.TAX3" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_CSA_TRANSFER_HEAD.tax4" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_CSA_TRANSFER_HEAD.tax5" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX6" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX7" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX8" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX9" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX10" + Environment.NewLine & _
" where 2=2 and  TSPL_CSA_TRANSFER_HEAD. DOC_CODE = '" & StrCode & "'" + Environment.NewLine & _
" ) XXX" + Environment.NewLine & _
" LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
                    dt = clsDBFuncationality.GetDataTable(strQuery)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Data found to print")
                    Else
                        Dim Qry2 As String = "select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as InvoiceNo, Abatement_Amt, TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc + '( MRP : ' +   ISNULL(convert(varchar,TSPL_CSA_TRANSFER_DETAIL.MRP),'') + '  Abatement : ' + convert(varchar,convert(int,100- Abatement_Pers)) + '%)' as Item_Desc," + Environment.NewLine & _
                    " TSPL_CSA_TRANSFER_DETAIL.TAX1, TSPL_CSA_TRANSFER_DETAIL.TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2, TSPL_CSA_TRANSFER_DETAIL.TAX2_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3 ,TSPL_CSA_TRANSFER_DETAIL.TAX3_Amt ,TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate," + Environment.NewLine & _
                    " TSPL_ITEM_MASTER.Cheapter_Heads, tax1.Tax_Code_Desc as tax1name,tax2.Tax_Code_Desc as tax2name,tax3.Tax_Code_Desc as tax3name " + Environment.NewLine & _
                        " from TSPL_CSA_TRANSFER_DETAIL" + Environment.NewLine & _
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code" + Environment.NewLine & _
                    " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_CSA_TRANSFER_DETAIL.tax1" + Environment.NewLine & _
                    " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_CSA_TRANSFER_DETAIL.tax2" + Environment.NewLine & _
                    " left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_CSA_TRANSFER_DETAIL .TAX3" + Environment.NewLine & _
                    " LEFT OUTER JOIN TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE" + Environment.NewLine & _
                    " where TSPL_CSA_TRANSFER_HEAD.DOC_CODE ='" & StrCode & "'"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptProductExciseTransfer", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
                    End If
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub BtnPrintChallan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintChallan.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "CSA Transfer No not found to Print", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funChallanPrint()
        End If
    End Sub

    Private Sub funChallanPrint()

        Try
            'clsCommon.ProgressBarShow()

            Dim dtBarCode As New DataTable

            dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
            Dim bytes() As Byte
            Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False).[GetType]())
            bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False), GetType(Byte())), Byte())

            '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
            Dim QryShowStatus As String = ""
            Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
            If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
                QryShowStatus = " ,(case when TSPL_CSA_TRANSFER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as SOStatus "
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
            Qry = " select tspl_customer_master.PAN as To_Location_Pan,TSPL_CUSTOMER_MASTER.GSTNO as To_Location_GSTNo,From_Loc_State_Name.state_name as from_state_Name, From_Loc_State_Name.GST_STATE_Code as From_Gst_StateCode, TSPL_LOCATION_MASTER.hoAdd1,TSPL_LOCATION_MASTER.hoadd2 , TSPL_CSA_TRANSFER_HEAD.Vehicle_Id,tspl_customer_master.City_Code,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_CSA_TRANSFER_HEAD.EWayBillNo as WayBill_No ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.EWayBillDate,103)as WayBill_Date, TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_CSA_TRANSFER_HEAD.EwayBillNo,Convert(varchar(15),TSPL_CSA_TRANSFER_HEAD.EwayBillDate,103) as EwaBillDate ,cast(convert(decimal(18,0),(tspl_csa_transfer_detail.qty * tspl_item_uom_detail.conversion_factor) / alt_convrsn.conversion_factor) as varchar)+' '+TSPL_CSA_TRANSFER_DETAIL.alt_unit_code as alt_unit_code,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name as comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax ,TSPL_COMPANY_MASTER.Email as comp_email ,TSPL_COMPANY_MASTER.Pincode as copm_pincode ,TSPL_COMPANY_MASTER.State as comp_state ,TSPL_COMPANY_MASTER.Tin_No  as copm_TinNo, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_CSA_TRANSFER_DETAIL.Item_Code , case when isnull(TSPL_CSA_TRANSFER_DETAIL.FOC ,'')='Y' then TSPL_ITEM_MASTER.Item_Desc+'(Free Scheme)' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc ,TSPL_CSA_TRANSFER_DETAIL.Qty ,TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Unit_Rate,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as 'STN_No' ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as [Date_N_Time_issue],TSPL_CSA_TRANSFER_HEAD.Document_Amount,tspl_customer_master.cust_code AS To_Location_Code,TSPL_LOCATION_MASTER.add1 as transpotor,tspl_customer_master.Add1 AS To_Location_Add1, tspl_customer_master.Add2 as To_Location_Add2 ,tspl_customer_master.Add3 as To_Location_Add3, tspl_customer_master.customer_name as To_Location_Desc ,tspl_customer_master.City_Code as To_Location_City_Code,tspl_customer_master.State as To_Location_State ,tspl_customer_master.State as To_Location_State, tspl_customer_master.Pin_Code as To_Location_Pin_Code,  tspl_customer_master.Country as To_Location_Country, case when ISNULL(tspl_customer_master.Phone1,'')='(+__)__________' then '' else tspl_customer_master.Phone1 end +  Case When   ISNULL(tspl_customer_master.Phone2,'')<>'(+__)__________' Then ', '+ tspl_customer_master.Phone2 Else'' End  as To_Location_Telphone, tspl_customer_master.Email as To_Location_Email ,  tspl_customer_master .TIN_No as to_location_tin_no,To_Loc_State_Name.GST_STATE_Code as To_LocationStateCode, tspl_customer_master .CST as to_location_cstno  ,TSPL_LOCATION_MASTER.GSTNO as From_location_GSTNo,TSPL_ITEM_MASTER.HSN_Code, TSPL_LOCATION_MASTER.Location_Code as From_Location_Code,  TSPL_LOCATION_MASTER.Location_Desc as From_Location_Dec,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 , TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 , TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,TSPL_LOCATION_MASTER.Add4  as From_Location_Add4,TSPL_LOCATION_MASTER.City_Code as From_Location_City_Code ,  TSPL_LOCATION_MASTER.State as From_Location_State,TSPL_LOCATION_MASTER.Pin_Code as From_Location_Pin_Code ,TSPL_LOCATION_MASTER.Country as From_Location_Country,  case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End   as From_Location_Telphone,TSPL_LOCATION_MASTER.Email as From_Location_Email,TSPL_LOCATION_MASTER.TIN_No AS From_Location_tin_no, TSPL_LOCATION_MASTER.CST_No AS From_Location_cstNo,TSPL_ITEM_MASTER.Weight_UOM as UOM2,"
            Qry += " TSPL_ITEM_MASTER.Weight_Value as Weight, case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From Due' end  as Against_Form ,TSPL_CSA_TRANSFER_DETAIL.Commission_Chrage ,TSPL_CSA_TRANSFER_DETAIL.Other_Chrage ,Transfer_Rate, "
            ''added by richa agarwal
            Qry += " TSPL_CSA_TRANSFER_HEAD.GR_No as GRNO ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as grdate,case when isnull( TSPL_CSA_TRANSFER_HEAD.Against_Form,'')='F' then 'F' else '' end as Termsofdelivery, "
            Qry += " tspl_customer_master.City_Code as consigneeaddress,tspl_city_master.city_name as ToLocCityName,TSPL_CSA_TRANSFER_HEAD.document_type,TSPL_CSA_TRANSFER_HEAD.Description"
            Qry += " from TSPL_CSA_TRANSFER_DETAIL"
            ''-------------------------------
            Qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE "
            Qry += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code "
            Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code "
            Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "
            Qry += "  left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.To_Location_Code "
            Qry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_csa_transfer_detail.item_code and tspl_item_uom_detail.uom_code=tspl_csa_transfer_detail.unit_code "
            Qry += " left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=tspl_csa_transfer_detail.item_code and alt_convrsn.uom_code=tspl_csa_transfer_detail.alt_unit_code "
            Qry += " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSA_TRANSFER_HEAD.Transport_Id "
            Qry += " LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "
            Qry += " LEFT OUTER JOIN tspl_city_master   ON tspl_customer_master.City_Code =tspl_city_master.City_Code "
            Qry += " left outer join tspl_state_master as From_Loc_State_Name on From_Loc_State_Name.STATE_CODE =TSPL_CSA_TRANSFER_HEAD.STATE_CODE"
            Qry += " left outer join tspl_state_master as To_Loc_State_Name on To_Loc_State_Name.STATE_CODE =TSPL_LOCATION_MASTER.State "
            ''added by richa agarwal
            'Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code"
            'Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State"
            ''------------------------
            Qry += " where 1=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" + txtDocNo.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dt.Columns.Add("BarCodeImage", GetType(Byte()))
            For Each dr As DataRow In dt.Rows
                dr("BarCodeImage") = bytes
            Next
            If dt.Rows.Count > 0 Then
                'SetItemWiseTax(dt, txtDocNo.Value)
                'KwalitySalesReportViewer.funreport(dt, "CSATransfer", "CSA Transfer")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Challan", "Challan", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If
            'clsCommon.ProgressBarHide()

        Catch ex As Exception
            'clsCommon.ProgressBarHide()
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
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + strShipFrm + "'   "
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
                            Throw New Exception("Printing Support only 3 Different Rates")
                        End If
                    End If
                Next
            Next
        End If
        Return dtAfterModify
    End Function

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        'UpdateAllTotals()
        'For ii As Integer = 1 To gv1.Rows.Count
        '    gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        'Next
        'RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                e.Cancel = True
                clsCommon.MyMessageBoxShow(Me, "Free item cannot deleted.", Me.Text)
                Exit Sub
            End If

            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colFOC).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)
                'ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                '    gv1.CurrentRow = gv1.Rows(intCurrRow)
                '    gv1.CurrentColumn = gv1.Columns(colDisPer)
                'ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                '    gv1.CurrentRow = gv1.Rows(intCurrRow)
                '    'gv1.CurrentColumn = gv1.Columns(colSpecification)

                '    'ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                '    '    gv1.CurrentRow = gv1.Rows(intCurrRow)
                '    '    gv1.CurrentColumn = gv1.Columns(colRemarks)
                '    'ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                '    '    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                '    '    gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub


    'Sub RefreshReqNo()
    '    txtReqNo.Value = ""
    '    If gv1.Rows.Count > 0 Then
    '        For ii As Integer = 0 To gv1.Rows.Count - 1
    '            Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colReqistionNo).Value)
    '            If clsCommon.myLen(strReqNo) > 0 Then
    '                txtReqNo.Value = strReqNo
    '                Exit Sub
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub
    Sub DoCustAmendment()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select Document No.")
            End If
            Dim obj As clsCSATransfer = clsCSATransfer.GetData(txtDocNo.Value, NavigatorType.Current)
            If clsCommon.CompairString(obj.Cust_Code, txtCustCode.Value) = CompairStringResult.Equal Then
                Throw New Exception("Please change CSA.")
            End If
            '' update cust_code and cust name in object

            obj.Cust_Code = txtCustCode.Value
            obj.Customer_Name = txtCustDesc.Text
            If clsCSATransfer.UpdateCustomerAfterSavePost(obj) Then
                txtCustCode.Enabled = False
                btnAmendment.Visible = False
                clsCommon.MyMessageBoxShow(Me, "CSA Changed Successfully", Me.Text)
            End If
        Catch ex As Exception
            txtCustCode.Enabled = False
            btnAmendment.Visible = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmendment.Click
        '' Done by Panch Raj for work to be done without Request/Ticket for Kwality
        DoCustAmendment()

        'Try
        '    Dim isDoAbandomentNo As Boolean = False
        '    If UsLock1.Status = ERPTransactionStatus.Approved Then
        '        If common.clsCommon.MyMessageBoxShow("Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '            isDoAbandomentNo = True
        '        End If
        '    End If
        '    Dim IsSavedData As Boolean = SaveData(isDoAbandomentNo)
        '    IsSavedData = IsSavedData AndAlso clsCSATransfer.PostData(txtDocNo.Value)

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
            common.clsCommon.MyMessageBoxShow("CSA Transfer No not found to Print")
        Else
            FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
        End If
    End Sub

    Dim i As Integer


    'Private Function GetItemType() As DataTable
    '    Dim dt As New DataTable()
    '    dt.Columns.Add("Code", GetType(String))

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = RowTypeItem
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = RowTypeMisc
    '    dt.Rows.Add(dr)

    '    Return dt
    'End Function

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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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


    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = 0
        UcItemBalance1.LocationCode = txtFromLocation.Value
        UcItemBalance1.LocationName = txtFromLocationDesc.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.CommitedQty = True
        UcItemBalance1.CommitedQtyLbl = True
        UcItemBalance1.ShowCSADOQty = True
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

#Region "New Mail System"
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New FrmMailSMSSettingNew2()
        'frm.FormId = clsUserMgtCode.frmCSATransfer
        frm.ShowDialog()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
        '    txtDocNo.Focus()
        '    txtDocNo.Select()
        '    Return
        'End If

        'attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
        'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)

        'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '    System.Diagnostics.Process.Start(NewSalesReportViewer.Emailreport(dt1, "crptSalesOrderReport", "Sales Order"))
        'End If
    End Sub

    Private Sub btnsend_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsend.Click
        'Try
        '    If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
        '        txtReqNo.Focus()
        '        txtReqNo.Select()
        '        Return
        '    End If

        '    If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
        '        Return
        '    End If
        '    LoadData(txtDocNo.Value, NavigatorType.Current)
        '    Dim lstUsers As New List(Of String)
        '    lstUsers.Add(txtVendorNo.Value)
        '    SendSMSandEmail(lstUsers, False)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        'Try
        '    If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
        '        txtReqNo.Focus()
        '        txtReqNo.Select()
        '        Return
        '    End If

        '    If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
        '        Return
        '    End If
        '    LoadData(txtDocNo.Value, NavigatorType.Current)

        '    Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '    Dim lstUsers As New List(Of String)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        For Each dr As DataRow In dt.Rows
        '            lstUsers.Add(dr("User_Code").ToString())
        '        Next
        '    End If

        '    If lstUsers.Count = 0 Then
        '        Throw New Exception("No Receiptent Found")
        '    End If
        '    SendSMSandEmail(lstUsers, True)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        'Try
        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSATransfer)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Dim strContactPerson As String = ""
        '    Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
        '    strSubject = strSubject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

        '    Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
        '    strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
        '    strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
        '    strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
        '    strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
        '    strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

        '    '------------------------code for attchament-------------------------------------
        '    Dim strRptPath As String = ""
        '    If obj.atchmnt = "Y" Then
        '        attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
        '        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)
        '        If dt1.Rows.Count > 0 Then
        '            SetItemWiseTax(dt1, txtDocNo.Value)
        '            strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptSalesOrderReport", "Sales Order")
        '        End If
        '    End If
        '    '---------------------------------------------------------------------------

        '    For Each strUser As String In lstUsers
        '        'lstUsers.Add(dr("User_Code").ToString())
        '        Dim lstReceiptents As New List(Of String)
        '        Dim qry As String = ""
        '        Dim emailId As String = ""
        '        If isSendForApproval Then
        '            strContactPerson = strUser
        '            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
        '            emailId = clsDBFuncationality.getSingleValue(qry)
        '        Else
        '            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
        '            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
        '        End If

        '        strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
        '        lstReceiptents.Add(emailId)

        '        Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

        '        clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
        '    Next
        '    clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)


        '    If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
        '        SMSSENDONLY(False)
        '    End If


        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try

    End Sub

    Private Sub SMSSENDONLY(ByVal isPost As Boolean)
        'Try
        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSATransfer)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    If clsCommon.myLen(obj.smsbody) <= 0 Then
        '        Return
        '    End If

        '    Dim strContactPerson As String = ""

        '    Dim strMes As String = obj.smsbody
        '    strMes = strMes.Replace("'", " ").Replace("`", "/")

        '    If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.VendorName) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
        '    End If


        '    Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

        '    If clsSMSSend.SendSMS(clsUserMgtCode.frmCSATransfer, strMes, strphone) Then
        '        If Not isPost Then
        '            clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub

    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
        End If
    End Sub


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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New FrmCrptFooter
        frm.strFormId = MyBase.Form_ID
        frm.ShowDialog()
    End Sub

    'Private Sub chkclose_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    If vaddnew = "N" Then
    '        Return
    '    End If
    '    If chkclose.Checked Then
    '        If chkclose.Checked = True AndAlso IsRemarksMandatory = True AndAlso clsCommon.myLen(txtCloseRemarks.Value) = 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please enter Remarks for Closing Order")
    '            chkclose.Checked = False
    '            txtCloseRemarks.Focus()
    '            Return
    '        End If
    '        If Not (common.clsCommon.MyMessageBoxShow("Want To Close Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
    '            Return
    '        End If
    '    End If

    '    If Not chkclose.Checked And btnSave.Enabled = False Then
    '        If Not (common.clsCommon.MyMessageBoxShow("Want To Open Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
    '            Return
    '        End If
    '    End If

    '    Dim qry As String = "select count(*) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
    '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
    '    If check <= 0 Then
    '        clsCommon.MyMessageBoxShow("There is no data found for closed", Me.Text)
    '        Return
    '    End If

    '    Dim obj As New clsCSATransfer()
    '    obj.CloseSO = "N"
    '    If chkclose.Checked = True Then
    '        obj.CloseSO = "Y"
    '    End If

    '    If clsCSATransfer.ClosedData(obj, txtDocNo.Value, txtCloseRemarks.Value) Then
    '        If chkclose.Checked Then
    '            clsCommon.MyMessageBoxShow("Sale Order No. " + txtDocNo.Value + " Is Closed Successfully", Me.Text)
    '            vaddnew = "N"
    '            chkclose.Checked = True
    '            vaddnew = "Y"
    '            btnSave.Enabled = False
    '            btnPost.Enabled = False
    '            btnDelete.Enabled = False
    '            btnCopy.Enabled = False
    '        End If
    '        If Not chkclose.Checked And btnSave.Enabled = False Then
    '            clsCommon.MyMessageBoxShow("Sale Order No. " + txtDocNo.Value + " Is Opened Successfully", Me.Text)
    '            vaddnew = "N"
    '            chkclose.Checked = False
    '            vaddnew = "Y"
    '            If UsLock1.Status = ERPTransactionStatus.Approved Then
    '                btnSave.Enabled = False
    '                btnPost.Enabled = False
    '                btnDelete.Enabled = False
    '                btnCopy.Enabled = False
    '            ElseIf Not UsLock1.Status = ERPTransactionStatus.Approved Then
    '                btnSave.Enabled = True
    '                btnPost.Enabled = True
    '                btnDelete.Enabled = True
    '                btnCopy.Enabled = True
    '            End If
    '        End If
    '    End If

    'End Sub

    '----------------------Done By Preeti Gupta 29/05/2014-------BM00000002659----------

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click

        Dim frm As New FrmSaleHistory
        frm.strFormId = MyBase.Form_ID
        frm.strCustId = txtCustCode.Value
        frm.strCustName = txtCustDesc.Text
        Dim strvendor As String = txtCustCode.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SetTax()
        If Not OpenALLTaxes Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtFromLocation.Value, txtCustCode.Value, "S")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        End If

    End Sub

    Function IsValidItem(ByVal obj As clsCSADeliveryOrderDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDOCode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Doc_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.icode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.uom) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "DO No : " + obj.Doc_No + "  Item : " + obj.iname + Environment.NewLine + ""
                common.clsCommon.MyMessageBoxShow(Me, strMsg, Me.Text)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub fndDONo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDONo._MYValidating
        Dim obj As New clsCSADeliveryOrder()
        Dim objLoc As New clsLocation()
        Dim dtWt As New DataTable()
        Try
            isInsideLoadData = True
            cmbEXType.SelectedValue = ""
            cmbEXType.Enabled = True

            Dim Auto_Scheme_Check As Integer = 0
            Auto_Scheme_Check = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoSchemeOn, clsFixedParameterCode.AutoSchemeOn, Nothing))

            Dim frm As New frmCSAPendingDO()
            frm.VendorCode = clsCommon.myCstr(txtCustCode.Value)
            frm.VendorName = clsCommon.myCstr(txtCustDesc.Text)
            frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
            frm.strCurrDate = clsCommon.myCDate(txtDate.Text)
            frm.MainFormID = MyBase.Form_ID
            frm.ShowDialog()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                obj = clsCSADeliveryOrder.GetData(frm.ArrReturn(0).Doc_No, arrLoc, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                    isbtnDOClick = True
                    fndDONo.Value = obj.docno
                    txtCustCode.Value = obj.cust_code
                    txtCustDesc.Text = obj.cust_name
                    txtFromLocation.Value = obj.frm_loc_code
                    txtFromLocationDesc.Text = obj.frm_loc_name
                    txtExcisable.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when excisable='T' then '1' else '0' end) as exciable from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
                    txtTermCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Code from tspl_customer_master where cust_code='" + txtCustCode.Value + "'"))

                    SetTax()
                    SetTaxDetails()

                    txtToLocation.Value = obj.to_loc_code
                    txtToLocationDesc.Text = obj.to_loc_name

                    txtState.Value = obj.state_code
                    txtStateDesc.Text = obj.state_name
                    txtCSARate.Text = obj.trans_rate
                    txtDocumentTotal.Text = obj.doc_amt
                    cmbTax.SelectedValue = obj.tax
                    Dim CommRate As Decimal = 0
                    Dim CommRateUOM As String = ""
                    Dim CommRateType As String = ""

                    If Not DisableCommissionColumn Then
                        objLoc = clsLocation.GetData(txtToLocation.Value)
                        If Not objLoc Is Nothing Then
                            CommRate = clsCommon.myCdbl(objLoc.csa_commision_rate)
                            CommRateUOM = clsCommon.myCstr(objLoc.csa_commision_type)
                            CommRateType = clsCommon.myCstr(objLoc.CSA_Commission_RS_PERS)
                        End If
                        If clsCommon.myLen(CommRateUOM) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Commission Rate UOM not defined for location " & txtToLocation.Value & "", Me.Text)
                            Exit Sub
                        End If
                    End If

                    gv1.Rows.Clear()
                    cmbEXType.Enabled = False

                    For Each objData As clsCSADeliveryOrderDetail In frm.ArrReturn
                        If IsValidItem(objData) Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objData.lineno)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objData.icode
                            Item_TaxType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Is_Tax_Exempted from TSPL_ITEM_MASTER where Item_Code in ('" + objData.icode + "')"))
                            If clsLocation.isLocatinExcisable(txtFromLocation.Value) = True AndAlso Item_TaxType = 2 Then
                                cmbEXType.SelectedValue = "E"
                                If ForUDLOnly Then
                                    MyLabel24.Visible = True
                                    txtSecondary_Doc_Code.Visible = True
                                Else
                                    MyLabel24.Visible = False
                                    txtSecondary_Doc_Code.Visible = False
                                End If
                            Else
                                cmbEXType.SelectedValue = "N"
                            End If
                            'cmbEXType.SelectedValue = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 1 from TSPL_ITEM_MASTER where Is_Tax_Exempted=2 and Item_Code ='" + objData.icode + "'")) = "1", "E", "N")


                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objData.iname
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCSAType).Value = objData.csa_type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objData.icode, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objData.uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objData.unit_rate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objData.Balance_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDOBalanceQty).Value = objData.Balance_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDOCode).Value = objData.Doc_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDOQty).Value = objData.qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = objData.toltalamt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = objData.tax
                            If ForUDLOnly AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value) <= 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = "No"
                            End If
                            If clsCommon.CompairString(gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value, "Yes") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = "Backward"
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = "Forward"
                            End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(objData.icode, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))

                            'CalUnitPrice(gv1.Rows.Count - 1, True)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0), 2)

                            Dim qry As String
                            Dim Weight_UOM As String = ""
                            Dim SKU_VALUE As Decimal = 0
                            qry = "select Item_Code,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'"
                            dtWt = New DataTable()
                            dtWt = clsDBFuncationality.GetDataTable(qry)
                            If dtWt.Rows.Count > 0 Then
                                SKU_VALUE = clsCommon.myCdbl(dtWt.Rows(0).Item("Weight_Value"))
                                Weight_UOM = clsCommon.myCstr(dtWt.Rows(0).Item("Weight_UOM"))
                            End If

                            If clsCommon.myLen(Weight_UOM) <= 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Weight UOM not defined for Item  " & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "", Me.Text)
                                Exit Sub
                            End If

                            Dim convFacter As Decimal = clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value, Nothing)
                            SKU_VALUE = SKU_VALUE * convFacter
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Pack_Size).Value = SKU_VALUE
                            qry = "select top 1 CF from (select (case when (Container_UOM='" & CommRateUOM & "' and Contained_UOM='" & Weight_UOM & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & CommRateUOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objData.icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                            'Dim Master_Sku As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select a.description from (select TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Master_Value='1' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + clsCommon.myCstr(objData.icode) + "')a"))
                            Dim Master_Sku As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If Master_Sku = 0 Then
                                Master_Sku = 1
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMaster_Pack_Size).Value = Master_Sku
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCSA_Commission_RS_PERS).Value = CommRateType
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Value = CommRate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Tag = CommRateUOM

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIUnitWt).Value = SKU_VALUE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colINetWt).Value = SKU_VALUE * clsCommon.myCdbl(objData.Balance_Qty)

                            qry = "select top 1 CF from (select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objData.icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                            Dim wt_uom_cnvrsn As Decimal = 1 ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_UOM, Nothing)
                            Dim mt_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_MT_Unit, Nothing)

                            If clsCommon.CompairString(Weight_UOM, Weight_MT_Unit) = CompairStringResult.Equal Then
                                mt_uom_cnvrsn = 1
                            End If

                            qry = "select top 1 CF from (select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Gross_Weight_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Gross_Weight_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objData.icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                            Dim gross_uom_cnvrsn As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If clsCommon.CompairString(Weight_UOM, Gross_Weight_Unit) = CompairStringResult.Equal Then
                                gross_uom_cnvrsn = 1
                            End If

                            If gross_uom_cnvrsn > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objData.Balance_Qty) * wt_uom_cnvrsn) * gross_uom_cnvrsn, 2) ' mt_uom_convrsn replaced by gross_uom_cnvrsn done by stuti on 04/01/2017
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = 0
                                If clsCommon.CompairString(Gross_Weight_Unit, Weight_UOM) = CompairStringResult.Equal Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objData.Balance_Qty)), 2)
                                End If
                            End If

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objData.icode)

                            '--------------update free itemcode in main item row------------------
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCode).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFOC).Value = "N"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                            '-------------------------------------------------------------

                            If Auto_Scheme_Check >= 1 OrElse clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeOnCSADeliveryOrder, clsFixedParameterCode.AllowSchemeOnCSADeliveryOrder, Nothing)) = "1" Then
                                '=CASH SCHEME=====================
                                Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(objData.icode, objData.uom, objData.qty, obj.cust_code, objData.Scheme_Code, clsCommon.myCDate(txtDate.Text))

                                If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                                    If obj_Cash.Cash_Pers > 0 Then
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = "P"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value) * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value)) / 100, 2)
                                    ElseIf obj_Cash.Cash_Amt > 0 AndAlso obj_Cash.Cash_Pers <= 0 Then
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCashSchemeType).Value = "A"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Pers).Value = (clsCommon.myCdbl(obj_Cash.Cash_Amt) * 100) / (clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value))
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                                    End If

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).Value = "Y"
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = Nothing
                                End If
                                '>>>>>>>>>>>>>>do code for scheme item------------------------------
                                Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(objData.icode, objData.uom, objData.qty, obj.cust_code, Nothing, objData.Scheme_Code, clsCommon.myCDate(txtDate.Text))

                                Dim index As Integer = gv1.Rows.Count - 1
                                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                    For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                                        '--------------update free itemcode in main item row------------------
                                        gv1.Rows(index).Cells(colSchmCode).Value = Nothing
                                        gv1.Rows(index).Cells(colSchmCodeType).Value = objtr.schm_Type
                                        gv1.Rows(index).Cells(colMainIcode).Value = objtr.Schm_Icode
                                        gv1.Rows(index).Cells(colMainIQty).Value = objtr.Schm_Qty
                                        gv1.Rows(index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                                        gv1.Rows(index).Cells(colFOC).Value = "N"
                                        gv1.Rows(index).Cells(colIsSchmItem).Value = "Y"
                                        '-------------------------------------------------------------

                                        gv1.Rows.AddNew()
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(gv1.Rows.Count)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSAType).Value = objtr.Schm_Item_CSA_Type
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Schm_Item_Uom
                                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colAltUnitCOde).Value = objtr.Schm_Item_Uom
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Schm_IUnit_Rate
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBasicAmount).Value = 0
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = Nothing
                                        If ForUDLOnly AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value) <= 0 Then
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = "No"
                                        End If
                                        If clsCommon.CompairString(gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value, "Yes") = CompairStringResult.Equal Then
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = "Backward"
                                        Else
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = "Forward"
                                        End If
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Schm_Icode)

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0), 2)

                                        qry = ""
                                        Weight_UOM = ""
                                        SKU_VALUE = 0

                                        qry = "select Item_Code,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'"
                                        dtWt = New DataTable()
                                        dtWt = clsDBFuncationality.GetDataTable(qry)
                                        If dtWt.Rows.Count > 0 Then
                                            SKU_VALUE = clsCommon.myCdbl(dtWt.Rows(0).Item("Weight_Value"))
                                            Weight_UOM = clsCommon.myCstr(dtWt.Rows(0).Item("Weight_UOM"))
                                        End If

                                        If clsCommon.myLen(Weight_UOM) <= 0 Then
                                            clsCommon.MyMessageBoxShow(Me, "Weight UOM not defined for Item  " & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "", Me.Text)
                                            Exit Sub
                                        End If

                                        convFacter = clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value, Nothing)
                                        SKU_VALUE = SKU_VALUE * convFacter
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Pack_Size).Value = SKU_VALUE
                                        qry = "select top 1 CF from (select (case when (Container_UOM='" & CommRateUOM & "' and Contained_UOM='" & Weight_UOM & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & CommRateUOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"

                                        Master_Sku = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                                        If Master_Sku = 0 Then
                                            Master_Sku = 1
                                        End If

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMaster_Pack_Size).Value = Master_Sku
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCSA_Commission_RS_PERS).Value = CommRateType
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Value = CommRate
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Tag = CommRateUOM

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIUnitWt).Value = SKU_VALUE
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colINetWt).Value = SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty)
                                        qry = "select top 1 CF from (Select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Weight_MT_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Weight_MT_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                                        wt_uom_cnvrsn = 1 ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_UOM, Nothing)
                                        mt_uom_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) ' clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, Weight_MT_Unit, Nothing)

                                        If clsCommon.CompairString(Weight_UOM, Weight_MT_Unit) = CompairStringResult.Equal Then
                                            mt_uom_cnvrsn = 1
                                        End If

                                        qry = "select top 1 CF from (Select (case when (Container_UOM='" & Weight_UOM & "' and Contained_UOM='" & Gross_Weight_Unit & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & Gross_Weight_Unit & "' and Contained_UOM='" & Weight_UOM & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(objtr.Schm_Icode, Nothing) + "'))aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                                        gross_uom_cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                                        If clsCommon.CompairString(Weight_UOM, Gross_Weight_Unit) = CompairStringResult.Equal Then
                                            gross_uom_cnvrsn = 1
                                        End If

                                        If gross_uom_cnvrsn > 0 Then
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty) * wt_uom_cnvrsn) * gross_uom_cnvrsn, 2) ' mt_uom_convrsn replaced by gross_uom_cnvrsn done by stuti on 04/01/2017
                                        Else
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = 0
                                            If clsCommon.CompairString(Weight_UOM, Gross_Weight_Unit) = CompairStringResult.Equal Then
                                                gv1.Rows(gv1.Rows.Count - 1).Cells(colINetMTWt).Value = System.Math.Round((SKU_VALUE * clsCommon.myCdbl(objtr.Schm_Qty)), 2)
                                            End If
                                        End If

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMRPMandatory).Value = clsItemMaster.IsMRPItem(objtr.Schm_Icode, Nothing)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPers).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TOP(1) Abatement_Percent from TSPL_ABATEMENT_MASTER"))

                                        'CalUnitPrice(gv1.Rows.Count - 1, True)

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCode).Value = objtr.Schm_Code
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIcode).Value = objData.icode
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIQty).Value = objData.qty
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainIUOM).Value = objData.uom
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFOC).Value = "Y"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).Value = clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 2).Cells(colTaxBasis).Value)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxCalcType).Value = clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 2).Cells(colTaxCalcType).Value)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).Value = "0"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).Value = "0"
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).Value = "0"

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBasis).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommision_Rate).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCommisionCharges).ReadOnly = True
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOther_Chrage).ReadOnly = True
                                    Next
                                End If '-------------------scheme if cond.
                            End If '-----------------end scheme check cond.

                        End If ''valid item cond. end


                    Next ''end objdta,frm.arrreturn loop

                End If ''end obj
            Else
                AddNew()
            End If ''frm.arrreturn end

            For Each grow As GridViewRowInfo In gv1.Rows
                UpdateCurrentRow(grow.Index)
            Next
            UpdateAllTotals()

            If GrossWtfromItemMaster Then
                TotalGrossWt_FromItemMaster()
            End If

            txtDate.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isbtnDOClick = False
            obj = Nothing
            objLoc = Nothing
            dtWt = Nothing
            RefreshSerialNo()
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub isValid_CashScheme()
        Dim scheme_Code As String = ""
        Dim isSchemeApply As String = ""
        Dim cash_scheme_code As String = ""
        Dim cash_amt As Decimal = 0
        Dim amount As Decimal = 0

        For Each grow As GridViewRowInfo In gv1.Rows
            isSchemeApply = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
            scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
            cash_scheme_code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
            cash_amt = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
            amount = clsCommon.myCdbl(grow.Cells(colRate).Value) * clsCommon.myCdbl(grow.Cells(colQty).Value)

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

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtFromLocation.Value = obj.Default_LocCode
                txtFromLocationDesc.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        If clsCommon.myLen(txtWayBill_No.Text) > 0 Then
            clsDBFuncationality.ExecuteNonQuery("update tspl_csa_transfer_head set waybill_no='" + txtWayBill_No.Text + "',WayBill_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(ttxway_bill_date.Text), "dd/MMM/yyyy") + "' where doc_code='" + txtDocNo.Value + "'")
            clsCommon.MyMessageBoxShow(Me, "Waybill No. Updated successfully.", Me.Text)
        End If
    End Sub

    Private Sub txtvehicle_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtTransporter_Code._MYValidating
        Dim qry As String = "select Transport_Id as Code,Transporter_Name as Description,City,State,Pincode,Phone from TSPL_TRANSPORT_MASTER"
        txtTransporter_Code.Value = clsCommon.ShowSelectForm("CSATRANSPORTFND", qry, "Code", "", txtTransporter_Code.Value, "Code", isButtonClicked)
        txtTransporter_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id='" + txtTransporter_Code.Value + "'"))

        FillVehicleCharges()
        'txtvehicle_Charge.Text = Math.Round(clsCSATransfer.GetProvisionCharge(txtFromLocation.Value, txtCustCode.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicle_Capacity.Text), txtTransporter_Code.Value), 2)
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
                    If common.clsCommon.MyMessageBoxShow(Me, "Do you want to cancel the CSA Transfer?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                        Dim qrySaveCancel = "Update TSPL_CSA_TRANSFER_HEAD set CancelFlag=1 where Doc_Code='" & txtDocNo.Value & "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qrySaveCancel)
                        If isSaved = True Then
                            clsCommon.MyMessageBoxShow(Me, "CSA Transfer cancelled successfully!", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FillVehicleCharges()
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Else
                Dim dt As New DataTable()
                dt = clsCSATransfer.GetProvisionCharge(txtFromLocation.Value, txtCustCode.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicle_Capacity.Text), txtTransporter_Code.Value)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    txtvehicle_Charge.Text = clsCommon.myCdbl(dt.Rows(0)("FreightCharge"))
                    txtvehicle_Charge.Tag = dt
                Else
                    txtvehicle_Charge.Text = "0"
                    txtvehicle_Charge.Tag = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtGross_Wt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGross_Wt.TextChanged
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
        Else
            If clsCommon.myCdbl(txtGross_Wt.Text) > 0 Then
                'txtvehicle_Charge.Text = clsCSATransfer.GetProvisionCharge(txtFromLocation.Value, txtCustCode.Value, clsCommon.myCdbl(txtGross_Wt.Text), clsCommon.myCdbl(txtVehicle_Capacity.Text), txtTransporter_Code.Value)
                FillVehicleCharges()
            Else
                txtvehicle_Charge.Text = 0
                txtvehicle_Charge.Tag = Nothing
            End If
        End If
    End Sub

    Private Sub txtship_to_loc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtship_to_loc_code._MYValidating
        If clsCommon.myLen(txtCustCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select customer detail.", Me.Text)
            txtCustCode.Focus()
            txtCustCode.Focus()
            Exit Sub
        End If

        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select from location.", Me.Text)
            txtFromLocation.Focus()
            txtFromLocation.Select()
            Exit Sub
        End If

        Dim qry As String = " select TSPL_SHIP_TO_LOCATION.Ship_To_Code as [Code],TSPL_SHIP_TO_LOCATION.Ship_To_Desc as [Description],TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code as[Customer Code] ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_SHIP_TO_LOCATION.Add1,'')='' then '' else TSPL_SHIP_TO_LOCATION.add1 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add2,'')='' then '' else TSPL_SHIP_TO_LOCATION.add2 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add3,'')='' then '' else TSPL_SHIP_TO_LOCATION.add3 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add4,'')='' then '' else TSPL_SHIP_TO_LOCATION.add4 +',' end ,',,',',') as [Ship to Address],TSPL_SHIP_TO_LOCATION.CST_No as [CST NO],TSPL_SHIP_TO_LOCATION.Tin_No as [TIN No]  from TSPL_SHIP_TO_LOCATION left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code "
        txtship_to_loc_code.Value = clsCommon.ShowSelectForm("CSASHPFND", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtCustCode.Value) & "' and loc_code='" & clsCommon.myCstr(txtFromLocation.Value) & "'", txtship_to_loc_code.Value, "Code", isButtonClicked)
        txtship_to_loc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtship_to_loc_code.Value + "'"))
    End Sub

    Private Sub chkownvehicle_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkownvehicle.ToggleStateChanged
        'txtTransporter_Code.MendatroryField = Not chkownvehicle.Checked
        'txtTransporter_Code.Enabled = Not chkownvehicle.Checked
        'txtTransporter_Code.Value = ""
        'txtTransporter_desc.Text = ""
        'txtvehicle_Charge.Text = Nothing
        If chkownvehicle.Checked = True Then
            TxtTransportorMName.Visible = True
            TxtTransportorMName.Location = New Point(100, 112)
            txtTransporter_Code.Visible = False
            txtTransporter_desc.Visible = False
        Else
            txtTransporter_Code.MendatroryField = Not chkownvehicle.Checked
            TxtTransportorMName.Visible = False
            TxtTransportorMName.Location = New Point(834, 132)
            txtTransporter_Code.Visible = True
            txtTransporter_desc.Visible = True
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsCSATransfer.UnPostData(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub

    ''richa agarwal 29 Nov,2016
    Private Sub btn_exciseinvoice_Click(sender As Object, e As EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "CSA Transfer No not found to Print", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funprintexciseInvoice(txtDocNo.Value, "CSATransfer_ExcisableNormal")
        End If
    End Sub
    Private Sub btn_nonexciseinvoice_Click(sender As Object, e As EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "CSA Transfer No not found to Print", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funprintexciseInvoice(txtDocNo.Value, "CSATransfer_ExcisableWithTaxAmount")
        End If
    End Sub


    Public Sub funprintexciseInvoice(ByVal StrCode As String, ByVal strreportname As String)
        Dim frmCRV As New frmCrystalReportViewer()
        Dim dtBarCode As New DataTable
        '  Dim strQuery As String
        Dim dt As DataTable
        dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
        Dim bytes() As Byte
        Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(StrCode, 1, False).[GetType]())
        bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(StrCode, 1, False), GetType(Byte())), Byte())

        '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
        Dim QryShowStatus As String = ""
        Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
        If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
            QryShowStatus = " ,(case when TSPL_CSA_TRANSFER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as SOStatus "
        Else
            QryShowStatus = ""
        End If

        Dim Qry As String
        Dim FooterText As String
        Dim frm As New frmPurchaseOrder
        frm.strFormId = MyBase.Form_ID
        Qry = ""

        FooterText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "'"))
        Qry = " select isnull(TSPL_CSA_TRANSFER_DETAIL.FOC ,'') as FOC,LEFT(CAST(TSPL_CSA_TRANSFER_HEAD.Modify_Date AS TIME ),5) AS TIMEOFPREPARATION, " & _
        " TSPL_COMPANY_MASTER.Insurance_Comp_Name ,TSPL_COMPANY_MASTER.Insurance_No ,convert(varchar,TSPL_COMPANY_MASTER.Insurance_Valid_Date ,103) AS Insurance_Valid_Date,TSPL_CSA_TRANSFER_HEAD.Total_Item_Wt ,TSPL_CSA_TRANSFER_HEAD.Gross_Item_Wt ," & _
        "  convert(varchar,TSPL_CSA_TRANSFER_HEAD.Removal_Date ,103) AS Removal_Date,TSPL_BATCH_ITEM.Batch_No,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_COMPANY_MASTER.CINNo ,TSPL_COMPANY_MASTER.Tcan_No as website,TSPL_COMPANY_MASTER.Pan_No ,TSPL_COMPANY_MASTER.Access_Officer ,convert(varchar,TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) as TinNo_Issue_Date,convert(varchar,TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) PanNo_Issue_Date , " & _
        " tspl_customer_master.PAN as To_Location_Pan,from_location_state.state_name as from_state_Name,from_location_state.State_code as Loc_state_code, TSPL_LOCATION_MASTER.hoAdd1,TSPL_LOCATION_MASTER.hoadd2 , TSPL_CSA_TRANSFER_HEAD.Vehicle_Id,tspl_customer_master.City_Code,TSPL_CSA_TRANSFER_HEAD.Secondary_Doc_Code,TSPL_CSA_TRANSFER_DETAIL.MRP,case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as Transporter_Name,TSPL_CSA_TRANSFER_HEAD.WayBill_No ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.WayBill_Date,103)as WayBill_Date, cast(convert(decimal(18,0),(tspl_csa_transfer_detail.qty * tspl_item_uom_detail.conversion_factor) / alt_convrsn.conversion_factor) as varchar)+' '+TSPL_CSA_TRANSFER_DETAIL.alt_unit_code as alt_unit_code,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name as comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax ,TSPL_COMPANY_MASTER.Email as comp_email ,TSPL_COMPANY_MASTER.Pincode as copm_pincode ,TSPL_COMPANY_MASTER.State as comp_state ,TSPL_COMPANY_MASTER.Tin_No  as copm_TinNo, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_CSA_TRANSFER_DETAIL.Item_Code ,tspl_item_master.Is_Batch_Item, case when isnull(TSPL_CSA_TRANSFER_DETAIL.FOC ,'')='Y' then TSPL_ITEM_MASTER.Item_Desc+'(Free Scheme)' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc ,case when isnull(TSPL_BATCH_ITEM.Batch_No,'')<>'' then TSPL_BATCH_ITEM.qty else TSPL_CSA_TRANSFER_DETAIL.Qty end as Qty ,TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Unit_Rate,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as 'STN_No' ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as [Date_N_Time_issue],TSPL_CSA_TRANSFER_HEAD.Document_Amount,tspl_customer_master.cust_code AS To_Location_Code,TSPL_LOCATION_MASTER.add1 as transpotor,tspl_customer_master.Add1 AS To_Location_Add1, tspl_customer_master.Add2 as To_Location_Add2 ,tspl_customer_master.Add3 as To_Location_Add3, tspl_customer_master.customer_name as To_Location_Desc ,tspl_customer_master.City_Code as To_Location_City_Code , StateMaster_Customer.State_Name as To_Location_State, tspl_customer_master.Pin_Code as To_Location_Pin_Code,  tspl_customer_master.Country as To_Location_Country, case when ISNULL(tspl_customer_master.Phone1,'')='(+__)__________' then '' else tspl_customer_master.Phone1 end +  Case When   ISNULL(tspl_customer_master.Phone2,'')<>'(+__)__________' Then ', '+ tspl_customer_master.Phone2 Else'' End  as To_Location_Telphone, tspl_customer_master.Email as To_Location_Email ,  tspl_customer_master .TIN_No as to_location_tin_no, tspl_customer_master .CST as to_location_cstno , TSPL_LOCATION_MASTER.Location_Code as From_Location_Code,  TSPL_LOCATION_MASTER.Location_Desc as From_Location_Dec,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 , TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 , TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,TSPL_LOCATION_MASTER.Add4  as From_Location_Add4,TSPL_LOCATION_MASTER.City_Code as From_Location_City_Code ,  TSPL_LOCATION_MASTER.State as From_Location_State,TSPL_LOCATION_MASTER.Pin_Code as From_Location_Pin_Code ,TSPL_LOCATION_MASTER.Country as From_Location_Country,  case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End   as From_Location_Telphone,TSPL_LOCATION_MASTER.Email as From_Location_Email,TSPL_LOCATION_MASTER.TIN_No AS From_Location_tin_no, TSPL_LOCATION_MASTER.CST_No AS From_Location_cstNo,TSPL_ITEM_MASTER.Weight_UOM as UOM2," & _
        " TSPL_ITEM_MASTER.Weight_Value as Weight, case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From Due' end  as Against_Form ,TSPL_CSA_TRANSFER_DETAIL.Commission_Chrage ,TSPL_CSA_TRANSFER_DETAIL.Other_Chrage ,Transfer_Rate, " & _
        " TSPL_CSA_TRANSFER_HEAD.GR_No as GRNO ,TSPL_COMPANY_MASTER.Circle_No AS TarrifNo,TSPL_COMPANY_MASTER.CE_Division as Divison,TSPL_COMPANY_MASTER.CE_Commissionerate ,TSPL_COMPANY_MASTER.CE_Range ,TSPL_COMPANY_MASTER.Ecc_No as ExciseRegdNo , TSPL_CUSTOMER_MASTER.ECC,convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as grdate,case when isnull( TSPL_CSA_TRANSFER_HEAD.Against_Form,'')='F' then 'F' else '' end as Termsofdelivery, " & _
        " tspl_customer_master.City_Code as consigneeaddress, TSPL_CSA_TRANSFER_DETAIL.abatement_pers, TSPL_CSA_TRANSFER_DETAIL.Abatement_amt,tspl_city_master.city_name as ToLocCityName,TSPL_CSA_TRANSFER_HEAD.document_type,TSPL_CSA_TRANSFER_HEAD.Description, " & _
        " isnull(tax1.Tax_Code_Desc,'') as tax1name,isnull (TSPL_CSA_TRANSFER_HEAD.tax1_amt,0) as txt1amt,  isnull( tax2.Tax_Code_Desc,'') as tax2name, isnull (TSPL_CSA_TRANSFER_HEAD.tax2_amt,0) as txt2amt,   isnull(tax3.Tax_Code_Desc,'') as tax3name, isnull (TSPL_CSA_TRANSFER_HEAD.tax3_amt,0) as txt3amt,  isnull( tax4.Tax_Code_Desc,'') as tax4name, isnull (TSPL_CSA_TRANSFER_HEAD.tax4_amt,0) as txt4amt,  isnull( tax5.Tax_Code_Desc,'') as tax5name, isnull (TSPL_CSA_TRANSFER_HEAD.tax5_amt,0) as txt5amt,  isnull( tax6.Tax_Code_Desc,'') as tax6name, isnull (TSPL_CSA_TRANSFER_HEAD.tax6_amt,0) as txt6amt,   isnull(tax7.Tax_Code_Desc,'') as tax7name, isnull (TSPL_CSA_TRANSFER_HEAD.tax7_amt,0) as txt7amt,  isnull( tax8.Tax_Code_Desc,'') as tax8name, isnull (TSPL_CSA_TRANSFER_HEAD.tax8_amt,0) as txt8amt, isnull(tax9.Tax_Code_Desc,'') as tax9name, isnull (TSPL_CSA_TRANSFER_HEAD.tax9_amt,0) as txt9amt,   isnull(tax10.Tax_Code_Desc,'') as tax10name, isnull (TSPL_CSA_TRANSFER_HEAD.tax10_amt,0) as txt10amt," + Environment.NewLine & _
        " TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate" + Environment.NewLine & _
        ",isnull(TSPL_ITEM_MASTER.HSN_Code,'') as HSN_Code,TSPL_CSA_TRANSFER_HEAD.EwayBillNo,Convert(varchar(15),TSPL_CSA_TRANSFER_HEAD.EwayBillDate,103) as EwaBillDate,TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_LOCATION_MASTER.GSTNO as GSTIN_NO,from_location_state.GST_STATE_CODE AS From_GST_StateCode,tspl_customer_master.GSTNO as To_Loc_GstinNo,StateMaster_Customer.GST_STATE_Code as To_Loc_GSTStateCode" & _
        ",tax1.Type as tax1Type , " & _
        "  tax2.Type as tax2Type, " & _
        " tax3.Type as tax3Type,  " & _
        " tax4.Type as tax4Type,  " & _
        " tax5.Type as tax5Type,  " & _
        " tax6.Type as tax6Type,  " & _
        " tax7.Type as tax7Type,  " & _
        " tax8.Type as tax8Type, " & _
        " tax9.Type as tax9Type,  " & _
        " tax10.Type as tax10Type, " & _
        " isnull(tspl_csa_transfer_detail.TAX1_Amt,0) as DTax1_Amt, isnull(tspl_csa_transfer_detail.TAX2_Amt,0) as DTax2_Amt," & _
        "isnull(tspl_csa_transfer_detail.TAX3_Amt,0) as DTax3_Amt, isnull(tspl_csa_transfer_detail.TAX4_Amt,0) as DTax4_Amt," & _
        " isnull(tspl_csa_transfer_detail.TAX5_Amt,0) as DTax5_Amt, isnull(tspl_csa_transfer_detail.TAX6_Amt,0) as DTax6_Amt," & _
        " isnull(tspl_csa_transfer_detail.TAX7_Amt,0) as DTax7_Amt, isnull(tspl_csa_transfer_detail.TAX8_Amt,0) as DTax8_Amt," & _
        " isnull(tspl_csa_transfer_detail.TAX9_Amt,0) as DTax9_Amt, isnull(tspl_csa_transfer_detail.TAX10_Amt,0) as DTax10_Amt," & _
        " tspl_csa_transfer_detail.TAX1_Rate as DTax1_Rate,tspl_csa_transfer_detail.TAX2_Rate as DTax2_Rate," & _
        " tspl_csa_transfer_detail.TAX3_Rate as DTax3_Rate,tspl_csa_transfer_detail.TAX4_Rate as DTax4_Rate," & _
        " tspl_csa_transfer_detail.TAX5_Rate as DTax5_Rate,tspl_csa_transfer_detail.TAX6_Rate as DTax6_Rate," & _
        " tspl_csa_transfer_detail.TAX7_Rate as DTax7_Rate,tspl_csa_transfer_detail.TAX8_Rate as DTax8_Rate," & _
        " tspl_csa_transfer_detail.TAX9_Rate as DTax9_Rate,tspl_csa_transfer_detail.TAX10_Rate as DTax10_Rate" & _
        " from TSPL_CSA_TRANSFER_DETAIL" & _
        " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " & _
        " left outer join TSPL_BATCH_ITEM on TSPL_BATCH_ITEM.Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code and TSPL_CSA_TRANSFER_DETAIL.Line_No =TSPL_BATCH_ITEM.Parent_Line_No and TSPL_BATCH_ITEM.Document_Code =TSPL_CSA_TRANSFER_HEAD.DOC_CODE and TSPL_BATCH_ITEM .In_Out_Type ='O' " & _
        " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code " & _
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code " & _
        " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code " & _
        " left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.To_Location_Code " & _
        " left outer join tspl_state_master as from_location_state on from_location_state.state_code=TSPL_LOCATION_MASTER.state" & _
        " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_csa_transfer_detail.item_code and tspl_item_uom_detail.uom_code=tspl_csa_transfer_detail.unit_code " & _
        " left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=tspl_csa_transfer_detail.item_code and alt_convrsn.uom_code=tspl_csa_transfer_detail.alt_unit_code " & _
        " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSA_TRANSFER_HEAD.Transport_Id " & _
        " LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code " & _
        " LEFT OUTER JOIN tspl_city_master   ON tspl_customer_master.City_Code =tspl_city_master.City_Code " & _
        " left outer join tspl_state_master as From_Loc_State_Name on From_Loc_State_Name.STATE_CODE =TSPL_CSA_TRANSFER_HEAD.STATE_CODE" & _
        " LEFT OUTER JOIN TSPL_STATE_MASTER StateMaster_Customer on StateMaster_Customer.State_Code=TSPL_CUSTOMER_MASTER.State" & _
        " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_CSA_TRANSFER_HEAD.tax1" + Environment.NewLine & _
        " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_CSA_TRANSFER_HEAD.tax2" + Environment.NewLine & _
        " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_CSA_TRANSFER_HEAD.TAX3" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_CSA_TRANSFER_HEAD.tax4" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_CSA_TRANSFER_HEAD.tax5" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX6" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX7" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX8" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX9" + Environment.NewLine & _
        " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX10" + Environment.NewLine & _
        " where 1=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" + StrCode + "'"

        dt = clsDBFuncationality.GetDataTable(Qry)
        dt.Columns.Add("BarCodeImage", GetType(Byte()))
        For Each dr As DataRow In dt.Rows
            dr("BarCodeImage") = bytes
        Next
        If dt.Rows.Count > 0 Then
            'SetItemWiseTax(dt, txtDocNo.Value)
            'KwalitySalesReportViewer.funreport(dt, "CSATransfer", "CSA Transfer

            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue"))) Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("from_state_Name")), clsCommon.myCstr(dt.Rows(0)("To_Location_State"))) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(dt.Rows(0)("txt1amt")) <> 0 Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Manditax", "CSA Transfer Local With MandiTax", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                    Else
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Intrastate", "CSA Transfer Local", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                    End If
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Interstate", "CSA Transfer Interstate", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                End If

            Else
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), strreportname, "CSA Transfer", "rptCompanyAddress.rpt")
            End If
        End If
        frmCRV = Nothing
    End Sub

    ''----------------------
    Private Sub btn_Depo_print_Click(sender As Object, e As EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("CSA Transfer No not found to Print")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funPrint(txtDocNo.Value, "Y")
        End If
    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
        If GSTStatus Then
            chk_F_Form.Visible = False
        Else
            chk_F_Form.Visible = True
        End If

    End Sub

    Private Sub btnEwaybillnoupdate_Click(sender As Object, e As EventArgs) Handles btnEwaybillnoupdate.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            clsDBFuncationality.ExecuteNonQuery("update tspl_csa_transfer_head set EWayBillNo='" + TxtEWayBillNo.Text + "',EWayBillDate='" + clsCommon.GetPrintDate(clsCommon.myCDate(TxtEWayBillDate.Value), "dd/MMM/yyyy") + "', Electronic_Ref_No= '" + txtElectronicRefNo.Text + "' where doc_code='" + txtDocNo.Value + "'")
            clsCommon.MyMessageBoxShow(Me, "E-Waybill No. Updated successfully.", Me.Text)
        End If
    End Sub
    Private Sub txtVehicle_Capacity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVehicle_Capacity.TextChanged
        Try
            If clsCommon.myLen(txtVehicle_Capacity.Text) <= 0 Then
                Exit Sub
            Else
                Dim city As String = clsDBFuncationality.getSingleValue("select City_Code from tspl_customer_master where cust_code='" & txtCustCode.Value & "'")
                Dim qry As String = clsDBFuncationality.getSingleValue("select freight from TSPL_ROUTE_FREIGHT_DETAILS where city_code='" & city & "' and location_code='" & txtFromLocation.Value & "' and transport_id='" & txtTransporter_Code.Value & "' and capacityMT='" & txtVehicle_Capacity.Text & "'")
                If clsCommon.myLen(qry) > 0 Then
                    txtvehicle_Charge.Text = qry
                Else
                    txtvehicle_Charge.Text = 0
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintMandi_Click(sender As Object, e As EventArgs) Handles btnPrintMandi.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "CSA Transfer No not found to Print", Me.Text)
            RadPageView1.SelectedPage = RadPageViewPage1
            txtDocNo.Focus()
            txtDocNo.Select()
        Else
            funPrintMandi(txtDocNo.Value, "Y")
        End If
    End Sub
    Public Sub funPrintMandi(ByVal StrCode As String, Optional ByVal strDepPrint As String = Nothing)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.myLen(StrCode) > 0 Then
                Dim PrintType As String = ""
                Dim strQuery As String
                Dim dt As DataTable
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select TSPL_CSA_TRANSFER_HEAD.transfer_date from TSPL_CSA_TRANSFER_HEAD where DOC_CODE='" + StrCode + "' "), "dd/MMM/yyyy")

                PrintType = "Select Item_Tax_Type from TSPL_CSA_TRANSFER_HEAD WHERE DOC_CODE='" + StrCode + "'"
                PrintType = clsDBFuncationality.getSingleValue(PrintType)
                If (Not (PrintType = "2") OrElse clsERPFuncationality.GetGSTStatus(dtDocdate) = True) Then
                    Dim dtBarCode As New DataTable

                    dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
                    Dim bytes() As Byte
                    Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(StrCode, 1, False).[GetType]())
                    bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(StrCode, 1, False), GetType(Byte())), Byte())

                    '' Anubhooti 28-Aug-2014 (Demo Setting For Status) BM00000003672
                    Dim QryShowStatus As String = ""
                    Dim ShowStatusForSale As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForSales' And Type ='ShowStatusForSales'")
                    If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForSale), "1") = CompairStringResult.Equal Then
                        QryShowStatus = " ,(case when TSPL_CSA_TRANSFER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as SOStatus "
                    Else
                        QryShowStatus = ""
                    End If
                    Dim StrGheeItem As Double = 0
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        If clsCommon.myLen(StrCode) > 0 Then
                            StrGheeItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from tspl_item_master where Item_Code in (select Item_Code from TSPL_CSA_TRANSFER_DETAIL where doc_code='" & StrCode & "') and Structure_Code in('GHEE','CGHEE')"))
                        End If
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
                    Qry = " select " & StrGheeItem & " AS CheckForGhee,(case when " & StrGheeItem & " >0 THEN 'This is Stock Transfer being Dispatched to above Destination Court order and other relevent documents copy enclosed.' else '' end) as Special_Instruction," & _
                        " TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_CSA_TRANSFER_HEAD.EwayBillNo,Convert(varchar(15),TSPL_CSA_TRANSFER_HEAD.EwayBillDate,103) as EwaBillDate,TSPL_CSA_TRANSFER_HEAD.Electronic_Ref_No,TSPL_ITEM_MASTER.HSN_Code,isnull(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0) as Disc_Amt," & _
                        " TSPL_LOCATION_MASTER.GSTNO as GSTIN_NO,from_location_state.GST_STATE_CODE AS From_GST_StateCode,tspl_customer_master.GSTNO as To_Loc_GstinNo,StateMaster_Customer.GST_STATE_Code as To_Loc_GSTStateCode,  STUFF((SELECT DISTINCT(',' + cst.DELEVERY_ORDER_NO) FROM TSPL_CSA_TRANSFER_DETAIL as cst WHERE cst.DOC_CODE  =TSPL_CSA_TRANSFER_HEAD .DOC_CODE FOR XML PATH('')), 1, 1, '') AS csaDespatchAdviceNo," & _
                    " STUFF((SELECT DISTINCT(',' + convert(varchar,dah.Doc_Date,103)) FROM TSPL_CSA_DO_HEAD as dah WHERE dah.Doc_No in ( select ct.DELEVERY_ORDER_NO from TSPL_CSA_TRANSFER_DETAIL  ct where ct.DOC_CODE  = TSPL_CSA_TRANSFER_HEAD .DOC_CODE)   FOR XML PATH('')), 1, 1, '') AS csaDespatchAdviceDate,LEFT(CAST(TSPL_CSA_TRANSFER_HEAD.Modify_Date AS TIME ),5) AS TIMEOFPREPARATION, " & _
                        " TSPL_COMPANY_MASTER.Insurance_Comp_Name ,TSPL_COMPANY_MASTER.Insurance_No ,convert(varchar,TSPL_COMPANY_MASTER.Insurance_Valid_Date ,103) AS Insurance_Valid_Date,TSPL_CSA_TRANSFER_HEAD.Total_Item_Wt ,TSPL_CSA_TRANSFER_HEAD.Gross_Item_Wt ,"
                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += "  TSPL_BATCH_ITEM.Batch_No, "
                    End If
                    ''--------------
                    Qry += " convert(varchar,TSPL_CSA_TRANSFER_HEAD.Removal_Date ,103) AS Removal_Date,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_COMPANY_MASTER.CINNo ,TSPL_COMPANY_MASTER.Tcan_No as website,TSPL_COMPANY_MASTER.Pan_No ,TSPL_COMPANY_MASTER.Access_Officer ,convert(varchar,TSPL_COMPANY_MASTER.TinNo_Issue_Date,103) as TinNo_Issue_Date,convert(varchar,TSPL_COMPANY_MASTER.PanNo_Issue_Date,103) PanNo_Issue_Date , " & _
                        " tspl_customer_master.PAN as To_Location_Pan,from_location_state.state_name as from_state_Name,from_location_state.State_code as Loc_state_code, TSPL_LOCATION_MASTER.hoAdd1,TSPL_LOCATION_MASTER.hoadd2 , TSPL_CSA_TRANSFER_HEAD.Vehicle_Id,tspl_customer_master.City_Code,case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as Transporter_Name,TSPL_CSA_TRANSFER_HEAD.WayBill_No ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.WayBill_Date,103)as WayBill_Date, cast(convert(decimal(18,0),(tspl_csa_transfer_detail.qty * tspl_item_uom_detail.conversion_factor) / alt_convrsn.conversion_factor) as varchar)+' '+TSPL_CSA_TRANSFER_DETAIL.alt_unit_code as alt_unit_code,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name as comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax ,TSPL_COMPANY_MASTER.Email as comp_email ,TSPL_COMPANY_MASTER.Pincode as copm_pincode ,TSPL_COMPANY_MASTER.State as comp_state ,TSPL_COMPANY_MASTER.Tin_No  as copm_TinNo, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_CSA_TRANSFER_DETAIL.Item_Code , case when isnull(TSPL_CSA_TRANSFER_DETAIL.FOC ,'')='Y' then TSPL_ITEM_MASTER.Item_Desc+'(Free Scheme)' else TSPL_ITEM_MASTER.Item_Desc end as Item_Desc  ,tspl_item_master.Is_Batch_Item,"

                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += " case when isnull(TSPL_BATCH_ITEM.Batch_No,'')<>'' then TSPL_BATCH_ITEM.qty else TSPL_CSA_TRANSFER_DETAIL.Qty end as Qty,TSPL_CSA_TRANSFER_DETAIL.FOC, "
                    Else
                        Qry += " TSPL_CSA_TRANSFER_DETAIL.Qty, "
                    End If
                    ''--------------
                    Qry += " TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Unit_Rate,TSPL_CSA_TRANSFER_HEAD.DOC_CODE as 'STN_No' ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as [Date_N_Time_issue] "
                    'Qry += " TSPL_CSA_TRANSFER_HEAD.Document_Amount "
                    'Qry += " ,case when tspl_csa_transfer_detail.CALC_TYPE='Backward' then TSPL_CSA_TRANSFER_HEAD.Document_Amount+TSPL_CSA_TRANSFER_HEAD.Total_Tax_Amt " & _
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        Qry += " ,TSPL_CSA_TRANSFER_HEAD.Document_Amount as Document_Amount "
                    Else
                        Qry += " ,(case when tspl_csa_transfer_detail.CALC_TYPE='Backward' then (select sum(Amount) from TSPL_CSA_TRANSFER_DETAIL where doc_code='" + StrCode + "') " & _
                   " else TSPL_CSA_TRANSFER_HEAD.Document_Amount end) as Document_Amount"
                    End If
                   

                    Qry += " ,tspl_customer_master.cust_code AS To_Location_Code,TSPL_LOCATION_MASTER.add1 as transpotor,tspl_customer_master.Add1 AS To_Location_Add1, tspl_customer_master.Add2 as To_Location_Add2 ,tspl_customer_master.Add3 as To_Location_Add3, tspl_customer_master.customer_name as To_Location_Desc ,tspl_customer_master.City_Code as To_Location_City_Code , StateMaster_Customer.State_Name as To_Location_State, tspl_customer_master.pin_no as To_Location_Pin_Code,  tspl_customer_master.Country as To_Location_Country, case when ISNULL(tspl_customer_master.Phone1,'')='(+__)__________' then '' else tspl_customer_master.Phone1 end +  Case When   ISNULL(tspl_customer_master.Phone2,'')<>'(+__)__________' Then ', '+ tspl_customer_master.Phone2 Else'' End  as To_Location_Telphone, tspl_customer_master.Email as To_Location_Email ,  tspl_customer_master .TIN_No as to_location_tin_no, tspl_customer_master .CST as to_location_cstno , TSPL_LOCATION_MASTER.Location_Code as From_Location_Code,  TSPL_LOCATION_MASTER.Location_Desc as From_Location_Dec,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 , TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 , TSPL_LOCATION_MASTER.Add3 as From_Location_Add3,TSPL_LOCATION_MASTER.Add4  as From_Location_Add4,TSPL_LOCATION_MASTER.City_Code as From_Location_City_Code ,  TSPL_LOCATION_MASTER.State as From_Location_State,TSPL_LOCATION_MASTER.Pin_Code as From_Location_Pin_Code ,TSPL_LOCATION_MASTER.Country as From_Location_Country,  case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End   as From_Location_Telphone,TSPL_LOCATION_MASTER.Email as From_Location_Email,TSPL_LOCATION_MASTER.TIN_No AS From_Location_tin_no, TSPL_LOCATION_MASTER.CST_No AS From_Location_cstNo,TSPL_ITEM_MASTER.Weight_UOM as UOM2,"
                    Qry += " TSPL_ITEM_MASTER.Weight_Value as Weight, "
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
                        Qry += "   case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From' end  as Against_Form ,"
                    Else
                        Qry += "   case when  TSPL_CSA_TRANSFER_head.Against_Form='F' then 'Against F-From Due' end  as Against_Form ,"
                    End If


                    Qry += "  TSPL_CSA_TRANSFER_DETAIL.Commission_Chrage, TSPL_CSA_TRANSFER_DETAIL.Other_Chrage, "
                    ' Ticket No : KDI/10/09/18-000427 
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                        Qry += " Transfer_Rate as Transfer_Rate,"
                    Else
                        Qry += " (case when tspl_csa_transfer_detail.CALC_TYPE='Backward' then Unit_Rate else Transfer_Rate end) as Transfer_Rate,"
                    End If
                    Qry += " tspl_csa_transfer_detail.CALC_TYPE ,"

                    ''added by richa agarwal
                    Qry += " TSPL_CSA_TRANSFER_HEAD.GR_No as GRNO ,convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as grdate,case when isnull( TSPL_CSA_TRANSFER_HEAD.Against_Form,'')='F' then 'F' else '' end as Termsofdelivery, "
                    Qry += " tspl_customer_master.City_Code as consigneeaddress,tspl_city_master.city_name as ToLocCityName,StateMaster_Customer.STATE_NAME as ToLocStateName,TSPL_CSA_TRANSFER_HEAD.document_type,TSPL_CSA_TRANSFER_HEAD.Description "
                    '============Sanjeet(GST 09/06/2017)======================
                    Qry += ", tax1.Tax_Code_Desc as tax1name,isnull (TSPL_CSA_TRANSFER_HEAD.TAX1_Amt,0) as txt1amt,tax1.Type as tax1Type , " & _
                        " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_CSA_TRANSFER_HEAD.tax2_amt,0) as txt2amt, tax2.Type as tax2Type, " & _
                      " tax3.Tax_Code_Desc as tax3name,  isnull (TSPL_CSA_TRANSFER_HEAD.tax3_amt,0) as txt3amt,tax3.Type as tax3Type,  " & _
                     " tax4.Tax_Code_Desc as tax4name, isnull (TSPL_CSA_TRANSFER_HEAD.tax4_amt,0) as txt4amt,tax4.Type as tax4Type,  " & _
                     " tax5.Tax_Code_Desc as tax5name, isnull (TSPL_CSA_TRANSFER_HEAD.tax5_amt,0) as txt5amt, tax5.Type as tax5Type,  " & _
                      " tax6.Tax_Code_Desc as tax6name, isnull (TSPL_CSA_TRANSFER_HEAD.tax6_amt,0) as txt6amt,tax6.Type as tax6Type,  " & _
                     "tax7.Tax_Code_Desc as tax7name, isnull (TSPL_CSA_TRANSFER_HEAD.tax7_amt,0) as txt7amt, tax7.Type as tax7Type,  " & _
                     "tax8.Tax_Code_Desc as tax8name, isnull (TSPL_CSA_TRANSFER_HEAD.tax8_amt,0) as txt8amt, tax8.Type as tax8Type, " & _
                     "tax9.Tax_Code_Desc as tax9name, isnull (TSPL_CSA_TRANSFER_HEAD.tax9_amt,0) as txt9amt, tax9.Type as tax9Type,  " & _
                     "tax10.Tax_Code_Desc as tax10name, isnull (TSPL_CSA_TRANSFER_HEAD.tax10_amt,0) as txt10amt, tax10.Type as tax10Type, " & _
                     " isnull(tspl_csa_transfer_detail.TAX1_Amt,0) as DTax1_Amt, isnull(tspl_csa_transfer_detail.TAX2_Amt,0) as DTax2_Amt," & _
              "isnull(tspl_csa_transfer_detail.TAX3_Amt,0) as DTax3_Amt, isnull(tspl_csa_transfer_detail.TAX4_Amt,0) as DTax4_Amt," & _
              " isnull(tspl_csa_transfer_detail.TAX5_Amt,0) as DTax5_Amt, isnull(tspl_csa_transfer_detail.TAX6_Amt,0) as DTax6_Amt," & _
               " isnull(tspl_csa_transfer_detail.TAX7_Amt,0) as DTax7_Amt, isnull(tspl_csa_transfer_detail.TAX8_Amt,0) as DTax8_Amt," & _
                " isnull(tspl_csa_transfer_detail.TAX9_Amt,0) as DTax9_Amt, isnull(tspl_csa_transfer_detail.TAX10_Amt,0) as DTax10_Amt," & _
            " tspl_csa_transfer_detail.TAX1_Rate as DTax1_Rate,tspl_csa_transfer_detail.TAX2_Rate as DTax2_Rate," & _
                " tspl_csa_transfer_detail.TAX3_Rate as DTax3_Rate,tspl_csa_transfer_detail.TAX4_Rate as DTax4_Rate," & _
                    " tspl_csa_transfer_detail.TAX5_Rate as DTax5_Rate,tspl_csa_transfer_detail.TAX6_Rate as DTax6_Rate," & _
                        " tspl_csa_transfer_detail.TAX7_Rate as DTax7_Rate,tspl_csa_transfer_detail.TAX8_Rate as DTax8_Rate," & _
                            " tspl_csa_transfer_detail.TAX9_Rate as DTax9_Rate,tspl_csa_transfer_detail.TAX10_Rate as DTax10_Rate," & _
                     "TSPL_CSA_TRANSFER_HEAD.TAX1_Rate ,TSPL_CSA_TRANSFER_HEAD.TAX2_Rate ,TSPL_CSA_TRANSFER_HEAD.TAX3_Rate,TSPL_CSA_TRANSFER_HEAD.TAX4_Rate,TSPL_CSA_TRANSFER_HEAD.TAX5_Rate,TSPL_CSA_TRANSFER_HEAD.TAX6_Rate,TSPL_CSA_TRANSFER_HEAD.TAX7_Rate,TSPL_CSA_TRANSFER_HEAD.TAX8_Rate,TSPL_CSA_TRANSFER_HEAD.TAX9_Rate,TSPL_CSA_TRANSFER_HEAD.TAX10_Rate, " & _
                     "TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name1,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt1,0) as Add_Charge_Amt1,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name2,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt2,0) as Add_Charge_Amt2,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name3,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt3,0) as Add_Charge_Amt3,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name4,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt4,0) as Add_Charge_Amt4,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name5,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt5,0) as Add_Charge_Amt5,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name6,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt6,0) as Add_Charge_Amt6,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name7,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt7,0) as Add_Charge_Amt7,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name8,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt8,0) as Add_Charge_Amt8,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name9,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt9,0) as Add_Charge_Amt9,TSPL_CSA_TRANSFER_HEAD.Add_Charge_Name10,isnull (TSPL_CSA_TRANSFER_HEAD.Add_Charge_Amt10,0) as Add_Charge_Amt10,TSPL_CSA_TRANSFER_DETAIL.TAX1,TSPL_CSA_TRANSFER_DETAIL.TAX2 ,TSPL_CSA_TRANSFER_DETAIL.TAX3  "
                    '========================================================
                    Qry += " ,TSPL_CSA_TRANSFER_DETAIL.Amount,TSPL_CSA_TRANSFER_HEAD.RoundOffAmount "
                    Qry += " from TSPL_CSA_TRANSFER_DETAIL"
                    ''-------------------------------
                    Qry += " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE "
                    ''added by richa agarwal
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += " left outer join TSPL_BATCH_ITEM on TSPL_BATCH_ITEM.Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code and TSPL_CSA_TRANSFER_DETAIL.Line_No =TSPL_BATCH_ITEM.Parent_Line_No and TSPL_BATCH_ITEM.Document_Code =TSPL_CSA_TRANSFER_HEAD.DOC_CODE and TSPL_BATCH_ITEM .In_Out_Type ='O' "
                    End If
                    ''--------------
                    Qry += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code "
                    Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code "
                    Qry += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "

                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        Qry += "  left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.cust_code "
                    Else
                        Qry += "  left outer join tspl_customer_master on tspl_customer_master.cust_code =  TSPL_CSA_TRANSFER_HEAD.To_Location_Code "
                    End If
                    Qry += " left outer join tspl_state_master as from_location_state on from_location_state.state_code=TSPL_LOCATION_MASTER.state"
                    Qry += " left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=tspl_csa_transfer_detail.item_code and tspl_item_uom_detail.uom_code=tspl_csa_transfer_detail.unit_code "
                    Qry += " left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=tspl_csa_transfer_detail.item_code and alt_convrsn.uom_code=tspl_csa_transfer_detail.alt_unit_code "
                    Qry += " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSA_TRANSFER_HEAD.Transport_Id "
                    Qry += " LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.Location_Code =TSPL_CSA_TRANSFER_HEAD.From_Location_Code "
                    Qry += " LEFT OUTER JOIN tspl_city_master   ON tspl_customer_master.City_Code =tspl_city_master.City_Code "
                    Qry += " left outer join tspl_state_master as From_Loc_State_Name on From_Loc_State_Name.STATE_CODE =TSPL_CSA_TRANSFER_HEAD.STATE_CODE" & _
                        " LEFT OUTER JOIN TSPL_STATE_MASTER StateMaster_Customer on StateMaster_Customer.State_Code=TSPL_CUSTOMER_MASTER.State"
                    '============Sanjeet(GST 09/06/2017)======================
                    Qry += " LEFT JOIN TSPL_TAX_MASTER tax1 ON tax1.Tax_Code=tspl_csa_transfer_detail.TAX1 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax2 ON tax2.Tax_Code=tspl_csa_transfer_detail.TAX2 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax3 ON tax3.Tax_Code=tspl_csa_transfer_detail.TAX3 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax4 ON tax4.Tax_Code=tspl_csa_transfer_detail.TAX4 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax5 ON tax5.Tax_Code=tspl_csa_transfer_detail.TAX5 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax6 ON tax6.Tax_Code=tspl_csa_transfer_detail.TAX6 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax7 ON tax7.Tax_Code=tspl_csa_transfer_detail.TAX7 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax8 ON tax8.Tax_Code=tspl_csa_transfer_detail.TAX8 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax9 ON tax9.Tax_Code=tspl_csa_transfer_detail.TAX9 " & _
                          " LEFT JOIN TSPL_TAX_MASTER tax10 ON tax10.Tax_Code=tspl_csa_transfer_detail.TAX10 "
                    '======================================================================
                    ''added by richa agarwal
                    'Qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_COMPANY_MASTER.City_Code"
                    'Qry += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State"
                    ''------------------------
                    Qry += " where 1=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE='" + StrCode + "'"

                    dt = clsDBFuncationality.GetDataTable(Qry)
                    dt.Columns.Add("BarCodeImage", GetType(Byte()))
                    For Each dr As DataRow In dt.Rows
                        dr("BarCodeImage") = bytes
                    Next

                    Dim arrTaxType As New List(Of String)
                    For ii As Integer = 1 To 10
                        If clsCommon.myCstr(dt.Rows(0)(clsCommon.myCstr("tax" + clsCommon.myCstr(ii) + "Type"))) <> "" Then
                            arrTaxType.Add(dt.Rows(0)(clsCommon.myCstr("tax" + clsCommon.myCstr(ii) + "Type")))
                        End If
                    Next

                    If dt.Rows.Count > 0 Then

                        If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue"))) Then

                            If arrTaxType.Contains("M") AndAlso arrTaxType.Contains("IGST") Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_InterStateWithMandi", "CSA Transfer Inter State With Mandi", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            ElseIf arrTaxType.Contains("M") AndAlso arrTaxType.Contains("SGST") AndAlso arrTaxType.Contains("CGST") Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Local_WithMandi", "CSA Transfer Local With MandiTax", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            ElseIf arrTaxType.Contains("SGST") AndAlso arrTaxType.Contains("CGST") Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Intrastate", "CSA Transfer Local", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            ElseIf arrTaxType.Contains("IGST") Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Interstate", "CSA Transfer", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            ElseIf arrTaxType.Contains("M") AndAlso (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal) Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer_Mandi", "CSA Transfer", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            ElseIf (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal) Then
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer", "CSA Transfer", clsCommon.myCDate(dt.Rows(0)("Date_N_Time_issue")), "rptCompanyAddress.rpt")
                            Else
                                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer", "CSA Transfer", "rptCompanyAddress.rpt")
                            End If

                        Else
                            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CSATransfer", "CSA Transfer", "rptCompanyAddress.rpt")
                        End If



                    End If
                ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
                    strQuery = "Select * from ( select CMTo.City_Name as City_Name,SMFrom.STATE_NAME as Loc_State_Name, SMFrom.state_code as frm_State_code, LMFrom.HOAdd1, LMFrom.HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_HEAD.Transport_Id, TSPL_TRANSPORT_MASTER.Transporter_Name, TSPL_CSA_TRANSFER_HEAD.GR_No, case when coalesce(TSPL_CSA_TRANSFER_HEAD.GR_No,'')='' then null else convert(varchar,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) end as GR_Date," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_No, 0 as Alter_UnitQty, TSPL_CSA_TRANSFER_HEAD.Discount_Amt  as HeadDisc_Amt, 0 as HeadDisc_PerAmt, TSPL_ITEM_MASTER.Cheapter_Heads," + Environment.NewLine & _
" TSPL_CHAPTER_HEAD.Description as Chap_Desc, LMFrom.Registration_Number, '' as Payment_Terms, TSPL_CSA_TRANSFER_HEAD.Modify_By, TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO," + Environment.NewLine & _
" LMFrom.add1 +case when len(LMFrom.add2)>0 then ', '+LMFrom.add2 else '' end +case when LEN(isnull(LMFrom.Add3,''))>0 then ', '+isnull(LMFrom.Add3,'') else ' ' end + case when LEN(CMFrom.City_Name)>0 then ', '+CMFrom .City_Name else ' ' end + case when len(SMFrom.STATE_NAME  )>0 then ', '+ SMFrom.STATE_NAME else ' ' end  + case when len(LMFrom.Pin_Code   )>0 then ', Pin Code - '+ cast(LMFrom.Pin_Code  as varchar)  else ' ' end  + case when len(LMFrom.Tin_No     )>0 then ', Tin No - '+ cast(LMFrom.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(LMFrom.Phone1,''))>0 and LMFrom.Phone1='(+__)__________' then '' else ', Phone'+LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(LMFrom.Email    )>0 then ', Email - '+ LMFrom.Email else '' end  as Location_Address," + Environment.NewLine & _
" LMFrom.CST_No as Loc_CSTNo, LMFrom.Excisable as loc_Excisable,LMFrom.Range_Address as Loc_range_Add,LMFrom.Division_Address  as loc_Division_Address,LMFrom.Commissionerate  as Loc_Commissionerate, '' as Challan_No, '' as Challan_Date, '' as Removal_Date, TSPL_CSA_TRANSFER_HEAD.WayBill_No," + Environment.NewLine & _
" TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(CM_Company.City_Name)>0 then ', '+CM_Company.City_Name else ' ' end + case when len(SMCompany.STATE_NAME  )>0 then ', '+ SMCompany.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No)>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end + case when len(TSPL_COMPANY_MASTER.Pan_No)>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address," + Environment.NewLine & _
" LMFrom .Add1 as Loc_Add1, LMFrom.Add2 as Loc_ADd2, LMFrom.Add3 as Loc_Add3, LMFrom.Pin_Code as Loc_Pin_Code, LMFrom.TIN_No as Loc_TinNo, Case when ISNULL(LMFrom.Phone1,'')='(+__)__________' then '' else LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ LMFrom.Phone2 Else'' End as  Loc_Phn, LMFrom.Email as Loc_Email, 'Excise Invoice' as Invoice_Type," + Environment.NewLine & _
" (case when len(isnull(TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code,''))>0 then round(TSPL_ITEM_UOM_DETAIL .Conversion_Factor * (TSPL_CSA_TRANSFER_DETAIL.Qty)/case when alt_convrsn.Conversion_Factor<=0 then 1 else alt_convrsn.Conversion_Factor end,2) else null end) as Alternet_Qty," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code as Alternate_UOM, 0 as Scheme_Qty, '' as Scheme_Item_UOM, TSPL_CSA_TRANSFER_HEAD.Discount_Base, TSPL_CSA_TRANSFER_HEAD.GR_No as Dis_Doc_No, TSPL_CSA_TRANSFER_HEAD.Description, TSPL_COMPANY_MASTER .State as Comp_State, '' as Buyer_order_no, '' as Buyer_order_date, '' as Terms_of_delivery, TSPL_CSA_TRANSFER_HEAD.DOC_CODE as InvoiceNo, TSPL_CSA_TRANSFER_HEAD.GR_No as GRNo, CONVERT(VARCHAR,TSPL_CSA_TRANSFER_HEAD.GR_Date,103) as Date_Time_Invoice, convert(varchar ,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as InvoiceDate, '' as ShipmentNo, 0 as Alt_Qty, TSPL_CSA_TRANSFER_DETAIL.Alt_Unit_Code as Alt_UOM, '' as ShipmentDate, '' as DeliveryOrderNo, '' as TermCondition, LMFrom.Location_Desc," + Environment.NewLine & _
" TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone, TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode, TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3, '' as P_Add1, '' as P_Add2, '' as P_Add3, '' as P_PinNo, '' as P_CstNo, '' as P_TinNo, '' as P_Email, '' as P_Fax, '' as P_LstNo, '' as P_CustCode, '' as P_Cust_Name, '' as P_City_Name, '' as P_State_Name, '' as P_Cust_Phn,LMTo .PAN as Customer_PAN, LMTo.Cust_Code as Cust_Code, LMTo.Customer_Name as Customer_Name, LMTo.Add1 as Cust_Add1, LMTo.Add2 as Cust_add2, LMTo.Add3 as cust_add3, case when ISNULL(LMTo.Phone1,'')='(+__)__________' then '' else LMTo.Phone1 end +  Case When   ISNULL(LMTo.Phone2,'')<>'(+__)__________' Then ', '+ LMTo.Phone2 Else'' End as Cust_Phn,LMTo.Tin_No  as Cust_TinNo, LMTo.CST as Cust_CSTNo, '' Cust_LSTNo, LMTo.Email as Cust_Email, LMTo.PIN_Code as Cust_PinCode, CMTo.City_Name as Cust_City_Name, '' as Cust_Fax, SMTo.STATE_NAME as Cust_State_Name, TSPL_CSA_TRANSFER_DETAIL.item_code, TSPL_ITEM_MASTER.Item_Desc as itemdesc, TSPL_CSA_TRANSFER_DETAIL.Qty, TSPL_CSA_TRANSFER_DETAIL.mrp, (TSPL_CSA_TRANSFER_DETAIL.Unit_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty) -TSPL_CSA_TRANSFER_DETAIL.Cash_Scheme_Amount as amount, TSPL_CSA_TRANSFER_DETAIL.unit_code as uom, '' as RATE_UOM, TSPL_CSA_TRANSFER_DETAIl.Unit_Rate as itemcost," + Environment.NewLine & _
" tax1.Tax_Code_Desc as tax1name,isnull (TSPL_CSA_TRANSFER_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name, isnull (TSPL_CSA_TRANSFER_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name, isnull (TSPL_CSA_TRANSFER_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name, isnull (TSPL_CSA_TRANSFER_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name, isnull (TSPL_CSA_TRANSFER_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name, isnull (TSPL_CSA_TRANSFER_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name, isnull (TSPL_CSA_TRANSFER_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_CSA_TRANSFER_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name, isnull (TSPL_CSA_TRANSFER_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name, isnull (TSPL_CSA_TRANSFER_HEAD.tax10_amt,0) as txt10amt," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate," + Environment.NewLine & _
" TSPL_CSA_TRANSFER_DETAIL.Disc_Per, isnull(TSPL_CSA_TRANSFER_HEAD.Discount_Amt,0) as Discount_Amt,isnull(TSPL_CSA_TRANSFER_HEAD.Amount_Less_Discount,0) as Amount_Less_Discount,  isnull(TSPL_CSA_TRANSFER_HEAD .Total_Tax_Amt,0) as Total_Tax_Amt, TSPL_CSA_TRANSFER_HEAD.Document_Amount as Total_Amt, '1' as CopyType," + Environment.NewLine & _
" '' as Add_Charge_Name1, 0 as Add_Charge_Amt1, '' as Add_Charge_Name2, 0 as Add_Charge_Amt2, '' as Add_Charge_Name3, 0 as Add_Charge_Amt3, '' as Add_Charge_Name4, 0 as Add_Charge_Amt4, '' as Add_Charge_Name5, 0 as Add_Charge_Amt5, '' as Add_Charge_Name6, 0 as Add_Charge_Amt6, '' as Add_Charge_Name7, 0 as Add_Charge_Amt7, '' as Add_Charge_Name8, 0 as Add_Charge_Amt8, '' as Add_Charge_Name9, 0 as Add_Charge_Amt9, '' as Add_Charge_Name10, 0 as Add_Charge_Amt10" + Environment.NewLine & _
                " from TSPL_CSA_TRANSFER_DETAIL" + Environment.NewLine & _
" join TSPL_CSA_TRANSFER_HEAD  on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_CSA_TRANSFER_DETAIL.iTEM_CODE" + Environment.NewLine & _
" left outer join TSPL_LOCATION_MASTER LMFrom on LMFrom.Location_Code=  TSPL_CSA_TRANSFER_HEAD.From_Location_Code" + Environment.NewLine & _
" left outer join TSPL_CUSTOMER_MASTER LMTo on LMTo.Cust_Code =  TSPL_CSA_TRANSFER_HEAD.Cust_Code" + Environment.NewLine & _
" Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_CSA_TRANSFER_HEAD.Comp_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CM_Company ON CM_Company.City_Code =TSPL_COMPANY_MASTER.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CMFrom ON CMFrom.City_Code =LMFrom.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_CITY_MASTER  AS CMTo ON CMTo.City_Code =LMTo.City_Code" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER AS SMCompany  ON SMCompany.STATE_CODE  =TSPL_COMPANY_MASTER.State" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER SMFrom on SMFrom.STATE_CODE=LMFrom.State" + Environment.NewLine & _
" LEFT OUTER JOIN TSPL_STATE_MASTER SMTo ON SMTo.State_Code=LMTo.State" + Environment.NewLine & _
" left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code and tspl_item_uom_detail.uom_code=TSPL_CSA_TRANSFER_DETAIL.unit_code" + Environment.NewLine & _
" left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code and alt_convrsn.uom_code=TSPL_CSA_TRANSFER_DETAIL.alt_unit_code" + Environment.NewLine & _
" left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.transport_id=TSPL_CSA_TRANSFER_HEAD.transport_id" + Environment.NewLine & _
" left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_CSA_TRANSFER_HEAD.tax1" + Environment.NewLine & _
" left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_CSA_TRANSFER_HEAD.tax2" + Environment.NewLine & _
" left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_CSA_TRANSFER_HEAD.TAX3" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_CSA_TRANSFER_HEAD.tax4" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_CSA_TRANSFER_HEAD.tax5" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX6" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX7" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX8" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX9" + Environment.NewLine & _
" left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_CSA_TRANSFER_HEAD.TAX10" + Environment.NewLine & _
" where 2=2 and  TSPL_CSA_TRANSFER_HEAD. DOC_CODE = '" & StrCode & "'" + Environment.NewLine & _
" ) XXX" + Environment.NewLine & _
" LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1 UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as CopyType1) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2"
                    dt = clsDBFuncationality.GetDataTable(strQuery)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Data found to print")
                    Else
                        Dim Qry2 As String = "select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as InvoiceNo, Abatement_Amt, TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc + '( MRP : ' +   ISNULL(convert(varchar,TSPL_CSA_TRANSFER_DETAIL.MRP),'') + '  Abatement : ' + convert(varchar,convert(int,100- Abatement_Pers)) + '%)' as Item_Desc," + Environment.NewLine & _
                    " TSPL_CSA_TRANSFER_DETAIL.TAX1, TSPL_CSA_TRANSFER_DETAIL.TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2, TSPL_CSA_TRANSFER_DETAIL.TAX2_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3 ,TSPL_CSA_TRANSFER_DETAIL.TAX3_Amt ,TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate," + Environment.NewLine & _
                    " TSPL_ITEM_MASTER.Cheapter_Heads, tax1.Tax_Code_Desc as tax1name,tax2.Tax_Code_Desc as tax2name,tax3.Tax_Code_Desc as tax3name " + Environment.NewLine & _
                        " from TSPL_CSA_TRANSFER_DETAIL" + Environment.NewLine & _
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code" + Environment.NewLine & _
                    " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_CSA_TRANSFER_DETAIL.tax1" + Environment.NewLine & _
                    " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_CSA_TRANSFER_DETAIL.tax2" + Environment.NewLine & _
                    " left outer join tspl_tax_master as tax3 on tax3.Tax_Code =TSPL_CSA_TRANSFER_DETAIL .TAX3" + Environment.NewLine & _
                    " LEFT OUTER JOIN TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE" + Environment.NewLine & _
                    " where TSPL_CSA_TRANSFER_HEAD.DOC_CODE ='" & StrCode & "'"
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                        frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "rptProductExciseTransfer", "Excise Transfer", "rptSubReportExciseTransferSaleInvoice.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
                    End If
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
