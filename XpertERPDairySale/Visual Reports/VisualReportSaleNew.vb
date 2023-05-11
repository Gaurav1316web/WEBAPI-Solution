Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports Telerik.Charting
Imports System.IO
Public Class VisualReportSaleNew
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim dtZoneWise As DataTable = Nothing
    Dim dtCustomerGrp As DataTable = Nothing
    Dim dtItemGroup As DataTable = Nothing
    Dim dtItemType As DataTable = Nothing

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
        txtFromDate.Value = clsCommon.GETSERVERDATE()

        txtFromDate.CustomFormat = "MM-yyyy"
        txtToDate.CustomFormat = "MM-yyyy"

        LoadFigure()

        cvZone.Views.AddNew()
        Dim controllerZone As New DrillDownController()
        cvZone.Controllers.Add(controllerZone)
        cvZone.ShowDrillNavigation = False
        cvZonePie.ShowDrillNavigation = False

        cvItemGroup.Views.AddNew()
        Dim controllerItemGroup As New DrillDownController()
        cvItemGroup.Controllers.Add(controllerItemGroup)
        cvItemGroup.ShowDrillNavigation = False
        cvItemGroupPie.ShowDrillNavigation = False

        cvCustomerGroup.Views.AddNew()
        Dim controllerCustomerGroup As New DrillDownController()
        cvCustomerGroup.Controllers.Add(controllerCustomerGroup)
        cvCustomerGroup.ShowDrillNavigation = False
        cvCustomerGroupPie.ShowDrillNavigation = False

        cvItemType.Views.AddNew()
        Dim controllerItemType As New DrillDownController()
        cvItemType.Controllers.Add(controllerItemType)
        cvItemType.ShowDrillNavigation = False
        cvItemTypePie.ShowDrillNavigation = False

        Reset()
        MyCheckBox1.Checked = True
    End Sub

    Sub LoadFigure()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "AMT"
        dr("Name") = "Amount (Lakhs)"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Kg"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Ltr"
        dr("Name") = "LTR"
        dt.Rows.Add(dr)


        cboFigureIn.DataSource = dt
        cboFigureIn.ValueMember = "Code"
        cboFigureIn.DisplayMember = "Name"

        cboFigureIn.SelectedValue = "AMT"
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        dtZoneWise = Nothing
        gvZone.DataSource = Nothing
        gvZone.Rows.Clear()
        gvZone.Columns.Clear()
        scZone.Panel1Collapsed = False
        scZone.Panel2Collapsed = True
        cvZone.Series.Clear()


        dtItemGroup = Nothing
        gvItemGroup.DataSource = Nothing
        gvItemGroup.Rows.Clear()
        gvItemGroup.Columns.Clear()
        scItemGroup.Panel1Collapsed = False
        scItemGroup.Panel2Collapsed = True
        cvItemGroup.Series.Clear()


        dtCustomerGrp = Nothing
        gvCustomerGroup.DataSource = Nothing
        gvCustomerGroup.Rows.Clear()
        gvCustomerGroup.Columns.Clear()
        scCustomerGroup.Panel1Collapsed = False
        scCustomerGroup.Panel2Collapsed = True
        cvCustomerGroup.Series.Clear()

        dtItemType = Nothing
        gvItemType.DataSource = Nothing
        gvItemType.Rows.Clear()
        gvItemType.Columns.Clear()
        scItemType.Panel1Collapsed = False
        scItemType.Panel2Collapsed = True
        cvItemType.Series.Clear()

        EnableDisableCntrl(True)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 4
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Zone Wise Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            If chkZoneWise.Checked Then
                Load_Report_Zone()
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Item Group Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            If chkItemGrpWise.Checked Then
                Load_Report_ItemGroup()
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Cusotmer Group Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            If chkCustomerGrpWise.Checked Then
                Load_Report_CustomerGroup()
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Item Type Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            If chkItemTypeWise.Checked Then
                Load_Report_ItemType()
            End If
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
        cboFigureIn.Enabled = val
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

    Private Function GetBaseQuery() As String
        Dim dtFrom As Date = txtFromDate.Value
        Dim dtTo As Date = txtToDate.Value

        dtFrom = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        dtTo = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1).AddMonths(1).AddDays(-1)


        Dim obj As New clsSaleRegisterParameterType
        obj.Unit_Code = "Ltr"
        If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
            obj.Unit_Code = "Kg"
        End If
        obj.From_Date = clsCommon.myCDate(dtFrom)
        obj.To_Date = clsCommon.myCDate(dtTo)
        obj.Other_Cond = " and xx.Status=1  "
        Dim qry1 As String
        qry1 = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
        Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry1)
        Dim arrTrans As New ArrayList
        For Each dr As DataRow In dtTrans.Rows
            arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
        Next
        obj.Trans_Type_List = arrTrans

        Dim qry As String = clsPSInvoiceHead.GetReportDataQuery(obj)
        qry = "Select (SUBSTRING([Document_date],7,4)+'-'+SUBSTRING([Document_date],4,2)) as GrpMonth,
case when len(isnull([Customer Zone Code],''))>0 then [Customer Zone Code] else 'Not Defined' end as [Customer Zone Code],
case when len(isnull([Customer Zone Code],''))>0 then [Customer Zone Description] else 'Not Defined' end as [Customer Zone Description],
case when len(isnull([Customer Group Code],''))>0 then [Customer Group Code] else 'Not Defined' end as [Customer Group Code],
case when len(isnull([Customer Group Code],''))>0 then [Customer Group Description] else 'Not Defined' end as [Customer Group Description],
case when len(isnull(TSPL_ITEM_MASTER.Cheapter_Heads,''))>0 then TSPL_ITEM_MASTER.Cheapter_Heads else 'Not Defined' end as Cheapter_Heads,
case when len(isnull(TSPL_ITEM_MASTER.Cheapter_Heads,''))>0 then TSPL_CHAPTER_HEAD.Description else 'Not Defined' end as Cheapter_Heads_Name,
Case When isnull(TSPL_ITEM_MASTER.Is_Ambient,0) =1 Then 'Ambient' else 'Fresh' end as ItemType,[Sale Amount],Quantity from (   " + clsPSInvoiceHead.GetReportDataQuery(obj) +
        ")xxxx left join tspl_item_master on tspl_item_master.Item_Code=xxxx.[Item Code]  left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads  where (isnull(TSPL_ITEM_MASTER.Is_Ambient,0) =1 or isnull(TSPL_ITEM_MASTER.Is_FreshItem,0) =1)"
        Return qry
    End Function

    Public Sub Load_Report_Zone()
        Try
            If dtZoneWise Is Nothing OrElse dtZoneWise.Rows.Count <= 0 Then
                Dim sQuery As String = "Select GrpMonth,[Customer Zone Code] as GrpCode,max([Customer Zone Description]) as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,[Customer Zone Code] order by GrpMonth"
                'sQuery = "select * from tempZone order by GrpMonth"
                dtZoneWise = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtZoneWise IsNot Nothing AndAlso dtZoneWise.Rows.Count > 0 Then
                cvZone.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvZone.AreaDesign = CartesianArea1
                cvZone.Series.Clear()
                Dim strValue As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
                    strValue = "Sale Amount"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                End If

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtZoneWise.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpName"))

                        'dtNew.Columns.Add("Code", GetType(String))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpName"))).NewRow()
                    'drTS("Code") = dtZoneWise.Rows(ii)("GrpCode")
                    drTS("Name") = dtZoneWise.Rows(ii)("GrpMonth")
                    drTS("Value") = dtZoneWise.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvZone.ShowTitle = True
                cvZone.ChartElement.TitlePosition = TitlePosition.Top
                cvZone.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

                cvZone.Title = "Zone wise Sale"
                Dim smartLabelsController As New SmartLabelsController()
                cvZone.Controllers.Add(smartLabelsController)
                cvZone.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvZone.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvZone.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvZone.Series.Add(barSeries)
                Next
                cvZone.ShowLegend = True
                setOrientation(cvZone, "Vertical", 0)
                smartLabelsController.Strategy = strategy



                gvZone.DataSource = Nothing
                gvZone.Columns.Clear()
                gvZone.Rows.Clear()
                gvZone.GroupDescriptors.Clear()
                gvZone.MasterTemplate.SummaryRowsBottom.Clear()
                gvZone.ShowGroupPanel = False
                gvZone.EnableFiltering = False
                gvZone.AllowAddNewRow = False

                gvZone.GroupDescriptors.Clear()
                gvZone.TableElement.TableHeaderHeight = 40
                gvZone.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvZone.MasterTemplate.Columns.Add(repoComplete)
                'dtMatrix.Columns.Add("GrpCode", GetType(String))
                'dtMatrix.Columns.Add("GrpName", GetType(String))
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)
                For ii As Integer = 0 To dtZoneWise.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode"))) Then
                        Dim repoRate As New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode"))
                        repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvZone.MasterTemplate.Columns.Add(repoRate)


                        arrColumn.Add(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode")), gvZone.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpMonth"))) Then
                        gvZone.Rows.AddNew()
                        gvZone.Rows(gvZone.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpMonth")), gvZone.Rows.Count - 1)
                    End If
                    gvZone.Rows(arrRow.Item(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtZoneWise.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtZoneWise.Rows(ii)(strValue))
                Next
                For ii As Integer = 0 To gvZone.Columns.Count - 1
                    gvZone.Columns(ii).ReadOnly = True
                    gvZone.Columns(ii).IsVisible = True
                Next
                gvZone.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub cvZone_Drill(sender As Object, e As UI.DrillEventArgs) Handles cvZone.Drill
        Try
            Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            e.Cancel = True
            cvZonePie.Series.Clear()

            scZone.Panel1Collapsed = True
            scZone.Panel2Collapsed = False

            Dim dt As New DataTable
            dt.Columns.Add("Name", GetType(String))
            dt.Columns.Add("Value", GetType(Decimal))
            For ii As Integer = 0 To gvZone.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvZone.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
                    For jj As Integer = 1 To gvZone.Columns.Count - 1
                        Dim drTS As DataRow = dt.NewRow()
                        drTS("Name") = gvZone.Columns(jj).HeaderText
                        drTS("Value") = clsCommon.myCdbl(gvZone.Rows(ii).Cells(jj).Value)
                        dt.Rows.Add(drTS)
                    Next
                    Exit For
                End If
            Next
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cvZonePie.ShowTitle = True
                cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
                cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
                cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvZonePie.AreaType = ChartAreaType.Pie
                cvZonePie.ShowLegend = True
                cvZonePie.View.Margin = New Padding(0, 15, 0, 15)
                Me.cvZonePie.AreaType = ChartAreaType.Pie
                Dim series As New PieSeries()
                For Each dr As DataRow In dt.Rows
                    series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
                Next
                series.ShowLabels = True
                Me.cvZonePie.Series.Add(series)

                'Dim strategy As New PieTwoLabelColumnsStrategy()
                'cvZonePie.ShowTitle = True
                'cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
                'cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
                'cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                'cvZonePie.AreaType = ChartAreaType.Pie
                'cvZonePie.ShowLegend = True
                'cvZonePie.View.Margin = New Padding(60, 0, 50, 0)
                'Dim series As New PieSeries()
                'series.Range = New AngleRange(270, 360)
                'series.LabelFormat = "{0:P2}"
                'series.RadiusFactor = 0.9F
                'series.ValueMember = "Value"
                'series.DataSource = dt
                'series.ShowLabels = True
                'series.DrawLinesToLabels = True
                'series.SyncLinesToLabelsColor = True
                'series.DisplayMember = "Name"
                'cvZonePie.Series.Add(series)

                'For Each item As LegendItem In Me.cvZonePie.ChartElement.LegendElement.Provider.LegendInfos
                '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                '    item.Title = clsCommon.myCstr(row("Name"))
                'Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Public Sub Load_Report_ItemGroupOLD()
    '    Try
    '        If dtItemGroup Is Nothing OrElse dtItemGroup.Rows.Count <= 0 Then
    '            Dim sQuery As String = "Select GrpMonth,[Cheapter_Heads] as GrpCode,max([Cheapter_Heads_Name]) as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,[Cheapter_Heads] order by GrpMonth"
    '            sQuery = "select * from tempItemGrp order by GrpMonth"
    '            dtItemGroup = clsDBFuncationality.GetDataTable(sQuery)
    '        End If

    '        If dtItemGroup IsNot Nothing AndAlso dtItemGroup.Rows.Count > 0 Then
    '            cvItemGroupWise.Area.View.Palette = New CustomPalette()
    '            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
    '            cvItemGroupWise.AreaDesign = CartesianArea1
    '            cvItemGroupWise.Series.Clear()
    '            Dim strValue As String = ""
    '            If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
    '                strValue = "Sale Amount"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            End If

    '            Dim ds As DataSet = New DataSet


    '            Dim arrLegend As New List(Of String)
    '            For ii As Integer = 0 To dtItemGroup.Rows.Count - 1
    '                If Not arrLegend.Contains(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))) Then
    '                    Dim dtNew As New DataTable
    '                    dtNew.TableName = clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))
    '                    dtNew.Columns.Add("Code", GetType(String))
    '                    dtNew.Columns.Add("Name", GetType(String))
    '                    dtNew.Columns.Add("Value", GetType(Decimal))
    '                    arrLegend.Add(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth")))
    '                    ds.Tables.Add(dtNew)
    '                End If

    '                Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))).NewRow()
    '                drTS("Code") = dtItemGroup.Rows(ii)("GrpCode")
    '                drTS("Name") = dtItemGroup.Rows(ii)("GrpName")
    '                drTS("Value") = dtItemGroup.Rows(ii)(strValue)
    '                ds.Tables(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))).Rows.Add(drTS)
    '            Next


    '            cvItemGroupWise.ShowTitle = True
    '            cvItemGroupWise.ChartElement.TitlePosition = TitlePosition.Top
    '            cvItemGroupWise.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

    '            cvItemGroupWise.Title = "Item Group Wise Sale"
    '            Dim smartLabelsController As New SmartLabelsController()
    '            cvItemGroupWise.Controllers.Add(smartLabelsController)
    '            cvItemGroupWise.ShowSmartLabels = True
    '            Dim strategy As SmartLabelsStrategyBase = Nothing
    '            cvItemGroupWise.AreaType = ChartAreaType.Cartesian
    '            strategy = New VerticalAdjusmentLabelsStrategy()

    '            cvItemGroupWise.DataSource = ds

    '            For ii As Integer = 0 To ds.Tables.Count - 1
    '                Dim barSeries As New BarSeries("Value", "Name")
    '                barSeries.DataMember = ds.Tables(ii).TableName
    '                barSeries.LegendTitle = ds.Tables(ii).TableName
    '                cvItemGroupWise.Series.Add(barSeries)
    '            Next
    '            cvItemGroupWise.ShowLegend = True
    '            setOrientation(cvItemGroupWise, "Vertical", 0)
    '            smartLabelsController.Strategy = strategy
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Public Sub Load_Report_ItemGroup()
        Try
            If dtItemGroup Is Nothing OrElse dtItemGroup.Rows.Count <= 0 Then
                Dim sQuery As String = "Select GrpMonth,[Cheapter_Heads] as GrpCode,max([Cheapter_Heads_Name]) as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,[Cheapter_Heads] order by GrpMonth"
                'sQuery = "select * from tempItemGrp where len(isnull( GrpCode,''))>0  order by GrpMonth"
                dtItemGroup = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtItemGroup IsNot Nothing AndAlso dtItemGroup.Rows.Count > 0 Then
                cvItemGroup.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvItemGroup.AreaDesign = CartesianArea1
                cvItemGroup.Series.Clear()
                Dim strValue As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
                    strValue = "Sale Amount"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                End If

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtItemGroup.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpName"))

                        'dtNew.Columns.Add("Code", GetType(String))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpName"))).NewRow()
                    'drTS("Code") = dtItemGroup.Rows(ii)("GrpCode")
                    drTS("Name") = dtItemGroup.Rows(ii)("GrpMonth")
                    drTS("Value") = dtItemGroup.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvItemGroup.ShowTitle = True
                cvItemGroup.ChartElement.TitlePosition = TitlePosition.Top
                cvItemGroup.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

                cvItemGroup.Title = "Item Group Wise Sale"
                Dim smartLabelsController As New SmartLabelsController()
                cvItemGroup.Controllers.Add(smartLabelsController)
                cvItemGroup.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvItemGroup.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvItemGroup.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvItemGroup.Series.Add(barSeries)
                Next
                cvItemGroup.ShowLegend = True
                setOrientation(cvItemGroup, "Vertical", 0)
                smartLabelsController.Strategy = strategy



                gvItemGroup.DataSource = Nothing
                gvItemGroup.Columns.Clear()
                gvItemGroup.Rows.Clear()
                gvItemGroup.GroupDescriptors.Clear()
                gvItemGroup.MasterTemplate.SummaryRowsBottom.Clear()
                gvItemGroup.ShowGroupPanel = False
                gvItemGroup.EnableFiltering = False
                gvItemGroup.AllowAddNewRow = False

                gvItemGroup.GroupDescriptors.Clear()
                gvItemGroup.TableElement.TableHeaderHeight = 40
                gvItemGroup.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvItemGroup.MasterTemplate.Columns.Add(repoComplete)
                'dtMatrix.Columns.Add("GrpCode", GetType(String))
                'dtMatrix.Columns.Add("GrpName", GetType(String))
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)
                For ii As Integer = 0 To dtItemGroup.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode"))) Then
                        Dim repoRate As New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode"))
                        repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvItemGroup.MasterTemplate.Columns.Add(repoRate)


                        arrColumn.Add(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode")), gvItemGroup.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))) Then
                        gvItemGroup.Rows.AddNew()
                        gvItemGroup.Rows(gvItemGroup.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth")), gvItemGroup.Rows.Count - 1)
                    End If
                    gvItemGroup.Rows(arrRow.Item(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtItemGroup.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtItemGroup.Rows(ii)(strValue))
                Next
                For ii As Integer = 0 To gvItemGroup.Columns.Count - 1
                    gvItemGroup.Columns(ii).ReadOnly = True
                    gvItemGroup.Columns(ii).IsVisible = True
                Next
                gvItemGroup.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub cvItemGroup_Drill(sender As Object, e As UI.DrillEventArgs) Handles cvItemGroup.Drill
        Try
            Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            e.Cancel = True
            cvItemGroupPie.Series.Clear()

            scItemGroup.Panel1Collapsed = True
            scItemGroup.Panel2Collapsed = False

            Dim dt As New DataTable
            dt.Columns.Add("Name", GetType(String))
            dt.Columns.Add("Value", GetType(Decimal))
            For ii As Integer = 0 To gvItemGroup.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvItemGroup.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
                    For jj As Integer = 1 To gvItemGroup.Columns.Count - 1
                        Dim drTS As DataRow = dt.NewRow()
                        drTS("Name") = gvItemGroup.Columns(jj).HeaderText
                        drTS("Value") = clsCommon.myCdbl(gvItemGroup.Rows(ii).Cells(jj).Value)
                        dt.Rows.Add(drTS)
                    Next
                    Exit For
                End If
            Next
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cvItemGroupPie.ShowTitle = True
                cvItemGroupPie.Title = "Item Group Wise Sale For [" + strSelectedValue + "]"
                cvItemGroupPie.ChartElement.TitlePosition = TitlePosition.Top
                cvItemGroupPie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvItemGroupPie.AreaType = ChartAreaType.Pie
                cvItemGroupPie.ShowLegend = True
                cvItemGroupPie.View.Margin = New Padding(0, 15, 0, 15)
                Me.cvItemGroupPie.AreaType = ChartAreaType.Pie
                Dim series As New PieSeries()
                For Each dr As DataRow In dt.Rows
                    series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
                Next
                series.ShowLabels = True
                Me.cvItemGroupPie.Series.Add(series)

                'Dim strategy As New PieTwoLabelColumnsStrategy()
                'cvItemGroupPie.ShowTitle = True
                'cvItemGroupPie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
                'cvItemGroupPie.ChartElement.TitlePosition = TitlePosition.Top
                'cvItemGroupPie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                'cvItemGroupPie.AreaType = ChartAreaType.Pie
                'cvItemGroupPie.ShowLegend = True
                'cvItemGroupPie.View.Margin = New Padding(60, 0, 50, 0)
                'Dim series As New PieSeries()
                'series.Range = New AngleRange(270, 360)
                'series.LabelFormat = "{0:P2}"
                'series.RadiusFactor = 0.9F
                'series.ValueMember = "Value"
                'series.DataSource = dt
                'series.ShowLabels = True
                'series.DrawLinesToLabels = True
                'series.SyncLinesToLabelsColor = True
                'series.DisplayMember = "Name"
                'cvItemGroupPie.Series.Add(series)

                'For Each item As LegendItem In Me.cvItemGroupPie.ChartElement.LegendElement.Provider.LegendInfos
                '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                '    item.Title = clsCommon.myCstr(row("Name"))
                'Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Public Sub Load_Report_CustomerGroupOLD()
    '    Try
    '        If dtCustomerGrp Is Nothing OrElse dtCustomerGrp.Rows.Count <= 0 Then
    '            Dim sQuery As String = "Select GrpMonth,[Customer Group Code] as GrpCode,max([Customer Group Description]) as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,[Customer Group Code] order by GrpMonth"
    '            sQuery = "select * from tempCustomerGrp order by GrpMonth"
    '            dtCustomerGrp = clsDBFuncationality.GetDataTable(sQuery)
    '        End If

    '        If dtCustomerGrp IsNot Nothing AndAlso dtCustomerGrp.Rows.Count > 0 Then
    '            cvCustomerGroup.Area.View.Palette = New CustomPalette()
    '            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
    '            cvCustomerGroup.AreaDesign = CartesianArea1
    '            cvCustomerGroup.Series.Clear()
    '            Dim strValue As String = ""
    '            If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
    '                strValue = "Sale Amount"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            End If

    '            Dim ds As DataSet = New DataSet


    '            Dim arrLegend As New List(Of String)
    '            For ii As Integer = 0 To dtCustomerGrp.Rows.Count - 1
    '                If Not arrLegend.Contains(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))) Then
    '                    Dim dtNew As New DataTable
    '                    dtNew.TableName = clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))
    '                    dtNew.Columns.Add("Code", GetType(String))
    '                    dtNew.Columns.Add("Name", GetType(String))
    '                    dtNew.Columns.Add("Value", GetType(Decimal))
    '                    arrLegend.Add(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth")))
    '                    ds.Tables.Add(dtNew)
    '                End If

    '                Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))).NewRow()
    '                drTS("Code") = dtCustomerGrp.Rows(ii)("GrpCode")
    '                drTS("Name") = dtCustomerGrp.Rows(ii)("GrpName")
    '                drTS("Value") = dtCustomerGrp.Rows(ii)(strValue)
    '                ds.Tables(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))).Rows.Add(drTS)
    '            Next


    '            cvCustomerGroup.ShowTitle = True
    '            cvCustomerGroup.ChartElement.TitlePosition = TitlePosition.Top
    '            cvCustomerGroup.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

    '            cvCustomerGroup.Title = "Customer Group Wise Sale"
    '            Dim smartLabelsController As New SmartLabelsController()
    '            cvCustomerGroup.Controllers.Add(smartLabelsController)
    '            cvCustomerGroup.ShowSmartLabels = True
    '            Dim strategy As SmartLabelsStrategyBase = Nothing
    '            cvCustomerGroup.AreaType = ChartAreaType.Cartesian
    '            strategy = New VerticalAdjusmentLabelsStrategy()

    '            cvCustomerGroup.DataSource = ds

    '            For ii As Integer = 0 To ds.Tables.Count - 1
    '                Dim barSeries As New BarSeries("Value", "Name")
    '                barSeries.DataMember = ds.Tables(ii).TableName
    '                barSeries.LegendTitle = ds.Tables(ii).TableName
    '                cvCustomerGroup.Series.Add(barSeries)
    '            Next
    '            cvCustomerGroup.ShowLegend = True
    '            setOrientation(cvCustomerGroup, "Vertical", 0)
    '            smartLabelsController.Strategy = strategy
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Public Sub Load_Report_CustomerGroup()
        Try
            If dtCustomerGrp Is Nothing OrElse dtCustomerGrp.Rows.Count <= 0 Then
                Dim sQuery As String = "Select GrpMonth,[Customer Group Code] as GrpCode,max([Customer Group Description]) as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,[Customer Group Code] order by GrpMonth"
                'sQuery = "select * from tempCustomerGrp where len(isnull( GrpCode,''))>0 order by GrpMonth"
                dtCustomerGrp = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtCustomerGrp IsNot Nothing AndAlso dtCustomerGrp.Rows.Count > 0 Then
                cvCustomerGroup.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvCustomerGroup.AreaDesign = CartesianArea1
                cvCustomerGroup.Series.Clear()
                Dim strValue As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
                    strValue = "Sale Amount"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                End If

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtCustomerGrp.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpName"))

                        'dtNew.Columns.Add("Code", GetType(String))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpName"))).NewRow()
                    'drTS("Code") = dtCustomerGrp.Rows(ii)("GrpCode")
                    drTS("Name") = dtCustomerGrp.Rows(ii)("GrpMonth")
                    drTS("Value") = dtCustomerGrp.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvCustomerGroup.ShowTitle = True
                cvCustomerGroup.ChartElement.TitlePosition = TitlePosition.Top
                cvCustomerGroup.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

                cvCustomerGroup.Title = "Customer Group Wise Sale"
                Dim smartLabelsController As New SmartLabelsController()
                cvCustomerGroup.Controllers.Add(smartLabelsController)
                cvCustomerGroup.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvCustomerGroup.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvCustomerGroup.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvCustomerGroup.Series.Add(barSeries)
                Next
                cvCustomerGroup.ShowLegend = True
                setOrientation(cvCustomerGroup, "Vertical", 0)
                smartLabelsController.Strategy = strategy



                gvCustomerGroup.DataSource = Nothing
                gvCustomerGroup.Columns.Clear()
                gvCustomerGroup.Rows.Clear()
                gvCustomerGroup.GroupDescriptors.Clear()
                gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Clear()
                gvCustomerGroup.ShowGroupPanel = False
                gvCustomerGroup.EnableFiltering = False
                gvCustomerGroup.AllowAddNewRow = False

                gvCustomerGroup.GroupDescriptors.Clear()
                gvCustomerGroup.TableElement.TableHeaderHeight = 40
                gvCustomerGroup.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvCustomerGroup.MasterTemplate.Columns.Add(repoComplete)
                'dtMatrix.Columns.Add("GrpCode", GetType(String))
                'dtMatrix.Columns.Add("GrpName", GetType(String))
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)
                For ii As Integer = 0 To dtCustomerGrp.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode"))) Then
                        Dim repoRate As New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode"))
                        repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvCustomerGroup.MasterTemplate.Columns.Add(repoRate)


                        arrColumn.Add(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode")), gvCustomerGroup.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))) Then
                        gvCustomerGroup.Rows.AddNew()
                        gvCustomerGroup.Rows(gvCustomerGroup.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth")), gvCustomerGroup.Rows.Count - 1)
                    End If
                    gvCustomerGroup.Rows(arrRow.Item(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtCustomerGrp.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtCustomerGrp.Rows(ii)(strValue))
                Next
                For ii As Integer = 0 To gvCustomerGroup.Columns.Count - 1
                    gvCustomerGroup.Columns(ii).ReadOnly = True
                    gvCustomerGroup.Columns(ii).IsVisible = True
                Next
                gvCustomerGroup.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub cvCustomerGroup_Drill(sender As Object, e As UI.DrillEventArgs) Handles cvCustomerGroup.Drill
        Try
            Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            e.Cancel = True
            cvCustomerGroupPie.Series.Clear()

            scCustomerGroup.Panel1Collapsed = True
            scCustomerGroup.Panel2Collapsed = False

            Dim dt As New DataTable
            dt.Columns.Add("Name", GetType(String))
            dt.Columns.Add("Value", GetType(Decimal))
            For ii As Integer = 0 To gvCustomerGroup.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvCustomerGroup.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
                    For jj As Integer = 1 To gvCustomerGroup.Columns.Count - 1
                        Dim drTS As DataRow = dt.NewRow()
                        drTS("Name") = gvCustomerGroup.Columns(jj).HeaderText
                        drTS("Value") = clsCommon.myCdbl(gvCustomerGroup.Rows(ii).Cells(jj).Value)
                        dt.Rows.Add(drTS)
                    Next
                    Exit For
                End If
            Next
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cvCustomerGroupPie.ShowTitle = True
                cvCustomerGroupPie.Title = "Customer Group Wise Sale For [" + strSelectedValue + "]"
                cvCustomerGroupPie.ChartElement.TitlePosition = TitlePosition.Top
                cvCustomerGroupPie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvCustomerGroupPie.AreaType = ChartAreaType.Pie
                cvCustomerGroupPie.ShowLegend = True
                cvCustomerGroupPie.View.Margin = New Padding(0, 15, 0, 15)
                Me.cvCustomerGroupPie.AreaType = ChartAreaType.Pie
                Dim series As New PieSeries()
                For Each dr As DataRow In dt.Rows
                    series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
                Next
                series.ShowLabels = True
                Me.cvCustomerGroupPie.Series.Add(series)

                'Dim strategy As New PieTwoLabelColumnsStrategy()
                'cvCustomerGroupPie.ShowTitle = True
                'cvCustomerGroupPie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
                'cvCustomerGroupPie.ChartElement.TitlePosition = TitlePosition.Top
                'cvCustomerGroupPie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                'cvCustomerGroupPie.AreaType = ChartAreaType.Pie
                'cvCustomerGroupPie.ShowLegend = True
                'cvCustomerGroupPie.View.Margin = New Padding(60, 0, 50, 0)
                'Dim series As New PieSeries()
                'series.Range = New AngleRange(270, 360)
                'series.LabelFormat = "{0:P2}"
                'series.RadiusFactor = 0.9F
                'series.ValueMember = "Value"
                'series.DataSource = dt
                'series.ShowLabels = True
                'series.DrawLinesToLabels = True
                'series.SyncLinesToLabelsColor = True
                'series.DisplayMember = "Name"
                'cvCustomerGroupPie.Series.Add(series)

                'For Each item As LegendItem In Me.cvCustomerGroupPie.ChartElement.LegendElement.Provider.LegendInfos
                '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                '    item.Title = clsCommon.myCstr(row("Name"))
                'Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Public Sub Load_Report_ItemTypeOLD()
    '    Try
    '        If dtItemType Is Nothing OrElse dtItemType.Rows.Count <= 0 Then
    '            Dim sQuery As String = "Select GrpMonth,ItemType as GrpCode,ItemType as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,ItemType order by GrpMonth"
    '            sQuery = "select * from tempItemGrp order by GrpMonth"
    '            dtItemType = clsDBFuncationality.GetDataTable(sQuery)
    '        End If
    '        If dtItemType IsNot Nothing AndAlso dtItemType.Rows.Count > 0 Then
    '            cvItemType.Area.View.Palette = New CustomPalette()
    '            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
    '            cvItemType.AreaDesign = CartesianArea1
    '            cvItemType.Series.Clear()
    '            Dim strValue As String = ""
    '            If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
    '                strValue = "Sale Amount"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
    '                strValue = "Quantity"
    '            End If

    '            Dim ds As DataSet = New DataSet


    '            Dim arrLegend As New List(Of String)
    '            For ii As Integer = 0 To dtItemType.Rows.Count - 1
    '                If Not arrLegend.Contains(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))) Then
    '                    Dim dtNew As New DataTable
    '                    dtNew.TableName = clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))
    '                    dtNew.Columns.Add("Code", GetType(String))
    '                    dtNew.Columns.Add("Name", GetType(String))
    '                    dtNew.Columns.Add("Value", GetType(Decimal))
    '                    arrLegend.Add(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth")))
    '                    ds.Tables.Add(dtNew)
    '                End If

    '                Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))).NewRow()
    '                drTS("Code") = dtItemType.Rows(ii)("GrpCode")
    '                drTS("Name") = dtItemType.Rows(ii)("GrpName")
    '                drTS("Value") = dtItemType.Rows(ii)(strValue)
    '                ds.Tables(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))).Rows.Add(drTS)
    '            Next


    '            cvItemType.ShowTitle = True
    '            cvItemType.ChartElement.TitlePosition = TitlePosition.Top
    '            cvItemType.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

    '            cvItemType.Title = "Item Type Wise Sale"
    '            Dim smartLabelsController As New SmartLabelsController()
    '            cvItemType.Controllers.Add(smartLabelsController)
    '            cvItemType.ShowSmartLabels = True
    '            Dim strategy As SmartLabelsStrategyBase = Nothing
    '            cvItemType.AreaType = ChartAreaType.Cartesian
    '            strategy = New VerticalAdjusmentLabelsStrategy()

    '            cvItemType.DataSource = ds

    '            For ii As Integer = 0 To ds.Tables.Count - 1
    '                Dim barSeries As New BarSeries("Value", "Name")
    '                barSeries.DataMember = ds.Tables(ii).TableName
    '                barSeries.LegendTitle = ds.Tables(ii).TableName
    '                cvItemType.Series.Add(barSeries)
    '            Next
    '            cvItemType.ShowLegend = True
    '            setOrientation(cvItemType, "Vertical", 0)
    '            smartLabelsController.Strategy = strategy
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Public Sub Load_Report_ItemType()
        Try
            If dtItemType Is Nothing OrElse dtItemType.Rows.Count <= 0 Then
                Dim sQuery As String = "Select GrpMonth,ItemType as GrpCode,ItemType as GrpName,Convert(Decimal(18,2),sum([Sale Amount])/100000) As [Sale Amount],Sum(Quantity) As Quantity from (" + GetBaseQuery() + ")xxxxx Group by GrpMonth,ItemType order by GrpMonth"
                'sQuery = "select * from tempItemType order by GrpMonth"
                dtItemType = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtItemType IsNot Nothing AndAlso dtItemType.Rows.Count > 0 Then
                cvItemType.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvItemType.AreaDesign = CartesianArea1
                cvItemType.Series.Clear()
                Dim strValue As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "Amt") = CompairStringResult.Equal Then
                    strValue = "Sale Amount"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "KG") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFigureIn.SelectedValue), "LTR") = CompairStringResult.Equal Then
                    strValue = "Quantity"
                End If

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtItemType.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtItemType.Rows(ii)("GrpName"))

                        'dtNew.Columns.Add("Code", GetType(String))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtItemType.Rows(ii)("GrpName"))).NewRow()
                    'drTS("Code") = dtItemType.Rows(ii)("GrpCode")
                    drTS("Name") = dtItemType.Rows(ii)("GrpMonth")
                    drTS("Value") = dtItemType.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtItemType.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvItemType.ShowTitle = True
                cvItemType.ChartElement.TitlePosition = TitlePosition.Top
                cvItemType.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter

                cvItemType.Title = "Item Type Wise Sale"
                Dim smartLabelsController As New SmartLabelsController()
                cvItemType.Controllers.Add(smartLabelsController)
                cvItemType.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvItemType.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvItemType.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvItemType.Series.Add(barSeries)
                Next
                cvItemType.ShowLegend = True
                setOrientation(cvItemType, "Vertical", 0)
                smartLabelsController.Strategy = strategy



                gvItemType.DataSource = Nothing
                gvItemType.Columns.Clear()
                gvItemType.Rows.Clear()
                gvItemType.GroupDescriptors.Clear()
                gvItemType.MasterTemplate.SummaryRowsBottom.Clear()
                gvItemType.ShowGroupPanel = False
                gvItemType.EnableFiltering = False
                gvItemType.AllowAddNewRow = False

                gvItemType.GroupDescriptors.Clear()
                gvItemType.TableElement.TableHeaderHeight = 40
                gvItemType.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvItemType.MasterTemplate.Columns.Add(repoComplete)
                'dtMatrix.Columns.Add("GrpCode", GetType(String))
                'dtMatrix.Columns.Add("GrpName", GetType(String))
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)
                For ii As Integer = 0 To dtItemType.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode"))) Then
                        Dim repoRate As New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtItemType.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode"))
                        repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvItemType.MasterTemplate.Columns.Add(repoRate)


                        arrColumn.Add(clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode")), gvItemType.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))) Then
                        gvItemType.Rows.AddNew()
                        gvItemType.Rows(gvItemType.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth")), gvItemType.Rows.Count - 1)
                    End If
                    gvItemType.Rows(arrRow.Item(clsCommon.myCstr(dtItemType.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtItemType.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtItemType.Rows(ii)(strValue))
                Next
                For ii As Integer = 0 To gvItemType.Columns.Count - 1
                    gvItemType.Columns(ii).ReadOnly = True
                    gvItemType.Columns(ii).IsVisible = True
                Next
                gvItemType.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub cvItemType_Drill(sender As Object, e As UI.DrillEventArgs) Handles cvItemType.Drill
        Try
            Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            e.Cancel = True
            cvItemTypePie.Series.Clear()

            scItemType.Panel1Collapsed = True
            scItemType.Panel2Collapsed = False

            Dim dt As New DataTable
            dt.Columns.Add("Name", GetType(String))
            dt.Columns.Add("Value", GetType(Decimal))
            For ii As Integer = 0 To gvItemType.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvItemType.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
                    For jj As Integer = 1 To gvItemType.Columns.Count - 1
                        Dim drTS As DataRow = dt.NewRow()
                        drTS("Name") = gvItemType.Columns(jj).HeaderText
                        drTS("Value") = clsCommon.myCdbl(gvItemType.Rows(ii).Cells(jj).Value)
                        dt.Rows.Add(drTS)
                    Next
                    Exit For
                End If
            Next
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cvItemTypePie.ShowTitle = True
                cvItemTypePie.Title = "Item Type Wise Sale For [" + strSelectedValue + "]"
                cvItemTypePie.ChartElement.TitlePosition = TitlePosition.Top
                cvItemTypePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvItemTypePie.AreaType = ChartAreaType.Pie
                cvItemTypePie.ShowLegend = True
                cvItemTypePie.View.Margin = New Padding(0, 15, 0, 15)
                Me.cvItemTypePie.AreaType = ChartAreaType.Pie
                Dim series As New PieSeries()
                For Each dr As DataRow In dt.Rows
                    series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
                Next
                series.ShowLabels = True
                Me.cvItemTypePie.Series.Add(series)

                'Dim strategy As New PieTwoLabelColumnsStrategy()
                'cvItemTypePie.ShowTitle = True
                'cvItemTypePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
                'cvItemTypePie.ChartElement.TitlePosition = TitlePosition.Top
                'cvItemTypePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                'cvItemTypePie.AreaType = ChartAreaType.Pie
                'cvItemTypePie.ShowLegend = True
                'cvItemTypePie.View.Margin = New Padding(60, 0, 50, 0)
                'Dim series As New PieSeries()
                'series.Range = New AngleRange(270, 360)
                'series.LabelFormat = "{0:P2}"
                'series.RadiusFactor = 0.9F
                'series.ValueMember = "Value"
                'series.DataSource = dt
                'series.ShowLabels = True
                'series.DrawLinesToLabels = True
                'series.SyncLinesToLabelsColor = True
                'series.DisplayMember = "Name"
                'cvItemTypePie.Series.Add(series)

                'For Each item As LegendItem In Me.cvItemTypePie.ChartElement.LegendElement.Provider.LegendInfos
                '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                '    item.Title = clsCommon.myCstr(row("Name"))
                'Next
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
    End Sub

    Private Sub MyCheckBox1_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles MyCheckBox1.ToggleStateChanged
        chkCustomerGrpWise.Checked = MyCheckBox1.Checked
        chkItemGrpWise.Checked = MyCheckBox1.Checked
        chkItemTypeWise.Checked = MyCheckBox1.Checked
        chkZoneWise.Checked = MyCheckBox1.Checked
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        scZone.Panel2Collapsed = True
        scZone.Panel1Collapsed = False
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        scItemGroup.Panel2Collapsed = True
        scItemGroup.Panel1Collapsed = False
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        scCustomerGroup.Panel2Collapsed = True
        scCustomerGroup.Panel1Collapsed = False
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        scItemType.Panel2Collapsed = True
        scItemType.Panel1Collapsed = False
    End Sub
End Class




