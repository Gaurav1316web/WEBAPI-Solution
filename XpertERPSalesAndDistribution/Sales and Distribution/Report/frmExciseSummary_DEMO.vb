Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports XpertERPEngine
'--Ticket No-[BM00000000586]-Updation By Pankaj Chaudhary

Public Class FrmExciseSummary_DEMO
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ExportToExcel As Boolean = False

    Private Sub FrmExciseSummary_DEMO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmExciseSummary_DEMO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtStart.Value = System.DateTime.Now()
        dtEnd.Value = System.DateTime.Now()
        LoadLocation()
        chkLocAll.IsChecked = True
        btnExport.Visible = False
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ExciseSummary1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        print(False)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(True)
    End Sub

    Sub print(ByVal IsPrint As Boolean)
        Try
            Dim strReportName As String = Nothing
            Dim startDate As String = clsCommon.GetPrintDate(dtStart.Value, "dd/MMM/yyyy hh:mm tt")
            Dim EndDate As String = clsCommon.GetPrintDate(dtEnd.Value, "dd/MMM/yyyy hh:mm tt")
            Dim startTime As String = dtStart.Value.ToString("hh:mm tt")
            Dim EndTime As String = dtEnd.Value.ToString("hh:mm tt")
            Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")

            If chkLocSelect.IsChecked Then
                If cbgLocation.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location Code.", Me.Text)
                    Return
                End If
            End If
            Dim LocFilter As String
            If cbgLocation.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If
            '---------------------------------------------Finished Goods------------------------------------
            Dim StrQry As String = "SELECT '" + clsCommon.GetPrintDate(startDate, "dd/MM/yyyy") + "' AS StartDate, '" + clsCommon.GetPrintDate(EndDate, "dd/MM/yyyy") + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], TSPL_SD_SALE_INVOICE_HEAD.Document_Code AS Sale_Invoice_No, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as  Sale_Invoice_Date, "
            StrQry += " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Qty AS Qty, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Base_Amt as Total_Assessable_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate,   TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt, "
            StrQry += " TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate,  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt,  TSPL_SD_SALE_INVOICE_HEAD.Document_Date, "
            StrQry += " TSPL_SD_SALE_INVOICE_DETAIL.MRP as MRP_Amt,  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location  "
            StrQry += " FROM TSPL_SD_SALE_INVOICE_HEAD "
            StrQry += " Left Outer JOIN  TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
            StrQry += " Left Outer JOIN  TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER.Location_Code"
            StrQry += " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code"
            StrQry += " where (Document_Date >= '" + startDate + "') AND (Document_Date <= '" + EndDate + "') AND TSPL_SD_SALE_INVOICE_HEAD.Status =1 AND TSPL_LOCATION_MASTER.Excisable='T' and 'E'=( select Type  from TSPL_TAX_MASTER where Tax_Code =TSPL_SD_SALE_INVOICE_HEAD.TAX1  )"
            '------------------------------------------------------------------------------------------------
            If chkLocSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
                StrQry += " AND TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If

            If rbtnSummary.IsChecked Then
                StrQry = "SELECT MAX(StartDate ) AS StartDate, MAX([End Date]) AS EndDate, MAX([Start Time]) AS [Start Time], " & _
            " MAX([End Time]) AS [End Time], '" + RunDate + "' AS RunDate, Sale_Invoice_No, MAX(Sale_Invoice_Date) as Sale_Invoice_Date, '' as Cheapter_Heads, SUM(Qty) as Qty, " & _
            " SUM(TAX1_Amt) as Total_Assessable_Amt, " & _
            " SUM([TAX1_Amt]) as BED, SUM(TAX2_Amt) as ECess, SUM(TAX3_Amt) as HCess, " & _
            " (SUM(TAX2_Amt)+SUM(TAX3_Amt)) [TotalCess], (SUM(TAX1_Amt)+SUM(TAX2_Amt)+SUM(TAX3_Amt)) as TotalTax" & _
            " from ( " + StrQry + " ) XXX  GROUP BY Sale_Invoice_No "
            End If
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(StrQry)
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            FormatGrid()
            If IsPrint Then
                If rbtnSummary.IsChecked = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptExciseSummary", "Excise Report")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptExciseDetails", "Excise Report")
                End If
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        For Each col As GridViewColumn In gv1.Columns
            col.IsVisible = False
        Next
        If rbtnSummary.IsChecked Then

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").HeaderText = "INVOICE NO"
            gv1.Columns("Sale_Invoice_No").Width = 100

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "QUANTITY"
            gv1.Columns("Qty").Width = 80

            gv1.Columns("BED").IsVisible = True
            gv1.Columns("BED").HeaderText = "B.E.D."
            gv1.Columns("BED").Width = 110

            gv1.Columns("ECess").IsVisible = True
            gv1.Columns("ECess").HeaderText = "EDN CESSS"
            gv1.Columns("ECess").Width = 110

            gv1.Columns("HCess").IsVisible = True
            gv1.Columns("HCess").HeaderText = "SH.ED CESS"
            gv1.Columns("HCess").Width = 110

            gv1.Columns("TotalCess").IsVisible = True
            gv1.Columns("TotalCess").HeaderText = "TOTAL CESS"
            gv1.Columns("TotalCess").Width = 150

            gv1.Columns("TotalTax").IsVisible = True
            gv1.Columns("TotalTax").HeaderText = "G.TOTAL"
            gv1.Columns("TotalTax").Width = 170

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)
            Dim SumBed As New GridViewSummaryItem("BED", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumBed)
            Dim SumEcess As New GridViewSummaryItem("ECess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumEcess)
            Dim SumHCess As New GridViewSummaryItem("HCess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumHCess)
            Dim SumTotalCess As New GridViewSummaryItem("TotalCess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotalCess)
            Dim SumTotalTax As New GridViewSummaryItem("TotalTax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotalTax)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"
            gv1.Columns("Sale_Invoice_No").Width = 100

            gv1.Columns("Sale_Invoice_Date").IsVisible = True
            gv1.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"
            gv1.Columns("Sale_Invoice_Date").Width = 150

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").HeaderText = "Item"
            gv1.Columns("Item_Desc").Width = 200

            gv1.Columns("MRP_Amt").IsVisible = True
            gv1.Columns("MRP_Amt").HeaderText = "MRP"
            gv1.Columns("MRP_Amt").Width = 80

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "QUANTITY"
            gv1.Columns("Qty").Width = 80

            gv1.Columns("TAX1_Amt").IsVisible = True
            gv1.Columns("TAX1_Amt").HeaderText = "B.E.D."
            gv1.Columns("TAX1_Amt").Width = 100

            gv1.Columns("TAX2_Amt").IsVisible = True
            gv1.Columns("TAX2_Amt").HeaderText = "EDN CESSS"
            gv1.Columns("TAX2_Amt").Width = 100

            gv1.Columns("TAX3_Amt").IsVisible = True
            gv1.Columns("TAX3_Amt").HeaderText = "SH.ED CESS"
            gv1.Columns("TAX3_Amt").Width = 100

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)
            Dim SumAmount As New GridViewSummaryItem("Total_Assessable_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmount)
            Dim SumBed As New GridViewSummaryItem("TAX1_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumBed)
            Dim SumEcess As New GridViewSummaryItem("TAX2_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumEcess)
            Dim SumHCess As New GridViewSummaryItem("TAX3_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumHCess)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' and Excisable='T' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToExcel = True
            print(False)
            ExportToExcel = False
            If gv1.DataSource Is Nothing OrElse gv1.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)

            arrHeader.Add("EXCISE DUTY CALCULATION  " + clsCommon.GETSERVERDATE() + " ")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtStart.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MMM/yyyy") + " ")
            arrHeader.Add("From Time : " + clsCommon.GetPrintDate(dtStart.Value, "hh:mm tt") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "hh:mm tt") + " ")
            clsCommon.MyExportToExcel("Excise Summary", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub ShowButtons()
        btnPrint.Visible = True
        btnExport.Visible = True
    End Sub

End Class
