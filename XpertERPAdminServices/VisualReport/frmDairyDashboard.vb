'DashboardMilkUnion
Imports common
Imports Telerik.Charting
Public Class frmDairyDashboard
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim dtMPSummary As DataTable = Nothing
    Dim dtMPTopDCS As DataTable = Nothing

    Dim dtDBTSummary As DataTable = Nothing
    Dim dtDBTTopDCS As DataTable = Nothing
    Dim dtDBTTopMP As DataTable = Nothing

    Dim dtSaleMilk As DataTable = Nothing
    Dim dtSaleProduct As DataTable = Nothing
    Dim TabPrefix As String = ""

#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmMCCSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
            TabPrefix = objCommonVar.CurrentUnionDataBase + ".dbo."
        End If
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(RadButton1, "Press Alt+R Refresh ")

        txtToDate.Value = clsCommon.GETSERVERDATE()



        gvMPSummary.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvMPTopDCS.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        gvSaleMilk.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvSaleProduct.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill



        gvDBTTopMP.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvDBTTopDCS.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvDBTSummary.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        'cvZone.Views.AddNew()
        'Dim controllerZone As New DrillDownController()
        'cvZone.Controllers.Add(controllerZone)
        'cvZone.ShowDrillNavigation = False
        'cvZonePie.ShowDrillNavigation = False
        Reset()
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        lblQuality.Text = "CURRENT STATUS"

        dtMPSummary = Nothing
        gvMPSummary.DataSource = Nothing
        gvMPSummary.Rows.Clear()
        gvMPSummary.Columns.Clear()

        dtMPTopDCS = Nothing
        gvMPTopDCS.DataSource = Nothing
        gvMPTopDCS.Rows.Clear()
        gvMPTopDCS.Columns.Clear()

        dtDBTSummary = Nothing
        gvDBTSummary.DataSource = Nothing
        gvDBTSummary.Rows.Clear()
        gvDBTSummary.Columns.Clear()

        dtDBTTopDCS = Nothing
        gvDBTTopDCS.DataSource = Nothing
        gvDBTTopDCS.Rows.Clear()
        gvDBTTopDCS.Columns.Clear()

        dtDBTTopMP = Nothing
        gvDBTTopMP.DataSource = Nothing
        gvDBTTopMP.Rows.Clear()
        gvDBTTopMP.Columns.Clear()

        dtSaleMilk = Nothing
        gvSaleMilk.DataSource = Nothing
        gvSaleMilk.Rows.Clear()
        gvSaleMilk.Columns.Clear()

        dtSaleProduct = Nothing
        gvSaleProduct.DataSource = Nothing
        gvSaleProduct.Rows.Clear()
        gvSaleProduct.Columns.Clear()

        EnableDisableCntrl(True)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 3
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Sale Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Sale()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Procurement Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Milk_Procurement()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading DBT Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_DBT()

            clsCommon.ProgressBarPercentHide()
            EnableDisableCntrl(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetSaleBaseQuery(ByVal IsTaxable As Boolean) As String

        Dim qry As String = "select top 10 Item_Code,max(Short_Description) as Short_Description,cast( sum(Qty) as decimal(18,2)) as Qty,max(UOM) as UOM from (
select  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_UOM_DETAIL.UOM_Code as UOM,
case when isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 then 0 else (TSPL_SD_SHIPMENT_DETAIL.Qty*CFCurrentUOM.Conversion_Factor/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) end as Qty
from " + TabPrefix + "TSPL_SD_SHIPMENT_HEAD
left join " + TabPrefix + "TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
left join " + TabPrefix + "TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
left join " + TabPrefix + "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_UOM_DETAIL.Report_UOM=1
left join (select Conversion_Factor,Item_Code,UOM_Code from " + TabPrefix + "TSPL_ITEM_UOM_DETAIL)CFCurrentUOM on CFCurrentUOM.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code and CFCurrentUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
        If IsTaxable Then
            qry += "and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T'"
        Else
            qry += "and TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT'"
        End If
        qry += " )x group by Item_Code
order by Qty desc "
        Return qry
    End Function

    Public Sub Load_Sale()
        Try
            If dtSaleMilk Is Nothing OrElse dtSaleMilk.Rows.Count <= 0 Then
                dtSaleMilk = clsDBFuncationality.GetDataTable(GetSaleBaseQuery(False))
            End If


            If dtSaleMilk IsNot Nothing AndAlso dtSaleMilk.Rows.Count > 0 Then

                gvSaleMilk.DataSource = Nothing
                gvSaleMilk.Columns.Clear()
                gvSaleMilk.Rows.Clear()
                gvSaleMilk.GroupDescriptors.Clear()
                gvSaleMilk.MasterTemplate.SummaryRowsBottom.Clear()
                gvSaleMilk.ShowGroupPanel = False
                gvSaleMilk.EnableFiltering = False
                gvSaleMilk.AllowAddNewRow = False

                gvSaleMilk.GroupDescriptors.Clear()
                gvSaleMilk.TableElement.TableHeaderHeight = 40
                gvSaleMilk.MasterTemplate.ShowRowHeaderColumn = False
                gvSaleMilk.DataSource = dtSaleMilk

                gvSaleMilk.Columns("Short_Description").HeaderText = "Item"
                gvSaleMilk.Columns("Qty").HeaderText = "Qty"
                gvSaleMilk.Columns("UOM").HeaderText = "UOM"

                For ii As Integer = 0 To gvSaleMilk.Columns.Count - 1
                    gvSaleMilk.Columns(ii).ReadOnly = True
                    gvSaleMilk.Columns(ii).IsVisible = True
                    gvSaleMilk.Columns(ii).Width = 150
                Next
                gvSaleMilk.Columns("Item_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtSaleProduct Is Nothing OrElse dtSaleProduct.Rows.Count <= 0 Then
                dtSaleProduct = clsDBFuncationality.GetDataTable(GetSaleBaseQuery(True))
            End If

            If dtSaleProduct IsNot Nothing AndAlso dtSaleProduct.Rows.Count > 0 Then

                gvSaleProduct.DataSource = Nothing
                gvSaleProduct.Columns.Clear()
                gvSaleProduct.Rows.Clear()
                gvSaleProduct.GroupDescriptors.Clear()
                gvSaleProduct.MasterTemplate.SummaryRowsBottom.Clear()
                gvSaleProduct.ShowGroupPanel = False
                gvSaleProduct.EnableFiltering = False
                gvSaleProduct.AllowAddNewRow = False

                gvSaleProduct.GroupDescriptors.Clear()
                gvSaleProduct.TableElement.TableHeaderHeight = 40
                gvSaleProduct.MasterTemplate.ShowRowHeaderColumn = False
                gvSaleProduct.DataSource = dtSaleProduct


                gvSaleProduct.Columns("Short_Description").HeaderText = "Item"
                gvSaleProduct.Columns("Qty").HeaderText = "Qty"
                gvSaleProduct.Columns("UOM").HeaderText = "UOM"

                For ii As Integer = 0 To gvSaleProduct.Columns.Count - 1
                    gvSaleProduct.Columns(ii).ReadOnly = True
                    gvSaleProduct.Columns(ii).IsVisible = True
                    gvSaleProduct.Columns(ii).Width = 150
                Next
                gvSaleProduct.Columns("Item_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
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
    Private Sub cvZone_Drill(sender As Object, e As UI.DrillEventArgs)
        Try
            'Dim strSelectedValue As String = clsCommon.myCstr(e.SelectedPoint.DataItem.Row.ItemArray(0))
            'e.Cancel = True
            'cvZonePie.Series.Clear()

            'scZone.Panel1Collapsed = True
            'scZone.Panel2Collapsed = False

            'Dim dt As New DataTable
            'dt.Columns.Add("Name", GetType(String))
            'dt.Columns.Add("Value", GetType(Decimal))
            'For ii As Integer = 0 To gvZone.Rows.Count - 1
            '    If clsCommon.CompairString(clsCommon.myCstr(gvZone.Rows(ii).Cells("GrpCode").Value), strSelectedValue) = CompairStringResult.Equal Then
            '        For jj As Integer = 1 To gvZone.Columns.Count - 1
            '            Dim drTS As DataRow = dt.NewRow()
            '            drTS("Name") = gvZone.Columns(jj).HeaderText
            '            drTS("Value") = clsCommon.myCdbl(gvZone.Rows(ii).Cells(jj).Value)
            '            dt.Rows.Add(drTS)
            '        Next
            '        Exit For
            '    End If
            'Next
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    cvZonePie.ShowTitle = True
            '    cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
            '    cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
            '    cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
            '    cvZonePie.AreaType = ChartAreaType.Pie
            '    cvZonePie.ShowLegend = True
            '    cvZonePie.View.Margin = New Padding(0, 15, 0, 15)
            '    Me.cvZonePie.AreaType = ChartAreaType.Pie
            '    Dim series As New PieSeries()
            '    For Each dr As DataRow In dt.Rows
            '        series.DataPoints.Add(New PieDataPoint(clsCommon.myCdbl(dr("Value")), clsCommon.myCstr(dr("Name"))))
            '    Next
            '    series.ShowLabels = True
            '    Me.cvZonePie.Series.Add(series)

            '    'Dim strategy As New PieTwoLabelColumnsStrategy()
            '    'cvZonePie.ShowTitle = True
            '    'cvZonePie.Title = "Zone wise Sale For [" + strSelectedValue + "]"
            '    'cvZonePie.ChartElement.TitlePosition = TitlePosition.Top
            '    'cvZonePie.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
            '    'cvZonePie.AreaType = ChartAreaType.Pie
            '    'cvZonePie.ShowLegend = True
            '    'cvZonePie.View.Margin = New Padding(60, 0, 50, 0)
            '    'Dim series As New PieSeries()
            '    'series.Range = New AngleRange(270, 360)
            '    'series.LabelFormat = "{0:P2}"
            '    'series.RadiusFactor = 0.9F
            '    'series.ValueMember = "Value"
            '    'series.DataSource = dt
            '    'series.ShowLabels = True
            '    'series.DrawLinesToLabels = True
            '    'series.SyncLinesToLabelsColor = True
            '    'series.DisplayMember = "Name"
            '    'cvZonePie.Series.Add(series)

            '    'For Each item As LegendItem In Me.cvZonePie.ChartElement.LegendElement.Provider.LegendInfos
            '    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
            '    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
            '    '    item.Title = clsCommon.myCstr(row("Name"))
            '    'Next
            'End If
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
    Public Function GetDBTBaseQuery(ByRef FromDate As String, ByRef ToDate As String) As String
        Dim qry As String = "select top 1 from_date,To_Date from " + TabPrefix + "TSPL_DBT_NEFT where from_date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' order by from_date desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        FromDate = "01/Jan/2000"
        ToDate = "31/Jan/2000"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            FromDate = clsCommon.GetPrintDate(dt.Rows(0)("from_date"), "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(dt.Rows(0)("To_Date"), "dd/MMM/yyyy")
        End If

        qry = " Select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,
TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code ,(TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) as MP_Uploader_Code,TSPL_DBT_NEFT_DETAIL.MP_Name
,Format(TSPL_DBT_NEFT.From_Date,'MM-yyyy') As[Month],TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty,(TSPL_DBT_NEFT_DETAIL.Amount) as Amount 
from " + TabPrefix + "TSPL_DBT_NEFT_DETAIL 
inner join ( select * from ( select ROW_NUMBER() over(Partition by from_date order by UKID) as Rep,Document_Code,RCDF_Status,From_Date,To_Date from " + TabPrefix + "TSPL_DBT_NEFT where TSPL_DBT_NEFT.from_date='" + FromDate + "'
 )x where rep=1 )TSPL_DBT_NEFT on TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
Left Outer Join " + TabPrefix + "TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
left outer join " + TabPrefix + "TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
left outer join " + TabPrefix + "TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code  "
        Return qry
    End Function
    Public Sub Load_Report_DBT()
        Dim FromDate As String = ""
        Dim ToDate As String = ""
        Try

            If dtDBTSummary Is Nothing OrElse dtDBTSummary.Rows.Count <= 0 Then
                Dim sQuery As String = " select max([Month]) as [Month],count(distinct VLC_Code) as VLC_Code,max([BilledQty]) as BilledQty,count(distinct MP_Code) as MP_Code,  sum(Qty) as Qty,sum(Amount) as Amount
 from ( " + GetDBTBaseQuery(FromDate, ToDate) + "  )xx 
left outer join ( select sum([BilledQty]) as [BilledQty] from ( Select TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.* from (
select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code, (TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty)[BilledQty] from " + TabPrefix + "TSPL_DCS_MP_INCENTIVE_RECO_DETAIL
union all
select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.VLC_Code,(TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Qty)[Billed Qty] from " + TabPrefix + "TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID 
) as TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
Left Outer Join " + TabPrefix + "TSPL_DCS_MP_INCENTIVE_RECO_HEAD On TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
where Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)>=Convert(Date,'" + FromDate + "',103) And Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)<=Convert(Date,'" + ToDate + "',103)) x ) as TabReco on 1=1"
                dtDBTSummary = clsDBFuncationality.GetDataTable(sQuery)
            End If
            If dtDBTSummary IsNot Nothing AndAlso dtDBTSummary.Rows.Count > 0 Then
                gvDBTSummary.DataSource = Nothing
                gvDBTSummary.Columns.Clear()
                gvDBTSummary.Rows.Clear()
                gvDBTSummary.GroupDescriptors.Clear()
                gvDBTSummary.MasterTemplate.SummaryRowsBottom.Clear()
                gvDBTSummary.ShowGroupPanel = False
                gvDBTSummary.EnableFiltering = False
                gvDBTSummary.AllowAddNewRow = False

                gvDBTSummary.GroupDescriptors.Clear()
                gvDBTSummary.TableElement.TableHeaderHeight = 40
                gvDBTSummary.MasterTemplate.ShowRowHeaderColumn = False
                gvDBTSummary.DataSource = dtDBTSummary

                gvDBTSummary.Columns("Month").HeaderText = "Month"
                gvDBTSummary.Columns("VLC_Code").HeaderText = "DCS"
                gvDBTSummary.Columns("BilledQty").HeaderText = "DCS Qty"
                gvDBTSummary.Columns("MP_Code").HeaderText = "Farmer"
                gvDBTSummary.Columns("Qty").HeaderText = "Farmer Qty"
                gvDBTSummary.Columns("Amount").HeaderText = "Subsidy Amt"

                For ii As Integer = 0 To gvDBTSummary.Columns.Count - 1
                    gvDBTSummary.Columns(ii).ReadOnly = True
                    gvDBTSummary.Columns(ii).IsVisible = True
                    gvDBTSummary.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtDBTTopDCS Is Nothing OrElse dtDBTTopDCS.Rows.Count <= 0 Then
                Dim sQuery As String = "  select top 10  VLC_Code,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(VLC_Name) as VLC_Name , sum(Qty) as Qty,sum(Amount) as Amount
 from ( " + GetDBTBaseQuery(FromDate, ToDate) + " )xx  group by VLC_Code order by Qty desc"
                dtDBTTopDCS = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtDBTTopDCS IsNot Nothing AndAlso dtDBTTopDCS.Rows.Count > 0 Then

                gvDBTTopDCS.DataSource = Nothing
                gvDBTTopDCS.Columns.Clear()
                gvDBTTopDCS.Rows.Clear()
                gvDBTTopDCS.GroupDescriptors.Clear()
                gvDBTTopDCS.MasterTemplate.SummaryRowsBottom.Clear()
                gvDBTTopDCS.ShowGroupPanel = False
                gvDBTTopDCS.EnableFiltering = False
                gvDBTTopDCS.AllowAddNewRow = False

                gvDBTTopDCS.GroupDescriptors.Clear()
                gvDBTTopDCS.TableElement.TableHeaderHeight = 40
                gvDBTTopDCS.MasterTemplate.ShowRowHeaderColumn = False
                gvDBTTopDCS.DataSource = dtDBTTopDCS

                gvDBTTopDCS.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS"
                gvDBTTopDCS.Columns("VLC_Name").HeaderText = "DCS Name"
                gvDBTTopDCS.Columns("Qty").HeaderText = "Farmer Qty"
                gvDBTTopDCS.Columns("Amount").HeaderText = "Subsidy Amt"

                For ii As Integer = 0 To gvDBTTopDCS.Columns.Count - 1
                    gvDBTTopDCS.Columns(ii).ReadOnly = True
                    gvDBTTopDCS.Columns(ii).IsVisible = True
                    gvDBTTopDCS.Columns(ii).Width = 150
                Next
                gvDBTTopDCS.Columns("VLC_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtDBTTopMP Is Nothing OrElse dtDBTTopMP.Rows.Count <= 0 Then
                Dim sQuery As String = " select top 10  VLC_Code,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(VLC_Name) as VLC_Name,MP_Code,max(MP_Uploader_Code) as MP_Uploader_Code,max(MP_Name) as MP_Name , sum(Qty) as Qty,sum(Amount) as Amount
 from ( " + GetDBTBaseQuery(FromDate, ToDate) + "  )xx  group by VLC_Code,MP_Code order by Qty desc "
                dtDBTTopMP = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtDBTTopMP IsNot Nothing AndAlso dtDBTTopMP.Rows.Count > 0 Then

                gvDBTTopMP.DataSource = Nothing
                gvDBTTopMP.Columns.Clear()
                gvDBTTopMP.Rows.Clear()
                gvDBTTopMP.GroupDescriptors.Clear()
                gvDBTTopMP.MasterTemplate.SummaryRowsBottom.Clear()
                gvDBTTopMP.ShowGroupPanel = False
                gvDBTTopMP.EnableFiltering = False
                gvDBTTopMP.AllowAddNewRow = False

                gvDBTTopMP.GroupDescriptors.Clear()
                gvDBTTopMP.TableElement.TableHeaderHeight = 40
                gvDBTTopMP.MasterTemplate.ShowRowHeaderColumn = False
                gvDBTTopMP.DataSource = dtDBTTopMP

                gvDBTTopMP.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS"
                gvDBTTopMP.Columns("VLC_Name").HeaderText = "DCS Name"
                gvDBTTopMP.Columns("MP_Uploader_Code").HeaderText = "Farmer"
                gvDBTTopMP.Columns("MP_Name").HeaderText = "Farmer Name"
                gvDBTTopMP.Columns("Qty").HeaderText = "Farmer Qty"
                gvDBTTopMP.Columns("Amount").HeaderText = "Subsidy Amt"

                For ii As Integer = 0 To gvDBTTopMP.Columns.Count - 1
                    gvDBTTopMP.Columns(ii).ReadOnly = True
                    gvDBTTopMP.Columns(ii).IsVisible = True
                    gvDBTTopMP.Columns(ii).Width = 150
                Next
                gvDBTTopMP.Columns("MP_Code").IsVisible = False
                gvDBTTopMP.Columns("VLC_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    Private Function GetMPBaseQuery() As String
        Dim qry As String = "select TSPL_VENDOR_MASTER.Zone_Code,TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO ,TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight,TSPL_MILK_SHIFT_UPLOADER_DETAIL.FAT,TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNF,(TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight*TSPL_MILK_SHIFT_UPLOADER_DETAIL.FAT/100) as FATKG, (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight*TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNF/100) as SNFKG   
from " + TabPrefix + "TSPL_MILK_SHIFT_UPLOADER_DETAIL 
left outer join " + TabPrefix + "TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
left outer join " + TabPrefix + "TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
left outer join " + TabPrefix + "TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
where TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
        Return qry
    End Function

    Public Sub Load_Milk_Procurement()
        Try
            If dtMPSummary Is Nothing OrElse dtMPSummary.Rows.Count <= 0 Then
                Dim sQuery As String = "select  COUNT(distinct Zone_Code) as Zone_Code, COUNT(distinct BULK_ROUTE_NO) as BULK_ROUTE_NO,COUNT(distinct VLC_Code) as VLC_Code,sum(Milk_Weight) as Milk_Weight
,cast( (case when isnull(sum(Milk_Weight),0)=0 then 0 else (sum(FATKG)*100/sum(Milk_Weight)) end ) as decimal(18,1)) as FAT
,cast((case when isnull(sum(Milk_Weight),0)=0 then 0 else (sum(SNFKG)*100/sum(Milk_Weight)) end )  as decimal(18,1)) as SNF
from ( " + GetMPBaseQuery() + " ) xx"
                dtMPSummary = clsDBFuncationality.GetDataTable(sQuery)
            End If


            If dtMPSummary IsNot Nothing AndAlso dtMPSummary.Rows.Count > 0 Then
                lblQuality.Text = "SHORT SUMMARY of " + " " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + ""

                gvMPSummary.DataSource = Nothing
                gvMPSummary.Columns.Clear()
                gvMPSummary.Rows.Clear()
                gvMPSummary.GroupDescriptors.Clear()
                gvMPSummary.MasterTemplate.SummaryRowsBottom.Clear()
                gvMPSummary.ShowGroupPanel = False
                gvMPSummary.EnableFiltering = False
                gvMPSummary.AllowAddNewRow = False

                gvMPSummary.GroupDescriptors.Clear()
                gvMPSummary.TableElement.TableHeaderHeight = 40
                gvMPSummary.MasterTemplate.ShowRowHeaderColumn = False
                gvMPSummary.DataSource = dtMPSummary

                gvMPSummary.Columns("Zone_Code").HeaderText = "Zone"
                gvMPSummary.Columns("BULK_ROUTE_NO").HeaderText = "Routes"
                gvMPSummary.Columns("VLC_Code").HeaderText = "DCS"
                gvMPSummary.Columns("Milk_Weight").HeaderText = "Qty"
                gvMPSummary.Columns("FAT").HeaderText = "FAT"
                gvMPSummary.Columns("SNF").HeaderText = "SNF"

                For ii As Integer = 0 To gvMPSummary.Columns.Count - 1
                    gvMPSummary.Columns(ii).ReadOnly = True
                    gvMPSummary.Columns(ii).IsVisible = True
                    gvMPSummary.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtMPTopDCS Is Nothing OrElse dtMPTopDCS.Rows.Count <= 0 Then
                Dim sQuery As String = " select top 10 max(Zone_Code) as Zone_Code,max(BULK_ROUTE_NO) as BULK_ROUTE_NO,VLC_Code,max(VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(VLC_Name) as VLC_Name,sum(Milk_Weight) as Milk_Weight,cast( (case when isnull(sum(Milk_Weight),0)=0 then 0 else (sum(FATKG)*100/sum(Milk_Weight)) end ) as decimal(18,1)) as FAT
,cast((case when isnull(sum(Milk_Weight),0)=0 then 0 else (sum(SNFKG)*100/sum(Milk_Weight)) end )  as decimal(18,1)) as SNF  from ( " + GetMPBaseQuery() + " ) x group by VLC_Code order by Milk_Weight desc"
                dtMPTopDCS = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtMPTopDCS IsNot Nothing AndAlso dtMPTopDCS.Rows.Count > 0 Then
                lblQualitySummary.Text = "Top 10 DCS"

                gvMPTopDCS.DataSource = Nothing
                gvMPTopDCS.Columns.Clear()
                gvMPTopDCS.Rows.Clear()
                gvMPTopDCS.GroupDescriptors.Clear()
                gvMPTopDCS.MasterTemplate.SummaryRowsBottom.Clear()
                gvMPTopDCS.ShowGroupPanel = False
                gvMPTopDCS.EnableFiltering = False
                gvMPTopDCS.AllowAddNewRow = False

                gvMPTopDCS.GroupDescriptors.Clear()
                gvMPTopDCS.TableElement.TableHeaderHeight = 40
                gvMPTopDCS.MasterTemplate.ShowRowHeaderColumn = False
                gvMPTopDCS.DataSource = dtMPTopDCS

                gvMPTopDCS.Columns("Zone_Code").HeaderText = "Zone"
                gvMPTopDCS.Columns("BULK_ROUTE_NO").HeaderText = "Routes"
                gvMPTopDCS.Columns("VLC_Code").HeaderText = "DCS Code"
                gvMPTopDCS.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS"
                gvMPTopDCS.Columns("VLC_Name").HeaderText = "DCS Name"
                gvMPTopDCS.Columns("Milk_Weight").HeaderText = "Qty"
                gvMPTopDCS.Columns("FAT").HeaderText = "FAT"
                gvMPTopDCS.Columns("SNF").HeaderText = "SNF"

                For ii As Integer = 0 To gvMPTopDCS.Columns.Count - 1
                    gvMPTopDCS.Columns(ii).ReadOnly = True
                    gvMPTopDCS.Columns(ii).IsVisible = True
                    gvMPTopDCS.Columns(ii).Width = 150
                Next
                gvMPTopDCS.Columns("VLC_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvQuality_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvMPSummary.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQuality_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvMPSummary.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQualitySummary_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvMPTopDCS.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQualitySummary_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvMPTopDCS.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub


    Private Sub gvRMStock_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvSaleMilk.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMStock_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvSaleMilk.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvRMSupply_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvSaleProduct.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMSupply_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvSaleProduct.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvRMInPlant_RowFormatting(sender As Object, e As RowFormattingEventArgs)
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMInPlant_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs)
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

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
    Private Sub LegendElement_VisualItemCreating(sender As Object, e As LegendItemElementCreatingEventArgs)
        e.ItemElement = New CustomLegendItemElement(e.LegendItem)
    End Sub

    Private Sub gvDBTSummary_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvDBTSummary.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvDBTSummary_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvDBTSummary.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountVendor_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvDBTTopDCS.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountVendor_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvDBTTopDCS.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountCustomer_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvDBTTopMP.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvAccountCustomer_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvDBTTopMP.ViewCellFormatting

        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gvAccountVendor_RowFormatting_1(sender As Object, e As RowFormattingEventArgs) Handles gvDBTTopDCS.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

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
End Class