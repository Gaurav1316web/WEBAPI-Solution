Imports common
Imports Telerik.Charting
Public Class frmDBTemp
    Inherits XpertERPEngine.FrmMainTranScreen
    Private Sub FrmDasboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            LoadReportType()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadReportType()
        Dim dr As DataRow
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Bank Cash Book"
        dr("Name") = "Bank Cash Book"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sales Vehicle Utilization"
        dr("Name") = "Sales Vehicle Utilization"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Procurement Milk Purchase"
        dr("Name") = "Procurement Milk Purchase"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Milk Received At Factory"
        dr("Name") = "Milk Received At Factory"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Milk Sale"
        dr("Name") = "Milk Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Product Sale"
        dr("Name") = "Product Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "FG Mass Balance"
        dr("Name") = "FG Mass Balance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "SFG/Raw Milk Mass Balance"
        dr("Name") = "SFG/Raw Milk Mass Balance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sales Transport Costing"
        dr("Name") = "Sales Transport Costing"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Purchase"
        dr("Name") = "Purchase"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Stock/Inventory"
        dr("Name") = "Stock/Inventory"
        dt1.Rows.Add(dr)

        cboReport.DataSource = dt1
        cboReport.ValueMember = "Code"
        cboReport.DisplayMember = "Name"
        cboReport.SelectedIndex = 0
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 14
        clsCommon.ProgressBarPercentShow()
        Try

            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Top 5 Banks Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Loadbankdata()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Top 5 Customers Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadTop5Customers()


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Procuremnt Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadStockInventory()


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Purchase..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadPurchase()


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Vehicle Utilization... " & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadVehicleUtilization()


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading MCC Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadTop5MCC()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Route Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadTop5Route()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading DCS Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadTop5DCS()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading SNF Mass Balance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadSFGMassBalance()


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading FG Mass Balance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadFGMassBalance()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Sale Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            MilkSale()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Product Sale Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            ProductSale()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Received at Factory Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            MilkReceivedAtFactory()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Procurement Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            MilkProcurement()


            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MilkProcurement()
        Try

            Dim qry As String = "select [MCC Name],sum([Qty In LTR]) as [Qty In LTR] from TSPL_DB_ProcMilkPurchase$ group by [MCC Name]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If

            Dim series As New PieSeries()
            series.DataSource = dt
            series.ValueMember = "Qty In LTR"
            series.DisplayMember = "Qty In LTR"
            series.LegendTitleMember = "MCC Name"
            series.ShowLabels = True
            series.DrawLinesToLabels = True
            series.SyncLinesToLabelsColor = True
            series.GradientPercentage = True


            rcvMilkProcurement.Series.Clear()
            rcvMilkProcurement.AreaType = ChartAreaType.Pie
            rcvMilkProcurement.Series.Add(series)
            rcvMilkProcurement.ShowLegend = True
            rcvMilkProcurement.ShowSmartLabels = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MilkReceivedAtFactory()
        Try

            Dim qry As String = "select [MCC Name],sum([Qty In KG]) as [Qty In KG] from TSPL_DB_MilkReceivedAtFactory$ group by [MCC Name]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If


            Dim series As New PieSeries()
            series.DataSource = dt
            series.ValueMember = "Qty In KG"
            series.DisplayMember = "Qty In KG"
            series.LegendTitleMember = "MCC Name"
            series.ShowLabels = True
            series.DrawLinesToLabels = True
            series.SyncLinesToLabelsColor = True
            series.GradientPercentage = True


            rcvMilkReceivedAtFactory.Series.Clear()
            rcvMilkReceivedAtFactory.AreaType = ChartAreaType.Pie
            rcvMilkReceivedAtFactory.Series.Add(series)
            rcvMilkReceivedAtFactory.ShowLegend = True
            rcvMilkReceivedAtFactory.ShowSmartLabels = True

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MilkSale()
        Try
            Dim qry As String = "select Type,sum(Sales) as Sales from TSPL_DB_MilkSale$ group by Type"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If

            Dim series As New PieSeries()
            series.DataSource = dt
            series.ValueMember = "Sales"
            series.DisplayMember = "Sales"
            series.LegendTitleMember = "Type"
            series.ShowLabels = True
            series.DrawLinesToLabels = True
            series.SyncLinesToLabelsColor = True
            series.GradientPercentage = True


            rcvMilkSale.Series.Clear()
            rcvMilkSale.AreaType = ChartAreaType.Pie
            rcvMilkSale.Series.Add(series)
            rcvMilkSale.ShowLegend = True
            rcvMilkSale.ShowSmartLabels = True




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ProductSale()
        Try

            Dim qry As String = "select Type,sum([Total Sale Amount]) as [Total Sale Amount] from TSPL_DB_ProductSale$ where len(isnull(Type,''))>0 group by Type"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If

            Dim series As New PieSeries()
            series.DataSource = dt
            series.ValueMember = "Total Sale Amount"
            series.DisplayMember = "Total Sale Amount"
            series.LegendTitleMember = "Type"
            series.ShowLabels = True
            series.DrawLinesToLabels = True
            series.SyncLinesToLabelsColor = True
            series.GradientPercentage = True


            rcvProductSale.Series.Clear()
            rcvProductSale.AreaType = ChartAreaType.Pie
            rcvProductSale.Series.Add(series)
            rcvProductSale.ShowLegend = True
            rcvProductSale.ShowSmartLabels = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub LoadFGMassBalance()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "FG_M_B"
            Dim Qry As String = "select * from TSPL_DB_FGMassBalance$ order by Alpha"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvFGMassBalance.DataSource = dt
                gvFGMassBalance.GroupDescriptors.Clear()
                gvFGMassBalance.MasterTemplate.SummaryRowsBottom.Clear()
                gvFGMassBalance.MasterTemplate.BestFitColumns()
                gvFGMassBalance.EnableFiltering = True
                For i As Integer = 0 To gvFGMassBalance.Columns.Count - 1
                    gvFGMassBalance.Columns(i).BestFit()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadSFGMassBalance()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "SNF_M_B"
            Dim Qry As String = "select * from TSPL_DB_SNFMass$ order by Alpha"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvSFGMassBalance.DataSource = dt
                gvSFGMassBalance.GroupDescriptors.Clear()
                gvSFGMassBalance.MasterTemplate.SummaryRowsBottom.Clear()
                gvSFGMassBalance.MasterTemplate.BestFitColumns()
                gvSFGMassBalance.EnableFiltering = True
                For i As Integer = 0 To gvSFGMassBalance.Columns.Count - 1
                    gvSFGMassBalance.Columns(i).BestFit()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTop5DCS()
        Try

            Dim qry As String = " select top 5 [VLC Name]+ ' ['+max([VLC Uploader Code])+']' as [VLC Name],sum([Milk Weight]) as [Milk Weight],SUM([ FAT(KG)]) as FAT,sum([SNF(KG)]) as SNF from TSPL_DB_VSPRouteWiseMilkQtyAmt$  group by [VLC Name] order by [Milk Weight] desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrRow As New List(Of String)
            arrRow.Add("Milk Weight")
            arrRow.Add("FAT")
            arrRow.Add("SNF")

            'rcvTop5DCS.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            rcvTop5DCS.AreaDesign = CartesianArea1
            rcvTop5DCS.Series.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                Dim dtNew As DataTable = dt.Copy


                rcvTop5DCS.ShowTitle = True
                rcvTop5DCS.ChartElement.TitlePosition = TitlePosition.Top
                rcvTop5DCS.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                rcvTop5DCS.Title = ""
                rcvTop5DCS.ShowSmartLabels = True
                rcvTop5DCS.AreaType = ChartAreaType.Cartesian
                rcvTop5DCS.ShowLegend = True
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = "VLC Name"
                        barSeries.DataSource = dtNew
                        barSeries.ShowLabels = True
                        barSeries.DrawLinesToLabels = True
                        rcvTop5DCS.Series.Add(barSeries)
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTop5Route()
        Try

            Dim qry As String = "select top 5 [Route Name],sum([Milk Weight]) as [Milk Weight],SUM([ FAT(KG)]) as FAT,sum([SNF(KG)]) as SNF from TSPL_DB_VSPRouteWiseMilkQtyAmt$  group by [Route Name] order by [Milk Weight] desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrRow As New List(Of String)
            arrRow.Add("Milk Weight")
            arrRow.Add("FAT")
            arrRow.Add("SNF")

            'rcvTop5Route.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            rcvTop5Route.AreaDesign = CartesianArea1
            rcvTop5Route.Series.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                Dim dtNew As DataTable = dt.Copy


                rcvTop5Route.ShowTitle = True
                rcvTop5Route.ChartElement.TitlePosition = TitlePosition.Top
                rcvTop5Route.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                rcvTop5Route.Title = ""
                rcvTop5Route.ShowSmartLabels = True
                rcvTop5Route.AreaType = ChartAreaType.Cartesian
                rcvTop5Route.ShowLegend = True
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = "Route Name"
                        barSeries.DataSource = dtNew
                        barSeries.ShowLabels = True
                        barSeries.DrawLinesToLabels = True
                        rcvTop5Route.Series.Add(barSeries)
                    Next
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTop5MCC()
        Try

            Dim qry As String = "select top 5 [MCC Name],sum([Milk Weight]) as [Milk Weight],SUM([ FAT(KG)]) as FAT,sum([SNF(KG)]) as SNF from TSPL_DB_VSPRouteWiseMilkQtyAmt$  group by [MCC Name] order by [Milk Weight] desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrRow As New List(Of String)

            arrRow.Add("Milk Weight")
            arrRow.Add("FAT")
            arrRow.Add("SNF")

            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            rcvTop5MCC.AreaDesign = CartesianArea1
            rcvTop5MCC.Series.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                Dim dtNew As DataTable = dt.Copy

                rcvTop5MCC.ShowTitle = True
                rcvTop5MCC.ChartElement.TitlePosition = TitlePosition.Top
                rcvTop5MCC.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                rcvTop5MCC.ShowSmartLabels = True
                rcvTop5MCC.Title = ""
                rcvTop5MCC.AreaType = ChartAreaType.Cartesian
                rcvTop5MCC.ShowLegend = True
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = "MCC Name"
                        barSeries.DataSource = dtNew
                        barSeries.ShowLabels = True
                        barSeries.DrawLinesToLabels = True
                        rcvTop5MCC.Series.Add(barSeries)
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadVehicleUtilization()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Vehicle_Utilization"
            Dim Qry As String = "select * from TSPL_DB_Vehicle$  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvVehicleUtilization.DataSource = dt
                gvVehicleUtilization.GroupDescriptors.Clear()
                gvVehicleUtilization.MasterTemplate.SummaryRowsBottom.Clear()
                gvVehicleUtilization.MasterTemplate.BestFitColumns()
                gvVehicleUtilization.EnableFiltering = True
                For i As Integer = 0 To gvVehicleUtilization.Columns.Count - 1
                    gvVehicleUtilization.Columns(i).BestFit()
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim NoofKMRunningPerMonth As New GridViewSummaryItem("Vehicle Capacity Utilized", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(NoofKMRunningPerMonth)
                Dim FreightCost As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FreightCost)
                Dim SalesInLTR As New GridViewSummaryItem("Sales In LTR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR)

                Dim SalesInLTR1 As New GridViewSummaryItem("Sales Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR1)
                Dim SalesInLTR2 As New GridViewSummaryItem("Fixed KM", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR2)
                Dim SalesInLTR3 As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR3)
                Dim SalesInLTR4 As New GridViewSummaryItem("Vehicle Capacity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR4)

                gvVehicleUtilization.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadPurchase()
        Try

            Dim qry As String = "select Structure,sum(Value) as Value from TSPL_DB_Purchase$ group by Structure"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            'rcvPurchase.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            rcvPurchase.AreaDesign = CartesianArea1
            rcvPurchase.Series.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                Dim dtNew As DataTable = dt.Copy

                rcvPurchase.ShowTitle = True
                rcvPurchase.ChartElement.TitlePosition = TitlePosition.Top
                rcvPurchase.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                rcvPurchase.Title = ""
                rcvPurchase.ShowSmartLabels = True
                rcvPurchase.ShowLegend = True
                rcvPurchase.AreaType = ChartAreaType.Cartesian
                For Each dr As DataRow In dt.Rows
                    Dim barSeries As New BarSeries()
                    barSeries.Name = clsCommon.myCstr(dr("Structure"))
                    barSeries.LegendTitle = clsCommon.myCstr(dr("Structure"))
                    barSeries.ValueMember = "Value"
                    barSeries.CategoryMember = "Structure"
                    Dim dtTemp As DataTable = dt.Select("Structure = '" + clsCommon.myCstr(dr("Structure")) + "'").CopyToDataTable()
                    barSeries.DataSource = dtTemp
                    barSeries.ShowLabels = True
                    barSeries.DrawLinesToLabels = True
                    rcvPurchase.Series.Add(barSeries)
                Next

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTop5Customers()
        Try

            Dim qry As String = "select top 5 [Customer Code],max([Customer Name]) as [Customer Name],sum([Total Amount]) as [Total Amount] from TSPL_DB_Customer$ group by [Customer Code] order by [Total Amount] desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If

            Dim pieSeries As New PieSeries()
            pieSeries.DataSource = dt
            pieSeries.ValueMember = "Total Amount"
            pieSeries.DisplayMember = "Total Amount"
            pieSeries.LegendTitleMember = "Customer Name"
            pieSeries.DrawLinesToLabels = True
            pieSeries.SyncLinesToLabelsColor = True
            pieSeries.GradientPercentage = True
            pieSeries.ShowLabels = True

            rcvTop5Customer.Series.Clear()
            rcvTop5Customer.AreaType = ChartAreaType.Pie
            rcvTop5Customer.ShowSmartLabels = True
            Me.rcvTop5Customer.Series.Add(pieSeries)
            Me.rcvTop5Customer.ShowLegend = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Loadbankdata()
        Try

            Dim qry As String = "select [Bank Name],Closing  from TSPL_DB_Bank$ order  by [Bank Name]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If
            rcvTop5Bank.Series.Clear()
            rcvTop5Bank.ShowLegend = True
            rcvTop5Bank.AreaType = ChartAreaType.Pie
            rcvTop5Bank.ShowSmartLabels = True
            rcvTop5Bank.View.Margin = New Padding(60, 0, 50, 0)
            Dim series As New PieSeries()

            series.DataSource = dt
            series.ValueMember = "Closing"
            series.DisplayMember = "Closing"
            series.LegendTitleMember = "Bank Name"
            series.ShowLabels = True
            series.DrawLinesToLabels = True
            series.SyncLinesToLabelsColor = True
            series.GradientPercentage = True
            rcvTop5Bank.Series.Add(series)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadStockInventory()
        Try

            Dim qry As String = "select top 5 [Item Type],sum([Opening Stock Value]) as [Opening Stock Value],sum([Purchase]) as [Purchase],sum([Issues]) as [Issues],sum([Closing Value]) as [Closing Value],sum([Consumption]) as [Consumption] from TSPL_DB_Stock$ group by [Item Type] order by [Item Type] desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrRow As New List(Of String)
            'arrRow.Add("Opening Stock Value")
            arrRow.Add("Purchase")
            arrRow.Add("Issues")
            arrRow.Add("Consumption")
            arrRow.Add("Closing Value")



            'rcvStockInventory.Area.View.Palette = New CustomPalette()
            Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
            rcvStockInventory.AreaDesign = CartesianArea1
            rcvStockInventory.Series.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strValue As String = ""
                Dim dtNew As DataTable = dt.Copy

                rcvStockInventory.ShowTitle = True
                rcvStockInventory.ChartElement.TitlePosition = TitlePosition.Top
                rcvStockInventory.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                rcvStockInventory.Title = ""
                rcvStockInventory.ShowSmartLabels = True
                rcvStockInventory.AreaType = ChartAreaType.Cartesian
                rcvStockInventory.ShowLegend = True
                If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                    For Each strVal As String In arrRow
                        Dim barSeries As New BarSeries()
                        barSeries.Name = strVal
                        barSeries.LegendTitle = strVal
                        barSeries.ValueMember = strVal
                        barSeries.CategoryMember = "Item Type"
                        barSeries.DataSource = dtNew
                        barSeries.ShowLabels = True
                        barSeries.DrawLinesToLabels = True
                        rcvStockInventory.Series.Add(barSeries)
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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



    Sub FormatGrid(ByRef GridName As RadGridView)

        GridName.Columns("Quantity In Ltr").HeaderText = "Sales"
        GridName.Columns("Quantity In Ltr").FormatString = "{0:F2}"
        GridName.Columns("Scheme Quantity In Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Ltr").HeaderText = "Sample"

        GridName.Columns("Quantity In Kg").HeaderText = "Sales"
        GridName.Columns("Quantity In Kg").FormatString = "{0:F2}"
        GridName.Columns("Scheme Quantity In Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Kg").HeaderText = "Sample"

        GridName.Columns("FAT KG").HeaderText = "Sales"
        GridName.Columns("FAT KG").FormatString = "{0:F2}"
        GridName.Columns("Scheme FAT KG").HeaderText = "Scheme"
        GridName.Columns("Sample FAT KG").HeaderText = "Sample"

        GridName.Columns("SNF KG").HeaderText = "Sales"
        GridName.Columns("SNF KG").FormatString = "{0:F2}"
        GridName.Columns("Scheme SNF KG").HeaderText = "Scheme"
        GridName.Columns("Sample SNF KG").HeaderText = "Sample"

        GridName.Columns("Sale Amount").HeaderText = "Sales"
        GridName.Columns("Sale Amount").FormatString = "{0:F2}"
        GridName.Columns("Scheme Sale Amount").HeaderText = "Scheme"
        GridName.Columns("Sample Sale Amount").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Ltr").HeaderText = "Sales"
        GridName.Columns("Ave Realisa Per Ltr").FormatString = "{0:F2}"
        GridName.Columns("Scheme Ave Realisa Per Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Ltr").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Kg").HeaderText = "Sales"
        GridName.Columns("Ave Realisa Per Kg").FormatString = "{0:F2}"
        GridName.Columns("Scheme Ave Realisa Per Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Kg").HeaderText = "Sample"


        GridName.Columns("Total Quantity In Ltr").HeaderText = "Total Quantity In Ltr"
        GridName.Columns("Total Quantity In Ltr").FormatString = "{0:F2}"

        GridName.Columns("Total Quantity In Kg").HeaderText = "Total Quantity In Kg"
        GridName.Columns("Total Quantity In Kg").FormatString = "{0:F2}"

        GridName.Columns("Total FAT KG").HeaderText = "Total FAT KG"
        GridName.Columns("Total FAT KG").FormatString = "{0:F2}"

        GridName.Columns("Total SNF KG").HeaderText = "Total SNF KG"
        GridName.Columns("Total SNF KG").FormatString = "{0:F2}"

        GridName.Columns("Total Sale Amount").HeaderText = "Total Sale Amount"
        GridName.Columns("Total Sale Amount").FormatString = "{0:F2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim QtyInLTR As New GridViewSummaryItem("Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(QtyInLTR)
        Dim SchemeQtyInLTR As New GridViewSummaryItem("Scheme Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeQtyInLTR)
        Dim SampleQtyInLTR As New GridViewSummaryItem("Sample Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleQtyInLTR)

        Dim QtyInKG As New GridViewSummaryItem("Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(QtyInKG)
        Dim SchemeQtyInKG As New GridViewSummaryItem("Scheme Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeQtyInKG)
        Dim SampleQtyInKG As New GridViewSummaryItem("Sample Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleQtyInKG)

        Dim FATKG As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(FATKG)
        Dim SchemeFATKG As New GridViewSummaryItem("Scheme FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeFATKG)
        Dim SampleFATKG As New GridViewSummaryItem("Sample FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleFATKG)

        Dim SNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SNFKG)
        Dim SchemeSNFKG As New GridViewSummaryItem("Scheme SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeSNFKG)
        Dim SampleSNFKG As New GridViewSummaryItem("Sample SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSNFKG)

        Dim SaleAmount As New GridViewSummaryItem("Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SaleAmount)
        Dim SchemeSaleAmount As New GridViewSummaryItem("Scheme Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeSaleAmount)
        Dim SampleSaleAmount As New GridViewSummaryItem("Sample Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)


        SampleSaleAmount = New GridViewSummaryItem("Total Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)


        GridName.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Sub View(ByRef GridName As RadGridView)

        If GridName.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(GridName.Columns("Item").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity In Ltr"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Sample Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Total Quantity In Ltr").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity In Kg"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Sample Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Total Quantity In Kg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("FAT KG"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Scheme FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Sample FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Total FAT KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("SNF KG"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Scheme SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Sample SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Total SNF KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sale Amount"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sample Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Total Sale Amount").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Ave Realisa Per Ltr"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Ave Realisa Per Ltr").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Ave Realisa Per Ltr").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Sample Ave Realisa Per Ltr").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Ave Realisa Per Kg"))
            view.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Ave Realisa Per Kg").Name)
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Ave Realisa Per Kg").Name)
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Sample Ave Realisa Per Kg").Name)

            GridName.ViewDefinition = view
        End If

    End Sub

    Sub LoadProductSale()
        'Try
        '    PageSetupReport_ID = clsUserMgtCode.frmDasboard + "ProductSale"
        '    gv_ProductSale.DataSource = Nothing
        '    Dim dtAll As DataTable = clsDB.GetTableProductSale(txtFromDate.Value, txtToDate.Value)
        '    If dtAll IsNot Nothing OrElse dtAll.Rows.Count > 0 Then
        '        gv_ProductSale.GroupDescriptors.Clear()
        '        gv_ProductSale.MasterTemplate.SummaryRowsBottom.Clear()
        '        gv_ProductSale.DataSource = dtAll
        '        gv_ProductSale.MasterTemplate.BestFitColumns()
        '        gv_ProductSale.EnableFiltering = True
        '        For i As Integer = 0 To gv_ProductSale.Columns.Count - 1
        '            gv_ProductSale.Columns(i).ReadOnly = True
        '            gv_ProductSale.Columns(i).BestFit()
        '        Next
        '        FormatGrid(gv_ProductSale)
        '        View(gv_ProductSale)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub
    Sub LoadTransportCharges()
        'Try
        '    Dim Qry As String = clsDB.GetQueryTransportCharges(txtFromDate.Value, txtToDate.Value)
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
        '        gvTransportcost.DataSource = dt
        '        gvTransportcost.GroupDescriptors.Clear()
        '        gvTransportcost.MasterTemplate.SummaryRowsBottom.Clear()
        '        gvTransportcost.MasterTemplate.BestFitColumns()
        '        gvTransportcost.EnableFiltering = True
        '        For i As Integer = 0 To gvTransportcost.Columns.Count - 1
        '            gvTransportcost.Columns(i).BestFit()
        '        Next
        '        gvTransportcost.Columns("Zone").IsVisible = False
        '        gvTransportcost.Columns("Route").IsVisible = False
        '        gvTransportcost.Columns("Vehicle").IsVisible = False

        '        Dim summaryRowItem As New GridViewSummaryRowItem()
        '        Dim SalesInLTR As New GridViewSummaryItem("Sales In LTR", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(SalesInLTR)
        '        Dim SalesValue As New GridViewSummaryItem("Sales Value", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(SalesValue)
        '        Dim FreightAmount As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(FreightAmount)

        '        gvTransportcost.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub
    Sub LoadPO()
        'Try
        '    PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport1"
        '    Dim Qry As String = clsDB.GetQueryStorePO(txtFromDate.Value, txtToDate.Value)
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
        '        gv_po.DataSource = dt
        '        gv_po.GroupDescriptors.Clear()
        '        gv_po.MasterTemplate.SummaryRowsBottom.Clear()
        '        gv_po.MasterTemplate.BestFitColumns()
        '        gv_po.EnableFiltering = True
        '        gv_po.Columns("Structure_Code").IsVisible = False
        '        For i As Integer = 0 To gv_po.Columns.Count - 1
        '            gv_po.Columns(i).BestFit()
        '        Next
        '        Dim summaryRowItem As New GridViewSummaryRowItem()

        '        Dim NoofPO As New GridViewSummaryItem("No of PO", "", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(NoofPO)
        '        Dim NoofGRN As New GridViewSummaryItem("No of GRN", "", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(NoofGRN)
        '        Dim NoofSRN As New GridViewSummaryItem("No of SRN", "", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(NoofSRN)
        '        Dim Values As New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(Values)
        '        gv_po.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub

    Sub LoadStore()
        'Try
        '    PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport"
        '    Dim Qry As String = clsDB.GetQueryStoreStore(txtFromDate.Value, txtToDate.Value)
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
        '        gv_store.DataSource = dt
        '        gv_store.GroupDescriptors.Clear()
        '        gv_store.MasterTemplate.SummaryRowsBottom.Clear()
        '        gv_store.MasterTemplate.BestFitColumns()
        '        gv_store.EnableFiltering = True
        '        For i As Integer = 0 To gv_store.Columns.Count - 1
        '            gv_store.Columns(i).BestFit()
        '        Next
        '        gv_store.Columns("Structure_Code").IsVisible = False
        '        gv_store.Columns("ITEM_TYPE_CODE").IsVisible = False
        '        Dim summaryRowItem As New GridViewSummaryRowItem()

        '        Dim OpeningStockValue As New GridViewSummaryItem("Opening Stock Value", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(OpeningStockValue)
        '        Dim Purchase As New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(Purchase)
        '        Dim Issues As New GridViewSummaryItem("Issues", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(Issues)
        '        Dim ClosingValue As New GridViewSummaryItem("Closing Value", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(ClosingValue)
        '        Dim Consumption As New GridViewSummaryItem("Consumption", "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(Consumption)

        '        gv_store.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub

    Sub LoadMassBalance(ByVal isSFG As Boolean, ByVal gv1 As RadGridView)
        'Try
        '    Dim strTotalInput As String = ""
        '    Dim strTotalOutput As String = ""
        '    Dim qry As String = clsDB.GetQueryMassBalance(Nothing, txtFromDate.Value, txtToDate.Value, IIf(isSFG, 1, 2), "", "", False, strTotalInput, strTotalOutput)
        '    Dim strColPraticularName As String = "ParticularName"
        '    qry = "select Alpha,case when max(Trans)='' then max(ParticularName) else max(Trans) end as Trans,sum(QtyKg) as QtyKg,sum(QtyLtr) as QtyLtr,case when sum(QtyKg)=0 then 0 else cast( sum(Fat_KG)*100/sum(QtyKg)as decimal(18,2)) end as Fat_Per, case when sum(QtyKg)=0 then 0 else CAST(sum(SNF_KG)*100/sum(QtyKg)as decimal(18,2)) end as SNF_Per ,sum(Fat_KG) as Fat_KG,sum(SNF_KG) as SNF_KG,sum(Avg_Cost) as Avg_Cost from (" + Environment.NewLine + qry + Environment.NewLine + ")xxx group by Alpha"
        '    strColPraticularName = "Trans"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If dt IsNot Nothing And dt.Rows.Count > 0 Then
        '        qry = strTotalInput + Environment.NewLine + " Union all " + Environment.NewLine + strTotalOutput
        '        Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(qry)
        '        If dtTotal IsNot Nothing AndAlso dtTotal.Rows.Count > 0 Then
        '            Dim drKg As DataRow = dt.NewRow()
        '            drKg("Alpha") = "L"
        '            drKg(strColPraticularName) = "Kg FAT & Kg SNF Loss/Gain"
        '            drKg("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
        '            drKg("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
        '            drKg("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)

        '            Dim drPer As DataRow = dt.NewRow()
        '            drPer("Alpha") = "M"
        '            drPer(strColPraticularName) = "Kg FAT & Kg SNF Loss/Gain %"
        '            If clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) <> 0 Then
        '                drPer("Fat_KG") = Math.Round((clsCommon.myCdbl(drKg("Fat_KG")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
        '            End If
        '            If clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")) <> 0 Then
        '                drPer("SNF_KG") = Math.Round((clsCommon.myCdbl(drKg("SNF_KG")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
        '            End If
        '            If clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")) <> 0 Then
        '                drPer("Avg_Cost") = Math.Round((clsCommon.myCdbl(drKg("Avg_Cost")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)
        '            End If

        '            Dim drTS As DataRow = dt.NewRow()
        '            drTS("Alpha") = "N"
        '            drTS(strColPraticularName) = "Total TS Loss/gain %"
        '            drTS("Fat_KG") = clsCommon.myCdbl(drPer("Fat_KG")) + clsCommon.myCdbl(drPer("SNF_KG"))

        '            dt.Rows.Add(drKg)
        '            dt.Rows.Add(drPer)
        '            dt.Rows.Add(drTS)
        '        End If
        '        gv1.DataSource = dt
        '        gv1.GroupDescriptors.Clear()
        '        gv1.ShowGroupPanel = False
        '        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        '        gv1.EnableFiltering = False
        '        For ii As Integer = 0 To gv1.Columns.Count - 1
        '            gv1.Columns(ii).ReadOnly = True
        '            gv1.Columns(ii).IsVisible = True
        '        Next
        '        gv1.BestFitColumns()
        '        gv1.Columns("Alpha").HeaderText = "Alpha"
        '        gv1.Columns("QtyKg").HeaderText = "Qty KG"
        '        gv1.Columns("QtyLtr").HeaderText = "Qty Ltr"
        '        gv1.Columns("Fat_Per").HeaderText = "FAT %"
        '        gv1.Columns("SNF_Per").HeaderText = "SNF %"
        '        gv1.Columns("Fat_KG").HeaderText = "FAT Kg"
        '        gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
        '        gv1.Columns("Avg_Cost").HeaderText = "Amount"
        '    End If
        '    gv1.BestFitColumns()
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub
    Private Sub gvFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
        'Try
        '    OpenMass(gvFGMassBalance, True)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub
    Private Sub gvSFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
        'Try
        '    OpenMass(gvSFGMassBalance, False)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub
    Sub OpenMass(ByVal gv As RadGridView, ByVal isFG As Boolean)
        Try
            Dim frm As New rptMassBalanceReport
            frm.SetUserMgmt(clsUserMgtCode.MISMassBalanceReport)
            frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.MISMassBalanceReport)
            frm.FilterON = True
            frm.FilterfromDate = txtFromDate.Value
            frm.FilterToDate = txtToDate.Value
            frm.FilterisFG = isFG
            frm.FilterAlpha = clsCommon.myCstr(gv.CurrentRow.Cells("Alpha").Value)
            frm.MdiParent = MDI
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_MilkSale_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
        'Try
        '    If clsCommon.myLen(clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value)) > 0 Then
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select item_code from tspl_item_master where Alies_Name='" + clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value) + "'")
        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '            Dim frm As RptSaleRegisterReport
        '            frm = New RptSaleRegisterReport(clsUserMgtCode.MISSaleRegister)
        '            frm.isReadFlag = True
        '            frm.isDataLoad = True
        '            frm.dtFrom = txtFromDate.Value
        '            frm.dtTo = txtToDate.Value
        '            frm.Unit_Code = "Ltr"
        '            'frm.arrTransaction = txtTransaction.arrValueMember
        '            frm.arrItem = New ArrayList()
        '            For Each dr As DataRow In dt.Rows
        '                frm.arrItem.Add(clsCommon.myCstr(dr("item_code")))
        '            Next

        '            'frm.arrItemGroup = txtItemGroup.arrValueMember
        '            'frm.arrLocation = txtLocation.arrValueMember
        '            'frm.arrCustomer = txtCustomer.arrValueMember
        '            'frm.arrCustGroup = txtCustGroup.arrValueMember
        '            'frm.arrState = txtState.arrValueMember
        '            'frm.arrCat = New Dictionary(Of String, Object)
        '            'Dim arrSel As Dictionary(Of String, Object) = Nothing
        '            'Dim TempCode As String = ""
        '            'Dim dtCategory As DataTable

        '            'dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        '            'If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
        '            '    For ii As Integer = 0 To dtCategory.Rows.Count - 1
        '            '        If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value), "") = CompairStringResult.Equal Then
        '            '            Exit For
        '            '        End If
        '            '        arrSel = New Dictionary(Of String, Object)
        '            '        TempCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "' AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value) + "'"))
        '            '        arrSel.Add(TempCode, Nothing)
        '            '        frm.arrCat.Add(clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim(), arrSel)
        '            '    Next
        '            'End If


        '            frm.strType = "Item Wise"
        '            frm.WindowState = FormWindowState.Maximized
        '            frm.Focus()
        '            frm.Visible = False
        '            frm.MdiParent = MDI
        '            frm.Show()
        '        End If
        '    Else
        '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select item_code+'  ['+Item_Desc+']' as Item from tspl_item_master where item_type='F' and Alies_Name='" + clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value) + "'")
        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '            Dim exc As String = "Please Set the Alias Name of following Items"
        '            For Each dr As DataRow In dt.Rows
        '                exc += Environment.NewLine + clsCommon.myCstr(dr("Item"))
        '            Next
        '            Dim logFile As String = "MissingItemAliesName.txt"
        '            If System.IO.File.Exists(logFile) Then
        '                Dim stream As New IO.StreamWriter(logFile, False)
        '                stream.WriteLine("")
        '                stream.Close()
        '            Else
        '                Dim fs As IO.FileStream = System.IO.File.Create(logFile)
        '                fs.Close()
        '            End If
        '            Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '            objWriter.WriteLine(exc)
        '            objWriter.Close()

        '            Dim objreader As New System.IO.StringReader(logFile)
        '            If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
        '                Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText(logFile))
        '                If clsCommon.myLen(str) > 0 Then
        '                    System.Diagnostics.Process.Start(logFile)
        '                End If
        '            End If

        '        End If

        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub














    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub



    Private Sub Export(ByVal ExportType As Exporter)
        'Dim arrHeader As List(Of String) = New List(Of String)()
        'arrHeader.Add("Name : Dashboard Report")
        'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        'arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

        'If ExportType = Exporter.Excel Then
        '    If clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "All") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv3, "", "Bank Cash Book", , arrHeader)
        '        transportSql.QuickExportToExcel(gv4, "", "Sales Vehicle Utilization", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_Procurement, "", "Procurement Milk Purchase", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_MilkReceived, "", "Milk Received At Factory", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_MilkSale, "", "Milk Sale", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_ProductSale, "", "Product Sale", , arrHeader)
        '        transportSql.QuickExportToExcel(gvFGMassBalance, "", "FG Mass Balance", , arrHeader)
        '        transportSql.QuickExportToExcel(gvSFGMassBalance, "", "SFG/Raw Milk Mass Balance", , arrHeader)
        '        transportSql.QuickExportToExcel(gvTransportcost, "", "Sales Transport Costing", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_po, "", "Purchase", , arrHeader)
        '        transportSql.QuickExportToExcel(gv_store, "", "Stock/Inventory", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Bank Cash Book") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv3, "", "Bank Cash Book", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Vehicle Utilization") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv4, "", "Sales Vehicle Utilization", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Procurement Milk Purchase") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_Procurement, "", "Procurement Milk Purchase", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Received At Factory") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_MilkReceived, "", "Milk Received At Factory", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Sale") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_MilkSale, "", "Milk Sale", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_ProductSale, "", "Product Sale", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "FG Mass Balance") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gvFGMassBalance, "", "FG Mass Balance", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "SFG/Raw Milk Mass Balance") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gvSFGMassBalance, "", "SFG/Raw Milk Mass Balance", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Transport Costing") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gvTransportcost, "", "Sales Transport Costing", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Purchase") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_po, "", "Purchase", , arrHeader)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Stock/Inventory") = CompairStringResult.Equal Then
        '        transportSql.QuickExportToExcel(gv_store, "", "Stock/Inventory", , arrHeader)
        '    End If
        'ElseIf ExportType = Exporter.PDF Then
        '    If clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "All") = CompairStringResult.Equal Then
        '        If gv3.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Bank Cash Book", gv3, arrHeader, "Bank Cash Book")
        '        End If
        '        If gv4.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Sales Vehicle Utilization", gv4, arrHeader, "Sales Vehicle Utilization")
        '        End If
        '        If gv_Procurement.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Procurement Milk Purchase", gv_Procurement, arrHeader, "Procurement Milk Purchase")
        '        End If
        '        If gv_MilkReceived.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Milk Received At Factory", gv_MilkReceived, arrHeader, "Milk Received At Factory")
        '        End If
        '        If gv_MilkSale.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Milk Sale", gv_MilkSale, arrHeader, "Milk Sale")
        '        End If
        '        If gv_ProductSale.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Product Sale", gv_ProductSale, arrHeader, "Product Sale")
        '        End If
        '        If gvFGMassBalance.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("FG Mass Balance", gvFGMassBalance, arrHeader, "FG Mass Balance")
        '        End If
        '        If gvSFGMassBalance.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("SFG-Raw Milk Mass Balance", gvSFGMassBalance, arrHeader, "SFG-Raw Milk Mass Balance")
        '        End If
        '        If gvTransportcost.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Sales Transport Costing", gvTransportcost, arrHeader, "Sales Transport Costing")
        '        End If
        '        If gv_po.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Purchase", gv_po, arrHeader, "Purchase")
        '        End If
        '        If gv_store.Rows.Count > 0 Then
        '            clsCommon.MyExportToPDF("Stock-Inventory", gv_store, arrHeader, "Stock-Inventory")
        '        End If

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Bank Cash Book") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Bank Cash Book", gv3, arrHeader, "Bank Cash Book")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Vehicle Utilization") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Sales Vehicle Utilization", gv4, arrHeader, "Sales Vehicle Utilization")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Procurement Milk Purchase") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Procurement Milk Purchase", gv_Procurement, arrHeader, "Procurement Milk Purchase")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Received At Factory") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Milk Received At Factory", gv_MilkReceived, arrHeader, "Milk Received At Factory")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Sale") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Milk Sale", gv_MilkSale, arrHeader, "Milk Sale")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Product Sale", gv_ProductSale, arrHeader, "Product Sale")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "FG Mass Balance") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("FG Mass Balance", gvFGMassBalance, arrHeader, "FG Mass Balance")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "SFG/Raw Milk Mass Balance") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("SFG-Raw Milk Mass Balance", gvSFGMassBalance, arrHeader, "SFG-Raw Milk Mass Balance")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Transport Costing") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Sales Transport Costing", gvTransportcost, arrHeader, "Sales Transport Costing")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Purchase") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Purchase", gv_po, arrHeader, "Purchase")
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Stock/Inventory") = CompairStringResult.Equal Then
        '        clsCommon.MyExportToPDF("Stock-Inventory", gv_store, arrHeader, "Stock-Inventory")
        '    End If
        'End If
    End Sub

    Private Sub EXExcel_Click(sender As Object, e As EventArgs) Handles EXExcel.Click
        Export(Exporter.Excel)
    End Sub

    Private Sub EXPDF_Click(sender As Object, e As EventArgs) Handles EXPDF.Click
        Export(Exporter.PDF)
    End Sub
End Class


