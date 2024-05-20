Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common


''=============Ticket No: BM00000010010 Parteek

Public Class frmAssembDis
    Inherits FrmMainTranScreen
    Dim RunBatchFifowise As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim icodestatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Private isCellValueChangedOpen As Boolean = False
    Dim isLoadTime As Boolean = False
    Public Document_No As String = Nothing
    Public strMainLoc As String = Nothing ' Prabhakar
    '' component grid columns
    Const colIcodeStatus As String = "IcodeStatus"
    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colProductType As String = "ProductType"
    Const colStockQty As String = "StockQty"

    Const colFATPER As String = "colFATPER"
    Const colSNFPER As String = "colSNFPER"
    Const colFATKG As String = "colFATKG"
    Const colSNFKG As String = "colSNFKG"

    Const colFATRate As String = "colFATRate"
    Const colSNFRate As String = "colSNFRate"

    Const colqty As String = "Qty"
    Const colUnitCode As String = "UnitCode"
    Const colItemstatus As String = "ItemStatus"
    Const colLocationCode As String = "Location"
    Const colLocationName As String = "colLocationName"
    Const colSerialNo As String = "colSerialNo"
    Const colItemAmount As String = "colItemAmount"
    Dim settPickCostFromItemMaster As Boolean = False
    Dim settFATSNFRateMandatory As Boolean = False
    Dim settAutoFillSameLocationInGrid As Boolean = False
    Dim CalculateItemCostonAvgForAssembly As Boolean = False

#Region "Functions"
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
            btnsave.Visible = MyBase.isModifyFlag
            btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isExport = True Then
            RadMenuItem1.Enabled = True
            RadMenuItem2.Enabled = True
        Else
            RadMenuItem1.Enabled = False
            RadMenuItem2.Enabled = False
        End If
        btnunpost.Visible = False
        'If MyBase.isReverse Then
        '    btnunpost.Enabled = True
        'Else
        '    btnunpost.Enabled = False
        'End If
        'RadMenu1.Visible = MyBase.isExport
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAssemblies)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Visible = MyBase.isPrintFlag
        'If btnsave.Visible = True Then
        '    RadMenuItem1.Enabled = True
        '    RadMenuItem2.Enabled = True
        'Else
        '    RadMenuItem1.Enabled = False
        '    RadMenuItem2.Enabled = False
        'End If
    End Sub
    ' Ticket No : ERO/09/10/19-001043 Main item qty allow to decimal point 
    Function fillgridcombobox() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SCRAP"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()
        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemCategoryCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim ProductType As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn
        Dim Stockqty As New GridViewDecimalColumn
        Dim Fat_KG As New GridViewDecimalColumn
        Dim SNF_KG As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim LocationCode As New GridViewTextBoxColumn
        Dim LocationName As New GridViewTextBoxColumn
        Dim SerialNo As New GridViewTextBoxColumn
        Dim ItemAmount As New GridViewDecimalColumn

        Dim itemstatus As New GridViewComboBoxColumn
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)


        icodestatus.FormatString = ""
        icodestatus.HeaderText = "Item Status"
        icodestatus.Width = 80
        icodestatus.Name = colIcodeStatus
        icodestatus.DataSource = fillgridcombobox()
        icodestatus.IsVisible = False
        If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
            icodestatus.IsVisible = True
        End If
        icodestatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        icodestatus.DisplayMember = "Code"
        icodestatus.ValueMember = "Code"
        gvBOM.Columns.Add(icodestatus)


        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            ItemCode.ReadOnly = False
        Else
            ItemCode.ReadOnly = True
        End If

        ItemCode.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 200
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(itemDesc)

        ProductType.FormatString = ""
        ProductType.HeaderText = "Product Type"
        ProductType.Name = colProductType
        ProductType.Width = 80
        ProductType.ReadOnly = True
        ProductType.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(ProductType)

        qty.FormatString = ""
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
        'If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
        '    qty.ReadOnly = False
        'Else
        '    qty.ReadOnly = True
        'End If
        qty.ReadOnly = False
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(qty)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "UOM"

        UnitCode.Name = colUnitCode
        UnitCode.Width = 100
        'UnitCode.ReadOnly = True
        UnitCode.ReadOnly = False
        UnitCode.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(UnitCode)


        LocationCode.FormatString = ""
        LocationCode.HeaderText = "Location Code"
        LocationCode.Name = colLocationCode
        LocationCode.Width = 100
        'LocationCode.ReadOnly = True
        LocationCode.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(LocationCode)

        '===================Added by preeti Gupta=====================
        LocationName.FormatString = ""
        LocationName.HeaderText = "Location Name"
        LocationName.Name = colLocationName
        LocationName.Width = 100
        'LocationCode.ReadOnly = True
        LocationName.TextAlignment = ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(LocationName)
        '=============================================================

        Stockqty.FormatString = ""
        Stockqty.HeaderText = "Stock Qty"
        Stockqty.Name = colStockQty
        Stockqty.Width = 80
        Stockqty.ReadOnly = True
        If clsCommon.CompairString(ddlDisassemblyType.SelectedValue, "Assemble") = CompairStringResult.Equal Then
            Stockqty.IsVisible = True
        Else
            Stockqty.IsVisible = False
        End If
        Stockqty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(Stockqty)

        '' fat %
        Fat_KG.FormatString = ""
        Fat_KG.HeaderText = "Fat %"
        Fat_KG.Name = colFATPER
        Fat_KG.Width = 80
        'Fat_KG.ReadOnly = False
        Fat_KG.IsVisible = True
        Fat_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(Fat_KG)

        '' fat kg
        Fat_KG = New GridViewDecimalColumn()
        Fat_KG.FormatString = ""
        Fat_KG.HeaderText = "Fat KG"
        Fat_KG.Name = colFATKG
        Fat_KG.Width = 80
        Fat_KG.ReadOnly = True
        Fat_KG.IsVisible = True
        Fat_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(Fat_KG)

        '' fat kg
        Fat_KG = New GridViewDecimalColumn()
        Fat_KG.FormatString = ""
        Fat_KG.HeaderText = "Fat Rate"
        Fat_KG.Name = colFATRate
        Fat_KG.Width = 80
        Fat_KG.ReadOnly = False
        Fat_KG.IsVisible = True
        Fat_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(Fat_KG)

        '' SNF PER
        SNF_KG.FormatString = ""
        SNF_KG.HeaderText = "SNF %"
        SNF_KG.Name = colSNFPER
        SNF_KG.Width = 80
        'SNF_KG.ReadOnly = False
        SNF_KG.IsVisible = True
        SNF_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(SNF_KG)


        '' SNF kg
        SNF_KG = New GridViewDecimalColumn()
        SNF_KG.FormatString = ""
        SNF_KG.HeaderText = "SNF KG"
        SNF_KG.Name = colSNFKG
        SNF_KG.Width = 80
        SNF_KG.ReadOnly = True
        SNF_KG.IsVisible = True
        SNF_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(SNF_KG)

        Fat_KG = New GridViewDecimalColumn()
        Fat_KG.FormatString = ""
        Fat_KG.HeaderText = "SNF Rate"
        Fat_KG.Name = colSNFRate
        Fat_KG.Width = 80
        Fat_KG.ReadOnly = False
        Fat_KG.IsVisible = True
        Fat_KG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(Fat_KG)

        SerialNo.FormatString = ""
        SerialNo.HeaderText = "Serial No"
        SerialNo.Name = colSerialNo
        SerialNo.Width = 100
        'LocationCode.ReadOnly = True
        SerialNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(SerialNo)

        ItemAmount.FormatString = ""
        ItemAmount.HeaderText = "Item Amount"
        ItemAmount.Name = colItemAmount
        ItemAmount.Width = 100
        ItemAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemAmount)


    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As clsAssembliesDis = clsAssembliesDis.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                btnCancel.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnCancel.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            isNewEntry = False
            isLoadTime = True
            If CheckSerialNo() Then
                txtSerialNo.Enabled = True
            Else
                txtSerialNo.Enabled = False
            End If
            txtCode.Value = obj.CODE
            dtp_AssembleDate.Value = obj.ASSEMBLY_DATE
            cboTransactionType.SelectedValue = obj.TRANSACTION_TYPE
            ddlDisassemblyType.SelectedValue = obj.DISASSEMBLY_TYPE
            fndAssemblyCode.Value = obj.ASSEMBLY_CODE
            lblAssemblyDesc.Text = obj.ASSEMBLY_DESC
            Me.txtDesc.Text = obj.DESCRIPTION
            txtComment.Text = obj.COMMENTS
            fndMainItem.Value = obj.Main_Item_Code
            'fndMainItem.Tag = obj.Main_Item_Code
            lblMainItemDesc.Text = obj.Main_Item_Desc
            fndBomCode.Value = obj.BOM_CODE
            lblBomDesc.Text = obj.BOM_DESC
            cboCompoAssMethod.SelectedValue = obj.COMP_ASSEMBL_METHOD
            Me.fndLocation.Value = obj.LOCATION_CODE
            lblLocationDesc.Text = obj.LOCATION_DESC
            txtBuildQty.Text = obj.BUILD_QUANTITY

            txtDisassCost.Text = obj.DISASSEMBLY_COST
            txtQuantity.Text = obj.QUANTITY
            lblUnitName.Text = obj.BUILD_ITEM_UNIT_CODE
            Me.txtSerialNo.Text = obj.Serial_No
            fndUom.Value = obj.BUILD_ITEM_UNIT_CODE
            txtFatPer.Text = obj.FAT_PER
            txtFatKG.Text = obj.FAT_KG
            ''richa BHA/12/09/18-000538
            txtSNFPer.Text = obj.SNF_PER
            txtSNFKG.Text = obj.SNF_KG

            txtCode.MyReadOnly = True
            fndMainItem.Tag = obj.arrBatchItem
            '' load item details
            Dim objList1 As New List(Of clsAssembliesDisDetail)
            objList1 = obj.objList
            Me.gvBOM.Rows.Clear()
            LoadGridColumns()

            If objList1 IsNot Nothing AndAlso objList1.Count > 0 Then
                For Each obj1 As clsAssembliesDisDetail In objList1
                    Me.gvBOM.Rows.AddNew()
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = obj1.LINE_NO

                    If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                        Try
                            If clsCommon.CompairString(obj1.ItemType, "OK") = CompairStringResult.Equal Then
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colIcodeStatus).Value = "OK"
                            ElseIf clsCommon.CompairString(obj1.ItemType, "SCRAP") = CompairStringResult.Equal Then
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colIcodeStatus).Value = "SCRAP"
                            End If
                        Catch exx As Exception
                        End Try
                    End If
                    isCellValueChangedOpen = True
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Tag = obj1.arrBatchItem
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = obj1.CONSM_QUANTITY
                    'Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = (obj1.CONSM_QUANTITY / Val(Me.txtQuantity.Text)) * Val(txtBuildQty.Text)
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value = obj1.Product_Type
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = obj1.LOCATION_CODE

                    '===============Added by preeti [29/12/2016]
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationName).Value = clsLocation.GetName(obj1.LOCATION_CODE, Nothing)
                    '===========================================
                    If clsCommon.CompairString(obj1.Product_Type, "MI") = CompairStringResult.Equal Then
                        Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = False
                        Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = False
                    Else
                        Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = True
                        Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = True
                    End If
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).Value = obj1.FAT_PER
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATKG).Value = obj1.FAT_KG
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATRate).Value = obj1.FAT_Rate

                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).Value = obj1.SNF_PER
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFKG).Value = obj1.SNF_KG
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFRate).Value = obj1.SNF_Rate

                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSerialNo).Value = obj1.Serial_No
                    Me.gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemAmount).Value = obj1.Item_Amount
                    isCellValueChangedOpen = False
                Next
            End If
            '==============Parteek===========
            If clsCommon.CompairString(ddlDisassemblyType.SelectedValue, "Assemble") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colStockQty).Value = clsAssembliesDis.GetLocationStock(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value, dtp_AssembleDate.Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value, fndUom.Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value)
                Else
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colStockQty).Value = clsAssembliesDis.GetLocationStock(fndLocation.Value, dtp_AssembleDate.Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value, fndUom.Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value)
                End If
            End If


            '============End==================
            btnsave.Text = "Update"
            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(obj.CODE)
            End If
            ''End of For Custom Fields


        End If
        isLoadTime = False
    End Sub
    ' done by priti BHA/23/08/18-000476 for bharat ,main item batch wise entry.
    Sub OpenBatchItem(ByVal IsShowBatchForm As Boolean)
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtp_AssembleDate.Value) = True Then

            Dim TransType_Str As String = ""
            Dim blnBatch As Boolean = False
            blnBatch = clsItemMaster.IsBatchItem(fndMainItem.Value, Nothing)
            If clsCommon.CompairString(cboTransactionType.Text, "Assembly") = CompairStringResult.Equal Then
                If clsCommon.myCBool(blnBatch) Then
                    Dim frm As frmBatchItemIn = New frmBatchItemIn()
                    frm.strItemCode = clsCommon.myCstr(fndMainItem.Value)
                    frm.strItemName = clsCommon.myCstr(lblMainItemDesc.Text)
                    frm.dblqty = clsCommon.myCdbl(txtQuantity.Value)
                    frm.strUOM = clsCommon.myCstr(fndUom.Value)
                    frm.TransDate = dtp_AssembleDate.Value
                    'frm.dblMRP = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colMRP).Value)
                    frm.arr = TryCast(fndMainItem.Tag, List(Of clsBatchInventory))
                    If IsShowBatchForm = True Then
                        frm.ShowDialog()
                    End If

                    If Not frm.isCencelButtonClicked Then
                        fndMainItem.Tag = frm.arr
                    End If
                End If
            Else
                If clsCommon.myCBool(blnBatch) Then
                    TransType_Str = "Disassembly"
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(fndMainItem.Value)
                    frm.strItemName = clsCommon.myCstr(lblMainItemDesc.Text)
                    frm.strLocationCode = fndLocation.Value
                    frm.strCurrDocNo = txtCode.Value
                    frm.strCurrDocType = TransType_Str
                    frm.strUOM = clsCommon.myCstr(fndUom.Value)
                    frm.dblqty = clsCommon.myCdbl(txtQuantity.Value)
                    frm.arr = TryCast(fndMainItem.Tag, List(Of clsBatchInventory))
                    If RunBatchFifowise = 0 Then
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            fndMainItem.Tag = frm.arr
                        End If
                    Else
                        If frm.OpenSerialList(0, "", "") Then
                            fndMainItem.Tag = frm.arr
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Function SaveData(Optional ByVal isPost As Boolean = False) As Boolean

        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsAssembliesDis()
                obj.CODE = txtCode.Value
                obj.ASSEMBLY_DATE = Me.dtp_AssembleDate.Value
                obj.TRANSACTION_TYPE = Me.cboTransactionType.SelectedValue.ToString
                If Not Me.ddlDisassemblyType.SelectedValue Is Nothing Then
                    obj.DISASSEMBLY_TYPE = Me.ddlDisassemblyType.SelectedValue.ToString
                Else
                    obj.DISASSEMBLY_TYPE = ""
                End If

                obj.ASSEMBLY_CODE = Me.fndAssemblyCode.Value.ToString
                obj.DESCRIPTION = Me.txtDesc.Text
                obj.COMMENTS = Me.txtComment.Text
                obj.Main_Item_Code = fndMainItem.Value
                obj.BOM_CODE = Me.fndBomCode.Value.ToString
                obj.COMP_ASSEMBL_METHOD = Me.cboCompoAssMethod.SelectedValue.ToString
                obj.LOCATION_CODE = Me.fndLocation.Value.ToString
                obj.BUILD_QUANTITY = clsCommon.myCdbl(Me.txtBuildQty.Text)
                obj.QUANTITY = clsCommon.myCdbl(Me.txtQuantity.Text)
                obj.DISASSEMBLY_COST = clsCommon.myCdbl(Me.txtDisassCost.Text)
                obj.BUILD_ITEM_UNIT_CODE = fndUom.Value ''lblUnitName.Text
                obj.Comp_Code = objCommonVar.CurrentCompanyCode
                obj.Serial_No = txtSerialNo.Text
                obj.BOM_PROD_ITEM_UNIT_CODE = clsBOM.GetBOMBuildUOM(obj.BOM_CODE, trans)
                obj.MainItemStatus = ""
                If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                    obj.MainItemStatus = "SCRAP"
                End If

                obj.FAT_PER = clsCommon.myCdbl(txtFatPer.Text)
                obj.SNF_PER = clsCommon.myCdbl(txtSNFPer.Text)
                obj.FAT_KG = clsCommon.myCdbl(txtFatKG.Text)
                obj.SNF_KG = clsCommon.myCdbl(txtSNFKG.Text)
                obj.arrBatchItem = TryCast(fndMainItem.Tag, List(Of clsBatchInventory))
                '' saving item details 
                Dim objTr As clsAssembliesDisDetail
                Dim objListItem As New List(Of clsAssembliesDisDetail)

                For Each row As GridViewRowInfo In gvBOM.Rows
                    If clsCommon.myLen(row.Cells(colItemCode).Value) <= 0 Then
                        Continue For
                    End If
                    objTr = New clsAssembliesDisDetail
                    objTr.ASSEMBLY_CODE = Me.txtCode.Value
                    objTr.BOM_CODE = Me.fndBomCode.Value
                    objTr.CONSM_ITEM_CODE = row.Cells(colItemCode).Value
                    objTr.CONSM_ITEM_UNIT_CODE = row.Cells(colUnitCode).Value
                    objTr.CONSM_QUANTITY = clsCommon.myCdbl(row.Cells(colqty).Value)
                    objTr.ITEM_DESCRIPTION = row.Cells(colitemDesc).Value
                    objTr.LINE_NO = row.Cells(colLineNo).Value
                    objTr.LOCATION_CODE = row.Cells(colLocationCode).Value

                    objTr.Item_Amount = clsCommon.myCdbl(row.Cells(colItemAmount).Value)

                    objTr.FAT_PER = clsCommon.myCdbl(row.Cells(colFATPER).Value)
                    objTr.FAT_KG = clsCommon.myCdbl(row.Cells(colFATKG).Value)
                    objTr.FAT_Rate = clsCommon.myCdbl(row.Cells(colFATRate).Value)

                    objTr.SNF_PER = clsCommon.myCdbl(row.Cells(colSNFPER).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(row.Cells(colSNFKG).Value)
                    objTr.SNF_Rate = clsCommon.myCdbl(row.Cells(colSNFRate).Value)


                    objTr.ItemType = ""
                    objTr.ItemStatus = ""
                    If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                        objTr.ItemType = row.Cells(colIcodeStatus).Value
                        objTr.ItemStatus = "OLD"
                    End If
                    objTr.Serial_No = row.Cells(colSerialNo).Value
                    objTr.arrBatchItem = TryCast(row.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                    objListItem.Add(objTr)

                Next
                obj.objList = objListItem
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields


                If (clsAssembliesDis.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    If isPost = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.CODE, NavigatorType.Current)

                    End If

                End If
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            '    txtCode.Focus()
            '    Throw New Exception("Please Fill  Code")
            'End If
            If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly") = CompairStringResult.Equal Then
                UpdateQty(False)
            Else
                UpdateQty(True)
            End If

            If AllowFutureDateTransaction(dtp_AssembleDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(clsCommon.myCstr(cboTransactionType.SelectedValue)) <= 0 Then
                cboTransactionType.Focus()
                Throw New Exception("Please select Transaction Type")
            Else
                If Me.cboTransactionType.SelectedValue = "Disassembly" Then
                    If clsCommon.myLen(clsCommon.myCstr(ddlDisassemblyType.SelectedValue)) <= 0 Then
                        ddlDisassemblyType.Focus()
                        Throw New Exception("Please Select Disassembly Type")
                    Else
                        If ddlDisassemblyType.SelectedValue = "Assembly" Then
                            If clsCommon.myLen(Me.fndAssemblyCode.Value) <= 0 Then
                                fndAssemblyCode.Focus()
                                Throw New Exception("Please Fill Assembly Code")
                            End If

                        End If
                    End If
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
                txtDesc.Focus()
                Throw New Exception("Please Fill  Description")
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndMainItem.Value)) <= 0 Then
                fndMainItem.Focus()
                Throw New Exception("Please select Item ")
            End If

            'If clsCommon.myLen(clsCommon.myCstr(fndMainItem.Tag.ToString)) <= 0 Then
            '    fndMainItem.Focus()
            '    Throw New Exception("Please select Main Item ")
            'End If
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            Else

                If clsCommon.myLen(clsCommon.myCstr(fndBomCode.Value)) <= 0 Then
                    fndBomCode.Focus()
                    Throw New Exception("Please select Bom ")
                End If
            End If

            If txtSerialNo.Enabled = True And clsCommon.myLen(clsCommon.myCstr(txtSerialNo.Text)) <= 0 Then
                txtSerialNo.Focus()
                Throw New Exception("Please Enter  Serial no for Main Item.")
            End If
            If clsCommon.myLen(clsCommon.myCstr(cboCompoAssMethod.SelectedValue)) <= 0 Then
                cboCompoAssMethod.Focus()
                Throw New Exception("Please select Component Assembly Method ")
            End If
            'If Me.cboTransactionType.SelectedValue = "Disassembly" Then
            '    OpenBatchItem()
            'End If
            If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) <= 0 Then
                fndLocation.Focus()
                Throw New Exception("Please select Location ")
            Else
                '' check for itemwise location
                For Each dr As GridViewRowInfo In gvBOM.Rows
                    If clsCommon.myLen(dr.Cells(colItemCode).Value) > 0 And clsCommon.myLen(dr.Cells(colLocationCode).Value) <= 0 Then
                        Throw New Exception("Select Loacation for Item Code: " & dr.Cells(colItemCode).Value & "")
                    End If
                    ''richa TEC/09/10/18-000337 pick cost from item master mandatory on setting based 
                    If settFATSNFRateMandatory Then
                        If clsCommon.CompairString(clsCommon.myCstr(dr.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dr.Cells(colFATRate).Value) <= 0 Then
                                Throw New Exception("Please enter FAT Rate of Item Code: " & dr.Cells(colItemCode).Value & "")
                            End If
                            If clsCommon.myCdbl(dr.Cells(colSNFRate).Value) <= 0 Then
                                Throw New Exception("Please enter SNF Rate of Item Code: " & dr.Cells(colItemCode).Value & "")
                            End If
                        End If
                    End If
                    ''-----------------------
                    If clsCommon.myLen(dr.Cells(colSerialNo).Value) < 0 Then
                        Continue For
                    End If
                    For rw As Integer = 0 To gvBOM.Rows.IndexOf(dr) - 1
                        If gvBOM.Rows(rw).Cells(colSerialNo).Value = dr.Cells(colSerialNo).Value And clsCommon.myLen(dr.Cells(colSerialNo).Value) > 0 Then
                            Throw New Exception("Serial No : " & dr.Cells(colSerialNo).Value & " duplicated at row no-" & rw + 1 & "")
                        End If
                    Next
                Next

                '' check for available quantity on selected location
                If Me.cboTransactionType.SelectedValue = "Disassembly" Then
                    Dim AvailQty As Double = 0
                    If clsCommon.CompairString(clsItemMaster.GetItemProductType(fndMainItem.Value, Nothing), "MI") = CompairStringResult.Equal Then
                        AvailQty = ClsLoadingTanker.getBalance(fndMainItem.Value, "", fndLocation.Value, Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, fndUom.Value)
                    Else
                        AvailQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(Me.fndMainItem.Value, Me.fndLocation.Value, Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, fndUom.Value)
                    End If
                    If AvailQty < clsCommon.myCdbl(Me.txtQuantity.Text) Then
                        Throw New Exception("Item Code: " & Me.fndMainItem.Value & "; Required Qty : " & Me.txtQuantity.Text & " ; Available Qty : " & AvailQty & "")
                    End If
                ElseIf Me.cboTransactionType.SelectedValue = "Assembly" Then
                    '' check for available quantites for raw material
                    'Dim dtRM As DataTable
                    'Dim strq As String
                    'strq = "select TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_MF_BOM_DETAIL.CONSM_QUANTITY,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE " & _
                    '       " from TSPL_MF_BOM_DETAIL inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE " & _
                    '       " where TSPL_MF_BOM_DETAIL.BOM_CODE='" & Me.fndBomCode.Value & "' and TSPL_MF_BOM_HEAD.PROD_ITEM_CODE='" & fndMainItem.Value & "' " & _
                    '       " order by TSPL_MF_BOM_DETAIL.LINE_NO"
                    'dtRM = clsDBFuncationality.GetDataTable(strq)
                    'For Each dr As GridViewRowInfo In gvBOM.Rows
                    '    Dim availQty As Double = 0
                    '    Dim reqQty As Double = 0
                    '    'availQty = dr.Cells(colStockQty).Value
                    '    availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Cells(colItemCode).Value, dr.Cells(colLocationCode).Value, Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, dr.Cells(colUnitCode).Value)
                    '    reqQty = dr.Cells(colqty).Value ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
                    '    If availQty <= reqQty Then
                    '        Throw New Exception("Item Code: " & dr.Cells(colItemCode).Value & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                    '    End If
                    'Next
                    For jj As Integer = 0 To gvBOM.Rows.Count - 1
                        Dim availQty As Double = 0
                        Dim reqQty As Double = 0
                        If clsCommon.myLen(clsCommon.myCstr(gvBOM.Rows(jj).Cells(colItemCode).Value)) > 0 Then
                            availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(clsCommon.myCstr(gvBOM.Rows(jj).Cells(colItemCode).Value), clsCommon.myCstr(gvBOM.Rows(jj).Cells(colLocationCode).Value), Me.txtCode.Value, dtp_AssembleDate.Value, Nothing, clsCommon.myCstr(gvBOM.Rows(jj).Cells(colUnitCode).Value))
                            reqQty = clsCommon.myCdbl(gvBOM.Rows(jj).Cells(colqty).Value)
                            If availQty <= reqQty Then
                                Throw New Exception("Item Code: " & clsCommon.myCstr(gvBOM.Rows(jj).Cells(colItemCode).Value) & " ; Required Qty : " & clsCommon.myCstr(gvBOM.Rows(jj).Cells(colqty).Value) & " ; Available Qty : " & availQty & "")
                            End If
                        End If
                    Next
                End If

            End If
            'Ticket No-BHA/02/05/19-000878 sanjay
            If clsCommon.myCdbl(txtQuantity.Text) <= 0 Then
                txtQuantity.Focus()
                Throw New Exception("Please fill Quantity ")
                ''============comment by  preeti gupta[28/12/2016]
                'Else
                '    If clsCommon.myCdbl(txtQuantity.Text) Mod clsCommon.myCdbl(Me.txtBuildQty.Text) > 0 Then
                '        txtQuantity.Focus()
                '        Throw New Exception("Quantity must be multiple of Build quantity")
                '    End If
            End If


            '-----------------check for status------------------------------------------------------------------------------
            If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
                For ii As Integer = 0 To gvBOM.Rows.Count - 1
                    Dim icode As String = ""
                    Dim istatus As String = ""

                    icode = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value)
                    istatus = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colIcodeStatus).Value)

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(istatus) <= 0 Then
                        Throw New Exception("Please Select Item Status(OK/SCRAP) For Item At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                        Return False
                    End If

                Next
            End If
            If clsERPFuncationality.GetBatchWiseApplicableStatus(dtp_AssembleDate.Value) = True Then
                Dim isbatchMainitem As Boolean = clsItemMaster.IsBatchItem(clsCommon.myCstr(fndMainItem.Value))
                Dim dblQtyMainItem As Double = clsCommon.myCdbl(txtQuantity.Value)

                If dblQtyMainItem > 0 AndAlso clsCommon.myCBool(isbatchMainitem) Then
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(fndMainItem.Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then

                        Throw New Exception("Please provide Batch no for item : " + lblMainItemDesc.Text + " ")
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> dblQtyMainItem Then
                            Throw New Exception("Item : " + lblMainItemDesc.Text + " Entered Qty " + clsCommon.myCstr(dblQtyMainItem) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " ")
                        End If
                    End If
                End If


                For ii As Integer = 0 To gvBOM.Rows.Count - 1
                    Dim icode As String = ""

                    icode = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value)
                    Dim dblQty As Double = clsCommon.myCdbl(gvBOM.Rows(ii).Cells(colqty).Value)
                    Dim isbatch1 As Boolean = clsItemMaster.IsBatchItem(clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value))

                    If dblQty > 0 AndAlso clsCommon.myCBool(isbatch1) Then
                        Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gvBOM.Rows(ii).Cells(colItemCode).Tag, List(Of clsBatchInventory))
                        If arrBatchNo Is Nothing Then
                            Throw New Exception("Please provide Batch no for item : " + icode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Else
                            Dim tQty As Decimal = 0
                            For Each objBatch As clsBatchInventory In arrBatchNo
                                tQty += objBatch.Qty
                            Next
                            If tQty <> dblQty Then
                                Throw New Exception("Item : " + icode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    End If

                Next

            End If

            '-----------------------------------------------------------------------------------------------------------------

            UcCustomFields1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Private Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try


            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If
            Dim Reason As String = ""
            If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

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
                trans = clsDBFuncationality.GetTransactin
                clsAssembliesDis.DeleteData(Me.txtCode.Value, trans)
                saveCancelLog(Reason, "Delete", trans)
                '' custom fields
                clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)

            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
            trans.Rollback()
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
    Sub AddNew()
        txtCode.Value = ""
        isNewEntry = True
        txtSerialNo.Text = ""
        gvBOM.Rows.Clear()
        Me.dtp_AssembleDate.Value = clsCommon.GETSERVERDATE()
        txtCode.MyReadOnly = False

        txtDesc.Text = ""
        Me.cboTransactionType.SelectedValue = "Assembly"
        Me.cboCompoAssMethod.SelectedIndex = -1
        Me.ddlDisassemblyType.SelectedIndex = -1
        Me.cboCompoAssMethod.SelectedValue = "0"

        Me.txtDesc.Text = ""
        Me.txtComment.Text = ""
        fndMainItem.Value = Nothing
        'Me.fndMainItem.Tag = ""
        lblMainItemDesc.Text = ""
        fndBomCode.Value = Nothing
        lblBomDesc.Text = ""

        Me.fndLocation.Value = Nothing
        Me.lblLocationDesc.Text = ""
        Me.txtBuildQty.Text = "0"
        Me.txtDisassCost.Text = "0"
        Me.txtQuantity.Text = "0"
        fndUom.Value = ""
        lblUnitName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        btnCancel.Enabled = False
        fndMainItem.Tag = Nothing
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        btnsave.Text = "Save"
    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_PROD_ASSEMBLIES where CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select CODE as Code,Description as Name,ASSEMBLY_DATE AS [Date],TRANSACTION_TYPE as [Type],Main_Item_Code as [Main Item Code],BOM_CODE as [BOM Code],LOCATION_CODE as [Location Code] from  TSPL_PROD_ASSEMBLIES"
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_PROD_ASSEMBLIES", qry, "Code", "", txtCode.Value, "", isButtonClicked, "TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE")
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub frmAssemblies_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
            clsFixedParameter.UpdateData(clsFixedParameterType.FATSNFRateMandatory, clsFixedParameterCode.FATSNFRateMandatory, "0", Nothing)
        End If


        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        settPickCostFromItemMaster = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickCostFromItemMaster, clsFixedParameterCode.PickCostFromItemMaster, Nothing)) = 1)
        settFATSNFRateMandatory = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFRateMandatory, clsFixedParameterCode.FATSNFRateMandatory, Nothing)) > 0)
        settAutoFillSameLocationInGrid = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoFillSameLocationInGrid, clsFixedParameterCode.AutoFillSameLocationInGrid, Nothing)) = 1)
        CalculateItemCostonAvgForAssembly = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateItemCostonAvgForAssembly, clsFixedParameterCode.CalculateItemCostonAvgForAssembly, Nothing)) = 1)

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        isNewEntry = True
        isLoadTime = True
        txtFatKG.ReadOnly = True
        txtSNFKG.ReadOnly = True

        LoadGridColumns()
        '' fill transaction type
        cboTransactionType.DataSource = clsAssemblies.GetTransactionTypeTable()
        cboTransactionType.DisplayMember = "Name"
        cboTransactionType.ValueMember = "Code"

        '' fill disassembly type
        ddlDisassemblyType.DataSource = clsAssemblies.GetDisassemblyTypeTable()
        ddlDisassemblyType.DisplayMember = "Name"
        ddlDisassemblyType.ValueMember = "Code"

        '' fill disassembly type
        cboCompoAssMethod.DataSource = clsAssemblies.GetComponentAssemblyMethodTable()
        cboCompoAssMethod.DisplayMember = "Name"
        cboCompoAssMethod.ValueMember = "Code"


        'Me.ddlDisassemblyType.Enabled = False
        Me.fndAssemblyCode.Enabled = False

        isLoadTime = False
        If clsCommon.myLen(Document_No) > 0 Then
            LoadData(Document_No, NavigatorType.Current)
        Else
            AddNew()
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub LoadAssemblyDisassemblyType()
        If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly", False) = CompairStringResult.Equal Then
            '' fill assembly type
            ddlDisassemblyType.DataSource = clsAssemblies.GetAssemblyTypeTable()
            ddlDisassemblyType.DisplayMember = "Name"
            ddlDisassemblyType.ValueMember = "Code"
        Else
            '' fill disassembly type
            ddlDisassemblyType.DataSource = clsAssemblies.GetDisassemblyTypeTable()
            ddlDisassemblyType.DisplayMember = "Name"
            ddlDisassemblyType.ValueMember = "Code"
        End If

    End Sub

    Private Sub frmAssemblies_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                              "TSPL_PROD_ASSEMBLIES " + Environment.NewLine +
                                              "TSPL_PROD_ASSEMBLIES_ITEM_DETAIL " + Environment.NewLine +
                                              "TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine +
                                              "Post Trasnaction " + Environment.NewLine +
                                              "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
                                              "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                              "TSPL_INVENTORY_MOVEMENT_new ")
            If btnPost.Enabled = False AndAlso btnsave.Enabled = False Then
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
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
        ElseIf e.KeyCode = Keys.F3 Then
            '======update by preeti gupta 17/10/2018
            If RunBatchFifowise = 0 Then
                OpenBatchItem(True)
            Else
                OpenBatchItemIfFIFIOSettingON()
            End If
        End If
    End Sub
#End Region
    '============created by preeti gupta===============
    Public Sub OpenBatchItemIfFIFIOSettingON()
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtp_AssembleDate.Value) = True Then
            Dim arr As List(Of clsBatchInventory) = Nothing
            Dim strBatchunion As String = ""
            If clsCommon.myLen(fndMainItem.Value) > 0 Then
                arr = TryCast(fndMainItem.Tag, List(Of clsBatchInventory))
            End If
            If Not arr Is Nothing Then
                If arr.Count > 0 Then
                    For Each obj As clsBatchInventory In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                    clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
                End If
            End If
        End If
    End Sub
    Public Sub OpenBatchItemIfFIFIOSettingONGridLevel()
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtp_AssembleDate.Value) = True Then
            Dim arr As List(Of clsBatchInventory) = Nothing
            Dim strBatchunion As String = ""
            If clsCommon.myLen(gvBOM.CurrentRow.Cells(colItemCode).Value) > 0 Then
                arr = TryCast(gvBOM.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
            End If
            If Not arr Is Nothing Then
                If arr.Count > 0 Then
                    For Each obj As clsBatchInventory In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "  Unit - " & clsCommon.myCstr(obj.UOM) & "        Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                    clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub gvBom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvBOM.KeyDown

        If e.KeyCode = Keys.F5 Then
            'If clsCommon.CompairString(cboTransactionType.Text, "DisAssembly") = CompairStringResult.Equal Then

            'End If
            If RunBatchFifowise = 0 Then
                OpenBatchItemGridLevel()
            Else
                OpenBatchItemIfFIFIOSettingONGridLevel()
            End If
        End If
    End Sub
    Sub OpenBatchItemGridLevel()
        If clsERPFuncationality.GetBatchWiseApplicableStatus(dtp_AssembleDate.Value) = True Then
            Dim TransType_Str As String = ""
            Dim blnBatchqty As Boolean = False
            If clsCommon.CompairString(cboTransactionType.Text, "DisAssembly") = CompairStringResult.Equal Then
                If gvBOM.Rows.Count > 0 Then
                    Dim isbatch As Boolean = clsItemMaster.IsBatchItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value))
                    If clsCommon.myCBool(isbatch) Then
                        Dim frm As frmBatchItemIn = New frmBatchItemIn()
                        frm.strItemCode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value)
                        frm.strItemName = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colitemDesc).Value)
                        frm.dblqty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)
                        frm.strUOM = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value)
                        'frm.dblMRP = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colMRP).Value)
                        frm.TransDate = dtp_AssembleDate.Value
                        frm.arr = TryCast(gvBOM.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvBOM.CurrentRow.Cells(colItemCode).Tag = frm.arr
                        End If
                    End If

                End If

            Else
                If RunBatchFifowise = 0 Then
                    ' w/o fifo start here
                    Dim isbatch As Boolean = clsItemMaster.IsBatchItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value))
                    If clsCommon.myCBool(isbatch) Then
                        TransType_Str = "Assembly"
                        Dim frm As frmBatchItemOut = New frmBatchItemOut()
                        frm.strItemCode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value)
                        frm.strItemName = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colitemDesc).Value)
                        frm.strLocationCode = fndLocation.Value
                        frm.strCurrDocNo = txtCode.Value
                        frm.strCurrDocType = TransType_Str
                        '"PS-SH"
                        frm.strUOM = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value)
                        'frm.dblMRP = clsCommon.myCdbl(gvBom.CurrentRow.Cells(colMRP).Value) comment by balwinder On UDL On 02/12/2016
                        frm.dblqty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)

                        frm.arr = TryCast(gvBOM.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gvBOM.CurrentRow.Cells(colItemCode).Tag = frm.arr
                        End If
                    End If

                    ' w/o fifo ends here

                Else
                    ' fifo start
                    TransType_Str = "Assembly"

                    For ii As Integer = 0 To gvBOM.Rows.Count - 1
                        Dim isbatch As Boolean = clsItemMaster.IsBatchItem(clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value))
                        If isbatch = True Then
                            Dim strBatchunion As String = ""
                            If RunBatchFifowise = 1 Then
                                If ii > 0 Then
                                    Dim strICodeOuter As String = clsCommon.myCstr(gvBOM.Rows(ii).Cells(colItemCode).Value)
                                    For jj As Integer = 0 To ii - 1
                                        Dim strICodeInner As String = clsCommon.myCstr(gvBOM.Rows(jj).Cells(colItemCode).Value)
                                        If clsCommon.CompairString(strICodeOuter, strICodeInner) = CompairStringResult.Equal Then
                                            Dim arr As List(Of clsBatchInventory) = Nothing
                                            arr = TryCast(gvBOM.Rows(jj).Cells(colItemCode).Tag, List(Of clsBatchInventory))
                                            For Each obj As clsBatchInventory In arr
                                                Dim dblqty As Double = obj.Qty
                                                If clsCommon.CompairString(clsCommon.myCstr(gvBOM.Rows(jj).Cells(colUnitCode).Value), clsCommon.myCstr(gvBOM.Rows(ii).Cells(colUnitCode).Value)) <> CompairStringResult.Equal Then
                                                    dblqty = GetConvQuantity(strICodeInner, clsCommon.myCstr(gvBOM.Rows(ii).Cells(colUnitCode).Value), clsCommon.myCstr(gvBOM.Rows(jj).Cells(colUnitCode).Value), obj.Qty)
                                                End If
                                                strBatchunion += " union all select '" & clsCommon.myCstr(obj.Batch_No) & "' as Batch_No, " &
                                                    "'" & clsCommon.myCstr(obj.Manual_BatchNo) & "' as Manual_BatchNo,'O' as In_Out_Type, " &
                                                    "'" & clsCommon.myCstr(gvBOM.Rows(ii).Cells(colUnitCode).Value) & "' as OrgUOM," & dblqty & " as OrgQty,0 as OrgMRP, " &
                                                    "'" & clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") & "' as Expiry_Date, " &
                                                    "'" & clsCommon.GetPrintDate(obj.Manufacture_Date, "dd/MMM/yyyy") & "' as Manufacture_Date, " &
                                                    "" & dblqty & " as Qty, 0 as MRP "
                                            Next
                                        End If
                                    Next
                                End If
                                gvBOM.CurrentRow = gvBOM.Rows(ii)

                                Dim frm As frmBatchItemOut = New frmBatchItemOut()
                                frm.strItemCode = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value)
                                frm.strItemName = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colitemDesc).Value)
                                frm.strLocationCode = fndLocation.Value
                                frm.strCurrDocNo = txtCode.Value
                                frm.strCurrDocType = TransType_Str
                                '"PS-SH"
                                frm.strUOM = clsCommon.myCstr(gvBOM.CurrentRow.Cells(colUnitCode).Value)
                                'frm.dblMRP = clsCommon.myCdbl(gvBom.CurrentRow.Cells(colMRP).Value) comment by balwinder on UDL on 02/12/2016
                                frm.dblqty = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value)
                                frm.arr = TryCast(gvBOM.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))

                                If frm.OpenSerialList(0, "", strBatchunion) Then
                                    gvBOM.CurrentRow.Cells(colItemCode).Tag = frm.arr
                                    blnBatchqty = True
                                Else
                                    Dim batchQty As Double = 0
                                    For Each obj As clsBatchInventory In frm.arr
                                        batchQty += obj.Qty
                                    Next
                                    clsCommon.MyMessageBoxShow("Please increase stock Item Code - " & frm.strItemCode & " , Entered Qty - " & clsCommon.myCstr(frm.dblqty) & " Batch Qty - " & clsCommon.myCstr(batchQty), Me.Text)
                                    blnBatchqty = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub
    Public Shared Function GetConvQuantity(ByVal strItem As String, ByVal strCurrentUnit As String, ByVal strConvertedUnit As String, ByVal dblQty As Double) As Double
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strConvertedUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strConvertedUnit & "'"))
            If dblCurrentConvF > 0 Then
                dblConvQty = Math.Round(Math.Round((dblOrgConvF / dblCurrentConvF), 2) * dblQty, 2)
            End If
        End If
        Return dblConvQty
    End Function
    Private Sub cboTransactionType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboTransactionType.SelectedIndexChanged
        Try
            icodestatus.IsVisible = False
            If isLoadTime = False Then
                If Me.cboTransactionType.SelectedValue = "Assembly" Then
                    LoadAssemblyDisassemblyType()
                    txtDisassCost.ReadOnly = False
                Else
                    LoadAssemblyDisassemblyType()
                    txtDisassCost.ReadOnly = True
                    If clsCommon.CompairString(ddlDisassemblyType.SelectedValue, "Other") = CompairStringResult.Equal Then
                        icodestatus.IsVisible = True
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub ddlDisassemblyType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlDisassemblyType.SelectedIndexChanged
        Try
            icodestatus.IsVisible = False
            If isLoadTime = False Then
                If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Assembly", False) = CompairStringResult.Equal Then
                    Me.fndAssemblyCode.Enabled = True
                    Me.fndBomCode.Enabled = True
                ElseIf clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                    Me.fndAssemblyCode.Enabled = False
                    Me.fndBomCode.Enabled = False
                    Me.fndBomCode.Value = Nothing
                    Me.gvBOM.Rows.Clear()
                    If clsCommon.CompairString(cboTransactionType.Text, "DisAssembly") = CompairStringResult.Equal Then
                        icodestatus.IsVisible = True
                    End If
                Else
                    Me.fndAssemblyCode.Enabled = False
                    Me.fndBomCode.Enabled = True
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fndAssemblyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndAssemblyCode._MYValidating
        Dim qry As String = "select code as [Code],description as [Name] from TSPL_PJC_ASSEMBLIES "
        fndAssemblyCode.Value = clsCommon.ShowSelectForm("TSPL_PJC_ASSEMBLIES", qry, "Code", "", fndAssemblyCode.Value.ToString, "CODE", isButtonClicked)
        lblAssemblyDesc.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_ASSEMBLIES where CODE='" + fndAssemblyCode.Value + "' ")
    End Sub
    Sub OpenLocationSubCalaculation(ByVal isButtonClick As Boolean)
        Dim qry As String
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select location", Me.Text)
        End If
        qry = "select * from (select Location,ICode,convert(decimal(18,2),SUM(qty*RI)) as StockQty from ( select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ( select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from " & _
 " ( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew ,0 as fat_kg,0 as snf_kg from TSPL_INVENTORY_MOVEMENT " & _
" left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code  " & _
" where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "'  " & _
" and TSPL_INVENTORY_MOVEMENT.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) " & _
" and tspl_location_master.Main_Location_Code='" & fndLocation.Value & "' " & _
" union all  select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew ,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg from TSPL_INVENTORY_MOVEMENT_new " & _
" left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code  " & _
" where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "' " & _
" and tspl_location_master.Main_Location_Code='" & fndLocation.Value & "' " & _
" and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103))ax)axa " & _
 " group by Item_Code,Location_Code,UOMNew)xx " & _
" left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM " & _
" left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" & fndUom.Value & "')axx group by axx.Location,axx.ICode)final where 2=2 "

        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("LOcation", qry)
        If Not dr Is Nothing Then
            If clsCommon.myLen(clsCommon.myCstr(dr("Location"))) > 0 Then
                gvBOM.CurrentRow.Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location"))
                gvBOM.CurrentRow.Cells(colLocationName).Value = clsLocation.GetName(clsCommon.myCstr(dr("Location")), Nothing)
                gvBOM.CurrentRow.Cells(colStockQty).Value = clsCommon.myCstr(dr("StockQty"))
            End If
        End If

    End Sub
    Sub OpenLocationCurrent(ByVal isButtonClick As Boolean)
        Dim qry As String
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select location", Me.Text)
        End If
        qry = "select * from (select Location,ICode,convert(decimal(18,2),SUM(qty*RI)) as StockQty from ( select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ( select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from " & _
 " ( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew ,0 as fat_kg,0 as snf_kg from TSPL_INVENTORY_MOVEMENT " & _
" left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code  " & _
" where TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "'  " & _
" and TSPL_INVENTORY_MOVEMENT.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) " & _
" and tspl_location_master.Location_Code='" & fndLocation.Value & "' " & _
" union all  select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew ,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103) then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg from TSPL_INVENTORY_MOVEMENT_new " & _
" left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code  " & _
" where TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "' " & _
" and tspl_location_master.Location_Code='" & fndLocation.Value & "' " & _
" and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<=convert(date,'" & dtp_AssembleDate.Value & "',103))ax)axa " & _
 " group by Item_Code,Location_Code,UOMNew)xx " & _
" left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM " & _
" left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" & fndUom.Value & "')axx group by axx.Location,axx.ICode)final where 2=2 "

        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("LOcationCur", qry)
        If Not dr Is Nothing Then
            If clsCommon.myLen(clsCommon.myCstr(dr("Location"))) > 0 Then
                gvBOM.CurrentRow.Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location"))
                gvBOM.CurrentRow.Cells(colLocationName).Value = clsLocation.GetName(clsCommon.myCstr(dr("Location")), Nothing)
                gvBOM.CurrentRow.Cells(colStockQty).Value = clsCommon.myCstr(dr("StockQty"))
            End If
        End If

    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            If clsCommon.CompairString(cboTransactionType.SelectedValue, "Disassembly") = CompairStringResult.Equal Then
                fndLocation.Value = clsProductionEntry.getLocationFinderWithBalance("", fndLocation.Value, fndMainItem.Value, True, isButtonClicked)
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                strMainLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 case when  TSPL_LOCATION_MASTER.Main_Location_Code is null then  TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end  as Main_Location_Code  from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Main_Location_Code='" + fndLocation.Value + "' OR Location_Code='" + fndLocation.Value + "' ")) ' prabhakar
                ' Add By prabhakar 
                If settAutoFillSameLocationInGrid Then
                    For Each row As GridViewRowInfo In gvBOM.Rows
                        row.Cells(colLocationCode).Value = fndLocation.Value
                        row.Cells(colLocationName).Value = clsLocation.GetName(Me.fndLocation.Value, Nothing)
                    Next
                End If
                ' End
                UpdateQty(False)
            Else
                Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
                Dim WhrCls As String = " Location_Type='Physical'  "
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                fndLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                strMainLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 case when  TSPL_LOCATION_MASTER.Main_Location_Code is null then  TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end  as Main_Location_Code  from TSPL_LOCATION_MASTER  where TSPL_LOCATION_MASTER.Main_Location_Code='" + fndLocation.Value + "' OR Location_Code='" + fndLocation.Value + "' ")) ' Prabhakar
                If settAutoFillSameLocationInGrid Then
                    For Each row As GridViewRowInfo In gvBOM.Rows
                        row.Cells(colLocationCode).Value = fndLocation.Value
                        row.Cells(colLocationName).Value = clsLocation.GetName(Me.fndLocation.Value, Nothing)
                    Next
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndMainItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMainItem._MYValidating
        Try
            'ITEM_TYPE IN ('F','O')
            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS [Item Description],ITEM_TYPE AS Type,Is_Serial_Item as [Is Serail Item],Product_type,STD_FatPer,STD_SNFPer FROM TSPL_ITEM_MASTER "
            fndMainItem.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", "", fndMainItem.Value, "", isButtonClicked)
            Dim objItm As New clsItemMaster
            '' NO CLASS  FOR ITEM MASTER(FINISHED)
            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE,coalesce(Is_Serial_Item,0) as Is_Serial_Item,Product_type,STD_FatPer,STD_SNFPer  FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & fndMainItem.Value & "'"

            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMainItemDesc.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
                fndUom.Value = DT_ITEM.Rows(0).Item("UNIT_CODE")
                Me.lblUnitName.Text = clsUnitMaster.GetUnitName(fndUom.Value)

                If DT_ITEM.Rows(0).Item("Is_Serial_Item") = 1 Then
                    Me.txtSerialNo.Enabled = True
                Else
                    Me.txtSerialNo.Enabled = False
                End If
                If clsCommon.CompairString(DT_ITEM.Rows(0).Item("Product_type"), "MI") = CompairStringResult.Equal Then
                    txtFatPer.ReadOnly = False
                    txtSNFPer.ReadOnly = False
                Else
                    txtFatPer.ReadOnly = True
                    txtSNFPer.ReadOnly = True
                End If
                txtFatPer.Text = clsCommon.myCdbl(DT_ITEM.Rows(0).Item("STD_FatPer"))
                txtSNFPer.Text = clsCommon.myCdbl(DT_ITEM.Rows(0).Item("STD_SNFPer"))

                If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                    LoadGridColumns()
                    gvBOM.Rows.AddNew()
                    'Me.fndMainItem.Tag = fndMainItem.Value
                Else
                    DisplayBOM()
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Function CheckSerialNo() As Boolean
        Dim DT_ITEM As DataTable
        Dim STRQ As String
        STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE,coalesce(Is_Serial_Item,0) as Is_Serial_Item,Product_type FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & fndMainItem.Value & "'"

        DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
        If DT_ITEM.Rows.Count > 0 Then
            If DT_ITEM.Rows(0).Item("Is_Serial_Item") = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Private Sub fndUom__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndUom._MYValidating
        Try
            Dim WhrCls As String = " tspl_item_master.Item_Code='" & fndMainItem.Value & "'  "
            Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as [UOM Description] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=tspl_item_uom_detail.item_code  left join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code "
            fndUom.Value = clsCommon.ShowSelectForm("TSPL_UOM_HEAD", qry, "Code", WhrCls, fndUom.Value, "", isButtonClicked)
            lblUnitName.Text = fndUom.Value
            UpdateQty(True)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub
    Private Sub fndBomCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBomCode._MYValidating
        Try
            If clsCommon.myLen(Me.fndMainItem.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Item first.")
                Exit Sub
            End If
            Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_QUANTITY as [Build Qty],TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD  "
            Dim WhrClause As String = ""

            If Me.cboTransactionType.SelectedValue = "Assembly" Then
                WhrClause = " PROD_ITEM_CODE IN ('" & fndMainItem.Value & "') AND Is_POST=1 and '" & clsCommon.GetPrintDate(dtp_AssembleDate.Value, "dd-MMM-yyyy") & "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date)"
            Else
                WhrClause = " (BOM_CODE in (select BOM_CODE from TSPL_MF_BOM_DETAIL WHERE CONSM_ITEM_CODE='" & Me.fndMainItem.Value & "') or PROD_ITEM_CODE IN ('" & fndMainItem.Value & "')) AND Is_POST=1 and '" & clsCommon.GetPrintDate(dtp_AssembleDate.Value, "dd-MMM-yyyy") & "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date)"
            End If
            fndBomCode.Value = clsCommon.ShowSelectForm("TSPL_PP_BOM_HEAD", qry, "Code", WhrClause, fndBomCode.Value.ToString, "BOM_CODE", isButtonClicked)
            Dim strq As String = "select PROD_ITEM_CODE,PROD_QUANTITY,DESCRIPTION,PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD where BOM_CODE='" + fndBomCode.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strq)
            If dt.Rows.Count > 0 Then
                lblBomDesc.Text = dt.Rows(0).Item("DESCRIPTION")
                txtBuildQty.Text = dt.Rows(0).Item("PROD_QUANTITY")
                txtQuantity.Text = dt.Rows(0).Item("PROD_QUANTITY")
                fndMainItem.Value = dt.Rows(0).Item("PROD_ITEM_CODE")
                fndUom.Value = clsCommon.myCstr(dt.Rows(0).Item("PROD_ITEM_UNIT_CODE"))
                lblUnitName.Text = clsUnitMaster.GetUnitName(fndUom.Value)
                LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
                UpdateQty(True)
            Else
                lblBomDesc.Text = ""
                txtBuildQty.Text = ""
                fndUom.Value = ""
                lblUnitName.Text = ""
                Me.gvBOM.Rows.Clear()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub DisplayBOM()
        Dim qry As String = "select BOM_CODE as [Code],DESCRIPTION as [Name],PROD_ITEM_CODE as [Build Item Code],PROD_QUANTITY as [Build Qty],TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD  "
        Dim WhrClause As String = ""

        If Me.cboTransactionType.SelectedValue = "Assembly" Then
            WhrClause = " PROD_ITEM_CODE IN ('" & fndMainItem.Value & "') AND Is_POST=1"
        Else
            WhrClause = " (BOM_CODE in (select BOM_CODE from TSPL_PP_BOM_HEAD WHERE CONSM_ITEM_CODE='" & Me.fndMainItem.Value & "') or PROD_ITEM_CODE IN ('" & fndMainItem.Value & "'))AND IS_POST=1"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry & " where " & WhrClause)
        If dt.Rows.Count = 0 Then
            Me.fndBomCode.Value = Nothing
            Me.lblBomDesc.Text = ""
            clsCommon.MyMessageBoxShow("BOM not found for Item '" & Me.fndMainItem.Value & "'")
            Exit Sub
        ElseIf dt.Rows.Count = 1 Then
            Me.fndBomCode.Value = dt.Rows(0).Item("Code")
            Me.lblBomDesc.Text = dt.Rows(0).Item("Name")
            txtBuildQty.Text = dt.Rows(0).Item("Build Qty")
            txtQuantity.Text = dt.Rows(0).Item("Build Qty")
            fndMainItem.Value = dt.Rows(0).Item("Build Item Code")
            fndUom.Value = clsCommon.myCstr(dt.Rows(0).Item("PROD_ITEM_UNIT_CODE"))
            Me.lblUnitName.Text = clsUnitMaster.GetUnitName(fndUom.Value)
            'fndMainItem.Tag = dt.Rows(0).Item("Build Item Code")
            LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
        Else
            fndBomCode.Value = clsCommon.ShowSelectForm("TSPL_PP_BOM_HEAD", qry, "Code", WhrClause, fndBomCode.Value.ToString, "BOM_CODE", False)
            Dim strq As String = "select PROD_QUANTITY,DESCRIPTION,PROD_ITEM_CODE as [Build Item Code],TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE from TSPL_PP_BOM_HEAD where BOM_CODE='" + fndBomCode.Value + "'"

            dt = clsDBFuncationality.GetDataTable(strq)

            If dt.Rows.Count > 0 Then
                lblBomDesc.Text = dt.Rows(0).Item("DESCRIPTION")
                txtBuildQty.Text = dt.Rows(0).Item("PROD_QUANTITY")
                txtQuantity.Text = dt.Rows(0).Item("PROD_QUANTITY")
                fndMainItem.Value = dt.Rows(0).Item("Build Item Code")
                fndUom.Value = clsCommon.myCstr(dt.Rows(0).Item("PROD_ITEM_UNIT_CODE"))
                lblUnitName.Text = clsUnitMaster.GetUnitName(fndUom.Value)
                LoadBomDetail(fndBomCode.Value, NavigatorType.Current)
            Else
                lblBomDesc.Text = ""
                txtBuildQty.Text = ""
                txtQuantity.Text = 0
                fndUom.Value = ""
                lblUnitName.Text = ""
                Me.gvBOM.Rows.Clear()
            End If
        End If


    End Sub
    Sub LoadBomDetail(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        gvBOM.Rows.Clear()
        Dim obj As clsBOM
        obj = clsBOM.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso obj.ObjListBOM.Count > 0) Then
            For Each obj1 As clsBOMItemDetail In obj.ObjListBOM
                If clsItemMaster.IsSerializeItem(obj1.CONSM_ITEM_CODE) = True Then
                    For intloop As Integer = 0 To (obj1.CONSM_QUANTITY * Val(Me.txtQuantity.Text)) / Val(Me.txtBuildQty.Text) - 1
                        gvBOM.Rows.AddNew()
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = gvBOM.CurrentRow.Index + 1
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                        'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = obj1.CONSM_QUANTITY
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value = obj1.CONSM_ITEM_PRODUCT_TYPE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = 1
                        If clsCommon.CompairString(obj1.CONSM_ITEM_PRODUCT_TYPE, "MI") = CompairStringResult.Equal Then
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = False
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = False
                        Else
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = True
                            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = True
                        End If
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).Value = obj1.FAT
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).Value = obj1.SNF
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATKG).Value = obj1.fat_kg
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFKG).Value = obj1.snf_kg
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = Me.fndLocation.Value
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationName).Value = clsLocation.GetName(Me.fndLocation.Value, Nothing)
                    Next
                Else
                    gvBOM.Rows.AddNew()
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = gvBOM.CurrentRow.Index + 1
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj1.CONSM_ITEM_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj1.ITEM_DESCRIPTION
                    'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Tag = obj1.CONSM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colProductType).Value = obj1.CONSM_ITEM_PRODUCT_TYPE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = (obj1.CONSM_QUANTITY * Val(Me.txtQuantity.Text)) / Val(Me.txtBuildQty.Text)

                    If clsCommon.CompairString(obj1.CONSM_ITEM_PRODUCT_TYPE, "MI") = CompairStringResult.Equal Then
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = False
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = False
                        If settPickCostFromItemMaster Then
                            Dim objQCPAR As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(obj1.CONSM_ITEM_CODE, Nothing)
                            If objQCPAR IsNot Nothing Then
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATRate).Value = objQCPAR.FATRate
                                gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFRate).Value = objQCPAR.SNFRate
                            End If
                        End If
                    Else
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).ReadOnly = True
                        gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).ReadOnly = True
                    End If

                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).Value = obj1.FAT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).Value = obj1.SNF
                    'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATKG).Value = obj1.fat_kg
                    'gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFKG).Value = obj1.snf_kg


                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj1.CONSM_ITEM_UNIT_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationCode).Value = Me.fndLocation.Value
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLocationName).Value = clsLocation.GetName(Me.fndLocation.Value, Nothing)
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value, clsCommon.myCdbl(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value), clsCommon.myCdbl(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFATPER).Value), Nothing)
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value, gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value, clsCommon.myCdbl(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value), clsCommon.myCdbl(gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colSNFPER).Value), Nothing)


                End If
            Next
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim trans As SqlClient.SqlTransaction = Nothing
        Try

            If (myMessages.postConfirm()) Then
                If (SaveData(True)) Then
                    trans = clsDBFuncationality.GetTransactin

                    clsAssembliesDis.PostData(txtCode.Value, True, trans)
                    'UpdateInventoryMovement(trans)

                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If

            End If

        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Function UpdateInventoryMovement(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
    '    Dim obj As New clsInventoryMovement
    '    Dim objMilk As New clsInventoryMovementNew
    '    '' get data
    '    Dim objData As clsAssembliesDis = clsAssembliesDis.GetData(Me.txtCode.Value, NavigatorType.Current, trans)
    '    Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
    '    Dim ArrInventoryMovementMilk As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
    '    Dim strq As String = ""
    '    Dim strItemTypeToSave As String = ""
    '    Dim strItemType As String
    '    Dim Product_Type As String = ""

    '    If objData.TRANSACTION_TYPE = "Assembly" Then
    '        '' in produced item
    '        Product_Type = clsItemMaster.GetItemProductType(Me.fndMainItem.Value, trans)
    '        If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
    '            obj = New clsInventoryMovement
    '            obj.Trans_Type = "Assembly"
    '            obj.InOut = "I"
    '            obj.Location_Code = Me.fndLocation.Value
    '            obj.Item_Code = Me.fndMainItem.Value
    '            obj.Item_Desc = lblMainItemDesc.Text
    '            obj.Qty = Me.txtQuantity.Text
    '            obj.UOM = Me.lblUnitName.Text
    '            obj.Source_Doc_No = Me.txtCode.Value
    '            obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "AT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            obj.ItemType = strItemTypeToSave
    '            obj.Basic_Cost = clsCommon.myCdbl(Me.txtDisassCost.Text)
    '            obj.MRP = obj.Basic_Cost
    '            obj.Add_Cost = obj.Basic_Cost
    '            obj.Net_Cost = obj.Basic_Cost
    '            obj.MFG_Date = dtp_AssembleDate.Value

    '            ArrInventoryMovement.Add(obj)
    '        Else
    '            objMilk = New clsInventoryMovementNew
    '            objMilk.Trans_Type = "Assembly"
    '            objMilk.InOut = "I"
    '            objMilk.Location_Code = Me.fndLocation.Value
    '            objMilk.Item_Code = Me.fndMainItem.Value
    '            objMilk.Item_Desc = lblMainItemDesc.Text
    '            objMilk.Qty = Me.txtQuantity.Text
    '            objMilk.UOM = Me.lblUnitName.Text
    '            objMilk.Source_Doc_No = Me.txtCode.Value
    '            objMilk.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "AT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            objMilk.ItemType = strItemTypeToSave
    '            objMilk.Basic_Cost = clsCommon.myCdbl(Me.txtDisassCost.Text)
    '            objMilk.MRP = obj.Basic_Cost
    '            objMilk.Add_Cost = obj.Basic_Cost
    '            objMilk.Net_Cost = obj.Basic_Cost
    '            objMilk.MFG_Date = dtp_AssembleDate.Value

    '            objMilk.FAT_Per = obj.FAT_Per
    '            objMilk.SNF_Per = obj.SNF_Per
    '            objMilk.FAT_KG = obj.FAT_KG
    '            objMilk.SNF_KG = obj.SNF_KG


    '            '' UPDATE PRODUCTION COST
    '            'objInventoryMovemntMilk.Fat_Rate = objTr.FAT_Rate
    '            'objInventoryMovemntMilk.SNF_Rate = objTr.SNF_Rate
    '            'objInventoryMovemntMilk.Fat_Amt = objTr.Fat_Amt
    '            'objInventoryMovemntMilk.SNF_Amt = objTr.SNF_Amt

    '            'objInventoryMovemntMilk.Avg_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '            'objInventoryMovemntMilk.FIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '            'objInventoryMovemntMilk.LIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '            'If clsCommon.CompairString(objInventoryMovemntMilk.InOut, "I") = CompairStringResult.Equal Then
    '            'objInventoryMovemntMilk.Basic_Cost = (objTr.Fat_Amt + objTr.SNF_Amt) / IIf(objTr.FINAL_PRODUCTION_QTY = 0, 1, objTr.FINAL_PRODUCTION_QTY)
    '            'objInventoryMovemntMilk.Net_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '            'End If
    '            ArrInventoryMovementMilk.Add(objMilk)
    '        End If



    '        For Each dr As GridViewRowInfo In gvBOM.Rows
    '            obj = New clsInventoryMovement
    '            Product_Type = clsItemMaster.GetItemProductType(clsCommon.myCstr(dr.Cells(colItemCode).Value), trans)
    '            If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
    '                obj.Trans_Type = "Assembly"
    '                obj.InOut = "O"
    '                obj.Location_Code = dr.Cells(colLocationCode).Value
    '                obj.Item_Code = clsCommon.myCstr(dr.Cells(colItemCode).Value)
    '                obj.Item_Desc = dr.Cells(colitemDesc).Value.ToString
    '                obj.Qty = dr.Cells(colqty).Value
    '                obj.UOM = dr.Cells(colUnitCode).Value
    '                obj.Source_Doc_No = Me.txtCode.Value
    '                obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '                obj.itemstatus = ""
    '                obj.itemtypeinventry = ""



    '                strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
    '                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "RM"
    '                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "OT"
    '                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "FT"
    '                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "AT"
    '                Else
    '                    strItemTypeToSave = strItemType

    '                End If
    '                obj.ItemType = strItemTypeToSave
    '                obj.Basic_Cost = clsCommon.myCdbl(dr.Cells(colItemAmount).Value)
    '                obj.MRP = obj.Basic_Cost
    '                obj.Add_Cost = obj.Basic_Cost
    '                obj.Net_Cost = obj.Basic_Cost
    '                ArrInventoryMovement.Add(obj)
    '            Else
    '                objMilk.Trans_Type = "Assembly"
    '                objMilk.InOut = "O"
    '                objMilk.Location_Code = dr.Cells(colLocationCode).Value
    '                objMilk.Item_Code = clsCommon.myCstr(dr.Cells(colItemCode).Value)
    '                objMilk.Item_Desc = dr.Cells(colitemDesc).Value.ToString
    '                objMilk.Qty = dr.Cells(colqty).Value
    '                objMilk.UOM = dr.Cells(colUnitCode).Value
    '                objMilk.Source_Doc_No = Me.txtCode.Value
    '                objMilk.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '                objMilk.itemstatus = ""
    '                objMilk.itemtypeinventry = ""

    '                objMilk.FAT_Per = obj.FAT_Per
    '                objMilk.SNF_Per = obj.SNF_Per
    '                objMilk.FAT_KG = obj.FAT_KG
    '                objMilk.SNF_KG = obj.SNF_KG

    '                '' UPDATE PRODUCTION COST
    '                'objInventoryMovemntMilk.Fat_Rate = objTr.FAT_Rate
    '                'objInventoryMovemntMilk.SNF_Rate = objTr.SNF_Rate
    '                'objInventoryMovemntMilk.Fat_Amt = objTr.Fat_Amt
    '                'objInventoryMovemntMilk.SNF_Amt = objTr.SNF_Amt

    '                'objInventoryMovemntMilk.Avg_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '                'objInventoryMovemntMilk.FIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '                'objInventoryMovemntMilk.LIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '                'If clsCommon.CompairString(objInventoryMovemntMilk.InOut, "I") = CompairStringResult.Equal Then
    '                'objInventoryMovemntMilk.Basic_Cost = (objTr.Fat_Amt + objTr.SNF_Amt) / IIf(objTr.FINAL_PRODUCTION_QTY = 0, 1, objTr.FINAL_PRODUCTION_QTY)
    '                'objInventoryMovemntMilk.Net_Cost = objTr.Fat_Amt + objTr.SNF_Amt
    '                'End If

    '                strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
    '                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "RM"
    '                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "OT"
    '                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "FT"
    '                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "AT"
    '                Else
    '                    strItemTypeToSave = strItemType

    '                End If
    '                objMilk.ItemType = strItemTypeToSave
    '                objMilk.Basic_Cost = clsCommon.myCdbl(dr.Cells(colItemAmount).Value)
    '                objMilk.MRP = obj.Basic_Cost
    '                objMilk.Add_Cost = obj.Basic_Cost
    '                objMilk.Net_Cost = obj.Basic_Cost
    '                ArrInventoryMovementMilk.Add(objMilk)
    '            End If
    '        Next
    '        clsInventoryMovement.SaveData("Assembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '        clsInventoryMovementNew.SaveData("Assembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovementMilk, trans)

    '        If clsCommon.myLen(txtSerialNo.Text) > 0 Then
    '            Dim objList As New List(Of clsSerializeInvenotry)
    '            Dim objsri As New clsSerializeInvenotry
    '            objsri.Auto_Sr_No = txtSerialNo.Text
    '            objsri.Document_Code = txtCode.Value
    '            objsri.Document_Date = dtp_AssembleDate.Value
    '            objsri.Document_Type = "Assembly"
    '            objsri.In_Out_Type = "I"
    '            objsri.Item_Code = fndMainItem.Value
    '            objsri.Line_No = 1
    '            objsri.Location_Code = fndLocation.Value
    '            objsri.Parent_Line_No = 1
    '            objsri.Tag_No = ""
    '            objList.Add(objsri)
    '            clsSerializeInvenotry.SaveData("Assembly", txtCode.Value, dtp_AssembleDate.Value, "I", fndMainItem.Value, fndLocation.Value, 1, objList, trans)
    '        End If
    '    Else
    '        '' in consumed item

    '        Product_Type = clsItemMaster.GetItemProductType(Me.fndMainItem.Value, trans)
    '        If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
    '            obj = New clsInventoryMovement
    '            obj.Trans_Type = "Disassembly"
    '            obj.InOut = "O"
    '            obj.Location_Code = Me.fndLocation.Value
    '            obj.Item_Code = Me.fndMainItem.Value
    '            obj.Item_Desc = lblMainItemDesc.Text
    '            obj.Qty = Me.txtQuantity.Text
    '            obj.UOM = Me.lblUnitName.Text
    '            obj.Source_Doc_No = Me.txtCode.Value
    '            obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "AT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            obj.ItemType = strItemTypeToSave
    '            obj.Basic_Cost = clsCommon.myCdbl(Me.txtDisassCost.Text)
    '            obj.MRP = obj.Basic_Cost
    '            obj.Add_Cost = obj.Basic_Cost
    '            obj.Net_Cost = obj.Basic_Cost
    '            'obj.MFG_Date = dtp_AssembleDate.Value

    '            ArrInventoryMovement.Add(obj)
    '        Else
    '            objMilk = New clsInventoryMovementNew
    '            objMilk.Trans_Type = "Disassembly"
    '            objMilk.InOut = "O"
    '            objMilk.Location_Code = Me.fndLocation.Value
    '            objMilk.Item_Code = Me.fndMainItem.Value
    '            objMilk.Item_Desc = lblMainItemDesc.Text
    '            objMilk.Qty = Me.txtQuantity.Text
    '            objMilk.UOM = Me.lblUnitName.Text
    '            objMilk.Source_Doc_No = Me.txtCode.Value
    '            objMilk.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '            strItemType = clsItemMaster.GetItemType(Me.fndMainItem.Value, trans)
    '            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "RM"
    '            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "OT"
    '            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "FT"
    '            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                strItemTypeToSave = "AT"
    '            Else
    '                strItemTypeToSave = strItemType
    '                'Throw New Exception("Item Type not found: " + strItemType)
    '            End If
    '            objMilk.ItemType = strItemTypeToSave
    '            objMilk.Basic_Cost = clsCommon.myCdbl(Me.txtDisassCost.Text)
    '            objMilk.MRP = obj.Basic_Cost
    '            objMilk.Add_Cost = obj.Basic_Cost
    '            objMilk.Net_Cost = obj.Basic_Cost

    '            objMilk.FAT_Per = obj.FAT_Per
    '            objMilk.SNF_Per = obj.SNF_Per
    '            objMilk.FAT_KG = obj.FAT_KG
    '            objMilk.SNF_KG = obj.SNF_KG
    '            ArrInventoryMovementMilk.Add(objMilk)
    '        End If

    '        For Each dr As GridViewRowInfo In gvBOM.Rows
    '            Product_Type = clsItemMaster.GetItemProductType(clsCommon.myCstr(dr.Cells(colItemCode).Value), trans)
    '            If clsCommon.CompairString(Product_Type, "MI") <> CompairStringResult.Equal Then
    '                obj = New clsInventoryMovement
    '                obj.Trans_Type = "Disassembly"
    '                obj.InOut = "I"
    '                obj.Location_Code = dr.Cells(colLocationCode).Value
    '                obj.Item_Code = dr.Cells(colItemCode).Value
    '                obj.Item_Desc = dr.Cells(colitemDesc).Value.ToString
    '                obj.Qty = dr.Cells(colqty).Value
    '                obj.UOM = dr.Cells(colUnitCode).Value
    '                obj.Source_Doc_No = Me.txtCode.Value
    '                obj.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '                obj.itemstatus = ""
    '                obj.itemtypeinventry = ""
    '                If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
    '                    obj.itemtypeinventry = clsCommon.myCstr(dr.Cells(colIcodeStatus).Value)
    '                    obj.itemstatus = "OLD"
    '                End If

    '                strItemType = clsItemMaster.GetItemType(obj.Item_Code, trans)
    '                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "RM"
    '                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "OT"
    '                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "FT"
    '                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "AT"
    '                Else
    '                    strItemTypeToSave = strItemType
    '                    'Throw New Exception("Item Type not found: " + strItemType)
    '                End If
    '                obj.ItemType = strItemTypeToSave
    '                obj.Basic_Cost = clsCommon.myCdbl(dr.Cells(colItemAmount).Value)
    '                obj.MRP = obj.Basic_Cost
    '                obj.Add_Cost = obj.Basic_Cost
    '                obj.Net_Cost = obj.Basic_Cost
    '                obj.MFG_Date = dtp_AssembleDate.Value

    '                If clsCommon.CompairString(obj.itemtypeinventry, "SCRAP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
    '                    ArrInventoryMovement.Add(obj)
    '                Else
    '                    ArrInventoryMovement.Add(obj)
    '                End If
    '            Else
    '                objMilk = New clsInventoryMovementNew
    '                objMilk.Trans_Type = "Disassembly"
    '                objMilk.InOut = "I"
    '                objMilk.Location_Code = dr.Cells(colLocationCode).Value
    '                objMilk.Item_Code = dr.Cells(colItemCode).Value
    '                objMilk.Item_Desc = dr.Cells(colitemDesc).Value.ToString
    '                objMilk.Qty = dr.Cells(colqty).Value
    '                objMilk.UOM = dr.Cells(colUnitCode).Value
    '                objMilk.Source_Doc_No = Me.txtCode.Value
    '                objMilk.Source_Doc_Date = Me.dtp_AssembleDate.Value

    '                objMilk.itemstatus = ""
    '                objMilk.itemtypeinventry = ""
    '                If clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
    '                    objMilk.itemtypeinventry = clsCommon.myCstr(dr.Cells(colIcodeStatus).Value)
    '                    objMilk.itemstatus = "OLD"
    '                End If

    '                strItemType = clsItemMaster.GetItemType(objMilk.Item_Code, trans)
    '                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "RM"
    '                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "OT"
    '                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "FT"
    '                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
    '                    strItemTypeToSave = "AT"
    '                Else
    '                    strItemTypeToSave = strItemType
    '                    'Throw New Exception("Item Type not found: " + strItemType)
    '                End If
    '                objMilk.ItemType = strItemTypeToSave
    '                objMilk.Basic_Cost = clsCommon.myCdbl(dr.Cells(colItemAmount).Value)
    '                objMilk.MRP = obj.Basic_Cost
    '                objMilk.Add_Cost = obj.Basic_Cost
    '                objMilk.Net_Cost = obj.Basic_Cost
    '                objMilk.MFG_Date = dtp_AssembleDate.Value

    '                objMilk.FAT_Per = 0 'obj.FAT_Per
    '                objMilk.SNF_Per = 0 'obj.SNF_Per
    '                objMilk.FAT_KG = 0 ' obj.FAT_KG
    '                objMilk.SNF_KG = 0 'obj.SNF_KG

    '                If clsCommon.CompairString(obj.itemtypeinventry, "SCRAP") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboTransactionType.Text, "Disassembly") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlDisassemblyType.Text, "Other") = CompairStringResult.Equal Then
    '                    ArrInventoryMovementMilk.Add(objMilk)
    '                Else
    '                    ArrInventoryMovementMilk.Add(objMilk)
    '                End If
    '            End If                
    '        Next
    '        clsInventoryMovement.SaveData("Disassembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '        clsInventoryMovementNew.SaveData("Disassembly", Me.txtCode.Value, clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(Me.dtp_AssembleDate.Value, "dd/MM/yyyy"), ArrInventoryMovementMilk, trans)
    '    End If
    '    Return True
    'End Function
    '' makes slow so commented by Panch Raj. used lost focus instead
    'Private Sub txtQuantity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQuantity.TextChanged
    '    UpdateQty()
    'End Sub
    Sub UpdateQty(ByVal IsShowBatchForm As Boolean)
        Try
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
            Else
                For Each row As GridViewRowInfo In gvBOM.Rows
                    Dim BomQty As Decimal = clsBOM.GetBOMQty(fndBomCode.Value, row.Cells(colItemCode).Value, Nothing)
                    'If BomQty > 0 Then
                    Dim BuilD_UOM As String = clsBOM.GetBOMBuildUOM(fndBomCode.Value, Nothing)
                    If clsCommon.myLen(BuilD_UOM) <= 0 Then
                        Throw New Exception("Build UOM not found for BOM -" & fndBomCode.Value & "")
                    End If
                    If clsItemMaster.IsSerializeItem(row.Cells(colItemCode).Value) = True Then
                        row.Cells(colqty).Value = 1
                    Else
                        Dim qty As Decimal = Val(Me.txtQuantity.Text) * clsItemMaster.GetConvertionFactor(fndMainItem.Value, fndUom.Value, Nothing) / clsItemMaster.GetConvertionFactor(fndMainItem.Value, BuilD_UOM, Nothing)
                        If Val(Me.txtBuildQty.Text) <= 0 Then
                            Throw New Exception("Build Qty not found for BOM -" & fndBomCode.Value & "")
                        End If
                        row.Cells(colqty).Value = (BomQty * qty) / clsCommon.myCdbl(Me.txtBuildQty.Text)
                    End If
                    If clsCommon.CompairString(row.Cells(colProductType).Value, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(row.Cells(colProductType).Value, "MP") = CompairStringResult.Equal Then
                        row.Cells(colFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(row.Cells(colItemCode).Value, row.Cells(colUnitCode).Value, clsCommon.myCdbl(row.Cells(colqty).Value), clsCommon.myCdbl(row.Cells(colFATPER).Value), Nothing)
                        row.Cells(colSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(row.Cells(colItemCode).Value, row.Cells(colUnitCode).Value, clsCommon.myCdbl(row.Cells(colqty).Value), clsCommon.myCdbl(row.Cells(colSNFPER).Value), Nothing)
                    End If

                Next
            End If
            '' calculate
            If isLoadTime = False Then
                If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Disassembly") = CompairStringResult.Equal Then
                    If clsCommon.myLen(fndLocation.Value) > 0 Then
                        txtDisassCost.Text = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, fndMainItem.Value, fndLocation.Value, clsCommon.myCdbl(txtQuantity.Text) * clsItemMaster.GetConvertionFactor(fndMainItem.Value, fndUom.Value, Nothing), dtp_AssembleDate.Value, dtp_AssembleDate.Value, False, Nothing)
                    End If
                End If
            End If
            '' calculate fan kg and snf kg
            txtFatKG.Text = clsBOM.GetFatSNFKG_AfterConversion(fndMainItem.Value, fndUom.Value, clsCommon.myCdbl(txtQuantity.Text), clsCommon.myCdbl(txtFatPer.Text), Nothing)
            txtSNFKG.Text = clsBOM.GetFatSNFKG_AfterConversion(fndMainItem.Value, fndUom.Value, clsCommon.myCdbl(txtQuantity.Text), clsCommon.myCdbl(txtSNFPer.Text), Nothing)
            'If Me.cboTransactionType.SelectedValue = "Disassembly" Then
            '    OpenBatchItem()
            'End If
            'If Me.cboTransactionType.SelectedValue = "Assembly" Then
            '    OpenBatchItemGridLevel()
            'End If
            If UsLock1.Status = ERPTransactionStatus.Pending Then
                OpenBatchItem(IsShowBatchForm)
            End If

            If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly") = CompairStringResult.Equal Then
                OpenBatchItemGridLevel()
            End If


        Catch ex As Exception
        Finally
            isCellValueChangedOpen = False
        End Try

    End Sub

    Sub UpdateCost()
        Try
            Dim Bomcost As Double = 0
            For Each row As GridViewRowInfo In gvBOM.Rows
                Bomcost += clsCommon.myCdbl(row.Cells(colItemAmount).Value)
            Next
            txtDisassCost.Text = Bomcost
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
        If gvBOM.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            ''=============Parteek================

            If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                If e.Column Is gvBOM.Columns(colLocationCode) Then
                    'OpenLocationSubCalaculation(False)  
                    If clsCommon.myLen(clsCommon.myCstr(strMainLoc)) > 0 Then
                        gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("TSPL_LOCATION_MASTER.Main_Location_Code='" & strMainLoc & "' OR Location_Code='" & strMainLoc & "'", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False) ' prabhakar
                    Else
                        gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False)
                    End If

                    ' gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("TSPL_LOCATION_MASTER.Main_Location_Code='" & fndLocation.Value & "' OR Location_Code='" & fndLocation.Value & "'", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False)
                    gvBOM.CurrentRow.Cells(colLocationName).Value = clsLocation.GetName(gvBOM.CurrentRow.Cells(colLocationCode).Value, Nothing)
                End If
            Else
                If e.Column Is gvBOM.Columns(colLocationCode) Then
                    If clsCommon.myLen(clsCommon.myCstr(strMainLoc)) > 0 Then
                        gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("TSPL_LOCATION_MASTER.Main_Location_Code='" & strMainLoc & "' OR Location_Code='" & strMainLoc & "'", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False) ' prabhakar
                    Else
                        gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False)
                    End If


                    'gvBOM.CurrentRow.Cells(colLocationCode).Value = clsProductionEntry.getLocationFinderWithBalance("TSPL_LOCATION_MASTER.Main_Location_Code='" & fndLocation.Value & "' OR Location_Code='" & fndLocation.Value & "'", gvBOM.CurrentRow.Cells(colLocationCode).Value, gvBOM.CurrentRow.Cells(colItemCode).Value, False, False)
                    gvBOM.CurrentRow.Cells(colLocationName).Value = clsLocation.GetName(gvBOM.CurrentRow.Cells(colLocationCode).Value, Nothing)
                    'OpenLocationCurrent(False)


                End If

            End If
            ''=============End================
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                If e.Column Is gvBOM.Columns(colItemCode) Then
                    Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), "", False)

                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                        gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                        gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                        gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                        gvBOM.CurrentRow.Cells(colProductType).Value = clsItemMaster.GetItemProductType(obj.PROD_ITEM_CODE, Nothing)
                        If clsCommon.CompairString(gvBOM.CurrentRow.Cells(colProductType).Value, "MI") = CompairStringResult.Equal Then
                            gvBOM.CurrentRow.Cells(colFATPER).ReadOnly = False
                            gvBOM.CurrentRow.Cells(colSNFPER).ReadOnly = False
                            gvBOM.CurrentRow.Cells(colFATPER).Value = clsItemMaster.GetItemFatSNF(gvBOM.CurrentRow.Cells(colItemCode).Value, Nothing).FAT_Per
                            gvBOM.CurrentRow.Cells(colSNFPER).Value = clsItemMaster.GetItemFatSNF(gvBOM.CurrentRow.Cells(colItemCode).Value, Nothing).SNF_Per
                            If settPickCostFromItemMaster Then
                                Dim objQCPAR As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(obj.PROD_ITEM_CODE, Nothing)
                                If objQCPAR IsNot Nothing Then
                                    gvBOM.CurrentRow.Cells(colFATRate).Value = objQCPAR.FATRate
                                    gvBOM.CurrentRow.Cells(colSNFRate).Value = objQCPAR.SNFRate
                                End If
                            End If
                        ElseIf clsCommon.CompairString(gvBOM.CurrentRow.Cells(colProductType).Value, "MP") = CompairStringResult.Equal Then
                            gvBOM.CurrentRow.Cells(colFATPER).ReadOnly = True
                            gvBOM.CurrentRow.Cells(colSNFPER).ReadOnly = True
                            gvBOM.CurrentRow.Cells(colFATPER).Value = clsItemMaster.GetItemFatSNF(gvBOM.CurrentRow.Cells(colItemCode).Value, Nothing).FAT_Per
                            gvBOM.CurrentRow.Cells(colSNFPER).Value = clsItemMaster.GetItemFatSNF(gvBOM.CurrentRow.Cells(colItemCode).Value, Nothing).SNF_Per
                        Else
                            gvBOM.CurrentRow.Cells(colFATPER).ReadOnly = True
                            gvBOM.CurrentRow.Cells(colSNFPER).ReadOnly = True
                            gvBOM.CurrentRow.Cells(colFATPER).Value = 0
                            gvBOM.CurrentRow.Cells(colSNFPER).Value = 0
                        End If

                    End If
                End If
            End If
            If e.Column Is gvBOM.Columns(colUnitCode) Then

                Dim WhrCls As String = " tspl_item_master.Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "'  "
                Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_ITEM_UOM_DETAIL.UOM_Description as [UOM Description] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_ITEM_MASTER on tspl_item_master.item_code=tspl_item_uom_detail.item_code  "
                gvBOM.CurrentRow.Cells(colUnitCode).Value = clsCommon.ShowSelectForm("TSPL_UOM_HEAD", qry, "Code", WhrCls, gvBOM.CurrentRow.Cells(colUnitCode).Value, "", False)
                'lblUnitName.Text = fndUom.Value

            End If
            '' update fat and snf kg
            gvBOM.CurrentRow.Cells(colFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvBOM.CurrentRow.Cells(colItemCode).Value, gvBOM.CurrentRow.Cells(colUnitCode).Value, clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value), clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colFATPER).Value), Nothing)
            gvBOM.CurrentRow.Cells(colSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvBOM.CurrentRow.Cells(colItemCode).Value, gvBOM.CurrentRow.Cells(colUnitCode).Value, clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value), clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colSNFPER).Value), Nothing)

            If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                gvBOM.CurrentRow.Cells(colItemAmount).Value = clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colFATKG).Value) * clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colFATRate).Value) + clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colSNFKG).Value) * clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colSNFRate).Value)
            Else
                If CalculateItemCostonAvgForAssembly = True Then
                    If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly") = CompairStringResult.Equal Then
                        gvBOM.CurrentRow.Cells(colItemAmount).Value = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, gvBOM.CurrentRow.Cells(colItemCode).Value, gvBOM.CurrentRow.Cells(colLocationCode).Value, clsCommon.myCdbl(gvBOM.CurrentRow.Cells(colqty).Value) * clsItemMaster.GetConvertionFactor(gvBOM.CurrentRow.Cells(colItemCode).Value, gvBOM.CurrentRow.Cells(colUnitCode).Value, Nothing), dtp_AssembleDate.Value, dtp_AssembleDate.Value, False, Nothing)
                        gvBOM.CurrentRow.Cells(colItemAmount).ReadOnly = True
                        UpdateCost()
                    End If
                Else
                    gvBOM.CurrentRow.Cells(colItemAmount).ReadOnly = False
                End If
            End If

            If e.Column Is gvBOM.Columns(colSerialNo) Then
                Dim qry As String = "SELECT Auto_Sr_No as Code,Item_Code as [Item Code],Document_Code as [Document Code], " & _
                " Document_Date as [Document Date],Document_Type as [Document Type]  FROM TSPL_SERIAL_ITEM"

                Dim whrcls As String = " Item_Code='" & gvBOM.CurrentRow.Cells(colItemCode).Value & "' and Auto_Sr_No not in (select Serial_No from TSPL_PJC_ASSEMBLIES_ITEM_DETAIL where ASSEMBLY_CODE<>'" & txtCode.Value & "')"
                gvBOM.CurrentRow.Cells(colSerialNo).Value = clsCommon.ShowSelectForm("SerialNo", qry, "Code", whrcls, gvBOM.CurrentRow.Cells(colSerialNo).Value, "Code", False)

            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gvBOM_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvBOM.CellFormatting
        Try
            If e.Column Is gvBOM.Columns(colFATRate) Then
                gvBOM.CurrentRow.Cells(colFATRate).ReadOnly = True
                If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                    gvBOM.CurrentRow.Cells(colFATRate).ReadOnly = settPickCostFromItemMaster
                End If
            ElseIf e.Column Is gvBOM.Columns(colSNFRate) Then
                gvBOM.CurrentRow.Cells(colSNFRate).ReadOnly = True
                If clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal Then
                    gvBOM.CurrentRow.Cells(colSNFRate).ReadOnly = settPickCostFromItemMaster
                End If
            ElseIf e.Column Is gvBOM.Columns(colItemAmount) Then

                If CalculateItemCostonAvgForAssembly = True Then
                    If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly") = CompairStringResult.Equal Then
                        gvBOM.CurrentRow.Cells(colItemAmount).ReadOnly = True
                    Else
                        gvBOM.CurrentRow.Cells(colItemAmount).ReadOnly = (clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal)
                    End If
                Else
                    gvBOM.CurrentRow.Cells(colItemAmount).ReadOnly = (clsCommon.CompairString(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colProductType).Value), "MI") = CompairStringResult.Equal)
                End If
            End If
            'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            'cell.GradientStyle = GradientStyles.Solid
            'cell.BackColor = Color.FromArgb(243, 181, 51)
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub



    Private Sub gvBOM_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvBOM.CurrentColumnChanged
        If gvBOM.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBOM.CurrentRow.Index
            gvBOM.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If clsCommon.CompairString(Me.ddlDisassemblyType.SelectedValue, "Other", False) = CompairStringResult.Equal Then
                If intCurrRow = gvBOM.Rows.Count - 1 Then
                    gvBOM.Rows.AddNew()
                    gvBOM.CurrentRow = gvBOM.Rows(intCurrRow)
                End If
            End If

        End If
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Code", Me.Text)
        Else
            funPrint(txtCode.Value)
        End If
    End Sub
    Public Sub funPrint(ByVal StrCode As String)
        Try
            Dim qry As String = ""
            qry = " Select  '" & objCommonVar.CurrentCompanyName & "' as Company_Name , TSPL_PROD_ASSEMBLIES.CODE as AssemblyCode,Convert(varchar,TSPL_PROD_ASSEMBLIES.ASSEMBLY_DATE,103) as Assemblydate ,TSPL_PROD_ASSEMBLIES.Main_Item_Code,"
            qry += " TSPL_PROD_ASSEMBLIES.Serial_No as MainSerialNo,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.LINE_NO as SL_No,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE as ItemCode, "
            qry += " TSPL_ITEM_MASTER.Item_Desc as ItemDesc ,TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.Serial_No as SerialNo,TSPL_PROD_ASSEMBLIES.Modified_By as ModifiedBy,TSPL_PROD_ASSEMBLIES.Created_By as CreatedBy from TSPL_PROD_ASSEMBLIES_ITEM_DETAIL  Left Outer Join TSPL_PROD_ASSEMBLIES on TSPL_PROD_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE"
            qry += " where 2=2"

            If StrCode <> "" Then
                qry += " and  TSPL_PROD_ASSEMBLIES.CODE='" & StrCode & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "rptAssembliesDeassembliesReport", "Assembly Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub CalculateFatandSNF()

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
            clsAssembliesDis.CancelData(txtCode.Value)
            clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub txtQuantity_LostFocus(sender As Object, e As EventArgs) Handles txtQuantity.LostFocus
        If clsCommon.CompairString(Me.cboTransactionType.SelectedValue, "Assembly") = CompairStringResult.Equal Then
            UpdateQty(True)
        Else
            UpdateQty(False)
        End If


    End Sub
    ''richa BHA/24/08/18-000481
    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Select document for unpost.")
            End If

            Dim qry As String = "select count(*) from TSPL_PROD_ASSEMBLIES where Posted='0' and CODE='" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                Throw New Exception("Current document is not posted.")
            End If

            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
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


                If clsAssembliesDis.UnpostData(txtCode.Value, IIf(clsCommon.myLen(cboTransactionType.SelectedValue) > 0, "Disassembly", "Assembly")) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Unpost and Recreated", Me.Text)
                        btnunpost.Visible = False
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "CODE", "TSPL_PROD_ASSEMBLIES")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gvBOM_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvBOM.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvBOM.Columns(colqty) Then
                    If clsCommon.CompairString(cboTransactionType.Text, "DisAssembly") = CompairStringResult.Equal Then
                        OpenBatchItemGridLevel()
                    Else
                        '    OpenBatchItem()
                    End If
                End If

                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
