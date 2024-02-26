Imports common
Imports System.Data.SqlClient
Imports System
Public Class FrmReceipttNew
    Inherits FrmMainTranScreen


#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isSettlementBankOnly As Boolean = False
    Public RcptType As String = Nothing
    Public strRcptNo As String = Nothing
    Dim inSideLoadData As Boolean = False
    Dim strQuery As String
    Dim myDs As DataSet
    Dim row As DataRow
    Dim myDr As SqlDataReader
    Dim myDataTable As DataTable
    Dim myCmd As New SqlCommand
    Dim tran As SqlTransaction
    Dim userCode, companyCode As String
    Dim i As Integer
    Dim isFlag As Boolean = False
    Dim btntooltip As ToolTip = New ToolTip()
    Dim x As Integer = 0
    Dim IsPaymentTypeChanged As Boolean = False
    Dim IsGobtnClicked As Boolean = False
    Public ChequeNo As String = Nothing
    Public ChequeDate As Date? = Nothing
    Public DocNo As String = Nothing
    Public Amount As Decimal = 0
    Public EntryDesc As String = Nothing
    Public IsNewEntry As Boolean = True
    Dim GSTStatus As Boolean = False
    '----------grid Varibales-----------
    Const colApply As String = "Apply"
    Const colDocType As String = "DocType"
    Const colSINo As String = "SaleInvoice"
    Const colDocNo As String = "DocNo"
    Const colDocDate As String = "DocDate"
    Const colVendorInvNo As String = "VendorInvNo"
    Const colFilledTotal As String = "FilledTotal"
    Const colEmptyTotal As String = "EmptyTotal"
    Const colOrgnlAmt As String = "OrgnlAmount"
    Const colBalAmt As String = "BalAmt"
    Const colTemp As String = "TempAmt"
    Const colTemp1 As String = "TempAmt1"
    Const colAppliedAmt As String = "AppliedAmt"
    Const colTDSAmt As String = "TDSAmt"
    Const colAdjNo As String = "AdjNo"
    Const colAdjAmt As String = "AdjAmt"
    Const colComment As String = "Comment"
    Const colInvisibleTag As String = "InvisibleTag"
    Const colChild_Cust_Code As String = "Child_Cust_Code"
    Const colChild_Cust_Name As String = "Child_Cust_Name"
    Const colConvRateOld As String = "colConvRateOld"

    Const colLineNo As String = "LineNo"
    Const colGLAccount As String = "GLAccount"
    Const colAccDesc As String = "AccDesc"
    Const colAmount As String = "Amount"
    Const colRemark As String = "Remark"
    '-----------------------------------
    Const colHirerachyCenter As String = "colHirerachyCenter"
    Const colHirerachyName As String = "colHirerachyName"
    Const colCostCenter As String = "colCostCenter"
    Const colCostCenterName As String = "colCostCenterName"
    Dim isApplyCostCenter As Boolean
    Dim Arr As ArrayList
    Dim Qry As String = ""
    Dim isApplyBranchAccounting As Boolean
    Dim isCustomerFinderLocationWiseARReceipt As Boolean
    Dim IsFirstTimeSave As Boolean = False
    Dim DOTaggingForDairySaleModule As Boolean = False
    Dim ApplyCardSaleInvoiceOnlyWithCardSaleAdvance As Boolean = False
    ' -------------- delievery order item grid details 
    Const AdcolDocument_Code As String = "AdcolDocument_Code"
    Const AdcolLine_No As String = "AdcolLine_No"
    Const AdcolRow_Type As String = "AdcolRow_Type"
    Const AdcolItem_Code As String = "AdcolItem_Code"
    Const adcolIName As String = "adcolIName"
    Const adcolIHSN As String = "adcolIHSN"
    Const AdcolQty As String = "AdcolQty"
    Const AdcolBalance_Qty As String = "AdcolBalance_Qty"
    Const AdcolUnit_code As String = "AdcolUnit_code"
    Const AdcolItem_Cost As String = "AdcolItem_Cost"
    Const AdcolTAX1 As String = "COLTAX1"
    Const AdcolTAX1_Amt As String = "COLTAXAMT1"
    Const AdcolTAX1_Base_Amt As String = "COLTAXBASEAMT1"
    Const AdcolTAX1_Rate As String = "COLTAXRATE1"
    Const Adcoltax2 As String = "COLTAX2"
    Const AdcolTAX2_Base_Amt As String = "COLTAXBASEAMT2"
    Const AdcolTAX2_Rate As String = "COLTAXRATE2"
    Const AdcolTAX2_Amt As String = "COLTAXAMT2"
    Const AdcolTAX3 As String = "COLTAX3"
    Const AdcolTAX3_Base_Amt As String = "COLTAXBASEAMT3"
    Const AdcolTAX3_Rate As String = "COLTAXRATE3"
    Const AdcolTAX3_Amt As String = "COLTAXAMT3"
    Const AdcolTAX4 As String = "COLTAX4"
    Const AdcolTAX4_Base_Amt As String = "COLTAXBASEAMT4"
    Const AdcolTAX4_Rate As String = "COLTAXRATE4"
    Const AdcolTAX4_Amt As String = "COLTAXAMT4"
    Const AdcolTAX5 As String = "COLTAX5"
    Const AdcolTAX5_Base_Amt As String = "COLTAXBASEAMT5"
    Const AdcolTAX5_Rate As String = "COLTAXRATE5"
    Const AdcolTAX5_Amt As String = "COLTAXAMT5"
    Const AdcolTAX6 As String = "COLTAX6"
    Const AdcolTAX6_Base_Amt As String = "COLTAXBASEAMT6"
    Const AdcolTAX6_Rate As String = "COLTAXRATE6"
    Const AdcolTAX6_Amt As String = "COLTAXAMT6"
    Const AdcolTAX7 As String = "COLTAX7"
    Const AdcolTAX7_Base_Amt As String = "COLTAXBASEAMT7"
    Const AdcolTAX7_Rate As String = "COLTAXRATE7"
    Const AdcolTAX7_Amt As String = "COLTAXAMT7"
    Const AdcolTAX8 As String = "COLTAX8"
    Const AdcolTAX8_Base_Amt As String = "COLTAXBASEAMT8"
    Const AdcolTAX8_Rate As String = "COLTAXRATE8"
    Const AdcolTAX8_Amt As String = "COLTAXAMT8"
    Const AdcolTAX9 As String = "COLTAX9"
    Const AdcolTAX9_Base_Amt As String = "COLTAXBASEAMT9"
    Const AdcolTAX9_Rate As String = "COLTAXRATE9"
    Const AdcolTAX9_Amt As String = "COLTAXAMT9"
    Const AdcolTAX10 As String = "COLTAX10"
    Const AdcolTAX10_Base_Amt As String = "COLTAXBASEAMT10"
    Const AdcolTAX10_Rate As String = "COLTAXRATE10"
    Const AdcolTAX10_Amt As String = "COLTAXAMT10"
    Const AdcolAmount As String = "AdcolAmount"
    Const AdcolDisc_Per As String = "AdcolDisc_Per"
    Const AdcolDisc_Amt As String = "AdcolDisc_Amt"
    Const AdcolAmt_Less_Discount As String = "AdcolAmt_Less_Discount"
    Const AdcolTotal_Tax_Amt As String = "AdcolTotal_Tax_Amt"
    Const AdcolItem_Net_Amt As String = "AdcolItem_Net_Amt"
    Const AdcolMRP As String = "AdcolMRP"
    Const AdcolAbatement_Per As String = "AdcolAbatement_Per"
    Const AdcolAbatement_Amt As String = "AdcolAbatement_Amt"
    Const AdcolScheme_Code As String = "AdcolScheme_Code"
    Const AdcolScheme_Applicable As String = "AdcolScheme_Applicable"
    Const AdcolScheme_Item As String = "AdcolScheme_Item"
    Const AdcolFOC_Item As String = "AdcolFOC_Item"
    Const AdcolItem_Tax As String = "AdcolItem_Tax"
    Const AdcolTotal_MRP_Amt As String = "AdcolTotal_MRP_Amt"
    Const AdcolTotal_Basic_Amt As String = "AdcolTotal_Basic_Amt"
    Const AdcolTotal_Disc_Amt As String = "AdcolTotal_Disc_Amt"
    Const AdcolActualRate As String = "AdcolActualRate"
    Const AdcolConv_Factor As String = "AdcolConv_Factor"
    Const AdcolTotalItem_Weight As String = "AdcolTotalItem_Weight"
    Const AdcolLanding_Cost As String = "AdcolLanding_Cost"

    Const AdcolTAX1_Amt_Receipt As String = "COLTAXAMTRECEIPT1"
    Const AdcolTAX2_Amt_Receipt As String = "COLTAXAMTRECEIPT2"
    Const AdcolTAX3_Amt_Receipt As String = "COLTAXAMTRECEIPT3"
    Const AdcolTAX4_Amt_Receipt As String = "COLTAXAMTRECEIPT4"
    Const AdcolTAX5_Amt_Receipt As String = "COLTAXAMTRECEIPT5"
    Const AdcolTAX6_Amt_Receipt As String = "COLTAXAMTRECEIPT6"
    Const AdcolTAX7_Amt_Receipt As String = "COLTAXAMTRECEIPT7"
    Const AdcolTAX8_Amt_Receipt As String = "COLTAXAMTRECEIPT8"
    Const AdcolTAX9_Amt_Receipt As String = "COLTAXAMTRECEIPT9"
    Const AdcolTAX10_Amt_Receipt As String = "COLTAXAMTRECEIPT10"

    Const AdcolReceiptAdvance As String = "AdcolReceiptAdvance"
    Const AdcolReceiptTotalTax As String = "AdcolReceiptTotalTax"
    Const AdcolReceiptTotalAdvanceAmt As String = "AdcolReceiptTotalAdvanceAmt"



    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"




    '' grid detial of tax 

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    ''----------
    Dim ERPStartDate As Date
    Dim RefundknockoffwithCreditNote As Boolean = False
    Dim EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt As Boolean = False
    Dim receiptForm As Boolean = True
    Public Check As Boolean = False
    Public Property StringPass As String
    Public Property StringPass1 As String
    Public ReceiptFormOpen As Boolean = False
    'Dim rceceiptformOpens As New frmCustomer("", "")
    'Dim valueEntry As Boolean = rceceiptformOpens.ReceiptFormOpens
    Dim strRecieptCode As String = ""
    Dim chk As Boolean = False
    'Private valueEntryforreciept As Boolean = True
    'Dim frm As New frmCustomer("", "")
    'Public Event _MYOpenMasterForm As EventHandler
    'Dim frm1 As New frmCustomer("user", "company")








#End Region

#Region "Button Click"

#Region "Page Load"

    Private Sub FrmReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ERPStartDate = objCommonVar.ERPStartDate

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Invalid ERP Start Date", Me.Text)
            Me.Close()
        End Try
        ''------------------------
        isApplyBranchAccounting = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 1, True, False)
        isCustomerFinderLocationWiseARReceipt = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CustomerMasterFinderOnLocationwiseARReceipt, clsFixedParameterCode.CustomerMasterFinderOnLocationwiseARReceipt, Nothing)) = 1, True, False)
        isApplyCostCenter = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowHierarchyAndCostCenterInARInvoiceEntry, clsFixedParameterCode.ShowHierarchyAndCostCenterInARInvoiceEntry, Nothing)) = 1
        DOTaggingForDairySaleModule = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DOTaggingForDairySaleModule, clsFixedParameterCode.DOTaggingForDairySaleModule, Nothing)) = 1, True, False))
        RefundknockoffwithCreditNote = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RefundknockoffwithCreditNote, clsFixedParameterCode.RefundknockoffwithCreditNote, Nothing)) = 1, True, False)
        EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt, clsFixedParameterCode.EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt, Nothing)) = 1, True, False)
        ApplyCardSaleInvoiceOnlyWithCardSaleAdvance = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCardSaleInvoiceOnlyWithCardSaleAdvance, clsFixedParameterCode.ApplyCardSaleInvoiceOnlyWithCardSaleAdvance, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        btnGo.Enabled = False
        txtmemoamt.Enabled = False
        dtRcpt.Value = clsCommon.GETSERVERDATE()
        dtPost.Value = clsCommon.GETSERVERDATE()
        dtCheque.Value = clsCommon.GETSERVERDATE()
        pnlCheque.Enabled = False
        pnlbankbranch.Enabled = True
        txtRcptAmt.ReadOnly = True
        globalFunc.mandatoryText(txtChkNo, txtCusName)
        btntooltip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnClose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnNew, "Press Alt+N Adding New Transaction")
        txtEntrDesc.MaxLength = 100
        btnAdjment.Enabled = True
        txtRcptAmt.Text = ""
        txtUnApplAmt.Text = ""
        txtUnAppliedBal.Text = ""

        If RcptType = "Misc" Then
            ddlTransType.SelectedIndex = 3
        End If
        ValidateLength()
        ApplyReadOptions()
        txtCusName.TabIndex = txtSamesmanName.TabIndex
        fndCustomer.TabIndex = txtsalesmanCode.TabIndex
        chkSalesmanType.Checked = False
        chkSalesmanType.Enabled = False
        loadReceiptType()
        loadSecurityDepositType()
        TypeChange()
        LoadBlankGrid("R")
        setGridFocus()
        'ddlTransType.Items(4).Enabled = False   ' Disables 'On Account' from combo box.
        ddlTransType.Items(5).Enabled = False   ' Disables 'Unapplied' from combo box.
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        RadPageView1.Pages("TabForGST").Item.Visibility = ElementVisibility.Collapsed

        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
        Me.txtBaseCurrency.Value = objCommonVar.BaseCurrencyCode
        SetMultiCurrencyVisibility()
        If clsCommon.myLen(strRcptNo) > 0 Then
            funFillDetails(strRcptNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            funFillDetails(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            ''richa agarwal VIJ/01/10/19-000002
            If clsCommon.myLen(objCommonVar.ObjVar1) > 0 Then
                ddlTransType.SelectedValue = "P"
                fndBookingNo.Visible = True
                fndBookingNo.Value = objCommonVar.ObjVar1
                txtUnApplAmt.Text = objCommonVar.ObjVar3
                dtRcpt.Value = objCommonVar.ObjVar2
                fndCustomer.Value = objCommonVar.ObjVar4
                txtCusName.Text = clsCustomerMaster.GetName(fndCustomer.Value, Nothing)
            End If

        End If

        '' MultiCurrency




        '' End of MultiCurrency

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        txtTotalPaymentBaseCurr.Enabled = False
        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
            If isApplyBranchAccounting = True Then
                RadLabel18.Visible = True
                txtlocation.Visible = True
                LblLocDesp.Visible = True
                txtlocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ) "))
                If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                    LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
                Else
                    LblLocDesp.Text = ""
                End If
            Else
                RadLabel18.Visible = False
                txtlocation.Visible = False
                LblLocDesp.Visible = False
                txtlocation.Value = ""
                LblLocDesp.Text = ""
            End If
        End If

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Try
                clsDBFuncationality.ExecuteNonQuery("alter table TSPL_RECEIPT_HEADER alter column ConvRate decimal(18,9) null ")
                txtConversionRate.DecimalPlaces = 6
            Catch ex As Exception

            End Try
        End If
        ''richa agarwal VIJ/01/10/19-000002
        If clsCommon.myLen(Me.Tag) > 0 Then
            If clsCommon.myLen(objCommonVar.ObjVar1) > 0 Then
                txtlocation.Value = objCommonVar.ObjVar5
                LblLocDesp.Text = clsLocation.GetName(txtlocation.Value, Nothing)
            End If
        End If

        GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
            chkForCardSale.Visible = True
        Else
            chkForCardSale.Visible = False
        End If

        If frmCustomer.valueEntry = True Then
            ddlTransType.SelectedValue = "P"
            chkSecurityDposit.Checked = True
            ddlSecDepositType.SelectedValue = "S"
            fndCustomer.Value = StringPass1
            txtCusName.Text = StringPass
            funFillDetails(ddlTransType.SelectedValue, NavigatorType.Current)

        End If

        'Dim obj1 As New clsRcptEntryHeader()
        If clsCommon.myLen(strRecieptCode) > 0 Then
            fndRcptNo.Value = strRecieptCode
            funFillDetails(fndRcptNo.Value, NavigatorType.Current)
        End If
        Dim index As Integer = sender.tag.ToString.IndexOf(",")
        Dim Cust_Code As String
        Dim Doc_Date As Date
        If index > 0 Then
            Cust_Code = sender.tag.ToString.Substring(0, index)
            Doc_Date = sender.tag.ToString.Substring(index + 1)
        End If

        fndCustomer.Value = Cust_Code
        dtRcpt.Value = Doc_Date
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            chk = True
        End If
        txtCusName.Text = clsCustomerMaster.GetName(fndCustomer.Value, Nothing)
    End Sub
    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True

            If clsCommon.myLen(Me.fndCustomer.Value) > 0 Then
                strq = "select currency_code from TSPL_CUSTOMER_MASTER where CUST_CODE='" & clsCommon.myCstr(Me.fndCustomer.Value) & "'"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
                ShowCurrencyDetail()
            End If
            ShowCurrencyDetail()
        Else
            SplitContainer1.SplitterDistance = SplitContainer1.SplitterDistance - pnlCurrConv.Height
            pnlCurrConv.Height = 0
            pnlCurrConv.Visible = False
        End If

    End Sub
    Sub ShowCurrencyDetail()
        '' Anubhooti 07-May-2015 BM00000006486
        Dim dt As DataTable
        If clsCommon.myLen(clsCommon.myCstr(Me.txtCurrencyCode.Value)) = 0 Then
            Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
            Me.txtConversionRate.ReadOnly = True
            Exit Sub
        End If

        If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
            dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.dtRcpt.Value, txtCurrencyCode.Value)
            If dt.Rows.Count = 0 Then
                If Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode Then
                    Me.txtConversionRate.Text = 1
                    Me.txtApplicableFrom.Text = ""
                    Me.txtConversionRate.ReadOnly = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "Conversion rate not entered for currency '" & Me.txtCurrencyCode.Value & "'")
                    Exit Sub
                End If
            Else
                ' Me.txtConversionRate.Text = dt.Rows(0).Item("Rate")
                Me.txtConversionRate.Text = clsCommon.myCdbl(dt.Rows(0).Item("Rate"))
                Me.txtConversionRate.ReadOnly = False
                Me.txtApplicableFrom.Text = clsCommon.GetPrintDate(dt.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
            End If
        Else
            'Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode.ToString
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
            Me.txtConversionRate.ReadOnly = True
        End If

    End Sub
    Private Sub ValidateLength()
        fndRcptNo.MyMaxLength = 30
        txtEntrDesc.MaxLength = 500
        txtChkNo.MaxLength = 6
        txtReceivedFrom.MaxLength = 100
        txtUnApplieadNo.MaxLength = 20
    End Sub

    Private Sub ApplyReadOptions()
        fndRcptNo.MyReadOnly = False
    End Sub
    Sub EnableDisableIncaseofRefund()
        If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
            txtChkFavOf.Enabled = True
            chkACPayee.Enabled = True
            lblChqFavoroff.Enabled = True
        Else
            txtChkFavOf.Enabled = False
            chkACPayee.Enabled = False
            lblChqFavoroff.Enabled = False
        End If
    End Sub
#End Region

    Sub LoadBlankGrid(ByVal ReceiptType As String)

        If clsCommon.CompairString(ReceiptType, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "A") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "K") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
            dgvReceipt.DataSource = Nothing
            dgvReceipt.Rows.Clear()
            dgvReceipt.Columns.Clear()

            dgvReceipt.AllowDeleteRow = True
            dgvReceipt.AllowAddNewRow = False

            Dim apply As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            apply.FormatString = ""
            apply.HeaderText = colApply
            apply.Name = colApply
            apply.Width = 50
            apply.ReadOnly = True
            apply.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvReceipt.MasterTemplate.Columns.Add(apply)

            Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            docType.FormatString = ""
            docType.HeaderText = "Document Type"
            docType.Name = colDocType
            docType.Width = 100
            docType.ReadOnly = True
            dgvReceipt.MasterTemplate.Columns.Add(docType)

            Dim SiNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            SiNo.FormatString = ""
            SiNo.HeaderText = "Document Sale Invoice No"
            SiNo.Name = colSINo
            SiNo.Width = 100
            SiNo.ReadOnly = True
            SiNo.IsVisible = True
            dgvReceipt.MasterTemplate.Columns.Add(SiNo)

            Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            docNo.FormatString = ""
            docNo.HeaderText = "Document No"
            docNo.Name = colDocNo
            docNo.Width = 150
            docNo.ReadOnly = True
            docNo.IsVisible = True
            dgvReceipt.MasterTemplate.Columns.Add(docNo)

            Dim docDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            docDate.FormatString = ""
            docDate.HeaderText = "Document Date"
            docDate.Name = colDocDate
            docDate.Width = 150
            docDate.ReadOnly = True
            dgvReceipt.MasterTemplate.Columns.Add(docDate)

            Dim FilledTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
            FilledTotal.FormatString = ""
            FilledTotal.HeaderText = "Filled"
            FilledTotal.Name = colFilledTotal
            FilledTotal.Width = 70
            FilledTotal.ReadOnly = True
            FilledTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(FilledTotal)

            Dim EmptyTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
            EmptyTotal.FormatString = ""
            EmptyTotal.HeaderText = "Empty"
            EmptyTotal.Name = colEmptyTotal
            EmptyTotal.Width = 70
            EmptyTotal.ReadOnly = True
            EmptyTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(EmptyTotal)

            Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            originalInvAmt.FormatString = ""
            originalInvAmt.HeaderText = "Original Amount"
            originalInvAmt.Name = colOrgnlAmt
            originalInvAmt.Width = 100
            originalInvAmt.ReadOnly = True
            originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(originalInvAmt)

            Dim BalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            BalAmt.FormatString = ""
            BalAmt.DecimalPlaces = 2
            BalAmt.HeaderText = "Balance Amt"
            BalAmt.Name = colBalAmt
            BalAmt.Width = 100
            BalAmt.ReadOnly = True
            BalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(BalAmt)

            Dim appliedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            appliedAmt.FormatString = ""
            appliedAmt.DecimalPlaces = 2
            appliedAmt.HeaderText = "Applied Amount"
            appliedAmt.Name = colAppliedAmt
            appliedAmt.Width = 100
            appliedAmt.ReadOnly = False
            appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(appliedAmt)

            appliedAmt = New GridViewDecimalColumn()
            appliedAmt.FormatString = ""
            appliedAmt.DecimalPlaces = 4
            appliedAmt.HeaderText = "Conv Rate Old"
            appliedAmt.Name = colConvRateOld
            appliedAmt.Width = 100
            appliedAmt.ReadOnly = True
            appliedAmt.IsVisible = False
            appliedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(appliedAmt)

            Dim temp As GridViewDecimalColumn = New GridViewDecimalColumn()
            temp.FormatString = ""
            temp.Name = colTemp
            temp.ReadOnly = True
            temp.IsVisible = False
            temp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(temp)

            temp = New GridViewDecimalColumn()
            temp.FormatString = ""
            temp.Name = colTemp1
            temp.ReadOnly = True
            temp.IsVisible = False
            temp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(temp)

            Dim tdsAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            tdsAmt.FormatString = ""
            tdsAmt.HeaderText = "TDS Amount"
            tdsAmt.Name = colTDSAmt
            tdsAmt.Width = 100
            tdsAmt.ReadOnly = True
            tdsAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(tdsAmt)

            Dim adjNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            adjNo.FormatString = ""
            adjNo.HeaderText = "Adjustment No"
            adjNo.Width = 100
            adjNo.Name = colAdjNo
            adjNo.ReadOnly = True
            adjNo.IsVisible = True
            dgvReceipt.MasterTemplate.Columns.Add(adjNo)

            Dim adjAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            adjAmt.FormatString = ""
            adjAmt.HeaderText = "Adjustment Amt"
            adjAmt.Name = colAdjAmt
            adjAmt.Width = 100
            adjAmt.ReadOnly = True
            adjAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvReceipt.MasterTemplate.Columns.Add(adjAmt)

            Dim comment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            comment.FormatString = ""
            comment.HeaderText = "Comment"
            comment.Name = colComment
            comment.Width = 150
            comment.ReadOnly = False
            dgvReceipt.MasterTemplate.Columns.Add(comment)

            Dim InvisibleTag As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            InvisibleTag.FormatString = ""
            InvisibleTag.HeaderText = "InvTag"
            InvisibleTag.Name = colInvisibleTag
            InvisibleTag.ReadOnly = True
            InvisibleTag.IsVisible = False
            dgvReceipt.MasterTemplate.Columns.Add(InvisibleTag)

            Dim Child_Cust_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Child_Cust_Code.FormatString = ""
            Child_Cust_Code.HeaderText = "Child Customer Code"
            Child_Cust_Code.Name = colChild_Cust_Code
            Child_Cust_Code.Width = 100
            Child_Cust_Code.ReadOnly = True
            dgvReceipt.MasterTemplate.Columns.Add(Child_Cust_Code)

            Dim Child_Cust_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Child_Cust_Name.FormatString = ""
            Child_Cust_Name.HeaderText = "Child Customer Name"
            Child_Cust_Name.Name = colChild_Cust_Name
            Child_Cust_Name.Width = 200
            Child_Cust_Name.ReadOnly = True
            dgvReceipt.MasterTemplate.Columns.Add(Child_Cust_Name)

            clsCustomFieldGrid.LoadBlankGrid(dgvReceipt, MyBase.ArrDetailFields)

            dgvReceipt.ShowGroupPanel = False
            dgvReceipt.AllowColumnReorder = False
            dgvReceipt.AllowRowReorder = False
            dgvReceipt.EnableSorting = False
            dgvReceipt.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            dgvReceipt.MasterTemplate.ShowRowHeaderColumn = False
            dgvReceipt.AllowAddNewRow = False
            MyBase.ReStoreGridLayoutMain(dgvReceipt)
        ElseIf clsCommon.CompairString(ReceiptType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "S") = CompairStringResult.Equal Then
            dgvmiscpayment.DataSource = Nothing
            dgvmiscpayment.Rows.Clear()
            dgvmiscpayment.Columns.Clear()

            dgvmiscpayment.AllowDeleteRow = True
            dgvmiscpayment.AllowAddNewRow = True
            dgvmiscpayment.AddNewRowPosition = SystemRowPosition.Bottom
            dgvmiscpayment.Rows.AddNew()

            Dim LineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            LineNo.FormatString = ""
            LineNo.HeaderText = "Line No"
            LineNo.Name = colLineNo
            LineNo.Width = 60
            LineNo.ReadOnly = True
            LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            dgvmiscpayment.MasterTemplate.Columns.Add(LineNo)

            Dim GLAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            GLAccount.FormatString = ""
            GLAccount.HeaderText = "Account Code"
            GLAccount.Name = colGLAccount
            GLAccount.Width = 175
            GLAccount.ReadOnly = False
            dgvmiscpayment.MasterTemplate.Columns.Add(GLAccount)

            Dim AccDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            AccDesc.FormatString = ""
            AccDesc.HeaderText = "Description"
            AccDesc.Name = colAccDesc
            AccDesc.Width = 300
            AccDesc.ReadOnly = True
            dgvmiscpayment.MasterTemplate.Columns.Add(AccDesc)


            GLAccount = New GridViewTextBoxColumn()
            GLAccount.FormatString = ""
            GLAccount.HeaderText = "Hierarchy Level"
            GLAccount.Name = colHirerachyCenter
            GLAccount.Width = 100
            GLAccount.ReadOnly = False
            GLAccount.IsVisible = isApplyCostCenter
            dgvmiscpayment.MasterTemplate.Columns.Add(GLAccount)

            AccDesc = New GridViewTextBoxColumn()
            AccDesc.FormatString = ""
            AccDesc.HeaderText = "Hierarchy Level Description"
            AccDesc.Name = colHirerachyName
            AccDesc.Width = 200
            AccDesc.ReadOnly = True
            AccDesc.IsVisible = isApplyCostCenter
            dgvmiscpayment.MasterTemplate.Columns.Add(AccDesc)

            GLAccount = New GridViewTextBoxColumn()
            GLAccount.FormatString = ""
            GLAccount.HeaderText = "Cost Center Code"
            GLAccount.Name = colCostCenter
            GLAccount.Width = 100
            GLAccount.ReadOnly = False
            GLAccount.IsVisible = isApplyCostCenter
            dgvmiscpayment.MasterTemplate.Columns.Add(GLAccount)

            AccDesc = New GridViewTextBoxColumn()
            AccDesc.FormatString = ""
            AccDesc.HeaderText = "Cost Center Description"
            AccDesc.Name = colCostCenterName
            AccDesc.Width = 200
            AccDesc.ReadOnly = True
            AccDesc.IsVisible = isApplyCostCenter
            dgvmiscpayment.MasterTemplate.Columns.Add(AccDesc)


            Dim Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
            Amount = New GridViewDecimalColumn()
            Amount.FormatString = ""
            Amount.HeaderText = "Amount"
            Amount.Name = colAmount
            Amount.Width = 150
            Amount.ReadOnly = False
            Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            dgvmiscpayment.MasterTemplate.Columns.Add(Amount)

            Dim remark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            remark.FormatString = ""
            If clsCommon.CompairString(ReceiptType, "M") = CompairStringResult.Equal Then
                remark.HeaderText = "Received from"
            Else
                remark.HeaderText = "Remarks"
            End If
            remark.Name = colRemark
            remark.Width = 200
            remark.ReadOnly = False
            dgvmiscpayment.MasterTemplate.Columns.Add(remark)

            clsCustomFieldGrid.LoadBlankGrid(dgvmiscpayment, MyBase.ArrDetailFields)

            dgvmiscpayment.ShowGroupPanel = False
            dgvmiscpayment.AllowColumnReorder = False
            dgvmiscpayment.AllowRowReorder = False
            dgvmiscpayment.EnableSorting = False
            dgvmiscpayment.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            dgvmiscpayment.MasterTemplate.ShowRowHeaderColumn = False
            dgvmiscpayment.AllowAddNewRow = False
            MyBase.ReStoreGridLayoutMain(dgvmiscpayment)
        End If
    End Sub

    Public Sub SaveDataF()
        Try
            If savedata() Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Try
        '    If savedata() Then
        '        clsCommon.MyMessageBoxShow("Data saved successfully.")
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message)
        'End Try
        SaveDataF()
    End Sub

    Public Function allowToSave() As Boolean
        Try
            'If IsNewEntry And ddlTransType.SelectedValue = "O" Then
            '    common.clsCommon.MyMessageBoxShow("You can not create 'On Account' Entry.")
            '    ddlTransType.Focus()
            '    Return False
            'End If 
            '=================Added by Richa AGARWAL 8 nOV,2019
            If clsCommon.myLen(txtEntrDesc.Text) <= 0 AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Description can't be blank.", Me.Text)
                txtEntrDesc.Focus()
                Return False
            End If
            If IsNewEntry = False AndAlso clsCommon.myLen(fndRcptNo.Value) > 0 Then
                Dim strBookingType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Booking_Type ,'') from TSPL_BOOKING_MATSER where isnull(Against_Receipt_No ,'')='" & fndRcptNo.Value & "'"))
                If clsCommon.myLen(strBookingType) > 0 Then
                    If clsCommon.CompairString(strBookingType, "CD") = CompairStringResult.Equal Then
                        Throw New Exception("Receipt can't be changed because it has created against Card Sale Booking.")
                    End If
                End If
            End If

            If AllowFutureDateTransaction(dtRcpt.Value, Nothing) = False Then
                dtRcpt.Focus()
                Return False
            End If
            If chkSecurityDposit.Visible = True AndAlso chkSecurityDposit.Checked = True Then
                If clsCommon.myLen(ddlSecDepositType.SelectedValue) <= 0 Then
                    Throw New Exception("Please select Security Deposit Type.")
                End If
            End If
            If IsNewEntry And ddlTransType.SelectedValue = "U" Then
                common.clsCommon.MyMessageBoxShow(Me, "You can not create 'Unapplied' Entry.", Me.Text)
                ddlTransType.Focus()
                Return False
            End If
            If clsCommon.myLen(fndRcptNo.Value) > 0 Then
                Dim isPosted As String = clsDBFuncationality.getSingleValue("Select Posted from TSPL_RECEIPT_HEADER Where Receipt_No='" + fndRcptNo.Value + "'")
                If clsCommon.CompairString(isPosted, "Y") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Document already posted", Me.Text)
                    funFillDetails(fndRcptNo.Value, NavigatorType.Current)
                End If
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Then
                If clsCommon.myLen(dgvmiscpayment.Rows(0).Cells(colAmount).Value) <= 0 Then
                    Throw New Exception("Please fill atleast one row on grid.")
                End If
            End If

            ''richa 17 July,2019  TEC/24/07/19-000958
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
            If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                If clsCommon.myCDate(dtRcpt.Value, "dd/MM/yyyy") <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                    If (clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso chkSecurityDposit.Checked = True) Or clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal Then
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Document Date should be Greater Than ERP Start Date " & clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") & "", Me.Text)
                        ddlTransType.Focus()
                        Return False
                    End If
                End If
            End If
            '------------------------Checks Bank Code-----------------------------
            Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No='" & txtDocumentNo.Value & "'"))
            If clsCommon.myLen(strdocumentType) > 0 AndAlso clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                Dim strbancode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, Nothing))
                If clsCommon.myLen(strbancode) > 0 Then
                    Dim bankcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE as [Code] from TSPL_BANK_MASTEr where TSPL_bank_master.INACTIVE ='Active' and TSPL_BANK_MASTER.bank_type<>'S' and Bank_Code='" & clsCommon.myCstr(strbancode) & "' "))
                    If clsCommon.myLen(bankcode) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter Bank Code into fixed parameter.", Me.Text)
                        Return False
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Bank Code into fixed parameter.", Me.Text)
                    Return False
                End If
            Else
                If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Bank Code.", Me.Text)
                    fndBankCode.Focus()
                    Return False
                End If

                '------------------------Payment Mode And Cheque Number-----------------------------------------------
                If clsCommon.myLen(fndPayType.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Payment  Type .", Me.Text)
                    fndPayType.Focus()
                    Return False
                Else
                    Dim strcheckcode As String = connectSql.RunScalar("select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndPayType.Value + "'")
                    If clsCommon.CompairString(strcheckcode, "Cheque") = CompairStringResult.Equal Then
                        If txtChkNo.Text = "" Then
                            If (chkCheckPrint.Visible And chkCheckPrint.Checked = False) Then
                                txtChkNo.Focus()
                                clsCommon.MyMessageBoxShow(Me, "Cheque No can't be blank", Me.Text)
                                txtChkNo.Focus()
                                Return False
                            End If

                        Else
                            'Dim check As String = "select isnull(sum(Type),0) as Type,max(receipt_no) as receipt_no from ( Select 1 as Type, receipt_no from TSPL_RECEIPT_HEADER Where Cheque_No='" + txtChkNo.Text + "'  AND receipt_no <> '" + fndRcptNo.Value + "'" & _
                            '" union all select (-1) as Type, Document_No from TSPL_BANK_REVERSE where Cheque_No ='" + txtChkNo.Text + "') as xx "
                            Dim check As String = "select isnull(sum(Type),0) as Type,max(receipt_no) as receipt_no from ( Select 1 as Type, receipt_no from TSPL_RECEIPT_HEADER Where Cheque_No='" + txtChkNo.Text + "' and Bank_Code='" + fndBankCode.Value + "' AND receipt_no <> '" + fndRcptNo.Value + "'" &
                            " union all select (-1) as Type, Document_No from TSPL_BANK_REVERSE where Cheque_No ='" + txtChkNo.Text + "' and Bank_Code='" + fndBankCode.Value + "' ) as xx "


                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(check)

                            Dim chkNo As String = txtChkNo.Text
                            If dt.Rows.Count > 0 Then
                                If clsCommon.myCstr(dt.Rows(0).Item("Type")) = 1 Then
                                    txtChkNo.Text = ""
                                    txtChkNo.Focus()
                                    Throw New Exception("This Cheque No '" + chkNo + "' is Already Exists Against Payment No '" + clsCommon.myCstr(dt.Rows(0).Item("receipt_no")) + "'")

                                End If
                            End If
                            If clsCommon.myLen(txtChkNo.Text) > 0 Then
                                If clsCommon.myLen(txtChkNo.Text) < 6 Then
                                    txtChkNo.Focus()
                                    Throw New Exception("Length of Cheque No should be of 6 digits.")
                                End If
                            End If

                        End If
                        ''richa VIJ/17/12/19-000122
                    ElseIf clsCommon.CompairString(strcheckcode, "Credit/Debit") = CompairStringResult.Equal Then
                        If clsCommon.myLen(txtChkNo.Text) <= 0 Then
                            txtChkNo.Focus()
                            Throw New Exception("Credit/Debit card No can't be blank")
                        End If
                        If clsCommon.myLen(txtChkNo.Text) > 4 Or clsCommon.myLen(txtChkNo.Text) < 4 Then
                            txtChkNo.Focus()
                            Throw New Exception("Length of Credit/Debit card No should be of 4 digits.")
                        End If
                        If IsNumeric(txtChkNo.Text) = False Then
                            txtChkNo.Focus()
                            Throw New Exception("Credit/Debit card No should be of numbers only")
                        End If
                    End If
                End If
                '--------------------------------------------------------------------------------------------------


            End If


            '---------------------------------------------------------------------
            Dim RcptAmt As Decimal = clsCommon.myCdbl(txtUnApplAmt.Text)
            If chkmemorndm.Checked = False AndAlso RcptAmt <= 0 AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "K") <> CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Fill Up Receipt Amount!", "Receipt Entry", MessageBoxButtons.OK)
                Return False
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
                If RcptAmt <> 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Receipt Amount should be 0", "Receipt Entry", MessageBoxButtons.OK)
                    Return False
                End If
                If clsCommon.myCdbl(txtRcptAmt.Text) <> 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Applied Amount should be 0", "Receipt Entry", MessageBoxButtons.OK)
                    Return False
                End If
            End If

            ''richa agarwal 22 May,2018 ERO/21/05/18-000319,ERO/20/06/18-000357
            Dim AllowtoSetReceiptAmountForCashTransaction As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSetReceiptAmountForCashTransaction, clsFixedParameterCode.AllowtoSetReceiptAmountForCashTransaction, Nothing))
            If AllowtoSetReceiptAmountForCashTransaction > 0 And clsCommon.CompairString(ddlTransType.SelectedValue, "A") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), clsDBFuncationality.getSingleValue("select Payment_Code from tspl_payment_code where Payment_Type ='Cash'")) = CompairStringResult.Equal Then
                    Dim dblTotalreceiptCashAmount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("sELECT ISNULL(sum(Receipt_Amount),0) FROM TSPL_RECEIPT_HEADER WHERE CUST_CODE='" & clsCommon.myCstr(fndCustomer.Value) & "' AND Receipt_Date ='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "' and Payment_Code='CASH' and TSPL_RECEIPT_HEADER.Receipt_Type <>'A' and Receipt_No <>'" & clsCommon.myCstr(fndRcptNo.Value) & "'"))
                    If (dblTotalreceiptCashAmount + clsCommon.myCdbl(txtRcptAmt.Text)) > AllowtoSetReceiptAmountForCashTransaction Then
                        Throw New Exception("You cannot create receipt entry of amount greater than " & AllowtoSetReceiptAmountForCashTransaction & " for Payment mode Cash for each customer.")
                    End If
                End If
            End If
            ''---------------
            '-------------------------Customer Code in case if Misc Receipt Or Misc Refund--------------------
            If ddlTransType.SelectedValue = "M" OrElse ddlTransType.SelectedValue = "S" Then

            Else
                If fndCustomer.Value = "" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please  Select Customer", "Receipt Entry", MessageBoxButtons.OK, Me.Text)
                    Return False
                End If
            End If
            '--------------------------------------------------------------------------------------------------

            '' 25-Sep-2015 BM00000007964 (Unapplied amount can not be greater than receipt amount)
            If clsCommon.myCdbl(txtUnAppliedBal.Text) > clsCommon.myCdbl(txtUnApplAmt.Text) Then
                common.clsCommon.MyMessageBoxShow(Me, "Please check ! Unapplied amount can not be greater than from receipt amount.", "Receipt Entry", MessageBoxButtons.OK, Me.Text)
                Return False
            End If
            ''
            '' Anubhooti 07-May-2015 BM00000006486 (Conversion rate can not be zero)
            If clsCommon.myCdbl(txtConversionRate.Text) <= 0 Then
                txtConversionRate.Focus()
                Throw New Exception("Conversion rate can not be zero or less than zero")
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                If isApplyBranchAccounting = True Then
                    If clsCommon.myLen(txtlocation.Value) <= 0 Then
                        txtlocation.Focus()
                        Throw New Exception("Please Select Location")
                    End If
                End If
            End If
            'If clsCommon.myLen(txtlocation.Value) <= 0 Then
            '    txtlocation.Focus()
            '    Throw New Exception("Please Select Location")
            'End 

            Dim TotalAmt As Double = clsCommon.myCdbl(txtUnApplAmt.Text)
            Dim EnteredAmt As Double = 0
            Dim Counter As Integer = 0
            If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                If clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please select Applied document.", Me.Text)
                        txtDocumentNo.Focus()
                        Return False
                    End If
                End If
                Dim isDebitNoteExist As Boolean = False
                Dim isCreditNoteExist As Boolean = False
                Dim isRefundNoteExist As Boolean = False
                Dim isInvoiceExist As Boolean = False
                Dim isDataInGrid As Boolean = False
                For ii As Integer = 0 To dgvReceipt.Rows.Count - 1
                    If clsCommon.CompairString("Yes", clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colApply).Value)) = CompairStringResult.Equal Then
                        isDataInGrid = True
                        If clsCommon.CompairString("Invoice", clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colDocType).Value)) = CompairStringResult.Equal Then
                            isInvoiceExist = True
                        ElseIf clsCommon.CompairString("Debit Note", clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colDocType).Value)) = CompairStringResult.Equal Then
                            isDebitNoteExist = True
                        ElseIf clsCommon.CompairString("Credit Note", clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colDocType).Value)) = CompairStringResult.Equal Then
                            isCreditNoteExist = True
                        ElseIf clsCommon.CompairString("Refund", clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colDocType).Value)) = CompairStringResult.Equal Then
                            isRefundNoteExist = True
                        End If
                        If clsCommon.myCdbl(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value) > 0 Then
                            Counter = Counter + 1 '''''validate for apply atleast single document
                        End If
                        If clsCommon.CompairString(dgvReceipt.Rows(ii).Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                            If (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                                EnteredAmt += clsCommon.myCdbl(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value)
                            Else
                                EnteredAmt -= clsCommon.myCdbl(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value)
                            End If
                        Else
                            EnteredAmt += clsCommon.myCdbl(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value)
                        End If
                        ''richa agarwal 7 Aug,2019 ERO/07/08/19-000987

                        Dim availBal As Double = BalanceAmount_invoice(fndCustomer.Value, dgvReceipt.Rows(ii).Cells(colDocNo).Value)
                        If availBal < clsCommon.myCdbl(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Available Balance:  " + clsCommon.myCstr(availBal) + " Applied Amount: " + clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colAppliedAmt).Value) + " against Document: " + clsCommon.myCstr(dgvReceipt.Rows(ii).Cells(colSINo).Value) + "", Me.Text)
                            dgvReceipt.Rows(ii).Cells(colBalAmt).Value = availBal
                            Return False
                        End If
                    End If
                Next
                If isDataInGrid Then
                    If Not isInvoiceExist And Not isDebitNoteExist And Not isRefundNoteExist Then
                        'If isCreditNoteExist Then
                        '    Throw New Exception("Can not Apply Only Credit note type document")
                        'End If

                        If isCreditNoteExist AndAlso Not (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                            Throw New Exception("Can not Apply Only Credit note type document")
                        End If
                    End If
                End If
                If Counter <= 0 Then
                    If dgvReceipt.Rows.Count <= 0 AndAlso (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                        If clsCommon.MyMessageBoxShow("There is no pending credit note for this Customer " & fndCustomer.Value & ". Do you still want to continue to create this Refund", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                            Return False
                        End If
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please apply atleast single document", Me.Text)
                        Return False
                    End If

                End If
                If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                    If EnteredAmt - TotalAmt > 0.1 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Applied Amount " + clsCommon.myCstr(EnteredAmt) + " Receipt Amount " + clsCommon.myCstr(TotalAmt), Me.Text)
                        Return False
                    End If
                Else
                    If EnteredAmt - clsCommon.myCdbl(lblBalAmt.Text) > 0.1 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Applied Amount " + clsCommon.myCstr(EnteredAmt) + " Receipt Amount " + clsCommon.myCstr(TotalAmt), Me.Text)
                        Return False
                    End If
                End If

            ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Then
                Dim arrAccountCode As New List(Of String)
                For ii As Integer = 1 To dgvmiscpayment.Rows.Count - 1
                    Dim strACode As String = clsCommon.myCstr(dgvmiscpayment.Rows(ii).Cells(colGLAccount).Value)
                    If clsCommon.myLen(strACode) > 0 Then
                        If Not arrAccountCode.Contains(strACode) Then
                            arrAccountCode.Add(strACode)
                        End If
                    End If

                    EnteredAmt += clsCommon.myCdbl(dgvmiscpayment.Rows(ii).Cells(colAmount).Value)
                Next
                If chkSalesmanType.Checked = True AndAlso clsCommon.myLen(txtsalesmanCode.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Salesman Code Or Uncheck Salesman Type", Me.Text)
                    txtsalesmanCode.Focus()
                    Return False
                End If

                If clsCommon.myCdbl(txtUnAppliedBal.Text) > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Receipt Amount Should Be Equal to Applied Amount", "Recepit Entry", MessageBoxButtons.OK, RadMessageIcon.Info, Me.Text)
                    Return False
                End If

                If clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal Then
                    If arrAccountCode IsNot Nothing AndAlso arrAccountCode.Count > 0 Then
                        If clsCommon.GetDateWithStartTime(dtRcpt.Value) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                            'richa 17 SEp,2019 TEC/03/07/19-000927
                            Dim qry As String = "select Account_Code from TSPL_GL_ACCOUNTS where Account_Code in (" + clsCommon.GetMulcallString(arrAccountCode) + ") and ControlAccount<>'N' AND TSPL_GL_ACCOUNTS.Account_Code NOT IN  (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForReceipt   =1)"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Can not select control Account -" + clsCommon.myCstr(dt.Rows(0)("Account_Code")), Me.Text)
                                Return False
                            End If
                        End If
                    End If
                End If


            ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                If chkCForm.Checked = True Then
                    If clsCommon.myLen(txtCFormInvNo.Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please Select Invoice No for applying C Form.", Me.Text)
                        Return False
                    End If
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), "Cash", False) = CompairStringResult.Equal And chkAutoGeneBT.Checked And (clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal) Then
                If clsCommon.myLen(txtToBank.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select To Bank for Auto Bank Transfer.", Me.Text)
                    Return False
                End If
            End If

            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then '------------in advance and memorandum case---
                If chkmemorndm.Checked = True AndAlso clsCommon.myCdbl(txtmemoamt.Text) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill Memorandum Amount", Me.Text)
                    Return False
                End If

                If chkmemorndm.Checked = True AndAlso clsCommon.myCdbl(txtUnApplAmt.Text) > 0 AndAlso clsCommon.myCdbl(txtUnApplAmt.Text) <> clsCommon.myCdbl(txtmemoamt.Text) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Receipt Amount Should Be Equal To Memorandum Amount", Me.Text)
                    Return False
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bank_type from tspl_bank_master where BANK_CODE='" + fndBankCode.Value + "'")), "S") = CompairStringResult.Equal Then
                If Not clsCommon.CompairString("O", clsCommon.myCstr(ddlTransType.SelectedValue)) = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Transaction type should be on Account for settlement bank.", Me.Text)
                    ddlTransType.Focus()
                    Return False
                End If
            End If

            GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
            If GSTStatus Then
                If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal And clsCommon.myLen(txtDONo.Value) > 0 Then
                    'If clsCommon.myCdbl(txtUnApplAmt.Text) > clsCommon.myCdbl(LblDOTotalAmount.Text) Then
                    '    clsCommon.MyMessageBoxShow("Receipt Amount cannot be greater than DO Amount")
                    '    txtUnApplAmt.Focus()
                    '    Return False
                    'End If
                    Dim strCustmer As String = String.Empty
                    If clsCommon.CompairString(lblReceiptThroughSO.Text, "DO") = CompairStringResult.Equal Then
                        If DOTaggingForDairySaleModule = True Then
                            strCustmer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Code from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" & clsCommon.myCstr(txtDONo.Value) & "' "))
                        Else
                            strCustmer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Code from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code='" & clsCommon.myCstr(txtDONo.Value) & "' "))
                        End If
                    Else
                        strCustmer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Code from TSPL_SD_SALES_ORDER_HEAD where Document_Code='" & clsCommon.myCstr(txtDONo.Value) & "' "))
                    End If

                    If clsCommon.CompairString(strCustmer, fndCustomer.Value) <> CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Customer of Receipt should be same as DO", Me.Text)
                        fndCustomer.Focus()
                        Return False
                    End If
                End If
            End If


            ''richa agarwal  to check bank balance
            If Not isFlag Then
                CheckNegativeBankBalance()
            End If
            ''--------
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''richa agarwal create function to check bank balance on save
    Public Function CheckNegativeBankBalanceonDelete() As Boolean
        If clsCommon.CompairString(ddlTransType.SelectedValue, "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "S") <> CompairStringResult.Equal Then
            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
            Dim Bank_Type As String = clsBankMaster.GetBankType(fndBankCode.Value, Nothing)
            Dim Bank_Balance As Decimal = 0
            ''richa agarwal 02 Sep, 2016
            ' Dim Refund_Amount As Decimal = clsCommon.myCdbl(Me.txtTotalPaymentBaseCurr.Text)
            Dim Refund_Amount As Decimal = clsCommon.myCdbl(Me.txtTotalPaymentBaseCurr.Text) + clsCommon.myCdbl(txtUnAppliedBal.Text)
            Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndBankCode.Value & "')", Nothing))

            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing, True)
                    ''------------
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing, True)
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing, True)
                If Bank_Balance < Refund_Amount Then
                    Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function


    ''--------------


    ''richa agarwal create function to check bank balance on save
    Public Function CheckNegativeBankBalance() As Boolean
        If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Then
            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
            Dim Bank_Type As String = clsBankMaster.GetBankType(fndBankCode.Value, Nothing)
            Dim Bank_Balance As Decimal = 0
            Dim Refund_Amount As Decimal = clsCommon.myCdbl(Me.txtTotalPaymentBaseCurr.Text)
            Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndBankCode.Value & "')", Nothing))

            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing)
                    ''------------
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing)
                    If Bank_Balance < Refund_Amount Then
                        Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                Bank_Balance = clsPaymentHeader.GetBankBalance(fndRcptNo.Value, dtRcpt.Value, fndBankCode.Value, Bank_Location, Nothing)
                If Bank_Balance < Refund_Amount Then
                    Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function


    ''--------------

    Public Function savedata(Optional ByVal isPosted As Boolean = False)
        Try
            If (allowToSave()) Then
                If clsCommon.CompairString(ddlTransType.SelectedValue, "A") <> CompairStringResult.Equal Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndRcptNo.Value))
                End If

                Dim obj As New clsRcptEntryHeader()

                obj.memorndmamt = "0"
                If chkmemorndm.Checked = True Then
                    obj.memorndmamt = clsCommon.myCstr(txtmemoamt.Text)
                End If

                obj.SecurityDepositType = ddlSecDepositType.SelectedValue
                obj.Receipt_No = clsCommon.myCstr(fndRcptNo.Value)
                obj.Entry_Desc = clsCommon.myCstr(txtEntrDesc.Text)
                ''richa agarwal 23 Nov,2016
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ApplyDocumentDate, clsFixedParameterCode.ApplyDocumentDate, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "A") = CompairStringResult.Equal Then
                    Dim lbldocinvoicedate As Date?
                    For Each grow As GridViewRowInfo In dgvReceipt.Rows
                        If grow.Cells(colApply).Value = "Yes" Then
                            If clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value) >= clsCommon.myCDate(grow.Cells(colDocDate).Value) Then
                                lbldocinvoicedate = clsCommon.myCDate(dgvReceipt.Rows(0).Cells(colDocDate).Value)
                            Else
                                lbldocinvoicedate = clsCommon.myCDate(grow.Cells(colDocDate).Value)
                            End If
                        End If
                    Next
                    Dim lblapplydocumentdate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No ='" & clsCommon.myCstr(txtDocumentNo.Value) & "'"))
                    If clsCommon.myCDate(lblapplydocumentdate) >= clsCommon.myCDate(lbldocinvoicedate) Then
                        obj.Receipt_Date = clsCommon.myCDate(lblapplydocumentdate)
                    Else
                        obj.Receipt_Date = clsCommon.myCDate(lbldocinvoicedate)
                    End If
                End If
                '----------- end --------------------
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ApplyDocumentDate, clsFixedParameterCode.ApplyDocumentDate, Nothing), "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "A") = CompairStringResult.Equal Then
                Else
                    obj.Receipt_Date = dtRcpt.Value
                End If

                obj.Receipt_Post_Date = dtPost.Value
                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                obj.Receipt_Type = clsCommon.myCstr(ddlTransType.SelectedValue)
                obj.CSATransfer_No = clsCommon.myCstr(txttransfer_no.Value)
                '' Anubhooti 02-Dec-2014 (Booking No. should only visible in case of Advance only)
                If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                    obj.Booking_Code = clsCommon.myCstr(fndBookingNo.Value)
                Else
                    obj.Booking_Code = ""
                End If
                obj.TapalNo = clsCommon.myCstr(txtTapalNo.Text)
                If txtDataAndTimeSelection.Checked Then
                    obj.DateAndTime = txtDataAndTimeSelection.Value
                End If
                '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
                obj.SaleOrderNo = clsCommon.myCstr(FndSalesOrder.Value)
                obj.Foreign_Bank_Charges_Amt = clsCommon.myCdbl(TxtForeignBankCharges.Value)
                obj.Bank_Charges_Amt = clsCommon.myCdbl(TxtBankCharges.Value)
                If chkForCardSale.Checked Then
                    obj.isCardSale = 1
                Else
                    obj.isCardSale = 0
                End If
                ''-----------------------
                If clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal Then
                    obj.Loadout_No = txtLoadOutno.Value
                End If
                obj.Payment_Code = clsCommon.myCstr(fndPayType.Value)
                Dim strPayType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Type from TSPL_PAYMENT_CODE where Payment_Code='" + fndPayType.Value + "'")) ''ERO/29/08/19-001007 by balwinder on 29/08/2019

                If clsCommon.CompairString(strPayType, "Cheque") = CompairStringResult.Equal Then
                    obj.Cheque_No = clsCommon.myCstr(txtChkNo.Text)
                    obj.Cheque_Date = clsCommon.myCDate(dtCheque.Value)
                    obj.Cheque_From = clsCommon.myCstr(txtReceivedFrom.Text)
                    obj.From_Branch = clsCommon.myCstr(txtBranch.Text)
                ElseIf clsCommon.CompairString(strPayType, "Credit/Debit") = CompairStringResult.Equal Then
                    obj.Cheque_No = clsCommon.myCstr(txtChkNo.Text)
                    obj.Cheque_Date = Nothing
                Else
                    obj.Cheque_No = ""
                    obj.Cheque_Date = Nothing
                End If
                ''richa default bank code in case of apply documnet whe we use credit note
                Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No='" & txtDocumentNo.Value & "'"))
                If clsCommon.myLen(strdocumentType) > 0 AndAlso clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                    obj.Bank_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDefaultBankCodeforCreditNote, clsFixedParameterCode.AllowDefaultBankCodeforCreditNote, Nothing))
                    obj.Payment_Code = "NEFT"
                End If

                obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
                obj.Against_RCDF_Loadin = txtLoadIn.Value
                obj.Distr_Code = clsCommon.myCstr(txtDistr_Code.Text)
                '' Anubhooti 30-Oct-2014 BM00000003904
                Dim OutstandingAmt As Decimal = 0
                'If IsNewEntry Then '' No need to consider outstanding
                '    If Not (clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal) Then
                '        If common.clsCommon.MyMessageBoxShow("Outstanding of customer is (" & lblOutstanding.Text & "). Do you want to consider it ?", "Receipt", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            OutstandingAmt = clsCommon.myCdbl(lblOutstanding.Text)
                '        End If
                '    End If
                'End If
                ''
                obj.Receipt_Amount = IIf(clsCommon.myCdbl(txtRcptAmt.Text) = 0, clsCommon.myCdbl(txtUnApplAmt.Text), clsCommon.myCdbl(txtRcptAmt.Text))
                obj.Receipt_Amount = obj.Receipt_Amount + OutstandingAmt

                obj.Balance_Amt = obj.Receipt_Amount
                If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                    obj.UnApplied_Balance = clsCommon.myCdbl(txtUnAppliedBal.Text)
                    obj.UnApplied_No = clsCommon.myCstr(txtUnApplieadNo.Text)
                End If
                If clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                    obj.UnApply_Amt = 0
                Else
                    obj.UnApply_Amt = clsCommon.myCdbl(txtUnApplAmt.Text) + OutstandingAmt
                End If

                If chkSalesmanType.Checked = True Then
                    obj.IsSalesmanType = "Y"
                    obj.Salesman_Code = clsCommon.myCstr(txtsalesmanCode.Value)
                    obj.Salesman_Name = clsCommon.myCstr(txtSamesmanName.Text)
                Else
                    obj.IsSalesmanType = "N"
                End If

                If chkSecurityDposit.Enabled = True AndAlso chkSecurityDposit.Checked = True Then
                    obj.SecurityDeposit = "Y"
                Else
                    obj.SecurityDeposit = "N"
                End If


                If chkCForm.Checked = True Then
                    obj.CFormRecd = "Y"
                Else
                    obj.CFormRecd = "N"
                End If
                obj.CForm_InvoiceNo = txtCFormInvNo.Value
                '' is parent customer
                obj.IsParentCust = Me.chkParentCust.Checked
                If chkCheckPrint.Checked Then
                    obj.CHECK_PRINT = 1
                Else
                    obj.CHECK_PRINT = 0
                End If
                obj.AUTO_GEN_BT_ENTRY = chkAutoGeneBT.Checked

                If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                    obj.Location_GL_Code = clsCommon.myCstr(txtlocation.Value)
                End If
                obj.TO_BANK_CODE = Me.txtToBank.Value

                If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                    obj.Applied_Receipt = clsCommon.myCstr(txtDocument_ForAdvanceDoc.Value)
                Else
                    obj.Applied_Receipt = clsCommon.myCstr(txtDocumentNo.Value)
                End If
                If chkACPayee.Checked Then
                    obj.AC_Payee = 1
                Else
                    obj.AC_Payee = 0
                End If

                obj.cheque_in_favour_of = clsCommon.myCstr(txtChkFavOf.Text)

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                'obj.is_Opening = chkOpening.Checked

                ''richa 17 July,2019 
                'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
                obj.is_Opening = 0
                If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                    Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                    If clsCommon.myCDate(obj.Receipt_Date, "dd/MM/yyyy") <= clsCommon.GetPrintDate(dtERPStartDate, "dd/MM/yyyy") Then
                        obj.is_Opening = 1
                    End If
                End If

                obj.ArrTr = New List(Of clsReceiptDettail)
                obj.ArrTrRefund = New List(Of clsReceiptDetail_Refund)
                '============================Detail Section==============================
                If clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "K") = CompairStringResult.Equal Then
                    For i = 0 To dgvReceipt.Rows.Count - 1
                        Dim objTr As New clsReceiptDettail()
                        If dgvReceipt.Rows(i).Cells(colApply).Value = "Yes" Then
                            objTr.Apply = "Y"
                            If clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "D"
                            ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "C"
                            ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "F"
                            Else
                                objTr.Receipt_Type = "I"
                            End If
                            If dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "N" Then
                                objTr.TagType = "N"
                            ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "S" Then
                                objTr.TagType = "S"
                            ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "C" Then
                                objTr.TagType = "C"
                            End If
                            objTr.Document_No = dgvReceipt.Rows(i).Cells(colDocNo).Value
                            objTr.Document_Date = clsCommon.GetPrintDate(dgvReceipt.Rows(i).Cells(colDocDate).Value, "yyyy-MM-dd")
                            objTr.Original_Amt = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colOrgnlAmt).Value)
                            objTr.Pending_Balance = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colBalAmt).Value)
                            objTr.Applied_Amount = dgvReceipt.Rows(i).Cells(colAppliedAmt).Value
                            objTr.Adjustment_No = dgvReceipt.Rows(i).Cells(colAdjNo).Value
                            objTr.Adjustment_Cost = dgvReceipt.Rows(i).Cells(colAdjAmt).Value
                            objTr.Comment = dgvReceipt.Rows(i).Cells(colComment).Value

                            If clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value) = 0 Then
                                objTr.ConvRateOld = 1
                            Else
                                objTr.ConvRateOld = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value)
                            End If
                            '' child cust code
                            objTr.Child_Cust_Code = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colChild_Cust_Code).Value)
                            obj.ArrTr.Add(objTr)
                        End If
                    Next
                    If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                        clsCustomFieldGrid.GetData(obj.arrCustomFields, dgvReceipt, MyBase.ArrDetailFields, colDocNo)
                    End If
                ElseIf RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                    For i = 0 To dgvReceipt.Rows.Count - 1
                        Dim objTr As New clsReceiptDetail_Refund()
                        If dgvReceipt.Rows(i).Cells(colApply).Value = "Yes" Then
                            objTr.Apply = "Y"
                            If clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Debit Note") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "D"
                            ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "C"
                            ElseIf clsCommon.CompairString(dgvReceipt.Rows(i).Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                                objTr.Receipt_Type = "F"
                            Else
                                objTr.Receipt_Type = "I"
                            End If
                            If dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "N" Then
                                objTr.TagType = "N"
                            ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "S" Then
                                objTr.TagType = "S"
                            ElseIf dgvReceipt.Rows(i).Cells(colInvisibleTag).Value = "C" Then
                                objTr.TagType = "C"
                            End If
                            objTr.Document_No = dgvReceipt.Rows(i).Cells(colDocNo).Value
                            objTr.Document_Date = clsCommon.GetPrintDate(dgvReceipt.Rows(i).Cells(colDocDate).Value, "yyyy-MM-dd")
                            objTr.Original_Amt = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colOrgnlAmt).Value)
                            objTr.Pending_Balance = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colBalAmt).Value)
                            objTr.Applied_Amount = dgvReceipt.Rows(i).Cells(colAppliedAmt).Value
                            objTr.Adjustment_No = dgvReceipt.Rows(i).Cells(colAdjNo).Value
                            objTr.Adjustment_Cost = dgvReceipt.Rows(i).Cells(colAdjAmt).Value
                            objTr.Comment = dgvReceipt.Rows(i).Cells(colComment).Value

                            If clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value) = 0 Then
                                objTr.ConvRateOld = 1
                            Else
                                objTr.ConvRateOld = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colConvRateOld).Value)
                            End If
                            objTr.Child_Cust_Code = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colChild_Cust_Code).Value)
                            obj.ArrTrRefund.Add(objTr)
                        End If
                    Next
                ElseIf clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "S") = CompairStringResult.Equal Then
                    For i = 0 To dgvmiscpayment.Rows.Count - 1
                        Dim objTr As New clsReceiptDettail()
                        If dgvmiscpayment.Rows(i).Cells(colAmount).Value <> 0 And Not String.IsNullOrEmpty(dgvmiscpayment.Rows(i).Cells(colAmount).Value) Then
                            objTr.Apply = "Y"
                            objTr.Receipt_Type = "M"
                            objTr.Applied_Amount = clsCommon.myCdbl(dgvmiscpayment.Rows(i).Cells(colAmount).Value)
                            objTr.Account_Code = clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colGLAccount).Value)
                            objTr.Description = clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colAccDesc).Value)
                            objTr.Remarks = clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colRemark).Value)

                            objTr.Hirerachy_Level_Code = clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colHirerachyCenter).Value)
                            objTr.Cost_Center_Fin_Code = clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colCostCenter).Value)
                            If isApplyCostCenter Then
                                Dim grouptype As String = ""
                                grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(dgvmiscpayment.Rows(i).Cells(colGLAccount).Value), Nothing)
                                If clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                                Else
                                    If clsCommon.myLen(objTr.Hirerachy_Level_Code) <= 0 Then
                                        Throw New Exception("Please provide the Hierarchy Level " + clsCommon.myCstr(i))
                                    ElseIf clsCommon.myLen(objTr.Cost_Center_Fin_Code) <= 0 Then
                                        Throw New Exception("Please provide the Cost Center " + clsCommon.myCstr(i))
                                    End If
                                End If

                            End If

                            obj.ArrTr.Add(objTr)
                        End If
                    Next
                    If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                        clsCustomFieldGrid.GetData(obj.arrCustomFields, dgvmiscpayment, MyBase.ArrDetailFields, colDocNo)
                    End If
                End If

                '==================Detail Section Ends Here=======================

                '' CurrencConversion
                If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then

                    obj.CURRENCY_CODE = Me.txtCurrencyCode.Value
                    obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                    obj.ConvRateOld = clsCommon.myCdbl(Me.txtConversionRate.Text)

                    If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                        obj.ApplicableFrom = Me.txtApplicableFrom.Text
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                    obj.BASE_CURRENCY_CODE = clsCommon.myCstr(Me.txtBaseCurrency.Value)
                    obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(Me.txtTotalPaymentBaseCurr.Text)

                    ''richa agarwal 18/05/2015

                    If clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "K") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) <> CompairStringResult.Equal Then
                            '' only during posting of transaction
                            '' gather information for exchange gain and loss account
                            Dim dt As DataTable
                            dt = clsRcptEntryHeader.GetExchangeDetailDt(fndCustomer.Value)
                            If dt.Rows.Count > 0 Then
                                obj.EXCHANGE_GAIN_ACCOUNT = clsCommon.myCstr(dt.Rows(0).Item("EXCHANGE_GAIN_ACCOUNT"))
                                obj.EXCHANGE_LOSS_ACCOUNT = clsCommon.myCstr(dt.Rows(0).Item("EXCHANGE_LOSS_ACCOUNT"))
                            Else
                                obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                                obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                            End If
                            Dim dtLastRate As DataTable
                            '' gather conv rate and amount of transaction to calculate exchange loss and gain
                            Dim strInvoiceNo As String = String.Empty
                            Dim lossorgainamount As Double = 0
                            Dim Totallossorgainamount As Double = 0

                            Dim InvoiceType As String = ""
                            For i = 0 To dgvReceipt.Rows.Count - 1
                                strInvoiceNo = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocNo).Value)
                                InvoiceType = clsCommon.myCstr(dgvReceipt.Rows(i).Cells(colDocType).Value)
                                dtLastRate = clsRcptEntryHeader.GetExchangeRateAmount(strInvoiceNo, dtRcpt.Value)
                                If clsCommon.CompairString(InvoiceType, "Credit Note") = CompairStringResult.Equal Then
                                    lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate") * -1
                                Else
                                    lossorgainamount = clsCommon.myCdbl(dgvReceipt.Rows(i).Cells(colAppliedAmt).Value) * dtLastRate.Rows(0).Item("ConvRate")
                                End If

                                Totallossorgainamount = Totallossorgainamount + lossorgainamount
                            Next


                            Dim diff As Double = 0.0
                            If Totallossorgainamount <> 0 Then
                                'diff = obj.RECEIVED_AMOUNT_BASE_CURRENCY - Totallossorgainamount
                                diff = Math.Round(obj.RECEIVED_AMOUNT_BASE_CURRENCY, 2, MidpointRounding.AwayFromZero) - Math.Round(Totallossorgainamount, 2, MidpointRounding.AwayFromZero)
                                diff = Math.Round(diff, 2, MidpointRounding.AwayFromZero)
                            End If

                            If diff = 0 Then
                                obj.EXCHANGE_LOSS_AMT = 0
                                obj.EXCHANGE_GAIN_AMT = 0
                            ElseIf diff < 0 Then
                                If clsCommon.myLen(obj.EXCHANGE_LOSS_ACCOUNT) = 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Exchange Loss Account not defined.", Me.Text)
                                    Return False
                                End If
                                obj.EXCHANGE_LOSS_AMT = -diff
                                obj.EXCHANGE_GAIN_AMT = 0
                            Else
                                If clsCommon.myLen(obj.EXCHANGE_GAIN_ACCOUNT) = 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Exchange Gain Account not defined.", Me.Text)
                                    Return False
                                End If
                                obj.EXCHANGE_LOSS_AMT = 0
                                obj.EXCHANGE_GAIN_AMT = diff
                            End If
                        End If
                    Else
                        obj.EXCHANGE_LOSS_AMT = 0
                        obj.EXCHANGE_GAIN_AMT = 0
                        obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                        obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                    End If

                Else
                    obj.CURRENCY_CODE = Nothing
                    obj.ConvRate = 1
                    obj.ConvRateOld = 1
                    obj.ApplicableFrom = Nothing
                    obj.BASE_CURRENCY_CODE = Nothing
                    obj.RECEIVED_AMOUNT_BASE_CURRENCY = 0
                    obj.EXCHANGE_LOSS_AMT = 0
                    obj.EXCHANGE_GAIN_AMT = 0
                    obj.EXCHANGE_GAIN_ACCOUNT = Nothing
                    obj.EXCHANGE_LOSS_ACCOUNT = Nothing
                End If
                '' end CurrencyConversion

                ''richa agarwal
                GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
                If GSTStatus Then
                    If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                        obj.Delivery_order_Amount = clsCommon.myCdbl(LblDOTotalAmount.Text)
                        obj.DO_Total_Add_Amount = clsCommon.myCdbl(lblDOTotalAdditionalCharge.Text)
                        obj.Tax_Amount_Advance = clsCommon.myCdbl(lblDOTotalTaxAmt.Text)
                        obj.Tax_Group = clsCommon.myCstr(txtTaxGroup.Value)
                        obj.Delivery_Code_PS = clsCommon.myCstr(txtDONo.Value)
                        obj.SO_Location_Code = clsCommon.myCstr(lblDO_Location.Text)
                        obj.ReceiptAgainstSO_DO = clsCommon.myCstr(lblReceiptThroughSO.Text)
                        obj.ArrTrGST = New List(Of clsReceiptDetailGST)

                        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0 Then

                            If clsCommon.myLen(txtitem.Value) > 0 Then
                                Dim objTr As New clsReceiptDetailGST()

                                objTr.Receipt_No = clsCommon.myCstr(fndRcptNo.Value)
                                objTr.Line_No = 1
                                objTr.Row_Type = "Item"
                                objTr.Item_Code = clsCommon.myCstr(txtitem.Value)
                                objTr.Unit_code = clsDBFuncationality.getSingleValue("select UOM_Code from tspl_item_uom_detail where item_code='" & objTr.Item_Code & "' and Default_UOM=1 ")
                                objTr.ReceiptTotalTax = clsCommon.myCdbl(lblDOTotalTaxAmt.Text)
                                objTr.ReceiptTotalAdvanceAmt = clsCommon.myCdbl(txtRcptAmt.Text)
                                objTr.ReceiptAdvance = clsCommon.myCdbl(txtRcptAmt.Text) - clsCommon.myCdbl(lblDOTotalTaxAmt.Text)
                                obj.ArrTrGST.Add(objTr)

                            End If

                        Else
                            For i = 0 To gvItem.Rows.Count - 1
                                Dim objTr As New clsReceiptDetailGST()

                                objTr.Document_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolDocument_Code).Value)
                                objTr.Receipt_No = clsCommon.myCstr(fndRcptNo.Value)
                                objTr.Line_No = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolLine_No).Value)
                                objTr.Row_Type = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolRow_Type).Value)
                                objTr.Item_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolItem_Code).Value)
                                objTr.Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolQty).Value)
                                objTr.Balance_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolBalance_Qty).Value)
                                objTr.Item_Cost = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolItem_Cost).Value)
                                objTr.Unit_code = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolUnit_code).Value)
                                objTr.TAX1 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX1).Value)
                                objTr.TAX1_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt).Value)
                                objTr.TAX1_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Base_Amt).Value)
                                objTr.TAX1_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value)
                                objTr.tax2 = clsCommon.myCstr(gvItem.Rows(i).Cells(Adcoltax2).Value)
                                objTr.TAX2_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Base_Amt).Value)
                                objTr.TAX2_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value)
                                objTr.TAX2_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Amt).Value)
                                objTr.TAX3 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX3).Value)
                                objTr.TAX3_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Base_Amt).Value)
                                objTr.TAX3_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value)
                                objTr.TAX3_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Amt).Value)
                                objTr.TAX4 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX4).Value)
                                objTr.TAX4_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Base_Amt).Value)
                                objTr.TAX4_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value)
                                objTr.TAX4_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Amt).Value)
                                objTr.tax5 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX5).Value)
                                objTr.TAX5_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Base_Amt).Value)
                                objTr.TAX5_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value)
                                objTr.TAX5_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Amt).Value)
                                objTr.TAX6_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Base_Amt).Value)
                                objTr.tax6 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX6).Value)
                                objTr.TAX6_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value)
                                objTr.TAX6_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Amt).Value)
                                objTr.tax7 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX7).Value)
                                objTr.TAX7_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Base_Amt).Value)
                                objTr.TAX7_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value)
                                objTr.TAX7_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Amt).Value)
                                objTr.tax8 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX8).Value)
                                objTr.TAX8_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Base_Amt).Value)
                                objTr.TAX8_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value)
                                objTr.TAX8_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Amt).Value)
                                objTr.tax9 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX9).Value)
                                objTr.TAX9_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Base_Amt).Value)
                                objTr.TAX9_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Amt).Value)
                                objTr.TAX9_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value)
                                objTr.tax10 = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolTAX10).Value)
                                objTr.TAX10_Base_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Base_Amt).Value)
                                objTr.TAX10_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)
                                objTr.TAX10_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Amt).Value)
                                objTr.Amount = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolAmount).Value)
                                objTr.Disc_Per = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolDisc_Per).Value)
                                objTr.Disc_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolDisc_Amt).Value)
                                objTr.Amt_Less_Discount = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolAmt_Less_Discount).Value)
                                objTr.Total_Tax_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTotal_Tax_Amt).Value)
                                objTr.Item_Net_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolItem_Net_Amt).Value)
                                objTr.MRP = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolMRP).Value)
                                objTr.Abatement_Per = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolAbatement_Per).Value)
                                objTr.Abatement_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolAbatement_Amt).Value)
                                objTr.Scheme_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolScheme_Code).Value)
                                objTr.Scheme_Applicable = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolScheme_Applicable).Value)
                                objTr.Scheme_Item = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolScheme_Item).Value)
                                objTr.FOC_Item = clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolFOC_Item).Value)
                                objTr.Item_Tax = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolItem_Tax).Value)
                                objTr.Total_MRP_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTotal_MRP_Amt).Value)
                                objTr.Total_Basic_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTotal_Basic_Amt).Value)
                                objTr.Total_Disc_Amt = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTotal_Disc_Amt).Value)
                                objTr.ActualRate = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolActualRate).Value)
                                objTr.TotalItem_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTotalItem_Weight).Value)
                                objTr.Conv_Factor = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolConv_Factor).Value)
                                objTr.Landing_Cost = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolLanding_Cost).Value)
                                objTr.TAX1_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value), 4)
                                objTr.TAX2_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value), 4)
                                objTr.TAX3_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value), 4)
                                objTr.TAX4_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value), 4)
                                objTr.TAX5_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value), 4)
                                objTr.TAX6_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value), 4)
                                objTr.TAX7_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value), 4)
                                objTr.TAX8_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value), 4)
                                objTr.TAX9_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value), 4)
                                objTr.TAX10_Amt_Receipt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value), 4)
                                objTr.ReceiptAdvance = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value), 4)
                                objTr.ReceiptTotalTax = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value), 4)
                                objTr.ReceiptTotalAdvanceAmt = Math.Round(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value), 4)
                                obj.ArrTrGST.Add(objTr)
                            Next
                        End If

                        For i = 0 To gvTaxDetail.Rows.Count - 1
                            If i = 0 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.TAX1 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX1_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX1_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX1_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If

                            If i = 1 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax2 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX2_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX2_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX2_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If

                            If i = 2 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.TAX3 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX3_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX3_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX3_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If

                            If i = 3 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.TAX4 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX4_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX4_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX4_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If

                            If i = 4 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax5 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX5_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX5_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX5_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If
                            If i = 5 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax6 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX6_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX6_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX6_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If

                            If i = 6 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax7 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX7_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX7_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX7_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If
                            If i = 7 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax8 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX8_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX8_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX8_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If
                            If i = 8 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax9 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX9_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX9_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX9_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If
                            If i = 9 Then
                                If clsCommon.myLen(clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)) > 0 Then
                                    obj.tax10 = clsCommon.myCstr(gvTaxDetail.Rows(i).Cells(colTTaxAutCode).Value)
                                    obj.TAX10_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxAmt).Value), 2)
                                    obj.TAX10_Base_Amt = Math.Round(clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTBaseAmt).Value), 2)
                                    obj.TAX10_Rate = clsCommon.myCdbl(gvTaxDetail.Rows(i).Cells(colTTaxRate).Value)
                                End If
                            End If


                        Next
                    End If

                End If

                ''----------

                'If obj.SaveData(obj, IsNewEntry, Nothing) Then
                If obj.SaveData(obj, IsNewEntry) Then
                    UcAttachment1.SaveData(obj.Receipt_No)
                    '==============preeti=================
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoReceiptPayment, clsFixedParameterCode.IsAutoReceiptPayment, Nothing)) = 1 Then
                        IsFirstTimeSave = True
                        '==============added by preeti gupta against ticket no[]05/10/2016===========
                        clsRcptEntryHeader.funRcptPost(obj.Receipt_No)
                        AutoInvoice(fndCustomer.Value, ddlTransType.SelectedValue)
                    End If
                End If
                ''approval work 11/02/2020
                If clsCommon.CompairString(ddlTransType.SelectedValue, "A") <> CompairStringResult.Equal Then
                    Dim xNewDesc As String = ""
                    xNewDesc = "Party Name : " + obj.Customer_Name
                    ''=====================capex cond==============

                    xNewDesc = xNewDesc + Environment.NewLine + "Description : " + obj.Entry_Desc
                    clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, obj.Receipt_No, obj.Receipt_Date, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(obj.Entry_Desc), clsCommon.myCdbl(obj.Receipt_Amount), 0, "")
                End If
                '================================================================

                funFillDetails(obj.Receipt_No, NavigatorType.Current)

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoReceiptPayment, clsFixedParameterCode.IsAutoReceiptPayment, Nothing)) = 1 Then
                    If clsCommon.myLen(obj.Receipt_No) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If
                    btnSave.Enabled = False

                End If


                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub

    Public Sub deletedata()
        Try
            Dim Reason As String = ""
            If fndRcptNo.Value <> String.Empty Then
                If myMessages.deleteConfirm() Then
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
                    If CheckNegativeBankBalanceonDelete() Then
                        clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(fndRcptNo.Value))
                        If clsRcptEntryHeader.fundelete(fndRcptNo.Value) Then
                            saveCancelLog(Reason, "Delete", Nothing)
                            myMessages.delete()
                            funReset()
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndRcptNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postdata()
    End Sub

    Public Sub postdata()

        If btnSave.Text = "Update" And fndRcptNo.Value <> "" Then
            If myMessages.postConfirm = True Then

                Try
                    isFlag = True
                    If savedata(True) = False Then
                        Exit Sub
                    End If
                    If clsRcptEntryHeader.funRcptPost(fndRcptNo.Value) = True Then
                        funFillDetails(fndRcptNo.Value, NavigatorType.Current)
                        If common.clsCommon.MyMessageBoxShow(" Record Posted Successfully. Do You Want To Print Receipt Voucher ?", "Receipt Entry", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            Dim arrreceipt As New ArrayList()
                            If fndRcptNo.Value <> "" Then
                                PrintData()
                                'arrreceipt.Add(fndRcptNo.Value)
                                'Frmreceiptvoucher2.PrintData(Nothing, Nothing, Nothing, arrreceipt, Nothing, Nothing, Nothing, Nothing)
                            End If
                        End If
                        'funReset()
                    End If
                Catch ex As Exception
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
                Finally
                    isFlag = False
                End Try
            End If
        End If
        IsNewEntry = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ChequeNo = txtChkNo.Text
        ChequeDate = dtCheque.Value
        DocNo = fndRcptNo.Value
        Amount = clsCommon.myCdbl(txtRcptAmt.Text)
        EntryDesc = txtEntrDesc.Text
        closeform()
    End Sub

    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            inSideLoadData = True
            If clsCommon.myLen(fndCustomer.Value) > 0 Then
                If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal AndAlso chkSecurityDposit.Checked = False) Or clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
                    IsGobtnClicked = True
                    funFillGrid(fndCustomer.Value)
                    IsGobtnClicked = False
                    txtRcptAmt.Text = "0.00"
                    '' Anubhooti 29-Oct-2014
                    '' Anubhooti 02-Dec-2014 (No need to consider outstanding)
                    'If clsCommon.myCdbl(lblOutstanding.Text) <> 0 AndAlso common.clsCommon.MyMessageBoxShow("Outstanding of customer is (" & lblOutstanding.Text & "). Do You Want To consider it ?", "Receipt", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '    txtUnApplAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(txtUnApplAmt.Text) - clsCommon.myCdbl(lblOutstanding.Text))
                    '    AutoApplyAmt(txtUnApplAmt.Text)
                    'Else
                    AutoApplyAmt(clsCommon.myCdbl(txtUnApplAmt.Text))
                    ' End If
                End If
            End If
            If dgvReceipt.Rows.Count > 0 Then
                btnAdjment.Enabled = True
            End If
            inSideLoadData = False
            'IsNewEntry = True
            dgvReceipt.Select()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            inSideLoadData = False
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
        ValidateLength()
        ApplyReadOptions()
        IsNewEntry = True
        If clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
            btnGo.Enabled = True
        Else
            btnGo.Enabled = False
        End If
        dtRcpt.Enabled = True
    End Sub

#End Region

#Region "Finder"

    Private Sub fndRcptNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRcptNo._MYValidating
        'Dim Qry As String = " SELECT     TSPL_BANK_MASTER.DESCRIPTION AS Bank, TSPL_RECEIPT_HEADER.Receipt_No AS ReceiptNo, TSPL_RECEIPT_HEADER.Receipt_Date AS [Receipt Date], Receipt_Amount as [Amount] , (SELECT     CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type = 'R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type = 'M' THEN 'Miscellaneous' WHEN TSPL_RECEIPT_HEADER.Receipt_Type = 'U' THEN 'UnApplied Cash' WHEN TSPL_RECEIPT_HEADER.Receipt_Type = 'A' THEN 'Apply Document' WHEN  TSPL_RECEIPT_HEADER.Receipt_Type = 'P' THEN 'PrePayemnt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type = 'O' THEN 'OnAccount' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' Then 'Refund' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='S' Then 'Misc Refund' END AS Expr1) AS Type, TSPL_RECEIPT_HEADER.Cust_Code AS [Customer No], TSPL_RECEIPT_HEADER.Customer_Name AS [Customer Name], (SELECT     CASE WHEN Posted = 'Y' THEN 'Posted' ELSE 'Open' END AS Expr1) AS Status, TSPL_RECEIPT_HEADER.Payer AS [Remit To] FROM  TSPL_RECEIPT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE"
        Dim Qry As String = "select TSPL_BANK_MASTER.DESCRIPTION AS BANK, TSPL_RECEIPT_HEADER.Receipt_No as [ReceiptNo] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Date,103) as [Receipt Date] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Post_Date,103) as [Receipt Post Date] ,TSPL_RECEIPT_HEADER.Entry_Desc as [Entry Desc] ,TSPL_RECEIPT_HEADER.Bank_Code as [Bank Code] ,TSPL_RECEIPT_HEADER.Receipt_Type as [Receipt Type] ,TSPL_RECEIPT_HEADER.Cust_Code as [Cust Code] ,TSPL_RECEIPT_HEADER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name] ,TSPL_RECEIPT_HEADER.Reference as [Reference] ,TSPL_RECEIPT_HEADER.Narration as [Narration] ,TSPL_RECEIPT_HEADER.Payment_Code as [Payment Code] ,TSPL_RECEIPT_HEADER.Cheque_No as [Cheque No] ,TSPL_RECEIPT_HEADER.Cheque_Date as [Cheque Date] ,TSPL_RECEIPT_HEADER.Receipt_Amount as [Receipt Amount] ,TSPL_RECEIPT_HEADER.Cust_Account as [Cust Account] ,TSPL_RECEIPT_HEADER.Apply_By as [Apply By] ,TSPL_RECEIPT_HEADER.Apply_To as [Apply To] ,TSPL_RECEIPT_HEADER.Posted as [Posted] ,TSPL_RECEIPT_HEADER.Document_No as [Document No] ,TSPL_RECEIPT_HEADER.Payer as [Payer] ,TSPL_RECEIPT_HEADER.QuickEntryNo as [Quickentryno] ,TSPL_RECEIPT_HEADER.SecurityDeposit as [Securitydeposit] ,TSPL_RECEIPT_HEADER.Salesman_Code as [Salesman Code] ,TSPL_RECEIPT_HEADER.Salesman_Name as [Salesman Name] ,TSPL_RECEIPT_HEADER.Loadout_No as [Loadout No] ,TSPL_RECEIPT_HEADER.Cheque_From as [Cheque From] ,TSPL_RECEIPT_HEADER.CURRENCY_CODE as [Currency Code] ,TSPL_RECEIPT_HEADER.ConvRate as [Convrate] ,TSPL_RECEIPT_HEADER.ApplicableFrom as [Applicablefrom] ,TSPL_RECEIPT_HEADER.CFormRecd as [Cformrecd] ,TSPL_RECEIPT_HEADER.CForm_InvoiceNo as [Cform Invoiceno] ,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE as [Base Currency Code] ,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY as [Received Amount Base Currency] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as [Exchange Loss Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as [Exchange Gain Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] ,TSPL_RECEIPT_HEADER.ConvRateOld as [Convrateold] ,TSPL_RECEIPT_HEADER.PROJECT_ID as [Project Id] ,TSPL_RECEIPT_HEADER.IsParentCust as [Isparentcust] ,TSPL_RECEIPT_HEADER.CHECK_PRINT as [Check Print] ,TSPL_RECEIPT_HEADER.CHECK_CODE as [Check Code] ,TSPL_RECEIPT_HEADER.AUTO_GEN_BT_ENTRY as [Auto Gen Bt Entry] ,TSPL_RECEIPT_HEADER.TO_BANK_CODE as [To Bank Code] ,TSPL_RECEIPT_HEADER.Transfer_No as [Transfer No] ,TSPL_RECEIPT_HEADER.From_Branch as [From Branch] ,TSPL_RECEIPT_HEADER.memorandum_amt as [Memorandum Amt] ,TSPL_RECEIPT_HEADER.Applied_Receipt as [Applied Receipt],TSPL_RECEIPT_HEADER.SaleOrderNo,isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS,'') as [Delivery Order]  From TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code "
        '' Anubhooti 13-Mar-2015 (Fetch Alies Name On Vendor Finder)
        Qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code "

        If isCustomerFinderLocationWiseARReceipt Then
            Qry += " left outer join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.customer_code=tspl_customer_master.cust_code "
        End If

        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""

        strwherecls = Xtra.CustomerPermission()
        'If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
        '    strwherecls = objCommonVar.strCurrUserCustomers
        'Else
        '    strwherecls = Xtra.CustomerPermission()
        'End If
        '-----------------------------------------------------
        '----------Added By--Pankaj Kumar-----For GL Security-----
        Dim Arrloc As New ArrayList
        Dim ArrAcc As New ArrayList
        clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
        Dim WhrCls As String = ""
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = ""
        Else
            WhrCls = " (substring(TSPL_BANK_MASTER.BANKACC,(len(TSPL_BANK_MASTER.BANKACC)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_BANK_MASTER.BANKACC IN (" + clsCommon.GetMulcallString(ArrAcc) + "))"
        End If
        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        If clsCommon.myLen(strwherecls) > 0 Then
            If clsCommon.myLen(WhrCls) > 0 Then
                WhrCls += " and TSPL_RECEIPT_HEADER.Cust_Code in (" + strwherecls + ")"
            Else
                WhrCls = " TSPL_RECEIPT_HEADER.Cust_Code in (" + strwherecls + ")"
            End If

        End If
        '-------------------------Code Ends Here------------------
        'If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
        '    If clsCommon.myLen(WhrCls) > 0 Then
        '        WhrCls += " and TSPL_RECEIPT_HEADER.Location_GL_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        '    Else
        '        WhrCls += " TSPL_RECEIPT_HEADER.Location_GL_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        '    End If
        'End If
        If isCustomerFinderLocationWiseARReceipt Then
            If clsCommon.myLen(WhrCls) > 0 Then
                WhrCls += " and TSPL_CUSTOMER_LOCATION_MAPPING.location_code in (select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "') "
            Else
                WhrCls += " TSPL_CUSTOMER_LOCATION_MAPPING.location_code in (select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "') "
            End If
        End If
        fndRcptNo.Value = clsCommon.ShowSelectForm("BankSelectrid", Qry, "ReceiptNo", WhrCls, fndRcptNo.Value, " Convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) desc", isButtonClicked, "TSPL_RECEIPT_HEADER.Receipt_Date")
        funFillDetails(fndRcptNo.Value, NavigatorType.Current)
    End Sub

    Private Sub fndRcptNo__MYNavigator_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndRcptNo._MYNavigator
        Try
            ''-------richa 12/08/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            Dim strcondition As String = ""
            strwherecls = Xtra.CustomerPermission()
            'If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            '    strwherecls = objCommonVar.strCurrUserCustomers
            'Else
            '    strwherecls = Xtra.CustomerPermission()
            'End If
            If clsCommon.myLen(strwherecls) > 0 Then
                strcondition = " and TSPL_RECEIPT_HEADER.Cust_Code in (" + strwherecls + ")"
            End If
            '-----------------------------------------------------
            'Dim qst As String = "select count(*) from TSPL_RECEIPT_HEADER where Receipt_No='" + fndRcptNo.Value + "'"
            Dim qst As String = "select count(*) from TSPL_RECEIPT_HEADER where Receipt_No='" + fndRcptNo.Value + "' " + strcondition + ""
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                fndRcptNo.MyReadOnly = False
            Else
                fndRcptNo.MyReadOnly = True
            End If
            funFillDetails(fndRcptNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndRcptNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndRcptNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankCode._MYValidating
        Dim strWhrcls As String = ""


        Dim Qry As String
        '' Anubhooti 23-July-2014  (BM00000003301 :Added Else condition(Qry))
        If IsNewEntry Then
            Qry = clsERPFuncationality.glbankqueryNew(strWhrcls)
            strWhrcls += " and TSPL_bank_master.INACTIVE ='Active' "
            If isSettlementBankOnly Then
                strWhrcls += " and TSPL_BANK_MASTER.bank_type='S'"
            Else
                strWhrcls += " and TSPL_BANK_MASTER.bank_type<>'S'"
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                strWhrcls += " and TSPL_BANK_MASTER.IsSettlementBankForAD='0' "
            End If
            fndBankCode.Value = clsCommon.ShowSelectForm("RcptBanFilter", Qry, "Code", strWhrcls, fndBankCode.Value, "Code", isButtonClicked)
        Else
            Qry = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
            '" Where Bank_type = (Select Bank_type from TSPL_Bank_MASTER  Where BANK_CODE = '" & fndBankCode.Value & "' )"
            Dim Cond As String = ""
            If isSettlementBankOnly Then
                Cond = " TSPL_BANK_MASTER.bank_type='S'"
            Else
                Cond = "  Bank_type = (Select Bank_type from TSPL_Bank_MASTER  Where BANK_CODE = '" & fndBankCode.Value & "' )"
            End If
            Cond += " and TSPL_bank_master.INACTIVE ='Active' "
            If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                Cond += " and TSPL_BANK_MASTER.IsSettlementBankForAD='0' "
            End If
            fndBankCode.Value = clsCommon.ShowSelectForm("RcptBanFilter", Qry, "Code", Cond, "", "Code", isButtonClicked)
        End If


        fndPayType.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' else 'CASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + fndBankCode.Value + "' )")
        fnBankDetails()

        ''richa 14 feb,2019  TEC/05/02/19-000412 check for opening in case of Miscellenous
        'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, Nothing)
        Dim JEWithOPening As Boolean = False
        If clsCommon.myLen(clsCommon.GetDateWithStartTime(ERPStartDate)) > 0 Then
            If clsCommon.GetDateWithStartTime(dtRcpt.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
                JEWithOPening = True
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, Nothing)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "M") = CompairStringResult.Equal) And JEWithOPening = True Then
            LoadBlankGrid(ddlTransType.SelectedValue)
            gvItem.Rows.AddNew()
        End If
    End Sub

    Private Sub txtchangedpayType()
        txtChkNo.Text = ""
        txtReceivedFrom.Text = ""
        Dim strcheckcode As String = connectSql.RunScalar("select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndPayType.Value + "'")
        If Not String.IsNullOrEmpty(strcheckcode) Then
            ''richa VIJ/17/12/19-000122
            If strcheckcode.Trim() = "Cheque" Or strcheckcode.Trim() = "Credit/Debit" Then
                pnlCheque.Enabled = True
            Else
                pnlCheque.Enabled = False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), "Cash", False) = CompairStringResult.Equal And (clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal) Then
                chkAutoGeneBT.Enabled = True
                Me.txtToBank.Enabled = True
            Else
                chkAutoGeneBT.Enabled = False
                Me.txtToBank.Enabled = False
            End If
            pnlbankbranch.Enabled = Not (clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), "Cash", False) = CompairStringResult.Equal)
        End If
    End Sub

    Private Sub fndBankCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndBankCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        'Dim Qry As String = "select Cust_Code as [Code],Customer_Name as [Name],Cust_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER "
        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        'strwherecls = Xtra.CustomerPermission()
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            strwherecls = objCommonVar.strCurrUserCustomers
        Else
            strwherecls = Xtra.CustomerPermission()
        End If
        '-----------------------------------------------------
        Dim Qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as [Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name] ,TSPL_CUSTOMER_MASTER.Add1 as [Add1] ,TSPL_CUSTOMER_MASTER.Add2 as [Add2] ,TSPL_CUSTOMER_MASTER.Add3 as [Add3] ,TSPL_CUSTOMER_MASTER.Closing_Date as [Closing Date] ,TSPL_CUSTOMER_MASTER.Cust_Category_Code as [Cust Category Code] ,TSPL_CUSTOMER_MASTER.Cust_Group_Code as [Cust Group Code] ,TSPL_CUSTOMER_MASTER.Cust_Type_Code as [Cust Type Code] ,TSPL_CUSTOMER_MASTER.Route_No as [Route No] ,TSPL_CUSTOMER_MASTER.Route_Desc as [Route Desc] ,TSPL_CUSTOMER_MASTER.Price_Code as [Price Code] ,TSPL_CUSTOMER_MASTER.City_Code as [City Code] ,TSPL_CUSTOMER_MASTER.State as [State] ,TSPL_CUSTOMER_MASTER.Country as [Country] ,TSPL_CUSTOMER_MASTER.Phone1 as [Phone1] ,TSPL_CUSTOMER_MASTER.Phone2 as [Phone2] ,TSPL_CUSTOMER_MASTER.Fax as [Fax] ,TSPL_CUSTOMER_MASTER.Email as [Email] ,TSPL_CUSTOMER_MASTER.WebSite as [Website] ,TSPL_CUSTOMER_MASTER.Contact_Person_Name as [Contact Person Name] ,TSPL_CUSTOMER_MASTER.Contact_Person_Phone as [Contact Person Phone] ,TSPL_CUSTOMER_MASTER.Contact_Person_Fax as [Contact Person Fax] ,TSPL_CUSTOMER_MASTER.Contact_Person_Website as [Contact Person Website] ,TSPL_CUSTOMER_MASTER.Contact_Person_Email as [Contact Person Email] ,TSPL_CUSTOMER_MASTER.Terms_Code as [Terms Code] ,TSPL_CUSTOMER_MASTER.Cust_Account as [Cust Account] ,TSPL_CUSTOMER_MASTER.Tax_Group as [Tax Group] ,TSPL_CUSTOMER_MASTER.TAX1 as [Tax1] ,TSPL_CUSTOMER_MASTER.TAX1_Rate as [Tax1 Rate] ,TSPL_CUSTOMER_MASTER.TAX2 as [Tax2] ,TSPL_CUSTOMER_MASTER.TAX2_Rate as [Tax2 Rate] ,TSPL_CUSTOMER_MASTER.TAX3 as [Tax3] ,TSPL_CUSTOMER_MASTER.TAX3_Rate as [Tax3 Rate] ,TSPL_CUSTOMER_MASTER.TAX4 as [Tax4] ,TSPL_CUSTOMER_MASTER.TAX4_Rate as [Tax4 Rate] ,TSPL_CUSTOMER_MASTER.TAX5 as [Tax5] ,TSPL_CUSTOMER_MASTER.TAX5_Rate as [Tax5 Rate] ,TSPL_CUSTOMER_MASTER.TAX6 as [Tax6] ,TSPL_CUSTOMER_MASTER.TAX6_Rate as [Tax6 Rate] ,TSPL_CUSTOMER_MASTER.TAX7 as [Tax7] ,TSPL_CUSTOMER_MASTER.TAX7_Rate as [Tax7 Rate] ,TSPL_CUSTOMER_MASTER.TAX8 as [Tax8] ,TSPL_CUSTOMER_MASTER.TAX8_Rate as [Tax8 Rate] ,TSPL_CUSTOMER_MASTER.TAX9 as [Tax9] ,TSPL_CUSTOMER_MASTER.TAX9_Rate as [Tax9 Rate] ,TSPL_CUSTOMER_MASTER.TAX10 as [Tax10] ,TSPL_CUSTOMER_MASTER.TAX10_Rate as [Tax10 Rate] ,TSPL_CUSTOMER_MASTER.Payment_Code as [Payment Code] ,TSPL_CUSTOMER_MASTER.Service_Tax_No as [Service Tax No] ,TSPL_CUSTOMER_MASTER.Tin_No as [Tin No] ,TSPL_CUSTOMER_MASTER.Lst_No as [Lst No] ,TSPL_CUSTOMER_MASTER.Form_Type as [Form Type] ,TSPL_CUSTOMER_MASTER.Channel_Code as [Channel Code] ,TSPL_CUSTOMER_MASTER.Channel_Desc as [Channel Desc] ,TSPL_CUSTOMER_MASTER.Status as [Status] ,TSPL_CUSTOMER_MASTER.OnHold as [Onhold] ,TSPL_CUSTOMER_MASTER.Remarks1 as [Remarks1] ,TSPL_CUSTOMER_MASTER.Remarks2 as [Remarks2] ,TSPL_CUSTOMER_MASTER.Additional1 as [Additional1] ,TSPL_CUSTOMER_MASTER.Additional2 as [Additional2] ,TSPL_CUSTOMER_MASTER.Additional3 as [Additional3] ,TSPL_CUSTOMER_MASTER.Salesman_Code as [Salesman Code] ,TSPL_CUSTOMER_MASTER.Salesman_Desc as [Salesman Desc] ,TSPL_CUSTOMER_MASTER.Visi_Id as [Visi Id] ,TSPL_CUSTOMER_MASTER.Visi_Desc as [Visi Desc] ,TSPL_CUSTOMER_MASTER.OutLet_Commossion as [Outlet Commossion] ,TSPL_CUSTOMER_MASTER.Balance_ToDate as [Balance Todate] ,TSPL_CUSTOMER_MASTER.Credit_Limit as [Credit Limit] ,TSPL_CUSTOMER_MASTER.Created_By as [Created By] ,TSPL_CUSTOMER_MASTER.Created_Date as [Created Date] ,TSPL_CUSTOMER_MASTER.Modify_By as [Modify By] ,TSPL_CUSTOMER_MASTER.Modify_Date as [Modify Date] ,TSPL_CUSTOMER_MASTER.Comp_Code as [Comp Code] ,TSPL_CUSTOMER_MASTER.Route_Group as [Route Group] ,TSPL_CUSTOMER_MASTER.CST as [Cst] ,TSPL_CUSTOMER_MASTER.ECC as [Ecc] ,TSPL_CUSTOMER_MASTER.Range as [Range] ,TSPL_CUSTOMER_MASTER.Collectorate as [Collectorate] ,TSPL_CUSTOMER_MASTER.PAN as [Pan] ,TSPL_CUSTOMER_MASTER.Division as [Division] ,TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Customer No] ,TSPL_CUSTOMER_MASTER.Customer_Class as [Customer Class] ,TSPL_CUSTOMER_MASTER.Credit_Customer as [Credit Customer] ,TSPL_CUSTOMER_MASTER.LastInvoice_No as [Lastinvoice No] ,TSPL_CUSTOMER_MASTER.LastInvoice_Date as [Lastinvoice Date] ,TSPL_CUSTOMER_MASTER.price_CodeNon as [Price Codenon] ,TSPL_CUSTOMER_MASTER.Inter_Branch as [Inter Branch] ,TSPL_CUSTOMER_MASTER.TRANSACTION_TYPE as [Transaction Type] ,TSPL_CUSTOMER_MASTER.Credit_Limit_Alert_Type as [Credit Limit Alert Type] ,TSPL_CUSTOMER_MASTER.PIN_Code as [Pin Code] ,TSPL_CUSTOMER_MASTER.Cust_DOB as [Cust Dob] ,TSPL_CUSTOMER_MASTER.Cust_Spouse_DOB as [Cust Spouse Dob] ,TSPL_CUSTOMER_MASTER.Anniversary_Date as [Anniversary Date] ,TSPL_CUSTOMER_MASTER.Gender as [Gender] ,TSPL_CUSTOMER_MASTER.Occation as [Occation] ,TSPL_CUSTOMER_MASTER.Agg_Made_Date as [Agg Made Date] ,TSPL_CUSTOMER_MASTER.Agg_Close_Date as [Agg Close Date] ,TSPL_CUSTOMER_MASTER.CURRENCY_CODE as [Currency Code] ,TSPL_CUSTOMER_MASTER.Parent_Customer_YN as [Parent Customer Yn] ,TSPL_CUSTOMER_MASTER.Service_Dealer_Code as [Service Dealer Code] ,TSPL_CUSTOMER_MASTER.TDM_Code as [Tdm Code] ,TSPL_CUSTOMER_MASTER.Distributor_Code as [Distributor Code] ,TSPL_CUSTOMER_MASTER.IsDistributor as [Isdistributor] ,TSPL_CUSTOMER_MASTER.Price_Group_Code as [Price Group Code]  From TSPL_CUSTOMER_MASTER "

        If isCustomerFinderLocationWiseARReceipt Then
            Qry += " left outer join TSPL_CUSTOMER_LOCATION_MAPPING on TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code "
        End If

        Dim wherecls As String = ""
        If chkDCS.Checked Then
            wherecls = "CUSTOMER_FORM_TYPE = 'VSP' "
        Else
            wherecls = "Status ='N' AND OnHold='N' "
        End If
        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        If clsCommon.myLen(strwherecls) > 0 Then
            wherecls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
        End If
        If isCustomerFinderLocationWiseARReceipt Then
            wherecls += " and TSPL_CUSTOMER_LOCATION_MAPPING .Location_Code in (select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "') "
        End If
        '-----------------------------------------------------

        fndCustomer.Value = clsCommon.ShowSelectForm("CustomFterfnd", Qry, "Code", wherecls, fndCustomer.Value, "Code", isButtonClicked)
        IsTransferVisible()
        ''richa 29/08/2014
        If fndCustomer.Value = "" Then
            FndSalesOrder.Value = ""
        End If
        ''''------------------------------
        Customer_Event()
        SetMultiCurrencyVisibility()
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoReceiptPayment, clsFixedParameterCode.IsAutoReceiptPayment, Nothing)) = 1 Then
            AutoInvoice(fndCustomer.Value, ddlTransType.SelectedValue)
        End If
        dtRcpt.Enabled = False
        '' Anubhooti 29-Oct-2014 BM00000003904
        '' Anubhooti 02-Dec-2014 (No need to consider outstanding)
        'FillVendorOutstanding(fndCustomer.Value)
    End Sub

    Private Sub IsTransferVisible()
        Try
            Dim check As Integer = 0
            check = clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where isnull(csa_type,'N')='Y' and cust_code='" + fndCustomer.Value + "'")

            If check > 0 AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                MyLabel22.Visible = True
                txttransfer_no.Visible = True
            Else
                MyLabel22.Visible = False
                txttransfer_no.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function fnCustomer(ByVal strCustId As String) As String
        Dim strName As String
        strName = ""
        Try
            strQuery = "select Cust_Code as [Customer No],Customer_Name as [Name],Cust_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER where cust_code='" + strCustId + "'"
            Dim myDr As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If myDr IsNot Nothing AndAlso myDr.Rows.Count > 0 Then
                For Each tdr As DataRow In myDr.Rows
                    strName = Convert.ToString(tdr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        End Try
        Return strName
    End Function
    Private Sub Customer_Event()
        Me.txtCusName.Text = fnCustomer(Me.fndCustomer.Value)
    End Sub

    Private Sub fndPayType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPayType._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + fndBankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + fndBankCode.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", fndPayType.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", fndPayType.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS','Credit/Debit','IMPS','IFT')", fndPayType.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "S" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Cheque' or PAYMENT_TYPE = 'Cash'", fndPayType.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector5", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", fndPayType.Value, "PaymentMode", isButtonClicked)
            End If
        End If
        fnBankDetails()
        txtchangedpayType()
    End Sub


    Private Function funUnApplAmt(ByVal strDoc As String) As String
        Dim strAmt As String = ""
        Try
            strQuery = "select Balance_Amt  from TSPL_RECEIPT_HEADER where Receipt_No ='" + strDoc + "'"
            Dim myDr As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If myDr IsNot Nothing AndAlso myDr.Rows.Count > 0 Then
                For Each dr As DataRow In myDr.Rows
                    strAmt = Convert.ToString(dr(0).ToString().Trim())
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        End Try
        Return strAmt
    End Function

    Private Sub fnBankDetails()
        Dim strType As String
        strType = ""
        Try
            strQuery = "select BANK_CODE as [Bank Code], DESCRIPTION as [Bank Name] ,Bank_Type  from TSPL_BANK_MASTER where bank_code='" + fndBankCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each myDr As DataRow In dt.Rows
                    strType = Convert.ToString(myDr(2).ToString().Trim())
                    '---06/11/2012--Added by Pankaj Kumar-------------
                    If clsCommon.CompairString(myDr("Bank_type"), "C") = CompairStringResult.Equal Then
                        dtRcpt.Checked = True
                        pnlbankbranch.Enabled = False
                    Else
                        dtRcpt.Checked = False
                        pnlbankbranch.Enabled = True
                    End If
                    '-------------------------------------------------
                    If strType = "C" Or strType = "O" Then
                        pnlCheque.Enabled = False
                    ElseIf strType = "B" Then
                        pnlCheque.Enabled = True
                    Else
                        txtchangedpayType()
                    End If
                Next
                txtchangedpayType()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        End Try
    End Sub

#End Region

    Private Sub SetUserMgmtNew()

        Me.Form_ID = clsUserMgtCode.ReceiptEntry
        MyBase.SetUserMgmt(clsUserMgtCode.ReceiptEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        RadMenu1.Visible = MyBase.isExport
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub


#Region "AutoNumber"

    Public Function fnAutoGenerateNo() As String

        Dim Maxvlu As String
        Dim NxtMaxNo As Int32
        strQuery = "SELECT MAX(Receipt_No) as MaxValue from TSPL_RECEIPT_HEADER  where Receipt_No like '%RCPT%' "
        myDs = connectSql.RunSQLReturnDS(strQuery)
        If myDs.Tables(0).Rows.Count > 0 Then
            If myDs.Tables(0).Rows(0)(0).ToString <> "" Then
                Maxvlu = myDs.Tables(0).Rows(0)(0).ToString()
                Maxvlu = Maxvlu.Remove(0, 4)
                NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
                NxtMaxNo = NxtMaxNo + 1
                Dim strCount As String
                strCount = NxtMaxNo.ToString()
                If strCount.Length = 1 Then
                    Maxvlu = "RCPT" & "000" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 2 Then
                    Maxvlu = "RCPT" & "00" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 3 Then
                    Maxvlu = "RCPT" & "0" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 4 Then
                    Maxvlu = "RCPT" & NxtMaxNo.ToString()
                End If
                Return Maxvlu
            Else
                Maxvlu = "RCPT0001"
                Return Maxvlu
            End If
        Else
            Maxvlu = "RCPT0001"
            Return Maxvlu
        End If
        Return Maxvlu
    End Function

#End Region

#Region "Function"

    Private Sub setGridFocus()
        If dgvReceipt.Rows.Count > 0 Then
            Dim intCurrRow As Integer = dgvReceipt.CurrentRow.Index
            Dim IntCurrColumn As Integer = dgvReceipt.CurrentColumn.Index
            If dgvReceipt.Rows.Count = intCurrRow And dgvReceipt.Columns.Count = IntCurrColumn Then
                btnSave.Focus()
            End If
        End If

    End Sub
    Public Function BalanceAmount_invoice(ByVal strCustCode As String, ByVal strDocNo As String) As Double
        Try
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = ""
            Else
                WhrCls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            strQuery = " select [Balance Amount] from (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
            " case when type ='Refund'  then [Doc Total] else [Balance Amount] end -((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No] and TSPL_RECEIPT_DETAIL.Receipt_No <>'" & clsCommon.myCstr(fndRcptNo.Value) & "')+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
            " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
            " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
            "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code  from (" &
            " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
            " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
            " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
            " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
            " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
            " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0)) as [Balance Amount]  " + Environment.NewLine +
            " , '0.00' as [Apply_Amt], " &
            " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtRcpt.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' " &
            " UNION All " &
            " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
            " (Receipt_Amount " &
            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
            " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
            " ) as xxx " &
            " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" &
            ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]  =TSPL_Customer_Invoice_Head.Document_No  WHERE [Balance Amount]<>0 AND [Invoice Date] <='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "'  and [Invoice No] <>'" & txtDocumentNo.Value & "' " &
            " and    ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' " &
            " and xxx.[Invoice No]  ='" & strDocNo & "'  "


            Dim dblBalanceAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQuery, Nothing))
            Return dblBalanceAmt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Sub funFillGrid(ByVal strCustCode As String)
        Try
            LoadBlankGrid(ddlTransType.SelectedValue)
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = ""
            Else
                WhrCls = " and location in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            ''remove securit deposit type of refunds from query i.e. only those refunds will be applied which are of non security deposit type 12 Nov,2019 ERO/07/11/19-001090

            strQuery = " Select * from  (select Apply,Type, SaleInvoice as DocNo,[Invoice No],[Invoice Date],[Doc Total]," &
            " [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Balance Amount] ,Apply_Amt,Tag, Due_Date, " &
            " (Select Adjustment_No from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjNo," &
            " (Select Adjustment_Amount from (Select ROW_NUMBER() Over (Order BY Adjustment_Date Desc) as RowNo, Adjustment_No, Adjustment_Amount, Adjustment_Date from TSPL_RECEIPT_ADJUSTMENT_HEADER WHERE Doc_No=SaleInvoice) XXX WHERE RowNo=1) as AdjAmt, " &
            "  EmptyTotal ,case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate,(case when TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N' then TSPL_CUSTOMER_MASTER.Cust_Code else '' end) as Child_Customer_Code  from (" &
            " select 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type], " &
            " Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice," &
            " Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," + Environment.NewLine +
            " (TSPL_Customer_Invoice_Head.Document_Total " + Environment.NewLine +
            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " + Environment.NewLine +
            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL_REFUND inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_DETAIL_REFUND.Receipt_No =TSPL_RECEIPT_HEADER.Receipt_No where TSPL_RECEIPT_DETAIL_REFUND.Document_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_RECEIPT_HEADER.Posted='N' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL_REFUND.Receipt_No)),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and Posted='Y' and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD " + Environment.NewLine +
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE " + Environment.NewLine +
            " left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN .Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN  " + Environment.NewLine +
            " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No " + Environment.NewLine +
            " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.Invoice_No " + Environment.NewLine +
            " where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
            " -isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_INVOICE_HEAD inner JOIN TSPL_Customer_Invoice_Head as innCRNHead  ON innCRNHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where innCRNHead.Document_No =  TSPL_Customer_Invoice_Head.Document_No  ),0)  " + Environment.NewLine +
            " +isnull((select sum(isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount ,0)) from TSPL_SD_SALE_RETURN_HEAD inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No inner JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code inner JOIN TSPL_Customer_Invoice_Head as innCRNHead ON innCRNHead.Against_MCC_Material_Sale_Return=TSPL_SD_SALE_RETURN_HEAD.Document_Code inner join TSPL_Receipt_Adjustment_Header on innCRNHead.Document_No= TSPL_Receipt_Adjustment_Header.ARInvoiceNo where  innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " + Environment.NewLine +
            " ) as [Balance Amount]  " + Environment.NewLine +
            " , '0.00' as [Apply_Amt], " &
            " Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtRcpt.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY' "

            If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
                If chkForCardSale.Checked Then
                    strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=1 "
                Else
                    strQuery += " and isnull(TSPL_Customer_Invoice_Head.isCardSale,0)=0 "
                End If
            End If

            strQuery += " UNION All " &
            " select 'No' as [Apply], 'Refund' as [Type],  Receipt_No as SaleInvoice, Receipt_No as [Invoice No],convert(date,Receipt_Date,103) as [Invoice Date] ,Receipt_Amount as [Doc Total] ," &
            " (Receipt_Amount " &
            " -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_RECEIPT_HEADER.Receipt_No  and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0) ) as [Balance Amount]  " &
            " , '0.00' as [Apply_Amt],  Cust_Code as  Cust_Code  ,'C' as [Tag], convert(date,Receipt_Date,103) as Due_Date ,  0 as EmptyTotal, ConvRate ,1 as ConvRateRevaluation from TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' and IsChkReverse ='N' and isnull(SecurityDeposit,'')='N' AND ISNULL(TSPL_RECEIPT_HEADER.Applied_Receipt ,'')='' " &
            " ) as xxx " &
            " left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + strCustCode + "' or TSPL_CUSTOMER_MASTER.Parent_Customer_No ='" + strCustCode + "' )" &
            ") XXX left outer join TSPL_Customer_Invoice_Head on XXX.[Invoice No]  =TSPL_Customer_Invoice_Head.Document_No AND TSPL_Customer_Invoice_Head.Document_Type ='C' WHERE [Invoice Date] <='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "'  and [Invoice No] <>'" & txtDocumentNo.Value & "' " &
            " and    ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' "




            If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                strQuery += " and [Balance Amount]<>0 AND   isnull(XXX.Type,'') ='Credit Note'  ORDER By [Invoice Date] "
            ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
                strQuery += " and (([Balance Amount]>0 AND isnull(XXX.Type,'') ='Debit Note') or ([Balance Amount]<>0 AND isnull(XXX.Type,'') ='Invoice'))  ORDER By [Invoice Date] "
            Else
                strQuery += " and [Balance Amount]<>0  ORDER By [Invoice Date]"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myCdbl(dt.Rows(ii)("Balance Amount")) <> 0 Then
                        dgvReceipt.Rows.AddNew()
                        dgvReceipt.CurrentRow.Cells(colApply).Value = clsCommon.myCstr(dt.Rows(ii)("Apply"))
                        dgvReceipt.CurrentRow.Cells(colDocType).Value = clsCommon.myCstr(dt.Rows(ii)("Type"))
                        dgvReceipt.CurrentRow.Cells(colSINo).Value = clsCommon.myCstr(dt.Rows(ii)("DocNo"))
                        dgvReceipt.CurrentRow.Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice No"))
                        dgvReceipt.CurrentRow.Cells(colConvRateOld).Value = clsCommon.myCdbl(dt.Rows(ii)("ConvRate"))
                        dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsCommon.myCstr(dt.Rows(ii)("Invoice Date"))
                        dgvReceipt.CurrentRow.Cells(colFilledTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                        dgvReceipt.CurrentRow.Cells(colEmptyTotal).Value = clsCommon.myCdbl(dt.Rows(ii)("EmptyTotal"))
                        dgvReceipt.CurrentRow.Cells(colOrgnlAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Doc Total"))
                        dgvReceipt.CurrentRow.Cells(colBalAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                        dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
                        dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
                        dgvReceipt.CurrentRow.Cells(colTemp).Value = clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                        dgvReceipt.CurrentRow.Cells(colTemp1).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt")) + clsCommon.myCdbl(dt.Rows(ii)("Balance Amount"))
                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Apply_Amt"))
                        dgvReceipt.CurrentRow.Cells(colAdjNo).Value = clsCommon.myCstr(dt.Rows(ii)("AdjNo"))
                        dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AdjAmt"))
                        dgvReceipt.CurrentRow.Cells(colInvisibleTag).Value = clsCommon.myCstr(dt.Rows(ii)("Tag"))
                        dgvReceipt.CurrentRow.Cells(colChild_Cust_Code).Value = clsCommon.myCstr(dt.Rows(ii)("Child_Customer_Code"))
                    End If
                Next
                dgvReceipt.CurrentRow = dgvReceipt.Rows(0)
            End If
            IsGobtnClicked = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AutoApplyAmt(ByVal tempAmt As Decimal)
        Dim ReceiptAmt As Decimal = 0
        For Each grow As GridViewRowInfo In dgvReceipt.Rows
            If tempAmt > 0 Then
                If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal Then
                    grow.Cells(colApply).Value = "Yes"
                    If clsCommon.myCdbl(grow.Cells(colBalAmt).Value) <= tempAmt Then
                        grow.Cells(colAppliedAmt).Value = clsCommon.myCdbl(grow.Cells(colBalAmt).Value)
                        grow.Cells(colBalAmt).Value = 0.0
                    ElseIf clsCommon.myCdbl(grow.Cells(colBalAmt).Value) > tempAmt Then
                        grow.Cells(colAppliedAmt).Value = tempAmt
                        grow.Cells(colBalAmt).Value = clsCommon.myCdbl(grow.Cells(colBalAmt).Value) - tempAmt
                    End If
                    If clsCommon.CompairString(grow.Cells(colDocType).Value, "Invoice") = CompairStringResult.Equal Then
                        tempAmt = tempAmt - clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                        ReceiptAmt = ReceiptAmt + clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                    End If
                End If
            Else
                Exit For
            End If
        Next
        txtRcptAmt.Text = clsCommon.myCstr(ReceiptAmt)
    End Sub

    Public Sub funFillDetails(ByVal strCode As String, ByVal navType As common.NavigatorType)
        inSideLoadData = True
        Dim DblReceiptTaxAmt1 As Double = 0
        Dim DblReceiptTaxAmt2 As Double = 0
        Dim DblReceiptTaxAm3 As Double = 0
        Dim DblReceiptTaxAmt4 As Double = 0
        Dim DblReceiptTaxAmt5 As Double = 0
        Dim DblReceiptTaxAmt6 As Double = 0
        Dim DblReceiptTaxAmt7 As Double = 0
        Dim DblReceiptTaxAmt8 As Double = 0
        Dim DblReceiptTaxAmt9 As Double = 0
        Dim DblReceiptTaxAmt10 As Double = 0


        Try
            Dim obj As New clsRcptEntryHeader()
            obj = clsRcptEntryHeader.GetData(strCode, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Receipt_No) > 0) Then
                fndBankCode.Enabled = False
                If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                    LblBookingNo.Visible = True
                    fndBookingNo.Visible = True
                    fndBookingNo.Value = obj.Booking_Code
                Else
                    LblBookingNo.Visible = False
                    fndBookingNo.Visible = False
                    fndBookingNo.Value = ""
                End If

                txttransfer_no.Value = obj.CSATransfer_No
                txtmemoamt.Text = obj.memorndmamt
                chkmemorndm.Checked = False
                If clsCommon.myCdbl(txtmemoamt.Text) > 0 Then
                    chkmemorndm.Checked = True
                End If

                dgvmiscpayment.Visible = False
                dgvReceipt.Visible = False
                ddlTransType.Enabled = False
                If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Tax_Group) > 0 Then
                    txtlocation.Enabled = False
                Else
                    txtlocation.Enabled = True
                End If
                fndBankCode.Value = obj.Bank_Code

                ddlTransType.SelectedValue = obj.Receipt_Type
                TxtForeignBankCharges.Value = obj.Foreign_Bank_Charges_Amt
                TxtBankCharges.Value = obj.Bank_Charges_Amt
                txtlocation.Value = obj.Location_GL_Code
                txtEntrDesc.Text = obj.Entry_Desc
                If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                    LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
                Else
                    LblLocDesp.Text = ""
                End If

                fndCustomer.Value = obj.Cust_Code
                txtLoadIn.Value = obj.Against_RCDF_Loadin
                txtCusName.Text = obj.Customer_Name
                txtDistr_Code.Text = obj.Distr_Code
                If clsCommon.myLen(obj.DateAndTime) > 0 Then
                    txtDataAndTimeSelection.Value = obj.DateAndTime
                    txtDataAndTimeSelection.Checked = True
                End If
                txtTapalNo.Text = clsCommon.myCstr(obj.TapalNo)
                txtsalesmanCode.Value = obj.Salesman_Code
                txtSamesmanName.Text = obj.Salesman_Name
                dtRcpt.Value = obj.Receipt_Date
                dtPost.Value = obj.Receipt_Post_Date
                fndPayType.Value = obj.Payment_Code
                txtchangedpayType() '--Hide/Unhide Cheque Detail
                txtChkNo.Text = obj.Cheque_No
                If clsCommon.myLen(obj.Cheque_No) > 0 AndAlso clsCommon.CompairString(fndPayType.Value, "Credit/Debit") <> CompairStringResult.Equal Then
                    dtCheque.Value = obj.Cheque_Date
                End If

                If obj.isCardSale = 1 Then
                    chkForCardSale.Checked = True
                Else
                    chkForCardSale.Checked = False
                End If
                txtReceivedFrom.Text = obj.Cheque_From
                txtBranch.Text = obj.From_Branch
                txtreference.Text = obj.Reference
                txtNarration.Text = obj.Narration
                txtremitto.Text = obj.Payer
                txtRcptAmt.Text = clsCommon.myCstr(obj.Receipt_Amount)
                txtTotalPaymentBaseCurr.Value = clsCommon.myCdbl(obj.Receipt_Amount) * clsCommon.myCdbl(obj.ConvRate)
                FndSalesOrder.Value = clsCommon.myCstr(obj.SaleOrderNo)
                chkCForm.IsChecked = IIf(obj.CFormRecd = "Y", True, False)
                txtCFormInvNo.Value = obj.CForm_InvoiceNo
                If obj.Receipt_Type = "F" Then
                    txtUnApplAmt.Text = clsCommon.myCstr(obj.Receipt_Amount)
                Else
                    txtUnApplAmt.Text = clsCommon.myCstr(obj.UnApply_Amt)
                End If
                txtUnAppliedBal.Text = clsCommon.myCstr(obj.UnApplied_Balance)
                txtUnApplieadNo.Text = clsCommon.myCstr(obj.UnApplied_No)
                LoadBlankGrid(obj.Receipt_Type)
                btnAdjment.Enabled = False
                txtUnApplAmt.Enabled = True
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Receipt_No)
                End If
                chkParentCust.Checked = obj.IsParentCust
                chkCheckPrint.Checked = IIf(obj.CHECK_PRINT = 1, True, False)

                chkACPayee.Checked = IIf(obj.AC_Payee = 1, True, False)
                txtChkFavOf.Text = clsCommon.myCstr(obj.cheque_in_favour_of)
                If (obj.CHECK_PRINT = 1 Or clsCommon.myLen(obj.Cheque_No) > 0) Then
                    Me.btnPrintCheck.Enabled = True
                    btnVoidCheck.Enabled = True
                Else
                    Me.btnPrintCheck.Enabled = False
                    btnVoidCheck.Enabled = False
                End If
                chkAutoGeneBT.Checked = obj.AUTO_GEN_BT_ENTRY
                Me.txtToBank.Value = obj.TO_BANK_CODE
                Me.txtBTNo.Text = obj.Transfer_No
                chkSecurityDposit.Checked = IIf(obj.SecurityDeposit = "Y", True, False)
                ddlSecDepositType.SelectedValue = obj.SecurityDepositType
                chkOpening.Checked = obj.is_Opening
                If clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Receipt_Type, "S") = CompairStringResult.Equal Then
                    If obj.SecurityDeposit = "Y" Then
                        chkSecurityDposit.Checked = True
                        fndCustomer.Visible = True
                        txtDistr_Code.Visible = True
                        txtCusName.Visible = True
                        lblcustomer.Visible = True
                    Else
                        chkSecurityDposit.Checked = False
                        fndCustomer.Visible = False
                        txtDistr_Code.Visible = False
                        txtCusName.Visible = False
                        lblcustomer.Visible = False
                    End If

                    If obj.Receipt_Type = "M" OrElse obj.Receipt_Type = "S" Then
                        chkSalesmanType.Enabled = True
                        If clsCommon.CompairString(obj.Receipt_Type, "M") = CompairStringResult.Equal Then
                            If clsCommon.myLen(obj.Loadout_No) > 0 Then
                                txtLoadOutno.Value = obj.Loadout_No
                            Else
                                txtLoadOutno.Value = ""
                            End If
                        End If
                        If obj.IsSalesmanType = "Y" Then
                            chkSalesmanType.Checked = True
                            lblSalesman.Visible = True
                            txtsalesmanCode.Visible = True
                            txtSamesmanName.Visible = True
                        Else
                            chkSalesmanType.Checked = False
                            lblSalesman.Visible = False
                            txtsalesmanCode.Value = ""
                            txtsalesmanCode.Visible = False
                            txtSamesmanName.Text = ""
                            txtSamesmanName.Visible = False
                        End If
                    End If
                    For Each objTr As clsReceiptDettail In obj.ArrTr
                        dgvmiscpayment.CurrentRow.Cells(colLineNo).Value = objTr.Receipt_Line_No
                        dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value = objTr.Account_Code
                        dgvmiscpayment.CurrentRow.Cells(colAccDesc).Value = objTr.Description
                        dgvmiscpayment.CurrentRow.Cells(colAmount).Value = objTr.Applied_Amount
                        dgvmiscpayment.CurrentRow.Cells(colRemark).Value = objTr.Remarks
                        dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value = objTr.Hirerachy_Level_Code
                        dgvmiscpayment.CurrentRow.Cells(colHirerachyName).Value = objTr.Hirerachy_Level_Name
                        dgvmiscpayment.CurrentRow.Cells(colCostCenter).Value = objTr.Cost_Center_Fin_Code
                        dgvmiscpayment.CurrentRow.Cells(colCostCenterName).Value = objTr.Cost_Center_Fin_Name
                        dgvmiscpayment.Rows.AddNew()
                    Next
                    dgvmiscpayment.Visible = True
                    clsCustomFieldGrid.FillDataInGrid(obj.Receipt_No, MyBase.Form_ID, dgvmiscpayment)
                ElseIf clsCommon.CompairString(obj.Receipt_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "K") = CompairStringResult.Equal Then
                    btnAdjment.Enabled = True
                    dgvmiscpayment.Visible = False
                    dgvReceipt.Visible = True
                    If clsCommon.CompairString(obj.Receipt_Type, "A") = CompairStringResult.Equal Then
                        txtDocumentNo.Value = obj.Applied_Receipt
                        lblBalAmt.Text = clsRcptEntryHeader.GetBalance(obj.Applied_Receipt, obj.Receipt_No, Nothing)
                        txtUnApplAmt.Enabled = False
                        txtConversionRate.Enabled = False
                    Else
                        txtUnApplAmt.Enabled = True
                        txtConversionRate.Enabled = True
                    End If
                    For Each objTr As clsReceiptDettail In obj.ArrTr
                        dgvReceipt.Rows.AddNew()
                        dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes"
                        If clsCommon.CompairString(objTr.Receipt_Type, "D") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Debit Note"
                        ElseIf clsCommon.CompairString(objTr.Receipt_Type, "C") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Credit Note"
                        ElseIf clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Refund"
                        Else
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Invoice"
                        End If
                        dgvReceipt.CurrentRow.Cells(colSINo).Value = objTr.SaleInvoice
                        dgvReceipt.CurrentRow.Cells(colDocNo).Value = objTr.Document_No
                        dgvReceipt.CurrentRow.Cells(colConvRateOld).Value = objTr.ConvRateOld
                        If clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsDBFuncationality.getSingleValue(" Select Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No ='" & objTr.Document_No & "'")
                        Else
                            dgvReceipt.CurrentRow.Cells(colDocDate).Value = objTr.Document_Date
                        End If

                        dgvReceipt.CurrentRow.Cells(colFilledTotal).Value = objTr.FilledTotal
                        dgvReceipt.CurrentRow.Cells(colEmptyTotal).Value = objTr.EmptyTotal
                        dgvReceipt.CurrentRow.Cells(colOrgnlAmt).Value = objTr.Original_Amt
                        Dim BalAmt As Double = BalanceAmount_invoice(obj.Cust_Code, objTr.Document_No)
                        If clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = objTr.Pending_Balance
                        Else
                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = BalAmt
                        End If
                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = objTr.Applied_Amount
                        dgvReceipt.CurrentRow.Cells(colAdjNo).Value = objTr.Adjustment_No
                        dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Adjustment_Amount  from TSPL_Receipt_Adjustment_Header WHERE    Adjustment_No = '" + objTr.Adjustment_No + "' "))
                        dgvReceipt.CurrentRow.Cells(colTemp).Value = BalAmt + objTr.Applied_Amount + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value)
                        dgvReceipt.CurrentRow.Cells(colComment).Value = objTr.Comment
                        dgvReceipt.CurrentRow.Cells(colInvisibleTag).Value = objTr.TagType
                        dgvReceipt.CurrentRow.Cells(colChild_Cust_Code).Value = clsCommon.myCstr(objTr.Child_Cust_Code)
                    Next
                    EnableDisableSettlement()
                    clsCustomFieldGrid.FillDataInGrid(obj.Receipt_No, MyBase.Form_ID, dgvReceipt)

                    If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                        fndPayType.Enabled = False
                        fndBankCode.Enabled = False
                    Else
                        fndPayType.Enabled = True
                        fndBankCode.Enabled = True
                    End If
                ElseIf RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                    For Each objTr As clsReceiptDetail_Refund In obj.ArrTrRefund
                        btnAdjment.Enabled = True
                        dgvmiscpayment.Visible = False
                        dgvReceipt.Visible = True
                        txtUnApplAmt.Enabled = True
                        txtConversionRate.Enabled = True

                        dgvReceipt.Rows.AddNew()
                        dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes"
                        If clsCommon.CompairString(objTr.Receipt_Type, "D") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Debit Note"
                        ElseIf clsCommon.CompairString(objTr.Receipt_Type, "C") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Credit Note"
                        ElseIf clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Refund"
                        Else
                            dgvReceipt.CurrentRow.Cells(colDocType).Value = "Invoice"
                        End If
                        dgvReceipt.CurrentRow.Cells(colSINo).Value = objTr.SaleInvoice
                        dgvReceipt.CurrentRow.Cells(colDocNo).Value = objTr.Document_No
                        dgvReceipt.CurrentRow.Cells(colConvRateOld).Value = objTr.ConvRateOld
                        If clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colDocDate).Value = clsDBFuncationality.getSingleValue(" Select Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No ='" & objTr.Document_No & "'")
                        Else
                            dgvReceipt.CurrentRow.Cells(colDocDate).Value = objTr.Document_Date
                        End If

                        dgvReceipt.CurrentRow.Cells(colFilledTotal).Value = objTr.FilledTotal
                        dgvReceipt.CurrentRow.Cells(colEmptyTotal).Value = objTr.EmptyTotal
                        dgvReceipt.CurrentRow.Cells(colOrgnlAmt).Value = objTr.Original_Amt
                        Dim BalAmt As Double = BalanceAmount_invoice(obj.Cust_Code, objTr.Document_No)
                        If clsCommon.CompairString(objTr.Receipt_Type, "F") = CompairStringResult.Equal Then
                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = objTr.Pending_Balance
                        Else
                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = BalAmt
                        End If
                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = objTr.Applied_Amount
                        dgvReceipt.CurrentRow.Cells(colAdjNo).Value = objTr.Adjustment_No
                        dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Adjustment_Amount  from TSPL_Receipt_Adjustment_Header WHERE    Adjustment_No = '" + objTr.Adjustment_No + "' "))
                        dgvReceipt.CurrentRow.Cells(colTemp).Value = BalAmt + objTr.Applied_Amount + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value)
                        dgvReceipt.CurrentRow.Cells(colComment).Value = objTr.Comment
                        dgvReceipt.CurrentRow.Cells(colInvisibleTag).Value = objTr.TagType
                        dgvReceipt.CurrentRow.Cells(colChild_Cust_Code).Value = clsCommon.myCstr(objTr.Child_Cust_Code)

                        EnableDisableSettlement()
                        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                            fndPayType.Enabled = False
                            fndBankCode.Enabled = False
                        Else
                            fndPayType.Enabled = True
                            fndBankCode.Enabled = True
                        End If
                    Next

                End If

                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                    Me.txtApplicableFrom.Text = clsCommon.myCstr(clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
                End If
                fndRcptNo.Value = obj.Receipt_No
                UcAttachment1.LoadData(obj.Receipt_No)
                btnSave.Text = "Update"
                IsNewEntry = False
                If obj.Posted = "Y" Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnGo.Enabled = False
                    dgvReceipt.ReadOnly = True
                    UsLock1.Status = ERPTransactionStatus.Approved
                    chkSecurityDposit.Enabled = False
                ElseIf obj.Posted = "N" Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                    If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                        chkSecurityDposit.Enabled = True
                    Else
                        chkSecurityDposit.Enabled = False
                    End If
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnGo.Enabled = False
                    dgvReceipt.ReadOnly = False
                End If
                If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                    txtDocument_ForAdvanceDoc.Value = obj.Applied_Receipt
                End If

                GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
                If GSTStatus Then
                    LoadBlankGridTax()
                    LoadBlankGridDOItemDetail()
                    LblDOTotalAmount.Text = obj.Delivery_order_Amount
                    lblDOTotalAdditionalCharge.Text = obj.DO_Total_Add_Amount
                    lblDOTotalTaxAmt.Text = obj.Tax_Amount_Advance
                    txtTaxGroup.Value = obj.Tax_Group
                    txtDONo.Value = obj.Delivery_Code_PS
                    lblDO_Location.Text = obj.SO_Location_Code
                    lblTaxGrpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code ='" & clsCommon.myCstr(txtTaxGroup.Value) & "'"))
                    lblReceiptThroughSO.Text = obj.ReceiptAgainstSO_DO
                    If clsCommon.myLen(txtDONo.Value) > 0 Then
                        txtDONo.Enabled = True
                        txtTaxGroup.Enabled = False
                    Else
                        txtDONo.Enabled = False
                        txtTaxGroup.Enabled = True
                    End If
                    For Each objTr As clsReceiptDetailGST In obj.ArrTrGST
                        gvItem.Rows.AddNew()
                        gvItem.CurrentRow.Cells(AdcolDocument_Code).Value = objTr.Document_Code
                        gvItem.CurrentRow.Cells(AdcolLine_No).Value = objTr.Line_No
                        gvItem.CurrentRow.Cells(AdcolRow_Type).Value = objTr.Row_Type
                        gvItem.CurrentRow.Cells(AdcolItem_Code).Value = objTr.Item_Code
                        gvItem.CurrentRow.Cells(adcolIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
                        gvItem.CurrentRow.Cells(adcolIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItem.CurrentRow.Cells(AdcolQty).Value = objTr.Qty
                        gvItem.CurrentRow.Cells(AdcolBalance_Qty).Value = objTr.Balance_Qty
                        gvItem.CurrentRow.Cells(AdcolItem_Cost).Value = objTr.Item_Cost
                        gvItem.CurrentRow.Cells(AdcolUnit_code).Value = objTr.Unit_code
                        gvItem.CurrentRow.Cells(AdcolTAX1).Value = objTr.TAX1
                        gvItem.CurrentRow.Cells(AdcolTAX1_Amt).Value = objTr.TAX1_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX1_Base_Amt).Value = objTr.TAX1_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX1_Rate).Value = objTr.TAX1_Rate
                        gvItem.CurrentRow.Cells(Adcoltax2).Value = objTr.tax2
                        gvItem.CurrentRow.Cells(AdcolTAX2_Base_Amt).Value = objTr.TAX2_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX2_Rate).Value = objTr.TAX2_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX2_Amt).Value = objTr.TAX2_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX3).Value = objTr.TAX3
                        gvItem.CurrentRow.Cells(AdcolTAX3_Base_Amt).Value = objTr.TAX3_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX3_Rate).Value = objTr.TAX3_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX3_Amt).Value = objTr.TAX3_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX4).Value = objTr.TAX4
                        gvItem.CurrentRow.Cells(AdcolTAX4_Base_Amt).Value = objTr.TAX4_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX4_Rate).Value = objTr.TAX4_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX4_Amt).Value = objTr.TAX4_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX5).Value = objTr.tax5
                        gvItem.CurrentRow.Cells(AdcolTAX5_Base_Amt).Value = objTr.TAX5_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX5_Rate).Value = objTr.TAX5_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX5_Amt).Value = objTr.TAX5_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX6_Base_Amt).Value = objTr.TAX6_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX6).Value = objTr.tax6
                        gvItem.CurrentRow.Cells(AdcolTAX6_Rate).Value = objTr.TAX6_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX6_Amt).Value = objTr.TAX6_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX7).Value = objTr.tax7
                        gvItem.CurrentRow.Cells(AdcolTAX7_Base_Amt).Value = objTr.TAX7_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX7_Rate).Value = objTr.TAX7_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX7_Amt).Value = objTr.TAX7_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX8).Value = objTr.tax8
                        gvItem.CurrentRow.Cells(AdcolTAX8_Base_Amt).Value = objTr.TAX8_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX8_Rate).Value = objTr.TAX8_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX8_Amt).Value = objTr.TAX8_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9).Value = objTr.tax9
                        gvItem.CurrentRow.Cells(AdcolTAX9_Base_Amt).Value = objTr.TAX9_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Amt).Value = objTr.TAX9_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Rate).Value = objTr.TAX9_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX10).Value = objTr.tax10
                        gvItem.CurrentRow.Cells(AdcolTAX10_Base_Amt).Value = objTr.TAX10_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX10_Rate).Value = objTr.TAX10_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX10_Amt).Value = objTr.TAX10_Amt
                        gvItem.CurrentRow.Cells(AdcolAmount).Value = objTr.Amount
                        gvItem.CurrentRow.Cells(AdcolDisc_Per).Value = objTr.Disc_Per
                        gvItem.CurrentRow.Cells(AdcolDisc_Amt).Value = objTr.Disc_Amt
                        gvItem.CurrentRow.Cells(AdcolAmt_Less_Discount).Value = objTr.Amt_Less_Discount
                        gvItem.CurrentRow.Cells(AdcolTotal_Tax_Amt).Value = objTr.Total_Tax_Amt
                        gvItem.CurrentRow.Cells(AdcolItem_Net_Amt).Value = objTr.Item_Net_Amt
                        gvItem.CurrentRow.Cells(AdcolMRP).Value = objTr.MRP
                        gvItem.CurrentRow.Cells(AdcolAbatement_Per).Value = objTr.Abatement_Per
                        gvItem.CurrentRow.Cells(AdcolAbatement_Amt).Value = objTr.Abatement_Amt
                        gvItem.CurrentRow.Cells(AdcolScheme_Code).Value = objTr.Scheme_Code
                        gvItem.CurrentRow.Cells(AdcolScheme_Applicable).Value = objTr.Scheme_Applicable
                        gvItem.CurrentRow.Cells(AdcolScheme_Item).Value = objTr.Scheme_Item
                        gvItem.CurrentRow.Cells(AdcolFOC_Item).Value = objTr.FOC_Item
                        gvItem.CurrentRow.Cells(AdcolItem_Tax).Value = objTr.Item_Tax
                        gvItem.CurrentRow.Cells(AdcolTotal_MRP_Amt).Value = objTr.Total_MRP_Amt
                        gvItem.CurrentRow.Cells(AdcolTotal_Basic_Amt).Value = objTr.Total_Basic_Amt
                        gvItem.CurrentRow.Cells(AdcolTotal_Disc_Amt).Value = objTr.Total_Disc_Amt
                        gvItem.CurrentRow.Cells(AdcolActualRate).Value = objTr.ActualRate
                        gvItem.CurrentRow.Cells(AdcolTotalItem_Weight).Value = objTr.TotalItem_Weight
                        gvItem.CurrentRow.Cells(AdcolConv_Factor).Value = objTr.Conv_Factor
                        gvItem.CurrentRow.Cells(AdcolLanding_Cost).Value = objTr.Landing_Cost
                        gvItem.CurrentRow.Cells(AdcolTAX1_Amt_Receipt).Value = objTr.TAX1_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX2_Amt_Receipt).Value = objTr.TAX2_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX3_Amt_Receipt).Value = objTr.TAX3_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX4_Amt_Receipt).Value = objTr.TAX4_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX5_Amt_Receipt).Value = objTr.TAX5_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX6_Amt_Receipt).Value = objTr.TAX6_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX7_Amt_Receipt).Value = objTr.TAX7_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX8_Amt_Receipt).Value = objTr.TAX8_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Amt_Receipt).Value = objTr.TAX9_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX10_Amt_Receipt).Value = objTr.TAX10_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolReceiptAdvance).Value = objTr.ReceiptAdvance
                        gvItem.CurrentRow.Cells(AdcolReceiptTotalTax).Value = objTr.ReceiptTotalTax
                        gvItem.CurrentRow.Cells(AdcolReceiptTotalAdvanceAmt).Value = objTr.ReceiptTotalAdvanceAmt


                        DblReceiptTaxAmt1 = DblReceiptTaxAmt1 + clsCommon.myCdbl(objTr.TAX1_Amt_Receipt)
                        DblReceiptTaxAmt2 = DblReceiptTaxAmt2 + clsCommon.myCdbl(objTr.TAX2_Amt_Receipt)
                        DblReceiptTaxAm3 = DblReceiptTaxAm3 + clsCommon.myCdbl(objTr.TAX3_Amt_Receipt)
                        DblReceiptTaxAmt4 = DblReceiptTaxAmt4 + clsCommon.myCdbl(objTr.TAX4_Amt_Receipt)
                        DblReceiptTaxAmt5 = DblReceiptTaxAmt5 + clsCommon.myCdbl(objTr.TAX5_Amt_Receipt)
                        DblReceiptTaxAmt6 = DblReceiptTaxAmt6 + clsCommon.myCdbl(objTr.TAX6_Amt_Receipt)
                        DblReceiptTaxAmt7 = DblReceiptTaxAmt7 + clsCommon.myCdbl(objTr.TAX7_Amt_Receipt)
                        DblReceiptTaxAmt8 = DblReceiptTaxAmt8 + clsCommon.myCdbl(objTr.TAX8_Amt_Receipt)
                        DblReceiptTaxAmt9 = DblReceiptTaxAmt9 + clsCommon.myCdbl(objTr.TAX9_Amt_Receipt)
                        DblReceiptTaxAmt10 = DblReceiptTaxAmt10 + clsCommon.myCdbl(objTr.TAX10_Amt_Receipt)

                        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0 Then
                            txtitem.Value = objTr.Item_Code
                            lblItemName.Text = clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master where Item_Code='" & objTr.Item_Code & "'")
                            CaptionItem.Visible = True
                            lblItemName.Visible = True
                            txtitem.Visible = True
                        Else
                            CaptionItem.Visible = False
                            lblItemName.Visible = False
                            txtitem.Visible = False
                        End If

                    Next
                    If clsCommon.myLen(txtDONo.Value) > 0 Then
                        For Each objTr As clsReceiptDetailGST In obj.ArrTrGST
                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX1)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX1)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX1))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX1_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax2)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax2)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax2))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX2_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX3)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX3)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX3))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX3_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX4)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX4)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX4))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX4_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax5)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax5)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax5))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX5_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax6)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax6)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax6))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX6_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax7)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax7)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax7))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX7_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax8)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax8)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax8))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX8_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax9)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax9)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax9))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX9_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax10)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax10)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax10))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX10_Rate)
                            End If
                            Exit For
                        Next
                        SetitemWiseTaxSetting(False, False)
                        CalculateTaxDetailForTaxgrid()
                    Else
                        ' If Advance is created without DO and tax group is selected '' added by priti sharma on 17.08.2016
                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX1)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX1)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX1))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX1_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX1_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX1_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax2)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax2)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax2))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX2_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX2_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX2_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX3)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX3)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX3))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX3_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX3_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX3_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX4)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX4)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX4))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX4_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX4_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX4_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax5)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax5)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax5))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX5_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX5_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX5_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax6)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax6)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax6))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX6_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX6_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX6_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax7)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax7)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax7))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX7_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX7_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX7_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax8)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax8)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax8))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX8_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX8_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX8_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax9)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax9)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax9))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX9_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX9_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX9_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax10)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax10)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax10))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX10_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX10_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX10_Base_Amt)
                        End If
                    End If


                    If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                        fndBankCode.Enabled = False
                        fndCustomer.Enabled = False
                        txtUnApplAmt.Enabled = False
                        fndPayType.Enabled = False
                        txtlocation.Enabled = False
                        txtTaxGroup.Enabled = False
                    End If
                End If

                If clsCommon.CompairString(ddlTransType.SelectedValue, "A") <> CompairStringResult.Equal Then
                    btnPost.Visible = MyBase.isPostFlag
                    If Not clsApply_Approval.Visibility_PostButtonForApproval(MyBase.Form_ID, clsCommon.myCstr(fndRcptNo.Value), clsCommon.myCdbl(obj.Receipt_Amount), 0, "") Then
                        btnPost.Visible = False
                        If UsLock1.Status = ERPTransactionStatus.Pending Then
                            UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, clsCommon.myCstr(fndRcptNo.Value), Nothing)
                        End If
                    End If
                End If


            Else
                IsNewEntry = True
                btnSave.Text = "Save"
            End If
            MyBase.ReStoreGridLayoutMain(dgvReceipt)
            MyBase.ReStoreGridLayoutMain(dgvmiscpayment)
            MyBase.ReStoreGridLayoutMain(gvItem)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        Finally
            inSideLoadData = False
        End Try
        x = 0
        IsTransferVisible()
    End Sub


    Public Sub funReset()
        txtDONo.Enabled = True
        txtTaxGroup.Enabled = True
        fndBankCode.Enabled = True
        fndPayType.Enabled = True
        chkSecurityDposit.Enabled = False
        chkSecurityDposit.Checked = False
        txttransfer_no.Value = ""
        fndBookingNo.Value = ""
        ddlSecDepositType.SelectedValue = ""
        lblSecurityDep.Visible = False
        ddlSecDepositType.Visible = False
        txtDistr_Code.Text = ""
        btnAdjment.Enabled = False
        If clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Then
            LoadBlankGrid("M")
            chkSecurityDposit.Enabled = True
            chkSecurityDposit.Checked = False
        ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
            LoadBlankGrid("R")
            btnAdjment.Enabled = True
        ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
            chkSecurityDposit.Enabled = True
            chkSecurityDposit.Checked = False
        End If
        txtTotalPaymentBaseCurr.Value = 0
        lblSecurityDep.Visible = False
        ddlSecDepositType.Visible = False
        txtmemoamt.Text = ""
        chkmemorndm.Checked = False
        fndRcptNo.Value = ""
        txtEntrDesc.Text = ""
        fndCustomer.Value = ""
        txtCusName.Text = ""
        txtLoadIn.Value = ""
        dtPost.Value = connectSql.serverDate()
        dtCheque.Value = connectSql.serverDate()
        txtChkNo.Text = ""
        txtNarration.Text = ""
        txtreference.Text = ""
        txtremitto.Text = ""
        txtLoadOutno.Value = ""
        txtUnApplieadNo.Text = ""
        txtRcptAmt.Text = ""
        txtUnApplAmt.Enabled = True
        txtUnApplAmt.Text = ""
        txtUnAppliedBal.Text = ""
        fndBankCode.Enabled = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = True
        If EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = True And clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Then
            btnGo.Enabled = True
        Else
            btnGo.Enabled = False
        End If
        dgvReceipt.ReadOnly = False
        UsLock1.Status = ERPTransactionStatus.Pending
        fnBankDetails()
        dtRcpt.Value = clsCommon.GETSERVERDATE
        ddlTransType.Enabled = True
        dtRcpt.Enabled = True
        chkSalesmanType.Checked = False
        txtsalesmanCode.Value = ""
        txtSamesmanName.Text = ""
        txtLoadOutno.Text = ""
        txtLoadOutno.Visible = False
        lblLoadOutNo.Visible = False
        chkCForm.Checked = False
        txtCFormInvNo.Value = ""
        pnlCform.Visible = False
        txtDocumentNo.Value = ""
        lblBalAmt.Text = ""
        txtConversionRate.Enabled = True
        lblOutstanding.Text = "0"
        FndSalesOrder.Value = ""
        TxtBankCharges.Value = 0
        TxtForeignBankCharges.Value = 0
        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then '-- Advance
            If objCommonVar.IsDemoERP = True Then
                pnlCform.Visible = True
            Else
                pnlCform.Visible = False
            End If
        End If
        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
            LblBookingNo.Visible = True
            fndBookingNo.Visible = True
        Else
            LblBookingNo.Visible = False
            fndBookingNo.Visible = False
        End If
        If clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
            txtUnApplAmt.Enabled = False
        Else
            txtUnApplAmt.Enabled = True
        End If
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        UcAttachment1.BlankAllControls()
        chkCheckPrint.Checked = False
        btnPrintCheck.Enabled = False
        btnPrintCheck.Enabled = False
        btnVoidCheck.Enabled = False
        chkAutoGeneBT.Checked = False
        txtToBank.Value = Nothing
        txtBTNo.Text = ""
        IsTransferVisible()
        chkOpening.Checked = False
        If isApplyBranchAccounting = True And (clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal) Then
            RadLabel18.Visible = True
            txtlocation.Visible = True
            LblLocDesp.Visible = True
            txtlocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "')"))
            If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
            Else
                LblLocDesp.Text = ""
            End If
        Else
            RadLabel18.Visible = False
            txtlocation.Visible = False
            LblLocDesp.Visible = False
            txtlocation.Value = ""
            LblLocDesp.Text = ""
        End If
        txtlocation.Enabled = True
        Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode
        Me.txtConversionRate.Text = 1

        txtDONo.Value = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        LblDOTotalAmount.Text = 0
        lblDOTotalAdditionalCharge.Text = 0
        txtDocument_ForAdvanceDoc.Value = ""
        lblDO_Location.Text = ""
        lblReceiptThroughSO.Text = ""
        lblDOTotalTaxAmt.Text = 0
        LoadBlankGridDOItemDetail()
        LoadBlankGridTax()
        IsNewEntry = True
        lblItemName.Visible = False
        txtitem.Visible = False
        CaptionItem.Visible = False
        lblItemName.Text = ""
        txtitem.Value = ""
        txtCusName.Enabled = True
        fndCustomer.Enabled = True
        txtChkFavOf.Text = ""
        chkACPayee.Checked = False
        EnableDisableIncaseofRefund()
        txtDataAndTimeSelection.Value = clsCommon.GETSERVERDATE()
        txtTapalNo.Text = ""
        txtDataAndTimeSelection.Checked = False
        chkForCardSale.Checked = False
        chkForCardSale.Enabled = False
        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
            chkForCardSale.Visible = True
        Else
            chkForCardSale.Visible = False
        End If

    End Sub

#End Region

#Region "Event"

    Dim IsCellValueChanged As Boolean = True

    Private Sub dgvReceipt_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvReceipt.CellDoubleClick
        If e.RowIndex >= 0 Then
            GridDouble_Click()
            If dgvReceipt.CurrentColumn Is dgvReceipt.Columns(colDocNo) And clsCommon.myLen(dgvReceipt.CurrentRow.Cells(colDocNo).Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, dgvReceipt.CurrentRow.Cells(colDocNo).Value)
            ElseIf dgvReceipt.CurrentColumn Is dgvReceipt.Columns(colAdjNo) Then
                Dim frm As New frmAdj()
                frm.strDocumnentNo = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colAdjNo).Value)
                frm.fndCusCode.Value = fndCustomer.Value
                txtCusName.Text = txtCusName.Text
                frm.fndDocNo.Value = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colSINo).Value)
                frm.txtDocAmt.Text = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colFilledTotal).Value)
                frm.txtBalanceAmt.Text = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colBalAmt).Value)
                frm.isFromSettlement = True
                frm.dtLoadOut = dtRcpt.Value
                frm.ShowDialog()
            End If
        End If
    End Sub



    Private Sub dgvReceipt_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvReceipt.CellEditorInitialized
        If TypeOf Me.dgvReceipt.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvReceipt.ActiveEditor, RadMultiColumnComboBoxElement)
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
    Private Sub MasterTemplate_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvReceipt.CellValueChanged
        Try
            If Not inSideLoadData Then
                If IsCellValueChanged And dgvReceipt.CurrentRow.Index >= 0 Then
                    IsCellValueChanged = False
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(dgvReceipt, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
                        Dim TempAmt As Double = clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colTemp).Value)
                        If (e.Column Is dgvReceipt.Columns(colApply)) Then
                            If dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes" Then
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = Math.Round(dgvReceipt.CurrentRow.Cells(colTemp).Value, 2)
                                dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
                            ElseIf e.Row.Cells(colApply).Value = "No" Then
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
                                dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
                            End If
                        End If
                        If (e.Column Is dgvReceipt.Columns(colAppliedAmt) Or e.Column Is dgvReceipt.Columns(colAdjAmt)) Then
                            If e.Column Is dgvReceipt.Columns(colAdjAmt) Then
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0.0
                                TempAmt = clsSNInvoiceHead.GetInvoiceBalanceAmt(dgvReceipt.CurrentRow.Cells(colSINo).Value, Nothing)
                                dgvReceipt.CurrentRow.Cells(colTemp).Value = TempAmt
                                dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt
                            End If
                            If dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value > dgvReceipt.CurrentRow.Cells(colTemp).Value Then
                                common.clsCommon.MyMessageBoxShow(Me, "Applied Amount can not be greater than Balance Amount.", Me.Text)
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
                            End If
                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
                        End If
                        Dim ReceiptAmt As Double = 0
                        For Each grow As GridViewRowInfo In dgvReceipt.Rows
                            If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            Else
                                ''richa agarwal ERO/18/07/19-000956
                                If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                                    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                Else
                                    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                End If
                            End If

                            'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                            '    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            'Else
                            '    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            'End If
                        Next
                        ''richa VIJ/18/12/19-000124
                        If EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = False Or (EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = True And clsCommon.CompairString(ddlTransType.SelectedValue, "R") <> CompairStringResult.Equal) Then
                            If ReceiptAmt > clsCommon.myCdbl(txtUnApplAmt.Text) Then
                                common.clsCommon.MyMessageBoxShow(Me, "Applied Amount can not be greater than Receipt Amount.", Me.Text)
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
                                ReceiptAmt = 0
                                For Each grow As GridViewRowInfo In dgvReceipt.Rows
                                    If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                        ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                    Else
                                        ''richa agarwal ERO/18/07/19-000956
                                        If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                                            ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                        Else
                                            ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                        End If
                                    End If

                                    'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                                    '    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                    'Else
                                    '    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                    'End If
                                Next
                                dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = Math.Round(clsCommon.myCdbl(txtUnApplAmt.Text) - ReceiptAmt, 2)
                                dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colTemp).Value) - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
                            End If
                        End If
                        dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
                        Dim AppliedAmt As Double = 0
                        For Each grow As GridViewRowInfo In dgvReceipt.Rows
                            If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
                                AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            Else
                                ''richa agarwal ERO/18/07/19-000956
                                If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                                    AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                Else
                                    AppliedAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                                End If
                            End If
                            'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
                            '    AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            'Else
                            '    AppliedAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
                            'End If
                        Next
                        ''richa VIJ/18/12/19-000124
                        If EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                            txtUnApplAmt.Text = clsCommon.myCstr(AppliedAmt)
                        End If
                        txtRcptAmt.Text = clsCommon.myCstr(AppliedAmt)

                        EnableDisableSettlement()
                    End If
                    IsCellValueChanged = True
                    setGridFocus()
                End If
            End If
        Catch ex As Exception
            IsCellValueChanged = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub MasterTemplate_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvReceipt.CellValueChanged
    '    Try
    '        If Not inSideLoadData Then
    '            If IsCellValueChanged And dgvReceipt.CurrentRow.Index >= 0 Then
    '                IsCellValueChanged = False
    '                If e.Column.FieldName.StartsWith("_CFLD_") Then
    '                    clsCustomFieldGrid.getFinderForCustomFieldGrid(dgvReceipt, e.Column.Name.ToString, MyBase.Form_ID)
    '                End If
    '                If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Or (RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) Then
    '                    Dim TempAmt As Double = clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colTemp).Value)
    '                    If (e.Column Is dgvReceipt.Columns(colApply)) Then
    '                        If dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes" Then
    '                            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = Math.Round(dgvReceipt.CurrentRow.Cells(colTemp).Value, 2)
    '                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
    '                        ElseIf e.Row.Cells(colApply).Value = "No" Then
    '                            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
    '                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
    '                        End If
    '                    End If
    '                    If (e.Column Is dgvReceipt.Columns(colAppliedAmt) Or e.Column Is dgvReceipt.Columns(colAdjAmt)) Then
    '                        If e.Column Is dgvReceipt.Columns(colAdjAmt) Then
    '                            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0.0
    '                            TempAmt = clsSNInvoiceHead.GetInvoiceBalanceAmt(dgvReceipt.CurrentRow.Cells(colSINo).Value, Nothing)
    '                            dgvReceipt.CurrentRow.Cells(colTemp).Value = TempAmt
    '                            dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt
    '                        End If
    '                        If dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value > dgvReceipt.CurrentRow.Cells(colTemp).Value Then
    '                            common.clsCommon.MyMessageBoxShow("Applied Amount can not be greater than Balance Amount.")
    '                            dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
    '                        End If
    '                        dgvReceipt.CurrentRow.Cells(colBalAmt).Value = TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
    '                    End If
    '                    Dim ReceiptAmt As Double = 0
    '                    For Each grow As GridViewRowInfo In dgvReceipt.Rows
    '                        If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
    '                            ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        Else
    '                            ''richa agarwal ERO/18/07/19-000956
    '                            If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
    '                                ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            Else
    '                                ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            End If
    '                        End If

    '                        'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
    '                        '    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        'Else
    '                        '    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        'End If
    '                    Next
    '                    If ReceiptAmt > clsCommon.myCdbl(txtUnApplAmt.Text) Then
    '                        common.clsCommon.MyMessageBoxShow("Applied Amount can not be greater than Receipt Amount.")
    '                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = 0
    '                        ReceiptAmt = 0
    '                        For Each grow As GridViewRowInfo In dgvReceipt.Rows
    '                            If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
    '                                ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            Else
    '                                ''richa agarwal ERO/18/07/19-000956
    '                                If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
    '                                    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                                Else
    '                                    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                                End If
    '                            End If

    '                            'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
    '                            '    ReceiptAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            'Else
    '                            '    ReceiptAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            'End If
    '                        Next
    '                        dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value = Math.Round(clsCommon.myCdbl(txtUnApplAmt.Text) - ReceiptAmt, 2)
    '                        dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colTemp).Value) - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
    '                    End If
    '                    dgvReceipt.CurrentRow.Cells(colBalAmt).Value = Math.Round(TempAmt - (clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAppliedAmt).Value)), 2) ' + clsCommon.myCdbl(dgvReceipt.CurrentRow.Cells(colAdjAmt).Value) Removed
    '                    Dim AppliedAmt As Double = 0
    '                    For Each grow As GridViewRowInfo In dgvReceipt.Rows
    '                        If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal Then
    '                            AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        Else
    '                            ''richa agarwal ERO/18/07/19-000956
    '                            If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
    '                                AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            Else
    '                                AppliedAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                            End If
    '                        End If
    '                        'If Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Credit Note") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(grow.Cells(colDocType).Value, "Refund") = CompairStringResult.Equal Then
    '                        '    AppliedAmt += clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        'Else
    '                        '    AppliedAmt -= clsCommon.myCdbl(grow.Cells(colAppliedAmt).Value)
    '                        'End If
    '                    Next
    '                    txtRcptAmt.Text = clsCommon.myCstr(AppliedAmt)
    '                    EnableDisableSettlement()
    '                End If
    '                IsCellValueChanged = True
    '                setGridFocus()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        IsCellValueChanged = True
    '        clsCommon.MyMessageBoxShow(Me, ex.Message)
    '    End Try
    'End Sub

    'Private Sub MasterTemplate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvReceipt.Click
    '    If dgvReceipt.Rows.Count > 0 Then
    '        If clsCommon.myCdbl(dgvReceipt.CurrentRow.Index) >= 0 Then
    '            If clsCommon.myCBool(dgvReceipt.CurrentRow.Cells(colAppliedAmt).IsCurrent) Then
    '                x = 0
    '                If dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes" Then
    '                    dgvReceipt.CurrentRow.Cells(colAppliedAmt).ReadOnly = False
    '                Else
    '                    dgvReceipt.CurrentRow.Cells(colAppliedAmt).ReadOnly = True
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub



    Private Sub MasterTemplate_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvReceipt.DoubleClick
        ''GridDouble_Click() by balwinder on 03/06/2019 becuse when double click on header column this evenet create problem to convet Yes To No.
        ''Call this function on cell double event.

    End Sub



    Sub GridDouble_Click()
        If dgvReceipt.Rows.Count > 0 Then
            If clsCommon.myCdbl(dgvReceipt.CurrentRow.Index) >= 0 Then
                If dgvReceipt.CurrentRow.Cells(colApply).IsCurrent = True Then
                    x = 0
                    If dgvReceipt.CurrentRow.Cells(colApply).Value = "No" Then
                        dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes"
                    ElseIf dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes" Then
                        dgvReceipt.CurrentRow.Cells(colApply).Value = "No"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub loadSecurityDepositType()
        ddlSecDepositType.DataSource = clsCustomerInvoiceHead.GetSecurityDepositType()
        ddlSecDepositType.DisplayMember = "Code"
        ddlSecDepositType.ValueMember = "Value"
    End Sub
    Private Sub loadReceiptType()
        IsPaymentTypeChanged = False
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        dt.Rows.Add("Receipt", "R")
        dt.Rows.Add("Advance", "P")
        dt.Rows.Add("Apply Document", "A")
        dt.Rows.Add("Misc Receipt", "M")
        dt.Rows.Add("On Account", "O")
        dt.Rows.Add("Unapplied", "U")
        dt.Rows.Add("Refund", "F")
        dt.Rows.Add("Misc Refund", "S")
        dt.Rows.Add("Set OFF", "K")

        ddlTransType.DataSource = dt
        ddlTransType.DisplayMember = "Code"
        ddlTransType.ValueMember = "Value"
        IsPaymentTypeChanged = True
    End Sub

    Private Sub FrmReceipt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            'If savedata() Then
            '    clsCommon.MyMessageBoxShow("Data Saved Successfully.")
            'End If
            SaveDataF()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            PrintData()
        End If
        If e.KeyCode = Keys.F2 AndAlso dgvmiscpayment.CurrentCell IsNot Nothing Then
            If dgvmiscpayment.CurrentColumn Is dgvmiscpayment.Columns(colGLAccount) Then
                dgvmiscpayment.CurrentColumn = dgvmiscpayment.Columns(2)
                OpenICodeList(True)
                dgvmiscpayment.CurrentColumn = dgvmiscpayment.Columns(1)
                dgvmiscpayment.CurrentColumn = dgvmiscpayment.Columns(3)
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                           "========Table Name=========" + Environment.NewLine +
                           "TSPL_RECEIPT_HEADER,TSPL_RECEIPT_DETAIL " + Environment.NewLine +
                           "TSPL_RECEIPT_DETAIL_GST , TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine +
                           "TSPL_SD_SALE_INVOICE_HEAD ,TSPL_Customer_Invoice_Head, " + Environment.NewLine +
                           "Tspl_bank_transfer(For Bank transfer Post)" + Environment.NewLine +
                           "tspl_BankReco_Head & tspl_BankReco_Detail ( For Outstanding Entry) " + Environment.NewLine +
                           "=========Setting Name======" + Environment.NewLine +
                           "AllowBranchAcconReceiptPrint" + Environment.NewLine +
                           "AllowDefaultBankCodeforCreditNote" + Environment.NewLine +
                           "Apply Document Date" + Environment.NewLine +
                           "Auto Receipt Payment" + Environment.NewLine +
                           "Customer master finder location-wise on AR Receipt" + Environment.NewLine +
                           "Default Bank for Cash Payment" + Environment.NewLine +
                           "Default Location for Cash Payment" + Environment.NewLine +
                           "SecurityDocumentKnockOffonReceipt" + Environment.NewLine +
                           "AllowToUseSubAccount" + Environment.NewLine +
                           "AllowtoSkipJournalEntryofPaymentandReceiptforAD " + Environment.NewLine +
                           "AllowUseApplyDocSeriesForReceipt" + Environment.NewLine +
                           "ApplyBrachAccounting" + Environment.NewLine +
                           "StopNegativeBankBalance" + Environment.NewLine +
                           "AllowReceiptThroughSO" + Environment.NewLine +
                           "ShowTaxRateColumnOnTransaction" + Environment.NewLine +
                           "ERPStartDate" + Environment.NewLine +
                           "ShowHierarchyAndCostCenterInARInvoiceEntry" + Environment.NewLine +
                           "DOTaggingForDairySaleModule" + Environment.NewLine +
                           "AllowtoSetReceiptAmountForCashTransaction" + Environment.NewLine +
                           "=========Function======" + Environment.NewLine +
                           "Journal Entry (On Post Button)  ")

        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F10 Then
            If MyBase.isReverse Then
                If Not isSettlementBankOnly Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = clsFixedParameterType.SettlementBankOnlyPWD
                    frm.strCode = clsFixedParameterCode.SettlementBankOnlyPWD
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        isSettlementBankOnly = True
                    End If
                Else
                    isSettlementBankOnly = False
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

    End Sub

    Public Sub OpenICodeList(ByVal IsButtonClicked As Boolean)
        If clsCommon.myLen(fndBankCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Bank Code ", Me.Text)
            fndBankCode.Focus()
            Return
        End If
        Dim bankseg As String = " select right(BANKACC,3) as segment,BANKACC,BANK_CODE from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
        Dim val As String = clsDBFuncationality.getSingleValue(bankseg)
        Dim qry As String = ""
        Dim whrCls As String = ""
        Dim arrlist As New ArrayList()
        arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        qry = arrlist.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        whrCls = arrlist.Item(1)
        If whrCls = "" Then

        Else
            whrCls = "(" + whrCls + ")"
        End If
        If whrCls Is Nothing OrElse whrCls = "" Then
            whrCls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        Else
            whrCls = whrCls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        End If
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 0 Then
            whrCls += " and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + val + "'"
        End If
        whrCls += " AND ControlAccount<>'Y'"

        If clsCommon.GetDateWithStartTime(dtRcpt.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
            whrCls = clsCommon.ReplaceString(whrCls, "TSPL_GL_ACCOUNTS.ControlAccount='N'", " 2=2 ")
            whrCls = clsCommon.ReplaceString(whrCls, "ControlAccount ='N'", " 2=2 ")
            whrCls = clsCommon.ReplaceString(whrCls, "( ControlAccount ='N')", " ")

            whrCls = clsCommon.ReplaceString(whrCls, "TSPL_GL_ACCOUNTS.ControlAccount<>'Y'", " 2=2 ")
            whrCls = clsCommon.ReplaceString(whrCls, "ControlAccount<>'Y'", " 2=2 ")
            whrCls = clsCommon.ReplaceString(whrCls, "( ControlAccount<>'Y')", " ")
        End If


        ''richa 14 feb,2019  TEC/05/02/19-000412 check for opening in case of Miscellenous
        Dim JEWithOPening As Boolean = False
        If clsCommon.myLen(clsCommon.GetDateWithStartTime(ERPStartDate)) > 0 Then
            If clsCommon.GetDateWithStartTime(dtRcpt.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
                JEWithOPening = True
            End If
        End If
        Dim strCustomerOpeningAccount As String = String.Empty
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, Nothing)), "1") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "M") = CompairStringResult.Equal) And JEWithOPening = True Then
            strCustomerOpeningAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Bank_Opening_Clearing_Account ,'') from tspl_bank_master where BANK_CODE ='" & fndBankCode.Value & "'"))
            If clsCommon.myLen(strCustomerOpeningAccount) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please set Bank Opening Clearing Account for Bank - " + fndBankCode.Value, Me.Text)
                Return
            End If
            whrCls += " and Account_Code='" & strCustomerOpeningAccount & "'"
        End If


        'richa 17 SEp,2019 TEC/03/07/19-000927
        Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrCls & Environment.NewLine &
            " UNION All " & Environment.NewLine &
            " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine &
" left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine &
" and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine &
    " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine &
    " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine &
    " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForReceipt =1) "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, Nothing)) = 0 Then
            strqry += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + val + "'"
        End If

        If clsCommon.myLen(strCustomerOpeningAccount) > 0 Then
            strqry += " and Account_Code='" & strCustomerOpeningAccount & "'"
        End If
        strqry += " ) Final "

        dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value = clsCommon.ShowSelectForm("PaymentGLAC", strqry, "Account_Code", "", clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), "Account_Code", IsButtonClicked)
        'dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value = clsCommon.ShowSelectForm("PaymentGLAC", qry, "Account_Code", whrCls, clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), "Account_Code", IsButtonClicked)
        dgvmiscpayment.CurrentRow.Cells(colAccDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value) + "'"))
    End Sub


    Private Sub dgvmiscpayment_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvmiscpayment.CellEditorInitialized
        If TypeOf Me.dgvmiscpayment.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvmiscpayment.ActiveEditor, RadMultiColumnComboBoxElement)
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

    Private Sub dgvmiscpayment_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles dgvmiscpayment.CellFormatting
        If clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "S") = CompairStringResult.Equal Then
            If clsCommon.myLen(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value) > 0 Then
                Dim grouptype As String = ""
                grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), Nothing)
                If clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                    dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).ReadOnly = True
                    dgvmiscpayment.CurrentRow.Cells(colCostCenter).ReadOnly = True
                Else
                    dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).ReadOnly = False
                    dgvmiscpayment.CurrentRow.Cells(colCostCenter).ReadOnly = False
                End If
            End If
        End If
    End Sub

    Private Sub RadGridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvmiscpayment.CellValueChanged
        Try

            If Not inSideLoadData Then
                If e.Column.Name = colGLAccount Then
                    OpenICodeList(False)
                End If
                Dim appliedAmt As Decimal = 0.0
                If e.Column.Name = colAmount Then
                    If clsCommon.myLen(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value) <= 0 Then
                        If clsCommon.myCdbl(dgvmiscpayment.CurrentRow.Cells(colAmount).Value) > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please select Account Code First.", Me.Text)
                            dgvmiscpayment.CurrentRow.Cells(colAmount).Value = 0
                            OpenICodeList(True)
                        End If
                    End If

                    For Each row As GridViewRowInfo In dgvmiscpayment.Rows
                        appliedAmt = appliedAmt + CDec(row.Cells(colAmount).Value)
                    Next
                    If dgvmiscpayment.CurrentRow.Index < 0 Then
                        appliedAmt = appliedAmt + CDec(dgvmiscpayment.CurrentRow.Cells(colAmount).Value)
                    End If
                    txtRcptAmt.Text = appliedAmt
                End If
                ''richa

                If dgvmiscpayment.CurrentColumn Is dgvmiscpayment.Columns(colGLAccount) Then
                    OpenICodeList(False)
                    dgvmiscpayment.CurrentRow.Cells(colAccDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value) + "'"))
                    Dim grouptype As String = ""
                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), Nothing)
                    If clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                        dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).ReadOnly = True
                        dgvmiscpayment.CurrentRow.Cells(colCostCenter).ReadOnly = True
                    Else
                        dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).ReadOnly = False
                        dgvmiscpayment.CurrentRow.Cells(colCostCenter).ReadOnly = False
                    End If

                ElseIf dgvmiscpayment.CurrentColumn Is dgvmiscpayment.Columns(colHirerachyCenter) Then
                    Dim grouptype As String = ""
                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), Nothing)
                    If clsCommon.CompairString(grouptype, "Balance Sheet") <> CompairStringResult.Equal Then
                        OpenHierarchyCode(False)
                    End If

                ElseIf dgvmiscpayment.CurrentColumn Is dgvmiscpayment.Columns(colCostCenter) Then
                    Dim grouptype As String = ""
                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colGLAccount).Value), Nothing)
                    If clsCommon.CompairString(grouptype, "Balance Sheet") <> CompairStringResult.Equal Then
                        OpenCostCenterCode(False)
                    End If

                End If


                ''-----

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
        dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value = clsCommon.ShowSelectForm("HierarchyPN", qry, "Code", "", clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value), "Code", isButtonClick)
        dgvmiscpayment.CurrentRow.Cells(colHirerachyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where HIRERACHY_CODE='" + clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value) + "'"))
    End Sub
    Private Sub OpenCostCenterCode(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value)) > 0 Then
            Dim DBLevel As String = String.Empty
            DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colHirerachyCenter).Value) + "' "))
            Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
            dgvmiscpayment.CurrentRow.Cells(colCostCenter).Value = clsCommon.ShowSelectForm("HierarchyPNCc", qry, "Code", " Hirerachy_Level = '" + DBLevel + "'", clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colCostCenter).Value), "Code", isButtonClick)
            dgvmiscpayment.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + clsCommon.myCstr(dgvmiscpayment.CurrentRow.Cells(colCostCenter).Value) + "'"))
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
        End If
    End Sub

    Private Sub dgvmiscpayment_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles dgvmiscpayment.CurrentColumnChanged
        If dgvmiscpayment.RowCount > 0 Then
            Dim intCurrRow As Integer = dgvmiscpayment.CurrentRow.Index
            dgvmiscpayment.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = dgvmiscpayment.Rows.Count - 1 Then
                dgvmiscpayment.Rows.AddNew()
                dgvmiscpayment.CurrentRow = dgvmiscpayment.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub MasterTemplate_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgvmiscpayment.UserAddedRow
        For i As Integer = 0 To dgvmiscpayment.Rows.Count - 1
            dgvmiscpayment.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                dgvmiscpayment.Rows(i).Cells(0).Value = i + 1
            End If
        Next
    End Sub

    Private Sub ddlTransType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlTransType.SelectedIndexChanged
        If chk = False Then
            IsTransferVisible()
            If IsPaymentTypeChanged Then
                TypeChange()
            End If
        End If

        'EnableDisableIncaseofRefund()
    End Sub

    Private Sub TypeChange()
        funReset()
        TxtForeignBankCharges.Value = 0
        TxtForeignBankCharges.Value = 0
        TxtForeignBankCharges.Enabled = True
        TxtBankCharges.Enabled = True
        dgvReceipt.Visible = False
        dgvmiscpayment.Visible = False
        chkSalesmanType.Enabled = False
        chkSalesmanType.Checked = False
        lblSalesman.Visible = False
        txtsalesmanCode.Visible = False
        txtSamesmanName.Visible = False
        lblcustomer.Visible = True
        fndCustomer.Visible = True
        txtDistr_Code.Visible = True
        txtCusName.Visible = True
        txtremitto.Visible = False
        chkSecurityDposit.Enabled = False
        btnGo.Visible = False
        lblRcptAmt.Visible = True
        txtRcptAmt.Visible = True
        lblUnApplyAmt.Visible = True
        txtUnApplAmt.Visible = True
        lblUnAppliedBal.Visible = True
        txtUnAppliedBal.Visible = True
        lblUnAppliedNo.Visible = True
        txtUnApplieadNo.Visible = True
        lblLoadOutNo.Visible = False
        txtLoadOutno.Visible = False
        pnlCform.Visible = False
        'txtLoadOutno.Text = ""
        chkParentCust.Enabled = True
        Pnlmemorandum.Visible = False
        lblDocumentNo.Visible = False
        txtDocumentNo.Visible = False
        txtDocument_ForAdvanceDoc.Visible = False
        lblBalAmt.Visible = False
        txtUnApplAmt.Enabled = True
        '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
        lblSalesOrder.Visible = False
        FndSalesOrder.Visible = False
        ''--------------------------
        ResetDocument()
        If ddlTransType.SelectedValue = "M" OrElse ddlTransType.SelectedValue = "S" Then
            TxtForeignBankCharges.Enabled = False
            TxtBankCharges.Enabled = False
            chkSalesmanType.Enabled = True
            ''richa agarwal against ticket no BM00000008631 on 07/01/2016 
            'chkSalesmanType.Checked = True


            'txtremitto.Visible = True
            'chkSecurityDposit.Enabled = True
            chkSecurityDposit.Enabled = False
            chkParentCust.Enabled = False
            If chkSecurityDposit.Checked = True Then
                lblcustomer.Visible = True
                fndCustomer.Visible = True
                txtCusName.Visible = True
            Else
                lblcustomer.Visible = False
                fndCustomer.Visible = False
                txtCusName.Visible = False
            End If
            btnAdjment.Enabled = False
            If (ddlTransType.SelectedValue = "S") Then
                LoadBlankGrid("S")

            Else
                LoadBlankGrid("M")
                '' Anubhooti 07-May-2015 BM00000006486
                'lblLoadOutNo.Visible = True
                ' txtLoadOutno.Visible = True
            End If

            dgvmiscpayment.Visible = True
        ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then '--Receipt
            LoadBlankGrid("R")
            dgvReceipt.Visible = True
            btnGo.Visible = True
            If clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                txtUnApplAmt.Enabled = False
                lblDocumentNo.Visible = True
                txtDocumentNo.Visible = True
                lblBalAmt.Visible = True
                TxtForeignBankCharges.Enabled = False
                TxtBankCharges.Enabled = False
                chkForCardSale.Checked = False
                chkForCardSale.Enabled = True
            Else
                txtUnApplAmt.Enabled = True
                chkForCardSale.Checked = False
                chkForCardSale.Enabled = False
                If clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
                    btnGo.Enabled = True
                Else
                    btnGo.Enabled = False
                End If
            End If
        ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal Then '-- Advance
            lblRcptAmt.Visible = False
            txtRcptAmt.Visible = False
            lblUnAppliedBal.Visible = False
            txtUnAppliedBal.Visible = False
            lblUnAppliedNo.Visible = False
            txtUnApplieadNo.Visible = False
            'chkSecurityDposit.Enabled = True

            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                Pnlmemorandum.Visible = True
                '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
                lblSalesOrder.Visible = True
                FndSalesOrder.Visible = True
                chkSecurityDposit.Enabled = True
                ''--------------------------
            Else
                chkSecurityDposit.Enabled = False
            End If

            If objCommonVar.IsDemoERP = True Then
                pnlCform.Visible = True
            Else
                pnlCform.Visible = False
            End If


        End If
        If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal Then
            chkCheckPrint.Visible = True
        Else
            chkCheckPrint.Visible = False
        End If
        If clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), "Cash", False) = CompairStringResult.Equal And (clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "M") = CompairStringResult.Equal) Then
            chkAutoGeneBT.Enabled = True
            Me.txtToBank.Enabled = True
        Else
            chkAutoGeneBT.Enabled = False
            Me.txtToBank.Enabled = False
        End If
        '' Anubhooti 02-Dec-2014 (Booking No. should only visible in case of Advance only)
        If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
            LblBookingNo.Visible = True
            fndBookingNo.Visible = True
        Else
            LblBookingNo.Visible = False
            fndBookingNo.Visible = False
        End If
        ''
        If isApplyBranchAccounting = True And (clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal) Then
            RadLabel18.Visible = True
            txtlocation.Visible = True
            LblLocDesp.Visible = True
            txtlocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' )"))
            If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
            Else
                LblLocDesp.Text = ""
            End If
        Else
            RadLabel18.Visible = False
            txtlocation.Visible = False
            LblLocDesp.Visible = False
            txtlocation.Value = ""
            LblLocDesp.Text = ""
        End If
        ''richa

        GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
        If GSTStatus Then
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                RadPageView1.Pages("TabForGST").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("TabForGST").Item.Visibility = ElementVisibility.Collapsed
            End If

            If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                lblDocumentNo.Visible = True
                txtDocument_ForAdvanceDoc.Visible = True
            Else
                txtDocument_ForAdvanceDoc.Visible = False
            End If
        End If
        ''richa ERO/18/07/19-000956
        If RefundknockoffwithCreditNote = True Then
            If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then '--Refund
                LoadBlankGrid("F")
                dgvReceipt.Visible = True
                btnGo.Visible = True
                txtUnApplAmt.Enabled = True
                lblRcptAmt.Visible = True
                txtRcptAmt.Visible = True
                lblUnAppliedBal.Visible = True
                txtUnAppliedBal.Visible = True
            End If
        End If
        ''---------------
    End Sub

    Sub ResetDocument()
        txtDocumentNo.Value = ""
        lblBalAmt.Text = ""
        txtUnApplAmt.Text = ""
    End Sub


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData()
    End Sub

    Sub PrintData()
        Try

            Dim arrreceipt As New ArrayList()
            If fndRcptNo.Value <> "" Then
                arrreceipt.Add(fndRcptNo.Value)
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select Receipt No.", Me.Text)
                Exit Sub
            End If
            Dim DocDate As Date?
            DocDate = Nothing
            DocDate = dtRcpt.Value
            'If clsERPFuncationality.GetGSTStatus(DocDate) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(txtDONo.Value)) > 0 OrElse clsERPFuncationality.GetGSTStatus(DocDate) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(txtDONo.Value)) > 0 OrElse clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0 Then
            If (clsERPFuncationality.GetGSTStatus(DocDate) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(txtDONo.Value)) > 0) OrElse (clsERPFuncationality.GetGSTStatus(DocDate) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(txtDONo.Value)) > 0) OrElse ((clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0) Then
                'Dim ChkAscTaxType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT type  FROM TSPL_TAX_MASTER where Tax_Code in(select TAX_Code from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + txtTaxGroup.Value + "' ) "))
                'Dim ChkDescTaxType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT type  FROM TSPL_TAX_MASTER where Tax_Code in(select TAX_Code from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + txtTaxGroup.Value + "' ) ORDER BY TSPL_TAX_MASTER.Tax_Code  desc "))
                Dim arr As New ArrayList()
                Dim dttype As New DataTable()
                dttype = clsDBFuncationality.GetDataTable("SELECT type  FROM TSPL_TAX_MASTER where Tax_Code in(select TAX_Code from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + txtTaxGroup.Value + "' ) ")
                If dttype IsNot Nothing AndAlso dttype.Rows.Count > 0 Then
                    For Each dr As DataRow In dttype.Rows
                        arr.Add(clsCommon.myCstr(dr("type").ToString()))
                    Next
                End If
                Dim StrQuery As String = Nothing
                StrQuery = "select  isnull(TSPL_SD_SALES_ORDER_HEAD.Document_Code,'') as SO_Code,isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code,'') as DO_Code, isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'') as Applied_Receipt,convert(varchar,Against_Receipt_Header.Receipt_Date,103) as Against_ReceiptDate,TSPL_STATE_MASTER.STATE_NAME as CustomerStateName, TSPL_STATE_MASTER.Is_GST_UT, " &
                    " (case when isnull(Sales_Order_Location.state,'')<>'' then Sales_Order_Location.state when isnull(TSPL_LOCATION_MASTER.state,'')<>'' then TSPL_LOCATION_MASTER.state else NormalLocation.State end )as Location_State ," &
                    " (case when isnull(Sales_Order_State.GST_STATE_Code,'')<>'' then Sales_Order_State.GST_STATE_Code when  isnull(tspl_state_master_for_location_state.GST_STATE_Code,'')<>'' then tspl_state_master_for_location_state.GST_STATE_Code else Normal_loc_gstState.GST_STATE_Code end ) as LOC_GST_State_Code," &
                    " (case when ISNULL(Sales_Order_Location.GSTNO,'')<>'' then Sales_Order_Location.GSTNO when ISNULL(TSPL_LOCATION_MASTER.GSTNO,'')<>'' then TSPL_LOCATION_MASTER.GSTNO else NormalLocation.GSTNO  end) as Comp_GSTIN_NO, " &
                    " TSPL_CUSTOMER_MASTER.state AS Customer_State, tspl_company_master.state as Comp_State, TSPL_CITY_MASTER.City_Name as Customer_city," &
                    " TSPL_RECeIPT_HEADER.Delivery_Code_PS,'' AS Electronic_Ref_No,TSPL_CUSTOMER_MASTER.Customer_Name,(CASE WHEN  ISNULL(TSPL_CUSTOMER_MASTER.Add1,'')<>'' THEN TSPL_CUSTOMER_MASTER.Add1   WHEN ISNULL(TSPL_CUSTOMER_MASTER.Add2,'')<>'' THEN ','+TSPL_CUSTOMER_MASTER.Add2  WHEN ISNULL(TSPL_CUSTOMER_MASTER.Add3,'')<>'' THEN ','+TSPL_CUSTOMER_MASTER.Add3  END ) AS Cust_Address,TSPL_STATE_MASTER.STATE_NAME as Customer_State,TSPL_STATE_MASTER.GST_STATE_Code as Cust_GST_StateCode,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GSTIN_NO, " &
                    " TSPL_COMPANY_MASTER.Comp_Name,COMP_Address=TSPL_COMPANY_MASTER.Add1 + CASE WHEN ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' THEN ','+TSPL_COMPANY_MASTER.Add2 WHEN ISNULL(TSPL_COMPANY_MASTER.Add3,'')<>'' THEN ','+TSPL_COMPANY_MASTER.Add3  END ," &
                    " SO_Address=Sales_Order_Location.Add1 + CASE WHEN ISNULL(Sales_Order_Location.Add2,'')<>'' THEN ','+Sales_Order_Location.Add2 WHEN ISNULL(Sales_Order_Location.Add3,'')<>'' THEN ','+Sales_Order_Location.Add3  END ," &
                    " DO_Address=(case when isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code,'')<>'' then TSPL_LOCATION_MASTER.Add1 + CASE WHEN ISNULL(TSPL_LOCATION_MASTER.Add2,'')<>'' THEN ','+TSPL_LOCATION_MASTER.Add2 WHEN ISNULL(TSPL_LOCATION_MASTER.Add3,'')<>'' THEN ','+TSPL_LOCATION_MASTER.Add3 END  else  NormalLocation.Add1 + CASE WHEN ISNULL(NormalLocation.Add2,'')<>'' THEN ','+NormalLocation.Add2 WHEN ISNULL(NormalLocation.Add3,'')<>'' THEN ','+NormalLocation.Add3 end end )  ," &
                    " TSPL_COMPANY_MASTER.Pan_No as Comp_PanNo,TSPL_RECEIPT_HEADER.Receipt_Amount,TSPL_RECEIPT_HEADER.Receipt_No," &
                    " CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Receipt_Date,103) AS Receipt_Date, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_RECEIPT_DETAIL_GST.Qty,TSPL_RECEIPT_DETAIL_GST.Unit_code,TSPL_RECEIPT_DETAIL_GST.Item_Cost,TSPL_RECEIPT_DETAIL_GST.Amount,TSPL_RECEIPT_DETAIL_GST.ReceiptAdvance,TSPL_RECEIPT_DETAIL_GST.ReceiptTotalTax,TSPL_RECEIPT_DETAIL_GST.ReceiptTotalAdvanceAmt,TSPL_RECEIPT_HEADER.Receipt_Type, "

                If (clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0 Then
                    StrQuery += " isnull(TSPL_RECEIPT_HEADER.TAX1_Amt,0) as TAX1_Amt, isnull(TSPL_RECEIPT_HEADER.TAX1_Rate,0) as TAX1_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX2_Amt,0) as TAX2_Amt, isnull(TSPL_RECEIPT_HEADER.TAX2_Rate,0) as TAX2_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX3_Amt,0) as TAX3_Amt, isnull(TSPL_RECEIPT_HEADER.TAX3_Rate,0) as TAX3_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX4_Amt,0) as TAX4_Amt, isnull(TSPL_RECEIPT_HEADER.TAX4_Rate,0) as TAX4_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX5_Amt,0) as TAX5_Amt, isnull(TSPL_RECEIPT_HEADER.TAX5_Rate,0) as TAX5_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX6_Amt,0) as TAX6_Amt, isnull(TSPL_RECEIPT_HEADER.TAX6_Rate,0) as TAX6_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX7_Amt,0) as TAX7_Amt, isnull(TSPL_RECEIPT_HEADER.TAX7_Rate,0) as TAX7_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX8_Amt,0) as TAX8_Amt, isnull(TSPL_RECEIPT_HEADER.TAX8_Rate,0) as TAX8_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX9_Amt,0) as TAX9_Amt, isnull(TSPL_RECEIPT_HEADER.TAX9_Rate,0) as TAX9_Rate,  isnull(TSPL_RECEIPT_HEADER.TAX10_Amt,0) as TAX10_Amt, isnull(TSPL_RECEIPT_HEADER.TAX10_Rate,0) as TAX10_Rate "
                Else
                    StrQuery += " isnull(TSPL_RECEIPT_DETAIL_GST.TAX1_Amt_Receipt,0) as TAX1_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX1_Rate,0) as TAX1_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX2_Amt_Receipt,0) as TAX2_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX2_Rate,0) as TAX2_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX3_Amt_Receipt,0) as TAX3_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX3_Rate,0) as TAX3_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX4_Amt_Receipt,0) as TAX4_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX4_Rate,0) as TAX4_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX5_Amt_Receipt,0) as TAX5_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX5_Rate,0) as TAX5_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX6_Amt_Receipt,0) as TAX6_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX6_Rate,0) as TAX6_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX7_Amt_Receipt,0) as TAX7_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX7_Rate,0) as TAX7_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX8_Amt_Receipt,0) as TAX8_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX8_Rate,0) as TAX8_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX9_Amt_Receipt,0) as TAX9_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX9_Rate,0) as TAX9_Rate,  isnull(TSPL_RECEIPT_DETAIL_GST.TAX10_Amt_Receipt,0) as TAX10_Amt, isnull(TSPL_RECEIPT_DETAIL_GST.TAX10_Rate,0) as TAX10_Rate "

                End If
                If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                    StrQuery += " ,'Receiving location' as Caption,TSPL_RECEIPT_HEADER.Location_GL_Code as Location,NormalLocation.Location_Code,NormalLocation.Location_Desc,NormalLocation.Add1,NormalLocation.Add2,NormalLocation.Add3,NormalLocation.Add4 "
                End If

                StrQuery += " ,tax1.Type as TAX1,tax2.Type as TAX2,tax3.Type as TAX3,tax4.Type as TAX4,tax5.Type as TAX5,tax6.Type as TAX6,tax7.Type as TAX7,tax8.Type as TAX8,tax9.Type as TAX9,tax10.Type as TAX10 " &
                " from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_DETAIL_GST ON TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL_GST.Receipt_No " &
                " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " &
                " left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE " &
                "  left outer join TSPL_CITY_MASTER on TSPL_CUSTOMER_MASTER.City_Code =TSPL_CITY_MASTER.City_Code " &
                " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_RECEIPT_DETAIL_GST.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                " left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_RECEIPT_HEADER.Delivery_Code_PS=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " &
                " left join TSPL_LOCATION_MASTER ON TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " &
                " left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state "

                If (clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso clsCommon.myLen(txtDONo.Value) <= 0 Then
                    StrQuery += " left join TSPL_TAX_MASTER as tax1 on TSPL_RECEIPT_HEADER.tax1=tax1.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax2 on TSPL_RECEIPT_HEADER.tax2=tax2.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax3 on TSPL_RECEIPT_HEADER.tax3=tax3.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax4 on TSPL_RECEIPT_HEADER.tax4=tax4.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax5 on TSPL_RECEIPT_HEADER.tax5=tax5.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax6 on TSPL_RECEIPT_HEADER.tax6=tax6.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax7 on TSPL_RECEIPT_HEADER.tax7=tax7.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax8 on TSPL_RECEIPT_HEADER.tax8=tax8.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax9 on TSPL_RECEIPT_HEADER.tax9=tax9.Tax_Code " &
    " left join TSPL_TAX_MASTER as tax10 on TSPL_RECEIPT_HEADER.tax10=tax10.Tax_Code "

                Else
                    StrQuery += " left join TSPL_TAX_MASTER as tax1 on TSPL_RECEIPT_DETAIL_GST.tax1=tax1.Tax_Code " &
              " left join TSPL_TAX_MASTER as tax2 on TSPL_RECEIPT_DETAIL_GST.tax2=tax2.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax3 on TSPL_RECEIPT_DETAIL_GST.tax3=tax3.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax4 on TSPL_RECEIPT_DETAIL_GST.tax4=tax4.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax5 on TSPL_RECEIPT_DETAIL_GST.tax5=tax5.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax6 on TSPL_RECEIPT_DETAIL_GST.tax6=tax6.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax7 on TSPL_RECEIPT_DETAIL_GST.tax7=tax7.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax8 on TSPL_RECEIPT_DETAIL_GST.tax8=tax8.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax9 on TSPL_RECEIPT_DETAIL_GST.tax9=tax9.Tax_Code" &
              " left join TSPL_TAX_MASTER as tax10 on TSPL_RECEIPT_DETAIL_GST.tax10=tax10.Tax_Code"

                End If

                ' If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                StrQuery += " left join (select top 1 * from TSPL_LOCATION_MASTER where Loc_Segment_Code='" + txtlocation.Value + "') as NormalLocation on NormalLocation.Loc_Segment_Code=TSPL_RECEIPT_HEADER.Location_GL_Code " &
                    " 	left join TSPL_STATE_MASTER as Normal_loc_gstState on Normal_loc_gstState.STATE_CODE=NormalLocation.State "
                'End If

                StrQuery += " left join TSPL_RECEIPT_HEADER  as Against_Receipt_Header on TSPL_RECEIPT_HEADER.Applied_Receipt=Against_Receipt_Header.Receipt_No  " &
                " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_RECEIPT_HEADER.Delivery_Code_PS=TSPL_SD_SALES_ORDER_HEAD.Document_Code" &
                " left outer join TSPL_LOCATION_MASTER as Sales_Order_Location on TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location = Sales_Order_Location.location_Code " &
                " left outer join TSPL_STATE_MASTER as Sales_Order_State on Sales_Order_Location.State =Sales_Order_State.STATE_CODE " &
                                " where TSPL_RECEIPT_HEADER.Receipt_No='" + fndRcptNo.Value + "'"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If arr.Contains("CGST") OrElse arr.Contains("SGST") Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_SGST_CGST", "Receipt Voucher", DocDate)
                    ElseIf arr.Contains("UGST") Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_UGST", "Receipt Voucher", DocDate)
                    ElseIf arr.Contains("IGST") Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_IGST", "Receipt Voucher", DocDate)
                    End If

                    'If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Customer_State")), clsCommon.myCstr(dt.Rows(0)("Location_State"))) = CompairStringResult.Equal Then
                    '    If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("Is_GST_UT")), 1) = CompairStringResult.Equal Then
                    '        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_UGST", "Receipt Voucher", DocDate)
                    '    Else
                    '        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_SGST_CGST", "Receipt Voucher", DocDate)
                    '    End If

                    'Else
                    '    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher_IGST", "Receipt Voucher", DocDate)
                    'End If
                    ''frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RptReceiptVoucher", "Receipt Voucher", DocDate)
                    frmCRV = Nothing
                End If
            Else
                Frmreceiptvoucher2.PrintData(Nothing, Nothing, Nothing, arrreceipt, Nothing, Nothing, Nothing, Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnGo.KeyDown
        inSideLoadData = True
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Alt Then
                funFillGrid(fndCustomer.Value)
                btnAdjment.Enabled = True
            End If
            inSideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            inSideLoadData = False
        End Try
    End Sub

    Private Sub btnAdjment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjment.Click
        Dim frm As New frmAdj()
        frm.strDocumnentNo = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colAdjNo).Value)
        frm.fndCusCode.Value = fndCustomer.Value
        txtCusName.Text = txtCusName.Text
        frm.fndDocNo.Value = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colSINo).Value)
        frm.txtDocAmt.Text = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colFilledTotal).Value)
        frm.txtBalanceAmt.Text = clsCommon.myCstr(dgvReceipt.CurrentRow.Cells(colBalAmt).Value)
        frm.isFromSettlement = True
        frm.dtLoadOut = dtRcpt.Value
        frm.ShowDialog()

        'Dim posted As String = clsDBFuncationality.getSingleValue("select ISNULL(Is_Post, 'N') from TSPL_Receipt_Adjustment_Header WHERE Adjustment_No='" + frm.strAdjNo + "'")
        'If clsCommon.CompairString(posted, "Y") = CompairStringResult.Equal Then
        '    clsCommon.MyMessageBoxShow("This Adjustment is already posted.")
        '    dgvReceipt.CurrentRow.Cells(colAdjNo).Value = ""
        '    Exit Sub
        'Else
        '    For Each grow As GridViewRowInfo In dgvReceipt.Rows
        '        If dgvReceipt.CurrentRow.Index <> grow.Index Then
        '            If clsCommon.myLen(frm.strAdjNo) > 0 And clsCommon.CompairString(grow.Cells(colAdjNo).Value, frm.strAdjNo) = CompairStringResult.Equal And Not clsCommon.CompairString(dgvReceipt.CurrentRow.Cells(colAdjNo).Value, frm.strAdjNo) = CompairStringResult.Equal Then
        '                clsCommon.MyMessageBoxShow("This Adjustment is already used in this document.")
        '                dgvReceipt.CurrentRow.Cells(colAdjNo).Value = ""
        '                Exit Sub
        '            End If
        '        End If
        '    Next
        'End If
        Dim InvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Doc_No  from TSPL_Receipt_Adjustment_Header WHere Adjustment_No='" + frm.strAdjNo + "'"))
        If dgvReceipt.CurrentRow.Cells(colSINo).Value <> InvoiceNo And clsCommon.myLen(InvoiceNo) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "This adjustment is not against this document.", Me.Text)
            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = ""
            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = 0
        Else
            dgvReceipt.CurrentRow.Cells(colAdjNo).Value = frm.strAdjNo
            dgvReceipt.CurrentRow.Cells(colAdjAmt).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Adjustment_Amount  from TSPL_Receipt_Adjustment_Header WHERE    Adjustment_No = '" + frm.strAdjNo + "' "))
        End If

        'End If
    End Sub

    Private Sub txtRcptAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRcptAmt.TextChanged
        If inSideLoadData = False Then
            If txtUnApplAmt.Text Is Nothing OrElse txtUnApplAmt.Text = "0.00" OrElse txtUnApplAmt.Text = "" Then
                Exit Sub
            Else
                Dim UnAppliedBal As Decimal = clsCommon.myCdbl(txtUnApplAmt.Text) - clsCommon.myCdbl(txtRcptAmt.Text)
                If UnAppliedBal < 0 And (ddlTransType.SelectedValue = "M" OrElse ddlTransType.SelectedValue = "S") Then
                    common.clsCommon.MyMessageBoxShow("Applied Amount Shouldn't Be More Than Receipt Amount", "Receipt Entry", MessageBoxButtons.OK)
                    dgvmiscpayment.CurrentRow.Cells(colAmount).Value = 0.0
                    Return
                End If
            End If
        End If
        txtUnAppliedBal.Text = Math.Round(clsCommon.myCdbl(txtUnApplAmt.Text) - clsCommon.myCdbl(txtRcptAmt.Text), 2)
    End Sub

    Private Sub txtUnApplied_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUnApplAmt.TextChanged
        If inSideLoadData = False Then
            Me.txtTotalPaymentBaseCurr.Text = clsCommon.myCdbl(txtUnApplAmt.Text) * clsCommon.myCdbl(txtConversionRate.Text)
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "O") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                txtRcptAmt.Text = clsCommon.myCdbl(txtUnApplAmt.Text)
                GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
                If GSTStatus AndAlso clsCommon.myLen(txtDONo.Value) > 0 Then
                    CalculateTaxAmountInAdvnce()
                Else
                    If GSTStatus AndAlso clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                        CalculateTaxwithoutDO()
                    End If
                End If
                ''richa agarwal ERO/18/07/19-000956
                If RefundknockoffwithCreditNote = True AndAlso clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                    If txtUnApplAmt.Text = "" Or clsCommon.myCdbl(txtUnApplAmt.Text) = 0 Then
                        btnGo.Enabled = False
                    Else
                        txtUnAppliedBal.Text = clsCommon.myCdbl(txtUnApplAmt.Text) - clsCommon.myCdbl(txtRcptAmt.Text)
                        btnGo.Enabled = True
                    End If
                End If
            Else
                ''richa VIJ/18/12/19-000124
                If EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = False Or (EnableGoButtonofReceiptEntryWithoutEnteringReceiptAmt = True And clsCommon.CompairString(ddlTransType.SelectedValue, "R") <> CompairStringResult.Equal) Then
                    If txtUnApplAmt.Text = "" Or clsCommon.myCdbl(txtUnApplAmt.Text) = 0 Then
                        If clsCommon.CompairString(ddlTransType.SelectedValue, "K") = CompairStringResult.Equal Then
                            btnGo.Enabled = True
                        Else
                            btnGo.Enabled = False
                        End If

                    Else
                        txtUnAppliedBal.Text = clsCommon.myCdbl(txtUnApplAmt.Text) - clsCommon.myCdbl(txtRcptAmt.Text)
                        btnGo.Enabled = True
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub txtUnApplAmt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnApplAmt.KeyPress
        If (e.KeyChar = Chr(8)) Or (e.KeyChar = Chr(127)) Or ((e.KeyChar >= Chr(46)) And (e.KeyChar <= Chr(58))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRcptAmt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRcptAmt.KeyPress
        If ((e.KeyChar >= Chr(46)) And (e.KeyChar <= Chr(58))) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

#End Region

    Private Sub FillVendorOutstanding(ByVal strCustomerCode As String)
        Try
            Arr = New ArrayList
            Arr.Add("I")
            Arr.Add("C")
            Arr.Add("D")
            Arr.Add("AV")
            Arr.Add("OA")
            Arr.Add("P")
            Arr.Add("RC")
            Qry = clsVendorMaster.GetOutStandingQry(dtRcpt.Value, dtRcpt.Value, Arr, False, Nothing, Nothing, Nothing)
            Qry = "Select Document_Total from (" & Qry & ") ZZZ WHERE [Vendor_Code]=(Select Top 1 Vendor_Code from TSPL_CUSTOMER_VENDOR_MAPPING WHERE Cust_Code='" + strCustomerCode + "')"
            lblOutstanding.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub dtRcpt_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtRcpt.ValueChanged
        dtPost.Value = dtRcpt.Value
        LoadBlankGrid(ddlTransType.SelectedValue)
    End Sub




    Private Sub chkSecurityDposit_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSecurityDposit.ToggleStateChanged
        If chkSecurityDposit.Checked = True Then
            chkSalesmanType.Checked = False
            fndCustomer.Visible = True
            txtDistr_Code.Visible = True
            txtCusName.Visible = True
            lblcustomer.Visible = True
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                lblSecurityDep.Visible = True
                ddlSecDepositType.Visible = True
                ddlSecDepositType.SelectedValue = ""
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                LoadBlankGrid("F")
            End If
        ElseIf chkSecurityDposit.Checked = False Then
            fndCustomer.Visible = True
            txtDistr_Code.Visible = True
            txtCusName.Visible = True
            lblcustomer.Visible = True
            fndCustomer.Value = ""
            txtCusName.Text = ""
            lblSecurityDep.Visible = False
            ddlSecDepositType.Visible = False
            ddlSecDepositType.SelectedValue = ""
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                fndCustomer.Visible = True
                txtDistr_Code.Visible = True
                txtCusName.Visible = True
                lblcustomer.Visible = True
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "F") = CompairStringResult.Equal Then
                LoadBlankGrid("F")
            End If
        End If
    End Sub


    Private Sub chkSalesmanType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSalesmanType.ToggleStateChanged
        If chkSalesmanType.Checked = True Then
            chkSecurityDposit.Checked = False
            lblSalesman.Visible = True
            txtsalesmanCode.Visible = True
            txtSamesmanName.Visible = True
        Else
            lblSalesman.Visible = False
            txtsalesmanCode.Visible = False
            txtSamesmanName.Visible = False
        End If
    End Sub

    Private Sub txtsalesmanCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtsalesmanCode._MYValidating
        Dim Qry As String = "Select EMP_CODE, Emp_Name  from TSPL_EMPLOYEE_MASTER "
        Dim strWhrcls As String = " Emp_type='Salesman'"
        txtsalesmanCode.Value = clsCommon.ShowSelectForm("SalesmanView", Qry, "EMP_CODE", strWhrcls, txtsalesmanCode.Value, "EMP_CODE", isButtonClicked)
        txtSamesmanName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Emp_Name  from TSPL_EMPLOYEE_MASTER Where Emp_type='Salesman' AND EMP_CODE='" + txtsalesmanCode.Value + "'"))
    End Sub


    Private Sub dgvmiscpayment_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvmiscpayment.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub dgvReceipt_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles dgvReceipt.CurrentRowChanged
        If Not IsGobtnClicked Then
            EnableDisableSettlement()
        End If
    End Sub

    Private Sub EnableDisableSettlement()
        btnAdjment.Enabled = False
        If dgvReceipt.Rows.Count > 0 Then
            If ddlTransType.SelectedValue = "R" And dgvReceipt.CurrentRow.Cells(colApply).Value = "Yes" And dgvReceipt.CurrentRow.Cells(colBalAmt).Value > 0 Then
                btnAdjment.Enabled = True
            Else
                btnAdjment.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtUnAppliedBal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUnAppliedBal.TextChanged
        If ddlTransType.SelectedValue = "M" Or ddlTransType.SelectedValue = "S" Or ddlTransType.SelectedValue = "R" Then
            If clsCommon.myCdbl(txtUnApplAmt.Text) > 0 Then
                If clsCommon.myCdbl(txtUnAppliedBal.Text) = clsCommon.myCdbl(txtUnApplAmt.Text) Then
                    txtUnApplAmt.Enabled = True
                Else
                    txtUnApplAmt.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub txtLoadOutno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLoadOutno._MYValidating
        Dim Qry As String = "select Transfer_No as [TransferNo],Transfer_Date as [TransferDate],Salesmancode ,Reference_Doc_No as [Ref. Document No] from tspl_transfer_head"
        Dim strWhrcls As String = "Location_Type='Logical' and Post='Y' and Transfer_Type  ='Lo' "
        txtLoadOutno.Value = clsCommon.ShowSelectForm("LoadOut", Qry, "TransferNo", strWhrcls, txtLoadOutno.Value, "TransferNo", isButtonClicked)

    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
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

                If clsRcptEntryHeader.ReverseAndUnpost(fndRcptNo.Value) Then
                    saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    funFillDetails(fndRcptNo.Value, NavigatorType.Current)
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

    Private Sub txtCFormInvNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCFormInvNo._MYValidating
        If Not String.IsNullOrEmpty(ddlTransType.SelectedValue) And chkCForm.Checked = True Then
            If Not String.IsNullOrEmpty(fndCustomer.Value) Then
                Dim qry As String = "select Document_Code,Document_Date from TSPL_SD_SALE_INVOICE_HEAD "
                txtCFormInvNo.Value = clsCommon.ShowSelectForm("InvoiceNo", qry, "Document_Code", "Posting_Date is not null and Against_C_Form=1 and CFormRecd=0 and CFormApplied=0 and Customer_Code='" & fndCustomer.Value & " ' and Document_Code not in (select isnull(CForm_InvoiceNo,'')  from  TSPL_RECEIPT_HEADER) ", txtCFormInvNo.Value, "Document_Code", isButtonClicked)
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select customer before selecting Invoice No", Me.Text)
            End If
        End If
    End Sub

    Private Sub txtConversionRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConversionRate.TextChanged

        Me.txtTotalPaymentBaseCurr.Text = clsCommon.myCdbl(txtUnApplAmt.Text) * clsCommon.myCdbl(txtConversionRate.Text)
    End Sub

    Private Sub chkCheckPrint_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCheckPrint.ToggleStateChanged
        If Me.chkCheckPrint.Checked Then
            Me.txtChkNo.Enabled = False
        Else
            Me.txtChkNo.Enabled = True
        End If
    End Sub

    Private Sub btnPrintCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCheck.Click
        If chkCheckPrint.Checked Then
            Dim obj As New clsRcptEntryHeader
            obj = clsRcptEntryHeader.GetData(Me.fndRcptNo.Value, NavigatorType.Current)
            Dim frm As New frmPrintCheck
            frmPrintCheck.Manual_Print = 0
            frmPrintCheck.Manual_Print = 0
            frmPrintCheck.BankCode = obj.Bank_Code
            frmPrintCheck.CheckCode = obj.CHECK_CODE
            frm.lblCheckDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + obj.CHECK_CODE + "'")
            frmPrintCheck.DocumentType = "Receipt Entry"
            frmPrintCheck.DocumentCode = obj.Receipt_No
            If clsCommon.myLen(obj.CHECK_CODE) > 0 Then
                frm.btnPrint.Text = "RePrint"
            End If
            frm.Show()
            obj = Nothing
        Else

            Dim obj As New clsRcptEntryHeader()
            obj = clsRcptEntryHeader.GetData(Me.fndRcptNo.Value, NavigatorType.Current)
            If clsPrintCheck.CheckforVoidCheck(obj.Bank_Code, obj.Cheque_No) Then
                clsCommon.MyMessageBoxShow(Me, "Please enter valid Cheque No, Entered Cheque No -" & obj.Cheque_No & " is Void.", Me.Text)
                Exit Sub
            End If
            Dim frm As New frmPrintCheck
            frmPrintCheck.BankCode = obj.Bank_Code
            frmPrintCheck.CheckCode = "" ''obj.CHECK_CODE
            frmPrintCheck.fndCheckCode.Enabled = False
            frmPrintCheck.DocumentType = "Receipt Entry"
            frmPrintCheck.DocumentCode = obj.Receipt_No
            frmPrintCheck.Manual_Print = 1
            frmPrintCheck.Manual_Print = 1
            frmPrintCheck.Manual_Check_No = txtChkNo.Text
            frmPrintCheck.Manual_Check_No = txtChkNo.Text
            frm.Show()
            obj = Nothing
        End If
    End Sub

    Private Sub txtToBank__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtToBank._MYValidating
        Dim strWhrcls As String = "Bank_Type !='C'"
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrcls)
        If clsCommon.myLen(strWhrcls) <= 0 Then
            strWhrcls = "Bank_Type !='C'"
        Else
            strWhrcls = strWhrcls & " and Bank_Type !='C'"
        End If
        txtToBank.Value = clsCommon.ShowSelectForm("ToBank", Qry, "Code", strWhrcls, txtToBank.Value, "Code", isButtonClicked)
    End Sub


    Private Sub txtmemoamt_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtmemoamt.Validating
        Try
            If clsCommon.myLen(txtmemoamt.Text) > 0 Then
                clsCommon.myCdbl(txtmemoamt.Text)
            End If
        Catch ex As Exception
            txtmemoamt.Text = "0"
        End Try
    End Sub

    Private Sub chkmemorndm_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmemorndm.ToggleStateChanged
        txtmemoamt.Enabled = False
        If chkmemorndm.Checked = True Then
            txtmemoamt.Enabled = True
        End If
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating

        Dim WhrCls As String = String.Empty

        WhrCls = " and ISNULL (TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'')='' and ISNULL (TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'')='' and ISNULL (TSPL_Customer_Invoice_Head.AgainstScrapReturn,'')='' "
        If clsCommon.myLen(fndBankCode.Value) > 0 Then
            WhrCls += " AND ([Bank Code] ='" & fndBankCode.Value & "' or [Bank Code] ='') "
        End If

        Dim strSecurityDocumentKnockOffonReceiptSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SecurityDocumentKnockOffonReceipt, clsFixedParameterCode.SecurityDocumentKnockOffonReceipt, Nothing))
        Dim strwhrsecurity As String = ""
        If clsCommon.CompairString(strSecurityDocumentKnockOffonReceiptSetting, "0") = CompairStringResult.Equal Then
            strwhrsecurity = " and isnull(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "
        End If


        strQuery = "Select * from (" &
        " Select Receipt_No as [Code], Entry_Desc as [Desc.], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code]," &
        " Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>'" + fndRcptNo.Value + "'),0) as [Bal Amt] from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Cust_Code='" + fndCustomer.Value + "' AND Receipt_Type IN ('P','O','U') AND Receipt_No <> '" + fndRcptNo.Value + "' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N' and Receipt_Date<='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "' " & strwhrsecurity & " " & Environment.NewLine &
        " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN (sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and isnull(TSPL_RECEIPT_HEADER.IsChkReverse,'') ='N') " & Environment.NewLine &
        " union all " & Environment.NewLine &
        " Select * from  (select [Invoice No] as [Code],Description as [Desc.],[Invoice Date] as [Receipt Date],Type as [Receipt Type],[Doc Total] as [Receipt Amt],'' As [Bank Code],'' AS [Payment Code], [Balance Amount]-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=[Invoice No])+(Select ISNULL(SUM(Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER WHere ISNULL(Is_Post,'N')<>'Y' AND Doc_No=SaleInvoice)) as [Bal Amt] from ( select " & Environment.NewLine &
        " TSPL_Customer_Invoice_Head.Description , " & Environment.NewLine &
        " 'No' as [Apply], Case When Document_Type='I' Then 'Invoice' When Document_Type='D' Then 'Debit Note' When Document_Type='C' Then 'Credit Note' End as [Type],  Case When ISNULL(Against_Sale_No,'')<>'' Then Against_Sale_No When ISNULL(Against_Sale_Return_No,'')<> '' Then Against_Sale_Return_No Else Document_No End as SaleInvoice, Document_No as [Invoice No],convert(date,Document_Date,103) as [Invoice Date] ,Document_Total as [Doc Total] ," & Environment.NewLine &
        " (TSPL_Customer_Invoice_Head.Document_Total -isnull((select sum(isnull(Applied_Amount,0)) from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_DETAIL.Receipt_No)),0)  " & Environment.NewLine &
        " -isnull((select sum(isnull(Receipt_Amount,0)) from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Applied_Receipt=TSPL_Customer_Invoice_Head.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Receipts' and TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No) and TSPL_Customer_Invoice_Head.Document_Type='C' and isnull(TSPL_RECEIPT_HEADER.Applied_Receipt,'')<>'' AND Receipt_No <> '" + fndRcptNo.Value + "'),0)  " & Environment.NewLine &
        " -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No " & Environment.NewLine &
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  ),0) " & Environment.NewLine &
        " -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo " & Environment.NewLine &
        " LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No ),0) " & Environment.NewLine &
        " -isnull((select sum(isnull(Adjustment_Amount,0)) from TSPL_Receipt_Adjustment_Header where TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_Customer_Invoice_Head.Document_No),0)) as [Balance Amount]  " & Environment.NewLine &
        " , '0.00' as [Apply_Amt],  Customer_Code as  Cust_Code  ,'C' as [Tag], Due_Date,  0 as EmptyTotal, ConvRate ,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Status =1 and TSPL_Customer_Invoice_Head.RefDocType<> 'REVALUATION ENTRY'  " & Environment.NewLine &
        " ) as xxx  left outer join TSPL_CUSTOMER_MASTER on xxx.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where     ( xxx.cust_Code ='" + fndCustomer.Value + "' )) XXX WHERE [Bal Amt]>0 " & Environment.NewLine &
        "and [Receipt Type]  ='Credit Note' and [Receipt Date]<='" & clsCommon.GetPrintDate(dtRcpt.Value, "dd/MMM/yyyy") & "' " & Environment.NewLine &
        " ) Final left outer join TSPL_Customer_Invoice_Head on Final.Code =TSPL_Customer_Invoice_Head.Document_No  "

        If ApplyCardSaleInvoiceOnlyWithCardSaleAdvance Then
            If chkForCardSale.Checked = True Then
                WhrCls += " and  Code in (Select distinct against_receipt_no from TSPL_BOOKING_PAYMENT_MODE_DETAIL )"
            Else
                WhrCls += " and  Code not in (Select distinct against_receipt_no from TSPL_BOOKING_PAYMENT_MODE_DETAIL )"
            End If
        End If


        txtDocumentNo.Value = clsCommon.ShowSelectForm("Payment@Payment", strQuery, "Code", "[Bal Amt]>0 " & WhrCls, txtDocumentNo.Value, "Code", isButtonClicked)
        lblBalAmt.Text = clsRcptEntryHeader.GetBalance(txtDocumentNo.Value, fndRcptNo.Value, Nothing)
        txtUnApplAmt.Text = clsCommon.myCdbl(lblBalAmt.Text)

        If clsCommon.myCdbl(lblBalAmt.Text) > 0 Then
            LoadBlankGrid(ddlTransType.SelectedValue)
        End If

        Dim strdocumentType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_Type from TSPL_Customer_Invoice_Head where Document_No='" & txtDocumentNo.Value & "'"))

        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_RECEIPT_HEADER.Bank_Code,TSPL_RECEIPT_HEADER.Payment_Code FROM TSPL_RECEIPT_HEADER  WHERE TSPL_RECEIPT_HEADER.Receipt_No ='" & txtDocumentNo.Value & "'")
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                fndBankCode.Value = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                fndPayType.Value = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
                fndBankCode.Enabled = False
                fndPayType.Enabled = False
            Else
                fndBankCode.Value = ""
                fndPayType.Value = ""
                fndBankCode.Enabled = True
                fndPayType.Enabled = True
            End If
        Else
            fndBankCode.Value = ""
            fndPayType.Value = ""
            fndBankCode.Enabled = True
            fndPayType.Enabled = True
        End If
        If clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal Then
            fndBankCode.Value = ""
            fndPayType.Value = ""
            fndBankCode.Enabled = False
            fndPayType.Enabled = False
        End If
        AutoApplyAmt(clsCommon.myCdbl(txtUnApplAmt.Text))
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            txtConversionRate.Enabled = False
            If clsCommon.CompairString(strdocumentType, "C") = CompairStringResult.Equal Then
                txtConversionRate.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_Customer_Invoice_Head.ConvRate,1) as ConvRate, isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AR_Invoice_No=TSPL_Customer_Invoice_Head.Document_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtRcpt.Value), "dd/MMM/yyyy hh:mm tt") + "'  order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_Customer_Invoice_Head where Document_No ='" & txtDocumentNo.Value & "' )xx"))
            Else
                txtConversionRate.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_RECEIPT_HEADER.ConvRate,1) as ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.receipt_no=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtRcpt.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_RECEIPT_HEADER where Receipt_No ='" & txtDocumentNo.Value & "' )xx"))
            End If
        Else
            txtConversionRate.Enabled = True
            txtConversionRate.Value = 1
        End If
    End Sub


    Private Sub FndSalesOrder__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndSalesOrder._MYValidating
        Try
            If ddlTransType.SelectedValue = "P" Then
                If clsCommon.myLen(fndCustomer.Value) > 0 Then
                    Dim qry As String = "Select TSPL_SD_SALES_ORDER_HEAD.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Document_Date as Date,TSPL_SD_SALES_ORDER_HEAD.SalesOrder_Type as [Sales Order Type],TSPL_SD_SALES_ORDER_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] ,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location as [Bill to Location],TSPL_LOCATION_MASTER.Location_Desc as [Bill to Location Description],Total_Amt as [Total Amount]  from TSPL_SD_SALES_ORDER_HEAD Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SD_SALES_ORDER_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code Left outer Join TSPL_LOCATION_MASTER on TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code  "
                    FndSalesOrder.Value = clsCommon.ShowSelectForm("salesOrderShow", qry, "Code", " TSPL_SD_SALES_ORDER_HEAD.status=0 and TSPL_SD_SALES_ORDER_HEAD.Customer_Code ='" + fndCustomer.Value + "' ", FndSalesOrder.Value, "Code", isButtonClicked)
                    txtUnApplAmt.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Amt  from TSPL_SD_SALES_ORDER_HEAD where Document_Code='" & FndSalesOrder.Value & "' "))
                Else
                    Throw New Exception("Please select customer first")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txttransfer_no__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txttransfer_no._MYValidating
        Try
            Dim qry As String = "select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Code,TSPL_CSA_TRANSFER_HEAD.Transfer_Date as [Date],TSPL_CSA_TRANSFER_HEAD.Description,TSPL_CSA_TRANSFER_HEAD.DELEVERY_ORDER_NO as [DO Code],TSPL_CSA_TRANSFER_HEAD.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CSA_TRANSFER_HEAD.Document_Amount as [Total Amount],TSPL_CSA_TRANSFER_HEAD.Total_Tax_Amt as [Total Tax Amount],TSPL_CSA_TRANSFER_HEAD.Total_Commission_Chrage as [Total Commission Amount] from TSPL_CSA_TRANSFER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code"
            Dim whrcls As String = " isnull(TSPL_CSA_TRANSFER_HEAD.status,0)=1 and TSPL_CSA_TRANSFER_HEAD.cust_code='" + fndCustomer.Value + "'"
            whrcls += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in (select isnull(Against_CSA_Transfer_Code,'') from TSPL_RECEIPT_HEADER where Receipt_No<>'" + fndRcptNo.Value + "')"

            txttransfer_no.Value = clsCommon.ShowSelectForm("TRNSFND", qry, "Code", whrcls, txttransfer_no.Value, "Code", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ddlSecDepositType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlSecDepositType.SelectedIndexChanged

    End Sub

    Private Sub fndBookingNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBookingNo._MYValidating
        Try
            If ddlTransType.SelectedValue = "P" Then
                If clsCommon.myLen(fndCustomer.Value) > 0 Then
                    Dim qry As String = "Select TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code as Code,CONVERT(varchar(10), TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date,103)+' '+ CONVERT(varchar(5), TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Date,114)  as Date,TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] ,TSPL_BOOKING_MASTER_PRODUCTSALE.Total_Amt as [Total Amount]  from TSPL_BOOKING_MASTER_PRODUCTSALE Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code "
                    fndBookingNo.Value = clsCommon.ShowSelectForm("salesOrderShow", qry, "Code", " TSPL_BOOKING_MASTER_PRODUCTSALE.status=1 and TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code ='" + fndCustomer.Value + "' ", fndBookingNo.Value, "Code", isButtonClicked)
                    txtUnApplAmt.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Amt  from TSPL_BOOKING_MASTER_PRODUCTSALE where Document_Code='" & fndBookingNo.Value & "' "))
                Else
                    Throw New Exception("Please select customer first")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnVoidCheck_Click(sender As Object, e As EventArgs) Handles btnVoidCheck.Click
        If clsCommon.myLen(fndRcptNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select document no to void check.", Me.Text)
            Exit Sub
        End If
        Dim obj As New clsRcptEntryHeader
        obj = clsRcptEntryHeader.GetData(Me.fndRcptNo.Value, NavigatorType.Current)
        If clsCommon.myLen(obj.Bank_Code) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Bank Code not found for selected document.", Me.Text)
            Exit Sub

        End If
        If clsPrintCheck.VoidCheck(obj.Bank_Code, obj.CHECK_CODE, "Receipt Entry", obj.Receipt_No) Then
            clsCommon.MyMessageBoxShow(Me, "done successfully", Me.Text)
        End If
    End Sub

    Private Sub txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocation._MYValidating
        Try
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtlocation.Value = clsCommon.ShowSelectForm("PELoc", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
            If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
            Else
                LblLocDesp.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Qry = "Select '' as [Receipt Date], '' as [Description], '' as [Customer Code], '' as [Bank Code], '' as [Receipt Type(P/O/F)], '' as [Payment Mode], '' as [Cheque No], '' as [Cheque Date], 0 as Amount,'' as [Location Code],'N' as [Security Deposit],'' as [Security Deposit Type],0 as [Bank Charges],0 as [Foreign Bank Charges],1 as [Conv Rate],'' as [Distributer Code]"
            transportSql.ExporttoExcel(Qry, "", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            funImport(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funImport(ByVal IsForPost As Boolean)
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = ""
        If transportSql.importExcel(gv, "Receipt Date", "Description", "Customer Code", "Bank Code", "Receipt Type(P/O/F)", "Payment Mode", "Cheque No", "Cheque Date", "Amount", "Location Code", "Security Deposit", "Security Deposit Type", "Bank Charges", "Foreign Bank Charges", "Conv Rate", "Distributer Code") Then
            Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                Dim obj As clsRcptEntryHeader
                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)
                    If clsCommon.myLen(grow.Cells("Receipt Date").Value) > 0 Then
                        obj = New clsRcptEntryHeader()
                        obj.Receipt_No = ""
                        obj.Entry_Desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Entry_Desc) > 250 Then
                            Throw New Exception("Description Length can not be more than 250.")
                        End If
                        obj.Receipt_Date = clsCommon.myCDate(grow.Cells("Receipt Date").Value)
                        obj.Receipt_Post_Date = obj.Receipt_Date
                        obj.Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                        If clsCommon.myLen(obj.Cust_Code) > 0 Then
                            obj.Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code  from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Cust_Code + "'", trans))
                            If clsCommon.myLen(obj.Cust_Code) <= 0 Then
                                Throw New Exception("Customer Code does not exist.")
                            End If
                            obj.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Cust_Code + "'", trans))
                        Else
                            Throw New Exception("Please select Customer Code.")
                        End If

                        obj.Distr_Code = clsCommon.myCstr(grow.Cells("Distributer Code").Value)
                        If clsCommon.myLen(obj.Distr_Code) > 0 Then
                            obj.Distr_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code  from TSPL_SECONDARY_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Distr_Code + "'", trans))
                            If clsCommon.myLen(obj.Distr_Code) <= 0 Then
                                Throw New Exception("Distributer Code- " & obj.Distr_Code & " does not exist.")
                            End If
                        End If

                        obj.Bank_Code = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                        If clsCommon.myLen(obj.Bank_Code) > 0 Then
                            obj.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE from TSPL_BANK_MASTER WHERE Bank_Code='" + obj.Bank_Code + "'", trans))
                            If clsCommon.myLen(obj.Bank_Code) <= 0 Then
                                Throw New Exception("Bank Code does not exist.")
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_bank_master.INACTIVE from TSPL_BANK_MASTER Where Bank_Code='" & obj.Bank_Code & "'", trans)), "Active") <> CompairStringResult.Equal Then
                                Throw New Exception("Bank Code should be Active .")
                            End If
                        Else
                            Throw New Exception("Please select Bank Code.")
                        End If
                        obj.Receipt_Type = clsCommon.myCstr(grow.Cells("Receipt Type(P/O/F)").Value)
                        If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal Then
                            obj.Receipt_Type = "P"
                        ElseIf clsCommon.CompairString(obj.Receipt_Type, "O") = CompairStringResult.Equal Then
                            obj.Receipt_Type = "O"
                        ElseIf clsCommon.CompairString(obj.Receipt_Type, "F") = CompairStringResult.Equal Then
                            obj.Receipt_Type = "F"
                        Else
                            Throw New Exception("Payment type can be 'O' or 'P' or 'F'.")
                        End If
                        obj.Payment_Code = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                        If clsCommon.myLen(obj.Payment_Code) > 0 Then
                            obj.Payment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PAYMENT_CODE.Payment_Code from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code='" + obj.Payment_Code + "'", trans))
                            If clsCommon.myLen(obj.Payment_Code) <= 0 Then
                                Throw New Exception("Receipt Mode does not exist.")
                            End If
                        Else
                            Throw New Exception("Enter Receipt Mode.")
                        End If
                        Dim strPayType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Type from TSPL_PAYMENT_CODE where Payment_Desc='" + obj.Payment_Code + "'", trans))
                        If clsCommon.CompairString(strPayType, "Cheque") = CompairStringResult.Equal Then
                            obj.Cheque_No = clsCommon.myCstr(grow.Cells("Cheque No").Value)
                            If clsCommon.myLen(obj.Cheque_No) > 0 Then
                                If clsCommon.myLen(obj.Cheque_No) < 6 Or clsCommon.myLen(obj.Cheque_No) > 20 Then
                                    Throw New Exception("Length of Cheque No should between 6-20.")
                                End If
                                Qry = "Select Receipt_No from TSPL_RECEIPT_HEADER Where Cheque_No='" & obj.Cheque_No & "'"
                                Qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry, trans))
                                If clsCommon.myLen(Qry) > 0 Then
                                    Throw New Exception("Cheque '" & obj.Cheque_No & "' is Already Exists Against Receipt No '" & Qry & "'")
                                End If
                                If clsCommon.myLen(grow.Cells("Cheque Date").Value) > 0 Then
                                    obj.Cheque_Date = CDate(grow.Cells("Cheque Date").Value)
                                Else
                                    Throw New Exception("Please enter Cheque Date.")
                                End If
                            Else
                                Throw New Exception("Cheque No can't be blank")



                            End If

                        End If
                        If clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                            Throw New Exception("Enter Receipt Amount.")
                        End If

                        obj.memorndmamt = 0.0
                        obj.CHECK_PRINT = 0
                        Dim Loc_Code As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                        If clsCommon.myLen(Loc_Code) > 0 Then
                            Loc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left " _
                                       & " outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code  Where Seg_No = '7' AND GIT='N' and Segment_code = '" & Loc_Code & "'", trans))
                            If Loc_Code = "" Then
                                Throw New Exception("Please Check Location Code dose not Exits") ' + LineNo + " does not exist. ")
                            End If
                        Else
                            Throw New Exception("Insert Location Code in All Rows ") ' + LineNo + ".")
                        End If
                        obj.Location_GL_Code = Loc_Code

                        obj.Receipt_Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                        obj.UnApply_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value)
                        obj.Balance_Amt = clsCommon.myCdbl(grow.Cells("Amount").Value)
                        Dim Sec_Deposit As String = clsCommon.myCstr(grow.Cells("Security Deposit").Value)
                        If clsCommon.CompairString(Sec_Deposit, "N") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Sec_Deposit, "Y") <> CompairStringResult.Equal Then
                            Throw New Exception("Security Deposit must be Y or N  at line no: " & (grow.Index + 1) & "")
                        End If
                        obj.SecurityDeposit = Sec_Deposit
                        obj.SecurityDepositType = clsCommon.myCstr(grow.Cells("Security Deposit Type").Value)
                        If clsCommon.myLen(obj.SecurityDepositType) > 0 Then
                            If clsCommon.CompairString(obj.SecurityDepositType, "S") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.SecurityDepositType, "C") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.SecurityDepositType, "R") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.SecurityDepositType, "O") <> CompairStringResult.Equal Then
                                Throw New Exception("Security Deposit Type must be any of (S,C,R,O) at line no: " & (grow.Index + 1) & "")
                            End If
                        End If

                        If clsCommon.CompairString(obj.Receipt_Type, "P") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.SecurityDeposit, "Y") = CompairStringResult.Equal Then
                            If clsCommon.myLen(obj.SecurityDepositType) <= 0 Then
                                Throw New Exception("Security Deposit Type must be any of (S,C,R,O) at line no: " & (grow.Index + 1) & "")
                            End If
                        End If

                        If clsCommon.CompairString(obj.Receipt_Type, "O") = CompairStringResult.Equal Then
                            obj.SecurityDepositType = ""
                        End If

                        obj.IsChkReverse = "N"
                        obj.Bank_Charges_Amt = clsCommon.myCdbl(grow.Cells("Bank Charges").Value)
                        obj.Foreign_Bank_Charges_Amt = clsCommon.myCdbl(grow.Cells("Foreign Bank Charges").Value)
                        obj.CURRENCY_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CURRENCY_CODE from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Cust_Code & "'", trans))
                        obj.BASE_CURRENCY_CODE = objCommonVar.BaseCurrencyCode
                        obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount
                        obj.ConvRateOld = 1
                        obj.ConvRate = IIf(clsCommon.myCdbl(grow.Cells("Conv Rate").Value) <= 0, 1, clsCommon.myCdbl(grow.Cells("Conv Rate").Value))
                        If clsCommon.CompairString(obj.CURRENCY_CODE, obj.BASE_CURRENCY_CODE) = CompairStringResult.Equal Then
                            obj.ConvRate = 1
                        End If
                        obj.RECEIVED_AMOUNT_BASE_CURRENCY = obj.Receipt_Amount * obj.ConvRate
                        obj.Applied_Receipt = ""

                        obj.ArrTr = Nothing
                        obj.SaveData(obj, True, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub dgvReceipt_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles dgvReceipt.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If clsCommon.CompairString(dgvReceipt.CurrentRow.Cells(colApply).Value, "Yes") = CompairStringResult.Equal Then
                    dgvReceipt.CurrentRow.Cells(colAppliedAmt).ReadOnly = False
                Else
                    dgvReceipt.CurrentRow.Cells(colAppliedAmt).ReadOnly = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub AutoInvoice(ByVal Customer As String, ByVal ReceiptType As String)
        Dim DocNo As String = Nothing
        Try
            inSideLoadData = True
            If clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                Dim WhrCls As String = String.Empty
                If clsCommon.myLen(fndBankCode.Value) > 0 Then
                    WhrCls = " AND [Bank Code] ='" & fndBankCode.Value & "'"
                End If
                strQuery = "Select top 1 * from (" &
                   " Select Receipt_No as [Code], Entry_Desc as [Description], Receipt_Date as [Receipt Date], Case When Receipt_Type='P' Then 'Advance' When Receipt_Type='O' Then 'On Account' Else 'UnApplied' End As [Receipt Type], Receipt_Amount as [Receipt Amt],TSPL_RECEIPT_HEADER.Bank_Code As [Bank Code],TSPL_RECEIPT_HEADER.Payment_Code AS [Payment Code]," &
                   " Balance_Amt-ISNULL((Select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER RH WHERE RH.Posted<>'Y' AND RH.Receipt_Type ='A' AND RH.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code AND RH.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No AND RH.Receipt_No<>'" + fndRcptNo.Value + "'),0) as [Bal Amt] from TSPL_RECEIPT_HEADER WHERE Posted='Y' AND Cust_Code='" + Customer + "' AND Receipt_Type in ('P','O','U') AND Receipt_No <> '" + fndRcptNo.Value + "'" &
                   " ) Final where [Bal Amt]>0 order by [Receipt Date]"
                DocNo = clsDBFuncationality.getSingleValue(strQuery)
                txtDocumentNo.Value = DocNo
                lblBalAmt.Text = clsRcptEntryHeader.GetBalance(txtDocumentNo.Value, fndRcptNo.Value, Nothing)
                txtUnApplAmt.Text = clsCommon.myCdbl(lblBalAmt.Text)
                If clsCommon.myCdbl(lblBalAmt.Text) > 0 Then
                    LoadBlankGrid(ddlTransType.SelectedValue)
                End If

                If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_RECEIPT_HEADER.Bank_Code,TSPL_RECEIPT_HEADER.Payment_Code FROM TSPL_RECEIPT_HEADER  WHERE TSPL_RECEIPT_HEADER.Receipt_No ='" & txtDocumentNo.Value & "'")
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        fndBankCode.Value = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                        fndPayType.Value = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
                        fndBankCode.Enabled = False
                        fndPayType.Enabled = False
                    Else
                        fndBankCode.Value = ""
                        fndPayType.Value = ""
                        fndBankCode.Enabled = True
                        fndPayType.Enabled = True
                    End If
                Else
                    fndBankCode.Value = ""
                    fndPayType.Value = ""
                    fndBankCode.Enabled = True
                    fndPayType.Enabled = True
                End If

                AutoApplyAmt(clsCommon.myCdbl(txtUnApplAmt.Text))
                If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                    txtConversionRate.Enabled = False
                    txtConversionRate.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_RECEIPT_HEADER.ConvRate,1) as ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.receipt_no=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtRcpt.Value), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_RECEIPT_HEADER where Receipt_No ='" & txtDocumentNo.Value & "' )xx"))
                Else
                    txtConversionRate.Enabled = True
                    txtConversionRate.Value = 1
                End If
                If clsCommon.myLen(DocNo) > 0 Then
                    If clsCommon.myLen(fndCustomer.Value) > 0 Then
                        If clsCommon.CompairString(ddlTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(ddlTransType.SelectedValue, "A") = CompairStringResult.Equal Then
                            IsGobtnClicked = True
                            funFillGrid(fndCustomer.Value)
                            IsGobtnClicked = False
                            txtRcptAmt.Text = "0.00"
                            AutoApplyAmt(clsCommon.myCdbl(txtUnApplAmt.Text))
                        End If
                    End If
                    If dgvReceipt.Rows.Count > 0 Then
                        btnAdjment.Enabled = True
                    End If
                End If

                inSideLoadData = False
                If clsCommon.myLen(DocNo) > 0 Then
                    If IsFirstTimeSave Then
                        savedata()

                    End If


                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "select Apply Document ", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            inSideLoadData = False
        End Try


    End Sub

    Private Sub txtDONo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDONo._MYValidating
        GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)

        If GSTStatus AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "P") = CompairStringResult.Equal Then
            Dim whrcls As String = String.Empty
            Dim gstdate As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicableDate, clsFixedParameterCode.GSTApplicableDate, Nothing))

            Dim strreceiptrhroughSo As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowReceiptThroughSO, clsFixedParameterCode.AllowReceiptThroughSO, Nothing))

            If DOTaggingForDairySaleModule = True Then

                Qry = "Select Final.* from (Select zz.Document_No as Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt from  (" & Environment.NewLine &
                 " SELECT TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No ,Customer_Code ,Total_Amt as delieveryAmt " & Environment.NewLine &
                 " ,0 as Receiptamt  FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & Environment.NewLine &
                 " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " & Environment.NewLine &
                 " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & Environment.NewLine &
                 " and   isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,'') <>TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " & Environment.NewLine &
                 " AND TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date >=convert(date,'" & gstdate & "',103) " & Environment.NewLine &
                 " UNION ALL " & Environment.NewLine &
                 " SELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt  FROM TSPL_RECEIPT_HEADER where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz " & Environment.NewLine &
                 " group by Document_No) Final "

            Else

                If clsCommon.CompairString(strreceiptrhroughSo, "0") = CompairStringResult.Equal Then
                    Qry = " Select Final.* from (Select zz.Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt from  (" & Environment.NewLine &
                    " sELECT TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code ,Customer_Code ,Total_Amt as delieveryAmt,0 as Receiptamt  FROM TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE " & Environment.NewLine &
                    " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " & Environment.NewLine &
                    " where TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted=1 and  isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.close_yn='N'" & Environment.NewLine &
                    " and   isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,'') <>TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " & Environment.NewLine &
                    " AND TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Date >=convert(date,'" & gstdate & "',103)" & Environment.NewLine &
                    " UNION ALL" & Environment.NewLine &
                    " sELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt  FROM TSPL_RECEIPT_HEADER where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz" & Environment.NewLine &
                    " group by Document_Code) Final "
                Else
                    Qry = " Select Final.* from (Select zz.Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt from  (" & Environment.NewLine &
                    " sELECT TSPL_SD_SALES_ORDER_HEAD.Document_Code ,Customer_Code ,Total_Amt as delieveryAmt,0 as Receiptamt  FROM TSPL_SD_SALES_ORDER_HEAD " & Environment.NewLine &
                    " left outer join TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE  on TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Against_Sales_Order =TSPL_SD_SALES_ORDER_HEAD.Document_Code " & Environment.NewLine &
                    " where TSPL_SD_SALES_ORDER_HEAD.Status =1 and TSPL_SD_SALES_ORDER_HEAD.close_yn='N' " & Environment.NewLine &
                    " and isnull(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Against_Sales_Order ,'') <>TSPL_SD_SALES_ORDER_HEAD.Document_Code " & Environment.NewLine &
                    " AND TSPL_SD_SALES_ORDER_HEAD.Document_Date >=convert(date,'" & gstdate & "',103) AND Trans_Type='PS' " & Environment.NewLine &
                    " UNION ALL" & Environment.NewLine &
                    " sELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt  FROM TSPL_RECEIPT_HEADER where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz" & Environment.NewLine &
                    " group by Document_Code) Final "
                End If
            End If
            whrcls = " (Final.delieveryAmt - Final.Receiptamt) > 0 "
            If clsCommon.myLen(clsCommon.myCstr(fndCustomer.Value)) > 0 Then
                whrcls += " and Final.Customer_Code ='" & clsCommon.myCstr(fndCustomer.Value) & "' "
            End If


            txtDONo.Value = clsCommon.ShowSelectForm("PSDelieverOrderFinder", Qry, "Document_Code", whrcls, clsCommon.myCstr(txtDONo.Value), "Document_Code", isButtonClicked)
            If clsCommon.myLen(txtDONo.Value) > 0 Then
                FillDOofProductItems()
            Else
                txtDONo.Value = ""
                txtTaxGroup.Value = ""
                lblTaxGrpName.Text = ""
                LblDOTotalAmount.Text = 0
                lblDOTotalAdditionalCharge.Text = 0
                txtDocument_ForAdvanceDoc.Value = ""
                lblDO_Location.Text = ""
                lblReceiptThroughSO.Text = ""
                lblDOTotalTaxAmt.Text = 0
                LoadBlankGridDOItemDetail()
                LoadBlankGridTax()
            End If
        End If
        If clsCommon.myLen(txtDONo.Value) > 0 Then
            txtTaxGroup.Enabled = False
        End If
    End Sub

    Sub FillDOofProductItems()
        LoadBlankGridDOItemDetail()
        LoadBlankGridTax()

        Dim DblReceiptTaxAmt1 As Double = 0
        Dim DblReceiptTaxAmt2 As Double = 0
        Dim DblReceiptTaxAm3 As Double = 0
        Dim DblReceiptTaxAmt4 As Double = 0
        Dim DblReceiptTaxAmt5 As Double = 0
        Dim DblReceiptTaxAmt6 As Double = 0
        Dim DblReceiptTaxAmt7 As Double = 0
        Dim DblReceiptTaxAmt8 As Double = 0
        Dim DblReceiptTaxAmt9 As Double = 0
        Dim DblReceiptTaxAmt10 As Double = 0



        If clsCommon.myLen(txtDONo.Value) > 0 Then

            Dim strreceiptrhroughSo As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowReceiptThroughSO, clsFixedParameterCode.AllowReceiptThroughSO, Nothing))

            If DOTaggingForDairySaleModule = True Then

                Qry = "   Select Final.* from (Select zz.Document_no as Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt, max(zz.Tax_Group )as Tax_Group,max(zz.Tax_Group_Desc )as Tax_Group_Desc " & Environment.NewLine &
                     ",sum(AddCharge) as AddCharge,max(zz.Bill_To_Location )as Bill_To_Location  " & Environment.NewLine &
                     " from  (SELECT TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt as delieveryAmt,0 as Receiptamt,TSPL_SD_SHIPMENT_HEAD.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,(TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt1 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt2 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt3  +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt4 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt5 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt6 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt7 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt8 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt9 +TSPL_SD_SHIPMENT_HEAD.Add_Charge_Amt10) as AddCharge,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location  " & Environment.NewLine &
                     " FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE  " & Environment.NewLine &
                    " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No  " & Environment.NewLine &
                     " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  " & Environment.NewLine &
                     " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_SD_SHIPMENT_HEAD.Tax_Group  " & Environment.NewLine &
                     " and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S'  " & Environment.NewLine &
                    " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted=1 and  isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N'  " & Environment.NewLine &
                    " and   isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,'') <>TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No  " & Environment.NewLine &
                    " AND TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No ='" & clsCommon.myCstr(txtDONo.Value) & "'   " & Environment.NewLine &
                     " UNION ALL  " & Environment.NewLine &
                    " SELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt,TSPL_RECEIPT_HEADER.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc " & Environment.NewLine &
                    " , 0 as AddCharge,isnull(TSPL_RECEIPT_HEADER.SO_Location_Code,'') as Bill_To_Location " & Environment.NewLine &
                      " FROM TSPL_RECEIPT_HEADER   " & Environment.NewLine &
                     " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_RECEIPT_HEADER.Tax_Group and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S'  " & Environment.NewLine &
                     " where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')='" & clsCommon.myCstr(txtDONo.Value) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz  " & Environment.NewLine &
                     " group by Document_no) Final where (Final.delieveryAmt - Final.Receiptamt) > 0  "

                lblReceiptThroughSO.Text = "DO"
            Else

                If clsCommon.CompairString(strreceiptrhroughSo, "0") = CompairStringResult.Equal Then
                    Qry = " Select Final.* from (Select zz.Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt, max(zz.Tax_Group )as Tax_Group,max(zz.Tax_Group_Desc )as Tax_Group_Desc,sum(AddCharge) as AddCharge,max(zz.Bill_To_Location )as Bill_To_Location from  (" & Environment.NewLine &
                    " sELECT TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code ,Customer_Code ,Total_Amt as delieveryAmt,0 as Receiptamt,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Total_Add_Charge,0) as AddCharge,TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Bill_To_Location  FROM TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE " & Environment.NewLine &
                    " left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " & Environment.NewLine &
                    " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Tax_Group and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S' " & Environment.NewLine &
                    " where TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Posted=1 and  isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.close_yn='N'" & Environment.NewLine &
                    " and   isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,'') <>TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " & Environment.NewLine &
                    " AND TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "'  " & Environment.NewLine &
                    " UNION ALL" & Environment.NewLine &
                    " sELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt,TSPL_RECEIPT_HEADER.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc, 0 as AddCharge,isnull(TSPL_RECEIPT_HEADER.SO_Location_Code,'') as Bill_To_Location  FROM TSPL_RECEIPT_HEADER  " & Environment.NewLine &
                    " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_RECEIPT_HEADER.Tax_Group and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S' " & Environment.NewLine &
                    " where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')='" & clsCommon.myCstr(txtDONo.Value) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz" & Environment.NewLine &
                    " group by Document_Code) Final where (Final.delieveryAmt - Final.Receiptamt) > 0"
                    lblReceiptThroughSO.Text = "DO"
                Else
                    Qry = " Select Final.* from (Select zz.Document_Code,max(zz.Customer_Code)as Customer_Code ,sum(isnull(zz.delieveryAmt ,0)) as delieveryAmt,sum(isnull(zz.Receiptamt ,0)) as Receiptamt, max(zz.Tax_Group )as Tax_Group,max(zz.Tax_Group_Desc )as Tax_Group_Desc,sum(AddCharge) as AddCharge,max(zz.Bill_To_Location )as Bill_To_Location from  (" & Environment.NewLine &
                    " sELECT TSPL_SD_SALES_ORDER_HEAD.Document_Code ,Customer_Code ,Total_Amt as delieveryAmt,0 as Receiptamt, " & Environment.NewLine &
                    " TSPL_SD_SALES_ORDER_HEAD.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,isnull(TSPL_SD_SALES_ORDER_HEAD.Total_Add_Charge,0) as AddCharge,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location " & Environment.NewLine &
                    " FROM TSPL_SD_SALES_ORDER_HEAD " & Environment.NewLine &
                    " left outer join TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE  on TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Against_Sales_Order =TSPL_SD_SALES_ORDER_HEAD.Document_Code " & Environment.NewLine &
                    " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_SD_SALES_ORDER_HEAD.Tax_Group and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S' " & Environment.NewLine &
                    " where TSPL_SD_SALES_ORDER_HEAD.Status =1 and TSPL_SD_SALES_ORDER_HEAD.close_yn='N' and isnull(TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Against_Sales_Order ,'') <>TSPL_SD_SALES_ORDER_HEAD.Document_Code " & Environment.NewLine &
                    " AND TSPL_SD_SALES_ORDER_HEAD.Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "' AND TSPL_SD_SALES_ORDER_HEAD.Trans_Type='PS'  " & Environment.NewLine &
                    " UNION ALL" & Environment.NewLine &
                    " sELECT Delivery_Code_PS as Document_Code,Cust_Code as Customer_Code ,0 as delieveryAmt,Receipt_Amount as Receiptamt,TSPL_RECEIPT_HEADER.Tax_Group,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc, 0 as AddCharge,isnull(TSPL_RECEIPT_HEADER.SO_Location_Code,'') as Bill_To_Location  FROM TSPL_RECEIPT_HEADER  " & Environment.NewLine &
                    " Left Outer Join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code =TSPL_RECEIPT_HEADER.Tax_Group and isnull(TSPL_TAX_GROUP_MASTER.Tax_Group_Type,'')='S' " & Environment.NewLine &
                    " where isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')<>'' and Receipt_Type ='P' and isnull(TSPL_RECEIPT_HEADER. Delivery_Code_PS,'')='" & clsCommon.myCstr(txtDONo.Value) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' ) zz" & Environment.NewLine &
                    " group by Document_Code) Final where (Final.delieveryAmt - Final.Receiptamt) > 0"
                    lblReceiptThroughSO.Text = "SO"
                End If

            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                LblDOTotalAmount.Text = clsCommon.myCdbl(dt.Rows(0)("delieveryAmt")) - clsCommon.myCdbl(dt.Rows(0)("Receiptamt"))
                lblDOTotalAdditionalCharge.Text = clsCommon.myCdbl(dt.Rows(0)("AddCharge"))
                lblDO_Location.Text = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))

            End If

            If clsCommon.myLen(txtTaxGroup.Value) > 0 Then

                If DOTaggingForDairySaleModule = True Then

                    Qry = "select Document_Code ,Line_No ,Row_Type ,Item_Code ,Qty ,Balance_Qty ,Unit_code,Item_Cost ,TAX1 ,TAX1_Amt ,TAX1_Base_Amt ,TAX1_Rate , " &
                       " tax2,TAX2_Base_Amt,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Base_Amt,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Base_Amt,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Base_Amt,TAX5_Rate," &
                       " TAX5_Amt,TAX6,TAX6_Base_Amt,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Base_Amt,TAX7_Rate,TAX7_Amt,TAX8," &
                       " TAX8_Base_Amt,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Base_Amt,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Base_Amt,TAX10_Rate,TAX10_Amt,Amount," &
                       " Disc_Per,Disc_Amt,Amt_Less_Discount,Total_Tax_Amt,Item_Net_Amt,MRP,Abatement_Per,Abatement_Amt,Scheme_Code,Scheme_Applicable,Scheme_Item," &
                       " FOC_Item,Item_Tax,Total_MRP_Amt,Total_Basic_Amt,Total_Disc_Amt,ActualRate,Conv_Factor,TotalItem_Weight,Landing_Cost " &
                       " from TSPL_SD_SHIPMENT_DETAIL where TSPL_SD_SHIPMENT_DETAIL.DELIVERY_CODE ='" & clsCommon.myCstr(txtDONo.Value) & "'"
                Else
                    If clsCommon.CompairString(strreceiptrhroughSo, "0") = CompairStringResult.Equal Then
                        Qry = "select Document_Code ,Line_No ,Row_Type ,Item_Code ,Qty ,Balance_Qty ,Unit_code,Item_Cost ,TAX1 ,TAX1_Amt ,TAX1_Base_Amt ,TAX1_Rate , " &
                        " tax2,TAX2_Base_Amt,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Base_Amt,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Base_Amt,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Base_Amt,TAX5_Rate," &
                        " TAX5_Amt,TAX6,TAX6_Base_Amt,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Base_Amt,TAX7_Rate,TAX7_Amt,TAX8," &
                        " TAX8_Base_Amt,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Base_Amt,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Base_Amt,TAX10_Rate,TAX10_Amt,Amount," &
                        " Disc_Per,Disc_Amt,Amt_Less_Discount,Total_Tax_Amt,Item_Net_Amt,MRP,Abatement_Per,Abatement_Amt,Scheme_Code,Scheme_Applicable,Scheme_Item," &
                        " FOC_Item,Item_Tax,Total_MRP_Amt,Total_Basic_Amt,Total_Disc_Amt,ActualRate,Conv_Factor,TotalItem_Weight,Landing_Cost " &
                        " from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE where TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE.Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "'"
                    Else
                        Qry = "select Document_Code ,Line_No ,Row_Type ,Item_Code ,Qty ,Balance_Qty ,Unit_code,Item_Cost ,TAX1 ,TAX1_Amt ,TAX1_Base_Amt ,TAX1_Rate , " &
                        " tax2,TAX2_Base_Amt,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Base_Amt,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Base_Amt,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Base_Amt,TAX5_Rate," &
                        " TAX5_Amt,TAX6,TAX6_Base_Amt,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Base_Amt,TAX7_Rate,TAX7_Amt,TAX8," &
                        " TAX8_Base_Amt,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Base_Amt,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Base_Amt,TAX10_Rate,TAX10_Amt,Amount," &
                        " Disc_Per,Disc_Amt,Amt_Less_Discount,Total_Tax_Amt,Item_Net_Amt,MRP,Abatement_Per,Abatement_Amt,Scheme_Code,Scheme_Applicable,Scheme_Item," &
                        " FOC_Item,Item_Tax,Total_MRP_Amt,Total_Basic_Amt,Total_Disc_Amt,ActualRate,Conv_Factor,TotalItem_Weight,Landing_Cost " &
                        " from TSPL_SD_SALES_ORDER_DETAIL where TSPL_SD_SALES_ORDER_DETAIL.Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "'"
                    End If
                End If

                dt = clsDBFuncationality.GetDataTable(Qry)

                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolDocument_Code).Value = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolLine_No).Value = clsCommon.myCdbl(dt.Rows(i)("Line_No"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolRow_Type).Value = clsCommon.myCstr(dt.Rows(i)("Row_Type"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolItem_Code).Value = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(adcolIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dt.Rows(i)("Item_Code")), Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(adcolIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(i)("Item_Code")), Nothing)
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolQty).Value = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolBalance_Qty).Value = clsCommon.myCdbl(dt.Rows(i)("Balance_Qty"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolUnit_code).Value = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolItem_Cost).Value = clsCommon.myCdbl(dt.Rows(i)("Item_Cost"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX1).Value = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX1_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX1_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX1_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(Adcoltax2).Value = clsCommon.myCstr(dt.Rows(i)("tax2"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX2_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX2_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX2_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX3).Value = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX3_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX3_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX3_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX4).Value = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX4_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX4_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX4_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX5).Value = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX5_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX5_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX5_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX6).Value = clsCommon.myCstr(dt.Rows(i)("TAX6"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX6_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX6_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX6_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX6_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX6_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX6_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX7).Value = clsCommon.myCstr(dt.Rows(i)("TAX7"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX7_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX7_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX7_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX7_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX7_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX7_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX8).Value = clsCommon.myCstr(dt.Rows(i)("TAX8"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX8_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX8_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX8_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX8_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX8_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX8_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX9).Value = clsCommon.myCstr(dt.Rows(i)("TAX9"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX9_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX9_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX9_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX9_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX9_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX9_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX10).Value = clsCommon.myCstr(dt.Rows(i)("TAX10"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX10_Base_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX10_Base_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX10_Rate).Value = clsCommon.myCdbl(dt.Rows(i)("TAX10_Rate"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTAX10_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("TAX10_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolAmount).Value = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolDisc_Per).Value = clsCommon.myCdbl(dt.Rows(i)("Disc_Per"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolDisc_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("Disc_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolAmt_Less_Discount).Value = clsCommon.myCdbl(dt.Rows(i)("Amt_Less_Discount"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTotal_Tax_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))

                    If DOTaggingForDairySaleModule = True Then

                        Qry = " Select sum(amount) as Item_Net_Amt  from " & Environment.NewLine &
                                " (select Document_NO ,Line_No ,'Item' as Row_Type,Item_Code ,AMOUNT  from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE where Document_NO ='" & clsCommon.myCstr(txtDONo.Value) & "' " & Environment.NewLine &
                                " and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "' " & Environment.NewLine &
                               " union all  " & Environment.NewLine &
                             " select Document_Code ,Line_No ,Row_Type ,Item_Code , ReceiptTotalAdvanceAmt * -1  from TSPL_RECEIPT_DETAIL_GST Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL_GST.Receipt_No  where isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS,'')  ='" & clsCommon.myCstr(txtDONo.Value) & "' and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' )zz  " & Environment.NewLine &
                               " group by zz.Document_no ,zz.Item_Code,Line_No  order by Line_No  "

                    Else
                        If clsCommon.CompairString(strreceiptrhroughSo, "0") = CompairStringResult.Equal Then
                            Qry = " Select sum(Item_Net_Amt) as Item_Net_Amt  from " & Environment.NewLine &
                            "(select Document_Code ,Line_No ,Row_Type ,Item_Code ,Item_Net_Amt  from TSPL_DELIVERY_ORDER_DETAIL_PRODUCTSALE where Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "' and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "' " & Environment.NewLine &
                            " union all" & Environment.NewLine &
                            " select Document_Code ,Line_No ,Row_Type ,Item_Code , ReceiptTotalAdvanceAmt * -1  from TSPL_RECEIPT_DETAIL_GST Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL_GST.Receipt_No  where isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS,'')  ='" & clsCommon.myCstr(txtDONo.Value) & "' and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' )zz " & Environment.NewLine &
                            "group by zz.Document_Code ,zz.Item_Code,Line_No  order by Line_No "
                        Else
                            Qry = " Select sum(Item_Net_Amt) as Item_Net_Amt  from " & Environment.NewLine &
                            "(select Document_Code ,Line_No ,Row_Type ,Item_Code ,Item_Net_Amt  from TSPL_SD_SALES_ORDER_DETAIL where Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "' and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "' " & Environment.NewLine &
                            " union all" & Environment.NewLine &
                            " select Document_Code ,Line_No ,Row_Type ,Item_Code , ReceiptTotalAdvanceAmt * -1  from TSPL_RECEIPT_DETAIL_GST Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL_GST.Receipt_No  where isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS,'')  ='" & clsCommon.myCstr(txtDONo.Value) & "' and Item_Code ='" & clsCommon.myCstr(dt.Rows(i)("Item_Code")) & "'  and TSPL_RECEIPT_HEADER.Receipt_No<>'" & clsCommon.myCstr(fndRcptNo.Value) & "' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N' )zz " & Environment.NewLine &
                            "group by zz.Document_Code ,zz.Item_Code,Line_No  order by Line_No "
                        End If
                    End If

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolItem_Net_Amt).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolMRP).Value = clsCommon.myCdbl(dt.Rows(i)("MRP"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolAbatement_Per).Value = clsCommon.myCdbl(dt.Rows(i)("Abatement_Per"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolAbatement_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("Abatement_Amt"))

                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolScheme_Code).Value = clsCommon.myCstr(dt.Rows(i)("Scheme_Code"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolScheme_Applicable).Value = clsCommon.myCstr(dt.Rows(i)("Scheme_Applicable"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolScheme_Item).Value = clsCommon.myCstr(dt.Rows(i)("Scheme_Item"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolFOC_Item).Value = clsCommon.myCstr(dt.Rows(i)("FOC_Item"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTotal_MRP_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("Total_MRP_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTotal_Disc_Amt).Value = clsCommon.myCdbl(dt.Rows(i)("Total_Disc_Amt"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolConv_Factor).Value = clsCommon.myCdbl(dt.Rows(i)("Conv_Factor"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolTotalItem_Weight).Value = clsCommon.myCdbl(dt.Rows(i)("TotalItem_Weight"))
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(AdcolLanding_Cost).Value = clsCommon.myCdbl(dt.Rows(i)("Landing_Cost"))




                Next

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX1"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX1")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX2"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("tax2"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX2")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX3"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX3")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX4"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX4")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX5"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX5")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX6"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX6"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX6")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX7"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX7"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX7")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX8"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX8"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX8")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX9"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX9"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX9")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))

                End If

                If (clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("TAX10"))) > 0) Then
                    gvTaxDetail.Rows.AddNew()
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dt.Rows(0)("TAX10"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(dt.Rows(0)("TAX10")))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
                    gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
                End If

                SetitemWiseTaxSetting(False, False)
                For ii As Integer = 0 To gvItem.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
                CalculateTaxAmountInAdvnce()
            End If


        End If

    End Sub

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                If clsCommon.myLen(gvItem.CurrentRow.Cells(AdcolItem_Code)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gvItem.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gvItem.Rows.Count - 1
                    If clsCommon.myLen(gvItem.Rows(intRowNo).Cells(AdcolItem_Code)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gvItem.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)


            Dim dblAmt As Double = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(AdcolAmt_Less_Discount).Value)
            Dim dblAmtAfterDis As Double = dblAmt
            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0

                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)


                        If gvItem.Rows(IntRowNo).Cells(AdcolFOC_Item).Value = 0 Then
                            dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                        End If

                    End If

                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If

                Else
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                    gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Nothing
                    'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                End If

            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
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
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function
    Private Function GetCurrentRowTaxRate(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function


    Sub LoadBlankGridTax()
        gvTaxDetail.Rows.Clear()
        gvTaxDetail.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gvTaxDetail.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gvTaxDetail.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = "{0:n4}"
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTaxDetail.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = True
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTaxDetail.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = "{0:n4}"
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTaxDetail.MasterTemplate.Columns.Add(repoTaxAmt)

        gvTaxDetail.AllowDeleteRow = True
        gvTaxDetail.AllowAddNewRow = False
        gvTaxDetail.ShowGroupPanel = False
        gvTaxDetail.AllowColumnReorder = False
        gvTaxDetail.AllowRowReorder = False
        gvTaxDetail.EnableSorting = False
        gvTaxDetail.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTaxDetail.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Sub LoadBlankGridDOItemDetail()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = AdcolLine_No
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoDocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocCode.FormatString = ""
        repoDocCode.HeaderText = "Document No"
        repoDocCode.Width = 70
        repoDocCode.Name = AdcolDocument_Code
        repoDocCode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoDocCode)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Row Type"
        repoRowType.Name = AdcolRow_Type
        repoRowType.Width = 50
        repoRowType.ReadOnly = True
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.MasterTemplate.Columns.Add(repoRowType)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = AdcolItem_Code
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = adcolIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIName)


        Dim repoIHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSN.FormatString = ""
        repoIHSN.HeaderText = "HSN Code"
        repoIHSN.Name = adcolIHSN
        repoIHSN.Width = 150
        repoIHSN.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIHSN)


        Dim repoBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = AdcolBalance_Qty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.WrapText = True
        repoQty.HeaderText = "Qty"
        repoQty.Name = AdcolQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnitcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitcode.FormatString = ""
        repoUnitcode.HeaderText = "Unit Code"
        repoUnitcode.Name = AdcolUnit_code
        repoUnitcode.IsVisible = False
        repoUnitcode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoUnitcode)

        Dim repoItemCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemCost = New GridViewDecimalColumn()
        repoItemCost.FormatString = ""
        repoItemCost.HeaderText = "Item Cost"
        repoItemCost.Name = AdcolItem_Cost
        repoItemCost.IsVisible = False
        repoItemCost.Minimum = 0
        repoItemCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemCost.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoItemCost)



        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = AdcolTAX1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax1)

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = AdcolTAX1_Base_Amt
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = AdcolTAX1_Rate
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = AdcolTAX1_Amt
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt1)


        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTaxAmt1Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1Receipt = New GridViewDecimalColumn()
        repoTaxAmt1Receipt.FormatString = ""
        repoTaxAmt1Receipt.HeaderText = "Tax Amt 1 Receipt"
        repoTaxAmt1Receipt.Name = AdcolTAX1_Amt_Receipt
        repoTaxAmt1Receipt.IsVisible = False
        repoTaxAmt1Receipt.ReadOnly = True
        repoTaxAmt1Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt1Receipt)


        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = Adcoltax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax2)

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = AdcolTAX2_Base_Amt
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = AdcolTAX2_Rate
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = AdcolTAX2_Amt
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable2)

        Dim repoTaxAmt2Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2Receipt = New GridViewDecimalColumn()
        repoTaxAmt2Receipt.FormatString = ""
        repoTaxAmt2Receipt.HeaderText = "Tax Amt 2 Receipt"
        repoTaxAmt2Receipt.Name = AdcolTAX2_Amt_Receipt
        repoTaxAmt2Receipt.IsVisible = False
        repoTaxAmt2Receipt.ReadOnly = True
        repoTaxAmt2Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt2Receipt)


        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = AdcolTAX3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax3)

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = AdcolTAX3_Base_Amt
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = AdcolTAX3_Rate
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = AdcolTAX3_Amt
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable3)


        Dim repoTaxAmt3Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3Receipt = New GridViewDecimalColumn()
        repoTaxAmt3Receipt.FormatString = ""
        repoTaxAmt3Receipt.HeaderText = "Tax Amt 3 Receipt"
        repoTaxAmt3Receipt.Name = AdcolTAX3_Amt_Receipt
        repoTaxAmt3Receipt.IsVisible = False
        repoTaxAmt3Receipt.ReadOnly = True
        repoTaxAmt3Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt3Receipt)


        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = AdcolTAX4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax4)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = AdcolTAX4_Base_Amt
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = AdcolTAX4_Rate
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = AdcolTAX4_Amt
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable4)


        Dim repoTaxAmt4Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4Receipt = New GridViewDecimalColumn()
        repoTaxAmt4Receipt.FormatString = ""
        repoTaxAmt4Receipt.HeaderText = "Tax Amt 4 Receipt"
        repoTaxAmt4Receipt.Name = AdcolTAX4_Amt_Receipt
        repoTaxAmt4Receipt.IsVisible = False
        repoTaxAmt4Receipt.ReadOnly = True
        repoTaxAmt4Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt4Receipt)


        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = AdcolTAX5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax5)

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = AdcolTAX5_Base_Amt
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = AdcolTAX5_Rate
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = AdcolTAX5_Amt
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable5)

        Dim repoTaxAmt5Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5Receipt = New GridViewDecimalColumn()
        repoTaxAmt5Receipt.FormatString = ""
        repoTaxAmt5Receipt.HeaderText = "Tax Amt 5 Receipt"
        repoTaxAmt5Receipt.Name = AdcolTAX5_Amt_Receipt
        repoTaxAmt5Receipt.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt5Receipt)


        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = AdcolTAX6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax6)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = AdcolTAX6_Base_Amt
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = AdcolTAX6_Rate
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = AdcolTAX6_Amt
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable6)


        Dim repoTaxAmt6Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6Receipt = New GridViewDecimalColumn()
        repoTaxAmt6Receipt.FormatString = ""
        repoTaxAmt6Receipt.HeaderText = "Tax Amt 6 Receipt"
        repoTaxAmt6Receipt.Name = AdcolTAX6_Amt_Receipt
        repoTaxAmt6Receipt.IsVisible = False
        repoTaxAmt6Receipt.ReadOnly = True
        repoTaxAmt6Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt6Receipt)


        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = AdcolTAX7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax7)

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = AdcolTAX7_Base_Amt
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = AdcolTAX7_Rate
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = AdcolTAX7_Amt
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable7)


        Dim repoTaxAmt7Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7Receipt = New GridViewDecimalColumn()
        repoTaxAmt7Receipt.FormatString = ""
        repoTaxAmt7Receipt.HeaderText = "Tax Amt 7 Receipt"
        repoTaxAmt7Receipt.Name = AdcolTAX7_Amt_Receipt
        repoTaxAmt7Receipt.IsVisible = False
        repoTaxAmt7Receipt.ReadOnly = True
        repoTaxAmt7Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt7Receipt)



        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = AdcolTAX8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax8)

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = AdcolTAX8_Base_Amt
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = AdcolTAX8_Rate
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = AdcolTAX8_Amt
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable8)

        Dim repoTaxAmt8Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8Receipt = New GridViewDecimalColumn()
        repoTaxAmt8Receipt.FormatString = ""
        repoTaxAmt8Receipt.HeaderText = "Tax Amt 8 Receipt"
        repoTaxAmt8Receipt.Name = AdcolTAX8_Amt_Receipt
        repoTaxAmt8Receipt.IsVisible = False
        repoTaxAmt8Receipt.ReadOnly = True
        repoTaxAmt8Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt8Receipt)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = AdcolTAX9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax9)

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = AdcolTAX9_Base_Amt
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = AdcolTAX9_Rate
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = AdcolTAX9_Amt
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt9)


        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable9)

        Dim repoTaxAmt9Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9Receipt = New GridViewDecimalColumn()
        repoTaxAmt9Receipt.FormatString = ""
        repoTaxAmt9Receipt.HeaderText = "Tax Amt 9 Receipt"
        repoTaxAmt9Receipt.Name = AdcolTAX9_Amt_Receipt
        repoTaxAmt9Receipt.IsVisible = False
        repoTaxAmt9Receipt.ReadOnly = True
        repoTaxAmt9Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt9Receipt)


        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = AdcolTAX10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTax10)

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = AdcolTAX10_Base_Amt
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = AdcolTAX10_Rate
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = AdcolTAX10_Amt
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoIsTaxable10)

        Dim repoTaxAmt10Receipt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10Receipt = New GridViewDecimalColumn()
        repoTaxAmt10Receipt.FormatString = ""
        repoTaxAmt10Receipt.HeaderText = "Tax Amt 10 Receipt"
        repoTaxAmt10Receipt.Name = AdcolTAX10_Amt_Receipt
        repoTaxAmt10Receipt.IsVisible = False
        repoTaxAmt10Receipt.ReadOnly = True
        repoTaxAmt10Receipt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTaxAmt10Receipt)


        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = AdcolAmount
        repoAmount.WrapText = True
        repoAmount.Width = 80
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.VisibleInColumnChooser = False
        repoAmount.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoAmount)


        Dim repoDisc_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisc_Per.FormatString = ""
        repoDisc_Per.HeaderText = "Disc Per"
        repoDisc_Per.Name = AdcolDisc_Per
        repoDisc_Per.Width = 80
        repoDisc_Per.ReadOnly = True
        repoDisc_Per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoDisc_Per)

        Dim repoDisc_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisc_Amt.FormatString = ""
        repoDisc_Amt.HeaderText = "Disc Amount"
        repoDisc_Amt.Name = AdcolDisc_Amt
        repoDisc_Amt.Width = 80
        repoDisc_Amt.ReadOnly = True
        repoDisc_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoDisc_Amt)

        Dim repoAmt_Less_Discount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt_Less_Discount.FormatString = ""
        repoAmt_Less_Discount.HeaderText = "Amount Less Discount"
        repoAmt_Less_Discount.Name = AdcolAmt_Less_Discount
        repoAmt_Less_Discount.Width = 80
        repoAmt_Less_Discount.ReadOnly = True
        repoAmt_Less_Discount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoAmt_Less_Discount)

        Dim repoTotal_Tax_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotal_Tax_Amt.FormatString = ""
        repoTotal_Tax_Amt.HeaderText = "Total Tax Amount"
        repoTotal_Tax_Amt.Name = AdcolTotal_Tax_Amt
        repoTotal_Tax_Amt.Width = 80
        repoTotal_Tax_Amt.ReadOnly = True
        repoTotal_Tax_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTotal_Tax_Amt)


        Dim repoItem_Net_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItem_Net_Amt.FormatString = ""
        repoItem_Net_Amt.HeaderText = "Item Net Amount"
        repoItem_Net_Amt.Name = AdcolItem_Net_Amt
        repoItem_Net_Amt.Width = 80
        repoItem_Net_Amt.ReadOnly = True
        repoItem_Net_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoItem_Net_Amt)

        Dim repoReceiptAdvance As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReceiptAdvance.FormatString = "{0:n4}"
        repoReceiptAdvance.HeaderText = "Receipt Advance"
        repoReceiptAdvance.Name = AdcolReceiptAdvance
        repoReceiptAdvance.Width = 80
        repoReceiptAdvance.ReadOnly = True
        repoReceiptAdvance.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReceiptAdvance)

        Dim repoReceiptTotalTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReceiptTotalTax.FormatString = "{0:n4}"
        repoReceiptTotalTax.HeaderText = "Receipt Total Tax"
        repoReceiptTotalTax.Name = AdcolReceiptTotalTax
        repoReceiptTotalTax.Width = 80
        repoReceiptTotalTax.ReadOnly = True
        repoReceiptTotalTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReceiptTotalTax)

        Dim repoReceiptTotalAdvanceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReceiptTotalAdvanceAmt.FormatString = "{0:n4}"
        repoReceiptTotalAdvanceAmt.HeaderText = "Receipt Total Advance"
        repoReceiptTotalAdvanceAmt.Name = AdcolReceiptTotalAdvanceAmt
        repoReceiptTotalAdvanceAmt.Width = 80
        repoReceiptTotalAdvanceAmt.ReadOnly = True
        repoReceiptTotalAdvanceAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReceiptTotalAdvanceAmt)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = AdcolMRP
        repoMRP.WrapText = True
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAbatement_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatement_Per.FormatString = ""
        repoAbatement_Per.HeaderText = "Abatement Per"
        repoAbatement_Per.Name = AdcolAbatement_Per
        repoAbatement_Per.WrapText = True
        repoAbatement_Per.Width = 80
        repoAbatement_Per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAbatement_Per.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoAbatement_Per)

        Dim repoAbatement_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatement_Amt.FormatString = ""
        repoAbatement_Amt.HeaderText = "Abatement Amount"
        repoAbatement_Amt.Name = AdcolAbatement_Amt
        repoAbatement_Amt.WrapText = True
        repoAbatement_Amt.Width = 80
        repoAbatement_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAbatement_Amt.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoAbatement_Amt)

        Dim repoScheme_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme_Code.FormatString = ""
        repoScheme_Code.HeaderText = "Scheme Code "
        repoScheme_Code.Name = AdcolScheme_Code
        repoScheme_Code.ReadOnly = True
        repoScheme_Code.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoScheme_Code)

        Dim repoScheme_Applicable As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme_Applicable.FormatString = ""
        repoScheme_Applicable.HeaderText = "Scheme Applicable"
        repoScheme_Applicable.Name = AdcolScheme_Applicable
        repoScheme_Applicable.ReadOnly = True
        repoScheme_Applicable.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoScheme_Applicable)


        Dim repoScheme_Item As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme_Item.FormatString = ""
        repoScheme_Item.HeaderText = "Scheme Item"
        repoScheme_Item.Name = AdcolScheme_Item
        repoScheme_Item.ReadOnly = True
        repoScheme_Item.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoScheme_Item)


        Dim repoFOC_Item As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFOC_Item.FormatString = ""
        repoFOC_Item.HeaderText = "FOC Item"
        repoFOC_Item.Name = AdcolFOC_Item
        repoFOC_Item.ReadOnly = True
        repoFOC_Item.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoFOC_Item)


        Dim repoItem_Tax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItem_Tax.FormatString = ""
        repoItem_Tax.HeaderText = "Item Tax"
        repoItem_Tax.Name = AdcolItem_Tax
        repoItem_Tax.WrapText = True
        repoItem_Tax.Width = 80
        repoItem_Tax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItem_Tax.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoItem_Tax)

        Dim repoTotal_MRP_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotal_MRP_Amt.FormatString = ""
        repoTotal_MRP_Amt.HeaderText = "Total MRP Amt"
        repoTotal_MRP_Amt.Name = AdcolTotal_MRP_Amt
        repoTotal_MRP_Amt.WrapText = True
        repoTotal_MRP_Amt.Width = 80
        repoTotal_MRP_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotal_MRP_Amt.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTotal_MRP_Amt)

        Dim repoTotal_Basic_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotal_Basic_Amt.FormatString = ""
        repoTotal_Basic_Amt.HeaderText = "Total Basic Amount"
        repoTotal_Basic_Amt.Name = AdcolTotal_Basic_Amt
        repoTotal_Basic_Amt.WrapText = True
        repoTotal_Basic_Amt.Width = 80
        repoTotal_Basic_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotal_Basic_Amt.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTotal_Basic_Amt)

        Dim repoTotal_Disc_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotal_Disc_Amt.FormatString = ""
        repoTotal_Disc_Amt.HeaderText = "Total Disc Amount"
        repoTotal_Disc_Amt.Name = AdcolTotal_Disc_Amt
        repoTotal_Disc_Amt.WrapText = True
        repoTotal_Disc_Amt.Width = 80
        repoTotal_Disc_Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotal_Disc_Amt.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTotal_Disc_Amt)

        Dim repoActualRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualRate.FormatString = ""
        repoActualRate.HeaderText = "Actual Rate"
        repoActualRate.Name = AdcolActualRate
        repoActualRate.WrapText = True
        repoActualRate.Width = 80
        repoActualRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualRate.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoActualRate)

        Dim repoConv_Factor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv_Factor.FormatString = ""
        repoConv_Factor.HeaderText = "Conv Factor"
        repoConv_Factor.Name = AdcolConv_Factor
        repoConv_Factor.WrapText = True
        repoConv_Factor.Width = 80
        repoConv_Factor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConv_Factor.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoConv_Factor)

        Dim repoTotalItem_Weight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalItem_Weight.FormatString = ""
        repoTotalItem_Weight.HeaderText = "Total Item Weight"
        repoTotalItem_Weight.Name = AdcolTotalItem_Weight
        repoTotalItem_Weight.WrapText = True
        repoTotalItem_Weight.Width = 80
        repoTotalItem_Weight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalItem_Weight.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTotalItem_Weight)

        Dim repoLanding_Cost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLanding_Cost.FormatString = ""
        repoLanding_Cost.HeaderText = "Landing Cost"
        repoLanding_Cost.Name = AdcolLanding_Cost
        repoLanding_Cost.WrapText = True
        repoLanding_Cost.Width = 80
        repoLanding_Cost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLanding_Cost.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoLanding_Cost)


        gvItem.AllowAddNewRow = False
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = True
        gvItem.AllowRowReorder = False
        gvItem.EnableSorting = False
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.TableElement.TableHeaderHeight = 40

        MyBase.ReStoreGridLayoutMain(gvItem)
    End Sub


    Sub CalculateTaxAmountInAdvnce()
        Dim DblReceiptAmt As Double = 0
        Dim dblDOTotalAmt As Double = 0
        Dim dblTotalRateOfAllTaxes As Double = 0
        Dim dblTotalAmountforNonTable As Double = 0
        Dim dblDOTotalAddCharge As Double = 0
        DblReceiptAmt = clsCommon.myCdbl(txtUnApplAmt.Text)
        dblDOTotalAmt = clsCommon.myCdbl(LblDOTotalAmount.Text)
        'dblDOTotalAddCharge = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select isnull(Total_Add_Charge,0) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "' "))
        dblDOTotalAddCharge = clsCommon.myCdbl(lblDOTotalAdditionalCharge.Text)
        dblDOTotalAmt = dblDOTotalAmt - dblDOTotalAddCharge
        Dim dbltotalTaxamount As Double = 0
        If DblReceiptAmt <= 0 Then
            DblReceiptAmt = dblDOTotalAmt
        End If

        If DblReceiptAmt > 0 Then

            For i As Integer = 0 To gvItem.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolScheme_Item).Value), "N") = CompairStringResult.Equal Then

                    gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value = clsCommon.myCdbl((DblReceiptAmt * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolItem_Net_Amt).Value)) / dblDOTotalAmt)

                    ' UpdateCurrentRowForTaxReceipt(i, DblReceiptAmt)
                    dblTotalRateOfAllTaxes = getcurrentTaxReceiptRateNonTaxable(i)
                    'dbl()
                    gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value = clsCommon.myCdbl((clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value) * 100.0) / (100.0 + clsCommon.myCdbl(dblTotalRateOfAllTaxes)))


                    'gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value)) / 100.0
                    'gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)) / 100.0


                    'clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim dblTotTaxAmtTaxInclusive As Double = 0
                    For ii As Integer = 10 To 1 Step -1
                        Dim Strii As String = clsCommon.myCstr(ii)
                        Dim dbltaxamt As Double = 0
                        Dim dblreceiptamtforcalculation As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value)
                        Dim dblTotTaxRate As Double = dblTotalRateOfAllTaxes
                        Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                        If clsCommon.myLen(strTaxCode) > 0 Then
                            Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                            Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                            If clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value) Then
                                dbltaxamt = clsCommon.myCdbl(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * dblTaxRate) / (100 + dblTaxRate)
                            Else
                                dbltaxamt = clsCommon.myCdbl(dblreceiptamtforcalculation * dblTaxRate) / 100
                                'dblTotTaxRate = dblTotTaxRate - dblTaxRate
                            End If
                            dblTotTaxAmtTaxInclusive += dbltaxamt
                            gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Math.Round(dbltaxamt, 4)
                            ' dbltaxamt = dblreceiptamtforcalculation - dbltaxamt
                        End If
                    Next
                    'gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value = DblReceiptAmt - dblTotTaxAmtTaxInclusive
                    gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value) - dblTotTaxAmtTaxInclusive
                    gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value = dblTotTaxAmtTaxInclusive

                    'Dim dblamountfortableamt As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) - dblTotTaxAmtTaxInclusive
                    'For ii As Integer = 10 To 1 Step -1
                    '    Dim Strii As String = clsCommon.myCstr(ii)
                    '    Dim dbltaxamt As Double = 0
                    '    'Dim dblreceiptamtforcalculation As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value)
                    '    'Dim dblTotTaxRate As Double = dblTotalRateOfAllTaxes
                    '    Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    '    If clsCommon.myLen(strTaxCode) > 0 Then
                    '        Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    '        Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    '        If clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value) Then
                    '            dbltaxamt = clsCommon.myCdbl(dblamountfortableamt * dblTaxRate) / (100 + dblTaxRate)
                    '            gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Math.Round(dbltaxamt, 2)
                    '        End If
                    '    End If
                    'Next

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable10).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable9).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable8).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable7).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable6).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable5).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable4).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable3).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable2).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value)
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable1).Value) Then
                    'Else
                    '    gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value)) / 100.0
                    '    dblTotalAmountforNonTable = dblTotalAmountforNonTable + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value)
                    'End If

                    'Dim dblamountfortableamt As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) - dblTotalAmountforNonTable

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable10).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable9).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable8).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable7).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable6).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable5).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable4).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable3).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable2).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value)) / 100.0
                    'End If

                    'If clsCommon.myCBool(gvItem.Rows(i).Cells(colIsTaxable1).Value) Then
                    '    gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value = (clsCommon.myCdbl(dblamountfortableamt) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value)) / 100.0
                    'End If

                    'gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value)
                    'gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value) - clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value)

                    ' ''richa agarwal
                    ' '''' non excisable without surcharge entry start here
                    'For ii As Integer = 10 To 1 Step -1
                    '    Dim Strii As String = clsCommon.myCstr(ii)
                    '    Dim dblAmtAfterComm As Double = 0
                    '    Dim dblAmtAfterTax As Double = 0
                    '    Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    '    If clsCommon.myLen(strTaxCode) > 0 Then
                    '        dblAmtAfterComm = 0
                    '        dblAmtAfterTax = 0
                    '        Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    '        Dim IsSurTax As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    '        Dim strSurTaxCode As String = clsCommon.myCstr(gvItem.Rows(i).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    '        Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    '        Dim IsExcisable As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                    '        Dim dblBaseAmt As Double = 0
                    '        Dim dblTaxAmt As Double = 0
                    '        Dim dblTotTaxRate = 0
                    '        For jj As Integer = ii To 1 Step -1

                    '            Dim Strjj As String = clsCommon.myCstr(jj)
                    '            Dim dblTaxRateinner As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(clsCommon.myCstr("COLTAXRATE" + Strjj)).Value)
                    '            Dim IsTaxableinner As Boolean = clsCommon.myCBool(gvItem.Rows(i).Cells(clsCommon.myCstr("ISTAXABLE" + Strjj)).Value)
                    '            If IsTaxableinner = False Then
                    '                dblTotTaxRate += dblTaxRateinner
                    '            End If
                    '        Next
                    '        If dblTotTaxRate = 0 Then
                    '            dblTotTaxRate = dblTaxRate
                    '        End If
                    '        If IsSurTax Then
                    '            Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(i, ii, strSurTaxCode)
                    '            dblBaseAmt = dblSurTaxAmt
                    '        Else
                    '            Dim dblOtherTaxAmt As Double = 0
                    '            dblOtherTaxAmt = GetCurrentRowOtherTaxAmtIncusiveTax(IntRowNo, Strii + 1, arrTaxableAuth)

                    '            If strExcise = True AndAlso IsExcisable = True Then 'AndAlso intMRPwithabatement = 1  commented by priti on 04/10/2015 for excise calculation same as dispatch
                    '                dblBaseAmt = (dblAbatementAmt - dblOtherTaxAmt)
                    '            Else
                    '                dblBaseAmt = (dblAmt - dblTotTaxAmtTaxInclusive)
                    '            End If
                    '            dblTaxAmt = (dblBaseAmt * dblTaxRate) / (100 + dblTotTaxRate)
                    '            dblTotTaxAmtTaxInclusive += dblTaxAmt
                    '            dblBaseAmt = dblBaseAmt - dblTaxAmt
                    '            'If dblBaseAmt = dblAmt And dblTotTaxAmtTaxInclusive > 0 Then
                    '            '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                    '            'ElseIf IsTaxable Then
                    '            '    dblBaseAmt = dblBaseAmt - dblTotTaxAmtTaxInclusive
                    '            'End If

                    '        End If
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    '        If dblBaseAmt > 0 Then
                    '            dblRate = dblBaseAmt / dblQty
                    '        End If
                    '        'dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                    '        If Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                    '            arrTaxableAuth.Add(strTaxCode.ToUpper())
                    '        End If


                    '    Else
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                    '        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                    '    End If
                    'Next
                    '''' non excisable without surcharge entry ends here

                    ''------------




                End If
            Next

            For i As Integer = 0 To gvItem.Rows.Count - 1
                dbltotalTaxamount = dbltotalTaxamount + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value)
            Next

            lblDOTotalTaxAmt.Text = Math.Round(clsCommon.myCdbl(dbltotalTaxamount), 2)
            SetitemWiseTaxSetting(False, False)
            CalculateTaxDetailForTaxgrid()
        End If
    End Sub
    Private Function getcurrentTaxReceiptRateNonTaxable(ByVal IntRowNo As Integer) As Double
        Dim dblrate As Double = 0
        Try
            Dim arrTaxableAuth As New List(Of String)
            If txtTaxGroup.Enabled = False Then
                For ii As Integer = 1 To 10
                    Dim Strii As String = clsCommon.myCstr(ii)
                    Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                        If Not IsTaxable Then
                            dblrate = dblrate + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value)
                        End If
                    End If
                Next
            Else
                For ii As Integer = 0 To gvTaxDetail.Rows.Count - 1
                    Dim Strii As String = clsCommon.myCstr(ii)
                    Dim strTaxCode As String = clsCommon.myCstr(gvTaxDetail.Rows(ii).Cells(colTTaxAutCode).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim IsTaxable As String = clsDBFuncationality.getSingleValue("Select Taxable from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & txtTaxGroup.Value & "' and Tax_code='" & strTaxCode & "'")
                        If clsCommon.CompairString(IsTaxable, "N") = CompairStringResult.Equal Then
                            dblrate = dblrate + clsCommon.myCdbl(gvTaxDetail.Rows(ii).Cells(colTTaxRate).Value)
                        End If
                    End If
                Next
            End If

            Return dblrate
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        'Try
        '    Dim arrTaxableAuth As New List(Of String)


        '    'Dim dblAmt As Double = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(AdcolAmt_Less_Discount).Value)
        '    Dim dblAmtAfterDis As Double = dblAmt
        '    For ii As Integer = 1 To 10
        '        Dim Strii As String = clsCommon.myCstr(ii)
        '        Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
        '        If clsCommon.myLen(strTaxCode) > 0 Then
        '            Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
        '            Dim IsSurTax As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
        '            Dim strSurTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
        '            Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
        '            'Dim IsExcisable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
        '            Dim dblBaseAmt As Double = 0
        '            Dim dblTaxAmt As Double = 0
        '            If IsSurTax Then
        '                Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
        '                dblBaseAmt = dblSurTaxAmt
        '            Else
        '                Dim dblOtherTaxAmt As Double = 0

        '                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)


        '                If gvItem.Rows(IntRowNo).Cells(AdcolFOC_Item).Value = 0 Then
        '                    dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
        '                End If

        '            End If

        '            ' gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
        '            dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
        '            ' gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
        '            gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Math.Round(dblTaxAmt, 6)
        '            If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
        '                arrTaxableAuth.Add(strTaxCode.ToUpper())
        '            End If

        '        Else
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
        '            gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMTRECEIPT" + Strii)).Value = Nothing
        '            'gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
        '        End If

        '    Next



        '    'Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        '    'Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message)
        'End Try

    End Function
    Private Function getcurrentTaxReceiptAmountNonTaxable(ByVal IntRowNo As Integer) As Double
        Dim dblrate As Double = 0
        Try
            Dim arrTaxableAuth As New List(Of String)

            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    If Not IsTaxable Then
                        dblrate = dblrate + clsCommon.myCdbl(gvItem.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value)
                    End If
                End If
            Next
            Return dblrate
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Function


    Sub CalculateTaxDetailForTaxgrid()
        Dim DblReceiptTaxAmt1 As Double = 0
        Dim DblReceiptTaxAmt2 As Double = 0
        Dim DblReceiptTaxAm3 As Double = 0
        Dim DblReceiptTaxAmt4 As Double = 0
        Dim DblReceiptTaxAmt5 As Double = 0
        Dim DblReceiptTaxAmt6 As Double = 0
        Dim DblReceiptTaxAmt7 As Double = 0
        Dim DblReceiptTaxAmt8 As Double = 0
        Dim DblReceiptTaxAmt9 As Double = 0
        Dim DblReceiptTaxAmt10 As Double = 0

        For i As Integer = 0 To gvItem.Rows.Count - 1
            DblReceiptTaxAmt1 = DblReceiptTaxAmt1 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value)
            DblReceiptTaxAmt2 = DblReceiptTaxAmt2 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value)
            DblReceiptTaxAm3 = DblReceiptTaxAm3 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value)
            DblReceiptTaxAmt4 = DblReceiptTaxAmt4 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value)
            DblReceiptTaxAmt5 = DblReceiptTaxAmt5 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value)
            DblReceiptTaxAmt6 = DblReceiptTaxAmt6 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value)
            DblReceiptTaxAmt7 = DblReceiptTaxAmt7 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value)
            DblReceiptTaxAmt8 = DblReceiptTaxAmt8 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value)
            DblReceiptTaxAmt9 = DblReceiptTaxAmt9 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value)
            DblReceiptTaxAmt10 = DblReceiptTaxAmt10 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value)
        Next

        If clsCommon.myCdbl(gvItem.Rows.Count) > 0 Then
            Dim DblReceiptTaxAmt As Double = 0
            For ii As Integer = 0 To 9
                Dim Strii As String = clsCommon.myCstr(ii + 1)
                'Dim dbltaxamt As Double = 0
                'Dim dblreceiptamtforcalculation As Double = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value)
                'Dim dblTotTaxRate As Double = dblTotalRateOfAllTaxes
                Dim strTaxCode As String = clsCommon.myCstr(gvItem.Rows(0).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                Select Case (ii + 1)
                    Case 1
                        DblReceiptTaxAmt = DblReceiptTaxAmt1
                    Case 2
                        DblReceiptTaxAmt = DblReceiptTaxAmt2
                    Case 3
                        DblReceiptTaxAmt = DblReceiptTaxAm3
                    Case 4
                        DblReceiptTaxAmt = DblReceiptTaxAmt4
                    Case 5
                        DblReceiptTaxAmt = DblReceiptTaxAmt5
                    Case 6
                        DblReceiptTaxAmt = DblReceiptTaxAmt6
                    Case 7
                        DblReceiptTaxAmt = DblReceiptTaxAmt7
                    Case 8
                        DblReceiptTaxAmt = DblReceiptTaxAmt8
                    Case 9
                        DblReceiptTaxAmt = DblReceiptTaxAmt9
                    Case 10
                        DblReceiptTaxAmt = DblReceiptTaxAmt10
                End Select

                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gvItem.Rows(0).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gvItem.Rows(0).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    If clsCommon.myCBool(gvItem.Rows(0).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value) Then
                        'dbltaxamt = clsCommon.myCdbl(clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * dblTaxRate) / (100 + dblTaxRate)
                        gvTaxDetail.Rows(ii).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt)
                        gvTaxDetail.Rows(ii).Cells(colTBaseAmt).Value = IIf(clsCommon.myCdbl(txtUnApplAmt.Text) > 0, clsCommon.myCdbl(txtUnApplAmt.Text), clsCommon.myCdbl(LblDOTotalAmount.Text)) - clsCommon.myCdbl(lblDOTotalTaxAmt.Text)
                        '  gvTaxDetail.Rows(ii).Cells(colTBaseAmt).Value = Math.Round(clsCommon.myCdbl((DblReceiptTaxAmt * (100 + dblTaxRate)) / dblTaxRate), 2)
                    Else
                        gvTaxDetail.Rows(ii).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt)
                        gvTaxDetail.Rows(ii).Cells(colTBaseAmt).Value = Math.Round(clsCommon.myCdbl((DblReceiptTaxAmt * 100) / dblTaxRate), 2)
                    End If

                End If
            Next

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX1).Value)) > 0) Then
            '    gvTaxDetail.Rows(0).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt1)
            '    gvTaxDetail.Rows(0).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt1 * 100) / gvTaxDetail.Rows(0).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(Adcoltax2).Value)) > 0) Then
            '    gvTaxDetail.Rows(1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt2)
            '    gvTaxDetail.Rows(1).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt2 * 100) / gvTaxDetail.Rows(1).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX3).Value)) > 0) Then
            '    gvTaxDetail.Rows(2).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAm3)
            '    gvTaxDetail.Rows(2).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAm3 * 100) / gvTaxDetail.Rows(2).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX4).Value)) > 0) Then
            '    gvTaxDetail.Rows(3).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt4)
            '    gvTaxDetail.Rows(3).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt4 * 100) / gvTaxDetail.Rows(3).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX5).Value)) > 0) Then
            '    gvTaxDetail.Rows(4).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt5)
            '    gvTaxDetail.Rows(4).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt5 * 100) / gvTaxDetail.Rows(4).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX6).Value)) > 0) Then
            '    gvTaxDetail.Rows(5).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt6)
            '    gvTaxDetail.Rows(5).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt6 * 100) / gvTaxDetail.Rows(5).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX7).Value)) > 0) Then
            '    gvTaxDetail.Rows(6).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt7)
            '    gvTaxDetail.Rows(6).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt7 * 100) / gvTaxDetail.Rows(6).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX8).Value)) > 0) Then
            '    gvTaxDetail.Rows(7).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt8)
            '    gvTaxDetail.Rows(7).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt8 * 100) / gvTaxDetail.Rows(7).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX9).Value)) > 0) Then
            '    gvTaxDetail.Rows(8).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt9)
            '    gvTaxDetail.Rows(8).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt9 * 100) / gvTaxDetail.Rows(8).Cells(colTTaxRate).Value)
            'End If

            'If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX10).Value)) > 0) Then
            '    gvTaxDetail.Rows(9).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt10)
            '    gvTaxDetail.Rows(9).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt10 * 100) / gvTaxDetail.Rows(9).Cells(colTTaxRate).Value)
            'End If
        End If
    End Sub



    'Sub CalculateTaxAmountInAdvnce()
    '    Dim DblReceiptAmt As Double = 0
    '    Dim dblDOTotalAmt As Double = 0
    '    Dim dblTotalRateOfAllTaxes As Double = 0
    '    Dim dblDOTotalAddCharge As Double = 0
    '    DblReceiptAmt = clsCommon.myCdbl(txtUnApplAmt.Text)
    '    dblDOTotalAmt = clsCommon.myCdbl(LblDOTotalAmount.Text)
    '    'dblDOTotalAddCharge = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select isnull(Total_Add_Charge,0) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE where Document_Code ='" & clsCommon.myCstr(txtDONo.Value) & "' "))
    '    dblDOTotalAddCharge = clsCommon.myCdbl(lblDOTotalAdditionalCharge.Text)
    '    dblDOTotalAmt = dblDOTotalAmt - dblDOTotalAddCharge
    '    Dim dbltotalTaxamount As Double = 0
    '    If DblReceiptAmt > 0 Then

    '        For i As Integer = 0 To gvItem.Rows.Count - 1
    '            If clsCommon.CompairString(clsCommon.myCstr(gvItem.Rows(i).Cells(AdcolScheme_Item).Value), "N") = CompairStringResult.Equal Then
    '                dblTotalRateOfAllTaxes = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value) + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)
    '                gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value = clsCommon.myCdbl((DblReceiptAmt * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolItem_Net_Amt).Value)) / dblDOTotalAmt)
    '                gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value = clsCommon.myCdbl((clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalAdvanceAmt).Value) * 100.0) / (100.0 + clsCommon.myCdbl(dblTotalRateOfAllTaxes)))
    '                gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value = (clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptAdvance).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Rate).Value)) / 100.0
    '                gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value = clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value + gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value)
    '            End If
    '        Next

    '        For i As Integer = 0 To gvItem.Rows.Count - 1
    '            dbltotalTaxamount = dbltotalTaxamount + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolReceiptTotalTax).Value)
    '        Next

    '        lblDOTotalTaxAmt.Text = Math.Round(clsCommon.myCdbl(dbltotalTaxamount), 2)

    '        CalculateTaxDetailForTaxgrid()
    '    End If
    'End Sub

    'Sub CalculateTaxDetailForTaxgrid()
    '    Dim DblReceiptTaxAmt1 As Double = 0
    '    Dim DblReceiptTaxAmt2 As Double = 0
    '    Dim DblReceiptTaxAm3 As Double = 0
    '    Dim DblReceiptTaxAmt4 As Double = 0
    '    Dim DblReceiptTaxAmt5 As Double = 0
    '    Dim DblReceiptTaxAmt6 As Double = 0
    '    Dim DblReceiptTaxAmt7 As Double = 0
    '    Dim DblReceiptTaxAmt8 As Double = 0
    '    Dim DblReceiptTaxAmt9 As Double = 0
    '    Dim DblReceiptTaxAmt10 As Double = 0

    '    For i As Integer = 0 To gvItem.Rows.Count - 1
    '        DblReceiptTaxAmt1 = DblReceiptTaxAmt1 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX1_Amt_Receipt).Value)
    '        DblReceiptTaxAmt2 = DblReceiptTaxAmt2 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX2_Amt_Receipt).Value)
    '        DblReceiptTaxAm3 = DblReceiptTaxAm3 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX3_Amt_Receipt).Value)
    '        DblReceiptTaxAmt4 = DblReceiptTaxAmt4 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX4_Amt_Receipt).Value)
    '        DblReceiptTaxAmt5 = DblReceiptTaxAmt5 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX5_Amt_Receipt).Value)
    '        DblReceiptTaxAmt6 = DblReceiptTaxAmt6 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX6_Amt_Receipt).Value)
    '        DblReceiptTaxAmt7 = DblReceiptTaxAmt7 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX7_Amt_Receipt).Value)
    '        DblReceiptTaxAmt8 = DblReceiptTaxAmt8 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX8_Amt_Receipt).Value)
    '        DblReceiptTaxAmt9 = DblReceiptTaxAmt9 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX9_Amt_Receipt).Value)
    '        DblReceiptTaxAmt10 = DblReceiptTaxAmt10 + clsCommon.myCdbl(gvItem.Rows(i).Cells(AdcolTAX10_Amt_Receipt).Value)
    '    Next

    '    If clsCommon.myCdbl(gvItem.Rows.Count) > 0 Then


    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX1).Value)) > 0) Then
    '            gvTaxDetail.Rows(0).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt1)
    '            gvTaxDetail.Rows(0).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt1 * 100) / gvTaxDetail.Rows(0).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(Adcoltax2).Value)) > 0) Then
    '            gvTaxDetail.Rows(1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt2)
    '            gvTaxDetail.Rows(1).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt2 * 100) / gvTaxDetail.Rows(1).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX3).Value)) > 0) Then
    '            gvTaxDetail.Rows(2).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAm3)
    '            gvTaxDetail.Rows(2).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAm3 * 100) / gvTaxDetail.Rows(2).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX4).Value)) > 0) Then
    '            gvTaxDetail.Rows(3).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt4)
    '            gvTaxDetail.Rows(3).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt4 * 100) / gvTaxDetail.Rows(3).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX5).Value)) > 0) Then
    '            gvTaxDetail.Rows(4).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt5)
    '            gvTaxDetail.Rows(4).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt5 * 100) / gvTaxDetail.Rows(4).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX6).Value)) > 0) Then
    '            gvTaxDetail.Rows(5).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt6)
    '            gvTaxDetail.Rows(5).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt6 * 100) / gvTaxDetail.Rows(5).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX7).Value)) > 0) Then
    '            gvTaxDetail.Rows(6).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt7)
    '            gvTaxDetail.Rows(6).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt7 * 100) / gvTaxDetail.Rows(6).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX8).Value)) > 0) Then
    '            gvTaxDetail.Rows(7).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt8)
    '            gvTaxDetail.Rows(7).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt8 * 100) / gvTaxDetail.Rows(7).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX9).Value)) > 0) Then
    '            gvTaxDetail.Rows(8).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt9)
    '            gvTaxDetail.Rows(8).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt9 * 100) / gvTaxDetail.Rows(8).Cells(colTTaxRate).Value)
    '        End If

    '        If (clsCommon.myLen(clsCommon.myCstr(gvItem.Rows(0).Cells(AdcolTAX10).Value)) > 0) Then
    '            gvTaxDetail.Rows(9).Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt10)
    '            gvTaxDetail.Rows(9).Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt10 * 100) / gvTaxDetail.Rows(9).Cells(colTTaxRate).Value)
    '        End If
    '    End If
    'End Sub

    Private Sub txtDocument_ForAdvanceDoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocument_ForAdvanceDoc._MYValidating
        Try
            GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
            If GSTStatus AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTransType.SelectedValue), "F") = CompairStringResult.Equal Then
                Dim whrcls As String = String.Empty
                Dim gstdate As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicableDate, clsFixedParameterCode.GSTApplicableDate, Nothing))
                Qry = " Select Receipt_No ,convert(varchar,Receipt_Date,103) as [Receipt Date],Cust_Code as [Customer Code] ,Customer_Name ,Receipt_Amount ,Bank_Code from TSPL_RECEIPT_HEADER "
                whrcls = " Receipt_Type ='P' and (isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS ,'')<>'' or isnull(TSPL_RECEIPT_HEADER.Tax_Group,'')<>'' ) and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N'  and TSPL_RECEIPT_HEADER.Receipt_No not in (select Applied_Receipt  from TSPL_RECEIPT_HEADER where Receipt_Type ='F' and ISNULL (Applied_Receipt ,'')<>'' and isnull(TSPL_RECEIPT_HEADER. IsChkReverse,'') ='N') and Posted ='Y' "
                If clsCommon.myLen(clsCommon.myCstr(fndCustomer.Value)) > 0 Then
                    whrcls += " and TSPL_RECEIPT_HEADER.Cust_Code ='" & clsCommon.myCstr(fndCustomer.Value) & "' "
                End If
                If clsCommon.myLen(clsCommon.myCstr(fndBankCode.Value)) > 0 Then
                    whrcls += " and TSPL_RECEIPT_HEADER.Bank_Code ='" & clsCommon.myCstr(fndBankCode.Value) & "' "
                End If


                txtDocument_ForAdvanceDoc.Value = clsCommon.ShowSelectForm("AdvancePOFinderGST", Qry, "Receipt_No", whrcls, clsCommon.myCstr(txtDocument_ForAdvanceDoc.Value), "Receipt_No", isButtonClicked)
                If clsCommon.myLen(txtDocument_ForAdvanceDoc.Value) > 0 Then
                    LoadDataInCaseOFAdvance_Refund()
                End If
                If clsCommon.CompairString(clsCommon.myCstr(fndPayType.Value), "Cheque") = CompairStringResult.Equal Then
                    pnlCheque.Enabled = True
                Else
                    pnlCheque.Enabled = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadDataInCaseOFAdvance_Refund()
        Try
            Dim DblReceiptTaxAmt1 As Double = 0
            Dim DblReceiptTaxAmt2 As Double = 0
            Dim DblReceiptTaxAm3 As Double = 0
            Dim DblReceiptTaxAmt4 As Double = 0
            Dim DblReceiptTaxAmt5 As Double = 0
            Dim DblReceiptTaxAmt6 As Double = 0
            Dim DblReceiptTaxAmt7 As Double = 0
            Dim DblReceiptTaxAmt8 As Double = 0
            Dim DblReceiptTaxAmt9 As Double = 0
            Dim DblReceiptTaxAmt10 As Double = 0
            Dim obj As New clsRcptEntryHeader()
            obj = clsRcptEntryHeader.GetData(txtDocument_ForAdvanceDoc.Value, NavigatorType.Current)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Receipt_No) > 0) Then
                fndBankCode.Enabled = False


                dgvmiscpayment.Visible = False
                dgvReceipt.Visible = False
                ddlTransType.Enabled = False

                fndBankCode.Value = obj.Bank_Code
                'ddlTransType.SelectedValue = obj.Receipt_Type
                TxtForeignBankCharges.Value = obj.Foreign_Bank_Charges_Amt
                TxtBankCharges.Value = obj.Bank_Charges_Amt

                fndCustomer.Value = obj.Cust_Code
                txtCusName.Text = obj.Customer_Name
                txtDistr_Code.Text = obj.Distr_Code
                'txtChkNo.Text = obj.Cheque_No
                'If clsCommon.myLen(obj.Cheque_No) > 0 Then
                '    dtCheque.Value = obj.Cheque_Date
                'End If
                fndPayType.Value = obj.Payment_Code
                txtRcptAmt.Text = clsCommon.myCstr(obj.Receipt_Amount)
                txtTotalPaymentBaseCurr.Value = clsCommon.myCdbl(obj.Receipt_Amount) * clsCommon.myCdbl(obj.ConvRate)


                If obj.Receipt_Type = "F" Then
                    txtUnApplAmt.Text = clsCommon.myCstr(obj.Receipt_Amount)
                Else
                    txtUnApplAmt.Text = clsCommon.myCstr(obj.UnApply_Amt)
                End If
                txtUnAppliedBal.Text = clsCommon.myCstr(obj.UnApplied_Balance)
                txtUnApplieadNo.Text = clsCommon.myCstr(obj.UnApplied_No)

                txtUnApplAmt.Enabled = True
                txtlocation.Value = obj.Location_GL_Code
                LblLocDesp.Text = clsCommon.myCstr(clsLocation.GetName(obj.Location_GL_Code, Nothing))
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                If clsCommon.myLen(obj.ApplicableFrom) > 0 Then
                    Me.txtApplicableFrom.Text = clsCommon.myCstr(clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
                End If

                GSTStatus = clsERPFuncationality.GetGSTStatus(dtRcpt.Value)
                If GSTStatus Then
                    LoadBlankGridTax()
                    LoadBlankGridDOItemDetail()
                    LblDOTotalAmount.Text = obj.Delivery_order_Amount
                    lblDOTotalAdditionalCharge.Text = obj.DO_Total_Add_Amount
                    lblDOTotalTaxAmt.Text = obj.Tax_Amount_Advance
                    txtTaxGroup.Value = obj.Tax_Group
                    txtDONo.Value = obj.Delivery_Code_PS
                    lblTaxGrpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code ='" & clsCommon.myCstr(txtTaxGroup.Value) & "'"))

                    lblDO_Location.Text = obj.SO_Location_Code

                    For Each objTr As clsReceiptDetailGST In obj.ArrTrGST
                        gvItem.Rows.AddNew()
                        gvItem.CurrentRow.Cells(AdcolDocument_Code).Value = objTr.Document_Code
                        gvItem.CurrentRow.Cells(AdcolLine_No).Value = objTr.Line_No
                        gvItem.CurrentRow.Cells(AdcolRow_Type).Value = objTr.Row_Type
                        gvItem.CurrentRow.Cells(AdcolItem_Code).Value = objTr.Item_Code
                        gvItem.CurrentRow.Cells(adcolIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
                        gvItem.CurrentRow.Cells(adcolIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItem.CurrentRow.Cells(AdcolQty).Value = objTr.Qty
                        gvItem.CurrentRow.Cells(AdcolBalance_Qty).Value = objTr.Balance_Qty
                        gvItem.CurrentRow.Cells(AdcolItem_Cost).Value = objTr.Item_Cost
                        gvItem.CurrentRow.Cells(AdcolUnit_code).Value = objTr.Unit_code
                        gvItem.CurrentRow.Cells(AdcolTAX1).Value = objTr.TAX1
                        gvItem.CurrentRow.Cells(AdcolTAX1_Amt).Value = objTr.TAX1_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX1_Base_Amt).Value = objTr.TAX1_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX1_Rate).Value = objTr.TAX1_Rate
                        gvItem.CurrentRow.Cells(Adcoltax2).Value = objTr.tax2
                        gvItem.CurrentRow.Cells(AdcolTAX2_Base_Amt).Value = objTr.TAX2_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX2_Rate).Value = objTr.TAX2_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX2_Amt).Value = objTr.TAX2_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX3).Value = objTr.TAX3
                        gvItem.CurrentRow.Cells(AdcolTAX3_Base_Amt).Value = objTr.TAX3_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX3_Rate).Value = objTr.TAX3_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX3_Amt).Value = objTr.TAX3_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX4).Value = objTr.TAX4
                        gvItem.CurrentRow.Cells(AdcolTAX4_Base_Amt).Value = objTr.TAX4_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX4_Rate).Value = objTr.TAX4_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX4_Amt).Value = objTr.TAX4_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX5).Value = objTr.tax5
                        gvItem.CurrentRow.Cells(AdcolTAX5_Base_Amt).Value = objTr.TAX5_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX5_Rate).Value = objTr.TAX5_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX5_Amt).Value = objTr.TAX5_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX6_Base_Amt).Value = objTr.TAX6_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX6).Value = objTr.tax6
                        gvItem.CurrentRow.Cells(AdcolTAX6_Rate).Value = objTr.TAX6_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX6_Amt).Value = objTr.TAX6_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX7).Value = objTr.tax7
                        gvItem.CurrentRow.Cells(AdcolTAX7_Base_Amt).Value = objTr.TAX7_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX7_Rate).Value = objTr.TAX7_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX7_Amt).Value = objTr.TAX7_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX8).Value = objTr.tax8
                        gvItem.CurrentRow.Cells(AdcolTAX8_Base_Amt).Value = objTr.TAX8_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX8_Rate).Value = objTr.TAX8_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX8_Amt).Value = objTr.TAX8_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9).Value = objTr.tax9
                        gvItem.CurrentRow.Cells(AdcolTAX9_Base_Amt).Value = objTr.TAX9_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Amt).Value = objTr.TAX9_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Rate).Value = objTr.TAX9_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX10).Value = objTr.tax10
                        gvItem.CurrentRow.Cells(AdcolTAX10_Base_Amt).Value = objTr.TAX10_Base_Amt
                        gvItem.CurrentRow.Cells(AdcolTAX10_Rate).Value = objTr.TAX10_Rate
                        gvItem.CurrentRow.Cells(AdcolTAX10_Amt).Value = objTr.TAX10_Amt
                        gvItem.CurrentRow.Cells(AdcolAmount).Value = objTr.Amount
                        gvItem.CurrentRow.Cells(AdcolDisc_Per).Value = objTr.Disc_Per
                        gvItem.CurrentRow.Cells(AdcolDisc_Amt).Value = objTr.Disc_Amt
                        gvItem.CurrentRow.Cells(AdcolAmt_Less_Discount).Value = objTr.Amt_Less_Discount
                        gvItem.CurrentRow.Cells(AdcolTotal_Tax_Amt).Value = objTr.Total_Tax_Amt
                        gvItem.CurrentRow.Cells(AdcolItem_Net_Amt).Value = objTr.Item_Net_Amt
                        gvItem.CurrentRow.Cells(AdcolMRP).Value = objTr.MRP
                        gvItem.CurrentRow.Cells(AdcolAbatement_Per).Value = objTr.Abatement_Per
                        gvItem.CurrentRow.Cells(AdcolAbatement_Amt).Value = objTr.Abatement_Amt
                        gvItem.CurrentRow.Cells(AdcolScheme_Code).Value = objTr.Scheme_Code
                        gvItem.CurrentRow.Cells(AdcolScheme_Applicable).Value = objTr.Scheme_Applicable
                        gvItem.CurrentRow.Cells(AdcolScheme_Item).Value = objTr.Scheme_Item
                        gvItem.CurrentRow.Cells(AdcolFOC_Item).Value = objTr.FOC_Item
                        gvItem.CurrentRow.Cells(AdcolItem_Tax).Value = objTr.Item_Tax
                        gvItem.CurrentRow.Cells(AdcolTotal_MRP_Amt).Value = objTr.Total_MRP_Amt
                        gvItem.CurrentRow.Cells(AdcolTotal_Basic_Amt).Value = objTr.Total_Basic_Amt
                        gvItem.CurrentRow.Cells(AdcolTotal_Disc_Amt).Value = objTr.Total_Disc_Amt
                        gvItem.CurrentRow.Cells(AdcolActualRate).Value = objTr.ActualRate
                        gvItem.CurrentRow.Cells(AdcolTotalItem_Weight).Value = objTr.TotalItem_Weight
                        gvItem.CurrentRow.Cells(AdcolConv_Factor).Value = objTr.Conv_Factor
                        gvItem.CurrentRow.Cells(AdcolLanding_Cost).Value = objTr.Landing_Cost
                        gvItem.CurrentRow.Cells(AdcolTAX1_Amt_Receipt).Value = objTr.TAX1_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX2_Amt_Receipt).Value = objTr.TAX2_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX3_Amt_Receipt).Value = objTr.TAX3_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX4_Amt_Receipt).Value = objTr.TAX4_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX5_Amt_Receipt).Value = objTr.TAX5_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX6_Amt_Receipt).Value = objTr.TAX6_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX7_Amt_Receipt).Value = objTr.TAX7_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX8_Amt_Receipt).Value = objTr.TAX8_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX9_Amt_Receipt).Value = objTr.TAX9_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolTAX10_Amt_Receipt).Value = objTr.TAX10_Amt_Receipt
                        gvItem.CurrentRow.Cells(AdcolReceiptAdvance).Value = objTr.ReceiptAdvance
                        gvItem.CurrentRow.Cells(AdcolReceiptTotalTax).Value = objTr.ReceiptTotalTax
                        gvItem.CurrentRow.Cells(AdcolReceiptTotalAdvanceAmt).Value = objTr.ReceiptTotalAdvanceAmt


                        DblReceiptTaxAmt1 = DblReceiptTaxAmt1 + clsCommon.myCdbl(objTr.TAX1_Amt_Receipt)
                        DblReceiptTaxAmt2 = DblReceiptTaxAmt2 + clsCommon.myCdbl(objTr.TAX2_Amt_Receipt)
                        DblReceiptTaxAm3 = DblReceiptTaxAm3 + clsCommon.myCdbl(objTr.TAX3_Amt_Receipt)
                        DblReceiptTaxAmt4 = DblReceiptTaxAmt4 + clsCommon.myCdbl(objTr.TAX4_Amt_Receipt)
                        DblReceiptTaxAmt5 = DblReceiptTaxAmt5 + clsCommon.myCdbl(objTr.TAX5_Amt_Receipt)
                        DblReceiptTaxAmt6 = DblReceiptTaxAmt6 + clsCommon.myCdbl(objTr.TAX6_Amt_Receipt)
                        DblReceiptTaxAmt7 = DblReceiptTaxAmt7 + clsCommon.myCdbl(objTr.TAX7_Amt_Receipt)
                        DblReceiptTaxAmt8 = DblReceiptTaxAmt8 + clsCommon.myCdbl(objTr.TAX8_Amt_Receipt)
                        DblReceiptTaxAmt9 = DblReceiptTaxAmt9 + clsCommon.myCdbl(objTr.TAX9_Amt_Receipt)
                        DblReceiptTaxAmt10 = DblReceiptTaxAmt10 + clsCommon.myCdbl(objTr.TAX10_Amt_Receipt)

                    Next

                    If clsCommon.myLen(txtDONo.Value) > 0 Then
                        For Each objTr As clsReceiptDetailGST In obj.ArrTrGST
                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX1)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX1)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX1))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX1_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt1)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt1 * 100) / objTr.TAX1_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax2)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax2)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax2))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX2_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt2)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt2 * 100) / objTr.TAX2_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX3)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX3)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX3))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX3_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAm3)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAm3 * 100) / objTr.TAX3_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.TAX4)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.TAX4)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.TAX4))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX4_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt4)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt4 * 100) / objTr.TAX4_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax5)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax5)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax5))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX5_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt5)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt5 * 100) / objTr.TAX5_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax6)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax6)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax6))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX6_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt6)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt6 * 100) / objTr.TAX6_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax7)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax7)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax7))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX7_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt7)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt7 * 100) / objTr.TAX7_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax8)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax8)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax8))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX8_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt8)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt8 * 100) / objTr.TAX8_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax9)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax9)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax9))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX9_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt9)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt9 * 100) / objTr.TAX9_Rate)
                            End If

                            If (clsCommon.myLen(clsCommon.myCstr(objTr.tax10)) > 0) Then
                                gvTaxDetail.Rows.AddNew()
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(objTr.tax10)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(objTr.tax10))
                                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(objTr.TAX10_Rate)
                                gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(DblReceiptTaxAmt10)
                                gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl((DblReceiptTaxAmt10 * 100) / objTr.TAX10_Rate)
                            End If
                            Exit For
                        Next
                    Else
                        ' If Advance is created without DO and tax group is selected '' added by priti sharma on 17.08.2016
                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX1)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX1)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX1))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX1_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX1_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX1_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax2)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax2)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax2))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX2_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX2_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX2_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX3)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX3)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX3))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX3_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX3_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX3_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.TAX4)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.TAX4)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.TAX4))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX4_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX4_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX4_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax5)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax5)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax5))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX5_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX5_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX5_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax6)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax6)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax6))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX6_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX6_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX6_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax7)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax7)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax7))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX7_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX7_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX7_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax8)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax8)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax8))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX8_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX8_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX8_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax9)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax9)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax9))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX9_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX9_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX9_Base_Amt)
                        End If

                        If (clsCommon.myLen(clsCommon.myCstr(obj.tax10)) > 0) Then
                            gvTaxDetail.Rows.AddNew()
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value = clsCommon.myCstr(obj.tax10)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAutName).Value = clsTaxMaster.GetName(clsCommon.myCstr(obj.tax10))
                            gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = clsCommon.myCdbl(obj.TAX10_Rate)
                            gvTaxDetail.CurrentRow.Cells(colTTaxAmt).Value = clsCommon.myCdbl(obj.TAX10_Amt)
                            gvTaxDetail.CurrentRow.Cells(colTBaseAmt).Value = clsCommon.myCdbl(obj.TAX10_Base_Amt)
                        End If
                        txtTaxGroup.Enabled = False
                    End If

                End If

                ''--------------------- end of gst work
                fndBankCode.Enabled = False
                fndCustomer.Enabled = False
                txtUnApplAmt.Enabled = False
                fndPayType.Enabled = False
                txtlocation.Enabled = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    Private Sub txtTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTaxGroup._MYValidating
        Try
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupLocationSegment(txtlocation.Value, fndCustomer.Value, "S", txtTaxGroup.Value, isButtonClicked)
            LoadBlankGridTax()
            If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                txtDONo.Enabled = False
            End If
            SetTaxDetails()
            CalculateTaxwithoutDO()
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal And clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                CaptionItem.Visible = True
                txtitem.Visible = True
                lblItemName.Visible = True
            Else
                CaptionItem.Visible = False
                txtitem.Visible = False
                lblItemName.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtitem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtitem._MYValidating
        Try
            Dim qry As String = "select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            txtitem.Value = clsCommon.ShowSelectForm("ReceipItemfndd", qry, "Code", "", txtitem.Value, "Code", isButtonClicked)
            lblItemName.Text = clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master where item_code='" & txtitem.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function GetTaxDetails(ByVal GrpCode As String) As DataTable
        Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='S') AS TaxRate,TSPL_TAX_GROUP_DETAILS.Taxable, TSPL_TAX_MASTER.Excisable , TSPL_TAX_MASTER.Tax_Recoverable,TSPL_TAX_MASTER.Type from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + GrpCode + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='S' order by Trans_Code"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
    Sub SetTaxDetails()
        Dim dt As DataTable = GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gvTaxDetail.Rows.AddNew()
                gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                gvTaxDetail.Rows(gvTaxDetail.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
            Next
        End If

    End Sub
    Sub CalculateTaxwithoutDO()
        If gvTaxDetail.Rows.Count >= 0 Then
            Dim dblTotalRateOfAllTaxes As Double = getcurrentTaxReceiptRateNonTaxable(i)
            Dim DblReceiptTaxAmt As Double = clsCommon.myCdbl((clsCommon.myCdbl(txtRcptAmt.Text) * 100.0) / (100.0 + clsCommon.myCdbl(dblTotalRateOfAllTaxes)))

            Dim dblTotTaxAmtTaxInclusive As Double = 0
            i = gvTaxDetail.Rows.Count - 1
            For ii As Integer = i To 0 Step -1
                Dim Strii As String = clsCommon.myCstr(ii)
                Dim dbltaxamt As Double = 0
                Dim dblreceiptamtforcalculation As Double = DblReceiptTaxAmt
                Dim dblTotTaxRate As Double = dblTotalRateOfAllTaxes
                Dim strTaxCode As String = clsCommon.myCstr(gvTaxDetail.Rows(ii).Cells(colTTaxAutCode).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gvTaxDetail.Rows(ii).Cells(colTTaxRate).Value)
                    Dim IsTaxable As String = clsDBFuncationality.getSingleValue("Select Taxable from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & txtTaxGroup.Value & "' and tax_code='" & strTaxCode & "'")
                    If clsCommon.CompairString(IsTaxable, "Y") = CompairStringResult.Equal Then
                        dbltaxamt = clsCommon.myCdbl(clsCommon.myCdbl(DblReceiptTaxAmt) * dblTaxRate) / (100 + dblTaxRate)
                    Else
                        dbltaxamt = clsCommon.myCdbl(dblreceiptamtforcalculation * dblTaxRate) / 100
                    End If
                    dblTotTaxAmtTaxInclusive += dbltaxamt
                    gvTaxDetail.Rows(ii).Cells(colTTaxAmt).Value = Math.Round(dbltaxamt, 4)

                End If
            Next
            lblDOTotalTaxAmt.Text = Math.Round(clsCommon.myCdbl(dblTotTaxAmtTaxInclusive), 2)

            If clsCommon.myLen(txtTaxGroup.Value) > 0 And DblReceiptTaxAmt > 0 Then
                For ii As Integer = 0 To gvTaxDetail.Rows.Count - 1
                    Dim strTaxCode As String = clsCommon.myCstr(gvTaxDetail.Rows(ii).Cells(colTTaxAutCode).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gvTaxDetail.Rows(ii).Cells(colTTaxRate).Value)
                    Dim IsTaxable As String = clsDBFuncationality.getSingleValue("Select Taxable from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & txtTaxGroup.Value & "' and tax_code='" & strTaxCode & "'")
                    If clsCommon.CompairString(IsTaxable, "Y") = CompairStringResult.Equal Then
                        DblReceiptTaxAmt = gvTaxDetail.Rows(ii).Cells(colTTaxAmt).Value
                        gvTaxDetail.Rows(ii).Cells(colTBaseAmt).Value = IIf(clsCommon.myCdbl(txtUnApplAmt.Text) > 0, clsCommon.myCdbl(txtUnApplAmt.Text), clsCommon.myCdbl(LblDOTotalAmount.Text)) - clsCommon.myCdbl(lblDOTotalTaxAmt.Text)
                    Else
                        DblReceiptTaxAmt = gvTaxDetail.Rows(ii).Cells(colTTaxAmt).Value
                        gvTaxDetail.Rows(ii).Cells(colTBaseAmt).Value = Math.Round(clsCommon.myCdbl((DblReceiptTaxAmt * 100) / dblTaxRate), 2)
                    End If

                Next

            End If

        End If
    End Sub


    Private Sub gvTaxDetail_DoubleClick(sender As Object, e As EventArgs) Handles gvTaxDetail.DoubleClick
        Try
            If clsCommon.myLen(txtTaxGroup.Value) > 0 AndAlso txtTaxGroup.Enabled = True Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndTaxfnd", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gvTaxDetail.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='S'", "", "", True))
                gvTaxDetail.CurrentRow.Cells(colTTaxRate).Value = dblNewRate
                CalculateTaxwithoutDO()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtUnApplAmt_Leave(sender As Object, e As EventArgs) Handles txtUnApplAmt.Leave
        btnGo.Select()
    End Sub

    Private Sub chkForCardSale_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkForCardSale.ToggleStateChanged

    End Sub

    Private Sub btnOpenBankCashBook_Click(sender As Object, e As EventArgs) Handles btnOpenBankCashBook.Click
        clsOpenBankCashBook.ShowBankCashBookDatails(fndRcptNo.Value)
    End Sub

    'Public Sub txtsalesmanCode_MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtsalesmanCode._MYOpenMasterForm
    '    ' Replace "MasterForm" with your actual master form class name.
    '    customerform.Show()

    'End Sub

    'Private Sub txtsalesmanCode_Click(sender As Object, e As EventArgs) Handles txtsalesmanCode.Click
    '    RaiseEvent _MYOpenMasterForm(Me, EventArgs.Empty)

    'End Sub

    Public Sub fndCustomer_MYOpenMaterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomer._MYOpenMasterForm
        'Application.OpenForms("MDI").Controls("__txtDocNo").Text = "10003"
        'Application.OpenForms("MDI").Controls("__txtScreenID").Text = clsUserMgtCode.CustomerMaster
        Dim strCode As String = Nothing

        Try
            Dim frm As New frmCustomer("", "")

            Dim strProgramName As String = ""
            Dim strProgramCode As String = clsUserMgtCode.CustomerMaster
            If MDI.setCountertoblockforOpenForm(strProgramCode) = True Then
                If MDI.IsOriginalName = True Then
                    strProgramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Program_Name as Program_Name from TSPL_PROGRAM_MASTER where Program_Code='" + strProgramCode + "'"))
                Else
                    strProgramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when LEN(ISNULL(Re_Name,''))>0 then Re_Name else Program_Name end as Program_Name from TSPL_PROGRAM_MASTER where Program_Code='" + strProgramCode + "'"))
                End If

                MDI.formShow(frm, strProgramCode, strProgramName, True, strCode, True)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    'Private Sub fndCustomer_Click(sender As Object, e As EventArgs) Handles fndCustomer.Click
    '    RaiseEvent _MYOpenMasterForm(Me, EventArgs.Empty)

    'End Sub


    'Private Sub txtsalesmanCode_Click(sender As Object, e As EventArgs) Handles txtsalesmanCode.Click
    '    OpenCustomerFormFromReceiptForm()

    'End Sub

    'Private Sub OpenCustomerFormFromReceiptForm()
    '    Dim customerForm As New frmCustomer()
    '    customerForm.Show()
    'End Sub

    Private Sub txtLoadIn__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLoadIn._MYValidating
        Dim Qry As String = "select TSPL_RCDF_LOAD_IN.Document_Code,TSPL_RCDF_LOAD_IN.Document_Date,TSPL_RCDF_LOAD_IN.Location_Code,TSPL_RCDF_LOAD_IN.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_RCDF_LOAD_IN.Vehicle_No 
from TSPL_RCDF_LOAD_IN
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RCDF_LOAD_IN.Customer_Code"
        Dim strWhrcls As String = "TSPL_RCDF_LOAD_IN.Status=0 and TSPL_RCDF_LOAD_IN.Customer_Code='" + fndCustomer.Value + "'"
        txtLoadIn.Value = clsCommon.ShowSelectForm("RCDFLoadin", Qry, "Document_Code", strWhrcls, txtLoadIn.Value, "Document_Code", isButtonClicked)

        If clsCommon.myLen(txtLoadIn.Value) > 0 Then
            Qry = "select sum(Amount) as Amount from  TSPL_RCDF_LOAD_IN_ITEM where Document_Code='" + txtLoadIn.Value + "'"
            txtUnApplAmt.Text = clsCommon.myFormat(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(Qry)))
        End If
    End Sub



End Class