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
Public Class FrmSaleInvoiceSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleInvoiceSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
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

        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then

            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If

            If cbgLocation.CheckedValue.Count > 0 Then
                strLocationInv += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strLocationInv += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If
            Dim str As String = "select max(Fdate) as Fdate,max(Tdate) as Tdate,Document_Code,Document_Date,max(aa.Parent_Customer_No) as ParentCode,MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code, " &
                        "aa.Customer_Name,Salesman_Name,Bill_To_Location,Location_Desc,SUM(TotalAmt) as totalInvAmt,'" & objCommonVar.CurrentUser & "' as User_Name  from ( " &
                        "SELECT    '" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate, TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " &
                        "TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, " &
                        "TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, " &
                        "TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, " &
                        "TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Qty as InvoiceQty, 0 as ShippedQty, " &
                        "TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,Item_Cost as rate,Amount,Disc_Per,Disc_Amt, " &
                        "TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt, " &
                        "TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as TotalAmt FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " &
                        "TSPL_SD_SALE_INVOICE_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code RIGHT OUTER JOIN " &
                        "TSPL_ITEM_MASTER RIGHT OUTER JOIN " &
                        "TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ON " &
                        "TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " &
       " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " &
" where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " &
                         " convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'   " & strCustomer & "  " & strLocationInv & ") aa left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=aa.Parent_Customer_No " &
                        "group by Document_Code,Document_Date,Customer_Code,aa.Customer_Name,Salesman_Name,Bill_To_Location,Location_Desc order by Document_Code"



            dt = clsDBFuncationality.GetDataTable(str)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptSaleInvoiceSummary", "Sale Invoice Summary Report")
                frmCRV = Nothing
            Else
                gv.DataSource = dt
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcel("Sale Invoice Summary", gv, Nothing, Me.Text)
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

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        cbgCustomer.UnCheckedAll()
        cbgLocation.UnCheckedAll()
        gv.DataSource = Nothing
    End Sub

    Private Sub FrmSaleInvoiceSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        LoadLocation()
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
            Dim strDoc
            strDoc = gv.CurrentRow.Cells("Document_Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
        End If
    End Sub
End Class
