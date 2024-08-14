Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System
Imports XpertERPEngine
Public Class FrmAPInvoiceEntry
    Inherits FrmMainTranScreen
    ''Checkin Richa 19/06/2020
#Region "Variables"
    Const ReportID As String = "APInvoiceGrid"
    Dim GSTStatus As Boolean = False
    Dim EnableBankFromMaster As Boolean = False
    Public strAPInvoice As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "LNO"
    Const colACCode As String = "NAME"
    Const colACName As String = "QTY"
    Const colGLType As String = "colGLType"
    Const colAmt As String = "AMT"
    Const colDeductionCode As String = "DedCode"
    Const colDeductionDesc As String = "DedDesc"
    Const colDisPer As String = "DISPER"
    Const colDisAmt As String = "DISAMT"
    Const colAmtAfterDis As String = "AMTAFTERDIS"
    Const colLandedAmt As String = "LANDEDAMT"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colIsUnclaimedTax As String = "ISUNCLAIMEDTAX"
    Const colDocType As String = "DOCTYPE"
    Const colDocNo As String = "DOCNO"
    Const colRemarks As String = "REMARKS"
    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Const colAChgCode As String = "COLACCODE"
    Const colAChgName As String = "COLACNAME"
    Const colSACCode As String = "COLSACCODE"
    Const colSACName As String = "COLSACNAME"
    Const colAChgAmount As String = "COLACAMOUNT"
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colCostCenterCode As String = "colCostCenterCode"

    Const colHierarchyLevelNumber As String = "colHierarchyLevelNumber"
    Const colHierarchyLevel1 As String = "colHierarchyLevel1"
    Const colHierarchyLevel2 As String = "colHierarchyLevel2"
    Const colHierarchyLevel3 As String = "colHierarchyLevel3"
    Const colHierarchyLevel4 As String = "colHierarchyLevel4"
    '*************************02/04/2014**********************
    Const colchrValue As String = "COLCHRVALUE"
    Const colchrName As String = "COLCHRNAME"
    Const colchrcode As String = "COLCHRCODE"
    Const colitemcode As String = "COLITMCODE"
    Const colitemname As String = "COLITMNAME"
    Dim repoChrCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repochrName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repochrValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoItmCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoItmName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoAcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()

    '******************************************************************************

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoAdChagCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoAdChagName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim strQry As String
    Public Const DocTypeSaleInvoice As String = "Sale Invoice"
    Public Const DocTypeSalesReturn As String = "Sale Return"
    Public Const DocTypeCSATransfer As String = "CSA Transfer"
    Public Const DocTypeProductInvoice As String = "Product Invoice"
    Public Const DocTypeMaterialSale As String = "Material Sale"
    Public Const DocTypeSRN As String = "SRN"
    Public Const DocTypeAdjustment As String = "Adjustment"
    Public Const DocTypeTransfer As String = "Transfer"
    Public Shared Milk_PI_No As String = ""
    Public arrProvDocNo As List(Of String) = Nothing

    Dim isDeductionFinder As Boolean = False
    Dim dblPreviousTDSAmt As Double = 0
    Dim UDLPurchaseOrderthroughAP As Boolean = False
    Dim CheckDocAmountInAPInvoiceEntry As Boolean = False
    Dim FreightProvisionAccount As String = Nothing
    Dim SettingCostCenterlevel As Boolean = False
    Dim SettingCostCenter As Boolean = False
    Dim ERPStartDate As Date
    Private SettingAutoRoundOffSeprateAccountOnVendorTransaction As Boolean = False
    Dim CalculateProvisionOnGateePass As Integer = 0
    Dim SettShowMCCFinder As Boolean = False
    Public arrInvoiceNo As List(Of String) = Nothing
    Dim UseVLCUploaderCodeMPUploaderCodeInMCCProcurement As Boolean = False
    Dim FORMTYPE As String = Nothing
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False
#End Region

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If btnSave.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        If MyBase.isExport = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        CalculateProvisionOnGateePass = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateProvisionOnGateePass, clsFixedParameterCode.CalculateProvisionOnGateePass, Nothing))
        SettShowMCCFinder = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1)
        Try
            ERPStartDate = clsCommon.myCDate(objCommonVar.ERPStartDate)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Invalid ERP Start Date")
            Me.Close()
        End Try
        'BHA/01/10/18-000583 by balwinder on 04/10/2018
        SettingCostCenter = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowHierarchyAndCostCenterInAPInvoiceEntry, clsFixedParameterCode.ShowHierarchyAndCostCenterInAPInvoiceEntry, Nothing)) = 1)
        SettingCostCenterlevel = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableHirerachyCostCentre, clsFixedParameterCode.EnableHirerachyCostCentre, Nothing)) = 1)
        SettingAutoRoundOffSeprateAccountOnVendorTransaction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, Nothing)) = 1)
        UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, clsFixedParameterCode.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, Nothing)) = 1, True, False)
        EnableBankFromMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, Nothing)) = 1, True, False)
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        SetUserMgmtNew()
        SetMailRight()
        Setlength()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        ButtonToolTip.SetToolTip(btnViewTDSDetails, "Press Alt+V View TDS Details")

        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGridGL()
        LoadBlankGridTax()
        LoadRefDocumenType()
        LoadInvoiceType()
        AddNew()
        CheckDocAmountInAPInvoiceEntry = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckDocAmountInAPInvoiceEntry, clsFixedParameterCode.CheckDocAmountInAPInvoiceEntry, Nothing)) = 0, False, True)
        FreightProvisionAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FreightProvisionAccount, clsFixedParameterCode.FreightProvisionAccount, Nothing))
        If clsCommon.myLen(FreightProvisionAccount) > 0 Then
            FreightProvisionAccount = clsDBFuncationality.getSingleValue("select Account_Code from TSPL_GL_ACCOUNTS  where Account_Code='" + FreightProvisionAccount + "'")
            If clsCommon.myLen(FreightProvisionAccount) <= 0 Then
                clsCommon.MyMessageBoxShow("Please set correct Freight Provision Account in setting form(CTRL+ALT+SHIFT+F7)")
            End If
        End If
        txtTotalAmt.Visible = CheckDocAmountInAPInvoiceEntry
        'btnPrint.Visible = False
        txtlocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ) "))
        '' Anubhooti 17-Nov-2014 BM00000004657
        If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
            LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
        Else
            LblLocDesp.Text = ""
        End If

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency

        If clsCommon.myLen(strAPInvoice) > 0 Then
            LoadData(strAPInvoice)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        End If
        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        SettShowMCCFinder = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1)
        txtMCC.Visible = SettShowMCCFinder
        lblMCC.Visible = SettShowMCCFinder
        lblMCC2.Visible = SettShowMCCFinder

        ''richa agarwal 19 June 2020
        If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
            cboDocType.SelectedValue = "D"
            chkDeduction.Enabled = False
            cboDocType.Enabled = False
            chkDeduction.Checked = True
        Else
            chkDeduction.Enabled = True
            cboDocType.Enabled = True
        End If

        ''For Attachment
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        ''End of For Attachment

        If Not objCommonVar.IsDemoERP Then
            pnlPCJ.Visible = False
        End If

    End Sub

    Sub LoadITC_Elibible()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        If CboxITCType.SelectedIndex = 0 Then

            dr = dt.NewRow()
            dr("Code") = "ITC for Both Taxable or Non-Taxable"
            dr("Name") = "ITC for Both Taxable or Non-Taxable"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "ITC for 100% Taxable"
            dr("Name") = "ITC for 100% Taxable"
            dt.Rows.Add(dr)
        Else
            dr = dt.NewRow()
            dr("Code") = "ITC for 100% Non-Taxable"
            dr("Name") = "ITC for 100% Non-Taxable"
            dt.Rows.Add(dr)
        End If


        CboxITCCateogory.DataSource = dt
        CboxITCCateogory.ValueMember = "Code"
        CboxITCCateogory.DisplayMember = "Name"
    End Sub

    Sub setProvisionVisibility()
        grpProvision.Enabled = True
        btlShowProvision.Enabled = True
        btnProvSelect.Enabled = True
        dtpFromProv.Enabled = True
        dtpToProv.Enabled = True
        txtProvAmt.Enabled = True
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "I") = CompairStringResult.Equal Then
            chkProvision.Visible = True
        Else
            chkProvision.Visible = False
            chkProvision.Checked = False
            arrProvDocNo = Nothing
            arrInvoiceNo = Nothing
            dtpFromProv.Value = clsCommon.GETSERVERDATE()
            dtpToProv.Value = clsCommon.GETSERVERDATE()
        End If
        If chkProvision.Checked Then
            grpProvision.Visible = True
        Else
            grpProvision.Visible = False
            txtProvAmt.Text = ""
            dtpFromProv.Value = clsCommon.GETSERVERDATE()
            dtpToProv.Value = clsCommon.GETSERVERDATE()
            arrProvDocNo = Nothing
            '============Added by preeti gupta Against ticket no[UDL/22/02/19-000270]
            arrInvoiceNo = Nothing
        End If
        If chkProvision.Checked AndAlso clsCommon.myLen(txtlocation.Value) > 0 AndAlso clsCommon.myLen(TxtVendorNo.Value) > 0 AndAlso isNewEntry Then
            If gv1.Rows.Count > 0 Then
                If clsCommon.myLen(gv1.Rows(0).Cells(colACCode).Value) <= 0 Then
                    Try
                        ''RICHA UDL/26/02/19-000274 
                        'Dim qry As String = "select TSPL_PURCHASE_ACCOUNTS.Provision_Clearing from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) + "'"
                        Dim qry As String = "select ISNULL(TSPL_VENDOR_ACCOUNT_SET.Freight_Provision,'') from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Account =TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code where TSPL_VENDOR_MASTER.Vendor_Code ='" & TxtVendorNo.Value & "'"
                        Dim strAC As String = clsDBFuncationality.getSingleValue(qry)
                        If clsCommon.myLen(strAC) <= 0 Then
                            Throw New Exception("Please set Freight Provision account for vendor:" + TxtVendorNo.Value)
                        End If
                        strAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strAC, txtlocation.Value, True, Nothing)
                        gv1.CurrentRow = gv1.Rows(0)
                        gv1.CurrentColumn = gv1.Columns(colACCode)
                        gv1.Rows(0).Cells(colACCode).Value = strAC
                        gv1.CurrentRow = gv1.Rows(0)
                        gv1.CurrentColumn = gv1.Columns(colAmt)
                        gv1.Rows(0).Cells(colAmt).Value = clsCommon.myCdbl(txtProvAmt.Text)
                    Catch ex As Exception
                        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                    End Try
                End If
            End If
        End If


    End Sub

    Sub setDeductionVisibility()
        '' BM00000007571 Deduction will also be visible in CN like DN
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Then
            chkDeduction.Visible = True
        Else
            chkDeduction.Visible = False
            chkDeduction.Checked = False
        End If
        If chkDeduction.Checked Then
            pnlDeduction.Visible = True
            ChkSecurity.Visible = True
        Else
            pnlDeduction.Visible = False
            ChkSecurity.Checked = False
        End If
        LoadBlankGridGL()
        'gv1.Rows.AddNew()
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        '=======shivani
        Dim Currency As Integer = clsModuleCurrencyMapping.GetmulticurrencyDecimalPlaces()
        txtConversionRate.DecimalPlaces = clsCommon.myCdbl(Currency)
        '================
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True

            If clsCommon.myLen(Me.TxtVendorNo.Value) > 0 Then
                strq = "select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(Me.TxtVendorNo.Value) & "'"
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
        If clsCommon.myLen(clsCommon.myCstr(Me.TxtVendorNo.Value)) = 0 Then
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

    Public Sub Setlength()
        txtDesc.MaxLength = 250
        txtOrderNo.MaxLength = 30
        txtPONo.MaxLength = 30
        txtVendorInvoiceNo.MaxLength = 30
    End Sub

    Private Sub txtChangeVendorNo()
        If Not isInsideLoadData Then
            Dim qry As String = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc,Bank_Name,Bank_Code ,ifsc_code,Branch_Name,Account_No from TSPL_VENDOR_MASTER where Vendor_Code ='" + clsCommon.myCstr(TxtVendorNo.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                TxtVendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                txtACSet.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Account"))
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                'chkTDSProvision.Checked = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Provisional from TSPL_VENDOR_MASTER where Vendor_Code='" + TxtVendorNo.Value + "'")) = 1, True, False)
                txtBankCode.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                TxtBankName.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                TxtIFSCCode.Text = clsCommon.myCstr(dt.Rows(0)("ifsc_code"))
                txtbranchname.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
                txtVendor_Bank_ACNo.Text = clsCommon.myCstr(dt.Rows(0)("Account_No"))
            Else
                TxtVendorNo.Value = ""
                lblVendorName.Text = ""
                txtTermCode.Value = ""
                lblTermName.Text = ""
                txtACSet.Value = ""
                txtTaxGroup.Value = ""
                lblTaxGrpName.Text = ""
                txtBankCode.Text = ""
                TxtBankName.Text = ""
                TxtIFSCCode.Text = ""
                txtbranchname.Text = ""
                txtVendor_Bank_ACNo.Text = ""
            End If


            ''richa agarwal 08/07/2015 BM00000007279 
            SetVendorTDSDetails()
            'Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(TxtVendorNo.Value)
            'If objVendor IsNot Nothing Then
            '    qry = "select 1 from TSPL_PAYMENT_HEADER where Payment_Type='AV' and (TSPL_PAYMENT_HEADER.Posted='1' or TSPL_PAYMENT_HEADER.Posted='P') and  Vendor_Code='" + TxtVendorNo.Value + "' and isnull(Payment_Amount,0)-isnull(Total_Applied_Amount,0)>0"
            '    dt = clsDBFuncationality.GetDataTable(qry)
            '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '        If (clsCommon.MyMessageBoxShow("Advance TDS payment found.Do you want to Apply TDS on this Document", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
            '            btnViewTDSDetails.Enabled = False
            '            objRemittance = New clsRemittance()
            '            objRemittance.IsApplyTDS = False
            '            objRemittance.Deduction_Code = ""
            '            objRemittance.TDS_Per = 0
            '            objRemittance.Surcharge_Per = 0
            '            objRemittance.Edu_Cess_Per = 0
            '            objRemittance.Sec_Educess_Per = 0
            '            UpdateTDSAmount()
            '        Else
            '            btnViewTDSDetails.Enabled = True
            '        End If
            '    End If
            '    If True Then
            '    End If
            'End If
            '---------------------------
        End If
    End Sub

    Sub SetVendorTDSDetails()
        btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(TxtVendorNo.Value)
        If objVendor IsNot Nothing Then
            ''richa agarwal 26/04/2018
            Dim ISOverride As Integer = 0
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                objVendor.Nature_Of_Deduction = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Deduction_Code  from TSPL_REMITTANCE where Document_No ='" & txtDocNo.Value & "' "))
            End If
            If clsCommon.myLen(clsCommon.myCstr(objVendor.Nature_Of_Deduction)) <= 0 Then
                objVendor.Nature_Of_Deduction = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Deduction_Code,'')  from tspl_vendor_master where Vendor_Code ='" & TxtVendorNo.Value & "'"))
            End If
            ''-------------------------
            btnViewTDSDetails.Enabled = True
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + TxtVendorNo.Value + "'")
            Dim appAmt As Double = 0
            If (clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal) Then
                appAmt = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            Else
                appAmt = clsCommon.myCdbl(lblTotRAmt.Text)
            End If
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, appAmt, Nothing, False, TxtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                ''By Balwinder on 09/11/2016 against ticket no BM00000010070
                Dim isApplyTDS As Boolean = False
                Dim qry As String = "select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" + txtDate.Value + "',103)>=  convert(date,Start_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,End_Date,103) "
                Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
                    Throw New Exception("Please make fiscal year where document date exists")
                End If

                ''Check if any TDS entry found in Document Fiscal Year
                qry = "select top 1 Remittance_Code from TSPL_REMITTANCE  where Vendor_Code='" + TxtVendorNo.Value + "' and convert(date, Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and Document_No not in ('" + txtDocNo.Value + "')"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    isApplyTDS = True
                Else
                    qry = "select Cumm_Cutoff,Cumm_Cutoff_Document from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + objVendor.Nature_Of_Deduction + "'"
                    dtTemp = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) <= 0 AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) <= 0 Then
                            isApplyTDS = True
                        Else
                            qry = "select sum( " + IIf(clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal, "TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount", "TSPL_VENDOR_INVOICE_HEAD.Document_Total") + ") as Document_Total from TSPL_VENDOR_INVOICE_HEAD where Vendor_Code='" + TxtVendorNo.Value + "' and Document_Type in ('I','C') and Document_No not in ('" + txtDocNo.Value + "') and  convert(date, Invoice_Entry_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Invoice_Entry_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null "
                            dblPreviousTDSAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If appAmt >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) > 0 Then
                                isApplyTDS = True
                            ElseIf (dblPreviousTDSAmt + appAmt) >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) > 0 Then
                                isApplyTDS = True
                            End If
                        End If
                    End If
                End If

                If isApplyTDS Then
                    objRemittance = New clsRemittance()
                    objRemittance.Branch_Code = objVendor.Branch_Code
                    objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                    objRemittance.TDS_Per = objDedDetails.TDS
                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                    If isNewEntry Then
                        objRemittance.IsApplyTDS = True
                    Else
                        objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                    End If
                    objRemittance.Section_Code = objVendor.TDSSection
                    objRemittance.Section_Description = objVendor.TDSSectionDescription
                    objRemittance.Select_By = objVendor.VendorTypeCode
                    'objRemittance.Include_Tax = objVendor.Include_Tax
                    ''richa agarwal 26/04/2018
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select case when Actual_TDS_Base=Document_Amount then 0 else 1  end as isoverride,Actual_TDS,Actual_TDS_Base ,Actual_Total_TDS  from TSPL_REMITTANCE where Document_No ='" & txtDocNo.Value & "' ")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objRemittance.Actual_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS_Base"))
                        objRemittance.Actual_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_TDS"))
                        objRemittance.Actual_Total_TDS = clsCommon.myCdbl(dt.Rows(0)("Actual_Total_TDS"))
                        ISOverride = clsCommon.myCdbl(dt.Rows(0)("isoverride"))
                    End If
                    If ISOverride = 1 Then
                        objRemittance.IsTDSOverride = True
                    Else
                        objRemittance.IsTDSOverride = False
                    End If
                    ''-----------------------------

                    objRemittance.Fiscal_Year = clsCommon.myCstr(dtFY.Rows(0)("Fiscal_Code"))
                    objRemittance.Quarter = "First"
                End If
            End If
        End If
    End Sub

    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        End If
        If (objRemittance IsNot Nothing) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + TxtVendorNo.Value + "'")
            Dim applicableAmt As Double = 0
            If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
                applicableAmt = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            Else
                applicableAmt = clsCommon.myCdbl(lblTotRAmt.Text)
            End If
            applicableAmt += dblPreviousTDSAmt


            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, applicableAmt, Nothing, False, TxtVendorNo.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If

            objRemittance.Vendor_Code = TxtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
            objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
            objRemittance.Calculated_TDS_Base = applicableAmt
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = applicableAmt
            End If

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
    End Sub

    Private Sub txtTaxGroup_TxtChanged()
        If Not isInsideLoadData AndAlso iStxtTaxGroup_TxtChangedComplete Then
            iStxtTaxGroup_TxtChangedComplete = False
            LoadBlankGridTax()
            Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If (dt.Rows.Count > 10) Then
                    MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                    Return
                End If
                Dim ii As Integer = 0
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Code"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                For Each dr As DataRow In dt.Rows
                    gv2.Rows.AddNew()
                    gv2.Rows(ii).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                    gv2.Rows(ii).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                    If rbtnTaxCalAutomatic.IsChecked Then
                        gv2.Rows(ii).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                        'gv2.Rows(ii).Cells(colTIsSurTax).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        'gv2.Rows(ii).Cells(colTIsTaxable).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        'gv2.Rows(ii).Cells(colTSurTaxCode).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                    ElseIf rbtnTaxCalManual.IsChecked Then
                        gv2.Rows(ii).Cells(colTTaxRate).Value = Nothing
                        'gv2.Rows(ii).Cells(colTIsSurTax).Value = Nothing
                        'gv2.Rows(ii).Cells(colTIsTaxable).Value = Nothing
                        'gv2.Rows(ii).Cells(colTSurTaxCode).Value = Nothing
                    End If
                    ii = ii + 1
                Next
                SetitemWiseTaxSetting(True, False)
            Else
                lblTaxGrpName.Text = ""
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRowWihtRowNo(ii)
            Next
            UpdateAllTotals()
            iStxtTaxGroup_TxtChangedComplete = True
        End If
    End Sub

    Private Sub txtTermCode_TxtChanged()

        If Not isInsideLoadData Then
            Dim qry As String = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(txtTermCode.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'txtTermCode.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                txtDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
            Else
                lblTermName.Text = ""
            End If
        End If
    End Sub

    Sub LoadInvoiceType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "I"
        dr("Name") = "Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Debit Note"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Credit Note"
        dt.Rows.Add(dr)

        '---------- added parteek 23-03-2016 related direct ap 
        UDLPurchaseOrderthroughAP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UDLPurchaseOrderthroughAP, clsFixedParameterCode.UDLPurchaseOrderthroughAP, Nothing)) = 1, True, False)
        If UDLPurchaseOrderthroughAP = True Then
            dr = dt.NewRow()
            dr("Code") = "P"
            dr("Name") = "Purchase"
            dt.Rows.Add(dr)
        End If

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"

    End Sub

    Sub LoadRefDocumenType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SRN No"
        dt.Rows.Add(dr)

        ''dr = dt.NewRow()
        ''dr("Code") = "LO"
        ''dr("Name") = "Loadout"
        ''dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AP"
        dr("Name") = "AP Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "API"
        dr("Name") = "All AP Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "WO"
        dr("Name") = "Work Order"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CH"
        dr("Name") = "Charges"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DED-MAP"
        dr("Name") = "Deduction Mapping"
        dt.Rows.Add(dr)

        cmbRefType.DataSource = dt
        cmbRefType.ValueMember = "Code"
        cmbRefType.DisplayMember = "Name"

    End Sub

    Sub LoadRefDocTypeForDC()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AP"
        dr("Name") = "AP Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "API"
        dr("Name") = "All AP Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CH"
        dr("Name") = "Charges"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "BS"
        dr("Name") = "Bulk SRN"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "WO"
        dr("Name") = "Work Order"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DED-MAP"
        dr("Name") = "Deduction Mapping"
        dt.Rows.Add(dr)

        cmbRefType.DataSource = dt
        cmbRefType.ValueMember = "Code"
        cmbRefType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        dblPreviousTDSAmt = 0
        txtAdd_Doc_TYpe.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        TxtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtACSet.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        cboDocType.SelectedIndex = 0
        txtPONo.Text = ""
        txtOrderNo.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblRoundOff.Text = ""
        lblTotRAmt.Text = ""
        lblTotRAmt1.Text = ""
        txtVendorInvoiceNo.Text = ""
        txtVendorInvDatre.Value = txtDate.Value
        cmbRefType.SelectedIndex = 0
        txtRefDocNo.Value = ""
        rbtnTaxCalAutomatic.IsChecked = True
        lblTotEmptyAmt.Text = ""
        lblProject.Text = ""
        fndProject.Value = ""
        fndProject.Enabled = True
        lblProject.Enabled = True

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        txtAmtAfterCashDiscount.Text = ""
        TxtCashDiscount.Text = 0
        chkITCEligible.Checked = False
        CboxITCType.SelectedIndex = 0
        Dim EmployeeSalaryGeneration As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEmployeeSalaryIntegration, clsFixedParameterCode.AllowtoEmployeeSalaryIntegration, Nothing)) = 1, True, False))
        If EmployeeSalaryGeneration = True Then
            loadEmployeeType()
            ddlEmployeeType.Visible = True
            MyLabel13.Visible = True
        Else
            loadEmployeeType()
            ddlEmployeeType.Visible = False
            MyLabel13.Visible = False
        End If
        txtDataAndTimeSelection.Value = clsCommon.GETSERVERDATE()
        txtTapalNo.Text = ""
        txtDataAndTimeSelection.Checked = False
    End Sub

    Private Sub TxtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVendorNo._MYValidating
        Try
            If txtlocation.Value = "" Then
                'If clsCommon.myLen(TxtVendorNo.Value) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
                TxtVendorNo.Value = ""
                txtlocation.Focus()
                Exit Sub
                'End If
            End If
            'Dim Qry As String = clsERPFuncationality.glvendorqueryNew
            Dim Qry As String = " select M.Vendor_Code AS [Code], m.Vendor_Name as [Name],ISNULL(m.alies_name,'') As [Alies Name],TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [Plant Name],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code " &
                               " left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = M.Vendor_Code " &
                               " Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC " &
                               " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code "

            TxtVendorNo.Value = clsCommon.ShowSelectForm("VendSelectfnd", Qry, "Code", " m.Status='N'", TxtVendorNo.Value, "Code", isButtonClicked)
            txtChangeVendorNo()
            txtTaxGroup_TxtChanged()
            txtTermCode_TxtChanged()
            SetMultiCurrencyVisibility()
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsEmployee from TSPL_VENDOR_MASTER where Vendor_Code ='" & TxtVendorNo.Value & "' ")), "1") = CompairStringResult.Equal Then
                ddlEmployeeType.Enabled = True
                loadEmployeeType()
            Else
                ddlEmployeeType.Enabled = False
                loadEmployeeType()
            End If
            FillVendorDetails()

            ''richa 05 feb,2019  TEC/05/02/19-000411 not check for opening in case of Credit or debit note 
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
            Dim JEWithOPening As Boolean = False
            If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                    JEWithOPening = True
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, Nothing)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal) And JEWithOPening = True Then
                LoadBlankGridGL()
                gv1.Rows.AddNew()
            End If
            txtSecurityAdjusment.arrValueMember = Nothing
        Catch ex As Exception
            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendorNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtVendorNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtACSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtACSet._MYValidating
        Dim Qry As String = "select Acct_Set_Code as Code ,Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtACSet.Value = clsCommon.ShowSelectForm("AccFiltrFND", Qry, "Code", "", txtACSet.Value, "Code", isButtonClicked)

    End Sub

    Private Sub txtACSet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtACSet.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        'Dim Qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("TaxGrpSFND", Qry, "Code", " Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        'txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtlocation.Value, TxtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)

        'txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupFinance("P", txtTaxGroup.Value, isButtonClicked)
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupLocationSegment(txtlocation.Value, TxtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)
        txtTaxGroup_TxtChanged()
    End Sub

    Private Sub txtTaxGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxGroup.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim Qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("TermCodeFNDD", Qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
        txtTermCode_TxtChanged()
    End Sub

    Private Sub txtTermCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTermCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Function GetDocType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = DocTypeSaleInvoice
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = DocTypeSalesReturn
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeCSATransfer
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeProductInvoice
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = DocTypeSRN
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = DocTypeAdjustment
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeTransfer
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = DocTypeMaterialSale
        'dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGridGL()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        repoChrCode = New GridViewTextBoxColumn()
        repochrName = New GridViewTextBoxColumn()
        repochrValue = New GridViewTextBoxColumn()
        repoItmCode = New GridViewTextBoxColumn()
        repoItmName = New GridViewTextBoxColumn()
        repoAcCode = New GridViewTextBoxColumn()
        Dim repoDedcutionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoDedcutionDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoHierarchyCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        '***************************add new columns for refdoctype charges*********02/04/2014*******************

        repoChrCode.FormatString = ""
        repoChrCode.HeaderText = "Charge Category Code"
        repoChrCode.Name = colchrcode
        repoChrCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoChrCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoChrCode.Width = 150
        'repoChrCode.Index = 0
        repoChrCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoChrCode)


        repochrName.FormatString = ""
        repochrName.HeaderText = "Charge Category Description"
        repochrName.Name = colchrName
        repochrName.Width = 150
        'repochrName.Index = 1
        repochrName.ReadOnly = True
        repochrName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repochrName)

        repoItmCode.FormatString = ""
        repoItmCode.HeaderText = "Item Code"
        repoItmCode.Name = colitemcode
        repoItmCode.Width = 150
        repoItmCode.IsVisible = False
        'repochrValue.Index = 2
        repoItmCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItmCode)

        repoItmName.FormatString = ""
        repoItmName.HeaderText = "Item Description"
        repoItmName.Name = colitemname
        repoItmName.Width = 150
        repoItmName.IsVisible = False
        'repochrValue.Index = 2
        repoItmName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItmName)


        repochrValue.FormatString = ""
        repochrValue.HeaderText = "Charges"
        repochrValue.Name = colchrValue
        repochrValue.Width = 150
        repochrValue.IsVisible = False
        'repochrValue.Index = 2
        repochrValue.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repochrValue)

        '************************************************************************************************

        repoAdChagCode = New GridViewTextBoxColumn()
        repoAdChagCode.FormatString = ""
        repoAdChagCode.HeaderText = "Additional Charges"
        repoAdChagCode.Name = colAChgCode
        repoAdChagCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAdChagCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAdChagCode.Width = 150
        repoAdChagCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagCode)

        repoAdChagName = New GridViewTextBoxColumn()
        repoAdChagName.FormatString = ""
        repoAdChagName.HeaderText = "Additional Charges Description"
        repoAdChagName.Name = colAChgName
        repoAdChagName.Width = 150
        repoAdChagName.ReadOnly = True
        repoAdChagName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagName)

        repoAdChagCode = New GridViewTextBoxColumn()
        repoAdChagCode.FormatString = ""
        repoAdChagCode.HeaderText = "SAC Code"
        repoAdChagCode.Name = colSACCode
        repoAdChagCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAdChagCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAdChagCode.Width = 100
        repoAdChagCode.ReadOnly = True
        repoAdChagCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagCode)

        repoAdChagName = New GridViewTextBoxColumn()
        repoAdChagName.FormatString = ""
        repoAdChagName.HeaderText = "SAC Description"
        repoAdChagName.Name = colSACName
        repoAdChagName.Width = 150
        repoAdChagName.ReadOnly = True
        repoAdChagName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagName)

        repoDedcutionCode.FormatString = ""
        repoDedcutionCode.HeaderText = "Deduction Code"
        repoDedcutionCode.Name = colDeductionCode
        repoDedcutionCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDedcutionCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDedcutionCode.Width = 150
        repoDedcutionCode.IsVisible = chkDeduction.Checked
        gv1.MasterTemplate.Columns.Add(repoDedcutionCode)


        repoDedcutionDesc.FormatString = ""
        repoDedcutionDesc.HeaderText = "Deduction Desc"
        repoDedcutionDesc.Name = colDeductionDesc
        repoDedcutionDesc.Width = 150
        repoDedcutionDesc.ReadOnly = True
        repoDedcutionDesc.IsVisible = chkDeduction.Checked
        gv1.MasterTemplate.Columns.Add(repoDedcutionDesc)

        repoAcCode.FormatString = ""
        repoAcCode.HeaderText = "GL Account"
        repoAcCode.Name = colACCode
        repoAcCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAcCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAcCode.Width = 150
        repoAcCode.ReadOnly = chkDeduction.Checked
        gv1.MasterTemplate.Columns.Add(repoAcCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Account Description"
        repoACName.Name = colACName
        repoACName.Width = 150
        repoACName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoACName)

        Dim repoGLType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGLType.FormatString = ""
        repoGLType.HeaderText = "GL Type"
        repoGLType.Name = colGLType
        repoGLType.Width = 100
        repoGLType.ReadOnly = True
        repoGLType.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoGLType)

        Dim repoAmt As GridViewCalculatorColumn = New GridViewCalculatorColumn()
        'repoAmt = New GridViewCalculatorColumn
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        'repoAmt.Minimum = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 100
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.Width = 100
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)


        Dim repoLandedamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedamt.FormatString = ""
        repoLandedamt.HeaderText = "Landed Amount"
        repoLandedamt.Name = colLandedAmt
        repoLandedamt.Width = 100
        repoLandedamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedamt.VisibleInColumnChooser = False
        repoLandedamt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedamt)

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

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1)

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


        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

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


        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

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


        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

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


        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)


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


        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

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

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

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

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

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

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

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
        repoTotTaxAmt.Width = 100
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.Width = 120
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

        Dim repoIsUnclaimedTax As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsUnclaimedTax.HeaderText = "Unclaimed Tax"
        repoIsUnclaimedTax.Name = colIsUnclaimedTax
        repoIsUnclaimedTax.Width = 90
        repoIsUnclaimedTax.ReadOnly = False
        repoIsUnclaimedTax.IsVisible = True
        repoIsUnclaimedTax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsUnclaimedTax)

        Dim repoDocType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDocType.FormatString = ""
        repoDocType.HeaderText = "Document Type"
        repoDocType.Name = colDocType
        repoDocType.Width = 100
        repoDocType.ReadOnly = False
        repoDocType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDocType.DataSource = GetDocType()
        repoDocType.ValueMember = "Code"
        repoDocType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoDocType)

        Dim repoInviceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInviceNo.FormatString = ""
        repoInviceNo.HeaderText = "Document No"
        repoInviceNo.Name = colDocNo
        repoInviceNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoInviceNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoInviceNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoInviceNo)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.IsVisible = True
        repoInviceNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)


        repoHierarchyCode.FormatString = ""
        repoHierarchyCode.HeaderText = "Hierarchy Level"
        repoHierarchyCode.Name = colHierarchyCode
        repoHierarchyCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoHierarchyCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchyCode.Width = 150
        repoHierarchyCode.IsVisible = (SettingCostCenter Or SettingCostCenterlevel)
        gv1.MasterTemplate.Columns.Add(repoHierarchyCode)

        repoCostCenterCode.FormatString = ""
        repoCostCenterCode.HeaderText = "Cost Center"
        repoCostCenterCode.Name = colCostCenterCode
        repoCostCenterCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCostCenterCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCostCenterCode.Width = 150
        repoCostCenterCode.IsVisible = IIf(SettingCostCenterlevel, False, IIf(SettingCostCenter, True, False))
        gv1.MasterTemplate.Columns.Add(repoCostCenterCode)


        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Hierarchy Level Number"
        repoAmtAfterTax.Name = colHierarchyLevelNumber
        repoAmtAfterTax.IsVisible = SettingCostCenterlevel
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

        Dim repoHierarchyCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()

        repoHierarchyCode3 = New GridViewTextBoxColumn()
        repoHierarchyCode3.FormatString = ""
        repoHierarchyCode3.HeaderText = "Hierarchy Level 4"
        repoHierarchyCode3.Name = colHierarchyLevel4
        repoHierarchyCode3.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoHierarchyCode3.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchyCode3.Width = 150
        repoHierarchyCode3.IsVisible = SettingCostCenterlevel
        repoHierarchyCode3.Tag = 4
        gv1.MasterTemplate.Columns.Add(repoHierarchyCode3)

        repoHierarchyCode3 = New GridViewTextBoxColumn()
        repoHierarchyCode3.FormatString = ""
        repoHierarchyCode3.HeaderText = "Hierarchy Level 3"
        repoHierarchyCode3.Name = colHierarchyLevel3
        repoHierarchyCode3.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoHierarchyCode3.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchyCode3.Width = 150
        repoHierarchyCode3.IsVisible = SettingCostCenterlevel
        repoHierarchyCode3.Tag = 3
        gv1.MasterTemplate.Columns.Add(repoHierarchyCode3)

        repoHierarchyCode3 = New GridViewTextBoxColumn()
        repoHierarchyCode3.FormatString = ""
        repoHierarchyCode3.HeaderText = "Hierarchy Level 2"
        repoHierarchyCode3.Name = colHierarchyLevel2
        repoHierarchyCode3.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoHierarchyCode3.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchyCode3.Width = 150
        repoHierarchyCode3.IsVisible = SettingCostCenterlevel
        repoHierarchyCode3.Tag = 2
        gv1.MasterTemplate.Columns.Add(repoHierarchyCode3)



        repoHierarchyCode3 = New GridViewTextBoxColumn()
        repoHierarchyCode3.FormatString = ""
        repoHierarchyCode3.HeaderText = "Hierarchy Level 1"
        repoHierarchyCode3.Name = colHierarchyLevel1
        repoHierarchyCode3.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoHierarchyCode3.TextImageRelation = TextImageRelation.TextBeforeImage
        repoHierarchyCode3.Width = 150
        repoHierarchyCode3.IsVisible = SettingCostCenterlevel
        repoHierarchyCode3.Tag = 1
        gv1.MasterTemplate.Columns.Add(repoHierarchyCode3)








        RefreshHeaderText()

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub RefreshHeaderText()
        If SettingCostCenterlevel Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Description from TSPL_HIRERACHY_LEVEL_MASTER  order by level")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    gv1.Columns(colHierarchyLevel1).HeaderText = clsCommon.myCstr(dt.Rows(0)("Description"))
                End If
                If dt.Rows.Count > 1 Then
                    gv1.Columns(colHierarchyLevel2).HeaderText = clsCommon.myCstr(dt.Rows(1)("Description"))
                End If
                If dt.Rows.Count > 2 Then
                    gv1.Columns(colHierarchyLevel3).HeaderText = clsCommon.myCstr(dt.Rows(2)("Description"))
                End If
                If dt.Rows.Count > 3 Then
                    gv1.Columns(colHierarchyLevel4).HeaderText = clsCommon.myCstr(dt.Rows(3)("Description"))
                End If
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

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        'repoTaxRate.ReadOnly = True
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        'Dim repoIsSurTax As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoIsSurTax.HeaderText = "Is Surtax"
        'repoIsSurTax.Name = colTIsSurTax
        'repoIsSurTax.Width = 80
        'repoIsSurTax.ReadOnly = True
        'repoIsSurTax.IsVisible = False
        'repoIsSurTax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        'gv2.MasterTemplate.Columns.Add(repoIsSurTax)

        'Dim repoSurTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSurTaxCode.FormatString = ""
        'repoSurTaxCode.HeaderText = "Surtax"
        'repoSurTaxCode.Name = colTSurTaxCode
        'repoSurTaxCode.Width = 100
        'repoSurTaxCode.ReadOnly = True
        'repoSurTaxCode.IsVisible = False
        'gv2.MasterTemplate.Columns.Add(repoSurTaxCode)

        'Dim repoIsTaxable As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoIsTaxable.HeaderText = "Is Taxable"
        'repoIsTaxable.Name = colTIsTaxable
        'repoIsTaxable.Width = 80
        'repoIsTaxable.ReadOnly = True
        'repoIsTaxable.IsVisible = False
        'repoIsTaxable.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        'gv2.MasterTemplate.Columns.Add(repoIsTaxable)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)


        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        ''repoTaxAmt.Minimum = 0
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
                    If ((clsCommon.CompairString(e.Column.Name, colAmt) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colchrcode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colIsUnclaimedTax) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colACCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colAChgCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDocNo) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDeductionCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colHierarchyCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colCostCenterCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colHierarchyLevel3) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colHierarchyLevel4) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colHierarchyLevel2) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colHierarchyLevel1) = CompairStringResult.Equal)) Then
                        If ((clsCommon.CompairString(e.Column.Name, colAmt) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colIsUnclaimedTax) = CompairStringResult.Equal)) Then
                            If clsCommon.CompairString(e.Column.Name, colAmt) = CompairStringResult.Equal Then
                                OpenSecondaryTransporterDeduction()
                            End If
                            If rbtnTaxCalAutomatic.IsChecked Then
                                UpdateCurrentRowWihtRowNo(gv1.CurrentRow.Index)
                            ElseIf rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRowWihtRowNo(ii)
                                Next
                            End If
                            UpdateAllTotals()

                        ElseIf (clsCommon.CompairString(e.Column.Name, colACCode) = CompairStringResult.Equal) AndAlso cmbRefType.Text <> "Charges" Then
                            If Not isDeductionFinder Then
                                OpenGLAccount(False)
                            End If
                        ElseIf (clsCommon.CompairString(e.Column.Name, colDeductionCode) = CompairStringResult.Equal) Then
                            OpenDeduction(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colAChgCode) = CompairStringResult.Equal) Then
                            OpenAdditionCharges(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colDocNo) = CompairStringResult.Equal) Then
                            OpenInvoiceNo(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colchrcode) = CompairStringResult.Equal) Then
                            OpenChargeCategory(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colHierarchyCode) = CompairStringResult.Equal) Then
                            OpenHierarchyCode(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colCostCenterCode) = CompairStringResult.Equal) Then
                            OpenCostCenterCode(False)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colHierarchyLevel4) = CompairStringResult.Equal) Then
                            OpenHirerchyALLlevel(4)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colHierarchyLevel3) = CompairStringResult.Equal) Then
                            OpenHirerchyALLlevel(3)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colHierarchyLevel2) = CompairStringResult.Equal) Then
                            OpenHirerchyALLlevel(2)
                        ElseIf (clsCommon.CompairString(e.Column.Name, colHierarchyLevel1) = CompairStringResult.Equal) Then
                            OpenHirerchyALLlevel(1)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Private Sub OpenHirerchyALLlevel(ByVal lvl As Integer)
        If SettingCostCenterlevel Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") <> CompairStringResult.Equal Then
                    Dim dr As DataRow = OpenHirerchylevelFinder(lvl)
                    If dr IsNot Nothing Then
                        Select Case lvl
                            Case 1
                                gv1.CurrentRow.Cells(colHierarchyLevel1).Value = clsCommon.myCstr(dr(0))
                            Case 2
                                gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dr(0))
                                gv1.CurrentRow.Cells(colHierarchyLevel1).Value = clsCommon.myCstr(dr(1))
                            Case 3
                                gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dr(0))
                                gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dr(1))
                                gv1.CurrentRow.Cells(colHierarchyLevel1).Value = clsCommon.myCstr(dr(2))
                            Case 4
                                gv1.CurrentRow.Cells(colHierarchyLevel4).Value = clsCommon.myCstr(dr(0))
                                gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dr(1))
                                gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dr(2))
                                gv1.CurrentRow.Cells(colHierarchyLevel1).Value = clsCommon.myCstr(dr(3))
                        End Select
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
            End If
        End If
    End Sub

    Function OpenHirerchylevelFinder(ByVal lvl As Integer) As DataRow
        Dim qry As String = " select "
        Select Case lvl
            Case 1
                qry += " TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel1).HeaderText) + "],TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as [Description] "
            Case 2
                qry += " COST_CENTRE_HIRERACHY_CODE as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel2).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel1).HeaderText) + "],TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as [Description] "
            Case 3
                qry += " COST_CENTRE_HIRERACHY_CODE as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel3).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel2).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel1).HeaderText) + "],TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as [Description] "
            Case 4
                qry += " COST_CENTRE_HIRERACHY_CODE as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel4).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel3).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel2).HeaderText) + "],TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 as [" + clsCommon.myCstr(gv1.Columns(colHierarchyLevel1).HeaderText) + "],TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as [Description] "
            Case Else
                Throw New Exception("Wrong level ")
        End Select
        qry += " from TSPL_COST_CENTRE_HIRERACHY_DETAIL left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE where TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL=" + clsCommon.myCstr(lvl)

        Return clsCommon.ShowSelectFormForRow("DDAdde" + clsCommon.myCstr(lvl), qry)
    End Function



    Private Sub OpenCostCenterCode(ByVal isButtonClick As Boolean)
        If SettingCostCenter Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") <> CompairStringResult.Equal Then
                    Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                    gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", " Hirerachy_Level = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "Code", isButtonClick)
                End If
            Else
                clsCommon.MyMessageBoxShow("Please select hirerachy level first.", Me.Text)
            End If
        End If
    End Sub


    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") <> CompairStringResult.Equal Then
            Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
            gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))

            gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
            gv1.CurrentRow.Cells(colHierarchyLevel1).Value = ""
            gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
            gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
            gv1.CurrentRow.Cells(colHierarchyLevel4).Value = ""
        End If
    End Sub


    Sub OpenSecondaryTransporterDeduction()
        If chkSecTrans.Checked AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colDeductionCode).Value) > 0 AndAlso clsCommon.myLen(TxtVendorNo.Value) > 0 Then
            Dim frm As New frmSecondaryTranporterDispatch()
            frm.strDeductionCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colDeductionCode).Value)
            frm.strDeductionName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDeductionDesc).Value)
            frm.strSegmentCode = txtlocation.Value
            frm.strSegmentName = LblLocDesp.Text
            frm.dblAMT = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value)
            frm.dtFrom = txtFromDateSecTrans.Value
            frm.dtTo = txtToDateSecTrans.Value
            frm.strVendorCode = TxtVendorNo.Value
            frm.strVendorName = lblVendorName.Text
            frm.arr = TryCast(gv1.CurrentRow.Cells(colDeductionCode).Tag, List(Of clsAPSecondaryTranporterDeductionDetail))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Cells(colDeductionCode).Tag = frm.arr
            End If
        End If
    End Sub

    Private Sub ApplyQuickMode()
        If chkQuickMode.Checked Then
            If gv1.RowCount - 1 > gv1.CurrentRow.Index Then
                If clsCommon.myLen(gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value) <= 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
                    gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
                    gv1.Rows(gv1.RowCount - 1).Cells(colACName).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
                    If clsCommon.myLen(gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value) > 0 Then
                        gv1.Rows(gv1.RowCount - 1).Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value), Nothing)
                    Else
                        gv1.Rows(gv1.RowCount - 1).Cells(colGLType).Value = ""
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            If chkQuickMode.Checked Then
                gv1.CurrentRow.Cells(colACCode).Value = gv1.Rows(intCurrRow).Cells(colACCode).Value
                gv1.CurrentRow.Cells(colACName).Value = gv1.Rows(intCurrRow).Cells(colACName).Value
            End If
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colACCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colAmt)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colAmt) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisPer)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colACCode)
            End If
        End If

    End Sub

    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Dim qry As String
        Dim whrcls As String
        Dim arr As New ArrayList()
        If txtlocation.Value = "" Then
            clsCommon.MyMessageBoxShow("Please first select Location", Me.Text)
            Return
        End If
        arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        qry = arr.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        whrcls = arr.Item(1)

        If whrcls = "" Then

        Else
            whrcls = "(" + whrcls + ")"
        End If
        If whrcls Is Nothing OrElse whrcls = "" Then
            whrcls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        Else
            whrcls = whrcls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        End If
        whrcls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtlocation.Value + "'  and TSPL_GL_ACCOUNTS.ControlAccount='N'  "
        If chkProvision.Checked OrElse clsCommon.GetDateWithStartTime(txtDate.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
            whrcls = clsCommon.ReplaceString(whrcls, "TSPL_GL_ACCOUNTS.ControlAccount='N'", " 2=2 ")
            whrcls = clsCommon.ReplaceString(whrcls, "ControlAccount ='N'", " 2=2 ")
            whrcls = clsCommon.ReplaceString(whrcls, "( ControlAccount ='N')", " ")
        End If
        ''richa 05 feb,2019  TEC/05/02/19-000411 not check for opening in case of Credit or debit note 
        'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        Dim JEWithOPening As Boolean = False
        If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
            If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                JEWithOPening = True
            End If
        End If
        Dim strCustomerOpeningAccount As String = String.Empty
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, Nothing)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal) And JEWithOPening = True Then
            If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please first select Vendor", Me.Text)
                TxtVendorNo.Focus()
                Return
            End If
            strCustomerOpeningAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select left(Opening_Clearing,len(Opening_Clearing)-4)  from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code  in (select Vendor_Account from TSPL_vendor_MASTER where Vendor_Code='" & TxtVendorNo.Value & "')"))
            If clsCommon.myLen(strCustomerOpeningAccount) = 0 Then
                clsCommon.MyMessageBoxShow("Please set Vendor Opening Clearing Account for Vendor - " + TxtVendorNo.Value)
                Return
            End If
            whrcls += " and Account_Seg_Code1='" & strCustomerOpeningAccount & "'"
        End If
        ''gv1.CurrentColumn = gv1.Columns(colAmt)
        'richa 17 SEp,2019 TEC/03/07/19-000927

        Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrcls & Environment.NewLine &
          " UNION All " & Environment.NewLine &
          " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine &
" left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine &
" and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine &
  " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine &
  " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine &
  " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1) and  TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtlocation.Value + "' "

        If clsCommon.myLen(strCustomerOpeningAccount) > 0 Then
            strqry += " and Account_Seg_Code1='" & strCustomerOpeningAccount & "'"
        End If
        strqry += " ) Final "
        gv1.CurrentRow.Cells(colACCode).Value = clsCommon.ShowSelectForm("TaxRateChangFND", strqry, "Account_Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), "Account_Code", isButtonClick)
        'gv1.CurrentRow.Cells(colACCode).Value = clsCommon.ShowSelectForm("TaxRateChangFND", qry, "Account_Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), "Account_Code", isButtonClick)
        gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value) + "'"))
        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), Nothing)
        Else
            gv1.CurrentRow.Cells(colGLType).Value = ""
        End If

        txtlocation.Enabled = False

        SetitemWiseTaxSetting(True, True)
    End Sub

    Private Sub OpenDeduction(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Ded_Grp_Code as [Deduction Group Code],TSPL_DEDUCTION_GROUP.Ded_Description as [Deduction Group Description] ,TSPL_DEDUCTION_MASTER.GL_Account_Code as [GL Account],TSPL_GL_ACCOUNTS.Description as [GL Account Desc],Security  from TSPL_DEDUCTION_MASTER  left outer join TSPL_DEDUCTION_GROUP on TSPL_DEDUCTION_GROUP.Ded_Code=TSPL_DEDUCTION_MASTER.Ded_Grp_Code  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code     "
        Dim whrcls As String
        If ChkSecurity.Checked Then
            whrcls = "  Security ='1' "
        Else
            whrcls = "  Security ='0' "
        End If
        'isDeductionFinder = True
        gv1.CurrentRow.Cells(colDeductionCode).Value = clsCommon.ShowSelectForm("dedFnd", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDeductionCode).Value), "Code", isButtonClick)
        isInsideLoadData = True
        gv1.CurrentRow.Cells(colDeductionDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_DEDUCTION_MASTER where Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colDeductionCode).Value) + "'"))
        gv1.CurrentRow.Cells(colACCode).Value = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_Account_Code  from TSPL_DEDUCTION_MASTER where Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colDeductionCode).Value) + "'")), txtlocation.Value, True, Nothing)
        gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value) + "'"))
        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), Nothing)
        Else
            gv1.CurrentRow.Cells(colGLType).Value = ""
        End If
        isInsideLoadData = False
        'txtlocation.Enabled = False
    End Sub

    Private Sub OpenInvoiceNo(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Vendor", Me.Text)
            gv1.CurrentRow.Cells(colDocNo).Value = ""
            Exit Sub
        End If
        Dim qry As String = ""
        Dim whrcls As String = ""
        If clsCommon.CompairString(DocTypeSaleInvoice, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Code , CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Date,TSPL_SALE_INVOICE_HEAD.Cust_Code as [Customer Code],TSPL_SALE_INVOICE_HEAD.Cust_Name as Customer"
            qry += " from TSPL_SALE_INVOICE_HEAD "
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_INVOICE_HEAD.Vehicle_Code"
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id"
            whrcls = "TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFiND", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeSalesReturn, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_SALE_RETURN_HEAD.Sale_Return_No as Code , CONVERT(varchar(10), TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as Date,TSPL_SALE_RETURN_HEAD.Cust_Code as [Customer Code],TSPL_SALE_RETURN_HEAD.Cust_Name as Customer,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_SALE_RETURN_HEAD "
            qry += " inner join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_SALE_RETURN_HEAD.Invoice_No"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_INVOICE_HEAD.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id"
            whrcls = " TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInceFinderSR", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeCSATransfer, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Code , CONVERT(varchar(10), TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as Date ,TSPL_CSA_TRANSFER_HEAD.Transport_Id " &
                  " from TSPL_CSA_TRANSFER_HEAD left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_CSA_TRANSFER_HEAD.Vehicle_Id " &
                  " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_CSA_TRANSFER_HEAD.Transport_Id "
            whrcls = " TSPL_CSA_TRANSFER_HEAD.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_CSA_TRANSFER_HEAD.DOC_CODE AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN ('" + txtDocNo.Value + "')) and TSPL_CSA_TRANSFER_HEAD.Status='1' "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderCSATransfer", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeTransfer, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_TRANSFER_ORDER_HEAD.Document_No as Code , CONVERT(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as DocDate ,TSPL_TRANSFER_ORDER_HEAD.Transport_Id  " &
                  " from TSPL_TRANSFER_ORDER_HEAD " &
                  " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code " &
                  " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id "
            whrcls = " TSPL_TRANSFER_ORDER_HEAD.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_TRANSFER_ORDER_HEAD.Document_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_TRANSFER_ORDER_HEAD.Status='1'  "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderLI", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeProductInvoice, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Code , CONVERT(varchar, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Date, " &
                " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as Customer " &
                " from TSPL_SD_SALE_INVOICE_HEAD " &
                " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " &
                " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code " &
                " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SD_SHIPMENT_HEAD.Transport_Id   " &
                " inner join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
            whrcls = " TSPL_SD_SHIPMENT_HEAD.Transport_Id='" + TxtVendorNo.Value + "' and not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SD_SALE_INVOICE_HEAD.Status='1' "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFiND", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
            'qry = "select TSPL_IssueReturn_HEAD.Doc_No as Code , CONVERT(varchar(10), TSPL_IssueReturn_HEAD.Doc_Date,103) as Date  "
            'qry += " from TSPL_IssueReturn_HEAD    "
            'whrcls = " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_IssueReturn_HEAD.Doc_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_IssueReturn_HEAD.Status='1' and TSPL_IssueReturn_HEAD.Doc_Type='Transfer'  "
            'gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderTR", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeAdjustment, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "  select TSPL_ADJUSTMENT_HEADER.Adjustment_No as Code , CONVERT(varchar(10), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Date,TSPL_ADJUSTMENT_HEADER.Customer_CODE as [Customer Code],TSPL_ADJUSTMENT_HEADER.Customer_NAME as Customer,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_ADJUSTMENT_HEADER"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_ADJUSTMENT_HEADER.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id "
            whrcls = " TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_HEADER.ItemType='E' "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderAd", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeSRN, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "select TSPL_SRN_HEAD.SRN_No as Code , CONVERT(varchar(10), TSPL_SRN_HEAD.SRN_Date,103) as Date,TSPL_SRN_HEAD.Vendor_Code as [Vendor Code],TSPL_SRN_HEAD.Vendor_Name as Vendor from TSPL_SRN_HEAD  "
            whrcls = "not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SRN_HEAD.SRN_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SRN_HEAD.Status=1   "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderSRN", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeMaterialSale, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            'qry = "select TSPL_SRN_HEAD.SRN_No as Code , CONVERT(varchar(10), TSPL_SRN_HEAD.SRN_Date,103) as Date,TSPL_SRN_HEAD.Vendor_Code as [Vendor Code],TSPL_SRN_HEAD.Vendor_Name as Vendor from TSPL_SRN_HEAD  "
            'whrcls = "not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SRN_HEAD.SRN_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SRN_HEAD.Status=1   "
            'gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderSRN", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        End If

    End Sub

    Private Sub OpenAdditionCharges(ByVal isButtonClick As Boolean)
        Try
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colAChgCode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colAChgCode).Value = obj.Code
                gv1.CurrentRow.Cells(colAChgName).Value = obj.desc

                If clsCommon.CompairString(obj.ServiceType, "Y") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colSACCode).Value = obj.SACCode
                    gv1.CurrentRow.Cells(colSACName).Value = obj.SAC_Description
                Else
                    gv1.CurrentRow.Cells(colSACCode).Value = ""
                    gv1.CurrentRow.Cells(colSACName).Value = ""
                End If

                If clsCommon.CompairString(cmbRefType.SelectedValue, "S") = CompairStringResult.Equal Then
                    obj.Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Account_Code, txtlocation.Value)
                End If
                gv1.CurrentRow.Cells(colACCode).Value = obj.Account_Code
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS WHere Account_Code='" + obj.Account_Code + "'"))
                If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
                    gv1.CurrentRow.Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), Nothing)
                Else
                    gv1.CurrentRow.Cells(colGLType).Value = ""
                End If
            Else
                gv1.CurrentRow.Cells(colAChgCode).Value = ""
                gv1.CurrentRow.Cells(colAChgName).Value = ""
                gv1.CurrentRow.Cells(colGLType).Value = ""
                gv1.CurrentRow.Cells(colSACCode).Value = ""
                gv1.CurrentRow.Cells(colSACName).Value = ""
            End If
        Catch ex As Exception
            gv1.CurrentRow.Cells(colAChgCode).Value = ""
            gv1.CurrentRow.Cells(colAChgName).Value = ""
            gv1.CurrentRow.Cells(colACCode).Value = ""
            gv1.CurrentRow.Cells(colACName).Value = ""
            gv1.CurrentRow.Cells(colGLType).Value = ""
            gv1.CurrentRow.Cells(colSACCode).Value = ""
            gv1.CurrentRow.Cells(colSACName).Value = ""
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub BlankTaxDetailsCurrentRow()
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Function GetCurrentRowTotalTaxAmt() As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
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

    Private Sub UpdateCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)



        Dim dblAmt As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colAmt).Value)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt
        Dim isUnClaimedTax As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(colIsUnclaimedTax).Value)
        'BlankTaxDetailsCurrentRowWihtRowNo(intRowNo)

        'Dim ii As String = "1"
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    ''Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(intRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        ''If IsTaxable Then
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(intRowNo, Strii, arrTaxableAuth)
                        ''End If
                        ''If IsExcisable Then
                        ''    dblBaseAmt = (dblAssessableAmt + dblOtherTaxAmt)
                        ''Else
                        dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                        ''End If
                    End If
                    If isUnClaimedTax Then
                        dblBaseAmt = 0
                    End If

                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If
                Else
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                End If
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(intRowNo)).Cells(colAmt).Value)
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmt).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If
            End If
        Next

        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmtWihtRowNo(intRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
        gv1.Rows(intRowNo).Cells(colDisAmt).Value = dblDisAmt
        gv1.Rows(intRowNo).Cells(colAmtAfterDis).Value = dblAmtAfterDis
        gv1.Rows(intRowNo).Cells(colTotTaxAmt).Value = dblTotTaxAmt
        gv1.Rows(intRowNo).Cells(colAmtAfterTax).Value = dblAmtAfterTax
    End Sub

    Private Function GetCurrentRowOtherTaxAmtWihtRowNo(ByVal intRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub BlankTaxDetailsCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Function GetCurrentRowSurTaxAmtWihtRowNo(ByVal intRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowTotalTaxAmtWihtRowNo(ByVal intRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
        Next
        Return dblTotTax
    End Function

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

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
        Dim dblLandedAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
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
                dblLandedAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colLandedAmt).Value)
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
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colAChgCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colAChgAmount).Value)
            End If
        Next

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        lblLandedAmt.Text = clsCommon.myFormat(dblLandedAmt)
        dblNetAmt = dblNetAmt + dblACAmount + dblLandedAmt
        Dim dclROAmt As Decimal = 0
        If SettingAutoRoundOffSeprateAccountOnVendorTransaction Then
            dclROAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero) - dblNetAmt
            dblNetAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero)
        End If
        lblRoundOff.Text = clsCommon.myFormat(dclROAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblTotRAmt1.Text = clsCommon.myFormat(dblNetAmt)
        txtAmtAfterCashDiscount.Text = clsCommon.myFormat(clsCommon.myCdbl(lblTotRAmt1.Text) - clsCommon.myCdbl(TxtCashDiscount.Text))
    End Sub

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
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        txtAdd_Doc_TYpe.Text = ""
        LblVCGL.Text = ""
        TxtVendorNo.Enabled = True
        TxtVendorNo.Value = ""
        txtlocation.Value = ""
        LblLocDesp.Text = ""
        txtlocation.Enabled = True
        BlankAllControls()
        LoadBlankGridGL()
        LoadBlankGridTax()
        LoadBlankGridAC()
        cboDocType.Enabled = True
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        btnViewTDSDetails.Enabled = False
        txtDate.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        chkRCM.Checked = False
        chkTDSProvision.Checked = False
        gvAC.Rows.AddNew()
        cmbRefType.SelectedIndex = 1
        cmbRefType.SelectedIndex = 0
        btnPrint.Enabled = True
        txtVendorInvoiceNo.Enabled = True
        txtVendorInvDatre.Enabled = True
        grpProvision.Enabled = False
        txtProvAmt.Text = ""
        arrProvDocNo = Nothing
        arrInvoiceNo = Nothing
        ' dtpFromProv.Value = clsCommon.GETSERVERDATE()
        'dtpToProv.Value = clsCommon.GETSERVERDATE()
        'chkProvision.Checked = False
        ChkSecurity.Checked = False
        pnlDeduction.Visible = False
        TxtHirerachy.MendatroryField = False
        TxtCostCentre.MendatroryField = False
        TxtHirerachy.Value = ""
        LblHirerachy.Text = ""
        TxtCostCentre.Value = ""
        LblCostCentre.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        chkProvision.Checked = False
        setProvisionVisibility()
        setDeductionVisibility()
        gv1.Rows.AddNew()
        chkSecTrans.Checked = False
        SetSeondaryTranporterPaymentCyle()
        txtToDateSecTrans.Value = clsCommon.GETSERVERDATE()
        txtFromDateSecTrans.Value = txtToDateSecTrans.Value.AddMonths(-1)
        isNewEntry = True

        'If UDLPurchaseOrderthroughAP = True Then
        '    lblCaption1.Visible = True
        '    txtBillNo.Visible = True
        '    lblcaption2.Visible = True
        '    dtBillDate.Visible = True
        '    dtBillDate.Value = clsCommon.GETSERVERDATE()
        'End If
        txtTotalAmt.Text = ""
        txtTotalAmt.Enabled = True
        FillVendorDetails()
        txtMCC.Text = ""
        lblMCC.Text = ""
        ''richa agarwal 19 June 2020
        If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
            cboDocType.SelectedValue = "D"
            chkDeduction.Enabled = False
            cboDocType.Enabled = False
            chkDeduction.Checked = True
        Else
            chkDeduction.Enabled = True
            cboDocType.Enabled = True
        End If
        txtBankCode.Text = ""
        TxtBankName.Text = ""
        TxtIFSCCode.Text = ""
        txtbranchname.Text = ""
        txtVendor_Bank_ACNo.Text = ""
        If EnableBankFromMaster = True Then
            grpVendorBankDetails.Visible = False
        Else
            grpVendorBankDetails.Visible = True
        End If
        txtSecurityAdjusment.arrValueMember = Nothing
        chkSecurityAdjusment.Checked = False
    End Sub

    Function AllowToSave() As Boolean

        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If clsCommon.myLen(txtTermCode.Value) > 0 Then
                txtTermCode_TxtChanged()
            End If
            btnSave.Focus()
            If clsCommon.CompairString(cmbRefType.SelectedValue, "WO") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtRefDocNo.Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") <> CompairStringResult.Equal Then
                    Dim WOBal As Decimal = clsVedorInvoiceHead.GetWorkOrderBalanceAmount(txtRefDocNo.Value, txtDocNo.Value, Nothing)
                    If WOBal < clsCommon.myCdbl(txtAmtAfterCashDiscount.Text) Then
                        Throw New Exception("Work Order Balance Amount : " & WOBal & " Invoice Document Total : " & clsCommon.myCdbl(txtAmtAfterCashDiscount.Text) & " ")
                    End If
                End If
            End If
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Posting_Date from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
                If clsCommon.myLen(chkpost) > 0 Then
                    clsCommon.MyMessageBoxShow("Transaction already posted", Me.Text)
                    Return False
                End If
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRowWihtRowNo(ii)
            Next

            UpdateAllTotals()

            Dim isTDSOverride As Boolean = False
            If objRemittance IsNot Nothing Then
                If objRemittance.IsTDSOverride Then
                    isTDSOverride = True
                End If
            End If
            If Not isTDSOverride AndAlso objRemittance IsNot Nothing Then
                SetVendorTDSDetails()
            End If
            If Not objRemittance Is Nothing Then
                UpdateTDSAmount()
            End If
            If objRemittance Is Nothing AndAlso objCommonVar.TDSValidationFrom IsNot Nothing Then
                If txtDate.Value >= objCommonVar.TDSValidationFrom Then
                    Dim AmountToCheckVendorOutstandingForTCSTax As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckVendorOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckVendorOutstandingForTCSTax, Nothing))
                    If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(TxtVendorNo.Value, txtDate.Value))
                        If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                            clsCommon.MyMessageBoxShow(Me, "Outstanding Amount for Vendor [" + TxtVendorNo.Value + "] Crossed TDS Limit.Please Apply TDS on Same.", Me.Text, MessageBoxButtons.OK)
                        End If
                    End If
                End If
            End If

            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please first select Location", Me.Text)
                txtlocation.Focus()
                Return False
            End If

            If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Vendor", Me.Text)
                TxtVendorNo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtACSet.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Vendor Account Set", Me.Text)
                txtACSet.Focus()
                Return False
            End If
            '================added by shivani tyagi
            If clsCommon.myCdbl(lblTotRAmt.Text) < clsCommon.myCdbl(TxtCashDiscount.Text) Then
                clsCommon.MyMessageBoxShow("Cash Discount should not be greater than Document Amount", Me.Text)
                TxtCashDiscount.Focus()
                Return False
            End If
            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Tax Group", Me.Text)
                txtTaxGroup.Focus()
                Return False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") <> CompairStringResult.Equal Then
                ''richa agarwal 
                Dim strcurrentfisyearstartdate As Date? = Nothing
                Dim strcurrentfisyearenddate As Date? = Nothing
                Dim strcurrentfisyear As String = String.Empty
                Dim strmonth As Integer = Convert.ToDateTime(txtDate.Value).Month()
                Dim stryear As Integer = Convert.ToDateTime(txtDate.Value).Year()
                If strmonth <= 3 Then
                    strcurrentfisyearstartdate = "01-Apr-" + clsCommon.myCstr(stryear - 1)
                    strcurrentfisyearenddate = "31-Mar-" + clsCommon.myCstr(stryear)
                    strcurrentfisyear = clsCommon.myCstr(stryear - 1) + "-" + clsCommon.myCstr(stryear)
                Else
                    strcurrentfisyearstartdate = "01-Apr-" + clsCommon.myCstr(stryear)
                    strcurrentfisyearenddate = "31-Mar-" + clsCommon.myCstr(stryear + 1)
                    strcurrentfisyear = clsCommon.myCstr(stryear) + "-" + clsCommon.myCstr(stryear + 1)
                End If
                ''------------------
                If clsCommon.myLen(txtVendorInvoiceNo.Text) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_No from TSPL_VENDOR_INVOICE_HEAD  where  addition_doc_type='" + clsCommon.myCstr(txtAdd_Doc_TYpe.Text) + "' and convert(date,Invoice_Entry_Date,103)>= convert(date,'" & strcurrentfisyearstartdate & "',103)  and convert(datetime,Invoice_Entry_Date,103)<= convert(datetime ,'" & strcurrentfisyearenddate & "',103)  and isnull(Vendor_Invoice_No,'') ='" & clsCommon.myCstr(txtVendorInvoiceNo.Text) & "' and Vendor_Code ='" & clsCommon.myCstr(TxtVendorNo.Value) & "' and Document_Type ='I' and Document_No<>'" + clsCommon.myCstr(txtDocNo.Value) + "' ")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        clsCommon.MyMessageBoxShow("Vendor Invoice No. " & txtVendorInvoiceNo.Text & " is already exist for Vendor " & TxtVendorNo.Value & " in current financial year " & strcurrentfisyear & ". ")
                        txtVendorInvoiceNo.Focus()
                        Return False
                    End If
                End If

            End If

            If clsCommon.GetDateWithStartTime(txtVendorInvDatre.Value) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
                clsCommon.MyMessageBoxShow("Vendor Invoice Date can't be Greate then Document Date", Me.Text)
                Return False
            End If
            If clsCommon.myLen(cmbRefType.SelectedValue) > 0 AndAlso clsCommon.myLen(txtRefDocNo.Value) <= 0 AndAlso cmbRefType.Text <> "Charges" Then

                clsCommon.MyMessageBoxShow("Please select Ref Document No", Me.Text)
                txtRefDocNo.Focus()
                Return False
            End If
            ''richa agarwal 21/12/2016
            If chkProRated.Checked = True Then
                If clsCommon.myLen(cmbRefType.SelectedValue) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please select Ref Doc Type", Me.Text)
                    cmbRefType.Focus()
                    Return False
                End If
                If clsCommon.myLen(cmbRefType.SelectedValue) > 0 AndAlso cmbRefType.Text <> "Bulk SRN" Then
                    clsCommon.MyMessageBoxShow("Please select Ref Doc Type as Bulk SRN", Me.Text)
                    cmbRefType.Focus()
                    Return False
                End If
            End If

            Dim l As Integer
            Dim k As Integer




            For k = 0 To gv1.Rows.Count - 1
                ''
                If chkSecTrans.Checked Then
                    If clsCommon.myLen(gv1.Rows(k).Cells(colDeductionCode).Value) > 0 Then
                        Dim arrSTDed As List(Of clsAPSecondaryTranporterDeductionDetail) = TryCast(gv1.Rows(k).Cells(colDeductionCode).Tag, List(Of clsAPSecondaryTranporterDeductionDetail))
                        If arrSTDed Is Nothing OrElse arrSTDed.Count <= 0 Then
                            Throw New Exception("Please set the secondary tranporter dispatch charges at row no " + clsCommon.myCstr(k))
                        End If
                        Dim dblDedAmt As Double = 0
                        For Each objSTDD As clsAPSecondaryTranporterDeductionDetail In arrSTDed
                            dblDedAmt += objSTDD.Amount
                        Next
                        If dblDedAmt <> clsCommon.myCdbl(gv1.Rows(k).Cells(colAmt).Value) Then
                            Throw New Exception("Amount " + clsCommon.myCstr(gv1.Rows(k).Cells(colAmt).Value) + " and  secondary tranporter dispatch deduction amount " + clsCommon.myCstr(dblDedAmt) + ".It Should match at row no " + clsCommon.myCstr(k))
                        End If
                    End If
                End If


                If clsCommon.myLen(gv1.Rows(k).Cells(colDocType).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(k).Cells(colDocNo).Value) > 0 Then
                    Dim DocType As String = clsCommon.myCstr(gv1.Rows(k).Cells(colDocType).Value)
                    Dim DocNo As String = clsCommon.myCstr(gv1.Rows(k).Cells(colDocNo).Value)
                    If chkDeduction.Checked AndAlso clsCommon.myLen(gv1.Rows(k).Cells(colACCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(k).Cells(colDeductionCode).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(" Please Deduction at Row no  " & clsCommon.myCstr(k + 1))
                        Return False
                    End If
                    For l = k + 1 To gv1.Rows.Count - 1
                        Dim NextDocType As String = clsCommon.myCstr(gv1.Rows(l).Cells(colDocType).Value)
                        Dim NextDocNo As String = clsCommon.myCstr(gv1.Rows(l).Cells(colDocNo).Value)
                        If DocType = NextDocType AndAlso DocNo = NextDocNo Then
                            clsCommon.MyMessageBoxShow(" DocType : " + DocType + "  and  DocNo : " + DocNo + " Should not be same in next row ")
                            Return False
                        Else
                            Continue For
                        End If
                    Next

                End If


                If SettingCostCenter AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(k).Cells(colACCode).Value)) > 0 Then
                    Dim grouptype As String = ""
                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.Rows(k).Cells(colACCode).Value), Nothing)
                    If Not clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(k).Cells(colHierarchyCode).Value)) <= 0 Then
                            clsCommon.MyMessageBoxShow(" Please select Hierarchy ", Me.Text)
                            Return False
                        End If
                        If SettingCostCenterlevel Then
                            Dim lvl As Integer = clsCommon.myCdbl(gv1.Rows(k).Cells(colHierarchyLevelNumber).Value)
                            If lvl > 0 Then
                                gv1.Rows(k).Cells(colCostCenterCode).Value = clsCommon.myCstr(gv1.Rows(k).Cells("colHierarchyLevel" + clsCommon.myCstr(lvl)).Value)
                                For pp As Integer = lvl To 1 Step -1
                                    If clsCommon.myLen(gv1.Rows(k).Cells("colHierarchyLevel" + clsCommon.myCstr(pp)).Value) <= 0 Then
                                        clsCommon.MyMessageBoxShow("Please select " & gv1.Columns("colHierarchyLevel" + clsCommon.myCstr(pp)).HeaderText)
                                        Return False
                                    End If
                                Next
                            End If
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(k).Cells(colCostCenterCode).Value)) <= 0 Then
                            clsCommon.MyMessageBoxShow(" Please select Cost Center ", Me.Text)
                            Return False
                        End If
                    End If
                End If
            Next

            For ii As Integer = 0 To gvAC.Rows.Count - 1
                If clsCommon.myLen(gvAC.Rows(ii).Cells(colAChgCode).Value) > 0 Then
                    For jj As Integer = 0 To gvAC.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colAChgCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colAChgCode).Value)) = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colAChgCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        End If
                    Next
                End If
            Next
            Dim isFirstTime As Boolean = True
            Dim strFirstLocSeg As String = ""
            Dim arrAccountCode As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strACode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colACCode).Value)
                If clsCommon.myLen(strACode) > 0 Then
                    If Not arrAccountCode.Contains(strACode) Then
                        arrAccountCode.Add(strACode)
                    End If
                    If (isFirstTime) Then
                        strFirstLocSeg = strACode.Substring(strACode.Length - 3, 3)
                        isFirstTime = False
                    End If
                    Dim strCurrLocSeg As String = strACode.Substring(strACode.Length - 3, 3)
                    If Not clsCommon.CompairString(strCurrLocSeg, strFirstLocSeg) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Location segment should be same for all the GL Accounts", Me.Text)
                        Return False
                    End If
                End If
            Next
            If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "All AP Invoice") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "I") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Can't Create AP Invoice against AP Invoice", Me.Text)
                    Return False
                End If
                Dim qry As String = "select Vendor_Code from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtRefDocNo.Value + "' "
                Dim strRefVendorCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                If Not clsCommon.CompairString(strRefVendorCode, TxtVendorNo.Value) = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Reference Document's Vendor:" + strRefVendorCode + " and Document Vendor" + TxtVendorNo.Value, Me.Text)
                    Return False
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "BS") = CompairStringResult.Equal Then
                Dim qry As String = "select TSPL_Bulk_MILK_SRN.SRN_NO as Code,TSPL_Bulk_MILK_SRN.SRN_Date as Date,TSPL_Bulk_MILK_SRN.Vendor_Code as VendorCode ,TSPL_Bulk_MILK_SRN.Loc_Code as LocationCode " &
                " from TSPL_Bulk_MILK_SRN  where TSPL_Bulk_MILK_SRN.isPosted = 1 and TSPL_Bulk_MILK_SRN.SRN_NO='" + txtRefDocNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If Not clsCommon.CompairString(TxtVendorNo.Value, clsCommon.myCstr(dt.Rows(0)("VendorCode"))) = CompairStringResult.Equal Then
                        TxtVendorNo.Focus()
                        Throw New Exception("Vendor Should be " + clsCommon.myCstr(dt.Rows(0)("Vendor")))
                    End If
                    If Not clsCommon.CompairString(txtlocation.Value, clsCommon.myCstr(dt.Rows(0)("LocationCode"))) = CompairStringResult.Equal Then
                        txtlocation.Focus()
                        Throw New Exception("Location Should be " + clsCommon.myCstr(dt.Rows(0)("LocationCode")))
                    End If
                Else
                    txtRefDocNo.Focus()
                    Throw New Exception("Bulk SRN No is not correct.")
                End If
                If Not (clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal) Then
                    cboDocType.Focus()
                    Throw New Exception("If you select Ref Doc Type to Bulk SRN then Invoice type should be Debit note or Credit Note")
                End If
            End If
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                If chkProvision.Checked AndAlso (clsCommon.myCdbl(txtProvAmt.Text) <= 0 OrElse arrProvDocNo Is Nothing) Then
                    clsCommon.MyMessageBoxShow("Please select atleast one provision to Knock Off", Me.Text)
                    Return False
                End If
            End If
            If CheckDocAmountInAPInvoiceEntry = True Then
                Dim DocAmt As Double = clsCommon.myCdbl(lblTotRAmt1.Text)
                Dim TotalAmt As Double = clsCommon.myCdbl(txtTotalAmt.Text)
                If (DocAmt = TotalAmt) Then
                Else
                    clsCommon.MyMessageBoxShow("Document Amount is not equal to Manual Amount", Me.Text)
                    txtTotalAmt.Focus()
                    Return False
                End If
            End If

            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus = True Then
                If (clsCommon.CompairString(cboDocType.SelectedValue, "C") = CompairStringResult.Equal) Then
                    Dim strcreditNoteSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowCreditNoteWithoutReferenceonAP, clsFixedParameterCode.AllowCreditNoteWithoutReferenceonAP, Nothing))
                    If clsCommon.CompairString(strcreditNoteSetting, "1") = CompairStringResult.Equal Then
                        Dim strtaxgroup As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select distinct Tax_Group_Code from TSPL_TAX_GROUP_MASTER  where TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 and Tax_Group_Type='P' and TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(txtTaxGroup.Value) & "'"))
                        If clsCommon.myLen(strtaxgroup) <= 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") <> CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(" Please select Ref DocType as AP Invoice  ", Me.Text)
                                Return False
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(txtRefDocNo.Value)) <= 0 Then
                                clsCommon.MyMessageBoxShow(" Please select Invoice No ", Me.Text)
                                Return False
                            End If
                        End If
                    Else
                        ''richa 05 feb,2019  TEC/05/02/19-000411 not check for opening in case of Credit or debit note 
                        'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
                        Dim JEWithOPening As Boolean = False
                        If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                            Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                            If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                                JEWithOPening = True
                            End If
                        End If
                        If JEWithOPening = False Then
                            If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") <> CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(" Please select Ref DocType as AP Invoice  ", Me.Text)
                                Return False
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(txtRefDocNo.Value)) <= 0 Then
                                clsCommon.MyMessageBoxShow(" Please select Invoice No ", Me.Text)
                                Return False
                            End If
                        End If

                    End If
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal) Then
            Else
                If clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Enter Vendor Invoice No", Me.Text)
                    txtVendorInvoiceNo.Focus()
                    Return False
                End If
            End If

            ''RICHA AGARWAL KDI/30/07/18-000413 30 July,2018
            If arrAccountCode IsNot Nothing AndAlso arrAccountCode.Count > 0 Then
                ''richa UDL/25/02/19-000273 remove ChkSecurity.Checked = False from below conditions
                ' If Not chkProvision.Checked AndAlso (ChkSecurity.Checked = False Or chkDeduction.Checked = False) AndAlso clsCommon.GetDateWithStartTime(txtDate.Value) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                If Not chkProvision.Checked AndAlso (chkDeduction.Checked = False) AndAlso clsCommon.GetDateWithStartTime(txtDate.Value) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                    'richa 17 SEp,2019 TEC/03/07/19-000927
                    Dim qry As String = "select Account_Code from TSPL_GL_ACCOUNTS where Account_Code in (" + clsCommon.GetMulcallString(arrAccountCode) + ") and ControlAccount<>'N' AND TSPL_GL_ACCOUNTS.Account_Code NOT IN  (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1)"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Can not select control Account -" + clsCommon.myCstr(dt.Rows(0)("Account_Code")))
                    End If
                End If
            End If

            Dim IsVendorRegisted As Double = clsDBFuncationality.getSingleValue("select 1 from TSPL_VENDOR_MASTER where Vendor_Code='" + TxtVendorNo.Value + "' and GSTRegistered=0 ")
            Dim IsTaxExempted As Double = clsDBFuncationality.getSingleValue("select Is_Tax_Exempted from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + txtTaxGroup.Value + "'")


            If IsVendorRegisted = 1 AndAlso IsTaxExempted = 0 Then
                If clsCommon.MyMessageBoxShow("Apply RCM", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    chkRCM.Checked = True
                Else
                    chkRCM.Checked = False
                End If

            End If
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Provisional from TSPL_VENDOR_MASTER where Vendor_Code='" + TxtVendorNo.Value + "'")) = 1 Then
                If clsCommon.MyMessageBoxShow("Do You Want to Apply TDS Provision", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    chkTDSProvision.Checked = True
                Else
                    chkTDSProvision.Checked = False
                End If

            End If

            If chkSecurityAdjusment.Checked Then
                If txtSecurityAdjusment.arrValueMember Is Nothing OrElse txtSecurityAdjusment.arrValueMember.Count <= 0 Then
                    txtSecurityAdjusment.Focus()
                    Throw New Exception("Plesae select secuirty docuemnts to adjust")
                End If
            End If
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.AllowToSave()
            End If

            UcAttachment1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If AllowToSave() Then
        SaveData(False)
        'End If

    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
                Dim obj As New clsVedorInvoiceHead()
                obj.Invoice_Type = "AP"
                obj.Document_No = txtDocNo.Value
                obj.Invoice_Entry_Date = txtDate.Value
                obj.Vendor_Code = TxtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                obj.Vendor_Invoice_Date = txtVendorInvDatre.Value
                obj.Security = IIf(ChkSecurity.Checked = True, 1, 0)
                obj.Account_Set = txtACSet.Value
                obj.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.PO_Number = txtPONo.Text
                obj.Employee_Type = clsCommon.myCstr(ddlEmployeeType.SelectedValue)
                '---------added by  usha
                obj.loc_code = txtlocation.Value
                '---------end
                obj.MCC_Code = txtMCC.Text
                obj.MCC_Name = lblMCC.Text

                obj.Vendor_Bank_Code = txtBankCode.Text
                obj.Vendor_Bank_Name = TxtBankName.Text
                obj.Branch_IFSC_Code = TxtIFSCCode.Text
                obj.Branch_Name = txtbranchname.Text
                obj.Vendor_Bank_ACNo = txtVendor_Bank_ACNo.Text
                ''added by priti
                obj.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
                obj.RefDocNo = txtRefDocNo.Value
                Dim GstStatus As Boolean = clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                If GstStatus Then
                    obj.GSTRegistered = IIf(clsVendorMaster.IsGSTRegisteredVendor(obj.Vendor_Code, Nothing), 1, 0)
                Else
                    obj.GSTRegistered = 1
                End If
                ''-- added by parteek 17/08/2017
                If clsCommon.myCBool(chkITCEligible.Checked) = True Then
                    obj.ITC_Elibible = IIf(chkITCEligible.Checked, 1, 0)
                    If clsCommon.myCdbl(CboxITCType.SelectedIndex) = 0 Then
                        obj.ITC_Type = 1
                    Else
                        obj.ITC_Type = 0
                    End If

                    obj.ITC_Type_Category = CboxITCCateogory.SelectedValue
                End If
                ''----End

                '' priti ends here
                obj.Order_No = txtOrderNo.Text
                obj.Total_Tax = clsCommon.myCdbl(lblTaxAmt.Text)

                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.PROJECT_ID = fndProject.Value

                obj.is_For_Provision = IIf(chkProvision.Checked, 1, 0)
                obj.isDeduction = IIf(chkDeduction.Checked, 1, 0)

                obj.is_Secondary_Transporter_Deduction = chkSecTrans.Checked
                obj.TapalNo = clsCommon.myCstr(txtTapalNo.Text)
                If txtDataAndTimeSelection.Checked Then
                    obj.DateAndTime = txtDataAndTimeSelection.Value
                End If
                obj.RCM = chkRCM.Checked
                obj.TDS_Provision = chkTDSProvision.Checked
                '' Anubhooti 06-Apr-2015 (Added two columns)
                obj.Hirerachy_Level_Code = clsCommon.myCstr(TxtHirerachy.Value)
                obj.Cost_Centre_Fin_Level_Code = clsCommon.myCstr(TxtCostCentre.Value)
                If obj.is_For_Provision = 1 Then
                    obj.Prov_From_Date = clsCommon.GetPrintDate(dtpFromProv.Value, "dd/MMM/yyyy")
                    obj.Prov_To_Date = clsCommon.GetPrintDate(dtpToProv.Value, "dd/MMM/yyyy")
                    obj.Prov_Amt = clsCommon.myCdbl(txtProvAmt.Text)
                    obj.arrProvDocNo = arrProvDocNo
                    obj.arrInvoiceNo = arrInvoiceNo
                    If clsCommon.myLen(FreightProvisionAccount) > 0 Then
                        obj.isFreightProvisionAccount = True
                    Else
                        obj.isFreightProvisionAccount = False
                    End If
                End If
                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)

                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.Tax1_BAmount = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)

                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.Tax2_BAmount = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)

                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.Tax3_BAmount = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)

                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.Tax4_BAmount = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)

                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.Tax5_BAmount = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)

                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.Tax6_BAmount = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)

                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.Tax7_BAmount = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)

                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.Tax8_BAmount = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)

                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.Tax9_BAmount = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)

                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.Tax10_BAmount = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If

                obj.Terms_Code = txtTermCode.Value
                obj.Terms_Description = lblTermName.Text
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amount = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.RoundOffAmount = clsCommon.myCdbl(lblRoundOff.Text)
                obj.Document_Total = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Balance_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                '=================added by shivani
                obj.Cash_Discount_Amt = clsCommon.myCdbl(TxtCashDiscount.Text)
                obj.Amt_After_Cash_Discount = clsCommon.myCdbl(lblTotRAmt.Text) - clsCommon.myCdbl(TxtCashDiscount.Text)
                '==============================
                obj.Total_Landed_Amt = clsCommon.myCdbl(lblLandedAmt.Text)
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                    obj.ISProcurementDeduction = 1
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Advance_Against_Imprest ,Advance_Against_Travelling,Employee_Salary  from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + txtACSet.Value + "'")

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(ddlEmployeeType.SelectedValue), "TD") = CompairStringResult.Equal Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlEmployeeType.SelectedValue), "T") = CompairStringResult.Equal Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlEmployeeType.SelectedValue), "S") = CompairStringResult.Equal Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlEmployeeType.SelectedValue), "I") = CompairStringResult.Equal Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                    Else
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    End If


                    If clsCommon.myCdbl(lblDiscountAmt.Text) > 0 Then
                        obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    End If
                End If

                If (gvAC.Rows.Count > 0) Then
                    If clsCommon.myLen(gvAC.Rows(0).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 1) Then
                    If clsCommon.myLen(gvAC.Rows(1).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 2) Then
                    If clsCommon.myLen(gvAC.Rows(2).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 3) Then
                    If clsCommon.myLen(gvAC.Rows(3).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 4) Then
                    If clsCommon.myLen(gvAC.Rows(4).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 5) Then
                    If clsCommon.myLen(gvAC.Rows(5).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 6) Then
                    If clsCommon.myLen(gvAC.Rows(6).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 7) Then
                    If clsCommon.myLen(gvAC.Rows(7).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 8) Then
                    If clsCommon.myLen(gvAC.Rows(8).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 9) Then
                    If clsCommon.myLen(gvAC.Rows(9).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colAChgAmount).Value)
                    End If
                End If
                obj.Total_Add_Charge = clsCommon.myCdbl(lblAddCharges.Text)
                obj.Empty_Amount = clsCommon.myCdbl(lblTotEmptyAmt.Text)
                obj.Is_ProRated = IIf(chkProRated.Checked, "Y", "N")

                ''========================03/10/2016==========
                obj.Addition_Doc_Type = clsCommon.myCstr(txtAdd_Doc_TYpe.Text)
                ''=========================================================

                obj.Arr = New List(Of clsVedorInvoiceDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsVedorInvoiceDetail()

                    objTr.chrgcatcode = clsCommon.myCstr(grow.Cells(colchrcode).Value)
                    objTr.chrgcatdesc = clsCommon.myCstr(grow.Cells(colchrName).Value)
                    objTr.chrgcatvalue = clsCommon.myCstr(grow.Cells(colchrValue).Value)
                    objTr.chritemcode = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                    objTr.chritemdesc = clsCommon.myCstr(grow.Cells(colitemname).Value)
                    objTr.DeductionCode = clsCommon.myCstr(grow.Cells(colDeductionCode).Value)
                    objTr.DeductionDesc = clsCommon.myCstr(grow.Cells(colDeductionDesc).Value)
                    If chkSecTrans.Checked Then
                        objTr.arrSTDed = TryCast(grow.Cells(colDeductionCode).Tag, List(Of clsAPSecondaryTranporterDeductionDetail))
                    End If
                    objTr.Detail_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.GL_Account_Code = clsCommon.myCstr(grow.Cells(colACCode).Value)
                    objTr.GL_Account_Desc = clsCommon.myCstr(grow.Cells(colACName).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Landed_Amount = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    objTr.Discount = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amount_less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                    objTr.Total_Tax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Total_Amount = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.AddChargeCode = clsCommon.myCstr(grow.Cells(colAChgCode).Value)
                    objTr.AddChargeDesc = clsCommon.myCstr(grow.Cells(colAChgName).Value)
                    objTr.is_Unclaimed_Tax = clsCommon.myCBool(grow.Cells(colIsUnclaimedTax).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Invoice_Type = clsCommon.myCstr(grow.Cells(colDocType).Value)
                    objTr.Invoice_No = clsCommon.myCstr(grow.Cells(colDocNo).Value)

                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                    ''shivani 

                    objTr.SAC_Code = clsCommon.myCstr(grow.Cells(colSACCode).Value)



                    objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                    objTr.Cost_Centre_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                    ''richa 22 dec,2017

                    objTr.Hirerachy_Code1 = clsCommon.myCstr(grow.Cells(colHierarchyLevel1).Value)
                    objTr.Hirerachy_Code2 = clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value)
                    objTr.Hirerachy_Code3 = clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)
                    objTr.Hirerachy_Code4 = clsCommon.myCstr(grow.Cells(colHierarchyLevel4).Value)



                    If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                If objRemittance IsNot Nothing Then
                    obj.RemittanceObject = New clsRemittance()
                    obj.RemittanceObject = objRemittance
                    obj.RemittanceObject.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                    obj.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                    obj.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                    obj.TDS_Percentage = objRemittance.TDS_Per
                    obj.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                    obj.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                    obj.Nature_of_deduction = objRemittance.Deduction_Code
                    obj.Branch_Code = objRemittance.Branch_Code
                    obj.Balance_Amt = clsCommon.myCdbl(lblTotRAmt.Text) - objRemittance.Actual_Total_TDS
                    obj.RemittanceObject.Previous_TDS_Amt = dblPreviousTDSAmt
                    Dim SectionCode As String = clsDBFuncationality.getSingleValue("select TDS_Section  from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" + objRemittance.Deduction_Code + "'")
                    '===============update by preeti gupta against ticket no [BM00000008664]
                    If clsCommon.myLen(SectionCode) > 0 Then
                        obj.Section_Code = SectionCode
                    Else
                        obj.Section_Code = objRemittance.Section_Code
                    End If

                End If
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    clsCommon.MyMessageBoxShow("Please Fill at list one GL Acount")
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

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colACCode)
                End If
                ''End of For Custom Fields
                If chkSecurityAdjusment.Checked Then
                    obj.arrSecurityAdjustment = txtSecurityAdjusment.arrValueMember
                End If

                If (obj.SaveData(obj, isNewEntry, True)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    txtDocNo.Value = obj.Document_No
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    End If
                    ''approval work 11/02/2020
                    Dim xNewDesc As String = ""
                    xNewDesc = "Party Name : " + obj.Vendor_Name
                    ''=====================capex cond==============

                    xNewDesc = xNewDesc + Environment.NewLine + "Description : " + obj.Description
                    clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, obj.Document_No, txtDate.Value, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(txtDesc.Text), clsCommon.myCdbl(lblTotRAmt1.Text), 0, "")
                    '================================================================

                    LoadData(obj.Document_No)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String)
        Try
            isNewEntry = False
            txtlocation.Enabled = False
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            txtTotalAmt.Enabled = True
            btnViewTDSDetails.Enabled = False
            objRemittance = Nothing
            cboDocType.Enabled = False
            isInsideLoadData = True
            btnSave.Text = "Update"
            txtDocNo.MyReadOnly = True
            BlankAllControls()
            LoadBlankGridGL()
            LoadBlankGridTax()
            LoadBlankGridAC()
            chkProvision.Checked = False
            txtProvAmt.Text = ""

            Dim obj As New clsVedorInvoiceHead()
            obj = clsVedorInvoiceHead.GetData(strDocumentNo, "AP','VC")
            'btnPrint.Visible = True

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If (obj.RemittanceObject IsNot Nothing) Then
                    objRemittance = New clsRemittance()
                    objRemittance = obj.RemittanceObject
                    btnViewTDSDetails.Enabled = True
                    dblPreviousTDSAmt = obj.RemittanceObject.Previous_TDS_Amt
                End If

                If clsCommon.myLen(obj.Posting_Date) > 0 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    txtTotalAmt.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    If CostCenterAndHirerachyCodeUpdateAfterPost = True Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Invoice_Entry_Date
                TxtVendorNo.Value = obj.Vendor_Code

                If clsCommon.myCBool(obj.ITC_Elibible) = True Then
                    chkITCEligible.Checked = IIf(obj.ITC_Elibible = 1, True, False)
                    If clsCommon.myCdbl(obj.ITC_Type) = 1 Then
                        CboxITCType.SelectedIndex = 0
                    Else
                        CboxITCType.SelectedIndex = 1
                    End If

                    CboxITCCateogory.SelectedValue = obj.ITC_Type_Category
                    RadGroupBox4.Enabled = True
                End If
                ddlEmployeeType.SelectedValue = obj.Employee_Type
                '--------------Enables/Disables--TDS button------------------------
                Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(obj.Vendor_Code)
                If objVendor IsNot Nothing Then
                    btnViewTDSDetails.Enabled = True
                Else
                    btnViewTDSDetails.Enabled = False
                End If
                '------------------------------------------------------------------
                chkRCM.Checked = obj.RCM
                chkTDSProvision.Checked = obj.TDS_Provision

                chkProRated.Checked = IIf(clsCommon.CompairString(obj.Is_ProRated, "Y") = CompairStringResult.Equal, True, False)
                txtlocation.Value = obj.loc_code
                '' Anubhooti 17-Nov-2014 BM00000004657
                If clsCommon.myLen(clsCommon.myCstr(obj.loc_code)) > 0 Then
                    LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(obj.loc_code) & "'"))
                Else
                    LblLocDesp.Text = ""
                End If
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    txtDataAndTimeSelection.Value = obj.DateAndTime
                    txtDataAndTimeSelection.Checked = True
                End If
                txtTapalNo.Text = clsCommon.myCstr(obj.TapalNo)

                txtMCC.Text = obj.MCC_Code
                lblMCC.Text = obj.MCC_Name
                'txtVendorNo.SelectedValue = obj.Vendor_Code
                '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
                lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
                txtVendorInvDatre.Value = obj.Vendor_Invoice_Date
                txtACSet.Value = obj.Account_Set
                'txtACSet.SelectedValue = obj.Account_Set
                '' Anubhooti 12-Nov-2014 BM00000004627
                If clsCommon.CompairString(clsCommon.myCstr(obj.Document_Type), "D") = CompairStringResult.Equal Then
                    LoadRefDocTypeForDC()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Document_Type), "C") = CompairStringResult.Equal Then
                    LoadRefDocTypeForDC()
                Else
                    LoadRefDocumenType()
                End If
                txtBankCode.Text = clsCommon.myCstr(obj.Vendor_Bank_Code)
                TxtBankName.Text = clsCommon.myCstr(obj.Vendor_Bank_Name)
                TxtIFSCCode.Text = clsCommon.myCstr(obj.Branch_IFSC_Code)
                txtbranchname.Text = clsCommon.myCstr(obj.Branch_Name)
                txtVendor_Bank_ACNo.Text = clsCommon.myCstr(obj.Vendor_Bank_ACNo)
                LblVCGL.Text = clsCommon.myCstr(obj.Against_VCGL)
                TxtHirerachy.Value = clsCommon.myCstr(obj.Hirerachy_Level_Code)
                If clsCommon.myLen(clsCommon.myCstr(obj.Hirerachy_Level_Code)) > 0 Then
                    LblHirerachy.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_HIRERACHY_LEVEL_MASTER WHERE Hirerachy_Code ='" & clsCommon.myCstr(obj.Hirerachy_Level_Code) & "'"))
                Else
                    LblHirerachy.Text = ""
                End If
                TxtCostCentre.Value = clsCommon.myCstr(obj.Cost_Centre_Fin_Level_Code)
                If clsCommon.myLen(clsCommon.myCstr(obj.Cost_Centre_Fin_Level_Code)) > 0 Then
                    LblCostCentre.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Cost_Center_Fin_Name,'') As Cost_Center_Fin_Name FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code ='" & clsCommon.myCstr(obj.Cost_Centre_Fin_Level_Code) & "'"))
                Else
                    LblCostCentre.Text = ""
                End If
                ''
                arrProvDocNo = obj.arrProvDocNo
                arrInvoiceNo = obj.arrInvoiceNo
                cboDocType.SelectedValue = obj.Document_Type
                txtPONo.Text = obj.PO_Number
                Milk_PI_No = obj.Against_MillkPurchaseInvoice_No
                '' priti starts here
                cmbRefType.SelectedValue = obj.RefDocType
                txtRefDocNo.Value = obj.RefDocNo
                '' priti ends here
                txtOrderNo.Text = obj.Order_No

                ChkSecurity.Checked = IIf(obj.Security = 1, True, False)
                chkOnHold.Checked = obj.On_Hold

                ''=============03/10/2016----------------------
                txtAdd_Doc_TYpe.Text = obj.Addition_Doc_Type
                ''=======================================================

                '======================shivani Tyagi========================================
                Dim t1 As New clsVCGLHead()
                t1 = clsVCGLHead.GetData(obj.Against_VCGL)
                If obj.Against_VCGL = "" Then
                    If clsCommon.myCstr(clsCommon.myLen(obj.Description)) > 0 Then
                        txtDesc.Text = obj.Description
                    End If
                Else

                    If t1.Document_Type = "V" And obj.Vendor_Code = t1.VC_Code Then
                        txtDesc.Text = t1.Remarks

                    End If
                    For Each Tr1 As clsVCGLDetail In t1.Arr
                        If Tr1.Row_Type = "Vendor" And obj.Vendor_Code = Tr1.VCGL_Code Then
                            txtDesc.Text = Tr1.Remarks
                        End If
                    Next
                End If
                '======================================================================
                txtTaxGroup.Value = obj.Tax_Group
                fndProject.Value = obj.PROJECT_ID
                chkProvision.Checked = IIf(obj.is_For_Provision = 1, True, False)
                chkDeduction.Checked = IIf(obj.isDeduction = 1, True, False)
                If obj.is_For_Provision = 1 Then
                    grpProvision.Visible = True
                    dtpFromProv.Value = obj.Prov_From_Date
                    dtpToProv.Value = obj.Prov_To_Date
                    txtProvAmt.Text = obj.Prov_Amt
                    dtpFromProv.Enabled = True
                    dtpToProv.Enabled = True
                    txtProvAmt.Enabled = True
                    btnProvSelect.Enabled = True
                    btlShowProvision.Enabled = True
                Else
                    grpProvision.Visible = False
                    txtProvAmt.Text = ""
                    dtpFromProv.Enabled = False
                    dtpToProv.Enabled = False
                    txtProvAmt.Enabled = False
                    btnProvSelect.Enabled = False
                    btlShowProvision.Enabled = False
                End If
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                '' Anubhooti 12-Nov-2014
                If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
                    txtVendorInvoiceNo.Enabled = False
                    txtVendorInvDatre.Enabled = False
                    pnlDeduction.Visible = False
                Else
                    txtVendorInvoiceNo.Enabled = True
                    txtVendorInvDatre.Enabled = True
                    pnlDeduction.Visible = True
                End If

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                txtTaxGroup.Value = obj.Tax_Group
                txtTermCode.Value = obj.Terms_Code
                'txtTermCode.SelectedValue = obj.Terms_Code
                lblTermName.Text = obj.Terms_Description

                If clsCommon.myLen(obj.Due_Date) > 0 Then
                    txtDueDate.Value = obj.Due_Date
                End If
                chkSecTrans.Checked = obj.is_Secondary_Transporter_Deduction
                If chkSecTrans.Checked Then
                    Dim qry As String = "select  min(TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date) as MinDate,max(TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date) as MaxDate" + Environment.NewLine +
                    " from TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL " + Environment.NewLine +
                    " left outer join TSPL_MCC_DISPATCH_CHALLAN on TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO=TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.DC_Challan_No " + Environment.NewLine +
                    " where AP_Invoice_No='" + obj.Document_No + "'"
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        txtFromDateSecTrans.Value = clsCommon.myCDate(dtTemp.Rows(0)("MinDate"))
                        txtToDateSecTrans.Value = clsCommon.myCDate(dtTemp.Rows(0)("MaxDate"))
                    End If

                End If

                fndProject.Value = obj.PROJECT_ID
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amount)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax)
                lblRoundOff.Text = clsCommon.myFormat(obj.RoundOffAmount)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Document_Total)
                lblTotRAmt1.Text = clsCommon.myFormat(obj.Document_Total)
                lblTotEmptyAmt.Text = clsCommon.myFormat(obj.Empty_Amount)
                '=================added by shivani
                TxtCashDiscount.Text = obj.Cash_Discount_Amt
                txtAmtAfterCashDiscount.Text = clsCommon.myFormat(obj.Document_Total) - clsCommon.myCdbl(obj.Cash_Discount_Amt)
                '==============================
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                lblLandedAmt.Text = clsCommon.myFormat(obj.Total_Landed_Amt)
                If obj.arrSecurityAdjustment IsNot Nothing AndAlso obj.arrSecurityAdjustment.Count > 0 Then
                    chkSecurityAdjusment.Checked = True
                    txtSecurityAdjusment.arrValueMember = obj.arrSecurityAdjustment
                End If

                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax1_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax2_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax3_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax4_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax5_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax6_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax7_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax8_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax9_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax10_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If


                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt1
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt10
                End If

                lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                lblAddCharges1.Text = clsCommon.myFormat(obj.Total_Add_Charge)

                If clsCommon.myLen(obj.Against_POInvoice_No) > 0 OrElse clsCommon.myLen(obj.Against_PurchaseReturn_No) > 0 Then
                    btnPrint.Visible = False
                End If
                Dim TotalAmt As Double = 0
                gv1.Rows.Clear()
                For Each objTr As clsVedorInvoiceDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Detail_Line_No

                    '************************02/04/2014**********************************************
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colchrcode).Value = objTr.chrgcatcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colchrName).Value = objTr.chrgcatdesc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = objTr.chritemcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = objTr.chritemdesc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colchrValue).Value = objTr.chrgcatvalue
                    ''shivani



                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.Cost_Centre_Code
                    If clsCommon.myLen(objTr.Hirerachy_Code) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevelNumber).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Level from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'"))
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel1).Value = objTr.Hirerachy_Code1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel2).Value = objTr.Hirerachy_Code2
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel3).Value = objTr.Hirerachy_Code3
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel4).Value = objTr.Hirerachy_Code4


                    '***************************************************************************************

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionCode).Value = objTr.DeductionCode
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionCode).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionDesc).Value = objTr.DeductionDesc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductionCode).Tag = objTr.arrSTDed

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACCode).Value = objTr.GL_Account_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACName).Value = objTr.GL_Account_Desc

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSACCode).Value = objTr.SAC_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSACName).Value = objTr.SAC_Name

                    If clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colACCode).Value) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colACCode).Value), Nothing)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGLType).Value = ""
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Discount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Discount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amount_less_Discount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Total_Amount
                    TotalAmt += objTr.Total_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAChgCode).Value = objTr.AddChargeCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAChgName).Value = objTr.AddChargeDesc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsUnclaimedTax).Value = objTr.is_Unclaimed_Tax
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.Invoice_Type

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objTr.TAX1_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objTr.TAX2_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objTr.TAX3_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objTr.TAX4_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objTr.TAX5_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objTr.TAX6_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objTr.TAX7_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objTr.TAX8_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objTr.TAX9_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objTr.TAX10_Base_Amt


                Next


                If clsCommon.myLen(obj.Posting_Date) <= 0 Then
                    gv1.Rows.AddNew()
                    gvAC.Rows.AddNew()
                    txtDate.Enabled = True
                Else
                    txtDate.Enabled = False
                End If
                SetitemWiseTaxOnlySetting()

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  MULTICURRENCY

                '=====================if document go for approval then no post button visible or if document contain related setting
                btnPost.Visible = MyBase.isPostFlag
                If Not clsApply_Approval.Visibility_PostButtonForApproval(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), clsCommon.myCdbl(lblTotRAmt1.Text), 0, "") Then
                    btnPost.Visible = False
                    If UsLock1.Status = ERPTransactionStatus.Pending Then
                        UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), Nothing)
                    End If
                End If
                '============================================

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Document_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Document_No)

                If CheckDocAmountInAPInvoiceEntry = True Then
                    txtTotalAmt.Text = lblTotRAmt1.Text
                End If
                FillVendorDetails()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            If rbtnTaxCalAutomatic.IsChecked Then
                'If rbtnTaxCalAutomatic.IsChecked AndAlso clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVendorTaxRateIO", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRowWihtRowNo(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnViewTDSDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub

    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            If UsLock1.Status = ERPTransactionStatus.Pending Then
                UpdateTDSAmount()
            End If
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            'If (frm.ObjReturn IsNot Nothing) Then
            objRemittance = frm.ObjReturn
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
            If (myMessages.postConfirm()) Then
                'For Each gr As GridViewRowInfo In gv2.Rows
                '    Dim strTaxCode As String = clsCommon.myCstr(gr.Cells(colTTaxAutCode).Value)
                '    Dim dblTaxRate As Double = clsCommon.myCdbl(gr.Cells(colTTaxRate).Value)
                '    Dim IsSurTax As Boolean = clsCommon.myCBool(gr.Cells(colTIsSurTax).Value)
                '    Dim strSurTaxCode As String = clsCommon.myCstr(gr.Cells(colTSurTaxCode).Value)
                '    Dim IsTaxable As Boolean = clsCommon.myCBool(gr.Cells(colTIsTaxable).Value)

                'Next
                If Not AllowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsVedorInvoiceHead.PostData(MyBase.Form_ID, txtDocNo.Value, txtRefDocNo.Value)) Then
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
                clsCommon.MyMessageBoxShow(msg)
                LoadData(txtDocNo.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
                If (clsVedorInvoiceHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
            '----------Added By--Pankaj Kumar-----For GL Security-----31/08/2012
            Dim Arrloc As New ArrayList
            Dim ArrAcc As New ArrayList
            clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = " and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC')"
            Else
                WhrCls = " and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC') AND substring(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code,(len(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code IN (" + clsCommon.GetMulcallString(ArrAcc) + ")"
            End If
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                WhrCls += " and TSPL_VENDOR_INVOICE_HEAD .ISProcurementDeduction =1 "
            Else
                WhrCls += " and TSPL_VENDOR_INVOICE_HEAD .ISProcurementDeduction =0 "
            End If
            '-------------------------Code Ends Here------------------
            Dim qry As String = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_No="
            Select Case NavType
                Case NavigatorType.First
                    qry += "(select MIN(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where 2=2  " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += "(select Max(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where 2=2  " + WhrCls + ")"
                Case NavigatorType.Next
                    qry += "(select Min(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where TSPL_VENDOR_INVOICE_HEAD.Document_No>'" + txtDocNo.Value + "' " + WhrCls + ")"
                Case NavigatorType.Previous
                    qry += "(select Max(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where TSPL_VENDOR_INVOICE_HEAD.Document_No<'" + txtDocNo.Value + "' " + WhrCls + ")"
            End Select
            LoadData(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_VENDOR_INVOICE_HEAD.Document_No as DocumentNo,Invoice_Entry_Date as Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],Vendor_Invoice_No as [Vendor Invoice No],Against_POInvoice_No as [PO Invoice No],Vendor_Invoice_Date as [Vendor Invoice Date],(case when len(Posting_Date) is null then 'UnPosted' else 'Posted' end) as [Status],Account_Set as AccountSet,Against_PurchaseReturn_No as [PO Return No],TSPL_VENDOR_INVOICE_HEAD.Against_Acquisition as [Acquisition No],TSPL_VENDOR_INVOICE_HEAD.Against_VCGL As [Against VCGL],ISNULL(TSPL_VENDOR_INVOICE_HEAD.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_VENDOR_INVOICE_HEAD.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code]," &
                            "TSPL_VENDOR_INVOICE_head.refDocNo as [Reference Doc No],Against_POInvoice_No as [Against PO Invoice No] ,Against_PurchaseReturn_No as [Against Purchase Return No]," &
                            "Against_Acquisition as [Against Acquisition],TSPL_VENDOR_INVOICE_head.invoice_type as [Invoice Type]," &
                            "against_millkpurchaseinvoice_No as [Against Milk Purchase Invoice No],Against_bulkmillkpurchaseinvoice_No as [Against Bulk Milk Purchase Invoice No]," &
                            "against_asset_work as [Against Asset Work],case when Document_Type='C' then 'Credit Note'  when Document_Type ='D' then 'Debit Note'  when document_type='I' then 'Invoice' end  as [Document Type],case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered=0 then 'No' else 'Yes' end as GSTRegistered,Purchase_Tax_Invoice,Purchase_Tax_Invoice_Type " &
                            " ,TSPL_VENDOR_INVOICE_HEAD.Against_Salary_Generation_Code as [Against Salary Generation],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Plant Name] from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 " &
                             " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                             " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                             "  Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC  Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code   "
        '----------Added By--Pankaj Kumar-----For GL Security-----31/08/2012
        Dim Arrloc As New ArrayList
        Dim ArrAcc As New ArrayList
        clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
        Dim WhrCls As String = ""
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = " TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC')"
        Else
            WhrCls = " TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC') and ( substring(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code,(len(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code IN (" + clsCommon.GetMulcallString(ArrAcc) + "))"
        End If
        If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
            WhrCls += " and TSPL_VENDOR_INVOICE_HEAD .ISProcurementDeduction =1 "
        Else
            WhrCls += " and TSPL_VENDOR_INVOICE_HEAD .ISProcurementDeduction =0 "
        End If

        '-------------------------Code Ends Here------------------
        LoadData(clsCommon.ShowSelectForm("APInvcSLCtr", qry, "DocumentNo", WhrCls, txtDocNo.Value, "DocumentNo", isButtonClicked, "(case when TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null then TSPL_VENDOR_INVOICE_HEAD.Posting_Date else convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) end)"))

    End Sub

    Private Sub txtDocNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()

    End Sub

    Sub PrintData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Document No not found to print", Me.Text)
        End If
        Dim strDocNo As String = clsCommon.myCstr(txtDocNo.Value)

        ''richa KDI/11/07/18-000397 pick vendor name from vendor master
        '    Dim qry As String = "select *,TSPL_COMPANY_MASTER .Logo_Img ,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,TSPL_VENDOR_MASTER.Vendor_Name  from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Document_Type) as Document_Type,max(Loc_Code) as Loc_Code,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No," & Environment.NewLine & _
        '" max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_NameOLD,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription,max(Hirerachy_Code) as Hirerachy_Code ,max(Cost_Centre_Code) as Cost_Centre_Code,MAX(Hirerachy_Code3) AS Hirerachy_Code3,MAX(Hirerachy_Code4) AS Hirerachy_Code4  from(select  '' as FromDate,'' as ToDate, '' as Location,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Type,'' as Vendor, '" & strDocNo & "' as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,Hirerachy_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Cost_Centre_Code,Hirerachy_Code3,Hirerachy_Code4  ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType  , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription,final.Detail_Line_No   from ( " & Environment.NewLine & _
        '" SELECT TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No as Document_No,TSPL_JOURNAL_DETAILS.Account_code as ACCode, TSPL_JOURNAL_DETAILS.Account_Desc as ACName,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,TSPL_JOURNAL_DETAILS.Hirerachy_Code as Hirerachy_Code,TSPL_JOURNAL_DETAILS.Cost_Centre_Code as Cost_Centre_Code, VI.Hirerachy_Code3 ,VI.Hirerachy_Code4  " & Environment.NewLine & _
        '" FROM TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "

        ''RICHA AGARWAL 24 DEC 2018,  SELECT APPROPRIATE COLUMNS FROM INNER QUERY OTHERWISE SAME COLUMN HAS BEEN FETCHED MULTIPLE TIMES SO THAT WRONG DATA SHOWN ON REPORT
        Dim qry As String = "select isnull (TBL_Remarks.Rmarks,'') as AccCodeRemarks, '' as SSC_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,xxxx.RefDocNo ,xxxx.RefDocType ,xxxx.Document_No ,xxxx.ACCode ,xxxx.ACName ,xxxx.DrAmt ,xxxx.CrAmt ,xxxx.Comp_Name ,xxxx.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,xxxx.Invoice_Entry_Date ,xxxx.Due_Date ,xxxx.Description ,xxxx.Vendor_Invoice_Date ,xxxx.CreateBy ,xxxx.ApproveBy ,xxxx.InvStatus ,xxxx.InvoiceType ,xxxx.Vendor_Invoice_No , '' as Remarks,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2 ,TSPL_LOCATION_MASTER.Add3 ,TSPL_LOCATION_MASTER.Add4 ,XXXX.Document_Type,TSPL_COMPANY_MASTER .Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,XXXX.TapalNo,XXXX.DateAndTime,XXXX.Vendor_Bank_Code,XXXX.Vendor_Bank_Name,XXXX.Branch_IFSC_Code,XXXX.Branch_Name,XXXX.Vendor_Bank_ACNo from (select MAX( FromDate) as FromDate,max(ToDate) as ToDate,max(Location) as Location,max(Document_Type) as Document_Type,max(Loc_Code) as Loc_Code,max(Vendor) as Vendor,max(Document) as Document,max(Document_No) as Document_No," & Environment.NewLine &
    " max(ACCode) as ACCode,max(ACName) as ACName, case when SUM(DrAmt-CrAmt)>0 then sum(DrAmt-CrAmt) else 0 end   as DrAmt ,case when SUM( CrAmt-DrAmt)>0 then SUM( CrAmt-DrAmt) else 0 end   as CrAmt,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,max(Vendor_Code) as Vendor_Code,max(Invoice_Entry_Date) as Invoice_Entry_Date,max(RefDocNo) as RefDocNo,max(RefDocType) as RefDocType,max(Vendor_Name) as Vendor_NameOLD,max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Due_Date) as Due_Date,max(Description) as Description,max(Vendor_Invoice_No) as Vendor_Invoice_No,max(Vendor_Invoice_Date) as Vendor_Invoice_Date,max(CreateBy) as CreateBy,max(ApproveBy) as ApproveBy,max(InvStatus) as InvStatus,max(InvoiceType) as InvoiceType,max(RefDocDescription) as RefDocDescription,max(Hirerachy_Code) as Hirerachy_Code ,max(Cost_Centre_Code) as Cost_Centre_Code,MAX(Hirerachy_Code3) AS Hirerachy_Code3,MAX(Hirerachy_Code4) AS Hirerachy_Code4,MAX(TapalNo) AS TapalNo,MAX(DateAndTime) AS DateAndTime,MAX(Vendor_Bank_Code) as Vendor_Bank_Code,max(Vendor_Bank_Name) as Vendor_Bank_Name,max(Branch_IFSC_Code) as Branch_IFSC_Code,max(Branch_Name) as Branch_Name,max(Vendor_Bank_ACNo) as Vendor_Bank_ACNo  from(select  '' as FromDate,'' as ToDate, '' as Location,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Type,'' as Vendor, '" & strDocNo & "' as Document, Final.Document_No,final.ACCode,Final.ACName,DrAmt,CrAmt,Hirerachy_Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Cost_Centre_Code,Hirerachy_Code3,Hirerachy_Code4  ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Created_By  ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Due_Date,TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,CreatedBy.User_Name as CreateBy,AuthorisedBy.User_Name as ApproveBy,case when ((TSPL_VENDOR_INVOICE_HEAD.Posting_Date IS null ) Or (TSPL_VENDOR_INVOICE_HEAD.Posting_Date='') ) then 'Pending' else 'Posted' end as InvStatus,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Bill Inward Voucher' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end end end as InvoiceType  , ( select case when LEN(ISNULL(RefDocType,''))>0 then case when RefDocType='S' then 'SRN' else case when RefDocType='AP' then 'AP Invoice' end end +' : '+RefDocNo  +' - ' +(case when RefDocType='S' then (Select top 1 convert(varchar(100),SRN_Date,110)   from TSPL_SRN_HEAD where SRN_No =RefDocNo) else (select top 1 convert(varchar(100),Invoice_Entry_Date,110)  from TSPL_VENDOR_INVOICE_HEAD where RefDocNo  = RefDocNo) end)  else '' end from TSPL_VENDOR_INVOICE_HEAD where Document_No=Final.Document_No) as RefDocDescription,final.Detail_Line_No,TSPL_VENDOR_INVOICE_HEAD.TapalNo,TSPL_VENDOR_INVOICE_HEAD.DateAndTime ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Bank_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Bank_Name,TSPL_VENDOR_INVOICE_HEAD.Branch_IFSC_Code,TSPL_VENDOR_INVOICE_HEAD.Branch_Name,TSPL_VENDOR_INVOICE_HEAD.Vendor_Bank_ACNo   from ( " & Environment.NewLine &
    " SELECT TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No as Document_No,TSPL_JOURNAL_DETAILS.Account_code as ACCode, TSPL_JOURNAL_DETAILS.Account_Desc as ACName,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,TSPL_JOURNAL_DETAILS.Hirerachy_Code as Hirerachy_Code,TSPL_JOURNAL_DETAILS.Cost_Centre_Code as Cost_Centre_Code, VI.Hirerachy_Code3 ,VI.Hirerachy_Code4  " & Environment.NewLine &
    " FROM TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "


        qry = qry + " LEFT OUTER JOIN (select Detail_Line_No,TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code," & Environment.NewLine &
        " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4, TSPL_VENDOR_INVOICE_HEAD.RefDocNo, TSPL_VENDOR_INVOICE_HEAD.RefDocType " & Environment.NewLine &
        " from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  " & Environment.NewLine &
        " GROUP BY TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, " & Environment.NewLine &
        " TSPL_VENDOR_INVOICE_DETAIL.Cost_Centre_Code,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code3,TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code4,TSPL_VENDOR_INVOICE_HEAD.RefDocNo,TSPL_VENDOR_INVOICE_HEAD.RefDocType,Detail_Line_No ) VI" & Environment.NewLine &
        "  on VI.Document_No  =TSPL_JOURNAL_MASTER.Source_Doc_No and VI.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code AND TSPL_JOURNAL_DETAILS.Detail_Line_No=VI.Detail_Line_No " & Environment.NewLine &
        " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =VI.Cost_Centre_Code  left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code =VI.Hirerachy_Code  where  TSPL_JOURNAL_MASTER.Source_Doc_No = '" & strDocNo & "' " & Environment.NewLine &
        " group by TSPL_JOURNAL_DETAILS.Detail_Line_No ,VI.RefDocNo,VI.RefDocType, TSPL_JOURNAL_MASTER.Source_doc_No ,TSPL_JOURNAL_DETAILS.Account_code , TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,TSPL_JOURNAL_DETAILS.Hirerachy_Code " & Environment.NewLine &
        " ,TSPL_JOURNAL_DETAILS.Cost_Centre_Code,VI.Hirerachy_Code3,VI.Hirerachy_Code4 " & Environment.NewLine &
         " )Final left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=final.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VENDOR_INVOICE_HEAD.Comp_Code left outer join TSPL_USER_MASTER as CreatedBy on CreatedBy.User_Code=TSPL_VENDOR_INVOICE_HEAD.Created_By left outer join TSPL_USER_MASTER as AuthorisedBy on AuthorisedBy .User_Code=TSPL_VENDOR_INVOICE_HEAD.Modify_By left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =final.Cost_Centre_Code where 2=2  and TSPL_VENDOR_INVOICE_HEAD.Document_No in ('" & strDocNo & "') " & Environment.NewLine &
        " )xxx     group by Document_No  ,ACCode ,Detail_Line_No " & Environment.NewLine &
        " )xxxx 
         left outer join (SELECT  TSPL_VENDOR_INVOICE_DETAIL2.GL_Account_Code , STUFF((SELECT distinct ',' + (TSPL_VENDOR_INVOICE_DETAIL.Remarks) as Remarks   from TSPL_VENDOR_INVOICE_DETAIL   where TSPL_VENDOR_INVOICE_DETAIL2.GL_Account_Code = TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code and len(isnull (Remarks,'')) > 0 and TSPL_VENDOR_INVOICE_DETAIL.Document_No  = '" + strDocNo + "' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  as Rmarks
         from TSPL_VENDOR_INVOICE_DETAIL  as TSPL_VENDOR_INVOICE_DETAIL2  where  TSPL_VENDOR_INVOICE_DETAIL2.Document_No =  '" + strDocNo + "'  group by GL_Account_Code)  TBL_Remarks on TBL_Remarks.GL_Account_Code = xxxx.ACCode 

        left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxxx.Comp_Code  left join TSPL_LOCATION_MASTER on left(TSPL_LOCATION_MASTER.Location_Code,3)=xxxx.Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = xxxx.Vendor_Code   left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state   order by xxxx.Document_No,xxxx.DrAmt desc,xxxx.CrAmt desc"



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim qry1 As String = "select  TSPL_ITEM_MASTER.HSN_Code, TSPL_SRN_DETAIL.Item_Code ,TSPL_SRN_DETAIL.Item_Desc,TSPL_VENDOR_INVOICE_HEAD .Description ,TSPL_VENDOR_INVOICE_HEAD.RefDocNo ,TSPL_VENDOR_INVOICE_HEAD.RefDocType   from TSPL_SRN_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_VENDOR_INVOICE_HEAD .RefDocNo  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SRN_DETAIL.Item_Code where RefDocType ='S'and TSPL_VENDOR_INVOICE_HEAD .Document_No in('" & strDocNo & "') and ISNULL(Against_POInvoice_No,'')= '' and ISNULL(Against_PurchaseReturn_No,'')= ''  "
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If SettingCostCenterlevel AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptAPInvoice_Hierarchy", "AP Invoice", "AP_InvoiceDetails.rpt", clsCommon.myCDate(txtDate.Value))
            Else
                frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, qry1, "rptAPInvoice", "AP Invoice", "AP_InvoiceDetails.rpt", clsCommon.myCDate(txtDate.Value))
            End If
            frmCRV = Nothing

        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colACCode) = CompairStringResult.Equal Then
                OpenGLAccount(True)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colAChgCode) = CompairStringResult.Equal Then
                OpenAdditionCharges(True)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colDocNo) = CompairStringResult.Equal Then
                OpenInvoiceNo(True)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            PrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.V AndAlso MyBase.isPostFlag Then
            ViewTDS()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso btnDelete.Enabled Then
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
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                          " TSPL_VENDOR_INVOICE_HEAD   " + Environment.NewLine +
                          " TSPL_VENDOR_INVOICE_DETAIL  " + Environment.NewLine +
                          " TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL (For AP Secondary Tranporter Deduction Detail) " + Environment.NewLine +
                          " TSPL_REMITTANCE (For Remittance) " + Environment.NewLine +
                          " TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine +
                          " TSPL_AP_Invoice_Asset_EMI_Details " + Environment.NewLine +
                          " TSPL_AP_Invoice_Advance_Interest " + Environment.NewLine +
                          " TSPL_APPROVAL_LEVEL_SCREEN " + Environment.NewLine +
                          " TSPL_APPROVAL_LEVEL_SCREEN_HISTORY " + Environment.NewLine +
                          " TSPL_PROVISION_ENTRY_KNOCKOFF " + Environment.NewLine +
                          " TSPL_Bulk_MILK_PURCHASE_INVOICE_HEAD (update during Journal Entry) " + Environment.NewLine +
                          " TSPL_MILK_PURCHASE_INVOICE_HEAD (update during Journal Entry) " + Environment.NewLine +
                          " TSPL_PI_HEAD (update during Journal Entry) " + Environment.NewLine +
                          " TSPL_PI_HEAD (update during Journal Entry) " + Environment.NewLine +
                          " TSPL_ADJUSTMENT_HEADER  " + Environment.NewLine +
                          " TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine +
                          " TSPL_SALE_INVOICE_HEAD " + Environment.NewLine +
                          " TSPL_INVENTORY_MOVEMENT (For Store Adjustment) " + Environment.NewLine +
                          " TSPL_BATCH_ITEM (During Inventory Movement save) ")
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub gv1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
        If TypeOf Me.gv1.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gv1.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)

        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F5 Then
            OpenSecondaryTransporterDeduction()
        End If
    End Sub

    Private Sub MyComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbRefType.SelectedIndexChanged
        txtRefDocNo.Value = ""
        txtRefDocNo.Enabled = False
        repoAdChagCode.IsVisible = False
        repoAdChagName.IsVisible = False
        If clsCommon.myLen(cmbRefType.SelectedValue) > 0 Then
            txtRefDocNo.Enabled = True
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "S") = CompairStringResult.Equal Then
            repoAdChagCode.IsVisible = True
            repoAdChagName.IsVisible = True
            txtVendorInvoiceNo.Enabled = True
            txtVendorInvDatre.Enabled = True
            'chkProvision.Visible = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
            txtVendorInvoiceNo.Enabled = False
            txtVendorInvDatre.Enabled = False
            pnlDeduction.Visible = False

            'chkProvision.Visible = False
        Else
            txtVendorInvoiceNo.Enabled = True
            txtVendorInvDatre.Enabled = True
            pnlDeduction.Visible = True
            'chkProvision.Visible = True
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try

            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colACCode) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colAChgCode).Value) > 0 Then
                        gv1.CurrentRow.Cells(colACCode).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colACCode).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colIsUnclaimedTax) Then
                    If rbtnTaxCalManual.IsChecked Then
                        gv1.CurrentRow.Cells(colIsUnclaimedTax).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colIsUnclaimedTax).ReadOnly = False
                    End If

                ElseIf e.Column Is gv1.Columns(colHierarchyCode) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHierarchyCode).ReadOnly = True
                    Else
                        If SettingCostCenter Or SettingCostCenterlevel Then
                            gv1.CurrentRow.Cells(colHierarchyCode).ReadOnly = False
                        Else
                            gv1.CurrentRow.Cells(colHierarchyCode).ReadOnly = True
                        End If
                    End If
                ElseIf e.Column Is gv1.Columns(colCostCenterCode) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 OrElse SettingCostCenterlevel Then
                        gv1.CurrentRow.Cells(colCostCenterCode).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colCostCenterCode).ReadOnly = Not SettingCostCenter
                    End If
                ElseIf e.Column Is gv1.Columns(colHierarchyLevel4) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 OrElse Not SettingCostCenterlevel OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colHierarchyCode).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHierarchyLevel4).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colHierarchyLevel4).ReadOnly = Not (clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) = 4)
                    End If
                ElseIf e.Column Is gv1.Columns(colHierarchyLevel3) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 OrElse Not SettingCostCenterlevel OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colHierarchyCode).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHierarchyLevel3).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colHierarchyLevel3).ReadOnly = Not (clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) = 3)
                    End If
                ElseIf e.Column Is gv1.Columns(colHierarchyLevel2) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 OrElse Not SettingCostCenterlevel OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colHierarchyCode).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHierarchyLevel2).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colHierarchyLevel2).ReadOnly = Not (clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) = 2)
                    End If
                ElseIf e.Column Is gv1.Columns(colHierarchyLevel1) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colGLType).Value), "Balance Sheet") = CompairStringResult.Equal OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colGLType).Value) <= 0 OrElse Not SettingCostCenterlevel OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colHierarchyCode).Value) <= 0 Then
                        gv1.CurrentRow.Cells(colHierarchyLevel1).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colHierarchyLevel1).ReadOnly = Not (clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevelNumber).Value) = 1)
                    End If
                End If
            End If
            'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            'cell.GradientStyle = GradientStyles.Solid
            'cell.BackColor = Color.FromArgb(243, 181, 51)
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub

    Private Sub txtRefDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRefDocNo._MYValidating
        Try
            Dim RefDate As String
            If clsCommon.myLen(cmbRefType.SelectedValue) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "S") = CompairStringResult.Equal Then

                    Dim qry As String = "select SRN_No as Code,SRN_Date,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD"
                    Dim whrclas As String = " Status=1 "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrclas += " and TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("APINVSRNFND", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)
                    fndProject.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Project_Id,'') from TSPL_SRN_HEAD where SRN_No='" + txtRefDocNo.Value + "'"))
                    lblProject.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
                    Dim qry As String = "select Document_No as Code,Invoice_Entry_Date as Date,Vendor_Code as [Vendor Code],Vendor_Name as Vendor,Vendor_Invoice_No as [Vendor Invoice No],Vendor_Invoice_Date as [Vendor Invoice Date] from TSPL_VENDOR_INVOICE_HEAD "
                    Dim whrclas As String = "Document_Type='I' and LEN(Posting_Date)>0 and Vendor_Code='" + TxtVendorNo.Value + "'"
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                        whrclas += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
                    End If

                    txtRefDocNo.Value = clsCommon.ShowSelectForm("APINVAPInv", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)
                    If clsCommon.myLen(txtRefDocNo.Value) > 0 Then
                        RefDate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Entry_Date as Date from TSPL_VENDOR_INVOICE_HEAD  Where Document_No='" + clsCommon.myCstr(txtRefDocNo.Value) + "'"))
                        If clsCommon.GetPrintDate(RefDate, "yyyy/MM/dd") > clsCommon.GetPrintDate(clsCommon.myCstr(txtDate.Value), "yyyy/MM/dd") Then
                            clsCommon.MyMessageBoxShow("Please check ! ref doc date must be greater than from document date for ref doc no.( " & clsCommon.myCstr(txtRefDocNo.Value) & " ).")
                            txtRefDocNo.Value = ""
                            txtVendorInvoiceNo.Text = ""
                            Return
                        End If

                        txtVendorInvoiceNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Invoice_No as [Vendor Invoice No] from TSPL_VENDOR_INVOICE_HEAD Where Document_No='" + clsCommon.myCstr(txtRefDocNo.Value) + "'"))
                        fndProject.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Project_Id,'') AS Project_Id from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtRefDocNo.Value + "'"))
                        lblProject.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'"))
                        txtVendorInvDatre.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Invoice_Date from TSPL_VENDOR_INVOICE_HEAD Where Document_No='" + clsCommon.myCstr(txtRefDocNo.Value) + "'"))
                        ' ChkSecurity.Checked = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when coalesce(is_security,0)=0 then false else true end from TSPL_VENDOR_INVOICE_HEAD Where Document_No='" + clsCommon.myCstr(txtRefDocNo.Value) + "'"))
                    Else
                        txtVendorInvoiceNo.Text = ""
                        fndProject.Value = ""
                        lblProject.Text = ""
                        txtVendorInvoiceNo.Text = ""
                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "API") = CompairStringResult.Equal Then
                    Dim qry As String = " SELECT zzz.Vendor_Code,zzz.[Vendor Name],zzz.[Vendor Invoice date],zzz.[Vendor Invoice No],zzz.[AP Document No],zzz.[AP Document date],zzz.[PI Document No] as PI_NO,zzz.PI_Date
                    FROM (SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name AS [Vendor Name],
                            CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, '') <> ''  THEN TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date 
                                ELSE NULL END AS [Vendor Invoice date], TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AS [Vendor Invoice No], TSPL_VENDOR_INVOICE_HEAD.Document_No AS [AP Document No],  TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date AS [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No AS [PI Document No],
                            TSPL_PI_HEAD.PI_Date FROM TSPL_VENDOR_INVOICE_HEAD LEFT OUTER JOIN  TSPL_PI_HEAD 
                        ON  TSPL_PI_HEAD.PI_No = TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No
                        LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_No = TSPL_PI_DETAIL.PI_No
                        LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code) zzz
                        LEFT OUTER JOIN ( SELECT TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name AS [Vendor Name],
                        CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No, '') <> '' THEN TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ELSE NULL  END AS [Vendor Invoice date], TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AS [Vendor Invoice No], TSPL_VENDOR_INVOICE_HEAD.Document_No AS [AP Document No], TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date AS [AP Document date],TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No AS [PI Document No], TSPL_PI_HEAD.PI_Date FROM TSPL_VENDOR_INVOICE_HEAD
                          LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No = TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No
                          LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_HEAD.PI_No = TSPL_PI_DETAIL.PI_No
                          LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ) T1 
                    ON T1.[AP Document No] = zzz.[AP Document No] "
                    Dim whrclas As String = " 1=1 and zzz.[PI Document No] is not null "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                        whrclas += ""
                    End If
                    whrclas += " GROUP BY zzz.Vendor_Code,zzz.[Vendor Name],zzz.[Vendor Invoice date],zzz.[Vendor Invoice No],zzz.[AP Document No],zzz.[AP Document date], zzz.[PI Document No],zzz.PI_Date "
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("WORKORDERJOBAP", qry, "PI_NO", whrclas, txtRefDocNo.Value, "PI_NO", isButtonClicked)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "WO") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtlocation.Value) <= 0 Then
                        Throw New Exception("Please select Location first.")
                    End If
                    If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
                        Throw New Exception("Please select Vendor first.")
                    End If
                    ''richa done on 12 Sep,2017
                    Dim BalQry As String = clsVedorInvoiceHead.GetWorkOrderBalanceAmountBaseQry("", "")
                    Dim qry As String = "select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as [Vendor code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as [Vendor Name],TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location as [Bill To Location],Balance.Balance_WO_Amt as [Order Balance] from TSPL_PURCHASE_ORDER_HEAD " &
                        " left join (" & BalQry & ") as Balance on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=Balance.PurchaseOrder_No "
                    Dim whrclas As String = " TSPL_PURCHASE_ORDER_HEAD.Status='1' and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" & TxtVendorNo.Value & "' and  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (select Location_Code   from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & txtlocation.Value & "' and Is_Sub_Location='N' and Is_Section='N') and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type ='J'  and Balance.Balance_WO_Amt>0 "
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KDIL") = CompairStringResult.Equal Then
                        whrclas += " and item_type='N'" 'only non-inventory items ,ref. by Amit Sir
                    End If
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrclas += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("WORKORDERJOBAP", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)
                    fndProject.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Project_Id from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + txtRefDocNo.Value + "'"))
                    lblProject.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "BS") = CompairStringResult.Equal Then
                    Dim qry As String = "select TSPL_Bulk_MILK_SRN.SRN_NO as Code,TSPL_Bulk_MILK_SRN.SRN_Date as Date,TSPL_Bulk_MILK_SRN.Vendor_Code as VendorCode ,TSPL_VENDOR_MASTER.Vendor_Name as Vendor,TSPL_Bulk_MILK_SRN.Loc_Code as LocationCode ,TSPL_LOCATION_MASTER.Location_Desc as Location " &
                    " from TSPL_Bulk_MILK_SRN " &
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Bulk_MILK_SRN.Vendor_Code " &
                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Bulk_MILK_SRN.Loc_Code "

                    Dim whrclas As String = " TSPL_Bulk_MILK_SRN.isPosted = 1 "
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrclas += " and TSPL_Bulk_MILK_SRN.Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If

                    txtRefDocNo.Value = clsCommon.ShowSelectForm("BullkSRNAP", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " where TSPL_Bulk_MILK_SRN.SRN_NO='" + txtRefDocNo.Value + "'")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        TxtVendorNo.Value = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
                        lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor"))
                        txtlocation.Value = clsCommon.myCstr(dt.Rows(0)("LocationCode"))
                        LblLocDesp.Text = clsCommon.myCstr(dt.Rows(0)("Location"))

                        txtChangeVendorNo()
                        txtTaxGroup_TxtChanged()
                        txtTermCode_TxtChanged()
                        SetMultiCurrencyVisibility()
                    End If
                End If
                fndProject.Enabled = False
                lblProject.Enabled = False

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridAC()
        gvAC.Rows.Clear()
        gvAC.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colAChgCode
        repoACCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 150
        repoACCode.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colAChgName
        repoACName.Width = 300
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colAChgAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACAmt)

        gvAC.AllowAddNewRow = False
        gvAC.ShowGroupPanel = False
        gvAC.AllowColumnReorder = True
        gvAC.AllowRowReorder = False
        gvAC.EnableSorting = False
        gvAC.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAC.MasterTemplate.ShowRowHeaderColumn = False
        gvAC.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub

    Private Sub gvAC_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAC.Columns(colAChgAmount) Then

                        UpdateAllTotals()
                    ElseIf e.Column Is gvAC.Columns(colAChgCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colAChgCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvAC.CurrentRow.Cells(colAChgCode).Value = obj.Code
                            gvAC.CurrentRow.Cells(colAChgName).Value = obj.desc
                        Else
                            gvAC.CurrentRow.Cells(colAChgCode).Value = ""
                            gvAC.CurrentRow.Cells(colAChgName).Value = ""
                            gvAC.CurrentRow.Cells(colAChgAmount).Value = 0
                        End If
                    End If
                End If
                setGridFocusAC()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        'If clsCommon.myLen(Milk_PI_No) <= 0 Then
        '    Dim strPoInvcNo As String = connectSql.RunScalar("select PI_No  from TSPL_PI_HEAD where Vendor_Invoice_No ='" + txtVendorInvoiceNo.Text + "' and Vendor_Code ='" + TxtVendorNo.Value + "'")
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strPoInvcNo)
        'Else
        '    'Dim strPoInvcNo As String = connectSql.RunScalar("select Doc_Code  from TSPL_MILK_PURCHASE_INVOICE_HEAD where Vendor_Invoice_No ='" + txtVendorInvoiceNo.Text + "' and Vendor_Code ='" + TxtVendorNo.Value + "'")
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, Milk_PI_No)
        'End If

        Dim qry As String = "select Against_VCGL,Against_Acquisition,Against_Asset_Work,Against_BulkMillkPurchaseInvoice_No,Against_MillkPurchaseInvoice_No,Against_POInvoice_No,Against_PurchaseReturn_No,Against_VSPItemIssue_No,RefDocType,RefDocNo  from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtDocNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If clsCommon.myLen(dt.Rows(0)("Against_Acquisition")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.FAAcquisitionEntry, "", True, clsCommon.myCstr(dt.Rows(0)("Against_Acquisition")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_Asset_Work")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.FAAssetWork, "", True, clsCommon.myCstr(dt.Rows(0)("Against_Asset_Work")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_BulkMillkPurchaseInvoice_No")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, "", True, clsCommon.myCstr(dt.Rows(0)("Against_BulkMillkPurchaseInvoice_No")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_MillkPurchaseInvoice_No")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.frmMilkPurchaseInvoice, "", True, clsCommon.myCstr(dt.Rows(0)("Against_MillkPurchaseInvoice_No")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_POInvoice_No")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseInvoice, "", True, clsCommon.myCstr(dt.Rows(0)("Against_POInvoice_No")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_PurchaseReturn_No")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.mbtnPurchaseReturn, "", True, clsCommon.myCstr(dt.Rows(0)("Against_PurchaseReturn_No")))
                'ElseIf clsCommon.myLen(dt.Rows(0)("Against_VSPItemIssue_No")) > 0 Then
                '    MDI.ShowForm(clsUserMgtCode.frmVSPItemIssue, "", True, clsCommon.myCstr(dt.Rows(0)("Against_VSPItemIssue_No")))
            ElseIf clsCommon.myLen(dt.Rows(0)("Against_VCGL")) > 0 Then
                MDI.ShowForm(clsUserMgtCode.mbtnVCGLEntry, "", True, clsCommon.myCstr(dt.Rows(0)("Against_VCGL")))
            ElseIf clsCommon.myLen(dt.Rows(0)("RefDocType")) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("RefDocType")), "BM-PR") = CompairStringResult.Equal Then
                    MDI.ShowForm(clsUserMgtCode.FrmMilkPurchaseReturn, "", True, clsCommon.myCstr(dt.Rows(0)("RefDocNo")))
                End If

                'RefDocType()
                'AP()
                'BM -PI
                'BM -PR
                'BS()
                'CH()
                'Milk_HE()
                'Milk_OW()
                'MI -PI
                'REVALUATION ENTRY
                'V_I_Issue()
                'V_I_Issue_Return()
                'VSP -Int()
                'VSP -MP
                'WO()
            End If
        End If

    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        gv2.Columns(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                txtTaxGroup_TxtChanged()
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
                    gv1.Rows(intRowNo).Cells(colIsUnclaimedTax).Value = False
                Next
            End If
        End If
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
            'clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRowWihtRowNo(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        ''Try
        ''    If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
        ''        Dim frm As New FrmPOItemTaxDetails()
        ''        frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
        ''        frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
        ''        frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
        ''        frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
        ''        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
        ''        If clsCommon.myLen(frm.strItemCode) > 0 Then
        ''            frm.ArrIn = New List(Of clsTempItemTaxDetails)
        ''            For ii As Integer = 1 To 10
        ''                Dim strii As String = clsCommon.myCstr(ii)
        ''                Dim obj As New clsTempItemTaxDetails()
        ''                obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
        ''                If clsCommon.myLen(obj.AuthorityCode) > 0 Then
        ''                    obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
        ''                    obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
        ''                    obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
        ''                    obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
        ''                    obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
        ''                    obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
        ''                    obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
        ''                    frm.ArrIn.Add(obj)
        ''                End If
        ''            Next

        ''            frm.ShowDialog()
        ''            If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
        ''                BlankTaxDetailsCurrentRowWihtRowNo(gv1.CurrentRow.Index)
        ''                For ii As Integer = 0 To frm.ArrOut.Count - 1
        ''                    Dim strii As String = clsCommon.myCstr(ii + 1)
        ''                    gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
        ''                    gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
        ''                    gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
        ''                    gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
        ''                    gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
        ''                    gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
        ''                    gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
        ''                Next
        ''                gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
        ''                UpdateAllTotals()
        ''            End If
        ''        End If
        ''    End If
        ''Catch ex As Exception
        ''    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        ''End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()

                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
            ApplyQuickMode()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Dim Sql As String = "select '' as [Date], '' as Vendor, 0 as Amount,'' as LocSegment"
        transportSql.ExporttoExcel(Sql, Me)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim type As String = "I"
        funfillimport(type)
    End Sub
    '----------------------For Credit Note---------------------
    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem6.Click
        Dim type As String = "C"
        funfillimport(type)
    End Sub
    '----------------------For Debit Note------------------------

    Private Sub RadMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem7.Click
        Dim type As String = "D"
        funfillimport(type)
    End Sub

    Sub funfillimport(ByRef type As String)
        Dim BalanceType As String = type
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Counter As Integer = 0
        Dim qry As String = "select top 1 Account_Code,Description from TSPL_GL_ACCOUNTS where LEN(Account_Code)>6 order by Account_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim strACCode As String = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
        Dim strACName As String = clsCommon.myCstr(dt.Rows(0)("Description"))
        If transportSql.importExcel(gv, "Date", "Vendor", "Amount", "LocSegment") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarShow()
            Try
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strVendor As String = clsCommon.myCstr(grow.Cells("Vendor").Value)
                    Dim dblAmt As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    Dim strDate As String = clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MM/yyyy")
                    Dim segment As String = clsCommon.myCstr(grow.Cells("LocSegment").Value)

                    If clsCommon.myLen(strVendor) <= 0 Then
                        Throw New Exception("Vendor not found")
                    End If
                    If clsCommon.myLen(grow.Cells("Date").Value) <= 0 Then
                        Throw New Exception("Transaction Date is not found")
                    End If
                    If dblAmt <= 0 Then
                        Throw New Exception("Amount not found")
                    End If
                    Counter += 1
                    Dim obj As New clsVedorInvoiceHead()
                    'obj.Document_No = txtDocNo.Value
                    obj.Invoice_Entry_Date = strDate
                    obj.Vendor_Code = strVendor
                    obj.loc_code = segment
                    Dim objVendor As clsVendorMaster = clsVendorMaster.GetData(strVendor, trans)
                    obj.Vendor_Name = objVendor.Vendor_Name
                    obj.Vendor_Invoice_No = "Opening Balance " + clsCommon.myCstr(Counter)
                    obj.Vendor_Invoice_Date = strDate
                    obj.Account_Set = objVendor.Vendor_Account
                    'obj.Document_Type = "I"
                    obj.Document_Type = BalanceType
                    obj.Order_No = "C" ''if it is C then can't be put in GL entry
                    obj.Invoice_Type = "AP"
                    obj.On_Hold = False

                    obj.Tax_Group = "NILL"

                    obj.Due_Date = strDate
                    obj.Discount_Base = dblAmt
                    obj.Discount_Amount = 0
                    obj.Amount_Less_Discount = dblAmt
                    obj.Document_Total = dblAmt
                    obj.Balance_Amt = dblAmt


                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                    ''Anubhooti 17-Nov-2014 BM00000004657
                    dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + obj.Account_Set + "'", trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        If clsCommon.myCdbl(obj.Discount_Amount) > 0 Then
                            obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        End If
                    End If


                    obj.Total_Add_Charge = 0



                    obj.Arr = New List(Of clsVedorInvoiceDetail)
                    ''For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsVedorInvoiceDetail()
                    objTr.Detail_Line_No = 1





                    '---------------------------------------------------

                    'For retriving Control Account for Vendor

                    'Dim segment As String = clsCommon.myCstr(grow.Cells("LocSegment").Value)

                    ''richa 05 feb,2019  TEC/05/02/19-000411 
                    'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
                    Dim JEWithOPening As Boolean = False
                    If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                        Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                        If clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) <= clsCommon.myCDate(clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy")) Then
                            JEWithOPening = True
                        End If
                    End If
                    Dim qry1 As String = String.Empty
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal And JEWithOPening = True Then
                        qry1 = "select TSPL_VENDOR_ACCOUNT_SET.Opening_Clearing ,tspl_gl_accounts.Description   from TSPL_VENDOR_MASTER   " &
                                        " left outer join TSPL_VENDOR_ACCOUNT_SET  on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account  " &
                                       " left outer join tspl_gl_accounts on  TSPL_VENDOR_ACCOUNT_SET.Payable_Account=tspl_gl_accounts.Account_Code " &
                                        " where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendor + "' "

                    Else
                        qry1 = "select TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,tspl_gl_accounts.Description   from TSPL_VENDOR_MASTER   " &
                                        " left outer join TSPL_VENDOR_ACCOUNT_SET  on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account  " &
                                       " left outer join tspl_gl_accounts on  TSPL_VENDOR_ACCOUNT_SET.Payable_Account=tspl_gl_accounts.Account_Code " &
                                        " where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendor + "' "

                    End If


                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                    Dim strACName1 As String
                    Dim strACCode1 As String
                    If dt1.Rows.Count > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)), "1") = CompairStringResult.Equal And JEWithOPening = True Then
                            strACCode1 = clsCommon.myCstr(dt1.Rows(0)("Opening_Clearing"))
                        Else
                            strACCode1 = clsCommon.myCstr(dt1.Rows(0)("Payable_Account"))
                        End If
                        strACName1 = clsCommon.myCstr(dt1.Rows(0)("Description"))
                    Else
                        Throw New Exception("Account does not exist for Vendor '" + strVendor + "'")
                    End If


                    '-------------For  Segment code overwriting
                    'Dim acccontrol As String
                    ' Dim acccontdesc As String

                    Dim AccountFinal As String = strACCode1.Substring(0, (clsCommon.myLen(strACCode1) - 4)) + "-" + segment

                    Dim qryAcc As String = "select Account_Code,Description from TSPL_GL_ACCOUNTS where Account_Code='" + AccountFinal + "'"
                    Dim dtfinal As DataTable = clsDBFuncationality.GetDataTable(qryAcc, trans)
                    Dim FinalAcc As String
                    Dim FinalDesc As String
                    If dtfinal.Rows.Count > 0 Then
                        FinalAcc = clsCommon.myCstr(dtfinal.Rows(0)("Account_Code"))
                        FinalDesc = clsCommon.myCstr(dtfinal.Rows(0)("Description"))
                    Else
                        Throw New Exception("Account does not exist for Vendor '" + strVendor + "'")
                    End If

                    objTr.GL_Account_Code = FinalAcc
                    objTr.GL_Account_Desc = FinalDesc
                    '---------------------------------------------------




                    'objTr.GL_Account_Code = strACCode
                    'objTr.GL_Account_Desc = strACName
                    objTr.Amount = dblAmt
                    objTr.Discount_Per = 0
                    objTr.Discount = 0
                    objTr.Amount_less_Discount = dblAmt

                    objTr.Total_Tax = 0
                    objTr.Total_Amount = dblAmt

                    objTr.is_Unclaimed_Tax = False ''clsCommon.myCBool(grow.Cells(colIsUnclaimedTax).Value)

                    If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                        obj.Arr.Add(objTr)
                    End If

                    obj.SaveData(obj, True, trans)
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow("Error at Rowno " + clsCommon.myCstr(Counter) + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            Finally
                clsCommon.ProgressBarHide()
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub txtVendorInvoiceNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorInvoiceNo.TextChanged
        'If txtlocation.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Please first select Location")
        '    txtlocation.Focus()
        '    Return
        'End If
    End Sub

    Private Sub txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocation._MYValidating
        Try
            If SettShowMCCFinder Then
                Dim whrCls As String = " 1=1 "
                If Not clsMccMaster.isCurrentUserHO() Then
                    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                    End If
                End If

                whrCls = whrCls & " and  isnull(Loc_Segment_Code ,'')<>''  AND isnull(GIT_Type,'') <>'Y' and Location_Type ='Physical' and isnull(Is_Section,'')<>'Y' and isnull(is_sub_location,'')<>'Y' "
                Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
                If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
                    txtlocation.Value = ""
                    txtMCC.Text = ""
                    lblMCC.Text = ""
                    LblLocDesp.Text = ""
                    Exit Sub
                End If

                txtlocation.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & txtlocation.Value & "' "))
                If SettShowMCCFinder Then
                    txtMCC.Text = clsCommon.myCstr(dr("Code"))
                    lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtMCC.Text + "'"))
                End If

            Else
                Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
                Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                txtlocation.Value = clsCommon.ShowSelectForm("GLsegmentcode", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
                If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                    LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
                Else
                    LblLocDesp.Text = ""
                End If


            End If

            'Dim qry As String = "select Segment_code as Code ,Description  from TSPL_GL_SEGMENT_CODE "


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mbtnExportApTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnExportApTransaction.Click
        Dim Sql As String = "select 'dd-MMM-yyyy' as [Date], '' as Vendor,'' as VendorInvoiceNo,'' as LocSegment,'Exempted' as TaxGroup"
        Dim ii As Integer
        For ii = 0 To 39
            Sql += ",'' as GLAccount" + clsCommon.myCstr(ii + 1) + ",0 as GLAmount" + clsCommon.myCstr(ii + 1)
        Next
        Sql += " ,'' as Remarks,'dd-MMM-yyyy' as [Vendor Invoice Date],'' as [Employee Expense Type]"
        If SettingCostCenter Then
            Sql += ",'' AS [Hirerachy Level]"
            If SettingCostCenterlevel Then
                Sql += ",'' AS [" + gv1.Columns(colHierarchyLevel4).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel3).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel2).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel1).HeaderText + "] "
            Else
                Sql += ",'' AS [Cost Centre]"
            End If
        End If
        transportSql.ExporttoExcel(Sql, Me)
    End Sub

    Private Sub mbtnImportApTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnImportApTransaction.Click
        APInvoiceImport()
    End Sub
    Sub APInvoiceImport()
        cboDocType.SelectedValue = "I"
        If clsCommon.MyMessageBoxShow("You are going to import " & IIf(clsCommon.myCstr(cboDocType.SelectedValue) = "I", "Invoice", IIf(clsCommon.myCstr(cboDocType.SelectedValue) = "D", "Debit Note", "Credit Note")) & Environment.NewLine & "want to continue ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Counter As String = ""
        Dim IsLevel As Boolean = False

        Dim amt As Integer
        Dim discountbase As Double = 0
        Dim colnameacc As String = Nothing
        Dim colnameaccamt As String = Nothing
        Dim flag As Boolean = False
        If SettingCostCenter Then
            If SettingCostCenterlevel Then
                flag = transportSql.importExcel(gv, "Date", "Vendor", "VendorInvoiceNo", "LocSegment", "TaxGroup", "GLAccount1", "GLAmount1", "GLAccount2", "GLAmount2", "GLAccount3", "GLAmount3", "GLAccount4", "GLAmount4", "GLAccount5", "GLAmount5", "GLAccount6", "GLAmount6", "GLAccount7", "GLAmount7", "GLAccount8", "GLAmount8", "GLAccount9", "GLAmount9", "GLAccount10", "GLAmount10", "GLAccount11", "GLAmount11", "GLAccount12", "GLAmount12", "GLAccount13", "GLAmount13", "GLAccount14", "GLAmount14", "GLAccount15", "GLAmount15", "GLAccount16", "GLAmount16", "GLAccount17", "GLAmount17", "GLAccount18", "GLAmount18", "GLAccount19", "GLAmount19", "GLAccount20", "GLAmount20", "GLAccount21", "GLAmount21", "GLAccount22", "GLAmount22", "GLAccount23", "GLAmount23", "GLAccount24", "GLAmount24", "GLAccount25", "GLAmount25", "GLAccount26", "GLAmount26", "GLAccount27", "GLAmount27", "GLAccount28", "GLAmount28", "GLAccount29", "GLAmount29", "GLAccount30", "GLAmount30", "GLAccount31", "GLAmount31", "GLAccount32", "GLAmount32", "GLAccount33", "GLAmount33", "GLAccount34", "GLAmount34", "GLAccount35", "GLAmount35", "GLAccount36", "GLAmount36", "GLAccount37", "GLAmount37", "GLAccount38", "GLAmount38", "GLAccount39", "GLAmount39", "GLAccount40", "GLAmount40", "Vendor Invoice Date", "Employee Expense Type", gv1.Columns(colHierarchyLevel4).HeaderText, gv1.Columns(colHierarchyLevel3).HeaderText, gv1.Columns(colHierarchyLevel2).HeaderText, gv1.Columns(colHierarchyLevel1).HeaderText)
            Else
                flag = transportSql.importExcel(gv, "Date", "Vendor", "VendorInvoiceNo", "LocSegment", "TaxGroup", "GLAccount1", "GLAmount1", "GLAccount2", "GLAmount2", "GLAccount3", "GLAmount3", "GLAccount4", "GLAmount4", "GLAccount5", "GLAmount5", "GLAccount6", "GLAmount6", "GLAccount7", "GLAmount7", "GLAccount8", "GLAmount8", "GLAccount9", "GLAmount9", "GLAccount10", "GLAmount10", "GLAccount11", "GLAmount11", "GLAccount12", "GLAmount12", "GLAccount13", "GLAmount13", "GLAccount14", "GLAmount14", "GLAccount15", "GLAmount15", "GLAccount16", "GLAmount16", "GLAccount17", "GLAmount17", "GLAccount18", "GLAmount18", "GLAccount19", "GLAmount19", "GLAccount20", "GLAmount20", "GLAccount21", "GLAmount21", "GLAccount22", "GLAmount22", "GLAccount23", "GLAmount23", "GLAccount24", "GLAmount24", "GLAccount25", "GLAmount25", "GLAccount26", "GLAmount26", "GLAccount27", "GLAmount27", "GLAccount28", "GLAmount28", "GLAccount29", "GLAmount29", "GLAccount30", "GLAmount30", "GLAccount31", "GLAmount31", "GLAccount32", "GLAmount32", "GLAccount33", "GLAmount33", "GLAccount34", "GLAmount34", "GLAccount35", "GLAmount35", "GLAccount36", "GLAmount36", "GLAccount37", "GLAmount37", "GLAccount38", "GLAmount38", "GLAccount39", "GLAmount39", "GLAccount40", "GLAmount40", "Vendor Invoice Date", "Employee Expense Type", "Hirerachy Level", "Cost Centre")
            End If
        Else
            flag = transportSql.importExcel(gv, "Date", "Vendor", "VendorInvoiceNo", "LocSegment", "TaxGroup", "GLAccount1", "GLAmount1", "GLAccount2", "GLAmount2", "GLAccount3", "GLAmount3", "GLAccount4", "GLAmount4", "GLAccount5", "GLAmount5", "GLAccount6", "GLAmount6", "GLAccount7", "GLAmount7", "GLAccount8", "GLAmount8", "GLAccount9", "GLAmount9", "GLAccount10", "GLAmount10", "GLAccount11", "GLAmount11", "GLAccount12", "GLAmount12", "GLAccount13", "GLAmount13", "GLAccount14", "GLAmount14", "GLAccount15", "GLAmount15", "GLAccount16", "GLAmount16", "GLAccount17", "GLAmount17", "GLAccount18", "GLAmount18", "GLAccount19", "GLAmount19", "GLAccount20", "GLAmount20", "GLAccount21", "GLAmount21", "GLAccount22", "GLAmount22", "GLAccount23", "GLAmount23", "GLAccount24", "GLAmount24", "GLAccount25", "GLAmount25", "GLAccount26", "GLAmount26", "GLAccount27", "GLAmount27", "GLAccount28", "GLAmount28", "GLAccount29", "GLAmount29", "GLAccount30", "GLAmount30", "GLAccount31", "GLAmount31", "GLAccount32", "GLAmount32", "GLAccount33", "GLAmount33", "GLAccount34", "GLAmount34", "GLAccount35", "GLAmount35", "GLAccount36", "GLAmount36", "GLAccount37", "GLAmount37", "GLAccount38", "GLAmount38", "GLAccount39", "GLAmount39", "GLAccount40", "GLAmount40", "Vendor Invoice Date", "Employee Expense Type")
        End If
        If flag Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarShow()
            Try
                Dim obj As clsVedorInvoiceHead
                Dim dtTemp As DataTable
                For Each grow As GridViewRowInfo In gv.Rows

                    amt = 0
                    discountbase = 0
                    colnameacc = ""
                    colnameaccamt = ""

                    Counter = clsCommon.myCstr(grow.Index + 2) + " :"
                    obj = New clsVedorInvoiceHead()
                    obj.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor").Value)
                    If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                        obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & obj.Vendor_Code & "'", trans))
                        If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                            Throw New Exception("Vendor does not exist.")
                        End If
                    Else
                        Throw New Exception("Please enter Vendor Code")
                    End If
                    Dim objVendor As clsVendorMaster = clsVendorMaster.GetData(obj.Vendor_Code, trans)
                    obj.Account_Set = objVendor.Vendor_Account
                    obj.Vendor_Name = objVendor.Vendor_Name
                    obj.Terms_Code = objVendor.Terms_Code
                    If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code, trans) = True Then
                        obj.CURRENCY_CODE = objVendor.CURRENCY_CODE
                    End If
                    If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                        dtTemp = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.txtDate.Value, txtCurrencyCode.Value)
                        If dtTemp.Rows.Count > 0 Then
                            obj.ConvRate = clsCommon.myCdbl(dtTemp.Rows(0).Item("Rate"))
                        Else
                            obj.ConvRate = 1
                        End If
                    End If
                    obj.loc_code = clsCommon.myCstr(grow.Cells("LocSegment").Value)
                    If clsCommon.myLen(obj.loc_code) > 0 Then
                        obj.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code WHERE TSPL_GL_SEGMENT_CODE.Segment_code='" + obj.loc_code + "' AND Seg_No = '7' AND GIT='N'", trans))
                        If clsCommon.myLen(obj.loc_code) <= 0 Then
                            Throw New Exception("Location Segment does not exist.")
                        End If
                    Else
                        Throw New Exception("Please enter Location Segment Code")
                    End If
                    obj.Invoice_Entry_Date = clsCommon.myCstr(grow.Cells("Date").Value)
                    obj.Vendor_Invoice_No = clsCommon.myCstr(grow.Cells("VendorInvoiceNo").Value)
                    obj.Vendor_Invoice_Date = clsCommon.myCstr(grow.Cells("Vendor Invoice Date").Value)
                    obj.Due_Date = obj.Invoice_Entry_Date
                    obj.Terms_Code = objVendor.Terms_Code
                    obj.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                    obj.On_Hold = False
                    For amt = 0 To 39
                        colnameaccamt = "GLAmount" + clsCommon.myCstr(amt + 1)
                        discountbase += clsCommon.myCdbl(grow.Cells(colnameaccamt).Value)
                    Next

                    'obj.Discount_Base = clsCommon.myCdbl(grow.Cells("GLAmount1").Value) + clsCommon.myCdbl(grow.Cells("GLAmount2").Value) + clsCommon.myCdbl(grow.Cells("GLAmount3").Value) + clsCommon.myCdbl(grow.Cells("GLAmount4").Value) + clsCommon.myCdbl(grow.Cells("GLAmount5").Value)
                    obj.Discount_Base = discountbase
                    If obj.Discount_Base <= 0 Then
                        Throw New Exception("Please enter amount.")
                    End If
                    'obj.Discount_Percentage = clsCommon.myCdbl(grow.Cells("Discount %").Value)

                    dtTemp = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code WHERE TSPL_VENDOR_MASTER.Vendor_Code='" + obj.Vendor_Code + "'", trans)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dtTemp.Rows(0)("Payable_Account"))
                        obj.Discount_GL_AC = clsCommon.myCstr(dtTemp.Rows(0)("Discount_Account"))
                    End If
                    If obj.Discount_Percentage > 0 Then
                        obj.Discount_Amount = (obj.Discount_Base * obj.Discount_Percentage) / 100
                    Else
                        obj.Discount_Amount = 0
                    End If
                    obj.Invoice_Type = "AP"
                    obj.Amount_Less_Discount = obj.Discount_Base - obj.Discount_Amount
                    obj.Document_Total = obj.Amount_Less_Discount
                    obj.Balance_Amt = obj.Amount_Less_Discount
                    obj.Total_Landed_Amt = 0
                    obj.is_For_Provision = 0
                    obj.isDeduction = 0 'IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Deduction (Yes/No)").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                    'If obj.isDeduction = 1 Then
                    '    obj.Security = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Security Deduction (Yes/No)").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                    'Else
                    '    obj.Security = 0
                    'End If
                    obj.Security = 0
                    obj.PO_Number = ""
                    obj.Description = clsCommon.myCstr(grow.Cells("Remarks").Value) '"Opening Balance as On " + obj.Invoice_Entry_Date + " for Vendor " + obj.Vendor_Code + ""
                    obj.Tax_Group = "Exempted"
                    obj.PROJECT_ID = ""

                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                    obj.Total_Add_Charge = 0
                    obj.Empty_Amount = 0
                    obj.Is_ProRated = "N"

                    '-------------------------Detail Level Starts From Here----------------
                    obj.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim objTr As New clsVedorInvoiceDetail()
                    '"Deduction Code", "Account Code", "Amount", "Discount %", "Discount Amount", "Remarks"                   

                    For amt = 0 To 39
                        colnameacc = "GLAccount" + clsCommon.myCstr(amt + 1)
                        colnameaccamt = "GLAmount" + clsCommon.myCstr(amt + 1)
                        If clsCommon.myLen(grow.Cells(colnameacc).Value) > 0 Then
                            objTr = New clsVedorInvoiceDetail
                            objTr.Detail_Line_No = obj.Arr.Count + 1
                            objTr.GL_Account_Code = clsCommon.myCstr(grow.Cells(colnameacc).Value)
                            If clsCommon.myLen(objTr.GL_Account_Code) > 0 Then
                                Dim intAcctSeg As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" & objTr.GL_Account_Code & "' and Account_Seg_Code7='" & obj.loc_code & "'", trans))
                                If intAcctSeg = 0 Then
                                    Throw New Exception("Account No " & objTr.GL_Account_Code & " Location segmemt should be  " & obj.loc_code & ".")
                                End If

                                dtTemp = clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & objTr.GL_Account_Code & "'", trans)
                                If dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Account does not exist.")
                                Else
                                    objTr.GL_Account_Code = clsCommon.myCstr(dtTemp.Rows(0)("Account_Code"))
                                    objTr.GL_Account_Desc = clsCommon.myCstr(dtTemp.Rows(0)("Account_Desc"))
                                End If
                                If clsCommon.GetDateWithStartTime(clsCommon.myCDate(obj.Invoice_Entry_Date)) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                                    'richa 17 SEp,2019 TEC/03/07/19-000927
                                    dtTemp = clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & objTr.GL_Account_Code & "' " + " and ( ControlAccount='N' or TSPL_GL_ACCOUNTS.Account_Code IN ( select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1)) ", trans)
                                    If dtTemp.Rows.Count <= 0 Then
                                        Throw New Exception("Account should be type of non Control Account.")
                                    Else
                                        objTr.GL_Account_Code = clsCommon.myCstr(dtTemp.Rows(0)("Account_Code"))
                                        objTr.GL_Account_Desc = clsCommon.myCstr(dtTemp.Rows(0)("Account_Desc"))
                                    End If
                                End If
                            Else
                                Throw New Exception("Please enter Account code")
                            End If

                            Dim strGLType As String = clsPaymentHeader.CheckGLAccountType(objTr.GL_Account_Code, trans)
                            If SettingCostCenter AndAlso Not clsCommon.CompairString(strGLType, "Balance Sheet") = CompairStringResult.Equal Then
                                objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells("Hirerachy Level").Value)
                                If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                                    Throw New Exception("Hierarchy code not found")
                                End If
                                objTr.Hirerachy_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Hirerachy_Code As Row from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'", trans))
                                If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                                    Throw New Exception("Invalid Hierarchy code-" + clsCommon.myCstr(grow.Cells("Hirerachy Level").Value))
                                End If

                                If SettingCostCenterlevel Then
                                    objTr.Hirerachy_Code4 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel4).HeaderText).Value)
                                    objTr.Hirerachy_Code3 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel3).HeaderText).Value)
                                    objTr.Hirerachy_Code2 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel2).HeaderText).Value)
                                    objTr.Hirerachy_Code1 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel1).HeaderText).Value)

                                    If clsCommon.myLen(objTr.Hirerachy_Code4) > 0 Then
                                        objTr.Hirerachy_Code4 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code4 + "'", trans))
                                    End If
                                    If clsCommon.myLen(objTr.Hirerachy_Code3) > 0 Then
                                        objTr.Hirerachy_Code3 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code3 + "'", trans))
                                    End If
                                    If clsCommon.myLen(objTr.Hirerachy_Code2) > 0 Then
                                        objTr.Hirerachy_Code2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code2 + "'", trans))
                                    End If
                                    If clsCommon.myLen(objTr.Hirerachy_Code1) > 0 Then
                                        objTr.Hirerachy_Code1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code1 + "'", trans))
                                    End If
                                    Dim lvl As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + objTr.Hirerachy_Code + "' ", trans))
                                    If lvl > 3 Then
                                        If clsCommon.myLen(objTr.Hirerachy_Code4) <= 0 Then
                                            Throw New Exception("Hierarchy Level 4 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code3) <= 0 Then
                                            Throw New Exception("Hierarchy Level 3 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                            Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                            Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=4 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code4 + "' and HIRERACHY_LEVEL_CODE3='" + objTr.Hirerachy_Code3 + "' and HIRERACHY_LEVEL_CODE2='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                        dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                            Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code4 + "][" + objTr.Hirerachy_Code3 + "][" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                        End If
                                        objTr.Cost_Centre_Code = objTr.Hirerachy_Code4
                                    ElseIf lvl > 2 Then
                                        If clsCommon.myLen(objTr.Hirerachy_Code3) <= 0 Then
                                            Throw New Exception("Hierarchy Level 3 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                            Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                            Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=3 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code3 + "' and HIRERACHY_LEVEL_CODE2='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                        dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                            Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code3 + "][" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                        End If
                                        objTr.Cost_Centre_Code = objTr.Hirerachy_Code3
                                    ElseIf lvl > 1 Then
                                        If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                            Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                            Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=2 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                        dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                            Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                        End If
                                        objTr.Cost_Centre_Code = objTr.Hirerachy_Code2
                                    ElseIf lvl > 0 Then
                                        If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                            Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                        End If
                                        strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=1 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code1 + "'"
                                        dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                            Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code1 + "]")
                                        End If
                                        objTr.Cost_Centre_Code = objTr.Hirerachy_Code1
                                    End If
                                Else
                                    objTr.Cost_Centre_Code = clsCommon.myCstr(grow.Cells("Cost Centre").Value)
                                    If clsCommon.myLen(objTr.Cost_Centre_Code) <= 0 Then
                                        Throw New Exception("Cost center not found")
                                    End If
                                    objTr.Cost_Centre_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Cost_Centre_Code + "'", trans))
                                    If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                                        Throw New Exception("Invalid Cost center-" + clsCommon.myCstr(grow.Cells("Cost Centre").Value))
                                    End If
                                End If
                                If clsCommon.myLen(objTr.Cost_Centre_Code) <= 0 Then
                                    Throw New Exception("Please select Cost Center of GL Account -" + objTr.GL_Account_Code)
                                End If
                            End If


                            objTr.Amount = clsCommon.myCdbl(grow.Cells(colnameaccamt).Value)
                            objTr.Discount_Per = obj.Discount_Percentage
                            objTr.Discount = obj.Discount_Amount
                            objTr.Amount_less_Discount = objTr.Amount - obj.Discount_Amount
                            objTr.Landed_Amount = 0
                            objTr.Total_Tax = 0
                            objTr.Total_Amount = obj.Amount_Less_Discount
                            objTr.AddChargeCode = ""
                            objTr.AddChargeDesc = ""
                            objTr.is_Unclaimed_Tax = False
                            objTr.Remarks = "" 'clsCommon.myCstr(grow.Cells("Remarks").Value)
                            objTr.Invoice_Type = ""
                            objTr.Invoice_No = ""

                            If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                                obj.Arr.Add(objTr)
                            End If
                        Else
                            If amt = 0 Then
                                Throw New Exception("Please enter Account code")
                            End If
                        End If
                    Next
                    Dim EmployeeSalaryGeneration As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEmployeeSalaryIntegration, clsFixedParameterCode.AllowtoEmployeeSalaryIntegration, trans)) = 1, True, False))
                    If EmployeeSalaryGeneration = True Then
                        Dim strEmployeeExpenseType As String = String.Empty
                        strEmployeeExpenseType = clsCommon.myCstr(grow.Cells("Employee Expense Type").Value)

                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsEmployee from TSPL_VENDOR_MASTER where Vendor_Code ='" & obj.Vendor_Code & "' ", trans)), "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strEmployeeExpenseType) > 0 Then
                                If clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "TD") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "I") = CompairStringResult.Equal Then
                                Else
                                    Throw New Exception("Please enter T/TD/S/I for (Travelling/TA-DA/Salary/Imprest) for Employee Expense Type. ")
                                End If
                                obj.Employee_Type = strEmployeeExpenseType.ToUpper

                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Advance_Against_Imprest ,Advance_Against_Travelling,Employee_Salary  from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + obj.Account_Set + "'", trans)

                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                                    Else
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    End If
                                End If

                            End If
                        Else
                            obj.Employee_Type = ""
                        End If
                    End If
                    '---------------------------------Detail Level Ends Here-----------------------------

                    dblPreviousTDSAmt = 0
                    obj.RemittanceObject = clsVSPBillAndIncentiveCalculation.SetVendorTDSDetails(True, True, obj.Document_No, obj.Invoice_Entry_Date, dblPreviousTDSAmt, obj.Vendor_Code, obj.Amount_Less_Discount, obj.Document_Total, trans)
                    ''richa 26 April,2022 tds amount not saved into header table
                    If (obj.RemittanceObject IsNot Nothing) Then
                        obj.TDS_Actual_Amount = clsCommon.myCdbl(obj.RemittanceObject.Actual_Total_TDS)
                    End If
                    obj.SaveData(obj, True, trans)
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Error at Rowno " + Counter + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            Finally
                clsCommon.ProgressBarHide()
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub



    Private Sub RadMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem8.Click
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
                clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
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

    Private Sub RadMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem9.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked AndAlso Not clsCommon.myCBool(e.Row.Cells(colIsUnclaimedTax).Value) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                frm.strTaxType = "P"
                frm.IslocationSegment = True
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
                        BlankTaxDetailsCurrentRowWihtRowNo(gv1.CurrentRow.Index)
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
                        UpdateAllTotals()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode)) > 0 Then
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
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colACCode)) > 0 Then
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
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colACCode)) > 0 Then
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

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim qry As String = "select isnull(Against_Salary_Generation_Code,'') as Against_Salary_Generation_Code,Posting_Date,Against_POInvoice_No,Against_PurchaseReturn_No,ISNULL(Against_VCGL,'') AS Against_VCGL,isnull(Against_BulkMillkPurchaseInvoice_No,'') as Against_BulkMillkPurchaseInvoice_No from tspl_vendor_invoice_head where Document_No='" + txtDocNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '' REASON FOR DELETE 
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If

                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        If clsCommon.myLen(dt.Rows(0)("Posting_Date")) <= 0 Then
                            Throw New Exception("Status should be post to perform this operation")
                        End If
                        If clsCommon.myLen(dt.Rows(0)("Against_POInvoice_No")) > 0 Then
                            Throw New Exception("Current Transaction is created by Purhcase Invoice.So Can't perform this operation")
                        End If
                        If clsCommon.myLen(dt.Rows(0)("Against_PurchaseReturn_No")) > 0 Then
                            Throw New Exception("Current Transaction is created by Purhcase Return.So Can't perform this operation")
                        End If
                        '' Anubhooti 19-Mar-2015 (If AP is against VCGL then system should not allow to reverse/unpost from AP screen itself)
                        If clsCommon.myLen(dt.Rows(0)("Against_VCGL")) > 0 Then
                            Throw New Exception("Current Transaction is created by VCGL.So Can't perform this operation")
                        End If
                        ''
                        '' richa 26 Nov,2019 ERO/19/11/19-001120 (If AP is against Salary generation then system should not allow to reverse/unpost from AP screen itself)
                        If clsCommon.myLen(dt.Rows(0)("Against_Salary_Generation_Code")) > 0 Then
                            Throw New Exception("Current Transaction is created by Salary generation.So Can't perform this operation")
                        End If
                        '' richa 13 Aug,2021 (If AP is Bulk Milk Purchase Invoice then system should not allow to reverse/unpost from AP screen itself)
                        If clsCommon.myLen(dt.Rows(0)("Against_BulkMillkPurchaseInvoice_No")) > 0 Then
                            Throw New Exception("Current Transaction is created by Bulk Milk Purchase Invoice.So Can't perform this operation")
                        End If
                        Dim dt7 As DataTable = clsDBFuncationality.GetDataTable("select Invoice_Entry_Date,Loc_Code from TSPL_VENDOR_INVOICE_HEAD  where  Document_No ='" + txtDocNo.Value + "'", trans)
                        If dt7 IsNot Nothing AndAlso dt7.Rows.Count > 0 Then
                            clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePayable, clsUserMgtCode.mbtnAPInvoiceEntry, clsCommon.myCstr(dt7.Rows(0)("Loc_Code")), clsCommon.myCDate(dt7.Rows(0)("Invoice_Entry_Date")), trans)
                        End If
                        ''changes done by richa agarwal 29/03/2018
                        qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No='" + txtDocNo.Value + "'"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            'qry = "Current AP-Invoice is used in following Payment -"
                            qry = ""
                            For Each dr As DataRow In dt.Rows
                                If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Reverse_Code,'')  from TSPL_BANK_REVERSE WHERE DOCUMENT_NO = '" & clsCommon.myCstr(dr("Payment_No")) & "' AND isnull(TSPL_BANK_REVERSE.POST,'')='P'", trans))) <= 0 Then
                                    qry += Environment.NewLine + clsCommon.myCstr(dr("Payment_No"))
                                End If
                            Next
                            If clsCommon.myLen(qry) > 0 Then
                                Throw New Exception("Current AP-Invoice is used in following Payment -" & qry)
                            End If

                        End If

                        ''to check  document used into payment process 
                        qry = "select Doc_No from TSPL_PAYMENT_PROCESS_DEDUCTION where AP_Invoice_No in (select Vendor_Invoice_No from TSPL_VENDOR_INVOICE_HEAD where document_no='" & txtDocNo.Value & "')"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            qry = "Current document is used in following Payment Process -"
                            For Each dr As DataRow In dt.Rows
                                qry += Environment.NewLine + clsCommon.myCstr(dr("Doc_No"))
                            Next
                            Throw New Exception(qry)
                        End If


                        ''Delete AP Journal Entry 
                        ''changes done by richa agarwal TEC/02/11/18-000361(add 'GL-JE' TYPE IN SOURCE CODE CONDITION)
                        Dim VoucherNo As String = String.Empty
                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal Then
                            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in ('AP-DN','GL-JE') and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Then
                            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in ('AP-CN','GL-JE') and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "I") = CompairStringResult.Equal Then
                            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in ('AP-IN','GL-JE') and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                        End If
                        ' Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-IN' and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                        'qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No ='" + VoucherNo + "'"
                        'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        'qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No ='" + VoucherNo + "'"
                        'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        If clsCommon.myLen(VoucherNo) > 0 Then
                            qry = "update TSPL_JOURNAL_MASTER set TSPL_JOURNAL_MASTER.Authorized ='N',TSPL_JOURNAL_MASTER.Posting_Date =null where TSPL_JOURNAL_MASTER.Voucher_No  ='" + VoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                        End If

                        '' TO DELETE DATA FROM JOURNAL MASTER OPENING TABLE ''richa TEC/02/11/18-000361 24 Dec,2018
                        Dim VoucherNoOP As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where TSPL_JOURNAL_MASTER_OP.Source_Code in ('AP-IN','AP-DN','AP-CN','GL-JE') AND  TSPL_JOURNAL_MASTER_OP.Source_Doc_No='" + txtDocNo.Value + "'", trans)
                        If clsCommon.myLen(VoucherNoOP) > 0 Then
                            qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNoOP + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNoOP + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            '' to delete data from jounal entry main table in case of opening
                            If clsCommon.myLen(VoucherNo) > 0 Then
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If

                        ''delete Remitence(TDS) Entry '' Anubhooti 16-Dec-2014 (TDS Detail should not delete from table, it should update Remit_TDS field)
                        'qry = "Delete from TSPL_REMITTANCE where Document_No='" + txtDocNo.Value + "'"
                        'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "update TSPL_REMITTANCE set Remit_TDS=NULL where Document_No='" + clsCommon.myCstr(txtDocNo.Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        ''Change status to unpost

                        qry = "update TSPL_VENDOR_INVOICE_HEAD set Posting_Date=null  where Document_No='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        Xtra.UpdateAPInvoiceBalanceAmount(txtDocNo.Value, trans)

                        saveCancelLog(Reason, "Reverse And Recreate", trans)
                        clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, txtDocNo.Value, "TSPL_VENDOR_INVOICE_HEAD", "Document_No", "TSPL_VENDOR_INVOICE_DETAIL", "Document_No", "TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL", "AP_Invoice_No", "TSPL_AP_Invoice_Asset_EMI_Details", "AP_Invoice_No", "", "", "", "", "", "", trans)
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, txtDocNo.Value, "TSPL_REMITTANCE", "Document_No", "TSPL_AP_INVOICE_ADVANCE_INTEREST", "AP_Invoice_No", "TSPL_PROVISION_ENTRY_KNOCKOFF", "AP_Invoice_No", trans)
                        trans.Commit()
                        clsCommon.MyMessageBoxShow("Task done Successfully", Me.Text)
                        LoadData(txtDocNo.Value)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Public Sub GetChargesData()
        txtRefDocNo.Enabled = False
        repoChrCode.IsVisible = True
        repochrName.IsVisible = True
        repochrValue.IsVisible = True
        repoItmCode.IsVisible = True
        repoItmName.IsVisible = True
    End Sub

    Private Sub OpenChargeCategory(ByVal isButtonClick As Boolean)
        Dim qry As String
        Dim whrcls As String

        repoAcCode.ReadOnly = False

        If clsCommon.myLen(txtlocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please first select location", Me.Text)
            txtlocation.Focus()
            Return
        End If

        If clsCommon.myLen(TxtVendorNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please first select vendor", Me.Text)
            TxtVendorNo.Focus()
            Return
        End If

        Dim strsplit As String

        qry = "select a.* from (select TSPL_Charge_Category.Charge_Cat_Code as [Charge Cat Code],TSPL_Charge_Category.Description,TSPL_Charge_Category.GL_Code as [Account Code],TSPL_GL_ACCOUNTS.Description as [Account Desc],TSPL_ITEM_FRANCHISE_MAPPING.item_code as [Item Code],TSPL_ITEM_MASTER.item_desc as [Item Desc],TSPL_ITEM_FRANCHISE_MAPPING.Charges,TSPL_GL_ACCOUNTS.Account_Seg_Code7 as [Location Code],TSPL_LOCATION_MASTER.Location_Desc,(TSPL_Charge_Category.Charge_Cat_Code+'#$'+cast(TSPL_ITEM_FRANCHISE_MAPPING.charges as varchar)+'#$'+TSPL_ITEM_FRANCHISE_MAPPING.item_code) as MergeValue from TSPL_Charge_Category left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_Charge_Category.GL_Code and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtlocation.Value + "' right outer join TSPL_ITEM_FRANCHISE_MAPPING on TSPL_ITEM_FRANCHISE_MAPPING.Charge_Cat_Code=TSPL_Charge_Category.Charge_Cat_Code and TSPL_ITEM_FRANCHISE_MAPPING.vendor_code='" + TxtVendorNo.Value + "' left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ITEM_FRANCHISE_MAPPING.item_code)a"
        'gv1.CurrentRow.Cells(colchrcode).Value = clsCommon.ShowSelectForm("CHRIDFND", qry, "MergeValue", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colchrcode).Value), "MergeValue", isButtonClick)
        whrcls = " a.[charge cat code]<>''"
        strsplit = clsCommon.ShowSelectForm("CHRIDFND", qry, "MergeValue", whrcls, gv1.CurrentRow.Cells(colchrcode).Value, "MergeValue", isButtonClick)
        'ShowSelectForm("VendSelectfnd", Qry, "Code", "", TxtVendorNo.Value, "Code", isButtonClicked)

        If strsplit Is Nothing Then
            Return
        End If

        Dim xsplit() As String
        xsplit = strsplit.Split("#$")

        Try
            gv1.CurrentRow.Cells(colchrcode).Value = xsplit(0).Replace("#", "").Replace("$", "")
        Catch ex As Exception
        End Try

        Try
            gv1.CurrentRow.Cells(colchrValue).Value = xsplit(1).Replace("#", "").Replace("$", "")
        Catch ex As Exception
        End Try

        Try
            gv1.CurrentRow.Cells(colitemcode).Value = xsplit(2).Replace("#", "").Replace("$", "")
        Catch ex As Exception
        End Try

        Try
            gv1.CurrentRow.Cells(colitemname).Value = clsDBFuncationality.getSingleValue("select item_desc from TSPL_ITEM_MASTER where item_code='" + xsplit(2).Replace("#", "").Replace("$", "") + "'")
        Catch ex As Exception
        End Try

        Try
            gv1.CurrentRow.Cells(colchrName).Value = clsDBFuncationality.getSingleValue("select description from TSPL_Charge_Category where Charge_Cat_Code='" + xsplit(0).Replace("#", "").Replace("$", "") + "'")
        Catch ex As Exception
        End Try

        Try
            gv1.CurrentRow.Cells(colACCode).Value = clsDBFuncationality.getSingleValue("select gl_code from TSPL_Charge_Category where Charge_Cat_Code='" + xsplit(0).Replace("#", "").Replace("$", "") + "'")
            If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colGLType).Value = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value), Nothing)
            Else
                gv1.CurrentRow.Cells(colGLType).Value = ""
            End If
            repoAcCode.ReadOnly = True
        Catch ex As Exception
            repoAcCode.ReadOnly = False
        End Try

        Try
            gv1.CurrentRow.Cells(colACName).Value = clsDBFuncationality.getSingleValue("select description from TSPL_GL_ACCOUNTS where account_code='" + gv1.CurrentRow.Cells(colACCode).Value + "'")
        Catch ex As Exception
        End Try

        TxtVendorNo.Enabled = False
        txtlocation.Enabled = False

    End Sub

    Private Sub cmbRefType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRefType.TextChanged
        Try
            repoChrCode.IsVisible = False
            repochrName.IsVisible = False
            repochrValue.IsVisible = False
            repoItmCode.IsVisible = False
            repoItmName.IsVisible = False
            If cmbRefType.Text <> "" And cmbRefType.Text = "Charges" Then
                GetChargesData()
            End If
        Catch ex As Exception
            txtRefDocNo.Enabled = True
            repoChrCode.IsVisible = False
            repochrName.IsVisible = False
            repochrValue.IsVisible = False
            repoItmCode.IsVisible = False
            repoItmName.IsVisible = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value)

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

    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnSend.Enabled = True
        Else
            btnSend.Enabled = False
        End If

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(TxtVendorNo.Value)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Ticket No-TEC/01/08/19-000976 ,Sanjay
    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)

        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseInvoice)

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
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, TxtVendorNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            ''------------------------code for attchament-------------------------------------
            'Dim strRptPath As String = ""
            'obj.atchmnt = "N"
            'If obj.atchmnt = "Y" Then

            '    'Dim atchqry As String = GetAtchmentPrintQuery()
            '    'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            '    'If dt1.Rows.Count > 0 Then
            '    '    'SetItemWiseTax(dt1, txtDocNo.Value)
            '    '    strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptAPInvc", "AR Invoice")
            '    'End If
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

            'sanjay
            Dim strPhoneno As String = Nothing
            Dim dtContent As DataTable = Nothing
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmProcurementDeduction + "'", Nothing)
            Else
                dtContent = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnAPInvoiceEntry + "'", Nothing)
            End If

            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, TxtVendorNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)

                End If


                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, TxtVendorNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, lblVendorName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)
                End If

            End If


            For Each strUser As String In lstUsers
                Dim lstReceiptents As New List(Of String)
                Dim qry As String = ""
                Dim emailId As String = ""
                If isSendForApproval Then
                    qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                    emailId = clsDBFuncationality.getSingleValue(qry)
                    strPhoneno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(mob_no,'') from TSPL_USER_MASTER where user_code ='" & strUser & "' "))
                Else
                    emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_vendor_MASTER where vendor_code ='" & strUser & "' ")
                    strPhoneno = clsDBFuncationality.getSingleValue("select isnull(Phone1,'') from TSPL_vendor_MASTER where vendor_code ='" & strUser & "' ")
                End If

                lstReceiptents.Add(emailId)
                objSMSH.arrMobilNo.Add(clsCommon.myCstr(strPhoneno))
                objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))
            Next

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                    objEmailH.SaveData(clsUserMgtCode.frmProcurementDeduction, objEmailH, Nothing)
                Else
                    objEmailH.SaveData(clsUserMgtCode.mbtnAPInvoiceEntry, objEmailH, Nothing)
                End If

                objEmailH = Nothing
            End If
            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                    objSMSH.SaveData(clsUserMgtCode.frmProcurementDeduction, objSMSH, Nothing)
                Else
                    objSMSH.SaveData(clsUserMgtCode.mbtnAPInvoiceEntry, objSMSH, Nothing)
                End If


                objSMSH = Nothing
            End If
            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Or clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                clsCommon.MyMessageBoxShow("E-Mail/SMS Send Successfully", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        'Try
        '    Dim client As New System.Net.WebClient()

        '    If clsCommon.myLen(obj.smsbody) <= 0 Then
        '        Return
        '    End If

        '    'strMes = "Dear  " & strCustomer & "  your order No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "has been booked with amount" & lblTotRAmt.Text

        '    strMes = obj.smsbody
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
        '        strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
        '    End If
        '    If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
        '        strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
        '    End If


        '    strphone = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

        '    Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '    Dim data As Stream = client.OpenRead(baseurl)
        '    Dim reader As StreamReader = New StreamReader(data)
        '    Dim s As String = reader.ReadToEnd()
        '    data.Close()
        '    reader.Close()

        '    clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "D") = CompairStringResult.Equal Then
            LoadRefDocTypeForDC()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "C") = CompairStringResult.Equal Then
            LoadRefDocTypeForDC()
        Else
            LoadRefDocumenType()
        End If
        'If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "I") = CompairStringResult.Equal Then
        '    grpProvision.Enabled = True
        'Else
        '    grpProvision.Enabled = False
        '    txtProvAmt.Text = ""
        '    arrProvDocNo = Nothing
        '    dtpFromProv.Value = clsCommon.GETSERVERDATE()
        '    dtpToProv.Value = clsCommon.GETSERVERDATE()
        '    chkProvision.Checked = False
        'End If
        setProvisionVisibility()
        setDeductionVisibility()
        gv1.Rows.AddNew()
    End Sub

    Private Sub chkProvision_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProvision.CheckStateChanged
        'If chkProvision.Checked Then
        '    dtpFromProv.Enabled = True
        '    dtpToProv.Enabled = True
        '    btnProvSelect.Enabled = True
        '    btlShowProvision.Enabled = True
        '    txtProvAmt.Enabled = True
        'Else

        '    dtpFromProv.Enabled = False
        '    dtpToProv.Enabled = False
        '    btnProvSelect.Enabled = False
        '    btlShowProvision.Enabled = False
        '    txtProvAmt.Enabled = False
        'End If
        setProvisionVisibility()
    End Sub

    Sub selectProvision()

        If dtpFromProv.Value > dtpToProv.Value Then
            clsCommon.MyMessageBoxShow(" 'Provision From Date' can't be greator than 'Provision To Date'", Me.Text)
            dtpFromProv.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(" Please Select Location", Me.Text)
            txtlocation.Focus()
            Exit Sub
        End If

        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(" Please Select Vendor", Me.Text)
            TxtVendorNo.Focus()
            Exit Sub
        End If

        Dim frm As FrmSelectProv = New FrmSelectProv()
        clsLocation.GetLocationSegments()
        If clsCommon.myLen(FreightProvisionAccount) > 0 Then
            frm.qry = clsProvisionEntryAdvanceKnockOff.GetBalanceQry(txtDocNo.Value, TxtVendorNo.Value, dtpFromProv.Value, dtpToProv.Value, Nothing, Nothing, txtlocation.Value, CalculateProvisionOnGateePass)
            'frm.qry = "select max( Prog_Code) as Prog_Code,max(Doc_No) as Doc_No,max(Doc_Date) as Doc_Date,max(Loc_Code) as Loc_Code,max(Loc_Desc) as Loc_Desc,max(Vendor_Code) as Vendor_Code,max(Vendor_Desc) as Vendor_Desc,max(Vendor_Type) as Vendor_Type,max(Route_Code) as Route_Code,max(Ref_Doc_No) as Ref_Doc_No,max(Prov_type) as Prov_type" + Environment.NewLine + _
            '",max(null) as Status,max(null) as Prov_Month,max(null) as Prov_Year,max(null) as Comp_Code,max(null) as Created_by,max(null) as  Created_Date,max(null) as Modified_By,max(null) as Modified_Date,max(null) as isPosted,max(null) Posting_Date" + Environment.NewLine + _
            '",sum( Amount * RI) as Amount from (" + Environment.NewLine + _
            '"select Prog_Code,Doc_No,Doc_Date,Loc_Code,Loc_Desc,Vendor_Code,Vendor_Desc,Vendor_Type,Route_Code,Ref_Doc_No,Prov_type,Amount ,1 as RI,1 as chk" + Environment.NewLine + _
            '",Status,Prov_Month,Prov_Year,Comp_Code,Created_by, Created_Date,Modified_By,Modified_Date,isPosted,Posting_Date" + Environment.NewLine + _
            '"from tspl_provision_entry " + Environment.NewLine + _
            '"where Vendor_Code='" + TxtVendorNo.Value + "' and Status='No' and loc_Code in (select location_code from TSPL_LOCATION_MASTER   where Loc_Segment_Code='" + txtlocation.Value + "')  and isPosted='1'  and Doc_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromProv.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Doc_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToProv.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine + _
            '"union all" + Environment.NewLine + _
            '"select null as Prog_Code,Provision_No as Doc_No,null as Doc_Date,null as Loc_Code,null as Loc_Desc,null as Vendor_Code, null as Vendor_Desc,null as Vendor_Type,null as Route_Code,null as Ref_Doc_No,null as Prov_type,Knockoff_Amount as Amount ,-1 as RI ,0 as chk" + Environment.NewLine + _
            '",null as Status,null as Prov_Month,null as Prov_Year,null as Comp_Code,null as Created_by,null as  Created_Date,null as Modified_By,null as Modified_Date,null as isPosted,null Posting_Date" + Environment.NewLine + _
            '"from TSPL_PROVISION_ENTRY_KNOCKOFF where AP_Invoice_No not in ('" + txtDocNo.Value + "')" + Environment.NewLine + _
            '")xx group by Doc_No having sum(chk)>0 and sum(Amount)>0"
        Else
            frm.qry = "Select * from ("
            frm.qry = frm.qry & " select Doc_No,Doc_Date,Vendor_Code,Vendor_Desc,Vendor_Type,Status,Ref_Doc_No,Prov_type,Amount,Prog_Code,Prov_Month,Prov_Year,Comp_Code,Created_by," _
                & " Created_Date,Modified_By,Modified_Date,isPosted,Posting_Date,Loc_Code,Loc_Desc,Status_Update_Doc_No,Route_Code from tspl_provision_entry" _
                & " where Vendor_Code='" & TxtVendorNo.Value & "' and Status='No' and loc_Code in ( select location_code from TSPL_LOCATION_MASTER  " _
                & " where Loc_Segment_Code='" & txtlocation.Value & "')  and isPosted='1'  and Doc_Date between '" & clsCommon.GetPrintDate(dtpFromProv.Value, "dd/MMM/yyyy") & "'" _
                & " and '" & clsCommon.GetPrintDate(dtpToProv.Value, "dd/MMM/yyyy") & "' and Vendor_Type not like '%Product Sale%'"
            frm.qry &= " union all "
            frm.qry &= "select Doc_No,Doc_Date,Vendor_Code,Vendor_Desc,Vendor_Type,tspl_provision_entry.Status,coalesce(TSPL_SD_SHIPMENT_HEAD.sale_Invoice_No,Ref_Doc_No) as Ref_Doc_No,Prov_type,Amount,Prog_Code,Prov_Month,Prov_Year,tspl_provision_entry.Comp_Code,tspl_provision_entry.Created_by," _
               & " tspl_provision_entry.Created_Date,tspl_provision_entry.Modified_By,tspl_provision_entry.Modified_Date,tspl_provision_entry.isPosted,tspl_provision_entry.Posting_Date,Loc_Code,Loc_Desc,Status_Update_Doc_No,Route_Code from tspl_provision_entry left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=Ref_Doc_No" _
               & " where Vendor_Code='" & TxtVendorNo.Value & "' and tspl_provision_entry.Status='No' and loc_Code in ( select location_code from TSPL_LOCATION_MASTER  " _
               & " where Loc_Segment_Code='" & txtlocation.Value & "')  and tspl_provision_entry.isPosted='1'  and Doc_Date between '" & clsCommon.GetPrintDate(dtpFromProv.Value, "dd/MMM/yyyy") & "'" _
               & " and '" & clsCommon.GetPrintDate(dtpToProv.Value, "dd/MMM/yyyy") & "' and Vendor_Type like '%Product Sale%'"
            frm.qry &= ") as Prov "
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                frm.qry = frm.qry & " union all  select Doc_No,Doc_Date,Vendor_Code,Vendor_Desc,Vendor_Type,Status,Ref_Doc_No,Prov_type,Amount,Prog_Code,Prov_Month," _
                    & " Prov_Year,Comp_Code,Created_by,Created_Date,Modified_By,Modified_Date,isPosted,Posting_Date,Loc_Code,Loc_Desc,Status_Update_Doc_No,Route_Code " _
                    & " from tspl_provision_entry  where Status_Update_Doc_No='" & txtDocNo.Value & "' "
            End If
        End If

        frm.ShowDialog()

        If frm.btnOkClicked Then
            txtProvAmt.Text = clsCommon.myFormat(frm.ProvAmount)
            arrProvDocNo = frm.arrProvDocNo
            arrInvoiceNo = frm.arrInvoiceNo
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(colAmt)
            gv1.Rows(0).Cells(colAmt).Value = clsCommon.myCdbl(txtProvAmt.Text)
        End If
    End Sub

    Sub selectProvisionForCurrentDocment()

        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select a Document", Me.Text)
            Exit Sub
        End If

        Dim frm As FrmSelectProv = New FrmSelectProv()
        clsLocation.GetLocationSegments()

        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            If CalculateProvisionOnGateePass = 0 Then
                frm.qry = "select Prog_Code,Doc_No,Doc_Date,Loc_Code,Loc_Desc,Vendor_Code,Vendor_Desc,Vendor_Type,Route_Code,Ref_Doc_No,Prov_type,TSPL_PROVISION_ENTRY_KNOCKOFF.Balance_Amount as Amount" +
           " ,Status,Prov_Month,Prov_Year,Comp_Code,Created_by, Created_Date,Modified_By,Modified_Date,isPosted,Posting_Date" +
           " from TSPL_PROVISION_ENTRY_KNOCKOFF" +
           " inner join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Doc_No=TSPL_PROVISION_ENTRY_KNOCKOFF.Provision_No" +
           " where TSPL_PROVISION_ENTRY_KNOCKOFF.AP_Invoice_No='" + txtDocNo.Value + "'"
            Else
                frm.qry = "select Prog_Code,Doc_No,Doc_Date,Loc_Code,Loc_Desc,Vendor_Code,Vendor_Desc,Vendor_Type,Route_Code,TSPL_PROVISION_ENTRY_KNOCKOFF.Invoice_No as Ref_Doc_No,Prov_type,TSPL_PROVISION_ENTRY_KNOCKOFF.Balance_Amount as Amount" +
           " ,Status,Prov_Month,Prov_Year,Comp_Code,Created_by, Created_Date,Modified_By,Modified_Date,isPosted,Posting_Date" +
           " from TSPL_PROVISION_ENTRY_KNOCKOFF" +
           " inner join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Doc_No=TSPL_PROVISION_ENTRY_KNOCKOFF.Provision_No" +
           " where TSPL_PROVISION_ENTRY_KNOCKOFF.AP_Invoice_No='" + txtDocNo.Value + "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(frm.qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                frm.qry = "  select * from tspl_provision_entry  where Status_Update_Doc_No='" & txtDocNo.Value & "' "
            End If
            dt = Nothing
        End If
        frm.ShowDialog()
    End Sub

    Private Sub btnProvSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvSelect.Click
        selectProvision()
    End Sub

    Private Sub btnProvSelect_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btlShowProvision.Click
        selectProvisionForCurrentDocment()
    End Sub

    Private Sub chkDeduction_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeduction.CheckStateChanged
        setDeductionVisibility()
        gv1.Rows.AddNew()
    End Sub

    Private Sub chkLandingCost_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkProRated.ToggleStateChanged
        Try
            SetIsProRated(chkProRated.Checked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetIsProRated(ByVal IsProRated As Boolean)
        Try
            'cmbRefType.Enabled = Not IsProRated
            If IsProRated Then
                cmbRefType.SelectedValue = "S"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub TxtCashDiscount_TextChanged(sender As Object, e As EventArgs) Handles TxtCashDiscount.TextChanged
        If (clsCommon.myCdbl(lblTotRAmt.Text)) > 0 Then
            txtAmtAfterCashDiscount.Text = clsCommon.myCdbl(lblTotRAmt.Text) - clsCommon.myCdbl(TxtCashDiscount.Text)
        End If
    End Sub

    Private Sub RadMenuItem10_Click(sender As Object, e As EventArgs) Handles RadMenuItem10.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim Counter As String = ""
            Dim IsLevel As Boolean = False
            If transportSql.LoadDocument(gv, "", Nothing) Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                Try
                    Dim obj As clsVedorInvoiceHead
                    Dim dtTemp As DataTable
                    For ii As Integer = 0 To gv.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / gv.RowCount, "")
                        Counter = clsCommon.myCstr(ii + 1)
                        Dim strVendor As String = clsCommon.myCstr(gv.Rows(ii).Cells(0).Value)
                        Dim strGLAccount As String = clsCommon.myCstr(gv.Rows(ii).Cells(1).Value)
                        For jj As Integer = 2 To gv.Columns.Count - 1
                            Dim amt As Decimal = clsCommon.myCdbl(gv.Rows(ii).Cells(jj).Value)
                            If amt <= 0 Then
                                Continue For
                            End If

                            Dim TDate As DateTime = clsCommon.myCDate(gv.Columns(jj).HeaderText)
                            TDate = New Date(TDate.Year, TDate.Month, 10)

                            obj = New clsVedorInvoiceHead()
                            obj.Vendor_Code = strVendor
                            If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                                obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & obj.Vendor_Code & "'", trans))
                                If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                                    Throw New Exception("Vendor does not exist.")
                                End If
                            Else
                                Throw New Exception("Please enter Vendor Code")
                            End If
                            Dim objVendor As clsVendorMaster = clsVendorMaster.GetData(obj.Vendor_Code, trans)
                            obj.Account_Set = objVendor.Vendor_Account
                            obj.Vendor_Name = objVendor.Vendor_Name
                            obj.Terms_Code = objVendor.Terms_Code
                            If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code, trans) = True Then
                                obj.CURRENCY_CODE = objVendor.CURRENCY_CODE
                            End If
                            If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                                dtTemp = clsModuleCurrencyMapping.GetLatestCurConvRateDT(TDate, txtCurrencyCode.Value)
                                If dtTemp.Rows.Count > 0 Then
                                    obj.ConvRate = clsCommon.myCdbl(dtTemp.Rows(0).Item("Rate"))
                                Else
                                    obj.ConvRate = 1
                                End If
                            End If
                            obj.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + strGLAccount + "'", trans))

                            If clsCommon.myLen(obj.loc_code) > 0 Then
                                obj.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code WHERE TSPL_GL_SEGMENT_CODE.Segment_code='" + obj.loc_code + "' AND Seg_No = '7' AND GIT='N'", trans))
                                If clsCommon.myLen(obj.loc_code) <= 0 Then
                                    Throw New Exception("Location Segment does not exist.")
                                End If
                            Else
                                Throw New Exception("Please enter Location Segment Code")
                            End If

                            obj.Invoice_Entry_Date = TDate
                            obj.Vendor_Invoice_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Vendor_Invoice_No) as Vendor_Invoice_No  from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No like 'AUTOH%'", trans))
                            If clsCommon.myLen(obj.Vendor_Invoice_No) <= 0 Then
                                obj.Vendor_Invoice_No = "AUTOH000001"
                            Else
                                obj.Vendor_Invoice_No = clsCommon.incval(obj.Vendor_Invoice_No)
                            End If

                            obj.Vendor_Invoice_Date = TDate
                            obj.Due_Date = obj.Invoice_Entry_Date
                            obj.Terms_Code = objVendor.Terms_Code
                            obj.Document_Type = "I"
                            obj.On_Hold = False
                            obj.Discount_Base = amt
                            If obj.Discount_Base <= 0 Then
                                Throw New Exception("Please enter amount.")
                            End If

                            dtTemp = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code WHERE TSPL_VENDOR_MASTER.Vendor_Code='" + obj.Vendor_Code + "'", trans)
                            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                obj.Vendor_Control_AC = clsCommon.myCstr(dtTemp.Rows(0)("Payable_Account"))
                                obj.Discount_GL_AC = clsCommon.myCstr(dtTemp.Rows(0)("Discount_Account"))
                            End If
                            If obj.Discount_Percentage > 0 Then
                                obj.Discount_Amount = (obj.Discount_Base * obj.Discount_Percentage) / 100
                            Else
                                obj.Discount_Amount = 0
                            End If
                            obj.Invoice_Type = "AP"
                            obj.Amount_Less_Discount = obj.Discount_Base - obj.Discount_Amount
                            obj.Document_Total = obj.Amount_Less_Discount
                            obj.Balance_Amt = obj.Amount_Less_Discount
                            obj.Total_Landed_Amt = 0
                            obj.is_For_Provision = 0
                            obj.isDeduction = 0
                            obj.Security = 0
                            obj.PO_Number = ""
                            obj.Description = ""
                            obj.Tax_Group = "Exempted"
                            obj.PROJECT_ID = ""

                            obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                            obj.Total_Add_Charge = 0
                            obj.Empty_Amount = 0
                            obj.Is_ProRated = "N"

                            '-------------------------Detail Level Starts From Here----------------
                            obj.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim objTr As New clsVedorInvoiceDetail()
                            objTr.Detail_Line_No = 1
                            objTr.GL_Account_Code = strGLAccount
                            If clsCommon.myLen(objTr.GL_Account_Code) > 0 Then
                                dtTemp = clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & objTr.GL_Account_Code & "'", trans)
                                If dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Account does not exist.")
                                Else
                                    objTr.GL_Account_Code = clsCommon.myCstr(dtTemp.Rows(0)("Account_Code"))
                                    objTr.GL_Account_Desc = clsCommon.myCstr(dtTemp.Rows(0)("Account_Desc"))
                                End If
                            Else
                                Throw New Exception("Please enter Account code")
                            End If

                            objTr.Amount = amt
                            objTr.Discount_Per = obj.Discount_Percentage
                            objTr.Discount = obj.Discount_Amount
                            objTr.Amount_less_Discount = objTr.Amount - obj.Discount_Amount
                            objTr.Landed_Amount = 0
                            objTr.Total_Tax = 0
                            objTr.Total_Amount = obj.Amount_Less_Discount
                            objTr.AddChargeCode = ""
                            objTr.AddChargeDesc = ""
                            objTr.is_Unclaimed_Tax = False
                            objTr.Remarks = ""
                            objTr.Invoice_Type = ""
                            objTr.Invoice_No = ""

                            If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                                obj.Arr.Add(objTr)
                            End If
                            '---------------------------------Detail Level Ends Here-----------------------------
                            obj.SaveData(obj, True, trans)
                        Next
                    Next
                    clsCommon.ProgressBarPercentHide()
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow("Error at Row No " + clsCommon.myCstr(Counter) + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                Finally
                    clsCommon.ProgressBarPercentHide()
                    Me.Controls.Remove(gv)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkSecTrans_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSecTrans.ToggleStateChanged
        SetSeondaryTranporterPaymentCyle()
    End Sub

    Sub SetSeondaryTranporterPaymentCyle()
        pnlSecondaryTranporterPaymentCycle.Visible = chkSecTrans.Checked
    End Sub

    Private Sub chkITCEligible_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkITCEligible.ToggleStateChanged
        Try
            If chkITCEligible.Checked = True Then
                RadGroupBox4.Enabled = True
            Else
                RadGroupBox4.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CboxITCType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CboxITCType.SelectedIndexChanged
        Try
            LoadITC_Elibible()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub loadEmployeeType()
        Dim dt As DataTable = Nothing
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        dt.Rows.Add("Select", "")
        dt.Rows.Add("TA-DA", "TD")
        dt.Rows.Add("Salary", "S")
        dt.Rows.Add("Travelling", "T")
        dt.Rows.Add("Imprest", "I")
        ddlEmployeeType.DataSource = dt
        ddlEmployeeType.DisplayMember = "Code"
        ddlEmployeeType.ValueMember = "Value"

    End Sub

    Private Sub rmiExportDrNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExportDrNote.Click
        BlankExportDRCR()
    End Sub

    Private Sub rmiExportCrNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCreditNoteExport.Click
        BlankExportDRCR()
    End Sub

    Sub BlankExportDRCR()
        Try
            strQry = "Select '' as [Location Code], '' as [Vendor], '' as [Document Date], '' as [Deduction (Yes/No)], '' as [Security Deduction (Yes/No)], '' as [Deduction Code], '' as [Account Code], 0 as [Amount], 0 as [Discount %], '' as [Remarks],'' as [Employee Expense Type]"
            If SettingCostCenter Then
                strQry += ",'' AS [Hirerachy Level]"
                If SettingCostCenterlevel Then
                    strQry += ",'' AS [" + gv1.Columns(colHierarchyLevel4).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel3).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel2).HeaderText + "],'' AS [" + gv1.Columns(colHierarchyLevel1).HeaderText + "] "
                Else
                    strQry += ",'' AS [Cost Centre]"
                End If
            ElseIf SettShowMCCFinder Then
                strQry += ",'' AS [MCC Code],'' as [VLC Uploader Code]"
            End If
            transportSql.ExporttoExcel(strQry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiImportDrNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportDrNote.Click
        ImportDRCR("D")
    End Sub

    Private Sub rmiImportCrNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportCreditNote.Click
        ImportDRCR("C")
    End Sub

    Sub ImportDRCR(ByVal TransType As String)
        If clsCommon.MyMessageBoxShow("You are going to import " & IIf(clsCommon.myCstr(TransType) = "I", "Invoice", IIf(clsCommon.myCstr(TransType) = "D", "Debit Note", "Credit Note")) & Environment.NewLine & "want to continue ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Counter As String = ""
        Dim IsLevel As Boolean = False
        Dim flag As Boolean = False
        If SettingCostCenter Then
            If SettingCostCenterlevel Then
                flag = transportSql.importExcel(gv, "Location Code", "Vendor", "Document Date", "Deduction (Yes/No)", "Security Deduction (Yes/No)", "Deduction Code", "Account Code", "Amount", "Discount %", "Remarks", "Employee Expense Type", "Hirerachy Level", gv1.Columns(colHierarchyLevel4).HeaderText, gv1.Columns(colHierarchyLevel3).HeaderText, gv1.Columns(colHierarchyLevel2).HeaderText, gv1.Columns(colHierarchyLevel1).HeaderText)
            Else
                flag = transportSql.importExcel(gv, "Location Code", "Vendor", "Document Date", "Deduction (Yes/No)", "Security Deduction (Yes/No)", "Deduction Code", "Account Code", "Amount", "Discount %", "Remarks", "Employee Expense Type", "Hirerachy Level", "Cost Centre")
            End If
        Else
            If SettShowMCCFinder = True Then
                flag = transportSql.importExcel(gv, "Location Code", "Vendor", "Document Date", "Deduction (Yes/No)", "Security Deduction (Yes/No)", "Deduction Code", "Account Code", "Amount", "Discount %", "Remarks", "Employee Expense Type", "MCC Code", "VLC Uploader Code")
            Else
                flag = transportSql.importExcel(gv, "Location Code", "Vendor", "Document Date", "Deduction (Yes/No)", "Security Deduction (Yes/No)", "Deduction Code", "Account Code", "Amount", "Discount %", "Remarks", "Employee Expense Type")
            End If
        End If
        If flag Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarShow()
            Try
                Dim obj As clsVedorInvoiceHead
                Dim dtTemp As DataTable
                For Each grow As GridViewRowInfo In gv.Rows
                    Counter = clsCommon.myCstr(grow.Index + 2) + " :"
                    obj = New clsVedorInvoiceHead()
                    Dim objVendor As clsVendorMaster = Nothing
                    If SettShowMCCFinder = False Then
                        obj.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor").Value)
                        If clsCommon.myLen(obj.Vendor_Code) > 0 Then

                            If (clsCommon.CompairString(clsUserMgtCode.frmProcurementDeduction, FORMTYPE) = CompairStringResult.Equal) AndAlso (UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True) Then
                                obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VSP_Code from TSPL_VLC_MASTER_HEAD where VLC_code_vlc_Uploader='" & obj.Vendor_Code & "'", trans))
                            Else
                                obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & obj.Vendor_Code & "'", trans))
                            End If

                            If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                                Throw New Exception("Vendor does not exist.")
                            End If
                        Else
                            Throw New Exception("Please enter Vendor Code")
                        End If
                        objVendor = clsVendorMaster.GetData(obj.Vendor_Code, trans)
                        obj.Account_Set = objVendor.Vendor_Account
                        obj.Vendor_Name = objVendor.Vendor_Name
                        obj.Terms_Code = objVendor.Terms_Code
                        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code, trans) = True Then
                            obj.CURRENCY_CODE = objVendor.CURRENCY_CODE
                        End If
                        If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                            dtTemp = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.txtDate.Value, txtCurrencyCode.Value)
                            If dtTemp.Rows.Count > 0 Then
                                obj.ConvRate = clsCommon.myCdbl(dtTemp.Rows(0).Item("Rate"))
                            Else
                                obj.ConvRate = 1
                            End If
                        End If
                    End If
                    obj.loc_code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If clsCommon.myLen(obj.loc_code) > 0 Then
                        obj.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code WHERE TSPL_GL_SEGMENT_CODE.Segment_code='" + obj.loc_code + "' AND Seg_No = '7' AND GIT='N'", trans))
                        If clsCommon.myLen(obj.loc_code) <= 0 Then
                            Throw New Exception("Location Segment does not exist.")
                        End If
                    Else
                        Throw New Exception("Please enter Location Segment Code")
                    End If

                    obj.Invoice_Entry_Date = clsCommon.myCstr(grow.Cells("Document Date").Value)
                    obj.Vendor_Invoice_No = "" '"Opening Balance " + clsCommon.myCstr(Counter)
                    obj.Vendor_Invoice_Date = obj.Invoice_Entry_Date
                    obj.Due_Date = obj.Invoice_Entry_Date
                    If SettShowMCCFinder = False Then
                        obj.Terms_Code = objVendor.Terms_Code
                    End If
                    obj.Document_Type = clsCommon.myCstr(TransType)
                    obj.On_Hold = False
                    obj.Discount_Base = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    If obj.Discount_Base <= 0 Then
                        Throw New Exception("Please enter amount.")
                    End If
                    obj.Discount_Percentage = clsCommon.myCdbl(grow.Cells("Discount %").Value)
                    If SettShowMCCFinder = False Then
                        dtTemp = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code WHERE TSPL_VENDOR_MASTER.Vendor_Code='" + obj.Vendor_Code + "'", trans)
                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            obj.Vendor_Control_AC = clsCommon.myCstr(dtTemp.Rows(0)("Payable_Account"))
                            If clsCommon.myCdbl(grow.Cells("Discount %").Value) > 0 Then
                                obj.Discount_GL_AC = clsCommon.myCstr(dtTemp.Rows(0)("Discount_Account"))
                            End If

                        End If
                    End If
                    If obj.Discount_Percentage > 0 Then
                        obj.Discount_Amount = (obj.Discount_Base * obj.Discount_Percentage) / 100
                    Else
                        obj.Discount_Amount = 0
                    End If
                    obj.Invoice_Type = "AP"
                    obj.Amount_Less_Discount = obj.Discount_Base - obj.Discount_Amount
                    obj.Document_Total = obj.Amount_Less_Discount
                    obj.Balance_Amt = obj.Amount_Less_Discount
                    obj.Total_Landed_Amt = 0
                    obj.is_For_Provision = 0
                    obj.isDeduction = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Deduction (Yes/No)").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                    If obj.isDeduction = 1 Then
                        obj.Security = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Security Deduction (Yes/No)").Value), "Yes") = CompairStringResult.Equal, 1, 0)
                    Else
                        obj.Security = 0
                    End If

                    obj.PO_Number = ""
                    obj.Description = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj.Tax_Group = "Exempted"
                    obj.PROJECT_ID = ""

                    If SettShowMCCFinder = True Then
                        obj.Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor").Value)
                        obj.MCC_Code = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        Dim strvlcUploaderCode As String = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)
                        If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(FORM_TYPE,'') AS FORM_TYPE  FROM TSPL_VENDOR_MASTER WHERE vENDOR_CODE ='" & obj.Vendor_Code & "'", trans)), "VSP") = CompairStringResult.Equal Then
                                Dim strMCCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT MCC FROM TSPL_VLC_MASTER_HEAD WHERE VSP_CODE='" & obj.Vendor_Code & "'", trans))
                                If clsCommon.CompairString(strMCCCode, obj.MCC_Code) <> CompairStringResult.Equal Then
                                    Throw New Exception("MCC Code not mapped with VLC master with VSP.")
                                Else
                                    obj.MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  WHERE TSPL_Location_MASTER.Location_Code='" + obj.MCC_Code + "'  and Location_Category='MCC' ", trans))
                                    If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                                        Throw New Exception("MCC Code does not exist.")
                                    End If
                                    obj.MCC_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + obj.MCC_Code + "'", trans))
                                End If

                            Else
                                If clsCommon.myLen(obj.MCC_Code) > 0 Then
                                    obj.MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  WHERE TSPL_Location_MASTER.Location_Code='" + obj.MCC_Code + "' AND  Rejected_Type='N' and Location_Category='MCC' ", trans))
                                    If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                                        Throw New Exception("MCC Code does not exist.")
                                    End If
                                    obj.MCC_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + obj.MCC_Code + "'", trans))
                                End If
                            End If
                        ElseIf clsCommon.myLen(strvlcUploaderCode) > 0 Then
                            Dim strVendor As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT VSP_Code  FROM TSPL_VLC_MASTER_HEAD WHERE MCC ='" & obj.MCC_Code & "' and VLC_Code_VLC_Uploader ='" & strvlcUploaderCode & "'", trans))
                            If clsCommon.myLen(strVendor) > 0 Then
                                obj.Vendor_Code = strVendor
                                obj.MCC_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + obj.MCC_Code + "'", trans))
                            Else
                                Throw New Exception("VSP not found for VLC and MCC ")
                            End If
                        Else
                            Throw New Exception("Please enter VLC Uploader Code/Vendor.")
                        End If


                        If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                            obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & obj.Vendor_Code & "'", trans))
                            If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                                Throw New Exception("Vendor does not exist.")
                            End If
                        Else
                            Throw New Exception("Please enter Vendor Code")
                        End If
                        objVendor = clsVendorMaster.GetData(obj.Vendor_Code, trans)
                        obj.Account_Set = objVendor.Vendor_Account
                        obj.Vendor_Name = objVendor.Vendor_Name
                        obj.Terms_Code = objVendor.Terms_Code
                        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code, trans) = True Then
                            obj.CURRENCY_CODE = objVendor.CURRENCY_CODE
                        End If
                        If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                            dtTemp = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.txtDate.Value, txtCurrencyCode.Value)
                            If dtTemp.Rows.Count > 0 Then
                                obj.ConvRate = clsCommon.myCdbl(dtTemp.Rows(0).Item("Rate"))
                            Else
                                obj.ConvRate = 1
                            End If
                        End If
                        dtTemp = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Account=TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code WHERE TSPL_VENDOR_MASTER.Vendor_Code='" + obj.Vendor_Code + "'", trans)
                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            obj.Vendor_Control_AC = clsCommon.myCstr(dtTemp.Rows(0)("Payable_Account"))
                            obj.Discount_GL_AC = clsCommon.myCstr(dtTemp.Rows(0)("Discount_Account"))
                        End If
                    End If

                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic

                    obj.Total_Add_Charge = 0
                    obj.Empty_Amount = 0
                    obj.Is_ProRated = "N"
                    ''richa agarwal 29 Jan,2021
                    If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Form_ID, clsUserMgtCode.frmProcurementDeduction) = CompairStringResult.Equal Then
                            obj.ISProcurementDeduction = 1
                        End If
                    End If
                    '-------------------------Detail Level Starts From Here----------------
                    obj.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim objTr As New clsVedorInvoiceDetail()
                    objTr.Detail_Line_No = 1
                    If obj.isDeduction = 1 And (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
                        objTr.DeductionCode = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
                        If clsCommon.myLen(objTr.DeductionCode) > 0 Then
                            dtTemp = clsDBFuncationality.GetDataTable("select TSPL_DEDUCTION_MASTER.Code as Deduction_Code, TSPL_DEDUCTION_MASTER.Description as Deduction_Desc, TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_DEDUCTION_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code WHERE TSPL_DEDUCTION_MASTER.Code='" & objTr.DeductionCode & "' AND TSPL_DEDUCTION_MASTER.Security=" & obj.Security & "", trans)
                            If dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Deduction does not exist.")
                            Else
                                objTr.DeductionCode = clsCommon.myCstr(dtTemp.Rows(0)("Deduction_Code"))
                                objTr.DeductionDesc = clsCommon.myCstr(dtTemp.Rows(0)("Deduction_Desc"))
                                objTr.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dtTemp.Rows(0)("Account_Code")), obj.loc_code, True, trans)
                                objTr.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(objTr.GL_Account_Code) + "'", trans))
                            End If
                        Else
                            Throw New Exception("Please enter Deduction code")
                        End If
                    Else
                        objTr.GL_Account_Code = clsCommon.myCstr(grow.Cells("Account Code").Value)
                        If clsCommon.myLen(objTr.GL_Account_Code) > 0 Then
                            dtTemp = clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & objTr.GL_Account_Code & "'", trans)
                            If dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Account does not exist.")
                            Else
                                objTr.GL_Account_Code = clsCommon.myCstr(dtTemp.Rows(0)("Account_Code"))
                                objTr.GL_Account_Desc = clsCommon.myCstr(dtTemp.Rows(0)("Account_Desc"))
                            End If
                            If clsCommon.GetDateWithStartTime(clsCommon.myCDate(obj.Invoice_Entry_Date)) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                                'richa 17 SEp,2019 TEC/03/07/19-000927
                                dtTemp = clsDBFuncationality.GetDataTable("select TSPL_GL_ACCOUNTS.Account_Code, TSPL_GL_ACCOUNTS.Description as Account_Desc from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & objTr.GL_Account_Code & "' and ( ControlAccount='N' or TSPL_GL_ACCOUNTS.Account_Code IN (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForAP =1)) ", trans)
                                If dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Account should be type of non Control Account.")
                                Else
                                    objTr.GL_Account_Code = clsCommon.myCstr(dtTemp.Rows(0)("Account_Code"))
                                    objTr.GL_Account_Desc = clsCommon.myCstr(dtTemp.Rows(0)("Account_Desc"))
                                End If
                            End If
                        Else
                            Throw New Exception("Please enter Account code")
                        End If
                    End If
                    Dim intAcctSeg As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" & objTr.GL_Account_Code & "' and Account_Seg_Code7='" & obj.loc_code & "'", trans))
                    If intAcctSeg = 0 Then
                        Throw New Exception("Account No " & objTr.GL_Account_Code & " Location segmemt should be  " & obj.loc_code & ".")
                    End If
                    Dim strGLType As String = clsPaymentHeader.CheckGLAccountType(objTr.GL_Account_Code, trans)
                    If SettingCostCenter AndAlso Not clsCommon.CompairString(strGLType, "Balance Sheet") = CompairStringResult.Equal Then
                        objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells("Hirerachy Level").Value)
                        If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                            Throw New Exception("Hierarchy code not found")
                        End If
                        objTr.Hirerachy_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Hirerachy_Code As Row from TSPL_HIRERACHY_LEVEL_MASTER where Hirerachy_Code='" + objTr.Hirerachy_Code + "'", trans))
                        If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                            Throw New Exception("Invalid Hierarchy code-" + clsCommon.myCstr(grow.Cells("Hirerachy Level").Value))
                        End If

                        If SettingCostCenterlevel Then
                            objTr.Hirerachy_Code4 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel4).HeaderText).Value)
                            objTr.Hirerachy_Code3 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel3).HeaderText).Value)
                            objTr.Hirerachy_Code2 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel2).HeaderText).Value)
                            objTr.Hirerachy_Code1 = clsCommon.myCstr(grow.Cells(gv1.Columns(colHierarchyLevel1).HeaderText).Value)
                            If clsCommon.myLen(objTr.Hirerachy_Code4) > 0 Then
                                objTr.Hirerachy_Code4 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code4 + "'", trans))
                            End If
                            If clsCommon.myLen(objTr.Hirerachy_Code3) > 0 Then
                                objTr.Hirerachy_Code3 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code3 + "'", trans))
                            End If
                            If clsCommon.myLen(objTr.Hirerachy_Code2) > 0 Then
                                objTr.Hirerachy_Code2 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code2 + "'", trans))
                            End If
                            If clsCommon.myLen(objTr.Hirerachy_Code1) > 0 Then
                                objTr.Hirerachy_Code1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Hirerachy_Code1 + "'", trans))
                            End If
                            Dim lvl As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(Level,0) AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + objTr.Hirerachy_Code + "' ", trans))
                            If lvl > 3 Then
                                If clsCommon.myLen(objTr.Hirerachy_Code4) <= 0 Then
                                    Throw New Exception("Hierarchy Level 4 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code3) <= 0 Then
                                    Throw New Exception("Hierarchy Level 3 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                    Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                    Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=4 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code4 + "' and HIRERACHY_LEVEL_CODE3='" + objTr.Hirerachy_Code3 + "' and HIRERACHY_LEVEL_CODE2='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code4 + "][" + objTr.Hirerachy_Code3 + "][" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                End If
                                objTr.Cost_Centre_Code = objTr.Hirerachy_Code4
                            ElseIf lvl > 2 Then
                                If clsCommon.myLen(objTr.Hirerachy_Code3) <= 0 Then
                                    Throw New Exception("Hierarchy Level 3 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                    Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                    Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=3 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code3 + "' and HIRERACHY_LEVEL_CODE2='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code3 + "][" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                End If
                                objTr.Cost_Centre_Code = objTr.Hirerachy_Code3
                            ElseIf lvl > 1 Then
                                If clsCommon.myLen(objTr.Hirerachy_Code2) <= 0 Then
                                    Throw New Exception("Hierarchy Level 2 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                    Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=2 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code2 + "' and HIRERACHY_LEVEL_CODE1='" + objTr.Hirerachy_Code1 + "'"
                                dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code2 + "][" + objTr.Hirerachy_Code1 + "] ")
                                End If
                                objTr.Cost_Centre_Code = objTr.Hirerachy_Code2
                            ElseIf lvl > 0 Then
                                If clsCommon.myLen(objTr.Hirerachy_Code1) <= 0 Then
                                    Throw New Exception("Hierarchy Level 1 Code not found for GL Account -" + objTr.GL_Account_Code)
                                End If
                                strQry = "select 1 from TSPL_COST_CENTRE_HIRERACHY_DETAIL where HIRERACHY_LEVEL=1 and COST_CENTRE_HIRERACHY_CODE='" + objTr.Hirerachy_Code1 + "'"
                                dtTemp = clsDBFuncationality.GetDataTable(strQry, trans)
                                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                    Throw New Exception("Not a Valid Combition of Hierachy level [" + objTr.Hirerachy_Code1 + "]")
                                End If
                                objTr.Cost_Centre_Code = objTr.Hirerachy_Code1
                            End If
                        Else
                            objTr.Cost_Centre_Code = clsCommon.myCstr(grow.Cells("Cost Centre").Value)
                            If clsCommon.myLen(objTr.Cost_Centre_Code) <= 0 Then
                                Throw New Exception("Cost center not found")
                            End If
                            objTr.Cost_Centre_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + objTr.Cost_Centre_Code + "'", trans))
                            If clsCommon.myLen(objTr.Hirerachy_Code) <= 0 Then
                                Throw New Exception("Invalid Cost center-" + clsCommon.myCstr(grow.Cells("Cost Centre").Value))
                            End If
                        End If
                        If clsCommon.myLen(objTr.Cost_Centre_Code) <= 0 Then
                            Throw New Exception(" Please select Cost Center of GL Account -" + objTr.GL_Account_Code)
                        End If
                    End If


                    objTr.Amount = obj.Discount_Base
                    objTr.Discount_Per = obj.Discount_Percentage
                    objTr.Discount = obj.Discount_Amount
                    objTr.Amount_less_Discount = obj.Discount_Base - obj.Discount_Amount
                    objTr.Landed_Amount = 0
                    objTr.Total_Tax = 0
                    objTr.Total_Amount = obj.Amount_Less_Discount
                    objTr.AddChargeCode = ""
                    objTr.AddChargeDesc = ""
                    objTr.is_Unclaimed_Tax = False
                    objTr.Remarks = ""
                    objTr.Invoice_Type = ""
                    objTr.Invoice_No = ""
                    If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                        obj.Arr.Add(objTr)
                    End If

                    Dim EmployeeSalaryGeneration As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoEmployeeSalaryIntegration, clsFixedParameterCode.AllowtoEmployeeSalaryIntegration, trans)) = 1, True, False))
                    If EmployeeSalaryGeneration = True Then
                        Dim strEmployeeExpenseType As String = String.Empty
                        strEmployeeExpenseType = clsCommon.myCstr(grow.Cells("Employee Expense Type").Value)

                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select IsEmployee from TSPL_VENDOR_MASTER where Vendor_Code ='" & obj.Vendor_Code & "' ", trans)), "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strEmployeeExpenseType) > 0 Then
                                If clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "TD") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEmployeeExpenseType.ToUpper, "I") = CompairStringResult.Equal Then
                                Else
                                    Throw New Exception("Please enter T/TD/S/I for (Travelling/TA-DA/Salary/Imprest) for Employee Expense Type. ")
                                End If
                                obj.Employee_Type = strEmployeeExpenseType.ToUpper

                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account,Advance_Against_Imprest ,Advance_Against_Travelling,Employee_Salary  from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + obj.Account_Set + "'", trans)

                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "TD") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "T") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Travelling"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "S") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Employee_Salary"))
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Employee_Type), "I") = CompairStringResult.Equal Then
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Advance_Against_Imprest"))
                                    Else
                                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    End If
                                End If

                            End If
                        Else
                            obj.Employee_Type = ""
                        End If
                    End If
                    obj.SaveData(obj, True, trans)
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Error at Rowno " + Counter + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            Finally
                clsCommon.ProgressBarHide()
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub
    ' Ticket No : KDI/02/05/18-000284 by Prabhakar
    Public Sub FillVendorDetails()
        lblRegisterOrUnregister.Text = clsVendorMaster.GetVendorRegisterORNonRegister(txtVendorNo.Value, Nothing)
        lblGstinNo.Text = clsVendorMaster.GetVendorGSTINNo(txtVendorNo.Value, Nothing)
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + txtDocNo.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                'clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER set Authorized ='N' where Source_Doc_No ='" + txtDocNo.Value + "'", trans)

                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim coll As New Hashtable()
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colACCode).Value)) > 0 Then
                    Dim strGLAccountCode As String = clsCommon.myCstr(grow.Cells(colACCode).Value)
                    'clsCommon.AddColumnsForChange(coll, "GL_Account_Code", clsCommon.myCstr(grow.Cells(colACCode).Value))
                    'clsCommon.AddColumnsForChange(coll, "Detail_Line_No", clsCommon.myCstr(lineNo))
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(grow.Cells(colHierarchyCode).Value), True)
                    clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", clsCommon.myCstr(grow.Cells(colCostCenterCode).Value), True)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code1", clsCommon.myCstr(grow.Cells(colHierarchyLevel1).Value), True)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code2", clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value), True)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code3", clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value), True)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code4", clsCommon.myCstr(grow.Cells(colHierarchyLevel4).Value), True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_INVOICE_DETAIL", OMInsertOrUpdate.Update, "Document_No='" + txtDocNo.Value + "' and GL_Account_Code = '" + strGLAccountCode + "'", trans)
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + txtDocNo.Value + "' and  Source_Code in ('AP-IN','AP-CN','AP-DN') ", trans))
                    Dim qry As String = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "',Hirerachy_Code3= " + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value) & "' ", "NULL") + ",Hirerachy_Code4=" + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel4).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel4).Value) & "' ", "NULL") + " WHERE Voucher_No='" + strVoucherNo + "' and Account_code='" + strGLAccountCode + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Next

            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkSecurityAdjusment_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSecurityAdjusment.ToggleStateChanged
        txtSecurityAdjusment.Visible = chkSecurityAdjusment.Checked
    End Sub

    Private Sub txtSecurityAdjusment__My_Click(sender As Object, e As EventArgs) Handles txtSecurityAdjusment._My_Click
        Try
            If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
                Throw New Exception("Please first select Vendor")
            End If
            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                Throw New Exception("Please first select Location")
            End If
            Dim whrclas As String = ""
            whrclas += " TSPL_VENDOR_INVOICE_HEAD.Loc_Code in ('" + txtlocation.Value + "')"
            Dim qry As String = clsVedorInvoiceHead.GetRefundBaseQuery(TxtVendorNo.Value, txtDocNo.Value, whrclas)
            txtSecurityAdjusment.arrValueMember = clsCommon.ShowMultipleSelectForm("SecRef@API", qry, "DocumentCode", Nothing, txtSecurityAdjusment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
