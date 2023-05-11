Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports Telerik.Charting
Imports System.IO
Public Class RptRouteWiseTrendBar
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim tmpValLoad As Boolean = True

    Public ReportLevel As Integer = 0
    Public strCurrentGrp As String = Nothing
    Public Frm_Date As Date
    Dim arrLoc As String = Nothing

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MccSummaryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub

  

    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadVSP()
        Reset()
        If ReportLevel = 1 Then
            Dim arr As New ArrayList()

            txtFromDate.Value = Frm_Date
            txtToDate.Value = Frm_Date
            Load_Report()
        End If
    End Sub
    
    Private Sub chkVSPAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
    End Sub
    Sub LoadVSP()
        Dim qry As String = "select Route_Code as [Code] ,Route_Name as [Name]  from TSPL_MCC_ROUTE_MASTER "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String = String.Empty
            Dim Couner As Integer = 0
            sQuery = "Select Max(xxx.MCC) As ROUTE_CODE, xxx.DATEView, Cast(Sum(xxx.[Qty (KG)]) As decimal(18,0)) As [Qty (KG)], Cast(Sum(xxx.Amount) / 100 As decimal(18,0)) As [Amount ( Hundred )], Cast(Sum(xxx.Amount) As decimal(18,0)) As Amt From (Select TSPL_MILK_SRN_HEAD.DOC_CODE, Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,111) DATEView, TSPL_MILK_SRN_HEAD.DOC_DATE, TSPL_MILK_SRN_HEAD.ROUTE_CODE, (TSPL_MCC_ROUTE_MASTER.Route_Name) As MCC, TSPL_MILK_SRN_DETAIL.ACC_Qty As [Qty (KG)], TSPL_MILK_SRN_DETAIL.FAT_KG As [FAT (KG)], TSPL_MILK_SRN_DETAIL.SNF_KG As [SNF (KG)], TSPL_MILK_SRN_DETAIL.AMOUNT As Amount, TSPL_MILK_SRN_HEAD.SHIFT    From TSPL_MILK_SRN_DETAIL Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE    Where TSPL_MILK_SRN_HEAD.ROUTE_CODE Is Not Null "

            sQuery += " And TSPL_MILK_SRN_HEAD.DOC_DATE>=convert(date,'" & txtFromDate.Value & "',103) and TSPL_MILK_SRN_HEAD.DOC_DATE<=convert(date,'" & txtToDate.Value & "',103) "
            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                sQuery += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If
            sQuery += " And 2 = 2) xxx Group By xxx.DATEView, xxx.ROUTE_CODE"

            Dim RadChartView1 As New RadChartView()
            RadChartView1.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            RadChartView1.AreaDesign = CartesianArea1
            RadChartView1.Series.Clear()
            'AddHandler RadChartView1.LabelFormatting, AddressOf radChartView_LabelFormatting
            Dim obj As clsCreateBIReport = clsCreateBIReport.GetData("CRRTRENDB", True, NavigatorType.Current)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
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

                RadChartView1.ShowTitle = True
                RadChartView1.ChartElement.TitlePosition = TitlePosition.Top
                RadChartView1.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                RadChartView1.Title = "Route Wise Trend (Bar)"
                Dim smartLabelsController As New SmartLabelsController()
                RadChartView1.Controllers.Add(smartLabelsController)
                RadChartView1.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing

                Dim combineMode As ChartSeriesCombineMode = ChartSeriesCombineMode.None
                If clsCommon.CompairString("Cluster", "Cluster") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Cluster
                End If


                RadChartView1.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = strColumn
                        barSeries.DataSource = dt
                        barSeries.CombineMode = combineMode
                        barSeries.ShowLabels = "1"

                        barSeries.DrawLinesToLabels = True
                        barSeries.SyncLinesToLabelsColor = True
                        RadChartView1.Series.Add(barSeries)
                    Next
                    RadChartView1.ShowLegend = IIf(arrRow.Count > 1, True, False)
                    setOrientation(RadChartView1, "Vertical", "270")
                End If



                Dim PanelContainer As Telerik.WinControls.UI.RadScrollablePanelContainer = New Telerik.WinControls.UI.RadScrollablePanelContainer()
                Dim RadScrollablePanel2 As RadScrollablePanel = New RadScrollablePanel()

                TableLayoutPanel1.Controls.Add(RadScrollablePanel2, Couner Mod 2, Math.Floor(Couner / 2))

                RadScrollablePanel2.Dock = System.Windows.Forms.DockStyle.Fill
                RadScrollablePanel2.Location = New System.Drawing.Point(0, 0)
                RadScrollablePanel2.Name = "RSP" + clsCommon.myCstr(Couner)

                RadScrollablePanel2.PanelContainer.Controls.Add(RadChartView1)

                RadChartView1.Dock = System.Windows.Forms.DockStyle.Fill

            End If
            Couner += 1


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
        categoricalAxis.LabelFitMode = AxisLabelFitMode.Rotate
        categoricalAxis.LabelRotationAngle = LableRotationAngel
    End Sub
    
    Sub Reset()

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

   


    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        Load_Report()
      
        tmpValLoad = False
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    

    

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub FrmMCCSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

   

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.ExciseSummary1 & "'"))

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            '     transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
