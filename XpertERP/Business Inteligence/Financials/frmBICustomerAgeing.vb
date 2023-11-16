'----Created By--[Pankaj Kumar Chaudhary] Against Ticket no--[BM00000001162]
Imports common
Imports System.Data.SqlClient
Imports Telerik.Charting

Public Class FrmBICustomerAgeing
    Inherits FrmMainTranScreen
    Dim IsFormLoad As Boolean = False
    Dim strToDate As String
    Dim dt As DataTable
    Dim qry As String

    Private Sub FrmBICustomerAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IsFormLoad = True
        SetUserMgmtNew()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"
        txtDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomers()
        LoadFigures()
        cbgCustomer.CheckedAll()
        chkAutoScroll.Checked = True
        IsFormLoad = False
       
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCustomerAgingSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadCustomers()
        Dim qry As String = " Select Cust_Code As Code, Customer_Name As Name from TSPL_CUSTOMER_MASTER "
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)

    End Sub

    Private Sub LoadFigures()
        cmbFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub

    Public Function GetQry(ByVal asOnDate As String, ByVal figure As String, ByVal isForAllCustomer As Boolean, ByVal arrCustomer As ArrayList) As String
        If cbgCustomer.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select atleast single customer.")
        End If

        qry = "Select YYY.Bucket, CONVERT(Decimal(18,2), SUM(ISNULL(Amount,0))/" + figure + ") As Amount from ("

        qry += " Select Cust_Code, DocNo, DocDate, Amount, Case When Ageing_Days<=30 Then '0 - 30' When (Ageing_Days>30 AND Ageing_Days<=60) Then '31 - 60' When (Ageing_Days>60 AND Ageing_Days<=90) Then '61 - 90' WHEN (Ageing_Days>90 AND Ageing_Days<=120) Then '91 - 120' WHEN (Ageing_Days>120 AND Ageing_Days<=150) Then '121 - 150' Else 'Over 150' End As Bucket  from (  "

        qry += " SELECT  TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_Customer_Invoice_Head.Document_No  as [DocNo], TSPL_Customer_Invoice_Head.Document_Date as DocDate, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Amount], DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as DocType FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'   "
        qry += " UNION ALL "
        qry += " SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id], Document_No as DocNo, TSPL_SALE_RETURN_INTER_HEAD.Document_Date  as DocDate, (Total_Order_Amt)*-1 as [Amount], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days, 'SR' as DocType from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1   "
        qry += " union All "
        qry += " select  TSPL_VCGL_Head.VC_Code as Cust_Code, TSPL_VCGL_Head.Document_No as DocNo, Document_Date  as DocDate, CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END as Amount, DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days,'VGCL' as DocType from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1   "
        qry += " UNION ALL "
        qry += " select TSPL_VCGL_Detail.VCGL_Code as Cust_Code, TSPL_VCGL_Head.Document_No as DocNo, Document_Date as DocDate, CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END As Amount, DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days,'VGCL' as DocType from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer'   "
        qry += " union All "
        qry += " select  TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, Receipt_Date as DocDate, Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End As Amount, DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + asOnDate + "',103)) As Ageing_Days, case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end As DocType from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0   "
        qry += " UNION ALL "
        qry += " Select  Customer_No As Cust_Code, Adjustment_No as DocNo, Adjustment_Date as DocDate, Adjustment_Amount*-1 as Amount, DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days, 'RC' as DocType from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y'   "
        qry += " Union All  "
        qry += " SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, Document_No as DocNo, Document_Date as DocDate, Empty_Value*-1 AS Amount, DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days, 'SR' as DocType from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1   "
        qry += " UNION ALL "
        qry += " SELECT TSPL_ADJUSTMENT_HEADER.Customer_CODE As Cust_Code, TSPL_ADJUSTMENT_HEADER.Adjustment_No, Adjustment_Date as DocDate, case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end As Amount, DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + asOnDate + "',103)) AS Ageing_Days, 'AD' As DocType FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y'   "

        qry += " ) XXX  where  XXX.DocType in ('IN','DB','CR','RC','AV','OA','UC','SR','VGCL','AD','RF','RC'  )  and convert(date,XXX.DocDate ,103) <= convert(date,'" + asOnDate + "',103) AND Amount <> 0  "
        If Not isForAllCustomer And cbgCustomer.CheckedValue.Count > 0 Then
            qry += " AND XXX.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If

        qry += " ) Query Right JOIN (Select '0 - 30' as Bucket, 1 as odr UNION Select '31 - 60' as Bucket, 2 as odr UNION Select '61 - 90' as Bucket, 3 as odr UNION Select '91 - 120' as Bucket, 4 as odr UNION Select '121 - 150' as Bucket, 5 as odr UNION Select 'Over 150' as Bucket, 6 as odr) YYY ON YYY.Bucket=Query.Bucket Group By YYY.Bucket Order by MAX(odr)"
        Return qry
    End Function

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            strToDate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
            dt = clsDBFuncationality.GetDataTable(GetQry(strToDate, cmbFigure.SelectedValue, False, cbgCustomer.CheckedValue))
            SnDUtility.GenerateExcelChart(dt, cboType.SelectedValue, "", "Bucket", "Amount")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Try
            strToDate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
            qry = GetQry(strToDate, cmbFigure.SelectedValue, False, cbgCustomer.CheckedValue)
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
            Dim Qry As String
            Qry = "Select * from ( "

            Qry += " Select XXX.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As [Customer Name], DocNo as [Document No], DocDate As [Document Date], Amount, Case When Ageing_Days<=30 Then '0 - 30' When (Ageing_Days>30 AND Ageing_Days<=60) Then '31 - 60' When (Ageing_Days>60 AND Ageing_Days<=90) Then '61 - 90' WHEN (Ageing_Days>90 AND Ageing_Days<=120) Then '91 - 120' WHEN (Ageing_Days>120 AND Ageing_Days<=150) Then '121 - 150' When (Ageing_Days>150 AND Ageing_Days<=180) Then '151 - 180' When (Ageing_Days>180 AND Ageing_Days<=210) Then '181 - 210' When (Ageing_Days>210 AND Ageing_Days<=240) Then '211 - 240' When (Ageing_Days>240 AND Ageing_Days<=270) Then '241 - 270' When (Ageing_Days>270 AND Ageing_Days<=300) Then '271 - 300' Else 'Over 300' End As Bucket  from (   "

            Qry += " SELECT  TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_Customer_Invoice_Head.Document_No  as [DocNo], TSPL_Customer_Invoice_Head.Document_Date as DocDate, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Amount], DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as DocType FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_Customer_Invoice_Head.Status='1'    "
            Qry += " UNION ALL  "
            Qry += " SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id], Document_No as DocNo, TSPL_SALE_RETURN_INTER_HEAD.Document_Date  as DocDate, (Total_Order_Amt)*-1 as [Amount], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, 'SR' as DocType from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1    "
            Qry += " union All  "
            Qry += " select  TSPL_VCGL_Head.VC_Code as Cust_Code, TSPL_VCGL_Head.Document_No as DocNo, Document_Date  as DocDate, CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END as Amount, DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days,'VGCL' as DocType from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1    "
            Qry += " UNION ALL  "
            Qry += " select TSPL_VCGL_Detail.VCGL_Code as Cust_Code, TSPL_VCGL_Head.Document_No as DocNo, Document_Date as DocDate, CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END As Amount, DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days,'VGCL' as DocType from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer'   "
            Qry += " union All "
            Qry += " select  TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, Receipt_Date as DocDate, Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End As Amount, DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" + strToDate + "',103)) As Ageing_Days, case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end As DocType from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0    "
            Qry += " UNION ALL  "
            Qry += " Select  Customer_No As Cust_Code, Adjustment_No as DocNo, Adjustment_Date as DocDate, Adjustment_Amount*-1 as Amount, DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, 'RC' as DocType from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y'    "
            Qry += " Union All   "
            Qry += " SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, Document_No as DocNo, Document_Date as DocDate, Empty_Value*-1 AS Amount, DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, 'SR' as DocType from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1    "
            Qry += " UNION ALL  "
            Qry += " SELECT TSPL_ADJUSTMENT_HEADER.Customer_CODE As Cust_Code, TSPL_ADJUSTMENT_HEADER.Adjustment_No, Adjustment_Date as DocDate, case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end As Amount, DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" + strToDate + "',103)) AS Ageing_Days, 'AD' As DocType FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y'    "

            Qry += " ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code where  XXX.DocType in ('IN','DB','CR','RC','AV','OA','UC','SR','VGCL','AD','RF','RC'  )  and convert(date,XXX.DocDate ,103) <= convert(date,'" + strToDate + "',103) AND Amount <> 0   AND XXX.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"

            Qry += " ) Query WHERE Bucket='" + bucket + "' Order by [Customer Code]"

            gvDetails.DataSource = Nothing
            gvDetails.DataSource = clsDBFuncationality.GetDataTable(Qry)
            FormatGrid()
            RadPageView2.SelectedPage = RadPageViewPage5
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvDetails.Columns("Customer Code").Width = 100
        gvDetails.Columns("Customer Name").Width = 250
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


    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
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

    
End Class