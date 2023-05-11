''03/09/2014---Created by --[Panch Raj]-- Ticket no : BM00000003645 ,[BHA/03/08/18-000388]
'============= Ticket No: BM00000010016 by Parteek'
Imports common
Imports Microsoft.Office.Interop

Public Class frmProductionEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False

    Dim DecimalPointQty As Integer = 3
    Dim DecimalPointFatSNFPer As Integer = 3

    '' issue tab columns
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
    'Const colIssue_Status As String = "colIssue_Status"
    Const colIssue_Fat_Rate As String = "colIssue_Fat_Rate"
    Const colIssue_SNF_Rate As String = "colIssue_SNF_Rate"
    Const colIssue_Fat_Amt As String = "colIssue_Fat_Amt"
    Const colIssue_SNF_Amt As String = "colIssue_SNF_Amt"
    '' end issue tab columns

    '' stage tab
    Const colARSno As String = "colARSno"
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
    Public objListSP As List(Of clsPPPELogSheetDetail) = New List(Of clsPPPELogSheetDetail)
    '' end stage tab

    Const colLineNo As String = "LineNO"
    'Const colPPCode As String = "PPCode"
    'Const colProdLineCode As String = "ProdLineCode"
    Const colBOMCode As String = "BOMCode"
    Const colBOMDesc As String = "colBOMDesc"

    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    Const colUOM As String = "UOM"
    Const colBatchQty As String = "BatchQTY"

    Const colPrevProdQty As String = "colPrevProdQty"
    Const colPendingBatchQty As String = "colPendingBatchQty"

    Const colReceiptQty As String = "ReceiptQty"
    Const colRejHead As String = "RejHead"
    Const colRejQty As String = "RejQty"
    Const colBreakageHead As String = "BreakageHead"
    Const colBreakageQty As String = "BreakageQty"
    Const colLabTesting As String = "LabTesting"
    Const colFINAL_PROD_Qty As String = "colFINAL_PROD_Qty"
    Const colSP_Loaction_Code As String = "colSP_Loaction_Code"
    Const colSP_Loaction_Desc As String = "colSP_Loaction_Desc"
    'Const colStartTime As String = "StartTime"
    'Const colEndTime As String = "EndTime"
    'Const colMfgDate As String = "MfgDate"
    'Const colExpDate As String = "ExpDate"
    Const colFIFO_Cost As String = "colFIFO_Cost"
    Const colLIFO_Cost As String = "colLIFO_Cost"
    Const colAVG_Cost As String = "colAVG_Cost"
    Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    Const colIsSerialseItem As String = "colIsSerialseItem"

    Const colCost_Method As String = "colCost_Method"

    Const colShiftCode As String = "colShiftCode"
    Const colSectionCode As String = "colSectionCode"

    Const colFAT_Per As String = "colFAT_Per"
    Const colSNF_Per As String = "colSNF_Per"
    Const colFAT_KG As String = "colFAT_KG"
    Const colSNF_KG As String = "colSNF_KG"

    '' qc tab 
    '' QC tab columns
    Const colQCSno As String = "colQCSno"
    Const colQCType As String = "colQCType"
    Const colQCItemCode As String = "colQCItemCode"
    Const colQCItemName As String = "colQCItemName"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCBooleanStatus As String = "colQCBooleanStatus"
    Const colQCAlphaValue As String = "colQCAlphaValue"
    Const colActual_Range As String = "colActual_Range"
    Const colActual_Status As String = "colActual_Status"
    Const colActual_Value As String = "colActual_Value"
    Const colQc_Status As String = "colQc_Status"
    Const colQCremarks As String = "colQCremarks"
    '' qc tab end

    '' wreckage and flashing
    Const colWFSNO As String = "colWFSNO"
    Const colWFItem_Code As String = "colWFItem_Code"
    Const colWFItemName As String = "colWFItemName"
    Const colWFUnit_Code As String = "colWFUnit_Code"
    Const colWFUnit_Desc As String = "colWFUnit_Desc"
    Const colWFBACK_QTY As String = "colWFBACK_QTY"
    Const colWFWRECKAGE_QTY As String = "colWFWRECKAGE_QTY"
    Const colWFLocation_Code As String = "colWFLocation_Code"
    Const colWFAvail_FAT_Per As String = "colWFAvail_FAT_Per"
    Const colWFAvail_SNF_Per As String = "colWFAvail_SNF_Per"
    Const colWFAvail_FAT_KG As String = "colWFAvail_FAT_KG"
    Const colWFAvail_SNF_KG As String = "colWFAvail_SNF_KG"
    Const colWFRemarks As String = "colWFRemarks"

    '' end wreckage and flashing

    '' Scrap
    Const colScrapSNO As String = "colScrapSNO"
    Const colScrapItem_Code As String = "colScrapItem_Code"
    Const colScrapItemName As String = "colScrapItemName"
    Const colScrapUnit_Code As String = "colScrapUnit_Code"
    Const colScrapUnit_Desc As String = "colScrapUnit_Desc"
    Const colScrapLocation_Code As String = "colScrapLocation_Code"
    Const colScrapAvail_FAT_Per As String = "colScrapAvail_FAT_Per"
    Const colScrapAvail_FAT_KG As String = "colScrapAvail_FAT_KG"
    Const colScrapAvail_SNF_Per As String = "colScrapAvail_SNF_Per"
    Const colScrapAvail_SNF_KG As String = "colScrapAvail_SNF_KG"
    Const colScrapRemarks As String = "colScrapRemarks"
    Const colScrapQty As String = "colScrapQty"
    '' End Scrap

    '' cost grid
    Const colMainItemCode As String = "colMainItemCode"
    Const colMainItemDesc As String = "colMainItemDesc"
    Const colMainUOM As String = "colMainUOM"
    Const colMainUOMDesc As String = "colMainUOMDesc"
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public strDocumentNo As String = ""
    Dim obj As New clsProductionEntry
    Public Const strCostTransaction As String = "Production Entry"
    Public MOActive As Boolean = False

    Dim arrLoc As String = Nothing
    Dim isCellValueChanged As Boolean = False
    Dim ShowoverheadCost As String = "0"
    Private settAllowNegativeStockInDairyProduction As Boolean = False

    Dim MI_Consm_Type As Integer = 0 ''0: Issue Basis 1. BOM Basis
    Dim MP_Consm_Type As Integer = 0
    Dim Othr_Consm_Type As Integer = 0
    Dim UseProductionPlaningDateForWholeProductionCycle As Boolean = False
#End Region

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'txtlocationcode.Value = obj.Default_LocCode
                'txtlocationname.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_PP_PRODUCTION_ENTRY_DETAIL alter column Avg_Cost decimal(18,2) null")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_PP_PRODUCTION_ENTRY_DETAIL alter column FIFO_Cost decimal(18,2) null")
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_PP_PRODUCTION_ENTRY_DETAIL alter column LIFO_Cost decimal(18,2) null")
        SetUserMgmtNew()
        '' get decimal point for qty
        ShowoverheadCost = clsFixedParameter.GetData(clsFixedParameterType.ShowOverheadCostOnProductionEntry, clsFixedParameterCode.ShowOverheadCostOnProductionEntry, Nothing)
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
        MI_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkStandardization, Nothing))
        MP_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkProduct, Nothing))
        Othr_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeOther, Nothing))


        gvSectionStock.AutoGenerateColumns = True
        gvSectionStockHistory.AutoGenerateColumns = True
        gvSectionStock.ReadOnly = True
        gvSectionStockHistory.ReadOnly = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = pageIssueDetail

        LoadBlankGrid()
        LoadBlankIssueGrid()
        LoadSPBlankGrid()
        LoadQCBlankGrid()
        LoadBlankWreckageGrid()
        LoadBlankScrapGrid()
        gvSectionStock.AutoGenerateColumns = True
        funReset()
        SetLength()
        If ShowoverheadCost = "1" Then
            pageoverheadcost.Item.Visibility = ElementVisibility.Visible
            LoadBlankGridCost()
        Else
            pageoverheadcost.Item.Visibility = ElementVisibility.Collapsed
        End If

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        UcAttachment1.Form_ID = MyBase.Form_ID
        txtReceivedBy.Visible = False
        lblReceivedBy.Visible = False
        lblEmpName.Visible = False
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        dtpDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        dtpBatchDate.Value = Nothing
        dtpBatchDate.Enabled = False
        txtReceivedBy.Value = ""
        lblEmpName.Text = ""
        gvIssue.Rows.Clear()
        gvStage.Rows.Clear()
        gvBatch.Rows.Clear()
        gvParameter.Rows.Clear()
        gvWreckage.Rows.Clear()
        gvWreckage.Rows.Clear()
        GvScrap.Rows.Clear()
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()
    End Sub

    Sub LoadBlankGrid()
        gvBatch.Rows.Clear()
        gvBatch.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn

        Dim PPCode As New GridViewTextBoxColumn
        Dim ProdLineCode As New GridViewTextBoxColumn
        Dim BOMCode As New GridViewTextBoxColumn

        Dim ItemCode As New GridViewTextBoxColumn
        Dim ItemDesc As New GridViewTextBoxColumn
        Dim UOM As New GridViewTextBoxColumn
        Dim BatchQty As New GridViewDecimalColumn
        Dim PrevProdQty As New GridViewDecimalColumn
        Dim PendBatchQty As New GridViewDecimalColumn
        Dim ReceiptQty As New GridViewDecimalColumn
        Dim RejHead As New GridViewTextBoxColumn
        Dim RejQty As New GridViewDecimalColumn
        Dim BreakageHead As New GridViewTextBoxColumn
        Dim BreakageQty As New GridViewDecimalColumn
        Dim LabTesting As New GridViewTextBoxColumn
        Dim StartTime As New GridViewDateTimeColumn
        Dim EndTime As New GridViewDateTimeColumn
        Dim MfgDate As New GridViewDateTimeColumn
        Dim ExpDate As New GridViewDateTimeColumn

        Dim FIFOCost As New GridViewDecimalColumn
        Dim LIFOCost As New GridViewDecimalColumn
        Dim AvgCost As New GridViewDecimalColumn
        Dim CostingMethod As New GridViewTextBoxColumn


        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 50
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(LineNo)

        'PPCode.FormatString = ""
        'PPCode.HeaderText = "PP Code"
        'PPCode.Name = colPPCode
        'PPCode.Width = 70
        'PPCode.ReadOnly = True
        'PPCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(PPCode)

        'ProdLineCode.FormatString = ""
        'ProdLineCode.HeaderText = "Prod.Line Code"
        'ProdLineCode.Name = colProdLineCode
        'ProdLineCode.Width = 70
        'ProdLineCode.ReadOnly = True
        'ProdLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(ProdLineCode)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 70
        BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.Columns.Add(BOMCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.Columns.Add(ItemCode)

        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        ItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.Columns.Add(ItemDesc)

        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(UOM)

        BatchQty.FormatString = ""
        BatchQty.HeaderText = "Batch Qty"
        BatchQty.DecimalPlaces = DecimalPointQty
        BatchQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        BatchQty.Name = colBatchQty
        BatchQty.Width = 100
        BatchQty.ReadOnly = True
        BatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(BatchQty)

        PrevProdQty.FormatString = ""
        PrevProdQty.HeaderText = "Prev. Production Qty"
        PrevProdQty.DecimalPlaces = DecimalPointQty
        PrevProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        PrevProdQty.Name = colPrevProdQty
        PrevProdQty.Width = 100
        PrevProdQty.ReadOnly = True
        PrevProdQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(PrevProdQty)

        PendBatchQty.FormatString = ""
        PendBatchQty.HeaderText = "Pending Batch Qty"
        PendBatchQty.DecimalPlaces = DecimalPointQty
        PendBatchQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        PendBatchQty.Name = colPendingBatchQty
        PendBatchQty.Width = 100
        PendBatchQty.ReadOnly = True
        PendBatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(PendBatchQty)

        ReceiptQty.FormatString = ""
        ReceiptQty.HeaderText = "Receipt Qty"
        ReceiptQty.DecimalPlaces = DecimalPointQty
        ReceiptQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        ReceiptQty.Name = colReceiptQty
        ReceiptQty.Width = 100
        ReceiptQty.IsVisible = False
        ReceiptQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(ReceiptQty)

        RejHead.FormatString = ""
        RejHead.HeaderText = "Rejection Head"
        RejHead.Name = colRejHead
        RejHead.Width = 100
        'RejHead.IsVisible = False
        RejHead.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        RejHead.IsVisible = False
        gvBatch.Columns.Add(RejHead)

        RejQty.FormatString = ""
        RejQty.HeaderText = "Rejection Qty"
        RejQty.DecimalPlaces = DecimalPointQty
        RejQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        RejQty.Name = colRejQty
        RejQty.Width = 100
        'RejQty.IsVisible = False
        RejQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        RejQty.IsVisible = False
        gvBatch.Columns.Add(RejQty)

        BreakageHead.FormatString = ""
        BreakageHead.HeaderText = "Breakage Head"
        BreakageHead.Name = colBreakageHead
        BreakageHead.Width = 100
        'BreakageHead.ReadOnly = True
        BreakageHead.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        BreakageHead.IsVisible = False
        gvBatch.Columns.Add(BreakageHead)

        BreakageQty.FormatString = ""
        BreakageQty.HeaderText = "Breakage Qty"
        BreakageQty.DecimalPlaces = DecimalPointQty
        BreakageQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        BreakageQty.Name = colBreakageQty
        BreakageQty.Width = 100
        'BreakageQty.ReadOnly = True
        BreakageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        BreakageQty.IsVisible = False
        gvBatch.Columns.Add(BreakageQty)

        LabTesting.FormatString = ""
        LabTesting.HeaderText = "Lab Testing"
        LabTesting.Name = colLabTesting
        LabTesting.Width = 100
        'LabTesting.ReadOnly = True
        LabTesting.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        LabTesting.IsVisible = False
        gvBatch.Columns.Add(LabTesting)

        Dim repoFinalProdQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQty.FormatString = ""
        repoFinalProdQty.Name = colFINAL_PROD_Qty
        repoFinalProdQty.Width = 100
        repoFinalProdQty.HeaderText = "Final Production Qty"
        repoFinalProdQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQty.DecimalPlaces = 3
        repoFinalProdQty.ReadOnly = False
        gvBatch.MasterTemplate.Columns.Add(repoFinalProdQty)

        Dim repoSP_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Code.FormatString = ""
        repoSP_Location_Code.Name = colSP_Loaction_Code
        repoSP_Location_Code.Width = 100
        repoSP_Location_Code.HeaderText = "Location Code"
        repoSP_Location_Code.ReadOnly = False
        gvBatch.MasterTemplate.Columns.Add(repoSP_Location_Code)

        Dim repoSP_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Desc.FormatString = ""
        repoSP_Location_Desc.Name = colSP_Loaction_Desc
        repoSP_Location_Desc.Width = 150
        repoSP_Location_Desc.HeaderText = "Location Description"
        repoSP_Location_Desc.ReadOnly = True
        gvBatch.MasterTemplate.Columns.Add(repoSP_Location_Desc)
        'StartTime.Format = DateTimePickerFormat.Time
        'StartTime.HeaderText = "Start Time"
        'StartTime.Name = colStartTime
        'StartTime.CustomFormat = "hh:mm tt"
        'StartTime.FormatString = "{0:hh:mm tt}"
        'StartTime.Width = 130
        'StartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(StartTime)

        'EndTime.Format = DateTimePickerFormat.Time
        'EndTime.HeaderText = "End Time"
        'EndTime.CustomFormat = "hh:mm tt"
        'EndTime.FormatString = "{0:hh:mm tt}"
        'EndTime.Name = colEndTime
        'EndTime.Width = 130
        'EndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(EndTime)

        'MfgDate.FormatString = ""
        'MfgDate.HeaderText = "Manufacturing Date"
        'MfgDate.Name = colMfgDate
        'MfgDate.Width = 100
        'MfgDate.ReadOnly = True
        'MfgDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(MfgDate)

        'ExpDate.FormatString = ""
        'ExpDate.HeaderText = "Expiry Date"
        'ExpDate.Name = colExpDate
        'ExpDate.Width = 100
        ''ExpDate.ReadOnly = True
        'ExpDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(ExpDate)

        ''costing columns

        CostingMethod.FormatString = ""
        CostingMethod.HeaderText = "Costing Method"
        CostingMethod.Name = colCost_Method
        CostingMethod.Width = 100
        CostingMethod.ReadOnly = True
        CostingMethod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        CostingMethod.IsVisible = False
        gvBatch.Columns.Add(CostingMethod)

        FIFOCost.FormatString = ""
        FIFOCost.HeaderText = "FIFO Cost"
        FIFOCost.Name = colFIFO_Cost
        FIFOCost.Width = 100
        FIFOCost.ReadOnly = True
        FIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        FIFOCost.IsVisible = False
        gvBatch.Columns.Add(FIFOCost)

        LIFOCost.FormatString = ""
        LIFOCost.HeaderText = "LIFO Cost"
        LIFOCost.Name = colLIFO_Cost
        LIFOCost.Width = 100
        LIFOCost.ReadOnly = True
        LIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        LIFOCost.IsVisible = False
        gvBatch.Columns.Add(LIFOCost)

        AvgCost.FormatString = ""
        AvgCost.HeaderText = "AVG Cost"
        AvgCost.Name = colAVG_Cost
        AvgCost.Width = 100
        AvgCost.ReadOnly = True
        AvgCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        AvgCost.IsVisible = False
        gvBatch.Columns.Add(AvgCost)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvBatch.MasterTemplate.Columns.Add(repoIsPickAutoSerNo) '140

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvBatch.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoShiftCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftCode.HeaderText = "Shift Code"
        repoShiftCode.Name = colShiftCode
        repoShiftCode.Width = 100
        repoShiftCode.ReadOnly = True
        repoShiftCode.IsVisible = True
        repoShiftCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.MasterTemplate.Columns.Add(repoShiftCode)

        Dim repoSectionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSectionCode.HeaderText = "Section Code"
        repoSectionCode.Name = colSectionCode
        repoSectionCode.Width = 100
        repoSectionCode.ReadOnly = True
        repoSectionCode.IsVisible = True
        repoSectionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.MasterTemplate.Columns.Add(repoSectionCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT %"
        repoFatPer.DecimalPlaces = DecimalPointFatSNFPer
        repoFatPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoFatPer.Name = colFAT_Per
        repoFatPer.Width = 100
        repoFatPer.ReadOnly = True
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoFatPer)

        Dim repoFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKG.FormatString = ""
        repoFatKG.HeaderText = "FAT KG"
        repoFatKG.DecimalPlaces = DecimalPointQty
        repoFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFatKG.Name = colFAT_KG
        repoFatKG.Width = 100
        repoFatKG.ReadOnly = True
        repoFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoFatKG)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF %"
        repoSNFPer.DecimalPlaces = DecimalPointFatSNFPer
        repoSNFPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoSNFPer.Name = colSNF_Per
        repoSNFPer.Width = 100
        repoSNFPer.ReadOnly = True
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoSNFPer)

        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF KG"
        repoSNFKG.DecimalPlaces = DecimalPointQty
        repoSNFKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoSNFKG.Name = colSNF_KG
        repoSNFKG.Width = 100
        repoSNFKG.ReadOnly = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoSNFKG)

        gvBatch.EnableFiltering = False
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
        repoIssueCode.ReadOnly = True
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
        gvIssue.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colAvail_FAT_Per
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Available FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = False
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
        repoAvail_SNF_Per.ReadOnly = False
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


        'Dim Issue_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        'Issue_Status.FormatString = ""
        'Issue_Status.Name = colIssue_Status
        'Issue_Status.Width = 100
        'Issue_Status.HeaderText = "Status"
        'Issue_Status.ReadOnly = False
        'Issue_Status.DataSource = GetIssueStatus()
        'Issue_Status.ValueMember = "Code"
        'Issue_Status.DisplayMember = "Name"
        'gvIssue.MasterTemplate.Columns.Add(Issue_Status)

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
        gvIssue.AllowColumnReorder = False
        gvIssue.AllowRowReorder = False
        gvIssue.EnableSorting = False
        gvIssue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvIssue.MasterTemplate.ShowRowHeaderColumn = False
        gvIssue.Rows.AddNew()
        gvIssue.EnableFiltering = False
    End Sub

    Private Sub LoadSPBlankGrid()
        gvStage.Rows.Clear()
        gvStage.Columns.Clear()

        Dim repoARSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSno.FormatString = ""
        repoARSno.Name = colARSno
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
        Status.DataSource = GetARType()
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

    Public Shared Function GetARType() As DataTable
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

    Private Sub LoadQCBlankGrid()
        gvParameter.Rows.Clear()
        gvParameter.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCSno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(reposno)

        Dim reportype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reportype.FormatString = ""
        reportype.HeaderText = "QC From"
        reportype.Width = 80
        reportype.Name = colQCType
        reportype.WrapText = True
        reportype.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(reportype)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.Name = colQCItemCode
        repoItemCode.Width = 100
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoQCItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCItemName.FormatString = ""
        repoQCItemName.Name = colQCItemName
        repoQCItemName.Width = 120
        repoQCItemName.HeaderText = "Item Description"
        repoQCItemName.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoQCItemName)

        Dim repoQCparamcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCparamcode.FormatString = ""
        repoQCparamcode.Name = colQCparamcode
        repoQCparamcode.Width = 100
        repoQCparamcode.HeaderText = "Parameter Code"
        repoQCparamcode.ReadOnly = True
        'bomcode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'bomcode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gvParameter.MasterTemplate.Columns.Add(repoQCparamcode)

        Dim repoQCparam_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCparam_desc.FormatString = ""
        repoQCparam_desc.Name = colQCparam_desc
        repoQCparam_desc.Width = 120
        repoQCparam_desc.HeaderText = "Parameter Description"
        repoQCparam_desc.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoQCparam_desc)

        Dim repotype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotype.FormatString = ""
        repotype.Name = colQCparam_type
        repotype.Width = 120
        repotype.HeaderText = "Parameter Type"
        repotype.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repotype)

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.FormatString = ""
        reponature.Name = colQCparam_nature
        reponature.Width = 120
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(reponature)

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colQCrange1
        repolower.Width = 120
        repolower.HeaderText = "Standard Range"
        repolower.DecimalPlaces = DecimalPointFatSNFPer
        repolower.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repolower.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repolower)

        Dim repoBoolean_Status As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoolean_Status.Name = colQCBooleanStatus
        repoBoolean_Status.Width = 120
        repoBoolean_Status.HeaderText = "Standard Status"
        repoBoolean_Status.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoBoolean_Status)

        Dim repoAlpha_Value As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAlpha_Value.Name = colQCAlphaValue
        repoAlpha_Value.HeaderText = "Standard Value"
        repoAlpha_Value.Width = 120
        repoAlpha_Value.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoAlpha_Value)


        Dim repoActual_Range As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActual_Range.Name = colActual_Range
        repoActual_Range.Width = 120
        repoActual_Range.HeaderText = "Actual Range"
        repoActual_Range.DecimalPlaces = DecimalPointFatSNFPer
        repoActual_Range.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        'repoActual_Range.MaxLength = 30
        gvParameter.MasterTemplate.Columns.Add(repoActual_Range)

        Dim repoActual_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoActual_Status.Name = colActual_Status
        repoActual_Status.Width = 120
        repoActual_Status.HeaderText = "Actual Status"
        repoActual_Status.DataSource = GetQCActualStatus()
        repoActual_Status.ValueMember = "Code"
        repoActual_Status.DisplayMember = "Name"
        gvParameter.MasterTemplate.Columns.Add(repoActual_Status)

        Dim repoActual_Value As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActual_Value.Name = colActual_Value
        repoActual_Value.HeaderText = "Actual Value"
        repoActual_Value.Width = 120
        gvParameter.MasterTemplate.Columns.Add(repoActual_Value)

        Dim repoQc_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoQc_Status.Name = colQc_Status
        repoQc_Status.Width = 120
        repoQc_Status.HeaderText = "QC Status"
        repoQc_Status.DataSource = GetQCType()
        repoQc_Status.ValueMember = "Code"
        repoQc_Status.DisplayMember = "Name"
        'repoQc_Status.ReadOnly = True
        gvParameter.MasterTemplate.Columns.Add(repoQc_Status)

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colQCremarks
        reporem.Width = 120
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gvParameter.MasterTemplate.Columns.Add(reporem)

        gvParameter.AllowDeleteRow = True
        gvParameter.AllowAddNewRow = False
        gvParameter.ShowGroupPanel = False
        gvParameter.AllowColumnReorder = False
        gvParameter.AllowRowReorder = False
        gvParameter.EnableSorting = False
        gvParameter.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvParameter.MasterTemplate.ShowRowHeaderColumn = False
        gvParameter.EnableFiltering = False
        gvParameter.Rows.AddNew()
    End Sub

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

    Sub OpenSerialItem()
        If clsCommon.myCBool(gvBatch.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
            frm.strItemCode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemDesc).Value)
            frm.dblqty = clsCommon.myCdbl(gvBatch.CurrentRow.Cells(colReceiptQty).Value)
            frm.arr = TryCast(gvBatch.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            'gv1.CurrentRow.Cells(colReceiptQty).Value = frm.AcceptedQty
            'gv1.CurrentRow.Cells(colRejQty).Value = frm.RejectedQty
            If Not frm.isCencelButtonClicked Then
                gvBatch.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub

    Sub funReset()
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpDate.Focus()
        txtLocation.Enabled = True
        gvBatch.Rows.Clear()
        Me.txtBatchNo.Value = Nothing
        Me.txtReceivedBy.Value = Nothing
        Me.txtLocation.Value = Nothing
        dtpBatchDate.Text = Nothing
        dtpBatchDate.Value = Nothing
        Me.txtDesc.Text = ""
        TxtManualBatchNo.Text = ""
        lblLineNo.Text = ""
        LblCostCenterCode.Text = ""
        lblCostCenterName.Text = ""
        lblProfitCenterCode.Text = ""
        lblProfitCenterName.Text = ""
        Me.txtComment.Text = ""
        lblConsmSectionCode.Text = ""
        lblConsmSectionLocCode.Text = ""
        gvSectionStock.DataSource = Nothing
        txtCode.MyReadOnly = False
        LOCATIONRIGTHS()
        chkJobWorkInward.Checked = False
    End Sub

    Function AllowToSave(Optional ByVal isPost As Boolean = False) As Boolean
        If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            Return False
        End If
        If clsCommon.myLen(txtBatchNo.Value) <= 0 Then
            myMessages.blankValue("Batch Order Code")
            txtBatchNo.Focus()
            Return False
        End If
        If dtpBatchDate.Value > dtpDate.Value Then
            common.clsCommon.MyMessageBoxShow("Batch Date can not be greater then Document Date")
            dtpBatchDate.Focus()
            Exit Function
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue("Location Code")
            txtLocation.Focus()
            Return False
        End If
        If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
            myMessages.blankValue("Consumption Location Code")
            txtBatchNo.Focus()
            Return False
        End If
        If clsCommon.myLen(lblConsmSectionCode.Text) <= 0 Then
            myMessages.blankValue("Consumption Section Code")
            txtBatchNo.Focus()
            Return False
        End If

        If Me.gvBatch.Rows.Count = 0 Then
            myMessages.blankValue("Batch List is Empty")
            Return False
        End If
        Dim strBatchORderExistIntPIE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Prod_entry_code from TSPL_PP_PRODUCTION_ENTRY where TSPL_PP_PRODUCTION_ENTRY.Batch_Code='" + txtBatchNo.Value + "' and TSPL_PP_PRODUCTION_ENTRY.Prod_entry_code not in ('" + txtCode.Value + "')"))
        If clsCommon.myLen(clsCommon.myCstr(strBatchORderExistIntPIE)) > 0 Then
            Throw New Exception("Please select different Batch Order, Same Batch exists with Production Entry " & strBatchORderExistIntPIE & "  ")
        End If


        ''By Balwinder on 06/12/2019 One Production entry against one batch order.
        Dim qry As String = "select PROD_ENTRY_CODE from tspl_PP_Production_entry where Batch_Code='" + txtBatchNo.Value + "' and PROD_ENTRY_CODE not in ('" + txtCode.Value + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Already Production Entry [" + clsCommon.myCstr(dt.Rows(0)(0)) + "] found againt batch Code [" + txtBatchNo.Value + "]")
        End If

        Dim flag As Boolean = False ''ERO/11/07/19-000680 by balwinder on 02/08/2019
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colSP_Loaction_Code).Value) <= 0 Then
                myMessages.blankValue("Enter Location in Batch Production tab at line no- " & (grow.Index + 1) & "")
                Return False
            End If
            If clsCommon.myCdbl(grow.Cells(colBatchQty).Value) <> clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value) Then
                flag = True
            End If

            If clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value) > 0 AndAlso clsCommon.myCBool(clsItemMaster.IsBatchItem(grow.Cells(colItemCode).Value)) AndAlso clsERPFuncationality.GetBatchWiseApplicableStatus(dtpDate.Value) = True Then
                Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                If arrBatchNo Is Nothing Then
                    Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " . At Line No" + clsCommon.myCstr(grow.Index + 1))
                Else
                    Dim tQty As Decimal = 0
                    For Each objBatch As clsBatchInventory In arrBatchNo
                        tQty += objBatch.Qty
                    Next
                    If tQty <> clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value) Then
                        Throw New Exception("Item : " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " Entered Qty " + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(grow.Index + 1))
                    End If
                End If
            End If
        Next
        If flag Then
            flag = False
            Dim flag_IssueBasis As Boolean = False
            For ii As Integer = 0 To gvIssue.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueItemProductType).Value), "Milk") = CompairStringResult.Equal Then
                    If MI_Consm_Type = 1 Then
                        flag = True
                    Else
                        flag_IssueBasis = True
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueItemProductType).Value), "Milk Product") = CompairStringResult.Equal Then
                    If MP_Consm_Type = 1 Then
                        flag = True
                    Else
                        flag_IssueBasis = True
                    End If
                Else
                    If Othr_Consm_Type = 1 Then
                        flag = True
                    Else
                        flag_IssueBasis = True
                    End If
                End If
            Next
            If flag_IssueBasis Then
                If clsCommon.MyMessageBoxShow("Batch Qty is Not Equal to Final Produce Qty." + Environment.NewLine + "All Issued Qty Will be consumed" + Environment.NewLine + "Do You want to Continue...", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                    Return False
                End If
            End If

            If flag Then
                flag = False
                For Each grow As GridViewRowInfo In gvWreckage.Rows
                    If clsCommon.myLen(grow.Cells(colWFItem_Code).Value) > 0 AndAlso (clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0 OrElse clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) > 0) Then
                        flag = True
                    End If
                    If Not flag Then
                        Throw New Exception("Please enter Back to Silo/Wreckage Qty in becuase Produce Qty is not equal to Bath order Qty." + Environment.NewLine + " Wreckage Tab at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                Next
            End If
        End If


        '' validation for STAGE ROCESS stages existence
        If gvStage.Rows.Count <= 0 Then
            Throw New Exception("Production Entry Stages not found for selected child batch's section and structure.")
        End If
        For Each dr As GridViewRowInfo In gvStage.Rows
            If clsCommon.myLen(dr.Cells(colUnit_Code).Value) <= 0 Then
                Throw New Exception("No any Item found of type Milk or Milk Product in Issue Grid.")
            End If
            'total = total + 1
        Next
        For Each grow As GridViewRowInfo In GvScrap.Rows
            If clsCommon.myLen(grow.Cells(colScrapItem_Code).Value) <= 0 Then
                Continue For
            End If
            If clsCommon.myLen(grow.Cells(colScrapLocation_Code).Value) <= 0 Then
                myMessages.blankValue("Enter Location in Scrap Detail tab at line no- " & (grow.Index + 1) & "")
                Return False
            End If
            '' apply qty validation

        Next
        '-----------------qc grid----------------------------------------------
        If isPost = True Then
            '' check item stock on section
            '' add back to and flash items 
            For Each grow As GridViewRowInfo In gvWreckage.Rows
                If clsCommon.myLen(grow.Cells(colWFItem_Code).Value) <= 0 Then
                    Continue For
                End If
                If clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(colWFLocation_Code).Value) <= 0 Then
                        Throw New Exception("Enter Back To Location in Wreckage Tab at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                End If
            Next

            Dim paramcode As String = ""
            Dim nature As String = ""
            Dim range1 As Decimal = Nothing
            Dim range2 As Decimal = Nothing
            Dim Actual_Range As String = ""
            Dim Actual_Value As String = ""
            Dim Actual_Status As String = ""
            Dim QC_Status As String = ""
            For ii As Integer = 0 To gvParameter.Rows.Count - 1
                paramcode = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colQCparamcode).Value)
                If ii = 0 AndAlso clsCommon.myLen(paramcode) <= 0 Then
                    RadPageView1.SelectedPage = pageParameterDetail
                    Throw New Exception("Press Go button for filling QC detail in grid")
                End If

                nature = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colQCparam_nature).Tag)
                range1 = clsCommon.myCdbl(gvParameter.Rows(ii).Cells(colQCrange1).Value)
                'range2 = clsCommon.myCdbl(gvParameter.Rows(ii).Cells(colQCrange2).Value)
                Actual_Range = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colActual_Range).Value)
                Actual_Value = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colActual_Value).Value)
                Actual_Status = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colActual_Status).Value)
                QC_Status = clsCommon.myCstr(gvParameter.Rows(ii).Cells(colQc_Status).Value)

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
            '' validation for stage
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
                For Each obj As clsPPPELogSheetDetail In grow.Tag
                    If clsCommon.myLen(obj.Parameter_ACT_Value) <= 0 Then
                        Throw New Exception("QC Log Sheet not filled for stage " & grow.Cells(colStage_Code).Value & " for parameter " & obj.QCLM_CODE & ".")
                    End If
                Next
            Next
            '' check fat/snf control
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.FatSNFControlOnProductionConsumption, clsFixedParameterCode.FatSNFControlOnProductionConsumption, Nothing), "1") = CompairStringResult.Equal Then
                If ValidateFatSNFQuantityControl() = False Then
                    Return False
                End If
            End If
            UcAttachment1.AllowToSave()
        End If



        'End If

        Return True
    End Function

    Function ValidateFatSNFQuantityControl() As Boolean
        If Not settAllowNegativeStockInDairyProduction Then
            Dim TotIssueFatKg As Decimal = 0
            Dim TotIssueSnfKg As Decimal = 0
            Dim TotIssueQty As Decimal = 0

            Dim TotProdFatKg As Decimal = 0
            Dim TotProdSnfKg As Decimal = 0
            Dim TotProdQty As Decimal = 0

            '' for issued/added qty
            For Each grow As GridViewRowInfo In gvIssue.Rows
                If clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                    TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colAvail_FAT_KG).Value)
                    TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colAvail_SNF_KG).Value)
                    TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colIssueItemCode).Value, grow.Cells(colIssueUom).Value, clsCommon.myCdbl(grow.Cells(colAvail_Qty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colAvail_Qty).Value)
                End If
            Next
            For Each grow As GridViewRowInfo In gvWreckage.Rows
                Dim ProductType As String = clsItemMaster.GetItemProductType(grow.Cells(colWFItem_Code).Value, Nothing)
                If clsCommon.CompairString(ProductType, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(ProductType, "MP") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value) > 0 Then
                        TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_KG).Value)
                        TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_KG).Value)
                        TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colWFItem_Code).Value, grow.Cells(colWFUnit_Code).Value, clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)
                    End If
                End If
            Next

            '' for Produced/removed qty
            For Each grow As GridViewRowInfo In gvBatch.Rows
                Dim ProductType As String = clsItemMaster.GetItemProductType(grow.Cells(colItemCode).Value, Nothing)
                If clsCommon.CompairString(ProductType, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(ProductType, "MP") = CompairStringResult.Equal Then
                    TotProdFatKg = TotProdFatKg + clsCommon.myCdbl(grow.Cells(colFAT_KG).Value)
                    TotProdSnfKg = TotProdSnfKg + clsCommon.myCdbl(grow.Cells(colSNF_KG).Value)
                    TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colItemCode).Value, grow.Cells(colUOM).Value, grow.Cells(colFINAL_PROD_Qty).Value, Nothing)
                End If
            Next
            For Each grow As GridViewRowInfo In gvWreckage.Rows
                Dim ProductType As String = clsItemMaster.GetItemProductType(grow.Cells(colWFItem_Code).Value, Nothing)
                If clsCommon.CompairString(ProductType, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(ProductType, "MP") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0 Then
                        TotProdFatKg = TotProdFatKg + clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_KG).Value)
                        TotProdSnfKg = TotProdSnfKg + clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_KG).Value)
                        TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colWFItem_Code).Value, grow.Cells(colWFUnit_Code).Value, clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value)
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
        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub updateBatchGridParameter()
        For Each grow As GridViewRowInfo In gvParameter.Rows
            isCellValueChanged = True
            UpdateBatchFatSNF(clsCommon.myCstr(grow.Cells(colQCItemCode).Value), clsCommon.myCdbl(grow.Cells(colActual_Range).Value), clsCommon.myCstr(grow.Cells(colQCparam_type).Value), clsCommon.myCstr(grow.Cells(colQCType).Value))
            isCellValueChanged = False
        Next

    End Sub

    Function SaveData(ByVal ChekBtnPost As Boolean) As Boolean
        Try
            '' Anubhooti 09-Sep-2014 BM00000003735
            If ChekBtnPost = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                    Exit Function
                End If
            End If
            updateBatchGridParameter()
            If AllowToSave() Then
                Dim obj As New clsProductionEntry
                obj.PROD_ENTRY_CODE = Me.txtCode.Value
                obj.DESCRIPTION = Me.txtDesc.Text
                obj.PROD_DATE = Me.dtpDate.Value
                obj.Batch_Code = Me.txtBatchNo.Value
                obj.BATCH_DATE = Me.dtpBatchDate.Value
                obj.RECEIVED_BY = clsCommon.myCstr(Me.txtReceivedBy.Value)
                obj.LOCATION_CODE = clsCommon.myCstr(Me.txtLocation.Value)
                obj.COMMENTS = Me.txtComment.Text
                If gvStage.Tag Is Nothing Then
                    obj.Section_Stage_Map_Code = ""
                Else
                    obj.Section_Stage_Map_Code = gvStage.Tag
                End If
                obj.CONSM_LOCATION_CODE = lblConsmSectionLocCode.Text
                obj.CONSM_SECTION_CODE = lblConsmSectionCode.Text
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                obj.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                obj.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
                obj.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
                obj.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
                ''----------------
                Dim obj1 As clsProductionEntryDetail
                'objList = New List(Of clsProductionEntryDetail)
                obj.Is_Job_Work_Inward = chkJobWorkInward.Checked
                obj.ArrBatchItem = New List(Of clsProductionEntryDetail)
                obj.ArrIssueItem = New List(Of clsProcessProductionPEIssueItemDetail)
                obj.ArrQC = New List(Of clsProcessProductionPEQCDetail)
                obj.ArrStage = New List(Of clsProcessProductionPEStageDetail)
                obj.ArrWF = New List(Of clsPPPEWFItemDetail)
                obj.ArrScrap = New List(Of clsPPScrapItemDetail)


                For Each grow As GridViewRowInfo In gvBatch.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        obj1 = New clsProductionEntryDetail()
                        obj1.PROD_ENTRY_CODE = txtCode.Value
                        obj1.Shift_Code = clsCommon.myCstr(grow.Cells(colShiftCode).Value)
                        obj1.Section_Code = clsCommon.myCstr(grow.Cells(colSectionCode).Value)
                        obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)

                        obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        obj1.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                        obj1.RECEIPT_QTY = clsCommon.myCdbl(grow.Cells(colReceiptQty).Value)
                        obj1.REJ_HEAD = clsCommon.myCstr(grow.Cells(colRejHead).Value)
                        obj1.REJ_QTY = clsCommon.myCdbl(grow.Cells(colRejQty).Value)
                        obj1.BREAKAGE_HEAD = clsCommon.myCstr(grow.Cells(colBreakageHead).Value)
                        obj1.BREAKAGE_QTY = clsCommon.myCdbl(grow.Cells(colBreakageQty).Value)
                        obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        obj1.LAB_TESTING = clsCommon.myCstr(grow.Cells(colLabTesting).Value)

                        obj1.FAT_Per = clsCommon.myCdbl(grow.Cells(colFAT_Per).Value)
                        obj1.FAT_KG = clsCommon.myCdbl(grow.Cells(colFAT_KG).Value)
                        obj1.SNF_Per = clsCommon.myCdbl(grow.Cells(colSNF_Per).Value)
                        obj1.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNF_KG).Value)

                        obj1.FINAL_PRODUCTION_QTY = clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value)
                        If obj1.FINAL_PRODUCTION_QTY <= 0 Then
                            Throw New Exception("Please enter final produce Qty in Batch Production TAB")
                        End If

                        obj1.LOCATION_CODE = clsCommon.myCstr(grow.Cells(colSP_Loaction_Code).Value)
                        obj1.arrBatchItem = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                        obj.ArrBatchItem.Add(obj1)
                    End If
                Next
                '' assign value to Issue item array
                For Each grow As GridViewRowInfo In gvIssue.Rows
                    Dim objtr As New clsProcessProductionPEIssueItemDetail()

                    objtr.SNO = CInt(grow.Cells(colIssueSno).Value)
                    objtr.Issue_Code = clsCommon.myCstr(grow.Cells(colIssue_Code).Value)
                    objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colIssueItemCode).Value)
                    objtr.Item_Type = clsCommon.myCstr(grow.Cells(colIssueItemType).Value)
                    objtr.Product_Type = clsCommon.myCstr(grow.Cells(colIssueItemProductType).Value)
                    objtr.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colAvail_FAT_KG).Value)
                    objtr.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colAvail_FAT_Per).Value)
                    objtr.Avail_Qty = clsCommon.myCdbl(grow.Cells(colAvail_Qty).Value)
                    objtr.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colAvail_SNF_KG).Value)
                    objtr.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colAvail_SNF_Per).Value)

                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemarks).Value)
                    objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(txtCode.Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colIssueUom).Value)
                    objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colIssueUOMDesc).Value)

                    objtr.From_Loaction_Code = clsCommon.myCstr(grow.Cells(colFrom_Loaction_Code).Value)
                    objtr.To_Location_Code = clsCommon.myCstr(grow.Cells(colTo_Location_Code).Value)

                    'objtr.Issue_Status = clsCommon.myCstr(grow.Cells(colIssueStatus).Value)
                    '' production costing columns
                    objtr.Fat_Rate = clsCommon.myCdbl(grow.Cells(colIssue_Fat_Rate).Value)
                    objtr.SNF_Rate = clsCommon.myCdbl(grow.Cells(colIssue_SNF_Rate).Value)
                    objtr.Fat_Amt = clsCommon.myCdbl(grow.Cells(colIssue_Fat_Amt).Value)
                    objtr.SNF_Amt = clsCommon.myCdbl(grow.Cells(colIssue_SNF_Amt).Value)
                    If clsCommon.myLen(objtr.Item_Code) > 0 Then
                        obj.ArrIssueItem.Add(objtr)
                    End If
                Next


                '' assign value to QC array
                For Each grow As GridViewRowInfo In gvParameter.Rows
                    Dim objtr As New clsProcessProductionPEQCDetail()

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
                    Dim objtr As New clsProcessProductionPEStageDetail()

                    objtr.SNO = CInt(grow.Cells(colARSno).Value)
                    objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                    objtr.Stage_Code = clsCommon.myCstr(grow.Cells(colStage_Code).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit_Code).Value)
                    objtr.Log_Sheet_No = clsCommon.myCstr(grow.Cells(colLog_Sheet_No).Value)
                    objtr.Status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                    objtr.Received_Qty = clsCommon.myCstr(grow.Cells(colReceived_Qty).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colSPRemarks).Value)
                    objtr.Section_Code = clsCommon.myCstr(grow.Cells(colSPSection).Value)
                    objtr.Structure_Code = clsCommon.myCstr(grow.Cells(colSPProdCategory).Value)

                    objtr.PRODUCTION_ENTRY_CODE = clsCommon.myCstr(txtCode.Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit_Code).Value)
                    objtr.Batch_Code = clsCommon.myCstr(grow.Cells(colStageBatch_Code).Value)

                    objtr.SPQCList = grow.Tag
                    If clsCommon.myLen(objtr.Stage_Code) > 0 Then
                        obj.ArrStage.Add(objtr)
                    End If
                Next
                '' assign value to wreckage and flashing
                For Each grow As GridViewRowInfo In gvWreckage.Rows
                    Dim objtr As New clsPPPEWFItemDetail

                    objtr.SNO = CInt(grow.Cells(colWFSNO).Value)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colWFItem_Code).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colWFUnit_Code).Value)
                    objtr.BACK_QTY = clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value)
                    objtr.WRECKAGE_QTY = clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)
                    objtr.Location_Code = clsCommon.myCstr(grow.Cells(colWFLocation_Code).Value)

                    objtr.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_KG).Value)
                    objtr.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colWFAvail_FAT_Per).Value)
                    objtr.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_KG).Value)
                    objtr.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colWFAvail_SNF_Per).Value)

                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colWFRemarks).Value).Replace("'", "`")

                    If clsCommon.myLen(objtr.Item_Code) > 0 Then
                        obj.ArrWF.Add(objtr)
                    End If
                Next

                '' assign value to Scrap
                For Each grow As GridViewRowInfo In GvScrap.Rows
                    Dim objScrap As New clsPPScrapItemDetail

                    objScrap.SNO = CInt(grow.Cells(colScrapSNO).Value)
                    objScrap.Item_Code = clsCommon.myCstr(grow.Cells(colScrapItem_Code).Value)
                    objScrap.Unit_Code = clsCommon.myCstr(grow.Cells(colScrapUnit_Code).Value)
                    objScrap.Scrap_QTY = clsCommon.myCdbl(grow.Cells(colScrapQty).Value)
                    objScrap.Location_Code = clsCommon.myCstr(grow.Cells(colScrapLocation_Code).Value)
                    objScrap.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colScrapAvail_FAT_KG).Value)
                    objScrap.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colScrapAvail_FAT_Per).Value)
                    objScrap.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colScrapAvail_SNF_KG).Value)
                    objScrap.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colScrapAvail_SNF_Per).Value)

                    objScrap.Remarks = clsCommon.myCstr(grow.Cells(colScrapRemarks).Value).Replace("'", "`")

                    If clsCommon.myLen(objScrap.Item_Code) > 0 Then
                        obj.ArrScrap.Add(objScrap)
                    End If
                Next

                '' save overhead cost
                If ShowoverheadCost = "1" Then
                    Dim obj3 As New clsConsumptionCostWithoutBatch
                    For Each grow As GridViewRowInfo In gvProductionCost.Rows
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                            obj3 = New clsConsumptionCostWithoutBatch()
                            obj3.PROD_ENTRY_CODE = txtCode.Value

                            obj3.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                            obj3.BOM_Desc = clsCommon.myCstr(grow.Cells(colBOMDesc).Value)

                            obj3.COST_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            obj3.COST_CODE_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                            obj3.Main_ITEM_CODE = clsCommon.myCstr(grow.Cells(colMainItemCode).Value)
                            obj3.Main_ITEM_Desc = clsCommon.myCstr(grow.Cells(colMainItemDesc).Value)
                            obj3.MAIN_UOM = clsCommon.myCstr(grow.Cells(colMainUOM).Value)
                            obj3.MAIN_UOM_Desc = clsCommon.myCstr(grow.Cells(colMainUOMDesc).Value)
                            obj3.OverHead_Cost = clsCommon.myCdbl(grow.Cells(colAVG_Cost).Value)

                            obj.ArrConsmCost.Add(obj3)
                        End If
                    Next
                End If

                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, obj.ArrBatchItem, isNewEntry, clsCommon.myCstr(txtCode.Value))

                If issaved = True Then
                    UcAttachment1.SaveData(obj.PROD_ENTRY_CODE)
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.PROD_ENTRY_CODE, NavigatorType.Current)
                    Return True
                End If

                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isInsideLoadData = True
        obj = clsProductionEntry.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Batch_Code) > 0 Then

            isNewEntry = False
            btnSave.Text = "Update"
            If obj.POSTED Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                btnCancel.Enabled = True
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                btnCancel.Enabled = False
            End If
            Dim ii As Int16 = 0
            LoadBlankGrid()
            txtCode.Value = obj.PROD_ENTRY_CODE
            Me.txtDesc.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.txtComment.Text = clsCommon.myCstr(obj.COMMENTS)
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            TxtManualBatchNo.Text = obj.ManualBatchNo
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            lblLineNo.Text = obj.LINE_NO
            LblCostCenterCode.Text = obj.CostCenterCode
            lblCostCenterName.Text = obj.CostCenterName
            lblProfitCenterCode.Text = obj.ProfitCenterCode
            lblProfitCenterName.Text = obj.ProfitCenterName
            ''----------------
            Me.txtReceivedBy.Value = clsCommon.myCstr(obj.RECEIVED_BY)
            Me.txtLocation.Value = obj.LOCATION_CODE
            Me.dtpDate.Value = obj.PROD_DATE
            Me.dtpBatchDate.Value = obj.BATCH_DATE
            Me.lblLocation.Text = obj.LOCATION_NAME
            Me.lblEmpName.Text = obj.RECEIVED_BY_NAME
            Me.txtBatchNo.Value = obj.Batch_Code
            gvStage.Tag = obj.Section_Stage_Map_Code
            lblConsmSectionLocCode.Text = obj.CONSM_LOCATION_CODE
            lblConsmSectionCode.Text = obj.CONSM_SECTION_CODE
            chkJobWorkInward.Checked = obj.Is_Job_Work_Inward
            Dim arr_BatchItem As New List(Of String)
            Dim arr_WrckageItem As New List(Of String)
            Dim arr_ScrapItem As New List(Of String)
            arr_BatchItem = New List(Of String)
            arr_WrckageItem = New List(Of String)
            arr_ScrapItem = New List(Of String)

            If (obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0) Then
                For Each objTr As clsProductionEntryDetail In obj.ArrBatchItem
                    gvBatch.Rows.AddNew()
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Tag = objTr.arrSrItem
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLineNo).Value = Me.gvBatch.Rows.Count
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colShiftCode).Value = clsCommon.myCstr(objTr.Shift_Code)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSectionCode).Value = clsCommon.myCstr(objTr.Section_Code)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(objTr.BOM_CODE)

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(objTr.ITEM_CODE)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(objTr.ITEM_DESCRIPTION)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCdbl(objTr.BATCH_QTY)

                    '' new code 
                    Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value = clsProductionEntry.GetPrevProductionQty(txtBatchNo.Value, txtCode.Value, objTr.ITEM_CODE, Nothing)
                    Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPendingBatchQty).Value = objTr.BATCH_QTY - clsCommon.myCdbl(Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value)

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colReceiptQty).Value = clsCommon.myCdbl(objTr.RECEIPT_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colRejHead).Value = clsCommon.myCstr(objTr.REJ_HEAD)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colRejQty).Value = clsCommon.myCdbl(objTr.REJ_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBreakageHead).Value = clsCommon.myCstr(objTr.BREAKAGE_HEAD)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBreakageQty).Value = clsCommon.myCdbl(objTr.BREAKAGE_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLabTesting).Value = objTr.LAB_TESTING

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value = objTr.UNIT_CODE
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = objTr.FINAL_PRODUCTION_QTY
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = objTr.LOCATION_CODE
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(objTr.LOCATION_CODE, Nothing)

                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colStartTime).Value = objTr.START_TIME
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colEndTime).Value = objTr.END_TIME

                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colMfgDate).Value = objTr.MFG_DATE
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colExpDate).Value = objTr.EXP_DATE

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colCost_Method).Value = objTr.Costing_Method
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFIFO_Cost).Value = objTr.FIFO_Cost
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLIFO_Cost).Value = objTr.LIFO_Cost
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colAVG_Cost).Value = objTr.AVG_Cost

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_Per).Value = objTr.FAT_Per
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_KG).Value = objTr.FAT_KG
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_Per).Value = objTr.SNF_Per
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_KG).Value = objTr.SNF_KG
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Tag = objTr.arrBatchItem
                    If clsCommon.myLen(objTr.ITEM_CODE) > 0 AndAlso Not arr_BatchItem.Contains(objTr.ITEM_CODE) Then
                        arr_BatchItem.Add(objTr.ITEM_CODE)
                    End If
                Next
            End If

            '' load issue item grid
            If obj.ArrIssueItem IsNot Nothing AndAlso obj.ArrIssueItem.Count > 0 Then
                For Each objtr As clsProcessProductionPEIssueItemDetail In obj.ArrIssueItem
                    gvIssue.Rows.AddNew()
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = objtr.SNO
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).Value = objtr.Issue_Code
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).ReadOnly = True
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colFrom_Loaction_Code).Value = objtr.From_Loaction_Code
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTo_Location_Code).Value = objtr.To_Location_Code

                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = objtr.Item_Code
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = objtr.Item_Desc
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = objtr.Item_Type
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = clsItemMaster.ProductType(objtr.Product_Type)
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
                    'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Status).Value = objtr.Issue_Status

                    '' production costing columns
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = objtr.Fat_Rate
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = objtr.SNF_Rate
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = objtr.Fat_Amt
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = objtr.SNF_Amt
                Next
                'gvIssue.Rows.AddNew()
            End If

            '' load Stage Process grid
            If obj.ArrStage IsNot Nothing AndAlso obj.ArrStage.Count > 0 Then
                For Each objtr As clsProcessProductionPEStageDetail In obj.ArrStage
                    gvStage.Rows.AddNew()
                    gvStage.Rows(gvStage.Rows.Count - 1).Cells(colARSno).Value = objtr.SNO

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

            '' load wreckage grid
            If obj.ArrWF IsNot Nothing AndAlso obj.ArrWF.Count > 0 Then
                gvWreckage.Rows.Clear()
                For Each objtr As clsPPPEWFItemDetail In obj.ArrWF
                    gvWreckage.Rows.AddNew()
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFSNO).Value = objtr.SNO
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value = objtr.Item_Code
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFItemName).Value = objtr.Item_Desc 'clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value = objtr.Unit_Code
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFUnit_Desc).Value = objtr.Unit_Desc
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).Value = objtr.BACK_QTY
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).Value = objtr.WRECKAGE_QTY
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFLocation_Code).Value = objtr.Location_Code
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_KG).Value = objtr.Avail_FAT_KG
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_Per).Value = objtr.Avail_FAT_Per
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_KG).Value = objtr.Avail_SNF_KG
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_Per).Value = objtr.Avail_SNF_Per
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_KG).ReadOnly = True
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_FAT_Per).ReadOnly = False
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_KG).ReadOnly = True
                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFAvail_SNF_Per).ReadOnly = False

                    gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFRemarks).Value = objtr.Remarks
                    If objtr.BACK_QTY > 0 Then
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = True
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = False
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = False
                    End If
                    If objtr.WRECKAGE_QTY > 0 Then
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFWRECKAGE_QTY).ReadOnly = False
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFBACK_QTY).ReadOnly = True
                        gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFLocation_Code).ReadOnly = True
                    End If

                    If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_WrckageItem.Contains(objtr.Item_Code) Then
                        arr_WrckageItem.Add(objtr.Item_Code)
                    End If
                Next
            Else
                gvWreckage.Rows.AddNew()
            End If

            '' load Scrap grid
            If obj.ArrScrap IsNot Nothing AndAlso obj.ArrScrap.Count > 0 Then
                GvScrap.Rows.Clear()
                For Each objtr As clsPPScrapItemDetail In obj.ArrScrap
                    GvScrap.Rows.AddNew()
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapSNO).Value = objtr.SNO
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapItem_Code).Value = objtr.Item_Code
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapItemName).Value = objtr.Item_Desc
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapUnit_Code).Value = objtr.Unit_Code
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapUnit_Desc).Value = objtr.Unit_Desc
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapQty).Value = objtr.Scrap_QTY
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapLocation_Code).Value = objtr.Location_Code

                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_FAT_KG).Value = objtr.Avail_FAT_KG
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_FAT_Per).Value = objtr.Avail_FAT_Per
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_SNF_KG).Value = objtr.Avail_SNF_KG
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_SNF_Per).Value = objtr.Avail_SNF_Per
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_FAT_KG).ReadOnly = True
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_FAT_Per).ReadOnly = False
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_SNF_KG).ReadOnly = True
                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapAvail_SNF_Per).ReadOnly = False

                    GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapRemarks).Value = objtr.Remarks

                    If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_WrckageItem.Contains(objtr.Item_Code) Then
                        arr_ScrapItem.Add(objtr.Item_Code)
                    End If
                Next
            Else
                GvScrap.Rows.AddNew()
            End If

            '' load QC grid
            If obj.ArrQC IsNot Nothing AndAlso obj.ArrQC.Count > 0 Then

                For Each objtr As clsProcessProductionPEQCDetail In obj.ArrQC
                    gvParameter.Rows.AddNew()
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCSno).Value = objtr.sno
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = objtr.QC_Type

                    If clsCommon.myLen(objtr.QC_Type) <= 0 AndAlso arr_WrckageItem.Contains(objtr.Item_Code) Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Wreckage"
                    ElseIf clsCommon.myLen(objtr.QC_Type) <= 0 AndAlso arr_BatchItem.Contains(objtr.Item_Code) Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Batch Order"
                    ElseIf clsCommon.myLen(objtr.QC_Type) <= 0 AndAlso arr_ScrapItem.Contains(objtr.Item_Code) Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Scrap"
                    End If

                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCItemCode).Value = objtr.Item_Code
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCItemName).Value = objtr.ItemDesc
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparamcode).Value = objtr.param_code
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_desc).Value = objtr.param_desc
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_type).Value = objtr.param_type
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Value = objtr.param_nature_Desc
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag = objtr.param_nature
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCrange1).Value = objtr.Standard_Range
                    'gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCrange2).Value = objtr.urange

                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCBooleanStatus).Value = objtr.Standard_Status
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCAlphaValue).Value = objtr.Standard_Value

                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).Value = objtr.Actual_Range
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).Value = objtr.Actual_Status
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).Value = objtr.Actual_Value
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQc_Status).Value = objtr.Qc_Status
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCremarks).Value = objtr.remarks

                    If clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "R") = CompairStringResult.Equal Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = False
                        'gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).Style.Font = Color.Green

                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                    ElseIf clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "A") = CompairStringResult.Equal Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = False
                    ElseIf clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "B") = CompairStringResult.Equal Then
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = False
                        gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                    End If
                Next
                UcAttachment1.LoadData(obj.PROD_ENTRY_CODE)
            End If
            If ShowoverheadCost = "1" Then
                gvProductionCost.Rows.Clear()
                For Each obj3 As clsConsumptionCostWithoutBatch In obj.ArrConsmCost
                    If clsCommon.myLen(obj3.COST_CODE) > 0 Then
                        gvProductionCost.Rows.AddNew()
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colLineNo).Value = gvProductionCost.Rows.Count

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMCode).Value = obj3.BOM_CODE
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMDesc).Value = obj3.BOM_Desc

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemCode).Value = obj3.COST_CODE
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemDesc).Value = obj3.COST_CODE_Desc
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemCode).Value = obj3.Main_ITEM_CODE
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemDesc).Value = obj3.Main_ITEM_Desc
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOM).Value = obj3.MAIN_UOM
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOMDesc).Value = obj3.MAIN_UOM_Desc
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colAVG_Cost).Value = obj3.OverHead_Cost

                    End If
                Next
            End If
            '' load section 
            FillSection()
        End If
        isInsideLoadData = False
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()

        Try

            If (myMessages.postConfirm()) Then
                If AllowToSave(True) And SaveData(True) Then
                    '' Anubhooti 09-Sep-2014 BM00000003735
                    If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                        Exit Sub
                    End If
                    clsProductionEntry.PostData(Form_ID, txtCode.Value, arrLoc, True)
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
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
                If (clsProductionEntry.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim check As Boolean = False
        check = clsProductionEntry.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsProductionEntry.GetFinder(" TSPL_PP_PRODUCTION_ENTRY.location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += "  and  Location_Code in (" + arrLoc + ")"
            End If
            txtLocation.Value = clsLocation.getFinder(WhrCls, Me.txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(Me.txtLocation.Value, Nothing)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmProductionEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gvBatch.CurrentCell IsNot Nothing Then

            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                "TSPL_PP_PRODUCTION_ENTRY " + Environment.NewLine +
                                                "TSPL_PP_PRODUCTION_ENTRY_DETAIL " + Environment.NewLine +
                                                "TSPL_BATCH_ITEM " + Environment.NewLine +
                                                "TSPL_PP_PE_ISSUE_ITEM_DETAIL " + Environment.NewLine +
                                                "TSPL_PP_PE_STAGE_DETAIL " + Environment.NewLine +
                                                "TSPL_PP_PE_STAGE_QC_LOG_SHEET " + Environment.NewLine +
                                                "TSPL_PP_PE_QC_DETAIL " + Environment.NewLine +
                                                "TSPL_PP_PE_WRECKAGE_FLASHING " + Environment.NewLine +
                                                "TSPL_PP_PE_SCRAP_DETAIL " + Environment.NewLine +
                                                "TSPL_PP_COST_WITHOUT_BATCH " + Environment.NewLine +
                                                "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                                                "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " + Environment.NewLine +
                                                "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                                "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                                "TSPL_BATCH_ITEM " + Environment.NewLine +
                                                "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine +
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
        ElseIf e.KeyData = (Keys.Control + Keys.W) Then
            RadPageView1.SelectedPage = pageWreckageAndFlashing
        ElseIf e.KeyData = (Keys.Control + Keys.A + Keys.T) Then
            'RadPageView1.SelectedPage = 
        ElseIf e.KeyData = (Keys.Control + Keys.B) Then
            RadPageView1.SelectedPage = pageBatchProduction
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'If txtCode.Value = "" Then
        '    myMessages.blankValue("Requisition Number")
        'Else
        '    funPrint()
        'End If
    End Sub

    Private Sub funPrint()
        'Try
        '    Dim no As Integer = 0
        '    Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date , " & _
        '    "convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , " & _
        '    "TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By , " & _
        '    "TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Specification, " & _
        '    "TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty, " & _
        '    "(select SUM( case when InOut='I' then Qty else  -1* Qty end )from TSPL_INVENTORY_MOVEMENT where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_REQUISITION_HEAD.Location) as AvaiQty  , " & _
        '    "TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , " & _
        '    "TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,'' as AuthorizeBy ,TSPL_REQUISITION_HEAD.Request_By, " & _
        '    "TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , " & _
        '    "TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading  " & _
        '    "from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"
        '    If txtReceiptCode.Value <> "" Then
        '        qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReceiptCode.Value + "'"
        '    End If
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        If dt.Rows(i)("vendor_name").ToString() <> "" Then
        '            no = no + 1
        '        End If
        '    Next
        '    If no = 0 Then
        '        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisitionWithoutVendor-G", "Purchase Requisition")
        '        Else
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
        '        End If

        '    Else
        '        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisition-G", "Purchase Requisition")
        '        Else
        '            PurchaseOrderViewer.funreport(dt, "PurchaseRequisition", "Purchase Requisition")
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub txtReceivedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReceivedBy._MYValidating
        Try
            Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee(txtReceivedBy.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
                txtReceivedBy.Value = obj.EMP_CODE
                lblEmpName.Text = obj.Emp_Name
            Else
                txtReceivedBy.Value = ""
                lblEmpName.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtBatchNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBatchNo._MYValidating
        Try
            'and Quantity >[Production Quantity] 
            Me.txtBatchNo.Value = clsProductionEntry.GetBatchFinder("Posted='1' and Quantity>coalesce([Production Quantity],0) and [Manual Closed]=0 and convert (date, Date,103) <= convert (date,'" + dtpDate.Value + "',103)  and [Location Code] in (" + arrLoc + ")", Me.txtBatchNo.Value, isButtonClicked, txtCode.Value)
            If clsCommon.myLen(Me.txtBatchNo.Value) > 0 Then
                If UseProductionPlaningDateForWholeProductionCycle = True Then
                    dtpDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Batch_Date from TSPL_PP_BATCH_ORDER_HEAD where batch_Code='" + txtBatchNo.Value + "'"))
                End If
                Dim objBatch As clsProcessBatchOrder = clsProcessBatchOrder.GetData(Me.txtBatchNo.Value, arrLoc, NavigatorType.Current)
                If Not objBatch Is Nothing Then
                    dtpBatchDate.Value = objBatch.Batchdate
                    Me.txtLocation.Value = objBatch.locationcode
                    Me.lblLocation.Text = objBatch.locationname
                    ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                    TxtManualBatchNo.Text = objBatch.ManualBatchNo
                    ''richa agarwal againt ticket no BHA/02/07/18-000120
                    lblLineNo.Text = objBatch.LINE_NO
                    LblCostCenterCode.Text = objBatch.CostCenterCode
                    lblCostCenterName.Text = objBatch.CostCenterName
                    lblProfitCenterCode.Text = objBatch.ProfitCenterCode
                    lblProfitCenterName.Text = objBatch.ProfitCenterName
                    ''----------------
                    lblConsmSectionLocCode.Text = clsProductionEntry.GetBatchConsumptionSection(txtLocation.Value, txtBatchNo.Value)
                    If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
                        lblConsmSectionCode.Text = ""
                        clsCommon.MyMessageBoxShow("Consumption Location not found for batch " & txtBatchNo.Value & "")
                        Exit Sub
                    Else
                        lblConsmSectionCode.Text = clsLocation.GetSectionCode(lblConsmSectionLocCode.Text, Nothing)
                    End If
                    InitialLoadAllGrid(objBatch)
                End If
            End If
            chkJobWorkInward.Checked = clsProcessBatchOrder.IsJobWorkBatchOrder(txtBatchNo.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'and TSPL_PP_BATCH_ORDER_HEAD.location_code in (" + arrLoc + ")
    End Sub

    Sub ShowBatchItems(ByVal objBatch As clsProcessBatchOrder)
        If isNewEntry = False Then
            Exit Sub
        End If
        gvBatch.Rows.Clear()
        For Each obj As clsProcessBatchOrderMainDetail In objBatch.ArrMainItem
            Me.gvBatch.Rows.AddNew()
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLineNo).Value = gvBatch.Rows.Count
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colShiftCode).Value = obj.shiftcode
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSectionCode).Value = obj.sectioncode
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBOMCode).Value = obj.bomcode

            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Value = obj.icode
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemDesc).Value = obj.iname
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBatchQty).Value = obj.qty
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value = clsProductionEntry.GetPrevProductionQty(objBatch.Batchcode, txtCode.Value, obj.icode, Nothing)
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPendingBatchQty).Value = obj.qty - clsCommon.myCdbl(Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value)
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value = obj.UOM

            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = txtLocation.Value
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = lblLocation.Text

            ' Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_Per).Value = clsProductionEntry.GetLastStageParameterValue(Me.txtBatchNo.Value, "FAT")
            'Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_Per).Value = clsProductionEntry.GetLastStageParameterValue(Me.txtBatchNo.Value, "SNF")

            'Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_KG).Value = Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_Per).Value * Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value / 100
            'Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_KG).Value = Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_Per).Value * Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value / 100

            'Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_Per).Value, Nothing)
            'Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value, Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFAT_Per).Value, Nothing)


            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = False
            Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colIsSerialseItem).Value = False

        Next

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBatch.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If e.Column Is gvBatch.Columns(colFINAL_PROD_Qty) Or e.Column Is gvBatch.Columns(colFAT_Per) Or e.Column Is gvBatch.Columns(colSNF_Per) Then
                        Me.gvBatch.Rows(e.RowIndex).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Me.gvBatch.Rows(e.RowIndex).Cells(colItemCode).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colUOM).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colFAT_Per).Value, Nothing)
                        Me.gvBatch.Rows(e.RowIndex).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Me.gvBatch.Rows(e.RowIndex).Cells(colItemCode).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colUOM).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value, Me.gvBatch.Rows(e.RowIndex).Cells(colSNF_Per).Value, Nothing)
                        Dim currRec As Decimal = gvBatch.CurrentRow.Cells(colReceiptQty).Value
                        If ShowoverheadCost = "1" Then
                            FillCostGridFromBOM()
                        End If
                        Dim dblBatchQty As Double = clsCommon.myCdbl(Me.gvBatch.Rows(e.RowIndex).Cells(colBatchQty).Value)
                        Dim dblProdQty As Double = clsCommon.myCdbl(Me.gvBatch.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value)
                        If dblProdQty > dblBatchQty Then
                            If clsCommon.MyMessageBoxShow("Production Qty is more than Batch Qty" + Environment.NewLine + "Do you want to continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                Me.gvBatch.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value = 0
                            End If
                        End If
                        If e.Column Is gvBatch.Columns(colFINAL_PROD_Qty) Then
                            OpenBatchItem()
                        End If
                    ElseIf e.Column Is gvBatch.Columns(colSP_Loaction_Code) Then
                        Dim WhrCls As String = ""
                        WhrCls += "(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "') or Location_Code='" & txtLocation.Value & "')"
                        gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value = clsLocation.getFinder(WhrCls, gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value, False)
                        gvBatch.CurrentRow.Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value, Nothing)
                    End If
                    isCellValueChanged = False
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvBatch.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        ElseIf e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub

    Sub FillIssueAgainstBatchOrder()
        Me.gvIssue.Rows.Clear()
        Dim dt As DataTable = clsProductionEntry.GetIssueAgainstBatch(Me.txtBatchNo.Value, Me.txtCode.Value)
        Dim totalIssued As Decimal = 0

        isInsideLoadData = True
        For Each dr As DataRow In dt.Rows
            If clsCommon.myCdbl(dr.Item("Issue_Qty")) > 0 Then
                Me.gvIssue.Rows.AddNew()
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSno).Value = gvIssue.Rows.Count
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Code).Value = clsCommon.myCstr(dr.Item("Issue_Code"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colFrom_Loaction_Code).Value = clsCommon.myCstr(dr.Item("From_Loaction_Code"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colTo_Location_Code).Value = clsCommon.myCstr(dr.Item("To_Location_Code"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = dr.Item("Item_Code")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = dr.Item("Item_Desc")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = clsItemMaster.ItemType(dr.Item("Item_Type"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = clsItemMaster.ProductType(dr.Item("Product_Type"))
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
            End If
        Next
        isInsideLoadData = False
    End Sub

    Sub InitialLoadAllGrid(ByVal objBatch As clsProcessBatchOrder)
        Try
            If clsCommon.myLen(txtBatchNo.Value) <= 0 Then
                txtBatchNo.Focus()
                Throw New Exception("Select Main Batch order first.")
            End If

            FillIssueAgainstBatchOrder()
            FillStageDetail()
            ShowBatchItems(objBatch)
            FillQCGrid()
            LoadBlankWreckageGrid()
            FillSection()
            LoadBlankScrapGrid()
            If ShowoverheadCost = "1" Then
                FillCostGridFromBOM()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillStageDetail()
        Me.gvStage.Rows.Clear()
       
        Dim TotalRec As Double = 0
        Dim Unit_Code As String = ""
        Dim Unit_Desc As String = ""
        For Each dr As GridViewRowInfo In gvIssue.Rows
            If clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(dr.Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                TotalRec = TotalRec + dr.Cells(colAvail_Qty).Value

                Unit_Code = dr.Cells(colIssueUom).Value
                Unit_Desc = dr.Cells(colIssueUOMDesc).Value
            End If

        Next
        Dim obj As ClsSectionStageMapping
        obj = clsProcessProductionStageProcess.FillStageDetail(Me.txtBatchNo.Value)
        If obj Is Nothing Then
            Exit Sub
        End If
        isInsideLoadData = True
        gvStage.Tag = obj.doc_code
        For Each objStage As clsSectionStageMappingDetail In obj.Arr
            If clsCommon.CompairString(objStage.Stage_Type, "PE") = CompairStringResult.Equal Then
                Me.gvStage.Rows.AddNew()

                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colARSno).Value = objStage.sequnceno
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
                gvStage.Rows(gvStage.Rows.Count - 1).Cells(colStageBatch_Code).Value = txtBatchNo.Value
            End If
        Next
        isInsideLoadData = False
    End Sub

    Sub FillQCGrid(Optional ByVal Grd_type As String = "", Optional ByVal IsAddRemoveItem As Boolean = False, Optional ByVal Item_Code As String = "")
        'Dim dt As DataTable = clsProcessProductionStandardization.GetQCParameters(Me.txtBatchNo.Value)
        'gvParameter.Rows.Clear()

        Dim dt As New DataTable
        If IsAddRemoveItem = False Then
            dt = clsProcessProductionStandardization.GetQCParameters(Me.txtBatchNo.Value)
            gvParameter.Rows.Clear()
        Else
            dt = clsProcessProductionStandardization.GetQCParametersForItem(Item_Code)
        End If


        isInsideLoadData = True
        For Each dr As DataRow In dt.Rows
            gvParameter.Rows.AddNew()
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCSno).Value = CInt(dr("sno"))
            If Not IsAddRemoveItem Then
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Batch Order"
            Else
                If clsCommon.myCstr(Grd_type) = "Wreckage" Then
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Wreckage"
                Else
                    gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCType).Value = "Scrap"
                End If

            End If
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCItemName).Value = clsCommon.myCstr(dr("Item_Desc"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparamcode).Value = clsCommon.myCstr(dr("Code"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_desc).Value = clsCommon.myCstr(dr("parameterdesc"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_type).Value = clsCommon.myCstr(dr("Type"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Value = clsCommon.myCstr(dr("Nature"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag = clsCommon.myCstr(dr("Nature_Code"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCrange1).Value = clsCommon.myCdbl(dr("Actual_Range"))
            'gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCrange2).Value = clsCommon.myCdbl(dr("Upper_range"))

            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCBooleanStatus).Value = clsCommon.myCstr(dr("Actual_Status"))
            gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCAlphaValue).Value = clsCommon.myCstr(dr("Actual_Value"))
            'gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQc_Status).Value = clsCommon.myCstr(dr("QC_Status"))
            If clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "R") = CompairStringResult.Equal Then
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = False
                'gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).Style.Font = Color.Green

                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).Value = clsCommon.myCdbl(dr("Actual_Range"))
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
            ElseIf clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "A") = CompairStringResult.Equal Then
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = True
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = False

                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).Value = clsCommon.myCstr(dr("Actual_Value"))
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
            ElseIf clsCommon.CompairString(gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQCparam_nature).Tag, "B") = CompairStringResult.Equal Then
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Range).ReadOnly = True
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).ReadOnly = False
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Value).ReadOnly = True

                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colActual_Status).Value = clsCommon.myCstr(dr("Actual_Status"))
                gvParameter.Rows(gvParameter.Rows.Count - 1).Cells(colQc_Status).Value = "Ok"
            End If
        Next

        isInsideLoadData = False
    End Sub

    Private Sub gvStage_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvStage.CellValueChanged
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
                                    For Each obj As clsPPPELogSheetDetail In grow.Tag
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
                                    For Each obj As clsPPPELogSheetDetail In grow.Tag
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

    Private Sub gvStage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvStage.KeyDown
        If Not gvStage.CurrentRow Is Nothing AndAlso e.KeyCode = (Keys.F4) Then
            Dim frm As New frmPPStageProcessQCLogSheet
            frm.Stage_Code = gvStage.CurrentRow.Cells(colStage_Code).Value
            frm.Stage_Desc = gvStage.CurrentRow.Cells(colStage_Desc).Value
            frm.txtstagecode.Value = gvStage.CurrentRow.Cells(colStage_Code).Value

            frm.txtstagename.Text = gvStage.CurrentRow.Cells(colStage_Desc).Value
            frm.Stage_Desc = gvStage.CurrentRow.Cells(colStage_Desc).Value

            frm.PRODUCTION_ENTRY_CODE = Me.txtCode.Value
            frm.txtcategorycode.Value = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.ProductionCategoryCode = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.ProductionCategoryDesc = gvStage.CurrentRow.Cells(colSPProdCategory).Value
            frm.txtCode.Value = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
            frm.Log_Sheet_No = gvStage.CurrentRow.Cells(colLog_Sheet_No).Value
            frm.Sequence = gvStage.CurrentRow.Cells(colARSno).Value
            frm.txtsequnce.Text = gvStage.CurrentRow.Cells(colARSno).Value
            frm.objListPE = gvStage.CurrentRow.Tag
            frm.Stage_Type = gvStage.CurrentRow.Cells(colStage_Code).Tag
            frm.Batch_Code = txtBatchNo.Value
            frm.arrXtime = gvStage.CurrentRow.Cells(0).Tag
            If clsCommon.myLen(gvStage.Tag) <= 0 Then
                clsCommon.MyMessageBoxShow("Permission denied.")
                Exit Sub
            End If
            frm.objListUsers = clsSectionStageMapping_User.GetLogsheetUsers(gvStage.Tag, frm.Stage_Code, Nothing)
            frm.ShowDialog()
            If frm.IsCancelled = False Then
                gvStage.CurrentRow.Tag = frm.objListPE
                gvStage.CurrentRow.Cells(0).Tag = frm.arrXtime
            End If

        End If
    End Sub

    Private Sub gvParameter_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvParameter.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gvParameter.Columns(colActual_Value) Then
                    isCellValueChanged = True
                    Dim Values As String = gvParameter.CurrentRow.Cells(colQCAlphaValue).Value
                    If clsCommon.myLen(Values) <= 0 Then
                        Exit Sub
                    End If
                    Dim val() As String = Values.Split(",")                   
                    Dim lst As New List(Of String)
                    'Values = ""
                    For Each Str As String In val
                        lst.Add(Str)
                    Next
                    Values = clsCommon.GetMulcallString(lst)
                    gvParameter.CurrentRow.Cells(colActual_Value).Value = clsProcessProductionStandardization.GetFinderParameterValueList("Value in (" & Values & ")", gvParameter.CurrentRow.Cells(colActual_Value).Value, False) 'clsItemMaster.getFinder("", gvARDetail.CurrentRow.Cells(colARItemCode).Value, False)

                    isCellValueChanged = False
                ElseIf e.Column Is gvParameter.Columns(colActual_Range) Then
                    isCellValueChanged = True
                    UpdateBatchFatSNF(clsCommon.myCstr(gvParameter.CurrentRow.Cells(colQCItemCode).Value), clsCommon.myCstr(gvParameter.CurrentRow.Cells(colActual_Range).Value), clsCommon.myCstr(gvParameter.CurrentRow.Cells(colQCparam_type).Value), clsCommon.myCstr(gvParameter.CurrentRow.Cells(colQCType).Value))
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub
   
    Sub UpdateBatchFatSNF(ByVal Item_Code As String, ByVal Value As Decimal, ByVal Type As String, ByVal QC_Type As String)
        If clsCommon.CompairString(QC_Type, "Batch Order") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvBatch.Rows
                If clsCommon.CompairString(grow.Cells(colItemCode).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colFAT_Per).Value = Value
                        grow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colUOM).Value, grow.Cells(colFINAL_PROD_Qty).Value, grow.Cells(colFAT_Per).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colSNF_Per).Value = Value
                        grow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colUOM).Value, grow.Cells(colFINAL_PROD_Qty).Value, grow.Cells(colSNF_Per).Value, Nothing)
                    End If
                    Exit Sub
                End If
            Next
        End If

        If clsCommon.CompairString(QC_Type, "Wreckage") = CompairStringResult.Equal Then
            '' update fat/snf in add/remove tab
            For Each grow As GridViewRowInfo In gvWreckage.Rows
                If clsCommon.CompairString(grow.Cells(colWFItem_Code).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colWFAvail_FAT_Per).Value = Value
                        grow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_FAT_Per).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colWFAvail_SNF_Per).Value = Value
                        grow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCdbl(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCdbl(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_SNF_Per).Value, Nothing)
                    End If
                    'Exit Sub
                End If
            Next
        End If ''end cond.

        If clsCommon.CompairString(QC_Type, "Scrap") = CompairStringResult.Equal Then
            '' update fat/snf in add/remove tab
            For Each grow As GridViewRowInfo In GvScrap.Rows
                If clsCommon.CompairString(grow.Cells(colScrapItem_Code).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colScrapAvail_FAT_Per).Value = Value
                        grow.Cells(colScrapAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colScrapUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colScrapQty).Value) > 0, clsCommon.myCdbl(grow.Cells(colScrapQty).Value), clsCommon.myCdbl(grow.Cells(colScrapQty).Value)), grow.Cells(colScrapAvail_FAT_Per).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colScrapAvail_SNF_Per).Value = Value
                        grow.Cells(colScrapAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colScrapUnit_Code).Value, IIf(clsCommon.myCdbl(grow.Cells(colScrapQty).Value) > 0, clsCommon.myCdbl(grow.Cells(colScrapQty).Value), clsCommon.myCdbl(grow.Cells(colScrapQty).Value)), grow.Cells(colScrapAvail_SNF_Per).Value, Nothing)
                    End If
                    'Exit Sub
                End If
            Next
        End If ''end cond.
        
    End Sub

    Private Sub LoadBlankWreckageGrid()
        gvWreckage.Rows.Clear()
        gvWreckage.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colWFSNO
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(reposno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colWFItem_Code
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = False
        gvWreckage.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colWFItemName
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repoiname)

        'Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoitype.FormatString = ""
        'repoitype.Name = colIssueItemType
        'repoitype.Width = 100
        'repoitype.HeaderText = "Item Type"
        'repoitype.ReadOnly = True
        'gvWreckage.MasterTemplate.Columns.Add(repoitype)

        'Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoPtype.FormatString = ""
        'repoPtype.Name = colIssueItemProductType
        'repoPtype.Width = 100
        'repoPtype.HeaderText = "Product Type"
        'repoPtype.ReadOnly = True
        'gvWreckage.MasterTemplate.Columns.Add(repoPtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colWFUnit_Code
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        gvWreckage.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colWFUnit_Desc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repouomname)

        Dim repoAvail_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_Qty.FormatString = ""
        repoAvail_Qty.Name = colWFBACK_QTY
        repoAvail_Qty.Width = 120
        repoAvail_Qty.HeaderText = "Back Quantity"
        repoAvail_Qty.DecimalPlaces = DecimalPointQty
        repoAvail_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        gvWreckage.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoWr_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWr_Qty.FormatString = ""
        repoWr_Qty.Name = colWFWRECKAGE_QTY
        repoWr_Qty.Width = 120
        repoWr_Qty.HeaderText = "Wreckage Quantity"
        repoWr_Qty.DecimalPlaces = DecimalPointQty
        repoWr_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        gvWreckage.MasterTemplate.Columns.Add(repoWr_Qty)


        Dim repoFromLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocationCode.FormatString = ""
        repoFromLocationCode.Name = colWFLocation_Code
        repoFromLocationCode.Width = 100
        repoFromLocationCode.HeaderText = "Back To Location"
        repoFromLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFromLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLocationCode.ReadOnly = False
        gvWreckage.MasterTemplate.Columns.Add(repoFromLocationCode)


        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colWFAvail_FAT_Per
        repoAvail_FAT_Per.Width = 100
        repoAvail_FAT_Per.HeaderText = "FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colWFAvail_FAT_KG
        repoAvail_FAT_KG.Width = 100
        repoAvail_FAT_KG.HeaderText = "FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colWFAvail_SNF_Per
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colWFAvail_SNF_KG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        gvWreckage.MasterTemplate.Columns.Add(repoAvail_SNF_KG)


        'Dim Issue_Status As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        'Issue_Status.FormatString = ""
        'Issue_Status.Name = colIssue_Status
        'Issue_Status.Width = 100
        'Issue_Status.HeaderText = "Status"
        'Issue_Status.ReadOnly = False
        'Issue_Status.DataSource = GetIssueStatus()
        'Issue_Status.ValueMember = "Code"
        'Issue_Status.DisplayMember = "Name"
        'gvWreckage.MasterTemplate.Columns.Add(Issue_Status)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colWFRemarks
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        gvWreckage.MasterTemplate.Columns.Add(repoIssueRemarks)

        '-------------------------------------------------

        gvWreckage.AllowDeleteRow = True
        gvWreckage.AllowAddNewRow = False
        gvWreckage.ShowGroupPanel = False
        gvWreckage.AllowColumnReorder = False
        gvWreckage.AllowRowReorder = False
        gvWreckage.EnableSorting = False
        gvWreckage.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvWreckage.MasterTemplate.ShowRowHeaderColumn = False
        gvWreckage.EnableFiltering = False
        gvWreckage.Rows.AddNew()
    End Sub

    Private Sub LoadBlankScrapGrid()
        GvScrap.Rows.Clear()
        GvScrap.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colScrapSNO
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(reposno)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colScrapItem_Code
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = False
        GvScrap.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colScrapItemName
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repoiname)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colScrapUnit_Code
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        GvScrap.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colScrapUnit_Desc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repouomname)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.Name = colScrapQty
        repoQty.Width = 120
        repoQty.HeaderText = "Quantity"
        repoQty.DecimalPlaces = DecimalPointQty
        repoQty.IsVisible = True
        repoQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        GvScrap.MasterTemplate.Columns.Add(repoQty)

        Dim repoFromLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocationCode.FormatString = ""
        repoFromLocationCode.Name = colScrapLocation_Code
        repoFromLocationCode.Width = 100
        repoFromLocationCode.HeaderText = "Location"
        repoFromLocationCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFromLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLocationCode.ReadOnly = False
        GvScrap.MasterTemplate.Columns.Add(repoFromLocationCode)

        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colScrapAvail_FAT_Per
        repoAvail_FAT_Per.Width = 100
        repoAvail_FAT_Per.HeaderText = "FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colScrapAvail_FAT_KG
        repoAvail_FAT_KG.Width = 100
        repoAvail_FAT_KG.HeaderText = "FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colScrapAvail_SNF_Per
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colScrapAvail_SNF_KG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        GvScrap.MasterTemplate.Columns.Add(repoAvail_SNF_KG)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colScrapRemarks
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        GvScrap.MasterTemplate.Columns.Add(repoIssueRemarks)

        '-------------------------------------------------

        GvScrap.AllowDeleteRow = True
        GvScrap.AllowAddNewRow = False

        GvScrap.AllowColumnReorder = False
        GvScrap.AllowRowReorder = False
        GvScrap.EnableSorting = False
        GvScrap.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvScrap.MasterTemplate.ShowRowHeaderColumn = False
        GvScrap.EnableFiltering = False
        GvScrap.Rows.AddNew()
    End Sub

    Private Sub gvWreckage_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvWreckage.CellValueChanged
        If Not isInsideLoadData Then
           
            If Not isCellValueChanged Then
                If clsCommon.myLen(txtBatchNo.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select Batch Order Detail", Me.Text)
                    Return
                End If
                '==============main----------------------
                'If e.Column Is gvWreckage.Columns(colWFItem_Code) Then
                '    isCellValueChanged = True
                '    gvWreckage.CurrentRow.Cells(colWFItem_Code).Value = clsProductionEntry.getSectionStockItemFinder("", lblConsmSectionLocCode.Text, gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, False)
                '    Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, NavigatorType.Current)
                '    If Not objItem Is Nothing Then
                '        gvWreckage.CurrentRow.Cells(colWFItemName).Value = objItem.Item_Desc
                '        'gvWreckage.CurrentRow.Cells(colwf).Value = objItem.Item_Type
                '        'gvARDetail.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                '        gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value = objItem.Unit_Code
                '        gvWreckage.CurrentRow.Cells(colWFUnit_Desc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
                '        If clsCommon.CompairString(objItem.Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(objItem.Product_Type, "MP") = CompairStringResult.Equal Then
                '            FillQCGrid(True, gvWreckage.CurrentRow.Cells(colWFItem_Code).Value)
                '        End If
                '    End If
                '    isCellValueChanged = False
                'End If
                '===============================================
                ''===============changes by shivani against ticket [BM00000007310] =======================================================================>

                If e.Column Is gvWreckage.Columns(colWFItem_Code) Then
                    isCellValueChanged = True
                    Dim arrList As New ArrayList
                    Dim arr1 As New ArrayList
                    Dim intRow As Integer = gvWreckage.CurrentRow.Index
                    arrList.Add(clsCommon.myCstr(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value))

                    arr1 = clsProductionEntry.getSectionStockItemMultipleFinder("", lblConsmSectionLocCode.Text, arrList)

                    If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                        For ii As Integer = 0 To arr1.Count - 1
                            If ii = 0 Then
                                gvWreckage.Rows(intRow).Cells(colWFItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                gvWreckage.Rows(intRow).Cells(colWFItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Desc from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                gvWreckage.Rows(intRow).Cells(colWFUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Unit_Code from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                gvWreckage.Rows(intRow).Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(gvWreckage.Rows(intRow).Cells(colWFUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    FillQCGrid("Wreckage", True, gvWreckage.Rows(intRow).Cells(colWFItem_Code).Value)
                                End If
                            Else
                                gvWreckage.Rows.AddNew()

                                gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFSNO).Value = clsCommon.myCdbl(gvWreckage.Rows(intRow).Cells(colWFSNO).Value)
                                gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing)
                                gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing)
                                gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    FillQCGrid("Wreckage", True, gvWreckage.Rows(gvWreckage.Rows.Count - 1).Cells(colWFItem_Code).Value)
                                End If

                                gvWreckage.Rows.Move(gvWreckage.Rows.Count - 1, intRow)
                            End If ''ii cond

                        Next
                        gvWreckage.CurrentRow = gvWreckage.Rows(intRow)
                        gvWreckage.CurrentColumn = gvWreckage.Columns(colWFItem_Code)
                        If clsCommon.myLen(gvWreckage.Columns(colWFItem_Code)) > 0 Then
                            For jj As Integer = 0 To gvWreckage.Rows.Count - 1
                                gvWreckage.Rows(jj).Cells(colWFSNO).Value = clsCommon.myCdbl(jj) + 1
                            Next
                        End If
                    Else
                        gvWreckage.CurrentRow.Cells(colWFItem_Code).Value = ""
                        gvWreckage.CurrentRow.Cells(colWFItemName).Value = ""
                    End If

                    isCellValueChanged = False
                End If
                '===========================================================================================================================================
                If e.Column Is gvWreckage.Columns(colWFLocation_Code) Then
                    isCellValueChanged = True
                    gvWreckage.CurrentRow.Cells(colWFLocation_Code).Value = clsLocation.getFinder("(((Is_Section='Y' or Is_Sub_Location='Y')) and Main_Location_Code='" & txtLocation.Value & "') OR Location_Code='" & txtLocation.Value & "'", gvWreckage.CurrentRow.Cells(colWFLocation_Code).Value, False)
                    'gvWreckage.CurrentRow.Cells(colwf).Value = clsLocation.GetName(gvARDetail.CurrentRow.Cells(colLoaction_Code).Value, Nothing)
                    'If clsCommon.CompairString(gvARDetail.CurrentRow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Then
                    '    gvARDetail.CurrentRow.Cells(colARAvailQty).Value = ClsLoadingTanker.getBalance(gvARDetail.CurrentRow.Cells(colARItemCode).Value, Me.txtLocation.Text, gvARDetail.CurrentRow.Cells(colLoaction_Code).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                    'Else
                    '    gvARDetail.CurrentRow.Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colLoaction_Code).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                    'End If

                    isCellValueChanged = False
                End If
                If e.Column Is gvWreckage.Columns(colWFUnit_Code) Then
                    isCellValueChanged = True
                    OpenWFUnitCode(True)
                    isCellValueChanged = False
                End If
                If e.Column Is gvWreckage.Columns(colWFBACK_QTY) Then
                    isCellValueChanged = True
                    gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).ReadOnly = True
                    gvWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = False
                    If gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        'gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        'gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    ElseIf gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        'gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        'gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    End If

                    isCellValueChanged = False
                End If
                If e.Column Is gvWreckage.Columns(colWFWRECKAGE_QTY) Then
                    isCellValueChanged = True
                    gvWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                    gvWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                    If gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                        'gvARDetail.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                        'gvARDetail.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARUom).Value, gvARDetail.CurrentRow.Cells(colARQty).Value, gvARDetail.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                        ''gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        'gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        'gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value, gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value, gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, Nothing)
                        'gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value, gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value, gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, Nothing)
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    ElseIf gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                        'gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                        'gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value, "FAT", "Wreckage")
                        UpdateBatchFatSNF(gvWreckage.CurrentRow.Cells(colWFItem_Code).Value, gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value, "SNF", "Wreckage")
                    End If
                    isCellValueChanged = False
                End If

                'If e.Column Is gvWreckage.Columns(colWFAvail_FAT_Per) Then
                '    isCellValueChanged = True
                '    gvWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                '    gvWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                '    If gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                '        gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                '        gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                '    ElseIf gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                '        gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                '        gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                '    End If
                '    isCellValueChanged = False
                'End If

                'If e.Column Is gvWreckage.Columns(colWFAvail_SNF_Per) Then
                '    isCellValueChanged = True
                '    gvWreckage.CurrentRow.Cells(colWFBACK_QTY).ReadOnly = True
                '    gvWreckage.CurrentRow.Cells(colWFLocation_Code).ReadOnly = True

                '    If gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value > 0 Then
                '        gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                '        gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFWRECKAGE_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                '    ElseIf gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value > 0 Then
                '        gvWreckage.CurrentRow.Cells(colWFAvail_FAT_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_FAT_Per).Value / 100
                '        gvWreckage.CurrentRow.Cells(colWFAvail_SNF_KG).Value = gvWreckage.CurrentRow.Cells(colWFBACK_QTY).Value * gvWreckage.CurrentRow.Cells(colWFAvail_SNF_Per).Value / 100
                '    End If
                '    isCellValueChanged = False
                'End If
            End If
        End If
    End Sub

    Private Sub GvScrap_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GvScrap.CellValueChanged
        If Not isInsideLoadData Then

            If Not isCellValueChanged Then
                If clsCommon.myLen(txtBatchNo.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select Batch Order Detail", Me.Text)
                    Return
                End If

                If e.Column Is GvScrap.Columns(colScrapItem_Code) Then
                    isCellValueChanged = True
                    Dim arrList As New ArrayList
                    Dim arr1 As New ArrayList
                    Dim intRow As Integer = GvScrap.CurrentRow.Index
                    arrList.Add(clsCommon.myCstr(GvScrap.CurrentRow.Cells(colScrapItem_Code).Value))
                    'arr1 = clsProductionEntry.getSectionStockItemMultipleFinder("", lblConsmSectionLocCode.Text, arrList, "Scrap")
                    arr1 = clsProductionEntry.getItemFinder("", "", arrList, "Scrap")
                    If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                        For ii As Integer = 0 To arr1.Count - 1
                            If ii = 0 Then
                                GvScrap.Rows(intRow).Cells(colScrapItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                GvScrap.Rows(intRow).Cells(colScrapItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Item_Desc from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                GvScrap.Rows(intRow).Cells(colScrapUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.Unit_Code from tspl_item_master  left outer join TSPL_WARRANTY_MASTER on TSPL_WARRANTY_MASTER.Code=tspl_item_master.Warranty_Code where  tspl_item_master.item_code = '" + clsCommon.myCstr(arr1(ii)) + "'"))
                                GvScrap.Rows(intRow).Cells(colScrapUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(GvScrap.Rows(intRow).Cells(colScrapUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    FillQCGrid("Scrap", True, GvScrap.Rows(intRow).Cells(colScrapItem_Code).Value)
                                End If
                            Else
                                GvScrap.Rows.AddNew()

                                GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapSNO).Value = clsCommon.myCdbl(GvScrap.Rows(intRow).Cells(colScrapSNO).Value)
                                GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapItem_Code).Value = clsCommon.myCstr(arr1(ii))
                                GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(arr1(ii)), Nothing)
                                GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapUnit_Code).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(arr1(ii)), Nothing)
                                GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapUnit_Code).Value))
                                If clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MI") = CompairStringResult.Equal Or clsCommon.CompairString(clsItemMaster.GetItemProductType(clsCommon.myCstr(arr1(ii)), Nothing), "MP") = CompairStringResult.Equal Then
                                    FillQCGrid("Scrap", True, GvScrap.Rows(GvScrap.Rows.Count - 1).Cells(colScrapItem_Code).Value)
                                End If

                                GvScrap.Rows.Move(GvScrap.Rows.Count - 1, intRow)
                            End If ''ii cond

                        Next
                        GvScrap.CurrentRow = GvScrap.Rows(intRow)
                        GvScrap.CurrentColumn = GvScrap.Columns(colScrapItem_Code)
                        If clsCommon.myLen(GvScrap.Columns(colScrapItem_Code)) > 0 Then
                            For jj As Integer = 0 To GvScrap.Rows.Count - 1
                                GvScrap.Rows(jj).Cells(colScrapSNO).Value = clsCommon.myCdbl(jj) + 1
                            Next
                        End If
                    Else
                        GvScrap.CurrentRow.Cells(colScrapItem_Code).Value = ""
                        GvScrap.CurrentRow.Cells(colScrapItemName).Value = ""
                    End If

                    isCellValueChanged = False
                End If
                '===========================================================================================================================================
                If e.Column Is GvScrap.Columns(colScrapLocation_Code) Then
                    isCellValueChanged = True
                    GvScrap.CurrentRow.Cells(colScrapLocation_Code).Value = clsLocation.getFinder(" (((Is_Section='Y' or Is_Sub_Location='Y')) and Main_Location_Code='" & txtLocation.Value & "') OR Location_Code='" & txtLocation.Value & "'", GvScrap.CurrentRow.Cells(colScrapLocation_Code).Value, False)

                    isCellValueChanged = False
                End If
                '=================Added by preeti Gupta=============
                If e.Column Is GvScrap.Columns(colScrapUnit_Code) Then
                    isCellValueChanged = True
                    OpenScrapUnitCode(True)
                    isCellValueChanged = False
                End If
                '=======================================================
                If e.Column Is GvScrap.Columns(colScrapQty) Then
                    isCellValueChanged = True
                    'GvScrap.CurrentRow.Cells(colScrapQty).ReadOnly = True

                    If GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then

                        UpdateBatchFatSNF(GvScrap.CurrentRow.Cells(colScrapItem_Code).Value, GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value, "FAT", "Scrap")
                        UpdateBatchFatSNF(GvScrap.CurrentRow.Cells(colScrapItem_Code).Value, GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value, "SNF", "Scrap")
                    ElseIf GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then

                        UpdateBatchFatSNF(GvScrap.CurrentRow.Cells(colScrapItem_Code).Value, GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value, "FAT", "Scrap")
                        UpdateBatchFatSNF(GvScrap.CurrentRow.Cells(colScrapItem_Code).Value, GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value, "SNF", "Scrap")
                    End If
                    isCellValueChanged = False
                End If
                'If e.Column Is GvScrap.Columns(colScrapAvail_FAT_Per) Then
                '    isCellValueChanged = True
                '    GvScrap.CurrentRow.Cells(colScrapQty).ReadOnly = True

                '    If GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then
                '        GvScrap.CurrentRow.Cells(colScrapAvail_FAT_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value / 100
                '        GvScrap.CurrentRow.Cells(colScrapAvail_SNF_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value / 100
                '    ElseIf GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then
                '        GvScrap.CurrentRow.Cells(colScrapAvail_FAT_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value / 100
                '        GvScrap.CurrentRow.Cells(colScrapAvail_SNF_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value / 100
                '    End If
                '    isCellValueChanged = False
                'End If

                'If e.Column Is GvScrap.Columns(colScrapAvail_SNF_Per) Then
                '    isCellValueChanged = True
                '    GvScrap.CurrentRow.Cells(colScrapQty).ReadOnly = True

                '    If GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then
                '        GvScrap.CurrentRow.Cells(colScrapAvail_FAT_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value / 100
                '        GvScrap.CurrentRow.Cells(colScrapAvail_SNF_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value / 100
                '    ElseIf GvScrap.CurrentRow.Cells(colScrapQty).Value > 0 Then
                '        GvScrap.CurrentRow.Cells(colScrapAvail_FAT_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_FAT_Per).Value / 100
                '        GvScrap.CurrentRow.Cells(colScrapAvail_SNF_KG).Value = GvScrap.CurrentRow.Cells(colScrapQty).Value * GvScrap.CurrentRow.Cells(colScrapAvail_SNF_Per).Value / 100
                '    End If
                '    isCellValueChanged = False
                'End If

            End If
        End If
    End Sub

    Private Sub GvScrap_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles GvScrap.CurrentColumnChanged
        If GvScrap.RowCount > 0 Then
            Dim intCurrRow As Integer = GvScrap.CurrentRow.Index
            GvScrap.CurrentRow.Cells(colScrapSNO).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = GvScrap.Rows.Count - 1 Then
                GvScrap.Rows.AddNew()
                GvScrap.CurrentRow = GvScrap.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvWreckage_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvWreckage.CurrentColumnChanged
        If gvWreckage.RowCount > 0 Then
            Dim intCurrRow As Integer = gvWreckage.CurrentRow.Index
            gvWreckage.CurrentRow.Cells(colWFSNO).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvWreckage.Rows.Count - 1 Then
                gvWreckage.Rows.AddNew()
                gvWreckage.CurrentRow = gvWreckage.Rows(intCurrRow)
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
                If clsProductionEntry.UnpostData(txtCode.Value, Me.Form_ID, True) Then
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

    Sub OpenScrapUnitCode(ByVal isButtonClick As Boolean)
        Dim WhrCls As String = " tspl_item_master.Item_Code='" & GvScrap.CurrentRow.Cells(colScrapItem_Code).Value & "'  "
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_ITEM_UOM_DETAIL.UOM_Description as [UOM Description] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=tspl_item_uom_detail.item_code  "
        GvScrap.CurrentRow.Cells(colScrapUnit_Code).Value = clsCommon.ShowSelectForm("TSPL_UOM_HEAD", qry, "Code", WhrCls, GvScrap.CurrentRow.Cells(colScrapUnit_Code).Value, "", False)
        GvScrap.CurrentRow.Cells(colScrapUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(GvScrap.CurrentRow.Cells(colScrapUnit_Code).Value))
    End Sub

    Sub OpenWFUnitCode(ByVal isButtonClick As Boolean)

        Dim WhrCls As String = " tspl_item_master.Item_Code='" & gvWreckage.CurrentRow.Cells(colWFItem_Code).Value & "'  "
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_ITEM_UOM_DETAIL.UOM_Description as [UOM Description] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=tspl_item_uom_detail.item_code  "
        gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value = clsCommon.ShowSelectForm("TSPL_UOM_HEAD", qry, "Code", WhrCls, gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value, "", False)
        gvWreckage.CurrentRow.Cells(colWFUnit_Desc).Value = clsItemUOMDetails.GetName(clsCommon.myCstr(gvWreckage.CurrentRow.Cells(colWFUnit_Code).Value))
    End Sub

    Sub LoadBlankGridCost()
        gvProductionCost.Rows.Clear()
        gvProductionCost.Columns.Clear()
        ''SNO,HCODE,OverHead_Cost
        Dim LineNo As New GridViewTextBoxColumn

        Dim MainItemCode As New GridViewTextBoxColumn
        Dim MainItemDesc As New GridViewTextBoxColumn

        Dim BOMCode As New GridViewTextBoxColumn
        Dim BOMDesc As New GridViewTextBoxColumn

        Dim MainItemUOM As New GridViewTextBoxColumn
        Dim MainItemUOMDesc As New GridViewTextBoxColumn

        Dim HCODE As New GridViewTextBoxColumn
        Dim HCODEDesc As New GridViewTextBoxColumn

        Dim OverHead_Cost As New GridViewDecimalColumn


        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 50
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProductionCost.Columns.Add(LineNo)

        MainItemCode.FormatString = ""
        MainItemCode.HeaderText = "Main Item Code"
        MainItemCode.Name = colMainItemCode
        MainItemCode.Width = 100
        MainItemCode.ReadOnly = True
        MainItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(MainItemCode)

        MainItemDesc.FormatString = ""
        MainItemDesc.HeaderText = "Main Item Desc"
        MainItemDesc.Name = colMainItemDesc
        MainItemDesc.Width = 100
        MainItemDesc.ReadOnly = True
        MainItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(MainItemDesc)

        MainItemUOM.FormatString = ""
        MainItemUOM.HeaderText = "Main UOM"
        MainItemUOM.Name = colMainUOM
        MainItemUOM.Width = 100
        MainItemUOM.ReadOnly = True
        MainItemUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(MainItemUOM)

        MainItemUOMDesc.FormatString = ""
        MainItemUOMDesc.HeaderText = "Main UOM Desc"
        MainItemUOMDesc.Name = colMainUOMDesc
        MainItemUOMDesc.Width = 100
        MainItemUOMDesc.ReadOnly = True
        MainItemUOMDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(MainItemUOMDesc)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 100
        BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(BOMCode)

        BOMDesc.FormatString = ""
        BOMDesc.HeaderText = "BOM Desc"
        BOMDesc.Name = colBOMDesc
        BOMDesc.Width = 100
        BOMDesc.ReadOnly = True
        BOMDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(BOMDesc)

        HCODE.FormatString = ""
        HCODE.HeaderText = "Cost Code"
        HCODE.Name = colItemCode
        HCODE.Width = 100
        HCODE.ReadOnly = True
        HCODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(HCODE)

        HCODEDesc.FormatString = ""
        HCODEDesc.HeaderText = "Cost Desc"
        HCODEDesc.Name = colItemDesc
        HCODEDesc.Width = 100
        HCODEDesc.ReadOnly = True
        HCODEDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(HCODEDesc)

        OverHead_Cost.FormatString = ""
        OverHead_Cost.HeaderText = "Cost"
        OverHead_Cost.Name = colAVG_Cost
        OverHead_Cost.Width = 100
        OverHead_Cost.ReadOnly = False
        OverHead_Cost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        OverHead_Cost.IsVisible = True
        OverHead_Cost.DecimalPlaces = 2
        gvProductionCost.Columns.Add(OverHead_Cost)

        gvProductionCost.EnableFiltering = False
    End Sub

    Sub FillCostGridFromBOM()
        If isInsideLoadData Then
            Exit Sub
        End If
        Dim objlist As New List(Of clsBomCostMappingDetails)
        gvProductionCost.Rows.Clear()
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colUOM).Value) > 0 Then
                objlist = New List(Of clsBomCostMappingDetails)
                objlist = clsProductionEntryWithoutBatch.getBOMCost(grow.Cells(colBOMCode).Value, grow.Cells(colUOM).Value, Nothing)
                If objlist IsNot Nothing AndAlso objlist.Count > 0 Then
                    For Each objtr As clsBomCostMappingDetails In objlist
                        gvProductionCost.Rows.AddNew()
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colLineNo).Value = gvProductionCost.Rows.Count
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemCode).Value = grow.Cells(colItemCode).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemDesc).Value = clsItemMaster.GetItemName(grow.Cells(colItemCode).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMCode).Value = grow.Cells(colBOMCode).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMDesc).Value = clsBOM.GetBOMDesc(grow.Cells(colBOMCode).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOM).Value = grow.Cells(colUOM).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(grow.Cells(colUOM).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemCode).Value = objtr.COST_CODE
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemDesc).Value = objtr.COST_DESC

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colAVG_Cost).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colFINAL_PROD_Qty).Value) * objtr.Overheadcost, 2)

                    Next
                End If
            End If
        Next

    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Receipt Code")
                Exit Sub
            End If
            clsERPFuncationality.ShowTransHistoryData(txtCode.Value, "PROD_ENTRY_CODE", "TSPL_PP_PRODUCTION_ENTRY", "TSPL_PP_PE_ISSUE_ITEM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
            clsProductionEntry.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            funReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub OpenBatchItem()
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtpDate.Value) = True Then
            Dim blnBatchqty As Boolean = False
            If clsCommon.myCBool(clsItemMaster.IsBatchItem(gvBatch.CurrentRow.Cells(colItemCode).Value)) Then
                Dim frm As frmBatchItemIn = New frmBatchItemIn()
                frm.strItemCode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)
                frm.strItemName = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemDesc).Value)
                frm.dblqty = clsCommon.myCdbl(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value)
                frm.strUOM = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value)
                frm.TransDate = dtpDate.Value
                'frm.dblMRP = clsCommon.myCdbl(gvBatch.CurrentRow.Cells(colMRP).Value)
                frm.arr = TryCast(gvBatch.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gvBatch.CurrentRow.Cells(colItemCode).Tag = frm.arr
                End If
            End If
        End If
    End Sub
End Class
