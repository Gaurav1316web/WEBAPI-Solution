'---------------SALEINVOICESTATUSREPORT----------------------------------
Imports common
Imports System.IO


Public Class rptSaleInvoiceStatusReport
    Inherits FrmMainTranScreen

    Dim EnableProductSaleForJPR As Boolean = False

    Private Sub rptSaleInvoiceStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtItem.Visible = False
        MyLabel4.Visible = False
        EnableProductSaleForJPR = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableProductSaleForJPR, clsFixedParameterCode.EnableProductSaleForJPR, Nothing)) = 1, True, False)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub reset()
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableCntrl(True)
    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
        txtfDate.Enabled = val
        txtToDate.Enabled = val
        txtMultiCustomer.Enabled = val
        TxtCustomerType.Enabled = val
        txtItem.Enabled = val
        TxtSubLocation.Enabled = val
        TxtTransaction.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox1.Enabled = val
        chkItemWiseCustomer.Enabled = val
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvData.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvData.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code as [Code], Item_Desc as [Name] from TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master  "
        If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
            qry += " Where Cust_Type_Code In (" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")"
        End If
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub

    Private Sub TxtCustomerType__My_Click(sender As Object, e As EventArgs) Handles TxtCustomerType._My_Click
        Dim qry As String = " SELECT  [Cust_Type_Code] as Code,[Cust_Type_Desc] as Name FROM [TSPL_CUSTOMER_TYPE_MASTER] Where 2=2  "
        TxtCustomerType.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)

        txtMultiCustomer.arrValueMember = Nothing
    End Sub

    Private Sub TxtTransaction__My_Click(sender As Object, e As EventArgs) Handles TxtTransaction._My_Click
        'Dim qry As String = " select Cust_Group_Code as [Code], Customer_Name as [Name],Cust_Type,Cust_Group_Code,Cust_Category_Code,Cust_Type_Code from tspl_customer_master "
        Dim qry As String = ""
        If rdbSaleReturn.IsChecked Then
            qry = " Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.saleReturn + "'
union all
Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmMCCMaterialSaleReturn + "'
union all
Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.ScrapSaleRetrun + "' "
        Else
            qry = " Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmDairyBookingCustomer + "'
                              union all
                              Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmSaleDispatchDairy + "'
                              union all
                              Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.FrmSalesOrderDispatch + "'
                              union all
                              Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmMCCMaterial + "'"
            If EnableProductSaleForJPR Then
                qry += "          Union all 
                              Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.FrmProductDispatch + "'"
            End If
        End If
        'qry = " Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmDairyBookingCustomer + "'
        '                      union all
        '                      Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmSaleDispatchDairy + "'
        '                      union all
        '                      Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.FrmSalesOrderDispatch + "'
        '                      union all
        '                      Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.frmMCCMaterial + "'"
        'If EnableProductSaleForJPR Then
        '    qry += "          Union all 
        '                      Select coalesce(Re_Name,Program_Name)Code,Program_Code as Name from TSPL_PROGRAM_MASTER where Program_Code='" + clsUserMgtCode.FrmProductDispatch + "'"
        'End If
        TxtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", TxtTransaction.arrValueMember, TxtTransaction.arrDispalyMember)

    End Sub

    Private Sub TxtSubLocation__My_Click(sender As Object, e As EventArgs) Handles TxtSubLocation._My_Click
        Dim qry As String = " Select Location_Code AS Code,Location_Desc as Name,Is_Sub_Location from TSPL_LOCATION_MASTER where Is_Sub_Location='Y' "
        TxtSubLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", TxtSubLocation.arrValueMember, TxtSubLocation.arrDispalyMember)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If chkItemWiseCustomer.Checked = True Then
            LoadDataItemWiseCustomer()
        Else
            LoadData()
        End If
    End Sub

    Sub LoadDataItemWiseCustomer()
        Try
            Dim itemsWithZeroUOM As New List(Of String)

            Dim qry1 As String = Nothing



            Dim qry2 As String = "(
    SELECT DISTINCT 
           TSPL_ITEM_MASTER.Item_Code,
          ISNULL(TSPL_ITEM_UOM_DETAIL.Report_UOM,0)Report_UOM
    FROM TSPL_SD_SALE_INVOICE_HEAD
    JOIN TSPL_SD_SALE_INVOICE_DETAIL
         ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
    JOIN TSPL_ITEM_MASTER 
         ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
    JOIN TSPL_ITEM_UOM_DETAIL 
         ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code
    JOIN (
        SELECT Item_Code, UOM_Code, Conversion_Factor 
        FROM TSPL_ITEM_UOM_DETAIL
    ) AS ItemCF 
         ON ItemCF.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
        AND ItemCF.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Billing_Unit_code
    WHERE TSPL_ITEM_MASTER.Item_Type = 'F'  AND TSPL_ITEM_UOM_DETAIL.Report_UOM = 0
      and ItemCF.UOM_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code
	  and CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "',103)
  AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)

  AND NOT EXISTS (
        SELECT 1
        FROM TSPL_ITEM_UOM_DETAIL U
        WHERE U.Item_Code = TSPL_ITEM_MASTER.Item_Code
          AND U.Report_UOM = 1
  )
);"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)

            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then

                Dim msg As New System.Text.StringBuilder()

                For Each dr As DataRow In dt2.Rows
                    msg.AppendLine(
            "Report UOM is 0 for item " &
            dr("Item_Code").ToString() &
            " please set it first"
        )
                Next

                common.clsCommon.MyMessageBoxShow(Me, msg.ToString())

                Exit Sub   ' stop further processing
            End If

            Dim whrcls As String = Nothing
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_CUSTOMER_MASTER.Cust_Code in ('" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + "')"
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_SD_SALE_INVOICE_DETAIL.Item_code in ('" + clsCommon.GetMulcallString(txtItem.arrValueMember) + "')"
            End If



            Dim qry As String = "DECLARE @cols nvarchar(max) = '',
 @totalAmt NVARCHAR(MAX) = '',
        @sql  nvarchar(max);


SELECT @cols = @cols + '
    SUM(CASE WHEN TSPL_ITEM_MASTER.Item_Desc = ''' + Item_Desc + ''' 
             THEN CAST((TSPL_SD_SALE_INVOICE_DETAIL.Qty * IMCF) / TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS DECIMAL(18,2))
             ELSE 0 END) AS [' + Item_Desc + '(' + UOM_Code + ')],
    SUM(CASE WHEN TSPL_ITEM_MASTER.Item_Desc = ''' + Item_Desc + ''' 
             THEN TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt 
             ELSE 0 END) AS [' + Item_Desc + ' (Amt)],',


    @totalAmt = @totalAmt + '
    + SUM(CASE WHEN TSPL_ITEM_MASTER.Item_Desc = ''' + Item_Desc + ''' 
               THEN CAST(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt AS DECIMAL(18,2))
               ELSE 0 END)'
FROM (
    SELECT DISTINCT 
           TSPL_ITEM_MASTER.Item_Desc,
           TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
           ItemCF.Conversion_Factor AS IMCF,
           TSPL_ITEM_UOM_DETAIL.UOM_Code
    FROM TSPL_SD_SALE_INVOICE_HEAD
    JOIN TSPL_SD_SALE_INVOICE_DETAIL
         ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
    JOIN TSPL_ITEM_MASTER 
         ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
    JOIN TSPL_ITEM_UOM_DETAIL 
         ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code
    JOIN (
        SELECT Item_Code, UOM_Code, Conversion_Factor 
        FROM TSPL_ITEM_UOM_DETAIL
    ) AS ItemCF 
         ON ItemCF.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
        AND ItemCF.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Billing_Unit_code
    WHERE TSPL_ITEM_MASTER.Item_Type = 'F' 
      AND TSPL_ITEM_UOM_DETAIL.Report_UOM = 1  and ItemCF.UOM_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code
) AS X;

SET @cols = LEFT(@cols, LEN(@cols) - 1);

SET @sql = N'
SELECT 
    RIGHT(TSPL_SD_SALE_INVOICE_HEAD.Document_Code, 6) AS BillNo,
    MAX(CONVERT(varchar(20), TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)) AS BillDate,
    MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS PARTY_Name,
    ' + @cols + ',
	    CAST((' + @totalAmt + ') AS DECIMAL(18,2)) AS [Total Amount]
FROM TSPL_SD_SALE_INVOICE_HEAD
LEFT JOIN TSPL_SD_SALE_INVOICE_DETAIL
       ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
LEFT JOIN TSPL_ITEM_MASTER 
       ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
LEFT JOIN TSPL_ITEM_UOM_DETAIL
       ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code
LEFT JOIN TSPL_CUSTOMER_MASTER
       ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
	  left JOIN (
        SELECT Item_Code, UOM_Code, Conversion_Factor as IMCF
        FROM TSPL_ITEM_UOM_DETAIL
    ) AS ItemCF 
         ON ItemCF.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
        AND ItemCF.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Billing_Unit_code
WHERE CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,''" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "'',103)
  AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,''" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'',103)
  
  " + whrcls + "
GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date
ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Date;';

EXEC sp_executesql @sql; "

            'qry += " group by Document_Code order by Document_Code "
            ' qry += " group by Document_Code ,Item_Code order by Document_Code "
            Dim dt As DataTable = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            gvData.DataSource = Nothing
            gvData.Rows.Clear()
            gvData.Columns.Clear()
            gvData.GroupDescriptors.Clear()
            gvData.MasterView.Refresh()
            Dim viewBlank As New TableViewDefinition()
            gvData.ViewDefinition = viewBlank
            If dt.Rows.Count > 0 Then
                gvData.DataSource = dt
                gvData.GroupDescriptors.Clear()
                gvData.EnableFiltering = True
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormation()
                EnableDisableCntrl(False)
                gvData.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        ReStoreGridLayout()
    End Sub

    Sub SetGridFormation()
        gvData.AutoExpandGroups = True
        gvData.ShowGroupPanel = True
        gvData.ShowRowHeaderColumn = False
        gvData.AllowAddNewRow = False
        gvData.AllowDeleteRow = False
        gvData.EnableFiltering = True
        gvData.ShowFilteringRow = True
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
            gvData.Columns(ii).BestFit()
        Next


        gvData.Columns("PARTY_Name").HeaderText = "PARTY Name"
        gvData.Columns("BillNo").HeaderText = "Bill No"
        gvData.Columns("BillDate").HeaderText = "Bill Date"

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gvData.ShowGroupPanel = True
        gvData.MasterTemplate.AutoExpandGroups = True
        'For ii As Integer = gv1.Columns("Qc").Index + 1 To gv1.Columns("Total Deduction").Index - 1
        '    gv1.Columns(ii).HeaderText = (gv1.Columns(ii).HeaderText).Replace("Ded", "")
        'Next
        'gv1.Columns("Qc").IsVisible = False
    End Sub
    Sub LoadData()

        Try
            Dim WhrCust As String = ""
            Dim Sublocn As String = ""
            Dim item As String = ""

            Dim dt As DataTable = Nothing
            Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = ""



            Dim Baseqry As String = ""

            Baseqry = " select  
        CASE WHEN EXISTS ( SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD 
        LEFT JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD.Against_Booking_No IS NOT NULL)  And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <> 'MCC'  THEN 'CUSTOMER BOOKING'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD 
        LEFT JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD.Against_Booking_No IS NULL AND ISNULL(TSPL_BOOKING_MATSER.Is_APS,0) = 0
          AND TSPL_SD_SHIPMENT_HEAD.Item_Type = 'S') And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <> 'MCC' THEN 'DISPATCH'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD 
        LEFT JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD.Against_Booking_No IS NULL AND ISNULL(TSPL_BOOKING_MATSER.Is_APS,0) = 0
          AND TSPL_SD_SHIPMENT_HEAD.Item_Type In ('P','I')) And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <> 'MCC' THEN 'PRODUCT DISPATCH'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD 
		  LEFT JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_SD_SHIPMENT_HEAD.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD.Against_Booking_No IS NULL AND (TSPL_BOOKING_MATSER.Is_APS=1 OR TSPL_SD_SHIPMENT_HEAD.Screen_Type= ('CT'))) And TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <> 'MCC' THEN 'APS SALES'
		  ELSE 'DCS SALE' END AS Transcation_Type,
case when TSPL_SD_SALE_INVOICE_HEAD.Status=1 then 'Approved' else'Pending' end as Doc_Status,
TSPL_CUSTOMER_MASTER.Cust_Type_Code as[Customer Type],
TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS [Location],
TSPL_SD_SALE_INVOICE_HEAD.Sub_Location_code AS [Sub Location],
Convert(varchar(20),TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Invoice_Date,
TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No,
TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No],
TSPL_CUSTOMER_MASTER.Cust_Code as[Party Code],
TSPL_CUSTOMER_MASTER.Customer_Name AS [Party Name],
 TSPL_LOCATION_MASTER.GSTNO AS [GST No],
 TSPL_CUSTOMER_MASTER.GSTNO as [Customer GSTNo],
  TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
   TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Item Code],
tspl_item_master.Item_Desc as [Item Name],
TSPL_SD_SALE_INVOICE_DETAIL.Billing_Unit_code as [Measure of Qty],
  TSPL_SD_SALE_INVOICE_DETAIL.Billing_Qty as [Product Qty],
  tspl_item_master.HSN_Code,
  case when TSPL_SD_SALE_INVOICE_DETAIL.Tax1='KKF' or TSPL_SD_SALE_INVOICE_DETAIL.Tax2='KKF' then (case when TSPL_SD_SALE_INVOICE_DETAIL.tax3='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate else  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate + TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate end ) else (case when  TSPL_SD_SALE_INVOICE_DETAIL.tax1='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate else TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate +TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate end)end as [IGST Rate], 
  --case when TSPL_SD_SALE_INVOICE_DETAIL.Tax1='KKF' or TSPL_SD_SALE_INVOICE_DETAIL.Tax2='KKF' then (case when TSPL_SD_SALE_INVOICE_DETAIL.tax3='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Base_Amt else  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Base_Amt end ) else (case when  TSPL_SD_SALE_INVOICE_DETAIL.tax1='IGST' then TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt else TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt end)end as [Basic Amt]
  TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Basic Amt]
,Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='KKF' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt else 0  END) AS [KKF],
					CASE When TSPL_SD_SALE_INVOICE_DETAIL.TAX1='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='MNDTAX' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt else 0 END AS [Mandi Tax Amt],
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='TCS' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt  else 0 END AS [Party TCS Amt],
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='CGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt else 0 END AS [CGST Amt],
					Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='SGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt else 0 END) AS [SGST Amt],
					
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10='IGST' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt  else 0 END AS [IGST Amt],
					TSPL_SD_SALE_INVOICE_Detail.Item_Net_Amt as [Total Amt],
					 case when TSPL_SD_SALE_INVOICE_HEAD.EInvoice_Type='BB' then 'B2B' else 'B2C' end as [B2B/B2C],
TSPL_SD_SALE_INVOICE_HEAD.Ack_No,TSPL_SD_SALE_INVOICE_HEAD.Ack_Date,TSPL_SD_SALE_INVOICE_HEAD.IRN_No,
Report_UOM.UOM_Code as Report_UOM,
(Billing_Qty * tspl_item_uom_detail.Conversion_Factor/Report_UOM.Conversion_Factor ) as ReportUOM_Qty
,TSPL_SD_SHIPMENT_HEAD.TotalSubsidyAmt as [Subsidy Amt],Case when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=1 then 'Taxable' else 'Non-Taxable' end as [Invoice Type]
                         from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left  join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join tspl_item_uom_detail  on tspl_item_uom_detail.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and tspl_item_uom_detail.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
left join tspl_item_uom_detail Report_UOM  on Report_UOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and Report_UOM.Report_UOM=1
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
 LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
 left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=Convert( Date,'" + strtxtfDate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=Convert( Date,'" + strToDate + "',103)"
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                Baseqry += " and tspl_customer_master.Cust_Group_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                Baseqry += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtSubLocation.arrValueMember IsNot Nothing AndAlso TxtSubLocation.arrValueMember.Count > 0 Then
                Baseqry += " and TSPL_SD_SALE_INVOICE_HEAD.Sub_Location_code in(" + clsCommon.GetMulcallString(TxtSubLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                Baseqry += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")" + Environment.NewLine
            End If

            Baseqry += "            union all

select  'MATERIAL SALE' as Transcation_Type,case when TSPL_SCRAPINVOICE_HEAD.ispost=1 then 'Approved' else'Pending' end as Doc_Status,
TSPL_CUSTOMER_MASTER.Cust_Type_Code as[Customer Type],
TSPL_SCRAPINVOICE_HEAD.Loc_Code AS [Location],
TSPL_SCRAPINVOICE_HEAD.Sub_Location_code AS [Sub Location],
Convert(varchar(20),TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Invoice_Date,
TSPL_SCRAPINVOICE_HEAD.invoice_No as Invoice_No,
TSPL_ROUTE_MASTER.Route_No as [Route No],
TSPL_CUSTOMER_MASTER.Cust_Code as[Party Code],
TSPL_CUSTOMER_MASTER.Customer_Name AS [Party Name],
 TSPL_LOCATION_MASTER.GSTNO AS [GST No],
 TSPL_CUSTOMER_MASTER.GSTNO as [Customer GSTNo],
  TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
   TSPL_SCRAPINVOICE_Detail.Item_Code as [Item Code],
tspl_item_master.Item_Desc as [Item Name],
TSPL_SCRAPINVOICE_Detail.Unit_code as [Measure of Qty],
  TSPL_SCRAPINVOICE_Detail.shipped_Qty as [Product Qty],
  tspl_item_master.HSN_Code,
  case when TSPL_SCRAPINVOICE_Detail.Tax1='KKF' or TSPL_SCRAPINVOICE_Detail.Tax2='KKF' then (case when TSPL_SCRAPINVOICE_Detail.tax3='IGST' then TSPL_SCRAPINVOICE_Detail.TAX3_Rate else  TSPL_SCRAPINVOICE_Detail.TAX3_Rate + TSPL_SCRAPINVOICE_Detail.TAX4_Rate end ) else (case when  TSPL_SCRAPINVOICE_Detail.tax1='IGST' then TSPL_SCRAPINVOICE_Detail.TAX1_Rate else TSPL_SCRAPINVOICE_Detail.TAX1_Rate +TSPL_SCRAPINVOICE_Detail.TAX2_Rate end)end as [IGST Rate], 
  --case when TSPL_SCRAPINVOICE_Detail.Tax1='KKF' or TSPL_SCRAPINVOICE_Detail.Tax2='KKF' then (case when TSPL_SCRAPINVOICE_Detail.tax3='IGST' then TSPL_SCRAPINVOICE_Detail.TAX3_Base_Amt else  TSPL_SCRAPINVOICE_Detail.TAX3_Base_Amt end ) else (case when  TSPL_SCRAPINVOICE_Detail.tax1='IGST' then TSPL_SCRAPINVOICE_Detail.TAX1_Base_Amt else TSPL_SCRAPINVOICE_Detail.TAX1_Base_Amt end)end as [Basic Amt]
   TSPL_SCRAPINVOICE_Detail.ItemNetAmt as [Basic Amt]
,Convert(decimal(18,2),CASE WHEN TSPL_SCRAPINVOICE_Detail.TAX1='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='KKF'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt 
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='KKF' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt else 0  END) AS [KKF],
					CASE When TSPL_SCRAPINVOICE_Detail.TAX1='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='MNDTAX'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='MNDTAX' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt else 0 END AS [Mandi Tax Amt],
					CASE WHEN TSPL_SCRAPINVOICE_Detail.TAX1='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='TCS'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='TCS' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt  else 0 END AS [Party TCS Amt],
					CASE WHEN TSPL_SCRAPINVOICE_Detail.TAX1='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='CGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='CGST' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt else 0 END AS [CGST Amt],
					Convert(decimal(18,2),CASE WHEN TSPL_SCRAPINVOICE_Detail.TAX1='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Rate
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='SGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='SGST' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt else 0 END) AS [SGST Amt],
					
					CASE WHEN TSPL_SCRAPINVOICE_Detail.TAX1='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX2='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX3='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX4='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX5='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX6='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX7='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX8='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX9='IGST'  THEN TSPL_SCRAPINVOICE_Detail.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_Detail.TAX10='IGST' THEN TSPL_SCRAPINVOICE_Detail.TAX10_Amt  else 0 END AS [IGST Amt],
					TSPL_SCRAPINVOICE_Detail.ItemNetAmt as [Total Amt],
					 case when TSPL_SCRAPINVOICE_HEAD.EInvoice_Type='BB' then 'B2B' else 'B2C' end as [B2B/B2C],
TSPL_SCRAPINVOICE_HEAD.Ack_No,TSPL_SCRAPINVOICE_HEAD.Ack_Date,TSPL_SCRAPINVOICE_HEAD.IRN_No
,Report_UOM.UOM_Code as Report_UOM,
(shipped_Qty * tspl_item_uom_detail.Conversion_Factor/Report_UOM.Conversion_Factor ) as ReportUOM_Qty,0 as [Subsidy Amt], TSPL_SCRAPINVOICE_HEAD.Invoice_Type as [Invoice Type]
                           from TSPL_SCRAPINVOICE_HEAD
                    left join TSPL_SCRAPINVOICE_Detail on TSPL_SCRAPINVOICE_Detail.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No
                    LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_Detail.Item_Code
                    left join tspl_item_uom_detail  on tspl_item_uom_detail.item_code=TSPL_SCRAPINVOICE_Detail.item_code and tspl_item_uom_detail.UOM_Code=TSPL_SCRAPINVOICE_Detail.Unit_code
					left join tspl_item_uom_detail Report_UOM  on Report_UOM.item_code=TSPL_SCRAPINVOICE_Detail.item_code and Report_UOM.Report_UOM=1
                    LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SCRAPINVOICE_HEAD.Loc_Code
                     LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.cust_Code
                     LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
                     left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                    where convert(date,shipment_Date,103)>=Convert( Date,'" + strtxtfDate + "',103) and convert(date,shipment_Date,103)<=Convert( Date,'" + strToDate + "',103)  "
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                Baseqry += " and tspl_customer_master.Cust_Group_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                Baseqry += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtSubLocation.arrValueMember IsNot Nothing AndAlso TxtSubLocation.arrValueMember.Count > 0 Then
                Baseqry += " and TSPL_SCRAPINVOICE_HEAD.Sub_Location_code in(" + clsCommon.GetMulcallString(TxtSubLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                Baseqry += " and TSPL_SCRAPINVOICE_Detail.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")" + Environment.NewLine
            End If

            If rbtnDetail.IsChecked AndAlso rdbGstInvoice.IsChecked Then
                qry = "Select (Transcation_Type)[Transcation Type],([Customer Type])[Customer Type],(Doc_Status)[Doc Status],(Location)Location,([Sub Location])[Sub Location],(Invoice_Date)Invoice_Date,Invoice_No,([Route No])[Route No],([Party Code])[Party Code],([Party Name])[Party Name],([GST No])[GST No],
                    ([Customer GSTNo])[Customer GSTNo],([State Code])[State Code],([Measure of Qty])[Measure of Qty],([Product Qty])[Product Qty],Cast(([IGST Rate]) as decimal(10,2))[GST Rate],Cast(([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast((KKF) as decimal(10,2))KKF,Cast(([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(([IGST Amt]) as decimal(10,2))[GST Amt],cast(([Total Amt]) as decimal(10,2))[Total Amt],([B2B/B2C])[B2B/B2C],(Ack_No)[Ack No],(Ack_Date)[Ack Date],(IRN_No)[IRN No],(Report_UOM)[Report UOM],(ReportUOM_Qty)[ReportUOM Qty],([Subsidy Amt])[Subsidy Amt] from (" + Baseqry + ")XX   "
                If TxtTransaction.arrValueMember IsNot Nothing AndAlso TxtTransaction.arrValueMember.Count > 0 Then
                    qry += " where XX.Transcation_Type In(" + clsCommon.GetMulcallString(TxtTransaction.arrValueMember) + ")" + Environment.NewLine
                End If
            ElseIf rbtnSummary.IsChecked AndAlso rdbGstInvoice.IsChecked Then
                qry = " Select max(Transcation_Type)[Transcation Type],max([Customer Type])[Customer Type],max(Doc_Status)[Doc Status],max(Location)Location,max([Sub Location])[Sub Location],max(Invoice_Date)Invoice_Date,Invoice_No,MAX([Route No])[Route No],max([Party Code])[Party Code],max([Party Name])[Party Name],max([GST No])[GST No],
                        max([Customer GSTNo])[Customer GSTNo],max([State Code])[State Code],max([Measure of Qty])[Measure of Qty],max([Product Qty])[Product Qty],Cast(Max([IGST Rate]) as decimal(10,2))[GST Rate],Cast(sum([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast(max(KKF) as decimal(10,2))KKF,Cast(sum([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(sum([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(sum([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(sum([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(sum([IGST Amt]) as decimal(10,2))[GST Amt],cast(sum([Total Amt]) as decimal(10,2))[Total Amt],max([B2B/B2C])[B2B/B2C],max(Ack_No)[Ack No],max(Ack_Date)[Ack Date],max(IRN_No)[IRN No],max(Report_UOM)[Report UOM],Sum(ReportUOM_Qty)[ReportUOM Qty],sum([Subsidy Amt])[Subsidy Amt]
                        from ( " + Baseqry + " )XX  "
                If TxtTransaction.arrValueMember IsNot Nothing AndAlso TxtTransaction.arrValueMember.Count > 0 Then
                    qry += " where XX.Transcation_Type In(" + clsCommon.GetMulcallString(TxtTransaction.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " Group by xx.Invoice_No "
            End If

            Dim BaseQryCancel As String = ""

            BaseQryCancel = " 
select  CASE WHEN EXISTS ( SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD_Cancel_Data 
        LEFT JOIN TSPL_BOOKING_MATSER_Cancel_Data ON TSPL_BOOKING_MATSER_Cancel_Data.Document_No = TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No IS NOT NULL) 
        And TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Trans_Type <> 'MCC' THEN 'CUSTOMER BOOKING'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD_Cancel_Data 
        LEFT JOIN TSPL_BOOKING_MATSER_Cancel_Data ON TSPL_BOOKING_MATSER_Cancel_Data.Document_No = TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No IS NULL AND ISNULL(TSPL_BOOKING_MATSER_Cancel_Data.Is_APS,0) = 0
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Item_Type = 'S') And TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Trans_Type <> 'MCC' THEN 'DISPATCH'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD_Cancel_Data 
        LEFT JOIN TSPL_BOOKING_MATSER_Cancel_Data ON TSPL_BOOKING_MATSER_Cancel_Data.Document_No = TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No IS NULL AND ISNULL(TSPL_BOOKING_MATSER_Cancel_Data.Is_APS,0) = 0
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Item_Type In ('P','I')) And TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Trans_Type <> 'MCC' THEN 'PRODUCT DISPATCH'
		  WHEN EXISTS (SELECT 1 FROM TSPL_SD_SHIPMENT_HEAD_Cancel_Data 
		  LEFT JOIN TSPL_BOOKING_MATSER_Cancel_Data ON TSPL_BOOKING_MATSER_Cancel_Data.Document_No = TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No
        WHERE TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Against_Shipment_No
          AND TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_Booking_No IS NULL AND (TSPL_BOOKING_MATSER_Cancel_Data.Is_APS=1 OR TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Screen_Type= ('CT')))
         And TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Trans_Type <> 'MCC' THEN 'APS SALES'
		  ELSE 'DCS SALE' END AS Transcation_Type,
'Cancel' as Doc_Status,
TSPL_CUSTOMER_MASTER.Cust_Type_Code as[Customer Type],
TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location AS [Location],
TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Sub_Location_code AS [Sub Location],
Convert(varchar(20),TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103) as Invoice_Date,
TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Code as Invoice_No,
TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Route_No as [Route No],
TSPL_CUSTOMER_MASTER.Cust_Code as[Party Code],
TSPL_CUSTOMER_MASTER.Customer_Name AS [Party Name],
 TSPL_LOCATION_MASTER.GSTNO AS [GST No],
 TSPL_CUSTOMER_MASTER.GSTNO as [Customer GSTNo],
  TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
   TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code as [Item Code],
tspl_item_master.Item_Desc as [Item Name],
TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Billing_Unit_code as [Measure of Qty],
  TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Billing_Qty as [Product Qty],
  tspl_item_master.HSN_Code,
  case when TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Tax1='KKF' or TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Tax2='KKF' then (case when TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.tax3='IGST' then TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Rate else  TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Rate + TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Rate end ) else (case when  TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.tax1='IGST' then TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Rate else TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Rate +TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Rate end)end as [IGST Rate], 
  --case when TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Tax1='KKF' or TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Tax2='KKF' then (case when TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.tax3='IGST' then TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Base_Amt else  TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Base_Amt end ) else (case when  TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.tax1='IGST' then TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Base_Amt else TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Base_Amt end)end as [Basic Amt]
   TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Cost*TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Qty as [Basic Amt]
,Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='KKF'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt 
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='KKF' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0  END) AS [KKF],
					CASE When TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='MNDTAX'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='MNDTAX' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END AS [Mandi Tax Amt],
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='TCS'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='TCS' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt  else 0 END AS [Party TCS Amt],
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='CGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='CGST' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END AS [CGST Amt],
					Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Rate
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='SGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='SGST' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END) AS [SGST Amt],
					
					CASE WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9='IGST'  THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10='IGST' THEN TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.TAX10_Amt  else 0 END AS [IGST Amt],
					TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Net_Amt as [Total Amt],
					 case when TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.EInvoice_Type='BB' then 'B2B' else 'B2C' end as [B2B/B2C],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Ack_No,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Ack_Date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.IRN_No,
Report_UOM.UOM_Code as Report_UOM,
(Billing_Qty * tspl_item_uom_detail.Conversion_Factor/Report_UOM.Conversion_Factor ) as ReportUOM_Qty
,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.TotalSubsidyAmt as [Subsidy Amt],Case when TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Is_Taxable=1 then 'Taxable' else 'Non-Taxable' end as [Invoice Type]
                              from TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data
left join TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data on TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Code
left  join TSPL_SD_SHIPMENT_HEAD_Cancel_Data on TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Against_Shipment_No
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code
left join tspl_item_uom_detail  on tspl_item_uom_detail.item_code=TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.item_code and tspl_item_uom_detail.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Unit_code
left join tspl_item_uom_detail Report_UOM  on Report_UOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.item_code and Report_UOM.Report_UOM=1
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code
 LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
 left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
where convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103)>=Convert( Date,'" + strtxtfDate + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103)<=Convert( Date,'" + strToDate + "',103)"
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                BaseQryCancel += " and tspl_customer_master.Cust_Group_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                BaseQryCancel += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtSubLocation.arrValueMember IsNot Nothing AndAlso TxtSubLocation.arrValueMember.Count > 0 Then
                BaseQryCancel += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Sub_Location_code in(" + clsCommon.GetMulcallString(TxtSubLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                BaseQryCancel += " and TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")" + Environment.NewLine
            End If

            BaseQryCancel += " union all

select  'MATERIAL SALE' as Transcation_Type,'Cancel' as Doc_Status,
TSPL_CUSTOMER_MASTER.Cust_Type_Code as[Customer Type],
TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Loc_Code AS [Location],
TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Sub_Location_code AS [Sub Location],
Convert(varchar(20),TSPL_SCRAPINVOICE_HEAD_Cancel_Data.shipment_Date,103) as Invoice_Date,
TSPL_SCRAPINVOICE_HEAD_Cancel_Data.invoice_No as Invoice_No,
 TSPL_ROUTE_MASTER.Route_No as [Route No],
TSPL_CUSTOMER_MASTER.Cust_Code as[Party Code],
TSPL_CUSTOMER_MASTER.Customer_Name AS [Party Name],
 TSPL_LOCATION_MASTER.GSTNO AS [GST No],
 TSPL_CUSTOMER_MASTER.GSTNO as [Customer GSTNo],
  TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
   TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Item_Code as [Item Code],
tspl_item_master.Item_Desc as [Item Name],
TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Unit_code as [Measure of Qty],
  TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.shipped_Qty as [Product Qty],
  tspl_item_master.HSN_Code,
  case when TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Tax1='KKF' or TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Tax2='KKF' then (case when TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.tax3='IGST' then TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Rate else  TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Rate + TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Rate end ) else (case when  TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.tax1='IGST' then TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Rate else TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Rate +TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Rate end)end as [IGST Rate], 
  --case when TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Tax1='KKF' or TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Tax2='KKF' then (case when TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.tax3='IGST' then TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Base_Amt else  TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Base_Amt end ) else (case when  TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.tax1='IGST' then TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Base_Amt else TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Base_Amt end)end as [Basic Amt]
   TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.ItemNetAmt as [Basic Amt]
,Convert(decimal(18,2),CASE WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='KKF'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt 
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='KKF' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0  END) AS [KKF],
					CASE When TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='MNDTAX'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='MNDTAX' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END AS [Mandi Tax Amt],
					CASE WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='TCS'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='TCS' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt  else 0 END AS [Party TCS Amt],
					CASE WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='CGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='CGST' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END AS [CGST Amt],
					Convert(decimal(18,2),CASE WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Rate
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='SGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='SGST' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt else 0 END) AS [SGST Amt],
					
					CASE WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX1_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX2_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX3_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX4_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX5_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX6_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX7_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX8_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9='IGST'  THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX9_Amt
    				WHEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10='IGST' THEN TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.TAX10_Amt  else 0 END AS [IGST Amt],
					TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.ItemNetAmt as [Total Amt],
					 case when TSPL_SCRAPINVOICE_HEAD_Cancel_Data.EInvoice_Type='BB' then 'B2B' else 'B2C' end as [B2B/B2C]
,TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Ack_No,TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Ack_Date,TSPL_SCRAPINVOICE_HEAD_Cancel_Data.IRN_No
,Report_UOM.UOM_Code as Report_UOM,
(shipped_Qty * tspl_item_uom_detail.Conversion_Factor/Report_UOM.Conversion_Factor ) as ReportUOM_Qty,0 as [Subsidy Amt], TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Invoice_Type as [Invoice Type]
                         from TSPL_SCRAPINVOICE_HEAD_Cancel_Data
left join TSPL_SCRAPINVOICE_DETAIL_Cancel_Data on TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.invoice_No=TSPL_SCRAPINVOICE_HEAD_Cancel_Data.invoice_No
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Item_Code
left join tspl_item_uom_detail  on tspl_item_uom_detail.item_code=TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.item_code and tspl_item_uom_detail.UOM_Code=TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Unit_code
left join tspl_item_uom_detail Report_UOM  on Report_UOM.item_code=TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.item_code and Report_UOM.Report_UOM=1
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Loc_Code
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD_Cancel_Data.cust_Code
 LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
 left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
where convert(date,shipment_Date,103)>=Convert( Date,'" + strtxtfDate + "',103) and convert(date,shipment_Date,103)<=Convert( Date,'" + strToDate + "',103) "
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                BaseQryCancel += " and tspl_customer_master.Cust_Group_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                BaseQryCancel += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtSubLocation.arrValueMember IsNot Nothing AndAlso TxtSubLocation.arrValueMember.Count > 0 Then
                BaseQryCancel += " and TSPL_SCRAPINVOICE_HEAD_Cancel_Data.Sub_Location_code in(" + clsCommon.GetMulcallString(TxtSubLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                BaseQryCancel += " and TSPL_SCRAPINVOICE_DETAIL_Cancel_Data.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")" + Environment.NewLine
            End If

            If rbtnDetail.IsChecked AndAlso rdbCancelInvoice.IsChecked Then
                qry = "Select (Transcation_Type)[Transcation Type],([Customer Type])[Customer Type],(Doc_Status)[Doc Status],(Location)Location,([Sub Location])[Sub Location],(Invoice_Date)Invoice_Date,Invoice_No,([Route No])[Route No],([Party Code])[Party Code],([Party Name])[Party Name],([GST No])[GST No],
                       ([Customer GSTNo])[Customer GSTNo],([State Code])[State Code],([Measure of Qty])[Measure of Qty],([Product Qty])[Product Qty],Cast(([IGST Rate]) as decimal(10,2))[GST Rate],Cast(([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast((KKF) as decimal(10,2))KKF,Cast(([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(([IGST Amt]) as decimal(10,2))[GST Amt],cast(([Total Amt]) as decimal(10,2))[Total Amt],([B2B/B2C])[B2B/B2C],(Ack_No)[Ack No],(Ack_Date)[Ack Date],(IRN_No)[IRN No],(Report_UOM)[Report UOM],(ReportUOM_Qty)[ReportUOM Qty],([Subsidy Amt])[Subsidy Amt] from (" + BaseQryCancel + ")XX   "
                If TxtTransaction.arrValueMember IsNot Nothing AndAlso TxtTransaction.arrValueMember.Count > 0 Then
                    qry += " where XX.Transcation_Type In(" + clsCommon.GetMulcallString(TxtTransaction.arrValueMember) + ")" + Environment.NewLine
                End If
                'qry = BaseQryCancel
            ElseIf rbtnSummary.IsChecked AndAlso rdbCancelInvoice.IsChecked Then
                qry = " Select max(Transcation_Type)[Transcation Type],max([Customer Type])[Customer Type],max(Doc_Status)[Doc Status],max(Location)Location,max([Sub Location])[Sub Location],max(Invoice_Date)Invoice_Date,Invoice_No,MAX([Route No])[Route No],max([Party Code])[Party Code],max([Party Name])[Party Name],max([GST No])[GST No],
                        max([Customer GSTNo])[Customer GSTNo],max([State Code])[State Code],max([Measure of Qty])[Measure of Qty],max([Product Qty])[Product Qty],Cast(Max([IGST Rate]) as decimal(10,2))[GST Rate],Cast(sum([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast(max(KKF) as decimal(10,2))KKF,Cast(sum([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(sum([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(sum([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(sum([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(sum([IGST Amt]) as decimal(10,2))[GST Amt],cast(sum([Total Amt]) as decimal(10,2))[Total Amt],max([B2B/B2C])[B2B/B2C],max(Ack_No)[Ack No],max(Ack_Date)[Ack Date],max(IRN_No)[IRN No],max(Report_UOM)[Report UOM],Sum(ReportUOM_Qty)[ReportUOM Qty],sum([Subsidy Amt])[Subsidy Amt] from (" + BaseQryCancel + " )XX  "
                If TxtTransaction.arrValueMember IsNot Nothing AndAlso TxtTransaction.arrValueMember.Count > 0 Then
                    qry += " where XX.Transcation_Type In(" + clsCommon.GetMulcallString(TxtTransaction.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " Group by xx.Invoice_No "
                'group by xx.Invoice_No "
            End If

            Dim qryreturn As String = ""

            qryreturn = " select  case when TSPL_SD_SALE_RETURN_HEAD.Screen_Type='CT' then 'APS' else (case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' then 'MCC' else 'Dairy Sale'end)end as Transaction_Type,
case when TSPL_SD_SALE_RETURN_HEAD.Status=1 then 'Approved' else'Pending' end as Doc_Status,
TSPL_CUSTOMER_MASTER.Cust_Type_Code as[Customer Type],
TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location AS [Location],
TSPL_SD_SALE_RETURN_HEAD.Sub_Location_code AS [Sub Location],
Convert(varchar(20),TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Invoice_Date,
TSPL_SD_SALE_RETURN_HEAD.Document_Code as Invoice_No,
TSPL_SD_SALE_RETURN_HEAD.Route_No as [Route No],
TSPL_CUSTOMER_MASTER.Cust_Code as[Party Code],
TSPL_CUSTOMER_MASTER.Customer_Name AS [Party Name],
 TSPL_LOCATION_MASTER.GSTNO AS [GST No],
 TSPL_CUSTOMER_MASTER.GSTNO as [Customer GSTNo],
  TSPL_STATE_MASTER.GST_STATE_Code AS [State Code],
   TSPL_SD_SALE_RETURN_DETAIL.Item_Code as [Item Code],
tspl_item_master.Item_Desc as [Item Name],
  TSPL_SD_SALE_RETURN_DETAIL.Qty as [Product Qty],
  tspl_item_master.HSN_Code,
  case when TSPL_SD_SALE_RETURN_DETAIL.Tax1='KKF' or TSPL_SD_SALE_RETURN_DETAIL.Tax2='KKF' then (case when TSPL_SD_SALE_RETURN_DETAIL.tax3='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate else  TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate + TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate end ) else (case when  TSPL_SD_SALE_RETURN_DETAIL.tax1='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate else TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate +TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate end)end as [IGST Rate], 
  --case when TSPL_SD_SALE_RETURN_DETAIL.Tax1='KKF' or TSPL_SD_SALE_RETURN_DETAIL.Tax2='KKF' then (case when TSPL_SD_SALE_RETURN_DETAIL.tax3='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Base_Amt else  TSPL_SD_SALE_RETURN_DETAIL.TAX3_Base_Amt end ) else (case when  TSPL_SD_SALE_RETURN_DETAIL.tax1='IGST' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Base_Amt else TSPL_SD_SALE_RETURN_DETAIL.TAX1_Base_Amt end)end as [Basic Amt]
  TSPL_SD_SALE_RETURN_DETAIL.Item_Cost*TSPL_SD_SALE_RETURN_DETAIL.Qty as [Basic Amt]
,Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX1='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='KKF'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt 
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='KKF' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt else 0  END) AS [KKF],
					CASE When TSPL_SD_SALE_RETURN_DETAIL.TAX1='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='MNDTAX'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='MNDTAX' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt else 0 END AS [Mandi Tax Amt],
					CASE WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX1='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='TCS'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='TCS' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt  else 0 END AS [Party TCS Amt],
					CASE WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX1='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='CGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='CGST' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt else 0 END AS [CGST Amt],
					Convert(decimal(18,2),CASE WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX1='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='SGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='SGST' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt else 0 END) AS [SGST Amt],
					
					CASE WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX1='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX2='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX3='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX4='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX5='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX6='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX7='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX8='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX9='IGST'  THEN TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SALE_RETURN_DETAIL.TAX10='IGST' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt  else 0 END AS [IGST Amt],
					TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt as [Total Amt],
					 case when TSPL_SD_SALE_RETURN_HEAD.EInvoice_Type='BB' then 'B2B' else 'B2C' end as [B2B/B2C],
TSPL_SD_SALE_RETURN_HEAD.Ack_No,TSPL_SD_SALE_RETURN_HEAD.Ack_Date,TSPL_SD_SALE_RETURN_HEAD.IRN_No,
Report_UOM.UOM_Code as Report_UOM,
(Qty * tspl_item_uom_detail.Conversion_Factor/Report_UOM.Conversion_Factor ) as ReportUOM_Qty
,TSPL_SD_SALE_RETURN_HEAD.TotalSubsidyAmt as [Subsidy Amt],Case when TSPL_SD_SALE_RETURN_HEAD.Is_Taxable=1 then 'Taxable' else 'Non-Taxable' end as [Invoice Type]
                           from TSPL_SD_SALE_RETURN_HEAD
left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
left join tspl_item_uom_detail  on tspl_item_uom_detail.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and tspl_item_uom_detail.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
left join tspl_item_uom_detail Report_UOM  on Report_UOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and Report_UOM.Report_UOM=1
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location
 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
 LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State = TSPL_STATE_MASTER.STATE_CODE
 left outer join TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
where convert(date,Document_Date,103)>=Convert( Date,'" + strtxtfDate + "',103) and convert(date,Document_Date,103)<=Convert( Date,'" + strToDate + "',103) "

            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                qryreturn += " and tspl_customer_master.Cust_Group_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qryreturn += " and tspl_customer_master.cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtSubLocation.arrValueMember IsNot Nothing AndAlso TxtSubLocation.arrValueMember.Count > 0 Then
                qryreturn += " and TSPL_SD_SALE_RETURN_HEAD.Sub_Location_code in(" + clsCommon.GetMulcallString(TxtSubLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qryreturn += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code in(" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")" + Environment.NewLine
            End If

            If rbtnDetail.IsChecked AndAlso rdbSaleReturn.IsChecked Then
                qry = "Select (Transaction_Type)[Transcation Type],([Customer Type])[Customer Type],(Doc_Status)[Doc Status],(Location)Location,([Sub Location])[Sub Location],(Invoice_Date)Invoice_Date,Invoice_No,([Route No])[Route No],([Party Code])[Party Code],([Party Name])[Party Name],([GST No])[GST No],
                       ([Customer GSTNo])[Customer GSTNo],([State Code])[State Code],([Product Qty])[Product Qty],Cast(([IGST Rate]) as decimal(10,2))[GST Rate],Cast(([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast((KKF) as decimal(10,2))KKF,Cast(([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(([IGST Amt]) as decimal(10,2))[IGST Amt],cast(([Total Amt]) as decimal(10,2))[Total Amt],([B2B/B2C])[B2B/B2C],(Ack_No)[Ack No],(Ack_Date)[Ack Date],(IRN_No)[IRN No],(Report_UOM)[Report UOM],(ReportUOM_Qty)[ReportUOM Qty],([Subsidy Amt])[Subsidy Amt] from (" + qryreturn + ")XX "
            ElseIf rbtnSummary.IsChecked AndAlso rdbSaleReturn.IsChecked Then
                qry = " Select  max(Transaction_Type)[Transcation Type],max([Customer Type])[Customer Type],max(Doc_Status)[Doc Status],max(Location)Location,max([Sub Location])[Sub Location],max(Invoice_Date)Invoice_Date,Invoice_No,MAX([Route No])[Route No],max([Party Code])[Party Code],max([Party Name])[Party Name],max([GST No])[GST No],
                        max([Customer GSTNo])[Customer GSTNo],max([State Code])[State Code],max([Product Qty])[Product Qty],Cast(Max([IGST Rate]) as decimal(10,2))[GST Rate],Cast(sum([Basic Amt]) as Decimal(10,2))[Basic Amt],Cast(max(KKF) as decimal(10,2))KKF,Cast(sum([Mandi Tax Amt]) as Decimal(10,2))[Mandi Tax Amt],Cast(sum([Party TCS Amt]) as Decimal(10,2))[Party TCS Amt],
                        Cast(sum([CGST Amt]) as Decimal(10,2))[CGST Amt],Cast(sum([SGST Amt]) as Decimal(10,2))[SGST Amt],Cast(sum([IGST Amt]) as decimal(10,2))[GST Amt],cast(sum([Total Amt]) as decimal(10,2))[Total Amt],max([B2B/B2C])[B2B/B2C],max(Ack_No)[Ack No],max(Ack_Date)[Ack Date],max(IRN_No)[IRN No],max(Report_UOM)[Report UOM],Sum(ReportUOM_Qty)[ReportUOM Qty],sum([Subsidy Amt])[Subsidy Amt] from (" + qryreturn + " )XX  group by xx.Invoice_No "
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = Nothing
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt

                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = True
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                SetGridFormat()
                EnableDisableCntrl(False)
                'SetGridFormationOFGV1()
                gvData.BestFitColumns()

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        ReStoreGridLayout()
    End Sub


    Sub SetGridFormat()
        Try
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            For ii As Integer = 0 To gvData.Columns.Count - 1
                gvData.Columns(ii).ReadOnly = True
                gvData.Columns(ii).BestFit()
            Next

            'gvData.Columns("Transcation Type").HeaderText = "Transaction Type"
            gvData.Columns("Doc Status").HeaderText = "Status"
            'gvData.Columns("Location").HeaderText = "Location"
            'gvData.Columns("Sub Location").HeaderText = "Sub Location"
            gvData.Columns("Invoice_Date").HeaderText = "Invoice Date"
            gvData.Columns("Invoice_No").HeaderText = "Invoice No"
            'gvData.Columns("Party Name").HeaderText = "Party Name"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
            'gvData.Columns("Transaction_Type").HeaderText = "Transaction Type"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Sale Invoice Status Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Sale Invoice Status Report", gvData, arrHeader, "Sale Invoice Status Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If

    End Sub

    Private Sub rbtnDetail_CheckStateChanged(sender As Object, e As EventArgs) Handles rbtnDetail.CheckStateChanged
        If rbtnDetail.IsChecked Then
            txtItem.Visible = True
            MyLabel4.Visible = True
        ElseIf rbtnDetail.IsChecked = False Then
            txtItem.Visible = False
            MyLabel4.Visible = False
        End If
    End Sub
End Class