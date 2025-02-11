Imports common
Imports Telerik.Charting
Imports Telerik.QuickStart.WinControls
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class rptCattleFeedProduction
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim dtProduction As DataTable = Nothing
    Dim dtSale As DataTable = Nothing
    Dim dtPurchase As DataTable = Nothing

#End Region
    Private Sub SetUserMgmtNew()
        'SetUserMgmt(clsUserMgtCode.RCDFDashboard)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptCattleFeedProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btngo, "Press Alt+R Refresh ")

        todate.Value = clsCommon.GETSERVERDATE()
        SetFromData()

        AddHandler cvProduction.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvProduction.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        AddHandler CVPurchase.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        GVPurchase.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        AddHandler CVSale.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        GVSale.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        Reset()
    End Sub

    Private Sub SetFromData()
        txtfromDate.Value = todate.Value.AddYears(-1)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        dtProduction = Nothing
        gvProduction.DataSource = Nothing
        gvProduction.Rows.Clear()
        gvProduction.Columns.Clear()
        cvProduction.Series.Clear()

        dtSale = Nothing
        GVSale.DataSource = Nothing
        GVSale.Rows.Clear()
        GVSale.Columns.Clear()
        CVSale.Series.Clear()

        dtPurchase = Nothing
        GVPurchase.DataSource = Nothing
        GVPurchase.Rows.Clear()
        GVPurchase.Columns.Clear()
        CVPurchase.Series.Clear()

        EnableDisableCntrl(True)
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 2
        Dim startDate As DateTime = txtFromDate.Value

        If startDate < New DateTime(2023, 4, 1) Then
            'MessageBox.Show("Start date cannot be earlier than 01/Apr/2023.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error)
            clsCommon.MyMessageBoxShow(Me, "Start date cannot be earlier than 01/Apr/2023.Invalid Date", Me.Text)
            ' Optionally, you can set the Start Date to the minimum allowed date
            'DateTimePickerStart.Value = New DateTime(2023, 4, 1)
            Exit Sub
        End If



        clsCommon.ProgressBarPercentShow()
        Try
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Production()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PURCHASE Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Purchase()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading SALE Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Sale()

            clsCommon.ProgressBarPercentHide()
            EnableDisableCntrl(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Load_Sale()
        Try
            If dtSale Is Nothing OrElse dtSale.Rows.Count <= 0 Then
                Dim sQuery As String = "   with CTERawData as (Select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,
                        case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity 
                        from (SELECT YEAR(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) YearCode,MONTH(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) MonthCode,
                        format(TSPL_SD_SALE_INVOICE_HEAD.Document_Date,'MMM') AS [MonthName],((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Quantity 
                        FROM TSPL_SD_SALE_INVOICE_DETAIL 
                        left join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE TSPL_SD_SALE_INVOICE_HEAD.Status=1 and 
						CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' 
                        AND '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
                If chkFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.FG_for_CF_RPT=1 "
                ElseIf ChkSFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.SFG_for_CF=1 "
                Else
                    sQuery += " and (TSPL_Item_Master.FG_for_CF_RPT=1 or TSPL_Item_Master.SFG_for_CF=1) "
                End If
                sQuery += ")xx)
						select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(SUM(Quantity)as decimal(10,2)) AS Quantity 
                        from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode "

                dtSale = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtSale IsNot Nothing AndAlso dtSale.Rows.Count > 0 Then
                'cvSale.Area.View.Palette = New CustomPalette()
                CVSale.Area.View.Palette = KnownPalette.Flower
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                CVSale.AreaDesign = CartesianArea1
                CVSale.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtSale.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtSale.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtSale.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtSale.Rows(ii)("GrpMonth")
                    drTS("Value") = dtSale.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                CVSale.ShowTitle = True
                CVSale.ChartElement.TitlePosition = TitlePosition.Top
                CVSale.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                CVSale.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                CVSale.Title = "CATTLE FEED SALES"

                Dim smartLabelsController As New SmartLabelsController()
                CVSale.Controllers.Add(smartLabelsController)
                CVSale.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                CVSale.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                CVSale.DataSource = ds
                'LineSeries
                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim lineSeries As New LineSeries()
                    lineSeries.Name = ii
                    lineSeries.LegendTitle = ds.Tables(ii).TableName
                    lineSeries.ValueMember = "Value"
                    lineSeries.CategoryMember = "Name"
                    lineSeries.DataSource = ds.Tables(ii)
                    lineSeries.BorderWidth = 3
                    lineSeries.ShowLabels = True

                    CVSale.Series.Add(lineSeries)

                Next
                CVSale.ShowLegend = True
                setOrientation(CVSale, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                CVSale.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)

                GVSale.DataSource = Nothing
                GVSale.Columns.Clear()
                GVSale.Rows.Clear()
                GVSale.GroupDescriptors.Clear()
                GVSale.MasterTemplate.SummaryRowsBottom.Clear()
                GVSale.ShowGroupPanel = False
                GVSale.EnableFiltering = False
                GVSale.AllowAddNewRow = False

                GVSale.GroupDescriptors.Clear()
                GVSale.TableElement.TableHeaderHeight = 40
                GVSale.MasterTemplate.ShowRowHeaderColumn = False

                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                GVSale.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtSale.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth"))
                        repoRate.Name = clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n0}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        GVSale.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth")), GVSale.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))) Then ''1
                        GVSale.Rows.AddNew()
                        GVSale.Rows(GVSale.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtSale.Rows(ii)("GrpName")), GVSale.Rows.Count - 1) ''2
                    End If
                    GVSale.Rows(arrRow.Item(clsCommon.myCstr(dtSale.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtSale.Rows(ii)(strValue)) ''3
                Next

                'repoRate = New GridViewDecimalColumn()
                'repoRate.FormatString = ""
                'repoRate.HeaderText = "Avg" 
                'repoRate.Name = "TOTAL"
                'repoRate.Width = 100
                'repoRate.Minimum = 0
                'repoRate.FormatString = "{0:n0}"
                ''repoRate.DecimalPlaces = 2
                'repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                'GVSale.MasterTemplate.Columns.Add(repoRate)

                'For jj As Integer = 0 To GVSale.Rows.Count - 1
                '    For ii As Integer = 1 To GVSale.Columns.Count - 2
                '        GVSale.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(GVSale.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(GVSale.Rows(jj).Cells(ii).Value)
                '    Next
                '    GVSale.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(GVSale.Rows(jj).Cells("Total").Value, GVSale.Columns.Count - 2)
                'Next

                For ii As Integer = 0 To GVSale.Columns.Count - 1
                    GVSale.Columns(ii).ReadOnly = True
                    GVSale.Columns(ii).IsVisible = True
                    GVSale.Columns(ii).Width = 150
                Next


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Load_Purchase()
        Try
            If dtPurchase Is Nothing OrElse dtPurchase.Rows.Count <= 0 Then
                Dim sQuery As String = "  with CTERawData as (select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity from (SELECT YEAR(SRN_Date) YearCode,MONTH(SRN_Date)MonthCode,format(SRN_Date,'MMM') AS [MonthName],ROUND((SRN_Qty),0) Quantity
                                          FROM TSPL_SRN_HEAD
                                        LEFT OUTER JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No
                                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SRN_DETAIL.ITEM_CODE
                                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SRN_DETAIL.Item_Code 
						                AND FromUOM.UOM_Code=TSPL_SRN_DETAIL.Unit_code
						                left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SRN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                        WHERE  CONVERT(DATE, TSPL_SRN_HEAD.SRN_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' 
                                        AND '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_SRN_HEAD.Status=1"
                If chkFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.FG_for_CF_RPT=1 "
                ElseIf ChkSFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.SFG_for_CF=1 "
                Else
                    sQuery += " and (TSPL_Item_Master.FG_for_CF_RPT=1 or TSPL_Item_Master.SFG_for_CF=1) "
                End If

                sQuery += " )xx) select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(sum(Quantity)/1000 as int) as Quantity  from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode "

                dtPurchase = clsDBFuncationality.GetDataTable(sQuery)
            End If


            If dtPurchase IsNot Nothing AndAlso dtPurchase.Rows.Count > 0 Then
                'cvSale.Area.View.Palette = New CustomPalette()
                CVPurchase.Area.View.Palette = KnownPalette.Flower
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                CVPurchase.AreaDesign = CartesianArea1
                CVPurchase.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtPurchase.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtSale.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtPurchase.Rows(ii)("GrpMonth")
                    drTS("Value") = dtPurchase.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                CVPurchase.ShowTitle = True
                CVPurchase.ChartElement.TitlePosition = TitlePosition.Top
                CVPurchase.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                CVPurchase.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                CVPurchase.Title = "CATTLE FEED PURCHASE"

                Dim smartLabelsController As New SmartLabelsController()
                CVPurchase.Controllers.Add(smartLabelsController)
                CVPurchase.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                CVPurchase.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                CVPurchase.DataSource = ds
                'LineSeries
                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim lineSeries As New LineSeries()
                    lineSeries.Name = ii
                    lineSeries.LegendTitle = ds.Tables(ii).TableName
                    lineSeries.ValueMember = "Value"
                    lineSeries.CategoryMember = "Name"
                    lineSeries.DataSource = ds.Tables(ii)
                    lineSeries.BorderWidth = 3
                    lineSeries.ShowLabels = True

                    CVPurchase.Series.Add(lineSeries)

                Next
                CVPurchase.ShowLegend = True
                setOrientation(CVPurchase, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                CVPurchase.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)


                GVPurchase.DataSource = Nothing
                GVPurchase.Columns.Clear()
                GVPurchase.Rows.Clear()
                GVPurchase.GroupDescriptors.Clear()
                GVPurchase.MasterTemplate.SummaryRowsBottom.Clear()
                GVPurchase.ShowGroupPanel = False
                GVPurchase.EnableFiltering = False
                GVPurchase.AllowAddNewRow = False

                GVPurchase.GroupDescriptors.Clear()
                GVPurchase.TableElement.TableHeaderHeight = 40
                GVPurchase.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                GVSale.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtPurchase.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpMonth"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtPurchase.Rows(ii)("GrpMonth"))
                        repoRate.Name = clsCommon.myCstr(dtPurchase.Rows(ii)("GrpMonth"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n0}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        GVPurchase.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpMonth")), GVPurchase.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName"))) Then ''1
                        GVPurchase.Rows.AddNew()
                        GVPurchase.Rows(GVPurchase.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName")), GVPurchase.Rows.Count - 1) ''2
                    End If
                    GVPurchase.Rows(arrRow.Item(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtPurchase.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtPurchase.Rows(ii)(strValue)) ''3
                Next

                'repoRate = New GridViewDecimalColumn()
                'repoRate.FormatString = ""
                'repoRate.HeaderText = "Avg"
                'repoRate.Name = "TOTAL"
                'repoRate.Width = 100
                'repoRate.Minimum = 0
                'repoRate.FormatString = "{0:n0}"
                ''repoRate.DecimalPlaces = 2
                'repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                'GVPurchase.MasterTemplate.Columns.Add(repoRate)

                'For jj As Integer = 0 To GVPurchase.Rows.Count - 1
                '    For ii As Integer = 1 To GVPurchase.Columns.Count - 2
                '        GVPurchase.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(GVPurchase.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(GVPurchase.Rows(jj).Cells(ii).Value)
                '    Next
                '    GVPurchase.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(GVPurchase.Rows(jj).Cells("Total").Value, GVPurchase.Columns.Count - 2)
                'Next

                For ii As Integer = 0 To GVPurchase.Columns.Count - 1
                    GVPurchase.Columns(ii).ReadOnly = True
                    GVPurchase.Columns(ii).IsVisible = True
                    GVPurchase.Columns(ii).Width = 150
                Next

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Load_Production()
        Try
            If dtProduction Is Nothing OrElse dtProduction.Rows.Count <= 0 Then
                Dim sQuery As String = "  with CTERawData as (select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity from (SELECT YEAR(PROD_DATE) YearCode,MONTH(PROD_DATE)MonthCode,format(PROD_DATE,'MMM') AS [MonthName],ROUND((FINAL_PRODUCTION_QTY),0) Quantity
                                        FROM TSPL_SPP_PRODUCTION_ENTRY
                                        LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY_DETAIL ON TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code 
						                AND FromUOM.UOM_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Unit_code
						                left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                        WHERE  CONVERT(DATE, TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' 
                                        and TSPL_SPP_PRODUCTION_ENTRY.POSTED=1"
                If chkFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.FG_for_CF_RPT=1 "
                ElseIf ChkSFG.IsChecked = True Then
                    sQuery += " and TSPL_Item_Master.SFG_for_CF=1 "
                Else
                    sQuery += " and (TSPL_Item_Master.FG_for_CF_RPT=1 or TSPL_Item_Master.SFG_for_CF=1) "
                End If

                sQuery += " )xx) select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(sum(Quantity)/1000 as int) as Quantity  from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode   "

                dtProduction = clsDBFuncationality.GetDataTable(sQuery)

            End If

            If dtProduction IsNot Nothing AndAlso dtProduction.Rows.Count > 0 Then
                cvProduction.Area.View.Palette = KnownPalette.Flower

                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvProduction.AreaDesign = CartesianArea1
                cvProduction.Series.Clear()
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


                cvProduction.ShowTitle = True
                cvProduction.ChartElement.TitlePosition = TitlePosition.Top
                cvProduction.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvProduction.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvProduction.Title = "CATTLE FEED PRODUCTION"

                Dim smartLabelsController As New SmartLabelsController()
                cvProduction.Controllers.Add(smartLabelsController)
                cvProduction.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProduction.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProduction.DataSource = ds

                'LineSeries
                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim lineSeries As New LineSeries()
                    lineSeries.Name = ii
                    lineSeries.LegendTitle = ds.Tables(ii).TableName
                    lineSeries.ValueMember = "Value"
                    lineSeries.CategoryMember = "Name"
                    lineSeries.DataSource = ds.Tables(ii)
                    lineSeries.BorderWidth = 3
                    lineSeries.ShowLabels = True

                    cvProduction.Series.Add(lineSeries)

                Next

                cvProduction.ShowLegend = True
                setOrientation(cvProduction, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProduction.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)




                gvProduction.DataSource = Nothing
                gvProduction.Columns.Clear()
                gvProduction.Rows.Clear()
                gvProduction.GroupDescriptors.Clear()
                gvProduction.MasterTemplate.SummaryRowsBottom.Clear()
                gvProduction.ShowGroupPanel = False
                gvProduction.EnableFiltering = False
                gvProduction.AllowAddNewRow = False

                gvProduction.GroupDescriptors.Clear()
                gvProduction.TableElement.TableHeaderHeight = 40
                gvProduction.MasterTemplate.ShowRowHeaderColumn = False

                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvProduction.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        repoRate.Name = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n0}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvProduction.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")), gvProduction.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))) Then ''1
                        gvProduction.Rows.AddNew()
                        gvProduction.Rows(gvProduction.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName")), gvProduction.Rows.Count - 1) ''2
                    End If
                    gvProduction.Rows(arrRow.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtProduction.Rows(ii)(strValue)) ''3
                Next

                'repoRate = New GridViewDecimalColumn()
                'repoRate.FormatString = ""
                'repoRate.HeaderText = "Avg"
                'repoRate.Name = "TOTAL"
                'repoRate.Width = 100
                'repoRate.Minimum = 0
                'repoRate.FormatString = "{0:n0}"
                ''repoRate.DecimalPlaces = 2
                'repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                'gvProduction.MasterTemplate.Columns.Add(repoRate)

                'For jj As Integer = 0 To gvProduction.Rows.Count - 1
                '    For ii As Integer = 1 To gvProduction.Columns.Count - 2
                '        gvProduction.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvProduction.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvProduction.Rows(jj).Cells(ii).Value)
                '    Next
                '    gvProduction.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(gvProduction.Rows(jj).Cells("Total").Value, gvProduction.Columns.Count - 2)
                'Next

                For ii As Integer = 0 To gvProduction.Columns.Count - 1
                    gvProduction.Columns(ii).ReadOnly = True
                    gvProduction.Columns(ii).IsVisible = True
                    gvProduction.Columns(ii).Width = 150
                Next

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



    Private Sub LegendElement_VisualItemCreating(sender As Object, e As LegendItemElementCreatingEventArgs)
        e.ItemElement = New CustomLegendItemElement(e.LegendItem)
    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        ToDate.Enabled = val
        chkFG.Enabled = val
        ChkSFG.Enabled = val
        ChkBoth.Enabled = val

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gvProduction_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvProduction.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvProduction_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvProduction.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GVPurchase_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles GVPurchase.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GVPurchase_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles GVPurchase.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GVSale_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles GVSale.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GVSale_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles GVSale.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
End Class

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