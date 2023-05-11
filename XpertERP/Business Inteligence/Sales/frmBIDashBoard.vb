Imports common
Imports System.IO
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Class frmBIDashBoard
    Inherits FrmMainTranScreen


    Private Sub FrmBIMonthWiseSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadData()

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BITopExpence)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Sub LoadData()
        ''Load month Wise Sale
        Dim dtSale As DataTable = clsDBFuncationality.GetDataTable(FrmBIMonthWiseSale.GetQuery(txtToDate.Value.AddMonths(-11), txtToDate.Value, 100000))
        RadChart1.ChartTitle.TextBlock.Text = ""
        RadChart1.PlotArea.XAxis.DataLabelsColumn = "DateMonth"
        If dtSale IsNot Nothing AndAlso dtSale.Rows.Count > 0 Then
            RadChart1.DataManager.LabelsColumn = "Amount"
            RadChart1.ChartTitle.TextBlock.Text = "Month wise Sale"
            RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical
            RadChart1.AutoTextWrap = True
            RadChart1.IntelligentLabelsEnabled = True
            RadChart1.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart1.Series.Clear()
            RadChart1.DataSource = Nothing
            RadChart1.DataSource = dtSale
            RadChart1.Skin = "Gradient"
            RadChart1.DefaultType = Charting.ChartSeriesType.Line
            RadChart1.DataBind()
            RadChart1.Update()
        End If

        ''Load Top 5 Customers
        Dim dtCustomer As DataTable = clsDBFuncationality.GetDataTable(frmBITopCustomer.GetQuery(txtToDate.Value.AddMonths(-1), txtToDate.Value, 100000, 5))
        RadChart2.ChartTitle.TextBlock.Text = ""
        RadChart2.PlotArea.XAxis.DataLabelsColumn = "Customer_Name"
        If dtCustomer IsNot Nothing AndAlso dtCustomer.Rows.Count > 0 Then
            RadChart2.ChartTitle.TextBlock.Text = "Top 5 Customers"
            RadChart2.DataManager.LabelsColumn = "Customer_Name"
            RadChart2.AutoTextWrap = True
            RadChart2.IntelligentLabelsEnabled = True
            RadChart2.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart2.Series.Clear()
            RadChart2.DataSource = Nothing
            RadChart2.DataSource = dtCustomer
            RadChart2.Skin = "Gradient"
            RadChart2.DefaultType = Charting.ChartSeriesType.Pie

            RadChart2.DataBind()
            RadChart2.Update()
        End If

        ''Load Bank Closing Balance
        Dim dtBank As DataTable = clsDBFuncationality.GetDataTable(FrmBankBookClosing.GetBankClosingQry(clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"), "100000", True, Nothing))
        RadChart3.ChartTitle.TextBlock.Text = ""
        RadChart3.PlotArea.XAxis.DataLabelsColumn = "Bank"
        If dtBank IsNot Nothing AndAlso dtBank.Rows.Count > 0 Then
            RadChart3.DataManager.LabelsColumn = "ClosingBal"
            RadChart3.ChartTitle.TextBlock.Text = "Bank Closing Balance"
            RadChart3.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
            RadChart3.AutoTextWrap = True
            RadChart3.IntelligentLabelsEnabled = True
            RadChart3.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart3.Series.Clear()
            RadChart3.DataSource = Nothing
            RadChart3.DataSource = dtBank
            RadChart3.Skin = "Gradient"
            RadChart3.DefaultType = Charting.ChartSeriesType.Bar
            RadChart3.DataBind()
            RadChart3.Update()
        End If


        ''Load Top 5 Expences
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(frmBITopExpence.GetQry(txtFromDate.Value, txtToDate.Value, 100000, 5))
        RadChart4.ChartTitle.TextBlock.Text = ""
        RadChart4.PlotArea.XAxis.DataLabelsColumn = "Customer_Name"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            RadChart4.DataManager.LabelsColumn = "Amount"
            RadChart4.ChartTitle.TextBlock.Text = "Top 5 Expenses"
            RadChart4.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
            RadChart4.Legend.Appearance.Visible = False
            RadChart4.AutoTextWrap = True
            RadChart4.IntelligentLabelsEnabled = True
            RadChart4.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart4.Series.Clear()
            RadChart4.DataSource = Nothing
            RadChart4.DataSource = dt
            RadChart4.Skin = "Gradient"
            RadChart4.DefaultType = Charting.ChartSeriesType.Bar
            RadChart4.DataBind()
            RadChart4.Update()
        End If

        If IsGenerateExcelChart Then
            GenerateChart1(dtSale, dtCustomer, dtBank, dt)
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        LoadData()
    End Sub

    Public Shared Sub GenerateExcelChart(ByVal dt As DataTable, ByVal EnuChartType As Integer, ByVal Title As String, ByVal LabelColumn As String, ByVal ValuColumn1 As String, Optional ByVal ValuColumn2 As String = "", Optional ByVal ValuColumn3 As String = "")
        Try
            Try
                Dim excel As New Excel.Application
                excel.Visible = True
                excel.Workbooks.Add()
                excel.Range("A1").Value2 = LabelColumn
                excel.Range("B1").Value2 = ValuColumn1
                If clsCommon.myLen(ValuColumn2) > 0 Then
                    excel.Range("C1").Value2 = ValuColumn2
                End If
                If clsCommon.myLen(ValuColumn3) > 0 Then
                    excel.Range("D1").Value2 = ValuColumn3
                End If

                Dim ii As Integer = 2
                For Each dr As DataRow In dt.Rows
                    excel.Range("A" & ii).Value2 = dr(LabelColumn)
                    excel.Range("B" & ii).Value2 = dr(ValuColumn1)
                    If clsCommon.myLen(ValuColumn2) > 0 Then
                        excel.Range("C" & ii).Value2 = dr(ValuColumn2)
                    End If
                    If clsCommon.myLen(ValuColumn3) > 0 Then
                        excel.Range("D" & ii).Value2 = dr(ValuColumn3)
                    End If
                    ii += 1
                Next
                Dim range As Excel.Range = excel.Range("A1")
                Dim chart As Excel.Chart = excel.ActiveWorkbook.Charts.Add()
                Dim chart1 As Excel.Chart = excel.ActiveWorkbook.Charts.Add()
                chart.ChartWizard(Source:=range.CurrentRegion, Title:="Title")
                If EnuChartType = 1 Then
                    chart.ChartStyle = 32
                    chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered
                ElseIf EnuChartType = 8 Then
                    chart.ChartStyle = 34
                    chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie
                ElseIf EnuChartType = 4 Then
                    chart.ChartStyle = 34
                    chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
                ElseIf EnuChartType = 5 Then
                    chart.ChartStyle = 34
                    chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DArea
                End If
                chart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowValue)
                '----
                chart1.ChartWizard(Source:=range.CurrentRegion, Title:="Title")
                If EnuChartType = 1 Then
                    chart1.ChartStyle = 32
                    chart1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered
                ElseIf EnuChartType = 8 Then
                    chart1.ChartStyle = 34
                    chart1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie
                ElseIf EnuChartType = 4 Then
                    chart1.ChartStyle = 34
                    chart1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
                ElseIf EnuChartType = 5 Then
                    chart1.ChartStyle = 34
                    chart1.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DArea
                End If
                chart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowValue)
                '----
                excel.Visible = True
                'chart.Ind
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Dim IsGenerateExcelChart As Boolean = False
    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            IsGenerateExcelChart = True
            LoadData()
            IsGenerateExcelChart = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsGenerateExcelChart = False
        End Try
    End Sub

    Public oexcel As Excel.Application
    Public obook As Excel.Workbook
    Public osheet As Excel.Worksheet

    Private Sub GenerateChart1(ByVal dtSale As DataTable, ByVal dtCustomer As DataTable, ByVal dtBank As DataTable, ByVal dt As DataTable)
        oexcel = CreateObject("Excel.Application")
        'add a new workbook
        obook = oexcel.Workbooks.Add
        'check total sheets in workbook
        Dim S As Integer = oexcel.Application.Sheets.Count()
        'leaving first sheet delete all the remaining sheets
        If S > 1 Then
            oexcel.Application.DisplayAlerts = False
            Dim J As Integer = S
            Do While J > 1
                oexcel.Application.Sheets(J).delete()
                J = oexcel.Application.Sheets.Count()
            Loop
        End If
        osheet = oexcel.Worksheets(1)
        'rename the sheet
        osheet.Name = "Excel Charts"
        oexcel.DisplayFullScreen = True
        '------------------------------------------------------------Chart [Month Wise Sale]--------------------
        'columns heading
        osheet.Range("W1").Value = "DateMonth"
        osheet.Range("X1").Value = "Amount"
        'populate data from DB
        Dim R As Integer = 1
        For Each dr As DataRow In dtSale.Rows
            R = R + 1
            osheet.Range("W" & R).Value = dr("DateMonth")
            osheet.Range("X" & R).Value = dr("Amount")
        Next
        'create chart objects
        Dim oChart As Excel.Chart
        Dim MyCharts As Excel.ChartObjects
        Dim MyCharts1 As Excel.ChartObject
        MyCharts = osheet.ChartObjects
        MyCharts1 = MyCharts.Add(0, 0, 500, 250)
        oChart = MyCharts1.Chart
        With oChart
            Dim chartRange As Excel.Range
            chartRange = osheet.Range("W1", "X" & R)
            .SetSourceData(chartRange)
            .PlotBy = Excel.XlRowCol.xlColumns
            .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
            .HasLegend = True
            .Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight
            .HasTitle = True
            .ChartTitle.Text = "Month Wise Sale"
            .HasTitle = True
            .ChartStyle = 34
            .ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
        End With

        '-1----------------------------------------------------------Chart [Top 5 Customers]--------------------
        'columns heading
        osheet.Range("Y1").Value = "Customer_Name"
        osheet.Range("Z1").Value = "Amount"
        R = 1
        For Each dr As DataRow In dtCustomer.Rows
            R = R + 1
            osheet.Range("Y" & R).Value = dr("Customer_Name")
            osheet.Range("Z" & R).Value = dr("Amount")
        Next
        MyCharts = osheet.ChartObjects
        MyCharts1 = MyCharts.Add(505, 0, 500, 250)
        oChart = MyCharts1.Chart
        With oChart
            Dim chartRange As Excel.Range
            chartRange = osheet.Range("Y1", "Z" & R)
            .SetSourceData(chartRange)
            .PlotBy = Excel.XlRowCol.xlColumns
            .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
            .HasLegend = True
            .Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight
            .HasTitle = True
            .ChartTitle.Text = "Top 5 Customers"
            .HasTitle = True
            .ChartStyle = 34
            .ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie
        End With

        '-2----------------------------------------------------------Chart [Bank Closing Balance]--------------------
        'columns heading
        osheet.Range("AA1").Value = "Bank"
        osheet.Range("AB1").Value = "ClosingBal"
        R = 1
        For Each dr As DataRow In dtBank.Rows
            R = R + 1
            osheet.Range("AA" & R).Value = dr("Bank")
            osheet.Range("AB" & R).Value = dr("ClosingBal")
        Next
        MyCharts = osheet.ChartObjects
        MyCharts1 = MyCharts.Add(0, 255, 500, 250)
        oChart = MyCharts1.Chart
        With oChart
            Dim chartRange As Excel.Range
            chartRange = osheet.Range("AA1", "AB" & R)
            .SetSourceData(chartRange)
            .PlotBy = Excel.XlRowCol.xlColumns
            .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
            .HasLegend = True
            .Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight
            .HasTitle = True
            .ChartTitle.Text = "Bank Closing Balance"
            .HasTitle = True
            .ChartStyle = 32
            .ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered
        End With

        '------------------------------------------------------------Chart [Top 5 Expenses]--------------------
        'columns heading
        osheet.Range("AC1").Value = "Customer_Name"
        osheet.Range("AD1").Value = "Amount"
        R = 1
        For Each dr As DataRow In dt.Rows
            R = R + 1
            osheet.Range("AC" & R).Value = dr("Customer_Name")
            osheet.Range("AD" & R).Value = dr("Amount")
        Next
        MyCharts = osheet.ChartObjects
        MyCharts1 = MyCharts.Add(505, 255, 500, 250)
        oChart = MyCharts1.Chart
        With oChart
            Dim chartRange As Excel.Range
            chartRange = osheet.Range("AC1", "AD" & R)
            .SetSourceData(chartRange)
            .PlotBy = Excel.XlRowCol.xlColumns
            .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
            .HasLegend = True
            .Legend.Position = Excel.XlLegendPosition.xlLegendPositionRight
            .HasTitle = True
            .ChartTitle.Text = "Top 5 Expenses"
            .HasTitle = True
            .ChartStyle = 3
            .ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DColumnClustered
        End With

        oexcel.Visible = True
    End Sub


End Class
