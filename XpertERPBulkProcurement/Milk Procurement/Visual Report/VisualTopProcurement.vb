Imports common
Imports Telerik.Charting
Public Class VisualTopProcurement
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Public strCurrentGrp As String = Nothing
    Dim arrDrillDownBack As List(Of String)
    Public arrDrillDownPlant As ArrayList
    Public arrDrillDownMCC As ArrayList
    Public arrDrillDownRoute As ArrayList
    Public arrDrillDownVLC As ArrayList
    Private radPrintDocument1 As RadPrintDocument
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Reset")

        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()


        LoadReportType()
        LoadMilkType()
        LoadFigure()

        'LoadDuration()
        'setDurationSplit()

        RadChartView1.Views.AddNew()

        Dim controller As New DrillDownController()
        Me.RadChartView1.Controllers.Add(controller)
        Me.RadChartView1.ShowDrillNavigation = False

        Reset()
    End Sub

    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "TopRoute"
        dr("Name") = "Top 10 Route"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "TopDCS"
        dr("Name") = "Top 10 DCS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "BottomRoute"
        dr("Name") = "Bottom 10 Route"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "BottomDCS"
        dr("Name") = "Bottom 10 DCS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"

    End Sub

    Sub LoadMilkType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "All"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Mix"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Cow"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Buffalo"
        dt.Rows.Add(dr)


        cboMilkType.DataSource = dt
        cboMilkType.ValueMember = "Code"
        cboMilkType.DisplayMember = "Name"
    End Sub

    Sub LoadFigure()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "QTY"
        dr("Name") = "Quantity"
        dt.Rows.Add(dr)


        'dr = dt.NewRow()
        'dr("Code") = "FAT"
        'dr("Name") = "FAT KG"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "SNF"
        'dr("Name") = "SNF KG"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AMT"
        dr("Name") = "Amount"
        dt.Rows.Add(dr)

        cboFigureIn.DataSource = dt
        cboFigureIn.ValueMember = "Code"
        cboFigureIn.DisplayMember = "Name"
    End Sub

    'Sub LoadDuration()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = "D"
    '    dr("Name") = "Date Range"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "M"
    '    dr("Name") = "Month Wise"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Q"
    '    dr("Name") = "Quarter Wise"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "HY"
    '    dr("Name") = "Half Yearly"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Y"
    '    dr("Name") = "Yearly"
    '    dt.Rows.Add(dr)

    '    cboDuration.DataSource = dt
    '    cboDuration.ValueMember = "Code"
    '    cboDuration.DisplayMember = "Name"

    '    cboDuration.SelectedValue = "D"
    'End Sub

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
    End Sub

    Sub Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        arrDrillDownBack = New List(Of String)
        RadGroupBox1.Enabled = True
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        Load_Report()
        RadGroupBox1.Enabled = False
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try

            RadChartView1.PrintPreview(Me.radPrintDocument1)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, "No Data found To Print", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
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

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)


        Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue)
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim filePath As String
        sfd.FileName = Me.Text
        sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx|CSV Files (*.csv) |*.csv"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            '     transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
        Process.Start(filePath)
        'common.clsCommon.MyMessageBoxShow(Me, ex.Message)
    End Sub

    Private Sub txtPlant__My_Click(sender As Object, e As EventArgs) Handles txtPlant._My_Click
        Dim qry As String = "select  Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Type='PLANT'"
        txtPlant.arrValueMember = clsCommon.ShowMultipleSelectForm("VRPCUPLT", qry, "Code", "Name", txtPlant.arrValueMember, txtPlant.arrDispalyMember)
        RefreshMCC()
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2 "
        If txtPlant.arrValueMember IsNot Nothing AndAlso txtPlant.arrValueMember.Count > 0 Then
            qry += "  and TSPL_MCC_MASTER.plant_code in (" + clsCommon.GetMulcallString(txtPlant.arrValueMember) + ")"
        End If
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("VrPCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("vrCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  where 2=2 "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VRPCULC1", qry, "Code", "Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshMCC()
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            Dim qry As String = "select MCC_Code from TSPL_MCC_MASTER where MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  and Plant_Code in (" + clsCommon.GetMulcallString(txtPlant.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtMCC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
                txtMCC.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub
    Private Sub RadChartView1_Drill(sender As Object, e As UI.DrillEventArgs) Handles RadChartView1.Drill
        'Try
        '    Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
        '    e.Cancel = True
        '    If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Federation") = CompairStringResult.Equal Then
        '        If Not arrDrillDownBack.Contains("Federation") Then
        '            arrDrillDownBack.Add("Federation")
        '        End If
        '        cboReportType.SelectedValue = "Plant"
        '        Load_Report()
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Plant") = CompairStringResult.Equal Then
        '        If Not arrDrillDownBack.Contains("Plant") Then
        '            arrDrillDownBack.Add("Plant")
        '        End If
        '        cboReportType.SelectedValue = "MCC"
        '        arrDrillDownPlant = New ArrayList()
        '        arrDrillDownPlant = txtPlant.arrValueMember()

        '        Dim tmp As New ArrayList()
        '        tmp.Add(strSelectedValue)
        '        txtPlant.arrValueMember = tmp

        '        Load_Report()
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC") = CompairStringResult.Equal Then
        '        If Not arrDrillDownBack.Contains("MCC") Then
        '            arrDrillDownBack.Add("MCC")
        '        End If
        '        cboReportType.SelectedValue = "Route"
        '        arrDrillDownMCC = New ArrayList()
        '        arrDrillDownMCC = txtMCC.arrValueMember()

        '        Dim tmp As New ArrayList()
        '        tmp.Add(strSelectedValue)
        '        txtMCC.arrValueMember = tmp

        '        Load_Report()
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Route") = CompairStringResult.Equal Then
        '        If Not arrDrillDownBack.Contains("Route") Then
        '            arrDrillDownBack.Add("Route")
        '        End If
        '        cboReportType.SelectedValue = "VLC"
        '        arrDrillDownRoute = New ArrayList()
        '        arrDrillDownRoute = txtRoute.arrValueMember()

        '        Dim tmp As New ArrayList()
        '        tmp.Add(strSelectedValue)
        '        txtRoute.arrValueMember = tmp
        '        Load_Report()
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "VLC") = CompairStringResult.Equal Then
        '        If Not arrDrillDownBack.Contains("VLC") Then
        '            arrDrillDownBack.Add("VLC")
        '        End If
        '        cboReportType.SelectedValue = "Day"
        '        arrDrillDownVLC = New ArrayList()
        '        arrDrillDownVLC = txtVLC.arrValueMember()

        '        Dim tmp As New ArrayList()
        '        tmp.Add(strSelectedValue)
        '        txtVLC.arrValueMember = tmp
        '        Load_Report()
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Federation") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Plant") = CompairStringResult.Equal AndAlso arrDrillDownBack.Contains("Federation") Then
                arrDrillDownBack.Remove("Federation")
                cboReportType.SelectedValue = "Federation"
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC") = CompairStringResult.Equal AndAlso arrDrillDownBack.Contains("Plant") Then
                arrDrillDownBack.Remove("Plant")
                cboReportType.SelectedValue = "Plant"
                txtPlant.arrValueMember = arrDrillDownPlant
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Route") = CompairStringResult.Equal AndAlso arrDrillDownBack.Contains("MCC") Then
                arrDrillDownBack.Remove("MCC")
                cboReportType.SelectedValue = "MCC"
                txtMCC.arrValueMember = arrDrillDownMCC
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "VLC") = CompairStringResult.Equal AndAlso arrDrillDownBack.Contains("Route") Then
                arrDrillDownBack.Remove("Route")
                cboReportType.SelectedValue = "Route"
                txtRoute.arrValueMember = arrDrillDownRoute
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Day") = CompairStringResult.Equal AndAlso arrDrillDownBack.Contains("VLC") Then
                arrDrillDownBack.Remove("VLC")
                cboReportType.SelectedValue = "VLC"
                txtVLC.arrValueMember = arrDrillDownVLC
                Load_Report()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Load_Report()
        Try
            Dim dtFrom As Date = txtFromDate.Value
            Dim dtTo As Date = txtToDate.Value
            Dim strOrderColumn As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "QTY") = CompairStringResult.Equal Then
                strOrderColumn = "Milk Weight"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "AMT") = CompairStringResult.Equal Then
                strOrderColumn = "SRN Amount"
            End If
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed

            Dim qry As String = clsMilkRejectHead.GetMCCRegisterQuery(dtFrom, dtTo, "M", "E", "", "", txtPlant.arrValueMember, txtMCC.arrValueMember, txtRoute.arrValueMember, txtVLC.arrValueMember, "", clsCommon.myCstr(cboMilkType.SelectedValue))
            Dim sQuery As String = ""
            Dim arrRow As New List(Of String)
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "TopRoute") = CompairStringResult.Equal Then
                sQuery = "select top 10 [Route Code] as GrpCode,max(Short_Description_Route) as GrpName,sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Route Code] order by [" + strOrderColumn + "] desc"
                LoadChart(RadChartView1, sQuery, strOrderColumn, cboReportType.Text)
                RadPageView1.SelectedPage = RadPageViewPage2
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "TopDCS") = CompairStringResult.Equal Then
                sQuery = "select top 10 [VLC Code] as GrpCode,max(Short_Description_VLC) as GrpName,sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [VLC Code] order by  [" + strOrderColumn + "] desc"
                LoadChart(RadChartView1, sQuery, strOrderColumn, cboReportType.Text)
                RadPageView1.SelectedPage = RadPageViewPage2
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "BottomRoute") = CompairStringResult.Equal Then
                sQuery = "select top 10 [Route Code] as GrpCode,max(Short_Description_Route) as GrpName,sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Route Code] order by [" + strOrderColumn + "]"
                LoadChart(RadChartView1, sQuery, strOrderColumn, cboReportType.Text)
                RadPageView1.SelectedPage = RadPageViewPage2
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "BottomDCS") = CompairStringResult.Equal Then
                sQuery = "select top 10 [VLC Code] as GrpCode,max(Short_Description_VLC) as GrpName,sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [VLC Code] order by  [" + strOrderColumn + "]"
                LoadChart(RadChartView1, sQuery, strOrderColumn, cboReportType.Text)
                RadPageView1.SelectedPage = RadPageViewPage2
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "All") = CompairStringResult.Equal Then
                sQuery = "select top 5 [Route Code] as GrpCode,max(Short_Description_Route) as GrpName,sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Route Code] order by [" + strOrderColumn + "] desc"
                LoadChart(RC1, sQuery, strOrderColumn, "Top 5 Route")

                sQuery = "select top 5 [VLC Code] as GrpCode,max(Short_Description_VLC) as GrpName,sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [VLC Code] order by  [" + strOrderColumn + "] desc"
                LoadChart(RC3, sQuery, strOrderColumn, "Top 5 DCS")

                sQuery = "select top 5 [Route Code] as GrpCode,max(Short_Description_Route) as GrpName,sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Route Code] order by [" + strOrderColumn + "]"
                LoadChart(RC2, sQuery, strOrderColumn, "Bottom 5 Route")

                sQuery = "select top 5 [VLC Code] as GrpCode,max(Short_Description_VLC) as GrpName,sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [VLC Code] order by  [" + strOrderColumn + "]"
                LoadChart(RC4, sQuery, strOrderColumn, "Bottom 5 DCS")
                RadPageView1.SelectedPage = RadPageViewPage3
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadChart(ByVal Chart As RadChartView, ByVal sQuery As String, ByVal strOrderColumn As String, ByVal strcboReportType As String)
        Chart.Area.View.Palette = New CustomPalette()
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Chart.AreaDesign = CartesianArea1
        Chart.Series.Clear()

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Chart.ShowTitle = True
            Chart.ChartElement.TitlePosition = TitlePosition.Top
            Chart.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
            Dim qry As String = ""
            If txtPlant.arrValueMember IsNot Nothing AndAlso txtPlant.arrValueMember.Count > 0 Then
                If txtPlant.arrValueMember.Count = 1 Then
                    qry += "->Plant [" + clsLocation.GetName(txtPlant.arrValueMember(0), Nothing) + "]"
                End If
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                If txtMCC.arrValueMember.Count = 1 Then
                    qry += "->MCC [" + clsMccMaster.GetName(txtMCC.arrValueMember(0), Nothing) + "]"
                End If
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                If txtRoute.arrValueMember.Count = 1 Then
                    qry += "->Route [" + clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" + txtRoute.arrValueMember(0) + "'")) + "]"
                End If
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                If txtVLC.arrValueMember.Count = 1 Then
                    qry += "->VLC [" + clsfrmVLCMaster.getVLcNameOnVLcCode(txtVLC.arrValueMember(0), Nothing) + "]"
                End If
            End If

            Chart.Title = cboFigureIn.Text + " Wise " + strcboReportType + " " + qry
            Dim smartLabelsController As New SmartLabelsController()
            Chart.Controllers.Add(smartLabelsController)
            Chart.ShowSmartLabels = True
            Dim strategy As SmartLabelsStrategyBase = Nothing
            Chart.AreaType = ChartAreaType.Cartesian
            strategy = New VerticalAdjusmentLabelsStrategy()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr As DataRow In dt.Rows
                    Dim dtNew As New DataTable
                    dtNew.Columns.Add("GrpCode", GetType(String))
                    dtNew.Columns.Add("GrpName", GetType(String))
                    dtNew.Columns.Add(strOrderColumn, GetType(Decimal))

                    Dim drTS As DataRow = dtNew.NewRow()
                    drTS("GrpCode") = dr("GrpCode")
                    drTS("GrpName") = dr("GrpName")
                    drTS(strOrderColumn) = dr(strOrderColumn)
                    dtNew.Rows.Add(drTS)


                    Dim barSeries As New BarSeries()
                    barSeries.Name = clsCommon.myCstr(dr("GrpName"))
                    barSeries.LegendTitle = clsCommon.myCstr(dr("GrpName"))
                    barSeries.ValueMember = strOrderColumn
                    barSeries.CategoryMember = "GrpName"
                    barSeries.DataSource = dtNew
                    barSeries.CombineMode = ChartSeriesCombineMode.Cluster
                    barSeries.ShowLabels = True
                    barSeries.BorderBoxStyle = BorderBoxStyle.SingleBorder
                    barSeries.DrawLinesToLabels = True
                    barSeries.SyncLinesToLabelsColor = False
                    Chart.Series.Add(barSeries)
                Next
                Chart.ShowLegend = True
                'If dt.Rows.Count < 7 Then
                '    setOrientation(Chart, "Vertical", "0")
                'Else
                '    setOrientation(Chart, "Vertical", "270")
                'End If
                setOrientation(Chart, "Vertical", "0")
            End If
            smartLabelsController.Strategy = strategy
        End If
    End Sub

    Private Sub RadChartView1_LabelFormatting(sender As Object, e As ChartViewLabelFormattingEventArgs) Handles RadChartView1.LabelFormatting
        e.LabelElement.BackColor = Color.White

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            RadChartView1.Print()
            'RadChartView1.PrintPreview(Me.radPrintDocument1)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, "No Data found To Print", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
