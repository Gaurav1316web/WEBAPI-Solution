Imports common
Imports System.IO

Public Class frmBatchItemIn_ForMilkItem
#Region "Variables"
    Dim IsmanualBatchNomandatory As Double = 0
    Public strItemCode As String = ""
    Public strItemName As String = ""
    Public dblqty As Double = 0
    Public strUOM As String = ""
    Public dblMRP As Double = 0

    Const ColSNo As String = "COLSNO"
    Const colBatchNo As String = "COLBATCHNO"
    Const colQty As String = "COLQTY"
    Public arr As List(Of clsBatchInventoryNew) = Nothing
    Const ReportID As String = "BatchInvInNew"
    Public isCencelButtonClicked As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False
#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isInsideLoadData = True
        lblItemCode.Text = strItemCode
        lblItemName.Text = strItemName
        lblQty.Text = clsCommon.myFormat(dblqty)
        lblMRP.Text = clsCommon.myFormat(dblMRP)
        lblUOM.Text = strUOM
        LoadBlankGrid()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsBatchInventoryNew In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = obj.Batch_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Qty
            Next
        End If
        gv1.Rows.AddNew()
        RefeshSNO()
        If gv1.RowCount > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(colBatchNo)
        End If
        gv1.Focus()
        isInsideLoadData = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SrNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 20
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Batch No"
        repoLocationName.Name = colBatchNo
        repoLocationName.ReadOnly = False
        repoLocationName.IsVisible = True
        repoLocationName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:n2}"
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.ReadOnly = False
        repoQty.IsVisible = True
        repoQty.Width = 80
        gv1.MasterTemplate.Columns.Add(repoQty)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        gv1.AllowDeleteRow = True
        ReStoreGridLayout()

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        OKPressed()
    End Sub

    Sub OKPressed()
        Try
            If AllowToSave() Then
                arr = New List(Of clsBatchInventoryNew)
                For ii As Integer = 0 To gv1.RowCount - 1
                    Dim obj As clsBatchInventoryNew = New clsBatchInventoryNew()
                    obj.Batch_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colBatchNo).Value)
                    obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                    If clsCommon.myLen(obj.Batch_No) > 0 AndAlso obj.Qty <> 0 Then
                        arr.Add(obj)
                    End If
                Next
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim dblTotQty As Double = 0
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) > 0 Then
                dblTotQty += clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            End If
        Next
        If clsCommon.myCdbl(lblQty.Text) <> dblTotQty Then
            Throw New Exception("Total Quantity " + lblQty.Text + Environment.NewLine + "But Batch wise Quantity" + clsCommon.myCstr(dblTotQty))
        End If
        Return True
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            OKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        RefeshSNO()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If gv1.RowCount <= clsCommon.myCdbl(lblQty.Text) Then
            e.Cancel = True
            Exit Sub
        End If

        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub


    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For ii As Integer = 0 To gv1.RowCount - 1
            If ii < dblqty Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
                    gv1.Rows(ii).Cells(colBatchNo).Value = clsItemMaster.GetItemSerialCounter(strItemCode, Nothing)
                End If
            End If
        Next
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

End Class
