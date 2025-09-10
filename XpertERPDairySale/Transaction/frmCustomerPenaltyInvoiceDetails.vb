
Imports System.IO
Imports Telerik.WinControls.UI
Imports common
Public Class frmCustomerPenaltyInvoiceDetails
#Region "Variables"
    Const ColSNo As String = "ColSNo"
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colInvoiceAmt As String = "colInvoiceAmt"
    Public arr As List(Of clsCustomerPenaltyInvoiceDetail) = Nothing
    Const ReportID As String = "CustomerPenaltyInvoice"
    Public isCancelButtonClicked As Boolean = False
#End Region

    Private Sub frmCustomerPenaltyInvoiceDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsCustomerPenaltyInvoiceDetail In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceNo).Value = obj.Invoice_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmt).Value = obj.Invoice_Amt
            Next
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

        Dim repoInvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceNo.FormatString = ""
        repoInvoiceNo.HeaderText = "Invoice No"
        repoInvoiceNo.Name = colInvoiceNo
        repoInvoiceNo.ReadOnly = True
        repoInvoiceNo.IsVisible = True
        repoInvoiceNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoInvoiceNo)

        Dim repoInvoiceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoInvoiceAmt.FormatString = ""
        repoInvoiceAmt.HeaderText = "Invoice Amount"
        repoInvoiceAmt.Name = colInvoiceAmt
        repoInvoiceAmt.ReadOnly = True
        repoInvoiceAmt.IsVisible = True
        repoInvoiceAmt.Width = 150
        gv1.MasterTemplate.Columns.Add(repoInvoiceAmt)

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

    Private Sub frmCustomerPenaltyInvoiceDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
