'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmAdjustmentProduction
    Inherits FrmMainTranScreen
#Region "Variables"

    Public Const RowTypeAdjustmentQty As String = "Quantity"
    Public Const RowTypeAdjustmentCost As String = "Cost"
    Public Const RowTypeAdjustmentBoth As String = "Both"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colAdjustmentType As String = "COLADJTYPE"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colCost As String = "COLCOST"
    Const colMRP As String = "MRP"

    Const colBreakCode As String = "BREAKCODE"
    Const colBreakQty As String = "BREAKQTY"
    Const colBreakCost As String = "BREAKCOST"
    Const colLeakQty As String = "LEAKQTY"
    Const colMFGDate As String = "MFGDATE"
    Const colBatchNo As String = "BATCHNO"
    Const colExpiryDate As String = "EXPIRYDATE"


    Const colRemarks As String = "REMARKS"
    Const colComment As String = "COMMENT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const strCostTransaction As String = "Production Entry"

    Public strDocumentNo As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnProductionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        '' Anubhooti 1-Aug-2014 BM00000003172 
        RadMenu1.Visible = MyBase.isModifyFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
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
        LoadItemType()
        LoadBlankGrid()
        AddNew()

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

    Sub LoadItemType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "FT"
        dr("Name") = "Trade"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FM"
        dr("Name") = "Manufactur"
        dt.Rows.Add(dr)

        cboItemType.DataSource = dt
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
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

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Cost"
        repoAmt.Name = colCost
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

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
        repoBreakCode.Name = colBreakCode
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

        Dim repoBreakCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBreakCost.WrapText = True
        repoBreakCost.ReadOnly = False
        repoBreakCost.FormatString = ""
        repoBreakCost.HeaderText = "Breakage Cost"
        repoBreakCost.Name = colBreakCost
        repoBreakCost.Width = 80
        repoBreakCost.Minimum = 0
        repoBreakCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBreakCost.ShowUpDownButtons = False
        repoBreakCost.Step = 0
        gv1.MasterTemplate.Columns.Add(repoBreakCost)

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

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colMFGDate
        repoManDate.ReadOnly = False
        repoManDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colExpiryDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBatchNo.Width = 100
        repoBatchNo.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

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
        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
        cboItemType.SelectedIndex = 0
    End Sub

    Sub BlankAllControls()
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
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAdjustmentType) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colComment) OrElse e.Column Is gv1.Columns(colBreakCode) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colMFGDate) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colBreakCode) Then
                            OpenBreakageList(False)
                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colAdjustmentType) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            Dim qry As String = "select distinct Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER"
                            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsCommon.ShowSelectForm("ADJPROGRIDMRPSE", qry, "MRP", "Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM = '" & clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) & "'", gv1.CurrentRow.Cells(colMRP).Value, "MRP", False))
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colMFGDate) Then
                            Dim MfgDate, Expdate As Date
                            MfgDate = gv1.CurrentRow.Cells(colMFGDate).Value
                            Dim intShelfLife As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(tech_shelf_life,0) from TSPL_ITEM_MASTER where ITEM_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'"))
                            Expdate = MfgDate.AddDays(intShelfLife)
                            gv1.CurrentRow.Cells(colExpiryDate).Value = Expdate
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "F", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = ""
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

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

    Sub OpenBreakageList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Breakage_Type as Code,Description  from TSPL_BREAKAGE_HEAD "
        gv1.CurrentRow.Cells(colBreakCode).Value = clsCommon.ShowSelectForm("BreakgeCodeProdAdj", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colBreakCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colBreakCode).Value) <= 0 Then
            gv1.CurrentRow.Cells(colBreakQty).Value = 0
            gv1.CurrentRow.Cells(colBreakCost).Value = 0
        End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
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

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = 0
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                gv1.Rows(IntRowNo).Cells(colCost).Value = Math.Round(dblAmt, 2)
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
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
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
            ElseIf e.Column Is gv1.Columns(colCost) Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentQty) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentCost) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value), RowTypeAdjustmentBoth) = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells(colCost).ReadOnly = True
                End If
            ElseIf e.Column Is gv1.Columns(colBreakQty) Then
                gv1.CurrentRow.Cells(colBreakQty).ReadOnly = IIf(clsCommon.myLen(gv1.CurrentRow.Cells(colBreakCode).Value) > 0, False, True)
            ElseIf e.Column Is gv1.Columns(colBreakCost) Then
                gv1.CurrentRow.Cells(colBreakCost).ReadOnly = IIf(clsCommon.myLen(gv1.CurrentRow.Cells(colBreakCode).Value) > 0, False, True)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
        whrclas = " Location_Type='Physical' and GIT_Type='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "FT") = CompairStringResult.Equal Then
            whrclas += " and Excisable='F' "
        Else
            whrclas += " and Excisable='T' "
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("AdjStoreLocation", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
        lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        ''Added by preeti Gupta====[16/10/2017]
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If
        UpdateAllTotals()

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If

        Dim qry As String = "select Excisable from TSPL_LOCATION_MASTER where Location_Type='Physical' and Location_Code='" + txtLocation.Value + "'"
        Dim strISExcisable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "FT") = CompairStringResult.Equal Then
            If Not clsCommon.CompairString(strISExcisable, "F") = CompairStringResult.Equal Then
                Throw New Exception("Location should be Non-Excisable for Item Type Trande")
            End If
        Else
            If Not clsCommon.CompairString(strISExcisable, "T") = CompairStringResult.Equal Then
                Throw New Exception("Location should be Excisable for Item Type Manufacture")
            End If
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim strMRP As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    Throw New Exception("Please enter MRP of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colMFGDate).Value) <= 0 Then
                    Throw New Exception("Please enter Manufacturer Date of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                    Throw New Exception("Please enter Batch No of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "Out") = CompairStringResult.Equal Then
                If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                    Dim dblBalQty As Double = Math.Round(clsItemLocationDetails.getBalanceWithUnapprove(strICode, txtLocation.Value, strMRP, strUOM, txtAdjustmentNo.Value, txtDate.Value), 2, MidpointRounding.ToEven)
                    If dblQty > dblBalQty Then
                        Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Actual Balance Quantity - " + clsCommon.myCstr(dblBalQty) + " at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                End If
            End If
        Next
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsAdjustments()
                obj.Adjustment_No = txtAdjustmentNo.Value
                obj.Adjustment_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()

                obj.Unit_Code = "ALL"
                obj.ItemType = clsCommon.myCstr(cboItemType.SelectedValue)
                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                If chkStock.Checked Then
                    obj.Stock_Type = "P"
                End If

                obj.Arr = New List(Of ClsAdjustmentsDetails)()

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsAdjustmentsDetails()
                    'objTr.Adjustment_No=
                    objTr.Adjustment_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Adjustment_Type = clsCommon.myCstr(grow.Cells(colAdjustmentType).Value).Substring(0, 1) + IIf(clsCommon.CompairString(cboTransType.SelectedValue, "In") = CompairStringResult.Equal, "I", "D")
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                    objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)

                    objTr.BreakageType = clsCommon.myCstr(grow.Cells(colBreakCode).Value)
                    objTr.Breakage = clsCommon.myCdbl(grow.Cells(colBreakQty).Value)
                    objTr.Breakage_Cost = clsCommon.myCdbl(grow.Cells(colBreakCost).Value)
                    objTr.LeakageQty = clsCommon.myCdbl(grow.Cells(colLeakQty).Value)

                    objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colMFGDate).Value)
                    objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                    objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiryDate).Value)

                    objTr.ItemType = clsCommon.myCstr(cboItemType.SelectedValue)
                    obj.ItemType = obj.ItemType
                    objTr.Basic_Price = clsItemBasicPrice.GetBasicPrice(objTr.Item_Code, objTr.mrp)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Adjustment_No, NavigatorType.Current)
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
            Dim obj As New ClsAdjustments()
            obj = obj.GetData(strCode, strCostTransaction, NavTyep, Nothing)
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
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                cboTransType.SelectedValue = obj.Trans_Type
                cboItemType.SelectedValue = obj.ItemType
                If clsCommon.CompairString(obj.Stock_Type, "P") = CompairStringResult.Equal Then
                    chkStock.Checked = True
                Else
                    chkStock.Checked = False
                End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsAdjustmentsDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Adjustment_Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        Dim AdjTypeFirstChar As String = objTr.Adjustment_Type.Substring(0, 1)
                        If clsCommon.CompairString(AdjTypeFirstChar, "Q") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentQty
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "C") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentCost
                        ElseIf clsCommon.CompairString(AdjTypeFirstChar, "B") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakCode).Value = objTr.BreakageType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakQty).Value = objTr.Breakage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreakCost).Value = objTr.Breakage_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeakQty).Value = objTr.LeakageQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMFGDate).Value = objTr.MFG_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.Batch_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpiryDate).Value = objTr.Expiry_Date
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

    Private Sub txtDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesc.TextChanged

    End Sub

    Private Sub cboRefDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (ClsAdjustments.PostData(txtAdjustmentNo.Value, strCostTransaction)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
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
                If (ClsAdjustments.DeleteData(txtAdjustmentNo.Value, strCostTransaction)) Then
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
            Dim qst As String = "select count(*) from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + txtAdjustmentNo.Value + "' AND ItemType IN ('FM', 'FT') "
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
        Dim qry As String = "SELECT Adjustment_No AS [AdjustmentNumber], Adjustment_Date as [Date], Document_No,case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location] FROM  TSPL_ADJUSTMENT_HEADER  "
        Dim whrClas As String = " 1=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        whrClas += " AND ItemType IN ('FM', 'FT')"


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
            ElseIf gv1.CurrentColumn Is gv1.Columns(colBreakCode) Then
                OpenBreakageList(True)
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
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        PrintData()
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            PrintData(txtAdjustmentNo.Value, False, False)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub PrintData(ByVal strAdjustmentNo As String, ByVal IsPreprinted As Boolean, ByVal IsEmpty As Boolean)
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + strAdjustmentNo + "' and TSPL_ADJUSTMENT_HEADER.ItemType='E' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No=1"
            Dim TransType As String = clsDBFuncationality.getSingleValue("select TSPL_ADJUSTMENT_HEADER.Trans_Type  from TSPL_ADJUSTMENT_HEADER  where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + strAdjustmentNo + "'")
            If (clsCommon.CompairString(TransType, "Out") = CompairStringResult.Equal) Then
                TransType = "Out"
            Else
                TransType = "In"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub funPrint()
    '    Try
    '        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
    '            Throw New Exception("Adjustment No not found to Print")
    '        End If

    '        Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "' and TSPL_ADJUSTMENT_HEADER.ItemType='E' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No=1"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quality Increase' else CASE when detail.Adjustment_Type='QD' then 'Quality Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1] from TSPL_ADJUSTMENT_HEADER as Head left outer join TSPL_ADJUSTMENT_DETAIL as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Head.Adjustment_No='" + txtAdjustmentNo.Value + "' order by detail.Adjustment_Line_No "
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '            InventryViewer.funreport(dt, "crptAdjustment", "Adjustment Detail")
    '        Else
    '            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Adjustment_Type")), "BD") = CompairStringResult.Equal Then
    '                qry = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,(TSPL_ADJUSTMENT_HEADER.Adjustment_Date+' '+TSPL_ADJUSTMENT_HEADER.created_time) as Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Item_Quantity,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No " & _
    '                " from TSPL_ADJUSTMENT_DETAIL" & _
    '                " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No" & _
    '                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER.Customer_CODE" & _
    '                " where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "' ORDER by TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No"
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomIssue", "Adjustment Detail")

    '            Else
    '                ''For both Increase OR Receipt Challan
    '                Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
    '                Dim strACaption As String = "From"
    '                Dim strIssueCaption As String = "Empty Receipt"
    '                Dim strDateCaption As String = "Receipt Date"
    '                qry = "select Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME,MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short, SUM(Amount ) as Amount, '" + strReportName + "' as ReportName,'" + strACaption + "' as ACaption,'" + strIssueCaption + "' as EmptyCaption,'" + strDateCaption + "' as DateCaption,max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date,max(Vehicle_No) as Vehicle_No ,MAX(Add1) as Add1,max(Add2) as Add2,max(Add3) as Add3,max(City_Name) as City_Name,max(State_Name) as State_Name from(" & _
    '                "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Description ,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Unit_Code,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FC' then Item_Quantity end as FCS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' then Item_Quantity end as FBS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then Item_Quantity end as FSH, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then Item_Quantity end as ECS,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then Item_Quantity end as EBS, 0 as Leak_Qty,TSPL_ADJUSTMENT_DETAIL.Breakage,0 As Short_Qty, Case When TSPL_CUSTOMER_MASTER.Cust_Type_Code Not IN ('F','S') Then (ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0)+ISNULL(TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0)) Else ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) End as Amount, TSPL_ADJUSTMENT_HEADER.EMP_NAME as SalesManName,TSPL_ADJUSTMENT_HEADER.Challan_No,TSPL_ADJUSTMENT_HEADER.Challan_date,TSPL_ADJUSTMENT_HEADER.Vehicle_No,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3,TSPL_CITY_MASTER.City_Name,TSPL_TDS_STATE_MASTER.State_Name from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No= TSPL_ADJUSTMENT_DETAIL.Adjustment_No "
    '                qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_ADJUSTMENT_HEADER.Customer_CODE"
    '                qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
    '                qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
    '                qry += " where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "'  " & _
    '                ")xxx group by Adjustment_No,Item_Code order by Item_Desc"
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                InventryViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "crptAdjustmentCustomReceipt", "Adjustment Detail")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub


    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        ExportProductionEntry()
    End Sub

    Public Sub ExportProductionEntry()
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Item Type], '' as [Date], '' [Description], '' as [Location], '' as [Item Code], '' as [UOM], '' as [Quantity], '' as [MRP], '' as [MFG Date], '' as [Batch No], 0 as [Cost], '' as [Remarks] "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Production")
        End Try
    End Sub


    Private Sub Opening_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Opening.Click

        Dim gv As New RadGridView()
        Dim line As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Item Type", "Date", "Description", "Location", "Item Code", "UOM", "Quantity", "MRP", "MFG Date", "Batch No", "Cost", "Remarks") Then
            Dim trans As SqlTransaction = Nothing
            Try
                Dim obj As New ClsAdjustments()
                obj.Arr = New List(Of ClsAdjustmentsDetails)()

                'trans = clsDBFuncationality.GetTransactin()
                Dim strAdcode As String = ""

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2

                    Dim strIType As String
                    Dim ItmType = clsCommon.myCstr(grow.Cells("Item Type").Value)
                    If clsCommon.CompairString(ItmType, "FT") = CompairStringResult.Equal Then
                        strIType = "FT"     '---FG Trading---
                    ElseIf clsCommon.CompairString(ItmType, "FM") = CompairStringResult.Equal Then
                        strIType = "FM"     '---FG Manufacturing---
                    Else
                        Throw New Exception("Please Insert Item Type as Only 'FT' Or 'FM' at Line No '" + LineNo + "'")
                    End If

                    '-----------------Adjustment Date---------------------------
                    Dim strADate As DateTime
                    If clsCommon.myLen(grow.Cells("Date").Value) <= 0 Then
                        strADate = clsCommon.GETSERVERDATE()
                    Else
                        strADate = clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MMM/yyyy")
                    End If
                    '-------------------------------------------------------------

                    Dim strStime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    '-----------------Location--------------------
                    Dim strLoc As String
                    Dim Loc As String = clsCommon.myCstr(grow.Cells("Location").Value)
                    If clsCommon.myLen(Loc) > 0 Then
                        strLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_LOCATION_MASTER Where location_Code='" + Loc + "'"))
                        If Not clsCommon.CompairString(strLoc, Loc) = CompairStringResult.Equal Then
                            Throw New Exception("The Location '" + strLoc + "' at Line No '" + LineNo + "' does not exist in Master")
                        End If
                    Else
                        Throw New Exception("please Insert Location at Line No '" + LineNo + "' ")
                    End If
                    '---------------------------------------------

                    Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + strLoc + "' ")

                    Dim Description As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(Description) > 200 Then
                        Throw New Exception("Please Check the length of Description at line no '" + LineNo + "'")
                    End If

                    Dim UnitCode As String = "All"      '---Default---
                    Dim Modifytime As String = clsCommon.GetPrintDate((DateTime.Now), "hh:mm tt")

                    '--------------Item Code----------------
                    Dim ICode As String = grow.Cells("Item Code").Value.ToString()
                    Dim ItemCode As String
                    If clsCommon.myLen(ICode) > 0 Then
                        ItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Code  from TSPL_ITEM_MASTER where Item_Code='" + ICode + "'"))
                        If Not clsCommon.CompairString(ItemCode, ICode) = CompairStringResult.Equal Then
                            Throw New Exception("The Item Code '" + ICode + "' at Line Line No '" + LineNo + "' Does not Exist In Master")
                        End If
                    Else
                        Throw New Exception("Please Insert Item Code at Line No '" + LineNo + "'")
                    End If
                    '------------------------------------------

                    Dim ItemDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where  Item_Code='" + ItemCode + "'"))

                    Dim account As String = clsDBFuncationality.getSingleValue("select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + ItemCode + "')")
                    Dim AdjType As String = "BI"

                    '-------------------------------Unit Code----------------
                    Dim struom As String
                    Dim UOM As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                    Dim Dt As DataTable = clsDBFuncationality.GetDataTable("Select UOM_Code  from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + ItemCode + "'")
                    Dim arrUom As New List(Of String)
                    For Each dr As DataRow In Dt.Rows
                        arrUom.Add(clsCommon.myCstr(dr("UOM_Code")))
                    Next
                    If Not arrUom.Contains(UOM) Then
                        Throw New Exception("Please Insert UOM among (" + clsCommon.GetMulcallString(arrUom) + ") at Line No '" + LineNo + "'")
                    Else
                        struom = clsDBFuncationality.getSingleValue("Select UOM_Code from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + ItemCode + "' AND UOM_Code='" + UOM + "'")
                    End If
                    '--------------------------------------------------------

                    '-----------------Quantity------------------------------
                    Dim Iqty As Double = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    If Iqty <= 0 Then
                        Throw New Exception("Please Insert Item Quantity at Line No '" + LineNo + "'")
                    End If
                    '-------------------------------------------------------

                    '---------------COst Adjustment------------------
                    Dim ItemCost As Double = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells("Cost").Value))
                    If ItemCost > 0 Then
                        Dim Convfact As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + ItemCode + "' and UOM_Code='" + struom + "'"))
                        'Dim ItemCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Cost from TSPL_ITEM_MASTER where  Item_Code='" + ItemCode + "'", trans))
                        ItemCost = (Iqty / Convfact) * ItemCost
                    End If
                    '------------------------------------------------

                    Dim BrkgType As String = "Select"
                    Dim Bqty As Decimal = 0
                    Dim Bcost As Decimal = 0
                    Dim Lqty As Decimal = 0
                    '---------------Batch No---------------
                    Dim BatchNo As String = clsCommon.myCstr(grow.Cells("Batch No").Value)
                    If clsCommon.myLen(BatchNo) <= 0 Then
                        Throw New Exception("Please 'Batch No' at Line No '" + LineNo + "'")
                    End If
                    '--------------------------------------

                    '---------------MRP---------------------
                    Dim MRP As Double = clsCommon.myCdbl(grow.Cells("MRP").Value)
                    If MRP > 0 Then
                        Dim MRP1 As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Distinct Item_Basic_Net from TSPL_ITEM_PRICE_MASTER Where Item_Code='" + ItemCode + "' AND Item_Basic_Net = " + clsCommon.myCstr(MRP) + " AND UOM='" + UOM + "'"))
                        If MRP <> MRP1 Then
                            Throw New Exception("The MRP '" + clsCommon.myCstr(MRP) + "' Against Item- '" + ItemCode + "' AND UOM- '" + UOM + "' at Line No '" + LineNo + "' does not exist")
                        End If
                    Else
                        Throw New Exception("Please Insert MRP at Line No '" + LineNo + "'")
                    End If
                    '---------------------------------------

                    Dim Remark As String = clsCommon.myCstr(grow.Cells("Remark"))

                    '--------------------MFG Date---------------
                    Dim mfgDate As String = ""
                    If clsCommon.myLen(grow.Cells("MFG Date").Value) > 0 Then
                        mfgDate = clsCommon.GetPrintDate(grow.Cells("MFG Date").Value, "dd/MM/yyyy")
                    ElseIf clsCommon.myLen(mfgDate) <= 0 Or clsCommon.myLen(mfgDate) > 10 Then
                        Throw New Exception("Please Insert a Valid 'MFG Date' at Line No '" + LineNo + "'")
                    End If
                    '--------------------------------------------

                    Dim expdate As String = clsCommon.GETSERVERDATE()

                    Dim commt As String = ""
                    Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE()
                    If line = 1 Then
                        ''started by priti

                        obj.Adjustment_No = strAdcode
                        obj.Adjustment_Date = strADate
                        'obj.Posting_Date
                        obj.Reference = ""
                        obj.Description = Description
                        'obj.Posted()

                        obj.Unit_Code = "ALL"
                        obj.ItemType = strIType
                        obj.Loc_Code = strLoc
                        obj.Loc_Desc = strLocDesc
                        obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)



                        '' ended by priti
                    End If

                    Dim objTr As New ClsAdjustmentsDetails()

                    'objTr.Adjustment_No = ""
                    objTr.Adjustment_Line_No = line
                    objTr.Item_Code = ItemCode
                    objTr.Item_Description = ItemDesc
                    objTr.Adjustment_Type = AdjType
                    'objTr.Location_Code=Pick in SaveData from header
                    objTr.Item_Quantity = Iqty
                    objTr.Item_Cost = ItemCost
                    objTr.Unit_Code = struom
                    'objTr.Account_Code= Pick in SaveData
                    'objTr.Account_Description=Pick in SaveData
                    objTr.Remarks = Remark
                    objTr.Comments = commt
                    objTr.mrp = MRP

                    objTr.BreakageType = BrkgType
                    objTr.Breakage = Bqty
                    objTr.Breakage_Cost = Bcost
                    objTr.LeakageQty = Lqty

                    objTr.MFG_Date = mfgDate
                    objTr.Batch_No = BatchNo
                    objTr.Expiry_Date = expdate

                    objTr.ItemType = strIType
                    obj.ItemType = strIType

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                    line = line + 1
                Next

                'trans.Commit()
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                RadMessageBox.Show("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try
        End If
        Me.Controls.Remove(gv)
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
                If ClsAdjustments.ReverseAndUnpost(txtAdjustmentNo.Value, trans) Then
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

    Private Sub cboItemType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboItemType.SelectedIndexChanged

    End Sub
End Class
