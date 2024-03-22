'Namespace Telerik.Examples.WinControls.GridView.Export.ExportHierarchy
'============Created By Monika 14/10/2014''======================BM00000004328

Imports common
Imports System.Data.SqlClient
Imports System.IO
'Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Text
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.UI.Export
Imports System.ComponentModel
Imports System.Drawing
Imports XpertERPEngine
Imports Telerik.WinControls


Public Class frmRiceBOM
    Inherits FrmMainTranScreen

    Dim clsWriter As System.IO.StreamWriter

#Region "Variables"
    '' component grid columns
    Dim ReportID As String = "RICE-BOM"
    Dim Errorcontrol As New clsErrorControl()
    Const colLineNo As String = "LineNo"
    Const colItemCategoryCode As String = "ItemCategoryCode"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colqty As String = "Qty"
    Const colUnitCode As String = "UnitCode"
    Const colscrap_per As String = "Scrap_per"
    Const colwastage_per As String = "Wastage_per"
    Const colRemarks As String = "Remarks"
    Const colPrinci As String = "Principle"
    Const colQty_Percentage As String = "Qty_Pers"
    Dim Princi_On As Boolean = False
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
    Dim obj As New clsRiceBOM
    Private ObjList As New List(Of clsRiceBOM)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
#End Region
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
        StdCost.ReadOnly = True
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

        ReStoreGridLayout()
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
        OperationCode.HeaderImage = My.Resources.search4
        OperationCode.TextImageRelation = TextImageRelation.TextBeforeImage
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
        workCenterCode.HeaderImage = My.Resources.search4
        workCenterCode.TextImageRelation = TextImageRelation.TextBeforeImage
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

        ReStoreGridLayout()
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
        ResourceCode.HeaderImage = My.Resources.search4
        ResourceCode.TextImageRelation = TextImageRelation.TextBeforeImage
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

        ReStoreGridLayout()
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
        ToolTypeCode.HeaderImage = My.Resources.search4
        ToolTypeCode.TextImageRelation = TextImageRelation.TextBeforeImage
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

        ReStoreGridLayout()
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
        ItemCategoryCode.IsVisible = False
        ItemCategoryCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCategoryCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.HeaderImage = My.Resources.search4
        ItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
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

        Dim repoprinci As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoprinci.Width = 60
        repoprinci.HeaderText = "Principle"
        repoprinci.Name = colPrinci
        repoprinci.IsVisible = False
        If Princi_On Then
            repoprinci.IsVisible = True
        End If
        gvBOM.Columns.Add(repoprinci)

        Dim repoqty_pers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty_pers.FormatString = ""
        repoqty_pers.DecimalPlaces = 2
        repoqty_pers.Name = colQty_Percentage
        repoqty_pers.HeaderText = "Percentage"
        repoqty_pers.Width = 80
        gvBOM.Columns.Add(repoqty_pers)

        qty.FormatString = ""
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
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

        ReStoreGridLayout()
    End Sub


    Private Sub frmBillOfMaterialCosting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmBillOfMaterialCosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Princi_On = True 'clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.Princi_Bom, clsFixedParameterCode.Princi_Bom, Nothing) = "0", False, True))
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

        funReset()
        txtDescription.MendatroryField = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRiceBOM)
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
        txtprocess_amt.Text = 0
        txtadmin_amt.Text = 0
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        Me.txtDescription.Text = ""
        Me.txtProducedItem.Value = Nothing
        Me.lblMasterItemName.Text = ""
        Me.txtBuildQty.Text = ""
        Me.dtpBOMDate.Value = Today
        Me.dtpStartDate.Value = Today
        Me.dtpEndDate.Value = Today
        Me.dtpEndDate.Checked = False
        Me.cboBOMStatus.SelectedValue = "Open"
        Me.txtBuildQty.Text = ""
        Me.txtMinBatchQty.Text = ""
        txtDocPath.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
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



        RadPageView1.SelectedPage = pageGeneral
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

        obj = clsRiceBOM.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0) Then
            btnPrint.Enabled = True
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
            Me.lblRevisionNo.Text = obj.REVISION_NO
            Me.dtpStartDate.Value = obj.START_DATE

            txtprocess_amt.Text = obj.Process_charge
            txtadmin_amt.Text = obj.Admin_Charge

            If clsCommon.myLen(obj.END_DATE) > 0 Then
                Me.dtpEndDate.Value = obj.END_DATE
                Me.dtpEndDate.Checked = True
            Else
                Me.dtpEndDate.Checked = False
            End If

            Me.cboBOMStatus.Text = obj.STATUS
            Me.txtDocPath.Text = obj.ATTACHED_DOC_PATH
            Me.txtProducedItem.Value = obj.PROD_ITEM_CODE
            Me.txtBuildQty.Text = obj.PROD_QUANTITY
            Me.lblUnitName.Text = obj.PROD_ITEM_UNIT_CODE
            Me.txtMinBatchQty.Text = obj.MIN_BATCH_SIZE
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
            If (clsRiceBOM.ObjList IsNot Nothing AndAlso clsRiceBOM.ObjList.Count > 0) Then
                For Each obj As clsRiceBOM In clsRiceBOM.ObjList
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
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colQty_Percentage).Value = obj.Qty_Pers
                Next
            Else
                gvBOM.Rows.AddNew()
            End If
        End If
        '' display operations
        gvOperations.Rows.Clear()
        If obj.ObjListOP IsNot Nothing And obj.ObjListOP.Count > 0 Then
            For Each objOP As clsRiceBOMOperations In obj.ObjListOP
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

                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colOperationCode).Tag = clsRiceBOMResources.GetBomResources(objOP.BOM_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)
                gvOperations.Rows(gvOperations.Rows.Count - 1).Cells(colworkCenterCode).Tag = clsRiceBOMToolTypes.GetBomToolTypes(objOP.BOM_CODE, objOP.OPERATION_CODE, objOP.WORK_CENTER_CODE)

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
            Dim obj As New clsRiceBOM
            obj.BOM_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text.Replace("'", "`")
            obj.BOM_DATE = Me.dtpBOMDate.Value
            obj.REVISION_NO = Me.lblRevisionNo.Text
            obj.START_DATE = Me.dtpStartDate.Value
            obj.Process_charge = clsCommon.myCdbl(txtprocess_amt.Text)
            obj.Admin_Charge = clsCommon.myCdbl(txtadmin_amt.Text)

            If Me.dtpEndDate.Checked = True Then
                obj.END_DATE = Me.dtpEndDate.Value
            Else
                obj.END_DATE = Nothing
            End If

            obj.STATUS = Me.cboBOMStatus.Text
            obj.ATTACHED_DOC_PATH = Me.txtDocPath.Text
            obj.PROD_ITEM_CODE = Me.txtProducedItem.Value
            obj.PROD_QUANTITY = Me.txtBuildQty.Text
            obj.PROD_ITEM_UNIT_CODE = Me.lblUnitName.Text
            obj.MIN_BATCH_SIZE = clsCommon.myCdbl(Me.txtMinBatchQty.Text)
            obj.CREATED_BY = Me.lblCreatedBy.Text
            'obj.APPROVED_BY = Me.lblApprovedBy.Text

            Dim obj1 As clsRiceBOM
            ObjList = New List(Of clsRiceBOM)
            For Each grow As GridViewRowInfo In gvBOM.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                    obj1 = New clsRiceBOM()

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
                    obj1.Qty_Pers = clsCommon.myCdbl(grow.Cells(colQty_Percentage).Value)

                    ObjList.Add(obj1)
                End If
            Next

            '' saving operations
            Dim objListOP As New List(Of clsRiceBOMOperations)
            Dim objListResource As New List(Of clsRiceBOMResources)
            Dim objListToolTypes As New List(Of clsRiceBOMToolTypes)

            Dim objOP As clsRiceBOMOperations
            For Each row As GridViewRowInfo In gvOperations.Rows
                If clsCommon.myLen(row.Cells(colOperationCode).Value) > 0 And clsCommon.myLen(row.Cells(colworkCenterCode).Value) > 0 Then
                    objOP = New clsRiceBOMOperations
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
                        Dim objListRes As List(Of clsRiceBOMResources)
                        objListRes = row.Cells(colOperationCode).Tag

                        objListResource.AddRange(objListRes)

                    End If

                    '' tools 

                    If row.Cells(colworkCenterCode).Tag IsNot Nothing Then
                        Dim objListTools As List(Of clsRiceBOMToolTypes)
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
            Dim objListCost As New List(Of clsRiceBOMCosting)
            Dim objCost As clsRiceBOMCosting


            objCost = New clsRiceBOMCosting
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

            objCost = New clsRiceBOMCosting
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
            str = "UPDATE TSPL_MF_BOM_HEAD set ATTACHED_DOC = @BLOBData where BOM_CODE = '" + txtCode.Value + "' and trans_type='RICE' "
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
        Else
            Return False
        End If
    End Function
    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_BOM_HEAD where BOM_CODE = '" + txtCode.Value + "' and trans_type='RICE' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        If clsCommon.myCdbl(txtBuildQty.Text) <= 0 Then
            myMessages.blankValue(Me, "Build Quantity must be greater than zero.", Me.Text)
            RadPageView1.SelectedPage = pageGeneral
            txtBuildQty.Focus()
            txtBuildQty.Select()
            Errorcontrol.SetError(txtBuildQty, "Build Quantity must be greater than zero.")
            Return False
        Else
            Errorcontrol.ResetError(txtBuildQty)
        End If

        'If clsCommon.myCdbl(txtprocess_amt.Text) <= 0 Then
        '    myMessages.blankValue("Processing Charge must be greater than zero.")
        '    RadPageView1.SelectedPage = pageGeneral
        '    txtprocess_amt.Focus()
        '    txtprocess_amt.Select()
        '    Errorcontrol.SetError(txtprocess_amt, "Processing Charge must be greater than zero.")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(txtprocess_amt)
        'End If

        'If clsCommon.myCdbl(txtadmin_amt.Text) <= 0 Then
        '    myMessages.blankValue("Admin Charge must be greater than zero.")
        '    RadPageView1.SelectedPage = pageGeneral
        '    txtadmin_amt.Focus()
        '    txtadmin_amt.Select()
        '    Errorcontrol.SetError(txtadmin_amt, "Admin Charge must be greater than zero.")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(txtadmin_amt)
        'End If

        Dim ii As Int16 = 0
        Dim Princi_Count As Integer = 0
        Dim total_pers As Decimal = 0
        Dim Prod_Item_Type As String = clsItemMaster.GetItemType(Me.txtProducedItem.Value, Nothing)

        For Each grow As GridViewRowInfo In gvBOM.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                ii += 1
                'total_pers += clsCommon.myCdbl(grow.Cells(colQty_Percentage).Value)

                'If total_pers > 100 Then
                '    myMessages.blankValue("Total percentage should not exceed 100.")
                '    RadPageView1.SelectedPage = pageGeneral
                '    txtBuildQty.Focus()
                '    txtBuildQty.Select()
                '    Errorcontrol.SetError(txtBuildQty, "Build Quantity must be greater than zero.")
                '    Return False
                'Else
                '    Errorcontrol.ResetError(txtBuildQty)
                'End If
            End If
            If (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemType).Value), "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemType).Value), "F") = CompairStringResult.Equal) AndAlso clsCommon.myCBool(grow.Cells(colPrinci).Value) = True Then
                Princi_Count += 1
            End If

        Next
        If ii = 0 Then
            clsCommon.MyMessageBoxShow("Please Fill Raw Material Grid.")
            RadPageView1.SelectedPage = pageComponent
            Return False
        End If

        If Princi_Count <= 0 Then
            RadPageView1.SelectedPage = pageComponent
            clsCommon.MyMessageBoxShow("Please select principle item.", Me.Text)
            Return False
        End If

        If Princi_Count > 1 Then
            RadPageView1.SelectedPage = pageComponent
            clsCommon.MyMessageBoxShow("More than one principle not allowed.", Me.Text)
            Return False
        End If
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
                If (clsRiceBOM.DeleteData(txtCode.Value)) Then
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

            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS ITEM_NAME,ITEM_TYPE AS TYPE FROM TSPL_ITEM_MASTER "
            txtProducedItem.Value = clsCommon.ShowSelectForm("ITEMFND", qry, "Code", "ITEM_TYPE not IN ('F','S') ", txtProducedItem.Value, "", isButtonClicked)

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
            Dim rev_no As String
            STRQ = "SELECT (COUNT(BOM_CODE)) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & txtProducedItem.Value & "' and trans_type='RICE' "
            Dim dt_rev As DataTable
            dt_rev = clsDBFuncationality.GetDataTable(STRQ)
            If dt_rev.Rows(0).Item("rev") = 0 Then
                rev_no = txtProducedItem.Value
            Else
                rev_no = txtProducedItem.Value & "/" & (dt_rev.Rows(0).Item("rev") + 1)
            End If

            Me.lblRevisionNo.Text = rev_no
            CalculateBOMCosting()
        Catch ex As Exception

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

        Dim str As String = "select count(*) from TSPL_MF_BOM_HEAD where BOM_CODE ='" + txtCode.Value + "' and trans_type='RICE' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.BOM_CODE AS Code,T1.DESCRIPTION,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
            qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T1.PROD_QUANTITY,T1.PROD_ITEM_UNIT_CODE,"
            qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", " T1.trans_type='RICE'", txtCode.Value, "Code", isButtonClicked)
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
            Me.cboBOMStatus.Text = "Approved"
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsRiceBOM.PostData(txtCode.Value, True)) Then
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

    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
        If gvBOM.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvBOM.Columns(colItemCategoryCode) Then
                Dim strq As String = ""
                strq = "select PROD_ITEM_CATEGORY_CODE as Code,DESCRIPTION from TSPL_MF_PRODUCTION_ITEM_CATEGORY "
                gvBOM.CurrentRow.Cells(colItemCategoryCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCategoryCode).Value))
            End If


            If e.Column Is gvBOM.Columns(colItemCode) Then
                Dim obj As clsRiceBOM = clsRiceBOM.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), "ITEM_TYPE IN ('F','S') ", False)
                ''and prod_item_category_code='" & gvBOM.CurrentRow.Cells(colItemCategoryCode).Value & "'
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                    gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                    gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                    gvBOM.CurrentRow.Cells(colItemType).Value = obj.ITEM_TYPE

                End If
            End If

            If e.Column Is gvBOM.Columns(colQty_Percentage) Then
                gvBOM.CurrentRow.Cells(colqty).Value = System.Math.Round((clsCommon.myCdbl(txtBuildQty.Text) * clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colQty_Percentage).Value)) / 100, 2)
            End If

            If e.Column Is gvBOM.Columns(colqty) Then
                If clsCommon.myCdbl(txtBuildQty.Text) > 0 Then
                    gvBOM.CurrentRow.Cells(colQty_Percentage).Value = System.Math.Round((100 * clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)) / clsCommon.myCdbl(txtBuildQty.Text), 2)
                Else
                    gvBOM.CurrentRow.Cells(colQty_Percentage).Value = 0
                End If
            End If

            isCellValueChangedOpen = False
        End If
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
    Sub showAttachedDocs(ByVal obj As clsRiceBOM)

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


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
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
            qry += " where 2=2"

            If txtCode.Value <> "" Then
                qry += " and  TSPL_MF_BOM_HEAD.BOM_CODE='" & txtCode.Value & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptBOMPrint", "Bill Of Material")
            frmCRV = Nothing
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
    Function getResourcesFromResourceMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsRiceBOMResources)
        Dim objList As New List(Of clsRiceBOMResources)
        Dim obj1 As clsRiceBOMResources
        Dim objList1 As List(Of clsWorkCenterResourceDetail)

        objList1 = clsWorkCenterResourceDetail.GetData(WorkCenterCode, trans)
        If Not objList1 Is Nothing And objList1.Count > 0 Then
            For Each obj As clsWorkCenterResourceDetail In objList1
                obj1 = New clsRiceBOMResources
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
    Function FillResources(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsRiceBOMResources)
        Dim objList As New List(Of clsRiceBOMResources)
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
                    objList = clsRiceBOMResources.GetBomResources(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
            For Each obj As clsRiceBOMResources In objList
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

    Function getToolTypeFromToolTypeMaster(ByVal OperationCode As String, ByVal WorkCenterCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsRiceBOMToolTypes)
        Dim objList As New List(Of clsRiceBOMToolTypes)
        Dim obj1 As clsRiceBOMToolTypes
        Dim objList1 As List(Of clsWorkCenterToolDetail)

        objList1 = clsWorkCenterToolDetail.GetData(WorkCenterCode, trans)
        If Not objList1 Is Nothing And objList1.Count > 0 Then
            For Each obj As clsWorkCenterToolDetail In objList1
                obj1 = New clsRiceBOMToolTypes
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
    Function FillToolTypes(ByVal Op_RowNo As Integer, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsRiceBOMToolTypes)
        Dim objList As New List(Of clsRiceBOMToolTypes)
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
                    objList = clsRiceBOMToolTypes.GetBomToolTypes(Me.txtCode.Value, gvOperations.Rows(Op_RowNo).Cells(colOperationCode).Value, gvOperations.Rows(Op_RowNo).Cells(colworkCenterCode).Value, trans)
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
            For Each obj As clsRiceBOMToolTypes In objList
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
                strq = "SELECT TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE as Code,TSPL_MF_RESOURCE_MASTER.DESCRIPTION AS Name FROM " & _
                       " TSPL_MF_WORK_CENTER_RESOURCE_DETAIL LEFT JOIN TSPL_MF_RESOURCE_MASTER " & _
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
        Dim objList As New List(Of clsRiceBOMResources)
        Dim obj As clsRiceBOMResources
        For Each rw As GridViewRowInfo In gvResources.Rows
            If clsCommon.myLen(rw.Cells(colResourceCode).Value) > 0 Then

                Me.gvResources.Rows(rw.Index).Cells(colResourceTotalCost).Value = Me.gvResources.Rows(rw.Index).Cells(colResourceQty).Value * Me.gvResources.Rows(rw.Index).Cells(colResourceUnitCost).Value
                obj = New clsRiceBOMResources
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
        Dim objList As New List(Of clsRiceBOMToolTypes)
        Dim obj As clsRiceBOMToolTypes
        For Each rw As GridViewRowInfo In gvTools.Rows
            If clsCommon.myLen(rw.Cells(colToolTypeCode).Value) > 0 Then

                Me.gvTools.Rows(rw.Index).Cells(colToolTypeTotalCost).Value = Me.gvTools.Rows(rw.Index).Cells(colToolTypeQty).Value * Me.gvTools.Rows(rw.Index).Cells(colToolTypeUnitCost).Value
                obj = New clsRiceBOMToolTypes
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
                strq = "SELECT TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE as Code,TSPL_MF_TOOL_TYPE.DESCRIPTION AS Name FROM " & _
                       " TSPL_MF_WORK_CENTER_TOOL_DETAIL LEFT JOIN TSPL_MF_TOOL_TYPE " & _
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
            ResetCosting()
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
                Dim objlist As List(Of clsRiceBOMResources)

                objlist = row.Cells(colOperationCode).Tag
                For Each obj As clsRiceBOMResources In objlist
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
                Dim objlist As List(Of clsRiceBOMToolTypes)

                objlist = row.Cells(colworkCenterCode).Tag
                For Each obj As clsRiceBOMToolTypes In objlist
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

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVBOM", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvBOM.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvBOM.Columns.Count - 1 Step ii + 1
                        gvBOM.Columns(ii).IsVisible = False
                        gvBOM.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvBOM.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                '===============cost
                obj = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVCOST", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvCost.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvCost.Columns.Count - 1 Step ii + 1
                        gvCost.Columns(ii).IsVisible = False
                        gvCost.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvCost.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                '===============resource
                obj = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVRES", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvResources.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvResources.Columns.Count - 1 Step ii + 1
                        gvResources.Columns(ii).IsVisible = False
                        gvResources.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvResources.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                '===============operation
                obj = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVOPR", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvOperations.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvOperations.Columns.Count - 1 Step ii + 1
                        gvOperations.Columns(ii).IsVisible = False
                        gvOperations.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvOperations.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                '===============tools
                obj = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVTOOL", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvTools.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvTools.Columns.Count - 1 Step ii + 1
                        gvTools.Columns(ii).IsVisible = False
                        gvTools.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvTools.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvBOM.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID + "GVBOM"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvBOM.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvBOM.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
            End If
            '==resource==================
            gvResources.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID + "GVRES"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvResources.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvResources.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
            End If
            '============tool===============
            gvTools.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID + "GVTOOL"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvTools.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvTools.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
            End If
            '==operation=====================
            gvOperations.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID + "GVOPR"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvOperations.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvOperations.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
            End If
            '==========cost grid==============
            gvCost.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID + "GVCOST"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvCost.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvCost.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID + "GVBOM", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVCOST", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVRES", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVOPR", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVTOOL", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
    End Sub
End Class