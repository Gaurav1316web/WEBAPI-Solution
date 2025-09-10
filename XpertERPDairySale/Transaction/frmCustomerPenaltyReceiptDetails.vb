
Imports System.IO
Imports Telerik.WinControls.UI
Imports common
Public Class frmCustomerPenaltyReceiptDetails
#Region "Variables"
    Const ColSNo As String = "ColSNo"
    Const colReceiptNo As String = "colReceiptNo"
    Public arr As List(Of clsCustomerPenaltyReceiptDetail) = Nothing
    Const ReportID As String = "CustomerPenaltyReceipt"
    Public isCancelButtonClicked As Boolean = False
    Const colReceiptAmt As String = "colReceiptAmt"
#End Region

    Private Sub frmCustomerPenaltyReceiptDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsCustomerPenaltyReceiptDetail In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colReceiptNo).Value = obj.Receipt_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colReceiptAmt).Value = obj.Receipt_Amt
            Next
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
        End If
        RefeshSNO()
        gv1.Focus()
        gv1.BestFitColumns()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 20
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoReceiptNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReceiptNo.FormatString = ""
        repoReceiptNo.HeaderText = "Receipt No"
        repoReceiptNo.Name = colReceiptNo
        repoReceiptNo.ReadOnly = True
        repoReceiptNo.IsVisible = True
        repoReceiptNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoReceiptNo)

        Dim repoReceiptAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReceiptAmt.FormatString = ""
        repoReceiptAmt.HeaderText = "Receipt Amount"
        repoReceiptAmt.Name = colReceiptAmt
        repoReceiptAmt.ReadOnly = True
        repoReceiptAmt.IsVisible = True
        repoReceiptAmt.Width = 150
        gv1.MasterTemplate.Columns.Add(repoReceiptAmt)

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
        Me.Close()
    End Sub
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub frmCustomerPenaltyReceiptDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            OKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub

    Sub CancelPressed()
        isCancelButtonClicked = True
        Me.Close()
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelPressed()
    End Sub

End Class
