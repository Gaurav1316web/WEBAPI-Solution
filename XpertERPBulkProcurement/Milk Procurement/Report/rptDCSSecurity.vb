Imports common
Public Class rptDCSSecurity
    Inherits FrmMainTranScreen
    Dim isLoad As Boolean = False

    Private Sub rptDCSSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = "select '" + txtFromDate.Value + "' as Fromdate,'" + txtToDate.Value + "' as ToDate,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name
                                    ,CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                    cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                     when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                    cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                    else 0 end AS [Base Amount/Quantity] ,TSPL_COMPANY_MASTER.Comp_Name  ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount]                                   
                                     from TSPL_VENDOR_INVOICE_DETAIL
                                    LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                    left outer join TSPL_MCC_MASTER ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_Code
                                    left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VLC_MASTER_HEAD.comp_code
                                    WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'  and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=1 and  Applicable_DCS_Type=2 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptDcsSecurity", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub griddata()
        Try
            Dim qry As String = "select   row_number() over(order by(select 1)) as SNo, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name
                                    ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount]                                   
                                     from TSPL_VENDOR_INVOICE_DETAIL
                                    LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                    left outer join TSPL_MCC_MASTER ON TSPL_VLC_MASTER_HEAD.MCC=TSPL_MCC_MASTER.MCC_Code
                                    WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'  and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=1 and  Applicable_DCS_Type=2 "
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
        Next
        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = True

        gv1.Columns("DCS Code").HeaderText = "DCS Code"
        gv1.Columns("DCS Code").Width = 250
        gv1.Columns("DCS Code").IsVisible = True
        gv1.Columns("VLC_Name").FormatString = "{0:n2}"

        gv1.Columns("VLC_Name").HeaderText = "DCS Name"
        gv1.Columns("VLC_Name").Width = 500


        gv1.Columns("Addition/Deduction Amount").HeaderText = "Amount"
        gv1.Columns("Addition/Deduction Amount").Width = 250
        gv1.Columns("Addition/Deduction Amount").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Addition/Deduction Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        griddata()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class