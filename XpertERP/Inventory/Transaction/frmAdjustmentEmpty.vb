'-Updation by--[Pankaj Kumar Chaudhary]--Against Ticket no---[BM00000001243, BM00000001291, BM00000001297, BM00000001297, BM00000001630]
Imports common
Imports System.Data.SqlClient

Public Class frmAdjustmentEmpty
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
    Const colbrkgType As String = "BRKGTYPE"
    Const colbrkgQty As String = "BRKGQTY"
    Const colbrkgCost As String = "BRKGCOST"
    Const colCost As String = "COLCOST"
    Const colMRP As String = "MRP"
    Const colRemarks As String = "REMARKS"
    Const colComment As String = "COMMENT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Const strCostTransaction As String = "Empty Transactions"

    Public strDocumentNo As String = ""
    Public strCustomer As String = ""
    Public strSalesman As String = ""
    Public strLocation As String = ""
    Public dtTransDate As DateTime? = Nothing
    Public strVehicleCode As String = ""
    Public strLoadoutNo As String = ""
    Public isSaleInvoice As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnEmptyTrans)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag

        btnDelete.Visible = MyBase.isDeleteFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            RadMenuItem4.Enabled = True
            mtbnImportOPBalance.Enabled = True
        Else
            RadMenuItem4.Enabled = False
            mtbnImportOPBalance.Enabled = False
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
        LoadRefDocument()
        LoadBlankGrid()
        AddNew()
        rbtnRoute.IsChecked = True

        ''For filled value from Previous form
        If clsCommon.myLen(strLoadoutNo) > 0 Then
            txtDocumentNo.Value = strLoadoutNo
            If isSaleInvoice Then
                cboRefDocument.SelectedValue = "Sale Invoice"
            Else
                cboRefDocument.SelectedValue = "Load out/Transfer"
            End If
        End If
         

        If clsCommon.myLen(strCustomer) > 0 Then
            txtCustomer.Value = strCustomer
            txtCustomer__MYValidating(Nothing, Nothing, False)
        End If
        If clsCommon.myLen(strSalesman) > 0 Then
            txtSalesman.Value = strSalesman
            txtSalesman__MYValidating(Nothing, Nothing, False)
        End If
        If clsCommon.myLen(strLocation) > 0 Then
            txtLocation.Value = strLocation
            txtLocation__MYValidating(Nothing, Nothing, False)
        End If
        If dtTransDate.HasValue > 0 Then
            txtDate.Value = dtTransDate
        End If
        If clsCommon.myLen(strVehicleCode) > 0 Then
            txtVehicleNo.Value = strVehicleCode
            txtVehicleNo__MYValidating(Nothing, Nothing, False)
        End If

        ''End of For filled value from Previous form

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

    Sub LoadRefDocument()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Sale Invoice"
        dr("Name") = "Sale Invoice"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Load out/Transfer"
        dr("Name") = "Load out/Transfer"
        dt.Rows.Add(dr)

        cboRefDocument.DataSource = dt
        cboRefDocument.ValueMember = "Code"
        cboRefDocument.DisplayMember = "Name"
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
        'repoMRP.ReadOnly = True
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ShowUpDownButtons = False
        repoMRP.Step = 0
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoBrkgType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoBrkgType.FormatString = ""
        repoBrkgType.HeaderText = "Breakage Type"
        repoBrkgType.Name = colbrkgType
        repoBrkgType.DataSource = clsDBFuncationality.GetDataTable("Select Breakage_Type, Description  From (Select 'Select' as Breakage_Type,'Select' as [Description], 0 as OrderBy Union Select Breakage_Type, Description, 1 as OrderBy  from TSPL_BREAKAGE_HEAD ) XXX Order by OrderBy")
        repoBrkgType.ValueMember = "Breakage_Type"
        repoBrkgType.DisplayMember = "Description"
        repoBrkgType.Width = 150
        gv1.MasterTemplate.Columns.Add(repoBrkgType)

        Dim repoBrkgQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBrkgQty.FormatString = ""
        repoBrkgQty.HeaderText = "Breakage Qty"
        repoBrkgQty.Name = colbrkgQty
        repoBrkgQty.Width = 80
        repoBrkgQty.Minimum = 0
        repoBrkgQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBrkgQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoBrkgQty)

        Dim repoBrkgCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBrkgCost.WrapText = True
        repoBrkgCost.ReadOnly = True
        repoBrkgCost.FormatString = ""
        repoBrkgCost.HeaderText = "Breakage Cost"
        repoBrkgCost.Name = colbrkgCost
        repoBrkgCost.Width = 80
        repoBrkgCost.Minimum = 0
        repoBrkgCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBrkgCost.ShowUpDownButtons = False
        repoBrkgCost.Step = 0
        gv1.MasterTemplate.Columns.Add(repoBrkgCost)

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

    End Sub

    Sub BlankAllControls()
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtAdjustmentNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        cboTransType.SelectedIndex = 0
        rbtnRoute.IsChecked = True
        UsLock1.Status = ERPTransactionStatus.Pending
        cboRefDocument.SelectedIndex = 0
        txtDocumentNo.Value = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReference.Text = ""
        txtCustomer.Value = ""
        lblCustomer.Text = ""
        txtChallanNo.Text = ""
        txtChallanDate.Value = txtDate.Value
        txtChallanDate.Checked = False
        txtSalesman.Value = ""
        lblSalesman.Text = ""
        txtGENo.Text = ""
        txtGEDate.Value = txtDate.Value
        txtGEDate.Checked = False
        txtVehicleNo.Value = ""
        lblVehicle.Text = ""
        btnPost.Enabled = True
    End Sub

   

    Public Sub FunFbFcTotal()
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        Dim totalSH As Decimal = 0
        Dim totalBkg As Decimal = 0
        'If clsCommon.myCdbl(g.Cells(colICode).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colBreakage).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colLeak).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colShortage).Value) <> 0 Then

        For Each g As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(g.Cells(colICode).Value) <> 0 Then
                If clsCommon.CompairString(g.Cells(colUnit).Value, "EC") = CompairStringResult.Equal Then
                    totalfc = totalfc + clsCommon.myCdbl(g.Cells(colQty).Value)
                End If
                If clsCommon.CompairString(g.Cells(colUnit).Value, "EB") = CompairStringResult.Equal Then
                    totalfb = totalfb + clsCommon.myCdbl(g.Cells(colQty).Value)
                End If
                If clsCommon.CompairString(g.Cells(colUnit).Value, "SH") = CompairStringResult.Equal Then
                    totalSH = totalSH + clsCommon.myCdbl(g.Cells(colQty).Value)
                End If
                If clsCommon.myCdbl(g.Cells(colbrkgQty).Value) > 0 Then
                    totalBkg = totalBkg + clsCommon.myCdbl(g.Cells(colbrkgQty).Value)
                End If
            End If
        Next
     
        'End If
        lblfb.Text = CStr(totalfb)
        lblfc.Text = CStr(totalfc)
        lblshell.Text = CStr(totalSH)
        lblBreakage.Text = CStr(totalBkg)
        'If totalfc = 0 Then
        '   lblfc.Text = 0
        'ElseIf totalfb = 0 Then
        '   lblfb.Text = 0
        'ElseIf totalfb = 0 And totalfc = 0 And totalSH = 0 Then
        '    lblfc.Text = 0
        '    lblfb.Text = 0
        '    lblshell.Text = 0
        'Else


        'End If


    End Sub






    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                'Dim total1 As Decimal

                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colAdjustmentType) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colComment) OrElse e.Column Is gv1.Columns(colbrkgType) OrElse e.Column Is gv1.Columns(colbrkgQty) OrElse e.Column Is gv1.Columns(colMRP) Then
                        ''''''''''''''''Updated by shipra'''''''''''''''''''''''''''''

                        ''''''''''''''''Updated by shipra'''''''''''''''''''''''''''''
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                            gv1.CurrentRow.Cells(colbrkgType).Value = "Select"
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colAdjustmentType) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            Dim qry As String = "select distinct Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER"
                            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsCommon.ShowSelectForm("EmptyAdjMrpFinder", qry, "MRP", "Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and UOM = '" & clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) & "'", gv1.CurrentRow.Cells(colMRP).Value, "MRP", False))
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colbrkgType) Then
                            If clsCommon.CompairString(gv1.CurrentRow.Cells(colbrkgType).Value, "Select") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colbrkgQty).ReadOnly = True
                                gv1.CurrentRow.Cells(colbrkgQty).Value = 0
                                gv1.CurrentRow.Cells(colbrkgCost).Value = 0
                            Else
                                gv1.CurrentRow.Cells(colbrkgQty).ReadOnly = False
                            End If
                        ElseIf e.Column Is gv1.Columns(colbrkgQty) Then
                            gv1.CurrentRow.Cells(colbrkgCost).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colbrkgQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                        End If
                    End If
                    isCellValueChangedOpen = False
                    FunFbFcTotal()
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim strItemType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colAdjustmentType).Value)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItemEmpty(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
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
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("AdjEmptyUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) > 0 Then
            qry = "select top 1 Item_Basic_Net as MRP  from TSPL_ITEM_PRICE_MASTER where Item_Code ='" + strICode + "' and UOM='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value) + "'"
            gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
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
        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMRP).Value)
            Dim dblAmt As Double = dblQty * dblRate
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
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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
        whrclas = " Location_Type='Physical'  and GIT_Type='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("Location Code", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
        lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")
    End Sub

    Private Sub txtCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select Cust_Code ,Customer_Name, Case WHen OnHold='Y' Then 'Yes' Else 'No' End As [Full & Final Settlement Pending] from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("Customer Code", qry, "Cust_Code", "Status='N'", txtCustomer.Value, "", isButtonClicked)
        qry = "select Cust_Code,Customer_Name,Salesman_Code,Emp_Name as Salesman_Name from TSPL_CUSTOMER_MASTER left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.Salesman_Code where Cust_Code='" + txtCustomer.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblCustomer.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
        Else
            lblCustomer.Text = ""
            txtSalesman.Value = ""
            lblSalesman.Text = ""
        End If
    End Sub

    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "SELECT EMP_CODE ,Emp_Name  FROM TSPL_EMPLOYEE_MASTER"
        Dim whrclas As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("ADJEMSalesManCode", qry, "EMP_CODE", whrclas, txtSalesman.Value, "", isButtonClicked)
        lblSalesman.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name as [Salesman Name] FROM TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtSalesman.Value + "' ")
    End Sub

    Private Sub txtVehicleNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleNo._MYValidating
        Dim qry As String = "Select Vehicle_Id ,Number,Description,Model from TSPL_VEHICLE_MASTER"
        txtVehicleNo.Value = clsCommon.ShowSelectForm("EmptADjVehicleCode", qry, "Vehicle_Id", "", txtVehicleNo.Value, "", isButtonClicked)
        lblVehicle.Text = clsDBFuncationality.getSingleValue("Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleNo.Value + "' ")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        UpdateAllTotals()
        If rbtnRoute.IsChecked Then
            If clsCommon.myLen(cboRefDocument.SelectedValue) <= 0 Then
                cboRefDocument.Focus()
                Throw New Exception("Please select Reference Document Type")
            End If
            If clsCommon.myLen(txtReference.Text) <= 0 Then
                txtReference.Focus()
                Throw New Exception("Please select Reference No")
            End If
            If refDocDate > txtDate.Value.Date Then
                Throw New Exception("Reference Document Date is '" + clsCommon.GetPrintDate(refDocDate, "dd/MMM/yyyy") + "', And Adjustment Date is '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'")
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If

        Dim dbltotalCost As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim strMRP As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                If Not clsCommon.CompairString(gv1.Rows(ii).Cells(colbrkgType).Value, "Select") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colbrkgQty).Value) <= 0 Then
                        Throw New Exception("Please enter 'Breakage Qty' against item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                End If
                dbltotalCost += clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)


                If clsCommon.CompairString(clsCommon.myCstr(cboTransType.SelectedValue), "Out") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strUOM, "EB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "EC") = CompairStringResult.Equal Then
                        Dim dblBalQty As Double = Math.Round(clsItemLocationDetails.getBalanceWithUnapproveEmpty(strICode, txtLocation.Value, strMRP, strUOM, txtAdjustmentNo.Value, txtDate.Value), 2, MidpointRounding.ToEven)
                        If dblQty > dblBalQty Then
                            Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Actual Balance Quantity - " + clsCommon.myCstr(dblBalQty) + " at Row No " + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            End If
        Next
        '-------This validation for Only GUNTUR.......
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
            If rbtnRoute.IsChecked AndAlso clsCommon.CompairString(clsCommon.myCstr(cboRefDocument.SelectedValue), "Sale Invoice") = CompairStringResult.Equal Then
                Dim EmptyValue As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Empty_Value from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + txtDocumentNo.Value + "'"))
                If dbltotalCost > EmptyValue Then
                    Throw New Exception("Total Empty value of Invoice is " + clsCommon.myCstr(EmptyValue) + " and Document Total Cost " + clsCommon.myCstr(dbltotalCost))
                End If
            End If
        End If
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
                obj.Reference_Document = clsCommon.myCstr(cboRefDocument.SelectedValue)
                obj.Document_No = txtDocumentNo.Value
                obj.Unit_Code = "ALL"
                obj.ItemType = "E"
                obj.EMP_CODE = txtSalesman.Value
                obj.EMP_NAME = lblSalesman.Text
                obj.Customer_CODE = txtCustomer.Value
                obj.Customer_NAME = lblCustomer.Text

                obj.Vehicle_Code = txtVehicleNo.Value
                obj.Vehicle_No = lblVehicle.Text
                obj.Challan_No = txtChallanNo.Text

                If txtChallanDate.Checked Then
                    obj.Challan_date = txtChallanDate.Value
                End If

                obj.GateEntry_No = txtGENo.Text
                If txtGEDate.Checked Then
                    obj.GateEntry_Date = txtGEDate.Value
                End If
                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Trans_Type = clsCommon.myCstr(cboTransType.SelectedValue)
                If rbtnStock.IsChecked Then
                    obj.Stock_Type = "E"
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
                    'objTr.MFG_Date =
                    'objTr.Batch_No=
                    'objTr.Expiry_Date =
                    objTr.BreakageType = clsCommon.myCstr(grow.Cells(colbrkgType).Value)
                    objTr.Breakage = clsCommon.myCdbl(grow.Cells(colbrkgQty).Value)
                    objTr.Breakage_Cost = clsCommon.myCdbl(grow.Cells(colbrkgCost).Value)
                    objTr.ItemType = "E"
                    'objTr.LeakageQty =
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
                If clsCommon.myLen(obj.Reference_Document) > 0 Then
                    rbtnRoute.IsChecked = True
                Else
                    rbtnDepot.IsChecked = True
                End If
                If clsCommon.CompairString(obj.Stock_Type, "E") = CompairStringResult.Equal Then
                    rbtnStock.IsChecked = True
                End If

                txtAdjustmentNo.Value = obj.Adjustment_No
                txtDate.Value = obj.Adjustment_Date
                'obj.Posting_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                'obj.Posted()
                cboRefDocument.SelectedValue = obj.Reference_Document
                txtDocumentNo.Value = obj.Document_No
                'obj.Unit_Code = "ALL"
                'obj.ItemType = "E"
                txtSalesman.Value = obj.EMP_CODE
                lblSalesman.Text = obj.EMP_NAME

                txtCustomer.Value = obj.Customer_CODE
                lblCustomer.Text = obj.Customer_NAME

                txtVehicleNo.Value = obj.Vehicle_Code
                lblVehicle.Text = obj.Vehicle_No
                txtChallanNo.Text = obj.Challan_No

                If obj.Challan_date IsNot Nothing Then
                    txtChallanDate.Value = obj.Challan_date
                    txtChallanDate.Checked = True
                End If

                txtGENo.Text = obj.GateEntry_No
                If obj.GateEntry_Date IsNot Nothing Then
                    txtGEDate.Value = obj.GateEntry_Date
                    txtGEDate.Checked = True
                End If
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                cboTransType.SelectedValue = obj.Trans_Type

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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbrkgType).Value = objTr.BreakageType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbrkgQty).Value = objTr.Breakage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbrkgCost).Value = objTr.Breakage_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                    Next
                    FunFbFcTotal()
                    If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAdjustmentType).Value = RowTypeAdjustmentBoth
                    End If
                End If
                If rbtnRoute.IsChecked AndAlso clsCommon.CompairString(clsCommon.myCstr(cboRefDocument.SelectedValue), "Load out/Transfer") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Dim refDocDate As Date
    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(cboRefDocument.SelectedValue) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Reference Document", Me.Text)
            txtDocumentNo.Value = ""
            cboRefDocument.Focus()
            Exit Sub
        End If
        Dim AdjustmentDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
        txtCustomer.Value = ""
        lblCustomer.Text = ""
        txtVehicleNo.Value = ""
        lblVehicle.Text = ""
        txtSalesman.Value = ""
        lblSalesman.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""

        Dim qry As String = ""
        Dim whrclas As String = ""
        Dim orderBy As String = ""
        Dim Name As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboRefDocument.SelectedValue), "Sale Invoice") = CompairStringResult.Equal Then
            qry = "select Sale_Invoice_No as InvoiceNo,CONVERT(varchar(30), Sale_Invoice_Date,103) as [Sale Invoice Date], Balance_Amt  as [Balance Amount] from TSPL_SALE_INVOICE_HEAD "
            whrclas = " Sale_Invoice_No Not in (Select Document_No from TSPL_ADJUSTMENT_HEADER WHERE ItemType='E') AND Sale_Invoice_Date<'" + AdjustmentDate + "' AND Is_Post='Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas += " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            orderBy = " Sale_Invoice_ID desc "
            Name = "SI"
            txtDocumentNo.Value = clsCommon.ShowSelectForm("AdjDocumentNo" + Name + "", qry, "InvoiceNo", whrclas, txtDocumentNo.Value, orderBy, isButtonClicked)
            Dim StrQ As String = "SELECT Location,Vehicle_Code ,S.Salesman_Code,S.Cust_Code, S.Sale_Invoice_Date, L.Location_Desc ,V.Number ,E.Emp_Name ,C.Customer_Name  FROM TSPL_SALE_INVOICE_HEAD S inner join TSPL_LOCATION_MASTER L on S.Location=L.Location_Code inner join TSPL_CUSTOMER_MASTER C on S.Cust_Code =C.Cust_Code inner join TSPL_EMPLOYEE_MASTER E on S.Salesman_Code =E.EMP_CODE inner join TSPL_VEHICLE_MASTER V on S.Vehicle_Code = V.Vehicle_Id  WHERE S.Sale_Invoice_No ='" + txtDocumentNo.Value + "'"
            Dim Dt As DataTable = clsDBFuncationality.GetDataTable(StrQ)
            If Dt.Rows.Count > 0 Then
                txtCustomer.Value = clsCommon.myCstr(Dt.Rows(0).Item("Cust_Code"))
                lblCustomer.Text = clsCommon.myCstr(Dt.Rows(0).Item("Customer_Name"))
                txtVehicleNo.Value = clsCommon.myCstr(Dt.Rows(0).Item("Vehicle_Code"))
                lblVehicle.Text = clsCommon.myCstr(Dt.Rows(0).Item("Number"))
                txtSalesman.Value = clsCommon.myCstr(Dt.Rows(0).Item("Salesman_Code"))
                lblSalesman.Text = clsCommon.myCstr(Dt.Rows(0).Item("Emp_Name"))
                txtLocation.Value = clsCommon.myCstr(Dt.Rows(0).Item("Location"))
                lblLocation.Text = clsCommon.myCstr(Dt.Rows(0).Item("Location_Desc"))
                refDocDate = clsCommon.myCDate(Dt.Rows(0).Item("Sale_Invoice_Date"))
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboRefDocument.SelectedValue), "Load out/Transfer") = CompairStringResult.Equal Then
            qry = "select Transfer_No as Code, convert (varchar(11),Transfer_Date,103) as [Transfer Date], Reference_Doc_No as [Reference Document], Salesmancode as [Salesman] from tspl_transfer_head "
            whrclas = " Post='Y' AND Transfer_Date<'" + AdjustmentDate + "' AND To_Location  in (select Location_Code from TSPL_LOCATION_MASTER where Location_Type ='logical')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas += " and To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            orderBy = " Transfer_No desc"
            Name = "LO"
            txtDocumentNo.Value = clsCommon.ShowSelectForm("AdjDocumentNo" + Name + "", qry, "Code", whrclas, txtDocumentNo.Value, orderBy, isButtonClicked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT Salesmancode, dbo.TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesmanDesc, TSPL_TRANSFER_HEAD.Transfer_Date, From_Location AS Location, FromLoc_Desc AS LocDesc FROM dbo.TSPL_TRANSFER_HEAD LEFT OUTER JOIN dbo.TSPL_EMPLOYEE_MASTER ON dbo.TSPL_TRANSFER_HEAD.Salesmancode=dbo.TSPL_EMPLOYEE_MASTER.EMP_CODE WHERE dbo.TSPL_TRANSFER_HEAD.Transfer_No='" + txtDocumentNo.Value + "'")
            If dt.Rows.Count > 0 Then
                txtSalesman.Value = clsCommon.myCstr(dt.Rows(0).Item("Salesmancode"))
                lblSalesman.Text = clsCommon.myCstr(dt.Rows(0).Item("SalesmanDesc"))
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0).Item("Location"))
                lblLocation.Text = clsCommon.myCstr(dt.Rows(0).Item("LocDesc"))
                refDocDate = clsCommon.myCDate(dt.Rows(0).Item("Transfer_Date"))
            End If
        End If
    End Sub

    Private Sub txtDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesc.TextChanged

    End Sub

    Private Sub cboRefDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboRefDocument.SelectedIndexChanged

    End Sub

    Private Sub rbtnRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnRoute.ToggleStateChanged, rbtnDepot.ToggleStateChanged
        pnlInvoiceDetails.Enabled = rbtnDepot.IsChecked
        cboRefDocument.SelectedValue = ""
        txtDocumentNo.Value = ""
        cboRefDocument.Enabled = Not rbtnDepot.IsChecked
        txtDocumentNo.Enabled = Not rbtnDepot.IsChecked
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
        Dim qry As String = "SELECT Adjustment_No AS [AdjustmentNumber], Adjustment_Date as [Date], Document_No,Reference_Document as Type,Trans_Type as [Trans Type],case when  ItemType='E' then 'Empty' when ItemType='FM' then 'FG Manufacturing' when ItemType='FT' then 'FG Trading' when ItemType='RM' then 'Raw Material' when ItemType='OT' then 'Others'  end as [Item Type],case when Posted='Y' then 'Yes' else 'No' end as Posted, EMP_NAME as [Salesman], Customer_NAME as [Customer], Vehicle_No as [Vehicle No], Challan_No as [Challan No], GateEntry_No as [Gate No],Loc_Code as [Location] FROM  TSPL_ADJUSTMENT_HEADER  "
        Dim whrClas As String = " 1=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        whrClas += " AND ItemType='E'"


        txtAdjustmentNo.Value = clsCommon.ShowSelectForm("AdjustmentNumberEmp", qry, "AdjustmentNumber", whrClas, txtAdjustmentNo.Value, "AdjustmentNumber", isButtonClicked)
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

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Purchase Order No not found to Print", Me.Text)
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Adjustment No not found to Print")
            End If

            Dim qry As String = "select TSPL_ADJUSTMENT_DETAIL.Adjustment_Type  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Adjustment_No='" + txtAdjustmentNo.Value + "' and TSPL_ADJUSTMENT_HEADER.ItemType='E' and TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Sub PrintData(ByVal isPrePrint As Boolean)
        Try
            If clsCommon.myLen(txtAdjustmentNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            'ClsAdjustments.PrintData(txtAdjustmentNo.Value, isPrePrint, True)
            PrintData(txtAdjustmentNo.Value, isPrePrint, True)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    'richa agarwal 06/07/2015
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
    ''--------------------------
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        PrintData(False)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        PrintData(True)
    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Try
            ExportEmptyEntry()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub ExportEmptyEntry()
        Try
            Dim qryExport As String
            qryExport = " Select '' as [Customer], '' as [Location], '' [Date], 0 as [Amount], '' as [Type (I / D)] "
            transportSql.ExporttoExcel(qryExport, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Empty")
        End Try
    End Sub

    Private Sub mtbnImportOPBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtbnImportOPBalance.Click
        Try
            If ImportBalance() Then
                clsCommon.MyMessageBoxShow(Me, "Data imported successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ImportBalance() As Boolean
        Dim gv1 As New RadGridView()
        Dim isSaved As Boolean = True
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If transportSql.importExcel(gv1, "Customer", "Location", "Date", "Amount", "Type (I / D)") Then

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New ClsAdjustments()
                    obj.Customer_CODE = clsCommon.myCstr(grow.Cells("Customer").Value)
                    If clsCommon.myLen(obj.Customer_CODE) > 0 Then
                        obj.Customer_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_CODE + "'", trans))
                        If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                            Throw New Exception("Customer at Line No '" + LineNo + "' does not exist in Master")
                        End If

                        obj.Customer_NAME = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Customer_CODE + "'", trans))

                        '-----------------Location--------------------
                        obj.Loc_Code = clsCommon.myCstr(grow.Cells("Location").Value)
                        If clsCommon.myLen(obj.Loc_Code) > 0 Then
                            obj.Loc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_LOCATION_MASTER Where location_Code='" + obj.Loc_Code + "'", trans))
                            If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                                Throw New Exception("Location at Line No '" + LineNo + "' does not exist in Master")
                            End If
                        Else
                            Throw New Exception("Please insert 'Location' at Line No '" + LineNo + "' ")
                        End If

                        obj.Loc_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Loc_Code + "'", trans))
                        '---------------------------------------------

                        Dim AdjDate As String = clsCommon.myCstr(grow.Cells("Date").Value)
                        If clsCommon.myLen(AdjDate) < 10 Then
                            Throw New Exception("Please insert 'Date' in correct format (dd/MM/yyyy) at Line No '" + LineNo + "'")
                        End If
                        obj.Adjustment_Date = clsCommon.GetPrintDate(AdjDate, "dd/MMM/yyyy")

                        Dim Amt As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                        If Amt <= 0 Then
                            Throw New Exception("Please insert 'Amount' at Line No '" + LineNo + "'")
                        End If

                        Dim qry As String = "Select Adjustment_No from TSPL_ADJUSTMENT_HEADER WHERE Is_Imported=1 AND Customer_CODE='" + obj.Customer_CODE + "' AND Loc_Code = '" + obj.Loc_Code + "'"
                        obj.Adjustment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(obj.Adjustment_No) <= 0 Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If

                        obj.Reference = ""
                        obj.Description = "Closing balance of Empty (Customer)"
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = "E"
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.Challan_date = Nothing
                        obj.GateEntry_No = ""
                        obj.GateEntry_Date = Nothing

                        obj.Trans_Type = clsCommon.myCstr(grow.Cells("Type (I / D)").Value)
                        If clsCommon.CompairString(obj.Trans_Type, "I") = CompairStringResult.Equal Then
                            obj.Trans_Type = "In"
                        ElseIf clsCommon.CompairString(obj.Trans_Type, "D") = CompairStringResult.Equal Then
                            obj.Trans_Type = "Out"
                        Else
                            Throw New Exception("Please enter Type as 'I or D' at Line No '" + LineNo + "'")
                        End If

                        obj.Is_Imported = 1

                        obj.Arr = New List(Of ClsAdjustmentsDetails)()
                        Dim objTr As New ClsAdjustmentsDetails()
                        objTr.Adjustment_Line_No = 1
                        objTr.Item_Code = "PC300BFC"
                        objTr.Item_Description = "PEPSI COLA 300 ML BOTTLE FULL CASE"

                        If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                            objTr.Adjustment_Type = "CI"
                        ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                            objTr.Adjustment_Type = "CD"
                        End If

                        objTr.Item_Quantity = 0
                        objTr.Item_Cost = Amt
                        objTr.Unit_Code = "FC"
                        objTr.Remarks = ""
                        objTr.Comments = ""
                        objTr.mrp = 220
                        objTr.BreakageType = ""
                        objTr.Breakage = 0
                        objTr.Breakage_Cost = 0
                        objTr.ItemType = "E"
                        obj.Arr.Add(objTr)

                        obj.SaveData(obj, isNewEntry, "", trans)
                    End If
                Next
                trans.Commit()
                Return True
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


    Private Sub rbtnStock_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnStock.ToggleStateChanged
        pnlInvoiceDetails.Enabled = True
        cboRefDocument.SelectedValue = ""
        txtDocumentNo.Value = ""
        cboRefDocument.Enabled = False
        txtDocumentNo.Enabled = False
        txtLocation.Enabled = True
        txtCustomer.Enabled = False
        txtSalesman.Enabled = False
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Delete"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If ClsAdjustments.ReverseAndUnpost(txtAdjustmentNo.Value, trans) Then
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    trans.Commit()
                    LoadData(txtAdjustmentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
