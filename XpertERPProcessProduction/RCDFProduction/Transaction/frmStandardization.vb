Imports common
Imports System.Data.SqlClient

Public Class frmStandardization
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

    ''Produce Columns
    Const colProduceSNo As String = "colProduceSNo"
    Const colProduceBOMCode As String = "colProduceBOMCode"
    Const colProduceBOM As String = "colProduceBOM"
    Const colProduceItemCode As String = "colProduceItemCode"
    Const colProduceItem As String = "colProduceItem"
    Const colProduceItemProductType As String = "colProduceItemProductType"
    Const colProduceUOM As String = "colProduceUOM"
    Const colProduceReqQty As String = "colProduceReqQty"
    Const colProduceReqFAT As String = "colProduceReqFAT"
    Const colProduceReqFATKG As String = "colProduceReqFATKG"
    Const colProduceReqSNF As String = "colProduceReqSNF"
    Const colProduceReqSNFKG As String = "colProduceReqSNFKG"
    Const colProduceQty As String = "colProduceQty"
    Const colProduceFAT As String = "colProduceFAT"
    Const colProduceFATKG As String = "colProduceFATKG"
    Const colProduceSNF As String = "colProduceSNF"
    Const colProduceSNFKG As String = "colProduceSNFKG"
    Const colProduceStdInLocation As String = "colProduceStdInLocation"
    Const colProduceStdInLocationName As String = "colProduceStdInLocationName"

    ''Issue Columns


    Const colIssueSNo As String = "colIssueSNo"
    Const colIssueItemCode As String = "colIssueItemCode"
    Const colIssueItemName As String = "colIssueItemName"
    Const colIssueItemProductType As String = "colIssueItemProductType"
    Const colIssueLocationCode As String = "colIssueLocationCode"
    Const colIssueLocation As String = "colIssueLocation"
    Const colIssueQty As String = "colIssueQty"
    Const colIssueUOM As String = "colIssueUOM"
    Const colIssueFAT As String = "colIssueFAT"
    Const colIssueFATKG As String = "colIssueFATKG"
    Const colIssueSNF As String = "colIssueSNF"
    Const colIssueSNFKG As String = "colIssueSNFKG"
    Const colIssueReqFAT As String = "colIssueReqFAT"
    Const colIssueReqSNF As String = "colIssueReqSNF"
    Const colIssueDiffFAT As String = "colIssueDiffFAT"
    Const colIssueDiffFATKG As String = "colIssueDiffFATKG"
    Const colIssueDiffSNF As String = "colIssueDiffSNF"
    Const colIssueDiffSNFKG As String = "colIssueDiffSNFKG"
    Const colIssueRemark As String = "colIssueRemark"



    '' add/remove tab columns
    Const colARSno As String = "colARSno"
    Const colARItemCode As String = "colARItemCode"
    Const colARItemName As String = "colARItemName"
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

    Private Sub LoadBlankGridProduce()
        gvProduce.Rows.Clear()
        gvProduce.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colProduceSNo
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(reposno)

        Dim repobomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobomcode.FormatString = ""
        repobomcode.Name = colProduceBOMCode
        repobomcode.Width = 100
        repobomcode.HeaderText = "BOM Code"
        repobomcode.ReadOnly = True
        repobomcode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repobomcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gvProduce.MasterTemplate.Columns.Add(repobomcode)

        Dim repoBOMDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBOMDesc.FormatString = ""
        repoBOMDesc.Name = colProduceBOM
        repoBOMDesc.Width = 150
        repoBOMDesc.HeaderText = "BOM Description"
        repoBOMDesc.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoBOMDesc)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colProduceItemCode
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoicode.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoicode)

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colProduceItem
        repoiname.Width = 150
        repoiname.HeaderText = "Item Description"
        repoiname.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoiname)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colitemtype
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoitype)

        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colProduceItemProductType
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoPtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colProduceUOM
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colUOMDesc
        repouomname.Width = 150
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repouomname)

        Dim repoShiftCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftCode.FormatString = ""
        repoShiftCode.Name = colShift_Code
        repoShiftCode.Width = 100
        repoShiftCode.HeaderText = "Shift Code"
        repoShiftCode.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoShiftCode)

        Dim repoShiftDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftDesc.FormatString = ""
        repoShiftDesc.Name = colShift_Desc
        repoShiftDesc.Width = 150
        repoShiftDesc.HeaderText = "Shift Description"
        repoShiftDesc.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoShiftDesc)

        Dim repoSection_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSection_Code.FormatString = ""
        repoSection_Code.Name = colSection_Code
        repoSection_Code.Width = 100
        repoSection_Code.HeaderText = "Section Code"
        repoSection_Code.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoSection_Code)

        Dim repoSection_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSection_Desc.FormatString = ""
        repoSection_Desc.Name = colSection_Desc
        repoSection_Desc.Width = 150
        repoSection_Desc.HeaderText = "Section Description"
        repoSection_Desc.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoSection_Desc)

        Dim repoQuantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQuantity.FormatString = ""
        repoQuantity.Name = colProduceReqQty
        repoQuantity.Width = 100
        repoQuantity.HeaderText = "Quantity"
        repoQuantity.DecimalPlaces = DecimalPointQty
        repoQuantity.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoQuantity.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoQuantity)

        Dim repoRequirFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirFat.FormatString = ""
        repoRequirFat.Name = colProduceReqFAT
        repoRequirFat.Width = 100
        repoRequirFat.HeaderText = "Required FAT%"
        repoRequirFat.DecimalPlaces = DecimalPointFatSNFPer
        repoRequirFat.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoRequirFat.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoRequirFat)

        Dim repoRequirFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirFatKG.FormatString = ""
        repoRequirFatKG.Name = colProduceReqFATKG
        repoRequirFatKG.Width = 150
        repoRequirFatKG.HeaderText = "Required FAT KG"
        repoRequirFatKG.DecimalPlaces = DecimalPointQty
        repoRequirFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoRequirFatKG.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoRequirFatKG)

        Dim repoRequirSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirSnf.FormatString = ""
        repoRequirSnf.Name = colProduceReqSNF
        repoRequirSnf.Width = 150
        repoRequirSnf.HeaderText = "Required SNF%"
        repoRequirSnf.DecimalPlaces = DecimalPointFatSNFPer
        repoRequirSnf.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoRequirSnf.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoRequirSnf)

        Dim repoRequirSnfKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirSnfKG.FormatString = ""
        repoRequirSnfKG.Name = colProduceReqSNFKG
        repoRequirSnfKG.Width = 150
        repoRequirSnfKG.HeaderText = "Required SNF KG"
        repoRequirSnfKG.DecimalPlaces = DecimalPointQty
        repoRequirSnfKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoRequirSnfKG.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoRequirSnfKG)

        Dim repoProduced_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_Qty.FormatString = ""
        repoProduced_Qty.Name = colProduceQty
        repoProduced_Qty.Width = 150
        repoProduced_Qty.HeaderText = "Produced Quantity"
        repoProduced_Qty.DecimalPlaces = DecimalPointQty
        repoProduced_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        gvProduce.MasterTemplate.Columns.Add(repoProduced_Qty)

        Dim repoProducedFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProducedFat.FormatString = ""
        repoProducedFat.Name = colProduceFAT
        repoProducedFat.Width = 100
        repoProducedFat.HeaderText = "Produced FAT%"
        repoProducedFat.DecimalPlaces = DecimalPointFatSNFPer
        repoProducedFat.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoProducedFat.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoProducedFat)

        Dim repoProducedSnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProducedSnf.FormatString = ""
        repoProducedSnf.Name = colProduceSNF
        repoProducedSnf.Width = 150
        repoProducedSnf.HeaderText = "Produced SNF%"
        repoProducedSnf.DecimalPlaces = DecimalPointFatSNFPer
        repoProducedSnf.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoProducedSnf.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoProducedSnf)




        Dim repoProduced_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_FAT_KG.FormatString = ""
        repoProduced_FAT_KG.Name = colProduceFATKG
        repoProduced_FAT_KG.Width = 150
        repoProduced_FAT_KG.HeaderText = "Produced FAT KG"
        repoProduced_FAT_KG.DecimalPlaces = DecimalPointQty
        repoProduced_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoProduced_FAT_KG.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoProduced_FAT_KG)

        Dim repoProduced_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProduced_SNF_KG.FormatString = ""
        repoProduced_SNF_KG.Name = colProduceSNFKG
        repoProduced_SNF_KG.Width = 150
        repoProduced_SNF_KG.HeaderText = "Produced SNF KG"
        repoProduced_SNF_KG.DecimalPlaces = DecimalPointQty
        repoProduced_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoProduced_SNF_KG.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoProduced_SNF_KG)

        Dim repoSTD_Location_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSTD_Location_Code.FormatString = ""
        repoSTD_Location_Code.Name = colProduceStdInLocation
        repoSTD_Location_Code.Width = 100
        repoSTD_Location_Code.HeaderText = "Location Code"
        repoSTD_Location_Code.ReadOnly = False
        gvProduce.MasterTemplate.Columns.Add(repoSTD_Location_Code)

        Dim repoSTD_Location_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSTD_Location_Desc.FormatString = ""
        repoSTD_Location_Desc.Name = colProduceStdInLocationName
        repoSTD_Location_Desc.Width = 150
        repoSTD_Location_Desc.HeaderText = "Location Description"
        repoSTD_Location_Desc.ReadOnly = True
        gvProduce.MasterTemplate.Columns.Add(repoSTD_Location_Desc)
        '-------------------------------------------------

        gvProduce.AllowDeleteRow = True
        gvProduce.AllowAddNewRow = False
        gvProduce.ShowGroupPanel = False
        gvProduce.AllowColumnReorder = False
        gvProduce.AllowRowReorder = False
        gvProduce.EnableSorting = False
        gvProduce.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvProduce.MasterTemplate.ShowRowHeaderColumn = False
        gvProduce.EnableFiltering = False
        gvProduce.Rows.AddNew()
    End Sub

    Private Sub LoadBlankGridIssue()
        gvIssue.Rows.Clear()
        gvIssue.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colIssueSNo
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
        repoicode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
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



        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colIssueItemProductType
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoPtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colIssueUOM
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repouom)



        Dim repoAvail_Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_Qty.FormatString = ""
        repoAvail_Qty.Name = colIssueQty
        repoAvail_Qty.Width = 120
        repoAvail_Qty.HeaderText = "Issued Quantity"
        repoAvail_Qty.DecimalPlaces = DecimalPointQty
        repoAvail_Qty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_Qty.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_Qty)

        Dim repoAvail_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colIssueFAT
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Issued FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        Dim repoAvail_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_FAT_KG.FormatString = ""
        repoAvail_FAT_KG.Name = colIssueFATKG
        repoAvail_FAT_KG.Width = 120
        repoAvail_FAT_KG.HeaderText = "Issued FAT KG"
        repoAvail_FAT_KG.DecimalPlaces = DecimalPointQty
        repoAvail_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_FAT_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_KG)

        Dim repoAvail_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_Per.FormatString = ""
        repoAvail_SNF_Per.Name = colIssueSNF
        repoAvail_SNF_Per.Width = 120
        repoAvail_SNF_Per.HeaderText = "Issued SNF%"
        repoAvail_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_SNF_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_Per)

        Dim repoAvail_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvail_SNF_KG.FormatString = ""
        repoAvail_SNF_KG.Name = colIssueSNFKG
        repoAvail_SNF_KG.Width = 120
        repoAvail_SNF_KG.HeaderText = "Issued SNF KG"
        repoAvail_SNF_KG.DecimalPlaces = DecimalPointQty
        repoAvail_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoAvail_SNF_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_SNF_KG)

        repoAvail_FAT_Per = New GridViewDecimalColumn()
        repoAvail_FAT_Per.Name = colIssueReqFAT
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Required FAT%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)

        repoAvail_FAT_Per = New GridViewDecimalColumn()
        repoAvail_FAT_Per.FormatString = ""
        repoAvail_FAT_Per.Name = colIssueReqSNF
        repoAvail_FAT_Per.Width = 120
        repoAvail_FAT_Per.HeaderText = "Required SNF%"
        repoAvail_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoAvail_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoAvail_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoAvail_FAT_Per)



        Dim repoDiff_FAT_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_FAT_Per.FormatString = ""
        repoDiff_FAT_Per.Name = colIssueDiffFAT
        repoDiff_FAT_Per.Width = 120
        repoDiff_FAT_Per.HeaderText = "Diff. FAT%"
        repoDiff_FAT_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoDiff_FAT_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoDiff_FAT_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_Per)

        Dim repoDiff_FAT_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_FAT_KG.FormatString = ""
        repoDiff_FAT_KG.Name = colIssueDiffFATKG
        repoDiff_FAT_KG.Width = 120
        repoDiff_FAT_KG.HeaderText = "Diff. FAT KG"
        repoDiff_FAT_KG.DecimalPlaces = DecimalPointQty
        repoDiff_FAT_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoDiff_FAT_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_FAT_KG)

        Dim repoDiff_SNF_Per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_SNF_Per.FormatString = ""
        repoDiff_SNF_Per.Name = colIssueDiffSNF
        repoDiff_SNF_Per.Width = 120
        repoDiff_SNF_Per.HeaderText = "Diff. SNF%"
        repoDiff_SNF_Per.DecimalPlaces = DecimalPointFatSNFPer
        repoDiff_SNF_Per.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoDiff_SNF_Per.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_Per)

        Dim repoDiff_SNF_KG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiff_SNF_KG.FormatString = ""
        repoDiff_SNF_KG.Name = colIssueDiffSNFKG
        repoDiff_SNF_KG.Width = 120
        repoDiff_SNF_KG.HeaderText = "Diff. SNF KG"
        repoDiff_SNF_KG.DecimalPlaces = DecimalPointQty
        repoDiff_SNF_KG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoDiff_SNF_KG.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoDiff_SNF_KG)

        Dim repoIssueRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIssueRemarks.FormatString = ""
        repoIssueRemarks.Name = colIssueRemark
        repoIssueRemarks.Width = 120
        repoIssueRemarks.MaxLength = 200
        repoIssueRemarks.HeaderText = "Remarks"
        gvIssue.MasterTemplate.Columns.Add(repoIssueRemarks)

        Dim repoTo_Loc_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTo_Loc_Code.FormatString = ""
        repoTo_Loc_Code.Name = colIssueLocationCode
        repoTo_Loc_Code.Width = 120
        repoTo_Loc_Code.MaxLength = 200
        repoTo_Loc_Code.HeaderText = "Location Code"
        repoTo_Loc_Code.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoTo_Loc_Code)

        Dim repoTo_Loc_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTo_Loc_Desc.FormatString = ""
        repoTo_Loc_Desc.Name = colIssueLocation
        repoTo_Loc_Desc.Width = 120
        repoTo_Loc_Desc.MaxLength = 200
        repoTo_Loc_Desc.HeaderText = "Location Desc"
        repoTo_Loc_Desc.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoTo_Loc_Desc)





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
    Private Sub LoadBlankGridAR()
        gvAddRemove.Rows.Clear()
        gvAddRemove.Columns.Clear()

        Dim repoARSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSno.FormatString = ""
        repoARSno.Name = colARSno
        repoARSno.Width = 60
        repoARSno.DecimalPlaces = 0
        repoARSno.HeaderText = "S.No."
        repoARSno.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoARSno)

        Dim ARType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        ARType.FormatString = ""
        ARType.Name = colARType
        ARType.Width = 120
        ARType.HeaderText = "Type(Add/Remove)"
        ARType.ReadOnly = False
        ARType.DataSource = GetARType()
        ARType.ValueMember = "Code"
        ARType.DisplayMember = "Name"
        gvAddRemove.MasterTemplate.Columns.Add(ARType)

        Dim repoLoaction_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoaction_Code.FormatString = ""
        repoLoaction_Code.Name = colARLocCode
        repoLoaction_Code.Width = 120
        repoLoaction_Code.HeaderText = "Location Code"
        repoLoaction_Code.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoLoaction_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLoaction_Code.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(repoLoaction_Code)

        Dim repoLoaction_Desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoaction_Desc.FormatString = ""
        repoLoaction_Desc.Name = colARLocDesc
        repoLoaction_Desc.Width = 120
        repoLoaction_Desc.HeaderText = "Location Description"
        repoLoaction_Desc.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoLoaction_Desc)

        Dim ARItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ARItemCode.FormatString = ""
        ARItemCode.Name = colARItemCode
        ARItemCode.Width = 100
        ARItemCode.HeaderText = "Item Code"
        ARItemCode.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(ARItemCode)

        Dim repoARItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoARItemName.FormatString = ""
        repoARItemName.Name = colARItemName
        repoARItemName.Width = 120
        repoARItemName.HeaderText = "Item Description"
        repoARItemName.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoARItemName)

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colARItemType
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoitype)

        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colARItemProductType
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoPtype)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Batch Item"
        repoIsSerItem.Name = colARIsBatchItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvAddRemove.MasterTemplate.Columns.Add(repoIsSerItem)


        Dim repoARAvailQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARAvailQty.FormatString = ""
        repoARAvailQty.Name = colARAvailQty
        repoARAvailQty.Width = 120
        repoARAvailQty.HeaderText = "Available Quantity"
        repoARAvailQty.DecimalPlaces = DecimalPointQty
        repoARAvailQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARAvailQty.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repoARAvailQty)

        Dim repoARQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARQty.FormatString = ""
        repoARQty.Name = colARQty
        repoARQty.Width = 120
        repoARQty.HeaderText = "Add/Remove Qty"
        repoARQty.DecimalPlaces = DecimalPointQty
        repoARQty.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARQty.Minimum = 0
        repoARQty.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(repoARQty)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = colARUom
        repouom.Width = 100
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(repouom)

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colARUOMDesc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM Description"
        repouomname.ReadOnly = True
        gvAddRemove.MasterTemplate.Columns.Add(repouomname)

        Dim repoARFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARFatPer.FormatString = ""
        repoARFatPer.Name = colAR_FAT_Per
        repoARFatPer.Width = 100
        repoARFatPer.HeaderText = "Fat %"
        repoARFatPer.DecimalPlaces = DecimalPointFatSNFPer
        repoARFatPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoARFatPer.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(repoARFatPer)

        Dim repoARSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSNFPer.FormatString = ""
        repoARSNFPer.Name = colAR_SNF_Per
        repoARSNFPer.Width = 100
        repoARSNFPer.HeaderText = "SNF %"
        repoARSNFPer.DecimalPlaces = DecimalPointFatSNFPer
        repoARSNFPer.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointFatSNFPer) & "}"
        repoARSNFPer.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(repoARSNFPer)

        Dim repoARFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARFatKG.FormatString = ""
        repoARFatKG.Name = colAR_FAT_KG
        repoARFatKG.Width = 100
        repoARFatKG.HeaderText = "FAT KG"
        repoARFatKG.DecimalPlaces = DecimalPointQty
        repoARFatKG.Minimum = 0
        repoARFatKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARFatKG.ReadOnly = If(AutoCalcQtyAddRem = True, False, True)
        gvAddRemove.MasterTemplate.Columns.Add(repoARFatKG)

        Dim repoARSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoARSNFKG.FormatString = ""
        repoARSNFKG.Name = colAR_SNF_KG
        repoARSNFKG.Width = 100
        repoARSNFKG.HeaderText = "SNF KG"
        repoARSNFKG.DecimalPlaces = DecimalPointQty
        repoARSNFKG.Minimum = 0
        repoARSNFKG.FormatString = "{0:n" & clsCommon.myCstr(DecimalPointQty) & "}"
        repoARSNFKG.ReadOnly = If(AutoCalcQtyAddRem = True, False, True)
        gvAddRemove.MasterTemplate.Columns.Add(repoARSNFKG)


        Dim ARRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ARRemarks.FormatString = ""
        ARRemarks.Name = colARRemarks
        ARRemarks.Width = 120
        ARRemarks.HeaderText = "Remarks"
        ARRemarks.ReadOnly = False
        gvAddRemove.MasterTemplate.Columns.Add(ARRemarks)

        gvAddRemove.AllowDeleteRow = True
        gvAddRemove.AllowAddNewRow = False
        gvAddRemove.ShowGroupPanel = False
        gvAddRemove.AllowColumnReorder = False
        gvAddRemove.AllowRowReorder = False
        gvAddRemove.EnableSorting = False
        gvAddRemove.EnableFiltering = False
        gvAddRemove.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAddRemove.MasterTemplate.ShowRowHeaderColumn = False
        gvAddRemove.Rows.AddNew()
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

        lblTotProduceQty.Text = ""
        lblTotProduceFATKG.Text = ""
        lblTotProduceSNFKG.Text = ""
        lblTotIssueQty.Text = ""
        lblTotIssueFATKG.Text = ""
        lblTotIssueSNFKG.Text = ""

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

        LoadBlankGridProduce()
        LoadBlankGridIssue()
        LoadBlankGridAR()

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        'fndChildBatchNo.Enabled = True
        'fndChildBatchNo.Enabled = True

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
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            'ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
            '                         "TSPL_PP_STANDARDIZATION_HEAD " + Environment.NewLine +
            '                         "TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_STD_ISSUE_ITEM_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_STD_QC_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_STD_STAGE_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_STD_QC_LOG_SHEET " + Environment.NewLine +
            '                         "Press Alt+P for Post Trasnaction " + Environment.NewLine +
            '                         "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " + Environment.NewLine +
            '                         "TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL " + Environment.NewLine +
            '                         "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
            '                         "TSPL_SERIAL_ITEM " + Environment.NewLine +
            '                         "TSPL_BATCH_ITEM " + Environment.NewLine +
            '                         "TSPL_INVENTORY_MOVEMENT_new " + Environment.NewLine +
            '                         "TSPL_JOURNAL_MASTER ")
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

        If e.KeyData = Keys.F2 AndAlso gvAddRemove.CurrentColumn IsNot Nothing AndAlso gvAddRemove.CurrentColumn Is gvAddRemove.Columns(colARUom) Then
            isCellValueChanged = True
            OpenUOM(True)
            isCellValueChanged = False
        End If
        If e.KeyData = Keys.F2 AndAlso gvAddRemove.CurrentColumn IsNot Nothing AndAlso gvAddRemove.CurrentColumn Is gvAddRemove.Columns(colARItemCode) Then
            isCellValueChanged = True
            gvAddRemove.CurrentRow.Cells(colARItemCode).Value = clsItemMaster.getFinder("", gvAddRemove.CurrentRow.Cells(colARItemCode).Value, True)
            Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, NavigatorType.Current)
            If Not objItem Is Nothing Then
                gvAddRemove.CurrentRow.Cells(colARItemName).Value = objItem.Item_Desc
                gvAddRemove.CurrentRow.Cells(colARItemType).Value = objItem.Item_Type
                gvAddRemove.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                gvAddRemove.CurrentRow.Cells(colARUom).Value = objItem.Unit_Code
                gvAddRemove.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
            End If
            isCellValueChanged = False
        End If

        If e.KeyData = Keys.F2 AndAlso gvAddRemove.CurrentColumn IsNot Nothing AndAlso gvAddRemove.CurrentColumn Is gvAddRemove.Columns(colARLocCode) Then
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
        End If
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
                Dim strItemType = clsDBFuncationality.getSingleValue("select item_type from TSPL_ITEM_MASTER  where item_code='" & clsCommon.myCstr(gvProduce.Rows(0).Cells(colProduceItemCode).Value) & "' ")
                If clsCommon.CompairString(strItemType, "S") <> CompairStringResult.Equal Then
                    Throw New Exception("Batch Order item should be only Semi FG Type.")
                End If
            End If
            ' '' validation for standardization batch location
            'For Each growBatch As GridViewRowInfo In gv.Rows
            '    If clsCommon.myLen(growBatch.Cells(colProduceStdInLocation).Value) <= 0 Then
            '        Throw New Exception("Select Location Code in Batch Detail tab at line no- " & (growBatch.Index + 1) & ".")
            '    End If
            'Next

            'Dim strBatchORderExistIntPIE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Standardization_code from TSPL_PP_STANDARDIZATION_HEAD where TSPL_PP_STANDARDIZATION_HEAD.Main_Batch_Code='" + fndMainBatchNo.Value + "' and TSPL_PP_STANDARDIZATION_HEAD.Standardization_code not in ('" + txtCode.Value + "')"))
            'If clsCommon.myLen(clsCommon.myCstr(strBatchORderExistIntPIE)) > 0 Then
            '    Throw New Exception("Please select different Batch Order, Same Batch exists with Production Standardization " & strBatchORderExistIntPIE & "  ")
            'End If

            '' validation for standardization stages existence
            'If gvStage.Rows.Count <= 0 Then
            '    Throw New Exception("Standardization Stages not found for selected child batch's section and structure.")
            'End If
            ''richa BHA/05/09/18-000509
            'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.CreateJEOnProduction, clsFixedParameterCode.CreateJEOnProduction, Nothing), "0") = CompairStringResult.Equal Then
            '    For Each dr As GridViewRowInfo In gvStage.Rows
            '        If clsCommon.myLen(dr.Cells(colUnit_Code).Value) <= 0 Then
            '            Throw New Exception("No any Item found of type Milk or Milk Product in Issue Grid.")
            '        End If
            '        'total = total + 1
            '    Next
            'End If
            If IsPost = True Then
                Dim paramcode As String = ""
                Dim nature As String = ""
                Dim range1 As Decimal = Nothing
                Dim range2 As Decimal = Nothing
                Dim Actual_Range As String = ""
                Dim Actual_Value As String = ""
                Dim Actual_Status As String = ""
                Dim QC_Status As String = ""
                For Each growBatch As GridViewRowInfo In gvProduce.Rows
                    If clsCommon.myLen(growBatch.Cells(colProduceStdInLocation).Value) <= 0 Then
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
            For Each dr As GridViewRowInfo In gvAddRemove.Rows
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
                        'For ii As Integer = 0 To gv_qc.Rows.Count - 1
                        '    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCItemCode).Value), clsCommon.myCstr(dr.Cells(colARItemCode).Value)) = CompairStringResult.Equal Then
                        '        If clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = dr.Index Then
                        '            gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                        '            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                        '                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblFATPer
                        '            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                        '                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblSNFPer
                        '            End If
                        '        End If
                        '    End If
                        'Next
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
                        If Not clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colARUom).Value), clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueUOM).Value)) = CompairStringResult.Equal Then
                            Throw New Exception("Add/Remove Tab item [" + clsCommon.myCstr(dr.Cells(colARItemCode).Value + "] UOM should be [" + clsCommon.myCstr(gvIssue.Rows(ii).Cells(colIssueUOM).Value) + "] because if add/remove item is from issued item then its unit must be same as issued unit as at line no " & (dr.Index + 1) & ""))
                        End If
                    End If
                Next

                Dim availQty As Decimal = clsCommon.myCdbl(dr.Cells(colARAvailQty).Value)
                Dim reqQty As Decimal = clsCommon.myCdbl(dr.Cells(colARQty).Value)

                If RunBatchFifowise Then
                    If clsCommon.CompairString(dr.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                        gvAddRemove.CurrentRow = gvAddRemove.Rows(dr.Index)
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
                TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colIssueFATKG).Value)
                TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colIssueSNFKG).Value)
                TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colIssueItemCode).Value, grow.Cells(colIssueUOM).Value, clsCommon.myCdbl(grow.Cells(colIssueQty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colIssueQty).Value)
            End If
        Next
        For Each grow As GridViewRowInfo In gvAddRemove.Rows
            If clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colARItemProductType).Value, "MP") = CompairStringResult.Equal Then
                If clsCommon.CompairString(grow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                    TotIssueFatKg = TotIssueFatKg + clsCommon.myCdbl(grow.Cells(colAR_FAT_KG).Value)
                    TotIssueSnfKg = TotIssueSnfKg + clsCommon.myCdbl(grow.Cells(colAR_SNF_KG).Value)
                    TotIssueQty = TotIssueQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colARItemCode).Value, grow.Cells(colARItemCode).Value, clsCommon.myCdbl(grow.Cells(colARQty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colARQty).Value)
                End If
            End If
        Next

        '' for Produced/removed qty
        For Each grow As GridViewRowInfo In gvProduce.Rows
            If clsCommon.CompairString(grow.Cells(colProduceItemProductType).Value, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(grow.Cells(colProduceItemProductType).Value, "MP") = CompairStringResult.Equal Then
                TotProdFatKg = TotProdFatKg + clsCommon.myCdbl(grow.Cells(colProduceFATKG).Value)
                TotProdSnfKg = TotProdSnfKg + clsCommon.myCdbl(grow.Cells(colProduceSNFKG).Value)
                TotProdQty = TotProdQty + clsProcessProductionStandardization.GetKG_AfterConversion(grow.Cells(colProduceItemCode).Value, grow.Cells(colProduceUOM).Value, clsCommon.myCdbl(grow.Cells(colProduceQty).Value), Nothing) 'clsCommon.myCdbl(grow.Cells(colProduceQty).Value)
            End If
        Next
        For Each grow As GridViewRowInfo In gvAddRemove.Rows
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

            obj.Posted = 0
            If clsCommon.CompairString(UsLock1.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then
                obj.Posted = 1
            End If


            '' save enable/disable tab
            obj.ISSUE_TAB_ENABLED = pageIssueDetail.Enabled
            obj.AR_TAB_ENABLED = pageAddRemoveDetail.Enabled

            obj.BATCH_TAB_ENABLED = pageBatchDetail.Enabled




            obj.Tot_Produce_Qty = lblTotProduceQty.Text
            obj.Tot_Produce_FATKG = lblTotProduceFATKG.Text
            obj.Tot_Produce_SNFKG = lblTotProduceSNFKG.Text

            obj.Tot_Issue_Qty = lblTotIssueQty.Text
            obj.Tot_Issue_FATKG = lblTotIssueFATKG.Text
            obj.Tot_Issue_SNFKG = lblTotIssueSNFKG.Text


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
            For Each grow As GridViewRowInfo In gvProduce.Rows
                Dim objtr As New clsProcessProductionSTDBatchItemDetail()

                objtr.SNO = CInt(grow.Cells(colProduceSNo).Value)
                objtr.BOM_Code = clsCommon.myCstr(grow.Cells(colProduceBOMCode).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colProduceItemCode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colitemtype).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colProduceItemProductType).Value)
                objtr.Produced_FAT_KG = clsCommon.myCdbl(grow.Cells(colProduceFATKG).Value)
                objtr.Produced_Qty = clsCommon.myCdbl(grow.Cells(colProduceQty).Value)
                objtr.Produced_SNF_KG = clsCommon.myCdbl(grow.Cells(colProduceSNFKG).Value)
                objtr.Quantity = clsCommon.myCdbl(grow.Cells(colProduceReqQty).Value)
                objtr.Requir_FAT_KG = clsCommon.myCdbl(grow.Cells(colProduceReqFATKG).Value)
                objtr.Requir_FAT_per = clsCommon.myCdbl(grow.Cells(colProduceReqFAT).Value)
                objtr.Requir_SNF_KG = clsCommon.myCdbl(grow.Cells(colProduceReqSNFKG).Value)
                objtr.Requir_SNF_Per = clsCommon.myCdbl(grow.Cells(colProduceReqSNF).Value)
                objtr.Section_Code = clsCommon.myCstr(grow.Cells(colSection_Code).Value)
                objtr.Shift_Code = clsCommon.myCstr(grow.Cells(colShift_Code).Value)
                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colProduceUOM).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colUOMDesc).Value)
                objtr.STD_Loaction_Code = clsCommon.myCstr(grow.Cells(colProduceStdInLocation).Value)

                objtr.Produced_FAT_per = clsCommon.myCdbl(grow.Cells(colProduceFAT).Value)
                objtr.Produced_SNF_per = clsCommon.myCdbl(grow.Cells(colProduceSNF).Value)
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

                objtr.SNO = CInt(grow.Cells(colIssueSNo).Value)
                objtr.Comp_Code = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colIssueItemCode).Value)
                objtr.Item_Type = clsCommon.myCstr(grow.Cells(colIssueItemType).Value)
                objtr.Product_Type = clsCommon.myCstr(grow.Cells(colIssueItemProductType).Value)
                objtr.Avail_FAT_KG = clsCommon.myCdbl(grow.Cells(colIssueFATKG).Value)
                objtr.Avail_FAT_Per = clsCommon.myCdbl(grow.Cells(colIssueFAT).Value)
                objtr.Avail_Qty = clsCommon.myCdbl(grow.Cells(colIssueQty).Value)
                objtr.Avail_SNF_KG = clsCommon.myCdbl(grow.Cells(colIssueSNFKG).Value)
                objtr.Avail_SNF_Per = clsCommon.myCdbl(grow.Cells(colIssueSNF).Value)

                objtr.Requir_FAT_Per = clsCommon.myCdbl(grow.Cells(colIssueReqFAT).Value)
                objtr.Requir_SNF_Per = clsCommon.myCdbl(grow.Cells(colIssueReqSNF).Value)

                objtr.Diff_FAT_KG = clsCommon.myCdbl(grow.Cells(colIssueDiffFATKG).Value)
                objtr.Diff_FAT_Per = clsCommon.myCdbl(grow.Cells(colIssueDiffFAT).Value)
                'objtr.Diff_Qty = clsCommon.myCdbl(grow.Cells(colDiff_Qty).Value)
                objtr.Diff_SNF_KG = clsCommon.myCdbl(grow.Cells(colIssueDiffSNFKG).Value)
                objtr.Diff_SNF_Per = clsCommon.myCdbl(grow.Cells(colIssueDiffSNF).Value)

                objtr.Remarks = clsCommon.myCstr(grow.Cells(colIssueRemark).Value)
                objtr.Standardization_Code = clsCommon.myCstr(txtCode.Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colIssueUOM).Value)
                objtr.Unit_Desc = clsCommon.myCstr(grow.Cells(colIssueUOMDesc).Value)

                objtr.TO_LOC_CODE = clsCommon.myCstr(grow.Cells(colIssueLocationCode).Value)
                objtr.TO_LOC_DESC = clsCommon.myCstr(grow.Cells(colIssueLocation).Value)

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
            For Each grow As GridViewRowInfo In gvAddRemove.Rows
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

    Private Sub txtfrmsub__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
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
                    Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Line_No as [Line No],TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name]" &
                    " from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode " &
                    " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode " &
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


                lblTotProduceQty.Text = clsCommon.myFormat(obj.Tot_Produce_Qty)
                lblTotProduceFATKG.Text = clsCommon.myFormat(obj.Tot_Produce_FATKG)
                lblTotProduceSNFKG.Text = clsCommon.myFormat(obj.Tot_Produce_SNFKG)

                lblTotIssueQty.Text = clsCommon.myFormat(obj.Tot_Issue_Qty)
                lblTotIssueFATKG.Text = clsCommon.myFormat(obj.Tot_Issue_FATKG)
                lblTotIssueSNFKG.Text = clsCommon.myFormat(obj.Tot_Issue_SNFKG)


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




                pageIssueDetail.Enabled = obj.ISSUE_TAB_ENABLED
                pageAddRemoveDetail.Enabled = obj.AR_TAB_ENABLED

                pageBatchDetail.Enabled = obj.BATCH_TAB_ENABLED

                pageAttachment.Enabled = obj.ATTACH_TAB_ENABLED
                LoadBlankGridAR()
                gvProduce.Rows.Clear()

                gvIssue.Rows.Clear()
                gvAddRemove.Rows.Clear()


                Dim arr_BatchIcode As New List(Of String)
                Dim arr_ARIcode As New List(Of String)
                arr_BatchIcode = New List(Of String)
                arr_ARIcode = New List(Of String)

                '' load batch item grid
                If obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0 Then
                    For Each objtr As clsProcessProductionSTDBatchItemDetail In obj.ArrBatchItem
                        gvProduce.Rows.AddNew()
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceSNo).Value = objtr.SNO
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceBOMCode).Value = objtr.BOM_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceBOM).Value = objtr.BOM_Desc
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemCode).Value = objtr.Item_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItem).Value = objtr.Item_Desc
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colitemtype).Value = objtr.Item_Type
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemProductType).Value = IIf(clsCommon.myLen(objtr.Product_Type) <= 0, "Others", objtr.Product_Type)
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceUOM).Value = objtr.Unit_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colUOMDesc).Value = objtr.Unit_Desc
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceQty).Value = objtr.Produced_Qty
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqFATKG).Value = objtr.Requir_FAT_KG
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqFAT).Value = objtr.Requir_FAT_per
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqSNFKG).Value = objtr.Requir_SNF_KG
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqSNF).Value = objtr.Requir_SNF_Per
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqQty).Value = objtr.Quantity

                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceFAT).Value = objtr.Produced_FAT_per
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceSNF).Value = objtr.Produced_SNF_per

                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceFATKG).Value = objtr.Produced_FAT_KG
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceQty).Value = objtr.Produced_Qty
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceSNFKG).Value = objtr.Produced_SNF_KG

                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colSection_Code).Value = objtr.Section_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colSection_Desc).Value = objtr.Section_Desc

                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colShift_Code).Value = objtr.Shift_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colShift_Desc).Value = objtr.Shift_Desc

                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceStdInLocation).Value = objtr.STD_Loaction_Code
                        gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceStdInLocationName).Value = objtr.STD_Loaction_Desc

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
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNo).Value = objtr.SNO

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = objtr.Item_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = objtr.Item_Desc
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = objtr.Item_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(objtr.Product_Type) 'objtr.Product_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = objtr.Product_Type
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOM).Value = objtr.Unit_Code
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = objtr.Unit_Desc

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueFATKG).Value = objtr.Avail_FAT_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueFAT).Value = objtr.Avail_FAT_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value = objtr.Avail_Qty
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNFKG).Value = objtr.Avail_SNF_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNF).Value = objtr.Avail_SNF_Per

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqFAT).Value = objtr.Requir_FAT_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqSNF).Value = objtr.Requir_SNF_Per

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffFATKG).Value = objtr.Diff_FAT_KG

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffFAT).Value = objtr.Diff_FAT_Per
                        'gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colDiff_Qty).Value = objtr.Diff_Qty
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffSNFKG).Value = objtr.Diff_SNF_KG
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffSNF).Value = objtr.Diff_SNF_Per
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueRemark).Value = objtr.Remarks

                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueLocationCode).Value = objtr.TO_LOC_CODE
                        gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueLocation).Value = objtr.TO_LOC_DESC

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
                        gvAddRemove.Rows.AddNew()
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARSno).Value = objtr.SNO

                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemCode).Value = objtr.Item_Code
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemName).Value = objtr.Item_Desc
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemType).Value = objtr.Item_Type
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemProductType).Value = objtr.Product_Type
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARUom).Value = objtr.Unit_Code
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARUOMDesc).Value = objtr.Unit_Desc

                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARQty).Value = objtr.ADD_REMOVE_QTY
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARType).Value = objtr.ADD_REMOVE_TYPE
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARLocCode).Value = objtr.Loaction_Code
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARLocDesc).Value = objtr.Location_Desc
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARRemarks).Value = objtr.Remarks
                        'gvARDetail.Rows(gvARDetail.Rows.Count - 1).Cells(colARAvailQty).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvARDetail.CurrentRow.Cells(colARItemCode).Value, gvARDetail.CurrentRow.Cells(colARLocCode).Value, Me.txtCode.Value, dtpDate.Value, Nothing, gvARDetail.CurrentRow.Cells(colARUom).Value)
                        If clsCommon.CompairString(gvAddRemove.CurrentRow.Cells(colARItemProductType).Value, "MI") = CompairStringResult.Equal Then
                            gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItemNew
                        Else
                            gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemCode).Tag = objtr.arrBatchItem
                        End If
                        Dim loc_type As Integer = 0
                        qry = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + clsCommon.myCstr(gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARLocCode).Value) + "'"
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "MAIN") = CompairStringResult.Equal Then
                            loc_type = 2
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SUB") = CompairStringResult.Equal Then
                            loc_type = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SEC") = CompairStringResult.Equal Then
                            loc_type = 0
                        End If
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARAvailQty).Value = "0"

                        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARUom).Value), loc_type, IIf(clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARAvailQty).Value = clsCommon.myCdbl(dt.Rows(0)("qty"))
                        End If


                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objtr.Product_Type) <= 0, "Others", objtr.Product_Type)

                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colAR_FAT_Per).Value = objtr.AR_FAT_Per
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colAR_FAT_KG).Value = objtr.AR_FAT_KG
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colAR_SNF_Per).Value = objtr.AR_SNF_Per
                        gvAddRemove.Rows(gvAddRemove.Rows.Count - 1).Cells(colAR_SNF_KG).Value = objtr.AR_SNF_KG

                        If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso Not arr_ARIcode.Contains(objtr.Item_Code) Then
                            arr_ARIcode.Add(objtr.Item_Code)
                        End If
                    Next
                End If
                gvAddRemove.Rows.AddNew()
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
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvProduce.CellValueChanged
        If Not isInsideLoadData Then
            If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Child Batch Order Detail", Me.Text)
                fndChildBatchNo.Select()
                fndChildBatchNo.Focus()
                Return
            End If
            If Not isCellValueChanged Then
                If (e.Column Is gvProduce.Columns(colProduceQty)) Then '' Or (e.Column Is gv.Columns(colProduceFAT))
                    isCellValueChanged = True
                    calculateProduceFATSNFKG(gvProduce.CurrentRow.Index)
                    'gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colProduceQty).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
                    'gv.CurrentRow.Cells(colProduceFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.CurrentRow.Cells(colProduceItemCode).Value, gv.CurrentRow.Cells(colProduceUOM).Value, clsCommon.myCdbl(gv.CurrentRow.Cells(colProduceQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colProduceFAT).Value), Nothing)
                    'gv.CurrentRow.Cells(colProduceSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.CurrentRow.Cells(colProduceItemCode).Value, gv.CurrentRow.Cells(colProduceUOM).Value, clsCommon.myCdbl(gv.CurrentRow.Cells(colProduceQty).Value), clsCommon.myCdbl(gv.CurrentRow.Cells(colProduceSNF).Value), Nothing)
                    'Cal_FAT()
                    'Cal_SNF()
                    isCellValueChanged = False
                End If
                'If (e.Column Is gv.Columns(colDAMAGE_Qty)) Then
                '    isCellValueChanged = True
                '    gv.CurrentRow.Cells(colFINAL_PROD_Qty).Value = gv.CurrentRow.Cells(colProduceQty).Value - gv.CurrentRow.Cells(colDAMAGE_Qty).Value
                '    isCellValueChanged = False
                'End If
                If e.Column Is gvProduce.Columns(colProduceStdInLocation) Then
                    isCellValueChanged = True
                    OpenGVLocation(False)
                    isCellValueChanged = False
                End If
                'If (e.Column Is gv.Columns(colProduceQty)) Or (e.Column Is gv.Columns(colProduceSNF)) Then
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
            Dim qry As String = clsProcessProductionStandardization.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvProduce.CurrentRow.Cells(colProduceItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvProduce.CurrentRow.Cells(colProduceUOM).Value), True)

            gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value = clsCommon.ShowSelectForm("LOCSUBFND", qry, "Code", " [Type] in ('Section','Sub Location') and [Stock Qty]>=0 and is_Consumption_Location=0 ", clsCommon.myCstr(gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value), "Code", isButtonClicked) 'clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "')", gv.CurrentRow.Cells(colProduceStdInLocation).Value, False) '
        Else
            gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value = clsLocation.getFinder("((Is_Section='Y' or Is_Sub_Location='Y') and  is_Consumption_Location=0 and Main_Location_Code='" & txtlocation.Text & "')", gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value, False)
        End If

        gvProduce.CurrentRow.Cells(colProduceStdInLocationName).Value = clsLocation.GetName(gvProduce.CurrentRow.Cells(colProduceStdInLocation).Value, Nothing)
        If EnableParameterTab() Then
            pageParameterDetail.Enabled = True
        End If
    End Sub

    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvProduce.CurrentRow.Cells(colProduceQty).Value)
            fat = clsCommon.myCdbl(gvProduce.CurrentRow.Cells(colProduceFAT).Value)

            If qty >= 0 AndAlso fat >= 0 Then
                fat_kg = (qty * fat) / 100
                gvProduce.CurrentRow.Cells(colProduceFATKG).Value = fat_kg
            Else
                gvProduce.CurrentRow.Cells(colProduceFATKG).Value = 0
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

            qty = clsCommon.myCdbl(gvProduce.CurrentRow.Cells(colProduceQty).Value)
            snf = clsCommon.myCdbl(gvProduce.CurrentRow.Cells(colProduceSNF).Value)

            If qty >= 0 AndAlso snf_kg >= 0 Then
                snf_kg = (qty * snf) / 100
                gvProduce.CurrentRow.Cells(colProduceSNFKG).Value = snf_kg
            Else
                gvProduce.CurrentRow.Cells(colProduceSNFKG).Value = 0
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenLocation(ByVal isButtonClicked As Boolean)
        gvProduce.CurrentRow.Cells(colProduceBOMCode).Value = clsCommon.myCstr(clsLocation.getFinder(" isnull(Is_Sub_Location,'N')='Y' and location_code in (" + arrLoc + ")", clsCommon.myCstr(gvProduce.CurrentRow.Cells(colProduceBOMCode).Value), isButtonClicked))

        'If clsCommon.myLen(gv.CurrentRow.Cells(colProduceBOMCode).Value) > 0 Then
        '    gv.CurrentRow.Cells(colProduceBOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colProduceBOMCode).Value) + "'"))
        '    FillAvail_Stock(CInt(gv.CurrentCell.RowIndex), clsCommon.myCstr(gv.CurrentRow.Cells(colProduceItemCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colProduceBOMCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colProduceItemProductType).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colProduceUOM).Value))
        'Else
        '    gv.CurrentRow.Cells(colProduceBOMCode).Value = ""
        '    gv.CurrentRow.Cells(colProduceBOM).Value = ""
        '    gv.CurrentRow.Cells(colProduceQty).Value = Nothing
        '    gv.CurrentRow.Cells(colProduceReqFATKG).Value = Nothing
        '    gv.CurrentRow.Cells(colProduceFAT).Value = Nothing
        '    gv.CurrentRow.Cells(colProduceReqSNFKG).Value = Nothing
        '    gv.CurrentRow.Cells(colProduceSNF).Value = Nothing
        'End If
    End Sub
    Dim intCellFormaLastIndex As Integer = -1
    Private Sub gv_qc_CellFormatting(sender As Object, e As CellFormattingEventArgs)
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

    Private Sub gv_qc_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
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

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                TotalRec = TotalRec + dr.Cells(colIssueQty).Value

                Unit_Code = dr.Cells(colIssueUOM).Value
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
        Me.gvProduce.Rows.Clear()
        Dim objBO As clsProcessBatchOrder = clsProcessBatchOrder.GetData(fndChildBatchNo.Value, arrLoc, NavigatorType.Current)

        isInsideLoadData = True
        If Not objBO Is Nothing Then
            For Each dr As clsProcessBatchOrderMainDetail In objBO.ArrMainItem
                Me.gvProduce.Rows.AddNew()
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceSNo).Value = dr.SNO
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceBOMCode).Value = dr.bomcode
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceBOM).Value = dr.bomcode
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemCode).Value = dr.icode
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItem).Value = dr.iname
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colitemtype).Value = dr.itype
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemProductType).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select coalesce(Product_Type,'') as Product_Type from TSPL_ITEM_MASTER where item_code='" & dr.icode & "'"))
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceUOM).Value = dr.UOM
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colUOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" & dr.UOM & "' "))
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqQty).Value = dr.qty
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colShift_Code).Value = dr.shiftcode
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colShift_Desc).Value = dr.shiftname
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colSection_Code).Value = dr.sectioncode
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colSection_Desc).Value = dr.sectionname

                Dim dtQC As DataTable = clsProcessProductionStandardization.GetItemQCParameter(dr.icode)
                Dim drQC() As DataRow = dtQC.Select("type='FAT'")
                If drQC.Length > 0 Then
                    gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqFAT).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range"))
                    'gv.Rows(gv.Rows.Count - 1).Cells(colProduceReqFATKG).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range")) * dr.qty / 100
                End If

                drQC = dtQC.Select("type='SNF'")
                If drQC.Length > 0 Then
                    gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqSNF).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range"))
                    'gv.Rows(gv.Rows.Count - 1).Cells(colProduceReqSNFKG).Value = clsCommon.myCdbl(drQC(0).Item("Actual_Range")) * dr.qty / 100
                End If

                '' set required fat/snf
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemCode).Value, gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceUOM).Value, clsCommon.myCdbl(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqQty).Value), clsCommon.myCdbl(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqFAT).Value), Nothing)
                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceItemCode).Value, gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceUOM).Value, clsCommon.myCdbl(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqQty).Value), clsCommon.myCdbl(gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceReqSNF).Value), Nothing)


                gvProduce.Rows(gvProduce.Rows.Count - 1).Cells(colProduceQty).Value = dr.qty
                '' set produced fat/snf
                calculateProduceFATSNFKG(gvProduce.Rows.Count - 1)
            Next
        End If
        calculateALL()
        isInsideLoadData = False
    End Sub

    Sub calculateProduceFATSNFKG(ByVal intRowNo As Integer)
        gvProduce.Rows(intRowNo).Cells(colProduceFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvProduce.Rows(intRowNo).Cells(colProduceItemCode).Value, gvProduce.Rows(intRowNo).Cells(colProduceUOM).Value, clsCommon.myCdbl(gvProduce.Rows(intRowNo).Cells(colProduceQty).Value), clsCommon.myCdbl(gvProduce.Rows(intRowNo).Cells(colProduceFAT).Value), Nothing)
        gvProduce.Rows(intRowNo).Cells(colProduceSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvProduce.Rows(intRowNo).Cells(colProduceItemCode).Value, gvProduce.Rows(intRowNo).Cells(colProduceUOM).Value, clsCommon.myCdbl(gvProduce.Rows(intRowNo).Cells(colProduceQty).Value), clsCommon.myCdbl(gvProduce.Rows(intRowNo).Cells(colProduceSNF).Value), Nothing)
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
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNo).Value = gvIssue.Rows.Count
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemCode).Value = dr.Item("Item_Code")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemName).Value = dr.Item("Item_Desc")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemType).Value = ItemType(dr.Item("Item_Type"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Value = ProductType(dr.Item("Product_Type"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag = dr.Item("Product_Type")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOM).Value = dr.Item("Unit_Code")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueUOMDesc).Value = dr.Item("Unit_Desc")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value = dr.Item("Issue_Qty")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueFATKG).Value = dr.Item("Issued_FAT_KG")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueFAT).Value = dr.Item("Issued_FAT_Pers")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNFKG).Value = dr.Item("Issued_SNF_KG")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNF).Value = dr.Item("Issued_SNF_Pers")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueLocationCode).Value = clsCommon.myCstr(dr.Item("To_Location_Code"))

                '' producton costing columns
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Rate).Value = dr.Item("Issued_FAT_Rate")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Rate).Value = dr.Item("Issued_SNF_Rate")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_Fat_Amt).Value = dr.Item("Issued_FAT_Amt")
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssue_SNF_Amt).Value = dr.Item("Issued_SNF_Amt")

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueLocation).Value = clsLocation.GetName(clsCommon.myCstr(dr.Item("To_Location_Code")), Nothing)
                If clsCommon.CompairString(gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueItemProductType).Tag, "MP") = CompairStringResult.Equal Then
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqFAT).Value = gvProduce.Rows(0).Cells(colProduceReqFAT).Value
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqSNF).Value = gvProduce.Rows(0).Cells(colProduceReqSNF).Value
                Else
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqFAT).Value = 0
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueReqSNF).Value = 0
                End If

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffFAT).Value = gvProduce.Rows(0).Cells(colProduceReqFAT).Value - gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueFAT).Value
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffFATKG).Value = gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffFAT).Value * gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value / 100

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffSNF).Value = gvProduce.Rows(0).Cells(colProduceReqSNF).Value - gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueSNF).Value
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffSNFKG).Value = gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueDiffSNF).Value * gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value / 100

                totalIssued = totalIssued + gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value
            End If
        Next

        For intloop As Integer = 0 To gvProduce.Rows.Count - 1
            calculateProduceFATSNFKG(intloop)
            'gv.Rows(intloop).Cells(colProduceFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intloop).Cells(colProduceItemCode).Value, gv.Rows(intloop).Cells(colProduceUOM).Value, clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduceQty).Value), clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduceFAT).Value), Nothing)
            'gv.Rows(intloop).Cells(colProduceSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gv.Rows(intloop).Cells(colProduceItemCode).Value, gv.Rows(intloop).Cells(colProduceUOM).Value, clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduceQty).Value), clsCommon.myCdbl(gv.Rows(intloop).Cells(colProduceSNF).Value), Nothing)
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

    Private Sub gvARDetail_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAddRemove.CellFormatting
        Try
            If e.Column Is gvAddRemove.Columns(colARQty) Then
                If clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value) = 0 AndAlso clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value) = 0 Then
                    gvAddRemove.Columns(colARQty).ReadOnly = False
                Else
                    gvAddRemove.Columns(colARQty).ReadOnly = AutoCalcQtyAddRem
                End If
            ElseIf e.Column Is gvAddRemove.Columns(colAR_FAT_Per) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                    gvAddRemove.Columns(colAR_FAT_Per).ReadOnly = True
                Else
                    gvAddRemove.Columns(colAR_FAT_Per).ReadOnly = False
                End If
            ElseIf e.Column Is gvAddRemove.Columns(colAR_SNF_Per) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                    gvAddRemove.Columns(colAR_SNF_Per).ReadOnly = True
                Else
                    gvAddRemove.Columns(colAR_SNF_Per).ReadOnly = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvARDetail_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvAddRemove.CurrentColumnChanged
        If gvAddRemove.RowCount > 0 Then
            Dim intCurrRow As Integer = gvAddRemove.CurrentRow.Index
            gvAddRemove.CurrentRow.Cells(colARSno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvAddRemove.Rows.Count - 1 Then
                gvAddRemove.Rows.AddNew()
                gvAddRemove.CurrentRow = gvAddRemove.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvARDetail_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAddRemove.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gvAddRemove.Columns(colARItemCode) Then
                    If clsCommon.myLen(fndChildBatchNo.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Select Child Batch Order Detail", Me.Text)
                        fndChildBatchNo.Select()
                        fndChildBatchNo.Focus()
                        Return
                    End If

                    isCellValueChanged = True
                    '' filter for item_used_asapplied by Panch Raj on 10-07-2019 against ticket no:ERO/12/06/18-000342
                    gvAddRemove.CurrentRow.Cells(colARItemCode).Value = clsItemMaster.getFinder(If(ShowOnlyProdItemsOnAddRemove = True, " Item_Used_as='P' ", ""), gvAddRemove.CurrentRow.Cells(colARItemCode).Value, False)
                    Dim objItem As clsItemMaster = clsItemMaster.GetDataRMOther(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, NavigatorType.Current)
                    If Not objItem Is Nothing Then
                        RemoveQCGrid() ''BHA/10/08/18-000411 by balwinder on 17/08/2018
                        gvAddRemove.CurrentRow.Cells(colARItemName).Value = objItem.Item_Desc
                        gvAddRemove.CurrentRow.Cells(colARItemType).Value = objItem.Item_Type
                        gvAddRemove.CurrentRow.Cells(colARItemProductType).Value = IIf(clsCommon.myLen(objItem.Product_Type) <= 0, "Others", objItem.Product_Type)
                        gvAddRemove.CurrentRow.Cells(colARItemProductType).Tag = objItem.Product_Type
                        gvAddRemove.CurrentRow.Cells(colARIsBatchItem).Value = objItem.Is_Batch_Item
                        gvAddRemove.CurrentRow.Cells(colARUom).Value = objItem.Unit_Code
                        gvAddRemove.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(objItem.Unit_Code, Nothing)
                        gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value = Nothing
                        gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value = Nothing
                        gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value = Nothing
                        gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value = Nothing
                        If clsCommon.CompairString(objItem.Product_Type, "MI") = CompairStringResult.Equal Or clsCommon.CompairString(objItem.Product_Type, "MP") = CompairStringResult.Equal Then
                            FillQCGrid(gvAddRemove.CurrentRow.Index, True, gvAddRemove.CurrentRow.Cells(colARItemCode).Value)
                        End If
                        SetARBalance()
                    End If
                    isCellValueChanged = False
                End If
                If e.Column Is gvAddRemove.Columns(colARUom) Then
                    isCellValueChanged = True
                    OpenUOM(False)
                    If clsCommon.myLen(gvAddRemove.CurrentRow.Cells(colARUom).Value) > 0 Then
                        gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                        gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                        calculateALL()
                    End If
                    isCellValueChanged = False
                End If
                If e.Column Is gvAddRemove.Columns(colARLocCode) Then
                    isCellValueChanged = True
                    OpenARLocationCode(False)
                    isCellValueChanged = False
                End If
                If e.Column Is gvAddRemove.Columns(colARType) Then
                    isCellValueChanged = True
                    calculateALL()
                    isCellValueChanged = False
                End If
                If e.Column Is gvAddRemove.Columns(colARQty) AndAlso AutoCalcQtyAddRem = False Then
                    'BHA/10/08/18-000411 by balwinder on 09/08/2017
                    isCellValueChanged = True
                    gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    OpenBatchItem()
                    isCellValueChanged = False
                ElseIf e.Column Is gvAddRemove.Columns(colAR_FAT_Per) Then
                    isCellValueChanged = True
                    For ii As Integer = 0 To gv_qc.Rows.Count - 1
                        If (gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvAddRemove.CurrentRow.Index Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                    gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                                    gv_qc.Rows(ii).Cells(colActual_Range).Value = clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvAddRemove.Columns(colAR_SNF_Per) Then
                    isCellValueChanged = True
                    For ii As Integer = 0 To gv_qc.Rows.Count - 1
                        If (gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvAddRemove.CurrentRow.Index Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCType).Value), "Add/Remove") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                    gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                                    gv_qc.Rows(ii).Cells(colActual_Range).Value = clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value)
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvAddRemove.Columns(colAR_FAT_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value) > 0 Then
                        Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, 1, 100, Nothing)
                        gvAddRemove.CurrentRow.Cells(colARQty).Value = gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value * 100 / (gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value * IIf(conFat = 0, 1, conFat))
                    End If
                    gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value, Nothing)
                    calculateALL()
                    isCellValueChanged = False
                ElseIf e.Column Is gvAddRemove.Columns(colAR_SNF_KG) AndAlso AutoCalcQtyAddRem = True Then
                    isCellValueChanged = True
                    If clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value) > 0 Then
                        Dim conFat As Decimal = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, 1, 100, Nothing)
                        gvAddRemove.CurrentRow.Cells(colARQty).Value = gvAddRemove.CurrentRow.Cells(colAR_SNF_KG).Value * 100 / (gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value * IIf(conFat = 0, 1, conFat))
                    End If
                    gvAddRemove.CurrentRow.Cells(colAR_FAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvAddRemove.CurrentRow.Cells(colARItemCode).Value, gvAddRemove.CurrentRow.Cells(colARUom).Value, gvAddRemove.CurrentRow.Cells(colARQty).Value, gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value, Nothing)
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
            If ShowLocationItemLocationwise = 1 AndAlso clsCommon.CompairString(gvAddRemove.CurrentRow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                strItemLoc = " and Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value) & "')"
            End If
            Dim qry As String = clsProcessProductionStandardization.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value), IIf(clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
            gvAddRemove.CurrentRow.Cells(colARLocCode).Value = clsCommon.ShowSelectForm("LOCSUBLOCSTCKFND", qry, "Code", " [Type] in ('Section','Sub Location') and [Stock Qty]>=0 " & strItemLoc, clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value), "Code", isButtonClicked) '
        Else
            If ShowLocationItemLocationwise = 1 AndAlso clsCommon.CompairString(gvAddRemove.CurrentRow.Cells(colARType).Value, "Add") = CompairStringResult.Equal Then
                strItemLoc = " and Location_Code in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value) & "')"
            End If
            gvAddRemove.CurrentRow.Cells(colARLocCode).Value = clsLocation.getFinder("(((Is_Section='Y' or Is_Sub_Location='Y') and Main_Location_Code='" & txtlocation.Text & "') or Location_Code='" & txtlocation.Text & "')" & strItemLoc, gvAddRemove.CurrentRow.Cells(colARLocCode).Value, False)
        End If

        gvAddRemove.CurrentRow.Cells(colARLocDesc).Value = clsLocation.GetName(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value), Nothing)
        SetARBalance()
    End Sub

    Sub SetARBalance()
        Dim loc_type As Integer = 0
        qry = "select case when (isnull(is_section,'N')='N' and isnull(is_sub_location,'N')='N') then 'MAIN' when (isnull(is_section,'N')='Y' and isnull(is_sub_location,'N')='N') then 'SEC' when (isnull(is_section,'N')='N' and isnull(is_sub_location,'Y')='Y') then 'SUB' else'MAIN' end as [type] from tspl_location_master where location_code='" + clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value) + "'"
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "MAIN") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SUB") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)), "SEC") = CompairStringResult.Equal Then
            loc_type = 0
        End If
        gvAddRemove.CurrentRow.Cells(colARAvailQty).Value = "0"

        Dim dt As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value), txtlocation.Text, clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value), loc_type, IIf(clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal, True, False))
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvAddRemove.CurrentRow.Cells(colARAvailQty).Value = clsCommon.myCdbl(dt.Rows(0)("qty"))
            If settTankerDispatchAvgFATSNFPer AndAlso clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                Dim dblFATPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value), clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("fat_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                Dim dblSNFPer As Decimal = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value), clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value), clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("snf_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                gvAddRemove.CurrentRow.Cells(colAR_FAT_Per).Value = dblFATPer
                gvAddRemove.CurrentRow.Cells(colAR_SNF_Per).Value = dblSNFPer
                'For ii As Integer = 0 To gv_qc.Rows.Count - 1
                '    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCItemCode).Value), clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value)) = CompairStringResult.Equal Then
                '        If clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCParentLineNo).Value) = gvAddRemove.CurrentRow.Index Then
                '            gv_qc.CurrentColumn = gv_qc.Columns(colActual_Range)
                '            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                '                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblFATPer
                '            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                '                gv_qc.Rows(ii).Cells(colActual_Range).Value = dblSNFPer
                '            End If
                '        End If
                '    End If
                'Next
            End If
        End If
    End Sub


    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gvAddRemove.CurrentRow.Cells(colARItemCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select item code first", Me.Text)
            Exit Sub
        End If
        qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as Description,TSPL_ITEM_UOM_DETAIL.Weight,TSPL_ITEM_UOM_DETAIL.Stocking_Unit as [Stocking Unit],TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Conversion Factor] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        gvAddRemove.CurrentRow.Cells(colARUom).Value = clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value) + "'", clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value), "Code", isButtonClicked)
        If clsCommon.myLen(gvAddRemove.CurrentRow.Cells(colARUom).Value) > 0 Then
            gvAddRemove.CurrentRow.Cells(colARUOMDesc).Value = clsUOMInfo.GetUnitDesc(gvAddRemove.CurrentRow.Cells(colARUom).Value, Nothing)
        Else
            gvAddRemove.CurrentRow.Cells(colARUOMDesc).Value = ""
        End If
        SetARBalance()
    End Sub

    Private Sub gvIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvIssue.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                'If (e.Column Is gvIssue.Columns(colIssueStatus)) Then
                '    isCellValueChanged = True
                '    If clsCommon.myLen(gvIssue.Rows(e.RowIndex).Cells(colIssueStatus).Value) > 0 Then
                '        If EnableAddRemAndStageTab() Then
                '            pageAddRemoveDetail.Enabled = True
                '            pageStageDetail.Enabled = True
                '        End If
                '    End If
                '    isCellValueChanged = False
                'End If
            End If
        End If
    End Sub

    Function EnableAddRemAndStageTab() As Boolean
        'For Each grow As GridViewRowInfo In gvIssue.Rows
        '    If clsCommon.myLen(grow.Cells(colIssueStatus).Value) <= 0 Then
        '        Return False
        '    End If
        'Next
        Return True
    End Function



    Function EnableParameterTab() As Boolean
        For Each grow As GridViewRowInfo In gvProduce.Rows
            If clsCommon.myLen(grow.Cells(colProduceStdInLocation).Value) <= 0 Then
                Return False
            End If
        Next
        Return True
    End Function





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







    Sub UpdateBatchFatSNF(ByVal Item_Code As String, ByVal Value As Decimal, ByVal Type As String, ByVal QC_Type As String)
        '' update fat/snf in batch tab
        If clsCommon.CompairString(QC_Type, "Batch Order") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvProduce.Rows
                If clsCommon.CompairString(grow.Cells(colProduceItemCode).Value, Item_Code) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(Type, "FAT") = CompairStringResult.Equal Then
                        grow.Cells(colProduceFAT).Value = Value
                        grow.Cells(colProduceFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colProduceUOM).Value, grow.Cells(colProduceQty).Value, grow.Cells(colProduceFAT).Value, Nothing)
                    ElseIf clsCommon.CompairString(Type, "SNF") = CompairStringResult.Equal Then
                        grow.Cells(colProduceSNF).Value = Value
                        grow.Cells(colProduceSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(Item_Code, grow.Cells(colProduceUOM).Value, grow.Cells(colProduceQty).Value, grow.Cells(colProduceSNF).Value, Nothing)
                    End If
                    'Exit Sub
                End If
            Next
        End If


        '' update fat/snf in add/remove tab
        If clsCommon.CompairString(QC_Type, "Add/Remove") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvAddRemove.Rows
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


        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gvProduce.Rows.Count - 1
            dblQty += clsCommon.myCdbl(gvProduce.Rows(ii).Cells(colProduceQty).Value)
            dblFATKG += clsCommon.myCdbl(gvProduce.Rows(ii).Cells(colProduceFATKG).Value)
            dblSNFKG += clsCommon.myCdbl(gvProduce.Rows(ii).Cells(colProduceSNFKG).Value)
        Next
        lblTotProduceQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotProduceFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotProduceSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))

        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0

        For ii As Integer = 0 To gvIssue.Rows.Count - 1
            dblQty += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssueQty).Value)

            If clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssueFATKG).Value) > 0 Then
                dblFATKG += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssueFATKG).Value)
            End If
            If clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssueSNFKG).Value) > 0 Then
                dblSNFKG += clsCommon.myCdbl(gvIssue.Rows(ii).Cells(colIssueSNFKG).Value)
            End If
        Next
        lblTotIssueQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotIssueFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotIssueSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))
        lblTotDifferenceQty.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueQty.Text) - clsCommon.myCdbl(lblTotProduceQty.Text), 2, MidpointRounding.ToEven))
        lblTotDifferenceFATKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueFATKG.Text) - clsCommon.myCdbl(lblTotProduceFATKG.Text), 2, MidpointRounding.ToEven))
        lblTotDifferenceSNFKG.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(lblTotIssueSNFKG.Text) - clsCommon.myCdbl(lblTotProduceSNFKG.Text), 2, MidpointRounding.ToEven))

        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gvAddRemove.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.Rows(ii).Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                dblQty += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colARQty).Value)
                dblFATKG += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colAR_FAT_KG).Value)
                dblSNFKG += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colAR_SNF_KG).Value)
            End If
        Next
        lblTotAddedQty.Text = clsCommon.myFormat(Math.Round(dblQty, 2, MidpointRounding.ToEven))
        lblTotAddedFATKG.Text = clsCommon.myFormat(Math.Round(dblFATKG, 2, MidpointRounding.ToEven))
        lblTotAddedSNFKG.Text = clsCommon.myFormat(Math.Round(dblSNFKG, 2, MidpointRounding.ToEven))


        dblQty = 0
        dblFATKG = 0
        dblSNFKG = 0
        For ii As Integer = 0 To gvAddRemove.Rows.Count - 1
            If Not clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.Rows(ii).Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                dblQty += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colARQty).Value)
                dblFATKG += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colAR_FAT_KG).Value)
                dblSNFKG += clsCommon.myCdbl(gvAddRemove.Rows(ii).Cells(colAR_SNF_KG).Value)
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


    End Sub

    Private Sub gvARDetail_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvAddRemove.UserDeletedRow
        calculateALL()
    End Sub

    Private Sub gvARDetail_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvAddRemove.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True

        End If
    End Sub


    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gvAddRemove.CurrentRow.Cells(colARIsBatchItem).Value) Then
            If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Add") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                    Dim frm As frmBatchItemOutNew = New frmBatchItemOutNew()
                    frm.strItemCode = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemName).Value)
                    frm.strLocationCode = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value)
                    frm.strCurrDocNo = txtCode.Value
                    frm.strCurrDocType = MyBase.Form_ID
                    frm.strUOM = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value)
                    frm.dblqty = clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colARQty).Value)
                    frm.arr = TryCast(gvAddRemove.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                    If RunBatchFifowise Then
                        frm.OpenSerialList(0, "")
                        gvAddRemove.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    Else
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvAddRemove.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                        End If
                    End If
                Else
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemName).Value)
                    frm.strLocationCode = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARLocCode).Value)
                    frm.strCurrDocNo = txtCode.Value
                    frm.strCurrDocType = MyBase.Form_ID
                    frm.strUOM = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value)
                    frm.dblMRP = 0
                    frm.dblqty = clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colARQty).Value)
                    frm.arr = TryCast(gvAddRemove.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                    If RunBatchFifowise Then
                        frm.OpenSerialList(0, "")
                        gvAddRemove.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    Else
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvAddRemove.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                        End If
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                    '--BSP
                    'Dim frm As frmBatchItemIn_ForMilkItem = New frmBatchItemIn_ForMilkItem()
                    'frm.strItemCode = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemCode).Value)
                    'frm.strItemName = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARItemName).Value)
                    'frm.dblqty = clsCommon.myCdbl(gvARDetail.CurrentRow.Cells(colARQty).Value)
                    'frm.strUOM = clsCommon.myCstr(gvARDetail.CurrentRow.Cells(colARUom).Value)
                    'frm.arr = TryCast(gvARDetail.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                    'frm.ShowDialog()
                    'If Not frm.isCencelButtonClicked Then
                    '    gvARDetail.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    'End If
                Else
                    Dim frm As frmBatchItemIn = New frmBatchItemIn()
                    frm.strItemCode = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemCode).Value)
                    frm.strItemName = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemName).Value)
                    frm.dblqty = clsCommon.myCdbl(gvAddRemove.CurrentRow.Cells(colARQty).Value)
                    frm.strUOM = clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARUom).Value)
                    frm.arr = TryCast(gvAddRemove.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gvAddRemove.CurrentRow.Cells(colARItemCode).Tag = frm.arr
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingON()
        If clsCommon.myCBool(gvAddRemove.CurrentRow.Cells(colARIsBatchItem).Value) Then
            Dim strBatchunion As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARItemProductType).Value), "MI") = CompairStringResult.Equal Then
                Dim arr As List(Of clsBatchInventoryNew) = TryCast(gvAddRemove.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventoryNew))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsBatchInventoryNew In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                End If
            Else
                Dim arr As List(Of clsBatchInventory) = TryCast(gvAddRemove.CurrentRow.Cells(colARItemCode).Tag, List(Of clsBatchInventory))
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

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gvAddRemove.KeyDown
        If e.KeyCode = Keys.F5 Then
            If RunBatchFifowise AndAlso clsCommon.CompairString(clsCommon.myCstr(gvAddRemove.CurrentRow.Cells(colARType).Value), "Remove") = CompairStringResult.Equal Then
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
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "Standardization_Code", "TSPL_PP_STANDARDIZATION_HEAD", "TSPL_PP_STD_ISSUE_ITEM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
