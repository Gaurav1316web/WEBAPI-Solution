Imports common
Public Class FrmBIMonthWiseSale
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Private Sub FrmBIMonthWiseSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isInsideLoadData = True
        LoadFigures()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-12)
        chkAutoScroll.Checked = True
        LoadData()
        isInsideLoadData = False
    End Sub

    Sub LoadOrientation()
        cboOrientation.DataSource = GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BIMonthWiseSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadSkinType()
        cboSkin.DataSource = GetChartSkin()
        cboSkin.ValueMember = "Code"
        cboSkin.DisplayMember = "Code"
    End Sub

    Sub LoadChartType()
        cboType.DataSource = GetChartType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Public Function GetChartType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Charting.ChartSeriesType))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = Charting.ChartSeriesType.Bar
        dr("Name") = "Bar"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = Charting.ChartSeriesType.Line
        dr("Name") = "Line"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = Charting.ChartSeriesType.Area
        dr("Name") = "Area"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = Charting.ChartSeriesType.Pie
        dr("Name") = "Pie"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Public Function GetChartOrientation() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Vertical"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Horizontal"
        dt.Rows.Add(dr)

       
        Return dt
    End Function

    Public Function GetChartSkin() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = "BabyBlue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        dr("Code") = "Black"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Blue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "BlueStripes"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Brick"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Classic"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Colorful"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "DeepBlue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "DeepGray"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "DeepGreen"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DeepRed"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Default"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "BlueStripes"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Default2006"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Desert"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "ExcelClassic"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Forest"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Gradient"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Gray"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "GrayStripes"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Green"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "GreenStripes"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Hay"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Inox"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "LightBlue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "LightBrown"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "LightGreen"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Mac"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Marble"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Metal"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Office2007"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Outlook"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pastel"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "SkyBlue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Sunset"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Telerik"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "UltraGreen"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Vista"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Web"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Web20"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "WebBlue"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Wood"
        'dt.Rows.Add(dr)
        Return dt
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQuery(txtFromDate.Value, txtToDate.Value, clsCommon.myCdbl(cmbFigure.SelectedValue)))
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "DateMonth", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function GetQuery(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal FigureIn As Integer) As String
        Dim tempdt As Date = New Date(dtFrom.Year, dtFrom.Month, 1)
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        tempdt = New Date(dtto.Year, dtto.Month, 1)
        tempdt = tempdt.AddMonths(1)
        tempdt = tempdt.AddDays(-1)
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        Dim qry As String = "select DateMonthOrder, max(DateMonth) as DateMonth, CONVERT(Decimal(18,2), SUM(Amount_Less_Discount)/" + clsCommon.myCstr(FigureIn) + ") as Amount from("
        If objCommonVar.IsDemoERP Then
            qry += " select Amount_Less_Discount,Document_Date,REPLACE( SUBSTRING( CONVERT(varchar,Document_Date,102),0,8),'.','-') as DateMonthOrder,SUBSTRING(CONVERT(varchar,Document_Date,106),3,5 )+'-'+SUBSTRING(CONVERT(varchar,Document_Date,106),10,11 ) as DateMonth  from TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "' "
        Else
            qry += " select Total_Invoice_Amt-Inv_Detail_Disc_Amt as Amount_Less_Discount,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as Document_Date,REPLACE( SUBSTRING( CONVERT(varchar,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,102),0,8),'.','-') as DateMonthOrder,SUBSTRING(CONVERT(varchar,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,106),3,5 )+'-'+SUBSTRING(CONVERT(varchar,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,106),10,11 ) as DateMonth  from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= '" + strToDate + "' "
        End If
        qry += " )xxx group by DateMonthOrder order by DateMonthOrder"
        Return qry
    End Function

    Sub LoadData()
        isInsideLoadData = True
       

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQuery(txtFromDate.Value, txtToDate.Value, clsCommon.myCdbl(cmbFigure.SelectedValue))) '' GetData(cc) ''
        RadChart1.ChartTitle.TextBlock.Text = ""
        RadChart1.PlotArea.XAxis.DataLabelsColumn = "DateMonth"

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                RadChart1.DataManager.LabelsColumn = "DateMonth"
            Else
                RadChart1.DataManager.LabelsColumn = "Amount"
                If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
                Else
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical
                End If
            End If

            RadChart1.Height = Me.SplitContainer1.Panel2.Height - 5
            RadChart1.Width = Me.SplitContainer1.Panel2.Width - 5
            Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            If chkAutoScroll.Checked Then
                If cboType.SelectedValue = Charting.ChartSeriesType.Bar Then
                    If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                        RadChart1.Width = Me.SplitContainer1.Panel2.Width - 5
                        RadChart1.Height = (120 * dt.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

                    Else
                        RadChart1.Width = (120 * dt.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        RadChart1.Height = Me.SplitContainer1.Panel2.Height - 5
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles)
                    End If
                End If
            End If

            RadChart1.AutoTextWrap = True
            RadChart1.IntelligentLabelsEnabled = True
            RadChart1.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart1.Series.Clear()
            RadChart1.DataSource = Nothing
            RadChart1.DataSource = dt
            RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
            RadChart1.DefaultType = cboType.SelectedValue
            RadChart1.DataBind()
            RadChart1.Update()
        End If
        isInsideLoadData = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        LoadData()
    End Sub

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged, cboOrientation.SelectedValueChanged
        Try
            LoadData()
            'If Not isInsideLoadData Then
            '    RadChart1.DefaultType = cboType.SelectedValue
            '    If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
            '        RadChart1.DataManager.LabelsColumn = "DateMonth"
            '    Else
            '        RadChart1.DataManager.LabelsColumn = "Amount"
            '    End If
            '    RadChart1.Update()
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboSkin_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSkin.SelectedValueChanged
        Try
            If Not isInsideLoadData Then
                RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
                RadChart1.Update()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkAutoScroll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAutoScroll.ToggleStateChanged
        LoadData()
    End Sub

    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
        If Not isInsideLoadData Then
            LoadData()
        End If
    End Sub

    Private Sub LoadFigures()
        cmbFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub
End Class
