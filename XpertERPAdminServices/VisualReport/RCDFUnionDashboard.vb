Imports common
Imports Telerik.Charting
Public Class RCDFUnionDashboard
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim dtProc As DataTable = Nothing
    Dim dtSale As DataTable = Nothing
#End Region
    Private Sub SetUserMgmtNew()
        'SetUserMgmt(clsUserMgtCode.RCDFDashboard)
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

        AddHandler cvSale.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvSale.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        AddHandler cvProc.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvProc.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill


        Reset()
    End Sub
    Private Sub SetFromData()
        txtFromDate.Value = txtToDate.Value.AddYears(-1)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        dtProc = Nothing
        gvProc.DataSource = Nothing
        gvProc.Rows.Clear()
        gvProc.Columns.Clear()
        cvProc.Series.Clear()

        dtSale = Nothing
        gvSale.DataSource = Nothing
        gvSale.Rows.Clear()
        gvSale.Columns.Clear()
        cvSale.Series.Clear()



        EnableDisableCntrl(True)
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 2
        clsCommon.ProgressBarPercentShow()
        Try
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading  MILK PROCUREMENT Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Procurement()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
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
                Dim sQuery As String = " with CTERawData as ( 
                select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity from ("

                Dim BaseQry As String = "select (case when MONTH(TSPL_DEMAND_BOOKING_MASTER.Document_Date)>3 then Year(TSPL_DEMAND_BOOKING_MASTER.Document_Date) else Year(TSPL_DEMAND_BOOKING_MASTER.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_DEMAND_BOOKING_MASTER.Document_Date) as MonthCode,format(TSPL_DEMAND_BOOKING_MASTER.Document_Date,'MMM') as [MonthName], CASE when  TSPL_ITEM_MASTER.Is_FreshItem =1 then convert(decimal(18,2),(isnull( TotalLtr_ItemWise,0)) ) ELSE convert(decimal(18,2),(isnull( Qty,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Quantity
from DBNamePrefixTSPL_DEMAND_BOOKING_DETAIL  
LEFT JOIN DBNamePrefixTSPL_DEMAND_BOOKING_MASTER   ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
LEFT JOIN DBNamePrefixTSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT JOIN DBNamePrefixTSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code =   TSPL_DEMAND_BOOKING_DETAIL.Unit_code
LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    DBNamePrefixTSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG] )) P ) I ON   TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code
WHERE CONVERT(DATE, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' 
 UNION ALL
SELECT (case when MONTH(TSPL_Dispatch_BulkSale.Document_Date)>3 then Year(TSPL_Dispatch_BulkSale.Document_Date) else Year(TSPL_Dispatch_BulkSale.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_Dispatch_BulkSale.Document_Date) as MonthCode,format(TSPL_Dispatch_BulkSale.Document_Date,'MMM') as [MonthName],case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Quantity
FROM  DBNamePrefixTSPL_Dispatch_Detail_BulkSale
LEFT JOIN  DBNamePrefixTSPL_Dispatch_BulkSale  ON TSPL_Dispatch_BulkSale.Document_No = TSPL_Dispatch_Detail_BulkSale.Document_No
inner join DBNamePrefixTSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join DBNamePrefixTSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  
WHERE CONVERT(DATE, TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
 UNION ALL
select  (case when MONTH(TSPL_BOOKING_MATSER.Document_Date)>3 then Year(TSPL_BOOKING_MATSER.Document_Date) else Year(TSPL_BOOKING_MATSER.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_BOOKING_MATSER.Document_Date) as MonthCode,format(TSPL_BOOKING_MATSER.Document_Date,'MMM') as [MonthName],CASE when  tspl_item_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull( TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Quantity 
FROM DBNamePrefixTSPL_BOOKING_DETAIL 
LEFT JOIN DBNamePrefixTSPL_BOOKING_MATSER  ON TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No
LEFT JOIN DBNamePrefixtspl_item_master  ON tspl_item_master.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
LEFT JOIN  DBNamePrefixTSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code =   TSPL_BOOKING_DETAIL.Item_Code and  TSPL_ITEM_UOM_DETAIL.UOM_Code =   TSPL_BOOKING_DETAIL.Unit_code
LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    DBNamePrefixTSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   TSPL_BOOKING_DETAIL.ITEM_CODE = I.item_code
WHERE  CONVERT(DATE, TSPL_BOOKING_MATSER.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'   "
                If objCommonVar.RCDFCFP Then
                    sQuery += clsERPFuncationality.ConvertQryForAllUnion(BaseQry, "DBNamePrefix")
                Else
                    sQuery += BaseQry.Replace("DBNamePrefix", "")
                End If
                sQuery += ") x 
                ) 
                select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(sum(Quantity)/1000 as int) as Quantity  from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode "
                dtSale = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtSale IsNot Nothing AndAlso dtSale.Rows.Count > 0 Then
                'cvSale.Area.View.Palette = New CustomPalette()
                cvSale.Area.View.Palette = KnownPalette.Flower
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvSale.AreaDesign = CartesianArea1
                cvSale.Series.Clear()
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


                cvSale.ShowTitle = True
                cvSale.ChartElement.TitlePosition = TitlePosition.Top
                cvSale.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvSale.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvSale.Title = "AVERAGE MILK MARKETING ( TLPD )"

                Dim smartLabelsController As New SmartLabelsController()
                cvSale.Controllers.Add(smartLabelsController)
                cvSale.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvSale.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvSale.DataSource = ds
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

                    cvSale.Series.Add(lineSeries)

                Next
                cvSale.ShowLegend = True
                setOrientation(cvSale, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvSale.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)







                gvSale.DataSource = Nothing
                gvSale.Columns.Clear()
                gvSale.Rows.Clear()
                gvSale.GroupDescriptors.Clear()
                gvSale.MasterTemplate.SummaryRowsBottom.Clear()
                gvSale.ShowGroupPanel = False
                gvSale.EnableFiltering = False
                gvSale.AllowAddNewRow = False

                gvSale.GroupDescriptors.Clear()
                gvSale.TableElement.TableHeaderHeight = 40
                gvSale.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvSale.MasterTemplate.Columns.Add(repoComplete)
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
                        gvSale.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth")), gvSale.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))) Then ''1
                        gvSale.Rows.AddNew()
                        gvSale.Rows(gvSale.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtSale.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtSale.Rows(ii)("GrpName")), gvSale.Rows.Count - 1) ''2
                    End If
                    gvSale.Rows(arrRow.Item(clsCommon.myCstr(dtSale.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtSale.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtSale.Rows(ii)(strValue)) ''3
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "Avg"
                repoRate.Name = "TOTAL"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n0}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvSale.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvSale.Rows.Count - 1
                    For ii As Integer = 1 To gvSale.Columns.Count - 2
                        gvSale.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvSale.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvSale.Rows(jj).Cells(ii).Value)
                    Next
                    gvSale.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(gvSale.Rows(jj).Cells("Total").Value, gvSale.Columns.Count - 2)
                Next

                For ii As Integer = 0 To gvSale.Columns.Count - 1
                    gvSale.Columns(ii).ReadOnly = True
                    gvSale.Columns(ii).IsVisible = True
                    gvSale.Columns(ii).Width = 150
                Next

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
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
    Public Sub Load_Procurement()
        Try
            If dtProc Is Nothing OrElse dtProc.Rows.Count <= 0 Then
                Dim sQuery As String = " with CTERawData as ( 
                select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity from ("
                '                Dim BaseQry As String = "select (case when MONTH(DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE)>3 then Year(DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE) else Year(DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE)-1 end ) as YearCode ,MONTH(DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE) as MonthCode,format(DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE,'MMM') as [MonthName],DBNamePrefixTSPL_MILK_SRN_DETAIL.ACC_Qty as Quantity 
                'from DBNamePrefixTSPL_MILK_SRN_DETAIL
                'left outer join DBNamePrefixTSPL_MILK_SRN_Head on DBNamePrefixTSPL_MILK_SRN_Head.DOC_CODE=DBNamePrefixTSPL_MILK_SRN_DETAIL.DOC_CODE
                'where convert(date, DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date, DBNamePrefixTSPL_MILK_SRN_Head.DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"

                Dim BaseQry As String = "select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                from DBNamePrefixTSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                left outer join DBNamePrefixTSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
                union all
                select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                from DBNamePrefixTSPL_MILK_SHIFT_UPLOADER_DETAIL
                left outer join DBNamePrefixTSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
                union all
                select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                from DBNamePrefixTSPL_MILK_COLLECTION_DCS_DETAIL
                left outer join DBNamePrefixTSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_DCS.Status = 0 "
                If objCommonVar.RCDFCFP Then
                    sQuery += clsERPFuncationality.ConvertQryForAllUnion(BaseQry, "DBNamePrefix")
                Else
                    sQuery += BaseQry.Replace("DBNamePrefix", "")
                End If
                sQuery += ") x 
                ) 
                select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(sum(Quantity)/1000 as int) as Quantity  from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode "



                '                Dim sQuery As String = "with CTERawData as ( 
                'select YearCode,(CAST(YearCode as varchar)+'-'+cast((YearCode-1999) as varchar)) as YearName,case when MonthCode>3 then MonthCode-3 else MonthCode+9 end as MonthCode,[MonthName],Quantity from (select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from CHU.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join CHU.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from CHU.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join CHU.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from CHU.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join CHU.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BRN.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BRN.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BRN.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BRN.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BRN.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BRN.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from TNK.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join TNK.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from TNK.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join TNK.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from TNK.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join TNK.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from SWM.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join SWM.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from SWM.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join SWM.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from SWM.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join SWM.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from SKR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join SKR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from SKR.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join SKR.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from SKR.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join SKR.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from PLI.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join PLI.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from PLI.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join PLI.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from PLI.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join PLI.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from NAG.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join NAG.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from NAG.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join NAG.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from NAG.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join NAG.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from KTA.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join KTA.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from KTA.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join KTA.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from KTA.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join KTA.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JSL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join JSL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JSL.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join JSL.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from JSL.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join JSL.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JHL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join JHL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JHL.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join JHL.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from JHL.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join JHL.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JDH.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join JDH.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JDH.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join JDH.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from JDH.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join JDH.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JAL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join JAL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JAL.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join JAL.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from JAL.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join JAL.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from GNG.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join GNG.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from GNG.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join GNG.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from GNG.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join GNG.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document
                '_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BKN.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BKN.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BKN.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BKN.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BKN.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BKN.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BHR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BHR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BHR.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BHR.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BHR.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BHR.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BHL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BHL.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BHL.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BHL.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BHL.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BHL.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BAR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BAR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BAR.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BAR.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BAR.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BAR.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from ALW.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join ALW.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from ALW.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join ALW.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from ALW.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join ALW.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from AJM.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join AJM.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from AJM.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join AJM.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from AJM.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join AJM.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JPR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join JPR.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from JPR.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join JPR.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from JPR.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join JPR.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from UDP.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join UDP.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from UDP.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join UDP.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from UDP.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join UDP.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BANSWARA.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join BANSWARA.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from BANSWARA.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join BANSWARA.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from BANSWARA.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join BANSWARA.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from RJS.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join RJS.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from RJS.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join RJS.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from RJS.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join RJS.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024'
                'Union all
                'select  (case when MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)>3 then Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) else Year(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date) as MonthCode,format(TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,'MMM') as [MonthName],TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from CHITTORGARH.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
                'left outer join CHITTORGARH.dbo.TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)>3 then Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) else Year(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as MonthCode,format(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,'MMM') as [MonthName],TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight as Quantity 
                'from CHITTORGARH.dbo.TSPL_MILK_SHIFT_UPLOADER_DETAIL
                'left outer join CHITTORGARH.dbo.TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                'where convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103)<='31/Dec/2024'
                'union all
                'select (case when MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date)>3 then Year(TSPL_MILK_COLLECTION_DCS.Document_Date) else Year(TSPL_MILK_COLLECTION_DCS.Document_Date)-1 end ) as YearCode ,MONTH(TSPL_MILK_COLLECTION_DCS.Document_Date) as MonthCode,format(TSPL_MILK_COLLECTION_DCS.Document_Date,'MMM') as [MonthName],TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as Quantity 
                'from CHITTORGARH.dbo.TSPL_MILK_COLLECTION_DCS_DETAIL
                'left outer join CHITTORGARH.dbo.TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                'where convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='01/Apr/2024' and convert(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='31/Dec/2024') x 
                ') 
                'select max([MonthName]) as GrpMonth,YearCode as GrpCode,max(YearName) as GrpName,cast(sum(Quantity)/1000 as int) as Quantity  from CTERawData group by YearCode,MonthCode order by MonthCode,YearCode"

                dtProc = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtProc IsNot Nothing AndAlso dtProc.Rows.Count > 0 Then
                'cvProc.Area.View.Palette = New CustomPalette()
                cvProc.Area.View.Palette = KnownPalette.Flower
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvProc.AreaDesign = CartesianArea1
                cvProc.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtProc.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtProc.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtProc.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtProc.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtProc.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtProc.Rows(ii)("GrpMonth")
                    drTS("Value") = dtProc.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtProc.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next


                cvProc.ShowTitle = True
                cvProc.ChartElement.TitlePosition = TitlePosition.Top
                cvProc.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvProc.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvProc.Title = "AVERAGE MILK PROCUREMENT ( TKGPD )"

                Dim smartLabelsController As New SmartLabelsController()
                cvProc.Controllers.Add(smartLabelsController)
                cvProc.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProc.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProc.DataSource = ds
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

                    cvProc.Series.Add(lineSeries)

                Next
                cvProc.ShowLegend = True
                setOrientation(cvProc, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProc.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)







                gvProc.DataSource = Nothing
                gvProc.Columns.Clear()
                gvProc.Rows.Clear()
                gvProc.GroupDescriptors.Clear()
                gvProc.MasterTemplate.SummaryRowsBottom.Clear()
                gvProc.ShowGroupPanel = False
                gvProc.EnableFiltering = False
                gvProc.AllowAddNewRow = False

                gvProc.GroupDescriptors.Clear()
                gvProc.TableElement.TableHeaderHeight = 40
                gvProc.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpMonth"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvProc.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtProc.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtProc.Rows(ii)("GrpMonth"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtProc.Rows(ii)("GrpMonth"))
                        repoRate.Name = clsCommon.myCstr(dtProc.Rows(ii)("GrpMonth"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n0}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvProc.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtProc.Rows(ii)("GrpMonth")), gvProc.Columns.Count - 1)
                    End If
                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtProc.Rows(ii)("GrpName"))) Then ''1
                        gvProc.Rows.AddNew()
                        gvProc.Rows(gvProc.Rows.Count - 1).Cells("GrpMonth").Value = clsCommon.myCstr(dtProc.Rows(ii)("GrpName"))
                        arrRow.Add(clsCommon.myCstr(dtProc.Rows(ii)("GrpName")), gvProc.Rows.Count - 1) ''2
                    End If
                    gvProc.Rows(arrRow.Item(clsCommon.myCstr(dtProc.Rows(ii)("GrpName")))).Cells(arrColumn.Item(clsCommon.myCstr(dtProc.Rows(ii)("GrpMonth")))).Value = clsCommon.myCdbl(dtProc.Rows(ii)(strValue)) ''3
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "Avg"
                repoRate.Name = "TOTAL"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n0}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvProc.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvProc.Rows.Count - 1
                    For ii As Integer = 1 To gvProc.Columns.Count - 2
                        gvProc.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvProc.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvProc.Rows(jj).Cells(ii).Value)
                    Next
                    gvProc.Rows(jj).Cells("Total").Value = clsCommon.myCDivide(gvProc.Rows(jj).Cells("Total").Value, gvProc.Columns.Count - 2)
                Next

                For ii As Integer = 0 To gvProc.Columns.Count - 1
                    gvProc.Columns(ii).ReadOnly = True
                    gvProc.Columns(ii).IsVisible = True
                    gvProc.Columns(ii).Width = 150
                Next

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvProc_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvProc.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvProc_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvProc.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvSale_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvSale.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvSale_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvSale.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LegendElement_VisualItemCreating(sender As Object, e As LegendItemElementCreatingEventArgs)
        e.ItemElement = New CustomLegendItemElement(e.LegendItem)
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









