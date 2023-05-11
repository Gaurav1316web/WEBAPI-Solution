Imports common
Public Class frmBIMonthWiseAsset
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim qry As String
    Dim dt As DataTable

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
        cboOrientation.DataSource = FrmBIMonthWiseSale.GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BIMonthWiseAssset)
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

    Sub LoadChartType()
        cboType.DataSource = FrmBIMonthWiseSale.GetChartType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Public Function GetQry() As String
        Try
            Dim tempdt As Date = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
            Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(tempdt), "dd/MMM/yyyy hh:mm tt")
            tempdt = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1)
            tempdt = tempdt.AddMonths(1)
            tempdt = tempdt.AddDays(-1)
            Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tempdt), "dd/MMM/yyyy hh:mm tt")


            qry = "select DateMonthOrder, max(DateMonth) as DateMonth, CONVERT(Decimal(18,2), SUM(Book_Source_value)/" + cmbFigure.SelectedValue + ") as Amount from( "
            qry += " select Book_Source_value,TSPL_ACQUISITION_HEAD.Acquisition_Date,REPLACE( SUBSTRING( CONVERT(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,102),0,8),'.','-') as DateMonthOrder,SUBSTRING(CONVERT(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,106),3,5 )+'-'+SUBSTRING(CONVERT(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,106),10,11 ) as DateMonth  "
            qry += " from TSPL_ACQUISITION_DETAIL "
            qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
            qry += " where TSPL_ACQUISITION_HEAD.Status=1 and TSPL_ACQUISITION_HEAD.Acquisition_Date>='" + strFromDate + "' and TSPL_ACQUISITION_HEAD.Acquisition_Date<='" + strToDate + "'  "
            qry += " )xxx group by DateMonthOrder order by DateMonthOrder"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            qry = GetQry()
            dt = clsDBFuncationality.GetDataTable(qry)
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "DateMonth", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData()
        isInsideLoadData = True
        qry = GetQry()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
