Imports common
Public Class FrmBIMonthWisePurchase
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim qry As String
    Dim dt As DataTable
    Private Sub FrmBIMonthWiseSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isInsideLoadData = True
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-12)
        LoadFigures()
        chkAutoScroll.Checked = True
        LoadData()
        isInsideLoadData = False
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BIMonthWisePurchase)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadSkinType()
        cboSkin.DataSource = FrmBIMonthWiseSale.GetChartSkin()
        cboSkin.ValueMember = "Code"
        cboSkin.DisplayMember = "Code"

    End Sub

    Sub LoadOrientation()
        cboOrientation.DataSource = FrmBIMonthWiseSale.GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Sub LoadChartType()
        cboType.DataSource = FrmBIMonthWiseSale.GetChartType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub
    'Dim cc As Integer = 2
    'Public Function GetxData(ByVal ii As Integer) As DataTable
    '    Dim dt As New DataTable()
    '    dt.Columns.Add("DateMonth", GetType(String))
    '    dt.Columns.Add("Amount", GetType(Double))
    '    Dim dr As DataRow = dt.NewRow()
    '    dr("DateMonth") = "BabyBlue"
    '    dr("Amount") = 1000
    '    dt.Rows.Add(dr)
    '    If 2 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Black"
    '        dr("Amount") = 10000
    '        dt.Rows.Add(dr)
    '        cc = 3
    '    End If
    '    If 3 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Blue"
    '        dr("Amount") = 500
    '        dt.Rows.Add(dr)
    '        cc = 5
    '    End If
    '    If 5 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "BlueStripes"
    '        dr("Amount") = 7000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Brick"
    '        dr("Amount") = 10000
    '        dt.Rows.Add(dr)
    '        cc = 10
    '    End If


    '    If 10 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Classic"
    '        dr("Amount") = 12000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Colorful"
    '        dr("Amount") = 16000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "DeepBlue"
    '        dr("Amount") = 14000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "DeepGray"
    '        dr("Amount") = 9000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "DeepGreen"
    '        dr("Amount") = 2500
    '        dt.Rows.Add(dr)
    '        cc = 15
    '    End If


    '    If 15 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "DeepRed"
    '        dr("Amount") = 1000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Default"
    '        dr("Amount") = 1000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "BlueStripes"
    '        dr("Amount") = 10400
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Default2006"
    '        dr("Amount") = 154000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Desert"
    '        dr("Amount") = 76000
    '        dt.Rows.Add(dr)
    '        cc = 20
    '    End If



    '    If 20 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "ExcelClassic"
    '        dr("Amount") = 70000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Forest"
    '        dr("Amount") = 100900
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Gradient"
    '        dr("Amount") = 10060
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Gray"
    '        dr("Amount") = 10500
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "GrayStripes"
    '        dr("Amount") = 14000
    '        dt.Rows.Add(dr)
    '        cc = 25
    '    End If



    '    If 25 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "LightBrown"
    '        dr("Amount") = 14300
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "LightGreen"
    '        dr("Amount") = 13400
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Mac"
    '        dr("Amount") = 14000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Marble"
    '        dr("Amount") = 145000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Metal"
    '        dr("Amount") = 143000
    '        dt.Rows.Add(dr)
    '        cc = 30
    '    End If

    '    If 30 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Office2007"
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Outlook"
    '        dr("Amount") = 106500
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Pastel"
    '        dr("Amount") = 10090
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "SkyBlue"
    '        dr("Amount") = 10800
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Sunset"
    '        dr("Amount") = 10400
    '        dt.Rows.Add(dr)
    '        cc = 35
    '    End If
    '    If 35 <= ii Then
    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Green"
    '        dr("Amount") = 12000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "GreenStripes"
    '        dr("Amount") = 4000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Hay"
    '        dr("Amount") = 18000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "Inox"
    '        dr("Amount") = 54000
    '        dt.Rows.Add(dr)

    '        dr = dt.NewRow()
    '        dr("DateMonth") = "LightBlue"
    '        dr("Amount") = 54000
    '        dt.Rows.Add(dr)
    '        cc = 2
    '    End If
    '    Return dt
    'End Function

    Public Function GetQry() As String
        Dim tempdt As Date = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        tempdt = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1)
        tempdt = tempdt.AddMonths(1)
        tempdt = tempdt.AddDays(-1)
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tempdt), "dd/MMM/yyyy hh:mm tt")

        Dim qry As String = "select DateMonthOrder, max(DateMonth) as DateMonth,CONVERT(Decimal(18,2), SUM(Amount_Less_Discount)/" + cmbFigure.SelectedValue + ") as Amount from("
        qry += " select Amount_Less_Discount,PI_Date,REPLACE( SUBSTRING( CONVERT(varchar,PI_Date,102),0,8),'.','-') as DateMonthOrder,SUBSTRING(CONVERT(varchar,PI_Date,106),3,5 )+'-'+SUBSTRING(CONVERT(varchar,PI_Date,106),10,11 ) as DateMonth  from TSPL_PI_HEAD where TSPL_PI_HEAD.Status=1 and PI_Date>='" + strFromDate + "' and PI_Date<='" + strToDate + "'  )xxx group by DateMonthOrder order by DateMonthOrder"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            dt = clsDBFuncationality.GetDataTable(GetQry())
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "DateMonth", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData()
        isInsideLoadData = True
        qry = GetQry()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry) '' GetData(cc) ''
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
