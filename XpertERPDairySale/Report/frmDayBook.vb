Imports common

Public Class frmDayBook
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtfDate.Value
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            'RadPageView1.SelectedPage = RadPageViewPage2
            'gvData.GroupDescriptors.Clear()
            'gvData.MasterTemplate.SummaryRowsBottom.Clear()
            'gvData.DataSource = dt
            ''SetGridFormationgvData()
            'gvData.AutoExpandGroups = True
            'gvData.ShowGroupPanel = True
            'gvData.ShowRowHeaderColumn = False
            'gvData.AllowAddNewRow = False
            'gvData.AllowDeleteRow = False
            'gvData.EnableFiltering = True
            'gvData.ShowFilteringRow = True
            'gvData.BestFitColumns()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class