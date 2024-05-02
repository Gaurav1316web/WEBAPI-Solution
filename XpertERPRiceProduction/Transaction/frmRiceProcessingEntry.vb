'======================BM00000004330
Imports common
Imports System.Data.SqlClient

Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmRiceProcessingEntry
    Inherits FrmMainTranScreen

#Region "variables"
    Public strDocumentNo As String = ""
    Dim ReportID As String = "RICE-PROC"
    Dim Errorcontrol As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim ButtonToolTip As New ToolTip()

    Const colLineno As String = "Lineno"
    Const colBOMCode As String = "BOMCode"
    Const colBOMDesc As String = "BOMDesc"
    Const colItemCode As String = "Icode"
    Const colItemName As String = "Iname"
    Const colItemUom As String = "IUOM"
    Const colItemType As String = "ItemType"
    Const colQty As String = "Qty"
    Const colStockBal As String = "StockQty"
    Const colLotNo As String = "LotNo"
    Const colBalance As String = "balance"
    Const colCost As String = "COst"
    Const colValue As String = "ItemValue"
    Const colRemarks As String = "Remarks"

    '==============
    Const colFLineno As String = "FLineno"
    Const colFItemCode As String = "FIcode"
    Const colFItemName As String = "FIname"
    Const colFItemUom As String = "FIUOM"
    Const colFPriniciple As String = "FPrinci"
    Const colFItemType As String = "FItemType"
    Const colFQty As String = "FQty"
    Const colFQty_Pers As String = "Fqtypers"
    Const colFCost As String = "FCOst"
    Const colFValue As String = "FItemValue"
    Const colFRemarks As String = "FRemarks"
    Const colFLotNo As String = "FLotNo"
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRiceProcessingEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub Funreset()
        txtCode.Value = ""
        txtto_loc_code.Value = ""
        txtto_loc_desc.Text = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDescription.Text = ""
        txtfrm_loc_code.Value = ""
        txtfrm_loc_name.Text = ""
        txtprocess_charge.Text = ""
        txtprocess_cost.Text = ""
        txtadmin_charge.Text = ""
        txtadmin_cost.Text = ""
        txttotal_cost.Text = ""
        txteffective_cost.Text = ""

        gv.Rows.Clear()
        gv.Rows.AddNew()

        gv_finish.Rows.Clear()

        isNewEntry = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        UsLock1.Status = ERPTransactionStatus.Pending

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        txtDescription.Select()
        txtDescription.Focus()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub FrmRiceProcessingEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            Funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        ElseIf e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.C Then
            CalculateOnShortCut()
        End If
    End Sub

    Private Sub FrmRiceProcessingEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadFinishGrid()
        Funreset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save/update record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoline As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colLineno
        repoline.HeaderText = "S.No."
        repoline.Width = 60
        repoline.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoline)

        Dim repolinebom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolinebom.FormatString = ""
        repolinebom.Name = colBOMCode
        repolinebom.HeaderText = "BOM Code"
        repolinebom.Width = 60
        repolinebom.HeaderImage = My.Resources.search4
        repolinebom.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repolinebom)

        Dim repolinebom1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolinebom1.FormatString = ""
        repolinebom1.Name = colBOMDesc
        repolinebom1.HeaderText = "BOM Description"
        repolinebom1.Width = 60
        repolinebom1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repolinebom1)

        Dim repolin As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolin.FormatString = ""
        repolin.Name = colBalance
        repolin.HeaderText = "Balance"
        repolin.Width = 60
        repolin.IsVisible = False
        gv.MasterTemplate.Columns.Add(repolin)

        Dim repoline1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline1.FormatString = ""
        repoline1.Name = colItemCode
        repoline1.HeaderText = "Item Code"
        repoline1.Width = 110
        repoline1.ReadOnly = True
        'repoline1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoline1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoline1)

        Dim repoline2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline2.FormatString = ""
        repoline2.Name = colItemName
        repoline2.HeaderText = "Description"
        repoline2.Width = 210
        repoline2.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoline2)

        Dim repoline3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline3.FormatString = ""
        repoline3.Name = colItemType
        repoline3.HeaderText = "Type"
        repoline3.Width = 100
        repoline3.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoline3)

        Dim repoline4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline4.FormatString = ""
        repoline4.Name = colItemUom
        repoline4.HeaderText = "Item Unit"
        repoline4.Width = 80
        repoline4.ReadOnly = True
        'repoline4.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoline4.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoline4)

        Dim repoline5111 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline5111.FormatString = ""
        repoline5111.Name = colLotNo
        repoline5111.HeaderText = "Lot No."
        repoline5111.Width = 80
        repoline5111.ReadOnly = False
        repoline5111.HeaderImage = My.Resources.search4
        repoline5111.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoline5111)

        Dim repoline512 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline512.FormatString = ""
        repoline512.Name = colStockBal
        repoline512.HeaderText = "Stock Quantity"
        repoline512.Width = 80
        repoline512.DecimalPlaces = 2
        repoline512.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoline512)

        Dim repoline5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline5.FormatString = ""
        repoline5.Name = colQty
        repoline5.HeaderText = "Quantity"
        repoline5.Width = 80
        repoline5.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoline5)

        Dim repoline7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline7.FormatString = ""
        repoline7.Name = colCost
        repoline7.HeaderText = "Item Cost"
        repoline7.Width = 80
        repoline7.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoline7)

        Dim repoline8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline8.FormatString = ""
        repoline8.Name = colValue
        repoline8.HeaderText = "Total Cost"
        repoline8.Width = 100
        repoline8.ReadOnly = True
        repoline8.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoline8)

        Dim repoline81 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline81.FormatString = ""
        repoline81.Name = colRemarks
        repoline81.HeaderText = "Remarks"
        repoline81.Width = 100
        repoline81.MaxLength = 100
        gv.MasterTemplate.Columns.Add(repoline81)

        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.AllowDeleteRow = True

        ReStoreGridLayout()
    End Sub

    Private Sub LoadFinishGrid()
        gv_finish.Rows.Clear()
        gv_finish.Columns.Clear()

        Dim repoline As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colFLineno
        repoline.HeaderText = "S.No."
        repoline.Width = 60
        repoline.ReadOnly = True
        gv_finish.MasterTemplate.Columns.Add(repoline)

        Dim repoline1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline1.FormatString = ""
        repoline1.Name = colFItemCode
        repoline1.HeaderText = "Item Code"
        repoline1.Width = 110
        repoline1.ReadOnly = True
        'repoline1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoline1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_finish.MasterTemplate.Columns.Add(repoline1)

        Dim repoline2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline2.FormatString = ""
        repoline2.Name = colFItemName
        repoline2.HeaderText = "Description"
        repoline2.Width = 210
        repoline2.ReadOnly = True
        gv_finish.MasterTemplate.Columns.Add(repoline2)

        Dim repoline3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline3.FormatString = ""
        repoline3.Name = colFItemType
        repoline3.HeaderText = "Type"
        repoline3.Width = 100
        repoline3.ReadOnly = True
        gv_finish.MasterTemplate.Columns.Add(repoline3)

        Dim repoline4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline4.FormatString = ""
        repoline4.Name = colFItemUom
        repoline4.HeaderText = "Item Unit"
        repoline4.Width = 80
        repoline4.ReadOnly = True
        'repoline4.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoline4.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_finish.MasterTemplate.Columns.Add(repoline4)

        Dim repoprinci As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoprinci.FormatString = ""
        repoprinci.Name = colFPriniciple
        repoprinci.HeaderText = "Principle Item"
        repoprinci.IsVisible = False
        repoprinci.Width = 60
        gv_finish.MasterTemplate.Columns.Add(repoprinci)

        Dim repoline5per As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline5per.FormatString = ""
        repoline5per.Name = colFQty_Pers
        repoline5per.HeaderText = "Percentage"
        repoline5per.Width = 80
        repoline5per.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline5per)

        Dim repoline5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline5.FormatString = ""
        repoline5.Name = colFQty
        repoline5.HeaderText = "Quantity"
        repoline5.Width = 80
        repoline5.DecimalPlaces = 2
        repoline5.ReadOnly = True
        gv_finish.MasterTemplate.Columns.Add(repoline5)

        Dim repoline723 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline723.FormatString = ""
        repoline723.Name = colFLotNo
        repoline723.HeaderText = "Lot No"
        repoline723.Width = 80
        gv_finish.MasterTemplate.Columns.Add(repoline723)

        Dim repoline7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline7.FormatString = ""
        repoline7.Name = colFCost
        repoline7.HeaderText = "Item Cost"
        repoline7.Width = 80
        repoline7.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline7)

        Dim repoline8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline8.FormatString = ""
        repoline8.Name = colFValue
        repoline8.HeaderText = "Total Value"
        repoline8.Width = 100
        repoline8.ReadOnly = True
        repoline8.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline8)

        Dim repoline811 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline811.FormatString = ""
        repoline811.Name = colFRemarks
        repoline811.HeaderText = "Remarks"
        repoline811.Width = 100
        repoline811.MaxLength = 100
        gv_finish.MasterTemplate.Columns.Add(repoline811)

        gv_finish.AllowAddNewRow = False
        gv_finish.ShowGroupPanel = False
        gv_finish.AllowColumnReorder = True
        gv_finish.AllowRowReorder = False
        gv_finish.EnableSorting = False
        gv_finish.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_finish.MasterTemplate.ShowRowHeaderColumn = False
        gv_finish.TableElement.TableHeaderHeight = 40
        gv_finish.AllowDeleteRow = True

        ReStoreGridLayout()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Funreset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtfrm_loc_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtfrm_loc_code.Select()
                txtfrm_loc_code.Focus()
                Errorcontrol.SetError(txtfrm_loc_name, "Select From location.")
                Throw New Exception("Select From location.")
            Else
                Errorcontrol.ResetError(txtfrm_loc_name)
            End If

            If clsCommon.myLen(txtto_loc_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtto_loc_code.Select()
                txtto_loc_code.Focus()
                Errorcontrol.SetError(txtto_loc_desc, "Select To location.")
                Throw New Exception("Select To location.")
            Else
                Errorcontrol.ResetError(txtto_loc_desc)
            End If

            If clsCommon.myCdbl(txtprocess_charge.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtprocess_charge.Select()
                txtprocess_charge.Focus()
                Errorcontrol.SetError(txtprocess_charge, "Fill processing charge.")
                Throw New Exception("Fill processing charge.")
            Else
                Errorcontrol.ResetError(txtprocess_charge)
            End If

            If clsCommon.myCdbl(txtadmin_charge.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtadmin_charge.Select()
                txtadmin_charge.Focus()
                Errorcontrol.SetError(txtadmin_charge, "Fill admin charge.")
                Throw New Exception("Fill admin charge.")
            Else
                Errorcontrol.ResetError(txtadmin_charge)
            End If

            If clsCommon.myLen(gv.Rows(0).Cells(colBOMCode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select Bill of Material in grid.")
            End If

            Dim qty As Decimal = 0
            Dim balance As Decimal = 0
            Dim cost As Decimal = 0
            Dim qty_pers As Decimal = 0
            Dim stock_qty As Decimal = 0
            Dim lotno As String = ""

            For ii As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(ii).Cells(colBOMCode).Value) > 0 Then
                    qty = clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value)
                    balance = clsCommon.myCdbl(gv.Rows(ii).Cells(colBalance).Value)
                    cost = clsCommon.myCdbl(gv.Rows(ii).Cells(colCost).Value)
                    lotno = clsCommon.myCstr(gv.Rows(ii).Cells(colLotNo).Value)

                    If clsCommon.myLen(gv.Rows(ii).Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(lotno) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Fill item lot no. at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    End If

                    If qty > balance Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Item Code: " + clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value) + " having BOM balance qty " + clsCommon.myCstr(balance) + " at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    End If

                    If cost <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Fill item cost at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    End If

                    'stock_qty = clsRiceProcessingEntry.GetStockBalance(txtfrm_loc_code.Value, txtCode.Value, clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colItemUom).Value))
                    stock_qty = clsCommon.myCdbl(gv.Rows(ii).Cells(colStockBal).Value)
                    If qty > stock_qty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Item Code: " + clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value) + " having Stock qty " + clsCommon.myCstr(stock_qty) + " at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    End If
                End If
            Next

            qty_pers = 0
            cost = 0
            For Each grow As GridViewRowInfo In gv_finish.Rows
                qty_pers += clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)
                cost += clsCommon.myCdbl(grow.Cells(colFValue).Value)

                If clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage3
                    Throw New Exception("Fill item percentage at row no. " + clsCommon.myCstr(grow.Index) + ".")
                End If
            Next

            If qty_pers > 100 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Item percentage should not exceed maximum 100.")
            End If

            Dim comprcost As Decimal = clsCommon.myCdbl(txttotal_cost.Text)

            If qty_pers < 100 Then
                comprcost = System.Math.Round((comprcost * qty_pers) / 100, 2)
            End If

            If cost > clsCommon.myCdbl(comprcost) OrElse cost < clsCommon.myCdbl(comprcost) Then
                RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Sum of Item Value should not exceed or less than Total cost i.e. " + clsCommon.myCstr(comprcost) + ".")
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub SaveData(ByVal is_Post As Boolean)
        Try
            Dim obj As New clsRiceProcessingEntry()

            obj.doccode = clsCommon.myCstr(txtCode.Value)
            obj.Docdate = clsCommon.myCDate(dtpDate.Text)
            obj.descrptn = clsCommon.myCstr(txtDescription.Text).Replace("'", "`")
            obj.frm_loc_code = clsCommon.myCstr(txtfrm_loc_code.Value)
            obj.process_Charge = clsCommon.myCdbl(txtprocess_charge.Text)
            obj.process_cost = clsCommon.myCdbl(txtprocess_cost.Text)
            obj.admin_charge = clsCommon.myCdbl(txtadmin_charge.Text)
            obj.admin_cost = clsCommon.myCdbl(txtadmin_cost.Text)
            obj.total_cost = clsCommon.myCdbl(txttotal_cost.Text)
            obj.effective_cost = clsCommon.myCdbl(txteffective_cost.Text)
            obj.to_loc_code = clsCommon.myCstr(txtto_loc_code.Value)

            obj.Arr = New List(Of clsRiceProcessingDetail)
            obj.Arr_FInish = New List(Of clsRiceProcessingFinishDetail)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsRiceProcessingDetail()

                objtr.bom_code = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                objtr.Lineno = clsCommon.myCdbl(grow.Cells(colLineno).Value)
                objtr.Itemcode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(colItemUom).Value)
                objtr.cost = clsCommon.myCstr(grow.Cells(colCost).Value)
                objtr.value = clsCommon.myCdbl(grow.Cells(colValue).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                objtr.qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objtr.Lot_no = clsCommon.myCstr(grow.Cells(colLotNo).Value)
                objtr.balance = clsCommon.myCdbl(grow.Cells(colBalance).Value)

                If clsCommon.myLen(objtr.Itemcode) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            For Each grow As GridViewRowInfo In gv_finish.Rows
                Dim objtr As New clsRiceProcessingFinishDetail()

                objtr.Lineno = clsCommon.myCdbl(grow.Cells(colFLineno).Value)
                objtr.Itemcode = clsCommon.myCstr(grow.Cells(colFItemCode).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(colFItemUom).Value)
                objtr.cost = clsCommon.myCstr(grow.Cells(colFCost).Value)
                objtr.value = clsCommon.myCdbl(grow.Cells(colFValue).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colFRemarks).Value)
                objtr.lot_no = clsCommon.myCstr(grow.Cells(colFLotNo).Value)
                objtr.qty = clsCommon.myCdbl(grow.Cells(colFQty).Value)
                objtr.qty_pers = clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)
                objtr.is_Prinicple = CInt(IIf(clsCommon.myCBool(grow.Cells(colFPriniciple).Value) = True, 1, 0))

                If clsCommon.myLen(objtr.Itemcode) > 0 Then
                    obj.Arr_FInish.Add(objtr)
                End If
            Next

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsRiceProcessingEntry.SaveData(obj, isNewEntry, trans) Then
                If Not is_Post Then
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If
                txtCode.Value = obj.doccode
                UcAttachment1.SaveData(txtCode.Value)

                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsRiceProcessingEntry = clsRiceProcessingEntry.GetData(strCode, NavType)

            isInsideLoadData = True
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.doccode) > 0 Then
                txtCode.Value = obj.doccode
                dtpDate.Text = obj.Docdate
                txtDescription.Text = obj.descrptn
                txtfrm_loc_code.Value = obj.frm_loc_code
                txtfrm_loc_name.Text = obj.frm_loc_desc
                txtto_loc_code.Value = obj.to_loc_code
                txtto_loc_desc.Text = obj.to_loc_desc
                txtprocess_charge.Text = obj.process_Charge
                txtprocess_cost.Text = obj.process_cost
                txtadmin_charge.Text = obj.admin_charge
                txtadmin_cost.Text = obj.admin_cost
                txttotal_cost.Text = obj.total_cost
                txteffective_cost.Text = obj.effective_cost

                gv.Rows.Clear()
                gv_finish.Rows.Clear()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsRiceProcessingDetail In obj.Arr
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = objtr.Lineno
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMCode).Value = objtr.bom_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colBOMDesc).Value = objtr.bom_desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Itemcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsItemMaster.GetItemName(objtr.Itemcode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsItemMaster.GetItemType(objtr.Itemcode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemUom).Value = objtr.uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colCost).Value = objtr.cost
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = objtr.value
                        gv.Rows(gv.Rows.Count - 1).Cells(colLotNo).Value = objtr.Lot_no
                        gv.Rows(gv.Rows.Count - 1).Cells(colStockBal).Value = clsRiceProcessingEntry.GetStockBalance(txtfrm_loc_code.Value, txtCode.Value, objtr.Itemcode, objtr.uom, objtr.Lot_no)
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colBalance).Value = objtr.balance
                    Next
                End If

                If obj.Arr_FInish IsNot Nothing AndAlso obj.Arr_FInish.Count > 0 Then
                    For Each objtr As clsRiceProcessingFinishDetail In obj.Arr_FInish
                        gv_finish.Rows.AddNew()

                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFLineno).Value = objtr.Lineno
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemCode).Value = objtr.Itemcode
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemName).Value = clsItemMaster.GetItemName(objtr.Itemcode, Nothing)
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemType).Value = clsItemMaster.GetItemType(objtr.Itemcode, Nothing)
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemUom).Value = objtr.uom
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFQty).Value = objtr.qty
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFCost).Value = objtr.cost
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFQty_Pers).Value = objtr.qty_pers
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFValue).Value = objtr.value
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFRemarks).Value = objtr.remarks
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFLotNo).Value = objtr.lot_no
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFPriniciple).Value = clsCommon.myCBool(IIf(objtr.is_Prinicple = 1, True, False))
                    Next
                End If

                isNewEntry = False
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnsave.Text = "Update"

                UsLock1.Status = ERPTransactionStatus.Pending

                If obj.is_post = 1 Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                UcAttachment1.LoadData(txtCode.Value)
                RadPageView1.SelectedPage = RadPageViewPage1
            Else
                Funreset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select document code first.", Me.Text)
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Select document code first.")
                Return
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsRiceProcessingEntry.DeleteData(txtCode.Value, trans) Then
                    myMessages.delete()

                    Funreset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select document code first.", Me.Text)
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Select document code first.")
                Return
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If myMessages.postConfirm() Then
                If AllowToSave() Then SaveData(True)

                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsRiceProcessingEntry.PostData(txtCode.Value, trans) Then
                    myMessages.post()

                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_RICE_PROCESSING_HEAD where doc_no='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsRiceProcessingEntry.GetFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtfrm_loc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtfrm_loc_code._MYValidating
        txtfrm_loc_code.Value = clsLocation.getFinder(" isnull(csa_type,'N')='N'", txtfrm_loc_code.Value, isButtonClicked)
        txtfrm_loc_name.Text = clsLocation.GetName(txtfrm_loc_code.Value, Nothing)
    End Sub

    Private Sub txtadmin_charge_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadmin_charge.TextChanged
        If clsCommon.myCdbl(txtadmin_charge.Text) > 0 Then
            CalculateCost()
            CalculateFinishGrid(True)
        End If
    End Sub

    Private Sub txtprocess_charge_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtprocess_charge.TextChanged
        If clsCommon.myCdbl(txtprocess_charge.Text) > 0 Then
            CalculateCost()
            CalculateFinishGrid(True)
        End If
    End Sub

    Private Sub CalculateCost()
        If isInsideLoadData Then
            Exit Sub
        End If
        Dim total_qty As Decimal = 0
        Dim total_totalcost As Decimal = 0

        For Each grow As GridViewRowInfo In gv.Rows
            total_qty += clsCommon.myCdbl(grow.Cells(colQty).Value)
            total_totalcost += clsCommon.myCdbl(grow.Cells(colValue).Value)
        Next

        txtprocess_cost.Text = clsCommon.myCdbl(txtprocess_charge.Text) * clsCommon.myCdbl(total_qty)
        txtadmin_cost.Text = clsCommon.myCdbl(txtadmin_charge.Text) * clsCommon.myCdbl(total_qty)
        txttotal_cost.Text = clsCommon.myCdbl(txtprocess_cost.Text) + clsCommon.myCdbl(txtadmin_cost.Text) + clsCommon.myCdbl(total_totalcost)
        If clsCommon.myCdbl(total_qty) > 0 Then
            txteffective_cost.Text = clsCommon.myCdbl(txttotal_cost.Text) / clsCommon.myCdbl(total_qty)
        Else
            txteffective_cost.Text = 0
        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv.Columns(colBOMCode) Then
                    isCellValueChanged = True
                    OpenBOM(False)
                    CalculateCost()
                    CalculateFinishGrid(False)
                    isCellValueChanged = False
                End If
                If e.Column Is gv.Columns(colLotNo) AndAlso clsCommon.myLen(gv.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    isCellValueChanged = True
                    OpenLotNo(False)
                    CalculateCost()
                    CalculateFinishGrid(False)
                    isCellValueChanged = False
                End If
                If e.Column Is gv.Columns(colCost) OrElse e.Column Is gv.Columns(colQty) Then
                    isCellValueChanged = True
                    gv.CurrentRow.Cells(colValue).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCost).Value)
                    CalculateCost()
                    CalculateFinishGrid(False)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub OpenLotNo(ByVal isButtonClicked As Boolean)
        Dim CF As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colItemUom).Value), Nothing)

        If CF <= 0 Then
            CF = 1
        End If

        Dim qry As String = "select aa.batch_no,sum(aa.Qty) as Qty,sum(aa.Basic_Cost)/count(aa.item_code) as Basic_Cost from ( "
        qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,round((isnull(TSPL_INVENTORY_MOVEMENT.Qty,0)*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Qty,TSPL_INVENTORY_MOVEMENT.UOM,round((TSPL_INVENTORY_MOVEMENT.Basic_Cost*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT left outer join tspl_item_uom_detail on TSPL_INVENTORY_MOVEMENT.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.uom_code=TSPL_INVENTORY_MOVEMENT.uom where TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.location_code='" + txtfrm_loc_code.Value + "' "
        qry += "union all "
        qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,round(((0-isnull(TSPL_INVENTORY_MOVEMENT.Qty,0))*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Qty,TSPL_INVENTORY_MOVEMENT.UOM,round((TSPL_INVENTORY_MOVEMENT.Basic_Cost*tspl_item_uom_detail.conversion_factor)/" + clsCommon.myCstr(CF) + ",2) as Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT left outer join tspl_item_uom_detail on TSPL_INVENTORY_MOVEMENT.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.uom_code=TSPL_INVENTORY_MOVEMENT.uom where TSPL_INVENTORY_MOVEMENT.InOut='O' and TSPL_INVENTORY_MOVEMENT.location_code='" + txtfrm_loc_code.Value + "' "
        qry += ")aa left outer join TSPL_ITEM_MASTER on aa.Item_Code=TSPL_ITEM_MASTER.Item_Code  "
        qry += " where aa.item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "' group by aa.batch_no"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("PRCFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colLotNo).Value = clsCommon.myCstr(dr("batch_no"))
            gv.CurrentRow.Cells(colStockBal).Value = clsCommon.myCdbl(dr("Qty"))
            gv.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(dr("Basic_Cost"))

            gv.CurrentRow.Cells(colValue).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCost).Value)

            CalculateCost()
            For Each grow As GridViewRowInfo In gv_finish.Rows
                grow.Cells(colFQty).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(0).Cells(colQty).Value) * clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)) / 100, 2)
                grow.Cells(colFCost).Value = System.Math.Round((clsCommon.myCdbl(txteffective_cost.Text) * clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)) / 100, 2)
            Next
            CalculateFinishGrid(False)
        Else
            gv.CurrentRow.Cells(colLotNo).Value = ""
            gv.CurrentRow.Cells(colStockBal).Value = 0
            gv.CurrentRow.Cells(colCost).Value = 0
        End If
    End Sub

    Private Sub OpenBOM(ByVal isButtonClicked As Boolean)
        Dim bomcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colBOMCode).Value)
        Dim qry As String = "select a1.BOM_CODE as Code,a1.DESCRIPTION,a1.REVISION_NO as [Revision No],a1.BOM_DATE as [Date],a1.PROD_ITEM_CODE as [Item Code],a1.Item_Desc as [Item Description],a1.PROD_ITEM_UNIT_CODE as [Unit],a1.qty as [Pending Quantity] from ( "
        qry += "select a.BOM_CODE,a.DESCRIPTION,a.REVISION_NO,a.BOM_DATE,a.PROD_ITEM_CODE,a.Item_Desc,a.PROD_ITEM_UNIT_CODE,sum(isnull(a.PROD_QUANTITY,0)) as qty from ( "
        qry += "select TSPL_MF_BOM_HEAD.BOM_CODE,DESCRIPTION,REVISION_NO,BOM_DATE,PROD_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,PROD_ITEM_UNIT_CODE,PROD_QUANTITY from TSPL_MF_BOM_HEAD left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_HEAD.PROD_ITEM_CODE "
        qry += " where TSPL_MF_BOM_HEAD.POSTED=1 and TSPL_MF_BOM_HEAD.trans_type='RICE' )a group by a.BOM_CODE,a.DESCRIPTION,a.REVISION_NO,a.BOM_DATE,a.PROD_ITEM_CODE,a.Item_Desc,a.PROD_ITEM_UNIT_CODE)a1 "
        qry += " "
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RiceBOMFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colBOMCode).Value = clsCommon.myCstr(dr("Code"))
            gv.CurrentRow.Cells(colBOMDesc).Value = clsCommon.myCstr(dr("description"))
            gv.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(dr("item code"))
            gv.CurrentRow.Cells(colItemName).Value = clsCommon.myCstr(dr("item description"))
            gv.CurrentRow.Cells(colItemType).Value = clsItemMaster.GetItemType(clsCommon.myCstr(dr("item code")), Nothing)
            gv.CurrentRow.Cells(colItemUom).Value = clsCommon.myCstr(dr("Unit"))
            gv.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(dr("Pending Quantity"))
            gv.CurrentRow.Cells(colBalance).Value = clsCommon.myCdbl(dr("Pending Quantity"))
            gv.CurrentRow.Cells(colCost).Value = Nothing
            gv.CurrentRow.Cells(colValue).Value = Nothing
            gv.CurrentRow.Cells(colRemarks).Value = ""
            If clsCommon.myCdbl(txtprocess_charge.Text) <= 0 Then
                txtprocess_charge.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Processing_Charge from tspl_mf_bom_head where bom_code='" + clsCommon.myCstr(dr("Code")) + "'"))
            End If
            If clsCommon.myCdbl(txtadmin_charge.Text) <= 0 Then
                txtadmin_charge.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Admin_Charge from tspl_mf_bom_head where bom_code='" + clsCommon.myCstr(dr("Code")) + "'"))
            End If
            FillFinishGoods()
        Else
            gv.CurrentRow.Cells(colBOMCode).Value = ""
            gv.CurrentRow.Cells(colBOMDesc).Value = ""
            gv.CurrentRow.Cells(colItemCode).Value = ""
            gv.CurrentRow.Cells(colItemName).Value = ""
            gv.CurrentRow.Cells(colItemType).Value = ""
            gv.CurrentRow.Cells(colItemUom).Value = ""
            gv.CurrentRow.Cells(colQty).Value = Nothing
            gv.CurrentRow.Cells(colBalance).Value = Nothing
            gv.CurrentRow.Cells(colCost).Value = Nothing
            gv.CurrentRow.Cells(colValue).Value = Nothing
            gv.CurrentRow.Cells(colRemarks).Value = ""
            gv_finish.Rows.Clear()
        End If
    End Sub

    Private Sub FillFinishGoods()
        gv_finish.Rows.Clear()
        Dim bomcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colBOMCode).Value)

        If clsCommon.myLen(bomcode) > 0 Then
            Dim qry As String = "select TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE,TSPL_MF_BOM_DETAIL.qty_pers,TSPL_MF_BOM_DETAIL.CONSM_QUANTITY,TSPL_MF_BOM_DETAIL.Is_Principle "
            qry += "from TSPL_MF_BOM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE where TSPL_MF_BOM_DETAIL.bom_code='" + bomcode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv_finish.Rows.AddNew()

                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFLineno).Value = clsCommon.myCstr(gv_finish.Rows.Count)
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemCode).Value = clsCommon.myCstr(dr("CONSM_ITEM_CODE"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemType).Value = clsCommon.myCstr(dr("Item_Type"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemUom).Value = clsCommon.myCstr(dr("CONSM_ITEM_UNIT_CODE"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFQty).Value = clsCommon.myCdbl(dr("CONSM_QUANTITY"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFQty_Pers).Value = clsCommon.myCdbl(dr("qty_pers"))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFPriniciple).Value = clsCommon.myCBool(IIf(CInt(dr("Is_Principle")) = 1, True, False))
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFCost).Value = Nothing
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFValue).Value = Nothing
                    gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFRemarks).Value = ""
                Next
            End If
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv.Rows.Count - 1 Then
                'gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_finish_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_finish.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv_finish.Columns(colFQty_Pers) OrElse e.Column Is gv_finish.Columns(colFCost) Then
                    isCellValueChanged = True
                    CalculateFinishGrid(True)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub CalculateFinishGrid(ByVal iscellchanged As Boolean)
        If isInsideLoadData Then
            Exit Sub
        End If
        Dim totalcost As Decimal = 0
        Dim XR As Integer = 0
        For Each grow As GridViewRowInfo In gv_finish.Rows
            grow.Cells(colFQty).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(0).Cells(colQty).Value) * clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)) / 100, 2)
            If clsCommon.myCdbl(grow.Cells(colFCost).Value) = 0 Then
                'grow.Cells(colFCost).Value = System.Math.Round((clsCommon.myCdbl(txteffective_cost.Text) * clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)) / 100, 2)
            End If
            totalcost += clsCommon.myCdbl(grow.Cells(colFCost).Value)

            grow.Cells(colFValue).Value = clsCommon.myCdbl(grow.Cells(colFCost).Value) * clsCommon.myCdbl(grow.Cells(colFQty).Value)
            XR += 1
        Next
    End Sub

    Private Sub CalculateOnShortCut()
        If isInsideLoadData Then
            Exit Sub
        End If

        gv_finish.CurrentRow = gv_finish.Rows(0)
        Dim totalcost As Decimal = 0
        Dim XR As Integer = 0
        Dim counter As Integer = 0
        Dim qty_pers As Decimal = 0

        For Each grow As GridViewRowInfo In gv_finish.Rows
            If clsCommon.myCdbl(grow.Cells(colFCost).Value) = 0 Then
                counter += 1
                XR = grow.Index
            End If

            qty_pers += clsCommon.myCdbl(grow.Cells(colFQty_Pers).Value)
            grow.Cells(colFValue).Value = clsCommon.myCdbl(grow.Cells(colFCost).Value) * clsCommon.myCdbl(grow.Cells(colFQty).Value)
            totalcost += clsCommon.myCdbl(grow.Cells(colFValue).Value)
        Next

        If counter > 1 Then
            clsCommon.MyMessageBoxShow("Only one item with 0(zero) cost allowed for auto calculation.")
            Exit Sub
        End If
        If counter < 1 Then
            clsCommon.MyMessageBoxShow("Set one item with 0(zero) cost for auto calculation.")
            Exit Sub
        End If

        Dim comprcost As Decimal = clsCommon.myCdbl(txttotal_cost.Text)
        If qty_pers > 100 Then
            RadPageView1.SelectedPage = RadPageViewPage2
            clsCommon.MyMessageBoxShow("Total Percentage should not exceed max. 100.")
            Exit Sub
        Else
            comprcost = System.Math.Round((comprcost * qty_pers) / 100, 2)
        End If

        If clsCommon.myCdbl(totalcost) > 0 AndAlso clsCommon.myCdbl(comprcost) < clsCommon.myCdbl(totalcost) Then
            gv_finish.Rows(XR).Cells(colFValue).Value = 0 ' totalcost - comprcost
            gv_finish.Rows(XR).Cells(colFCost).Value = System.Math.Round(clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFValue).Value) / clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFQty).Value), 2)
            clsCommon.MyMessageBoxShow("Total Value should exceed max. " + clsCommon.myCstr(comprcost) + ".")
            Exit Sub
        End If

        If clsCommon.myCdbl(totalcost) > 0 AndAlso totalcost < clsCommon.myCdbl(comprcost) Then
            gv_finish.Rows(XR).Cells(colFValue).Value = comprcost - totalcost
            gv_finish.Rows(XR).Cells(colFCost).Value = System.Math.Round(clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFValue).Value) / clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFQty).Value), 2)
        End If

        clsCommon.MyMessageBoxShow("Done", Me.Text)
    End Sub

    Private Sub txtto_loc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtto_loc_code._MYValidating
        txtto_loc_code.Value = clsLocation.getFinder(" isnull(csa_type,'N')='N'", txtto_loc_code.Value, isButtonClicked)
        txtto_loc_desc.Text = clsLocation.GetName(txtto_loc_code.Value, Nothing)
    End Sub

    Private Sub btndeletelayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(ReportID + "GV", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVFINISH", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GV", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                '===============finish
                obj = New clsGridLayout()
                obj = CType(obj.GetData(ReportID + "GVFINISH", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv_finish.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv_finish.Columns.Count - 1 Step ii + 1
                        gv_finish.Columns(ii).IsVisible = False
                        gv_finish.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv_finish.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnsavelayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavelayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID + "GV"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
            End If
            '==========finish grid==============
            gv_finish.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID + "GVFINISH"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv_finish.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv_finish.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
End Class
