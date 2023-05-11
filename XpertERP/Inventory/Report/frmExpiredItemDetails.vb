'-Updation by--[Pankaj Kumar Chaudhary] - Against Ticket No--[BM00000001621]
Imports common
Public Class FrmExpiredItemDetails
    Inherits FrmMainTranScreen
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master WHERE 2=2 "
        cbgCust.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCust.ValueMember = "Customer Code"
        cbgCust.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select distinct Loc_Segment_Code as Location,Description as [Location Description] from TSPL_LOCATION_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_LOCATION_MASTER.Loc_Segment_Code=TSPL_GL_SEGMENT_CODE.Segment_code where Location_Type='Physical'"
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmEmptyTransactionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub
    Private Sub FrmExpiredItemDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Reset()
        SetUserMgmtNew()
    End Sub
    Sub Reset()
        LoadCustomer()
        LoadLocation()
        chkLocationAll.IsChecked = True
        chkCustAll.IsChecked = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rdbSummary.IsChecked = True
    End Sub
   

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCust.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkCustSelect.IsChecked = True AndAlso cbgCust.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return
            End If

            Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")

            gv1.EnableFiltering = True
            Dim dt As DataTable
            'Dim strSql As String = "select TSPL_EXPIRY_HEADER.Document_No,Document_Date,Loc_Desc,Customer_NAME,Item_Code, " & _
            '"Item_Description,Item_Quantity,Item_Cost,Amount,MRP,Comments,Remarks,Reference ,Description from " & _
            '"TSPL_EXPIRY_HEADER left outer join TSPL_EXPIRY_DETAIL on TSPL_EXPIRY_HEADER.Document_No=TSPL_EXPIRY_DETAIL.Document_No " & _
            '" where convert(date,Document_Date,103) >='" + strFromDate + "' and convert(date,Document_Date,103) <='" + strToDate + "'"
            'If rdbSummary.IsChecked Then
            '    strSql = "select Document_No,Document_Date,Loc_Desc,Customer_NAME,SUM(Item_Quantity) as Item_Quantity, " & _
            '    "sum(Amount) as Amount,Reference,Description from ( " & strSql & "   ) aa group by Document_No,Document_Date, " & _
            '    "Loc_Desc,Customer_NAME,Reference,Description "
            'End If

            Dim strSql As String
            strSql = "select TSPL_EXPIRY_HEADER.Document_No,Document_Date,TSPL_EXPIRY_HEADER.Description,Voucher_No,Customer_CODE,Customer_NAME,Loc_Code,Loc_Desc, " & _
            "Vehicle_No,TSPL_EXPIRY_DETAIL.Item_Code, TSPL_EXPIRY_DETAIL.Unit_Code, MRP * TSPL_ITEM_UOM_DETAIL.Conversion_Factor as MRPCase, " & _
            "convert(decimal(18,2),MRP/TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor) as MRPBottle,Leakage_Qty, " & _
            "isnull(Leakage_Qty*liquid_rate,0) as LeakageAmt,Breakage_Qty,isnull(Breakage_Qty*liquid_rate,0) as BreakageAmt,Item_Quantity,isnull(Item_Quantity*liquid_rate,0) as ExpiryAmt, " & _
            "(Leakage_Qty+Breakage_Qty+Item_Quantity) as TotQty,isnull(liquid_amount,0) as TotAmt from  " & _
            "TSPL_EXPIRY_HEADER left outer join  TSPL_EXPIRY_DETAIL on TSPL_EXPIRY_HEADER.Document_No=TSPL_EXPIRY_DETAIL.Document_No left outer join  " & _
            "TSPL_JOURNAL_MASTER on TSPL_EXPIRY_HEADER.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No left outer join TSPL_ITEM_UOM_DETAIL on  " & _
            "TSPL_EXPIRY_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_EXPIRY_DETAIL.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on TSPL_EXPIRY_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code and " & _
            "TSPL_ITEM_UOM_DETAIL_1.UOM_Code= " & _
            "(case when TSPL_EXPIRY_DETAIL.unit_code='FC' then 'FB' " & _
            "when TSPL_EXPIRY_DETAIL.unit_code='FB' then 'FC'  " & _
            "when TSPL_EXPIRY_DETAIL.unit_code='EC' then 'EB'  " & _
            "when  TSPL_EXPIRY_DETAIL.unit_code='EB' then 'EC'  end) " & _
            " where convert(date,Document_Date,103) >='" + strFromDate + "' and convert(date,Document_Date,103) <='" + strToDate + "'"

            dt = clsDBFuncationality.GetDataTable(strSql)

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormationOFGV1()

            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)

        End Try


    End Sub
    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Document_No").IsVisible = True
        gv1.Columns("Document_No").Width = 80
        gv1.Columns("Document_No").HeaderText = "Voucher No"

        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").Width = 80
        gv1.Columns("Document_Date").HeaderText = "Date"

        gv1.Columns("Description").IsVisible = True
        gv1.Columns("Description").Width = 80
        gv1.Columns("Description").HeaderText = "Description"

        gv1.Columns("Voucher_No").IsVisible = True
        gv1.Columns("Voucher_No").Width = 80
        gv1.Columns("Voucher_No").HeaderText = "J.V No"

        gv1.Columns("Customer_CODE").IsVisible = True
        gv1.Columns("Customer_CODE").Width = 80
        gv1.Columns("Customer_CODE").HeaderText = "Party Code"

        gv1.Columns("Customer_NAME").IsVisible = True
        gv1.Columns("Customer_NAME").Width = 80
        gv1.Columns("Customer_NAME").HeaderText = "Party Name"

        gv1.Columns("Loc_Code").IsVisible = True
        gv1.Columns("Loc_Code").Width = 80
        gv1.Columns("Loc_Code").HeaderText = "District Code"

        gv1.Columns("Loc_Desc").IsVisible = True
        gv1.Columns("Loc_Desc").Width = 80
        gv1.Columns("Loc_Desc").HeaderText = "District Desc"

        gv1.Columns("Item_Quantity").IsVisible = True
        gv1.Columns("Item_Quantity").Width = 100
        gv1.Columns("Item_Quantity").HeaderText = "Quantity"

        gv1.Columns("Vehicle_No").IsVisible = True
        gv1.Columns("Vehicle_No").Width = 100
        gv1.Columns("Vehicle_No").HeaderText = "Vehicle"

        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").Width = 80
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("Unit_Code").IsVisible = True
        gv1.Columns("Unit_Code").Width = 80
        gv1.Columns("Unit_Code").HeaderText = "Unit Code"

        gv1.Columns("MRPCase").IsVisible = True
        gv1.Columns("MRPCase").Width = 100
        gv1.Columns("MRPCase").HeaderText = "MRPC ase"

        gv1.Columns("MRPBottle").IsVisible = True
        gv1.Columns("MRPBottle").Width = 100
        gv1.Columns("MRPBottle").HeaderText = "MRP Bottle"

        gv1.Columns("Leakage_Qty").IsVisible = True
        gv1.Columns("Leakage_Qty").Width = 100
        gv1.Columns("Leakage_Qty").HeaderText = "Leakage Qty"

        gv1.Columns("LeakageAmt").IsVisible = True
        gv1.Columns("LeakageAmt").Width = 100
        gv1.Columns("LeakageAmt").HeaderText = "Leakage Amt"

        gv1.Columns("Breakage_Qty").IsVisible = True
        gv1.Columns("Breakage_Qty").Width = 100
        gv1.Columns("Breakage_Qty").HeaderText = "Breakage Qty"

        gv1.Columns("BreakageAmt").IsVisible = True
        gv1.Columns("BreakageAmt").Width = 100
        gv1.Columns("BreakageAmt").HeaderText = "Breakage Amt"

        gv1.Columns("Item_Quantity").IsVisible = True
        gv1.Columns("Item_Quantity").Width = 100
        gv1.Columns("Item_Quantity").HeaderText = "Expiry Qty"

        gv1.Columns("ExpiryAmt").IsVisible = True
        gv1.Columns("ExpiryAmt").Width = 100
        gv1.Columns("ExpiryAmt").HeaderText = "Expiry Amt"


        gv1.Columns("TotQty").IsVisible = True
        gv1.Columns("TotQty").Width = 100
        gv1.Columns("TotQty").HeaderText = "Total Qty"

        gv1.Columns("TotAmt").IsVisible = True
        gv1.Columns("TotAmt").Width = 100
        gv1.Columns("TotAmt").HeaderText = "Total Amt"

        

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Leakage_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("LeakageAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Breakage_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("BreakageAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Item_Quantity", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("ExpiryAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("TotQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("TotAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)



        ' ''gv1.Columns("Document_No").IsVisible = True
        ' ''gv1.Columns("Document_No").Width = 80
        ' ''gv1.Columns("Document_No").HeaderText = "Document No"

        ' ''gv1.Columns("Document_Date").IsVisible = True
        ' ''gv1.Columns("Document_Date").Width = 80
        ' ''gv1.Columns("Document_Date").HeaderText = "Document Date"

        ' ''gv1.Columns("Loc_Desc").IsVisible = True
        ' ''gv1.Columns("Loc_Desc").Width = 80
        ' ''gv1.Columns("Loc_Desc").HeaderText = "Location"

        ' ''gv1.Columns("Customer_NAME").IsVisible = True
        ' ''gv1.Columns("Customer_NAME").Width = 80
        ' ''gv1.Columns("Customer_NAME").HeaderText = "Customer"

        ' ''gv1.Columns("Item_Quantity").IsVisible = True
        ' ''gv1.Columns("Item_Quantity").Width = 100
        ' ''gv1.Columns("Item_Quantity").HeaderText = "Quantity"

        ' ''gv1.Columns("Amount").IsVisible = True
        ' ''gv1.Columns("Amount").Width = 100
        ' ''gv1.Columns("Amount").HeaderText = "Amount"

        ' ''If rdbDetail.IsChecked Then
        ' ''    gv1.Columns("Item_Code").IsVisible = True
        ' ''    gv1.Columns("Item_Code").Width = 80
        ' ''    gv1.Columns("Item_Code").HeaderText = "Item Code"

        ' ''    gv1.Columns("Item_Description").IsVisible = True
        ' ''    gv1.Columns("Item_Description").Width = 100
        ' ''    gv1.Columns("Item_Description").HeaderText = "Item Description"

        ' ''    gv1.Columns("Item_Cost").IsVisible = True
        ' ''    gv1.Columns("Item_Cost").Width = 100
        ' ''    gv1.Columns("Item_Cost").HeaderText = "Cost"

        ' ''    gv1.Columns("MRP").IsVisible = True
        ' ''    gv1.Columns("MRP").Width = 100
        ' ''    gv1.Columns("MRP").HeaderText = "MRP"
        ' ''Else
        ' ''    gv1.Columns("Reference").IsVisible = True
        ' ''    gv1.Columns("Reference").Width = 100
        ' ''    gv1.Columns("Reference").HeaderText = "Reference"

        ' ''    gv1.Columns("Description").IsVisible = True
        ' ''    gv1.Columns("Description").Width = 100
        ' ''    gv1.Columns("Description").HeaderText = "Description"
        ' ''End If
        ' ''Dim summaryRowItem As New GridViewSummaryRowItem()
        ' ''Dim intCount As Integer = 0
        ' ''Dim item1 As New GridViewSummaryItem("Item_Quantity", "{0:F2}", GridAggregateFunction.Sum)
        ' ''summaryRowItem.Add(item1)
        ' ''Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        ' ''summaryRowItem.Add(item2)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
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

            If chkCustSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCust.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Expired Item Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Expired Item Report", gv1, arrHeader, "Expired Item Report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
