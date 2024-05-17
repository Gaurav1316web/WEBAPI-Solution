Imports common
Public Class FrmPjcDrilldown

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


            Dim item20 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item20)
            Dim item21 As New GridViewSummaryItem("Billing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gv1.EnableFiltering = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim DocNo As String

        DocNo = clsCommon.myCstr(gv1.CurrentRow.Cells("Document No").Value)

        If clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "Expense") = CompairStringResult.Equal Then
            Dim frm As New FrmPJCExpense
            frm.SetUserMgmt(clsUserMgtCode.FrmPJCExpense)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "APInvoice") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntry, DocNo)
            'Dim frm As New FrmAPInvoiceEntry
            'frm.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
            'frm.Show()
            'frm.LoadData(DocNo)
        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "TimeSheet") = CompairStringResult.Equal Then
            Dim frm As New frmTimesheet
            frm.SetUserMgmt(clsUserMgtCode.frmTimeSheet)
            frm.Show()
            frm.LoadData("Filled", DocNo)
        ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("Type").Value, "ARInvoice") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, DocNo)
            'Dim frm As New FrmARInvoiceEntry
            'frm.SetUserMgmt(clsUserMgtCode.mbtnARInvoiceEntry)
            'frm.Show()
            'frm.LoadData(DocNo)
        End If
    End Sub
End Class
