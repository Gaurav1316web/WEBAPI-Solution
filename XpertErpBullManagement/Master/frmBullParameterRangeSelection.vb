Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmBullParameterRangeSelection

    Public Form_ID As String = ""
    Public Range_Selection As String
    Public Range As Decimal
    Const colSelection As String = "colSelection"
    Public Const colRange As String = "colRange"
    'Public ArrRangeSelection As New Dictionary(Of Decimal, Decimal)
    Public ArrRangeSelection As New Dictionary(Of String, String)
    Public ArrRange As New List(Of String)
    Public isOK As Boolean = False


    Private Sub frmBullParameterRangeSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            loadBlankGrid()
            Dim flag As Boolean = False
            'For ii As Decimal = 0 To Range Step 1
            If Range > 0 Then
                If Range > gvRangeDetails.Rows.Count Then
                    For ii As Integer = 0 To (Range - gvRangeDetails.Rows.Count) - 1
                        gvRangeDetails.Rows.AddNew()
                        LoadData(ArrRangeSelection)
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub


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

            Dim repoDeciCol As GridViewTextBoxColumn
            repoDeciCol = New GridViewTextBoxColumn()
            repoDeciCol.Name = colRange
            repoDeciCol.Width = 100
            'repoDeciCol.DecimalPlaces = 0
            'repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 15
            'repoDeciCol.Step = 0
            'repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Range"
            repoDeciCol.ReadOnly = True
            gvRangeDetails.MasterTemplate.Columns.Add(repoDeciCol)

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
                isOK = True
                'ArrRangeSelection = New Dictionary(Of Decimal, Decimal)
                ArrRangeSelection = New Dictionary(Of String, String)
                ArrRange = New List(Of String)
                For ii As Integer = 0 To gvRangeDetails.Rows.Count - 1
                    'ArrRangeSelection.Add(clsCommon.myCdbl(gvRangeDetails.Rows(ii).Cells(colRange).Value), clsCommon.myCdbl(gvRangeDetails.Rows(ii).Cells(colSelection).Value))
                    ArrRangeSelection.Add(clsCommon.myCstr(gvRangeDetails.Rows(ii).Cells(colRange).Value), clsCommon.myCstr(gvRangeDetails.Rows(ii).Cells(colSelection).Value)) ' Convert to string
                    'ArrRange.Add(clsCommon.myCstr(gvRangeDetails.Rows(ii).Cells(colSelection).Value))
                Next
                Me.Close()
            Else
                If gvRangeDetails IsNot Nothing AndAlso gvRangeDetails.Rows.Count > 0 Then
                    ArrRangeSelection = New Dictionary(Of String, String)
                    For ii As Integer = 0 To gvRangeDetails.Rows.Count - 1

                        ArrRangeSelection.Add(clsCommon.myCstr(gvRangeDetails.Rows(ii).Cells(colRange).Value), clsCommon.myCstr(gvRangeDetails.Rows(ii).Cells(colSelection).Value)) ' Convert to string

                    Next
                End If
                isOK = True
                Me.Close()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gvRangeDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles gvRangeDetails.KeyDown
        If e.KeyCode = Keys.Enter Then
            gvRangeDetails.Rows.AddNew()
        End If
    End Sub


    Private Sub gvRangeDetails_RowsChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gvRangeDetails.RowsChanged
        UpdateSerialNumbers()
    End Sub

    Private Sub UpdateSerialNumbers()
        ' Iterate over the rows and update serial numbers
        For i As Integer = 0 To gvRangeDetails.RowCount - 1
            ' Assuming the serial number column is at index 0
            clsCommon.myCstr(gvRangeDetails.Rows(i).Cells(colRange).Value = (i + 1))
        Next
    End Sub

    Public Sub LoadData(ByVal arrSelection As Dictionary(Of String, String))
        Try
            If (arrSelection IsNot Nothing AndAlso arrSelection.Count > 0) Then
                For ii As Integer = 0 To arrSelection.Count - 1
                    gvRangeDetails.Rows(ii).Cells(colSelection).Value = arrSelection(arrSelection.Keys(ii))
                    ArrRange.Add(gvRangeDetails.Rows(ii).Cells(colSelection).Value)
                Next
            Else

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
