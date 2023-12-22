Imports System.Data.SqlClient
Imports System.IO
Public Class FrmItemMasterRMOther
    Inherits FrmMainTranScreen
    ''check In Prabhakar 19/06/2020
#Region "Variables"
    Dim strWeightImp As String = Nothing
    Dim dblWeightImp As Double = 0
    Dim AllowTo As Boolean = False
    Dim AllowFinishGoodAsBatchItem As Boolean = False
    Dim ToleranceMandatoryFor_RM_Other_Trade As Boolean = False
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Dim ShelfLifeManadatoryOnFG As Boolean = False
    Dim IndustryType As String = Nothing
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim QrySheet As String
    Dim qry As String
    Const UOMColQty As String = "Uom Qty"
    Const UOMColUnit As String = "Unit Code"
    Const UOMColEqual As String = "Unit Equal"
    Const UOMColUnitDesc As String = "Unit Description"
    Const UOMColConvFact As String = "Conversion Factor"
    Const UOMColStockUnit As String = "STOCKUNIT"
    Const UOMColConvUom As String = "Conv Uom"
    Const UOMColStockUnitChangable As String = "STOCKUNITCHG"
    Const UOMColStockUnitChangable2 As String = "STOCKUNITCHG2"
    Const UOMDefault As String = "UOMDefault"
    Const UOMPieces As String = "UOMPieces"
    Const UOMGrossWeight As String = "GrossWeight"
    Const UOMNetWeight As String = "UOMNetWeight"
    Const UOMJobWorkRate As String = "UOMJobWorkRate"
    Const UOMItemCost As String = "UOMItemCost"
    Const UOMCustomConversion As String = "UOMCustomConversion"


    Dim btntooltip As ToolTip = New ToolTip()

    Const CatcolCode As String = "CatcolCode"
    Const CatcolCodeDesc As String = "CatcolCodeDesc"
    Const CatcolValue As String = "CatcolValue"
    Const CatcolValueDesc As String = "CatcolValueDesc"
    Const CatBinNo As String = "CatBinNo"
    Const Catcolmaster As String = "Catcolmaster"
    Const CatcolSKU As String = "CatcolSKU"

    Const colparamCode As String = "Code"
    Const colparamDesc As String = "Description"
    Const colparamnature As String = "Nature"
    Const colparamLower As String = "Lower Range"
    Const colparamUpper As String = "Upper Range"
    Const colparamDate As String = "Effective Date"
    Const colparamStatus As String = "Status"
    Const colparamValue1 As String = "Value1"
    Const colparamValue2 As String = "Value2"
    Const colActualRange As String = "Actualrange"
    Const colActualvalue As String = "Actualvalue"
    Const colSTDRate As String = "STDRate"
    Const colActualstatus As String = "Actualstatus"

    Const colPurQCParamSNo As String = "colPurQCParamSNo"
    Const colPurQCParamCode As String = "colPurQCParamCode"
    Const colPurQCParamDesc As String = "colPurQCParamDesc"
    Const colPurQCParamSpecification As String = "colPurQCParamSpecification"


    Const ColScheduleSNo As String = "ColScheduleSNo"
    Const ColScheduleDays As String = "ColScheduleDays"
    Const ColSchedulePerQty As String = "ColSchedulePerQty"
    Const ColSchedulePerShort As String = "ColSchedulePerShort"
    Const ColScheduleLateDays As String = "ColScheduleLateDays"

    Dim File_Name As String = ""
    Dim CreateGLAccToItem As Boolean
    Dim SHowCheckBoxChangeRateOnDiaryDispatch As Integer = 0
    Dim IsFormLoaded As Boolean = False
    Dim ItemMasterPostedData As Boolean
    Dim AllowGSTApplicable As Boolean = False
    '=======================Added by preeti gupta Against Ticket No[ADV/17/05/18-000032]======================
    Dim AllowDoNotShowDairyTypeItems As Boolean = False
    '======================================================================================
    Dim AllowItemConversionAutomation As Integer = 0
    Dim SelectUOMTab As Boolean = False
    Dim colIndex As Integer = 0
    Dim isCellValueChangedOpenCat As Boolean = False
    Dim SettItemWiseQualityCheckInGeneralPurchase As Boolean = False
    Dim UpdateItemMasterConversationWithoutValidation As Boolean = False
    Dim AllowDuplicateItemShortDescriptionInItemMaster As Boolean = False
    Dim OneTimeCheck As Boolean = False
#End Region
    Private Sub FrmItemMasterRMOther_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowDuplicateItemShortDescriptionInItemMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDuplicateItemShortDescriptionInItemMaster, clsFixedParameterCode.AllowDuplicateItemShortDescriptionInItemMaster, Nothing)) = 1, True, False)
        AllowItemConversionAutomation = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemConversionAutomation, clsFixedParameterCode.AllowItemConversionAutomation, Nothing))
        AllowDoNotShowDairyTypeItems = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotShowDairyTypeItems, clsFixedParameterCode.DoNotShowDairyTypeItems, Nothing)) = 1, True, False)
        SettItemWiseQualityCheckInGeneralPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1)
        SplitContainer2.Panel2Collapsed = Not SettItemWiseQualityCheckInGeneralPurchase

        SetUserMgmtNew()
        SetLength()

        AllowFinishGoodAsBatchItem = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickFinishedItemasBatchItem, clsFixedParameterCode.PickFinishedItemasBatchItem, Nothing)) = "1", True, False)
        ToleranceMandatoryFor_RM_Other_Trade = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ToleranceFixFor_RM_OT_TRADE, clsFixedParameterCode.ToleranceFixFor_RM_OT_TRADE, Nothing)) = "1", True, False)

        ''====================27/06/2016=====If user have csa module rights and item-wise accounting is ON then panel is visible========================================================
        Dim qry As String = "select isavailable from TSPL_MODULE_PERMISSION where module_name='" + clsUserMgtCode.ModuleCSASale + "'"
        CSA_Panel.Visible = False
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, Nothing)), "1") = CompairStringResult.Equal Then
            CSA_Panel.Visible = True
        End If
        '==========================================================================
        UpdateItemMasterConversationWithoutValidation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UpdateItemMasterConversationWithoutValidation, clsFixedParameterCode.UpdateItemMasterConversationWithoutValidation, Nothing)) = 1, True, False)
        If UpdateItemMasterConversationWithoutValidation = True Then
            chkDoNotCheckOnSave.Visible = True
        End If
        '============================================================================
        Panel2.Visible = False
        RadPageViewPage7.Item.Visibility = ElementVisibility.Collapsed

        IndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            Panel2.Visible = True
            RadPageViewPage7.Item.Visibility = ElementVisibility.Visible
        End If
        '=======================================================================
        CreateGLAccToItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGLAccToItem, clsFixedParameterCode.CreateGLAccToItem, Nothing)) = 1, True, False)
        ItemStructureMandatoryOnWeightConversion = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False)
        SHowCheckBoxChangeRateOnDiaryDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionOnItemMasterChangeItemRate, clsFixedParameterCode.ShowOptionOnItemMasterChangeItemRate, Nothing))
        ShelfLifeManadatoryOnFG = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowShelfLifeMandatoryOnFG, clsFixedParameterCode.AllowShelfLifeMandatoryOnFG, Nothing)) = 1, True, False)
        If SHowCheckBoxChangeRateOnDiaryDispatch = 1 Then
            chkChangeRate.Visible = True
        Else
            chkChangeRate.Visible = False
        End If
        If clsCommon.CompairString(objCommonVar.CurrentIndustryType, "D") <> CompairStringResult.Equal Then
            chkChilledFreezen.Visible = False
            chkMilkPouch.Visible = False
            'lblCSaType.Visible = False
            'cboCSAType.Visible = False
        End If
        btntooltip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnClose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnNew, "Press Alt+N Adding New Transaction")
        LoadType()
        LoadItemType()
        LoadItemSubType()
        BlankAllConrols()
        LoadDatabase()
        LoadBlankGridUOM()
        LoadBlankGridSchedule()
        LoadBlankGridCat()
        LoadItemProductType()
        LoadUsedas()
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing)), "A") = CompairStringResult.Equal Then
            lblWarranty.Visible = True
            CmbWarrApp.Visible = True
            LoadWarrantyDate()
        Else
            LblWarrDate.Visible = False
            CmbWarrApp.Visible = False
        End If

        ''-----------Jakson---done by Parteek----------''
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing)), "D") = CompairStringResult.Equal Then
            MyLabel21.Visible = True
            fndProductType.Visible = True
        Else
            MyLabel21.Visible = False
            fndProductType.Visible = False
        End If
        ''-----------------------End--------''

        RadPageView1.SelectedPage = RadPageViewPage1
        MyLabelL.Visible = False
        txtBmBdQty.Visible = False
        '' Anubhooti 10-Oct-2014
        'RadPageView1.Pages("pageviewSerializedIinvenoty").Item.Visibility = ElementVisibility.Collapsed
        'If objCommonVar.IsDemoERP Then
        '    RadPageView1.Pages("pageviewSerializedIinvenoty").Item.Visibility = ElementVisibility.Visible
        'End If

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        AddNew()
        ''End of For Custom Fields
        applyNLevelCategorySetting()
        'Me.cboCSAType.Visible = False
        LoadItemCSAType()
        '' Anubhooti 10-Sep-2014 BM00000003847
        ItemShortDesp()
        If CreateGLAccToItem = True Then
            rdlblGLAcc.Visible = True
            fndGLAcc.Visible = True
            If clsCommon.myLen(fndGLAcc.Value) > 0 Then
                LblGLAcc.Visible = True
            Else
                LblGLAcc.Visible = False
            End If

        Else
            rdlblGLAcc.Visible = False
            fndGLAcc.Visible = False
            LblGLAcc.Visible = False
        End If
        IsFormLoaded = True
        AllowGSTApplicable = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = 1, True, False)
        If AllowGSTApplicable = True Then
            RadGroupBox2.Visible = True
        End If

        ' For dril down open in Ticket no : BHA/04/10/18-000595
        If clsCommon.myLen(Me.Tag) > 0 Then
            txtCode.Value = clsCommon.myCstr(Me.Tag)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
        lblBBValue.Visible = False
        txtBBValue.Visible = False

    End Sub

    Sub LoadBlankGridCat()
        gvCategory.Rows.Clear()
        gvCategory.Columns.Clear()

        Dim repoCatCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCode.FormatString = ""
        repoCatCode.HeaderText = "Category Code"
        repoCatCode.Name = CatcolCode
        repoCatCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatCode.Width = 100L
        gvCategory.MasterTemplate.Columns.Add(repoCatCode)

        Dim repoCatCodeDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCodeDesc.FormatString = ""
        repoCatCodeDesc.HeaderText = "Category Description"
        repoCatCodeDesc.Name = CatcolCodeDesc

        repoCatCodeDesc.Width = 200
        gvCategory.MasterTemplate.Columns.Add(repoCatCodeDesc)

        Dim repoCatValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValue.FormatString = ""
        repoCatValue.HeaderText = "Category Value"
        repoCatValue.Name = CatcolValue
        repoCatValue.Width = 100
        repoCatValue.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        repoCatValue.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatValue.ReadOnly = False
        gvCategory.MasterTemplate.Columns.Add(repoCatValue)

        Dim repoCatValueDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValueDesc.FormatString = ""
        repoCatValueDesc.HeaderText = "Category Value Description"
        repoCatValueDesc.Name = CatcolValueDesc
        repoCatValueDesc.Width = 200
        repoCatValueDesc.ReadOnly = True
        gvCategory.MasterTemplate.Columns.Add(repoCatValueDesc)

        Dim repobinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobinNo.FormatString = ""
        repobinNo.HeaderText = "Bin No"
        repobinNo.Name = CatBinNo
        repobinNo.Width = 200
        repobinNo.ReadOnly = True
        repobinNo.IsVisible = False
        repobinNo.VisibleInColumnChooser = False
        Dim ShowBinMapping As Boolean = False
        ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
        If ShowBinMapping = True Then
            repobinNo.IsVisible = True
            repobinNo.VisibleInColumnChooser = True

        End If
        gvCategory.MasterTemplate.Columns.Add(repobinNo)



        Dim repoCatValueDesc1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCatValueDesc1.FormatString = ""
        repoCatValueDesc1.HeaderText = "Master Value"
        repoCatValueDesc1.Name = Catcolmaster
        repoCatValueDesc1.Width = 80
        repoCatValueDesc1.ThreeState = False
        repoCatValueDesc1.IsVisible = False
        gvCategory.MasterTemplate.Columns.Add(repoCatValueDesc1)

        Dim repoCatValueDesc11 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCatValueDesc11.FormatString = ""
        repoCatValueDesc11.HeaderText = "SKU Value"
        repoCatValueDesc11.Name = CatcolSKU
        repoCatValueDesc11.Width = 80
        repoCatValueDesc11.ThreeState = False
        repoCatValueDesc11.IsVisible = False
        gvCategory.MasterTemplate.Columns.Add(repoCatValueDesc11)

        gvCategory.AllowAddNewRow = False
        gvCategory.ShowGroupPanel = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Private Sub BlankAllConrols()
        chkFGforCF.Checked = False
        chkNIRQC.Checked = False
        chkAllowSRNwoShort.Checked = False
        chkIsReqBatch.Checked = False
        chkRAL.Checked = False
        chkSchemeItem.Checked = False
        txtDistbtr_Amt.Text = 0
        txtCNF_Amt.Text = 0
        txtCorrectionFactor.Value = 0
        txt_shelflife.Text = Nothing
        numMinSelfLife.Text = Nothing
        chkChangeRate.Checked = False
        fndCSA_AC_Code.Value = ""
        txtCSA_AC_Name.Text = ""
        txtBmBdQty.Text = ""
        txtRackNo.Text = ""
        txtPartNo.Value = ""
        txtDescription.Text = ""
        txtItemDescHindi.Text = ""
        txtShortDescHindi.Text = ""
        txtAliesNameHindi.Text = ""
        txtBrand.Text = ""
        txttype.Text = ""
        txtReleasedBy.Text = ""
        txtReleasedDate.Text = clsCommon.GETSERVERDATE()
        txtSubPart.Text = ""
        txtPartNo.MyReadOnly = False
        LoadBlankGridPurQCParameter()
        txtdrawing_no.Text = ""
        LoadBlankQCParameterGrid()
        txtstnd_pur_rate.Text = "0"
        LoadBlankGridUOM()
        LoadBlankGridSchedule()
        LoadBlankGridCat()
        txtCode.Value = ""
        File_Name = ""
        PicImage.Image = Nothing
        rbtnBBNA.IsChecked = True
        lblBBValue.Visible = False
        txtBBValue.Visible = False
        txtBBValue.Text = ""
        'If clsCommon.myLen(auto_icode_seperator) <= 0 Then
        '    txtCode.MyReadOnly = False
        'End If
        txtCode.MyReadOnly = False
        txtDesc.Text = ""
        txtShortDescription.Text = ""
        txtAliesName.Text = ""
        txtAliesName2.Text = ""
        txtAliesName3.Text = ""
        chkMorning.Checked = False
        chkChilledFreezen.Checked = False
        chkTaxable.Checked = False
        rbtnNA.IsChecked = True
        ChkIspurchaseAble.Checked = False
        ChkAllowQC.Checked = False
        txtStructurer.Value = ""
        lblStructurer.Text = ""
        txtPurchaseACSet.Value = ""
        lblPurchaseACSet.Text = ""
        txtSaleAcSet.Value = ""
        lblSaleAcSet.Text = ""
        txtCategory.Value = ""
        lblCategory.Text = ""
        txtSubCategory.Value = ""
        lblSubCategory.Text = ""
        txtUOM.Value = ""
        lblUOM.Text = ""
        fndChptr.Value = ""
        lblchptrdesc.Text = ""
        cboItemType.SelectedValue = ""
        cmbUsedAs.SelectedValue = ""
        cboType.SelectedValue = ""
        txtCost.Value = 0
        txt_tolerance.Enabled = True
        txt_tolerance.Value = 0
        TxtProdTolerance.Value = 0
        txtRate.Value = 0
        txtAssetLife.Value = 0
        txtWarrantyPeriod.Value = 0
        txtLastModiDate.Text = ""
        txtItemSpecification.Text = ""
        ''updation by richa agarwal active by default true on form loading
        chkActive.Checked = True
        ''=======================================
        txtAlternativeItem.Value = Nothing
        txtCategoryStructureCode.Value = ""
        chkIsSerailzedInventory.Checked = False
        chkPickAutoSrNo.Checked = False
        txtNextAutoSerialCounter.Text = ""
        chkMRP.Checked = False
        chkFresh.Checked = False
        chkAmbient.Checked = False
        chkMorning.Visible = Not objCommonVar.IsDemoERP

        txtRate.Visible = Not objCommonVar.IsDemoERP
        lblRate.Visible = Not objCommonVar.IsDemoERP
        txtLastModiDate.Visible = Not objCommonVar.IsDemoERP
        lblLastModiDate.Visible = Not objCommonVar.IsDemoERP
        txtItemSpecification.Visible = Not objCommonVar.IsDemoERP
        lblItemSpecification.Visible = Not objCommonVar.IsDemoERP
        'chkActive.Visible = Not objCommonVar.IsDemoERP
        txtAlternativeItem.Visible = Not objCommonVar.IsDemoERP
        lblAlteernativeItem.Visible = Not objCommonVar.IsDemoERP
        lblAlternativeItemName.Visible = Not objCommonVar.IsDemoERP
        'cboCSAType.Visible = objCommonVar.IsDemoERP
        'lblCSaType.Visible = objCommonVar.IsDemoERP

        isNewEntry = True
        lblWarranty.Text = ""
        txtWarranty.Value = ""
        txtUOM.Value = ""
        txtWeightUOM.Value = ""
        lblWeightUOMDesc.Text = ""
        txtWeightValue.Text = ""
        txtITFCode.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        Me.cboItemSubType.SelectedValue = "Direct"
        Me.cboCSAType.SelectedValue = "None"
        LoadBlankGridCat()
        lblCategory.Text = ""
        lblCategoryStructureCode.Text = ""
        fndProductType.SelectedValue = ""
        '' Anubhooti 22-Sep-2014 BM00000003939
        ChkCrateType.Checked = False
        chkIsCanType.Checked = False
        chkMilkPouch.Checked = False
        chkAdvanceRequired.Checked = False
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnSave.Text = "Save"

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        ''richa agarwal 13/10/2014
        txtCategoryStructureCode.Enabled = True
        gvCategory.ReadOnly = False
        ''=========================
        '' Pankaj Jha, Added Seq No.
        '' 
        txtSeqNo.Text = ""
        txtMarSeqNo.Text = ""

        chkApplyVisualQC.Checked = False
        txtSecurityDedPer.Value = 0
        chkApplyRounding.Checked = False
        ''-------
        ''richa 20/02/2015
        fndGLAcc.Value = ""
        LblGLAcc.Text = ""
        ''------------------
        chkFresh.Visible = True
        chkAmbient.Visible = True
        cmbUsedAs.Visible = True
        'cboCSAType.Visible = True
        'lblCSaType.Visible = True
        MyLabel23.Visible = True
        ChkCrateType.Visible = True
        chkIsCanType.Visible = True
        chkAutoWeighment.Checked = False
        FndHSNCode.Value = ""
        lblHSNDesc.Text = ""
        FndHSNCode.MendatroryField = False
        chkSkipGST.Checked = False
        chkPowerAndFuel.Checked = False
        ChkCrate.Checked = False
        chkCAN.Checked = False
        chkScrapItem.Checked = False
        fndScrapItem.Visible = False
        chkLeakageNotApplicable.Checked = False
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmItemMasterRMOther_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag AndAlso btnNew.Enabled Then
            BlankAllConrols()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                chkDoNotCheckOnSave.Visible = True
            End If
        End If
    End Sub
    Public Sub SetLength()
        txtCode.MyMaxLength = 50
        txtDesc.MaxLength = 100
    End Sub
    Sub LoadBlankQCParameterGrid()
        gv_param.Rows.Clear()
        gv_param.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colparamCode
        repocode.Width = 155
        repocode.HeaderText = "Parameter Code"
        repocode.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_param.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colparamDesc
        reponame.Width = 255
        reponame.HeaderText = "Description"
        reponame.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(reponame)

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.Name = colparamnature
        reponature.Width = 80
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(reponature)

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colparamLower
        repolower.Width = 80
        repolower.HeaderText = "Lower Range"
        'repolower.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repolower)

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colparamUpper
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        'repoupper.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repoupper)

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colparamValue1
        repovalue1.Width = 80
        repovalue1.HeaderText = "Value"
        repovalue1.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repovalue1)

        Dim repovalue2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue2.Name = colparamValue2
        repovalue2.Width = 80
        repovalue2.HeaderText = "Value-2"
        repovalue2.IsVisible = False
        gv_param.MasterTemplate.Columns.Add(repovalue2)

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colparamStatus
        repostatus.Width = 80
        repostatus.HeaderText = "Status(Yes/No)"
        repostatus.DataSource = LoadComboboxParam()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        repostatus.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repostatus)

        Dim repodate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repodate.FormatString = ""
        repodate.Name = colparamDate
        repodate.Width = 80
        repodate.HeaderText = "Effective Date"
        repodate.IsVisible = False
        gv_param.MasterTemplate.Columns.Add(repodate)

        Dim repolower1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower1.Name = colActualRange
        repolower1.Width = 80
        repolower1.HeaderText = "Std. Range"
        repolower1.DecimalPlaces = 2
        gv_param.MasterTemplate.Columns.Add(repolower1)

        Dim repovalue21 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue21.Name = colActualvalue
        repovalue21.Width = 80
        repovalue21.HeaderText = "Std. Value"
        repovalue21.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repovalue21)

        Dim repostatus1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus1.Name = colActualstatus
        repostatus1.Width = 80
        repostatus1.HeaderText = "Std. Status(Yes/No)"
        repostatus1.DataSource = LoadComboboxParam()
        repostatus1.ValueMember = "Code"
        repostatus1.DisplayMember = "Name"
        'repostatus1.ReadOnly = True
        gv_param.MasterTemplate.Columns.Add(repostatus1)

        Dim repovalue22 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repovalue22.Name = colSTDRate
        repovalue22.Width = 80
        repovalue22.HeaderText = "Standard Rate"
        repovalue22.ReadOnly = False
        gv_param.MasterTemplate.Columns.Add(repovalue22)


        gv_param.AllowDeleteRow = True
        gv_param.AllowAddNewRow = False
        gv_param.ShowGroupPanel = False
        gv_param.AllowColumnReorder = False
        gv_param.AllowRowReorder = False
        gv_param.EnableSorting = False
        gv_param.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_param.MasterTemplate.ShowRowHeaderColumn = False
        gv_param.Rows.AddNew()
    End Sub
    Sub LoadBlankGridPurQCParameter()
        gvPurQCPar.Rows.Clear()
        gvPurQCPar.Columns.Clear()

        Dim NumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        NumBox.Name = colPurQCParamSNo
        NumBox.Width = 80
        NumBox.HeaderText = "SNo"
        NumBox.DecimalPlaces = 0
        NumBox.ReadOnly = True
        gvPurQCPar.MasterTemplate.Columns.Add(NumBox)

        Dim txtBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        txtBox.Name = colPurQCParamCode
        txtBox.Width = 150
        txtBox.HeaderText = "Parameter Code"
        txtBox.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        txtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gvPurQCPar.MasterTemplate.Columns.Add(txtBox)

        txtBox = New GridViewTextBoxColumn()
        txtBox.Name = colPurQCParamDesc
        txtBox.Width = 200
        txtBox.HeaderText = "Description"
        txtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        txtBox.ReadOnly = True
        gvPurQCPar.MasterTemplate.Columns.Add(txtBox)


        txtBox = New GridViewTextBoxColumn()
        txtBox.Name = colPurQCParamSpecification
        txtBox.Width = 200
        txtBox.HeaderText = "Specification"
        txtBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gvPurQCPar.MasterTemplate.Columns.Add(txtBox)


        gvPurQCPar.AllowDeleteRow = True
        gvPurQCPar.AllowAddNewRow = False
        gvPurQCPar.ShowGroupPanel = False
        gvPurQCPar.AllowColumnReorder = False
        gvPurQCPar.AllowRowReorder = False
        gvPurQCPar.EnableSorting = False
        gvPurQCPar.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPurQCPar.MasterTemplate.ShowRowHeaderColumn = False
        gvPurQCPar.Rows.AddNew()
    End Sub
    Function LoadComboboxParam() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Sub applyNLevelCategorySetting()
        Dim qry As String = "select IsNLevelCatForItem from TSPL_INV_PARAMETERS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("IsNLevelCatForItem") = 0 Then
                txtCategory.Enabled = True
                txtSubCategory.Enabled = True
                RadPageView1.Pages(2).Enabled = False
            Else
                txtCategory.Enabled = False
                txtSubCategory.Enabled = False
                RadPageView1.Pages(2).Enabled = True
            End If
        End If

        'auto_icode_seperator = Nothing
        'auto_iname_seperator = Nothing
        'If RadPageView1.Pages(2).Enabled = True Then
        '    Dim autoitemcode As Boolean = IIf(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, Nothing) = "0", False, True)

        '    If autoitemcode = True Then
        '        txtDesc.ReadOnly = True
        '        txtCode.MyReadOnly = True
        '        auto_icode_seperator = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AutoItemNLevel + "' and code='" + clsFixedParameterType.AutoItemNLevel + "'"))
        '        If clsCommon.myLen(auto_icode_seperator) > 1 Then
        '            auto_iname_seperator = auto_icode_seperator.Substring(1, auto_icode_seperator.Length - 1)
        '            auto_icode_seperator = auto_icode_seperator.Substring(0, 1)
        '        End If
        '        If clsCommon.myLen(auto_iname_seperator) <= 0 Then
        '            auto_iname_seperator = " "
        '        End If
        '    Else
        '        txtDesc.ReadOnly = False
        '        txtCode.MyReadOnly = False
        '        auto_icode_seperator = Nothing
        '        auto_iname_seperator = Nothing
        '    End If
        'End If

    End Sub
    Private Sub ItemConversionAutomation(ByVal IntRowNo As Integer)
        Dim dblConvF As Double = 0
        Dim strStockingUnit = clsCommon.myCstr(gvUOM.Rows(0).Cells(UOMColUnit).Value)
        Dim strUnit = clsCommon.myCstr(gvUOM.Rows(IntRowNo).Cells(UOMColUnit).Value)
        gvUOM.Rows(IntRowNo).Cells(UOMColStockUnit).ReadOnly = True
        Dim IsStockingUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strStockingUnit & "'"))
        Dim StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnit & "'"))
        If IntRowNo = 0 Then
            gvUOM.Rows(IntRowNo).Cells(UOMColConvFact).ReadOnly = True
            gvUOM.Rows(IntRowNo).Cells(UOMColStockUnit).Value = "Y"
            If AllowTo = False Then
                txtWeightUOM.Value = ""
                txtWeightValue.Value = 0
            End If
            If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                txtWeightUOM.Value = gvUOM.Rows(IntRowNo).Cells(UOMColUnit).Value
                txtWeightValue.Value = 1
            End If
        Else
            gvUOM.Rows(IntRowNo).Cells(UOMColStockUnit).Value = "N"
            Dim IsUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strUnit & "'"))
            Dim IsUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strUnit & "'"))
            Dim IsUnitPackingType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Packet_Type from tspl_unit_master where Unit_Code='" & strUnit & "'"))
            If clsCommon.CompairString(IsUnitWeight, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IsUnitPackingType, "N") = CompairStringResult.Equal Then
                If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUnit & "' and Contained_UOM='" & strStockingUnit & "'"))
                    Else
                        If clsCommon.myLen(txtStructurer.Value) = 0 Then
                            Throw New Exception("Please enter Structure Code")
                        End If
                        ''BHA/18/01/19-000785 by balwinder on 24/06/2019
                        qry = "select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUnit & "' and Contained_UOM='" & strStockingUnit & "' and Structure_Code='" & txtStructurer.Value & "'"
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    End If
                    If dblConvF = 0 Then
                        Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUnit & " Contained Unit - " & strStockingUnit & " ")
                    End If
                Else
                    If clsCommon.CompairString(cboItemType.SelectedValue, "F") = CompairStringResult.Equal Then
                        If clsCommon.myLen(txtWeightUOM.Value) = 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            txtWeightUOM.Focus()
                            Throw New Exception("Please enter Weight UOM")
                        ElseIf clsCommon.myCdbl(txtWeightValue.Value) = 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            txtWeightValue.Focus()
                            Throw New Exception("Please enter Weight UOM Conversion")
                        End If
                        Dim strStockingUnitWeight = txtWeightUOM.Value
                        If clsCommon.CompairString(strStockingUnitWeight, strUnit) = CompairStringResult.Equal Then
                            ''richa agarwal TEC/02/07/19-000577 25 Sep,2019
                            If clsCommon.myCdbl(txtWeightValue.Value) <> 1 Then
                                dblConvF = 1 / clsCommon.myCdbl(txtWeightValue.Value)
                            Else
                                dblConvF = 1
                            End If

                        Else
                            StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnitWeight & "'"))
                            If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                                dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUnit) & "' and Contained_UOM='" & strStockingUnitWeight & "'"))
                            Else
                                dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUnit) & "' and Contained_UOM='" & strStockingUnitWeight & "' and Structure_Code='" & clsCommon.myCstr(txtStructurer.Value) & "'"))
                            End If
                            If dblConvF > 0 Then
                                dblConvF = dblConvF / clsCommon.myCdbl(txtWeightValue.Value)
                            Else
                                Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUnit & " Contained Unit - " & strStockingUnitWeight & " ")
                            End If
                        End If


                    End If
                End If
                gvUOM.Rows(IntRowNo).Cells(UOMColConvFact).Value = dblConvF
            Else
                If AllowTo = False Then
                    gvUOM.Rows(IntRowNo).Cells(UOMColConvFact).Value = 0
                End If
            End If
        End If

    End Sub
    Private Sub ReadOnlyUOMGrid(ByVal IntRowNo As Integer)
        gvUOM.Rows(IntRowNo).Cells(UOMColUnit).ReadOnly = IIf(clsCommon.myCdbl(gvUOM.Rows(IntRowNo).Cells(UOMColStockUnitChangable).Value) = "1", True, False)
        gvUOM.Rows(IntRowNo).Cells(UOMColConvFact).ReadOnly = IIf(clsCommon.myCdbl(gvUOM.Rows(IntRowNo).Cells(UOMColStockUnitChangable).Value) = "1", True, False)
        '======now user able to change stocking unit status,but not conversion factor
        gvUOM.Rows(IntRowNo).Cells(UOMColStockUnit).ReadOnly = IIf(clsCommon.myCdbl(gvUOM.Rows(IntRowNo).Cells(UOMColStockUnitChangable2).Value) = "1", True, False)   'False 
        gvUOM.Rows(IntRowNo).Cells(UOMDefault).ReadOnly = False
        gvUOM.Rows(IntRowNo).Cells(UOMPieces).ReadOnly = False
        gvUOM.Rows(IntRowNo).Cells(UOMGrossWeight).ReadOnly = False
        gvUOM.Rows(IntRowNo).Cells(UOMNetWeight).ReadOnly = False
        gvUOM.Rows(IntRowNo).Cells(UOMJobWorkRate).ReadOnly = False
        If AllowItemConversionAutomation = 1 Then
            Dim IsUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & clsCommon.myCstr(gvUOM.Rows(IntRowNo).Cells(UOMColUnit).Value) & "'"))
            If clsCommon.CompairString(IsUnitWeight, "Y") = CompairStringResult.Equal Then
                gvUOM.Rows(IntRowNo).Cells(UOMColConvFact).ReadOnly = True
            End If
        End If
    End Sub
    Sub LoadBlankGridUOM()
        gvUOM.Rows.Clear()
        gvUOM.Columns.Clear()

        Dim repoUOMQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMQty.FormatString = ""
        repoUOMQty.HeaderText = ""
        repoUOMQty.Name = UOMColQty
        repoUOMQty.Width = 18
        repoUOMQty.ReadOnly = True
        gvUOM.MasterTemplate.Columns.Add(repoUOMQty)

        Dim repoUOMCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMCode.FormatString = ""
        repoUOMCode.HeaderText = "UOM"
        repoUOMCode.Name = UOMColUnit
        repoUOMCode.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        repoUOMCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUOMCode.Width = 100
        gvUOM.MasterTemplate.Columns.Add(repoUOMCode)

        'UOMColEqual
        Dim repoUOMEqual As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMEqual.FormatString = ""
        repoUOMEqual.HeaderText = ""
        repoUOMEqual.Name = UOMColEqual
        repoUOMEqual.Width = 18
        repoUOMEqual.ReadOnly = True
        gvUOM.MasterTemplate.Columns.Add(repoUOMEqual)

        Dim repoUOMName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMName.FormatString = ""
        repoUOMName.HeaderText = "UOM Descriiption"
        repoUOMName.Name = UOMColUnitDesc
        repoUOMName.Width = 150
        repoUOMName.ReadOnly = True
        repoUOMName.IsVisible = False
        gvUOM.MasterTemplate.Columns.Add(repoUOMName)

        Dim repoConvFactor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Conversion Factor"
        repoConvFactor.Name = UOMColConvFact
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)

        Dim repoUOMConvUom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOMConvUom.FormatString = ""
        repoUOMConvUom.HeaderText = ""
        repoUOMConvUom.Name = UOMColConvUom
        repoUOMConvUom.Width = 90
        repoUOMConvUom.ReadOnly = True
        gvUOM.MasterTemplate.Columns.Add(repoUOMConvUom)

        Dim repoStockUnit As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStockUnit.FormatString = ""
        repoStockUnit.HeaderText = "Stock Unit"
        repoStockUnit.Name = UOMColStockUnit
        repoStockUnit.Width = 100
        repoStockUnit.ReadOnly = IIf(AllowItemConversionAutomation = 1, True, False)
        repoStockUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStockUnit.DataSource = GetStockUnit()
        repoStockUnit.ValueMember = "Code"
        repoStockUnit.DisplayMember = "Name"
        gvUOM.MasterTemplate.Columns.Add(repoStockUnit)

        Dim repoStockUnitChangable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStockUnitChangable.FormatString = ""
        repoStockUnitChangable.HeaderText = "Stock Unit Changable"
        repoStockUnitChangable.Name = UOMColStockUnitChangable
        repoStockUnitChangable.Minimum = 0
        repoStockUnitChangable.Width = 100
        repoStockUnitChangable.IsVisible = False
        repoStockUnitChangable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoStockUnitChangable)

        'UOMColStockUnitChangable2
        Dim repoStockUnitChangable2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStockUnitChangable2.FormatString = ""
        repoStockUnitChangable2.HeaderText = "Stock Unit Changable2"
        repoStockUnitChangable2.Name = UOMColStockUnitChangable2
        repoStockUnitChangable2.Minimum = 0
        repoStockUnitChangable2.Width = 100
        repoStockUnitChangable2.IsVisible = False
        repoStockUnitChangable2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoStockUnitChangable2)



        ''Added by richa agarwal against ticket no BM00000004327
        Dim repoDefaultUom As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoDefaultUom.FormatString = ""
        repoDefaultUom.HeaderText = "Default UOM"
        repoDefaultUom.Name = UOMDefault
        repoDefaultUom.Width = 80
        repoDefaultUom.ThreeState = False
        repoDefaultUom.IsVisible = True
        repoDefaultUom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoDefaultUom)
        ''===================================
        Dim repoPieces As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoPieces.FormatString = ""
        repoPieces.HeaderText = "Custom Conversion"
        repoPieces.Name = UOMCustomConversion
        repoPieces.Width = 80
        repoPieces.ThreeState = False
        repoPieces.IsVisible = True
        repoPieces.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoPieces)
        '================Added by preeti gupta against Ticket No[ERO/01/05/18-000285]================
        repoPieces = New GridViewCheckBoxColumn()
        repoPieces.FormatString = ""
        repoPieces.HeaderText = "Pieces"
        repoPieces.Name = UOMPieces
        repoPieces.Width = 80
        repoPieces.ThreeState = False
        repoPieces.IsVisible = True
        repoPieces.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUOM.MasterTemplate.Columns.Add(repoPieces)
        ''======================================================

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Gross Wt."
        repoConvFactor.Name = UOMGrossWeight
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.WrapText = True
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)


        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Net Wt."
        repoConvFactor.Name = UOMNetWeight
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.WrapText = True
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Job Work Rate"
        repoConvFactor.Name = UOMJobWorkRate
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.WrapText = True
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 2
        repoConvFactor.FormatString = "{0:n2}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)


        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Item Cost"
        repoConvFactor.Name = UOMItemCost
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.WrapText = True
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 2
        repoConvFactor.FormatString = "{0:n2}"
        gvUOM.MasterTemplate.Columns.Add(repoConvFactor)



        gvUOM.AllowAddNewRow = True
        gvUOM.ShowGroupPanel = False
        gvUOM.AllowColumnReorder = False
        gvUOM.AllowRowReorder = True
        gvUOM.AllowDeleteRow = True
        gvUOM.AllowEditRow = True
        gvUOM.EnableSorting = False
        gvUOM.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvUOM.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Sub LoadDatabase()
        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim ArrHide As List(Of String) = New List(Of String)
        ArrHide.Add("DataBase_Name")
        cbgDatabase.ArrHideColumns = ArrHide

        cbgDatabase.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDatabase.ValueMember = "DataBase_Name"
    End Sub
    Function GetStockUnit() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "No"
        dt.Rows.Add(dr)
        gvUOM.AllowDeleteRow = True

        Return dt
    End Function
    Sub LoadType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "A"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "B"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "C"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "G"
        dr("Name") = "GHEE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Technical Spare Parts"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub
    Sub LoadUsedas()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "MCC Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "I"
        dr("Name") = "Mcc Issue"
        dt.Rows.Add(dr)
        '==============Added by preeti gupta Against Ticket no[ERO/29/06/18-000363]
        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Production"
        dt.Rows.Add(dr)

        cmbUsedAs.DataSource = dt
        cmbUsedAs.ValueMember = "Code"
        cmbUsedAs.DisplayMember = "Name"
    End Sub
    Sub LoadItemType()
        ''Note If Do Any change please also change in LoadItemTypeQuery function
        Dim dt As New DataTable()
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))
        'Dim dr As DataRow = Nothing

        'dr = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "R"
        'dr("Name") = "Raw Material"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "F"
        'dr("Name") = "Finished Good"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "S"
        'dr("Name") = "Semi Finished Good"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "A"
        'dr("Name") = "Asset"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "T"
        'dr("Name") = "Trading Good"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "O"
        'dr("Name") = "Other"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "J"
        'dr("Name") = "Job Work"
        'dt.Rows.Add(dr)
        'Dim qry = clsItemMaster.getItemTypeQuery()
        Dim Whr = " AND IS_NON_INVENTORY=0 "
        dt = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.DataSource = dt
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub
    Public Shared Function LoadItemTypeQuery() As String
        ''Note If Do Any change please also change in LoadItemType function
        'Dim qry As String = "select 'R' as Code,'Raw Material' as Name" & _
        '" union " & _
        '" select 'F' as Code,'Finished Good' as Name " & _
        '" union " & _
        '" select 'S' as Code,'Semi Finished Good' as Name " & _
        '" union  " & _
        '" select 'A' as Code,'Asset' as Name " & _
        '" union " & _
        '" select 'O' as Code,'Other' as Name" & _
        '" union " & _
        '" select 'J' as Code,'Job Work' as Name"
        Dim qry As String = " SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  "
        Return qry
    End Function
    Sub LoadItemProductType()
        Dim dt As DataTable = clsItemMaster.LoadItemProductType()

        fndProductType.DataSource = dt
        fndProductType.ValueMember = "Code"
        fndProductType.DisplayMember = "Name"
    End Sub
    Sub LoadWarrantyDate()
        Dim dt As DataTable = clsItemMaster.LoadWarrantyDate()

        CmbWarrApp.DataSource = dt
        CmbWarrApp.ValueMember = "Code"
        CmbWarrApp.DisplayMember = "Name"
    End Sub
    Sub LoadItemSubType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Direct"
        dr("Name") = "Direct"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Packaging"
        dr("Name") = "Packaging"
        dt.Rows.Add(dr)

        cboItemSubType.DataSource = dt
        cboItemSubType.ValueMember = "Code"
        cboItemSubType.DisplayMember = "Name"
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllConrols()
        gvUOM.Rows.AddNew()
        gvSchedule.Rows.AddNew()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        '' Anubhooti 10-Sep-2014 BM00000003847
        ItemShortDesp()
        ' BM00000007910
        fndScrapItem.Visible = False
        chkScrapItem.Checked = False
        chkMilkPouch.Checked = False
        chkQCSNFBssed.Checked = False
        chkAdvanceRequired.Checked = False
        chkIsDisplayDemad.Checked = False
        chkExcludeInApp.Checked = False
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing)), "A") = CompairStringResult.Equal Then
            CmbWarrApp.Visible = True
            LblWarrDate.Visible = True
            chkFresh.Visible = True
            chkAmbient.Visible = True
            ChkCrateType.Visible = True
            chkIsCanType.Visible = True

        Else
            CmbWarrApp.Visible = False
            LblWarrDate.Visible = False
            chkFresh.Visible = False
            chkAmbient.Visible = False
            'cboCSAType.Visible = False
            ChkCrateType.Visible = False
            chkIsCanType.Visible = False
        End If
        ''======Parteek=======''
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing), "D") = CompairStringResult.Equal Then
            chkFresh.Visible = True
            chkAmbient.Visible = True
            cmbUsedAs.Visible = True
            'cboCSAType.Visible = True
            'lblCSaType.Visible = True
            MyLabel23.Visible = True
            ChkCrateType.Visible = True
            chkIsCanType.Visible = True
        Else
            cmbUsedAs.Visible = False
            'cboCSAType.Visible = False
            'lblCSaType.Visible = False
            MyLabel23.Visible = False

        End If
        ''======End=======''
        If clsCommon.myLen(txtWarranty.Value) > 0 Then
            CmbWarrApp.Enabled = True
        Else
            CmbWarrApp.Enabled = False
            CmbWarrApp.SelectedValue = ""
        End If
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        txtInsurance.Text = ""
        chkInsurance.Checked = False
        RadGroupBoxInsurance.Enabled = False
    End Sub
    Private Sub txtStructurer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtStructurer._MYValidating
        Dim qry As String = "select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
        txtStructurer.Value = clsCommon.ShowSelectForm("IMStruCode", qry, "Code", "", txtStructurer.Value, "", isButtonClicked)
        lblStructurer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtStructurer.Value + "'"))
    End Sub
    Private Sub txtPurchaseACSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPurchaseACSet._MYValidating
        Dim qry As String = "select Purchase_Class_Code as [Code], Purchase_Class_Desc as [Description] from dbo.TSPL_PURCHASE_ACCOUNTS"
        txtPurchaseACSet.Value = clsCommon.ShowSelectForm("IMPurchaseACSet", qry, "Code", "", txtPurchaseACSet.Value, "", isButtonClicked)
        lblPurchaseACSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Purchase_Class_Desc  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + txtPurchaseACSet.Value + "'"))
    End Sub
    Private Sub FndHSNCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndHSNCode._MYValidating
        Try
            Dim qry As String = "select Code as [Code], Description as [Description] from dbo.TSPL_HSN_MASTER"
            FndHSNCode.Value = clsCommon.ShowSelectForm("IMHSN", qry, "Code", "", FndHSNCode.Value, "", isButtonClicked)
            lblHSNDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description  from TSPL_HSN_MASTER where Code ='" + FndHSNCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtSaleAcSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSaleAcSet._MYValidating
        Dim qry As String = " select Sales_Class_Code as [SalesAccountSet], Sales_Class_Desc as [Description] from TSPL_SALES_ACCOUNTS"
        txtSaleAcSet.Value = clsCommon.ShowSelectForm("IMSaleACSEt", qry, "SalesAccountSet", "", txtSaleAcSet.Value, "", isButtonClicked)
        lblSaleAcSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Desc  from TSPL_SALES_ACCOUNTS where Sales_Class_Code='" + txtSaleAcSet.Value + "'"))
    End Sub
    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "select category_code as [ItemCategory],category_name as [Description] from tspl_Item_category"
        txtCategory.Value = clsCommon.ShowSelectForm("IMCategroy", qry, "ItemCategory", "", txtCategory.Value, "", isButtonClicked)
        lblCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  category_name from tspl_Item_category where category_code='" + txtCategory.Value + "'"))
    End Sub
    Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select unit_code as [Code], unit_desc as [Description] from tspl_unit_master"
        txtUOM.Value = clsCommon.ShowSelectForm("IMUOM", qry, "Code", "", txtUOM.Value, "", isButtonClicked)
        lblUOM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code ='" + txtUOM.Value + "'"))
    End Sub
    Private Sub txtSubCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSubCategory._MYValidating
        Dim Qry As String = "select Sub_Category_Code as [Code], Description  from TSPL_ITEM_SUB_CATEGORY "
        txtSubCategory.Value = clsCommon.ShowSelectForm("fnsubcat", Qry, "Code", "Category_Code='" + txtCategory.Value + "'", txtSubCategory.Value, "Code", isButtonClicked)
        lblSubCategory.Text = clsDBFuncationality.getSingleValue("select Description  from TSPL_ITEM_SUB_CATEGORY where Sub_Category_Code='" + txtSubCategory.Value + "'")
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "Update" AndAlso OneTimeCheck = False Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.UpdatePassword
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                ShowRemarks()
                OneTimeCheck = True
            End If
        ElseIf btnSave.Text = "Update" AndAlso OneTimeCheck Then
            ShowRemarks()
        Else
            Savedata()
        End If
    End Sub
    Sub Savedata()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmItemMasterRMOther, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As clsItemMaster = New clsItemMaster()
                obj.Is_Rate_Change_OnDairyDispatch = IIf(chkChangeRate.Checked = True, 1, 0)
                obj.AllowSRNWithoutShortReject = IIf(chkAllowSRNwoShort.Checked = True, 1, 0)
                obj.Item_Code = txtCode.Value
                obj.Item_Desc = txtDesc.Text
                obj.Item_Desc_Hindi = txtItemDescHindi.Text
                obj.Part_No = clsCommon.myCstr(txtPartNo.Value).Replace("'", "`")
                obj.Drawing_No = clsCommon.myCstr(txtdrawing_no.Text).Replace("'", "`")
                obj.Item_Short_Desc = txtShortDescription.Text
                obj.Item_Short_Desc_Hindi = txtShortDescHindi.Text
                '==============Added by preeti Gupta Against Ticket No[ERO/10/05/18-000302]=============
                obj.Alies_Name = txtAliesName.Text
                obj.Alies_Name_Hindi = txtAliesNameHindi.Text
                obj.Alies_Name2 = txtAliesName2.Text
                obj.Alies_Name3 = txtAliesName3.Text
                obj.Crate = ChkCrate.Checked
                obj.Can = chkCAN.Checked
                '====================================================================
                obj.Unit_Code = txtUOM.Value
                obj.std_pur_rate = clsCommon.myCdbl(txtstnd_pur_rate.Text)
                obj.Structure_Code = txtStructurer.Value
                obj.Structure_Desc = txtStructurer.Value
                obj.Purchase_Class_Code = txtPurchaseACSet.Value
                obj.Cost = txtCost.Value
                obj.Tolerance = txt_tolerance.Value
                obj.Production_Tolerance = TxtProdTolerance.Value
                obj.Sale_Class_Code = txtSaleAcSet.Value
                obj.item_category = txtCategory.Value
                obj.Sub_item_category = txtSubCategory.Value
                obj.Cheapter_Heads = fndChptr.Value
                obj.TypeOfItm = clsCommon.myCstr(cboType.SelectedValue)
                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Morning = chkMorning.Checked
                obj.Chilled_Freezen = IIf(chkChilledFreezen.Checked = True, 1, 0)
                obj.IsTaxable = chkTaxable.Checked
                obj.shelflife = clsCommon.myCstr(txt_shelflife.Text)
                obj.Min_shelf_life = clsCommon.myCstr(numMinSelfLife.Text)
                obj.Is_Purchaseable_item = ChkIspurchaseAble.Checked
                obj.Is_Allow_QC = ChkAllowQC.Checked
                obj.Rate = txtRate.Value
                obj.Asset_Life = txtAssetLife.Value
                obj.Warranty_period = txtWarrantyPeriod.Value
                obj.Modify_Date = txtLastModiDate.Text
                obj.ItemSpecification = txtItemSpecification.Text
                obj.Active = chkActive.Checked
                obj.AlternativeItem = txtAlternativeItem.Value
                obj.Item_Category_Struct_Code = txtCategoryStructureCode.Value
                obj.Sku_Seq = clsCommon.myCdbl(txtSeqNo.Text)
                obj.Is_DisplayDemand = chkIsDisplayDemad.Checked
                obj.Is_ExcludeAPP = chkExcludeInApp.Checked
                obj.Marketing_Seq = clsCommon.myCdbl(txtMarSeqNo.Text)
                obj.ApplyRoundingInStdProd = chkApplyRounding.Checked
                obj.Visual_QC = chkApplyVisualQC.Checked
                obj.Security_Deduction = txtSecurityDedPer.Value
                If rbtnBBNA.IsChecked Then
                    obj.BuyBackType = 0

                ElseIf rbtnBBAmount.IsChecked Then
                    obj.BuyBackType = 1
                    obj.BuyBackValue = txtBBValue.Text
                ElseIf rbtnBBPer.IsChecked Then
                    obj.BuyBackType = 2
                    obj.BuyBackValue = txtBBValue.Text

                End If

                obj.ArrUomDetails = New List(Of clsItemUOMDetails)()
                '' newly added column SubItemType
                If Me.cboItemSubType.SelectedValue Is Nothing Then
                    obj.SubItemType = "Direct"
                ElseIf clsCommon.myLen(Me.cboItemSubType.SelectedValue) <= 0 Then
                    obj.SubItemType = "Direct"
                Else
                    obj.SubItemType = Me.cboItemSubType.SelectedValue
                End If
                '' end newly added column SubItemType
                obj.Is_Serial_Item = chkIsSerailzedInventory.Checked
                obj.Is_Batch_Item = chkIsReqBatch.Checked
                obj.RAL = chkRAL.Checked
                obj.Is_Pick_Auto_SrNo = chkPickAutoSrNo.Checked
                obj.Serial_Counter = txtNextAutoSerialCounter.Text
                obj.Warranty_Code = txtWarranty.Value
                obj.Weight_UOM = clsCommon.myCstr(txtWeightUOM.Value)
                obj.Weight_Value = clsCommon.myCdbl(txtWeightValue.Text)
                obj.ITFCode = txtITFCode.Text
                obj.Is_MRP = chkMRP.Checked
                obj.Is_FreshItem = chkFresh.Checked
                obj.Is_Ambient = chkAmbient.Checked
                obj.Skip_GST = chkSkipGST.Checked
                obj.Is_Power_And_Fuel = chkPowerAndFuel.Checked
                If AllowGSTApplicable = True Then
                    obj.HSNCode = FndHSNCode.Value
                End If
                If rbtnTaxExempted.IsChecked Then
                    obj.Tax_Exempted = 1
                ElseIf rbtnExcisable.IsChecked Then
                    obj.Tax_Exempted = 2
                End If
                obj.FG_for_CF = IIf(chkFGforCF.Checked = True, 1, 0)
                If chkFGforCF.Checked = True Then
                    obj.BomBuildQty = txtBmBdQty.Text
                End If
                obj.NIR_QC = chkNIRQC.Checked
                ' Ticket No - BM00000003041 3/July/2014 by Puran
                obj.Is_Scheme_Item = chkSchemeItem.Checked
                obj.Distributor_Commission = clsCommon.myCdbl(txtDistbtr_Amt.Text)
                obj.CNF_Commission = clsCommon.myCdbl(txtCNF_Amt.Text)
                obj.Correction_Factor = txtCorrectionFactor.Value

                obj.Product_Type = clsCommon.myCstr(fndProductType.SelectedValue)
                obj.CSA_Type = clsCommon.myCstr(cboCSAType.Text)
                '' Anubhooti 11-Sep-2014
                obj.Is_CrateType = ChkCrateType.Checked
                obj.Is_CAN_Type = chkIsCanType.Checked
                'If CreateGLAccToItem = True AndAlso fndGLAcc.Visible = True Then
                '    obj.GL_Account = fndGLAcc.Value
                'End If
                ' Ticket No : BHA/02/08/18-000209 By Prabhakar - For Scrap Item 
                obj.Is_Scrap_Item = chkScrapItem.Checked
                obj.Scrap_Item_Code = fndScrapItem.Value
                obj.Is_Milk_Pouch = chkMilkPouch.Checked
                obj.Is_QC_SNF_Based = IIf(chkQCSNFBssed.Checked = True, 1, 0)
                obj.Is_Advance_Required = chkAdvanceRequired.Checked

                If clsCommon.myLen(fndGLAcc.Value) > 0 Then
                    obj.GL_Account = fndGLAcc.Value
                End If
                ''
                ' BM00000007860
                obj.Warranty_Applied_From = clsCommon.myCstr(CmbWarrApp.SelectedValue)
                ''added by richa agarwal against ticket no BM00000004327
                '===============rohit nov 18,2014 ==========
                obj.Item_used_as = clsCommon.myCstr(cmbUsedAs.SelectedValue)
                '=====================================
                Dim CountDefaultUnit As Integer = 0
                For ii As Integer = 0 To gvUOM.RowCount - 1
                    If gvUOM.Rows(ii).Cells(UOMDefault).Value = True Then
                        CountDefaultUnit = CountDefaultUnit + 1
                    End If
                Next
                ''===========================================
                Dim CountPieces As Integer = 0
                For ii As Integer = 0 To gvUOM.RowCount - 1
                    If gvUOM.Rows(ii).Cells(UOMPieces).Value = True Then
                        CountPieces = CountPieces + 1
                    End If
                Next
                ''===========================================
                '============================================
                obj.Cust_Account = clsCommon.myCstr(fndCSA_AC_Code.Value)
                obj.Is_Auto_Weighment = chkAutoWeighment.Checked
                obj.Is_Leakage_Not_Applicable = chkLeakageNotApplicable.Checked
                If chkInsurance.Checked = True Then
                    obj.Is_Insurance = CInt(clsCommon.myCstr(IIf(chkInsurance.Checked = True, "1", "0")))
                    obj.InsuranceFromDate = txtFromDate.Value
                    obj.InsuranceToDate = txtToDate.Value
                    obj.InsuranceNo = clsCommon.myCstr(txtInsurance.Text)
                Else
                    obj.Is_Insurance = CInt(clsCommon.myCstr(IIf(chkInsurance.Checked = True, "1", "0")))
                    obj.InsuranceFromDate = Nothing
                    obj.InsuranceToDate = Nothing
                    obj.InsuranceNo = Nothing
                End If
                For ii As Integer = 0 To gvUOM.RowCount - 1
                    Dim objtr As New clsItemUOMDetails()
                    objtr.UOM_Code = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value)
                    objtr.UOM_Description = clsItemUOMDetails.GetName(txtUOM.Value)
                    objtr.Stocking_Unit = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColStockUnit).Value)
                    If clsCommon.CompairString(objtr.Stocking_Unit, "Y") = CompairStringResult.Equal Then
                        objtr.Conversion_Factor = 1
                    Else
                        objtr.Conversion_Factor = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMColConvFact).Value)
                    End If
                    ''added by richa agarwal against ticket no BM00000004327
                    If clsCommon.CompairString(gvUOM.Rows(ii).Cells(UOMDefault).Value, True) = CompairStringResult.Equal And CountDefaultUnit = 1 Then
                        objtr.Default_UOM = 1
                    ElseIf clsCommon.CompairString(objtr.Stocking_Unit, "Y") = CompairStringResult.Equal And CountDefaultUnit <> 1 Then
                        objtr.Default_UOM = 1
                    Else
                        objtr.Default_UOM = 0
                    End If
                    If clsCommon.CompairString(gvUOM.Rows(ii).Cells(UOMPieces).Value, True) = CompairStringResult.Equal And CountPieces = 1 Then
                        objtr.Pieces = 1
                    Else
                        objtr.Pieces = 0
                    End If
                    objtr.Gross_Weight = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMGrossWeight).Value)
                    objtr.Net_Weight = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMNetWeight).Value)
                    objtr.Job_Work_Rate = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMJobWorkRate).Value)
                    objtr.Item_Cost = clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMItemCost).Value)
                    objtr.Custom_Conversion = clsCommon.myCBool(gvUOM.Rows(ii).Cells(UOMCustomConversion).Value)
                    If clsCommon.myLen(objtr.UOM_Code) > 0 Then
                        obj.ArrUomDetails.Add(objtr)
                    End If
                Next

                obj.ArrItemMasterCategory = New List(Of clsItemMasterCategory)()
                Dim strBinNo As String = Nothing
                For ii As Integer = 0 To gvCategory.Rows.Count - 1
                    Dim objItemCatg As New clsItemMasterCategory()
                    objItemCatg.Item_Category_Code = clsCommon.myCstr(gvCategory.Rows(ii).Cells(CatcolCode).Value)
                    objItemCatg.Item_Cagetory_Values = clsCommon.myCstr(gvCategory.Rows(ii).Cells(CatcolValue).Value)

                    objItemCatg.Master_Value = clsCommon.myCstr(IIf(clsCommon.myCBool(gvCategory.Rows(ii).Cells(Catcolmaster).Value) = True, "1", "0"))
                    objItemCatg.SKU_Value = clsCommon.myCstr(IIf(clsCommon.myCBool(gvCategory.Rows(ii).Cells(CatcolSKU).Value) = True, "1", "0"))
                    If clsCommon.myLen(gvCategory.Rows(ii).Cells(CatBinNo).Value) > 0 Then
                        strBinNo = clsCommon.myCstr(gvCategory.Rows(ii).Cells(CatBinNo).Value)
                    End If
                    obj.ArrItemMasterCategory.Add(objItemCatg)
                Next

                If clsCommon.myLen(strBinNo) > 0 Then
                    obj.Rack_No = strBinNo
                Else
                    obj.Rack_No = ""
                End If
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.Item_Code = txtCode.Value
                obj.Item_Desc = txtDesc.Text
                obj.CreateSepAssetForEachQty = IIf(chkCreateSepAssetForEachQty.Checked, "1", "0")
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                '---------------------------Parameter Range----------------------------
                obj.Arr_Param = New List(Of clsItemMaster)
                For Each grow As GridViewRowInfo In gv_param.Rows
                    Dim objtrp As New clsItemMaster()

                    objtrp.paramcode = clsCommon.myCstr(grow.Cells(colparamCode).Value)
                    objtrp.Lrange = clsCommon.myCdbl(grow.Cells(colparamLower).Value)
                    objtrp.Urange = clsCommon.myCdbl(grow.Cells(colparamUpper).Value)
                    objtrp.status = clsCommon.myCstr(grow.Cells(colparamStatus).Value)
                    objtrp.value1 = clsCommon.myCstr(grow.Cells(colparamValue1).Value)
                    objtrp.value2 = clsCommon.myCstr(grow.Cells(colparamValue2).Value)
                    objtrp.Actual_range = clsCommon.myCdbl(grow.Cells(colActualRange).Value)
                    objtrp.actual_status = clsCommon.myCstr(grow.Cells(colActualstatus).Value)
                    objtrp.StandardRate = clsCommon.myCdbl(grow.Cells(colSTDRate).Value)
                    If clsCommon.myLen(objtrp.paramcode) > 0 Then
                        obj.Arr_Param.Add(objtrp)
                    End If
                Next
                '-----------------------------------------------------------------------

                '---------------------------Purchase Parameter Range----------------------------
                If SettItemWiseQualityCheckInGeneralPurchase Then
                    If ChkAllowQC.Checked Then
                        obj.Arr_Purchase_QC_Parameter = New List(Of clsItemPurchaseQCParameter)
                        Dim counter As Integer = 0
                        For Each grow As GridViewRowInfo In gvPurQCPar.Rows
                            counter += 1
                            Dim objtrp As New clsItemPurchaseQCParameter()
                            objtrp.SNo = counter
                            objtrp.QC_Code = clsCommon.myCstr(grow.Cells(colPurQCParamCode).Value)
                            objtrp.Specifications = clsCommon.myCstr(grow.Cells(colPurQCParamSpecification).Value)
                            If clsCommon.myLen(objtrp.QC_Code) > 0 Then
                                obj.Arr_Purchase_QC_Parameter.Add(objtrp)
                            End If
                        Next
                        If obj.Arr_Purchase_QC_Parameter Is Nothing OrElse obj.Arr_Purchase_QC_Parameter.Count <= 0 Then
                            Throw New Exception("Please select Puchase QC Parametrs on QC Parameters Tab")
                        End If
                    End If
                End If

                '-----------------------------------------------------------------------
                obj.ArrSchedule = New List(Of clsItemSchedule)
                For Each grow As GridViewRowInfo In gvSchedule.Rows
                    Dim objtrp As New clsItemSchedule()
                    objtrp.Days = clsCommon.myCDecimal(grow.Cells(ColScheduleDays).Value)
                    objtrp.Qty_Per = clsCommon.myCDecimal(grow.Cells(ColSchedulePerQty).Value)
                    objtrp.Short_Per = clsCommon.myCDecimal(grow.Cells(ColSchedulePerShort).Value)
                    objtrp.Late_Days = clsCommon.myCDecimal(grow.Cells(ColScheduleLateDays).Value)
                    objtrp.Arr = TryCast(grow.Cells(ColScheduleLateDays).Tag, List(Of clsItemSchedulePenalty))
                    If objtrp.Days > 0 AndAlso objtrp.Qty_Per > 0 Then
                        obj.ArrSchedule.Add(objtrp)
                    End If
                Next

                If obj.SaveDataRMOther(obj, GetDatabase(), isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
                    UcAttachment1.SaveData(txtCode.Value)
                End If
                '========================Rohit Add COde to save Image==================================
                If clsCommon.myLen(File_Name) > 0 Then
                    Dim bData As Byte()
                    Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(File_Name)))
                    bData = br.ReadBytes(br.BaseStream.Length)
                    Dim Str As String = " UPDATE TSPL_Item_Master set Item_Image = @BLOBData where Item_CODE='" + txtCode.Value + "'"
                    Dim cmd As SqlCommand = New SqlCommand(Str, clsDBFuncationality.GetConnnection)
                    Dim prm As New SqlParameter("@BLOBData", bData)
                    cmd.Parameters.Add(prm)
                    cmd.ExecuteNonQuery()
                    br.Close() ' done by stuti reagrding memory leakage
                End If
                '=============================================

                LoadData(obj.Item_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Function GetDatabase() As List(Of String)
        Dim ArrDB As New List(Of String)()
        ArrDB.Add(objCommonVar.CurrDatabase)
        Dim arr As ArrayList = cbgDatabase.CheckedValue

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For ii As Integer = 0 To arr.Count - 1
                ArrDB.Add(clsCommon.myCstr(arr(ii)))
            Next
        End If
        Return ArrDB
    End Function
    Public Sub ItemShortDesp()
        Try
            Dim col As New AutoCompleteStringCollection
            Dim strquery As String = "select isnull(Short_Description,'') As Short_Description  from tspl_item_master"
            Dim ds As DataTable
            'Dim strvalue As String
            ds = clsDBFuncationality.GetDataTable(strquery)
            Dim comp As Integer
            For comp = 0 To ds.Rows.Count - 1
                col.Add(ds.Rows(comp).Item(0))

            Next
            txtShortDescription.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtShortDescription.AutoCompleteCustomSource = col
            txtShortDescription.AutoCompleteMode = AutoCompleteMode.Suggest

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If chkDoNotCheckOnSave.Checked Then
                Return True
            End If
            AllowTo = True
            Dim ShortDesp As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row from tspl_item_master Where (Short_Description ='" & clsCommon.myCstr(txtShortDescription.Text) & "' and LEN(ISNULL( Short_Description,'')) > 0) AND Item_Code <>'" & clsCommon.myCstr(txtCode.Value) & "'"))
            'If clsCommon.myLen(txtCode.Value) <= 0 AndAlso txtCode.MyReadOnly = False Then
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    clsCommon.MyMessageBoxShow("Please Enter item Code", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            '    txtCode.Focus()
            '    Return False
            'Else
            If clsCommon.myLen(txtDesc.Text) <= 0 AndAlso txtDesc.ReadOnly = False Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please Enter Item Description", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtDesc.Focus()
                Return False
            ElseIf chkFresh.Checked = True AndAlso chkAmbient.Checked = True AndAlso Not (chkFresh.Enabled = False AndAlso chkAmbient.Enabled = False) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "You cannot select Fresh item/Ambient at a time, Please select either fresh Item or Ambient.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                Return False

                'ElseIf chkFresh.Checked = False AndAlso chkAmbient.Checked = False AndAlso Not (chkFresh.Enabled = False AndAlso chkAmbient.Enabled = False) Then
                'RadPageView1.SelectedPage = RadPageViewPage1
                'clsCommon.MyMessageBoxShow("Please select either fresh Item or Ambient.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                'Return False

                '' Anubhooti 10-Sep-2014 BM00000003847
            ElseIf chkFresh.Checked = True AndAlso clsCommon.myLen(txtShortDescription.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please fill short description", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtShortDescription.Focus()
                Return False
            ElseIf ShortDesp > 0 AndAlso AllowDuplicateItemShortDescriptionInItemMaster = False Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please check ! short description should not be duplicate", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtShortDescription.Focus()
                Return False
            ElseIf clsCommon.myLen(txtStructurer.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Structure Code", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtStructurer.Focus()
                Return False
            ElseIf clsCommon.myLen(txtPurchaseACSet.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Purchase Account set", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtPurchaseACSet.Focus()
                Return False
            ElseIf clsCommon.myLen(txtSaleAcSet.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Sale Account set", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtSaleAcSet.Focus()
                Return False
                'ElseIf clsCommon.myLen(txtCategory.Value) <= 0 Then
                '    clsCommon.MyMessageBoxShow("Please select Category", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    txtCategory.Focus()
                '    Return False
                'ElseIf clsCommon.myLen(txtSubCategory.Value) <= 0 Then
                '    clsCommon.MyMessageBoxShow("Please select Sub category", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    txtSubCategory.Focus()
                '    Return False

            ElseIf clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                cboItemType.Focus()
                Return False
            ElseIf clsCommon.myLen(cboItemType.SelectedValue) > 0 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, Nothing)) = 1 Then
                Dim qry As String = " SELECT Count( PREFIX ) FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE = '" + cboItemType.SelectedValue + "' and PREFIX is not null and PREFIX <> ''"
                Dim isPrefixExit As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
                If isPrefixExit = False Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    clsCommon.MyMessageBoxShow(Me, "Please Enter Prefix of Item Type (" + cboItemType.Text + ") in Inventory Setting screen.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    cboItemType.Focus()
                    Return False
                End If
            ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "F") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtWeightUOM.Value) = 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Weight UOM", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtWeightUOM.Focus()
                Return False
            ElseIf clsCommon.CompairString(cboItemType.SelectedValue, "F") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(txtWeightValue.Value) = 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(Me, "Please select Weight Value", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtWeightValue.Focus()
                Return False
                'ElseIf clsCommon.myLen(cmbUsedAs.SelectedValue) <= 0 Then
                '    RadPageView1.SelectedPage = RadPageViewPage1
                '    clsCommon.MyMessageBoxShow("Please select Used as", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    cmbUsedAs.Focus()
                '    Return False
                'ElseIf clsCommon.myLen(cboType.SelectedValue) <= 0 Then
                '    clsCommon.MyMessageBoxShow("Please select Type", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    cboType.Focus()
                '    Return False
                'ElseIf CreateGLAccToItem = True AndAlso clsCommon.myLen(fndGLAcc.Value) <= 0 Then
                '    RadPageView1.SelectedPage = RadPageViewPage1   
                '    clsCommon.MyMessageBoxShow("Please select GL account", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    fndGLAcc.Focus()
                '    Return False
            End If

            If chkInsurance.Checked = True Then
                If clsCommon.myLen(txtInsurance.Text) = 0 Then
                    myMessages.blankValue("Insurance No")
                    txtInsurance.Focus()
                    Return False
                End If
                If txtFromDate.Value.Date > txtToDate.Value.Date Then
                    clsCommon.MyMessageBoxShow(Me, "Insurance To date can not be before than from date.")
                    txtToDate.Focus()
                    Return False
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing)), "A") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtWarranty.Value) > 0 AndAlso clsCommon.myLen(CmbWarrApp.SelectedValue) <= 0 Then
                    RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                    clsCommon.MyMessageBoxShow(Me, "Please select warranty applied from", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    CmbWarrApp.Focus()
                    Return False
                End If
            End If
            '=================Added by preeti Gupta Against ticket no[BHA/03/07/19-000920]============
            If ShelfLifeManadatoryOnFG Then
                If clsCommon.myLen(txt_shelflife.Text) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Shelf life", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    txt_shelflife.Focus()
                    Return False
                End If
            End If
            '========================================================================
            ''=========================================================================================================
            If ToleranceMandatoryFor_RM_Other_Trade AndAlso clsCommon.myCdbl(txt_tolerance.Text) = 0 AndAlso (clsCommon.CompairString(cboItemType.SelectedValue, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "O") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "T") = CompairStringResult.Equal) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txt_tolerance.Focus()
                txt_tolerance.Select()
                clsCommon.MyMessageBoxShow(Me, "Please fill tolerance%.", Me.Text)
                Return False
            End If
            ''=========================================================================================================
            If TxtProdTolerance.Value < 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                TxtProdTolerance.Focus()
                TxtProdTolerance.Select()
                clsCommon.MyMessageBoxShow(Me, "Production Tolerance should be non negative.", Me.Text)
                Return False
            End If

            If chkIsSerailzedInventory.Checked = True Then
                If clsCommon.myLen(txtNextAutoSerialCounter.Text) < 4 Then
                    clsCommon.MyMessageBoxShow(Me, "Please check Next Auto Serial No.Its Length should be minimum 4.", Me.Text)
                    Return False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbUsedAs.SelectedValue), "S") = CompairStringResult.Equal And chkIsSerailzedInventory.Checked = True Then
                    clsCommon.MyMessageBoxShow(Me, "You can either make Item serialized or Mcc Sale(used as).", Me.Text)
                    Return False
                End If
            End If

            If chkScrapItem.Checked = True Then
                If clsCommon.myLen(fndScrapItem.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please fill Scrap Item.", Me.Text)
                    fndScrapItem.Focus()
                    Return False
                End If
                If clsCommon.myLen(fndScrapItem.Value) > 0 Then
                    Dim chkStructCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count (*)  from tspl_item_master where Item_Code = '" + fndScrapItem.Value + "' and Structure_Code = '" + txtStructurer.Value + "'"))
                    If chkStructCode <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Structure Code of Item Code and Scrap Item Should be Same.", Me.Text)
                        fndScrapItem.Value = ""
                        fndScrapItem.Focus()
                        Return False
                    End If

                End If
            End If

            Dim isMultipleUOM As Boolean = False
            If True Then
                'txtUOM.Value = ""
                Dim strUOM As String = ""
                Dim CountDefaultUnit As Integer = 0
                Dim isCustomConversion As Boolean = False
                For ii As Integer = 0 To gvUOM.RowCount - 1
                    If clsCommon.myLen(gvUOM.Rows(ii).Cells(UOMColUnit).Value) > 0 Then
                        If clsCommon.CompairString("Y", clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColStockUnit).Value)) = CompairStringResult.Equal Then
                            If gvUOM.Rows(ii).Cells(UOMColConvFact).Value <> 1 Then
                                RadPageView1.SelectedPage = RadPageViewPage2
                                Throw New Exception("The Coversion Unit Should be [1] for Stocking Unit [Yes]")
                            End If
                            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemCostMandatoryForStockingUnit, clsFixedParameterCode.AllowItemCostMandatoryForStockingUnit, Nothing)) = 1, True, False) = True Then
                                If clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMItemCost).Value) <= 0 Then
                                    Throw New Exception("Item Cost Can not be blank Or Zero for Stocking Unit [Yes].")
                                End If
                            End If
                            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                                'RadPageView1.SelectedPage = RadPageViewPage2
                                'Throw New Exception("There should be only one stock unit")

                                txtUOM.Value = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value)
                                lblUOM.Text = clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnitDesc).Value)
                            End If
                        Else
                            If clsCommon.myCdbl(gvUOM.Rows(ii).Cells(UOMColConvFact).Value) <= 0 Then
                                RadPageView1.SelectedPage = RadPageViewPage2
                                Throw New Exception("The Convrsion Factor Should be greater than Zero.")
                            End If
                        End If
                        'added by richa agarwal against ticket no BM00000004327
                        If gvUOM.Rows(ii).Cells(UOMDefault).Value = True Then
                            CountDefaultUnit = CountDefaultUnit + 1
                        End If
                        ''===========================================
                        If clsCommon.myLen(strUOM) > 0 Then
                            strUOM += ","
                            isMultipleUOM = True
                        End If
                        strUOM += "'" + clsCommon.myCstr(gvUOM.Rows(ii).Cells(UOMColUnit).Value) + "'"
                        If AllowItemConversionAutomation = 1 Then
                            ItemConversionAutomation(ii)
                        End If
                        If clsCommon.myCBool(gvUOM.Rows(ii).Cells(UOMCustomConversion).Value) Then
                            If isCustomConversion Then
                                Throw New Exception("Not more than one UOM Can be of Custom Conversion Type")
                            End If
                            isCustomConversion = True
                        End If
                    End If
                Next
                If clsCommon.myLen(strUOM) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Throw New Exception("Please Enter UOM Details")
                End If
                'added by richa agarwal against ticket no BM00000004327
                If CountDefaultUnit > 1 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Throw New Exception("Default UOM should be 1")
                End If
                ''===============
                Dim dt As DataTable
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    dt = clsDBFuncationality.GetDataTable(strQtyForIsUOMUsed(strUOM, False))
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage2
                        Throw New Exception("This Record Cannot be changed." + Environment.NewLine + "This is already in Process")
                    End If
                End If


                If chkIsSerailzedInventory.Checked Then
                    If clsCommon.myLen(txtNextAutoSerialCounter.Text) <= 0 Then
                        RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                        clsCommon.MyMessageBoxShow(Me, "Please Enter Next Serialzed counter", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
                        txtNextAutoSerialCounter.Focus()
                        Return False
                    End If
                    If clsCommon.CompairString(txtNextAutoSerialCounter.Text, clsCommon.incval(txtNextAutoSerialCounter.Text)) = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                        clsCommon.MyMessageBoxShow(Me, "Not a valid Serialzed counter", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                        txtNextAutoSerialCounter.Focus()
                        Return False
                    End If
                    If clsCommon.myLen(txtWarranty.Value) <= 0 Then
                        RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                        clsCommon.MyMessageBoxShow(Me, "Please select warranty", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                        txtWarranty.Focus()
                        Return False
                    End If

                    If isMultipleUOM Then
                        RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                        clsCommon.MyMessageBoxShow(Me, "Multiple UOM is not applicable for serial item", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                        chkIsSerailzedInventory.Focus()
                        Return False
                    End If
                End If
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    dt = clsDBFuncationality.GetDataTable(strQtyForIsUOMUsed("", False))
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim isSerialInventoryOLD As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Serial_Item from TSPL_ITEM_MASTER where Item_Code='" + txtCode.Value + "'")) = 1, True, False)
                        If Not isSerialInventoryOLD = chkIsSerailzedInventory.Checked Then
                            RadPageView1.SelectedPage = pageviewSerializedIinvenoty
                            clsCommon.MyMessageBoxShow(Me, "Can't change the serialization because item is in use.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                            chkIsSerailzedInventory.Focus()
                            Return False
                        End If
                    End If
                End If

            End If

            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage2
                clsCommon.MyMessageBoxShow(Me, "Please select UOM", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                txtUOM.Focus()
                Return False
            End If
            UcCustomFields1.AllowToSave()

            '-----------------check for n-level category------------------------------------------------
            'If clsCommon.myLen(auto_icode_seperator) > 0 AndAlso clsCommon.myLen(txtCategoryStructureCode.Value) <= 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage4
            '    clsCommon.MyMessageBoxShow("Fill Category Structure", Me.Text)
            '    txtCategoryStructureCode.Focus()
            '    txtCategoryStructureCode.Select()
            '    Return False
            'End If
            If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                Dim ii As Integer = 0
                Dim ij As Integer = 0
                Dim categcode As String = ""
                Dim valuecatcode As String = ""
                Dim mastercounter As Integer = 0
                Dim skucounter As Integer = 0

                For ii = 0 To gvCategory.Rows.Count - 1
                    ij = ii + 1
                    Try
                        categcode = gvCategory.Rows(ii).Cells(CatcolCode).Value
                    Catch ex2 As Exception
                        categcode = ""
                    End Try
                    Try
                        valuecatcode = gvCategory.Rows(ii).Cells(CatcolValue).Value
                    Catch ex1 As Exception
                        valuecatcode = ""
                    End Try

                    'If clsCommon.myCBool(gvCategory.Rows(ii).Cells(Catcolmaster).Value) = True Then
                    '    mastercounter += 1
                    'End If

                    'If clsCommon.myCBool(gvCategory.Rows(ii).Cells(CatcolSKU).Value) = True Then
                    '    skucounter += 1
                    'End If

                    If ii = 0 AndAlso clsCommon.myLen(CatcolCode) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage4
                        clsCommon.MyMessageBoxShow(Me, "Please fill category code in Category Structure at row no. " + clsCommon.myCstr(ij) + "", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                        Return False
                    End If
                    If clsCommon.myLen(categcode) > 0 AndAlso clsCommon.myLen(valuecatcode) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage4
                        clsCommon.MyMessageBoxShow(Me, "Please fill category values in Category Structure at row no. " + clsCommon.myCstr(ij) + "", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                        Return False
                    End If
                Next
                'If (mastercounter <= 0 OrElse mastercounter > 1) Then
                '    RadPageView1.SelectedPage = RadPageViewPage4
                '    clsCommon.MyMessageBoxShow("Please set any one as master value of category.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    Return False
                'End If

                'If (skucounter <= 0 OrElse skucounter > 1) Then
                '    RadPageView1.SelectedPage = RadPageViewPage4
                '    clsCommon.MyMessageBoxShow("Please set any one as sku value of category.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                '    Return False
                'End If
            End If
            '' check for product type
            Dim Product_Type As String = clsItemMaster.GetItemProductType(txtCode.Value, Nothing)
            If clsCommon.CompairString(fndProductType.SelectedValue, Product_Type) <> CompairStringResult.Equal Then
                If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                    Dim qry As String = "select Count(*) from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & txtCode.Value & "'"
                    Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If totalCount > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Product Type of this item can not be changed because some transactions are already done for this item in Product Type Milk.")
                        Return False
                    End If
                ElseIf clsCommon.CompairString(fndProductType.SelectedValue, "MI") = CompairStringResult.Equal Then
                    Dim qry As String = "select Count(*) from TSPL_INVENTORY_MOVEMENT where Item_Code='" & txtCode.Value & "'"
                    Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If totalCount > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Product Type of this item can not be changed because some transactions are already done for this item.")
                        Return False
                    End If
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MP") = CompairStringResult.Equal Then
                If clsCommon.myLen(gv_param.Rows(0).Cells(colparamCode).Value) = 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill QC Parameters.")
                    RadPageView1.SelectedPage = RadPageViewPage5
                    Return False
                End If
            End If
            Dim paramcode As String = ""
            Dim paramlrange As Decimal = Nothing
            Dim paramurange As Decimal = Nothing
            Dim paramstatus As String = Nothing
            Dim paramvalue1 As String = Nothing
            Dim paramvalue2 As String = Nothing
            Dim paramcode1 As String = ""
            Dim act_range As Decimal = Nothing
            Dim act_value As String = Nothing
            Dim act_status As String = Nothing
            Dim ismentdatory As Integer = 0


            For ii As Integer = 0 To gv_param.Rows.Count - 1
                paramcode = clsCommon.myCstr(gv_param.Rows(ii).Cells(colparamCode).Value)
                paramlrange = clsCommon.myCdbl(gv_param.Rows(ii).Cells(colparamLower).Value)
                paramurange = clsCommon.myCdbl(gv_param.Rows(ii).Cells(colparamUpper).Value)
                paramstatus = clsCommon.myCstr(gv_param.Rows(ii).Cells(colparamStatus).Value)
                paramvalue1 = clsCommon.myCstr(gv_param.Rows(ii).Cells(colparamValue1).Value)
                paramvalue2 = clsCommon.myCstr(gv_param.Rows(ii).Cells(colparamValue2).Value)
                act_range = clsCommon.myCdbl(gv_param.Rows(ii).Cells(colActualRange).Value)
                act_status = clsCommon.myCstr(gv_param.Rows(ii).Cells(colActualstatus).Value)
                act_value = clsCommon.myCstr(gv_param.Rows(ii).Cells(colActualvalue).Value)

                qry = "select isnull(IsMandatory,0) as IsMandatory from TSPL_PARAMETER_MASTER where code='" + paramcode + "'"
                ismentdatory = CInt(clsDBFuncationality.getSingleValue(qry))

                If clsCommon.myLen(paramcode) > 0 And ismentdatory = 1 Then
                    If paramlrange > 0 AndAlso paramurange > 0 AndAlso (act_range > paramurange OrElse act_range < paramlrange) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard range should meet the range of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramvalue1) > 0 AndAlso clsCommon.myLen(act_value) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Fill Standard value of QC at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramvalue1) > 0 AndAlso Not paramvalue1.Contains(act_value) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard value should meet the value of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramstatus) > 0 AndAlso clsCommon.CompairString(paramstatus, act_status) <> CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard status should meet the status of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If
                ElseIf clsCommon.myLen(paramcode) > 0 Then
                    If paramlrange > 0 AndAlso paramurange > 0 AndAlso act_range > 0 AndAlso (act_range > paramurange OrElse act_range < paramlrange) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard range should meet the range of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    ElseIf paramlrange > 0 AndAlso (clsCommon.myLen(act_status) > 0 OrElse clsCommon.myLen(act_value) > 0) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard range should meet the range of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramvalue1) > 0 AndAlso clsCommon.myLen(act_value) > 0 AndAlso Not paramvalue1.Contains(act_value) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard value should meet the value of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    ElseIf clsCommon.myLen(paramvalue1) > 0 AndAlso (act_range > 0 OrElse clsCommon.myLen(act_status) > 0) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard value should meet the value of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramstatus) > 0 AndAlso clsCommon.myLen(act_status) > 0 AndAlso clsCommon.CompairString(paramstatus, act_status) <> CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard status should meet the status of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    ElseIf clsCommon.myLen(paramstatus) > 0 AndAlso (act_range > 0 OrElse clsCommon.myLen(act_value) > 0) Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Standard status should meet the status of QC,see at line no." + clsCommon.myCstr(ii + 1) + "")
                    End If

                End If

                For jj As Integer = CInt(ii) + 1 To gv_param.Rows.Count - 1
                    paramcode1 = clsCommon.myCstr(gv_param.Rows(jj).Cells(colparamCode).Value)

                    If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(paramcode, paramcode1) = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage5
                        Throw New Exception("Duplicate paramter value not allowed at row no. " + clsCommon.myCstr(jj + 1) + ".")
                    End If
                Next
            Next
            Dim isUomKGExist As Boolean = False
            Dim strProductionFATSNF_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, Nothing))
            '' Anubhooti 11-Sep-2014 BM00000003891 Duplication Check
            For i As Integer = 0 To gvUOM.Rows.Count - 1
                If clsCommon.myLen(gvUOM.Rows(i).Cells(UOMColUnit).Value) > 0 Then
                    Dim UOM As String = gvUOM.Rows(i).Cells(UOMColUnit).Value
                    If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                        If clsCommon.CompairString(UOM, strProductionFATSNF_KG_Unit) = CompairStringResult.Equal Then
                            isUomKGExist = True
                        End If
                    End If
                    For j As Integer = i + 1 To gvUOM.Rows.Count - 1
                        Dim SecondUOM As String = gvUOM.Rows(j).Cells(UOMColUnit).Value
                        If UOM = SecondUOM Then
                            clsCommon.MyMessageBoxShow(Me, "Please check ! duplicate UOM in grid")
                            Return False
                        End If
                    Next
                End If
            Next
            If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MP") = CompairStringResult.Equal Then
                    If isUomKGExist = False Then
                        Throw New Exception("If Item is of MP or Milk type then " + strProductionFATSNF_KG_Unit + " must be defined as one of the conversion in Item Master.")
                    End If
                End If
            End If
            ''
            '-------'' Pankaj jha against ticket no BM00000003905 on dated: 13-09-2014 for adding Seal Type of Item with serialized inventory
            If clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MS") = CompairStringResult.Equal Then
                If Not chkIsSerailzedInventory.Checked Then
                    clsCommon.MyMessageBoxShow(Me, "When Item Type is Paper Seal Or Manual Seal, It Must be serialized")
                    Return False
                End If
            End If
            Dim recCount As Integer
            If clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "PS") = CompairStringResult.Equal Then
                recCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Product_Type='PS' and   item_code<>'" & txtCode.Value & "'"))
                If recCount > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Paper Seal Type of item can not be created twice")
                    Return False
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(fndProductType.SelectedValue), "MS") = CompairStringResult.Equal Then
                recCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Product_Type='MS' and   item_code<>'" & txtCode.Value & "'"))
                If recCount > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Manual Seal Type of item can not be created twice")
                    Return False
                End If
            End If
            'If recCount > 1 AndAlso clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("Seal No Type of item can not be created twice")
            '    Return False
            'End If
            If clsCommon.myLen(txtSeqNo.Text) > 0 And clsCommon.myCdbl(txtSeqNo.Text) > 0 Then
                recCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where sku_seq=" & clsCommon.myCdbl(txtSeqNo.Text) & " and   item_code<>'" & txtCode.Value & "'"))
                If recCount > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Duplicate Sequence No", Me.Text)
                    Return False
                End If
            End If

            If clsCommon.myLen(txtMarSeqNo.Text) > 0 And clsCommon.myCdbl(txtMarSeqNo.Text) > 0 Then
                recCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Marketing_Seq=" & clsCommon.myCdbl(txtMarSeqNo.Text) & " and   item_code<>'" & txtCode.Value & "'"))
                If recCount > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Duplicate Marketing Sequence No", Me.Text)
                    Return False
                End If
            End If
            If rbtnBBAmount.IsChecked Then
                If clsCommon.myLen(txtBBValue.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Amount cann't be empty", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    txtBBValue.Focus()
                    Return False
                ElseIf clsCommon.myCDecimal(txtBBValue.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Amount must be greater then Zero", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    txtBBValue.Focus()
                    Return False
                End If
            End If
            If rbtnBBPer.IsChecked Then
                If clsCommon.myLen(txtBBValue.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Percentage cann't be empty", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    txtBBValue.Focus()
                    Return False
                ElseIf clsCommon.myCDecimal(txtBBValue.Text) <= 0 Or clsCommon.myCDecimal(txtBBValue.Text) > 100 Then
                    clsCommon.MyMessageBoxShow(Me, "Percentage must be Between 1 to 100", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                    txtBBValue.Focus()
                    Return False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            AllowTo = False
            Return False
        End Try



        ''====================27/06/2016=====If user have csa module rights and item-wise accounting is ON then panel is visible========================================================
        qry = "select isavailable from TSPL_MODULE_PERMISSION where module_name='" + clsUserMgtCode.ModuleCSASale + "'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowItemWiseCSAAccountingON_CSASale, clsFixedParameterCode.AllowItemWiseCSAAccountingON_CSASale, Nothing)), "1") = CompairStringResult.Equal Then
            If clsCommon.myLen(fndCSA_AC_Code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                fndCSA_AC_Code.Focus()
                fndCSA_AC_Code.Select()
                clsCommon.MyMessageBoxShow(Me, "Please select CSA Account Set.", Me.Text)
                Return False
            End If
        End If
        '==========================================================================
        If AllowGSTApplicable = True Then
            '    Dim qry As String = clsDBFuncationality.getSingleValue("Select Item_code from tspl_item_master where HSN_Code='" & FndHSNCode.Value & "' and Item_Code not in ('" & txtCode.Value & "')")
            '    If clsCommon.myLen(qry) > 0 Then
            '        clsCommon.MyMessageBoxShow("Already Mapped to another Item.")
            '        Return False
            '    End If
            If chkTaxable.Checked = True Then
                If clsCommon.myLen(FndHSNCode.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill HSN Code.", Me.Text)
                    FndHSNCode.Focus()
                    FndHSNCode.MendatroryField = True
                    Return False
                End If
            End If
        End If


        Return True
        AllowTo = False
    End Function
    Function strQtyForIsUOMUsed(ByVal strUOM As String, ByVal isUOMInclude As Boolean) As String
        Dim strUOMIncludeOrExclude = " not in "
        If isUOMInclude Then
            strUOMIncludeOrExclude = " in "
        End If
        Dim isIncludeUOM As Boolean = True
        If clsCommon.myLen(strUOM) <= 0 Then
            isIncludeUOM = False
        End If

        Dim qry As String = " select Unit_Code from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_IssueReturn_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union"
        qry += " select Unit_Code from TSPL_SCRAPINVOICE_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_SCRAPSALE_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_PI_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_REQUISITION_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_PURCHASE_ORDER_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_RGP_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_SRN_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ") ", "")
        qry += " Union "
        qry += " select Unit_Code from TSPL_PR_DETAIL where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Unit_Code " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += "select UOM from TSPL_INVENTORY_MOVEMENT where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and UOM " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        qry += " Union "
        qry += "select Stock_UOM from TSPL_INVENTORY_MOVEMENT where Item_Code='" + txtCode.Value + "' " + IIf(isIncludeUOM, " and Stock_UOM " + strUOMIncludeOrExclude + " (" + strUOM + ")", "")
        Return qry
    End Function
    Private Sub gvUOM_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvUOM.CellFormatting
        If e.Column Is gvUOM.Columns(UOMItemCost) Then
            If clsCommon.CompairString(clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColStockUnit).Value), "Y") = CompairStringResult.Equal Then
                gvUOM.CurrentRow.Cells(UOMColStockUnit).ReadOnly = False
            Else
                gvUOM.CurrentRow.Cells(UOMColStockUnit).ReadOnly = True
            End If
        End If

    End Sub
    Private Sub gvUOM_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvUOM.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvUOM.Columns(UOMColUnit) OrElse e.Column Is gvUOM.Columns(UOMColConvFact) Then
                        If e.Column Is gvUOM.Columns(UOMColUnit) Then
                            Dim i As Integer = e.RowIndex
                            OpenUOMCodeList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenUOMCodeList(ByVal isButtonClick As Boolean)
        gvUOM.CurrentRow.Cells(UOMColUnitDesc).Value = ""
        gvUOM.CurrentRow.Cells(UOMColConvFact).Value = 0
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description,Conv_Factor as [Conversion Factor] from TSPL_UNIT_MASTER"
        gvUOM.CurrentRow.Cells(UOMColUnit).Value = clsCommon.ShowSelectForm("IMRMUOM", qry, "Code", "", clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvUOM.CurrentRow.Cells(UOMColUnit).Value) > 0 Then
            qry = "select Unit_Desc,Conv_Factor from TSPL_UNIT_MASTER  WHERE Unit_Code ='" + clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvUOM.CurrentRow.Cells(UOMColQty).Value = "1"
                gvUOM.CurrentRow.Cells(UOMColEqual).Value = "="
                gvUOM.CurrentRow.Cells(UOMColUnitDesc).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Desc"))
                gvUOM.CurrentRow.Cells(UOMColConvFact).Value = clsCommon.myCdbl(dt.Rows(0)("Conv_Factor"))
                If AllowItemConversionAutomation = 1 Then
                    Try
                        ItemConversionAutomation(gvUOM.CurrentRow.Index)
                    Catch ex As Exception
                        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        gvUOM.CurrentRow.Cells(UOMColUnit).Value = ""
                    End Try

                End If
            End If
        End If
    End Sub
    Private Sub gvUOM_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvUOM.CurrentColumnChanged
        If gvUOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvUOM.CurrentRow.Index
            If intCurrRow = gvUOM.Rows.Count - 1 Then
                gvUOM.Rows.AddNew()
                gvUOM.CurrentRow = gvUOM.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If UpdateItemMasterConversationWithoutValidation = True Then
            clsFixedParameter.UpdateData("UpdateItemMasterConversationWithoutValidation", "UpdateItemMasterConversationWithoutValidation", "0", Nothing)
        End If
        Me.Close()
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from tspl_item_master where item_code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select item_code as [Code], item_desc as [Description],ITF_CODE as [ITF CODE] from tspl_item_master "
            'txtCode.Value = clsCommon.ShowSelectForm("IMROCodeFinder", qry, "Code", "Item_Type in ('R','O','A','F')", txtCode.Value, "", isButtonClicked)
            'Ticket No : ERO/12/06/19-000643 By Prabhakar
            txtCode.Value = clsItemMaster.getFinderForActiveAndIncative("", txtCode.Value, isButtonClicked) 'Item_Type in ('R','O','A','F')
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
        'If clsCommon.myLen(auto_icode_seperator) > 0 Then '---------------if item code is auto generated
        '    txtCode.MyReadOnly = True
        'End If
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        Try
            BlankAllConrols()

            IsFormLoaded = False

            Dim obj As clsItemMaster = clsItemMaster.GetDataRMOther(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                isNewEntry = False
                isInsideLoadData = True
                txtCode.MyReadOnly = True
                '' Anubhooti 21-Oct-2014
                LoadItemCSAType()
                txtCode.Value = obj.Item_Code
                txtDesc.Text = obj.Item_Desc
                txtItemDescHindi.Text = obj.Item_Desc_Hindi
                txtdrawing_no.Text = obj.Drawing_No
                txtPartNo.Value = obj.Part_No
                txt_shelflife.Text = clsCommon.myCstr(obj.shelflife)
                numMinSelfLife.Text = clsCommon.myCstr(obj.Min_shelf_life)
                If clsCommon.myLen(txtPartNo.Value) > 0 Then
                    LoadDataPartNo(txtPartNo.Value, NavigatorType.Current)
                End If
                txtShortDescription.Text = obj.Item_Short_Desc
                txtShortDescHindi.Text = obj.Item_Short_Desc_Hindi
                txtAliesName.Text = obj.Alies_Name
                txtAliesNameHindi.Text = obj.Alies_Name_Hindi
                txtAliesName2.Text = obj.Alies_Name2
                txtAliesName3.Text = obj.Alies_Name3
                chkCAN.Checked = obj.Can
                ChkCrate.Checked = obj.Crate
                txtstnd_pur_rate.Text = obj.std_pur_rate
                txtUOM.Value = obj.Unit_Code
                txtCost.Value = obj.Cost
                'Load buyBackTye and Value
                If obj.BuyBackType = 1 Then
                    rbtnBBAmount.IsChecked = True
                    txtBBValue.Text = obj.BuyBackValue
                    lblBBValue.Visible = True
                    lblBBValue.Text = "Amount"
                    txtBBValue.Visible = True
                ElseIf obj.BuyBackType = 2 Then
                    rbtnBBAmount.IsChecked = True
                    txtBBValue.Text = obj.BuyBackValue
                    lblBBValue.Visible = True
                    lblBBValue.Text = "Precentage"
                    txtBBValue.Visible = True
                Else
                    rbtnBBNA.IsChecked = True
                    lblBBValue.Visible = False
                    txtBBValue.Visible = False
                End If
                'txt_tolerance.Value = obj.Tolerance
                TxtProdTolerance.Value = obj.Production_Tolerance
                fndChptr.Value = obj.Cheapter_Heads
                txtCategoryStructureCode.Value = obj.Item_Category_Struct_Code
                lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "'"))
                txtRate.Value = obj.Rate
                txtAssetLife.Value = obj.Asset_Life
                txtWarrantyPeriod.Value = obj.Warranty_period
                txtLastModiDate.Text = obj.Modify_Date
                txtItemSpecification.Text = obj.ItemSpecification
                chkActive.Checked = obj.Active
                txtAlternativeItem.Value = obj.AlternativeItem
                lblAlternativeItemName.Text = clsItemMaster.GetItemName(txtAlternativeItem.Value, Nothing)
                lblchptrdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_CHAPTER_HEAD where Chapter_head_code ='" + fndChptr.Value + "'"))

                fndCSA_AC_Code.Value = obj.Cust_Account
                txtCSA_AC_Name.Text = obj.Cust_Account_Name
                chkFGforCF.Checked = IIf(obj.FG_for_CF = 1, True, False)
                If chkFGforCF.Checked = True Then
                    txtBmBdQty.Text = obj.BomBuildQty
                End If
                chkNIRQC.Checked = obj.NIR_QC
                chkSchemeItem.Checked = obj.Is_Scheme_Item
                txtDistbtr_Amt.Text = obj.Distributor_Commission
                txtCNF_Amt.Text = obj.CNF_Commission
                txtCorrectionFactor.Value = obj.Correction_Factor
                chkIsReqBatch.Checked = obj.Is_Batch_Item
                chkRAL.Checked = obj.RAL
                ''----- GST Function
                If AllowGSTApplicable = True Then
                    FndHSNCode.Value = obj.HSNCode
                    lblHSNDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HSN_Master where Code ='" + FndHSNCode.Value + "'"))
                End If
                '-----End
                lblUOM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code ='" + txtUOM.Value + "'"))
                txtStructurer.Value = obj.Structure_Code
                lblStructurer.Text = obj.Structure_Desc
                txtPurchaseACSet.Value = obj.Purchase_Class_Code
                lblPurchaseACSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Purchase_Class_Desc  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + txtPurchaseACSet.Value + "'"))
                txtSaleAcSet.Value = obj.Sale_Class_Code
                lblSaleAcSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Desc  from TSPL_SALES_ACCOUNTS where Sales_Class_Code='" + txtSaleAcSet.Value + "'"))
                txtCategory.Value = obj.item_category
                lblCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select category_name from tspl_Item_category where category_code='" + txtCategory.Value + "'"))
                txtSubCategory.Value = obj.Sub_item_category
                lblSubCategory.Text = clsDBFuncationality.getSingleValue("select Description  from TSPL_ITEM_SUB_CATEGORY where Sub_Category_Code='" + txtSubCategory.Value + "'")
                cboType.SelectedValue = obj.TypeOfItm
                cboItemType.SelectedValue = obj.Item_Type

                If ToleranceMandatoryFor_RM_Other_Trade AndAlso (clsCommon.CompairString(cboItemType.SelectedValue, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "O") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "T") = CompairStringResult.Equal) Then
                    txt_tolerance.MendatroryField = True
                Else
                    txt_tolerance.MendatroryField = False
                End If
                txt_tolerance.Value = obj.Tolerance
                cmbUsedAs.SelectedValue = obj.Item_used_as
                txtSeqNo.Text = obj.Sku_Seq
                chkMorning.Checked = obj.Morning
                chkChilledFreezen.Checked = obj.Chilled_Freezen
                chkTaxable.Checked = obj.IsTaxable
                chkIsSerailzedInventory.Checked = obj.Is_Serial_Item
                chkPickAutoSrNo.Checked = obj.Is_Pick_Auto_SrNo
                txtNextAutoSerialCounter.Text = obj.Serial_Counter
                txtWarranty.Value = obj.Warranty_Code
                lblWarranty.Text = obj.Warranty_Name
                txtWeightUOM.Value = obj.Weight_UOM
                lblWeightUOMDesc.Text = clsUOMInfo.GetUnitDesc(txtWeightUOM.Value, Nothing)
                txtWeightValue.Text = obj.Weight_Value
                txtITFCode.Text = obj.ITFCode
                chkMRP.Checked = obj.Is_MRP
                chkIsDisplayDemad.Checked = obj.Is_DisplayDemand
                chkExcludeInApp.Checked = obj.Is_ExcludeAPP
                txtRackNo.Text = obj.Rack_No
                chkFresh.Checked = obj.Is_FreshItem
                chkChangeRate.Checked = IIf(obj.Is_Rate_Change_OnDairyDispatch = 1, True, False)
                chkAllowSRNwoShort.Checked = IIf(obj.AllowSRNWithoutShortReject = 1, True, False)
                chkAmbient.Checked = obj.Is_Ambient
                If obj.Tax_Exempted = 1 Then
                    rbtnTaxExempted.IsChecked = True
                ElseIf obj.Tax_Exempted = 2 Then
                    rbtnExcisable.IsChecked = True
                End If
                fndProductType.SelectedValue = obj.Product_Type
                cboCSAType.Text = obj.CSA_Type
                '=============Rohit============================
                ChkIspurchaseAble.Checked = obj.Is_Purchaseable_item
                ChkAllowQC.Checked = obj.Is_Allow_QC
                '=====================================
                '' Anubhooti 11-Sep-2014
                ChkCrateType.Checked = obj.Is_CrateType
                chkIsCanType.Checked = obj.Is_CAN_Type
                chkScrapItem.Checked = obj.Is_Scrap_Item
                fndScrapItem.Value = obj.Scrap_Item_Code
                chkLeakageNotApplicable.Checked = obj.Is_Leakage_Not_Applicable
                chkMilkPouch.Checked = obj.Is_Milk_Pouch
                chkAdvanceRequired.Checked = obj.Is_Advance_Required
                chkQCSNFBssed.Checked = IIf(clsCommon.myCstr(obj.Is_QC_SNF_Based) = "1", True, False)
                chkInsurance.Checked = IIf(clsCommon.myCstr(obj.Is_Insurance) = "1", True, False)
                If chkInsurance.Checked = True Then
                    txtInsurance.Text = clsCommon.myCstr(obj.InsuranceNo)
                    txtFromDate.Value = clsCommon.myCDate(obj.InsuranceFromDate)
                    txtToDate.Value = clsCommon.myCDate(obj.InsuranceToDate)
                Else
                    txtInsurance.Text = ""
                    txtFromDate.Value = clsCommon.GETSERVERDATE
                    txtToDate.Value = clsCommon.GETSERVERDATE
                End If
                txtMarSeqNo.Text = obj.Marketing_Seq
                chkApplyRounding.Checked = obj.ApplyRoundingInStdProd
                chkApplyVisualQC.Checked = obj.Visual_QC
                txtSecurityDedPer.Value = obj.Security_Deduction

                chkAutoWeighment.Checked = obj.Is_Auto_Weighment
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing)), "A") = CompairStringResult.Equal Then
                    CmbWarrApp.Visible = True
                    LblWarrDate.Visible = True
                    If clsCommon.myLen(txtWarranty.Value) > 0 Then
                        CmbWarrApp.Enabled = True
                        CmbWarrApp.SelectedValue = obj.Warranty_Applied_From
                    Else
                        CmbWarrApp.Enabled = False
                        CmbWarrApp.SelectedValue = ""
                    End If
                Else
                    CmbWarrApp.Visible = False
                    LblWarrDate.Visible = False
                    CmbWarrApp.SelectedValue = ""
                End If
                chkSkipGST.Checked = obj.Skip_GST
                chkPowerAndFuel.Checked = obj.Is_Power_And_Fuel
                ''richa 
                fndGLAcc.Value = obj.GL_Account
                If clsCommon.myLen(fndGLAcc.Value) > 0 Then
                    LblGLAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndGLAcc.Value + "' ")
                Else
                    LblGLAcc.Text = ""
                End If
                ''-----------
                If CreateGLAccToItem = True Then
                    rdlblGLAcc.Visible = True
                    fndGLAcc.Visible = True
                    LblGLAcc.Visible = True
                    'fndGLAcc.Value = obj.GL_Account
                    'If clsCommon.myLen(fndGLAcc.Value) > 0 Then
                    '    LblGLAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndGLAcc.Value + "' ")
                    'Else
                    '    LblGLAcc.Text = ""
                    'End If
                Else
                    rdlblGLAcc.Visible = False
                    fndGLAcc.Visible = False
                    LblGLAcc.Visible = False
                    fndGLAcc.Value = ""
                    LblGLAcc.Text = ""
                End If
                ''
                If obj.ArrUomDetails IsNot Nothing AndAlso obj.ArrUomDetails.Count > 0 Then
                    For Each objtr As clsItemUOMDetails In obj.ArrUomDetails
                        gvUOM.Rows.AddNew()
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColQty).Value = "1"
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColUnit).Value = objtr.UOM_Code
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColEqual).Value = "="
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColUnitDesc).Value = objtr.UOM_Description
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColConvFact).Value = objtr.Conversion_Factor
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColConvUom).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from tspl_item_uom_detail where Item_Code = '" + obj.Item_Code + "' and Stocking_Unit ='Y'"))
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMColStockUnit).Value = objtr.Stocking_Unit
                        ''added by richa agarwal against ticket no BM00000004327
                        If objtr.Default_UOM = 1 Then
                            gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMDefault).Value = True
                        Else
                            gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMDefault).Value = False
                        End If

                        ''==============================
                        If objtr.Pieces = 1 Then
                            gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMPieces).Value = True
                        Else
                            gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMPieces).Value = False
                        End If
                        ''==============================
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMGrossWeight).Value = objtr.Gross_Weight
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMNetWeight).Value = objtr.Net_Weight
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMJobWorkRate).Value = objtr.Job_Work_Rate
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMItemCost).Value = objtr.Item_Cost
                        gvUOM.Rows(gvUOM.RowCount - 1).Cells(UOMCustomConversion).Value = objtr.Custom_Conversion
                    Next
                    If AllowItemConversionAutomation = 1 Then
                        gvUOM.Rows.AddNew()
                    End If

                Else
                    gvUOM.Rows.AddNew()
                End If
                SelectUOMTab = False
                If obj.ArrItemMasterCategory IsNot Nothing AndAlso obj.ArrItemMasterCategory.Count > 0 Then
                    For Each objtr As clsItemMasterCategory In obj.ArrItemMasterCategory
                        gvCategory.Rows.AddNew()
                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCode).Value = objtr.Item_Category_Code
                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCodeDesc).Value = objtr.Item_Category_Code_Desc
                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolValue).Value = objtr.Item_Cagetory_Values
                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolValueDesc).Value = objtr.Item_Cagetory_Values_Desc
                        Dim ShowBinMapping As Boolean = False
                        ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
                        If ShowBinMapping = True Then
                            gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatBinNo).Value = objtr.Item_Cagetory_Values_BIN_NO
                        End If

                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(Catcolmaster).Value = clsCommon.myCBool(IIf(objtr.Master_Value = "1", True, False))
                        gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolSKU).Value = clsCommon.myCBool(IIf(objtr.SKU_Value = "1", True, False))
                    Next
                    'Else
                    '    gvCategory.Rows.AddNew()
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Item_Code)
                End If
                ''End of For Custom Fields

                '' newly added column SubItemType
                Me.cboItemSubType.SelectedValue = obj.SubItemType

                '-------------------------------------
                gv_param.Rows.Clear()
                gv_param.Rows.AddNew()
                If obj.Arr_Param IsNot Nothing AndAlso obj.Arr_Param.Count > 0 Then
                    For Each objtr As clsItemMaster In obj.Arr_Param

                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamCode).Value = objtr.paramcode
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamDesc).Value = objtr.paramdesc
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamnature).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from tspl_parameter_master where code='" + objtr.paramcode + "'"))
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamLower).Value = objtr.Lrange
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamUpper).Value = objtr.Urange
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamStatus).Value = objtr.status
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamValue1).Value = objtr.value1
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colparamValue2).Value = objtr.value2
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colActualRange).Value = objtr.Actual_range
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colActualstatus).Value = objtr.actual_status
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colActualvalue).Value = objtr.actual_value
                        gv_param.Rows(gv_param.Rows.Count - 1).Cells(colSTDRate).Value = objtr.StandardRate
                        gv_param.Rows.AddNew()
                    Next
                End If

                If obj.Arr_Purchase_QC_Parameter IsNot Nothing AndAlso obj.Arr_Purchase_QC_Parameter.Count > 0 Then
                    For Each objtr As clsItemPurchaseQCParameter In obj.Arr_Purchase_QC_Parameter
                        gvPurQCPar.Rows(gvPurQCPar.Rows.Count - 1).Cells(colPurQCParamCode).Value = objtr.QC_Code
                        gvPurQCPar.Rows(gvPurQCPar.Rows.Count - 1).Cells(colPurQCParamSNo).Value = objtr.SNo
                        gvPurQCPar.Rows(gvPurQCPar.Rows.Count - 1).Cells(colPurQCParamDesc).Value = objtr.QC_Name
                        gvPurQCPar.Rows(gvPurQCPar.Rows.Count - 1).Cells(colPurQCParamSpecification).Value = objtr.Specifications
                        gvPurQCPar.Rows.AddNew()
                    Next
                End If

                If obj.ArrSchedule IsNot Nothing AndAlso obj.ArrSchedule.Count > 0 Then
                    If gvSchedule.Rows.Count = 0 Then
                        gvSchedule.Rows.AddNew()
                    End If
                    For Each objtr As clsItemSchedule In obj.ArrSchedule
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColScheduleSNo).Value = objtr.SNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColScheduleDays).Value = objtr.Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColSchedulePerQty).Value = objtr.Qty_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColSchedulePerShort).Value = objtr.Short_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColScheduleLateDays).Value = objtr.Late_Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(ColScheduleLateDays).Tag = objtr.Arr
                        gvSchedule.Rows.AddNew()
                    Next
                Else
                    gvSchedule.Rows.AddNew()
                End If


                chkCreateSepAssetForEachQty.Checked = IIf(obj.CreateSepAssetForEachQty = "1", True, False)

                btnSave.Enabled = True
                btnDelete.Enabled = True

                btnSave.Text = "Update"

                UcAttachment1.LoadData(txtCode.Value)
                Try
                    '=============ROhit Add Code to Display Image====================
                    Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select Item_image from tspl_item_master where item_code='" & txtCode.Value & "'")
                    Using ms As New IO.MemoryStream(CType(Filename, Byte()))
                        Dim img As Image = Image.FromStream(ms)
                        PicImage.Image = img
                    End Using
                    '=============================================
                Catch ex As Exception
                End Try

                ''richa agarwal to enabled/disabled code
                Dim desc As String = ""
                desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToEditCategoryCodeinItemMaster, clsFixedParameterCode.AllowToEditCategoryCodeinItemMaster, Nothing))
                If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
                    txtCategoryStructureCode.Enabled = False
                    gvCategory.ReadOnly = True
                Else
                    txtCategoryStructureCode.Enabled = True
                    gvCategory.ReadOnly = False
                End If

                If clsCommon.CompairString(cboItemType.SelectedValue, "A") = CompairStringResult.Equal Then
                    chkCreateSepAssetForEachQty.Checked = IIf(obj.CreateSepAssetForEachQty = "1", True, False)
                    chkCreateSepAssetForEachQty.Enabled = True
                    ChkCrateType.Enabled = False
                    chkIsCanType.Enabled = False
                    chkFresh.Enabled = False
                    chkAmbient.Enabled = False
                    txtSeqNo.Enabled = False
                    chkChilledFreezen.Enabled = False
                Else
                    chkCreateSepAssetForEachQty.Checked = False
                    chkCreateSepAssetForEachQty.Enabled = False

                    If ChkCrateType.Checked = True Then
                        chkIsCanType.Enabled = False
                    ElseIf chkIsCanType.Checked = True Then
                        ChkCrateType.Enabled = False
                    Else
                        ChkCrateType.Enabled = True
                        chkIsCanType.Enabled = True
                    End If


                    chkFresh.Enabled = True
                    chkAmbient.Enabled = True
                    txtSeqNo.Enabled = True
                    chkChilledFreezen.Enabled = True
                End If

                ''======================================
                ''=============Parteek==============''
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing), "D") = CompairStringResult.Equal Then
                    chkFresh.Visible = True
                    chkAmbient.Visible = True
                    cmbUsedAs.Visible = True
                    'cboCSAType.Visible = True
                    'lblCSaType.Visible = True
                    MyLabel23.Visible = True
                    ChkCrateType.Visible = True
                    chkIsCanType.Visible = True
                Else

                    cmbUsedAs.Visible = False
                    'cboCSAType.Visible = False
                    'lblCSaType.Visible = False
                    MyLabel23.Visible = False

                End If
                ''=============End==============''
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        Finally
            isInsideLoadData = False
            IsFormLoaded = True
        End Try
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If deletedata() Then
                clsCommon.MyMessageBoxShow(Me, "Record deleted successfully.", Me.Text)
                BlankAllConrols()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Function deletedata() As Boolean
        If txtCode.Value <> "" Then
            If myMessages.deleteConfirm() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim qst As String
                Dim dpt As String
                Try
                    qst = "select Item_Code from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_RGP_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_IssueReturn_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_SCRAPINVOICE_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If

                    qst = "select Item_Code from TSPL_REQUISITION_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_PURCHASE_ORDER_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If

                    qst = "select Item_Code from TSPL_SRN_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_PI_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    qst = "select Item_Code from TSPL_SCRAPSALE_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If


                    qst = "select Item_Code from TSPL_PR_DETAIL where Item_Code='" + txtCode.Value + "'"
                    dpt = clsDBFuncationality.getSingleValue(qst, trans)
                    If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                        trans.Rollback()
                        Return False
                    End If
                    Dim qry As String = " delete from TSPL_ITEM_UOM_DETAIL where Item_Code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, GetDatabase(), trans)

                    '' Anubhooti 08-Oct-2014 (Because of Foreign Ref of item_code in TSPL_ITEM_QC_PARAMETER_MASTER and TSPL_ITEM_MASTER_CATEGORY deleted first)
                    qry = "delete from TSPL_ITEM_QC_PARAMETER_MASTER where item_code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER where Item_Code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ITEM_MASTER_CATEGORY where item_code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ITEM_SCHEDULE_PENALTY where Against_Schedule_PK_Id in (select PK_ID from TSPL_ITEM_SCHEDULE where Item_Code='" + txtCode.Value + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ITEM_SCHEDULE where Item_Code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_ITEM_MASTER where Item_Code='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, GetDatabase(), trans)
                    clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)

                    trans.Commit()
                    Return True
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
                End Try

            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "You Can Not delete The Record", Me.Text)
        End If
        Return False
    End Function
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        If UpdateItemMasterConversationWithoutValidation = True Then
            clsFixedParameter.UpdateData("UpdateItemMasterConversationWithoutValidation", "UpdateItemMasterConversationWithoutValidation", "0", Nothing)
        End If
        Me.Close()
    End Sub
    Private Sub fndChptr__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChptr._MYValidating
        Dim Qry As String = "select Chapter_Head_Code as Code,Description from TSPL_CHAPTER_HEAD "
        fndChptr.Value = clsCommon.ShowSelectForm("fnsubcat", Qry, "Code", "", txtSubCategory.Value, "Code", isButtonClicked)
        lblchptrdesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_CHAPTER_HEAD where Chapter_Head_Code='" + fndChptr.Value + "'")

    End Sub
    Private Sub txtAlternativeItem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAlternativeItem._MYValidating
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(txtAlternativeItem.Value), "cboItemType.SelectedValue", True, isButtonClicked, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            txtAlternativeItem.Value = obj.Item_Code
            lblAlternativeItemName.Text = obj.Item_Desc
        Else
            txtAlternativeItem.Value = ""
            lblAlternativeItemName.Text = ""
        End If

    End Sub
    Private Sub txtCategoryStructureCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategoryStructureCode._MYValidating
        Dim qry As String = "select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE"
        txtCategoryStructureCode.Value = clsCommon.ShowSelectForm("ITEMMASTERCATSTRU", qry, "Code", " isnull(form_type,'Item')='Item'", txtCategoryStructureCode.Value, "Code", isButtonClicked)
        LoadBlankGridCat()
        ''richa agarwal remove order by from below query 13/10/2014
        If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
            lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(form_type,'Item')='Item'"))

            qry = "select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION  "
            qry += " from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type"
            qry += " where TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='Item' "
            ''order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE  "
            'TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvCategory.Rows.AddNew()
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCode).Value = clsCommon.myCstr(dr("ITEM_CATEGORY_CODE"))
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCodeDesc).Value = clsCommon.myCstr(dr("DESCRIPTION"))
                Next
            End If
        Else
            lblCategoryStructureCode.Text = ""
        End If
    End Sub
    Private Sub gvCategory_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCategory.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenCat Then
                    isCellValueChangedOpenCat = True
                    If e.Column Is gvCategory.Columns(CatcolValue) Then
                        OpenCatValueList(False)
                    End If
                    isCellValueChangedOpenCat = False
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenCatValueList(ByVal isButtonClick As Boolean)
        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = ""
        Dim qry As String = " select CODE,DESCRIPTION,Bin_No from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        ''richa agarwal against ticket no BM00000004640(add condition in finder )
        gvCategory.CurrentRow.Cells(CatcolValue).Value = clsCommon.ShowSelectForm("itemtFinxtd", qry, "CODE", " isnull(form_type,'Item')='Item' and ITEM_CATEGORY_CODE ='" & clsCommon.myCstr(gvCategory.CurrentRow.Cells(0).Value) & "'", clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value), "CODE", isButtonClick)
        qry = "select DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='Item'"
        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        qry = "select Bin_No from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='Item'"
        gvCategory.CurrentRow.Cells(CatBinNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
    Private Sub txtUOM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUOM.Load

    End Sub
    Private Sub gvUOM_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvUOM.UserDeletingRow
        If clsCommon.myCdbl(gvUOM.CurrentRow.Cells(UOMColStockUnitChangable).Value) = 1 Then
            If Not chkDoNotCheckOnSave.Checked Then
                clsCommon.MyMessageBoxShow(Me, "The Item '" + txtCode.Value + "' with UOM '" + clsCommon.myCstr(gvUOM.CurrentRow.Cells(UOMColUnit).Value) + "' is in use.", Me.Text)
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub gvUOM_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gvUOM.RowFormatting
        Try
            'e.RowElement.Enabled = IIf(clsCommon.myCdbl(e.RowElement.RowInfo.Cells(UOMColStockUnitChangable).Value) = "1", False, True)
            ReadOnlyUOMGrid(gvUOM.CurrentRow.Index)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txtWarranty__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtWarranty._MYValidating
        Dim qry As String = "select Code,Name from TSPL_WARRANTY_MASTER"
        txtWarranty.Value = clsCommon.ShowSelectForm("IMWarCode", qry, "Code", "", txtWarranty.Value, "", isButtonClicked)
        If clsCommon.myLen(txtWarranty.Value) > 0 Then
            lblWarranty.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_WARRANTY_MASTER where Code='" + txtWarranty.Value + "'"))
            CmbWarrApp.Enabled = True
        Else
            lblWarranty.Text = ""
            CmbWarrApp.Enabled = False
        End If
    End Sub
    Private Sub txtWeightUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtWeightUOM._MYValidating
        Dim qry As String = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtWeightUOM.Value = clsCommon.ShowSelectForm("WeightUOMfndnder@IM", qry, "Code", "Weight_Type='Y'", txtWeightUOM.Value, "Code", isButtonClicked)
        lblWeightUOMDesc.Text = clsUOMInfo.GetUnitDesc(txtWeightUOM.Value, Nothing)
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub rmiItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiItemDetails.Click
        Try
            '----------------------for n-level category-----------------------------------
            Dim code As String = ""
            Dim whrcls As String = ""
            If txtCategory.Enabled = False Then
                Dim qry As String = "select a.code as [Code],a.Description from (select distinct TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE,TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL.CATEGORY_LEVEL=1 left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE)a "
                code = clsCommon.ShowSelectForm("CATFND", qry, "Code", " isnull(a.ITEM_CATEGORY_CODE,'')<>''", code, "Code", True)

                If clsCommon.myLen(code) <= 0 Then
                    If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure,Want To Export Item Detail Without Any Category Filter?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Return
                    End If
                Else
                    whrcls = " and Item_Cagetory_Values='" + code + "'"
                    code = " left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_item_master.item_code "
                End If
            End If
            '--------------------------------------------------------------------------------

            QrySheet = " Select TSPL_ITEM_MASTER.Item_Code as [Item Code], Item_Desc as [Item Description],sku_seq as [Seq No],Short_Description as [Short Description], Structure_Code as [Structure Code], Rack_No as [Rack No],"
            QrySheet += " Purchase_Class_Code as [Purchase A/c Set], Sale_Class_Code as [Sale A/c Set], item_category as [Category], "
            QrySheet += " Sub_item_category as [Sub Category], Unit_Code as [UOM], Item_Type as [Item Type], TypeOfItm as [Type],TSPL_ITEM_MASTER.tolerence as [Tolerence], Cost,Purchase_Price as [Standard Purchase Price], "
            QrySheet += " Morning,Cheapter_Heads as [Chapter Code], Item_Category_Struct_Code as [Category Structure], Weight_UOM, Weight_Value,Is_MRP,ITF_CODE,Active,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Weight,TSPL_ITEM_UOM_DETAIL.Stocking_Unit,Is_FreshItem,Is_Ambient,Product_type as [Product Type],case when coalesce(Is_Purchaseable,'0')='1' then 'Yes' else 'No' end as [Is Purchaseable],case when coalesce(Is_AllowQC_On_Purchase,'0')='1' then 'Yes' else 'No' end as [Is Allow QC on Purchase],Is_CrateType,Is_CAN_Type,case When Item_used_as='I' then 'MCC Issue' when item_used_as='S' then 'MCC Sale' when item_used_as='P' then 'Production' else 'None' end as [Used As],ISNULL(TSPL_ITEM_MASTER.GL_Account,'') AS [GL Account],(SELECT Description  From TSPL_GL_ACCOUNTS where Account_Code=TSPL_ITEM_MASTER.GL_Account) AS [Account Description] "
            If clsCommon.myLen(code) > 0 Then
                QrySheet += ",TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values"
            Else
                QrySheet += ",'' as Item_Cagetory_Values"
            End If
            QrySheet += " ,Part_No,Drawing_No,Is_Rate_Change_OnDairyDispatch,case when Is_Auto_Weighment=1 then 'Y' else 'N' end [Auto Weightment(Y/N)],AllowSRNWithoutShortReject "

            If AllowGSTApplicable = True Then
                QrySheet += " ,HSN_Code as [HSN Code]"
            End If
            QrySheet += " ,case when Is_Power_And_Fuel=1 then 'Y' else 'N' end [Power And Fuel(Y/N)] , Chilled_Freezen as [Chilled Freezen],Correction_Factor as [Correction Factor] ,Is_Leakage_Not_Applicable as [Leakage Not Applicable] "
            QrySheet += " ,TSPL_ITEM_MASTER.Marketing_Seq as [Marketing Seq No] "
            QrySheet += " from TSPL_ITEM_MASTER left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_item_master.item_code " + code + ""
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "Short Description", "Structure Code", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Used As"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Item Code"})
            transportSql.ExporttoExcel(QrySheet, whrcls, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "ItemDetails")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExportUOMDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExportUOMDetails.Click
        Try
            qry = "Select TSPL_ITEM_MASTER.Item_Code as [Item Code], UOM_Code as [UOM], Conversion_Factor as [Conversion Factor], Stocking_Unit as [Stocking Unit],Default_UOM As [Default UOM], Weight,Gross_Weight,Net_Weight,Job_Work_Rate,Pieces,TSPL_ITEM_UOM_DETAIL.Item_Cost as [Item Cost],(case when Custom_Conversion=1 then 'Y' else 'N' end) as [Custom Conversion] from TSPL_ITEM_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code "
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "UOM", "Default UOM", "Pieces", "Conversion Factor"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Item Code", "UOM"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "UOMDetails")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub
    Private Sub rmiExportCategoryStructure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExportCategoryStructure.Click
        Try
            qry = "select  TSPL_ITEM_MASTER.Item_Code as [Item Code], TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE as [Category Structure],TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no, TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as [Category Code], TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values as [Category Value] from TSPL_ITEM_MASTER " &
            " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_STRUCTURE ON TSPL_ITEM_CATEGORY_STRUCTURE.ITEM_CATEGORY_STRUCT_CODE=TSPL_ITEM_MASTER.Item_Category_Struct_Code and isnull(TSPL_ITEM_CATEGORY_STRUCTURE.form_type,'item')='item'" &
            " left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code " &
            " LEFT OUTER JOIN TSPL_ITEM_CATEGORY_STRUCT_DETAIL ON  TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=tspl_item_master.ITEM_CATEGORY_STRUCT_CODE and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_MASTER_CATEGORY.item_category_code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=TSPL_ITEM_CATEGORY_STRUCTURE.item_category_struct_code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type=TSPL_ITEM_CATEGORY_STRUCTURE.form_type and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='Item'"
            Dim orderByCls = " [Item Code],line_no "
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "Category Structure", "Category Code", "Category Value"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Item Code", "Category Structure"})
            transportSql.ExporttoExcel(qry, "", orderByCls, Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "CategoryStructure")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub
    Private Sub rmiImportItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportItemDetails.Click
        Try
            ImportItemDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiImportUOMDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportUOMDetails.Click
        Try
            ImportItemUOMDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiImportCategoryStructure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportCategoryStructure.Click
        Try
            ImportItemCategoryStructure()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ImportItemDetail()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim inputs() As String = {}

        ''[Item Code],[Item Description],[Short Description],[Structure Code],[Rack No],[Purchase A/c Set],[Sale A/c Set],[Category],[Sub Category],[UOM],[Item Type],[Type],Cost,[Standard Purchase Price],Morning,[Chapter Code],[Category Structure], Weight_UOM, Weight_Value,Is_MRP,ITF_CODE,Active,Conversion_Factor,Weight,Stocking_Unit,Is_FreshItem,[Product Type],[Is Purchaseable],[Is Allow QC on Purchase] ,Item_Cagetory_Values

        If AllowGSTApplicable = True Then
            inputs = {"Item Code", "Item Description", "Seq No", "Short Description", "Structure Code", "Rack No", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Item Type", "Type", "Cost", "Tolerence", "Standard Purchase Price", "Morning", "Chapter Code", "Category Structure", "Weight_UOM", "Weight_Value", "Is_MRP", "ITF_CODE", "Active", "Conversion_Factor", "Weight", "Stocking_Unit", "Is_FreshItem", "Is_Ambient", "Product Type", "Is Purchaseable", "Is Allow QC on Purchase", "Item_Cagetory_Values", "Is_CrateType", "Is_CAN_Type", "Used As", "Part_No", "Drawing_No", "GL Account", "Account Description", "Is_Rate_Change_OnDairyDispatch", "Auto Weightment(Y/N)", "AllowSRNWithoutShortReject", "HSN Code", "Power And Fuel(Y/N)", "Chilled Freezen", "Correction Factor", "Leakage Not Applicable", "Marketing Seq No"}
        Else
            inputs = {"Item Code", "Item Description", "Seq No", "Short Description", "Structure Code", "Rack No", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Item Type", "Type", "Cost", "Tolerence", "Standard Purchase Price", "Morning", "Chapter Code", "Category Structure", "Weight_UOM", "Weight_Value", "Is_MRP", "ITF_CODE", "Active", "Conversion_Factor", "Weight", "Stocking_Unit", "Is_FreshItem", "Is_Ambient", "Product Type", "Is Purchaseable", "Is Allow QC on Purchase", "Item_Cagetory_Values", "Is_CrateType", "Is_CAN_Type", "Used As", "Part_No", "Drawing_No", "GL Account", "Account Description", "Is_Rate_Change_OnDairyDispatch", "Auto Weightment(Y/N)", "AllowSRNWithoutShortReject", "Power And Fuel(Y/N)", "Chilled_Freezen", "Chilled Freezen", "Correction Factor", "Leakage Not Applicable", "Marketing Seq No"}
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)

        If transportSql.importExcel(gv, Strs.ToArray()) Then

            clsCommon.ProgressBarShow()
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                Dim Item_Code As String
                Dim Item_Desc As String
                Dim Item_Short_Desc As String
                Dim Structure_Code As String
                Dim Rack_No As String
                Dim Structure_Desc As String
                Dim Purchase_Class_Code As String
                Dim Sale_Class_Code As String
                Dim item_category As String
                Dim Sub_item_category As String
                Dim Unit_Code As String
                Dim Item_Type As String
                Dim TypeOfItm As String
                Dim Cost As Double
                Dim Tolerence As Double
                Dim Morning As String
                Dim ChilledFreezen As String
                Dim Is_PurchaseAble As String
                Dim Is_Allow_QC As String
                Dim Chapter_Code As String
                Dim Weight_UOM As String
                Dim wt As String = ""
                Dim cnvrsnfctr As String = ""
                Dim stckunit As String = ""
                Dim Product_Type As String
                Dim Part_No As String = ""
                Dim Drawing_no As String = ""
                'Dim CSA_Type As String
                Dim GL_Account As String = ""
                Dim strName As String = ""
                Dim HSNCode As String = ""

                Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)



                    If (clsCommon.myLen(grow.Cells("Item Code").Value) > 0 And clsCommon.myLen(grow.Cells("Item Code").Value) <= 50) OrElse clsCommon.myLen(grow.Cells("Item Description").Value) > 0 Then
                        Dim coll As New Hashtable()

                        '-------------------------Item Type---------------------------
                        Item_Type = clsCommon.myCstr(grow.Cells("Item Type").Value)
                        If Not (clsCommon.CompairString(Item_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "O") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "S") = CompairStringResult.Equal) Then
                            Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ITEM_TYPE_CODE from TSPL_ITEM_TYPE_MASTER where ITEM_TYPE_CODE='" + Item_Type + "'", trans))
                            If clsCommon.myLen(Item_Type) <= 0 Then
                                Throw New Exception("Item Type can not be other than ('R' or 'O' or 'A' or 'F' or 'S') at line '" + LineNo + "'")
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "Item_Type", Item_Type)
                        '---------------------------------------------------------------

                        '-----------------Item Code-------------------------
                        Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        If clsCommon.myLen(Item_Code) <= 0 Then
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, trans)) = 1 Then
                                'Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, Item_Type, trans))
                                'Dim Qry1 As String = "update TSPL_FIXED_PARAMETER set Description='" + clsCommon.incval(Item_Code) + "' where Type='" + clsFixedParameterType.AutoItemNLevel + " ' and Code='" + Item_Type + "'"
                                'clsDBFuncationality.ExecuteNonQuery(Qry1, trans)
                                Dim Qry1 As String = "SELECT PREFIX FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry1, trans))

                                Dim Qry2 As String = "update TSPL_ITEM_TYPE_MASTER set PREFIX='" + clsCommon.incval(Item_Code) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                clsDBFuncationality.ExecuteNonQuery(Qry2, trans)
                            End If
                        End If
                        If clsCommon.myLen(Item_Code) <= 0 Then
                            Throw New Exception("Item Code not found to save")
                        End If

                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                        '---------------------------------------------------

                        '------------------Item description------------------
                        Item_Desc = clsCommon.myCstr(grow.Cells("Item Description").Value)
                        If clsCommon.myLen(Item_Desc) > 100 Then
                            Throw New Exception("Length of item description on line '" + LineNo + "' is greater than 100.")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Item_Desc", Item_Desc)
                        '-----------------------------------------------------

                        '------------------Item description------------------
                        Item_Short_Desc = clsCommon.myCstr(grow.Cells("Short Description").Value)
                        If clsCommon.myLen(Item_Short_Desc) > 200 Then
                            Throw New Exception("Length of Item Short description on line '" + LineNo + "' is greater than 200.")
                        End If

                        '-----------------------------------------------------
                        '' Anubhooti 12-Sep-2014
                        Dim Is_Fresh As String = clsCommon.myCstr(grow.Cells("Is_FreshItem").Value)
                        If clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(Item_Short_Desc) <= 0 Then
                                Throw New Exception("Short description can not be left blank on line '" + LineNo + "'")
                            Else
                                Dim ShortDesp As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row from tspl_item_master Where Short_Description ='" & clsCommon.myCstr(Item_Short_Desc.Trim()) & "'  AND Item_Code <>'" & clsCommon.myCstr(Item_Code) & "'", trans))
                                If ShortDesp > 0 Then
                                    Throw New Exception("Please check ! short description should not be duplicate  on line '" + LineNo + "'")
                                End If
                            End If

                        End If

                        '' Anubhooti 12-Sep-2014


                        clsCommon.AddColumnsForChange(coll, "Short_Description", Item_Short_Desc)

                        If AllowGSTApplicable = True Then
                            HSNCode = clsCommon.myCstr(grow.Cells("HSN Code").Value)
                            Dim hsnqry As String = clsDBFuncationality.getSingleValue("select code from tspl_hsn_master where code='" & HSNCode & "'", trans)
                            If clsCommon.myLen(hsnqry) <= 0 Then
                                Throw New Exception("HSN Code not match'" + LineNo + "'")
                            End If
                        End If


                        '' Richa 13-July-2016
                        Dim Is_Ambient As String = clsCommon.myCstr(grow.Cells("Is_Ambient").Value)
                        If clsCommon.CompairString(Is_Ambient, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                            Throw New Exception("You cannot select Fresh item/Ambient at a time, Please select either fresh Item or Ambient on line '" + LineNo + "'")
                        End If

                        ''
                        '------------------Rack No------------------
                        Rack_No = clsCommon.myCstr(grow.Cells("Rack No").Value)
                        If clsCommon.myLen(Rack_No) > 30 Then
                            Throw New Exception("Length of Rack No on line '" + LineNo + "' is greater than 30.")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Rack_No", Rack_No)
                        '-----------------------------------------------------
                        Dim reccount As Integer = 0
                        Dim seqNo As Int64 = 0
                        seqNo = clsCommon.myCdbl(grow.Cells("Seq No").Value)
                        If clsCommon.myCdbl(txtSeqNo.Text) > 0 Then
                            reccount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where sku_seq=" & seqNo & " and   item_code<>'" & Item_Code & "'", trans))
                            If reccount > 0 Then
                                Throw New Exception("Duplicate Sequence No at Line No " & LineNo)
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "sku_seq", seqNo)

                        '''''''''''''''''
                        Dim Marreccount As Integer = 0
                        Dim MarseqNo As Int64 = 0
                        MarseqNo = clsCommon.myCdbl(grow.Cells("Marketing Seq No").Value)
                        If clsCommon.myCdbl(MarseqNo) > 0 Then
                            Marreccount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Marketing_Seq=" & MarseqNo & " and   item_code<>'" & Item_Code & "'", trans))
                            If Marreccount > 0 Then
                                Throw New Exception("Duplicate Marketing Sequence No at Line No " & LineNo)
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "Marketing_Seq", MarseqNo)
                        ''''''''''''''''

                        Dim std_pur_rate As Decimal = clsCommon.myCdbl(grow.Cells("Standard Purchase Price").Value)

                        '----------------Structure Code-----------------------
                        Dim StructureCode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                        If StructureCode <> "" Then
                            Structure_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Code from TSPL_STRUCTURE_MASTER WHERE Structure_Code='" + StructureCode + "'", trans))
                            If clsCommon.CompairString(Structure_Code, StructureCode) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Structure_Code", Structure_Code)
                            Else
                                Throw New Exception("Structure Code at line '" + LineNo + "' does not exist.")
                            End If
                        Else
                            Throw New Exception("Structure Code can not be blank at line '" + LineNo + "'.")
                        End If
                        '-----------------------------------------------------

                        '----------------Structure Desc-------------------------
                        Structure_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Structure_Descq from TSPL_STRUCTURE_MASTER Where Structure_Code='" + Structure_Code + "'", trans))
                        clsCommon.AddColumnsForChange(coll, "Structure_Desc", Structure_Desc)
                        '-------------------------------------------------------

                        '----------------------Purchase A/c Set---------------
                        Dim PurchaseAcSet As String = clsCommon.myCstr(grow.Cells("Purchase A/c Set").Value)
                        If PurchaseAcSet <> "" Then
                            Purchase_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from dbo.TSPL_PURCHASE_ACCOUNTS WHERE Purchase_Class_Code ='" + PurchaseAcSet + "'", trans))
                            If clsCommon.CompairString(Purchase_Class_Code, PurchaseAcSet) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", Purchase_Class_Code)
                            Else
                                Throw New Exception("Purchase Acccount Set at line '" + LineNo + "' does not exist.")
                            End If
                        Else
                            Throw New Exception("Purchase A/c Set can not be blank at line '" + LineNo + "'.")
                        End If
                        '-----------------------------------------------------

                        '------------------------sale A/c Set------------------
                        Dim SaleAcSet As String = clsCommon.myCstr(grow.Cells("Sale A/c Set").Value)
                        If SaleAcSet <> "" Then
                            Sale_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Code from TSPL_SALES_ACCOUNTS WHERE Sales_Class_Code ='" + SaleAcSet + "'", trans))
                            If clsCommon.CompairString(Sale_Class_Code, SaleAcSet) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", Sale_Class_Code)
                            Else
                                Throw New Exception("Sale Account Set at line '" + LineNo + "' does not exist.")
                            End If
                        Else
                            Throw New Exception("Sale A/c Set can not be blank at line '" + LineNo + "'.")
                        End If
                        '-------------------------------------------------------

                        '---------------------Item Category-----------------------
                        If Not RadPageView1.Pages(2).Enabled Then ''------------------------------BM00000002280 Monika
                            Dim ItemCategory As String = clsCommon.myCstr(grow.Cells("Category").Value)
                            If ItemCategory <> "" Then
                                item_category = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select category_code from tspl_Item_category WHERE category_code ='" + ItemCategory + "'", trans))
                                If clsCommon.CompairString(item_category, ItemCategory) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "item_category", item_category)
                                Else
                                    Throw New Exception("Item Category at line '" + LineNo + "' does not exist.")
                                End If
                            Else
                                Throw New Exception("Item Category can not be blank at line '" + LineNo + "'.")
                            End If
                            '------------------------------------------------------------

                            '-----------------Item Sub Category--------------------------
                            Dim ItemSubCategory As String = clsCommon.myCstr(grow.Cells("Sub Category").Value)
                            If ItemSubCategory <> "" Then
                                Sub_item_category = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_Category_Code from TSPL_ITEM_SUB_CATEGORY WHERE Sub_Category_Code ='" + ItemSubCategory + "'", trans))
                                If clsCommon.CompairString(Sub_item_category, ItemSubCategory) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Sub_item_category", Sub_item_category)
                                Else
                                    Throw New Exception("Item Sub Category at line '" + LineNo + "' does not exist.")
                                End If
                            Else
                                Throw New Exception("Item Sub Category can not be blank at line '" + LineNo + "'.")
                            End If
                            '-------------------------------------------------------------
                        End If
                        '------------------------End------BM00000002280 Monika


                        '---------------------Unit_Code-------------------------------
                        Dim Uom As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                        If Uom <> "" Then
                            Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master WHERE unit_code ='" + Uom + "'", trans))
                            If clsCommon.CompairString(Unit_Code, Uom) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Unit_Code", Unit_Code)
                            Else
                                Throw New Exception("UOM Code at line '" + LineNo + "' does not exist.")
                            End If
                        Else
                            Throw New Exception("UOM can not be blank at line '" + LineNo + "'.")
                        End If
                        '--------------------------------------------------------------



                        '-------------------------type-----------------------------------
                        TypeOfItm = clsCommon.myCstr(grow.Cells("Type").Value)
                        If Not (clsCommon.CompairString(TypeOfItm, "A") = CompairStringResult.Equal Or clsCommon.CompairString(TypeOfItm, "B") = CompairStringResult.Equal Or clsCommon.CompairString(TypeOfItm, "C") = CompairStringResult.Equal) Then
                            Throw New Exception("Type can not be other than ('A' or 'B' or 'C') at line '" + LineNo + "'")
                        End If
                        clsCommon.AddColumnsForChange(coll, "TypeOfItm", TypeOfItm)
                        '----------------------------------------------------------------

                        '-------------------------Cost--------------------------------
                        Cost = clsCommon.myCdbl(grow.Cells("Cost").Value)
                        clsCommon.AddColumnsForChange(coll, "Cost", Cost)
                        '---------------------------------------------------------------
                        Tolerence = clsCommon.myCdbl(grow.Cells("Tolerence").Value)
                        clsCommon.AddColumnsForChange(coll, "Tolerence", Tolerence)
                        '-------------------------Morining--------------------------------
                        Morning = clsCommon.myCstr(grow.Cells("Morning").Value)
                        If Not (clsCommon.CompairString(Morning, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Morning, "0") = CompairStringResult.Equal) Then
                            Throw New Exception("Morning can not be other than ('1' or '0') at line '" + LineNo + "'")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Morning", Morning)
                        '---------------------------------------------------------------

                        'Chilled_Freezen --------------------------------------------------------
                        If clsCommon.CompairString(Item_Type, "A") <> CompairStringResult.Equal Then
                            ChilledFreezen = clsCommon.myCstr(grow.Cells("Chilled Freezen").Value)
                            If Not (clsCommon.CompairString(ChilledFreezen, "1") = CompairStringResult.Equal Or clsCommon.CompairString(ChilledFreezen, "0") = CompairStringResult.Equal) Then
                                Throw New Exception("Chilled Freezen can not be other than ('1' or '0') at line '" + LineNo + "'")
                            End If
                            clsCommon.AddColumnsForChange(coll, "Chilled_Freezen", ChilledFreezen)
                        End If

                        ' -------------------------------------------------------------------------

                        '-------------------------Is Purchaseable--------------------------------
                        Is_PurchaseAble = IIf(LCase(clsCommon.myCstr(grow.Cells("Is Purchaseable").Value)) = "yes", "1", "0")
                        If Not (clsCommon.CompairString(Is_PurchaseAble, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Is_PurchaseAble, "0") = CompairStringResult.Equal) Then
                            Throw New Exception("[Is Purchaseable] can not be other than ('1' or '0') at line '" + LineNo + "'")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Is_Purchaseable", Is_PurchaseAble)
                        '---------------------------------------------------------------

                        '-------------------------Is Allow QC--------------------------------
                        Is_Allow_QC = IIf(LCase(clsCommon.myCstr(grow.Cells("Is Allow QC on Purchase").Value)) = "yes", "1", "0")
                        If Not (clsCommon.CompairString(Is_PurchaseAble, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Is_PurchaseAble, "0") = CompairStringResult.Equal) Then
                            Throw New Exception("[Is Allow QC on Purchase] can not be other than ('1' or '0') at line '" + LineNo + "'")
                        End If
                        clsCommon.AddColumnsForChange(coll, "Is_AllowQC_on_Purchase", Is_Allow_QC)
                        '---------------------------------------------------------------

                        '---------------------Chapter_code-------------------------------
                        Dim Chapter As String = clsCommon.myCstr(grow.Cells("Chapter Code").Value)
                        If Chapter <> "" Then
                            Chapter_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Chapter_Head_Code from TSPL_CHAPTER_HEAD  WHERE Chapter_Head_Code ='" + Chapter + "'", trans))
                            If clsCommon.CompairString(Chapter, Chapter_Code) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", Chapter)
                            Else
                                Throw New Exception("Chapter Code Code at line '" + LineNo + "' does not exist.")
                            End If
                        End If
                        '--------------------------------------------------------------

                        '----------------Weight UOM-----------------------
                        Dim WeightUOM As String = clsCommon.myCstr(grow.Cells("Weight_UOM").Value)
                        If StructureCode <> "" Then
                            Weight_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER WHERE Weight_Type='Y' AND Unit_Code ='" + WeightUOM + "'", trans))
                            If clsCommon.CompairString(Weight_UOM, WeightUOM) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Weight_UOM", Weight_UOM)
                            Else
                                Throw New Exception("Weight_UOM at line '" + LineNo + "' does not exist.")
                            End If
                        End If

                        wt = clsCommon.myCstr(grow.Cells("Weight").Value)
                        cnvrsnfctr = clsCommon.myCstr(grow.Cells("Conversion_Factor").Value)
                        stckunit = clsCommon.myCstr(grow.Cells("Stocking_unit").Value)

                        If clsCommon.myLen(wt) <= 0 Then
                            wt = "0.00"
                        End If
                        If clsCommon.myLen(cnvrsnfctr) <= 0 Then
                            cnvrsnfctr = "1"
                        End If
                        If clsCommon.myLen(stckunit) <= 0 Then
                            stckunit = "Y"
                        End If

                        '' Anubhooti 11-Sep-2014 BM00000003891
                        Dim Is_CrateType As String = "N"
                        If clsCommon.CompairString(grow.Cells("Is_CrateType").Value, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("Is_CAN_Type").Value, "1") = CompairStringResult.Equal Then
                            Throw New Exception("Item Type can be CRATE type or CAN type")
                        End If

                        If clsCommon.myLen(grow.Cells("Is_CrateType").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Is_CrateType").Value, "1") = CompairStringResult.Equal Then
                                Is_CrateType = "1"
                            ElseIf clsCommon.CompairString(grow.Cells("Is_CrateType").Value, "0") = CompairStringResult.Equal Then
                                Is_CrateType = "0"
                            Else
                                Throw New Exception("Enter Crate Type As '1' Or '0' Or left blank.")
                            End If
                        Else
                            Is_CrateType = "0"
                        End If
                        '===============Added by preeti gupta==============
                        Dim Is_CAN_Type As String = Nothing
                        If clsCommon.myLen(grow.Cells("Is_CAN_Type").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Is_CAN_Type").Value, "1") = CompairStringResult.Equal Then
                                Is_CAN_Type = "1"
                            ElseIf clsCommon.CompairString(grow.Cells("Is_CAN_Type").Value, "0") = CompairStringResult.Equal Then
                                Is_CAN_Type = "0"
                            Else
                                Throw New Exception("Enter CAN Type As '1' Or '0' Or left blank.")
                            End If
                        Else
                            Is_CAN_Type = "0"
                        End If
                        '============================================================
                        ''

                        Part_No = clsCommon.myCstr(grow.Cells("Part_No").Value)
                        If Part_No.Length > 100 Then
                            Throw New Exception("User Code length can not be greater than 100")
                        Else
                            If Part_No.Length > 0 Then
                                strName = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_PART_NO_MASTER Where CODE ='" & Part_No & "'", trans)
                                If strName <= 0 Then
                                    Throw New Exception("Part No (" & Part_No & ") does not exist . Please make it entry first.")
                                End If
                            End If
                        End If


                        Part_No = clsCommon.myCstr(grow.Cells("Part_No").Value).Replace("'", "`")


                        Drawing_no = clsCommon.myCstr(grow.Cells("Drawing_No").Value).Replace("'", "`")
                        If clsCommon.myLen(Part_No) > 100 Then
                            Part_No = Part_No.Substring(0, 100)
                        End If
                        If clsCommon.myLen(Drawing_no) > 100 Then
                            Drawing_no = Drawing_no.Substring(0, 100)
                        End If

                        '==============Used as ===============
                        Dim Used_as As String = clsCommon.myCstr(grow.Cells("Used As").Value)
                        '' Anubhooti 14-Jan-2014 (Check/Valdiations of used as)
                        '==============Added by preeti gupta Against Ticket no[ERO/29/06/18-000363]
                        If clsCommon.myLen(Used_as) > 0 Then
                            If clsCommon.CompairString(Used_as, "None") = CompairStringResult.Equal Then
                                Used_as = ""
                            ElseIf clsCommon.CompairString(Used_as, "MCC Issue") = CompairStringResult.Equal Then
                                Used_as = "I"
                            ElseIf clsCommon.CompairString(Used_as, "MCC Sale") = CompairStringResult.Equal Then
                                Used_as = "S"
                            ElseIf clsCommon.CompairString(Used_as, "Production") = CompairStringResult.Equal Then
                                Used_as = "P"
                            Else
                                Throw New Exception("Entered Used As should be amoung 'None','MCC Issue','MCC Sale','Production'.")
                            End If
                        Else
                            Throw New Exception("Please fill used as from amoung 'None','MCC Issue','MCC Sale','Production'.")
                        End If
                        '================
                        Dim Is_Leakage_Not_Applicable As Integer = 0
                        If clsCommon.myLen(grow.Cells("Leakage Not Applicable").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Leakage Not Applicable").Value, "1") = CompairStringResult.Equal Then
                                Is_Leakage_Not_Applicable = "1"
                            ElseIf clsCommon.CompairString(grow.Cells("Leakage Not Applicable").Value, "0") = CompairStringResult.Equal Then
                                Is_Leakage_Not_Applicable = "0"
                            Else
                                Throw New Exception("Enter Crate Type As '1' Or '0' Or left blank.")
                            End If
                        Else
                            Is_Leakage_Not_Applicable = "0"
                        End If
                        '================
                        '------------------GL Account------------------
                        If CreateGLAccToItem = True AndAlso fndGLAcc.Visible = True Then
                            GL_Account = clsCommon.myCstr(grow.Cells("GL Account").Value)
                            If clsCommon.myLen(GL_Account) > 0 Then
                                Dim GLqry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account + "'"
                                Dim check As Integer = clsDBFuncationality.getSingleValue(GLqry, trans)
                                If check <= 0 Then
                                    Throw New Exception("Filled GL account (" & GL_Account & ") does not exist" + Environment.NewLine + ".First make its entry at line '" + LineNo + "'.")
                                End If
                                'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account + "' AND ControlAccount ='Y'"
                                'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                                'If check1 <= 0 Then
                                '    Throw New Exception("Filled GL account (" & GL_Account & ") must be control account at line '" + LineNo + "'.")
                                'End If
                                'Else
                                '    Throw New Exception("Please fill GL account at line '" + LineNo + "'.")
                            End If
                        End If
                        clsCommon.AddColumnsForChange(coll, "GL_Account", GL_Account, True)
                        '-----------------------------------------------------

                        clsCommon.AddColumnsForChange(coll, "Item_Used_As", Used_as)
                        '================================
                        '-------------------------Product Type---------------------------
                        '' check for product type
                        Product_Type = clsCommon.myCstr(grow.Cells("Product Type").Value)
                        Dim Initial_Product_Type As String = clsItemMaster.GetItemProductType(Item_Code, trans)
                        Dim Qry As String = ""
                        If clsCommon.CompairString(Product_Type, Initial_Product_Type) <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(Initial_Product_Type, "MI") = CompairStringResult.Equal Then
                                Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & Item_Code & "'"
                                Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                                If totalCount > 0 Then
                                    Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & " in Product Type Milk.")

                                End If
                            ElseIf clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT where Item_Code='" & Item_Code & "'"
                                Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                                If totalCount > 0 Then
                                    Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & ".")

                                End If
                            End If
                        End If


                        clsCommon.AddColumnsForChange(coll, "Product_Type", Product_Type)

                        'CSA_Type = clsCommon.myCstr(grow.Cells("CSA Type").Value)
                        'clsCommon.AddColumnsForChange(coll, "CSA_Type", CSA_Type)
                        '---------------------------------------------------------------
                        clsCommon.AddColumnsForChange(coll, "Weight_Value", clsCommon.myCdbl(grow.Cells("Weight_Value").Value))
                        '-----------------------------------------------------
                        clsCommon.AddColumnsForChange(coll, "Is_MRP", clsCommon.myCdbl(grow.Cells("Is_MRP").Value))
                        clsCommon.AddColumnsForChange(coll, "ITF_CODE", clsCommon.myCdbl(grow.Cells("ITF_CODE").Value))
                        clsCommon.AddColumnsForChange(coll, "Is_FreshItem", clsCommon.myCdbl(grow.Cells("Is_FreshItem").Value))
                        clsCommon.AddColumnsForChange(coll, "Is_Rate_Change_OnDairyDispatch", clsCommon.myCdbl(grow.Cells("Is_Rate_Change_OnDairyDispatch").Value))
                        clsCommon.AddColumnsForChange(coll, "AllowSRNWithoutShortReject", clsCommon.myCdbl(grow.Cells("AllowSRNWithoutShortReject").Value))
                        clsCommon.AddColumnsForChange(coll, "Is_Ambient", clsCommon.myCdbl(grow.Cells("Is_Ambient").Value))
                        clsCommon.AddColumnsForChange(coll, "Active", clsCommon.myCdbl(grow.Cells("Active").Value))
                        clsCommon.AddColumnsForChange(coll, "Is_CrateType", clsCommon.myCdbl(grow.Cells("Is_CrateType").Value))
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Purchase_Price", std_pur_rate)
                        clsCommon.AddColumnsForChange(coll, "Part_No", Part_No)
                        clsCommon.AddColumnsForChange(coll, "Drawing_No", Drawing_no)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(Datee, "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment", IIf(clsCommon.CompairString("Y", clsCommon.myCstr(grow.Cells("Auto Weightment(Y/N)").Value)) = CompairStringResult.Equal, 1, 0))
                        clsCommon.AddColumnsForChange(coll, "Is_Power_And_Fuel", IIf(clsCommon.CompairString("Y", clsCommon.myCstr(grow.Cells("Power And Fuel(Y/N)").Value)) = CompairStringResult.Equal, 1, 0))
                        If AllowGSTApplicable = True Then
                            clsCommon.AddColumnsForChange(coll, "HSN_Code", HSNCode)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Is_CAN_Type", clsCommon.myCdbl(grow.Cells("Is_CAN_Type").Value))
                        clsCommon.AddColumnsForChange(coll, "Correction_Factor", clsCommon.myCdbl(grow.Cells("Correction Factor").Value))
                        clsCommon.AddColumnsForChange(coll, "Is_Leakage_Not_Applicable", clsCommon.myCdbl(grow.Cells("Leakage Not Applicable").Value))
                        Qry = "Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            'clsCommon.AddColumnsForChange(coll, "Created_Date", Datee)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(Datee, "dd/MMM/yyyy hh:mm tt"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "TSPL_ITEM_MASTER.Item_Code='" + Item_Code + "'", trans)
                        End If

                        clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_ITEM_UOM_DETAIL WHere Item_Code='" + Item_Code + "'", trans)
                        Dim coll1 As New Hashtable()
                        clsCommon.AddColumnsForChange(coll1, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll1, "UOM_Code", Unit_Code)
                        Dim UomDesc As String = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + Unit_Code + "'", trans)
                        clsCommon.AddColumnsForChange(coll1, "UOM_Description", UomDesc)
                        clsCommon.AddColumnsForChange(coll1, "Conversion_Factor", cnvrsnfctr)
                        clsCommon.AddColumnsForChange(coll1, "Stocking_Unit", stckunit)
                        clsCommon.AddColumnsForChange(coll1, "weight", wt)
                        clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub ImportItemUOMDetails()
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim LineNo As String = ""
        Dim countDefaultUOM As Integer = 0
        If transportSql.importExcel(gv1, "Item Code", "UOM", "Conversion Factor", "Stocking Unit", "Weight", "Gross_Weight", "Net_Weight", "Job_Work_Rate", "Pieces", "Item Cost", "Custom Conversion") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            Dim arrStockUOMCost As New Dictionary(Of String, Decimal)
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(i).Cells("UOM").Value) > 0 Then
                        Dim UOM As String = clsCommon.myCstr(gv1.Rows(i).Cells("UOM").Value)
                        Dim FirstItemCode As String = clsCommon.myCstr(gv1.Rows(i).Cells("Item Code").Value)
                        Dim StockUnit As String = clsCommon.myCstr(gv1.Rows(i).Cells("Stocking Unit").Value)
                        For j As Integer = i + 1 To gv1.Rows.Count - 1
                            Dim SecondUOM As String = clsCommon.myCstr(gv1.Rows(j).Cells("UOM").Value)
                            Dim SecondItemCode As String = clsCommon.myCstr(gv1.Rows(j).Cells("Item Code").Value)
                            Dim SecondStockUnit As String = clsCommon.myCstr(gv1.Rows(j).Cells("Stocking Unit").Value)
                            If clsCommon.CompairString(FirstItemCode, SecondItemCode) = CompairStringResult.Equal Then
                                If clsCommon.CompairString(UOM, SecondUOM) = CompairStringResult.Equal Then
                                    Throw New Exception("Please check ! duplicate UOM between Line No '" + clsCommon.myCstr(i + 1) + "' and '" + clsCommon.myCstr(j + 1) + "'")
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Custom Conversion").Value), "Y") = CompairStringResult.Equal Then
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(j).Cells("Custom Conversion").Value), "Y") = CompairStringResult.Equal Then
                                        Throw New Exception("Not more than one UOM Can be of Custom Conversion Type '" + clsCommon.myCstr(i + 1) + "' and '" + clsCommon.myCstr(j + 1) + "'")
                                    End If
                                End If
                            End If
                            If clsCommon.CompairString(StockUnit, "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(StockUnit, SecondStockUnit) = CompairStringResult.Equal And clsCommon.CompairString(FirstItemCode, SecondItemCode) = CompairStringResult.Equal Then
                                    Throw New Exception("The Coversion Unit Should be [1] for Stocking Unit [Yes] for item :" + FirstItemCode)
                                End If
                                If Not arrStockUOMCost.ContainsKey(FirstItemCode) Then
                                    '======================================================================
                                    If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemCostMandatoryForStockingUnit, clsFixedParameterCode.AllowItemCostMandatoryForStockingUnit, trans)) = 1, True, False) = True Then
                                        If clsCommon.myCdbl(gv1.Rows(i).Cells("Item Cost").Value) <= 0 Then
                                            Throw New Exception("Item Cost Can not be blank Or Zero for Stocking Unit [Yes] in Item Code " + FirstItemCode + ".")
                                        End If
                                    End If
                                    '======================================================================
                                    arrStockUOMCost.Add(FirstItemCode, clsCommon.myCdbl(gv1.Rows(i).Cells("Item Cost").Value))
                                End If
                            End If
                        Next
                    End If
                Next

                Dim IsDefaultUOM As Double = 0
                Dim DefaultUOMCount As Double = 0
                Dim DefaultPiecesCount As Double = 0
                Dim MergedItemCodes As String = String.Empty
                For Each grow As GridViewRowInfo In gv1.Rows
                    LineNo = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()
                    Dim strItemCode As String
                    Dim itemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(itemCode) >= 0 Then
                        strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER Where Item_Code='" + itemCode + "'", trans))
                        If clsCommon.myLen(strItemCode) <= 0 Then
                            Throw New Exception("The Item '" + itemCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Item Code at Line No '" + LineNo + "' ")
                    End If
                    MergedItemCodes = MergedItemCodes + "," + "'" + itemCode + "'"
                    Dim strUOM As String
                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(UnitCode) > 0 Then
                        strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                        If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("The UOM '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert UOM at Line No '" + LineNo + "' ")
                    End If


                    Dim StockCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select SUM(Item_Code) as Item_Code From (Select Count(Item_Code) as Item_Code from tspl_inventory_movement Where Item_Code='" + strItemCode + "' AND Stock_UOM ='" + strUOM + "' union all Select Count(Item_Code) as Item_Code from TSPL_INVENTORY_MOVEMENT_NEW Where Item_Code='" + strItemCode + "' AND Stock_UOM ='" + strUOM + "')mm", trans))
                    If StockCount >= 1 AndAlso clsCommon.myCdbl(grow.Cells("Conversion Factor").Value) <> 1 Then
                        Throw New Exception("Unit '" + UnitCode + "' is used as  Stocking unit,please set Conversion Factor 1 at Line No '" + LineNo + "'")
                    End If

                    Dim UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER Where Unit_Code='" + strUOM + "'", trans))
                    clsCommon.AddColumnsForChange(coll, "UOM_Description", UOM_Description)


                    If clsCommon.myCstr(grow.Cells("Stocking Unit").Value) = "Y" Then
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "Y")
                        If clsCommon.myCdbl(grow.Cells("Conversion Factor").Value) <> 1 Then
                            Throw New Exception("Please set Conversion Factor 1 at Line No '" + LineNo + "' ")
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "N")

                    End If
                    If clsCommon.myLen(grow.Cells("Default UOM").Value) > 0 Then
                        DefaultUOMCount = +1
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM").Value), "1") = CompairStringResult.Equal Then
                            IsDefaultUOM = IsDefaultUOM + 1
                            clsCommon.AddColumnsForChange(coll, "Default_UOM", 1)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM").Value), "0") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Default_UOM", 0)
                        Else
                            Throw New Exception("Please enter Default UOM As '1' Or '0' at line '" + LineNo + "'.")
                        End If
                    Else
                        Throw New Exception("Please enter Default UOM As '1' Or '0' at line '" + LineNo + "'.")
                    End If
                    '========================Added by preeti Gupta[01/05/2018] against Ticket No[ERO/01/05/18-000285]================================
                    If clsCommon.myLen(grow.Cells("Pieces").Value) > 0 Then
                        DefaultPiecesCount = +1
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Pieces").Value), "1") = CompairStringResult.Equal Then
                            DefaultPiecesCount = DefaultPiecesCount + 1
                            clsCommon.AddColumnsForChange(coll, "Pieces", 1)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Pieces").Value), "0") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Pieces", 0)
                        Else
                            Throw New Exception("Please enter Pieces As '1' Or '0' at line '" + LineNo + "'.")
                        End If
                    Else
                        Throw New Exception("Please enter Pieces As '1' Or '0' at line '" + LineNo + "'.")
                    End If
                    Dim ConversionFactor As Double = clsCommon.myCdbl(grow.Cells("Conversion Factor").Value)
                    If ConversionFactor > 0 Then
                        clsCommon.AddColumnsForChange(coll, "COnversion_Factor", ConversionFactor)
                    Else
                        Throw New Exception("Please Insert Convrsion Factor at Line No '" + LineNo + "' ")
                    End If
                    If clsCommon.myLen(grow.Cells("Weight").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("Weight").Value) Then
                            Throw New Exception("Please insert decimal data in Weight at Line No '" + LineNo + "' ")
                        Else
                            clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                    End If
                    clsCommon.AddColumnsForChange(coll, "Gross_Weight", clsCommon.myCdbl(grow.Cells("Gross_Weight").Value))
                    clsCommon.AddColumnsForChange(coll, "Net_Weight", clsCommon.myCdbl(grow.Cells("Net_Weight").Value))
                    clsCommon.AddColumnsForChange(coll, "Job_Work_Rate", clsCommon.myCdbl(grow.Cells("Job_Work_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", Math.Round(arrStockUOMCost(itemCode) * ConversionFactor, 2, MidpointRounding.AwayFromZero))
                    clsCommon.AddColumnsForChange(coll, "Custom_Conversion", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Custom Conversion").Value), "Y") = CompairStringResult.Equal, 1, 0))
                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + strItemCode + "' AND UOM_Code='" + strUOM + "'", trans))
                    If Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", strItemCode)
                        clsCommon.AddColumnsForChange(coll, "UOM_Code", strUOM)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + strItemCode + "' and uom_code='" + strUOM + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
                If MergedItemCodes.Length > 0 Then
                    If MergedItemCodes.Substring(0, 1) = "," Then
                        MergedItemCodes = MergedItemCodes.Substring(1, MergedItemCodes.Length - 1)
                    End If
                End If
                Dim DuplicateEntry As String = String.Empty
                Dim NoDefaultUOM As String = String.Empty
                NoDefaultUOM = "select count(*) As Row,Item_Code from tspl_item_uom_detail Where Item_Code IN (" & MergedItemCodes & ") group by Item_Code having sum(default_uom) =0"
                DuplicateEntry = "select Item_Code,Default_UOM, SUM(1) as Repeated from tspl_item_uom_detail Where Default_UOM=1 AND Item_Code IN (" & MergedItemCodes & ") group by Item_Code,Default_UOM  having SUM(1) > 1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(NoDefaultUOM, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! Item code (" & clsCommon.myCstr(dt.Rows(0)("Item_Code")) & ") with default UOM (" & clsCommon.myCstr(dt.Rows(0)("Default_UOM")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    Throw New Exception("Please set default UOM for item code (" & clsCommon.myCstr(dt1.Rows(0)("Item_Code")) & ").")
                End If
                If isSaved Then
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully ...")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show("Error at Line No " + LineNo + Environment.NewLine + ex.Message)
            Finally
                Me.Controls.Remove(gv1)
            End Try
        End If
    End Sub
    Private Sub ImportItemCategoryStructure()
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        ''-Insertig for ITem MAster
        If transportSql.importExcel(gv1, "Item Code", "Category Structure", "line_no", "Category Code", "Category Value") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim strItemCode As String = ""
            Dim strCategoryStructure As String = ""
            Dim strCategoryCode As String = ""
            Dim strCategoryValue As String = ""
            Dim whrClas As String = ""
            Dim sno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                Dim Item_Code As String = ""
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()
                    Dim itemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(itemCode) >= 0 Then
                        strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER Where Item_Code='" + itemCode + "'", trans))
                        If clsCommon.myLen(strItemCode) <= 0 Then
                            Throw New Exception("The Item '" + itemCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        Else
                            clsCommon.AddColumnsForChange(coll, "Item_Code", strItemCode)
                        End If
                    Else
                        Throw New Exception("Please Insert Item Code at Line No '" + LineNo + "' ")
                    End If

                    Dim CategoryStructure As String = clsCommon.myCstr(grow.Cells("Category Structure").Value)
                    If clsCommon.myLen(CategoryStructure) > 0 Then
                        strCategoryStructure = clsDBFuncationality.getSingleValue("Select ITEM_CATEGORY_STRUCT_CODE from TSPL_ITEM_CATEGORY_STRUCTURE WHERE ITEM_CATEGORY_STRUCT_CODE='" + CategoryStructure + "' and isnull(form_type,'item')='item'", trans)
                        If clsCommon.CompairString(strCategoryStructure, CategoryStructure) = CompairStringResult.Equal Then
                            'clsCommon.AddColumnsForChange(coll, "UOM_Code", strCategoryStructure)
                        Else
                            Throw New Exception("The Category Structure '" + CategoryStructure + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Category Structure at Line No '" + LineNo + "' ")
                    End If

                    sno = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select CATEGORY_LEVEL from TSPL_ITEM_CATEGORY_LEVEL WHERE ITEM_CATEGORY_CODE='" + strCategoryCode + "' and isnull(form_type,'item')='item'", trans))
                    clsCommon.AddColumnsForChange(coll, "SNO", sno)
                    '------------Structure Category Code--------------------
                    Dim CategoryCode As String = clsCommon.myCstr(grow.Cells("Category Code").Value)
                    If clsCommon.myLen(CategoryCode) > 0 Then
                        strCategoryCode = clsDBFuncationality.getSingleValue("Select ITEM_CATEGORY_CODE from TSPL_ITEM_CATEGORY_STRUCT_DETAIL WHERE ITEM_CATEGORY_STRUCT_CODE='" + strCategoryStructure + "' AND ITEM_CATEGORY_CODE='" + CategoryCode + "' and isnull(form_type,'item')='item'", trans)
                        If clsCommon.CompairString(strCategoryCode, CategoryCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Item_Category_Code", strCategoryCode)
                        Else
                            Throw New Exception("Category Code at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Category Code at Line No '" + LineNo + "' ")
                    End If
                    '----------------------------------------------

                    '------------Category Value--------------------

                    Dim CategoryValue As String = clsCommon.myCstr(grow.Cells("Category Value").Value)
                    If clsCommon.myLen(CategoryValue) > 0 Then
                        strCategoryValue = clsDBFuncationality.getSingleValue("Select CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES WHERE ITEM_CATEGORY_CODE='" + strCategoryCode + "' AND CODE='" + CategoryValue + "' and isnull(form_type,'item')='item'", trans)
                        If clsCommon.CompairString(strCategoryValue, CategoryValue) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Item_Cagetory_Values", strCategoryValue)
                        Else
                            Throw New Exception("Category Value at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Category Value at Line No '" + LineNo + "' ")
                    End If
                    '----------------------------------------------
                    '=====================added By preeti Gupta=================
                    Dim strBinNo As String = clsDBFuncationality.getSingleValue(" select Bin_No  from TSPL_ITEM_CATEGORY_LEVEL_VALUES where 2=2 and ITEM_CATEGORY_CODE ='" + strCategoryCode + "' and  code='" + strCategoryValue + "'", trans)

                    '===================================================
                    If Not clsCommon.CompairString(Item_Code, itemCode) = CompairStringResult.Equal Then
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ITEM_MASTER_CATEGORY WHERE Item_code='" + strItemCode + "'", trans)
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_MASTER Set Item_Category_Struct_Code='" + strCategoryStructure + "' WHERE Item_Code='" + strItemCode + "'", trans)
                        '=============================Added by Preeti Gupta======================
                        If clsCommon.myLen(strBinNo) > 0 Then
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_MASTER Set RACK_NO='" + strBinNo + "' WHERE Item_Code='" + strItemCode + "'", trans)
                        End If
                        '=================================================================================
                    End If
                    Item_Code = strItemCode

                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COunt(*) from TSPL_ITEM_MASTER_CATEGORY WHERE Item_code='" + strItemCode + "' AND Item_Category_Code='" + strCategoryCode + "'", trans))
                    If Count <= 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER_CATEGORY", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        whrClas = "Item_code='" + strItemCode + "' AND Item_Category_Code='" + strCategoryStructure + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER_CATEGORY", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
                If isSaved Then
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully ...")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gv1)
            End Try
        End If

    End Sub
    Private Sub gv_param_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_param.CellDoubleClick
        Try
            If e.Column Is gv_param.Columns(colActualvalue) AndAlso clsCommon.myLen(clsCommon.myCstr(gv_param.CurrentRow.Cells(colparamCode).Value)) > 0 Then
                Dim frm As New FrmCheckBoxGrid()
                frm.qry = "select value from tspl_Parameter_value_master where parameter_code='" + clsCommon.myCstr(gv_param.CurrentRow.Cells(colparamCode).Value) + "'"
                frm.ShowDialog()

                Dim arrValue As New List(Of String)

                arrValue = frm.arrValue
                gv_param.CurrentRow.Cells(colActualvalue).Value = ""
                If arrValue IsNot Nothing AndAlso arrValue.Count > 0 Then
                    For Each arr As String In arrValue
                        gv_param.CurrentRow.Cells(colActualvalue).Value = clsCommon.myCstr(gv_param.CurrentRow.Cells(colActualvalue).Value) + "," + clsCommon.myCstr(arr)

                        If clsCommon.myCstr(gv_param.CurrentRow.Cells(colActualvalue).Value).Substring(0, 1) = "," Then
                            gv_param.CurrentRow.Cells(colActualvalue).Value = clsCommon.myCstr(gv_param.CurrentRow.Cells(colActualvalue).Value).Substring(1, clsCommon.myCstr(gv_param.CurrentRow.Cells(colActualvalue).Value).Length - 1)
                        End If
                    Next
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv_param_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_param.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv_param.Columns(colparamCode) Then
                        OpenQCParameters(False)
                    End If
                    If gv_param.CurrentColumn Is gv_param.Columns(colActualRange) Then
                        gv_param.CurrentRow.Cells(colActualstatus).Value = ""
                        gv_param.CurrentRow.Cells(colActualvalue).Value = ""
                    End If

                    If gv_param.CurrentColumn Is gv_param.Columns(colActualstatus) Then
                        gv_param.CurrentRow.Cells(colActualvalue).Value = ""
                        gv_param.CurrentRow.Cells(colActualRange).Value = Nothing
                    End If

                    If gv_param.CurrentColumn Is gv_param.Columns(colActualvalue) Then
                        gv_param.CurrentRow.Cells(colActualstatus).Value = ""
                        gv_param.CurrentRow.Cells(colActualRange).Value = Nothing
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenQCParameters(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select a.* from (select ROW_NUMBER() over (partition by tspl_parameter_range_master_qc.code order by tspl_parameter_range_master_qc.Effective_Date desc) as sno,tspl_parameter_master.nature,tspl_parameter_range_master_qc.Code,tspl_parameter_master.Description,tspl_parameter_range_master_qc.effective_date as [Effective Date],tspl_parameter_range_master_qc.lower_range as [Lower Range],tspl_parameter_range_master_qc.upper_range as [Upper Range],tspl_parameter_range_master_qc.Status,tspl_parameter_range_master_qc.value1 as [Value-1],tspl_parameter_range_master_qc.value2 as [Value-2] from tspl_parameter_range_master_qc left outer join tspl_parameter_master on tspl_parameter_range_master_qc.code=tspl_parameter_master.code where tspl_parameter_range_master_qc.trans_id='PRODUCTION')a where a.sno='1'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ItemQCFND", qry)

        If dr IsNot Nothing Then
            gv_param.CurrentRow.Cells(colparamCode).Value = clsCommon.myCstr(dr("code"))
            gv_param.CurrentRow.Cells(colparamnature).Value = clsCommon.myCstr(dr("nature"))
            gv_param.CurrentRow.Cells(colparamDesc).Value = clsCommon.myCstr(dr("description"))
            gv_param.CurrentRow.Cells(colparamLower).Value = clsCommon.myCdbl(dr("lower range"))
            gv_param.CurrentRow.Cells(colparamUpper).Value = clsCommon.myCdbl(dr("upper range"))
            gv_param.CurrentRow.Cells(colparamStatus).Value = clsCommon.myCstr(dr("status"))
            gv_param.CurrentRow.Cells(colparamValue1).Value = clsCommon.myCstr(dr("value-1"))
            gv_param.CurrentRow.Cells(colparamValue2).Value = clsCommon.myCstr(dr("value-2"))
            Try
                gv_param.CurrentRow.Cells(colparamDate).Value = Convert.ToDateTime(dr("effective_date"))
            Catch exx As Exception
                gv_param.CurrentRow.Cells(colparamDate).Value = Nothing
            End Try

            Try
                If clsCommon.myLen(gv_param.CurrentRow.Cells(colparamDate).Value) > 0 AndAlso clsCommon.myCstr(gv_param.CurrentRow.Cells(colparamDate).Value).Substring(6, 4) = "0001" Then
                    gv_param.CurrentRow.Cells(colparamDate).Value = Nothing
                End If
            Catch exx As Exception
                gv_param.CurrentRow.Cells(colparamDate).Value = Nothing
            End Try
        Else
            gv_param.CurrentRow.Cells(colparamCode).Value = ""
            gv_param.CurrentRow.Cells(colparamDesc).Value = ""
            gv_param.CurrentRow.Cells(colparamnature).Value = ""
            gv_param.CurrentRow.Cells(colparamLower).Value = Nothing
            gv_param.CurrentRow.Cells(colparamUpper).Value = Nothing
            gv_param.CurrentRow.Cells(colparamStatus).Value = ""
            gv_param.CurrentRow.Cells(colparamValue1).Value = ""
            gv_param.CurrentRow.Cells(colparamValue2).Value = ""
            gv_param.CurrentRow.Cells(colparamDate).Value = Nothing
            gv_param.CurrentRow.Cells(colActualstatus).Value = ""
            gv_param.CurrentRow.Cells(colActualvalue).Value = ""
            gv_param.CurrentRow.Cells(colActualRange).Value = Nothing
        End If
    End Sub
    Private Sub gv_param_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_param.CurrentColumnChanged
        If gv_param.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_param.CurrentRow.Index
            If intCurrRow = gv_param.Rows.Count - 1 Then
                gv_param.Rows.AddNew()
                gv_param.CurrentRow = gv_param.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub btnparam_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnparam_import.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If transportSql.importExcel(gv1, "Item_Code", "Code", "Description", "Lower_Range", "Upper_Range", "Status", "Value1", "Actual_Range", "Actual_Value", "Actual_Status", "StandardRate") Then

            clsCommon.ProgressBarShow()
            Try
                Dim Item_Code As String = ""
                Dim paramcode As String = ""
                Dim paramdesc As String = ""
                Dim Lrange As Decimal = 0
                Dim Urange As Decimal = 0
                Dim status As String = ""
                Dim value1 As String = ""
                Dim value2 As String = ""
                Dim act_range As Decimal = 0
                Dim act_value As String = ""
                Dim act_status As String = ""
                Dim qry As String = ""
                Dim ismendatory As Integer = 0
                Dim nature As String = ""
                Dim StandradRate As Decimal = 0
                Dim check As Integer = 0
                Dim counter As Integer = 1
                For Each grow As GridViewRowInfo In gv1.Rows
                    Item_Code = clsCommon.myCstr(grow.Cells("item_code").Value)
                    If clsCommon.myLen(Item_Code) <= 0 Then
                        Throw New Exception("Please fill item code at line no. " + clsCommon.myCstr(counter) + "")
                    Else
                        qry = "select count(*) from tspl_item_master where item_code='" + Item_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Item does not exist in item master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    paramcode = clsCommon.myCstr(grow.Cells("code").Value)
                    paramdesc = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(paramcode) <= 0 Then
                        Throw New Exception("Fill Parameter Code/Description at line no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.myLen(paramcode) > 0 Then
                        qry = "select count(*) from tspl_parameter_master where code='" + paramcode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Parameter Code does not exist in parameter master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    qry = "select isnull(IsMandatory,0) as IsMandatory from TSPL_PARAMETER_MASTER where code='" + paramcode + "'"
                    ismendatory = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                    qry = "select nature from TSPL_PARAMETER_MASTER where code='" + paramcode + "'"
                    nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))


                    Lrange = clsCommon.myCdbl(grow.Cells("Lower_Range").Value)
                    Urange = clsCommon.myCdbl(grow.Cells("Upper_Range").Value)
                    status = clsCommon.myCstr(grow.Cells("Status").Value)
                    value1 = clsCommon.myCstr(grow.Cells("Value1").Value)
                    value2 = "" ' clsCommon.myCstr(grow.Cells("Value2").Value)

                    StandradRate = clsCommon.myCdbl(grow.Cells("StandardRate").Value)

                    If clsCommon.CompairString(nature, "A") = CompairStringResult.Equal AndAlso clsCommon.myLen(value1) <= 0 Then
                        Throw New Exception("Fill value1 at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal AndAlso clsCommon.myLen(value1) > 0 Then
                        Lrange = 0
                        Urange = 0
                        status = ""
                        qry = "select count(*) from TSPL_PARAMETER_RANGE_MASTER_QC where trans_id='PRODUCTION' and code='" + paramcode + "' and value1 like '%" + value1 + "%'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled value1 is invalid at row no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.CompairString(nature, "R") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(Lrange) <= 0 AndAlso clsCommon.myCdbl(Urange) <= 0 Then
                        Throw New Exception("Fill lower range and upper range at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(Lrange) > 0 OrElse clsCommon.myCdbl(Urange) > 0) Then
                        status = ""
                        value1 = ""
                        qry = "select count(*) from TSPL_PARAMETER_RANGE_MASTER_QC where trans_id='PRODUCTION' and code='" + paramcode + "' and lower_range = '" + clsCommon.myCstr(Lrange) + "' and upper_range='" + clsCommon.myCstr(Urange) + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled lower range and upper range is invalid at row no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(status) <= 0 Then
                        Throw New Exception("Fill status at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(status) > 0 Then
                        Lrange = 0
                        Urange = 0
                        value1 = ""
                        qry = "select count(*) from TSPL_PARAMETER_RANGE_MASTER_QC where trans_id='PRODUCTION' and code='" + paramcode + "' and status = '" + status + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled status is invalid at row no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    act_range = clsCommon.myCdbl(grow.Cells("Actual_Range").Value)
                    act_value = clsCommon.myCstr(grow.Cells("Actual_Value").Value)
                    act_status = clsCommon.myCstr(grow.Cells("Actual_Status").Value)
                    If clsCommon.CompairString(nature, "A") = CompairStringResult.Equal AndAlso ismendatory = 1 AndAlso clsCommon.myLen(act_value) <= 0 Then
                        Throw New Exception("Fill actual value at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "A") = CompairStringResult.Equal AndAlso clsCommon.myLen(act_value) > 0 AndAlso Not value1.ToUpper().Contains(act_value.ToUpper()) Then
                        Throw New Exception("Filled actual value should meet standard value at row no. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(nature, "R") = CompairStringResult.Equal AndAlso ismendatory = 1 AndAlso clsCommon.myCdbl(act_range) <= 0 Then
                        Throw New Exception("Fill actual range at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "R") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(act_range) > clsCommon.myCdbl(Urange) OrElse clsCommon.myCdbl(act_range) < clsCommon.myCdbl(Lrange)) Then
                        Throw New Exception("Filled actual range should meet standard lower range and upper range at row no. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal AndAlso ismendatory = 1 AndAlso clsCommon.myLen(act_status) <= 0 Then
                        Throw New Exception("Fill actual status at row no. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(nature, "B") = CompairStringResult.Equal AndAlso clsCommon.myLen(act_status) > 0 AndAlso Not status.ToUpper().Contains(act_status.ToUpper()) Then
                        Throw New Exception("Filled actual status should meet standard status at row no. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim coll As New Hashtable()
                    Dim isSaved As Boolean = True

                    qry = "delete from TSPL_ITEM_QC_PARAMETER_MASTER where item_code='" + Item_Code + "' and code='" + paramcode + "' and lower_range='" + clsCommon.myCstr(Lrange) + "' and upper_range='" + clsCommon.myCstr(Urange) + "' and status='" + status + "' and value1='" + value1 + "' and value2='" + value2 + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Code", paramcode)
                    clsCommon.AddColumnsForChange(coll, "Lower_range", Lrange)
                    clsCommon.AddColumnsForChange(coll, "Upper_range", Urange)
                    clsCommon.AddColumnsForChange(coll, "Status", status)
                    clsCommon.AddColumnsForChange(coll, "Value1", value1)
                    clsCommon.AddColumnsForChange(coll, "Value2", value2)
                    clsCommon.AddColumnsForChange(coll, "Actual_Range", act_range)
                    clsCommon.AddColumnsForChange(coll, "Actual_Value", act_value)
                    clsCommon.AddColumnsForChange(coll, "Actual_Status", act_status)
                    clsCommon.AddColumnsForChange(coll, "StandardRate", StandradRate)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode, True)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_QC_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)

                    qry = " update TSPL_ITEM_MASTER set STD_FatPer=QC.Fat_Per,STD_SNFPer=QC.SNF_Per from (select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from ( select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range else 0 end) as Fat_Per, " &
                     " (case when QCP.Type='SNF' then Item_QCP.Actual_Range else 0  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP  left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code where Item_QCP.Item_Code='" & Item_Code & "') as QC  group by Item_Code) QC " &
                     " where TSPL_ITEM_MASTER.Item_Code=QC.Item_Code and TSPL_ITEM_MASTER.Item_Code='" & Item_Code & "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    counter += 1
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv1)
    End Sub
    Private Sub btnparam_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnparam_export.Click
        Dim qry As String = "select count(*) from TSPL_ITEM_QC_PARAMETER_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Code,tspl_parameter_master.Description,TSPL_ITEM_QC_PARAMETER_MASTER.Lower_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Upper_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Status,TSPL_ITEM_QC_PARAMETER_MASTER.Value1,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Value,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Status,TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join tspl_parameter_master on TSPL_ITEM_QC_PARAMETER_MASTER.code=tspl_parameter_master.code"
        Else
            qry = "select '' as Item_Code,'' as Code,'' as Description,0 as Lower_Range,0 as Upper_Range,'' as Status,'' as Value1,'' as Actual_Range,'' as Actual_Value,'' as Actual_Status,0 as StandardRate "
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"item_code", "code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory)
    End Sub
    Private Sub BtnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim FileName As String = OpenFileDialog1.FileName
                If clsCommon.myLen(FileName) > 0 Then
                    Using ms As New IO.MemoryStream(CType(GetPhoto(FileName), Byte()))
                        Dim img As Image = Image.FromStream(ms)
                        PicImage.Image = img
                    End Using
                    File_Name = OpenFileDialog1.FileName
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
    Public Function GetPhoto(ByVal filePath As String) As Byte()
        Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim reader As BinaryReader = New BinaryReader(stream)
        Dim photo() As Byte = reader.ReadBytes(CInt(stream.Length))
        reader.Close()
        stream.Close()
        Return photo
    End Function
    Sub LoadItemCSAType()
        Dim qry As String = Nothing
        qry = "select distinct code,Name  from( " &
                " select 'None' as Code,'None' as Name "
        If AllowDoNotShowDairyTypeItems = False Then
            qry += " union all select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name " &
               " union all select 'BULK -DESI GHEE' as Code,'BULK-DESI GHEE' as Name" &
               " union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name " &
               " union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name" &
               " union all select distinct CSA_TYPE as Code,CSA_TYPE as Name "
        End If
        qry += " from TSPL_ITEM_MASTER ) xx"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        cboCSAType.DataSource = dt
        cboCSAType.ValueMember = "Code"
        cboCSAType.DisplayMember = "Name"
        'Return dt
    End Sub
    Private Sub btnSerializedExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerializedExport.Click
        Try
            qry = "Select Item_Code as [Item Code], Is_Serial_Item as [Is Serialized Item] ,isnull(Serial_Counter,'') as [Serial Counter] ,isnull(WARRANTY_CODE,'') as [Warranty Code],isnull(Asset_Life,0) as [Asset Life],isnull(Warranty_Period,0) as [Warranty Period] from TSPL_ITEM_MASTER "
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "Warranty Code"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSerializedImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerializedImport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If transportSql.importExcel(gv1, "Item Code", "Is Serialized Item", "Serial Counter", "Warranty Code", "Asset Life", "Warranty Period") Then

            clsCommon.ProgressBarShow()
            Try
                Dim Item_Code As String = ""
                Dim SerialCounter As String = ""
                Dim WarrantyCode As String = ""
                Dim AssetLife As Decimal = 0
                Dim WarrantyPeriod As Decimal = 0

                Dim check As Integer = 0
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv1.Rows
                    Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(Item_Code) <= 0 Then
                        Throw New Exception("Please fill item code at line no. " + clsCommon.myCstr(counter) + "")
                    Else
                        qry = "select count(*) from tspl_item_master where item_code='" + Item_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Item does not exist in item master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If



                    Dim strSerializedItem As String = clsCommon.myCstr(grow.Cells("Is Serialized Item").Value).ToUpper()
                    If clsCommon.myLen(strSerializedItem) > 0 Then
                        If clsCommon.CompairString(strSerializedItem, "1") = CompairStringResult.Equal Or clsCommon.CompairString(strSerializedItem, "0") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Default UOM should be 1 or 0,see at Line No " + clsCommon.myCstr(counter) + " ")
                        End If
                    End If


                    SerialCounter = clsCommon.myCstr(grow.Cells("Serial Counter").Value).ToUpper()

                    WarrantyCode = clsCommon.myCstr(grow.Cells("Warranty Code").Value)
                    If clsCommon.myLen(strSerializedItem) > 0 Then
                        If clsCommon.myLen(WarrantyCode) <= 0 Then
                            Throw New Exception("Please fill Warranty Code at line no. " + clsCommon.myCstr(counter) + "")
                        Else
                            qry = "select count(*) from TSPL_WARRANTY_MASTER where Code='" + WarrantyCode + "'"
                            check = clsDBFuncationality.getSingleValue(qry, trans)
                            If check <= 0 Then
                                Throw New Exception("Warranty Code not exist in Warranty Master main data,see at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                    End If

                    If IsNumeric(grow.Cells("Asset Life").Value) Then
                        AssetLife = clsCommon.myCdbl(grow.Cells("Asset Life").Value)
                    Else
                        Throw New Exception("Asset Life should be numeric,see at line no. " + clsCommon.myCstr(counter) + "")
                    End If

                    If IsNumeric(grow.Cells("Warranty Period").Value) Then
                        WarrantyPeriod = clsCommon.myCdbl(grow.Cells("Warranty Period").Value)
                    Else
                        Throw New Exception("Warranty Period should be numeric,see at line no. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim coll As New Hashtable()
                    If clsCommon.CompairString(strSerializedItem, "1") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                        clsCommon.AddColumnsForChange(coll, "Is_Serial_Item", strSerializedItem)
                        clsCommon.AddColumnsForChange(coll, "Serial_Counter", SerialCounter)
                        clsCommon.AddColumnsForChange(coll, "WARRANTY_CODE", WarrantyCode)
                        clsCommon.AddColumnsForChange(coll, "Warranty_Period", WarrantyPeriod)
                        clsCommon.AddColumnsForChange(coll, "Asset_Life", AssetLife)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, " Item_Code='" & Item_Code & "'", trans)
                    End If
                    counter += 1
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data imported Successfully", Me.Text)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv1)
    End Sub
    Private Function ItemConversionAutomationImport(ByVal IntRowNo As Integer, ByVal strStockingUnit As String, ByVal strUnit As String, ByVal strWeightUom As String, ByVal dblWeightValue As Double, ByVal strStructure As String, ByVal lineNo As Double, ByVal Trans As SqlTransaction) As Double
        IntRowNo = IntRowNo - 1
        Dim dblConvF As Double = 0
        Dim IsStockingUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", Trans))
        Dim StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", Trans))
        If IntRowNo = 0 Then
            If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                strWeightUom = strStockingUnit
                dblWeightValue = 1
                strWeightImp = strWeightUom
                dblWeightImp = dblWeightValue
            End If
        Else
            Dim IsUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strUnit & "'", Trans))
            Dim IsUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strUnit & "'", Trans))
            Dim IsUnitPackingType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Packet_Type from tspl_unit_master where Unit_Code='" & strUnit & "'", Trans))
            If clsCommon.CompairString(IsUnitWeight, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IsUnitPackingType, "N") = CompairStringResult.Equal Then
                If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUnit & "' and Contained_UOM='" & strStockingUnit & "'", Trans))
                    Else
                        If clsCommon.myLen(strStructure) = 0 Then
                            Throw New Exception("Please enter Structure Code at line no '" + lineNo + "' ")
                        End If
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUnit & "' and Contained_UOM='" & strStockingUnit & "' and Structure_Code='" & strStructure & "'", Trans))
                    End If
                    If dblConvF = 0 Then
                        Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUnit & " Contained Unit - " & strStockingUnit & " for line no '" + clsCommon.myCstr(lineNo) + "' ")
                    End If
                Else
                    If clsCommon.myLen(txtWeightUOM.Value) = 0 Then
                        Throw New Exception("Please enter Weight UOM at line no '" + lineNo + "' ")
                    ElseIf clsCommon.myCdbl(txtWeightValue.Value) = 0 Then
                        Throw New Exception("Please enter Weight UOM Conversion at line no '" + lineNo + "' ")
                    End If
                    strStockingUnit = strWeightUom
                    StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", Trans))
                    If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUnit) & "' and Contained_UOM='" & strStockingUnit & "'", Trans))
                    Else
                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUnit) & "' and Contained_UOM='" & strStockingUnit & "' and Structure_Code='" & clsCommon.myCstr(strStockingUnit) & "'", Trans))
                    End If
                    If dblConvF > 0 Then
                        dblConvF = dblConvF / clsCommon.myCdbl(dblWeightValue)
                    Else
                        Throw New Exception("Please enter Weight Conversion in Weight master Container unit - " & strUnit & " Contained Unit - " & strStockingUnit & " for line no '" + lineNo + "' ")
                    End If

                End If

            Else

            End If
        End If
        Return dblConvF
    End Function
    Private Sub rmWholeImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmWholeImport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim countDefaultUOM As Integer = 0
        Dim inputs() As String = {}
        Dim Strs As List(Of String) = New List(Of String)(inputs)
        If AllowGSTApplicable = True Then
            inputs = {"Item Code", "Item Description", "Seq No", "Short Description", "Structure Code", "Rack No", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Item Type", "Type", "Cost", "Tolerence", "Shelf Life", "Min Shelf Life", "Standard Purchase Price", "Morning", "Production Tolerance", "Chapter Code", "Category Structure", "Weight_UOM", "Weight_Value", "Is_MRP", "ITF_CODE", "Active", "Is_FreshItem", "Is_Ambient", "Product Type", "Is Purchaseable", "Is Allow QC on Purchase", "Is_CrateType", "Is_CAN_Type", "Used As", "Part_No", "Drawing_No", "Item_Cagetory_Values", "Is_Serial_Item", "Serial_Counter", "Is_Pick_Auto_SrNo", "Tax Exempted", "GL Account", "Account Description", "Warranty Code", "Warranty Applied From", "Alies Name", "Leakage Not Applicable", "UOM1", "UOM2", "UOM3", "UOM4", "UOM5", "UOM6", "UOM7", "UOM8", "UOM9", "UOM10", "Conversion Factor1", "Conversion Factor2", "Conversion Factor3", "Conversion Factor4", "Conversion Factor5", "Conversion Factor6", "Conversion Factor7", "Conversion Factor8", "Conversion Factor9", "Conversion Factor10", "Default UOM1", "Default UOM2", "Default UOM3", "Default UOM4", "Default UOM5", "Default UOM6", "Default UOM7", "Default UOM8", "Default UOM9", "Default UOM10", "Stocking Unit1", "Stocking Unit2", "Stocking Unit3", "Stocking Unit4", "Stocking Unit5", "Stocking Unit6", "Stocking Unit7", "Stocking Unit8", "Stocking Unit9", "Stocking Unit10", "Weight1", "Weight2", "Weight3", "Weight4", "Weight5", "Weight6", "Weight7", "Weight8", "Weight9", "Weight10", "Gross_Weight1", "Gross_Weight2", "Gross_Weight3", "Gross_Weight4", "Gross_Weight5", "Gross_Weight6", "Gross_Weight7", "Gross_Weight8", "Gross_Weight9", "Gross_Weight10", "Item Category Code1", "Item Category Code Description1", "Item Category Code2", "Item Category Code Description2", "Item Category Code3", "Item Category Code Description3", "Item Category Code4", "Item Category Code Description4", "Item Category Code5", "Item Category Code Description5", "Item Category Code6", "Item Category Code Description6", "Item Category Code7", "Item Category Code Description7", "Item Category Code8", "Item Category Code Description8", "Item Category Code9", "Item Category Code Description9", "Item Category Code10", "Item Category Code Description10", "Item Category Code11", "Item Category Code Description11", "Item Category Code12", "Item Category Code Description12", "Item Category Code13", "Item Category Code Description13", "Item Category Code14", "Item Category Code Description14", "Item Category Code15", "Item Category Code Description15", "Item Category Level1", "Item Category Level Desp1", "Item Category Level2", "Item Category Level Desp2", "Item Category Level3", "Item Category Level Desp3", "Item Category Level4", "Item Category Level Desp4", "Item Category Level5", "Item Category Level Desp5", "Item Category Level6", "Item Category Level Desp6", "Item Category Level7", "Item Category Level Desp7", "Item Category Level8", "Item Category Level Desp8", "Item Category Level9", "Item Category Level Desp9", "Item Category Level10", "Item Category Level Desp10", "Item Category Level11", "Item Category Level Desp11", "Item Category Level12", "Item Category Level Desp12", "Item Category Level13", "Item Category Level Desp13", "Item Category Level14", "Item Category Level Desp14", "Item Category Level15", "Item Category Level Desp15", "Is_Rate_Change_OnDairyDispatch", "Is_Scheme_Item", "Distributor_Commission", "CNF_Commission", "CSA_Type", "IsTaxable", "is_Batch_Item", "AllowSRNWithoutShortReject", "HSN Code", "Chilled Freezen", "IS SCRAP ITEM", "Scrap Item Code", "Item Cost(Stocking Unit)", "Is_Insurance", "InsuranceNo", "InsuranceFromDate", "InsuranceToDate", "Marketing Seq No", "Alies Name2", "Alies Name3"}
        Else
            inputs = {"Item Code", "Item Description", "Seq No", "Short Description", "Structure Code", "Rack No", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Item Type", "Type", "Cost", "Tolerence", "Shelf Life", "Min Shelf Life", "Standard Purchase Price", "Morning", "Production Tolerance", "Chapter Code", "Category Structure", "Weight_UOM", "Weight_Value", "Is_MRP", "ITF_CODE", "Active", "Is_FreshItem", "Is_Ambient", "Product Type", "Is Purchaseable", "Is Allow QC on Purchase", "Is_CrateType", "Is_CAN_Type", "Used As", "Part_No", "Drawing_No", "Item_Cagetory_Values", "Is_Serial_Item", "Serial_Counter", "Is_Pick_Auto_SrNo", "Tax Exempted", "GL Account", "Account Description", "Warranty Code", "Warranty Applied From", "UOM1", "UOM2", "UOM3", "UOM4", "UOM5", "UOM6", "UOM7", "UOM8", "UOM9", "UOM10", "Conversion Factor1", "Conversion Factor2", "Conversion Factor3", "Conversion Factor4", "Conversion Factor5", "Conversion Factor6", "Conversion Factor7", "Conversion Factor8", "Conversion Factor9", "Conversion Factor10", "Default UOM1", "Default UOM2", "Default UOM3", "Default UOM4", "Default UOM5", "Default UOM6", "Default UOM7", "Default UOM8", "Default UOM9", "Default UOM10", "Stocking Unit1", "Stocking Unit2", "Stocking Unit3", "Stocking Unit4", "Stocking Unit5", "Stocking Unit6", "Stocking Unit7", "Stocking Unit8", "Stocking Unit9", "Stocking Unit10", "Weight1", "Weight2", "Weight3", "Weight4", "Weight5", "Weight6", "Weight7", "Weight8", "Weight9", "Weight10", "Gross_Weight1", "Gross_Weight2", "Gross_Weight3", "Gross_Weight4", "Gross_Weight5", "Gross_Weight6", "Gross_Weight7", "Gross_Weight8", "Gross_Weight9", "Gross_Weight10", "Item Category Code1", "Item Category Code Description1", "Item Category Code2", "Item Category Code Description2", "Item Category Code3", "Item Category Code Description3", "Item Category Code4", "Item Category Code Description4", "Item Category Code5", "Item Category Code Description5", "Item Category Code6", "Item Category Code Description6", "Item Category Code7", "Item Category Code Description7", "Item Category Code8", "Item Category Code Description8", "Item Category Code9", "Item Category Code Description9", "Item Category Code10", "Item Category Code Description10", "Item Category Code11", "Item Category Code Description11", "Item Category Code12", "Item Category Code Description12", "Item Category Code13", "Item Category Code Description13", "Item Category Code14", "Item Category Code Description14", "Item Category Code15", "Item Category Code Description15", "Item Category Level1", "Item Category Level Desp1", "Item Category Level2", "Item Category Level Desp2", "Item Category Level3", "Item Category Level Desp3", "Item Category Level4", "Item Category Level Desp4", "Item Category Level5", "Item Category Level Desp5", "Item Category Level6", "Item Category Level Desp6", "Item Category Level7", "Item Category Level Desp7", "Item Category Level8", "Item Category Level Desp8", "Item Category Level9", "Item Category Level Desp9", "Item Category Level10", "Item Category Level Desp10", "Item Category Level11", "Item Category Level Desp11", "Item Category Level12", "Item Category Level Desp12", "Item Category Level13", "Item Category Level Desp13", "Item Category Level14", "Item Category Level Desp14", "Item Category Level15", "Item Category Level Desp15", "Is_Rate_Change_OnDairyDispatch", "Is_Scheme_Item", "Distributor_Commission", "CNF_Commission", "CSA_Type", "IsTaxable", "is_Batch_Item", "AllowSRNWithoutShortReject", "Chilled Freezen", "IS SCRAP ITEM", "Scrap Item Code", "Item Cost(Stocking Unit)", "Is_Insurance", "InsuranceNo", "InsuranceFromDate", "InsuranceToDate", "Marketing Seq No", "Alies Name2", "Alies Name3"}
        End If



        If transportSql.importExcel(gv1, Strs.ToArray()) Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction
            Dim gv2 As New RadGridView

            Dim dtt As DataTable = TryCast(gv1.DataSource, DataTable)

            dtt.Columns.Add("ErrorDesc", "".GetType())

            'clsCommon.ProgressBarShow()
            clsCommon.ProgressBarPercentShow()
            Try
                Dim Item_Code1 As String = ""
                trans = clsDBFuncationality.GetTransactin()
                Try
                    Dim Item_Code As String = ""
                    Dim Item_Desc As String
                    Dim Item_Short_Desc As String
                    Dim Alies_Name As String
                    Dim Alies_Name2 As String
                    Dim Alies_Name3 As String
                    Dim Is_Leakage_Not_Applicable As Integer
                    Dim Structure_Code As String
                    Dim Rack_No As String
                    Dim Structure_Desc As String
                    Dim Purchase_Class_Code As String
                    Dim Sale_Class_Code As String
                    Dim item_category As String
                    Dim Sub_item_category As String
                    Dim Unit_Code As String = Nothing
                    Dim Item_Type As String = ""
                    Dim TypeOfItm As String
                    Dim Cost As Double
                    Dim Tolerence As Double
                    Dim Morning As String
                    Dim Is_PurchaseAble As String
                    Dim Is_Allow_QC As String
                    Dim Chapter_Code As String
                    Dim Weight_UOM As String
                    Dim wt As String = ""
                    Dim cnvrsnfctr As String = ""
                    Dim stckunit As String = ""
                    Dim Product_Type As String
                    Dim part_no As String = Nothing
                    Dim shelflife As String = Nothing
                    Dim Minshelflife As String = Nothing
                    Dim drawing_no As String = Nothing
                    'Dim CSA_Type As String
                    Dim GL_Account As String = Nothing
                    Dim jj As Integer = -1
                    Dim ErrCount As Integer = 0
                    Dim HSNCode As String = ""
                    Dim HSNDescription As String = ""
                    Dim strChilledFreezen As String = ""
                    Dim Is_Insurance As Integer = 0
                    Dim InsuranceNo As String = ""
                    Dim InsuranceFromDate As String
                    Dim InsuranceToDate As String

                    Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)
                        jj = jj + 1
                        clsCommon.ProgressBarPercentUpdate(((jj + 1) * 100) / dtt.Rows.Count, " Inserting/Updating Records " & (jj + 1) & " Of Total " & dtt.Rows.Count & " Records , Error Found :" & ErrCount)
                        If (clsCommon.myLen(grow.Cells("Item Code").Value) > 0 And clsCommon.myLen(grow.Cells("Item Code").Value) <= 50) OrElse clsCommon.myLen(grow.Cells("Item Description").Value) > 0 Then
                            Dim coll As New Hashtable()
                            '-------------------------Item Type---------------------------
                            Item_Type = clsCommon.myCstr(grow.Cells("Item Type").Value)
                            If Not (clsCommon.CompairString(Item_Type, "R") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "O") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "A") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "S") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "T") = CompairStringResult.Equal Or clsCommon.CompairString(Item_Type, "J") = CompairStringResult.Equal) Then
                                Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ITEM_TYPE_CODE from TSPL_ITEM_TYPE_MASTER where ITEM_TYPE_CODE='" + Item_Type + "'", trans))
                                If clsCommon.myLen(Item_Type) <= 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Item Type can not be other than ('R' or 'O' or 'A' or 'F' or 'S' or 'T' or 'J')"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "Item_Type", Item_Type)
                            '---------------------------------------------------------------

                            '-----------------Item Code-------------------------
                            Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                            Dim isExistItem As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'", trans))
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, trans)) = 1 AndAlso isExistItem = False Then
                                Dim isPrefixExit As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("SELECT Count( PREFIX ) FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE = '" + clsCommon.myCstr(Item_Type) + "' and PREFIX is not null and PREFIX <> ''", trans))
                                If isPrefixExit = False Then
                                    dtt.Rows(jj)("ErrorDesc") = "Please Enter Prefix of Item Type (" + Item_Type + ") in Inventory Setting screen."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                Dim Qry1 As String = "SELECT PREFIX FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry1, trans))

                                Dim Qry2 As String = "update TSPL_ITEM_TYPE_MASTER set PREFIX='" + clsCommon.incval(Item_Code) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                clsDBFuncationality.ExecuteNonQuery(Qry2, trans)
                            End If
                            If clsCommon.myLen(Item_Code) <= 0 Then
                                dtt.Rows(jj)("ErrorDesc") = "Item Code not found to save"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Code)
                            Item_Desc = clsCommon.myCstr(grow.Cells("Item Description").Value)
                            If clsCommon.myLen(Item_Desc) > 100 Then
                                dtt.Rows(jj)("ErrorDesc") = "Length of item description  is greater than 100."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Item_Desc", Item_Desc)
                            Item_Short_Desc = clsCommon.myCstr(grow.Cells("Short Description").Value)
                            If clsCommon.myLen(Item_Short_Desc) > 200 Then
                                dtt.Rows(jj)("ErrorDesc") = "Length of Item Short description  is greater than 200."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            Alies_Name = clsCommon.myCstr(grow.Cells("Alies Name").Value)
                            If clsCommon.myLen(Alies_Name) > 200 Then

                                dtt.Rows(jj)("ErrorDesc") = "Length of Alies Name  is greater than 200."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Alies_Name", Alies_Name)

                            Alies_Name2 = clsCommon.myCstr(grow.Cells("Alies Name2").Value)
                            If clsCommon.myLen(Alies_Name2) > 200 Then

                                dtt.Rows(jj)("ErrorDesc") = "Length of Alies Name2  is greater than 200."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Alies_Name2", Alies_Name2)
                            Alies_Name3 = clsCommon.myCstr(grow.Cells("Alies Name3").Value)
                            If clsCommon.myLen(Alies_Name3) > 200 Then

                                dtt.Rows(jj)("ErrorDesc") = "Length of Alies Name3  is greater than 200."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Alies_Name3", Alies_Name3)

                            If clsCommon.myLen(grow.Cells("Leakage Not Applicable").Value) > 0 Then
                                If clsCommon.CompairString(grow.Cells("Leakage Not Applicable").Value, "1") = CompairStringResult.Equal Then
                                    Is_Leakage_Not_Applicable = "1"
                                ElseIf clsCommon.CompairString(grow.Cells("Leakage Not Applicable").Value, "0") = CompairStringResult.Equal Then
                                    Is_Leakage_Not_Applicable = "0"
                                Else
                                    Throw New Exception("Enter [Leakage Not Applicable] As '1' Or '0' Or left blank.")
                                End If
                            Else
                                Is_Leakage_Not_Applicable = "0"
                            End If
                            clsCommon.AddColumnsForChange(coll, "Is_Leakage_Not_Applicable", Is_Leakage_Not_Applicable)
                            Dim reccount As Integer = 0
                            Dim seqNo As Int64 = 0
                            seqNo = clsCommon.myCdbl(grow.Cells("Seq No").Value)
                            If clsCommon.myCdbl(txtSeqNo.Text) > 0 Then
                                reccount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where sku_seq=" & seqNo & " and   item_code<>'" & Item_Code & "'", trans))
                                If reccount > 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Duplicate Sequence No"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "sku_seq", seqNo)

                            '''''''''''''''''
                            Dim Marreccount As Integer = 0
                            Dim MarseqNo As Int64 = 0
                            MarseqNo = clsCommon.myCdbl(grow.Cells("Marketing Seq No").Value)
                            If clsCommon.myCdbl(MarseqNo) > 0 Then
                                Marreccount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where Marketing_Seq=" & MarseqNo & " and   item_code<>'" & Item_Code & "'", trans))
                                If Marreccount > 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Duplicate Marketing Sequence No"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "Marketing_Seq", MarseqNo)
                            ''''''''''''''''

                            If clsCommon.myCdbl(grow.Cells("Production Tolerance").Value) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "Production_Tolerance", clsCommon.myCdbl(grow.Cells("Production Tolerance").Value))
                            End If
                            Dim Is_Fresh As String = clsCommon.myCstr(grow.Cells("Is_FreshItem").Value)
                            If clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                                If clsCommon.myLen(Item_Short_Desc) <= 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Short description can not be left blank"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                Else
                                    Dim ShortDesp As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row from tspl_item_master Where Short_Description ='" & clsCommon.myCstr(Item_Short_Desc.Trim()) & "'  AND Item_Code <>'" & clsCommon.myCstr(Item_Code) & "'", trans))
                                    If ShortDesp > 0 Then
                                        dtt.Rows(jj)("ErrorDesc") = "Please check ! short description should not be duplicate"
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "Short_Description", Item_Short_Desc)
                            ''
                            '' Richa 13-July-2016
                            Dim Is_Ambient As String = clsCommon.myCstr(grow.Cells("Is_Ambient").Value)
                            If clsCommon.CompairString(Is_Ambient, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_Fresh, "1") = CompairStringResult.Equal Then
                                dtt.Rows(jj)("ErrorDesc") = "You cannot select Fresh item/Ambient at a time, Please select either fresh Item or Ambient on line"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If

                            If clsCommon.CompairString(Is_Ambient, "0") = CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_Fresh, "0") = CompairStringResult.Equal Then
                                dtt.Rows(jj)("ErrorDesc") = "Please select either fresh Item or Ambient on line"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If

                            ''
                            ''---Setting Based HSN Code GST
                            If AllowGSTApplicable = True Then
                                Dim IsTaxable As Integer = clsCommon.myCdbl(grow.Cells("IsTaxable").Value)
                                If IsTaxable > 0 Then
                                    HSNCode = clsCommon.myCstr(grow.Cells("HSN Code").Value)
                                    If clsCommon.myLen(HSNCode) <= 0 Then
                                        Throw New Exception("Please Fill HSN Code.")
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                    Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Code from tspl_HSN_master where code='" & HSNCode & "'", trans)
                                    If clsCommon.myLen(qry1) <= 0 Then
                                        Throw New Exception("HSN Code not match.")
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                End If
                                HSNCode = clsCommon.myCstr(grow.Cells("HSN Code").Value)

                            End If
                            ''----ENd

                            Is_Insurance = clsCommon.myCdbl(grow.Cells("Is_Insurance").Value)
                            If Is_Insurance = 1 Then
                                If clsCommon.myLen(grow.Cells("InsuranceNo").Value.ToString()) = 0 Then
                                    Throw New Exception("Insurance No cannot be blank at line '" + LineNo + "'")
                                End If

                                If clsCommon.myLen(grow.Cells("InsuranceFromDate").Value) = 0 Then
                                    Throw New Exception("Insurance From Date cannot be blank at line '" + LineNo + "'")
                                End If

                                If clsCommon.myLen(grow.Cells("InsuranceToDate").Value) = 0 Then
                                    Throw New Exception("Insurance To Date cannot be blank at line '" + LineNo + "'")
                                End If

                                If clsCommon.myCDate(grow.Cells("InsuranceFromDate").Value) > clsCommon.myCDate(grow.Cells("InsuranceToDate").Value) Then
                                    Throw New Exception("Insurance To date can not be before than from date at line '" + LineNo + "'")
                                End If

                                InsuranceNo = clsCommon.myCstr(grow.Cells("InsuranceNo").Value)
                                InsuranceFromDate = clsCommon.GetPrintDate(grow.Cells("InsuranceFromDate").Value, "dd/MMM/yyyy")
                                InsuranceToDate = clsCommon.GetPrintDate(grow.Cells("InsuranceToDate").Value, "dd/MMM/yyyy")
                            Else
                                InsuranceNo = Nothing
                                InsuranceFromDate = Nothing
                                InsuranceToDate = Nothing
                            End If
                            clsCommon.AddColumnsForChange(coll, "Is_Insurance", Is_Insurance)
                            clsCommon.AddColumnsForChange(coll, "InsuranceNo", InsuranceNo, True)
                            clsCommon.AddColumnsForChange(coll, "InsuranceFromDate", InsuranceFromDate, True)
                            clsCommon.AddColumnsForChange(coll, "InsuranceToDate", InsuranceToDate, True)


                            'Chilled_Freezen --------------------------------------------------------

                            If clsCommon.CompairString(Item_Type, "A") <> CompairStringResult.Equal Then
                                strChilledFreezen = clsCommon.myCstr(grow.Cells("Chilled Freezen").Value)
                                If Not (clsCommon.CompairString(strChilledFreezen, "1") = CompairStringResult.Equal Or clsCommon.CompairString(strChilledFreezen, "0") = CompairStringResult.Equal) Then
                                    Throw New Exception("Chilled Freezen can not be other than ('1' or '0') at line '" + LineNo + "'")
                                End If
                                clsCommon.AddColumnsForChange(coll, "Chilled_Freezen", strChilledFreezen)
                            End If

                            '---------------------------------------------------------------------------

                            '------------------Rack No------------------
                            Rack_No = clsCommon.myCstr(grow.Cells("Rack No").Value)
                            If clsCommon.myLen(Rack_No) > 30 Then
                                'Throw New Exception("Length of Rack No on line '" + LineNo + "' is greater than 30.")
                                dtt.Rows(jj)("ErrorDesc") = "Length of Rack No  is greater than 30."
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Rack_No", Rack_No)
                            '-----------------------------------------------------

                            Dim std_pur_rate As Decimal = clsCommon.myCdbl(grow.Cells("Standard Purchase Price").Value)

                            '----------------Structure Code-----------------------
                            Dim StructureCode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                            If StructureCode <> "" Then
                                Structure_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Code from TSPL_STRUCTURE_MASTER WHERE Structure_Code='" + StructureCode + "'", trans))
                                If clsCommon.CompairString(Structure_Code, StructureCode) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Structure_Code", Structure_Code)
                                Else
                                    '    Throw New Exception("Structure Code at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "Structure Code  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                'Throw New Exception("Structure Code can not be blank at line '" + LineNo + "'.")
                                dtt.Rows(jj)("ErrorDesc") = "Structure Code can not be blank "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            '-----------------------------------------------------

                            '----------------Structure Desc-------------------------
                            Structure_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Structure_Descq from TSPL_STRUCTURE_MASTER Where Structure_Code='" + Structure_Code + "'", trans))
                            clsCommon.AddColumnsForChange(coll, "Structure_Desc", Structure_Desc)
                            '---------------SCRAP ITEM------------------------------------------------------
                            Dim isScrapTypeItem As String = clsCommon.myCstr(grow.Cells("IS SCRAP ITEM").Value)
                            If clsCommon.myLen(isScrapTypeItem) <= 0 Then
                                isScrapTypeItem = "0"
                            End If
                            Dim ScrapItemCode As String = clsCommon.myCstr(grow.Cells("SCrap Item Code").Value)
                            If clsCommon.CompairString(isScrapTypeItem, "1") = CompairStringResult.Equal Then
                                If clsCommon.myLen(ScrapItemCode) > 0 Then
                                    If clsCommon.CompairString(ScrapItemCode, Item_Code) = CompairStringResult.Equal Then
                                        dtt.Rows(jj)("ErrorDesc") = "Item Code and Scrap Item Code can not be Same."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        'Throw New Exception("Item Code and Scrap Item Code can not be Same.")
                                    End If
                                    Dim isValidScrapItem As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count (*) from tspl_item_master  where tspl_item_master.Item_Code = '" + ScrapItemCode + "'", trans))
                                    If isValidScrapItem = False Then
                                        dtt.Rows(jj)("ErrorDesc") = "Invalid Item Code."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        'Throw New Exception("Invalid Item Code.")
                                    End If
                                    isValidScrapItem = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count ( Scrap_Item_Code) from tspl_item_master  where Scrap_Item_Code is not null and Scrap_Item_Code = '" + ScrapItemCode + "' and  Item_code not in ('" + Item_Code + "')", trans))
                                    If isValidScrapItem = True Then
                                        dtt.Rows(jj)("ErrorDesc") = "Item Code (" + ScrapItemCode + ") Already used as Scrap Item with another Item code."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        'Throw New Exception("Item Code (" + ScrapItemCode + ") Already used as Scrap Item with another Item code")
                                    End If
                                    isValidScrapItem = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" Select count(*) from tspl_item_master where Structure_Code = '" + Structure_Code + "' and Item_Code = '" + ScrapItemCode + "'", trans))
                                    If isValidScrapItem = False Then
                                        dtt.Rows(jj)("ErrorDesc") = "Scrap Item Code and Main Item code should be Same Scrature Code."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        'Throw New Exception(" Scrap Item Code and Main Item code should be Same Scrature Code.")
                                    End If
                                Else
                                    dtt.Rows(jj)("ErrorDesc") = "SCrap Item Code Can not be blank."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    'Throw New Exception("SCrap Item Code Can not be blank.")
                                End If
                            Else
                                ScrapItemCode = ""
                            End If
                            clsCommon.AddColumnsForChange(coll, "IS_SCRAP_ITEM", isScrapTypeItem)
                            If clsCommon.CompairString(isScrapTypeItem, "1") = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "SCrap_Item_Code", ScrapItemCode)
                            End If

                            '----------------------Purchase A/c Set---------------
                            Dim PurchaseAcSet As String = clsCommon.myCstr(grow.Cells("Purchase A/c Set").Value)
                            If PurchaseAcSet <> "" Then
                                Purchase_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from dbo.TSPL_PURCHASE_ACCOUNTS WHERE Purchase_Class_Code ='" + PurchaseAcSet + "'", trans))
                                If clsCommon.CompairString(Purchase_Class_Code, PurchaseAcSet) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", Purchase_Class_Code)
                                Else
                                    '    Throw New Exception("Purchase Acccount Set at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "Purchase Acccount Set  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                'Throw New Exception("Purchase A/c Set can not be blank at line '" + LineNo + "'.")
                                dtt.Rows(jj)("ErrorDesc") = "Purchase A/c Set can not be blank"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            '-----------------------------------------------------

                            '------------------------sale A/c Set------------------
                            Dim SaleAcSet As String = clsCommon.myCstr(grow.Cells("Sale A/c Set").Value)
                            If SaleAcSet <> "" Then
                                Sale_Class_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Code from TSPL_SALES_ACCOUNTS WHERE Sales_Class_Code ='" + SaleAcSet + "'", trans))
                                If clsCommon.CompairString(Sale_Class_Code, SaleAcSet) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", Sale_Class_Code)
                                Else
                                    'Throw New Exception("Sale Account Set at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "Sale Account Set  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                'Throw New Exception("Sale A/c Set can not be blank at line '" + LineNo + "'.")
                                dtt.Rows(jj)("ErrorDesc") = "Sale A/c Set can not be blank"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            '-------------------------------------------------------

                            '---------------------Item Category-----------------------
                            If Not RadPageView1.Pages(2).Enabled Then ''------------------------------BM00000002280 Monika
                                Dim ItemCategory As String = clsCommon.myCstr(grow.Cells("Category").Value)
                                If ItemCategory <> "" Then
                                    item_category = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select category_code from tspl_Item_category WHERE category_code ='" + ItemCategory + "'", trans))
                                    If clsCommon.CompairString(item_category, ItemCategory) = CompairStringResult.Equal Then
                                        clsCommon.AddColumnsForChange(coll, "item_category", item_category)
                                    Else
                                        '        Throw New Exception("Item Category at line '" + LineNo + "' does not exist.")
                                        dtt.Rows(jj)("ErrorDesc") = "Item Category  does not exist."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                Else
                                    'Throw New Exception("Item Category can not be blank at line '" + LineNo + "'.")
                                    dtt.Rows(jj)("ErrorDesc") = "Item Category can not be blank"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                '------------------------------------------------------------

                                '-----------------Item Sub Category--------------------------
                                Dim ItemSubCategory As String = clsCommon.myCstr(grow.Cells("Sub Category").Value)
                                If ItemSubCategory <> "" Then
                                    Sub_item_category = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_Category_Code from TSPL_ITEM_SUB_CATEGORY WHERE Sub_Category_Code ='" + ItemSubCategory + "'", trans))
                                    If clsCommon.CompairString(Sub_item_category, ItemSubCategory) = CompairStringResult.Equal Then
                                        clsCommon.AddColumnsForChange(coll, "Sub_item_category", Sub_item_category)
                                    Else
                                        'Throw New Exception("Item Sub Category at line '" + LineNo + "' does not exist.")
                                        dtt.Rows(jj)("ErrorDesc") = "Item Sub Category  does not exist."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                Else
                                    'Throw New Exception("Item Sub Category can not be blank at line '" + LineNo + "'.")
                                    dtt.Rows(jj)("ErrorDesc") = "Item Sub Category can not be blank"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                                '-------------------------------------------------------------
                            End If
                            '------------------------End------BM00000002280 Monika


                            '---------------------Unit_Code-------------------------------
                            'commented by priti no need of this column.this column will be automatically updated by stocking unit
                            Dim Uom As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                            If Uom <> "" Then
                                Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master WHERE unit_code ='" + Uom + "'", trans))
                                If clsCommon.CompairString(Unit_Code, Uom) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Unit_Code", Unit_Code)
                                Else
                                    'Throw New Exception("UOM Code at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "UOM Code  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                'Throw New Exception("UOM can not be blank at line '" + LineNo + "'.")
                                dtt.Rows(jj)("ErrorDesc") = "UOM can not be blank"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            '--------------------------------------------------------------



                            '-------------------------type-----------------------------------
                            TypeOfItm = clsCommon.myCstr(grow.Cells("Type").Value)
                            If Not (clsCommon.CompairString(TypeOfItm, "A") = CompairStringResult.Equal Or clsCommon.CompairString(TypeOfItm, "B") = CompairStringResult.Equal Or clsCommon.CompairString(TypeOfItm, "C") = CompairStringResult.Equal) Then
                                'Throw New Exception("Type can not be other than ('A' or 'B' or 'C') at line '" + LineNo + "'")
                                dtt.Rows(jj)("ErrorDesc") = "Type can not be other than ('A' or 'B' or 'C')"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "TypeOfItm", TypeOfItm)
                            '----------------------------------------------------------------

                            '-------------------------Cost--------------------------------
                            Cost = clsCommon.myCdbl(grow.Cells("Cost").Value)
                            clsCommon.AddColumnsForChange(coll, "Cost", Cost)
                            '---------------------------------------------------------------
                            Tolerence = clsCommon.myCdbl(grow.Cells("Tolerence").Value)

                            If ToleranceMandatoryFor_RM_Other_Trade AndAlso Tolerence = 0 AndAlso (clsCommon.CompairString(Item_Type, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(Item_Type, "O") = CompairStringResult.Equal OrElse clsCommon.CompairString(Item_Type, "T") = CompairStringResult.Equal) Then
                                dtt.Rows(jj)("ErrorDesc") = "Fill tolerance"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If

                            clsCommon.AddColumnsForChange(coll, "Tolerence", Tolerence)
                            '-------------------------Morining--------------------------------
                            '-----------added by stuti----------------------------------------------------


                            If clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal And ShelfLifeManadatoryOnFG Then ''by stuti on 15/11/2016
                                shelflife = clsCommon.myCstr(grow.Cells("Shelf Life").Value)
                                If clsCommon.myLen(shelflife) <= 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Fill Shelf life for finished good"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If

                            clsCommon.AddColumnsForChange(coll, "tech_shelf_life", shelflife)
                            '---------------------------------------------------------
                            '====================Added by preeti Gupta against ticket no[BHA/03/07/19-000920]=============
                            Minshelflife = clsCommon.myCstr(grow.Cells("Min Shelf Life").Value)
                            clsCommon.AddColumnsForChange(coll, "min_shelf_life", Minshelflife)
                            '======================================================
                            Morning = clsCommon.myCstr(grow.Cells("Morning").Value)
                            If Not (clsCommon.CompairString(Morning, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Morning, "0") = CompairStringResult.Equal) Then
                                'Throw New Exception("Morning can not be other than ('1' or '0') at line '" + LineNo + "'")
                                dtt.Rows(jj)("ErrorDesc") = "Morning can not be other than ('1' or '0')"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Morning", Morning)
                            '---------------------------------------------------------------

                            '-------------------------Is Purchaseable--------------------------------
                            Is_PurchaseAble = IIf(LCase(clsCommon.myCstr(grow.Cells("Is Purchaseable").Value)) = "yes", "1", "0")
                            If Not (clsCommon.CompairString(Is_PurchaseAble, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Is_PurchaseAble, "0") = CompairStringResult.Equal) Then
                                'Throw New Exception("[Is Purchaseable] can not be other than ('1' or '0') at line '" + LineNo + "'")
                                dtt.Rows(jj)("ErrorDesc") = "[Is Purchaseable] can not be other than ('1' or '0')"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Is_Purchaseable", Is_PurchaseAble)
                            '---------------------------------------------------------------

                            '-------------------------Is Allow QC--------------------------------
                            Is_Allow_QC = IIf(LCase(clsCommon.myCstr(grow.Cells("Is Allow QC on Purchase").Value)) = "yes", "1", "0")
                            If Not (clsCommon.CompairString(Is_PurchaseAble, "1") = CompairStringResult.Equal Or clsCommon.CompairString(Is_PurchaseAble, "0") = CompairStringResult.Equal) Then
                                'Throw New Exception("[Is Allow QC on Purchase] can not be other than ('1' or '0') at line '" + LineNo + "'")
                                dtt.Rows(jj)("ErrorDesc") = "[Is Allow QC on Purchase] can not be other than ('1' or '0')"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            clsCommon.AddColumnsForChange(coll, "Is_AllowQC_on_Purchase", Is_Allow_QC)
                            '---------------------------------------------------------------

                            '---------------------Chapter_code-------------------------------
                            Dim Chapter As String = clsCommon.myCstr(grow.Cells("Chapter Code").Value)
                            If Chapter <> "" Then
                                Chapter_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Chapter_Head_Code from TSPL_CHAPTER_HEAD  WHERE Chapter_Head_Code ='" + Chapter + "'", trans))
                                If clsCommon.CompairString(Chapter, Chapter_Code) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", Chapter)
                                Else
                                    'Throw New Exception("Chapter Code Code at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "Chapter Code Code  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If
                            '--------------------------------------------------------------

                            '----------------Weight UOM-----------------------
                            Dim WeightUOM As String = clsCommon.myCstr(grow.Cells("Weight_UOM").Value)
                            If StructureCode <> "" Then
                                Weight_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER WHERE Weight_Type='Y' AND Unit_Code ='" + WeightUOM + "'", trans))
                                If clsCommon.CompairString(Weight_UOM, WeightUOM) = CompairStringResult.Equal Then
                                    clsCommon.AddColumnsForChange(coll, "Weight_UOM", Weight_UOM)
                                Else
                                    'Throw New Exception("Weight_UOM at line '" + LineNo + "' does not exist.")
                                    dtt.Rows(jj)("ErrorDesc") = "Weight_UOM  does not exist."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If

                            'wt = clsCommon.myCstr(grow.Cells("Weight").Value)
                            'cnvrsnfctr = clsCommon.myCstr(grow.Cells("Conversion_Factor").Value)
                            'stckunit = clsCommon.myCstr(grow.Cells("Stocking_unit").Value)

                            'If clsCommon.myLen(wt) <= 0 Then
                            '    wt = "0.00"
                            'End If
                            'If clsCommon.myLen(cnvrsnfctr) <= 0 Then
                            '    cnvrsnfctr = "1"
                            'End If
                            'If clsCommon.myLen(stckunit) <= 0 Then
                            '    stckunit = "Y"
                            'End If
                            If clsCommon.CompairString(clsCommon.myCdbl(grow.Cells("Is_CrateType").Value), "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCdbl(grow.Cells("Is_CAN_Type").Value), "1") = CompairStringResult.Equal Then
                                Throw New Exception("Item Type can be CRATE type or CAN type")
                            End If
                            '' Anubhooti 11-Sep-2014 BM00000003891
                            Dim Is_CrateType As String = "N"
                            If clsCommon.myLen(grow.Cells("Is_CrateType").Value) > 0 Then
                                If clsCommon.CompairString(grow.Cells("Is_CrateType").Value, "1") = CompairStringResult.Equal Then
                                    Is_CrateType = "1"
                                ElseIf clsCommon.CompairString(grow.Cells("Is_CrateType").Value, "0") = CompairStringResult.Equal Then
                                    Is_CrateType = "0"
                                Else
                                    'Throw New Exception("Enter Crate Type As '1' Or '0' Or left blank at line '" + LineNo + "'.")
                                    dtt.Rows(jj)("ErrorDesc") = "Enter Crate Type As '1' Or '0' Or left blank"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                Is_CrateType = "0"
                            End If


                            '===============Added by preeti gupta==============
                            Dim Is_CAN_Type As String = Nothing
                            If clsCommon.myLen(grow.Cells("Is_CAN_Type").Value) > 0 Then
                                If clsCommon.CompairString(grow.Cells("Is_CAN_Type").Value, "1") = CompairStringResult.Equal Then
                                    Is_CAN_Type = "1"
                                ElseIf clsCommon.CompairString(grow.Cells("Is_CAN_Type").Value, "0") = CompairStringResult.Equal Then
                                    Is_CAN_Type = "0"
                                Else
                                    Throw New Exception("Enter CAN Type As '1' Or '0' Or left blank.")
                                End If
                            Else
                                Is_CAN_Type = "0"
                            End If
                            '============================================================

                            ''

                            part_no = clsCommon.myCstr(grow.Cells("part_no").Value)
                            drawing_no = clsCommon.myCstr(grow.Cells("drawing_no").Value)
                            If clsCommon.myLen(part_no) > 100 Then
                                part_no = part_no.Substring(0, 100)
                            End If
                            If clsCommon.myLen(drawing_no) > 100 Then
                                drawing_no = drawing_no.Substring(0, 100)
                            End If

                            '==============Used as ===============
                            Dim Used_as As String = clsCommon.myCstr(grow.Cells("Used As").Value)
                            '' Anubhooti 14-Jan-2014 (Check/Valdiations of used as)
                            If clsCommon.myLen(Used_as) > 0 Then
                                If clsCommon.CompairString(Used_as, "None") = CompairStringResult.Equal Then
                                    Used_as = ""
                                ElseIf clsCommon.CompairString(Used_as, "MCC Issue") = CompairStringResult.Equal Then
                                    Used_as = "I"
                                ElseIf clsCommon.CompairString(Used_as, "MCC Sale") = CompairStringResult.Equal Then
                                    Used_as = "S"
                                Else
                                    'Throw New Exception("Entered Used As should be amoung 'None','MCC Issue','MCC Sale' at line '" + LineNo + "'.")
                                    dtt.Rows(jj)("ErrorDesc") = "Entered Used As should be amoung 'None','MCC Issue','MCC Sale'"
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            Else
                                'Throw New Exception("Please fill used as from amoung 'None','MCC Issue','MCC Sale' at line '" + LineNo + "'.")
                                dtt.Rows(jj)("ErrorDesc") = "Please fill used as from amoung 'None','MCC Issue','MCC Sale'"
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            '------------------GL Account------------------
                            If CreateGLAccToItem = True AndAlso fndGLAcc.Visible = True Then
                                GL_Account = clsCommon.myCstr(grow.Cells("GL Account").Value)
                                If clsCommon.myLen(GL_Account) > 0 Then
                                    Dim GLqry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(GLqry, trans)
                                    If check <= 0 Then
                                        '        Throw New Exception("Filled GL account (" & GL_Account & ") does not exist" + Environment.NewLine + ".First make its entry at line '" + LineNo + "'.")
                                        dtt.Rows(jj)("ErrorDesc") = "Filled GL account (" & GL_Account & ") does not exist" + Environment.NewLine + ".First make its entry"
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                    'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account + "' AND ControlAccount ='Y'"
                                    'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                                    'If check1 <= 0 Then
                                    '    Throw New Exception("Filled GL account (" & GL_Account & ") must be control account at line '" + LineNo + "'.")
                                    'End If
                                    'Else
                                    '    Throw New Exception("Please fill GL account at line '" + LineNo + "'.")
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "GL_Account", GL_Account, True)
                            '-----------------------------------------------------
                            clsCommon.AddColumnsForChange(coll, "Item_Used_As", Used_as)
                            '================================

                            '-------------------------Product Type---------------------------
                            Product_Type = clsCommon.myCstr(grow.Cells("Product Type").Value)
                            '' check for product type                           
                            Dim Initial_Product_Type As String = clsItemMaster.GetItemProductType(Item_Code, trans)
                            Dim Qry As String = ""
                            If clsCommon.CompairString(Product_Type, Initial_Product_Type) <> CompairStringResult.Equal Then
                                If clsCommon.CompairString(Initial_Product_Type, "MI") = CompairStringResult.Equal Then
                                    Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & Item_Code & "'"
                                    Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
                                    If totalCount > 0 Then
                                        Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & " in Product Type Milk.")
                                    End If
                                End If
                                If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                    Qry = "select Count(*) from TSPL_INVENTORY_MOVEMENT where Item_Code='" & Item_Code & "'"
                                    Dim totalCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans))
                                    If totalCount > 0 Then
                                        Throw New Exception("Product Type of this item can not be changed because some transactions are already done for item " & Item_Code & " at Line No " & LineNo & ".")
                                    End If
                                End If
                            End If


                            If clsCommon.myLen(Product_Type) > 0 AndAlso clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MB") <> CompairStringResult.Equal _
                                AndAlso clsCommon.CompairString(Product_Type, "CH") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "CU") <> CompairStringResult.Equal _
                                AndAlso clsCommon.CompairString(Product_Type, "CA") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "DG") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "BU") <> CompairStringResult.Equal _
                                AndAlso clsCommon.CompairString(Product_Type, "BM") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "PS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MP") <> CompairStringResult.Equal Then
                                Throw New Exception("Product Type should be MI(Milk),MB(Melted Butter),CH(Cheese),CU(Curd),CA(Cream),DG(Desi-Ghee),BU(Butter),BM(Butter Milk),PS(Paper Seal),MS(Manual Seal) and MP(Milk Product) for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If


                            ''=========================================================================================
                            If clsCommon.myLen(grow.Cells("is_Batch_Item").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("is_Batch_Item").Value), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("is_Batch_Item").Value), "1") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill 0 or 1 in is_Batch_Item for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If

                            Qry = "Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'"
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then ''when new entry
                                If AllowFinishGoodAsBatchItem AndAlso clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal AndAlso clsCommon.myLen(grow.Cells("is_Batch_Item").Value) <= 0 Then
                                    grow.Cells("is_Batch_Item").Value = 1
                                End If
                            End If

                            If clsCommon.myLen(grow.Cells("Is_Scheme_Item").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is_Scheme_Item").Value), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is_Scheme_Item").Value), "1") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill 0 or 1 in Is_Scheme_Item for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If
                            If grow.Cells("Distributor_Commission").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Distributor_Commission").Value) > 0 AndAlso Not IsNumeric(grow.Cells("Distributor_Commission").Value) Then
                                Throw New Exception("Fill numeric value in Distributor_Commission for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If
                            If grow.Cells("CNF_Commission").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("CNF_Commission").Value) > 0 AndAlso Not IsNumeric(grow.Cells("CNF_Commission").Value) Then
                                Throw New Exception("Fill numeric value in CNF_Commission for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If
                            If clsCommon.myLen(grow.Cells("IsTaxable").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsTaxable").Value), "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsTaxable").Value), "1") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill 0 or 1 in IsTaxable for item " & Item_Code & " at Line No " & LineNo & ".")
                            End If
                            'If clsCommon.myLen(grow.Cells("CSA_Type").Value) > 0 AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "BULK -DESI GHEE") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "BULK-DESI GHEE") <> CompairStringResult.Equal _
                            '   AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "BULK-OTHER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "BUTTER") <> CompairStringResult.Equal _
                            '   AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "CPD-DESI GHEE") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "CPD-OTHER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "CREAMER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "FRESH") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "MILK") <> CompairStringResult.Equal _
                            '   AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "None") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "SMP") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "WHITENER") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(grow.Cells("CSA_Type").Value, "WMP") <> CompairStringResult.Equal Then
                            '    Throw New Exception("CSA Type should be BULK -DESI GHEE,BULK-DESI GHEE,BULK-OTHER,BUTTER,CPD-DESI GHEE,CPD-OTHER,CREAMER,FRESH,MILK,None,SMP,WHITENER,WMP for item " & Item_Code & " at Line No " & LineNo & ".")
                            'End If
                            ''=========================================================================================

                            '' Advantek Warranty Applied From 22-Sep-2015
                            Dim Warranty_Code As String = String.Empty
                            Dim Warranty_Applied_From As String = String.Empty
                            Dim WarrantyCodeDB As Double = 0
                            Warranty_Code = clsCommon.myCstr(grow.Cells("Warranty Code").Value)
                            Warranty_Applied_From = clsCommon.myCstr(grow.Cells("Warranty Applied From").Value)

                            If clsCommon.myLen(Warranty_Code) > 0 Then
                                WarrantyCodeDB = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_WARRANTY_MASTER WHERE Code ='" & Warranty_Code & "'", trans))
                                If WarrantyCodeDB <= 0 Then
                                    Throw New Exception("Please check ! Warranty Code (" & Warranty_Code & ") does not exists in master at Line No " & LineNo & ".")
                                End If
                            End If
                            clsCommon.AddColumnsForChange(coll, "WARRANTY_CODE", Warranty_Code, True)

                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, trans)), "A") = CompairStringResult.Equal Then
                                If clsCommon.myLen(Warranty_Code) > 0 Then
                                    If clsCommon.CompairString(Warranty_Applied_From, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(Warranty_Applied_From, "M") = CompairStringResult.Equal Then
                                    Else
                                        Throw New Exception("Please check ! Warranty Applied From (" & Warranty_Applied_From & ") must be 'S' or 'M' at Line No " & LineNo & ".")
                                    End If
                                Else
                                    Warranty_Applied_From = ""
                                End If
                            Else
                                Warranty_Applied_From = ""
                            End If


                            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowItemCostMandatoryForStockingUnit, clsFixedParameterCode.AllowItemCostMandatoryForStockingUnit, trans)) = 1, True, False) = True Then
                                If clsCommon.myCdbl(grow.Cells("Item Cost(Stocking Unit)").Value) <= 0 Then
                                    dtt.Rows(jj)("ErrorDesc") = "Item Cost Can not be blank Or Zero for Stocking Unit [Yes] in Item Code " + Item_Code + "."
                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                End If
                            End If

                            clsCommon.AddColumnsForChange(coll, "Warranty_Applied_From", Warranty_Applied_From.ToUpper(), True)
                            ''

                            clsCommon.AddColumnsForChange(coll, "Product_Type", Product_Type)

                            clsCommon.AddColumnsForChange(coll, "Part_No", part_no)
                            clsCommon.AddColumnsForChange(coll, "Drawing_No", drawing_no)

                            'CSA_Type = clsCommon.myCstr(grow.Cells("CSA Type").Value)
                            'clsCommon.AddColumnsForChange(coll, "CSA_Type", CSA_Type)
                            '---------------------------------------------------------------
                            Dim Weight_Value As Double = clsCommon.myCdbl(grow.Cells("Weight_Value").Value)
                            clsCommon.AddColumnsForChange(coll, "Weight_Value", clsCommon.myCdbl(grow.Cells("Weight_Value").Value))
                            '-----------------------------------------------------
                            clsCommon.AddColumnsForChange(coll, "Is_Tax_Exempted", IIf(clsCommon.myCdbl(grow.Cells("Tax Exempted").Value) > 0, 1, 0))

                            clsCommon.AddColumnsForChange(coll, "Is_MRP", clsCommon.myCdbl(grow.Cells("Is_MRP").Value))
                            clsCommon.AddColumnsForChange(coll, "ITF_CODE", clsCommon.myCdbl(grow.Cells("ITF_CODE").Value))
                            clsCommon.AddColumnsForChange(coll, "Is_FreshItem", clsCommon.myCdbl(grow.Cells("Is_FreshItem").Value))
                            clsCommon.AddColumnsForChange(coll, "Is_Ambient", clsCommon.myCdbl(grow.Cells("Is_Ambient").Value))
                            clsCommon.AddColumnsForChange(coll, "Is_Rate_Change_OnDairyDispatch", clsCommon.myCdbl(grow.Cells("Is_Rate_Change_OnDairyDispatch").Value))
                            clsCommon.AddColumnsForChange(coll, "AllowSRNWithoutShortReject", clsCommon.myCdbl(grow.Cells("AllowSRNWithoutShortReject").Value))
                            clsCommon.AddColumnsForChange(coll, "Active", clsCommon.myCdbl(grow.Cells("Active").Value))
                            clsCommon.AddColumnsForChange(coll, "Is_CrateType", clsCommon.myCdbl(grow.Cells("Is_CrateType").Value))
                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "Purchase_Price", std_pur_rate)
                            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Modify_Date", Datee)
                            clsCommon.AddColumnsForChange(coll, "is_batch_item", clsCommon.myCdbl(grow.Cells("is_batch_item").Value))

                            clsCommon.AddColumnsForChange(coll, "Is_Scheme_Item", clsCommon.myCdbl(grow.Cells("Is_Scheme_Item").Value))
                            clsCommon.AddColumnsForChange(coll, "Distributor_Commission", clsCommon.myCdbl(grow.Cells("Distributor_Commission").Value))
                            clsCommon.AddColumnsForChange(coll, "CNF_Commission", clsCommon.myCdbl(grow.Cells("CNF_Commission").Value))
                            clsCommon.AddColumnsForChange(coll, "IsTaxable", clsCommon.myCdbl(grow.Cells("IsTaxable").Value))
                            clsCommon.AddColumnsForChange(coll, "CSA_TYpe", clsCommon.myCstr(grow.Cells("CSA_TYpe").Value))
                            clsCommon.AddColumnsForChange(coll, "Is_CAN_Type", clsCommon.myCdbl(grow.Cells("Is_CAN_Type").Value))
                            If AllowGSTApplicable = True Then
                                clsCommon.AddColumnsForChange(coll, "HSN_Code", clsCommon.myCstr(grow.Cells("HSN Code").Value))
                            End If

                            Qry = "Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'"
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 0 Then
                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                clsCommon.AddColumnsForChange(coll, "Created_Date", Datee)
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "TSPL_ITEM_MASTER.Item_Code='" + Item_Code + "'", trans)
                                Qry = "delete from TSPL_ITEM_UOM_DETAIL where Item_Code='" + Item_Code + "'"
                                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                Qry = "delete from TSPL_ITEM_MASTER_CATEGORY where Item_Code='" + Item_Code + "'"
                                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                            End If
                            ''===================changes by shivani against ticket [BM00000008694]
                            'clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_ITEM_UOM_DETAIL WHere Item_Code='" + Item_Code + "'", trans)
                            Dim UOMCount As Integer = 0
                            Dim DefaultUOMCount As Integer = 0
                            Dim IsDefaultUOM As Integer = 0
                            Dim uom_GrossWt As Double = 0
                            Dim strStockingUnit As String = ""
                            Dim strProductionFATSNF_KG_Unit As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNF_KG_Unit, clsFixedParameterCode.ProductionFATSNF_KG_Unit, trans))
                            Dim isUomKGExist As Boolean = False
                            Dim dblItemCost = clsCommon.myCdbl(grow.Cells("Item Cost(Stocking Unit)").Value)
                            For j As Integer = 1 To 10

                                Dim colUOM As New Hashtable()
                                Dim strUOM As String
                                Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM" & clsCommon.myCstr(j) & "").Value)
                                If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                                    If clsCommon.CompairString(UnitCode, strProductionFATSNF_KG_Unit) = CompairStringResult.Equal Then
                                        isUomKGExist = True
                                    End If
                                End If
                                If j = 1 Then
                                    strStockingUnit = clsCommon.myCstr(grow.Cells("UOM" & clsCommon.myCstr(j) & "").Value)
                                    Qry = "update TSPL_ITEM_MASTER set Unit_Code='" & strStockingUnit & "'  where Item_Code='" & Item_Code & "'"
                                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                End If
                                If clsCommon.myLen(UnitCode) > 0 Then
                                    UOMCount = +1
                                    strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                                    If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                                    Else
                                        'Throw New Exception("The UOM '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                                        dtt.Rows(jj)("ErrorDesc") = "The UOM " + UnitCode + " Does Not Exist"
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                    '==============================================ADDED BY SHIVANI================================================= correction is done by richa
                                    Dim isCustomConversion As Boolean = False
                                    Dim FirstStockUnit As String = clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(j) & "").Value)
                                    For i As Integer = j + 1 To 10
                                        Dim SecondStockUnit As String = clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(i) & "").Value)
                                        If clsCommon.CompairString(FirstStockUnit, SecondStockUnit) = CompairStringResult.Equal AndAlso (clsCommon.CompairString(SecondStockUnit, "Y") = CompairStringResult.Equal And clsCommon.CompairString(FirstStockUnit, "Y") = CompairStringResult.Equal) Then
                                            Throw New Exception("More than one Stock Unit [Yes] not accepted at line no '" + LineNo + "' ")
                                        End If
                                        If UOMCount = 1 Then
                                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Custom Conversion" & clsCommon.myCstr(i) & "").Value), "Y") = CompairStringResult.Equal Then
                                                If isCustomConversion Then
                                                    Throw New Exception("Not more than one UOM Can be of Custom Conversion Type. At line no '" + LineNo + "' ")
                                                End If
                                                isCustomConversion = True
                                            End If
                                        End If
                                    Next
                                    clsCommon.AddColumnsForChange(colUOM, "Custom_Conversion", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Custom Conversion" & clsCommon.myCstr(j) & "").Value), "Y") = CompairStringResult.Equal, 1, 0))
                                    '======================================================================================================================
                                    Dim StockCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select SUM(Item_Code) as Item_Code From (Select Count(Item_Code) as Item_Code from tspl_inventory_movement Where Item_Code='" + Item_Code + "' AND Stock_UOM ='" + strUOM + "' union all Select Count(Item_Code) as Item_Code from TSPL_INVENTORY_MOVEMENT_NEW Where Item_Code='" + Item_Code + "' AND Stock_UOM ='" + strUOM + "')mm", trans))
                                    If StockCount >= 1 AndAlso clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value) <> 1 Then
                                        Throw New Exception("Unit '" + UnitCode + "' is used as  Stocking unit,please set Conversion Factor 1 at line no '" + LineNo + "' ")
                                    End If




                                    'Else
                                    '    Throw New Exception("Please Insert UOM at Line No '" + LineNo + "' ")
                                    '------------UOM DESCRIPTION--------------------
                                    Dim UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans))
                                    clsCommon.AddColumnsForChange(colUOM, "UOM_Description", UOM_Description)
                                    '----------------------------------------------
                                    If AllowItemConversionAutomation = 0 Then
                                        If clsCommon.myCstr(grow.Cells("Stocking Unit" & clsCommon.myCstr(j) & "").Value) = "Y" Then
                                            clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "Y")
                                            If clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value) > 1 Then
                                                Throw New Exception("The Coversion Unit Should be [1] for Stocking Unit [Yes] at line no '" + LineNo + "'")
                                            End If
                                            'clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 1)
                                        Else
                                            clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "N")
                                            'clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 0)
                                        End If
                                    Else
                                        If j = 1 Then
                                            clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "Y")
                                        Else
                                            clsCommon.AddColumnsForChange(colUOM, "Stocking_Unit", "N")
                                        End If
                                    End If



                                    Dim dblConvF As Double = 0


                                    '' Anubhooti 25-Feb-2015 (Default UOM should not be dependent upon stocking_unit)
                                    If clsCommon.myLen(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value) > 0 Then
                                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "0") = CompairStringResult.Equal Then
                                            DefaultUOMCount = +1
                                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "1") = CompairStringResult.Equal Then
                                                IsDefaultUOM = IsDefaultUOM + 1
                                                clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 1)
                                            ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Default UOM" & clsCommon.myCstr(j) & "").Value), "0") = CompairStringResult.Equal Then
                                                clsCommon.AddColumnsForChange(colUOM, "Default_UOM", 0)
                                            End If
                                        Else
                                            '    Throw New Exception("Please enter Default UOM As '1' Or '0' at line '" + LineNo + "'.")
                                            dtt.Rows(jj)("ErrorDesc") = "Please enter Default UOM As '1' Or '0'"
                                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        End If

                                    End If
                                    '  Dim strconFactor As String
                                    If AllowItemConversionAutomation = 1 Then
                                        Dim IsStockingUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", trans))
                                        Dim StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnit & "'", trans))
                                        If j = 1 Then
                                            If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                                                Qry = "update TSPL_ITEM_MASTER set Weight_UOM='" & strStockingUnit & "',Weight_Value=1  where Item_Code='" & Item_Code & "'"
                                                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                                            End If
                                            clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", 1)
                                            clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost)
                                        Else
                                            '' Automatic Unit conversion start here

                                            Dim IntRowNo As Integer = j
                                            IntRowNo = IntRowNo - 1
                                            Dim IsUnitWeight = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weight_Type from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                            Dim IsUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                            Dim IsUnitPackingType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Packet_Type from tspl_unit_master where Unit_Code='" & strUOM & "'", trans))
                                            If clsCommon.CompairString(IsUnitWeight, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IsUnitPackingType, "N") = CompairStringResult.Equal Then
                                                If clsCommon.CompairString(IsStockingUnitWeight, "Y") = CompairStringResult.Equal Then
                                                    If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                                                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUOM & "' and Contained_UOM='" & strStockingUnit & "'", trans))
                                                    Else
                                                        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & strUOM & "' and Contained_UOM='" & strStockingUnit & "' and Structure_Code='" & Structure_Code & "'", trans))
                                                    End If
                                                    If dblConvF = 0 Then
                                                        dtt.Rows(jj)("ErrorDesc") = "Please enter Weight Conversion in Weight master Container unit - " & strUOM & " Contained Unit - " & strStockingUnit
                                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                                    End If
                                                Else
                                                    If clsCommon.CompairString(Item_Type, "F") = CompairStringResult.Equal Then
                                                        If clsCommon.myLen(WeightUOM) = 0 Then
                                                            dtt.Rows(jj)("ErrorDesc") = "Please enter Weight UOM"
                                                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                                        ElseIf clsCommon.myCdbl(Weight_Value) = 0 Then
                                                            dtt.Rows(jj)("ErrorDesc") = "Please enter Weight UOM Conversion"
                                                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                                        End If
                                                        Dim strStockingUnitWeight = WeightUOM
                                                        If clsCommon.CompairString(strStockingUnitWeight, strUOM) = CompairStringResult.Equal Then
                                                            dblConvF = 1
                                                        Else
                                                            StockingUnitFamily = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Category from tspl_unit_master where Unit_Code='" & strStockingUnitWeight & "'", trans))
                                                            If clsCommon.CompairString(StockingUnitFamily, IsUnitFamily) = CompairStringResult.Equal Then
                                                                dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUOM) & "' and Contained_UOM='" & strStockingUnitWeight & "'", trans))
                                                            Else
                                                                dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Contained_Qty from TSPL_WEIGHT_CONVERSION where Container_UOM='" & clsCommon.myCstr(strUOM) & "' and Contained_UOM='" & strStockingUnitWeight & "' and Structure_Code='" & clsCommon.myCstr(Structure_Code) & "'", trans))
                                                            End If
                                                            If dblConvF > 0 Then
                                                                dblConvF = dblConvF / clsCommon.myCdbl(Weight_Value)
                                                            Else
                                                                dtt.Rows(jj)("ErrorDesc") = "Please enter Weight Conversion in Weight master Container unit - " & strUOM & " Contained Unit - " & strStockingUnitWeight
                                                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                                            End If
                                                        End If

                                                    End If

                                                End If

                                            Else
                                                dblConvF = clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value)
                                            End If

                                            '' automatic unit conversion end here
                                            If dblConvF > 0 Then
                                                clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", dblConvF)
                                                clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost * dblConvF)
                                            Else
                                                dtt.Rows(jj)("ErrorDesc") = "Please Insert Conversion Factor for unit " & strUOM
                                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                            End If
                                        End If
                                    Else

                                        Dim ConversionFactor As Double = clsCommon.myCdbl(grow.Cells("Conversion Factor" & clsCommon.myCstr(j) & "").Value)
                                        If ConversionFactor > 0 Then
                                            clsCommon.AddColumnsForChange(colUOM, "COnversion_Factor", ConversionFactor)
                                            clsCommon.AddColumnsForChange(colUOM, "Item_Cost", dblItemCost * ConversionFactor)
                                        Else
                                            'Throw New Exception("Please Insert Convrsion Factor at Line No '" + LineNo + "' ")
                                            dtt.Rows(jj)("ErrorDesc") = "Please Insert Convrsion Factor"
                                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        End If
                                    End If

                                    ''richa agarwal 19/01/2015
                                    Dim dbweight As Double = clsCommon.myCdbl(grow.Cells("Weight" & clsCommon.myCstr(j) & "").Value)
                                    clsCommon.AddColumnsForChange(colUOM, "Net_Weight", dbweight)

                                    If grow.Cells("Gross_Weight" & clsCommon.myCstr(j) & "").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Gross_Weight" & clsCommon.myCstr(j) & "").Value) > 0 AndAlso IsNumeric(grow.Cells("Gross_Weight" & clsCommon.myCstr(j) & "").Value) Then
                                        uom_GrossWt = clsCommon.myCdbl(grow.Cells("Gross_Weight" & clsCommon.myCstr(j) & "").Value)
                                        clsCommon.AddColumnsForChange(colUOM, "Gross_Weight", uom_GrossWt)
                                    End If

                                    ''----------
                                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + Item_Code + "' AND UOM_Code='" + UnitCode + "'", trans))
                                    If Count <= 0 Then
                                        clsCommon.AddColumnsForChange(colUOM, "Item_Code", Item_Code)
                                        clsCommon.AddColumnsForChange(colUOM, "UOM_Code", UnitCode)
                                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colUOM, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                    Else
                                        Dim whrClas As String = "Item_Code = '" + Item_Code + "' and uom_code='" + UnitCode + "'"
                                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colUOM, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                                    End If
                                End If


                                'If clsCommon.myLen(grow.Cells("Weight" & clsCommon.myCstr(j) & "").Value) > 0 Then
                                '    If Not IsNumeric(grow.Cells("Weight" & clsCommon.myCstr(j) & "").Value) Then
                                '        Throw New Exception("Please insert decimal data in Weight at Line No '" + LineNo + "' ")
                                '    Else
                                '        clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight" & clsCommon.myCstr(j) & "").Value))
                                '    End If
                                'Else
                                '    clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight" & clsCommon.myCstr(j) & "").Value))
                                'End If


                                ''
                            Next
                            If UOMCount <= 0 Then
                                'Throw New Exception("Please insert at least one UOM for item code '" + Item_Code + "' ")
                                dtt.Rows(jj)("ErrorDesc") = "Please insert at least one UOM for item code '" + Item_Code + "' "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            If DefaultUOMCount <= 0 Then
                                'Throw New Exception("Please enter 1 to make UOM as default UOM for item code '" + Item_Code + "' ")
                                dtt.Rows(jj)("ErrorDesc") = "Please enter 1 to make UOM as default UOM for item code '" + Item_Code + "' "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            If IsDefaultUOM <= 0 Then
                                'Throw New Exception("Please check ! One UOM should be Default UOM for item code '" + Item_Code + "' ")
                                dtt.Rows(jj)("ErrorDesc") = "Please check ! One UOM should be Default UOM for item code '" + Item_Code + "'  "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            If IsDefaultUOM > 1 Then
                                'Throw New Exception("Please check ! Default UOM should not be more than one for item code '" + Item_Code + "' ")
                                dtt.Rows(jj)("ErrorDesc") = "Please check ! Default UOM should not be more than one for item code '" + Item_Code + "' "
                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
                            End If
                            ' Ticket No : TEC/26/07/19-000962 By Prabhakar
                            If clsCommon.myLen(strProductionFATSNF_KG_Unit) > 0 Then
                                Dim isProductionFATSNF_KG_UnitPrvExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_ITEM_UOM_DETAIL where item_code = '" + Item_Code + "' and UOM_Code = '" + strProductionFATSNF_KG_Unit + "'", trans))
                                If clsCommon.CompairString(clsCommon.myCstr(Product_Type), "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Product_Type), "MP") = CompairStringResult.Equal Then
                                    If isUomKGExist = False AndAlso isProductionFATSNF_KG_UnitPrvExist = False Then
                                        dtt.Rows(jj)("ErrorDesc") = "If Item is of MP or Milk type then " + strProductionFATSNF_KG_Unit + " must be defined as one of the conversion in Item Master for item code '" + Item_Code + "'."
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        'Throw New Exception("If Item is of MP or Milk type then " + strProductionFATSNF_KG_Unit + " must be defined as one of the conversion in Item Master.")
                                    End If
                                End If
                            End If

                            '' Category Detail 

                            Dim strCategoryStructure As String = ""
                            Dim CategoryCode As String = ""
                            Dim strCategoryValue As String = ""
                            Dim CategroyDetails As String = clsCommon.myCstr("select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION   from TSPL_ITEM_CATEGORY_STRUCT_DETAIL left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type where TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" & clsCommon.myCstr(grow.Cells("Category Structure").Value) & "' and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='Item' order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no")
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(CategroyDetails, trans)

                            'For Each dr1 As DataRow In dt1.Rows
                            '    clsCommon.AddColumnsForChange(colCat, "Item_Cagetory_Code", clsCommon.myCstr(dr1("ITEM_CATEGORY_CODE")))
                            'Next
                            For i As Integer = 1 To 15
                                Dim colCat As New Hashtable()
                                If dt1.Rows.Count >= i Then
                                    clsCommon.AddColumnsForChange(colCat, "Item_Category_Code", clsCommon.myCstr(dt1.Rows(i - 1)("Item_Category_Code")))

                                    Dim CategoryValue As String = clsCommon.myCstr(grow.Cells("Item Category Level" & clsCommon.myCstr(i) & "").Value)
                                    CategoryCode = clsCommon.myCstr(clsCommon.myCstr(dt1.Rows(i - 1)("Item_Category_Code")))
                                    If clsCommon.myLen(CategoryValue) > 0 Then
                                        strCategoryValue = clsDBFuncationality.getSingleValue("Select CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES WHERE ITEM_CATEGORY_CODE='" + CategoryCode + "' AND CODE='" + CategoryValue + "' and isnull(form_type,'item')='item'", trans)
                                        If clsCommon.CompairString(strCategoryValue, CategoryValue) = CompairStringResult.Equal Then
                                            clsCommon.AddColumnsForChange(colCat, "Item_Cagetory_Values", strCategoryValue)
                                        Else
                                            '            Throw New Exception("Item category level" & clsCommon.myCstr(i) & " at Line No '" + LineNo + "' Does Not Exist")
                                            dtt.Rows(jj)("ErrorDesc") = "Item category level" & clsCommon.myCstr(i) & "  Does Not Exist "
                                            ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                        End If
                                    Else
                                        'Throw New Exception("Please insert item category level" & clsCommon.myCstr(i) & " at Line No '" + LineNo + "' ")
                                        dtt.Rows(jj)("ErrorDesc") = "Please insert item category level" & clsCommon.myCstr(i) & " "
                                        ErrCount = ErrCount + 1 : GoTo ExitLOOP
                                    End If
                                    Dim whrClas1 As String
                                    'If Not clsCommon.CompairString(Item_Code, Item_Code1) = CompairStringResult.Equal Then
                                    If i = 1 Then
                                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ITEM_MASTER_CATEGORY WHERE Item_code='" + Item_Code + "'", trans)
                                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_ITEM_MASTER Set Item_Category_Struct_Code='" + clsCommon.myCstr(grow.Cells("Category Structure").Value) + "' WHERE Item_Code='" + Item_Code + "'", trans)
                                    End If
                                    Item_Code = Item_Code

                                    Dim Count1 As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COunt(*) from TSPL_ITEM_MASTER_CATEGORY WHERE Item_code='" + Item_Code + "' AND Item_Category_Code='" + CategoryCode + "'", trans))
                                    If Count1 <= 0 Then
                                        clsCommon.AddColumnsForChange(colCat, "Item_code", Item_Code)
                                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colCat, "TSPL_ITEM_MASTER_CATEGORY", OMInsertOrUpdate.Insert, "", trans)
                                    Else
                                        whrClas1 = "Item_code='" + Item_Code + "' AND Item_Category_Code='" + strCategoryStructure + "'"
                                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(colCat, "TSPL_ITEM_MASTER_CATEGORY", OMInsertOrUpdate.Update, whrClas1, trans)
                                    End If
                                End If

                            Next

                            '
                            'Dim CategoryStructure As String = clsCommon.myCstr(grow.Cells("Category Structure" & clsCommon.myCstr(j) & "").Value)
                            'If clsCommon.myLen(CategoryStructure) > 0 Then
                            '    strCategoryStructure = clsDBFuncationality.getSingleValue("Select ITEM_CATEGORY_STRUCT_CODE from TSPL_ITEM_CATEGORY_STRUCTURE WHERE ITEM_CATEGORY_STRUCT_CODE='" + CategoryStructure + "' and isnull(form_type,'item')='item'", trans)
                            '    If clsCommon.CompairString(strCategoryStructure, CategoryStructure) = CompairStringResult.Equal Then
                            '    Else
                            '        Throw New Exception("The Category Structure '" + CategoryStructure + "' at Line No '" + LineNo + "' Does Not Exist")
                            '    End If
                            'Else
                            '    Throw New Exception("Please Insert Category Structure at Line No '" + LineNo + "' ")
                            'End If


                            '----------------------------------------------

                            '------------Category Value--------------------


                            '----------------------------------------------

                            ''

                        End If
ExitLOOP:
                        Try
                            ' Ticket No : ERO/17/07/19-000954 By Prabhakar
                            Dim isExistItem2 As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_ITEM_MASTER Where Item_Code='" + Item_Code + "'", trans))
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoItemNLevel, clsFixedParameterCode.AutoItemNLevel, trans)) = 1 AndAlso isExistItem2 = False Then
                                Dim Qry2 As String = "update TSPL_ITEM_TYPE_MASTER set PREFIX='" + clsCommon.incval(Item_Code, 0, -1) + "' where ITEM_TYPE_CODE='" + clsCommon.myCstr(Item_Type) + "'"
                                clsDBFuncationality.ExecuteNonQuery(Qry2, trans)
                            End If
                        Catch ex As Exception

                        End Try


                    Next
                    trans.Commit()
                    'clsCommon.ProgressBarHide()
                    clsCommon.ProgressBarPercentHide()
                    dtt.DefaultView.RowFilter = "ErrorDesc<>''"
                    dtt = dtt.DefaultView.ToTable
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!" & IIf(dtt.Rows.Count > 0, " Except of  " & dtt.Rows.Count & " Records", ""), Me.Text, MessageBoxButtons.OK)
                    If dtt.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "UnImportedItemList"
                        ff.Text = "Record Could not Saved"
                        ff.dt = dtt
                        ff.ShowDialog()
                    End If

                Catch ex As Exception
                    'clsCommon.ProgressBarHide()
                    clsCommon.ProgressBarPercentHide()
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            Catch ex As Exception

                'clsCommon.ProgressBarHide()
                clsCommon.ProgressBarPercentHide()
                'trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gv1)
            End Try
        End If
    End Sub
    Private Sub rmWholeExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmWholeExport.Click
        Try
            Dim code As String = ""
            Dim whrcls As String = ""
            QrySheet = " Select TSPL_ITEM_MASTER.Item_Code as [Item Code], Item_Desc as [Item Description],Sku_Seq [Seq No],Short_Description as [Short Description], Structure_Code as [Structure Code], Rack_No as [Rack No],"
            QrySheet += " Purchase_Class_Code as [Purchase A/c Set], Sale_Class_Code as [Sale A/c Set], item_category as [Category], "
            QrySheet += " Sub_item_category as [Sub Category], Unit_Code as [UOM], Item_Type as [Item Type], TypeOfItm as [Type], Cost,TSPL_ITEM_MASTER.tolerence as [Tolerence],TSPL_ITEM_MASTER.tech_shelf_life as [Shelf Life],TSPL_ITEM_MASTER.min_shelf_life as [Min Shelf Life],Purchase_Price as [Standard Purchase Price],"
            ''CHANGES BY RICHA AGARWAL IN QUERY
            QrySheet += " Morning,Cheapter_Heads as [Chapter Code], Item_Category_Struct_Code as [Category Structure], Weight_UOM, Weight_Value,Is_MRP,ITF_CODE,Active,Is_FreshItem,Is_Ambient,Product_type as [Product Type],case when coalesce(Is_Purchaseable,'0')='1' then 'Yes' else 'No' end as [Is Purchaseable],case when coalesce(Is_AllowQC_On_Purchase,'0')='1' then 'Yes' else 'No' end as [Is Allow QC on Purchase],Is_CrateType,Is_CAN_Type,case When Item_used_as='I' then 'MCC Issue' when item_used_as='S' then 'MCC Sale' when item_used_as='P' then 'Production' else 'None' end as [Used As],Part_No,Drawing_No,ISNULL(TSPL_ITEM_MASTER.GL_Account,'') AS [GL Account],(SELECT Description  From TSPL_GL_ACCOUNTS where Account_Code=TSPL_ITEM_MASTER.GL_Account) AS [Account Description],ISNULL(WARRANTY_CODE,'') AS [Warranty Code],ISNULL(Warranty_Applied_From ,'') AS [Warranty Applied From],Alies_Name as [Alies Name],is_leakage_not_Applicable as [Leakage Not Applicable],Production_Tolerance as [Production Tolerance]"
            ''------------
            If clsCommon.myLen(code) > 0 Then
                QrySheet += ",TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,Is_Serial_Item,Serial_Counter,Is_Pick_Auto_SrNo,Is_Tax_Exempted as [Tax Exempted]"
            Else
                QrySheet += ",'' as Item_Cagetory_Values,Is_Serial_Item,Serial_Counter,Is_Pick_Auto_SrNo,Is_Tax_Exempted as [Tax Exempted],Is_Rate_Change_OnDairyDispatch,AllowSRNWithoutShortReject"
            End If
            '' UOM Details Section
            Dim UOMTotal As String = ""
            Dim UOMConTotal As String = ""
            Dim UOMDefTotal As String = ""
            Dim UOMStockUnitTotal As String = ""

            Dim UOMConversion As String = ""
            Dim UOMDefault As String = ""
            Dim UOMDetail As String = ""
            Dim UOMStockUnit As String = ""
            ''richA AGARWAL  
            Dim UOMWeight As String = ""
            Dim TotalUOMWeight As String = ""
            ''------------
            Dim UOMGrossWeight As String = ""
            Dim TotalUOMGrossWt As String = ""
            Dim CustomConversion As String = ""
            For j As Integer = 1 To 10
                UOMDetail = "(Select UOM_Code From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,UOM_Code,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS UOM" & j & ""
                UOMConversion = "(Select Convert (varchar,Conversion_Factor) as Conversion_Factor From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Conversion_Factor,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Conversion Factor" & j & "]"
                UOMDefault = "(Select Default_UOM From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Default_UOM,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Default UOM" & j & "]"
                CustomConversion += ",(Select Convert (varchar, Custom_Conversion) as Custom_Conversion From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,(case when  Custom_Conversion=1 then 'Y' else 'N' end) as Custom_Conversion,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Custom Conversion" & j & "]"
                UOMStockUnit = "(Select Stocking_Unit From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Stocking_Unit,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Stocking Unit" & j & "]"
                ''richa agawral
                UOMWeight = "(Select Convert (varchar,Net_Weight) as Weight From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Net_Weight,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Weight" & j & "]"
                ''----------------
                UOMGrossWeight = "(Select Convert (varchar,Gross_Weight) as Gross_Weight From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,UOM_Code ) As SNo,Gross_Weight,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =" & j & " )  AS [Gross_Weight" & j & "]"
                UOMTotal = UOMTotal + "," + "" + UOMDetail + ""
                UOMConTotal = UOMConTotal + "," + "" + UOMConversion + ""
                UOMDefTotal = UOMDefTotal + "," + "" + UOMDefault + ""
                UOMStockUnitTotal = UOMStockUnitTotal + "," + "" + UOMStockUnit + ""
                ''richa agawral
                TotalUOMWeight = TotalUOMWeight + "," + "" + UOMWeight + ""
                ''--------------
                TotalUOMGrossWt = TotalUOMGrossWt + "," + "" + UOMGrossWeight + ""
            Next

            '' Category Structure Section 
            Dim UOMCatCodeTotal As String = ""
            Dim UOMCatLevelTotal As String = ""
            Dim ItemCatDespTotal As String = ""

            Dim UOMCatCode As String = ""
            Dim UOMCatLevel As String = ""
            Dim ItemCatDesp As String = ""
            For j As Integer = 1 To 15
                UOMCatCode = "(Select Item_Category_Code  From (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no as SNo,Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   From TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_MASTER_CATEGORY.item_category_code and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='item' where Item_Code =TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=tspl_item_master.item_category_struct_code)xxx where xxx.SNo =" & j & " ) AS [Item Category Code" & j & "],(Select DESCRIPTION  From (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no As SNo  ,Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code  ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION  From TSPL_ITEM_MASTER_CATEGORY LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'item')='item' left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_MASTER_CATEGORY.item_category_code and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='item' and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_CATEGORY_LEVEL.item_category_code and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type  where Item_Code =TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=tspl_item_master.item_category_struct_code )xxx where xxx.SNo =" & j & " ) AS [Item Category Code Description" & j & "]"
                UOMCatLevel = "(Select Item_Cagetory_Values  From (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no As SNo  ,Item_Code,Item_Cagetory_Values   From TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_MASTER_CATEGORY.item_category_code and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='item' where Item_Code =TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=tspl_item_master.item_category_struct_code)xxx where xxx.SNo =" & j & " ) AS [Item Category Level" & j & "],(Select DESCRIPTION  From (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no As SNo  ,Item_Code,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION   From TSPL_ITEM_MASTER_CATEGORY LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE =Item_Cagetory_Values and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'item')='item' left outer join TSPL_ITEM_CATEGORY_STRUCT_DETAIL on TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_MASTER_CATEGORY.item_category_code and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='item' and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_code=TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code where Item_Code =TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_CATEGORY_STRUCT_DETAIL.item_category_struct_code=tspl_item_master.item_category_struct_code)xxx where xxx.SNo =" & j & " ) AS [Item Category Level Desp" & j & "]"
                ItemCatDesp = "(select DESCRIPTION From (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no As SNo,DESCRIPTION,ITEM_CATEGORY_STRUCT_CODE,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.Form_Type    From  TSPL_ITEM_CATEGORY_STRUCT_DETAIL left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='item' where TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE=tspl_item_master.item_category_struct_code and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='Item' ) xxx Where xxx.SNo =" & j & " ) AS [Item Category Level Description" & j & "]"""
                UOMCatCodeTotal = UOMCatCodeTotal + "," + "" + UOMCatCode + ""

                ItemCatDespTotal = ItemCatDespTotal + "," + "" + ItemCatDesp + ""
                UOMCatLevelTotal = UOMCatLevelTotal + "," + "" + UOMCatLevel + ""
            Next
            Dim itemCostForStockingUnit = "(Select Convert (varchar,Item_Cost) as Item_Cost  From (Select ROW_NUMBER () over (order by Stocking_Unit desc, item_Code,Item_Cost ) As SNo,Item_Cost,Item_Code  From TSPL_ITEM_UOM_DETAIL where Item_Code =TSPL_ITEM_MASTER.Item_Code) xxx where xxx.SNo =1 ) "
            ''CHANGES BY RICHA AGARWAL
            QrySheet += UOMTotal + " " + UOMConTotal + " " + UOMDefTotal + " " + UOMStockUnitTotal + " " + CustomConversion + " " + TotalUOMWeight + " " + TotalUOMGrossWt + " " + UOMCatCodeTotal + " " + UOMCatLevelTotal + ",case when coalesce(TSPL_ITEM_MASTER.Is_Scheme_Item,0)=1 then 1 else 0 end as Is_Scheme_Item,TSPL_ITEM_MASTER.Distributor_Commission,TSPL_ITEM_MASTER.CNF_Commission,CSA_Type,IsTaxable,is_Batch_Item,TSPL_ITEM_MASTER.HSN_Code as 'HSN Code',TSPL_ITEM_MASTER.Chilled_Freezen as [Chilled Freezen],TSPL_ITEM_MASTER.IS_SCRAP_ITEM as [IS SCRAP ITEM],TSPL_ITEM_MASTER.SCrap_Item_Code as [Scrap Item Code], " + itemCostForStockingUnit + " as [Item Cost(Stocking Unit)] "
            QrySheet += ",isnull(TSPL_ITEM_MASTER.Is_Insurance,0) as Is_Insurance,TSPL_ITEM_MASTER.InsuranceNo,TSPL_ITEM_MASTER.InsuranceFromDate,TSPL_ITEM_MASTER.InsuranceToDate"
            QrySheet += " ,TSPL_ITEM_MASTER.Marketing_Seq as [Marketing Seq No] "
            QrySheet += " ,TSPL_ITEM_MASTER.Alies_Name2 as [Alies Name2],TSPL_ITEM_MASTER.Alies_Name3 as [Alies Name3] "
            QrySheet += " from TSPL_ITEM_MASTER  " + code + ""
            transportSql.ExporttoExcel(QrySheet, whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkIsSerailzedInventory_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsSerailzedInventory.CheckStateChanged
        Try
            txtNextAutoSerialCounter.Text = 0
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndGLAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGLAcc._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndGLAcc.Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ", fndGLAcc.Value, "Account_Code", isButtonClicked)
        If clsCommon.myLen(fndGLAcc.Value) > 0 Then
            LblGLAcc.Visible = True
            LblGLAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndGLAcc.Value + "' ")
        Else
            LblGLAcc.Text = ""
            LblGLAcc.Visible = False
        End If

    End Sub
    Private Sub cboItemType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboItemType.SelectedIndexChanged

        If IsFormLoaded Then
            If clsCommon.CompairString(cboItemType.SelectedValue, "A") = CompairStringResult.Equal Then
                chkCreateSepAssetForEachQty.Checked = False
                chkCreateSepAssetForEachQty.Enabled = True
                ChkCrateType.Enabled = False
                chkIsCanType.Enabled = False
                chkFresh.Enabled = False
                chkAmbient.Enabled = False
                txtSeqNo.Enabled = False
                chkChilledFreezen.Enabled = False
            Else
                chkCreateSepAssetForEachQty.Checked = False
                chkCreateSepAssetForEachQty.Enabled = False
                ChkCrateType.Enabled = True
                chkIsCanType.Enabled = True
                chkFresh.Enabled = True
                chkAmbient.Enabled = True
                txtSeqNo.Enabled = True
                chkChilledFreezen.Enabled = True
            End If

            ''====================================================================
            If AllowFinishGoodAsBatchItem AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "F") = CompairStringResult.Equal Then
                chkIsReqBatch.Checked = True
            Else
                chkIsReqBatch.Checked = False
            End If
            If ToleranceMandatoryFor_RM_Other_Trade AndAlso (clsCommon.CompairString(cboItemType.SelectedValue, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "O") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboItemType.SelectedValue, "T") = CompairStringResult.Equal) Then
                txt_tolerance.MendatroryField = True
            Else
                txt_tolerance.MendatroryField = False
            End If
            ''=========================================================================

        End If

        Dim row As Integer = cboItemType.SelectedIndex
        If row > 0 Then
            Dim Qry = clsItemType.GetNavQry(NavigatorType.Current, cboItemType.SelectedValue)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("IsFixedTolerance")), "Y") = CompairStringResult.Equal Then
                    txt_tolerance.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePer"))
                    txt_tolerance.Enabled = False
                Else
                    txt_tolerance.Value = 0
                    txt_tolerance.Enabled = True
                End If
            End If
        Else
            txt_tolerance.Value = 0
            txt_tolerance.Enabled = True
        End If
    End Sub
    Private Sub LoadDataPartNo(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsPartNoMaster()
        Try
            obj = clsPartNoMaster.GetData(strCode, NavType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtPartNo.Value = obj.Code
                txtDescription.Text = obj.Description
                txtBrand.Text = obj.Brand
                txttype.Text = obj.Type
                txtReleasedBy.Text = obj.Released_By
                txtReleasedDate.Text = obj.Released_Date
                txtSubPart.Text = obj.Sub_Part

                txtPartNo.MyReadOnly = False

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub txtPartNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPartNo._MYValidating
        'Dim qry As String = "select count(*) from tspl_part_no_master where code='" + txtPartNo.Value + "'"
        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        'txtPartNo.MyReadOnly = False
        'If check > 0 Then
        '    txtPartNo.MyReadOnly = False
        'End If

        'If txtPartNo.MyReadOnly OrElse isButtonClicked Then

        'If isButtonClicked Then
        txtPartNo.Value = clsPartNoMaster.GetFinder("", txtPartNo.Value, isButtonClicked)
        LoadDataPartNo(txtPartNo.Value, NavigatorType.Current)
        'End If
    End Sub
    Private Sub fndCSA_AC_Code__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCSA_AC_Code._MYValidating
        Dim qry As String = "select Cust_Account as Code,Cust_Acct_Desc as [Description],Receivable_Control_acct as [Debitor Account],Receipts_Discount_acct as [Discount Account],Advance_acct as [Advance A/c],Write_Offs as [Write Off],Container_Deposit as [Container Deposit],SECURITY_ACCOUNT as [Security A/c],BANK_GUARANTEE as [Bank Guarantee A/c],GSOC_Acct as [GSOC A/c],Consignment_Acct as [Consignment A/c],Gain_Acct as [Gain A/c],Loss_Acct as [Loss A/c] from TSPL_CUSTOMER_ACCOUNT_SET"
        fndCSA_AC_Code.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CSASALEAC", qry, "Code", "", fndCSA_AC_Code.Value, "", isButtonClicked))
        txtCSA_AC_Name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Acct_Desc from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + fndCSA_AC_Code.Value + "'"))
    End Sub
    Private Sub cboItemType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboItemType.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal Then
            MyLabel32.Visible = True
            txt_shelflife.Visible = True
        Else
            MyLabel32.Visible = False
            txt_shelflife.Visible = False
        End If
    End Sub
    Private Sub txt_tolerance_TextChanged(sender As Object, e As EventArgs) Handles txt_tolerance.TextChanged
        If txt_tolerance.Text.Contains("-") Then
            txt_tolerance.Text = ""
        End If
    End Sub
    Private Sub RadMenu1_Click(sender As Object, e As EventArgs) Handles RadMenu1.Click

    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Item Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "Item_Code", "TSPL_ITEM_MASTER", "TSPL_ITEM_UOM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub ChkCrate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCrate.ToggleStateChanged
        If ChkCrate.Checked = True Then
            chkCAN.Enabled = False
        Else
            chkCAN.Enabled = True
        End If
    End Sub
    Private Sub chkCAN_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCAN.ToggleStateChanged
        If chkCAN.Checked = True Then
            ChkCrate.Enabled = False
        Else
            ChkCrate.Enabled = True
        End If
    End Sub
    Private Sub ChkCrateType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCrateType.ToggleStateChanged
        '====================Added by preeti Gupta Against ticket no[BHA/12/06/18-000050]==========
        If ChkCrateType.Checked = True Then
            chkIsCanType.Enabled = False
        Else
            chkIsCanType.Enabled = True
        End If
        '====================Added by preeti Gupta Against ticket no[BHA/12/06/18-000050]==========
    End Sub
    Private Sub chkIsCanType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIsCanType.ToggleStateChanged
        If chkIsCanType.Checked = True Then
            ChkCrateType.Enabled = False
        Else
            ChkCrateType.Enabled = True
        End If
    End Sub
    Private Sub chkScrapItem_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkScrapItem.ToggleStateChanged
        If chkScrapItem.Checked = True Then
            fndScrapItem.Visible = True
        Else
            fndScrapItem.Visible = False
            fndScrapItem.Value = ""
        End If
    End Sub
    Private Sub fndScrapItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndScrapItem._MYValidating
        Try
            Dim wher As String = " tspl_item_master.Item_Code not in ( '" + clsCommon.myCstr(txtCode.Value) + "' ) and tspl_item_master.Item_Code not in (select Scrap_Item_Code from tspl_item_master  where Scrap_Item_Code is not null) and tspl_item_master.Structure_Code = '" + txtStructurer.Value + "' "
            Dim qry As String = "select tspl_item_master.Item_Code as Code ,tspl_item_master.Item_Desc as Description   from  tspl_item_master  "
            fndScrapItem.Value = clsCommon.ShowSelectForm("ISCRAP@Item", qry, "Code", wher, fndScrapItem.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
        'Ticket No-TEC/06/08/19-000980
        Try
            If RadPageView1.SelectedPage.Name = "RadPageViewPage2" AndAlso txtCode.MyReadOnly = True Then
                If SelectUOMTab = False Then

                    For ii As Integer = 0 To gvUOM.RowCount - 1
                        If clsCommon.myLen(gvUOM.Rows(ii).Cells(UOMColUnit).Value) > 0 Then

                            If clsItemMaster.IsItemUsedWithUOM(txtCode.Value, gvUOM.Rows(ii).Cells(UOMColUnit).Value, Nothing) = True Then
                                gvUOM.Rows(ii).Cells(UOMColStockUnitChangable).Value = 1
                            End If
                            If clsItemMaster.IsItemUsedWithUOMForStockingCheck(txtCode.Value, gvUOM.Rows(ii).Cells(UOMColUnit).Value, Nothing) = True Then
                                gvUOM.Rows(ii).Cells(UOMColStockUnitChangable2).Value = 1
                            End If

                        End If
                    Next

                    SelectUOMTab = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvPurQCPar_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvPurQCPar.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvPurQCPar.Columns(colPurQCParamCode) Then
                        OpenPurchaseQCParameters(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenPurchaseQCParameters(ByVal isButtonClicked As Boolean)
        Dim qry As String = ""
        If SettItemWiseQualityCheckInGeneralPurchase = True Then
            qry = "select Code,Description from TSPL_QC_LOG_SHEET_MASTER "
        Else
            qry = "select Code,Description from TSPL_QC_LOG_SHEET_MASTER where nature='A'"
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ItemQCFNDP", qry)
        If dr IsNot Nothing Then
            gvPurQCPar.CurrentRow.Cells(colPurQCParamCode).Value = clsCommon.myCstr(dr("Code"))
            gvPurQCPar.CurrentRow.Cells(colPurQCParamDesc).Value = clsCommon.myCstr(dr("Description"))
        Else
            gvPurQCPar.CurrentRow.Cells(colPurQCParamCode).Value = ""
            gvPurQCPar.CurrentRow.Cells(colPurQCParamDesc).Value = ""
            gvPurQCPar.CurrentRow.Cells(colPurQCParamSpecification).Value = Nothing
        End If
    End Sub
    Private Sub gvPurQCPar_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvPurQCPar.CurrentColumnChanged
        If gvPurQCPar.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPurQCPar.CurrentRow.Index
            gvPurQCPar.CurrentRow.Cells(colPurQCParamSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvPurQCPar.Rows.Count - 1 Then
                gvPurQCPar.Rows.AddNew()
                gvPurQCPar.CurrentRow = gvPurQCPar.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If SettItemWiseQualityCheckInGeneralPurchase Then
                qry = "select Item_Code as [Item Code],SNo,QC_Code as [QC Code],TSPL_QC_LOG_SHEET_MASTER.Description as [QC Name],Specifications from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code"

                transportSql.ExporttoExcel(qry, Me)
            Else
                Throw New Exception("This is Not for you")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            If SettItemWiseQualityCheckInGeneralPurchase Then
                Dim gv1 As New RadGridView()
                Me.Controls.Add(gv1)
                Dim LineNo As String = ""
                Dim countDefaultUOM As Integer = 0
                If transportSql.importExcel(gv1, "Item Code", "SNo", "QC Code", "QC Name", "Specifications") Then
                    Dim isSaved As Boolean = True
                    Dim currentdate As Date = Date.Today
                    Dim trans As SqlTransaction = Nothing
                    Dim arr As New Dictionary(Of String, List(Of clsItemPurchaseQCParameter))
                    clsCommon.ProgressBarShow()
                    Try
                        trans = clsDBFuncationality.GetTransactin()
                        For i As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.myLen(gv1.Rows(i).Cells("Item Code").Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(i).Cells("QC Code").Value) > 0 Then
                                Dim obj As New clsItemPurchaseQCParameter()
                                obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item Code").Value)
                                If clsCommon.myLen(obj.Item_Code) > 0 Then
                                    obj.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER Where Item_Code='" + obj.Item_Code + "'", trans))
                                    If clsCommon.myLen(obj.Item_Code) <= 0 Then
                                        Throw New Exception("The Item '" + clsCommon.myCstr(gv1.Rows(i).Cells("Item Code").Value) + "' at Line No '" + LineNo + "' Does Not Exist")

                                    End If
                                End If

                                obj.QC_Code = clsCommon.myCstr(gv1.Rows(i).Cells("QC Code").Value)
                                If clsCommon.myLen(obj.QC_Code) > 0 Then
                                    obj.QC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_QC_LOG_SHEET_MASTER where Code = '" + obj.QC_Code + "'", trans))
                                    If clsCommon.myLen(obj.QC_Code) <= 0 Then
                                        Throw New Exception("The QC Code '" + clsCommon.myCstr(gv1.Rows(i).Cells("QC Code").Value) + "' at Line No '" + LineNo + "' Does Not Exist")
                                    End If
                                End If
                                obj.SNo = clsCommon.myCstr(gv1.Rows(i).Cells("SNo").Value)
                                obj.Specifications = clsCommon.myCstr(gv1.Rows(i).Cells("Specifications").Value)

                                If Not arr.ContainsKey(obj.Item_Code) Then
                                    arr.Add(obj.Item_Code, New List(Of clsItemPurchaseQCParameter))
                                End If
                                arr(obj.Item_Code).Add(obj)
                            End If
                        Next

                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each key As String In arr.Keys
                                clsItemPurchaseQCParameter.SaveData(key, arr(key), trans)
                            Next
                            trans.Commit()
                            clsCommon.ProgressBarHide()
                            RadMessageBox.Show("Data Imported Successfully ...")
                        Else
                            Throw New Exception("Error in Import")
                        End If
                    Catch ex As Exception
                        clsCommon.ProgressBarHide()
                        trans.Rollback()
                        RadMessageBox.Show("Error at Line No " + LineNo + Environment.NewLine + ex.Message)
                    Finally
                        Me.Controls.Remove(gv1)
                    End Try
                End If
            Else
                Throw New Exception("This is Not for you")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub
    Private Sub chkInsurance_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInsurance.ToggleStateChanged
        If chkInsurance.Checked = False Then
            txtFromDate.Value = clsCommon.GETSERVERDATE
            txtToDate.Value = clsCommon.GETSERVERDATE
            txtInsurance.Text = ""
            RadGroupBoxInsurance.Enabled = False
        Else
            RadGroupBoxInsurance.Enabled = True
        End If
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = clsItemMaster.getFinderForActiveAndIncative("", txtCode.Value, True) 'Item_Type in ('R','O','A','F')
            LoadData(txtCode.Value, NavigatorType.Current)
            txtCode.Value = ""
            txtDesc.Text = ""
            txtShortDescription.Text = ""
            txtAliesName.Text = ""
            txtAliesName2.Text = ""
            txtAliesName3.Text = ""
            If AllowFinishGoodAsBatchItem AndAlso clsCommon.CompairString(cboItemType.SelectedValue, "F") = CompairStringResult.Equal Then
                chkIsReqBatch.Checked = True
            Else
                chkIsReqBatch.Checked = False
            End If
            isNewEntry = True
            btnDelete.Enabled = False
            btnSave.Text = "Save"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridSchedule()
        gvSchedule.Rows.Clear()
        gvSchedule.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColScheduleSNo
        repoNumBox.Minimum = 0
        repoNumBox.Width = 100
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.DecimalPlaces = 0
        repoNumBox.FormatString = "{0:n0}"
        repoNumBox.ReadOnly = True
        gvSchedule.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Days"
        repoNumBox.Name = ColScheduleDays
        repoNumBox.Minimum = 0
        repoNumBox.Width = 100
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.DecimalPlaces = 0
        repoNumBox.FormatString = "{0:n0}"
        gvSchedule.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Qty %"
        repoNumBox.Name = ColSchedulePerQty
        repoNumBox.Minimum = 0
        repoNumBox.Width = 100
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.DecimalPlaces = 0
        repoNumBox.FormatString = "{0:n0}"
        gvSchedule.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Short %"
        repoNumBox.Name = ColSchedulePerShort
        repoNumBox.Minimum = 0
        repoNumBox.Width = 100
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.DecimalPlaces = 0
        repoNumBox.FormatString = "{0:n0}"
        gvSchedule.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Late Days"
        repoNumBox.Name = ColScheduleLateDays
        repoNumBox.Minimum = 0
        repoNumBox.Width = 100
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.DecimalPlaces = 0
        repoNumBox.FormatString = "{0:n0}"
        gvSchedule.MasterTemplate.Columns.Add(repoNumBox)


        gvSchedule.AllowAddNewRow = False
        gvSchedule.ShowGroupPanel = False
        gvSchedule.AllowColumnReorder = False
        gvSchedule.AllowRowReorder = True
        gvSchedule.AllowDeleteRow = True
        gvSchedule.AllowEditRow = True
        gvSchedule.EnableSorting = False
        gvSchedule.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSchedule.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Private Sub gvSchedule_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvSchedule.CurrentColumnChanged
        If gvSchedule.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSchedule.CurrentRow.Index
            gvSchedule.CurrentRow.Cells(ColScheduleSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvSchedule.Rows.Count - 1 Then
                gvSchedule.Rows.AddNew()
                gvSchedule.CurrentRow = gvSchedule.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gvSchedule_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvSchedule.CellValidated
        Try
            SetScheduleGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetScheduleGridFocus()
        If gvSchedule.CurrentCell IsNot Nothing Then
            If gvSchedule.CurrentCell.ColumnInfo.Name = ColScheduleDays Then
                gvSchedule.CurrentColumn = gvSchedule.Columns(ColSchedulePerQty)
            ElseIf (gvSchedule.CurrentCell.ColumnInfo.Name = ColSchedulePerQty) Then
                gvSchedule.CurrentColumn = gvSchedule.Columns(ColSchedulePerShort)
            ElseIf (gvSchedule.CurrentCell.ColumnInfo.Name = ColSchedulePerShort) Then
                gvSchedule.CurrentColumn = gvSchedule.Columns(ColScheduleLateDays)
            ElseIf gvSchedule.CurrentCell.ColumnInfo.Name = ColScheduleLateDays Then
                If gvSchedule.Rows.Count >= gvSchedule.CurrentRow.Index + 1 Then
                    gvSchedule.CurrentRow = gvSchedule.Rows(gvSchedule.CurrentRow.Index + 1)
                End If
                gvSchedule.CurrentColumn = gvSchedule.Columns(ColScheduleDays)
            End If
        End If
    End Sub
    Private Sub gvSchedule_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvSchedule.UserDeletedRow
        RefeshScheduleSNO()
    End Sub
    Private Sub gvSchedule_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvSchedule.UserDeletingRow
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Sub RefeshScheduleSNO()
        For ii As Integer = 1 To gvSchedule.Rows.Count
            gvSchedule.Rows(ii - 1).Cells(ColScheduleSNo).Value = ii
        Next
    End Sub
    Private Sub gvSchedule_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSchedule.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSchedule.Columns(ColScheduleLateDays) Then
                        ShowPenalty()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub ShowPenalty()
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Days", GetType(Integer))
            dt.Columns.Add("Penalty", GetType(Decimal))

            Dim arr As List(Of clsItemSchedulePenalty) = TryCast(gvSchedule.CurrentRow.Cells(ColScheduleLateDays).Tag, List(Of clsItemSchedulePenalty))
            If arr Is Nothing OrElse arr.Count <= 0 Then
                For ii As Integer = 0 To clsCommon.myCDecimal(gvSchedule.CurrentRow.Cells(ColScheduleLateDays).Value) - 1
                    Dim dr As DataRow = dt.NewRow
                    dr("Days") = ii + 1
                    dr("Penalty") = 0
                    dt.Rows.Add(dr)
                Next
            Else
                For ii As Integer = 0 To clsCommon.myCDecimal(gvSchedule.CurrentRow.Cells(ColScheduleLateDays).Value) - 1
                    If arr.Count < ii Then
                        Dim dr As DataRow = dt.NewRow
                        dr("Days") = ii + 1
                        dr("Penalty") = 0
                        dt.Rows.Add(dr)
                    Else
                        Dim dr As DataRow = dt.NewRow
                        dr("Days") = ii + 1
                        dr("Penalty") = arr(ii).Penalty
                        dt.Rows.Add(dr)
                    End If
                Next
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmFreeGrid
                frm.dt = dt
                frm.arrEditableColumn = New List(Of String)
                frm.arrEditableColumn.Add("Penalty")
                frm.strFormName = "Set Penalty"
                frm.ReportID = "SchedulePenalty"
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
                If frm.dt IsNot Nothing AndAlso frm.dt.Rows.Count > 0 Then
                    Dim ArrTemp As New List(Of clsItemSchedulePenalty)
                    Dim obj As clsItemSchedulePenalty = Nothing
                    For Each dr As DataRow In frm.dt.Rows
                        obj = New clsItemSchedulePenalty()
                        obj.Penalty_Days = clsCommon.myCDecimal(dr("Days"))
                        obj.Penalty = clsCommon.myCDecimal(dr("Penalty"))
                        ArrTemp.Add(obj)
                    Next
                    gvSchedule.CurrentRow.Cells(ColScheduleLateDays).Tag = ArrTemp
                Else
                    gvSchedule.CurrentRow.Cells(ColScheduleLateDays).Tag = Nothing
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub gvSchedule_KeyDown(sender As Object, e As KeyEventArgs) Handles gvSchedule.KeyDown
        If e.KeyCode = Keys.Enter Then
            gvSchedule.BeginEdit()
        ElseIf e.KeyCode = Keys.F5 Then
            ShowPenalty()
        End If
    End Sub
    Private Sub ShowRemarks()
        Try
            Dim Reason As String = ""
            Dim frm As New FrmFreeTxtBox1
            frm.Text = "Remarks for Update"
            frm.ShowDialog()
            If clsCommon.myLen(frm.strRmks) <= 0 Then
                Exit Sub
            Else
                Reason = frm.strRmks
            End If
            Savedata()
            saveCancelLog(Reason, "Updated", Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnBBNA_Click(sender As Object, e As EventArgs) Handles rbtnBBNA.Click
        lblBBValue.Visible = False
        txtBBValue.Visible = False
    End Sub

    Private Sub rbtnBBAmount_Click(sender As Object, e As EventArgs) Handles rbtnBBAmount.Click
        lblBBValue.Visible = True
        lblBBValue.Text = "Amount"
        txtBBValue.Visible = True
    End Sub

    Private Sub rbtnBBPer_Click(sender As Object, e As EventArgs) Handles rbtnBBPer.Click
        lblBBValue.Visible = True
        lblBBValue.Text = "Percentage"
        txtBBValue.Visible = True
    End Sub

    Private Sub chkFGforCF_CheckStateChanged(sender As Object, e As EventArgs) Handles chkFGforCF.CheckStateChanged
        If chkFGforCF.Checked = True Then
            txtBmBdQty.Visible = True
            MyLabelL.Visible = True
        Else
            txtBmBdQty.Visible = False
            MyLabelL.Visible = False

        End If
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
End Class
