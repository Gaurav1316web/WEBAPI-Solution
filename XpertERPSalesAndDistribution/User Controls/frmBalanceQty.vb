Imports common
Imports System.IO
Imports System.ComponentModel
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmBalanceQty
#Region "Varables"
    Public qry As String = ""
    Public ReportID As String = "COmmitedQty"
    Public strUOM As String = ""
    Public IsMRPMandatory As Boolean = False
    Public _METEXT As String = "Commited Quantity"
#End Region

    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = _METEXT
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            Next
            lblUOM.Text = strUOM

            gv1.Columns("TransType").Width = 150
            gv1.Columns("TransType").IsVisible = False
            gv1.Columns("TransType").HeaderText = "Transaction Type"

            gv1.Columns("ICode").IsVisible = True
            gv1.Columns("ICode").Width = 100
            gv1.Columns("ICode").HeaderText = "Item"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 150
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 150
            gv1.Columns("DocNo").HeaderText = "Document No"


            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").Width = 150
            gv1.Columns("Qty").HeaderText = "Quantity"
            gv1.Columns("Qty").FormatString = "{0:n2}"


            gv1.Columns("MRP").Width = 100
            gv1.Columns("MRP").IsVisible = IsMRPMandatory
            gv1.Columns("MRP").HeaderText = "MRP"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gv1.ShowGroupPanel = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.ShowFilteringRow = True
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False

            ReStoreGridLayout()
        Else
            clsCommon.MyMessageBoxShow("No Data found to dispaly")
            Me.Close()
            Exit Sub
        End If
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

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If

    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            Dim strDocNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("DocNo").Value)
            If clsCommon.myLen(strDocNo) > 0 Then
                'Dim colorValue As EnumTransType = [Enum].Parse(GetType(EnumTransType), clsCommon.myCstr(gv1.CurrentRow.Cells("TransCode").Value)) ''CType(Enum.Parse(GetType(EnumTransType), clsCommon.myCstr(gv1.CurrentRow.Cells("TransCode").Value), EnumTransType))
                'clsOpenTransactionForm.OpenTransacionForm(colorValue, strDocNo)
                clsOpenTransactionForm.OpenTransacionForm(clsCommon.myCstr(gv1.CurrentRow.Cells("TransCode").Value), strDocNo)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
