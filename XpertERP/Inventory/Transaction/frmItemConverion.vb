Imports common
Imports System.Data.SqlClient

Public Class FrmItemConverion
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isInSideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False
    Public Const colSLNo As String = "colSLNo"
    Public Const colUOMCode As String = "colUOMCode"
    Public Const colUomDesc As String = "colUomDesc"
    Public Const colConveriosnFactor As String = "colConveriosnFactor"
    Public Const colStockingUnit As String = "colStockingUnit"
    Public Const colStockInHand As String = "colStockInHand"
    Public Const colRequiredUomCode As String = "colRequiredUomCode"
    Public Const colRequiredUomDesc As String = "colRequiredUomDesc"
    Public Const colInputFromMRP As String = "colInputFromMRP"
    Public Const colRequiredQty As String = "colRequiredQty"
    Public Const colInputFromUnit As String = "colInputFromUnit"
    Public Const colOutputToUnit As String = "colOutputToUnit"
    Public Const colOutputStkUnt As String = "colOutputStkUnt"
    Public Const colOutputToMRP As String = "colOutputToMRP"
    Public Const colOutputStockMRP As String = "colOutputStockMRP"
    Public Const colRemarks As String = "colRemarks"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Sub loadItemData(ByVal strItemCode As String)
        Try
            loadBlankGrid()
            Dim qry As String = " select Item_Code,UOM_Code as UOM,max(UOM_Description) as UOM_Description,max(Conversion_Factor) as conversion_factor,max(Stocking_Unit) as Stocking_Unit, sum(qty) as qty  from (select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0) as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code ='" & fndLocation.Value & "' and TSPL_INVENTORY_MOVEMENT.InOut='I' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code='" & strItemCode & "' union all select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0)*-1 as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code ='" & fndLocation.Value & "' and TSPL_INVENTORY_MOVEMENT.InOut='O' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code='" & strItemCode & "') ttt group by Item_Code,UOM_Code  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'Item_Code	UOM	Qty	UOM_Description	Stocking_Unit	Conversion_Factor
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItem.Rows.AddNew()
                    gvItem.Rows(i).Cells(colSLNo).Value = (i + 1)
                    gvItem.Rows(i).Cells(colUOMCode).Value = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    gvItem.Rows(i).Cells(colUomDesc).Value = clsCommon.myCstr(dt.Rows(i)("UOM_Description"))
                    gvItem.Rows(i).Cells(colConveriosnFactor).Value = clsCommon.myCdbl(dt.Rows(i)("Conversion_Factor"))
                    gvItem.Rows(i).Cells(colStockingUnit).Value = clsCommon.myCstr(dt.Rows(i)("Stocking_Unit"))
                    gvItem.Rows(i).Cells(colStockInHand).Value = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    gvItem.Rows(i).Cells(colRequiredUomCode).Value = ""
                    gvItem.Rows(i).Cells(colRequiredUomDesc).Value = ""
                    gvItem.Rows(i).Cells(colRequiredQty).Value = 0
                    gvItem.Rows(i).Cells(colRemarks).Value = ""
                    gvItem.Rows(i).Cells(colInputFromUnit).Value = 0
                    gvItem.Rows(i).Cells(colOutputToUnit).Value = 0
                    gvItem.Rows(i).Cells(colOutputStkUnt).Value = 0
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            Dim obj As clsItemStockConveriosnHead = New clsItemStockConveriosnHead()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
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
            obj.Item_Code = clsCommon.myCstr(fndItem.Value)
            obj.Item_Desc = clsCommon.myCstr(lblItemDesc.Text)
            obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Location_Desc = clsCommon.myCstr(lblLocationDesc.Text)
            Dim i As Integer = 0
            Dim objDetail As New clsItemStockConveriosnDetail
            obj.arrDetail = New List(Of clsItemStockConveriosnDetail)
            For i = 0 To gvItem.Rows.Count - 1
                objDetail = New clsItemStockConveriosnDetail
                objDetail.Doc_No = clsCommon.myCstr(obj.Doc_No)
                objDetail.UOM_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(colUOMCode).Value)
                objDetail.UOM_DESC = clsCommon.myCstr(gvItem.Rows(i).Cells(colUomDesc).Value)
                objDetail.Conversion_factor = clsCommon.myCdbl(gvItem.Rows(i).Cells(colConveriosnFactor).Value)
                objDetail.Stocking_Unit = clsCommon.myCstr(gvItem.Rows(i).Cells(colStockingUnit).Value)
                objDetail.Stock_In_Hand = clsCommon.myCdbl(gvItem.Rows(i).Cells(colStockInHand).Value)
                objDetail.Required_UOM_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(colRequiredUomCode).Value)
                objDetail.Required_UOM_Desc = clsCommon.myCstr(gvItem.Rows(i).Cells(colRequiredUomDesc).Value)
                objDetail.Required_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colRequiredQty).Value)

                objDetail.Input_From_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colInputFromUnit).Value)
                objDetail.Output_To_qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colOutputToUnit).Value)
                objDetail.Output_Stock_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colOutputStkUnt).Value)
                objDetail.Remarks = clsCommon.myCstr(gvItem.Rows(i).Cells(colRemarks).Value)

                objDetail.Input_From_MRP = clsCommon.myCdbl(gvItem.Rows(i).Cells(colInputFromMRP).Value)
                objDetail.Output_To_MRP = clsCommon.myCdbl(gvItem.Rows(i).Cells(colOutputToMRP).Value)
                objDetail.Output_Stock_MRP = clsCommon.myCdbl(gvItem.Rows(i).Cells(colOutputStockMRP).Value)


                obj.arrDetail.Add(objDetail)
            Next

            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If

            If clsItemStockConveriosnHead.SaveData(obj, trans) Then
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
                If (clsItemStockConveriosnHead.PostData(Me.Form_ID, fndDocNo.Value)) Then
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
        Dim obj As clsItemStockConveriosnHead = clsItemStockConveriosnHead.getData(strDocNo, nav)
        If obj IsNot Nothing Then
            fndDocNo.Value = obj.Doc_No
            dtpDocDate.Value = obj.Doc_Date
            fndItem.Value = obj.Item_Code
            chkMRP.Checked = clsItemMaster.IsMRPItem(obj.Item_Code)
            lblItemDesc.Text = obj.Item_Desc
            fndLocation.Value = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            loadBlankGrid()
            If obj.arrDetail IsNot Nothing Then
                For i As Integer = 0 To obj.arrDetail.Count - 1
                    Try
                        gvItem.Rows.AddNew()
                        gvItem.Rows(i).Cells(colSLNo).Value = (i + 1)
                        gvItem.Rows(i).Cells(colUOMCode).Value = clsCommon.myCstr(obj.arrDetail.Item(i).UOM_Code)
                        gvItem.Rows(i).Cells(colUomDesc).Value = clsCommon.myCstr(obj.arrDetail.Item(i).UOM_DESC)
                        gvItem.Rows(i).Cells(colConveriosnFactor).Value = clsCommon.myCdbl(obj.arrDetail.Item(i).Conversion_factor)
                        gvItem.Rows(i).Cells(colStockingUnit).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Stocking_Unit)
                        gvItem.Rows(i).Cells(colStockInHand).Value = clsCommon.myCdbl(obj.arrDetail.Item(i).Stock_In_Hand)
                        gvItem.Rows(i).Cells(colRequiredUomCode).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Required_UOM_Code)
                        gvItem.Rows(i).Cells(colRequiredUomDesc).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Required_UOM_Desc)
                        gvItem.Rows(i).Cells(colRequiredQty).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Required_Qty)
                        gvItem.Rows(i).Cells(colInputFromUnit).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Input_From_Qty)
                        gvItem.Rows(i).Cells(colOutputToUnit).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Output_To_qty)
                        gvItem.Rows(i).Cells(colOutputStkUnt).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Output_Stock_Qty)
                        gvItem.Rows(i).Cells(colRemarks).Value = clsCommon.myCstr(obj.arrDetail.Item(i).Remarks)

                        gvItem.Rows(i).Cells(colInputFromMRP).Value = clsCommon.myCdbl(obj.arrDetail.Item(i).Input_From_MRP)
                        gvItem.Rows(i).Cells(colOutputToMRP).Value = clsCommon.myCdbl(obj.arrDetail.Item(i).Output_To_MRP)
                        gvItem.Rows(i).Cells(colOutputStockMRP).Value = clsCommon.myCdbl(obj.arrDetail.Item(i).Output_Stock_MRP)

                    Catch exxx As Exception
                    End Try
                Next
            End If

            If obj.isPosted = 1 Then
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
        loadBlankGrid()
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        dtpDocDate.Value = dt
        fndItem.Value = ""
        lblItemDesc.Text = ""
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
    Sub loadBlankGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colSLNo
        repoTextColumn.Width = 60
        repoTextColumn.HeaderText = "SL.No"
        repoTextColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colUOMCode
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "UOM Code"
        repoTextColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colUomDesc
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "UOM Desc"
        repoTextColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colStockingUnit
        repoTextColumn.Width = 100
        repoTextColumn.HeaderText = "Stocking Unit"
        repoTextColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)


        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colConveriosnFactor
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Conversion Factor"
        repoDecimalColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colInputFromUnit
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Input From Unit"
        repoDecimalColumn.ReadOnly = True
        repoDecimalColumn.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colInputFromMRP
        repoDecimalColumn.Width = 130
        repoDecimalColumn.HeaderText = "Required MRP"
        repoDecimalColumn.ReadOnly = False
        repoDecimalColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDecimalColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colOutputToUnit
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Output To Unit"
        repoDecimalColumn.ReadOnly = True
        repoDecimalColumn.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colOutputStkUnt
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Output Stocking Unit"
        repoDecimalColumn.ReadOnly = True
        repoDecimalColumn.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colStockInHand
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Stock In Hand"
        repoDecimalColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)


        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colRequiredUomCode
        repoTextColumn.Width = 130
        repoTextColumn.HeaderText = "Required UOM Code"
        repoTextColumn.ReadOnly = False
        repoTextColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colRequiredUomDesc
        repoTextColumn.Width = 150
        repoTextColumn.HeaderText = "Required UOM Desc"
        repoTextColumn.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colRequiredQty
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Required Qty"
        repoDecimalColumn.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colOutputToMRP
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Out MRP"
        repoDecimalColumn.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colOutputStockMRP
        repoDecimalColumn.FormatString = "{0:n2}"
        repoDecimalColumn.Width = 100
        repoDecimalColumn.HeaderText = "Out Stock MRP"
        repoDecimalColumn.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoDecimalColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colRemarks
        repoTextColumn.Width = 400
        repoTextColumn.HeaderText = "Remarks"
        repoTextColumn.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoTextColumn)

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
    End Sub

    Private Sub FrmItemConverion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(fndItem.Value) <= 0 Then
                Throw New Exception("Please select Item")
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
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
                If clsItemStockConveriosnHead.ReverseAndUnpost(fndDocNo.Value) Then
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
                If clsItemStockConveriosnHead.deleteData(fndDocNo.Value, Nothing) Then
                    reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub

    
    Private Sub fndItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndItem._MYValidating
        Try
            fndItem.Value = clsItemMaster.getFinder("", fndItem.Value, isButtonClicked)
            lblItemDesc.Text = clsCommon.myCstr(clsItemMaster.GetItemName(fndItem.Value, Nothing))
            chkMRP.Checked = clsItemMaster.IsMRPItem(fndItem.Value)
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
        fndDocNo.Value = clsItemStockConveriosnHead.getFinder("", fndDocNo.Value, isButtonClicked)
        LoadData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub gvItem_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellEndEdit
        'Dim qry As String = " select UOM_Code as  UOM from TSPL_ITEM_UOM_DETAIL "
        'If Not isCellValueChangedOpen Then
        '    isCellValueChangedOpen = True
        '    If e.Column Is gvItem.Columns(colRequiredUomCode) AndAlso clsCommon.myLen(fndItem.Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colUOMCode).Value) > 0 Then
        '        gvItem.CurrentRow.Cells(colRequiredUomCode).Value = clsCommon.ShowSelectForm("UOMFND", qry, "UOM", " Item_Code='" & fndItem.Value & "'  and Uom_Code<>'" & gvItem.CurrentRow.Cells(colUOMCode).Value & "'", gvItem.CurrentRow.Cells(colRequiredUomCode).Value, "UOM_code", Not isCellValueChangedOpen)
        '        gvItem.CurrentRow.Cells(colRequiredUomDesc).Value = clsCommon.myCstr(clsUOMInfo.GetUnitDesc(gvItem.CurrentRow.Cells(colRequiredUomCode).Value, Nothing))
        '    End If
        '    If e.Column Is gvItem.Columns(colRequiredQty) AndAlso clsCommon.myLen(fndItem.Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colUOMCode).Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colRequiredUomCode).Value) > 0 Then


        '    End If
        'End If
        'isCellValueChangedOpen = False
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If clsCommon.myLen(fndItem.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select The Item", Me.Text)
            fndItem.Focus()
            Exit Sub
        End If

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select The Location", Me.Text)
            fndLocation.Focus()
            Exit Sub
        End If
        loadItemData(fndItem.Value)
    End Sub

    Private Sub gvItem_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvItem.CellFormatting
        Try
            If e.Column Is gvItem.Columns(colInputFromMRP) Then
                gvItem.CurrentRow.Cells(colInputFromMRP).ReadOnly = Not chkMRP.Checked
            ElseIf e.Column Is gvItem.Columns(colOutputToMRP) Then
                gvItem.CurrentRow.Cells(colOutputToMRP).ReadOnly = Not chkMRP.Checked
            ElseIf e.Column Is gvItem.Columns(colOutputStockMRP) Then
                gvItem.CurrentRow.Cells(colOutputStockMRP).ReadOnly = Not chkMRP.Checked
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            Dim qry As String = ""
            Dim stkUnit As String = String.Empty
            Dim stkUnitCF As Double = 0
            Dim fromUnit As String = String.Empty
            Dim fromUnitCF As Double = 0
            Dim toUnit As String = String.Empty
            Dim toUnitCF As Double = 0
            Dim ReqStkUntQty As Double = 0
            Dim fromUnitAvlQtyInStkUnt As Double = 0
            If Not isInSideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colRequiredQty) AndAlso clsCommon.myLen(fndItem.Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colUOMCode).Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colRequiredUomCode).Value) > 0 Then
                        stkUnit = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItem.Value & "' and Stocking_Unit='Y' "))
                        stkUnitCF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItem.Value & "' and Stocking_Unit='Y' "))

                        fromUnit = clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOMCode).Value)
                        fromUnitCF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItem.Value & "' and UOM_Code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOMCode).Value) & "' "))

                        toUnit = clsCommon.myCstr(gvItem.CurrentRow.Cells(colRequiredUomCode).Value)
                        toUnitCF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItem.Value & "' and Uom_Code='" & toUnit & "' "))

                        ReqStkUntQty = toUnitCF * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colRequiredQty).Value)
                        fromUnitAvlQtyInStkUnt = fromUnitCF * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colStockInHand).Value)

                        If fromUnitAvlQtyInStkUnt < ReqStkUntQty Then
                            gvItem.CurrentRow.Cells(colRequiredQty).Value = 0
                            Throw New Exception(" Required Stock Not Found in this UOM ")
                        End If
                        Dim ReqCF As Double = 0
                        Dim ReqUomQty As Double = 0
                        qry = "declare @ReqUOM varchar(100); set @ReqUOM  ='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colRequiredUomCode).Value) & "'; declare @ItemCode varchar(100); set @ItemCode  ='" & fndItem.Value & "'; declare @Loc varchar(100); set @Loc  ='" & fndLocation.Value & "'; select test.*, test.qty*test.conversion_factor as Stock_In_Stocking_Unit,(( test.qty*test.conversion_factor) /(select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='D0001' and UOM_Code ='Pack' )) as Stock_In_required_UOM,qty/(( test.qty*test.conversion_factor) /(select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code=@ItemCode  and UOM_Code =@ReqUOM  )) as CF_IN_Req from ( select Item_Code,UOM_Code as UOM,max(UOM_Description) as UOM_Description,max(Conversion_Factor) as conversion_factor,max(Stocking_Unit) as Stocking_Unit, sum(qty) as qty  from (select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0) as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code =@Loc  and TSPL_INVENTORY_MOVEMENT.InOut='I' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code=@ItemCode  union all select TSPL_ITEM_UOM_DETAIL.Item_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.UOM_Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit ,isnull(t1.qty,0) as qty   from TSPL_ITEM_UOM_DETAIL left join ( select Item_Code,UOM, coalesce( SUM(Qty),0)*-1 as qty  from TSPL_INVENTORY_MOVEMENT where TSPL_INVENTORY_MOVEMENT.Location_Code =@Loc  and TSPL_INVENTORY_MOVEMENT.InOut='O' group by Item_Code,UOM) as t1 on t1.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and t1.UOM=TSPL_ITEM_UOM_DETAIL .UOM_Code  where TSPL_ITEM_UOM_DETAIL.Item_Code=@ItemCode ) ttt group by Item_Code,UOM_Code  ) test where test.UOM ='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOMCode).Value) & "' "
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            ReqCF = clsCommon.myCdbl(dt.Rows(0)("CF_IN_Req"))
                        End If
                        ReqUomQty = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colRequiredQty).Value) * ReqCF
                        Dim RoundedReqUOMQty As Double = 0
                        If ReqUomQty > CInt(Math.Floor(ReqUomQty)) Then
                            RoundedReqUOMQty = CInt(Math.Floor(ReqUomQty)) + 1
                        Else
                            RoundedReqUOMQty = ReqUomQty
                        End If
                        Dim RestQty As Double = (RoundedReqUOMQty * fromUnitCF) - ReqStkUntQty
                        If RestQty - CInt(RestQty) > 0 Then
                            Throw New Exception("Invalid Conversion.")
                        End If
                        Dim Remarks As String = " Out:  " & RoundedReqUOMQty & " " & fromUnit
                        Remarks = Remarks & ", In:  " & clsCommon.myCstr(gvItem.CurrentRow.Cells(colRequiredQty).Value) & " " & toUnit
                        If RestQty > 0 Then
                            Remarks = Remarks & ", In:  " & RestQty & " " & stkUnit
                        End If
                        gvItem.CurrentRow.Cells(colRemarks).Value = Remarks
                        gvItem.CurrentRow.Cells(colInputFromUnit).Value = RoundedReqUOMQty
                        gvItem.CurrentRow.Cells(colOutputToUnit).Value = clsCommon.myCstr(gvItem.CurrentRow.Cells(colRequiredQty).Value)
                        gvItem.CurrentRow.Cells(colOutputStkUnt).Value = RestQty
                    ElseIf e.Column Is gvItem.Columns(colRequiredUomCode) AndAlso clsCommon.myLen(fndItem.Value) > 0 AndAlso clsCommon.myLen(gvItem.CurrentRow.Cells(colUOMCode).Value) > 0 Then
                        qry = " select UOM_Code as  UOM from TSPL_ITEM_UOM_DETAIL "
                        gvItem.CurrentRow.Cells(colRequiredUomCode).Value = clsCommon.ShowSelectForm("UOMFND", qry, "UOM", " Item_Code='" & fndItem.Value & "'  and Uom_Code<>'" & gvItem.CurrentRow.Cells(colUOMCode).Value & "'", gvItem.CurrentRow.Cells(colRequiredUomCode).Value, "UOM_code", Not isCellValueChangedOpen)
                        gvItem.CurrentRow.Cells(colRequiredUomDesc).Value = clsCommon.myCstr(clsUOMInfo.GetUnitDesc(gvItem.CurrentRow.Cells(colRequiredUomCode).Value, Nothing))
                    ElseIf e.Column Is gvItem.Columns(colInputFromMRP) Then
                        qry = " select distinct  CAST(MRP as varchar) as MRP from TSPL_INVENTORY_MOVEMENT   "
                        gvItem.CurrentRow.Cells(colInputFromMRP).Value = clsCommon.myCdbl(clsCommon.ShowSelectForm("MRPITemConv", qry, "MRP", " Item_Code='" + clsCommon.myCstr(fndItem.Value) + "' and UOM='" + clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOMCode).Value) + "'", clsCommon.myCstr(gvItem.CurrentRow.Cells(colInputFromMRP).Value), "", False))
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
