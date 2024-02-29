'=============Created By Rohit Gupta.=======Date : 05-Jan-2014===========================
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports XpertERPEngine

Public Class frmMccMilkTransportorInvoice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim qry As String
    Dim atchqry As String = ""
    Public strPOInvoice As String = Nothing
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
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
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colQty As String = "COLQTY"
    Const colfreeQty As String = "COLFREEQTY"
    ''Const colRejectedQty As String = "COLREJECTEDQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colDisType As String = "COLDISTYPE"
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



    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colSRNNo As String = "SRNNO"
    Const colLeakQty As String = "COLEAKQTY"
    Const colBurstQty As String = "COLBURSTQTY"
    Const colShortQty As String = "COLSHORTQTY"
    Const colRejectQty As String = "COLREJECTQTY"
    ''Const colLocationCode As String = "LOCATIONCODE"
    ''Const colLocationName As String = "LOCATIONNAME"
    Const colMRP As String = "MRP"
    ''Const colAssessableRate As String = "ASSESSABLERATE"
    Const colAssessableAmount As String = "ASSESSABLEAMT"
    Const colBatchNo As String = "BATCHNO"
    Const colBinNo As String = "colBinNo"
    Const colExpiry As String = "EXPIRYDATE"
    Const colManufactureDate As String = "MANUFACTUREDATE"
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
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"


    Const colACCode As String = "COLACCODE"
    Const colACName As String = "COLACNAME"
    Const colACAmount As String = "COLACAMOUNT"

    Const colLandedRate As String = "LANDEDRATE"
    Const colLandedAmt As String = "LANDEDAMT"

    Const colUnitTotRecTax As String = "colUnitTotRecTax"
    Const colUnitTotNonRecTax As String = "colUnitTotNonRecTax"
    Const colUnitTotAddCost As String = "colUnitTotAddCost"
    '' for abatement PI
    Const colAbatementRate As String = "colAbatementRate"
    Const colAssesableMRP As String = "colAssesableMRP"
    Const colTotalAssesableMRP As String = "colTotalAssesableMRP"
    Const colSRNUnitCost As String = "colSRNUnitCost"
    Dim IsAbatementPO As Boolean

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Dim IsNotIncludeWasteQtyInCal As Boolean = False
    Dim iscalculationonrejqty As Boolean = False
    Dim Is_SRN_Rej_Store_true As Boolean = False
    Dim is_Load_MRN As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetMailRight()
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
        is_Load_MRN = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
        IsAbatementPO = clsPurchaseOrderHead.GetPurchaseSetting().Rows(0).Item("IsAbatementPO")
        Is_SRN_Rej_Store_true = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SRN_Rejected_Store from TSPL_PURCHASE_SETTINGS")) = 0, False, True)
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        LoadBlankGridTax()
        AddNew()
        SetLength()

        LoadBlankGridAC()
        If clsCommon.myLen(strPOInvoice) > 0 Then
            LoadData(strPOInvoice, NavigatorType.Current)
        End If
        IsNotIncludeWasteQtyInCal = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsNotIncludeWasteQtyInCal, clsFixedParameterCode.IsNotIncludeWasteQtyInCal, Nothing)) = 1, True, False)

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
        '===Commented by Rohit on 13 Aug 2014,Because C Form will be Taken According to Tax Rate and will be Define in tax rate Screen. 
        'If objCommonVar.IsDemoERP Then
        '    chkAgainstCForm.Visible = True
        'Else
        '    chkAgainstCForm.Visible = False
        'End If
        '==========================================================
        fndBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(fndBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + fndBillToLocation.Value + "' "))
        End If
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        txtTermCode.Enabled = clsPurchaseOrderHead.GetInventorySetting().Rows(0).Item("IsTermsEditableOnPurchase")
        RadMenuItem1.Visibility = ElementVisibility.Collapsed
        btnsetting.Visible = False
    End Sub

    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            btnsend.Enabled = True
        Else
            btnsend.Enabled = False
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

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        lblVendorName.Text = ""

        DtpLastMaintainedDate.Value = clsCommon.GETSERVERDATE()
        DtpStartDate.Value = clsCommon.GETSERVERDATE()
        DtpExpirationDate.Value = clsCommon.GETSERVERDATE()
        DtpNextInvoiceDate.Value = clsCommon.GETSERVERDATE()
        DtpLastGeneratedDate.Value = clsCommon.GETSERVERDATE()
        TxtExpirationAmount.Text = Nothing

        txtDate.Value = clsCommon.GETSERVERDATE()
        fndBillToLocation.Value = ""
        lblBillToLocation.Text = ""


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
        lblDocAmount.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        lblAddCharges.Text = ""
        lblAddCharges1.Text = ""

        fndBillToLocation.Enabled = True
        rbtnTaxCalAutomatic.IsChecked = True

        TxtExpirationAmount.Text = Nothing
        DtpExpirationDate.Value = Nothing
        CboExpiration.SelectedIndex = 0
        FndScheduler.Value = Nothing
        FndRemitTo.Value = Nothing

        chkExciseOnQty.Checked = False
        chkExciseOnQty.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
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
        repoRowType.Width = 0
        repoRowType.IsVisible = False
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = frmSRN.GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
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
        repoOrgSRNQty.HeaderText = "Accepted Quantity"
        repoOrgSRNQty.Name = colOrgSRNQty
        repoOrgSRNQty.Width = 80
        repoOrgSRNQty.Minimum = 0
        repoOrgSRNQty.ReadOnly = True
        repoOrgSRNQty.IsVisible = False
        repoOrgSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgSRNQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = False

        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoFreeQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreeQty = New GridViewDecimalColumn()
        repoFreeQty.FormatString = ""
        repoFreeQty.HeaderText = "Qty"
        repoFreeQty.Name = colfreeQty
        repoFreeQty.IsVisible = False
        repoFreeQty.ReadOnly = False
        repoFreeQty.Width = 80
        repoFreeQty.Minimum = 0
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

        Dim repoLeadQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeadQty.FormatString = ""
        repoLeadQty.HeaderText = "Leakage"
        repoLeadQty.Name = colLeakQty
        repoLeadQty.Width = 80
        repoLeadQty.Minimum = 0
        repoLeadQty.ReadOnly = True
        repoLeadQty.IsVisible = False
        repoLeadQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeadQty)

        Dim repoBurstQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurstQty.FormatString = ""
        repoBurstQty.HeaderText = "Burst"
        repoBurstQty.Name = colBurstQty
        repoBurstQty.Width = 80
        repoBurstQty.Minimum = 0
        repoBurstQty.ReadOnly = True
        repoBurstQty.IsVisible = False
        repoBurstQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurstQty)

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.FormatString = ""
        repoShortQty.HeaderText = "Shortage"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 80
        repoShortQty.Minimum = 0
        repoShortQty.ReadOnly = True
        repoShortQty.IsVisible = False
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShortQty)

        Dim repoRejectQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRejectQty.FormatString = ""
        repoRejectQty.HeaderText = "Reject"
        repoRejectQty.Name = colRejectQty
        repoRejectQty.Width = 80
        repoRejectQty.Minimum = 0
        repoRejectQty.ReadOnly = True
        repoRejectQty.IsVisible = False
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRejectQty)


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
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.ReadOnly = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)
        'gv1.Columns(colRate).ReadOnly = False

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

        Dim repoDiscountType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDiscountType.FormatString = ""
        repoDiscountType.HeaderText = "Discount Type"
        repoDiscountType.Name = colDisType
        repoDiscountType.Width = 50
        repoDiscountType.ReadOnly = False
        repoDiscountType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDiscountType.DataSource = frmSRN.GetDiscountType()
        repoDiscountType.ValueMember = "Code"
        repoDiscountType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoDiscountType)

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 80
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

        Dim repoTaxRecoverable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable1.HeaderText = "Recoverable Tax 1"
        repoTaxRecoverable1.Name = colTaxRecoverable1
        repoTaxRecoverable1.ReadOnly = True
        repoTaxRecoverable1.IsVisible = False
        repoTaxRecoverable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable1)

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

        Dim repoTaxRecoverable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable2.HeaderText = "Recoverable Tax 2"
        repoTaxRecoverable2.Name = colTaxRecoverable2
        repoTaxRecoverable2.ReadOnly = True
        repoTaxRecoverable2.IsVisible = False
        repoTaxRecoverable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable2)

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

        Dim repoTaxRecoverable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable3.HeaderText = "Recoverable Tax 3"
        repoTaxRecoverable3.Name = colTaxRecoverable3
        repoTaxRecoverable3.ReadOnly = True
        repoTaxRecoverable3.IsVisible = False
        repoTaxRecoverable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable3)

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

        Dim repoTaxRecoverable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable4.HeaderText = "Recoverable Tax 4"
        repoTaxRecoverable4.Name = colTaxRecoverable4
        repoTaxRecoverable4.ReadOnly = True
        repoTaxRecoverable4.IsVisible = False
        repoTaxRecoverable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable4)

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

        Dim repoTaxRecoverable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable5.HeaderText = "Recoverable Tax 5"
        repoTaxRecoverable5.Name = colTaxRecoverable5
        repoTaxRecoverable5.ReadOnly = True
        repoTaxRecoverable5.IsVisible = False
        repoTaxRecoverable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable5)

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


        Dim repoTaxRecoverable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable6.HeaderText = "Recoverable Tax 6"
        repoTaxRecoverable6.Name = colTaxRecoverable6
        repoTaxRecoverable6.ReadOnly = True
        repoTaxRecoverable6.IsVisible = False
        repoTaxRecoverable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable6)

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

        Dim repoTaxRecoverable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable7.HeaderText = "Recoverable Tax 7"
        repoTaxRecoverable7.Name = colTaxRecoverable7
        repoTaxRecoverable7.ReadOnly = True
        repoTaxRecoverable7.IsVisible = False
        repoTaxRecoverable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable7)

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

        Dim repoTaxRecoverable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable8.HeaderText = "Recoverable Tax 8"
        repoTaxRecoverable8.Name = colTaxRecoverable8
        repoTaxRecoverable8.ReadOnly = True
        repoTaxRecoverable8.IsVisible = False
        repoTaxRecoverable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable8)

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


        Dim repoTaxRecoverable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable9.HeaderText = "Recoverable Tax 9"
        repoTaxRecoverable9.Name = colTaxRecoverable9
        repoTaxRecoverable9.ReadOnly = True
        repoTaxRecoverable9.IsVisible = False
        repoTaxRecoverable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable9)

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

        Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaxRecoverable10.HeaderText = "Recoverable Tax 10"
        repoTaxRecoverable10.Name = colTaxRecoverable10
        repoTaxRecoverable10.ReadOnly = True
        repoTaxRecoverable10.IsVisible = False
        repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoTaxRecoverable10)

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
        repoRequition.HeaderText = "SRN No"
        repoRequition.Name = colSRNNo
        repoRequition.ReadOnly = True
        repoRequition.IsVisible = False
        repoRequition.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRequition)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoLandedRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedRate.FormatString = ""
        repoLandedRate.HeaderText = "Landed Rate"
        repoLandedRate.Name = colLandedRate
        repoLandedRate.WrapText = True
        repoLandedRate.Width = 80
        repoLandedRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedRate.ReadOnly = True
        repoLandedRate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLandedRate)

        Dim repoLandedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedAmt.FormatString = ""
        repoLandedAmt.HeaderText = "Landed Amount"
        repoLandedAmt.Name = colLandedAmt
        repoLandedAmt.WrapText = True
        repoLandedAmt.Width = 80
        repoLandedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLandedAmt.ReadOnly = True
        repoLandedAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLandedAmt)

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
        repoBinNo.IsVisible = False
        repoBinNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.ReadOnly = True
        repoBatchNo.IsVisible = False
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
        repoExpiry.IsVisible = False
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
        repoManDate.IsVisible = False
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

        Dim repoMannulaAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMannulaAmt.FormatString = ""
        repoMannulaAmt.HeaderText = "Is Mannual amount"
        repoMannulaAmt.Name = colIsMannualAmt
        repoMannulaAmt.IsVisible = False
        repoMannulaAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMannulaAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMannulaAmt)

        '' for abatenment PI
        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.WrapText = True
        repoAbatementRate.ReadOnly = True
        repoAbatementRate.FormatString = ""
        repoAbatementRate.Width = 100
        repoAbatementRate.HeaderText = "Abatement Rate"
        repoAbatementRate.Name = colAbatementRate
        repoAbatementRate.IsVisible = IsAbatementPO
        repoAbatementRate.Minimum = 0
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAbatementRate)

        Dim repoAssesableMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssesableMRP.WrapText = True
        repoAssesableMRP.ReadOnly = True
        repoAssesableMRP.FormatString = ""
        repoAssesableMRP.Width = 150
        repoAssesableMRP.HeaderText = "Assessable MRP"
        repoAssesableMRP.Name = colAssesableMRP
        repoAssesableMRP.IsVisible = IsAbatementPO
        repoAssesableMRP.Minimum = 0
        repoAssesableMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssesableMRP)

        Dim repoTotalAssesableMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAssesableMRP.WrapText = True
        repoTotalAssesableMRP.ReadOnly = True
        repoTotalAssesableMRP.FormatString = ""
        repoTotalAssesableMRP.Width = 150
        repoTotalAssesableMRP.HeaderText = "Total Assessable MRP"
        repoTotalAssesableMRP.Name = colTotalAssesableMRP
        repoTotalAssesableMRP.IsVisible = IsAbatementPO
        repoTotalAssesableMRP.Minimum = 0
        repoTotalAssesableMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalAssesableMRP)
        'clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)
        '' Anubhooti 21-Oct-2014 BM00000004222
        Dim repoSRNUCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSRNUCost = New GridViewDecimalColumn()
        repoSRNUCost.FormatString = ""
        repoSRNUCost.HeaderText = "SRN Unit Cost"
        repoSRNUCost.Name = colSRNUnitCost
        repoSRNUCost.Width = 80
        repoSRNUCost.Minimum = 0
        repoSRNUCost.FormatString = "{0:n4}"
        repoSRNUCost.DecimalPlaces = 4
        repoSRNUCost.IsVisible = False
        repoSRNUCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSRNUCost)
        ''
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
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
        repoTaxAmt.Minimum = 0
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
    Function checkVendorItemPrice() As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                Dim strCode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)
                Dim cellPrice As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value)
                Dim vendorPrice As Double = clsDBFuncationality.getSingleValue("select item_rate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and item_no='" & strCode & "'")
                If cellPrice > vendorPrice Then
                    clsCommon.MyMessageBoxShow("The Larger Price Of Item is not Allowed then the Vendor Item Price  at Row no " & (i + 1))
                    Return False
                End If
            Next
        Else
            Return True
        End If
        Return True
    End Function
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAmt) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colfreeQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDisPer) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colBatchNo) OrElse e.Column Is gv1.Columns(colExpiry) OrElse e.Column Is gv1.Columns(colManufactureDate) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal) Then
                        If ((clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colfreeQty) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colRate) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (e.Column Is gv1.Columns(colAmt) AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal)) Then
                            If e.Column Is gv1.Columns(colRate) Then
                                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLargerItemCostThenVendorItemCost, clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost, Nothing)) = 0 And clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchasePickItemFromVendorItemDetails, clsFixedParameterCode.PurchasePickItemFromVendorItemDetails, Nothing)) = 1 Then
                                    Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                    Dim cellPrice As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
                                    Dim vendorPrice As Double = clsDBFuncationality.getSingleValue("select item_rate from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and item_no='" & strCode & "'")
                                    If cellPrice > vendorPrice Then
                                        clsCommon.MyMessageBoxShow(Me, "The Larger Price Of Item is not Allowed then the Vendor Item Price ", Me.Text)
                                        gv1.CurrentRow.Cells(colRate).Value = vendorPrice
                                    End If

                                End If
                            End If
                            If (clsCommon.CompairString(e.Column.Name, colQty) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0) Then

                                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPendingQty).Value)
                                Dim dblEnteredQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                If dblEnteredQty > dblPendingQty Then
                                    common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblEnteredQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQty))
                                    gv1.CurrentRow.Cells(colQty).Value = dblPendingQty
                                End If
                            End If

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()

                        ElseIf (clsCommon.CompairString(e.Column.Name, colICode) = CompairStringResult.Equal) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colAmt) Then

                            ' ''If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 Then
                            ' ''    gv1.CurrentRow.Cells(colRate).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmt).Value) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 2, MidpointRounding.ToEven)
                            ' ''Else
                            ' ''    gv1.CurrentRow.Cells(colAmt).Value = 0
                            ' ''End If

                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            If rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRow(ii)
                                Next
                            End If
                            UpdateAllTotals()
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

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            ''If intCurrRow = gv1.Rows.Count - 1 Then
            ''    gv1.Rows.AddNew()
            ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''End If
            ''If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            ''    If gv1.CurrentColumn Is gv1.Columns(colICode) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colQty)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colRate)
            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
            ''        gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        gv1.CurrentColumn = gv1.Columns(colDisPer)

            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colMRP)
            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colMRP) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colBatchNo)
            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colBatchNo) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colExpiry) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colManufactureDate) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colSpecification)
            ''        ''ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
            ''        ''    gv1.CurrentRow = gv1.Rows(intCurrRow)
            ''        ''    gv1.CurrentColumn = gv1.Columns(colRemarks)

            ''    ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
            ''        If Not (intCurrRow = gv1.Rows.Count - 1) Then
            ''            gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
            ''        End If
            ''        gv1.CurrentColumn = gv1.Columns(colICode)
            ''    End If
            ''End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

        Dim dblTaxAssessableAmt As Double = 0
        Dim dblTotalAssesableMRP As Double = 0

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
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
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

                dblTaxAssessableAmt = dblTaxAssessableAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableAmount).Value)
                dblTotalAssesableMRP = dblTotalAssesableMRP + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalAssesableMRP).Value)

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


        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If chkExciseOnQty.Checked Then
                            If dblTaxAssessableAmt <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxAssessableAmt, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        ElseIf dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = Math.Round(dblTaxAssessableAmt, 2)
                    Case 2
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 3
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 4
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                End Select
            Next
        End If


        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colACAmount).Value)
            End If
            '' abatement PO
            If IsAbatementPO Then
                gv1.CurrentRow.Cells(colAssesableMRP).Value = gv1.CurrentRow.Cells(colMRP).Value - (gv1.CurrentRow.Cells(colMRP).Value * gv1.CurrentRow.Cells(colAbatementRate).Value / 100)
                gv1.CurrentRow.Cells(colTotalAssesableMRP).Value = gv1.CurrentRow.Cells(colQty).Value * gv1.CurrentRow.Cells(colAssesableMRP).Value
            End If
        Next

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        dblNetAmt = dblNetAmt + dblACAmount
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblDocAmount.Text = lblTotRAmt.Text
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
        LoadBlankGridAC()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields

        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave() As Boolean
        Try
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next

            UpdateAllTotals()
            If Not objRemittance Is Nothing Then
                UpdateTDSAmount()
            End If
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
            If clsCommon.myLen(fndBillToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Bill to Location", Me.Text)
                fndBillToLocation.Focus()
                Return False
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "PI No Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If


            Dim dtCurrDate As Date = clsCommon.GETSERVERDATE()
            If txtDate.Value > dtCurrDate Then
                common.clsCommon.MyMessageBoxShow("Invoice Date can't be more then Current Date")
                txtDate.Focus()
                Return False
            End If






            ''If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtBillToLocation.Value)) Then
            ''    common.clsCommon.MyMessageBoxShow("Location should be Excisable for Raw Material")
            ''    txtBillToLocation.Focus()
            ''    Return False
            ''End If

            Dim arrReqNo As New List(Of String)
            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblPendingQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPendingQty).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
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
            If Not checkVendorItemPrice() Then
                Return False
            End If

            clsSRNHead.IsValidVendorForSRN(arrReqNo, txtVendorNo.Value)
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    '' Anubhooti 27-Oct-2014
    Private Function SRNItemRate(ByVal strSRNNo As String, ByVal strItemCode As String) As Double
        Dim ItemRate As Double = 0
        ItemRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select TSPL_SRN_DETAIL.Item_Cost  From TSPL_Mcc_Milk_Transport_Invoice_Detail  Left Outer Join TSPL_SRN_DETAIL  On TSPL_SRN_DETAIL.SRN_No    = TSPL_Mcc_Milk_Transport_Invoice_Detail.SRN_Id  Where SRN_Id ='" & strSRNNo & "' And TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "'"))
        Return ItemRate
    End Function
    ''
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If ChekPostBtn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Invoice", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''
            If (AllowToSave()) Then
                Dim obj As New clsMilkTransporterInvoiceMCC()
                obj.PI_No = txtDocNo.Value
                obj.PI_Date = txtDate.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Ref_No = txtRefNo.Text
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Remarks = txtRemarks.Text
                obj.Bill_To_Location = fndBillToLocation.Value

                obj.Comments = txtComment.Text
                obj.Description = txtDesc.Text

                obj.Tax_Group = txtTaxGroup.Value

                obj.Scheduler_code = FndScheduler.Value
                obj.Remit_To = FndRemitTo.Value
                obj.Expiration_Date = DtpExpirationDate.Value
                obj.Expiration_Amount = clsCommon.myCdbl(TxtExpirationAmount.Text)

                If DtpExpirationDate.Checked Then
                    obj.Invdate = DtpExpirationDate.Value
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
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If

                obj.is_Excise_On_Qty = chkExciseOnQty.Checked
                If objRemittance IsNot Nothing Then
                    obj.objPIRemittance = New clsPIRemittance()
                    obj.objPIRemittance.Vendor_Code = objRemittance.Vendor_Code
                    obj.objPIRemittance.Vendor_Name = objRemittance.Vendor_Name
                    obj.objPIRemittance.Document_No = objRemittance.Document_No
                    obj.objPIRemittance.Document_Date = objRemittance.Document_Date
                    obj.objPIRemittance.Document_Type = objRemittance.Document_Type
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
                obj.PI_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)



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
                obj.Tot_Empty_Amount = 0
                obj.IsAbatementPO = IsAbatementPO

                obj.LRNo = CboExpiration.Text 'txtCurrentCount.Text

                obj.Arr = New List(Of clsMilkTransporterInvoiceMCCDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsMilkTransporterInvoiceMCCDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.PI_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(colRowType).Value)
                    ''objTr.Rejected_Qty = clsCommon.myCdbl(grow.Cells(colRejectedQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    ''added bu usha--------
                    objTr.Free_qty = clsCommon.myCdbl(grow.Cells(colfreeQty).Value)
                    '---end-----
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.SRN_Id = clsCommon.myCstr(grow.Cells(colSRNNo).Value)
                    'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Disc_Type = clsCommon.myCdbl(grow.Cells(colDisType).Value)
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
                    objTr.Location = fndBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
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

                    objTr.Leak_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                    objTr.Burst_Qty = clsCommon.myCdbl(grow.Cells(colBurstQty).Value)
                    objTr.Short_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                    objTr.Reject_Qty = clsCommon.myCdbl(grow.Cells(colRejectQty).Value)
                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(grow.Cells(colIsMannualAmt).Value)
                    objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
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
                        objTr.Empty_Amount = dblVal * objTr.PI_Qty
                        obj.Tot_Empty_Amount += objTr.Empty_Amount
                    End If

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    If IsAbatementPO Then
                        objTr.AbatementRate = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                        objTr.AssessableMRP = clsCommon.myCdbl(grow.Cells(colAssesableMRP).Value)
                        objTr.TotalAssessableMRP = clsCommon.myCdbl(grow.Cells(colTotalAssesableMRP).Value)
                    End If
                Next

                obj.objJVC = SetPJVData(obj)
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
                '' end CurrencyConversion
                ' Dim trans As SqlTransaction
                If iscalculationonrejqty Then
                    'By Balwinder on 18/12/2014 no need to make debit note.
                    'If ChekPostBtn And Is_SRN_Rej_Store_true Then
                    '    Dim trans As SqlTransaction
                    '    obj.SaveDebitNoteEntry(obj, trans)
                    'End If
                Else
                    If (obj.SaveData(obj, isNewEntry)) Then
                        If ChekPostBtn And Is_SRN_Rej_Store_true Then
                            iscalculationonrejqty = True
                            'Dim rejqty As Double
                            If iscalculationonrejqty Then
                                Dim Dtrej As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_SRN_DETAIL where SRN_No='" & obj.Against_SRN & "'")
                                If Dtrej.Select("Rejected_Qty>0").Length <= 0 Then
                                    iscalculationonrejqty = False
                                    GoTo a
                                End If
                                For Each dr As DataRow In Dtrej.Rows()
                                    If dr("Rejected_Qty") > 0 Then
                                        For Each grow As GridViewRowInfo In gv1.Rows
                                            If clsCommon.myCstr(grow.Cells(colICode).Value) = dr("Item_Code") Then
                                                grow.Cells(colQty).Value = dr("Rejected_Qty")
                                            End If
                                        Next
                                        For ii As Integer = 0 To gv1.Rows.Count - 1
                                            UpdateCurrentRow(ii)
                                        Next
                                        UpdateAllTotals()
                                    End If
                                Next

                                SaveData(ChekPostBtn)
                                iscalculationonrejqty = False
                            End If
                        End If
a:                      UcAttachment1.SaveData(obj.PI_No)

                        If ChekPostBtn = False Then
                            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        End If
                        LoadData(obj.PI_No, NavigatorType.Current)
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsMilkTransporterInvoiceMCC
                obj.PI_No = clsCommon.myCstr(txtDocNo.Value)

                obj.LRNo = CboExpiration.Text 'txtCurrentCount.Text
                If clsMilkTransporterInvoiceMCC.UpdateSecondaryInfo(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsMilkTransporterInvoiceMCCDetail)) As String
        For Each objtr As clsMilkTransporterInvoiceMCCDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Private Function SetPJVData(ByVal objPI As clsMilkTransporterInvoiceMCC) As clsPJVHead
        Dim obj As New clsPJVHead
        obj.PJV_No = lblPJVNo.Text
        obj.Vendor_Invoice_No = objPI.Vendor_Invoice_No
        obj.PJV_Date = objPI.PI_Date
        obj.Vendor_Code = objPI.Vendor_Code
        obj.Vendor_Name = objPI.Vendor_Name
        obj.PO_No = objPI.Against_PO
        obj.Dept = objPI.Dept
        obj.Dept_Desc = objPI.Dept_Desc
        obj.Narration = objPI.Description
        If clsCommon.myLen(obj.PO_No) > 0 Then
            obj.PO_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select PurchaseOrder_Date from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + obj.PO_No + "'"))
        End If
        obj.SRN_No = objPI.Against_SRN
        If clsCommon.myLen(obj.SRN_No) > 0 Then
            obj.SRN_Date = clsDBFuncationality.getSingleValue("select SRN_Date from TSPL_SRN_HEAD where SRN_No='" + obj.SRN_No + "'")
        End If
        obj.Invoice_No = objPI.PI_No
        obj.Invoice_Date = objPI.PI_Date
        obj.PJV_Net_Amount = objPI.PI_Total_Amt
        If objPI.objPIRemittance IsNot Nothing Then
            obj.PJV_TDS_Amount = objPI.objPIRemittance.Calculated_Total_TDS
        End If
        obj.PJV_Amount = objPI.PI_Total_Amt - obj.PJV_TDS_Amount
        obj.Due_Date = objPI.Due_Date

        Dim ii As Integer = 1


        Dim ArrTemp As List(Of clsPJVDetail) = New List(Of clsPJVDetail)

        ArrTemp = New List(Of clsPJVDetail)
        Dim Account_Set As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objPI.Vendor_Code + "'"))
        If (clsCommon.myLen(Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + objPI.Vendor_Name)
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,TSPL_GL_ACCOUNTS.Description,Discount_Account  from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_ACCOUNT_SET.Payable_Account where TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code='" + Account_Set + "'")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Vendor's Control Account not found for Purchase jouranl Voucher")
        End If

        Dim objTR As clsPJVDetail = New clsPJVDetail()
        Dim isFirstTime As Boolean = True
        Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(objPI.Arr)

        ''Dim dblEmpyAmount As Double = 0
        Dim strEmptyAccount As String = ""
        For Each objPIDetail As clsMilkTransporterInvoiceMCCDetail In objPI.Arr
            ''Fill VendorInvoice details Data
            Dim strICode As String = objPIDetail.Item_Code
            If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                strICode = strFirstItemCodeNonItemRowType
            End If

            Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
            End If
            Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt1.Rows(0)("Inv_Payable_Clearing"))
            strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, objPI.Bill_To_Location, Nothing)
            Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'"))
            Dim dblRecoverableAmt As Double = GetTaxAmt(objPIDetail)
            objTR = New clsPJVDetail()
            objTR.Line_No = ii
            objTR.GL_Account_Code = strPaybleCleanigCtrlAC
            objTR.GL_Account_Desc = strPaybleCleanigCtrlACName
            objTR.PJV_Amount = objPIDetail.Landed_Cost_Amount ''+ dblRecoverableAmt + IIf(isFirstTime, objPI.Total_Add_Charge, 0)
            ArrTemp.Add(objTR)
            ii = ii + 1
            'totDrAmt = totDrAmt + objPIDetail.Amount + dblRecoverableAmt + IIf(isFirstTime, objPI.Total_Add_Charge - objPI.Discount_Amt, 0)
            isFirstTime = False

            If objPIDetail.Empty_Amount > 0 AndAlso clsCommon.myLen(strEmptyAccount) <= 0 Then
                strEmptyAccount = clsCommon.myCstr(dt1.Rows(0)("EmptyAccount"))
                strEmptyAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strEmptyAccount, objPI.Bill_To_Location, Nothing)
            End If

            'If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
            '    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, Nothing))
            '    dblEmpyAmount += dblVal * objPIDetail.PI_Qty
            'End If
        Next

        If objPI.Tot_Empty_Amount > 0 Then
            If clsCommon.myLen(strEmptyAccount) <= 0 Then
                Throw New Exception("Please set Inventory Control Empties")
            End If
            obj.PJV_Amount += objPI.Tot_Empty_Amount
        End If


        Dim strRecAc As String = ""
        Dim isTaxRecoverable As Boolean = False

        Dim amtCal As Double = 0
        Dim objTM As clsTaxMaster = clsTaxMaster.GetData(objPI.TAX1, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If

        objTM = clsTaxMaster.GetData(objPI.TAX2, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX3, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX4, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX5, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX6, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX7, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX8, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX9, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If
        objTM = clsTaxMaster.GetData(objPI.TAX10, Nothing)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If

            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal

            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, fndBillToLocation.Value)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strRecAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc)
                amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                objTR.PJV_Amount = amtCal
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + amtCal
            End If
        End If

        objTR = New clsPJVDetail()
        objTR.Line_No = ii
        Dim strPayableAc As String = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
        strPayableAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPayableAc, objPI.Bill_To_Location, Nothing)
        objTR.GL_Account_Code = strPayableAc
        objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc) ' 
        objTR.PJV_Amount = -1 * (objPI.PI_Total_Amt + objPI.Tot_Empty_Amount)
        'totCrAmt = totCrAmt + (objPI.PI_Total_Amt + objPI.Tot_Empty_Amount)
        ii = ii + 1
        ArrTemp.Add(objTR)

        If objPI.Tot_Empty_Amount > 0 Then
            objTR = New clsPJVDetail()
            objTR.Line_No = ii
            objTR.GL_Account_Code = strEmptyAccount
            objTR.GL_Account_Desc = clsGLAccount.GetName(strEmptyAccount) ' 
            objTR.PJV_Amount = objPI.Tot_Empty_Amount
            'totDrAmt = totDrAmt + objPI.Tot_Empty_Amount
            ii = ii + 1
            ArrTemp.Add(objTR)
        End If


        ''If objPI.Discount_Amt > 0 Then
        ''    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Discount_Account"))) <= 0 Then
        ''        Throw New Exception("Discount GL Account Not found")
        ''    End If
        ''    objTR = New clsPJVDetail()
        ''    objTR.Line_No = ii
        ''    Dim strDisAccount As String = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
        ''    strDisAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strDisAccount, txtBillToLocation.Value)
        ''    objTR.GL_Account_Code = strDisAccount

        ''    objTR.GL_Account_Desc = clsGLAccount.GetName(strDisAccount)
        ''    objTR.PJV_Amount = -1 * objPI.Discount_Amt
        ''    totCrAmt = totCrAmt + objPI.Discount_Amt
        ''    ArrTemp.Add(objTR)
        ''    ii = ii + 1
        ''End If

        If (objPI.objPIRemittance IsNot Nothing) Then
            If objPI.objPIRemittance.Actual_Total_TDS <> 0 Then
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                Dim strPayableAC1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Payable_Account")), fndBillToLocation.Value)
                objTR.GL_Account_Code = strPayableAC1
                objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAC1)
                objTR.PJV_Amount = objPI.objPIRemittance.Actual_Total_TDS
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + objPI.objPIRemittance.Actual_Total_TDS

                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                'Dim STRBranchGLAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Account from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + objPI.objPIRemittance.Branch_Code + "'"))
                Dim STRBranchGLAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD Where Deduction_Code='" + objPI.objPIRemittance.Deduction_Code + "'"))
                If clsCommon.myLen(STRBranchGLAc) <= 0 Then
                    Throw New Exception("Please set GL Account for Deduction code" + objPI.objPIRemittance.Deduction_Code)
                End If
                STRBranchGLAc = clsERPFuncationality.ChangeGLAccountLocationSegment(STRBranchGLAc, fndBillToLocation.Value)
                objTR.GL_Account_Code = STRBranchGLAc
                objTR.GL_Account_Desc = clsGLAccount.GetName(STRBranchGLAc)
                objTR.PJV_Amount = -1 * objPI.objPIRemittance.Actual_Total_TDS
                'totCrAmt = totCrAmt + objPI.objPIRemittance.Actual_Total_TDS
                ArrTemp.Add(objTR)
                ii = ii + 1
            End If
        End If

        obj.Arr = MergePJV(ArrTemp)

        If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
            Throw New Exception("No GL Account Found For PJV")
        End If

        Dim dblTotDrAmt As Decimal = 0
        Dim dblTotCrAmt As Decimal = 0
        For jj As Integer = 0 To obj.Arr.Count - 1
            If obj.Arr(jj).PJV_Amount > 0 Then
                dblTotDrAmt += Math.Round(clsCommon.myCdbl(obj.Arr(jj).PJV_Amount), 2, MidpointRounding.ToEven)
            Else
                dblTotCrAmt += -1 * Math.Round(clsCommon.myCdbl(obj.Arr(jj).PJV_Amount), 2, MidpointRounding.ToEven)
            End If
        Next
        Dim dblDiffence As Double = dblTotDrAmt - dblTotCrAmt
        dblDiffence = Math.Round(dblDiffence, 2, MidpointRounding.ToEven)
        If Math.Abs(dblDiffence) <= 0.99 Then ''Against ticket BM00000002283 change differece 1 paise to 99 paise
            obj.Arr(0).PJV_Amount = obj.Arr(0).PJV_Amount - dblDiffence ''Working for all four conditions.
        End If


        Return obj
    End Function

    Function MergePJV(ByVal ArrTemp As List(Of clsPJVDetail)) As List(Of clsPJVDetail)
        Dim ArrReturn As List(Of clsPJVDetail) = Nothing
        If ArrTemp IsNot Nothing AndAlso ArrTemp.Count > 0 Then
            ArrReturn = New List(Of clsPJVDetail)
            For Each Str As clsPJVDetail In ArrTemp
                Dim isFound As Boolean = False
                If ArrReturn IsNot Nothing AndAlso ArrReturn.Count > 0 Then
                    For ii As Integer = 0 To ArrReturn.Count - 1
                        If clsCommon.CompairString(ArrReturn(ii).GL_Account_Code, Str.GL_Account_Code) = CompairStringResult.Equal Then
                            isFound = True
                            ArrReturn(ii).PJV_Amount += Str.PJV_Amount
                            Exit For
                        End If
                    Next
                End If

                If Not isFound Then
                    Dim objTR As clsPJVDetail = New clsPJVDetail()
                    objTR.GL_Account_Code = Str.GL_Account_Code
                    objTR.GL_Account_Desc = Str.GL_Account_Desc
                    objTR.PJV_Amount = Str.PJV_Amount
                    ArrReturn.Add(objTR)
                End If
            Next
        End If
        Return ArrReturn
    End Function

    Function GetTaxAmt(ByVal objPIDetail As clsMilkTransporterInvoiceMCCDetail) As Double
        Dim dblTotalTax As Double = 0
        Dim isTaxRecoverable As Boolean = False
        If objPIDetail.TAX1_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX1) Then
            dblTotalTax += objPIDetail.TAX1_Amt
        End If
        If objPIDetail.TAX2_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX2) Then
            dblTotalTax += objPIDetail.TAX2_Amt
        End If
        If objPIDetail.TAX3_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX3) Then
            dblTotalTax += objPIDetail.TAX3_Amt
        End If
        If objPIDetail.TAX4_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX4) Then
            dblTotalTax += objPIDetail.TAX4_Amt
        End If
        If objPIDetail.TAX5_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX5) Then
            dblTotalTax += objPIDetail.TAX5_Amt
        End If
        If objPIDetail.TAX6_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX6) Then
            dblTotalTax += objPIDetail.TAX6_Amt
        End If
        If objPIDetail.TAX7_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX7) Then
            dblTotalTax += objPIDetail.TAX7_Amt
        End If
        If objPIDetail.TAX8_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX8) Then
            dblTotalTax += objPIDetail.TAX8_Amt
        End If
        If objPIDetail.TAX9_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX9) Then
            dblTotalTax += objPIDetail.TAX9_Amt
        End If
        If objPIDetail.TAX10_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX10) Then
            dblTotalTax += objPIDetail.TAX10_Amt
        End If
        Return dblTotalTax
    End Function

    Sub CalAddtionalAmt()
        Dim dblLandedCost As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotItemCost As String = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = RowTypeItem Then
                dblTotItemCost = dblTotItemCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
            If gv1.Rows(ii).Cells(colRowType).Value = RowTypeMisc Then
                dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + CDec(lblAddCharges.Text)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(ii).Cells(colRowType).Value = RowTypeItem Then
                dblLandedCost = gv1.Rows(ii).Cells(colAmt).Value / dblTotItemCost * dblAdditionalAmt
                If dblAdditionalAmt = 0 Then
                    gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                Else
                    If dblLandedCost <> 0 Then
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = Math.Round(dblLandedCost / (CDec(gv1.Rows(ii).Cells(colQty).Value) + CDec(gv1.Rows(ii).Cells(colLeakQty).Value) + CDec(gv1.Rows(ii).Cells(colShortQty).Value) + CDec(gv1.Rows(ii).Cells(colRejectQty).Value) + CDec(gv1.Rows(ii).Cells(colBurstQty).Value)), 6)
                    Else
                        gv1.Rows(ii).Cells(colUnitTotAddCost).Value = 0
                    End If
                End If
            End If
        Next
    End Sub

    ''Sub CalLandAmt()
    ''    Dim dblLandedCost As Double = 0
    ''    Dim dblAdditionalAmt As Double = 0
    ''    Dim dblTotItemCost As String = 0
    ''    For ii As Integer = 0 To gv1.Rows.Count - 1
    ''        If gv1.Rows(ii).Cells(colRowType).Value = RowTypeItem Then
    ''            dblTotItemCost = dblTotItemCost + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt1).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt2).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt3).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt4).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt5).Value)
    ''        End If
    ''        If clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) = False Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colTaxAmt6).Value)
    ''        End If
    ''        If gv1.Rows(ii).Cells(colRowType).Value = RowTypeMisc Then
    ''            dblAdditionalAmt = dblAdditionalAmt + CDec(gv1.Rows(ii).Cells(colAmt).Value)
    ''        End If
    ''    Next
    ''    dblAdditionalAmt = dblAdditionalAmt + CDec(lblAddCharges.Text)
    ''    For ii As Integer = 0 To gv1.Rows.Count - 1
    ''        If gv1.Rows(ii).Cells(colRowType).Value = RowTypeItem Then
    ''            dblLandedCost = (gv1.Rows(ii).Cells(colAmt).Value / dblTotItemCost * dblAdditionalAmt) / (CDec(gv1.Rows(ii).Cells(colQty).Value) + CDec(gv1.Rows(ii).Cells(colLeakQty).Value) + CDec(gv1.Rows(ii).Cells(colShortQty).Value) + CDec(gv1.Rows(ii).Cells(colBurstQty).Value))
    ''            If dblLandedCost > 0 Then
    ''                gv1.Rows(ii).Cells(colLandedRate).Value = Math.Round(dblLandedCost + CDec(gv1.Rows(ii).Cells(colRate).Value), 4)
    ''                gv1.Rows(ii).Cells(colLandedAmt).Value = Math.Round((dblLandedCost + CDec(gv1.Rows(ii).Cells(colRate).Value)) * (CDec(gv1.Rows(ii).Cells(colQty).Value) + CDec(gv1.Rows(ii).Cells(colLeakQty).Value) + CDec(gv1.Rows(ii).Cells(colShortQty).Value) + CDec(gv1.Rows(ii).Cells(colBurstQty).Value)), 4)
    ''            Else
    ''                gv1.Rows(ii).Cells(colLandedRate).Value = gv1.Rows(ii).Cells(colRate).Value
    ''                gv1.Rows(ii).Cells(colLandedAmt).Value = gv1.Rows(ii).Cells(colAmt).Value
    ''            End If
    ''        End If
    ''    Next
    ''End Sub

    Sub CalLandAmt()
        Dim dblLandedCost As Double = 0
        Dim dblAdditionalAmt As Double = 0
        Dim dblTotAmtAfterDiscount As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeItem) = CompairStringResult.Equal Then
                    dblTotAmtAfterDiscount = dblTotAmtAfterDiscount + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), RowTypeMisc) = CompairStringResult.Equal Then
                    dblAdditionalAmt = dblAdditionalAmt + IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                End If


                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable1).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                End If
                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable2).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                End If
                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable3).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                End If
                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable4).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                End If
                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable5).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                End If
                If Not clsCommon.myCBool(gv1.Rows(ii).Cells(colTaxRecoverable6).Value) Then
                    dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                End If

            End If
        Next
        dblAdditionalAmt = dblAdditionalAmt + clsCommon.myCdbl(lblAddCharges.Text)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                If gv1.Rows(ii).Cells(colRowType).Value = RowTypeItem Then
                    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value)
                    If dblQty > 0 Then
                        Dim dblAmtAfterDiscount As Double = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisType).Value) = 0, clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value))
                        dblLandedCost = dblAmtAfterDiscount + IIf(dblTotAmtAfterDiscount > 0, ((dblAdditionalAmt * dblAmtAfterDiscount) / dblTotAmtAfterDiscount), 0)

                        gv1.Rows(ii).Cells(colLandedAmt).Value = Math.Round(dblLandedCost, 2)
                        gv1.Rows(ii).Cells(colLandedRate).Value = Math.Round(dblLandedCost / dblQty, 4)
                    End If
                End If
            End If
        Next
    End Sub

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
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value)
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
                Dim dblTotQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejectQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colBurstQty).Value)
                If dblTotQty = 0 Then
                    dblTotQty = 1
                End If
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = dblAdditionalAmt / dblTotQty
            Else
                gv1.Rows(ii).Cells(colUnitTotRecTax).Value = 0
            End If
        Next
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, Optional ByVal rejqty As Double = 0)
        Try
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

            fndBillToLocation.Enabled = False
            Dim obj As New clsMilkTransporterInvoiceMCC()
            obj = clsMilkTransporterInvoiceMCC.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PI_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False

                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                End If

                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.PI_No
                txtDate.Value = obj.PI_Date
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRefNo.Text = obj.Ref_No
                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group
                FndScheduler.Value = obj.Scheduler_code
                LblScheduler.Text = clsDBFuncationality.getSingleValue("select Description from Tspl_Recurring_Scheduler_Head where DOC_CODE='" & FndScheduler.Value & "'")
                FndRemitTo.Value = obj.Remit_To
                CboExpiration.SelectedValue = obj.LRNo
                If clsCommon.myLen(obj.Expiration_Date) > 0 Then
                    DtpExpirationDate.Value = obj.Expiration_Date
                End If
                TxtExpirationAmount.Text = obj.Expiration_Amount



                txtComment.Text = obj.Comments

                fndBillToLocation.Value = obj.Bill_To_Location
                txtRemarks.Text = obj.Remarks


                If obj.Invdate IsNot Nothing Then
                    DtpExpirationDate.Value = obj.Invdate
                    DtpExpirationDate.Checked = True
                Else
                    DtpExpirationDate.Checked = False
                End If


                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                If obj.objJVC IsNot Nothing AndAlso clsCommon.myLen(obj.objJVC.PJV_No) > 0 Then
                    lblPJVNo.Text = obj.objJVC.PJV_No
                End If

                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If

                txtTermCode.Value = obj.Terms_Code
                'lblTermName.Text = obj.Terms_Description
                txtDueDate.Value = obj.Due_Date
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblTotRAmt.Text = clsCommon.myFormat(obj.PI_Total_Amt)
                lblDocAmount.Text = lblTotRAmt.Text

                lblBillToLocation.Text = obj.BillToLocationName

                lblTaxGrpName.Text = obj.TaxGroupName
                lblTermName.Text = obj.TermsName

                chkExciseOnQty.Checked = obj.is_Excise_On_Qty

                txtCurrentCount.Text = obj.LRNo
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



                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkTransporterInvoiceMCCDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.Row_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = IIf(objTr.Status = 0, "No", "Yes")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = objTr.OrgSRNQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        '-------added by usha
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfreeQty).Value = objTr.Free_qty
                        '------ends here--------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = IIf(iscalculationonrejqty And rejqty > 0, rejqty, objTr.PI_Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = objTr.SRN_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        '' Anubhooti 27-Oct-2014
                        If clsCommon.myLen(obj.Against_SRN) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value = SRNItemRate(clsCommon.myCstr(objTr.SRN_Id), clsCommon.myCstr(objTr.Item_Code))
                            'If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value) > 0 Then
                            '    'gv1.Columns(colRate).ReadOnly = True
                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                            'Else
                            '    'gv1.Columns(colRate).ReadOnly = False
                            '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                            'End If
                        End If
                        ''

                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = objTr.Disc_Type
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        If objTr.Expiry_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiry).Value = objTr.Expiry_Date
                        End If

                        If objTr.MFG_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManufactureDate).Value = objTr.MFG_Date
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leak_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = objTr.Burst_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Short_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectQty).Value = objTr.Reject_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedRate).Value = objTr.Landed_Cost_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Cost_Amount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotNonRecTax).Value = objTr.Total_NonRecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotRecTax).Value = objTr.Total_RecTax_PerUnit
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitTotAddCost).Value = objTr.Total_AddtionalCost_PerUnit

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = objTr.Is_Mannual_Amt

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssessableAmount).Value = objTr.AssessableAmt
                        If clsCommon.myLen(objTr.SRN_Id) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = clsSRNDetail.GetBalanceSRNQty(objTr.SRN_Id, objTr.Item_Code, obj.PI_No, objTr.Unit_code, objTr.MRP, objTr.Assessable)
                        End If
                        '' abatement PO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = objTr.AbatementRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = objTr.AssessableMRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = objTr.TotalAssessableMRP
                    Next

                End If
                SetitemWiseTaxOnlySetting()
                gvAC.Rows.AddNew()

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PI_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.PI_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' MULTICURRENCY
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  MULTICURRENCY
                UcAttachment1.LoadData(obj.PI_No)

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
        Me.Close()
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                'Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                'Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVeRateTAXFND", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(fndBillToLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtVendorNo.Value, "P")

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
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                '' Anubhooti 12-Sep-2014 BM00000003735
                'If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Invoice", txtDate.Value) = False Then
                '    Exit Sub
                'End If
                ''
                If (clsMilkTransporterInvoiceMCC.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
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
                    'SMSSENDONLY(True)
                End If

                If (common.clsCommon.MyMessageBoxShow("Do you  want to print", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    print()
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
                If (clsMilkTransporterInvoiceMCC.DeleteData(txtDocNo.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_Mcc_Milk_Transport_Invoice_HEAD where Doc_No='" + txtDocNo.Value + "'"
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
        qry = "select DOc_No as Code,DOc_Date as Date,Vendor_Code as [Vendor Code], Vendor_Name as Vendor,Vendor_Invoice_No as [Vendor Invoice No],Convert(Varchar(12),InvoiceDate,103) as [Invoice Date],PI_Total_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status] ,(select top 1 PJV_No from TSPL_PJV_HEAD where Invoice_No=Doc_No) as [PJV No],Against_SRN as [SRN No] from TSPL_Mcc_Milk_Transport_Invoice_HEAD"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("PICoerFND", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ''If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) <= 0 Then
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
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
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("POTermCodeNEW", qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
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
        'txtTaxGroup.Value = clsCommon.ShowSelectForm("POFNDID", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(fndBillToLocation.Value, txtVendorNo.Value, "P", txtTaxGroup.Value, isButtonClicked)
        SetTaxDetails()
    End Sub

    Private Sub SetTax()
        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndBillToLocation.Value, txtVendorNo.Value, "P")
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
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", txtVendorNo.Value, fndBillToLocation.Value)
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
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
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
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
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
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country from TSPL_VENDOR_MASTER"
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVeFNDID", qry, "Code", " TSPL_VENDOR_MASTER.Status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"
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
                If isNewEntry Then
                    objRemittance.IsApplyTDS = True
                Else
                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                End If

                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + txtDate.Value + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"
            End If
        End If
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBillToLocation._MYValidating
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


        fndBillToLocation.Value = clsCommon.ShowSelectForm("VendRFNDster", qry, "Code", WhrCls, fndBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndBillToLocation.Value + "'"))

        SetTax()

    End Sub


    Sub SelectSRNItems()
        Dim objMRNHead As clsSRNHead = Nothing
        isInsideLoadData = True
        Dim frm As New frmPendingSRN()
        frm.VendorCode = txtVendorNo.Value
        frm.strCurrCode = txtDocNo.Value
        frm.ShowDialog()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).SRN_No) > 0 Then
                objMRNHead = clsSRNHead.GetData(frm.ArrReturn(0).SRN_No, NavigatorType.Current)
                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.SRN_No) > 0 Then
                    IsAbatementPO = objMRNHead.IsAbatementPO
                    LoadBlankGrid()
                    If clsCommon.myLen(txtRefNo.Text) <= 0 Then
                        txtRefNo.Text = objMRNHead.Ref_No
                    End If
                    If clsCommon.myLen(txtDesc.Text) <= 0 Then
                        txtDesc.Text = objMRNHead.Description
                    End If
                    'Dim InVdate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Date  from TSPL_SRN_HEAD where SRN_No='" + frm.ArrReturn(0).SRN_No + "' And Remarks='" + txtVendorInvoiceNo.Text + "'"))
                    If clsCommon.myLen(objMRNHead.Inv_Date) > 0 Then
                        DtpExpirationDate.Value = objMRNHead.Inv_Date
                        DtpExpirationDate.Checked = True
                    Else
                        DtpExpirationDate.Checked = False
                    End If
                    'If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                    fndBillToLocation.Value = objMRNHead.Bill_To_Location
                    lblBillToLocation.Text = objMRNHead.BillToLocationName
                    'End If

                    'If (clsCommon.myLen(txtVendorNo.Value) <= 0) Then
                    txtVendorNo.Value = frm.VendorCode
                    lblVendorName.Text = frm.VendorName
                    'End If
                    If (clsCommon.myLen(txtTermCode.Value) <= 0) Then
                        txtTermCode.Value = objMRNHead.Terms_Code
                        lblTermName.Text = objMRNHead.TermsName
                        txtDueDate.Value = objMRNHead.Due_Date
                    End If
                    If objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                        rbtnTaxCalAutomatic.IsChecked = True
                        chkExciseOnQty.Checked = objMRNHead.is_Excise_On_Qty
                    ElseIf objMRNHead.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                        rbtnTaxCalManual.IsChecked = True
                    End If

                    '' multicurrency
                    Me.txtCurrencyCode.Value = objMRNHead.CURRENCY_CODE
                    txtConversionRate.Text = objMRNHead.ConvRate

                    If objMRNHead.ApplicableFrom IsNot Nothing Then
                        Me.txtApplicableFrom.Text = objMRNHead.ApplicableFrom
                    End If
                    If gvAC.Rows.Count < 1 Then
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code1) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code1
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name1
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt1
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code2) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code2
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name2
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt2
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code3) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code3
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name3
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt3
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code4) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code4
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name4
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt4
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code5) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code5
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name5
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt5
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code6) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code6
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name6
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt6
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code7) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code7
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name7
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt7
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code8) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code8
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name8
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt8
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code9) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code9
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name9
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt9
                        End If
                        If (clsCommon.myLen(objMRNHead.Add_Charge_Code10) > 0) Then
                            gvAC.Rows.AddNew()
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACCode).Value = objMRNHead.Add_Charge_Code10
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACName).Value = objMRNHead.Add_Charge_Name10
                            gvAC.Rows(gvAC.Rows.Count - 1).Cells(colACAmount).Value = objMRNHead.Add_Charge_Amt10
                        End If
                    End If
                    If gvAC.Rows.Count < 1 Then
                        gvAC.Rows.AddNew()
                    End If
                End If
            End If

            For Each obj As clsSRNDetail In frm.ArrReturn
                If IsValidItem(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = obj.SRN_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = obj.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgSRNQty).Value = obj.SRN_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPendingQty).Value = obj.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colfreeQty).Value = obj.Freeqty
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
                    '' Anubhooti 21-Oct-2014 BM00000004222
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value = obj.Item_Cost
                    'If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNUnitCost).Value) > 0 Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = True
                    'Else
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).ReadOnly = False
                    'End If
                    ''
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

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = obj.Leak_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBurstQty).Value = obj.Burst_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = obj.Short_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectQty).Value = obj.Rejected_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = obj.Disc_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
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

                    If obj.Is_Mannual_Amt = 1 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMannualAmt).Value = obj.Is_Mannual_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Amount
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP
                End If
            Next
        End If
        ''For Filling Additional Charges

        If objMRNHead IsNot Nothing Then
            For Each obj As clsSRNDetail In objMRNHead.Arr
                ''If IsValidItem(obj) Then
                If clsCommon.CompairString(obj.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = obj.SRN_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeMisc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
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

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisType).Value = obj.Disc_Type
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = obj.Disc_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementRate).Value = obj.AbatementRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssesableMRP).Value = obj.AssessableMRP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAssesableMRP).Value = obj.TotalAssessableMRP
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
        If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
            For ii As Integer = 0 To gv1.RowCount - 1
                UpdateCurrentRow(ii)
            Next
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        SetVendorTDSDetails()
    End Sub

    Function IsValidItem(ByVal obj As clsSRNDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.SRNTax_Group
            SetTaxDetails()
        End If
        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.SRNTax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " SRN No: " + obj.SRN_No + "  contain Tax Group :" + obj.SRNTax_Group + Environment.NewLine)
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            ''Dim dblAssessable As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssessableRate).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.SRN_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP And dblRate = obj.Item_Cost Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "SRN No : " + obj.SRN_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
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
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = fndBillToLocation.Value
                frm.strTaxType = "P"
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
                        UpdateAllTotals()
                    End If
                End If
            ElseIf gv1.CurrentColumn Is gv1.Columns(colComplete) AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "No") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If clsMilkTransporterInvoiceMCCDetail.CompletePI(txtDocNo.Value, strICode, intSNo) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Completed", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        ''Try
        ''    If e.Column.Index >= 0 Then
        ''        If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
        ''            gv1.Columns(colExpiry).FormatString = "{0:d}"
        ''        ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
        ''            If clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
        ''                gv1.CurrentRow.Cells(colICode).ReadOnly = True
        ''                gv1.CurrentRow.Cells(colMRP).ReadOnly = True
        ''                ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
        ''            Else
        ''                gv1.CurrentRow.Cells(colICode).ReadOnly = False
        ''                gv1.CurrentRow.Cells(colMRP).ReadOnly = False
        ''                ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
        ''            End If
        ''        End If
        ''        Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
        ''        cell.GradientStyle = GradientStyles.Solid
        ''        cell.BackColor = Color.FromArgb(243, 181, 51)
        ''    End If
        ''Catch ex As Exception
        ''    common.clsCommon.MyMessageBoxShow(ex.Message)
        ''End Try

        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv1.Columns(colExpiry)) OrElse (e.Column Is gv1.Columns(colManufactureDate)) Then
                    gv1.Columns(colExpiry).FormatString = "{0:d}"
                ElseIf e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colMRP) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colSRNNo).Value) > 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                        gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                        ''gv1.CurrentRow.Cells(colAssessableRate).ReadOnly = False
                    End If

                    'ElseIf e.Column Is gv1.Columns(colQty) Then
                    '    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    '        gv1.CurrentRow.Cells(colQty).ReadOnly = False
                    '    Else
                    '        gv1.CurrentRow.Cells(colQty).ReadOnly = True
                    '    End If
                    'ElseIf e.Column Is gv1.Columns(colRate) Then

                    '    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    '        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    '    Else
                    '        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    '    End If
                ElseIf e.Column Is gv1.Columns(colAmt) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colIsMannualAmt).Value) = 1 Then
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colAmt).ReadOnly = True
                    End If

                End If
                Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                cell.GradientStyle = GradientStyles.Solid
                cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
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
            'If (frm.ObjReturn IsNot Nothing) Then
            objRemittance = frm.ObjReturn
            'End If
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
            objRemittance.Vendor_Code = txtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            '' objRemittance.Document_No = txtDocNo.SelectedValue   Should pass when saveing the data
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = "I" 'Purchase Invoice Type
            objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
            'objRemittance.Service_Type = txtDate.Value

            objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
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
    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        SelectSRNItems()
    End Sub


    Private Sub btnprintjvl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        print()

        ''       Try
        ''           '           Dim qry As String = "SELECT TSPL_PJV_HEAD.Vendor_Code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, " & _
        ''           '                     "TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, " & _
        ''           '                     "TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Add1 AS Expr1, TSPL_VENDOR_MASTER.Add2 AS Expr2, " & _
        ''           '                     "TSPL_VENDOR_MASTER.Add3 AS Expr3, TSPL_VENDOR_MASTER.City_Code_Desc, TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, " & _
        ''           '                     "TSPL_PJV_HEAD.Vendor_Name AS Expr4, TSPL_PJV_HEAD.PO_No, TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, " & _
        ''           '           "TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Invoice_No, TSPL_PJV_HEAD.Invoice_Date, TSPL_PJV_HEAD.Status, TSPL_PJV_HEAD.Posting_Date, " & _
        ''           '           "TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, " & _
        ''           '                    " TSPL_PJV_HEAD.Due_Date, TSPL_PJV_Detail.PJV_No AS Expr5, TSPL_PJV_Detail.Line_No, TSPL_PJV_Detail.GL_Account_Code, " & _
        ''           '                     "TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr6,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_PJV_HEAD.Created_By " & _
        ''           '" FROM         TSPL_PJV_HEAD INNER JOIN " & _
        ''           '                     "TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.pjv_No INNER JOIN " & _
        ''           '                     "TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
        ''           '                     "TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code   where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "'"


        ''           Dim qry As String = "  SELECT     TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.PO_No, " & _
        ''                    " TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Invoice_No, TSPL_PJV_HEAD.Invoice_Date, " & _
        ''                   "  case when TSPL_PJV_HEAD.Status=0 then 'Pending' else 'Approved'end as status, TSPL_PJV_HEAD.Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, " & _
        ''                    " TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No, " & _
        ''                     " TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as Debit , " & _
        ''                     " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3, " & _
        ''                   "  TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " & _
        ''                    " TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4, " & _
        ''                    " TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc " & _
        ''" FROM         TSPL_PJV_HEAD LEFT OUTER JOIN " & _
        ''                   "  TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN " & _
        ''                   "  TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
        ''                    " TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "' "


        ''           Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        ''           PurchaseOrderViewer.funreport(dt, "rptPJV", "PJV Report")

        ''       Catch ex As Exception

        ''       End Try
    End Sub

    Public Sub print()


        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            Exit Sub
        End If


        Try
            '           Dim qry As String = "SELECT TSPL_PJV_HEAD.Vendor_Code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, " & _
            '                     "TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, " & _
            '                     "TSPL_VENDOR_MASTER.Vendor_Name, TSPL_VENDOR_MASTER.Add1 AS Expr1, TSPL_VENDOR_MASTER.Add2 AS Expr2, " & _
            '                     "TSPL_VENDOR_MASTER.Add3 AS Expr3, TSPL_VENDOR_MASTER.City_Code_Desc, TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, " & _
            '                     "TSPL_PJV_HEAD.Vendor_Name AS Expr4, TSPL_PJV_HEAD.PO_No, TSPL_PJV_HEAD.PO_Date, TSPL_PJV_HEAD.SRN_No, " & _
            '           "TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Invoice_No, TSPL_PJV_HEAD.Invoice_Date, TSPL_PJV_HEAD.Status, TSPL_PJV_HEAD.Posting_Date, " & _
            '           "TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, " & _
            '                    " TSPL_PJV_HEAD.Due_Date, TSPL_PJV_Detail.PJV_No AS Expr5, TSPL_PJV_Detail.Line_No, TSPL_PJV_Detail.GL_Account_Code, " & _
            '                     "TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr6,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_PJV_HEAD.Created_By " & _
            '" FROM         TSPL_PJV_HEAD INNER JOIN " & _
            '                     "TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.pjv_No INNER JOIN " & _
            '                     "TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
            '                     "TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code   where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "'"


            Dim qry As String = "  SELECT    TSPL_PJV_HEAD.Modify_By,TSPL_PJV_HEAD.PJV_No, TSPL_PJV_HEAD.PJV_Date, TSPL_PJV_HEAD.Vendor_Code, TSPL_PJV_HEAD.Vendor_Name, TSPL_PJV_HEAD.Invoice_No, TSPL_SRN_HEAD.Against_PO, TSPL_PJV_HEAD.PO_No, " & _
                     " TSPL_PJV_HEAD.PO_Date, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile') as SRN_No, TSPL_PJV_HEAD.SRN_Date, TSPL_PJV_HEAD.Vendor_Invoice_No, TSPL_PJV_HEAD.Invoice_Date, " & _
                    "  case when TSPL_PJV_HEAD.Status=0 then 'Pending' else 'Approved'end as status, TSPL_PJV_HEAD.Posting_Date, TSPL_PJV_HEAD.PJV_Amount, TSPL_PJV_HEAD.PJV_TDS_Amount, " & _
                     " TSPL_PJV_HEAD.PJV_Net_Amount, TSPL_PJV_HEAD.Narration, TSPL_PJV_HEAD.Created_By, TSPL_PJV_Detail.Line_No, " & _
                      " TSPL_PJV_Detail.GL_Account_Code, TSPL_PJV_Detail.GL_Account_Desc, TSPL_PJV_Detail.PJV_Amount AS Expr1,case when TSPL_PJV_Detail.PJV_Amount<0 then -1 * TSPL_PJV_Detail.PJV_Amount else 0 end as Credit,case when TSPL_PJV_Detail.PJV_Amount>=0 then TSPL_PJV_Detail.PJV_Amount else 0 end as Debit ,TSPL_SRN_HEAD.Bill_To_Location, " & _
                      " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2, TSPL_COMPANY_MASTER.Add3, " & _
                    "  TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " & _
                     " TSPL_VENDOR_MASTER.Vendor_Name AS Expr2, TSPL_VENDOR_MASTER.Add1 AS Expr3, TSPL_VENDOR_MASTER.Add2 AS Expr4, " & _
                     " TSPL_VENDOR_MASTER.Add3 AS Expr5, TSPL_VENDOR_MASTER.City_Code_Desc,TSPL_PJV_HEAD .Due_Date,(select top 1 InvoiceDate from TSPL_Mcc_Milk_Transport_Invoice_HEAD " & _
                     " where PI_No=TSPL_PJV_HEAD.Invoice_No) as Vendor_Invoice_Date, TSPL_Mcc_Milk_Transport_Invoice_HEAD.Comments,TSPL_PJV_HEAD.Dept_Desc,TSPL_Mcc_Milk_Transport_Invoice_HEAD.TransporterDesc,TSPL_Mcc_Milk_Transport_Invoice_HEAD.VehicleDesc " & _
                    " FROM         TSPL_PJV_HEAD LEFT OUTER JOIN " & _
                    "  TSPL_PJV_Detail ON TSPL_PJV_HEAD.PJV_No = TSPL_PJV_Detail.PJV_No LEFT OUTER JOIN " & _
                     " TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PJV_HEAD.SRN_No " & _
                     "LEFT OUTER JOIN TSPL_Mcc_Milk_Transport_Invoice_HEAD  ON TSPL_Mcc_Milk_Transport_Invoice_HEAD.PI_No = TSPL_PJV_HEAD.Invoice_No " & _
                    "  LEFT OUTER JOIN  TSPL_VENDOR_MASTER ON TSPL_PJV_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code LEFT OUTER JOIN " & _
                     " TSPL_COMPANY_MASTER ON TSPL_PJV_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code " & _
                    "CROSS APPLY  (SELECT TSPL_Mcc_Milk_Transport_Invoice_Detail.SRN_Id + ',' AS [text()]   FROM TSPL_PJV_HEAD left outer join  TSPL_Mcc_Milk_Transport_Invoice_Detail on TSPL_PJV_HEAD.Invoice_No=TSPL_Mcc_Milk_Transport_Invoice_Detail.PI_No  where TSPL_Mcc_Milk_Transport_Invoice_Detail.PI_No='" & txtDocNo.Value & "' FOR XML PATH(''))el(files) " & _
                     "where TSPL_PJV_HEAD.Invoice_No='" + txtDocNo.Value + "' "
            Dim qry1 As String = "select TSPL_Mcc_Milk_Transport_Invoice_Detail.line_no,TSPL_PJV_HEAD.Created_By,TSPL_PJV_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_Code as Item_Code , " & _
            "TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_Desc as Item_Desc,TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_GL_Account_Desc as FaAccount, " & _
            "(isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail .PI_Qty,0)+ISNULL(TSPL_Mcc_Milk_Transport_Invoice_Detail .Burst_Qty ,0)+ISNULL( TSPL_Mcc_Milk_Transport_Invoice_Detail .Leak_Qty ,0)+isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.Short_Qty ,0))as  SRN_Qty, " & _
            "TSPL_Mcc_Milk_Transport_Invoice_Detail.Landed_Cost_Rate as Landed_Cost_Rate,TSPL_Mcc_Milk_Transport_Invoice_Detail.Landed_Cost_Amount as Landed_Cost_Amount , " & _
            "(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when 'O' then 'Other'when 'P' then " & _
            "'Promotional Item' else '' end) as Item_Type from TSPL_Mcc_Milk_Transport_Invoice_Detail left outer join TSPL_SRN_HEAD  on " & _
            " TSPL_SRN_HEAD .SRN_No =TSPL_Mcc_Milk_Transport_Invoice_Detail .SRN_Id left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD .SRN_No =TSPL_SRN_HEAD .SRN_No " & _
            " CROSS APPLY  (SELECT SRN_Id + ',' AS [text()] FROM TSPL_Mcc_Milk_Transport_Invoice_Detail  where TSPL_Mcc_Milk_Transport_Invoice_Detail.PI_No='" + txtDocNo.Value + "' FOR XML PATH(''))el(files)  " & _
            " where TSPL_Mcc_Milk_Transport_Invoice_Detail .Row_Type <>'Misc' and TSPL_Mcc_Milk_Transport_Invoice_Detail.PI_No   ='" + txtDocNo.Value + "' and 1=1 "
            qry1 += "union all"
            qry1 += "  select TSPL_Mcc_Milk_Transport_Invoice_Detail.line_no,TSPL_PJV_HEAD.Created_By,TSPL_PJV_HEAD.Modify_By, isnull(LEFT(el.files ,LEN(el.files )-1),'NoFile')  as srn_no,TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_Code as Item_Code , TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_Desc as Item_Desc, " & _
            "TSPL_Mcc_Milk_Transport_Invoice_Detail .Item_GL_Account_Desc as FaAccount,(isnull(TSPL_Mcc_Milk_Transport_Invoice_Detail.Free_Qty  ,0))as  SRN_Qty, '0' as Landed_Cost_Rate, " & _
            "'0' as Landed_Cost_Amount ,(case TSPL_SRN_HEAD .Item_Type  when 'F'then'Finished Goods'when 'R' then 'Raw Material' when  " & _
            "'O' then 'Other'when 'P' then 'Promotional Item' else '' end) as Item_Type from TSPL_Mcc_Milk_Transport_Invoice_Detail left outer join  " & _
            "TSPL_SRN_HEAD  on TSPL_SRN_HEAD .SRN_No =TSPL_Mcc_Milk_Transport_Invoice_Detail .SRN_Id left outer join TSPL_PJV_HEAD on  " & _
            "TSPL_PJV_HEAD .SRN_No =TSPL_SRN_HEAD .SRN_No " & _
            " CROSS APPLY  (SELECT SRN_Id + ',' AS [text()]   FROM TSPL_Mcc_Milk_Transport_Invoice_Detail  where TSPL_Mcc_Milk_Transport_Invoice_Detail .PI_No='" + txtDocNo.Value + "' FOR XML PATH(''))el(files)  " & _
            " where TSPL_Mcc_Milk_Transport_Invoice_Detail .Row_Type <>'Misc' and " & _
            "TSPL_Mcc_Milk_Transport_Invoice_Detail.PI_No   ='" + txtDocNo.Value + "' and 1=1  and Free_Qty >0 order by line_no"

            qry = "select aa.* from (" + qry + ") aa order by aa.line_no"
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreport(CrystalReportFolder.PurchaseOrder, qry, qry1, "rptPJV-V", "PJV Report", "PurchaseDetails1.rpt")
            frmCRV = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)

        'If Not IsNotIncludeWasteQtyInCal Then
        '    dblQty = dblQty + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value)
        'End If

        If is_Load_MRN Then
            dblQty = dblQty + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShortQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colBurstQty).Value) + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRejectQty).Value)
        End If
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate

        'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIsMannualAmt).Value) = 0 Then
        '    gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        'Else
        '    dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmt).Value)
        'End If

        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt
        ''Dim dblAssessableRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAssessableRate).Value)
        ''Dim dblAssessableAmt As Double = dblQty * dblAssessableRate
        ''gv1.Rows(IntRowNo).Cells(colAssessableAmount).Value = Math.Round(dblAssessableAmt, 2)
        'BlankTaxDetailsCurrentRow()

        '' abatement PO
        If IsAbatementPO Then
            gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colMRP).Value - (gv1.Rows(IntRowNo).Cells(colMRP).Value * gv1.Rows(IntRowNo).Cells(colAbatementRate).Value / 100)
            gv1.Rows(IntRowNo).Cells(colTotalAssesableMRP).Value = gv1.Rows(IntRowNo).Cells(colQty).Value * gv1.Rows(IntRowNo).Cells(colAssesableMRP).Value
        End If

        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    '' For abatement PO
                    Dim dtTax As DataTable = clsPurchaseOrderHead.GetTaxDetail(strTaxCode)
                    Dim IsExciseType As Boolean = False
                    If dtTax.Rows.Count > 0 Then
                        If clsCommon.CompairString(dtTax.Rows(0).Item("Tax Type"), "E", False) = CompairStringResult.Equal Then
                            IsExciseType = True
                        Else
                            IsExciseType = False
                        End If
                    Else
                        IsExciseType = False
                    End If
                    '' End For abatement PO
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
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        If IsExciseType And IsAbatementPO Then
                            dblBaseAmt = (gv1.Rows(IntRowNo).Cells(colTotalAssesableMRP).Value + dblOtherTaxAmt)
                        Else
                            dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                        End If
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
                End If
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
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
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If
            End If
        Next

        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
        gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim qry As String = "select TSPL_Mcc_Milk_Transport_Invoice_HEAD.PI_No,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_PJV_HEAD.PJV_No from TSPL_Mcc_Milk_Transport_Invoice_HEAD "
            qry += "  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_Mcc_Milk_Transport_Invoice_HEAD.PI_No"
            qry += "  left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code in ('AP-IN','AP-DN','PI-CM')"
            qry += "  left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD.Invoice_No=TSPL_Mcc_Milk_Transport_Invoice_HEAD.PI_No "
            qry += "  where PI_No='" + txtDocNo.Value + "' and TSPL_Mcc_Milk_Transport_Invoice_HEAD.Status=1"
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
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        For Each dr As DataRow In dt.Rows
                            ''Delete AP Journal Entry 
                            Dim docNo As String = clsCommon.myCstr(dr("Voucher_No"))
                            qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No ='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No ='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)


                            ''Delete AP Invoice
                            docNo = clsCommon.myCstr(dr("Document_No"))

                            qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No='" + docNo + "'"
                            Dim dtAP As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                qry = "AP-Invoice " + docNo + " is used in following Payment -"
                                For Each drAP As DataRow In dtAP.Rows
                                    qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                Next
                                Throw New Exception(qry)
                            End If

                            qry = "Delete from TSPL_REMITTANCE WHERE Document_No='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            ''Change status to unpost of PJV
                            docNo = clsCommon.myCstr(dr("PJV_No"))
                            qry = "update TSPL_PJV_HEAD set Status=0,Posting_Date=null where PJV_No='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            ''Change status to unpost
                            docNo = clsCommon.myCstr(dr("PI_No"))
                            qry = "update TSPL_Mcc_Milk_Transport_Invoice_HEAD set Status=0 where PI_No='" + docNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Next


                        saveCancelLog(Reason, "Reverse and Recreate", trans)
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub gv2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.Click

    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Private Function GetVehicleNo(ByVal strVehicleId As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Number from TSPL_VEHICLE_MASTER WHERE Vehicle_Id='" + strVehicleId + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutrb.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.mbtnPurchaseInvoice
        frm.ShowDialog()
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
            'SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
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
            'SendSMSandEmail(lstUsers, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpreview.Click
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)

    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseInvoice)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        obj.atchmnt = "N"
    '        If obj.atchmnt = "Y" Then

    '            'atchqry = GetAtchmentPrintQuery(txtDocNo.Value)
    '            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
    '            'If dt1.Rows.Count > 0 Then
    '            '    'SetItemWiseTax(dt1, txtDocNo.Value)
    '            '    strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptSalesOrderReport", "Sales Order")
    '            'End If
    '        End If
    '        '---------------------------------------------------------------------------



    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where Vendor_code ='" & strUser & "' ")
    '            End If

    '            strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '        Next
    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '        'Catch ex As Exception
    '        '    Throw New Exception(ex.Message)
    '        'End Try

    '        'Try

    '        If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
    '            SMSSENDONLY(False)
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SMSSENDONLY(ByVal isPost As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseInvoice)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        'strMes = "Dear  " & strCustomer & "  your order No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "has been booked with amount" & lblTotRAmt.Text

    '        Dim strMes As String = obj.smsbody
    '        strMes = strMes.Replace("'", " ").Replace("`", "/")

    '        If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.VendorName) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, "")
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        End If


    '        Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' ")

    '        If clsSMSSend.SendSMS(clsUserMgtCode.mbtnPurchaseInvoice, strMes, strphone) Then
    '            If Not isPost Then
    '                clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
    '---------------added by meenesh on 30/05/2014--------------------------

    Private Sub BtnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHistory.Click
        Dim frm As New FrmPurchaseHistory
        frm.strFormId = MyBase.Form_ID
        frm.strVendorCode = txtVendorNo.Value
        frm.strVendorName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub FndRemitTo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndRemitTo._MYValidating
        ''-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        strwherecls = MyBase.Cust_CustomerVendorMapping()
        If clsCommon.myLen(strwherecls) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Customer Found", Me.Text)
            Exit Sub
        End If
        '-----------------------------------------------------
        Dim qry As String = ""
        Dim dt As DataTable
        'qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,TSPL_CUSTOMER_MASTER.Tax_Group as [Tax Group],TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman  " & _
        '     ",TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Number ,TSPL_VENDOR_MASTER.Vendor_Code as [VSP Code],Vendor_Name as [VSP Name],VLC_Code_VLC_Uploader as [Vlc Code],VLC_Name as [VLc Name] "
        'qry += " from TSPL_CUSTOMER_MASTER "
        'qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        'qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        'qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        'qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'" & _
        '"left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No  " & _
        '"left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code left join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code"

        ''qry = "select Vendor_Code AS [Code],Vendor_Name As [Name],Add1 + ' ' + Add2 + ' ' + Add3 AS [Vendor Address],Closing_Date As [Closing Date] " & _
        ''                " ,Vendor_Group_Code As [Vendor Group Code],Vendor_Group_Code_Desc  AS [Vendor Group Code Desc] ,City_Code As [City Code],City_Code_Desc AS [City Code Desc], " & _
        ''                " State,Country ,Phone1 ,Phone2 ,Fax,Email ,WebSite,Contact_Person_Name As [Contact Person Name],Contact_Person_Email As [Contact Person Email] " & _
        ''                " ,Contact_Person_Phone As [Contact Person Phone],Contact_Person_Fax As [Contact Person Fax],Contact_Person_Website AS [Contact Person Website] " & _
        ''                " ,Terms_Code AS [Terms Code],Terms_Code_Desc As [Terms Code Desc],Vendor_Account As [Vendor Account],Vendor_Account_Desc As [Vendor Account Desc],Payment_Code As [Payment Code],Bank_Code As [Bank Code],Tax_Group As [Tax Group],Ven_Type_Code As [Ven Type Code],Status ,OnHold ,CURRENCY_CODE As [Currency Code],Form_Type As [Form Type],State_Code As [State Code],Country_Code AS [Country Code],IFSC_Code As [IFSC Code],Account_Type As [Account Type] from TSPL_VENDOR_MASTER "

        'txtVendorNo.Value = clsCommon.ShowSelectForm("MCC Customer Lists", qry, "Code", " ", txtVendorNo.Value, "Code", isButtonClicked)
        'qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"

        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
        '    txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Term Code"))
        '    lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Term Description"))
        '    txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
        '    lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group Description"))


        '    txtDate.Enabled = False
        '    txtVendorNo.Enabled = False
        '    SetMultiCurrencyVisibility()
        'Else
        '    lblVendorName.Text = ""
        '    txtTermCode.Value = ""
        '    lblTermName.Text = ""
        '    txtTaxGroup.Value = ""
        '    lblTaxGrpName.Text = ""

        '    Me.txtCurrencyCode.Value = ""
        '    Me.txtConversionRate.Text = 1
        '    Me.txtApplicableFrom.Text = ""
        'End If

        '''' priti change start here
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
        "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
        "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(FndScheduler.Value) + "'"
        Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtLocation.Rows.Count > 0 Then
            Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
            Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))

            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                qry = "select Price_Code,price_CodeNon,State,price_group_code from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"
                dt = clsDBFuncationality.GetDataTable(qry)

                txtVendorNo.Enabled = False

                If clsCommon.CompairString(clsCommon.myCstr(dtLocation.Rows(0)("State")), clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
                    txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("LocalTaxGroup"))
                    lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Local_Tax_GroupName"))
                Else
                    txtTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("InterstateTaxGroup"))
                    lblTaxGrpName.Text = clsCommon.myCstr(dtLocation.Rows(0)("Interstate_Tax_GroupName"))
                End If

            End If
        End If

        '''' priti change ends here
        SetTaxDetails()
        SetTermDetails()
    End Sub

    Private Sub FndScheduler__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndScheduler._MYValidating
        Dim qry As String = "select Doc_Code as Code,Description,DOC_DATE as Date from Tspl_Recurring_Scheduler_Head "
        Dim WhrCls As String = "  "


        FndScheduler.Value = clsCommon.ShowSelectForm("SchedulerFND", qry, "Code", WhrCls, FndScheduler.Value, "Code", isButtonClicked)
        LblScheduler.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from Tspl_Recurring_Scheduler_Head where Doc_Code='" + FndScheduler.Value + "'"))

    End Sub

    Private Sub CboExpiration_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CboExpiration.SelectedIndexChanged
        Try
            If CboExpiration.Text = "Specific Date" Then
                DtpExpirationDate.Visible = True
                TxtExpirationAmount.Visible = False
                'txtCurrentCount.Visible=true
            ElseIf CboExpiration.Text = "Specific Date" Then
                DtpExpirationDate.Visible = False
                TxtExpirationAmount.Visible = True
            Else
                DtpExpirationDate.Visible = False
                TxtExpirationAmount.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub CboExpiration_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboExpiration.SelectedValueChanged
        Try
            If CboExpiration.Text = "Specific Date" Then
                DtpExpirationDate.Visible = True
                TxtExpirationAmount.Visible = False
                'txtCurrentCount.Visible=true
            ElseIf CboExpiration.Text = "Specific Date" Then
                DtpExpirationDate.Visible = False
                TxtExpirationAmount.Visible = True
            Else
                DtpExpirationDate.Visible = False
                TxtExpirationAmount.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        'If clsCommon.myLen(fndReqNo.Value) <= 0 Then
        If clsCommon.myLen(fndBillToLocation.Value) = 0 Then

            common.clsCommon.MyMessageBoxShow("Select the from location", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0

        Else
            'Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
            Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", False, isButtonClick, "", "", "Product_Type not in ('MI') and Item_Type not in ('A')  ")
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code

                Dim srtcost As String = "select  convert(decimal(18,2),sum(case when  Item_Qty =0 or Amount=0 then 0 else  (Amount/Item_Qty )end)) as cost  from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + obj.Item_Code + "' and location_code='" + fndBillToLocation.Value + "'  "
                Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(srtcost))
                ''COMMENTED BY PRITI
                'If cost <= 0 Then
                '    common.clsCommon.MyMessageBoxShow(" '" + obj.Item_Code + "' item don't have unit cost")
                'End If
                gv1.CurrentRow.Cells(colRate).Value = clsEkoPro.GetRateMccSale(fndBillToLocation.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), txtDate.Value) 'cost
                ' gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
            Else
                gv1.CurrentRow.Cells(colICode).Value = ""
                gv1.CurrentRow.Cells(colIName).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colRate).Value = 0
            End If

            SetitemWiseTaxSetting(True, True)
        End If
        'Else

        'SetitemWiseTaxSetting(True, True)
        'End If
    End Sub

End Class
