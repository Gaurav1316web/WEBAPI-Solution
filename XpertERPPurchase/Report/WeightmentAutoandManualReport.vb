Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class Weightment_Auto_and_Manual_Report

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'qry += " where 2=2 and Seg_No = '7' AND GIT='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub


    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        rbtLoadSlip.Checked = False
        rbtWeightment.Checked = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))
            arrHeader.Add("Auto Weightment And Manual Weightment Report")


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If


            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Auto Weightment and Manual Weightment Report", gv1, arrHeader, "Auto Weightment and Manual Weightment Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub



    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Getdata(False)
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtWeightment.Checked Then
            VarID += "_W"
        ElseIf rbtLoadSlip.Checked Then
            VarID += "_LS"
        End If
        gv1.VarID = VarID
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Getdataprint(True)
    End Sub
    Public Sub Getdata(ByVal print As Boolean)
        Try
            Dim strqry As String = ""
            GetReportID()
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            If rbtWeightment.Checked = True Then
                strqry = " SELECT aa.Location_Code as Location,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%]
             FROM (SELECT Location_Code,
             CASE WHEN (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1) THEN COUNT(*)END AS Auto_wt,
             CASE WHEN (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
             END AS Manual_wt, COUNT(*) AS Total_wt
             FROM TSPL_PO_WEIGHTMENT_HEAD 
             where convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY Location_Code,Is_Auto_Gross_Weight,Is_Auto_Tare_Weight) aa  "

            Else
                strqry = " SELECT aa.Location_Code as Location,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%]
             FROM (SELECT Location_Code,
             CASE WHEN (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1) THEN COUNT(*)END AS Auto_wt,
             CASE WHEN (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
             END AS Manual_wt, COUNT(*) AS Total_wt
             FROM TSPL_RCDF_LOAD_IN 
             where convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY Location_Code,Is_Auto_Gross_Weight,Is_Auto_Tare_Weight) aa  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " where aa.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            strqry += "GROUP BY aa.Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.SummaryRowsBottom.Clear()

                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()

                    gv1.DataSource = dt
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    summary()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                    gv1.EnableFiltering = True
                Else
                    If rbtWeightment.Checked = True Then
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then
                            Dim frmCRV As New frmCrystalReportViewer()
                            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "WeightmentAutoandManualReport2", "")
                            frmCRV = Nothing
                        Else
                            clsCommon.MyMessageBoxShow("No Data Found")
                        End If
                    ElseIf rbtLoadSlip.Checked = True Then
                        If dt IsNot Nothing And dt.Rows.Count > 0 Then
                            Dim frmCRV As New frmCrystalReportViewer()
                            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "WeightmentAutoandManualReport", "")
                            frmCRV = Nothing
                        Else
                            clsCommon.MyMessageBoxShow("No Data Found")
                        End If
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            Dim strloc As String = ""
            Dim strcolm As String = ""
            Dim strcolm2 As String = ""
            Dim strcolm1 As String = ""
            If gv1.Rows.Count > 0 Then
                If gv1.CurrentRow IsNot Nothing Then
                    strcolm = gv1.Columns(0).Name
                    strloc = gv1.CurrentRow.Cells(0).Value
                    strcolm1 = gv1.CurrentRow.Cells("Location").Value
                    strcolm2 = gv1.CurrentColumn.Name
                End If
            End If
            Dim strqry As String = ""
            'TemplateGridview = gv1
            If rbtWeightment.Checked = True Then
                strqry = " SELECT ROW_NUMBER() OVER (PARTITION BY TSPL_PO_WEIGHTMENT_HEAD.Location_Code ORDER BY TSPL_PO_WEIGHTMENT_HEAD.Location_Code) AS SrNo, 
             TSPL_PO_WEIGHTMENT_HEAD.Location_Code Location,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code [Document Code],TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date [Document Date],
             TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No [Gate Entry No],tspl_grn_head.GRN_Date[Gate Entry Date],tspl_grn_head.VehicleNo [Vehicle NO],tspl_grn_head.Vendor_Code[Vendor Code],tspl_grn_head.Vendor_Name[Vendor Name],
             TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight[Gross Weight],
             TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight[Tare Weight],TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight[Extra Weight],TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight[Net Weight]
             FROM TSPL_PO_WEIGHTMENT_HEAD 
			 left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
			 left outer join tspl_grn_head on tspl_grn_head.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
             where convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)<=convert(date,('" + txtToDate.Value + "'),103) "
                If strcolm2 = "Auto Weighment" Then
                    strqry += "  And (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1)   "
                END If
                If strcolm2 = "Manual Weighment" Then
                    strqry += "  And ((Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0))  "
                END If
                If strcolm2 = "Pending" Then
                    strqry += "  and (isnull(Is_Auto_Gross_Weight,2)=2 or isnull(Is_Auto_Tare_Weight,2)=2)  "
                End If
                If strcolm = "Location" Then
                    strqry += " and TSPL_PO_WEIGHTMENT_HEAD.location_code=('" + strloc + "')"
                End If
            Else
                strqry = "SELECT ROW_NUMBER() OVER (PARTITION BY TSPL_RCDF_LOAD_IN.Location_Code ORDER BY TSPL_RCDF_LOAD_IN.Location_Code) AS SrNo,
            TSPL_RCDF_LOAD_IN.Location_Code[Location],TSPL_RCDF_LOAD_IN.Document_Code [Document Code],TSPL_RCDF_LOAD_IN.Document_Date [Document Date],TSPL_RCDF_LOAD_IN.Vehicle_No[Vehicle No],TSPL_RCDF_LOAD_IN.Customer_Code[Customer Code],
            TSPL_CUSTOMER_MASTER.Customer_Name[Customer Name],TSPL_RCDF_LOAD_IN.Gross_Weight[Gross Weight],TSPL_RCDF_LOAD_IN.Tare_Weight[Tare Weight],TSPL_RCDF_LOAD_IN.Extra_Weight[Extra Weight],TSPL_RCDF_LOAD_IN.Net_Weight[Net Weight]
             FROM TSPL_RCDF_LOAD_IN 
			 
			 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RCDF_LOAD_IN.Customer_Code
             where convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)<=convert(date,('" + txtToDate.Value + "'),103) "
                If strcolm2 = "Auto Weighment" Then
                    strqry += "  And (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1)   "
                End If
                If strcolm2 = "Manual Weighment" Then
                    strqry += "  And ((Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0))  "
                End If
                If strcolm2 = "Pending" Then
                    strqry += "  and  (isnull(Is_Auto_Gross_Weight,2)=2 or isnull(Is_Auto_Tare_Weight,2)=2) "
                End If
                If strcolm = "Location" Then
                    strqry += " and TSPL_RCDF_LOAD_IN.location_code=('" + strloc + "')"
                End If
            End If

            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    strqry += " where aa.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            'End If
            'strqry += "GROUP BY aa.Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then

                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.SummaryRowsBottom.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.MasterView.Refresh()

                gv2.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                Next
                summaryDrill()
                RadPageView1.SelectedPage = RadPageViewPage3
                gv2.BestFitColumns()
                gv2.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub summary()

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item0 As New GridViewSummaryItem("Location", "Total: {0:F0}", GridAggregateFunction.Count)
        summaryRowItem.Add(item0)

        Dim item1 As New GridViewSummaryItem("Auto Weighment", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim summaryRow As New GridViewSummaryRowItem()
        summaryRow.Add(item1)

        Dim item2 As New GridViewSummaryItem("Manual Weighment", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Pending", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Total", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Auto%", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("Manual%", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("Pending%", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item7)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Sub summaryDrill()

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item0 As New GridViewSummaryItem("Location", "Total: {0:F0}", GridAggregateFunction.Count)
        summaryRowItem.Add(item0)

        Dim item1 As New GridViewSummaryItem("Gross Weight", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim summaryRow As New GridViewSummaryRowItem()
        summaryRow.Add(item1)

        Dim item2 As New GridViewSummaryItem("Tare Weight", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Extra Weight", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Net Weight", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)


        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Public Sub Getdataprint(ByVal print As Boolean)
        Try
            Dim strqry As String = ""
            GetReportID()
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            If rbtWeightment.Checked = True Then
                strqry = " SELECT 
   'Weightment' AS ReportName,
    aa.Location_Code AS Location,
    '" + txtFromDate.Value + "' as From_Date,
	'" + txtToDate.Value + "' as To_Date,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%],
    aa.Location_Desc
FROM (
    SELECT 
        r.Location_Code,
        r.Weighment_Date,
        l.Location_Desc,
        CASE WHEN (r.Is_Auto_Gross_Weight = 1 AND r.Is_Auto_Tare_Weight = 1) THEN COUNT(*) END AS Auto_wt,
        CASE WHEN (r.Is_Auto_Gross_Weight = 0 AND r.Is_Auto_Tare_Weight = 0) 
               OR (r.Is_Auto_Gross_Weight = 0 AND r.Is_Auto_Tare_Weight = 1) 
               OR (r.Is_Auto_Gross_Weight = 1 AND r.Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
        END AS Manual_wt, 
        COUNT(*) AS Total_wt
    FROM TSPL_PO_WEIGHTMENT_HEAD r
    LEFT JOIN TSPL_LOCATION_MASTER l ON r.Location_Code = l.Location_Code
             where CONVERT(DATE, r.Weighment_Date, 103)>=convert(date,('" + txtFromDate.Value + "'),103) and CONVERT(DATE, r.Weighment_Date, 103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY r.Location_Code, r.Weighment_Date, r.Is_Auto_Gross_Weight, r.Is_Auto_Tare_Weight, l.Location_Desc
) aa  
GROUP BY aa.Location_Code, aa.Location_Desc
ORDER BY aa.Location_Code  "

            Else
                strqry = " SELECT aa.Location_Code as Location,
'" + txtFromDate.Value + "' as From_Date,
	'" + txtToDate.Value + "' as To_Date,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%]
             FROM (SELECT Location_Code,
             CASE WHEN (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1) THEN COUNT(*)END AS Auto_wt,
             CASE WHEN (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
             END AS Manual_wt, COUNT(*) AS Total_wt
             FROM TSPL_RCDF_LOAD_IN 
             where convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY Location_Code, Is_Auto_Gross_Weight, Is_Auto_Tare_Weight
            ) aa
            GROUP BY aa.Location_Code
            ORDER BY aa.Location_Code  "
            End If
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    strqry += " where aa.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            'End If
            'strqry += "GROUP BY aa.Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)

            If rbtWeightment.Checked = True Then
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.Purchase, dt, "WeightmentAutoandManualReport2", "")
                    frmCRV = Nothing
                Else
                    clsCommon.MyMessageBoxShow("No Data Found")
                End If
            ElseIf rbtLoadSlip.Checked = True Then
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.Purchase, dt, "WeightmentAutoandManualReport", "")
                    frmCRV = Nothing
                Else
                    clsCommon.MyMessageBoxShow("No Data Found")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv2_DoubleClick(sender As Object, e As EventArgs) Handles gv2.DoubleClick
        Dim strDoc, strDoc1, strcolm
        If gv2.Rows.Count > 0 Then
            strcolm = gv2.CurrentColumn.Name
            'strcolm = gv2.Columns(1).Name
            If strcolm = "Document Code" Then
                strDoc = gv2.CurrentRow.Cells("Document Code").Value
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.POWeighment, strDoc)
                'MDI.ShowForm(clsUserMgtCode.mbtnGRN, "", True, strDoc)
            Else

                If strcolm = "Gate Entry No" Then
                    strDoc1 = gv2.CurrentRow.Cells("Gate Entry No").Value
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, strDoc1)
                End If
            End If
        End If
    End Sub
    Private Sub Weightment_Auto_and_Manual_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        AddHandler gv1.ViewCellFormatting, AddressOf gv1_ViewCellFormatting
        AddHandler gv2.ViewCellFormatting, AddressOf gv2_ViewCellFormatting
    End Sub
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))
            arrHeader.Add("Auto Weightment And Manual Weightment Report")


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If


            If gv2.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If gv2.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Auto Weightment and Manual Weightment Report", gv2, arrHeader, "Auto Weightment and Manual Weightment Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(sender As Object, e As UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is GridSummaryCellElement Then
            Dim summaryCell As GridSummaryCellElement = DirectCast(e.CellElement, GridSummaryCellElement)
            If e.CellElement.ColumnInfo.Name = "Auto Weighment" Or e.CellElement.ColumnInfo.Name = "Manual Weighment" Or e.CellElement.ColumnInfo.Name = "Pending" Or e.CellElement.ColumnInfo.Name = "Total" Or e.CellElement.ColumnInfo.Name = "Auto%" Or e.CellElement.ColumnInfo.Name = "Manual%" Or e.CellElement.ColumnInfo.Name = "Pending%" Then
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            End If
            e.CellElement.Font = New Font("Arial", 8, FontStyle.Bold)
        End If
    End Sub

    Private Sub gv2_ViewCellFormatting(sender As Object, e As UI.CellFormattingEventArgs) Handles gv2.ViewCellFormatting
        If TypeOf e.CellElement Is GridSummaryCellElement Then
            Dim summaryCell As GridSummaryCellElement = DirectCast(e.CellElement, GridSummaryCellElement)
            If e.CellElement.ColumnInfo.Name = "Location" Then
                e.CellElement.TextAlignment = ContentAlignment.MiddleLeft
            End If
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            e.CellElement.Font = New Font("Arial", 8, FontStyle.Bold)
            e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Inherited)
            e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Inherited)
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid

        End If
    End Sub
    Private Sub gv1_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv1.ViewRowFormatting
        If TypeOf e.RowElement Is GridDataRowElement Then
            ' Get the Transaction Type column value
            Dim transactionType As String = e.RowElement.RowInfo.Cells("Location").Value.ToString()
            Select Case transactionType
                Case "AJMR"
                    e.RowElement.BackColor = Color.LightGreen
                Case "BIKR"
                    e.RowElement.BackColor = Color.LightGoldenrodYellow
                Case "JODH"
                    e.RowElement.BackColor = Color.LightCoral
                Case "KALR"
                    e.RowElement.BackColor = Color.LightSkyBlue
                Case "LAMB"
                    e.RowElement.BackColor = Color.LightSalmon
                Case "NADB"
                    e.RowElement.BackColor = Color.LightSeaGreen
                Case "PALI"
                    e.RowElement.BackColor = Color.LightPink
                Case Else
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End Select
            e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Inherited)
            e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Inherited)
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid

        End If
    End Sub
    Private Sub gv2_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv2.ViewRowFormatting
        If TypeOf e.RowElement Is GridDataRowElement Then
            Dim rowIndex As Integer = e.RowElement.RowInfo.Index
            Dim transactionType As String = e.RowElement.RowInfo.Cells("Location").Value.ToString()
            Select Case transactionType
                Case "AJMR"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightGreen  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "BIKR"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightGoldenrodYellow  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "JODH"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightCoral  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "KALR"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightSkyBlue  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "LAMB"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightSalmon  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "NADB"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightSeaGreen  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case "PALI"
                    If rowIndex Mod 2 = 0 Then
                        e.RowElement.BackColor = Color.LightPink  ' Even row color
                    Else
                        e.RowElement.BackColor = Color.LightGray  ' Odd row color
                    End If
                Case Else
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End Select
            If TypeOf e.RowElement Is GridSummaryRowElement Then
                e.RowElement.Font = New Font("Arial", 8, FontStyle.Bold)
            End If
            e.RowElement.DrawFill = True

        End If
    End Sub

End Class