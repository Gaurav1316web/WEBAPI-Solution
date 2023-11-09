Imports common
Public Class frmBITopExpence
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim qry As String
    Dim dt As DataTable

    Private Sub FrmBIMonthWiseSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isInsideLoadData = True
        LoadChartType()
        LoadSkinType()
        LoadTop()
        LoadOrientation()
        LoadFigures()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-12)
        cboTop.SelectedValue = 5
        LoadData()
        isInsideLoadData = False
        chkAutoScroll.Checked = True
    End Sub

    Sub LoadFigures()
        cboFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cboFigure.ValueMember = "Value"
        cboFigure.DisplayMember = "Code"
    End Sub

    Sub LoadOrientation()
        cboOrientation.DataSource = FrmBIMonthWiseSale.GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BITopExpence)
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

    Sub LoadTop()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 1
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 2
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 3
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 4
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 5
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 6
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 7
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 8
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 9
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 10
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 11
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 12
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 13
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 14
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 15
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 16
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 17
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 18
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 19
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 20
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 21
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 22
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 23
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 24
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 25
        dt.Rows.Add(dr)

        cboTop.DataSource = dt
        cboTop.ValueMember = "Code"
        cboTop.DisplayMember = "Code"
    End Sub

    Public Function GetQry(ByVal fromDate As Date, ByVal toDate As Date, ByVal FigureDivider As Integer, ByVal TopLimit As Integer) As String
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm tt")
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(toDate), "dd/MMM/yyyy hh:mm tt")
        qry = "select top " + clsCommon.myCstr(TopLimit) + "  Account_code as Code,MAX(Description) as Customer_Name,convert(decimal(18,2), SUM(amount)/" + clsCommon.myCstr(FigureDivider) + ") as Amount from ("
        qry += "  select TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description,Amount from TSPL_JOURNAL_DETAILS left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code where TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_GL_ACCOUNTS.Account_Type='Income Statement' and Amount>0  and TSPL_JOURNAL_MASTER.Voucher_Date>='" + strFromDate + "' and TSPL_JOURNAL_MASTER.Voucher_Date<='" + strToDate + "' "
        qry += " )xxx group by Account_code order by Amount desc"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            dt = clsDBFuncationality.GetDataTable(GetQry(txtFromDate.Value, txtToDate.Value, clsCommon.myCdbl(cboFigure.SelectedValue), clsCommon.myCdbl(cboTop.SelectedValue)))
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Customer_Name", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        isInsideLoadData = True
        
        dt = clsDBFuncationality.GetDataTable(GetQry(txtFromDate.Value, txtToDate.Value, clsCommon.myCdbl(cboFigure.SelectedValue), clsCommon.myCdbl(cboTop.SelectedValue)))
        RadChart1.ChartTitle.TextBlock.Text = ""
        RadChart1.PlotArea.XAxis.DataLabelsColumn = "Customer_Name"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                RadChart1.DataManager.LabelsColumn = "Customer_Name"
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
                        RadChart1.Height = (120 * dt.Rows.Count)
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

                    Else
                        RadChart1.Width = (120 * dt.Rows.Count)
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

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged, cboOrientation.SelectedValueChanged, cboFigure.SelectedValueChanged
        Try
            LoadData()
            'If Not isInsideLoadData Then
            '    RadChart1.DefaultType = cboType.SelectedValue
            '    If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
            '        RadChart1.DataManager.LabelsColumn = "Amount"
            '    Else
            '        RadChart1.DataManager.LabelsColumn = "Customer_Name"
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
End Class
