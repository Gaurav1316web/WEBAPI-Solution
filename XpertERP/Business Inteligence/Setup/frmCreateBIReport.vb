Imports common
Imports System.Data.SqlClient
Imports ActiveDatabaseSoftware.ActiveQueryBuilder
Imports System.IO
Imports Telerik.Charting
Imports Telerik.WinControls.UI
Imports System.Text
Imports Telerik.Pivot.Core

Public Class frmCreateBIReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colSelect As String = "colSelect"
    Const colCode As String = "colCode"
    Const colFilterCode As String = "colFilterCode"
    Const colFilterName As String = "colFilterName"
    Const colFilterType As String = "colFilterType"
    Const colMaxLevel As String = "colMaxLevel"
    Const colOrderBy As String = "colOrderBy"
    Const colIsOrderDesc As String = "colIsOrderDesc"
    Const colFigure As String = "colFigure"
    Const colDrillDownColumn As String = "colDrillDownColumn"
    Const colDrillDownTransactionTypeColumn As String = "colDrillDownTransactionTypeColumn"

    Const colLevel As String = "colLevel"
    Const colIsDateTypeColumn As String = "colIsDateTypeColumn"
    Const colIsNumericTypeColumn As String = "colIsNumericTypeColumn"
    Const colIsTotal As String = "colIsTotal"
    Const colChartColumn As String = "colChartColumn"
    Const colChartRow As String = "colChartRow"
    Const colTotalFormula As String = "colTotalFormula"
    Const colDateRange As String = "colDateRange"

    Const colTableName As String = "colTableName"
    Const colColumnName As String = "colColumnName"
    Const colOperatorName As String = "colOperatorName"
    Const colFromDateRange As String = "colFromDateRange"
    Const colToDateRange As String = "colToDateRange"


    Public isInsideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False

    Dim isNewEntry As Boolean = False
    Dim ConnString As String = ""
    Dim currLayout As Stream
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowAllReport As Boolean = False

    Private suspendEvents As Boolean = False
    Private provider As LocalDataSourceProvider
#End Region

    Private Sub RadForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Add New")
        ResetQueryBuilder()
        SplitPanel3.Collapsed = True
        SplitPanel2.Collapsed = True
        LoadType()
        LoadChartType()
        AddNew()
        LoadSetting()
        WireEvents()
        LoadCombineMode()
        LoadOrientation()
      
    End Sub

    Private Sub LoadOrientation()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Vertical"
        dr("Name") = "Vertical"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Horizontal"
        dr("Name") = "Horizontal"
        dt.Rows.Add(dr)

        cboOrientation.DataSource = dt
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Name"
    End Sub

    Private Sub LoadCombineMode()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Cluster"
        dr("Name") = "Cluster"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Stack"
        dr("Name") = "Stack"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Stack100"
        dr("Name") = "Stack100"
        dt.Rows.Add(dr)

        cboCombineMode.DataSource = dt
        cboCombineMode.ValueMember = "Code"
        cboCombineMode.DisplayMember = "Name"
    End Sub

    Private Sub LoadSetting()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Formula"
        dr("Name") = "Formula"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Options"
        dr("Name") = "Options"
        dt.Rows.Add(dr)

        cboSetting.DataSource = dt
        cboSetting.ValueMember = "Code"
        cboSetting.DisplayMember = "Name"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BICreateReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadChartType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Bar"
        dr("Name") = "Bar"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pie"
        dr("Name") = "Pie"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Line"
        dr("Name") = "Line"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Area"
        dr("Name") = "Area"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Donut"
        dr("Name") = "Donut"
        dt.Rows.Add(dr)

        cboChartType.DataSource = dt
        cboChartType.ValueMember = "Code"
        cboChartType.DisplayMember = "Name"
    End Sub

    Private Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Grid"
        dr("Name") = "Grid"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pivot Grid"
        dr("Name") = "Pivot Grid"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Chart"
        dr("Name") = "Chart"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub SetConnection()
        Try
            Dim line As String
            Dim objReader As New System.IO.StreamReader("config.Txp")
            Do While objReader.Peek() <> -1
                line = objReader.ReadLine()
                clsDBFuncationality.SetConnectionEncryptFormat(line)
                ConnString = clsDBFuncationality.connectionString
            Loop
            ''stuti regarding memory leakage
            objReader.Close()
            objReader.Dispose()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        MssqlMetadataProvider1.Connection = New SqlConnection(ConnString)

        ' setup the query builder with metadata and syntax providers
        queryBuilder.MetadataProvider = MssqlMetadataProvider1
        queryBuilder.SyntaxProvider = MssqlSyntaxProvider1

        ' kick the query builder to fill metadata tree
        queryBuilder.InitializeDatabaseSchemaTree()

    End Sub

    Public Sub ResetQueryBuilder()
        Try
            queryBuilder.MetadataContainer.Items.Clear()
            queryBuilder.OfflineMode = False
            queryBuilder.MetadataProvider = Nothing
            queryBuilder.SyntaxProvider = Nothing
        Catch ex As Exception

        End Try

        SetConnection()
    End Sub

    Sub SetControlByQuery()
        Try
            ' Update the query builder with manually edited query text
            If clsCommon.myLen(sqlQueryText.Text) <= 0 Then
                ResetQueryBuilder()
            Else
                queryBuilder.SQL = sqlQueryText.Text
            End If
        Catch ex As SQLParsingException
            ' Set caret to error position
            sqlQueryText.SelectionStart = ex.ErrorPos.pos
            ' Report error
            MessageBox.Show(ex.Message, "Parsing error")
        Catch ex1 As Exception
            clsCommon.MyMessageBoxShow(ex1.Message)
        End Try
    End Sub

    Private Sub MyTextBox1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        SetControlByQuery()
    End Sub

    Private Sub plainTextSQLBuilder1_SQLUpdated(ByVal sender As Object, ByVal e As EventArgs) Handles PlainTextSQLBuilder1.SQLUpdated
        ' Handle the event raised by SQL Builder object that the text of SQL query is changed

        ' update the text box
        sqlQueryText.Text = PlainTextSQLBuilder1.SQL
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

        Try
            If RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage2") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                gv1.DataSource = dt
                gv1.TableElement.TableHeaderHeight = 30
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = True
                gv1.BestFitColumns()

                Dim tempGV As New RadGridView
                tempGV.DataSource = dt.Copy()
                tempGV.LoadLayout(currLayout)
                Dim arr As New List(Of String)
                For ii As Integer = 0 To tempGV.Columns.Count - 1
                    arr.Add(tempGV.Columns(ii).Name)
                Next
                Dim isSameLayout As Boolean = True
                For ii As Integer = 0 To dt.Columns.Count - 1
                    If Not arr.Contains(dt.Columns(ii).ColumnName) Then
                        isSameLayout = False
                        Exit For
                    End If
                Next

                tempGV.Dispose()
                If isSameLayout Then
                    gv1.LoadLayout(currLayout)
                End If
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For ii As Integer = 0 To gvFilter.RowCount - 1
                    'If arr.Contains(clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value)) Then
                    If clsCommon.myCBool(gvFilter.Rows(ii).Cells(colIsTotal).Value) Then
                        If clsCommon.myLen(gvFilter.Rows(ii).Cells(colTotalFormula).Value) > 0 Then
                            'Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value), "{0:F2}", clsCommon.myCstr(gvFilter.Rows(ii).Cells(colTotalFormula).Value))
                            ''GridViewSummaryItem taxClient1Item = new GridViewSummaryItem("CLIENT1", "", "Sum(CLIENT1) * 0.2");
                            'summaryRowItem.Add(item1)
                            Dim summaryItem As New GridViewSummaryItem()
                            summaryItem.FormatString = "{0:F2}"
                            summaryItem.Name = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value)
                            summaryItem.AggregateExpression = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colTotalFormula).Value)
                            summaryRowItem.Add(summaryItem)
                        Else
                            Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value), "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(item1)
                        End If
                    End If
                    'End If
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            ElseIf RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage3") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                pg1.DataSource = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                pg1.LoadLayout(currLayout)
            ElseIf RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage6") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                If dt IsNot Nothing AndAlso dt.Columns.Count > 0 Then
                    Dim arrAdded As New List(Of String)
                    For Each dc As DataColumn In dt.Columns
                        Dim isFound As Boolean = False
                        For jj As Integer = 0 To gvFilter.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvFilter.Rows(jj).Cells(colCode).Value), dc.ColumnName) = CompairStringResult.Equal Then
                                isFound = True
                                arrAdded.Add(dc.ColumnName.ToUpper())
                                Exit For
                            End If
                        Next
                        If Not isFound Then
                            gvFilter.Rows.AddNew()
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colCode).Value = dc.ColumnName
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsDateTypeColumn).Value = IIf((dc.DataType Is System.Type.GetType("System.DateTime") OrElse (dc.DataType Is System.Type.GetType("System.Date"))), True, False)
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsNumericTypeColumn).Value = IIf((dc.DataType Is System.Type.GetType("System.Double") OrElse (dc.DataType Is System.Type.GetType("System.Int32")) OrElse (dc.DataType Is System.Type.GetType("System.Decimal"))), True, False)

                            arrAdded.Add(dc.ColumnName.ToUpper())
                        End If
                    Next
                    For jj As Integer = gvFilter.Rows.Count - 1 To 0 Step -1
                        If Not arrAdded.Contains(clsCommon.myCstr(gvFilter.Rows(jj).Cells(colCode).Value).ToUpper()) Then
                            gvFilter.Rows.RemoveAt(jj)
                        End If
                    Next
                End If
            ElseIf RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage5") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then

                'Dim strOldValue As String = clsCommon.myCstr(cboCombineMode.SelectedValue)
                'Dim strOldCategory As String = clsCommon.myCstr(cboOrientation.SelectedValue)
                'Dim strOldSeries As String = clsCommon.myCstr(cboSeries.SelectedValue)
                ''LoadChartCombo()
                'cboCombineMode.SelectedValue = strOldValue
                'cboOrientation.SelectedValue = strOldCategory
                'cboSeries.SelectedValue = strOldSeries
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    'Sub LoadChartCombo()
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
    '    Dim dtNew As New DataTable
    '    dtNew.Columns.Add("Code", GetType(String))
    '    dtNew.Rows.Add("")
    '    If dt IsNot Nothing AndAlso dt.Columns.Count > 0 Then
    '        For Each dc As DataColumn In dt.Columns
    '            dtNew.Rows.Add(dc.ColumnName)
    '        Next




    '        cboCategory.DataSource = dtNew.Copy()
    '        cboCategory.ValueMember = "Code"
    '        cboCategory.DisplayMember = "Code"


    '        cboSeries.DataSource = dtNew.Copy()
    '        cboSeries.ValueMember = "Code"
    '        cboSeries.DisplayMember = "Code"

    '    End If
    'End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        txtCode.Value = ""
        txtDesc.Text = ""
        txtReportModule.Value = ""
        cboType.SelectedValue = ""
        sqlQueryText.Text = ""
        currLayout = Nothing
        queryBuilder.SQL = sqlQueryText.Text
        ResetQueryBuilder()
        LoadBlankGrid()
        LoadBlankGridFilterInner()
        RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
        isCellValueChangedOpen = False
        txtDrilldownReport.Value = ""
        txtDrilldownFilter.Value = ""
        lblDrillDownColumn.Text = ""
        lblTransactionTypeColumn.Text = ""
        rbtnDrillDownNA.IsChecked = True
    End Sub

    Sub LoadBlankGrid()
        gvFilter.Rows.Clear()
        gvFilter.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = " "
        repoSelect.Width = 30
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoSelect)

        Dim repoColumn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Column"
        repoColumn.Name = colCode
        repoColumn.ReadOnly = True
        repoColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        repoColumn.Width = 200
        gvFilter.MasterTemplate.Columns.Add(repoColumn)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:N0}"
        repoNumBox.HeaderText = "Order By"
        repoNumBox.Name = colOrderBy
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.Minimum = 0
        repoNumBox.Width = 80
        gvFilter.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoIsTotal As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTotal.FormatString = ""
        repoIsTotal.HeaderText = "Descending Order"
        repoIsTotal.Width = 50
        repoIsTotal.Name = colIsOrderDesc
        repoIsTotal.ReadOnly = False
        repoIsTotal.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoIsTotal)

        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Drill Down Column"
        repoSelect.Width = 100
        repoSelect.Name = colDrillDownColumn
        repoSelect.ReadOnly = False
        repoSelect.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoSelect)

        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Drill Down Transaction Type"
        repoSelect.Width = 100
        repoSelect.Name = colDrillDownTransactionTypeColumn
        repoSelect.ReadOnly = False
        repoSelect.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoSelect)

        Dim repoFilterColumn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFilterColumn.FormatString = ""
        repoFilterColumn.HeaderText = "Filter Code"
        repoFilterColumn.Name = colFilterCode
        repoFilterColumn.ReadOnly = False
        repoFilterColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFilterColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFilterColumn.Width = 100
        gvFilter.MasterTemplate.Columns.Add(repoFilterColumn)

        Dim repoFilterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFilterName.FormatString = ""
        repoFilterName.HeaderText = "Filter Name"
        repoFilterName.Name = colFilterName
        repoFilterName.ReadOnly = True
        repoFilterName.Width = 150
        gvFilter.MasterTemplate.Columns.Add(repoFilterName)

        repoColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Filter Type"
        repoColumn.Name = colFilterType
        repoColumn.ReadOnly = True
        repoColumn.IsVisible = False
        gvFilter.MasterTemplate.Columns.Add(repoColumn)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:N0}"
        repoNumBox.HeaderText = "Max Tree Level"
        repoNumBox.Name = colMaxLevel
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.Minimum = 0
        gvFilter.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:N0}"
        repoNumBox.HeaderText = "Level"
        repoNumBox.Name = colLevel
        repoNumBox.ReadOnly = False
        repoNumBox.Width = 50
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 10
        gvFilter.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoIsDateRange As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsDateRange.FormatString = ""
        repoIsDateRange.HeaderText = "Is Date Range"
        repoIsDateRange.Width = 100
        repoIsDateRange.Name = colDateRange
        repoIsDateRange.ReadOnly = False
        repoIsDateRange.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoIsDateRange)

        Dim repoIsDateTypeColumn As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsDateTypeColumn.FormatString = ""
        repoIsDateTypeColumn.HeaderText = "Is Date Type"
        repoIsDateTypeColumn.Width = 100
        repoIsDateTypeColumn.Name = colIsDateTypeColumn
        repoIsDateTypeColumn.ReadOnly = True
        repoIsDateTypeColumn.IsVisible = False
        gvFilter.MasterTemplate.Columns.Add(repoIsDateTypeColumn)

        Dim repoIsNumericTypeColumn As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsNumericTypeColumn.FormatString = ""
        repoIsNumericTypeColumn.HeaderText = "Is Numeric Type"
        repoIsNumericTypeColumn.Width = 100
        repoIsNumericTypeColumn.Name = colIsNumericTypeColumn
        repoIsNumericTypeColumn.ReadOnly = True
        repoIsNumericTypeColumn.IsVisible = False
        gvFilter.MasterTemplate.Columns.Add(repoIsNumericTypeColumn)

        repoIsTotal = New GridViewCheckBoxColumn()
        repoIsTotal.FormatString = ""
        repoIsTotal.HeaderText = "Total"
        repoIsTotal.Width = 100
        repoIsTotal.Name = colIsTotal
        repoIsTotal.ReadOnly = False
        repoIsTotal.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoIsTotal)

        repoIsTotal = New GridViewCheckBoxColumn()
        repoIsTotal.FormatString = ""
        repoIsTotal.HeaderText = "Chart Column"
        repoIsTotal.Width = 80
        repoIsTotal.Name = colChartColumn
        repoIsTotal.ReadOnly = False
        repoIsTotal.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoIsTotal)

        repoIsTotal = New GridViewCheckBoxColumn()
        repoIsTotal.FormatString = ""
        repoIsTotal.HeaderText = "Chart Row"
        repoIsTotal.Width = 80
        repoIsTotal.Name = colChartRow
        repoIsTotal.ReadOnly = False
        repoIsTotal.IsVisible = True
        gvFilter.MasterTemplate.Columns.Add(repoIsTotal)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Figure"
        repoRowType.Name = colFigure
        repoRowType.Width = 50
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetFigure()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvFilter.MasterTemplate.Columns.Add(repoRowType) '2

        Dim repoTotalFormula As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTotalFormula.FormatString = ""
        repoTotalFormula.HeaderText = "Total Formula"
        repoTotalFormula.Name = colTotalFormula
        repoTotalFormula.ReadOnly = False
        repoTotalFormula.Width = 500
        gvFilter.MasterTemplate.Columns.Add(repoTotalFormula)

        gvFilter.AllowDeleteRow = True
        gvFilter.AllowAddNewRow = False
        gvFilter.ShowGroupPanel = False
        gvFilter.AllowColumnReorder = False
        gvFilter.AllowRowReorder = False
        gvFilter.EnableSorting = False
        gvFilter.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFilter.MasterTemplate.ShowRowHeaderColumn = False
        gvFilter.TableElement.TableHeaderHeight = 40
    End Sub

    Private Function GetFigure() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "Hundred"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 2
        dr("Name") = "Thousand"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 3
        dr("Name") = "Ten Thousand"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 4
        dr("Name") = "Lakh"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 5
        dr("Name") = "Ten Lakh"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 6
        dr("Name") = "Crore"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 7
        dr("Name") = "Ten Crore"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGridFilterInner()
        gvFilterInner.Rows.Clear()
        gvFilterInner.Columns.Clear()

        Dim repoColumn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Filter Name"
        repoColumn.Name = colCode
        repoColumn.ReadOnly = True
        repoColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        repoColumn.Width = 200
        gvFilterInner.MasterTemplate.Columns.Add(repoColumn)

        repoColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Table"
        repoColumn.Name = colTableName
        repoColumn.ReadOnly = False
        repoColumn.Width = 100
        gvFilterInner.MasterTemplate.Columns.Add(repoColumn)

        repoColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Column"
        repoColumn.Name = colColumnName
        repoColumn.ReadOnly = False
        repoColumn.Width = 100
        gvFilterInner.MasterTemplate.Columns.Add(repoColumn)

        repoColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Opeartor"
        repoColumn.Name = colOperatorName
        repoColumn.ReadOnly = False
        repoColumn.IsVisible = True
        repoColumn.Width = 100
        gvFilterInner.MasterTemplate.Columns.Add(repoColumn)

        Dim repoFilterColumn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFilterColumn.FormatString = ""
        repoFilterColumn.HeaderText = "Filter Code"
        repoFilterColumn.Name = colFilterCode
        repoFilterColumn.ReadOnly = False
        repoFilterColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFilterColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFilterColumn.Width = 100
        gvFilterInner.MasterTemplate.Columns.Add(repoFilterColumn)

        Dim repoFilterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFilterName.FormatString = ""
        repoFilterName.HeaderText = "Filter Name"
        repoFilterName.Name = colFilterName
        repoFilterName.ReadOnly = True
        repoFilterName.Width = 150
        gvFilterInner.MasterTemplate.Columns.Add(repoFilterName)

        repoColumn = New GridViewTextBoxColumn()
        repoColumn.FormatString = ""
        repoColumn.HeaderText = "Filter Type"
        repoColumn.Name = colFilterType
        repoColumn.ReadOnly = True
        repoColumn.IsVisible = False
        gvFilterInner.MasterTemplate.Columns.Add(repoColumn)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:N0}"
        repoNumBox.HeaderText = "Max Tree Level"
        repoNumBox.Name = colMaxLevel
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.Minimum = 0
        gvFilterInner.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoIsDateRange As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsDateRange.FormatString = ""
        repoIsDateRange.HeaderText = "Is Date Range"
        repoIsDateRange.Width = 100
        repoIsDateRange.Name = colDateRange
        repoIsDateRange.ReadOnly = False
        repoIsDateRange.IsVisible = True
        gvFilterInner.MasterTemplate.Columns.Add(repoIsDateRange)

        repoIsDateRange = New GridViewCheckBoxColumn()
        repoIsDateRange.FormatString = ""
        repoIsDateRange.HeaderText = "Is From Date"
        repoIsDateRange.Width = 100
        repoIsDateRange.Name = colFromDateRange
        repoIsDateRange.ReadOnly = False
        repoIsDateRange.IsVisible = True
        gvFilterInner.MasterTemplate.Columns.Add(repoIsDateRange)


        repoIsDateRange = New GridViewCheckBoxColumn()
        repoIsDateRange.FormatString = ""
        repoIsDateRange.HeaderText = "Is To Date"
        repoIsDateRange.Width = 100
        repoIsDateRange.Name = colToDateRange
        repoIsDateRange.ReadOnly = False
        repoIsDateRange.IsVisible = True
        gvFilterInner.MasterTemplate.Columns.Add(repoIsDateRange)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:N0}"
        repoNumBox.HeaderText = "Level"
        repoNumBox.Name = colLevel
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = False
        repoNumBox.Width = 50
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 10
        gvFilterInner.MasterTemplate.Columns.Add(repoNumBox)

        gvFilterInner.AllowDeleteRow = True
        gvFilterInner.AllowAddNewRow = False
        gvFilterInner.ShowGroupPanel = False
        gvFilterInner.AllowColumnReorder = False
        gvFilterInner.AllowRowReorder = False
        gvFilterInner.EnableSorting = False
        gvFilterInner.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFilterInner.MasterTemplate.ShowRowHeaderColumn = False
        gvFilterInner.TableElement.TableHeaderHeight = 40
    End Sub


    Sub SetTab()
        MyLabel6.Visible = False
        cboSetting.Visible = False
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        RadPageView1.Pages("RadPageViewPage5").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Grid") = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Pivot Grid") = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
            MyLabel6.Visible = True
            cboSetting.Visible = True
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Pivot Grid & Chart") = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Chart") = CompairStringResult.Equal Then
            RadPageView1.Pages("RadPageViewPage5").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsCreateBIReport()
            obj = clsCreateBIReport.GetData(strCode, isShowAllReport, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isInsideLoadData = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                cboType.SelectedValue = obj.Type
                sqlQueryText.Text = obj.Qry
                currLayout = obj.Layout
                txtReportModule.Value = obj.Report_Module
                chkDashboard.Checked = obj.is_For_Dashboard
                txtDrilldownReport.Value = obj.Drill_Down_Report
                txtDrilldownFilter.Value = obj.Drill_Down_Filter
                lblDrillDownColumn.Text = obj.Drill_Down_Column
                lblTransactionTypeColumn.Text = obj.Drill_Down_Transaction_Type
                SetControlByQuery()
                SetTab()
                If clsCommon.CompairString(obj.Type, "Grid") = CompairStringResult.Equal Then
                    gv1.LoadLayout(currLayout)
                ElseIf clsCommon.CompairString(obj.Type, "Pivot Grid") = CompairStringResult.Equal Then
                    pg1.LoadLayout(currLayout)
                ElseIf clsCommon.CompairString(obj.Type, "Pivot Grid & Chart") = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(obj.Type, "Chart") = CompairStringResult.Equal Then
                    cboChartType.SelectedValue = obj.Chart_Type
                    cboCombineMode.SelectedValue = obj.Chart_Combine_Mode
                    cboOrientation.SelectedValue = obj.Chart_Orientation
                    txtLableRotation.Value = obj.Chart_Label_Rotation
                    chkShowLables.Checked = obj.Chart_Show_Label
                    chkScroll.Checked = obj.Chart_Show_Scroll
                End If

                rbtnDrillDownNA.IsChecked = True
                If obj.Drill_Down_Type = 1 Then
                    rbtnDrillDownReport.IsChecked = True
                ElseIf obj.Drill_Down_Type = 2 Then
                    rbtnDrillDownTransaction.IsChecked = True
                End If

                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsCreateBIReportFilterDetails In obj.arr
                        gvFilter.Rows.AddNew()
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colSelect).Value = objtr.Is_Select
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colCode).Value = objtr.Filter_Column
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFilterCode).Value = objtr.Against_Filter
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFilterName).Value = objtr.Against_Filter_Name

                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFilterType).Value = objtr.Against_Filter_Type
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colMaxLevel).Value = objtr.Against_Filter_Max_Level
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colLevel).Value = objtr.Tree_Level
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colOrderBy).Value = objtr.Order_By
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsOrderDesc).Value = objtr.Is_Order_Desc
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFigure).Value = objtr.Figure_In
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsDateTypeColumn).Value = objtr.Is_Date_Type_Column
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colDateRange).Value = objtr.Is_Date_Range
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsNumericTypeColumn).Value = objtr.Is_Numeric_Type_Column
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colIsTotal).Value = objtr.Is_Show_Total
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colTotalFormula).Value = objtr.Total_Formula

                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colChartColumn).Value = objtr.Chart_Column
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colChartRow).Value = objtr.Chart_Row

                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colDrillDownColumn).Value = False
                        If clsCommon.CompairString(objtr.Filter_Column, obj.Drill_Down_Column) = CompairStringResult.Equal Then
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colDrillDownColumn).Value = True
                        End If
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colDrillDownTransactionTypeColumn).Value = False
                        If clsCommon.CompairString(objtr.Filter_Column, obj.Drill_Down_Transaction_Type) = CompairStringResult.Equal Then
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colDrillDownTransactionTypeColumn).Value = True
                        End If
                    Next
                End If

                If obj.arrInner IsNot Nothing AndAlso obj.arrInner.Count > 0 Then
                    For Each objtr As clsCreateBIReportInnerFilterDetails In obj.arrInner
                        gvFilterInner.Rows.AddNew()
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colCode).Value = objtr.Filter_SNo
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colTableName).Value = objtr.Table_Name
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colColumnName).Value = objtr.Column_Name
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colOperatorName).Value = objtr.Operator_Name
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colFilterCode).Value = objtr.Against_Filter
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colFilterName).Value = objtr.Against_Filter_Name
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colFilterType).Value = objtr.Against_Filter_Type
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colMaxLevel).Value = objtr.Against_Filter_Max_Level
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colLevel).Value = objtr.Tree_Level
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colDateRange).Value = objtr.Is_Date_Range
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colFromDateRange).Value = objtr.Is_From_Date
                        gvFilterInner.Rows(gvFilterInner.RowCount - 1).Cells(colToDateRange).Value = objtr.Is_To_Date
                    Next
                End If
                If clsCommon.CompairString(obj.Type, "Chart") = CompairStringResult.Equal Then
                    ShoChartsData()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        cboType.SelectedValue = ""
        sqlQueryText.Text = ""
        txtReportModule.Value = ""
        gvFilter.Rows.Clear()
        gvFilter.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        chkDashboard.Checked = False
        txtReportModule.Enabled = False
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsCreateBIReport()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Type = cboType.SelectedValue
                obj.Qry = sqlQueryText.Text
                obj.Report_Module = txtReportModule.Value
                obj.is_For_Dashboard = chkDashboard.Checked
                obj.Drill_Down_Report = txtDrilldownReport.Value
                obj.Drill_Down_Filter = txtDrilldownFilter.Value
                If chkDashboard.Checked Then
                    obj.Report_Module = ""
                End If

                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Grid") = CompairStringResult.Equal Then
                    obj.Layout = New MemoryStream()
                    gv1.SaveLayout(obj.Layout)
                    obj.Layout.Seek(0, System.IO.SeekOrigin.Begin)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Pivot Grid") = CompairStringResult.Equal Then
                    obj.Layout = New MemoryStream()
                    pg1.SaveLayout(obj.Layout)
                    obj.Layout.Seek(0, System.IO.SeekOrigin.Begin)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Pivot Grid & Chart") = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Chart") = CompairStringResult.Equal Then
                    'obj.Chart_Type = clsCommon.myCstr(cboChartType.SelectedValue)
                    'obj.Chart_Category_Member = clsCommon.myCstr(cboOrientation.SelectedValue)
                    'obj.Chart_Series_Member = clsCommon.myCstr(cboSeries.SelectedValue)
                    'obj.Chart_Value_Member = clsCommon.myCstr(cboCombineMode.SelectedValue)
                    obj.Chart_Type = cboChartType.SelectedValue
                    obj.Chart_Combine_Mode = cboCombineMode.SelectedValue
                    obj.Chart_Orientation = cboOrientation.SelectedValue
                    obj.Chart_Label_Rotation = txtLableRotation.Value
                    obj.Chart_Show_Label = chkShowLables.Checked
                    obj.Chart_Show_Scroll = chkScroll.Checked
                End If
                obj.arr = New List(Of clsCreateBIReportFilterDetails)
                For ii As Integer = 0 To gvFilter.RowCount - 1
                    Dim objtr As New clsCreateBIReportFilterDetails
                    objtr.Is_Select = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colSelect).Value)
                    objtr.Filter_Column = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value)
                    objtr.Against_Filter = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colFilterCode).Value)
                    objtr.Tree_Level = clsCommon.myCdbl(gvFilter.Rows(ii).Cells(colLevel).Value)
                    objtr.Is_Date_Range = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colDateRange).Value)
                    objtr.Is_Date_Type_Column = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colIsDateTypeColumn).Value)
                    objtr.Total_Formula = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colTotalFormula).Value)
                    objtr.Is_Numeric_Type_Column = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colIsNumericTypeColumn).Value)
                    objtr.Is_Show_Total = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colIsTotal).Value)
                    objtr.Chart_Column = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colChartColumn).Value)
                    objtr.Chart_Row = clsCommon.myCBool(gvFilter.Rows(ii).Cells(colChartRow).Value)
                    objtr.Order_By = clsCommon.myCdbl(gvFilter.Rows(ii).Cells(colOrderBy).Value)
                    objtr.Is_Order_Desc = clsCommon.myCdbl(gvFilter.Rows(ii).Cells(colIsOrderDesc).Value)
                    objtr.Figure_In = clsCommon.myCdbl(gvFilter.Rows(ii).Cells(colFigure).Value)
                    If clsCommon.myCBool(gvFilter.Rows(ii).Cells(colDrillDownColumn).Value) Then
                        lblDrillDownColumn.Text = objtr.Filter_Column
                    End If
                    If clsCommon.myCBool(gvFilter.Rows(ii).Cells(colDrillDownTransactionTypeColumn).Value) Then
                        lblTransactionTypeColumn.Text = objtr.Filter_Column
                    End If
                    obj.arr.Add(objtr)
                Next
                obj.Drill_Down_Column = lblDrillDownColumn.Text
                obj.Drill_Down_Transaction_Type = lblTransactionTypeColumn.Text


                obj.Drill_Down_Type = 0
                If rbtnDrillDownReport.IsChecked Then
                    obj.Drill_Down_Type = 1
                ElseIf rbtnDrillDownTransaction.IsChecked Then
                    obj.Drill_Down_Type = 2
                End If

                obj.arrInner = New List(Of clsCreateBIReportInnerFilterDetails)
                For ii As Integer = 0 To gvFilterInner.RowCount - 1
                    Dim objtr As New clsCreateBIReportInnerFilterDetails
                    objtr.Filter_SNo = clsCommon.myCstr(gvFilterInner.Rows(ii).Cells(colCode).Value)
                    objtr.Table_Name = clsCommon.myCstr(gvFilterInner.Rows(ii).Cells(colTableName).Value)
                    objtr.Column_Name = clsCommon.myCstr(gvFilterInner.Rows(ii).Cells(colColumnName).Value)
                    objtr.Operator_Name = clsCommon.myCstr(gvFilterInner.Rows(ii).Cells(colOperatorName).Value)
                    objtr.Against_Filter = clsCommon.myCstr(gvFilterInner.Rows(ii).Cells(colFilterCode).Value)
                    objtr.Tree_Level = clsCommon.myCdbl(gvFilterInner.Rows(ii).Cells(colLevel).Value)
                    objtr.Is_Date_Range = clsCommon.myCBool(gvFilterInner.Rows(ii).Cells(colDateRange).Value)
                    objtr.Is_From_Date = clsCommon.myCBool(gvFilterInner.Rows(ii).Cells(colFromDateRange).Value)
                    objtr.Is_To_Date = clsCommon.myCBool(gvFilterInner.Rows(ii).Cells(colToDateRange).Value)
                    obj.arrInner.Add(objtr)
                Next

                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
                MDI.LoadImageList()
                MDI.LoadMenu()
                ''stuti regarding memory leakage
                obj.Layout.Close()
                obj.Layout.Dispose()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
            cboType.Focus()
            Throw New Exception("Please select report type")
        End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            cboType.Focus()
            Throw New Exception("Please select report description Type")
        End If
        If Not chkDashboard.Checked AndAlso clsCommon.myLen(txtReportModule.Value) <= 0 Then
            txtReportModule.Focus()
            Throw New Exception("Please select Report Module")
        End If
        If clsCommon.myLen(txtDrilldownReport.Value) > 0 Then
            If clsCommon.myLen(txtDrilldownFilter.Value) <= 0 Then
                txtDrilldownFilter.Focus()
                Throw New Exception("Please select drill down filter")
            End If
        End If

        Return True
    End Function

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged
        SetTab()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            'Dim qst As String = "select count(*) from TSPL_CREATE_BI_REPORT where Code='" + txtCode.Value + "'"
            'Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            'If count = 0 Then
            '    txtCode.MyReadOnly = False
            'Else
            '    txtCode.MyReadOnly = True
            'End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Code,Description,Type from TSPL_CREATE_BI_REPORT"
        Dim whrclas As String = ""
        If Not isShowAllReport Then
            whrclas += "  TSPL_CREATE_BI_REPORT.Is_Create_By_Developer = 0"
        End If
        LoadData(clsCommon.ShowSelectForm("CreateBIRpt", qry, "Code", whrclas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Delete the current data" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCreateBIReport.DeleteData(txtCode.Value)
                    AddNew()
                    MDI.LoadMenu()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FRM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.A Then
            AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt And e.KeyCode = Keys.X Then
            Dim frm As New FrmBIReport
            frm.SetUserMgmt("BI-RPT")
            frm.obj = clsCreateBIReport.GetData(txtCode.Value, True, NavigatorType.Current)
            frm.Show()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                isShowAllReport = Not isShowAllReport
                RadButton2.Visible = isShowAllReport
            End If
        End If
    End Sub

    Private Sub gvFilter_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvFilter.CellFormatting
        Try
            If e.Column Is gvFilter.Columns(colDateRange) Then
                If clsCommon.myCBool(gvFilter.CurrentRow.Cells(colIsDateTypeColumn).Value) Then
                    gvFilter.CurrentRow.Cells(colDateRange).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colDateRange).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colDateRange).Value = False
                End If
            ElseIf e.Column Is gvFilter.Columns(colIsTotal) Then
                If clsCommon.myCBool(gvFilter.CurrentRow.Cells(colIsNumericTypeColumn).Value) Then
                    gvFilter.CurrentRow.Cells(colIsTotal).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colIsTotal).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colIsTotal).Value = False
                End If

            ElseIf e.Column Is gvFilter.Columns(colTotalFormula) Then
                If clsCommon.myCBool(gvFilter.CurrentRow.Cells(colIsTotal).Value) Then
                    gvFilter.CurrentRow.Cells(colTotalFormula).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colTotalFormula).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colTotalFormula).Value = ""
                End If
            ElseIf e.Column Is gvFilter.Columns(colFilterCode) Then
                If clsCommon.myCBool(gvFilter.CurrentRow.Cells(colSelect).Value) Then
                    gvFilter.CurrentRow.Cells(colFilterCode).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colFilterCode).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colFilterCode).Value = ""
                    gvFilter.CurrentRow.Cells(colFilterName).Value = ""
                    gvFilter.CurrentRow.Cells(colMaxLevel).Value = Nothing
                    gvFilter.CurrentRow.Cells(colLevel).Value = Nothing
                End If
            ElseIf e.Column Is gvFilter.Columns(colLevel) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvFilter.CurrentRow.Cells(colFilterType).Value), "T") = CompairStringResult.Equal Then
                    gvFilter.CurrentRow.Cells(colLevel).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colLevel).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colLevel).Value = Nothing
                End If
            ElseIf e.Column Is gvFilter.Columns(colFigure) Then
                If clsCommon.myCBool(gvFilter.CurrentRow.Cells(colIsNumericTypeColumn).Value) Then
                    gvFilter.CurrentRow.Cells(colFigure).ReadOnly = False
                Else
                    gvFilter.CurrentRow.Cells(colFigure).ReadOnly = True
                    gvFilter.CurrentRow.Cells(colFigure).Value = 0
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtReportModule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReportModule._MYValidating
        Try
            Dim qry As String = "select TSPL_PROGRAM_MASTER.Program_Code as ReportModule,TSPL_PROGRAM_MASTER.Program_Name as ReportModuleDescription, TSPL_PROGRAM_MASTER.Parent_Code as ModuleCode,OutTabale.Program_Name as ModuleName from TSPL_PROGRAM_MASTER "
            qry += " inner join TSPL_PROGRAM_MASTER as OutTabale on OutTabale.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code"
            qry += " inner join tspl_Module_Permission on tspl_Module_Permission.Module_Name= TSPL_PROGRAM_MASTER.Parent_Code"
            Dim WhrCls As String = " tspl_Module_Permission.IsAvailable=1 and TSPL_PROGRAM_MASTER.Type='SM' and TSPL_PROGRAM_MASTER.PROGRAM_NAME like '%Report%'  "
            txtReportModule.Value = clsCommon.ShowSelectForm("ReportModule", qry, "ReportModule", WhrCls, txtReportModule.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Sub ShoChartsData()
        Try
            RadChartView2.Series.Clear()
            AddHandler RadChartView2.LabelFormatting, AddressOf radChartView_LabelFormatting
            RadChartView2.Area.View.Palette = New CustomPalette()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strColumn As String = ""
                Dim arrRow As New List(Of String)
                For ii As Integer = 0 To gvFilter.RowCount - 1
                    If clsCommon.myCBool(gvFilter.Rows(ii).Cells(colChartColumn).Value) Then
                        strColumn = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value)
                    ElseIf clsCommon.myCBool(gvFilter.Rows(ii).Cells(colChartRow).Value) Then
                        arrRow.Add(clsCommon.myCstr(gvFilter.Rows(ii).Cells(colCode).Value))
                    End If
                Next
                If clsCommon.myLen(strColumn) <= 0 Then
                    Throw New Exception("Please select chart Column")
                End If
                If arrRow Is Nothing OrElse arrRow.Count <= 0 Then
                    Throw New Exception("Please select at least one chart row")
                End If
                If clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Pie") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Donut") = CompairStringResult.Equal Then
                    If arrRow.Count <> 1 Then
                        Throw New Exception("Please select at only one chart row")
                    End If
                End If
                RadChartView2.ShowTitle = True
                RadChartView2.ChartElement.TitlePosition = TitlePosition.Top
                RadChartView2.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                RadChartView2.Title = txtDesc.Text
                Dim smartLabelsController As New SmartLabelsController()
                RadChartView2.Controllers.Add(smartLabelsController)
                RadChartView2.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                Dim combineMode As ChartSeriesCombineMode = ChartSeriesCombineMode.None
                If clsCommon.CompairString(clsCommon.myCstr(cboCombineMode.SelectedValue), "Cluster") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Cluster
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboCombineMode.SelectedValue), "Stack") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Stack
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboCombineMode.SelectedValue), "Stack100") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Stack100
                End If

                If clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Bar") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    strategy = New VerticalAdjusmentLabelsStrategy()
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim barSeries As New BarSeries()
                            barSeries.Name = strVal
                            barSeries.LegendTitle = strVal
                            barSeries.ValueMember = strVal
                            barSeries.CategoryMember = strColumn
                            barSeries.DataSource = dt
                            barSeries.CombineMode = combineMode
                            barSeries.ShowLabels = chkShowLables.Checked

                            barSeries.DrawLinesToLabels = True
                            barSeries.SyncLinesToLabelsColor = True
                            RadChartView2.Series.Add(barSeries)
                        Next
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation()
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Line") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    strategy = New VerticalAdjusmentLabelsStrategy()
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim lineSeries As New LineSeries()
                            lineSeries.Name = strVal
                            lineSeries.LegendTitle = strVal
                            lineSeries.ValueMember = strVal
                            lineSeries.CategoryMember = strColumn
                            lineSeries.DataSource = dt
                            lineSeries.ShowLabels = True
                            lineSeries.CombineMode = combineMode
                            lineSeries.ShowLabels = chkShowLables.Checked
                            lineSeries.DrawLinesToLabels = True
                            lineSeries.SyncLinesToLabelsColor = True
                            RadChartView2.Series.Add(lineSeries)
                        Next
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation()
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Area") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim AreaSeries As New AreaSeries()
                            AreaSeries.Name = strVal
                            AreaSeries.LegendTitle = strVal
                            AreaSeries.ValueMember = strVal
                            AreaSeries.CategoryMember = strColumn
                            AreaSeries.DataSource = dt
                            AreaSeries.BorderWidth = 2
                            AreaSeries.ShowLabels = True
                            AreaSeries.CombineMode = combineMode
                            AreaSeries.ShowLabels = chkShowLables.Checked
                            AreaSeries.DrawLinesToLabels = True
                            AreaSeries.SyncLinesToLabelsColor = True
                            RadChartView2.Series.Add(AreaSeries)
                        Next
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation()
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Pie") = CompairStringResult.Equal Then
                    strategy = New PieTwoLabelColumnsStrategy()
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.ShowLegend = chkShowLables.Checked
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)

                    Dim series As New PieSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.ValueMember = arrRow(0)
                    series.DataSource = dt
                    series.ShowLabels = True
                    series.DrawLinesToLabels = True
                    series.SyncLinesToLabelsColor = True
                    series.DisplayMember = strColumn
                    RadChartView2.Series.Add(series)
                    'For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                    '    item.Title = clsCommon.myCstr(row(strColumn))
                    'Next
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Donut") = CompairStringResult.Equal Then
                    strategy = New PieTwoLabelColumnsStrategy()
                    Dim series As New DonutSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.InnerRadiusFactor = 50 / 100
                    series.ValueMember = arrRow(0)
                    series.DataSource = dt
                    series.ShowLabels = True
                    series.DrawLinesToLabels = True
                    series.SyncLinesToLabelsColor = True
                    series.DisplayMember = strColumn
                    RadChartView2.ShowLegend = chkShowLables.Checked
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.Series.Add(series)
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                    'For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                    '    item.Title = clsCommon.myCstr(row(strColumn))
                    'Next
                End If
                smartLabelsController.Strategy = strategy
            End If
            'RadChartView2.Refresh()
            'RadChartView2.Refresh()
            'RadChartView2.Refresh()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub radChartView_LabelFormatting(ByVal sender As Object, ByVal e As ChartViewLabelFormattingEventArgs)
        e.LabelElement.BorderColor = (CType(e.LabelElement.Parent, DataPointElement)).BackColor
    End Sub

    Sub setOrientation()
        Dim grid As CartesianGrid = RadChartView2.GetArea(Of CartesianArea)().GetGrid(Of CartesianGrid)()
        If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
            Me.RadChartView2.GetArea(Of CartesianArea)().Orientation = Orientation.Horizontal
            grid.DrawVerticalStripes = True
            grid.DrawHorizontalStripes = False
        Else
            Me.RadChartView2.GetArea(Of CartesianArea)().Orientation = Orientation.Vertical
            grid.DrawVerticalStripes = False
            grid.DrawHorizontalStripes = True
        End If


        Dim categoricalAxis As CategoricalAxis = TryCast(RadChartView2.Axes(0), CategoricalAxis)
        categoricalAxis.PlotMode = AxisPlotMode.OnTicksPadded
        categoricalAxis.LabelFitMode = AxisLabelFitMode.Rotate
        categoricalAxis.LabelRotationAngle = txtLableRotation.Value
    End Sub


    'Sub ShoChartsDataOLD()
    '    Try
    '        RadChartView2.Series.Clear()
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Dim arrValueCol As New List(Of String)
    '            If clsCommon.myLen(cboSeries.SelectedValue) > 0 Then
    '                For Each dr As DataRow In dt.Rows
    '                    If Not arrValueCol.Contains(clsCommon.myCstr(dr(clsCommon.myCstr(cboSeries.SelectedValue)))) Then
    '                        arrValueCol.Add(clsCommon.myCstr(dr(clsCommon.myCstr(cboSeries.SelectedValue))))
    '                    End If
    '                Next
    '            End If

    '            If clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Bar") = CompairStringResult.Equal Then
    '                RadChartView2.AreaType = ChartAreaType.Cartesian
    '                If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
    '                    For Each strVal As String In arrValueCol
    '                        Dim dv As DataView = dt.DefaultView
    '                        dv.RowFilter = "" + clsCommon.myCstr(cboSeries.SelectedValue) + "=" + strVal + ""
    '                        Dim barSeries As New BarSeries()
    '                        barSeries.Name = txtDesc.Text
    '                        barSeries.LegendTitle = strVal
    '                        barSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                        barSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                        barSeries.DataSource = dv.ToTable()
    '                        barSeries.ShowLabels = True
    '                        RadChartView2.Series.Add(barSeries)
    '                        RadChartView2.ShowLegend = True
    '                    Next
    '                Else
    '                    Dim barSeries As New BarSeries()
    '                    barSeries.Name = txtDesc.Text
    '                    barSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                    barSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                    barSeries.DataSource = dt
    '                    barSeries.ShowLabels = True
    '                    RadChartView2.Series.Add(barSeries)
    '                    RadChartView2.ShowLegend = False
    '                End If
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Pie") = CompairStringResult.Equal Then
    '                Dim series As New PieSeries()
    '                series.Range = New AngleRange(270, 360)
    '                series.LabelFormat = "{0:P2}"
    '                series.RadiusFactor = 0.9F
    '                series.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                series.DataSource = dt
    '                series.ShowLabels = True
    '                series.DrawLinesToLabels = True
    '                series.SyncLinesToLabelsColor = True
    '                RadChartView2.ShowLegend = True
    '                RadChartView2.AreaType = ChartAreaType.Pie
    '                RadChartView2.Series.Add(series)
    '                RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
    '                For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
    '                    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
    '                    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
    '                    item.Title = clsCommon.myCstr(row(clsCommon.myCstr(cboCategory.SelectedValue)))
    '                Next
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Line") = CompairStringResult.Equal Then
    '                RadChartView2.AreaType = ChartAreaType.Cartesian
    '                If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
    '                    For Each strVal As String In arrValueCol
    '                        Dim dv As DataView = dt.DefaultView
    '                        dv.RowFilter = "" + clsCommon.myCstr(cboSeries.SelectedValue) + "=" + strVal + ""
    '                        Dim lineSeries As New LineSeries()
    '                        lineSeries.Name = txtDesc.Text
    '                        lineSeries.LegendTitle = strVal
    '                        lineSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                        lineSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                        lineSeries.DataSource = dv.ToTable()
    '                        lineSeries.BorderWidth = 2
    '                        lineSeries.ShowLabels = True
    '                        RadChartView2.Series.Add(lineSeries)
    '                        RadChartView2.ShowLegend = True
    '                    Next
    '                Else
    '                    Dim lineSeries As New LineSeries()
    '                    lineSeries.Name = txtDesc.Text
    '                    lineSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                    lineSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                    lineSeries.DataSource = dt
    '                    lineSeries.BorderWidth = 2
    '                    lineSeries.ShowLabels = True
    '                    RadChartView2.Series.Add(lineSeries)
    '                    RadChartView2.ShowLegend = False
    '                End If

    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Area") = CompairStringResult.Equal Then
    '                RadChartView2.AreaType = ChartAreaType.Cartesian
    '                If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
    '                    For Each strVal As String In arrValueCol
    '                        Dim dv As DataView = dt.DefaultView
    '                        dv.RowFilter = "" + clsCommon.myCstr(cboSeries.SelectedValue) + "=" + strVal + ""
    '                        Dim AreaSeries As New AreaSeries()
    '                        AreaSeries.Name = txtDesc.Text
    '                        AreaSeries.LegendTitle = strVal
    '                        AreaSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                        AreaSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                        AreaSeries.DataSource = dv.ToTable()
    '                        AreaSeries.BorderWidth = 2
    '                        AreaSeries.ShowLabels = True
    '                        RadChartView2.Series.Add(AreaSeries)
    '                        RadChartView2.ShowLegend = True
    '                    Next
    '                Else
    '                    Dim AreaSeries As New AreaSeries()
    '                    AreaSeries.Name = txtDesc.Text
    '                    AreaSeries.LegendTitle = ""
    '                    AreaSeries.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                    AreaSeries.CategoryMember = clsCommon.myCstr(cboCategory.SelectedValue)
    '                    AreaSeries.DataSource = dt
    '                    AreaSeries.BorderWidth = 2
    '                    AreaSeries.ShowLabels = True
    '                    RadChartView2.Series.Add(AreaSeries)
    '                    RadChartView2.ShowLegend = False
    '                End If

    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboChartType.SelectedValue), "Donut") = CompairStringResult.Equal Then
    '                RadChartView2.AreaType = ChartAreaType.Pie
    '                Dim series As New DonutSeries()
    '                series.Range = New AngleRange(270, 360)
    '                series.LabelFormat = "{0:P2}"
    '                series.RadiusFactor = 0.9F
    '                series.InnerRadiusFactor = 50 / 100
    '                series.ValueMember = clsCommon.myCstr(cboValueMember.SelectedValue)
    '                series.DataSource = dt
    '                series.ShowLabels = True
    '                RadChartView2.ShowLegend = True
    '                RadChartView2.AreaType = ChartAreaType.Pie
    '                RadChartView2.Series.Add(series)
    '                RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
    '                For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
    '                    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
    '                    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
    '                    item.Title = clsCommon.myCstr(row(clsCommon.myCstr(cboCategory.SelectedValue)))
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        ShoChartsData()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_CREATE_BI_REPORT where code='" + txtCode.Value + "'")
            Dim builder As New StringBuilder
            builder.AppendLine("Private Shared Function FunctionName( )" + Environment.NewLine + "Dim obj As New clsCreateBIReport()")

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Columns.Count - 1
                    If clsCommon.CompairString(dt.Columns(ii).ColumnName, "Layout") = CompairStringResult.Equal Then
                        builder.AppendLine("Dim strGridLayout As String =""" + clsCommon.myCstr(dt.Rows(0)(ii)).Replace("""", """""") + """")
                        builder.AppendLine("Dim byteArray As Byte() = Encoding.ASCII.GetBytes(strGridLayout)")
                        builder.AppendLine("obj.Layout = New MemoryStream(byteArray)")
                    ElseIf clsCommon.CompairString(dt.Columns(ii).ColumnName, "Is_Create_By_Developer") = CompairStringResult.Equal Then
                        builder.AppendLine("obj." + clsCommon.myCstr(dt.Columns(ii).ColumnName) + "=1")
                    ElseIf clsCommon.CompairString(dt.Columns(ii).ColumnName, "Created_By") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Created_Date") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Modify_By") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Modify_Date") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Comp_Code") = CompairStringResult.Equal Then

                    Else
                        builder.AppendLine("obj." + clsCommon.myCstr(dt.Columns(ii).ColumnName) + "=""" + clsCommon.myCstr(dt.Rows(0)(ii)).Replace(vbCr, " ").Replace(vbLf, " ") + """")
                    End If
                Next

                dt = clsDBFuncationality.GetDataTable("select * from TSPL_CREATE_BI_REPORT_FILTERS where code='" + txtCode.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    builder.AppendLine("obj.arr = New List(Of clsCreateBIReportFilterDetails)")
                    builder.AppendLine(" Dim objtr As clsCreateBIReportFilterDetails")
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        builder.AppendLine("objtr = New clsCreateBIReportFilterDetails")
                        For jj As Integer = 0 To dt.Columns.Count - 1
                            builder.Append("objtr." + clsCommon.myCstr(dt.Columns(jj).ColumnName) + "=""" + clsCommon.myCstr(dt.Rows(ii)(jj)) + """" + Environment.NewLine)
                        Next
                        builder.AppendLine("obj.arr.Add(objtr)")
                    Next
                End If

                dt = clsDBFuncationality.GetDataTable("select * from TSPL_CREATE_BI_REPORT_FILTERS_INNER where code='" + txtCode.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    builder.AppendLine("obj.arrInner = New List(Of clsCreateBIReportInnerFilterDetails)")
                    builder.AppendLine(" Dim objInntr As clsCreateBIReportInnerFilterDetails")
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        builder.AppendLine("objInntr = New clsCreateBIReportInnerFilterDetails")
                        For jj As Integer = 0 To dt.Columns.Count - 1
                            builder.Append("objInntr." + clsCommon.myCstr(dt.Columns(jj).ColumnName) + "=""" + clsCommon.myCstr(dt.Rows(ii)(jj)) + """" + Environment.NewLine)
                        Next
                        builder.AppendLine("obj.arrInner.Add(objInntr)")
                    Next
                End If
                builder.AppendLine("obj.SaveData(obj, False, True)")
                builder.AppendLine("End Function")
            End If
            Dim frm As New FrmFreeTxtBox1()
            frm.WindowState = FormWindowState.Maximized
            frm.strRmks = builder.ToString()

            frm.ShowDialog()
            frm.Dispose()
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub cboSetting_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboSetting.SelectedIndexChanged
        SplitPanel3.Collapsed = True
        SplitPanel2.Collapsed = True
        If clsCommon.CompairString(clsCommon.myCstr(cboSetting.SelectedValue), "Formula") = CompairStringResult.Equal Then
            SplitPanel2.Collapsed = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboSetting.SelectedValue), "Options") = CompairStringResult.Equal Then
            SplitPanel3.Collapsed = False
        End If
    End Sub

    Public Sub WireEvents()
        Me.provider = New LocalDataSourceProvider()
        AddHandler rowGrandTotalNone.ToggleStateChanged, AddressOf RowGrandTotalChecked
        AddHandler rowGrandTotalLast.ToggleStateChanged, AddressOf RowGrandTotalChecked
        AddHandler rowGrandTotalFirst.ToggleStateChanged, AddressOf RowGrandTotalChecked
        AddHandler rowSubTotalNone.ToggleStateChanged, AddressOf RowSubTotalChecked
        AddHandler rowSubTotalLast.ToggleStateChanged, AddressOf RowSubTotalChecked
        AddHandler rowSubTotalFirst.ToggleStateChanged, AddressOf RowSubTotalChecked
        AddHandler columnGrandTotalNone.ToggleStateChanged, AddressOf ColumnGrandTotalChecked
        AddHandler columnGrandTotalLast.ToggleStateChanged, AddressOf ColumnGrandTotalChecked
        AddHandler columnGrandTotalFirst.ToggleStateChanged, AddressOf ColumnGrandTotalChecked
        AddHandler columnSubTotalNone.ToggleStateChanged, AddressOf ColumnSubTotalChecked
        AddHandler columnSubTotalLast.ToggleStateChanged, AddressOf ColumnSubTotalChecked
        AddHandler columnSubTotalFirst.ToggleStateChanged, AddressOf ColumnSubTotalChecked
    End Sub

    Private Sub RowGrandTotalChecked(ByVal sender As Object, ByVal e As StateChangedEventArgs)
        If Me.suspendEvents Then
            Return
        End If

        If pg1 IsNot Nothing AndAlso sender IsNot Nothing Then
            Me.pg1.RowGrandTotalsPosition = GetPosition(sender)
        End If
    End Sub

    Private Function GetPosition(ByVal sender As Object) As TotalsPos
        If sender Is Me.rowGrandTotalFirst OrElse sender Is Me.columnGrandTotalFirst OrElse sender Is Me.rowSubTotalFirst OrElse sender Is Me.columnSubTotalFirst Then
            Return TotalsPos.First
        End If

        If sender Is Me.rowGrandTotalLast OrElse sender Is Me.columnGrandTotalLast OrElse sender Is Me.rowSubTotalLast OrElse sender Is Me.columnSubTotalLast Then
            Return TotalsPos.Last
        End If

        Return TotalsPos.None
    End Function

    Private Sub RowSubTotalChecked(ByVal sender As Object, ByVal e As StateChangedEventArgs)
        If Me.suspendEvents Then
            Return
        End If

        If pg1 IsNot Nothing AndAlso sender IsNot Nothing Then
            Me.pg1.RowsSubTotalsPosition = GetPosition(sender)
        End If
    End Sub

    Private Sub ColumnGrandTotalChecked(ByVal sender As Object, ByVal e As StateChangedEventArgs)
        If Me.suspendEvents Then
            Return
        End If

        If pg1 IsNot Nothing AndAlso sender IsNot Nothing Then
            Me.pg1.ColumnGrandTotalsPosition = GetPosition(sender)
        End If
    End Sub

    Private Sub ColumnSubTotalChecked(ByVal sender As Object, ByVal e As StateChangedEventArgs)
        If Me.suspendEvents Then
            Return
        End If

        If pg1 IsNot Nothing AndAlso sender IsNot Nothing Then
            Me.pg1.ColumnsSubTotalsPosition = GetPosition(sender)
        End If
    End Sub


    Private Sub gvFilter_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvFilter.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvFilter.Columns(colFilterCode) Then
                        OpenFilterMAster(gvFilter, False)
                    ElseIf e.Column Is gvFilter.Columns(colLevel) Then
                        If clsCommon.myCdbl(gvFilter.CurrentRow.Cells(colLevel).Value) > clsCommon.myCdbl(gvFilter.CurrentRow.Cells(colMaxLevel).Value) Then
                            clsCommon.MyMessageBoxShow("Max Level is " + clsCommon.myCstr(gvFilter.CurrentRow.Cells(colMaxLevel).Value))
                            gvFilter.CurrentRow.Cells(colLevel).Value = 0
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenFilterMAster(ByVal GV As RadGridView, ByVal isButtonClick As Boolean)
        Dim qry As String = "select Code,Description as Name,case when Filter_type='T' then 'Tree' else 'Grid' end as Type  from TSPL_CREATE_BI_FILTER"
        Dim whrCls As String = ""
        GV.CurrentRow.Cells(colFilterCode).Value = clsCommon.ShowSelectForm("BIFLTXYZ", qry, "Code", whrCls, clsCommon.myCstr(GV.CurrentRow.Cells(colFilterCode).Value), "Code", isButtonClick)
        qry = "select Description,Filter_type,Tree_Level from TSPL_CREATE_BI_FILTER where Code='" + clsCommon.myCstr(GV.CurrentRow.Cells(colFilterCode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            GV.CurrentRow.Cells(colFilterName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            GV.CurrentRow.Cells(colFilterType).Value = clsCommon.myCstr(dt.Rows(0)("Filter_type"))
            GV.CurrentRow.Cells(colMaxLevel).Value = clsCommon.myCdbl(dt.Rows(0)("Tree_Level"))
        Else
            GV.CurrentRow.Cells(colFilterName).Value = ""
            GV.CurrentRow.Cells(colFilterType).Value = ""
            GV.CurrentRow.Cells(colMaxLevel).Value = 0
        End If
    End Sub

    Private Sub chkDashboard_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDashboard.ToggleStateChanged
        txtReportModule.Enabled = Not chkDashboard.Checked
    End Sub


    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            cboChartType.SelectedValue = "Bar"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Vertical"
            txtLableRotation.Value = 330
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        Try
            cboChartType.SelectedValue = "Bar"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Horizontal"
            txtLableRotation.Value = 0
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            cboChartType.SelectedValue = "Line"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Vertical"
            txtLableRotation.Value = 0
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        Try
            cboChartType.SelectedValue = "Area"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Vertical"
            txtLableRotation.Value = 0
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            cboChartType.SelectedValue = "Pie"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Vertical"
            txtLableRotation.Value = 0
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        Try
            cboChartType.SelectedValue = "Donut"
            cboCombineMode.SelectedValue = "Cluster"
            cboOrientation.SelectedValue = "Vertical"
            txtLableRotation.Value = 0
            'chkShowLables.Checked = False
            ShoChartsData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        gvFilterInner.Rows.AddNew()
        gvFilterInner.Rows(gvFilterInner.Rows.Count - 1).Cells(colCode).Value = "#$FILTER" + clsCommon.myCstr(gvFilterInner.Rows.Count) + "$#"
    End Sub

    Private Sub gvFilterInner_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvFilterInner.CellFormatting, gvFilter.CellFormatting
        Try
            If e.Column Is gvFilterInner.Columns(colLevel) Then
                If clsCommon.CompairString(clsCommon.myCstr(gvFilterInner.CurrentRow.Cells(colFilterType).Value), "T") = CompairStringResult.Equal Then
                    gvFilterInner.CurrentRow.Cells(colLevel).ReadOnly = False
                Else
                    gvFilterInner.CurrentRow.Cells(colLevel).ReadOnly = True
                    gvFilterInner.CurrentRow.Cells(colLevel).Value = Nothing
                End If

            ElseIf e.Column Is gvFilterInner.Columns(colFilterCode) Then
                If clsCommon.myCBool(gvFilterInner.CurrentRow.Cells(colDateRange).Value) Then
                    gvFilterInner.CurrentRow.Cells(colFilterCode).ReadOnly = True
                    gvFilterInner.CurrentRow.Cells(colFilterCode).Value = ""
                    gvFilterInner.CurrentRow.Cells(colFilterName).Value = ""
                Else
                    gvFilterInner.CurrentRow.Cells(colFilterCode).ReadOnly = False
                End If
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvFilterInner_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvFilterInner.CellValueChanged, gvFilter.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvFilterInner.Columns(colFilterCode) Then
                        OpenFilterMAster(gvFilterInner, False)
                    ElseIf e.Column Is gvFilterInner.Columns(colLevel) Then
                        If clsCommon.myCdbl(gvFilterInner.CurrentRow.Cells(colLevel).Value) > clsCommon.myCdbl(gvFilterInner.CurrentRow.Cells(colMaxLevel).Value) Then
                            clsCommon.MyMessageBoxShow("Max Level is " + clsCommon.myCstr(gvFilterInner.CurrentRow.Cells(colMaxLevel).Value))
                            gvFilterInner.CurrentRow.Cells(colLevel).Value = 0
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ContextMenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked
        If RadPageView1.SelectedPage Is RadPageViewPage1 Then
            Dim strText As String = " '" + e.ClickedItem.Text + "' = '" + e.ClickedItem.Text + "'"
            sqlQueryText.Text = sqlQueryText.Text.Insert(sqlQueryText.SelectionStart, strText)
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        ContextMenuStrip1.Items.Clear()
        For Index As Integer = 0 To gvFilterInner.Rows.Count - 1
            ContextMenuStrip1.Items.Add(clsCommon.myCstr(gvFilterInner.Rows(Index).Cells(colCode).Value))
        Next
    End Sub

    Private Sub sqlQueryText_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles sqlQueryText.Validating
        SetControlByQuery()
    End Sub

    Private Sub txtDrilldownReport__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDrilldownReport._MYValidating
        Try
            Dim qry As String = "select Code,Description,Type from TSPL_CREATE_BI_REPORT "
            Dim WhrCls As String = " TSPL_CREATE_BI_REPORT.code not in ('" + txtCode.Value + "') "
            txtDrilldownReport.Value = clsCommon.ShowSelectForm("DrillReport", qry, "Code", WhrCls, txtDrilldownReport.Value, "", isButtonClicked)
            txtDrilldownFilter.Value = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDrilldownFilter__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDrilldownFilter._MYValidating
        Try
            If clsCommon.myLen(txtDrilldownReport.Value) <= 0 Then
                txtDrilldownReport.Focus()
                Throw New Exception("Please first select Drilldown report")
            End If
            Dim qry As String = "select  Against_Filter as Code ,TSPL_CREATE_BI_FILTER.Description from TSPL_CREATE_BI_REPORT_FILTERS  left outer join TSPL_CREATE_BI_FILTER on TSPL_CREATE_BI_FILTER.Code=TSPL_CREATE_BI_REPORT_FILTERS.Against_Filter"
            Dim WhrCls As String = "Against_Filter is not null and len(Against_Filter)>0 and TSPL_CREATE_BI_REPORT_FILTERS.Code='" + txtDrilldownReport.Value + "'"
            txtDrilldownFilter.Value = clsCommon.ShowSelectForm("Drillfilter", qry, "Code", WhrCls, txtDrilldownFilter.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub MyRadioButton1_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnDrillDownNA.ToggleStateChanged, rbtnDrillDownTransaction.ToggleStateChanged, rbtnDrillDownReport.ToggleStateChanged
        txtDrilldownReport.Visible = False
        txtDrilldownFilter.Visible = False
        lblDrillDownColumn.Visible = False
        lblTransactionTypeColumn.Visible = False
        MyLabel7.Visible = False
        MyLabel8.Visible = False
        If rbtnDrillDownNA.IsChecked Then
        ElseIf rbtnDrillDownReport.IsChecked Then
            txtDrilldownReport.Visible = True
            txtDrilldownFilter.Visible = True
            lblDrillDownColumn.Visible = True
            MyLabel7.Visible = True
            MyLabel8.Visible = True
        ElseIf rbtnDrillDownTransaction.IsChecked Then
            lblDrillDownColumn.Visible = True
            lblTransactionTypeColumn.Visible = True
        End If
    End Sub

    Dim isValueChangeEvenRun As Boolean = False
    Private Sub gvFilter_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gvFilter.ValueChanging
        Try
            If Not isValueChangeEvenrun Then
                isValueChangeEvenrun = True
                If Not isInsideLoadData Then
                    If gvFilter.CurrentColumn Is gvFilter.Columns(colDrillDownColumn) Then
                        If e.NewValue Then
                            For ii As Integer = 0 To gvFilter.RowCount - 1
                                If ii <> gvFilter.CurrentRow.Index Then
                                    gvFilter.Rows(ii).Cells(colDrillDownColumn).Value = False
                                End If
                            Next
                            lblDrillDownColumn.Text = clsCommon.myCstr(gvFilter.CurrentRow.Cells(colCode).Value)
                        End If
                    ElseIf gvFilter.CurrentColumn Is gvFilter.Columns(colDrillDownTransactionTypeColumn) Then
                        If e.NewValue Then
                            For ii As Integer = 0 To gvFilter.RowCount - 1
                                If ii <> gvFilter.CurrentRow.Index Then
                                    gvFilter.Rows(ii).Cells(colDrillDownTransactionTypeColumn).Value = False
                                End If
                            Next
                            lblTransactionTypeColumn.Text = clsCommon.myCstr(gvFilter.CurrentRow.Cells(colCode).Value)
                        End If
                    End If
                End If
                isValueChangeEvenrun = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    
End Class
