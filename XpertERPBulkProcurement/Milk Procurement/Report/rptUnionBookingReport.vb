Imports common

Public Class rptUnionBookingReport
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptUnionBookingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

        Reset()
    End Sub
    Private Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        rbtnAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        FrmClose()
    End Sub
    Private Sub FrmClose()
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        fillGridReport(False)
    End Sub
    Private Sub fillGridReport(ByVal isPrint As Boolean)
        Try

            TemplateGridview = gv1

            Dim query As String = ""
            gv1.DataSource = Nothing
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RCDF") = CompairStringResult.Equal Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gv1.DataSource = Nothing
                    Exit Sub
                End If
                Dim dtr As DataTable = clsMilkUnion.UnionDBName()
                For ii As Integer = 0 To dtr.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query = " SELECT " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name], COUNT(DISTINCT XX.Zone_Code) AS [No of Zone],COUNT(DISTINCT XX.Route_No) AS [No of Route],COUNT(DISTINCT XX.Cust_Code) AS [No of Booth],Round(SUM(XX.ltrkg),2) AS [Milk Qty in LTR/KG], Round((SUM(XX.ltrkg)/DATEDIFF(DAY, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')),2) as [Avarage Qty]   FROM ( 
SELECT DISTINCT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Zone_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Route_No, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Short_Description, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.qty, CASE WHEN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER
LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No 
LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code
where convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 "
                    If rbtnPosted.IsChecked Then
                        query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Posted=1"
                    ElseIf rbtnUnposted.IsChecked Then
                        query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Posted=0"
                    End If
                    query += " Union ALL SELECT DISTINCT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Zone_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Route_No, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Cust_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Short_Description, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Unit_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty, CASE WHEN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER
LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_No
LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Cust_Code
LEFT JOIN TSPL_ITEM_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILUOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILUOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code 
LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAILConversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Unit_code
where convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 
"
                    If rbtnPosted.IsChecked Then
                        query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Posted=1 "
                    ElseIf rbtnUnposted.IsChecked Then
                        query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Posted=0 "
                    End If
                    query += " ) XX"
                Next
                Dim dtRCDF As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dtRCDF IsNot Nothing AndAlso dtRCDF.Rows.Count > 0) Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.Visible = True
                    gv1.DataSource = dtRCDF
                    gv1.ReadOnly = True
                    gv1.BestFitColumns()
                    If isPrint Then
                        Dim frmCRV As New frmCrystalReportViewer()

                        frmCRV.funreport(CrystalReportFolder.SalesReport, dtRCDF, "rptERPStatusTrackingReportUnion", "")
                        frmCRV = Nothing

                    End If


                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found")
                    gv1.DataSource = Nothing
                End If
            Else
                query = " SELECT COUNT(DISTINCT XX.Zone_Code) AS [No of Zone],COUNT(DISTINCT XX.Route_No) AS [No of Route],COUNT(DISTINCT XX.Cust_Code) AS [No of Booth],Round(SUM(XX.ltrkg),2) AS [Milk Qty in LTR/KG], Round((SUM(XX.ltrkg)/DATEDIFF(DAY, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')),2) as [Avarage Qty]   FROM ( 
SELECT DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DEMAND_BOOKING_MASTER.Route_No, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.qty, CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM TSPL_DEMAND_BOOKING_MASTER
LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No 
LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_ITEM_MASTER.IsTaxable=0 "
                If rbtnPosted.IsChecked Then
                    query += " and TSPL_DEMAND_BOOKING_MASTER.Posted=1"
                ElseIf rbtnUnposted.IsChecked Then
                    query += " and TSPL_DEMAND_BOOKING_MASTER.Posted=0"
                End If
                query += " Union ALL SELECT DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_Booking_DETAIL.Route_No, TSPL_Booking_DETAIL.Cust_Code, TSPL_Booking_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_Booking_DETAIL.Unit_code, TSPL_Booking_DETAIL.Booking_Qty, CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM TSPL_Booking_MATSER
LEFT JOIN TSPL_Booking_DETAIL ON TSPL_Booking_DETAIL.Document_No = TSPL_Booking_MATSER.Document_No
LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Booking_DETAIL.Cust_Code
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = TSPL_Booking_DETAIL.Item_Code 
LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_Booking_DETAIL.Unit_code
where convert(date,TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_ITEM_MASTER.IsTaxable=0 
"
                If rbtnPosted.IsChecked Then
                    query += " and TSPL_Booking_MATSER.Posted=1 "
                ElseIf rbtnUnposted.IsChecked Then
                    query += " and TSPL_Booking_MATSER.Posted=0 "
                End If
                query += " ) XX"
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.Visible = True
                gv1.DataSource = dt2
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()

                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt2, "rptERPStatusTrackingReportUnion", "")
                    frmCRV = Nothing

                End If


            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
            gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class