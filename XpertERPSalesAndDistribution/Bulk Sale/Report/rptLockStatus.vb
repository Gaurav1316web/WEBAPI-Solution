Imports common

Public Class rptLockStatus

    Private Sub rptLockStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub FormatGrid()
        gv1.AllowAddNewRow = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.ShowGroupPanel = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.Columns("Route_No").HeaderText = "Route No"
        gv1.Columns("Route_Desc").HeaderText = "Route Name"
    End Sub

    Sub LoadData()
        Try
            Dim qry As String = "select Route_No ,Route_Desc,convert(varchar,getdate(),103) as Date,case when (convert(varchar,MorningCutOff_Time,108) < convert(varchar,SYSDATETIME(),108) ) then 'Close'  else 'Open' end  as [Morning Staus] ,case when (convert(varchar,EveningCutOff_Time,108) < convert(varchar,SYSDATETIME(),108)) then 'Close' else 'Open' end as  [Evening Status] from tspl_route_master order by Route_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                gv1.MasterTemplate.AutoExpandGroups = True
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If gv1.Rows.Count > 0 Then
                For i As Integer = 0 To gv1.Rows.Count - 1
                    For ic As Integer = 3 To gv1.Columns.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(ic).Value), "Close") = CompairStringResult.Equal Then
                            gv1.Rows(i).Cells(ic).Style.ForeColor = System.Drawing.Color.Red
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(ic).Value), "Open") = CompairStringResult.Equal Then
                            gv1.Rows(i).Cells(ic).Style.ForeColor = Color.Green

                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class