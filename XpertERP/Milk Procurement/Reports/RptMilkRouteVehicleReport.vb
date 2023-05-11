Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports Telerik.Charting
Public Class RptMilkRouteVehicleReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public obj As New clsCreateBIReport()
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptMilkRouteVehicleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptMilkRouteVehicleReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

      
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnGraphicStatus, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
     

        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMilkRouteVehicleReport & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If RbMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If

                If RbRouteSelect.IsChecked Then
                    Dim strRouteName As String = ""
                    For Each StrName As String In cbgRoute.CheckedDisplayMember
                        If clsCommon.myLen(strRouteName) > 0 Then
                            strRouteName += ", "
                        End If
                        strRouteName += StrName
                    Next
                    Dim strRouteCode As String = ""
                    For Each StrCode As String In cbgRoute.CheckedValue
                        If clsCommon.myLen(strRouteCode) > 0 Then
                            strRouteCode += ", "
                        End If
                        strRouteCode += StrCode
                    Next
                    arrHeader.Add(("Route Name: " + strRouteName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(" Milk Route Vehicle Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub



    Private Sub RptMilkRouteVehicleReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

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
    Sub LoadMCC()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If

    End Sub
    Sub LoadRoute()
        Dim qry As String = "select Route_Code as [Code] ,Route_Name as [Name] from TSPL_MCC_ROUTE_MASTER  "
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Code"
        cbgRoute.DisplayMember = "Name"

    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            If RbMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")
            End If
            If RbRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Route or select all.")
            End If
            sQuery = " Select xxx.*,  Case When xxx.[Vehicle Capicity] = 0 Then 0    Else Convert(decimal(18,2),(xxx.[Total Receipt Capicity ] /    xxx.[Vehicle Capicity]) * 100) End As [Per Vechicle]From (Select final.MCC_Code As [MCC Code],        final.MCC_NAME As [MCC Name],   convert(varchar,final.DOC_DATE,103) as DOC_DATE, final.DOC_DATE as DATE,      final.SHIFT As Shift,      final.Route_CODE As [Route No.],    final.Route_Name As [Route Name],    final.Vehicle_Code As [Vehicle No.],    final.[Vehicle Name],    final.[Max Storage Capacity] As [Vehicle Capicity],    final.Milk_Weight As [Total Receipt Capicity ],   cast ( final.Milk_Weight as numeric(15,2)) - cast ( final.[Max Storage Capacity] as numeric(15,2))  As Difference  From (Select xx.*,      TSPL_MCC_ROUTE_MASTER.Route_Name,      TSPL_MILK_Shift_End_Route_DETAIL.Milk_Weight,      TSPL_Primary_Vehicle_Master.Storage_Capacity As [Max Storage Capacity]    From (Select Max(TSPL_MCC_MASTER.MCC_Code) As MCC_Code,        Max(TSPL_MCC_MASTER.MCC_NAME) As MCC_NAME,        Max(TSPL_MILK_Shift_End_HEAD.DOC_DATE) As DOC_DATE,        Max(TSPL_MILK_Shift_End_HEAD.DOC_CODE) As DOC_CODE,        TSPL_MILK_Shift_End_HEAD.SHIFT,        (TSPL_Primary_Vehicle_Master.Vehicle_Code) As Vehicle_Code,        TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE,        Max(TSPL_Primary_Vehicle_Master.Description) As [Vehicle Name]      From TSPL_MILK_Shift_End_Route_DETAIL        Left Outer Join TSPL_MILK_Shift_End_HEAD          On TSPL_MILK_Shift_End_HEAD.DOC_CODE =          TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE        Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =          TSPL_MILK_Shift_End_HEAD.MCC_CODE        Left Outer Join TSPL_Primary_Vehicle_Master          On TSPL_Primary_Vehicle_Master.Vehicle_Code =          TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE      Group By  TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE, TSPL_Primary_Vehicle_Master.Vehicle_Code,        TSPL_MILK_Shift_End_HEAD.DOC_DATE,TSPL_MILK_Shift_End_HEAD.SHIFT) xx      Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL        On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = xx.DOC_CODE And        TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = xx.Route_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code        = xx.Route_CODE      Left Outer Join TSPL_Primary_Vehicle_Master        On TSPL_Primary_Vehicle_Master.Vehicle_Code =        TSPL_MCC_ROUTE_MASTER.Vehicle_Code) final) xxx"
            sQuery += " where 2 = 2  and  convert(date,DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date,DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when convert(date,DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date,DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when convert(date,DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date,DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If

            If cbgMCC.CheckedValue.Count > 0 Then 'RbMCCSelect.IsChecked And
                sQuery += " and  [MCC Code]  in (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ")"
            End If
            If RbRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
                sQuery += " and [Route No.] in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            sQuery += "  order by Date "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
              

                RadPageView1.SelectedPage = RadPageViewPage2

            Else

                clsCommon.MyMessageBoxShow("No Data Found")
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()


        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            
        Next

        gv.Columns("MCC Code").IsVisible = True
        gv.Columns("MCC Code").Width = 100
        gv.Columns("MCC Code").HeaderText = " MCC Code"



        gv.Columns("MCC Name").IsVisible = True
        gv.Columns("MCC Name").Width = 100
        gv.Columns("MCC Name").HeaderText = " MCC Name"

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = " Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("Shift").IsVisible = False
        gv.Columns("Shift").Width = 100
        gv.Columns("Shift").HeaderText = " Shift"

        gv.Columns("Route No.").IsVisible = False
        gv.Columns("Route No.").Width = 100
        gv.Columns("Route No.").HeaderText = " Route No."


        gv.Columns("Shift").IsVisible = True
        gv.Columns("Shift").Width = 80
        gv.Columns("Shift").HeaderText = "Shift"

        gv.Columns("Route Name").IsVisible = True
        gv.Columns("Route Name").Width = 80
        gv.Columns("Route Name").HeaderText = "Route Name"

        gv.Columns("Vehicle No.").IsVisible = True
        gv.Columns("Vehicle No.").Width = 80
        gv.Columns("Vehicle No.").HeaderText = "Vehicle No"

        gv.Columns("Vehicle Name").IsVisible = True
        gv.Columns("Vehicle Name").Width = 50
        gv.Columns("Vehicle Name").HeaderText = "Vehicle Name"

        gv.Columns("Vehicle Capicity").IsVisible = True
        gv.Columns("Vehicle Capicity").Width = 100
        gv.Columns("Vehicle Capicity").HeaderText = "Vehicle Capacity"

        gv.Columns("Total Receipt Capicity ").IsVisible = True
        gv.Columns("Total Receipt Capicity ").Width = 100
        gv.Columns("Total Receipt Capicity ").HeaderText = "Total Receipt Capacity"

        gv.Columns("Difference").IsVisible = True
        gv.Columns("Difference").Width = 100
        gv.Columns("Difference").HeaderText = "Difference"

        gv.Columns("Per Vechicle").IsVisible = True
        gv.Columns("Per Vechicle").Width = 100
        gv.Columns("Per Vechicle").HeaderText = "Per Vehicle"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Total Receipt Capicity ", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadMCC()
        LoadRoute()
        RbMCCAll.IsChecked = True
        RbRouteAll.IsChecked = True



        LoadShiftFrom()
        LoadShiftTo()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Try
            RadChartView2.Series.Clear()
            RadChartView2.DataSource = Nothing
        Catch ex As Exception
        End Try

    End Sub

    Private Sub RbMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RbMCCAll.ToggleStateChanged
        cbgMCC.Enabled = RbMCCSelect.IsChecked
        If RbMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub RbRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RbRouteAll.ToggleStateChanged
        cbgRoute.Enabled = RbRouteSelect.IsChecked
    End Sub

    Sub Load_Chart(ByVal qry As String)
        Try
            RadChartView2.Series.Clear()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arrValueCol As New List(Of String)
                If clsCommon.myLen(obj.Chart_Series_Member) > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not arrValueCol.Contains(clsCommon.myCstr(dr(obj.Chart_Series_Member))) Then
                            arrValueCol.Add(clsCommon.myCstr(dr(clsCommon.myCstr(obj.Chart_Series_Member))))
                        End If
                    Next 'dr cond.
                End If 'dt cond.

                If clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Bar") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
                        For Each strVal As String In arrValueCol
                            Dim dv As DataView = dt.DefaultView
                            dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
                            Dim barSeries As New BarSeries()
                            barSeries.Name = obj.Description
                            barSeries.LegendTitle = strVal
                            barSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                            barSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                            barSeries.DataSource = dv.ToTable()
                            barSeries.ShowLabels = True
                            RadChartView2.Series.Add(barSeries)
                            RadChartView2.ShowLegend = True
                        Next
                    Else
                        Dim barSeries As New BarSeries()
                        barSeries.Name = obj.Description
                        barSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                        barSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                        barSeries.DataSource = dt
                        barSeries.ShowLabels = True
                        RadChartView2.Series.Add(barSeries)
                        RadChartView2.ShowLegend = False
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Pie") = CompairStringResult.Equal Then
                    Dim series As New PieSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                    series.DataSource = dt
                    series.ShowLabels = True
                    series.DrawLinesToLabels = True
                    series.SyncLinesToLabelsColor = True
                    RadChartView2.ShowLegend = True
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.Series.Add(series)
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                    For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                        Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                        Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                        item.Title = clsCommon.myCstr(row(clsCommon.myCstr(obj.Chart_Category_Member)))
                    Next
                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Line") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
                        For Each strVal As String In arrValueCol
                            Dim dv As DataView = dt.DefaultView
                            dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
                            Dim lineSeries As New LineSeries()
                            lineSeries.Name = obj.Description
                            lineSeries.LegendTitle = strVal
                            lineSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                            lineSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                            lineSeries.DataSource = dv.ToTable()
                            lineSeries.BorderWidth = 2
                            lineSeries.ShowLabels = True
                            RadChartView2.Series.Add(lineSeries)
                            RadChartView2.ShowLegend = True
                        Next
                    Else
                        Dim lineSeries As New LineSeries()
                        lineSeries.Name = obj.Description
                        lineSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                        lineSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                        lineSeries.DataSource = dt
                        lineSeries.BorderWidth = 2
                        lineSeries.ShowLabels = True
                        RadChartView2.Series.Add(lineSeries)
                        RadChartView2.ShowLegend = False
                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Area") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    If arrValueCol IsNot Nothing AndAlso arrValueCol.Count > 0 Then
                        For Each strVal As String In arrValueCol
                            Dim dv As DataView = dt.DefaultView
                            dv.RowFilter = "" + clsCommon.myCstr(obj.Chart_Series_Member) + "=" + strVal + ""
                            Dim AreaSeries As New AreaSeries()
                            AreaSeries.Name = obj.Description
                            AreaSeries.LegendTitle = strVal
                            AreaSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                            AreaSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                            AreaSeries.DataSource = dv.ToTable()
                            AreaSeries.BorderWidth = 2
                            AreaSeries.ShowLabels = True
                            RadChartView2.Series.Add(AreaSeries)
                            RadChartView2.ShowLegend = True
                        Next
                    Else
                        Dim AreaSeries As New AreaSeries()
                        AreaSeries.Name = obj.Description
                        AreaSeries.LegendTitle = ""
                        AreaSeries.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                        AreaSeries.CategoryMember = clsCommon.myCstr(obj.Chart_Category_Member)
                        AreaSeries.DataSource = dt
                        AreaSeries.BorderWidth = 2
                        AreaSeries.ShowLabels = True
                        RadChartView2.Series.Add(AreaSeries)
                        RadChartView2.ShowLegend = False
                    End If

                ElseIf clsCommon.CompairString(clsCommon.myCstr(obj.Chart_Type), "Donut") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Pie
                    Dim series As New DonutSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.InnerRadiusFactor = 50 / 100
                    series.ValueMember = clsCommon.myCstr(obj.Chart_Value_Member)
                    series.DataSource = dt
                    series.ShowLabels = True
                    RadChartView2.ShowLegend = True
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.Series.Add(series)
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                    For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                        Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                        Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                        item.Title = clsCommon.myCstr(row(obj.Chart_Category_Member))
                    Next
                        End If

                    
            End If
            RadPageView1.SelectedPage = RadPageViewPage3
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGraphicStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraphicStatus.Click
        Dim sQuery As String
        'Public Chart_Value_Member As String = ""
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater then to Date")
        End If
        If RbMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            Throw New Exception("Please select atleast single MCC or select all.")
        End If
        If RbRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
            Throw New Exception("Please select atleast single Route or select all.")
        End If
        sQuery = " Select xxxxx.RangeName,  Sum(1) As Value From (Select Case      When xxxx.[% Vehicle Utilization] > 0 And xxxx.[% Vehicle Utilization] <=      30 Then '0-30' Else Case        When xxxx.[% Vehicle Utilization] > 30 And        xxxx.[% Vehicle Utilization] <= 60 Then '31-60' Else Case          When xxxx.[% Vehicle Utilization] > 60 Then 'Above 60' End End    End As RangeName  From (Select xxx.*,      Case When xxx.[Vehicle Capicity] = 0 Then 0        Else Convert(decimal(18,2),(xxx.[Total Receipt Capicity ] /        xxx.[Vehicle Capicity]) * 100) End As [% Vehicle Utilization]    From (Select final.MCC_Code As [MCC Code],        final.MCC_Code As MCC,        final.MCC_NAME As [MCC Name],        final.DOC_DATE,        Convert(varchar,final.DOC_DATE,103) As Date,        final.SHIFT As Shift,        final.Route_CODE As Route,        final.Route_CODE As [Route No.],        final.Route_Name As [Route Name],        final.Vehicle_Code As [Vehicle No.],        final.[Vehicle Name],        final.[Max Storage Capacity] As [Vehicle Capicity],        final.Milk_Weight As [Total Receipt Capicity ],        final.Milk_Weight - final.[Max Storage Capacity] As Difference      From (Select xx.*,          TSPL_MCC_ROUTE_MASTER.Route_Name,          TSPL_MILK_Shift_End_Route_DETAIL.Milk_Weight,          TSPL_Primary_Vehicle_Master.Storage_Capacity As [Max Storage Capacity]        From (Select Max(TSPL_MCC_MASTER.MCC_Code) As MCC_Code,            Max(TSPL_MCC_MASTER.MCC_NAME) As MCC_NAME,            Max(TSPL_MILK_Shift_End_HEAD.DOC_DATE) As DOC_DATE,            Max(TSPL_MILK_Shift_End_HEAD.DOC_CODE) As DOC_CODE,            TSPL_MILK_Shift_End_HEAD.SHIFT,            (TSPL_Primary_Vehicle_Master.Vehicle_Code) As Vehicle_Code,            TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE,            Max(TSPL_Primary_Vehicle_Master.Description) As [Vehicle Name]          From TSPL_MILK_Shift_End_Route_DETAIL            Left Outer Join TSPL_MILK_Shift_End_HEAD              On TSPL_MILK_Shift_End_HEAD.DOC_CODE =              TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE            Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =              TSPL_MILK_Shift_End_HEAD.MCC_CODE            Left Outer Join TSPL_Primary_Vehicle_Master              On TSPL_Primary_Vehicle_Master.Vehicle_Code =              TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE          Group By TSPL_MILK_Shift_End_HEAD.SHIFT,            TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE,            TSPL_Primary_Vehicle_Master.Vehicle_Code,            TSPL_MILK_Shift_End_HEAD.DOC_DATE) xx          Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL            On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = xx.DOC_CODE            And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = xx.Route_CODE          Left Outer Join TSPL_MCC_ROUTE_MASTER            On TSPL_MCC_ROUTE_MASTER.Route_Code = xx.Route_CODE          Left Outer Join TSPL_Primary_Vehicle_Master            On TSPL_Primary_Vehicle_Master.Vehicle_Code =            TSPL_MCC_ROUTE_MASTER.Vehicle_Code) final) xxx"
        sQuery += " where 2 = 2  and  DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
        End If

        If cbgMCC.CheckedValue.Count > 0 Then 'RbMCCSelect.IsChecked And
            sQuery += " and  [MCC Code]  in (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ")"
        End If
        If RbRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
            sQuery += " and [Route No.] in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        End If
        sQuery += " ) xxxx) xxxxx Group By xxxxx.RangeName"

        obj.Chart_Type = "Pie"
        obj.Chart_Value_Member = "Value"
        obj.Chart_Series_Member = "RangeName"
        obj.Chart_Category_Member = "RangeName"

        Load_Chart(sQuery)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
