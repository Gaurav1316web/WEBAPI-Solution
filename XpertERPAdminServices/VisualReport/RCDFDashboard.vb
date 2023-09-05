Imports common
Imports Telerik.Charting
Public Class RCDFDashboard
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Dim dtFinishGoods As DataTable = Nothing
    Dim dtProduction As DataTable = Nothing

    Dim dtQuality As DataTable = Nothing
    Dim dtQualitySummary As DataTable = Nothing
    Dim dtQcPending As DataTable = Nothing

    Dim dtRMStock As DataTable = Nothing
    Dim dtRMSupply As DataTable = Nothing
    Dim dtRMInPlant As DataTable = Nothing

    Dim dtAccountVendor As DataTable = Nothing
    Dim dtAccountCustomer As DataTable = Nothing
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
        SetFromData()

        AddHandler cvProdution.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvProdution.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        AddHandler cvFinishGoods.ChartElement.LegendElement.VisualItemCreating, AddressOf LegendElement_VisualItemCreating
        gvFinishGoods.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        gvQuality.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvQualitySummary.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        gvRMStock.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvRMSupply.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvRMInPlant.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill


        gvAccountCustomer.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        gvAccountVendor.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        'cvZone.Views.AddNew()
        'Dim controllerZone As New DrillDownController()
        'cvZone.Controllers.Add(controllerZone)
        'cvZone.ShowDrillNavigation = False
        'cvZonePie.ShowDrillNavigation = False
        Reset()
    End Sub
    Private Sub txtToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtToDate.ValueChanged
        SetFromData()
    End Sub
    Private Sub SetFromData()
        txtFromDate.Value = txtToDate.Value.AddDays(-9)
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        lblQuality.Text = "CURRENT STATUS"
        dtFinishGoods = Nothing
        gvFinishGoods.DataSource = Nothing
        gvFinishGoods.Rows.Clear()
        gvFinishGoods.Columns.Clear()
        cvFinishGoods.Series.Clear()

        dtProduction = Nothing
        gvProdution.DataSource = Nothing
        gvProdution.Rows.Clear()
        gvProdution.Columns.Clear()
        cvProdution.Series.Clear()

        dtQuality = Nothing
        gvQuality.DataSource = Nothing
        gvQuality.Rows.Clear()
        gvQuality.Columns.Clear()

        dtQcPending = Nothing

        dtQualitySummary = Nothing
        gvQualitySummary.DataSource = Nothing
        gvQualitySummary.Rows.Clear()
        gvQualitySummary.Columns.Clear()

        dtRMStock = Nothing
        gvRMStock.DataSource = Nothing
        gvRMStock.Rows.Clear()
        gvRMStock.Columns.Clear()

        dtRMSupply = Nothing
        gvRMSupply.DataSource = Nothing
        gvRMSupply.Rows.Clear()
        gvRMSupply.Columns.Clear()

        dtRMInPlant = Nothing
        gvRMInPlant.DataSource = Nothing
        gvRMInPlant.Rows.Clear()
        gvRMInPlant.Columns.Clear()

        dtAccountVendor = Nothing
        gvAccountVendor.DataSource = Nothing
        gvAccountVendor.Rows.Clear()
        gvAccountVendor.Columns.Clear()

        dtAccountCustomer = Nothing
        gvAccountCustomer.DataSource = Nothing
        gvAccountCustomer.Rows.Clear()
        gvAccountCustomer.Columns.Clear()


        EnableDisableCntrl(True)
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 5
        clsCommon.ProgressBarPercentShow()
        Try
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading RAW MATERIAL Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Raw_Material()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading  FINISH GOODS Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Finish_Goods()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PRODUCTION Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_PRODUCTION()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading QUALITY Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Quality()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading ACCOUNT Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Load_Report_Account()

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
        txtLocation.Enabled = val
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
    Public Sub Load_Report_Account()
        Try
            If dtAccountVendor Is Nothing OrElse dtAccountVendor.Rows.Count <= 0 Then
                Dim qry As String = "Select  VCode, MAX(VName) as VName, SUM(CONVERT(DECIMAL(18,2),DrAmt)) as DrAmt, SUM( CONVERT(DECIMAL(18,2),CrAmt)) as CrAmt, SUM(CONVERT(DECIMAL(18,2),OpngBal))+  SUM(CONVERT(DECIMAL(18,2),CrAmt))- SUM(CONVERT(DECIMAL(18,2),DrAmt)) as [Closing]  from (

                        Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName,SUM(CrAmt)-SUM(DrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt From ( 


                          Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,   CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,CONVERT(DECIMAL(18,2),InnQuery.Purchase) AS Purchase,CONVERT(DECIMAL(18,2),InnQuery.Payments) AS Payments ,  InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end)  as  CrAmt ,
                         case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then 
                        case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then 
                        case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*    InnQuery.ConvRate) +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else 
                        case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else 
                        case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   InnQuery.ConvRate)  else case when (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else 
                        case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then 
                        case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt*  InnQuery.ConvRate)  else case when (DocType)<>'EXC' then  (DrAmt*  InnQuery.ConvRate)  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else 
                        case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt*  InnQuery.ConvRate)  else  (DrAmt*  InnQuery.ConvRate)  end end end as DrAmt, 
                         CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end + ( case when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END) as CrAmt,   case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code 
                         from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0   
                          and  convert(date,Invoice_Entry_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                         UNION ALL
                         Select * from ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,'WCT' as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, 0 as CrAmt,case when isnull(TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END  DrAmt,  Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  ) FinalWCt where (FinalWCt.CrAmt <>0 or FinalWCt.DrAmt <>0) 
                         and  convert(date,FinalWCt.DocDate ,103) <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   UNION ALL
                         Select  Reference_Doc_No,Emp_type,Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt, case when DocType='Pur.Invoice' then  CrAmt-DrAmt else 0 end as Purchase, 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (
                         select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN , TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end ) as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1
                         and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "') XXX UNION ALL
                         select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then  Actual_Total_TDS  else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN ('I','D') AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' UNION ALL
                         select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND 1=2
                          and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  UNION ALL
                         select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   UNION ALL
                         select      (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  
                         and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' UNION ALL
                         select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''
                         and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' UNION ALL
                          select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END as CrAmt, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Header.Adjustment_Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Header  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='P'  
                         and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103) <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  UNION ALL
                          select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as CrAmt,  CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='R'  
                         and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' UNION ALL
                         Select max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
                         select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                          and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   ) XX Group By XX.account, XX.DocNo UNION ALL
                         select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName,TSPL_PAYMENT_HEADER.Payment_No as DocNo,'EXC' as [DocType],convert(date,Payment_Date, 103) as DocDate, case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(TSPL_PAYMENT_HEADER.cheque_No+'-'+ CONVERT(VARCHAR,Cheque_Date , 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, 0 as Balance_Amount,isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_PAYMENT_HEADER.Comp_Code from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_Vendor_Master ON TSPL_Vendor_Master.Vendor_Code =TSPL_PAYMENT_HEADER.Vendor_Code 
                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code
                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_PAYMENT_HEADER.Payment_No 
                         LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
                         LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No  
                         where TSPL_PAYMENT_HEADER.Posted=1 and  TSPL_PAYMENT_HEADER.Payment_Type  in ('PY','AD') 
                         AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0)  
                          and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                         UNION ALL
                         Select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN,TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
                         'EXC'  as DocType  ,convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate,case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security ,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_PAYMENT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+ convert(varchar ,TSPL_PAYMENT_HEADER . Cheque_Date ,103)else '' end)) as ChequeDetails, TSPL_PAYMENT_HEADER.Currency_Code, 1 as ConvRate,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as DrAmt,  0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,
                         'RV',0, isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType,TSPL_PAYMENT_HEADER.Comp_Code 
                         from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_VENDOR_MASTER  ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code   
                         LEFT OUTER JOIN TSPL_Vendor_TYPE_MASTER  ON TSPL_Vendor_TYPE_MASTER.Ven_Type_Code  = TSPL_VENDOR_MASTER.Ven_Type_Code   
                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
                         LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.PAYMENT_NO =TSPL_BANK_REVERSE.Document_No 
                         LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
                         LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No 
                         where  TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P'
                         and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                        and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                        UNION ALL
                         Select  max(FinalQryForAdvance.Reference_Doc_No) as Reference_Doc_No,max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account 
                         , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code 
                         from (select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
                         left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code 
                         left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code
                         left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code
                         left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary 
                         where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   
                          and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103) <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance
                         group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code UNION ALL
                        Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         ) XX Group By XX.account, XX.DocNo
                         UNION ALL
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1
                         UNION ALL
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   ) InnQuery 
                          left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code   where InnQuery.DocNo not in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'

                         Union All
                         Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
                         ) )    ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo   where 1=1  ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in ('TDS','I','C','D','AV','OA','PY','MI','RV','Vendor','RC','PAE','AD') and CONVERT(date,DocDate ,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and DocType <>'Mislleneous' "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    qry += "and RIGHT(final.account,3)=substring('" + txtLocation.Value + "',1,3) "
                End If


                qry += "GROUP BY VCode
                             UNION ALL
                             Select VCode, MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VName, 0 as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt from (   Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate,   CONVERT(DECIMAL(18,2),InnQuery.CrAmt) AS CrAmt,CONVERT(DECIMAL(18,2),InnQuery.DrAmt) AS DrAmt,CONVERT(DECIMAL(18,2),InnQuery.Purchase) AS Purchase,CONVERT(DECIMAL(18,2),InnQuery.Payments) AS Payments ,  InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code from (Select InnQuery.Reference_Doc_No,InnQuery.Emp_type,InnQuery.Emp_Adv_Type,InnQuery.Due_Date, InnQuery.PO_SRN, InnQuery.VCode, InnQuery.VName, InnQuery.DocNo, InnQuery.DocType, InnQuery.DocDate, InnQuery.DocNarr, InnQuery.ChequeDetails, InnQuery.Currency_Code, InnQuery.ConvRate, InnQuery.CrAmt  *(case when (DocType)<>'EXC' then   InnQuery.ConvRate else 1 end)  as  CrAmt ,
                             case when len( (isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'')))<=0 then 
                            case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.account) then 
                            case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*    InnQuery.ConvRate) +isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0) else 
                            case when isnull((DrAmt),0)>0 AND (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) -isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) else isnull((DrAmt),0) end end else 
                            case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' then  (DrAmt*   InnQuery.ConvRate)  else case when (DocType)<>'EXC' then (DrAmt*   InnQuery.ConvRate) else CASE WHEN  isnull((DrAmt),0)>0 THEN isnull(DrAmt,0) ELSE isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  ),0) END end  end end else 
                            case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_PAYMENT_HEADER.Location_GL_Code,'') then 
                            case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND  (DocType)<>'EXC' then   (DrAmt*  InnQuery.ConvRate)  else case when (DocType)<>'EXC' then  (DrAmt*  InnQuery.ConvRate)  else isnull((TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT ),0) end end else 
                            case when isnull((TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (DrAmt*  InnQuery.ConvRate)  else  (DrAmt*  InnQuery.ConvRate)  end end end as DrAmt, 
                             CONVERT(DECIMAL(18,2),InnQuery.CrAmt-InnQuery.DrAmt) as Purchase, InnQuery.Payments, InnQuery.drNote, InnQuery.CrNote, InnQuery.Document_Type, InnQuery.Balance_Amount, InnQuery.account,InnQuery.Posting_Date, InnQuery.GLDocType, InnQuery.Comp_Code    from  ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.vendor_code as VCode,TSPL_VENDOR_INVOICE_HEAD.vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' else case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('I','C') Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end + ( case when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TSPL_VENDOR_INVOICE_HEAD.TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END) as CrAmt,   case when TSPL_VENDOR_INVOICE_HEAD.Document_Type IN('D') then Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end as DrAmt, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code 
                             from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0   
                              and  convert(date,Invoice_Entry_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                            "' and  convert(date,Invoice_Entry_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                            "'
                         UNION ALL
                         Select * from ( select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,'WCT' as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then case when TSPL_VENDOR_INVOICE_HEAD.Remarks <>'' then TSPL_VENDOR_INVOICE_HEAD.Remarks +' - ' else ''  end else ' - 'end) +(RefDocNo)+'Vendor Invoice No-' +Vendor_Invoice_No+' Date-' +Vendor_Invoice_Date  ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, 0 as CrAmt,case when isnull(TAX1 ,'')='WCT' THEN TAX1_Amt * -1  when isnull(TAX2 ,'')='WCT' THEN TAX2_Amt * -1 when isnull(TAX3 ,'')='WCT' THEN TAX3_Amt * -1 when isnull(TAX4 ,'')='WCT' THEN TAX4_Amt * -1 when isnull(TAX5 ,'')='WCT' THEN TAX5_Amt * -1 when isnull(TAX6 ,'')='WCT' THEN TAX6_Amt * -1 when isnull(TAX7 ,'')='WCT' THEN TAX7_Amt * -1 when isnull(TAX8 ,'')='WCT' THEN TAX8_Amt * -1 when isnull(TAX9 ,'')='WCT' THEN TAX9_Amt * -1 when isnull(TAX10 ,'')='WCT' THEN TAX10_Amt * -1 ELSE 0 END  DrAmt,  Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' Then document_total Else 0 End as Purchase, 0 as Payments, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then document_total Else 0 End as DrNote, Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then document_total Else 0 End as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type,balance_Amt as Balance_Amount, ( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No  ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, tspl_vendor_invoice_head.Comp_Code from tspl_vendor_invoice_head where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0  ) FinalWCt where (FinalWCt.CrAmt <>0 or FinalWCt.DrAmt <>0) 
                         and  convert(date,FinalWCt.DocDate ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,FinalWCt.DocDate ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' UNION ALL
                         Select  Reference_Doc_No,Emp_type,Emp_Adv_Type,Due_Date, PO_SRN, VCode, VName, DocNo, DocType, DocDate, DocNarr, ChequeDetails, Currency_Code, ConvRate, CrAmt, DrAmt, case when DocType='Pur.Invoice' then  CrAmt-DrAmt else 0 end as Purchase, 0 as Payments, 0 as drNote, 0 as CrNote, Document_Type, Balance_Amount, account, Posting_Date, GLDocType, Comp_Code from (
                         select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date ,CASE WHEN isnull(TSPL_PI_HEAD.Against_PO,'') <> '' THEN isnull(TSPL_PI_HEAD.Against_PO,'')  ELSE ''  + isnull(TSPL_PI_HEAD.Against_SRN,'') END  AS PO_SRN , TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No +' Date-' +Vendor_Invoice_Date)+ (SELECT ' & Items Are- '+STUFF((SELECT ', ' + t2.Item_Desc+'('+Convert(Varchar,COnvert(Decimal(18,2),t2.Item_Cost))+' Rs)' FROM TSPL_PI_DETAIL t2 WHERE t2.PI_No = TSPL_PI_HEAD.PI_No FOR XML PATH ('')),1,2,'')) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE, TSPL_VENDOR_INVOICE_HEAD.ConvRate, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end) as CrAmt, (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then TSPL_VENDOR_INVOICE_HEAD.Document_Total  - (case when TSPL_VENDOR_INVOICE_HEAD.GSTRegistered =0 or TSPL_VENDOR_INVOICE_HEAD.RCM=1 then  ( case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax1)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX1_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX2)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX2_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX3)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX3_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX4)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX4_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX5)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX5_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX6)='Y'   then TSPL_VENDOR_INVOICE_HEAD.TAX6_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX7)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX7_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX8)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX8_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.TAX9)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX9_Amt else 0 end +  case when len(isnull(TSPL_VENDOR_INVOICE_HEAD.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =TSPL_VENDOR_INVOICE_HEAD.Tax10)='Y'  then TSPL_VENDOR_INVOICE_HEAD.TAX10_Amt else 0 end ) else 0 end)  else 0 end ) as DrAmt, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt - (case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' and LEN(ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))>0 then (Select sum(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) from TSPL_TRANSFER_ORDER_HEAD where Status=1 and TSPL_TRANSFER_ORDER_HEAD.RMDA_Code in (select TSPL_SRN_HEAD.RMDA_No from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN where TSPL_PI_HEAD.PI_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  )) else 0 end ) as Balance_Amount, ( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType, TSPL_PI_HEAD.Comp_Code from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1
                         and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_PI_HEAD.PI_Date ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "') XXX UNION ALL
                         select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,TSPL_VENDOR_INVOICE_HEAD.Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, Case When TSPL_REMITTANCE.Document_Type IN ('I','C','D') Then TSPL_VENDOR_INVOICE_HEAD.Description Else TSPL_PAYMENT_HEADER.Entry_Desc End as DocNarr, '', Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE Else TSPL_PAYMENT_HEADER.CURRENCY_CODE End as Currency_Code, Case When TSPL_REMITTANCE.Document_Type in ('I','D','C') Then TSPL_VENDOR_INVOICE_HEAD.ConvRate Else TSPL_PAYMENT_HEADER.ConvRate End as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then  Actual_Total_TDS  else 0 END, case when TSPL_REMITTANCE.Document_Type ='I' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as Purchase, case when TSPL_REMITTANCE.Document_Type IN ('I','D') AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then 0 else Actual_Total_TDS END as Payments, case when TSPL_REMITTANCE.Document_Type ='D' AND TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount >0 then -1*Actual_Total_TDS else 0 END as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, Case When ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>'' Then TSPL_VENDOR_INVOICE_HEAD.Posting_Date Else TSPL_PAYMENT_HEADER.Payment_Post_Date End as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('C') then 'AP-CN' when TSPL_REMITTANCE.Document_Type in ('I') then 'AP-IN' when TSPL_REMITTANCE.Document_Type in ('D') then 'AP-DN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' UNION ALL
                         select '' as Reference_Doc_No, ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '', TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end AS CrAmt, 0 as DrAmt, 0 as Purchase, case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS*-1 else 0 end as Payments, 0 as DrNote, 0 as CrNote, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No inner join TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P' and Branch_GL_AC  is not null and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   AND 1=2
                          and  convert(date,TSPL_REMITTANCE.Document_Date  ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_REMITTANCE.Document_Date ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' UNION ALL
                         select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount ELSE -1*TSPL_BANK_REVERSE.Amount end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         and  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date  ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' UNION ALL
                         select      (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, VC_Code as VCode, VC_Name as VName, TSPL_VCGL_Head.Document_No as DocNo,case when TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, TSPL_VCGL_Head.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, (case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt, 0 as Purchase, 0 as Payments, case when Amount_Type='Cr' then Amount else 0 end as DrNote, case when Amount_Type='Dr' then Amount else 0 end as CrNote, (case when  TSPL_VCGL_Head.Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date,'VC-GL' as GLDocType,TSPL_VCGL_Head.Comp_Code from TSPL_VCGL_Head LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where TSPL_VCGL_Head.Document_Type='v' and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''  
                         and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'
                         UNION ALL
                         select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_VCGL_Detail.VCGL_Code as VCode, TSPL_VCGL_Detail.VCGL_Name as VName, TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, 0 as Purchase, 0 as Payments, TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as CrNote, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, (TSPL_VCGL_Head.Location_Segment) as account, TSPL_VCGL_Head.Posting_Date as Posting_Date ,'VC-GL' as GLDocType, TSPL_VCGL_Head.Comp_Code from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Against_VCGL=TSPL_VCGL_Head.Document_No where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,'')=''
                         and  convert(date,TSPL_VCGL_Head.Document_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' UNION ALL
                          select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No,  ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END as CrAmt, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Header.Adjustment_Amount ELSE 0 END  as DrAmt, 0 as Purchase, 0 as Payments, TSPL_Payment_Adjustment_Header.Adjustment_Amount as DrNote, 0 as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Header  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='P'  
                         and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'  UNION ALL
                          select       (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, ISNULL(TSPL_VENDOR_INVOICE_HEAD.Employee_Type ,'') AS Emp_type,'' as Emp_Adv_Type,NULL AS Due_Date, '' AS PO_SRN, TSPL_Payment_Adjustment_Header.Vendor_No as VCode, TSPL_Payment_Adjustment_Header.Vendor_Name as VName, TSPL_Payment_Adjustment_Header.Adjustment_No as DocNo, 'Adjustment' as DocType,CONVERT(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103) as DocDate,'Against Vendor Invoice No. ' +TSPL_Payment_Adjustment_Header.Doc_No as DocNarr,'' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate, CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')<>'D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as CrAmt,  CASE WHEN ISNULL(TSPL_VENDOR_INVOICE_HEAD.Document_Type,'')='D' THEN  TSPL_Payment_Adjustment_Detail.Amount ELSE 0 END as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, TSPL_Payment_Adjustment_Detail.Amount as CrNote, 'PAE' as Document_Type,0 as Balance_Amount, TSPL_VENDOR_INVOICE_HEAD.Loc_Code as account, TSPL_Payment_Adjustment_Header.Post_Date as Posting_Date ,'' as GLDocType, TSPL_Payment_Adjustment_Header.Comp_Code from  TSPL_Payment_Adjustment_Detail  left outer join  TSPL_Payment_Adjustment_Header on TSPL_Payment_Adjustment_Header.Adjustment_No =TSPL_Payment_Adjustment_Detail .Adjustment_No  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No  =TSPL_Payment_Adjustment_Header.Doc_No  where isnull(TSPL_Payment_Adjustment_Header.Is_Post,'') ='Y' and TSPL_Payment_Adjustment_Header.Adjust_Type='R'  
                         and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_Payment_Adjustment_Header.Adjustment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'UNION ALL
                         Select max(Reference_Doc_No) as Reference_Doc_No,max(Emp_type ) as Emp_type,max(Emp_Adv_Type) as Emp_Adv_Type ,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
                         select  '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then case when isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code,'')='' then TSPL_PAYMENT_HEADER.Location_GL_Code else TSPL_VENDOR_INVOICE_HEAD.Loc_Code end When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)<>1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                          and  convert(date,TSPL_PAYMENT_HEADER.payment_date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_PAYMENT_HEADER.payment_date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "') XX Group By XX.account, XX.DocNo UNION ALL
                         select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName,TSPL_PAYMENT_HEADER.Payment_No as DocNo,'EXC' as [DocType],convert(date,Payment_Date, 103) as DocDate, case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(TSPL_PAYMENT_HEADER.cheque_No+'-'+ CONVERT(VARCHAR,Cheque_Date , 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, 0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, 0 as Balance_Amount,isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_PAYMENT_HEADER.Comp_Code from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_Vendor_Master ON TSPL_Vendor_Master.Vendor_Code =TSPL_PAYMENT_HEADER.Vendor_Code 
                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code
                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_PAYMENT_HEADER.Payment_No 
                         LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
                         LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No  
                         where TSPL_PAYMENT_HEADER.Posted=1 and  TSPL_PAYMENT_HEADER.Payment_Type  in ('PY','AD') 
                         AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1 and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0)  
                          and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'
                         UNION ALL
                         Select '' as Reference_Doc_No,ISNULL(TSPL_PAYMENT_HEADER.Employee_Type ,'') AS Emp_type,ISNULL(TSPL_PAYMENT_HEADER.Employee_Advance_Type ,'') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN,TSPL_VENDOR_MASTER.Vendor_Code as VCode, TSPL_VENDOR_MASTER.Vendor_Name as VName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
                         'EXC'  as DocType  ,convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) as DocDate,case when ISNULL(TSPL_PAYMENT_HEADER.Is_Security ,0)=1 Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr,(rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_PAYMENT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+ convert(varchar ,TSPL_PAYMENT_HEADER . Cheque_Date ,103)else '' end)) as ChequeDetails, TSPL_PAYMENT_HEADER.Currency_Code, 1 as ConvRate,  TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT as DrAmt,  0 as Purchase, 0 as Payments, 0 as DrNote, 0 as CrNote,
                         'RV',0, isnull(TSPL_VENDOR_INVOICE_HEAD.Loc_Code ,'') as account, TSPL_PAYMENT_HEADER.Payment_Date as Posting_Date,'RV-TA' as GLDocType,TSPL_PAYMENT_HEADER.Comp_Code 
                         from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_VENDOR_MASTER  ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_BANK_REVERSE.Vendor_Code   
                         LEFT OUTER JOIN TSPL_Vendor_TYPE_MASTER  ON TSPL_Vendor_TYPE_MASTER.Ven_Type_Code  = TSPL_VENDOR_MASTER.Ven_Type_Code   
                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
                         LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.PAYMENT_NO =TSPL_BANK_REVERSE.Document_No 
                         LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No and TSPL_PAYMENT_DETAIL.Payment_Line_No =1 
                         LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No 
                         where  TSPL_BANK_REVERSE.Reverse_Document='Payments' AND TSPL_BANK_REVERSE.Post ='P'
                         and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                        and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'
                         UNION ALL
                         Select  max(FinalQryForAdvance.Reference_Doc_No) as Reference_Doc_No,max(FinalQryForAdvance.Emp_type) as Emp_type,max(FinalQryForAdvance.Emp_Adv_Type) as Emp_Adv_Type,null as Due_Date ,max(FinalQryForAdvance.PO_SRN) as PO_SRN, max(FinalQryForAdvance.VCode) as VCode,max(FinalQryForAdvance.VName) as VName,FinalQryForAdvance.DocNo,max(FinalQryForAdvance.DocType) as DocType,max(FinalQryForAdvance.DocDate) as DocDate, max(FinalQryForAdvance.DocNarr) as DocNarr, max(FinalQryForAdvance.ChequeDetails) as ChequeDetails, max(FinalQryForAdvance.CURRENCY_CODE) as CURRENCY_CODE, max(FinalQryForAdvance.ConvRate) as ConvRate,sum(CrAmt) as CrAmt,sum(DrAmt) as DrAmt, sum(Purchase) as Purchase, sum(Payments) as Payments, sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(FinalQryForAdvance.Document_Type) as Document_Type ,0 as Balance_Amount, max(FinalQryForAdvance.account) as account 
                         , max(FinalQryForAdvance.Posting_Date) as Posting_Date,max(FinalQryForAdvance.GLDocType) as GLDocType,max(FinalQryForAdvance.Comp_Code) as Comp_Code 
                         from (select    (case when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No  ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No when isnull(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL   ,'')<>'' then TSPL_VENDOR_INVOICE_HEAD.Against_VCGL  else '' end )as Reference_Doc_No, TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, '' AS Emp_type, 'AAS' as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  as VCode, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'AAS' as DocType,CONVERT(Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) as DocDate, TSPL_VENDOR_INVOICE_HEAD.Remarks as DocNarr, '' as ChequeDetails, 'INR' as CURRENCY_CODE, 1 as ConvRate,case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrAmt,case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end as DrAmt, 0 as Purchase, 0 as Payments, case when TSPL_VENDOR_INVOICE_DETAIL.Amount >0 then TSPL_VENDOR_INVOICE_DETAIL.Amount else 0 end  as DrNote, case when TSPL_VENDOR_INVOICE_DETAIL.Amount <0 then TSPL_VENDOR_INVOICE_DETAIL.Amount*-1  else 0 end as CrNote, TSPL_VENDOR_INVOICE_HEAD.Document_Type as Document_Type ,0 as Balance_Amount, (TSPL_VENDOR_INVOICE_HEAD.Loc_Code ) as account, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Posting_Date,case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'AP-DN' when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'AP-CN' else 'AP-IN' end as GLDocType,TSPL_VENDOR_INVOICE_HEAD.Comp_Code from TSPL_VENDOR_INVOICE_DETAIL  LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No 
                         left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code 
                         left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Group_Code
                         left outer join  TSPL_GL_ACCOUNTS AS GL1 ON GL1.account_code=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code
                         left outer join  TSPL_GL_ACCOUNTS AS GL2 ON GL2.account_code=TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary 
                         where  ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''   
                          and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and TSPL_VENDOR_MASTER.Vendor_Group_Code  ='EMPLOYEES' and GL1.Account_seg_code1=GL2.Account_seg_code1) FinalQryForAdvance
                         group by FinalQryForAdvance.DocNo,FinalQryForAdvance.GL_Account_Code UNION ALL
                        Select max(Reference_Doc_No) as Reference_Doc_No, MAX(Emp_type) as Emp_type, 'S' as Emp_Adv_Type,NULL as Due_Date, MAX(PO_SRN) as PO_SRN, MAX(VCode) as VCode, MAX(VName) as VName, DocNo, MAX(DocType) as DocType, MAX(DocDate) as DocDate, MAX(DocNarr) as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(CrAmt) as CrAmt, SUM(DrAmt) DrAmt, 0 as Purchase, SUM(DrAmt)-SUM(CrAmt) as Payments, 0 as DrNote, 0 as CrNote, MAX(Document_Type) as Document_Type, SUM(Balance_Amount) as Balance_Amount, account, MAX(Posting_Date) as Posting_Date, MAX(GLDocType) as GLDocType, MAX(Comp_Code) as Comp_Code from (
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'') as Emp_Adv_Type,NULL AS Due_Date,'' AS PO_SRN, tspl_payment_header.vendor_code as VCode, tspl_payment_header.vendor_name as VName, TSPL_PAYMENT_HEADER.payment_no as DocNo, case when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' else 'Mislleneous' end as DocType, COnvert(date,payment_date, 103) as DocDate, ((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr, ((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.payment_type in ('RC') then Payment_Amount else 0 end as CrAmt, case when TSPL_PAYMENT_HEADER.payment_type IN('OA','AV') then Payment_Amount When TSPL_PAYMENT_HEADER.payment_type IN('PY') Then (Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then -TSPL_PAYMENT_DETAIL.Applied_Amount Else TSPL_PAYMENT_DETAIL.Applied_Amount End) else 0 end as DrAmt, TSPL_PAYMENT_HEADER.Payment_Type as Document_Type,Payment_Amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right(TSPL_BANK_MASTER.BANKACC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when TSPL_PAYMENT_HEADER.Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType, tspl_payment_header.Comp_Code from tspl_payment_header LEFT OUTER JOIN TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No Where tspl_payment_header.Payment_Type NOT IN ('MI','AD') AND (Posted='P' or Posted='1') AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   
                         ) XX Group By XX.account, XX.DocNo
                         UNION ALL
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type  ,'S') as Emp_Adv_Type,TSPL_PAYMENT_HEADER.Payment_Date As Due_Date, '' AS PO_SRN, TSPL_REMITTANCE.Vendor_Code, TSPL_REMITTANCE.Vendor_Name, TSPL_REMITTANCE.Document_No, 'TDS' as [DocType], convert(date,Document_Date,103)as Document_Date, TSPL_PAYMENT_HEADER.Entry_Desc as DocNarr, '', TSPL_PAYMENT_HEADER.CURRENCY_CODE as Currency_Code, TSPL_PAYMENT_HEADER.ConvRate as ConvRate, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then 0 else Actual_Total_TDS END as CrAmt, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else 0 END as DrAmt, 0 as Purchase, case when TSPL_REMITTANCE.Document_Type IN('OA','AV') then Actual_Total_TDS else Actual_Total_TDS END as Payments, 0 as DrNotes, 0 as CrNotes, 'TDS',0, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code Else right( Branch_GL_AC,3) End as account, TSPL_PAYMENT_HEADER.Payment_Post_Date as Posting_Date, case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end as GLDocType, TSPL_REMITTANCE.Comp_Code from TSPL_REMITTANCE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1
                         UNION ALL
                         select '' as Reference_Doc_No, ISNULL(tspl_payment_header.Employee_Type ,'') AS Emp_type,ISNULL(tspl_payment_header.Employee_Advance_Type ,'S') as Emp_Adv_Type,null AS Due_Date, '' AS PO_SRN, TSPL_BANK_REVERSE.vendor_code as VCode, TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate, TSPL_BANK_REVERSE.Document_No+case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then ', '+TSPL_PAYMENT_DETAIL.Document_No Else '' End as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+ CONVERT(VARCHAR,Pay_Rec_Date, 103))as ChequeDetails, TSPL_PAYMENT_HEADER.CURRENCY_CODE, TSPL_PAYMENT_HEADER.ConvRate, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt, 0 as Purchase, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount when TSPL_PAYMENT_HEADER.Payment_Type='PY' then TSPL_PAYMENT_DETAIL.Applied_Amount*-1 ELSE TSPL_BANK_REVERSE.Amount*-1 end as Payments, 0 as DrNote, 0 as CrNote, 'RV'as Document_Type, amount as Balance_Amount, Case When TSPL_PAYMENT_HEADER.Payment_Type in ('AV','OA','RC') Then TSPL_PAYMENT_HEADER.Location_GL_Code When TSPL_PAYMENT_HEADER.Payment_Type in ('PY') Then TSPL_VENDOR_INVOICE_HEAD.Loc_Code Else right(BANKACC,3) End as account, TSPL_BANK_REVERSE.Reversal_Date as Posting_Date,'RV-TA' as GLDocType, TSPL_BANK_REVERSE.Comp_Code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No LEFT OUTER JOIN TSPL_PAYMENT_DETAIL ON TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_DETAIL.Document_No left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And TSPL_BANK_REVERSE.Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y' AND ISNULL(TSPL_PAYMENT_HEADER.Advance_Against_Salary,0)=1 AND ISNULL(TSPL_PAYMENT_HEADER.Is_Security,0)<>1   ) InnQuery 
                          left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No  =InnQuery.DocNo LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code   where InnQuery.DocNo not in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'
                         Union All
                         Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Payment_No  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_Type  ='AD' and   TSPL_PAYMENT_HEADER.Posted='1' and (TSPL_PAYMENT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_PAYMENT_HEADER.EXCHANGE_GAIN_AMT >0)    and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'
                         ) )    ) InnQuery left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =InnQuery.VCode left outer join TSPL_VENDOR_INVOICE_HEAD on  TSPL_VENDOR_INVOICE_HEAD.Document_No =InnQuery.DocNo   where 1=1  ) Final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_GROUP.Acct_Set_Code=TSPL_VENDOR_ACCOUNT_SET.Acct_set_Code LEFT OUTER JOIN TSPL_COMPANY_MASTER ON final.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code where final.Document_Type in ('TDS','I','C','D','AV','OA','PY','MI','RV','Vendor','RC','PAE','AD') and CONVERT(date,DocDate ,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "' and CONVERT(date,DocDate ,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                                        "'and DocType <>'Mislleneous' "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    qry += "and RIGHT(final.account,3)=substring('" + txtLocation.Value + "',1,3)"
                End If
                qry += "GROUP BY VCode
 ) XXX GROUP BY VCode ORDER BY VCode"
                dtAccountVendor = clsDBFuncationality.GetDataTable(qry)
            End If


            If dtAccountVendor IsNot Nothing AndAlso dtAccountVendor.Rows.Count > 0 Then
                lblvendor.Text = "VENDOR LEDGER"

                gvAccountVendor.DataSource = Nothing
                gvAccountVendor.Columns.Clear()
                gvAccountVendor.Rows.Clear()
                gvAccountVendor.GroupDescriptors.Clear()
                gvAccountVendor.MasterTemplate.SummaryRowsBottom.Clear()
                gvAccountVendor.ShowGroupPanel = False
                gvAccountVendor.EnableFiltering = False
                gvAccountVendor.AllowAddNewRow = False

                gvAccountVendor.GroupDescriptors.Clear()
                gvAccountVendor.TableElement.TableHeaderHeight = 35
                gvAccountVendor.MasterTemplate.ShowRowHeaderColumn = False
                gvAccountVendor.DataSource = dtAccountVendor
                gvAccountVendor.Columns("VCode").HeaderText = "Account Code"
                gvAccountVendor.Columns("VName").HeaderText = "Account Name"
                gvAccountVendor.Columns("DrAmt").HeaderText = "Debit Amount"
                gvAccountVendor.Columns("CrAmt").HeaderText = "Credit Amount"
                gvAccountVendor.Columns("Closing").HeaderText = "Closing"
                For ii As Integer = 0 To gvAccountVendor.Columns.Count - 1
                    gvAccountVendor.Columns(ii).ReadOnly = True
                    gvAccountVendor.Columns(ii).IsVisible = True
                    gvAccountVendor.Columns(ii).Width = 150
                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Try
            If dtAccountCustomer Is Nothing OrElse dtAccountCustomer.Rows.Count <= 0 Then
                Dim strqry As String = " select ACode, AName,DrAmt,CrAmt,BalAmt from (Select ACode,  MAX(AName) as AName, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt
  ,( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt  From (
                                         Select  MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code,case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate, convert(decimal(18,2),InnQuery.DrAmt) as DrAmt,convert(decimal(18,2),InnQuery.CrAmt) as CrAmt,convert(decimal(18,2),InnQuery.Sales) as Sales,case when InnQuery.DocType='IM' then case when InnQuery.CrAmt>0 then  convert(decimal(18,2),InnQuery.CrAmt) else convert(decimal(18,2),InnQuery.DrAmt) * -1 end  else convert(decimal(18,2),InnQuery.CollectionRefund) end  as CollectionRefund , InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,InnQuery .EXCHANGE_GAIN_AMT ,InnQuery .EXCHANGE_LOSS_AMT   from (Select InnQuery.ACode AS ACode,InnQuery.Child  , InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ,
                                        case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then 
                                         case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt*  InnQuery. ConvRate) else 
                                         case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt*  InnQuery. ConvRate) when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate) WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate)  else 0 end end else 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else (CrAmt*  InnQuery. ConvRate)  end end else 
                                         case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt*  InnQuery. ConvRate) +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt*  InnQuery. ConvRate) -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else  (CrAmt*  InnQuery. ConvRate)  end end end as CrAmt, 
                                         InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (SELECT 
                                         ACode AS ACode,max(Child) as Child, 
                                        MAX(AName) AS AName,MAX(DocNo) AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType  ,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt) AS DrAmt, SUM(CrAmt) AS CrAmt, SUM(SecurityDrAmt) as SecurityDrAmt, SUM(SecurityCrAmt) as SecurityCrAmt, SUM(Sales) as Sales,case when MAX(DocType)='IM' then case when SUM(CrAmt)>0 then  SUM(CrAmt) else  0 end  else  Sum(CollectionRefund) end  as CollectionRefund , SUM(DrNote) as DrNote,case when MAX(DocType)='IM' then 0 else  Sum(CrNote) end  as CrNote, Location AS Location, 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code, MAX(Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='S' THEN 'Security' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='C' THEN 'Crate Security' ELSE  CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='R' THEN 'Refrigerator' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='O' THEN 'Others' END END END END Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,  Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as DrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (CASE WHEN   TSPL_RECEIPT_HEADER.Receipt_Type='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' THEN TSPL_RECEIPT_DETAIL.Applied_Amount  WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS SecurityCrAmt, 0 as [Sales], case when TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') Then Receipt_Amount*-1 When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then TSPL_RECEIPT_DETAIL.Applied_Amount WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount Else Receipt_Amount end as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_Customer_Invoice_Head.Loc_Code  WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN TSPL_RECEIPT_HEADER.Location_GL_Code ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END  ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END as [Location], (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Case When TSPL_RECEIPT_HEADER.Receipt_Type='R' Then TSPL_Customer_Invoice_Head.Customer_Code Else TSPL_RECEIPT_HEADER.Cust_Code End LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y'   and  TSPL_RECEIPT_HEADER.Receipt_Type not in ('M','A','U','K') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'   and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                                         UNION ALL 
                                          Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
                                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_RECEIPT_HEADER.Receipt_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
                                         where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('R','A') 
                                         AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                                         UNION ALL 
                                         Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, ''  as AgainstInvoiceNo, Reversal_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_BANK_REVERSE.Source_Type As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_BANK_REVERSE  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
                                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
                                         where  TSPL_BANK_REVERSE.Reverse_Document='Receipts' AND TSPL_BANK_REVERSE.Post ='P'
                                         and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                                         UNION ALL 
                                         Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when RD.Receipt_Type='C' then 0 else RD.Applied_Amount  end as DrAmt,case when RD.Receipt_Type='C' then RD.Applied_Amount  else 0  end  AS CrAmt, Receipt_Amount as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],     CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE  CASE WHEN (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt )<>''  THEN  (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt) ELSE  substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END END AS [Location],  
                                         (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON TSPL_RECEIPT_HEADER.Receipt_No =RD.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND CIH.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' 
                                          UNION ALL 
                                         SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  )INV  GROUP BY  DocNo,Location  
                                         UNION ALL 
                                         SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM'  WHEN TSPL_RECEIPT_HEADER.Receipt_Type='K' THEN 'KN' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount *-1  end  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then TSPL_RECEIPT_DETAIL.Applied_Amount else 0 end  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' when TSPL_RECEIPT_HEADER.Receipt_Type='K' then 'AR-KN' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y'  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'K'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   )INV  GROUP BY  DocNo,Location 
                                          UNION ALL 
                                          Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child,TSPL_RECEIPT_HEADER.Customer_Name as AName, (Select r1.Receipt_No from TSPL_RECEIPT_HEADER r1 where r1 .UnApplied_No  =TSPL_RECEIPT_HEADER.Receipt_No)  as DocNo,'' as AgainstInvoiceNo,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate,'RC' as DocType,'' as DocNarr,(rtrim(TSPL_RECEIPT_HEADER.Entry_Desc) + (case when len(RTRIM(TSPL_RECEIPT_HEADER.Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_RECEIPT_HEADER.Cheque_No +  ' - '+TSPL_RECEIPT_HEADER.Cheque_Date else '' end)) as ChequeDetails, RH.Currency_Code, RH.ConvRate, 0 as DrAmt,  TSPL_RECEIPT_HEADER.Receipt_Amount  AS CrAmt, 0 as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 End  AS SecurityCrAmt, 0 as [Sales], TSPL_RECEIPT_HEADER.Receipt_Amount as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], Right(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location],  'AR-RC' as SourceCode, '' as Item_Code, '' as Item_Desc, TSPL_RECEIPT_HEADER.Receipt_Type As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_HEADER RH ON TSPL_RECEIPT_HEADER.Receipt_No =RH.UnApplied_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON RH.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('U') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   )XX GROUP BY XX.ACode, XX.Location, XX.DocNo,XX.DocType 
                                          UNION ALL
                                         Select ACode,Child, AName, DocNo, AgainstInvoiceNo, DocDate, DocType, Narration, ChequeDetails, CURRENCY_CODE, ConvRate, DrAmt, CrAmt, SecurityDrAmt, SecurityCrAmt, Sales, Collectionrefund, DrNote, CrNote, Location, SourceCode, Item_Code, Item_Desc, Receipt_Type, Bank_Code, Cust_Type_Code, Cust_Type_Desc, Cust_Category_Code, CUST_CATEGORY_DESC from (
                                         Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child, CIH.Customer_Code, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, '' as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then RD.Applied_Amount Else 0 end as DrAmt, 0 as CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 end as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales, 0 as Collectionrefund, TSPL_RECEIPT_HEADER.Receipt_Amount as DrNote, 0 as CrNote, 
                                          CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC, ROW_NUMBER() OVER (Partition By TSPL_RECEIPT_HEADER.Receipt_No ORDER BY TSPL_RECEIPT_HEADER.Receipt_No) as RowNo  from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
                                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A'  AND ISNULL(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   ) XX 
                                         UNION ALL
                                         Select max(ACode) as ACode ,max(Child) as Child , max(AName) as AName, max(DocNo) as DocNo, max(AgainstInvoiceNo) as AgainstInvoiceNo,max( DocDate) as DocDate, max(DocType) as DocType, max(Narration) as Narration, max(ChequeDetails) as Narration, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate, Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt, sum(SecurityDrAmt) as SecurityDrAmt, sum(SecurityCrAmt) as SecurityCrAmt, sum(Sales) as Sales, Sum(Collectionrefund) as Collectionrefund, Sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(Location) as Location, max(SourceCode) as SourceCode, max(Item_Code) as Item_Code,max( Item_Desc) as Item_Desc, max(Receipt_Type) as Receipt_Type , max(Bank_Code) as Bank_Code, max(Cust_Type_Code) as Cust_Type_Code, max(Cust_Type_Desc) as Cust_Type_Desc, max(Cust_Category_Code) as Cust_Category_Code,max( CUST_CATEGORY_DESC) as  CUST_CATEGORY_DESC from (
                                         Select  isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code )  as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='A' then RD.Child_Cust_Code else CIH.Customer_Code end as Child, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, CIH.Document_No as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, 0 as DrAmt, RD.Applied_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales,RD.Applied_Amount as CollectionRefund,0 as DrNote, 0 as CrNote,
                                         CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC   from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
                                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A' AND isnull(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  )z group by DocNo ,Location,ACode
                                          UNION ALL
                                         SELECT max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as ACode,max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as Child,max( TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,'' as AgainstInvoiceNo,  CONVERT(DATE,MAX(Final.Adjustment_Date),103) AS DocDate, 'AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost)*-1 else SUM(Final.Cost) end as Collectionrefund, 0 as [drNote], 0 as [CrNote], max(Final.Location) as [Location], Max(SourceCode)as SourceCode, Item_Code, MAX(Item_Description) as Item_Desc  ,MAX(Receipt_Type) As Receipt_Type, '' as Bank_Code,MAX(Cust_Type_Code) as Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No,  TSPL_ADJUSTMENT_HEADER.Adjustment_Date,  TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN  TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost,  TSPL_ADJUSTMENT_HEADER.Document_No , TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type ,'IC-AD' as SourceCode, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description  ,'' As Receipt_Type , TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    FROM TSPL_ADJUSTMENT_HEADER  LEFT OUTER JOIN  TSPL_ADJUSTMENT_DETAIL ON  TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left OUTER JOIN  TSPL_LOCATION_MASTER on  TSPL_ADJUSTMENT_DETAIL.Location_Code= TSPL_LOCATION_MASTER.Location_Code inner join  tspl_customer_master on  tspl_adjustment_header.customer_code= tspl_customer_master.cust_code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   WHERE ( ( TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND ( TSPL_ADJUSTMENT_HEADER.Document_No= '' and  TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') or TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and  TSPL_ADJUSTMENT_HEADER.Posted='Y'   and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  ) AS Final INNER JOIN  TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No =  TSPL_ADJUSTMENT_HEADER.Adjustment_No  GROUP BY Final.Adjustment_No, Final.Item_Code
                                         UNION ALL
                                        select TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.Customer_Code as Child,TSPL_Customer_Invoice_Head.Customer_Name , TSPL_Customer_Invoice_Head.Document_No  AS DocNo, CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN  CASE WHEN LEN(ISNULL( (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No),''))>0 THEN (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) ELSE (Select Top 1 ISNULL(TSPL_INVOICE_MASTER_BULKSALE.Document_No,'')  FROM TSPL_INVOICE_MASTER_BULKSALE  where TSPL_INVOICE_MASTER_BULKSALE.Document_No  =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) END   WHEN TSPL_Customer_Invoice_Head.Document_Type ='I' AND len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 THEN (Select Top 1 TSPL_SCRAPINVOICE_HEAD.invoice_No  FROM TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.invoice_No  =TSPL_CUSTOMER_Invoice_Head.AgainstScrap ) END AS AgainstInvoiceNo,  CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'', TSPL_Customer_Invoice_Head.Currency_Code, TSPL_Customer_Invoice_Head.ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], 0 as SecurityDrAmt, 0 as SecurityCrAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='I' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [Sales], 0 as [CollectionRefund], case when TSPL_Customer_Invoice_Head.Document_Type ='D' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [DrNote], Case when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_Customer_Invoice_Head.Status=1    and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  
                                         UNION ALL
                                         select TSPL_Receipt_Adjustment_Header.Customer_No  as ACode,TSPL_Receipt_Adjustment_Header.Customer_No as Child, TSPL_Receipt_Adjustment_Header.Customer_Name  as AName, TSPL_Receipt_Adjustment_Header.Adjustment_No  as DocNo,  TSPL_Receipt_Adjustment_Header.ARInvoiceNo as AgainstInvoiceNo, CONVERT(DATE, TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) as DocDate,'Adjustment' as docType,  TSPL_Receipt_Adjustment_Header.Remarks as DocNarr,  '' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as DrAmt ,case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund],  case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end *-1 as [DrNote],  case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end  * -1 as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code as Location, '' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,  TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Receipt_Adjustment_Header.Customer_No = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   where isnull(TSPL_Receipt_Adjustment_Header.Is_Post,'')='Y'   and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' 
                                         UNION ALL
                                         select (coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as ACode,(coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as Child,(coalesce( TSPL_Customer_Invoice_Head.Customer_Name,TSPL_BANK_REVERSE.Cust_Name))  as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, 
                                         TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END End as DrAmt,  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN TSPL_BANK_REVERSE.Amount When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END ELSE 0 END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
                                         Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else TSPL_BANK_REVERSE.Amount End*-1 as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
                                         CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location], 
                                         'RV-TA'as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'  
                                          and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                                         union all
                                        select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
                                        TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'UA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  TSPL_RECEIPT_HEADER.UnApplied_Balance as DrAmt,  0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
                                        TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
                                        substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) AS [Location], 
                                         'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER as UnappliedReceipt ON UnappliedReceipt.Receipt_No = TSPL_RECEIPT_HEADER .UnApplied_No  
                                         where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and UnappliedReceipt.Receipt_Type ='U' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>'' 
                                          and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   
                                         UNION ALL
                                         select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when  TSPL_VCGL_Head.Amount_Type='Cr' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when  TSPL_VCGL_Head.Amount_Type='Dr' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount Else 0 End as [DrNote], case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount*-1 Else 0 End as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_VCGL_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   
                                         UNION ALL
                                         select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Detail.Document_No       where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  ) InnQuery 
                                         LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo where 1=1   and InnQuery.DocType<>'IM'  and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  
                                         Union All
                                         Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'   ) )  
                                         )InnQuery    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode WHERE 1=1 ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND LEN(ACode)>0 "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    strqry += "and substring(Location, 1,3)= substring('" + txtLocation.Value + "',1,3) "
                End If
                strqry += "AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode
                                          UNION ALL
                                         Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child,ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,  isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No,MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt*  Final.ConvRate)) -  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_GAIN_AMT,0) ) else 0 end +  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_LOSS_AMT ,0) ) else 0 end as DrAmt, 
                                        SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales*  Final.ConvRate)) as [Sales], SUM(convert(decimal(18,2),CollectionRefund*  Final.ConvRate)) as CollectionRefund, SUM(convert(decimal(18,2),DrNote*  Final.ConvRate)) as DrNote, SUM(convert(decimal(18,2),CrNote*  Final.ConvRate)) as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( Select InnQuery.ACode AS ACode,InnQuery.Child AS Child, tspl_customer_master.customer_name AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate, convert(decimal(18,2),InnQuery.DrAmt) as DrAmt,convert(decimal(18,2),InnQuery.CrAmt) as CrAmt,convert(decimal(18,2),InnQuery.Sales) as Sales,case when InnQuery.DocType='IM' then case when InnQuery.CrAmt>0 then  convert(decimal(18,2),InnQuery.CrAmt) else convert(decimal(18,2),InnQuery.DrAmt) * -1 end  else convert(decimal(18,2),InnQuery.CollectionRefund) end  as CollectionRefund , 
                                         InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,InnQuery .EXCHANGE_GAIN_AMT ,InnQuery .EXCHANGE_LOSS_AMT   from (Select InnQuery.ACode AS ACode,InnQuery.Child  , InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ,
                                        case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then 
                                         case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt*  InnQuery. ConvRate) else 
                                         case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt*  InnQuery. ConvRate) when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate) WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt*  InnQuery. ConvRate)  else 0 end end else 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else (CrAmt*  InnQuery. ConvRate)  end end else 
                                         case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt*  InnQuery. ConvRate) +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt*  InnQuery. ConvRate) -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else 
                                         case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt*  InnQuery. ConvRate)  else  (CrAmt*  InnQuery. ConvRate)  end end end as CrAmt, 
                                         InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (SELECT 
                                         ACode AS ACode,max(Child) as Child, 
                                        MAX(AName) AS AName,MAX(DocNo) AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType  ,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails, MAX(Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt) AS DrAmt, SUM(CrAmt) AS CrAmt, SUM(SecurityDrAmt) as SecurityDrAmt, SUM(SecurityCrAmt) as SecurityCrAmt, SUM(Sales) as Sales,case when MAX(DocType)='IM' then case when SUM(CrAmt)>0 then  SUM(CrAmt) else  0 end  else  Sum(CollectionRefund) end  as CollectionRefund , SUM(DrNote) as DrNote,case when MAX(DocType)='IM' then 0 else  Sum(CrNote) end  as CrNote, Location AS Location, 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code, MAX(Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, Receipt_Date as DocDate,case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UR' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'PR' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA' else null end as DocType,case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='S' THEN 'Security' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='C' THEN 'Crate Security' ELSE  CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='R' THEN 'Refrigerator' ELSE CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.SecurityDepositType,'')='O' THEN 'Others' END END END END Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,  Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as DrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then (CASE WHEN   TSPL_RECEIPT_HEADER.Receipt_Type='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' THEN TSPL_RECEIPT_DETAIL.Applied_Amount  WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then Receipt_Amount else 0 end) Else 0 End as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then (CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_RECEIPT_DETAIL.Applied_Amount when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 0 else Receipt_Amount END) Else 0 End AS SecurityCrAmt, 0 as [Sales], case when TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') Then Receipt_Amount*-1 When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') AND TSPL_RECEIPT_DETAIL.Receipt_Type<>'C' Then TSPL_RECEIPT_DETAIL.Applied_Amount WHEN TSPL_RECEIPT_HEADER.Receipt_Type ='R' AND TSPL_RECEIPT_DETAIL.Receipt_Type='C' THEN  -1 * TSPL_RECEIPT_DETAIL.Applied_Amount Else Receipt_Amount end as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN TSPL_Customer_Invoice_Head.Loc_Code  WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN TSPL_RECEIPT_HEADER.Location_GL_Code ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END  ELSE substring(TSPL_RECEIPT_HEADER.Dr_Account, len(TSPL_RECEIPT_HEADER.Dr_Account)-2,3) END as [Location], (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Case When TSPL_RECEIPT_HEADER.Receipt_Type='R' Then TSPL_Customer_Invoice_Head.Customer_Code Else TSPL_RECEIPT_HEADER.Cust_Code End LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y'   and  TSPL_RECEIPT_HEADER.Receipt_Type not in ('M','A','U','K') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'   and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                         UNION ALL 
                                          Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
                                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_RECEIPT_HEADER.Receipt_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
                                         where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('R','A') 
                                         AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                         UNION ALL 
                                         Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, ''  as AgainstInvoiceNo, Reversal_Date as DocDate,'EXC'  as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + 'Exchange Gain/Loss against Journal Entry No  '+isnull(TSPL_JOURNAL_MASTER.Voucher_No,'')    end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_BANK_REVERSE.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_BANK_REVERSE.Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, 1 as ConvRate,   TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as DrAmt, TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT AS CrAmt, 0  as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],isnull( TSPL_Customer_Invoice_Head.Loc_Code,'') as [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_BANK_REVERSE.Source_Type As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC from TSPL_BANK_REVERSE  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BANK_REVERSE.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   
                                         left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No =TSPL_BANK_REVERSE.Reverse_Code  
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No  =  TSPL_RECEIPT_HEADER.Receipt_No and TSPL_RECEIPT_DETAIL.Receipt_Line_No =1 
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No =TSPL_RECEIPT_DETAIL.Document_No 
                                         where  TSPL_BANK_REVERSE.Reverse_Document='Receipts' AND TSPL_BANK_REVERSE.Post ='P'
                                         and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT<>0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT  <>0) 
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                         UNION ALL 
                                         Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, ''  as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType  , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when RD.Receipt_Type='C' then 0 else RD.Applied_Amount  end as DrAmt,case when RD.Receipt_Type='C' then RD.Applied_Amount  else 0  end  AS CrAmt, Receipt_Amount as SecurityDrAmt, 0 AS SecurityCrAmt  , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],     CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE  CASE WHEN (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt )<>''  THEN  (SELECT ISNULL( TSPL_Customer_Invoice_Head.Loc_Code,'') FROM TSPL_Customer_Invoice_Head  WHERE TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_HEADER.Applied_Receipt) ELSE  substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END END AS [Location],  
                                         (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_HEADER.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON TSPL_RECEIPT_HEADER.Receipt_No =RD.Receipt_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND CIH.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                         UNION ALL 
                                         SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type ='A' AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' )INV  GROUP BY  DocNo,Location 
                                         UNION ALL 
                                         SELECT MAX(ACode) AS Cust_Code,max(Child) as Child,MAX(AName) as AName,DocNo AS DocNo,MAX(AgainstInvoiceNo) AS AgainstInvoiceNo,MAX(DocDate) AS DocDate,MAX(DocType) AS DocType,MAX(DocNarr) AS DocNarr,MAX(ChequeDetails) AS ChequeDetails  ,MAX(Currency_Code) AS Currency_Code,MAX(ConvRate) AS ConvRate,SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,MAX(SecurityDrAmt) AS SecurityDrAmt,SUM(SecurityCrAmt) AS SecurityCrAmt  ,MAX([Sales]) AS [Sales],MAX([CollectionRefund]) AS [CollectionRefund],MAX([DrNote]),MAX([CrNote]) AS [CrNote],    [Location], 
                                         MAX(SourceCode) AS SourceCode,MAX(Item_Code) AS Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(Receipt_Type) AS Receipt_Type,MAX(Bank_Code) AS Bank_Code,MAX(Cust_Type_Code) AS Cust_Type_Code  ,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (  Select TSPL_CUSTOMER_MASTER.Cust_Code as ACode,TSPL_CUSTOMER_MASTER.Cust_Code as Child, TSPL_CUSTOMER_MASTER.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_DETAIL.Document_No   as AgainstInvoiceNo, Receipt_Date as DocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'IM'  WHEN TSPL_RECEIPT_HEADER.Receipt_Type='K' THEN 'KN' else null end as DocType , case when ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='Y' Then 'Security' Else '' + TSPL_RECEIPT_HEADER.Entry_Desc end as DocNarr, (rtrim(Entry_Desc) + (case when len(RTRIM(Entry_Desc))>0 and len(RTRIM(Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(Cheque_No,''))>0 then 'Cheque No. - ' + Cheque_No +  ' - '+Cheque_Date else '' end)) as ChequeDetails, TSPL_RECEIPT_HEADER.Currency_Code, TSPL_RECEIPT_HEADER.ConvRate,    case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then 0 else TSPL_RECEIPT_DETAIL.Applied_Amount *-1  end  end  as DrAmt,  case when TSPL_RECEIPT_DETAIL.Receipt_Type='C' then 0 else case when TSPL_RECEIPT_DETAIL.Applied_Amount>0 then TSPL_RECEIPT_DETAIL.Applied_Amount else 0 end  end as CrAmt,  0 as SecurityDrAmt,TSPL_RECEIPT_DETAIL.Applied_Amount  AS SecurityCrAmt , 0 as [Sales], 0  as [CollectionRefund], 0 as [DrNote], 0 as [CrNote],  ISNULL(TSPL_Customer_Invoice_Head.Loc_Code ,'') AS [Location],  (Case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'AR-RC' when TSPL_RECEIPT_HEADER.Receipt_Type='f' then 'AR-RF' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'AR-UN' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AR-PY' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'AR-OA' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'AR-IM' when TSPL_RECEIPT_HEADER.Receipt_Type='K' then 'AR-KN' end) as SourceCode, '' as Item_Code, '' as Item_Desc,TSPL_RECEIPT_DETAIL.Receipt_Type  As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    from TSPL_RECEIPT_HEADER    LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code     LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE    LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No  =TSPL_RECEIPT_DETAIL.Document_No   where TSPL_RECEIPT_HEADER.Posted='Y'  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND TSPL_Customer_Invoice_Head.Customer_Code = TSPL_RECEIPT_HEADER.Cust_Code  AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'K'     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' )INV  GROUP BY  DocNo,Location 
                                          UNION ALL 
                                         Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child,TSPL_RECEIPT_HEADER.Customer_Name as AName, (Select r1.Receipt_No from TSPL_RECEIPT_HEADER r1 where r1 .UnApplied_No  =TSPL_RECEIPT_HEADER.Receipt_No)  as DocNo,'' as AgainstInvoiceNo,TSPL_RECEIPT_HEADER.Receipt_Date as DocDate,'RC' as DocType,'' as DocNarr,(rtrim(TSPL_RECEIPT_HEADER.Entry_Desc) + (case when len(RTRIM(TSPL_RECEIPT_HEADER.Entry_Desc))>0 and len(RTRIM(TSPL_RECEIPT_HEADER.Cheque_No))>0  then ' / ' else '' end)  + (case when LEN(ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,''))>0 then 'Cheque No. - ' + TSPL_RECEIPT_HEADER.Cheque_No +  ' - '+TSPL_RECEIPT_HEADER.Cheque_Date else '' end)) as ChequeDetails, RH.Currency_Code, RH.ConvRate, 0 as DrAmt,  TSPL_RECEIPT_HEADER.Receipt_Amount  AS CrAmt, 0 as SecurityDrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 End  AS SecurityCrAmt, 0 as [Sales], TSPL_RECEIPT_HEADER.Receipt_Amount as [CollectionRefund], 0 as [DrNote], 0 as [CrNote], Right(TSPL_RECEIPT_HEADER.Dr_Account,3) as [Location],  'AR-RC' as SourceCode, '' as Item_Code, '' as Item_Desc, TSPL_RECEIPT_HEADER.Receipt_Type As Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_RECEIPT_HEADER RH ON TSPL_RECEIPT_HEADER.Receipt_No =RH.UnApplied_No   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON RH.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where  TSPL_RECEIPT_HEADER.Posted='Y' and  TSPL_RECEIPT_HEADER.Receipt_Type in ('U') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "')XX GROUP BY XX.ACode, XX.Location, XX.DocNo,XX.DocType 
                                          UNION ALL
                                         Select ACode,Child, AName, DocNo, AgainstInvoiceNo, DocDate, DocType, Narration, ChequeDetails, CURRENCY_CODE, ConvRate, DrAmt, CrAmt, SecurityDrAmt, SecurityCrAmt, Sales, Collectionrefund, DrNote, CrNote, Location, SourceCode, Item_Code, Item_Desc, Receipt_Type, Bank_Code, Cust_Type_Code, Cust_Type_Desc, Cust_Category_Code, CUST_CATEGORY_DESC from (
                                         Select TSPL_RECEIPT_HEADER.Cust_Code as ACode,TSPL_RECEIPT_HEADER.Cust_Code as Child, CIH.Customer_Code, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, '' as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, Case When TSPL_RECEIPT_HEADER.SecurityDeposit<>'Y' Then RD.Applied_Amount Else 0 end as DrAmt, 0 as CrAmt, Case When TSPL_RECEIPT_HEADER.SecurityDeposit='Y' Then TSPL_RECEIPT_HEADER.Receipt_Amount Else 0 end as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales, 0 as Collectionrefund, TSPL_RECEIPT_HEADER.Receipt_Amount as DrNote, 0 as CrNote, 
                                          CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC, ROW_NUMBER() OVER (Partition By TSPL_RECEIPT_HEADER.Receipt_No ORDER BY TSPL_RECEIPT_HEADER.Receipt_No) as RowNo  from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
                                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A'  AND ISNULL(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'
                                          and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'  ) XX --WHERE RowNo=1
                                         UNION ALL
                                         Select max(ACode) as ACode ,max(Child) as Child , max(AName) as AName, max(DocNo) as DocNo, max(AgainstInvoiceNo) as AgainstInvoiceNo,max( DocDate) as DocDate, max(DocType) as DocType, max(Narration) as Narration, max(ChequeDetails) as Narration, max(CURRENCY_CODE) as CURRENCY_CODE, max(ConvRate) as ConvRate, Sum(DrAmt) as Dramt, Sum(CrAmt) as CrAmt, sum(SecurityDrAmt) as SecurityDrAmt, sum(SecurityCrAmt) as SecurityCrAmt, sum(Sales) as Sales, Sum(Collectionrefund) as Collectionrefund, Sum(DrNote) as DrNote, sum(CrNote) as CrNote, max(Location) as Location, max(SourceCode) as SourceCode, max(Item_Code) as Item_Code,max( Item_Desc) as Item_Desc, max(Receipt_Type) as Receipt_Type , max(Bank_Code) as Bank_Code, max(Cust_Type_Code) as Cust_Type_Code, max(Cust_Type_Desc) as Cust_Type_Desc, max(Cust_Category_Code) as Cust_Category_Code,max( CUST_CATEGORY_DESC) as  CUST_CATEGORY_DESC from (
                                         Select  isnull(CIH.Customer_Code,TSPL_RECEIPT_HEADER.Cust_Code )  as ACode,case when TSPL_RECEIPT_HEADER.Receipt_Type='A' then RD.Child_Cust_Code else CIH.Customer_Code end as Child, CM.Customer_Name as AName, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, CIH.Document_No as AgainstInvoiceNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, 'IM' as DocType, TSPL_RECEIPT_HEADER.Narration, Case WHEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No,'')<>'' Then TSPL_RECEIPT_HEADER.Cheque_No+' - '+CONVERT(VARCHAR,TSPL_RECEIPT_HEADER.Cheque_Date,103) Else '' End as ChequeDetails, TSPL_RECEIPT_HEADER.CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate, 0 as DrAmt, RD.Applied_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as Sales,RD.Applied_Amount as CollectionRefund,0 as DrNote, 0 as CrNote,
                                         CASE WHEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt )<>'' THEN (SELECT ISNULL(TRH.Location_GL_Code,'') FROM TSPL_RECEIPT_HEADER TRH WHERE TRH.Receipt_No =TSPL_RECEIPT_HEADER.Applied_Receipt ) ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END  AS [Location], 'AR-IM' as SourceCode, '' as Item_Code, '' as Item_Desc, 'A' as Receipt_Type, TSPL_RECEIPT_HEADER.Bank_Code, CM.Cust_Type_Code, CTM.Cust_Type_Desc, CM.Cust_Category_Code, CCM.CUST_CATEGORY_DESC   from TSPL_RECEIPT_HEADER TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL RD ON RD.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No
                                         LEFT OUTER JOIN TSPL_Customer_Invoice_Head CIH ON CIH.Document_No=RD.Document_No
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER CTM ON CTM.Cust_Type_Code = CM.Cust_Type_Code
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER CCM ON CCM.Cust_Category_Code = CM.CUST_CATEGORY_CODE
                                         left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.bank_Code=TSPL_RECEIPT_HEADER.Bank_Code 
                                         WHERE TSPL_RECEIPT_HEADER.Receipt_Type='A' AND isnull(CIH.Customer_Code,'')<>TSPL_RECEIPT_HEADER.Cust_Code AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "')z group by DocNo ,Location,ACode
                                         UNION ALL
                                         SELECT max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as ACode,max( TSPL_ADJUSTMENT_HEADER.Customer_CODE) as Child,max( TSPL_ADJUSTMENT_HEADER.Customer_NAME ) as AName, Final.Adjustment_No AS DocNo,'' as AgainstInvoiceNo,  CONVERT(DATE,MAX(Final.Adjustment_Date),103) AS DocDate, 'AD' AS DocType, MAX(Final.Description) AS DocNarr, '' AS ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when max(Final.Trans_Type) = 'In' then 0 else  SUM(Final.Cost)  end  AS DrAmt , case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost) else 0 end AS CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], case when max(Final.Trans_Type) ='In' then  SUM(Final.Cost)*-1 else SUM(Final.Cost) end as Collectionrefund, 0 as [drNote], 0 as [CrNote], max(Final.Location) as [Location], Max(SourceCode)as SourceCode, Item_Code, MAX(Item_Description) as Item_Desc  ,MAX(Receipt_Type) As Receipt_Type, '' as Bank_Code,MAX(Cust_Type_Code) as Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  FROM (SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No,  TSPL_ADJUSTMENT_HEADER.Adjustment_Date,  TSPL_ADJUSTMENT_HEADER.Description + (CASE WHEN  TSPL_ADJUSTMENT_HEADER.Description = '' THEN '' ELSE ' - ' END) AS Description, case when tspl_customer_master.cust_type_code ='F' or tspl_customer_master.cust_type_code ='S' then 0 else  ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) end  + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS Cost,  TSPL_ADJUSTMENT_HEADER.Document_No , TSPL_LOCATION_MASTER.Loc_Segment_Code as [Location],TSPL_ADJUSTMENT_HEADER.Trans_Type ,'IC-AD' as SourceCode, TSPL_ADJUSTMENT_DETAIL.Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description  ,'' As Receipt_Type , TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC    FROM TSPL_ADJUSTMENT_HEADER  LEFT OUTER JOIN  TSPL_ADJUSTMENT_DETAIL ON  TSPL_ADJUSTMENT_HEADER.Adjustment_No =  TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left OUTER JOIN  TSPL_LOCATION_MASTER on  TSPL_ADJUSTMENT_DETAIL.Location_Code= TSPL_LOCATION_MASTER.Location_Code inner join  tspl_customer_master on  tspl_adjustment_header.customer_code= tspl_customer_master.cust_code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   WHERE ( ( TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND ( TSPL_ADJUSTMENT_HEADER.Document_No= '' and  TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') or TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice')  AND (ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage_Cost, 0) + ISNULL( TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) <> 0)and  TSPL_ADJUSTMENT_HEADER.Posted='Y'   and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          ) AS Final INNER JOIN  TSPL_ADJUSTMENT_HEADER  ON Final.Adjustment_No =  TSPL_ADJUSTMENT_HEADER.Adjustment_No  GROUP BY Final.Adjustment_No, Final.Item_Code
                                         UNION ALL
                                        select TSPL_Customer_Invoice_Head.Customer_Code, TSPL_Customer_Invoice_Head.Customer_Code as Child,TSPL_Customer_Invoice_Head.Customer_Name , TSPL_Customer_Invoice_Head.Document_No  AS DocNo, CASE WHEN TSPL_Customer_Invoice_Head.Document_Type ='C' AND len(TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No)>0 THEN (Select Top 1 TSPL_SD_SALE_RETURN_HEAD.Document_Code  FROM TSPL_SD_SALE_RETURN_HEAD where TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_Return_No) WHEN (TSPL_Customer_Invoice_Head.Document_Type ='D' OR  TSPL_Customer_Invoice_Head.Document_Type ='I') AND LEN(TSPL_Customer_Invoice_Head.Against_Sale_No)>0 THEN  CASE WHEN LEN(ISNULL( (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No),''))>0 THEN (Select Top 1 ISNULL(TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'')  FROM TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) ELSE (Select Top 1 ISNULL(TSPL_INVOICE_MASTER_BULKSALE.Document_No,'')  FROM TSPL_INVOICE_MASTER_BULKSALE  where TSPL_INVOICE_MASTER_BULKSALE.Document_No  =TSPL_CUSTOMER_Invoice_Head.Against_Sale_No) END   WHEN TSPL_Customer_Invoice_Head.Document_Type ='I' AND len(TSPL_Customer_Invoice_Head.AgainstScrap)>0 THEN (Select Top 1 TSPL_SCRAPINVOICE_HEAD.invoice_No  FROM TSPL_SCRAPINVOICE_HEAD where TSPL_SCRAPINVOICE_HEAD.invoice_No  =TSPL_CUSTOMER_Invoice_Head.AgainstScrap ) END AS AgainstInvoiceNo,  CONVERT(Date,TSPL_Customer_Invoice_Head.Document_Date,103) as DocDate ,(case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DR' end) as [DocType], TSPL_Customer_Invoice_Head.Description ,'', TSPL_Customer_Invoice_Head.Currency_Code, TSPL_Customer_Invoice_Head.ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type ='C' then 0 else TSPL_Customer_Invoice_Head.Document_Total end as [DRAmt],case when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total else 0 end as [CRAmt], 0 as SecurityDrAmt, 0 as SecurityCrAmt, case when TSPL_Customer_Invoice_Head.Document_Type ='I' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [Sales], 0 as [CollectionRefund], case when TSPL_Customer_Invoice_Head.Document_Type ='D' Then TSPL_Customer_Invoice_Head.Document_Total Else 0 End as [DrNote], Case when TSPL_Customer_Invoice_Head.Document_Type ='C' Then TSPL_Customer_Invoice_Head.Document_Total*-1 Else 0 End as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'AR-IN'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'AR-CR'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'AR-DN' end as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC   from  TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_Customer_Invoice_Head.Status=1    and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'and  convert(date,TSPL_Customer_Invoice_Head.Document_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          UNION ALL
                                         select TSPL_Receipt_Adjustment_Header.Customer_No  as ACode,TSPL_Receipt_Adjustment_Header.Customer_No as Child, TSPL_Receipt_Adjustment_Header.Customer_Name  as AName, TSPL_Receipt_Adjustment_Header.Adjustment_No  as DocNo,  TSPL_Receipt_Adjustment_Header.ARInvoiceNo as AgainstInvoiceNo, CONVERT(DATE, TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) as DocDate,'Adjustment' as docType,  TSPL_Receipt_Adjustment_Header.Remarks as DocNarr,  '' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as DrAmt ,case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end   as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund],  case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end *-1 as [DrNote],  case when TSPL_Customer_Invoice_Head.Document_Type<>'C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else 0 end  * -1 as [CrNote], TSPL_Customer_Invoice_Head.Loc_Code as Location, '' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,  TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_Receipt_Adjustment_Header  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Receipt_Adjustment_Header.Customer_No = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   where isnull(TSPL_Receipt_Adjustment_Header.Is_Post,'')='Y'   and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          UNION ALL
                                          select (coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as ACode,(coalesce(TSPL_Customer_Invoice_Head.Customer_Code,TSPL_BANK_REVERSE.Cust_Code))   as Child,(coalesce( TSPL_Customer_Invoice_Head.Customer_Name,TSPL_BANK_REVERSE.Cust_Name))  as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo, 
                                         TSPL_Customer_Invoice_Head.Document_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'RV-TA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  0 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type<>'F' THEN TSPL_BANK_REVERSE.Amount  ELSE 0  END End as DrAmt,  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN TSPL_BANK_REVERSE.Amount When TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount ELSE 0 END ELSE 0 END as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
                                         Case When TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') Then CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN  TSPL_RECEIPT_DETAIL.Applied_Amount * -1 ELSE TSPL_RECEIPT_DETAIL.Applied_Amount END Else TSPL_BANK_REVERSE.Amount End*-1 as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
                                         CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Location_GL_Code,'')<>'' THEN  CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('O','P','F') THEN TSPL_RECEIPT_HEADER.Location_GL_Code END WHEN TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') THEN TSPL_Customer_Invoice_Head.Loc_Code  ELSE substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) END AS [Location], 
                                         'RV-TA'as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_DETAIL On TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N'  AND TSPL_RECEIPT_HEADER.Receipt_Type <>'A'  
                                          and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                         union all
                                        select TSPL_BANK_REVERSE.Cust_Code as ACode,TSPL_BANK_REVERSE.Cust_Code as Child, TSPL_BANK_REVERSE.Cust_Name as AName, TSPL_BANK_REVERSE.Reverse_Code as DocNo,
                                        TSPL_RECEIPT_HEADER.UnApplied_No as AgainstInvoiceNo, TSPL_BANK_REVERSE.Reversal_Date as DocDate , 'UA' as DocType,  TSPL_BANK_REVERSE.Document_No as DocNarr, TSPL_BANK_REVERSE.Cheque_No as ChequeDetails, TSPL_Receipt_Header.Currency_Code, TSPL_Receipt_Header.ConvRate,  TSPL_RECEIPT_HEADER.UnApplied_Balance as DrAmt,  0 as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales],
                                        TSPL_RECEIPT_HEADER.UnApplied_Balance as [CollectionRefund],  0 as [DrNote], 0 as [CrNote],
                                        substring(TSPL_BANK_MASTER.BANKACC, len( TSPL_BANK_MASTER.BANKACC)-2,3) AS [Location], 
                                         'AR-UN' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, TSPL_BANK_REVERSE.Bank_Code, TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from  TSPL_BANK_REVERSE  left outer join  TSPL_BANK_MASTER on  TSPL_BANK_REVERSE .Bank_Code = TSPL_BANK_MASTER.BANK_CODE  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code  
                                         LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No 
                                         LEFT OUTER JOIN TSPL_RECEIPT_HEADER as UnappliedReceipt ON UnappliedReceipt.Receipt_No = TSPL_RECEIPT_HEADER .UnApplied_No  
                                         where  TSPL_BANK_REVERSE.Source_Type='AR' and  TSPL_BANK_REVERSE.post='P' AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' and UnappliedReceipt.Receipt_Type ='U' and isnull(TSPL_RECEIPT_HEADER.Receipt_No ,'')<>'' 
                                          and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)  <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          UNION ALL
                                         select TSPL_VCGL_Head.VC_Code as ACode,TSPL_VCGL_Head.VC_Code as Child, TSPL_VCGL_Head.VC_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, case when  TSPL_VCGL_Head.Amount_Type='Cr' then  TSPL_VCGL_Head.Amount else 0 end  as DrAmt ,case when  TSPL_VCGL_Head.Amount_Type='Dr' then  TSPL_VCGL_Head.Amount else 0 end as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], case when TSPL_VCGL_Head.Amount_Type='Dr' then TSPL_VCGL_Head.Amount Else 0 End as [DrNote], case when TSPL_VCGL_Head.Amount_Type='Cr' then TSPL_VCGL_Head.Amount*-1 Else 0 End as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type, '' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC      from TSPL_VCGL_Head  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE  where TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          UNION ALL
                                         select TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_VCGL_Detail.VCGL_Code as Child, TSPL_VCGL_Detail.VCGL_Name as AName, TSPL_VCGL_Head.Document_No as DocNo,'' as AgainstInvoiceNo, CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) as DocDate,'VCGL' as docType,  TSPL_VCGL_Detail.Remarks as DocNarr,'' as ChequeDetails, 'INR' as Currency_Code, 1 as ConvRate, TSPL_VCGL_Detail.Dr_Amount as DrAmt , TSPL_VCGL_Detail.Cr_Amount as CrAmt, 0 as SecurityDrAmt, 0 as SecurityCrAmt, 0 as [Sales], 0 as [CollectionRefund], TSPL_VCGL_Detail.Dr_Amount as DrNote, TSPL_VCGL_Detail.Cr_Amount as [CrNote], TSPL_VCGL_Head.Location_Segment as Location, 'VC-GL' as SourceCode, '' as Item_Code, '' as Item_Desc  ,'' As Receipt_Type,'' as Bank_Code,TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc ,TSPL_CUSTOMER_MASTER.Cust_Category_Code,TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC     from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code = TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code   LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.Cust_Category_Code  = TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE   LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Detail.Document_No       where TSPL_VCGL_Head.Status=1 and  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''   and  convert(date,TSPL_VCGL_Head.Document_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_VCGL_Head.Document_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                           ) InnQuery 
                                         LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo where 1=1   and InnQuery.DocType<>'IM'  and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                          Union All
                                         Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)     and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'
                                           ) )    )InnQuery    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =InnQuery.ACode WHERE 1=1 ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code 
                                         Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code 
                                        where  CONVERT(DATE,final.DocDate,103) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'  AND CONVERT(DATE,final.DocDate,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "'   AND LEN(ACode)>0"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    strqry += "and substring(Location, 1,3)=substring('" + txtLocation.Value + "',1,3) "
                End If
                strqry += " AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode,DocType
                                         ) XXX left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode GROUP BY ACode)XXXX  ORDER BY ACode "
                dtAccountCustomer = clsDBFuncationality.GetDataTable(strqry)

                If dtAccountCustomer IsNot Nothing AndAlso dtAccountCustomer.Rows.Count > 0 Then
                    lblCustomer.Text = "CUSTOMER LEDGER"

                    gvAccountCustomer.DataSource = Nothing
                    gvAccountCustomer.Columns.Clear()
                    gvAccountCustomer.Rows.Clear()
                    gvAccountCustomer.GroupDescriptors.Clear()
                    gvAccountCustomer.MasterTemplate.SummaryRowsBottom.Clear()
                    gvAccountCustomer.ShowGroupPanel = False
                    gvAccountCustomer.EnableFiltering = False
                    gvAccountCustomer.AllowAddNewRow = False

                    gvAccountCustomer.GroupDescriptors.Clear()
                    gvAccountCustomer.TableElement.TableHeaderHeight = 40
                    gvAccountCustomer.MasterTemplate.ShowRowHeaderColumn = False
                    gvAccountCustomer.DataSource = dtAccountCustomer
                    gvAccountCustomer.Columns("ACode").HeaderText = "Account Code"
                    gvAccountCustomer.Columns("AName").HeaderText = "Account Name"
                    gvAccountCustomer.Columns("DrAmt").HeaderText = "Debit Amount"
                    gvAccountCustomer.Columns("CrAmt").HeaderText = "Credit Amount"
                    gvAccountCustomer.Columns("BalAmt").HeaderText = "Closing"
                    For ii As Integer = 0 To gvAccountCustomer.Columns.Count - 1
                        gvAccountCustomer.Columns(ii).ReadOnly = True
                        gvAccountCustomer.Columns(ii).IsVisible = True
                        gvAccountCustomer.Columns(ii).Width = 150
                    Next


                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub
    Public Sub Load_Report_Finish_Goods()
        Try
            If dtFinishGoods Is Nothing OrElse dtFinishGoods.Rows.Count <= 0 Then
                Dim sQuery As String = "Select convert(varchar, GrpMonth,103) as GrpMonth,GrpCode,max(GrpName) as GrpName,MAX(SR_NO) AS SR_NO,Sum(Quantity)/1000 As Quantity from (
                select  convert(date, Document_Date,103) as GrpMonth,price_CodeNon as GrpCode,price_CodeNon as GrpName,SR_NO,Qty as Quantity   from (
                select TSPL_SD_SALE_INVOICE_HEAD.Document_Date,
                CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOSHALA','DCS','KVSS') then TSPL_CUSTOMER_MASTER.price_CodeNon  WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION') then 'MILK UNION'  WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOVTCR') then 'GOVT' else 'OTHER' end as price_CodeNon,
                CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION') then 6  WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOSHALA') then 5 WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('DCS') then 4
				WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOVTCR') then 3 	WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('KVSS') then 2 else 1
				end as 'SR_NO',
                (TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Qty
                from TSPL_SD_SALE_INVOICE_DETAIL
                left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                    LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") +
                "' and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and TSPL_SD_SALE_INVOICE_DETAIL.Location='" + txtLocation.Value + "' "
                End If
                sQuery += " union all
                select convert(date, thedate,103) as PROD_DATE,CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOSHALA','DCS','KVSS') then TSPL_CUSTOMER_MASTER.price_CodeNon 
                WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION') then 'MILK UNION'  WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOVTCR') then 'GOVT' else 'OTHER' end as price_CodeNon,
                CASE WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('MILKUNION') then 6  WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOSHALA') then 5 WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('DCS') then 4
				WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('GOVTCR') then 3 	WHEN TSPL_CUSTOMER_MASTER.price_CodeNon in ('KVSS') then 2 else 1
				end as 'SR_NO',0 as Qty from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),(select TSPL_CUSTOMER_MASTER.price_CodeNon as 'price_CodeNon' from TSPL_CUSTOMER_MASTER) as TSPL_CUSTOMER_MASTER
                )x 
                )xxxxx Group by GrpMonth,GrpCode order by convert(date, GrpMonth,103),SR_NO desc"
                dtFinishGoods = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtFinishGoods IsNot Nothing AndAlso dtFinishGoods.Rows.Count > 0 Then
                cvFinishGoods.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvFinishGoods.AreaDesign = CartesianArea1
                cvFinishGoods.Series.Clear()
                Dim strValue As String = "Quantity"

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtFinishGoods.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))) Then
                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))
                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtFinishGoods.Rows(ii)("GrpMonth")
                    drTS("Value") = dtFinishGoods.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next

                cvFinishGoods.ShowTitle = True
                cvFinishGoods.ChartElement.TitlePosition = TitlePosition.Top
                cvFinishGoods.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvFinishGoods.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvFinishGoods.Title = "SALE CHART"

                Dim smartLabelsController As New SmartLabelsController()
                cvFinishGoods.Controllers.Add(smartLabelsController)
                cvFinishGoods.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvFinishGoods.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvFinishGoods.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvFinishGoods.Series.Add(barSeries)
                Next
                cvFinishGoods.ShowLegend = True
                setOrientation(cvFinishGoods, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvFinishGoods.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)


                gvFinishGoods.DataSource = Nothing
                gvFinishGoods.Columns.Clear()
                gvFinishGoods.Rows.Clear()
                gvFinishGoods.GroupDescriptors.Clear()
                gvFinishGoods.MasterTemplate.SummaryRowsBottom.Clear()
                gvFinishGoods.ShowGroupPanel = False
                gvFinishGoods.EnableFiltering = False
                gvFinishGoods.AllowAddNewRow = False

                gvFinishGoods.GroupDescriptors.Clear()
                gvFinishGoods.TableElement.TableHeaderHeight = 40
                gvFinishGoods.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvFinishGoods.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtFinishGoods.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvFinishGoods.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")), gvFinishGoods.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth"))) Then
                        gvFinishGoods.Rows.AddNew()
                        gvFinishGoods.Rows(gvFinishGoods.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth")), gvFinishGoods.Rows.Count - 1)
                    End If
                    gvFinishGoods.Rows(arrRow.Item(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtFinishGoods.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtFinishGoods.Rows(ii)(strValue))
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "TOTAL"
                repoRate.Name = "TOTAL"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n2}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvFinishGoods.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvFinishGoods.Rows.Count - 1
                    For ii As Integer = 1 To gvFinishGoods.Columns.Count - 2
                        gvFinishGoods.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvFinishGoods.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvFinishGoods.Rows(jj).Cells(ii).Value)
                    Next
                Next

                For ii As Integer = 0 To gvFinishGoods.Columns.Count - 1
                    gvFinishGoods.Columns(ii).ReadOnly = True
                    gvFinishGoods.Columns(ii).IsVisible = True
                    gvFinishGoods.Columns(ii).Width = 150
                Next


                '
                'gvFinishGoods.BestFitColumns()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvFinishGoods_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvFinishGoods.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvFinishGoods_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvFinishGoods.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Load_Report_PRODUCTION()
        Try
            If dtProduction Is Nothing OrElse dtProduction.Rows.Count <= 0 Then
                Dim sQuery As String = "Select convert(varchar, GrpMonth,103) as GrpMonth,GrpCode,max(sr_no) as sr_no,max(GrpName) as GrpName,
            Sum(Quantity)/1000 As Quantity from ( 
            select PROD_DATE as GrpMonth,ITEM_CODE as GrpCode,sr_no,item_desc as GrpName,FINAL_PRODUCTION_QTY as Quantity 
						from (
            SELECT   TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,
            case when TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE in ('FG0001','FG0002','FG0003') then  substring(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE, 6,1) else '0' end as sr_no, 
            TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY
            FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL       
            LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
            left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.item_code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
            WHERE CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_ITEM_MASTER.STRUCTURE_CODE='FG'"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE='" + txtLocation.Value + "' "
                End If
                sQuery += "  union all
                select convert(date, thedate,103) as PROD_DATE,TSPL_ITEM_MASTER.Item_Code,
                case when TSPL_ITEM_MASTER.ITEM_CODE in ('FG0001','FG0002','FG0003') then  substring(TSPL_ITEM_MASTER.ITEM_CODE, 6,1) else '0' end as sr_no, 
                TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',0 as FINAL_PRODUCTION_QTY
                from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),TSPL_ITEM_MASTER
                where TSPL_ITEM_MASTER.STRUCTURE_CODE='FG'	 
                )x
                )xxxxx Group by GrpMonth,GrpCode order by convert(date, GrpMonth,103),sr_no desc"
                dtProduction = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtProduction IsNot Nothing AndAlso dtProduction.Rows.Count > 0 Then
                cvProdution.Area.View.Palette = New CustomPalette()
                Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
                cvProdution.AreaDesign = CartesianArea1
                cvProdution.Series.Clear()
                Dim strValue As String = "Quantity"
                Dim tempValue As Decimal = 0

                Dim ds As DataSet = New DataSet
                Dim arrLegend As New List(Of String)
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrLegend.Contains(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))) Then

                        Dim dtNew As New DataTable
                        dtNew.TableName = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        dtNew.Columns.Add("Name", GetType(String))


                        'If ii = 3 Then
                        '    For i As Integer = 0 To arrLegend.Count - 1
                        '        tempValue += dtProduction.Rows(i)("Quantity")
                        '    Next
                        '    'For Each item As DataRow In dtProduction.Rows
                        '    dtProduction.Rows(ii)("Quantity") = tempValue
                        '    'Next
                        'End If

                        dtNew.Columns.Add("Value", GetType(Decimal))
                        arrLegend.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")))
                        ds.Tables.Add(dtNew)
                    End If

                    Dim drTS As DataRow = ds.Tables(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))).NewRow()
                    drTS("Name") = dtProduction.Rows(ii)("GrpMonth")

                    drTS("Value") = dtProduction.Rows(ii)(strValue)
                    ds.Tables(clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))).Rows.Add(drTS)
                Next

                cvProdution.ShowTitle = True
                cvProdution.ChartElement.TitlePosition = TitlePosition.Top
                cvProdution.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                cvProdution.ChartElement.TitleElement.ForeColor = Color.WhiteSmoke

                cvProdution.Title = "PRODUCTION CHART"

                Dim smartLabelsController As New SmartLabelsController()
                cvProdution.Controllers.Add(smartLabelsController)
                cvProdution.ShowSmartLabels = True

                Dim strategy As SmartLabelsStrategyBase = Nothing
                cvProdution.AreaType = ChartAreaType.Cartesian
                strategy = New VerticalAdjusmentLabelsStrategy()

                cvProdution.DataSource = ds

                For ii As Integer = 0 To ds.Tables.Count - 1
                    Dim barSeries As New BarSeries("Value", "Name")
                    barSeries.DataMember = ds.Tables(ii).TableName
                    barSeries.LegendTitle = ds.Tables(ii).TableName
                    cvProdution.Series.Add(barSeries)
                Next
                cvProdution.ShowLegend = True
                setOrientation(cvProdution, "Vertical", 0)
                smartLabelsController.Strategy = strategy
                cvProdution.ChartElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)


                gvProdution.DataSource = Nothing
                gvProdution.Columns.Clear()
                gvProdution.Rows.Clear()
                gvProdution.GroupDescriptors.Clear()
                gvProdution.MasterTemplate.SummaryRowsBottom.Clear()
                gvProdution.ShowGroupPanel = False
                gvProdution.EnableFiltering = False
                gvProdution.AllowAddNewRow = False

                gvProdution.GroupDescriptors.Clear()
                gvProdution.TableElement.TableHeaderHeight = 40
                gvProdution.MasterTemplate.ShowRowHeaderColumn = False




                Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoComplete.FormatString = ""
                repoComplete.HeaderText = ""
                'repoComplete.Width = 70
                repoComplete.Name = "GrpCode"
                repoComplete.ReadOnly = True
                repoComplete.IsVisible = False
                gvProdution.MasterTemplate.Columns.Add(repoComplete)
                Dim arrColumn As New Dictionary(Of String, Integer)
                Dim arrRow As New Dictionary(Of String, Integer)

                Dim repoRate As GridViewDecimalColumn
                For ii As Integer = 0 To dtProduction.Rows.Count - 1
                    If Not arrColumn.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))) Then
                        repoRate = New GridViewDecimalColumn()
                        repoRate.FormatString = ""
                        repoRate.HeaderText = clsCommon.myCstr(dtProduction.Rows(ii)("GrpName"))
                        repoRate.Name = clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode"))
                        'repoRate.Width = 80
                        repoRate.Minimum = 0
                        repoRate.FormatString = "{0:n2}"
                        repoRate.DecimalPlaces = 2
                        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                        gvProdution.MasterTemplate.Columns.Add(repoRate)

                        arrColumn.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")), gvProdution.Columns.Count - 1)
                    End If


                    If Not arrRow.ContainsKey(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))) Then
                        gvProdution.Rows.AddNew()
                        gvProdution.Rows(gvProdution.Rows.Count - 1).Cells("GrpCode").Value = clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth"))
                        arrRow.Add(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")), gvProdution.Rows.Count - 1)
                    End If
                    gvProdution.Rows(arrRow.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpMonth")))).Cells(arrColumn.Item(clsCommon.myCstr(dtProduction.Rows(ii)("GrpCode")))).Value = clsCommon.myCdbl(dtProduction.Rows(ii)(strValue))
                Next

                repoRate = New GridViewDecimalColumn()
                repoRate.FormatString = ""
                repoRate.HeaderText = "TOTAL"
                repoRate.Name = "TOTAL"
                repoRate.Width = 100
                repoRate.Minimum = 0
                repoRate.FormatString = "{0:n2}"
                'repoRate.DecimalPlaces = 2
                repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvProdution.MasterTemplate.Columns.Add(repoRate)

                For jj As Integer = 0 To gvProdution.Rows.Count - 1
                    For ii As Integer = 1 To gvProdution.Columns.Count - 2
                        gvProdution.Rows(jj).Cells("Total").Value = clsCommon.myCDecimal(gvProdution.Rows(jj).Cells("Total").Value) + clsCommon.myCDecimal(gvProdution.Rows(jj).Cells(ii).Value)
                    Next
                Next

                For ii As Integer = 0 To gvProdution.Columns.Count - 1
                    gvProdution.Columns(ii).ReadOnly = True
                    gvProdution.Columns(ii).IsVisible = True
                    gvProdution.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvProdution_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvProdution.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvProdution_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvProdution.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Load_Report_Quality()

        Try
            If dtQcPending Is Nothing OrElse dtQcPending.Rows.Count <= 0 Then
                Dim sQuery As String = "
                select  TSPL_MRN_DETAIL.Location,count(*) as 'QC Pending' from TSPL_MRN_HEAD 
                left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
                where TSPL_MRN_DETAIL.MRN_No   not in (select TSPL_QC_CHECK_DETAIL.MRN_No from TSPL_QC_CHECK_DETAIL)  and TSPL_ITEM_MASTER.Is_AllowQC_ON_Purchase=1
                "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_MRN_DETAIL.Location='" + txtLocation.Value + "' "
                End If
                sQuery += " AND TSPL_ITEM_MASTER.structure_Code IN ('RM','PM')  
                AND TSPL_GRN_HEAD.IsSkipPurchaseQC=0
                and convert(date,TSPL_MRN_HEAD.MRN_Date,103) >= convert(date, '01-apr-2023', 103) 
                group by TSPL_MRN_DETAIL.Location"

                dtQcPending = clsDBFuncationality.GetDataTable(sQuery)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtQuality Is Nothing OrElse dtQuality.Rows.Count <= 0 Then
                Dim sQuery As String = " select  ROW_NUMBER() OVER (ORDER BY TSPL_GRN_HEAD.Bill_To_Location) AS 'S.NO',TSPL_GRN_HEAD.Ref_No,TSPL_MRN_HEAD.Vendor_Name as 'VENDOR',TSPL_ITEM_MASTER.Short_Description as 'ITEM_DESC',TSPL_MRN_HEAD.VehicleNo ,'Pending' as 'QCstatus','' as 'DEDPer'
from TSPL_MRN_HEAD 
                left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No
				left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_HEAD.Against_GRN
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code
                where TSPL_MRN_DETAIL.MRN_No   not in (select TSPL_QC_CHECK_DETAIL.MRN_No from TSPL_QC_CHECK_DETAIL)  and TSPL_ITEM_MASTER.Is_AllowQC_ON_Purchase=1               
                AND TSPL_ITEM_MASTER.structure_Code IN ('RM','PM')  
                and convert(date,TSPL_MRN_HEAD.MRN_Date,103)>=  convert(date, '01-apr-2023', 103)
                AND TSPL_GRN_HEAD.IsSkipPurchaseQC=0"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_MRN_DETAIL.Location='" + txtLocation.Value + "' "
                End If
                sQuery += " order by TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No desc "
                dtQuality = clsDBFuncationality.GetDataTable(sQuery)
            End If


            If dtQuality IsNot Nothing AndAlso dtQuality.Rows.Count > 0 Then
                Dim qcPendingValue As Integer = dtQcPending.Rows(0)("QC Pending").ToString()
                lblQuality.Text = "CURRENT STATUS" + " [TOTAL QC PENDING  - " + qcPendingValue.ToString() + "]"

                gvQuality.DataSource = Nothing
                gvQuality.Columns.Clear()
                gvQuality.Rows.Clear()
                gvQuality.GroupDescriptors.Clear()
                gvQuality.MasterTemplate.SummaryRowsBottom.Clear()
                gvQuality.ShowGroupPanel = False
                gvQuality.EnableFiltering = False
                gvQuality.AllowAddNewRow = False

                gvQuality.GroupDescriptors.Clear()
                gvQuality.TableElement.TableHeaderHeight = 40
                gvQuality.MasterTemplate.ShowRowHeaderColumn = False
                gvQuality.DataSource = dtQuality
                gvQuality.Columns("S.NO").HeaderText = "S.NO"
                gvQuality.Columns("S.NO").TextAlignment = ContentAlignment.MiddleCenter
                gvQuality.Columns("Ref_No").HeaderText = "RAL"
                gvQuality.Columns("Item_Desc").HeaderText = "ITEM NAME"
                gvQuality.Columns("VehicleNo").HeaderText = "TRUCK NO"
                gvQuality.Columns("QCstatus").HeaderText = "QC STATUS"
                gvQuality.Columns("DEDPer").HeaderText = "DEDUCTION %"

                For ii As Integer = 0 To gvQuality.Columns.Count - 1
                    If ii = 0 Then
                        gvQuality.Columns(ii).ReadOnly = True
                        gvQuality.Columns(ii).IsVisible = True
                        gvQuality.Columns(ii).MaxWidth = 50
                    Else
                        gvQuality.Columns(ii).ReadOnly = True
                        gvQuality.Columns(ii).IsVisible = True
                        gvQuality.Columns(ii).Width = 150
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtQualitySummary Is Nothing OrElse dtQualitySummary.Rows.Count <= 0 Then
                Dim sQuery As String = "select  ROW_NUMBER() OVER (ORDER BY TSPL_GRN_HEAD.Bill_To_Location) AS 'S.NO',TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',
                count((CASE WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 THEN 'FULL_REJECT'  end)) AS 'FULL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'PARTIAL_REJECT',
                count((case when TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 OR TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 then 'Partial Ok'  end)) AS 'TOTALL_REJECT',
                count((case when TSPL_GRN_HEAD.VisualQCStatus=1 AND (TSPL_GRN_HEAD.VisualQCStatusSecond<>3 OR TSPL_GRN_HEAD.VisualQCStatusSecond<>2 or TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected')then 'Ok'  end)) AS 'TOTAL_ACCEPTED'
                from  TSPL_GRN_HEAD
                left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
                left OUTER join TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                where 2=2  AND TSPL_ITEM_MASTER.structure_Code IN ('RM','PM')
                and CONVERT(DATE,TSPL_GRN_HEAD.GRN_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
                sQuery += " and TSPL_GRN_HEAD.Ref_No in (select   TSPL_GRN_HEAD.Ref_No
                from TSPL_GRN_HEAD
                left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
                where isnull(TSPL_GRN_HEAD.Ref_No,'')<>'' 
                and convert(date,TSPL_GRN_HEAD.GRN_Date,103) >=DATEADD(day, -15,convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"
                sQuery += ",103))"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_GRN_HEAD.Bill_To_Location ='" + txtLocation.Value + "' "
                End If
                sQuery += ")"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_GRN_HEAD.Bill_To_Location ='" + txtLocation.Value + "' "
                End If
                sQuery += " GROUP BY TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description
order by TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No desc"

                dtQualitySummary = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtQualitySummary IsNot Nothing AndAlso dtQualitySummary.Rows.Count > 0 Then
                lblQualitySummary.Text = "QUALITY SUMMARY RAL WISE"

                gvQualitySummary.DataSource = Nothing
                gvQualitySummary.Columns.Clear()
                gvQualitySummary.Rows.Clear()
                gvQualitySummary.GroupDescriptors.Clear()
                gvQualitySummary.MasterTemplate.SummaryRowsBottom.Clear()
                gvQualitySummary.ShowGroupPanel = False
                gvQualitySummary.EnableFiltering = False
                gvQualitySummary.AllowAddNewRow = False

                gvQualitySummary.GroupDescriptors.Clear()
                gvQualitySummary.TableElement.TableHeaderHeight = 40
                gvQualitySummary.MasterTemplate.ShowRowHeaderColumn = False
                gvQualitySummary.DataSource = dtQualitySummary
                gvQualitySummary.Columns("S.No").HeaderText = "S.No"
                gvQualitySummary.Columns("S.NO").TextAlignment = ContentAlignment.MiddleCenter
                gvQualitySummary.Columns("Ref_No").HeaderText = "RAL"
                gvQualitySummary.Columns("Item_Desc").HeaderText = "ITEM NAME"
                gvQualitySummary.Columns("FULL_REJECT").HeaderText = "FULL REJECTED"
                gvQualitySummary.Columns("FULL_REJECT").TextAlignment = ContentAlignment.MiddleCenter
                gvQualitySummary.Columns("PARTIAL_REJECT").HeaderText = "PARTIAL REJECTED"
                gvQualitySummary.Columns("PARTIAL_REJECT").TextAlignment = ContentAlignment.MiddleCenter
                gvQualitySummary.Columns("TOTALL_REJECT").HeaderText = "TOTAL REJECTED"
                gvQualitySummary.Columns("TOTALL_REJECT").TextAlignment = ContentAlignment.MiddleCenter
                gvQualitySummary.Columns("TOTAL_ACCEPTED").HeaderText = "TOTAL ACCEPTED"
                gvQualitySummary.Columns("TOTAL_ACCEPTED").TextAlignment = ContentAlignment.MiddleCenter

                For ii As Integer = 0 To gvQualitySummary.Columns.Count - 1
                    If ii = 0 Then
                        gvQualitySummary.Columns(ii).ReadOnly = True
                        gvQualitySummary.Columns(ii).IsVisible = True
                        gvQualitySummary.Columns(ii).MaxWidth = 50
                    Else
                        gvQualitySummary.Columns(ii).ReadOnly = True
                        gvQualitySummary.Columns(ii).IsVisible = True
                        gvQualitySummary.Columns(ii).Width = 150
                    End If
                Next
                gvQualitySummary.Columns("Item_Code").IsVisible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub gvQuality_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvQuality.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQuality_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvQuality.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQualitySummary_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvQualitySummary.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvQualitySummary_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvQualitySummary.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub


    Public Sub Load_Report_Raw_Material()
        Try
            If dtRMStock Is Nothing OrElse dtRMStock.Rows.Count <= 0 Then
                Dim sQuery As String = "select xx.Item_Desc,
                case when xx.UOM='KG' THEN 
		                CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE TSPL_ITEM_UOM_DETAIL.UOM_Code END 
	                 WHEN xx.UOM='GM' THEN 
	                    CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE KG_UOM_DETAIL.UOM_Code END
                 ELSE xx.UOM END AS 'Unit',	
                 CAST((case when xx.UOM='KG' THEN 
		                CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END 
	                 WHEN xx.UOM='GM' THEN 
	                    CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(KG_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END
                 ELSE xx.STOCK_QTY END) AS numeric(10,0)) AS 'STOCK_QTY',
                cast(xx.QTY_FOR_DAYS as integer)  as QTY_FOR_DAYS  
                from (
                SELECT RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,
                 max(RM_STOCK_DAYS.UOM)  AS 'UOM',
                SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))  AS 'STOCK_QTY',
                SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL,
	            CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE 
	            CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' 
	            FROM  (
	            SELECT RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM (
	            SELECT TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	            CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',
	            CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY'
	             FROM TSPL_INVENTORY_MOVEMENT
	            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
	            WHERE 2=2 "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_INVENTORY_MOVEMENT.Location_Code='" + txtLocation.Value + "' "
                End If
                sQuery += "  AND TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM')) RM_STOCK
	GROUP BY  RM_STOCK.Item_Code
	UNION ALL
	SELECT  
	TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM',
	0 AS 'STOCK_QTY',
	AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN 
    (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE 
    CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN
    ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END
    END)  AS 'REQ_STOCK',
	0 AS 'MIN_LEVEL'
	FROM TSPL_MF_BOM_HEAD 
	LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
	LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE
	INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2 "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and TSPL_MF_BOM_HEAD.LOCATION_CODE='" + txtLocation.Value + "' "
                End If
                sQuery += " GROUP BY PROD_ITEM_CODE
	) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO
	WHERE  TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM')  "
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " and 	TSPL_MF_BOM_HEAD.LOCATION_CODE='" + txtLocation.Value + "' "
                End If

                sQuery += "GROUP BY  
	TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	UNION ALL
	select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code ,TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	0 AS 'STOCK_QTY', 
	0 AS 'REQ_STOCK',
	TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS 'MIN_LEVEL' 
	from TSPL_ITEM_REORDER_LEVEL_NEW 
	left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code
	where TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM') and Apply='Y'"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " AND TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code='" + txtLocation.Value + "' "
                End If
                sQuery += " ) RM_STOCK_DAYS
GROUP BY  RM_STOCK_DAYS.ITEM_CODE
)xx 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(TSPL_ITEM_UOM_DETAIL.UOM_Code)='QTL'
left outer join TSPL_ITEM_UOM_DETAIL KG_UOM_DETAIL on KG_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(KG_UOM_DETAIL.UOM_Code)='KG'
WHERE XX.ITEM_CODE NOT IN ('PM0001','PM0002') and xx.STOCK_QTY>0
ORDER BY  XX.ITEM_DESC"
                dtRMStock = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtRMStock IsNot Nothing AndAlso dtRMStock.Rows.Count > 0 Then
                lblRMStock.Text = "Stock Details"

                gvRMStock.DataSource = Nothing
                gvRMStock.Columns.Clear()
                gvRMStock.Rows.Clear()
                gvRMStock.GroupDescriptors.Clear()
                gvRMStock.MasterTemplate.SummaryRowsBottom.Clear()
                gvRMStock.ShowGroupPanel = False
                gvRMStock.EnableFiltering = False
                gvRMStock.AllowAddNewRow = False

                gvRMStock.GroupDescriptors.Clear()
                gvRMStock.TableElement.TableHeaderHeight = 40
                gvRMStock.MasterTemplate.ShowRowHeaderColumn = False
                gvRMStock.DataSource = dtRMStock
                gvRMStock.Columns("Item_Desc").HeaderText = "Name of Material"
                gvRMStock.Columns("STOCK_QTY").HeaderText = "Stock Available"
                gvRMStock.Columns("QTY_FOR_DAYS").HeaderText = "Available Stock" + Environment.NewLine + "In Days"

                For ii As Integer = 0 To gvRMStock.Columns.Count - 1
                    gvRMStock.Columns(ii).ReadOnly = True
                    gvRMStock.Columns(ii).IsVisible = True
                    gvRMStock.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtRMSupply Is Nothing OrElse dtRMSupply.Rows.Count <= 0 Then
                Dim sQuery As String = "select final.Ref_No,final.ITEM_DESC,final.UOM,final.Vendor_Name,final.RAL_QTY,final.GRNQTY,final.Pending_Qty from (
                Select  TSPL_GRN_HEAD.Ref_No ,TSPL_ITEM_MASTER.Short_Description As 'ITEM_DESC',TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_GRN_HEAD.Vendor_Name,
                cast(RM_RAL.RAL_QTY as numeric (18,0)) as 'RAL_QTY',
                max(TendorSeqNo) as TendorSeqNo,
                SUM(CASE WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 then 0 else cast(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as numeric (18,0)) END) AS GRNQTY,
                (RM_RAL.RAL_QTY - sum((CASE WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' or TSPL_GRN_HEAD.VisualQCStatusSecond=2 or TSPL_GRN_HEAD.VisualQCStatus=2 then 0 else cast(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as numeric (18,0)) END))) as 'Pending_Qty'
                from TSPL_PO_WEIGHTMENT_HEAD
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
                LEFT JOIN TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
                INNER JOIN 
                (SELECT TSPL_TENDER_DETAIL.Location AS 'LOCATION' ,TSPL_TENDER_HEADER.DocumentCode AS 'RAL',TSPL_TENDER_DETAIL.Vendor_Code AS 'VENDORCODE',TSPL_VENDOR_MASTER.Vendor_Name AS 'VENDORNAME',TSPL_TENDER_DETAIL.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_NAME',TSPL_TENDER_DETAIL.Unit_code  AS 'UOM',max(TSPL_TENDER_HEADER.TendorSeqNo) as TendorSeqNo,SUM(TSPL_TENDER_DETAIL.Qty) AS 'RAL_QTY', 0 AS 'GRNQTY'
                FROM TSPL_TENDER_HEADER
                LEFT OUTER JOIN TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.ITEM_CODE
                INNER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_DETAIL.Vendor_Code
                GROUP BY TSPL_TENDER_DETAIL.Location ,TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_TENDER_DETAIL.Unit_code) 
	                RM_RAL ON RM_RAL.RAL=TSPL_GRN_HEAD.Ref_No AND RM_RAL.LOCATION=TSPL_PO_WEIGHTMENT_HEAD.Location_Code AND RM_RAL.ITEM_CODE=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code AND RM_RAL.VENDORCODE=TSPL_GRN_HEAD.Vendor_Code  AND RM_RAL.UOM=TSPL_PO_WEIGHTMENT_DETAIL.UOM
                where TSPL_ITEM_MASTER.RAL=1"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And TSPL_PO_WEIGHTMENT_HEAD.Location_Code='" + txtLocation.Value + "' "
                End If
                sQuery += "  GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_Code ,TSPL_GRN_HEAD.Ref_No,TSPL_PO_WEIGHTMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_PO_WEIGHTMENT_DETAIL.UOM ,TSPL_GRN_HEAD.Vendor_Code ,TSPL_GRN_HEAD.Vendor_Name ,TSPL_PO_WEIGHTMENT_DETAIL.UOM,RAL_QTY
                        ) final
                        order by final.TendorSeqNo desc"
                dtRMSupply = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtRMSupply IsNot Nothing AndAlso dtRMSupply.Rows.Count > 0 Then
                lblQuality.Text = "Supply / Pending Details Against RAL"
                gvRMSupply.DataSource = Nothing
                gvRMSupply.Columns.Clear()
                gvRMSupply.Rows.Clear()
                gvRMSupply.GroupDescriptors.Clear()
                gvRMSupply.MasterTemplate.SummaryRowsBottom.Clear()
                gvRMSupply.ShowGroupPanel = False
                gvRMSupply.EnableFiltering = False
                gvRMSupply.AllowAddNewRow = False

                gvRMSupply.GroupDescriptors.Clear()
                gvRMSupply.TableElement.TableHeaderHeight = 40
                gvRMSupply.MasterTemplate.ShowRowHeaderColumn = False
                gvRMSupply.DataSource = dtRMSupply
                gvRMSupply.Columns("Ref_No").HeaderText = "RAL"
                gvRMSupply.Columns("Item_Desc").HeaderText = "Name of" + Environment.NewLine + "Material"
                gvRMSupply.Columns("UOM").HeaderText = "Unit"
                gvRMSupply.Columns("Vendor_Name").HeaderText = "Vendor"
                gvRMSupply.Columns("RAL_QTY").HeaderText = "Ordered" + Environment.NewLine + "Quantity"
                gvRMSupply.Columns("GRNQTY").HeaderText = "Received" + Environment.NewLine + "Quantity"
                gvRMSupply.Columns("Pending_Qty").HeaderText = "Pending" + Environment.NewLine + "Quantity"


                For ii As Integer = 0 To gvRMSupply.Columns.Count - 1
                    gvRMSupply.Columns(ii).ReadOnly = True
                    gvRMSupply.Columns(ii).IsVisible = True
                    gvRMSupply.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Try
            If dtRMInPlant Is Nothing OrElse dtRMInPlant.Rows.Count <= 0 Then
                Dim sQuery As String = "select  TSPL_GRN_HEAD.Ref_No,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',TSPL_GRN_HEAD.Vendor_Name,TSPL_GRN_HEAD.VehicleNo AS VehicleNo from TSPL_GRN_HEAD
left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
left join tspl_item_master on tspl_item_master.Item_Code= TSPL_GRN_DETAIL.Item_Code
where convert(date,TSPL_GRN_HEAD.GRN_Date,103) >=DATEADD(day, -2,convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103))"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And Bill_To_Location='" + txtLocation.Value + "' "
                End If
                sQuery += "  AND TSPL_GRN_HEAD.IsCancel=0
And   TSPL_GRN_HEAD.GRN_No   Not IN (
select  TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No from TSPL_PO_WEIGHTMENT_HEAD
left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
where convert(date,TSPL_GRN_HEAD.GRN_Date,103) >=DATEADD(day, -2,convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103))"
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    sQuery += " And Bill_To_Location='" + txtLocation.Value + "' "
                End If
                sQuery += "  AND TSPL_PO_WEIGHTMENT_HEAD.Status=1)
ORDER BY  TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_DETAIL.Item_Desc,TSPL_GRN_HEAD.Vendor_Name"
                dtRMInPlant = clsDBFuncationality.GetDataTable(sQuery)
            End If

            If dtRMInPlant IsNot Nothing AndAlso dtRMInPlant.Rows.Count > 0 Then
                lblQuality.Text = "Indoor Vehicle Status"

                gvRMInPlant.DataSource = Nothing
                gvRMInPlant.Columns.Clear()
                gvRMInPlant.Rows.Clear()
                gvRMInPlant.GroupDescriptors.Clear()
                gvRMInPlant.MasterTemplate.SummaryRowsBottom.Clear()
                gvRMInPlant.ShowGroupPanel = False
                gvRMInPlant.EnableFiltering = False
                gvRMInPlant.AllowAddNewRow = False

                gvRMInPlant.GroupDescriptors.Clear()
                gvRMInPlant.TableElement.TableHeaderHeight = 40
                gvRMInPlant.MasterTemplate.ShowRowHeaderColumn = False
                gvRMInPlant.DataSource = dtRMInPlant
                gvRMInPlant.Columns("Ref_No").HeaderText = "RAL"
                gvRMInPlant.Columns("Item_Desc").HeaderText = "Name of Material"
                gvRMInPlant.Columns("Vendor_Name").HeaderText = "Vendor"
                gvRMInPlant.Columns("VehicleNo").HeaderText = "Vehicle No"

                For ii As Integer = 0 To gvRMInPlant.Columns.Count - 1
                    gvRMInPlant.Columns(ii).ReadOnly = True
                    gvRMInPlant.Columns(ii).IsVisible = True
                    gvRMInPlant.Columns(ii).Width = 150
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gvRMStock_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvRMStock.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMStock_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvRMStock.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvRMSupply_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvRMSupply.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMSupply_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvRMSupply.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvRMInPlant_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvRMInPlant.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvRMInPlant_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvRMInPlant.ViewCellFormatting
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


    Private Sub gvAccountVendor_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvAccountVendor.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountVendor_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAccountVendor.ViewCellFormatting
        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvAccountCustomer_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvAccountCustomer.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvAccountCustomer_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvAccountCustomer.ViewCellFormatting

        Try
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = GradientStyles.Solid
            e.CellElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.CellElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gvAccountVendor_RowFormatting_1(sender As Object, e As RowFormattingEventArgs) Handles gvAccountVendor.RowFormatting
        Try
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = System.Drawing.Color.FromArgb(0, 67, 138)
            e.RowElement.ForeColor = Color.MintCream
        Catch ex As Exception
        End Try
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







