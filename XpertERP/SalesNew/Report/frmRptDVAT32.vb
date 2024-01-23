''''' bug no BM00000000229 , BM00000000548 ,BM00000000540,BM00000000617
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Public Class frmRptDVAT32
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDVAT32)
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
        'chkLocAll.IsChecked = True
        LoadLocation()
        'chkLocAll.IsChecked = True
        rdbDVAT32.IsChecked = True
        chkInvoiceAll.IsChecked = True
        LoadCustomer()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadInvoice()
        chkCustAll.IsChecked = True

    End Sub

    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Location"
        'cbgLocation.DisplayMember = "Location Description"
    End Sub

    Sub LoadInvoice()
        Dim qry As String = " select Document_Code 'Invoice No', Document_Date 'Invoice Date', Customer_Code 'Customer Code',Bill_To_Location 'Location Code', Description from TSPL_SD_SALE_INVOICE_HEAD " & _
                            " where convert(date,Document_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) " & _
                            " and convert(date,Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
        cbgInvoice.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgInvoice.ValueMember = "Invoice No"
        cbgInvoice.DisplayMember = "Invoice No"
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Try
            Dim StrQuery As String
            If chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Customer")
            End If
            If chkInvoiceSelect.IsChecked AndAlso cbgInvoice.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Invoice")
            End If


            StrQuery = "select LM1.Location_Desc 'From Location', LM2.Location_Desc 'Ship Location',  '" & objCommonVar.CurrentUser & "' as User_Name ,convert(varchar(15), CURRENT_TIMESTAMP, 103) 'Current Date'," &
                       "(case when isnull(LM1.Add1,'')<>'' then LM1.Add1+', ' else '' end + case when isnull(LM1.Add2,'')<>'' then LM1.Add2+', ' else '' end + " &
                       "case when isnull(LM1.Add3,'')<>'' then LM1.Add3+', ' else '' end  + case when isnull(LM1.Add4,'')<>'' then LM1.Add4 else '' end + " &
                       "case when isnull(LM1.City_Code,'')<>'' then LM1.City_Code+', ' else '' end + case when isnull(SM1.STATE_NAME,'')<>'' then SM1.STATE_NAME else '' end ) as 'From Address', " &
                       "LM1.Tin_No 'From Tin No',LM1.CST_No 'From CST No',(case when isnull(SIHead.Ship_To_Location,'')<> '' then " &
                       "(case when isnull(LM2.Add1,'')<>'' then LM2.Add1+', ' else '' end + case when isnull(LM2.Add2,'')<>'' then LM2.Add2+', ' else '' end + " &
                       "case when isnull(LM2.Add3,'')<>'' then LM2.Add3+', ' else '' end +case when isnull(LM2.Add4,'')<>'' then LM2.Add4 else '' end  + " &
                       "case when isnull(LM2.City_Code,'')<>'' then LM2.City_Code+', ' else '' end + case when isnull(SM2.STATE_NAME,'')<>'' then SM2.STATE_NAME else '' end ) else (case when isnull(CM.Add1,'')<>'' then CM.Add1+', ' else '' end + case when isnull(CM.Add2,'')<>'' then CM.Add2+', ' else '' end + " &
                       "case when isnull(CM.Add3,'')<>'' then CM.Add3 else '' end ) end) as 'Shipping Address', " &
                       "CM.Customer_Name 'Customer Name',CM.Tin_No 'Customer TIN No', CM.CST 'Customer CST No', SIDetail.Qty 'Quantity' ,SIDetail.Unit_code 'Packing',  SIDetail.Amount 'Invoice Amt', " &
                       "SIDetail.TotalItem_Weight 'Weight', SIDetail.ActualRate 'Rate', SIDetail.Remarks 'Remarks', " &
                       "(case when AC1.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC2.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt2,0) else 0 end + " &
                       "case when AC3.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt3,0) else 0 end + " &
                       "case when AC4.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC5.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC6.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC7.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC8.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC9.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end + " &
                       "case when AC10.FreightCharges='Y' then ISNULL( SIHead.Add_Charge_Amt1,0) else 0 end  " &
                       ") as TotalFreight, CompanyMaster.Comp_Name 'Company Name',  SIDetail.Item_Net_Amt 'Item Net Amt', IM.Item_Desc 'Item Description', VH.Vehicle_No 'Vehicle No' " &
                       "from TSPL_SD_SALE_INVOICE_DETAIL SIDetail " &
                       "LEFT OUTER JOIN TSPL_ITEM_MASTER IM ON IM.Item_Code = SIDetail.Item_Code " &
                       "LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD SIHead on SIHead.Document_Code= SIDetail.DOCUMENT_CODE " &
                       "LEFT OUTER JOIN TSPL_COMPANY_MASTER CompanyMaster on CompanyMaster.Comp_Code = SIHead.Comp_Code " &
                       "LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM on CM.Cust_Code= SIHead.Customer_Code " &
                       "LEFT OUTER JOIN TSPL_LOCATION_MASTER LM1 on LM1.Location_Code = SIHead.Bill_To_Location " &
                       "LEFT OUTER JOIN TSPL_LOCATION_MASTER LM2 on LM2.Location_Code = SIHead.Ship_To_Location " &
                       "LEFT OUTER JOIN TSPL_STATE_MASTER SM1 on SM1.STATE_CODE= LM1.State " &
                       "LEFT OUTER JOIN TSPL_STATE_MASTER SM2 on SM2.STATE_CODE= LM2.State " &
                       "LEFT OUTER JOIN TSPL_VEHICLE_MASTER VH ON VH.Vehicle_Id= SIHead.Vehicle_Code " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC1 ON AC1.Code = SIHead.Add_Charge_Code1  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC2 ON AC2.Code = SIHead.Add_Charge_Code2  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC3 ON AC3.Code = SIHead.Add_Charge_Code3  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC4 ON AC4.Code = SIHead.Add_Charge_Code4  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC5 ON AC5.Code = SIHead.Add_Charge_Code5  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC6 ON AC6.Code = SIHead.Add_Charge_Code6  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC7 ON AC7.Code = SIHead.Add_Charge_Code7  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC8 ON AC8.Code = SIHead.Add_Charge_Code8  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC9 ON AC9.Code = SIHead.Add_Charge_Code9  " &
                       "LEFT OUTER JOIN TSPL_Additional_Charges AC10 ON AC10.Code = SIHead.Add_Charge_Code10  " &
                       "where 1 = 1 "

            If (chkInvoiceSelect.IsChecked AndAlso cbgInvoice.CheckedValue.Count > 0) Then
                StrQuery += " and SIHead.Document_Code in ( " + clsCommon.GetMulcallString(cbgInvoice.CheckedValue) + ")  "
            End If

            If (chkCustSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0) Then
                StrQuery += " and SIHead.Customer_Code IN  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If

            StrQuery += " and convert(date,SIHead.Document_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,SIHead.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"


            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(StrQuery)

            'gv1.DataSource = Nothing
            'gv1.Columns.Clear()
            'gv1.Rows.Clear()
            'gv1.GroupDescriptors.Clear()
            'gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1.EnableFiltering = True

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

            Else
                'gv1.DataSource = dt
                'SetGridFormationOFGV1()
                Dim frmCRV As New frmCrystalReportViewer()
                If rdbDVAT32.IsChecked = True Then
                    frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptDVAT32", "DVAT 32")
                Else
                    frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptDVAT33", "DVAT 33")
                End If
                frmCRV = Nothing
            End If

            'gv1.MasterTemplate.AllowAddNewRow = False
            'RadPageView1.SelectedPage = RadPageViewPage2

            'If dt.Rows.Count > 0 Then
            '    If rdbDVAT32.IsChecked = True Then
            '        NewSalesReportViewer.funreport(dt, "crptDVAT32", "DVAT 32")
            '    Else
            '        NewSalesReportViewer.funreport(dt, "crptDVAT33", "DVAT 33")
            '    End If

            'Else
            '    Throw New Exception("No data found.")
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Date of Sale").IsVisible = True
        gv1.Columns("Date of Sale").Width = 100
        gv1.Columns("Date of Sale").HeaderText = "Date of Sale"

        gv1.Columns("Item Code").IsVisible = True
        gv1.Columns("Item Code").Width = 150
        gv1.Columns("Item Code").HeaderText = "Item Code"

        gv1.Columns("Item Description").IsVisible = True
        gv1.Columns("Item Description").Width = 200
        gv1.Columns("Item Description").HeaderText = "Item Description"


        gv1.Columns("Qty").IsVisible = True
        gv1.Columns("Qty").Width = 150
        gv1.Columns("Qty").HeaderText = "Quantity"

        gv1.Columns("Item Cost").IsVisible = True
        gv1.Columns("Item Cost").Width = 150
        gv1.Columns("Item Cost").HeaderText = "Price"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Item Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtItem.txtValue.Text = ""
        'lblItem.Text = ""
        'chkLocAll.IsChecked = True
        chkInvoiceAll.IsChecked = True
        chkCustAll.IsChecked = True

    End Sub



    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgLocation.Enabled = True
    End Sub

    Private Sub chkIemAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgInvoice.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgInvoice.Enabled = True
    End Sub

    Private Sub frmRptDVAT32_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkInvoiceSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgInvoice.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If

            'If chkLocSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Location : " + strtemp)
            'End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Inventory Movement Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Inventory Movement Report", gv1, arrHeader, "Inventory Movement Report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInvoiceAll.ToggleStateChanged
        cbgInvoice.Enabled = Not chkInvoiceAll.IsChecked
    End Sub

    Private Sub chkmrpall_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked

    End Sub

    Private Sub chkLocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbgLocation.Enabled = Not chkLocAll.IsChecked

    End Sub


    Private Sub txtFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFromDate.ValueChanged
        Dim qry As String = " select Document_Code 'Invoice No', Document_Date 'Invoice Date', Customer_Code 'Customer Code', " & _
                            " Bill_To_Location 'Location Code', Description from TSPL_SD_SALE_INVOICE_HEAD " & _
                            " where convert(date,Document_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) " & _
                            " and convert(date,Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
        cbgInvoice.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgInvoice.ValueMember = "Invoice No"
        cbgInvoice.DisplayMember = "Invoice No"
    End Sub

    Private Sub txtToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtToDate.ValueChanged
        Dim qry As String = " select Document_Code 'Invoice No', Document_Date 'Invoice Date', Customer_Code 'Customer Code', " & _
                            " Bill_To_Location 'Location Code', Description from TSPL_SD_SALE_INVOICE_HEAD " & _
                            " where convert(date,Document_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) " & _
                            " and convert(date,Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
        cbgInvoice.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgInvoice.ValueMember = "Invoice No"
        cbgInvoice.DisplayMember = "Invoice No"
    End Sub

    Private Sub cbgLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
