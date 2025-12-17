Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmStartBatchEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "colLineNo"
    Const colLocationCode As String = "colLocationCode"
    Const colLocationName As String = "colLocationName"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colQty As String = "colQty"
    Const colStockUOM As String = "colStockUOM"
    Const colAmount As String = "colAmount"
    Dim isLoadData As Boolean = False
    Dim j As Integer = 0
    Dim obj As New clsStartBatchEntry()
    Dim objtr As New clsStartBatchEntryDetail()
#End Region

    Private Sub frmStartBatchEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Addnew()
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
        If Not objCommonVar.AutoGenrateBatchInventory Then
            lblBatch.Visible = True
            txtDefaultBatch.Visible = True
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSaveAndPost.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 40
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLineNo.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.HeaderText = "Location Code"
        repoLocation.Name = colLocationCode
        repoLocation.Width = 120
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.HeaderText = "Location Name"
        repoLocationName.Name = colLocationName
        repoLocationName.Width = 120
        repoLocationName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colItemName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoStockQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStockQty.FormatString = "{0:n2}"
        repoStockQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoStockQty.HeaderText = "Qty"
        repoStockQty.Name = colQty
        repoStockQty.Width = 130
        repoStockQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStockQty)

        Dim repoStockUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStockUOM.HeaderText = "Unit Code"
        repoStockUOM.Name = colStockUOM
        repoStockUOM.Width = 130
        repoStockUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStockUOM)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = "{0:n2}"
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.Width = 130
        repoAmount.ReadOnly = True
        repoAmount.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoAmount)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        ReStoreGridLayoutgv1()
    End Sub

    Private Sub ReStoreGridLayoutgv1()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii & 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        btnGo.Enabled = val
    End Sub
    Function AllowToSave() As Boolean
        Xtra.TransactionValidity(txtDocumentDate.Value)
        If Not objCommonVar.AutoGenrateBatchInventory Then
            If clsCommon.myLen(txtDefaultBatch.Text) = 0 Then
                txtDefaultBatch.Focus()
                Throw New Exception("Default Batch can't be blank.")
            End If
        End If
        Return True
    End Function

    Function SaveData() As Boolean
        Dim IsSaved As Boolean = False
        Try
            If (AllowToSave()) Then
                obj = New clsStartBatchEntry()
                obj.Document_No = txtDocumentNo.Value
                obj.Document_date = clsCommon.myCDate(txtDocumentDate.Value)
                obj.Default_Batch = txtDefaultBatch.Text
                obj.Remarks = txtRemarks.Text
                obj.Arr = New List(Of clsStartBatchEntryDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLocationCode).Value)) > 0 Then
                        Dim objTr As New clsStartBatchEntryDetail()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Location_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        objTr.Item_Code = clsCommon.myCstr((grow.Cells(colItemCode).Value))
                        If Not objCommonVar.AutoGenrateBatchInventory Then
                            objTr.arrBatchItem = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                        End If
                        objTr.Qty = clsCommon.myCDecimal((grow.Cells(colQty).Value))
                        objTr.Unit_code = clsCommon.myCstr((grow.Cells(colStockUOM).Value))
                        objTr.Amount = clsCommon.myCDecimal((grow.Cells(colAmount).Value))
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    IsSaved = True
                Else
                    IsSaved = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return IsSaved
    End Function

    Private Sub Addnew()
        isNewEntry = True
        txtItemType.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtDocumentNo.Value = ""
        txtDefaultBatch.Text = ""
        btnSaveAndPost.Enabled = True
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        txtRemarks.Text = ""
        LoadBlankGrid()
        isInsideLoadData = False
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayoutgv1()
        EnableDisableControls(True)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnSaveAndPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAndPost.Click
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data found to save", Me.Text)
                Exit Sub
            End If
            If clsCommon.MyMessageBoxShow(Me, "Save and Post the Current Document " & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If SaveData() Then
                    clsCommon.MyMessageBoxShow(Me, "Data save and posted successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Try
            If clsCommon.myLen(txtDocumentNo) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
            End If
            obj = New clsStartBatchEntry()
            txtDocumentNo.Value = obj.getFinder(txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_START_BATCH_ENTRY where Document_No='" & txtDocumentNo.Value & "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSaveAndPost.Enabled = True
            Addnew()
            obj = New clsStartBatchEntry()
            obj = obj.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                Addnew()
                isLoadData = True
                isNewEntry = False
                EnableDisableControls(False)
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnSaveAndPost.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                End If
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_date
                txtDefaultBatch.Text = obj.Default_Batch
                txtRemarks.Text = obj.Remarks
                txtItemType.arrValueMember = obj.arrItemType
                txtItem.arrValueMember = obj.arrItem
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objtr As clsStartBatchEntryDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objtr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objtr.Location_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objtr.Location_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Tag = objtr.arrBatchItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStockUOM).Value = objtr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objtr.Amount
                    Next
                End If

            End If
            isInsideLoadData = True
            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub frmStartBatchEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSaveAndPost.Enabled AndAlso MyBase.isModifyFlag Then
            btnSaveAndPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnReverseDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnReverseDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseDelete.Visible = True
            End If
        End If
    End Sub

    Private Sub btnReverseUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseDelete.Click
        Try
            obj = New clsStartBatchEntry()
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse,Unpost and Delete the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If obj.DeleteData(txtDocumentNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Deleted", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If Not objCommonVar.AutoGenrateBatchInventory Then
            If clsCommon.myLen(txtDefaultBatch.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Default Batch", Me.Text)
                Exit Sub
            End If
        End If
        LoadBlankGrid()
        isLoadData = False
        LoadGridData()
    End Sub

    Private Sub LoadGridData()
        Try
            Dim whrcls As String = " and TSPL_ITEM_MASTER.Is_Batch_Item = 0 "
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                whrcls += "  and  TSPL_ITEM_MASTER.Item_TYPE IN (" & clsCommon.GetMulcallString(txtItemType.arrValueMember) & ") "
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whrcls += "  and  TSPL_ITEM_MASTER.Item_Code IN (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If
            Dim qry As String = " ;WITH CTE_Main AS ( SELECT TSPL_INVENTORY_MOVEMENT.Location_Code,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS Location_Desc,TSPL_INVENTORY_MOVEMENT.Item_Code,MAX(TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc,SUM(TSPL_INVENTORY_MOVEMENT.Stock_Qty * CASE WHEN TSPL_INVENTORY_MOVEMENT.InOut = 'I' THEN 1 ELSE -1 END) AS Stock_Qty,
            MAX(TSPL_INVENTORY_MOVEMENT.Stock_UOM) AS Stock_UOM,SUM(TSPL_INVENTORY_MOVEMENT.Avg_Cost * CASE WHEN TSPL_INVENTORY_MOVEMENT.InOut = 'I' THEN 1 ELSE -1 END) AS Amount FROM TSPL_INVENTORY_MOVEMENT  LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_INVENTORY_MOVEMENT.Location_Code
            LEFT JOIN TSPL_ITEM_MASTER TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code WHERE 2=2 " & whrcls & " and TSPL_INVENTORY_MOVEMENT.Punching_Date <= '" & clsCommon.GetPrintDate(txtDocumentDate.Value, "dd/MMM/yyyy") & "' GROUP BY TSPL_INVENTORY_MOVEMENT.Location_Code, TSPL_INVENTORY_MOVEMENT.Item_Code ),
            CTE_PositiveRows AS ( SELECT * FROM CTE_Main WHERE ISNULL(Stock_Qty,0) <> 0 OR ISNULL(Amount,0) <> 0 ),  CTE_AllItems AS ( SELECT Item_Code, Item_Desc FROM TSPL_ITEM_MASTER WHERE 2=2 " & whrcls & " ),
            CTE_MissingItems AS ( SELECT TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, CTE_AllItems.Item_Code, CTE_AllItems.Item_Desc, 0.00 AS Stock_Qty, ''   AS Stock_UOM, 0.00 AS Amount FROM CTE_AllItems  CROSS JOIN ( SELECT TOP 1 Location_Code, Location_Desc FROM TSPL_LOCATION_MASTER ORDER BY Location_Code ) TSPL_LOCATION_MASTER
            WHERE NOT EXISTS ( SELECT 1 FROM CTE_PositiveRows  WHERE CTE_PositiveRows.Item_Code = CTE_AllItems.Item_Code ) ) SELECT  CTE_PositiveRows.Location_Code,CTE_PositiveRows.Location_Desc,CTE_PositiveRows.Item_Code, CTE_PositiveRows.Item_Desc,CTE_PositiveRows.Stock_Qty,ISNULL(CTE_PositiveRows.Stock_UOM, '') AS Stock_UOM,CTE_PositiveRows.Amount FROM CTE_PositiveRows 
            UNION ALL SELECT TSPL_INVENTORY_MOVEMENT.Location_Code, TSPL_INVENTORY_MOVEMENT.Location_Desc, TSPL_INVENTORY_MOVEMENT.Item_Code, TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM, TSPL_INVENTORY_MOVEMENT.Amount FROM CTE_MissingItems TSPL_INVENTORY_MOVEMENT ORDER BY Item_Code, Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                obj = New clsStartBatchEntry()
                For ii As Integer = 0 To dt.Rows.Count - 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = ii + 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dt.Rows(ii)("Location_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dt.Rows(ii)("Location_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dt.Rows(ii)("Stock_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStockUOM).Value = clsCommon.myCstr(dt.Rows(ii)("Stock_UOM"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCDecimal(dt.Rows(ii)("Amount"))
                    OpenBatchItem(False)
                Next
                EnableDisableControls(False)
            Else
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F5 Then
            OpenBatchItem(True)
        End If
    End Sub
    Sub OpenBatchItem(ByVal isFromF5 As Boolean)
        Dim frm As frmBatchItemIn = New frmBatchItemIn()
        frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemName).Value)
        frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
        frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colStockUOM).Value)
        frm.TransDate = txtDocumentDate.Value
        If frm.dblqty > 0 Then
            frm.arr = TryCast(gv1.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
            If Not objCommonVar.AutoGenrateBatchInventory Then
                If Not isFromF5 Then
                    frm.arr = New List(Of clsBatchInventory)
                    Dim dblTotalQty As Double = 0
                    Dim blnAvailable As Boolean = False

                    Dim obj As clsBatchInventory = New clsBatchInventory()
                    obj.Batch_No = txtDefaultBatch.Text
                    obj.Manual_BatchNo = txtDefaultBatch.Text
                    obj.Manufacture_Date = clsCommon.myCDate(txtDocumentDate.Value)
                    obj.Expiry_Date = clsCommon.myCDate(txtDocumentDate.Value)

                    obj.Qty = frm.dblqty
                    ' obj.Unit_code = strUnit_code
                    If obj.Qty > 0 Then
                        frm.arr.Add(obj)
                        gv1.CurrentRow.Cells(colItemCode).Tag = frm.arr
                    End If
                Else
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv1.CurrentRow.Cells(colItemCode).Tag = frm.arr
                    End If
                End If
            ElseIf isFromF5 Then
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colItemCode).Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControls(True)
        LoadBlankGrid()
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Try
            Dim qry As String = " SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER WHERE 1=1 "
            txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypeMul", qry, "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " select item_code as Code,Item_Desc as Name from TSPL_ITEM_MASTER WHERE is_batch_item = 0 "
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                qry += " and Item_Type IN (" & clsCommon.GetMulcallString(txtItemType.arrValueMember) & ")"
            End If
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMul", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocumentNo.Value)
    End Sub

End Class


