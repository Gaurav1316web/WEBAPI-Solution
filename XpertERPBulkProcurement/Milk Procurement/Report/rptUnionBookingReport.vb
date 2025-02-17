Imports common
Public Class rptUnionBookingReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Dim dtRCDF As DataTable = Nothing
#End Region
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
    Private Sub rptUnionBookingReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt And e.KeyCode = Keys.G Then
            fillGridReport(False)
        ElseIf e.Alt And e.KeyCode = Keys.P Then
            fillGridReport(True)

        End If
    End Sub
    Private Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        rbtnAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        dtRCDF = Nothing
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
            If dtRCDF Is Nothing OrElse dtRCDF.Rows.Count <= 0 Then
                Dim sQuery As String = " with CTERawData as ( "
                Dim BaseQry As String = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate,Sno AS SNo,'DBLocation' AS [Union_Name], COUNT(DISTINCT XX.Zone_Code) AS [Zone],COUNT(DISTINCT XX.Route_No) AS [Route],COUNT(DISTINCT XX.Cust_Code) AS [Booth],isnull(Round(SUM(isnull(XX.ltrkg,0)),2),0) AS [MilkQty], isnull(Round((SUM(isnull(XX.ltrkg,0))/DATEDIFF(DAY, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')),2),0) as [AvarageQty]   FROM ( 
SELECT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DEMAND_BOOKING_MASTER.Route_No, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.qty,  (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) AS ltrkg FROM DBNamePrefixTSPL_DEMAND_BOOKING_MASTER
LEFT JOIN DBNamePrefixTSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No 
LEFT JOIN DBNamePrefixTSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
LEFT JOIN DBNamePrefixTSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_ITEM_MASTER.IsTaxable=0"
                If rbtnPosted.IsChecked Then
                    BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.Posted=1 "
                ElseIf rbtnUnposted.IsChecked Then
                    BaseQry += " and TSPL_DEMAND_BOOKING_MASTER.Posted=0 "
                End If
                BaseQry += "Union ALL SELECT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_Booking_DETAIL.Route_No, TSPL_Booking_DETAIL.Cust_Code, TSPL_Booking_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_Booking_DETAIL.Unit_code, TSPL_Booking_DETAIL.Booking_Qty, (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) AS ltrkg FROM DBNamePrefixTSPL_Booking_MATSER
LEFT JOIN DBNamePrefixTSPL_Booking_DETAIL ON TSPL_Booking_DETAIL.Document_No = TSPL_Booking_MATSER.Document_No
LEFT JOIN DBNamePrefixTSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Booking_DETAIL.Cust_Code
LEFT JOIN DBNamePrefixTSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_Booking_DETAIL.Item_Code
LEFT JOIN ( SELECT TSPL_ITEM_UOM_DETAIL.Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM DBNamePrefixTSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_Booking_DETAIL.Unit_code
where convert(date,TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_ITEM_MASTER.IsTaxable=0"
                If rbtnPosted.IsChecked Then
                    BaseQry += " and TSPL_Booking_MATSER.Posted=1 "
                ElseIf rbtnUnposted.IsChecked Then
                    BaseQry += " and TSPL_Booking_MATSER.Posted=0 "
                End If

                BaseQry += " ) XX "

                If objCommonVar.RCDFCFP Then
                    sQuery += clsERPFuncationality.ConvertQryForAllUnion(BaseQry, "DBNamePrefix", "DBLocation", "Sno")
                Else
                    Dim strqry As String = BaseQry.Replace("DBNamePrefix", "")
                    strqry = strqry.Replace("DBLocation", objCommonVar.CurrLocationName)
                    strqry = strqry.Replace("Sno", "1")
                    sQuery += strqry
                End If
                sQuery += ") 
 select * from CTERawData "
                dtRCDF = clsDBFuncationality.GetDataTable(sQuery)
                If (dtRCDF IsNot Nothing AndAlso dtRCDF.Rows.Count > 0) Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.Visible = True
                    gv1.DataSource = dtRCDF
                    gv1.ReadOnly = True
                    SetGridFormat()
                    gv1.BestFitColumns()
                    If isPrint Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
                        frmCRV = Nothing
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found")
                    gv1.DataSource = Nothing
                End If

            Else
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    '    Private Sub fillGridReport(ByVal isPrint As Boolean)
    '        Try
    '            TemplateGridview = gv1
    '            Dim query As String = ""
    '            gv1.DataSource = Nothing
    '            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RCDFCF") = CompairStringResult.Equal Then
    '                If dtRCDF Is Nothing OrElse dtRCDF.Rows.Count <= 0 Then
    '                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
    '                    If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
    '                        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
    '                        gv1.DataSource = Nothing
    '                        Exit Sub
    '                    End If
    '                    'dtRCDF.Columns.Add("FromDate", GetType(Date))
    '                    'dtRCDF.Columns.Add("ToDate", GetType(Date))
    '                    'dtRCDF.Columns.Add("SNo", GetType(Integer))
    '                    'dtRCDF.Columns.Add("Union_Name", GetType(String))
    '                    'dtRCDF.Columns.Add("Zone", GetType(Integer))
    '                    'dtRCDF.Columns.Add("Route", GetType(Integer))
    '                    'dtRCDF.Columns.Add("Booth", GetType(Integer))
    '                    'dtRCDF.Columns.Add("MilkQty", GetType(Double))
    '                    'dtRCDF.Columns.Add("AvarageQty", GetType(Double))
    '                    Dim dtr As DataTable = clsMilkUnion.UnionDBName()
    '                    For ii As Integer = 0 To dtr.Rows.Count - 1
    '                        'If ii > 0 Then
    '                        '    query += " UNION ALL "
    '                        'End If
    '                        query = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate," + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union_Name], COUNT(DISTINCT XX.Zone_Code) AS [Zone],COUNT(DISTINCT XX.Route_No) AS [Route],COUNT(DISTINCT XX.Cust_Code) AS [Booth],isnull(Round(SUM(XX.ltrkg),2),0) AS [MilkQty], isnull(Round((SUM(XX.ltrkg)/DATEDIFF(DAY, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')),2),0) as [AvarageQty]   FROM ( 
    'SELECT DISTINCT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Zone_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Route_No, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Short_Description, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.qty, CASE WHEN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No 
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code
    'where convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 "
    '                        If rbtnPosted.IsChecked Then
    '                            query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Posted=1"
    '                        ElseIf rbtnUnposted.IsChecked Then
    '                            query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Posted=0"
    '                        End If
    '                        query += " Union ALL SELECT DISTINCT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Zone_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Route_No, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Cust_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Short_Description, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Unit_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty, CASE WHEN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE ([" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Document_No = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_No
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Cust_Code
    'LEFT JOIN [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER ON [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_factor, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_code, [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_DETAIL.Unit_code
    'where convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,[" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.IsTaxable=0 
    '"
    '                        If rbtnPosted.IsChecked Then
    '                            query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Posted=1 "
    '                        ElseIf rbtnUnposted.IsChecked Then
    '                            query += " and [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Booking_MATSER.Posted=0 "
    '                        End If
    '                        query += " ) XX"
    '                        If dtRCDF Is Nothing OrElse dtRCDF.Rows.Count <= 0 Then
    '                            dtRCDF = clsDBFuncationality.GetDataTable(query)
    '                        Else
    '                            Dim dtrs As DataTable = clsDBFuncationality.GetDataTable(query)
    '                            dtRCDF.Merge(dtrs)
    '                        End If
    '                        'If dtrs IsNot Nothing AndAlso dtrs.Rows.Count > 0 Then
    '                        '    'Dim dr As DataRow = dtRCDF.NewRow()
    '                        '    'dr("FromDate") = clsCommon.myCDate(dtrs.Rows(0)("FromDate"))
    '                        '    'dr("ToDate") = clsCommon.myCDate(dtrs.Rows(0)("ToDate"))
    '                        '    'dr("SNo") = clsCommon.myCdbl(dtrs.Rows(0)("SNo"))
    '                        '    'dr("Union_Name") = clsCommon.myCstr(dtrs.Rows(0)("Union_Name"))
    '                        '    'dr("Zone") = clsCommon.myCdbl(dtrs.Rows(0)("Zone"))
    '                        '    'dr("Route") = clsCommon.myCdbl(dtrs.Rows(0)("Route"))
    '                        '    'dr("Booth") = clsCommon.myCdbl(dtrs.Rows(0)("Booth"))
    '                        '    'dr("MilkQty") = clsCommon.myCdbl(dtrs.Rows(0)("MilkQty"))
    '                        '    'dr("AvarageQty") = clsCommon.myCdbl(dtrs.Rows(0)("AvarageQty"))
    '                        '    'dtRCDF.Rows.Add(dr)
    '                        'End If
    '                    Next
    '                    'dtRCDF As DataTable = clsDBFuncationality.GetDataTable(query)
    '                    If (dtRCDF IsNot Nothing AndAlso dtRCDF.Rows.Count > 0) Then
    '                        RadPageView1.SelectedPage = RadPageViewPage2
    '                        gv1.Visible = True
    '                        gv1.DataSource = dtRCDF
    '                        gv1.ReadOnly = True
    '                        SetGridFormat()
    '                        gv1.BestFitColumns()
    '                        If isPrint Then
    '                            Dim frmCRV As New frmCrystalReportViewer()
    '                            frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
    '                            frmCRV = Nothing
    '                        End If
    '                    Else
    '                        common.clsCommon.MyMessageBoxShow("No Data Found")
    '                        gv1.DataSource = Nothing
    '                    End If
    '                Else
    '                    If isPrint Then
    '                        Dim frmCRV As New frmCrystalReportViewer()
    '                        frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dtRCDF, "rptAllUnionBookingReport", "")
    '                        frmCRV = Nothing
    '                    End If
    '                End If
    '            Else
    '                query = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate, COUNT(DISTINCT XX.Zone_Code) AS [No of Zone],COUNT(DISTINCT XX.Route_No) AS [No of Route],COUNT(DISTINCT XX.Cust_Code) AS [No of Booth],Round(SUM(XX.ltrkg),2) AS [Milk Qty in LTR], Round((SUM(XX.ltrkg)/DATEDIFF(DAY, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')),2) as [Avarage Qty]   FROM ( 
    'SELECT DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DEMAND_BOOKING_MASTER.Route_No, TSPL_DEMAND_BOOKING_DETAIL.Cust_Code, TSPL_DEMAND_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.qty, CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE (TSPL_DEMAND_BOOKING_DETAIL.Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM TSPL_DEMAND_BOOKING_MASTER
    'LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No 
    'LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
    'LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
    'where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_ITEM_MASTER.IsTaxable=0 "
    '                If rbtnPosted.IsChecked Then
    '                    query += " and TSPL_DEMAND_BOOKING_MASTER.Posted=1"
    '                ElseIf rbtnUnposted.IsChecked Then
    '                    query += " and TSPL_DEMAND_BOOKING_MASTER.Posted=0"
    '                End If
    '                query += " Union ALL SELECT DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_Booking_DETAIL.Route_No, TSPL_Booking_DETAIL.Cust_Code, TSPL_Booking_DETAIL.Item_Code, TSPL_ITEM_MASTER.Short_Description, TSPL_Booking_DETAIL.Unit_code, TSPL_Booking_DETAIL.Booking_Qty, CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInLTR.Conversion_Factor, 0) ELSE (TSPL_Booking_DETAIL.Booking_Qty * ItemConversion.Conversion_Factor) / NULLIF(ItemConversionInKG.Conversion_Factor, 0) END AS ltrkg FROM TSPL_Booking_MATSER
    'LEFT JOIN TSPL_Booking_DETAIL ON TSPL_Booking_DETAIL.Document_No = TSPL_Booking_MATSER.Document_No
    'LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Booking_DETAIL.Cust_Code
    'LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_Booking_DETAIL.Item_Code
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR' ) AS ItemConversionInLTR ON ItemConversionInLTR.Item_code = TSPL_Booking_DETAIL.Item_Code
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code FROM TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'KG' ) AS ItemConversionInKG ON ItemConversionInKG.Item_code = TSPL_Booking_DETAIL.Item_Code 
    'LEFT JOIN ( SELECT Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code, TSPL_ITEM_UOM_DETAIL.UOM_Code FROM TSPL_ITEM_UOM_DETAIL ) AS ItemConversion ON ItemConversion.Item_code = TSPL_Booking_DETAIL.Item_Code AND ItemConversion.UOM_Code = TSPL_Booking_DETAIL.Unit_code
    'where convert(date,TSPL_Booking_MATSER.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_Booking_MATSER.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  and TSPL_ITEM_MASTER.IsTaxable=0 
    '"
    '                If rbtnPosted.IsChecked Then
    '                    query += " and TSPL_Booking_MATSER.Posted=1 "
    '                ElseIf rbtnUnposted.IsChecked Then
    '                    query += " and TSPL_Booking_MATSER.Posted=0 "
    '                End If
    '                query += " ) XX"
    '                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
    '                If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
    '                    RadPageView1.SelectedPage = RadPageViewPage2
    '                    gv1.Visible = True
    '                    gv1.DataSource = dt2
    '                    gv1.ReadOnly = True
    '                    SetGridFormat1()
    '                    gv1.BestFitColumns()
    '                    If isPrint Then
    '                        Dim frmCRV As New frmCrystalReportViewer()
    '                        frmCRV.funreport(CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "rptUnionBookingReport", "")
    '                        frmCRV = Nothing
    '                    End If
    '                Else
    '                    common.clsCommon.MyMessageBoxShow("No Data Found")
    '                    gv1.DataSource = Nothing
    '                End If
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
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
        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False
        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False
        gv1.Columns("SNo").HeaderText = "SL No"
        gv1.Columns("SNo").Width = 150
        gv1.Columns("SNo").IsVisible = True
        gv1.Columns("Union_Name").HeaderText = "Union Name"
        gv1.Columns("Union_Name").Width = 150
        gv1.Columns("Union_Name").IsVisible = True
        gv1.Columns("Zone").HeaderText = "No of Zone"
        gv1.Columns("Zone").Width = 150
        gv1.Columns("Zone").FormatString = "{0:n0}"
        gv1.Columns("Zone").IsVisible = True
        gv1.Columns("Route").HeaderText = "No of Route"
        gv1.Columns("Route").Width = 150
        gv1.Columns("Route").FormatString = "{0:n0}"
        gv1.Columns("Route").IsVisible = True
        gv1.Columns("Booth").HeaderText = "No of Booth"
        gv1.Columns("Booth").Width = 150
        gv1.Columns("Booth").FormatString = "{0:n0}"
        gv1.Columns("Booth").IsVisible = True
        gv1.Columns("MilkQty").HeaderText = "Milk Qty in LTR"
        gv1.Columns("MilkQty").Width = 150
        gv1.Columns("MilkQty").FormatString = "{0:n2}"
        gv1.Columns("MilkQty").IsVisible = True
        gv1.Columns("AvarageQty").HeaderText = "Avarage Qty"
        gv1.Columns("AvarageQty").Width = 170
        gv1.Columns("AvarageQty").FormatString = "{0:n0}"
        gv1.Columns("AvarageQty").IsVisible = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item3 As New GridViewSummaryItem("Zone", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Route", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Booth", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("MilkQty", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("AvarageQty", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True
        'View()
    End Sub
    Sub SetGridFormat1()
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
        gv1.Columns("FromDate").HeaderText = "From Date"
        gv1.Columns("FromDate").Width = 150
        gv1.Columns("FromDate").IsVisible = False
        gv1.Columns("ToDate").HeaderText = "To Date"
        gv1.Columns("ToDate").Width = 150
        gv1.Columns("ToDate").IsVisible = False
        gv1.Columns("No of Zone").HeaderText = "No of Zone"
        gv1.Columns("No of Zone").Width = 150
        gv1.Columns("No of Zone").FormatString = "{0:n0}"
        gv1.Columns("No of Zone").IsVisible = True
        gv1.Columns("No of Route").HeaderText = "No of Route"
        gv1.Columns("No of Route").Width = 150
        gv1.Columns("No of Route").FormatString = "{0:n0}"
        gv1.Columns("No of Route").IsVisible = True
        gv1.Columns("No of Booth").HeaderText = "No of Booth"
        gv1.Columns("No of Booth").Width = 150
        gv1.Columns("No of Booth").FormatString = "{0:n0}"
        gv1.Columns("No of Booth").IsVisible = True
        gv1.Columns("Milk Qty in LTR").HeaderText = "Milk Qty in LTR"
        gv1.Columns("Milk Qty in LTR").Width = 150
        gv1.Columns("Milk Qty in LTR").FormatString = "{0:n2}"
        gv1.Columns("Milk Qty in LTR").IsVisible = True
        gv1.Columns("Avarage Qty").HeaderText = "Avarage Qty"
        gv1.Columns("Avarage Qty").Width = 170
        gv1.Columns("Avarage Qty").FormatString = "{0:n0}"
        gv1.Columns("Avarage Qty").IsVisible = True
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item3 As New GridViewSummaryItem("Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("Average Quantity", "{0:f0}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        ' View()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        fillGridReport(True)

    End Sub



End Class