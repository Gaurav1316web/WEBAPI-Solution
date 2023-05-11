Imports common
Public Class frmBITopItemCategory
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim qry As String
    Dim strQtyOrAmt As String
    Dim dt As DataTable

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
        'MyBase.SetUserMgmt(clsUserMgtCode.BITopItemCategory)
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

    Public Function GetQry() As String
        isInsideLoadData = True
        Dim tempdt As Date = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(tempdt), "dd/MMM/yyyy hh:mm tt")
        tempdt = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1)
        tempdt = tempdt.AddMonths(1)
        tempdt = tempdt.AddDays(-1)
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tempdt), "dd/MMM/yyyy hh:mm tt")

        strQtyOrAmt = "Qty"
        If rbtnAmt.IsChecked Then
            strQtyOrAmt = "Amt"
        End If

        qry = "select Category, CONVERT(Decimal(18,2), " + strQtyOrAmt + "/" + cmbFigure.SelectedValue + ") As " + strQtyOrAmt + " from(  select top " + clsCommon.myCstr(clsCommon.myCdbl(cboTop.SelectedValue)) + "  item_category,MAX(Category_Name) as Category,sum(" + strQtyOrAmt + ")as " + strQtyOrAmt + " from ("
        qry += " select TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.item_category,TSPL_Item_Category.Category_Name,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as Amt "
        qry += " from TSPL_SD_SALE_INVOICE_DETAIL "
        qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
        qry += " left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER.item_category  where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and Document_Date>='" + strFromDate + "' and Document_Date<='" + strToDate + "' "
        qry += " )xxx group by item_category )xxxx order by " + strQtyOrAmt + " desc"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            dt = clsDBFuncationality.GetDataTable(GetQry())
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Category", strQtyOrAmt)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData()
        isInsideLoadData = True

        qry = GetQry()

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        RadChart1.ChartTitle.TextBlock.Text = ""
        RadChart1.PlotArea.XAxis.DataLabelsColumn = "Category"
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                RadChart1.DataManager.LabelsColumn = "Category"
            Else
                RadChart1.DataManager.LabelsColumn = strQtyOrAmt
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
            '        RadChart1.DataManager.LabelsColumn = "Category"
            '    Else
            '        RadChart1.DataManager.LabelsColumn = "Qty"
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
    
    Private Sub cboOrientation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboOrientation.SelectedIndexChanged

    End Sub
    Private Sub rbtnQuantity_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnQuantity.ToggleStateChanged

    End Sub
    Private Sub rbtnAmt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAmt.ToggleStateChanged

    End Sub
End Class
