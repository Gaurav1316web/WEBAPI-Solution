''''' bug no BM00000000229 , BM00000000548 ,BM00000000540,BM00000000617,BM00000003009
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Public Class frmCheckDepositPaySlip
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPaySlipReport)
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
        chkBankAll.IsChecked = True
        LoadBank()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Sub LoadBank()
        Dim qry As String = "select BANK_CODE as Code, DESCRIPTION as Name from TSPL_BANK_MASTER order by BANK_CODE "
        cbgBank.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBank.ValueMember = "Code"
        cbgBank.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Try
            Dim strQuery As String = ""

            If (chkBankSelect.IsChecked = True AndAlso cbgBank.CheckedValue.Count < 1) Then
                Throw New Exception("Please Select Atleast Single Bank")
            End If


            strQuery = "SELECT BM.DESCRIPTION 'Bank Name', BM.ADD1,BM.ADD2,BM.ADD3,BM.ADD3,BM.ADD4, BM.CITY,BM.STATE,BM.COUNTRY, " & _
                       "BM.BANKACCNUMBER 'Bank Account',RH.Cheque_From 'From Bank', RH.From_Branch 'From Branch',RH.Cheque_Date 'Cheque Date', " & _
                       "RH.Cheque_No 'Cheque No', RH.Receipt_Amount 'Amount', CM.Comp_Name 'Company Name',convert(varchar(15),RH.Receipt_Date, 103) 'Receipt Date', CustMaster.Customer_Name 'Customer Name' " & _
                       "FROM TSPL_RECEIPT_HEADER RH " & _
                       "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CustMaster ON CustMaster.Cust_Code= RH.Cust_Code " & _
                       "LEFT OUTER JOIN TSPL_BANK_MASTER BM ON BM.BANK_CODE=RH.Bank_Code " & _
                       "LEFT OUTER JOIN TSPL_COMPANY_MASTER CM on CM.Comp_Code= RH.Comp_Code " & _
                       "WHERE IsChkReverse='N'  and Receipt_Type not in ('F')  " & _
                       "AND Receipt_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Text)), "dd/MMM/yyyy") + "' " & _
                       "AND Receipt_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Text)), "dd/MMM/yyyy") + "' "

            If (chkBankSelect.IsChecked = True And cbgBank.CheckedValue.Count > 0) Then
                strQuery += "AND BM.BANK_CODE in (" + clsCommon.GetMulcallString(cbgBank.CheckedValue) + ") "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "rptPaySlip", "Cheque Deposit Slip/Pay-in-Slip")
                frmCRV = Nothing
            Else
                Throw New Exception("No data found.")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormationOFGV1()
        '  Dim strItemCode, head2 As String

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
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtItem.txtValue.Text = ""
        'lblItem.Text = ""
        chkBankAll.IsChecked = True
        'chkInvoiceAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Columns.Clear()

    End Sub



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgBank.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgBank.Enabled = True
    End Sub

    Private Sub chkIemAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = True
    End Sub

    Private Sub frmCheckDepositPaySlip_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
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
                clsCommon.MyExportToExcel("Cheque Deposit Slip/Pay-in-Slipt", gv1, Nothing, Me.Text)

            Else
                clsCommon.MyExportToPDF("Cheque Deposit Slip/Pay-in-Slip", gv1, Nothing, "Cheque Deposit Slip/Pay-in-Slip", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgInvoice.Enabled = Not chkInvoiceAll.IsChecked
    End Sub

    Private Sub chkmrpall_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)


    End Sub

    Private Sub chkLocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBankAll.ToggleStateChanged
        cbgBank.Enabled = Not chkBankAll.IsChecked

    End Sub

    Sub RefereshData()

        Try
            Dim strQuery As String = ""

            If (chkBankSelect.IsChecked = True AndAlso cbgBank.CheckedValue.Count < 1) Then
                Throw New Exception("Please Select Atleast Single Bank")
            End If
            Dim dt As New DataTable
            ' Dim dtFinal As DataTable

            strQuery = "SELECT RH.Receipt_No,CustMaster.Customer_Name 'Customer Name', RH.Cheque_No 'Cheque No',RH.Cheque_Date 'Cheque Date', " & _
                       "RH.Cheque_From 'From Bank',RH.From_Branch 'From Branch', RH.Receipt_Amount 'Amount',BM.DESCRIPTION+' , Account No: '+CONVERT(Varchar,BM.BANKACCNUMBER) as Bank_Name, " & _
                       " BM.ADD1,BM.ADD2, BM.BANKACCNUMBER 'Bank Account', convert(varchar(15),RH.Receipt_Date, 103) 'Receipt Date'  " & _
                       "FROM TSPL_RECEIPT_HEADER RH " & _
                       "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CustMaster ON CustMaster.Cust_Code= RH.Cust_Code " & _
                       "LEFT OUTER JOIN TSPL_BANK_MASTER BM ON BM.BANK_CODE=RH.Bank_Code " & _
                       "LEFT OUTER JOIN TSPL_COMPANY_MASTER CM on CM.Comp_Code= RH.Comp_Code " & _
                       "WHERE  IsChkReverse='N'  and Receipt_Type not in ('F')   " & _
                       "AND Receipt_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Text)), "dd/MMM/yyyy") + "' " & _
                       "AND Receipt_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Text)), "dd/MMM/yyyy") + "' "

            If (chkBankSelect.IsChecked = True And cbgBank.CheckedValue.Count > 0) Then
                strQuery += "AND BM.BANK_CODE in (" + clsCommon.GetMulcallString(cbgBank.CheckedValue) + ") "
            End If
            dt = clsDBFuncationality.GetDataTable(strQuery)

            'dtFinal = dt.Copy()
            'If dt.Rows.Count <> 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        Dim drFianl As DataRow = dtFinal.NewRow()
            '        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            '        drFianl("Customer Name") = dr("Customer Name")
            '        drFianl("Cheque No") = dr("Cheque No")
            '        drFianl("Cheque Date") = dr("Cheque Date")
            '        drFianl("From Bank") = dr("From Bank")
            '        drFianl("From Branch") = dr("From Branch")
            '        drFianl("Amount") = dr("Amount")

            '        dtFinal.Rows.Add(drFianl)

            '    Next
            'End If
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True

            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                SetGridFormatOFGV1()

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
            'End If
            'If dt.Rows.Count > 0 Then
            '    NewSalesReportViewer.funreport(dt, "rptPaySlip", "Cheque Deposit Slip/Pay-in-Slip")
            'Else
            '    Throw New Exception("No data found.")
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormatOFGV1()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Receipt_No").IsVisible = False
        gv1.Columns("Receipt_No").Width = 200
        gv1.Columns("Receipt_No").HeaderText = "Receipt No"


        gv1.Columns("Customer Name").IsVisible = True
        gv1.Columns("Customer Name").Width = 200
        gv1.Columns("Customer Name").HeaderText = "Customer Name"

        gv1.Columns("Cheque No").IsVisible = True
        gv1.Columns("Cheque No").Width = 70
        gv1.Columns("Cheque No").HeaderText = "Cheque No"

        gv1.Columns("Cheque Date").IsVisible = True
        gv1.Columns("Cheque Date").Width = 100
        gv1.Columns("Cheque Date").HeaderText = "Cheque Date"

        gv1.Columns("From Bank").IsVisible = True
        gv1.Columns("From Bank").Width = 100
        gv1.Columns("From Bank").HeaderText = "From Bank"

        gv1.Columns("From Branch").IsVisible = True
        gv1.Columns("From Branch").Width = 100
        gv1.Columns("From Branch").HeaderText = "From Branch"

        gv1.Columns("Amount").IsVisible = True
        gv1.Columns("Amount").Width = 120
        gv1.Columns("Amount").HeaderText = "Amount"

        gv1.Columns("Bank_Name").IsVisible = False
        gv1.Columns("Bank_Name").Width = 120
        gv1.Columns("Bank_Name").HeaderText = "Bank Name"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item5 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        gv1.GroupDescriptors.Add(New GridGroupByExpression("Bank_Name as Item format ""{0}: {1}"" Group By Bank_Name"))


        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub


    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        RefereshData()
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If clsCommon.myLen(gv1.CurrentRow.Cells("Receipt_No").Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gv1.CurrentRow.Cells("Receipt_No").Value)

        End If

    End Sub
End Class
