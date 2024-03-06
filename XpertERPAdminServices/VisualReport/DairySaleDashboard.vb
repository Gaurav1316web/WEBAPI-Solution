Imports common
Imports Telerik.Charting

Public Class DairySaleDashboard
    'Public Class RCDFDashboard
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim dtFinishGoods As DataTable = Nothing
    Dim dtProduction As DataTable = Nothing

    Dim dtQuality As DataTable = Nothing
    Dim dtQualitySummary As DataTable = Nothing
    Dim dtQcPending As DataTable = Nothing

    Dim dtRMStock As DataTable = Nothing
    Dim dtRMSupply As DataTable = Nothing
    Dim dtRMInPlant As DataTable = Nothing
    Dim dtsales As DataTable = Nothing
    Dim dt1 As DataTable = Nothing
    Dim dt2 As DataTable = Nothing

    Dim dtAccountVendor As DataTable = Nothing
    Dim dtAccountCustomer As DataTable = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        'SetUserMgmt(clsUserMgtCode.DairySaleDashboard)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(RadButton1, "Press Alt+R Refresh ")

        txtToDate.Value = clsCommon.GETSERVERDATE()
        SetFromData()

        AddHandler cvProdution.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvProdution.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        AddHandler cvFinishGoods.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvFinishGoods.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill


        AddHandler cvSaleitem.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        AddHandler cvSaleitemWise.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating

        gvSales.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        gvAccountCustomer.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvAccountVendor.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        Reset()
    End Sub
    Private Sub txtToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtToDate.ValueChanged
        SetFromData()
    End Sub
    Private Sub SetFromData()
        txtFromDate.Value = txtToDate.Value.AddDays(-9)
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        cvSaleitem.Series.Clear()
        cvSaleitemWise.Series.Clear()

        gvSales.DataSource = Nothing
        gvSales.Rows.Clear()
        gvSales.Columns.Clear()

        dtsales = Nothing
        dt1 = Nothing
        dt2 = Nothing

        EnableDisableCntrl(True)
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 5
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading RAW MATERIAL Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Sale()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading  FINISH GOODS Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Finish_Goods()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_PRODUCTION()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading ACCOUNT Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Account()

            clsCommon.ProgressBarPercentHide()
            EnableDisableCntrl(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        txtLocation.Enabled = val
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub FrmMCCSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
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

    'Sub SetPieChartOrientation(ByVal chv As RadChartView, ByVal strOrient As String, ByVal LableRotationAngel As Integer)
    '    'Dim grid As CartesianGrid = chv.GetArea(Of CartesianArea)().GetGrid(Of CartesianGrid)()
    '    Dim grid As CartesianGrid = chv.GetArea(Of PieArea)().GetGrid(Of CartesianGrid)()
    '    'If clsCommon.CompairString(strOrient, "Horizontal") = CompairStringResult.Equal Then
    '    '    chv.GetArea(Of CartesianArea)().Orientation = Orientation.Horizontal
    '    '    grid.DrawVerticalStripes = True
    '    '    grid.DrawHorizontalStripes = False
    '    'Else
    '    '    chv.GetArea(Of CartesianArea)().Orientation = Orientation.Vertical
    '    '    grid.DrawVerticalStripes = False
    '    '    grid.DrawHorizontalStripes = True
    '    'End If

    '    Dim categoricalAxis As CategoricalAxis = TryCast(chv.Axes(0), CategoricalAxis)
    '    categoricalAxis.PlotMode = AxisPlotMode.OnTicksPadded
    '    categoricalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
    '    categoricalAxis.LabelRotationAngle = LableRotationAngel
    '    categoricalAxis.ForeColor = Color.WhiteSmoke
    '    categoricalAxis.BorderColor = Color.WhiteSmoke
    '    For Each item In categoricalAxis.Children
    '        Dim labelElement As AxisLabelElement = TryCast(item, AxisLabelElement)
    '        If labelElement IsNot Nothing Then
    '            labelElement.BorderColor = Color.Transparent
    '        End If
    '    Next

    '    Dim verticalAxis As LinearAxis = TryCast(chv.Axes(1), LinearAxis)
    '    verticalAxis.ForeColor = Color.WhiteSmoke
    '    verticalAxis.BorderColor = Color.WhiteSmoke

    '    For Each item In verticalAxis.Children
    '        Dim labelElement As AxisLabelElement = TryCast(item, AxisLabelElement)
    '        If labelElement IsNot Nothing Then
    '            labelElement.BorderColor = Color.Transparent
    '        End If
    '    Next
    'End Sub
    'Sub SetPieChartOrientation(ByVal chv As RadChartView, ByVal strOrient As String, ByVal LableRotationAngel As Integer)
    '    'Dim grid As carte = chv.GetArea(Of CartesianArea)().GetGrid(Of CartesianGrid)()
    '    If clsCommon.CompairString(strOrient, "Clockwise") = CompairStringResult.Equal Then
    '        chv.PieOrientation = PieOrientation.Clockwise
    '    Else
    '        chv.PieOrientation = PieOrientation.CounterClockwise
    '    End If

    '    Dim pieSeries As PieSeries = TryCast(chv.Series(0), PieSeries)
    '    pieSeries.LabelMode = Telerik.WinControls.UI.PieLabelModes.Radial

    '    For Each slice As PieDataPointElement In pieSeries.Children
    '        slice.LabelElement.ForeColor = Color.WhiteSmoke
    '        slice.LabelElement.BorderColor = Color.WhiteSmoke
    '        slice.LabelElement.Font = New Font(slice.LabelElement.Font.FontFamily, slice.LabelElement.Font.Size, FontStyle.Bold)
    '        slice.LabelElement.Text = slice.LabelElement.Text
    '        slice.LabelElement.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid
    '        slice.LabelElement.BackColor = Color.Transparent
    '        slice.LabelElement.BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
    '        slice.LabelElement.BorderWidth = 1
    '        slice.LabelElement.BorderColor = Color.Transparent
    '    Next
    'End Sub

    Public Sub Load_Report_Account()

    End Sub
    Public Sub Load_Report_Finish_Goods()

    End Sub


    Public Sub Load_Report_PRODUCTION()

    End Sub

    Public Sub Load_Report_Sale()

        Try
            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                Dim sQuery As String = " select count(distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) as No_Of_Booth,count( Distinct TSPL_DEMAND_BOOKING_MASTER.Route_No) as No_Of_Route,
	                                    count(distinct TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code) as No_Of_Distributor
	                                    from TSPL_DEMAND_BOOKING_MASTER 
	                                    left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
	                                    left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
	                                    left join TSPL_DISTRIBUTOR_ROUTE_CUSTOMER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No
                                        where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= convert(date,('" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy")) + "'),103) 
                                        and TSPL_ITEM_MASTER.Is_FreshItem=1 and TSPL_ITEM_MASTER.IsTaxable=0 and TSPL_DEMAND_BOOKING_MASTER.Route_No in 
                                        (select distinct TSPL_DEMAND_BOOKING_MASTER.Route_No  from TSPL_DEMAND_BOOKING_MASTER 
                                        left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                                        left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code 
                                        where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= convert(date,('" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy")) + "'),103) 
                                        and TSPL_ITEM_MASTER.Is_FreshItem=1 and TSPL_ITEM_MASTER.IsTaxable=0 )  "

                dt1 = clsDBFuncationality.GetDataTable(sQuery)
            End If
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                gvSales.DataSource = Nothing
                gvSales.Columns.Clear()
                gvSales.Rows.Clear()
                gvSales.GroupDescriptors.Clear()
                gvSales.MasterTemplate.SummaryRowsBottom.Clear()
                gvSales.ShowGroupPanel = False
                gvSales.EnableFiltering = False
                gvSales.AllowAddNewRow = False

                gvSales.GroupDescriptors.Clear()
                gvSales.TableElement.TableHeaderHeight = 40
                gvSales.MasterTemplate.ShowRowHeaderColumn = False
                gvSales.DataSource = dt1
                gvSales.Columns("No_Of_Distributor").HeaderText = "Distributor"
                gvSales.Columns("No_Of_Route").HeaderText = "Routes"
                gvSales.Columns("No_Of_Booth").HeaderText = "Booths"

                For ii As Integer = 0 To gvSales.Columns.Count - 1
                    gvSales.Columns(ii).ReadOnly = True
                    gvSales.Columns(ii).IsVisible = True
                    gvSales.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtsales Is Nothing OrElse dtsales.Rows.Count <= 0 Then
                Dim sQuery As String = "select Convert(varchar,Document_Date,103) as Document_Date,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise 
                                        from ( select convert(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date,
                                        (TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) as TotalLtr_ItemWise from TSPL_DEMAND_BOOKING_MASTER 
                                        left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                                        left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
	                                    where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy").AddDays(-3)) + "'
                                        and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy")) + "'
                                        and TSPL_ITEM_MASTER.Is_FreshItem=1 and TSPL_ITEM_MASTER.IsTaxable=0 )xx group by Document_Date"
                dtsales = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtsales IsNot Nothing AndAlso dtsales.Rows.Count > 0 Then
                cvSaleitem.Area.View.Palette = New CustomPalettes()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvSaleitem.AreaDesign = CartesianArea1
                cvSaleitem.Series.Clear()
                Dim strValue As String = "TotalLtr_ItemWise"
                Dim tempValue As Decimal = 0

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtsales.Rows.Count - 1

                    Dim dtNew As New DataTable
                    dtNew.TableName = clsCommon.myCstr(dtsales.Rows(ii)("TotalLtr_ItemWise"))
                    dtNew.Columns.Add("Name", GetType(String))
                    dtNew.Columns.Add("Value", GetType(Decimal))
                    ds.Tables.Add(dtNew)

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtsales.Rows(ii)("TotalLtr_ItemWise"))).NewRow()
                    drTS("Name") = dtsales.Rows(ii)("Document_Date")

                    drTS("Value") = dtsales.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtsales.Rows(ii)("TotalLtr_ItemWise"))).Rows.Add(drTS)
                Next

                cvSaleitem.ShowTitle = True
                cvSaleitem.ChartElement.TitlePosition = TitlePosition.Top
                cvSaleitem.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvSaleitem.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvSaleitem.Title = "SALES CHART"

                Dim smartLabelsController As New SmartLabelsController()
                cvSaleitem.Controllers.Add(smartLabelsController)
                cvSaleitem.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvSaleitem.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvSaleitem.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvSaleitem.Series.Add(barSeries)
                Next

                cvSaleitem.ShowLegend = True
                setOrientation(cvSaleitem, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvSaleitem.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dt2 Is Nothing OrElse dt2.Rows.Count <= 0 Then
                Dim sQuery As String = "select Structure_Code,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise 
                                        from( select TSPL_ITEM_MASTER.Structure_Code,(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) as TotalLtr_ItemWise from TSPL_DEMAND_BOOKING_MASTER 
                                        left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                                        left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code 
                                        where convert(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)= convert(date,('" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy")) + "'),103) 
                                        and TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )xx group by Structure_Code"
                dt2 = clsDBFuncationality.GetDataTable(sQuery)
            End If
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                'cvSaleitemWise.Area.View.Palette = New CustomPalette()
                'Dim chartView As New Telerik.WinControls.UI.RadChartView()
                'Dim pieAreaOwner As New Telerik.WinControls.UI.ChartView(chartView)
                'Dim piearea As Telerik.WinControls.UI.PieArea = New Telerik.WinControls.UI.PieArea(pieAreaOwner)
                'Dim pieSeries As New PieSeries()
                'cvSaleitemWise.Series.Add(pieSeries)
                'cvSaleitemWise.AreaDesign = piearea
                cvSaleitemWise.Series.Clear()

                cvSaleitemWise.ShowTitle = True
                cvSaleitemWise.ChartElement.TitlePosition = TitlePosition.Top
                cvSaleitemWise.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvSaleitemWise.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvSaleitemWise.Title = "ITEMS CHART"

                cvSaleitemWise.AreaType = ChartAreaType.Pie
                cvSaleitemWise.ShowLegend = True
                cvSaleitemWise.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)

                Dim pieSeries As New PieSeries()

                For Each row As DataRow In dt2.Rows
                    Dim value As Double = CDbl(row("TotalLtr_ItemWise"))
                    Dim label As String = row("Structure_Code").ToString()
                    pieSeries.DataPoints.Add(New PieDataPoint(value, label))

                Next

                pieSeries.ShowLabels = True
                pieSeries.LabelFormat = "{0:P2}"
                pieSeries.RadiusFactor = 0.9F

                cvSaleitemWise.Series.Add(pieSeries)

            End If

        Catch ex As Exception
        Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub gvSales_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvSales.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvSales_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvSales.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
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
    Private Sub LegendElement_VisualItemCreating(sender As Object, e As LegendItemElementCreatingEventArgs)
        e.ItemElement = New CustomLegendItemElement(e.LegendItem)
    End Sub


    Private Sub gvAccountVendor_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvAccountVendor.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountVendor_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAccountVendor.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountCustomer_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvAccountCustomer.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvAccountCustomer_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAccountCustomer.ViewCellFormatting

        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gvAccountVendor_RowFormatting_1(sender As Object, e As RowFormattingEventArgs) Handles gvAccountVendor.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
End Class

Public Class CustomPalettes
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







