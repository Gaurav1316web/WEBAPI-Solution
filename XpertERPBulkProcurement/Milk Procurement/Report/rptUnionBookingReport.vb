Imports common
Public Class rptUnionBookingReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Dim dtRCDF As DataTable = Nothing
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptUnionBookingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub
    Private Sub rptUnionBookingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt And e.KeyCode = Keys.G Then
            fillGridReport(False)
        ElseIf e.Alt And e.KeyCode = Keys.P Then
            fillGridReport(True)

        End If
    End Sub
    Private Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        rbtnAll.IsChecked = True
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        btnGo.Enabled = True
        GroupBox1.Enabled = True
        gv1.DataSource = Nothing
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        dtRCDF = Nothing
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        FrmClose()
    End Sub
    Private Sub FrmClose()
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        fillGridReport(False)
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        btnGo.Enabled = False
        GroupBox1.Enabled = False

    End Sub
    Private Sub fillGridReport(ByVal isPrint As Boolean)
        Try
            If dtRCDF Is Nothing OrElse dtRCDF.Rows.Count <= 0 Then
                TemplateGridview = gv1
                gv1.DataSource = Nothing
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                clsCommon.ProgressBarShow()
                Dim noofday As Integer = DateDiff(DateInterval.Day, txtFromDate.Value, txtToDate.Value) + 1
                If noofday > 0 Then
                    Dim sQuery As String = " with CTERawData as ( "
                    Dim BaseQry As String = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate,Sno AS SNo,'DBLocation' AS [Union_Name], COUNT(DISTINCT XX.Zone_Code) AS [Zone],COUNT(DISTINCT XX.Route_No) AS [Route],COUNT(DISTINCT XX.Cust_Code) AS [Booth],isnull(Round(SUM(isnull(XX.ltrkg,0)),2),0) AS [MilkQty], isnull(Round((SUM(isnull(XX.ltrkg,0))/" + clsCommon.myCstr(noofday) + "),2),0) as [AvarageQty]   FROM ( 
SELECT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DEMAND_BOOKING_MASTER.Route_No, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.qty,  (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) AS ltrkg FROM DBNamePrefixTSPL_DEMAND_BOOKING_MASTER
LEFT JOIN DBNamePrefixTSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No 
LEFT JOIN DBNamePrefixTSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
LEFT JOIN DBNamePrefixTSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_ITEM_MASTER.IsTaxable=0"
                    If rbtnPosted.IsChecked Then
                        BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.Posted=1 "
                    ElseIf rbtnUnposted.IsChecked Then
                        BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.Posted=0 "
                    End If
                    BaseQry += "Union ALL SELECT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_Booking_DETAIL.Route_No, TSPL_Booking_DETAIL.Cust_Code, TSPL_Booking_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_Booking_DETAIL.Unit_code, TSPL_Booking_DETAIL.Booking_Qty, (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) AS ltrkg FROM DBNamePrefixTSPL_Booking_MATSER
LEFT JOIN DBNamePrefixTSPL_Booking_DETAIL ON TSPL_Booking_DETAIL.Document_No = TSPL_Booking_MATSER.Document_No
LEFT JOIN DBNamePrefixTSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Booking_DETAIL.Cust_Code
LEFT JOIN DBNamePrefixTSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_Booking_DETAIL.Unit_code
where convert(date,TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_ITEM_MASTER.IsTaxable=0"
                    If rbtnPosted.IsChecked Then
                        BaseQry += " and TSPL_Booking_MATSER.Posted=1 "
                    ElseIf rbtnUnposted.IsChecked Then
                        BaseQry += " and TSPL_Booking_MATSER.Posted=0 "
                    End If

                    BaseQry += " ) XX "

                    If objCommonVar.RCDFCFP Then
                        sQuery += clsERPFuncationality.ConvertQryForAllUnion(BaseQry, "DBNamePrefix", "DBLocation", "Sno")
                    Else
                        Dim strqry As String = BaseQry.Replace("DBNamePrefix", "")
                        strqry = strqry.Replace("DBLocation", "")
                        strqry = strqry.Replace("Sno", "1")
                        sQuery += strqry
                    End If
                    sQuery += ") 
 select * from CTERawData "
                    dtRCDF = clsDBFuncationality.GetDataTable(sQuery)
                    If (dtRCDF IsNot Nothing AndAlso dtRCDF.Rows.Count > 0) Then
                        RadPageView1.SelectedPage = RadPageViewPage2
                        gv1.Visible = True
                        gv1.DataSource = dtRCDF
                        gv1.ReadOnly = True
                        SetGridFormat()
                        gv1.BestFitColumns()
                        If isPrint Then
                            Dim frmCRV As New frmCrystalReportViewer()
                            frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
                            frmCRV = Nothing
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("No Data Found")
                        gv1.DataSource = Nothing
                    End If
                Else
                    Throw New Exception("From Date can't be greater than To Date")
                End If
                clsCommon.ProgressBarHide()

            Else
                clsCommon.ProgressBarShow()

                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
                    frmCRV = Nothing
                End If
                clsCommon.ProgressBarHide()

            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    
    Sub SetGridFormat()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False
        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False
        gv1.Columns("SNo").HeaderText = "SL No"
        gv1.Columns("SNo").Width = 150
        gv1.Columns("SNo").IsVisible = True
        gv1.Columns("Union_Name").HeaderText = "Union Name"
        gv1.Columns("Union_Name").Width = 150
        gv1.Columns("Union_Name").IsVisible = True
        gv1.Columns("Zone").HeaderText = "No of Zone"
        gv1.Columns("Zone").Width = 150
        gv1.Columns("Zone").FormatString = "{0:n0}"
        gv1.Columns("Zone").IsVisible = True
        gv1.Columns("Route").HeaderText = "No of Route"
        gv1.Columns("Route").Width = 150
        gv1.Columns("Route").FormatString = "{0:n0}"
        gv1.Columns("Route").IsVisible = True
        gv1.Columns("Booth").HeaderText = "No of Booth"
        gv1.Columns("Booth").Width = 150
        gv1.Columns("Booth").FormatString = "{0:n0}"
        gv1.Columns("Booth").IsVisible = True
        gv1.Columns("MilkQty").HeaderText = "Milk Qty in LTR"
        gv1.Columns("MilkQty").Width = 150
        gv1.Columns("MilkQty").FormatString = "{0:n2}"
        gv1.Columns("MilkQty").IsVisible = True
        gv1.Columns("AvarageQty").HeaderText = "Avarage Qty"
        gv1.Columns("AvarageQty").Width = 170
        gv1.Columns("AvarageQty").FormatString = "{0:n0}"
        gv1.Columns("AvarageQty").IsVisible = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item3 As New GridViewSummaryItem("Zone", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Route", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Booth", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("MilkQty", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("AvarageQty", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True
        'View()
    End Sub
    Sub SetGridFormat1()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False
        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False
        gv1.Columns("No of Zone").HeaderText = "No of Zone"
        gv1.Columns("No of Zone").Width = 150
        gv1.Columns("No of Zone").FormatString = "{0:n0}"
        gv1.Columns("No of Zone").IsVisible = True
        gv1.Columns("No of Route").HeaderText = "No of Route"
        gv1.Columns("No of Route").Width = 150
        gv1.Columns("No of Route").FormatString = "{0:n0}"
        gv1.Columns("No of Route").IsVisible = True
        gv1.Columns("No of Booth").HeaderText = "No of Booth"
        gv1.Columns("No of Booth").Width = 150
        gv1.Columns("No of Booth").FormatString = "{0:n0}"
        gv1.Columns("No of Booth").IsVisible = True
        gv1.Columns("Milk Qty in LTR").HeaderText = "Milk Qty in LTR"
        gv1.Columns("Milk Qty in LTR").Width = 150
        gv1.Columns("Milk Qty in LTR").FormatString = "{0:n2}"
        gv1.Columns("Milk Qty in LTR").IsVisible = True
        gv1.Columns("Avarage Qty").HeaderText = "Avarage Qty"
        gv1.Columns("Avarage Qty").Width = 170
        gv1.Columns("Avarage Qty").FormatString = "{0:n0}"
        gv1.Columns("Avarage Qty").IsVisible = True
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        ' View()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        fillGridReport(True)

    End Sub
    'Public Function FormatIndianNumber(ByVal number As Decimal) As String
    '    Dim numberString = Math.Abs(number).ToString("#,##,##0")
    '    If number < 0 Then
    '        Return "-" & numberString
    '    Else
    '        Return numberString
    '    End If
    'End Function

    Private Sub btnexport_Click(sender As Object, e As EventArgs)
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then

            Else
                Throw New Exception("No data found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Milk Marketing Report")
            arrHeader.Add(("Date Range  Form: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Milk Marketing Report", gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Milk Marketing Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    'Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
    '    If e.Column.Name = "Zone" OrElse e.Column.Name = "Route" OrElse e.Column.Name = "Booth" OrElse e.Column.Name = "MilkQty" OrElse e.Column.Name = "AvarageQty" Then
    '        If TypeOf e.CellElement.Value Is Decimal OrElse TypeOf e.CellElement.Value Is Double OrElse TypeOf e.CellElement.Value Is Integer Then
    '            ' Convert the value to a number
    '            Dim number As Decimal = Convert.ToDecimal(e.CellElement.Value)

    '            ' Format the number with hundred separator
    '            e.CellElement.Text = FormatIndianNumber(number)
    '        End If
    '    End If
    'End Sub
End Class