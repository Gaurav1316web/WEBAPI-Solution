'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
'========Updated By Rohit July 1,2014 on 4:51 PM.(Remark: Add code to Import and Export Items with Serial.............)========
'==============BM00000003063,Updated By Rohit===========
'---Preeti gupta--ticket no[BM00000004206]
''updation by Richa Agarwal Against Ticket No. BM00000003766
''richa agarwal 10/10/2014 Against Ticket No. BM00000004253
'============BM00000004798==============add section also in case of milk
'====================update by preeti gupta against ticket no [BM00000008677]

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmRawMilkConsumption
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isFlag As Boolean = False
    Public Const RowTypeAdjustmentQty As String = "Quantity"
    Public Const RowTypeAdjustmentCost As String = "Cost"
    Public Const RowTypeAdjustmentBoth As String = "Both"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colIsBatchItem As String = "colIsBatchItem"
    Dim repoicodestatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Const colProductyType As String = "ProductType"
    Const colBinNo As String = "colBinNo"
    Const colICodeStatus As String = "Status"
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colBarCode As String = "COLBARCODE"
    Const colAdjustmentType As String = "COLADJTYPE"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colItemCost As String = "ITEMCOST"
    Const colCost As String = "COLCOST"
    Const colisMRPMandatory As String = "colisMRPMandatory"
    Const colMRP As String = "MRP"
    Const colRemarks As String = "REMARKS"
    Const colComment As String = "COMMENT"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"
    Const colIsPickAutoSrNo As String = "colIsPickAutoSrNo"
    Const colFATPers As String = "FAT Pers"
    Const colFATKG As String = "FAT KG"
    Const colSNFPers As String = "SNFPers"
    Const colSNFKG As String = "SNF Kg"

    '' added by Panch Raj
    Const colPrice_Type As String = "colPrice_Type"
    Const colMCC_Price_Code As String = "colMCC_Price_Code"
    Const colBulk_Price_Code As String = "colBulk_Price_Code"
    Const colfat_Rate As String = "colfat_Rate"
    Const colsnf_Rate As String = "colsnf_Rate"
    Const colfat_Amt As String = "colfat_Amt"
    Const colsnf_Amt As String = "colsnf_Amt"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Public Const strCostTransaction As String = "Store Adjustment"
    Public strDocumentNo As String = ""
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmRawMilkConsumtion)
        If Not (MyBase.isReadFlag) Then

            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
        If btnSave.Visible = True Then
            RmiExport.Enabled = True

        Else
            RmiExport.Enabled = False

        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()

        LoadTransType()

        LoadBlankGrid()
        AddNew()

        '---------------------Done By Monika-------------------
        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            chklocation.Visible = True
        Else
            chklocation.Checked = False
        End If
        '----------------------------------------------------

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
       
    End Sub

    Sub SetLength()
        txtAdjustmentNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtReference.MaxLength = 200
        cboTransType.MaxLength = 1
    End Sub

    Sub LoadTransType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "In"
        dr("Name") = "In"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Out"
        dr("Name") = "Out"
        dt.Rows.Add(dr)

        cboTransType.DataSource = dt
        cboTransType.ValueMember = "Code"
        cboTransType.DisplayMember = "Name"
    End Sub

    Private Function GetAdjustmentType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeAdjustmentQty
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeAdjustmentCost
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeAdjustmentBoth
        dt.Rows.Add(dr)
        Return dt
    End Function
    Private Function GetPriceType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Bulk"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        repoicodestatus.FormatString = ""
        repoicodestatus.HeaderText = "Status"
        repoicodestatus.Name = colICodeStatus
        repoicodestatus.Width = 100
        repoicodestatus.DataSource = FillComboboxGridNEW()
        repoicodestatus.DisplayMember = "value"
        repoicodestatus.ValueMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoicodestatus)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoBarcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBarcode.FormatString = ""
        repoBarcode.HeaderText = "BAR Code"
        repoBarcode.Name = colBarCode
        repoBarcode.IsVisible = False
        repoBarcode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBarcode)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Adjustment Type"
        repoRowType.Name = colAdjustmentType
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetAdjustmentType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoproducttype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoproducttype.FormatString = ""
        repoproducttype.HeaderText = "Product Type"
        repoproducttype.Name = colProductyType
        repoproducttype.Width = 100
        repoproducttype.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoproducttype)

        Dim repoBinNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinNo.FormatString = ""
        repoBinNo.HeaderText = "Bin No"
        repoBinNo.Name = colBinNo
        repoBinNo.ReadOnly = True
        repoBinNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBinNo)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:N2}"
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)



        Dim repoItemCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemCost.FormatString = "{0:N2}"
        repoItemCost.HeaderText = "Item Cost"
        repoItemCost.Name = colItemCost
        repoItemCost.Width = 80
        repoItemCost.Minimum = 0
        repoItemCost.ShowUpDownButtons = False
        repoItemCost.Step = 0
        repoItemCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCost)

        Dim repofatpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatpers.FormatString = ""
        repofatpers.HeaderText = "FAT%"
        repofatpers.Width = 60
        repofatpers.DecimalPlaces = 2
        repofatpers.Name = colFATPers
        gv1.MasterTemplate.Columns.Add(repofatpers)

        Dim repofatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatkg.FormatString = ""
        repofatkg.HeaderText = "FAT KG"
        repofatkg.Width = 60
        repofatkg.DecimalPlaces = 2
        repofatkg.Name = colFATKG
        repofatkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatkg)

        Dim reposnfpers As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfpers.FormatString = ""
        reposnfpers.HeaderText = "SNF%"
        reposnfpers.Width = 60
        reposnfpers.DecimalPlaces = 2
        reposnfpers.Name = colSNFPers
        gv1.MasterTemplate.Columns.Add(reposnfpers)

        Dim reposnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfkg.FormatString = ""
        reposnfkg.HeaderText = "SNF KG"
        reposnfkg.Width = 60
        reposnfkg.DecimalPlaces = 2
        reposnfkg.Name = colSNFKG
        reposnfkg.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfkg)

        Dim repoPriceType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoPriceType.FormatString = ""
        repoPriceType.HeaderText = "Price Type"
        repoPriceType.Name = colPrice_Type
        repoPriceType.Width = 100
        repoPriceType.ReadOnly = False
        repoPriceType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoPriceType.DataSource = GetPriceType()
        repoPriceType.ValueMember = "Code"
        repoPriceType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoPriceType)

        Dim repoMCCPrice As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMCCPrice.FormatString = ""
        repoMCCPrice.HeaderText = "MCC Price Code"
        repoMCCPrice.Name = colMCC_Price_Code
        repoMCCPrice.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoMCCPrice.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMCCPrice.Width = 100
        'repoMCCPrice.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMCCPrice)

        Dim repoBulkPrice As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBulkPrice.FormatString = ""
        repoBulkPrice.HeaderText = "Bulk Price Code"
        repoBulkPrice.Name = colBulk_Price_Code
        repoBulkPrice.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoBulkPrice.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBulkPrice.Width = 100
        'repoBulkPrice.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBulkPrice)



        Dim repofatRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatRate.FormatString = ""
        repofatRate.HeaderText = "FAT Rate"
        repofatRate.Width = 60
        repofatRate.DecimalPlaces = 2
        repofatRate.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repofatRate.Name = colfat_Rate
        'repofatRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatRate)

        Dim repofatAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatAmt.FormatString = ""
        repofatAmt.HeaderText = "FAT Amount"
        repofatAmt.Width = 60
        repofatAmt.DecimalPlaces = 2
        repofatAmt.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        repofatAmt.Name = colfat_Amt
        repofatAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repofatAmt)

        Dim reposnfRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfRate.FormatString = ""
        reposnfRate.HeaderText = "SNF Rate"
        reposnfRate.Width = 60
        reposnfRate.DecimalPlaces = 2
        reposnfRate.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        reposnfRate.Name = colsnf_Rate
        'reposnfRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfRate)

        Dim reposnfAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfAmt.FormatString = ""
        reposnfAmt.HeaderText = "SNF Amount"
        reposnfAmt.Width = 60
        reposnfAmt.DecimalPlaces = 2
        reposnfAmt.FormatString = "{0:n" & clsCommon.myCstr(2) & "}"
        reposnfAmt.Name = colsnf_Amt
        reposnfAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposnfAmt)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = "{0:N2}"
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colCost
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoIsMRPMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsMRPMandatory.HeaderText = "Is MRP Mandatory"
        repoIsMRPMandatory.Name = colisMRPMandatory
        repoIsMRPMandatory.IsVisible = False
        repoIsMRPMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsMRPMandatory.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsMRPMandatory)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ShowUpDownButtons = False
        repoMRP.Step = 0
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Comment"
        repoSpecification.Name = colComment
        repoSpecification.Width = 150
        gv1.MasterTemplate.Columns.Add(repoSpecification)

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsBatchItem)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40


    End Sub

    Private Function FillComboboxGridNEW() As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NEW"
        dr("Value") = "NEW"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OLD"
        dr("Value") = "OLD"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        rbtnExportPosted.Visible = False
        rbtnImportPosted.Visible = False
        cmdEditAndPost.Visible = False
        ChkMilkType.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth

    End Sub

    Sub BlankAllControls()
        ChkMilkType.ReadOnly = True
        ChkMilkType.Checked = True
        chklocation.Checked = False
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtAdjustmentNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        cboTransType.SelectedIndex = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReference.Text = ""

        ''added by richa 09/10/2014
        Dim desc As String = ""
        desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToShowMilkTypeinAdjustmentEntry, clsFixedParameterCode.AllowToShowMilkTypeinAdjustmentEntry, Nothing))
        'If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
        '    ChkMilkType.Visible = True
        '    ChkMilkType.Checked = False
        'Else
        '    ChkMilkType.Visible = False
        '    ChkMilkType.Checked = False

        'End If
        'FndMainLocation.Enabled = False
        RadLabel15.Text = "Location"
        FndMainLocation.Value = ""
        LblMainLocation.Text = ""
        ''======================
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAdjustmentType) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colComment) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colAdjustmentType) Or e.Column Is gv1.Columns(colItemCost) Then
                            If e.Column Is gv1.Columns(colQty) Then
                                OpenBatchItem()
                            End If
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                        If (e.Column Is gv1.Columns(colQty)) Then
                            If Not (clsCommon.myCBool(gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value) AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "In") = CompairStringResult.Equal) Then
                                OpenSerialItem()
                            End If
                        End If
                    End If
                    If (e.Column Is gv1.Columns(colFATPers)) OrElse (e.Column Is gv1.Columns(colQty)) OrElse (e.Column Is gv1.Columns(colfat_Rate)) Then
                        CalFATKG()
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                        End If
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
                            'End If
                        End If
                    End If
                    If (e.Column Is gv1.Columns(colSNFPers)) OrElse (e.Column Is gv1.Columns(colQty)) OrElse (e.Column Is gv1.Columns(colsnf_Rate)) Then
                        CalSNFKG()
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                        End If
                        If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                            'If clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(colfat_Rate).ReadOnly = False
                            'End If
                        End If
                    End If
                    If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        If e.Column Is gv1.Columns(colPrice_Type) Then
                            If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = False
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
                            ElseIf clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = False
                            End If
                        End If
                    Else

                        gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                        gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
                    End If


                    If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        If e.Column Is gv1.Columns(colMCC_Price_Code) Then
                            OpenMCCPriceList(False)
                            If clsCommon.myLen(gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value) > 0 Then

                                If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                                End If

                                Dim arr As New clsFatSnfRateCalculator
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value & "'", Nothing)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value))
                                Else
                                    If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value) And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value) Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colItemCost).Value))
                                    End If
                                End If

                                

                                gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = Math.Round(arr.fatR, 2)
                                gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = Math.Round(arr.FatAmt, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = Math.Round(arr.snfR, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = Math.Round(arr.snfAmt, 2)
                                dtMilkPrice = Nothing
                                arr = Nothing
                            End If
                        ElseIf e.Column Is gv1.Columns(colBulk_Price_Code) Then
                            OpenBulkPriceList(False)
                            If clsCommon.myLen(gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value) > 0 Then

                                If clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
                                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                                    gv1.CurrentRow.Cells(colItemCost).Value = GetMilkRate(gv1.CurrentRow.Cells(colPrice_Type).Value, gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
                                End If

                                Dim arr As New clsFatSnfRateCalculator
                                Dim objPrice As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value, NavigatorType.Current, Nothing)

                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value))
                                Else
                                    If clsCommon.myCdbl(objPrice.Fat_Percentage) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value) And clsCommon.myCdbl(objPrice.Snf_Percentage) = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value) Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(gv1.Rows(e.RowIndex).Cells(colQty).Value, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFATPers).Value), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPers).Value), clsCommon.myCdbl(objPrice.Standard_Rate), clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colItemCost).Value))
                                    End If
                                End If

                                
                                gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = Math.Round(arr.fatR, 2)
                                gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = Math.Round(arr.FatAmt, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = Math.Round(arr.snfR, 2)
                                gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = Math.Round(arr.snfAmt, 2)
                                objPrice = Nothing
                                arr = Nothing
                            End If
                        End If
                    ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Out") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                        '' production costing columns
                        Dim objCost As New MIlkComponentType
                        Dim Loc_code As String = ""
                        Dim Main_Loc_code As String = ""
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            Loc_code = FndMainLocation.Value
                            Main_Loc_code = ""
                        Else
                            Loc_code = txtLocation.Value
                            Main_Loc_code = FndMainLocation.Value
                        End If
                        objCost = clsInventoryMovementNew.GetAvgCost(gv1.Rows(e.RowIndex).Cells(colProductyType).Tag, gv1.Rows(e.RowIndex).Cells(colICode).Value, Loc_code, gv1.Rows(e.RowIndex).Cells(colQty).Value, gv1.Rows(e.RowIndex).Cells(colUnit).Value, gv1.Rows(e.RowIndex).Cells(colFATKG).Value, gv1.Rows(e.RowIndex).Cells(colSNFKG).Value, txtDate.Value, txtDate.Value, True, Nothing)

                        gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = objCost.FAT_Cost / IIf(gv1.Rows(e.RowIndex).Cells(colFATKG).Value <= 0, 1, gv1.Rows(e.RowIndex).Cells(colFATKG).Value)
                        gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = objCost.FAT_Cost
                        gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = objCost.FAT_Cost / IIf(gv1.Rows(e.RowIndex).Cells(colSNFKG).Value <= 0, 1, gv1.Rows(e.RowIndex).Cells(colSNFKG).Value)
                        gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = objCost.SNF_Cost
                        'ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Out") = CompairStringResult.Equal And ChkMilkType.Checked = False Then
                        '    If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                        '        Dim objCost As New MIlkComponentType
                        '        objCost = clsInventoryMovementNew.GetAvgCost(gv1.Rows(e.RowIndex).Cells(colProductyType).Tag, gv1.Rows(e.RowIndex).Cells(colICode).Value, txtLocation.Value, gv1.Rows(e.RowIndex).Cells(colQty).Value, gv1.Rows(e.RowIndex).Cells(colUnit).Value, gv1.Rows(e.RowIndex).Cells(colFATKG).Value, gv1.Rows(e.RowIndex).Cells(colSNFKG).Value, txtDate.Value, txtDate.Value, True, Nothing)

                        '        gv1.Rows(e.RowIndex).Cells(colfat_Rate).Value = objCost.FAT_Cost / IIf(gv1.Rows(e.RowIndex).Cells(colFATKG).Value <= 0, 1, gv1.Rows(e.RowIndex).Cells(colFATKG).Value)
                        '        gv1.Rows(e.RowIndex).Cells(colfat_Amt).Value = objCost.FAT_Cost
                        '        gv1.Rows(e.RowIndex).Cells(colsnf_Rate).Value = objCost.FAT_Cost / IIf(gv1.Rows(e.RowIndex).Cells(colSNFKG).Value <= 0, 1, gv1.Rows(e.RowIndex).Cells(colSNFKG).Value)
                        '        gv1.Rows(e.RowIndex).Cells(colsnf_Amt).Value = objCost.SNF_Cost
                        '    End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub
    Function GetMilkRate(ByVal Price_Type As String, ByVal Price_Code As String) As Decimal
        Dim DtMCCPrice As New DataTable
        Dim objBulkPrice As New clsPriceChartBulkProc
        Dim MilkRate As Decimal = 0
        If clsCommon.CompairString(Price_Type, "MCC") = CompairStringResult.Equal Then
            DtMCCPrice = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & Price_Code & "'")
            MilkRate = ClsJobWorkRMConsum.GetMilkRate(DtMCCPrice.Rows(0).Item("Ratio"), DtMCCPrice.Rows(0).Item("Snf_Ratio"), DtMCCPrice.Rows(0).Item("Fat_Pers"), DtMCCPrice.Rows(0).Item("SNF_Pers"), DtMCCPrice.Rows(0).Item("Milk_Rate"), gv1.CurrentRow.Cells(colFATKG).Value, gv1.CurrentRow.Cells(colSNFKG).Value, gv1.CurrentRow.Cells(colQty).Value)

        ElseIf clsCommon.CompairString(Price_Type, "Bulk") = CompairStringResult.Equal Then
            objBulkPrice = clsPriceChartBulkProc.GetData(Price_Code, NavigatorType.Current, Nothing)
            MilkRate = ClsJobWorkRMConsum.GetMilkRate(objBulkPrice.Fat_Weightage, objBulkPrice.Snf_Weightage, objBulkPrice.Fat_Percentage, objBulkPrice.Snf_Percentage, objBulkPrice.Standard_Rate, gv1.CurrentRow.Cells(colFATKG).Value, gv1.CurrentRow.Cells(colSNFKG).Value, gv1.CurrentRow.Cells(colQty).Value)
        Else
            MilkRate = 0
        End If
        Return MilkRate
    End Function
    Function GetMilkRateImport(ByVal Price_Type As String, ByVal Price_Code As String, ByVal Fatkg As Decimal, ByVal SnfKg As Decimal, ByVal Qty As Decimal) As Decimal
        Dim DtMCCPrice As New DataTable
        Dim objBulkPrice As New clsPriceChartBulkProc
        Dim MilkRate As Decimal = 0
        If clsCommon.CompairString(Price_Type, "MCC") = CompairStringResult.Equal Then
            DtMCCPrice = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" & Price_Code & "'")
            MilkRate = ClsJobWorkRMConsum.GetMilkRate(DtMCCPrice.Rows(0).Item("Ratio"), DtMCCPrice.Rows(0).Item("Snf_Ratio"), DtMCCPrice.Rows(0).Item("Fat_Pers"), DtMCCPrice.Rows(0).Item("SNF_Pers"), DtMCCPrice.Rows(0).Item("Milk_Rate"), Fatkg, SnfKg, Qty)

        ElseIf clsCommon.CompairString(Price_Type, "Bulk") = CompairStringResult.Equal Then
            objBulkPrice = clsPriceChartBulkProc.GetData(Price_Code, NavigatorType.Current, Nothing)
            MilkRate = ClsJobWorkRMConsum.GetMilkRate(objBulkPrice.Fat_Weightage, objBulkPrice.Snf_Weightage, objBulkPrice.Fat_Percentage, objBulkPrice.Snf_Percentage, objBulkPrice.Standard_Rate, Fatkg, SnfKg, Qty)
        Else
            MilkRate = 0
        End If
        Return MilkRate
    End Function

    Sub CalFATKG()
        Try
            Dim pers As Decimal = 0
            Dim kg As Decimal = 0
            Dim qty As Decimal = 0

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            pers = clsCommon.myCdbl(gv1.CurrentRow.Cells(colFATPers).Value)

            If pers > 0 Then
                kg = (qty * pers) / 100
            End If

            gv1.CurrentRow.Cells(colFATKG).Value = kg
            gv1.CurrentRow.Cells(colfat_Amt).Value = gv1.CurrentRow.Cells(colFATKG).Value * gv1.CurrentRow.Cells(colfat_Rate).Value
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub CalSNFKG()
        Try
            Dim pers As Decimal = 0
            Dim kg As Decimal = 0
            Dim qty As Decimal = 0

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            pers = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNFPers).Value)

            If pers > 0 Then
                kg = (qty * pers) / 100
            End If

            gv1.CurrentRow.Cells(colSNFKG).Value = kg
            gv1.CurrentRow.Cells(colsnf_Amt).Value = gv1.CurrentRow.Cells(colSNFKG).Value * gv1.CurrentRow.Cells(colsnf_Rate).Value

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value)
        Dim obj As clsItemMaster
        '' changes by richa agarwal add condition in finder " Product_Type ='MI'"
        If ChkMilkType.Checked Then
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, " Product_Type ='MI'")
        Else
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, " coalesce(Product_Type,'') <>'MI'")
        End If

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            gv1.CurrentRow.Cells(colItemCost).Value = obj.Cost
            gv1.CurrentRow.Cells(colCost).Value = obj.Cost * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            gv1.CurrentRow.Cells(colIsSerialseItem).Value = obj.Is_Serial_Item
            gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value = obj.Is_Pick_Auto_SrNo
            gv1.CurrentRow.Cells(colisMRPMandatory).Value = obj.Is_MRP
            gv1.CurrentRow.Cells(colProductyType).Tag = obj.Product_Type
            gv1.CurrentRow.Cells(colIsBatchItem).Value = obj.Is_Batch_Item
            gv1.CurrentRow.Cells(colBinNo).Value = obj.Rack_No
            gv1.CurrentRow.Cells(colProductyType).Value = ProductType(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + obj.Item_Code + "'")))
            '' done by panch raj against Ticket No: BM00000007708:
            '' check for milk product
            If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                Dim objPer As MIlkComponentType = clsItemMaster.GetItemFatSNF(gv1.CurrentRow.Cells(colICode).Value, Nothing)
                gv1.CurrentRow.Cells(colFATPers).Value = objPer.FAT_Per
                gv1.CurrentRow.Cells(colSNFPers).Value = objPer.SNF_Per
            End If
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Private Function ProductType(ByVal Product_type As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(Product_type, "MI") = CompairStringResult.Equal Then
            values = "Milk"
        ElseIf clsCommon.CompairString(Product_type, "CH") = CompairStringResult.Equal Then
            values = "Cheese"
        ElseIf clsCommon.CompairString(Product_type, "MB") = CompairStringResult.Equal Then
            values = "Melted Butter"
        ElseIf clsCommon.CompairString(Product_type, "CU") = CompairStringResult.Equal Then
            values = "Curd"
        ElseIf clsCommon.CompairString(Product_type, "CA") = CompairStringResult.Equal Then
            values = "Cream"
        ElseIf clsCommon.CompairString(Product_type, "BU") = CompairStringResult.Equal Then
            values = "Butter"
        ElseIf clsCommon.CompairString(Product_type, "BM") = CompairStringResult.Equal Then
            values = "Butter Milk"
        ElseIf clsCommon.CompairString(Product_type, "") = CompairStringResult.Equal Then
            values = "Others"
        End If

        Return values
    End Function

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
            Exit Sub
        End If


        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "

        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("AdjStoreUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
            qry = "select top 1 Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER where Item_Code ='" + strICode + "' and UOM='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        End If
    End Sub
    Sub OpenMCCPriceList(ByVal isButtonClick As Boolean)
        Dim Code As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colMCC_Price_Code).Value)
        If clsCommon.myLen(Code) <= 0 Then
            'common.clsCommon.MyMessageBoxShow("Please select MCC Price Code")
            Exit Sub
        End If


        Dim qry As String = " select * from (select distinct TSPL_MILK_PRICE_MASTER.Price_Code as Code,TSPL_MILK_PRICE_MASTER.Effective_Date as [Price Date]," & _
                            " TSPL_MILK_PRICE_MASTER.Description,TSPL_MILK_PRICE_MASTER.Ratio as [Fat Ratio],TSPL_MILK_PRICE_MASTER.SNF_Ratio as [SNF Ratio]," & _
                            " TSPL_MILK_PRICE_MASTER.FAT_Pers as [Fat %],TSPL_MILK_PRICE_MASTER.SNF_Pers as [SNF %],TSPL_MILK_PRICE_MASTER.Milk_Rate as [Milk Rate] " & _
                            " from TSPL_MILK_PRICE_MASTER where Price_Code in (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.code where Mcc_Code='" & FndMainLocation.Value & "')) Price"

        Dim WhrCls As String = "" ''"TSPL_FAT_SNF_UPLOADER_MASTER.Code='" & FndMainLocation.Value & "'"
        gv1.CurrentRow.Cells(colMCC_Price_Code).Value = clsCommon.ShowSelectForm("AdjStoreMCCPrice", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colMCC_Price_Code).Value), "Code", isButtonClick)

    End Sub
    Sub OpenBulkPriceList(ByVal isButtonClick As Boolean)
        Dim Code As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colBulk_Price_Code).Value)
        If clsCommon.myLen(Code) <= 0 Then
            'common.clsCommon.MyMessageBoxShow("Please select MCC Price Code")
            Exit Sub
        End If


        Dim qry As String = "select Price_Code as Code,Price_Date as [Price Date],Fat_Weightage as [Fat Ratio],Snf_Weightage as [SNF Ratio]," & _
            " Fat_Percentage as [Fat %],Snf_Percentage as [SNF %],Standard_Rate as [Milk Rate] from TSPL_Bulk_Price_MASTER "

        Dim WhrCls As String = "" ''= "Location_Code='" & FndMainLocation.Value & "'"
        gv1.CurrentRow.Cells(colBulk_Price_Code).Value = clsCommon.ShowSelectForm("AdjStoreBulkPrice", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colBulk_Price_Code).Value), "Code", isButtonClick)

    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
        gv1.CurrentRow.Cells(colisMRPMandatory).Value = False
        gv1.CurrentRow.Cells(colProductyType).Value = ""
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = 0
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemCost).Value)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colQty).Value = 0
            End If
        End If
    End Sub

    Private Sub UpdateAllTotals()
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                isCellValueChangedOpen = True
                If ChkMilkType.Checked Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = "None"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).ReadOnly = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).ReadOnly = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).ReadOnly = False
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = "None"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).ReadOnly = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).ReadOnly = False
                End If
                isCellValueChangedOpen = False
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = clsCommon.myCstr(i + 1)
            End If
        Next
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column Is gv1.Columns(colQty) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colQty).ReadOnly = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colQty).ReadOnly = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colQty).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colAdjustmentType) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = True
                End If
            ElseIf e.Column Is gv1.Columns(colCost) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = False
                End If
            ElseIf e.Column Is gv1.Columns(colProductyType) Then
                If clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MI") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
                    gv1.CurrentRow.Cells(colFATPers).ReadOnly = False
                    gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
                    gv1.CurrentRow.Cells(colSNFPers).ReadOnly = False
                ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells(colProductyType).Tag, "MP") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colFATKG).ReadOnly = False
                    gv1.CurrentRow.Cells(colFATPers).ReadOnly = False
                    gv1.CurrentRow.Cells(colSNFKG).ReadOnly = False
                    gv1.CurrentRow.Cells(colSNFPers).ReadOnly = False
                    'gv1.CurrentRow.Cells(colFATKG).Value = Nothing
                    'gv1.CurrentRow.Cells(colFATPers).Value = Nothing
                    'gv1.CurrentRow.Cells(colSNFKG).Value = Nothing
                    'gv1.CurrentRow.Cells(colSNFPers).Value = Nothing
                Else

                    gv1.CurrentRow.Cells(colFATKG).ReadOnly = True
                    gv1.CurrentRow.Cells(colFATPers).ReadOnly = True
                    gv1.CurrentRow.Cells(colSNFKG).ReadOnly = True
                    gv1.CurrentRow.Cells(colSNFPers).ReadOnly = True
                    gv1.CurrentRow.Cells(colFATKG).Value = Nothing
                    gv1.CurrentRow.Cells(colFATPers).Value = Nothing
                    gv1.CurrentRow.Cells(colSNFKG).Value = Nothing
                    gv1.CurrentRow.Cells(colSNFPers).Value = Nothing
                End If
            End If
            If clsCommon.CompairString(cboTransType.SelectedValue, "IN") = CompairStringResult.Equal And ChkMilkType.Checked = True Then
                If e.Column Is gv1.Columns(colPrice_Type) Then
                    If clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                        'gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                        'gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                        gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = False
                        gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
                    ElseIf clsCommon.CompairString(gv1.Rows(e.RowIndex).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                        'gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).Value = ""
                        'gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).Value = ""

                        gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                        gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = False
                    End If
                End If
            Else
                gv1.Rows(e.RowIndex).Cells(colMCC_Price_Code).ReadOnly = True
                gv1.Rows(e.RowIndex).Cells(colBulk_Price_Code).ReadOnly = True
            End If

        Catch ex As Exception
            '        common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        ''richa agarwal 10/10/2014
        If ChkMilkType.Checked Then
            txtLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" + clsLocation.GetSegmentCode(FndMainLocation.Value, Nothing) + "'", txtLocation.Value, isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
            Else
                lblLocation.Text = ""
            End If
            ''==============================
        Else
            Dim qry As String = ""
            Dim whrclas As String = ""
            qry = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
            whrclas = "(Location_Type='Physical'  and GIT_Type<>'Y' and isnull(vendor_code,'')=''"
            If chklocation.Checked Then
                whrclas = "(Location_Type='Physical' and GIT_Type<>'Y' and isnull(vendor_code,'')<>''"
            End If

            If clsCommon.CompairString(MDI.IsLoc_Third_Party, "NO") = CompairStringResult.Equal Then
                whrclas = "(Location_Type='Physical'  and GIT_Type<>'Y'"
            End If

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            whrclas += ") OR CSA_Type='Y'"
            txtLocation.Value = clsCommon.ShowSelectForm("AdjStoreLocation", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
            lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")

        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        UpdateAllTotals()
        If ChkMilkType.Checked = True Then
            Dim qry As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + FndMainLocation.Value + "'")
            If clsCommon.CompairString(qry, "MCC") <> CompairStringResult.Equal Then
                If clsCommon.myLen(txtLocation.Value) <= 0 Then
                    txtLocation.Focus()
                    Throw New Exception("Please select Location")
                End If
            End If
        Else
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select Location")
            End If
        End If
        'If clsCommon.myLen(txtLocation.Value) <= 0 Then
        '    txtLocation.Focus()
        '    Throw New Exception("Please select Location")
        'End If
        Dim arrItemCode As List(Of String) = New List(Of String)

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim dblcost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
            Dim adjustmenttype As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colAdjustmentType).Value)
            Dim Product_Type As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colProductyType).Tag)

            If clsCommon.myLen(strICode) > 0 Then
                If arrItemCode.Contains(strICode) Then
                    Throw New Exception("Duplicate Item " & strICode & " Found at Row No " & (ii + 1))
                Else
                    arrItemCode.Add(strICode)
                End If
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " ar Row No " + clsCommon.myCstr(ii + 1))
                End If

                If clsCommon.CompairString(gv1.Rows(ii).Cells(colProductyType).Value, "Milk") = CompairStringResult.Equal Then
                    If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPers).Value) <= 0 Then
                        Throw New Exception("Please enter FAT% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                    If dblQty > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPers).Value) <= 0 Then
                        Throw New Exception("Please enter SNF% of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                End If

                '=====================BM00000005498============================================================
                If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Quantity") = CompairStringResult.Equal) AndAlso dblQty <= 0 Then
                    Throw New Exception("Fill quanity at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                If (clsCommon.CompairString(adjustmenttype, "Both") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmenttype, "Cost") = CompairStringResult.Equal) AndAlso dblcost <= 0 Then
                    Throw New Exception("Fill cost at row no " + clsCommon.myCstr(ii + 1) + "")
                End If
                If ChkMilkType.Checked And cboTransType.SelectedValue = "I" Then
                    If clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "None") = CompairStringResult.Equal Then
                        Throw New Exception("Select any Price Type at row no " + clsCommon.myCstr(ii + 1) + "")
                    ElseIf clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "MCC") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colMCC_Price_Code).Value) <= 0 Then
                            Throw New Exception("Select any MCC Price at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                            Throw New Exception("Fat rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                            Throw New Exception("SNF rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    ElseIf clsCommon.CompairString(gv1.Rows(ii).Cells(colPrice_Type).Value, "Bulk") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colBulk_Price_Code).Value) <= 0 Then
                            Throw New Exception("Select any Bulk Price at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colfat_Rate).Value) <= 0 Then
                            Throw New Exception("Fat rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(ii).Cells(colsnf_Rate).Value) <= 0 Then
                            Throw New Exception("SNF rate must be greater than 0 at row no " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If
                End If
                '======================================================================

                If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "Out") = CompairStringResult.Equal Then
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double
                    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                        Dim Loc_code As String = ""
                        Dim Main_Loc_code As String = ""
                        If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            Loc_code = FndMainLocation.Value
                            Main_Loc_code = ""
                        Else
                            Loc_code = txtLocation.Value
                            Main_Loc_code = FndMainLocation.Value
                        End If

                        ''richa agarwal 28/02/2016 apply tolerance limit
                        If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                            dblBalQty = clsInventoryMovementNew.getBalance(strICode, Main_Loc_code, Loc_code, txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM)
                            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                                If dblBalQty > 0 Then
                                    dblBalQty = ClsLoadingTanker.GetTolerane(dblBalQty, dblQty)
                                End If
                            End If
                        End If

                        ''-------------------------
                    Else
                        If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                            dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtLocation.Value, txtAdjustmentNo.Value, txtDate.Value, Nothing, strUOM)
                        End If

                    End If

                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            dblEnteredQty += dblQtyInner
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If (clsCommon.CompairString(adjustmenttype, "Cost") <> CompairStringResult.Equal) Then
                        If dblEnteredQty > dblBalQty Then
                            Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        End If
                    End If

                End If
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "In") = CompairStringResult.Equal Then
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsPickAutoSrNo).Value) Then
                            Dim arrOut As List(Of clsSerializeInvenotry) = New List(Of clsSerializeInvenotry)
                            If arrSerailNo Is Nothing OrElse arrSerailNo.Count <= 0 Then
                                For kk As Integer = 1 To dblQty
                                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                    obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                    arrOut.Add(obj)
                                Next
                            Else
                                For kk As Integer = 0 To arrSerailNo.Count - 1
                                    If kk > dblQty - 1 Then
                                        Exit For
                                    Else
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        If clsCommon.myLen(arrSerailNo(kk).Auto_Sr_No) > 0 Then
                                            obj.Auto_Sr_No = arrSerailNo(kk).Auto_Sr_No
                                        Else
                                            obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        End If
                                        arrOut.Add(obj)
                                    End If
                                Next
                                If arrOut.Count < dblQty Then
                                    For kk As Integer = arrOut.Count + 1 To dblQty
                                        Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                        obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(strICode, Nothing)
                                        arrOut.Add(obj)
                                    Next
                                End If
                            End If
                            gv1.Rows(ii).Tag = arrOut
                        End If
                    End If
                    arrSerailNo = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        Throw New Exception("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    End If
                End If

                If clsCommon.myCBool(gv1.Rows(ii).Cells(colisMRPMandatory).Value) AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter MRP for " + strICode + ". At Line No" + clsCommon.myCstr(ii + 1))
                    Return False
                End If
            End If

            If dblQty > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                If arrBatchNo Is Nothing Then
                    Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                Else
                    Dim tQty As Decimal = 0
                    For Each objBatch As clsBatchInventory In arrBatchNo
                        tQty += objBatch.Qty
                    Next
                    If tQty <> dblQty Then
                        Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                    End If
                End If
            End If
        Next

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            '' Anubhooti 09-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                Exit Function
            End If
            If (AllowToSave()) Then
                Dim obj As New ClsJobWorkRMConsum()
                obj.Adjustment_No = txtAdjustmentNo.Value
                obj.Adjustment_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()

                obj.Unit_Code = "ALL"
                ''obj.ItemType = "E" Fill at Detail level

                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)

                obj.chklocation = "N"
                If chklocation.Checked Then
                    obj.chklocation = "Y"
                End If

                If ChkMilkType.Checked Then
                    obj.IsMilkType = 1
                Else
                    obj.IsMilkType = 0
                End If
                obj.MainLocationCode = FndMainLocation.Value
                obj.MainLocationDesc = LblMainLocation.Text
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New ClsJobWorkRMConsumDetails()
                        'objTr.Adjustment_No=
                        objTr.Adjustment_Line_No = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                        objTr.Adjustment_Type = clsCommon.myCstr(grow.Cells(colAdjustmentType).Value).Substring(0, 1) + IIf(clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal, "I", "D")
                        'objTr.Location_Code=Pick in SaveData from header
                        objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        'objTr.Account_Code= Pick in SaveData
                        'objTr.Account_Description=Pick in SaveData
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                        objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)

                        objTr.fat_pers = clsCommon.myCdbl(grow.Cells(colFATPers).Value)
                        objTr.fat_kg = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.snf_pers = clsCommon.myCdbl(grow.Cells(colSNFPers).Value)

                        'objTr.MFG_Date =
                        'objTr.Batch_No=
                        'objTr.Expiry_Date =
                        'objTr.Breakage =
                        'objTr.Breakage_Cost =
                        objTr.ItemType = clsItemMaster.GetStoreAdjustmentItemType(objTr.Item_Code)
                        If isFirstTime Then
                            obj.ItemType = objTr.ItemType
                            isFirstTime = False
                        End If
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        objTr.Itemstatus = clsCommon.myCstr(grow.Cells(colICodeStatus).Value)

                        If clsCommon.myLen(objTr.Itemstatus) <= 0 Then
                            objTr.Itemstatus = "NEW"
                        End If

                        '' Ticket No : BM00000007708 : aded by Panch Raj
                        objTr.Price_Type = clsCommon.myCstr(grow.Cells(colPrice_Type).Value)
                        objTr.MCC_Price_Code = clsCommon.myCstr(grow.Cells(colMCC_Price_Code).Value)
                        objTr.Bulk_Price_Code = clsCommon.myCstr(grow.Cells(colBulk_Price_Code).Value)

                        objTr.fat_Rate = clsCommon.myCdbl(grow.Cells(colfat_Rate).Value)
                        objTr.fat_Amt = clsCommon.myCdbl(grow.Cells(colfat_Amt).Value)
                        objTr.snf_Rate = clsCommon.myCdbl(grow.Cells(colsnf_Rate).Value)
                        objTr.snf_Amt = clsCommon.myCdbl(grow.Cells(colsnf_Amt).Value)
                        objTr.Bin_No = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                        objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, "RM")

                '=============preet Gupta Ticket no.[BM00000005981]========
                If Not isFlag Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Adjustment_No, NavigatorType.Current)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                End If
                'End If

                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New ClsJobWorkRMConsum()
            obj = obj.GetData(strCode, AdjustmentEnum.strCostTransaction, NavTyep, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
                If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If


                txtAdjustmentNo.Value = obj.Adjustment_No
                txtDate.Value = obj.Adjustment_Date
                'obj.Posting_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                'obj.Posted()

                'obj.Unit_Code = "ALL"
                'obj.ItemType = "E"

                If obj.chklocation = "Y" Then
                    chklocation.Checked = True
                Else
                    chklocation.Checked = False
                End If

                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                cboTransType.SelectedValue = obj.Trans_Type

                If obj.IsMilkType = 1 Then
                    ChkMilkType.Checked = True
                Else
                    ChkMilkType.Checked = False
                End If
                ChkMilkType.Enabled = False
                FndMainLocation.Value = obj.MainLocationCode
                LblMainLocation.Text = obj.MainLocationDesc
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsJobWorkRMConsumDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(objTr.Adjustment_Line_No)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        Dim AdjTypeFirstChar As String = objTr.Adjustment_Type.Substring(0, 1)
                        If clsCommon.CompairString(AdjTypeFirstChar, "Q") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentQty
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "C") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentCost
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "B") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Unit_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisMRPMandatory).Value = clsItemMaster.IsMRPItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.Item_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select product_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value = ProductType(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Tag)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).Value = objTr.fat_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).Value = objTr.fat_pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_kg
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).Value = objTr.snf_pers

                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colProductyType).Value), "Milk") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = False
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPers).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPers).ReadOnly = True
                        End If

                        If clsCommon.CompairString(objTr.Itemstatus, "OLD") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "OLD"
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeStatus).Value = "NEW"
                        End If

                        '' aded by Panch Raj
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPrice_Type).Value = objTr.Price_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMCC_Price_Code).Value = objTr.MCC_Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBulk_Price_Code).Value = objTr.Bulk_Price_Code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Rate).Value = objTr.fat_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colfat_Amt).Value = objTr.fat_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Rate).Value = objTr.snf_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsnf_Amt).Value = objTr.snf_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBinNo).Value = objTr.Bin_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objTr.Item_Code)
                    Next

                    If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub OpenBatchItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
            If clsCommon.CompairString("In", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                Dim frm As frmBatchItemIn = New frmBatchItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            Else
                Dim frm As frmBatchItemOut = New frmBatchItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtLocation.Value
                frm.strCurrDocNo = txtAdjustmentNo.Value
                frm.strCurrDocType = "IC-AD"
                frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            End If
        End If
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        'Try
        '    '    If (myMessages.postConfirm()) Then
        '        SaveData()
        '        '' Anubhooti 09-Sep-2014 BM00000003735
        '        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
        '            Exit Sub
        '        End If
        '        If (ClsJobWorkRMConsum.PostData(txtAdjustmentNo.Value, strCostTransaction)) Then
        '            LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        '==============Preet Gupta======================
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Doc_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtAdjustmentNo.Value) > 0 Then
                Doc_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS adjustment_no from TSPL_ADJUSTMENT_HEADER  where TSPL_ADJUSTMENT_HEADER.adjustment_no='" + txtAdjustmentNo.Value + "'"))
                If Doc_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        If SaveData() = False Then
                            Exit Sub
                        End If
                        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                            Exit Sub
                        End If
                        If (ClsJobWorkRMConsum.PostData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction, "RM")) Then
                            LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering Document code")
                End If

            Else
                Throw New Exception("Document code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
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
                If (ClsJobWorkRMConsum.DeleteData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtAdjustmentNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAdjustmentNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + txtAdjustmentNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtAdjustmentNo.MyReadOnly = False
            Else
                txtAdjustmentNo.MyReadOnly = True
            End If
            LoadData(txtAdjustmentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAdjustmentNo._MYValidating
        Dim qry As String = "SELECT Adjustment_No AS [AdjustmentNumber], Adjustment_Date as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location],coalesce(Against_Item_Stock_Conv_Doc,against_Item_Stock_Conversion) as [Against Item Stock Conversion],Against_AP_Invoice_No as [Against AP Invoice No] FROM  TSPL_ADJUSTMENT_HEADER  "
        Dim whrClas As String = " 1=1 and  isnull(AdjustType,'') = 'Consume'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND (Loc_Code in (" + objCommonVar.strCurrUserLocations + ") or  mainlocationcode in (" + objCommonVar.strCurrUserLocations + "))"
        End If
        'whrClas += " AND ItemType IN ('RM', 'OT')"


        txtAdjustmentNo.Value = clsCommon.ShowSelectForm("AdjustmentStoreDoc", qry, "AdjustmentNumber", whrClas, txtAdjustmentNo.Value, "AdjustmentNumber", isButtonClicked)
        LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                OpenUOMList(True)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.E Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                rbtnExportPosted.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.I Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                rbtnImportPosted.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.P Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.StoreADJExportImportAfterPost
            frm.strCode = clsFixedParameterCode.StoreADJExportImportAfterPost
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                cmdEditAndPost.Visible = True
            End If
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
            myMessages.blankValue("Purchase Order No not found to Print")
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Adjustment No not found to Print")
            End If

            Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "' and TSPL_ADJUSTMENT_HEADER.ItemType='E' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quality Increase' else CASE when detail.Adjustment_Type='QD' then 'Quality Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1] from TSPL_ADJUSTMENT_HEADER as Head left outer join TSPL_ADJUSTMENT_DETAIL as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Head.Adjustment_No='" + txtAdjustmentNo.Value + "' order by detail.Adjustment_Line_No "
                dt = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustment", "Adjustment Detail")
            Else
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Adjustment_Type")), "BD") = CompairStringResult.Equal Then
                    qry = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,(TSPL_ADJUSTMENT_HEADER.Adjustment_Date+' '+TSPL_ADJUSTMENT_HEADER.created_time) as Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Item_Quantity,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No " & _
                    " from TSPL_ADJUSTMENT_DETAIL" & _
                    " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No" & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER.Customer_CODE" & _
                    " where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "' ORDER by TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomIssue", "Adjustment Detail")

                Else
                    ''For both Increase OR Receipt Challan
                    Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
                    Dim strACaption As String = "From"
                    Dim strIssueCaption As String = "Empty Receipt"
                    Dim strDateCaption As String = "Receipt Date"
                    qry = "select Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME,MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, '" + strReportName + "' as ReportName,'" + strACaption + "' as ACaption,'" + strIssueCaption + "' as EmptyCaption,'" + strDateCaption + "' as DateCaption,max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date,max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,max(City_Name) as City_Name,max(State_Name) as State_Name from(" & _
                    "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Description ,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Unit_Code,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FC' then Item_Quantity end as FCS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' then Item_Quantity end as FBS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then Item_Quantity end as FSH, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then Item_Quantity end as ECS,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then Item_Quantity end as EBS, 0 as Leak_Qty,TSPL_ADJUSTMENT_DETAIL.Breakage,0 As Short_Qty, Case When TSPL_CUSTOMER_MASTER.Cust_Type_Code Not IN ('F','S') Then (ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0)+ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0)) Else ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) End as Amount, TSPL_ADJUSTMENT_HEADER.EMP_NAME as SalesManName,TSPL_ADJUSTMENT_HEADER.Challan_No,TSPL_ADJUSTMENT_HEADER.Challan_date,TSPL_ADJUSTMENT_HEADER.Vehicle_No,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3,TSPL_CITY_MASTER.City_Name,TSPL_TDS_STATE_MASTER.State_Name from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No= TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
                    qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_ADJUSTMENT_HEADER.Customer_CODE"
                    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
                    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
                    qry += " where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "'  " & _
                    ")xxx group by Adjustment_No,Item_Code order by Item_Desc"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomReceipt", "Adjustment Detail")
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Function chkDuplicateValue(gv As RadGridView, columnName As String) As Boolean
    '    If gv.Rows Then
    'End Function


    Private Sub OpeningwithSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningwithSerial.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Location", "Adjustment Date", "Item Code", "Quantity", "Cost Adjustment", "Third Party Location", "Serial No", "Tag No", "Type", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsJobWorkRMConsum()
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim strAdcode As String = ""

                For Each grow As GridViewRowInfo In gv.Rows


                    Dim strIType As String = "RM"
                    Dim strLoc As String = grow.Cells(0).Value.ToString()
                    If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                        Throw New Exception("Check the value for Location")
                    End If
                    Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                    'Dim strADate As String = grow.Cells(1).Value.ToString()
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells(1).Value, "yyyy/MM/dd")
                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    Dim ItemCode As String = grow.Cells(2).Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"

                    '------------------------------------------------------------------------------------------------
                    Dim thirdparty As String = ""
                    thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

                    If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
                    End If
                    obj.chklocation = thirdparty
                    '--------------------------------------------------------------------------------------------------

                    '-------------------------------
                    Dim struom As String = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    ' Dim struom As String = "UNIT"
                    '---------------------------------------------------------------------------------------------------
                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells(3).Value)
                    Dim CostAd As Decimal = clsCommon.myCdbl(grow.Cells(4).Value)
                    Dim SerialNo As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If String.IsNullOrEmpty(SerialNo) Or SerialNo.Length > 50 Then
                        Throw New Exception("Check the value for Serial No Line No (" & line & ")")
                    End If
                    Dim TagNo As String = clsCommon.myCstr(grow.Cells(7).Value)
                    Dim InoutType As String = clsCommon.myCdbl(grow.Cells(8).Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""

                    'If line = 1 Then
                    '    strAdcode = clsERPFuncationality.GetNextCode(trans, strADate, clsDocType.StoreAdjustment, clsDocTransactionType.StoreAdjustmentAdjustment, strLoc)
                    '    connectSql.RunSpTransaction(trans, "sp_TSPL_ADJUSTMENT_HEADER_insert", New SqlParameter("@AdjustNum", strAdcode), New SqlParameter("@AdjustDate", strADate), New SqlParameter("@PostDate", strADate), New SqlParameter("@Reference", ""), New SqlParameter("@Description", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@CreatedBy", objCommonVar.CurrentUser), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", objCommonVar.CurrentUser), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompanyCode", objCommonVar.CurrentCompanyCode), New SqlParameter("@ReferenceDocument", ""), New SqlParameter("@DocumentNumber", ""), New SqlParameter("@Itemtype", strIType), New SqlParameter("@Unit_Code", "ALL"), New SqlParameter("@EMP_Code", ""), New SqlParameter("@EMP_NAME", ""), New SqlParameter("@Customer_CODE", ""), New SqlParameter("@Customer_NAME", ""), New SqlParameter("@Created_time", strStime), New SqlParameter("@Modified_time", strStime), New SqlParameter("@Vehicle_Code", ""), New SqlParameter("@Vehicle_No", ""), New SqlParameter("@Challan_No", ""), New SqlParameter("@Challan_date", strADate), New SqlParameter("@GateEntry_No", ""), New SqlParameter("@GateEntry_Date", strADate), New SqlParameter("@Loc_Code", strLoc), New SqlParameter("@Loc_Desc", strLocDesc), New SqlParameter("@Trans_type", cboTransType.Text))
                    '    Dim entrydatetime As String = clsCommon.GetPrintDate(strADate, "dd/MM/yyyy hh:mm tt")
                    '    Dim StrQuery As String = "update TSPL_ADJUSTMENT_HEADER  set entrydatetime=convert(datetime,'" + entrydatetime + "',103) where Adjustment_No ='" + strAdcode + "'"
                    '    clsDBFuncationality.ExecuteNonQuery(StrQuery, trans)

                    'End If

                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                    'connectSql.RunSpTransaction(trans, "sp_TSPL_ADJUSTMENT_DETAIL_insert", New SqlParameter("@AdjustNum", strAdcode), New SqlParameter("@AdjustLineNum", line), New SqlParameter("@ItemNum", ItemCode), New SqlParameter("@ItemDesc", iteDesc), New SqlParameter("@AdjustType", AdjType), New SqlParameter("@LocCode", strLoc), New SqlParameter("@ItemQuantity", Iqty), New SqlParameter("@ItemCost", CostAd), New SqlParameter("@UnitCode", struom), New SqlParameter("@AccCode", account), New SqlParameter("@AccDesc", ""), New SqlParameter("@Remarks", rmk), New SqlParameter("@Comments", commt), New SqlParameter("@MRP", StrMRP), New SqlParameter("@MFG_Date", strADate), New SqlParameter("@Batch_No", Batch), New SqlParameter("@Expiry_Date", strADate), New SqlParameter("@Breakage", Bqty), New SqlParameter("@breakage_cost", Bcost), New SqlParameter("@ItemType", strIType), New SqlParameter("@BreakageType", Btype), New SqlParameter("@LeakageQty", Lqty))

                    'line = line + 1
                    Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    If strDescription.Length > 300 Then
                        Throw New Exception("Length of Description can not be greater than 300")
                    End If

                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    If line = 1 Then
                        ''started by priti

                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        'obj.Posting_Date
                        obj.Reference = ""
                        obj.Description = ""
                        'obj.Posted()

                        obj.Unit_Code = "ALL"
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                        obj.Description = strDescription


                        '' ended by priti
                    End If

                    Dim objTr As New ClsJobWorkRMConsumDetails()
                    objTr.arrSrItem = New List(Of clsSerializeInvenotry)()
                    'objTr.Adjustment_No = ""
                    objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = Iqty
                    objTr.Item_Cost = CostAd
                    objTr.Unit_Code = struom
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP

                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty

                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate

                    objTr.ItemType = strIType
                    obj.ItemType = strIType

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    Dim objserial As New clsSerializeInvenotry
                    objserial.Auto_Sr_No = SerialNo
                    objserial.Document_Code = strAdcode
                    objserial.Document_Date = strADate
                    objserial.Item_Code = ItemCode
                    objserial.Document_Type = "IC-AD"
                    'objTr.Location_Code=Pick in SaveData from header
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData

                    objserial.In_Out_Type = InoutType
                    objserial.Line_No = line

                    objserial.Location_Code = strLoc
                    objserial.Tag_No = TagNo
                    'objserial.Parent_Line_No = 1

                    If (clsCommon.myLen(objserial.Item_Code) > 0) Then
                        objTr.arrSrItem.Add(objserial)
                    End If


                    line = line + 1
                Next

                'trans.Commit()
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, True, "RM")
                RadMessageBox.Show(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                myMessages.myExceptions(ex)
                ''trans.Rollback()

            End Try

        End If
    End Sub

    Private Sub RmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmiExport.Click
        'Try
        '    Dim qryExport As String
        '    qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code], '' as [Quantity], '' as [Cost Adjustment],'N' as [Third Party Location]"
        '    transportSql.ExporttoExcel(qryExport, Me)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        'End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        PrintData()
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            PrintData(txtAdjustmentNo.Value, False, False)
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
        Try
            Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + strAdjustmentNo + "' and TSPL_ADJUSTMENT_HEADER.ItemType='E' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No=1"
            Dim TransType As String = clsDBFuncationality.getSingleValue("select TSPL_ADJUSTMENT_HEADER.Trans_Type  from TSPL_ADJUSTMENT_HEADER  where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + strAdjustmentNo + "'")
            If (clsCommon.CompairString(TransType, "Out") = CompairStringResult.Equal) Then
                TransType = "Out"
            Else
                TransType = "In"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quantity Increase' else CASE when detail.Adjustment_Type='QD' then 'Quantity Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1],head.created_by as [Created by],head.modify_by as [Modified by] from TSPL_ADJUSTMENT_HEADER as Head left outer join TSPL_ADJUSTMENT_DETAIL as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Head.Adjustment_No='" + strAdjustmentNo + "' order by detail.Adjustment_Line_No "
                dt = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustment", "Adjustment Detail")
            Else
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Adjustment_Type")), "BD") = CompairStringResult.Equal And IsPreprinted = True Then
                    ''For both Decrese or Empty Issue/Sent

                    If IsPreprinted Then
                        qry = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,(TSPL_ADJUSTMENT_HEADER.Adjustment_Date+' '+TSPL_ADJUSTMENT_HEADER.created_time) as Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Item_Quantity,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No " & _
                    " from TSPL_ADJUSTMENT_DETAIL" & _
                    " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No" & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER.Customer_CODE" & _
                    " where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + strAdjustmentNo + "' ORDER by TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomIssue", "Adjustment Detail")
                    Else
                        ''For both Increase OR Receipt Challan
                        Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
                        Dim strACaption As String = "From"
                        Dim strIssueCaption As String = "Empty Receipt"
                        Dim strDateCaption As String = "Receipt Date"
                        qry = "select max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress,Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME, " & _
                        "MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, " & _
                        "SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ConvQty,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, " & _
                        "SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, " & _
                        "'EMPTY RECEIPT CHALLAN' as ReportName,'From' as ACaption,'Empty Receipt' as EmptyCaption,'Receipt Date' as DateCaption, " & _
                        "max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date, " & _
                        "max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3, " & _
                        "max(City_Name) as City_Name,max(State_Name) as State_Name,0 as ChipBT,sum(isnull(Breakage,0)) as Breakage," & _
                        "sum(isnull(Short_Qty,0)) as Short_Qty,0 as NSBT,0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, " & _
                        "0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,max(Document_No) as  Document_No,max(Docdate) as Docdate,MAX([Created by]) as [Created by],MAX([Modified by]) as [Modified by]  from ( " & _
                        "SELECT  Item_Quantity/Conversion_Factor as ConvQty,TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress, " & _
                        "TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, TSPL_ADJUSTMENT_HEADER.Customer_NAME," & _
                        "TSPL_ADJUSTMENT_HEADER.Description, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description, " & _
                        "TSPL_ADJUSTMENT_DETAIL.Unit_Code, CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'FC' THEN Item_Quantity END AS FCS, " & _
                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'FB' THEN Item_Quantity END AS FBS, " & _
                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'SH' THEN Item_Quantity END AS FSH,  " & _
                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'EC' THEN Item_Quantity END AS ECS, " & _
                        "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'EB' THEN Item_Quantity END AS EBS, 0 AS Leak_Qty, TSPL_ADJUSTMENT_DETAIL.Breakage, " & _
                        "0 AS Short_Qty, CASE WHEN TSPL_CUSTOMER_MASTER.Cust_Type_Code NOT IN ('F', 'S') THEN " & _
                        "(ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0)  + ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0)) ELSE " & _
                        "ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) END AS Amount,TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesManName, " & _
                        "TSPL_ADJUSTMENT_HEADER.Challan_No, " & _
                        "TSPL_ADJUSTMENT_HEADER.Challan_date, TSPL_ADJUSTMENT_HEADER.Vehicle_No, TSPL_CUSTOMER_MASTER.Add1, " & _
                        "TSPL_CUSTOMER_MASTER.Add2, TSPL_CUSTOMER_MASTER.Add3, TSPL_CITY_MASTER.City_Name, " & _
                        "TSPL_TDS_STATE_MASTER.State_Name, TSPL_ADJUSTMENT_HEADER.Document_No,case when Reference_Document='Sale Invoice' then " & _
                        "Sale_Invoice_Date else Transfer_Date end as Docdate,TSPL_ADJUSTMENT_HEADER.created_by as [Created by],TSPL_ADJUSTMENT_HEADER.modify_by as [Modified by] FROM TSPL_TRANSFER_HEAD RIGHT OUTER JOIN " & _
                        "TSPL_ADJUSTMENT_HEADER ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
                        "TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No RIGHT OUTER JOIN " & _
                        "TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No LEFT OUTER JOIN " & _
                        "TSPL_CUSTOMER_MASTER   ON TSPL_ADJUSTMENT_HEADER.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                        "TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code LEFT OUTER JOIN " & _
                        "TSPL_TDS_STATE_MASTER ON TSPL_TDS_STATE_MASTER.State_Code = TSPL_CUSTOMER_MASTER.State  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_ADJUSTMENT_HEADER.Comp_Code  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and TSPL_ADJUSTMENT_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                        "WHERE (TSPL_ADJUSTMENT_HEADER.Adjustment_No = '" + strAdjustmentNo + "')  " & _
                         ") xxx group by Adjustment_No,Item_Code order by Item_Desc"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "rptEmptyOutward", "Empty Issue Challan")
                    End If


                Else
                    ''For both Increase OR Receipt Challan
                    qry = "select '" & TransType & "' as TransType,MAX(LocAdd) as LocAdd,max(Route_Desc) as Route_Desc,MAX(GPCode) as GPCode,max(Transporter_Name) as Transporter_Name,SUM(RGB) as RGB,SUM(Pet) as Pet,SUM(FSHBreakage) as ShellBreak, " & _
                    "max(Tin_No) as  Tin_No,max(CST_LST) as CST_LST,max(Ecc_No) as Ecc_No,max(Comp_Name) as Comp_Name,max(CompAddress) as CompAddress,Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME, " & _
                    "MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, " & _
                    "SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, " & _
                    "SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, " & _
                    "'EMPTY RECEIPT CHALLAN' as ReportName,'From' as ACaption,'Empty Receipt' as EmptyCaption,'Receipt Date' as DateCaption, " & _
                    "max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date, " & _
                    "max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3, " & _
                    "max(City_Name) as City_Name,max(State_Name) as State_Name,0 as ChipBT,sum(isnull(Breakage,0)) as Breakage," & _
                    "sum(isnull(Short_Qty,0)) as Short_Qty,0 as NSBT,0 as HfilledBT,0 as burstBT,0 as Expdt,0 as UnloadBKG, " & _
                    "0 as TRLkg,0 as TRBkg,0 as rust,0 as dirty,0 as MRP,max(Document_No) as  Document_No,max(Docdate) as Docdate, MAX(locPin) as locPin, MAX(locTinNo) as locTinNo, MAX(locCSTNo) as locCSTNo, MAX(locName) as locName,MAX([Created by]) as [Created by],MAX([Modified by]) as [Modified by] " & _
"from ( " & _
                    "SELECT   (TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + Case When TSPL_LOCATION_MASTER.Add4='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add4 end ) as LocAdd, " & _
                    "case when TSPL_SALE_INVOICE_HEAD.Route_Desc='' then TSPL_SALE_INVOICE_HEAD.Cust_Name else TSPL_SALE_INVOICE_HEAD.Route_Desc end as Route_Desc, " & _
                            " TSPL_GATEPASS_DETAIL.GPCode as GPCode,Transporter_Name,0 as RGB,0 as Pet,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'SH' THEN Breakage else  0 END AS FSHBreakage , " & _
                            "TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No, TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress, " & _
                            "TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, TSPL_ADJUSTMENT_HEADER.Customer_NAME," & _
                            "TSPL_ADJUSTMENT_HEADER.Description, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description, " & _
                            "TSPL_ADJUSTMENT_DETAIL.Unit_Code, CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'FC' THEN Item_Quantity END AS FCS, " & _
                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'FB' THEN Item_Quantity END AS FBS, " & _
                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'SH' THEN Item_Quantity END AS FSH,  " & _
                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'EC' THEN Item_Quantity END AS ECS, " & _
                            "CASE WHEN TSPL_ADJUSTMENT_DETAIL.Unit_Code = 'EB' THEN Item_Quantity END AS EBS, 0 AS Leak_Qty, " & _
                            "case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then Breakage  else Breakage end as Breakage, " & _
                            "0 AS Short_Qty, CASE WHEN TSPL_CUSTOMER_MASTER.Cust_Type_Code NOT IN ('F', 'S') THEN " & _
                            "(ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0)  + ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0)) ELSE " & _
                            "ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) END AS Amount,TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesManName, " & _
                            "TSPL_ADJUSTMENT_HEADER.Challan_No, " & _
                            "TSPL_ADJUSTMENT_HEADER.Challan_date, TSPL_ADJUSTMENT_HEADER.Vehicle_No, TSPL_CUSTOMER_MASTER.Add1, " & _
                            "TSPL_CUSTOMER_MASTER.Add2, TSPL_CUSTOMER_MASTER.Add3, TSPL_CITY_MASTER.City_Name, " & _
                            "TSPL_TDS_STATE_MASTER.State_Name, TSPL_ADJUSTMENT_HEADER.Document_No,case when Reference_Document='Sale Invoice' then " & _
                            "Sale_Invoice_Date else Transfer_Date end as Docdate, TSPL_LOCATION_MASTER.Pin_Code as locPin, TSPL_LOCATION_MASTER.TIN_No as locTinNo, TSPL_LOCATION_MASTER.CST_No as locCSTNo, TSPL_LOCATION_MASTER.Location_Desc as locName ,TSPL_ADJUSTMENT_HEADER.created_by as [Created by],TSPL_ADJUSTMENT_HEADER.modify_by as [Modified by] FROM TSPL_TRANSFER_HEAD RIGHT OUTER JOIN " & _
                            "TSPL_ADJUSTMENT_HEADER ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
                            "TSPL_SALE_INVOICE_HEAD ON TSPL_ADJUSTMENT_HEADER.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No RIGHT OUTER JOIN " & _
                            "TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No LEFT OUTER JOIN " & _
                            "TSPL_CUSTOMER_MASTER   ON TSPL_ADJUSTMENT_HEADER.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                            "TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code LEFT OUTER JOIN " & _
                            "TSPL_TDS_STATE_MASTER ON TSPL_TDS_STATE_MASTER.State_Code = TSPL_CUSTOMER_MASTER.State  LEFT OUTER JOIN " & _
                            "TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_ADJUSTMENT_HEADER.Comp_Code  " & _
                            " left outer join TSPL_VEHICLE_MASTER on TSPL_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  left outer join " & _
                            " TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  left outer join " & _
                            " TSPL_ITEM_UOM_DETAIL on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code   " & _
                            " left outer join TSPL_GATEPASS_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_GATEPASS_DETAIL.DocNo " & _
                            " left outer join TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_HEADER.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
                            " WHERE (TSPL_ADJUSTMENT_HEADER.Adjustment_No = '" + strAdjustmentNo + "') and (TSPL_ITEM_UOM_DETAIL.UOM_Code='EB' or TSPL_ITEM_UOM_DETAIL.UOM_Code='SH')  "
                    If IsPreprinted Then
                        qry += " union all " & _
                       "SELECT   (TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + Case When TSPL_LOCATION_MASTER.Add4='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add4 end ) as LocAdd, " & _
                       "case when TSPL_SALE_INVOICE_HEAD.Route_Desc='' then TSPL_SALE_INVOICE_HEAD.Cust_Name else TSPL_SALE_INVOICE_HEAD.Route_Desc end as Route_Desc, " & _
                       "isnull(GPCode,'') as GPCode,Transporter_Name,case when TSPL_SALE_INVOICE_DETAIL.Empty_Value > 0 then " & _
                       "convert(decimal(18,2),(Invoice_Qty/Conversion_Factor)) else 0 end as RGB , " & _
                       "case when TSPL_SALE_INVOICE_DETAIL.Empty_Value > 0 then 0 else convert(decimal(18,2),(Invoice_Qty/Conversion_Factor))  end as Pet , " & _
                       "0 AS FSHBreakage ,'' as Tin_No, '' as CST_LST, '' as Ecc_No, '' as Comp_Name,  '' AS CompAddress, " & _
                       "TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, " & _
                       "TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Description, " & _
                       "'' as Item_Code, '' as Item_Description, '' as Unit_Code,0 AS FCS, 0 AS FBS, 0 AS FSH, " & _
                       "0 AS ECS, 0 AS EBS, 0 AS Leak_Qty, 0 as Breakage, 0 AS Short_Qty, 0 AS Amount, " & _
                       "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesManName, TSPL_ADJUSTMENT_HEADER.Challan_No, " & _
                       "TSPL_ADJUSTMENT_HEADER.Challan_date, TSPL_ADJUSTMENT_HEADER.Vehicle_No, " & _
                       "'' as Add1, '' as Add2, '' as Add3, '' as City_Name, '' as State_Name, " & _
                       "TSPL_ADJUSTMENT_HEADER.Document_No,case when Reference_Document='Sale Invoice' then Sale_Invoice_Date else null end as Docdate , '' as  locPin, '' as locTinNo, '' as locCSTNo, '' as locName " & _
                       ",TSPL_ADJUSTMENT_HEADER.created_by as [Created by],TSPL_ADJUSTMENT_HEADER.modify_by as [Modified by] from TSPL_ADJUSTMENT_HEADER inner join TSPL_SALE_INVOICE_HEAD on " & _
                       "TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No left outer join " & _
                       "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
                       "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                       "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join TSPL_VEHICLE_MASTER on " & _
                       "TSPL_SALE_INVOICE_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  left outer join TSPL_TRANSPORT_MASTER on " & _
                       "TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id left outer join TSPL_GATEPASS_DETAIL on " & _
                       "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_GATEPASS_DETAIL.DocNo   " & _
                       " left outer join TSPL_LOCATION_MASTER on TSPL_ADJUSTMENT_HEADER.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
                       "WHERE (TSPL_ADJUSTMENT_HEADER.Adjustment_No = '" + strAdjustmentNo + "') "
                    End If
                    qry += ") xxx group by Adjustment_No,Item_Code order by Item_Desc"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If IsEmpty Then
                        If IsPreprinted Then
                            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x12, "crptAdjustmentCustomReceiptGun", "Adjustment Detail")
                        Else

                            If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "guntur") = CompairStringResult.Equal) Then
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptAdjustmentCustomReceiptGuntur", "Adjustment Detail")
                            Else
                                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptAdjustmentCustomReceiptVizag", "Adjustment Detail")

                            End If
                        End If
                    Else
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomReceipt", "Adjustment Detail")
                    End If
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If ClsJobWorkRMConsum.ReverseAndUnpost(txtAdjustmentNo.Value, trans) Then
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim Item_type As String = clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'")
            If clsCommon.CompairString("In", clsCommon.myCstr(cboTransType.SelectedValue)) = CompairStringResult.Equal Then
                Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strBinNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colBinNo).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            Else
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtLocation.Value
                frm.strCurrDocNo = txtAdjustmentNo.Value

                frm.strCurrDocType = "IC-AD"
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
        If e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub

    Private Sub txtBarCode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBarCode.Validating
        If clsCommon.myLen(txtBarCode.Text) > 0 Then
            Dim obj As clsBarCodeGenerator = clsBarCodeGenerator.GetData(txtBarCode.Text)
            If obj Is Nothing Then
                clsCommon.MyMessageBoxShow(Me, "Not a Valid Barcode", Me.Text)
                txtBarCode.Text = ""
                Exit Sub
            End If

            Dim isFound As Boolean = False
            Dim CurrentRow As Integer = 1
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(txtBarCode.Text, clsCommon.myCstr(gv1.Rows(ii).Cells(colBarCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) + 1
                    CurrentRow = ii
                    isFound = True
                    Exit For
                End If
            Next
            If Not isFound Then

                gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = obj.Bar_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                OpenICodeList(False)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = obj.Item_Selling_Price
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.Item_MRP
                CurrentRow = gv1.Rows.Count - 1
                For ii As Integer = 1 To gv1.Rows.Count
                    gv1.Rows(ii - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
                Next
                gv1.Rows.AddNew()

            End If

            UpdateCurrentRow(CurrentRow)
            UpdateAllTotals()
            txtBarCode.Text = ""
            txtBarCode.Focus()
        End If
    End Sub

    Private Sub chklocation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocation.ToggleStateChanged
        If chklocation.Checked Then
            txtLocation.Value = ""
            lblLocation.Text = ""
        ElseIf Not chklocation.Checked Then
            txtLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub ExporttoExcelWithSerial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExporttoExcelWithSerial.Click
        Try
            Dim qryExport As String = Nothing
            'qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code], '' as [Quantity], '' as [Cost Adjustment],'N' as [Third Party Location],'' as [Serial No],'' as [Tag No],'' as Type"
            'qryExport = "Select Loc_Code as [Location],Loc_desc as [Location Desc], Adjustment_Date as [Adjustment Date], TSPL_ADJUSTMENT_detail.Item_Code as [Item Code],TSPL_ADJUSTMENT_detail.unit_code as [unit code]," _
            '& " Item_Quantity as [Quantity], Item_Cost as [Cost Adjustment],'N' as [Third Party Location],Auto_Sr_No as [Serial No],Tag_No as [Tag No],'' as Type,TSPL_ADJUSTMENT_HEADER.Description " _
            '& " from TSPL_ADJUSTMENT_HEADER inner join TSPL_ADJUSTMENT_detail on TSPL_ADJUSTMENT_detail.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " _
            '& " left join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_Code=TSPL_ADJUSTMENT_HEADER.Adjustment_No  and TSPL_ADJUSTMENT_detail.Adjustment_Line_No = TSPL_SERIAL_ITEM.Line_No  and TSPL_ADJUSTMENT_detail.Item_Code = TSPL_SERIAL_ITEM.Item_Code  left join TSPL_VISI_MASTER on " _
            '& " TSPL_VISI_MASTER.Serial_No=TSPL_SERIAL_ITEM.Auto_Sr_No"
            'KUNAL > JACKSON > BUG WAS : OUT OF MEMORY EXCEPTION > STATUS FIXED
            qryExport = " Select TSPL_ADJUSTMENT_detail.Location_Code as [Location],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_ADJUSTMENT_HEADER.Adjustment_Date , TSPL_ADJUSTMENT_HEADER.Adjustment_No, TSPL_ADJUSTMENT_detail.Item_Code as [Item Code],TSPL_ADJUSTMENT_detail.unit_code as [unit code], Item_Quantity as [Quantity], Item_Cost as [Cost Adjustment],'N' as [Third Party Location], TSPL_SERIAL_ITEM.Auto_Sr_No as [Serial No],'' as [Tag No],'' as Type  from TSPL_ADJUSTMENT_detail  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_detail.Adjustment_No  LEFT JOIN TSPL_SERIAL_ITEM  on  TSPL_SERIAL_ITEM.Document_Code=TSPL_ADJUSTMENT_detail.Adjustment_No and TSPL_SERIAL_ITEM.Parent_Line_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No and   TSPL_SERIAL_ITEM.Item_Code = TSPL_ADJUSTMENT_DETAIL.Item_Code  left join TSPL_VISI_MASTER on  TSPL_VISI_MASTER.Serial_No=TSPL_SERIAL_ITEM.Auto_Sr_No LEFT JOIN   TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_ADJUSTMENT_detail.Location_Code "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub
    ''added by richa agarwal 09/10/2014
    Private Sub ChkMilkType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkMilkType.ToggleStateChanged
        'If clsCommon.myLen(txtAdjustmentNo.Value) > 0 Then
        'Else

        '    If RadLabel15.Text = "Location" Then
        '        If ChkMilkType.Checked Then
        '            FndMainLocation.Enabled = True
        '            RadLabel15.Text = "Sub Location/Section"
        '            txtLocation.Value = ""
        '            lblLocation.Text = ""
        '            LblMainLocation.Text = ""
        '            LblMainLocation.Text = ""
        '            LoadBlankGrid()
        '            gv1.Rows.AddNew()
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
        '        Else
        '            FndMainLocation.Enabled = False
        '            RadLabel15.Text = "Location"
        '        End If
        '    Else
        '        If ChkMilkType.Checked Then
        '            FndMainLocation.Enabled = True
        '            RadLabel15.Text = "Sub Location/Section"

        '        Else
        '            FndMainLocation.Enabled = False
        '            RadLabel15.Text = "Location"
        '            FndMainLocation.Value = ""
        '            lblLocation.Text = ""
        '            txtLocation.Value = ""
        '            LblMainLocation.Text = ""
        '            LoadBlankGrid()
        '            gv1.Rows.AddNew()
        '            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
        '        End If
        '    End If


        'End If


    End Sub


    Private Sub FndMainLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMainLocation._MYValidating
        FndMainLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' ", FndMainLocation.Value, isButtonClicked)

        If clsCommon.myLen(FndMainLocation.Value) > 0 Then
            LblMainLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndMainLocation.Value & "'"))
        Else
            LblMainLocation.Text = ""
        End If
    End Sub
    '============================

    Private Sub rbtnExportPosted_Click(sender As Object, e As EventArgs) Handles rbtnExportPosted.Click
        Try
            Dim qryExport As String
            qryExport = " select TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Adjustment No],MainLocationCode as [Main Location],Loc_Code as [Location], convert(varchar,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as [Adjustment Date], " & _
                        " TSPL_ADJUSTMENT_HEADER.Trans_Type as [In/Out],TSPL_ADJUSTMENT_HEADER.IsMilkType as [Is Milk],TSPL_ADJUSTMENT_DETAIL.Item_Code as [Item Code],TSPL_ADJUSTMENT_DETAIL.Unit_Code as [Unit Code], " & _
                        " TSPL_ADJUSTMENT_DETAIL.Item_Quantity as [Quantity], TSPL_ADJUSTMENT_DETAIL.Unit_Cost as [Item Cost], TSPL_ADJUSTMENT_DETAIL.Item_Cost as [Amount], TSPL_ADJUSTMENT_HEADER.Third_Party_Location as [Third Party Location],TSPL_ADJUSTMENT_DETAIL.Adjustment_Type as [Adjustment Type(BD/BI/CD/CI/QD/QI)] " & _
                        " from TSPL_ADJUSTMENT_HEADER " & _
                        " inner join TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
            Dim Cond As String = "and Posted='Y' and Against_Bulk_Srn_PI_adjustment is null and Against_AP_Invoice_No is null and Auto_Gen_Againnt_PI_No is null" & _
                                 " and Against_Transfer_In_Doc_No is null and Against_Tanker_Dispatch_Doc_No is null and Against_PI_No_Difference is null " & _
                                 " and Against_PI_No_Difference_Rejected is null "
            transportSql.ExporttoExcel(qryExport, Cond, "[Adjustment No],[Item Code]", Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub rbtnImportPosted_Click(sender As Object, e As EventArgs) Handles rbtnImportPosted.Click
        '' done by panch raj against ticket No: BM00000008191,BM00000008189
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Adjustment No", "Main Location", "Location", "Adjustment Date", "In/Out", "Is Milk", "Item Code", "Unit Code", "Quantity", "Item Cost", "Amount", "Third Party Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Try
                Dim AdjList As New List(Of String)

                '' get list of adjustments
                For Each dr As GridViewRowInfo In gv.Rows
                    If AdjList.Contains(clsCommon.myCstr(dr.Cells("Adjustment No").Value)) = False Then
                        AdjList.Add(clsCommon.myCstr(dr.Cells("Adjustment No").Value))
                    End If
                Next
                For Each strAdcode As String In AdjList
                    'Dim strAdcode As String = ""
                    Dim obj As New ClsJobWorkRMConsum()
                    obj = obj.GetData(strAdcode, "", NavigatorType.Current, trans)
                    obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Adjustment No").Value), strAdcode) <> CompairStringResult.Equal Then
                            Continue For
                        End If
                        Dim line As Integer = 1
                        Dim strIType As String = "RM"
                        Dim IsMilk As String = grow.Cells("Is Milk").Value.ToString()
                        Dim MainLoc As String = ""
                        If clsCommon.CompairString(IsMilk, "1") = CompairStringResult.Equal Then
                            MainLoc = grow.Cells("Main Location").Value.ToString()
                        Else
                            MainLoc = ""
                        End If
                        Dim InOut As String = grow.Cells("In/Out").Value.ToString()

                        Dim strLoc As String = grow.Cells("Location").Value.ToString()
                        If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                            Throw New Exception("Check the value for Location")
                        End If
                        Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ", trans)
                        'Dim strADate As String = grow.Cells(1).Value.ToString()
                        Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "dd/MMM/yyyy")
                        Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                        Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                        Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                        If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                            Throw New Exception("Check the value for Item Code")
                        End If
                        Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')", trans)
                        Dim AdjType As String = "BI"
                        strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, trans))
                        If clsCommon.myLen(strIType) <= 0 Then
                            strIType = "RM"
                        End If
                        '------------------------------------------------------------------------------------------------
                        Dim thirdparty As String = ""
                        thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

                        If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
                            Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
                        End If



                        AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

                        If clsCommon.myLen(AdjType) <= 0 Then
                            Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                        End If
                        If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
                            Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                        End If

                        obj.chklocation = thirdparty
                        'Adjustment Type(BD/BI/CD/CI/QD/QI)
                        '--------------------------------------------------------------------------------------------------

                        '-------------------------------
                        Dim struom As String = grow.Cells("Unit Code").Value.ToString()
                        If clsCommon.myLen(struom) = 0 Then
                            struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                        Else
                            Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(unit_code) from tspl_unit_master where unit_code='" & struom & "'", trans))
                            If intCount = 0 Then
                                Throw New Exception("Unit code is not correct")
                            End If
                        End If

                        ' Dim struom As String = "UNIT"
                        '---------------------------------------------------------------------------------------------------
                        Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)

                        Dim Btype As String = "Select"
                        Dim Bqty As Decimal = 0
                        Dim Bcost As Decimal = 0
                        Dim Lqty As Decimal = 0
                        Dim StrMRP As Decimal = 0
                        Dim MFGDate As String = clsCommon.GETSERVERDATE(trans)
                        Dim Batch As String = ""
                        Dim expdate As String = clsCommon.GETSERVERDATE(trans)
                        Dim rmk As String = ""
                        Dim commt As String = ""

                        Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                        Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        obj.MainLocationCode = MainLoc
                        obj.IsMilkType = IsMilk
                        'obj.Reference = ""
                        'obj.Description = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = InOut

                        Dim objTr As New ClsJobWorkRMConsumDetails()
                        'objTr.Adjustment_No = ""
                        objTr.Adjustment_Line_No = line
                        objTr.Item_Code = ItemCode
                        objTr.Item_Description = ItemDesc
                        objTr.Adjustment_Type = AdjType
                        'objTr.Location_Code=Pick in SaveData from header
                        objTr.Item_Quantity = Iqty
                        If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
                            objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans))
                        Else
                            objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                        End If

                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value) 'Iqty * objTr.Unit_Cost
                        objTr.Unit_Code = struom
                        objTr.Remarks = rmk
                        objTr.Comments = commt
                        objTr.mrp = StrMRP

                        objTr.BreakageType = Btype
                        objTr.Breakage = Bqty
                        objTr.Breakage_Cost = Bcost
                        objTr.LeakageQty = Lqty

                        objTr.MFG_Date = MFGDate
                        objTr.Batch_No = Batch
                        objTr.Expiry_Date = expdate

                        objTr.ItemType = strIType
                        obj.ItemType = strIType

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If

                        line = line + 1
                    Next
                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        Throw New Exception("Please Fill at list one Item")
                    End If
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + obj.Adjustment_No + "'", trans)
                    Dim isSaved As Boolean = True
                    isSaved = isSaved AndAlso ClsJobWorkRMConsum.ReverseAndUnpost(obj.Adjustment_No, trans)
                    isSaved = isSaved AndAlso obj.SaveData(obj, False, "", trans, "RM")
                    isSaved = isSaved AndAlso ClsJobWorkRMConsum.PostData(obj.Adjustment_No, "Store Adjustment", trans, True, VoucherNo)
                Next
                trans.Commit()

                RadMessageBox.Show(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
    End Sub
    Private Function CaptureScreenData() As ClsJobWorkRMConsum
        Dim obj As New ClsJobWorkRMConsum()
        Try
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Store Adjustment", txtDate.Value) = False Then
                Return obj
            End If
            If (AllowToSave()) Then

                obj.Adjustment_No = txtAdjustmentNo.Value
                obj.Adjustment_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()

                obj.Unit_Code = "ALL"
                ''obj.ItemType = "E" Fill at Detail level

                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)

                obj.chklocation = "N"
                If chklocation.Checked Then
                    obj.chklocation = "Y"
                End If

                If ChkMilkType.Checked Then
                    obj.IsMilkType = 1
                Else
                    obj.IsMilkType = 0
                End If
                obj.MainLocationCode = FndMainLocation.Value
                obj.MainLocationDesc = LblMainLocation.Text
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New ClsJobWorkRMConsumDetails()
                        'objTr.Adjustment_No=
                        objTr.Adjustment_Line_No = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colLineNo).Value))
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                        objTr.Adjustment_Type = clsCommon.myCstr(grow.Cells(colAdjustmentType).Value).Substring(0, 1) + IIf(clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal, "I", "D")
                        'objTr.Location_Code=Pick in SaveData from header
                        objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        'objTr.Account_Code= Pick in SaveData
                        'objTr.Account_Description=Pick in SaveData
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                        objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)

                        objTr.fat_pers = clsCommon.myCdbl(grow.Cells(colFATPers).Value)
                        objTr.fat_kg = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.snf_kg = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.snf_pers = clsCommon.myCdbl(grow.Cells(colSNFPers).Value)

                        'objTr.MFG_Date =
                        'objTr.Batch_No=
                        'objTr.Expiry_Date =
                        'objTr.Breakage =
                        'objTr.Breakage_Cost =
                        objTr.ItemType = clsItemMaster.GetStoreAdjustmentItemType(objTr.Item_Code)
                        If isFirstTime Then
                            obj.ItemType = objTr.ItemType
                            isFirstTime = False
                        End If
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        objTr.Itemstatus = clsCommon.myCstr(grow.Cells(colICodeStatus).Value)

                        If clsCommon.myLen(objTr.Itemstatus) <= 0 Then
                            objTr.Itemstatus = "NEW"
                        End If

                        '' Ticket No : BM00000007708 : aded by Panch Raj
                        objTr.Price_Type = clsCommon.myCstr(grow.Cells(colPrice_Type).Value)
                        objTr.MCC_Price_Code = clsCommon.myCstr(grow.Cells(colMCC_Price_Code).Value)
                        objTr.Bulk_Price_Code = clsCommon.myCstr(grow.Cells(colBulk_Price_Code).Value)

                        objTr.fat_Rate = clsCommon.myCdbl(grow.Cells(colfat_Rate).Value)
                        objTr.fat_Amt = clsCommon.myCdbl(grow.Cells(colfat_Amt).Value)
                        objTr.snf_Rate = clsCommon.myCdbl(grow.Cells(colsnf_Rate).Value)
                        objTr.snf_Amt = clsCommon.myCdbl(grow.Cells(colsnf_Amt).Value)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return obj
    End Function

    Private Sub cmdEditAndPost_Click(sender As Object, e As EventArgs) Handles cmdEditAndPost.Click
        '' added by Panch raj against Ticket No:BM00000008482
        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Posted Document.", Me.Text)
            Exit Sub
        ElseIf UsLock1.Status <> ERPTransactionStatus.Posted And UsLock1.Status <> ERPTransactionStatus.Approved Then
            clsCommon.MyMessageBoxShow(Me, "Document must be posted for Edit and Post.", Me.Text)
            Exit Sub
        End If
        Dim objNew As New ClsJobWorkRMConsum
        objNew = CaptureScreenData()
        If Not objNew Is Nothing AndAlso clsCommon.myLen(objNew.Adjustment_No) > 0 Then
            EditAndPost(objNew)
        End If

    End Sub
    Function EditAndPost(ByVal objNew As ClsJobWorkRMConsum) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            'Dim obj As New ClsJobWorkRMConsum()
            'obj = obj.GetData(txtAdjustmentNo.Value, AdjustmentEnum.strCostTransaction, NavigatorType.Current, Nothing)
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + objNew.Adjustment_No + "'", trans)
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso ClsJobWorkRMConsum.ReverseAndUnpost(objNew.Adjustment_No, trans)
            isSaved = isSaved AndAlso objNew.SaveData(objNew, False, "", trans, "RM")
            isSaved = isSaved AndAlso ClsJobWorkRMConsum.PostData(objNew.Adjustment_No, "Store Adjustment", trans, True, VoucherNo)
            trans.Commit()
            RadMessageBox.Show(Me, "Edit and Posted Completed!", Me.Text, MessageBoxButtons.OK)
            Return isSaved
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()
            Return False
        End Try
    End Function

    '====================================added by shivani tyagi against ticket no [BM00000008966]
    Private Sub rmExcelforMilkType_Click(sender As Object, e As EventArgs) Handles rmExcelforMilkType.Click
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Location], '' as [Adjustment Date],'' as [In/Out], '' as [Item Code],'' as [unit code], '' as [Quantity], '' as [Item Cost], '' as [Amount],'' as [Main Location],'BI' as [Adjustment Type(BD/BI/CD/CI/QD/QI)],'' as Description,'' as [FAT %] ,'' as [SNF %],'' as [Price Type],'' as [Price Code],'' as [FAT Rate],'' as [SNF Rate]"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub rmOpeningForMilkType_Click(sender As Object, e As EventArgs) Handles rmOpeningForMilkType.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Location", "Adjustment Date", "In/Out", "Item Code", "unit code", "Quantity", "Item Cost", "Amount", "Main Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)", "Description", "FAT %", "SNF %", "Price Type", "Price Code", "FAT Rate", "SNF Rate") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsJobWorkRMConsum()
                clsCommon.ProgressBarShow()
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                Dim Checkqry As String = ""

                Dim countindex As Integer = 0
                Dim strAdcode As String = ""
                Dim strLocDesc As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New ClsJobWorkRMConsum()
                    obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                    Dim MilkType As Integer = 1
                    Dim strIType As String = "RM"
                    '===================
                    Dim strMainLoc As String = grow.Cells("Main Location").Value.ToString()
                    'If String.IsNullOrEmpty(strMainLoc) Or strMainLoc.Length > 12 Then
                    '    Throw New Exception("Check the value for Main Location")
                    'End If
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        Checkqry = "select count(Location_Code) from tspl_location_master where Location_Code='" + strMainLoc + "'"
                        countindex = clsDBFuncationality.getSingleValue(Checkqry, trans)
                        If (countindex) <= 0 Then
                            Throw New Exception("Main Location Code Is Invalid Or Does Not Exist")
                        End If
                    End If

                    Dim strMainLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strMainLoc + "' ")
                    '============================
                    'txtLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" + clsLocation.GetSegmentCode(FndMainLocation.Value, Nothing) + "'", txtLocation.Value, isButtonClicked)



                    Dim strLoc As String = grow.Cells("Location").Value.ToString()
                    Dim Checkqry1 As String = ""
                    Dim qry6 As String = clsDBFuncationality.getSingleValue("select Location_Category  from tspl_location_master where Location_Code = '" + strMainLoc + "'")
                    If clsCommon.CompairString(qry6, "MCC") <> CompairStringResult.Equal Then
                        If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                            Throw New Exception("Check the value for Location")
                        End If
                    End If
                    If clsCommon.myLen(strLoc) > 0 Then
                        Checkqry1 = clsDBFuncationality.getSingleValue("select (Loc_Segment_Code) from tspl_location_master where Location_Code='" + strMainLoc + "' ")
                        If clsCommon.myLen(Checkqry1) > 0 Then

                            Dim Checkqry2 As String = "select count(Location_Code) from TSPL_LOCATION_MASTER where Loc_Segment_Code ='" + Checkqry1 + "'  and (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') "
                            Dim countindex1 As Integer = clsDBFuncationality.getSingleValue(Checkqry2, trans)
                            If (countindex1) <= 0 Then
                                Throw New Exception("Sub Location Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.myLen(Checkqry2) > 0 Then
                                strLocDesc = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                            End If
                        End If
                    End If

                    '=========================================================
                    Dim InOut As String = grow.Cells("In/Out").Value.ToString()
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(InOut, "OUT") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Please Insert In or Out type")
                    End If

                    If grow.Cells("Adjustment Date").Value Is Nothing OrElse clsCommon.myLen(grow.Cells("Adjustment Date").Value) <= 0 OrElse Not IsDate(grow.Cells("Adjustment Date").Value) Then
                        Throw New Exception("Please check Adjsutment Date")
                    End If
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "yyyy/MM/dd")

                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    '===============================
                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim checkitem As String = "select count(Item_code) from TSPL_ITEM_MASTER where Item_Code = '" + ItemCode + "' and Product_Type ='MI'"
                    Dim CountItem As Integer = clsDBFuncationality.getSingleValue(checkitem, trans)
                    If (CountItem) <= 0 Then
                        Throw New Exception("Item Code Is Invalid for Milk Type Or Does Not Exist")
                    End If
                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'", trans)
                    '===========================================================

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, Nothing))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If
                    If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If

                    Dim struom As String = grow.Cells("unit code").Value.ToString()

                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As String = "select count(UOM_Code) from TSPL_ITEM_UOM_DETAIL where Item_code='" & ItemCode & "'"
                        If clsCommon.myLen(intCount) > 0 Then
                            Dim Checkqry3 As String = "select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_code='" + ItemCode + "' and UOM_Code = '" + struom + "' "
                            Dim countindex3 As String = clsDBFuncationality.getSingleValue(Checkqry3, trans)
                            If clsCommon.myLen(countindex3) <= 0 Then
                                Throw New Exception("Unit Code Is Invalid Or Does Not Exist")
                            End If

                        End If

                        'Dim dt As DataTable

                        'dt = clsDBFuncationality.GetDataTable(intCount)
                        'Dim CheckUOM As String = ""
                        'If dt.Rows.Count > 0 Then
                        '    For Each dr1 As DataRow In dt.Rows
                        '        Dim check As String = "'" & dr1.Item("UOM_Code") & "'"
                        '        If clsCommon.CompairString(check, struom) <> CompairStringResult.Equal Then
                        '            Throw New Exception("Unit Code Is Invalid Or Does Not Exist")
                        '        End If
                        '    Next
                        'End If
                    End If

                    Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    If strDescription.Length > 300 Then
                        Throw New Exception("Length of Description can not be greater than 300")
                    End If

                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim Fatper As Double = clsCommon.myCdbl(grow.Cells("FAT %").Value)
                    Dim snfper As Double = clsCommon.myCdbl(grow.Cells("SNF %").Value)
                    Dim FatKG As Double = 0
                    Dim srnKG As Double = 0
                    Dim PriceType As String = clsCommon.myCstr(grow.Cells("Price Type").Value)
                    Dim PriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                    Dim fatR As Double = 0
                    Dim snfR As Double = 0
                    Dim FatAmt As Double = 0
                    Dim snfAmt As Double = 0
                    Dim FatRate As Double = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                    Dim SnfRate As Double = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)



                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    'If line = 1 Then
                    obj.Adjustment_No = strAdcode
                    obj.Adjustment_Date = strADate
                    obj.Reference = ""
                    obj.Description = ""
                    obj.Unit_Code = "ALL"
                    obj.ItemType = strIType
                    obj.Loc_Code = strLoc
                    obj.Loc_Desc = strLocDesc
                    obj.MainLocationCode = strMainLoc
                    obj.MainLocationDesc = strMainLocDesc
                    obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                    obj.IsMilkType = MilkType
                    If clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    ElseIf clsCommon.CompairString(InOut, "Out") = CompairStringResult.Equal Then
                        obj.Trans_Type = InOut
                    End If

                    Dim objTr As New ClsJobWorkRMConsumDetails()
                    If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    ElseIf clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        objTr.Price_Type = PriceType
                    Else
                        objTr.Price_Type = "None"
                    End If
                    'objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    objTr.Item_Quantity = Iqty

                    If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                    End If

                    objTr.Item_Cost = Iqty * objTr.Unit_Cost
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP
                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty
                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate
                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    objTr.fat_pers = Fatper
                    objTr.snf_pers = snfper
                    FatKG = (Iqty * Fatper) / 100
                    srnKG = (Iqty * snfper) / 100
                    objTr.fat_kg = FatKG
                    objTr.snf_kg = srnKG
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    If (Fatper) <= 0 Then
                        Throw New Exception("Please Fill Fat%")
                    End If
                    If (snfper) <= 0 Then
                        Throw New Exception("Please Fill Snf%")
                    End If
                    If (clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal) AndAlso Iqty <= 0 Then
                        Throw New Exception("Please Fill quantity")
                    End If
                    If (clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                        Throw New Exception("Please Fill Amount ")
                    End If

                    If clsCommon.CompairString(PriceType, "None") = CompairStringResult.Equal Then
                        'Throw New Exception("Please fill Price Type")
                    ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill MCC Price Code")
                        End If

                    ElseIf clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal AndAlso clsCommon.CompairString(InOut, "In") = CompairStringResult.Equal Then
                        If clsCommon.myLen(PriceCode) <= 0 Then
                            Throw New Exception("Please fill Bulk Price Code ")
                        End If

                    End If

                    'line = line + 1
                    Dim qry As String = ""
                    Dim index As Integer = 0
                    If clsCommon.CompairString(obj.Trans_Type, "IN") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal Then

                            qry = "select count(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code='" + PriceCode + "'"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, srnKG, Iqty)
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                Dim arr As New clsFatSnfRateCalculator

                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                Dim objPrice As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(objTr.Bulk_Price_Code, NavigatorType.Current, trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(objPrice.Fat_Percentage) = objTr.fat_pers And clsCommon.myCdbl(objPrice.Snf_Percentage) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), clsCommon.myCdbl(objPrice.Fat_Weightage), clsCommon.myCdbl(objPrice.Snf_Weightage), clsCommon.myCdbl(objPrice.Standard_Rate))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(objPrice.Fat_Percentage), clsCommon.myCdbl(objPrice.Snf_Percentage), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(objPrice.Standard_Rate), objTr.Unit_Cost)
                                    End If
                                End If

                                
                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "Bulk"
                                objTr.Bulk_Price_Code = PriceCode
                                objTr.Item_Quantity = 0
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        ElseIf clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            qry = " select count(Code) from (select distinct TSPL_MILK_PRICE_MASTER.Price_Code as Code,TSPL_MILK_PRICE_MASTER.Effective_Date as [Price Date], TSPL_MILK_PRICE_MASTER.Description,TSPL_MILK_PRICE_MASTER.Ratio as [Fat Ratio],TSPL_MILK_PRICE_MASTER.SNF_Ratio as [SNF Ratio], TSPL_MILK_PRICE_MASTER.FAT_Pers as [Fat %],TSPL_MILK_PRICE_MASTER.SNF_Pers as [SNF %],TSPL_MILK_PRICE_MASTER.Milk_Rate as [Milk Rate]  from TSPL_MILK_PRICE_MASTER where Price_Code in (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.code where Mcc_Code='" + strMainLoc + "' and TSPL_MILK_PRICE_MASTER.Price_Code = '" + PriceCode + "')) Price"
                            index = clsDBFuncationality.getSingleValue(qry, trans)
                            If index <= 0 Then
                                Throw New Exception("Filled Price Code Is Invalid Or Does Not Exist")
                            End If
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Unit_Cost = GetMilkRateImport(PriceType, PriceCode, FatKG, srnKG, Iqty)
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                Dim arr As New clsFatSnfRateCalculator

                                objTr.Price_Type = "MCC"
                                objTr.MCC_Price_Code = PriceCode
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MILK_PRICE_MASTER where Price_Code='" + objTr.MCC_Price_Code + "'", trans)
                                If objCommonVar.ApplyStdFATSNFRate Then
                                    arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.fat_pers, objTr.snf_pers)
                                Else
                                    If clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")) = objTr.fat_pers And clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Pers")) = objTr.snf_pers Then
                                        arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                                    Else
                                        arr = clsFatSnfRateCalculator.CalculateIn(objTr.Item_Quantity, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("SNF_Pers")), objTr.fat_pers, objTr.snf_pers, clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.Unit_Cost)
                                    End If
                                End If

                                

                                objTr.fat_Rate = Math.Round(arr.fatR, 2)
                                objTr.fat_Amt = Math.Round(arr.FatAmt, 2)
                                objTr.snf_Rate = Math.Round(arr.snfR, 2)
                                objTr.snf_Amt = Math.Round(arr.snfAmt, 2)
                                dtMilkPrice = Nothing
                                arr = Nothing

                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "MCC"
                                objTr.Item_Quantity = 0
                                objTr.MCC_Price_Code = PriceCode
                                objTr.Unit_Cost = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then


                                objTr.Price_Type = "None"
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                                objTr.fat_Rate = FatRate
                                objTr.snf_Rate = SnfRate
                                objTr.Item_Cost = Iqty * objTr.Unit_Cost
                                objTr.fat_Amt = objTr.fat_kg * objTr.fat_Rate
                                objTr.snf_Amt = objTr.snf_kg * objTr.snf_Rate
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Item_Quantity = 0
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.fat_Rate = clsCommon.myCdbl(grow.Cells("FAT Rate").Value)
                                objTr.snf_Rate = clsCommon.myCdbl(grow.Cells("SNF Rate").Value)
                            End If
                        End If
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then

                        ''For RM Other balance Qty check And works only for one unit.
                        Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_Code, Nothing)
                        Dim dblBalQty As Double
                        ''richa agarwal 28/02/2016 apply tolerance limit BM00000007217
                        dblBalQty = clsInventoryMovementNew.getBalance(objTr.Item_Code, strMainLoc, strLoc, obj.Adjustment_No, obj.Adjustment_Date, Nothing, objTr.Unit_Code)
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                            If dblBalQty > 0 Then
                                dblBalQty = ClsLoadingTanker.GetTolerane(dblBalQty, objTr.Item_Quantity)
                            End If
                        End If
                        ''-------------------------

                        Dim dblEnteredQty As Double = objTr.Item_Quantity
                        Dim strICodeInner As String = objTr.Item_Code
                        Dim strUOMInner As String = objTr.Unit_Code
                        Dim dblQtyInner As Double = objTr.Item_Quantity
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, objTr.Item_Code) = CompairStringResult.Equal Then
                            dblEnteredQty = dblQtyInner
                        End If

                        dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        If dblEnteredQty > dblBalQty Then
                            Throw New Exception("Item - " + ItemCode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        End If
                        If clsCommon.CompairString(PriceType, "Bulk") = CompairStringResult.Equal OrElse clsCommon.CompairString(PriceType, "MCC") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                                objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                                objTr.fat_Amt = objCost.FAT_Cost
                                objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                                objTr.snf_Amt = objCost.SNF_Cost
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = clsCommon.myCstr(grow.Cells("Price Type").Value)
                                objTr.Item_Quantity = 0
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                            End If

                        Else
                            If clsCommon.CompairString(AdjType, "CI") <> CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                Dim objCost As New MIlkComponentType
                                objCost = clsInventoryMovementNew.GetAvgCost("MI", objTr.Item_Code, strLoc, objTr.Item_Quantity, objTr.Unit_Code, objTr.fat_kg, objTr.snf_kg, strADate, strADate, True, Nothing)

                                objTr.fat_Rate = objCost.FAT_Cost / IIf(objTr.fat_kg <= 0, 1, objTr.fat_kg)
                                objTr.fat_Amt = objCost.FAT_Cost
                                objTr.snf_Rate = objCost.FAT_Cost / IIf(objTr.snf_kg <= 0, 1, objTr.snf_kg)
                                objTr.snf_Amt = objCost.SNF_Cost
                            ElseIf clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal Then
                                objTr.Price_Type = "None"
                                objTr.Item_Quantity = 0
                                objTr.fat_kg = 0
                                objTr.snf_kg = 0
                                objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Amount").Value)
                                objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                            End If
                        End If
                    End If
                    Dim isSaved As Boolean = obj.SaveData(obj, True, "", trans, "RM")
                Next

                clsCommon.ProgressBarHide()
                RadMessageBox.Show(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

        End If
    End Sub

    Private Sub Exporttoexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exporttoexcel.Click
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Location], '' as [Adjustment Date], '' as [Item Code],'' as [unit code], '' as [Quantity], '' as [Item Cost], '' as [Amount], 'N' as [Third Party Location],'BI' as [Adjustment Type(BD/BI/CD/CI/QD/QI)],'' as Description,'' as [Batch],'' as [Batch Mfg Date],'' as [Batch Exp Date],'' as [Batch Qty]"
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Store Adjustment")
        End Try
    End Sub

    Private Sub Opening_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningExcel.Click
        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Dim arrExistBatchItem As New List(Of String)
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Location", "Adjustment Date", "Item Code", "unit code", "Quantity", "Item Cost", "Amount", "Third Party Location", "Adjustment Type(BD/BI/CD/CI/QD/QI)", "Description", "Batch", "Batch Mfg Date", "Batch Exp Date", "Batch Qty") Then
            Try
                Dim obj As New ClsJobWorkRMConsum()
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)()
                Dim strAdcode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strIType As String = "RM"
                    Dim strLoc As String = grow.Cells("Location").Value.ToString()
                    If String.IsNullOrEmpty(strLoc) Or strLoc.Length > 12 Then
                        Throw New Exception("Check the value for Location")
                    End If
                    Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")
                    Dim strADate As String = clsCommon.GetPrintDate(grow.Cells("Adjustment Date").Value, "yyyy/MM/dd")
                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    Dim ItemCode As String = grow.Cells("Item Code").Value.ToString()
                    If String.IsNullOrEmpty(ItemCode) Or ItemCode.Length > 50 Then
                        Throw New Exception("Check the value for Item Code")
                    End If
                    Dim ItemCodeNEw As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    If clsCommon.myLen(ItemCodeNEw) <= 0 Then
                        Throw New Exception("Item Code " + ItemCode + " is not exits in item master")
                    Else
                        ItemCode = ItemCodeNEw
                    End If

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"
                    strIType = clsCommon.myCstr(clsItemMaster.GetItemType(ItemCode, Nothing))
                    If clsCommon.myLen(strIType) <= 0 Then
                        strIType = "RM"
                    End If
                    '------------------------------------------------------------------------------------------------
                    Dim thirdparty As String = ""
                    thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location").Value.ToString().ToUpper())

                    If Not clsCommon.CompairString(thirdparty, "N") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(thirdparty, "Y") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be N or Y In ColumnName [Third Party Location]")
                    End If

                    AdjType = clsCommon.myCstr(grow.Cells("Adjustment Type(BD/BI/CD/CI/QD/QI)").Value.ToString().ToUpper())

                    If clsCommon.myLen(AdjType) <= 0 Then
                        Throw New Exception("Please Fill Adjustment Type In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If
                    If Not clsCommon.CompairString(AdjType, "BD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "BI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "CI") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QD") = CompairStringResult.Equal AndAlso Not clsCommon.CompairString(AdjType, "QI") = CompairStringResult.Equal Then
                        Throw New Exception("Values Should Be any from BD/BI/CD/CI/QD/QI In ColumnName [Adjustment Type(BD/BI/CD/CI/QD/QI)]")
                    End If

                    obj.chklocation = thirdparty
                    'Adjustment Type(BD/BI/CD/CI/QD/QI)
                    '--------------------------------------------------------------------------------------------------

                    '-------------------------------
                    Dim struom As String = grow.Cells("unit code").Value.ToString()
                    If clsCommon.myLen(struom) = 0 Then
                        struom = clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Else
                        Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(unit_code) from tspl_unit_master where unit_code='" & struom & "'"))
                        If intCount = 0 Then
                            Throw New Exception("Unit code is not correct")
                        End If
                    End If
                    '==============update by preeti gupta against ticket no[]
                    Dim strDescription As String = grow.Cells("Description").Value.ToString()
                    If strDescription.Length > 300 Then
                        Throw New Exception("Length of Description can not be greater than 300")
                    End If
                    'obj.Description = strDescription
                    '========================================================
                    ' Dim struom As String = "UNIT"
                    '---------------------------------------------------------------------------------------------------
                    Dim Iqty As Decimal = clsCommon.myCdbl(grow.Cells("Quantity").Value)

                    Dim Btype As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    Dim StrMRP As Decimal = 0
                    Dim MFGDate As String = clsCommon.GETSERVERDATE()
                    Dim Batch As String = ""
                    Dim expdate As String = clsCommon.GETSERVERDATE()
                    Dim rmk As String = ""
                    Dim commt As String = ""
                    Dim ItemDesc As String = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'")
                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    If line = 1 Then
                        ''started by priti
                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        'obj.Posting_Date
                        obj.Reference = ""
                        obj.Description = ""
                        'obj.Posted()
                        obj.Unit_Code = "ALL"
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                        '' ended by priti
                    End If

                    Dim objTr As New ClsJobWorkRMConsumDetails()

                    'objTr.Adjustment_No = ""
                    objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = Iqty
                    If clsCommon.myCdbl(grow.Cells("Item Cost").Value) <= 0 Then
                        objTr.Unit_Cost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Cost from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "'"))
                    Else
                        objTr.Unit_Cost = clsCommon.myCdbl(grow.Cells("Item Cost").Value)
                    End If

                    objTr.Item_Cost = Iqty * objTr.Unit_Cost
                    objTr.Unit_Code = struom
                    objTr.Remarks = rmk
                    objTr.Comments = commt
                    objTr.mrp = StrMRP

                    objTr.BreakageType = Btype
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty

                    objTr.MFG_Date = MFGDate
                    objTr.Batch_No = Batch
                    objTr.Expiry_Date = expdate

                    objTr.ItemType = strIType
                    obj.ItemType = strIType
                    obj.Description = strDescription
                    Dim isAddItem As Boolean = True
                    If clsItemMaster.IsBatchItem(objTr.Item_Code) Then
                        If clsCommon.myLen(grow.Cells("Batch").Value) > 0 Then
                            Dim objBatch As New clsBatchInventory
                            objBatch.Batch_No = clsCommon.myCstr(grow.Cells("Batch").Value)
                            objBatch.Manual_BatchNo = objBatch.Batch_No
                            If clsCommon.myLen(grow.Cells("Batch Mfg Date").Value) <= 0 Then
                                Throw New Exception("Please provide Mfg Date Of Batch " + objBatch.Batch_No)
                            End If
                            objBatch.Manufacture_Date = clsCommon.myCDate(grow.Cells("Batch Mfg Date").Value)
                            If clsCommon.myLen(grow.Cells("Batch Exp Date").Value) <= 0 Then
                                Throw New Exception("Please provide Expiry Date Of Batch " + objBatch.Batch_No)
                            End If
                            objBatch.Expiry_Date = clsCommon.myCDate(grow.Cells("Batch Exp Date").Value)
                            objBatch.Qty = clsCommon.myCdbl(grow.Cells("Batch Qty").Value)
                            If arrExistBatchItem.Contains(objTr.Item_Code) Then
                                For kk As Integer = 0 To obj.Arr.Count - 1
                                    If clsCommon.CompairString(obj.Arr(kk).Item_Code, objTr.Item_Code) = CompairStringResult.Equal Then
                                        obj.Arr(kk).arrBatchItem.Add(objBatch)
                                        Exit For
                                    End If
                                Next
                                Continue For
                            Else
                                arrExistBatchItem.Add(objTr.Item_Code)
                                objTr.arrBatchItem = New List(Of clsBatchInventory)
                                objTr.arrBatchItem.Add(objBatch)
                            End If
                        Else
                            Throw New Exception("Please provide Batch detail for item " + objTr.Item_Code)
                        End If
                    End If
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                    line = line + 1
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                If arrExistBatchItem.Count > 0 Then
                    For kk As Integer = 0 To obj.Arr.Count - 1
                        If obj.Arr(kk).arrBatchItem IsNot Nothing AndAlso obj.Arr(kk).arrBatchItem.Count > 0 Then
                            Dim TotBatQty As Decimal = 0
                            For ll As Integer = 0 To obj.Arr(kk).arrBatchItem.Count - 1
                                TotBatQty += obj.Arr(kk).arrBatchItem(ll).Qty
                            Next
                            If TotBatQty <> obj.Arr(kk).Item_Quantity Then
                                Throw New Exception("item " + obj.Arr(kk).Item_Code + " Quantity " + clsCommon.myCstr(obj.Arr(kk).Item_Quantity) + " Batch total Quantity " + clsCommon.myCstr(TotBatQty))
                            End If
                        End If
                    Next
                End If
                Dim isSaved As Boolean = obj.SaveData(obj, True, "", Nothing, "RM")
                RadMessageBox.Show(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
End Class
