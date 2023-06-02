Imports common
Imports Telerik.Charting
Public Class VisualReportProcurement
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
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Reset")

        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        SetFiscalYear()


        LoadReportType()
        LoadMilkType()
        LoadFigure()

        LoadDuration()
        setDurationSplit()

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
        dr("Code") = "Federation"
        dr("Name") = "Federation Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Plant"
        dr("Name") = "Plant Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC"
        dr("Name") = "MCC Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Route"
        dr("Name") = "Route Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "VLC"
        dr("Name") = "VLC Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Day"
        dr("Name") = "Day Wise"
        dt.Rows.Add(dr)

        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"

        cboReportType.SelectedValue = "Federation"
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


        dr = dt.NewRow()
        dr("Code") = "FAT"
        dr("Name") = "FAT KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SNF"
        dr("Name") = "SNF KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AMT"
        dr("Name") = "Amount"
        dt.Rows.Add(dr)

        cboFigureIn.DataSource = dt
        cboFigureIn.ValueMember = "Code"
        cboFigureIn.DisplayMember = "Name"
    End Sub

    Sub LoadDuration()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Date Range"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Month Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Q"
        dr("Name") = "Quarter Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "HY"
        dr("Name") = "Half Yearly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yearly"
        dt.Rows.Add(dr)

        cboDuration.DataSource = dt
        cboDuration.ValueMember = "Code"
        cboDuration.DisplayMember = "Name"

        cboDuration.SelectedValue = "D"
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



    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            RadChartView1.Print(False)
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
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" &"'))

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
        Try
            Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            e.Cancel = True
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Federation") = CompairStringResult.Equal Then
                If Not arrDrillDownBack.Contains("Federation") Then
                    arrDrillDownBack.Add("Federation")
                End If
                cboReportType.SelectedValue = "Plant"
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Plant") = CompairStringResult.Equal Then
                If Not arrDrillDownBack.Contains("Plant") Then
                    arrDrillDownBack.Add("Plant")
                End If
                cboReportType.SelectedValue = "MCC"
                arrDrillDownPlant = New ArrayList()
                arrDrillDownPlant = txtPlant.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(strSelectedValue)
                txtPlant.arrValueMember = tmp

                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC") = CompairStringResult.Equal Then
                If Not arrDrillDownBack.Contains("MCC") Then
                    arrDrillDownBack.Add("MCC")
                End If
                cboReportType.SelectedValue = "Route"
                arrDrillDownMCC = New ArrayList()
                arrDrillDownMCC = txtMCC.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(strSelectedValue)
                txtMCC.arrValueMember = tmp

                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Route") = CompairStringResult.Equal Then
                If Not arrDrillDownBack.Contains("Route") Then
                    arrDrillDownBack.Add("Route")
                End If
                cboReportType.SelectedValue = "VLC"
                arrDrillDownRoute = New ArrayList()
                arrDrillDownRoute = txtRoute.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(strSelectedValue)
                txtRoute.arrValueMember = tmp
                Load_Report()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "VLC") = CompairStringResult.Equal Then
                If Not arrDrillDownBack.Contains("VLC") Then
                    arrDrillDownBack.Add("VLC")
                End If
                cboReportType.SelectedValue = "Day"
                arrDrillDownVLC = New ArrayList()
                arrDrillDownVLC = txtVLC.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(strSelectedValue)
                txtVLC.arrValueMember = tmp
                Load_Report()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                Throw New Exception("Please select Fiscal Year")
            End If
            Dim qry As String = "select Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid Fiscal Year [" + txtFiscalYear.Value + "]")
            End If
            Dim dtFYFrom As Date = dt.Rows(0)("Start_Date")
            Dim dtFYTo As Date = dt.Rows(0)("End_Date")


            Dim dtFrom As Date = txtFromDate.Value
            Dim dtTo As Date = txtToDate.Value
            If clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "M") = CompairStringResult.Equal Then
                dtFrom = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                dtTo = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If

            Dim strColumn2 As String = ""
            Dim strColumn2AfterGrp As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "D") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "M") = CompairStringResult.Equal Then
                strColumn2 = ",Monthly as GrpCode2"
                strColumn2AfterGrp = ",Monthly"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "Q") = CompairStringResult.Equal Then
                strColumn2 = ",Quarterly as GrpCode2"
                strColumn2AfterGrp = ",Quarterly"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "HY") = CompairStringResult.Equal Then
                strColumn2 = ",HalfYearly as GrpCode2"
                strColumn2AfterGrp = ",HalfYearly"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "Y") = CompairStringResult.Equal Then
                strColumn2 = ",Yearly as GrpCode2"
                strColumn2AfterGrp = ",Yearly"
            End If

            qry = clsMilkRejectHead.GetMCCRegisterQuery(dtFrom, dtTo, "M", "E", "", "", txtPlant.arrValueMember, txtMCC.arrValueMember, txtRoute.arrValueMember, txtVLC.arrValueMember, "", clsCommon.myCstr(cboMilkType.SelectedValue))
            qry = "SELECT  1 as Yearly,case when x.Date>='01/Apr/" + clsCommon.myCstr(dtFrom.Year) + "' and x.Date<'01/Sep/" + clsCommon.myCstr(dtFrom.Year) + "' then 1 else 2 end as HalfYearly,case when x.Date>='01/Apr/" + clsCommon.myCstr(dtFrom.Year) + "' and x.Date<'01/Jul/" + clsCommon.myCstr(dtFrom.Year) + "' then 1 else (case when x.Date>='01/Jul/" + clsCommon.myCstr(dtFrom.Year) + "' and x.Date<'01/Oct/" + clsCommon.myCstr(dtFrom.Year) + "' then 2 else (case when x.Date>='01/Oct/" + clsCommon.myCstr(dtFrom.Year) + "' and x.Date<'01/Jan/" + clsCommon.myCstr(dtFYTo.Year) + "' then 3 else 4 end ) end ) end as Quarterly,
            SUBSTRING( CAST(convert(date, x.Date,103) as varchar),1,7) as Monthly,'" + objCommonVar.CurrentCompanyCode + "' as CompanyCode,X.* FROM (" + qry + " )x"
            Dim sQuery As String = ""

            Dim arrRow As New List(Of String)

            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Federation") = CompairStringResult.Equal Then
                sQuery = "select CompanyCode as GrpCode,'" + objCommonVar.CurrentCompanyName + "' as GrpName " + strColumn2 + ",sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx group by CompanyCode" + strColumn2AfterGrp + " order by CompanyCode" + strColumn2AfterGrp + " "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Plant") = CompairStringResult.Equal Then
                sQuery = "select [Plant Code] as GrpCode,max([Plant Name]) as GrpName" + strColumn2 + ",sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Plant Code]" + strColumn2AfterGrp + " order by [Plant Code]" + strColumn2AfterGrp + ""
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC") = CompairStringResult.Equal Then
                sQuery = "select [MCC Code] as GrpCode,max(Short_Description_MCC) as GrpName " + strColumn2 + ",sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [MCC Code]" + strColumn2AfterGrp + " order by [MCC Code]" + strColumn2AfterGrp + ""
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Route") = CompairStringResult.Equal Then
                sQuery = "select [Route Code] as GrpCode,max(Short_Description_Route) as GrpName " + strColumn2 + ",sum( [Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [Route Code]" + strColumn2AfterGrp + " order by [Route Code]" + strColumn2AfterGrp + ""
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "VLC") = CompairStringResult.Equal Then
                sQuery = "select [VLC Code] as GrpCode,max(Short_Description_VLC) as GrpName " + strColumn2 + ",sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by [VLC Code]" + strColumn2AfterGrp + " order by [VLC Code]" + strColumn2AfterGrp + ""
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Day") = CompairStringResult.Equal Then
                sQuery = "select [Date] as GrpCode,max([Doc Date]) as GrpName " + strColumn2 + ",sum([Milk Weight]) as [Milk Weight],sum([FAT(KG)]) as [FAT(KG)],sum([SNF(KG)]) as [SNF(KG)],sum([SRN Amount]) as [SRN Amount] from (" + qry + " )xxxxxx Group by Date" + strColumn2AfterGrp + " order by Date"
            End If



            RadChartView1.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            RadChartView1.AreaDesign = CartesianArea1
            RadChartView1.Series.Clear()

            dt = clsDBFuncationality.GetDataTable(sQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "QTY") = CompairStringResult.Equal Then
                    strValue = "Milk Weight"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "FAT") = CompairStringResult.Equal Then
                    strValue = "FAT(KG)"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "SNF") = CompairStringResult.Equal Then
                    strValue = "SNF(KG)"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "AMT") = CompairStringResult.Equal Then
                    strValue = "SRN Amount"
                End If
                Dim dtNew As DataTable = dt.Copy
                If clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "D") = CompairStringResult.Equal Then
                    arrRow.Add(strValue)
                Else
                    Dim arr As New List(Of String)
                    dtNew = New DataTable()
                    dtNew.Columns.Add("GrpCode", GetType(String))
                    dtNew.Columns.Add("GrpName", GetType(String))
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If Not arrRow.Contains(clsCommon.myCstr(dt.Rows(ii)("GrpCode2"))) Then
                            dtNew.Columns.Add(clsCommon.myCstr(dt.Rows(ii)("GrpCode2")), GetType(Decimal))
                            arrRow.Add(clsCommon.myCstr(dt.Rows(ii)("GrpCode2")))
                        End If
                        If Not arr.Contains(clsCommon.myCstr(dt.Rows(ii)("GrpCode"))) Then
                            Dim drTS As DataRow = dtNew.NewRow()
                            drTS("GrpCode") = dt.Rows(ii)("GrpCode")
                            drTS("GrpName") = dt.Rows(ii)("GrpName")
                            dtNew.Rows.Add(drTS)
                            arr.Add(clsCommon.myCstr(dt.Rows(ii)("GrpCode")))
                        End If
                        dtNew.Rows(dtNew.Rows.Count - 1)(clsCommon.myCstr(dt.Rows(ii)("GrpCode2"))) = clsCommon.myCdbl(dt.Rows(ii)(strValue))
                    Next
                End If

                RadChartView1.ShowTitle = True
                RadChartView1.ChartElement.TitlePosition = TitlePosition.Top
                RadChartView1.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                qry = ""
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
                RadChartView1.Title = cboReportType.Text + " " + qry
                Dim smartLabelsController As New SmartLabelsController()
                RadChartView1.Controllers.Add(smartLabelsController)
                RadChartView1.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                RadChartView1.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = "GrpName"
                        barSeries.DataSource = dtNew
                        barSeries.CombineMode = ChartSeriesCombineMode.Cluster
                        barSeries.ShowLabels = True
                        barSeries.BorderBoxStyle = BorderBoxStyle.SingleBorder
                        barSeries.DrawLinesToLabels = True
                        barSeries.SyncLinesToLabelsColor = False
                        RadChartView1.Series.Add(barSeries)
                    Next
                    RadChartView1.ShowLegend = IIf(arrRow.Count > 1 AndAlso arrRow.Count < 15, True, False)
                    'If dt.Rows.Count < 7 Then
                    '    setOrientation(RadChartView1, "Vertical", "0")
                    'Else
                    '    setOrientation(RadChartView1, "Vertical", "270")
                    'End If
                    setOrientation(RadChartView1, "Vertical", "0")
                End If
                smartLabelsController.Strategy = strategy
                Dim IntHight As Integer = Me.RadChartView1.Size.Height
                Dim IntWidth As Integer = 0
                If (dtNew.Rows.Count + arrRow.Count) > 50 Then
                    IntWidth = 400 + (300 * (dtNew.Rows.Count + arrRow.Count))
                ElseIf (dtNew.Rows.Count + arrRow.Count) > 40 Then
                    IntWidth = 400 + (250 * (dtNew.Rows.Count + arrRow.Count))
                ElseIf (dtNew.Rows.Count + arrRow.Count) > 30 Then
                    IntWidth = 400 + (200 * (dtNew.Rows.Count + arrRow.Count))
                ElseIf (dtNew.Rows.Count + arrRow.Count) > 20 Then
                    IntWidth = 400 + (150 * (dtNew.Rows.Count + arrRow.Count))
                ElseIf (dtNew.Rows.Count + arrRow.Count) > 10 Then
                    IntWidth = 400 + (100 * (dtNew.Rows.Count + arrRow.Count))
                ElseIf (dtNew.Rows.Count + arrRow.Count) > 0 Then
                    IntWidth = 400 + (50 * (dtNew.Rows.Count + arrRow.Count))
                End If
                Me.RadChartView1.Size = New System.Drawing.Size(IntWidth, IntHight)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboDuration_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboDuration.SelectedValueChanged
        setDurationSplit()
    End Sub

    Sub setDurationSplit()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "D") = CompairStringResult.Equal Then
                txtFromDate.CustomFormat = "dd-MM-yyyy"
                txtToDate.CustomFormat = "dd-MM-yyyy"
                Panel1.Enabled = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboDuration.SelectedValue), "M") = CompairStringResult.Equal Then
                txtFromDate.CustomFormat = "MM-yyyy"
                txtToDate.CustomFormat = "MM-yyyy"
                Panel1.Enabled = True
            Else
                txtFromDate.CustomFormat = "dd-MM-yyyy"
                txtToDate.CustomFormat = "dd-MM-yyyy"
                Panel1.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("rptrpMGTlBal", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        SetFiscalYear()
    End Sub
    Sub SetFiscalYear()
        txtFromDate.MinDate = New Date(2001, 4, 1)
        txtFromDate.MaxDate = New Date(3000, 12, 1)
        txtToDate.MinDate = txtFromDate.MinDate
        txtToDate.MaxDate = txtFromDate.MaxDate
        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                txtFromDate.MinDate = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                txtFromDate.MaxDate = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                txtToDate.MinDate = txtFromDate.MinDate
                txtToDate.MaxDate = txtFromDate.MaxDate

                txtFromDate.Value = txtFromDate.MinDate
                txtToDate.Value = txtFromDate.MaxDate
            End If
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE()
            If txtToDate.Value.Month >= 1 AndAlso txtToDate.Value.Month <= 3 Then
                txtFromDate.Value = New Date(txtToDate.Value.Year - 1, 4, 1)
            Else
                txtFromDate.Value = New Date(txtToDate.Value.Year, 4, 1)
            End If
        End If
    End Sub

    Private Sub RadChartView1_LabelFormatting(sender As Object, e As ChartViewLabelFormattingEventArgs) Handles RadChartView1.LabelFormatting
        e.LabelElement.BackColor = Color.White

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