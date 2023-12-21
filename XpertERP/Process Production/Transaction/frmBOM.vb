'===================BM00000003069,Updated By Rohit========================
'------------------BM00000003167--------------------BM00000004420----BM00000004866---BM00000004951
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmBOM
    Inherits FrmMainTranScreen

#Region "Variables"
    Public strBOMCodeForOpen As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim N_Level As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colItemType As String = "ItemType"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colqty As String = "Qty"
    Const colUnitCode As String = "UnitCode"
    Const colFAT As String = "FAT"
    Const colSNF As String = "SNF"
    Const colFAT_KG As String = "FAT KG"
    Const colSNF_KG As String = "SNF KG"
    Const colRemarks As String = "Remarks"
    Const colAltritemcode As String = "Altr_Item_Code"
    Const colAltrItemdesc As String = "Altr_Item_Desc"
    Const colAltrItemUom As String = "Altr_Uom"
    Const colrejctn As String = "Rej_pers"
    Const colAltrItemtype As String = "Alt_itemType"
    Const colStageCode As String = "StageCode"
    Const colStageDesc As String = "StageDesc"
    Const colStageSequence As String = "Sequence"
    Const colProductType As String = "ProductType"
    Const colDeactive As String = "Deactive"
    Const colEffectiveDate As String = "Effective Date"
    Const colBOMRecordShow As String = "BOMRecordShow"
    Const colProcessLossPer As String = "colProcessLossPer"
    Const colProcessLossQty As String = "colProcessLossQty"
    '' Item Grid More columns
    Const colConsm_Base As String = "colConsm_Base"

    '' Stage Grid More columns
    Const colAR_Item_Code As String = "colAR_Item_Code"
    Const colBi_Prod As String = "colBi_Prod"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsBOM
    Private ObjList As New List(Of clsBOM)
    Private isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoaddata As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
    Dim DecimalPoint As Integer = 3

    Const colCostLineNo As String = "colCostLineNo"
    Const colCost_Code As String = "colCost_Code"
    Const colCost_Desc As String = "colCost_Desc"
    Const colCostUOM As String = "colCostUOM"

    Const colStarnderRatePerHour As String = "colStarnderRatePerHour"
    Const colStanderHours As String = "colStanderHours"
    Const colStanderNO As String = "colStanderNO"
    Const colCost As String = "colCost" ' Standred Cost

    Const colBomRatePerHour As String = "colBomRatePerHour"
    Const colBomHours As String = "colBomHours"
    Const colBomNO As String = "colBomNO"
    Const ColOverHeadCost As String = "ColOverHeadCost" ' For Bom Cost

    Dim ActivateProductionWithoutBatch As String = "0"
    Dim isIncludeRatePerHoursIn As Boolean = False
#End Region

    Sub LoadItemType()
        ''richa BHA/13/07/18-000156 pick item type from item master
        ' TxtitemType.DataSource = clsItemMaster.GetItemType()
        TxtitemType.DataSource = clsItemMaster.getItemTypeQuery()
        TxtitemType.DisplayMember = "Name"
        TxtitemType.ValueMember = "Code"
    End Sub

    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemTypeCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim scrap_per As New GridViewDecimalColumn
        Dim wastage_per As New GridViewDecimalColumn
        Dim scrap_kg As New GridViewDecimalColumn
        Dim wastage_kg As New GridViewDecimalColumn
        Dim remarks As New GridViewTextBoxColumn
        Dim ProductTypeCode As New GridViewTextBoxColumn
        Dim Consm_Base As New GridViewComboBoxColumn
        Dim ProcessLossPer As New GridViewDecimalColumn
        Dim ProcessLossQty As New GridViewDecimalColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(LineNo)

        ItemTypeCode.FormatString = ""
        ItemTypeCode.HeaderText = "Item Type"
        ItemTypeCode.Name = colItemType
        ItemTypeCode.Width = 100
        'ItemTypeCode.ReadOnly = True
        ItemTypeCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ItemTypeCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ItemTypeCode.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(ItemTypeCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 130
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(itemDesc)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "UOM"
        UnitCode.Name = colUnitCode
        UnitCode.Width = 100
        UnitCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        UnitCode.TextImageRelation = TextImageRelation.TextBeforeImage
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(UnitCode)

        ProductTypeCode.FormatString = ""
        ProductTypeCode.HeaderText = "Product Type"
        ProductTypeCode.Name = colProductType
        ProductTypeCode.Width = 100
        ProductTypeCode.ReadOnly = True
        ProductTypeCode.IsVisible = True
        ProductTypeCode.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvBOM.MasterTemplate.Columns.Add(ProductTypeCode)


        qty.FormatString = ""
        qty.HeaderText = "Std. Quantity"
        qty.Name = colqty
        qty.Width = 100
        qty.DecimalPlaces = DecimalPoint
        gvBOM.MasterTemplate.Columns.Add(qty)

        scrap_per.FormatString = ""
        scrap_per.HeaderText = "FAT%"
        scrap_per.Name = colFAT
        scrap_per.Width = 80
        scrap_per.DecimalPlaces = 2
        scrap_per.ReadOnly = False
        'scrap_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(scrap_per)

        scrap_kg.FormatString = ""
        scrap_kg.HeaderText = "FAT KG"
        scrap_kg.Name = colFAT_KG
        scrap_kg.Width = 80
        scrap_kg.DecimalPlaces = DecimalPoint
        scrap_kg.ReadOnly = True
        'scrap_kg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(scrap_kg)

        wastage_per.FormatString = ""
        wastage_per.HeaderText = "SNF%"
        wastage_per.Name = colSNF
        wastage_per.Width = 80
        wastage_per.DecimalPlaces = 2
        wastage_per.ReadOnly = False
        'wastage_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(wastage_per)

        wastage_kg.FormatString = ""
        wastage_kg.HeaderText = "SNF KG"
        wastage_kg.Name = colSNF_KG
        wastage_kg.Width = 80
        wastage_kg.DecimalPlaces = DecimalPoint
        wastage_kg.ReadOnly = True
        'wastage_kg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(wastage_kg)

        Dim reporejection As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporejection.FormatString = ""
        reporejection.Name = colrejctn
        reporejection.Width = 80
        reporejection.HeaderText = "Rejection %"
        reporejection.DecimalPlaces = 2
        gvBOM.MasterTemplate.Columns.Add(reporejection)

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.HeaderText = "Alt.Item Code"
        repoicode.Name = colAltritemcode
        repoicode.Width = 100
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoicode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(repoicode)

        Dim repodesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodesc.FormatString = ""
        repodesc.HeaderText = "Alt.Item Description"
        repodesc.Name = colAltrItemdesc
        repodesc.Width = 130
        repodesc.ReadOnly = True
        'repodesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(repodesc)

        Dim repoitemtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemtype.FormatString = ""
        repoitemtype.HeaderText = "Alt.Item Type"
        repoitemtype.Name = colAltrItemtype
        repoitemtype.Width = 100
        repoitemtype.ReadOnly = True
        ' repoitemtype.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(repoitemtype)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.HeaderText = "Alt.UOM"
        repouom.Name = colAltrItemUom
        repouom.Width = 100
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        'repouom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(repouom)

        Dim repodeactive As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repodeactive.FormatString = ""
        repodeactive.HeaderText = "Is Deactive"
        repodeactive.Name = colDeactive
        repodeactive.Width = 50
        gvBOM.MasterTemplate.Columns.Add(repodeactive)

        Dim repoEffctdate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoEffctdate.FormatString = "{0:d}"
        repoEffctdate.HeaderText = "Effective Date"
        repoEffctdate.Name = colEffectiveDate
        repoEffctdate.Width = 80
        repoEffctdate.ReadOnly = False
        gvBOM.MasterTemplate.Columns.Add(repoEffctdate)

        remarks = New GridViewTextBoxColumn()
        remarks.FormatString = ""
        remarks.HeaderText = "Remarks"
        remarks.Name = colRemarks
        remarks.Width = 130
        remarks.MaxLength = 250
        'remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.MasterTemplate.Columns.Add(remarks)

        remarks = New GridViewTextBoxColumn()
        remarks.FormatString = ""
        remarks.HeaderText = "BOM Records"
        remarks.Name = colBOMRecordShow
        remarks.Width = 130
        remarks.ReadOnly = True
        gvBOM.MasterTemplate.Columns.Add(remarks)

        Consm_Base.FormatString = ""
        Consm_Base.HeaderText = "Consumption Basis"
        Consm_Base.Name = colConsm_Base
        Consm_Base.Width = 100
        Consm_Base.DataSource = FillConsm_Basis()
        Consm_Base.DisplayMember = "Name"
        Consm_Base.ValueMember = "Code"
        gvBOM.MasterTemplate.Columns.Add(Consm_Base)

        ProcessLossPer.FormatString = ""
        ProcessLossPer.HeaderText = "Process Loss%"
        ProcessLossPer.Name = colProcessLossPer
        ProcessLossPer.Width = 120
        ProcessLossPer.DecimalPlaces = 2
        ProcessLossPer.ReadOnly = False
        gvBOM.MasterTemplate.Columns.Add(ProcessLossPer)

        ProcessLossQty.FormatString = ""
        ProcessLossQty.HeaderText = "Process Loss Qty"
        ProcessLossQty.Name = colProcessLossQty
        ProcessLossQty.Width = 120
        ProcessLossQty.DecimalPlaces = 0
        ProcessLossQty.ReadOnly = True
        gvBOM.MasterTemplate.Columns.Add(ProcessLossQty)

        gvBOM.AllowDeleteRow = True
        gvBOM.AllowAddNewRow = False
        gvBOM.ShowGroupPanel = False
        gvBOM.AllowColumnReorder = True
        gvBOM.AllowRowReorder = False
        gvBOM.EnableSorting = False
        gvBOM.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvBOM.MasterTemplate.ShowRowHeaderColumn = False
        gvBOM.Rows.AddNew()
        gvBOM.Rows(0).Cells(colBOMRecordShow).Value = "No Record"



        LineNo = Nothing
        ItemTypeCode = Nothing
        ItemCode = Nothing
        itemDesc = Nothing
        qty = Nothing
        UnitCode = Nothing
        scrap_per = Nothing
        wastage_per = Nothing
        scrap_kg = Nothing
        wastage_kg = Nothing
        remarks = Nothing
        ProductTypeCode = Nothing
    End Sub
    Private Function FillConsm_Basis() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FAT"
        dr("Name") = "Fat Basis"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SNF"
        dr("Name") = "SNF Basis"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Function Fill_BiProd() As DataTable
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

        Return dt
    End Function

    Function LoadStageCombo()
        Dim dt As DataTable = New DataTable
        dt = clsDBFuncationality.GetDataTable("select TSPL_STAGE_MASTER.Stage_Code as Code,Description + '(' + TSPL_STAGE_MASTER.Stage_Code + ')' " _
              & " as Name from TSPL_STAGE_MASTER Inner join TSPL_SECTION_STAGE_MAPPING on TSPL_SECTION_STAGE_MAPPING.Stage_Code" _
              & " =TSPL_STAGE_MASTER.Stage_Code and Section_Code='" + fndSection.Value + "'")
        Return dt
    End Function

    Sub LoadStageGridColumns()
        gvStages.Rows.Clear()
        gvStages.Columns.Clear()

        Dim Code As New GridViewTextBoxColumn
        Dim Name As New GridViewTextBoxColumn
        Dim Bi_Prod As New GridViewComboBoxColumn

        Code.FormatString = ""
        Code.HeaderText = "Stage Code"
        Code.Name = colStageCode
        Code.ReadOnly = True
        Code.Width = CInt(gvStages.Width / 4)
        Code.TextImageRelation = TextImageRelation.TextBeforeImage
        'Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvStages.MasterTemplate.Columns.Add(Code)

        Name.FormatString = ""
        Name.HeaderText = "Stage Description"
        Name.Name = colStageDesc
        Name.ReadOnly = True
        Name.Width = CInt(gvStages.Width / 2)
        Name.ReadOnly = True
        'Name.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvStages.MasterTemplate.Columns.Add(Name)

        Dim reposeq As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposeq.FormatString = ""
        reposeq.Name = colStageSequence
        reposeq.ReadOnly = False
        reposeq.HeaderText = "Sequence"
        reposeq.Width = 60
        reposeq.DecimalPlaces = 0
        gvStages.MasterTemplate.Columns.Add(reposeq)

        Code = New GridViewTextBoxColumn
        Code.FormatString = ""
        Code.HeaderText = "Add/Remove Item Code"
        Code.Name = colAR_Item_Code
        Code.Width = 100
        Code.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Code.TextImageRelation = TextImageRelation.TextBeforeImage
        Code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvStages.MasterTemplate.Columns.Add(Code)

        Bi_Prod.FormatString = ""
        Bi_Prod.HeaderText = "Is Bi-Production"
        Bi_Prod.Name = colBi_Prod
        Bi_Prod.Width = 100
        Bi_Prod.DataSource = Fill_BiProd()
        Bi_Prod.DisplayMember = "Name"
        Bi_Prod.ValueMember = "Code"
        gvStages.MasterTemplate.Columns.Add(Bi_Prod)

        gvStages.AllowDeleteRow = True
        gvStages.AllowAddNewRow = False
        gvStages.ShowGroupPanel = False
        gvStages.AllowColumnReorder = True
        gvStages.AllowRowReorder = False
        gvStages.EnableSorting = False
        gvStages.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvStages.MasterTemplate.ShowRowHeaderColumn = False
        gvStages.Rows.AddNew()
    End Sub

    Private Sub frmBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
                btnNew.Focus()
                btnNew.Select()
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.Focus()
                btnsave.Select()
                Save(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.Focus()
                btndelete.Select()
                DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                btnclose.Select()
                btnclose.Focus()
                funClose()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                btnNew.Focus()
                btnNew.Select()
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                btnPost.Focus()
                btnPost.Select()
                PostData()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                 "TSPL_PP_BOM_HEAD " + Environment.NewLine + _
                                                 "TSPL_PP_BOM_ITEM_DETAIL " + Environment.NewLine + _
                                                 "TSPL_PP_BOM_STAGE_DETAIL " + Environment.NewLine + _
                                                 "TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS ")
                If btnPost.Enabled = False AndAlso btnsave.Enabled = False Then
                    'Dim frm As New FrmPWD(Nothing)
                    'frm.strType = "SIRC"
                    'frm.strCode = "SIReversAndCreate"
                    'frm.ShowDialog()
                    'If frm.isPasswordCorrect Then
                    '    btnReverse.Visible = True
                    'End If
                End If
            End If

            If e.Alt AndAlso e.KeyCode = Keys.A AndAlso btnReverse.Visible = True Then
                btnReverse.Focus()
                btnReverse.Select()
                btnReverse.PerformClick()
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colItemCode) Then
                isCellValueChangedOpen = True
                OpenIcode(True, "")
                Cal_FAT()
                Cal_SNF()
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colUnitCode) Then
                isCellValueChangedOpen = True
                OpenUom(True, "")
                Cal_FAT()
                Cal_SNF()
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colAltritemcode) Then
                isCellValueChangedOpen = True
                OpenIcode(True, "Alt")
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colAltrItemUom) Then
                isCellValueChangedOpen = True
                OpenUom(True, "Alt")
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso ((gvBOM.CurrentColumn Is gvBOM.Columns(colqty)) Or (gvBOM.CurrentColumn Is gvBOM.Columns(colFAT)) Or (gvBOM.CurrentColumn Is gvBOM.Columns(colFAT_KG))) Then
                isCellValueChangedOpen = True
                Cal_FAT()
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso ((gvBOM.CurrentColumn Is gvBOM.Columns(colqty)) Or (gvBOM.CurrentColumn Is gvBOM.Columns(colSNF)) Or (gvBOM.CurrentColumn Is gvBOM.Columns(colSNF_KG))) Then
                isCellValueChangedOpen = True
                Cal_SNF()
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colDeactive) Then
                isCellValueChangedOpen = True
                If clsCommon.myCBool(gvBOM.CurrentRow.Cells(colDeactive).Value) = True Then
                    gvBOM.CurrentRow.Cells(colEffectiveDate).ReadOnly = False
                Else
                    gvBOM.CurrentRow.Cells(colEffectiveDate).ReadOnly = True
                End If
                isCellValueChangedOpen = False
            End If

            'If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colItemCode) Then
            '    isCellValueChangedOpen = True
            '    If clsCommon.myCstr(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value) <> "Milk" Then
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF).ReadOnly = True
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT).ReadOnly = True
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF).Value = 0
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT).Value = 0
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = True
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = True
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF_KG).Value = 0
            '        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT_KG).Value = 0
            '    End If
            '    isCellValueChangedOpen = False
            'End If

            If e.KeyCode = Keys.F2 AndAlso gvBOM.CurrentCell IsNot Nothing AndAlso gvBOM.CurrentColumn Is gvBOM.Columns(colItemType) Then
                isCellValueChangedOpen = True
                OpenItemType(True)
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funClose()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub LoadCostGroupGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colCostLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = "Cost Code"
        repoGroupCode.Name = colCost_Code
        repoGroupCode.ReadOnly = True
        repoGroupCode.Width = 120
        gv1.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupDesc.FormatString = ""
        repoGroupDesc.HeaderText = "Cost Description"
        repoGroupDesc.Name = colCost_Desc
        repoGroupDesc.Width = 180
        repoGroupDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGroupDesc)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colCostUOM
        repoUOM.Width = 100
        repoUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUOM)

        'If isIncludeRatePerHoursIn = True Then
        '=============== For Standred cost ===================================
        'Dim repoGroupRatePerHour As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoGroupRatePerHour.FormatString = "{0:N2}"
        'repoGroupRatePerHour.HeaderText = "Rate/Hour"
        'repoGroupRatePerHour.Name = colStarnderRatePerHour
        'repoGroupRatePerHour.Width = 80
        'repoGroupRatePerHour.ReadOnly = False
        'repoGroupRatePerHour.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoGroupRatePerHour)

        'Dim repoGroupHour As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoGroupHour.FormatString = "{0:N2}"
        'repoGroupHour.HeaderText = "Hour"
        'repoGroupHour.Name = colStanderHours
        'repoGroupHour.Width = 80
        'repoGroupHour.ReadOnly = False
        'repoGroupHour.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoGroupHour)

        'Dim repoGroupNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoGroupNo.FormatString = "{0:N2}"
        'repoGroupNo.HeaderText = "NO"
        'repoGroupNo.Name = colStanderNO
        'repoGroupNo.Width = 80
        'repoGroupNo.ReadOnly = False
        'repoGroupNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoGroupNo)

        Dim repoGroupCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupCost.FormatString = "{0:N2}"
        repoGroupCost.Name = colCost
        repoGroupCost.Width = 150
        If isIncludeRatePerHoursIn = True Then
            repoGroupCost.HeaderText = "Standard Cost"
            repoGroupCost.ReadOnly = True
        Else
            repoGroupCost.HeaderText = "Cost"
            repoGroupCost.ReadOnly = False
        End If
        repoGroupCost.ReadOnly = True
        repoGroupCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupCost)

        '================================= Bom Cost=====================================
        Dim repoGroupRatePerHourBom As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupRatePerHourBom.FormatString = "{0:N2}"
        repoGroupRatePerHourBom.HeaderText = "Rate/Hour"
        repoGroupRatePerHourBom.Name = colBomRatePerHour
        repoGroupRatePerHourBom.Width = 80
        repoGroupRatePerHourBom.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupRatePerHourBom.IsVisible = True
        Else
            repoGroupRatePerHourBom.IsVisible = False
        End If
        repoGroupRatePerHourBom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupRatePerHourBom)

        Dim repoGroupHourBom As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupHourBom.FormatString = "{0:N2}"
        repoGroupHourBom.HeaderText = "Hour"
        repoGroupHourBom.Name = colBomHours
        repoGroupHourBom.Width = 80
        repoGroupHourBom.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupHourBom.IsVisible = True
        Else
            repoGroupHourBom.IsVisible = False
        End If
        repoGroupHourBom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupHourBom)

        Dim repoGroupNoBom As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupNoBom.FormatString = "{0:N2}"
        repoGroupNoBom.HeaderText = "NO"
        repoGroupNoBom.Name = colBomNO
        repoGroupNoBom.Width = 80
        repoGroupNoBom.ReadOnly = False
        If isIncludeRatePerHoursIn = True Then
            repoGroupNoBom.IsVisible = True
        Else
            repoGroupNoBom.IsVisible = False
        End If
        repoGroupNoBom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupNoBom)

        Dim repoGroupCostBom As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGroupCostBom.FormatString = "{0:N2}"
        repoGroupCostBom.Name = ColOverHeadCost
        repoGroupCostBom.Width = 150
        If isIncludeRatePerHoursIn = True Then
            repoGroupCostBom.HeaderText = "BOM Cost"
            repoGroupCostBom.ReadOnly = True
        Else
            repoGroupCostBom.HeaderText = "Overhead Cost"
            repoGroupCostBom.ReadOnly = False
        End If
        repoGroupCostBom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGroupCostBom)

        'Else
        '    Dim repoCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        '    repoCost.FormatString = ""
        '    repoCost.HeaderText = "Cost"
        '    repoCost.Name = colCost
        '    repoCost.Width = 100
        '    repoCost.ReadOnly = True
        '    gv1.MasterTemplate.Columns.Add(repoCost)

        '    Dim repoOverheadCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        '    repoOverheadCost.FormatString = ""
        '    repoOverheadCost.HeaderText = "Overhead Cost"
        '    repoOverheadCost.Name = ColOverHeadCost
        '    repoOverheadCost.Width = 100
        '    repoOverheadCost.ReadOnly = False
        '    gv1.MasterTemplate.Columns.Add(repoOverheadCost)
        'End If


        '  clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.AllowRowReorder = True
        'ReStoreGridLayout()
    End Sub

    Private Sub frmBOM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ActivateProductionWithoutBatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ActivateProductionWithoutBatch, clsFixedParameterCode.ActivateProductionWithoutBatch, Nothing))
        DecimalPoint = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        ' Ticket No : BHA/03/08/18-000387 By prabhakar for include Rate Per Hours
        isIncludeRatePerHoursIn = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IncludeRatePerHoursIn & "'")) = 0, False, True)
        If DecimalPoint <= 0 Then
            DecimalPoint = 3
        End If

        LoadItemType()
        funReset()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnReverse, "Press Alt+A for Amendment  ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        btnHistory.Visible = True
        btnPDF.Visibility = ElementVisibility.Collapsed
        btnReverse.Visible = True
        Dim values As String = CheckNLevelCat()
        N_Level = IIf(values = "1", True, False)

        If clsCommon.myLen(strBOMCodeForOpen) > 0 Then
            LoadData(strBOMCodeForOpen, NavigatorType.Current)

            btnsave.Enabled = False
            btndelete.Enabled = False
            btnPost.Enabled = False
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBillOfMaterialDairy)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPrint.Visible = MyBase.isPrintFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnReverse.Visible = MyBase.isReverse
        btnPrint.Enabled = MyBase.isPrintFlag
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
        txtbuildUnit.Text = ""
        txtdesc.Text = ""
        txtvalid.Text = clsCommon.GETSERVERDATE(Nothing)
        txtvalidupto.Text = clsCommon.GETSERVERDATE(Nothing).AddYears(3)
        isNewEntry = True
        txtrevisionno.Text = ""
        txtCode.MyReadOnly = False
        txtCode.Value = ""
        txtCode.Focus()
        Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE(Nothing)
        UsLock1.Status = ERPTransactionStatus.Open
        cboBOMStatus.Text = "Open"
        fndItemCategory.Value = ""
        TxtCategory.Text = ""
        txtsubcat_name.Text = ""
        txtsubcatcode.Text = ""
        Me.txtProducedItem.Value = ""
        Me.lblMasterItemName.Text = ""
        txtUomCode.Value = ""
        txtuomname.Text = ""
        TxtitemType.SelectedValue = ""
        TxtitemType.Enabled = True
        Me.txtBuildQty.Text = ""
        txtBuildQty.DecimalPlaces = DecimalPoint
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnReverse.Enabled = False
        LoadGridColumns()
        LoadStageGridColumns()
        fndSection.Value = ""
        TxtSection.Text = ""
        RadPageView.SelectedPage = RadPageItemDetails
        UsLock1.Status = ERPTransactionStatus.Open

        txtVendorCode.Value = ""
        txtvendorName.Text = ""
        chkOSP_JW.Checked = False
        txtVendorCode.Enabled = chkOSP_JW.Checked
        chkOSP_JW.Enabled = True

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        obj.Byproduct_Item_Code = ""
        obj.Byproduct_Item_UOM = ""
        obj.Byproduct_Item_Qty = 0

        RadSplitButton1.Visible = False
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        'btnReverse.Visible = False
        txtdesc.Focus()
        txtdesc.Select()
        txtJobWorkLoc.Value = ""
        LoadCostGroupGrid()
        txtCostGroup.Value = ""
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            RadSplitButton1.Visible = False
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed

            'btnReverse.Visible = False
            IsinsideLoaddata = True
            isNewEntry = True
            chkOSP_JW.Enabled = True

            obj = clsBOM.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0) Then
                isNewEntry = False
                TxtitemType.Enabled = False
                txtCode.Value = obj.BOM_CODE
                txtdesc.Text = obj.DESCRIPTION
                Me.dtpBOMDate.Value = obj.BOM_DATE
                Me.cboBOMStatus.Text = obj.STATUS
                Me.txtProducedItem.Value = obj.PROD_ITEM_CODE
                Me.lblMasterItemName.Text = obj.PROD_ITEM_NAME
                Me.TxtitemType.SelectedValue = obj.PROD_ITEM_TYPE
                txtrevisionno.Text = obj.revisionno
                txtUomCode.Value = obj.PROD_ITEM_UNIT_CODE
                txtvalid.Text = obj.START_DATE
                txtvalidupto.Text = obj.END_DATE
                txtuomname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + txtUomCode.Value + "'"))
                txtbuildUnit.Text = txtuomname.Text
                Me.txtBuildQty.Text = obj.PROD_QUANTITY
                Me.fndSection.Value = obj.Section_Code
                Me.TxtSection.Text = obj.Section_Name 'clsDBFuncationality.getSingleValue("select Description from tspl_section_master where section_code ='" + fndSection.Value + "'")
                fndItemCategory.Value = obj.prodcategorycode
                TxtCategory.Text = obj.prodcatdesc

                txtByproductItem.Value = obj.Byproduct_Item_Code
                txtByproductUOM.Value = obj.Byproduct_Item_UOM
                txtByproductQty.Value = obj.Byproduct_Item_Qty

                chkOSP_JW.Checked = IIf(obj.Is_OSP = 1, True, False)
                txtVendorCode.Value = obj.Vendor_Code
                txtvendorName.Text = obj.Vendor_Name
                txtVendorCode.Enabled = False
                chkOSP_JW.Enabled = False
                txtJobWorkLoc.Value = clsCommon.myCstr(obj.JobWork_Loc)

                LoadGridColumns()
                gvBOM.Rows.Clear()
                gvBOM.Rows.AddNew()
                If (obj.ObjListBOM IsNot Nothing AndAlso obj.ObjListBOM.Count > 0) Then
                    For Each objtr As clsBOMItemDetail In obj.ObjListBOM
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = objtr.Line_No
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = objtr.CONSM_ITEM_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = objtr.ITEM_DESCRIPTION
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemType).Value = ItemType(objtr.CONSM_ITEM_TYPE)
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = objtr.CONSM_ITEM_UNIT_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value = ProductType(objtr.CONSM_ITEM_PRODUCT_TYPE)
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = System.Math.Round(objtr.CONSM_QUANTITY, DecimalPoint)
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT).Value = objtr.FAT
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF).Value = objtr.SNF
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT_KG).Value = objtr.fat_kg
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF_KG).Value = objtr.snf_kg
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colrejctn).Value = objtr.Rejection
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAltritemcode).Value = objtr.alticode
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAltrItemdesc).Value = objtr.altiname
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAltrItemtype).Value = ItemType(objtr.altitype)
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colAltrItemUom).Value = objtr.altunitcode
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = objtr.REMARKS
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colDeactive).Value = clsCommon.myCBool(IIf(objtr.Deactive = "1", True, False))
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colConsm_Base).Value = objtr.Consm_Base
                        ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProcessLossPer).Value = objtr.ProcessLossPer
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProcessLossQty).Value = objtr.ProcessLossQty
                        ''---------------
                        If objtr.Deactive = "1" Then
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colEffectiveDate).Value = objtr.Effectivedate
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colEffectiveDate).ReadOnly = False
                        Else
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colEffectiveDate).Value = Nothing
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colEffectiveDate).ReadOnly = True
                        End If
                        ''richa agarwal BHA/12/07/18-000147 snf and fat colum should be enabled in case of milk item
                        If clsCommon.CompairString(clsCommon.myCstr(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value), "Milk") <> CompairStringResult.Equal Then
                            gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF).ReadOnly = True
                            gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT).ReadOnly = True
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF).Value = 0
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT).Value = 0
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF_KG).ReadOnly = True
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT_KG).ReadOnly = True
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF_KG).Value = 0
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT_KG).Value = 0
                        Else
                            gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF).ReadOnly = False
                            gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT).ReadOnly = False
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colSNF_KG).ReadOnly = False
                            'gvBOM.Rows(gvBOM.RowCount - 1).Cells(colFAT_KG).ReadOnly = False
                        End If
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colBOMRecordShow).Value = IIf(clsBOM.CheckBOMExist(txtCode.Value, objtr.CONSM_ITEM_CODE, objtr.CONSM_ITEM_UNIT_CODE) > 0, "Record Exist", "No Record")

                        gvBOM.Rows.AddNew()
                    Next
                End If

                LoadStageGridColumns()
                gvStages.Rows.Clear()
                gvStages.Rows.AddNew()
                If obj.ObjListBOMOP IsNot Nothing AndAlso obj.ObjListBOMOP.Count > 0 Then
                    For Each objtr As clsBOMStage In obj.ObjListBOMOP
                        gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageCode).Value = objtr.stagecode
                        gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageDesc).Value = objtr.stagename
                        gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageSequence).Value = objtr.stage_seq

                        gvStages.Rows(gvStages.Rows.Count - 1).Cells(colAR_Item_Code).Value = objtr.AR_Item_Code
                        gvStages.Rows(gvStages.Rows.Count - 1).Cells(colBi_Prod).Value = objtr.Bi_Prod
                        gvStages.Rows.AddNew()
                    Next
                End If
                txtCostGroup.Value = clsCommon.myCstr(obj.OverHeadCostGroupCode)
                LoadCostGroupGrid()
                gv1.Rows.Clear()
                gv1.Rows.AddNew()
                If obj.ObjCostGroupDetail IsNot Nothing AndAlso obj.ObjCostGroupDetail.Count > 0 Then
                    LoadCostGroupDetail(obj.ObjCostGroupDetail)
                End If
                txtCode.MyReadOnly = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnsave.Text = "Update"
                btnPost.Enabled = True
                btnReverse.Enabled = False
                If clsCommon.CompairString(obj.STATUS, "Open") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.STATUS, "On Hold") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.STATUS, "Approved") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                ElseIf clsCommon.CompairString(obj.STATUS, "In-Active") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Cancel
                End If

                If clsCommon.CompairString(obj.IsPost, "1") = CompairStringResult.Equal Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnReverse.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                UcAttachment1.LoadData(txtCode.Value)
            Else
                funReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsinsideLoaddata = False
        End Try


    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Private Function ItemType(ByVal itype As String) As String
        Dim values As String = Nothing
        ''richa BHA/13/07/18-000156 pick item type from item master
        'If clsCommon.CompairString(itype, "R") = CompairStringResult.Equal Then
        '    values = "Raw Material"
        'ElseIf clsCommon.CompairString(itype, "F") = CompairStringResult.Equal Then
        '    values = "Finished Good"
        'ElseIf clsCommon.CompairString(itype, "S") = CompairStringResult.Equal Then
        '    values = "Semi Finished Good"
        'ElseIf clsCommon.CompairString(itype, "A") = CompairStringResult.Equal Then
        '    values = "Asset"
        'ElseIf clsCommon.CompairString(itype, "H") = CompairStringResult.Equal Then
        '    values = "Fresh"
        'ElseIf clsCommon.CompairString(itype, "O") = CompairStringResult.Equal Then
        '    values = "Other"
        'End If
        'clsItemMaster.GetItemType()

        values = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER where ITEM_TYPE_CODE='" & clsCommon.myCstr(itype) & "'"))
        Return values
    End Function

    Private Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        values = clsItemMaster.ProductType(Product_type)

        Return values
    End Function

    Public Function Save(ByVal isPost As Boolean) As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New clsBOM
                obj.BOM_CODE = clsCommon.myCstr(txtCode.Value)
                obj.DESCRIPTION = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
                obj.BOM_DATE = clsCommon.myCDate(dtpBOMDate.Value)
                obj.STATUS = clsCommon.myCstr(cboBOMStatus.Text)
                obj.Section_Code = clsCommon.myCstr(fndSection.Value)
                obj.PROD_ITEM_CODE = clsCommon.myCstr(txtProducedItem.Value)
                obj.PROD_QUANTITY = clsCommon.myCdbl(txtBuildQty.Text)
                obj.PROD_ITEM_UNIT_CODE = clsCommon.myCstr(txtUomCode.Value)
                If clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then 'at very first no revision no, each update revision no change
                    'GenerateRevisionNo()
                End If

                obj.Byproduct_Item_Code = txtByproductItem.Value
                obj.Byproduct_Item_UOM = txtByproductUOM.Value
                obj.Byproduct_Item_Qty = txtByproductQty.Value

                obj.revisionno = clsCommon.myCstr(txtrevisionno.Text)
                obj.START_DATE = clsCommon.myCDate(txtvalid.Text)
                obj.END_DATE = clsCommon.myCDate(txtvalidupto.Text)
                obj.prodcategorycode = clsCommon.myCstr(fndItemCategory.Value)

                obj.Is_OSP = CInt(clsCommon.myCdbl(IIf(chkOSP_JW.Checked, "1", "0")))
                If chkOSP_JW.Checked Then
                    obj.Vendor_Code = clsCommon.myCstr(txtVendorCode.Value)
                End If
                obj.JobWork_Loc = clsCommon.myCstr(txtJobWorkLoc.Value)

                obj.IsPost = "0"
                If clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
                    obj.IsPost = "0"
                    If clsCommon.myCDate(txtvalidupto.Text) <= clsCommon.myCDate(txtvalid.Text) Then
                        If clsCommon.MyMessageBoxShow("Do you want to change Valid Upto date?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            txtvalidupto.Focus()
                            txtvalidupto.Select()
                            Errorcontrol.SetError(txtvalidupto, "Change date.")
                            Exit Function
                        Else
                            Errorcontrol.ResetError(txtvalidupto)
                        End If
                    End If
                End If
                '-----------------item detail=--------------------------
                Dim obj1 As clsBOMItemDetail
                obj.ObjListBOM = New List(Of clsBOMItemDetail)
                For Each grow As GridViewRowInfo In gvBOM.Rows
                    obj1 = New clsBOMItemDetail()

                    obj1.BOM_CODE = clsCommon.myCstr(txtCode.Value)
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.CONSM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj1.CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells(colqty).Value)
                    obj1.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    obj1.FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                    obj1.SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    obj1.fat_kg = clsCommon.myCdbl(grow.Cells(colFAT_KG).Value)
                    obj1.snf_kg = clsCommon.myCdbl(grow.Cells(colSNF_KG).Value)
                    obj1.Rejection = clsCommon.myCdbl(grow.Cells(colrejctn).Value)
                    obj1.alticode = clsCommon.myCstr(grow.Cells(colAltritemcode).Value)
                    obj1.altunitcode = clsCommon.myCstr(grow.Cells(colAltrItemUom).Value)
                    obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                    obj1.Deactive = CInt(IIf(clsCommon.myCBool(grow.Cells(colDeactive).Value) = True, 1, 0))
                    If obj1.Deactive = "1" Then
                        obj1.Effectivedate = clsCommon.myCDate(grow.Cells(colEffectiveDate).Value)
                    Else
                        obj1.Effectivedate = Nothing
                    End If
                    obj1.Consm_Base = clsCommon.myCstr(grow.Cells(colConsm_Base).Value)
                    ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                    obj1.ProcessLossPer = clsCommon.myCdbl(grow.Cells(colProcessLossPer).Value)
                    obj1.ProcessLossQty = clsCommon.myCdbl(grow.Cells(colProcessLossQty).Value)
                    ''---------------
                    If clsCommon.myLen(obj1.CONSM_ITEM_CODE) > 0 Then
                        obj.ObjListBOM.Add(obj1)
                    End If
                Next
                '---------------------------------------------------

                '-------------------------------stage detail-------------------
                obj.ObjListBOMOP = New List(Of clsBOMStage)
                For Each grow As GridViewRowInfo In gvStages.Rows
                    Dim objtr As New clsBOMStage()

                    objtr.stagecode = clsCommon.myCstr(grow.Cells(colStageCode).Value)
                    objtr.stage_seq = CInt(grow.Cells(colStageSequence).Value)

                    objtr.AR_Item_Code = clsCommon.myCstr(grow.Cells(colAR_Item_Code).Value)
                    objtr.Bi_Prod = clsCommon.myCstr(grow.Cells(colBi_Prod).Value)
                    If clsCommon.myLen(objtr.stagecode) > 0 Then
                        obj.ObjListBOMOP.Add(objtr)
                    End If
                Next
                '-------------------------------------------------------------
                '-------------------------------Overheat Cost detail-------------------
                obj.OverHeadCostGroupCode = clsCommon.myCstr(txtCostGroup.Value)
                obj.ObjCostGroupDetail = New List(Of clsBomCostMappingDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objtr As New clsBomCostMappingDetails()
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCost_Code).Value)) > 0 Then
                        'objtr.Document_Code = clsCommon.myCstr()
                        objtr.Overheadcost = clsCommon.myCdbl(grow.Cells(ColOverHeadCost).Value)
                        objtr.SNO = clsCommon.myCdbl(grow.Cells(colCostLineNo).Value)
                        objtr.COST_CODE = clsCommon.myCstr(grow.Cells(colCost_Code).Value)
                        objtr.HCODE = clsCommon.myCstr(txtCostGroup.Value)
                        If isIncludeRatePerHoursIn = True Then
                            objtr.BomRatePerHour = clsCommon.myCdbl(grow.Cells(colBomRatePerHour).Value)
                            objtr.BomHours = clsCommon.myCdbl(grow.Cells(colBomHours).Value)
                            objtr.BomNO = clsCommon.myCdbl(grow.Cells(colBomNO).Value)
                        End If
                        obj.OverHeadCost = obj.OverHeadCost + clsCommon.myCdbl(grow.Cells(ColOverHeadCost).Value)
                        If clsCommon.myLen(objtr.HCODE) > 0 Then
                            obj.ObjCostGroupDetail.Add(objtr)
                        End If
                    End If
                Next
                '-------------------------------------------------------------

                If clsBOM.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    txtCode.Value = obj.BOM_CODE
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    btnReverse.Enabled = False

                    UcAttachment1.SaveData(txtCode.Value)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Function AllowToSave() As Boolean
        Try

            If AllowFutureDateTransaction(dtpBOMDate.Value, Nothing) = False Then
                Return False
            End If
            If chkOSP_JW.Checked AndAlso clsCommon.myLen(txtVendorCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Vendor for OSP Job-Work.", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtVendorCode.Focus()
                txtVendorCode.Select()
                Errorcontrol.SetError(txtvendorName, "Select Vendor for OSP Job-Work.")
                Return False
            Else
                Errorcontrol.ResetError(txtvendorName)
            End If

            If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Production Category Detail", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                fndItemCategory.Focus()
                fndItemCategory.Select()
                Errorcontrol.SetError(TxtCategory, "Select Production Category Detail")
                Return False
            Else
                Errorcontrol.ResetError(TxtCategory)
            End If

            If clsCommon.myLen(txtProducedItem.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Main Item Detail", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtProducedItem.Focus()
                txtProducedItem.Select()
                Errorcontrol.SetError(lblMasterItemName, "Select Main Item Detail")
                Return False
            Else
                Errorcontrol.ResetError(lblMasterItemName)
            End If

            If clsCommon.CompairString(TxtitemType.SelectedValue, "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Select Main Item Type", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                TxtitemType.Select()
                Errorcontrol.SetError(TxtitemType, "Select Main Item Type")
                Return False
            Else
                Errorcontrol.ResetError(TxtitemType)
            End If

            If clsCommon.myLen(txtUomCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Fill Unit", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtUomCode.Focus()
                txtUomCode.Select()
                Errorcontrol.SetError(txtuomname, "Fill Unit")
                Return False
            Else
                Errorcontrol.ResetError(txtuomname)
            End If

            If clsCommon.myCdbl(txtBuildQty.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Fill Build Qty", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtBuildQty.Focus()
                txtBuildQty.Select()
                Errorcontrol.SetError(txtBuildQty, "Fill Build Qty")
                Return False
            Else
                Errorcontrol.ResetError(txtBuildQty)
            End If

            'If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
            '    myMessages.blankValue("Item Category")
            '    fndItemCategory.Focus()
            '    Return False
            'End If

            If clsCommon.myLen(txtvalid.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill BOM valid from date", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtvalid.Focus()
                txtvalid.Select()
                Errorcontrol.SetError(txtvalid, "Please fill BOM valid from date")
                Return False
            Else
                Errorcontrol.ResetError(txtvalid)
            End If

            If clsCommon.myLen(txtvalidupto.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill BOM valid upto date", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtvalidupto.Focus()
                txtvalidupto.Select()
                Errorcontrol.SetError(txtvalidupto, "Please fill BOM valid upto date")
                Return False
            Else
                Errorcontrol.ResetError(txtvalidupto)
            End If

            If clsCommon.myCDate(txtvalidupto.Text) < clsCommon.myCDate(txtvalid.Text) Then
                clsCommon.MyMessageBoxShow(Me, "BOM valid upto date should be greater than from date", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                txtvalidupto.Focus()
                txtvalidupto.Select()
                Errorcontrol.SetError(txtvalidupto, "BOM valid upto date should be greater than from date")
                Return False
            Else
                Errorcontrol.ResetError(txtvalidupto)
            End If
            '------------------------------------------------------------
            'Dim qry As String = "select count(*) from tspl_pp_bom_head where isnull(bom_code,'')<>'" + txtCode.Value + "' and prod_item_code='" + txtProducedItem.Value + "' and prod_item_unit_code='" + txtUomCode.Value + "' and prod_quantity='" + txtBuildQty.Text + "' and valid_upto_date >= '" + clsCommon.GetPrintDate(txtvalid.Text, "dd/MMM/yyyy") + "'" '((valid_from_date between '" + clsCommon.GetPrintDate(txtvalid.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtvalidupto.Text, "dd/MMM/yyyy") + "') or (valid_upto_date between '" + clsCommon.GetPrintDate(txtvalid.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtvalidupto.Text, "dd/MMM/yyyy") + "'))
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            'If check > 0 Then
            '    clsCommon.MyMessageBoxShow("BOM of same item with same build quantity and unit is already exist," + Environment.NewLine + "Please do changes in exist record.", Me.Text)
            '    RadPageView.SelectedPage = RadPageItemDetails
            '    txtProducedItem.Focus()
            '    txtProducedItem.Select()
            '    Errorcontrol.SetError(txtProducedItem, "BOM of same item with same build quantity and unit is already exist," + Environment.NewLine + "Please do changes in exist record.")
            '    Return False
            'Else
            '    Errorcontrol.ResetError(txtProducedItem)
            'End If

            If clsCommon.myLen(cboBOMStatus.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Select BOM Status", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                cboBOMStatus.Focus()
                cboBOMStatus.Select()
                Errorcontrol.SetError(cboBOMStatus, "Select BOM Status")
                Return False
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If
            If clsCommon.CompairString(ActivateProductionWithoutBatch, "0") = CompairStringResult.Equal Then
                If clsCommon.myLen(fndSection.Value) <= 0 AndAlso Not chkOSP_JW.Checked Then
                    RadPageView.SelectedPage = RadPageViewSectionDetail
                    clsCommon.MyMessageBoxShow(Me, "Select Section Detail", Me.Text)
                    fndSection.Focus()
                    fndSection.Select()
                    Errorcontrol.SetError(TxtSection, "Select Section Detail")
                    Return False
                Else
                    Errorcontrol.ResetError(TxtSection)
                End If
            End If

            If clsCommon.myLen(txtByproductItem.Value) > 0 OrElse clsCommon.myLen(txtByproductUOM.Value) > 0 OrElse txtByproductQty.Value > 0 Then
                If clsCommon.myLen(txtByproductItem.Value) <= 0 Then
                    txtByproductItem.Focus()
                    Throw New Exception("Please provide byproduct Item code")
                End If
                If clsCommon.myLen(txtByproductUOM.Value) <= 0 Then
                    txtByproductUOM.Focus()
                    Throw New Exception("Please provide byproduct Item UOM")
                End If
                If txtByproductQty.Value <= 0 Then
                    txtByproductQty.Focus()
                    Throw New Exception("Please provide byproduct Item qty")
                End If
            End If



            '----------------------item--------------------------------------
            Dim icode As String = ""
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim snf As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing
            Dim snf_kg As Decimal = Nothing
            Dim alticode As String = ""
            Dim oldicode As String = ""
            Dim fractnvalue As Decimal = Nothing
            Dim deactive As Integer = 0
            Dim effctvdate As Date = Nothing
            Dim arrIcode As List(Of String) = Nothing

            Dim dclDetailTotFATKg As Decimal = 0
            Dim dclDetailTotSNFKg As Decimal = 0


            Dim UOM As String = ""
            Dim OldUOM As String = ""
            arrIcode = New List(Of String)

            For ii As Integer = 0 To gvBOM.Rows.Count - 1
                gvBOM.Focus()
                gvBOM.Select()

                icode = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value)
                UOM = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colUnitCode).Value)
                qty = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colqty).Value)
                fat = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT).Value)
                snf = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF).Value)
                fat_kg = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT_KG).Value)
                snf_kg = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF_KG).Value)
                alticode = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colAltritemcode).Value)
                deactive = CInt(IIf(clsCommon.myCBool(gvBOM.Rows(ii).Cells(colDeactive).Value) = True, 1, 0))

                If Not arrIcode.Contains(icode) AndAlso clsCommon.myLen(icode) > 0 Then
                    arrIcode.Add(icode)
                End If

                If deactive = 1 Then
                    Try
                        Convert.ToDateTime(gvBOM.Rows(ii).Cells(colEffectiveDate).Value)
                    Catch exx As Exception
                        RadPageView.SelectedPage = RadPageItemDetails
                        gvBOM.CurrentRow = gvBOM.Rows(ii)
                        gvBOM.CurrentColumn = gvBOM.Columns(colEffectiveDate)
                        Throw New Exception("Fill effective date at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End Try

                    If clsCommon.myCDate(gvBOM.Rows(ii).Cells(colEffectiveDate).Value) < clsCommon.myCDate(clsCommon.GETSERVERDATE(Nothing)) Then
                        Throw New Exception("Effective date should be greater than or equal to current date at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                End If
                ''richa agarwal BHA/12/07/18-000147 snf and fat colum should be greater than 0 in case of milk item
                If clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT).Value) < 0 Then
                    Throw New Exception("Fat% can't be -ve or less than 0 at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
                If clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF).Value) < 0 Then
                    Throw New Exception("SNF% can't be -ve or less than 0 at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
                ''-----------
                If clsCommon.myLen(icode) > 0 Then
                    If qty <= 0 Then
                        RadPageView.SelectedPage = RadPageItemDetails
                        clsCommon.MyMessageBoxShow("Fill Std. Quantity at row no. " + clsCommon.myCstr(ii + 1) + "", Me.Text)

                        gvBOM.CurrentRow = gvBOM.Rows(ii)
                        gvBOM.CurrentColumn = gvBOM.Columns(colqty)
                        Errorcontrol.SetError(gvBOM, "Fill Std. Quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                        Return False
                    Else
                        Errorcontrol.ResetError(gvBOM)
                    End If

                    ''richa agarwal BHA/12/07/18-000147 snf and fat column should not be calculated again in case of milk becuase field is editable on screen
                    If clsCommon.CompairString(clsCommon.myCstr(gvBOM.Rows(ii).Cells(colProductType).Value), "Milk") <> CompairStringResult.Equal Then
                        gvBOM.Rows(ii).Cells(colFAT).Value = clsCommon.myCdbl(clsBOM.GetFAT_PERS(icode))
                        gvBOM.Rows(ii).Cells(colSNF).Value = clsCommon.myCdbl(clsBOM.GetSNF_PERS(icode))
                        gvBOM.Rows(ii).Cells(colFAT_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(icode, UOM, qty, clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT).Value), Nothing) ' System.Math.Round((qty * clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT).Value)) / 100, 2)
                        gvBOM.Rows(ii).Cells(colSNF_KG).Value = clsBOM.GetFatSNFKG_AfterConversion(icode, UOM, qty, clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF).Value), Nothing) 'System.Math.Round((qty * clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF).Value)) / 100, 2)
                    End If

                    dclDetailTotFATKg += clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colFAT_KG).Value)
                    dclDetailTotSNFKg += clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colSNF_KG).Value)
                    If clsCommon.CompairString(icode, alticode) = CompairStringResult.Equal Then
                        RadPageView.SelectedPage = RadPageItemDetails
                        clsCommon.MyMessageBoxShow("Duplicate alternate item not allowed at row no. " + clsCommon.myCstr(ii + 1) + "", Me.Text)

                        gvBOM.CurrentRow = gvBOM.Rows(ii)
                        gvBOM.CurrentColumn = gvBOM.Columns(colItemCode)
                        Errorcontrol.SetError(gvBOM, "Duplicate alternate item not allowed at row no. " + clsCommon.myCstr(ii + 1) + "")
                        Return False
                    Else
                        Errorcontrol.ResetError(gvBOM)
                    End If



                    For jj As Integer = ii + 1 To gvBOM.Rows.Count - 1
                        oldicode = clsCommon.myCstr(gvBOM.Rows(jj).Cells(colItemCode).Value)
                        OldUOM = clsCommon.myCstr(gvBOM.Rows(jj).Cells(colUnitCode).Value)

                        If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(UOM, OldUOM) = CompairStringResult.Equal Then
                            RadPageView.SelectedPage = RadPageItemDetails
                            clsCommon.MyMessageBoxShow("Same item with same unit not allowed at row no. " + clsCommon.myCstr(jj + 1) + "", Me.Text)

                            gvBOM.CurrentRow = gvBOM.Rows(jj)
                            gvBOM.CurrentColumn = gvBOM.Columns(colUnitCode)
                            Errorcontrol.SetError(gvBOM, "Same item with same unit not allowed at row no. " + clsCommon.myCstr(jj + 1) + "")
                            Return False
                        Else
                            Errorcontrol.ResetError(gvBOM)
                        End If
                    Next
                End If
            Next


            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView.SelectedPage = RadPageItemDetails
                clsCommon.MyMessageBoxShow(Me, "Fill atleast one row of item detail", Me.Text)
                gvBOM.Focus()
                gvBOM.Select()
                Errorcontrol.SetError(gvBOM, "Fill atleast one row of item detail")
                Return False
            Else
                Errorcontrol.ResetError(gvBOM)
            End If

            Dim dclMainTotFATKg As Decimal = clsBOM.GetFatSNFKG_AfterConversion(txtProducedItem.Value, txtUomCode.Value, txtBuildQty.Value, clsBOM.GetFAT_PERS(txtProducedItem.Value), Nothing)
            Dim dclMainTotSNFKg As Decimal = clsBOM.GetFatSNFKG_AfterConversion(txtProducedItem.Value, txtUomCode.Value, txtBuildQty.Value, clsBOM.GetSNF_PERS(txtProducedItem.Value), Nothing)

            If dclMainTotFATKg > dclDetailTotFATKg Then
                Throw New Exception("Main Item FAT KG(" + clsCommon.myCstr(dclMainTotFATKg) + ") can't be more than Detail Item FAT KG (" + clsCommon.myCstr(dclDetailTotFATKg) + ")")
            End If
            If dclMainTotSNFKg > dclDetailTotSNFKg Then
                Throw New Exception("Main Item SNF KG(" + clsCommon.myCstr(dclMainTotSNFKg) + ") can't be more than Detail Item SNF KG (" + clsCommon.myCstr(dclDetailTotSNFKg) + ")")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Function PostAllowToSave() As Boolean
        Try
            '--------------stage----------------------------------------
            Dim stagecode As String = ""
            Dim oldstagecode As String = ""
            Dim arrStage As New List(Of String)

            gvStages.Rows.AddNew()

            AutoFillStaged() ' at every post stage detail refersh Ref By. Shyam

            Dim seqno As Integer = 0
            Dim oldseq As Integer = 0
            For ii As Integer = 0 To gvStages.Rows.Count - 1
                gvStages.Focus()
                gvStages.Select()

                stagecode = clsCommon.myCstr(gvStages.Rows(ii).Cells(colStageCode).Value)
                seqno = CInt(gvStages.Rows(ii).Cells(colStageSequence).Value)

                'If clsCommon.myLen(stagecode) > 0 AndAlso seqno <= 0 Then
                '    RadPageView.SelectedPage = RadPageViewSectionDetail
                '    clsCommon.MyMessageBoxShow("Fill stage sequence no at row no. " + clsCommon.myCstr(ii + 1) + "", Me.Text)
                '    Errorcontrol.SetError(gvStages, "Fill stage sequence no at row no. " + clsCommon.myCstr(ii + 1) + "")
                '    Return False
                'Else
                '    Errorcontrol.ResetError(gvStages)
                'End If

                If clsCommon.myLen(stagecode) > 0 AndAlso Not arrStage.Contains(stagecode) Then
                    arrStage.Add(stagecode)
                End If

                For jj As Integer = ii + 1 To gvStages.Rows.Count - 1
                    oldstagecode = clsCommon.myCstr(gvStages.Rows(jj).Cells(colStageCode).Value)
                    oldseq = CInt(gvStages.Rows(jj).Cells(colStageSequence).Value)
                    If clsCommon.myLen(stagecode) > 0 AndAlso clsCommon.CompairString(stagecode, oldstagecode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(seqno, oldseq) = CompairStringResult.Equal Then
                        RadPageView.SelectedPage = RadPageViewSectionDetail
                        clsCommon.MyMessageBoxShow("Duplicate Stage Not Allowed At Row No. " + clsCommon.myCstr(jj + 1) + "", Me.Text)

                        gvStages.CurrentRow = gvStages.Rows(jj)
                        gvStages.CurrentColumn = gvStages.Columns(colStageCode)
                        Errorcontrol.SetError(gvStages, "Duplicate Stage Not Allowed At Row No. " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    Else
                        Errorcontrol.ResetError(gvStages)
                    End If

                    If clsCommon.myLen(stagecode) > 0 AndAlso (seqno) > 0 AndAlso clsCommon.CompairString(seqno, oldseq) = CompairStringResult.Equal Then
                        RadPageView.SelectedPage = RadPageViewSectionDetail
                        clsCommon.MyMessageBoxShow("Duplicate Sequences Not Allowed At Row No. " + clsCommon.myCstr(jj + 1) + "", Me.Text)

                        gvStages.CurrentRow = gvStages.Rows(jj)
                        gvStages.CurrentColumn = gvStages.Columns(colStageSequence)
                        Errorcontrol.SetError(gvStages, "Duplicate Sequences Not Allowed At Row No. " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    Else
                        Errorcontrol.ResetError(gvStages)
                    End If
                Next
            Next
            If clsCommon.CompairString(ActivateProductionWithoutBatch, "0") = CompairStringResult.Equal Then
                If (arrStage Is Nothing OrElse arrStage.Count <= 0) AndAlso Not chkOSP_JW.Checked Then
                    RadPageView.SelectedPage = RadPageViewSectionDetail
                    clsCommon.MyMessageBoxShow(Me, "Do mapping of section with stages.", Me.Text)
                    gvStages.Focus()
                    gvStages.Select()
                    Errorcontrol.SetError(gvStages, "Do mapping of section with stages.")
                    Return False
                Else
                    Errorcontrol.ResetError(gvStages)
                End If
            End If


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Select BOM Code for deletion", Me.Text)
            txtCode.Focus()
            txtCode.Select()
            Errorcontrol.SetError(txtCode, "Select BOM code for deletion.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtCode)
        End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If myMessages.deleteConfirm() Then
                If (clsBOM.DeleteData(txtCode.Value, fndSection.Value)) Then
                    myMessages.delete()
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtMasterItem__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtProducedItem._MYValidating
        Try
            If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
                Errorcontrol.SetError(TxtCategory, "Please select Production Cateogry first.")
                fndItemCategory.Focus()
                fndItemCategory.Select()
                Throw New Exception("Please select Production Cateogry first.")
            Else
                Errorcontrol.ResetError(TxtCategory)
            End If

            Dim whrcls As String = " Structure_Code='" + fndItemCategory.Value + "'"

            If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
                whrcls += " and item_type in ('" + TxtitemType.SelectedValue + "') "
            End If
            'If N_Level = True Then
            '    whrcls += " item_code in ('" + fndItemCategory.Value + "')"
            'Else
            '    whrcls += " item_category='" + fndItemCategory.Value + "' and Sub_item_category='" + txtsubcatcode.Text + "'"
            'End If
            txtProducedItem.Value = clsItemMaster.getFinder(whrcls, txtProducedItem.Value, isButtonClicked)

            If clsCommon.myLen(txtProducedItem.Value) > 0 Then
                TxtitemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + txtProducedItem.Value + "'"))
                'TxtitemType.Text = ItemType(TxtitemType.Text)
                lblMasterItemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + txtProducedItem.Value + "'"))
                txtUomCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + txtProducedItem.Value + "'"))
                txtuomname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + txtUomCode.Value + "'"))
                txtbuildUnit.Text = txtuomname.Text
            Else
                TxtitemType.SelectedValue = ""
                lblMasterItemName.Text = ""
                txtUomCode.Value = ""
                txtuomname.Text = ""
                txtbuildUnit.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GenerateRevisionNo()
        Dim qry As String = "select max(revision_no) from TSPL_PP_BOM_HEAD where bom_code='" + txtCode.Value + "'" ' and prod_item_code='" + txtProducedItem.Value + "'
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If clsCommon.myLen(str) > 0 Then
            txtrevisionno.Text = clsCommon.incval(str, 0, 1, True)
        Else
            txtrevisionno.Text = txtCode.Value + ".1"
        End If
    End Sub

    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_PP_BOM_Head where BOM_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no <= 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsBOM.GetBOMFinder("", txtCode.Value, isButtonClicked))
            If clsCommon.myLen(txtCode.Value) > 0 Then
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
            If clsCommon.CompairString(cboBOMStatus.Text, "Approved") <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Bom status must be Approved.", Me.Text)
                RadPageView.SelectedPage = RadPageItemDetails
                cboBOMStatus.Select()
                Errorcontrol.SetError(cboBOMStatus, "Bom status must be Approved.")
                Exit Sub
            Else
                Errorcontrol.ResetError(cboBOMStatus)
            End If
            If clsCommon.myCDate(txtvalidupto.Text) <= clsCommon.myCDate(txtvalid.Text) Then
                If clsCommon.MyMessageBoxShow("Do you want to change Valid Upto date?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    RadPageView.SelectedPage = RadPageItemDetails
                    txtvalidupto.Focus()
                    txtvalidupto.Select()
                    Errorcontrol.SetError(txtvalidupto, "Change date.")
                    Exit Sub
                Else
                    Errorcontrol.ResetError(txtvalidupto)
                End If
            End If
            If (myMessages.postConfirm()) Then
                If PostAllowToSave() Then
                    If AllowToSave() Then
                        SavingData(True)
                        If (clsBOM.PostData(txtCode.Value, True)) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                            LoadData(txtCode.Value, NavigatorType.Current)
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save(ChekBtnPost)) Then

        End If
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub gvBOM_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvBOM.CellDoubleClick
        Try
            If e.Row.Index >= 0 Then
                If gvBOM.CurrentColumn Is gvBOM.Columns(colBOMRecordShow) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colBOMRecordShow).Value), "Record Exist") = CompairStringResult.Equal Then
                        Dim whrcls As String = ""
                        Dim bom_code As String = clsBOM.GetBOMCodeOnDoubleClick(txtCode.Value, clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value))

                        If clsCommon.myLen(bom_code) > 0 AndAlso bom_code.Substring(0, 2) = ",'" Then
                            bom_code = bom_code.Substring(1, bom_code.Length - 1)
                        End If

                        whrcls = " tspl_pp_bom_head.bom_code in (" + bom_code + ") "

                        bom_code = clsBOM.GetBOMFinder(whrcls, Nothing, True)

                        Dim frm As New frmBOM()
                        frm.SetUserMgmt(clsUserMgtCode.frmBillOfMaterialDairy)
                        frm.strBOMCodeForOpen = bom_code
                        frm.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvBOM_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvBOM.CurrentColumnChanged
        If gvBOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBOM.CurrentRow.Index
            gvBOM.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvBOM.Rows.Count - 1 Then
                gvBOM.Rows.AddNew()
                gvBOM.CurrentRow = gvBOM.Rows(intCurrRow)
                gvBOM.CurrentRow.Cells(colBOMRecordShow).Value = "No Record"
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
    Private Sub funPrint()
        Try
            Dim qry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_PP_BOM_HEAD.BOM_CODE,convert(varchar,TSPL_PP_BOM_HEAD.BOM_DATE,103) as BOM_DATE,TSPL_PP_BOM_HEAD.Description,TSPL_PP_BOM_HEAD.ITEM_CATEGORY_CODE,TSPL_STRUCTURE_MASTER.Structure_Descq,TSPL_PP_BOM_HEAD.Section_Code,TSPL_SECTION_MASTER.Description as sectionname,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_HEAD.PROD_QUANTITY,TSPL_PP_BOM_HEAD.Revision_No,convert(varchar,TSPL_PP_BOM_HEAD.Valid_FROM_DATE,103) as Valid_FROM_DATE,convert(varchar,TSPL_PP_BOM_HEAD.Valid_UPTO_DATE,103) as Valid_UPTO_DATE,TSPL_PP_BOM_ITEM_DETAIL.LINE_NO,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,item.Item_Desc as rawitem,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.FAT,TSPL_PP_BOM_ITEM_DETAIL.FAT_KG,TSPL_PP_BOM_ITEM_DETAIL.SNF,TSPL_PP_BOM_ITEM_DETAIL.SNF_KG,TSPL_PP_BOM_ITEM_DETAIL.Deactive,convert(varchar,TSPL_PP_BOM_ITEM_DETAIL.Effective_Date,103) as Effective_Date "
            qry += "from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_ITEM_MASTER item on item.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_PP_BOM_HEAD.ITEM_CATEGORY_CODE left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code=TSPL_PP_BOM_HEAD.Section_Code "
            qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_PP_BOM_HEAD.comp_code "

            If clsCommon.myLen(txtCode.Value) > 0 Then
                qry += " where TSPL_PP_BOM_HEAD.BOM_CODE='" + clsCommon.myCstr(txtCode.Value) + "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            '=sub report=============
            Dim qry1 As String = "select TSPL_PP_BOM_STAGE_DETAIL.BOM_CODE,TSPL_PP_BOM_STAGE_DETAIL.Section_Code,TSPL_PP_BOM_STAGE_DETAIL.Stage_Code,TSPL_STAGE_MASTER.Description,TSPL_PP_BOM_STAGE_DETAIL.Sequence from TSPL_PP_BOM_STAGE_DETAIL left outer join TSPL_STAGE_MASTER on TSPL_STAGE_MASTER.Stage_Code=TSPL_PP_BOM_STAGE_DETAIL.Stage_Code "
            If clsCommon.myLen(txtCode.Value) > 0 Then
                qry1 += " where TSPL_PP_BOM_STAGE_DETAIL.BOM_CODE='" + clsCommon.myCstr(txtCode.Value) + "' "
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptPPBOMPrint", "Bill Of Material")
                frmCRV = Nothing
                'ProductionReportViewer.funsubreport(qry, qry1, "crptPPBOMPrint", "Bill Of Material", "crptPPBOMSUBPrint.rpt")
                'PurchaseOrderViewer.funsubreport(qry, qry1, "crptPPBOMPrint", "Bill Of Material", "crptPPBOMSUBPrint.rpt")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function CheckNLevelCat() As String
        Dim qry As String = "select IsNLevelCatForItem from TSPL_INV_PARAMETERS where IsNLevelCatForItem='1'"
        Dim strValue As String = ""
        strValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        Return strValue
    End Function

    Private Sub fndItemCategory__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndItemCategory._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER"
            fndItemCategory.Value = clsCommon.ShowSelectForm("PPSTRFND", qry, "Code", "", fndItemCategory.Value, "Code", isButtonClicked)
            TxtCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + fndItemCategory.Value + "'"))
            txtsubcat_name.Text = ""
            txtsubcatcode.Text = ""
            txtProducedItem.Value = ""
            TxtitemType.SelectedValue = ""
            lblMasterItemName.Text = ""
            txtUomCode.Value = ""
            txtuomname.Text = ""
            txtbuildUnit.Text = ""
            txtBuildQty.Text = Nothing
            ''ERO/15/03/19-000514 by balwinder on 08/04/2019  remove txtrevisionno.Text = ""
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndSection__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSection._MYValidating
        Try
            If clsCommon.myLen(fndItemCategory.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Production Category First.", Me.Text)
                fndItemCategory.Focus()
                fndItemCategory.Select()
                Errorcontrol.SetError(TxtCategory, "Select Production Category First.")
                Return
            Else
                Errorcontrol.ResetError(TxtCategory)
            End If

            Dim qry As String = "select Section_Code as Code,Description as Name from TSPL_SECTION_MASTER"
            fndSection.Value = clsCommon.ShowSelectForm("SECFND", qry, "Code", " ", fndSection.Value, "", isButtonClicked) 'section_code in (select section_code from TSPL_SECTION_STAGE_MAPPING_HEAD where structure_code='" + fndItemCategory.Value + "')

            gvStages.Rows.Clear()
            If clsCommon.myLen(fndSection.Value) > 0 Then
                TxtSection.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SECTION_MASTER where Section_Code='" + fndSection.Value + "'"))
                AutoFillStaged()
            Else
                TxtSection.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub AutoFillStaged()
        Try
            Dim qry As String = "select TSPL_SECTION_STAGE_MAPPING.Stage_Code as Code,TSPL_STAGE_MASTER.Description,TSPL_SECTION_STAGE_MAPPING.Sequence_No as [Sequence No],TSPL_PP_BOM_STAGE_DETAIL.AR_Item_Code,TSPL_PP_BOM_STAGE_DETAIL.Bi_Prod " & _
                " from TSPL_SECTION_STAGE_MAPPING left outer join TSPL_STAGE_MASTER on TSPL_STAGE_MASTER.Stage_Code=TSPL_SECTION_STAGE_MAPPING.Stage_Code " & _
                " left join (select * from TSPL_PP_BOM_STAGE_DETAIL where BOM_CODE='" & txtCode.Value & "') TSPL_PP_BOM_STAGE_DETAIL on TSPL_SECTION_STAGE_MAPPING.Section_Code=TSPL_PP_BOM_STAGE_DETAIL.Section_Code " & _
                " and TSPL_SECTION_STAGE_MAPPING.Stage_Code=TSPL_PP_BOM_STAGE_DETAIL.Stage_Code where TSPL_SECTION_STAGE_MAPPING.Section_Code in " & _
                " (select section_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + fndSection.Value + "' and Structure_Code='" + fndItemCategory.Value + "') " & _
                " and TSPL_SECTION_STAGE_MAPPING.doc_code in (select doc_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + fndSection.Value + "' " & _
                " and Structure_Code='" + fndItemCategory.Value + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gvStages.Rows.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvStages.Rows.AddNew()
                    gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageCode).Value = clsCommon.myCstr(dr("Code"))
                    gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageDesc).Value = clsCommon.myCstr(dr("Description"))
                    gvStages.Rows(gvStages.Rows.Count - 1).Cells(colStageSequence).Value = clsCommon.myCdbl(dr("Sequence No"))
                    gvStages.Rows(gvStages.Rows.Count - 1).Cells(colAR_Item_Code).Value = clsCommon.myCstr(dr("AR_Item_Code"))
                    gvStages.Rows(gvStages.Rows.Count - 1).Cells(colBi_Prod).Value = clsCommon.myCstr(dr("Bi_Prod"))
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub grdStages_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvStages.CellValueChanged
        Try
            If clsCommon.myLen(fndSection.Value) <= 0 Then
                Throw New Exception("First select section detail")
            End If

            If gvStages.CurrentRow Is Nothing Then
                Exit Sub
            End If

            If Not IsinsideLoaddata Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvStages.Columns(colStageCode) Then
                        'OpenStage(False)
                    End If
                End If

                If e.Column Is gvStages.Columns(colAR_Item_Code) Then
                    isCellValueChangedOpen = True
                    gvStages.CurrentRow.Cells(colAR_Item_Code).Value = clsItemMaster.getFinder("", gvStages.CurrentRow.Cells(colAR_Item_Code).Value, False)
                    isCellValueChangedOpen = False
                End If

                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenStage(ByVal isButtonCLicked As Boolean)
        Dim qry As String = "select TSPL_SECTION_STAGE_MAPPING.Stage_Code as Code,TSPL_STAGE_MASTER.Description,TSPL_SECTION_STAGE_MAPPING.Sequence_No as [Sequence No] from TSPL_SECTION_STAGE_MAPPING left outer join TSPL_STAGE_MASTER on TSPL_STAGE_MASTER.Stage_Code=TSPL_SECTION_STAGE_MAPPING.Stage_Code where TSPL_SECTION_STAGE_MAPPING.Section_Code in (select section_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + fndSection.Value + "' and Structure_Code='" + fndItemCategory.Value + "') and TSPL_SECTION_STAGE_MAPPING.doc_code in (select doc_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + fndSection.Value + "' and Structure_Code='" + fndItemCategory.Value + "')"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("STGFND", qry)


        If dr IsNot Nothing Then
            gvStages.CurrentRow.Cells(colStageCode).Value = clsCommon.myCstr(dr("Code"))
            gvStages.CurrentRow.Cells(colStageDesc).Value = clsCommon.myCstr(dr("Description"))
            gvStages.CurrentRow.Cells(colStageSequence).Value = CInt(dr("Sequence No"))
        Else
            gvStages.CurrentRow.Cells(colStageCode).Value = ""
            gvStages.CurrentRow.Cells(colStageDesc).Value = ""
            gvStages.CurrentRow.Cells(colStageSequence).Value = Nothing
        End If
    End Sub

    Sub OpenItemType(ByVal isButtonClicked As Boolean)
        ''richa BHA/13/07/18-000156 pick item type from item master
        'Dim qry As String = "select distinct item_type as Code,(case when item_type='R' then 'Raw Material' else case when item_type='F' then 'Finished Good' else case when item_type='S' then 'Semi Finished Good' else case when item_type='A' then 'Asset' else case when item_type='O' then 'Other' else '' end end end end end) as Name from tspl_item_master"
        Dim qry As String = " SELECT '' AS Code,'Select' as Name union SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER "
        gvBOM.CurrentRow.Cells(colItemType).Value = clsCommon.ShowSelectForm("PPTYPEFND", qry, "Name", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemType).Value), "Code", isButtonClicked)
    End Sub

    Private Sub gvBOM_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellValueChanged
        Try
            If Not IsinsideLoaddata Then
                If gvBOM.CurrentRow Is Nothing Then
                    Exit Sub
                End If

                If Not isCellValueChangedOpen Then

                    If e.Column Is gvBOM.Columns(colItemCode) Then
                        isCellValueChangedOpen = True
                        OpenIcode(False, "")
                        Cal_FAT()
                        Cal_SNF()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gvBOM.Columns(colItemType) Then
                        isCellValueChangedOpen = True
                        OpenItemType(False)
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gvBOM.Columns(colUnitCode) Then
                        isCellValueChangedOpen = True
                        OpenUom(False, "")
                        Cal_FAT()
                        Cal_SNF()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gvBOM.Columns(colAltritemcode) Then
                        isCellValueChangedOpen = True
                        OpenIcode(False, "Alt")
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gvBOM.Columns(colAltrItemUom) Then
                        isCellValueChangedOpen = True
                        OpenUom(False, "Alt")
                        isCellValueChangedOpen = False
                    End If

                    If (e.Column Is gvBOM.Columns(colqty)) Or (e.Column Is gvBOM.Columns(colFAT)) Or (e.Column Is gvBOM.Columns(colFAT_KG)) Then
                        isCellValueChangedOpen = True
                        Cal_FAT()
                        isCellValueChangedOpen = False
                    End If

                    If (e.Column Is gvBOM.Columns(colqty)) Or (e.Column Is gvBOM.Columns(colSNF)) Or (e.Column Is gvBOM.Columns(colSNF_KG)) Then
                        isCellValueChangedOpen = True
                        Cal_SNF()
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gvBOM.Columns(colDeactive) Then
                        isCellValueChangedOpen = True
                        If clsCommon.myCBool(gvBOM.CurrentRow.Cells(colDeactive).Value) = True Then
                            gvBOM.CurrentRow.Cells(colEffectiveDate).ReadOnly = False
                        Else
                            gvBOM.CurrentRow.Cells(colEffectiveDate).ReadOnly = True
                        End If
                        isCellValueChangedOpen = False
                    End If
                    ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
                    If (e.Column Is gvBOM.Columns(colqty)) Or (e.Column Is gvBOM.Columns(colProcessLossPer)) Then
                        isCellValueChangedOpen = True
                        Cal_ProcessLossQty()
                        isCellValueChangedOpen = False
                    End If
                    ''------------
                    If e.Column Is gvBOM.Columns(colItemCode) Then
                        isCellValueChangedOpen = True
                        If clsCommon.myCstr(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value) <> "Milk" Then
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT).ReadOnly = True
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF).Value = 0
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT).Value = 0
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF_KG).ReadOnly = True
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT_KG).ReadOnly = True
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNF_KG).Value = 0
                            'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFAT_KG).Value = 0
                        End If
                        isCellValueChangedOpen = False
                    End If
                End If
                'isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colFAT).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value), qty, fat, Nothing) '(qty * fat) / 100
            gvBOM.CurrentRow.Cells(colFAT_KG).Value = Math.Round(fat_kg, DecimalPoint)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''richa agarwal ERO/29/06/18-000364 on 3 July,2018
    Private Sub Cal_ProcessLossQty()
        Try
            Dim qty As Decimal = Nothing
            Dim ProcessLossPer As Decimal = Nothing
            Dim ProcessLossQty As Decimal = Nothing

            qty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)
            ProcessLossPer = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colProcessLossPer).Value)

            ProcessLossQty = (qty * ProcessLossPer) / 100
            gvBOM.CurrentRow.Cells(colProcessLossQty).Value = Math.Round(ProcessLossQty, DecimalPoint)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub Cal_SNF()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colSNF).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value), qty, fat, Nothing) '(qty * fat) / 100
            gvBOM.CurrentRow.Cells(colSNF_KG).Value = Math.Round(fat_kg, DecimalPoint)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenUom(ByVal isButtonClicked As Boolean, ByVal strType As String)
        Dim icode As String = ""
        Dim uomcode As String = ""
        If clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
            icode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colAltritemcode).Value)
            uomcode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colAltrItemUom).Value)
        Else
            icode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value)
            uomcode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value)
        End If

        Dim qry As String = "select UOM_Code as Code,UOM_Description as Description,Conversion_Factor as [Conversion Factor],Stocking_Unit as [Stocking Unit],Weight from TSPL_ITEM_UOM_DETAIL "
        'qry += " where item_code='" + icode + "'"
        uomcode = clsCommon.ShowSelectForm("PPUOMFND", qry, "Code", " item_code='" + icode + "'", uomcode, "Code", isButtonClicked)

        If clsCommon.myLen(uomcode) > 0 Then
            If clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colAltrItemUom).Value = uomcode
            ElseIf Not clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colUnitCode).Value = uomcode
            End If
        Else
            If clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colAltrItemUom).Value = ""
            ElseIf Not clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colUnitCode).Value = ""
            End If
        End If
    End Sub

    Sub OpenIcode(ByVal isButtonCLicked As Boolean, ByVal strType As String)
        If clsCommon.myLen(gvBOM.CurrentRow.Cells(colItemType).Value) <= 0 Then
            Throw New Exception("Select item type first.")
        End If
        Dim itemtyp As String = clsBOM.GetItemTypeCode(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemType).Value))

        Dim whrcls As String = " tspl_item_master.item_type in ('" + itemtyp + "') and tspl_item_master.Active='1' " 'and tspl_item_master.item_code<>'" + clsCommon.myCstr(txtProducedItem.Value) + "'
        If clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
            whrcls += " and tspl_item_master.item_code not in ('" + clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value) + "')"
        Else
            'whrcls = ""
        End If

        'gvBOM.CurrentRow.Cells(colSNF).ReadOnly = False
        'gvBOM.CurrentRow.Cells(colFAT).ReadOnly = False
        'gvBOM.CurrentRow.Cells(colSNF_KG).ReadOnly = False
        'gvBOM.CurrentRow.Cells(colFAT_KG).ReadOnly = False
        gvBOM.CurrentRow.Cells(colSNF).Value = 0
        gvBOM.CurrentRow.Cells(colFAT).Value = 0
        gvBOM.CurrentRow.Cells(colSNF_KG).Value = 0
        gvBOM.CurrentRow.Cells(colFAT_KG).Value = 0
        gvBOM.CurrentRow.Cells(colEffectiveDate).ReadOnly = True

        Dim obj As clsBOM = clsBOM.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), whrcls, isButtonCLicked)

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
            If Not clsCommon.CompairString(strType, "Alt") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                'gvBOM.CurrentRow.Cells(colItemType).Value = ItemType(clsCommon.myCstr(obj.PROD_ITEM_TYPE))
                gvBOM.CurrentRow.Cells(colProductType).Value = obj.CONSM_ITEM_PRODUCT_TYPE
                'If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "Milk") <> CompairStringResult.Equal Then
                '    gvBOM.CurrentRow.Cells(colSNF).ReadOnly = True
                '    gvBOM.CurrentRow.Cells(colFAT).ReadOnly = True
                '    gvBOM.CurrentRow.Cells(colSNF).Value = 0
                '    gvBOM.CurrentRow.Cells(colFAT).Value = 0
                '    gvBOM.CurrentRow.Cells(colSNF_KG).ReadOnly = True
                '    gvBOM.CurrentRow.Cells(colFAT_KG).ReadOnly = True
                '    gvBOM.CurrentRow.Cells(colSNF_KG).Value = 0
                '    gvBOM.CurrentRow.Cells(colFAT_KG).Value = 0
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "Milk") = CompairStringResult.Equal Then
                ''gvBOM.CurrentRow.Cells(colSNF).ReadOnly = False
                ''gvBOM.CurrentRow.Cells(colFAT).ReadOnly = False
                ''gvBOM.CurrentRow.Cells(colSNF_KG).ReadOnly = False
                ''gvBOM.CurrentRow.Cells(colFAT_KG).ReadOnly = False

                gvBOM.CurrentRow.Cells(colSNF).Value = clsBOM.GetSNF_PERS(obj.PROD_ITEM_CODE)
                gvBOM.CurrentRow.Cells(colFAT).Value = clsBOM.GetFAT_PERS(obj.PROD_ITEM_CODE)

                ''richa agarwal BHA/12/07/18-000147 snf and fat colum should be enabled in case of milk item
                If clsCommon.CompairString(clsCommon.myCstr(obj.CONSM_ITEM_PRODUCT_TYPE), "Milk") = CompairStringResult.Equal Then
                    gvBOM.CurrentRow.Cells(colSNF).ReadOnly = False
                    gvBOM.CurrentRow.Cells(colFAT).ReadOnly = False
                Else
                    gvBOM.CurrentRow.Cells(colSNF).ReadOnly = True
                    gvBOM.CurrentRow.Cells(colFAT).ReadOnly = True
                End If
                ''-----------------------
                Cal_FAT()
                Cal_SNF()
                'End If
            Else
                gvBOM.CurrentRow.Cells(colAltritemcode).Value = obj.PROD_ITEM_CODE
                gvBOM.CurrentRow.Cells(colAltrItemdesc).Value = obj.ITEM_DESCRIPTION
                gvBOM.CurrentRow.Cells(colAltrItemtype).Value = ItemType(clsCommon.myCstr(obj.PROD_ITEM_TYPE))
                gvBOM.CurrentRow.Cells(colAltrItemUom).Value = obj.PROD_ITEM_UNIT_CODE
            End If
            gvBOM.CurrentRow.Cells(colBOMRecordShow).Value = IIf(clsBOM.CheckBOMExist(txtCode.Value, clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value)) > 0, "Record Exist", "No Record")
        Else
            If clsCommon.CompairString(strType, "Alt") <> CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colItemCode).Value = ""
                gvBOM.CurrentRow.Cells(colitemDesc).Value = ""
                gvBOM.CurrentRow.Cells(colUnitCode).Value = ""
                gvBOM.CurrentRow.Cells(colItemType).Value = ""
                gvBOM.CurrentRow.Cells(colProductType).Value = ""
                gvBOM.CurrentRow.Cells(colSNF).Value = 0
                gvBOM.CurrentRow.Cells(colFAT_KG).Value = 0
                gvBOM.CurrentRow.Cells(colFAT).Value = 0
                gvBOM.CurrentRow.Cells(colSNF_KG).Value = 0
                gvBOM.CurrentRow.Cells(colBOMRecordShow).Value = "No Record"
            Else
                gvBOM.CurrentRow.Cells(colAltritemcode).Value = ""
                gvBOM.CurrentRow.Cells(colAltrItemdesc).Value = ""
                gvBOM.CurrentRow.Cells(colAltrItemtype).Value = ""
                gvBOM.CurrentRow.Cells(colAltrItemUom).Value = ""
            End If

        End If
    End Sub

    Private Sub gvStages_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvStages.CurrentColumnChanged
        'If gvStages.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvStages.CurrentRow.Index

        '    If intCurrRow = gvStages.Rows.Count - 1 Then
        '        gvStages.Rows.AddNew()
        '        gvStages.CurrentRow = gvStages.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub txtUomCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUomCode._MYValidating
        If clsCommon.myLen(txtProducedItem.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select item first", Me.Text)
            txtProducedItem.Focus()
            txtProducedItem.Select()
            Errorcontrol.SetError(lblMasterItemName, "Select item first")
            Return
        Else
            Errorcontrol.ResetError(lblMasterItemName)
        End If

        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_unit_master.unit_desc as Unit from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code"
        txtUomCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.item_code='" + txtProducedItem.Value + "'", txtUomCode.Value, "Code", isButtonClicked))

        If clsCommon.myLen(txtUomCode.Value) > 0 Then
            txtuomname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + txtUomCode.Value + "'"))
            txtbuildUnit.Text = txtuomname.Text
        Else
            txtuomname.Text = ""
            txtbuildUnit.Text = ""
        End If
    End Sub

    Private Sub cboBOMStatus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBOMStatus.TextChanged
        Try

            If clsCommon.CompairString(cboBOMStatus.Text, "Open") = CompairStringResult.Equal Then
                UsLock1.Status = ERPTransactionStatus.Pending
            ElseIf clsCommon.CompairString(cboBOMStatus.Text, "Approved") = CompairStringResult.Equal Then
                UsLock1.Status = ERPTransactionStatus.Approved
            ElseIf clsCommon.CompairString(cboBOMStatus.Text, "On Hold") = CompairStringResult.Equal Then
                UsLock1.Status = ERPTransactionStatus.Pending
            ElseIf clsCommon.CompairString(cboBOMStatus.Text, "In-Active") = CompairStringResult.Equal Then
                UsLock1.Status = ERPTransactionStatus.Cancel
            End If
            If btnsave.Enabled = False AndAlso btnPost.Enabled = False AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
                UsLock1.Status = ERPTransactionStatus.Approved
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim currentdate As Date = Date.Today
        Dim oldnewentry As Boolean = isNewEntry

        If transportSql.importExcel(gv_Import, "BOM_CODE", "Bom Date", "Revision No", "STATUS", "Description", "Production Category Code", "Main Item Code", "Main Item Unit", "Build Qty", "Valid_START_DATE", "Valid_UPTO_DATE", "LINE_NO", "Item Code", "Unit", "QUANTITY", "FAT%", "SNF%", "Rejection%", "Alt_Item_Code", "Alt_Unit_code", "Deactive", "Effective_Date", "REMARKS", "Section_Code", "Is_OSP", "Vendor_Code", "Job Work Location", "ProcessLossPer", "ProcessLossQty") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim bom_Date As Date = Nothing
                Dim rev_no As String = Nothing '100
                Dim status As String = Nothing
                Dim descrptn As String = Nothing '200
                Dim prod_cat As String = Nothing
                Dim main_item As String = Nothing
                Dim main_unit As String = Nothing
                Dim build_qty As Decimal = Nothing
                Dim valid_from As Date = Nothing
                Dim valid_upto As Date = Nothing
                Dim is_osp As Integer = Nothing
                Dim vendor_code As String = Nothing

                Dim line_no As Integer = Nothing
                Dim item_code As String = Nothing
                Dim unit As String = Nothing
                Dim qty As Decimal = Nothing
                Dim fat_pers As Decimal = Nothing
                Dim fat_kg As Decimal = Nothing
                Dim snf_pers As Decimal = Nothing
                Dim snf_kg As Decimal = Nothing
                Dim rej_pers As Decimal = Nothing
                Dim alt_item_code As String = Nothing
                Dim alt_unit As String = Nothing
                Dim deactive As Integer = Nothing
                Dim effective_date As Date = Nothing
                Dim remarks As String = Nothing '250
                Dim section_code As String = Nothing
                Dim qry As String = ""
                Dim check As Integer = 0
                Dim Job_Work_Location As String = Nothing
                Dim ProcessLossPer As Decimal = Nothing
                Dim ProcessLossQty As Decimal = Nothing
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows

                    Dim bomcode As String = clsCommon.myCstr(grow.Cells("BOM_CODE").Value)

                    If grow.Cells("Bom Date").Value Is Nothing OrElse grow.Cells("Bom Date").Value Is DBNull.Value Then
                        Throw New Exception("Fill bom date at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    bom_Date = clsCommon.myCDate(grow.Cells("Bom Date").Value)

                    rev_no = clsCommon.myCstr(grow.Cells("Revision No").Value).Replace("'", "`") '100
                    If clsCommon.myLen(rev_no) > 100 Then
                        rev_no = rev_no.Substring(0, 100)
                    End If

                    status = clsCommon.myCstr(grow.Cells("STATUS").Value)
                    If clsCommon.myLen(status) <= 0 Then
                        status = "Open"
                    End If

                    If clsCommon.CompairString(status, "Open") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(status, "On Hold") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(status, "Approved") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(status, "In-Active") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Status as [Open Or On Hold Or Approved Or In-Active] at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    descrptn = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`") '200
                    If clsCommon.myLen(descrptn) > 200 Then
                        descrptn = descrptn.Substring(0, 200)
                    End If

                    '==========================vendor=========================================================
                    If clsCommon.myLen(grow.Cells("is_osp").Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("is_osp").Value), "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("is_osp").Value), "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill 0 or 1 for OSP at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    is_osp = CInt(clsCommon.myCdbl(grow.Cells("is_osp").Value))
                    vendor_code = ""
                    If is_osp = 1 Then
                        vendor_code = clsCommon.myCstr(grow.Cells("vendor_code").Value)
                    End If
                    '===================end here================================================================

                    prod_cat = clsCommon.myCstr(grow.Cells("Production Category Code").Value)
                    If clsCommon.myLen(prod_cat) <= 0 Then
                        Throw New Exception("Fill Production category code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    Else
                        qry = "select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code='" + prod_cat + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Production category code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    main_item = clsCommon.myCstr(grow.Cells("Main Item Code").Value)
                    If clsCommon.myLen(main_item) <= 0 Then
                        Throw New Exception("Fill Main item code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    Else
                        qry = "select count(*) from tspl_item_master where item_code='" + main_item + "'" ' and item_type in ('F','S')
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Main item code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    main_unit = clsCommon.myCstr(grow.Cells("Main Item Unit").Value)
                    If clsCommon.myLen(main_unit) <= 0 Then
                        Throw New Exception("Fill Main item unit at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    Else
                        qry = "select count(*) from tspl_item_uom_detail where item_code='" + main_item + "' and uom_code='" + main_unit + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Main item unit does not mapped with Main Item see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    build_qty = Math.Round(clsCommon.myCdbl(grow.Cells("Build Qty").Value), DecimalPoint)
                    If clsCommon.myCdbl(build_qty) <= 0 Then
                        Throw New Exception("Fill build quantity at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If grow.Cells("Valid_START_DATE").Value Is Nothing OrElse grow.Cells("Valid_START_DATE").Value Is DBNull.Value Then
                        Throw New Exception("Fill Valid_START_DATE at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    valid_from = clsCommon.myCDate(grow.Cells("Valid_START_DATE").Value)

                    If grow.Cells("Valid_UPTO_DATE").Value Is Nothing OrElse grow.Cells("Valid_UPTO_DATE").Value Is DBNull.Value Then
                        valid_upto = clsCommon.myCDate(valid_from).AddYears(3)
                        'Throw New Exception("Fill Valid_UPTO_DATE at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    valid_upto = clsCommon.myCDate(grow.Cells("Valid_UPTO_DATE").Value)

                    line_no = clsCommon.myCdbl(grow.Cells("LINE_NO").Value)
                    If clsCommon.myCdbl(line_no) <= 0 Then
                        Throw New Exception("Fill Line_No at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    item_code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(item_code) <= 0 Then
                        Throw New Exception("Fill item code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    Else
                        qry = "select count(*) from tspl_item_master where item_code='" + item_code + "'" ' and item_type <>'F'
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled item code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    unit = clsCommon.myCstr(grow.Cells("Unit").Value)
                    If clsCommon.myLen(unit) <= 0 Then
                        Throw New Exception("Fill unit code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    Else
                        qry = "select count(*) from tspl_item_uom_detail where item_code='" + item_code + "' and uom_code='" + unit + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled unit code does not mapped with Item Code see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    Dim item_type As String = clsItemMaster.GetItemProductType(item_code, trans)

                    qty = Math.Round(clsCommon.myCdbl(grow.Cells("QUANTITY").Value), DecimalPoint)
                    If clsCommon.myCdbl(qty) <= 0 Then
                        Throw New Exception("Fill Quantity at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    fat_pers = Math.Round(clsCommon.myCdbl(grow.Cells("FAT%").Value), 2)

                    fat_kg = Math.Round((qty * fat_pers) / 100, DecimalPoint)

                    snf_pers = Math.Round(clsCommon.myCdbl(grow.Cells("SNF%").Value), 2)

                    snf_kg = Math.Round((qty * snf_pers) / 100, DecimalPoint)

                    rej_pers = clsCommon.myCdbl(grow.Cells("Rejection%").Value)
                    alt_item_code = clsCommon.myCstr(grow.Cells("Alt_Item_Code").Value)
                    If clsCommon.myLen(alt_item_code) > 0 Then
                        qry = "select count(*) from tspl_item_master where item_code='" + alt_item_code + "'" ' and item_type <>'F'
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Alt_Item_Code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    alt_unit = clsCommon.myCstr(grow.Cells("Alt_Unit_code").Value)
                    If clsCommon.myLen(alt_item_code) > 0 Then
                        qry = "select count(*) from tspl_item_uom_detail where item_code='" + alt_item_code + "' and uom_code='" + alt_unit + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Alt_Unit_code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    deactive = clsCommon.myCdbl(grow.Cells("Deactive").Value)
                    effective_date = Nothing

                    If clsCommon.myCdbl(deactive) > 1 OrElse clsCommon.myCdbl(deactive) < 0 Then
                        Throw New Exception("Fill 1-Deactive or 0-Active at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    If deactive = 1 AndAlso (grow.Cells("Effective_Date").Value Is Nothing OrElse grow.Cells("Effective_Date").Value Is DBNull.Value) Then
                        Throw New Exception("Fill effective date at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    ElseIf deactive = 1 Then
                        effective_date = clsCommon.myCDate(grow.Cells("Effective_Date").Value)
                    End If



                    remarks = clsCommon.myCstr(grow.Cells("REMARKS").Value).Replace("'", "`") '250
                    If clsCommon.myLen(remarks) > 250 Then
                        remarks = remarks.Substring(0, 250)
                    End If

                    section_code = clsCommon.myCstr(grow.Cells("Section_Code").Value)
                    If clsCommon.myLen(section_code) <= 0 AndAlso is_osp <> 1 Then
                        Throw New Exception("Fill section_code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    ElseIf clsCommon.myLen(section_code) > 0 Then
                        qry = "select count(*) from TSPL_SECTION_MASTER where Section_Code='" + section_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled section_code does not exist at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If

                        qry = "select count(*) from TSPL_SECTION_STAGE_MAPPING_HEAD where Section_Code='" + section_code + "' and structure_code='" + prod_cat + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            status = "Open"
                            'Throw New Exception("Filled section_code does not have entry in Section Stage Mapping Screen at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    '===================Head================================
                    Dim bom_code As String = ""

                    If clsCommon.myLen(bomcode) > 0 Then
                        qry = "select count(*) from tspl_pp_bom_head where bom_code='" + bomcode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check > 0 Then
                            bom_code = bomcode
                            isNewEntry = False
                        End If
                    End If

                    'If clsCommon.myLen(rev_no) <= 0 Then
                    '    qry = "select revision_no from tspl_pp_bom_head where prod_item_code='" + main_item + "' and prod_item_unit_code='" + main_unit + "' and prod_quantity='" + clsCommon.myCstr(build_qty) + "' and valid_upto_date >= '" + clsCommon.GetPrintDate(valid_from, "dd/MMM/yyyy") + "'"
                    '    rev_no = clsDBFuncationality.getSingleValue(qry, trans)

                    '    If clsCommon.myLen(rev_no) <= 0 Then
                    '        qry = "select max(revision_no) from TSPL_PP_BOM_HEAD where prod_item_code='" + main_item + "'"
                    '        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    '        If clsCommon.myLen(str) > 0 Then
                    '            rev_no = clsCommon.incval(str)
                    '        Else
                    '            rev_no = main_item + "/00001"
                    '        End If
                    '    End If
                    'End If
                    If clsCommon.myLen(bom_code) <= 0 Then
                        qry = "select bom_code from tspl_pp_bom_head where isnull(vendor_code,'')='" + vendor_code + "' and prod_item_code='" + main_item + "' and prod_item_unit_code='" + main_unit + "' and prod_quantity='" + clsCommon.myCstr(build_qty) + "' and valid_upto_date >= '" + clsCommon.GetPrintDate(valid_from, "dd/MMM/yyyy") + "'"
                        bom_code = clsDBFuncationality.getSingleValue(qry, trans)
                        isNewEntry = True

                        If bom_code IsNot Nothing AndAlso clsCommon.myLen(bom_code) > 0 Then
                            isNewEntry = False
                        End If
                    End If

                    If isNewEntry Then
                        If clsCommon.myLen(vendor_code) > 0 Then
                            bom_code = clsERPFuncationality.GetNextCode(trans, bom_Date, clsDocType.BOM, clsDocTransactionType.BOMOSPTYPE, "")
                        Else
                            bom_code = clsERPFuncationality.GetNextCode(trans, bom_Date, clsDocType.BOM, clsDocTransactionType.SNQuotationOther, "")
                        End If
                    End If

                    Job_Work_Location = clsCommon.myCstr(grow.Cells("Job Work Location").Value)
                    ProcessLossPer = clsCommon.myCdbl(grow.Cells("ProcessLossPer").Value)
                    ProcessLossQty = clsCommon.myCdbl(grow.Cells("ProcessLossQty").Value)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", bom_code)
                    clsCommon.AddColumnsForChange(coll, "Description", descrptn)
                    clsCommon.AddColumnsForChange(coll, "BOM_DATE", clsCommon.GetPrintDate(bom_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "valid_from_date", clsCommon.GetPrintDate(valid_from, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "valid_upto_date", clsCommon.GetPrintDate(valid_upto, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "STATUS", clsCommon.myCstr(status))
                    clsCommon.AddColumnsForChange(coll, "PROD_ITEM_CODE", clsCommon.myCstr(main_item))
                    clsCommon.AddColumnsForChange(coll, "PROD_ITEM_UNIT_CODE", clsCommon.myCstr(main_unit))
                    clsCommon.AddColumnsForChange(coll, "PROD_QUANTITY", clsCommon.myCdbl(build_qty))
                    clsCommon.AddColumnsForChange(coll, "Section_code", section_code, True)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MMM/yyyy hh:mm tt")))
                    clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", prod_cat)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_Values", "D")
                    clsCommon.AddColumnsForChange(coll, "Is_Post", IIf(clsCommon.CompairString(status, "Approved") = CompairStringResult.Equal, "1", "0"))
                    clsCommon.AddColumnsForChange(coll, "revision_no", rev_no)
                    clsCommon.AddColumnsForChange(coll, "is_osp", is_osp)
                    clsCommon.AddColumnsForChange(coll, "vendor_code", vendor_code, True)

                    clsCommon.AddColumnsForChange(coll, "JobWork_Loc", Job_Work_Location)
                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))

                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_HEAD", OMInsertOrUpdate.Update, " TSPL_PP_BOM_HEAD.BOM_CODE='" + bom_code + "'", trans)
                    End If

                    '======detail=========================================
                    qry = "delete from TSPL_PP_BOM_ITEM_DETAIL where bom_code='" + bom_code + "' and ITEM_CODE='" + item_code + "' and UNIT_CODE='" + unit + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "BOM_CODE", bom_code)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", line_no)
                    clsCommon.AddColumnsForChange(coll, "ITEM_CODE", item_code)
                    clsCommon.AddColumnsForChange(coll, "UNIT_CODE", unit)
                    clsCommon.AddColumnsForChange(coll, "QUANTITY", qty)
                    clsCommon.AddColumnsForChange(coll, "FAT", fat_pers)
                    clsCommon.AddColumnsForChange(coll, "SNF", snf_pers)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", fat_kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", snf_kg)
                    clsCommon.AddColumnsForChange(coll, "Rejection_Pers", rej_pers)
                    clsCommon.AddColumnsForChange(coll, "Alt_Item_Code", alt_item_code)
                    clsCommon.AddColumnsForChange(coll, "Alt_Unit_code", alt_unit)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", remarks)
                    clsCommon.AddColumnsForChange(coll, "Deactive", deactive)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(effective_date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "ProcessLossPer", ProcessLossPer)
                    clsCommon.AddColumnsForChange(coll, "ProcessLossQty", ProcessLossQty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    '=====================Stage detail=============================
                    qry = "delete from TSPL_PP_BOM_STAGE_DETAIL where bom_code='" + bom_code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "select TSPL_SECTION_STAGE_MAPPING.Stage_Code as Code,TSPL_STAGE_MASTER.Description,TSPL_SECTION_STAGE_MAPPING.Sequence_No as [Sequence No] from TSPL_SECTION_STAGE_MAPPING left outer join TSPL_STAGE_MASTER on TSPL_STAGE_MASTER.Stage_Code=TSPL_SECTION_STAGE_MAPPING.Stage_Code where TSPL_SECTION_STAGE_MAPPING.Section_Code in (select section_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + section_code + "' and Structure_Code='" + prod_cat + "') and TSPL_SECTION_STAGE_MAPPING.doc_code in (select doc_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + section_code + "' and Structure_Code='" + prod_cat + "')"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            coll = New Hashtable()

                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                            clsCommon.AddColumnsForChange(coll, "BOM_CODE", bom_code)
                            clsCommon.AddColumnsForChange(coll, "Section_Code", section_code)
                            clsCommon.AddColumnsForChange(coll, "Stage_Code", clsCommon.myCstr(dr("Code")))
                            clsCommon.AddColumnsForChange(coll, "Sequence", clsCommon.myCdbl(dr("Sequence No")))

                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_BOM_STAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If
                    '============================================================
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        isNewEntry = oldnewentry
        Me.Controls.Remove(gv_Import)
    End Sub
    'Ticket No-ERO/30/07/19-000972,Sanjay ,Add process loss % , process loss qty column in Import/Export.
    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim qry As String = "select count(*) from tspl_pp_bom_head"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.BOM_DATE as [Bom Date],TSPL_PP_BOM_HEAD.Revision_No as [Revision No],TSPL_PP_BOM_HEAD.STATUS,TSPL_PP_BOM_HEAD.Description, " & _
                "TSPL_PP_BOM_HEAD.ITEM_CATEGORY_CODE as [Production Category Code],TSPL_PP_BOM_HEAD.PROD_ITEM_CODE as [Main Item Code], " & _
                "TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as [Main Item Unit],MainItem.Item_Desc as [Main Item Desc],TSPL_PP_BOM_HEAD.PROD_QUANTITY as [Build Qty], " & _
                "TSPL_PP_BOM_HEAD.Valid_FROM_DATE as Valid_START_DATE,TSPL_PP_BOM_HEAD.Valid_UPTO_DATE,TSPL_PP_BOM_ITEM_DETAIL.LINE_NO, " & _
                "TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Desc],TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE as [Unit], " & _
                "TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.FAT as [FAT%],TSPL_PP_BOM_ITEM_DETAIL.SNF as [SNF%], " & _
                "TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers as [Rejection%],TSPL_PP_BOM_ITEM_DETAIL.Alt_Item_Code,AlterItem.Item_Desc as [Alternate Item Desc], " & _
                "TSPL_PP_BOM_ITEM_DETAIL.Alt_Unit_code,TSPL_PP_BOM_ITEM_DETAIL.Deactive,TSPL_PP_BOM_ITEM_DETAIL.Effective_Date,TSPL_PP_BOM_ITEM_DETAIL.REMARKS, " & _
                "TSPL_PP_BOM_HEAD.Section_Code,TSPL_PP_BOM_HEAD.Is_OSP,TSPL_PP_BOM_HEAD.Vendor_Code,TSPL_PP_BOM_HEAD.JobWork_Loc as [Job Work Location] " & _
                ",TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,TSPL_PP_BOM_ITEM_DETAIL.ProcessLossQty from TSPL_PP_BOM_HEAD left outer join  " & _
                "TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                "left outer join TSPL_ITEM_MASTER MainItem  on TSPL_PP_BOM_HEAD.PROD_ITEM_CODE=MainItem.Item_Code " & _
                "left outer join TSPL_ITEM_MASTER   on TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code   " & _
                "left outer join TSPL_ITEM_MASTER AlterItem  on TSPL_PP_BOM_ITEM_DETAIL.Alt_Item_Code=AlterItem.Item_Code"
        Else
            qry = "select '' as BOM_CODE,'' as [Bom Date],'' as [Revision No],'Open' as STATUS,'' as Description,'' as [Production Category Code],'' as [Main Item Code],'' as [Main Item Unit],0 as [Build Qty],'' as Valid_START_DATE,'' as Valid_UPTO_DATE,1 as LINE_NO,'' as [Item Code],'' as [Unit],0 as QUANTITY,0 as [FAT%],0 as [SNF%],0 as [Rejection%],'' as Alt_Item_Code,'' as Alt_Unit_code,'0-Active,1-Deactive' as Deactive,'' as Effective_Date,'' as REMARKS,'' as Section_Code,'0' as Is_OSP,'' as Vendor_Code,'' as [Job Work Location]"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub gvBOM_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvBOM.UserDeletingRow
        If clsCommon.myLen(txtCode.Value) > 0 AndAlso btnPost.Enabled Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else

                Dim qry As String = " delete from TSPL_PP_BOM_ITEM_DETAIL where bom_code='" + clsCommon.myCstr(txtCode.Value) + "' and item_code='" + clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Else
            e.Cancel = True
            clsCommon.MyMessageBoxShow(Me, "No row deleted", Me.Text)
        End If
    End Sub

    Private Sub gvStages_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvStages.UserDeletingRow
        If clsCommon.myLen(txtCode.Value) > 0 AndAlso btnPost.Enabled Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else

                Dim qry As String = " delete from TSPL_PP_BOM_STAGE_DETAIL where bom_code='" + clsCommon.myCstr(txtCode.Value) + "' and section_code='" + clsCommon.myCstr(fndSection.Value) + "' and stage_code='" + clsCommon.myCstr(gvStages.CurrentRow.Cells(colStageCode).Value) + "' and sequence='" + clsCommon.myCstr(CInt(clsCommon.myCdbl(gvStages.CurrentRow.Cells(colStageSequence).Value))) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Else
            e.Cancel = True
            clsCommon.MyMessageBoxShow("No row deleted", Me.Text)
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_PP_BOM_HEAD where Is_Post='0' and bom_code='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            Dim frm1 As New FrmPWD(Nothing)
            frm1.strType = clsFixedParameterType.BOM_Amend_Pswd
            frm1.strCode = clsFixedParameterCode.BOM_Amend_Pswd
            frm1.ShowDialog()
            If Not frm1.isPasswordCorrect Then
                Exit Sub
            End If

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

                If clsBOM.ReverseAndUnpost(txtCode.Value) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Amended and Recreated", Me.Text)
                        'btnReverse.Visible = False
                        LoadData(txtCode.Value, NavigatorType.Current)
                        GenerateRevisionNo()
                        clsDBFuncationality.ExecuteNonQuery("update tspl_pp_bom_head set revision_no='" + txtrevisionno.Text + "' where bom_code='" + txtCode.Value + "'")
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        If gv_History.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Dated: " + clsCommon.GetPrintDate(dtpBOMDate.Text, "dd/MM/yyyy"))
            clsCommon.MyExportToExcelGrid("BOM History", gv_History, arrHeader, Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If gv_History.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Dated: " + clsCommon.GetPrintDate(dtpBOMDate.Text, "dd/MM/yyyy"))
            clsCommon.MyExportToPDF("BOM History", gv_History, arrHeader, Me.Text)
        End If
    End Sub

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            txtCode.Select()
            txtCode.Focus()
            RadPageView.SelectedPage = RadPageItemDetails
            Errorcontrol.SetError(txtCode, "Select bom code first.")
            clsCommon.MyMessageBoxShow(Me, "Select bom code first.", Me.Text)
            Exit Sub
        Else
            Errorcontrol.ResetError(txtCode)
        End If

        Dim qry As String = "select BOM,BOM_Item,modified_date,Revision_No,Valid_FROM_DATE,Valid_UPTO_DATE,ITEM_CODE,RawItem,UNIT_CODE,QUANTITY,FAT_KG,SNF_KG,Deactive,(case when (isnull(convert(varchar,Effective_Date,103),'01/01/0001')='01/01/0001' or isnull(convert(varchar,Effective_Date,103),'01/01/0001')='01-01-0001') then '' else convert(varchar,Effective_Date,103) end) as Effective_Date,ProcessLossPer,ProcessLossQty from ("
        qry += "select tspl_pp_bom_head.modified_date,(case when isnull(TSPL_PP_BOM_HEAD.description,'')<>'' then cast(TSPL_PP_BOM_HEAD.BOM_CODE as varchar) +' - '+convert(varchar,TSPL_PP_BOM_HEAD.BOM_DATE,103)+' - '+cast(TSPL_PP_BOM_HEAD.Description as varchar)+' - '+cast(TSPL_SECTION_MASTER.Description as varchar) else cast(TSPL_PP_BOM_HEAD.BOM_CODE as varchar) +' - '+cast(TSPL_PP_BOM_HEAD.BOM_DATE as varchar)+' - '+cast(TSPL_SECTION_MASTER.Description as varchar) end) as BOM,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE+' - '+TSPL_ITEM_MASTER.Item_Desc+' - '+cast(TSPL_PP_BOM_HEAD.PROD_QUANTITY as varchar)+' - '+TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_Item,TSPL_PP_BOM_HEAD.Revision_No,convert(varchar,TSPL_PP_BOM_HEAD.Valid_FROM_DATE,103) as Valid_FROM_DATE,convert(varchar,TSPL_PP_BOM_HEAD.Valid_UPTO_DATE,103) as Valid_UPTO_DATE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,item.Item_Desc as RawItem,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL.FAT_KG,TSPL_PP_BOM_ITEM_DETAIL.SNF_KG,(case when TSPL_PP_BOM_ITEM_DETAIL.Deactive=1 then 'Deactive' else 'Active' end) as Deactive,convert(varchar,TSPL_PP_BOM_ITEM_DETAIL.Effective_Date,103) as Effective_Date,TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,TSPL_PP_BOM_ITEM_DETAIL.ProcessLossQty "
        qry += "from TSPL_PP_BOM_ITEM_DETAIL left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.bom_code=TSPL_PP_BOM_ITEM_DETAIL.bom_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code=TSPL_PP_BOM_HEAD.Section_Code left outer join TSPL_ITEM_MASTER item on item.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE "
        qry += "where TSPL_PP_BOM_HEAD.BOM_CODE='" + txtCode.Value + "' "
        qry += " union all "
        qry += "select TSPL_PP_BOM_HEAD_HISTORY.modified_date,(case when isnull(TSPL_PP_BOM_HEAD_HISTORY.description,'')<>'' then cast(TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE as varchar) +' - '+convert(varchar,TSPL_PP_BOM_HEAD_HISTORY.BOM_DATE,103)+' - '+cast(TSPL_PP_BOM_HEAD_HISTORY.Description as varchar)+' - '+cast(TSPL_SECTION_MASTER.Description as varchar) else cast(TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE as varchar) +' - '+cast(TSPL_PP_BOM_HEAD_HISTORY.BOM_DATE as varchar)+' - '+cast(TSPL_SECTION_MASTER.Description as varchar) end) as BOM,TSPL_PP_BOM_HEAD_HISTORY.PROD_ITEM_CODE+' - '+TSPL_ITEM_MASTER.Item_Desc+' - '+cast(TSPL_PP_BOM_HEAD_HISTORY.PROD_QUANTITY as varchar)+' - '+TSPL_PP_BOM_HEAD_HISTORY.PROD_ITEM_UNIT_CODE as BOM_Item,TSPL_PP_BOM_HEAD_HISTORY.Revision_No,convert(varchar,TSPL_PP_BOM_HEAD_HISTORY.Valid_FROM_DATE,103) as Valid_FROM_DATE,convert(varchar,TSPL_PP_BOM_HEAD_HISTORY.Valid_UPTO_DATE,103) as Valid_UPTO_DATE,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.ITEM_CODE,item.Item_Desc as RawItem,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.QUANTITY,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.FAT_KG,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.SNF_KG,(case when TSPL_PP_BOM_ITEM_DETAIL_HISTORY.Deactive=1 then 'Deactive' else 'Active' end) as Deactive,convert(varchar,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.Effective_Date,103) as Effective_Date,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.ProcessLossPer,TSPL_PP_BOM_ITEM_DETAIL_HISTORY.ProcessLossQty "
        qry += "from TSPL_PP_BOM_ITEM_DETAIL_HISTORY left outer join TSPL_PP_BOM_HEAD_HISTORY on TSPL_PP_BOM_HEAD_HISTORY.History_No=TSPL_PP_BOM_ITEM_DETAIL_HISTORY.History_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_HEAD_HISTORY.PROD_ITEM_CODE left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code=TSPL_PP_BOM_HEAD_HISTORY.Section_Code left outer join TSPL_ITEM_MASTER item on item.Item_Code=TSPL_PP_BOM_ITEM_DETAIL_HISTORY.ITEM_CODE "
        qry += "where TSPL_PP_BOM_HEAD_HISTORY.BOM_CODE='" + txtCode.Value + "' "
        qry += ")axa order by revision_no"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_History.DataSource = Nothing
        gv_History.AllowColumnReorder = True
        gv_History.Columns.Clear()
        gv_History.Rows.Clear()
        gv_History.GroupDescriptors.Clear()
        gv_History.MasterTemplate.SummaryRowsBottom.Clear()
        gv_History.ShowGroupPanel = False
        gv_History.EnableFiltering = True
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv_History.DataSource = dt

            SetFormatHistoryGrid()
        Else
            Throw New Exception("No data found.")
        End If

        RadSplitButton1.Visible = True
        RadPageViewPage2.Item.Visibility = ElementVisibility.Visible

        RadPageView.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub SetFormatHistoryGrid()
        gv_History.TableElement.TableHeaderHeight = 40
        'gv_History.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv_History.Columns.Count - 1
            gv_History.Columns(ii).ReadOnly = True
            gv_History.Columns(ii).IsVisible = False
        Next

        gv_History.Columns("BOM").Width = 100
        gv_History.Columns("BOM").IsVisible = True
        gv_History.Columns("BOM").HeaderText = "BOM Detail"
        gv_History.Columns("BOM").FormatString = "" '"BOM"

        gv_History.Columns("BOM_Item").Width = 100
        gv_History.Columns("BOM_Item").IsVisible = True
        gv_History.Columns("BOM_Item").HeaderText = "Produced Item"
        gv_History.Columns("BOM_Item").FormatString = ""

        gv_History.Columns("modified_date").Width = 80
        gv_History.Columns("modified_date").IsVisible = True
        gv_History.Columns("modified_date").HeaderText = "Amendment Date"
        gv_History.Columns("modified_date").FormatString = ""

        gv_History.Columns("Revision_No").Width = 100
        gv_History.Columns("Revision_No").IsVisible = True
        gv_History.Columns("Revision_No").HeaderText = "Revision No."
        gv_History.Columns("Revision_No").FormatString = ""

        gv_History.Columns("Valid_FROM_DATE").Width = 80
        gv_History.Columns("Valid_FROM_DATE").IsVisible = True
        gv_History.Columns("Valid_FROM_DATE").HeaderText = "Valid From"
        gv_History.Columns("Valid_FROM_DATE").FormatString = ""

        gv_History.Columns("Valid_UPTO_DATE").Width = 80
        gv_History.Columns("Valid_UPTO_DATE").IsVisible = True
        gv_History.Columns("Valid_UPTO_DATE").HeaderText = "Valid Upto"
        gv_History.Columns("Valid_UPTO_DATE").FormatString = ""

        gv_History.Columns("ITEM_CODE").Width = 100
        gv_History.Columns("ITEM_CODE").IsVisible = True
        gv_History.Columns("ITEM_CODE").HeaderText = "Item Code"
        gv_History.Columns("ITEM_CODE").FormatString = ""

        gv_History.Columns("RawItem").Width = 220
        gv_History.Columns("RawItem").IsVisible = True
        gv_History.Columns("RawItem").HeaderText = "Description"
        gv_History.Columns("RawItem").FormatString = ""

        gv_History.Columns("UNIT_CODE").Width = 100
        gv_History.Columns("UNIT_CODE").IsVisible = True
        gv_History.Columns("UNIT_CODE").HeaderText = "Unit"
        gv_History.Columns("UNIT_CODE").FormatString = ""

        gv_History.Columns("QUANTITY").Width = 100
        gv_History.Columns("QUANTITY").IsVisible = True
        gv_History.Columns("QUANTITY").HeaderText = "Quantity"
        gv_History.Columns("QUANTITY").FormatString = "{0:F2}"

        gv_History.Columns("FAT_KG").Width = 80
        gv_History.Columns("FAT_KG").IsVisible = True
        gv_History.Columns("FAT_KG").HeaderText = "FAT Kg"
        gv_History.Columns("FAT_KG").FormatString = "{0:F2}"

        gv_History.Columns("SNF_KG").Width = 80
        gv_History.Columns("SNF_KG").IsVisible = True
        gv_History.Columns("SNF_KG").HeaderText = "SNF Kg"
        gv_History.Columns("SNF_KG").FormatString = "{0:F2}"

        gv_History.Columns("Deactive").Width = 100
        gv_History.Columns("Deactive").IsVisible = True
        gv_History.Columns("Deactive").HeaderText = "Active Status"
        gv_History.Columns("Deactive").FormatString = ""

        gv_History.Columns("Effective_Date").Width = 80
        gv_History.Columns("Effective_Date").IsVisible = True
        gv_History.Columns("Effective_Date").HeaderText = "Deactive Date"
        gv_History.Columns("Effective_Date").FormatString = ""

        gv_History.Columns("ProcessLossPer").Width = 80
        gv_History.Columns("ProcessLossPer").IsVisible = True
        gv_History.Columns("ProcessLossPer").HeaderText = "Process Loss %"
        gv_History.Columns("ProcessLossPer").FormatString = ""

        gv_History.Columns("ProcessLossQty").Width = 80
        gv_History.Columns("ProcessLossQty").IsVisible = True
        gv_History.Columns("ProcessLossQty").HeaderText = "Process Loss Qty"
        gv_History.Columns("ProcessLossQty").FormatString = ""

        gv_History.ShowGroupedColumns = False
        gv_History.GroupDescriptors.Add(New GridGroupByExpression("BOM as BOM  format ""{0}: {1}"" Group By BOM"))
        gv_History.GroupDescriptors.Add(New GridGroupByExpression("BOM_Item as BOM_Item  format ""{0}: {1}"" Group By BOM_Item"))


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("QUANTITY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item22 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        gv_History.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv_History.AutoExpandGroups = True
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub chkOSP_JW_CheckedChanged(sender As Object, e As EventArgs) Handles chkOSP_JW.CheckedChanged
        txtVendorCode.Enabled = chkOSP_JW.Checked
        txtVendorCode.Value = ""
    End Sub

    Private Sub txtVendorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorCode._MYValidating
        If chkOSP_JW.Checked Then
            If clsCommon.myLen(txtJobWorkLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Job Work Location", Me.Text)
                txtJobWorkLoc.Focus()
                Exit Sub
            End If
            Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code as [Code],Vendor_Name as [Vendor Name],ISNULL(Alies_Name,'') As [Alies Name],TSPL_VENDOR_MASTER.Add1,TSPL_VENDOR_MASTER.Add2,TSPL_VENDOR_MASTER.Add3,Closing_Date as [Closing Date],Vendor_Group_Code as [Vendor Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],TSPL_VENDOR_MASTER.City_Code as [City Code],City_Code_Desc as [City Description],TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Country,TSPL_VENDOR_MASTER.Phone1,TSPL_VENDOR_MASTER.Phone2,Fax,TSPL_VENDOR_MASTER.Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person FAX],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Code Description],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code],Payment_Code_Desc as [Payment Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Description],Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Ven_Type_Code as [Vendor Type Code],Ven_Type_Desc as [Vendor Type Description],TAX1,TSPL_VENDOR_MASTER.TAX1_Rate as [TAX1 Rate],TAX2,TSPL_VENDOR_MASTER.TAX2_Rate as [Tax2 Rate],TAX3,TSPL_VENDOR_MASTER.TAX3_Rate as [Tax3 Rate],TAX4,TSPL_VENDOR_MASTER.TAX4_Rate as [Tax4 Rate],TAX5,TSPL_VENDOR_MASTER.TAX5_Rate as [Tax5 Rate],TAX6,TSPL_VENDOR_MASTER.TAX6_Rate as [Tax6 Rate],TAX7,TSPL_VENDOR_MASTER.TAX7_Rate as [Tax7 Rate],TAX8,TSPL_VENDOR_MASTER.TAX8_Rate as [Tax8 Rate],TAX9,TSPL_VENDOR_MASTER.TAX9_Rate as [Tax9 Rate],TAX10,TSPL_VENDOR_MASTER.TAX10_Rate as [Tax10 Rate],Service_Tax_No as [Service Tax No],TSPL_VENDOR_MASTER.Tin_No as [TIN No],Lst_No as [LST No],(select case when Status='N' then 'Active' else 'In Active' end ) as Status,OnHold as [On Hold],Transporter,Remarks1,Remarks2,Additional1,Additional2,Additional3,Credit_Limit as [Credit Limit],TSPL_VENDOR_MASTER.Created_By as [Created By],TSPL_VENDOR_MASTER.Created_Date as [Created Date],TSPL_VENDOR_MASTER.Modify_By as [Modify By],TSPL_VENDOR_MASTER.Modify_Date as [Modify Date],TSPL_VENDOR_MASTER.Comp_Code as [Company Code],CST,ECC,Range,Collectorate,PAN,Is_Gross_Receipt as [Is Gross Receipt],Inter_Branch as [Inter Branch],CURRENCY_CODE as [Currency Code],franchise_yn as [Is Franchise] from tspl_vendor_master left outer join tspl_location_master on tspl_location_master.Jobwork_Vendor=TSPL_VENDOR_MASTER.Vendor_Code "
            txtVendorCode.Value = clsCommon.ShowSelectForm("TVENDFND", qry, "Code", "TSPL_VENDOR_MASTER.Status='N' and tspl_location_master.location_code='" & txtJobWorkLoc.Value & "'", txtVendorCode.Value, "Code", isButtonClicked)
            txtvendorName.Text = clsVendorMaster.GetName(txtVendorCode.Value, Nothing)
        Else
            txtVendorCode.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N' ", txtVendorCode.Value, isButtonClicked)
            txtvendorName.Text = clsVendorMaster.GetName(txtVendorCode.Value, Nothing)
        End If


    End Sub
    Private Sub txtJobWorkLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtJobWorkLoc._MYValidating
        Try
            txtJobWorkLoc.Value = clsLocation.getFinder("Location_Type='Physical' and ISNULL(Is_Jobwork,0)=1 and Is_Sub_Location='Y'", txtJobWorkLoc.Value, isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCostGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostGroup._MYValidating
        If (clsCommon.myLen(txtProducedItem.Value) > 0 AndAlso clsCommon.myLen(txtUomCode.Value) > 0) Then
            Dim qry As String = "    select DISTINCT TSPL_ITEM_COST_MAPPING_HEADS.HCODE as Code , TSPL_ITEM_COST_MAPPING_HEADS.DOC_DATE  , TSPL_ITEM_COST_MAPPING_HEADS.Item_Code , TSPL_ITEM_COST_MAPPING_HEADS.UOM , GROUP_CODE , Description , Start_Date , End_Date  , Case when  Status=0 then 'Pending' else 'Approved' end as Status " & _
                    " from TSPL_ITEM_COST_MAPPING_HEADS INNER JOIN TSPL_ITEM_COST_MAPPING_DETAILS_ALL ON TSPL_ITEM_COST_MAPPING_HEADS.HCODE=TSPL_ITEM_COST_MAPPING_DETAILS_ALL.HCODE "
            Dim whrcls As String = " TSPL_ITEM_COST_MAPPING_HEADS.STATUS=1 AND TSPL_ITEM_COST_MAPPING_DETAILS_ALL.Item_Code ='" + txtProducedItem.Value + "' AND TSPL_ITEM_COST_MAPPING_DETAILS_ALL.UOM='" + txtUomCode.Value + "' "
            txtCostGroup.Value = clsCommon.ShowSelectForm("FNDDoc", qry, "Code", whrcls, txtCostGroup.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCostGroup.Value) > 0 Then
                LoadOverheadGroupData(txtCostGroup.Value, txtProducedItem.Value, txtUomCode.Value)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Pleae Select MainItem & UOM First.", Me.Text)
        End If
    End Sub

    Sub LoadOverheadGroupData(ByVal strCode As String, ByVal stritem As String, ByVal struom As String)
        Dim obj As clsBOM = clsBOM.GetOverHeadCostGroupDetail(strCode, stritem, struom)
        gv1.Rows.Clear()
        gv1.Rows.AddNew()
        LoadCostGroupDetail(obj.ObjCostGroupDetail)
    End Sub
    Sub LoadCostGroupDetail(ByVal Arr As List(Of clsBomCostMappingDetails))
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                gv1.Rows.Clear()
            End If
            For Each objtr As clsBomCostMappingDetails In Arr
                If clsCommon.myLen(txtCode.Value) <= 0 Then
                    gv1.Rows.AddNew()
                End If
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostLineNo).Value = clsCommon.myCdbl(objtr.SNO)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Code).Value = objtr.COST_CODE
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost_Desc).Value = clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST where COST_CODE ='" + objtr.COST_CODE + "'", Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = clsCommon.myCdbl(objtr.COST)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCostUOM).Value = objtr.UOM
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBomRatePerHour).Value = clsCommon.myCdbl(objtr.BomRatePerHour)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBomHours).Value = clsCommon.myCdbl(objtr.BomHours)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBomNO).Value = clsCommon.myCdbl(objtr.BomNO)
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColOverHeadCost).Value = clsCommon.myCdbl(objtr.Overheadcost)
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    gv1.Rows.AddNew()
                End If
            Next
        End If
    End Sub

    Private Sub MenuImportOverheadCost_Click(sender As Object, e As EventArgs) Handles MenuImportOverheadCost.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        Dim oldnewentry As Boolean = isNewEntry

        If transportSql.importExcel(gv1, "BOM_CODE", "Cost_Group_Code", "Cost_Code", "Over_Head_Cost") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New clsBOM
                obj.ObjCostGroupDetail = New List(Of clsBomCostMappingDetails)
                Dim line_no As Integer = Nothing
                Dim item_code As String = Nothing
                Dim unit As String = Nothing
                Dim bomcode As String = Nothing
                Dim CostGroupCode As String = Nothing
                Dim CostCode As String = Nothing
                Dim OverHeadCost As Decimal = 0
                Dim gvrownum As Integer = 0
                Dim dtOverHeadCost As DataTable = New DataTable()
                dtOverHeadCost.Columns.Add("Bom_Code", GetType(String))
                dtOverHeadCost.Columns.Add("CostGroupCode", GetType(String))
                dtOverHeadCost.Columns.Add("OverHeadCost", GetType(Decimal))
                dtOverHeadCost.Rows.Clear()
                Dim dr As DataRow

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim ChkBOM As Integer = 0
                    Dim ChkCostGroupCode As Integer = 0
                    Dim ChkCostCode As Integer = 0
                    gvrownum = clsCommon.myCdbl(gv1.CurrentRow)
                    bomcode = clsCommon.myCstr(grow.Cells("BOM_CODE").Value)
                    ChkBOM = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(BOM_CODE) from TSPL_PP_BOM_HEAD where BOM_CODE='" + bomcode + "'", trans))
                    If ChkBOM = 0 Then
                        Throw New Exception("Fill Valid BOM Code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    CostGroupCode = clsCommon.myCstr(grow.Cells("Cost_Group_Code").Value)
                    ChkCostGroupCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(HCODE) from TSPL_ITEM_COST_MAPPING_HEADS where HCODE='" + CostGroupCode + "'", trans))
                    If ChkCostGroupCode = 0 Then
                        Throw New Exception("Fill Valid Cost_Group_Code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    CostCode = clsCommon.myCstr(grow.Cells("Cost_Code").Value)
                    ChkCostCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(COST_CODE) from TSPL_OVERHEAD_COST where COST_CODE='" + CostCode + "'", trans))
                    If ChkCostCode = 0 Then
                        Throw New Exception("Fill Valid Cost_Code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    Dim qry As String = " select DDCODE,HCODE,Item_Code,UOM,SNO,COST_CODE,COST,Cost as Overhead_Cost from TSPL_ITEM_COST_MAPPING_DETAILS_ALL " & _
                        " where HCODE= '" + CostGroupCode + "' AND COST_CODE='" + CostCode + "' AND Item_Code =(select PROD_ITEM_CODE from TSPL_PP_BOM_HEAD where BOM_CODE='" + bomcode + "') AND " & _
                        " UOM=(select PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD where BOM_CODE='" + bomcode + "') order by SNO asc "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing AndAlso dt.Rows.Count <= 0 Then
                        Throw New Exception("Fill Valid BOM_Code Against Cost_Group_Code and Cost_Group_Code against Cost_Code at line no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                    OverHeadCost = clsCommon.myCdbl(grow.Cells("Over_Head_Cost").Value)
                    dr = dtOverHeadCost.NewRow()
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If jj = clsCommon.myCdbl(grow.Index) Then
                            'dr("Bom_Code") = clsCommon.myCstr(grow.Cells("BOM_CODE").Value)
                            'dr("CostGroupCode") = clsCommon.myCstr(grow.Cells("Cost_Group_Code").Value)
                            'dr("OverHeadCost") = 0
                            'dtOverHeadCost.Rows.Add(dr)
                            Continue For
                        End If
                        If clsCommon.CompairString(bomcode, clsCommon.myCstr(gv1.Rows(jj).Cells("BOM_CODE").Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(CostGroupCode, clsCommon.myCstr(gv1.Rows(jj).Cells("Cost_Group_Code").Value)) = CompairStringResult.Equal And clsCommon.CompairString(CostCode, clsCommon.myCstr(gv1.Rows(jj).Cells("Cost_Code").Value)) = CompairStringResult.Equal Then
                            Dim Msg As String = ""
                            Throw New Exception(" Same Cost Code  Exist at Row No." + clsCommon.myCstr(grow.Index + 1) + " And " + clsCommon.myCstr(jj + 1))
                        End If
                    Next
                    If dtOverHeadCost Is Nothing OrElse dtOverHeadCost.Rows.Count <= 0 Then
                        dr("Bom_Code") = clsCommon.myCstr(grow.Cells("BOM_CODE").Value)
                        dr("CostGroupCode") = clsCommon.myCstr(grow.Cells("Cost_Group_Code").Value)
                        dr("OverHeadCost") = 0
                        dtOverHeadCost.Rows.Add(dr)
                    Else
                        For index As Integer = 0 To dtOverHeadCost.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(dtOverHeadCost.Rows(index)("Bom_Code")), bomcode) <> CompairStringResult.Equal Then
                                dr("Bom_Code") = clsCommon.myCstr(grow.Cells("BOM_CODE").Value)
                                dr("CostGroupCode") = clsCommon.myCstr(grow.Cells("Cost_Group_Code").Value)
                                dr("OverHeadCost") = 0
                                dtOverHeadCost.Rows.Add(dr)
                            End If
                        Next
                    End If
                    For index As Integer = 0 To dtOverHeadCost.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dtOverHeadCost.Rows(index)("Bom_Code")), bomcode) = CompairStringResult.Equal Then
                            Dim ToatlCost As Decimal = clsCommon.myCdbl(dtOverHeadCost.Rows(index)("OverHeadCost"))
                            ToatlCost += clsCommon.myCdbl(grow.Cells("Over_Head_Cost").Value)
                            dtOverHeadCost.Rows(index)("OverHeadCost") = clsCommon.myCdbl(ToatlCost)
                        End If
                    Next
                    '=====================Group mapping detail=============================
                    qry = "delete from TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS where Document_Code='" + bomcode + "' and HCODE='" + CostGroupCode + "' AND COST_CODE='" + CostCode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", bomcode)
                    ' clsCommon.AddColumnsForChange(coll, "Section_Code", sectioncode)
                    clsCommon.AddColumnsForChange(coll, "HCODE", CostGroupCode, True)
                    clsCommon.AddColumnsForChange(coll, "COST_CODE", CostCode)
                    clsCommon.AddColumnsForChange(coll, "OverHead_Cost", OverHeadCost, True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                    '============================================================
                Next
                For Each drr As DataRow In dtOverHeadCost.Rows
                    Dim qryBom As String = "update TSPL_PP_BOM_HEAD set OverHead_Cost=" & clsCommon.myCdbl(drr("OverHeadCost")) & " ,OverHeadCostGroup_Code='" + clsCommon.myCstr(drr("CostGroupCode")) + "' where BOM_CODE='" + clsCommon.myCstr(drr("Bom_Code")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qryBom, trans)
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        isNewEntry = oldnewentry
        Me.Controls.Remove(gv1)
    End Sub

    Private Sub MenuExportOverheadCost_Click(sender As Object, e As EventArgs) Handles MenuExportOverheadCost.Click
        Dim qry As String = " select  Document_code as 'BOM_CODE', HCODE as 'Cost_Group_Code', COST_CODE, OverHead_Cost as 'Over_Head_Cost' from TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS "
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub txtByproductItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtByproductItem._MYValidating
        Try
            txtByproductItem.Value = clsItemMaster.getFinder("", txtByproductItem.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtByproductUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtByproductUOM._MYValidating
        Try
            Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_unit_master.unit_desc as Unit from TSPL_ITEM_UOM_DETAIL left outer join tspl_unit_master on tspl_unit_master.unit_code=TSPL_ITEM_UOM_DETAIL.uom_code"
            txtByproductUOM.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.item_code='" + txtByproductItem.Value + "'", txtByproductUOM.Value, "Code", isButtonClicked))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        If isIncludeRatePerHoursIn = True Then
            If e.Column Is gv1.Columns(colBomRatePerHour) Then
                gv1.CurrentRow.Cells(ColOverHeadCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomNO).Value)
            End If
            If e.Column Is gv1.Columns(colBomHours) Then
                gv1.CurrentRow.Cells(ColOverHeadCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomNO).Value)
            End If
            If e.Column Is gv1.Columns(colBomNO) Then
                gv1.CurrentRow.Cells(ColOverHeadCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomRatePerHour).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomHours).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colBomNO).Value)
            End If
        End If
    End Sub
End Class