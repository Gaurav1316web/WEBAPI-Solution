Imports common
Public Class FrmBoothCountReport


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            Dim Whr As String = ""
            Dim dt As DataTable = Nothing
            'Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            'Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = ""
            Dim dt1 As DataTable = Nothing
            Dim strqry As String = ""
            Dim whrh As String = ""
            Dim fromdate As Date = txtfDate.Value.AddDays(1)
            Dim todate As Date = txtToDate.Value.AddDays(-1)
            If BtnDateWise.IsChecked Then
                GetReportGridID()
                'qry = "SELECT FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS Document_Date, 
                '               COUNT(*) AS CountOfDocument 
                '        FROM TSPL_DEMAND_BOOKING_DETAIL 
                '        LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER 
                '        ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                '        where (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + txtfDate.Value + "',103) AND TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning') 
                '        Or (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) AND TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening') AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1  
                '        GROUP BY FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy')"

                qry = " SELECT FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS [Document Date], 
                               COUNT(distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) AS [Count Of Booth] 
                        FROM TSPL_DEMAND_BOOKING_DETAIL 
                        LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER 
                        ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No  
                        WHERE (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) BETWEEN convert(date,'" + txtfDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))
                         AND 
                        (TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'EVENING' OR TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'MORNING')
                        GROUP BY FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy'),
						CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) 
						ORDER BY CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)"

            ElseIf BtnDateGroupWise.IsChecked Then
                qry = " SELECT FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS [Document Date],Max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Cust Group Code],  
                               COUNT(distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) AS [Count Of Booth] 
                        FROM TSPL_DEMAND_BOOKING_DETAIL 
                        LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER 
                        ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        LEFT JOIN TSPL_CUSTOMER_MASTER 
						ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                        WHERE (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) BETWEEN convert(date,'" + txtfDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))
                         AND 
                        (TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'EVENING' OR TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'MORNING')
                        GROUP BY FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy'),
						CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) 
						ORDER BY CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) "
            ElseIf BtnBoothDetail.IsChecked Then
                qry = "SELECT DISTINCT 
                        FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date,'dd/MM/yyyy') AS [Document Date],
                        TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],
                        TSPL_CUSTOMER_MASTER.Customer_Name AS [Booth Name],
                        TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                        TSPL_ROUTE_MASTER.Route_No AS [Route No],
                        TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name] 
                    FROM TSPL_DEMAND_BOOKING_DETAIL 
                    LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER 
                    ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER 
                    ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER 
                    ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    WHERE (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) BETWEEN convert(date,'" + txtfDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))
                         AND 
                        (TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'EVENING' OR TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'MORNING') AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1 ORDER BY [Document Date] "

            End If
            'qry += " ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt
                'SetGridFormationgvData()
                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = True
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                gvData.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Booth Count Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Booth Count Report", gvData, arrHeader, "Booth Count Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub FrmBoothCountReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub rmenuPDF_Click_1(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Sub GetReportGridID()
        Dim VarID As String = ""
        If BtnDateWise.IsChecked Then
            VarID += "_DAWI"
        ElseIf BtnDateGroupWise.IsChecked Then
            VarID += "_DGW"
        ElseIf BtnBoothDetail.IsChecked Then
            VarID += "_BD"
        End If
        gvData.VarID = VarID
    End Sub

End Class
