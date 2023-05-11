Imports common
Imports Telerik.WinControls.UI

Public Class FrmCategorySelect
#Region "Variables"
    Public lvl As Integer = 0
    Public strCode As String = ""
    Public arrIn As Dictionary(Of String, Object) = Nothing
    Public arrOut As Dictionary(Of String, Object) = Nothing
    Public isCancel As Boolean = False
#End Region

    Private Sub FrmCategorySelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If lvl = 1 Then
            RadGroupBox3.Text = "Category"
            LoadCategory()
        ElseIf lvl = 2 Then
            RadGroupBox3.Text = "Category Values"
            LoadValues()
        ElseIf lvl = 3 Then
            RadGroupBox3.Text = "Location"
            LoadSubSectionLocation()
        ElseIf lvl = 4 Then
            RadGroupBox3.Text = "Location"
            LoadSubSectionLocationJobwork()
        Else
            Throw New Exception("Not a Valid Exception")
        End If
        If arrIn IsNot Nothing AndAlso arrIn.Count > 0 Then
            rbtnCategorySelect.IsChecked = True
            For Each Str As String In arrIn.Keys
                For ii As Integer = 0 To gvCategory.Rows.Count - 1
                    If clsCommon.CompairString(Str, clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value)) = CompairStringResult.Equal Then
                        gvCategory.Rows(ii).Cells("SEL").Value = True
                        gvCategory.Rows(ii).Tag = arrIn(Str)
                    End If
                Next
            Next
        Else
            rbtnCategoryAll.IsChecked = True
        End If
    End Sub
    Sub LoadSubSectionLocationJobwork()
        gvCategory.DataSource = Nothing
        '============29/07/205 add location code cond. because main location should also show in finder against ticket BM00000007506
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where (Main_Location_Code = '" + strCode + "' and Is_Jobwork=1) order by Location_Code"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        'gvCategory.Columns("Type").ReadOnly = True
        'gvCategory.Columns("Type").Width = 100
        'gvCategory.Columns("Type").HeaderText = "Type"

        'gvCategory.Columns("Section").ReadOnly = True
        'gvCategory.Columns("Section").Width = 100
        'gvCategory.Columns("Section").HeaderText = "Section"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False

        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
        gvCategory.BestFitColumns()
    End Sub
    Sub LoadSubSectionLocation()
        gvCategory.DataSource = Nothing
        '============29/07/205 add location code cond. because main location should also show in finder against ticket BM00000007506
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,case when Is_Sub_Location='Y' then 'Sub Location' else case when Is_Section='Y' then 'Section' else '' end end as Type, Section_Code as Section from TSPL_LOCATION_MASTER where (Main_Location_Code = '" + strCode + "' or location_code='" + strCode + "') order by Type desc,Location_Code"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.Columns("Type").ReadOnly = True
        gvCategory.Columns("Type").Width = 100
        gvCategory.Columns("Type").HeaderText = "Type"

        gvCategory.Columns("Section").ReadOnly = True
        gvCategory.Columns("Section").Width = 100
        gvCategory.Columns("Section").HeaderText = "Section"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False

        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
        gvCategory.BestFitColumns()
    End Sub


    Sub LoadValues()
        gvCategory.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION AS NAME from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + strCode + "'"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False

        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
        gvCategory.BestFitColumns()
    End Sub

    Sub LoadCategory()
        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as CODE,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION AS NAME from TSPL_ITEM_CATEGORY_STRUCT_DETAIL  left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE where ITEM_CATEGORY_STRUCT_CODE='" + strCode + "' order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False

        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
        gvCategory.BestFitColumns()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If rbtnCategoryAll.IsChecked Then
            arrOut = Nothing
        Else
            arrOut = New Dictionary(Of String, Object)
            For ii As Integer = 0 To gvCategory.Rows.Count - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    arrOut.Add(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), gvCategory.Rows(ii).Tag)
                End If
            Next
        End If
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        isCancel = True
        Me.Close()
    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If lvl = 1 AndAlso clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub
End Class
