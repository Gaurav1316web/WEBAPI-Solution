Imports common
Public Class frmDBTNEFTUnionReport

    Private Sub frmDBTNEFTUnionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Month()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        Try
            Month()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Month()
        If clsCommon.myLen(txtFromDate) > 0 Then
            txtToDate.Value = txtFromDate.Value.AddMonths(2)
        End If
    End Sub
    Sub Reset()
        Gv.DataSource = Nothing
        Gv.Rows.Clear()
        Gv.MasterTemplate.SummaryRowsBottom.Clear()
        Gv.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Dim query As String = Nothing
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Gv.DataSource = Nothing
                Exit Sub
            End If
            Dim dtr As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            For ii As Integer = 0 To dtr.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If
                query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
            Next
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                Gv.DataSource = dt2
                Gv.ReadOnly = True
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class