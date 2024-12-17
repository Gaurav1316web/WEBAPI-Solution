Imports common
Public Class FrmBoothDemandReport

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        TxtRoute.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDetail.IsChecked = True
        rbtnSummary.IsChecked = False
        rdbMilk.Checked = False
        rdbProduct.Checked = False
        rdbDemandBoth.Checked = True
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs)
        ExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs)
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
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
                clsCommon.MyExportToExcelGrid("Booth Demand Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Booth Demand Report", gvData, arrHeader, "Booth Demand Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub GetReportGridID()
        Dim VarID As String = ""
        If rbtnDetail.IsChecked Then
            VarID += "_DE"
        ElseIf rbtnSummary.IsChecked Then
            VarID += "_Su"
        End If
        If rdbMilk.Checked Then
            VarID += "_MK"
        ElseIf rdbProduct.Checked Then
            VarID += "_PR"
        ElseIf rdbDemandBoth.Checked Then
            VarID += "_BT"
        End If
        gvData.VarID = VarID
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            Dim Whr As String = ""
            Dim dt As DataTable = Nothing
            Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = ""
            'If rbtnDocumentdate.IsChecked Then
            '    Whr += " where Convert( Date, TSPL_DEMAND_BOOKING_MASTER.document_Date,103) >= Convert( Date,'" + strtxtfDate + "',103) AND 
            '                    Convert( Date, TSPL_DEMAND_BOOKING_MASTER.document_Date,103) <= Convert(Date,'" + strToDate + "',103) "
            'Else
            '    Whr += " where Convert( Date, TSPL_DEMAND_BOOKING_MASTER.Supply_Date,103) >= Convert( Date,'" + strtxtfDate + "',103) AND 
            '                    Convert( Date, TSPL_DEMAND_BOOKING_MASTER.Supply_Date,103) <= Convert(Date,'" + strToDate + "',103)  "
            'End If

            'If ChkB2B.Checked = True Then
            '    Whr += " and TSPL_CUSTOMER_MASTER.GST_Registered=1 "
            'ElseIf chkB2C.Checked = True Then
            '    Whr += " and TSPL_CUSTOMER_MASTER.GST_Registered=0 "
            'End If

            If rdbMilk.Checked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ItemType='Fresh' "
            ElseIf rdbProduct.Checked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ItemType='Product' "
            ElseIf rdbDemandBoth.Checked Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Both' "
            End If

            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.route_no In (" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")"
            End If

            If rbtnDetail.IsChecked Then
                GetReportGridID()
                                    qry = "SELECT 
                        [Date], 
                        [Booth Code], 
                        MAX([Customer Name]) AS [Booth Name], 
                        MAX([Mobile No]) AS [Mobile No], 
                        MAX([Route]) AS [Route], 
                        MAX([Route Name]) AS [Route Name], 
                        [Shift],
                        CAST(SUM([Tonned Milk 500 Ml]) AS DECIMAL(10, 2)) AS [TM500], 
                        CAST(SUM([Tonned Milk 1 LTR]) AS DECIMAL(10, 2)) AS [TM1LT], 
                        CAST(SUM([Standard Milk 500 Ml]) AS DECIMAL(10, 2)) AS [SM500],
                        CAST(SUM([Standard Milk 1 LTR]) AS DECIMAL(10, 2)) AS [SM1LT], 
                        CAST(SUM([GOLD Milk 500 Ml]) AS DECIMAL(10, 2)) AS [GM500], 
                        CAST(SUM([GOLD Milk 1 LTR]) AS DECIMAL(10, 2)) AS [GM1LT],
                        CAST(SUM([PLAIN CHHACH 1/2 LTR]) AS DECIMAL(10, 2)) AS [CHHACH], 
                        CAST(SUM([TOND Milk 6Ltr]) AS DECIMAL(10, 2)) AS [TM6LT], 
                        CAST(SUM([GOLD Milk 6Ltr]) AS DECIMAL(10, 2)) AS [GM6LT],
                        CAST(SUM([COW MILK 500 Ml]) AS DECIMAL(10, 2)) AS [COW500]
                    FROM (
                        SELECT 
                            CASE 
                                WHEN ITEMDETAIL1.Report_UOM = 1 THEN 
                                    ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)
                            END AS [Quantity],
                            CONVERT(VARCHAR, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) AS [Date],
                            TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],
                            TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],
                            TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                            TSPL_ROUTE_MASTER.Route_No AS [Route],
                            TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name],
                            TSPL_DEMAND_BOOKING_MASTER.ShiftType AS [Shift],
                            tspl_item_master.Item_Desc,
                            TSPL_DEMAND_BOOKING_DETAIL.Qty
                        FROM TSPL_DEMAND_BOOKING_MASTER
                        LEFT OUTER JOIN TSPL_ROUTE_MASTER 
                            ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                        LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL 
                            ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                        LEFT JOIN TSPL_CUSTOMER_MASTER 
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                        LEFT JOIN tspl_item_master 
                            ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                        LEFT JOIN TSPL_ITEM_UOM_DETAIL 
                            ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code 
                            AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                        LEFT JOIN (
                            SELECT 
                                Conversion_Factor,
                                Item_Code,
                                Report_UOM,
                                UOM_Code
                            FROM TSPL_ITEM_UOM_DETAIL
                            WHERE Report_UOM = 1
                        ) AS ITEMDETAIL1 
                            ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                        where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + txtfDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " + Whr + " 
                        ) AS SourceData
                        PIVOT (
                        SUM([Quantity])FOR item_desc IN ([Tonned Milk 500 Ml],[Tonned Milk 1 LTR],[Standard Milk 500 Ml],[Standard Milk 1 LTR], [GOLD Milk 500 Ml],[GOLD Milk 1 LTR], [PLAIN CHHACH 1/2 LTR], [TOND Milk 6Ltr],[GOLD Milk 6Ltr],[COW MILK 500 Ml])
                        ) AS PivotTable
                        GROUP BY 
                        [Date], [Booth Code], [Shift] ORDER BY 
                        CAST([Booth Code] AS INT)"
            ElseIf rbtnSummary.IsChecked Then
                qry = "SELECT 
                        [Booth Code],
                        MAX([Customer Name]) AS [Booth Name],
                        MAX([Mobile No]) AS [Mobile No],
                        MAX([Route]) AS [Route],
                        MAX([Route Name]) AS [Route Name],
                        CAST(SUM([Tonned Milk 500 Ml]) AS DECIMAL(10, 2)) AS [TM500], 
                        CAST(SUM([Tonned Milk 1 LTR]) AS DECIMAL(10, 2)) AS [TM1LT], 
                        CAST(SUM([Standard Milk 500 Ml]) AS DECIMAL(10, 2)) AS [SM500],
                        CAST(SUM([Standard Milk 1 LTR]) AS DECIMAL(10, 2)) AS [SM1LT], 
                        CAST(SUM([GOLD Milk 500 Ml]) AS DECIMAL(10, 2)) AS [GM500], 
                        CAST(SUM([GOLD Milk 1 LTR]) AS DECIMAL(10, 2)) AS [GM1LT],
                        CAST(SUM([PLAIN CHHACH 1/2 LTR]) AS DECIMAL(10, 2)) AS [CHHACH], 
                        CAST(SUM([TOND Milk 6Ltr]) AS DECIMAL(10, 2)) AS [TM6LT], 
                        CAST(SUM([GOLD Milk 6Ltr]) AS DECIMAL(10, 2)) AS [GM6LT],
                        CAST(SUM([COW MILK 500 Ml]) AS DECIMAL(10, 2)) AS [COW500]
                    FROM (
                        SELECT 
                            CASE 
                                WHEN ITEMDETAIL1.Report_UOM = 1 THEN 
                                    ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)
                            END AS [Quantity],
                            TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],
                            TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name],
                            TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                            TSPL_ROUTE_MASTER.Route_No AS [Route],
                            TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name],
                            tspl_item_master.Item_Desc,
                            TSPL_DEMAND_BOOKING_DETAIL.Qty,
                            TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                        FROM TSPL_DEMAND_BOOKING_MASTER
                        LEFT OUTER JOIN TSPL_ROUTE_MASTER 
                            ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                        LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL 
                            ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                        LEFT JOIN TSPL_CUSTOMER_MASTER 
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                        LEFT JOIN tspl_item_master 
                            ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                        LEFT JOIN TSPL_ITEM_UOM_DETAIL 
                            ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code 
                            AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                        LEFT JOIN (
                            SELECT 
                                Conversion_Factor,
                                Item_Code,
                                Report_UOM,
                                UOM_Code
                            FROM TSPL_ITEM_UOM_DETAIL
                            WHERE Report_UOM = 1
                        ) AS ITEMDETAIL1 
                            ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                        where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + txtfDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + txtToDate.Value + "',103) " + Whr + " 
                        ) AS SourceData
                    PIVOT (
                    SUM([Quantity])FOR item_desc IN ([Tonned Milk 500 Ml],[Tonned Milk 1 LTR],[Standard Milk 500 Ml],[Standard Milk 1 LTR], [GOLD Milk 500 Ml],[GOLD Milk 1 LTR], [PLAIN CHHACH 1/2 LTR], [TOND Milk 6Ltr],[GOLD Milk 6Ltr],[COW MILK 500 Ml])
                    ) AS PivotTable
                    GROUP BY 
                    [Booth Code] ORDER BY 
                    CAST([Booth Code] AS INT)"
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



                'SetGridFormationOFGV1()
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

    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrmBoothDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rbtnDetail.IsChecked = True
        rbtnSummary.IsChecked = False
        rdbMilk.Checked = False
        rdbProduct.Checked = False
        rdbDemandBoth.Checked = True
    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        If gvData.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmenuPDF_Click_1(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class
