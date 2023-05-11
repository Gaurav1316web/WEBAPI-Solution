Imports common
Imports System.IO
Imports Telerik.Charting
 

Public Class frmDashboard
    Inherits FrmMainTranScreen
#Region "Variables"
    Public objDB As clsCreateDashboard
#End Region

    Private Sub FrmBIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    Sub LoadData()
        Try
            clsCommon.ProgressBarPercentShow()
            Dim TotalControl As Integer = objDB.arr.Count
            TableLayoutPanel1.Controls.Clear()
            TableLayoutPanel1.ColumnStyles.Clear()
            TableLayoutPanel1.RowStyles.Clear()


            TableLayoutPanel1.ColumnCount = 2
            TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))

            TableLayoutPanel1.RowCount = Math.Ceiling(TotalControl / 2)
            For iiRows As Integer = 1 To (TotalControl / 2) + 1
                TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (100 / TableLayoutPanel1.RowCount)))
            Next
            TableLayoutPanel1.Size = New System.Drawing.Size(TableLayoutPanel1.Width, (TableLayoutPanel1.RowCount * 300))

            Dim Couner As Integer = 0
            For Each objDBDetail As clsCreateDashboardDetails In objDB.arr
                clsCommon.ProgressBarPercentUpdate(Couner * 100 / objDB.arr.Count, "")
                Dim obj As clsCreateBIReport = clsCreateBIReport.GetData(objDBDetail.Report_Code, True, NavigatorType.Current)
                Dim strQry As String = obj.Qry
                If obj.arrInner IsNot Nothing AndAlso obj.arrInner.Count > 0 Then
                    For Each objtr As clsCreateBIReportInnerFilterDetails In obj.arrInner
                        Dim strReplaceFromText As String = " '" + objtr.Filter_SNo + "' = '" + objtr.Filter_SNo + "'"
                        Dim strReplaceToText As String = " 2=2 "
                        If objtr.Is_Date_Range Then
                            strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") & _
                           "' and " + objtr.Table_Name + "." + objtr.Column_Name + " <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   "
                        ElseIf objtr.Is_From_Date Then
                            strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " " + objtr.Operator_Name + " '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "   "
                        ElseIf objtr.Is_To_Date Then
                            strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " " + objtr.Operator_Name + " '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "   "
                        End If
                        strQry = strQry.Replace(strReplaceFromText, strReplaceToText)
                    Next
                End If
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    Dim strNewQry As String = " select "

                    For ii As Integer = 0 To obj.arr.Count - 1
                        Dim objtr As clsCreateBIReportFilterDetails = obj.arr(ii)
                        If objtr.Figure_In > 0 Then
                            strNewQry += " cast(([" + objtr.Filter_Column + "] " + "/" + getFigure(objtr.Figure_In) + ") as decimal(18,2))  as [" + objtr.Filter_Column + "] "
                        Else
                            strNewQry += " [" + objtr.Filter_Column + "] "
                        End If
                        If Not ii = obj.arr.Count - 1 Then
                            strNewQry += ","
                        End If
                    Next
                    strNewQry += " from (" + strQry + ")xxx where 2=2 "
                    For Each objtr As clsCreateBIReportFilterDetails In obj.arr
                        If objtr.Is_Date_Range Then
                            strNewQry += " and [" + clsCommon.myCstr(objtr.Filter_Column) + "] >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                            strNewQry += " and [" + clsCommon.myCstr(objtr.Filter_Column) + "] <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                        End If
                    Next

                    Dim strOuterOrderBy As String = ""
                    Dim tqry As String = "select Filter_Column,Is_Order_Desc from TSPL_CREATE_BI_REPORT_FILTERS  where code ='" + objDBDetail.Report_Code + "' and Order_By>0 order by Order_By"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(tqry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If clsCommon.myLen(strOuterOrderBy) > 0 Then
                                strOuterOrderBy += ","
                            End If
                            strOuterOrderBy += "[" + clsCommon.myCstr(dr("Filter_Column")) + "]"
                            If clsCommon.myCdbl(dr("Is_Order_Desc")) > 0 Then
                                strOuterOrderBy += " Desc "
                            End If
                        Next
                    End If

                    strQry = strNewQry
                    If clsCommon.myLen(strOuterOrderBy) > 0 Then
                        strQry += " order by  " + strOuterOrderBy
                    End If

                End If

                Dim objLayout As clsGridLayout = New clsGridLayout()
                objLayout = CType(objLayout.GetData(obj.Code, "", objCommonVar.CurrentUserCode), clsGridLayout)

                If clsCommon.CompairString(obj.Type, "Grid") = CompairStringResult.Equal Then
                    ''For Grid
                    Dim gv1 As New RadGridView()
                    gv1.DataSource = Nothing
                    gv1.Columns.Clear()
                    gv1.Rows.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = clsDBFuncationality.GetDataTable(strQry)
                    gv1.TableElement.TableHeaderHeight = 30
                    gv1.MasterTemplate.ShowRowHeaderColumn = False
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    gv1.AllowAddNewRow = False
                    gv1.ShowGroupPanel = True
                    gv1.BestFitColumns()

                    If objLayout IsNot Nothing Then
                        gv1.LoadLayout(objLayout.GridLayout)
                    Else
                        gv1.LoadLayout(obj.Layout)
                    End If

                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For Each objDetail As clsCreateBIReportFilterDetails In obj.arr
                        If objDetail.Is_Show_Total Then
                            If clsCommon.myLen(objDetail.Total_Formula) > 0 Then
                                Dim summaryItem As New GridViewSummaryItem()
                                summaryItem.FormatString = "{0:F2}"
                                summaryItem.Name = objDetail.Filter_Column
                                summaryItem.AggregateExpression = objDetail.Total_Formula
                                summaryRowItem.Add(summaryItem)
                            Else
                                Dim item1 As New GridViewSummaryItem(objDetail.Filter_Column, "{0:F2}", GridAggregateFunction.Sum)
                                summaryRowItem.Add(item1)
                            End If
                        End If
                        'End If
                    Next
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                    Dim rgpBox As New RadGroupBox
                    rgpBox.Text = objDBDetail.Report_Description
                    rgpBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
                    rgpBox.Dock = System.Windows.Forms.DockStyle.Fill
                    rgpBox.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
                    rgpBox.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
                    rgpBox.HeaderText = objDBDetail.Report_Description
                    rgpBox.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                    TableLayoutPanel1.Controls.Add(rgpBox, Couner Mod 2, Math.Floor(Couner / 2))

                    rgpBox.Controls.Add(gv1)
                    gv1.Dock = System.Windows.Forms.DockStyle.Fill
                ElseIf clsCommon.CompairString(obj.Type, "Pivot Grid") = CompairStringResult.Equal Then
                    ''For Pivot Grid
                    Dim pg1 As New RadPivotGrid
                    pg1.DataSource = clsDBFuncationality.GetDataTable(strQry)

                    If objLayout IsNot Nothing Then
                        pg1.LoadLayout(objLayout.GridLayout)
                    Else
                        pg1.LoadLayout(obj.Layout)
                    End If


                    Dim rgpBox As New RadGroupBox
                    rgpBox.Text = objDBDetail.Report_Description
                    rgpBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
                    rgpBox.Dock = System.Windows.Forms.DockStyle.Fill
                    rgpBox.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office
                    rgpBox.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center
                    rgpBox.HeaderText = objDBDetail.Report_Description
                    rgpBox.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                    TableLayoutPanel1.Controls.Add(rgpBox, Couner Mod 2, Math.Floor(Couner / 2))
                    rgpBox.Controls.Add(pg1)
                    pg1.Dock = System.Windows.Forms.DockStyle.Fill
                ElseIf clsCommon.CompairString(obj.Type, "Chart") = CompairStringResult.Equal Then
                    Dim RadChartView2 As New RadChartView()
                    RadChartView2.Area.View.Palette = New CustomPalette()
                    Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                    RadChartView2.AreaDesign = CartesianArea1
                    RadChartView2.Series.Clear()
                    AddHandler RadChartView2.LabelFormatting, AddressOf radChartView_LabelFormatting
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim arrRow As New List(Of String)
                        Dim strColumn As String = ""
                        For Each objtr As clsCreateBIReportFilterDetails In obj.arr
                            If objtr.Chart_Column Then
                                strColumn = objtr.Filter_Column
                            ElseIf objtr.Chart_Row Then
                                arrRow.Add(objtr.Filter_Column)
                            End If
                        Next

                        RadChartView2.ShowTitle = True
                        RadChartView2.ChartElement.TitlePosition = TitlePosition.Top
                        RadChartView2.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                        RadChartView2.Title = obj.Description
                        Dim smartLabelsController As New SmartLabelsController()
                        RadChartView2.Controllers.Add(smartLabelsController)
                        RadChartView2.ShowSmartLabels = True
                        Dim strategy As SmartLabelsStrategyBase = Nothing

                        Dim combineMode As ChartSeriesCombineMode = ChartSeriesCombineMode.None
                        If clsCommon.CompairString(obj.Chart_Combine_Mode, "Cluster") = CompairStringResult.Equal Then
                            combineMode = ChartSeriesCombineMode.Cluster
                        ElseIf clsCommon.CompairString(obj.Chart_Combine_Mode, "Stack") = CompairStringResult.Equal Then
                            combineMode = ChartSeriesCombineMode.Stack
                        ElseIf clsCommon.CompairString(obj.Chart_Combine_Mode, "Stack100") = CompairStringResult.Equal Then
                            combineMode = ChartSeriesCombineMode.Stack100
                        End If

                        If clsCommon.CompairString(obj.Chart_Type, "Bar") = CompairStringResult.Equal Then
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
                                    barSeries.ShowLabels = obj.Chart_Show_Label

                                    barSeries.DrawLinesToLabels = True
                                    barSeries.SyncLinesToLabelsColor = True
                                    RadChartView2.Series.Add(barSeries)
                                Next
                                RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                                setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                            End If
                        ElseIf clsCommon.CompairString(obj.Chart_Type, "Line") = CompairStringResult.Equal Then
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
                                    lineSeries.ShowLabels = obj.Chart_Show_Label
                                    lineSeries.DrawLinesToLabels = True
                                    lineSeries.SyncLinesToLabelsColor = True
                                    RadChartView2.Series.Add(lineSeries)
                                Next
                                RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                                setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                            End If
                        ElseIf clsCommon.CompairString(obj.Chart_Type, "Area") = CompairStringResult.Equal Then
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
                                    AreaSeries.ShowLabels = obj.Chart_Show_Label
                                    AreaSeries.DrawLinesToLabels = True
                                    AreaSeries.SyncLinesToLabelsColor = True
                                    RadChartView2.Series.Add(AreaSeries)
                                Next
                                RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                                setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                            End If
                        ElseIf clsCommon.CompairString(obj.Chart_Type, "Pie") = CompairStringResult.Equal Then
                            strategy = New PieTwoLabelColumnsStrategy()
                            RadChartView2.AreaType = ChartAreaType.Pie
                            RadChartView2.ShowLegend = obj.Chart_Show_Label
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
                            'For Each item As LegendItem In RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                            '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                            '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                            '    item.Title = clsCommon.myCstr(row(strColumn))
                            'Next
                        ElseIf clsCommon.CompairString(obj.Chart_Type, "Donut") = CompairStringResult.Equal Then
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
                            RadChartView2.ShowLegend = obj.Chart_Show_Label
                            RadChartView2.AreaType = ChartAreaType.Pie
                            RadChartView2.Series.Add(series)
                            RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                            'For Each item As LegendItem In RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                            '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                            '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                            '    item.Title = clsCommon.myCstr(row(strColumn))
                            'Next
                        End If
                        'TableLayoutPanel1.Controls.Add(RadChartView2, Couner Mod 2, Math.Floor(Couner / 2))
                        'RadChartView2.Dock = System.Windows.Forms.DockStyle.Fill

                        Dim PanelContainer As Telerik.WinControls.UI.RadScrollablePanelContainer = New Telerik.WinControls.UI.RadScrollablePanelContainer()
                        Dim RadScrollablePanel2 As RadScrollablePanel = New RadScrollablePanel()

                        TableLayoutPanel1.Controls.Add(RadScrollablePanel2, Couner Mod 2, Math.Floor(Couner / 2))

                        RadScrollablePanel2.Dock = System.Windows.Forms.DockStyle.Fill
                        RadScrollablePanel2.Location = New System.Drawing.Point(0, 0)
                        RadScrollablePanel2.Name = "RSP" + clsCommon.myCstr(Couner)

                        RadScrollablePanel2.PanelContainer.Controls.Add(RadChartView2)

                        If obj.Chart_Show_Scroll Then
                            RadChartView2.Height = RadScrollablePanel2.Height - 3
                            RadChartView2.Width = RadScrollablePanel2.Width - 3
                            If clsCommon.CompairString(obj.Chart_Orientation, "Vertical") = CompairStringResult.Equal Then
                                Dim nWidth As Integer = dt.Rows.Count * arrRow.Count * 30
                                If nWidth > RadChartView2.Width Then
                                    RadChartView2.Width = nWidth
                                End If
                            Else
                                Dim nHeight As Integer = dt.Rows.Count * arrRow.Count * 30
                                If nHeight > RadChartView2.Height Then
                                    RadChartView2.Height = nHeight
                                End If
                            End If
                        Else
                            RadChartView2.Dock = System.Windows.Forms.DockStyle.Fill
                        End If
                    End If
                End If
                EnableDisableConrols(False)
                Couner += 1
                'ReStoreGridLayout()
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Private Function getFigure(ByVal num As Integer) As String
        Dim strRet As String = ""
        If num = 1 Then
            strRet = "100"
        ElseIf num = 2 Then
            strRet = "1000"
        ElseIf num = 3 Then
            strRet = "10000"
        ElseIf num = 4 Then
            strRet = "100000"
        ElseIf num = 5 Then
            strRet = "1000000"
        ElseIf num = 6 Then
            strRet = "10000000"
        ElseIf num = 7 Then
            strRet = "100000000"
        End If
        Return strRet
    End Function

    Private Sub radChartView_LabelFormatting(ByVal sender As Object, ByVal e As ChartViewLabelFormattingEventArgs)
        e.LabelElement.BorderColor = (CType(e.LabelElement.Parent, DataPointElement)).BackColor
    End Sub

    Sub setOrientation(ByVal chv As RadChartView, ByVal strOrient As String, ByVal LableRotationAngel As Integer)
        Dim grid As CartesianGrid = chv.GetArea(Of CartesianArea)().GetGrid(Of CartesianGrid)()
        If clsCommon.CompairString(strOrient, "Horizontal") = CompairStringResult.Equal Then
            chv.GetArea(Of CartesianArea)().Orientation = Orientation.Horizontal
            grid.DrawVerticalStripes = True
            grid.DrawHorizontalStripes = False
        Else
            chv.GetArea(Of CartesianArea)().Orientation = Orientation.Vertical
            grid.DrawVerticalStripes = False
            grid.DrawHorizontalStripes = True
        End If


        Dim categoricalAxis As CategoricalAxis = TryCast(chv.Axes(0), CategoricalAxis)
        categoricalAxis.PlotMode = AxisPlotMode.OnTicksPadded
        categoricalAxis.LabelFitMode = AxisLabelFitMode.Rotate
        categoricalAxis.LabelRotationAngle = LableRotationAngel
    End Sub

    Sub ShoChartsData(ByVal qry As String)
        'Try
        '    RadChartView2.Series.Clear()
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim arrValueCol As New List(Of String)
        '        If clsCommon.myLen(obj.Chart_Series_Member) > 0 Then
        '            For Each dr As DataRow In dt.Rows
        '                If Not arrValueCol.Contains(clsCommon.myCstr(dr(obj.Chart_Series_Member))) Then
        '                    arrValueCol.Add(clsCommon.myCstr(dr(clsCommon.myCstr(obj.Chart_Series_Member))))
        '                End If
        '            Next
        '        End If

        '        If clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Bar") = CompairStringResult.Equal Then
        '            RadChartView2.AreaType = ChartAreaType.Cartesian
        '            If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
        '                For Each strVal As String In arrValueCol
        '                    Dim dv As DataView = dt.DefaultView
        '                    dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
        '                    Dim barSeries As New BarSeries()
        '                    barSeries.Name = obj.Description
        '                    barSeries.LegendTitle = strVal
        '                    barSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                    barSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                    barSeries.DataSource = dv.ToTable()
        '                    barSeries.ShowLabels = True
        '                    RadChartView2.Series.Add(barSeries)
        '                    RadChartView2.ShowLegend = True
        '                Next
        '            Else
        '                Dim barSeries As New BarSeries()
        '                barSeries.Name = obj.Description
        '                barSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                barSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                barSeries.DataSource = dt
        '                barSeries.ShowLabels = True
        '                RadChartView2.Series.Add(barSeries)
        '                RadChartView2.ShowLegend = False
        '            End If
        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Pie") = CompairStringResult.Equal Then
        '            Dim series As New PieSeries()
        '            series.Range = New AngleRange(270, 360)
        '            series.LabelFormat = "{0:P2}"
        '            series.RadiusFactor = 0.9F
        '            series.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '            series.DataSource = dt
        '            series.ShowLabels = True
        '            series.DrawLinesToLabels = True
        '            series.SyncLinesToLabelsColor = True
        '            RadChartView2.ShowLegend = True
        '            RadChartView2.AreaType = ChartAreaType.Pie
        '            RadChartView2.Series.Add(series)
        '            RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
        '            For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
        '                Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
        '                Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
        '                item.Title = clsCommon.myCstr(row(clsCommon.myCstr(obj.Chart_Category_Member)))
        '            Next
        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Line") = CompairStringResult.Equal Then
        '            RadChartView2.AreaType = ChartAreaType.Cartesian
        '            If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
        '                For Each strVal As String In arrValueCol
        '                    Dim dv As DataView = dt.DefaultView
        '                    dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
        '                    Dim lineSeries As New LineSeries()
        '                    lineSeries.Name = obj.Description
        '                    lineSeries.LegendTitle = strVal
        '                    lineSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                    lineSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                    lineSeries.DataSource = dv.ToTable()
        '                    lineSeries.BorderWidth = 2
        '                    lineSeries.ShowLabels = True
        '                    RadChartView2.Series.Add(lineSeries)
        '                    RadChartView2.ShowLegend = True
        '                Next
        '            Else
        '                Dim lineSeries As New LineSeries()
        '                lineSeries.Name = obj.Description
        '                lineSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                lineSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                lineSeries.DataSource = dt
        '                lineSeries.BorderWidth = 2
        '                lineSeries.ShowLabels = True
        '                RadChartView2.Series.Add(lineSeries)
        '                RadChartView2.ShowLegend = False
        '            End If

        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Area") = CompairStringResult.Equal Then
        '            RadChartView2.AreaType = ChartAreaType.Cartesian
        '            If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
        '                For Each strVal As String In arrValueCol
        '                    Dim dv As DataView = dt.DefaultView
        '                    dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
        '                    Dim AreaSeries As New AreaSeries()
        '                    AreaSeries.Name = obj.Description
        '                    AreaSeries.LegendTitle = strVal
        '                    AreaSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                    AreaSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                    AreaSeries.DataSource = dv.ToTable()
        '                    AreaSeries.BorderWidth = 2
        '                    AreaSeries.ShowLabels = True
        '                    RadChartView2.Series.Add(AreaSeries)
        '                    RadChartView2.ShowLegend = True
        '                Next
        '            Else
        '                Dim AreaSeries As New AreaSeries()
        '                AreaSeries.Name = obj.Description
        '                AreaSeries.LegendTitle = ""
        '                AreaSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '                AreaSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
        '                AreaSeries.DataSource = dt
        '                AreaSeries.BorderWidth = 2
        '                AreaSeries.ShowLabels = True
        '                RadChartView2.Series.Add(AreaSeries)
        '                RadChartView2.ShowLegend = False
        '            End If

        '        ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Donut") = CompairStringResult.Equal Then
        '            RadChartView2.AreaType = ChartAreaType.Pie
        '            Dim series As New DonutSeries()
        '            series.Range = New AngleRange(270, 360)
        '            series.LabelFormat = "{0:P2}"
        '            series.RadiusFactor = 0.9F
        '            series.InnerRadiusFactor = 50 / 100
        '            series.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
        '            series.DataSource = dt
        '            series.ShowLabels = True
        '            RadChartView2.ShowLegend = True
        '            RadChartView2.AreaType = ChartAreaType.Pie
        '            RadChartView2.Series.Add(series)
        '            RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
        '            For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
        '                Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
        '                Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
        '                item.Title = clsCommon.myCstr(row(obj.Chart_Category_Member))
        '            Next
        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub reset()
        'Try
        '    EnableDisableConrols(True)
        '    gv1.DataSource = Nothing
        '    Try
        '        pg1.DataSource = Nothing
        '    Catch ex As Exception
        '    End Try
        '    RadPageView1.SelectedPage = RadPageViewPage1
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

    Private Sub EnableDisableConrols(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
    End Sub

    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        EnableDisableConrols(True)
    End Sub
End Class

Public Class CustomPalette
    Inherits ChartPalette
    Public Sub New()
        
        Me.GlobalEntries.Add(Color.RoyalBlue)
        Me.GlobalEntries.Add(Color.MediumSeaGreen)
        Me.GlobalEntries.Add(Color.DarkOrange)
        Me.GlobalEntries.Add(Color.DeepPink)
        Me.GlobalEntries.Add(Color.BlueViolet)
        Me.GlobalEntries.Add(Color.OrangeRed)
        Me.GlobalEntries.Add(Color.DarkGreen)
        Me.GlobalEntries.Add(Color.DarkOrchid)
        Me.GlobalEntries.Add(Color.Yellow)
        Me.GlobalEntries.Add(Color.DarkTurquoise)
        Me.GlobalEntries.Add(Color.DodgerBlue)
        Me.GlobalEntries.Add(Color.Goldenrod)
        Me.GlobalEntries.Add(Color.MediumVioletRed)
        Me.GlobalEntries.Add(Color.Orange)
        Me.GlobalEntries.Add(Color.Turquoise)
        Me.GlobalEntries.Add(Color.MediumSlateBlue)
        Me.GlobalEntries.Add(Color.MediumPurple)
        Me.GlobalEntries.Add(Color.MediumBlue)
    End Sub
End Class
