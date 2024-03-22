Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmManufacturingOrder
    Inherits FrmMainTranScreen
    '' component grid columns
    Const colLineNo As String = "LineNo"
    Const colItemCategoryCode As String = "ItemCategoryCode"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colItemType As String = "colItemType"
    Const colqty As String = "Qty"
    Const colBOMqty As String = "colBOMqty"
    Const colStockqty As String = "colStockqty"
    Const colUnitCode As String = "UnitCode"
    Const colscrap_per As String = "Scrap_per"
    Const colwastage_per As String = "Wastage_per"
    Const colRemarks As String = "Remarks"

    '' cost grid columns
    Const colCostType As String = "colCostType"
    Const colStdCost As String = "colStdCost"
    Const colProjectedCost As String = "colProjectedCost"
    Const colStdProjVar As String = "colStdProjVar"
    Const colActualCost As String = "colActualCost"
    Const colStdActualVar As String = "colStdActualVar"


    '' cost grid row no
    Const rowDirectMaterialCost As Integer = 0
    Const rowPackageingCost As Integer = 1
    Const rowSetupLaborCost As Integer = 2
    Const rowDirectLaborCost As Integer = 3
    Const rowOverheadCost As Integer = 4
    Const rowSubContractCost As Integer = 5
    Const rowToolCost As Integer = 6
    Const rowTotal As Integer = 7



    '' operation grid columns
    Const colOperationLineNo As String = "colOperationLineNo"
    Const colOperationCode As String = "colOperationCode"
    Const colOperationDesc As String = "colOperationDesc"
    Const colworkCenterCode As String = "colworkCenterCode"
    Const colProjectedSetupTimeMin As String = "colProjectedSetupTimeMin"
    Const colProjectedRunTimeMin As String = "colProjectedRunTimeMin"
    Const colProjectedCleanTimeMin As String = "colProjectedCleanTimeMin"
    Const colProjectedWaitTimeMin As String = "colProjectedWaitTimeMin"

    Const colActualSetupTimeMin As String = "colActualSetupTimeMin"
    Const colActualRunTimeMin As String = "colActualRunTimeMin"
    Const colActualCleanTimeMin As String = "colActualCleanTimeMin"
    Const colActualWaitTimeMin As String = "colActualWaitTimeMin"

    Const colStartDate As String = "colStartDate"
    Const colEndDate As String = "colEndDate"
    Const colOperationRemarks As String = "colOperationRemarks"

    '' resource grid columns
    Const colResourceCode As String = "colResourceCode"
    Const colResourceDesc As String = "colResourceDesc"
    Const colResourceRequirQty As String = "colResourceRequirQty"
    Const colResourceUsedQty As String = "colResourceUsedQty"
    Const colResourceUnitCost As String = "colResourceUnitCost"
    Const colResourceStdCost As String = "colResourceStdCost"
    Const colResourceActualCost As String = "colResourceActualCost"
    Const colResourceQtyVarPer As String = "colResourceQtyVarPer"
    Const colResourceCostVarPer As String = "colResourceCostVarPer"
    Const colResourceType As String = "colResourceType"
    Const colResourceUnitCostUom As String = "colResourceUnitCostUom"

    '' tool type grid columns
    Const colToolTypeCode As String = "colToolTypeCode"
    Const colToolTypeDesc As String = "colToolTypeDesc"
    Const colToolRequirQty As String = "colToolRequirQty"
    Const colToolUsedQty As String = "colToolUsedQty"
    Const colToolUnitCost As String = "colToolUnitCost"
    Const colToolStdCost As String = "colToolStdCost"
    Const colToolActualCost As String = "colToolActualCost"
    Const colToolQtyVarPer As String = "colToolQtyVarPer"
    Const colToolCostVarPer As String = "colToolCostVarPer"
    Const colToolUnitCostUom As String = "colToolUnitCostUom"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True

    Dim obj As New clsManufacturingOrder
    'Private ObjList As New List(Of clsManufacturingOrder)
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoad As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
    Sub LoadCostGridColumns()

        gvCost.Rows.Clear()
        gvCost.Columns.Clear()
        Dim CostType As New GridViewTextBoxColumn
        Dim StdCost As New GridViewDecimalColumn
        Dim ProjectedCost As New GridViewDecimalColumn
        Dim StdProjVar As New GridViewDecimalColumn
        Dim ActualCost As New GridViewDecimalColumn
        Dim StdActualVar As New GridViewDecimalColumn

        CostType.FormatString = ""
        CostType.HeaderText = "Cost Type"
        CostType.Name = colCostType
        CostType.Width = 150
        CostType.ReadOnly = True
        CostType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvCost.Columns.Add(CostType)

        StdCost.FormatString = ""
        StdCost.HeaderText = "Standard Cost"
        StdCost.Name = colStdCost
        StdCost.Width = 150
        StdCost.ReadOnly = True
        StdCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(StdCost)

        ProjectedCost.FormatString = ""
        ProjectedCost.HeaderText = "Projected Cost"
        ProjectedCost.Name = colProjectedCost
        ProjectedCost.Width = 150
        ProjectedCost.ReadOnly = True
        ProjectedCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(ProjectedCost)

        StdProjVar.FormatString = ""
        StdProjVar.HeaderText = "Variance(%)"
        StdProjVar.Name = colStdProjVar
        StdProjVar.Width = 150
        StdProjVar.ReadOnly = True
        StdProjVar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(StdProjVar)

        ActualCost.FormatString = ""
        ActualCost.HeaderText = "Actual Cost"
        ActualCost.Name = colActualCost
        ActualCost.Width = 150
        ActualCost.ReadOnly = True
        ActualCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(ActualCost)

        StdActualVar.FormatString = ""
        StdActualVar.HeaderText = "Variance(%)"
        StdActualVar.Name = colStdActualVar
        StdActualVar.Width = 150
        StdActualVar.ReadOnly = True
        StdActualVar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(StdActualVar)

        gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowDirectMaterialCost).Cells(colCostType).Value = "Direct Material Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowPackageingCost).Cells(colCostType).Value = "Packaging Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowSetupLaborCost).Cells(colCostType).Value = "Setup Labor Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowDirectLaborCost).Cells(colCostType).Value = "Direct Labor Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowOverheadCost).Cells(colCostType).Value = "Overhead Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowSubContractCost).Cells(colCostType).Value = "Subcontract Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowToolCost).Cells(colCostType).Value = "Tool Cost"

        'gvCost.Rows.Clear()
        gvCost.Rows.AddNew()
        gvCost.Rows(rowTotal).Cells(colCostType).Value = "Total :"


    End Sub

    Sub LoadOperationGridColumns()
        gvOperations.Rows.Clear()
        gvOperations.Columns.Clear()

        Dim OperationLineNo As New GridViewTextBoxColumn
        Dim OperationCode As New GridViewTextBoxColumn
        Dim OperationDesc As New GridViewTextBoxColumn
        Dim workCenterCode As New GridViewTextBoxColumn

        Dim ProjectedSetupTimeMin As New GridViewDecimalColumn
        Dim ProjectedRunTimeMin As New GridViewDecimalColumn
        Dim ProjectedCleanTimeMin As New GridViewDecimalColumn
        Dim ProjectedWaitTimeMin As New GridViewDecimalColumn

        Dim ActualSetupTimeMin As New GridViewDecimalColumn
        Dim ActualRunTimeMin As New GridViewDecimalColumn
        Dim ActualCleanTimeMin As New GridViewDecimalColumn
        Dim ActualWaitTimeMin As New GridViewDecimalColumn

        Dim StartDate As New GridViewDateTimeColumn
        Dim EndDate As New GridViewDateTimeColumn
        Dim OperationRemarks As New GridViewTextBoxColumn

        OperationLineNo.FormatString = ""
        OperationLineNo.HeaderText = "Line No"
        OperationLineNo.Name = colOperationLineNo
        OperationLineNo.Width = 70
        OperationLineNo.ReadOnly = True
        OperationLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(OperationLineNo)

        OperationCode.FormatString = ""
        OperationCode.HeaderText = "Operation Code"
        OperationCode.Name = colOperationCode
        OperationCode.Width = 100
        OperationCode.ReadOnly = True
        OperationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOperations.Columns.Add(OperationCode)

        OperationDesc.FormatString = ""
        OperationDesc.HeaderText = "Description"
        OperationDesc.Name = colOperationDesc
        OperationDesc.Width = 100
        OperationDesc.ReadOnly = True
        OperationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOperations.Columns.Add(OperationDesc)

        workCenterCode.FormatString = ""
        workCenterCode.HeaderText = "Work Center"
        workCenterCode.Name = colworkCenterCode
        workCenterCode.Width = 100
        workCenterCode.ReadOnly = True
        workCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOperations.Columns.Add(workCenterCode)

        ProjectedSetupTimeMin.FormatString = ""
        ProjectedSetupTimeMin.HeaderText = "Projected Setup Time(Minutes)"
        ProjectedSetupTimeMin.Name = colProjectedSetupTimeMin
        ProjectedSetupTimeMin.Width = 120
        ProjectedSetupTimeMin.ReadOnly = True
        ProjectedSetupTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ProjectedSetupTimeMin)

        ProjectedRunTimeMin.FormatString = ""
        ProjectedRunTimeMin.HeaderText = "Projected Run Time(Minutes)"
        ProjectedRunTimeMin.Name = colProjectedRunTimeMin
        ProjectedRunTimeMin.Width = 120
        ProjectedRunTimeMin.ReadOnly = True
        ProjectedRunTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ProjectedRunTimeMin)

        ProjectedCleanTimeMin.FormatString = ""
        ProjectedCleanTimeMin.HeaderText = "Projected Clean Time(Minutes)"
        ProjectedCleanTimeMin.Name = colProjectedCleanTimeMin
        ProjectedCleanTimeMin.Width = 120
        ProjectedCleanTimeMin.ReadOnly = True
        ProjectedCleanTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ProjectedCleanTimeMin)

        ProjectedWaitTimeMin.FormatString = ""
        ProjectedWaitTimeMin.HeaderText = "Projected Wait Time(Minutes)"
        ProjectedWaitTimeMin.Name = colProjectedWaitTimeMin
        ProjectedWaitTimeMin.Width = 120
        ProjectedWaitTimeMin.ReadOnly = True
        ProjectedWaitTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ProjectedWaitTimeMin)

        '' actual 
        ActualSetupTimeMin.FormatString = ""
        ActualSetupTimeMin.HeaderText = "Actual Setup Time(Minutes)"
        ActualSetupTimeMin.Name = colActualSetupTimeMin
        ActualSetupTimeMin.Width = 120
        'ActualSetupTimeMin.ReadOnly = True
        ActualSetupTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ActualSetupTimeMin)

        ActualRunTimeMin.FormatString = ""
        ActualRunTimeMin.HeaderText = "Actual Run Time(Minutes)"
        ActualRunTimeMin.Name = colActualRunTimeMin
        ActualRunTimeMin.Width = 120
        'ActualRunTimeMin.ReadOnly = True
        ActualRunTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ActualRunTimeMin)

        ActualCleanTimeMin.FormatString = ""
        ActualCleanTimeMin.HeaderText = "Actual Clean Time(Minutes)"
        ActualCleanTimeMin.Name = colActualCleanTimeMin
        ActualCleanTimeMin.Width = 120
        'ActualCleanTimeMin.ReadOnly = True
        ActualCleanTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ActualCleanTimeMin)

        ActualWaitTimeMin.FormatString = ""
        ActualWaitTimeMin.HeaderText = "Actual Wait Time(Minutes)"
        ActualWaitTimeMin.Name = colActualWaitTimeMin
        ActualWaitTimeMin.Width = 120
        'ActualWaitTimeMin.ReadOnly = True
        ActualWaitTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(ActualWaitTimeMin)


        StartDate.FormatString = ""
        StartDate.HeaderText = "Start Date"
        StartDate.Name = colStartDate
        StartDate.Width = 100
        'StartDate.ReadOnly = True
        StartDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(StartDate)

        EndDate.FormatString = ""
        EndDate.HeaderText = "End Date"
        EndDate.Name = colEndDate
        EndDate.Width = 100
        'StartDate.ReadOnly = True
        EndDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(EndDate)


        OperationRemarks.FormatString = ""
        OperationRemarks.HeaderText = "Remarks"
        OperationRemarks.Name = colOperationRemarks
        OperationRemarks.Width = 100
        'OperationRemarks.ReadOnly = True
        OperationRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOperations.Columns.Add(OperationRemarks)


    End Sub
    Sub LoadResourceGridColumns()
        gvResources.Rows.Clear()
        gvResources.Columns.Clear()

        Dim ResourceCode As New GridViewTextBoxColumn
        Dim ResourceDesc As New GridViewTextBoxColumn
        Dim ResourceRequirQty As New GridViewDecimalColumn
        Dim ResourceUsedQty As New GridViewDecimalColumn

        Dim ResourceUnitCost As New GridViewDecimalColumn
        Dim ResourceStdCost As New GridViewDecimalColumn
        Dim ResourceActualCost As New GridViewDecimalColumn
        Dim ResourceQtyVarPer As New GridViewDecimalColumn
        Dim ResourceCostVarPer As New GridViewDecimalColumn

        Dim ResourceType As New GridViewTextBoxColumn
        Dim ResourceUnitCostUOM As New GridViewTextBoxColumn

        ResourceCode.FormatString = ""
        ResourceCode.HeaderText = "Resource Code"
        ResourceCode.Name = colResourceCode
        ResourceCode.Width = 100
        ResourceCode.ReadOnly = True
        ResourceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResources.Columns.Add(ResourceCode)

        ResourceDesc.FormatString = ""
        ResourceDesc.HeaderText = "Description"
        ResourceDesc.Name = colResourceDesc
        ResourceDesc.Width = 100
        ResourceDesc.ReadOnly = True
        ResourceDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResources.Columns.Add(ResourceDesc)

        ResourceRequirQty.FormatString = ""
        ResourceRequirQty.HeaderText = "Required Quantity"
        ResourceRequirQty.Name = colResourceRequirQty
        ResourceRequirQty.Width = 100
        'ResourceRequirQty.ReadOnly = True
        ResourceRequirQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceRequirQty)

        ResourceUsedQty.FormatString = ""
        ResourceUsedQty.HeaderText = "Used Quantity"
        ResourceUsedQty.Name = colResourceUsedQty
        ResourceUsedQty.Width = 100
        'ResourceUsedQty.ReadOnly = True
        ResourceUsedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceUsedQty)

        ResourceUnitCost.FormatString = ""
        ResourceUnitCost.HeaderText = "Unit Cost"
        ResourceUnitCost.Name = colResourceUnitCost
        ResourceUnitCost.Width = 100
        ResourceUnitCost.ReadOnly = True
        ResourceUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ResourceUnitCost.IsVisible = False
        gvResources.Columns.Add(ResourceUnitCost)

        ResourceQtyVarPer.FormatString = ""
        ResourceQtyVarPer.HeaderText = "Qty Variance(%)"
        ResourceQtyVarPer.Name = colResourceQtyVarPer
        ResourceQtyVarPer.Width = 100
        ResourceQtyVarPer.ReadOnly = True
        ResourceQtyVarPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceQtyVarPer)

        ResourceCostVarPer.FormatString = ""
        ResourceCostVarPer.HeaderText = "Cost Variance(%)"
        ResourceCostVarPer.Name = colResourceCostVarPer
        ResourceCostVarPer.Width = 100
        ResourceCostVarPer.ReadOnly = True
        ResourceCostVarPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceCostVarPer)


        ResourceType.FormatString = ""
        ResourceType.HeaderText = "Resource Type"
        ResourceType.Name = colResourceType
        ResourceType.Width = 0
        ResourceType.ReadOnly = True
        ResourceType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ResourceType.IsVisible = False
        gvResources.Columns.Add(ResourceType)

        ResourceUnitCostUOM.FormatString = ""
        ResourceUnitCostUOM.HeaderText = "Unit Cost UOM"
        ResourceUnitCostUOM.Name = colResourceUnitCostUom
        ResourceUnitCostUOM.Width = 0
        ResourceUnitCostUOM.ReadOnly = True
        ResourceUnitCostUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ResourceUnitCostUOM.IsVisible = False
        gvResources.Columns.Add(ResourceUnitCostUOM)
    End Sub

    Sub LoadToolTypeGridColumns()
        gvTools.Rows.Clear()
        gvTools.Columns.Clear()

        Dim ToolTypeCode As New GridViewTextBoxColumn
        Dim ToolTypeDesc As New GridViewTextBoxColumn
        Dim ToolRequirQty As New GridViewDecimalColumn
        Dim ToolUsedQty As New GridViewDecimalColumn
        Dim ToolStdCost As New GridViewDecimalColumn
        Dim ToolActualCost As New GridViewDecimalColumn
        Dim ToolQtyVarPer As New GridViewDecimalColumn
        Dim ToolCostVarPer As New GridViewDecimalColumn
        Dim ToolTypeUnitCost As New GridViewDecimalColumn
        Dim ToolTypeUnitCostUOM As New GridViewTextBoxColumn

        ToolTypeCode.FormatString = ""
        ToolTypeCode.HeaderText = "Tool Type Code"
        ToolTypeCode.Name = colToolTypeCode
        ToolTypeCode.Width = 100
        ToolTypeCode.ReadOnly = True
        ToolTypeCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTools.Columns.Add(ToolTypeCode)

        ToolTypeDesc.FormatString = ""
        ToolTypeDesc.HeaderText = "Description"
        ToolTypeDesc.Name = colToolTypeDesc
        ToolTypeDesc.Width = 100
        ToolTypeDesc.ReadOnly = True
        ToolTypeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolTypeDesc)

        ToolRequirQty.FormatString = ""
        ToolRequirQty.HeaderText = "Required Quantity"
        ToolRequirQty.Name = colToolRequirQty
        ToolRequirQty.Width = 100
        'ToolRequirQty.ReadOnly = True
        ToolRequirQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolRequirQty)

        ToolUsedQty.FormatString = ""
        ToolUsedQty.HeaderText = "Used Quantity"
        ToolUsedQty.Name = colToolUsedQty
        ToolUsedQty.Width = 100
        'ToolUsedQty.ReadOnly = True
        ToolUsedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolUsedQty)

        ToolTypeUnitCost.FormatString = ""
        ToolTypeUnitCost.HeaderText = "Unit Cost"
        ToolTypeUnitCost.Name = colToolUnitCost
        ToolTypeUnitCost.Width = 100
        ToolTypeUnitCost.ReadOnly = True
        ToolTypeUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ToolTypeUnitCost.IsVisible = False
        gvTools.Columns.Add(ToolTypeUnitCost)

        ToolQtyVarPer.FormatString = ""
        ToolQtyVarPer.HeaderText = "Qty Variance(%)"
        ToolQtyVarPer.Name = colToolQtyVarPer
        ToolQtyVarPer.Width = 100
        ToolQtyVarPer.ReadOnly = True
        ToolQtyVarPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolQtyVarPer)

        ToolCostVarPer.FormatString = ""
        ToolCostVarPer.HeaderText = "Cost Variance(%)"
        ToolCostVarPer.Name = colToolCostVarPer
        ToolCostVarPer.Width = 100
        ToolCostVarPer.ReadOnly = True
        ToolCostVarPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolCostVarPer)

        ToolTypeUnitCostUOM.FormatString = ""
        ToolTypeUnitCostUOM.HeaderText = "Unit Cost UOM"
        ToolTypeUnitCostUOM.Name = colToolUnitCostUom
        ToolTypeUnitCostUOM.Width = 0
        ToolTypeUnitCostUOM.ReadOnly = True
        ToolTypeUnitCostUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ToolTypeUnitCostUOM.IsVisible = False
        gvTools.Columns.Add(ToolTypeUnitCostUOM)
    End Sub


    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemCategoryCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim itemType As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn
        Dim BOMqty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim scrap_per As New GridViewDecimalColumn
        Dim wastage_per As New GridViewDecimalColumn
        Dim remarks As New GridViewTextBoxColumn
        Dim StockQty As New GridViewDecimalColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)

        ItemCategoryCode.FormatString = ""
        ItemCategoryCode.HeaderText = "Item Category"
        ItemCategoryCode.Name = colItemCategoryCode
        ItemCategoryCode.Width = 100
        ItemCategoryCode.ReadOnly = True
        ItemCategoryCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCategoryCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 100
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(itemDesc)

        itemType.FormatString = ""
        itemType.HeaderText = "Item Type"
        itemType.Name = colItemType
        itemType.Width = 100
        itemType.ReadOnly = True
        itemType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(itemType)

        BOMqty.FormatString = ""
        BOMqty.HeaderText = "BOM Quantity"
        BOMqty.Name = colBOMqty
        BOMqty.Width = 100
        BOMqty.ReadOnly = True
        BOMqty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(BOMqty)

        StockQty.FormatString = ""
        StockQty.HeaderText = "Stock Quantity"
        StockQty.Name = colStockqty
        StockQty.Width = 100
        StockQty.ReadOnly = True
        StockQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(StockQty)

        qty.FormatString = ""
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
        qty.ReadOnly = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(qty)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "UOM"
        UnitCode.Name = colUnitCode
        UnitCode.Width = 100
        UnitCode.ReadOnly = True
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(UnitCode)

        scrap_per.FormatString = ""
        scrap_per.HeaderText = "Scrap(%)"
        scrap_per.Name = colscrap_per
        scrap_per.Width = 100
        scrap_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(scrap_per)

        wastage_per.FormatString = ""
        wastage_per.HeaderText = "Wastage(%)"
        wastage_per.Name = colwastage_per
        wastage_per.Width = 100
        wastage_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(wastage_per)

        remarks.FormatString = ""
        remarks.HeaderText = "Remarks"
        remarks.Name = colRemarks
        remarks.Width = 130
        remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(remarks)




    End Sub


    Private Sub frmManufacturingOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmManufacturingOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        LoadCostGridColumns()
        LoadOperationGridColumns()
        LoadResourceGridColumns()
        LoadToolTypeGridColumns()

        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        'Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        'Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
        'Me.gvBOM.Rows.Clear()
        'Me.gvBOM.Rows.AddNew()
        
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmManufacturingOrder)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending

        Me.cboMOStatus.DataSource = clsManufacturingOrder.GetMOStatusTable()
        Me.cboMOStatus.DisplayMember = "Name"
        Me.cboMOStatus.ValueMember = "Code"

        isInsideLoad = True
        Me.cboSourceDocType.DataSource = clsManufacturingOrder.GetMOSourceDocTypeTable()
        Me.cboSourceDocType.DisplayMember = "Name"
        Me.cboSourceDocType.ValueMember = "Code"
        isInsideLoad = False

        Me.cboMOStatus.Text = "Open"
        Me.cboSourceDocType.SelectedValue = "Individual"
        Me.txtProducedItem.Value = Nothing
        Me.txtMasterItemName.Text = ""
        Me.txtOrderedQty.Text = 0
        Me.fndOrderQtyUOM.Value = Nothing
        Me.txtOrderedQtyStockUnit.Text = 0
        Me.lblUnitName.Text = ""
        Me.txtMOReference.Text = ""
        Me.txtDescription.Text = ""
        '' general tab
        Me.fndBomCode.Value = Nothing
        Me.lblRevisionNo.Text = ""
        Me.fndPlanner.Value = Nothing
        Me.fndInCharge.Value = Nothing
        Me.txtComments.Text = ""

        Me.lblCreatedByName.Text = ""
        Me.lblApprovedByName.Text = ""
        Me.lblReleasedByCode.Text = ""
        Me.lblClosedByCode.Text = ""
        txtDocPath.Text = ""

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvBOM.Rows.Clear()
        Me.gvBOM.Rows.AddNew()

        '' reset date fields
        Dim ServerDate As Date
        ServerDate = clsCommon.GETSERVERDATE()

        Me.dtpMODate.Value = ServerDate
        Me.dtpDueDate.Value = ServerDate
        Me.dtpApprovedDate.Value = ServerDate
        Me.dtpCloseDate.Value = ServerDate
        Me.dtpCreatedDate.Value = ServerDate
        Me.dtpDueDate.Value = ServerDate
        Me.dtpMODate.Value = ServerDate
        Me.dtpPlanEndDate.Value = ServerDate
        Me.dtpPlanStartDate.Value = ServerDate
        Me.dtpReleasedDate.Value = ServerDate

        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode

        Me.gvBOM.Rows.Clear()
        'Me.gvBOM.Rows.AddNew()

        Me.gvOperations.Rows.Clear()
        'Me.gvOperations.Rows.AddNew()

        Me.gvResources.Rows.Clear()
        'Me.gvResources.Rows.AddNew()

        Me.gvTools.Rows.Clear()
        'Me.gvTools.Rows.AddNew()
        ResetCosting()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsManufacturingOrder.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MO_CODE) > 0) Then

            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.MO_CODE
            Me.cboMOStatus.Text = obj.MO_STATUS
            Me.txtProducedItem.Value = obj.ITEM_CODE
            Me.txtMasterItemName.Text = obj.ITEM_NAME
            Me.txtOrderedQty.Text = obj.QTY_ORDERED
            Me.fndOrderQtyUOM.Value = obj.UNIT_CODE
            Me.txtOrderedQtyStockUnit.Text = obj.QTY_ORDERED_STOCK
            Me.lblUnitName.Text = obj.UNIT_CODE_STOCK
            Me.txtMOReference.Text = obj.MO_REFERENCE
            Me.txtDescription.Text = obj.DESCRIPTION

            Me.cboSourceDocType.SelectedValue = obj.SOURCE_DOC_TYPE
            Me.fndProductionPlan.Value = obj.PROD_PLAN_CODE
            txtProductionPlanDesc.Text = obj.PROD_PLAN_DESC
            Me.fndSONo.Value = obj.SO_CODE
            txtSODesc.Text = obj.SO_DESC
            txtParentMOCode.Text = obj.PARENT_MO_CODE

            '' general tab

            Me.dtpMODate.Value = obj.MO_DATE
            Me.dtpDueDate.Value = obj.MO_DUE_DATE

            Me.fndBomCode.Value = obj.BOM_CODE
            Me.lblRevisionNo.Text = obj.BOM_REVISION_NO
            Me.fndProductionLine.Value = obj.PRODUCTION_AREA
            Me.fndPlanner.Value = obj.PLANNER
            Me.fndInCharge.Value = obj.IN_CHARGE
            Me.fndLocation.Value = obj.LOCATION_CODE
            Me.lblLocationDesc.Text = obj.LOCATION_Desc
            '' planned start date
            If clsCommon.myLen(obj.PLANNED_START_DATE) > 0 Then
                Me.dtpPlanStartDate.Value = obj.PLANNED_START_DATE
                Me.dtpPlanStartDate.Checked = True
            Else
                Me.dtpPlanStartDate.Checked = False
            End If

            '' planned end date
            If clsCommon.myLen(obj.PLANNED_END_DATE) > 0 Then
                Me.dtpPlanEndDate.Value = obj.PLANNED_END_DATE
                Me.dtpPlanEndDate.Checked = True
            Else
                Me.dtpPlanEndDate.Checked = False
            End If

            '' actual start date
            If clsCommon.myLen(obj.ACTUAL_START_DATE) > 0 Then
                Me.dtpActualStartDate.Value = obj.ACTUAL_START_DATE
                Me.dtpActualStartDate.Checked = True
            Else
                Me.dtpActualStartDate.Checked = False
            End If

            '' actual end date
            If clsCommon.myLen(obj.ACTUAL_END_DATE) > 0 Then
                Me.dtpActualEndDate.Value = obj.ACTUAL_END_DATE
                Me.dtpActualEndDate.Checked = True
            Else
                Me.dtpActualEndDate.Checked = False
            End If
            Me.lblCreatedByName.Text = obj.CREATED_BY
            Me.lblApprovedByName.Text = obj.APPROVED_BY
            Me.lblReleasedByCode.Text = obj.RELEASED_BY
            Me.lblClosedByCode.Text = obj.CLOSED_BY

            '' CREATED DATE
            If clsCommon.myLen(obj.Created_Date) > 0 Then
                Me.dtpCreatedDate.Value = obj.Created_Date
                Me.dtpCreatedDate.Checked = True
            Else
                Me.dtpCreatedDate.Checked = False
            End If

            '' APPROVED DATE
            If clsCommon.myLen(obj.APPROVED_DATE) > 0 Then
                Me.dtpApprovedDate.Value = obj.APPROVED_DATE
                Me.dtpApprovedDate.Checked = True
            Else
                Me.dtpApprovedDate.Checked = False
            End If

            '' RELEASE DATE
            If clsCommon.myLen(obj.RELEASED_DATE_DATE) > 0 Then
                Me.dtpReleasedDate.Value = obj.RELEASED_DATE_DATE
                Me.dtpReleasedDate.Checked = True
            Else
                Me.dtpReleasedDate.Checked = False
            End If

            '' CLOSE DATE
            If clsCommon.myLen(obj.CLOSED_DATE) > 0 Then
                Me.dtpCloseDate.Value = obj.CLOSED_DATE
                Me.dtpCloseDate.Checked = True
            Else
                Me.dtpCloseDate.Checked = False
            End If
            Me.txtComments.Text = obj.COMMENTS

            Me.txtDocPath.Text = obj.ATTACHED_DOC_PATH

            If clsCommon.myLen(obj.ATTACHED_DOC_PATH) > 0 Then
                btnBrowse.Text = "Download"
            Else
                btnBrowse.Text = "Browse"
            End If

            If (obj.ObjListMaterial IsNot Nothing AndAlso obj.ObjListMaterial.Count > 0) Then
                For Each objTr As clsMOMaterial In obj.ObjListMaterial
                    gvBOM.Rows.AddNew()

                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCategoryCode).Value = objTr.CONSM_ITEM_CATEGORY_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = objTr.CONSM_ITEM_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Tag = objTr.SubItemType.ToString
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = objTr.ITEM_DESCRIPTION
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemType).Value = objTr.ITEM_TYPE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = objTr.CONSM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colBOMqty).Value = objTr.BOM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = objTr.CONSM_ITEM_UNIT_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colscrap_per).Value = objTr.SCRAP_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colwastage_per).Value = objTr.WASTAGE_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = objTr.REMARKS
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colStockqty).Value = objTr.STOCK_QUANTITY
                Next
            Else
                'gvBOM.Rows.AddNew()
            End If
        End If
        '' display operations
        gvOperations.Rows.Clear()
        If obj.ObjListOP IsNot Nothing And obj.ObjListOP.Count > 0 Then
            For Each objOP As clsMOOperations In obj.ObjListOP
                gvOperations.Rows.AddNew()
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationLineNo).Value = objOP.Line_No
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Value = objOP.OPERATION_CODE
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Value = objOP.WORK_CENTER_CODE

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedSetupTimeMin).Value = objOP.PROJECTED_SETUP_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedRunTimeMin).Value = objOP.PROJECTED_RUN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedCleanTimeMin).Value = objOP.PROJECTED_CLEAN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedWaitTimeMin).Value = objOP.PROJECTED_WAIT_TIME_MINUTES

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualSetupTimeMin).Value = objOP.ACTUAL_SETUP_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualRunTimeMin).Value = objOP.ACTUAL_RUN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualCleanTimeMin).Value = objOP.ACTUAL_CLEAN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualWaitTimeMin).Value = objOP.ACTUAL_WAIT_TIME_MINUTES

                If objOP.START_DATE IsNot Nothing Then
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colStartDate).Value = objOP.START_DATE
                Else
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colStartDate).Value = Nothing
                End If

                If objOP.END_DATE IsNot Nothing Then
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colEndDate).Value = objOP.END_DATE
                Else
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colEndDate).Value = Nothing
                End If

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationRemarks).Value = objOP.REMARKS

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Tag = clsMOResources.GetMOResources(objOP.MO_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Tag = clsMOToolTypes.GetMOToolTypes(objOP.MO_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)

                Dim obj As New clsOperationMaster
                obj = obj.GetData(objOP.OPERATION_CODE, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Descraption) > 0 Then
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationDesc).Value = obj.Descraption
                End If



            Next
            CalculateMOCosting()
            'gvOperations.Rows.AddNew()
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New clsManufacturingOrder
                obj.MO_CODE = Me.txtCode.Value
                obj.MO_STATUS = Me.cboMOStatus.Text
                obj.ITEM_CODE = Me.txtProducedItem.Value
                obj.QTY_ORDERED = Me.txtOrderedQty.Text
                obj.UNIT_CODE = Me.fndOrderQtyUOM.Value
                obj.QTY_ORDERED_STOCK = Me.txtOrderedQtyStockUnit.Text
                obj.UNIT_CODE_STOCK = Me.lblUnitName.Text
                obj.MO_REFERENCE = Me.txtMOReference.Text
                obj.DESCRIPTION = Me.txtDescription.Text

                obj.SOURCE_DOC_TYPE = Me.cboSourceDocType.SelectedValue
                obj.PROD_PLAN_CODE = Me.fndProductionPlan.Value
                obj.SO_CODE = Me.fndSONo.Value

                '' general tab
                obj.BOM_CODE = Me.fndBomCode.Value
                obj.BOM_REVISION_NO = Me.lblRevisionNo.Text

                obj.MO_DATE = Me.dtpMODate.Value
                obj.MO_DUE_DATE = Me.dtpDueDate.Value

                obj.PLANNER = Me.fndPlanner.Value
                obj.IN_CHARGE = Me.fndInCharge.Value
                obj.COMMENTS = Me.txtComments.Text

                obj.PLANNED_START_DATE = IIf(Me.dtpPlanStartDate.Checked, Me.dtpPlanStartDate.Value, Nothing)
                obj.PLANNED_END_DATE = IIf(Me.dtpPlanEndDate.Checked, Me.dtpPlanEndDate.Value, Nothing)

                obj.ACTUAL_START_DATE = IIf(Me.dtpActualStartDate.Checked, Me.dtpActualStartDate.Value, Nothing)
                obj.ACTUAL_END_DATE = IIf(Me.dtpActualEndDate.Checked, Me.dtpActualEndDate.Value, Nothing)

                obj.CREATED_BY = Me.lblCreatedByName.Text
                obj.APPROVED_BY = Me.lblApprovedByName.Text
                obj.RELEASED_BY = Me.lblReleasedByCode.Text
                obj.CLOSED_BY = Me.lblClosedByCode.Text

                obj.Created_Date = IIf(Me.dtpCreatedDate.Checked, Me.dtpCreatedDate.Value, Nothing)
                obj.APPROVED_DATE = IIf(Me.dtpApprovedDate.Checked, Me.dtpApprovedDate.Value, Nothing)
                obj.RELEASED_DATE_DATE = IIf(Me.dtpReleasedDate.Checked, Me.dtpReleasedDate.Value, Nothing)
                obj.CLOSED_DATE = IIf(Me.dtpCloseDate.Checked, Me.dtpCloseDate.Value, Nothing)

                '' production area and document path
                obj.PRODUCTION_AREA = Me.fndProductionLine.Value
                obj.ATTACHED_DOC_PATH = Me.txtDocPath.Text
                obj.LOCATION_CODE = Me.fndLocation.Value
                '' saving material for MO
                Dim obj1 As clsMOMaterial
                Dim ObjListMaterial As List(Of clsMOMaterial)
                ObjListMaterial = New List(Of clsMOMaterial)
                For Each grow As GridViewRowInfo In gvBOM.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        obj1 = New clsMOMaterial()

                        obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        obj1.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)
                        obj1.CONSM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                        obj1.ITEM_TYPE = clsCommon.myCstr(grow.Cells(colItemType).Value)
                        obj1.CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells(colqty).Value)
                        obj1.BOM_QUANTITY = clsCommon.myCdbl(grow.Cells(colBOMqty).Value)
                        obj1.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        obj1.SCRAP_PERCENT = clsCommon.myCdbl(grow.Cells(colscrap_per).Value)
                        obj1.WASTAGE_PERCENT = clsCommon.myCdbl(grow.Cells(colwastage_per).Value)
                        obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        obj1.STOCK_QUANTITY = clsCommon.myCdbl(grow.Cells(colStockqty).Value)
                        ObjListMaterial.Add(obj1)
                    End If
                Next
                '' map material 

                If ObjListMaterial IsNot Nothing And ObjListMaterial.Count > 0 Then
                    obj.ObjListMaterial = ObjListMaterial
                Else
                    obj.ObjListMaterial = Nothing
                End If

                '' saving operations
                Dim objListOP As New List(Of clsMOOperations)
                Dim objListResource As New List(Of clsMOResources)
                Dim objListToolTypes As New List(Of clsMOToolTypes)

                Dim objOP As clsMOOperations
                For Each row As GridViewRowInfo In gvOperations.Rows
                    If clsCommon.myLen(row.Cells(colOperationCode).Value) > 0 And clsCommon.myLen(row.Cells(colworkCenterCode).Value) > 0 Then
                        objOP = New clsMOOperations
                        objOP.MO_CODE = Me.txtCode.Value
                        objOP.Line_No = row.Cells(colOperationLineNo).Value
                        objOP.OPERATION_CODE = row.Cells(colOperationCode).Value
                        objOP.WORK_CENTER_CODE = row.Cells(colworkCenterCode).Value
                        objOP.PROJECTED_SETUP_TIME_MINUTES = row.Cells(colProjectedSetupTimeMin).Value
                        objOP.PROJECTED_RUN_TIME_MINUTES = row.Cells(colProjectedRunTimeMin).Value
                        objOP.PROJECTED_CLEAN_TIME_MINUTES = row.Cells(colProjectedCleanTimeMin).Value
                        objOP.PROJECTED_WAIT_TIME_MINUTES = row.Cells(colProjectedWaitTimeMin).Value

                        objOP.ACTUAL_SETUP_TIME_MINUTES = row.Cells(colActualSetupTimeMin).Value
                        objOP.ACTUAL_RUN_TIME_MINUTES = row.Cells(colActualRunTimeMin).Value
                        objOP.ACTUAL_CLEAN_TIME_MINUTES = row.Cells(colActualCleanTimeMin).Value
                        objOP.ACTUAL_WAIT_TIME_MINUTES = row.Cells(colActualWaitTimeMin).Value

                        If clsCommon.myLen(row.Cells(colStartDate).Value) > 0 Then
                            objOP.START_DATE = row.Cells(colStartDate).Value
                        Else
                            objOP.START_DATE = Nothing
                        End If

                        If clsCommon.myLen(row.Cells(colEndDate).Value) > 0 Then
                            objOP.END_DATE = row.Cells(colEndDate).Value
                        Else
                            objOP.END_DATE = Nothing
                        End If
                        objOP.REMARKS = row.Cells(colOperationRemarks).Value
                        objListOP.Add(objOP)



                        '' resources 

                        If row.Cells(colOperationCode).Tag IsNot Nothing Then
                            Dim objListRes As List(Of clsMOResources)
                            objListRes = row.Cells(colOperationCode).Tag

                            objListResource.AddRange(objListRes)

                        End If

                        '' tools 

                        If row.Cells(colworkCenterCode).Tag IsNot Nothing Then
                            Dim objListTools As List(Of clsMOToolTypes)
                            objListTools = row.Cells(colworkCenterCode).Tag
                            objListToolTypes.AddRange(objListTools)
                        End If
                    End If

                Next
                '' map operations
                If objListOP IsNot Nothing And objListOP.Count > 0 Then
                    obj.ObjListOP = objListOP
                Else
                    obj.ObjListOP = Nothing
                End If

                '' map resources
                If objListResource IsNot Nothing And objListResource.Count > 0 Then
                    obj.ObjListRes = objListResource
                Else
                    obj.ObjListRes = Nothing
                End If

                '' map resources
                If objListToolTypes IsNot Nothing And objListToolTypes.Count > 0 Then
                    obj.ObjListToolType = objListToolTypes
                Else
                    obj.ObjListToolType = Nothing
                End If

                '' saving bom costing
                Dim objListCost As New List(Of clsMOCosting)
                Dim objCost As clsMOCosting


                objCost = New clsMOCosting
                objCost.MO_CODE = Me.txtCode.Value
                objCost.BOM_CODE = Me.fndBomCode.Value
                objCost.CALC_TYPE = "Standard"
                objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value
                objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value
                objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value
                objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value
                objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value
                objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value
                objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value
                objCost.TOTAL_COST = Val(Me.gvCost.Rows(rowTotal).Cells(colStdCost).Value.ToString)
                objListCost.Add(objCost)

                '' projected
                objCost = New clsMOCosting
                objCost.MO_CODE = Me.txtCode.Value
                objCost.BOM_CODE = Me.fndBomCode.Value
                objCost.CALC_TYPE = "Projected"
                objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colProjectedCost).Value
                objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colProjectedCost).Value
                objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colProjectedCost).Value
                objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colProjectedCost).Value
                objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colProjectedCost).Value
                objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colProjectedCost).Value
                objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colProjectedCost).Value
                objCost.TOTAL_COST = Val(Me.gvCost.Rows(rowTotal).Cells(colProjectedCost).Value.ToString)
                objListCost.Add(objCost)

                '' Variance of Standard/Projected
                objCost = New clsMOCosting
                objCost.MO_CODE = Me.txtCode.Value
                objCost.BOM_CODE = Me.fndBomCode.Value
                objCost.CALC_TYPE = "Variance of Standard/Projected"
                objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdProjVar).Value
                objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdProjVar).Value
                objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colStdProjVar).Value
                objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colStdProjVar).Value
                objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdProjVar).Value
                objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colStdProjVar).Value
                objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colStdProjVar).Value
                objCost.TOTAL_COST = 0 'Val(Me.gvCost.Rows(rowTotal).Cells(colStdProjVar).Value.ToString)
                objListCost.Add(objCost)

                '' Actual Cost
                objCost = New clsMOCosting
                objCost.MO_CODE = Me.txtCode.Value
                objCost.BOM_CODE = Me.fndBomCode.Value
                objCost.CALC_TYPE = "Actual"
                objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colActualCost).Value
                objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colActualCost).Value
                objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colActualCost).Value
                objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colActualCost).Value
                objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colActualCost).Value
                objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colActualCost).Value
                objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colActualCost).Value
                objCost.TOTAL_COST = Val(Me.gvCost.Rows(rowTotal).Cells(colActualCost).Value.ToString)
                objListCost.Add(objCost)

                '' Variance of standard and actual Cost
                objCost = New clsMOCosting
                objCost.MO_CODE = Me.txtCode.Value
                objCost.BOM_CODE = Me.fndBomCode.Value
                objCost.CALC_TYPE = "Variance of Standard/Actual"
                objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdActualVar).Value
                objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdActualVar).Value
                objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colStdActualVar).Value
                objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colStdActualVar).Value
                objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdActualVar).Value
                objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colStdActualVar).Value
                objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colStdActualVar).Value
                objCost.TOTAL_COST = 0 'Val(Me.gvCost.Rows(rowTotal).Cells(colStdActualVar).Value.ToString)
                objListCost.Add(objCost)

                '' map MO costing
                obj.ObjListCosting = objListCost
                Dim issaved As Boolean = False
                '' initilize transaction
                'trans = clsDBFuncationality.GetTransactin()
                issaved = clsManufacturingOrder.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))

                If OpenFileDialog1.FileName = "" Then
                    If issaved Then
                        'trans.Commit()
                        'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                        LoadData(obj.MO_CODE, NavigatorType.Current)
                        Return issaved
                    Else
                        'trans.Rollback()
                        Return issaved
                    End If

                End If
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(OpenFileDialog1.FileName))
                bData = br.ReadBytes(br.BaseStream.Length)
                obj.ATTACHED_DOC = bData

                Dim str As String
                str = "UPDATE TSPL_MF_MANUFACTURING_ORDER set ATTACHED_DOC = @BLOBData where MO_CODE = '" + txtCode.Value + "' "
                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Parameters.Add(prm)

                issaved = issaved And cmd.ExecuteNonQuery()
                br.Close() ' done by stuti reagrding memory leakage
                If issaved Then
                    'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                    LoadData(obj.MO_CODE, NavigatorType.Current)
                    Return issaved
                Else

                    'trans.Rollback()
                    Return issaved
                End If


            Catch ex As Exception

                'trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                Return False
            End Try
        Else
            Return False
        End If

        Return True
    End Function
    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_MANUFACTURING_ORDER where MO_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        If clsCommon.myLen(txtProducedItem.Value) <= 0 Then
            myMessages.blankValue(Me, "Ordered Item Code", Me.Text)
            txtProducedItem.Focus()
            Return False
        End If
        If clsCommon.myCdbl(txtOrderedQty.Text) <= 0 Then
            myMessages.blankValue(Me, "Ordered Quantity", Me.Text)
            txtOrderedQty.Focus()
            Return False
        End If

        If clsCommon.myLen(fndOrderQtyUOM.Value) <= 0 Then
            myMessages.blankValue(Me, "Ordered Item UOM", Me.Text)
            fndOrderQtyUOM.Focus()
            Return False
        End If

        If clsCommon.myCdbl(txtOrderedQtyStockUnit.Text) <= 0 Then
            myMessages.blankValue(Me, "Ordered Quantity(Stock Unit)", Me.Text)
            txtOrderedQtyStockUnit.Focus()
            Return False
        End If

        If clsCommon.myLen(lblUnitName.Text) <= 0 Then
            myMessages.blankValue(Me, "Ordered Item UOM", Me.Text)
            fndOrderQtyUOM.Focus()
            Return False
        End If

        If clsCommon.myLen(txtMOReference.Text) <= 0 Then
            myMessages.blankValue(Me, "MO Reference", Me.Text)
            txtMOReference.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(fndBomCode.Value) <= 0 Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
            fndBomCode.Focus()
            Return False
        End If

        If clsCommon.myLen(lblRevisionNo.Text) <= 0 Then
            myMessages.blankValue(Me, "BOM Revision No", Me.Text)
            lblRevisionNo.Focus()
            Return False
        End If

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            myMessages.blankValue(Me, "Location Code", Me.Text)
            fndLocation.Focus()
            Return False
        End If
        'If clsCommon.myLen(fndProductionLine.Value) <= 0 Then
        '    myMessages.blankValue("Production Line")
        '    fndProductionLine.Focus()
        '    Return False
        'End If

        'Dim ii As Int16 = 0
        'For Each grow As GridViewRowInfo In gvBOM.Rows
        '    'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
        '    '    ii += 1

        '    '    ObjList.Add(obj)
        '    'End If

        'Next

        Return True
    End Function



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
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
                If (clsManufacturingOrder.DeleteData(txtCode.Value)) Then
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

    Private Sub txtMasterItem__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtProducedItem._MYValidating
        Try
            If clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "Individual") = CompairStringResult.Equal Then
                Dim objItem As clsItemMaster = clsManufacturingOrder.FinderForFinishedGoods(txtProducedItem.Value, isButtonClicked)
                If Not objItem Is Nothing Then
                    txtProducedItem.Value = objItem.Item_Code
                    Me.txtMasterItemName.Text = objItem.Item_Desc
                    Me.fndOrderQtyUOM.Value = objItem.Unit_Code
                    Me.lblUnitName.Text = objItem.Unit_Code
                End If
            ElseIf clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "PP") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsProductionPlanning.GetFinerForPlanItem("TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE='" & fndProductionPlan.Value & "'", Me.txtProducedItem.Value, isButtonClicked)
                If dt.Rows.Count > 0 Then
                    Me.txtProducedItem.Value = dt.Rows(0).Item("Code")
                    Me.txtMasterItemName.Text = dt.Rows(0).Item("ITEM_DESCRIPTION")
                    Me.fndBomCode.Value = dt.Rows(0).Item("BOM_CODE")
                    'lblBOMCode.Text = dt.Rows(0).Item("Description")
                    Me.txtOrderedQty.Text = dt.Rows(0).Item("PLAN_QTY")
                    Me.lblUnitName.Text = dt.Rows(0).Item("UNIT_CODE")
                    Me.fndOrderQtyUOM.Value = dt.Rows(0).Item("UNIT_CODE")
                    lblRevisionNo.Text = clsCommon.myCstr(dt.Rows(0).Item("BOM_REVISION_NO"))
                End If
            ElseIf clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "SO") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsManufacturingOrder.GetFinerForSOItem("TSPL_SD_SALES_ORDER_HEAD.Document_Code='" & fndSONo.Value & "'", Me.txtProducedItem.Value, isButtonClicked)
                If dt.Rows.Count > 0 Then
                    Me.txtProducedItem.Value = dt.Rows(0).Item("Code")
                    Me.txtMasterItemName.Text = dt.Rows(0).Item("Item_Desc")
                    Me.txtOrderedQty.Text = dt.Rows(0).Item("Qty")
                    Me.lblUnitName.Text = dt.Rows(0).Item("UNIT_CODE")
                    Me.fndOrderQtyUOM.Value = dt.Rows(0).Item("UNIT_CODE")
                End If
            End If

            CalculateMOCosting()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_MANUFACTURING_ORDER where MO_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.MO_CODE AS Code,T1.MO_STATUS as [Status],T1.QTY_ORDERED as  [Ordered Qty],T1.UNIT_CODE as [Unit(Ordered)]," &
                                " T1.QTY_ORDERED_STOCK as [Ordered Qty(Stock)],T1.UNIT_CODE_STOCK as [Unit Code Stock],T1.MO_REFERENCE as [Reference],T1.DESCRIPTION as [Description], " &
                                " T1.MO_DATE as [Order Date],T1.MO_DUE_DATE as [Order Due Date],T1.BOM_CODE as [BOM Code],T1.BOM_REVISION_NO as [BOM Revision No] FROM TSPL_MF_MANUFACTURING_ORDER  T1 INNER JOIN TSPL_ITEM_MASTER T2 " &
                                " ON T1.ITEM_CODE=T2.ITEM_CODE "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_MANUFACTURING_ORDER", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If Me.cboMOStatus.Text <> "Approved" Then
                common.clsCommon.MyMessageBoxShow("MO Status must be Approved.")
                Exit Sub
            End If
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsManufacturingOrder.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub




    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub



    'Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
    '    If gvBOM.CurrentRow Is Nothing Then
    '        Exit Sub
    '    End If

    '    If gvBOM.CurrentRow.Cells(0).Value = "" Then
    '        gvBOM.CurrentRow.Cells(0).Value = gvBOM.RowCount
    '    End If

    '    If Not isCellValueChangedOpen Then
    '        isCellValueChangedOpen = True
    '        If e.Column Is gvBOM.Columns(colItemCategoryCode) Then
    '            Dim strq As String = ""
    '            strq = "select PROD_ITEM_CATEGORY_CODE as Code,DESCRIPTION from TSPL_MF_PRODUCTION_ITEM_CATEGORY "
    '            gvBOM.CurrentRow.Cells(colItemCategoryCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCategoryCode).Value))
    '        End If


    '        If e.Column Is gvBOM.Columns(colItemCode) Then
    '            Dim obj As clsManufacturingOrder = clsManufacturingOrder.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), "ITEM_TYPE IN ('R','O') ", False)
    '            ''and prod_item_category_code='" & gvBOM.CurrentRow.Cells(colItemCategoryCode).Value & "'
    '            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
    '                gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
    '                gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
    '                gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE

    '            End If
    '        End If
    '        isCellValueChangedOpen = False
    '    End If
    'End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If Me.btnBrowse.Text = "Browse" Then
            OpenFileDialog1.ShowDialog()
            txtDocPath.Text = OpenFileDialog1.SafeFileName
            Me.txtDocPath.ReadOnly = False
        Else
            showAttachedDocs(obj)
            Me.txtDocPath.ReadOnly = True
        End If

    End Sub
    Sub showAttachedDocs(ByVal obj As clsManufacturingOrder)

        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try



            filename = clsCommon.myCstr(obj.ATTACHED_DOC_PATH).Split("\")(clsCommon.myCstr(obj.ATTACHED_DOC_PATH).Split("\").Length - 1)
            Dim blob As Byte() = obj.ATTACHED_DOC
            file_path = Application.StartupPath

            Dim index As Int16 = filename.LastIndexOf(".")
            file_extn = filename.Substring(index)
            filename = filename.Remove(index)
            filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
            filename = filename.Replace(" ", "")
            filename = filename.Replace("/", "_")
            filename = filename.Replace(":", "_")
            Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
            fs.Write(blob, 0, blob.Length)
            fs.Close()
            System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNewFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFile.Click
        Me.txtDocPath.Text = ""
        Me.btnBrowse.Text = "Browse"
    End Sub

    Private Sub txtProducedItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducedItem.Load

    End Sub

    'Private Sub gvBOM_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvBOM.CurrentColumnChanged
    '    If gvBOM.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gvBOM.CurrentRow.Index
    '        gvBOM.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gvBOM.Rows.Count - 1 Then
    '            If (clsCommon.myLen(txtCode.Value) > 0) Then
    '                gvBOM.Rows.AddNew()
    '                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    '                gvBOM.CurrentRow = gvBOM.Rows(intCurrRow)
    '            End If
    '        End If
    '    End If

    'End Sub



    'Private Sub gvBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvBOM.KeyDown
    '    If e.KeyData = Keys.Enter Then
    '        Me.gvBOM.Rows.Add(1)

    '        gvBOM.Rows(gvBOM.RowCount - 1).Cells(0).Value = gvBOM.RowCount
    '    End If
    '    If e.KeyData = Keys.Right And gvBOM.CurrentCell.ColumnIndex = gvBOM.Columns.Count - 1 Then
    '        Me.gvBOM.Rows.Add(1)
    '        gvBOM.Rows(gvBOM.RowCount - 1).Cells(0).Value = gvBOM.RowCount
    '    End If
    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
        Else
            funPrint()
        End If
    End Sub
    Private Sub funPrint()
        'Try
        '    Dim qry As String = " select '" & objCommonVar.CurrentCompanyName & "' as Company_Name, TSPL_MF_MANUFACTURING_ORDER.PROD_ITEM_CODE  as BuildItemCode,CONVERT(VARCHAR,TSPL_MF_MANUFACTURING_ORDER.MO_DATE,103) as BOMDate,CONVERT(VARCHAR,TSPL_MF_MANUFACTURING_ORDER.START_DATE,103) as StartDate,"
        '    qry += " CONVERT(VARCHAR,TSPL_MF_MANUFACTURING_ORDER.END_DATE,103) as EndDate,TSPL_MF_MANUFACTURING_ORDER.STATUS as BomStatus,TSPL_MF_MANUFACTURING_ORDER.PROD_ITEM_UNIT_CODE as BuildUOM,"
        '    qry += " TSPL_MF_MANUFACTURING_ORDER.PROD_QUANTITY as BuildQty, "
        '    qry += " TSPL_MF_MANUFACTURING_ORDER.MIN_BATCH_SIZE as MinBatchSize,TSPL_MF_BOM_DETAIL.LINE_NO as SL_No,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE as ItemCategory,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION as ItemDesc,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UOM,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY as Quantity,TSPL_MF_BOM_DETAIL.SCRAP_PERCENT as Scrap,TSPL_MF_BOM_DETAIL.WASTAGE_PERCENT as Wastage,"
        '    qry += " TSPL_MF_BOM_DETAIL.REMARKS as Remarks from TSPL_MF_MANUFACTURING_ORDER inner join TSPL_MF_BOM_DETAIL on TSPL_MF_MANUFACTURING_ORDER.MO_CODE=TSPL_MF_BOM_DETAIL.MO_CODE"
        '    qry += " where 2=2"

        '    If txtCode.Value <> "" Then
        '        qry += " and  TSPL_MF_MANUFACTURING_ORDER.MO_CODE='" & txtCode.Value & "' "
        '    End If
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    ProductionReportViewer.funreport(dt, "crptBOMPrint", "Bill Of Material")

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

   
    Private Sub gvOperations_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvOperations.CellDoubleClick
        If Not gvOperations.CurrentRow Is Nothing Then
            FillResources(gvOperations.CurrentRow.Index)
            FillToolTypes(gvOperations.CurrentRow.Index)
        End If
    End Sub

    Private Sub gvOperations_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvOperations.CellEndEdit
        If gvOperations.CurrentRow Is Nothing Then
            Exit Sub
        End If
        'FillResources(e.RowIndex)
        'FillToolTypes(e.RowIndex)

        If Not isCellValueChangedOpen Then
            FillResources(e.RowIndex)
            FillToolTypes(e.RowIndex)
            '    isCellValueChangedOpen = True
            '    If e.Column Is gvOperations.Columns(colOperationCode) Then
            '        Dim strq As String = ""
            '        strq = "select OPERATION_CODE as Code,DESCRIPTION as Name from TSPL_MF_OPERATION "
            '        gvOperations.CurrentRow.Cells(colOperationCode).Value = clsCommon.ShowSelectForm("TSPL_MF_OPERATION", strq, "Code", "", clsCommon.myCstr(gvOperations.CurrentRow.Cells(colOperationCode).Value))
            '        Dim obj As New clsOperationMaster
            '        obj = obj.GetData(gvOperations.CurrentRow.Cells(colOperationCode).Value, NavigatorType.Current)
            '        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Descraption) > 0 Then
            '            gvOperations.CurrentRow.Cells(colOperationDesc).Value = obj.Descraption
            '        End If
            '        'gvOperations.CurrentRow.Cells(colOperationDesc).Value = New clsOperationMaster().GetData(gvOperations.CurrentRow.Cells(colOperationCode).Value, NavigatorType.Current).Descraption.ToString
            '    End If


            '    If e.Column Is gvOperations.Columns(colworkCenterCode) Then
            '        If clsCommon.myLen(gvOperations.CurrentRow.Cells(colOperationCode).Value) > 0 Then
            '            Dim strq As String = ""
            '            Dim cond As String = ""
            '            strq = "select TSPL_MF_OPERATION_WORK_CENTER.WORK_CENTER_CODE as Code,TSPL_MF_WORK_CENTER.DESCRIPTION AS Name from TSPL_MF_OPERATION_WORK_CENTER INNER JOIN TSPL_MF_WORK_CENTER ON TSPL_MF_OPERATION_WORK_CENTER.WORK_CENTER_CODE=TSPL_MF_WORK_CENTER.WORK_CENTER_CODE "
            '            cond = " TSPL_MF_OPERATION_WORK_CENTER.OPERATION_CODE='" & gvOperations.CurrentRow.Cells(colOperationCode).Value & "'"
            '            gvOperations.CurrentRow.Cells(colworkCenterCode).Value = clsCommon.ShowSelectForm("TSPL_MF_WORK_CENTER", strq, "Code", cond, clsCommon.myCstr(gvOperations.CurrentRow.Cells(colworkCenterCode).Value))

            '            Dim obj As clsWorkCenterMaster = clsWorkCenterMaster.GetData(gvOperations.CurrentRow.Cells(colworkCenterCode).Value, NavigatorType.Current)
            '            If obj IsNot Nothing Then
            '                gvOperations.CurrentRow.Cells(colProjectedSetupTimeMin).Value = IIf(obj.SETUP_TIME_TYPE = "Hour", obj.SETUP_TIME * 60, obj.SETUP_TIME)
            '                gvOperations.CurrentRow.Cells(colProjectedRunTimeMin).Value = IIf(obj.RUN_TIME_TYPE = "Hour", obj.RUN_TIME * 60, obj.RUN_TIME)
            '                gvOperations.CurrentRow.Cells(colProjectedCleanTimeMin).Value = IIf(obj.CLEANUP_TIME_TYPE = "Hour", obj.CLEANUP_TIME * 60, obj.CLEANUP_TIME)
            '                gvOperations.CurrentRow.Cells(colProjectedWaitTimeMin).Value = IIf(obj.WAIT_TIME_TYPE = "Hour", obj.WAIT_TIME * 60, obj.WAIT_TIME)

            '            End If
            '            FillResources(e.RowIndex)
            '            FillToolTypes(e.RowIndex)
            '        Else
            '            clsCommon.MyMessageBoxShow("Select valid Operation Code first")
            '        End If

            '    End If
            CalculateMOCosting()
            isCellValueChangedOpen = False
        End If
    End Sub
    'Function getResourcesFromResourceMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOResources)
    'Dim objList As New List(Of clsMOResources)
    'Dim obj1 As clsMOResources
    'Dim objList1 As List(Of clsWorkCenterResourceDetail)

    'objList1 = clsWorkCenterResourceDetail.GetData(WorkCenterCode, trans)
    'If Not objList1 Is Nothing And objList1.Count > 0 Then
    '    For Each obj As clsWorkCenterResourceDetail In objList1
    '        obj1 = New clsMOResources
    '        obj1.OPERATION_CODE = OperationCode
    '        obj1.WORK_CENTER_CODE = WorkCenterCode
    '        obj1.RESOURCE_CODE = obj.RESOURCE_CODE
    '        obj1.RESOURCE_Desc = obj.DESCRIPTION
    '        obj1.RESOURCE_Type = obj.RESOURCE_TYPE
    '        obj1.QUANTITY = 1
    '        obj1.UNIT_COST = obj.UNIT_COST
    '        obj1.TOTAL_COST = obj.UNIT_COST
    '        obj1.UNIT_COST_UOM = obj.UNIT_CODE_OTHER
    '        objList.Add(obj1)
    '    Next
    'End If
    'Return objList
    ' End Function
    Function FillResources(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOResources)
        Dim objList As New List(Of clsMOResources)
        If isNewEntry Then
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag Is Nothing Then
                    'objList = getResourcesFromResourceMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                Else
                    objList = gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag

                End If
            Else
                gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = Nothing
                '' nothing to do
            End If
        Else
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag Is Nothing Then
                    objList = clsMOResources.GetMOResources(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    If objList Is Nothing Or objList.Count = 0 Then
                        'objList = getResourcesFromResourceMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    End If
                Else
                    objList = gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag
                End If
            Else
                gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = Nothing
                '' nothing to do
            End If
        End If
        '' fill resources grid
        Me.gvResources.Rows.Clear()
        If Not objList Is Nothing Then
            For Each obj As clsMOResources In objList
                gvResources.Rows.AddNew()
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceCode).Value = obj.RESOURCE_CODE
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceDesc).Value = obj.RESOURCE_Desc
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceRequirQty).Value = obj.REQUIRED_QUANTITY
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceUsedQty).Value = obj.USED_QUANTITY
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceUnitCost).Value = obj.UNIT_COST
                'gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceStdCost).Value = obj.STD_COST
                'gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceActualCost).Value = obj.ACTUAL_COST
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceQtyVarPer).Value = obj.QTY_VARIENCE_PER
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceCostVarPer).Value = obj.COST_VARIENCE_PER
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceType).Value = obj.RESOURCE_Type
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceUnitCostUom).Value = obj.UNIT_COST_UOM
            Next
        End If
        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = objList
        Else
            gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = Nothing
        End If
        '' calculate resources cost
        CalculateResourceCost(Op_RowNo)

        CalculateMOCosting()
        'gvResources.Rows.AddNew()

        Return objList
    End Function

    'Function getToolTypeFromToolTypeMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOToolTypes)
    '    'Dim objList As New List(Of clsMOToolTypes)
    '    'Dim obj1 As clsMOToolTypes
    '    'Dim objList1 As List(Of clsWorkCenterToolDetail)

    '    'objList1 = clsWorkCenterToolDetail.GetData(WorkCenterCode, trans)
    '    'If Not objList1 Is Nothing And objList1.Count > 0 Then
    '    '    For Each obj As clsWorkCenterToolDetail In objList1
    '    '        obj1 = New clsMOToolTypes
    '    '        obj1.OPERATION_CODE = OperationCode
    '    '        obj1.WORK_CENTER_CODE = WorkCenterCode
    '    '        obj1.TOOL_TYPE_CODE = obj.TOOL_TYPE_CODE
    '    '        obj1.TOOL_TYPE_DESC = obj.DESCRIPTION
    '    '        obj1.QUANTITY = 1
    '    '        obj1.UNIT_COST = obj.UNIT_COST
    '    '        obj1.TOTAL_COST = obj.UNIT_COST
    '    '        obj1.UNIT_COST_UOM = obj.UNIT_CODE
    '    '        objList.Add(obj1)
    '    '    Next
    '    'End If
    '    'Return objList
    'End Function
    Function FillToolTypes(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMOToolTypes)
        Dim objList As New List(Of clsMOToolTypes)
        If isNewEntry Then
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag Is Nothing Then
                    'objList = getToolTypeFromToolTypeMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                Else
                    objList = gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag
                End If
            Else
                gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = Nothing
                '' nothing to do
            End If
        Else
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag Is Nothing Then
                    objList = clsMOToolTypes.GetMOToolTypes(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    If objList Is Nothing Or objList.Count = 0 Then
                        'objList = getToolTypeFromToolTypeMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    End If
                Else
                    objList = gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag
                End If
            Else
                gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = Nothing
                '' nothing to do
            End If
        End If
        '' fill resources grid
        Me.gvTools.Rows.Clear()
        If Not objList Is Nothing Then
            For Each obj As clsMOToolTypes In objList
                gvTools.Rows.AddNew()
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeCode).Value = obj.TOOL_TYPE_CODE
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeDesc).Value = obj.TOOL_TYPE_DESC

                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolRequirQty).Value = obj.REQUIRED_QUANTITY
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolUsedQty).Value = obj.USED_QUANTITY
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolUnitCost).Value = obj.UNIT_COST
                'gvTools.Rows(objList.IndexOf(obj)).Cells(colToolStdCost).Value = obj.STD_COST
                'gvTools.Rows(objList.IndexOf(obj)).Cells(colToolActualCost).Value = obj.ACTUAL_COST
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolQtyVarPer).Value = obj.QTY_VARIENCE_PER
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolCostVarPer).Value = obj.COST_VARIENCE_PER

                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolUnitCostUom).Value = obj.UNIT_COST_UOM

            Next
        End If
        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = objList
        Else
            gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = Nothing
        End If
        '' calculate tools cost
        CalculateTollTypeCost(Op_RowNo)

        CalculateMOCosting()
        'gvTools.Rows.AddNew()
        'gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = objList
        Return objList
    End Function

    'Private Sub gvOperations_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvOperations.CurrentColumnChanged
    '    If gvOperations.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gvOperations.CurrentRow.Index
    '        gvOperations.CurrentRow.Cells(colOperationLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gvOperations.Rows.Count - 1 Then
    '            If (clsCommon.myLen(txtCode.Value) > 0) Then
    '                gvOperations.Rows.AddNew()
    '                gvOperations.CurrentRow = gvOperations.Rows(intCurrRow)
    '                'gvOperations.CurrentRow.Cells(colOperationLineNo).Value = gvOperations.CurrentRow.Index + 1
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub gvResources_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvResources.CellEndEdit
        If gvOperations.CurrentRow Is Nothing Then
            Exit Sub
        End If
        If gvResources.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            'If e.Column Is gvResources.Columns(colResourceCode) Then
            '    Dim strq As String = ""
            '    Dim strWhrCls As String = ""
            '    strq = "SELECT TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE as Code,TSPL_MF_RESOURCE_MASTER.DESCRIPTION AS Name FROM " & _
            '           " TSPL_MF_WORK_CENTER_RESOURCE_DETAIL LEFT JOIN TSPL_MF_RESOURCE_MASTER " & _
            '           " ON TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE=TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE "
            '    strWhrCls = "TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.WORK_CENTER_CODE='" & gvOperations.CurrentRow.Cells(colworkCenterCode).Value & "'"

            '    gvResources.CurrentRow.Cells(colResourceCode).Value = clsCommon.ShowSelectForm("TSPL_MF_OPERATION", strq, "Code", strWhrCls, clsCommon.myCstr(gvResources.CurrentRow.Cells(colResourceCode).Value))
            '    Dim obj As New clsResourceMaster
            '    obj = clsResourceMaster.GetData(gvResources.CurrentRow.Cells(colResourceCode).Value, NavigatorType.Current)
            '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DESCRIPTION) > 0 Then
            '        gvResources.CurrentRow.Cells(colResourceDesc).Value = obj.DESCRIPTION
            '        gvResources.CurrentRow.Cells(colResourceQty).Value = 1
            '        gvResources.CurrentRow.Cells(colResourceUnitCost).Value = obj.COST
            '        gvResources.CurrentRow.Cells(colResourceType).Value = obj.RESOURCE_TYPE
            '        gvResources.CurrentRow.Cells(colResourceUnitCostUom).Value = obj.UNIT_CODE
            '    End If

            'End If


            CalculateResourceCost(Me.gvOperations.CurrentRow.Index)
            CalculateMOCosting()
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub CalculateResourceCost(ByVal op_row As Integer)
        Dim objList As New List(Of clsMOResources)
        Dim obj As clsMOResources
        For Each rw As GridViewRowInfo In gvResources.Rows
            If clsCommon.myLen(rw.Cells(colResourceCode).Value) > 0 Then

                'Me.gvResources.Rows(rw.Index).Cells(colResourceTotalCost).Value = Me.gvResources.Rows(rw.Index).Cells(colResourceQty).Value * Me.gvResources.Rows(rw.Index).Cells(colResourceUnitCost).Value
                obj = New clsMOResources
                obj.MO_CODE = Me.txtCode.Value
                obj.OPERATION_CODE = gvOperations.Rows(op_row).Cells(colOperationCode).Value
                obj.WORK_CENTER_CODE = gvOperations.Rows(op_row).Cells(colworkCenterCode).Value
                obj.RESOURCE_CODE = rw.Cells(colResourceCode).Value
                obj.RESOURCE_Desc = rw.Cells(colResourceDesc).Value
                obj.REQUIRED_QUANTITY = rw.Cells(colResourceRequirQty).Value
                obj.USED_QUANTITY = rw.Cells(colResourceUsedQty).Value

                obj.UNIT_COST = rw.Cells(colResourceUnitCost).Value
                obj.STD_COST = obj.REQUIRED_QUANTITY * obj.UNIT_COST
                obj.ACTUAL_COST = obj.USED_QUANTITY * obj.UNIT_COST
                obj.QTY_VARIENCE_PER = (obj.USED_QUANTITY - obj.REQUIRED_QUANTITY) / IIf(obj.REQUIRED_QUANTITY = 0, 1, obj.REQUIRED_QUANTITY) * 100
                obj.COST_VARIENCE_PER = (obj.ACTUAL_COST - obj.STD_COST) / IIf(obj.STD_COST = 0, 1, obj.STD_COST) * 100
                rw.Cells(colResourceQtyVarPer).Value = obj.QTY_VARIENCE_PER
                rw.Cells(colResourceCostVarPer).Value = obj.COST_VARIENCE_PER

                obj.RESOURCE_Type = rw.Cells(colResourceType).Value
                obj.UNIT_COST_UOM = rw.Cells(colResourceUnitCostUom).Value
                objList.Add(obj)
            End If
        Next
        If objList.Count > 0 Then
            gvOperations.Rows(op_row).Cells(colOperationCode).Tag = objList
        End If
        CalculateMOCosting()
    End Sub

    Sub CalculateTollTypeCost(ByVal op_row As Integer)
        Dim objList As New List(Of clsMOToolTypes)
        Dim obj As clsMOToolTypes
        For Each rw As GridViewRowInfo In gvTools.Rows
            If clsCommon.myLen(rw.Cells(colToolTypeCode).Value) > 0 Then

                'Me.gvTools.Rows(rw.Index).Cells(colToolTypeTotalCost).Value = Me.gvTools.Rows(rw.Index).Cells(colToolTypeQty).Value * Me.gvTools.Rows(rw.Index).Cells(colToolTypeUnitCost).Value
                obj = New clsMOToolTypes
                obj.MO_CODE = Me.txtCode.Value
                obj.OPERATION_CODE = gvOperations.Rows(op_row).Cells(colOperationCode).Value
                obj.WORK_CENTER_CODE = gvOperations.Rows(op_row).Cells(colworkCenterCode).Value
                obj.TOOL_TYPE_CODE = rw.Cells(colToolTypeCode).Value
                obj.TOOL_TYPE_DESC = rw.Cells(colToolTypeDesc).Value
                obj.REQUIRED_QUANTITY = rw.Cells(colToolRequirQty).Value
                obj.USED_QUANTITY = rw.Cells(colToolUsedQty).Value
                obj.UNIT_COST = rw.Cells(colToolUnitCost).Value

                obj.STD_COST = obj.REQUIRED_QUANTITY * obj.UNIT_COST
                obj.ACTUAL_COST = obj.USED_QUANTITY * obj.UNIT_COST
                obj.QTY_VARIENCE_PER = (obj.USED_QUANTITY - obj.REQUIRED_QUANTITY) / obj.REQUIRED_QUANTITY * 100
                obj.COST_VARIENCE_PER = (obj.ACTUAL_COST - obj.STD_COST) / obj.STD_COST * 100

                rw.Cells(colToolQtyVarPer).Value = obj.QTY_VARIENCE_PER
                rw.Cells(colToolCostVarPer).Value = obj.COST_VARIENCE_PER

                obj.UNIT_COST_UOM = rw.Cells(colToolUnitCostUom).Value
                objList.Add(obj)
            End If
        Next
        If objList.Count > 0 Then
            gvOperations.Rows(op_row).Cells(colworkCenterCode).Tag = objList
        End If
        CalculateMOCosting()
    End Sub


    'Private Sub gvResources_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvResources.CurrentColumnChanged
    '    If gvResources.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gvResources.CurrentRow.Index
    '        'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gvResources.Rows.Count - 1 Then
    '            If (clsCommon.myLen(txtCode.Value) > 0) Then
    '                gvResources.Rows.AddNew()
    '                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    '                gvResources.CurrentRow = gvResources.Rows(intCurrRow)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub gvTools_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTools.CellEndEdit
        If gvOperations.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If gvTools.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            'If e.Column Is gvTools.Columns(colToolTypeCode) Then
            '    Dim strq As String = ""
            '    Dim strWhrCls As String = ""
            '    strq = "SELECT TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE as Code,TSPL_MF_TOOL_TYPE.DESCRIPTION AS Name FROM " & _
            '           " TSPL_MF_WORK_CENTER_TOOL_DETAIL LEFT JOIN TSPL_MF_TOOL_TYPE " & _
            '           " ON TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE=TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE "
            '    strWhrCls = "TSPL_MF_WORK_CENTER_TOOL_DETAIL.WORK_CENTER_CODE='" & gvOperations.CurrentRow.Cells(colworkCenterCode).Value & "'"

            '    gvTools.CurrentRow.Cells(colToolTypeCode).Value = clsCommon.ShowSelectForm("tools", strq, "Code", strWhrCls, clsCommon.myCstr(gvTools.CurrentRow.Cells(colToolTypeCode).Value))
            '    Dim obj As New ClsMFToolType
            '    obj = ClsMFToolType.GetData(gvTools.CurrentRow.Cells(colToolTypeCode).Value, NavigatorType.Current)
            '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DESCRIPTION) > 0 Then
            '        gvTools.CurrentRow.Cells(colToolTypeDesc).Value = obj.DESCRIPTION
            '        gvTools.CurrentRow.Cells(colToolTypeQty).Value = 1
            '        gvTools.CurrentRow.Cells(colToolTypeUnitCost).Value = obj.COST_PER_UNIT
            '        gvTools.CurrentRow.Cells(colToolTypeUnitCostUom).Value = obj.UNIT_CODE
            '    End If

            'End If
            CalculateTollTypeCost(Me.gvOperations.CurrentRow.Index)
            CalculateMOCosting()
            isCellValueChangedOpen = False
        End If
    End Sub

    'Private Sub gvTools_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTools.CurrentColumnChanged
    '    If gvTools.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gvTools.CurrentRow.Index
    '        'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gvTools.Rows.Count - 1 Then
    '            If (clsCommon.myLen(txtCode.Value) > 0) Then
    '                gvTools.Rows.AddNew()
    '                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
    '                gvTools.CurrentRow = gvTools.Rows(intCurrRow)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub gvOperations_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gvOperations.CurrentRowChanged
        If Not gvOperations.CurrentRow Is Nothing Then
            FillResources(gvOperations.CurrentRow.Index)
            FillToolTypes(gvOperations.CurrentRow.Index)
        End If

    End Sub
    Sub ResetCosting()
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value = 0
        Me.gvCost.Rows(rowTotal).Cells(colStdCost).Value = 0

        '' set projected cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colProjectedCost).Value = 0
        Me.gvCost.Rows(rowTotal).Cells(colProjectedCost).Value = 0

        '' std and projected variance calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colStdProjVar).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colStdProjVar).Value = 0

        '' set actual cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colActualCost).Value = 0
        Me.gvCost.Rows(rowTotal).Cells(colActualCost).Value = 0

        '' std and actual variance calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colStdActualVar).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colStdActualVar).Value = 0
    End Sub
    Sub CalculateMOCosting()
        If clsCommon.myLen(txtProducedItem.Value) = 0 Then
            ResetCosting()
            Exit Sub
        End If
        Dim qry As String
        qry = "SELECT MATERIAL_COST,PACKAGING_COST,SETUP_COST,LABOR_COST,OVERHEAD_COST,SUBCONTRACT_COST,TOOL_COST,TOTAL_COST FROM TSPL_MF_ITEM_COST_MAINTENANCE WHERE ITEM_CODE='" & Me.txtProducedItem.Value.ToString & "'"
        Dim dtStd As DataTable
        dtStd = clsDBFuncationality.GetDataTable(qry)
        If dtStd.Rows.Count > 0 Then
            Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("MATERIAL_COST")
            Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("PACKAGING_COST")
            Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("SETUP_COST")
            Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("LABOR_COST")
            Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("OVERHEAD_COST")
            Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("SUBCONTRACT_COST")
            Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value = dtStd.Rows(0).Item("TOOL_COST")
            Me.gvCost.Rows(rowTotal).Cells(colStdCost).Value = dtStd.Rows(0).Item("TOTAL_COST")
        Else
            ResetCosting()
        End If
        '' calculation of calculated cost


        Dim DirectMaterialCost As Decimal = 0
        Dim PackagingCost As Decimal = 0

        Dim ResourceProjectedSetupCost As Decimal = 0
        Dim ResourceActualSetupCost As Decimal = 0
        Dim ResourceProjectedDLaborCost As Decimal = 0
        Dim ResourceActualDLaborCost As Decimal = 0
        Dim ResourceProjectedOverheadCost As Decimal = 0
        Dim ResourceActualOverheadCost As Decimal = 0
        Dim ToolProjectedCost As Decimal = 0
        Dim ToolActualCost As Decimal = 0
        Dim ProjectedSubContractedCost As Decimal = 0
        Dim ActualSubContractedCost As Decimal = 0

        '' calculation of materila and packaging cost
        Dim objCost As ClsCostMaintainance

        For Each row As GridViewRowInfo In gvBOM.Rows
            objCost = ClsCostMaintainance.GetData(row.Cells(colItemCode).Value, NavigatorType.Current)
            If objCost Is Nothing Then
                Continue For
            End If
            If row.Cells(colItemCode).Tag = "Direct" Then
                DirectMaterialCost = DirectMaterialCost + row.Cells(colqty).Value * objCost.MATERIAL_COST
            ElseIf row.Cells(colItemCode).Tag = "Packaging" Then
                PackagingCost = PackagingCost + row.Cells(colqty).Value * objCost.MATERIAL_COST
            End If
        Next

        '' calculation resources and tools cost calculation
        For Each row As GridViewRowInfo In Me.gvOperations.Rows
            '' resource cost calculation
            If row.Cells(colOperationCode).Tag IsNot Nothing Then
                Dim objlist As List(Of clsMOResources)

                objlist = row.Cells(colOperationCode).Tag
                For Each obj As clsMOResources In objlist
                    If obj.RESOURCE_Type = "Setup Labor" Then
                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceProjectedSetupCost = ResourceProjectedSetupCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * (row.Cells(colProjectedSetupTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceProjectedSetupCost = ResourceProjectedSetupCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * row.Cells(colProjectedSetupTimeMin).Value
                        End If
                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceActualSetupCost = ResourceActualSetupCost + obj.UNIT_COST * obj.USED_QUANTITY * (row.Cells(colActualSetupTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceActualSetupCost = ResourceActualSetupCost + obj.UNIT_COST * obj.USED_QUANTITY * row.Cells(colActualSetupTimeMin).Value
                        End If
                    ElseIf obj.RESOURCE_Type = "Run Labor" Then
                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceProjectedDLaborCost = ResourceProjectedDLaborCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * (row.Cells(colProjectedRunTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceProjectedDLaborCost = ResourceProjectedDLaborCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * row.Cells(colProjectedRunTimeMin).Value
                        End If

                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceActualDLaborCost = ResourceActualDLaborCost + obj.UNIT_COST * obj.USED_QUANTITY * (row.Cells(colActualRunTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceActualDLaborCost = ResourceActualDLaborCost + obj.UNIT_COST * obj.USED_QUANTITY * row.Cells(colActualRunTimeMin).Value
                        End If

                    ElseIf obj.RESOURCE_Type = "Overhead" Then
                        ResourceProjectedOverheadCost = ResourceProjectedOverheadCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY
                        ResourceActualOverheadCost = ResourceActualOverheadCost + obj.UNIT_COST * obj.USED_QUANTITY

                    End If
                Next
            End If

            '' tools cost calculation
            If row.Cells(colworkCenterCode).Tag IsNot Nothing Then
                Dim objlist As List(Of clsMOToolTypes)

                objlist = row.Cells(colworkCenterCode).Tag
                For Each obj As clsMOToolTypes In objlist
                    '' projected tool cost
                    If obj.UNIT_COST_UOM = "Hour" Then
                        ToolProjectedCost = ToolProjectedCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * (row.Cells(colProjectedRunTimeMin).Value / 60.0)
                    ElseIf obj.UNIT_COST_UOM = "Minute" Then
                        ToolProjectedCost = ToolProjectedCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY * row.Cells(colProjectedRunTimeMin).Value
                    Else
                        ToolProjectedCost = ToolProjectedCost + obj.UNIT_COST * obj.REQUIRED_QUANTITY
                    End If

                    '' actual tool cost
                    If obj.UNIT_COST_UOM = "Hour" Then
                        ToolActualCost = ToolActualCost + obj.UNIT_COST * obj.USED_QUANTITY * (row.Cells(colActualRunTimeMin).Value / 60.0)
                    ElseIf obj.UNIT_COST_UOM = "Minute" Then
                        ToolActualCost = ToolActualCost + obj.UNIT_COST * obj.USED_QUANTITY * row.Cells(colActualRunTimeMin).Value
                    Else
                        ToolActualCost = ToolActualCost + obj.UNIT_COST * obj.USED_QUANTITY
                    End If
                Next
            End If
        Next
        '' set projected cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colProjectedCost).Value = Format(DirectMaterialCost, "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colProjectedCost).Value = Format(PackagingCost, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colProjectedCost).Value = Format(ResourceProjectedSetupCost, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colProjectedCost).Value = Format(ResourceProjectedDLaborCost, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colProjectedCost).Value = Format(ResourceProjectedOverheadCost, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colProjectedCost).Value = Format(ProjectedSubContractedCost, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colProjectedCost).Value = Format(ToolProjectedCost, "###0.00")
        Me.gvCost.Rows(rowTotal).Cells(colProjectedCost).Value = Format(DirectMaterialCost + PackagingCost + ResourceProjectedSetupCost + ResourceProjectedDLaborCost + ResourceProjectedOverheadCost + ProjectedSubContractedCost + ToolProjectedCost, "###0.00")


        '' variance std and projected calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdProjVar).Value = Format(((DirectMaterialCost - Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colStdProjVar).Value = Format(((PackagingCost - Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdProjVar).Value = Format(((ResourceProjectedSetupCost - Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdProjVar).Value = Format(((ResourceProjectedDLaborCost - Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colStdProjVar).Value = Format(((ResourceProjectedOverheadCost - Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colStdProjVar).Value = Format(((ProjectedSubContractedCost - Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colStdProjVar).Value = Format(((ToolProjectedCost - Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value)) * 100, "###0.00")

        '' set actual cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colActualCost).Value = Format(clsMOCosting.GetMOActualDirectMaterialCost(txtCode.Value, Nothing), "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colActualCost).Value = Format(PackagingCost, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colActualCost).Value = Format(ResourceActualSetupCost, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colActualCost).Value = Format(ResourceActualDLaborCost, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colActualCost).Value = Format(ResourceActualOverheadCost, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colActualCost).Value = Format(ActualSubContractedCost, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colActualCost).Value = Format(ToolActualCost, "###0.00")
        Me.gvCost.Rows(rowTotal).Cells(colActualCost).Value = Format(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colActualCost).Value + PackagingCost + ResourceActualSetupCost + ResourceActualDLaborCost + ResourceActualOverheadCost + ActualSubContractedCost + ToolActualCost, "###0.00")

        '' variance std and actual calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdActualVar).Value = Format(((Me.gvCost.Rows(rowDirectMaterialCost).Cells(colActualCost).Value - Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colStdActualVar).Value = Format(((PackagingCost - Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdActualVar).Value = Format(((ResourceActualSetupCost - Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdActualVar).Value = Format(((ResourceActualDLaborCost - Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colStdActualVar).Value = Format(((ResourceActualOverheadCost - Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colStdActualVar).Value = Format(((ActualSubContractedCost - Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colStdActualVar).Value = Format(((ToolActualCost - Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value)) * 100, "###0.00")
    End Sub


    Private Sub gvResources_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvResources.UserDeletedRow
        If Me.gvOperations.CurrentRow IsNot Nothing Then
            CalculateResourceCost(gvOperations.CurrentRow.Index)
        End If

    End Sub

    Private Sub gvTools_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvTools.UserDeletedRow
        If Me.gvOperations.CurrentRow IsNot Nothing Then
            CalculateTollTypeCost(gvOperations.CurrentRow.Index)
        End If

    End Sub


    Private Sub fndBomCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBomCode._MYValidating
        If clsCommon.myLen(Me.txtProducedItem.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Item for MO first.")
            Exit Sub
        End If
        Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForBOM(clsCommon.myCstr(Me.fndBomCode.Value), isButtonClicked, clsCommon.myCstr(Me.txtProducedItem.Value))
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
            Me.fndBomCode.Value = obj.BOM_CODE
            Me.fndBomCode.Tag = obj.PROD_QUANTITY
            lblRevisionNo.Text = obj.REVISION_NO
            '' fill operations 
            FillOperations()
        Else
            Me.fndBomCode.Value = Nothing
            lblRevisionNo.Text = ""
        End If
    End Sub

    Private Sub cboMOStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboMOStatus.SelectedIndexChanged
        'If isNewEntry Then
        If Me.cboMOStatus.Text = "Open" Then
            Me.dtpCreatedDate.Checked = True
            Me.dtpApprovedDate.Checked = False
            Me.dtpReleasedDate.Checked = False
            Me.dtpCloseDate.Checked = False
        ElseIf Me.cboMOStatus.Text = "Approved" Then
            Me.dtpCreatedDate.Checked = True
            Me.dtpApprovedDate.Checked = True
            Me.dtpReleasedDate.Checked = False
            Me.dtpCloseDate.Checked = False
        ElseIf Me.cboMOStatus.Text = "Released" Then
            Me.dtpCreatedDate.Checked = True
            Me.dtpApprovedDate.Checked = False
            Me.dtpReleasedDate.Checked = True
            Me.dtpCloseDate.Checked = False
        ElseIf Me.cboMOStatus.Text = "Closed" Then
            Me.dtpCreatedDate.Checked = True
            Me.dtpApprovedDate.Checked = False
            Me.dtpReleasedDate.Checked = False
            Me.dtpCloseDate.Checked = True
        End If
        'End If
    End Sub
    Sub FillOperations()
        Dim objListOP As New List(Of clsBOMOperations)
        Dim objListRes As New List(Of clsBOMResources)
        Dim objListTools As New List(Of clsBOMToolTypes)

        Dim objTr As clsBillOfMaterial
        objTr = clsBillOfMaterial.GetData(Me.fndBomCode.Value, NavigatorType.Current)
        objListOP = objTr.ObjListOP
        objListRes = objTr.ObjListRes
        objListTools = objTr.ObjListToolType

        If objListOP IsNot Nothing And objListOP.Count > 0 Then
            gvOperations.Rows.Clear()
            For Each objOP As clsBOMOperations In objListOP
                gvOperations.Rows.AddNew()
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationLineNo).Value = objOP.Line_No
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Value = objOP.OPERATION_CODE
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Value = objOP.WORK_CENTER_CODE

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedSetupTimeMin).Value = objOP.SETUP_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedRunTimeMin).Value = objOP.RUN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedCleanTimeMin).Value = objOP.CLEAN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colProjectedWaitTimeMin).Value = objOP.WAIT_TIME_MINUTES

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualSetupTimeMin).Value = objOP.SETUP_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualRunTimeMin).Value = objOP.RUN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualCleanTimeMin).Value = objOP.CLEAN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colActualWaitTimeMin).Value = objOP.WAIT_TIME_MINUTES

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationRemarks).Value = objOP.REMARKS

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Tag = GetMOResources(clsBOMResources.GetBomResources(Me.fndBomCode.Value, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE))
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Tag = GetMOToolTypes(clsBOMToolTypes.GetBomToolTypes(Me.fndBomCode.Value, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE))

                Dim obj As New clsOperationMaster
                obj = obj.GetData(objOP.OPERATION_CODE, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Descraption) > 0 Then
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationDesc).Value = obj.Descraption
                End If


            Next
        End If
        '' fill material
        fillMaterial(objTr)
        '' calculate costing
        CalculateMOCosting()

    End Sub
    Sub fillMaterial(ByVal objTr As clsBillOfMaterial)
        '' fill material
        gvBOM.Rows.Clear()
        If clsBillOfMaterial.ObjList IsNot Nothing AndAlso clsBillOfMaterial.ObjList.Count > 0 Then
            For Each objTr1 As clsBillOfMaterial In clsBillOfMaterial.ObjList
                gvBOM.Rows.AddNew()

                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = objTr1.Line_No
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCategoryCode).Value = objTr1.CONSM_ITEM_CATEGORY_CODE
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = objTr1.CONSM_ITEM_CODE
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = objTr1.ITEM_DESCRIPTION
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colBOMqty).Value = objTr1.CONSM_QUANTITY
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = ((objTr1.CONSM_QUANTITY * clsCommon.myCdbl(Me.txtOrderedQtyStockUnit.Text)) / IIf(Me.fndBomCode.Tag = 0 Or Me.fndBomCode.Tag Is Nothing, 1, Me.fndBomCode.Tag)) / GetBulidQty(fndBomCode.Value)
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = objTr1.CONSM_ITEM_UNIT_CODE
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colscrap_per).Value = objTr1.SCRAP_PERCENT
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colwastage_per).Value = objTr1.WASTAGE_PERCENT
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = objTr1.REMARKS
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemType).Value = objTr1.ITEM_TYPE

                If clsCommon.myLen(fndLocation.Value) > 0 Then
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colStockqty).Value = clsItemLocationDetails.getBalance(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value, Me.fndLocation.Value, Me.txtCode.Value, Me.dtpMODate.Value, Nothing, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value, 0)
                Else
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colStockqty).Value = 0
                End If
            Next
        Else
            'gvBOM.Rows.AddNew()
        End If

    End Sub
    Function GetBulidQty(ByVal BOMCode As String)
        Dim strBulidQty As Integer = 0
        strBulidQty = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_MF_BOM_HEAD.PROD_QUANTITY  from TSPL_MF_BOM_HEAD where BOM_CODE ='" & BOMCode & "' "))
        Return strBulidQty
    End Function

    Sub updateMaterialQty()
        For Each rw As GridViewRowInfo In gvBOM.Rows
            rw.Cells(colqty).Value = (rw.Cells(colBOMqty).Value * clsCommon.myCdbl(Me.txtOrderedQtyStockUnit.Text)) / IIf(Me.fndBomCode.Tag = 0 Or Me.fndBomCode.Tag Is Nothing, 1, Me.fndBomCode.Tag)
            If clsCommon.myCdbl(GetBulidQty(fndBomCode.Value)) > 0 Then
                rw.Cells(colqty).Value = ((rw.Cells(colBOMqty).Value * clsCommon.myCdbl(Me.txtOrderedQtyStockUnit.Text)) / IIf(Me.fndBomCode.Tag = 0 Or Me.fndBomCode.Tag Is Nothing, 1, Me.fndBomCode.Tag)) / GetBulidQty(fndBomCode.Value)
            Else
                rw.Cells(colqty).Value = 0
            End If

        Next
    End Sub
    Function GetMOResources(ByVal objList As List(Of clsBOMResources)) As List(Of clsMOResources)
        Dim oblListMORes As New List(Of clsMOResources)
        Dim objMOResTr As clsMOResources

        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            For Each objtr As clsBOMResources In objList
                objMOResTr = New clsMOResources
                objMOResTr.OPERATION_CODE = objtr.OPERATION_CODE
                objMOResTr.WORK_CENTER_CODE = objtr.WORK_CENTER_CODE
                objMOResTr.RESOURCE_CODE = objtr.RESOURCE_CODE
                objMOResTr.RESOURCE_Desc = objtr.RESOURCE_Desc
                objMOResTr.RESOURCE_Type = objtr.RESOURCE_Type
                objMOResTr.REQUIRED_QUANTITY = objtr.QUANTITY
                objMOResTr.USED_QUANTITY = objtr.QUANTITY
                objMOResTr.UNIT_COST = objtr.UNIT_COST
                objMOResTr.UNIT_COST_UOM = objtr.UNIT_COST_UOM

                oblListMORes.Add(objMOResTr)
            Next
        End If

        If oblListMORes IsNot Nothing AndAlso oblListMORes.Count > 0 Then
            Return oblListMORes
        Else
            Return Nothing
        End If
    End Function

    Function GetMOToolTypes(ByVal objList As List(Of clsBOMToolTypes)) As List(Of clsMOToolTypes)
        Dim oblListMOTools As New List(Of clsMOToolTypes)
        Dim objMOtoolTr As clsMOToolTypes

        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            For Each objtr As clsBOMToolTypes In objList
                objMOtoolTr = New clsMOToolTypes
                objMOtoolTr.OPERATION_CODE = objtr.OPERATION_CODE
                objMOtoolTr.WORK_CENTER_CODE = objtr.WORK_CENTER_CODE
                objMOtoolTr.TOOL_TYPE_CODE = objtr.TOOL_TYPE_CODE
                objMOtoolTr.TOOL_TYPE_DESC = objtr.TOOL_TYPE_DESC
                objMOtoolTr.REQUIRED_QUANTITY = objtr.QUANTITY
                objMOtoolTr.USED_QUANTITY = objtr.QUANTITY
                objMOtoolTr.UNIT_COST = objtr.UNIT_COST
                objMOtoolTr.UNIT_COST_UOM = objtr.UNIT_COST_UOM

                oblListMOTools.Add(objMOtoolTr)
            Next
        End If
        If oblListMOTools IsNot Nothing AndAlso oblListMOTools.Count > 0 Then
            Return oblListMOTools
        Else
            Return Nothing
        End If
    End Function

    Private Sub fndOrderQtyUOM__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndOrderQtyUOM._MYValidating
        Try

            Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as [Code],TSPL_UNIT_MASTER.Unit_Desc as [Name], " & _
            " TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Conversion Factor],TSPL_ITEM_UOM_DETAIL.Stocking_Unit as [Is Stock Unit] " & _
            " from TSPL_ITEM_UOM_DETAIL inner join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code "
            '" where  TSPL_ITEM_UOM_DETAIL.Item_Code='ROD-5MTR'; "
            Dim WhrClause As String
            WhrClause = "  TSPL_ITEM_UOM_DETAIL.Item_Code='" & Me.txtProducedItem.Value.ToString & "' "
            fndOrderQtyUOM.Value = clsCommon.ShowSelectForm("TSPL_ITEM_UOM_DETAIL", qry, "Code", WhrClause, fndOrderQtyUOM.Value, "", isButtonClicked)
            ConvertStockQty()
        Catch ex As Exception

        End Try
    End Sub
    Sub ConvertStockQty()
        If clsCommon.CompairString(Me.fndOrderQtyUOM.Value, Me.lblUnitName.Text) = CompairStringResult.Equal Then
            Me.txtOrderedQtyStockUnit.Text = Me.txtOrderedQty.Text
        Else
            Dim dt As DataTable
            Dim qry As String = "select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Me.txtProducedItem.Value.ToString & "' and UOM_Code='" & Me.fndOrderQtyUOM.Value.ToString & "'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Me.txtOrderedQtyStockUnit.Text = clsCommon.myCdbl(Me.txtOrderedQty.Text) * (1 / dt.Rows(0).Item("Conversion_Factor"))
            Else
                Me.txtOrderedQtyStockUnit.Text = Me.txtOrderedQty.Text
            End If
        End If
    End Sub

    Private Sub txtOrderedQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderedQty.TextChanged
        ConvertStockQty()
    End Sub

    Private Sub fndPlanner__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPlanner._MYValidating
        Try
            Dim qry As String = "SELECT User_Code as [Code],User_Name as [Name] FROM TSPL_USER_MASTER"
            fndPlanner.Value = clsCommon.ShowSelectForm("fndPlanner", qry, "Code", "", fndPlanner.Value, "", isButtonClicked)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fndInCharge__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndInCharge._MYValidating
        Try
            Dim qry As String = "SELECT User_Code as [Code],User_Name as [Name] FROM TSPL_USER_MASTER"
            fndInCharge.Value = clsCommon.ShowSelectForm("fndInCharge", qry, "Code", "", fndInCharge.Value, "", isButtonClicked)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fndProductionLine__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProductionLine._MYValidating
        Try
            Dim qry As String = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME as Name from TSPL_MF_PRODUCTION_LINES"
            fndProductionLine.Value = clsCommon.ShowSelectForm("fndProductionLine", qry, "Code", "", fndProductionLine.Value, "", isButtonClicked)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtOrderedQtyStockUnit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderedQtyStockUnit.TextChanged
        updateMaterialQty()
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            'Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsLocation.getFinder(WhrCls, fndLocation.Value, isButtonClicked)
            lblLocationDesc.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            FillOperations()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub cboSourceDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboSourceDocType.SelectedIndexChanged
        If isInsideLoad Then
            Exit Sub
        End If
        If clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "Individual") = CompairStringResult.Equal Then
            Me.fndProductionPlan.Value = Nothing
            Me.txtProductionPlanDesc.Text = ""
            Me.fndSONo.Value = Nothing
            Me.txtSODesc.Text = ""
            Me.fndProductionPlan.Enabled = False
            Me.fndSONo.Enabled = False
        ElseIf clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "PP") = CompairStringResult.Equal Then
            Me.fndProductionPlan.Value = Nothing
            Me.txtProductionPlanDesc.Text = ""
            Me.fndSONo.Value = Nothing
            Me.txtSODesc.Text = ""
            Me.fndProductionPlan.Enabled = True
            Me.fndSONo.Enabled = False
        ElseIf clsCommon.CompairString(Me.cboSourceDocType.SelectedValue, "SO") = CompairStringResult.Equal Then
            Me.fndProductionPlan.Value = Nothing
            Me.txtProductionPlanDesc.Text = ""
            Me.fndSONo.Value = Nothing
            Me.txtSODesc.Text = ""
            Me.fndProductionPlan.Enabled = False
            Me.fndSONo.Enabled = True
        End If
    End Sub

    Private Sub fndProductionPlan__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProductionPlan._MYValidating
        Me.fndProductionPlan.Value = clsProductionPlanning.GetFinder("", Me.fndProductionPlan.Value, isButtonClicked)
        If clsCommon.myLen(Me.fndProductionPlan.Value) > 0 Then
            Dim objPP As clsProductionPlanning = clsProductionPlanning.GetData(Me.fndProductionPlan.Value, NavigatorType.Current)
            If Not objPP Is Nothing Then
                Me.txtProductionPlanDesc.Text = objPP.DESCRIPTION
                'Me.dtpFromDate.Value = objPP.PLAN_FOR_DATE
                'Me.dtpToDate.Value = objPP.PLAN_TO_DATE
            End If
        End If
    End Sub

    Private Sub fndSONo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSONo._MYValidating
        Me.fndSONo.Value = clsManufacturingOrder.FinderForSO(Me.fndSONo.Value, isButtonClicked)
        If clsCommon.myLen(Me.fndSONo.Value) > 0 Then
            Me.txtSODesc.Text = clsManufacturingOrder.GetSODescription(Me.fndSONo.Value)
        End If
    End Sub

End Class