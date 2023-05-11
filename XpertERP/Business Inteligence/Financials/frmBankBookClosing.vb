'----Created By--[Pankaj Kumar Chaudhary] Against Ticket no--[BM00000001220]
Imports common
Imports Telerik.Charting

Public Class FrmBankBookClosing
    Inherits FrmMainTranScreen
    Dim IsFormLoad As Boolean = False
    Dim strToDate As String
    Dim dt As DataTable

    Private Sub FrmBankBookClosing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IsFormLoad = True
        SetUserMgmtNew()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtDate.Value = clsCommon.GETSERVERDATE()
        LoadBanks()
        LoadParameters()
        cbgBanks.CheckedAll()
        chkAutoScroll.Checked = True
        IsFormLoad = False
        LoadData()
       
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBankBookClosing)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadBanks()
        Dim qry As String
        qry = " select BANK_CODE, DESCRIPTION  from TSPL_BANK_MASTER "
        cbgBanks.ValueMember = "BANK_CODE"
        cbgBanks.DisplayMember = "DESCRIPTION"
        cbgBanks.DataSource = clsDBFuncationality.GetDataTable(qry)
    End Sub

    Public Function LoadFigures() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))
        dt.Rows.Add("Original", "1")
        dt.Rows.Add("Hundreds", "100")
        dt.Rows.Add("Thousands", "1000")
        dt.Rows.Add("Lacks", "100000")
        dt.Rows.Add("Crores", "10000000")
        Return dt
    End Function

    Private Sub LoadParameters()
        cmbFigure.DataSource = LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub

    Public Function GetBankClosingQry(ByVal asOnDate As String, ByVal figure As String, ByVal isForAllBank As Boolean, ByVal arrBank As ArrayList) As String
        Dim qry As String = "Select Bank_Code as Bank, CONVERT(Decimal(18,2), SUM(Debit_Amount-Credit_Amount)/" + figure + ") as ClosingBal from TSPL_BANK_BOOK "
        qry += " Where SOURCEDOC_DATE<='" + asOnDate + "' "

        If Not isForAllBank Then
            qry += " AND BANK_CODE in (" + clsCommon.GetMulcallString(arrBank) + ")"
        End If

        qry += "GROUP BY BANK_CODE ORDER BY BANK_CODE"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            dt = clsDBFuncationality.GetDataTable(GetBankClosingQry(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"), cmbFigure.SelectedValue, False, cbgBanks.CheckedValue))
            SnDUtility.GenerateExcelChart(dt, EnuChartType.Bar, "", "Bank", "ClosingBal")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData()
        Try
            RadChart2.DataSource = Nothing
            gvDetails.DataSource = Nothing
            If cbgBanks.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select bank.")
            End If
            
            dt = clsDBFuncationality.GetDataTable(GetBankClosingQry(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"), cmbFigure.SelectedValue, False, cbgBanks.CheckedValue))

            RadChart1.ChartTitle.TextBlock.Text = ""
            RadChart1.PlotArea.XAxis.DataLabelsColumn = "Bank"

            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                RadChart1.DataManager.LabelsColumn = "Bank"
            Else
                RadChart1.DataManager.LabelsColumn = "ClosingBal"
                If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
                Else
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical
                End If
            End If

            RadChart1.Height = Me.SplitContainer2.Panel1.Height - 20
            RadChart1.Width = Me.SplitContainer2.Panel1.Width - 5
            Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            If chkAutoScroll.Checked Then
                If cboType.SelectedValue = Charting.ChartSeriesType.Bar Then
                    If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                        RadChart1.Width = Me.SplitContainer2.Panel1.Width - 5
                        RadChart1.Height = (120 * dt.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                    Else
                        RadChart1.Width = (120 * dt.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        RadChart1.Height = Me.SplitContainer2.Panel1.Height - 20
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
            RadChart1.Series(0).DataYColumn = "ClosingBal"
            RadChart1.Series(0).DataXColumn = "Bank"
            RadChart1.Series(0).Name = "Bank"
            RadPageView2.SelectedPage = Page1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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

    Sub LoadOrientation()
        cboOrientation.DataSource = FrmBIMonthWiseSale.GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Private Sub LoadDetails()
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub cboSkin_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSkin.SelectedValueChanged
        Try
            If Not IsFormLoad Then
                RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
                RadChart1.Update()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkAutoScroll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAutoScroll.ToggleStateChanged
        LoadData()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadChart1_Click(ByVal sender As System.Object, ByVal args As Telerik.Charting.ChartClickEventArgs) Handles RadChart1.Click
        Try
            If TypeOf args.Element.ActiveRegion.Parent Is ChartSeriesItem AndAlso (TryCast(args.Element.ActiveRegion.Parent, ChartSeriesItem)).Parent.Name = "Bank" Then
                Dim BankCode As String = clsCommon.myCstr(dt.Rows((TryCast(args.Element, ChartSeriesItem)).Index)("Bank"))
                LoadDetails(BankCode, strToDate)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadDetails(ByVal BankCode As String, ByVal ToDate As String)
        RadChart2.DataSource = Nothing
        gvDetails.DataSource = Nothing
        Try
            Dim Qry As String
            Qry = "Select RR.Type +' ('+CONVERT(VARCHAR,CONVERT(Decimal(18,2),SUM(xxx.Amount)/" + cmbFigure.SelectedValue + "))+')' As Type, SUM(XXX.Amount) As Amount from ("
            Qry += " Select BANK_CODE, 'Receipt' as Type, Debit_Amount as Amount from TSPL_BANK_BOOK Where ISNULL(Debit_Amount,0)>0 AND SOURCEDOC_DATE<='" + ToDate + "'"
            Qry += " UNION ALL"
            Qry += " Select BANK_CODE, 'Payment' as Type, Credit_Amount as Amount from TSPL_BANK_BOOK Where ISNULL(Credit_Amount ,0)>0 AND SOURCEDOC_DATE<='" + ToDate + "'"
            Qry += " UNION ALL"
            Qry += " Select BANK_CODE, 'Balance' as Type, Debit_Amount-Credit_Amount as Amount from TSPL_BANK_BOOK Where SOURCEDOC_DATE<='" + ToDate + "'"
            Qry += " ) XXX Right Join (Select 'Receipt' As Type, 0 as Amount UNION Select 'Payment' As Type, 0 as Amount UNION Select 'Balance' As Type, 0 as Amount) RR ON RR.Type=XXX.Type WHERE XXX.BANK_CODE='" + BankCode + "' Group By RR.Type, XXX.BANK_CODE"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)

            RadChart2.ChartTitle.TextBlock.Text = ""
            RadChart2.PlotArea.XAxis.DataLabelsColumn = "Type"
            RadChart2.DataManager.LabelsColumn = "Type"

            RadChart2.AutoTextWrap = True
            RadChart2.IntelligentLabelsEnabled = True
            RadChart2.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart2.Series.Clear()
            RadChart2.DataSource = Nothing
            RadChart2.DataSource = dt1
            RadChart2.DataBind()
            RadChart2.Update()

            Qry = " Select [Document No], [Document Date], Type, Debit_Amount As Debit, Credit_Amount As Credit from ( "
            Qry += " Select SOURCEDOC_NO As [Document No], CONVERT(VARCHAR,SOURCEDOC_DATE,103) As [Document Date], DocType as [Type], Debit_Amount, Credit_Amount"
            Qry += " from TSPL_BANK_BOOk WHERE BANK_CODE='" + BankCode + "' AND SOURCEDOC_DATE <='" + ToDate + "'"
            Qry += " ) XXXX Order By CONVERT(Date, [Document Date],103)"
            dt1 = clsDBFuncationality.GetDataTable(Qry)
            gvDetails.DataSource = Nothing
            gvDetails.DataSource = dt1
            FormatGrid()
            RadPageView2.SelectedPage = Page2
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvDetails.Columns("Document No").Width = 150
        gvDetails.Columns("Document Date").Width = 150
        gvDetails.Columns("Type").Width = 140
        gvDetails.Columns("Debit").Width = 151
        gvDetails.Columns("Credit").Width = 151

        gvDetails.SummaryRowsBottom.Clear()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SUMDrAmt As New GridViewSummaryItem("Debit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDrAmt)
        Dim SUMCrAmt As New GridViewSummaryItem("Credit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMCrAmt)

        gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub


    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
        If Not IsFormLoad Then
            LoadData()
        End If
    End Sub

    Private Sub RadChart2_Click(ByVal sender As System.Object, ByVal args As Telerik.Charting.ChartClickEventArgs) Handles RadChart2.Click
        RadPageView2.SelectedPage = Page3
    End Sub

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged, cboOrientation.SelectedValueChanged
        Try
            If Not IsFormLoad Then
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
