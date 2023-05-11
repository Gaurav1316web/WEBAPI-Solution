'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class FrmVehiclePendingStatusRpt

    Inherits FrmMainTranScreen
    Private Sub FrmVehicleINStatusRpt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadCustomer()
        'loadblank()
        reset()
        SetUserMgmtNew()
    End Sub
    '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmVehiclePendingStatusRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnExport.Visible = MyBase.isExport
    End Sub
    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER  order by Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub

    Sub LoadData()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strDateRange As String = ""
        Dim vehicle_in As String = ""
        Dim Receipt_in As String = ""

        Dim dt As DataTable
        Try
            If (chkCustomer_select.IsChecked And cbgCustomer.CheckedValue.Count <= 0) Then
                clsCommon.MyMessageBoxShow("Select atleast One customer or Check All.")
                'ElseIf (ChkVehicleIN.Checked = False And ChkReceiptIn.Checked = False) Then
                '    clsCommon.MyMessageBoxShow("Select Either Check box value")
            Else

                If (ChkVehicleIN.Checked = True) Then
                    vehicle_in = "N"
                Else
                    vehicle_in = "Y"
                End If
                If (ChkReceiptIn.Checked = True) Then
                    Receipt_in = "N"
                Else
                    Receipt_in = "Y"
                End If


                qry = "select Document_Code as [Invoice No],convert(date,Document_Date,105) as [Invoice Date],Bill_To_Location as [Bill To Location],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amount]," & _
                      " Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code as [Salesman Code], " & _
                      " TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as [Salesman Name],TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN as [Vehicle In], " & _
                      " TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN  as [Receipt In],'' as Remarks,'' as Comments,TSPL_RECEIPT_CHALLAN.grno,TSPL_RECEIPT_CHALLAN.grdate from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER " & _
                      " on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
" left outer join TSPL_RECEIPT_CHALLAN on TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO=TSPL_SD_SALE_INVOICE_HEAD.document_code " & _
                      " where (TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN ='" + vehicle_in + "' And TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN ='" + Receipt_in + "') " & _
                      " and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" & clsCommon.GetPrintDate(Me.dtpFrmDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpToDate.Value.AddDays(1), "dd/MMM/yyyy") & "'"

                If chkCustomer_select.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                    qry += " and  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in ( " + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If

                dt = clsDBFuncationality.GetDataTable(qry)
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                gv1.DataSource = dt
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageViewPage1
                chkCustomer_all.IsChecked = True
                ' ChkReceiptIn.Checked = False
                'ChkVehicleIN.Checked = False
                SetGridFormationOFgv()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE

        cbgCustomer.CheckedAll()

        cbgCustomer.Enabled = False
        ChkReceiptIn.Checked = False
        ChkVehicleIN.Checked = False
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1()
    End Sub
    Sub SetGridFormationOFgv()
        Try


            ' Dim strItemCode, head2 As String
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next


            gv1.Columns("Invoice No").IsVisible = True
            gv1.Columns("Invoice No").Width = 100
            gv1.Columns("Invoice No").HeaderText = "Invoice No"

            gv1.Columns("Invoice Date").IsVisible = True
            gv1.Columns("Invoice Date").Width = 100
            gv1.Columns("Invoice Date").HeaderText = "Invoice Date"

            gv1.Columns("grno").IsVisible = True
            gv1.Columns("grno").Width = 100
            gv1.Columns("grno").HeaderText = "GR No."

            gv1.Columns("grdate").IsVisible = True
            gv1.Columns("grdate").Width = 100
            gv1.Columns("grdate").HeaderText = "GR Date"

            gv1.Columns("Bill To Location").IsVisible = True
            gv1.Columns("Bill To Location").Width = 100
            gv1.Columns("Bill To Location").HeaderText = "Bill To Location"

            gv1.Columns("Total Amount").IsVisible = True
            gv1.Columns("Total Amount").Width = 80
            gv1.Columns("Total Amount").HeaderText = "Total Amount"

            gv1.Columns("Customer Code").IsVisible = True
            gv1.Columns("Customer Code").Width = 100
            gv1.Columns("Customer Code").HeaderText = "Customer Code"

            gv1.Columns("Customer Name").IsVisible = True
            gv1.Columns("Customer Name").Width = 80
            gv1.Columns("Customer Name").HeaderText = "Customer Name"

            gv1.Columns("Salesman Code").IsVisible = True
            gv1.Columns("Salesman Code").Width = 80
            gv1.Columns("Salesman Code").HeaderText = "Salesman Code"

            gv1.Columns("Salesman Name").IsVisible = True
            gv1.Columns("Salesman Name").Width = 80
            gv1.Columns("Salesman Name").HeaderText = "Salesman Name"

            gv1.Columns("Vehicle In").IsVisible = True
            gv1.Columns("Vehicle In").Width = 80
            gv1.Columns("Vehicle In").HeaderText = "Vehicle Pending"


            gv1.Columns("Receipt In").IsVisible = True
            gv1.Columns("Receipt In").Width = 80
            gv1.Columns("Receipt In").HeaderText = "Receipt Pending"



            gv1.EnableFiltering = True
            gv1.AllowDeleteRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AllowAddNewRow = False

            gv1.MasterTemplate.ShowRowHeaderColumn = False

            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub chkCustomer_all_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomer_all.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomer_all.IsChecked
        cbgCustomer.Refresh()

    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = ""
            arrHeader.Add(strtemp)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Vehicle Pending Status Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Vehicle Pending Status Report", gv1, arrHeader, "Vehicle Pending Status Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click

        LoadData()
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click, btnreset1.Click
        RadPageView1.SelectedPage = RadPageViewPage1

        ' chkCustomer_all.IsChecked = True
        reset()

    End Sub



    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub


    Private Sub chkCustomer_select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomer_select.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub


    Private Sub Export_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If clsCommon.myLen(gv1.CurrentRow.Cells("Invoice No").Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, gv1.CurrentRow.Cells("Invoice No").Value)

        End If

    End Sub
End Class
