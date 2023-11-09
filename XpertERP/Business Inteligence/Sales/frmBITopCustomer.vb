Imports common
Public Class frmBITopCustomer
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Private Sub FrmBIMonthWiseSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isInsideLoadData = True
        LoadFigures()
        LoadChartType()
        LoadSkinType()
        LoadTop()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-12)
        cboTop.SelectedValue = 5
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
        'MyBase.SetUserMgmt(clsUserMgtCode.BITopCustomer)
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

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQuery(txtFromDate.Value, txtToDate.Value, cmbFigure.SelectedValue, clsCommon.myCdbl(cboTop.SelectedValue)))
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Customer_Name", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function GetQuery(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal FigureIn As Integer, ByVal TopLimit As Integer) As String
        Dim tempdt As Date = New Date(dtFrom.Year, dtFrom.Month, 1)
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        tempdt = New Date(dtTo.Year, dtTo.Month, 1)
        tempdt = tempdt.AddMonths(1)
        tempdt = tempdt.AddDays(-1)
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        Dim qry As String = "select top " + clsCommon.myCstr(clsCommon.myCdbl(TopLimit)) + "  max(Customer_Name ) as Customer_Name , CONVERT(DECIMAL(18,2), SUM(Total_Amt)/" + clsCommon.myCstr(FigureIn) + ") as Amount from("

        If objCommonVar.IsDemoERP Then
            qry += " select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt   from TSPL_SD_SALE_INVOICE_HEAD  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "' "
        Else
            qry += " select TSPL_SALE_INVOICE_HEAD.Cust_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt as Total_Amt from TSPL_SALE_INVOICE_DETAIL left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code where TSPL_SALE_INVOICE_HEAD.Is_Post='Y' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + strToDate + "' "
        End If
        qry += " )xxx group by Customer_Code order by Amount desc"
        Return qry
    End Function

    Sub LoadData()
        isInsideLoadData = True
      
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQuery(txtFromDate.Value, txtToDate.Value, cmbFigure.SelectedValue, clsCommon.myCdbl(cboTop.SelectedValue)))
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
