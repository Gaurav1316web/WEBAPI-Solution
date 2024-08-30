''''' bug no BM00000000229 , BM00000000548 ,BM00000000540,BM00000000617
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Public Class frmCustomerWiseMonthlySalesAnalysis
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptCustomerWiseMonthlySalesAnalysis)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkVendorAll.IsChecked = True
        chkCustAll.IsChecked = True
        LoadVendor()
        LoadCustomer()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        txtYear.Value = clsCommon.GETSERVERDATE()

    End Sub

    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER order by Vendor_Code "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub
    Sub LoadCustomer()
        Dim qry As String = " select Cust_Code as Code, Customer_Name Name  from TSPL_CUSTOMER_MASTER order by Cust_Code "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbAmtAfterDisc.IsChecked = True Then
            VarID += "_A"
        Else
            rdbNetAmount.IsChecked = True
            VarID += "_N"
        End If
        gv1.VarID = VarID
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        GetReportGridID()
        PrintData()
    End Sub
    Sub PrintData()

        Try
            Dim StrQuery As String
            Dim strAmount As String
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Vendor")
                Return
            End If
            If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Customer")
                Return
            End If

            If rdbAmtAfterDisc.IsChecked Then
                strAmount = "Amt_Less_Discount"
            Else
                strAmount = "Item_Net_Amt" '--------------due to spelling mistake error comes BM00000002411
            End If
            StrQuery = "SELECT CM.Cust_Code as [CustomerCode] , max(CM.Customer_Name) as [CustomerName], " & _
                       "max(VM.Vendor_Name)as [VendorName], SUM(ISNULL(SIDetail." + strAmount + ",0)) as [" + strAmount + "] " & _
                       "FROM TSPL_SD_SALE_INVOICE_HEAD SIHead " & _
                       "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM ON CM.Cust_Code = SIHead.Customer_Code " & _
                       "LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL SIDetail on SIDetail.DOCUMENT_CODE= SIHead.Document_Code " & _
                       "LEFT OUTER JOIN TSPL_VENDOR_ITEM_DETAIL VIDetail on VIDetail.item_no = SIDetail.Item_Code " & _
                       "LEFT OUTER JOIN TSPL_VENDOR_MASTER VM ON VM.Vendor_Code = VIDetail.vendor_code " & _
                       "where 1 = 1 "

            Dim strWhrCls = ""

            If (chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count > 0) Then
                StrQuery += " and VM.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
            End If

            If (chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0) Then
                StrQuery += " and CM.Cust_Code IN  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If

            Dim Month As Integer = txtMonth.Value.Month
            Dim Year As Integer = txtYear.Value.Year

            Dim fDate As DateTime = New DateTime(Year, Month, 1)
            Dim tDate As DateTime = New DateTime(Year, Month, DateTime.DaysInMonth(Year, Month))

            StrQuery += " and convert(date,SIHead.Document_Date,103) >=convert(date,'" + clsCommon.GetDateWithStartTime(fDate) + "',103) and convert(date,SIHead.Document_Date,103)<=convert(date,'" + clsCommon.GetDateWithEndTime(tDate) + "',103)" & _
                        " group by CM.Cust_Code, VM.Vendor_Code "


            Dim strVendorQuery = "select DISTINCT  [VendorName] FROM ( " + StrQuery + " ) AS FINAL where isnull(final.vendorname,'')<>''" '---------------21/04/2014------------BM00000002411---Monika--------------
            Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable(strVendorQuery)

            Dim strVendor = ""
            '---------------21/04/2014------------BM00000002411---Monika--------------
            If dtVendor.Rows.Count = 0 Then
                clsCommon.MyMessageBoxShow("No Records Found")
                Return
            End If
            '------------------------------------------------------------------------

            If Not dtVendor Is Nothing And dtVendor.Rows.Count > 0 Then
                For Each dr As DataRow In dtVendor.Rows
                    If String.IsNullOrEmpty(strVendor) Then
                        strVendor = "[" + clsCommon.myCstr(dr("VendorName")) + "]"
                    Else
                        strVendor += ", [" + clsCommon.myCstr(dr("VendorName")) + "]"
                    End If
                Next
            End If

            Dim strFinalQuery = "select *  from ( " + StrQuery + " ) AS final Pivot ( sum(final." + strAmount + ") for [VendorName]  IN (" + strVendor + ") ) as Pivot1"
            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(strFinalQuery)

            dt.Columns.Add("Total", Type.GetType("System.Double"))
            Dim total As Double = 0
            For ii As Integer = 0 To dt.Rows.Count - 1
                For jj As Integer = 2 To dt.Columns.Count - 2
                    total += clsCommon.myCdbl(dt.Rows(ii)(jj))
                Next
                dt.Rows(ii)("Total") = total
            Next

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2

            'If dt.Rows.Count > 0 Then
            '    NewSalesReportViewer.funreport(dt, "crptGuarnteeForm-E", "Guarantee Form - E")
            'Else
            '    Throw New Exception("No data found.")
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            'gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns(0).IsVisible = True
        gv1.Columns(0).Width = 100
        gv1.Columns(0).HeaderText = "Customer Code"

        gv1.Columns(1).IsVisible = True
        gv1.Columns(1).Width = 150
        gv1.Columns(1).HeaderText = "Customer Name"


        For jj As Integer = 2 To gv1.Columns.Count - 1
            gv1.Columns(jj).Width = 100
        Next

        'gv1.Columns("Item Description").IsVisible = True
        'gv1.Columns("Item Description").Width = 200
        'gv1.Columns("Item Description").HeaderText = "Item Description"


        'gv1.Columns("Qty").IsVisible = True
        'gv1.Columns("Qty").Width = 150
        'gv1.Columns("Qty").HeaderText = "Quantity"

        'gv1.Columns("Item Cost").IsVisible = True
        'gv1.Columns("Item Cost").Width = 150
        'gv1.Columns("Item Cost").HeaderText = "Price"

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'Dim item4 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'Dim item5 As New GridViewSummaryItem("Item Cost", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)

        'gv1.ShowGroupPanel = False
        'gv1.MasterTemplate.AutoExpandGroups = True

        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        'RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        txtYear.Value = clsCommon.GETSERVERDATE()
        'txtItem.txtValue.Text = ""
        'lblItem.Text = ""
        chkVendorAll.IsChecked = True
        'chkInvoiceAll.IsChecked = True
        chkCustAll.IsChecked = True

    End Sub



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVendor.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgVendor.Enabled = True
    End Sub

    Private Sub chkIemAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = True
    End Sub

    Private Sub frmCustomerWiseMonthlySalesAnalysis_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub


    Private Sub cbgLocation_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'LoadMrp()
    End Sub

    Private Sub cbgItem_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'LoadMrp()
    End Sub
    Private Sub chkmrpselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCustomer.Enabled = True
    End Sub
    Private Sub chkmrpall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCustomer.Enabled = False
    End Sub


    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            'Dim arrHeader As List(Of String) = New List(Of String)()
            'Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtMonth.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtYear.Value, "dd/MM/yyyy")
            'arrHeader.Add(strtemp)

            'If chkInvoiceSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgInvoice.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Item : " + strtemp)
            'End If

            'If chkVendorSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgVendor.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Location : " + strtemp)
            'End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Customer Wise Monthly Sales Analysis Report", gv1, Nothing, Me.Text)

            Else
                clsCommon.MyExportToPDF("Customer Wise Monthly Sales Analysis Report", gv1, Nothing, "Customer Wise Monthly Sales Analysis Report", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = Not chkInvoiceAll.IsChecked
    End Sub

    Private Sub chkmrpall_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked

    End Sub

    Private Sub chkLocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked

    End Sub




End Class
