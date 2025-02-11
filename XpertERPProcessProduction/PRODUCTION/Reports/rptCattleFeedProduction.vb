Imports common
Imports Telerik.Charting
Imports Telerik.QuickStart.WinControls
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class rptCattleFeedProduction
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim dtProduction As DataTable = Nothing

#End Region
    Private Sub SetUserMgmtNew()
        'SetUserMgmt(clsUserMgtCode.RCDFDashboard)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptCattleFeedProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btngo, "Press Alt+R Refresh ")

        todate.Value = clsCommon.GETSERVERDATE()
        SetFromData()

        AddHandler cvProduction.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvProduction.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill


        Reset()
    End Sub

    Private Sub SetFromData()
        txtfromDate.Value = todate.Value.AddYears(-1)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        dtProduction = Nothing
        gvProduction.DataSource = Nothing
        gvProduction.Rows.Clear()
        gvProduction.Columns.Clear()
        cvProduction.Series.Clear()

        EnableDisableCntrl(True)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 2
        clsCommon.ProgressBarPercentShow()
        Try
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading  CATTLE FEED PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Production()

            clsCommon.ProgressBarPercentHide()
            EnableDisableCntrl(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Load_Production()
        Try
            If dtProduction Is Nothing OrElse dtProduction.Rows.Count <= 0 Then
                Dim sQuery As String = "   select YearCode as GrpCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as GrpName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName] as GrpMonth,Quantity from (SELECT YEAR(PROD_DATE) YearCode,MONTH(PROD_DATE)MonthCode,format(PROD_DATE,'MMM') AS [MonthName],ROUND(SUM(FINAL_PRODUCTION_QTY)/1000,0) Quantity FROM TSPL_SPP_PRODUCTION_ENTRY
                                            LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY_DETAIL ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                                            WHERE  CONVERT(DATE, TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' 
                                            GROUP BY YEAR(PROD_DATE),MONTH(PROD_DATE),format(PROD_DATE,'MMM') 
                                            )xx  ORDER BY xx.YearCode,xx.MonthCode  "

                dtProduction = clsDBFuncationality.GetDataTable(sQuery)

            End If

            If dtProduction IsNot Nothing AndAlso dtProduction.Rows.Count > 0 Then
                cvProduction.Area.View.Palette = KnownPalette.Flower

                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvProduction.AreaDesign = CartesianArea1
                cvProduction.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtProduction.Rows(ii)("GrpMonth")
                    drTS("Value") = dtProduction.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvProduction.ShowTitle = True
                cvProduction.ChartElement.TitlePosition = TitlePosition.Top
                cvProduction.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvProduction.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvProduction.Title = "CATTLE FEED PRODUCTION"

                Dim smartLabelsController As New SmartLabelsController()
                cvProduction.Controllers.Add(smartLabelsController)
                cvProduction.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProduction.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProduction.DataSource = ds

                'LineSeries
                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim lineSeries As New LineSeries()
                    lineSeries.Name = ii
                    lineSeries.LegendTitle = ds.Tables(ii).TableName
                    lineSeries.ValueMember = "Value"
                    lineSeries.CategoryMember = "Name"
                    lineSeries.DataSource = ds.Tables(ii)
                    lineSeries.BorderWidth = 3
                    lineSeries.ShowLabels = True

                    cvProduction.Series.Add(lineSeries)

                Next

                cvProduction.ShowLegend = True
                setOrientation(cvProduction, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProduction.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)




                gvProduction.DataSource = Nothing
                gvProduction.Columns.Clear()
                gvProduction.Rows.Clear()
                gvProduction.GroupDescriptors.Clear()
                gvProduction.MasterTemplate.SummaryRowsBottom.Clear()
                gvProduction.ShowGroupPanel = False
                gvProduction.EnableFiltering = False
                gvProduction.AllowAddNewRow = False

                gvProduction.GroupDescriptors.Clear()
                gvProduction.TableElement.TableHeaderHeight = 40
                gvProduction.MasterTemplate.ShowRowHeaderColumn = False

                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvProduction.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        repoRate.Name = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n0}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvProduction.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")), gvProduction.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))) Then ''1
                        gvProduction.Rows.AddNew()
                        gvProduction.Rows(gvProduction.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName")), gvProduction.Rows.Count - 1) ''2
                    End If
                    gvProduction.Rows(arrRow.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtProduction.Rows(ii)(strValue)) ''3
                Next

                'repoRate = New GridViewDecimalColumn()
                'repoRate.FormatString = ""
                'repoRate.HeaderText = "Avg"
                'repoRate.Name = "TOTAL"
                'repoRate.Width = 100
                'repoRate.Minimum = 0
                'repoRate.FormatString = "{0:n0}"
                ''repoRate.DecimalPlaces = 2
                'repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                'gvProduction.MasterTemplate.Columns.Add(repoRate)

                'For jj As Integer = 0 To gvProduction.Rows.Count - 1
                '    For ii As Integer = 1 To gvProduction.Columns.Count - 2
                '        gvProduction.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvProduction.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvProduction.Rows(jj).Cells(ii).Value)
                '    Next
                '    gvProduction.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(gvProduction.Rows(jj).Cells("Total").Value, gvProduction.Columns.Count - 2)
                'Next

                For ii As Integer = 0 To gvProduction.Columns.Count - 1
                    gvProduction.Columns(ii).ReadOnly = True
                    gvProduction.Columns(ii).IsVisible = True
                    gvProduction.Columns(ii).Width = 150
                Next

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        categoricalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
        categoricalAxis.LabelRotationAngle = LableRotationAngel
        categoricalAxis.ForeColor = Color.WhiteSmoke
        categoricalAxis.BorderColor = Color.WhiteSmoke
        For Each item In categoricalAxis.Children
            Dim labelElement As AxisLabelElement = TryCast(item, AxisLabelElement)
            If labelElement IsNot Nothing Then
                labelElement.BorderColor = Color.Transparent
            End If
        Next

        Dim verticalAxis As LinearAxis = TryCast(chv.Axes(1), LinearAxis)
        verticalAxis.ForeColor = Color.WhiteSmoke
        verticalAxis.BorderColor = Color.WhiteSmoke

        For Each item In verticalAxis.Children
            Dim labelElement As AxisLabelElement = TryCast(item, AxisLabelElement)
            If labelElement IsNot Nothing Then
                labelElement.BorderColor = Color.Transparent
            End If
        Next
    End Sub



    Private Sub LegendElement_VisualItemCreating(sender As Object, e As LegendItemElementCreatingEventArgs)
        e.ItemElement = New CustomLegendItemElement(e.LegendItem)
    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        ToDate.Enabled = val
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gvProduction_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvProduction.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvProduction_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvProduction.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
End Class

Public Class CustomLegendItemElement
    Inherits LegendItemElement
    Public Sub New(item As LegendItem)
        MyBase.New(item)
        Me.Children.Remove(Me.MarkerElement)
        Me.TitleElement.DrawFill = True
        Me.TitleElement.DrawBorder = True
        Me.StretchHorizontally = True
    End Sub
    Protected Overrides Sub Synchronize()
        MyBase.Synchronize()
        Me.SyncVisualStyleProperties(Me.LegendItem.Element, Me.TitleElement)
        Me.TitleElement.ForeColor = Color.White
    End Sub
End Class