Imports common
Public Class rptDCSCredit

    Inherits FrmMainTranScreen
    Dim isLoad As Boolean = False
    Private Sub rptDCSCredit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        funreset()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Sub funreset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        btngo.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub SetToDateNew()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = txtFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                txtFromDate.Value = today.AddDays(-dayDiff)
                txtToDate.Value = txtFromDate.Value.AddDays(6)
            End If
        End If
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        SetToDateNew()
    End Sub

    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDateNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub griddata()
        Try
            Dim qry As String = " SELECT MAX(FINAL.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,final.VLC_Code_VLC_Uploader,MAX(final.Vendor_CODE) AS Vendor_CODE,MAX(final.Vendor_NAME) AS Vendor_NAME,max(final.Ded_Desc)Ded_Desc,
	                              sum(final.Amount)Amount,SUM(final.Reduce_Deduc_Amt) AS Reduce_Deduc_Amt,SUM(CASE WHEN final.Ded_Desc = 'SHARE CAPITAL' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS Share_Capital,
                                  SUM(CASE WHEN final.Ded_Desc = 'ADVANCE' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS Advance,
	                              SUM(CASE WHEN final.ManAddDed = 1 AND final.Ded_Desc <> 'ADVANCE' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS All_Amount,
                                  MAX(final.ManAddDed) AS ManAddDed FROM (SELECT TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE AS Vendor_CODE,
                                  TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME AS Vendor_NAME,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code AS Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc AS Ded_Desc,
                                  TSPL_PAYMENT_PROCESS_DEDUCTION.Amount AS Amount,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt AS Reduce_Deduc_Amt,CASE WHEN TSPL_MULTIPLE_DEDUCTION_head.Document_No IS NOT NULL THEN 1 ELSE 0 END AS ManAddDed,
                                  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader FROM TSPL_PAYMENT_PROCESS_DEDUCTION 
                        LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        LEFT OUTER JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_no = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_no
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_head ON TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC
                        WHERE TRY_CONVERT(DATE, TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) 
                        AND TRY_CONVERT(DATE, TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) ) final 
                        WHERE  final.ManAddDed = 1 OR final.Ded_Desc IN ('SHARE CAPITAL', 'ADVANCE') 
                        GROUP BY final.VLC_Code_VLC_Uploader,final.Ded_Code,final.Ded_Desc ORDER BY CAST(final.VLC_Code_VLC_Uploader AS INT) "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
            gv1.Columns(ii).VisibleInColumnChooser = False
        Next

        gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "Route Code"
        gv1.Columns("Mcc_Code_VLC_Uploader").Width = 100
        gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = True

        gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
        gv1.Columns("VLC_Code_VLC_Uploader").Width = 100
        gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True

        gv1.Columns("Vendor_CODE").HeaderText = "Vendor Code"
        gv1.Columns("Vendor_CODE").Width = 150
        gv1.Columns("Vendor_CODE").IsVisible = True

        gv1.Columns("Vendor_NAME").HeaderText = "Vendor NAME"
        gv1.Columns("Vendor_NAME").Width = 200
        gv1.Columns("Vendor_NAME").IsVisible = True

        gv1.Columns("Ded_Desc").HeaderText = "Deduction Description"
        gv1.Columns("Ded_Desc").Width = 200
        gv1.Columns("Ded_Desc").IsVisible = True

        gv1.Columns("Amount").HeaderText = "Amount"
        gv1.Columns("Amount").Width = 200
        gv1.Columns("Amount").IsVisible = False
        gv1.Columns("Amount").VisibleInColumnChooser = True

        gv1.Columns("Reduce_Deduc_Amt").HeaderText = "Reduce Deduc Amt"
        gv1.Columns("Reduce_Deduc_Amt").Width = 200
        gv1.Columns("Reduce_Deduc_Amt").IsVisible = False
        gv1.Columns("Reduce_Deduc_Amt").VisibleInColumnChooser = True

        gv1.Columns("ManAddDed").HeaderText = "ManAddDed"
        gv1.Columns("ManAddDed").Width = 200
        gv1.Columns("ManAddDed").IsVisible = False
        gv1.Columns("ManAddDed").VisibleInColumnChooser = True

        gv1.Columns("Share_Capital").HeaderText = "Share Capital"
        gv1.Columns("Share_Capital").Width = 200
        gv1.Columns("Share_Capital").IsVisible = True

        gv1.Columns("Advance").HeaderText = "Advance"
        gv1.Columns("Advance").Width = 250
        gv1.Columns("Advance").IsVisible = True

        gv1.Columns("All_Amount").HeaderText = "GHEE+FEED+SEED+...."
        gv1.Columns("All_Amount").Width = 250
        gv1.Columns("All_Amount").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Share_Capital", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Advance", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("All_Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        griddata()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = "   SELECT '" + txtFromDate.Value + "' as Fromdate,'" + txtToDate.Value + "' as ToDate,'" + objCommonVar.CurrentUser + "' as username, (FINAL.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,final.VLC_Code_VLC_Uploader,MAX(final.Vendor_CODE) AS Vendor_CODE,MAX(final.Vendor_NAME) AS Vendor_NAME,max(final.Ded_Desc)Ded_Desc,
	                              sum(final.Amount)Amount,SUM(final.Reduce_Deduc_Amt) AS Reduce_Deduc_Amt,SUM(CASE WHEN final.Ded_Desc = 'SHARE CAPITAL' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS Share_Capital,
                                  SUM(CASE WHEN final.Ded_Desc = 'ADVANCE' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS Advance,
	                              SUM(CASE WHEN final.ManAddDed = 1 AND final.Ded_Desc <> 'ADVANCE' THEN (final.Amount-final.Reduce_Deduc_Amt) ELSE 0 END) AS All_Amount,
                                  MAX(final.ManAddDed) AS ManAddDed,MAX(final.Comp_Name)Comp_Name FROM (SELECT TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE AS Vendor_CODE,
                                  TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME AS Vendor_NAME,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code AS Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc AS Ded_Desc,
                                  TSPL_PAYMENT_PROCESS_DEDUCTION.Amount AS Amount,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt AS Reduce_Deduc_Amt,CASE WHEN TSPL_MULTIPLE_DEDUCTION_head.Document_No IS NOT NULL THEN 1 ELSE 0 END AS ManAddDed,
                                  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_COMPANY_MASTER.Comp_Name FROM TSPL_PAYMENT_PROCESS_DEDUCTION 
                        LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        LEFT OUTER JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_no = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_no
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_head ON TSPL_MULTIPLE_DEDUCTION_head.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC
                        left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VLC_MASTER_HEAD.comp_code
                        WHERE TRY_CONVERT(DATE, TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date, 103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) 
                        AND TRY_CONVERT(DATE, TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_Date, 103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) ) final 
                        WHERE  final.ManAddDed = 1 OR final.Ded_Desc IN ('SHARE CAPITAL', 'ADVANCE') 
                        GROUP BY final.Mcc_Code_VLC_Uploader,final.VLC_Code_VLC_Uploader ORDER BY CAST(final.Mcc_Code_VLC_Uploader AS INT)  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptDcsCredit", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDCSCredit & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class