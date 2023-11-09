'----Created By--[Pankaj Kumar Chaudhary] Against Ticket no--[BM00000001219]
Imports common
Imports System.Data.SqlClient
Imports XpertERPCommanServices

Public Class FrmBankBookChart
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim dt As DataTable
    Dim BankCode As String
    Dim strFromDate As String
    Dim strToDate As String
    Dim qry As String

    Private Sub FrmBankBookChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        isInsideLoadData = True
        SetUserMgmtNew()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        LoadFigures()
        cboType.SelectedValue = Charting.ChartSeriesType.Pie
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        chkAutoScroll.Checked = True

        isInsideLoadData = False
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBankCode._MYValidating
        Dim strWhrclas As String = ""
        qry = clsERPFuncationality.glbankqueryNew(strWhrclas)
        txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBankBookChart)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Public Function GetQry(ByVal BankCode As String, ByVal strFromDate As String, ByVal strToDate As String) As String
        qry = "Select Type+' ('+CONVERT(VARCHAR,SUM(Amount))+')' As Type, CONVERT(DECIMAL(18,2), SUM(Amount)/" + cmbFigure.SelectedValue + ") As Amount, MAX(row) As row from ("
        qry += " Select BANK_CODE, 'Opening' As Type, (isnull(Debit_Amount,0)-isnull(Credit_Amount,0)) as Amount, 1 as row from TSPL_BANK_BOOk WHERE sourceDoc_Date < '" + strFromDate + "' "
        qry += " UNION ALL"
        qry += " Select BANK_CODE, 'Receipt' as Type, Debit_Amount as Amount, 2 as row from TSPL_BANK_BOOK Where SOURCEDOC_DATE>='" + strFromDate + "' AND SOURCEDOC_DATE<='" + strToDate + "'"
        qry += " UNION ALL"
        qry += " Select BANK_CODE, 'Payment' as Type, Credit_Amount as Amount, 3 as row from TSPL_BANK_BOOK Where SOURCEDOC_DATE>='" + strFromDate + "' AND SOURCEDOC_DATE<='" + strToDate + "'"
        qry += " UNION ALL"
        qry += " Select BANK_CODE, 'Balance' as Type, Debit_Amount-Credit_Amount as Amount, 4 as row from TSPL_BANK_BOOK Where SOURCEDOC_DATE<='" + strToDate + "'"
        qry += " ) XXX WHERE XXX.BANK_CODE='" + BankCode + "' Group By XXX.Type, XXX.BANK_CODE"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            BankCode = clsCommon.myCstr(txtBankCode.Value)
            strFromDate = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy")
            strToDate = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy")
            dt = clsDBFuncationality.GetDataTable(GetQry(BankCode, strFromDate, strToDate))
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Type", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        BankCode = clsCommon.myCstr(txtBankCode.Value)
        strFromDate = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy")
        strToDate = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy")

        Try

            qry = GetQry(BankCode, strFromDate, strToDate)
            dt = clsDBFuncationality.GetDataTable(qry)

            RadChart1.ChartTitle.TextBlock.Text = ""
            RadChart1.PlotArea.XAxis.DataLabelsColumn = "Type"

            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                dt.DefaultView.Sort = "row"
                dt.Columns.Remove("row")
                RadChart1.DataManager.LabelsColumn = "Type"
            Else
                RadChart1.DataManager.LabelsColumn = "Amount"
                If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                    dt.DefaultView.Sort = "row Desc"
                    dt.Columns.Remove("row")
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
                Else
                    dt.DefaultView.Sort = "row"
                    dt.Columns.Remove("row")
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
            RadChart1.DataSource = dt.DefaultView
            RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
            RadChart1.DefaultType = cboType.SelectedValue
            RadChart1.DataBind()
            RadChart1.Update()
            LoadDetails(BankCode, strFromDate, strToDate)
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

    Private Sub LoadFigures()
        cmbFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub

    Private Sub LoadDetails(ByVal BankCode As String, ByVal FromDate As String, ByVal ToDate As String)
        Try
            Dim Qry As String = " Select [Document No], [Document Date], Type, Debit, Credit from ( "
            Qry += "Select '' As [Document No], '' As [Document Date], 'Opening Balance' as [Type], Case When SUM(OpeningBal)>0 Then SUM(OpeningBal) Else 0 End As Debit, Case When SUM(OpeningBal)<0 Then SUM(OpeningBal)*-1 Else 0 End As Credit From  ("
            Qry += " Select BANK_CODE, isnull(Debit_Amount,0)-isnull(Credit_Amount,0)  as OpeningBal "
            Qry += " from TSPL_BANK_BOOk WHERE sourceDoc_Date < '" + FromDate + "' "
            Qry += " ) XXX WHERE BANK_CODE='" + BankCode + "' Group by BANK_CODE  "
            Qry += " UNION ALL"
            Qry += " Select SOURCEDOC_NO As [Document No], CONVERT(VARCHAR,SOURCEDOC_DATE,103) As [Document Date], DocType as [Type], Debit_Amount, Credit_Amount"
            Qry += " from TSPL_BANK_BOOk WHERE BANK_CODE='" + BankCode + "' AND SOURCEDOC_DATE >='" + FromDate + "' AND  SOURCEDOC_DATE <='" + ToDate + "'"
            Qry += " ) XXXX Order By CONVERT(Date, [Document Date],103)"
            dt = clsDBFuncationality.GetDataTable(Qry)
            gvDetails.DataSource = Nothing
            gvDetails.DataSource = dt
            FormatGrid()
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadChart1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadChart1.DoubleClick
        Try
            RadPageView2.SelectedPage = RadPageViewPage5
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboSkin_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSkin.SelectedValueChanged
        Try
            RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
            RadChart1.Update()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gvDetails_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDetails.CellDoubleClick
        Try
            If clsCommon.myLen(gvDetails.CurrentRow.Cells("Document No").Value) > 0 Then
                Dim DocType As String = clsCommon.myCstr(gvDetails.CurrentRow.Cells("Type").Value)
                If clsCommon.CompairString(DocType, "Receipt") = CompairStringResult.Equal Then
                    Dim frm As New FrmReceipttNew
                    frm.SetUserMgmt(clsUserMgtCode.ReceiptEntry)
                    frm.strRcptNo = clsCommon.myCstr(gvDetails.CurrentRow.Cells("Document No").Value)
                    frm.Show()
                ElseIf clsCommon.CompairString(DocType, "Payment") = CompairStringResult.Equal Then
                    Dim frm As New FrmPaymentNew()
                    frm.SetUserMgmt(clsUserMgtCode.PaymentEntryNew)
                    frm.strPaymentNo = clsCommon.myCstr(gvDetails.CurrentRow.Cells("Document No").Value)
                    frm.Show()
                ElseIf clsCommon.CompairString(DocType, "BankTransfer") = CompairStringResult.Equal Then
                    Dim frm As New FrmBankTransfer(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    frm.SetUserMgmt(clsUserMgtCode.bankTransfer)
                    frm.strbankTrans = clsCommon.myCstr(gvDetails.CurrentRow.Cells("Document No").Value)
                    frm.Show()
                ElseIf clsCommon.CompairString(DocType, "Reverse") = CompairStringResult.Equal Then
                    Dim frm As New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    frm.SetUserMgmt(clsUserMgtCode.reverseTransaction)
                    frm.strBankRvrse = clsCommon.myCstr(gvDetails.CurrentRow.Cells("Document No").Value)
                    frm.Show()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged, cboOrientation.SelectedValueChanged
        Try
            If Not isInsideLoadData Then
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
        If Not isInsideLoadData Then
            LoadData()
        End If
    End Sub

    Private Sub chkAutoScroll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAutoScroll.ToggleStateChanged
        LoadData()
    End Sub

End Class
