Imports common
Imports System.Data.SqlClient

Public Class frmProcessProductionStageProcess
    Inherits FrmMainTranScreen

#Region "Variables"
    Public activateSFGProduction As Boolean = False
    Public ShowOnlyProdItemsOnAddRemove As Boolean = False
    Public AutoCalcQtyAddRem As Boolean = False ''BHA/08/10/18-000606 by balwinder on 09/10/2018
    Dim RunBatchFifowise As Boolean = False
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

    Const colNO_SAMPLE_QC As String = "colNO_SAMPLE_QC"
    Const colDAMAGE_Qty As String = "colDAMAGE_Qty"
    Const colFINAL_PROD_Qty As String = "colFINAL_PROD_Qty"
    Const colSP_Loaction_Code As String = "colSP_Loaction_Code"
    Const colSP_Loaction_Desc As String = "colSP_Loaction_Desc"

    Const colIssueSno As String = "colIssueSno"
    Const colIssue_Code As String = "colIssue_Code"
    Const colFrom_Loaction_Code As String = "colFrom_Loaction_Code"
    Const colTo_Location_Code As String = "colTo_Location_Code"
    Const colIssueItemCode As String = "colIssueItemCode"
    Const colIssueItemName As String = "colIssueItemName"
    Const colIssueItemType As String = "colIssueItemType"
    Const colIssueItemProductType As String = "colIssueItemProductType"
    Const colIssueUom As String = "colIssueUom"
    Const colIssueUOMDesc As String = "colIssueUOMDesc"
    Const colAvail_Qty As String = "colAvail_Qty"

    Const colAvail_FAT_Per As String = "colAvail_FAT_Per"
    Const colAvail_FAT_KG As String = "colAvail_FAT_KG"
    Const colAvail_SNF_Per As String = "colAvail_SNF_Per"
    Const colAvail_SNF_KG As String = "colAvail_SNF_KG"
    Const colIssueRemarks As String = "colIssueRemarks"
    Const colIssue_Status As String = "colIssue_Status"
    Const colProducedItem As String = "colProducedItem"
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

    '' fat and snf columns

    Const colAR_FAT_Per As String = "colAR_FAT_Per"
    Const colAR_FAT_KG As String = "colAR_FAT_KG"
    Const colAR_SNF_Per As String = "colAR_SNF_Per"
    Const colAR_SNF_KG As String = "colAR_SNF_KG"

    '' stage detail tab columns

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
    Public objList As List(Of clsPPStageProcessLogSheetDetail) = New List(Of clsPPStageProcessLogSheetDetail)

    Dim arrLoc As String = Nothing
    Private settAllowNegativeStockInDairyProduction As Boolean = False
    Dim UseProductionPlaningDateForWholeProductionCycle As Boolean = False
#End Region

    Private Sub frmProcessProductionStageProcess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        activateSFGProduction = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ActivateSFGProduction, clsFixedParameterCode.ActivateSFGProduction, Nothing), "1") = CompairStringResult.Equal, True, False)
        ShowOnlyProdItemsOnAddRemove = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowOnlyProductionItemInAdRemove, clsFixedParameterCode.ShowOnlyProductionItemInAdRemove, Nothing), "1") = CompairStringResult.Equal, True, False)
        AutoCalcQtyAddRem = If(clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoCalculateADDREMOVEQty, clsFixedParameterCode.AllowAutoCalculateADDREMOVEQty, Nothing), "1") = CompairStringResult.Equal, True, False)
        RunBatchFifowise = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing)) = 1)
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
        settAllowNegativeStockInDairyProduction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, Nothing)) > 0)
        UseProductionPlaningDateForWholeProductionCycle = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseProductionPlaningDateForWholeProductionCycle, clsFixedParameterCode.UseProductionPlaningDateForWholeProductionCycle, Nothing)) = 1, True, False)
        FunReset()
        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for deleting data")
        ButtonToolTip.SetToolTip(btnPost, "Alt+P for posting data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for window close")
        If strDocumentCode IsNot Nothing AndAlso clsCommon.myLen(strDocumentCode) > 0 Then
            LoadData(strDocumentCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
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
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        UsLock1.Status = ERPTransactionStatus.Pending
        txtlocation.Text = ""
        txtlocationname.Text = ""
        fndIssueNo.Value = ""
        lblFromLocation.Text = ""
        fndMainBatchNo.Value = ""
        'lblMainBatchDesc.Text = ""
        fndIssueNo.Value = Nothing
        fndMainBatchNo.Value = Nothing
        TxtManualBatchNo.Text = ""
        lblLineNo.Text = ""
        LblCostCenterCode.Text = ""
        lblCostCenterName.Text = ""
        lblProfitCenterCode.Text = ""
        lblProfitCenterName.Text = ""
        LoadBatchBlankGrid()
        LoadBlankIssueGrid()
        LoadARBlankGrid()
        LoadSPBlankGrid()
        'LoadQCBlankGrid()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        chkJobWorkInward.Checked = False
        fndIssueNo.Enabled = True
        fndIssueNo.Enabled = True
        'fndMainBatchNo.Enabled = True
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
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProcessProductionStageProcess)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        'btnPrint.Enabled = MyBase.isPrintFlag
    End Sub

    Private Sub frmProcessProductionStageProcess_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.G Then
            '    btngo.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isReverse AndAlso btnunpost.Visible Then
            btnunpost.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                      "TSPL_PP_STAGE_PROCESS_HEAD " + Environment.NewLine + _
                                      "TSPL_PP_SP_BATCH_ITEM_PRODUCTION_DETAIL " + Environment.NewLine + _
                                      "TSPL_PP_SP_ISSUE_ITEM_DETAIL " + Environment.NewLine + _
                                      "TSPL_PP_ISSUE_HEAD(Update STAGE_PROCESS_CODE) " + Environment.NewLine + _
                                      "TSPL_PP_STAGE_PROCESS_DETAIL " + Environment.NewLine + _
                                      "TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET " + Environment.NewLine + _
                                      "TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL " + Environment.NewLine + _
                                      "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
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
        ElseIf e.KeyData = (Keys.Control + Keys.I) Then
            RadPageView1.SelectedPage = pageIssueDetail
        ElseIf e.KeyData = (Keys.Control + Keys.A) Then
            RadPageView1.SelectedPage = pageAddRemove
        ElseIf e.KeyData = (Keys.Control + Keys.A + Keys.T) Then
            RadPageView1.SelectedPage = pageAttachment
        ElseIf e.KeyData = (Keys.Control + Keys.B) Then
            RadPageView1.SelectedPage = pageBatchDetail

        ElseIf e.KeyData = (Keys.Control + Keys.S) Then
            RadPageView1.SelectedPage = pageStageDetail
        End If
    End Sub

    Private Sub LoadBatchBlankGrid()
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
        repoQuantity.HeaderText = "Required Quantity"
        repoQuantity.DecimalPlaces = DecimalPointQty
        repoQuantity.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoQuantity.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoQuantity)

        Dim repoSampleNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSampleNo.FormatString = ""
        repoSampleNo.Name = colNO_SAMPLE_QC
        repoSampleNo.Width = 100
        repoSampleNo.HeaderText = "No Of Sample"
        repoSampleNo.DecimalPlaces = 0
        repoSampleNo.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSampleNo)

        Dim repoDamagedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDamagedQty.FormatString = ""
        repoDamagedQty.Name = colDAMAGE_Qty
        repoDamagedQty.Width = 100
        repoDamagedQty.HeaderText = "Damage Quantity"
        repoDamagedQty.DecimalPlaces = DecimalPointQty
        repoDamagedQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoDamagedQty.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoDamagedQty)

        Dim repoFinalProdQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQty.FormatString = ""
        repoFinalProdQty.Name = colFINAL_PROD_Qty
        repoFinalProdQty.Width = 100
        repoFinalProdQty.HeaderText = "Production Qty"
        repoFinalProdQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQty.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoFinalProdQty)

        Dim repoSP_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Code.FormatString = ""
        repoSP_Location_Code.Name = colSP_Loaction_Code
        repoSP_Location_Code.Width = 100
        repoSP_Location_Code.HeaderText = "Location Code"
        repoSP_Location_Code.ReadOnly = False
        repoSP_Location_Code.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSP_Location_Code)

        Dim repoSP_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Desc.FormatString = ""
        repoSP_Location_Desc.Name = colSP_Loaction_Desc
        repoSP_Location_Desc.Width = 150
        repoSP_Location_Desc.HeaderText = "Location Description"
        repoSP_Location_Desc.ReadOnly = True
        repoSP_Location_Desc.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSP_Location_Desc)

        'Dim repoRequirFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoRequirFat.FormatString = ""
        'repoRequirFat.Name = colRequir_FAT_per
        'repoRequirFat.Width = 100
        'repoRequirFat.HeaderText = "Required FAT%"
        'repoRequirFat.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoRequirFat)

        'Dim repoRequirFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoRequirFatKG.FormatString = ""
        'repoRequirFatKG.Name = colRequir_FAT_KG
        'repoRequirFatKG.Width = 150
        'repoRequirFatKG.HeaderText = "Required FAT KG"
        'repoRequirFatKG.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoRequirFatKG)

        'Dim repoRequirSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoRequirSnf.FormatString = ""
        'repoRequirSnf.Name = colRequir_SNF_Per
        'repoRequirSnf.Width = 150
        'repoRequirSnf.HeaderText = "Required SNF%"
        'repoRequirSnf.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoRequirSnf)

        'Dim repoRequirSnfKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoRequirSnfKG.FormatString = ""
        'repoRequirSnfKG.Name = colRequir_SNF_KG
        'repoRequirSnfKG.Width = 150
        'repoRequirSnfKG.HeaderText = "Required SNF KG"
        'repoRequirSnfKG.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoRequirSnfKG)

        'Dim repoProduced_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoProduced_Qty.FormatString = ""
        'repoProduced_Qty.Name = colProduced_Qty
        'repoProduced_Qty.Width = 150
        'repoProduced_Qty.HeaderText = "Produced Quantity"
        'repoProduced_Qty.DecimalPlaces = 3
        'gv.MasterTemplate.Columns.Add(repoProduced_Qty)

        'Dim repoProduced_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoProduced_FAT_KG.FormatString = ""
        'repoProduced_FAT_KG.Name = colProduced_FAT_KG
        'repoProduced_FAT_KG.Width = 150
        'repoProduced_FAT_KG.HeaderText = "Produced FAT KG"
        'repoProduced_FAT_KG.ReadOnly = False
        'gv.MasterTemplate.Columns.Add(repoProduced_FAT_KG)

        'Dim repoProduced_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoProduced_SNF_KG.FormatString = ""
        'repoProduced_SNF_KG.Name = colProduced_SNF_KG
        'repoProduced_SNF_KG.Width = 150
        'repoProduced_SNF_KG.HeaderText = "Produced SNF KG"
        'repoProduced_SNF_KG.ReadOnly = False
        'gv.MasterTemplate.Columns.Add(repoProduced_SNF_KG)
        ''-------------------------------------------------

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        'gv.Rows.AddNew()
        'MyBase.ReStoreGridLayoutMain(Me)
        FindAndRestoreGridLayout(Me)
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

        Dim repoIssueCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueCode.FormatString = ""
        repoIssueCode.Name = colIssue_Code
        repoIssueCode.Width = 100
        repoIssueCode.HeaderText = "Issue Code"
        repoIssueCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoIssueCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoIssueCode.ReadOnly = False
        gvIssue.MasterTemplate.Columns.Add(repoIssueCode)

        Dim repoFromLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocationCode.FormatString = ""
        repoFromLocationCode.Name = colFrom_Loaction_Code
        repoFromLocationCode.Width = 100
        repoFromLocationCode.HeaderText = "From Location Code"
        repoFromLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFromLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLocationCode.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoFromLocationCode)

        Dim repoToLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLocationCode.FormatString = ""
        repoToLocationCode.Name = colTo_Location_Code
        repoToLocationCode.Width = 100
        repoToLocationCode.HeaderText = "To Location Code"
        repoToLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoToLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoToLocationCode.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoToLocationCode)

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
        repoAvail_Qty.Name = colAvail_Qty
        repoAvail_Qty.Width = 120
        repoAvail_Qty.HeaderText = "Available Quantity"
        repoAvail_Qty.DecimalPlaces = DecimalPointQty
        repoAvail_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_Qty.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colAvail_FAT_Per
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Available FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colAvail_FAT_KG
        repoAvail_FAT_KG.Width = 120
        repoAvail_FAT_KG.HeaderText = "Available FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colAvail_SNF_Per
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "Available SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colAvail_SNF_KG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "Available SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_KG)

        'Dim repoDiff_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_Qty.FormatString = ""
        'repoDiff_Qty.Name = colDiff_Qty
        'repoDiff_Qty.Width = 120
        'repoDiff_Qty.HeaderText = "Diff. Quantity"
        'repoDiff_Qty.DecimalPlaces = 3
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_Qty)

        'Dim repoDiff_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_FAT_Per.FormatString = ""
        'repoDiff_FAT_Per.Name = colDiff_FAT_Per
        'repoDiff_FAT_Per.Width = 120
        'repoDiff_FAT_Per.HeaderText = "Diff. FAT%"
        'repoDiff_FAT_Per.ReadOnly = True
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_Per)

        'Dim repoDiff_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_FAT_KG.FormatString = ""
        'repoDiff_FAT_KG.Name = colDiff_FAT_KG
        'repoDiff_FAT_KG.Width = 120
        'repoDiff_FAT_KG.HeaderText = "Diff. FAT KG"
        'repoDiff_FAT_KG.ReadOnly = True
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_KG)

        'Dim repoDiff_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_SNF_Per.FormatString = ""
        'repoDiff_SNF_Per.Name = colDiff_SNF_Per
        'repoDiff_SNF_Per.Width = 120
        'repoDiff_SNF_Per.HeaderText = "Diff. SNF%"
        'repoDiff_SNF_Per.ReadOnly = True
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_Per)

        'Dim repoDiff_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoDiff_SNF_KG.FormatString = ""
        'repoDiff_SNF_KG.Name = colDiff_SNF_KG
        'repoDiff_SNF_KG.Width = 120
        'repoDiff_SNF_KG.HeaderText = "Diff. SNF KG"
        'repoDiff_SNF_KG.ReadOnly = True
        'gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_KG)

        Dim Issue_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Issue_Status.FormatString = ""
        Issue_Status.Name = colIssue_Status
        Issue_Status.Width = 100
        Issue_Status.HeaderText = "Status"
        Issue_Status.ReadOnly = False
        Issue_Status.DataSource = GetIssueStatus()
        Issue_Status.ValueMember = "Code"
        Issue_Status.DisplayMember = "Name"
        gvIssue.MasterTemplate.Columns.Add(Issue_Status)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colIssueRemarks
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        gvIssue.MasterTemplate.Columns.Add(repoIssueRemarks)


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

        '-------------------------------------------------

        gvIssue.AllowDeleteRow = True
        gvIssue.AllowAddNewRow = False
        gvIssue.ShowGroupPanel = False
        gvIssue.AllowColumnReorder = True
        gvIssue.AllowRowReorder = False
        gvIssue.EnableSorting = False
        gvIssue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvIssue.MasterTemplate.ShowRowHeaderColumn = False
        gvIssue.EnableFiltering = False
        gvIssue.Rows.AddNew()
        MyBase.FindAndRestoreGridLayout(Me)
    End Sub

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
        Status.DataSource = GetStageStatus()
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
        gvStage.AllowColumnReorder = True
        gvStage.AllowRowReorder = False
        gvStage.EnableSorting = False
        gvStage.EnableFiltering = False
        gvStage.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvStage.MasterTemplate.ShowRowHeaderColumn = False
        'gvARDetail.Rows.AddNew()
        MyBase.FindAndRestoreGridLayout(Me)
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

    Public Shared Function GetStageStatus() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Not Complete"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Complete"
        dt.Rows.Add(dr)

        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) > 0) Then
            dr = dt.NewRow()
            dr("Code") = "2"
            dr("Name") = "Skip"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(fndMainBatchNo.Value) <= 0 Then
                Errorcontrol.SetError(fndMainBatchNo, "Main Batch Order No is blank.")
                fndMainBatchNo.Select()
                fndMainBatchNo.Focus()
                Throw New Exception("Main Batch Order No is blank.")
            Else
                Errorcontrol.ResetError(fndMainBatchNo)
            End If
            Dim total As Integer = 0
            For Each dr As GridViewRowInfo In gv.Rows
                total = total + 1
            Next
            If total <= 0 Then
                clsCommon.MyMessageBoxShow("Batch grid is empty.")
                Return False
            End If

            total = 0
            '' validation for issue items accept/hold
            For Each growIssue As GridViewRowInfo In gvIssue.Rows
                If clsCommon.myLen(growIssue.Cells(colIssue_Code).Value) > 0 Then
                    If clsCommon.myLen(growIssue.Cells(colIssue_Status).Value) <= 0 Then
                        Throw New Exception("Select Status in Issue Detail tab at line no- " & (growIssue.Index + 1) & ".")
                    End If
                    total = total + 1
                End If
            Next
            If total <= 0 Then
                clsCommon.MyMessageBoxShow("Issue grid is empty.")
                Return False
            End If



            total = 0

            Dim strBatchORderExistIntPIE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stage_Process_Code from TSPL_PP_STAGE_PROCESS_HEAD where TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code='" + fndMainBatchNo.Value + "' and TSPL_PP_STAGE_PROCESS_HEAD.Stage_Process_Code not in ('" + txtCode.Value + "')"))
            If clsCommon.myLen(clsCommon.myCstr(strBatchORderExistIntPIE)) > 0 Then
                Throw New Exception("Please select different Batch Order, Same Batch exists with Production Stage Process " & strBatchORderExistIntPIE & "  ")
            End If
            '' validation for STAGE ROCESS stages existence
            If gvStage.Rows.Count <= 0 Then
                Throw New Exception("Stage Process Stages not found for selected child batch's section and structure.")
            End If
            ''richa BHA/05/09/18-000509
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, Nothing), "0") = CompairStringResult.Equal Then
                For Each dr As GridViewRowInfo In gvStage.Rows
                    If clsCommon.myLen(dr.Cells(colUnit_Code).Value) <= 0 Then
                        Throw New Exception("No any Item found of type Milk or Milk Product in Issue Grid.")
                    End If
                    total = total + 1
                Next
            End If

            ''richa BHA/27/07/18-000200
            For Each dr As GridViewRowInfo In gvARDetail.Rows
                If clsCommon.myLen(dr.Cells(colARItemCode).Value) <= 0 Then
                    Continue For
                End If
                If clsCommon.myLen(dr.Cells(colARLocCode).Value) <= 0 Then
                    Throw New Exception("Enter add/remove location at line no " & (dr.Index + 1) & "")
                End If
                ''BHA/12/12/18-000752 By balwinder on 12/12/2018
                Dim qty As Decimal = clsCommon.myCdbl(dr.Cells(colARQty).Value)
                If qty < 0 Then
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

                If (clsCommon.myCdbl(dr.Cells(colAR_FAT_KG).Value) = 0 AndAlso clsCommon.myCdbl(dr.Cells(colAR_FAT_Per).Value) > 0) OrElse (clsCommon.myCdbl(dr.Cells(colAR_FAT_KG).Value) > 0 AndAlso clsCommon.myCdbl(dr.Cells(colAR_FAT_Per).Value) = 0) Then
                    If Not settAllowNegativeStockInDairyProduction Then
                        Throw New Exception("Add/Remove Item :" + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " provide FATKG / FAT%")
                    End If
                End If
                If RunBatchFifowise Then
                    If clsCommon.CompairString(dr.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                        gvARDetail.CurrentRow = gvARDetail.Rows(dr.Index)
                        OpenBatchItem()
                    End If
                End If
                If qty > 0 AndAlso clsCommon.myCBool(clsCommon.myCdbl(dr.Cells(colARIsBatchItem).Value)) Then
                    If clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                        Dim arrBatchNo As List(Of clsBatchInventoryNew) = TryCast(dr.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventoryNew In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If Math.Abs(tQty - qty) > 0.01 Then
                                Throw New Exception("Item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " Entered Qty " + clsCommon.myCstr(qty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
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
                            If Math.Abs(tQty - qty) > 0.01 Then
                                Throw New Exception("Item : " + clsCommon.myCstr(dr.Cells(colARItemCode).Value) + " Entered Qty " + clsCommon.myCstr(qty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(dr.Cells(colARSno).Value))
                            End If
                        End If
                    End If

                End If
            Next
            ''---------------

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Function SaveData(ByVal isPost As Boolean) As Boolean
        Try
            Dim obj As New clsProcessProductionStageProcess()
            obj.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
            obj.STAGE_PROCESS_DATE = clsCommon.myCDate(dtpDate.Text)
            obj.Issue_Code = "" 'clsCommon.myCstr(fndIssueNo.Value)
            obj.Main_Batch_Code = clsCommon.myCstr(fndMainBatchNo.Value)
            If gvStage.Tag Is Nothing Then
                obj.Section_Stage_Map_Code = ""
            Else
                obj.Section_Stage_Map_Code = gvStage.Tag
            End If
            obj.Is_Job_Work_Inward = chkJobWorkInward.Checked
            obj.Loaction_Code = clsCommon.myCstr(txtlocation.Text)
            obj.Posted = 0
            If clsCommon.CompairString(UsLock1.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then
                obj.Posted = 1
            End If
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            obj.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            obj.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
            obj.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
            obj.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
            ''----------------
            obj.ArrBatchItem = New List(Of clsProcessProductionSPBatchItemDetail)
            obj.ArrIssueItem = New List(Of clsProcessProductionSPIssueItemDetail)
            obj.ArrStage = New List(Of clsProcessProductionSPDetail)
            obj.ArrARItem = New List(Of clsProcessProductionSPARDetail)

            '' assign value to batch item array
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsProcessProductionSPBatchItemDetail()

                objtr.SNO = CInt(grow.Cells(colSno).Value)
                objtr.BOM_Code = clsCommon.myCstr(grow.Cells(colBOM_Code).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colitemtype).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colitemproducttype).Value)
                'objtr.Produced_FAT_KG = clsCommon.myCdbl(grow.Cells(colProduced_FAT_KG).Value)
                'objtr.Produced_Qty = clsCommon.myCdbl(grow.Cells(colProduced_Qty).Value)
                'objtr.Produced_SNF_KG = clsCommon.myCdbl(grow.Cells(colProduced_SNF_KG).Value)
                objtr.Quantity = clsCommon.myCdbl(grow.Cells(colQuantity).Value)
                'objtr.Requir_FAT_KG = clsCommon.myCdbl(grow.Cells(colRequir_FAT_KG).Value)
                'objtr.Requir_FAT_per = clsCommon.myCdbl(grow.Cells(colRequir_FAT_per).Value)
                'objtr.Requir_SNF_KG = clsCommon.myCdbl(grow.Cells(colRequir_SNF_KG).Value)
                'objtr.Requir_SNF_Per = clsCommon.myCdbl(grow.Cells(colRequir_SNF_Per).Value)
                objtr.Section_Code = clsCommon.myCstr(grow.Cells(colSection_Code).Value)
                objtr.Shift_Code = clsCommon.myCstr(grow.Cells(colShift_Code).Value)
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(coluom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colUOMDesc).Value)

                '' save new columns
                objtr.NO_SAMPLE_QC = clsCommon.myCdbl(grow.Cells(colNO_SAMPLE_QC).Value)
                objtr.DAMAGE_Qty = clsCommon.myCdbl(grow.Cells(colDAMAGE_Qty).Value)
                objtr.FINAL_PROD_Qty = clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value)
                objtr.SP_Loaction_Code = clsCommon.myCstr(grow.Cells(colSP_Loaction_Code).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.ArrBatchItem.Add(objtr)
                End If
            Next


            '' assign value to Issue item array
            For Each grow As GridViewRowInfo In gvIssue.Rows
                If clsCommon.myLen(grow.Cells(colIssue_Code).Value) <= 0 Then
                    Continue For
                End If
                Dim objtr As New clsProcessProductionSPIssueItemDetail()

                objtr.SNO = CInt(grow.Cells(colIssueSno).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)

                objtr.Issue_Code = clsCommon.myCstr(grow.Cells(colIssue_Code).Value)
                objtr.From_Loaction_Code = clsCommon.myCstr(grow.Cells(colFrom_Loaction_Code).Value)
                objtr.To_Location_Code = clsCommon.myCstr(grow.Cells(colTo_Location_Code).Value)

                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colIssueItemCode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colIssueItemType).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colIssueItemProductType).Value)
                objtr.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colAvail_FAT_KG).Value)
                objtr.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colAvail_FAT_Per).Value)
                objtr.Avail_Qty = clsCommon.myCdbl(grow.Cells(colAvail_Qty).Value)
                objtr.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colAvail_SNF_KG).Value)
                objtr.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colAvail_SNF_Per).Value)
                'objtr.Diff_FAT_KG = clsCommon.myCdbl(grow.Cells(colDiff_FAT_KG).Value)
                'objtr.Diff_FAT_Per = clsCommon.myCdbl(grow.Cells(colDiff_FAT_Per).Value)
                'objtr.Diff_Qty = clsCommon.myCdbl(grow.Cells(colDiff_Qty).Value)
                'objtr.Diff_SNF_KG = clsCommon.myCdbl(grow.Cells(colDiff_SNF_KG).Value)
                'objtr.Diff_SNF_Per = clsCommon.myCdbl(grow.Cells(colDiff_SNF_Per).Value)

                objtr.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemarks).Value)
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colIssueUom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colIssueUOMDesc).Value)
                objtr.Issue_Status = clsCommon.myCstr(grow.Cells(colIssue_Status).Value)

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
                Dim objtr As New clsProcessProductionSPARDetail()

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
                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colARUom).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colARUOMDesc).Value)
                objtr.Fat_Per = clsCommon.myCdbl(grow.Cells(colAR_FAT_Per).Value)
                objtr.SNF_Per = clsCommon.myCdbl(grow.Cells(colAR_SNF_Per).Value)
                objtr.Fat_Kg = clsCommon.myCdbl(grow.Cells(colAR_FAT_KG).Value)
                objtr.SNF_Kg = clsCommon.myCdbl(grow.Cells(colAR_SNF_KG).Value)
                objtr.arrBatchItem = TryCast(grow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                objtr.arrBatchItemNew = TryCast(grow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.ArrARItem.Add(objtr)
                End If
            Next
            '' assign value to Stage Process  array
            For Each grow As GridViewRowInfo In gvStage.Rows
                Dim objtr As New clsProcessProductionSPDetail()

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

                objtr.STAGE_PROCESS_CODE = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit_Code).Value)
                objtr.Batch_Code = clsCommon.myCstr(grow.Cells(colStageBatch_Code).Value)
                objtr.SPQCList = grow.Tag
                If clsCommon.myLen(objtr.Stage_Code) > 0 Then
                    obj.ArrStage.Add(objtr)
                End If
            Next


            If clsProcessProductionStageProcess.SaveData(isNewEntry, obj) Then
                If isPost = False Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If

                txtCode.Value = obj.STAGE_PROCESS_CODE

                UcAttachment1.SaveData(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                Return False
            End If
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
                Throw New Exception("Select issue detail for deletion")
            End If

            'qry = "select count(*) from TSPL_PP_STAGE_PROCESS_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and STAGE_PROCESS_CODE='" + txtCode.Value + "'"
            'check = clsDBFuncationality.getSingleValue(qry, trans)

            'If check <= 0 Then
            '    txtCode.Select()
            '    txtCode.Focus()
            '    Throw New Exception("Code not found.")
            'End If

            Dim isDeleted As Boolean = False
            If clsProcessProductionStageProcess.DeleteData(txtCode.Value, trans) Then
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

            Dim check As Boolean = False
            check = clsProcessProductionStageProcess.CheckValidCode(Me.txtCode.Value)
            If check = False Then
                txtCode.Select()
                txtCode.Focus()
                Throw New Exception("Code not found.")
            End If
            '' validation for standardization batch location
            'For Each growBatch As GridViewRowInfo In gv.Rows
            '    If clsCommon.myLen(growBatch.Cells(colSP_Loaction_Code).Value) <= 0 Then
            '        Throw New Exception("Select Location Code in Batch Detail tab at line no- " & (growBatch.Index + 1) & ".")
            '    End If
            'Next
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
                For Each obj As clsPPStageProcessLogSheetDetail In grow.Tag
                    If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                        Throw New Exception("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                    End If
                Next
            Next

            If Not clsCommon.MyMessageBoxShow("Are you sure,want to post entry no. " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                Return
            End If

            If AllowToSave() Then
                SaveData(True)
            Else
                Exit Sub
            End If

            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProcessProductionStageProcess.PostData(Me.Form_ID, txtCode.Value, arrLoc) Then
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

    Private Sub txtfrmsub__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndIssueNo._MYValidating
        fndIssueNo.Value = clsProcessProductionIssueEntry.GetFinder("TSPL_PP_ISSUE_HEAD.Batch_Code='" & Me.fndMainBatchNo.Value & "' and TSPL_PP_ISSUE_HEAD.is_post='1' and TSPL_PP_ISSUE_HEAD.from_loaction_code in (" + arrLoc + ")", Me.fndMainBatchNo.Value, isButtonClicked)

        If clsCommon.myLen(fndIssueNo.Value) > 0 Then

            Dim objIssue As clsProcessProductionIssueEntry = clsProcessProductionIssueEntry.GetData(fndIssueNo.Value, arrLoc, NavigatorType.Current, Nothing)
            If Not objIssue Is Nothing Then
                Me.lblFromLocation.Text = objIssue.frm_loc_code
                Me.lblToLocation.Text = objIssue.to_loc_code
            Else
                Me.lblFromLocation.Text = ""
                Me.lblToLocation.Text = ""
            End If
        Else
            fndIssueNo.Value = Nothing
            Me.lblFromLocation.Text = ""
            Me.lblToLocation.Text = ""
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
        Dim check As Boolean = False
        check = clsProcessProductionStageProcess.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsProcessProductionStageProcess.GetFinder(" TSPL_PP_BATCH_ORDER_HEAD.Location_Code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            FunReset()
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsProcessProductionStageProcess()
            isNewEntry = True
            obj = clsProcessProductionStageProcess.GetData(strCode, NavType, arrLoc, Nothing)
            isInsideLoadData = True

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.STAGE_PROCESS_CODE) > 0 Then
                isNewEntry = False

                txtCode.Value = obj.STAGE_PROCESS_CODE
                dtpDate.Text = obj.STAGE_PROCESS_DATE
                lblFromLocation.Text = obj.Issue_From_Loaction_Code
                lblToLocation.Text = obj.Issue_To_Loaction_Code
                Me.txtlocation.Text = obj.Loaction_Code
                Me.txtlocationname.Text = obj.Loaction_Desc
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                TxtManualBatchNo.Text = obj.ManualBatchNo
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                lblLineNo.Text = obj.LINE_NO
                LblCostCenterCode.Text = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                lblProfitCenterCode.Text = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                ''----------------
                If obj.Posted = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnCancel.Enabled = True
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnCancel.Enabled = False
                End If
                'fndIssueNo.Value = obj.Issue_Code
                fndMainBatchNo.Value = obj.Main_Batch_Code

                chkJobWorkInward.Checked = obj.Is_Job_Work_Inward
                gv.Rows.Clear()
                'gv_qc.Rows.Clear()
                gvIssue.Rows.Clear()
                gvARDetail.Rows.Clear()
                gvStage.Rows.Clear()
                gvStage.Tag = obj.Section_Stage_Map_Code
                '' load batch item grid
                If obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSPBatchItemDetail In obj.ArrBatchItem
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = objtr.SNO
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Code).Value = objtr.BOM_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Desc).Value = objtr.BOM_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = objtr.Item_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = objtr.Product_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = objtr.Unit_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = objtr.Unit_Desc
                        'gv.Rows(gv.Rows.Count - 1).Cells(colProduced_Qty).Value = objtr.Produced_Qty
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_KG).Value = objtr.Requir_FAT_KG
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_per).Value = objtr.Requir_FAT_per
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_KG).Value = objtr.Requir_SNF_KG
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_Per).Value = objtr.Requir_SNF_Per
                        gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value = objtr.Quantity

                        'gv.Rows(gv.Rows.Count - 1).Cells(colProduced_FAT_KG).Value = objtr.Produced_FAT_KG
                        'gv.Rows(gv.Rows.Count - 1).Cells(colProduced_Qty).Value = objtr.Produced_Qty
                        'gv.Rows(gv.Rows.Count - 1).Cells(colProduced_SNF_KG).Value = objtr.Produced_SNF_KG

                        gv.Rows(gv.Rows.Count - 1).Cells(colSection_Code).Value = objtr.Section_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colSection_Desc).Value = objtr.Section_Desc

                        gv.Rows(gv.Rows.Count - 1).Cells(colShift_Code).Value = objtr.Shift_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colShift_Desc).Value = objtr.Shift_Desc

                        '' new columns
                        gv.Rows(gv.Rows.Count - 1).Cells(colNO_SAMPLE_QC).Value = objtr.NO_SAMPLE_QC
                        gv.Rows(gv.Rows.Count - 1).Cells(colDAMAGE_Qty).Value = objtr.DAMAGE_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = objtr.FINAL_PROD_Qty

                        gv.Rows(gv.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = objtr.SP_Loaction_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = objtr.SP_Loaction_Desc
                    Next
                End If

                '' load issue item grid
                If obj.ArrIssueItem IsNot Nothing AndAlso obj.ArrIssueItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSPIssueItemDetail In obj.ArrIssueItem
                        gvIssue.Rows.AddNew()
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = objtr.SNO
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).Value = objtr.Issue_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).ReadOnly = True
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colFrom_Loaction_Code).Value = objtr.From_Loaction_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTo_Location_Code).Value = objtr.To_Location_Code

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = objtr.Item_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = objtr.Item_Desc
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = objtr.Item_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(objtr.Product_Type)
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = objtr.Product_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUom).Value = objtr.Unit_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = objtr.Unit_Desc

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_KG).Value = objtr.Avail_FAT_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_Per).Value = objtr.Avail_FAT_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_Qty).Value = objtr.Avail_Qty
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_KG).Value = objtr.Avail_SNF_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_Per).Value = objtr.Avail_SNF_Per

                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_KG).Value = objtr.Diff_FAT_KG

                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_FAT_Per).Value = objtr.Diff_FAT_Per
                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_Qty).Value = objtr.Diff_Qty
                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_KG).Value = objtr.Diff_SNF_KG
                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_SNF_Per).Value = objtr.Diff_SNF_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRemarks).Value = objtr.Remarks
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Status).Value = objtr.Issue_Status

                        '' production costing columns
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = objtr.Fat_Rate
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = objtr.SNF_Rate
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = objtr.Fat_Amt
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = objtr.SNF_Amt
                    Next
                    gvIssue.Rows.AddNew()
                Else
                    gvIssue.Rows.AddNew()
                End If

                '' load Added/Removed item grid
                If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSPARDetail In obj.ArrARItem
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

                        '' fat and snf 
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_Per).Value = objtr.Fat_Per
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_Per).Value = objtr.SNF_Per
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_KG).Value = objtr.Fat_Kg
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_KG).Value = objtr.SNF_Kg

                        'gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                        If clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Then
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = ClsLoadingTanker.getBalance(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Value, Me.txtlocation.Text, gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARUom).Value)
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_Per).ReadOnly = False
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_Per).ReadOnly = False
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItemNew
                        Else
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Value, gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARUom).Value)
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_FAT_Per).ReadOnly = True
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colAR_SNF_Per).ReadOnly = True
                            gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItem
                        End If
                        gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objtr.Product_Type) <= 0, "Others", objtr.Product_Type)
                    Next
                End If
                gvARDetail.Rows.AddNew()
                '' load Stage Process grid
                If obj.ArrStage IsNot Nothing AndAlso obj.ArrStage.Count > 0 Then
                    For Each objtr As clsProcessProductionSPDetail In obj.ArrStage
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

                fndIssueNo.Enabled = False
                'fndMainBatchNo.Enabled = (btnunpost.Visible AndAlso obj.Posted = 0)


                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                txtCode.MyReadOnly = True
                UcAttachment1.LoadData(txtCode.Value)

                If obj.Posted = 1 Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If e.Column Is gv.Columns(colQuantity) Then
                isCellValueChanged = True

                Dim currRec As Decimal = gv.CurrentRow.Cells(colQuantity).Value
                If currRec > GetIssueQty() Then
                    clsCommon.MyMessageBoxShow("Quantity must be less than or equal to Issued Quantity.")
                    gv.CurrentRow.Cells(colQuantity).Value = 0
                End If
                'gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colQuantity).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
                isCellValueChanged = False
            End If
            'If (e.Column Is gv.Columns(colDAMAGE_Qty)) Then
            '    isCellValueChanged = True
            '    gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colQuantity).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
            '    isCellValueChanged = False
            'End If
            If e.Column Is gv.Columns(colSP_Loaction_Code) Then
                isCellValueChanged = True
                gv.CurrentRow.Cells(colSP_Loaction_Code).Value = clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "')", gv.CurrentRow.Cells(colSP_Loaction_Code).Value, False)
                gv.CurrentRow.Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(gv.CurrentRow.Cells(colSP_Loaction_Code).Value, Nothing)
                'If EnableParameterTab() Then
                '    pageParameterDetail.Enabled = True
                'End If
                isCellValueChanged = False
            End If
        End If
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If clsCommon.myLen(fndMainBatchNo.Value) <= 0 Then
                fndMainBatchNo.Focus()
                Throw New Exception("Select Main Batch order first.")
            End If
            If clsCommon.myLen(fndIssueNo.Value) <= 0 Then
                fndIssueNo.Focus()
                Throw New Exception("Select Issue No first.")
            End If
            FillBatchOrder()
            FillIssueAgainstBatchOrder()
            FillStageDetail()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub InitialLoadAllGrid()
        Try
            If clsCommon.myLen(fndMainBatchNo.Value) <= 0 Then
                fndMainBatchNo.Focus()
                Throw New Exception("Select Main Batch order first.")
            End If
            'If clsCommon.myLen(fndIssueNo.Value) <= 0 Then
            '    fndIssueNo.Focus()
            '    Throw New Exception("Select Issue No first.")
            'End If
            FillBatchOrder()
            'FillIssueAgainstBatchOrder()
            FillStageDetail()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillStageDetail()
        Me.gvStage.Rows.Clear()

        Dim TotalRec As Double = 0
        Dim Unit_Code As String = String.Empty
        Dim Unit_Desc As String = String.Empty
        For Each dr As GridViewRowInfo In gvIssue.Rows
            If clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                TotalRec = TotalRec + dr.Cells(colAvail_Qty).Value
                Unit_Code = dr.Cells(colIssueUom).Value
                Unit_Desc = dr.Cells(colIssueUOMDesc).Value
            End If
        Next

        If clsCommon.myLen(Unit_Code) <= 0 Then
            For Each dr As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(dr.Cells(coluom).Value) > 0 Then
                    Unit_Code = clsCommon.myCstr(dr.Cells(coluom).Value)
                    Unit_Desc = clsCommon.myCstr(dr.Cells(colUOMDesc).Value)
                End If
            Next
        End If

        Dim obj As ClsSectionStageMapping
        obj = clsProcessProductionStageProcess.FillStageDetail(Me.fndMainBatchNo.Value)
        If obj Is Nothing Then
            Exit Sub
        End If
        isInsideLoadData = True
        gvStage.Tag = obj.doc_code
        For Each objStage As clsSectionStageMappingDetail In obj.Arr
            If clsCommon.CompairString(objStage.Stage_Type, "SP") = CompairStringResult.Equal Then
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
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStageBatch_Code).Value = fndMainBatchNo.Value
            End If
        Next
        isInsideLoadData = False
    End Sub

    Sub FillBatchOrder()
        Me.gv.Rows.Clear()
        Dim objBO As clsProcessBatchOrder = clsProcessBatchOrder.GetData(fndMainBatchNo.Value, arrLoc, NavigatorType.Current)
        isInsideLoadData = True
        If Not objBO Is Nothing Then
            For Each dr As clsProcessBatchOrderMainDetail In objBO.ArrMainItem
                Me.gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = dr.SNO
                gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Code).Value = dr.bomcode
                gv.Rows(gv.Rows.Count - 1).Cells(colBOM_Desc).Value = dr.BomDesc
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
                gv.Rows(gv.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = gv.Rows(gv.Rows.Count - 1).Cells(colQuantity).Value - gv.Rows(gv.Rows.Count - 1).Cells(colDAMAGE_Qty).Value
                'Dim dtQC As DataTable = clsProcessProductionStageProcess.GetItemQCParameter(dr.icode)
                'Dim drQC() As DataRow = dtQC.Select("type='FAT'")
                'If drQC.Length > 0 Then
                '    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_per).Value = clsCommon.myCdbl(drQC(0).Item("lower_range"))
                '    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_FAT_KG).Value = clsCommon.myCdbl(drQC(0).Item("lower_range")) * dr.qty / 100
                'End If

                'drQC = dtQC.Select("type='SNF'")
                'If drQC.Length > 0 Then
                '    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_Per).Value = clsCommon.myCdbl(drQC(0).Item("lower_range"))
                '    gv.Rows(gv.Rows.Count - 1).Cells(colRequir_SNF_KG).Value = clsCommon.myCdbl(drQC(0).Item("lower_range")) * dr.qty / 100
                'End If
            Next
        End If
        isInsideLoadData = False
    End Sub

    Sub FillIssueAgainstBatchOrder()
        Me.gvIssue.Rows.Clear()
        Dim dt As DataTable = clsProcessProductionStageProcess.GetIssueAgainstBatch(Me.fndMainBatchNo.Value, Me.txtCode.Value)
        Dim totalIssued As Decimal = 0
        isInsideLoadData = True
        For Each dr As DataRow In dt.Rows
            Me.gvIssue.Rows.AddNew()
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = gvIssue.Rows.Count
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = dr.Item("Item_Code")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = dr.Item("Item_Desc")

            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = ItemType(dr.Item("Item_Type"))
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(dr.Item("Product_Type"))
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = dr.Item("Product_Type")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUom).Value = dr.Item("Unit_Code")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = dr.Item("Unit_Desc")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_Qty).Value = dr.Item("Issue_Qty")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_KG).Value = dr.Item("Avail_FAT_KG")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_Per).Value = dr.Item("Avail_FAT_Pers")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_KG).Value = dr.Item("Avail_SNF_KG")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_Per).Value = dr.Item("Avail_SNF_Pers")

            '' producton costing columns
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = dr.Item("Issued_FAT_Rate")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = dr.Item("Issued_SNF_Rate")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = dr.Item("Issued_FAT_Amt")
            gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = dr.Item("Issued_SNF_Amt")
            totalIssued = totalIssued + gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_Qty).Value
        Next
        isInsideLoadData = False
        'For intloop As Integer = 0 To gv.Rows.Count - 1
        '    gv.Rows(intloop).Cells(colProduced_Qty).Value = totalIssued
        'Next
    End Sub

    Private Sub gvARDetail_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvStage.CurrentColumnChanged
        'If gvARDetail.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvARDetail.CurrentRow.Index
        '    gvARDetail.CurrentRow.Cells(colStageSno).Value = clsCommon.myCdbl(intCurrRow) + 1
        '    If intCurrRow = gvARDetail.Rows.Count - 1 Then
        '        gvARDetail.Rows.AddNew()
        '        gvARDetail.CurrentRow = gvARDetail.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub gvARDetail_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvStage.CellValueChanged
        If Not isInsideLoadData Then

            If Not isCellValueChanged Then
                If e.Column Is gvStage.Columns(colStatus) Then
                    isCellValueChanged = True

                    If clsCommon.CompairString(gvStage.Rows(e.RowIndex).Cells(colStatus).Value, "2") = CompairStringResult.Equal Then
                        isCellValueChanged = False
                        'If EnableBatchItemTab() Then
                        '    pageBatchDetail.Enabled = True
                        'End If
                        Exit Sub
                    End If
                    '' for skip
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
                                    For Each obj As clsPPStageProcessLogSheetDetail In grow.Tag
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
                                    For Each obj As clsPPStageProcessLogSheetDetail In grow.Tag
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
                End If


                If e.Column Is gvStage.Columns(colReceived_Qty) Then
                    isCellValueChanged = True

                    Dim currRec As Decimal = gvStage.CurrentRow.Cells(colReceived_Qty).Value
                    If currRec > GetIssueQty() Then
                        clsCommon.MyMessageBoxShow("Received Quantity must be less than or equal to Issued Quantity.")
                        gvStage.CurrentRow.Cells(colReceived_Qty).Value = 0
                    End If
                    isCellValueChanged = False
                End If
            End If
        End If
        isCellValueChanged = False
    End Sub

    Function GetIssueQty() As Decimal
        Dim totQty As Decimal = 0
        For Each grow As GridViewRowInfo In gvIssue.Rows
            totQty = totQty + grow.Cells(colAvail_Qty).Value
        Next
        Return totQty
    End Function

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
    End Sub

    Private Sub gvIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvIssue.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If (e.Column Is gvIssue.Columns(colIssue_Code)) Then
                    isCellValueChanged = True
                    Dim Unit_Code As String = Nothing
                    Dim Unit_Desc As String = Nothing
                    Dim TotalRec As Decimal = 0

                    gvIssue.Rows(e.RowIndex).Cells(colIssue_Code).Value = clsProcessProductionIssueEntry.GetFinder("TSPL_PP_ISSUE_HEAD.Batch_Code='" & Me.fndMainBatchNo.Value & "' and (STAGE_PROCESS_CODE is null or STAGE_PROCESS_CODE in ('" + txtCode.Value + "') ) and TSPL_PP_ISSUE_HEAD.is_post='1' and TSPL_PP_ISSUE_HEAD.from_loaction_code in (" + arrLoc + ")", gvIssue.Rows(e.RowIndex).Cells(colIssue_Code).Value, False)
                    If clsCommon.myLen(gvIssue.Rows(e.RowIndex).Cells(colIssue_Code).Value) > 0 Then
                        Dim objIssue As clsProcessProductionIssueEntry = clsProcessProductionIssueEntry.GetData(gvIssue.Rows(e.RowIndex).Cells(colIssue_Code).Value, arrLoc, NavigatorType.Current, Nothing)
                        If Not objIssue Is Nothing Then
                            For Each objTr As clsProcessProductionIssueItemDetail In objIssue.ArrItem
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = gvIssue.Rows.Count
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).Value = objIssue.issuecode
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).ReadOnly = True
                                'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colFrom_Loaction_Code).Value = objIssue.frm_loc_code
                                'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTo_Location_Code).Value = objIssue.to_loc_code

                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colFrom_Loaction_Code).Value = objTr.frm_loc_code
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTo_Location_Code).Value = objTr.to_loc_code

                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = objTr.itemcode
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = objTr.itemname

                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = objTr.item_type
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = objTr.product_type
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = objTr.product_type
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUom).Value = objTr.uom_code
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = objTr.uom_desc
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_Qty).Value = objTr.issue_qty
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_KG).Value = objTr.fat_kg
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_FAT_Per).Value = objTr.fat_pers
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_KG).Value = objTr.snf_kg
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colAvail_SNF_Per).Value = objTr.snf_pers

                                '' producton costing columns
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = objTr.Fat_Rate
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = objTr.SNF_Rate
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = objTr.Fat_Amt
                                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = objTr.SNF_Amt

                                gvIssue.Rows.AddNew()
                            Next
                            updateStageTab()
                        End If
                    End If
                End If
                isCellValueChanged = False
            End If
        End If
    End Sub

    Sub updateStageTab()
        Dim Unit_Code As String = Nothing

        Dim Unit_Desc As String = Nothing
        Dim TotalRec As Decimal = 0
        For Each grow As GridViewRowInfo In gvIssue.Rows
            If clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                Unit_Code = grow.Cells(colIssueUom).Value
                Unit_Desc = grow.Cells(colIssueUOMDesc).Value
                TotalRec = TotalRec + grow.Cells(colAvail_Qty).Value
            End If
        Next
        If clsCommon.myLen(Unit_Code) <= 0 Then
            For Each dr As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(dr.Cells(coluom).Value) > 0 Then
                    Unit_Code = clsCommon.myCstr(dr.Cells(coluom).Value)
                    Unit_Desc = clsCommon.myCstr(dr.Cells(colUOMDesc).Value)
                End If
            Next
        End If
        If clsCommon.myLen(Unit_Code) > 0 Then
            For Each grow As GridViewRowInfo In gvStage.Rows
                grow.Cells(colUnit_Code).Value = Unit_Code
                grow.Cells(colSPUnit_Desc).Value = Unit_Desc
                grow.Cells(colReceived_Qty).Value = TotalRec
            Next
        End If

    End Sub

    Private Sub fndMainBatchNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMainBatchNo._MYValidating
        ''BHA/17/04/19-000864 by balwinder on 22/04/2019 Finder take 1 min to open.
        fndMainBatchNo.Value = clsProcessBatchOrder.GetFinder_PendingBatchQuantity(" isnull(Main.Quantity,0) > isnull( prod.Produced_Qty,0) and (len(main_Batch_Code)=0  or TSPL_PP_BATCH_ORDER_HEAD.batch_code like 'C-BO%') and TSPL_PP_BATCH_ORDER_HEAD.Is_post='1' and TSPL_PP_BATCH_ORDER_HEAD.location_code in (" + arrLoc + ") " & If(activateSFGProduction = True, " and isnull(TSPL_ITEM_MASTER.Product_Type,'')<>'MI'", "") & "", Me.fndMainBatchNo.Value, isButtonClicked, txtCode.Value)
        LoadBlankIssueGrid()
        If clsCommon.myLen(fndMainBatchNo.Value) > 0 Then
            If UseProductionPlaningDateForWholeProductionCycle = True Then
                dtpDate.Value = Nothing
                dtpDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Batch_Date from TSPL_PP_BATCH_ORDER_HEAD where batch_Code='" + fndMainBatchNo.Value + "'"))
            End If
            Dim objMainBatch As clsProcessBatchOrder = clsProcessBatchOrder.GetData(fndMainBatchNo.Value, arrLoc, NavigatorType.Current)
            If Not objMainBatch Is Nothing Then
                Me.fndMainBatchNo.Value = objMainBatch.Batchcode
                Me.txtlocation.Text = objMainBatch.locationcode
                Me.txtlocationname.Text = objMainBatch.locationname
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                TxtManualBatchNo.Text = objMainBatch.ManualBatchNo
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                lblLineNo.Text = objMainBatch.LINE_NO
                LblCostCenterCode.Text = objMainBatch.CostCenterCode
                lblCostCenterName.Text = objMainBatch.CostCenterName
                lblProfitCenterCode.Text = objMainBatch.ProfitCenterCode
                lblProfitCenterName.Text = objMainBatch.ProfitCenterName
                InitialLoadAllGrid()
            Else
                Me.fndMainBatchNo.Value = Nothing
                Me.fndMainBatchNo.Value = Nothing
                Me.txtlocation.Text = ""
                Me.txtlocationname.Text = ""
                TxtManualBatchNo.Text = ""
                lblLineNo.Text = ""
                LblCostCenterCode.Text = ""
                lblCostCenterName.Text = ""
                lblProfitCenterCode.Text = ""
                lblProfitCenterName.Text = ""
            End If
        Else
            Me.fndMainBatchNo.Value = Nothing
            Me.txtlocation.Text = ""
            Me.txtlocationname.Text = ""
            TxtManualBatchNo.Text = ""
        End If
        chkJobWorkInward.Checked = clsProcessBatchOrder.IsJobWorkBatchOrder(fndMainBatchNo.Value, Nothing)
    End Sub

    Private Sub gvARDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvStage.KeyDown
        If Not gvStage.CurrentRow Is Nothing AndAlso e.KeyCode = (Keys.F4) Then
            Dim frm As New frmPPStageProcessQCLogSheet
            frm.Stage_Code = gvStage.CurrentRow.Cells(colStage_Code).Value
            frm.Stage_Desc = gvStage.CurrentRow.Cells(colStage_Desc).Value
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
            frm.objListSP = gvStage.CurrentRow.Tag
            frm.Stage_Type = gvStage.CurrentRow.Cells(colStage_Code).Tag
            frm.Batch_Code = fndMainBatchNo.Value
            frm.arrXtime = gvStage.CurrentRow.Cells(0).Tag
            If clsCommon.myLen(gvStage.Tag) <= 0 Then
                clsCommon.MyMessageBoxShow("Permission denied.")
                Exit Sub
            End If
            frm.objListUsers = clsSectionStageMapping_User.GetLogsheetUsers(gvStage.Tag, frm.Stage_Code, Nothing)
            frm.ShowDialog()
            If frm.IsCancelled = False Then
                gvStage.CurrentRow.Tag = frm.objListSP
                gvStage.CurrentRow.Cells(0).Tag = frm.arrXtime
            End If

        End If
    End Sub

    Private Sub gvStage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvStage.DoubleClick
        'Try
        '    If gvStage.CurrentRow Is Nothing Then
        '        Exit Sub
        '    End If
        '    If gvStage.CurrentCell Is Nothing Then
        '        Exit Sub
        '    End If
        '    If (gvStage.CurrentCell.ColumnInfo.Name Is gvStage.Columns(colLog_Sheet_No)) Then
        '        Dim frm As New frmPPStageProcessQCLogSheet
        '        frm.Stage_Code = gvStage.CurrentRow.Cells(colStage_Code).Value
        '        frm.txtstagecode.Value = gvStage.CurrentRow.Cells(colStage_Code).Value

        '        frm.txtstagename.Text = gvStage.CurrentRow.Cells(colStage_Desc).Value
        '        frm.STAGE_PROCESS_CODE = Me.txtCode.Value
        '        frm.txtcategorycode.Value = gvStage.CurrentRow.Cells(colSPProdCategory).Value
        '        frm.ProductionCategoryCode = gvStage.CurrentRow.Cells(colSPProdCategory).Value

        '        frm.txtCode.Value = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
        '        frm.Log_Sheet_No = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
        '        frm.Sequence = gvStage.CurrentRow.Cells(colStageSno).Value
        '        frm.txtsequnce.Text = gvStage.CurrentRow.Cells(colStageSno).Value
        '        frm.objListSP = gvStage.CurrentRow.Tag
        '        frm.ShowDialog()
        '        If frm.IsCancelled = False Then
        '            gvStage.CurrentRow.Tag = frm.objListSP
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub

    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsProcessProductionStageProcess.UnpostData(txtCode.Value, Me.Form_ID) Then
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
        repoARFatKG.Minimum = 0
        repoARFatKG.DecimalPlaces = DecimalPointQty
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
        gvARDetail.AllowColumnReorder = True
        gvARDetail.AllowRowReorder = False
        gvARDetail.EnableSorting = False
        gvARDetail.EnableFiltering = False
        gvARDetail.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvARDetail.MasterTemplate.ShowRowHeaderColumn = False
        gvARDetail.Rows.AddNew()
        MyBase.FindAndRestoreGridLayout(Me)
    End Sub

    Private Sub gvARDetail_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvARDetail.CellFormatting
        Try
            If e.Column Is gvARDetail.Columns(colARQty) Then
                If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value) = 0 AndAlso clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value) = 0 Then
                    gvARDetail.Columns(colARQty).ReadOnly = False
                Else
                    gvARDetail.Columns(colARQty).ReadOnly = AutoCalcQtyAddRem
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvARDetail_CellValueChanged1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvARDetail.CellValueChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(fndMainBatchNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Batch Order Detail", Me.Text)
                fndMainBatchNo.Select()
                fndMainBatchNo.Focus()
                Return
            End If
            If Not isCellValueChanged Then
                If e.Column Is gvARDetail.Columns(colARItemCode) Then
                    isCellValueChanged = True
                    gvARDetail.CurrentRow.Cells(colARItemCode).Value = clsItemMaster.getFinder(If(ShowOnlyProdItemsOnAddRemove = True, " Item_Used_as='P' ", ""), gvARDetail.CurrentRow.Cells(colARItemCode).Value, False)
                    Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, NavigatorType.Current)
                    If Not objItem Is Nothing Then
                        gvARDetail.CurrentRow.Cells(colARItemName).Value = objItem.Item_Desc
                        gvARDetail.CurrentRow.Cells(colARItemType).Value = objItem.Item_Type
                        gvARDetail.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                        gvARDetail.CurrentRow.Cells(colARIsBatchItem).Value = objItem.Is_Batch_Item
                        gvARDetail.CurrentRow.Cells(colARUom).Value = objItem.Unit_Code
                        gvARDetail.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
                        If clsCommon.CompairString(objItem.Product_Type, "MI") = CompairStringResult.Equal Then
                            gvARDetail.CurrentRow.Cells(colAR_FAT_Per).ReadOnly = False
                            gvARDetail.CurrentRow.Cells(colAR_SNF_Per).ReadOnly = False
                        Else
                            gvARDetail.CurrentRow.Cells(colAR_FAT_Per).ReadOnly = True
                            gvARDetail.CurrentRow.Cells(colAR_SNF_Per).ReadOnly = True
                        End If
                        gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value = objItem.STD_FatPer
                        gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value = objItem.STD_SNFPer
                    End If
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARUom) Then
                    isCellValueChanged = True
                    OpenUOM(False)
                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARLocCode) Then
                    Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
                    Dim strItemLoc As String = ""
                    If ShowLocationItemLocationwise = 1 AndAlso clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                        strItemLoc = " and Location_Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value) & "')"
                    End If
                    isCellValueChanged = True
                    gvARDetail.CurrentRow.Cells(colARLocCode).Value = clsLocation.getFinder("(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "') or Location_Code='" & txtlocation.Text & "')" & strItemLoc, gvARDetail.CurrentRow.Cells(colARLocCode).Value, False)
                    gvARDetail.CurrentRow.Cells(colARLocDesc).Value = clsLocation.GetName(gvARDetail.CurrentRow.Cells(colARLocCode).Value, Nothing)
                    If clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Then
                        gvARDetail.CurrentRow.Cells(colARAvailQty).Value = ClsLoadingTanker.getBalance(gvARDetail.CurrentRow.Cells(colARItemCode).Value, Me.txtlocation.Text, gvARDetail.CurrentRow.Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                    Else
                        gvARDetail.CurrentRow.Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                    End If

                    isCellValueChanged = False
                End If
                If e.Column Is gvARDetail.Columns(colARQty) AndAlso AutoCalcQtyAddRem = False Then
                    isCellValueChanged = True
                    gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    OpenBatchItem()
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_FAT_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value) > 0 Then
                        gvARDetail.CurrentRow.Cells(colARQty).Value = Math.Round(clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value * 100 / gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value), 2)
                    End If
                    gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    isCellValueChanged = False
                ElseIf e.Column Is gvARDetail.Columns(colAR_SNF_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value) <> 0 Then
                        gvARDetail.CurrentRow.Cells(colARQty).Value = Math.Round(clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value * 100 / gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value), 2)
                    End If
                    gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub gvARDetail_CurrentColumnChanged1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvARDetail.CurrentColumnChanged
        If gvARDetail.RowCount > 0 Then
            Dim intCurrRow As Integer = gvARDetail.CurrentRow.Index
            gvARDetail.CurrentRow.Cells(colARSno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvARDetail.Rows.Count - 1 Then
                gvARDetail.Rows.AddNew()
                gvARDetail.CurrentRow = gvARDetail.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            clsProcessProductionStageProcess.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

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

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gvARDetail.KeyDown
        If e.KeyCode = Keys.F5 Then
            If RunBatchFifowise AndAlso clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal Then
                OpenBatchItemIfFIFIOSettingON()
            Else
                OpenBatchItem()
            End If
        End If
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingON()
        If clsCommon.myCBool(gvARDetail.CurrentRow.Cells(colARIsBatchItem).Value) Then
            Dim strBatchunion As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colitemproducttype).Value), "MI") = CompairStringResult.Equal Then
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

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "STAGE_PROCESS_CODE", "TSPL_PP_STAGE_PROCESS_HEAD", "TSPL_PP_STAGE_PROCESS_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
