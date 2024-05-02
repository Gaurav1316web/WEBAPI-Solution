'======================BM00000004329
Imports common
Imports System.Data.SqlClient

Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmRiceMixingEntry
    Inherits FrmMainTranScreen

#Region "variables"
    Public strDocumentNo As String = ""
    Dim ReportID As String = "RICE-MIX"
    Dim Errorcontrol As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim ButtonToolTip As New ToolTip()

    Const colLineno As String = "Lineno"
    Const colItemCode As String = "Icode"
    Const colItemName As String = "Iname"
    Const colItemUom As String = "IUOM"
    Const colItemType As String = "ItemType"
    Const colQty As String = "Qty"
    Const colBalance As String = "balance"
    Const colLotNo As String = "LotNo"
    Const colCost As String = "COst"
    Const colValue As String = "ItemValue"
    Const colRemarks As String = "Remarks"

    '==============
    Const colFLineno As String = "FLineno"
    Const colFItemCode As String = "FIcode"
    Const colFItemName As String = "FIname"
    Const colFItemUom As String = "FIUOM"
    Const colFItemType As String = "FItemType"
    Const colFQty As String = "FQty"
    Const colFLotNo As String = "FLotNo"
    Const colFCost As String = "FCOst"
    Const colFValue As String = "FItemValue"
    Const colFMixingCharge As String = "MixingCharge"
    Const colFTotalCost As String = "TotalCost"
    Const colFRemarks As String = "FRemarks"
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRiceMixingEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FunReset()
        txtmixing_uom.Enabled = True
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDescription.Text = ""
        txtfrm_loc_code.Value = ""
        txtfrm_loc_name.Text = ""
        txtto_loc_code.Value = ""
        txtto_loc_name.Text = ""
        txtcharge.Text = Nothing
        txtmixing_uom.Value = ""

        UsLock1.Status = ERPTransactionStatus.Pending
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        gv.Rows.Clear()
        gv.Rows.AddNew()

        gv_finish.Rows.Clear()
        gv_finish.Rows.AddNew()

        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        txtCode.MyReadOnly = False

        '=========
        txtcharge.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(Mixing_Charge,0) as Mixing_Charge from TSPL_MF_SETTINGS"))
        '=======================

        RadPageView1.SelectedPage = RadPageViewPage1
        txtDescription.Focus()
        txtDescription.Select()

    End Sub

    Private Sub FrmRiceMixingEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        End If
    End Sub

    Private Sub FrmRiceMixingEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadFinishGrid()
        FunReset()

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
        repoline1.HeaderImage = My.Resources.search4
        repoline1.TextImageRelation = TextImageRelation.TextBeforeImage
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

        Dim repoline5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline5.FormatString = ""
        repoline5.Name = colQty
        repoline5.HeaderText = "Quantity"
        repoline5.Width = 80
        repoline5.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoline5)

        Dim repoline6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline6.FormatString = ""
        repoline6.Name = colLotNo
        repoline6.HeaderText = "Lot No."
        repoline6.Width = 80
        repoline6.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoline6)

        Dim repoline7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline7.FormatString = ""
        repoline7.Name = colCost
        repoline7.HeaderText = "Item Cost"
        repoline7.Width = 80
        repoline7.DecimalPlaces = 2
        repoline7.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoline7)

        Dim repoline8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline8.FormatString = ""
        repoline8.Name = colValue
        repoline8.HeaderText = "Total Value"
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
        repoline1.HeaderImage = My.Resources.search4
        repoline1.TextImageRelation = TextImageRelation.TextBeforeImage
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
        repoline4.HeaderImage = My.Resources.search4
        repoline4.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_finish.MasterTemplate.Columns.Add(repoline4)

        Dim repoline5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline5.FormatString = ""
        repoline5.Name = colFQty
        repoline5.HeaderText = "Quantity"
        repoline5.Width = 80
        repoline5.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline5)

        Dim repoline6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline6.FormatString = ""
        repoline6.Name = colFLotNo
        repoline6.HeaderText = "Lot No."
        repoline6.Width = 80
        repoline6.ReadOnly = False
        gv_finish.MasterTemplate.Columns.Add(repoline6)

        Dim repoline7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline7.FormatString = ""
        repoline7.Name = colFCost
        repoline7.HeaderText = "Item Cost"
        repoline7.Width = 80
        repoline7.DecimalPlaces = 2
        repoline7.ReadOnly = True
        gv_finish.MasterTemplate.Columns.Add(repoline7)

        Dim repoline8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline8.FormatString = ""
        repoline8.Name = colFValue
        repoline8.HeaderText = "Total Value"
        repoline8.Width = 100
        repoline8.ReadOnly = True
        repoline8.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline8)

        Dim repoline81 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline81.FormatString = ""
        repoline81.Name = colFMixingCharge
        repoline81.HeaderText = "Total Mixing Charge"
        repoline81.Width = 100
        repoline81.ReadOnly = True
        repoline81.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline81)

        Dim repoline82 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline82.FormatString = ""
        repoline82.Name = colFTotalCost
        repoline82.HeaderText = "Total Item Cost"
        repoline82.Width = 100
        repoline82.ReadOnly = True
        repoline82.DecimalPlaces = 2
        gv_finish.MasterTemplate.Columns.Add(repoline82)

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

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
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
                Errorcontrol.SetError(txtto_loc_name, "Select To location.")
                Throw New Exception("Select To location.")
            Else
                Errorcontrol.ResetError(txtto_loc_name)
            End If

            If clsCommon.myCdbl(txtcharge.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtcharge.Select()
                txtcharge.Focus()
                Errorcontrol.SetError(txtcharge, "Fill Mixing charge.")
                Throw New Exception("Fill Mixing charge.")
            Else
                Errorcontrol.ResetError(txtcharge)
            End If

            If clsCommon.myLen(txtmixing_uom.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtmixing_uom.Select()
                txtmixing_uom.Focus()
                Errorcontrol.SetError(txtmixing_uom, "Select Mixing charge unit.")
                Throw New Exception("Select Mixing charge unit.")
            Else
                Errorcontrol.ResetError(txtmixing_uom)
            End If

            If clsCommon.myLen(gv.Rows(0).Cells(colItemCode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                'gv.CurrentColumn = gv.Cells(1)
                Throw New Exception("Fill atleast one item in grid.")
            End If


            Dim icode As String = ""
            Dim qty As Decimal = 0
            Dim uom As String = ""
            Dim lot_no As String = ""
            Dim cost As Decimal = 0
            Dim balance As Decimal = 0

            For ii As Integer = 0 To gv.Rows.Count - 1
                icode = clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value)
                qty = clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value)
                balance = clsCommon.myCdbl(gv.Rows(ii).Cells(colBalance).Value)
                uom = clsCommon.myCstr(gv.Rows(ii).Cells(colItemUom).Value)
                lot_no = clsCommon.myCstr(gv.Rows(ii).Cells(colLotNo).Value)
                cost = clsCommon.myCdbl(gv.Rows(ii).Cells(colCost).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If qty <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Fill Quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    'If clsCommon.myLen(lot_no) <= 0 Then
                    '    RadPageView1.SelectedPage = RadPageViewPage1
                    '    Throw New Exception("Fill Lot No. at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If
                    'If clsCommon.myLen(uom) <= 0 Then
                    '    RadPageView1.SelectedPage = RadPageViewPage1
                    '    Throw New Exception("Fill Unit at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If
                    'If cost <= 0 Then
                    '    RadPageView1.SelectedPage = RadPageViewPage1
                    '    Throw New Exception("Fill Item Cost at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If
                    If qty > balance Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Throw New Exception("Item Code: " + icode + " having balance " + clsCommon.myCstr(balance) + " at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    gv.Rows(ii).Cells(colValue).Value = System.Math.Round(qty * cost, 2)

                    For jj As Integer = ii + 1 To gv.Rows.Count - 1
                        Dim oldicode As String = clsCommon.myCstr(gv.Rows(jj).Cells(colItemCode).Value)
                        Dim oldLotno As String = clsCommon.myCstr(gv.Rows(jj).Cells(colLotNo).Value)

                        If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(lot_no, oldLotno) = CompairStringResult.Equal Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            Throw New Exception("Duplicate Item at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    Next
                End If
            Next

            If clsCommon.myLen(gv_finish.Rows(0).Cells(colFItemCode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                Throw New Exception("Fill atleast one produced item in grid.")
            End If

            CalFinishValue(0)

            For Each grow As GridViewRowInfo In gv_finish.Rows
                icode = clsCommon.myCstr(grow.Cells(colFItemCode).Value)
                qty = clsCommon.myCdbl(grow.Cells(colFQty).Value)
                lot_no = clsCommon.myCstr(grow.Cells(colFLotNo).Value)
                uom = clsCommon.myCstr(grow.Cells(colFItemUom).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If qty <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Fill Quantity at row no. " + clsCommon.myCstr(grow.Index) + "")
                    End If
                    If clsCommon.myLen(lot_no) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Fill Lot No. at row no. " + clsCommon.myCstr(grow.Index) + "")
                    End If
                    If clsCommon.myLen(uom) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage3
                        Throw New Exception("Fill Unit at row no. " + clsCommon.myCstr(grow.Index) + "")
                    End If
                End If
            Next
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Sub SaveData(ByVal isPostClick As Boolean)
        Try
            Dim obj As New clsRiceMixingEntry()

            obj.doccode = clsCommon.myCstr(txtCode.Value)
            obj.Docdate = clsCommon.myCDate(dtpDate.Text)
            obj.descrptn = clsCommon.myCstr(txtDescription.Text).Replace("'", "`")
            obj.frm_loc_code = clsCommon.myCstr(txtfrm_loc_code.Value)
            obj.to_loc_code = clsCommon.myCstr(txtto_loc_code.Value)
            obj.Charge = clsCommon.myCdbl(txtcharge.Text)
            obj.Mixi_uom = clsCommon.myCstr(txtmixing_uom.Value)

            obj.Arr = New List(Of clsRiceMixingDetail)
            obj.Arr_FInish = New List(Of clsRiceMixingFinishDetail)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsRiceMixingDetail()

                objtr.Lineno = clsCommon.myCdbl(grow.Cells(colLineno).Value)
                objtr.Itemcode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(colItemUom).Value)
                objtr.cost = clsCommon.myCstr(grow.Cells(colCost).Value)
                objtr.value = clsCommon.myCdbl(grow.Cells(colValue).Value)
                objtr.Lot_no = clsCommon.myCstr(grow.Cells(colLotNo).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                objtr.qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objtr.Balance = clsCommon.myCdbl(grow.Cells(colBalance).Value)

                If clsCommon.myLen(objtr.Itemcode) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            For Each grow As GridViewRowInfo In gv_finish.Rows
                Dim objtr As New clsRiceMixingFinishDetail()

                objtr.Lineno = clsCommon.myCdbl(grow.Cells(colFLineno).Value)
                objtr.Itemcode = clsCommon.myCstr(grow.Cells(colFItemCode).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(colFItemUom).Value)
                objtr.cost = clsCommon.myCdbl(grow.Cells(colFCost).Value)
                objtr.value = clsCommon.myCdbl(grow.Cells(colFValue).Value)
                objtr.Lot_no = clsCommon.myCstr(grow.Cells(colFLotNo).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colFRemarks).Value).Replace("'", "`")
                objtr.qty = clsCommon.myCdbl(grow.Cells(colFQty).Value)
                objtr.Mixi_charge = clsCommon.myCdbl(grow.Cells(colFMixingCharge).Value)
                objtr.Total_cost = clsCommon.myCdbl(grow.Cells(colFTotalCost).Value)

                If clsCommon.myLen(objtr.Itemcode) > 0 Then
                    obj.Arr_FInish.Add(objtr)
                End If
            Next

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsRiceMixingEntry.SaveData(obj, isNewEntry, trans) Then
                If Not isPostClick Then
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

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
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
                If clsRiceMixingEntry.DeleteData(txtCode.Value, trans) Then
                    myMessages.delete()

                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
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
                If clsRiceMixingEntry.PostData(txtCode.Value, trans) Then
                    myMessages.post()

                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_RICE_MIXING_HEAD where doc_no='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsRiceMixingEntry.GetFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            FunReset()
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsRiceMixingEntry = clsRiceMixingEntry.GetData(strCode, NavType)

            isInsideLoadData = True
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.doccode) > 0 Then
                txtCode.Value = obj.doccode
                dtpDate.Text = obj.Docdate
                txtDescription.Text = obj.descrptn
                txtfrm_loc_code.Value = obj.frm_loc_code
                txtfrm_loc_name.Text = obj.frm_loc_desc
                txtto_loc_code.Value = obj.to_loc_code
                txtto_loc_name.Text = obj.to_loc_desc
                txtcharge.Text = obj.Charge
                txtmixing_uom.Value = obj.Mixi_uom
                txtmixing_uom.Enabled = False

                gv.Rows.Clear()
                gv_finish.Rows.Clear()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsRiceMixingDetail In obj.Arr
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = objtr.Lineno
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Itemcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsItemMaster.GetItemName(objtr.Itemcode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsItemMaster.GetItemType(objtr.Itemcode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemUom).Value = objtr.uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colLotNo).Value = objtr.Lot_no
                        gv.Rows(gv.Rows.Count - 1).Cells(colCost).Value = objtr.cost
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = objtr.value
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colBalance).Value = objtr.Balance
                    Next
                End If

                If obj.Arr_FInish IsNot Nothing AndAlso obj.Arr_FInish.Count > 0 Then
                    For Each objtr As clsRiceMixingFinishDetail In obj.Arr_FInish
                        gv_finish.Rows.AddNew()

                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFLineno).Value = objtr.Lineno
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemCode).Value = objtr.Itemcode
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemName).Value = clsItemMaster.GetItemName(objtr.Itemcode, Nothing)
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemType).Value = clsItemMaster.GetItemType(objtr.Itemcode, Nothing)
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFItemUom).Value = objtr.uom
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFQty).Value = objtr.qty
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFCost).Value = objtr.cost
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFLotNo).Value = objtr.Lot_no
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFValue).Value = objtr.value
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFMixingCharge).Value = objtr.Mixi_charge
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFTotalCost).Value = objtr.Total_cost
                        gv_finish.Rows(gv_finish.Rows.Count - 1).Cells(colFRemarks).Value = objtr.remarks
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
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub txtfrm_loc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtfrm_loc_code._MYValidating
        txtfrm_loc_code.Value = clsLocation.getFinder(" isnull(csa_type,'N')='N'", txtfrm_loc_code.Value, isButtonClicked)
        If clsCommon.CompairString(txtfrm_loc_code.Value, txtto_loc_code.Value) = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("From location and To location can-not same.", Me.Text)
            txtfrm_loc_code.Value = ""
            txtfrm_loc_code.Select()
            txtfrm_loc_code.Focus()
            Errorcontrol.SetError(txtfrm_loc_name, "From location and To location can-not same.")
        Else
            Errorcontrol.ResetError(txtfrm_loc_name)
        End If
        txtfrm_loc_name.Text = clsLocation.GetName(txtfrm_loc_code.Value, Nothing)
    End Sub

    Private Sub txtto_loc_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtto_loc_code._MYValidating
        txtto_loc_code.Value = clsLocation.getFinder(" isnull(csa_type,'N')='N'", txtto_loc_code.Value, isButtonClicked)
        If clsCommon.CompairString(txtfrm_loc_code.Value, txtto_loc_code.Value) = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("From location and To location can-not same.", Me.Text)
            txtto_loc_code.Value = ""
            txtto_loc_code.Select()
            txtto_loc_code.Focus()
            Errorcontrol.SetError(txtto_loc_name, "From location and To location can-not same.")
        Else
            Errorcontrol.ResetError(txtto_loc_name)
        End If
        txtto_loc_name.Text = clsLocation.GetName(txtto_loc_code.Value, Nothing)
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv.Columns(colItemCode) Then
                    isCellValueChanged = True
                    OpenIcode(False, True)
                    isCellValueChanged = False
                End If

                If e.Column Is gv.Columns(colItemUom) Then
                    isCellValueChanged = True
                    OpenUOM(False, True)
                    CalFinishValue(0)
                    isCellValueChanged = False
                End If

                If e.Column Is gv.Columns(colQty) OrElse e.Column Is gv.Columns(colCost) Then
                    isCellValueChanged = True
                    gv.CurrentRow.Cells(colValue).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCost).Value), 2)
                    CalFinishValue(0)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub OpenIcode(ByVal isButtonClicked As Boolean, ByVal isGV As Boolean)
        Dim icode As String = ""
        If isGV Then
            icode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
        Else
            icode = clsCommon.myCstr(gv_finish.CurrentRow.Cells(colFItemCode).Value)
        End If


        If isGV Then
            Dim qry As String = "select * from  (select aa.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Description,aa.UOM,sum(isnull(aa.Qty,0)) as Qty,round(sum(isnull(aa.Basic_Cost,0))/COUNT(aa.Item_Code),2) as [Item Cost],aa.Batch_No as [Lot No] from ( "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.Qty,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT where InOut='I' and location_code='" + txtfrm_loc_code.Value + "' "
            qry += "union all "
            qry += "select TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,(0-TSPL_INVENTORY_MOVEMENT.Qty) as Qty,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.Basic_Cost,TSPL_INVENTORY_MOVEMENT.Batch_No from TSPL_INVENTORY_MOVEMENT where InOut='O' and location_code='" + txtfrm_loc_code.Value + "' "
            qry += ")aa left outer join TSPL_ITEM_MASTER on aa.Item_Code=TSPL_ITEM_MASTER.Item_Code  "
            qry += " group by aa.Item_Code,TSPL_ITEM_MASTER.Item_Desc,aa.UOM,aa.Batch_No)axa where axa.qty>0"

            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ITEMFND", qry)

            If dr IsNot Nothing Then
                gv.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(dr("Code"))
                icode = clsCommon.myCstr(dr("Code"))
                gv.CurrentRow.Cells(colItemName).Value = clsItemMaster.GetItemName(icode, Nothing)
                gv.CurrentRow.Cells(colItemType).Value = clsItemMaster.GetItemType(icode, Nothing)
                gv.CurrentRow.Cells(colItemUom).Value = clsCommon.myCstr(dr("uom"))
                If clsCommon.myLen(txtmixing_uom.Value) <= 0 Then
                    txtmixing_uom.Value = clsCommon.myCstr(dr("uom"))
                End If
                gv.CurrentRow.Cells(colLotNo).Value = clsCommon.myCstr(dr("lot no"))
                If clsCommon.myLen(dr("lot no")) > 0 Then
                    gv.CurrentRow.Cells(colLotNo).ReadOnly = True
                Else
                    gv.CurrentRow.Cells(colLotNo).ReadOnly = False
                End If
                gv.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(dr("qty"))
                gv.CurrentRow.Cells(colBalance).Value = clsCommon.myCdbl(dr("qty"))
                gv.CurrentRow.Cells(colCost).Value = clsCommon.myCdbl(dr("item cost"))
                gv.CurrentRow.Cells(colValue).Value = System.Math.Round(clsCommon.myCdbl(dr("qty")) * clsCommon.myCdbl(dr("item cost")), 2)
                gv.CurrentRow.Cells(colRemarks).Value = ""
            Else
                ResetCurrentRow(gv.CurrentRow.Index)
            End If

        Else
            icode = clsItemMaster.getFinder(" active=1", icode, isButtonClicked)

            If clsCommon.myLen(icode) > 0 Then
                gv_finish.CurrentRow.Cells(colFItemCode).Value = icode
                gv_finish.CurrentRow.Cells(colFItemName).Value = clsItemMaster.GetItemName(icode, Nothing)
                gv_finish.CurrentRow.Cells(colFItemType).Value = clsItemMaster.GetItemType(icode, Nothing)
                gv_finish.CurrentRow.Cells(colFItemUom).Value = clsItemMaster.GetStockUnit(icode, Nothing)
                txtmixing_uom.Value = clsItemMaster.GetStockUnit(icode, Nothing)
                txtmixing_uom.Enabled = False
                gv_finish.CurrentRow.Cells(colFLotNo).Value = ""
                gv_finish.CurrentRow.Cells(colFMixingCharge).Value = Nothing
                gv_finish.CurrentRow.Cells(colFTotalCost).Value = Nothing
                gv_finish.CurrentRow.Cells(colFQty).Value = Nothing
                gv_finish.CurrentRow.Cells(colFCost).Value = Nothing
                gv_finish.CurrentRow.Cells(colFValue).Value = Nothing
                gv_finish.CurrentRow.Cells(colFRemarks).Value = ""
            Else
                ResetFinishCurrentRow(gv_finish.CurrentRow.Index)
            End If
        End If

    End Sub

    Private Sub ResetCurrentRow(ByVal XR As Integer)
        gv.Rows(XR).Cells(colItemCode).Value = ""
        gv.Rows(XR).Cells(colItemName).Value = ""
        gv.Rows(XR).Cells(colItemType).Value = ""
        gv.Rows(XR).Cells(colItemUom).Value = ""
        gv.Rows(XR).Cells(colLotNo).Value = ""
        gv.CurrentRow.Cells(colBalance).Value = Nothing
        gv.Rows(XR).Cells(colQty).Value = Nothing
        gv.Rows(XR).Cells(colCost).Value = Nothing
        gv.Rows(XR).Cells(colValue).Value = Nothing
        gv.Rows(XR).Cells(colRemarks).Value = ""
    End Sub

    Private Sub ResetFinishCurrentRow(ByVal XR As Integer)
        gv_finish.Rows(XR).Cells(colFItemCode).Value = ""
        gv_finish.Rows(XR).Cells(colFItemName).Value = ""
        gv_finish.Rows(XR).Cells(colFItemType).Value = ""
        gv_finish.Rows(XR).Cells(colFItemUom).Value = ""
        gv_finish.Rows(XR).Cells(colFLotNo).Value = ""
        gv_finish.Rows(XR).Cells(colFMixingCharge).Value = Nothing
        gv_finish.Rows(XR).Cells(colFTotalCost).Value = Nothing
        gv_finish.Rows(XR).Cells(colFQty).Value = Nothing
        gv_finish.Rows(XR).Cells(colFCost).Value = Nothing
        gv_finish.Rows(XR).Cells(colFValue).Value = Nothing
        gv_finish.Rows(XR).Cells(colFRemarks).Value = ""
        txtmixing_uom.Enabled = True
    End Sub

    Private Sub OpenUOM(ByVal isButtonClicked As Boolean, ByVal isGV As Boolean)
        Dim uom As String = ""
        Dim icode As String = ""
        If isGV Then
            uom = clsCommon.myCstr(gv.CurrentRow.Cells(colItemUom).Value)
            icode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
        Else
            uom = clsCommon.myCstr(gv_finish.CurrentRow.Cells(colFItemUom).Value)
            icode = clsCommon.myCstr(gv_finish.CurrentRow.Cells(colFItemCode).Value)
        End If

        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_ITEM_UOM_DETAIL.UOM_Description as Description,TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Coversion Factor] from TSPL_ITEM_UOM_DETAIL"
        uom = clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.Item_Code='" + icode + "'", uom, "Code", isButtonClicked)

        If clsCommon.myLen(uom) > 0 Then
            If isGV Then
                gv.CurrentRow.Cells(colItemUom).Value = uom
            Else
                gv_finish.CurrentRow.Cells(colFItemUom).Value = uom
                txtmixing_uom.Value = uom
                txtmixing_uom.Enabled = False
            End If
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_finish_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_finish.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                If e.Column Is gv_finish.Columns(colFItemCode) Then
                    isCellValueChanged = True
                    OpenIcode(False, False)
                    CalFinishValue(gv_finish.CurrentRow.Index)
                    isCellValueChanged = False
                End If

                If e.Column Is gv_finish.Columns(colFItemUom) Then
                    isCellValueChanged = True
                    OpenUOM(False, False)
                    CalFinishValue(gv_finish.CurrentRow.Index)
                    isCellValueChanged = False
                End If

                If e.Column Is gv_finish.Columns(colFQty) Then
                    isCellValueChanged = True
                    CalFinishValue(gv_finish.CurrentRow.Index)
                    isCellValueChanged = False
                End If
            End If
        End If
    End Sub

    Private Sub CalFinishValue(ByVal XR As Integer)
        Dim total_value As Decimal = 0
        Dim total_qty As Decimal = 0
        Dim Mix_unit_Qty As Decimal = 0
        Dim icode As String = ""
        Dim uom As String = ""

        If clsCommon.myLen(txtmixing_uom.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select mixing charge unit")
            txtmixing_uom.Focus()
            txtmixing_uom.Select()
            Exit Sub
        End If

        For Each grow As GridViewRowInfo In gv.Rows
            icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
            uom = clsCommon.myCstr(grow.Cells(colItemUom).Value)

            total_qty += System.Math.Round((clsCommon.myCdbl(grow.Cells(colQty).Value) * clsRiceMixingEntry.GetConversionFactor(icode, uom)) / IIf(clsRiceMixingEntry.GetConversionFactor(icode, txtmixing_uom.Value) = 0, 1, clsRiceMixingEntry.GetConversionFactor(icode, txtmixing_uom.Value)), 2)
            total_value += System.Math.Round((clsCommon.myCdbl(grow.Cells(colValue).Value) * IIf(clsRiceMixingEntry.GetConversionFactor(icode, txtmixing_uom.Value) = 0, 1, clsRiceMixingEntry.GetConversionFactor(icode, txtmixing_uom.Value))) / IIf(clsRiceMixingEntry.GetConversionFactor(icode, uom) = 0, 1, clsRiceMixingEntry.GetConversionFactor(icode, uom)), 2)
        Next

        If clsCommon.myLen(gv_finish.Rows(XR).Cells(colFItemCode).Value) > 0 Then
            gv_finish.Rows(XR).Cells(colFValue).Value = total_value
            gv_finish.Rows(XR).Cells(colFMixingCharge).Value = System.Math.Round(total_qty * clsCommon.myCdbl(txtcharge.Text), 2)
            gv_finish.Rows(XR).Cells(colFTotalCost).Value = System.Math.Round(total_value + clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFMixingCharge).Value), 2)
            If clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFQty).Value) > 0 Then
                gv_finish.Rows(XR).Cells(colFCost).Value = System.Math.Round(clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFTotalCost).Value) / clsCommon.myCdbl(gv_finish.Rows(XR).Cells(colFQty).Value), 2)
            Else
                gv_finish.Rows(XR).Cells(colFCost).Value = 0
            End If
        End If
    End Sub

    Private Sub txtcharge_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcharge.TextChanged
        If clsCommon.myCdbl(txtcharge.Text) > 0 AndAlso gv_finish.Rows.Count >= 1 Then
            If clsCommon.myLen(gv_finish.Rows(0).Cells(colFItemCode).Value) > 0 Then
                CalFinishValue(0)
            End If
        End If
    End Sub

    Private Sub txtmixing_uom__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtmixing_uom._MYValidating
        If clsCommon.myCdbl(txtcharge.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill mixing charge first.", Me.Text)
            txtcharge.Focus()
            txtcharge.Select()
            Exit Sub
        End If
        Dim qry As String = "select distinct unit_code as Code,unit_desc as Description from tspl_unit_master"
        txtmixing_uom.Value = clsCommon.ShowSelectForm("UOMFND", qry, "Code", "", txtmixing_uom.Value, "Code", isButtonClicked)
    End Sub

    Private Sub gv_finish_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_finish.CurrentColumnChanged
        If gv_finish.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_finish.CurrentRow.Index
            gv_finish.CurrentRow.Cells(colFLineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv_finish.Rows.Count - 1 Then
                'gv.Rows.AddNew()
                gv_finish.CurrentRow = gv_finish.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnsaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveLayout.Click
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

    Private Sub btndeletelayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(ReportID + "GV", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportID + "GVFINISH", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Delete layout successfully", "Information")
    End Sub
End Class
