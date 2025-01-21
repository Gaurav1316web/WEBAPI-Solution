
Imports System.IO
Imports Telerik.WinControls.UI
Imports common
Public Class frmMultipleShareAPInvoiceDetails
#Region "Variables"
    Dim IsmanualBatchNomandatory As Double = 0
    Public strDCSCode As String = ""
    Public strDCSName As String = ""
    Const ColSNo As String = "ColSNo"
    Const colDate As String = "colDate"
    Const colAPInvoiceNo As String = "colAPInvoiceNo"
    Const colDCSCode As String = "colDCSCode"
    Const colDCSName As String = "colDCSName"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colUsedAmt As String = "colUsedAmt"

    Public arr As List(Of clsMultipleShareAllotmentAPInvoiceDetail) = Nothing
    Const ReportID As String = "MultipleShareAPInvoice"
    Public isCencelButtonClicked As Boolean = False
#End Region

    Private Sub frmMultipleShareAPInvoiceDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()

        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsMultipleShareAllotmentAPInvoiceDetail In arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = obj.AP_Date
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAPInvoiceNo).Value = obj.AP_Invoice_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = obj.VLC_Code_VLC_Uploader
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = obj.VLC_Name
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = obj.Balance_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUsedAmt).Value = obj.Used_Amt
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

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.ReadOnly = True
        repoDate.IsVisible = True
        repoDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoAPInvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAPInvoiceNo.FormatString = ""
        repoAPInvoiceNo.HeaderText = "AP Invoice No"
        repoAPInvoiceNo.Name = colAPInvoiceNo
        repoAPInvoiceNo.ReadOnly = True
        repoAPInvoiceNo.IsVisible = True
        repoAPInvoiceNo.Width = 150
        gv1.MasterTemplate.Columns.Add(repoAPInvoiceNo)

        Dim repoDCSCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSCode.HeaderText = "DCS Code"
        repoDCSCode.Name = colDCSCode
        repoDCSCode.ReadOnly = True
        repoDCSCode.IsVisible = True
        repoDCSCode.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDCSCode)

        Dim repoDCSName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSName.HeaderText = "DCS Name"
        repoDCSName.Name = colDCSName
        repoDCSName.ReadOnly = True
        repoDCSName.IsVisible = True
        repoDCSName.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDCSName)

        Dim repoBalanceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceAmt.FormatString = "{0:n2}"
        repoBalanceAmt.HeaderText = "Balance Amount"
        repoBalanceAmt.Name = colBalanceAmt
        repoBalanceAmt.ReadOnly = True
        repoBalanceAmt.IsVisible = False
        repoBalanceAmt.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBalanceAmt)

        Dim repoUsedAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUsedAmt.FormatString = "{0:n2}"
        repoUsedAmt.HeaderText = "Used Amount"
        repoUsedAmt.Name = colUsedAmt
        repoUsedAmt.ReadOnly = True
        repoUsedAmt.IsVisible = True
        repoUsedAmt.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUsedAmt)

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

    Private Sub frmMultipleShareAPInvoiceDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelPressed()
    End Sub

End Class
