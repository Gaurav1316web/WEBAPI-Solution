Imports common
Imports Telerik.Charting
Public Class RCDFDashboard
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim dtFinishGoods As DataTable = Nothing
    Dim dtProduction As DataTable = Nothing
    Dim dtQuality As DataTable = Nothing
#End Region

    Private Sub SetUserMgmtNew()
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

        gvQuality.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        'cvZone.Views.AddNew()
        'Dim controllerZone As New DrillDownController()
        'cvZone.Controllers.Add(controllerZone)
        'cvZone.ShowDrillNavigation = False
        'cvZonePie.ShowDrillNavigation = False
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
        dtFinishGoods = Nothing
        gvFinishGoods.DataSource = Nothing
        gvFinishGoods.Rows.Clear()
        gvFinishGoods.Columns.Clear()
        cvFinishGoods.Series.Clear()

        dtProduction = Nothing
        gvProdution.DataSource = Nothing
        gvProdution.Rows.Clear()
        gvProdution.Columns.Clear()
        cvProdution.Series.Clear()

        dtQuality = Nothing
        gvQuality.DataSource = Nothing
        gvQuality.Rows.Clear()
        gvQuality.Columns.Clear()

        'dtQualitysum = Nothing
        'gvQualitySummary.DataSource = Nothing
        'gvQualitySummary.Rows.Clear()
        'gvQualitySummary.Columns.Clear()

        EnableDisableCntrl(True)
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 5
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading RAW MATERIAL Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            'Load_Report_Zone()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading  FINISH GOODS Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Finish_Goods()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_PRODUCTION()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading QUALITY Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Quality()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading ACCOUNT Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            'Load_Report_ItemType()

            clsCommon.ProgressBarPercentHide()
            EnableDisableCntrl(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message)
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
    Private Sub cvZone_Drill(sender As Object, e As UI.DrillEventArgs)
        Try
            'Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            'e.Cancel = True
            'cvZonePie.Series.Clear()

            'scZone.Panel1Collapsed = True
            'scZone.Panel2Collapsed = False

            'Dim dt As New DataTable
            'dt.Columns.Add("Name", GetType(String))
            'dt.Columns.Add("Value", GetType(Decimal))
            'For ii As Integer = 0 To gvZone.Rows.Count - 1
            '    If clsCommon.CompairString(clsCommon.myCstr(gvZone.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
            '        For jj As Integer = 1 To gvZone.Columns.Count - 1
            '            Dim drTS As DataRow = dt.NewRow()
            '            drTS("Name") = gvZone.Columns(jj).HeaderText
            '            drTS("Value") = clsCommon.myCdbl(gvZone.Rows(ii).Cells(jj).Value)
            '            dt.Rows.Add(drTS)
            '        Next
            '        Exit For
            '    End If
            'Next
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    cvZonePie.ShowTitle = True
            '    cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
            '    cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
            '    cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
            '    cvZonePie.AreaType = ChartAreaType.Pie
            '    cvZonePie.ShowLegend = True
            '    cvZonePie.View.Margin = New Padding(0, 15, 0, 15)
            '    Me.cvZonePie.AreaType = ChartAreaType.Pie
            '    Dim series As New PieSeries()
            '    For Each dr As DataRow In dt.Rows
            '        series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
            '    Next
            '    series.ShowLabels = True
            '    Me.cvZonePie.Series.Add(series)

            '    'Dim strategy As New PieTwoLabelColumnsStrategy()
            '    'cvZonePie.ShowTitle = True
            '    'cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
            '    'cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
            '    'cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
            '    'cvZonePie.AreaType = ChartAreaType.Pie
            '    'cvZonePie.ShowLegend = True
            '    'cvZonePie.View.Margin = New Padding(60, 0, 50, 0)
            '    'Dim series As New PieSeries()
            '    'series.Range = New AngleRange(270, 360)
            '    'series.LabelFormat = "{0:P2}"
            '    'series.RadiusFactor = 0.9F
            '    'series.ValueMember = "Value"
            '    'series.DataSource = dt
            '    'series.ShowLabels = True
            '    'series.DrawLinesToLabels = True
            '    'series.SyncLinesToLabelsColor = True
            '    'series.DisplayMember = "Name"
            '    'cvZonePie.Series.Add(series)

            '    'For Each item As LegendItem In Me.cvZonePie.ChartElement.LegendElement.Provider.LegendInfos
            '    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
            '    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
            '    '    item.Title = clsCommon.myCstr(row("Name"))
            '    'Next
            'End If
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

    Public Sub Load_Report_Finish_Goods()
        Try
            If dtFinishGoods Is Nothing OrElse dtFinishGoods.Rows.Count <= 0 Then
                Dim sQuery As String = "Select convert(varchar, GrpMonth,103) as GrpMonth,GrpCode,max(GrpName) as GrpName,Sum(Quantity)/1000 As Quantity from (
select  convert(date, Document_Date,103) as GrpMonth,price_CodeNon as GrpCode,price_CodeNon as GrpName,Qty as Quantity   from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Date,
CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION','GOVTCR','GOSHALA','DCS','KVSS') then TSPL_CUSTOMER_MASTER.price_CodeNon else 'OTHER' end as price_CodeNon,
(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Qty
from TSPL_SD_SALE_INVOICE_DETAIL
left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and TSPL_SD_SALE_INVOICE_DETAIL.Location='" + txtLocation.Value + "' "
                End If
                sQuery += " union
select convert(date, thedate,103) as PROD_DATE,CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION','GOVTCR','GOSHALA','DCS','KVSS') then TSPL_CUSTOMER_MASTER.price_CodeNon else 'OTHER' end as price_CodeNon,0 as Qty from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),(select price_CodeNon from TSPL_CUSTOMER_MASTER where len(price_CodeNon)>0 group by price_CodeNon) as TSPL_CUSTOMER_MASTER
)x 
)xxxxx Group by GrpMonth,GrpCode order by GrpMonth "
                dtFinishGoods = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtFinishGoods IsNot Nothing AndAlso dtFinishGoods.Rows.Count > 0 Then
                cvFinishGoods.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvFinishGoods.AreaDesign = CartesianArea1
                cvFinishGoods.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtFinishGoods.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtFinishGoods.Rows(ii)("GrpMonth")
                    drTS("Value") = dtFinishGoods.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next

                cvFinishGoods.ShowTitle = True
                cvFinishGoods.ChartElement.TitlePosition = TitlePosition.Top
                cvFinishGoods.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvFinishGoods.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvFinishGoods.Title = "PRODUCTION CHART"

                Dim smartLabelsController As New SmartLabelsController()
                cvFinishGoods.Controllers.Add(smartLabelsController)
                cvFinishGoods.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvFinishGoods.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvFinishGoods.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvFinishGoods.Series.Add(barSeries)
                Next
                cvFinishGoods.ShowLegend = True
                setOrientation(cvFinishGoods, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvFinishGoods.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)


                gvFinishGoods.DataSource = Nothing
                gvFinishGoods.Columns.Clear()
                gvFinishGoods.Rows.Clear()
                gvFinishGoods.GroupDescriptors.Clear()
                gvFinishGoods.MasterTemplate.SummaryRowsBottom.Clear()
                gvFinishGoods.ShowGroupPanel = False
                gvFinishGoods.EnableFiltering = False
                gvFinishGoods.AllowAddNewRow = False

                gvFinishGoods.GroupDescriptors.Clear()
                gvFinishGoods.TableElement.TableHeaderHeight = 40
                gvFinishGoods.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvFinishGoods.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtFinishGoods.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvFinishGoods.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")), gvFinishGoods.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth"))) Then
                        gvFinishGoods.Rows.AddNew()
                        gvFinishGoods.Rows(gvFinishGoods.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth")), gvFinishGoods.Rows.Count - 1)
                    End If
                    gvFinishGoods.Rows(arrRow.Item(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtFinishGoods.Rows(ii)(strValue))
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "Total"
                repoRate.Name = "Total"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n2}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvFinishGoods.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvFinishGoods.Rows.Count - 1
                    For ii As Integer = 1 To gvFinishGoods.Columns.Count - 2
                        gvFinishGoods.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvFinishGoods.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvFinishGoods.Rows(jj).Cells(ii).Value)
                    Next
                Next

                For ii As Integer = 0 To gvFinishGoods.Columns.Count - 1
                    gvFinishGoods.Columns(ii).ReadOnly = True
                    gvFinishGoods.Columns(ii).IsVisible = True
                    gvFinishGoods.Columns(ii).Width = 150
                Next


                '
                'gvFinishGoods.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvFinishGoods_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvFinishGoods.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvFinishGoods_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvFinishGoods.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Load_Report_PRODUCTION()
        Try
            If dtProduction Is Nothing OrElse dtProduction.Rows.Count <= 0 Then
                Dim sQuery As String = "Select convert(varchar, GrpMonth,103) as GrpMonth,GrpCode,max(GrpName) as GrpName,Sum(Quantity)/1000 As Quantity from ( 
select PROD_DATE as GrpMonth,ITEM_CODE as GrpCode,Short_Description as GrpName,FINAL_PRODUCTION_QTY as Quantity   from (
SELECT   TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Short_Description,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY
FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL
LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.item_code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
WHERE CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE in ('FG0002','FG0003','FG0001')"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE='" + txtLocation.Value + "' "
                End If
                sQuery += " union
                select convert(date, thedate,103) as PROD_DATE,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Short_Description,0 as FINAL_PRODUCTION_QTY from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),TSPL_ITEM_MASTER
where TSPL_ITEM_MASTER.ITEM_CODE in ('FG0002','FG0003','FG0001')
                )x
)xxxxx Group by GrpMonth,GrpCode order by GrpMonth"
                dtProduction = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtProduction IsNot Nothing AndAlso dtProduction.Rows.Count > 0 Then
                cvProdution.Area.View.Palette = New CustomPalette()
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

                cvProdution.Title = "PRODUCTION CHART"

                Dim smartLabelsController As New SmartLabelsController()
                cvProdution.Controllers.Add(smartLabelsController)
                cvProdution.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProdution.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProdution.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvProdution.Series.Add(barSeries)
                Next
                cvProdution.ShowLegend = True
                setOrientation(cvProdution, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProdution.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)


                gvProdution.DataSource = Nothing
                gvProdution.Columns.Clear()
                gvProdution.Rows.Clear()
                gvProdution.GroupDescriptors.Clear()
                gvProdution.MasterTemplate.SummaryRowsBottom.Clear()
                gvProdution.ShowGroupPanel = False
                gvProdution.EnableFiltering = False
                gvProdution.AllowAddNewRow = False

                gvProdution.GroupDescriptors.Clear()
                gvProdution.TableElement.TableHeaderHeight = 40
                gvProdution.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvProdution.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvProdution.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")), gvProdution.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))) Then
                        gvProdution.Rows.AddNew()
                        gvProdution.Rows(gvProdution.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")), gvProdution.Rows.Count - 1)
                    End If
                    gvProdution.Rows(arrRow.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtProduction.Rows(ii)(strValue))
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "Total"
                repoRate.Name = "Total"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n2}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvProdution.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvProdution.Rows.Count - 1
                    For ii As Integer = 1 To gvProdution.Columns.Count - 2
                        gvProdution.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvProdution.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvProdution.Rows(jj).Cells(ii).Value)
                    Next
                Next

                For ii As Integer = 0 To gvProdution.Columns.Count - 1
                    gvProdution.Columns(ii).ReadOnly = True
                    gvProdution.Columns(ii).IsVisible = True
                    gvProdution.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvProdution_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvProdution.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvProdution_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvProdution.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Load_Report_Quality()
        Try
            If dtQuality Is Nothing OrElse dtQuality.Rows.Count <= 0 Then
                Dim sQuery As String = "select  TSPL_GRN_HEAD.Ref_No,TSPL_ITEM_MASTER.Item_Desc,TSPL_GRN_HEAD.VehicleNo,CASE WHEN TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2  or TabQC.QC_Status='Rejected' then 'Rejected' else CASE WHEN (TabQC.QC_Status in ('Accepted','Under Deviation') or TSPL_GRN_HEAD.VisualQCStatus=5) then 'Accepted' else 'Pending' END END AS QCStaus,ISNULL(TabQC.InputDataDeductionPer,0) AS DEDPer
from TSPL_GRN_HEAD
left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
left join (select Gate_Entry_No,max(QC_Status) as QC_Status,sum(InputDataDeductionPer) as InputDataDeductionPer from (
select TSPL_QC_CHECK_HEAD.Gate_Entry_No,TSPL_QC_CHECK_HEAD.QC_Status,ISNULL(TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer,0)  as InputDataDeductionPer
from  TSPL_QC_CHECK_HEAD  
left outer join TSPL_QC_CHECK_SRN_DETAIL ON TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code 
)x group by Gate_Entry_No) TabQC on TabQC.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
where convert(date,TSPL_GRN_HEAD.GRN_Date,103) = '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_GRN_HEAD.Bill_To_Location='" + txtLocation.Value + "' "
                End If
                sQuery += " AND TSPL_GRN_DETAIL.Item_Code LIKE 'RM%'
order by TSPL_GRN_HEAD.GRN_Date desc"
                dtQuality = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtQuality IsNot Nothing AndAlso dtQuality.Rows.Count > 0 Then
                lblQuality.Text = "Status of " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + ""

                gvQuality.DataSource = Nothing
                gvQuality.Columns.Clear()
                gvQuality.Rows.Clear()
                gvQuality.GroupDescriptors.Clear()
                gvQuality.MasterTemplate.SummaryRowsBottom.Clear()
                gvQuality.ShowGroupPanel = False
                gvQuality.EnableFiltering = False
                gvQuality.AllowAddNewRow = False

                gvQuality.GroupDescriptors.Clear()
                gvQuality.TableElement.TableHeaderHeight = 40
                gvQuality.MasterTemplate.ShowRowHeaderColumn = False
                gvQuality.DataSource = dtQuality
                gvQuality.Columns("Ref_No").HeaderText = "RM"
                gvQuality.Columns("Item_Desc").HeaderText = "ITEM NAME"
                gvQuality.Columns("VehicleNo").HeaderText = "TRUCK NO"
                gvQuality.Columns("QCStaus").HeaderText = "QC STATUS"
                gvQuality.Columns("DEDPer").HeaderText = "DEDUCTION"

                For ii As Integer = 0 To gvQuality.Columns.Count - 1
                    gvQuality.Columns(ii).ReadOnly = True
                    gvQuality.Columns(ii).IsVisible = True
                    gvQuality.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvQuality_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvQuality.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQuality_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvQuality.ViewCellFormatting
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







