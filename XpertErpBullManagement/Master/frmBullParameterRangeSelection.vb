Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmBullParameterRangeSelection

    Public Form_ID As String = ""
    Public Range_Selection As String
    Const colSelection As String = "colSelection"

    Public Sub AddGridView(ByVal Count As Decimal)
        Try
            If Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To Count - 1
                    gvRangeDetails.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub loadBlankGrid()
        Try
            Dim qry As String = String.Empty
            gvRangeDetails.DataSource = Nothing
            gvRangeDetails.Rows.Clear()
            gvRangeDetails.Columns.Clear()
            gvRangeDetails.Refresh()

            Dim gridcolSelection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolSelection.FormatString = ""
            gridcolSelection.HeaderText = "Selection Range"
            gridcolSelection.Name = colSelection
            gridcolSelection.Width = 110
            gvRangeDetails.MasterTemplate.Columns.Add(gridcolSelection)

            gvRangeDetails.AllowAddNewRow = False
            gvRangeDetails.AllowDeleteRow = True
            gvRangeDetails.AllowRowReorder = False
            gvRangeDetails.ShowGroupPanel = False
            gvRangeDetails.EnableFiltering = False
            gvRangeDetails.EnableSorting = False
            gvRangeDetails.EnableGrouping = False
            gvRangeDetails.AllowColumnChooser = True
            gvRangeDetails.AllowColumnReorder = True
            'gvRangeDetails.Rows.AddNew()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvRangeDetails.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvRangeDetails.Columns.Count - 1 Step ii + 1
                        gvRangeDetails.Columns(ii).IsVisible = False
                        gvRangeDetails.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvRangeDetails.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If clsCommon.CompairString(Form_ID, "BLL-CMU-GRP") = CompairStringResult.Equal Then
                Dim obj As New ClsCMUGrouping()
                obj.arrSelectionRange = New List(Of clsBullCMUGroupingDeatilRange)
                If gvRangeDetails IsNot Nothing AndAlso gvRangeDetails.Rows.Count > 0 Then
                    Dim objTr As New clsBullCMUGroupingDeatilRange()
                    For Each gvRows As GridViewRowInfo In gvRangeDetails.Rows
                        objTr.Range_Selection = clsCommon.myCstr(gvRows.Cells(colSelection).Value)
                        obj.arrSelectionRange.Add(objTr)
                    Next
                End If
                'If (clsBullCMUGroupingDeatilRange.SaveData(obj)) Then

                'End If
                Me.Close()
            Else
                Dim obj As New clsBullParameterGroup()
                obj.arrSelectionRange = New List(Of clsBullParameterGroupDeatilRange)
                If gvRangeDetails IsNot Nothing AndAlso gvRangeDetails.Rows.Count > 0 Then
                    Dim objTr As New clsBullParameterGroupDeatilRange()
                    For Each gvRows As GridViewRowInfo In gvRangeDetails.Rows
                        objTr.Range_Selection = clsCommon.myCstr(gvRows.Cells(colSelection).Value)
                        obj.arrSelectionRange.Add(objTr)
                    Next
                End If
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class