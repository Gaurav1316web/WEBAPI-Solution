Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmProductionPlanning
    Inherits FrmMainTranScreen

#Region "variables"
    Const colLineNo As String = "LineNo"
    Const colProductionLineCode As String = "ProductionLineCode"
    Const colBOMCode As String = "BOMCode"
    Const colRevisionNo As String = "RevisionNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colMinQty As String = "MinQty"
    Const colMaxQty As String = "MaxQty"
    Const colUnitCode As String = "UnitCode"
    Const colRemarks As String = "Remarks"

    Dim IndustryType As String = Nothing
    Dim MandatoryLineNoMaxMinQtyForProductionPlan As Boolean = False
    Const colSaleOrderNo As String = "SO_Code"
    Const colSaleOrderDesc As String = "SO_Desc"
    Const colPlannedQty As String = "PlannedQty"
    Const colStockQty As String = "StockQty"
    Const colBufferQty As String = "BufferQty"
    Const colExtraAddQty As String = "AddQty"
    Const colNetReqQty As String = "NetReqQty"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsProductionPlanning
    Private ObjList As New List(Of clsProductionPlanning)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
    Dim formtype As String = Nothing
#End Region
    

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub

    Sub LoadGridColumns()
        gvPP.Rows.Clear()
        gvPP.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim ProductionLineCode As New GridViewTextBoxColumn
        Dim BOMCode As New GridViewTextBoxColumn
        Dim RevisionNo As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim MinQty As New GridViewDecimalColumn
        Dim MaxQty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim Remarks As New GridViewTextBoxColumn

        LineNo = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(LineNo)

        ProductionLineCode = New GridViewTextBoxColumn()
        ProductionLineCode.FormatString = ""
        ProductionLineCode.HeaderText = "Production Line"
        ProductionLineCode.Name = colProductionLineCode
        ProductionLineCode.Width = 100
        'ProductionLineCode.ReadOnly = True
        ProductionLineCode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        ProductionLineCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ProductionLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ProductionLineCode.IsVisible = True
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            ProductionLineCode.IsVisible = False
        End If
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
            ProductionLineCode.IsVisible = True
        End If
        gvPP.Columns.Add(ProductionLineCode)

        BOMCode = New GridViewTextBoxColumn()
        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 100
        BOMCode.ReadOnly = False
        BOMCode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        BOMCode.TextImageRelation = TextImageRelation.TextBeforeImage
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(BOMCode)

        RevisionNo = New GridViewTextBoxColumn()
        RevisionNo.FormatString = ""
        RevisionNo.HeaderText = "Revision No"
        RevisionNo.Name = colRevisionNo
        RevisionNo.Width = 100
        RevisionNo.ReadOnly = True
        RevisionNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(RevisionNo)

        '==================================================================================
        BOMCode = New GridViewTextBoxColumn()
        BOMCode.FormatString = ""
        BOMCode.HeaderText = "SO Code"
        BOMCode.Name = colSaleOrderNo
        BOMCode.Width = 100
        BOMCode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        BOMCode.TextImageRelation = TextImageRelation.TextBeforeImage
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        BOMCode.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            BOMCode.IsVisible = True
        End If
        gvPP.Columns.Add(BOMCode)

        itemDesc = New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "SO Description"
        itemDesc.Name = colSaleOrderDesc
        itemDesc.Width = 100
        itemDesc.ReadOnly = True
        itemDesc.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
            itemDesc.IsVisible = True
        End If
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(itemDesc)
        '==========================end here==============================================================

        ItemCode = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            ItemCode.ReadOnly = False
            ItemCode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
            ItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        End If
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(ItemCode)

        itemDesc = New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 100
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(itemDesc)

        UnitCode = New GridViewTextBoxColumn()
        UnitCode.FormatString = ""
        UnitCode.HeaderText = "Unit Code"
        UnitCode.Name = colUnitCode
        UnitCode.Width = 60
        UnitCode.ReadOnly = True
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(UnitCode)


        '==================================================================================
        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Planned Qty"
        MinQty.Name = colPlannedQty
        MinQty.Width = 100
        MinQty.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MinQty.IsVisible = True
        End If
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)

        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Stock Qty"
        MinQty.Name = colStockQty
        MinQty.Width = 100
        MinQty.ReadOnly = True
        MinQty.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MinQty.IsVisible = True
        End If
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)

        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Buffer Qty"
        MinQty.Name = colBufferQty
        MinQty.Width = 100
        MinQty.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MinQty.IsVisible = True
        End If
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)

        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Extra Additional Qty"
        MinQty.Name = colExtraAddQty
        MinQty.Width = 100
        MinQty.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MinQty.IsVisible = True
        End If
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)

        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Net Required Qty"
        MinQty.Name = colNetReqQty
        MinQty.Width = 100
        MinQty.ReadOnly = True
        MinQty.IsVisible = False 'for auto industry purpose
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MinQty.IsVisible = True
        End If
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)
        '======================end here extra column for vinay===============

        MinQty = New GridViewDecimalColumn()
        MinQty.FormatString = ""
        MinQty.HeaderText = "Min Qty"
        MinQty.Name = colMinQty
        MinQty.Width = 100
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        MinQty.IsVisible = True
        'If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
        '    MinQty.IsVisible = False
        'End If
        MinQty.IsVisible = True
        gvPP.Columns.Add(MinQty)

        MaxQty = New GridViewDecimalColumn()
        MaxQty.FormatString = ""
        MaxQty.HeaderText = "Max Qty"
        MaxQty.Name = colMaxQty
        MaxQty.Width = 100
        MaxQty.IsVisible = True
        'If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
        '    MaxQty.IsVisible = False
        'End If
        MaxQty.IsVisible = True
        MaxQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MaxQty)

        Remarks = New GridViewTextBoxColumn()
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 130
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Remarks)


        gvPP.AllowDeleteRow = True
        gvPP.AllowAddNewRow = False
        gvPP.ShowGroupPanel = False
        gvPP.AllowColumnReorder = True
        gvPP.AllowRowReorder = False
        gvPP.EnableSorting = False
        gvPP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPP.MasterTemplate.ShowRowHeaderColumn = False
        gvPP.Rows.AddNew()

        LineNo = Nothing
        ProductionLineCode = Nothing
        BOMCode = Nothing
        ItemCode = Nothing
        itemDesc = Nothing
        RevisionNo = Nothing
        MinQty = Nothing
        MaxQty = Nothing
        UnitCode = Nothing
        Remarks = Nothing
    End Sub

    Private Sub frmProductionPlanning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                SavingData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                funClose()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
            End If

            If e.KeyCode = Keys.F12 AndAlso clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal AndAlso gvPP.Columns IsNot Nothing AndAlso gvPP.Columns Is gvPP.Columns(colItemCode) Then
                isCellValueChangedOpen = True
                OpenItemFinder(True)
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F12 AndAlso clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal AndAlso gvPP.Columns IsNot Nothing AndAlso gvPP.Columns Is gvPP.Columns(colSaleOrderNo) Then
                isCellValueChangedOpen = True
                OpenSOFinder(True)
                isCellValueChangedOpen = False
            End If

            If e.KeyCode = Keys.F12 AndAlso clsCommon.CompairString(IndustryType, "A") <> CompairStringResult.Equal AndAlso gvPP.Columns IsNot Nothing AndAlso gvPP.Columns Is gvPP.Columns(colProductionLineCode) Then
                Dim strq As String = ""
                strq = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME AS NAME,DESCRIPTION from TSPL_MF_PRODUCTION_LINES "
                gvPP.CurrentRow.Cells(colProductionLineCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvPP.CurrentRow.Cells(colProductionLineCode).Value), "Code", True)
            End If

            If e.KeyCode = Keys.F12 AndAlso gvPP.Columns IsNot Nothing AndAlso gvPP.Columns Is gvPP.Columns(colBOMCode) Then
                Dim item_code = ""
                If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                    item_code = clsCommon.myCstr(gvPP.CurrentRow.Cells(colItemCode).Value)
                End If

                Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForBOM(clsCommon.myCstr(gvPP.CurrentRow.Cells(colBOMCode).Value), False, item_code)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
                    gvPP.CurrentRow.Cells(colBOMCode).Value = obj.BOM_CODE
                    gvPP.CurrentRow.Cells(colRevisionNo).Value = obj.REVISION_NO
                    gvPP.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                    gvPP.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvPP.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                Else
                    gvPP.CurrentRow.Cells(colBOMCode).Value = Nothing
                    gvPP.CurrentRow.Cells(colRevisionNo).Value = Nothing
                    If clsCommon.CompairString(IndustryType, "A") <> CompairStringResult.Equal Then
                        gvPP.CurrentRow.Cells(colItemCode).Value = Nothing
                        gvPP.CurrentRow.Cells(colitemDesc).Value = Nothing
                        gvPP.CurrentRow.Cells(colUnitCode).Value = Nothing
                    End If
                End If
                obj = Nothing
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
        
    End Sub

    Sub funClose()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub frmProductionPlanning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        IndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        MandatoryLineNoMaxMinQtyForProductionPlan = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MandatoryLineNoMaxMinQtyForProductionPlan, clsFixedParameterCode.MandatoryLineNoMaxMinQtyForProductionPlan, Nothing)) = 1, True, False)
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            MyLabel1.Visible = True
            MyLabel2.Visible = True
            txtloc_code.Visible = True
            txtloc_name.Visible = True
            txtplan_to_date.Visible = True
        End If

        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
        'Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        'Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
        'Me.gvPP.Rows.Clear()
        'Me.gvPP.Rows.AddNew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmProductionPlanningSTD Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionPlanningSTD)
        ElseIf formtype = clsUserMgtCode.frmProductionPlanningPepsi Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionPlanningPepsi)
        End If
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
        txtPlannedBy.Value = ""
        lblPlannedByName.Text = ""
        lblCreatedByName.Text = ""
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        Me.txtDescription.Text = ""

        Dim serverDate As Date
        serverDate = clsCommon.GETSERVERDATE(Nothing)

        Me.dtpBOMDate.Value = serverDate
        Me.dtpPlanForDate.Value = serverDate
        Me.txtComments.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        Me.gvPP.Rows.Clear()
        Me.gvPP.Rows.AddNew()
        txtloc_code.Value = ""
        txtloc_name.Text = ""
        txtplan_to_date.Text = clsCommon.GETSERVERDATE(Nothing).AddMonths(1)

        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            funReset()
            txtCode.MyReadOnly = True
            btnsave.Text = "Save"
            btnsave.Enabled = True
            btndelete.Enabled = False
            btnPost.Enabled = False
            isCellValueChangedOpen = False

            obj = clsProductionPlanning.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_PLAN_CODE) > 0) Then
                isCellValueChangedOpen = True
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
                txtCode.Value = obj.PROD_PLAN_CODE
                Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
                Me.txtComments.Text = clsCommon.myCstr(obj.COMMENTS)
                Me.dtpBOMDate.Value = obj.PLANNING_DATE
                Me.dtpPlanForDate.Value = obj.PLAN_FOR_DATE
                Me.lblCreatedByName.Text = obj.CREATED_BY
                Me.txtPlannedBy.Value = obj.PLANNED_BY
                Me.lblPlannedByName.Text = obj.PLANNED_BY_NAME

                txtloc_code.Value = obj.Location_Code
                txtloc_name.Text = clsLocation.GetName(obj.Location_Code, Nothing)
                If clsCommon.myLen(obj.PLAN_TO_DATE) > 0 Then
                    txtplan_to_date.Text = obj.PLAN_TO_DATE
                End If

                If obj.POSTED = True Then
                    Me.lblApprovedByName.Text = obj.APPROVED_BY
                Else
                    Me.lblApprovedByName.Text = ""
                End If

                gvPP.Rows.Clear()
                If (clsProductionPlanning.ObjList IsNot Nothing AndAlso clsProductionPlanning.ObjList.Count > 0) Then
                    For Each obj As clsProductionPlanning In clsProductionPlanning.ObjList
                        gvPP.Rows.AddNew()

                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colProductionLineCode).Value = obj.PRODUCTION_LINE_CODE
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Value = obj.BOM_CODE
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRevisionNo).Value = obj.BOM_REVISION_NO
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Value = obj.ITEM_CODE
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMinQty).Value = obj.MIN_QUANTITY
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Value = obj.MAX_QUANTITY
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colUnitCode).Value = obj.UNIT_CODE
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS

                        '>XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSaleOrderNo).Value = obj.SO_Id
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSaleOrderDesc).Value = obj.SO_Desc
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlannedQty).Value = obj.PLAN_QTY
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colStockQty).Value = clsCommon.myCdbl(clsProductionPlanning.GetItemBalance(txtloc_code.Value, obj.ITEM_CODE, obj.UNIT_CODE))
                        If btnPost.Enabled = False AndAlso btnsave.Enabled = False Then
                            gvPP.Rows(gvPP.Rows.Count - 1).Cells(colStockQty).Value = obj.stock_qty
                        End If
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBufferQty).Value = obj.Buffer_Qty
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colExtraAddQty).Value = obj.Extra_Add_Qty
                        gvPP.Rows(gvPP.Rows.Count - 1).Cells(colNetReqQty).Value = obj.Net_Req_Qty
                    Next
                Else
                    gvPP.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            isCellValueChangedOpen = False
        End Try
        gvPP.Rows.AddNew()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Try
            If AllowToSave() Then
                For Each grow As GridViewRowInfo In gvPP.Rows
                    Dim strNetQty As Double = clsCommon.myCdbl(grow.Cells(colPlannedQty).Value) - clsCommon.myCdbl(grow.Cells(colStockQty).Value) + clsCommon.myCdbl(grow.Cells(colBufferQty).Value) + clsCommon.myCdbl(grow.Cells(colExtraAddQty).Value)
                    If clsCommon.myCdbl(strNetQty) >= 0 Then
                        grow.Cells(colNetReqQty).Value = strNetQty
                    Else
                        grow.Cells(colNetReqQty).Value = 0
                    End If
                    
                Next

                Dim obj As New clsProductionPlanning
                obj.PROD_PLAN_CODE = Me.txtCode.Value
                obj.DESCRIPTION = Me.txtDescription.Text
                obj.COMMENTS = Me.txtComments.Text
                obj.PLANNING_DATE = Me.dtpBOMDate.Value
                obj.PLAN_FOR_DATE = Me.dtpPlanForDate.Value
                obj.CREATED_BY = Me.lblCreatedBy.Text
                obj.PLANNED_BY = clsCommon.myCstr(Me.txtPlannedBy.Value)

                obj.Location_Code = clsCommon.myCstr(txtloc_code.Value)
                If clsCommon.myLen(obj.Location_Code) > 0 Then
                    obj.PLAN_TO_DATE = clsCommon.myCDate(txtplan_to_date.Text)
                End If


                Dim obj1 As clsProductionPlanning
                ObjList = New List(Of clsProductionPlanning)
                For Each grow As GridViewRowInfo In gvPP.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 Then
                        obj1 = New clsProductionPlanning()

                        obj1.PROD_PLAN_CODE = txtCode.Value
                        obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        obj1.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colProductionLineCode).Value)
                        obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        obj1.BOM_REVISION_NO = clsCommon.myCstr(grow.Cells(colRevisionNo).Value)
                        obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                        obj1.MIN_QUANTITY = clsCommon.myCdbl(grow.Cells(colMinQty).Value)
                        obj1.MAX_QUANTITY = clsCommon.myCdbl(grow.Cells(colMaxQty).Value)
                        obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                        obj1.PLAN_QTY = clsCommon.myCdbl(grow.Cells(colPlannedQty).Value)
                        obj1.SO_Id = clsCommon.myCstr(grow.Cells(colSaleOrderNo).Value)
                        obj1.SO_Desc = clsCommon.myCstr(grow.Cells(colSaleOrderDesc).Value)
                        obj1.Buffer_Qty = clsCommon.myCdbl(grow.Cells(colBufferQty).Value)
                        obj1.Extra_Add_Qty = clsCommon.myCdbl(grow.Cells(colExtraAddQty).Value)
                        obj1.Net_Req_Qty = clsCommon.myCdbl(grow.Cells(colNetReqQty).Value)
                        obj1.stock_qty = clsCommon.myCdbl(grow.Cells(colStockQty).Value)

                        If clsCommon.myLen(obj1.ITEM_CODE) > 0 Then
                            ObjList.Add(obj1)
                        End If

                    End If
                Next
                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))
                If issaved = True Then
                    'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                    LoadData(obj.PROD_PLAN_CODE, NavigatorType.Current)
                    Return True
                End If

                'Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
        Return False
    End Function

    Function AllowToSave(Optional ByVal chk_post As Boolean = False) As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(dtpBOMDate.Value, Nothing) = False Then
            dtpBOMDate.Select()
            Return False
        End If
        '===========================================================
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE = '" + txtCode.Value + "' "
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

        If clsCommon.myLen(txtPlannedBy.Value) <= 0 Then
            myMessages.blankValue("Planned By")
            txtCode.Focus()
            Return False
        End If

        For Each grow As GridViewRowInfo In gvPP.Rows
            'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
            '    ii += 1

            '    ObjList.Add(obj)
            'End If

        Next

        If ObjList Is Nothing Then
            Return False
        End If

        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then

            If clsCommon.myLen(txtloc_code.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select location.")
                txtloc_code.Focus()
                txtloc_code.Select()
                Return False
            End If

            If clsCommon.myLen(txtplan_to_date.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Select plan to date.")
                txtplan_to_date.Focus()
                txtplan_to_date.Select()
                Return False
            End If

            For ii As Integer = 0 To gvPP.Rows.Count - 1
                If ii = 0 AndAlso clsCommon.myLen(gvPP.Rows(ii).Cells(colItemCode).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Fill atleast one item in grid for planning.")
                    gvPP.CurrentRow = gvPP.Rows(ii)
                    Return False
                End If

                Dim icode As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colItemCode).Value)
                Dim bomcode As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colBOMCode).Value)
                Dim SOcode As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colSaleOrderNo).Value)
                Dim planqty As Decimal = clsCommon.myCdbl(gvPP.Rows(ii).Cells(colPlannedQty).Value)
                Dim lineNo As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colProductionLineCode).Value)
                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
                    If clsCommon.myLen(lineNo) <= 0 Then
                        clsCommon.MyMessageBoxShow("Production Line Code can not be blank for itemcode '" + icode + "'")
                        Return False
                    End If
                End If

                If clsCommon.myLen(icode) > 0 AndAlso chk_post = True AndAlso clsCommon.myLen(bomcode) <= 0 Then
                    clsCommon.MyMessageBoxShow("No BOM detail exist for item " + clsCommon.myCstr(gvPP.Rows(ii).Cells(colitemDesc).Value) + " at line no. " + clsCommon.myCstr(ii + 1) + ".")
                    gvPP.CurrentRow = gvPP.Rows(ii)
                    Return False
                End If
                If clsCommon.myLen(icode) > 0 AndAlso planqty <= 0 Then
                    clsCommon.MyMessageBoxShow("Fill planned quantity for item " + clsCommon.myCstr(gvPP.Rows(ii).Cells(colitemDesc).Value) + " at line no. " + clsCommon.myCstr(ii + 1) + ".")
                    gvPP.CurrentRow = gvPP.Rows(ii)
                    Return False
                End If

                For jj As Integer = ii + 1 To gvPP.Rows.Count - 1
                    Dim oldicode As String = clsCommon.myCstr(gvPP.Rows(jj).Cells(colItemCode).Value)
                    Dim oldbomcode As String = clsCommon.myCstr(gvPP.Rows(jj).Cells(colBOMCode).Value)
                    Dim oldSOcode As String = clsCommon.myCstr(gvPP.Rows(jj).Cells(colSaleOrderNo).Value)

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(bomcode, oldbomcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(SOcode, oldSOcode) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Duplicate data at line no. " + clsCommon.myCstr(jj + 1) + ".")
                        gvPP.CurrentRow = gvPP.Rows(jj)
                        Return False
                    End If
                Next
            Next
        Else
            If MandatoryLineNoMaxMinQtyForProductionPlan Then
                For ii As Integer = 0 To gvPP.Rows.Count - 1
                    If ii = 0 AndAlso clsCommon.myLen(gvPP.Rows(ii).Cells(colItemCode).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Fill atleast one item in grid for planning.")
                        gvPP.CurrentRow = gvPP.Rows(ii)
                        Return False
                    End If
                    Dim icode As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colItemCode).Value)
                    Dim lineNo As String = clsCommon.myCstr(gvPP.Rows(ii).Cells(colProductionLineCode).Value)
                    Dim minQty As String = clsCommon.myCdbl(gvPP.Rows(ii).Cells(colMinQty).Value)
                    Dim maxQty As Decimal = clsCommon.myCdbl(gvPP.Rows(ii).Cells(colMaxQty).Value)
                    If clsCommon.myLen(icode) > 0 Then
                        If clsCommon.myLen(lineNo) <= 0 Then
                            clsCommon.MyMessageBoxShow("Production Line Code can not be blank for itemcode '" + icode + "'")
                            Return False
                        End If
                        If minQty = 0 Then
                            clsCommon.MyMessageBoxShow("min qty should be greter then zero for itemcode '" + icode + "'")
                            Return False
                        End If
                        If maxQty = 0 Then
                            clsCommon.MyMessageBoxShow("max qty should be greter then zero for itemcode '" + icode + "'")
                            Return False
                        End If
                    End If
                Next
            End If
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
                If (clsProductionPlanning.DeleteData(txtCode.Value)) Then
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


    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim columnnames As String = ""
            If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                columnnames = ",T1.PLAN_TO_DATE,T1.Location_Code"
            End If

            Dim qry As String = "SELECT T1.PROD_PLAN_CODE AS CODE,T1.DESCRIPTION,T1.COMMENTS,T1.PLANNING_DATE,T1.PLANNED_BY,T2.EMP_NAME AS PLLANED_BY_NAME,T1.PLAN_FOR_DATE " + columnnames + "," & _
            " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_PRODUCTION_PLAN_HEAD  T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2  ON T1.PLANNED_BY=T2.EMP_CODE "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_PRODUCTION_PLAN_HEAD", qry, "Code", "", txtCode.Value, "convert(date, PLANNING_DATE,103) desc, Code desc", isButtonClicked)
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
            If (myMessages.postConfirm()) Then
                If AllowToSave(True) Then
                    SavingData(True)
                    If (clsProductionPlanning.PostData(txtCode.Value, True)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Posted")
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
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

    Private Sub OpenItemFinder(ByVal isButtonClicked As Boolean)
        Dim icode As String = clsCommon.myCstr(gvPP.CurrentRow.Cells(colItemCode).Value)
        Dim socode As String = clsCommon.myCstr(gvPP.CurrentRow.Cells(colSaleOrderNo).Value)
        Dim bomcode As String = clsCommon.myCstr(gvPP.CurrentRow.Cells(colBOMCode).Value)
        Dim iname As String = ""
        Dim iunit As String = ""
        Dim revision_no As String = ""
        Dim planqty As Decimal = clsCommon.myCdbl(gvPP.CurrentRow.Cells(colPlannedQty).Value)
        Dim stockqty As Decimal = 0
        Dim whrcls As String = " item_type in ('F','S') and active='1' "

        If clsCommon.myLen(socode) > 0 Then
            whrcls += " and item_code in (select item_code from tspl_sd_sales_order_detail where document_code='" + socode + "')"
        End If
        If clsCommon.myLen(bomcode) > 0 Then
            whrcls += " and item_code in (select prod_item_code from tspl_mf_bom_head where bom_code='" + bomcode + "')"
        End If

        icode = clsItemMaster.getFinder(whrcls, icode, isButtonClicked)
        If clsCommon.myLen(icode) > 0 Then
            iname = clsItemMaster.GetItemName(icode, Nothing)
            bomcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 bom_code from tspl_mf_bom_head where posted='1' and prod_item_code='" + icode + "' and (convert(date,'" + clsCommon.myCDate(dtpPlanForDate.Text) + "',103) between convert(date,start_date,103) and convert(date,isnull(end_date,DATEADD(year,1,start_date)),103))"))
            revision_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select revision_no from tspl_mf_bom_head where bom_code='" + bomcode + "'"))
            iunit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_mf_bom_head where bom_code='" + bomcode + "'"))
            If planqty <= 0 Then
                planqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select prod_quantity from tspl_mf_bom_head where bom_code='" + bomcode + "'"))
            End If

            stockqty = clsCommon.myCdbl(clsProductionPlanning.GetItemBalance(txtloc_code.Value, icode, iunit))
        End If

        '===add from date in cond. is left
        Dim qry As String = "select document_code,description from tspl_sd_sales_order_head where 1=1 " & _
        " and bill_to_location='" + txtloc_code.Value + "' and (convert(date,document_date,103) between convert(date,'" + clsCommon.myCDate(dtpPlanForDate.Text) + "',103) and convert(date,'" + clsCommon.myCDate(txtplan_to_date.Text) + "',103)) "
        If clsCommon.myLen(socode) > 0 Then
            qry += " and document_code='" + socode + "' "
        Else
            qry += " and document_code in (select document_code from tspl_sd_sales_order_detail where item_code='" + icode + "') "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim introw As Integer = gvPP.CurrentRow.Index

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvPP.Rows(introw).Cells(colItemCode).Value = icode
                gvPP.Rows(introw).Cells(colitemDesc).Value = iname
                gvPP.Rows(introw).Cells(colBOMCode).Value = bomcode
                gvPP.Rows(introw).Cells(colRevisionNo).Value = revision_no
                gvPP.Rows(introw).Cells(colUnitCode).Value = iunit
                gvPP.Rows(introw).Cells(colPlannedQty).Value = planqty
                gvPP.Rows(introw).Cells(colStockQty).Value = stockqty
                gvPP.Rows(introw).Cells(colSaleOrderNo).Value = clsCommon.myCstr(dr("document_code"))
                gvPP.Rows(introw).Cells(colSaleOrderDesc).Value = clsCommon.myCstr(dr("description"))
                gvPP.Rows(introw).Cells(colBufferQty).Value = Nothing
                gvPP.Rows(introw).Cells(colExtraAddQty).Value = Nothing
                gvPP.Rows(introw).Cells(colNetReqQty).Value = Nothing
                gvPP.Rows(introw).Cells(colRemarks).Value = Nothing
                gvPP.Rows(introw).Cells(colProductionLineCode).Value = Nothing

                introw += 1
            Next
        Else
            gvPP.Rows(introw).Cells(colItemCode).Value = icode
            gvPP.Rows(introw).Cells(colitemDesc).Value = iname
            gvPP.Rows(introw).Cells(colBOMCode).Value = bomcode
            gvPP.Rows(introw).Cells(colRevisionNo).Value = revision_no
            gvPP.Rows(introw).Cells(colUnitCode).Value = iunit
            gvPP.Rows(introw).Cells(colPlannedQty).Value = planqty
            gvPP.Rows(introw).Cells(colStockQty).Value = stockqty
            gvPP.Rows(introw).Cells(colSaleOrderNo).Value = Nothing
            gvPP.Rows(introw).Cells(colSaleOrderDesc).Value = Nothing
            gvPP.Rows(introw).Cells(colBufferQty).Value = Nothing
            gvPP.Rows(introw).Cells(colExtraAddQty).Value = Nothing
            gvPP.Rows(introw).Cells(colNetReqQty).Value = Nothing
            gvPP.Rows(introw).Cells(colRemarks).Value = Nothing
            gvPP.Rows(introw).Cells(colProductionLineCode).Value = Nothing
        End If
    End Sub

    Private Sub OpenSOFinder(ByVal isButtonClicked As Boolean)
        Dim socode As String = ""
        Dim whrcls As String = ""
        Dim icode As String = clsCommon.myCstr(gvPP.CurrentRow.Cells(colItemCode).Value)
        socode = clsCommon.myCstr(gvPP.CurrentRow.Cells(colSaleOrderNo).Value)

        Dim qry As String = "select document_code as Code,Description,document_date as [Date],Customer_Code as [Customer],bill_to_location as [Bill Location] from tspl_sd_sales_order_head "
        whrcls = " bill_to_location='" + txtloc_code.Value + "' and (convert(date,document_date,103) between convert(date,'" + clsCommon.myCDate(dtpPlanForDate.Text) + "',103) and convert(date,'" + clsCommon.myCDate(dtpPlanForDate.Text) + "',103)) "
        If clsCommon.myLen(icode) > 0 Then
            whrcls += " and document_code in (select document_code from tspl_sd_sales_order_detail where item_code='" + icode + "')"
        End If

        socode = clsCommon.ShowSelectForm("PLANSOFND", qry, "Code", whrcls, socode, "Code", isButtonClicked)

        gvPP.CurrentRow.Cells(colSaleOrderNo).Value = socode
        gvPP.CurrentRow.Cells(colSaleOrderDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_sd_sales_order_head where document_code='" + socode + "'"))
    End Sub

    Private Sub gvPP_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPP.CellValueChanged
        Try
            If gvPP.CurrentRow Is Nothing Then
                Exit Sub
            End If

            If clsCommon.myLen(gvPP.CurrentRow.Cells(0).Value) <= 0 Then
                gvPP.CurrentRow.Cells(0).Value = gvPP.RowCount
            End If

            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True

                If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                    If e.Column Is gvPP.Columns(colItemCode) Then
                        OpenItemFinder(False)
                    End If
                    If e.Column Is gvPP.Columns(colSaleOrderNo) Then
                        OpenSOFinder(False)
                    End If

                    If e.Column Is gvPP.Columns(colPlannedQty) OrElse e.Column Is gvPP.Columns(colStockQty) OrElse e.Column Is gvPP.Columns(colBufferQty) OrElse e.Column Is gvPP.Columns(colExtraAddQty) Then
                        Dim strNetQty As Double = clsCommon.myCdbl(gvPP.CurrentRow.Cells(colPlannedQty).Value) - clsCommon.myCdbl(gvPP.CurrentRow.Cells(colStockQty).Value) + clsCommon.myCdbl(gvPP.CurrentRow.Cells(colBufferQty).Value) + clsCommon.myCdbl(gvPP.CurrentRow.Cells(colExtraAddQty).Value)
                        If clsCommon.myCdbl(strNetQty) >= 0 Then
                            gvPP.CurrentRow.Cells(colNetReqQty).Value = strNetQty
                        Else
                            gvPP.CurrentRow.Cells(colNetReqQty).Value = 0
                        End If

                    End If
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Then
                        If e.Column Is gvPP.Columns(colProductionLineCode) Then
                            Dim strq As String = ""
                            strq = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME AS NAME,DESCRIPTION from TSPL_MF_PRODUCTION_LINES "
                            gvPP.CurrentRow.Cells(colProductionLineCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvPP.CurrentRow.Cells(colProductionLineCode).Value), "Code", False)
                        End If
                    End If
                Else
                    If e.Column Is gvPP.Columns(colProductionLineCode) Then
                        Dim strq As String = ""
                        strq = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME AS NAME,DESCRIPTION from TSPL_MF_PRODUCTION_LINES "
                        gvPP.CurrentRow.Cells(colProductionLineCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvPP.CurrentRow.Cells(colProductionLineCode).Value), "Code", False)
                    End If

                End If

                If e.Column Is gvPP.Columns(colBOMCode) Then
                    Dim item_code = ""
                    If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                        item_code = clsCommon.myCstr(gvPP.CurrentRow.Cells(colItemCode).Value)
                    End If

                    Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForBOM(clsCommon.myCstr(gvPP.CurrentRow.Cells(colBOMCode).Value), False, item_code)
                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
                        gvPP.CurrentRow.Cells(colBOMCode).Value = obj.BOM_CODE
                        gvPP.CurrentRow.Cells(colRevisionNo).Value = obj.REVISION_NO
                        gvPP.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                        gvPP.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                        gvPP.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                    Else
                        gvPP.CurrentRow.Cells(colBOMCode).Value = Nothing
                        gvPP.CurrentRow.Cells(colRevisionNo).Value = Nothing
                        If clsCommon.CompairString(IndustryType, "A") <> CompairStringResult.Equal Then
                            gvPP.CurrentRow.Cells(colItemCode).Value = Nothing
                            gvPP.CurrentRow.Cells(colitemDesc).Value = Nothing
                            gvPP.CurrentRow.Cells(colUnitCode).Value = Nothing
                        End If
                        
                    End If
                    obj = Nothing
                End If

                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub gvPP_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvPP.CurrentColumnChanged
        If gvPP.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPP.CurrentRow.Index
            gvPP.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvPP.Rows.Count - 1 Then

                gvPP.Rows.AddNew()

                gvPP.CurrentRow = gvPP.Rows(intCurrRow)

            End If
        End If
    End Sub

    Private Sub gvBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvPP.KeyDown
        'If e.KeyData = Keys.Enter Then
        '    Me.gvPP.Rows.Add(1)

        '    gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        'End If
        'If e.KeyData = Keys.Right And gvPP.CurrentCell.ColumnIndex = gvPP.Columns.Count - 1 Then
        '    Me.gvPP.Rows.Add(1)
        '    gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        'End If
    End Sub

    Private Sub txtPlannedBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPlannedBy._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtPlannedBy.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtPlannedBy.Value = OBJEMP.EMP_CODE
                Me.lblPlannedByName.Text = clsEmployeeMaster.GetData(Me.txtPlannedBy.Value, NavigatorType.Current).Emp_Name
            End If
            OBJEMP = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue("Production Plan Code")
        Else
            funPrint()
        End If
    End Sub
    Sub funPrint()
        Try
            Dim qry As String = ""
            qry += " SELECT  T2.PROD_PLAN_CODE,convert(VARCHAR,T2.PLANNING_DATE,103) AS PLANNING_DATE,  T1.PRODUCTION_LINE_CODE,T4.PRODUCTION_LINE_NAME,T1.ITEM_CODE AS "
            qry += " MAIN_ITEM_CODE,T5.Item_Desc AS MAIN_ITEM_DESC,T6.Class_Code AS PACKAGE,T7.Class_Code AS FLAVOUR, "
            qry += " T3.MIN_QUANTITY AS MIN_QTY,T3.MAX_QUANTITY AS MAX_QTY,CONVERT(varchar(5),T3.START_TIME,108) AS START_TIME,T3.SPEED , "
            qry += " (CONVERT(VARCHAR,T2.PLANNING_DATE ,103) + ' ' + CONVERT(varchar(5),T3.END_TIME,108)) AS STOP_TIME,T3.REASON, ('" & objCommonVar.CurrentCompanyName & "')  AS COMPANY_NAME  FROM TSPL_MF_PROD_PLAN_DETAIL T1 INNER JOIN "
            qry += " TSPL_MF_PRODUCTION_PLAN_HEAD T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE  "
            qry += " LEFT JOIN TSPL_MF_BATCH_PP_DETAIL T3 ON T2.PROD_PLAN_CODE=T3.PROD_PLAN_CODE "
            qry += " LEFT JOIN TSPL_MF_PRODUCTION_LINES T4 ON T1.PRODUCTION_LINE_CODE=T4.PRODUCTION_LINE_CODE "
            qry += " LEFT JOIN TSPL_ITEM_MASTER T5 ON T1.ITEM_CODE=T5.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='SIZE') T6 ON T1.ITEM_CODE=T6.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='FLAVOUR') T7 ON T1.ITEM_CODE=T7.Item_Code "
            qry += " WHERE 2=2"
            If txtCode.Value <> "" Then
                qry += " AND  T2.PROD_PLAN_CODE='" & clsCommon.myCstr(txtCode.Value) & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptProductionPlan", "Production Plan")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtloc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtloc_code._MYValidating
        txtloc_code.Value = clsLocation.getFinder(" location_type='Physical' ", txtloc_code.Value, isButtonClicked)
        txtloc_name.Text = clsLocation.GetName(txtloc_code.Value, Nothing)
    End Sub

    Private Sub mnuExportPlanHead_Click(sender As Object, e As EventArgs) Handles mnuExportPlanHead.Click
        '' Export BOM Head
        Try
            Dim qryExport As String
            qryExport = " select PROD_PLAN_CODE as [Plan Code],Location_Code as [Location Code],DESCRIPTION as [Description],COMMENTS as Comments," & _
                        " PLANNING_DATE as [Planning Date],PLAN_FOR_DATE as [Plan For Date],PLAN_TO_DATE as [Plan To Date],PLANNED_BY as [Planned By] " & _
                        " from TSPL_MF_PRODUCTION_PLAN_HEAD"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BOM Head Export")
        End Try
    End Sub

    Private Sub mnuExportPlanDetail_Click(sender As Object, e As EventArgs) Handles mnuExportPlanDetail.Click
        '' Export BOM Detail
        Try
            Dim qryExport As String
            qryExport = " SELECT BH.PROD_PLAN_CODE as [Plan Code],BD.BOM_CODE AS [BOM Code],SO_Id as [SO Code],LINE_NO as [Line No],ITEM_CODE as [Item Code]," & _
                        " ITEM_DESCRIPTION as [Item Desc],PLAN_QTY as [Plan Qty],Buffer_Qty as [Buffer Qty],Extra_Add_Qty as  [Extra Add Qty],UNIT_CODE as [Unit Code],REMARKS as [Remarks] FROM TSPL_MF_PROD_PLAN_DETAIL BD inner join TSPL_MF_PRODUCTION_PLAN_HEAD BH on BD.PROD_PLAN_CODE=BH.PROD_PLAN_CODE  "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BOM Detail Export")
        End Try
    End Sub

    Private Sub mnuImportPlanHead_Click(sender As Object, e As EventArgs) Handles mnuImportPlanHead.Click
        ImportHead()
    End Sub

    Private Sub mnuImportPlanDetail_Click(sender As Object, e As EventArgs) Handles mnuImportPlanDetail.Click
        ImportDetail()
    End Sub
    Function ImportHead() As Boolean
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Plan Code", "Location Code", "Description", "Comments", "Planning Date", "Plan For Date", "Plan To Date", "Planned By") Then
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim isNewEntry As Boolean = False
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsProductionPlanning

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Plan Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Plan Code at row no-" & (grow.Index + 1) & " Must be maximum 30 charecters.")
                    End If
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsProductionPlanning.CheckCode(strCode, Nothing) = False Then
                            Throw New Exception("Plan Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                        Else
                            isNewEntry = False
                            'obj = clsProductionPlanning.GetData(strCode, NavigatorType.Current)
                        End If
                    Else
                        isNewEntry = True
                    End If

                    obj.PROD_PLAN_CODE = strCode
                    '' Location
                    strCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If strCode.Length > 30 Then
                        Throw New Exception("Location Code at row no-" & (grow.Index + 1) & " Must be maximum 30 charecters.")
                    End If
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsLocation.CheckCode(strCode, Nothing) = False Then
                            Throw New Exception("Location Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                        End If                    
                    End If
                    obj.Location_Code = strCode

                    Dim description As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If description.Length > 200 Or (String.IsNullOrEmpty(description)) Then
                        Throw New Exception("Description at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = description

                    description = clsCommon.myCstr(grow.Cells("Comments").Value)                    
                    obj.COMMENTS = description

                    Dim Plan_Date As String = clsCommon.myCDate(grow.Cells("Planning Date").Value)
                    If Plan_Date.Length = 0 Then
                        Throw New Exception("Planing Date at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.PLANNING_DATE = Plan_Date

                    Plan_Date = clsCommon.myCDate(grow.Cells("Plan For Date").Value)
                    If Plan_Date.Length = 0 Then
                        Throw New Exception("Plan For Date at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.PLAN_FOR_DATE = Plan_Date

                    Plan_Date = clsCommon.myCDate(grow.Cells("Plan To Date").Value)
                    If Plan_Date.Length = 0 Then
                        Throw New Exception("Plan To Date at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    obj.PLAN_TO_DATE = Plan_Date
                    Dim Planned_by As String
                    Planned_by = clsCommon.myCstr(grow.Cells("Planned By").Value)
                    If Planned_by.Length > 12 Or Planned_by.Length = 0 Then
                        Throw New Exception("Planned By at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsEmployeeMaster.CheckExistence(Planned_by, Nothing) = False Then
                        Throw New Exception("Planned by " & Planned_by & " at row no-" & (grow.Index + 1) & " is invalid.")
                    End If
                    obj.PLANNED_BY = Planned_by
                    'obj.REVISION_NO = clsBillOfMaterial.GetBOMRevisionNo(obj.PROD_ITEM_CODE, "BOM")
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
        Return False
    End Function
    Function ImportDetail() As Boolean
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Plan Code", "BOM Code", "SO Code", "Line No", "Item Code", "Item Desc", "Plan Qty", "Buffer Qty", "Extra Add Qty", "Unit Code", "Remarks") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim obj As New clsProductionPlanning
                Dim obj1 As New clsProductionPlanning
                Dim ObjList As New List(Of clsProductionPlanning)
                For Each grow As GridViewRowInfo In gv.Rows
                    obj1 = New clsProductionPlanning
                    obj = New clsProductionPlanning
                    Dim Location_Code As String = ""

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Plan Code").Value)
                    If clsProductionPlanning.CheckCode(strCode, Nothing) = False Then
                        Throw New Exception("Plan Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                    Else
                        obj = clsProductionPlanning.GetData(strCode, NavigatorType.Current, trans)
                    End If
                    obj1.PROD_PLAN_CODE = strCode


                    strCode = clsCommon.myCstr(grow.Cells("BOM Code").Value)
                    If clsBillOfMaterial.CheckBOMCode(strCode, Nothing) = False Then
                        Throw New Exception("BOM Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                    End If
                    obj1.BOM_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("SO Code").Value)
                    If clsCommon.myLen(strCode) > 0 Then
                        If clsSalesOrder.CheckCode(strCode, Nothing) = False Then
                            Throw New Exception("SO Code " & strCode & " at row no-" & (grow.Index + 1) & " is invalid.")
                        End If
                    End If
                    obj1.SO_Id = strCode

                    Dim Line_No As Integer = clsCommon.myCdbl(grow.Cells("Line No").Value)
                    If Line_No = 0 Then
                        Throw New Exception("Line No at row no-" & (grow.Index + 1) & " is invalid.")
                    End If
                    obj1.Line_No = Line_No
                    Dim BOM_Field As String = ""
                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If BOM_Field.Length > 50 Or BOM_Field.Length = 0 Then
                        Throw New Exception("Item Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsItemMaster.CheckItemCode(BOM_Field, Nothing) = False Then
                        Throw New Exception("Item Code " & BOM_Field & " at row no-" & (grow.Index + 1) & " is invalid.")
                    End If

                    obj1.ITEM_CODE = BOM_Field
                    Dim Prod_Item_Code As String = clsBillOfMaterial.GetMainItem(obj1.BOM_CODE, trans)
                    If clsCommon.CompairString(BOM_Field, Prod_Item_Code) <> CompairStringResult.Equal Then
                        Throw New Exception("Item Code " & BOM_Field & "  does not match with Main Item of BOM:" & obj1.BOM_CODE & " ->" & Prod_Item_Code & " at row no-" & (grow.Index + 1) & ".")
                    End If
                    BOM_Field = clsCommon.myCstr(grow.Cells("Item Desc").Value)

                    obj1.ITEM_DESCRIPTION = BOM_Field
                    BOM_Field = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If BOM_Field.Length > 12 Then
                        Throw New Exception("Unit Code at row no-" & (grow.Index + 1) & " can not be blank or incorrect.")
                    End If
                    If clsUOMInfo.CheckUnitCode(BOM_Field, Nothing) = False Then
                        Throw New Exception("Invalid Unit Code " & BOM_Field & " at line no-" & (grow.Index + 1) & ".")
                    End If
                    obj1.UNIT_CODE = BOM_Field
                    obj1.stock_qty = clsCommon.myCdbl(clsProductionPlanning.GetItemBalance(obj.Location_Code, obj1.ITEM_CODE, obj1.UNIT_CODE))
                    obj1.PLAN_QTY = clsCommon.myCdbl(grow.Cells("Plan Qty").Value)
                    obj1.Buffer_Qty = clsCommon.myCdbl(grow.Cells("Buffer Qty").Value)
                    obj1.Extra_Add_Qty = clsCommon.myCdbl(grow.Cells("Extra Add Qty").Value)
                    obj1.Net_Req_Qty = obj1.PLAN_QTY + obj1.Buffer_Qty + obj1.Extra_Add_Qty - obj1.stock_qty
                    obj1.BOM_REVISION_NO = clsBillOfMaterial.FindBOMRevisionNo(obj1.BOM_CODE, trans)
                    ObjList.Add(obj1)

                Next
                clsProductionPlanningDetail.SaveDataImport(ObjList, trans)

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try

        End If
        Me.Controls.Remove(gv)
        Return False
    End Function

End Class