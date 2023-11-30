Imports System.Data.SqlClient
Imports common
Public Class FrmWarehouseBreakage
    Inherits FrmMainTranScreen

#Region "Variables"
    Private qry As String = ""
    Private dt As DataTable
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colMRP As String = "MRP"
    Const colMFGDate As String = "MFGDATE"
    Const colBreakage As String = "BREAKCODE"
    Const colBreakQty As String = "BREAKQTY"
    Const colLeakQty As String = "LEAKQTY"
    Const colShortQty As String = "SHORTQTY"
    Const colRemarks As String = "REMARKS"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmWarehouseBreakage)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        '--Preeti Gupta--Ticket no[BM00000003174]
        If btnSave.Visible = True Then
            rmiExport.Enabled = True
        Else
            rmiExport.Enabled = False
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
        LoadBlankGrid()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        ElseIf clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtReference.MaxLength = 200
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

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

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        'Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoAmt.FormatString = ""
        'repoAmt.HeaderText = "Cost"
        'repoAmt.Name = colCost
        'repoAmt.Width = 80
        'repoAmt.Minimum = 0
        'repoAmt.ReadOnly = False
        'repoAmt.ShowUpDownButtons = False
        'repoAmt.Step = 0
        'repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.MasterTemplate.Columns.Add(repoAmt)

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

        Dim repoBreakCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBreakCode.FormatString = ""
        repoBreakCode.HeaderText = "Breakage Code"
        repoBreakCode.Name = colBreakage
        repoBreakCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoBreakCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBreakCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBreakCode)

        Dim repoBreakQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBreakQty.WrapText = True
        repoBreakQty.ReadOnly = False
        repoBreakQty.FormatString = ""
        repoBreakQty.HeaderText = "Breakage Qty"
        repoBreakQty.Name = colBreakQty
        repoBreakQty.Width = 80
        repoBreakQty.Minimum = 0
        repoBreakQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBreakQty.ShowUpDownButtons = False
        repoBreakQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoBreakQty)

        Dim repoLeakQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeakQty.WrapText = True
        repoLeakQty.ReadOnly = False
        repoLeakQty.FormatString = ""
        repoLeakQty.HeaderText = "Leak Qty"
        repoLeakQty.Name = colLeakQty
        repoLeakQty.Width = 80
        repoLeakQty.Minimum = 0
        repoLeakQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLeakQty.ShowUpDownButtons = False
        repoLeakQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoLeakQty)

        Dim repoShortQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShortQty.WrapText = True
        repoShortQty.ReadOnly = False
        repoShortQty.FormatString = ""
        repoShortQty.HeaderText = "Short Qty"
        repoShortQty.Name = colShortQty
        repoShortQty.Width = 80
        repoShortQty.Minimum = 0
        repoShortQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoShortQty.ShowUpDownButtons = False
        repoShortQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoShortQty)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colMFGDate
        repoManDate.ReadOnly = False
        repoManDate.Width = 80
        repoManDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

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
        txtDate.Focus()
        gv1.Rows.AddNew()
    End Sub

    Sub BlankAllControls()
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReference.Text = ""
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colBreakage) OrElse e.Column Is gv1.Columns(colMRP) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) Then
                                gv1.CurrentRow.Cells(colQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtLocation.Value, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), txtDocNo.Value, txtDate.Value)
                            Else
                                gv1.CurrentRow.Cells(colQty).Value = 0.0
                            End If
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) Then
                                gv1.CurrentRow.Cells(colQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtLocation.Value, clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), txtDocNo.Value, txtDate.Value)
                            End If
                        ElseIf e.Column Is gv1.Columns(colBreakage) Then
                            OpenBreakageList(False)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            Dim qry As String = "select distinct Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER"
                            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsCommon.ShowSelectForm("ADJPROGRIDMRPSE", qry, "MRP", "Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM = '" & clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) & "'", gv1.CurrentRow.Cells(colMRP).Value, "MRP", False))
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub


    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", False, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code")
            Exit Sub
        End If
        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "
        'Dim WhrCls As String = "Item_Code ='" + strICode + "' and UOM_Code in ('FC','FB','EC','EB')"
        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("AdjStoreUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
            qry = "select top 1 Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER where Item_Code ='" + strICode + "' and UOM='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        End If
    End Sub

    Sub OpenBreakageList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Breakage_Type as Code,Description  from TSPL_BREAKAGE_HEAD "
        gv1.CurrentRow.Cells(colBreakage).Value = clsCommon.ShowSelectForm("BreakgeCodeProdAdj", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colBreakage).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colBreakage).Value) <= 0 Then
            gv1.CurrentRow.Cells(colBreakQty).Value = 0
        End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim dblAmt As Double
        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value), "FC") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value), "FB") = CompairStringResult.Equal Then
                Dim ItemCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Cost from TSPL_ITEM_MASTER where  Item_Code='" + strICode + "'"))
                dblAmt = dblQty * ItemCost
            Else
                dblAmt = dblQty * dblRate
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
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
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
        Dim qry As String = ""
        Dim whrclas As String = ""
        qry = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
        'whrclas = " Location_Type='Physical' and GIT_Type='N'"
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        'whrclas += " and Excisable='T' "

        txtLocation.Value = clsCommon.ShowSelectForm("AdjStoreLocation", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
        lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        '=======preeti Gupta=============
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '================================
        UpdateAllTotals()
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If

        Dim Count As Integer = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            If clsCommon.myLen(strICode) > 0 Then
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strMRP As String = clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value))
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    Throw New Exception("Please enter MRP of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                Count += 1
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBreakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeakQty).Value) + clsCommon.myCdbl(gv1.Rows(ii).Cells(colShortQty).Value)
                If dblQty <= 0 Then
                    Throw New Exception("Please enter Breakage/Leakage/Shortage at Row No " + clsCommon.myCstr(ii + 1))
                End If
                Dim dblBalQty As Double = 0 ''Math.Round(clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), txtLocation.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDocNo.Value, txtDate.Value), 2, MidpointRounding.ToEven)
                If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                    dblBalQty = Math.Round(clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), txtLocation.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDocNo.Value, txtDate.Value), 2, MidpointRounding.ToEven)
                ElseIf clsCommon.CompairString(strUOM, "EB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "EC") = CompairStringResult.Equal Then
                    dblBalQty = Math.Round(clsItemLocationDetails.getBalanceWithUnapproveEmpty(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), txtLocation.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDocNo.Value, txtDate.Value), 2, MidpointRounding.ToEven)
                Else
                    dblBalQty = Math.Round(clsItemLocationDetails.getBalanceWithUnapproveForRMOther(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)), 2, MidpointRounding.ToEven)
                End If

                If dblQty > dblBalQty Then
                    Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Actual Balance Quantity - " + clsCommon.myCstr(dblBalQty) + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
            End If
        Next
        If Count <= 0 Then
            Throw New Exception("Please enter atleast single item.")
        End If
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsWarehouseBreakage()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                obj.Loc_Code = txtLocation.Value
                'obj.Loc_Desc = lblLocation.Text
                obj.Arr = New List(Of clsWarehouseDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colICode).Value) > 0 Then
                        Dim objTr As New clsWarehouseDetail()
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        'objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        'objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colMFGDate).Value)
                        objTr.Breakage = clsCommon.myCstr(grow.Cells(colBreakage).Value)
                        objTr.Breakage_Qty = clsCommon.myCdbl(grow.Cells(colBreakQty).Value)
                        objTr.Leakage_Qty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)
                        objTr.Shortage_Qty = clsCommon.myCdbl(grow.Cells(colShortQty).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
                Return isSaved
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
            Dim obj As New clsWarehouseBreakage()
            obj = obj.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If obj.Is_Post = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsWarehouseDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakage).Value = objTr.Breakage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakQty).Value = objTr.Breakage_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.Leakage_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShortQty).Value = objTr.Shortage_Qty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colMFGDate).Value = objTr.MFG_Date
                    Next
                    If Not (obj.Is_Post = 1) Then
                        gv1.Rows.AddNew()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsWarehouseBreakage.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsWarehouseBreakage.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_WH_BREAKAGE_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT Document_No AS [Document], Document_Date as [Date], Loc_Code as [Location], Case When Is_Post=1 Then 'Posted' Else 'Pending' End as [Status] FROM  TSPL_WH_BREAKAGE_HEAD  "
        Dim whrClas As String = " 1=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtDocNo.Value = clsCommon.ShowSelectForm("WHDocFinder", qry, "Document", whrClas, txtDocNo.Value, "Document", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
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
            ElseIf gv1.CurrentColumn Is gv1.Columns(colBreakage) Then
                OpenBreakageList(True)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        PrintData()
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            PrintData(txtDocNo.Value, False, False)
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
        Try
            Dim qry As String = "select TSPL_WH_BREAKAGE_HEAD.Document_No,TSPL_WH_BREAKAGE_HEAD.Document_Date,TSPL_WH_BREAKAGE_HEAD.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as address,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_WH_BREAKAGE_DETAIL.Unit_Code ,TSPL_WH_BREAKAGE_DETAIL.mrp,TSPL_WH_BREAKAGE_DETAIL.Item_Quantity  ,TSPL_WH_BREAKAGE_DETAIL.Breakage,TSPL_WH_BREAKAGE_DETAIL.Breakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Leakage_Qty,TSPL_WH_BREAKAGE_DETAIL.Shortage_Qty "
            qry += ",TSPL_WH_BREAKAGE_HEAD.Created_By as [Created By] ,TSPL_WH_BREAKAGE_HEAD.Modified_By as [Modified By] from tspl_wh_breakage_head left outer join tspl_wh_breakage_detail on tspl_wh_breakage_head.Document_No=tspl_wh_breakage_detail.Document_No left outer join TSPL_LOCATION_MASTER on tspl_wh_breakage_head.Loc_code= TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_COMPANY_MASTER on tspl_wh_breakage_head.comp_code=TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_ITEM_MASTER on TSPL_WH_BREAKAGE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code where TSPL_WH_BREAKAGE_HEAD.Document_No='" + strAdjustmentNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptWareHouseBreakage", "Ware Houes Breakage")
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
