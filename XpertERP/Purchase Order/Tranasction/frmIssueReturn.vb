Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmIssueReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim AutoPurchaseReturn As Boolean = False
    Dim ZeroCostForReprocess As Boolean = False
    Dim RunBatchFifowise As Integer = 0
    Dim EnableRackBin As Integer = 0
    Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqIssueNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoCCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoReturnQty As GridViewDecimalColumn = New GridViewDecimalColumn()

    Const colLineNo As String = "COLLNO"
    Const colReq_IssueNo As String = "Req_IssueNo"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colCCCode As String = "COLCCCode"
    Const colCCDesc As String = "COLCCDesc"
    Const colQty As String = "COLQTY"
    Const colRetQty As String = "COLRetQTY"
    Const colUnit As String = "COLUNIT"
    Const colReqQty As String = "COLREQQTY"
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colHierarchyLevel2 As String = "colHierarchyLevel2"
    Const colHierarchyLevel3 As String = "colHierarchyLevel3"
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
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

    Const colRack As String = "colRack"
    Const colBin As String = "colBin"

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
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"

    ''==================for purchase invoice case============
    Const colSRNID As String = "SRNID"
    Const colMRNID As String = "MRNID"
    Const colGRNID As String = "GRNID"
    Const colPOID As String = "POID"
    ''=============================================================

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Const colPINo As String = "colPurchaseInvoiceNo"
    Const colPIPendingQty As String = "colPIPendingQty"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public DocumentNo As String = Nothing
    Dim CostEditOnIRT As Boolean
    Dim StoreRequisitionMandatory As Boolean
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
    Dim ChkAllowWithoutUnitCostEntry As Boolean = False
    Dim ShowCostCenterAndHierarchyLevelInPurchaseModule As Boolean = False
    Dim EnableHirerachyCostCentre As Double = 0
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim EnableStoreCostCentre As Double = 0
    Dim ShowDefaultUser As Boolean = False
    Dim AllowOnlyOneIssueAgainstStoreRequisition As Boolean = False
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False
#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnIssueReturn)
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
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        '' add setting by parteek on 21/11/2017 for item wise rack bin
        EnableRackBin = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableRackBin, clsFixedParameterCode.EnableRackBin, Nothing))
        '' End Setting
        '====Sanjeet (For Cost Centre Hiererachy level)====
        EnableHirerachyCostCentre = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableHirerachyCostCentre, clsFixedParameterCode.EnableHirerachyCostCentre, Nothing))
        EnableStoreCostCentre = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableStoreCostCentre, clsFixedParameterCode.EnableStoreCostCentre, Nothing))
        ShowDefaultUser = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowDefaultUser, clsFixedParameterCode.ShowDefaultUser, Nothing)) = 1, True, False)
        '========End=================
        StoreRequisitionMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StoreRequisitionMandatoryforstorerequest, clsFixedParameterCode.StoreRequisitionMandatoryforstorerequest, Nothing)) = 1, True, False)
        ''================================================================================================
        AutoPurchaseReturn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoPurchaseReturnFromIssueReturn, clsFixedParameterCode.AutoPurchaseReturnFromIssueReturn, Nothing)) = 1, True, False)

        '' added setting for Zero Cost For Reprocess on 09/02/2018
        ZeroCostForReprocess = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ZeroCostForReprocess, clsFixedParameterCode.ZeroCostForReprocess, Nothing)) = 1, True, False)
        '' End Process
        AllowOnlyOneIssueAgainstStoreRequisition = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOnlyOneIssueAgainstStoreRequisition, clsFixedParameterCode.AllowOnlyOneIssueAgainstStoreRequisition, Nothing)) = 1, True, False)
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)

        MyLabel2.Visible = AutoPurchaseReturn
        fndPurchaseInvoice.Visible = AutoPurchaseReturn
        chkAgnstPI.Visible = AutoPurchaseReturn
        ''================================================================================================

        If StoreRequisitionMandatory AndAlso chkReProcess.Checked = False Then
            fndReqNo.MendatroryField = True
        Else
            fndReqNo.MendatroryField = False
        End If
        If EnableStoreCostCentre = 1 Then
            pnlUnit_CostType.Visible = True
            txtCostCenterType.MendatroryField = False
            txtUnitCode.MendatroryField = False
        Else
            pnlUnit_CostType.Visible = False
        End If
       
        ShowCostCenterAndHierarchyLevelInPurchaseModule = IIf(clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "0") = CompairStringResult.Equal, True, False)
        chk_againstmonthend.Visible = False
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        'ButtonToolTip.SetToolTip(btnRequistionItems, "Press Ctrl+F7 for Select Purchase Requistion Items")
        RadPageView1.SelectedPage = RadPageViewPage1

        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        If ShowCapexCodeandSubCode Then
            SplitContainer2.Panel1Collapsed = False
        Else
            SplitContainer2.Panel1Collapsed = True
        End If

        LoadBlankGrid()
        LoadDocType()
        AddNew(True)
        SetLength()
        CostEditOnIRT = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsCostEditableOnIssueReturnTransfer, clsFixedParameterCode.IsCostEditableOnIssueReturnTransfer, Nothing)) = 1, True, False)
        txtFromLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "'"))
        If clsCommon.myLen(txtFromLocation.Value) > 0 Then
            lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtFromLocation.Value + "' "))
        End If
        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If

        ''End of For Custom Fields

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        ChkAllowWithoutUnitCostEntry = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowWithoutUnitCostIssueReturnEntry, clsFixedParameterCode.AllowWithoutUnitCostIssueReturnEntry, Nothing)) = "1", True, False)
        btncancel.Enabled = False
    End Sub

    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDepartment.Enabled = False
            txtDepartment.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            ' Ticket No : UDL/22/05/18-000172 By Prabhakar
            lblDepartment.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDepartment.Value + "'"))
        Else
            txtDepartment.Enabled = True
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtComment.MaxLength = 200

    End Sub

    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Issue"
        dr("Name") = "Issue"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Return"
        dr("Name") = "Return"
        dt.Rows.Add(dr)
        If ShowCapexCodeandSubCode Then
            dr = dt.NewRow()
            dr("Code") = "TransferCX"
            dr("Name") = "Transfer Store to Capex"
            dt.Rows.Add(dr)
        End If


        ''richa agarwal
        'dr = dt.NewRow()
        'dr("Code") = "Transfer"
        'dr("Name") = "Transfer"
        'dt.Rows.Add(dr)
        '-----------------

        'dr = dt.NewRow()
        'dr("Code") = "Sale"
        'dr("Name") = "Sale"
        'dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls(ByVal ChangeDocType As Boolean)
        fndPurchaseInvoice.Value = ""
        fndPurchaseInvoice.Enabled = True
        fndPurchaseInvoice.MendatroryField = False
        chkAgnstPI.Checked = False
        chkAgnstPI.Enabled = True
        txtVendor.Enabled = True
        txtToLocation.Enabled = True
        txtFromLocation.Enabled = True

        chkReject.Enabled = True
        txtDocNo.Value = ""
        txtComment.Text = ""
        chkOnHold.Checked = False
        txtIssueTo.Value = ""
        lblIssueTo.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtComment.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        If ChangeDocType Then
            cboDocType.SelectedValue = "Issue"
        End If
        MyLabel2.Visible = False
        fndPurchaseInvoice.Visible = False
        chkAgnstPI.Visible = False
        cboDocType.Enabled = True
        txtRequestBy.Enabled = True
        txtRequestBy.Value = ""
        lblRequestBy.Text = ""
        txtFromLocation.Value = ""
        lblFromLocation.Text = ""
        txtToLocation.Value = ""
        lblToLocation.Text = ""
        'txtDepartment.Value = ""
        'lblDepartment.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        lblDocAmount.Text = ""
        TxtVehicle.Value = ""
        'TxtMachinery.Value = ""
        lblVehicleDesc.Text = ""
        'lblMachineDesc.Text = ""
        txtTaxGroup.Enabled = False
        lblTaxGrpName.Enabled = False
        fndReqNo.Value = ""
        lblReqDate.Text = ""

        'added by priti
        fndReqNo.Enabled = True
        txtFromLocation.Enabled = True
        txtToLocation.Enabled = True
        cboDocType.Enabled = True
        fndReqNo.Visible = True
        lblReqDate.Visible = True
        lblReq.Visible = True
        lblReq.Text = "Requisition No"

        chkReProcess.Checked = False
        chkReProcess.Enabled = True
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            chkReProcess.Visible = True
        Else
            chkReProcess.Visible = False
        End If
        ' ended by priti
        txtDepartment.Value = ""
        lblDepartment.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()


        '====CLEAR CAPEX DETAIL=====
        lbl_capexcode.Text = ""
        lbl_capexsubcode.Text = ""
        fndcapexcode.Value = ""
        fndcapexsubcode.Value = ""
        lbl_budgetamt.Text = ""
        lbl_budgetamtwithtolerence.Text = ""
        lbl_rebudgetamt.Text = ""
        lbl_rebudgetamtwithtolerence.Text = ""
        '===========================
        txtUnitCode.Value = ""
        lblUnitDesc.Text = ""
        txtCostCenterType.Value = ""
        lblCostcenterTypeDesc.Text = ""
        btncancel.Enabled = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "Req. / Issue No"
        repoReqIssueNo.Name = colReq_IssueNo
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 200
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 300
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        ' '==========added by richa agarwal 1 Dec,2016 BM00000010390

        Dim CostCenter As String = clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)
        If CostCenter = "1" Then
            Dim repoHierarchyCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()

            repoHierarchyCode.FormatString = ""
            repoHierarchyCode.HeaderText = "Hierarchy Level"
            repoHierarchyCode.Name = colHierarchyCode
            repoHierarchyCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoHierarchyCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoHierarchyCode.Width = 150
            gv1.MasterTemplate.Columns.Add(repoHierarchyCode)

            Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCostCenterCode.FormatString = ""
            repoCostCenterCode.HeaderText = "Cost Center"
            repoCostCenterCode.Name = colCostCenterCode
            repoCostCenterCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoCostCenterCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoCostCenterCode.Width = 150
            gv1.MasterTemplate.Columns.Add(repoCostCenterCode)
            If EnableHirerachyCostCentre = 1 Then

                Dim repoHierarchyLevel2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoHierarchyLevel2.FormatString = ""
                repoHierarchyLevel2.HeaderText = "Hierarchy Level2"
                repoHierarchyLevel2.Name = colHierarchyLevel2
                repoHierarchyLevel2.HeaderImage = Global.ERP.My.Resources.Resources.search4
                repoHierarchyLevel2.TextImageRelation = TextImageRelation.TextBeforeImage
                repoHierarchyLevel2.Width = 150
                gv1.MasterTemplate.Columns.Add(repoHierarchyLevel2)

                Dim repoHierarchyLevel3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoHierarchyLevel3.FormatString = ""
                repoHierarchyLevel3.HeaderText = "Hierarchy Level3"
                repoHierarchyLevel3.Name = colHierarchyLevel3
                repoHierarchyLevel3.HeaderImage = Global.ERP.My.Resources.Resources.search4
                repoHierarchyLevel3.TextImageRelation = TextImageRelation.TextBeforeImage
                repoHierarchyLevel3.Width = 150
                gv1.MasterTemplate.Columns.Add(repoHierarchyLevel3)

            End If
        Else
            repoCCCode.HeaderText = "Cost Center"
            repoCCCode.Name = colCCCode
            repoCCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoCCCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoCCCode.Width = 100
            repoCCCode.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoCCCode)

            repoCCDesc.FormatString = ""
            repoCCDesc.HeaderText = "Description"
            repoCCDesc.Name = colCCDesc
            repoCCDesc.Width = 150
            repoCCDesc.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoCCDesc)
        End If


        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        repoReqQty = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Required Quantity"
        repoReqQty.Name = colReqQty
        repoReqQty.Width = 100
        repoReqQty.Minimum = 0
        repoReqQty.ReadOnly = True
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReqQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)



        repoReturnQty = New GridViewDecimalColumn()
        repoReturnQty.FormatString = ""
        repoReturnQty.HeaderText = "Return Quantity"
        repoReturnQty.Name = colRetQty
        repoReturnQty.Width = 100
        repoReturnQty.Minimum = 0
        repoReturnQty.IsVisible = False
        repoReturnQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnQty)




        ''========================================================================
        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "PI No."
        repoReqIssueNo.Name = colPINo
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        repoReqIssueNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoReturnQty = New GridViewDecimalColumn()
        repoReturnQty.FormatString = ""
        repoReturnQty.HeaderText = "PI Pending Qty"
        repoReturnQty.Name = colPIPendingQty
        repoReturnQty.Width = 100
        repoReturnQty.Minimum = 0
        repoReturnQty.IsVisible = False
        repoReturnQty.ReadOnly = True
        repoReturnQty.VisibleInColumnChooser = False
        repoReturnQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnQty)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "SRN No."
        repoReqIssueNo.Name = colSRNID
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        repoReqIssueNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "MRN No."
        repoReqIssueNo.Name = colMRNID
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        repoReqIssueNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "GRN No."
        repoReqIssueNo.Name = colGRNID
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        repoReqIssueNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)

        repoReqIssueNo = New GridViewTextBoxColumn()
        repoReqIssueNo.FormatString = ""
        repoReqIssueNo.HeaderText = "PO No."
        repoReqIssueNo.Name = colPOID
        repoReqIssueNo.Width = 100
        repoReqIssueNo.ReadOnly = True
        repoReqIssueNo.IsVisible = False
        repoReqIssueNo.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(repoReqIssueNo)
        ''====================================================================

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        'repoRate.ReadOnly = True
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

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
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
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


        Dim RackBin As String = clsFixedParameter.GetData(clsFixedParameterType.EnableRackBin, clsFixedParameterCode.EnableRackBin, Nothing)
        If EnableRackBin = "1" Then
            Dim repoRack As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoRack.FormatString = ""
            repoRack.HeaderText = "Rack No"
            repoRack.Name = colRack
            repoRack.ReadOnly = True
            repoRack.Width = 100
            gv1.MasterTemplate.Columns.Add(repoRack)

            Dim repoBin As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoBin.FormatString = ""
            repoBin.HeaderText = "Bin No"
            repoBin.Name = colBin
            repoBin.ReadOnly = True
            repoBin.Width = 100
            gv1.MasterTemplate.Columns.Add(repoBin)
        End If

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
    End Sub

    Sub OpenSerialItem()
        If Not clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                    Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = txtFromLocation.Value
                    'frm.strCurrDocNo = txtDocNo.Value
                    'frm.strCurrDocType = "ISSTRAN"

                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Tag = frm.arr
                    End If
                Else
                    Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = txtFromLocation.Value
                    frm.strCurrDocNo = txtDocNo.Value
                    frm.strCurrDocType = "ISSTRAN"
                    frm.strAgaintsDocNo = fndReqNo.Value
                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Tag = frm.arr
                    End If
                End If
            End If
        End If
    End Sub

    Sub OpenBatchItem()
        If Not clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
            If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = clsCommon.myCstr(txtFromLocation.Value)
                    frm.strCurrDocNo = clsCommon.myCstr(txtDocNo.Value)
                    frm.strCurrDocType = "ISSTRAN"
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                    frm.dblMRP = 0
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
                Else
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = clsCommon.myCstr(txtToLocation.Value)
                    frm.strCurrDocNo = clsCommon.myCstr(txtDocNo.Value)
                    frm.strAgaintsDocNo = fndReqNo.Value
                    frm.strSplTransaction = "ReturnAgainstIssue"
                    frm.strCurrDocType = "ISSTRAN"
                    Dim Arrlst As New ArrayList()
                    Arrlst.Add(clsCommon.myCstr(gv1.CurrentRow.Tag))

                    frm.ArrTransferNo = TryCast(Arrlst, ArrayList)
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                    frm.dblMRP = 0 ' clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colMRP).Value)
                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetQty).Value)


                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                    End If
                End If
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
                    If e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colRetQty) Then
                        If gv1.CurrentColumn Is gv1.Columns(colQty) Then
                            Dim stockqty As Double = 0
                            Dim ActualQty As Double = 0
                            If clsCommon.myLen(txtFromLocation.Value) <> 0 And clsCommon.myLen(clsCommon.myCdbl(gv1.CurrentRow.Cells(colICode).Value)) <> 0 Then
                                Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + txtFromLocation.Value + "' "
                                stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                Dim item As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                'If stockqty = 0 Then
                                '    common.clsCommon.MyMessageBoxShow("Stock Qty  not available at this location ")
                                '    gv1.CurrentRow.Cells(colQty).Value = 0
                                'Else
                                If clsCommon.myLen(fndReqNo.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal And clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
                                    str = "SELECT Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_IssueReturn_HEAD inner join " &
                                    "TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No and " &
                                    "TSPL_IssueReturn_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id and TSPL_REQUISITION_DETAIL.item_code=TSPL_IssueReturn_DETAIL.item_code where Doc_Type='Issue' and " &
                                    "TSPL_IssueReturn_HEAD.Doc_No <> '" & txtDocNo.Value & "' ) +   (select isnull(SUM(Issued_Qty),0) from TSPL_IssueReturn_HEAD inner join " &
                                    "TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No and " &
                                    "TSPL_IssueReturn_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id and TSPL_REQUISITION_DETAIL.item_code=TSPL_IssueReturn_DETAIL.item_code where Doc_Type='Return' ) FROM " &
                                    "TSPL_REQUISITION_DETAIL where TSPL_REQUISITION_DETAIL.item_code='" + item + "' and Requisition_Id='" + fndReqNo.Value + "' "
                                    Dim ReqQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                                    ActualQty = ReqQty - clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                                    If ReqQty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                        common.clsCommon.MyMessageBoxShow("Qty more then Req qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(ReqQty) + "' ")
                                        gv1.CurrentRow.Cells(colQty).Value = 0
                                    End If
                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                Else
                                    'If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                    '    common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                                    '    gv1.CurrentRow.Cells(colQty).Value = 0
                                    'End If
                                End If

                                'End If
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Select the Location", Me.Text)
                                gv1.CurrentRow.Cells(colQty).Value = 0
                            End If
                        End If

                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                            If (e.Column Is gv1.Columns(colQty)) Then
                                OpenSerialItem()
                                OpenBatchItem()
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                            If (e.Column Is gv1.Columns(colRetQty)) Then
                                OpenSerialItem()
                                OpenBatchItem()
                            End If
                        Else
                            If (e.Column Is gv1.Columns(colQty)) Then
                                OpenSerialItem()
                                OpenBatchItem()
                            End If
                        End If




                        If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colReqQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colRetQty) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            OpenBatchItem()
                        End If




                        ''richa agarwal 10/04/2015 against ticket no.BM00000006161
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                        ''----------------------
                        ' setGridFocus()
                    End If
                    ''richa agarwal 1 Dec,2016
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        If e.Column Is gv1.Columns(colHierarchyCode) Then
                            If EnableHirerachyCostCentre = 1 Then
                                If e.RowIndex = 0 Then
                                    OpenHierarchyCode(False)
                                Else
                                    Dim qry As String = Nothing
                                    qry = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER "
                                    gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE='" + clsCommon.myCstr(gv1.Rows(e.RowIndex - 1).Cells(colHierarchyCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", False)
                                    OpenCostCentreLevelName(False)
                                    OpenDefaultCostCentreLevel1(False)
                                    OpenDefaultCostCentreLevel2(False)
                                    OpenDefaultCostCentreLevel3(False)
                                End If
                            Else
                                OpenHierarchyCode(False)
                            End If
                        End If
                        If e.Column Is gv1.Columns(colCostCenterCode) Then
                            OpenCostCenterCode(False)
                        End If
                        If e.Column Is gv1.Columns(colHierarchyLevel2) Then
                            OpenCostCentreLevel2(False)
                        End If
                        If e.Column Is gv1.Columns(colHierarchyLevel3) Then
                            OpenCostCentreLevel3(False)
                        End If

                    Else
                        If e.Column Is gv1.Columns(colCCCode) Then
                            OpenCCList(False)
                        End If
                    End If
                    ''-----------------

                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("IRTItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                        gv1.CurrentColumn = gv1.Columns(colQty)
                    Else
                        gv1.CurrentColumn = gv1.Columns(colReqQty)
                    End If

                ElseIf gv1.CurrentColumn Is gv1.Columns(colReqQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) AndAlso clsCommon.myLen(fndReqNo.Value) <= 0 Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub setCost(ByVal index As Integer)
        Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(gv1.Rows(index).Cells(colICode).Value) & "' "))
        If dblCostMethod <> 0 Then
            gv1.Rows(index).Cells(colRate).Value = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(gv1.Rows(index).Cells(colICode).Value), txtFromLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(gv1.Rows(index).Cells(colUnit).Value))
        Else
            gv1.Rows(index).Cells(colRate).Value = 0
        End If
    End Sub


    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
            If clsCommon.myLen(txtFromLocation.Value) = 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") <> CompairStringResult.Equal Then

                common.clsCommon.MyMessageBoxShow(Me, "Select from location", Me.Text)
                gv1.CurrentRow.Cells(colICode).Value = ""
                gv1.CurrentRow.Cells(colIName).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colRate).Value = 0
                gv1.CurrentRow.Cells(colIsBatchItem).Value = False
            Else
                If chkReProcess.Checked Then
                    ''then open finish item
                    gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(clsItemMaster.getFinder(" tspl_Item_master.item_type='F' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick))
                    gv1.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
                    gv1.CurrentRow.Cells(colUnit).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), Nothing)
                    setCost(gv1.CurrentRow.Index)
                    gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(gv1.CurrentRow.Cells(colICode).Value)
                    gv1.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(gv1.CurrentRow.Cells(colICode).Value)
                Else
                    Dim obj As ClsScrapSaleDetail
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal AndAlso chk_againstmonthend.Checked Then
                        obj = ClsScrapSaleDetail.FinderItemwithBalanceQtyLocationWise(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
                    Else
                        obj = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
                    End If
                    ' Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                        gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal AndAlso chk_againstmonthend.Checked Then
                            gv1.CurrentRow.Cells(colRetQty).Value = obj.balance_Qty
                            ' gv1.CurrentRow.Cells(colQty).Value = obj.balance_Qty
                        End If
                        'Dim srtcost As String = "select  convert(decimal(18,2),sum(case when  Item_Qty =0 or Amount=0 then 0 else  (Amount/Item_Qty )end)) as cost  from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + obj.Item_Code + "' and location_code='" + txtFromLocation.Value + "'  "
                        'Dim cost As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(srtcost))
                        '' Anubhooti 28-Jan-2015 (Costing Method Avg/FIFO/LIFO)
                        setCost(gv1.CurrentRow.Index)
                        ''COMMENTED BY PRITI
                        'If cost <= 0 Then
                        '    common.clsCommon.MyMessageBoxShow(" '" + obj.Item_Code + "' item don't have unit cost")
                        'End If

                        'gv1.CurrentRow.Cells(colRate).Value = dblUnitCost
                        gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                        gv1.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code)

                        '' add function related to Rack/Bin on 21/11/2017 by Parteek
                        If EnableRackBin Then
                            gv1.CurrentRow.Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtFromLocation.Value & "'")
                            gv1.CurrentRow.Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtFromLocation.Value & "'")
                        End If

                        '' End function
                    Else
                        gv1.CurrentRow.Cells(colICode).Value = ""
                        gv1.CurrentRow.Cells(colIName).Value = ""
                        gv1.CurrentRow.Cells(colUnit).Value = ""
                        gv1.CurrentRow.Cells(colRate).Value = 0
                        gv1.CurrentRow.Cells(colIsBatchItem).Value = False
                    End If
                End If
                SetitemWiseTaxSetting(True, True)
            End If
        ElseIf (clsCommon.myLen(gv1.CurrentRow.Cells(colReq_IssueNo).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal AndAlso AllowOnlyOneIssueAgainstStoreRequisition = True) Then
            If clsCommon.myLen(txtFromLocation.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select from location", Me.Text)
                gv1.CurrentRow.Cells(colICode).Value = ""
                gv1.CurrentRow.Cells(colIName).Value = ""
                gv1.CurrentRow.Cells(colUnit).Value = ""
                gv1.CurrentRow.Cells(colRate).Value = 0
                gv1.CurrentRow.Cells(colIsBatchItem).Value = False
            Else
                Dim obj As ClsScrapSaleDetail
                obj = ClsScrapSaleDetail.FinderItemwithBalanceQty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", isButtonClick, txtFromLocation.Value)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                    gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
                    gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
                    gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code

                    setCost(gv1.CurrentRow.Index)
                    gv1.CurrentRow.Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                    gv1.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code)

                    If EnableRackBin Then
                        gv1.CurrentRow.Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtFromLocation.Value & "'")
                        gv1.CurrentRow.Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & obj.Item_Code & "' and Location='" & txtFromLocation.Value & "'")
                    End If
                Else
                    gv1.CurrentRow.Cells(colICode).Value = ""
                    gv1.CurrentRow.Cells(colIName).Value = ""
                    gv1.CurrentRow.Cells(colUnit).Value = ""
                    gv1.CurrentRow.Cells(colRate).Value = 0
                    gv1.CurrentRow.Cells(colIsBatchItem).Value = False
                End If
                SetitemWiseTaxSetting(True, True)
            End If
        Else

            SetitemWiseTaxSetting(True, True)
        End If
        setBalance()
    End Sub


    Private Sub OpenCCList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value), "", isButtonClick)
        gv1.CurrentRow.Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCCCode).Value)
    End Sub
    ''richa agarwal 1 Dec,2016
    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            Dim qry As String = Nothing
            qry = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
            OpenCostCentreLevelName(False)
            OpenDefaultCostCentreLevel1(False)
            OpenDefaultCostCentreLevel2(False)
            OpenDefaultCostCentreLevel3(False)
        Else
            Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub OpenCostCenterCode(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", " HIRERACHY_LEVEL = '" + DBLevel + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "Code", isButtonClick)
                OpenDefaultCostCentreLevel2(False)
                OpenDefaultCostCentreLevel3(False)
            Else
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", " Hirerachy_Level = '" + DBLevel + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "Code", isButtonClick)
            End If
        Else
            ' clsCommon.MyMessageBoxShow("Please select hirerachy level first.")
        End If
    End Sub
    ''--------
    '========Sanjeet(For Hirerachy Level)======
    Private Sub OpenCostCentreLevel2(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "' "))
                Dim QRY As String = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                    " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                    " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                    " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code "
                gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.ShowSelectForm("Hierarchy", QRY, "Code", " MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value), "Code", isButtonClick)
                OpenDefaultCostCentreLevel3(False)
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select " + clsCommon.myCstr(gv1.Columns(colHierarchyLevel2).HeaderText) + " first.")
        End If
    End Sub

    Private Sub OpenCostCentreLevel3(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel3).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "' "))
                Dim QRY As String = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                    " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                    " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                    " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code "
                gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.ShowSelectForm("Hierarchy", QRY, "Code", "  MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel3).Value), "Code", isButtonClick)
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select " + clsCommon.myCstr(gv1.Columns(colHierarchyLevel3).HeaderText) + " first.")
        End If
    End Sub

    Private Sub OpenDefaultCostCentreLevel1(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colHierarchyCode))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                qry = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL where HIRERACHY_LEVEL='" + DBLevel + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub OpenDefaultCostCentreLevel2(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colCostCenterCode))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "' "))
                qry = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code WHERE MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub OpenDefaultCostCentreLevel3(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colHierarchyLevel2))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "' "))
                qry = "select distinct MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code where  MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub OpenCostCentreLevelName(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            Dim DBLevel As String = String.Empty
            DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
            If clsCommon.CompairString(DBLevel, 4) = CompairStringResult.Equal Then
                gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colHierarchyLevel2).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 1) + "'"))
                gv1.Columns(colHierarchyLevel3).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 2) + "'"))
                'gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
            ElseIf clsCommon.CompairString(DBLevel, 3) = CompairStringResult.Equal Then
                gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colHierarchyLevel2).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 1) + "'"))
                gv1.Columns(colHierarchyLevel3).HeaderText = "Hiererachy Level3"
                'gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
                gv1.Columns(colHierarchyLevel3).ReadOnly = True
            Else
                'gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colCostCenterCode).HeaderText = "Hiererachy Level1"
                gv1.Columns(colHierarchyLevel2).HeaderText = "Hiererachy Level2"
                gv1.Columns(colHierarchyLevel3).HeaderText = "Hiererachy Level3"
                gv1.Columns(colHierarchyLevel2).ReadOnly = True
                gv1.Columns(colHierarchyLevel3).ReadOnly = True
            End If
        End If
    End Sub
    '====================
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
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblDocAmount.Text = lblTotRAmt.Text
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew(True)
    End Sub


    Sub AddNew(ByVal ChangeDocType As Boolean)
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        lblReq2.Visible = False
        lblReq3.Visible = False
        BlankAllControls(ChangeDocType)
        LoadBlankGrid()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        chk_againstmonthend.Checked = False
        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
            gv1.Rows.AddNew()
        End If
        chkWithoutRefNo.Checked = False
        chkWithoutRefNo.Enabled = False
        chkWithoutRefNoChanged()
        ''richa agarwal 11/11/2014
        lblToLocation.Visible = False
        txtToLocation.Visible = False
        RadLabel9.Visible = False
        txtFromLocation.Enabled = True
        lblReq3.Text = ""
        ''------------------
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        txtVendor.Value = ""
        lblVendor.Text = ""
        chkReject.Visible = False
        chkReject.Checked = False
        setRejectVisiblity()
        chkSkipIndentBalance.Checked = False

        If ShowDefaultUser Then
            txtRequestBy.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_code from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "' "))
            lblRequestBy.Text = clsEmployeeMaster.GetName(txtRequestBy.Value, Nothing)
            txtRequestBy.Enabled = False
            txtDepartment.Enabled = False
            txtDepartment.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_Code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            lblDepartment.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER WHERE DEPARTMENT_CODE='" + txtDepartment.Value + "'"))

        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
            txtRequestBy.Enabled = True
            txtDepartment.Enabled = True
            txtDepartment.Value = ""
            lblDepartment.Text = ""
        End If
        txtUnitCode.Enabled = True
        txtCostCenterType.Enabled = True
        'txtDepartment.Enabled = True
        If ShowDefaultUser = False Then
            AllowDepartmentMandatoryOnPurchaseCycle()
        End If
    End Sub

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

    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_IssueReturn_HEAD where Doc_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                End If
            End If
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                        setCost(gv1.CurrentRow.Index)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                        gv1.Rows(ii).Cells(colRate).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Unit_Cost,0) as Rate from TSPL_IssueReturn_DETAIL where Doc_No='" + fndReqNo.Value + "' and Item_Code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' and Unit_code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value) + "'"))
                    End If
                    UpdateCurrentRow(ii)
                End If
            Next

            UpdateAllTotals()

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                    txtFromLocation.Focus()
                    Throw New Exception("Please Enter From Location")
                End If
                If StoreRequisitionMandatory AndAlso chkReProcess.Checked = False Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 Then
                        fndReqNo.Focus()
                        Throw New Exception("Please select Requisition No.")
                    End If
                End If
                '=====Sanjeet(19/02/2018)==Mandetory while cost center is enable====
                'If EnableStoreCostCentre = 1 Then
                '    If clsCommon.myLen(Me.txtDepartment.Value) <= 0 Then
                '        txtDepartment.Focus()
                '        Throw New Exception("Please select Department")
                '    End If
                '    If clsCommon.myLen(Me.txtUnitCode.Value) <= 0 Then
                '        txtUnitCode.Focus()
                '        Throw New Exception("Please select Select Cost Center Unit Code")
                '    End If
                '    If clsCommon.myLen(Me.txtCostCenterType.Value) <= 0 Then
                '        txtCostCenterType.Focus()
                '        Throw New Exception("Please select Cost Center Type Code")
                '    End If
                'End If
                ''=================
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtToLocation.Value) <= 0 Then
                    txtToLocation.Focus()
                    Throw New Exception("Please Enter To Location")
                End If
                If StoreRequisitionMandatory AndAlso chkReProcess.Checked = False Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 Then
                        fndReqNo.Focus()
                        Throw New Exception("Please select Requisition No.")
                    End If
                End If
                If AutoPurchaseReturn AndAlso chkAgnstPI.Checked AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 AndAlso chk_againstmonthend.Checked = False Then
                    fndPurchaseInvoice.Focus()
                    fndPurchaseInvoice.Select()
                    Throw New Exception("Please select purchase invoice no.")
                End If
                If clsCommon.CompairString(cboDocType.SelectedValue, "Return") = CompairStringResult.Equal AndAlso chk_againstmonthend.Checked = True Then
                    If clsCommon.myLen(txtFromLocation.Value) = 0 Then
                        fndReqNo.Focus()
                        Throw New Exception("Please Select From Location")
                    End If
                Else
                    If clsCommon.myLen(fndReqNo.Value) <= 0 AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 Then
                        fndReqNo.Focus()
                        Throw New Exception("Please Select Issue No")
                    End If
                End If
                '=====Sanjeet(19/02/2018)==Mandetory while cost center is enable====
                If EnableStoreCostCentre = 1 Then
                    If clsCommon.myLen(Me.txtDepartment.Value) <= 0 Then
                        txtDepartment.Focus()
                        Throw New Exception("Please select Department")
                    End If
                    'If clsCommon.myLen(Me.txtUnitCode.Value) <= 0 Then
                    '    txtUnitCode.Focus()
                    '    Throw New Exception("Please select Select Cost Center Unit Code")
                    'End If
                    'If clsCommon.myLen(Me.txtCostCenterType.Value) <= 0 Then
                    '    txtCostCenterType.Focus()
                    '    Throw New Exception("Please select Cost Center Type Code")
                    'End If
                End If
                ''=============================
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                    txtFromLocation.Focus()
                    Throw New Exception("Please Enter From Location")
                End If
                If ShowCapexCodeandSubCode Then
                    If clsCommon.myLen(fndcapexsubcode.Value) <= 0 Then
                        Throw New Exception("Please Enter capex sub code")
                        fndcapexsubcode.Focus()
                        Return False
                    End If

                    If clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text) < clsCommon.myCdbl(lblDocAmount.Text) Then
                        clsCommon.MyMessageBoxShow(Me, "Document amount exceed budget amount and above tolerence limit.", Me.Text)
                        Return False
                    End If
                    If clsCommon.myCdbl(lbl_rebudgetamt.Text) < clsCommon.myCdbl(lblDocAmount.Text) Then
                        clsCommon.MyMessageBoxShow(Me, "Warning: Document amount exceed budget amount but under tolerence limit.", Me.Text)
                    End If
                End If
            End If

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                txtDocNo.Focus()
                Throw New Exception("Document No Not found to save")
            End If
            If clsCommon.myLen(cboDocType.SelectedValue) <= 0 Then
                cboDocType.Focus()
                Throw New Exception("Please select Document Type")
            End If


            If clsCommon.myLen(TxtVehicle.Value) > 0 Then
                Dim count As Decimal = 0
                Dim segno As String = String.Empty
                Dim strvehiclenum As String = clsCommon.myCstr(TxtVehicle.Value)
                Dim Sql As String = "select Description from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(TxtVehicle.Value) + "' or Description = '" + Convert.ToString(TxtVehicle.Value) + "'"
                If Not String.IsNullOrEmpty(connectSql.RunScalar(Sql)) Then

                Else
                    Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
                    Dim strVehicalNo As String = clsCommon.myCstr(TxtVehicle.Value)
                    strmessage += "Do you want to continue "
                    If clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        count = connectSql.RunScalar("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")
                        TxtVehicle.Value = Convert.ToString(count + 1) + "-Man"
                        Sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                        segno = CStr(connectSql.RunScalar(Sql))
                        connectSql.RunSpTransaction("sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@desc", strVehicalNo), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                        connectSql.RunSpTransaction("SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", clsCommon.myCstr(TxtVehicle.Value)), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                        lblVehicleDesc.Text = strvehiclenum
                    Else
                        TxtVehicle.Value = String.Empty
                    End If
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim Icode As String = gv1.Rows(ii).Cells(colICode).Value
                    Dim CCCode As String = String.Empty
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        CCCode = gv1.Rows(ii).Cells(colCostCenterCode).Value
                    Else
                        CCCode = gv1.Rows(ii).Cells(colCCCode).Value
                    End If
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colICode).Value
                        Dim CCCode1 As String = String.Empty
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            CCCode1 = gv1.Rows(jj).Cells(colCostCenterCode).Value
                        Else
                            CCCode1 = gv1.Rows(jj).Cells(colCCCode).Value
                        End If
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                Throw New Exception("'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                            End If
                        End If
                    Next
                Next
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    Dim Icode As String = gv1.Rows(ii).Cells(colICode).Value
                    Dim CCCode As String = String.Empty
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        CCCode = gv1.Rows(ii).Cells(colCostCenterCode).Value
                    Else
                        CCCode = gv1.Rows(ii).Cells(colCCCode).Value
                    End If
                    Dim itemL As Integer = clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value)
                    Dim qq As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Dim Reqqty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colReqQty).Value)
                    If itemL <= 0 Then
                    Else
                        If Reqqty <> 0 Then
                            If qq = 0 Then
                                Throw New Exception("Qty can not be zero for '" + Icode + "' Item ")
                            End If
                        End If
                        If clsCommon.CompairString(cboDocType.SelectedValue, "Issue") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndReqNo.Value) > 0 Then
                            If qq > Reqqty Then
                                Throw New Exception("Issue qty can not be greater than from Required qty for item : " + Icode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    End If
                    For jj As Integer = 0 To ii - 1
                        Dim Icode1 As String = gv1.Rows(jj).Cells(colICode).Value
                        Dim CCCode1 As String = String.Empty
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            CCCode1 = gv1.Rows(jj).Cells(colCostCenterCode).Value
                        Else
                            CCCode1 = gv1.Rows(jj).Cells(colCCCode).Value
                        End If
                        If clsCommon.myLen(Icode) > 0 Then
                            If clsCommon.CompairString(Icode + CCCode, Icode1 + CCCode1) = CompairStringResult.Equal Then
                                Throw New Exception("'Item' And 'Cost Code' are repeated on line '" + clsCommon.myCstr(jj + 1) + "'AND '" + clsCommon.myCstr(ii + 1) + "'")
                            End If
                        End If
                    Next
                Next
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strCostCode As String = String.Empty
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                    strCostCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colCostCenterCode).Value)
                Else
                    strCostCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colCCCode).Value)
                End If
                Dim RetQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value)
                Dim PINo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)
                Dim PIQty As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPIPendingQty).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    If chkReProcess.Checked AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "Issue") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsItemMaster.GetItemType(strICode, Nothing), "F") <> CompairStringResult.Equal Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colICode)
                        Throw New Exception("For reprocess item should be finished,see at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = 0
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                        dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
                        RetQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal AndAlso AutoPurchaseReturn AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) > 0 AndAlso chk_againstmonthend.Checked = False Then
                            dblBalQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPIPendingQty).Value)
                        Else
                            dblBalQty = dblQty
                        End If
                    End If


                    Dim dblEnteredQty As Double = RetQty
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
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") <> CompairStringResult.Equal AndAlso chk_againstmonthend.Checked = False Then
                        If dblEnteredQty > dblBalQty Then
                            Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        End If
                    End If


                    If Not clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                            Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                                If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                                    Throw New Exception("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                                If arrSerailNo Is Nothing OrElse RetQty <> arrSerailNo.Count Then
                                    Throw New Exception("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        End If

                        If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                            If RunBatchFifowise = 1 Then
                                gv1.CurrentRow = gv1.Rows(ii)
                                OpenBatchItem()
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                                Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                                If arrBatchNo Is Nothing Then
                                    Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                Else
                                    Dim tQty As Decimal = 0
                                    For Each objBatch As clsBatchInventory In arrBatchNo
                                        tQty += objBatch.Qty
                                    Next
                                    If tQty <> clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) > 0 Then
                                        Throw New Exception("Item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " Issued Qty " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                    End If
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value) > 0 Then
                                Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                                If arrBatchNo Is Nothing Then
                                    Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                Else
                                    Dim tQty As Decimal = 0
                                    For Each objBatch As clsBatchInventory In arrBatchNo
                                        tQty += objBatch.Qty
                                    Next
                                    If tQty <> clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value) > 0 Then
                                        Throw New Exception("Item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " Returned Qty " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                    End If
                                End If
                            End If
                        End If
                    End If


                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                        If RetQty = 0 Then
                            Throw New Exception("Return qty can not be zero for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If dblQty < RetQty AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 AndAlso chk_againstmonthend.Checked = False Then
                            Throw New Exception("Return qty can not be greater than from issued qty for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If AutoPurchaseReturn AndAlso clsCommon.myLen(PINo) > 0 Then
                            If RetQty > PIQty Then
                                Throw New Exception("Return qty can not be greater than from pending purchase invoice qty for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        ElseIf AutoPurchaseReturn AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) > 0 AndAlso clsCommon.myLen(PINo) <= 0 Then
                            Throw New Exception("Item : " + strICode + " is not allowed,because it is not in Purchase Invoice see at line no" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") <> CompairStringResult.Equal Then  '===Sanjeet(19/01/2018) Free Hierarchy Level And Cost Centre code==============

                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colHierarchyCode).Value)) <= 0 Then
                                Throw New Exception("Please provide Hierarchy for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If clsCommon.myLen(strCostCode) <= 0 Then
                                Throw New Exception("Please provide cost code for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        Else
                            If clsCommon.myLen(strCostCode) <= 0 AndAlso clsCommon.myLen(PINo) <= 0 Then
                                Throw New Exception("Please provide cost code for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If

                    End If

                    Dim ExtdAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                    If ChkAllowWithoutUnitCostEntry = False Then
                        If ExtdAmt <= 0 Then
                            Throw New Exception("Please provide amount for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            Next
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekPostBTn As Boolean)
        Try
            '' Anubhooti 09-Sep-2014 BM00000003735
            If ChekPostBTn = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Issue/Return/Transfer", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''
            If (AllowToSave()) Then
                Dim obj As New clsIssueReturnHead()
                obj.Doc_No = txtDocNo.Value
                obj.Doc_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.Issue_To = txtIssueTo.Value
                obj.Request_By = txtRequestBy.Value
                obj.Remarks = txtRemarks.Text
                obj.Comment = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.From_Location = txtFromLocation.Value
                obj.To_Location = txtToLocation.Value
                obj.Req_IssueNo = fndReqNo.Value
                obj.RequisitionNo = lblReq3.Text
                obj.Dept = txtDepartment.Value
                obj.Dept_Desc = lblDepartment.Text
                obj.Is_Skip_Dept_Indent_Balance = chkSkipIndentBalance.Checked
                obj.Tax_Group = txtTaxGroup.Value
                obj.Tax_Desc = lblTaxGrpName.Text
                obj.Is_Reject = chkReject.Checked
                obj.Reject_Vendor_Code = txtVendor.Value

                obj.Capex_Code = fndcapexcode.Value
                obj.Capex_SubCode = fndcapexsubcode.Value

                obj.CosCenter_Unit = txtUnitCode.Value
                obj.CostCenter_Type = txtCostCenterType.Value

                obj.Is_Reprocess = chkReProcess.Checked
                lblVendor.Text = clsVendorMaster.GetName(obj.Reject_Vendor_Code, Nothing)
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
                obj.BeforeTax_Amt = lblAmtAfterDiscount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.doc_Amt = lblTotRAmt.Text
                obj.Vehicle_Id = clsCommon.myCstr(TxtVehicle.Value)
                'obj.Machine_Id = clsCommon.myCstr(TxtMachinery.Value)
                obj.Against_Month_End = chk_againstmonthend.Checked

                obj.PurchaseInvoice_No = fndPurchaseInvoice.Value

                obj.Arr = New List(Of clsIssueReturnDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsIssueReturnDetail()
                    objTr.arrBatchItem = New List(Of clsBatchInventory)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Req_IssueNo = clsCommon.myCstr(grow.Cells(colReq_IssueNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    ''richa agarwal 1 Dec ,2016
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                        objTr.Cost_Centre_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                        If EnableHirerachyCostCentre = 1 Then
                            objTr.HirerachyLevelCode3 = clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value)
                            objTr.HirerachyLevelCode4 = clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)
                        End If
                    Else
                        objTr.Cost_Code = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                    End If
                    ''---------------

                    objTr.Required_Qty = clsCommon.myCdbl(grow.Cells(colReqQty).Value)

                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    If ZeroCostForReprocess = True AndAlso chkReProcess.Checked = True Then
                        objTr.Unit_Cost = 0
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    End If


                    ''================================================================================================
                    objTr.PurchaseInvoice_No = clsCommon.myCstr(grow.Cells(colPINo).Value)
                    objTr.PurchaseInvoice_Bal_Qty = clsCommon.myCdbl(grow.Cells(colPIPendingQty).Value)
                    objTr.SRN_ID = clsCommon.myCstr(grow.Cells(colSRNID).Value)
                    objTr.MRN_ID = clsCommon.myCstr(grow.Cells(colMRNID).Value)
                    objTr.GRN_Id = clsCommon.myCstr(grow.Cells(colGRNID).Value)
                    objTr.PO_ID = clsCommon.myCstr(grow.Cells(colPOID).Value)
                    ''================================================================================================

                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                        If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                            objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                            objTr.Issued_Qty_AgainstRet = 0
                        Else
                            objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                            objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        End If
                    Else
                        objTr.Issued_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Issued_Qty_AgainstRet = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    End If
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
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))


                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))

                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
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

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Doc_No)
                    'If clsCommon.CompairString(cboDocType.Text, "return") = CompairStringResult.Equal AndAlso chk_againstmonthend.Checked = True Then
                    '    obj.Doc_Date = clsCommon.myCDate(txtDate.Text).AddDays(1)
                    '    obj.Doc_Type = "Issue"
                    '    obj.Doc_No = ""
                    '    If (obj.SaveData(obj, isNewEntry)) Then
                    '        UcAttachment1.SaveData(obj.Doc_No)
                    '    End If
                    'End If
                    If ChekPostBTn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.Doc_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            chk_againstmonthend.Checked = False
            isInsideLoadData = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls(True)
            LoadBlankGrid()
            LoadBlankGridTax()

            Dim obj As New clsIssueReturnHead()
            obj = clsIssueReturnHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_No) > 0) Then
                chkSkipIndentBalance.Checked = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btncancel.Enabled = True
                    If CostCenterAndHirerachyCodeUpdateAfterPost = True Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If
                Else
                    btncancel.Enabled = False
                    butCostCenterAndHirerachy_Update_AfterPost.Visible = False
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Doc_No
                txtDate.Value = obj.Doc_Date
                txtIssueTo.Value = obj.Issue_To
                lblIssueTo.Text = obj.Issue_ToName
                txtRequestBy.Value = obj.Request_By
                lblRequestBy.Text = obj.Request_ByName
                chkOnHold.Checked = obj.On_Hold
                txtComment.Text = obj.Comment
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                cboDocType.SelectedValue = obj.Doc_Type
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                    chk_againstmonthend.Visible = True
                Else
                    chk_againstmonthend.Visible = False
                End If
                'txtFromLocation.Value = obj.From_Location
                'lblFromLocation.Text = obj.From_LocationName
                txtToLocation.Value = obj.To_Location
                lblToLocation.Text = obj.To_LocationName
                fndReqNo.Value = obj.Req_IssueNo
                lblReq3.Text = obj.RequisitionNo
                lblDocAmount.Text = obj.doc_Amt
                If clsCommon.myLen(fndReqNo.Value) > 0 Then
                    LoadReqDataHead(fndReqNo.Value)
                    gv1.Rows.Clear()
                End If
                txtFromLocation.Value = obj.From_Location
                lblFromLocation.Text = obj.From_LocationName
                txtDepartment.Value = obj.Dept
                lblDepartment.Text = obj.Dept_Desc
                chkSkipIndentBalance.Checked = obj.Is_Skip_Dept_Indent_Balance
                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGrpName.Text = obj.Tax_Desc
                lblAmtAfterDiscount.Text = obj.BeforeTax_Amt
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lblTotRAmt.Text = obj.doc_Amt
                lblDocAmount.Text = lblTotRAmt.Text
                chk_againstmonthend.Checked = obj.Against_Month_End
                TxtVehicle.Value = obj.Vehicle_Id
                lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where  Segment_Code= '" + TxtVehicle.Value + "'")
                'TxtMachinery.Value = obj.Machine_Id
                'lblMachineDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '5' AND Segment_Code= '" + TxtMachinery.Value + "'")

                chkReProcess.Checked = obj.Is_Reprocess
                chkReProcess.Enabled = False

                'added by priti
                fndReqNo.Enabled = False
                txtFromLocation.Enabled = False
                'added by priti on 25/07/2013  to allow  for wrong entry
                'txtToLocation.Enabled = False
                cboDocType.Enabled = False
                
                ''==Sanjeet (Load CAPEX Detail and check it's amount)======
                fndcapexcode.Value = obj.Capex_Code
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
                fndcapexsubcode.Value = obj.Capex_SubCode
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, obj.Doc_No, Nothing)
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, obj.Doc_No, Nothing)
                End If

                txtUnitCode.Value = obj.CosCenter_Unit
                If clsCommon.myLen(Me.txtUnitCode.Value) > 0 Then
                    lblUnitDesc.Text = clsUnitMaster.GetName(Me.txtUnitCode.Value)
                End If
                txtCostCenterType.Value = obj.CostCenter_Type
                If clsCommon.myLen(Me.txtCostCenterType.Value) > 0 Then
                    lblCostcenterTypeDesc.Text = clsCostCenterTypeMaster.GetName(Me.txtCostCenterType.Value)
                End If
                '======================
                If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                    lblReq2.Visible = False
                    lblReq3.Visible = False
                    lblReq.Visible = True
                    fndReqNo.Visible = True
                    lblReqDate.Visible = True
                    lblReq.Text = "Requisition No"
                    repoReqIssueNo.HeaderText = "Requisition No"
                    chkWithoutRefNo.Enabled = False
                    lblToLocation.Visible = False
                    txtToLocation.Visible = False
                    'richa agarwal
                    RadLabel2.Text = "Issue To"
                    RadLabel8.Visible = True
                    txtFromLocation.Visible = True
                    lblFromLocation.Visible = True
                    RadLabel9.Visible = False
                    '---------------
                    chkReject.Visible = False
                    chkReProcess.Visible = True
                    chkReProcess.Enabled = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                    MyLabel2.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Visible)
                    fndPurchaseInvoice.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Visible)
                    chkAgnstPI.Visible = AutoPurchaseReturn

                    lblReq2.Visible = True
                    lblReq3.Visible = True
                    lblReq.Visible = True
                    fndReqNo.Visible = True
                    lblReqDate.Visible = True
                    lblReq.Text = "Issue No"
                    repoReqIssueNo.HeaderText = "Issue No"
                    lblToLocation.Visible = True
                    txtToLocation.Visible = True
                    If clsCommon.myLen(obj.Req_IssueNo) <= 0 Then
                        chkWithoutRefNo.Checked = True
                        chkWithoutRefNo.Enabled = True
                        chkWithoutRefNoChanged()
                    End If
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                        gv1.Columns(colRetQty).IsVisible = True
                    End If

                    'richa agarwal
                    RadLabel9.Visible = True
                    RadLabel2.Text = "Return From"
                    ''=====Sanjeet (Fro Month end Return)===========
                    If chk_againstmonthend.Checked Then
                        RadLabel8.Visible = True
                        txtFromLocation.Visible = True
                        lblFromLocation.Visible = True
                    Else
                        RadLabel8.Visible = False
                        txtFromLocation.Visible = False
                        lblFromLocation.Visible = False
                    End If
                    ''==============================
                    'RadLabel8.Visible = False
                    'txtFromLocation.Visible = False
                    'lblFromLocation.Visible = False

                    chkReject.Visible = True
                    chkReject.Checked = obj.Is_Reject
                    txtVendor.Value = obj.Reject_Vendor_Code
                    lblVendor.Text = clsVendorMaster.GetName(obj.Reject_Vendor_Code, Nothing)

                    chkReProcess.Visible = False
                    chkReProcess.Enabled = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                    lblReq2.Visible = False
                    lblReq3.Visible = False
                    lblReq.Visible = False
                    fndReqNo.Visible = False
                    lblReqDate.Visible = False
                    chkWithoutRefNo.Enabled = False
                    lblToLocation.Visible = True
                    txtToLocation.Visible = True
                    chkReject.Visible = False

                    chkReProcess.Visible = False
                    chkReProcess.Enabled = False
                End If
                ' ended by priti
                setRejectVisiblity()
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
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

                chkReject.Checked = obj.Is_Reject
                fndPurchaseInvoice.Value = obj.PurchaseInvoice_No
                fndPurchaseInvoice.MendatroryField = False
                ''===================================purchase return work
                If AutoPurchaseReturn AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) > 0 AndAlso Not chkReject.Checked Then
                    chkAgnstPI.Checked = True
                    fndPurchaseInvoice.MendatroryField = True
                End If
                If AutoPurchaseReturn AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) > 0 Then
                    txtVendor.Enabled = False
                    txtToLocation.Enabled = False
                    txtFromLocation.Enabled = False
                    MyLabel6.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
                    txtVendor.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
                    lblVendor.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
                    'RadLabel8.Visible = AutoPurchaseReturn
                    'txtFromLocation.Visible = AutoPurchaseReturn
                    'lblFromLocation.Visible = AutoPurchaseReturn
                    If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                        gv1.Columns(colRetQty).IsVisible = True
                    End If
                End If
                fndPurchaseInvoice.Enabled = False
                chkAgnstPI.Enabled = False
                chkReject.Enabled = False
                ''=================================================================


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsIssueReturnDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = objTr.Req_IssueNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                        ''richa agarwal 1 Dec ,2016
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.Cost_Centre_Code
                            If EnableHirerachyCostCentre = 1 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel2).Value = objTr.HirerachyLevelCode3
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel3).Value = objTr.HirerachyLevelCode4
                                OpenCostCentreLevelName(False)
                            End If
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = objTr.Cost_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(objTr.Cost_Code)
                        End If
                        ''----------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = objTr.Required_Qty

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Unit_Cost

                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                            If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 0
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty_AgainstRet
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty
                            End If
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Issued_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.Issued_Qty_AgainstRet
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
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

                        ''=========================================================================================================
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = objTr.PurchaseInvoice_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPIPendingQty).Value = objTr.PurchaseInvoice_Bal_Qty
                        If clsCommon.myLen(objTr.PurchaseInvoice_No) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPIPendingQty).Value = clsPurchaseInvoiceDetail.GetBalancePIQty(objTr.PurchaseInvoice_No, objTr.Item_Code, Nothing, objTr.Unit_code, Nothing, objTr.Amount, chkReject.Checked, txtDocNo.Value)
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNID).Value = objTr.SRN_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNID).Value = objTr.MRN_ID
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNID).Value = objTr.GRN_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPOID).Value = objTr.PO_ID
                        ''=========================================================================================================

                        If EnableRackBin Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtFromLocation.Value & "'")
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & objTr.Item_Code & "' and location='" & txtFromLocation.Value & "'")
                        End If

                        If clsCommon.CompairString(obj.Doc_Type, "Issue") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = False
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                        End If


                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem

                    Next
                    If obj.Status = ERPTransactionStatus.Pending AndAlso clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.Rows.AddNew()
                    End If
                End If

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Doc_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Doc_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Doc_No)
            Else
                gv1.Rows.AddNew()
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
        GC.Collect()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Issue/Return/Transfer", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateGLAccToItem, clsFixedParameterCode.CreateGLAccToItem, Nothing)), "0") = CompairStringResult.Equal Then
                        Throw New Exception("Please select Create Journal Entry Acc. To Item(Issue/Return) on Purchase Setting.")
                        ' Return False
                    End If
                End If
                If (clsIssueReturnHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    If chkReject.Checked = True Then
                        SendSMSandEmail()
                    End If
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SendSMSandEmail()
        Try
            'Dim obj As clsESContent = clsESContent.GetData(clsUserMgtCode.mbtnIssueReturn)
            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'Dim strContactPerson As String = ""
            'Dim strSubject As String = obj.EMail_Subject

            'Dim strbody As String = obj.EMail_Text.Replace(clsEmailSMSConstants.DOC_NO, txtDocNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.Doc_Amount, lblTotRAmt.Text)
            'Dim body As String = strbody

            'Dim lstReceiptents As New List(Of String)
            'Dim emailId As String = Nothing
            'emailId = clsDBFuncationality.getSingleValue("select Contact_Person_Email from TSPL_VENDOR_MASTER where Vendor_code ='" & txtVendor.Value & "' ")
            'lstReceiptents.Add(emailId)
            'For Each dr As String In obj.arrAlertEmployeeEMail
            '    Dim qry As String = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + dr + "') "
            '    emailId = clsDBFuncationality.getSingleValue(qry)
            '    lstReceiptents.Add(emailId)
            'Next
            'clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, Nothing)
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)


            'sanjay
            Dim strContactPerson As String = ""

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnIssueReturn + "'", Nothing)
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
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myCstr(lblTotRAmt.Text))


                    Dim emailId As String = Nothing
                    emailId = clsDBFuncationality.getSingleValue("select Contact_Person_Email from TSPL_VENDOR_MASTER where Vendor_code ='" & txtVendor.Value & "' ")

                    objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))

                    objEmailH.SaveData(clsUserMgtCode.mbtnIssueReturn, objEmailH, Nothing)
                    objEmailH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                End If
                'sanjay

            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (AllowOnlyOneIssueAgainstStoreRequisition = True) AndAlso clsCommon.myLen(fndReqNo.Value) > 0 AndAlso (clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal) Then
                    Dim StrReqCloseStatus As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select close_yn from TSPL_REQUISITION_HEAD where Requisition_Id='" + fndReqNo.Value + "'"))
                    If clsCommon.CompairString(StrReqCloseStatus, "Y") = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow("Please Open Requistion No - " + fndReqNo.Value + " first.")
                        Exit Sub
                    End If
                End If
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
                If (clsIssueReturnHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew(True)
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_IssueReturn_HEAD where Doc_No='" + txtDocNo.Value + "'"
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
        Dim qry As String = "select Doc_No as Code,Doc_Date as Date,Doc_Type as Type,case when Status='0' then 'Pending' else 'Approved' end as [Status],RequisitionNo as [Requisition No.] from TSPL_IssueReturn_HEAD"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " from_location in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        LoadData(clsCommon.ShowSelectForm("IRTCodeFilter", qry, "Code", whrClas, txtDocNo.Value, "Doc_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.O Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                chkSkipIndentBalance.Checked = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                'Add Tool tip Task No- TEC/22/05/18-000245
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                       "TSPL_IssueReturn_HEAD " + Environment.NewLine +
                                       "TSPL_IssueReturn_DETAIL " + Environment.NewLine +
                                       "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                       "TSPL_BATCH_ITEM " + Environment.NewLine +
                                       "Press Alt+P for Post Trasnaction" + Environment.NewLine +
                                       "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                       "TSPL_JOURNAL_MASTER " + Environment.NewLine +
                                       "TSPL_JOURNAL_DETAILS " + Environment.NewLine +
                                       "TSPL_PR_HEAD (If setting-AutoPurchaseReturnFromIssueReturn is on) " + Environment.NewLine +
                                       "TSPL_PR_DETAIL " + Environment.NewLine +
                                       "TSPL_PI_REMITTANCE")
                'Add Tool tip Task No- TEC/22/05/18-000245
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

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    Else
                        If AllowOnlyOneIssueAgainstStoreRequisition = False Then
                            gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                        End If
                    End If
                ElseIf e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.myLen(fndReqNo.Value) <= 0 Then
                        gv1.CurrentRow.Cells(colICode).ReadOnly = False
                    Else
                        If AllowOnlyOneIssueAgainstStoreRequisition = False Then
                            gv1.CurrentRow.Cells(colICode).ReadOnly = True
                        End If
                    End If
                    '' Anubhooti 30-Jan-2015 (Unit Cost Editable/Non-Editable On Issue/Return/Transfer Based On Settings)
                ElseIf e.Column Is gv1.Columns(colRate) Then
                    If CostEditOnIRT = True Then
                        gv1.CurrentRow.Cells(colRate).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colRate).ReadOnly = True
                    End If
                End If
                ''richa agarwal 1 Dec ,2016
                If e.Column Is gv1.Columns(colCCCode) Then
                    If ShowCostCenterAndHierarchyLevelInPurchaseModule Then
                        If StoreRequisitionMandatory AndAlso chkReProcess.Checked = False AndAlso clsCommon.myLen(fndPurchaseInvoice.Value) <= 0 Then
                            gv1.CurrentRow.Cells(colCCCode).ReadOnly = True
                        Else
                            gv1.CurrentRow.Cells(colCCCode).ReadOnly = False
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Public Sub Print()
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            '            Dim qry As String = "SELECT     TSPL_IssueReturn_HEAD.Doc_No, TSPL_IssueReturn_HEAD.Doc_Date,TSPL_IssueReturn_HEAD.Doc_Type, TSPL_IssueReturn_HEAD.Remarks, TSPL_IssueReturn_HEAD.Comment, " & _
            '                     " case when  TSPL_IssueReturn_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueReturn_HEAD.Posting_Date, TSPL_IssueReturn_DETAIL.Item_Code,  " & _
            '                      " TSPL_IssueReturn_DETAIL.Item_Desc, TSPL_IssueReturn_DETAIL.Required_Qty, TSPL_IssueReturn_DETAIL.Issued_Qty,  " & _
            '                      " TSPL_IssueReturn_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,  " & _
            '                      " TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,  " & _
            '                      " loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy  " & _
            '    " FROM  TSPL_IssueReturn_HEAD INNER JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.Doc_No = TSPL_IssueReturn_DETAIL.Doc_No  " & _
            '" LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueReturn_HEAD.Issue_To = emp1.EMP_CODE  " & _
            '"  LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueReturn_HEAD.Request_By = emp2.EMP_CODE  " & _
            '"   LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueReturn_HEAD.From_Location = loc1.Location_Code  " & _
            ' "   LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueReturn_HEAD.To_Location = loc2.Location_Code  " & _
            ' "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueReturn_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code  " & _
            '"  where TSPL_IssueReturn_HEAD.Doc_No='" + txtDocNo.Value + "'"
            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim qry As String = " SELECT     TSPL_IssueReturn_HEAD.Doc_No, TSPL_IssueReturn_HEAD.Doc_Date,TSPL_IssueReturn_HEAD.Doc_Type, TSPL_IssueReturn_HEAD.Remarks, TSPL_IssueReturn_HEAD.Comment,  case when  TSPL_IssueReturn_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueReturn_HEAD.Posting_Date, TSPL_IssueReturn_DETAIL.Item_Code,   TSPL_IssueReturn_DETAIL.Item_Desc, TSPL_IssueReturn_DETAIL.Required_Qty, TSPL_IssueReturn_DETAIL.Issued_Qty as returnqty,   TSPL_IssueReturn_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,(select xxxx.Issued_Qty  from TSPL_IssueReturn_DETAIL  xxxx where xxxx.Doc_No=TSPL_IssueReturn_HEAD.Req_IssueNo and xxxx.Item_Code=TSPL_IssueReturn_DETAIL .Item_Code  )as [Issued_Qty]     FROM  TSPL_IssueReturn_HEAD "
            ''qry += " INNER JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.Doc_No = TSPL_IssueReturn_DETAIL.Doc_No"
            ''qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueReturn_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueReturn_HEAD .Issue_To = emp1.EMP_CODE  "
            ''qry += " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueReturn_HEAD.Request_By = emp2.EMP_CODE    "
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueReturn_HEAD.From_Location = loc1.Location_Code"
            ''qry += " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueReturn_HEAD.To_Location = loc2.Location_Code    "
            ''qry += " where TSPL_IssueReturn_HEAD.Doc_No='" + txtDocNo.Value + "'"

            Dim qry As String = "     SELECT  TSPL_CostCenter_MASTER.cost_name,  TSPL_IssueReturn_HEAD.Created_By ,TSPL_IssueReturn_HEAD.Modify_By ,   TSPL_IssueReturn_HEAD.Doc_No, TSPL_IssueReturn_HEAD.Doc_Date,TSPL_IssueReturn_HEAD.Doc_Type, TSPL_IssueReturn_HEAD.Remarks, TSPL_IssueReturn_HEAD.Comment,  case when  TSPL_IssueReturn_HEAD.Status=0 then 'Pending' else 'Approved' end as Status, TSPL_IssueReturn_HEAD.Posting_Date, TSPL_IssueReturn_DETAIL.Item_Code,   TSPL_IssueReturn_DETAIL.Item_Desc, TSPL_IssueReturn_DETAIL.Required_Qty, TSPL_IssueReturn_DETAIL.Issued_Qty_AgainstRet as returnqty,   TSPL_IssueReturn_DETAIL.Unit_code, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2,   TSPL_COMPANY_MASTER.Add3, TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, loc1.Location_Desc as Fromloc,loc2.Location_Desc as Toloc, emp1.Emp_Name as IssuesTo,emp2.Emp_Name as RequestBy,"
            'qry += " --(select xxxx.Issued_Qty  from TSPL_IssueReturn_DETAIL  xxxx where xxxx.Doc_No=TSPL_IssueReturn_HEAD.Req_IssueNo and xxxx.Item_Code=TSPL_IssueReturn_DETAIL .Item_Code  )"
            qry += " TSPL_IssueReturn_DETAIL.Issued_Qty as [Issued_Qty] ,TSPL_COMPANY_MASTER.Logo_Img as [Logo_Img],  TSPL_COMPANY_MASTER.Logo_Img2 as [Logo_Img2],'" + objCommonVar.CurrentUser + "' as User_Name    FROM  TSPL_IssueReturn_HEAD  INNER JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.Doc_No = TSPL_IssueReturn_DETAIL.Doc_No LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueReturn_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueReturn_HEAD .Issue_To = emp1.EMP_CODE   LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueReturn_HEAD.Request_By = emp2.EMP_CODE     LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueReturn_HEAD.From_Location = loc1.Location_Code LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueReturn_HEAD.To_Location = loc2.Location_Code              LEFT OUTER JOIN  TSPL_CostCenter_MASTER   ON TSPL_IssueReturn_DETAIL.Cost_Code  = TSPL_CostCenter_MASTER.Cost_Code  "
            qry += " where TSPL_IssueReturn_HEAD.Doc_No='" + txtDocNo.Value + "'"



            ''------------------------------ Changes by shipra on 24/06/13--------------------------------------------''

            ''Dim demo As String
            ''If demo = "" Then
            ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            ''    demo = dt.Rows(0)("Doc_type").ToString


            ''End If


            Dim type As String = "select Doc_type from TSPL_IssueReturn_HEAD where Doc_No='" + txtDocNo.Value + "'"
            Dim val As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(type))
            'Dim dr As SqlDataReader
            'dr = connectSql.RunSqlReturnDR(type)
            'While dr.Read()
            '    val = dr(0).ToString
            'End While

            If val = "Issue" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptissueNewV", "Issur/Return/Transfer")
                'PurchaseOrderViewer.funreport(dt, "rptissue", "Issur/Return/Transfer")
            ElseIf val = "Return" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptreturnNewV", "Issur/Return/Transfer")

                ' PurchaseOrderViewer.funreport(dt, "rptreturn", "Issur/Return/Transfer")

            ElseIf val = "Transfer" Then
                ''''---------------------Added By ----Pankaj Kumar----on 04/03/2012------------------------
                Dim QryTrnsfr As String = "select TSPL_IssueReturn_HEAD.Created_By,TSPL_IssueReturn_HEAD.Modify_By, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, TSPL_COMPANY_MASTER.logo_img, TSPL_COMPANY_MASTER.logo_img2, TSPL_COMPANY_MASTER.Comp_Name  as CompanyName, " & _
    " TSPL_COMPANY_MASTER.Tin_No as CompanyTin,Case when len(TSPL_COMPANY_MASTER.Add1)>0 then TSPL_COMPANY_MASTER.Add1 else '' end +case when len(TSPL_COMPANY_MASTER.Add2)>0 then ','  else  '' end  +case when len(TSPL_COMPANY_MASTER.Add2)>0 then TSPL_COMPANY_MASTER.Add2  else  '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ','  else  '' end +case when len(TSPL_COMPANY_MASTER.Add3)>0 then TSPL_COMPANY_MASTER.Add3  else  '' end as CompanyAddress, " & _
    " TSPL_IssueReturn_HEAD.Doc_No, TSPL_IssueReturn_HEAD.Doc_Date, TSPL_IssueReturn_HEAD.Doc_Type, " & _
    " (select Case when (len(TSPL_LOCATION_MASTER .Add1)>0) then convert(varchar(20),TSPL_LOCATION_MASTER.Add1,103) else '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add2)>0) then ', '+ convert(varchar(20),TSPL_LOCATION_MASTER.Add2,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add3)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add3,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.Add4)>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.Add4,103)  else  '' end + " & _
" case when (len(TSPL_LOCATION_MASTER.City_Code )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.City_Code,103) else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.State )>0) then ', '+convert(varchar(20),TSPL_LOCATION_MASTER.State,103)  else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.Pin_Code )>0) then ', '+convert(varchar(10),TSPL_LOCATION_MASTER.Pin_Code,103)  else  ''  end + " & _
" case when (len(TSPL_LOCATION_MASTER.Country )>0) then ', '+TSPL_LOCATION_MASTER.Country  else  ''  end  from TSPL_LOCATION_MASTER where location_Code='L001' ) as Address, " & _
    " (select Location_Desc from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueReturn_HEAD.To_Location) as ToLocDesc, " & _
    " (select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueReturn_HEAD.To_Location) as ToLocSegCode, " & _
    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueReturn_HEAD.To_Location) as TinNo, " & _
    " (select TCAN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueReturn_HEAD.To_Location) as CstNo, " & _
    " (select TIN_No from TSPL_LOCATION_MASTER where location_Code=TSPL_IssueReturn_HEAD.From_Location) as CompanyTin, " & _
    " '' as NRG_No, TSPL_IssueReturn_DETAIL.Item_Code AS ItemCode, TSPL_IssueReturn_DETAIL.Item_Desc AS Desciption, " & _
    " TSPL_IssueReturn_DETAIL.Issued_Qty AS Quantity, TSPL_IssueReturn_DETAIL.Unit_code AS Uom, TSPL_IssueReturn_DETAIL.Unit_Cost AS Rate, " & _
    " TSPL_IssueReturn_DETAIL.Amount AS Amount, TSPL_IssueReturn_HEAD.TAX1 AS TaxRateDesc1, TSPL_IssueReturn_HEAD.TAX1_Amt as TaxRate1, " & _
    " TSPL_IssueReturn_HEAD.TAX2 as TaxRateDesc2, TSPL_IssueReturn_HEAD.TAX2_Amt as TaxRate2, TSPL_IssueReturn_HEAD.TAX3 as TaxRateDesc3, " & _
    " TSPL_IssueReturn_HEAD.TAX3_Amt as TaxRate3, TSPL_IssueReturn_HEAD.TAX4 as TaxRateDesc4, TSPL_IssueReturn_HEAD.TAX4_Amt as TaxRate4, " & _
    " TSPL_IssueReturn_HEAD.TAX5 as TaxRateDesc5, TSPL_IssueReturn_HEAD.TAX5_Amt as TaxRate5, TSPL_IssueReturn_HEAD.TAX6 as TaxRateDesc6, " & _
    " TSPL_IssueReturn_HEAD.TAX6_Amt as TaxRate6, TSPL_IssueReturn_HEAD.TAX7 as TaxRateDesc7, TSPL_IssueReturn_HEAD.TAX7_Amt as TaxRate7, " & _
    " TSPL_IssueReturn_HEAD.TAX8 as TaxRateDesc8, TSPL_IssueReturn_HEAD.TAX8_Amt as TaxRate8, TSPL_IssueReturn_HEAD.TAX9 as TaxRateDesc9, " & _
    " TSPL_IssueReturn_HEAD.TAX9_Amt as TaxRate9, TSPL_IssueReturn_DETAIL.TAX10 as TaxRateDesc10, TSPL_IssueReturn_DETAIL.TAX10_Amt as  TaxRate10 " & _
    " FROM TSPL_IssueReturn_HEAD " & _
    " INNER JOIN TSPL_IssueReturn_DETAIL ON TSPL_IssueReturn_HEAD.Doc_No = TSPL_IssueReturn_DETAIL.Doc_No " & _
    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp1 ON TSPL_IssueReturn_HEAD.Issue_To = emp1.EMP_CODE " & _
    " LEFT OUTER JOIN  TSPL_EMPLOYEE_MASTER as emp2 ON TSPL_IssueReturn_HEAD.Request_By = emp2.EMP_CODE " & _
    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER as loc1 ON TSPL_IssueReturn_HEAD.From_Location = loc1.Location_Code " & _
    " LEFT OUTER JOIN  TSPL_LOCATION_MASTER  as loc2 ON TSPL_IssueReturn_HEAD.To_Location = loc2.Location_Code " & _
    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_IssueReturn_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code " & _
    " where TSPL_IssueReturn_HEAD.Doc_No='" + txtDocNo.Value + "' and TSPL_IssueReturn_HEAD.Doc_Type='" + val + "' "
                ''''--------------------------------------------------Code Ends Here--------------------------------------------------
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(QryTrnsfr)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "crptscrapTransfer", "Issur/Return/Transfer")

            End If
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtIssueTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtIssueTo._MYValidating
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtIssueTo.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtIssueTo.Value = obj.EMP_CODE
            lblIssueTo.Text = obj.Emp_Name
        Else
            txtIssueTo.Value = ""
            lblIssueTo.Text = ""
        End If
    End Sub

    Private Sub txtRequestBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequestBy._MYValidating
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtRequestBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtRequestBy.Value = obj.EMP_CODE
            lblRequestBy.Text = obj.Emp_Name
        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
        End If
    End Sub

    Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating
        'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtFromLocation.Value, isButtonClicked)
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
        '    txtFromLocation.Value = obj.Code
        '    lblFromLocation.Text = obj.Name
        'Else
        '    txtFromLocation.Value = ""
        '    lblFromLocation.Text = ""
        'End If


        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Category not in( 'MCC')  "
        'Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtFromLocation.Value = clsCommon.ShowSelectForm("IRFROMLOC", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
        lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))

    End Sub

    Private Sub txtToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtToLocation._MYValidating
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Sale") = CompairStringResult.Equal Then
            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
            txtToLocation.Value = clsCommon.ShowSelectForm("IRTCustCoode", qry, "Code", "Inter_Branch='Y'", txtToLocation.Value, "", isButtonClicked)
            lblToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtToLocation.Value + "' "))
        Else
            Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtToLocation.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtToLocation.Value = obj.Code
                lblToLocation.Text = obj.Name
            Else
                txtToLocation.Value = ""
                lblToLocation.Text = ""
            End If
        End If
    End Sub

    Private Sub cboDocType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocType.SelectedValueChanged
        repoReqQty.IsVisible = True
        repoReturnQty.IsVisible = False
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            repoReqQty.HeaderText = "Required Quantity"
            repoQty.HeaderText = "Issue Quantity"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            repoReturnQty.IsVisible = True
            repoReqQty.HeaderText = "Requisition Quantity"
            repoQty.HeaderText = "Issue Quantity"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
            repoReqQty.IsVisible = False
            repoQty.HeaderText = "Transfer Quantity"
        End If

    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("IRTAXGP", qry, "Code", "Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        SetTaxDetails()
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
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

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        ''richa agarwal 11/11/2014
        Dim dblQty As Double = 0
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRetQty).Value)
        Else
            dblQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)

        End If
        ''----------------------
        'Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)

        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = dblQty * dblRate
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
            If clsCommon.myLen(strTaxCode) > 0 Then
                Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
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
                    dblBaseAmt = (dblAmt + dblOtherTaxAmt)
                End If
                gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
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
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblAmt + dblTotTaxAmt
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)
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
        repoTaxRate.IsVisible = True
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

    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
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

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If True Then ''clsCommon.CompairString(gv2.CurrentCell.ColumnInfo.Name, colTTaxRate) = CompairStringResult.Equal Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FDTaxRate", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
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

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        If isInsideLoadData = False Then
            lblReq2.Visible = False
            lblReq3.Visible = False
            chkReject.Visible = False
            chkWithoutRefNo.Checked = False
            chkWithoutRefNo.Enabled = False
            chkReject.Enabled = True

            MyLabel2.Visible = False
            fndPurchaseInvoice.Visible = False
            chkAgnstPI.Visible = False
            fndcapexsubcode.Enabled = False

            txtIssueTo.Visible = True
            RadLabel2.Visible = True
            lblIssueTo.Visible = True

            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
                chk_againstmonthend.Visible = False
                AddNew(False)
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Requisition No"
                MyLabel2.Visible = False
                fndPurchaseInvoice.Visible = False
                chkAgnstPI.Visible = False
                lblReq.Visible = True
                fndReqNo.Visible = True
                lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Req. No"
                lblToLocation.Visible = False
                txtToLocation.Visible = False
                RadLabel9.Visible = False
                fndReqNo.MendatroryField = False
                RadLabel2.Text = "Issue To"
                RadLabel8.Visible = True
                txtFromLocation.Visible = True
                lblFromLocation.Visible = True
                chkReProcess.Enabled = True
                chkReProcess.Visible = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                AddNew(False)
                chk_againstmonthend.Visible = True
                chkWithoutRefNo.Enabled = True
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Issue No"
                lblReq.Visible = True
                fndReqNo.Visible = True
                lblReqDate.Visible = True
                repoReqIssueNo.HeaderText = "Issue No"
                lblToLocation.Visible = True
                txtToLocation.Visible = True
                RadLabel9.Visible = True
                If chk_againstmonthend.Checked Then
                    txtFromLocation.Enabled = True
                Else
                    txtFromLocation.Enabled = False
                End If
                fndReqNo.MendatroryField = True
                RadLabel2.Text = "Return From"
                RadLabel8.Visible = False
                If chk_againstmonthend.Checked Then
                    RadLabel8.Visible = True
                    txtFromLocation.Visible = True
                    lblFromLocation.Visible = True
                Else
                    txtFromLocation.Visible = False
                    lblFromLocation.Visible = False
                End If
                chkReject.Visible = True
                chkReProcess.Enabled = False
                chkReProcess.Visible = False

                gv1.Columns(colRetQty).IsVisible = True

                MyLabel2.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
                fndPurchaseInvoice.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
                chkAgnstPI.Visible = AutoPurchaseReturn
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "TransferCX") = CompairStringResult.Equal Then
                chk_againstmonthend.Visible = False
                fndcapexsubcode.Enabled = True
                AddNew(False)
                txtTaxGroup.Enabled = False
                lblTaxGrpName.Enabled = False
                lblReq.Text = "Requisition No"
                lblReq.Visible = True
                fndReqNo.Visible = True
                fndReqNo.MendatroryField = False
                lblReqDate.Visible = True
                txtIssueTo.Visible = True
                RadLabel2.Visible = True
                lblIssueTo.Visible = True

                MyLabel2.Visible = False
                fndPurchaseInvoice.Visible = False
                chkAgnstPI.Visible = False
                repoReqIssueNo.HeaderText = "Req. No"
                lblToLocation.Visible = False
                txtToLocation.Visible = False
                RadLabel9.Visible = False
                RadLabel2.Text = "Issue To"
                RadLabel8.Visible = True
                txtFromLocation.Visible = True
                lblFromLocation.Visible = True
                chkReProcess.Enabled = True
                chkReProcess.Visible = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer") = CompairStringResult.Equal Then
                AddNew(False)
                txtTaxGroup.Enabled = True
                lblTaxGrpName.Enabled = True
                lblReq.Visible = False
                fndReqNo.Visible = False
                lblReqDate.Visible = False
                lblToLocation.Visible = True
                txtToLocation.Visible = True
                RadLabel9.Visible = True
                txtFromLocation.Enabled = True
                chkReProcess.Enabled = False
                chkReProcess.Visible = False
            End If
            setRejectVisiblity()
        End If
    End Sub

    Private Sub TxtVehicle__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVehicle._MYValidating
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Seg_No= '2' And Segment_code='" + TxtVehicle.Value + "'"
        'Dim Check As String = "select Segment_code, Description From TSPL_GL_SEGMENT_CODE Where Segment_code='" + TxtVehicle.Value + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Check)
        'If dt.Rows.Count <= 0 And isButtonClicked = False Then
        '    lblVehicleDesc.Text = ""
        'Else
        '    Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        '    Dim WhrCls As String = " seg_no <>'7'  "
        '    TxtVehicle.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, TxtVehicle.Value, "", isButtonClicked)
        '    lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + TxtVehicle.Value + "'")
        'End If

        Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        TxtVehicle.Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", TxtVehicle.Value, "", isButtonClicked)
        lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Cost_name From TSPL_CostCenter_MASTER Where   Cost_Code= '" + TxtVehicle.Value + "'")


    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If clsCommon.myLen(fndReqNo.Value) <= 0 OrElse (AllowOnlyOneIssueAgainstStoreRequisition = True AndAlso clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal) Then
            If gv1.RowCount > 0 Then

                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub fndReqNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndReqNo._MYValidating
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal Then
            'Dim qry As String = "select Requisition_Id as Code,Requisition_Date as Date from TSPL_REQUISITION_HEAD "
            'Dim whrclas As String = "is_internal='Y' AND ISNULL(Requisition_Id,'') Not In (select ISNULL(Req_IssueNo,'') from TSPL_IssueReturn_HEAD)"
            Dim qry As String = ""
            If AllowOnlyOneIssueAgainstStoreRequisition = True Then
                qry = "Select Distinct AA.RequisitionNo ,AA.Date as RequisitionDate from (select TSPL_REQUISITION_HEAD.Requisition_Id as RequisitionNo,Convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date ,103) as Date " &
                  " From TSPL_REQUISITION_HEAD " &
                  " where TSPL_REQUISITION_HEAD.close_yn = 'N' and TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_HEAD.Is_Internal ='Y'    and isnull(TSPL_REQUISITION_HEAD.Capex_SubCode,'')=''  " &
                  " And isnull(TSPL_REQUISITION_HEAD.Capex_Code,'') =''  and TSPL_REQUISITION_HEAD.Requisition_Id not in " &
                  " (select TSPL_IssueReturn_HEAD.RequisitionNo from TSPL_IssueReturn_HEAD " &
                  " Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No " &
                  " where Doc_Type='Issue'  and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 )) AA"
            Else
                qry = " Select Distinct AA.RequisitionNo ,AA.Date as RequisitionDate from " &
                " ( Select MAX(Code) as RequisitionNo,MAX(Date) as Date,MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as RequisitionQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as IssueQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from" &
                " ( Select TSPL_REQUISITION_HEAD.Requisition_Id as Code,Convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date ,103) as Date,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved,1 as RI from  TSPL_REQUISITION_HEAD Left Outer Join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id  where TSPL_REQUISITION_HEAD.close_yn = 'N' and  TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_HEAD.Is_Internal ='Y' " &
                "   and isnull(TSPL_REQUISITION_HEAD.Capex_SubCode,'')='' and isnull(TSPL_REQUISITION_HEAD.Capex_Code,'') ='' " &
                " Union All " &
                " select TSPL_IssueReturn_HEAD.RequisitionNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=1 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 " &
                " Union All " &
                " select TSPL_IssueReturn_HEAD.RequisitionNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,0 as Qty,TSPL_IssueReturn_DETAIL.Issued_Qty  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=0 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" & txtDocNo.Value & "'))" &
                " Final group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA"
            End If

            fndReqNo.Value = clsCommon.ShowSelectForm("Req no", qry, "RequisitionNo", "", fndReqNo.Value, "", isButtonClicked)
            lblReq3.Text = fndReqNo.Value
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = False
                lblReq3.Visible = False

            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
            'Dim qry As String = "select Doc_No as Code,Doc_date as Date from TSPL_IssueReturn_HEAD "
            'Dim whrclas As String = "doc_type='Issue' and Posting_Date <> ''"

            'Dim qry As String = " Select Distinct AA.IssueNo  ,AA.Date as RequisitionDate from  ( Select MAX(Code) as IssueNo,MAX(Date) as Date,MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as IssueQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as ReturnQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from ( " & _
            '" select TSPL_IssueReturn_HEAD.Doc_No  as Code ,convert(varchar,TSPL_IssueReturn_HEAD.Doc_Date ,103) as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where TSPL_IssueReturn_HEAD.Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=1 " & _
            '" Union All " & _
            '" select TSPL_IssueReturn_HEAD.Req_IssueNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=1 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 " & _
            '" Union All " & _
            '" select TSPL_IssueReturn_HEAD.Req_IssueNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,0 as Qty,TSPL_IssueReturn_DETAIL.Issued_Qty  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=0 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" & txtDocNo.Value & "'))" & _
            '" Final group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA"

            Dim qry As String = " Select Distinct AA.IssueNo  ,AA.Date as RequisitionDate,AA.Location_Code as [Location Code],AA.locdesc as [Location Desc] ,AA.Request_By as [Requested By]  from  ( Select MAX(Code) as IssueNo,MAX(Date) as Date,MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as IssueQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as ReturnQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty ,MAX(Location_Code ) AS Location_Code,MAX(Request_By ) AS Request_By,MAX(TSPL_LOCATION_MASTER.Location_Desc ) AS locdesc from ( " &
           " select TSPL_IssueReturn_HEAD.Doc_No  as Code ,convert(varchar,TSPL_IssueReturn_HEAD.Doc_Date ,103) as Date,TSPL_IssueReturn_HEAD.Request_By ,TSPL_IssueReturn_HEAD.From_Location ,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where TSPL_IssueReturn_HEAD.Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=1 " &
           " Union All " &
           " select TSPL_IssueReturn_HEAD.Req_IssueNo as Code,'' as Date,TSPL_IssueReturn_HEAD.Request_By ,TSPL_IssueReturn_HEAD.From_Location ,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=1 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 " &
           " Union All " &
           " select TSPL_IssueReturn_HEAD.Req_IssueNo as Code,'' as Date,TSPL_IssueReturn_HEAD.Request_By ,TSPL_IssueReturn_HEAD.From_Location ,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,0 as Qty,TSPL_IssueReturn_DETAIL.Issued_Qty  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=0 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" & txtDocNo.Value & "'))" &
           " Final  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Final.From_Location group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA"


            fndReqNo.Value = clsCommon.ShowSelectForm("Issue no", qry, "IssueNo", "", fndReqNo.Value, "", isButtonClicked)
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = True
                lblReq3.Visible = True

            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer Store to Capex") = CompairStringResult.Equal Then
            Dim qry As String = "Select Distinct TSPL_REQUISITION_HEAD.Requisition_Id as Code,Convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date ,103) as Date " & _
                " from  TSPL_REQUISITION_HEAD Left Outer Join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id "
            Dim whrcls As String = "TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_HEAD.Is_Internal ='Y' and isnull(TSPL_REQUISITION_HEAD.Capex_SubCode,'')<>'' and isnull(TSPL_REQUISITION_HEAD.Capex_Code,'') <>'' " & _
                " and not exists(select 1 from TSPL_IssueReturn_HEAD where TSPL_IssueReturn_HEAD.Doc_Type='TransferCX' and  TSPL_IssueReturn_HEAD.Req_IssueNo=TSPL_REQUISITION_HEAD.Requisition_Id) "
            fndReqNo.Value = clsCommon.ShowSelectForm("Code", qry, "Code", whrcls, fndReqNo.Value, "", isButtonClicked)
            If clsCommon.myLen(fndReqNo.Value) > 0 Then
                LoadReqDataHead(fndReqNo.Value)
                LoadReqDataDetail(fndReqNo.Value)
                lblReq2.Visible = False
                lblReq3.Visible = False
            End If
        End If

    End Sub

    Private Sub LoadReqDataHead(ByVal strReqNo As String)
        Dim qry As String
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer Store to Capex") = CompairStringResult.Equal Then
            qry = "select Requisition_Date,Location,Location_Desc,Dept,Dept_Desc,Request_By,TSPL_REQUISITION_HEAD.Created_By,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_REQUISITION_HEAD.Capex_Code,TSPL_REQUISITION_HEAD.Capex_SubCode,TSPL_REQUISITION_HEAD.Cost_Center_Unit,TSPL_REQUISITION_HEAD.Cost_Center_Type from TSPL_REQUISITION_HEAD inner join " &
               "TSPL_LOCATION_MASTER on TSPL_REQUISITION_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_REQUISITION_HEAD.Request_By where Requisition_Id='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblReqDate.Text = clsCommon.myCstr(dt.Rows(0)("Requisition_Date"))
                txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
                lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                txtDepartment.Value = clsCommon.myCstr(dt.Rows(0)("Dept"))
                lblDepartment.Text = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
                txtRequestBy.Value = clsCommon.myCstr(dt.Rows(0)("Request_By"))
                lblRequestBy.Text = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
                ''==Sanjeet (Load CAPEX Detail and check it's amount)======
                fndcapexcode.Value = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
                If EnableStoreCostCentre = 1 Then
                    If clsCommon.myLen(Me.txtDepartment.Value) > 0 Then
                        txtDepartment.Enabled = False
                    Else
                        txtDepartment.Enabled = True
                    End If
                End If
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                    fndcapexsubcode.Enabled = False
                End If
                fndcapexsubcode.Value = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                End If

                txtUnitCode.Value = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Unit"))
                If clsCommon.myLen(Me.txtUnitCode.Value) > 0 Then
                    lblUnitDesc.Text = clsUnitMaster.GetName(Me.txtUnitCode.Value)
                    txtUnitCode.Enabled = False
                Else
                    txtUnitCode.Enabled = True
                End If
                txtCostCenterType.Enabled = False
                txtCostCenterType.Value = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Type"))
                If clsCommon.myLen(Me.txtCostCenterType.Value) > 0 Then
                    lblCostcenterTypeDesc.Text = clsCostCenterTypeMaster.GetName(Me.txtCostCenterType.Value)
                    txtCostCenterType.Enabled = False
                Else
                    txtCostCenterType.Enabled = True
                End If
                '======================
                If StoreRequisitionMandatory Then
                    txtIssueTo.Value = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                End If
                If EnableStoreCostCentre = 1 Then
                    txtCostCenterType.Enabled = True
                    txtUnitCode.Enabled = True
                End If
            End If
        Else
            qry = "SELECT TSPL_IssueReturn_HEAD.Doc_Date,TSPL_IssueReturn_HEAD.From_Location,FLocation.Location_Desc as FromLocationName, " & _
            "TSPL_IssueReturn_HEAD.To_Location,TLocation.Location_Desc as ToLocationName,TSPL_IssueReturn_HEAD.Issue_To, " & _
            "IssueEmp.Emp_Name as IssueToName ,TSPL_IssueReturn_HEAD.Request_By,RequestEmp.Emp_Name as RequestByName,TSPL_IssueReturn_HEAD.vehicle_Id,Req_IssueNo," & _
            " TSPL_IssueReturn_HEAD.Dept,TSPL_IssueReturn_HEAD.Dept_Desc,TSPL_IssueReturn_HEAD.Capex_Code,TSPL_IssueReturn_HEAD.Capex_SubCode ,TSPL_IssueReturn_HEAD.Cost_Center_Unit,TSPL_IssueReturn_HEAD.Cost_Center_Type " & _
            "FROM TSPL_IssueReturn_HEAD left outer join TSPL_LOCATION_MASTER as FLocation on  " & _
            "FLocation.Location_Code=TSPL_IssueReturn_HEAD.From_Location left outer join TSPL_LOCATION_MASTER as TLocation on " & _
            "TLocation.Location_Code=TSPL_IssueReturn_HEAD.To_Location left outer join TSPL_EMPLOYEE_MASTER as IssueEmp on " & _
            "IssueEmp.EMP_CODE= TSPL_IssueReturn_HEAD.issue_To left outer join  " & _
            "TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_IssueReturn_HEAD.Request_By where TSPL_IssueReturn_HEAD.doc_no='" & fndReqNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtDate.Value = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
                ''richa 11/11/2014
                'txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("To_Location"))
                'lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("ToLocationName"))
                'txtToLocation.Value = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                'lblToLocation.Text = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
                txtFromLocation.Value = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                lblFromLocation.Text = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
                ''RICHA AGARWAL 20/04/2015 AGAINST TICKET NO.BM00000006288
                txtToLocation.Value = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                lblToLocation.Text = clsCommon.myCstr(dt.Rows(0)("FromLocationName"))
                '-----------------------------------

                txtIssueTo.Value = clsCommon.myCstr(dt.Rows(0)("Issue_To"))
                lblIssueTo.Text = clsCommon.myCstr(dt.Rows(0)("IssueToName"))
                txtRequestBy.Value = clsCommon.myCstr(dt.Rows(0)("Request_By"))
                lblRequestBy.Text = clsCommon.myCstr(dt.Rows(0)("RequestByName"))
                TxtVehicle.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_Id"))
                lblReq3.Text = clsCommon.myCstr(dt.Rows(0)("Req_IssueNo"))
                ''==Sanjeet (Load CAPEX Detail and check it's amount)======
                txtDepartment.Value = clsCommon.myCstr(dt.Rows(0)("Dept"))
                lblDepartment.Text = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
                If EnableStoreCostCentre = 1 Then
                    If clsCommon.myLen(Me.txtDepartment.Value) > 0 Then
                        txtDepartment.Enabled = False
                    Else
                        txtDepartment.Enabled = True
                    End If
                End If

                fndcapexcode.Value = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                    fndcapexsubcode.Enabled = False
                End If
                fndcapexsubcode.Value = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
                If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                    lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                    lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                    lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                    lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                End If
                txtUnitCode.Value = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Unit"))
                If clsCommon.myLen(Me.txtUnitCode.Value) > 0 Then
                    lblUnitDesc.Text = clsUnitMaster.GetName(Me.txtUnitCode.Value)
                    txtUnitCode.Enabled = False
                Else
                    txtUnitCode.Enabled = True
                End If
                txtCostCenterType.Value = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Type"))
                If clsCommon.myLen(Me.txtCostCenterType.Value) > 0 Then
                    lblCostcenterTypeDesc.Text = clsCostCenterTypeMaster.GetName(Me.txtCostCenterType.Value)
                    txtCostCenterType.Enabled = False
                Else
                    txtCostCenterType.Enabled = True
                End If
                If EnableStoreCostCentre = 1 Then
                    txtCostCenterType.Enabled = True
                    txtUnitCode.Enabled = True
                End If
                '======================
            End If

        End If

    End Sub

    Private Sub LoadReqDataDetail(ByVal strReqNo As String)
        'gv1.Rows.Clear()
        LoadBlankGrid()
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Issue") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Transfer Store to Capex") = CompairStringResult.Equal Then
            'Dim Qry As String = "select Item_Code,Item_Cost,Item_Desc,Unit_Code,Requisition_Qty - (select isnull(SUM(Issued_Qty),0) from TSPL_IssueReturn_HEAD inner join " & _
            '"TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No and " & _
            '"TSPL_IssueReturn_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Issue') + " & _
            '"(select isnull(SUM(Issued_Qty),0) from TSPL_IssueReturn_HEAD inner join TSPL_IssueReturn_DETAIL on " & _
            '"TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No and " & _
            '"TSPL_IssueReturn_HEAD.RequisitionNo=TSPL_REQUISITION_DETAIL.Requisition_Id where Doc_Type='Return') as Requisition_Qty  " & _
            '"from TSPL_REQUISITION_DETAIL where Requisition_Id='" & fndReqNo.Value & "'"
            Dim qry As String = " Select  AA.RequisitionNo ,AA.Date as RequisitionDate,AA.Item_Code,AA.Item_Desc,AA.Unit_code,AA.RequisitionQty,AA.PendingQty,AA.[Cost Code],AA.Hirerachy_Code,AA.Cost_Centre_Code,AA.Hirerachy_Level3,AA.Hirerachy_Level4  from " &
           " ( Select MAX(Code) as RequisitionNo,max(Item_Code) as Item_Code ,max(Item_Desc) as Item_Desc ,MAX(Date) as Date,MAX(Unit_code) as [Unit_code],SUM(Qty* case when RI=1 then 1 else 0 end) as RequisitionQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as IssueQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,max(cost_Code) as [Cost Code],MAX(Hirerachy_Code) AS Hirerachy_Code,MAX(Cost_Centre_Code) AS Cost_Centre_Code,max(Hirerachy_Level3) as Hirerachy_Level3,max(Hirerachy_Level4) as Hirerachy_Level4 from" &
           " ( Select isnull(TSPL_REQUISITION_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_REQUISITION_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_REQUISITION_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_REQUISITION_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_REQUISITION_HEAD.Requisition_Id as Code,Convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date ,103) as Date,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved,1 as RI,TSPL_REQUISITION_DETAIL.Cost_Code from  TSPL_REQUISITION_HEAD Left Outer Join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id  where   TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_HEAD.Is_Internal ='Y' " &
           " Union All " &
           " select isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_IssueReturn_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_IssueReturn_HEAD.RequisitionNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,-1 as RI,TSPL_IssueReturn_DETAIL.Cost_Code   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=1 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 " &
           " Union All " &
           " select isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_IssueReturn_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_IssueReturn_HEAD.RequisitionNo as Code,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,0 as Qty,TSPL_IssueReturn_DETAIL.Issued_Qty  as Unapproved ,-1 as RI,TSPL_IssueReturn_DETAIL.Cost_Code   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=0 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" & txtDocNo.Value & "'))" &
           " Final where Final.Code ='" & fndReqNo.Value & "' group by Code,Item_Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    ''richa agarwal 1 Dec ,2016
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = clsCommon.myCstr(dr("Hirerachy_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = clsCommon.myCstr(dr("Cost_Centre_Code"))

                        If EnableHirerachyCostCentre = 1 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dr("Hirerachy_Level3"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dr("Hirerachy_Level4"))
                            OpenCostCentreLevelName(False)
                        End If
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Cost Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(clsCommon.myCstr(dr("Cost Code")))
                    End If

                    ''RICHA AGARWAL 20/04/2015 AGAINST TICKET NO.BM00000006288
                    gv1.Columns(colReqQty).IsVisible = True
                    gv1.Columns(colRetQty).IsVisible = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = "0"
                    ''------------------------------------
                    ''richa agarwal
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = False


                    setCost(gv1.Rows.Count - 1)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = Nothing

                    '' added by parteek for Item Rack Bin wise on 21/11/2017
                    If EnableRackBin Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' and location='" & txtFromLocation.Value & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' and location='" & txtFromLocation.Value & "'")
                    End If
                    '' End
                Next
                If AllowOnlyOneIssueAgainstStoreRequisition = True Then
                    gv1.Rows.AddNew()
                End If
            End If
        Else
            ' Dim Qry As String = "select Item_Code,Item_Desc,Unit_code,Required_Qty,Issued_Qty,Cost_Code,Unit_Cost from TSPL_IssueReturn_DETAIL where Doc_No='" & fndReqNo.Value & "'"
            Dim Qry As String = " Select AA.Hirerachy_Code,AA.Cost_Centre_Code,AA.Hirerachy_Level3,AA.Hirerachy_Level4,AA.IssueNo,AA.Unit_Cost,AA.Cost_Code,AA.Date as RequisitionDate,AA.Item_Code,AA.Item_Desc,AA.Unit_code,AA.IssueQty,AA.PendingQty from  ( Select MAX(Hirerachy_Code) AS Hirerachy_Code,MAX(Cost_Centre_Code) AS Cost_Centre_Code,max(Hirerachy_Level3) as Hirerachy_Level3,max(Hirerachy_Level4) as Hirerachy_Level4,MAx(Cost_Code) as Cost_Code,Max(Unit_Cost) as Unit_Cost,MAX(Code) as IssueNo,max(Item_Code) as Item_Code ,max(Item_Desc) as Item_Desc,MAX(Date) as Date,MAX(Unit_code) as [Unit_code],SUM(Qty* case when RI=1 then 1 else 0 end) as IssueQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as ReturnQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from ( " & _
            " select isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_IssueReturn_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_IssueReturn_HEAD.Doc_No  as Code ,TSPL_IssueReturn_DETAIL.Unit_Cost,TSPL_IssueReturn_DETAIL.Cost_Code ,convert(varchar,TSPL_IssueReturn_HEAD.Doc_Date ,103) as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where TSPL_IssueReturn_HEAD.Doc_Type='Issue' and TSPL_IssueReturn_HEAD.Status=1 " & _
            " Union All " & _
            " select isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_IssueReturn_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_IssueReturn_HEAD.Req_IssueNo as Code,TSPL_IssueReturn_DETAIL.Unit_Cost,TSPL_IssueReturn_DETAIL.Cost_Code ,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,0  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=1 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 " & _
            " Union All " & _
            " select isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Code,'') as Hirerachy_Code,isnull(TSPL_IssueReturn_DETAIL.Cost_Centre_Code,'') as Cost_Centre_Code,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level3,'') as Hirerachy_Level3 ,isnull(TSPL_IssueReturn_DETAIL.Hirerachy_Level4,'') as Hirerachy_Level4 ,TSPL_IssueReturn_HEAD.Req_IssueNo as Code,TSPL_IssueReturn_DETAIL.Unit_Cost,TSPL_IssueReturn_DETAIL.Cost_Code ,'' as Date,TSPL_IssueReturn_DETAIL.Item_Code ,TSPL_IssueReturn_DETAIL.Item_Desc ,TSPL_IssueReturn_DETAIL.Unit_code,0 as Qty,TSPL_IssueReturn_DETAIL.Issued_Qty  as Unapproved ,-1 as RI   from TSPL_IssueReturn_HEAD Left Outer Join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_HEAD.Doc_No =TSPL_IssueReturn_DETAIL.Doc_No  where Doc_Type='Return' and TSPL_IssueReturn_HEAD.Status=0 and len(isnull(TSPL_IssueReturn_DETAIL.Doc_No ,''))>0 and TSPL_IssueReturn_DETAIL.Doc_No not in ('" & txtDocNo.Value & "'))" & _
            " Final where Final.Code ='" & fndReqNo.Value & "' group by Code,Item_Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = clsCommon.myCstr(strReqNo)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    ''RICHA AGARWAL 20/04/2015 AGAINST TICKET NO.BM00000006288
                    gv1.Columns(colReqQty).IsVisible = False
                    gv1.Columns(colRetQty).IsVisible = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = "0"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = clsCommon.myCstr(dr("PendingQty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(dr("PendingQty"))
                    ''------------------------------------

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True

                    ''richa agarwal 1 Dec ,2016
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = clsCommon.myCstr(dr("Hirerachy_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = clsCommon.myCstr(dr("Cost_Centre_Code"))
                        If EnableHirerachyCostCentre = 1 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dr("Hirerachy_Level3"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dr("Hirerachy_Level4"))
                            OpenCostCentreLevelName(False)
                        End If
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Cost_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(clsCommon.myCstr(dr("Cost_Code")))
                    End If
                    ''--------------

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Unit_Cost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(dr("Unit_Cost")) * clsCommon.myCstr(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(dr("Item_Code"))
                    'gv1.Rows(gv1.Rows.Count - 1).Tag = clsSerializeInvenotry.GetData("ISSTRAN", objTr.Doc_No, objTr.Item_Code, objTr.Line_No, trans)


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = Nothing
                    '' added by parteek for Item Rack Bin wise on 21/11/2017
                    If EnableRackBin Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBin).Value = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' and location='" & txtFromLocation.Value & "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRack).Value = clsDBFuncationality.getSingleValue("select Rack_Code from TSPL_ITEM_RACK_BIN_MAPPING where Item_Code='" & clsCommon.myCstr(dr("Item_Code")) & "' and location='" & txtFromLocation.Value & "'")
                    End If
                    '' End
                Next
            End If
        End If
    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = -1
        UcItemBalance1.LocationCode = txtFromLocation.Value
        UcItemBalance1.LocationName = lblFromLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.CommitedQty = True
        UcItemBalance1.CommitedQtyLbl = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

    Private Sub chkWithoutRefNo_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkWithoutRefNo.ToggleStateChanged
        chkWithoutRefNoChanged()
    End Sub

    Private Sub chkWithoutRefNoChanged()
        gv1.Columns(colReq_IssueNo).IsVisible = Not chkWithoutRefNo.Checked
        gv1.Columns(colReqQty).IsVisible = Not chkWithoutRefNo.Checked
        gv1.Columns(colQty).IsVisible = Not chkWithoutRefNo.Checked
        fndReqNo.Enabled = Not chkWithoutRefNo.Checked
        txtIssueTo.Enabled = Not chkWithoutRefNo.Checked
        If chkWithoutRefNo.Checked Then
            fndReqNo.Value = ""
            txtIssueTo.Value = ""
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsIssueReturnHead.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse and Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
        If e.KeyCode = Keys.F5 Then
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
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub
    Private Sub UcItemBalance1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UcItemBalance1.Load

    End Sub

    Private Sub gv1_Pasting(sender As Object, e As GridViewClipboardEventArgs) Handles gv1.Pasting
        gv1.CurrentCell.Value = e.DataObject.GetText()
    End Sub

    Private Sub txtDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDepartment._MYValidating
        Try
            Dim obj As clsDepartment = clsDepartment.Finder(txtDepartment.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtDepartment.Value = obj.Code
                lblDepartment.Text = obj.Name
                ''=======For Blank all Cost Centre value in Department Changed============
                txtUnitCode.Value = ""
                lblUnitDesc.Text = ""
                txtCostCenterType.Value = ""
                lblCostcenterTypeDesc.Text = ""
                ''===============================
            Else
                txtDepartment.Value = ""
                lblDepartment.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkReject_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkReject.ToggleStateChanged
        setRejectVisiblity()
    End Sub

    Sub setRejectVisiblity()
        MyLabel6.Visible = False
        txtVendor.Visible = False
        lblVendor.Visible = False
        If (chkReject.Checked And chkReject.Visible) OrElse (chkAgnstPI.Visible AndAlso chkAgnstPI.Checked) Then
            MyLabel6.Visible = True
            txtVendor.Visible = True
            lblVendor.Visible = True
        End If

        MyLabel2.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        fndPurchaseInvoice.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
    End Sub

    Private Sub txtVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendor._MYValidating
        txtVendor.Value = clsVendorMaster.getFinder("", txtVendor.Value, isButtonClicked)
        lblVendor.Text = clsVendorMaster.GetName(txtVendor.Value, Nothing)
    End Sub

#Region "Purchase Return"
    Private Sub SetPurchaseInvoice()
        Try
            isInsideLoadData = True
            If Not chkAgnstPI.Checked Then
                Throw New Exception("First select Against Purchase Invoice then select document.")
            End If

            Dim frm As New frmPendingPI()
            frm.VendorCode = txtVendor.Value
            frm.strCurrCode = txtDocNo.Value
            frm.isRejectedItem = chkReject.Checked
            frm.ShowDialog()

            Dim Pi_Icode As String = ""
            Dim Icode As String = ""
            Dim arrIcode As New ArrayList()

            Dim objMRNHead As clsPurchaseInvoiceHead = Nothing
            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                objMRNHead = clsPurchaseInvoiceHead.GetData(frm.ArrReturn(0).PI_No, NavigatorType.Current, "")

                If objMRNHead IsNot Nothing AndAlso clsCommon.myLen(objMRNHead.PI_No) > 0 Then
                    txtFromLocation.Value = objMRNHead.Bill_To_Location
                    lblFromLocation.Text = objMRNHead.BillToLocationName
                    txtToLocation.Value = objMRNHead.Bill_To_Location
                    lblToLocation.Text = objMRNHead.BillToLocationName

                    txtToLocation.Enabled = False
                    '-----------------------------------
                    fndPurchaseInvoice.Value = objMRNHead.PI_No
                    txtComment.Text = objMRNHead.Comments
                    txtRemarks.Text = objMRNHead.Remarks
                    txtVendor.Value = objMRNHead.Vendor_Code
                    lblVendor.Text = objMRNHead.Vendor_Name
                    txtTaxGroup.Value = objMRNHead.Tax_Group
                    lblTaxGrpName.Text = objMRNHead.TaxGroupName
                End If ''head part

                gv1.Columns(colPINo).IsVisible = True
                gv1.Columns(colPINo).VisibleInColumnChooser = True
                gv1.Columns(colPIPendingQty).IsVisible = True
                gv1.Columns(colPIPendingQty).VisibleInColumnChooser = True
                gv1.Columns(colQty).IsVisible = False
                gv1.Columns(colRetQty).IsVisible = True
                gv1.Columns(colRetQty).VisibleInColumnChooser = True
                gv1.Rows.Clear()

                If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                    For Each obj As clsPurchaseInvoiceDetail In frm.ArrReturn
                        If IsValidItem(obj) Then
                            gv1.Rows.AddNew()

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colReq_IssueNo).Value = Nothing
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = "0"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = obj.Balance_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = "0"
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = Nothing
                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = Nothing
                            End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Item_Cost
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = obj.Balance_Qty * obj.Item_Cost
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(obj.Item_Code)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code, Nothing)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = Nothing

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPINo).Value = obj.PI_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPIPendingQty).Value = obj.Balance_Qty

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPOID).Value = obj.PO_ID
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colGRNID).Value = obj.GRN_ID
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRNID).Value = obj.MRN_ID
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNID).Value = obj.SRN_Id

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
                        End If ''check valid item or not
                    Next ''end arraygrid
                End If

                SetitemWiseTaxSetting(False, False)
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
                RefreshPINo()
            End If ''end array cond.
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub RefreshPINo()
        fndPurchaseInvoice.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    fndPurchaseInvoice.Value = clsCommon.myCstr(strReqNo)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Function IsValidItem(ByVal obj As clsPurchaseInvoiceDetail)
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Value = obj.PITax_Group
            SetTaxDetails()
        Else
            SetTaxDetails()
        End If
        If Not clsCommon.CompairString(txtTaxGroup.Value, obj.PITax_Group) = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " SRN No: " + obj.PI_No + "  contain Tax Group :" + obj.PITax_Group + Environment.NewLine)
            Return False
        End If
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colPINo).Value)

            Dim strPOCode As String = Nothing
            Dim strGRNCode As String = Nothing
            Dim strMRNCode As String = Nothing
            Dim strSRNCode As String = Nothing
            If clsCommon.myLen(strReqCode) > 0 Then
                strSRNCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_SRN FROM TSPL_PI_HEAD WHERE PI_No='" + strReqCode + "'"))
            End If
            If clsCommon.myLen(strSRNCode) > 0 Then
                strMRNCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_MRN FROM TSPL_SRN_HEAD WHERE SRN_No='" + strSRNCode + "'"))
            End If
            If clsCommon.myLen(strMRNCode) > 0 Then
                strGRNCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + strMRNCode + "'"))
            End If
            If clsCommon.myLen(strGRNCode) > 0 Then
                strPOCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + strGRNCode + "'"))
            End If


            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strPOCode, obj.PO_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strGRNCode, obj.GRN_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMRNCode, obj.MRN_ID) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strSRNCode, obj.SRN_Id) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.PI_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Purchase Invoice No : " + obj.PI_No + "  Item : " + obj.Item_Desc + ""
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub fndPurchaseInvoice__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPurchaseInvoice._MYValidating
        SetPurchaseInvoice()
    End Sub
#End Region

    Private Sub chkAgnstPI_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAgnstPI.ToggleStateChanged
        MyLabel2.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        fndPurchaseInvoice.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        fndPurchaseInvoice.MendatroryField = True
        fndPurchaseInvoice.Enabled = chkAgnstPI.Checked
        MyLabel6.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        txtVendor.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        lblVendor.Visible = AutoPurchaseReturn AndAlso ((chkReject.Visible AndAlso chkReject.Checked) OrElse chkAgnstPI.Checked)
        'RadLabel8.Visible = AutoPurchaseReturn AndAlso chkAgnstPI.Checked
        'txtFromLocation.Visible = AutoPurchaseReturn AndAlso chkAgnstPI.Checked
        'lblFromLocation.Visible = AutoPurchaseReturn AndAlso chkAgnstPI.Checked
        'txtFromLocation.Enabled = AutoPurchaseReturn AndAlso chkAgnstPI.Checked
    End Sub

    Private Sub chkReProcess_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkReProcess.ToggleStateChanged
        If StoreRequisitionMandatory AndAlso chkReProcess.Checked = False Then
            fndReqNo.MendatroryField = True
        Else
            fndReqNo.MendatroryField = False
        End If
    End Sub

    Private Sub chk_againstmonthend_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_againstmonthend.ToggleStateChanged
        If chk_againstmonthend.Checked Then
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "Return") = CompairStringResult.Equal Then
                RadLabel8.Visible = True
                txtFromLocation.Enabled = True
                txtFromLocation.Visible = True
                lblFromLocation.Visible = True
                fndReqNo.Enabled = False
            Else
                RadLabel8.Visible = False
                txtFromLocation.Enabled = True
                txtFromLocation.Visible = False
                lblFromLocation.Visible = False
                fndReqNo.Enabled = True
            End If
        Else
            RadLabel8.Visible = False
            txtFromLocation.Enabled = True
            txtFromLocation.Visible = False
            lblFromLocation.Visible = False
            fndReqNo.Enabled = True
        End If

    End Sub

    Private Sub fndcapexsubcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcapexsubcode._MYValidating
        Try
            lbl_capexcode.Text = ""
            fndcapexcode.Value = ""
            Me.fndcapexsubcode.Value = clsCapexBudget.getFinder("", fndcapexsubcode.Value, isButtonClicked)
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                fndcapexcode.Value = clsCapexBudget.GetCapexCode(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, txtDocNo.Value, Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtUnitCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUnitCode._MYValidating
        Try
            Dim obj As clsUnitMaster = clsUnitMaster.Finder(txtUnitCode.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtUnitCode.Value = obj.Code
                lblUnitDesc.Text = obj.Description
            Else
                txtUnitCode.Value = ""
                lblUnitDesc.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCostCenterType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostCenterType._MYValidating
        Try
            Dim obj As clsCostCenterTypeMaster = clsCostCenterTypeMaster.Finder(txtCostCenterType.Value, isButtonClicked, txtDepartment.Value)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtCostCenterType.Value = obj.Code
                lblCostcenterTypeDesc.Text = obj.Description
            Else
                txtCostCenterType.Value = ""
                lblCostcenterTypeDesc.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub Btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Do you want to cancel the Document?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsIssueReturnHead.CancelData(Me.Form_ID, txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Issue/Return cancelled successfully!", Me.Text)
                        AddNew(True)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click
        Dim ShowCostCenterAndHierarchyLevelInPurchaseModule As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)) = 1, True, False)

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + txtDocNo.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim coll As New Hashtable()

                Dim strGLAccountCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select GL_Account from TSPL_IssueReturn_DETAIL where Doc_No = '" + txtDocNo.Value + "' and Item_Code = '" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and Unit_code='" + clsCommon.myCstr(grow.Cells(colUnit).Value) + "'", trans))
                If clsCommon.myLen(strGLAccountCode) > 0 Then
                    'Dim strGLAccountCode As String = clsCommon.myCstr(grow.Cells(colACCode).Value)
                    'clsCommon.AddColumnsForChange(coll, "GL_Account_Code", clsCommon.myCstr(grow.Cells(colACCode).Value))
                    'clsCommon.AddColumnsForChange(coll, "Detail_Line_No", clsCommon.myCstr(lineNo))
                    If ShowCostCenterAndHierarchyLevelInPurchaseModule = True Then
                        clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", clsCommon.myCstr(grow.Cells(colHierarchyCode).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", clsCommon.myCstr(grow.Cells(colCostCenterCode).Value), True)
                        If EnableHirerachyCostCentre = 1 Then
                            clsCommon.AddColumnsForChange(coll, "Hirerachy_Code3", clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value), True)
                            clsCommon.AddColumnsForChange(coll, "Hirerachy_Code4", clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value), True)
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Cost_Code", clsCommon.myCstr(grow.Cells(colCCCode).Value), True)
                    End If





                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_IssueReturn_DETAIL", OMInsertOrUpdate.Update, "Doc_No='" + txtDocNo.Value + "' and Item_Code = '" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and  Unit_code = '" + clsCommon.myCstr(grow.Cells(colUnit).Value) + "' and GL_Account = '" + strGLAccountCode + "'", trans)
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No = '" + txtDocNo.Value + "'  ", trans))
                    Dim qry As String = ""
                    If ShowCostCenterAndHierarchyLevelInPurchaseModule = True Then
                        qry = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCostCenterCode).Value) + "' "
                        If EnableHirerachyCostCentre = 1 Then
                            qry += " ,Hirerachy_Code3= " + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value) & "' ", "NULL") + ",Hirerachy_Code4=" + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value) & "' ", "NULL") + "   "
                        End If
                        qry += " WHERE Voucher_No='" + strVoucherNo + "' and Account_code='" + strGLAccountCode + "' "

                    Else
                        'qry = "update TSPL_JOURNAL_DETAILS SET Hirerachy_Code='" + clsCommon.myCstr(grow.Cells(colHierarchyCode).Value) + "',Cost_Code='" + clsCommon.myCstr(grow.Cells(colCCCode).Value) + "',Hirerachy_Code3= " + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value) & "' ", "NULL") + ",Hirerachy_Code4=" + IIf(clsCommon.myLen(clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)) > 0, " '" & clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value) & "' ", "NULL") + " WHERE Voucher_No='" + strVoucherNo + "' and Account_code='" + strGLAccountCode + "' "
                        qry = "update TSPL_JOURNAL_DETAILS SET Cost_Centre_Code='" + clsCommon.myCstr(grow.Cells(colCCCode).Value) + "' WHERE Voucher_No='" + strVoucherNo + "' and Account_code='" + strGLAccountCode + "' "

                    End If

                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Next
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtDocNo.Value, "Doc_No", "TSPL_IssueReturn_HEAD", "TSPL_IssueReturn_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnJE_Click(sender As Object, e As EventArgs) Handles btnJE.Click
        ShowJE(MyBase.Form_ID, txtDocNo.Value)
    End Sub
End Class
