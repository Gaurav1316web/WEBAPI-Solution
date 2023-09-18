'Namespace Telerik.Examples.WinControls.GridView.Export.ExportHierarchy
'==============BM00000003812===================================
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Text
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.UI.Export
Imports System.ComponentModel
Imports System.Drawing


Public Class frmBillOfMaterialCosting
    Inherits FrmMainTranScreen

    Dim clsWriter As System.IO.StreamWriter

#Region "Variables"
    '' component grid columns
    Dim IsForAutoIndustry As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colItemCategoryCode As String = "ItemCategoryCode"
    Const colDrawingNo As String = "DrawingNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colqty As String = "Qty"
    Const colPercentage As String = "Percentage"
    Const colAlternateItemCode As String = "AlternateItemCode"
    Const colAlternateitemDesc As String = "AlternateItemDesc"
    Const colUnitCode As String = "UnitCode"
    Const colscrap_per As String = "Scrap_per"
    Const colwastage_per As String = "Wastage_per"
    Const colRemarks As String = "Remarks"
    Const colPrinci As String = "Principle"
    Dim Princi_On As Boolean = False
    Dim RunProductionBaseOnPercentage As Boolean = False
    Const colItemType As String = "colItemType"
    '' cost grid columns
    Const colCostType As String = "colCostType"
    Const colStdCost As String = "colStdCost"
    Const colCalcCost As String = "colCalcCost"
    Const colperVariance As String = "colperVariance"

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
    Const colSetupTimeMin As String = "colSetupTimeMin"
    Const colRunTimeMin As String = "colRunTimeMin"
    Const colCleanTimeMin As String = "colCleanTimeMin"
    Const colWaitTimeMin As String = "colWaitTimeMin"
    Const colperOverlap As String = "colperOverlap"
    Const colOperationRemarks As String = "colOperationRemarks"

    '' resource grid columns
    Const colResourceCode As String = "colResourceCode"
    Const colResourceDesc As String = "colResourceDesc"
    Const colResourceQty As String = "colResourceQty"
    Const colResourceUnitCost As String = "colResourceUnitCost"
    Const colResourceTotalCost As String = "colResourceTotalCost"
    Const colResourceType As String = "colResourceType"
    Const colResourceUnitCostUom As String = "colResourceUnitCostUom"

    '' tool type grid columns
    Const colToolTypeCode As String = "colToolTypeCode"
    Const colToolTypeDesc As String = "colToolTypeDesc"
    Const colToolTypeQty As String = "colToolTypeQty"
    Const colToolTypeUnitCost As String = "colToolTypeUnitCost"
    Const colToolTypeTotalCost As String = "colToolTypeTotalCost"
    Const colToolTypeUnitCostUom As String = "colToolTypeUnitCostUom"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsBillOfMaterial
    Private ObjList As New List(Of clsBillOfMaterial)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
    Public isInsideLoadData As Boolean = False
    Dim arrLoc As String = Nothing
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

    Sub LoadCostGridColumns()
        gvCost.Rows.Clear()
        gvCost.Columns.Clear()
        Dim CostType As New GridViewTextBoxColumn
        Dim StdCost As New GridViewDecimalColumn
        Dim CalcCost As New GridViewDecimalColumn
        Dim perVariance As New GridViewDecimalColumn

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
        StdCost.ReadOnly = False
        StdCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(StdCost)

        CalcCost.FormatString = ""
        CalcCost.HeaderText = "Calculated Cost"
        CalcCost.Name = colCalcCost
        CalcCost.Width = 150
        CalcCost.ReadOnly = True
        CalcCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(CalcCost)

        perVariance.FormatString = ""
        perVariance.HeaderText = "Variance(%)"
        perVariance.Name = colperVariance
        perVariance.Width = 150
        perVariance.ReadOnly = True
        perVariance.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCost.Columns.Add(perVariance)

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
        Dim SetupTimeMin As New GridViewDecimalColumn
        Dim RunTimeMin As New GridViewDecimalColumn
        Dim CleanTimeMin As New GridViewDecimalColumn
        Dim WaitTimeMin As New GridViewDecimalColumn
        Dim perOverlap As New GridViewDecimalColumn
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
        'OperationCode.ReadOnly = True
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
        'workCenterCode.ReadOnly = True
        workCenterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOperations.Columns.Add(workCenterCode)

        SetupTimeMin.FormatString = ""
        SetupTimeMin.HeaderText = "Setup Time(Minutes)"
        SetupTimeMin.Name = colSetupTimeMin
        SetupTimeMin.Width = 120
        'SetupTimeMin.ReadOnly = True
        SetupTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(SetupTimeMin)

        RunTimeMin.FormatString = ""
        RunTimeMin.HeaderText = "Run Time(Minutes)"
        RunTimeMin.Name = colRunTimeMin
        RunTimeMin.Width = 120
        'RunTimeMin.ReadOnly = True
        RunTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(RunTimeMin)

        CleanTimeMin.FormatString = ""
        CleanTimeMin.HeaderText = "Clean Time(Minutes)"
        CleanTimeMin.Name = colCleanTimeMin
        CleanTimeMin.Width = 120
        'CleanTimeMin.ReadOnly = True
        CleanTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(CleanTimeMin)

        WaitTimeMin.FormatString = ""
        WaitTimeMin.HeaderText = "Wait Time(Minutes)"
        WaitTimeMin.Name = colWaitTimeMin
        WaitTimeMin.Width = 120
        'WaitTimeMin.ReadOnly = True
        WaitTimeMin.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(WaitTimeMin)

        perOverlap.FormatString = ""
        perOverlap.HeaderText = "Overlap(%)"
        perOverlap.Name = colperOverlap
        perOverlap.Width = 100
        'perOverlap.ReadOnly = True
        perOverlap.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOperations.Columns.Add(perOverlap)

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
        Dim ResourceQty As New GridViewDecimalColumn
        Dim ResourceUnitCost As New GridViewDecimalColumn
        Dim ResourceTotalCost As New GridViewDecimalColumn
        Dim ResourceType As New GridViewTextBoxColumn
        Dim ResourceUnitCostUOM As New GridViewTextBoxColumn

        ResourceCode.FormatString = ""
        ResourceCode.HeaderText = "Resource Code"
        ResourceCode.Name = colResourceCode
        ResourceCode.Width = 100
        'ResourceCode.ReadOnly = True
        ResourceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResources.Columns.Add(ResourceCode)

        ResourceDesc.FormatString = ""
        ResourceDesc.HeaderText = "Description"
        ResourceDesc.Name = colResourceDesc
        ResourceDesc.Width = 100
        ResourceDesc.ReadOnly = True
        ResourceDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResources.Columns.Add(ResourceDesc)

        ResourceQty.FormatString = ""
        ResourceQty.HeaderText = "Quantity"
        ResourceQty.Name = colResourceQty
        ResourceQty.Width = 100
        'ResourceQty.ReadOnly = True
        ResourceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceQty)

        ResourceUnitCost.FormatString = ""
        ResourceUnitCost.HeaderText = "Unit Cost"
        ResourceUnitCost.Name = colResourceUnitCost
        ResourceUnitCost.Width = 100
        'ResourceUnitCost.ReadOnly = True
        ResourceUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceUnitCost)

        ResourceTotalCost.FormatString = ""
        ResourceTotalCost.HeaderText = "Total Cost"
        ResourceTotalCost.Name = colResourceTotalCost
        ResourceTotalCost.Width = 100
        'ResourceTotalCost.ReadOnly = True
        ResourceTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResources.Columns.Add(ResourceTotalCost)

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
        Dim ToolTypeQty As New GridViewDecimalColumn
        Dim ToolTypeUnitCost As New GridViewDecimalColumn
        Dim ToolTypeTotalCost As New GridViewDecimalColumn
        Dim ToolTypeUnitCostUOM As New GridViewTextBoxColumn

        ToolTypeCode.FormatString = ""
        ToolTypeCode.HeaderText = "Tool Type Code"
        ToolTypeCode.Name = colToolTypeCode
        ToolTypeCode.Width = 100
        'ToolTypeCode.ReadOnly = True
        ToolTypeCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTools.Columns.Add(ToolTypeCode)

        ToolTypeDesc.FormatString = ""
        ToolTypeDesc.HeaderText = "Description"
        ToolTypeDesc.Name = colToolTypeDesc
        ToolTypeDesc.Width = 100
        ToolTypeDesc.ReadOnly = True
        ToolTypeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolTypeDesc)

        ToolTypeQty.FormatString = ""
        ToolTypeQty.HeaderText = "Quantity"
        ToolTypeQty.Name = colToolTypeQty
        ToolTypeQty.Width = 100
        'ToolTypeQty.ReadOnly = True
        ToolTypeQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolTypeQty)

        ToolTypeUnitCost.FormatString = ""
        ToolTypeUnitCost.HeaderText = "Unit Cost"
        ToolTypeUnitCost.Name = colToolTypeUnitCost
        ToolTypeUnitCost.Width = 100
        'ToolTypeUnitCost.ReadOnly = True
        ToolTypeUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolTypeUnitCost)

        ToolTypeTotalCost.FormatString = ""
        ToolTypeTotalCost.HeaderText = "Total Cost"
        ToolTypeTotalCost.Name = colToolTypeTotalCost
        ToolTypeTotalCost.Width = 100
        'ToolTypeTotalCost.ReadOnly = True
        ToolTypeTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTools.Columns.Add(ToolTypeTotalCost)

        ToolTypeUnitCostUOM.FormatString = ""
        ToolTypeUnitCostUOM.HeaderText = "Unit Cost UOM"
        ToolTypeUnitCostUOM.Name = colToolTypeUnitCostUom
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
        Dim qty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim scrap_per As New GridViewDecimalColumn
        Dim wastage_per As New GridViewDecimalColumn
        Dim remarks As New GridViewTextBoxColumn
        Dim itemType As New GridViewTextBoxColumn
        Dim Percentage As New GridViewDecimalColumn
        Dim AlternateItemCode As New GridViewTextBoxColumn
        Dim AlternateitemDesc As New GridViewTextBoxColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)



        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        'ItemCode.ReadOnly = True
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

        itemType = New GridViewTextBoxColumn()
        itemType.FormatString = ""
        itemType.HeaderText = "Drawing No"
        itemType.Name = colDrawingNo
        itemType.Width = 100
        itemType.ReadOnly = True
        itemType.IsVisible = False
        If IsForAutoIndustry Then
            itemType.IsVisible = True
        End If
        itemType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(itemType)

        Dim repoprinci As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoprinci.Width = 60
        repoprinci.HeaderText = "Principle"
        repoprinci.Name = colPrinci
        repoprinci.IsVisible = False
        If Princi_On Then
            repoprinci.IsVisible = True
        End If
        gvBOM.Columns.Add(repoprinci)

        Percentage.FormatString = "{0:N6}"
        Percentage.DecimalPlaces = 6
        Percentage.HeaderText = "Percentage"
        Percentage.Name = colPercentage
        Percentage.Width = 100
        Percentage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Percentage.IsVisible = RunProductionBaseOnPercentage
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
            Percentage.IsVisible = True
        End If
        gvBOM.Columns.Add(Percentage)

        qty.FormatString = "{0:N4}"
        qty.DecimalPlaces = 4
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        qty.IsVisible = RunProductionBaseOnPercentage 'Not RunProductionBaseOnPercentage
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
            qty.IsVisible = True
        End If
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


        AlternateItemCode.FormatString = ""
        AlternateItemCode.HeaderText = "Alternate Item Code"
        AlternateItemCode.Name = colAlternateItemCode
        AlternateItemCode.Width = 100
        'ItemCode.ReadOnly = True
        AlternateItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(AlternateItemCode)

        AlternateitemDesc.FormatString = ""
        AlternateitemDesc.HeaderText = "Alternate Item Description"
        AlternateitemDesc.Name = colAlternateitemDesc
        AlternateitemDesc.Width = 100
        AlternateitemDesc.ReadOnly = True
        AlternateitemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(AlternateitemDesc)

        ItemCategoryCode.FormatString = ""
        ItemCategoryCode.HeaderText = "Item Category"
        ItemCategoryCode.Name = colItemCategoryCode
        ItemCategoryCode.Width = 100
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
            ItemCategoryCode.IsVisible = True
        End If
        'ItemCategoryCode.ReadOnly = True
        ItemCategoryCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCategoryCode)

    End Sub


    Private Sub frmBillOfMaterialCosting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                Save()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
                DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                funClose()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
                PostData()
            End If

            If gvBOM.CurrentRow Is Nothing Then
                Exit Sub
            End If

            'If gvBOM.CurrentRow.Cells(0).Value = "" Then
            '    gvBOM.CurrentRow.Cells(0).Value = gvBOM.RowCount
            'End If

            isCellValueChangedOpen = True
            If e.KeyData = Keys.F2 AndAlso gvBOM.Columns IsNot Nothing AndAlso gvBOM.Columns Is gvBOM.Columns(colItemCategoryCode) Then
                Dim strq As String = ""
                strq = "select PROD_ITEM_CATEGORY_CODE as Code,DESCRIPTION from TSPL_MF_PRODUCTION_ITEM_CATEGORY "
                gvBOM.CurrentRow.Cells(colItemCategoryCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCategoryCode).Value), "Code", True)
            End If
            If e.KeyData = Keys.F2 AndAlso gvBOM.Columns IsNot Nothing AndAlso gvBOM.Columns Is gvBOM.Columns(colItemCode) Then
                Dim whrcls As String = " ITEM_TYPE IN ('R','O','S') "

                'If IsForAutoIndustry Then
                '    gvBOM.CurrentRow.Cells(colDrawingNo).Value = clsItemMaster.GetFinderForDrawingNo("", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), True)

                '    whrcls += " and isnull(drawing_no,'')='" + clsCommon.myCstr(gvBOM.CurrentRow.Cells(colDrawingNo).Value) + "'"

                'End If
                Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), whrcls, True)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                    gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                    gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                    gvBOM.CurrentRow.Cells(colItemType).Value = obj.ITEM_TYPE
                    gvBOM.CurrentRow.Cells(colDrawingNo).Value = obj.CONSM_Drawing_No
                Else
                    gvBOM.CurrentRow.Cells(colItemCode).Value = Nothing
                    gvBOM.CurrentRow.Cells(colitemDesc).Value = Nothing
                    gvBOM.CurrentRow.Cells(colUnitCode).Value = Nothing
                    gvBOM.CurrentRow.Cells(colItemType).Value = Nothing
                    gvBOM.CurrentRow.Cells(colDrawingNo).Value = Nothing
                End If
            End If
            isCellValueChangedOpen = False
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmBillOfMaterialCosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Princi_On = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.Princi_Bom, clsFixedParameterCode.Princi_Bom, Nothing) = "0", False, True))
        IsForAutoIndustry = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing) = "A", True, False))
        RunProductionBaseOnPercentage = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.RunProductionBaseOnPercentage, clsFixedParameterCode.RunProductionBaseOnPercentage, Nothing) = "0", False, True))
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

        btntreeview.Visible = True
        'pageOperations.Hide()
        RadPageView1.SelectedPage = pageGeneral

        If IsForAutoIndustry Then
            MyLabel1.Visible = True
            txtdrawingNo.Visible = True
        End If

        '===============================================
        RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed

        If objCommonVar.RCDFCFP = True Then
            RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
            pageGeneral.Item.Visibility = ElementVisibility.Collapsed
            pageOperations.Item.Visibility = ElementVisibility.Collapsed
            pageComponent.Item.Visibility = ElementVisibility.Visible
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = pageComponent
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "'"))
        'txtConsmLocOther.Value = txtLocation.Value
        'RadPageView1.Pages(pageOperations.Name).Item.Visibility = ElementVisibility.Collapsed
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBillOfMaterialCosting)
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

            txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "'"))
            'txtConsmLocOther.Value = txtLocation.Value
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        lblRevisionNo.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtdrawingNo.Text = ""
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        btnCopy.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        Me.txtDescription.Text = ""
        Me.txtProducedItem.Value = Nothing
        Me.lblMasterItemName.Text = ""
        Me.txtBuildQty.Text = ""
        Me.dtpBOMDate.Value = Today
        Me.dtpStartDate.Value = Today
        Me.dtpEndDate.Value = Today
        Me.cboBOMStatus.SelectedValue = "Open"
        Me.chkDefaultBOM.Checked = False
        Me.txtBuildQty.Text = ""
        Me.txtMinBatchQty.Text = ""
        txtDocPath.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvBOM.Rows.Clear()
        Me.gvBOM.Rows.AddNew()
        Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode

        Me.gvBOM.Rows.Clear()
        Me.gvBOM.Rows.AddNew()

        Me.gvOperations.Rows.Clear()
        Me.gvOperations.Rows.AddNew()

        Me.gvResources.Rows.Clear()
        Me.gvResources.Rows.AddNew()

        Me.gvTools.Rows.Clear()
        Me.gvTools.Rows.AddNew()

        Me.gv_tree.DataSource = Nothing
        'Me.RadGridView1.Relations.Clear()
        'Me.RadGridView1.MasterTemplate.HierarchyDataProvider = Nothing
        'Me.RadGridView1.DataSource = Nothing
        LOCATIONRIGTHS()
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
        obj = clsBillOfMaterial.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0) Then

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
            txtCode.Value = obj.BOM_CODE
            Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.dtpBOMDate.Value = obj.BOM_DATE
            'Dim Sqlqry As String = "select REVISION_NO from TSPL_MF_BOM_HEAD
            '                        where TSPL_MF_BOM_HEAD.REVISION_NO ='" + (lblRevisionNo.Text + 1) + "'"
            'clsCommon.myLen(lblRevisionNo)
            'Me.lblRevisionNo.Text = obj.REVISION_NO 
            Me.lblRevisionNo.Text = obj.REVISION_NO
            'If lblRevisionNo.Text > 0 Then
            '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            '    Dim qry As String = ""
            '    qry = "SELECT REVISION_NO from TSPL_MF_BOM_HEAD where REVISION_NO='" + lblRevisionNo.Text + 1.ToString + "'"
            '    Dim dt As DataTable
            '    dt = clsDBFuncationality.GetDataTable(qry, trans)

            'End If
            'If clsCommon.myLen(obj.REVISION_NO) > 0 Then
            '    lblRevisionNo.Text = clsCommon.myCstr(obj.REVISION_NO + 1)
            'End If

            Me.dtpStartDate.Value = obj.START_DATE
            If clsCommon.myLen(obj.END_DATE) > 0 Then
                Me.dtpEndDate.Value = obj.END_DATE
                Me.dtpEndDate.Checked = True
            Else
                Me.dtpEndDate.Checked = False
            End If
            txtLocation.Value = obj.LOCATION_CODE
            lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
            Me.cboBOMStatus.Text = obj.STATUS
            Me.chkDefaultBOM.Checked = obj.IS_DEFAULT
            Me.txtDocPath.Text = obj.ATTACHED_DOC_PATH
            Me.txtProducedItem.Value = obj.PROD_ITEM_CODE
            Me.txtBuildQty.Text = obj.PROD_QUANTITY
            Me.lblUnitName.Text = obj.PROD_ITEM_UNIT_CODE
            Me.txtMinBatchQty.Text = obj.MIN_BATCH_SIZE
            txtdrawingNo.Text = obj.PROD_Drawing_No
            Me.lblCreatedByName.Text = obj.CREATED_BY
            If obj.POSTED = True Then
                Me.lblApprovedByName.Text = obj.APPROVED_BY
            Else
                Me.lblApprovedByName.Text = ""
            End If

            Me.lblMasterItemName.Text = obj.PROD_ITEM_NAME
            If clsCommon.myLen(obj.ATTACHED_DOC_PATH) > 0 Then
                btnBrowse.Text = "Download"
            Else
                btnBrowse.Text = "Browse"
            End If
            If (clsBillOfMaterial.ObjList IsNot Nothing AndAlso clsBillOfMaterial.ObjList.Count > 0) Then
                For Each obj As clsBillOfMaterial In clsBillOfMaterial.ObjList
                    gvBOM.Rows.AddNew()

                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCategoryCode).Value = obj.CONSM_ITEM_CATEGORY_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj.CONSM_ITEM_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = obj.CONSM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj.CONSM_ITEM_UNIT_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colscrap_per).Value = obj.SCRAP_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colwastage_per).Value = obj.WASTAGE_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colPrinci).Value = IIf(obj.Is_Principle = "1", True, False)
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemType).Value = obj.ITEM_TYPE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colDrawingNo).Value = obj.CONSM_Drawing_No
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAlternateItemCode).Value = obj.Alternate_Item_Code
                    If clsCommon.myLen(obj.Alternate_Item_Code) > 0 Then
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAlternateitemDesc).Value = clsItemMaster.GetItemName(obj.Alternate_Item_Code, Nothing)
                    End If
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colPercentage).Value = obj.Percentage
                Next
            Else
                gvBOM.Rows.AddNew()
            End If
        End If
        '' display operations
        gvOperations.Rows.Clear()
        If obj.ObjListOP IsNot Nothing And obj.ObjListOP.Count > 0 Then
            For Each objOP As clsBOMOperations In obj.ObjListOP
                gvOperations.Rows.AddNew()
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationLineNo).Value = objOP.Line_No
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Value = objOP.OPERATION_CODE

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Value = objOP.WORK_CENTER_CODE

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colSetupTimeMin).Value = objOP.SETUP_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colRunTimeMin).Value = objOP.RUN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colCleanTimeMin).Value = objOP.CLEAN_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colWaitTimeMin).Value = objOP.WAIT_TIME_MINUTES
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colperOverlap).Value = objOP.OVERLAP_PER
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationRemarks).Value = objOP.REMARKS

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Tag = clsBOMResources.GetBomResources(objOP.BOM_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Tag = clsBOMToolTypes.GetBomToolTypes(objOP.BOM_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)

                Dim obj As New clsOperationMaster
                obj = obj.GetData(objOP.OPERATION_CODE, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Descraption) > 0 Then
                    gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationDesc).Value = obj.Descraption
                End If


            Next
            CalculateBOMCosting()
            gvOperations.Rows.AddNew()
        Else
            gvOperations.Rows.AddNew()
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsBillOfMaterial
            obj.BOM_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text
            obj.BOM_DATE = Me.dtpBOMDate.Value
            obj.REVISION_NO = Me.lblRevisionNo.Text
            obj.PROD_Drawing_No = clsCommon.myCstr(txtdrawingNo.Text)

            obj.START_DATE = Me.dtpStartDate.Value
            If Me.dtpEndDate.Checked = True Then
                obj.END_DATE = Me.dtpEndDate.Value
            Else
                obj.END_DATE = Nothing
            End If
            obj.LOCATION_CODE = clsCommon.myCstr(Me.txtLocation.Value)
            obj.STATUS = Me.cboBOMStatus.Text
            obj.IS_DEFAULT = Me.chkDefaultBOM.Checked

            'obj.ATTACHED_DOC = Me.txtDocPath.Text
            obj.ATTACHED_DOC_PATH = Me.txtDocPath.Text
            obj.PROD_ITEM_CODE = Me.txtProducedItem.Value
            obj.PROD_QUANTITY = Me.txtBuildQty.Text
            obj.PROD_ITEM_UNIT_CODE = Me.lblUnitName.Text
            obj.MIN_BATCH_SIZE = clsCommon.myCdbl(Me.txtMinBatchQty.Text)
            obj.CREATED_BY = Me.lblCreatedBy.Text
            'obj.APPROVED_BY = Me.lblApprovedBy.Text

            Dim obj1 As clsBillOfMaterial
            ObjList = New List(Of clsBillOfMaterial)
            For Each grow As GridViewRowInfo In gvBOM.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                    obj1 = New clsBillOfMaterial()

                    obj1.BOM_CODE = txtCode.Value
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)
                    obj1.CONSM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                    obj1.CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells(colqty).Value)
                    obj1.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    obj1.SCRAP_PERCENT = clsCommon.myCdbl(grow.Cells(colscrap_per).Value)
                    obj1.WASTAGE_PERCENT = clsCommon.myCdbl(grow.Cells(colwastage_per).Value)
                    obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    obj1.Is_Principle = clsCommon.myCstr(IIf(clsCommon.myCBool(grow.Cells(colPrinci).Value) = True, "1", "0"))
                    obj1.CONSM_Drawing_No = clsCommon.myCstr(grow.Cells(colDrawingNo).Value)
                    obj1.Percentage = clsCommon.myCdbl(grow.Cells(colPercentage).Value)
                    obj1.Alternate_Item_Code = clsCommon.myCstr(grow.Cells(colAlternateItemCode).Value)
                    ObjList.Add(obj1)
                End If
            Next

            '' saving operations
            Dim objListOP As New List(Of clsBOMOperations)
            Dim objListResource As New List(Of clsBOMResources)
            Dim objListToolTypes As New List(Of clsBOMToolTypes)

            Dim objOP As clsBOMOperations
            For Each row As GridViewRowInfo In gvOperations.Rows
                If clsCommon.myLen(row.Cells(colOperationCode).Value) > 0 And clsCommon.myLen(row.Cells(colworkCenterCode).Value) > 0 Then
                    objOP = New clsBOMOperations
                    objOP.BOM_CODE = Me.txtCode.Value
                    objOP.Line_No = row.Cells(colOperationLineNo).Value
                    objOP.OPERATION_CODE = row.Cells(colOperationCode).Value
                    objOP.CLEAN_TIME_MINUTES = row.Cells(colCleanTimeMin).Value
                    objOP.OVERLAP_PER = row.Cells(colperOverlap).Value
                    objOP.REMARKS = row.Cells(colOperationRemarks).Value
                    objOP.RUN_TIME_MINUTES = row.Cells(colRunTimeMin).Value
                    objOP.SETUP_TIME_MINUTES = row.Cells(colSetupTimeMin).Value
                    objOP.WAIT_TIME_MINUTES = row.Cells(colWaitTimeMin).Value
                    objOP.WORK_CENTER_CODE = row.Cells(colworkCenterCode).Value
                    objListOP.Add(objOP)

                    '' resources 

                    If row.Cells(colOperationCode).Tag IsNot Nothing Then
                        Dim objListRes As List(Of clsBOMResources)
                        objListRes = row.Cells(colOperationCode).Tag

                        objListResource.AddRange(objListRes)

                    End If

                    '' tools 

                    If row.Cells(colworkCenterCode).Tag IsNot Nothing Then
                        Dim objListTools As List(Of clsBOMToolTypes)
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
            Dim objListCost As New List(Of clsBOMCosting)
            Dim objCost As clsBOMCosting


            objCost = New clsBOMCosting
            objCost.BOM_CODE = Me.txtCode.Value
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

            objCost = New clsBOMCosting
            objCost.BOM_CODE = Me.txtCode.Value
            objCost.CALC_TYPE = "Calculated"
            objCost.DIRECT_LABOR_COST = Me.gvCost.Rows(rowDirectLaborCost).Cells(colCalcCost).Value
            objCost.DIRECT_MATERIAL_COST = Me.gvCost.Rows(rowDirectMaterialCost).Cells(colCalcCost).Value
            objCost.OVERHEAD_COST = Me.gvCost.Rows(rowOverheadCost).Cells(colCalcCost).Value
            objCost.PACKAGING_MATERIAL_COST = Me.gvCost.Rows(rowPackageingCost).Cells(colCalcCost).Value
            objCost.SETUP_LABOR_COST = Me.gvCost.Rows(rowSetupLaborCost).Cells(colCalcCost).Value
            objCost.SUBCONTRACT_COST = Me.gvCost.Rows(rowSubContractCost).Cells(colCalcCost).Value
            objCost.TOOL_COST = Me.gvCost.Rows(rowToolCost).Cells(colCalcCost).Value
            objCost.TOTAL_COST = Val(Me.gvCost.Rows(rowTotal).Cells(colCalcCost).Value.ToString)
            objListCost.Add(objCost)

            '' map bom costing
            obj.ObjListCosting = objListCost
            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))

            If OpenFileDialog1.FileName = "" And issaved = True Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.BOM_CODE, NavigatorType.Current)
                Return True
            End If
            If clsCommon.myLen(OpenFileDialog1.FileName) <= 0 Then
                Return issaved
            End If
            Dim bData As Byte()
            Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(OpenFileDialog1.FileName))
            bData = br.ReadBytes(br.BaseStream.Length)
            obj.ATTACHED_DOC = bData

            Dim str As String
            str = "UPDATE TSPL_MF_BOM_HEAD set ATTACHED_DOC = @BLOBData where BOM_CODE = '" + txtCode.Value + "' "
            Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
            Dim prm As New SqlParameter("@BLOBData", bData)
            cmd.Parameters.Add(prm)
            Dim COUNT As Integer = 0
            COUNT = cmd.ExecuteNonQuery()
            br.Close() ' done by stuti reagrding memory leakage
            If COUNT > 0 AndAlso issaved Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.BOM_CODE, NavigatorType.Current)
                Return True
            End If

            Return False
        End If
    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtProducedItem.Value) <= 0 Then
            myMessages.blankValue("Main Item")
            txtProducedItem.Focus()
            Return False
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue("Location")
            txtLocation.Focus()
            Return False
        End If
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_BOM_HEAD where BOM_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        If clsCommon.myCdbl(txtBuildQty.Text) <= 0 Then
            myMessages.blankValue("Build Quantity must be greater than zero.")
            txtBuildQty.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        Dim Princi_Count As Integer = 0
        Dim Prod_Item_Type As String = clsItemMaster.GetItemType(Me.txtProducedItem.Value, Nothing)
        For Each grow2 As GridViewRowInfo In gvBOM.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow2.Cells(colItemCode).Value)) > 0 Then
                If clsCommon.myCdbl(grow2.Cells(colPercentage).Value) <= 0 AndAlso clsCommon.myCdbl(grow2.Cells(colqty).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Enter one value either quantity or percentage for Item Code -" + clsCommon.myCstr(grow2.Cells(colItemCode).Value) + " ", Me.Text)
                    Return False
                End If
                If clsCommon.myCdbl(grow2.Cells(colPercentage).Value) > 0 AndAlso clsCommon.myCdbl(grow2.Cells(colqty).Value) > 0 Then
                    clsCommon.MyMessageBoxShow("Enter one value either quantity or percentage for Item Code -" + clsCommon.myCstr(grow2.Cells(colItemCode).Value) + ".Not allow quantity and percentage for one item.", Me.Text)
                    Return False
                End If
            End If
        Next
        For Each grow As GridViewRowInfo In gvBOM.Rows
            'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
            '    ii += 1
            'End If
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                ii += 1
            End If
            If (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemType).Value), "S") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemType).Value), "F") = CompairStringResult.Equal) And clsCommon.myCBool(grow.Cells(colPrinci).Value) = False Then
                Dim dt As DataTable = clsManufacturingOrder.GetItemBOM(clsCommon.myCstr(grow.Cells(colItemCode).Value), Nothing)
                If dt.Rows.Count > 0 Then
                    If clsCommon.myLen(dt.Rows(0).Item("BOM_Code")) > 0 Then
                        Princi_Count = Princi_Count + clsBillOfMaterial.GetPrincipleItemCount(dt.Rows(0).Item("BOM_Code"))
                    End If

                End If


            Else
                If clsCommon.myCBool(grow.Cells(colPrinci).Value) = True Then
                    Princi_Count += 1
                End If
            End If

        Next
        If ii = 0 Then
            clsCommon.MyMessageBoxShow("Please Fill Raw Material Grid.")
            Return False
        End If

        If Princi_Count <= 0 AndAlso Princi_On AndAlso clsCommon.CompairString(Prod_Item_Type, "F") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please select principle item.", Me.Text)
            Return False
        End If

        If Princi_Count > 1 AndAlso Princi_On AndAlso clsCommon.CompairString(Prod_Item_Type, "F") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("More than one principle not allowed.", Me.Text)
            Return False
        End If

        '' Anubhooti 01-Oct-2015 BM00000008080 (If Main Item is SemiFG then system should not allowed to make any child/component item as principle)
        Dim MainItemType As String = String.Empty
        'MainItemType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Item_Type FROM TSPL_ITEM_MASTER WHERE ITEM_CODE ='" &  & "'"))
        If Princi_Count > 0 AndAlso clsCommon.CompairString(Prod_Item_Type, "S") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("You can not make any item as principle item in case of 'Semi Finished Good' BOM", Me.Text)
            Return False
        End If
        ''
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
                If (clsBillOfMaterial.DeleteData(txtCode.Value)) Then
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
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Select Location First.")
            End If
            Dim whrcls As String = " ITEM_TYPE IN ('F','S') "

            '===By Monika 14.01.2015=====if for auto  industry then select drawing no before item=
            'If IsForAutoIndustry Then
            '    txtdrawingNo.Text = clsItemMaster.GetFinderForDrawingNo(whrcls, txtProducedItem.Value, isButtonClicked)

            '    whrcls += " and isnull(drawing_no,'')='" + txtdrawingNo.Text + "'"

            'End If
            '============end here=================================

            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS ITEM_NAME,ITEM_TYPE AS TYPE FROM TSPL_ITEM_MASTER "
            txtProducedItem.Value = clsItemMaster.getFinder(whrcls, txtProducedItem.Value, isButtonClicked) ' clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", whrcls, txtProducedItem.Value, "", isButtonClicked)

            Dim objItm As New clsItemMaster
            '' NO CLASS  FOR ITEM MASTER(FINISHED)
            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & txtProducedItem.Value & "'"

            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMasterItemName.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
                Me.lblUnitName.Text = DT_ITEM.Rows(0).Item("UNIT_CODE")
            End If
            '' REVISION NO
            Dim rev_no As String = clsBillOfMaterial.GetBOMRevisionNo(txtProducedItem.Value, "BOM", txtLocation.Value)
            'STRQ = "SELECT (COUNT(BOM_CODE)) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & txtProducedItem.Value & "' and trans_type='BOM' "
            'Dim dt_rev As DataTable
            'dt_rev = clsDBFuncationality.GetDataTable(STRQ)
            'If dt_rev.Rows(0).Item("rev") = 0 Then
            '    rev_no = txtProducedItem.Value
            'Else
            '    rev_no = txtProducedItem.Value & "/" & (dt_rev.Rows(0).Item("rev") + 1)
            'End If

            Me.lblRevisionNo.Text = rev_no
            CalculateBOMCosting()

            If clsCommon.myLen(txtProducedItem.Value) <= 0 Then
                txtProducedItem.Value = ""
                lblMasterItemName.Text = ""
                lblUnitName.Text = ""
                lblRevisionNo.Text = ""
                txtdrawingNo.Text = ""
                ResetCosting()
            End If
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
        Dim WhrCls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += " and Location_Code in (" + arrLoc + ")"
        End If

        Dim str As String = "select count(*) from TSPL_MF_BOM_HEAD where BOM_CODE ='" + txtCode.Value + "' and trans_type='BOM' " + WhrCls + " "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.BOM_CODE AS Code,T1.DESCRIPTION,T1.LOCATION_CODE,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
            qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T1.PROD_QUANTITY,T1.PROD_ITEM_UNIT_CODE,"
            qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE "
            WhrCls = " T1.trans_type='BOM' "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += " and T1.Location_Code in (" + arrLoc + ")"
            End If
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", WhrCls, txtCode.Value, " convert(date, BOM_DATE,103) desc , Code desc ", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
                btnCopy.Enabled = False
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
            'If Me.cboBOMStatus.Text <> "Approved" Then
            '    common.clsCommon.MyMessageBoxShow("Bom Status must be Approved.")
            '    Exit Sub
            'End If
            Me.cboBOMStatus.Text = "Approved"
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsBillOfMaterial.PostData(txtCode.Value, True)) Then
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
                btnCopy.Enabled = False
            End If
        End If
    End Sub




    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub



    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
        Try
            If gvBOM.CurrentRow Is Nothing Then
                Exit Sub
            End If

            'If gvBOM.CurrentRow.Cells(0).Value = "" Then
            '    gvBOM.CurrentRow.Cells(0).Value = gvBOM.RowCount
            'End If

            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvBOM.Columns(colItemCategoryCode) Then
                    Dim strq As String = ""
                    strq = "select PROD_ITEM_CATEGORY_CODE as Code,DESCRIPTION from TSPL_MF_PRODUCTION_ITEM_CATEGORY "
                    gvBOM.CurrentRow.Cells(colItemCategoryCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCategoryCode).Value))
                End If


                If e.Column Is gvBOM.Columns(colItemCode) Then
                    Dim whrcls As String = "" '" ITEM_TYPE IN ('R','O','S') "

                    'If IsForAutoIndustry Then
                    '    gvBOM.CurrentRow.Cells(colDrawingNo).Value = clsItemMaster.GetFinderForDrawingNo(whrcls, clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), False)

                    '    whrcls += " and isnull(drawing_no,'')='" + clsCommon.myCstr(gvBOM.CurrentRow.Cells(colDrawingNo).Value) + "'"

                    'End If
                    Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), whrcls, False)
                    ''and prod_item_category_code='" & gvBOM.CurrentRow.Cells(colItemCategoryCode).Value & "'
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                        gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                        gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                        gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                        gvBOM.CurrentRow.Cells(colItemType).Value = obj.ITEM_TYPE
                        gvBOM.CurrentRow.Cells(colDrawingNo).Value = obj.CONSM_Drawing_No
                    Else
                        gvBOM.CurrentRow.Cells(colItemCode).Value = Nothing
                        gvBOM.CurrentRow.Cells(colitemDesc).Value = Nothing
                        gvBOM.CurrentRow.Cells(colUnitCode).Value = Nothing
                        gvBOM.CurrentRow.Cells(colItemType).Value = Nothing
                        gvBOM.CurrentRow.Cells(colDrawingNo).Value = Nothing
                    End If
                ElseIf e.Column Is gvBOM.Columns(colAlternateItemCode) Then
                    Dim whrcls As String = " ITEM_TYPE IN ('R','O','S') "
                    Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colAlternateItemCode).Value), whrcls, False)
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                        gvBOM.CurrentRow.Cells(colAlternateItemCode).Value = obj.PROD_ITEM_CODE
                        gvBOM.CurrentRow.Cells(colAlternateitemDesc).Value = obj.ITEM_DESCRIPTION
                    Else
                        gvBOM.CurrentRow.Cells(colAlternateItemCode).Value = Nothing
                        gvBOM.CurrentRow.Cells(colAlternateitemDesc).Value = Nothing
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


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
    Sub showAttachedDocs(ByVal obj As clsBillOfMaterial)

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

    Private Sub gvBOM_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvBOM.CurrentColumnChanged
        If gvBOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBOM.CurrentRow.Index
            gvBOM.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvBOM.Rows.Count - 1 Then

                gvBOM.Rows.AddNew()
                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gvBOM.CurrentRow = gvBOM.Rows(intCurrRow)

            End If
        End If

    End Sub



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
            myMessages.blankValue("BOM Code")
        Else
            funPrint()
        End If
    End Sub
    Private Sub funPrint()
        Try
            Dim qry As String = " select '" & objCommonVar.CurrentCompanyName & "' as Company_Name, TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  as BuildItemCode,CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.BOM_DATE,103) as BOMDate,CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.START_DATE,103) as StartDate,"
            qry += " CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.END_DATE,103) as EndDate,TSPL_MF_BOM_HEAD.STATUS as BomStatus,TSPL_MF_BOM_HEAD.PROD_ITEM_UNIT_CODE as BuildUOM,"
            qry += " TSPL_MF_BOM_HEAD.PROD_QUANTITY as BuildQty, "
            qry += " TSPL_MF_BOM_HEAD.MIN_BATCH_SIZE as MinBatchSize,TSPL_MF_BOM_DETAIL.LINE_NO as SL_No,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE as ItemCategory,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION as ItemDesc,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UOM,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY as Quantity,TSPL_MF_BOM_DETAIL.SCRAP_PERCENT as Scrap,TSPL_MF_BOM_DETAIL.WASTAGE_PERCENT as Wastage,"
            qry += " TSPL_MF_BOM_DETAIL.REMARKS as Remarks from TSPL_MF_BOM_HEAD inner join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE"
            qry += " where 2=2 and trans_type='BOM'"

            If txtCode.Value <> "" Then
                qry += " and  TSPL_MF_BOM_HEAD.BOM_CODE='" & txtCode.Value & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim objn As New frmCrystalReportViewer
            objn.funreport(CrystalReportFolder.PRODUCTION, dt, "crptBOMPrint", "Bill Of Material")

            If Not clsCommon.MyMessageBoxShow("Want to see tree structure?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            isCellValueChangedOpen = True
            If e.Column Is gvOperations.Columns(colOperationCode) Then
                Dim strq As String = ""
                strq = "select OPERATION_CODE as Code,DESCRIPTION as Name from TSPL_MF_OPERATION "
                gvOperations.CurrentRow.Cells(colOperationCode).Value = clsCommon.ShowSelectForm("TSPL_MF_OPERATION", strq, "Code", "", clsCommon.myCstr(gvOperations.CurrentRow.Cells(colOperationCode).Value))
                Dim obj As New clsOperationMaster
                obj = obj.GetData(gvOperations.CurrentRow.Cells(colOperationCode).Value, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Descraption) > 0 Then
                    gvOperations.CurrentRow.Cells(colOperationDesc).Value = obj.Descraption
                End If
                'gvOperations.CurrentRow.Cells(colOperationDesc).Value = New clsOperationMaster().GetData(gvOperations.CurrentRow.Cells(colOperationCode).Value, NavigatorType.Current).Descraption.ToString
            End If


            If e.Column Is gvOperations.Columns(colworkCenterCode) Then
                If clsCommon.myLen(gvOperations.CurrentRow.Cells(colOperationCode).Value) > 0 Then
                    Dim strq As String = ""
                    Dim cond As String = ""
                    strq = "select TSPL_MF_OPERATION_WORK_CENTER.WORK_CENTER_CODE as Code,TSPL_MF_WORK_CENTER.DESCRIPTION AS Name from TSPL_MF_OPERATION_WORK_CENTER INNER JOIN TSPL_MF_WORK_CENTER ON TSPL_MF_OPERATION_WORK_CENTER.WORK_CENTER_CODE=TSPL_MF_WORK_CENTER.WORK_CENTER_CODE "
                    cond = " TSPL_MF_OPERATION_WORK_CENTER.OPERATION_CODE='" & gvOperations.CurrentRow.Cells(colOperationCode).Value & "'"
                    gvOperations.CurrentRow.Cells(colworkCenterCode).Value = clsCommon.ShowSelectForm("TSPL_MF_WORK_CENTER", strq, "Code", cond, clsCommon.myCstr(gvOperations.CurrentRow.Cells(colworkCenterCode).Value))

                    Dim obj As clsWorkCenterMaster = clsWorkCenterMaster.GetData(gvOperations.CurrentRow.Cells(colworkCenterCode).Value, NavigatorType.Current)
                    If obj IsNot Nothing Then
                        gvOperations.CurrentRow.Cells(colSetupTimeMin).Value = IIf(obj.SETUP_TIME_TYPE = "Hour", obj.SETUP_TIME * 60, obj.SETUP_TIME)
                        gvOperations.CurrentRow.Cells(colRunTimeMin).Value = IIf(obj.RUN_TIME_TYPE = "Hour", obj.RUN_TIME * 60, obj.RUN_TIME)
                        gvOperations.CurrentRow.Cells(colCleanTimeMin).Value = IIf(obj.CLEANUP_TIME_TYPE = "Hour", obj.CLEANUP_TIME * 60, obj.CLEANUP_TIME)
                        gvOperations.CurrentRow.Cells(colWaitTimeMin).Value = IIf(obj.WAIT_TIME_TYPE = "Hour", obj.WAIT_TIME * 60, obj.WAIT_TIME)

                    End If
                    FillResources(e.RowIndex)
                    FillToolTypes(e.RowIndex)
                Else
                    clsCommon.MyMessageBoxShow("Select valid Operation Code first")
                End If

            End If
            CalculateBOMCosting()
            isCellValueChangedOpen = False
        End If
    End Sub
    Function getResourcesFromResourceMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMResources)
        Dim objList As New List(Of clsBOMResources)
        Dim obj1 As clsBOMResources
        Dim objList1 As List(Of clsWorkCenterResourceDetail)

        objList1 = clsWorkCenterResourceDetail.GetData(WorkCenterCode, trans)
        If Not objList1 Is Nothing And objList1.Count > 0 Then
            For Each obj As clsWorkCenterResourceDetail In objList1
                obj1 = New clsBOMResources
                obj1.OPERATION_CODE = OperationCode
                obj1.WORK_CENTER_CODE = WorkCenterCode
                obj1.RESOURCE_CODE = obj.RESOURCE_CODE
                obj1.RESOURCE_Desc = obj.DESCRIPTION
                obj1.RESOURCE_Type = obj.RESOURCE_TYPE
                obj1.QUANTITY = 1
                obj1.UNIT_COST = obj.UNIT_COST
                obj1.TOTAL_COST = obj.UNIT_COST
                obj1.UNIT_COST_UOM = obj.UNIT_CODE_OTHER
                objList.Add(obj1)
            Next
        End If
        Return objList
    End Function
    Function FillResources(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMResources)
        Dim objList As New List(Of clsBOMResources)
        If isNewEntry Then
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag Is Nothing Then
                    objList = getResourcesFromResourceMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
                    objList = clsBOMResources.GetBomResources(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    If objList Is Nothing Or objList.Count = 0 Then
                        objList = getResourcesFromResourceMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
            For Each obj As clsBOMResources In objList
                gvResources.Rows.AddNew()
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceCode).Value = obj.RESOURCE_CODE
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceDesc).Value = obj.RESOURCE_Desc
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceQty).Value = obj.QUANTITY
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceUnitCost).Value = obj.UNIT_COST
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceTotalCost).Value = obj.TOTAL_COST
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceType).Value = obj.RESOURCE_Type
                gvResources.Rows(objList.IndexOf(obj)).Cells(colResourceUnitCostUom).Value = obj.UNIT_COST_UOM
            Next
        End If
        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = objList
        Else
            gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Tag = Nothing
        End If
        CalculateBOMCosting()
        gvResources.Rows.AddNew()

        Return objList
    End Function

    Function getToolTypeFromToolTypeMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMToolTypes)
        Dim objList As New List(Of clsBOMToolTypes)
        Dim obj1 As clsBOMToolTypes
        Dim objList1 As List(Of clsWorkCenterToolDetail)

        objList1 = clsWorkCenterToolDetail.GetData(WorkCenterCode, trans)
        If Not objList1 Is Nothing And objList1.Count > 0 Then
            For Each obj As clsWorkCenterToolDetail In objList1
                obj1 = New clsBOMToolTypes
                obj1.OPERATION_CODE = OperationCode
                obj1.WORK_CENTER_CODE = WorkCenterCode
                obj1.TOOL_TYPE_CODE = obj.TOOL_TYPE_CODE
                obj1.TOOL_TYPE_DESC = obj.DESCRIPTION
                obj1.QUANTITY = 1
                obj1.UNIT_COST = obj.UNIT_COST
                obj1.TOTAL_COST = obj.UNIT_COST
                obj1.UNIT_COST_UOM = obj.UNIT_CODE
                objList.Add(obj1)
            Next
        End If
        Return objList
    End Function
    Function FillToolTypes(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBOMToolTypes)
        Dim objList As New List(Of clsBOMToolTypes)
        If isNewEntry Then
            If clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value) > 0 And clsCommon.myLen(gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value) > 0 Then
                If gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag Is Nothing Then
                    objList = getToolTypeFromToolTypeMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
                    objList = clsBOMToolTypes.GetBomToolTypes(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
                    If objList Is Nothing Or objList.Count = 0 Then
                        objList = getToolTypeFromToolTypeMaster(gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
            For Each obj As clsBOMToolTypes In objList
                gvTools.Rows.AddNew()
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeCode).Value = obj.TOOL_TYPE_CODE
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeDesc).Value = obj.TOOL_TYPE_DESC
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeQty).Value = obj.QUANTITY
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeUnitCost).Value = obj.UNIT_COST
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeTotalCost).Value = obj.TOTAL_COST
                gvTools.Rows(objList.IndexOf(obj)).Cells(colToolTypeUnitCostUom).Value = obj.UNIT_COST_UOM

            Next
        End If
        If objList IsNot Nothing AndAlso objList.Count > 0 Then
            gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = objList
        Else
            gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = Nothing
        End If
        CalculateBOMCosting()
        gvTools.Rows.AddNew()
        'gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Tag = objList
        Return objList
    End Function

    Private Sub gvOperations_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvOperations.CurrentColumnChanged
        If gvOperations.RowCount > 0 Then
            Dim intCurrRow As Integer = gvOperations.CurrentRow.Index
            gvOperations.CurrentRow.Cells(colOperationLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvOperations.Rows.Count - 1 Then

                gvOperations.Rows.AddNew()
                gvOperations.CurrentRow = gvOperations.Rows(intCurrRow)
                'gvOperations.CurrentRow.Cells(colOperationLineNo).Value = gvOperations.CurrentRow.Index + 1

            End If
        End If
    End Sub

    Private Sub gvResources_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvResources.CellEndEdit
        If gvOperations.CurrentRow Is Nothing Then
            Exit Sub
        End If
        If gvResources.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvResources.Columns(colResourceCode) Then
                Dim strq As String = ""
                Dim strWhrCls As String = ""
                strq = "SELECT TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE as Code,TSPL_MF_RESOURCE_MASTER.DESCRIPTION AS Name FROM " &
                       " TSPL_MF_WORK_CENTER_RESOURCE_DETAIL LEFT JOIN TSPL_MF_RESOURCE_MASTER " &
                       " ON TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE=TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE "
                strWhrCls = "TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.WORK_CENTER_CODE='" & gvOperations.CurrentRow.Cells(colworkCenterCode).Value & "'"

                gvResources.CurrentRow.Cells(colResourceCode).Value = clsCommon.ShowSelectForm("TSPL_MF_OPERATION", strq, "Code", strWhrCls, clsCommon.myCstr(gvResources.CurrentRow.Cells(colResourceCode).Value))
                Dim obj As New clsResourceMaster
                obj = clsResourceMaster.GetData(gvResources.CurrentRow.Cells(colResourceCode).Value, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DESCRIPTION) > 0 Then
                    gvResources.CurrentRow.Cells(colResourceDesc).Value = obj.DESCRIPTION
                    gvResources.CurrentRow.Cells(colResourceQty).Value = 1
                    gvResources.CurrentRow.Cells(colResourceUnitCost).Value = obj.COST
                    gvResources.CurrentRow.Cells(colResourceType).Value = obj.RESOURCE_TYPE
                    gvResources.CurrentRow.Cells(colResourceUnitCostUom).Value = obj.UNIT_CODE
                End If

            End If


            CalculateResourceCost(Me.gvOperations.CurrentRow.Index)
            CalculateBOMCosting()
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub CalculateResourceCost(ByVal op_row As Integer)
        Dim objList As New List(Of clsBOMResources)
        Dim obj As clsBOMResources
        For Each rw As GridViewRowInfo In gvResources.Rows
            If clsCommon.myLen(rw.Cells(colResourceCode).Value) > 0 Then

                Me.gvResources.Rows(rw.Index).Cells(colResourceTotalCost).Value = Me.gvResources.Rows(rw.Index).Cells(colResourceQty).Value * Me.gvResources.Rows(rw.Index).Cells(colResourceUnitCost).Value
                obj = New clsBOMResources
                obj.BOM_CODE = Me.txtCode.Value
                obj.OPERATION_CODE = gvOperations.Rows(op_row).Cells(colOperationCode).Value
                obj.WORK_CENTER_CODE = gvOperations.Rows(op_row).Cells(colworkCenterCode).Value
                obj.RESOURCE_CODE = rw.Cells(colResourceCode).Value
                obj.RESOURCE_Desc = rw.Cells(colResourceDesc).Value
                obj.QUANTITY = rw.Cells(colResourceQty).Value
                obj.UNIT_COST = rw.Cells(colResourceUnitCost).Value
                obj.TOTAL_COST = rw.Cells(colResourceTotalCost).Value
                obj.RESOURCE_Type = rw.Cells(colResourceType).Value
                obj.UNIT_COST_UOM = rw.Cells(colResourceUnitCostUom).Value
                objList.Add(obj)
            End If
        Next
        If objList.Count > 0 Then
            gvOperations.Rows(op_row).Cells(colOperationCode).Tag = objList
        End If
        CalculateBOMCosting()
    End Sub

    Sub CalculateTollTypeCost(ByVal op_row As Integer)
        Dim objList As New List(Of clsBOMToolTypes)
        Dim obj As clsBOMToolTypes
        For Each rw As GridViewRowInfo In gvTools.Rows
            If clsCommon.myLen(rw.Cells(colToolTypeCode).Value) > 0 Then

                Me.gvTools.Rows(rw.Index).Cells(colToolTypeTotalCost).Value = Me.gvTools.Rows(rw.Index).Cells(colToolTypeQty).Value * Me.gvTools.Rows(rw.Index).Cells(colToolTypeUnitCost).Value
                obj = New clsBOMToolTypes
                obj.BOM_CODE = Me.txtCode.Value
                obj.OPERATION_CODE = gvOperations.Rows(op_row).Cells(colOperationCode).Value
                obj.WORK_CENTER_CODE = gvOperations.Rows(op_row).Cells(colworkCenterCode).Value
                obj.TOOL_TYPE_CODE = rw.Cells(colToolTypeCode).Value
                obj.TOOL_TYPE_DESC = rw.Cells(colToolTypeDesc).Value
                obj.QUANTITY = rw.Cells(colToolTypeQty).Value
                obj.UNIT_COST = rw.Cells(colToolTypeUnitCost).Value
                obj.TOTAL_COST = rw.Cells(colToolTypeTotalCost).Value
                obj.UNIT_COST_UOM = rw.Cells(colToolTypeUnitCostUom).Value
                objList.Add(obj)
            End If
        Next
        If objList.Count > 0 Then
            gvOperations.Rows(op_row).Cells(colworkCenterCode).Tag = objList
        End If
        CalculateBOMCosting()
    End Sub


    Private Sub gvResources_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvResources.CurrentColumnChanged
        If gvResources.RowCount > 0 Then
            Dim intCurrRow As Integer = gvResources.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvResources.Rows.Count - 1 Then

                gvResources.Rows.AddNew()
                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gvResources.CurrentRow = gvResources.Rows(intCurrRow)

            End If
        End If
    End Sub

    Private Sub gvTools_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTools.CellEndEdit
        If gvOperations.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If gvTools.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            If e.Column Is gvTools.Columns(colToolTypeCode) Then
                Dim strq As String = ""
                Dim strWhrCls As String = ""
                strq = "SELECT TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE as Code,TSPL_MF_TOOL_TYPE.DESCRIPTION AS Name FROM " &
                       " TSPL_MF_WORK_CENTER_TOOL_DETAIL LEFT JOIN TSPL_MF_TOOL_TYPE " &
                       " ON TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE=TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE "
                strWhrCls = "TSPL_MF_WORK_CENTER_TOOL_DETAIL.WORK_CENTER_CODE='" & gvOperations.CurrentRow.Cells(colworkCenterCode).Value & "'"

                gvTools.CurrentRow.Cells(colToolTypeCode).Value = clsCommon.ShowSelectForm("tools", strq, "Code", strWhrCls, clsCommon.myCstr(gvTools.CurrentRow.Cells(colToolTypeCode).Value))
                Dim obj As New ClsMFToolType
                obj = ClsMFToolType.GetData(gvTools.CurrentRow.Cells(colToolTypeCode).Value, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DESCRIPTION) > 0 Then
                    gvTools.CurrentRow.Cells(colToolTypeDesc).Value = obj.DESCRIPTION
                    gvTools.CurrentRow.Cells(colToolTypeQty).Value = 1
                    gvTools.CurrentRow.Cells(colToolTypeUnitCost).Value = obj.COST_PER_UNIT
                    gvTools.CurrentRow.Cells(colToolTypeUnitCostUom).Value = obj.UNIT_CODE
                End If

            End If
            CalculateTollTypeCost(Me.gvOperations.CurrentRow.Index)
            CalculateBOMCosting()
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gvTools_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTools.CurrentColumnChanged
        If gvTools.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTools.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvTools.Rows.Count - 1 Then

                gvTools.Rows.AddNew()
                'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gvTools.CurrentRow = gvTools.Rows(intCurrRow)

            End If
        End If
    End Sub

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

        '' set calculated cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colCalcCost).Value = 0
        Me.gvCost.Rows(rowTotal).Cells(colCalcCost).Value = 0
        '' variance calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowPackageingCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowOverheadCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowSubContractCost).Cells(colperVariance).Value = 0
        Me.gvCost.Rows(rowToolCost).Cells(colperVariance).Value = 0
    End Sub
    Sub CalculateBOMCosting()
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
            qry = " select Direct_Material_Cost as MATERIAL_COST , PACKAGING_MATERIAL_COST as PACKAGING_COST , setup_labor_Cost as SETUP_COST, direct_labor_cost as LABOR_COST, overhead_cost as OVERHEAD_COST, SUBCONTRACT_COST as SUBCONTRACT_COST, tool_Cost as TOOL_COST, Total_Cost as TOTAL_COST from TSPL_MF_BOM_COSTING where BOM_CODE = '" + txtCode.Value + "' and CALC_TYPE = 'Standard'"
            dtStd = Nothing
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

        End If
        '' calculation of calculated cost
        '' resources and tools cost calculation

        Dim DirectMaterialCost As Decimal = 0
        Dim PackagingCost As Decimal = 0

        Dim ResourceSetupCost As Decimal = 0
        Dim ResourceDLaborCost As Decimal = 0
        Dim ResourceOverheadCost As Decimal = 0
        Dim ToolCost As Decimal = 0
        Dim SubContractedCost As Decimal = 0

        For Each row As GridViewRowInfo In Me.gvOperations.Rows
            '' resource cost calculation
            If row.Cells(colOperationCode).Tag IsNot Nothing Then
                Dim objlist As List(Of clsBOMResources)

                objlist = row.Cells(colOperationCode).Tag
                For Each obj As clsBOMResources In objlist
                    If obj.RESOURCE_Type = "Setup Labor" Then
                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceSetupCost = ResourceSetupCost + obj.UNIT_COST * obj.QUANTITY * (row.Cells(colSetupTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceSetupCost = ResourceSetupCost + obj.UNIT_COST * obj.QUANTITY * row.Cells(colSetupTimeMin).Value
                        End If

                    ElseIf obj.RESOURCE_Type = "Run Labor" Then
                        If obj.UNIT_COST_UOM = "Hour" Then
                            ResourceDLaborCost = ResourceDLaborCost + obj.UNIT_COST * obj.QUANTITY * (row.Cells(colRunTimeMin).Value / 60.0)
                        ElseIf obj.UNIT_COST_UOM = "Minute" Then
                            ResourceDLaborCost = ResourceDLaborCost + obj.UNIT_COST * obj.QUANTITY * row.Cells(colRunTimeMin).Value
                        End If
                    ElseIf obj.RESOURCE_Type = "Overhead" Then
                        ResourceOverheadCost = ResourceOverheadCost + obj.UNIT_COST * obj.QUANTITY

                    End If
                Next
            End If

            '' tools cost calculation
            If row.Cells(colworkCenterCode).Tag IsNot Nothing Then
                Dim objlist As List(Of clsBOMToolTypes)

                objlist = row.Cells(colworkCenterCode).Tag
                For Each obj As clsBOMToolTypes In objlist
                    If obj.UNIT_COST_UOM = "Hour" Then
                        ToolCost = ToolCost + obj.UNIT_COST * obj.QUANTITY * (row.Cells(colRunTimeMin).Value / 60.0)
                    ElseIf obj.UNIT_COST_UOM = "Minute" Then
                        ToolCost = ToolCost + obj.UNIT_COST * obj.QUANTITY * row.Cells(colRunTimeMin).Value
                    Else
                        ToolCost = ToolCost + obj.UNIT_COST * obj.QUANTITY
                    End If
                Next
            End If
        Next
        '' set calculated cost
        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colCalcCost).Value = Format(DirectMaterialCost, "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colCalcCost).Value = Format(PackagingCost, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colCalcCost).Value = Format(ResourceSetupCost, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colCalcCost).Value = Format(ResourceDLaborCost, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colCalcCost).Value = Format(ResourceOverheadCost, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colCalcCost).Value = Format(SubContractedCost, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colCalcCost).Value = Format(ToolCost, "###0.00")
        Me.gvCost.Rows(rowTotal).Cells(colCalcCost).Value = Format(DirectMaterialCost + PackagingCost + ResourceSetupCost + ResourceDLaborCost + ResourceOverheadCost + SubContractedCost + ToolCost, "###0.00")
        '' variance calculation

        Me.gvCost.Rows(rowDirectMaterialCost).Cells(colperVariance).Value = Format(((DirectMaterialCost - Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowPackageingCost).Cells(colperVariance).Value = Format(((PackagingCost - Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSetupLaborCost).Cells(colperVariance).Value = Format(((ResourceSetupCost - Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowDirectLaborCost).Cells(colperVariance).Value = Format(((ResourceDLaborCost - Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowOverheadCost).Cells(colperVariance).Value = Format(((ResourceOverheadCost - Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowSubContractCost).Cells(colperVariance).Value = Format(((SubContractedCost - Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value)) * 100, "###0.00")
        Me.gvCost.Rows(rowToolCost).Cells(colperVariance).Value = Format(((ToolCost - Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value) / IIf(Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value = 0, 1, Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value)) * 100, "###0.00")

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


    Private Sub btntreeview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntreeview.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select BOM Code first")
            End If

            Dim qry As String = "select a.* from (select distinct (TSPL_MF_BOM_HEAD.PROD_ITEM_CODE+' ('+tspl_item_master.item_desc+')') as lev1,(TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE+' ('+item.item_desc+')') as lev2,t1.CONSM_ITEM_CODE as lev3,t2.CONSM_ITEM_CODE as lev4,t3.CONSM_ITEM_CODE as lev5 from TSPL_MF_BOM_HEAD left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_HEAD.PROD_ITEM_CODE left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join tspl_item_master item on item.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE left outer join (select TSPL_MF_BOM_HEAD.PROD_ITEM_CODE,(TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE+' ('+tspl_item_master.item_desc+')') as CONSM_ITEM_CODE from TSPL_MF_BOM_HEAD left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE where trans_type='BOM') t1 on t1.PROD_ITEM_CODE=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE "
            qry += "left outer join (select TSPL_MF_BOM_HEAD.PROD_ITEM_CODE,(TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE+' ('+tspl_item_master.item_desc+')') as CONSM_ITEM_CODE from TSPL_MF_BOM_HEAD left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE where trans_type='BOM') t2 on t2.PROD_ITEM_CODE=t1.CONSM_ITEM_CODE "
            qry += "left outer join (select TSPL_MF_BOM_HEAD.PROD_ITEM_CODE,(TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE+' ('+tspl_item_master.item_desc+')') as CONSM_ITEM_CODE from TSPL_MF_BOM_HEAD left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE where trans_type='BOM') t3 on t3.PROD_ITEM_CODE=t2.CONSM_ITEM_CODE "
            qry += " where 2=2 and trans_type='BOM'"

            If txtCode.Value <> "" Then
                qry += " and  TSPL_MF_BOM_HEAD.BOM_CODE='" & txtCode.Value & "' "
            End If
            qry += ")a  order by a.lev1,a.lev2,a.lev3,a.lev4,a.lev5"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Me.gv_tree.DataSource = Nothing
                BindTreeView1(dt, True)
                RadPageView1.SelectedPage = RadPageViewPage2

                'Me.RadGridView1.DataSource = Nothing
                'BindTreeView(dt, True)

                'RadPageView1.SelectedPage = RadPageViewPage1
            Else
                clsCommon.MyMessageBoxShow("No Data Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BindTreeView(ByVal dt As DataTable, ByVal expandall As Boolean)
        Try
            Dim dataSource As New DataTable("fileSystem")
            dataSource.Columns.Add("ID", GetType(Integer))
            dataSource.Columns.Add("ParentID", GetType(Integer))
            dataSource.Columns.Add("Name", GetType(String))

            Dim ii As Integer = 1 'root node
            Dim jj As Integer = 2 'child node
            Dim kk As Integer = 3
            Dim yy As Integer = 4
            Dim xy As Integer = 5
            Dim node1 As String = ""
            Dim node2 As String = ""
            Dim node3 As String = ""
            Dim node4 As String = ""
            Dim node5 As String = ""
            Dim oldnode1 As String = ""
            Dim oldnode2 As String = ""
            Dim oldnode3 As String = ""
            Dim oldnode4 As String = ""
            Dim oldnode5 As String = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    node1 = clsCommon.myCstr(dr("lev1"))
                    node2 = clsCommon.myCstr(dr("lev2"))
                    node3 = clsCommon.myCstr(dr("lev3"))
                    node4 = clsCommon.myCstr(dr("lev4"))
                    node5 = clsCommon.myCstr(dr("lev5"))

                    If clsCommon.myLen(node1) > 0 AndAlso clsCommon.CompairString(node1, oldnode1) <> CompairStringResult.Equal Then
                        If clsCommon.myLen(oldnode1) > 0 AndAlso clsCommon.CompairString(node1, oldnode1) <> CompairStringResult.Equal Then
                            ii += 1
                        End If
                        dataSource.Rows.Add(ii, Nothing, node1)
                    End If
                    If clsCommon.myLen(node2) > 0 AndAlso clsCommon.CompairString(node2, oldnode2) <> CompairStringResult.Equal Then
                        If clsCommon.myLen(oldnode2) > 0 AndAlso clsCommon.CompairString(node2, oldnode2) <> CompairStringResult.Equal Then
                            jj += 1
                        End If
                        dataSource.Rows.Add(jj, ii, node2)
                    End If
                    If clsCommon.myLen(node3) > 0 AndAlso clsCommon.CompairString(node3, oldnode3) <> CompairStringResult.Equal Then
                        If clsCommon.myLen(oldnode3) > 0 AndAlso clsCommon.CompairString(node3, oldnode3) <> CompairStringResult.Equal Then
                            kk += 1
                        End If
                        dataSource.Rows.Add(kk, jj, node3)
                    End If
                    If clsCommon.myLen(node4) > 0 AndAlso clsCommon.CompairString(node4, oldnode4) <> CompairStringResult.Equal Then
                        If clsCommon.myLen(oldnode4) > 0 AndAlso clsCommon.CompairString(node4, oldnode4) <> CompairStringResult.Equal Then
                            yy += 1
                        End If
                        dataSource.Rows.Add(yy, kk, node4)
                    End If
                    If clsCommon.myLen(node5) > 0 AndAlso clsCommon.CompairString(node5, oldnode5) <> CompairStringResult.Equal Then
                        If clsCommon.myLen(oldnode5) > 0 AndAlso clsCommon.CompairString(node5, oldnode5) <> CompairStringResult.Equal Then
                            xy += 1
                        End If
                        dataSource.Rows.Add(xy, yy, node5)
                    End If

                    oldnode1 = node1
                    oldnode2 = node2
                    oldnode3 = node3
                    oldnode4 = node4
                    oldnode5 = node5
                Next

                Me.RadGridView1.ShowHeaderCellButtons = True
                Me.RadGridView1.AllowAddNewRow = False
                Me.RadGridView1.TableElement.RowHeight = 20

                Me.RadGridView1.ReadOnly = True
                Me.RadGridView1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
                Me.RadGridView1.DataSource = dataSource

                Try
                    Me.RadGridView1.Relations.AddSelfReference(Me.RadGridView1.MasterTemplate, "ID", "ParentID")
                Catch exx As Exception
                End Try

                Me.RadGridView1.Columns("ID").IsVisible = False
                Me.RadGridView1.Columns("ParentID").IsVisible = False

                'Me.RadGridView1.MasterTemplate.ExpandAll()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub btnCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim savefiledialog As New SaveFileDialog()
    '    savefiledialog.Filter = "CSV File (*.csv)|*.csv"
    '    If savefiledialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
    '        Return
    '    End If

    '    Dim filename As String = savefiledialog.FileName.ToString()

    '    Dim csvexport As New ExportToCSV(Me.RadGridView1)
    '    csvexport.ExportHierarchy = True

    '    Me.Cursor = Cursors.WaitCursor
    '    csvexport.RunExport(filename)

    '    Process.Start(filename)

    '    Me.Cursor = Cursors.Default
    'End Sub

    'Private Sub btnHTML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim savefiledialog As New SaveFileDialog()
    '    savefiledialog.Filter = "Html File (*.htm)|*.htm"
    '    If savefiledialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
    '        Return
    '    End If

    '    Dim filename As String = savefiledialog.FileName.ToString()

    '    Dim htmlexporter As New ExportToHTML(Me.RadGridView1)
    '    htmlexporter.ExportVisualSettings = True
    '    htmlexporter.ExportHierarchy = True

    '    Me.Cursor = Cursors.WaitCursor

    '    htmlexporter.RunExport(filename)
    '    Process.Start(filename)

    '    Me.Cursor = Cursors.Default
    'End Sub


    'Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim clsoutpoutstream As New System.IO.MemoryStream
    '    Dim clsprompt As New SaveFileDialog()
    '    clsprompt.Title = "Select Output File"
    '    clsprompt.Filter = "Text Files {*.txt}|*.txt"

    '    If clsprompt.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
    '        Return
    '    End If

    '    clsWriter = New System.IO.StreamWriter(clsprompt.FileName)

    '    ExportToText(gv_tree.Nodes, 0, clsoutpoutstream)

    '    clsoutpoutstream.Flush()
    '    clsoutpoutstream.Close()

    '    Process.Start(clsprompt.FileName)
    '    '==========================
    '    'Dim savefiledialog As New SaveFileDialog()
    '    'savefiledialog.Filter = "PDF File (*.pdf)|*.pdf"
    '    'If savefiledialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
    '    '    Return
    '    'End If

    '    'Dim filename As String = savefiledialog.FileName.ToString()

    '    'Dim pdfexporter As New ExportToPDF(Me.RadGridView1)
    '    'pdfexporter.PdfExportSettings.Title = "BOM Tree View"
    '    'pdfexporter.PdfExportSettings.PageWidth = 297
    '    'pdfexporter.PdfExportSettings.PageHeight = 210
    '    'pdfexporter.FitToPageWidth = True

    '    ''pdfexporter.SummariesExportOption = SummariesOption.ExportAll

    '    'pdfexporter.ExportVisualSettings = True
    '    'pdfexporter.ExportHierarchy = True

    '    'Me.Cursor = Cursors.WaitCursor
    '    'pdfexporter.RunExport(filename)

    '    'Process.Start(filename)

    '    'Me.Cursor = Cursors.Default
    'End Sub

#Region "Tree Control"
    Private Sub ExportToText(ByVal nodes As RadTreeNodeCollection, ByVal Indentation As Integer, ByVal outstream As System.IO.MemoryStream)
        Dim clsOutStream As New System.IO.MemoryStream

        '  Dim OutputPath As String
        'Dim clsWriter As New System.IO.StreamWriter

        For Each clsNode As RadTreeNode In nodes
            'write out node at the specified indentation...
            clsWriter.WriteLine(New String(" "c, Indentation * 4) & " - " & clsNode.Text)
            'write out child nodes...
            ExportToText(clsNode.Nodes, Indentation + 1, clsOutStream)
            ' flush stream..
            clsWriter.Flush()
            clsOutStream.Position = 0
            Dim clsReader As New System.IO.StreamReader(clsOutStream)
            ' MessageBox.Show(clsReader.ReadToEnd())
        Next
    End Sub

    Private Function Searchnode(ByVal nodetext As String) As RadTreeNode
        For Each node As RadTreeNode In gv_tree.Nodes
            If node.Text = nodetext Then
                Return node
            End If
        Next
        Return Nothing
    End Function

    Private Sub BindTreeView1(ByVal dt As DataTable, ByVal expandall As Boolean)
        gv_tree.Nodes.Clear()

        Dim node As RadTreeNode
        Dim subNode As RadTreeNode
        Dim subnode1 As RadTreeNode
        Dim subnode2 As RadTreeNode
        Dim subnode3 As RadTreeNode

        Dim oldnode As RadTreeNode = New RadTreeNode("")
        Dim oldsubNode As RadTreeNode = New RadTreeNode("")
        Dim oldsubnode1 As RadTreeNode = New RadTreeNode("")
        Dim oldsubnode2 As RadTreeNode = New RadTreeNode("")
        Dim oldsubnode3 As RadTreeNode = New RadTreeNode("")

        For Each row As DataRow In dt.Rows
            'search in the treeview if any country is already present
            node = Searchnode(clsCommon.myCstr(row.Item(0)))
            subNode = Searchnode(clsCommon.myCstr(row.Item(1)))
            subnode1 = Searchnode(clsCommon.myCstr(row.Item(2)))
            subnode2 = Searchnode(clsCommon.myCstr(row.Item(3)))
            subnode3 = Searchnode(clsCommon.myCstr(row.Item(4)))

            If node IsNot Nothing AndAlso subNode Is Nothing Then
                'Prduction Item is already present
                subNode = New RadTreeNode(clsCommon.myCstr(row.Item(1)))
                'Add Items to country
                If clsCommon.myLen(subNode.Text) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(subNode.Text), clsCommon.myCstr(oldsubNode.Text)) <> CompairStringResult.Equal Then
                    node.Nodes.Add(subNode)
                End If
            ElseIf node Is Nothing AndAlso subNode Is Nothing Then
                node = New RadTreeNode(clsCommon.myCstr(row.Item(0)))
                subNode = New RadTreeNode(clsCommon.myCstr(row.Item(1)))
                'Add Items to country
                If clsCommon.myLen(subNode.Text) > 0 Then
                    node.Nodes.Add(subNode)
                End If
                If clsCommon.myLen(node.Text) > 0 Then
                    gv_tree.Nodes.Add(node)
                End If
            End If

            '======================level2-3
            If subNode IsNot Nothing AndAlso subnode1 Is Nothing Then
                'Prduction Item is already present
                subnode1 = New RadTreeNode(clsCommon.myCstr(row.Item(2)))
                'Add Items to country
                If clsCommon.myLen(subnode1.Text) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(subnode1.Text), clsCommon.myCstr(oldsubnode1.Text)) <> CompairStringResult.Equal Then
                    ' subNode.Nodes.Add(subnode1)
                    node.Nodes(subNode.Text).Nodes.Add(subnode1)
                End If
            ElseIf subNode Is Nothing AndAlso subnode1 Is Nothing Then
                subNode = New RadTreeNode(clsCommon.myCstr(row.Item(1)))
                subnode1 = New RadTreeNode(clsCommon.myCstr(row.Item(2)))
                'Add Items to country
                If clsCommon.myLen(subnode1.Text) > 0 Then
                    node.Nodes(subNode.Text).Nodes.Add(subnode1)
                End If
                If clsCommon.myLen(subNode.Text) > 0 Then
                    node.Nodes.Add(subNode)
                End If
            End If

            '=============Level3-4
            If subnode1 IsNot Nothing AndAlso subnode2 Is Nothing Then
                'Prduction Item is already present
                subnode2 = New RadTreeNode(clsCommon.myCstr(row.Item(3)))
                'Add Items to country
                If clsCommon.myLen(subnode2.Text) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(subnode2.Text), clsCommon.myCstr(oldsubnode2.Text)) <> CompairStringResult.Equal Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes(clsCommon.myCstr(subnode1.Text)).Nodes.Add(subnode2)
                End If

            ElseIf subnode1 Is Nothing AndAlso subnode2 Is Nothing Then
                subnode1 = New RadTreeNode(clsCommon.myCstr(row.Item(2)))
                subnode2 = New RadTreeNode(clsCommon.myCstr(row.Item(3)))
                'Add Items to country
                If clsCommon.myLen(subnode2.Text) > 0 Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes(clsCommon.myCstr(subnode1.Text)).Nodes.Add(subnode2)
                End If
                If clsCommon.myLen(subnode1.Text) > 0 Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes.Add(subnode1)
                End If

            End If

            '===============Level4-5
            If subnode2 IsNot Nothing AndAlso subnode3 Is Nothing Then
                'Prduction Item is already present
                subnode3 = New RadTreeNode(clsCommon.myCstr(row.Item(4)))
                'Add Items to country
                If clsCommon.myLen(subnode3.Text) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(subnode3.Text), clsCommon.myCstr(oldsubnode3.Text)) <> CompairStringResult.Equal Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes(clsCommon.myCstr(subnode1.Text)).Nodes(clsCommon.myCstr(subnode2.Text)).Nodes.Add(subnode3)
                End If

            ElseIf subnode2 Is Nothing AndAlso subnode3 Is Nothing Then
                subnode2 = New RadTreeNode(clsCommon.myCstr(row.Item(3)))
                subnode3 = New RadTreeNode(clsCommon.myCstr(row.Item(4)))
                'Add Items to country
                If clsCommon.myLen(subnode3.Text) > 0 Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes(clsCommon.myCstr(subnode1.Text)).Nodes(clsCommon.myCstr(subnode2.Text)).Nodes.Add(subnode3)
                End If
                If clsCommon.myLen(subnode2.Text) > 0 Then
                    node.Nodes(clsCommon.myCstr(subNode.Text)).Nodes(clsCommon.myCstr(subnode1.Text)).Nodes.Add(subnode2)
                End If
            End If

            oldnode = node
            oldsubNode = subNode
            oldsubnode1 = subnode1
            oldsubnode2 = subnode2
            oldsubnode3 = subnode3
        Next
        If expandall Then
            ' Expand the TreeView
            gv_tree.ExpandAll()
        End If
    End Sub
#End Region

    Private Sub btnExportBOMHead_Click(sender As Object, e As EventArgs) Handles btnExportBOMHead.Click
        '' Export BOM Head
        Try
            Dim qryExport As String
            qryExport = " select PROD_ITEM_CODE as [Main Item Code],BOM_Code as [BOM Code],DESCRIPTION as [Description],PROD_QUANTITY as [Quantity],PROD_ITEM_UNIT_CODE as [Unit Code],BOM_DATE as [BOM Date],START_DATE as [Start Date],END_DATE as [End Date]," &
                        " (case when STATUS='Open' then 'O' when STATUS='Approved' then 'A' when STATUS='On Hold' then 'H' when STATUS='Discontinued' then 'D' else '' end)   as [Status(O,A,H,D)],(case when IS_DEFAULT=0 then 'N' else 'Y' end) as [Default],MIN_BATCH_SIZE as [Min Batch Size] from TSPL_MF_BOM_HEAD"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BOM Head Export")
        End Try
    End Sub
    Private Sub btnExportBOMDetail_Click(sender As Object, e As EventArgs) Handles btnExportBOMDetail.Click
        '' Export BOM Detail
        Try
            Dim qryExport As String
            qryExport = " SELECT PROD_ITEM_CODE as [Main Item Code],BD.BOM_CODE AS [BOM Code],LINE_NO as [Line No],CONSM_ITEM_CODE as [Item Code],ITEM_DESCRIPTION as [Item Desc],CONSM_QUANTITY as [Quantity],CONSM_ITEM_UNIT_CODE as [Unit Code]," _
                       & " SCRAP_PERCENT as [Scrap Percent],WASTAGE_PERCENT as [Wastage Percent],REMARKS as [Remarks] FROM TSPL_MF_BOM_DETAIL BD inner join TSPL_MF_BOM_HEAD BH on BD.BOM_CODE=BH.BOM_CODE "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BOM Detail Export")
        End Try
    End Sub
    Function ImportBOMHead() As Boolean
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Main Item Code", "BOM Code", "Description", "Quantity", "Unit Code", "BOM Date", "Start Date", "End Date", "Status(O,A,H,D)", "Default", "Min Batch Size") Then
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim isNewEntry As Boolean = False
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsBillOfMaterial()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("BOM Code at row no-" & (grow.Index + 1) & " Must be maximum 30 charecters.")
                    End If
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsBillOfMaterial.CheckBOMCode(strCode, Nothing) = False Then
                            Throw New Exception("BOM Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                        End If
                    End If

                    obj.BOM_CODE = strCode
                    Dim strq As String = "SELECT BOM_CODE FROM TSPL_MF_BOM_HEAD WHERE BOM_CODE='" & strCode & "'"
                    Dim dt As DataTable
                    dt = clsDBFuncationality.GetDataTable(strq)
                    If dt.Rows.Count > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If

                    Dim description As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If description.Length > 200 Or (String.IsNullOrEmpty(description)) Then
                        Throw New Exception("Description at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = description

                    Dim bom_date As String = clsCommon.myCDate(grow.Cells("BOM Date").Value)
                    If bom_date.Length = 0 Then
                        Throw New Exception("BOM Date at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.BOM_DATE = bom_date
                    Dim Bom_Field As String
                    Bom_Field = clsCommon.myCstr(grow.Cells("Main Item Code").Value)
                    If Bom_Field.Length > 50 Or Bom_Field.Length = 0 Then
                        Throw New Exception("Main Item Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsItemMaster.CheckItemCode(Bom_Field, Nothing) = False Then
                        Throw New Exception("Main Item Code " & Bom_Field & " at row no-" & (grow.Index + 1) & " is invalid.")
                    End If
                    obj.PROD_ITEM_CODE = Bom_Field
                    obj.PROD_Drawing_No = clsItemMaster.GetItemDrawingNo(obj.PROD_ITEM_CODE, Nothing)
                    'Bom_Field = clsCommon.myCstr(grow.Cells("Revision No").Value)
                    'If Bom_Field.Length > 50 Then
                    '    Throw New Exception("REVISION NO can not be blank or incorrect.")
                    'End If
                    obj.REVISION_NO = clsBillOfMaterial.GetBOMRevisionNo(obj.PROD_ITEM_CODE, "BOM", obj.LOCATION_CODE)

                    Dim start_date As Date
                    start_date = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    If clsCommon.myLen(start_date.ToString) = 0 Then
                        Throw New Exception("Satrt Date at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.START_DATE = start_date

                    Dim end_date As Date? = Nothing
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("End Date").Value)) = 0 Then
                        end_date = Nothing
                    Else
                        end_date = clsCommon.myCDate(grow.Cells("End Date").Value)
                    End If

                    obj.END_DATE = end_date


                    Bom_Field = clsCommon.myCstr(grow.Cells("Status(O,A,H,D)").Value)
                    If Bom_Field.Length > 30 Then
                        Throw New Exception("Status can not be blank or incorrect.")
                    End If
                    If clsCommon.CompairString(Bom_Field, "O") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bom_Field, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bom_Field, "H") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bom_Field, "D") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bom_Field, "O") = CompairStringResult.Equal Then
                            Bom_Field = "Open"
                        ElseIf clsCommon.CompairString(Bom_Field, "A") = CompairStringResult.Equal Then
                            Bom_Field = "Approved"
                        ElseIf clsCommon.CompairString(Bom_Field, "H") = CompairStringResult.Equal Then
                            Bom_Field = "On Hold"
                        ElseIf clsCommon.CompairString(Bom_Field, "D") = CompairStringResult.Equal Then
                            Bom_Field = "Discontinued"
                        End If
                    Else
                        Throw New Exception("Status at row no-" & (grow.Index + 1) & " must be in (O,A,H,D).")
                    End If
                    obj.STATUS = Bom_Field

                    Bom_Field = clsCommon.myCstr(grow.Cells("Default").Value)
                    If Bom_Field <> "Y" And Bom_Field <> "N" Then
                        Throw New Exception("Default at row no-" & (grow.Index + 1) & " Must be Y or N.")
                    End If
                    If Bom_Field = "Y" Then
                        obj.IS_DEFAULT = 1
                    ElseIf Bom_Field = "N" Then
                        obj.IS_DEFAULT = 0
                    End If

                    Dim PROD_QUANTITY As Double = 0
                    PROD_QUANTITY = clsCommon.myCdbl(grow.Cells("QUANTITY").Value)
                    If PROD_QUANTITY <= 0 Then
                        Throw New Exception("QUANTITY(Build Qty) at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.PROD_QUANTITY = PROD_QUANTITY

                    Bom_Field = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If Bom_Field.Length > 12 Then
                        Throw New Exception("Unit Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsUOMInfo.CheckUnitCode(Bom_Field, Nothing) = False Then
                        Throw New Exception("Invalid Unit Code " & Bom_Field & " at line no-" & (grow.Index + 1) & ".")
                    End If
                    obj.PROD_ITEM_UNIT_CODE = Bom_Field

                    Dim MIN_BATCH_SIZE As Double = 0
                    MIN_BATCH_SIZE = clsCommon.myCdbl(grow.Cells("Min Batch Size").Value)
                    If MIN_BATCH_SIZE <= 0 Then
                        Throw New Exception("Min Batch Size at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.MIN_BATCH_SIZE = MIN_BATCH_SIZE

                    obj.SaveData(obj, Nothing, isNewEntry, obj.BOM_CODE)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Function
    Function ImportBOMComponents() As Boolean
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Main Item Code", "BOM Code", "Line No", "Item Code", "Item Desc", "Quantity", "Unit Code", "Scrap Percent", "Wastage Percent", "Remarks") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim obj As New clsBOMDetail
                Dim obj1 As New clsBillOfMaterial
                Dim ObjList As New List(Of clsBillOfMaterial)
                For Each grow As GridViewRowInfo In gv.Rows
                    obj1 = New clsBillOfMaterial
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                    Dim Main_Item_Code As String = clsCommon.myCstr(grow.Cells("Main Item Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("BOM Code at row no-" & (grow.Index + 1) & " Must be maximum 30 charecters.")
                    End If
                    If clsCommon.myLen(strCode) <= 0 AndAlso clsCommon.myLen(Main_Item_Code) <= 0 Then
                        Throw New Exception("BOM Code and Main Item Code both are blank at row no-" & (grow.Index + 1) & ".")
                    End If
                    If clsCommon.myLen(strCode) <= 0 Then
                        If clsItemMaster.CheckItemCode(Main_Item_Code, Nothing) = False Then
                            Throw New Exception("Main Item Code " & Main_Item_Code & " at row no-" & (grow.Index + 1) & " is invalid.")
                        End If
                        Dim strLocationCode As String = clsBillOfMaterial.GetBOMLocationCode(strCode)
                        Dim BomList As List(Of String) = clsBillOfMaterial.GetMainItemBOMList(Main_Item_Code, "BOM", strLocationCode)
                        If BomList.Count <= 0 Then
                            Throw New Exception("No BOM found for Main Item Code " & Main_Item_Code & " at row no-" & (grow.Index + 1) & " is invalid.")
                        ElseIf BomList.Count > 1 Then
                            Throw New Exception("Multiple BOM(" & BomList.Item(0) & "," & BomList.Item(1) & ") found for Main Item Code " & Main_Item_Code & " at row no-" & (grow.Index + 1) & ". Please map BOM Code at this row.")
                        Else
                            strCode = BomList.Item(0)
                        End If
                    Else
                        If clsBillOfMaterial.CheckBOMCode(strCode, Nothing) = False Then
                            Throw New Exception("BOM Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                        End If
                    End If
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("BOM Code can not be blank at row no-" & (grow.Index + 1) & ".")
                    End If
                    obj1.BOM_CODE = strCode

                    Dim Line_No As Integer = clsCommon.myCdbl(grow.Cells("Line No").Value)
                    If Line_No = 0 Then
                        Line_No = clsBillOfMaterial.GetLineNo(obj1.BOM_CODE, Nothing) '
                    End If
                    obj1.Line_No = Line_No
                    Dim BOM_Field As String = ""
                    'BOM_Field = clsCommon.myCstr(grow.Cells("Production Category").Value)
                    'If BOM_Field.Length > 30 Then
                    '    Throw New Exception("Production Category can not be blank or incorrect.")
                    'End If
                    obj1.CONSM_ITEM_CATEGORY_CODE = ""

                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If BOM_Field.Length > 50 Or BOM_Field.Length = 0 Then
                        Throw New Exception("Item Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsItemMaster.CheckItemCode(BOM_Field, Nothing) = False Then
                        Throw New Exception("Item Code " & BOM_Field & " at row no-" & (grow.Index + 1) & " is invalid.")
                    End If
                    obj1.CONSM_ITEM_CODE = BOM_Field

                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Desc").Value)

                    obj1.ITEM_DESCRIPTION = BOM_Field

                    obj1.CONSM_Drawing_No = clsItemMaster.GetItemDrawingNo(obj1.CONSM_ITEM_CODE, Nothing)

                    Dim CONSM_QUANTITY As Double = 0
                    CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    If CONSM_QUANTITY = 0 Then
                        Throw New Exception("Quantity at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj1.CONSM_QUANTITY = CONSM_QUANTITY

                    BOM_Field = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If BOM_Field.Length = 0 Or BOM_Field.Length > 12 Then
                        Throw New Exception("Unit Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsUOMInfo.CheckUnitCode(BOM_Field, Nothing) = False Then
                        Throw New Exception("Invalid Unit Code " & BOM_Field & " at line no-" & (grow.Index + 1) & ".")
                    End If
                    obj1.CONSM_ITEM_UNIT_CODE = BOM_Field

                    Dim SCRAP_PERCENT As Double = 0
                    SCRAP_PERCENT = clsCommon.myCdbl(grow.Cells("Scrap Percent").Value)
                    obj1.SCRAP_PERCENT = SCRAP_PERCENT

                    Dim WASTAGE_PERCENT As Double = 0
                    WASTAGE_PERCENT = clsCommon.myCdbl(grow.Cells("Wastage Percent").Value)
                    obj1.WASTAGE_PERCENT = WASTAGE_PERCENT

                    BOM_Field = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj1.REMARKS = BOM_Field

                    BOM_Field = clsBillOfMaterial.FindBOMRevisionNo(obj1.BOM_CODE, Nothing)
                    'If BOM_Field.Length > 50 Then
                    '    Throw New Exception("REVISION NO can not be blank or incorrect.")
                    'End If
                    obj1.REVISION_NO = BOM_Field


                    ObjList.Add(obj1)

                Next
                clsBOMDetail.SaveDataImport(ObjList, trans)

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Function

    Private Sub btnImportBOMHead_Click(sender As Object, e As EventArgs) Handles btnImportBOMHead.Click
        ImportBOMHead()
    End Sub

    Private Sub btnImportBOMDetail_Click(sender As Object, e As EventArgs) Handles btnImportBOMDetail.Click
        ImportBOMComponents()
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        LoadGridColumns()
        isInsideLoadData = True
        Dim frm As New frmCopyBOM()
        frm.ShowDialog()

        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objReq As clsBillOfMaterial = clsBillOfMaterial.GetData(frm.strFirstPO, NavigatorType.Current, Nothing)
            If objReq IsNot Nothing AndAlso clsCommon.myLen(objReq.BOM_CODE) > 0 Then
                'If (clsCommon.myLen(txtProducedItem.Value) > 0) Then
                txtProducedItem.Value = objReq.PROD_ITEM_CODE
                lblMasterItemName.Text = objReq.PROD_ITEM_NAME
                lblUnitName.Text = objReq.PROD_ITEM_UNIT_CODE
                txtDescription.Text = objReq.DESCRIPTION
                dtpStartDate.Text = objReq.START_DATE
                dtpBOMDate.Text = objReq.BOM_DATE

                ''===FOR REVISION NO
                Dim STRQ As String = "SELECT max(revision_no) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & objReq.PROD_ITEM_CODE & "'"
                Dim rev_no As String = ""
                rev_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRQ))
                If clsCommon.myLen(rev_no) <= 0 Then
                    rev_no = objReq.PROD_ITEM_CODE & "/1"
                Else
                    rev_no = clsCommon.incval(rev_no, 1, 1, True)
                End If

                lblRevisionNo.Text = rev_no
                '======================================
                Dim qry2 As String = " select Drawing_No  from tspl_item_master where Item_Code = '" + txtProducedItem.Value + "'"
                Dim Drawing As String = clsDBFuncationality.getSingleValue(qry2)
                If clsCommon.myLen(Drawing) > 0 Then
                    txtdrawingNo.Text = Drawing
                End If
                Dim qry1 As String = "select sum(PROD_QUANTITY)  from TSPL_MF_BOM_HEAD where BOM_CODE in (" + clsCommon.GetMulcallString(frm.arrPONo) + ") "
                Dim Qty As Double = clsDBFuncationality.getSingleValue(qry1)
                txtBuildQty.Text = Qty

                Dim qry3 As String = "select sum(MIN_BATCH_SIZE)  from TSPL_MF_BOM_HEAD where BOM_CODE in (" + clsCommon.GetMulcallString(frm.arrPONo) + ") "
                Dim min_batch As Double = clsDBFuncationality.getSingleValue(qry3)
                txtMinBatchQty.Text = min_batch
                'End If

            End If
            If gvBOM.Rows.Count > 0 AndAlso clsCommon.myLen(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value) <= 0 Then
                gvBOM.Rows.RemoveAt(gvBOM.Rows.Count - 1)
            End If
            For Each obj As clsBillOfMaterial In frm.ArrReturn
                gvBOM.Rows.AddNew()
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = gvBOM.Rows.Count
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj.CONSM_ITEM_CODE
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj.CONSM_ITEM_UNIT_CODE
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCategoryCode).Value = Nothing
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = obj.CONSM_QUANTITY
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colDrawingNo).Value = obj.PROD_Drawing_No
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colscrap_per).Value = obj.SCRAP_PERCENT
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colwastage_per).Value = obj.WASTAGE_PERCENT
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colPercentage).Value = obj.Percentage
                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAlternateItemCode).Value = obj.Alternate_Item_Code
            Next
        End If
        isInsideLoadData = False
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        ''export to notepad
        ''new event create for export,becuase event is missed by last developer work.
        Dim clsoutpoutstream As New System.IO.MemoryStream
        Dim clsprompt As New SaveFileDialog()
        clsprompt.Title = "Select Output File"
        clsprompt.Filter = "Text Files {*.txt}|*.txt"

        If clsprompt.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Return
        End If

        clsWriter = New System.IO.StreamWriter(clsprompt.FileName)

        ExportToText(gv_tree.Nodes, 0, clsoutpoutstream)

        clsoutpoutstream.Flush()
        clsoutpoutstream.Close()

        Process.Start(clsprompt.FileName)

    End Sub

    Private Sub gvCost_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvCost.CellValueChanged
        ''Try
        ''    If gvCost.CurrentColumn Is gvCost.Columns(colStdCost) Then

        ''        Dim Cost1 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value)
        ''        Dim Cost2 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value)
        ''        Dim Cost3 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value)
        ''        Dim Cost4 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value)
        ''        Dim Cost5 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value)
        ''        Dim Cost6 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value)
        ''        Dim Cost7 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value)
        ''        Dim aaaa As Double = Cost1 + Cost2 + Cost3 + Cost4 + Cost5 + Cost6 + Cost7
        ''        'Me.gvCost.Rows(rowTotal).Cells(colStdCost).Value = Cost1 + Cost2 + Cost3 + Cost4 + Cost5 + Cost6 + Cost7

        ''        Try
        ''            gvCost.Rows(rowTotal).Cells(colStdCost).Value = 6000
        ''        Catch ex As Exception

        ''        End Try
        ''    End If
        ''Catch ex As Exception

        ''End Try

    End Sub

    Private Sub gvCost_Click(sender As Object, e As EventArgs) Handles gvCost.Click
        Try
            If gvCost.CurrentColumn Is gvCost.Columns(colStdCost) Then
                gvCost.Rows(7).Cells(colStdCost).ReadOnly = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvCost_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles gvCost.CellEndEdit
        Try
            If gvCost.CurrentColumn Is gvCost.Columns(colStdCost) Then

                Dim Cost1 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowDirectMaterialCost).Cells(colStdCost).Value)
                Dim Cost2 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowPackageingCost).Cells(colStdCost).Value)
                Dim Cost3 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowSetupLaborCost).Cells(colStdCost).Value)
                Dim Cost4 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowDirectLaborCost).Cells(colStdCost).Value)
                Dim Cost5 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowOverheadCost).Cells(colStdCost).Value)
                Dim Cost6 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowSubContractCost).Cells(colStdCost).Value)
                Dim Cost7 As Double = clsCommon.myCdbl(Me.gvCost.Rows(rowToolCost).Cells(colStdCost).Value)
                Dim aaaa As Double = Cost1 + Cost2 + Cost3 + Cost4 + Cost5 + Cost6 + Cost7
                'Me.gvCost.Rows(rowTotal).Cells(colStdCost).Value = Cost1 + Cost2 + Cost3 + Cost4 + Cost5 + Cost6 + Cost7

                Try
                    gvCost.Rows(rowTotal).Cells(colStdCost).Value = aaaa
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvCost_ValueChanged(sender As Object, e As EventArgs) Handles gvCost.ValueChanged

    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            'Dim WhrCls As String = " Location_Type='Physical'  "
            'If clsCommon.myLen(arrLoc) > 0 Then
            '    WhrCls += "  and  Location_Code in (" + arrLoc + ")"
            'End If
            'txtLocation.Value = clsLocation.getFinder(WhrCls, Me.txtLocation.Value, isButtonClicked)
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    lblLocation.Text = clsLocation.GetName(Me.txtLocation.Value, Nothing)
            'End If

            'Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name] from TSPL_Location_MASTER"
            'Dim WhrCls As String = " TSPL_LOCATION_MASTER.IsMainPlant='0' "
            'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '    WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            'End If
            'txtLocation.Value = clsCommon.ShowSelectForm("MulBDELocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            'lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(arrLoc) > 0 Then
                WhrCls += "  and  Location_Code in (" + arrLoc + ")"
            End If
            txtLocation.Value = clsLocation.getFinder(WhrCls, Me.txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(Me.txtLocation.Value, Nothing)
            End If
            'txtLocation.Value = clsCommon.ShowSelectForm("MulBDELocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            'lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnCC_Click(sender As Object, e As EventArgs) Handles btnCC.Click

        Try
            Dim isNewEntry As Boolean = True
            Dim whcls As String = Nothing
            Dim whcls1 As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whcls = " T1.LOCATION_CODE =" & objCommonVar.strCurrUserLocations & ""
                whcls1 = " AND LOCATION_CODE =" & objCommonVar.strCurrUserLocations & ""
            End If
            'isLoadCopy = True
            Dim qry As String = "SELECT T1.LOCATION_CODE,T1.BOM_CODE AS Code,T1.DESCRIPTION,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
            qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T1.PROD_QUANTITY,T1.PROD_ITEM_UNIT_CODE,"
            qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE "

            Dim strTender As String = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", whcls, txtCode.Value, " convert(date, BOM_DATE,103) desc , Code desc ", True)
            If clsCommon.myLen(strTender) > 0 Then
                LoadData(strTender, NavigatorType.Current)
                txtCode.Value = ""

                txtCode.MyReadOnly = False

                isNewEntry = True
                btnsave.Text = "Save"
                'lblTenderSeqNo.Text = ""
                btnsave.Enabled = True
                btndelete.Enabled = False
                btnPost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending

            End If

            Dim objReq As clsBillOfMaterial = clsBillOfMaterial.GetData(strTender, NavigatorType.Current, Nothing)


            ''===FOR REVISION NO
            Dim STRQ As String = "SELECT max(revision_no) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE= '" & objReq.PROD_ITEM_CODE & "'" + whcls1
            Dim revision_no As String = ""
            revision_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRQ))
            If clsCommon.myLen(revision_no) <= 0 Then
                ' revision_no = objReq.LOCATION_CODE & "/" & objReq.PROD_ITEM_CODE & "/001"
                revision_no = objReq.PROD_ITEM_CODE & "/" & objReq.LOCATION_CODE & "/001"
            Else
                If clsCommon.myLen(revision_no) <= 0 Then
                    'revision_no = objReq.PROD_ITEM_CODE & "/" & objReq.LOCATION_CODE & "/001"
                    revision_no = objReq.PROD_ITEM_CODE & "/" & objReq.LOCATION_CODE & "/001"
                Else
                    'Dim resultArray As New List(Of String)(revision_no.Split("/"))
                    'If clsCommon.myCstr(revision_no).Contains(objReq.LOCATION_CODE) Then
                    '    revision_no = resultArray(1) + "/" + objReq.LOCATION_CODE + "/" + resultArray(2)
                    'Else
                    '    revision_no = resultArray(0) + "/" + objReq.LOCATION_CODE + "/" + resultArray(1)
                    'End If
                    revision_no = clsCommon.incval(revision_no)
                End If
                lblRevisionNo.Text = revision_no
                objReq.REVISION_NO = clsBillOfMaterial.GetBOMRevisionNo(obj.PROD_ITEM_CODE, "BOM", obj.LOCATION_CODE)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class