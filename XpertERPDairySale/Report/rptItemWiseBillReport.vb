Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions
Public Class rptItemWiseBillReport
    Inherits FrmMainTranScreen
    Private Sub rptItemWiseBillReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            Dim whrcls As String = "where 1 = 1  And convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "



            Dim qry As String = "DECLARE @cols nvarchar(max) = '',
        @sql  nvarchar(max);


SELECT @cols = @cols + '
    SUM(CASE WHEN TSPL_ITEM_MASTER.Item_Desc = ''' + Item_Desc + ''' 
             THEN (TSPL_SD_SALE_INVOICE_DETAIL.Billing_Qty * IMCF) / TSPL_ITEM_UOM_DETAIL.Conversion_Factor 
             ELSE 0 END) AS [' + Item_Desc + '(' + UOM_Code + ')],
    SUM(CASE WHEN TSPL_ITEM_MASTER.Item_Desc = ''' + Item_Desc + ''' 
             THEN TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt 
             ELSE 0 END) AS [' + Item_Desc + ' (Amt)],'
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
      AND TSPL_ITEM_UOM_DETAIL.Report_UOM = 1
) AS X;

SET @cols = LEFT(@cols, LEN(@cols) - 1);

SET @sql = N'
SELECT 
    RIGHT(TSPL_SD_SALE_INVOICE_HEAD.Document_Code, 6) AS BillNo,
    MAX(CONVERT(varchar(20), TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)) AS BillDate,
    MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS PARTY_Name,
    ' + @cols + '
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
" + whrcls + "
GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date
ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Date;';

EXEC sp_executesql @sql; "

            'qry += " group by Document_Code order by Document_Code "
            ' qry += " group by Document_Code ,Item_Code order by Document_Code "
            Dim dt As DataTable = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            Dim viewBlank As New TableViewDefinition()
            gv1.ViewDefinition = viewBlank
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                'SetGridFormation()
                EnableDisableCntrl(False)
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)

    End Sub
End Class