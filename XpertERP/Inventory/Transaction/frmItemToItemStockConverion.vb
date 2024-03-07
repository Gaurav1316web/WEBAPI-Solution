Imports common
Imports System.Data.SqlClient

Public Class frmItemToItemStockConverion
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isInSideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False
    '' from grid columns
    Public Const colFrom_SLNo As String = "colSLNo"
    Public Const colFrom_Item_Code As String = "colFrom_Item_Code"
    Public Const colFrom_Item_Desc As String = "colFrom_Item_Desc"

    Public Const colFrom_Stock_Unit As String = "colFrom_Stock_Unit"
    Public Const colFrom_Stocking_Unit As String = "colFrom_Stocking_Unit"

    Public Const colFrom_Stock_In_Hand As String = "colFrom_Stock_In_Hand"
    Public Const colFrom_UOM_Code As String = "colFrom_UOM_Code"
    Public Const colFrom_UOM_DESC As String = "colFrom_UOM_DESC"
    Public Const colFrom_Conversion_factor As String = "colFrom_Conversion_factor"
    Public Const colFrom_Trans_Qty As String = "colFromTrans_Qty"
    Public Const colFrom_Trans_Stock_Qty As String = "colFrom_Trans_Stock_Qty"
    Public Const colFrom_Remarks As String = "colFrom_Remarks"

    '' to grid columns

    Public Const colTo_SLNo As String = "colTo_SLNo"
    Public Const colTo_Item_Code As String = "colTo_Item_Code"
    Public Const colTo_Item_Desc As String = "colTo_Item_Desc"

    Public Const colTo_Stocking_Unit As String = "colTo_Stocking_Unit"
    Public Const colTo_Stock_Unit As String = "colTo_Stock_Unit"

    Public Const colTo_UOM_Code As String = "colTo_UOM_Code"
    Public Const colTo_UOM_DESC As String = "colTo_UOM_DESC"
    Public Const colTo_Conversion_factor As String = "colTo_Conversion_factor"

    Public Const colTo_Stock_In_Hand As String = "colTo_Stock_In_Hand"
    Public Const colTo_Trans_Qty As String = "colToTrans_Qty"
    Public Const colTo_Trans_Stock_Qty As String = "colTo_Trans_Stock_Qty"
    Public Const colTo_Remarks As String = "colTo_Remarks"

    'Public Const colUOMCode As String = "colUOMCode"
    'Public Const colUomDesc As String = "colUomDesc"
    'Public Const colConveriosnFactor As String = "colConveriosnFactor"
    'Public Const colStockingUnit As String = "colStockingUnit"
    'Public Const colStockInHand As String = "colStockInHand"
    'Public Const colRequiredUomCode As String = "colRequiredUomCode"
    'Public Const colRequiredUomDesc As String = "colRequiredUomDesc"
    'Public Const colInputFromMRP As String = "colInputFromMRP"
    'Public Const colRequiredQty As String = "colRequiredQty"
    'Public Const colInputFromUnit As String = "colInputFromUnit"
    'Public Const colOutputToUnit As String = "colOutputToUnit"
    'Public Const colOutputStkUnt As String = "colOutputStkUnt"
    'Public Const colOutputToMRP As String = "colOutputToMRP"
    'Public Const colOutputStockMRP As String = "colOutputStockMRP"
    'Public Const colRemarks As String = "colRemarks"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    'Sub loadItemData(ByVal strItemCode As String)
    '    Try
    '        loadBlankGrid()
    '        Dim qry As String = " select Item_Code,UOM_Code as UOM,max(UOM_Description) as UOM_Description,max(Conversion_Factor) as conversion_factor,max(Stocking_Unit) as Stocking_Unit, sum(qty) as qty  from (select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0) as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code ='" & fndLocation.Value & "' and TSPL_INVENTORY_MOVEMENT.InOut='I' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code='" & strItemCode & "' union all select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0)*-1 as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code ='" & fndLocation.Value & "' and TSPL_INVENTORY_MOVEMENT.InOut='O' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code='" & strItemCode & "') ttt group by Item_Code,UOM_Code  "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            'Item_Code	UOM	Qty	UOM_Description	Stocking_Unit	Conversion_Factor
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                gvFromItem.Rows.AddNew()
    '                gvFromItem.Rows(i).Cells(colSLNo).Value = (i + 1)
    '                gvFromItem.Rows(i).Cells(colUOMCode).Value = clsCommon.myCstr(dt.Rows(i)("UOM"))
    '                gvFromItem.Rows(i).Cells(colUomDesc).Value = clsCommon.myCstr(dt.Rows(i)("UOM_Description"))
    '                gvFromItem.Rows(i).Cells(colConveriosnFactor).Value = clsCommon.myCdbl(dt.Rows(i)("Conversion_Factor"))
    '                gvFromItem.Rows(i).Cells(colStockingUnit).Value = clsCommon.myCstr(dt.Rows(i)("Stocking_Unit"))
    '                gvFromItem.Rows(i).Cells(colStockInHand).Value = clsCommon.myCdbl(dt.Rows(i)("Qty"))
    '                gvFromItem.Rows(i).Cells(colRequiredUomCode).Value = ""
    '                gvFromItem.Rows(i).Cells(colRequiredUomDesc).Value = ""
    '                gvFromItem.Rows(i).Cells(colRequiredQty).Value = 0
    '                gvFromItem.Rows(i).Cells(colRemarks).Value = ""
    '                gvFromItem.Rows(i).Cells(colInputFromUnit).Value = 0
    '                gvFromItem.Rows(i).Cells(colOutputToUnit).Value = 0
    '                gvFromItem.Rows(i).Cells(colOutputStkUnt).Value = 0
    '            Next
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            Dim obj As clsItemToItemStockConversion = New clsItemToItemStockConversion()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
            If obj.isNewEntry Then
                obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy"), clsDocType.ItemStockConversion, "", obj.Location_Code)
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Error in Doc  No genertion", Me.Text)
                    Exit Sub
                End If
            Else
                obj.Doc_No = clsCommon.myCstr(fndDocNo.Value)
            End If
            fndDocNo.Value = obj.Doc_No
            obj.Doc_Date = clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy")
            obj.Structure_Code = clsCommon.myCstr(fndItemStructure.Value)
            'obj.Item_Desc = clsCommon.myCstr(txtStructname.Text)
            obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Location_Desc = clsCommon.myCstr(lblLocationDesc.Text)
            'Dim i As Integer = 0
            '' from items grid
            Dim objDetail As New clsItemToItemStockConveriosnFromDetail
            obj.arrFromDetail = New List(Of clsItemToItemStockConveriosnFromDetail)
            For Each grow As GridViewRowInfo In gvFromItem.Rows
                If clsCommon.myLen(grow.Cells(colFrom_Item_Code).Value) <= 0 Then
                    Continue For
                End If

                objDetail = New clsItemToItemStockConveriosnFromDetail
                objDetail.Doc_No = clsCommon.myCstr(obj.Doc_No)
                objDetail.Line_No = clsCommon.myCdbl(grow.Cells(colFrom_SLNo).Value)
                objDetail.Item_Code = clsCommon.myCstr(grow.Cells(colFrom_Item_Code).Value)
                objDetail.Item_Desc = clsCommon.myCstr(grow.Cells(colFrom_Item_Desc).Value)
                objDetail.UOM_Code = clsCommon.myCstr(grow.Cells(colFrom_UOM_Code).Value)
                objDetail.UOM_DESC = clsCommon.myCstr(grow.Cells(colFrom_UOM_DESC).Value)
                objDetail.Conversion_factor = clsCommon.myCdbl(grow.Cells(colFrom_Conversion_factor).Value)
                objDetail.Stocking_Unit = clsCommon.myCstr(grow.Cells(colFrom_Stocking_Unit).Value)
                objDetail.Stock_Unit = clsCommon.myCstr(grow.Cells(colFrom_Stock_Unit).Value)
                objDetail.Stock_In_Hand = clsCommon.myCdbl(grow.Cells(colFrom_Stock_In_Hand).Value)
                objDetail.Trans_Qty = clsCommon.myCdbl(grow.Cells(colFrom_Trans_Qty).Value)
                objDetail.Trans_Stock_Qty = clsCommon.myCdbl(grow.Cells(colFrom_Trans_Stock_Qty).Value)
                objDetail.Remarks = clsCommon.myCstr(grow.Cells(colFrom_Remarks).Value)

                obj.arrFromDetail.Add(objDetail)
            Next

            '' To items grid
            Dim objToDetail As New clsItemToItemStockConveriosnToDetail
            obj.arrToDetail = New List(Of clsItemToItemStockConveriosnToDetail)
            For Each grow As GridViewRowInfo In gvToItems.Rows
                If clsCommon.myLen(grow.Cells(colTo_Item_Code).Value) <= 0 Then
                    Continue For
                End If
                objToDetail = New clsItemToItemStockConveriosnToDetail
                objToDetail.Doc_No = clsCommon.myCstr(obj.Doc_No)
                objToDetail.Line_No = clsCommon.myCdbl(grow.Cells(colTo_SLNo).Value)
                objToDetail.Item_Code = clsCommon.myCstr(grow.Cells(colTo_Item_Code).Value)
                objToDetail.Item_Desc = clsCommon.myCstr(grow.Cells(colTo_Item_Desc).Value)
                objToDetail.UOM_Code = clsCommon.myCstr(grow.Cells(colTo_UOM_Code).Value)
                objToDetail.UOM_DESC = clsCommon.myCstr(grow.Cells(colTo_UOM_DESC).Value)
                objToDetail.Conversion_factor = clsCommon.myCdbl(grow.Cells(colTo_Conversion_factor).Value)
                objToDetail.Stocking_Unit = clsCommon.myCstr(grow.Cells(colTo_Stocking_Unit).Value)
                objToDetail.Stock_Unit = clsCommon.myCstr(grow.Cells(colTo_Stock_Unit).Value)
                objToDetail.Stock_In_Hand = clsCommon.myCdbl(grow.Cells(colTo_Stock_In_Hand).Value)
                objToDetail.Trans_Qty = clsCommon.myCdbl(grow.Cells(colTo_Trans_Qty).Value)
                objToDetail.Trans_Stock_Qty = clsCommon.myCdbl(grow.Cells(colTo_Trans_Stock_Qty).Value)
                objToDetail.Remarks = clsCommon.myCstr(grow.Cells(colTo_Remarks).Value)

                obj.arrToDetail.Add(objToDetail)
            Next

            If Not isPost Then
                obj.IsPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If

            If clsItemToItemStockConversion.SaveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                LoadData(obj.Doc_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            fndDocNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub postData()
        Try

            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsItemToItemStockConversion.PostData(Me.Form_ID, fndDocNo.Value)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocNo As String, ByVal nav As NavigatorType)
        isInSideLoadData = True
        Dim obj As clsItemToItemStockConversion = clsItemToItemStockConversion.getData(strDocNo, nav)
        If obj IsNot Nothing Then
            fndDocNo.Value = obj.Doc_No
            dtpDocDate.Value = obj.Doc_Date
            fndItemStructure.Value = obj.Structure_Code

            'chkMRP.Checked = clsItemMaster.IsMRPItem(obj.Item_Code)
            txtStructname.Text = obj.Structure_Desc
            fndLocation.Value = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            '' fill from item grid
            loadBlankFromGrid()
            If obj.arrFromDetail IsNot Nothing Then
                For Each objTr As clsItemToItemStockConveriosnFromDetail In obj.arrFromDetail
                    Try

                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_SLNo).Value = objTr.Line_No
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Item_Code).Value = objTr.Item_Code
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Item_Desc).Value = objTr.Item_Desc
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_UOM_Code).Value = clsCommon.myCstr(objTr.UOM_Code)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_UOM_DESC).Value = clsCommon.myCstr(objTr.UOM_DESC)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Conversion_factor).Value = clsCommon.myCdbl(objTr.Conversion_factor)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Stocking_Unit).Value = clsCommon.myCstr(objTr.Stocking_Unit)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Stock_Unit).Value = clsCommon.myCstr(objTr.Stock_Unit)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Stock_In_Hand).Value = clsCommon.myCdbl(objTr.Stock_In_Hand)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Trans_Qty).Value = clsCommon.myCdbl(objTr.Trans_Qty)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Trans_Stock_Qty).Value = clsCommon.myCdbl(objTr.Trans_Stock_Qty)
                        gvFromItem.Rows(gvFromItem.Rows.Count - 1).Cells(colFrom_Remarks).Value = clsCommon.myCstr(objTr.Remarks)
                        gvFromItem.Rows.AddNew()
                    Catch exxx As Exception
                    End Try
                Next
            End If
            '' fill to item grid
            loadBlankToGrid()
            If obj.arrToDetail IsNot Nothing Then
                For Each objTr As clsItemToItemStockConveriosnToDetail In obj.arrToDetail
                    Try

                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_SLNo).Value = objTr.Line_No
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Item_Code).Value = objTr.Item_Code
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Item_Desc).Value = objTr.Item_Desc
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_UOM_Code).Value = clsCommon.myCstr(objTr.UOM_Code)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_UOM_DESC).Value = clsCommon.myCstr(objTr.UOM_DESC)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Conversion_factor).Value = clsCommon.myCdbl(objTr.Conversion_factor)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Stocking_Unit).Value = clsCommon.myCstr(objTr.Stocking_Unit)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Stock_Unit).Value = clsCommon.myCstr(objTr.Stock_Unit)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Stock_In_Hand).Value = clsCommon.myCdbl(objTr.Stock_In_Hand)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Trans_Qty).Value = clsCommon.myCdbl(objTr.Trans_Qty)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Trans_Stock_Qty).Value = clsCommon.myCdbl(objTr.Trans_Stock_Qty)
                        gvToItems.Rows(gvToItems.Rows.Count - 1).Cells(colTo_Remarks).Value = clsCommon.myCstr(objTr.Remarks)
                        gvToItems.Rows.AddNew()
                    Catch exxx As Exception
                    End Try
                Next
            End If
            If obj.IsPosted = 1 Then
                lblPending.Status = ERPTransactionStatus.Approved
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
            Else
                lblPending.Status = ERPTransactionStatus.Pending
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
            End If
            btnSave.Text = "Update"
        Else
            reset()
        End If
        isInSideLoadData = False
    End Sub

    Sub reset()
        loadBlankFromGrid()
        loadBlankToGrid()
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        dtpDocDate.Value = dt
        fndItemStructure.Value = ""
        txtStructname.Text = ""
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnReverse.Visible = False
        chkMRP.Checked = False
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemStockConversion)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub loadBlankFromGrid()
        gvFromItem.Rows.Clear()
        gvFromItem.Columns.Clear()
        gvFromItem.DataSource = Nothing
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_SLNo
        repoTextColumn.Width = 60
        repoTextColumn.HeaderText = "SL.No"
        repoTextColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_Item_Code
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Item Code"
        repoTextColumn.ReadOnly = False
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_Item_Desc
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "Item Desc"
        repoTextColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_Stock_Unit
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Stock UOM Code"
        repoTextColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

       

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFrom_Stock_In_Hand
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Stock In Hand"
        repoDecimalColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_UOM_Code
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "UOM Code"
        repoTextColumn.ReadOnly = False
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_UOM_DESC
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "UOM Desc"
        repoTextColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_Stocking_Unit
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Stocking Unit"
        repoTextColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFrom_Conversion_factor
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Conversion Factor"
        repoDecimalColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFrom_Trans_Qty
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Quantity"
        repoDecimalColumn.ReadOnly = False
        gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colFrom_Trans_Stock_Qty
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Stock Quantity"
        repoDecimalColumn.ReadOnly = True
        gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colInputFromUnit
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Input From Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colInputFromMRP
        'repoDecimalColumn.Width = 130
        'repoDecimalColumn.HeaderText = "Required MRP"
        'repoDecimalColumn.ReadOnly = False
        'repoDecimalColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoDecimalColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputToUnit
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Output To Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputStkUnt
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Output Stocking Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colStockInHand
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Stock In Hand"
        'repoDecimalColumn.ReadOnly = True
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)


        'repoTextColumn = New GridViewTextBoxColumn()
        'repoTextColumn.Name = colRequiredUomCode
        'repoTextColumn.Width = 130
        'repoTextColumn.HeaderText = "Required UOM Code"
        'repoTextColumn.ReadOnly = False
        'repoTextColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoTextColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        'gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        'repoTextColumn = New GridViewTextBoxColumn()
        'repoTextColumn.Name = colRequiredUomDesc
        'repoTextColumn.Width = 150
        'repoTextColumn.HeaderText = "Required UOM Desc"
        'repoTextColumn.ReadOnly = True
        'gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colRequiredQty
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Required Qty"
        'repoDecimalColumn.ReadOnly = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputToMRP
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Out MRP"
        'repoDecimalColumn.ReadOnly = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputStockMRP
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Out Stock MRP"
        'repoDecimalColumn.ReadOnly = False
        'gvFromItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colFrom_Remarks
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Remarks"
        repoTextColumn.ReadOnly = False
        gvFromItem.MasterTemplate.Columns.Add(repoTextColumn)

        gvFromItem.AllowAddNewRow = False
        gvFromItem.AllowDeleteRow = False
        gvFromItem.AllowRowReorder = False
        gvFromItem.ShowGroupPanel = False
        gvFromItem.EnableFiltering = False
        gvFromItem.EnableSorting = False
        gvFromItem.EnableGrouping = False
        gvFromItem.AddNewRowPosition = SystemRowPosition.Bottom
        gvFromItem.Rows.AddNew()
    End Sub
    Sub loadBlankToGrid()
        gvToItems.Rows.Clear()
        gvToItems.Columns.Clear()
        gvToItems.DataSource = Nothing
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_SLNo
        repoTextColumn.Width = 60
        repoTextColumn.HeaderText = "SL.No"
        repoTextColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_Item_Code
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Item Code"
        repoTextColumn.ReadOnly = False
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_Item_Desc
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "Item Desc"
        repoTextColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_Stock_Unit
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Stock UOM Code"
        repoTextColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colTo_Stock_In_Hand
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Stock In Hand"
        repoDecimalColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_UOM_Code
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "UOM Code"
        repoTextColumn.ReadOnly = False
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)



        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_UOM_DESC
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "UOM Desc"
        repoTextColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_Stocking_Unit
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Stocking Unit"
        repoTextColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colTo_Conversion_factor
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Conversion Factor"
        repoDecimalColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colTo_Trans_Qty
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Quantity"
        repoDecimalColumn.ReadOnly = False
        gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colTo_Trans_Stock_Qty
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Stock Quantity"
        repoDecimalColumn.ReadOnly = True
        gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colInputFromUnit
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Input From Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colInputFromMRP
        'repoDecimalColumn.Width = 130
        'repoDecimalColumn.HeaderText = "Required MRP"
        'repoDecimalColumn.ReadOnly = False
        'repoDecimalColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoDecimalColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputToUnit
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Output To Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputStkUnt
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Output Stocking Unit"
        'repoDecimalColumn.ReadOnly = True
        'repoDecimalColumn.IsVisible = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colStockInHand
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Stock In Hand"
        'repoDecimalColumn.ReadOnly = True
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)


        'repoTextColumn = New GridViewTextBoxColumn()
        'repoTextColumn.Name = colRequiredUomCode
        'repoTextColumn.Width = 130
        'repoTextColumn.HeaderText = "Required UOM Code"
        'repoTextColumn.ReadOnly = False
        'repoTextColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoTextColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        'gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        'repoTextColumn = New GridViewTextBoxColumn()
        'repoTextColumn.Name = colRequiredUomDesc
        'repoTextColumn.Width = 150
        'repoTextColumn.HeaderText = "Required UOM Desc"
        'repoTextColumn.ReadOnly = True
        'gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colRequiredQty
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Required Qty"
        'repoDecimalColumn.ReadOnly = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputToMRP
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Out MRP"
        'repoDecimalColumn.ReadOnly = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        'repoDecimalColumn = New GridViewDecimalColumn()
        'repoDecimalColumn.Name = colOutputStockMRP
        'repoDecimalColumn.FormatString = "{0:n2}"
        'repoDecimalColumn.Width = 100
        'repoDecimalColumn.HeaderText = "Out Stock MRP"
        'repoDecimalColumn.ReadOnly = False
        'gvToItems.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colTo_Remarks
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Remarks"
        repoTextColumn.ReadOnly = False
        gvToItems.MasterTemplate.Columns.Add(repoTextColumn)

        gvToItems.AllowAddNewRow = False
        gvToItems.AllowDeleteRow = False
        gvToItems.AllowRowReorder = False
        gvToItems.ShowGroupPanel = False
        gvToItems.EnableFiltering = False
        gvToItems.EnableSorting = False
        gvToItems.EnableGrouping = False
        gvToItems.AddNewRowPosition = SystemRowPosition.Bottom
        gvToItems.Rows.AddNew()
    End Sub

    Private Sub frmItemToItemStockConverion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub



    Private Sub FrmMilkTransferIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndItemStructure.Value) <= 0 Then
                Throw New Exception("Please select Item")
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(gvFromItem.Rows.Count) <= 0 Then
                Throw New Exception("Please enter from Items")
            End If
            If clsCommon.myLen(gvToItems.Rows.Count) <= 0 Then
                Throw New Exception("Please enter To Items")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Function
    'Function getRemarks() As String
    '    Dim str As String = String.Empty

    'End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData(False)
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsItemToItemStockConversion.ReverseAndUnpost(fndDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Doc No To delete ", Me.Text)
        Else
            If myMessages.deleteConfirm() Then
                If clsItemToItemStockConversion.deleteData(fndDocNo.Value, Nothing) Then
                    reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub


    Private Sub fndItemStructure__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            fndItemStructure.Value = clsItemMaster.getFinder("", fndItemStructure.Value, isButtonClicked)
            txtStructname.Text = clsCommon.myCstr(clsItemMaster.GetItemName(fndItemStructure.Value, Nothing))
            chkMRP.Checked = clsItemMaster.IsMRPItem(fndItemStructure.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            fndLocation.Value = clsLocation.getFinder("", fndLocation.Value, isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsLocation.GetName(fndLocation.Value, Nothing))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MyNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndDocNo.Value = clsItemToItemStockConversion.getFinder("", fndDocNo.Value, isButtonClicked)
        LoadData(fndDocNo.Value, NavigatorType.Current)
    End Sub


    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(fndItemStructure.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select The Item", Me.Text)
            fndItemStructure.Focus()
            Exit Sub
        End If

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select The Location", Me.Text)
            fndLocation.Focus()
            Exit Sub
        End If
        'loadItemData(fndItemStructure.Value)
    End Sub

    Private Sub gvFromItem_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvFromItem.CellFormatting
        'Try
        '    If e.Column Is gvFromItem.Columns(colInputFromMRP) Then
        '        gvFromItem.CurrentRow.Cells(colInputFromMRP).ReadOnly = Not chkMRP.Checked
        '    ElseIf e.Column Is gvFromItem.Columns(colOutputToMRP) Then
        '        gvFromItem.CurrentRow.Cells(colOutputToMRP).ReadOnly = Not chkMRP.Checked
        '    ElseIf e.Column Is gvFromItem.Columns(colOutputStockMRP) Then
        '        gvFromItem.CurrentRow.Cells(colOutputStockMRP).ReadOnly = Not chkMRP.Checked
        '    End If
        'Catch ex As Exception
        '    'common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub gvFromItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvFromItem.CellValueChanged
        Try

            If Not isInSideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvFromItem.Columns(colFrom_Item_Code) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value = clsItemMaster.getFinder("Structure_Code='" & fndItemStructure.Value & "'", gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, False)
                        Dim obj As clsItemMaster = clsItemMaster.GetDataRMOther(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, NavigatorType.Current)
                        If Not obj Is Nothing Then
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Desc).Value = obj.Item_Desc
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value = obj.Unit_Code
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_Stock_Unit).Value = obj.Unit_Code
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_DESC).Value = clsUOMInfo.GetUnitDesc(gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value, Nothing)
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_Conversion_factor).Value = clsItemMaster.GetConvertionFactor(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value, Nothing)
                            Dim Stock_Unit As String = clsItemMaster.GetStockUnit(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, Nothing)
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_Stocking_Unit).Value = IIf(clsCommon.CompairString(Stock_Unit, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value) = CompairStringResult.Equal, "Y", "N")
                            gvFromItem.Rows(e.RowIndex).Cells(colFrom_Stock_In_Hand).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, fndLocation.Value, fndDocNo.Value, dtpDocDate.Value, Nothing, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value)
                        End If
                    End If
                    If e.Column Is gvFromItem.Columns(colFrom_UOM_Code) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value = clsItemMaster.FinderForuom(gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value, gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, False)
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_DESC).Value = clsUOMInfo.GetUnitDesc(gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value, Nothing)
                        Dim Stock_Unit As String = clsItemMaster.GetStockUnit(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, Nothing)
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_Stocking_Unit).Value = IIf(clsCommon.CompairString(Stock_Unit, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value) = CompairStringResult.Equal, "Y", "N")
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_Conversion_factor).Value = clsItemMaster.GetConvertionFactor(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value, Nothing)
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_Stock_In_Hand).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Item_Code).Value, fndLocation.Value, fndDocNo.Value, dtpDocDate.Value, Nothing, gvFromItem.Rows(e.RowIndex).Cells(colFrom_UOM_Code).Value)
                    End If
                    If e.Column Is gvFromItem.Columns(colFrom_Trans_Qty) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvFromItem.Rows(e.RowIndex).Cells(colFrom_Trans_Stock_Qty).Value = gvFromItem.Rows(e.RowIndex).Cells(colFrom_Trans_Qty).Value / IIf(gvFromItem.Rows(e.RowIndex).Cells(colFrom_Conversion_factor).Value = 0, 1, gvFromItem.Rows(e.RowIndex).Cells(colFrom_Conversion_factor).Value)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub txtcategorycode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndItemStructure._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER "
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("CATFND1", qry)
            If dr IsNot Nothing Then
                fndItemStructure.Value = clsCommon.myCstr(dr("code"))
                txtStructname.Text = clsCommon.myCstr(dr("Description"))
            Else
                fndItemStructure.Value = ""
                txtStructname.Text = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gvToItems_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvToItems.CellValueChanged
        Try

            If Not isInSideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvToItems.Columns(colTo_Item_Code) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value = clsItemMaster.getFinder("Structure_Code='" & fndItemStructure.Value & "'", gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, False)
                        Dim obj As clsItemMaster = clsItemMaster.GetDataRMOther(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, NavigatorType.Current)
                        If Not obj Is Nothing Then
                            gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Desc).Value = obj.Item_Desc
                            gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value = obj.Unit_Code
                            gvToItems.Rows(e.RowIndex).Cells(colTo_Stock_Unit).Value = obj.Unit_Code
                            gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_DESC).Value = clsUOMInfo.GetUnitDesc(gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value, Nothing)
                            gvToItems.Rows(e.RowIndex).Cells(colTo_Conversion_factor).Value = clsItemMaster.GetConvertionFactor(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value, Nothing)
                            'gvToItems.Rows(e.RowIndex).Cells(colTo_Stocking_Unit).Value = obj.Unit_Code
                            gvToItems.Rows(e.RowIndex).Cells(colTo_Stock_In_Hand).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, fndLocation.Value, fndDocNo.Value, dtpDocDate.Value, Nothing, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value)
                            Dim Stock_Unit As String = clsItemMaster.GetStockUnit(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, Nothing)
                            gvToItems.Rows(e.RowIndex).Cells(colTo_Stocking_Unit).Value = IIf(clsCommon.CompairString(Stock_Unit, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value) = CompairStringResult.Equal, "Y", "N")
                        End If
                    End If
                    If e.Column Is gvToItems.Columns(colTo_UOM_Code) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value = clsItemMaster.FinderForuom(gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value, gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, False)
                        gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_DESC).Value = clsUOMInfo.GetUnitDesc(gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value, Nothing)
                        gvToItems.Rows(e.RowIndex).Cells(colTo_Conversion_factor).Value = clsItemMaster.GetConvertionFactor(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value, Nothing)
                        gvToItems.Rows(e.RowIndex).Cells(colTo_Stock_In_Hand).Value = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, fndLocation.Value, fndDocNo.Value, dtpDocDate.Value, Nothing, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value)
                        Dim Stock_Unit As String = clsItemMaster.GetStockUnit(gvToItems.Rows(e.RowIndex).Cells(colTo_Item_Code).Value, Nothing)
                        gvToItems.Rows(e.RowIndex).Cells(colTo_Stocking_Unit).Value = IIf(clsCommon.CompairString(Stock_Unit, gvToItems.Rows(e.RowIndex).Cells(colTo_UOM_Code).Value) = CompairStringResult.Equal, "Y", "N")
                    End If
                    If e.Column Is gvToItems.Columns(colTo_Trans_Qty) AndAlso clsCommon.myLen(fndItemStructure.Value) > 0 AndAlso clsCommon.myLen(fndLocation.Value) > 0 Then
                        gvToItems.Rows(e.RowIndex).Cells(colTo_Trans_Stock_Qty).Value = gvToItems.Rows(e.RowIndex).Cells(colTo_Trans_Qty).Value / IIf(gvToItems.Rows(e.RowIndex).Cells(colTo_Conversion_factor).Value = 0, 1, gvToItems.Rows(e.RowIndex).Cells(colTo_Conversion_factor).Value)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvFromItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvFromItem.CurrentColumnChanged
        If gvFromItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvFromItem.CurrentRow.Index
            gvFromItem.CurrentRow.Cells(colFrom_SLNo).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvFromItem.Rows.Count - 1 Then
                gvFromItem.Rows.AddNew()
                gvFromItem.CurrentRow = gvFromItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvToItems_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvToItems.CurrentColumnChanged
        If gvToItems.RowCount > 0 Then
            Dim intCurrRow As Integer = gvToItems.CurrentRow.Index
            gvToItems.CurrentRow.Cells(colTo_SLNo).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gvToItems.Rows.Count - 1 Then
                gvToItems.Rows.AddNew()
                gvToItems.CurrentRow = gvToItems.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
