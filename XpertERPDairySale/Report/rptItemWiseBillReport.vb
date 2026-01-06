Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions
Public Class rptItemWiseBillReport
    Inherits FrmMainTranScreen
    Private Sub rptItemWiseBillReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Private Sub LoadData()
        Try


            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("(
    SELECT DISTINCT 
           TSPL_ITEM_MASTER.Item_Desc,
           TSPL_ITEM_UOM_DETAIL.Conversion_Factor,
           ItemCF.Conversion_Factor AS IMCF,
           TSPL_ITEM_UOM_DETAIL.UOM_Code,TSPL_ITEM_UOM_DETAIL.Report_UOM
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
      and ItemCF.UOM_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code
)")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Report UOM Is Of That item")
                For ii As Integer = 0 To dt1.Rows.Count - 1

                Next
            End If

            Dim whrcls As String = Nothing
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_CUSTOMER_MASTER.Cust_Code in ('" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + "')"
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
WHERE CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=(convert(date,(''" & txtFromDate.Value & "''),103))
  AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) < (convert(date,(''" & txtToDate.Value & "''),103)) " + whrcls + "
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
                SetGridFormation()
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
        RadGroupBox1.Enabled = False
        txtItem.Enabled = False
        txtCustomer.Enabled = False
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox1.Enabled = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtItem.Enabled = True
        txtCustomer.Enabled = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub SetGridFormation()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next


        gv1.Columns("PARTY_Name").HeaderText = "PARTY Name"
        gv1.Columns("BillNo").HeaderText = "Bill No"
        gv1.Columns("BillDate").HeaderText = "Bill Date"

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True
        'For ii As Integer = gv1.Columns("Qc").Index + 1 To gv1.Columns("Total Deduction").Index - 1
        '    gv1.Columns(ii).HeaderText = (gv1.Columns(ii).HeaderText).Replace("Ded", "")
        'Next
        'gv1.Columns("Qc").IsVisible = False
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemWiseBillReport & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
End Class