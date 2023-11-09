'----Created By--[Pankaj Kumar Chaudhary] Against Ticket no--[BM00000001163]
Imports common
Imports System.Data.SqlClient
Imports Telerik.Charting

Public Class FrmBIVendorAgeing
    Inherits FrmMainTranScreen
    Dim IsFormLoad As Boolean = False
    Dim strToDate As String
    Dim qry As String
    Dim dt As DataTable

    Private Sub FrmBIVendorAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IsFormLoad = True
        SetUserMgmtNew()
        LoadVendors()
        SetMultiCurrencyVisibility()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtDate.Value = clsCommon.GETSERVERDATE()

        cbgVendor.CheckedAll()
        LoadFigures()
        chkAutoScroll.Checked = True
        IsFormLoad = False

       
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorAgingSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Sub LoadVendors()
        Dim qry As String = " Select Vendor_Code As Code, Vendor_Name As Name from TSPL_VENDOR_MASTER "
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)

    End Sub

    Private Sub LoadFigures()
        cmbFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub

    Public Function GetQry(ByVal strAsOnDate As Date, ByVal Figure As String, ByVal isForAllVendor As Boolean, ByVal arrVendor As ArrayList) As String
        Try
            strToDate = clsCommon.GetPrintDate(strAsOnDate, "dd/MMM/yyyy")
            Dim currencycode As String = ""
            If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                currencycode = " and TSPL_VENDOR_MASTER.CURRENCY_CODE='" + clsCommon.myCstr(txtCurrencyCode.Value) + "'"
            End If

            Dim qry As String
            If cbgVendor.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single customer.")
            End If
            qry = "Select YYY.Bucket, CONVERT(Decimal(18,2), SUM(ISNULL(Amount,0))/" + Figure + ") As Amount from ("

            qry += " Select Vendor_Code, DocNo, DocDate, Case when DocType IN ('D','AV','OA') then Amount*-1 Else Amount End As Amount, Case When AgingDays<=30 Then '0 - 30' When (AgingDays>30 AND AgingDays<=60) Then '31 - 60' When (AgingDays>60 AND AgingDays<=90) Then '61 - 90' WHEN (AgingDays>90 AND AgingDays<=120) Then '91 - 120' WHEN (AgingDays>120 AND AgingDays<=150) Then '121 - 150' Else 'Over 150' End As Bucket  from (  "

            qry += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_code, Document_No as DocNo , Document_Type as DocType, COnvert(Date,Invoice_Entry_Date, 103) as DocDate, Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  TSPL_PAYMENT_HEADER.Payment_Date< CONVERT(DATE,'" + strToDate + "',103)),0)-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type='D' AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End as Amount, DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" + strToDate + "') as AgingDays from TSPL_VENDOR_INVOICE_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code  where  Balance_Amt >0 AND ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''  " + currencycode + "  "
            qry += " UNION ALL  "
            qry += " select  TSPL_PAYMENT_HEADER.Vendor_code, TSPL_PAYMENT_HEADER.Payment_No as DocNo, TSPL_PAYMENT_HEADER.Payment_Type as DocType, Convert(Date,Payment_Date, 103) as DocDate,  Balance_Amt+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N') else 0 end ),0) as Amount, DATEDIFF(dd,convert(date,Payment_Date,103), '" + strToDate + "') as AgingDays from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PAYMENT_HEADER.vendor_code where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1') " + currencycode + "  "
            qry += " UNION ALL  "
            qry += " select  VC_Code as Vendor_code, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as DocType, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, Amount, DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103) , '" + strToDate + "') as AgingDays from TSPL_VCGL_Head left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VCGL_Head.VC_Code where Document_Type='v' and TSPL_VCGL_Head.Status='1' " + currencycode + "  "
            qry += " UNION ALL  "
            qry += " select  TSPL_VCGL_Detail.VCGL_Code as Vendor_code, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as DocType, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as Amount, DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103), '" + strToDate + "') as AgingDays from  TSPL_VCGL_Detail  left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VCGL_Head.VC_Code  where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'  " + currencycode + " "
            qry += " ) xxx where Convert(Date, DocDate, 103) <='" + strToDate + "'  and DocType  in ('I','C','D','AV','OA','P','RC' ) "

            If cbgVendor.CheckedValue.Count > 0 Then
                qry += " AND XXX.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
            End If

            qry += " ) Query Right JOIN (Select '0 - 30' as Bucket, 1 as odr UNION Select '31 - 60' as Bucket, 2 as odr UNION Select '61 - 90' as Bucket, 3 as odr UNION Select '91 - 120' as Bucket, 4 as odr UNION Select '121 - 150' as Bucket, 5 as odr UNION Select 'Over 300' as Bucket, 6 as odr) YYY ON YYY.Bucket=Query.Bucket Group By YYY.Bucket Order by MAX(odr) "
            Return qry


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            qry = GetQry(txtDate.Value, cmbFigure.SelectedValue, False, cbgVendor.CheckedValue)
            dt = clsDBFuncationality.GetDataTable(qry)
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Bucket", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Try
            qry = GetQry(txtDate.Value, cmbFigure.SelectedValue, False, cbgVendor.CheckedValue)
            dt = clsDBFuncationality.GetDataTable(qry)
            RadChart1.ChartTitle.TextBlock.Text = ""
            RadChart1.PlotArea.XAxis.DataLabelsColumn = "Bucket"

            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                RadChart1.DataManager.LabelsColumn = "Bucket"
            Else
                RadChart1.DataManager.LabelsColumn = "Amount"
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
            RadChart1.Series(0).DataYColumn = "Amount"
            RadChart1.Series(0).DataXColumn = "Bucket"
            RadChart1.Series(0).Name = "Bucket"
            RadPageView2.SelectedPage = RadPageViewPage4
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

    Private Sub LoadDetails(ByVal strToDate As String, ByVal bucket As String)
        gvDetails.DataSource = Nothing
        Try
            Dim currencycode As String = ""
            If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                currencycode = " and tspl_vendor_master.currency_code='" + clsCommon.myCstr(txtCurrencyCode.Value) + "'"
            End If

            Dim Qry As String
            Qry = "Select * from ( "

            Qry += " Select XXX.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], DocNo As [Document No], DocDate as [Document Date], Case when DocType IN ('D','AV','OA') then Amount*-1 Else Amount End As Amount, Case When AgingDays<=30 Then '0 - 30' When (AgingDays>30 AND AgingDays<=60) Then '31 - 60' When (AgingDays>60 AND AgingDays<=90) Then '61 - 90' WHEN (AgingDays>90 AND AgingDays<=120) Then '91 - 120' WHEN (AgingDays>120 AND AgingDays<=150) Then '121 - 150' When (AgingDays>150 AND AgingDays<=180) Then '151 - 180' When (AgingDays>180 AND AgingDays<=210) Then '181 - 210' When (AgingDays>210 AND AgingDays<=240) Then '211 - 240' When (AgingDays>240 AND AgingDays<=270) Then '241 - 270' When (AgingDays>270 AND AgingDays<=300) Then '271 - 300' Else 'Over 300' End As Bucket  from (  "

            Qry += " select TSPL_VENDOR_INVOICE_HEAD.Vendor_code, Document_No as DocNo , Document_Type as DocType, COnvert(Date,Invoice_Entry_Date, 103) as DocDate, Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  TSPL_PAYMENT_HEADER.Payment_Date< CONVERT(DATE,'" + strToDate + "',103)),0)-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type='D' AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End as Amount, DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" + strToDate + "') as AgingDays from TSPL_VENDOR_INVOICE_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VENDOR_INVOICE_HEAD.vendor_code  where  Balance_Amt >0 AND ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> '' " + currencycode + "    "
            Qry += " UNION ALL "
            Qry += " select  TSPL_PAYMENT_HEADER.Vendor_code, TSPL_PAYMENT_HEADER.Payment_No as DocNo, TSPL_PAYMENT_HEADER.Payment_Type as DocType, Convert(Date,Payment_Date, 103) as DocDate,  Balance_Amt+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N') else 0 end ),0) as Amount, DATEDIFF(dd,convert(date,Payment_Date,103), '" + strToDate + "') as AgingDays from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PAYMENT_HEADER.vendor_code where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1') " + currencycode + "   "
            Qry += " UNION ALL   "
            Qry += " select  VC_Code as Vendor_code, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as DocType, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, Amount, DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103) , '" + strToDate + "') as AgingDays from TSPL_VCGL_Head left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VCGL_Head.vc_code where Document_Type='v' and TSPL_VCGL_Head.Status='1' " + currencycode + "   "
            Qry += " UNION ALL   "
            Qry += " select  TSPL_VCGL_Detail.VCGL_Code as Vendor_code, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as DocType, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as Amount, DATEDIFF(dd,convert(date,TSPL_VCGL_Head.Posting_Date,103), '" + strToDate + "') as AgingDays from  TSPL_VCGL_Detail  left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_VCGL_Detail.VCGL_Code where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' " + currencycode + "   "

            Qry += " ) xxx LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=xxx.Vendor_Code where Convert(Date, DocDate, 103) <='" + strToDate + "'  and DocType  in ('I','C','D','AV','OA','P','RC' )  AND XXX.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") " + currencycode + " "

            Qry += " ) Query WHERE Bucket='" + bucket + "' Order by [Vendor Code]"

            gvDetails.DataSource = Nothing
            gvDetails.DataSource = clsDBFuncationality.GetDataTable(Qry)
            FormatGrid()
            RadPageView2.SelectedPage = RadPageViewPage5
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvDetails.Columns("Vendor Code").Width = 100
        gvDetails.Columns("Vendor Name").Width = 250
        gvDetails.Columns("Document No").Width = 150
        gvDetails.Columns("Document Date").Width = 150
        gvDetails.Columns("Amount").Width = 151

        gvDetails.SummaryRowsBottom.Clear()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SUMDrAmt As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDrAmt)

        gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
        If Not IsFormLoad Then
            LoadData()
        End If
    End Sub

    Private Sub RadChart1_Click(ByVal sender As System.Object, ByVal args As Telerik.Charting.ChartClickEventArgs) Handles RadChart1.Click
        Try
            If TypeOf args.Element.ActiveRegion.Parent Is ChartSeriesItem AndAlso (TryCast(args.Element.ActiveRegion.Parent, ChartSeriesItem)).Parent.Name = "Bucket" Then
                Dim Bucket As String = clsCommon.myCstr(dt.Rows((TryCast(args.Element, ChartSeriesItem)).Index)("Bucket"))
                LoadDetails(strToDate, Bucket)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged, cboOrientation.SelectedValueChanged
        Try
            If Not IsFormLoad Then
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '-------------------Multicurrency ---------------BM00000001804
    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then

            txtCurrencyCode.Enabled = True

            If cbgVendor.CheckedValue.Count > 0 Then
                strq = "select top 1 currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE in (" & clsCommon.GetMulcallString(cbgVendor.CheckedValue) & ")"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
            End If
        Else

            txtCurrencyCode.Enabled = False
        End If
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
    End Sub
    '-------------------------end here-------------------------------------
End Class