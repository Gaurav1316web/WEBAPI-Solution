Imports common
Imports Telerik.Charting
Imports Telerik.QuickStart.WinControls
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class rptCattleFeedProduction
    Inherits FrmMainTranScreen
    'Inherits ExamplesForm
    'Implements ICustomThemeExamplesForm
    Private collection1 As List(Of String), collection2 As List(Of String)
    Private lineCombineModes, areaCombineModes As BindingSource
    Private chartTypes As List(Of String)
    Public selectedChartType As String
    ''public selectedCombineMode As ChartSeriesCombineMode
    'Public showLabels As Boolean

#Region "Varibales"
    '    Dim ButtonToolTip As ToolTip = New ToolTip()
    '    Dim btnReferesh As Boolean = False

    Dim dtProduction As DataTable = Nothing

#End Region


    Sub Getdata()
        Try
            If dtProduction Is Nothing OrElse dtProduction.Rows.Count <= 0 Then
                Dim sQuery As String = " SELECT YEAR(PROD_DATE) GrpCode,MONTH(PROD_DATE)GrpMonth,max(TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE)LOCATION_CODE,DATENAME(MONTH, PROD_DATE) AS GrpName,ROUND(SUM(FINAL_PRODUCTION_QTY)/1000,0) Quantity FROM TSPL_SPP_PRODUCTION_ENTRY
LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY_DETAIL ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
WHERE TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE>=CONVERT(DATE,'01-03-2023',103) AND TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE<=CONVERT(DATE,'31-03-2025',103) 
GROUP BY YEAR(PROD_DATE),MONTH(PROD_DATE),DATENAME(MONTH, PROD_DATE)
ORDER BY YEAR(PROD_DATE) ,MONTH(PROD_DATE) "

                dtProduction = clsDBFuncationality.GetDataTable(sQuery)

            End If

            If dtProduction IsNot Nothing AndAlso dtProduction.Rows.Count > 0 Then
                cvProdution.Area.View.Palette = KnownPalette.Flower

                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvProdution.AreaDesign = CartesianArea1
                cvProdution.Series.Clear()
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


                cvProdution.ShowTitle = True
                cvProdution.ChartElement.TitlePosition = TitlePosition.Top
                cvProdution.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvProdution.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvProdution.Title = "CATTLE FEED PRODUCTION"

                Dim smartLabelsController As New SmartLabelsController()
                cvProdution.Controllers.Add(smartLabelsController)
                cvProdution.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProdution.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProdution.DataSource = ds

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

                    cvProdution.Series.Add(lineSeries)

                Next

                cvProdution.ShowLegend = True
                setOrientation(cvProdution, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProdution.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
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
    Private Function MonthNameToNumber(monthName As String) As Integer
        Select Case monthName.ToLower()
            Case "January" : Return 1
            Case "Febuary" : Return 2
            Case "March" : Return 3
            Case "April" : Return 4
            Case "May" : Return 5
            Case "June" : Return 6
            Case "July" : Return 7
            Case "August" : Return 8
            Case "September" : Return 9
            Case "October" : Return 10
            Case "November" : Return 11
            Case "December" : Return 12
            Case Else : Return 0 ' In case of an invalid month name
        End Select
    End Function
    Private Sub GenerateSeries(ByVal seriesType As String)
        Dim horizontalAxis As New CategoricalAxis()
        Dim verticalAxis As New LinearAxis()
        verticalAxis.AxisType = AxisType.Second

        For i As Integer = 0 To 1
            Dim series As CartesianSeries = Nothing

            If seriesType = "Area" Then
                series = New AreaSeries()
            ElseIf seriesType = "Line" Then
                series = New LineSeries()
            ElseIf seriesType = "Stepline" Then
                series = New SteplineSeries()
            End If

            series.PointSize = New SizeF(1, 1)
            series.HorizontalAxis = horizontalAxis
            series.VerticalAxis = verticalAxis
            series.BorderWidth = 1
            series.CategoryMember = "Month"
            series.ValueMember = "YRS"
            'series.DataSource = collection1
            series.DataSource = If(i = 0, collection1, collection2)
            'series.DataSource = ds
            'series.ShowLabels = showLabels
            'series.CombineMode = selectedCombineMode

            cvProdution.Series.Add(series)
        Next i

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Getdata()
    End Sub

    Private Sub rptCattleFeedProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        todate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = todate.Value.AddMonths(-1)
        'txtfromDate.Value = clsCommon.GETSERVERDATE()
    End Sub
End Class