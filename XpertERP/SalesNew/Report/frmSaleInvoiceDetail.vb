
'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 24/05/2013-------------------------------------
'--------------------------------Last modify Time - 12:45 PM -------------------------------------
'--------------------------------Ticket No-BM00000000580  Changes by- Dipti Waila -------------------------------------
'--------------------------------Ticket No-BM00000000767  Changes by- Shipra Jain -------------------------------------
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class FrmSaleInvoiceDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleInvoiceDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub
    Private Sub FrmSaleInvoiceDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadLocation()
        LoadItem()
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            strquery += "  and tspl_customer_master.cust_code in (" + objCommonVar.strCurrUserCustomers + ")"
        End If
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += "  and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Private Sub btnPrint1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint1.Click
        LoadData(Exporter.Print)
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Dim strCustomer As String = ""
        Dim strLocationInv As String = ""
        Dim strItemInv As String = ""

        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then
            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If

            If cbgItem.CheckedValue.Count > 0 Then
                strItemInv += " and  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            Else
                strItemInv = ""
            End If


            If cbgLocation.CheckedValue.Count > 0 Then
                strLocationInv += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strLocationInv += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If

            Dim str As String = "SELECT    '" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, " & _
            " TSPL_SD_SALE_INVOICE_HEAD.Document_Code, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode,Parent_Master.Customer_Name as ParentName ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, " & _
                        "TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code, " & _
                        "TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, " & _
                        "TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Qty as InvoiceQty, 0 as ShippedQty, " & _
                        "TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,Amount,Disc_Per,Disc_Amt, " & _
                       "TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt, " & _
    " TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt FROM TSPL_CUSTOMER_MASTER left outer join tspl_customer_master as Parent_Master on Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No RIGHT OUTER JOIN " & _
                            "TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                            "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                            "TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON " & _
                            "TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
"left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                            " convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'   " & strCustomer & " " & strLocationInv & " " & strItemInv & ""
            str += " order by Document_Code"


            dt = clsDBFuncationality.GetDataTable(str)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptSaleInvoiceDetail", "Sale Invoice Detail Report")
                frmCRV = Nothing
            Else
                gv.DataSource = dt
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcel("Sale Invoice Detail", gv, Nothing, Me.Text)
        Else
            clsCommon.MyExportToPDF(Me.Text, gv, Nothing, Me.Text)
        End If
    End Sub
    Sub SetGridFormation()
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.DataSource = dt
        gv.AllowAddNewRow = False
        gv.AllowDragToGroup = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        Try
            gv.Columns("Fdate").IsVisible = True
            gv.Columns("Fdate").Width = 100
            gv.Columns("Fdate").HeaderText = "From Date"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Tdate").IsVisible = True
            gv.Columns("Tdate").Width = 100
            gv.Columns("Tdate").HeaderText = "To date"
        Catch ex As Exception
        End Try
        'gv.Columns("status").IsVisible = True
        'gv.Columns("status").Width = 70
        'gv.Columns("status").HeaderText = "Status"
        Try
            gv.Columns("Document_Code").IsVisible = True
            gv.Columns("Document_Code").Width = 100
            gv.Columns("Document_Code").HeaderText = "Document Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Document_Date").IsVisible = True
            gv.Columns("Document_Date").Width = 100
            gv.Columns("Document_Date").HeaderText = "Document Date"
        Catch ex As Exception
        End Try
        Try
            gv.Columns("ParentCode").IsVisible = True
            gv.Columns("ParentCode").Width = 100
            gv.Columns("ParentCode").HeaderText = "Parent Customer Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("ParentName").IsVisible = True
            gv.Columns("ParentName").Width = 200
            gv.Columns("ParentName").HeaderText = "Parent Name"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Customer_Code").IsVisible = True
            gv.Columns("Customer_Code").Width = 100
            gv.Columns("Customer_Code").HeaderText = "Customer Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Customer_Name").IsVisible = True
            gv.Columns("Customer_Name").Width = 300
            gv.Columns("Customer_Name").HeaderText = "Customer Name"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Salesman_Name").IsVisible = True
            gv.Columns("Salesman_Name").Width = 100
            gv.Columns("Salesman_Name").HeaderText = "Salesman Name"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Bill_To_Location").IsVisible = True
            gv.Columns("Bill_To_Location").Width = 100
            gv.Columns("Bill_To_Location").HeaderText = "Location Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Location_Desc").IsVisible = True
            gv.Columns("Location_Desc").Width = 120
            gv.Columns("Location_Desc").HeaderText = "Location Desc"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 100
            gv.Columns("Item_Code").HeaderText = "Item Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 200
            gv.Columns("Item_Desc").HeaderText = "Item Description"
        Catch ex As Exception
        End Try


        Try
            gv.Columns("InvoiceQty").IsVisible = True
            gv.Columns("InvoiceQty").Width = 100
            gv.Columns("InvoiceQty").HeaderText = "Invoice Qty"
        Catch ex As Exception
        End Try
        'gv.Columns("OrderQty").IsVisible = True
        'gv.Columns("OrderQty").Width = 100
        'gv.Columns("OrderQty").HeaderText = "Order Qty"
        Try
            gv.Columns("ShippedQty").IsVisible = True
            gv.Columns("ShippedQty").Width = 100
            gv.Columns("ShippedQty").HeaderText = "Shipped Qty"
        Catch ex As Exception
        End Try
        'gv.Columns("Outstanding").IsVisible = True
        'gv.Columns("Outstanding").Width = 100
        'gv.Columns("Outstanding").HeaderText = "Outstanding"
        Try
            gv.Columns("Unit_code").IsVisible = True
            gv.Columns("Unit_code").Width = 100
            gv.Columns("Unit_code").HeaderText = "Unit Code"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("rate").IsVisible = True
            gv.Columns("rate").Width = 100
            gv.Columns("rate").HeaderText = "Rate"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Disc_Per").IsVisible = True
            gv.Columns("Disc_Per").Width = 100
            gv.Columns("Disc_Per").HeaderText = "Discount %"
        Catch ex As Exception
        End Try


        Try
            gv.Columns("Disc_Amt").IsVisible = True
            gv.Columns("Disc_Amt").Width = 100
            gv.Columns("Disc_Amt").HeaderText = "Discount Amt"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Amt_Less_Discount").IsVisible = True
            gv.Columns("Amt_Less_Discount").Width = 100
            gv.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Total_Tax_Amt").IsVisible = True
            gv.Columns("Total_Tax_Amt").Width = 100
            gv.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("TotalAmt").IsVisible = True
            gv.Columns("TotalAmt").Width = 100
            gv.Columns("TotalAmt").HeaderText = "Total Amt"
        Catch ex As Exception
        End Try
        'gv.Columns("TotalShippedAmt").IsVisible = True
        'gv.Columns("TotalShippedAmt").Width = 100
        'gv.Columns("TotalShippedAmt").HeaderText = "Total Shipped Amt"

    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        cbgCustomer.UnCheckedAll()
        cbgItem.UnCheckedAll()
        cbgLocation.UnCheckedAll()
        gv.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strDoc
            strDoc = gv.CurrentRow.Cells("Document_Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
        End If
    End Sub
End Class
