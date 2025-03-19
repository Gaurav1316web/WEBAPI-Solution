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

            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.route_no In (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ")"
            End If

            If BtnDateWise.IsChecked Then
                GetReportGridID()
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
                qry = " SELECT FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS [Document Date],(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Cust Group Code],  
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
						CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103),Cust_Group_Code 
						ORDER BY CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) "
            ElseIf BtnBoothDetail.IsChecked Then
                qry = "SELECT DISTINCT 
                        FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date,'dd/MM/yyyy') AS [Document Date],
                        TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],
                        TSPL_CUSTOMER_MASTER.Customer_Name AS [Booth Name],
                        TSPL_CUSTOMER_MASTER.Cust_Group_Code as [Cust Group Code],
						TSPL_ROUTE_MASTER.Zone_Code as [Zone],
                        TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                        TSPL_ROUTE_MASTER.Route_No AS [Route No],
                        TSPL_ROUTE_MASTER.Route_Desc AS [Route Name] 
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

            ElseIf BtnRouteWise.IsChecked Then
                qry = "Select [Document Date],Zone,[Route No],MAX([Route Name])[Route Name],SUM([Count Of Booth])[Count Of Booth] from (
                        SELECT Max(TSPL_DEMAND_BOOKING_MASTER.ShiftType)ShiftType,FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS [Document Date],  
                        TSPL_ROUTE_MASTER.Route_No AS [Route No],  
                        Max(TSPL_ROUTE_MASTER.Route_Desc) AS [Route Name],  
                        COUNT(distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) AS [Count Of Booth],
                        TSPL_ROUTE_MASTER.Zone_Code as [Zone]
                        FROM TSPL_DEMAND_BOOKING_DETAIL  
                        LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No  
                        LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No  
                        LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                        WHERE (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) BETWEEN convert(date,'" + txtfDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))
                         AND 
                        (TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'EVENING' OR TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'MORNING') AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " + Whr + " Group BY TSPL_DEMAND_BOOKING_MASTER.Document_Date,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Zone_Code
                        )xyz Group By [Document Date],[Route No],Zone
                        ORDER BY [Document Date],[Route No] "

            ElseIf BtnRouteGroupWise.IsChecked Then
                qry = "Select [Document Date],[Zone],[Route No],MAX([Route Name])[Route Name],[Cust Group Code],SUM([Count Of Booth])[Count Of Booth] from (
                            SELECT FORMAT(TSPL_DEMAND_BOOKING_MASTER.Document_Date, 'dd/MM/yyyy') AS [Document Date],  
                            TSPL_ROUTE_MASTER.Route_No AS [Route No],
	                        (TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Cust Group Code],
                            TSPL_ROUTE_MASTER.Route_Desc AS [Route Name],  
                            COUNT(distinct TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) AS [Count Of Booth],
                            TSPL_ROUTE_MASTER.Zone_Code as [Zone]
                        FROM TSPL_DEMAND_BOOKING_DETAIL  
                        LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER  
                            ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No  
                        LEFT OUTER JOIN TSPL_ROUTE_MASTER  
                            ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No  
                        LEFT JOIN TSPL_CUSTOMER_MASTER  
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  
                        WHERE  (Convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103) BETWEEN convert(date,'" + txtfDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103))
                         AND 
                        (TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'EVENING' OR TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'MORNING') AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " + Whr + " Group BY TSPL_DEMAND_BOOKING_MASTER.Document_Date,  
                        TSPL_ROUTE_MASTER.Route_No,  
                        TSPL_ROUTE_MASTER.Route_Desc,
                        Cust_Group_Code,
                        TSPL_ROUTE_MASTER.Zone_Code)xyz Group By [Document Date],[Route No],[Cust Group Code],Zone
                        ORDER BY [Document Date],[Route No] "

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
                SetGridFormat()
                gvData.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetGridFormat()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        gvData.AutoExpandGroups = True
        gvData.ShowGroupPanel = True
        gvData.ShowRowHeaderColumn = False
        gvData.AllowAddNewRow = False
        gvData.AllowDeleteRow = False
        gvData.EnableFiltering = True
        gvData.ShowFilteringRow = True

        'For ii As Integer = 0 To gvData.Columns.Count - 1
        '    gvData.Columns(ii).ReadOnly = True
        '    gvData.Columns(ii).BestFit()
        'Next
        If BtnDateWise.IsChecked Then

            gvData.Columns("Document Date").Name = "Document Date"
            gvData.Columns("Document Date").Width = 500
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Count Of Booth").HeaderText = "Count Of Booth"
            gvData.Columns("Count Of Booth").Width = 500
            gvData.Columns("Count Of Booth").IsVisible = True

        ElseIf BtnDateGroupWise.IsChecked Then

            gvData.Columns("Document Date").Name = "Document Date"
            gvData.Columns("Document Date").Width = 500
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Cust Group Code").HeaderText = "Cust Group Code"
            gvData.Columns("Cust Group Code").Width = 500
            gvData.Columns("Cust Group Code").IsVisible = True

            gvData.Columns("Count Of Booth").HeaderText = "Count Of Booth"
            gvData.Columns("Count Of Booth").Width = 500
            gvData.Columns("Count Of Booth").IsVisible = True

        ElseIf BtnBoothDetail.IsChecked Then

            gvData.Columns("Document Date").HeaderText = "Document Date"
            gvData.Columns("Document Date").Width = 500
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Booth Code").HeaderText = "Booth Code"
            gvData.Columns("Booth Code").Width = 500
            gvData.Columns("Booth Code").IsVisible = True

            gvData.Columns("Booth Name").HeaderText = "Booth Name"
            gvData.Columns("Booth Name").Width = 500
            gvData.Columns("Booth Name").IsVisible = True

            gvData.Columns("Cust Group Code").HeaderText = "Cust Group Code"
            gvData.Columns("Cust Group Code").Width = 500
            gvData.Columns("Cust Group Code").IsVisible = True

            gvData.Columns("Zone").HeaderText = "Zone"
            gvData.Columns("Zone").Width = 500
            gvData.Columns("Zone").IsVisible = True

            gvData.Columns("Mobile No").HeaderText = "Mobile No"
            gvData.Columns("Mobile No").Width = 500
            gvData.Columns("Mobile No").IsVisible = True

            gvData.Columns("Route No").HeaderText = "Route No"
            gvData.Columns("Route No").Width = 500
            gvData.Columns("Route No").IsVisible = True

            gvData.Columns("Route Name").HeaderText = "Route Name"
            gvData.Columns("Route Name").Width = 500
            gvData.Columns("Route Name").IsVisible = True

        ElseIf BtnRouteWise.IsChecked Then

            gvData.Columns("Document Date").HeaderText = "Document Date"
            gvData.Columns("Document Date").Width = 500
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Zone").HeaderText = "Zone"
            gvData.Columns("Zone").Width = 500
            gvData.Columns("Zone").IsVisible = True

            gvData.Columns("Route No").HeaderText = "Route No"
            gvData.Columns("Route No").Width = 500
            gvData.Columns("Route No").IsVisible = True

            gvData.Columns("Route Name").HeaderText = "Route Name"
            gvData.Columns("Route Name").Width = 500
            gvData.Columns("Route Name").IsVisible = True

            gvData.Columns("Count Of Booth").HeaderText = "Count Of Booth"
            gvData.Columns("Count Of Booth").Width = 500
            gvData.Columns("Count Of Booth").IsVisible = True

        ElseIf BtnRouteGroupWise.IsChecked Then

            gvData.Columns("Document Date").HeaderText = "Document Date"
            gvData.Columns("Document Date").Width = 500
            gvData.Columns("Document Date").IsVisible = True

            gvData.Columns("Zone").HeaderText = "Zone"
            gvData.Columns("Zone").Width = 500
            gvData.Columns("Zone").IsVisible = True

            gvData.Columns("Route No").HeaderText = "Route No"
            gvData.Columns("Route No").Width = 500
            gvData.Columns("Route No").IsVisible = True

            gvData.Columns("Route Name").HeaderText = "Route Name"
            gvData.Columns("Route Name").Width = 500
            gvData.Columns("Route Name").IsVisible = True

            gvData.Columns("Cust Group Code").HeaderText = "Cust Group Code"
            gvData.Columns("Cust Group Code").Width = 500
            gvData.Columns("Cust Group Code").IsVisible = True

            gvData.Columns("Count Of Booth").HeaderText = "Count Of Booth"
            gvData.Columns("Count Of Booth").Width = 500
            gvData.Columns("Count Of Booth").IsVisible = True

            'gvData.ShowGroupPanel = True
            'gvData.MasterTemplate.AutoExpandGroups = True
        End If
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
        TxtMultiRoute.arrValueMember = Nothing
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
        ElseIf BtnRouteWise.IsChecked Then
            VarID += "_RW"
        ElseIf BtnRouteGroupWise.IsChecked Then
            VarID += "_RGW"
        End If
        gvData.VarID = VarID
    End Sub

    Private Sub TxtMultiRoute__My_Click(sender As Object, e As EventArgs) Handles TxtMultiRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtMultiRoute.arrValueMember, TxtMultiRoute.arrDispalyMember)
    End Sub
End Class
