Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class RptcommissionTPTReport
    Private Sub RptcommissionTPTReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnSummary.IsChecked = True
        rbtnDetail.IsChecked = False
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnSummary.IsChecked = True
        rbtnDetail.IsChecked = False
        txtMultDistr.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Commission/TPT Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Commission/TPT Report", Gv1, arrHeader, "Commission/TPT Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub txtMultDistr__My_Click(sender As Object, e As EventArgs) Handles txtMultDistr._My_Click
        Dim qry As String = " select DISTINCT Distributor_Code,IsDistributor from TSPL_CUSTOMER_MASTER "
        txtMultDistr.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Distributor_Code", "Distributor_Code", txtMultDistr.arrValueMember, txtMultDistr.arrDispalyMember)
    End Sub

    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub

    Private Sub btnGenrate_Click(sender As Object, e As EventArgs) Handles btnGenrate.Click
        griddata()
    End Sub

    'Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
    '    griddata()
    'End Sub
    Public Sub griddata()
        Try
            Dim Whr As String = ""
            Dim qry As String = ""
            If txtMultDistr.arrValueMember IsNot Nothing AndAlso txtMultDistr.arrValueMember.Count > 0 Then
                Whr += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" + clsCommon.GetMulcallString(txtMultDistr.arrValueMember) + ")" + Environment.NewLine
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Whr += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in(" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")" + Environment.NewLine
            End If
            If rbtnSummary.IsChecked Then
                qry = "SELECT 
                            ROW_NUMBER() OVER (ORDER BY MAX(TSPL_SD_SALE_INVOICE_HEAD.Document_Date)) AS [Serial No],
                            FORMAT(MAX(TSPL_SD_SALE_INVOICE_HEAD.Document_Date), 'dd/MM/yyyy') AS [Date],
                            TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Code],
                            MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS [Customer Name],
                            MAX(TSPL_SD_SALE_INVOICE_HEAD.Route_No) AS [Route No],
                            MAX(TSPL_SD_SALE_INVOICE_HEAD.Route_Desc) AS [Route Name],
                            MAX(TSPL_ITEM_MASTER.Item_Code) AS [Item Code],
                            MAX(TSPL_ITEM_MASTER.Item_Desc) AS [Item Name],
                            'LETR' AS [UOM],
                            SUM(CASE 
                                    WHEN TSPL_SD_SALE_INVOICE_DETAIL.Weight_UOM = 'ML' 
                                    THEN TSPL_SD_SALE_INVOICE_DETAIL.Qty / 1000
                                    ELSE TSPL_SD_SALE_INVOICE_DETAIL.Qty
                                END) AS [UOM Qty],
                            SUM(TSPL_SD_SALE_INVOICE_DETAIL.Amount) AS [Total Amount],
                            MAX(TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt) AS [Commission],
                            MAX(TSPL_SD_SALE_INVOICE_HEAD.RT_Rate) AS [Transpotion],
							MAX(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount) AS [Security]
                        FROM 
                            TSPL_SD_SALE_INVOICE_HEAD
                        LEFT OUTER JOIN 
                            TSPL_SD_SALE_INVOICE_DETAIL 
                            ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                        LEFT OUTER JOIN 
                            TSPL_CUSTOMER_MASTER 
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        LEFT OUTER JOIN 
                            TSPL_ITEM_MASTER 
                            ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        WHERE 
                          TSPL_SD_SALE_INVOICE_HEAD.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date <='" + clsCommon.GetPrintDate(ToDate.Value) + "' " + Whr + "  
                        GROUP BY 
                            TSPL_CUSTOMER_MASTER.Cust_Code
                        ORDER BY 
                            MAX(TSPL_SD_SALE_INVOICE_HEAD.Document_Date)  "
            ElseIf rbtnDetail.IsChecked Then
                qry = "SELECT 
                        ROW_NUMBER() OVER (ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Date) AS [Serial No],
                        RIGHT('00' + CAST(DAY(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) AS VARCHAR), 2) + '/' +
                        RIGHT('00' + CAST(MONTH(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) AS VARCHAR), 2) + '/' +
                        CAST(YEAR(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) AS VARCHAR) AS [Date],
                        TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Code],
                        TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],
                        TSPL_SD_SALE_INVOICE_HEAD.Route_No AS [Route No],
                        TSPL_SD_SALE_INVOICE_HEAD.Route_Desc AS [Route Name],
                        TSPL_ITEM_MASTER.Item_Code AS [Item Code],
                        TSPL_ITEM_MASTER.Item_Desc AS [Item Name],
                        TSPL_SD_SALE_INVOICE_DETAIL.Weight_UOM AS [UOM],
                        TSPL_SD_SALE_INVOICE_DETAIL.Amount,
                        TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt AS [Commission],
                        TSPL_SD_SALE_INVOICE_HEAD.RT_Rate AS [Transpotion],
					    TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount AS [Security]
                    FROM 
                        TSPL_SD_SALE_INVOICE_HEAD
                    LEFT OUTER JOIN 
                        TSPL_SD_SALE_INVOICE_DETAIL 
                        ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                    LEFT OUTER JOIN 
                        TSPL_CUSTOMER_MASTER 
                        ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                    LEFT OUTER JOIN 
                        TSPL_ITEM_MASTER 
                        ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                    WHERE 
                       TSPL_SD_SALE_INVOICE_HEAD.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date <='" + clsCommon.GetPrintDate(ToDate.Value) + "' " + Whr + " "
            End If
            Dim dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt

                'SetGridFormat()

                Gv1.AutoExpandGroups = True
                Gv1.ShowGroupPanel = False
                Gv1.ShowRowHeaderColumn = False
                Gv1.AllowAddNewRow = False
                Gv1.AllowDeleteRow = False
                Gv1.EnableFiltering = True
                Gv1.ShowFilteringRow = True
                Gv1.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
