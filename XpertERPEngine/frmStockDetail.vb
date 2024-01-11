Imports common
Public Class FrmStockDetail
    Public strLoadSerial As Boolean = False

    Private Sub FrmStockDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Sub LoadDispatchData(ByVal strQuery As String)
        Try

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt.Rows.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Me.Close()
                Exit Sub
            End If
            gv1.DataSource = dt



            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item20 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.EnableFiltering = True
            gv1.ShowGroupPanel = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strQuery As String)
        Try

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt.Rows.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Me.Close()
                Exit Sub
            End If
            gv1.DataSource = dt

           

            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            Next
            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 150
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("Docdate").IsVisible = True
            gv1.Columns("Docdate").Width = 80
            gv1.Columns("Docdate").HeaderText = "Document Date"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 250
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").Width = 80
            gv1.Columns("Qty").HeaderText = "Quantity"

            Dim item20 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.EnableFiltering = True
            gv1.ShowGroupPanel = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub LoadSerialData(ByVal strQuery As String)
        Try
            strLoadSerial = True
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            If dt.Rows.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Me.Close()
                Exit Sub
            End If
            gv1.DataSource = dt

            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
            Next
          
            gv1.EnableFiltering = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Dim strType As String = ""
        Dim strDoc As String = ""
        If strLoadSerial Then
            strType = clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value)
            strDoc = clsCommon.myCstr(gv1.CurrentRow.Cells("Document").Value)
        Else
            strType = clsCommon.myCstr(gv1.CurrentRow.Cells("Trans Type").Value)
            strDoc = clsCommon.myCstr(gv1.CurrentRow.Cells("DocNo").Value)
        End If
        If clsCommon.myLen(strType) > 0 AndAlso clsCommon.myLen(strDoc) > 0 Then
            Select Case strType
                Case "IC-AD"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDoc)
                Case "ISSTRAN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDoc)
                Case "SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strDoc)
                Case "SD-IN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                Case "Sale Return"
                    If objCommonVar.IsDemoERP Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strDoc)
                    End If
                Case "Purchase Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDoc)
                Case "Purchase Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDoc)
                Case "Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strDoc)
                Case "Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDoc)
                Case "Load Out"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                Case "DISPATCH-DS"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, strDoc)
            End Select
        End If

        
    End Sub
End Class
