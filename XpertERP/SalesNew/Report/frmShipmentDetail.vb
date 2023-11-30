'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 24/05/2013-------------------------------------
'--------------------------------Last modify Time - 03:55 PM -------------------------------------
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
Public Class FrmShipmentDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmShipmentDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmShipmentDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Reset()
    End Sub
    Sub Reset()
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        rdbAll.IsChecked = True
        LoadLocation()
        LoadItem()
        gv.DataSource = Nothing
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
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
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
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Dim strCustomer As String = ""
        Dim strStatus As String = ""
        Dim strLocationInv As String = ""
        Dim strItemInv As String = ""
        Dim strItemShip As String = ""


        If rdbAll.IsChecked Then
            strStatus = "All"
        ElseIf rdbpending.IsChecked Then
            strStatus = "Pending"
        ElseIf rdbPartial.IsChecked Then
            strStatus = "Partial Shipped"
        ElseIf rdbComplete.IsChecked Then
            strStatus = "Complete"
        End If
        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then

            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If cbgItem.CheckedValue.Count > 0 Then
                strItemInv += " and  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                strItemShip += " and  TSPL_SD_SHIPMENT_Detail.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            Else
                strItemShip = ""
                strItemInv = ""

            End If


            If cbgLocation.CheckedValue.Count > 0 Then
                strLocationInv += " and  TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strLocationInv += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If
            Dim str As String = "select '" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate,'" & strStatus & "' as status, " & _
                          "Document_Code,Document_Date,Customer_Name,Customer_Code,Salesman_Name,Bill_To_Location ,Location_Desc,Item_Code,Item_Desc,sum(ShippedQty) as ShippedQty, " & _
                          "sum(InvQty) as InvoiceQty,sum(ShippedQty-InvQty) as Outstanding,Unit_code,max(rate) as rate,sum(Amount) as Amount, " & _
                          "max(Disc_Per) as Disc_Per,sum(Disc_Amt) as Disc_Amt,sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as Total_Tax_Amt, " & _
                          "sum(totalShipAmt) as totalShipAmt,sum(totalInvAmt) as totalInvAmt from ( " & _
                          "SELECT  TSPL_SD_SHIPMENT_HEAD.Document_Code, TSPL_SD_SHIPMENT_HEAD.Document_Date, " & _
                          "TSPL_SD_SHIPMENT_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SHIPMENT_HEAD.Salesman_Code, " & _
                          "TSPL_SD_SHIPMENT_HEAD.Salesman_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SHIPMENT_Detail.Item_Code, " & _
                          "TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SHIPMENT_Detail.Qty as ShippedQty, 0 as InvQty,TSPL_SD_SHIPMENT_Detail.Unit_code, " & _
                          "Item_Cost as rate,Amount,Disc_Per,Disc_Amt,TSPL_SD_SHIPMENT_Detail.Amt_Less_Discount,TSPL_SD_SHIPMENT_Detail.Total_Tax_Amt, " & _
                          "TSPL_SD_SHIPMENT_Detail.Item_Net_Amt as totalShipAmt,0 as totalInvAmt FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                          "TSPL_SD_SHIPMENT_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                          "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                          "TSPL_SD_SHIPMENT_Detail ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_Detail.Item_Code ON " & _
                          "TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_Detail.Document_Code " & _
                     " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location " & _
" where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                         " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strCustomer & "  " & strLocationInv & " " & strItemShip & " " & _
                          "union all " & _
                          " SELECT  TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No AS Document_Code, TSPL_SD_SHIPMENT_HEAD.Document_Date, " & _
                          "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code, " & _
                          "TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, " & _
                          "TSPL_ITEM_MASTER.Item_Desc,0as ShippedQty,  TSPL_SD_SALE_INVOICE_DETAIL.Qty  as InvQty, " & _
                          "TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,0 as amount,0 as Disc_Per,0 as Disc_Amt,0 as Amt_Less_Discount, " & _
                          " 0 as Total_Tax_Amt,0 as totalShipAmt,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as totalInvAmt " & _
                          " FROM   TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                          " TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code RIGHT OUTER JOIN " & _
                          " TSPL_SD_SALE_INVOICE_HEAD INNER JOIN " & _
                          " TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No ON " & _
                          " TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code LEFT OUTER JOIN " & _
                          " TSPL_CUSTOMER_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " & _
                     " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
" where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                          " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strCustomer & "  " & strLocationInv & " " & strItemInv & " " & _
                          ") xx " & _
                          " group by Item_Code,Document_Code,Document_Date,Customer_Name,Customer_Code,Salesman_Name,Item_Code,Item_Desc,Unit_code,Bill_To_Location,Location_Desc "
            If rdbAll.IsChecked Then
                str = str & " order by Document_Code"
            ElseIf rdbpending.IsChecked Then
                str = str & " having SUM(ShippedQty) = sum(ShippedQty-InvQty) order by Document_Code"
            ElseIf rdbPartial.IsChecked Then
                str = str & " having SUM(ShippedQty) > sum(ShippedQty-InvQty) and sum(ShippedQty-InvQty) <> 0 order by Document_Code"
            ElseIf rdbComplete.IsChecked Then
                str = str & " having sum(ShippedQty-InvQty)=0  order by Document_Code"

            End If


            dt = clsDBFuncationality.GetDataTable(str)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptShipmentDetail", "Shipment Detail Report")
                frmCRV = Nothing
            Else
                gv.DataSource = dt
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcel("Shipment Detail", gv, Nothing, Me.Text)
        Else
            clsCommon.MyExportToPDF("Shipment Detail", gv, Nothing, "Shipment Detail", False)
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

        Try
            gv.Columns("status").IsVisible = True
            gv.Columns("status").Width = 70
            gv.Columns("status").HeaderText = "Status"
        Catch ex As Exception
        End Try

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

        'gv.Columns("Delivery_date").IsVisible = True
        'gv.Columns("Delivery_date").Width = 100
        'gv.Columns("Delivery_date").HeaderText = "Delivery Date"

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

        'gv.Columns("OrderQty").IsVisible = True
        'gv.Columns("OrderQty").Width = 100
        'gv.Columns("OrderQty").HeaderText = "Order Qty"

        Try
            gv.Columns("ShippedQty").IsVisible = True
            gv.Columns("ShippedQty").Width = 100
            gv.Columns("ShippedQty").HeaderText = "Shipped Qty"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("InvoiceQty").IsVisible = True
            gv.Columns("InvoiceQty").Width = 100
            gv.Columns("InvoiceQty").HeaderText = "Invoice Qty"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("Outstanding").IsVisible = True
            gv.Columns("Outstanding").Width = 100
            gv.Columns("Outstanding").HeaderText = "Outstanding"
        Catch ex As Exception
        End Try

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
            gv.Columns("totalShipAmt").IsVisible = True
            gv.Columns("totalShipAmt").Width = 100
            gv.Columns("totalShipAmt").HeaderText = "Total Shipped Amt"
        Catch ex As Exception
        End Try

        Try
            gv.Columns("totalInvAmt").IsVisible = True
            gv.Columns("totalInvAmt").Width = 100
            gv.Columns("totalInvAmt").HeaderText = "Total Invoice Amt"
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnPrint1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint1.Click

        LoadData(Exporter.Print)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Reset()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strDoc = gv.CurrentRow.Cells("Document_Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strDoc)
        End If
    End Sub
End Class
