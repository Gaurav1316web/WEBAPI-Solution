
Imports common

Public Class frmStanderdProductionEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False
    Dim DecimalPointQty As Integer = 3
    Dim DecimalPointFatSNFPer As Integer = 3
    Const colLineNo As String = "LineNO"
    Const colBOMCode As String = "BOMCode"
    Const colBOMDesc As String = "colBOMDesc"
    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    Const colUOMBag As String = "UOMBag"
    Const colFINAL_PROD_Qty_Bag As String = "colFINAL_PROD_Qty_Bag"
    Const colUOM As String = "UOM"
    Const colConsumptionQtyOriginal As String = "colConsumptionQtyOriginal"
    Const colFINAL_PROD_Qty As String = "colFINAL_PROD_Qty"
    Const colBatchQty As String = "BatchQTY"
    Const colPrevProdQty As String = "colPrevProdQty"
    Const colPendingBatchQty As String = "colPendingBatchQty"
    Const colReceiptQty As String = "ReceiptQty"
    Const colRejHead As String = "RejHead"
    Const colRejQty As String = "RejQty"
    Const colBreakageHead As String = "BreakageHead"
    Const colBreakageQty As String = "BreakageQty"
    Const colLabTesting As String = "LabTesting"
    'Const colFINAL_PROD_QtyInBag As String = "colFINAL_PROD_QtyInBag"
    Const colFINAL_PROD_Qty_Stock As String = "colFINAL_PROD_Qty_Stock"
    Const colFINAL_PROD_Qty_Min As String = "colFINAL_PROD_Qty_Min"
    Const colFINAL_PROD_Qty_Max As String = "colFINAL_PROD_Qty_Max"
    Const colSP_Loaction_Code As String = "colSP_Loaction_Code"
    Const colSP_Loaction_Desc As String = "colSP_Loaction_Desc"
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


    Const colFat_Rate As String = "colFat_Rate"
    Const colSNF_Rate As String = "colSNF_Rate"
    Const colFat_Amt As String = "colFat_Amt"
    Const colSNF_Amt As String = "colSNF_Amt"
    Const colMainItemCode As String = "colMainItemCode"
    Const colMainItemDesc As String = "colMainItemDesc"
    Const colMainUOM As String = "colMainUOM"
    Const colMainUOMDesc As String = "colMainUOMDesc"
    Const colProductType As String = "colProductType"
    Dim AllBomCode As String = ""
    Dim ClickGo As Boolean = False
    '' End Scrap
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Public strDocumentNo As String = ""
    'Dim objList As New List(Of clsProductionEntryWithoutBatchDetail)
    Dim obj As New clsStanderdProductionEntry
    Public Const strCostTransaction As String = "Production Entry"
    Public MOActive As Boolean = False

    Dim arrLoc As String = Nothing
    Dim isCellValueChanged As Boolean = False
    Dim Import As Boolean = False

    Dim SettConsumptionQtyTollerance As Decimal = 0.6
    Dim SettCheckBalanceOnSave As Boolean = False
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionEntryWithoutBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isExport = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False

        End If
        btnunpost.Visible = False
        'If MyBase.isReverse Then
        '    btnunpost.Enabled = True
        'Else
        '    btnunpost.Enabled = False
        'End If
    End Sub
    Private Sub frmProductionEntryWithoutBatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' get mo setting
        'MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        '' get decimal point for qty
        DecimalPointQty = CInt(clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        If DecimalPointQty <= 0 Then
            DecimalPointQty = 3
        End If
        '' get decimal point for fat snf percentage
        DecimalPointFatSNFPer = CInt(clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ProductionFATSNFPerDecimalPoint, clsFixedParameterCode.ProductionFATSNFPerDecimalPoint, Nothing)))
        If DecimalPointFatSNFPer <= 0 Then
            DecimalPointFatSNFPer = 3
        End If

        SettConsumptionQtyTollerance = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionQtyTollerance, clsFixedParameterCode.ConsumptionQtyTollerance, Nothing))
        SettCheckBalanceOnSave = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.CheckBalanceOnSave, clsFixedParameterCode.RCDFCFProductionEntry, Nothing))

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = pageBatchProduction

        LoadBlankGrid()
        LoadBlankGridConsumption()
        LoadBlankGridCost()
        LoadShift()
        'LoadBlankIssueGrid()
        'LoadSPBlankGrid()
        'LoadQCBlankGrid()
        'LoadBlankWreckageGrid()
        'LoadBlankScrapGrid()
        'gvSectionStock.AutoGenerateColumns = True
        funReset()
        SetLength()
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "'"))
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        UcAttachment1.Form_ID = MyBase.Form_ID
        'txtReceivedBy.Visible = False
        'lblReceivedBy.Visible = False
        'lblEmpName.Visible = False
        '' disable enable import template control
        If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SPP_PRODUCTION_IMPORT where Import_Status='N'")) > 0 Then
            txtImportTemplate.Enabled = True
        Else
            txtImportTemplate.Enabled = False
        End If

        txtConsmLocOther.Value = txtLocation.Value
    End Sub
    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub
    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code as [Code] from TSPL_Location_MASTER  where Location_Type='Physical' and Location_Code in (" + objCommonVar.strCurrUserLocations + " ) ")) ' objCommonVar.arrCurrUserLocations
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_MASTER  where Location_Code in ('" + txtLocation.Value + "' ) "))
            txtConsmLocOther.Value = txtLocation.Value
            lblConsmLocOtherDesc.Text = lblLocation.Text
        Else
            txtLocation.Value = "" 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code] from TSPL_Location_MASTER  where Location_Type='Physical' and Location_Code in (" + objCommonVar.strCurrUserLocations + " ) ")) ' objCommonVar.arrCurrUserLocations
            lblLocation.Text = "" 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code] from TSPL_Location_MASTER  where Location_Code in ('" + txtLocation.Value + "' ) "))
            txtConsmLocOther.Value = "" 'txtLocation.Value
            lblConsmLocOtherDesc.Text = "" 'lblLocation.Text
        End If


        txtComment.Text = ""
        dtpDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        dtpBatchDate.Value = Nothing
        fndItemCategory.Value = Nothing
        'fndSection.Value = Nothing
        'TxtSection.Text = ""
        TxtCategory.Text = ""
        CboShift.SelectedValue = ""
        'txtReceivedBy.Value = ""
        'lblEmpName.Text = ""        
        gvBatch.Rows.Clear()
        gvConsumption.Rows.Clear()
        gvProductionCost.Rows.Clear()
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()
    End Sub
    Sub LoadShift()
        Dim qry As String = "select shift_code as Code,shift_name as Name from tspl_shift_master"
        Dim stShift As DataTable = clsDBFuncationality.GetDataTable(qry)
        CboShift.DataSource = stShift
        CboShift.ValueMember = "Code"
        CboShift.DisplayMember = "Name"
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
        Dim UOMBag As New GridViewTextBoxColumn
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
        BOMCode.Width = 100
        BOMCode.ReadOnly = False
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

        UOMBag.FormatString = ""
        UOMBag.HeaderText = "Bag Uom"
        UOMBag.Name = colUOMBag
        UOMBag.Width = 100
        UOMBag.ReadOnly = True
        UOMBag.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(UOMBag)

        Dim repoFinalProdQtyBag As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQtyBag.FormatString = ""
        repoFinalProdQtyBag.Name = colFINAL_PROD_Qty_Bag
        repoFinalProdQtyBag.Width = 150
        repoFinalProdQtyBag.HeaderText = "Production Bag Qty"
        repoFinalProdQtyBag.DecimalPlaces = DecimalPointQty
        repoFinalProdQtyBag.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQtyBag.DecimalPlaces = 3
        repoFinalProdQtyBag.ReadOnly = False
        repoFinalProdQtyBag.IsVisible = True
        gvBatch.MasterTemplate.Columns.Add(repoFinalProdQtyBag)

        UOM.FormatString = ""
        UOM.HeaderText = "LCF UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(UOM)

        Dim repoFinalProdQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQty.FormatString = ""
        repoFinalProdQty.Name = colFINAL_PROD_Qty
        repoFinalProdQty.Width = 150
        repoFinalProdQty.HeaderText = "Production LCF Qty"
        repoFinalProdQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQty.DecimalPlaces = 3
        repoFinalProdQty.ReadOnly = True
        repoFinalProdQty.IsVisible = True
        gvBatch.MasterTemplate.Columns.Add(repoFinalProdQty)

        BatchQty.FormatString = ""
        BatchQty.HeaderText = "Batch Qty"
        BatchQty.DecimalPlaces = DecimalPointQty
        BatchQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        BatchQty.Name = colBatchQty
        BatchQty.Width = 100
        BatchQty.ReadOnly = True
        BatchQty.IsVisible = False
        BatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(BatchQty)

        PrevProdQty.FormatString = ""
        PrevProdQty.HeaderText = "Prev. Production Qty"
        PrevProdQty.DecimalPlaces = DecimalPointQty
        PrevProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        PrevProdQty.Name = colPrevProdQty
        PrevProdQty.Width = 100
        PrevProdQty.ReadOnly = True
        PrevProdQty.IsVisible = False
        PrevProdQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(PrevProdQty)

        PendBatchQty.FormatString = ""
        PendBatchQty.HeaderText = "Pending Batch Qty"
        PendBatchQty.DecimalPlaces = DecimalPointQty
        PendBatchQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        PendBatchQty.Name = colPendingBatchQty
        PendBatchQty.Width = 100
        PendBatchQty.ReadOnly = True
        PendBatchQty.IsVisible = False
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

        'Dim repoFinalProdQtyInBag As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoFinalProdQtyInBag.FormatString = ""
        'repoFinalProdQtyInBag.Name = colFINAL_PROD_QtyInBag
        'repoFinalProdQtyInBag.Width = 100
        'repoFinalProdQtyInBag.HeaderText = "Production Bag Qty"
        ''repoFinalProdQtyInBag.DecimalPlaces = DecimalPointQty
        ''repoFinalProdQtyInBag.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        'repoFinalProdQtyInBag.DecimalPlaces = 0
        'repoFinalProdQtyInBag.ReadOnly = False
        'repoFinalProdQtyInBag.IsVisible = True
        'gvBatch.MasterTemplate.Columns.Add(repoFinalProdQtyInBag)



        Dim repoSP_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Code.FormatString = ""
        repoSP_Location_Code.Name = colSP_Loaction_Code
        repoSP_Location_Code.Width = 100
        repoSP_Location_Code.HeaderText = "Location Code"
        repoSP_Location_Code.ReadOnly = False
        repoSP_Location_Code.IsVisible = False
        gvBatch.MasterTemplate.Columns.Add(repoSP_Location_Code)

        Dim repoSP_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Desc.FormatString = ""
        repoSP_Location_Desc.Name = colSP_Loaction_Desc
        repoSP_Location_Desc.Width = 150
        repoSP_Location_Desc.HeaderText = "Location Description"
        repoSP_Location_Desc.ReadOnly = True
        repoSP_Location_Desc.IsVisible = False
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
        repoShiftCode.ReadOnly = False
        repoShiftCode.IsVisible = False
        repoShiftCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.MasterTemplate.Columns.Add(repoShiftCode)

        Dim repoSectionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSectionCode.HeaderText = "Section Code"
        repoSectionCode.Name = colSectionCode
        repoSectionCode.Width = 100
        repoSectionCode.ReadOnly = True
        repoSectionCode.IsVisible = False
        repoSectionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBatch.MasterTemplate.Columns.Add(repoSectionCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT %"
        repoFatPer.DecimalPlaces = DecimalPointFatSNFPer
        repoFatPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoFatPer.Name = colFAT_Per
        repoFatPer.Width = 100
        repoFatPer.ReadOnly = False
        repoFatPer.IsVisible = False
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
        repoFatKG.IsVisible = False
        repoFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoFatKG)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF %"
        repoSNFPer.DecimalPlaces = DecimalPointFatSNFPer
        repoSNFPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoSNFPer.Name = colSNF_Per
        repoSNFPer.Width = 100
        repoSNFPer.ReadOnly = False
        repoSNFPer.IsVisible = False
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
        repoSNFKG.IsVisible = False
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBatch.Columns.Add(repoSNFKG)

        gvBatch.EnableFiltering = False
    End Sub
    Sub LoadBlankGridConsumption()
        gvConsumption.Rows.Clear()
        gvConsumption.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn

        Dim ItemCode As New GridViewTextBoxColumn
        Dim ItemDesc As New GridViewTextBoxColumn
        Dim ProductType As New GridViewTextBoxColumn
        Dim UOM As New GridViewTextBoxColumn

        Dim MainItemCode As New GridViewTextBoxColumn
        Dim MainItemDesc As New GridViewTextBoxColumn

        Dim BOMCode As New GridViewTextBoxColumn
        Dim BOMDesc As New GridViewTextBoxColumn

        Dim MainItemUOM As New GridViewTextBoxColumn
        Dim MainItemUOMDesc As New GridViewTextBoxColumn

        Dim FatRate As New GridViewDecimalColumn
        Dim SNFRate As New GridViewDecimalColumn
        Dim FatAmount As New GridViewDecimalColumn
        Dim SNFAmount As New GridViewDecimalColumn

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
        gvConsumption.Columns.Add(LineNo)

        MainItemCode.FormatString = ""
        MainItemCode.HeaderText = "Main Item Code"
        MainItemCode.Name = colMainItemCode
        MainItemCode.Width = 100
        MainItemCode.ReadOnly = True
        MainItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(MainItemCode)

        MainItemDesc.FormatString = ""
        MainItemDesc.HeaderText = "Main Item Desc"
        MainItemDesc.Name = colMainItemDesc
        MainItemDesc.Width = 100
        MainItemDesc.ReadOnly = True
        MainItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(MainItemDesc)

        MainItemUOM.FormatString = ""
        MainItemUOM.HeaderText = "Main UOM"
        MainItemUOM.Name = colMainUOM
        MainItemUOM.Width = 100
        MainItemUOM.ReadOnly = True
        MainItemUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(MainItemUOM)

        MainItemUOMDesc.FormatString = ""
        MainItemUOMDesc.HeaderText = "Main UOM Desc"
        MainItemUOMDesc.Name = colMainUOMDesc
        MainItemUOMDesc.Width = 100
        MainItemUOMDesc.ReadOnly = True
        MainItemUOMDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(MainItemUOMDesc)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 100
        BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(BOMCode)

        BOMDesc.FormatString = ""
        BOMDesc.HeaderText = "BOM Desc"
        BOMDesc.Name = colBOMDesc
        BOMDesc.Width = 100
        BOMDesc.ReadOnly = True
        BOMDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(BOMDesc)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Consm. Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(ItemCode)

        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Consm. Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        ItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(ItemDesc)

        ProductType.FormatString = ""
        ProductType.HeaderText = "Product Type"
        ProductType.Name = colProductType
        ProductType.Width = 100
        ProductType.ReadOnly = True
        ProductType.IsVisible = False
        ProductType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvConsumption.Columns.Add(ProductType)

        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvConsumption.Columns.Add(UOM)

        Dim repoFinalProdQtyStock As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQtyStock.FormatString = ""
        repoFinalProdQtyStock.Name = colFINAL_PROD_Qty_Stock
        repoFinalProdQtyStock.Width = 100
        repoFinalProdQtyStock.HeaderText = "Stock Qty"
        repoFinalProdQtyStock.DecimalPlaces = DecimalPointQty
        repoFinalProdQtyStock.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQtyStock.DecimalPlaces = 3
        repoFinalProdQtyStock.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoFinalProdQtyStock)

        Dim repoFinalProdQtyMinQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQtyMinQty.FormatString = ""
        repoFinalProdQtyMinQty.Name = colFINAL_PROD_Qty_Min
        repoFinalProdQtyMinQty.Width = 100
        repoFinalProdQtyMinQty.HeaderText = "Min Qty"
        repoFinalProdQtyMinQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQtyMinQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQtyMinQty.DecimalPlaces = 3
        repoFinalProdQtyMinQty.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoFinalProdQtyMinQty)

        Dim repoFinalProdQtyMaxQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQtyMaxQty.FormatString = ""
        repoFinalProdQtyMaxQty.Name = colFINAL_PROD_Qty_Max
        repoFinalProdQtyMaxQty.Width = 100
        repoFinalProdQtyMaxQty.HeaderText = "Max Qty"
        repoFinalProdQtyMaxQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQtyMaxQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQtyMaxQty.DecimalPlaces = 3
        repoFinalProdQtyMaxQty.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoFinalProdQtyMaxQty)

        Dim repoFinalProdQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQty.FormatString = ""
        repoFinalProdQty.Name = colConsumptionQtyOriginal
        repoFinalProdQty.Width = 100
        repoFinalProdQty.HeaderText = "Consumption Original Qty"
        repoFinalProdQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQty.DecimalPlaces = 3
        repoFinalProdQty.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoFinalProdQty)

        repoFinalProdQty = New GridViewDecimalColumn()
        repoFinalProdQty.FormatString = ""
        repoFinalProdQty.Name = colFINAL_PROD_Qty
        repoFinalProdQty.Width = 100
        repoFinalProdQty.HeaderText = "Consumption Qty"
        repoFinalProdQty.DecimalPlaces = DecimalPointQty
        repoFinalProdQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFinalProdQty.DecimalPlaces = 3
        repoFinalProdQty.ReadOnly = False
        gvConsumption.MasterTemplate.Columns.Add(repoFinalProdQty)

        Dim repoSP_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Code.FormatString = ""
        repoSP_Location_Code.Name = colSP_Loaction_Code
        repoSP_Location_Code.Width = 100
        repoSP_Location_Code.HeaderText = "Location Code"
        repoSP_Location_Code.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoSP_Location_Code)

        Dim repoSP_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP_Location_Desc.FormatString = ""
        repoSP_Location_Desc.Name = colSP_Loaction_Desc
        repoSP_Location_Desc.Width = 150
        repoSP_Location_Desc.HeaderText = "Location Description"
        repoSP_Location_Desc.ReadOnly = True
        gvConsumption.MasterTemplate.Columns.Add(repoSP_Location_Desc)




        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT %"
        repoFatPer.DecimalPlaces = DecimalPointFatSNFPer
        repoFatPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoFatPer.Name = colFAT_Per
        repoFatPer.Width = 100
        repoFatPer.ReadOnly = True
        repoFatPer.IsVisible = False
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvConsumption.Columns.Add(repoFatPer)

        Dim repoFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKG.FormatString = ""
        repoFatKG.HeaderText = "FAT KG"
        repoFatKG.DecimalPlaces = DecimalPointQty
        repoFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoFatKG.Name = colFAT_KG
        repoFatKG.Width = 100
        repoFatKG.IsVisible = False
        repoFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvConsumption.Columns.Add(repoFatKG)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF %"
        repoSNFPer.DecimalPlaces = DecimalPointFatSNFPer
        repoSNFPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoSNFPer.Name = colSNF_Per
        repoSNFPer.Width = 100
        repoSNFPer.IsVisible = False
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvConsumption.Columns.Add(repoSNFPer)

        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF KG"
        repoSNFKG.DecimalPlaces = DecimalPointQty
        repoSNFKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoSNFKG.Name = colSNF_KG
        repoSNFKG.Width = 100
        repoSNFKG.ReadOnly = True
        repoSNFKG.IsVisible = False
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvConsumption.Columns.Add(repoSNFKG)

        FatRate.FormatString = ""
        FatRate.HeaderText = "FAT Rate"
        FatRate.Name = colFat_Rate
        FatRate.Width = 100
        FatRate.ReadOnly = True
        FatRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        FatRate.IsVisible = True
        FatRate.IsVisible = False
        gvConsumption.Columns.Add(FatRate)

        SNFRate.FormatString = ""
        SNFRate.HeaderText = "SNF Rate"
        SNFRate.Name = colSNF_Rate
        SNFRate.Width = 100
        SNFRate.ReadOnly = True
        SNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        SNFRate.IsVisible = True
        SNFRate.IsVisible = False
        gvConsumption.Columns.Add(SNFRate)

        FatAmount.FormatString = ""
        FatAmount.HeaderText = "Fat Amount"
        FatAmount.Name = colFat_Amt
        FatAmount.Width = 100
        FatAmount.ReadOnly = True
        FatAmount.DecimalPlaces = 2
        FatAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        FatAmount.IsVisible = True
        FatAmount.IsVisible = False
        gvConsumption.Columns.Add(FatAmount)

        SNFAmount.FormatString = ""
        SNFAmount.HeaderText = "SNF Amount"
        SNFAmount.Name = colSNF_Amt
        SNFAmount.Width = 100
        SNFAmount.ReadOnly = True
        SNFAmount.DecimalPlaces = 2
        SNFAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        SNFAmount.IsVisible = False
        gvConsumption.Columns.Add(SNFAmount)

        FIFOCost.FormatString = ""
        FIFOCost.HeaderText = "FIFO Cost"
        FIFOCost.Name = colFIFO_Cost
        FIFOCost.Width = 100
        FIFOCost.ReadOnly = True
        FIFOCost.DecimalPlaces = 2
        FIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        FIFOCost.IsVisible = False

        gvConsumption.Columns.Add(FIFOCost)

        LIFOCost.FormatString = ""
        LIFOCost.HeaderText = "LIFO Cost"
        LIFOCost.Name = colLIFO_Cost
        LIFOCost.Width = 100
        LIFOCost.ReadOnly = True
        LIFOCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        LIFOCost.IsVisible = False
        LIFOCost.DecimalPlaces = 2
        gvConsumption.Columns.Add(LIFOCost)

        AvgCost.FormatString = ""
        AvgCost.HeaderText = "AVG Cost"
        AvgCost.Name = colAVG_Cost
        AvgCost.Width = 100
        AvgCost.ReadOnly = True
        AvgCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        AvgCost.IsVisible = True
        AvgCost.DecimalPlaces = 2
        gvConsumption.Columns.Add(AvgCost)

        gvConsumption.EnableFiltering = False
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
        HCODE.IsVisible = False
        HCODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProductionCost.Columns.Add(HCODE)

        HCODEDesc.FormatString = ""
        HCODEDesc.HeaderText = "Cost Desc"
        HCODEDesc.Name = colItemDesc
        HCODEDesc.Width = 100
        HCODEDesc.ReadOnly = True
        HCODEDesc.IsVisible = False
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

        If (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) > 0) Then
            dr = dt.NewRow()
            dr("Code") = "2"
            dr("Name") = "Skip"
            dt.Rows.Add(dr)
        End If

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
    Sub OpenSerialItem()
        If clsCommon.myCBool(gvBatch.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
            frm.strItemCode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemDesc).Value)
            frm.dblqty = clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colReceiptQty).Value)
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
        Me.txtLocation.Value = Nothing
        txtConsmLocOther.Value = Nothing
        BlankAllControls()
        'LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpDate.Focus()
        txtLocation.Enabled = True
        gvBatch.Rows.Clear()
        Me.txtBatchNo.Text = ""
        'RadButton1.Visible = False
        'Me.txtReceivedBy.Value = Nothing

        dtpBatchDate.Text = Nothing
        dtpBatchDate.Value = Nothing
        Me.txtDesc.Text = ""
        Me.txtComment.Text = ""
        'lblConsmSectionCode.Text = ""
        'lblConsmSectionLocCode.Text = ""        
        txtCode.MyReadOnly = False
        btnGo.Enabled = True
        LOCATIONRIGTHS()
        gvBatch.Rows.Clear()
        gvBatch.Rows.AddNew()
        ClickGo = False
    End Sub
    Function AllowToSave(Optional ByVal isPost As Boolean = False) As Boolean

        If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            Return False
        End If
        'If clsCommon.myLen(txtBatchNo.Text) <= 0 Then
        '    myMessages.blankValue("Batch Order Code")
        '    txtBatchNo.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(CboShift.SelectedValue) <= 0 Then
            myMessages.blankValue(Me, "Shift", Me.Text)
            CboShift.Focus()
            Return False
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue(Me, "Location Code", Me.Text)
            txtLocation.Focus()
            Return False
        End If
        If clsCommon.myLen(txtConsmLocOther.Value) <= 0 Then
            myMessages.blankValue(Me, "Con. Location", Me.Text)
            txtConsmLocOther.Focus()
            Return False
        End If

        'If clsCommon.myLen(lblConsmSectionLocCode.Text) <= 0 Then
        '    myMessages.blankValue("Consumption Location Code")
        '    txtBatchNo.Focus()
        '    Return False
        'End If
        'If clsCommon.myLen(lblConsmSectionCode.Text) <= 0 Then
        '    myMessages.blankValue("Consumption Section Code")
        '    txtBatchNo.Focus()
        '    Return False
        'End If

        If Me.gvBatch.Rows.Count = 0 Then
            myMessages.blankValue(Me, "Batch List is Empty", Me.Text)
            Return False
        End If
        'Dim ii As Int16 = 0
        Dim countItem As Integer = 0
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 Then
                countItem += 1
            End If
        Next

        If countItem = 0 Then
            myMessages.blankValue(Me, "Please Select BOM First.", Me.Text)
            Return False
        End If

        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 Then
                If clsCommon.myLen(grow.Cells(colSP_Loaction_Code).Value) <= 0 Then
                    myMessages.blankValue(Me, "Enter Location in Batch Production tab at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If
            End If
            UpdateCurrentRow(grow.Index)
        Next

        For Each grow As GridViewRowInfo In gvConsumption.Rows
            If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                If clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(colSP_Loaction_Code).Value) <= 0 Then
                        myMessages.blankValue(Me, "Enter Location in consumption tab at line no- " & (grow.Index + 1) & "", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(grow.Cells(colUOM).Value) <= 0 Then
                    myMessages.blankValue(Me, "Enter UOM in consumption tab at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colMainUOM).Value) <= 0 Then
                    myMessages.blankValue(Me, "Main UOM in consumption tab is blank at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colMainItemCode).Value) <= 0 Then
                    myMessages.blankValue(Me, "Main Item Code in consumption tab is blank at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colBOMCode).Value) <= 0 Then
                    myMessages.blankValue(Me, "Bom Code in consumption tab is blank at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If

                If clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) <= 0 Then
                    myMessages.blankValue(Me, "Consumption Qty in consumption tab should be greter then zero(Item Conversion Factor not available for " + clsCommon.myCstr(grow.Cells(colUOM).Value) + " ). at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If

                If clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) < clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Min).Value) OrElse clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) > clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Max).Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Consumption Qty should be between  Minimum Qty (" + clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Min).Value)) + ") and Maximum Qty (" + clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Max).Value)) + ") for " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " ). at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If

                If clsCommon.myCDecimal(grow.Cells(colAVG_Cost).Value) < 0 Then
                    myMessages.blankValue(Me, "AVG Cost in consumption tab should be greter then or equals to zero( Stock not available). at line no- " & (grow.Index + 1) & "", Me.Text)
                    Return False
                End If

                If SettCheckBalanceOnSave Then
                    If clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Stock).Value) < clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Consumption Qty should be less than balance Qty (" + clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Stock).Value)) + ")  for " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " ). at line no- " & (grow.Index + 1) & "", Me.Text)
                        Return False
                    End If
                End If
            End If

        Next

        '' check fat/snf control
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.FatSNFControlOnProductionConsumption, clsFixedParameterCode.FatSNFControlOnProductionConsumption, Nothing), "1") = CompairStringResult.Equal Then
            If ValidateFatSNFQuantityControl() = False Then
                Return False
            End If
        End If
        UcAttachment1.AllowToSave()

        Return True
    End Function
    Function ValidateFatSNFQuantityControl() As Boolean
        Dim TotIssueFatKg As Decimal = 0
        Dim TotIssueSnfKg As Decimal = 0
        Dim TotIssueQty As Decimal = 0

        Dim TotProdFatKg As Decimal = 0
        Dim TotProdSnfKg As Decimal = 0
        Dim TotProdQty As Decimal = 0


        '' for Produced/removed qty
        For Each grow As GridViewRowInfo In gvBatch.Rows
            Dim ProductType As String = clsItemMaster.GetItemProductType(grow.Cells(colItemCode).Value, Nothing)
            If clsCommon.CompairString(ProductType, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(ProductType, "MP") = CompairStringResult.Equal Then
                TotProdFatKg = TotProdFatKg + clsCommon.myCDecimal(grow.Cells(colFAT_KG).Value)
                TotProdSnfKg = TotProdSnfKg + clsCommon.myCDecimal(grow.Cells(colSNF_KG).Value)
                TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colItemCode).Value, grow.Cells(colUOM).Value, grow.Cells(colFINAL_PROD_Qty).Value, Nothing)
            End If
        Next

        Dim Message As String = ""
        Dim TolerancePer As Decimal = 0
        Dim ToleranceQty As Decimal = 0
        TolerancePer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.QuantityControlToleranceOnProductionConsumption, clsFixedParameterCode.QuantityControlToleranceOnProductionConsumption, Nothing))
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
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Function SaveData(ByVal ChekBtnPost As Boolean) As Boolean
        Try
            If ClickGo = True Then
                clsCommon.MyMessageBoxShow("Please click Go Button.")
                btnGo.Focus()
                Exit Function
            End If
            '' Anubhooti 09-Sep-2014 BM00000003735
            If ChekBtnPost = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                    Exit Function
                End If
            End If
            'updateBatchGridParameter()
            If AllowToSave() Then
                Dim obj As New clsStanderdProductionEntry 'clsProductionEntryWithoutBatch              
                obj.PROD_ENTRY_CODE = Me.txtCode.Value
                obj.DESCRIPTION = Me.txtDesc.Text
                obj.PROD_DATE = Me.dtpDate.Value
                obj.Batch_Code_Manual = Me.txtBatchNo.Text
                obj.BATCH_DATE = Me.dtpBatchDate.Value
                obj.RECEIVED_BY = ""
                obj.LOCATION_CODE = clsCommon.myCstr(Me.txtLocation.Value)
                obj.COMMENTS = Me.txtComment.Text
                obj.Structure_Code = fndItemCategory.Value
                If gvBatch.Tag Is Nothing Then
                    obj.Section_Stage_Map_Code = ""
                Else
                    obj.Section_Stage_Map_Code = gvBatch.Tag
                End If
                obj.CONSM_LOCATION_CODE = txtConsmLocMilk.Value
                obj.CONSM_LOCATION_CODE_Other = txtConsmLocOther.Value
                obj.CONSM_SECTION_CODE = "" ' lblConsmSectionCode.Text
                obj.Shift_Code = Me.CboShift.SelectedValue
                Dim obj1 As clsStanderdProductionEntryDetail
                'objList = New List(Of clsProductionEntryWithoutBatchDetail)

                obj.ArrBatchItem = New List(Of clsStanderdProductionEntryDetail)
                'obj.ArrIssueItem = New List(Of clsProcessProductionPEIssueItemDetail)
                'obj.ArrQC = New List(Of clsProcessProductionPEQCDetail)
                'obj.ArrStage = New List(Of clsProcessProductionPEStageDetail)
                'obj.ArrWF = New List(Of clsPPPEWFItemDetail)
                'obj.ArrScrap = New List(Of clsPPScrapItemDetail)


                For Each grow As GridViewRowInfo In gvBatch.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        obj1 = New clsStanderdProductionEntryDetail()
                        obj1.PROD_ENTRY_CODE = txtCode.Value
                        obj1.Shift_Code = clsCommon.myCstr(grow.Cells(colShiftCode).Value)
                        obj1.Section_Code = clsCommon.myCstr(grow.Cells(colSectionCode).Value)
                        obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)

                        obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        obj1.BATCH_QTY = clsCommon.myCDecimal(grow.Cells(colBatchQty).Value)
                        obj1.RECEIPT_QTY = clsCommon.myCDecimal(grow.Cells(colReceiptQty).Value)
                        obj1.REJ_HEAD = clsCommon.myCstr(grow.Cells(colRejHead).Value)
                        obj1.REJ_QTY = clsCommon.myCDecimal(grow.Cells(colRejQty).Value)
                        obj1.BREAKAGE_HEAD = clsCommon.myCstr(grow.Cells(colBreakageHead).Value)
                        obj1.BREAKAGE_QTY = clsCommon.myCDecimal(grow.Cells(colBreakageQty).Value)
                        obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        obj1.LAB_TESTING = clsCommon.myCstr(grow.Cells(colLabTesting).Value)

                        obj1.FAT_Per = clsCommon.myCDecimal(grow.Cells(colFAT_Per).Value)
                        obj1.SNF_Per = clsCommon.myCDecimal(grow.Cells(colSNF_Per).Value)

                        grow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colFAT_Per).Value), Nothing)
                        grow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colSNF_Per).Value), Nothing)


                        obj1.FAT_KG = clsCommon.myCDecimal(grow.Cells(colFAT_KG).Value)
                        obj1.SNF_KG = clsCommon.myCDecimal(grow.Cells(colSNF_KG).Value)

                        obj1.FINAL_PRODUCTION_QTY = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value)
                        'obj1.FINAL_PRODUCTION_QTY_BAG = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Bag).Value)
                        'obj1.FINAL_PRODUCTION_QTY_Min = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Min).Value)
                        'obj1.FINAL_PRODUCTION_QTY_Max = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Max).Value)

                        obj1.LOCATION_CODE = clsCommon.myCstr(grow.Cells(colSP_Loaction_Code).Value)

                        obj.ArrBatchItem.Add(obj1)

                    End If
                Next
                If obj.ArrBatchItem Is Nothing OrElse obj.ArrBatchItem.Count <= 0 Then
                    Throw New Exception("No Production item found")
                End If
                If obj.ArrBatchItem.Count > 1 Then
                    Throw New Exception("You can produce only one item Production")
                End If
                Dim obj2 As New clsStanderdProductionEntryConsumption
                For Each grow As GridViewRowInfo In gvConsumption.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        obj2 = New clsStanderdProductionEntryConsumption()
                        obj2.PROD_ENTRY_CODE = txtCode.Value

                        obj2.AVG_COST = clsCommon.myCDecimal(grow.Cells(colAVG_Cost).Value)
                        obj2.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)

                        obj2.BOM_Desc = clsCommon.myCstr(grow.Cells(colBOMDesc).Value)
                        obj2.CONSM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj2.CONSM_QTY_Original = clsCommon.myCDecimal(grow.Cells(colConsumptionQtyOriginal).Value)
                        obj2.CONSM_QTY = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value)

                        obj2.FINAL_PRODUCTION_QTY_Min = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Min).Value)
                        obj2.FINAL_PRODUCTION_QTY_Max = clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty_Max).Value)

                        obj2.Fat_Amt = clsCommon.myCDecimal(grow.Cells(colFat_Amt).Value)
                        obj2.FAT_KG = clsCommon.myCDecimal(grow.Cells(colFAT_KG).Value)
                        obj2.FAT_Per = clsCommon.myCDecimal(grow.Cells(colFAT_Per).Value)
                        obj2.Fat_Rate = clsCommon.myCDecimal(grow.Cells(colFat_Rate).Value)
                        obj2.FIFO_COST = clsCommon.myCDecimal(grow.Cells(colFIFO_Cost).Value)
                        obj2.LIFO_COST = clsCommon.myCDecimal(grow.Cells(colLIFO_Cost).Value)
                        obj2.LOCATION_CODE = clsCommon.myCstr(grow.Cells(colSP_Loaction_Code).Value)

                        obj2.Main_ITEM_CODE = clsCommon.myCstr(grow.Cells(colMainItemCode).Value)
                        obj2.Main_ITEM_Desc = clsCommon.myCstr(grow.Cells(colMainItemDesc).Value)
                        obj2.MAIN_UOM = clsCommon.myCstr(grow.Cells(colMainUOM).Value)
                        obj2.MAIN_UOM_Desc = clsCommon.myCstr(grow.Cells(colMainUOMDesc).Value)

                        obj2.SNF_Amt = clsCommon.myCDecimal(grow.Cells(colSNF_Amt).Value)
                        obj2.SNF_KG = clsCommon.myCDecimal(grow.Cells(colSNF_KG).Value)
                        obj2.SNF_Per = clsCommon.myCDecimal(grow.Cells(colSNF_Per).Value)
                        obj2.SNF_Rate = clsCommon.myCDecimal(grow.Cells(colSNF_Rate).Value)
                        obj2.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        obj.ArrConsm.Add(obj2)
                    End If
                Next

                Dim obj3 As New clsStanderdProductionEntryConsumptionCost
                For Each grow As GridViewRowInfo In gvProductionCost.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colMainItemCode).Value)) > 0 Then
                        obj3 = New clsStanderdProductionEntryConsumptionCost()
                        obj3.PROD_ENTRY_CODE = txtCode.Value

                        obj3.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        obj3.BOM_Desc = clsCommon.myCstr(grow.Cells(colBOMDesc).Value)

                        obj3.COST_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj3.COST_CODE_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        obj3.Main_ITEM_CODE = clsCommon.myCstr(grow.Cells(colMainItemCode).Value)
                        obj3.Main_ITEM_Desc = clsCommon.myCstr(grow.Cells(colMainItemDesc).Value)
                        obj3.MAIN_UOM = clsCommon.myCstr(grow.Cells(colMainUOM).Value)
                        obj3.MAIN_UOM_Desc = clsCommon.myCstr(grow.Cells(colMainUOMDesc).Value)
                        obj3.OverHead_Cost = clsCommon.myCDecimal(grow.Cells(colAVG_Cost).Value)

                        obj.ArrConsmCost.Add(obj3)
                    End If
                Next

                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, obj.ArrBatchItem, isNewEntry, clsCommon.myCstr(txtCode.Value))

                If issaved = True Then
                    UcAttachment1.SaveData(obj.PROD_ENTRY_CODE)
                    If ChekBtnPost = False AndAlso Import = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.PROD_ENTRY_CODE, NavigatorType.Current)
                    If Import Then
                        Dim qry As String = "update TSPL_SPP_PRODUCTION_IMPORT set Import_Status='Y' where 2=2"
                        qry = qry & " and Prod_Date='" & clsCommon.GetPrintDate(dtpDate.Value, "dd-MMM-yyyy") & "'"
                        If clsCommon.myLen(txtLocation.Value) > 0 Then
                            qry = qry & " and Location_Code='" & txtLocation.Value & "'"
                        End If
                        If clsCommon.myLen(fndItemCategory.Value) > 0 Then
                            qry = qry & " and Category='" & fndItemCategory.Value & "'"
                        End If

                        If clsCommon.myLen(txtLocation.Value) > 0 Then
                            qry = qry & " and Consm_Loc_Milk='" & txtConsmLocMilk.Value & "'"
                        End If
                        If clsCommon.myLen(txtLocation.Value) > 0 Then
                            qry = qry & " and Consm_Loc_Other='" & txtConsmLocOther.Value & "'"
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                    Return True
                End If

                'Return False
            End If
            Return True
        Catch ex As Exception
            If Import Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
                Return False
            End If

        End Try
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isInsideLoadData = True
        obj = clsStanderdProductionEntry.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            btnSave.Text = "Update"
            'btnGo.Enabled = btnunpost.Visible
            If obj.POSTED Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                btnGo.Enabled = False
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadBlankGrid()
            txtCode.Value = obj.PROD_ENTRY_CODE
            Me.txtDesc.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.txtComment.Text = clsCommon.myCstr(obj.COMMENTS)

            'Me.txtReceivedBy.Value = clsCommon.myCstr(obj.RECEIVED_BY)
            Me.txtLocation.Value = obj.LOCATION_CODE
            Me.dtpDate.Value = obj.PROD_DATE
            Me.dtpBatchDate.Value = obj.BATCH_DATE
            Me.lblLocation.Text = obj.LOCATION_NAME
            'Me.lblEmpName.Text = obj.RECEIVED_BY_NAME
            Me.txtBatchNo.Text = obj.Batch_Code_Manual
            fndItemCategory.Value = obj.Structure_Code
            TxtCategory.Text = obj.Structure_Desc
            CboShift.SelectedValue = obj.Shift_Code
            gvBatch.Tag = obj.Section_Stage_Map_Code
            txtConsmLocMilk.Value = obj.CONSM_LOCATION_CODE
            lblConsmLocMilkDesc.Text = clsLocation.GetName(txtConsmLocMilk.Value, Nothing)

            txtConsmLocOther.Value = obj.CONSM_LOCATION_CODE_Other
            lblConsmLocOtherDesc.Text = clsLocation.GetName(txtConsmLocOther.Value, Nothing)

            Dim arr_BatchItem As New List(Of String)
            Dim arr_WrckageItem As New List(Of String)
            Dim arr_ScrapItem As New List(Of String)
            arr_BatchItem = New List(Of String)
            arr_WrckageItem = New List(Of String)
            arr_ScrapItem = New List(Of String)

            If (obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0) Then
                For Each objTr As clsStanderdProductionEntryDetail In obj.ArrBatchItem
                    gvBatch.Rows.AddNew()
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Tag = objTr.arrSrItem
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLineNo).Value = Me.gvBatch.Rows.Count
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colShiftCode).Value = clsCommon.myCstr(objTr.Shift_Code)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSectionCode).Value = clsCommon.myCstr(objTr.Section_Code)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(objTr.BOM_CODE)

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(objTr.ITEM_CODE)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(objTr.ITEM_DESCRIPTION)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCDecimal(objTr.BATCH_QTY)


                    '' new code 
                    Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value = clsStanderdProductionEntry.GetPrevProductionQty(txtBatchNo.Text, txtCode.Value, objTr.ITEM_CODE, Nothing)
                    Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPendingBatchQty).Value = objTr.BATCH_QTY - clsCommon.myCDecimal(Me.gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colPrevProdQty).Value)

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colReceiptQty).Value = clsCommon.myCDecimal(objTr.RECEIPT_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colRejHead).Value = clsCommon.myCstr(objTr.REJ_HEAD)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colRejQty).Value = clsCommon.myCDecimal(objTr.REJ_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBreakageHead).Value = clsCommon.myCstr(objTr.BREAKAGE_HEAD)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBreakageQty).Value = clsCommon.myCDecimal(objTr.BREAKAGE_QTY)
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colLabTesting).Value = objTr.LAB_TESTING

                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value = objTr.UNIT_CODE
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = objTr.FINAL_PRODUCTION_QTY
                    If objTr.FINAL_PRODUCTION_QTY > 0 Then
                        Dim convFactMul As Decimal = clsItemMaster.GetConvertionFactor(objTr.ITEM_CODE, objTr.UNIT_CODE, Nothing)
                        Dim convFactDiv As Decimal = clsItemMaster.GetConvertionFactor(objTr.ITEM_CODE, objTr.UOM_Bag, Nothing)
                        gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Bag).Value = clsCommon.myCDivide(objTr.FINAL_PRODUCTION_QTY * convFactMul, convFactDiv)
                    End If
                    'gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Bag).Value = objTr.FINAL_PRODUCTION_QTY_BAG
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = objTr.LOCATION_CODE
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(objTr.LOCATION_CODE, Nothing)
                    'gvBatch.CurrentRow.Cells(colUOMBag).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + bomcode + "'"))

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
                    gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOMBag).Value = objTr.UOM_Bag
                    If clsCommon.myLen(objTr.ITEM_CODE) > 0 AndAlso Not arr_BatchItem.Contains(objTr.ITEM_CODE) Then
                        arr_BatchItem.Add(objTr.ITEM_CODE)
                    End If
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.ITEM_CODE)
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.ITEM_CODE)
                Next
            Else
                'gv1.Rows.AddNew()
            End If
            For Each obj2 As clsStanderdProductionEntryConsumption In obj.ArrConsm

                gvConsumption.Rows.AddNew()
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colLineNo).Value = gvConsumption.Rows.Count
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colAVG_Cost).Value = obj2.AVG_COST
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colBOMCode).Value = obj2.BOM_CODE

                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colBOMDesc).Value = obj2.BOM_Desc
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colProductType).Value = obj2.Consm_Product_Type
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colItemCode).Value = obj2.CONSM_ITEM_CODE
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colItemDesc).Value = obj2.CONSM_ITEM_Desc
                If obj2.FINAL_PRODUCTION_QTY_Min = 0 Then
                    gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Min).Value = obj2.CONSM_QTY - (obj2.CONSM_QTY * (1 / 100))
                Else
                    gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Min).Value = obj2.FINAL_PRODUCTION_QTY_Min
                End If
                If obj2.FINAL_PRODUCTION_QTY_Max = 0 Then
                    gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Max).Value = obj2.CONSM_QTY + (obj2.CONSM_QTY * (1 / 100))
                Else
                    gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Max).Value = obj2.FINAL_PRODUCTION_QTY_Max
                End If

                'gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Max).Value = obj2.FINAL_PRODUCTION_QTY_Max
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = obj2.CONSM_QTY
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colConsumptionQtyOriginal).Value = obj2.CONSM_QTY_Original

                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Amt).Value = obj2.Fat_Amt
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_KG).Value = obj2.FAT_KG
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_Per).Value = obj2.FAT_Per
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Rate).Value = obj2.Fat_Rate
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFIFO_Cost).Value = obj2.FIFO_COST
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colLIFO_Cost).Value = obj2.LIFO_COST
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = obj2.LOCATION_CODE
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = obj2.LOCATION_Desc

                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainItemCode).Value = obj2.Main_ITEM_CODE
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainItemDesc).Value = obj2.Main_ITEM_Desc
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainUOM).Value = obj2.MAIN_UOM
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainUOMDesc).Value = obj2.MAIN_UOM_Desc

                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Amt).Value = obj2.SNF_Amt
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_KG).Value = obj2.SNF_KG
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Per).Value = obj2.SNF_Per
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Rate).Value = obj2.SNF_Rate
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colUOM).Value = obj2.UNIT_CODE

                Dim Product_Type As String = clsItemMaster.GetItemProductType(obj2.CONSM_ITEM_CODE, Nothing)
                Dim BalanceQty As Decimal = 0
                If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                    BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(obj2.CONSM_ITEM_CODE), clsLocation.GetMainLocationMilk(clsCommon.myCstr(obj2.LOCATION_CODE), Nothing), clsCommon.myCstr(obj2.LOCATION_CODE), txtCode.Value, dtpDate.Value, Nothing, clsCommon.myCstr(obj2.UNIT_CODE))
                Else
                    BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(obj2.CONSM_ITEM_CODE), clsCommon.myCstr(obj2.LOCATION_CODE), txtCode.Value, dtpDate.Value, Nothing, clsCommon.myCstr(obj2.UNIT_CODE), 0)
                End If
                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Stock).Value = BalanceQty

            Next

            'Dim obj3 As New clsConsumptionCostWithoutBatch
            For Each obj3 As clsStanderdProductionEntryConsumptionCost In obj.ArrConsmCost
                If clsCommon.myLen(obj3.Main_ITEM_CODE) > 0 Then
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
            'btnGo.Enabled = False
            UcAttachment1.LoadData(obj.PROD_ENTRY_CODE)
            '' load section 
            'FillSection()
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
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                    Exit Sub
                End If
                clsStanderdProductionEntry.PostData(Form_ID, txtCode.Value, arrLoc, True)
                common.clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtCode.Value, NavigatorType.Current)
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
                If (clsStanderdProductionEntry.DeleteData(txtCode.Value)) Then
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
        check = clsStanderdProductionEntry.CheckValidCode(Me.txtCode.Value)

        If check Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsStanderdProductionEntry.GetFinder(" TSPL_SPP_PRODUCTION_ENTRY.location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
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
    Private Sub frmProductionEntryWithoutBatch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                                              "TSPL_SPP_PRODUCTION_ENTRY " + Environment.NewLine +
                                              "TSPL_SPP_PRODUCTION_ENTRY_DETAIL " + Environment.NewLine +
                                              "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                              "TSPL_SPP_CONSUMPTION_WITHOUT_BATCH " + Environment.NewLine +
                                              "TSPL_SPP_COST_WITHOUT_BATCH " + Environment.NewLine +
                                              "Press Alt+P for Post Trasnaction " + Environment.NewLine +
                                              "TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL " + Environment.NewLine +
                                              "TSPL_SPP_PRODUCTION_ENTRY_DETAIL " + Environment.NewLine +
                                              "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                              "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                              "TSPL_BATCH_ITEM " + Environment.NewLine +
                                              "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine +
                                              "TSPL_JOURNAL_MASTER ")
            If btnPost.Enabled = False AndAlso btnSave.Enabled = False Then
                If MyBase.isReverse Then

                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnunpost.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            If btnPost.Enabled = False AndAlso btnSave.Enabled = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    RadButton1.Visible = True
                End If
            End If
        ElseIf e.KeyData = (Keys.Control + Keys.A + Keys.T) Then
        ElseIf e.KeyData = (Keys.Control + Keys.B) Then
            RadPageView1.SelectedPage = pageBatchProduction
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
    Private Sub funPrint()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select document", Me.Text)
                txtCode.Focus()
                Exit Sub
            End If
            Dim sQuery As String = ""



            sQuery = " select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , cast(TSPL_COMPANY_MASTER.logo_img as image) as logo_img,tspl_company_master.Pincode,tspl_company_master.Tcan_No,TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE,TSPL_SPP_PRODUCTION_ENTRY.DESCRIPTION,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_SPP_PRODUCTION_ENTRY.COMMENTS,TSPL_SPP_PRODUCTION_ENTRY.CONSM_LOCATION_CODE_Other,TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_ITEM_MASTER .Item_Desc,TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY ,TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE
from TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=.TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE
 left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'
where TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE='" + txtCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)


            sQuery = "select ITEM_CODE,ITEM_DESCRIPTION,RECEIPT_QTY,UNIT_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL where PROD_ENTRY_CODE='" + txtCode.Value + "'"
            Dim dtCredit As DataTable = clsDBFuncationality.GetDataTable(sQuery)



            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.PRODUCTION, dt, dtCredit, "rptProductionEntry", "rptProductionEntry.rpt", clsCommon.myCDate(dtpDate.Value), "SubPaymentProcessCredit.rpt")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()
        txtImportTemplate.arrValueMember = Nothing
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "'"))
        txtConsmLocOther.Value = txtLocation.Value
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBatch.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If e.Column Is gvBatch.Columns(colBOMCode) Then
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            txtLocation.Focus()
                            Throw New Exception("Select Location First!")
                        End If
                        OpenBOMCode(False)
                        UpdateCurrentRow(gvBatch.CurrentRow.Index)
                        ClickGo = True
                    ElseIf e.Column Is gvBatch.Columns(colItemCode) Then
                        isCellValueChanged = True
                        OpenBOMICode(False)
                        isCellValueChanged = False
                    ElseIf e.Column Is gvBatch.Columns(colUOM) Then
                        isCellValueChanged = True
                        OpenUOM(False)
                        isCellValueChanged = False
                    ElseIf e.Column Is gvBatch.Columns(colShiftCode) Then
                        isCellValueChanged = True
                        OpenShiftCode(False)
                        isCellValueChanged = False
                    ElseIf e.Column Is gvBatch.Columns(colFINAL_PROD_Qty) Then
                        isCellValueChanged = True
                        gvBatch.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
                        gvBatch.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
                        isCellValueChanged = False
                        ClickGo = True
                    ElseIf e.Column Is gvBatch.Columns(colSP_Loaction_Code) Then
                        isCellValueChanged = True
                        Dim WhrCls As String = ""
                        WhrCls += "(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "') or Location_Code='" & txtLocation.Value & "')"
                        'End If
                        gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value = clsLocation.getFinder(WhrCls, gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value, False)
                        gvBatch.CurrentRow.Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value, Nothing)
                    ElseIf e.Column Is gvBatch.Columns(colFINAL_PROD_Qty_Bag) Then
                        UpdateCurrentRow(gvBatch.CurrentRow.Index)
                        ClickGo = True
                    End If
                    isCellValueChanged = False
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenBOMICode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""

        bomcode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colBOMCode).Value)

        If clsCommon.myLen(bomcode) > 0 Then
            Dim qry As String = "select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"
            whrCls = " tspl_item_master.item_code='" + clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)) + "'  and tspl_item_master.Active='1' "
        Else
            whrCls = " tspl_item_master.item_type in ('F','S') and tspl_item_master.Active='1' "
        End If

        icode = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), isButtonClicked)

        If clsCommon.myLen(icode) > 0 Then
            gvBatch.CurrentRow.Cells(colItemCode).Value = icode
            gvBatch.CurrentRow.Cells(colItemDesc).Value = clsItemMaster.GetItemName(icode, Nothing) ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))

            gvBatch.CurrentRow.Cells(colFAT_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
            gvBatch.CurrentRow.Cells(colSNF_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + icode + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
            gvBatch.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
            gvBatch.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
            'If clsCommon.myLen(gvBatch.CurrentRow.Cells(colSectionCode).Value) <= 0 Then
            '    gvBatch.CurrentRow.Cells(colSectionCode).Value = fndSection.Value
            '    'gvBatch.CurrentRow.Cells(colSectionName).Value = txtSectionName.Text
            'End If

        Else
            gvBatch.CurrentRow.Cells(colItemCode).Value = ""
            gvBatch.CurrentRow.Cells(colItemDesc).Value = ""
            'gv.CurrentRow.Cells(colItype).Value = ""
            gvBatch.CurrentRow.Cells(colUOM).Value = ""
            gvBatch.CurrentRow.Cells(colFAT_Per).Value = Nothing
            gvBatch.CurrentRow.Cells(colFAT_KG).Value = Nothing
            gvBatch.CurrentRow.Cells(colSNF_Per).Value = Nothing
            gvBatch.CurrentRow.Cells(colSNF_KG).Value = Nothing
            'gv.CurrentRow.Cells(colSection).Value = ""
            'gv.CurrentRow.Cells(colSectionName).Value = ""
        End If
    End Sub
    Sub OpenBOMCode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        'Dim whrCls As String = ""
        Dim bomcode As String = ""
        'icode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)
        'Dim sectionCondition As String = ""

        'If clsCommon.myLen(fndItemCategory.Value) > 0 Then
        '    sectionCondition = " and tspl_pp_bom_head.ITEM_CATEGORY_CODE='" + fndItemCategory.Value + "' "
        'End If

        'If clsCommon.myLen(icode) > 0 Then
        '    whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + icode + "' and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        'Else
        '    whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and tspl_item_master.item_type in ('F','S') and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        'End If

        'Dim oldbomcode As String = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colBOMCode).Value)

        'Dim qry As String = " select TSPL_MF_BOM_HEAD.bom_code as Code,TSPL_MF_BOM_HEAD.bom_date as [BOM Date],TSPL_MF_BOM_HEAD.Description,  TSPL_MF_BOM_HEAD.START_DATE as [Valid From],TSPL_MF_BOM_HEAD.END_DATE as [Valid Upto],TSPL_MF_BOM_HEAD.Status,TSPL_MF_BOM_HEAD.prod_item_code as [Main Item Code],  tspl_item_master.item_desc as [Item Description],TSPL_MF_BOM_HEAD.prod_item_unit_code as [Unit],TSPL_MF_BOM_HEAD.prod_quantity as [Quantity],TSPL_MF_BOM_HEAD.revision_no as [Revision No], (case when TSPL_MF_BOM_HEAD.POSTED='1' then 'Posted' else 'UnPosted' end) as [Post Status] from TSPL_MF_BOM_HEAD  left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_HEAD.prod_item_code "
        ''tspl_item_master.item_type in ('F','S') and
        'Dim whr As String = "  isnull(TSPL_MF_BOM_HEAD.POSTED,'0')='1'  and
        '                      ( ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd-MMM-yyyy")) + "' between cast(TSPL_MF_BOM_HEAD.START_DATE as date) and cast(TSPL_MF_BOM_HEAD.END_DATE as date) ) or
        '                      (  '" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd-MMM-yyyy")) + "' >=  cast(TSPL_MF_BOM_HEAD.START_DATE as date)  and TSPL_MF_BOM_HEAD.END_DATE is null) )"
        '' bomcode = clsCommon.ShowSelectForm("SProductionBOM@Finder", qry, "Code", whr, "", "Code", isButtonClicked)  'clsBOM.GetBOMFinderWithValidityCheck(whrCls, oldbomcode, dtpDate.Value, isButtonClicked)
        ''bomcode = clsCommon.ShowSelectForm("SProductionBOM@Finder", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)
        'qry = qry + " inner join ( select TSPL_MF_BOM_HEAD.prod_item_code, max(REVISION_NO) as REVISION_NO from TSPL_MF_BOM_HEAD   where   " + whr + "   group by TSPL_MF_BOM_HEAD.prod_item_code )  TBL_REVISION on TBL_REVISION.prod_item_code = TSPL_MF_BOM_HEAD.prod_item_code and  TBL_REVISION.REVISION_NO = TSPL_MF_BOM_HEAD.revision_no "



        '   Dim whr As String = "  isnull(TSPL_MF_BOM_HEAD.POSTED,'0')='1' and TSPL_MF_BOM_HEAD.Location_code='" + txtLocation.Value + "' and
        '                         ( ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd-MMM-yyyy")) + "' between cast(TSPL_MF_BOM_HEAD.START_DATE as date) and cast(TSPL_MF_BOM_HEAD.END_DATE as date) ) or
        '                         (  '" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd-MMM-yyyy")) + "' >=  cast(TSPL_MF_BOM_HEAD.START_DATE as date)  and TSPL_MF_BOM_HEAD.END_DATE is null) )"

        '   Dim qry As String = " select TSPL_MF_BOM_HEAD.bom_code as Code,TSPL_MF_BOM_HEAD.bom_date as [BOM Date],TSPL_MF_BOM_HEAD.Description,  TSPL_MF_BOM_HEAD.START_DATE as [Valid From],TSPL_MF_BOM_HEAD.END_DATE as [Valid Upto],TSPL_MF_BOM_HEAD.Status,TSPL_MF_BOM_HEAD.prod_item_code as [Main Item Code],  tspl_item_master.item_desc as [Item Description],TSPL_MF_BOM_HEAD.prod_item_unit_code as [Unit],TSPL_MF_BOM_HEAD.prod_quantity as [Quantity],TSPL_MF_BOM_HEAD.revision_no as [Revision No], TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
        'TSPL_ITEM_UOM_DETAIL.UOM_Code, (case when TSPL_MF_BOM_HEAD.POSTED='1' then 'Posted' else 'UnPosted' end) as [Post Status] from TSPL_MF_BOM_HEAD  left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_HEAD.prod_item_code
        '      inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_mf_bom_head.PROD_ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
        '      WHERE isnull(TSPL_MF_BOM_HEAD.POSTED,'0')='1' and TSPL_MF_BOM_HEAD.Location_code='" + txtLocation.Value + "' and NOT EXISTS ( select TSPL_MF_BOM_HEAD.prod_item_code, max(REVISION_NO) as REVISION_NO from TSPL_MF_BOM_HEAD   where   " + whr + " AND  tspl_item_master.item_code=TSPL_MF_BOM_HEAD.prod_item_code  group by TSPL_MF_BOM_HEAD.prod_item_code ) "

        '   qry += " UNION ALL "

        '   qry += " select TSPL_MF_BOM_HEAD.bom_code as Code,TSPL_MF_BOM_HEAD.bom_date as [BOM Date],TSPL_MF_BOM_HEAD.Description,  TSPL_MF_BOM_HEAD.START_DATE as [Valid From],TSPL_MF_BOM_HEAD.END_DATE as [Valid Upto],TSPL_MF_BOM_HEAD.Status,TSPL_MF_BOM_HEAD.prod_item_code as [Main Item Code],  tspl_item_master.item_desc as [Item Description],TSPL_MF_BOM_HEAD.prod_item_unit_code as [Unit],TSPL_MF_BOM_HEAD.prod_quantity as [Quantity],TSPL_MF_BOM_HEAD.revision_no as [Revision No], TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
        '         TSPL_ITEM_UOM_DETAIL.UOM_Code, (case when TSPL_MF_BOM_HEAD.POSTED='1' then 'Posted' else 'UnPosted' end) as [Post Status] from TSPL_MF_BOM_HEAD  left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_HEAD.prod_item_code
        '           inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=tspl_mf_bom_head.PROD_ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
        '           INNER JOIN ( SELECT AAA.LOCATION_CODE,AAA.PROD_ITEM_CODE,MAX(AAA.REVISION_NO) AS REVISION_NO,AAA.BARLEY FROM ( select aa.LOCATION_CODE,aa.PROD_ITEM_CODE,MAX(aa.REVISION_NO) AS REVISION_NO ,AA.RM0001,AA.RM0006, case when ISNULL(aa.RM0001,0)='RM0001' AND ISNULL(aa.RM0006,0)='RM0006' THEN 0 WHEN ISNULL(aa.RM0001,0)<>'RM0001' AND ISNULL(aa.RM0006,0)='RM0006' THEN 1 WHEN ISNULL(aa.RM0001,0)='RM0001' AND ISNULL(aa.RM0006,0)<>'RM0006' THEN 0 END AS BARLEY from ( SELECT * FROM ( SELECT TSPL_MF_BOM_HEAD.LOCATION_CODE, TSPL_MF_BOM_HEAD.PROD_ITEM_CODE, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE, MAX(TSPL_MF_BOM_HEAD.REVISION_NO) AS REVISION_NO FROM TSPL_MF_BOM_HEAD LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE = TSPL_MF_BOM_HEAD.BOM_CODE WHERE CONSM_ITEM_CODE IN ('RM0001', 'RM0006') GROUP BY LOCATION_CODE, TSPL_MF_BOM_HEAD.PROD_ITEM_CODE, CONSM_ITEM_CODE ) yy PIVOT ( MAX(yy.CONSM_ITEM_CODE) FOR yy.CONSM_ITEM_CODE IN ([RM0001], [RM0006]) ) AS xx )aa GROUP BY aa.LOCATION_CODE,aa.PROD_ITEM_CODE,aa.RM0006,AA.RM0001 )AAA GROUP BY AAA.LOCATION_CODE,AAA.PROD_ITEM_CODE,AAA.BARLEY ) TBL_REVISION ON TBL_REVISION.PROD_ITEM_CODE = TSPL_MF_BOM_HEAD.PROD_ITEM_CODE AND TBL_REVISION.REVISION_NO = TSPL_MF_BOM_HEAD.REVISION_NO and  TSPL_MF_BOM_HEAD.LOCATION_CODE='" + txtLocation.Value + "' and TSPL_MF_BOM_HEAD.POSTED='1' "
        Dim qry As String = "select * from (
                            select TSPL_MF_BOM_HEAD.BOM_CODE AS CODE,TSPL_MF_BOM_HEAD.BOM_DATE AS BOM_DATE,TSPL_MF_BOM_HEAD.DESCRIPTION AS DESCRIPTION,TSPL_MF_BOM_HEAD.START_DATE AS [Valid From],TSPL_MF_BOM_HEAD.END_DATE AS [Valid Upto],TSPL_MF_BOM_HEAD.STATUS as status, TSPL_MF_BOM_HEAD.PROD_ITEM_CODE AS [Main Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Description],TSPL_MF_BOM_HEAD.PROD_ITEM_UNIT_CODE as Unit,TSPL_MF_BOM_HEAD.PROD_QUANTITY as Quantity, TSPL_MF_BOM_HEAD.REVISION_NO AS [Revision No], TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Conversion_Factor,TSPL_ITEM_UOM_DETAIL.UOM_Code as UOM_code,TSPL_MF_BOM_HEAD.location_code AS location_code  
                            from TSPL_MF_BOM_HEAD  
                            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  
                            left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MF_BOM_HEAD.PROD_ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='BAG' 
                            WHERE  TSPL_ITEM_MASTER.FG_for_CF=1 and TSPL_MF_BOM_HEAD.POSTED='1'   and CONVERT(DATE,GETDATE(),103) >= TSPL_MF_BOM_HEAD.START_DATE AND CONVERT(DATE,GETDATE(),103)<= TSPL_MF_BOM_HEAD.END_DATE
                            AND LOCATION_CODE='" + txtLocation.Value + "'
                            )xyyx "

        bomcode = clsCommon.ShowSelectForm("SProductionBOM@Finderr12345", qry, "Code", "", "Code", "", isButtonClicked)
        If clsCommon.myLen(bomcode) > 0 Then
            gvBatch.CurrentRow.Cells(colBOMCode).Value = bomcode
            'gvBatch.CurrentRow.Cells(colBOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "'"))
            gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select prod_quantity from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "'")), DecimalPointQty)
            gvBatch.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PROD_ITEM_UNIT_CODE from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "'"))
            gvBatch.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_code from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "'"))
            gvBatch.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value) + "'"))
            gvBatch.CurrentRow.Cells(colUOMBag).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value) + "'  and UOM_Code='Bag'"))
            gvBatch.CurrentRow.Cells(colSP_Loaction_Code).Value = txtConsmLocOther.Value
            Dim convFactMul As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), Nothing)
            Dim convFactDiv As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOMBag).Value), Nothing)
            gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty_Bag).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value) * convFactMul, convFactDiv)

            'If clsCommon.myLen(txtplancode.Value) > 0 AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colUnit).Value) > 0 Then
            'Else
            '    gv.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            'End If

            'If clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value) <= 0 Then
            '    gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select prod_quantity from tspl_pp_bom_head where bom_code='" + bomcode + "'")), DecimalPointQty)
            '    gvBatch.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PROD_ITEM_UNIT_CODE from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            'End If
            If clsCommon.myLen(icode) <= 0 Then '----------when no item fill in grid from planning then item detail fill from bom else item detail filled by planned remains same
                'gvBatch.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
                'gvBatch.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value) + "'"))
                'gvBatch.CurrentRow.Cells(colItype).Value = ItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'")))
                'gvBatch.CurrentRow.Cells(colFAT_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvBatch.CurrentRow.Cells(colItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
                'gvBatch.CurrentRow.Cells(colSNF_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvBatch.CurrentRow.Cells(colItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
                'gvBatch.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
                'gvBatch.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
                'Else
                '    gvBatch.CurrentRow.Cells(colFAT_Per).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value))
                '    gvBatch.CurrentRow.Cells(colSNF_Per).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value))
            End If
            'gvBatch.CurrentRow.Cells(colSectionCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select section_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            'gvBatch.CurrentRow.Cells(colSectionName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colSection).Value) + "'"))

            AllBomCode = AllBomCode + "','" + bomcode
        Else
            gvBatch.CurrentRow.Cells(colBOMCode).Value = ""
            'gvBatch.CurrentRow.Cells(colBOMDesc).Value = ""
            If clsCommon.myLen(icode) <= 0 Then
                gvBatch.CurrentRow.Cells(colItemCode).Value = ""
                gvBatch.CurrentRow.Cells(colItemDesc).Value = ""
                'gvBatch.CurrentRow.Cells(colItype).Value = ""
                gvBatch.CurrentRow.Cells(colUOM).Value = ""
                gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value = 0
            End If
            'gv.CurrentRow.Cells(colSection).Value = ""
            'gv.CurrentRow.Cells(colSectionName).Value = ""

            AllBomCode = "" 'AllBomCode.Replace(oldbomcode, "")
        End If
    End Sub
    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        Dim uom As String = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value)
        Dim icode As String = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        uom = clsCommon.myCstr(clsCommon.ShowSelectForm("PPBUOM", qry, "Code", " tspl_item_uom_detail.item_code='" + icode + "'", uom, "Code", isButtonClicked))
        gvBatch.CurrentRow.Cells(colUOM).Value = uom

        'gvBatch.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
        'gvBatch.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
    End Sub
    Private Sub OpenUOMRM(ByVal isButtonClicked As Boolean)
        Dim uom As String = clsCommon.myCstr(gvConsumption.CurrentRow.Cells(colUOM).Value)
        Dim icode As String = clsCommon.myCstr(gvConsumption.CurrentRow.Cells(colItemCode).Value)

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        uom = clsCommon.myCstr(clsCommon.ShowSelectForm("PPBUOMConsm", qry, "Code", " tspl_item_uom_detail.item_code='" + icode + "'", uom, "Code", isButtonClicked))
        gvConsumption.CurrentRow.Cells(colUOM).Value = uom

        gvConsumption.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
        gvConsumption.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
    End Sub
    Sub OpenShiftCode(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select shift_code as Code,shift_name as Description,from_time as [From Time],to_time as [To Time],interval_time as [Interval Time],fsthalf_adjust_min as [First Half Adjustment],sechalf_adjust_min as [Second Half Adjustment] from tspl_shift_master"
        Dim shiftcode As String = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colShiftCode).Value)
        shiftcode = clsCommon.ShowSelectForm("PPSFTFND", qry, "Code", "", shiftcode, "Code", isButtonClicked)

        If shiftcode IsNot Nothing AndAlso clsCommon.myLen(shiftcode) > 0 Then
            gvBatch.CurrentRow.Cells(colShiftCode).Value = shiftcode
            'gvBatch.CurrentRow.Cells(colShiftDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name from tspl_shift_master where shift_code='" + shiftcode + "'"))
        Else
            gvBatch.CurrentRow.Cells(colShiftCode).Value = ""
            'gv.CurrentRow.Cells(colShiftDesc).Value = ""
        End If
    End Sub
    Private Sub gvBatch_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvBatch.CurrentColumnChanged
        If gvBatch.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBatch.CurrentRow.Index
            gvBatch.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gvBatch.Rows.Count - 1 Then
                gvBatch.Rows.AddNew()
                gvBatch.CurrentRow = gvBatch.Rows(intCurrRow)
                'gvBatch.CurrentRow.Cells(colSectionCode).Value = fndSection.Value
            End If
        End If
    End Sub
    Private Sub UpdateCurrentRow(ByVal RowNo As Integer)
        Dim convFactMul As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvBatch.Rows(RowNo).Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.Rows(RowNo).Cells(colUOMBag).Value), Nothing)
        Dim convFactDiv As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvBatch.Rows(RowNo).Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.Rows(RowNo).Cells(colUOM).Value), Nothing)
        gvBatch.Rows(RowNo).Cells(colFINAL_PROD_Qty).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvBatch.Rows(RowNo).Cells(colFINAL_PROD_Qty_Bag).Value) * convFactMul, convFactDiv)
    End Sub



    Private Sub gvConsumption_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvConsumption.CurrentColumnChanged
        If gvConsumption.RowCount > 0 AndAlso 2 = 3 Then
            Dim intCurrRow As Integer = gvConsumption.CurrentRow.Index
            gvConsumption.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gvConsumption.Rows.Count - 1 Then
                gvConsumption.Rows.AddNew()
                gvConsumption.CurrentRow = gvConsumption.Rows(intCurrRow)
                If gvBatch.Rows.Count > 0 Then
                    gvConsumption.CurrentRow.Cells(colMainItemCode).Value = gvBatch.Rows(0).Cells(colItemCode).Value
                    gvConsumption.CurrentRow.Cells(colMainItemDesc).Value = gvBatch.Rows(0).Cells(colItemDesc).Value

                    gvConsumption.CurrentRow.Cells(colBOMCode).Value = gvBatch.Rows(0).Cells(colBOMCode).Value
                    gvConsumption.CurrentRow.Cells(colBOMDesc).Value = clsBOM.GetBOMDesc(gvConsumption.CurrentRow.Cells(colBOMCode).Value, Nothing)

                    gvConsumption.CurrentRow.Cells(colMainUOM).Value = gvBatch.Rows(0).Cells(colUOM).Value
                    gvConsumption.CurrentRow.Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(gvBatch.Rows(0).Cells(colUOM).Value, Nothing)
                End If

                'gvBatch.CurrentRow.Cells(colSectionCode).Value = fndSection.Value
            End If
        End If
    End Sub
    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvBatch.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub
    Sub InitialLoadAllGrid(ByVal objBOM As clsBOM)
        Try
            'If clsCommon.myLen(txtBatchNo.Text) <= 0 Then
            '    txtBatchNo.Focus()
            '    Throw New Exception("Select Main Batch order first.")
            'End If

            'FillIssueAgainstBatchOrder()
            'FillStageDetail()
            'ShowBatchItems(objBOM)
            'FillQCGrid()
            'LoadBlankWreckageGrid()
            'FillSection()
            'LoadBlankScrapGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

        'If clsCommon.CompairString(QC_Type, "Wreckage") = CompairStringResult.Equal Then
        '    '' update fat/snf in add/remove tab
        '    For Each grow As GridViewRowInfo In gvWreckage.Rows
        '        If clsCommon.CompairString(grow.Cells(colWFItem_Code).Value, Item_Code) = CompairStringResult.Equal Then
        '            If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
        '                grow.Cells(colWFAvail_FAT_Per).Value = Value
        '                grow.Cells(colWFAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCDecimal(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCDecimal(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCDecimal(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_FAT_Per).Value, Nothing)
        '            ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
        '                grow.Cells(colWFAvail_SNF_Per).Value = Value
        '                grow.Cells(colWFAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colWFUnit_Code).Value, IIf(clsCommon.myCDecimal(grow.Cells(colWFBACK_QTY).Value) > 0, clsCommon.myCDecimal(grow.Cells(colWFBACK_QTY).Value), clsCommon.myCDecimal(grow.Cells(colWFWRECKAGE_QTY).Value)), grow.Cells(colWFAvail_SNF_Per).Value, Nothing)
        '            End If
        '            'Exit Sub
        '        End If
        '    Next
        'End If ''end cond.

        'If clsCommon.CompairString(QC_Type, "Scrap") = CompairStringResult.Equal Then
        '    '' update fat/snf in add/remove tab
        '    For Each grow As GridViewRowInfo In GvScrap.Rows
        '        If clsCommon.CompairString(grow.Cells(colScrapItem_Code).Value, Item_Code) = CompairStringResult.Equal Then
        '            If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
        '                grow.Cells(colScrapAvail_FAT_Per).Value = Value
        '                grow.Cells(colScrapAvail_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colScrapUnit_Code).Value, IIf(clsCommon.myCDecimal(grow.Cells(colScrapQty).Value) > 0, clsCommon.myCDecimal(grow.Cells(colScrapQty).Value), clsCommon.myCDecimal(grow.Cells(colScrapQty).Value)), grow.Cells(colScrapAvail_FAT_Per).Value, Nothing)
        '            ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
        '                grow.Cells(colScrapAvail_SNF_Per).Value = Value
        '                grow.Cells(colScrapAvail_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colScrapUnit_Code).Value, IIf(clsCommon.myCDecimal(grow.Cells(colScrapQty).Value) > 0, clsCommon.myCDecimal(grow.Cells(colScrapQty).Value), clsCommon.myCDecimal(grow.Cells(colScrapQty).Value)), grow.Cells(colScrapAvail_SNF_Per).Value, Nothing)
        '            End If
        '            'Exit Sub
        '        End If
        '    Next
        'End If ''end cond.

    End Sub
    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_SPP_PRODUCTION_ENTRY where Posted='0' and PROD_ENTRY_CODE='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                RadPageView1.SelectedPage = pageBatchProduction
                Throw New Exception("Current document is not posted.")
            End If

            'qry = "select count(*) from TSPL_PP_STAGE_PROCESS_HEAD where issue_code='" + txtCode.Value + "'"
            'check = clsDBFuncationality.getSingleValue(qry)
            'If check > 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    Throw New Exception("Cannot unpost document,is used in Stage Process.")
            'End If

            'qry = "select count(*) from TSPL_PP_SP_ISSUE_ITEM_DETAIL where issue_code='" + txtCode.Value + "'"
            'check = clsDBFuncationality.getSingleValue(qry)
            'If check > 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    Throw New Exception("Cannot unpost document,is used in Standardization.")
            'End If

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


                If clsStanderdProductionEntry.UnpostData(txtCode.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        'btnunpost.Visible = False
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub FillRawItemGridFromBOM(ByVal import As Boolean)
        Dim objlist As New List(Of clsBOMRecursiveitems)
        Dim BOMList As New List(Of String)
        gvConsumption.Rows.Clear()
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colUOM).Value) > 0 Then
                objlist = New List(Of clsBOMRecursiveitems)
                clsBOMRecursiveitems.GetItemOfBOM(objlist, grow.Cells(colItemCode).Value, grow.Cells(colFINAL_PROD_Qty).Value, grow.Cells(colUOM).Value, "", "", dtpDate.Value, Nothing, 1, True, grow.Cells(colBOMCode).Value)
                If objlist IsNot Nothing AndAlso objlist.Count > 0 Then
                    For Each objtr As clsBOMRecursiveitems In objlist
                        gvConsumption.Rows.AddNew()
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colLineNo).Value = gvConsumption.Rows.Count
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainItemCode).Value = grow.Cells(colItemCode).Value
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainItemDesc).Value = clsItemMaster.GetItemName(grow.Cells(colItemCode).Value, Nothing)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colBOMCode).Value = grow.Cells(colBOMCode).Value
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colBOMDesc).Value = GetBOMDesc(grow.Cells(colBOMCode).Value, Nothing)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainUOM).Value = grow.Cells(colUOM).Value
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(grow.Cells(colUOM).Value, Nothing)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colItemCode).Value = objtr.ITEM_CODE
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colItemDesc).Value = clsItemMaster.GetItemName(objtr.ITEM_CODE, Nothing)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colProductType).Value = clsItemMaster.GetItemProductType(objtr.ITEM_CODE, Nothing)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colUOM).Value = objtr.UNIT_CODE
                        'gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(objtr.UNIT_CODE, Nothing)
                        Dim Product_Type As String = clsItemMaster.GetItemProductType(objtr.ITEM_CODE, Nothing)
                        Dim BalanceQty As Decimal = 0
                        If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                            BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(objtr.ITEM_CODE), clsLocation.GetMainLocationMilk(clsCommon.myCstr(txtConsmLocOther.Value), Nothing), clsCommon.myCstr(txtConsmLocOther.Value), txtCode.Value, dtpDate.Value, Nothing, clsCommon.myCstr(objtr.UNIT_CODE))
                        Else
                            BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(objtr.ITEM_CODE), clsCommon.myCstr(txtConsmLocOther.Value), txtCode.Value, dtpDate.Value, Nothing, clsCommon.myCstr(objtr.UNIT_CODE), 0)
                        End If

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Stock).Value = BalanceQty
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Min).Value = objtr.QUANTITY - (objtr.QUANTITY * SettConsumptionQtyTollerance / 100)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Max).Value = objtr.QUANTITY + (objtr.QUANTITY * SettConsumptionQtyTollerance / 100)

                        Dim IsItemRounding As Boolean = clsItemMaster.IsItemRounding(objtr.ITEM_CODE, Nothing)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = IIf(IsItemRounding, Math.Ceiling(objtr.QUANTITY), objtr.QUANTITY)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colConsumptionQtyOriginal).Value = clsCommon.myCDecimal(gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_Per).Value = 0 'objtr.FAT
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_KG).Value = 0 'objtr.FAT_KG

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Per).Value = 0 'objtr.SNF
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_KG).Value = 0 'objtr.SNF_KG

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_KG).Value = 0 'clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.FAT, Nothing)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_KG).Value = 0 'clsBOM.GetFatSNFKG_AfterConversion(objtr.ITEM_CODE, objtr.UNIT_CODE, objtr.QUANTITY, objtr.SNF, Nothing)
                        '' cost details
                        'Dim Product_Type As String = clsItemMaster.GetItemProductType(objtr.ITEM_CODE, Nothing)
                        If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_Per).ReadOnly = False
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Per).ReadOnly = False
                        Else
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFAT_Per).ReadOnly = True
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Per).ReadOnly = True
                        End If
                        If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = "" 'txtConsmLocMilk.Value
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = "" 'lblConsmLocMilkDesc.Text
                        Else
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = txtConsmLocOther.Value
                            gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = lblConsmLocOtherDesc.Text
                        End If
                        '' check stock for import only
                        If import Then
                            If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Product_Type, "MP") <> CompairStringResult.Equal Then
                                gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = objtr.QUANTITY
                                Dim availQty As Decimal = clsItemLocationDetails.getBalance(objtr.ITEM_CODE, clsCommon.myCstr(gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Code).Value), txtCode.Value, dtpDate.Value, Nothing, gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colUOM).Value, 0)
                                If objtr.QUANTITY > availQty Then
                                    gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = availQty
                                End If
                            End If
                        End If

                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost(Product_Type, objtr.ITEM_CODE, gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSP_Loaction_Code).Value, objtr.QUANTITY, objtr.UNIT_CODE, objtr.FAT_KG, objtr.SNF_KG, dtpDate.Value, clsCommon.GETSERVERDATE(Nothing), False, Nothing)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Rate).Value = 0 ' Math.Round(If(objtr.FAT_KG <= 0, 0, objCost.FAT_Cost / objtr.FAT_KG), 2)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Amt).Value = 0 ' Math.Round(objCost.FAT_Cost, 2)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Rate).Value = 0 ' Math.Round(If(objtr.SNF_KG <= 0, 0, objCost.SNF_Cost / objtr.SNF_KG), 2)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Amt).Value = 0 ' Math.Round(objCost.SNF_Cost, 2)

                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colAVG_Cost).Value = 0 ' Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFIFO_Cost).Value = 0 ' Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
                        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colLIFO_Cost).Value = 0 ' Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
                        setCost(gvConsumption.Rows.Count - 1)
                    Next
                End If
            End If
        Next

    End Sub
    Public Shared Function GetBOMDesc(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "select Description from TSPL_PP_BOM_HEAD where BOM_Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Sub FillCostGridFromBOM(ByVal import As Boolean)
        Dim objlist As New List(Of clsBomCostMappingDetails)
        gvProductionCost.Rows.Clear()
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colUOM).Value) > 0 Then
                objlist = New List(Of clsBomCostMappingDetails)
                objlist = clsStanderdProductionEntry.getBOMCost(grow.Cells(colBOMCode).Value, grow.Cells(colUOM).Value, Nothing)
                If objlist IsNot Nothing AndAlso objlist.Count > 0 Then
                    For Each objtr As clsBomCostMappingDetails In objlist
                        gvProductionCost.Rows.AddNew()
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colLineNo).Value = gvProductionCost.Rows.Count
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemCode).Value = grow.Cells(colItemCode).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemDesc).Value = clsItemMaster.GetItemName(grow.Cells(colItemCode).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMCode).Value = grow.Cells(colBOMCode).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMDesc).Value = GetBOMDesc(grow.Cells(colBOMCode).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOM).Value = grow.Cells(colUOM).Value
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(grow.Cells(colUOM).Value, Nothing)

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemCode).Value = objtr.COST_CODE
                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colItemDesc).Value = objtr.COST_DESC

                        gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colAVG_Cost).Value = Math.Round(clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) * objtr.Overheadcost, 2)

                    Next
                End If
            End If
        Next

    End Sub
    Sub FillHeader(ByVal import As Boolean)
        Dim qry As String
        Try
            qry = "select top 1 Import_Id,Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other from TSPL_SPP_PRODUCTION_IMPORT where Import_Id in (" & clsCommon.GetMulcallStringWithComma(txtImportTemplate.arrValueMember) & ") and Import_Status='N' order by Import_Id"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
            If dt.Rows.Count > 0 Then
                dtpDate.Value = clsCommon.myCDate(dt.Rows(0).Item("Prod_Date"))
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0).Item("Location_Code"))
                lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
                fndItemCategory.Value = clsCommon.myCstr(dt.Rows(0).Item("Category"))
                txtConsmLocMilk.Value = clsCommon.myCstr(dt.Rows(0).Item("Consm_Loc_Milk"))
                lblConsmLocMilkDesc.Text = clsLocation.GetName(txtConsmLocMilk.Value, Nothing)
                txtConsmLocOther.Value = clsCommon.myCstr(dt.Rows(0).Item("Consm_Loc_Other"))
                lblConsmLocOtherDesc.Text = clsLocation.GetName(txtConsmLocOther.Value, Nothing)
                txtImportTemplate.Tag = clsCommon.myCDecimal(dt.Rows(0).Item("Import_Id"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If txtImportTemplate.Enabled AndAlso txtImportTemplate.arrValueMember.Count > 0 Then
                Import = True
                automateImport()
            Else
                FillRawItemGridFromBOM(False)
                FillCostGridFromBOM(False)
                FillCostDetails()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        ClickGo = False
        RadPageView1.SelectedPage = pageConsumption
    End Sub
    Private Sub fndItemCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndItemCategory._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER"
            fndItemCategory.Value = clsCommon.ShowSelectForm("PPPlanSTRFND", qry, "Code", " Structure_Code in (select distinct Structure_Code from item_master)", fndItemCategory.Value, "Code", isButtonClicked)
            TxtCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + fndItemCategory.Value + "'"))

            gvBatch.Rows.Clear()
            gvBatch.Rows.AddNew()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gvConsumption_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvConsumption.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gvConsumption.Columns(colItemCode) Then
                        isCellValueChanged = True
                        gvConsumption.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("", gvConsumption.CurrentRow.Cells(colItemCode).Value, False)
                        gvConsumption.CurrentRow.Cells(colItemDesc).Value = clsItemMaster.GetItemName(gvConsumption.CurrentRow.Cells(colItemCode).Value, Nothing)
                        gvConsumption.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetItemDefaultUnit(gvConsumption.CurrentRow.Cells(colItemCode).Value, Nothing)
                        gvConsumption.CurrentRow.Cells(colProductType).Value = clsItemMaster.GetItemProductType(gvConsumption.CurrentRow.Cells(colItemCode).Value, Nothing)
                        gvConsumption.CurrentRow.Cells(colSP_Loaction_Code).Value = txtConsmLocOther.Value
                        gvConsumption.CurrentRow.Cells(colSP_Loaction_Desc).Value = lblConsmLocOtherDesc.Text
                        setCost(gvConsumption.CurrentRow.Index)
                        FillCostDetails()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gvConsumption.Columns(colUOM) Then
                        isCellValueChanged = True
                        OpenUOMRM(False)
                        setCost(gvConsumption.CurrentRow.Index)
                        FillCostDetails()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gvConsumption.Columns(colFINAL_PROD_Qty) Or e.Column Is gvConsumption.Columns(colAVG_Cost) Or e.Column Is gvConsumption.Columns(colSNF_Per) Then
                        isCellValueChanged = True
                        gvConsumption.Rows(e.RowIndex).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvConsumption.Rows(e.RowIndex).Cells(colItemCode).Value, gvConsumption.Rows(e.RowIndex).Cells(colUOM).Value, gvConsumption.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value, gvConsumption.Rows(e.RowIndex).Cells(colFAT_Per).Value, Nothing)
                        gvConsumption.Rows(e.RowIndex).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvConsumption.Rows(e.RowIndex).Cells(colItemCode).Value, gvConsumption.Rows(e.RowIndex).Cells(colUOM).Value, gvConsumption.Rows(e.RowIndex).Cells(colFINAL_PROD_Qty).Value, gvConsumption.Rows(e.RowIndex).Cells(colSNF_Per).Value, Nothing)
                        gvConsumption.Rows(e.RowIndex).Cells(colFat_Amt).Value = gvConsumption.Rows(e.RowIndex).Cells(colFAT_KG).Value * gvConsumption.Rows(e.RowIndex).Cells(colFat_Rate).Value
                        gvConsumption.Rows(e.RowIndex).Cells(colSNF_Amt).Value = gvConsumption.Rows(e.RowIndex).Cells(colSNF_KG).Value * gvConsumption.Rows(e.RowIndex).Cells(colSNF_Rate).Value
                        gvConsumption.Rows(e.RowIndex).Cells(colAVG_Cost).Value = gvConsumption.Rows(e.RowIndex).Cells(colFat_Amt).Value + gvConsumption.Rows(e.RowIndex).Cells(colSNF_Amt).Value
                        gvConsumption.Rows(e.RowIndex).Cells(colFIFO_Cost).Value = gvConsumption.Rows(e.RowIndex).Cells(colAVG_Cost).Value
                        gvConsumption.Rows(e.RowIndex).Cells(colLIFO_Cost).Value = gvConsumption.Rows(e.RowIndex).Cells(colAVG_Cost).Value
                        setCost(gvConsumption.CurrentRow.Index)
                        FillCostDetails()
                        isCellValueChanged = False
                    End If
                    If e.Column Is gvConsumption.Columns(colSP_Loaction_Code) Then
                        isCellValueChanged = True
                        Dim WhrCls As String = ""
                        Dim productType As String = gvConsumption.CurrentRow.Cells(colProductType).Value
                        If clsCommon.CompairString(productType, "MI") = CompairStringResult.Equal Then
                            WhrCls += "(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "') )"
                        Else
                            WhrCls += " location_type='Physical'"
                        End If
                        gvConsumption.CurrentRow.Cells(colSP_Loaction_Code).Value = clsLocation.getFinder(WhrCls, gvConsumption.CurrentRow.Cells(colSP_Loaction_Code).Value, False)
                        gvConsumption.CurrentRow.Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(gvConsumption.CurrentRow.Cells(colSP_Loaction_Code).Value, Nothing)
                        setCost(gvConsumption.CurrentRow.Index)
                        FillCostDetails()
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub updateConsumptionCost()
        '' cost details
        Dim Product_Type As String = clsItemMaster.GetItemProductType(gvConsumption.CurrentRow.Cells(colItemCode).Value, Nothing)
        Dim objCost As New MIlkComponentType
        objCost = clsInventoryMovementNew.GetAvgCost(Product_Type, gvConsumption.CurrentRow.Cells(colItemCode).Value, gvConsumption.CurrentRow.Cells(colSP_Loaction_Code).Value, gvConsumption.CurrentRow.Cells(colFINAL_PROD_Qty).Value, gvConsumption.CurrentRow.Cells(colUOM).Value, gvConsumption.CurrentRow.Cells(colFAT_KG).Value, gvConsumption.CurrentRow.Cells(colSNF_KG).Value, dtpDate.Value, dtpDate.Value, False, Nothing)

        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Rate).Value = Math.Round(If(gvConsumption.CurrentRow.Cells(colFAT_KG).Value <= 0, 0, objCost.FAT_Cost / gvConsumption.CurrentRow.Cells(colFAT_KG).Value), 2)
        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFat_Amt).Value = Math.Round(objCost.FAT_Cost, 2)

        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Rate).Value = Math.Round(If(gvConsumption.CurrentRow.Cells(colSNF_KG).Value <= 0, 0, objCost.SNF_Cost / gvConsumption.CurrentRow.Cells(colSNF_KG).Value), 2)
        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colSNF_Amt).Value = Math.Round(objCost.SNF_Cost, 2)

        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colAVG_Cost).Value = Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colFIFO_Cost).Value = Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
        gvConsumption.Rows(gvConsumption.Rows.Count - 1).Cells(colLIFO_Cost).Value = Math.Round(objCost.FAT_Cost, 2) + Math.Round(objCost.SNF_Cost, 2)
    End Sub
    Private Sub txtConsmLocMilk__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtConsmLocMilk._MYValidating
        Dim WhrCls As String = "" '" Location_Type='Physical' "       
        WhrCls += "(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "') )"
        txtConsmLocMilk.Value = clsLocation.getFinder(WhrCls, txtConsmLocMilk.Value, isButtonClicked)
        lblConsmLocMilkDesc.Text = clsLocation.GetName(txtConsmLocMilk.Value, Nothing)

    End Sub
    Private Sub txtConsmLocOther__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtConsmLocOther._MYValidating
        'Dim WhrCls As String = "" '" Location_Type='Physical' "      
        'WhrCls += "(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtLocation.Value & "') or Location_Code='" & txtLocation.Value & "' )"
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += "  and  Location_Code in (" + arrLoc + ")"
        End If
        txtConsmLocOther.Value = clsLocation.getFinder(WhrCls, txtConsmLocOther.Value, isButtonClicked)
        lblConsmLocOtherDesc.Text = clsLocation.GetName(txtConsmLocOther.Value, Nothing)

    End Sub
    Sub FillBatchTab(ByVal import As Boolean)
        Dim icode As String = ""
        Dim whrCls As String = ""
        Dim bomcode As String = ""
        Dim qry As String = ""
        If import = True Then
            qry = "select Import_Id,Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other,BOM_Code,Item_Code,Item_Desc,UOM,Prod_Qty from TSPL_SPP_PRODUCTION_IMPORT where Import_Status='N' "
            qry = qry & " and Prod_Date='" & clsCommon.GetPrintDate(dtpDate.Value, "dd-MMM-yyyy") & "'"
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                qry = qry & " and Location_Code='" & txtLocation.Value & "'"
            End If
            If clsCommon.myLen(fndItemCategory.Value) > 0 Then
                qry = qry & " and Category='" & fndItemCategory.Value & "'"
            End If

            If clsCommon.myLen(txtLocation.Value) > 0 Then
                qry = qry & " and Consm_Loc_Milk='" & txtConsmLocMilk.Value & "'"
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                qry = qry & " and Consm_Loc_Other='" & txtConsmLocOther.Value & "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
            gvBatch.Rows.Clear()
            For Each dr As DataRow In dt.Rows
                gvBatch.Rows.AddNew()
                gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Code).Value = txtLocation.Value
                gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(txtLocation.Value, Nothing)
                gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(dr.Item("BOM_Code"))
                'gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colBOMDesc).Value = clsBOM.GetBOMDesc(clsCommon.myCstr(dr.Item("BOM_Code")), Nothing)
                gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dr.Item("UOM"))
                gvBatch.Rows(gvBatch.Rows.Count - 1).Cells(colFINAL_PROD_Qty).Value = clsCommon.myCDecimal(dr.Item("Prod_Qty"))
            Next
        End If
        For Each grow As GridViewRowInfo In gvBatch.Rows
            bomcode = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
            If clsCommon.myLen(bomcode) > 0 Then
                icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                If clsCommon.myLen(icode) <= 0 Then
                    '' update item and other fields of the batch grid
                    qry = " select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.STD_FatPer,TSPL_ITEM_MASTER.STD_SNFPer,TSPL_PP_BOM_HEAD.PROD_QUANTITY " &
                          " from TSPL_PP_BOM_HEAD left join TSPL_ITEM_MASTER on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=TSPL_ITEM_MASTER.Item_Code " &
                          " where TSPL_PP_BOM_HEAD.BOM_CODE='" & bomcode & "' and  TSPL_PP_BOM_HEAD.Is_Post=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                    If dt.Rows.Count > 0 Then
                        grow.Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0).Item("PROD_ITEM_CODE"))
                        grow.Cells(colItemDesc).Value = clsCommon.myCstr(dt.Rows(0).Item("Item_Desc"))
                        If clsCommon.myLen(grow.Cells(colUOM).Value) <= 0 Then
                            grow.Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0).Item("PROD_ITEM_UNIT_CODE"))
                        End If
                        If clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value) <= 0 Then
                            grow.Cells(colFINAL_PROD_Qty).Value = clsCommon.myCDecimal(dt.Rows(0).Item("PROD_QUANTITY"))
                        End If
                        If clsCommon.myLen(grow.Cells(colSP_Loaction_Code).Value) <= 0 Then
                            grow.Cells(colSP_Loaction_Code).Value = txtLocation.Value
                            grow.Cells(colSP_Loaction_Desc).Value = clsLocation.GetName(txtLocation.Value, Nothing)
                        End If
                        grow.Cells(colFAT_Per).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0).Item("STD_FatPer")), 2)
                        grow.Cells(colSNF_Per).Value = Math.Round(clsCommon.myCDecimal(dt.Rows(0).Item("STD_SNFPer")), 2)
                        'grow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colFAT_Per).Value), Nothing)
                        'grow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colSNF_Per).Value), Nothing)
                    End If
                End If
                grow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colFAT_Per).Value), Nothing)
                grow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(grow.Cells(colItemCode).Value), clsCommon.myCstr(grow.Cells(colUOM).Value), clsCommon.myCDecimal(grow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(grow.Cells(colSNF_Per).Value), Nothing)
            End If
            grow.Cells(colLineNo).Value = grow.Index + 1
        Next

        ''icode = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value)
        ''Dim sectionCondition As String = ""

        ''If clsCommon.myLen(fndItemCategory.Value) > 0 Then
        ''    sectionCondition = " and tspl_pp_bom_head.ITEM_CATEGORY_CODE='" + fndItemCategory.Value + "' "
        ''End If

        ''If clsCommon.myLen(icode) > 0 Then
        ''    whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + icode + "' and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        ''Else
        ''    whrCls = " isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and tspl_item_master.item_type in ('F','S') and isnull(TSPL_PP_BOM_HEAD.is_post,'0')='1' " + sectionCondition + " "
        ''End If

        'Dim oldbomcode As String = clsCommon.myCstr(gvBatch.CurrentRow.Cells(colBOMCode).Value)
        'bomcode = clsBOM.GetBOMFinder(whrCls, oldbomcode, isButtonClicked)

        'If clsCommon.myLen(bomcode) > 0 Then
        '    gvBatch.CurrentRow.Cells(colBOMCode).Value = bomcode
        '    'gvBatch.CurrentRow.Cells(colBOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
        '    'If clsCommon.myLen(txtplancode.Value) > 0 AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colUnit).Value) > 0 Then
        '    'Else
        '    '    gv.CurrentRow.Cells(colUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
        '    'End If

        '    If clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value) <= 0 Then
        '        gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select prod_quantity from tspl_pp_bom_head where bom_code='" + bomcode + "'")), DecimalPointQty)
        '        gvBatch.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PROD_ITEM_UNIT_CODE from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
        '    End If
        '    If clsCommon.myLen(icode) <= 0 Then '----------when no item fill in grid from planning then item detail fill from bom else item detail filled by planned remains same
        '        gvBatch.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
        '        gvBatch.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value) + "'"))
        '        'gvBatch.CurrentRow.Cells(colItype).Value = ItemType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'")))
        '        gvBatch.CurrentRow.Cells(colFAT_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvBatch.CurrentRow.Cells(colItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='FAT'")), 2)
        '        gvBatch.CurrentRow.Cells(colSNF_Per).Value = Math.Round(clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.item_code='" + gvBatch.CurrentRow.Cells(colItemCode).Value + "' and TSPL_PARAMETER_MASTER.Type='SNF'")), 2)
        '        gvBatch.CurrentRow.Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFAT_Per).Value), Nothing)
        '        gvBatch.CurrentRow.Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBatch.CurrentRow.Cells(colUOM).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value), clsCommon.myCDecimal(gvBatch.CurrentRow.Cells(colSNF_Per).Value), Nothing)
        '    Else
        '        gvBatch.CurrentRow.Cells(colFAT_Per).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value))
        '        gvBatch.CurrentRow.Cells(colSNF_Per).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(gvBatch.CurrentRow.Cells(colItemCode).Value))
        '    End If
        '    gvBatch.CurrentRow.Cells(colSectionCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select section_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
        '    'gvBatch.CurrentRow.Cells(colSectionName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colSection).Value) + "'"))

        '    AllBomCode = AllBomCode + "','" + bomcode
        'Else
        '    gvBatch.CurrentRow.Cells(colBOMCode).Value = ""
        '    'gvBatch.CurrentRow.Cells(colBOMDesc).Value = ""
        '    If clsCommon.myLen(icode) <= 0 Then
        '        gvBatch.CurrentRow.Cells(colItemCode).Value = ""
        '        gvBatch.CurrentRow.Cells(colItemDesc).Value = ""
        '        'gvBatch.CurrentRow.Cells(colItype).Value = ""
        '        gvBatch.CurrentRow.Cells(colUOM).Value = ""
        '        gvBatch.CurrentRow.Cells(colFINAL_PROD_Qty).Value = 0
        '    End If
        '    'gv.CurrentRow.Cells(colSection).Value = ""
        '    'gv.CurrentRow.Cells(colSectionName).Value = ""

        '    AllBomCode = AllBomCode.Replace(oldbomcode, "")
        'End If
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            Dim qryExport As String
            qryExport = " select 1 as  [Seq No],'" & clsCommon.GetPrintDate(Today, "dd-MMM-yyyy") & "' as 	[Prod Date],'261' as [Location Code],'' as Category,'SILO-261' as [Consm Loc Milk],'' as [Consm Loc Other],'' as [BOM Code],'' as [Item Code],'' as [Item Desc],'' as [UOM],0 as [Prod Qty] "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Blank Sheet Export for Production")
        End Try
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        '' done by panch raj against ticket No: BM00000008191,BM00000008189
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        'Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Seq No", "Prod Date", "Location Code", "Category", "Consm Loc Milk", "Consm Loc Other", "BOM Code", "Item Code", "Item Desc", "UOM", "Prod Qty") Then
            'Dim trans As System.Data.SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin
            Try
                Dim objList As New List(Of clsStanderdProductionEntryImportTemplate)
                Dim LastImportId As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select max(Import_Id) from TSPL_SPP_PRODUCTION_IMPORT where Import_Status='N'"))
                '' get list of adjustments
                Dim objtr As New clsStanderdProductionEntryImportTemplate
                For Each grow As GridViewRowInfo In gv.Rows
                    objtr = New clsStanderdProductionEntryImportTemplate
                    objtr.Seq_No = clsCommon.myCDecimal(grow.Cells("Seq No").Value)
                    If IsDBNull(grow.Cells("Prod Date")) = False AndAlso clsCommon.myLen(grow.Cells("Prod Date")) > 0 Then
                        objtr.Prod_Date = clsCommon.myCDate(grow.Cells("Prod Date").Value)
                    Else
                        Throw New Exception("Prod Date is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myLen(grow.Cells("Location Code").Value) > 0 Then
                        objtr.Location_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                        '' check valid location
                        If clsLocation.CheckCode(objtr.Location_Code, Nothing) = False Then
                            Throw New Exception("Invalid Location Code at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("Location Code is blank at line no- " & grow.Index + 1 & "")
                    End If

                    objtr.Category = clsCommon.myCstr(grow.Cells("Category").Value)

                    If clsCommon.myLen(grow.Cells("Consm Loc Milk").Value) > 0 Then
                        objtr.Consm_Loc_Milk = clsCommon.myCstr(grow.Cells("Consm Loc Milk").Value)
                        '' check valid location
                        If clsLocation.CheckCode(objtr.Consm_Loc_Milk, Nothing) = False Then
                            Throw New Exception("Invalid Consm Loc Milk at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("Consm Loc Milk is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myLen(grow.Cells("Consm Loc Other").Value) > 0 Then
                        objtr.Consm_Loc_Other = clsCommon.myCstr(grow.Cells("Consm Loc Other").Value)
                        '' check valid location
                        If clsLocation.CheckCode(objtr.Consm_Loc_Other, Nothing) = False Then
                            Throw New Exception("Invalid Consm Loc Other at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("Consm Loc Other is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myLen(grow.Cells("BOM Code").Value) > 0 Then
                        objtr.BOM_Code = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                        '' check valid code
                        If clsBOM.CheckCode(objtr.BOM_Code, Nothing) = False Then
                            Throw New Exception("Invalid BOM Code at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("BOM Code is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myLen(grow.Cells("Item Code").Value) > 0 Then
                        objtr.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)

                        '' check valid code
                        If clsItemMaster.CheckItemCode(objtr.Item_Code, Nothing) = False Then
                            Throw New Exception("Invalid Item Code at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("Item Code is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myLen(grow.Cells("UOM").Value) > 0 Then
                        objtr.UOM = clsCommon.myCstr(grow.Cells("UOM").Value)
                        '' check valid code
                        If clsUOMInfo.CheckUnitCode(objtr.UOM, Nothing) = False Then
                            Throw New Exception("Invalid UOM at line no- " & grow.Index + 1 & "")
                        End If
                    Else
                        Throw New Exception("UOM is blank at line no- " & grow.Index + 1 & "")
                    End If
                    If clsCommon.myCDecimal(grow.Cells("Prod Qty").Value) > 0 Then
                        objtr.Prod_Qty = clsCommon.myCstr(grow.Cells("Prod Qty").Value)
                    Else
                        Throw New Exception("Prod Qty is blank at line no- " & grow.Index + 1 & "")
                    End If
                    objList.Add(objtr)
                Next
                clsStanderdProductionEntryImportTemplate.SaveData(objList)
                updateImportControl()
                If clsCommon.MyMessageBoxShow("Do yo want to create Docs right now ?", "Production Import", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If txtImportTemplate.Enabled Then
                        Dim qry As String = "select * from ( select MAX(Import_Id) as Import_Id, Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other  from TSPL_SPP_PRODUCTION_IMPORT " &
                                            " where Import_Status='N' and Import_Id > " & LastImportId & "  group by Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other) as Import order by Import_Id "
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                        Dim arr As New ArrayList
                        For Each dr As DataRow In dt.Rows
                            arr.Add(dr.Item("Import_Id"))
                        Next
                        txtImportTemplate.arrValueMember = arr
                        automateImport()


                        'Import = True
                        'FillHeader(True)
                        'FillBatchTab(True)
                        'FillRawItemGridFromBOM(True)
                        'FillCostGridFromBOM(True)
                    End If
                End If

                'RadMessageBox.Show("Data Transfer Completed! Kindly press Go Button", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try

        End If
    End Sub
    Sub automateImport()
        clsCommon.ProgressBarPercentShow()
        Try
            Import = True
            Dim arr As ArrayList = txtImportTemplate.arrValueMember
            Dim intloop As Integer = 0
            For Each ImportId As Integer In arr
                intloop = intloop + 1
                funReset()
                FillHeader(True)
                FillBatchTab(True)
                FillRawItemGridFromBOM(True)
                'FillCostGridFromBOM(True)
                ClickGo = False
                SaveData(False)
                clsCommon.ProgressBarPercentUpdate((intloop) * 100 / arr.Count, "document- " & txtCode.Value & "")
            Next
            updateImportControl()
            clsCommon.ProgressBarPercentHide()

            clsCommon.MyMessageBoxShow("Documents generated Successfully")
            Import = False
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub updateImportControl()
        If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SPP_PRODUCTION_IMPORT where Import_Status='N'")) > 0 Then
            txtImportTemplate.Enabled = True
        Else
            txtImportTemplate.Enabled = False
            txtImportTemplate.arrValueMember = Nothing
            Import = False
        End If
    End Sub
    Private Sub txtImportTemplate__My_Click(sender As Object, e As EventArgs) Handles txtImportTemplate._My_Click
        Dim qry As String = " select MAX(Import_Id) as Import_Id, Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other  from TSPL_SPP_PRODUCTION_IMPORT " &
                            " where Import_Status='N' group by Prod_Date,Location_Code,Category,Consm_Loc_Milk,Consm_Loc_Other "
        txtImportTemplate.arrValueMember = clsCommon.ShowMultipleSelectForm("ProdImport", qry, "Import_Id", "Prod_Date", txtImportTemplate.arrValueMember, txtImportTemplate.arrDispalyMember)
    End Sub
    Sub setCost(ByVal index As Integer)
        Dim dblCostMethod As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(gvConsumption.Rows(index).Cells(colItemCode).Value) & "' "))
        If dblCostMethod <> 0 Then
            gvConsumption.Rows(index).Cells(colAVG_Cost).Value = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(gvConsumption.Rows(index).Cells(colItemCode).Value), txtConsmLocOther.Value, clsCommon.myCDecimal(gvConsumption.Rows(index).Cells(colFINAL_PROD_Qty).Value), dtpDate.Value, dtpDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(gvConsumption.Rows(index).Cells(colUOM).Value))
        Else
            gvConsumption.Rows(index).Cells(colAVG_Cost).Value = 0
        End If
    End Sub
    Sub FillCostDetails()
        gvProductionCost.Rows.Clear()
        For Each grow As GridViewRowInfo In gvBatch.Rows
            If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 Then
                Dim strBomCode As String = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                Dim strBomDesc As String = clsDBFuncationality.getSingleValue(" select DESCRIPTION from TSPL_MF_BOM_head where BOM_CODE = '" + strBomCode + "' ") 'clsCommon.myCstr(grow.Cells(colBOMDesc).Value)
                Dim strMainItemCode As String = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                Dim strMainItemDesc As String = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                Dim strMainItemUOM As String = clsCommon.myCstr(grow.Cells(colUOM).Value)

                gvProductionCost.Rows.AddNew()
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colLineNo).Value = gvProductionCost.Rows.Count
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemCode).Value = strMainItemCode
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainItemDesc).Value = strMainItemDesc

                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMCode).Value = strBomCode
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colBOMDesc).Value = strBomDesc

                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOM).Value = strMainItemUOM
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colMainUOMDesc).Value = clsUOMInfo.GetUnitDesc(strMainItemUOM, Nothing)

                Dim dblAmount As Decimal = 0.0
                For Each growCons As GridViewRowInfo In gvConsumption.Rows
                    If clsCommon.CompairString(strMainItemCode, clsCommon.myCstr(growCons.Cells(colMainItemCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strBomCode, clsCommon.myCstr(growCons.Cells(colBOMCode).Value)) = CompairStringResult.Equal Then
                        dblAmount += clsCommon.myCDecimal(growCons.Cells(colAVG_Cost).Value)
                    End If
                Next
                gvProductionCost.Rows(gvProductionCost.Rows.Count - 1).Cells(colAVG_Cost).Value = Math.Round(dblAmount, 2)

            End If
        Next

    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If (myMessages.ReCreateJEConfirm()) Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Production Entry", dtpDate.Value) = False Then
                    Exit Sub
                End If
                clsStanderdProductionEntry.ReCreateJE(Form_ID, txtCode.Value, arrLoc, True)
                common.clsCommon.MyMessageBoxShow("Successfully JE Recreate")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub
End Class
