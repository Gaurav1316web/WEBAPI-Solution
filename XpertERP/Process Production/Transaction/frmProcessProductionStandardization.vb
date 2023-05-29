'----------------------created by Panch Raj 25/08/2014--BM00000003355----------BM00000003794----BM00000004866
Imports common
Imports System.Data.SqlClient

Public Class frmProcessProductionStandardization
    Inherits FrmMainTranScreen

#Region "Variables"
    Public activateSFGProduction As Boolean = False
    Public ShowOnlyProdItemsOnAddRemove As Boolean = False
    Public AutoCalcQtyAddRem As Boolean = False
    Dim ProductionOrStandAccordingToItemType As Integer = 0
    Dim OpenAvailorEmptyStckLocationOn_Standardization As Boolean = False
    Public strDocumentCode As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry As String = ""
    Dim check As Integer = 0
    Dim isNewEntry As Boolean = Nothing
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim DecimalPointQty As Integer = 3
    Dim DecimalPointFatSNFPer As Integer = 3
    Dim RunBatchFifowise As Boolean = False

    '' Batch Production Columns
    Const colSno As String = "SNO"
    Const colBOM_Code As String = "colBOM_Code"
    Const colBOM_Desc As String = "colBOM_Desc"
    Const colitemcode As String = "colitemcode"
    Const colitemname As String = "itemname"
    Const colitemtype As String = "itemtype"
    Const colitemproducttype As String = "producttype"
    Const coluom As String = "UOM"
    Const colUOMDesc As String = "UOM_Desc"
    Const colQuantity As String = "colQuantity"
    Const colShift_Code As String = "colShift_Code"
    Const colShift_Desc As String = "colShift_Desc"
    Const colSection_Code As String = "colSection_Code"
    Const colSection_Desc As String = "colSection_Desc"

    Const colRequir_FAT_per As String = "colRequir_FAT_per"
    Const colRequir_FAT_KG As String = "colRequir_FAT_KG"
    Const colRequir_SNF_per As String = "colRequir_SNF_per"
    Const colRequir_SNF_KG As String = "colRequir_SNF_KG"
    Const colProduced_FAT_per As String = "colProduced_FAT_per"
    Const colProduced_SNF_per As String = "colProduced_SNF_per"
    Const colProduced_Qty As String = "colProduced_Qty"
    Const colProduced_FAT_KG As String = "colProduced_FAT_KG"
    Const colProduced_SNF_KG As String = "colProduced_SNF_KG"
    Const colSTD_Loaction_Code As String = "colSTD_Loaction_Code"
    Const colSTD_Loaction_Desc As String = "colSTD_Loaction_Desc"

    Const colIssueSno As String = "colIssueSno"
    Const colIssueItemCode As String = "colIssueItemCode"
    Const colIssueItemName As String = "colIssueItemName"
    Const colIssueItemType As String = "colIssueItemType"
    Const colIssueItemProductType As String = "colIssueItemProductType"
    Const colIssueUom As String = "colIssueUom"
    Const colIssueUOMDesc As String = "colIssueUOMDesc"
    Const colIssued_Qty As String = "colIssued_Qty"

    Const colIssued_FAT_Per As String = "colIssued_FAT_Per"
    Const colIssued_FAT_KG As String = "colIssued_FAT_KG"
    Const colIssued_SNF_Per As String = "colIssued_SNF_Per"
    Const colIssued_SNF_KG As String = "colIssued_SNF_KG"


    Const colDiff_FAT_Per As String = "colDiff_FAT_Per"
    Const colDiff_SNF_Per As String = "colDiff_SNF_Per"

    Const colIssueRequir_FAT_Per As String = "colIssueRequir_FAT_Per"
    Const colIssueRequir_SNF_Per As String = "colIssueRequir_SNF_Per"

    Const colDiff_FAT_KG As String = "colDiff_FAT_KG"
    Const colDiff_SNF_KG As String = "colDiff_SNF_KG"
    Const colIssueRemarks As String = "colIssueRemarks"
    Const colProducedItem As String = "colProducedItem"
    Const colTO_LOC_CODE As String = "colTO_LOC_CODE"
    Const colTO_LOC_DESC As String = "colTO_LOC_DESC"
    Const colIssueStatus As String = "colIssueStatus"

    Const colIssue_Fat_Rate As String = "colIssue_Fat_Rate"
    Const colIssue_SNF_Rate As String = "colIssue_SNF_Rate"
    Const colIssue_Fat_Amt As String = "colIssue_Fat_Amt"
    Const colIssue_SNF_Amt As String = "colIssue_SNF_Amt"

    '' add/remove tab columns

    Const colARSno As String = "colARSno"
    Const colARItemCode As String = "colARItemCode"
    Const colARItemName As String = "colARItemName"
    Const colARItemType As String = "colARItemType"
    Const colARItemProductType As String = "colARItemProductType"
    Const colARIsBatchItem As String = "colIsBatchItem"
    Const colARUom As String = "colARUom"
    Const colARUOMDesc As String = "colARUOMDesc"
    Const colARAvailQty As String = "colARAvailQty"
    Const colARQty As String = "colARQty"
    Const colARType As String = "colARType"
    Const colARLocCode As String = "colARLocCode"
    Const colARLocDesc As String = "colARLocDesc"
    Const colARRemarks As String = "colARRemarks"

    Const colAR_FAT_Per As String = "colAR_FAT_Per"
    Const colAR_FAT_KG As String = "colAR_FAT_KG"
    Const colAR_SNF_Per As String = "colAR_SNF_Per"
    Const colAR_SNF_KG As String = "colAR_SNF_KG"


    '' QC tab columns
    Const colQCSno As String = "colQCSno"
    Const colQCType As String = "colQCType" 'Batch or Add/Remove
    Const colQCItemCode As String = "colQCItemCode"
    Const colQCItemName As String = "colQCItemName"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCParentLineNo As String = "colQCParentLineNo"
    'Const colQCrange2 As String = "range2"
    Const colQCBooleanStatus As String = "colQCBooleanStatus"
    Const colQCAlphaValue As String = "colQCAlphaValue"
    Const colActual_Range As String = "colActual_Range"
    Const colActual_Status As String = "colActual_Status"
    Const colActual_Value As String = "colActual_Value"
    Const colQc_Status As String = "colQc_Status"
    Const colQCremarks As String = "colQCremarks"

    '' stage detail tab
    Const colStageSno As String = "colStageSno"
    Const colStage_Code As String = "colStage_Code"
    Const colStage_Desc As String = "colStage_Desc"
    Const colReceived_Qty As String = "colReceived_Qty"
    Const colUnit_Code As String = "colUnit_Code"
    Const colSPUnit_Desc As String = "colSPUnit_Desc"
    Const colLog_Sheet_No As String = "colLog_Sheet_No"
    Const colStatus As String = "colStatus"
    Const colSPRemarks As String = "colSPRemarks"
    Const colSPProdCategory As String = "colSPProdCategory"
    Const colSPSection As String = "colSPSection"
    Const colStageBatch_Code As String = "colStageBatch_Code"
    Public CheckStockServerDate As Boolean = True
    Private settAllowNegativeStockInDairyProduction As Boolean = False
    Private SettUseProductFATSNFKgForEstimationCost As Boolean = False
    'Public objList As List(Of clsPPStageProcessLogSheetDetail) = New List(Of clsPPStageProcessLogSheetDetail)
    Dim settTankerDispatchAvgFATSNFPer As Boolean
    Dim arrLoc As String = Nothing
    Dim UseProductionPlaningDateForWholeProductionCycle As Boolean = False
    Dim settProductionRemoveFATSNFKgTollerance As Integer = 0
    Dim settCheckNetFatKg As Integer = 0
    Dim settCheckNetSNFKg As Integer = 0
#End Region

    Private Sub frmProcessProductionStandardization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, Nothing)) = 1)
        settProductionRemoveFATSNFKgTollerance = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionRemoveFATSNFKgTollerance, clsFixedParameterCode.ProductionRemoveFATSNFKgTollerance, Nothing))
        settCheckNetFatKg = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionCheckFATKg, clsFixedParameterCode.ProductionCheckFATKg, Nothing))
        settCheckNetSNFKg = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionCheckSNFKg, clsFixedParameterCode.ProductionCheckSNFKg, Nothing))
        RunBatchFifowise = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing)) = 1)
        settAllowNegativeStockInDairyProduction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, Nothing)) > 0)
        ProductionOrStandAccordingToItemType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionOrStandAccordingToItemType, clsFixedParameterCode.ProductionOrStandAccordingToItemType, Nothing))
        SettUseProductFATSNFKgForEstimationCost = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseProductFATSNFKgForEstimationCost, clsFixedParameterCode.UseProductFATSNFKgForEstimationCost, Nothing)) > 0)
        SetUserMgmtNew()
        '' done by Panch Raj on 09-07-2019 against ticket no:ERO/12/06/18-000342
        activateSFGProduction = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, Nothing), "1") = CompairStringResult.Equal, True, False)
        ShowOnlyProdItemsOnAddRemove = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowOnlyProductionItemInAdRemove, clsFixedParameterCode.ShowOnlyProductionItemInAdRemove, Nothing), "1") = CompairStringResult.Equal, True, False)
        AutoCalcQtyAddRem = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoCalculateADDREMOVEQty, clsFixedParameterCode.AllowAutoCalculateADDREMOVEQty, Nothing), "1") = CompairStringResult.Equal, True, False)
        UseProductionPlaningDateForWholeProductionCycle = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseProductionPlaningDateForWholeProductionCycle, clsFixedParameterCode.UseProductionPlaningDateForWholeProductionCycle, Nothing)) = 1, True, False)
        OpenAvailorEmptyStckLocationOn_Standardization = IIf(clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.OpenAvailorEmptyStckLocationOn_Standardization, clsFixedParameterCode.OpenAvailorEmptyStckLocationOn_Standardization, Nothing)), "1") = CompairStringResult.Equal, True, False)
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
            CheckStockServerDate = True
        Else
            CheckStockServerDate = False
        End If

        '' get decimal point for qty
        DecimalPointQty = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPointQty <= 0 Then
            DecimalPointQty = 3
        End If
        '' get decimal point for fat snf percentage
        DecimalPointFatSNFPer = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNFPerDecimalPoint, clsFixedParameterCode.ProductionFATSNFPerDecimalPoint, Nothing)))
        If DecimalPointFatSNFPer <= 0 Then
            DecimalPointFatSNFPer = 3
        End If
        gvSectionStock.AutoGenerateColumns = True
        gvSectionStockHistory.AutoGenerateColumns = True
        gvSectionStock.ReadOnly = True
        gvSectionStockHistory.ReadOnly = True
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for deleting data")
        ButtonToolTip.SetToolTip(btnPost, "Alt+P for posting data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for window close")
        ButtonToolTip.SetToolTip(btngo, "Alt+G for QC detail filling")


        If strDocumentCode IsNot Nothing AndAlso clsCommon.myLen(strDocumentCode) > 0 Then
            LoadData(strDocumentCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadSPBlankGrid()
        gvStage.Rows.Clear()
        gvStage.Columns.Clear()

        Dim repoARSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSno.FormatString = ""
        repoARSno.Name = colStageSno
        repoARSno.Width = 60
        repoARSno.DecimalPlaces = 0
        repoARSno.HeaderText = "S.No."
        repoARSno.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(repoARSno)

        Dim StageCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StageCode.FormatString = ""
        StageCode.Name = colStage_Code
        StageCode.Width = 100
        StageCode.HeaderText = "Stage Code"
        StageCode.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(StageCode)

        Dim StageDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StageDesc.FormatString = ""
        StageDesc.Name = colStage_Desc
        StageDesc.Width = 100
        StageDesc.HeaderText = "Stage Description"
        StageDesc.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(StageDesc)


        Dim repoRecQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRecQty.FormatString = ""
        repoRecQty.Name = colReceived_Qty
        repoRecQty.Width = 120
        repoRecQty.HeaderText = "Received Quantity"
        repoRecQty.DecimalPlaces = DecimalPointQty
        repoRecQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoRecQty.ReadOnly = False
        gvStage.MasterTemplate.Columns.Add(repoRecQty)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colUnit_Code
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colSPUnit_Desc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        repouomname.IsVisible = False
        gvStage.MasterTemplate.Columns.Add(repouomname)

        Dim Log_Sheet_No As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Log_Sheet_No.FormatString = ""
        Log_Sheet_No.Name = colLog_Sheet_No
        Log_Sheet_No.Width = 120
        Log_Sheet_No.HeaderText = "Log Sheet No"
        Log_Sheet_No.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(Log_Sheet_No)

        Dim Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Status.FormatString = ""
        Status.Name = colStatus
        Status.Width = 120
        Status.HeaderText = "Status"
        Status.ReadOnly = False
        Status.DataSource = clsProcessProductionStandardization.GetStageQCStatus()
        Status.ValueMember = "Code"
        Status.DisplayMember = "Name"
        gvStage.MasterTemplate.Columns.Add(Status)

        Dim SPemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SPemarks.FormatString = ""
        SPemarks.Name = colSPRemarks
        SPemarks.Width = 120
        SPemarks.HeaderText = "Remarks"
        SPemarks.ReadOnly = False
        gvStage.MasterTemplate.Columns.Add(SPemarks)

        Dim SProdCat As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SProdCat.FormatString = ""
        SProdCat.Name = colSPProdCategory
        SProdCat.Width = 120
        SProdCat.HeaderText = "Production Category"
        SProdCat.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(SProdCat)

        Dim spSection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        spSection.FormatString = ""
        spSection.Name = colSPSection
        spSection.Width = 120
        spSection.HeaderText = "Section"
        spSection.ReadOnly = True
        gvStage.MasterTemplate.Columns.Add(spSection)

        Dim spBatchCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        spBatchCode.FormatString = ""
        spBatchCode.Name = colStageBatch_Code
        spBatchCode.Width = 120
        spBatchCode.HeaderText = "Batch Code"
        spBatchCode.ReadOnly = True
        spBatchCode.IsVisible = False
        gvStage.MasterTemplate.Columns.Add(spBatchCode)


        gvStage.AllowDeleteRow = True
        gvStage.AllowAddNewRow = False
        gvStage.ShowGroupPanel = False
        gvStage.AllowColumnReorder = False
        gvStage.AllowRowReorder = False
        gvStage.EnableSorting = False
        gvStage.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvStage.MasterTemplate.ShowRowHeaderColumn = False
        gvStage.EnableFiltering = False
        'gvARDetail.Rows.AddNew()

    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FunReset()
        txtCode.Value = ""
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
            CheckStockServerDate = True
        Else
            CheckStockServerDate = False
        End If
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        UsLock1.Status = ERPTransactionStatus.Pending
        txtlocation.Text = ""
        txtlocationname.Text = ""
        TxtManualBatchNo.Text = ""
        lblLineNo.Text = ""
        LblCostCenterCode.Text = ""
        lblCostCenterName.Text = ""
        lblProfitCenterCode.Text = ""
        lblProfitCenterName.Text = ""
        fndChildBatchNo.Value = ""
        lblChildBatchDesc.Text = ""
        fndMainBatchNo.Value = ""
        lblMainBatchDesc.Text = ""
        fndChildBatchNo.Value = Nothing
        fndMainBatchNo.Value = Nothing
        lblConsmSectionCode.Text = ""
        lblConsmSectionLocCode.Text = ""
        chkJobWorkInward.Checked = False
        lblTotBatchQty.Text = ""
        lblTotBatchFATKG.Text = ""
        lblTotBatchSNFKG.Text = ""
        lblTotProduceQty.Text = ""
        lblTotProduceFATKG.Text = ""
        lblTotProduceSNFKG.Text = ""
        lblTotIssueQty.Text = ""
        lblTotIssueFATKG.Text = ""
        lblTotIssueSNFKG.Text = ""
        lblAvgRateFAT.Text = ""
        lblAvgRateSNF.Text = ""
        lblTotDifferenceQty.Text = ""
        lblTotDifferenceFATKG.Text = ""
        lblTotDifferenceSNFKG.Text = ""

        lblTotAddedQty.Text = ""
        lblTotAddedFATKG.Text = ""
        lblTotAddedSNFKG.Text = ""

        lblTotRemovedQty.Text = ""
        lblTotRemovedFATKG.Text = ""
        lblTotRemovedSNFKG.Text = ""

        lblTotAddRemoveQty.Text = ""
        lblTotAddRemoveFATKG.Text = ""
        lblTotAddRemoveSNFKG.Text = ""
        lblTotNetQty.Text = ""
        lblTotNetFATKG.Text = ""
        lblTotNetSNFKG.Text = ""
        lblJWEFATKg.Text = ""
        lblJWEFATAmt.Text = ""
        lblJWESNFKg.Text = ""
        lblJWESNFAmt.Text = ""
        LoadBlankGrid()
        LoadBlankIssueGrid()
        LoadARBlankGrid()
        LoadQCBlankGrid()
        LoadSPBlankGrid()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        'fndChildBatchNo.Enabled = True
        'fndChildBatchNo.Enabled = True
        fndMainBatchNo.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnCancel.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        RadPageView1.SelectedPage = pageBatchDetail
        txtCode.Focus()
        txtCode.Select()
        isNewEntry = True
        LOCATIONRIGTHS()
        DisableAllTabPages()
    End Sub

    Sub DisableAllTabPages()
        For Each page As RadPageViewPage In RadPageView1.Pages
            page.Enabled = False
        Next
        pageAttachment.Enabled = True
        pageSectionStock.Enabled = True
        pageSectionStockHistory.Enabled = True
        RadPageViewPage1.Enabled = True
    End Sub

    Sub EnableAllTabPages()
        For Each page As RadPageViewPage In RadPageView1.Pages
            page.Enabled = True
        Next
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProcessProductionStandardization)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        '  btnPrint.Enabled = MyBase.isPrintFlag
    End Sub

    Private Sub frmProcessProductionStandardization_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            If AllowToSave() Then SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.G Then
            btngo.PerformClick()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isReverse AndAlso btnunpost.Visible Then
            '    btnunpost.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                     "TSPL_PP_STANDARDIZATION_HEAD " + Environment.NewLine + _
                                     "TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_STD_ISSUE_ITEM_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_STD_QC_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_STD_STAGE_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_STD_QC_LOG_SHEET " + Environment.NewLine + _
                                     "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
                                     "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " + Environment.NewLine + _
                                     "TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL " + Environment.NewLine + _
                                     "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine + _
                                     "TSPL_SERIAL_ITEM " + Environment.NewLine + _
                                     "TSPL_BATCH_ITEM " + Environment.NewLine + _
                                     "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine + _
                                     "TSPL_JOURNAL_MASTER ")
            If btnunpost.Visible Then
                btnunpost.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnunpost.Visible = True
                End If
            End If
        End If

        If e.KeyData = Keys.F2 AndAlso gvARDetail.CurrentColumn IsNot Nothing AndAlso gvARDetail.CurrentColumn Is gvARDetail.Columns(colARUom) Then
            isCellValueChanged = True
            OpenUOM(True)
            isCellValueChanged = False
        End If
        If e.KeyData = Keys.F2 AndAlso gvARDetail.CurrentColumn IsNot Nothing AndAlso gvARDetail.CurrentColumn Is gvARDetail.Columns(colARItemCode) Then
            isCellValueChanged = True
            gvARDetail.CurrentRow.Cells(colARItemCode).Value = clsItemMaster.getFinder("", gvARDetail.CurrentRow.Cells(colARItemCode).Value, True)
            Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, NavigatorType.Current)
            If Not objItem Is Nothing Then
                gvARDetail.CurrentRow.Cells(colARItemName).Value = objItem.Item_Desc
                gvARDetail.CurrentRow.Cells(colARItemType).Value = objItem.Item_Type
                gvARDetail.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                gvARDetail.CurrentRow.Cells(colARUom).Value = objItem.Unit_Code
                gvARDetail.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
            End If
            isCellValueChanged = False
        End If

        If e.KeyData = Keys.F2 AndAlso gvARDetail.CurrentColumn IsNot Nothing AndAlso gvARDetail.CurrentColumn Is gvARDetail.Columns(colARLocCode) Then
            isCellValueChanged = True
            OpenARLocationCode(True)
            isCellValueChanged = False
        End If
        If e.KeyData = (Keys.Control + Keys.Right) Then
            If Not RadPageView1.SelectedPage Is Nothing Then
                RadPageView1.SelectedPage = RadPageView1.Pages(RadPageView1.Pages.IndexOf(RadPageView1.SelectedPage) Mod (RadPageView1.Pages.Count - 1))
            End If
        ElseIf e.KeyData = (Keys.Control + Keys.I) Then
            RadPageView1.SelectedPage = pageIssueDetail
        ElseIf e.KeyData = (Keys.Control + Keys.A) Then
            RadPageView1.SelectedPage = pageAddRemoveDetail
        ElseIf e.KeyData = (Keys.Control + Keys.A + Keys.T) Then
            RadPageView1.SelectedPage = pageAttachment
        ElseIf e.KeyData = (Keys.Control + Keys.B) Then
            RadPageView1.SelectedPage = pageBatchDetail
            'ElseIf e.KeyData = (Keys.Control + Keys.B + Keys.R) Then
            '    RadPageView1.SelectedPage = PageBreakMode
        ElseIf e.KeyData = (Keys.Control + Keys.P) Then
            RadPageView1.SelectedPage = pageParameterDetail
        ElseIf e.KeyData = (Keys.Control + Keys.S) Then
            RadPageView1.SelectedPage = pageStageDetail
        ElseIf e.KeyData = (Keys.Control + Keys.E) Then
            RadPageView1.SelectedPage = pageSectionStock
        ElseIf e.KeyData = (Keys.Control + Keys.H) Then
            RadPageView1.SelectedPage = pageSectionStockHistory
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colSno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposno)

        Dim repobomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomcode.FormatString = ""
        repobomcode.Name = colBOM_Code
        repobomcode.Width = 100
        repobomcode.HeaderText = "BOM Code"
        repobomcode.ReadOnly = True
        repobomcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repobomcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repobomcode)

        Dim repoBOMDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBOMDesc.FormatString = ""
        repoBOMDesc.Name = colBOM_Desc
        repoBOMDesc.Width = 150
        repoBOMDesc.HeaderText = "BOM Description"
        repoBOMDesc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBOMDesc)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colitemcode
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colitemname
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoiname)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colitemtype
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoitype)

        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colitemproducttype
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = coluom
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colUOMDesc
        repouomname.Width = 150
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repouomname)

        Dim repoShiftCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftCode.FormatString = ""
        repoShiftCode.Name = colShift_Code
        repoShiftCode.Width = 100
        repoShiftCode.HeaderText = "Shift Code"
        repoShiftCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoShiftCode)

        Dim repoShiftDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftDesc.FormatString = ""
        repoShiftDesc.Name = colShift_Desc
        repoShiftDesc.Width = 150
        repoShiftDesc.HeaderText = "Shift Description"
        repoShiftDesc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoShiftDesc)

        Dim repoSection_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSection_Code.FormatString = ""
        repoSection_Code.Name = colSection_Code
        repoSection_Code.Width = 100
        repoSection_Code.HeaderText = "Section Code"
        repoSection_Code.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSection_Code)

        Dim repoSection_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSection_Desc.FormatString = ""
        repoSection_Desc.Name = colSection_Desc
        repoSection_Desc.Width = 150
        repoSection_Desc.HeaderText = "Section Description"
        repoSection_Desc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSection_Desc)

        Dim repoQuantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQuantity.FormatString = ""
        repoQuantity.Name = colQuantity
        repoQuantity.Width = 100
        repoQuantity.HeaderText = "Quantity"
        repoQuantity.DecimalPlaces = DecimalPointQty
        repoQuantity.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoQuantity.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoQuantity)

        Dim repoRequirFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirFat.FormatString = ""
        repoRequirFat.Name = colRequir_FAT_per
        repoRequirFat.Width = 100
        repoRequirFat.HeaderText = "Required FAT%"
        repoRequirFat.DecimalPlaces = DecimalPointFatSNFPer
        repoRequirFat.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoRequirFat.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRequirFat)

        Dim repoRequirFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirFatKG.FormatString = ""
        repoRequirFatKG.Name = colRequir_FAT_KG
        repoRequirFatKG.Width = 150
        repoRequirFatKG.HeaderText = "Required FAT KG"
        repoRequirFatKG.DecimalPlaces = DecimalPointQty
        repoRequirFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoRequirFatKG.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRequirFatKG)

        Dim repoRequirSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirSnf.FormatString = ""
        repoRequirSnf.Name = colRequir_SNF_per
        repoRequirSnf.Width = 150
        repoRequirSnf.HeaderText = "Required SNF%"
        repoRequirSnf.DecimalPlaces = DecimalPointFatSNFPer
        repoRequirSnf.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoRequirSnf.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRequirSnf)

        Dim repoRequirSnfKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirSnfKG.FormatString = ""
        repoRequirSnfKG.Name = colRequir_SNF_KG
        repoRequirSnfKG.Width = 150
        repoRequirSnfKG.HeaderText = "Required SNF KG"
        repoRequirSnfKG.DecimalPlaces = DecimalPointQty
        repoRequirSnfKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoRequirSnfKG.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoRequirSnfKG)

        Dim repoProduced_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_Qty.FormatString = ""
        repoProduced_Qty.Name = colProduced_Qty
        repoProduced_Qty.Width = 150
        repoProduced_Qty.HeaderText = "Produced Quantity"
        repoProduced_Qty.DecimalPlaces = DecimalPointQty
        repoProduced_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        gv.MasterTemplate.Columns.Add(repoProduced_Qty)

        Dim repoProducedFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProducedFat.FormatString = ""
        repoProducedFat.Name = colProduced_FAT_per
        repoProducedFat.Width = 100
        repoProducedFat.HeaderText = "Produced FAT%"
        repoProducedFat.DecimalPlaces = DecimalPointFatSNFPer
        repoProducedFat.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoProducedFat.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoProducedFat)

        Dim repoProducedSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProducedSnf.FormatString = ""
        repoProducedSnf.Name = colProduced_SNF_per
        repoProducedSnf.Width = 150
        repoProducedSnf.HeaderText = "Produced SNF%"
        repoProducedSnf.DecimalPlaces = DecimalPointFatSNFPer
        repoProducedSnf.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoProducedSnf.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoProducedSnf)


        'Dim repoSampleNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoSampleNo.FormatString = ""
        'repoSampleNo.Name = colNO_SAMPLE_QC
        'repoSampleNo.Width = 100
        'repoSampleNo.HeaderText = "No Of Sample"
        'repoSampleNo.DecimalPlaces = 0
        'gv.MasterTemplate.Columns.Add(repoSampleNo)

        'Dim repoDamagedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDamagedQty.FormatString = ""
        'repoDamagedQty.Name = colDAMAGE_Qty
        'repoDamagedQty.Width = 100
        'repoDamagedQty.HeaderText = "Damage Quantity"
        'repoDamagedQty.DecimalPlaces = 3
        'gv.MasterTemplate.Columns.Add(repoDamagedQty)

        'Dim repoFinalProdQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoFinalProdQty.FormatString = ""
        'repoFinalProdQty.Name = colFINAL_PROD_Qty
        'repoFinalProdQty.Width = 100
        'repoFinalProdQty.HeaderText = "Final Production Qty"
        'repoFinalProdQty.DecimalPlaces = 3
        'repoFinalProdQty.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoFinalProdQty)

        Dim repoProduced_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_FAT_KG.FormatString = ""
        repoProduced_FAT_KG.Name = colProduced_FAT_KG
        repoProduced_FAT_KG.Width = 150
        repoProduced_FAT_KG.HeaderText = "Produced FAT KG"
        repoProduced_FAT_KG.DecimalPlaces = DecimalPointQty
        repoProduced_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoProduced_FAT_KG.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoProduced_FAT_KG)

        Dim repoProduced_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_SNF_KG.FormatString = ""
        repoProduced_SNF_KG.Name = colProduced_SNF_KG
        repoProduced_SNF_KG.Width = 150
        repoProduced_SNF_KG.HeaderText = "Produced SNF KG"
        repoProduced_SNF_KG.DecimalPlaces = DecimalPointQty
        repoProduced_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoProduced_SNF_KG.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoProduced_SNF_KG)

        Dim repoSTD_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSTD_Location_Code.FormatString = ""
        repoSTD_Location_Code.Name = colSTD_Loaction_Code
        repoSTD_Location_Code.Width = 100
        repoSTD_Location_Code.HeaderText = "Location Code"
        repoSTD_Location_Code.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoSTD_Location_Code)

        Dim repoSTD_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSTD_Location_Desc.FormatString = ""
        repoSTD_Location_Desc.Name = colSTD_Loaction_Desc
        repoSTD_Location_Desc.Width = 150
        repoSTD_Location_Desc.HeaderText = "Location Description"
        repoSTD_Location_Desc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSTD_Location_Desc)
        '-------------------------------------------------

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.EnableFiltering = False
        gv.Rows.AddNew()
    End Sub

    Private Sub LoadBlankIssueGrid()
        gvIssue.Rows.Clear()
        gvIssue.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colIssueSno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(reposno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colIssueItemCode
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colIssueItemName
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoiname)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colIssueItemType
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoitype)

        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colIssueItemProductType
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoPtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colIssueUom
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colIssueUOMDesc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repouomname)

        Dim repoAvail_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_Qty.FormatString = ""
        repoAvail_Qty.Name = colIssued_Qty
        repoAvail_Qty.Width = 120
        repoAvail_Qty.HeaderText = "Issued Quantity"
        repoAvail_Qty.DecimalPlaces = DecimalPointQty
        repoAvail_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_Qty.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colIssued_FAT_Per
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Issued FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colIssued_FAT_KG
        repoAvail_FAT_KG.Width = 120
        repoAvail_FAT_KG.HeaderText = "Issued FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colIssued_SNF_Per
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "Issued SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colIssued_SNF_KG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "Issued SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_KG)

        repoAvail_FAT_Per = New GridViewDecimalColumn()
        repoAvail_FAT_Per.Name = colIssueRequir_FAT_Per
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Required FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        repoAvail_FAT_Per = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colIssueRequir_SNF_Per
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Required SNF%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        'Dim repoDiff_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_Qty.FormatString = ""
        'repoDiff_Qty.Name = colDiff_Qty
        'repoDiff_Qty.Width = 120
        'repoDiff_Qty.HeaderText = "Diff. Quantity"
        'repoDiff_Qty.DecimalPlaces = 3
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_Qty)

        Dim repoDiff_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_FAT_Per.FormatString = ""
        repoDiff_FAT_Per.Name = colDiff_FAT_Per
        repoDiff_FAT_Per.Width = 120
        repoDiff_FAT_Per.HeaderText = "Diff. FAT%"
        repoDiff_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoDiff_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoDiff_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_Per)

        Dim repoDiff_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_FAT_KG.FormatString = ""
        repoDiff_FAT_KG.Name = colDiff_FAT_KG
        repoDiff_FAT_KG.Width = 120
        repoDiff_FAT_KG.HeaderText = "Diff. FAT KG"
        repoDiff_FAT_KG.DecimalPlaces = DecimalPointQty
        repoDiff_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoDiff_FAT_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_KG)

        Dim repoDiff_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_SNF_Per.FormatString = ""
        repoDiff_SNF_Per.Name = colDiff_SNF_Per
        repoDiff_SNF_Per.Width = 120
        repoDiff_SNF_Per.HeaderText = "Diff. SNF%"
        repoDiff_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoDiff_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoDiff_SNF_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_Per)

        Dim repoDiff_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_SNF_KG.FormatString = ""
        repoDiff_SNF_KG.Name = colDiff_SNF_KG
        repoDiff_SNF_KG.Width = 120
        repoDiff_SNF_KG.HeaderText = "Diff. SNF KG"
        repoDiff_SNF_KG.DecimalPlaces = DecimalPointQty
        repoDiff_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoDiff_SNF_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_KG)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colIssueRemarks
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        gvIssue.MasterTemplate.Columns.Add(repoIssueRemarks)

        Dim repoTo_Loc_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTo_Loc_Code.FormatString = ""
        repoTo_Loc_Code.Name = colTO_LOC_CODE
        repoTo_Loc_Code.Width = 120
        repoTo_Loc_Code.MaxLength = 200
        repoTo_Loc_Code.HeaderText = "Location Code"
        repoTo_Loc_Code.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoTo_Loc_Code)

        Dim repoTo_Loc_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTo_Loc_Desc.FormatString = ""
        repoTo_Loc_Desc.Name = colTO_LOC_DESC
        repoTo_Loc_Desc.Width = 120
        repoTo_Loc_Desc.MaxLength = 200
        repoTo_Loc_Desc.HeaderText = "Location Desc"
        repoTo_Loc_Desc.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoTo_Loc_Desc)

        Dim Issue_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Issue_Status.FormatString = ""
        Issue_Status.Name = colIssueStatus
        Issue_Status.Width = 100
        Issue_Status.HeaderText = "Status"
        Issue_Status.ReadOnly = False
        Issue_Status.DataSource = GetIssueStatus()
        Issue_Status.ValueMember = "Code"
        Issue_Status.DisplayMember = "Name"
        gvIssue.MasterTemplate.Columns.Add(Issue_Status)

        '' production costing columns
        Dim repoIssue_Fat_Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIssue_Fat_Rate.FormatString = ""
        repoIssue_Fat_Rate.Name = colIssue_Fat_Rate
        repoIssue_Fat_Rate.Width = 100
        repoIssue_Fat_Rate.HeaderText = "Fat Rate"
        repoIssue_Fat_Rate.DecimalPlaces = DecimalPointQty
        repoIssue_Fat_Rate.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoIssue_Fat_Rate.ReadOnly = True
        repoIssue_Fat_Rate.IsVisible = False
        gvIssue.MasterTemplate.Columns.Add(repoIssue_Fat_Rate)

        '' production costing columns
        Dim repoIssue_SNF_Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIssue_SNF_Rate.FormatString = ""
        repoIssue_SNF_Rate.Name = colIssue_SNF_Rate
        repoIssue_SNF_Rate.Width = 100
        repoIssue_SNF_Rate.HeaderText = "SNF Rate"
        repoIssue_SNF_Rate.DecimalPlaces = DecimalPointQty
        repoIssue_SNF_Rate.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoIssue_SNF_Rate.ReadOnly = True
        repoIssue_SNF_Rate.IsVisible = False
        gvIssue.MasterTemplate.Columns.Add(repoIssue_SNF_Rate)

        Dim repoIssue_Fat_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIssue_Fat_Amt.FormatString = ""
        repoIssue_Fat_Amt.Name = colIssue_Fat_Amt
        repoIssue_Fat_Amt.Width = 100
        repoIssue_Fat_Amt.HeaderText = "Fat Amount"
        repoIssue_Fat_Amt.DecimalPlaces = DecimalPointQty
        repoIssue_Fat_Amt.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoIssue_Fat_Amt.ReadOnly = True
        repoIssue_Fat_Amt.IsVisible = False
        gvIssue.MasterTemplate.Columns.Add(repoIssue_Fat_Amt)

        '' production costing columns
        Dim repoIssue_SNF_Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIssue_SNF_Amt.FormatString = ""
        repoIssue_SNF_Amt.Name = colIssue_SNF_Amt
        repoIssue_SNF_Amt.Width = 100
        repoIssue_SNF_Amt.HeaderText = "SNF Amount"
        repoIssue_SNF_Amt.DecimalPlaces = DecimalPointQty
        repoIssue_SNF_Amt.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoIssue_SNF_Amt.ReadOnly = True
        repoIssue_SNF_Amt.IsVisible = False
        gvIssue.MasterTemplate.Columns.Add(repoIssue_SNF_Amt)

        'Dim repoProducedItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoProducedItem.FormatString = ""
        'repoProducedItem.Name = colProducedItem
        'repoProducedItem.Width = 120
        'repoProducedItem.HeaderText = "Produced Item"
        'repoProducedItem.ReadOnly = True
        'repoProducedItem.IsVisible = False
        'gvIssue.MasterTemplate.Columns.Add(repoProducedItem)
        '-------------------------------------------------

        gvIssue.AllowDeleteRow = True
        gvIssue.AllowAddNewRow = False
        gvIssue.ShowGroupPanel = False
        gvIssue.AllowColumnReorder = False
        gvIssue.AllowRowReorder = False
        gvIssue.EnableSorting = False
        gvIssue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvIssue.MasterTemplate.ShowRowHeaderColumn = False
        gvIssue.EnableFiltering = False
        gvIssue.Rows.AddNew()
    End Sub

    Private Sub LoadARBlankGrid()
        gvARDetail.Rows.Clear()
        gvARDetail.Columns.Clear()

        Dim repoARSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSno.FormatString = ""
        repoARSno.Name = colARSno
        repoARSno.Width = 60
        repoARSno.DecimalPlaces = 0
        repoARSno.HeaderText = "S.No."
        repoARSno.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoARSno)

        Dim ARType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        ARType.FormatString = ""
        ARType.Name = colARType
        ARType.Width = 120
        ARType.HeaderText = "Type(Add/Remove)"
        ARType.ReadOnly = False
        ARType.DataSource = GetARType()
        ARType.ValueMember = "Code"
        ARType.DisplayMember = "Name"
        gvARDetail.MasterTemplate.Columns.Add(ARType)

        Dim repoLoaction_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoaction_Code.FormatString = ""
        repoLoaction_Code.Name = colARLocCode
        repoLoaction_Code.Width = 120
        repoLoaction_Code.HeaderText = "Location Code"
        repoLoaction_Code.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLoaction_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLoaction_Code.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(repoLoaction_Code)

        Dim repoLoaction_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoaction_Desc.FormatString = ""
        repoLoaction_Desc.Name = colARLocDesc
        repoLoaction_Desc.Width = 120
        repoLoaction_Desc.HeaderText = "Location Description"
        repoLoaction_Desc.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoLoaction_Desc)

        Dim ARItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ARItemCode.FormatString = ""
        ARItemCode.Name = colARItemCode
        ARItemCode.Width = 100
        ARItemCode.HeaderText = "Item Code"
        ARItemCode.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(ARItemCode)

        Dim repoARItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoARItemName.FormatString = ""
        repoARItemName.Name = colARItemName
        repoARItemName.Width = 120
        repoARItemName.HeaderText = "Item Description"
        repoARItemName.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoARItemName)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colARItemType
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoitype)

        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colARItemProductType
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoPtype)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Batch Item"
        repoIsSerItem.Name = colARIsBatchItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvARDetail.MasterTemplate.Columns.Add(repoIsSerItem)


        Dim repoARAvailQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARAvailQty.FormatString = ""
        repoARAvailQty.Name = colARAvailQty
        repoARAvailQty.Width = 120
        repoARAvailQty.HeaderText = "Available Quantity"
        repoARAvailQty.DecimalPlaces = DecimalPointQty
        repoARAvailQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARAvailQty.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repoARAvailQty)

        Dim repoARQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARQty.FormatString = ""
        repoARQty.Name = colARQty
        repoARQty.Width = 120
        repoARQty.HeaderText = "Add/Remove Qty"
        repoARQty.DecimalPlaces = DecimalPointQty
        repoARQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARQty.Minimum = 0
        repoARQty.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(repoARQty)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colARUom
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colARUOMDesc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gvARDetail.MasterTemplate.Columns.Add(repouomname)

        Dim repoARFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARFatPer.FormatString = ""
        repoARFatPer.Name = colAR_FAT_Per
        repoARFatPer.Width = 100
        repoARFatPer.HeaderText = "Fat %"
        repoARFatPer.DecimalPlaces = DecimalPointFatSNFPer
        repoARFatPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoARFatPer.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(repoARFatPer)

        Dim repoARSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSNFPer.FormatString = ""
        repoARSNFPer.Name = colAR_SNF_Per
        repoARSNFPer.Width = 100
        repoARSNFPer.HeaderText = "SNF %"
        repoARSNFPer.DecimalPlaces = DecimalPointFatSNFPer
        repoARSNFPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoARSNFPer.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(repoARSNFPer)

        Dim repoARFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARFatKG.FormatString = ""
        repoARFatKG.Name = colAR_FAT_KG
        repoARFatKG.Width = 100
        repoARFatKG.HeaderText = "FAT KG"
        repoARFatKG.DecimalPlaces = DecimalPointQty
        repoARFatKG.Minimum = 0
        repoARFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARFatKG.ReadOnly = If(AutoCalcQtyAddRem = True, False, True)
        gvARDetail.MasterTemplate.Columns.Add(repoARFatKG)

        Dim repoARSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSNFKG.FormatString = ""
        repoARSNFKG.Name = colAR_SNF_KG
        repoARSNFKG.Width = 100
        repoARSNFKG.HeaderText = "SNF KG"
        repoARSNFKG.DecimalPlaces = DecimalPointQty
        repoARSNFKG.Minimum = 0
        repoARSNFKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARSNFKG.ReadOnly = If(AutoCalcQtyAddRem = True, False, True)
        gvARDetail.MasterTemplate.Columns.Add(repoARSNFKG)


        Dim ARRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ARRemarks.FormatString = ""
        ARRemarks.Name = colARRemarks
        ARRemarks.Width = 120
        ARRemarks.HeaderText = "Remarks"
        ARRemarks.ReadOnly = False
        gvARDetail.MasterTemplate.Columns.Add(ARRemarks)

        gvARDetail.AllowDeleteRow = True
        gvARDetail.AllowAddNewRow = False
        gvARDetail.ShowGroupPanel = False
        gvARDetail.AllowColumnReorder = False
        gvARDetail.AllowRowReorder = False
        gvARDetail.EnableSorting = False
        gvARDetail.EnableFiltering = False
        gvARDetail.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvARDetail.MasterTemplate.ShowRowHeaderColumn = False
        gvARDetail.Rows.AddNew()
    End Sub

    Private Sub LoadQCBlankGrid()
        gv_qc.Rows.Clear()
        gv_qc.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCSno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reposno)

        Dim reportyp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reportyp.FormatString = ""
        reportyp.Name = colQCType
        reportyp.HeaderText = "QC From"
        reportyp.Width = 80
        reportyp.WrapText = True
        reportyp.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reportyp)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.Name = colQCItemCode
        repoItemCode.Width = 100
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoQCItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCItemName.FormatString = ""
        repoQCItemName.Name = colQCItemName
        repoQCItemName.Width = 120
        repoQCItemName.HeaderText = "Item Description"
        repoQCItemName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoQCItemName)

        Dim repoQCparamcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCparamcode.FormatString = ""
        repoQCparamcode.Name = colQCparamcode
        repoQCparamcode.Width = 100
        repoQCparamcode.HeaderText = "Parameter Code"
        repoQCparamcode.ReadOnly = True
        'bomcode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'bomcode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_qc.MasterTemplate.Columns.Add(repoQCparamcode)

        Dim repoQCparam_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCparam_desc.FormatString = ""
        repoQCparam_desc.Name = colQCparam_desc
        repoQCparam_desc.Width = 120
        repoQCparam_desc.HeaderText = "Parameter Description"
        repoQCparam_desc.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoQCparam_desc)

        Dim repotype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotype.FormatString = ""
        repotype.Name = colQCparam_type
        repotype.Width = 120
        repotype.HeaderText = "Parameter Type"
        repotype.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repotype)

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.FormatString = ""
        reponature.Name = colQCparam_nature
        reponature.Width = 120
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reponature)

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colQCrange1
        repolower.Width = 120
        repolower.HeaderText = "Standard Range"
        repolower.DecimalPlaces = DecimalPointFatSNFPer
        repolower.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repolower.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolower)

        'Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoupper.Name = colQCrange2
        'repoupper.Width = 120
        'repoupper.HeaderText = "Upper Range"
        'repoupper.DecimalPlaces = 3
        'repoupper.ReadOnly = True
        'gv_qc.MasterTemplate.Columns.Add(repoupper)

        Dim repoBoolean_Status As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoolean_Status.Name = colQCBooleanStatus
        repoBoolean_Status.Width = 120
        repoBoolean_Status.HeaderText = "Standard Status"
        'repoBoolean_Status.DataSource = LoadStatus()
        'repoBoolean_Status.ValueMember = "Code"
        'repoBoolean_Status.DisplayMember = "Name"
        repoBoolean_Status.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoBoolean_Status)

        Dim repoAlpha_Value As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAlpha_Value.Name = colQCAlphaValue
        repoAlpha_Value.HeaderText = "Standard Value"
        repoAlpha_Value.Width = 120
        repoAlpha_Value.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoAlpha_Value)


        Dim repoActual_Range As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActual_Range.Name = colActual_Range
        repoActual_Range.Width = 120
        repoActual_Range.HeaderText = "Actual Range"
        repoActual_Range.DecimalPlaces = DecimalPointFatSNFPer

        'repoActual_Range.MaxLength = 30
        gv_qc.MasterTemplate.Columns.Add(repoActual_Range)

        Dim repoActual_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoActual_Status.Name = colActual_Status
        repoActual_Status.Width = 120
        repoActual_Status.HeaderText = "Actual Status"
        repoActual_Status.DataSource = GetQCActualStatus()
        repoActual_Status.ValueMember = "Code"
        repoActual_Status.DisplayMember = "Name"
        'repoActual_Status.MaxLength = 30
        gv_qc.MasterTemplate.Columns.Add(repoActual_Status)

        Dim repoActual_Value As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActual_Value.Name = colActual_Value
        repoActual_Value.HeaderText = "Actual Value"
        repoActual_Value.Width = 120
        'repoActual_Value.DataSource = clsProcessProductionStandardization.OpenParameterValueList("")
        'repoActual_Value.ValueMember = "Code"
        'repoActual_Value.DisplayMember = "Code"
        gv_qc.MasterTemplate.Columns.Add(repoActual_Value)

        Dim repoQc_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoQc_Status.Name = colQc_Status
        repoQc_Status.Width = 120
        repoQc_Status.HeaderText = "QC Status"
        repoQc_Status.DataSource = GetQCType()
        repoQc_Status.ValueMember = "Code"
        repoQc_Status.DisplayMember = "Name"
        'repoQc_Status.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repoQc_Status)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colQCremarks
        reporem.Width = 120
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gv_qc.MasterTemplate.Columns.Add(reporem)

        reposno = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCParentLineNo
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "Parent Line No"
        reposno.ReadOnly = True
        reposno.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(reposno)

        gv_qc.AllowDeleteRow = False
        gv_qc.AllowAddNewRow = False
        gv_qc.ShowGroupPanel = False
        gv_qc.AllowColumnReorder = False
        gv_qc.AllowRowReorder = False
        gv_qc.EnableSorting = False
        gv_qc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_qc.MasterTemplate.ShowRowHeaderColumn = False
        gv_qc.EnableFiltering = False
        gv_qc.Rows.AddNew()
    End Sub

    Public Shared Function GetARType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Add"
        dr("Name") = "Add"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Remove"
        dr("Name") = "Remove"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetIssueStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Accept"
        dr("Name") = "Accept"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Hold"
        dr("Name") = "Hold"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetQCType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Ok"
        dr("Name") = "Ok"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Not Ok"
        dr("Name") = "Not Ok"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetQCActualStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "Yes"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "No"
        dr("Name") = "No"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Function AllowToSave(Optional ByVal IsPost As Boolean = False) As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If

            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                Errorcontrol.SetError(fndChildBatchNo, "Select Child batch order detail.")
                fndChildBatchNo.Select()
                fndChildBatchNo.Focus()
                Throw New Exception("Select Child batch order detail.")
            Else
                Errorcontrol.ResetError(fndChildBatchNo)
            End If

            If clsCommon.myLen(fndMainBatchNo.Value) <= 0 Then
                Errorcontrol.SetError(fndMainBatchNo, "Main Batch Order No is blank.")
                fndChildBatchNo.Select()
                fndChildBatchNo.Focus()
                Throw New Exception("Main Batch Order No is blank.")
            Else
                Errorcontrol.ResetError(fndChildBatchNo)
            End If
            If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
                myMessages.blankValue("Consumption Location Code")
                fndChildBatchNo.Focus()
                Return False
            End If
            If clsCommon.myLen(lblConsmSectionCode.Text) <= 0 Then
                myMessages.blankValue("Consumption Section Code")
                fndChildBatchNo.Focus()
                Return False
            End If
            '' validation for issue items accept/hold
            For Each growIssue As GridViewRowInfo In gvIssue.Rows
                If clsCommon.myLen(growIssue.Cells(colIssueStatus).Value) <= 0 Then
                    Throw New Exception("Select Status in Issue Detail tab at line no- " & (growIssue.Index + 1) & ".")
                End If
            Next
            If ProductionOrStandAccordingToItemType = 1 Then
                Dim strItemType = clsDBFuncationality.getSingleValue("select item_type from TSPL_ITEM_MASTER  where item_code='" & clsCommon.myCstr(gv.Rows(0).Cells(colitemcode).Value) & "' ")
                If clsCommon.CompairString(strItemType, "S") <> CompairStringResult.Equal Then
                    Throw New Exception("Batch Order item should be only Semi FG Type.")
                End If
            End If
            ' '' validation for standardization batch location
            'For Each growBatch As GridViewRowInfo In gv.Rows
            '    If clsCommon.myLen(growBatch.Cells(colSTD_Loaction_Code).Value) <= 0 Then
            '        Throw New Exception("Select Location Code in Batch Detail tab at line no- " & (growBatch.Index + 1) & ".")
            '    End If
            'Next

            Dim strBatchORderExistIntPIE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Standardization_code from TSPL_PP_STANDARDIZATION_HEAD where TSPL_PP_STANDARDIZATION_HEAD.Main_Batch_Code='" + fndMainBatchNo.Value + "' and TSPL_PP_STANDARDIZATION_HEAD.Standardization_code not in ('" + txtCode.Value + "')"))
            If clsCommon.myLen(clsCommon.myCstr(strBatchORderExistIntPIE)) > 0 Then
                Throw New Exception("Please select different Batch Order, Same Batch exists with Production Standardization " & strBatchORderExistIntPIE & "  ")
            End If

            '' validation for standardization stages existence
            If gvStage.Rows.Count <= 0 Then
                Throw New Exception("Standardization Stages not found for selected child batch's section and structure.")
            End If
            ''richa BHA/05/09/18-000509
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, Nothing), "0") = CompairStringResult.Equal Then
                For Each dr As GridViewRowInfo In gvStage.Rows
                    If clsCommon.myLen(dr.Cells(colUnit_Code).Value) <= 0 Then
                        Throw New Exception("No any Item found of type Milk or Milk Product in Issue Grid.")
                    End If
                    'total = total + 1
                Next
            End If
            If IsPost = True Then
                Dim paramcode As String = ""
                Dim nature As String = ""
                Dim range1 As Decimal = Nothing
                Dim range2 As Decimal = Nothing
                Dim Actual_Range As String = ""
                Dim Actual_Value As String = ""
                Dim Actual_Status As String = ""
                Dim QC_Status As String = ""
                For Each growBatch As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(growBatch.Cells(colSTD_Loaction_Code).Value) <= 0 Then
                        Throw New Exception("Select Location Code in Batch Detail tab at line no- " & (growBatch.Index + 1) & ".")
                    End If
                Next

                For ii As Integer = 0 To gv_qc.Rows.Count - 1
                    paramcode = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparamcode).Value)

                    If ii = 0 AndAlso clsCommon.myLen(paramcode) <= 0 Then
                        RadPageView1.SelectedPage = pageParameterDetail
                        Throw New Exception("Press Go button for filling QC detail in grid")
                    End If

                    nature = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_nature).Tag)
                    range1 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange1).Value)
                    'range2 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange2).Value)
                    Actual_Range = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colActual_Range).Value)
                    Actual_Value = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colActual_Value).Value)
                    Actual_Status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colActual_Status).Value)
                    QC_Status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQc_Status).Value)

                    If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "B") = CompairStringResult.Equal AndAlso (clsCommon.myLen(Actual_Status) <= 0) Then
                        RadPageView1.SelectedPage = pageParameterDetail
                        Throw New Exception("Fill Actual Status for parameter " & paramcode & " at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "R") = CompairStringResult.Equal AndAlso (clsCommon.myLen(Actual_Range) <= 0) Then
                        RadPageView1.SelectedPage = pageParameterDetail
                        Throw New Exception("Fill Actual Range Value for parameter " & paramcode & " at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "A") = CompairStringResult.Equal AndAlso (clsCommon.myLen(Actual_Value) <= 0) Then
                        RadPageView1.SelectedPage = pageParameterDetail
                        Throw New Exception("Fill Actual Value  for parameter " & paramcode & " at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(paramcode) > 0 AndAlso (clsCommon.myLen(QC_Status) <= 0) Then
                        RadPageView1.SelectedPage = pageParameterDetail
                        Throw New Exception("Fill QC Status for parameter " & paramcode & " at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                Next
                '' check fat/snf control
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.FatSNFControlOnProductionConsumption, clsFixedParameterCode.FatSNFControlOnProductionConsumption, Nothing), "1") = CompairStringResult.Equal Then
                    If ValidateFatSNFQuantityControl() = False Then
                        Return False
                    End If
                End If
            Else
                '' check fat/snf control
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.FatSNFControlOnProductionConsumption, clsFixedParameterCode.FatSNFControlOnProductionConsumption, Nothing), "1") = CompairStringResult.Equal Then
                    ValidateFatSNFQuantityControl()
                End If
            End If




            '' check for stock of add/remove items 
            For Each dr As GridViewRowInfo In gvARDetail.Rows
                If clsCommon.myLen(dr.Cells(colARItemCode).Value) <= 0 Then
                    Continue For
                End If
                If settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                    Dim loc_type As Integer = 0
                    qry = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + clsCommon.myCstr(dr.Cells(colARLocCode).Value) + "'"
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "MAIN") = CompairStringResult.Equal Then
                        loc_type = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SUB") = CompairStringResult.Equal Then
                        loc_type = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SEC") = CompairStringResult.Equal Then
                        loc_type = 0
                    End If
                    Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(dr.Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(dr.Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(dr.Cells(colARUom).Value), loc_type, IIf(clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        dr.Cells(colARAvailQty).Value = clsCommon.myCdbl(dt.Rows(0)("qty"))
                        If clsCommon.myCdbl(dr.Cells(colARQty).Value) > clsCommon.myCdbl(dr.Cells(colARAvailQty).Value) Then
                            Throw New Exception("Item Code: " & dr.Cells(colARItemCode).Value & " ; Required Qty : " & clsCommon.myCstr(dr.Cells(colARQty).Value) & " ; Available Qty : " & clsCommon.myCstr(dr.Cells(colARAvailQty).Value) & "")
                        End If
                        Dim dblFATPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(dr.Cells(colARItemCode).Value), clsCommon.myCstr(dr.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("fat_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                        Dim dblSNFPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(dr.Cells(colARItemCode).Value), clsCommon.myCstr(dr.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("snf_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                        For ii As Integer = 0 To gv_qc.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCItemCode).Value), clsCommon.myCstr(dr.Cells(colARItemCode).Value)) = CompairStringResult.Equal Then
                                If clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = dr.Index Then
                                    gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                                    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                        gv_qc.Rows(ii).Cells(colActual_Range).Value = dblFATPer
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                        gv_qc.Rows(ii).Cells(colActual_Range).Value = dblSNFPer
                                    End If
                                End If
                            End If
                        Next
                    End If

                End If
                If clsCommon.myLen(dr.Cells(colARLocCode).Value) <= 0 Then
                    Throw New Exception("Enter add/remove location at line no " & (dr.Index + 1) & "")
                End If
                ''BHA/12/12/18-000751 by balwinder on 12/12/2018
                If clsCommon.myCdbl(dr.Cells(colARQty).Value) < 0 Then
                    Throw New Exception("Add/Remove Qty can't be -ve at line no " & (dr.Index + 1) & "")
                End If
                If clsCommon.myCdbl(dr.Cells(colAR_FAT_KG).Value) < 0 Then
                    Throw New Exception("Add/Remove FAT Kg can't be -ve at line no " & (dr.Index + 1) & "")
                End If
                If clsCommon.myCdbl(dr.Cells(colAR_SNF_KG).Value) < 0 Then
                    Throw New Exception("Add/Remove SNF Kg can't be -ve at line no " & (dr.Index + 1) & "")
                End If
                If clsCommon.myCdbl(dr.Cells(colAR_FAT_Per).Value) < 0 Then
                    Throw New Exception("Add/Remove FAT % can't be -ve at line no " & (dr.Index + 1) & "")
                End If
                If clsCommon.myCdbl(dr.Cells(colAR_SNF_Per).Value) < 0 Then
                    Throw New Exception("Add/Remove SNF % can't be -ve at line no " & (dr.Index + 1) & "")
                End If
                For ii As Integer = 0 To gvIssue.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARItemCode).Value), clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueItemCode).Value)) = CompairStringResult.Equal Then
                        If Not clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARUom).Value), clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueUom).Value)) = CompairStringResult.Equal Then
                            Throw New Exception("Add/Remove Tab item [" + clsCommon.myCstr(dr.Cells(colARItemCode).Value + "] UOM should be [" + clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueUom).Value) + "] because if add/remove item is from issued item then its unit must be same as issued unit as at line no " & (dr.Index + 1) & ""))
                        End If
                    End If
                Next

                Dim availQty As Decimal = clsCommon.myCdbl(dr.Cells(colARAvailQty).Value)
                Dim reqQty As Decimal = clsCommon.myCdbl(dr.Cells(colARQty).Value)

                If RunBatchFifowise Then
                    If clsCommon.CompairString(dr.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                        gvARDetail.CurrentRow = gvARDetail.Rows(dr.Index)
                        OpenBatchItem()
                    End If
                End If

                If reqQty > 0 AndAlso clsCommon.myCBool(clsCommon.myCdbl(dr.Cells(colARIsBatchItem).Value)) Then
                    If clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                        Dim arrBatchNo As List(Of clsBatchInventoryNew) = TryCast(dr.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventoryNew In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If Math.Abs(tQty - reqQty) > 0.01 Then
                                Throw New Exception("Item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " Entered Qty " + clsCommon.myCstr(reqQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                            End If
                        End If
                    Else
                        Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(dr.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventory In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If Math.Abs(tQty - reqQty) > 0.01 Then
                                Throw New Exception("Item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " Entered Qty " + clsCommon.myCstr(reqQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                            End If
                        End If
                    End If

                End If

                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, Nothing)), "0") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(dr.Cells(colARType).Value, "Remove") = CompairStringResult.Equal Then
                        Continue For
                    End If
                End If
                If clsCommon.CompairString(dr.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                    If availQty < reqQty Then
                        If Not settAllowNegativeStockInDairyProduction Then
                            Throw New Exception("Item Code: " & dr.Cells(colARItemCode).Value & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                        End If
                    End If
                End If
                ''richa BHA/27/07/18-000200
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ConsiderSiloCapicityForStockIn, clsFixedParameterCode.ConsiderSiloCapicityForStockIn, Nothing)), "1") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARType).Value), "Remove") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARItemProductType).Value), "MP") = CompairStringResult.Equal) Then
                        Dim strsilo As String = clsCommon.myCstr(dr.Cells(colARLocCode).Value)
                        If clsCommon.myLen(strsilo) > 0 Then
                            Dim balqtyofvl As Double = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(dr.Cells(colARItemCode).Value), strsilo, "", txtCode.Value, dtpDate.Value, Nothing, "LTR"))
                            Dim itemQty As Double = clsCommon.myCdbl(dr.Cells(colARQty).Value)
                            Dim DblFinalQty As Double = balqtyofvl + itemQty
                            Dim SiloCapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Silo_Capacity,0) from TSPL_LOCATION_MASTER where location_code='" & strsilo & "'"))
                            If DblFinalQty > SiloCapacity Then
                                Throw New Exception("Silo Qty should be less than or equal to " & SiloCapacity)
                            End If
                        Else
                            Throw New Exception("Please entre silo location on Unloading")
                        End If
                    End If
                End If
            Next
            calculateALL()
            If settProductionRemoveFATSNFKgTollerance > 0 Then
                If clsCommon.myCdbl(lblTotRemovedFATKG.Text) > 0 Then
                    'Dim dblFatKG As Decimal = clsCommon.myCdbl(lblTotDifferenceFATKG.Text) + clsCommon.myCdbl(lblTotAddedFATKG.Text)
                    Dim dblFatKG As Decimal = clsCommon.myCdbl(lblTotIssueFATKG.Text)
                    dblFatKG = dblFatKG * (100 + settProductionRemoveFATSNFKgTollerance) / 100
                    If clsCommon.myCdbl(lblTotRemovedFATKG.Text) > dblFatKG Then
                        Throw New Exception("Removed FAT KG Can't be More Than " + clsCommon.myCstr(dblFatKG))
                    End If
                End If
                If clsCommon.myCdbl(lblTotRemovedSNFKG.Text) > 0 Then
                    'Dim dblSNFKG As Decimal = clsCommon.myCdbl(lblTotDifferenceSNFKG.Text) + clsCommon.myCdbl(lblTotAddedSNFKG.Text)
                    Dim dblSNFKG As Decimal = clsCommon.myCdbl(lblTotIssueSNFKG.Text)
                    dblSNFKG = dblSNFKG * (100 + settProductionRemoveFATSNFKgTollerance) / 100
                    If clsCommon.myCdbl(lblTotRemovedSNFKG.Text) > dblSNFKG Then
                        Throw New Exception("Removed SNF KG Can't be More Than " + clsCommon.myCstr(dblSNFKG))
                    End If
                End If
            End If
            If settCheckNetFatKg > 0 Then
                If Math.Abs(clsCommon.myCdbl(lblTotNetFATKG.Text)) > settCheckNetFatKg Then
                    Throw New Exception("Net FAT KG Can't be More " + clsCommon.myCstr(settCheckNetFatKg))
                End If
            End If
            If settCheckNetFatKg > 0 Then
                If Math.Abs(clsCommon.myCdbl(lblTotNetSNFKG.Text)) > settCheckNetSNFKg Then
                    Throw New Exception("Net SNF KG Can't be More " + clsCommon.myCstr(settCheckNetSNFKg))
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Function ValidateFatSNFQuantityControl() As Boolean
        Dim TotIssueFatKg As Decimal = 0
        Dim TotIssueSnfKg As Decimal = 0
        Dim TotIssueQty As Decimal = 0

        Dim TotProdFatKg As Decimal = 0
        Dim TotProdSnfKg As Decimal = 0
        Dim TotProdQty As Decimal = 0

        '' for issued/added qty
        For Each grow As GridViewRowInfo In gvIssue.Rows
            If clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colIssued_FAT_KG).Value)
                TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colIssued_SNF_KG).Value)
                TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colIssueItemCode).Value, grow.Cells(colIssueUom).Value, clsCommon.myCdbl(grow.Cells(colIssued_Qty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colIssued_Qty).Value)
            End If
        Next
        For Each grow As GridViewRowInfo In gvARDetail.Rows
            If clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MP") = CompairStringResult.Equal Then
                If clsCommon.CompairString(grow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                    TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colAR_FAT_KG).Value)
                    TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colAR_SNF_KG).Value)
                    TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colARItemCode).Value, grow.Cells(colARItemCode).Value, clsCommon.myCdbl(grow.Cells(colARQty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colARQty).Value)
                End If
            End If
        Next

        '' for Produced/removed qty
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(grow.Cells(colitemproducttype).Value, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colitemproducttype).Value, "MP") = CompairStringResult.Equal Then
                TotProdFatKg = TotProdFatKg + clsCommon.myCdbl(grow.Cells(colProduced_FAT_KG).Value)
                TotProdSnfKg = TotProdSnfKg + clsCommon.myCdbl(grow.Cells(colProduced_SNF_KG).Value)
                TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colitemcode).Value, grow.Cells(coluom).Value, clsCommon.myCdbl(grow.Cells(colProduced_Qty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colProduced_Qty).Value)
            End If
        Next
        For Each grow As GridViewRowInfo In gvARDetail.Rows
            If clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MP") = CompairStringResult.Equal Then
                If clsCommon.CompairString(grow.Cells(colARType).Value, "Remove") = CompairStringResult.Equal Then
                    TotProdFatKg = TotProdFatKg + clsCommon.myCdbl(grow.Cells(colAR_FAT_KG).Value)
                    TotProdSnfKg = TotProdSnfKg + clsCommon.myCdbl(grow.Cells(colAR_SNF_KG).Value)
                    TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colARItemCode).Value, grow.Cells(colARItemCode).Value, clsCommon.myCdbl(grow.Cells(colARQty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colARQty).Value)
                End If
            End If
        Next
        Dim Message As String = ""
        Dim TolerancePer As Decimal = 0
        Dim ToleranceQty As Decimal = 0
        TolerancePer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuantityControlToleranceOnProductionConsumption, clsFixedParameterCode.QuantityControlToleranceOnProductionConsumption, Nothing))
        ToleranceQty = TotProdQty * TolerancePer / 100

        If Math.Abs(TotIssueFatKg - TotProdFatKg) > 0 Then
            Message = "Total (Issued+Added) Fat: " & TotIssueFatKg & " " & Environment.NewLine & "Total (Produced+Removed) Fat: " & TotProdFatKg & ""
            clsCommon.MyMessageBoxShow(Message)
            Return False
        ElseIf Math.Abs(TotIssueSnfKg - TotProdSnfKg) > 0 Then
            Message = "Total (Issued+Added) SNF: " & TotIssueSnfKg & " " & Environment.NewLine & "Total (Produced+Removed) SNF: " & TotProdSnfKg & ""
            clsCommon.MyMessageBoxShow(Message)
            Return False
        ElseIf Math.Abs(TotIssueQty - TotProdQty) > 0 Then
            Dim diff As Decimal = Math.Abs(TotIssueQty - TotProdQty)
            If diff > ToleranceQty Then
                Message = "Total (Issued+Added) Quantity: " & TotIssueQty & " " & Environment.NewLine & "Total (Produced+Removed) Quantity: " & TotProdQty & "" & Environment.NewLine & " Tolerance Quantity(+/-) : " & ToleranceQty & ""
                clsCommon.MyMessageBoxShow(Message)
                Return False
            End If
        End If
        Return True
    End Function

    Private Function SaveData(ByVal isPost As Boolean) As Boolean
        Try
            updateBatchGridParameter()
            Dim obj As New clsProcessProductionStandardization()
            obj.Standardization_Code = clsCommon.myCstr(txtCode.Value)
            obj.Standardization_Date = clsCommon.myCDate(dtpDate.Text)
            obj.Child_Batch_Code = clsCommon.myCstr(fndChildBatchNo.Value)
            obj.Main_Batch_Code = clsCommon.myCstr(fndMainBatchNo.Value)
            obj.Loaction_Code = clsCommon.myCstr(txtlocation.Text)
            obj.Posted = 0
            If clsCommon.CompairString(UsLock1.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then
                obj.Posted = 1
            End If
            If gvStage.Tag Is Nothing Then
                obj.Section_Stage_Map_Code = ""
            Else
                obj.Section_Stage_Map_Code = gvStage.Tag
            End If
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            obj.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            obj.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
            obj.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
            obj.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
            ''----------------
            obj.CONSM_LOCATION_CODE = lblConsmSectionLocCode.Text
            obj.CONSM_SECTION_CODE = lblConsmSectionCode.Text
            '' save enable/disable tab
            obj.ISSUE_TAB_ENABLED = pageIssueDetail.Enabled
            obj.AR_TAB_ENABLED = pageAddRemoveDetail.Enabled
            obj.STAGE_TAB_ENABLED = pageStageDetail.Enabled
            obj.BATCH_TAB_ENABLED = pageBatchDetail.Enabled
            obj.PARAM_TAB_ENABLED = pageParameterDetail.Enabled
            obj.ATTACH_TAB_ENABLED = pageAttachment.Enabled

            obj.Is_Job_Work_Inward = chkJobWorkInward.Checked

            obj.Tot_Batch_Qty = lblTotBatchQty.Text
            obj.Tot_Batch_FATKG = lblTotBatchFATKG.Text
            obj.Tot_Batch_SNFKG = lblTotBatchSNFKG.Text

            obj.Tot_Produce_Qty = lblTotProduceQty.Text
            obj.Tot_Produce_FATKG = lblTotProduceFATKG.Text
            obj.Tot_Produce_SNFKG = lblTotProduceSNFKG.Text

            obj.Tot_Issue_Qty = lblTotIssueQty.Text
            obj.Tot_Issue_FATKG = lblTotIssueFATKG.Text
            obj.Tot_Issue_SNFKG = lblTotIssueSNFKG.Text
            obj.Avg_Rate_FAT = lblAvgRateFAT.Text
            obj.Avg_Rate_SNF = lblAvgRateSNF.Text

            obj.Tot_Difference_Qty = lblTotDifferenceQty.Text
            obj.Tot_Difference_FATKG = lblTotDifferenceFATKG.Text
            obj.Tot_Difference_SNFKG = lblTotDifferenceSNFKG.Text

            obj.Tot_Added_Qty = lblTotAddedQty.Text
            obj.Tot_Added_FATKG = lblTotAddedFATKG.Text
            obj.Tot_Added_SNFKG = lblTotAddedSNFKG.Text

            obj.Tot_Removed_Qty = lblTotRemovedQty.Text
            obj.Tot_Removed_FATKG = lblTotRemovedFATKG.Text
            obj.Tot_Removed_SNFKG = lblTotRemovedSNFKG.Text

            obj.Tot_AddRemove_Qty = lblTotAddRemoveQty.Text
            obj.Tot_AddRemove_FATKG = lblTotAddRemoveFATKG.Text
            obj.Tot_AddRemove_SNFKG = lblTotAddRemoveSNFKG.Text

            obj.Tot_Net_Qty = lblTotNetQty.Text
            obj.Tot_Net_FATKG = lblTotNetFATKG.Text
            obj.Tot_Net_SNFKG = lblTotNetSNFKG.Text


            obj.JW_Estimated_FAT_KG = clsCommon.myCdbl(lblJWEFATKg.Text) ''BHA/28/08/18-000496 by balwinder on 28/09/2018
            obj.JW_Estimated_FAT_Amt = clsCommon.myCdbl(lblJWEFATAmt.Text)
            obj.JW_Estimated_SNF_KG = clsCommon.myCdbl(lblJWESNFKg.Text)
            obj.JW_Estimated_SNF_Amt = clsCommon.myCdbl(lblJWESNFAmt.Text)

            obj.ArrBatchItem = New List(Of clsProcessProductionSTDBatchItemDetail)
            obj.ArrIssueItem = New List(Of clsProcessProductionSTDIssueItemDetail)
            obj.ArrARItem = New List(Of clsProcessProductionARDetail)
            obj.ArrQC = New List(Of clsProcessProductionStandardizationQCDetail)
            obj.ArrStageQC = New List(Of clsProcessProductionSTDDetail)
            '' assign value to batch item array
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsProcessProductionSTDBatchItemDetail()

                objtr.SNO = CInt(grow.Cells(colSno).Value)
                objtr.BOM_Code = clsCommon.myCstr(grow.Cells(colBOM_Code).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colitemtype).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colitemproducttype).Value)
                objtr.Produced_FAT_KG = clsCommon.myCdbl(grow.Cells(colProduced_FAT_KG).Value)
                objtr.Produced_Qty = clsCommon.myCdbl(grow.Cells(colProduced_Qty).Value)
                objtr.Produced_SNF_KG = clsCommon.myCdbl(grow.Cells(colProduced_SNF_KG).Value)
                objtr.Quantity = clsCommon.myCdbl(grow.Cells(colQuantity).Value)
                objtr.Requir_FAT_KG = clsCommon.myCdbl(grow.Cells(colRequir_FAT_KG).Value)
                objtr.Requir_FAT_per = clsCommon.myCdbl(grow.Cells(colRequir_FAT_per).Value)
                objtr.Requir_SNF_KG = clsCommon.myCdbl(grow.Cells(colRequir_SNF_KG).Value)
                objtr.Requir_SNF_Per = clsCommon.myCdbl(grow.Cells(colRequir_SNF_per).Value)
                objtr.Section_Code = clsCommon.myCstr(grow.Cells(colSection_Code).Value)
                objtr.Shift_Code = clsCommon.myCstr(grow.Cells(colShift_Code).Value)
                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(coluom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colUOMDesc).Value)
                objtr.STD_Loaction_Code = clsCommon.myCstr(grow.Cells(colSTD_Loaction_Code).Value)

                objtr.Produced_FAT_per = clsCommon.myCdbl(grow.Cells(colProduced_FAT_per).Value)
                objtr.Produced_SNF_per = clsCommon.myCdbl(grow.Cells(colProduced_SNF_per).Value)
                '' save new columns
                'objtr.NO_SAMPLE_QC = clsCommon.myCdbl(grow.Cells(colNO_SAMPLE_QC).Value)
                'objtr.DAMAGE_Qty = clsCommon.myCdbl(grow.Cells(colDAMAGE_Qty).Value)
                'objtr.FINAL_PROD_Qty = clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.ArrBatchItem.Add(objtr)
                End If
            Next

            '' assign value to Issue item array
            For Each grow As GridViewRowInfo In gvIssue.Rows
                Dim objtr As New clsProcessProductionSTDIssueItemDetail()

                objtr.SNO = CInt(grow.Cells(colIssueSno).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colIssueItemCode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colIssueItemType).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colIssueItemProductType).Value)
                objtr.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colIssued_FAT_KG).Value)
                objtr.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colIssued_FAT_Per).Value)
                objtr.Avail_Qty = clsCommon.myCdbl(grow.Cells(colIssued_Qty).Value)
                objtr.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colIssued_SNF_KG).Value)
                objtr.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colIssued_SNF_Per).Value)

                objtr.Requir_FAT_Per = clsCommon.myCdbl(grow.Cells(colIssueRequir_FAT_Per).Value)
                objtr.Requir_SNF_Per = clsCommon.myCdbl(grow.Cells(colIssueRequir_SNF_Per).Value)

                objtr.Diff_FAT_KG = clsCommon.myCdbl(grow.Cells(colDiff_FAT_KG).Value)
                objtr.Diff_FAT_Per = clsCommon.myCdbl(grow.Cells(colDiff_FAT_Per).Value)
                'objtr.Diff_Qty = clsCommon.myCdbl(grow.Cells(colDiff_Qty).Value)
                objtr.Diff_SNF_KG = clsCommon.myCdbl(grow.Cells(colDiff_SNF_KG).Value)
                objtr.Diff_SNF_Per = clsCommon.myCdbl(grow.Cells(colDiff_SNF_Per).Value)

                objtr.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemarks).Value)
                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colIssueUom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colIssueUOMDesc).Value)

                objtr.TO_LOC_CODE = clsCommon.myCstr(grow.Cells(colTO_LOC_CODE).Value)
                objtr.TO_LOC_DESC = clsCommon.myCstr(grow.Cells(colTO_LOC_DESC).Value)

                objtr.Issue_Status = clsCommon.myCstr(grow.Cells(colIssueStatus).Value)

                '' production costing columns
                objtr.Fat_Rate = clsCommon.myCdbl(grow.Cells(colIssue_Fat_Rate).Value)
                objtr.SNF_Rate = clsCommon.myCdbl(grow.Cells(colIssue_SNF_Rate).Value)
                objtr.Fat_Amt = clsCommon.myCdbl(grow.Cells(colIssue_Fat_Amt).Value)
                objtr.SNF_Amt = clsCommon.myCdbl(grow.Cells(colIssue_SNF_Amt).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.ArrIssueItem.Add(objtr)
                End If
            Next

            '' assign value to AR item array
            For Each grow As GridViewRowInfo In gvARDetail.Rows
                Dim objtr As New clsProcessProductionARDetail()

                objtr.SNO = CInt(grow.Cells(colARSno).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colARItemCode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colARItemType).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colARItemProductType).Value)
                objtr.ADD_REMOVE_QTY = clsCommon.myCdbl(grow.Cells(colARQty).Value)
                objtr.ADD_REMOVE_TYPE = clsCommon.myCstr(grow.Cells(colARType).Value)
                objtr.Item_Desc = clsCommon.myCstr(grow.Cells(colARItemName).Value)
                objtr.Loaction_Code = clsCommon.myCstr(grow.Cells(colARLocCode).Value)
                objtr.Remarks = clsCommon.myCstr(grow.Cells(colARRemarks).Value)

                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colARUom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colARUOMDesc).Value)

                objtr.AR_FAT_Per = clsCommon.myCdbl(grow.Cells(colAR_FAT_Per).Value)
                objtr.AR_SNF_Per = clsCommon.myCdbl(grow.Cells(colAR_SNF_Per).Value)

                objtr.AR_FAT_KG = clsCommon.myCdbl(grow.Cells(colAR_FAT_KG).Value)
                objtr.AR_SNF_KG = clsCommon.myCdbl(grow.Cells(colAR_SNF_KG).Value)
                objtr.arrBatchItem = TryCast(grow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                objtr.arrBatchItemNew = TryCast(grow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.ArrARItem.Add(objtr)
                End If
            Next

            '' assign value to QC array
            For Each grow As GridViewRowInfo In gv_qc.Rows
                Dim objtr As New clsProcessProductionStandardizationQCDetail()
                objtr.Parent_Line_No = clsCommon.myCdbl(grow.Cells(colQCParentLineNo).Value)
                objtr.sno = CInt(grow.Cells(colQCSno).Value)
                objtr.QC_Type = clsCommon.myCstr(grow.Cells(colQCType).Value)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colQCItemCode).Value)
                objtr.param_code = clsCommon.myCstr(grow.Cells(colQCparamcode).Value)
                objtr.Standard_Range = clsCommon.myCstr(grow.Cells(colQCrange1).Value)
                objtr.Standard_Status = clsCommon.myCstr(grow.Cells(colQCBooleanStatus).Value)
                objtr.Standard_Value = clsCommon.myCstr(grow.Cells(colQCAlphaValue).Value)
                'objtr.urange = clsCommon.myCdbl(grow.Cells(colQCrange2).Value)
                objtr.Actual_Range = clsCommon.myCstr(grow.Cells(colActual_Range).Value)
                objtr.Actual_Status = clsCommon.myCstr(grow.Cells(colActual_Status).Value)
                objtr.Actual_Value = clsCommon.myCstr(grow.Cells(colActual_Value).Value)
                objtr.Qc_Status = clsCommon.myCstr(grow.Cells(colQc_Status).Value)

                objtr.remarks = clsCommon.myCstr(grow.Cells(colQCremarks).Value).Replace("'", "`")

                If clsCommon.myLen(objtr.param_code) > 0 Then
                    obj.ArrQC.Add(objtr)
                End If
            Next

            '' assign value to Stage Process  array
            For Each grow As GridViewRowInfo In gvStage.Rows
                Dim objtr As New clsProcessProductionSTDDetail()

                objtr.SNO = CInt(grow.Cells(colStageSno).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Stage_Code = clsCommon.myCstr(grow.Cells(colStage_Code).Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit_Code).Value)
                objtr.Log_Sheet_No = clsCommon.myCstr(grow.Cells(colLog_Sheet_No).Value)
                objtr.Status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                objtr.Received_Qty = clsCommon.myCstr(grow.Cells(colReceived_Qty).Value)
                objtr.Remarks = clsCommon.myCstr(grow.Cells(colSPRemarks).Value)
                objtr.Section_Code = clsCommon.myCstr(grow.Cells(colSPSection).Value)
                objtr.Structure_Code = clsCommon.myCstr(grow.Cells(colSPProdCategory).Value)

                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit_Code).Value)
                objtr.Batch_Code = clsCommon.myCstr(grow.Cells(colStageBatch_Code).Value)

                objtr.SPQCList = grow.Tag
                If clsCommon.myLen(objtr.Stage_Code) > 0 Then
                    obj.ArrStageQC.Add(objtr)
                End If
            Next

            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProcessProductionStandardization.SaveData(isNewEntry, obj) Then
                If isPost = False Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If

                txtCode.Value = obj.Standardization_Code

                UcAttachment1.SaveData(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                Return False
            End If
            obj = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub DeleteData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Select Document Code to delete.")
            End If

            qry = "select count(*) from TSPL_PP_STANDARDIZATION_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Standardization_Code='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)

            If check <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Code not found.")
            End If

            Dim isDeleted As Boolean = False
            If clsProcessProductionStandardization.DeleteData(txtCode.Value, trans) Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                isDeleted = True
            End If


            If isDeleted Then
                FunReset()
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Select Document Code for posting.")
            End If

            qry = "select count(*) from TSPL_PP_STANDARDIZATION_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Standardization_Code='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Code not found.")
            End If

            For Each grow As GridViewRowInfo In gvStage.Rows
                If clsCommon.CompairString(grow.Cells(colStatus).Value, "2") = CompairStringResult.Equal Then
                    Continue For
                End If
                If grow.Tag Is Nothing Then
                    Throw New Exception("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & ".")
                End If
                If clsCommon.CompairString(grow.Cells(colStatus).Value, "1") <> CompairStringResult.Equal Then
                    Throw New Exception("QC Log Sheet is not completed for stage " & grow.Cells(colStage_Code).Value & ".")
                End If
                For Each obj As clsPPSTDLogSheetDetail In grow.Tag
                    If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                        Throw New Exception("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                    End If
                Next
            Next

            If Not clsCommon.MyMessageBoxShow("Are you sure,want to post entry no. " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                Return
            End If

            If AllowToSave(True) Then
                SaveData(True)
            Else
                Exit Sub
            End If
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProcessProductionStandardization.PostData(Me.Form_ID, txtCode.Value, arrLoc) Then
                clsCommon.MyMessageBoxShow("Data Posted Successfully", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtfrmsub__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndChildBatchNo._MYValidating
        fndChildBatchNo.Value = clsProcessProductionStandardization.GetFinder_PendingBatchQuantity("Main.Quantity>coalesce(Produced_Qty,0) " & If(activateSFGProduction = True, "", " and (LEN(Main_Batch_Code)>0 or (TSPL_PP_BATCH_ORDER_HEAD.batch_code like 'C-BO%' or TSPL_PP_BATCH_ORDER_HEAD.batch_code like 'SC-BO%'))") & "  and tspl_pp_batch_order_head.location_code in (" + arrLoc + ") and is_post=1  ", Me.fndChildBatchNo.Value, isButtonClicked, txtCode.Value)
        If clsCommon.myLen(fndChildBatchNo.Value) > 0 Then
            pageIssueDetail.Enabled = True
            RadPageView1.SelectedPage = pageIssueDetail
            'RadPageView1.Pages(0).Enabled = True
            Dim objChildBatch As clsProcessBatchOrder = clsProcessBatchOrder.GetData(fndChildBatchNo.Value, arrLoc, NavigatorType.Current)
            If UseProductionPlaningDateForWholeProductionCycle = True Then
                dtpDate.Value = Nothing
                dtpDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Batch_Date from TSPL_PP_BATCH_ORDER_HEAD where batch_Code='" + fndChildBatchNo.Value + "'"))
            End If
            If Not objChildBatch Is Nothing AndAlso clsCommon.myLen(objChildBatch.Main_batchcode) > 0 Then
                Me.fndMainBatchNo.Value = objChildBatch.Main_batchcode
                Me.lblMainBatchDesc.Text = clsProcessBatchOrder.GetDescription(Me.fndMainBatchNo.Value, Nothing)
                Me.txtlocation.Text = objChildBatch.locationcode

                Me.txtlocationname.Text = objChildBatch.locationname
                Me.lblChildBatchDesc.Text = objChildBatch.batchdesc
                lblConsmSectionLocCode.Text = clsProductionEntry.GetBatchConsumptionSection(txtlocation.Text, fndChildBatchNo.Value)
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                TxtManualBatchNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(ManualBatchNo,'')  from TSPL_PP_BATCH_ORDER_HEAD where batch_Code='" & objChildBatch.Main_batchcode & "'"))
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                If clsCommon.myLen(objChildBatch.Main_batchcode) > 0 Then
                    Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Line_No as [Line No],TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name]" & _
                    " from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode " & _
                    " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode " & _
                    " where TSPL_PP_BATCH_ORDER_HEAD.batch_code='" & clsCommon.myCstr(objChildBatch.Main_batchcode) & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        lblLineNo.Text = clsCommon.myCstr(dt.Rows(0).Item("Line No"))
                        LblCostCenterCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Code"))
                        lblCostCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Name"))
                        lblProfitCenterCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Code"))
                        lblProfitCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Name"))
                    Else
                        lblLineNo.Text = ""
                        LblCostCenterCode.Text = ""
                        lblCostCenterName.Text = ""
                        lblProfitCenterCode.Text = ""
                        lblProfitCenterName.Text = ""
                    End If
                End If
                ''----------------
                If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
                    lblConsmSectionCode.Text = ""
                    clsCommon.MyMessageBoxShow("Consumption Location not found for batch " & fndChildBatchNo.Value & "")
                    Exit Sub
                Else
                    lblConsmSectionCode.Text = clsLocation.GetSectionCode(lblConsmSectionLocCode.Text, Nothing)
                End If
                InitialLoadAllGrid()
            ElseIf Not objChildBatch Is Nothing Then
                Me.fndChildBatchNo.Value = objChildBatch.Batchcode
                Me.txtlocation.Text = objChildBatch.locationcode
                Me.txtlocationname.Text = objChildBatch.locationname
                Me.lblChildBatchDesc.Text = objChildBatch.batchdesc
                Me.lblMainBatchDesc.Text = ""
                TxtManualBatchNo.Text = objChildBatch.ManualBatchNo
                lblLineNo.Text = objChildBatch.LINE_NO
                LblCostCenterCode.Text = objChildBatch.CostCenterCode
                lblCostCenterName.Text = objChildBatch.CostCenterName
                lblProfitCenterCode.Text = objChildBatch.ProfitCenterCode
                lblProfitCenterName.Text = objChildBatch.ProfitCenterName
                If activateSFGProduction Then
                    fndMainBatchNo.Value = objChildBatch.Batchcode
                    lblMainBatchDesc.Text = objChildBatch.batchdesc
                End If
                lblConsmSectionLocCode.Text = clsProductionEntry.GetBatchConsumptionSection(txtlocation.Text, fndChildBatchNo.Value)
                If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
                    lblConsmSectionCode.Text = ""
                    clsCommon.MyMessageBoxShow("Consumption Location not found for batch " & fndChildBatchNo.Value & "")
                    Exit Sub
                Else
                    lblConsmSectionCode.Text = clsLocation.GetSectionCode(lblConsmSectionLocCode.Text, Nothing)
                End If
                InitialLoadAllGrid()
            Else
                Me.fndChildBatchNo.Value = Nothing
                Me.fndMainBatchNo.Value = Nothing
                TxtManualBatchNo.Text = ""
                Me.txtlocation.Text = ""
                Me.txtlocationname.Text = ""
                lblLineNo.Text = ""
                LblCostCenterCode.Text = ""
                lblCostCenterName.Text = ""
                lblProfitCenterCode.Text = ""
                lblProfitCenterName.Text = ""
            End If
            chkJobWorkInward.Checked = clsProcessBatchOrder.IsJobWorkBatchOrder(fndChildBatchNo.Value, Nothing)
        Else
            fndChildBatchNo.Value = ""
            lblChildBatchDesc.Text = ""
            pageIssueDetail.Enabled = False
            'RadPageView1.Pages(0).Enabled = False
        End If
    End Sub

    Private Function ItemType(ByVal itype As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(itype, "R") = CompairStringResult.Equal Then
            values = "Raw Material"
        ElseIf clsCommon.CompairString(itype, "F") = CompairStringResult.Equal Then
            values = "Finished Good"
        ElseIf clsCommon.CompairString(itype, "S") = CompairStringResult.Equal Then
            values = "Semi Finished Good"
        ElseIf clsCommon.CompairString(itype, "A") = CompairStringResult.Equal Then
            values = "Asset"
        ElseIf clsCommon.CompairString(itype, "H") = CompairStringResult.Equal Then
            values = "Fresh"
        ElseIf clsCommon.CompairString(itype, "O") = CompairStringResult.Equal Then
            values = "Other"
        End If

        Return values
    End Function

    Private Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        values = clsItemMaster.ProductType(Product_type)

        Return values
    End Function

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        qry = "select count(*) from TSPL_PP_STANDARDIZATION_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Standardization_Code='" + txtCode.Value + "'"
        check = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsProcessProductionStandardization.GetFinder(" TSPL_PP_BATCH_ORDER_HEAD.Location_Code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            FunReset()
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsProcessProductionStandardization()
            isNewEntry = True
            obj = clsProcessProductionStandardization.GetData(strCode, arrLoc, NavType, Nothing)
            isInsideLoadData = True
            EnableAllTabPages()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Standardization_Code) > 0 Then
                isNewEntry = False

                txtCode.Value = obj.Standardization_Code
                dtpDate.Text = obj.Standardization_Date

                If obj.Posted = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnCancel.Enabled = True
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnCancel.Enabled = False
                End If
                fndChildBatchNo.Value = obj.Child_Batch_Code
                txtlocation.Text = obj.Loaction_Code
                txtlocationname.Text = obj.Loaction_Desc
                fndMainBatchNo.Value = obj.Main_Batch_Code
                lblChildBatchDesc.Text = obj.Child_Batch_Desc
                lblMainBatchDesc.Text = obj.Main_Batch_Desc
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                TxtManualBatchNo.Text = obj.ManualBatchNo
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                lblLineNo.Text = obj.LINE_NO
                LblCostCenterCode.Text = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                lblProfitCenterCode.Text = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                ''----------------
                lblConsmSectionLocCode.Text = obj.CONSM_LOCATION_CODE
                lblConsmSectionCode.Text = obj.CONSM_SECTION_CODE
                '' enable/disable tab
                chkJobWorkInward.Checked = obj.Is_Job_Work_Inward
                lblTotBatchQty.Text = clsCommon.myFormat(obj.Tot_Batch_Qty)
                lblTotBatchFATKG.Text = clsCommon.myFormat(obj.Tot_Batch_FATKG)
                lblTotBatchSNFKG.Text = clsCommon.myFormat(obj.Tot_Batch_SNFKG)

                lblTotProduceQty.Text = clsCommon.myFormat(obj.Tot_Produce_Qty)
                lblTotProduceFATKG.Text = clsCommon.myFormat(obj.Tot_Produce_FATKG)
                lblTotProduceSNFKG.Text = clsCommon.myFormat(obj.Tot_Produce_SNFKG)

                lblTotIssueQty.Text = clsCommon.myFormat(obj.Tot_Issue_Qty)
                lblTotIssueFATKG.Text = clsCommon.myFormat(obj.Tot_Issue_FATKG)
                lblTotIssueSNFKG.Text = clsCommon.myFormat(obj.Tot_Issue_SNFKG)
                lblAvgRateFAT.Text = clsCommon.myFormat(obj.Avg_Rate_FAT)
                lblAvgRateSNF.Text = clsCommon.myFormat(obj.Avg_Rate_SNF)

                lblTotDifferenceQty.Text = clsCommon.myFormat(obj.Tot_Difference_Qty)
                lblTotDifferenceFATKG.Text = clsCommon.myFormat(obj.Tot_Difference_FATKG)
                lblTotDifferenceSNFKG.Text = clsCommon.myFormat(obj.Tot_Difference_SNFKG)

                lblTotAddedQty.Text = clsCommon.myFormat(obj.Tot_Added_Qty)
                lblTotAddedFATKG.Text = clsCommon.myFormat(obj.Tot_Added_FATKG)
                lblTotAddedSNFKG.Text = clsCommon.myFormat(obj.Tot_Added_SNFKG)

                lblTotRemovedQty.Text = clsCommon.myFormat(obj.Tot_Removed_Qty)
                lblTotRemovedFATKG.Text = clsCommon.myFormat(obj.Tot_Removed_FATKG)
                lblTotRemovedSNFKG.Text = clsCommon.myFormat(obj.Tot_Removed_SNFKG)


                lblTotAddRemoveQty.Text = clsCommon.myFormat(obj.Tot_AddRemove_Qty)
                lblTotAddRemoveFATKG.Text = clsCommon.myFormat(obj.Tot_AddRemove_FATKG)
                lblTotAddRemoveSNFKG.Text = clsCommon.myFormat(obj.Tot_AddRemove_SNFKG)

                lblTotNetQty.Text = clsCommon.myFormat(obj.Tot_Net_Qty)
                lblTotNetFATKG.Text = clsCommon.myFormat(obj.Tot_Net_FATKG)
                lblTotNetSNFKG.Text = clsCommon.myFormat(obj.Tot_Net_SNFKG)


                lblJWEFATKg.Text = clsCommon.myFormat(obj.JW_Estimated_FAT_KG)
                lblJWEFATAmt.Text = clsCommon.myFormat(obj.JW_Estimated_FAT_Amt)
                lblJWESNFKg.Text = clsCommon.myFormat(obj.JW_Estimated_SNF_KG)
                lblJWESNFAmt.Text = clsCommon.myFormat(obj.JW_Estimated_SNF_Amt)

                pageIssueDetail.Enabled = obj.ISSUE_TAB_ENABLED
                pageAddRemoveDetail.Enabled = obj.AR_TAB_ENABLED
                pageStageDetail.Enabled = obj.STAGE_TAB_ENABLED
                pageBatchDetail.Enabled = obj.BATCH_TAB_ENABLED
                pageParameterDetail.Enabled = obj.PARAM_TAB_ENABLED
                pageAttachment.Enabled = obj.ATTACH_TAB_ENABLED
                LoadARBlankGrid()
                gv.Rows.Clear()
                gv_qc.Rows.Clear()
                gvIssue.Rows.Clear()
                gvARDetail.Rows.Clear()
                gvStage.Rows.Clear()
                gvStage.Tag = obj.Section_Stage_Map_Code

                Dim arr_BatchIcode As New List(Of String)
                Dim arr_ARIcode As New List(Of String)
                arr_BatchIcode = New List(Of String)
                arr_ARIcode = New List(Of String)

                '' load batch item grid
                If obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSTDBatchItemDetail In obj.ArrBatchItem
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = objtr.SNO
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Code).Value = objtr.BOM_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Desc).Value = objtr.BOM_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = objtr.Item_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = IIf(clsCommon.myLen(objtr.Product_Type) <= 0, "Others", objtr.Product_Type)
                        gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = objtr.Unit_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = objtr.Unit_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_Qty).Value = objtr.Produced_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_KG).Value = objtr.Requir_FAT_KG
                        gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_per).Value = objtr.Requir_FAT_per
                        gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_KG).Value = objtr.Requir_SNF_KG
                        gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_per).Value = objtr.Requir_SNF_Per
                        gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value = objtr.Quantity

                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_FAT_per).Value = objtr.Produced_FAT_per
                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_SNF_per).Value = objtr.Produced_SNF_per

                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_FAT_KG).Value = objtr.Produced_FAT_KG
                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_Qty).Value = objtr.Produced_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colProduced_SNF_KG).Value = objtr.Produced_SNF_KG

                        gv.Rows(gv.Rows.Count - 1).Cells(colSection_Code).Value = objtr.Section_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colSection_Desc).Value = objtr.Section_Desc

                        gv.Rows(gv.Rows.Count - 1).Cells(colShift_Code).Value = objtr.Shift_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colShift_Desc).Value = objtr.Shift_Desc

                        gv.Rows(gv.Rows.Count - 1).Cells(colSTD_Loaction_Code).Value = objtr.STD_Loaction_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colSTD_Loaction_Desc).Value = objtr.STD_Loaction_Desc

                        If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_BatchIcode.Contains(objtr.Item_Code) Then
                            arr_BatchIcode.Add(objtr.Item_Code)
                        End If
                        '' new columns
                        'gv.Rows(gv.Rows.Count - 1).Cells(colNO_SAMPLE_QC).Value = objtr.NO_SAMPLE_QC
                        'gv.Rows(gv.Rows.Count - 1).Cells(colDAMAGE_Qty).Value = objtr.DAMAGE_Qty
                        'gv.Rows(gv.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = objtr.FINAL_PROD_Qty
                    Next
                End If

                '' load issue item grid
                If obj.ArrIssueItem IsNot Nothing AndAlso obj.ArrIssueItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSTDIssueItemDetail In obj.ArrIssueItem
                        gvIssue.Rows.AddNew()
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = objtr.SNO

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = objtr.Item_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = objtr.Item_Desc
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = objtr.Item_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(objtr.Product_Type) 'objtr.Product_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = objtr.Product_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUom).Value = objtr.Unit_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = objtr.Unit_Desc

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_FAT_KG).Value = objtr.Avail_FAT_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_FAT_Per).Value = objtr.Avail_FAT_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_Qty).Value = objtr.Avail_Qty
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_SNF_KG).Value = objtr.Avail_SNF_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_SNF_Per).Value = objtr.Avail_SNF_Per

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_FAT_Per).Value = objtr.Requir_FAT_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_SNF_Per).Value = objtr.Requir_SNF_Per

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_KG).Value = objtr.Diff_FAT_KG

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_Per).Value = objtr.Diff_FAT_Per
                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_Qty).Value = objtr.Diff_Qty
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_KG).Value = objtr.Diff_SNF_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_Per).Value = objtr.Diff_SNF_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRemarks).Value = objtr.Remarks

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTO_LOC_CODE).Value = objtr.TO_LOC_CODE
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTO_LOC_DESC).Value = objtr.TO_LOC_DESC

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueStatus).Value = objtr.Issue_Status
                        '' production costing columns
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = objtr.Fat_Rate
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = objtr.SNF_Rate
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = objtr.Fat_Amt
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = objtr.SNF_Amt
                    Next
                End If

                '' load Added/Removed item grid
                If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
                    For Each objtr As clsProcessProductionARDetail In obj.ArrARItem
                        gvARDetail.Rows.AddNew()
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARSno).Value = objtr.SNO

                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Value = objtr.Item_Code
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemName).Value = objtr.Item_Desc
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemType).Value = objtr.Item_Type
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemProductType).Value = objtr.Product_Type
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARUom).Value = objtr.Unit_Code
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARUOMDesc).Value = objtr.Unit_Desc

                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARQty).Value = objtr.ADD_REMOVE_QTY
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARType).Value = objtr.ADD_REMOVE_TYPE
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocCode).Value = objtr.Loaction_Code
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocDesc).Value = objtr.Location_Desc
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARRemarks).Value = objtr.Remarks
                        'gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                        If clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Then
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItemNew
                        Else
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItem
                        End If
                        Dim loc_type As Integer = 0
                        qry = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + clsCommon.myCstr(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocCode).Value) + "'"
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "MAIN") = CompairStringResult.Equal Then
                            loc_type = 2
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SUB") = CompairStringResult.Equal Then
                            loc_type = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SEC") = CompairStringResult.Equal Then
                            loc_type = 0
                        End If
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = "0"

                        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARUom).Value), loc_type, IIf(clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = clsCommon.myCdbl(dt.Rows(0)("qty"))
                        End If


                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objtr.Product_Type) <= 0, "Others", objtr.Product_Type)

                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_Per).Value = objtr.AR_FAT_Per
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_KG).Value = objtr.AR_FAT_KG
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_Per).Value = objtr.AR_SNF_Per
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_KG).Value = objtr.AR_SNF_KG

                        If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_ARIcode.Contains(objtr.Item_Code) Then
                            arr_ARIcode.Add(objtr.Item_Code)
                        End If
                    Next
                End If
                gvARDetail.Rows.AddNew()
                '' load QC grid
                If obj.ArrQC IsNot Nothing AndAlso obj.ArrQC.Count > 0 Then

                    For Each objtr As clsProcessProductionStandardizationQCDetail In obj.ArrQC
                        gv_qc.Rows.AddNew()
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCSno).Value = objtr.sno
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCParentLineNo).Value = objtr.Parent_Line_No
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value = objtr.QC_Type
                        '=================for old records ,where no tye save
                        If clsCommon.myLen(objtr.QC_Type) <= 0 AndAlso arr_ARIcode.Contains(objtr.Item_Code) Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value = "Add/Remove"
                        ElseIf clsCommon.myLen(objtr.QC_Type) <= 0 AndAlso arr_BatchIcode.Contains(objtr.Item_Code) Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value = "Batch Order"
                        End If
                        '=========================================================================
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCItemCode).Value = objtr.Item_Code
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCItemName).Value = objtr.ItemDesc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = objtr.param_code
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = objtr.param_desc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = objtr.param_type
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = objtr.param_nature_Desc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag = objtr.param_nature
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = objtr.Standard_Range
                        'gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = objtr.urange

                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCBooleanStatus).Value = objtr.Standard_Status
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCAlphaValue).Value = objtr.Standard_Value

                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).Value = objtr.Actual_Range
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).Value = objtr.Actual_Status
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).Value = objtr.Actual_Value
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQc_Status).Value = objtr.Qc_Status
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCremarks).Value = objtr.remarks

                        If clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "R") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = False
                            'gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).Style.Font = Color.Green

                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                        ElseIf clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "A") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = False
                        ElseIf clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "B") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = False
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                        End If
                    Next
                End If

                '' load Stage Process grid
                If obj.ArrStageQC IsNot Nothing AndAlso obj.ArrStageQC.Count > 0 Then
                    For Each objtr As clsProcessProductionSTDDetail In obj.ArrStageQC
                        gvStage.Rows.AddNew()
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStageSno).Value = objtr.SNO

                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Code).Value = objtr.Stage_Code
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Code).Tag = ClsStageMaster.GetStageType(objtr.Stage_Code)

                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Desc).Value = objtr.Stage_Desc
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colReceived_Qty).Value = objtr.Received_Qty
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colUnit_Code).Value = objtr.Unit_Code
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPUnit_Desc).Value = objtr.Unit_Desc

                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colLog_Sheet_No).Value = objtr.Log_Sheet_No
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPRemarks).Value = objtr.Remarks
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPSection).Value = objtr.Section_Code
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPProdCategory).Value = objtr.Structure_Code

                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStatus).Value = objtr.Status
                        gvStage.Rows(gvStage.Rows.Count - 1).Tag = objtr.SPQCList
                        gvStage.Rows(gvStage.Rows.Count - 1).Cells(0).Tag = objtr.arrXtime

                    Next
                End If

                'fndChildBatchNo.Enabled = False
                fndMainBatchNo.Enabled = False

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                txtCode.MyReadOnly = True
                UcAttachment1.LoadData(txtCode.Value)

                If obj.Posted = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                End If
            Else
                FunReset()
            End If
            FillSection()
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Child Batch Order Detail", Me.Text)
                fndChildBatchNo.Select()
                fndChildBatchNo.Focus()
                Return
            End If
            If Not isCellValueChanged Then
                If (e.Column Is gv.Columns(colProduced_Qty)) Then '' Or (e.Column Is gv.Columns(colProduced_FAT_per))
                    isCellValueChanged = True
                    calculateProduceFATSNFKG(gv.CurrentRow.Index)
                    'gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colProduced_Qty).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
                    'gv.CurrentRow.Cells(colProduced_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.CurrentRow.Cells(colitemcode).Value, gv.CurrentRow.Cells(coluom).Value, clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_FAT_per).Value), Nothing)
                    'gv.CurrentRow.Cells(colProduced_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.CurrentRow.Cells(colitemcode).Value, gv.CurrentRow.Cells(coluom).Value, clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_SNF_per).Value), Nothing)
                    'Cal_FAT()
                    'Cal_SNF()
                    isCellValueChanged = False
                End If
                'If (e.Column Is gv.Columns(colDAMAGE_Qty)) Then
                '    isCellValueChanged = True
                '    gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colProduced_Qty).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
                '    isCellValueChanged = False
                'End If
                If e.Column Is gv.Columns(colSTD_Loaction_Code) Then
                    isCellValueChanged = True
                    OpenGVLocation(False)
                    isCellValueChanged = False
                End If
                'If (e.Column Is gv.Columns(colProduced_Qty)) Or (e.Column Is gv.Columns(colProduced_SNF_per)) Then
                '    isCellValueChanged = True
                '    Cal_SNF()
                '    isCellValueChanged = False
                'End If

            End If
        End If
    End Sub

    Private Sub OpenGVLocation(ByVal isButtonClicked As Boolean)
        If OpenAvailorEmptyStckLocationOn_Standardization Then
            Dim TransDate As Date
            If CheckStockServerDate Then
                TransDate = clsCommon.GETSERVERDATE()
            Else
                TransDate = dtpDate.Value
            End If
            Dim qry As String = clsProcessProductionStandardization.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), txtlocation.Text, clsCommon.myCstr(gv.CurrentRow.Cells(colSTD_Loaction_Code).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), True)

            gv.CurrentRow.Cells(colSTD_Loaction_Code).Value = clsCommon.ShowSelectForm("LOCSUBFND", qry, "Code", " [Type] in ('Section','Sub Location') and [Stock Qty]>=0 and is_Consumption_Location=0 ", clsCommon.myCstr(gv.CurrentRow.Cells(colSTD_Loaction_Code).Value), "Code", isButtonClicked) 'clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "')", gv.CurrentRow.Cells(colSTD_Loaction_Code).Value, False) '
        Else
            gv.CurrentRow.Cells(colSTD_Loaction_Code).Value = clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and  is_Consumption_Location=0 and Main_Location_Code='" & txtlocation.Text & "')", gv.CurrentRow.Cells(colSTD_Loaction_Code).Value, False)
        End If

        gv.CurrentRow.Cells(colSTD_Loaction_Desc).Value = clsLocation.GetName(gv.CurrentRow.Cells(colSTD_Loaction_Code).Value, Nothing)
        If EnableParameterTab() Then
            pageParameterDetail.Enabled = True
        End If
    End Sub

    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_Qty).Value)
            fat = clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_FAT_per).Value)

            If qty >= 0 AndAlso fat >= 0 Then
                fat_kg = (qty * fat) / 100
                gv.CurrentRow.Cells(colProduced_FAT_KG).Value = fat_kg
            Else
                gv.CurrentRow.Cells(colProduced_FAT_KG).Value = 0
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Cal_SNF()
        Try
            Dim qty As Decimal = Nothing
            Dim snf As Decimal = Nothing
            Dim snf_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_Qty).Value)
            snf = clsCommon.myCdbl(gv.CurrentRow.Cells(colProduced_SNF_per).Value)

            If qty >= 0 AndAlso snf_kg >= 0 Then
                snf_kg = (qty * snf) / 100
                gv.CurrentRow.Cells(colProduced_SNF_KG).Value = snf_kg
            Else
                gv.CurrentRow.Cells(colProduced_SNF_KG).Value = 0
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenLocation(ByVal isButtonClicked As Boolean)
        gv.CurrentRow.Cells(colBOM_Code).Value = clsCommon.myCstr(clsLocation.getFinder(" isnull(Is_Sub_Location,'N')='Y' and location_code in (" + arrLoc + ")", clsCommon.myCstr(gv.CurrentRow.Cells(colBOM_Code).Value), isButtonClicked))

        'If clsCommon.myLen(gv.CurrentRow.Cells(colBOM_Code).Value) > 0 Then
        '    gv.CurrentRow.Cells(colBOM_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colBOM_Code).Value) + "'"))
        '    FillAvail_Stock(CInt(gv.CurrentCell.RowIndex), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colBOM_Code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value))
        'Else
        '    gv.CurrentRow.Cells(colBOM_Code).Value = ""
        '    gv.CurrentRow.Cells(colBOM_Desc).Value = ""
        '    gv.CurrentRow.Cells(colProduced_Qty).Value = Nothing
        '    gv.CurrentRow.Cells(colRequir_FAT_KG).Value = Nothing
        '    gv.CurrentRow.Cells(colProduced_FAT_per).Value = Nothing
        '    gv.CurrentRow.Cells(colRequir_SNF_KG).Value = Nothing
        '    gv.CurrentRow.Cells(colProduced_SNF_per).Value = Nothing
        'End If
    End Sub
    Dim intCellFormaLastIndex As Integer = -1
    Private Sub gv_qc_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv_qc.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv_qc.Columns(colActual_Range) Then
                    If intCellFormaLastIndex <> gv_qc.CurrentRow.Index Then
                        intCellFormaLastIndex = gv_qc.CurrentRow.Index
                        gv_qc.Columns(colActual_Range).ReadOnly = False
                        If settTankerDispatchAvgFATSNFPer Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                                If (clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal) Then
                                    gv_qc.Columns(colActual_Range).ReadOnly = True
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_qc_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_qc.CellValueChanged
        'If Not isInsideLoadData Then
        '    If Not isCellValueChanged Then
        '        'If (e.Column Is gv_qc.Columns(colQCparamcode)) Or (e.Column Is gv_qc.Columns(colQCparam_desc)) Or (e.Column Is gv_qc.Columns(colQCparam_type)) Or (e.Column Is gv_qc.Columns(colQCparam_nature)) Then
        '        isCellValueChanged = True
        '        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
        '            gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
        '            gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
        '            gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
        '            gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
        '        End If
        '        isCellValueChanged = False
        '        'End If
        '    End If
        'End If
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv_qc.Columns(colActual_Value) Then
                    isCellValueChanged = True
                    Dim Values As String = gv_qc.CurrentRow.Cells(colQCAlphaValue).Value
                    Dim val() As String = Values.Split(",")
                    Dim lst As New List(Of String)
                    'Values = ""
                    For Each Str As String In val
                        lst.Add(Str)
                    Next
                    Values = clsCommon.GetMulcallString(lst)
                    gv_qc.CurrentRow.Cells(colActual_Value).Value = clsProcessProductionStandardization.GetFinderParameterValueList("Value in (" & Values & ")", gv_qc.CurrentRow.Cells(colActual_Value).Value, False) 'clsItemMaster.getFinder("", gvARDetail.CurrentRow.Cells(colARItemCode).Value, False)
                    isCellValueChanged = False
                ElseIf e.Column Is gv_qc.Columns(colActual_Range) Then
                    isCellValueChanged = True
                    UpdateBatchFatSNF(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCItemCode).Value), clsCommon.myCstr(gv_qc.CurrentRow.Cells(colActual_Range).Value), clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCType).Value))
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try
            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                fndChildBatchNo.Focus()
                Throw New Exception("Select Child Batch order first.")
            End If
            FillBatchOrder()
            FillIssueAgainstBatchOrder()
            FillQCGrid(0)
            FillStageDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub InitialLoadAllGrid()
        Try
            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                fndChildBatchNo.Focus()
                Throw New Exception("Select Child Batch order first.")
            End If
            FillBatchOrder()
            FillIssueAgainstBatchOrder()
            FillQCGrid(0)
            FillStageDetail()
            FillSection()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillStageDetail()
        Me.gvStage.Rows.Clear()
        Dim obj As ClsSectionStageMapping
        obj = clsProcessProductionStandardization.FillStageDetail(Me.fndChildBatchNo.Value)
        gvStage.Tag = obj.doc_code

        Dim TotalRec As Double = 0
        Dim Unit_Code As String = ""
        Dim Unit_Desc As String = ""
        For Each dr As GridViewRowInfo In gvIssue.Rows
            If clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                TotalRec = TotalRec + dr.Cells(colIssued_Qty).Value

                Unit_Code = dr.Cells(colIssueUom).Value
                Unit_Desc = dr.Cells(colIssueUOMDesc).Value
            End If

        Next
        isInsideLoadData = True
        For Each objStage As clsSectionStageMappingDetail In obj.Arr
            If clsCommon.CompairString(objStage.Stage_Type, "STD") = CompairStringResult.Equal Then
                Me.gvStage.Rows.AddNew()

                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStageSno).Value = objStage.sequnceno
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Code).Value = objStage.stagecode
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Code).Tag = objStage.Stage_Type
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStage_Desc).Value = objStage.stagedesc
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colReceived_Qty).Value = TotalRec
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colUnit_Code).Value = Unit_Code
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPUnit_Desc).Value = Unit_Desc
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colLog_Sheet_No).Value = objStage.logsheetno
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStatus).Value = ""
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPRemarks).Value = ""
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPProdCategory).Value = obj.Cate_Code
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colSPSection).Value = obj.Section_Code
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStageBatch_Code).Value = fndChildBatchNo.Value
            End If


        Next
        If gvStage.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Standardization Stages not found for selected child batch's section and structure.")
        End If

        isInsideLoadData = False
    End Sub

    Sub FillBatchOrder()
        Me.gv.Rows.Clear()
        Dim objBO As clsProcessBatchOrder = clsProcessBatchOrder.GetData(fndChildBatchNo.Value, arrLoc, NavigatorType.Current)

        isInsideLoadData = True
        If Not objBO Is Nothing Then
            For Each dr As clsProcessBatchOrderMainDetail In objBO.ArrMainItem
                Me.gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = dr.SNO
                gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Code).Value = dr.bomcode
                gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Desc).Value = dr.bomcode
                gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = dr.icode
                gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = dr.iname
                gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = dr.itype
                gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select coalesce(Product_Type,'') as Product_Type from TSPL_ITEM_MASTER where item_code='" & dr.icode & "'"))
                gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = dr.UOM
                gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" & dr.UOM & "' "))
                gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value = dr.qty
                gv.Rows(gv.Rows.Count - 1).Cells(colShift_Code).Value = dr.shiftcode
                gv.Rows(gv.Rows.Count - 1).Cells(colShift_Desc).Value = dr.shiftname
                gv.Rows(gv.Rows.Count - 1).Cells(colSection_Code).Value = dr.sectioncode
                gv.Rows(gv.Rows.Count - 1).Cells(colSection_Desc).Value = dr.sectionname

                Dim dtQC As DataTable = clsProcessProductionStandardization.GetItemQCParameter(dr.icode)
                Dim drQC() As DataRow = dtQC.Select("type='FAT'")
                If drQC.Length > 0 Then
                    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_per).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range"))
                    'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_KG).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range")) * dr.qty / 100
                End If

                drQC = dtQC.Select("type='SNF'")
                If drQC.Length > 0 Then
                    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_per).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range"))
                    'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_KG).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range")) * dr.qty / 100
                End If

                '' set required fat/snf
                gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value, gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_per).Value), Nothing)
                gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value, gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value), clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_per).Value), Nothing)


                gv.Rows(gv.Rows.Count - 1).Cells(colProduced_Qty).Value = dr.qty
                '' set produced fat/snf
                calculateProduceFATSNFKG(gv.Rows.Count - 1)
            Next
        End If
        calculateALL()
        isInsideLoadData = False
    End Sub

    Sub calculateProduceFATSNFKG(ByVal intRowNo As Integer)
        gv.Rows(intRowNo).Cells(colProduced_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intRowNo).Cells(colitemcode).Value, gv.Rows(intRowNo).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(intRowNo).Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.Rows(intRowNo).Cells(colProduced_FAT_per).Value), Nothing)
        gv.Rows(intRowNo).Cells(colProduced_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intRowNo).Cells(colitemcode).Value, gv.Rows(intRowNo).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(intRowNo).Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.Rows(intRowNo).Cells(colProduced_SNF_per).Value), Nothing)
        calculateALL()
    End Sub

    Sub FillIssueAgainstBatchOrder()
        Me.gvIssue.Rows.Clear()
        Dim dt As DataTable = clsProcessProductionStandardization.GetIssueAgainstBatch(Me.fndChildBatchNo.Value, Me.txtCode.Value)
        Dim totalIssued As Decimal = 0

        isInsideLoadData = True
        For Each dr As DataRow In dt.Rows
            If clsCommon.myCdbl(dr.Item("Issue_Qty")) > 0 Then
                Me.gvIssue.Rows.AddNew()
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = gvIssue.Rows.Count
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = dr.Item("Item_Code")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = dr.Item("Item_Desc")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = ItemType(dr.Item("Item_Type"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(dr.Item("Product_Type"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = dr.Item("Product_Type")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUom).Value = dr.Item("Unit_Code")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = dr.Item("Unit_Desc")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_Qty).Value = dr.Item("Issue_Qty")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_FAT_KG).Value = dr.Item("Issued_FAT_KG")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_FAT_Per).Value = dr.Item("Issued_FAT_Pers")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_SNF_KG).Value = dr.Item("Issued_SNF_KG")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_SNF_Per).Value = dr.Item("Issued_SNF_Pers")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTO_LOC_CODE).Value = clsCommon.myCstr(dr.Item("To_Location_Code"))

                '' producton costing columns
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = dr.Item("Issued_FAT_Rate")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = dr.Item("Issued_SNF_Rate")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = dr.Item("Issued_FAT_Amt")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = dr.Item("Issued_SNF_Amt")

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTO_LOC_DESC).Value = clsLocation.GetName(clsCommon.myCstr(dr.Item("To_Location_Code")), Nothing)
                If clsCommon.CompairString(gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_FAT_Per).Value = gv.Rows(0).Cells(colRequir_FAT_per).Value
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_SNF_Per).Value = gv.Rows(0).Cells(colRequir_SNF_per).Value
                Else
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_FAT_Per).Value = 0
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRequir_SNF_Per).Value = 0
                End If

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_Per).Value = gv.Rows(0).Cells(colRequir_FAT_per).Value - gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_FAT_Per).Value
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_KG).Value = gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_Per).Value * gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_Qty).Value / 100

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_Per).Value = gv.Rows(0).Cells(colRequir_SNF_per).Value - gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_SNF_Per).Value
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_KG).Value = gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_Per).Value * gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_Qty).Value / 100

                totalIssued = totalIssued + gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssued_Qty).Value
            End If
        Next

        For intloop As Integer = 0 To gv.Rows.Count - 1
            calculateProduceFATSNFKG(intloop)
            'gv.Rows(intloop).Cells(colProduced_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intloop).Cells(colitemcode).Value, gv.Rows(intloop).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduced_FAT_per).Value), Nothing)
            'gv.Rows(intloop).Cells(colProduced_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intloop).Cells(colitemcode).Value, gv.Rows(intloop).Cells(coluom).Value, clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduced_Qty).Value), clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduced_SNF_per).Value), Nothing)
        Next
        calculateALL()
        isInsideLoadData = False
    End Sub

    Sub FillQCGrid(ByVal ParentLineNo As Integer, Optional ByVal IsAddRemoveItem As Boolean = False, Optional ByVal Item_Code As String = "")
        Dim dt As New DataTable
        If IsAddRemoveItem = False Then
            dt = clsProcessProductionStandardization.GetQCParameters(Me.fndChildBatchNo.Value)
            gv_qc.Rows.Clear()
        Else
            dt = clsProcessProductionStandardization.GetQCParametersForItem(Item_Code)
        End If


        isInsideLoadData = True
        For Each dr As DataRow In dt.Rows
            gv_qc.Rows.AddNew()
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCParentLineNo).Value = ParentLineNo
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCSno).Value = CInt(dr("sno"))
            If IsAddRemoveItem Then
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value = "Add/Remove"
            Else
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value = "Batch Order"
            End If
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCItemName).Value = clsCommon.myCstr(dr("Item_Desc"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = clsCommon.myCstr(dr("Code"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = clsCommon.myCstr(dr("parameterdesc"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = clsCommon.myCstr(dr("Type"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = clsCommon.myCstr(dr("Nature"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag = clsCommon.myCstr(dr("Nature_Code"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = clsCommon.myCdbl(dr("Actual_Range"))
            'gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = clsCommon.myCdbl(dr("Upper_range"))

            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCBooleanStatus).Value = clsCommon.myCstr(dr("Actual_Status"))
            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCAlphaValue).Value = clsCommon.myCstr(dr("Actual_Value"))
            'gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQc_Status).Value = clsCommon.myCstr(dr("QC_Status"))
            If clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "R") = CompairStringResult.Equal Then
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = False

                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).Value = clsCommon.myCdbl(dr("Actual_Range"))
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
                UpdateBatchFatSNF(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCItemCode).Value), clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).Value), clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value), clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCType).Value))

            ElseIf clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "A") = CompairStringResult.Equal Then
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = False

                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).Value = clsCommon.myCstr(dr("Actual_Value"))
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
            ElseIf clsCommon.CompairString(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Tag, "B") = CompairStringResult.Equal Then
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).ReadOnly = False
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True

                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colActual_Status).Value = clsCommon.myCstr(dr("Actual_Status"))
                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
            End If
        Next
        calculateALL()
        isInsideLoadData = False
    End Sub

    Sub updateBatchGridParameter()
        For Each grow As GridViewRowInfo In gv_qc.Rows
            isCellValueChanged = True
            UpdateBatchFatSNF(clsCommon.myCstr(grow.Cells(colQCItemCode).Value), clsCommon.myCdbl(grow.Cells(colActual_Range).Value), clsCommon.myCstr(grow.Cells(colQCparam_type).Value), clsCommon.myCstr(grow.Cells(colQCType).Value))
            isCellValueChanged = False
        Next

    End Sub

    Private Sub gvARDetail_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvARDetail.CellFormatting
        Try
            If e.Column Is gvARDetail.Columns(colARQty) Then
                If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value) = 0 AndAlso clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value) = 0 Then
                    gvARDetail.Columns(colARQty).ReadOnly = False
                Else
                    gvARDetail.Columns(colARQty).ReadOnly = AutoCalcQtyAddRem
                End If
            ElseIf e.Column Is gvARDetail.Columns(colAR_FAT_Per) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                    gvARDetail.Columns(colAR_FAT_Per).ReadOnly = True
                Else
                    gvARDetail.Columns(colAR_FAT_Per).ReadOnly = False
                End If
            ElseIf e.Column Is gvARDetail.Columns(colAR_SNF_Per) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                    gvARDetail.Columns(colAR_SNF_Per).ReadOnly = True
                Else
                    gvARDetail.Columns(colAR_SNF_Per).ReadOnly = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvARDetail_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvARDetail.CurrentColumnChanged
        If gvARDetail.RowCount > 0 Then
            Dim intCurrRow As Integer = gvARDetail.CurrentRow.Index
            gvARDetail.CurrentRow.Cells(colARSno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvARDetail.Rows.Count - 1 Then
                gvARDetail.Rows.AddNew()
                gvARDetail.CurrentRow = gvARDetail.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvARDetail_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvARDetail.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gvARDetail.Columns(colARItemCode) Then
                    If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Select Child Batch Order Detail", Me.Text)
                        fndChildBatchNo.Select()
                        fndChildBatchNo.Focus()
                        Return
                    End If

                    isCellValueChanged = True
                    '' filter for item_used_asapplied by Panch Raj on 10-07-2019 against ticket no:ERO/12/06/18-000342
                    gvARDetail.CurrentRow.Cells(colARItemCode).Value = clsItemMaster.getFinder(If(ShowOnlyProdItemsOnAddRemove = True, " Item_Used_as='P' ", ""), gvARDetail.CurrentRow.Cells(colARItemCode).Value, False)
                    Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, NavigatorType.Current)
                    If Not objItem Is Nothing Then
                        RemoveQCGrid() ''BHA/10/08/18-000411 by balwinder on 17/08/2018
                        gvARDetail.CurrentRow.Cells(colARItemName).Value = objItem.Item_Desc
                        gvARDetail.CurrentRow.Cells(colARItemType).Value = objItem.Item_Type
                        gvARDetail.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                        gvARDetail.CurrentRow.Cells(colARItemProductType).Tag = objItem.Product_Type
                        gvARDetail.CurrentRow.Cells(colARIsBatchItem).Value = objItem.Is_Batch_Item
                        gvARDetail.CurrentRow.Cells(colARUom).Value = objItem.Unit_Code
                        gvARDetail.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
                        gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value = Nothing
                        gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = Nothing
                        gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value = Nothing
                        gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = Nothing
                        If clsCommon.CompairString(objItem.Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(objItem.Product_Type, "MP") = CompairStringResult.Equal Then
                            FillQCGrid(gvARDetail.CurrentRow.Index, True, gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                        End If
                        SetARBalance()
                    End If
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARUom) Then
                    isCellValueChanged = True
                    OpenUOM(False)
                    If clsCommon.myLen(gvARDetail.CurrentRow.Cells(colARUom).Value) > 0 Then
                        gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                        gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                        calculateALL()
                    End If
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARLocCode) Then
                    isCellValueChanged = True
                    OpenARLocationCode(False)
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARType) Then
                    isCellValueChanged = True
                    calculateALL()
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARQty) AndAlso AutoCalcQtyAddRem = False Then
                    'BHA/10/08/18-000411 by balwinder on 09/08/2017
                    isCellValueChanged = True
                    gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    OpenBatchItem()
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_FAT_Per) Then
                    isCellValueChanged = True
                    For ii As Integer = 0 To gv_qc.Rows.Count - 1
                        If (gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvARDetail.CurrentRow.Index Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                    gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                                    gv_qc.Rows(ii).Cells(colActual_Range).Value = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_SNF_Per) Then
                    isCellValueChanged = True
                    For ii As Integer = 0 To gv_qc.Rows.Count - 1
                        If (gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvARDetail.CurrentRow.Index Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                    gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                                    gv_qc.Rows(ii).Cells(colActual_Range).Value = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_FAT_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value) > 0 Then
                        Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, 1, 100, Nothing)
                        gvARDetail.CurrentRow.Cells(colARQty).Value = gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value * 100 / (gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value * IIf(conFat = 0, 1, conFat))
                    End If
                    gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_SNF_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value) > 0 Then
                        Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, 1, 100, Nothing)
                        gvARDetail.CurrentRow.Cells(colARQty).Value = gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value * 100 / (gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value * IIf(conFat = 0, 1, conFat))
                    End If
                    gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub OpenARLocationCode(ByVal isButtonClicked As Boolean)
        Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
        Dim strItemLoc As String = ""


        If OpenAvailorEmptyStckLocationOn_Standardization Then
            If ShowLocationItemLocationwise = 1 AndAlso clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                strItemLoc = " and Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value) & "')"
            End If
            Dim qry As String = clsProcessProductionStandardization.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value), IIf(clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
            gvARDetail.CurrentRow.Cells(colARLocCode).Value = clsCommon.ShowSelectForm("LOCSUBLOCSTCKFND", qry, "Code", " [Type] in ('Section','Sub Location') and [Stock Qty]>=0 " & strItemLoc, clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value), "Code", isButtonClicked) '
        Else
            If ShowLocationItemLocationwise = 1 AndAlso clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                strItemLoc = " and Location_Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value) & "')"
            End If
            gvARDetail.CurrentRow.Cells(colARLocCode).Value = clsLocation.getFinder("(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "') or Location_Code='" & txtlocation.Text & "')" & strItemLoc, gvARDetail.CurrentRow.Cells(colARLocCode).Value, False)
        End If

        gvARDetail.CurrentRow.Cells(colARLocDesc).Value = clsLocation.GetName(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value), Nothing)
        SetARBalance()
    End Sub

    Sub SetARBalance()
        Dim loc_type As Integer = 0
        qry = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value) + "'"
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "MAIN") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SUB") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SEC") = CompairStringResult.Equal Then
            loc_type = 0
        End If
        gvARDetail.CurrentRow.Cells(colARAvailQty).Value = "0"

        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value), loc_type, IIf(clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvARDetail.CurrentRow.Cells(colARAvailQty).Value = clsCommon.myCdbl(dt.Rows(0)("qty"))
            If settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                Dim dblFATPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value), clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("fat_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                Dim dblSNFPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value), clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("snf_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value = dblFATPer
                gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value = dblSNFPer
                For ii As Integer = 0 To gv_qc.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCItemCode).Value), clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvARDetail.CurrentRow.Index Then
                            gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblFATPer
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblSNFPer
                            End If
                        End If
                    End If
                Next
            End If
        End If
    End Sub


    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gvARDetail.CurrentRow.Cells(colARItemCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select item code first", Me.Text)
            Exit Sub
        End If
        qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as Description,TSPL_ITEM_UOM_DETAIL.Weight,TSPL_ITEM_UOM_DETAIL.Stocking_Unit as [Stocking Unit],TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Conversion Factor] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        gvARDetail.CurrentRow.Cells(colARUom).Value = clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value) + "'", clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value), "Code", isButtonClicked)
        If clsCommon.myLen(gvARDetail.CurrentRow.Cells(colARUom).Value) > 0 Then
            gvARDetail.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(gvARDetail.CurrentRow.Cells(colARUom).Value, Nothing)
        Else
            gvARDetail.CurrentRow.Cells(colARUOMDesc).Value = ""
        End If
        SetARBalance()
    End Sub

    Private Sub gvIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvIssue.CellValueChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Child Batch Order Detail", Me.Text)
                fndChildBatchNo.Select()
                fndChildBatchNo.Focus()
                Return
            End If
            If Not isCellValueChanged Then
                If (e.Column Is gvIssue.Columns(colIssueStatus)) Then
                    isCellValueChanged = True
                    If clsCommon.myLen(gvIssue.Rows(e.RowIndex).Cells(colIssueStatus).Value) > 0 Then
                        If EnableAddRemAndStageTab() Then
                            pageAddRemoveDetail.Enabled = True
                            pageStageDetail.Enabled = True
                        End If
                    End If
                    isCellValueChanged = False
                End If

                'If (e.Column Is gvIssue.Columns(colIssued_FAT_Per)) Then
                '    isCellValueChanged = True
                '    gvIssue.Rows(e.RowIndex).Cells(colIssued_FAT_KG).Value = gvIssue.Rows(e.RowIndex).Cells(colIssued_FAT_Per).Value * gvIssue.Rows(e.RowIndex).Cells(colIssued_Qty).Value / 100
                '    For Each drQC As GridViewRowInfo In gv_qc.Rows
                '        If clsCommon.CompairString(drQC.Cells(colQCparam_type).Value, "FAT") = CompairStringResult.Equal Then
                '            drQC.Cells(colQCrange1).Value = e.Value
                '            drQC.Cells(colQCrange2).Value = e.Value
                '            If gv.Rows.Count > 0 Then
                '                gv.Rows(0).Cells(colProduced_FAT_KG).Value = gv.Rows(e.RowIndex).Cells(colProduced_Qty).Value * e.Value / 100
                '            End If
                '            Exit For
                '        End If
                '    Next
                '    If gv.Rows.Count > 0 Then
                '        gvIssue.Rows(e.RowIndex).Cells(colDiff_FAT_Per).Value = gv.Rows(0).Cells(colProduced_FAT_per).Value - gvIssue.Rows(e.RowIndex).Cells(colIssued_FAT_Per).Value
                '        gvIssue.Rows(e.RowIndex).Cells(colDiff_FAT_KG).Value = gvIssue.Rows(e.RowIndex).Cells(colDiff_FAT_Per).Value * gvIssue.Rows(e.RowIndex).Cells(colIssued_Qty).Value / 100

                '    End If
                '    isCellValueChanged = False
                'End If

                'If (e.Column Is gvIssue.Columns(colIssued_SNF_Per)) Then
                '    isCellValueChanged = True
                '    gvIssue.Rows(e.RowIndex).Cells(colIssued_SNF_KG).Value = gvIssue.Rows(e.RowIndex).Cells(colIssued_SNF_Per).Value * gvIssue.Rows(e.RowIndex).Cells(colIssued_Qty).Value / 100
                '    For Each drQC As GridViewRowInfo In gv_qc.Rows
                '        If clsCommon.CompairString(drQC.Cells(colQCparam_type).Value, "SNF") = CompairStringResult.Equal Then
                '            drQC.Cells(colQCrange1).Value = e.Value
                '            drQC.Cells(colQCrange2).Value = e.Value
                '            If gv.Rows.Count > 0 Then
                '                gv.Rows(0).Cells(colProduced_SNF_KG).Value = gv.Rows(e.RowIndex).Cells(colProduced_Qty).Value * e.Value / 100
                '            End If
                '            Exit For
                '        End If
                '    Next
                '    If gv.Rows.Count > 0 Then
                '        gvIssue.Rows(e.RowIndex).Cells(colDiff_SNF_Per).Value = gv.Rows(0).Cells(colProduced_SNF_per).Value - gvIssue.Rows(e.RowIndex).Cells(colIssued_SNF_Per).Value
                '        gvIssue.Rows(e.RowIndex).Cells(colDiff_SNF_KG).Value = gvIssue.Rows(e.RowIndex).Cells(colDiff_SNF_Per).Value * gvIssue.Rows(e.RowIndex).Cells(colIssued_Qty).Value / 100
                '    End If
                '    isCellValueChanged = False
                'End If
            End If
        End If
    End Sub

    Function EnableAddRemAndStageTab() As Boolean
        For Each grow As GridViewRowInfo In gvIssue.Rows
            If clsCommon.myLen(grow.Cells(colIssueStatus).Value) <= 0 Then
                Return False
            End If
        Next
        Return True
    End Function

    Function EnableBatchItemTab() As Boolean
        For Each grow As GridViewRowInfo In gvStage.Rows
            If clsCommon.CompairString(grow.Cells(colStatus).Value, "0") = CompairStringResult.Equal Then
                Return False
            End If
        Next
        Return True
    End Function

    Function EnableParameterTab() As Boolean
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myLen(grow.Cells(colSTD_Loaction_Code).Value) <= 0 Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub gvStage_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvStage.CellValueChanged
        If Not isInsideLoadData Then

            If Not isCellValueChanged Then
                If e.Column Is gvStage.Columns(colStatus) Then
                    isCellValueChanged = False
                    'If e.RowIndex > 0 Then
                    '    Dim PrevStatus As String = gvStage.Rows(e.RowIndex - 1).Cells(colStatus).Value
                    '    If clsCommon.CompairString(PrevStatus, "1") <> CompairStringResult.Equal Then
                    '        clsCommon.MyMessageBoxShow("First complete previous stage status.")
                    '        gvStage.CurrentRow.Cells(colStatus).Value = ""

                    '        For Each grow As GridViewRowInfo In gvStage.Rows
                    '            If grow.Tag Is Nothing Then
                    '                clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & ".")
                    '                gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                    '                isCellValueChanged = False
                    '                Exit Sub
                    '            End If
                    '            If clsCommon.CompairString(grow.Cells(colStatus).Value, "1") <> CompairStringResult.Equal Then
                    '                clsCommon.MyMessageBoxShow("QC Log Sheet is not completed for stage " & grow.Cells(colStage_Code).Value & ".")
                    '                gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                    '                isCellValueChanged = False
                    '                Exit Sub
                    '            End If
                    '            For Each obj As clsPPSTDLogSheetDetail In grow.Tag
                    '                If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                    '                    clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                    '                    gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                    '                    isCellValueChanged = False
                    '                    Exit Sub
                    '                End If
                    '            Next
                    '        Next
                    '    End If

                    'End If
                    '' for skip
                    If clsCommon.CompairString(gvStage.Rows(e.RowIndex).Cells(colStatus).Value, "2") = CompairStringResult.Equal Then
                        If EnableBatchItemTab() Then
                            pageBatchDetail.Enabled = True
                        End If
                        isCellValueChanged = False
                        Exit Sub
                    End If
                    If e.RowIndex > 0 Then
                        Dim PrevStatus As String = gvStage.Rows(e.RowIndex - 1).Cells(colStatus).Value
                        If clsCommon.CompairString(PrevStatus, "1") <> CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("First complete previous stage status.")
                            gvStage.CurrentRow.Cells(colStatus).Value = ""
                        Else
                            For Each grow As GridViewRowInfo In gvStage.Rows
                                If grow.Index > e.RowIndex Then
                                    isCellValueChanged = False
                                    Exit Sub
                                End If
                                If grow.Tag Is Nothing Then
                                    clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & ".")
                                    gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                                    isCellValueChanged = False
                                    Exit Sub
                                End If
                                If clsCommon.CompairString(grow.Cells(colStatus).Value, "1") = CompairStringResult.Equal Then
                                    'clsCommon.MyMessageBoxShow("QC Log Sheet is not completed for stage " & grow.Cells(colStage_Code).Value & ".")
                                    'gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                                    For Each obj As clsPPSTDLogSheetDetail In grow.Tag
                                        If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                                            clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                                            gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                                            isCellValueChanged = False
                                            Exit Sub
                                        End If
                                    Next

                                End If

                            Next
                        End If
                    Else
                        If clsCommon.CompairString(gvStage.Rows(e.RowIndex).Cells(colStatus).Value, "1") = CompairStringResult.Equal Then
                            For Each grow As GridViewRowInfo In gvStage.Rows
                                If grow.Index > e.RowIndex Then
                                    isCellValueChanged = False
                                    Exit Sub
                                End If
                                If grow.Tag Is Nothing Then
                                    clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & ".")
                                    gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                                    isCellValueChanged = False
                                    Exit Sub
                                Else
                                    For Each obj As clsPPSTDLogSheetDetail In grow.Tag
                                        If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                                            clsCommon.MyMessageBoxShow("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                                            gvStage.Rows(e.RowIndex).Cells(colStatus).Value = 0
                                            isCellValueChanged = False
                                            Exit Sub
                                        End If
                                    Next
                                End If

                            Next

                        End If
                        isCellValueChanged = False
                    End If
                    If EnableBatchItemTab() Then
                        pageBatchDetail.Enabled = True
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub gvStage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvStage.KeyDown
        If Not gvStage.CurrentRow Is Nothing AndAlso e.KeyCode = (Keys.F4) Then
            Dim frm As New frmPPStageProcessQCLogSheet
            frm.Stage_Code = gvStage.CurrentRow.Cells(colStage_Code).Value
            frm.txtstagecode.Value = gvStage.CurrentRow.Cells(colStage_Code).Value
            frm.txtstagename.Text = gvStage.CurrentRow.Cells(colStage_Desc).Value
            frm.Stage_Desc = gvStage.CurrentRow.Cells(colStage_Desc).Value
            frm.STAGE_PROCESS_CODE = Me.txtCode.Value
            frm.txtcategorycode.Value = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.ProductionCategoryCode = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.ProductionCategoryDesc = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.txtCode.Value = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
            frm.Log_Sheet_No = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
            frm.Sequence = gvStage.CurrentRow.Cells(colStageSno).Value
            frm.txtsequnce.Text = gvStage.CurrentRow.Cells(colStageSno).Value
            frm.Stage_Type = gvStage.CurrentRow.Cells(colStage_Code).Tag

            frm.objListSTD = gvStage.CurrentRow.Tag
            frm.Batch_Code = fndChildBatchNo.Value
            frm.arrXtime = gvStage.CurrentRow.Cells(0).Tag
            If clsCommon.myLen(gvStage.Tag) <= 0 Then
                clsCommon.MyMessageBoxShow("Permission denied.")
                Exit Sub
            End If
            frm.objListUsers = clsSectionStageMapping_User.GetLogsheetUsers(gvStage.Tag, frm.Stage_Code, Nothing)
            frm.ShowDialog()
            If frm.IsCancelled = False Then
                gvStage.CurrentRow.Tag = frm.objListSTD
                gvStage.CurrentRow.Cells(0).Tag = frm.arrXtime
            End If

        End If
    End Sub

    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsProcessProductionStandardization.UnpostData(txtCode.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillSection()
        gvSectionStock.DataSource = clsProductionEntry.GetSectionStock(lblConsmSectionLocCode.Text)
        gvSectionStock.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
    End Sub

    Sub FillSectionHistory(ByVal Item_Code As String)
        gvSectionStockHistory.DataSource = clsProductionEntry.GetSectionStockHistory(lblConsmSectionLocCode.Text, Item_Code)
        gvSectionStockHistory.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        RadPageView1.SelectedPage = pageSectionStockHistory
    End Sub

    Private Sub gvSectionStock_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSectionStock.DoubleClick
        If gvSectionStock.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        FillSectionHistory(gvSectionStock.SelectedRows(0).Cells(0).Value)
    End Sub

    Sub UpdateBatchFatSNF(ByVal Item_Code As String, ByVal Value As Decimal, ByVal Type As String, ByVal QC_Type As String)
        '' update fat/snf in batch tab
        If clsCommon.CompairString(QC_Type, "Batch Order") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.CompairString(grow.Cells(colitemcode).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colProduced_FAT_per).Value = Value
                        grow.Cells(colProduced_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(coluom).Value, grow.Cells(colProduced_Qty).Value, grow.Cells(colProduced_FAT_per).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colProduced_SNF_per).Value = Value
                        grow.Cells(colProduced_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(coluom).Value, grow.Cells(colProduced_Qty).Value, grow.Cells(colProduced_SNF_per).Value, Nothing)
                    End If
                    'Exit Sub
                End If
            Next
        End If


        '' update fat/snf in add/remove tab
        If clsCommon.CompairString(QC_Type, "Add/Remove") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvARDetail.Rows
                If clsCommon.CompairString(grow.Cells(colARItemCode).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colAR_FAT_Per).Value = Value
                        If Value > 0 AndAlso AutoCalcQtyAddRem = True Then
                            Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(grow.Cells(colARItemCode).Value, grow.Cells(colARUom).Value, 1, 100, Nothing)
                            grow.Cells(colARQty).Value = grow.Cells(colAR_FAT_KG).Value * 100 / (Value * IIf(conFat = 0, 1, conFat))
                        Else
                            grow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colARUom).Value, grow.Cells(colARQty).Value, grow.Cells(colAR_FAT_Per).Value, Nothing)
                        End If
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colAR_SNF_Per).Value = Value
                        If Value > 0 AndAlso AutoCalcQtyAddRem = True Then
                            Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(grow.Cells(colARItemCode).Value, grow.Cells(colARUom).Value, 1, 100, Nothing)
                            grow.Cells(colARQty).Value = grow.Cells(colAR_SNF_KG).Value * 100 / (Value * IIf(conFat = 0, 1, conFat))
                        Else
                            grow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colARUom).Value, grow.Cells(colARQty).Value, grow.Cells(colAR_SNF_Per).Value, Nothing)
                        End If
                    End If
                End If
            Next
        End If ''end cond.

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            clsProcessProductionStandardization.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub calculateALL()
        ''BHA/08/08/18-000395 by balwinder on 13/08/2018
        Dim dblQty As Decimal = 0
        Dim dblFATKG As Decimal = 0
        Dim dblSNFKG As Decimal = 0
        For ii As Integer = 0 To gv.Rows.Count - 1
            dblQty += clsCommon.myCdbl(gv.Rows(ii).Cells(colQuantity).Value)
            dblFATKG += clsCommon.myCdbl(gv.Rows(ii).Cells(colRequir_FAT_KG).Value)
            dblSNFKG += clsCommon.myCdbl(gv.Rows(ii).Cells(colRequir_SNF_KG).Value)
        Next
        lblTotBatchQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotBatchFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotBatchSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))

        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gv.Rows.Count - 1
            dblQty += clsCommon.myCdbl(gv.Rows(ii).Cells(colProduced_Qty).Value)
            dblFATKG += clsCommon.myCdbl(gv.Rows(ii).Cells(colProduced_FAT_KG).Value)
            dblSNFKG += clsCommon.myCdbl(gv.Rows(ii).Cells(colProduced_SNF_KG).Value)
        Next
        lblTotProduceQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotProduceFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotProduceSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))

        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        Dim dblFATAmt As Decimal = 0
        Dim dblSNFAmt As Decimal = 0
        For ii As Integer = 0 To gvIssue.Rows.Count - 1
            dblQty += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssued_Qty).Value)

            If clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssued_FAT_KG).Value) > 0 Then
                dblFATKG += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssued_FAT_KG).Value)
                dblFATAmt += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssue_Fat_Amt).Value)
            End If
            If clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssued_SNF_KG).Value) > 0 Then
                dblSNFKG += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssued_SNF_KG).Value)
                dblSNFAmt += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssue_SNF_Amt).Value)
            End If
        Next
        lblTotIssueQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotIssueFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotIssueSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))
        If dblFATAmt <= 0 OrElse dblFATKG <= 0 Then
            lblAvgRateFAT.Text = clsCommon.myFormat(Math.Round(0, 2, MidpointRounding.ToEven))
        Else
            lblAvgRateFAT.Text = clsCommon.myFormat(Math.Round(dblFATAmt / dblFATKG, 2, MidpointRounding.ToEven))
        End If
        If dblSNFAmt <= 0 OrElse dblSNFKG <= 0 Then
            lblAvgRateSNF.Text = clsCommon.myFormat(Math.Round(0, 2, MidpointRounding.ToEven))
        Else
            lblAvgRateSNF.Text = clsCommon.myFormat(Math.Round(dblSNFAmt / dblSNFKG, 2, MidpointRounding.ToEven))
        End If

        lblTotDifferenceQty.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueQty.Text) - clsCommon.myCdbl(lblTotProduceQty.Text), 2, MidpointRounding.ToEven))
        lblTotDifferenceFATKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueFATKG.Text) - clsCommon.myCdbl(lblTotProduceFATKG.Text), 2, MidpointRounding.ToEven))
        lblTotDifferenceSNFKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueSNFKG.Text) - clsCommon.myCdbl(lblTotProduceSNFKG.Text), 2, MidpointRounding.ToEven))

        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gvARDetail.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.Rows(ii).Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                dblQty += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colARQty).Value)
                dblFATKG += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colAR_FAT_KG).Value)
                dblSNFKG += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colAR_SNF_KG).Value)
            End If
        Next
        lblTotAddedQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotAddedFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotAddedSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))


        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gvARDetail.Rows.Count - 1
            If Not clsCommon.CompairString(clsCommon.myCstr(gvARDetail.Rows(ii).Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                dblQty += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colARQty).Value)
                dblFATKG += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colAR_FAT_KG).Value)
                dblSNFKG += clsCommon.myCdbl(gvARDetail.Rows(ii).Cells(colAR_SNF_KG).Value)
            End If
        Next
        lblTotRemovedQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotRemovedFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotRemovedSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))


        dblQty = clsCommon.myCdbl(lblTotAddedQty.Text) - clsCommon.myCdbl(lblTotRemovedQty.Text)
        dblFATKG = clsCommon.myCdbl(lblTotAddedFATKG.Text) - clsCommon.myCdbl(lblTotRemovedFATKG.Text)
        dblSNFKG = clsCommon.myCdbl(lblTotAddedSNFKG.Text) - clsCommon.myCdbl(lblTotRemovedSNFKG.Text)

        lblTotAddRemoveQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotAddRemoveFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotAddRemoveSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))


        lblTotNetQty.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotDifferenceQty.Text) + clsCommon.myCdbl(lblTotAddRemoveQty.Text), 2, MidpointRounding.ToEven))
        lblTotNetFATKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotDifferenceFATKG.Text) + clsCommon.myCdbl(lblTotAddRemoveFATKG.Text), 2, MidpointRounding.ToEven))
        lblTotNetSNFKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotDifferenceSNFKG.Text) + clsCommon.myCdbl(lblTotAddRemoveSNFKG.Text), 2, MidpointRounding.ToEven))

        lblJWEFATKg.Text = "0.00"
        lblJWEFATAmt.Text = "0.00"
        lblJWESNFKg.Text = "0.00"
        lblJWESNFAmt.Text = "0.00"
        Dim strStructureCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PP_BATCH_ORDER_HEAD.Structure_Code  from TSPL_PP_BATCH_ORDER_HEAD where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" + fndChildBatchNo.Value + "'"))
        Dim objJobEst As clsJWFormulaSoln = clsJWFormulaSoln.CalculateEstimate(dtpDate.Value, strStructureCode, txtlocation.Text, clsCommon.myCdbl(IIf(SettUseProductFATSNFKgForEstimationCost, lblTotProduceFATKG.Text, lblTotIssueFATKG.Text)), clsCommon.myCdbl(IIf(SettUseProductFATSNFKgForEstimationCost, lblTotProduceSNFKG.Text, lblTotIssueSNFKG.Text)))
        If objJobEst IsNot Nothing Then
            If objJobEst.Type = 1 Then
                lblJWEFATKg.Text = clsCommon.myFormat(Math.Round(objJobEst.EstFATKG, 2, MidpointRounding.ToEven))
                lblJWEFATAmt.Text = clsCommon.myFormat(Math.Round(objJobEst.EstFATKG * clsCommon.myCdbl(lblAvgRateFAT.Text), 2, MidpointRounding.ToEven))
            ElseIf objJobEst.Type = 2 Then
                lblJWESNFKg.Text = clsCommon.myFormat(Math.Round(objJobEst.EstSNFKG, 2, MidpointRounding.ToEven))
                lblJWESNFAmt.Text = clsCommon.myFormat(Math.Round(objJobEst.EstSNFKG * clsCommon.myCdbl(lblAvgRateSNF.Text), 2, MidpointRounding.ToEven))
            End If
        End If
    End Sub

    Private Sub gvARDetail_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvARDetail.UserDeletedRow
        calculateALL()
    End Sub

    Private Sub gvARDetail_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvARDetail.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            RemoveQCGrid()
        End If
    End Sub

    Sub RemoveQCGrid()
        For ii As Integer = gv_qc.Rows.Count - 1 To 0 Step -1
            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvARDetail.CurrentRow.Index Then
                    gv_qc.Rows.RemoveAt(ii)
                End If
            End If
        Next
    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gvARDetail.CurrentRow.Cells(colARIsBatchItem).Value) Then
            If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                    Dim frm As frmBatchItemOutNew = New frmBatchItemOutNew()
                    frm.strItemCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemName).Value)
                    frm.strLocationCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value)
                    frm.strCurrDocNo = txtCode.Value
                    frm.strCurrDocType = MyBase.Form_ID
                    frm.strUOM = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value)
                    frm.dblqty = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colARQty).Value)
                    frm.arr = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                    If RunBatchFifowise Then
                        frm.OpenSerialList(0, "")
                        gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    Else
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                        End If
                    End If
                Else
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemName).Value)
                    frm.strLocationCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARLocCode).Value)
                    frm.strCurrDocNo = txtCode.Value
                    frm.strCurrDocType = MyBase.Form_ID
                    frm.strUOM = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value)
                    frm.dblMRP = 0
                    frm.dblqty = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colARQty).Value)
                    frm.arr = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                    If RunBatchFifowise Then
                        frm.OpenSerialList(0, "")
                        gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    Else
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                        End If
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                    Dim frm As frmBatchItemIn_ForMilkItem = New frmBatchItemIn_ForMilkItem()
                    frm.strItemCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemName).Value)
                    frm.dblqty = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colARQty).Value)
                    frm.strUOM = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value)
                    frm.arr = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    End If
                Else
                    Dim frm As frmBatchItemIn = New frmBatchItemIn()
                    frm.strItemCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemName).Value)
                    frm.dblqty = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colARQty).Value)
                    frm.strUOM = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value)
                    frm.arr = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingON()
        If clsCommon.myCBool(gvARDetail.CurrentRow.Cells(colARIsBatchItem).Value) Then
            Dim strBatchunion As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                Dim arr As List(Of clsBatchInventoryNew) = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsBatchInventoryNew In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                End If
            Else
                Dim arr As List(Of clsBatchInventory) = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsBatchInventory In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                End If
            End If
            If clsCommon.myLen(strBatchunion) > 0 Then
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gvARDetail.KeyDown
        If e.KeyCode = Keys.F5 Then
            If RunBatchFifowise AndAlso clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal Then
                OpenBatchItemIfFIFIOSettingON()
            Else
                OpenBatchItem()
            End If
        End If
    End Sub

    
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "Standardization_Code", "TSPL_PP_STANDARDIZATION_HEAD", "TSPL_PP_STD_ISSUE_ITEM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
