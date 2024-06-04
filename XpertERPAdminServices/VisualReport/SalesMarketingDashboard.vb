Imports common
Public Class SalesMarketingDashboard
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        AddNew()
    End Sub
    Sub Addnew()
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        gv3.DataSource = Nothing
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGridgv1(EnumExportTo.Excel)
        ExportGridgv2(EnumExportTo.Excel)
        'ExportGridgv3(EnumExportTo.Excel)
    End Sub

    Private Sub SalesMarketingDashboard_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        RadPageView1.SelectedPage = RadPageViewPage1
        Addnew()
        RadPageViewPage4.Visible = False
        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        UnionWiseDemand()
        RouteAndBoothWiseDemand()
        'ItemWiseDemand()
    End Sub
    Sub UnionWiseDemand()
        Try
            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL') "
            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If


                    query += " select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                     ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,
                    ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS Dis_FATKG,
                    ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS Dis_SNFKG,
                    ISNULL(SUM(DEMANDINKG.DEMANDQTYKG), 0) AS DEMANDQTYKG,
                    ISNULL(SUM(DEMANDINKG.FATKGPRODUCT), 0) AS FATKGPRODUCT,
                    ISNULL(SUM(DEMANDINKG.SNFKGPRODUCT), 0) AS SNFKGPRODUCT
					FROM
					(SELECT 
                        SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                        SUM(FATKGDemand) AS FATKGDemand,
                        SUM(SNFKGDemand) AS SNFKGDemand
						
                    FROM (
                    (SELECT 
                        SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                        SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                          AND dbm.Posted=1
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand
						
                from (
                SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,0 as Route_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                             AND TSPL_Dispatch_BulkSale.Posted=1  
                )xxxx ))Disp_BUlksale
                    ) AS Dis_Demand,

					

					(SELECT 
                        SUM(DEMANDQTYKG) AS DEMANDQTYKG,
                        SUM(FATKGPRODUCT) AS FATKGPRODUCT,
                        SUM(SNFKGPRODUCT) AS SNFKGPRODUCT
                    FROM (
                    (SELECT 
                        SUM(CASE 
                                WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                                WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                                WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                                ELSE 0 
                            END) AS DEMANDQTYKG,
							                        SUM(ISNULL((CASE 
                                WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                                WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                                WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                                ELSE 0 
                            END )* im.STD_FatPer / 100,0)) AS FATKGPRODUCT,
							    SUM(ISNULL((CASE 
                                WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                                WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
								WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                                WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                                ELSE 0 
                            END )* im.STD_SNFPer / 100,0)) AS SNFKGPRODUCT
							
							--,
                       -- SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        --SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
						LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='POUCH') AS POUCHCONVER ON POUCHCONVER.Item_code = dbd.Item_Code
						LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='BOTTLE') AS BOTTLECONVER ON BOTTLECONVER.Item_code = dbd.Item_Code
						LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='CUP') AS CUPCONVER ON CUPCONVER.Item_code = dbd.Item_Code
						LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='PACK') AS PACKCONVER ON PACKCONVER.Item_code = dbd.Item_Code
						LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='KG') AS KGCONVER ON KGCONVER.Item_code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 1
                        AND im.Is_Ambient = 1
                            AND dbm.Posted=1
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS DEMANDQTYKG,
                        SUM(xxxx.Fat_KG ) AS FATKGPRODUCT,
                        SUM(xxxx.SNF_KG) AS SNFKGPRODUCT
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='KG'  
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code
                    WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                              and Is_Ambient=1 and IsTaxable=1  AND TSPL_Dispatch_BulkSale.Posted=1      
                    )xxxx ))Disp_BUlksale
                    ) AS DEMANDINKG)FINAL "
                Next
            End If
            'query = "select * from (" + query + ")xx "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat1()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
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
        gv1.Columns("SNo").IsVisible = True '

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 500
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 500
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 500
        gv1.Columns("Todate").IsVisible = False



        gv1.Columns("username").HeaderText = "User Name"
        gv1.Columns("username").Width = 200
        gv1.Columns("username").IsVisible = False

        gv1.Columns("DEMAND_INLTR").HeaderText = "Milk Demand Qty"
        gv1.Columns("DEMAND_INLTR").FormatString = "{0:n3}"

        gv1.Columns("Dis_FATKG").HeaderText = " Milk FAT KG"
        gv1.Columns("Dis_FATKG").IsVisible = True
        gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"


        gv1.Columns("Dis_SNFKG").HeaderText = "Milk SNF KG"
        gv1.Columns("Dis_SNFKG").IsVisible = True
        gv1.Columns("Dis_SNFKG").FormatString = ""

        gv1.Columns("DEMANDQTYKG").HeaderText = "Product Demand QTY"
        gv1.Columns("DEMANDQTYKG").IsVisible = True
        gv1.Columns("DEMANDQTYKG").FormatString = "{0:n3}"

        gv1.Columns("FATKGPRODUCT").HeaderText = "Product FAT KG"
        gv1.Columns("FATKGPRODUCT").IsVisible = True
        gv1.Columns("FATKGPRODUCT").FormatString = "{0:n3}"


        gv1.Columns("SNFKGPRODUCT").HeaderText = "Product SNF KG"
        gv1.Columns("SNFKGPRODUCT").IsVisible = True
        gv1.Columns("SNFKGPRODUCT").FormatString = "{0:n3}"

        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        ' View()
    End Sub
    Sub RouteAndBoothWiseDemand()
        Try
            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL') "
            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If


                    query += " select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                     
	    Max(Dis_Demand.route_no) AS route_no,
	   MAx(dis_demand.booth) as Booth,
       ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,
       ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS Dis_FATKG,
       ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS Dis_SNFKG,
      
       ISNULL(SUM(DEMANDINKG.DEMANDQTYKG), 0) AS DEMANDQTYKG,
       ISNULL(SUM(DEMANDINKG.FATKGPRODUCT), 0) AS FATKGPRODUCT,
       ISNULL(SUM(DEMANDINKG.SNFKGPRODUCT), 0) AS SNFKGPRODUCT
FROM
(
    SELECT SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
           SUM(FATKGDemand) AS FATKGDemand,
           SUM(SNFKGDemand) AS SNFKGDemand,
        count(distinct route_no)route_no,
	   count(distinct Booth)Booth
    FROM 
    (
        SELECT SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
               SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
               SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand,

              dbm.route_no,
			  dbd.Cust_Code AS Booth
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
          AND im.IsTaxable = 0
          AND im.Is_FreshItem = 1
		  and dbm.Posted=1
        GROUP BY dbm.route_no,dbd.Cust_Code
        
        UNION ALL
        
        SELECT SUM(xxxx.Qty) AS TotalLtr_ItemWiseDemand,
               SUM(xxxx.Fat_KG) AS FATKGDemand,
               SUM(xxxx.SNF_KG) AS SNFKGDemand,
         NULL AS route_no,
		 null as Booth
        FROM 
        (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,
                   NULL AS route_no,
				   null as Booth
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'LTR'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx
    ) Dis_Demand
) Dis_Demand,

(
    SELECT SUM(DEMANDQTYKG) AS DEMANDQTYKG,
           SUM(FATKGPRODUCT) AS FATKGPRODUCT,
           SUM(SNFKGPRODUCT) AS SNFKGPRODUCT
		  
    FROM 
    (
        SELECT SUM(CASE 
                       WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                       WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                       ELSE 0 
                   END) AS DEMANDQTYKG,
               SUM(ISNULL((CASE 
                               WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                               ELSE 0 
                           END) * im.STD_FatPer / 100, 0)) AS FATKGPRODUCT,
               SUM(ISNULL((CASE 
                               WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                               ELSE 0 
                           END) * im.STD_SNFPer / 100, 0)) AS SNFKGPRODUCT
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='POUCH') AS POUCHCONVER ON POUCHCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='BOTTLE') AS BOTTLECONVER ON BOTTLECONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='CUP') AS CUPCONVER ON CUPCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='PACK') AS PACKCONVER ON PACKCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='KG') AS KGCONVER ON KGCONVER.Item_code = dbd.Item_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
          AND im.IsTaxable = 1
          AND im.Is_Ambient = 1
       and dbm.Posted=1
        
        UNION ALL
        
        SELECT SUM(xxxx.Qty) AS DEMANDQTYKG,
               SUM(xxxx.Fat_KG) AS FATKGPRODUCT,
               SUM(xxxx.SNF_KG) AS SNFKGPRODUCT
			   
   
        FROM 
        (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'KG'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx
        
    ) DEMANDINKG
  
) DEMANDINKG )Final

 "
                Next
            End If
            'query = "select * from (" + query + ")xx "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                gv2.DataSource = Nothing
                gv2.Rows.Clear()
                gv2.Columns.Clear()
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.MasterView.Refresh()
                gv2.DataSource = dt2
                For ii As Integer = 0 To gv2.Columns.Count - 1
                    gv2.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.EnableFiltering = True
                SetGridFormat2()
                gv2.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat2()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        gv2.AutoExpandGroups = True
        gv2.ShowGroupPanel = True
        gv2.ShowRowHeaderColumn = False
        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = False
        gv2.EnableFiltering = True
        gv2.ShowFilteringRow = True


        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).BestFit()
        Next
        gv2.Columns("SNo").Name = "SNo"
        gv2.Columns("SNo").IsVisible = True '

        gv2.Columns("Union Name").HeaderText = "Union Name"
        gv2.Columns("Union Name").Width = 500
        gv2.Columns("Union Name").IsVisible = True

        gv2.Columns("Fromdate").HeaderText = "From Date"
        gv2.Columns("Fromdate").Width = 500
        gv2.Columns("Fromdate").IsVisible = False

        gv2.Columns("Todate").HeaderText = "To Date"
        gv2.Columns("Todate").Width = 500
        gv2.Columns("Todate").IsVisible = False



        gv2.Columns("username").HeaderText = "User Name"
        gv2.Columns("username").Width = 200
        gv2.Columns("username").IsVisible = False

        gv2.Columns("route_no").HeaderText = "No of Routes"
        gv2.Columns("route_no").Width = 200

        gv2.Columns("Booth").HeaderText = " No of Booths"
        gv2.Columns("Booth").IsVisible = True


        gv2.Columns("DEMAND_INLTR").HeaderText = "Milk Demand Qty"
        gv2.Columns("DEMAND_INLTR").FormatString = "{0:n3}"

        gv2.Columns("Dis_FATKG").HeaderText = " Milk FAT KG"
        gv2.Columns("Dis_FATKG").IsVisible = True
        gv2.Columns("Dis_FATKG").FormatString = "{0:n3}"


        gv2.Columns("Dis_SNFKG").HeaderText = "Milk SNF KG"
        gv2.Columns("Dis_SNFKG").IsVisible = True
        gv2.Columns("Dis_SNFKG").FormatString = "{0:n3}"

        gv2.Columns("DEMANDQTYKG").HeaderText = "Product Demand QTY"
        gv2.Columns("DEMANDQTYKG").IsVisible = True
        gv2.Columns("DEMANDQTYKG").FormatString = "{0:n3}"

        gv2.Columns("FATKGPRODUCT").HeaderText = "Product FAT KG"
        gv2.Columns("FATKGPRODUCT").IsVisible = True
        gv2.Columns("FATKGPRODUCT").FormatString = "{0:n3}"


        gv2.Columns("SNFKGPRODUCT").HeaderText = "Product SNF KG"
        gv2.Columns("SNFKGPRODUCT").IsVisible = True
        gv2.Columns("SNFKGPRODUCT").FormatString = "{0:n3}"

        gv2.ShowGroupPanel = True
        gv2.MasterTemplate.AutoExpandGroups = True

        ' View()
    End Sub
    Sub ExportGrids3()
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv3, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub ExportGrid2()
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv2, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub ExportGrid1()
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                ' arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        ExportGrid1()
        ExportGrid2()
        ' ExportGrids3()
    End Sub
    Private Sub ExportGridgv1(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.SalesMarketingDashboard & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridgv2(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.SalesMarketingDashboard & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv2, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridgv3(ByVal exporter As EnumExportTo)
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.SalesMarketingDashboard & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv3, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub ItemWiseDemand()
        Try
            gv3.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strCreateConv As String = ""
            Dim strIsMilkPouch As String = ""
            Dim strGrandTotalWithoutScheme As String = ""
            'Dim arrItem As String()
            Dim strItem2WithSum As String = ""
            Dim strItem2 As String = ""
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
        SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL') "
            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For jj As Integer = 0 To dt.Rows.Count - 1
                    Dim strDate As String = "Document_Date"
                    strWhrClause2 = " and convert(date, [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
                    strWhrRoutSummaryPrint = "  and convert(date, [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'' "
                    Dim ItemInUse As String = ""
                    ItemInUse = " [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL Left Outer Join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER On [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No = [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No Left Outer Join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER On [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER On [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER On [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER.Vehicle_Id = [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER On [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER.Location_Code = [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.location_code left outer join [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER on [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=[" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Group_Code where ([" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name !='' or [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name is null) "
                    If isSchemeItem = False Then
                        ItemInUse += " and Scheme_Item='N' "
                    End If
                    ItemInUse += "  and [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1 "
                    ItemInUse += "  or [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 "
                    Dim strAliasCol As String = "( [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name )"
                    ItemInUse += strWhrClause2
                    Dim strItem As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

                    strItem2 += clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'arrItem = strItem2.Split(",")
                    Dim strItmeHeadingScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( " + strAliasCol + ") +' as ' + QUOTENAME( " + strAliasCol + "+'(S)')  as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    Dim strSumItemOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    Dim strSumItemSchemeOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    Dim strGrandTotal As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol + "+'(S)') +',0))'  as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    strGrandTotalWithoutScheme = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                    Dim TempItemInUse As String = ItemInUse + " group by [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name,[" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Sku_Seq order by [" + clsCommon.myCstr(dt.Rows(jj).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Sku_Seq "
                    'strItem2WithSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + TempItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                    strItem2WithSum += clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT ',' +'Sum(' + QUOTENAME( " + strAliasCol + ") +')' + ' as ' + QUOTENAME( " + strAliasCol + ") as Alies_Name FROM " + TempItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                Next
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    itemCode = " and 2=2 "
                    Dim strDate As String = "Document_Date"
                    strWhrClause = "  and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'' "
                    strWhrClause2 = " and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
                    strWhrRoutSummaryPrint = "  and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) >= ''" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER." + strDate + ",103) <= ''" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'' "
                    Dim ItemInUse As String = ""
                    ItemInUse = " [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER.Vehicle_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER.Location_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.location_code left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Group_Code where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name is null) "
                    If isSchemeItem = False Then
                        ItemInUse += " and Scheme_Item='N' "
                    End If
                    ItemInUse += "  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1 "
                    ItemInUse += "  or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 "
                    Dim strAliasCol As String = "( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name )"
                    ItemInUse += strWhrClause2
                    Dim strSchemeItem As String = Nothing
                    strSchemeItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                    If String.IsNullOrEmpty(strSchemeItem) Then
                        clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                        Exit Sub
                    End If

                    strCreateConv = " convert (decimal(18,2), [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) "
                    strIsMilkPouch = "  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1 "

                    MainQuery = " select  Document_No as [Document No],max(Document_Date) as [Document Date],max([Time])as [Time],[DO No],[Short Close],max ([Dispatch No(NT)]) as [Dispatch No(NT)] ,max( [Invoice No(NT)]) as [Invoice No(NT)],max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)], max(Booking_Type) as [Booking Type], max(BookingThrough) as [Booking Through], Case when [Group] in ('Distributor','Other') Or max(AgainstGatePass) =1 then 'NA' else case when  max(TruckSheetGenerate) =1 then 'Yes' else 'No' end end as TruckSheetGenerate, Case when  max(AgainstGatePass) =1 then 'Yes' else '' end as AgainstGatePass,Case when max(is_Cancelled) =1 then 'Yes' else '' end Cancelled,max(Payment_Mode) as [Payment Mode],[VEHICLE NO],max([Customer Code]) as [Customer Code],[WdName] as [Customer Name],max(Booth) as Booth,[Group],[Cust Group Desc],[Customer Category Code],[Zone],[Route No], max([Booking Time(AM/PM)]) as [Booking Time(AM/PM)],Max(Created_By) as [Created By],max(Created_Date) as [Created Date],max(Modified_By) as [Modified By],max(Modified_Date) as [Modified Date],sum(DocumentAmount) as [Amount], max(isnull (Scheme_Booking_Qty,0)) as [Scheme Qty] , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Time]) as [Time]  ,zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,sum(Amount_with_Tax) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No, Convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Booking_Type,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Payment_Mode,( format ( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Created_By,convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Amount_with_Tax,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Scheme_Item, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Sku_Seq, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Location_Code, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER.Location_Desc,isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Customer_Name As WdName, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code as Item_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Unit_code as UOM,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.route_no as [Route No] , [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Alies_Name As [Description] ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER.Description [Lorry_No],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Group_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_Date As [Order Date],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VEHICLE_MASTER.Vehicle_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_LOCATION_MASTER.Location_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.location_code left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                              " Left Outer Join ( Select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                              " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No " &
                                              "  Left Outer Join ( Select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                              "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No " &
                                              "  left Outer Join (Select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL where  " &
                                              " Scheme_Item = 'Y' Group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No )TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No  LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No  " &
                                              " left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                              "  where 2=2  " + strIsMilkPouch + "  " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_No,zpivot.[VEHICLE NO],zpivot.[WdName],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Customer Category Code],zpivot.[Zone],zpivot.[Route No],zpivot.[DO NO],zpivot.[Short Close] "

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username, " + strItem2WithSum + " from (select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                                                  ,  XXXFinal.[Customer Name] asc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], " + strItem2WithSum + " ,sum([Grand Total]) as [Grand Total], max( XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By]  from ( " + MainQuery + " ) XXXFinal left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]	 group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name] )ttt "

                Next
            End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            gv3.DataSource = dtgv
            gv3.GroupDescriptors.Clear()
            gv3.MasterTemplate.SummaryRowsBottom.Clear()
            gv3.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim item As Integer = 0
            If gv3.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = item To gv3.Columns.Count - 1
                    Dim aa = gv3.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    If clsCommon.CompairString(aa, "Modified By") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(aa, "Created By") <> CompairStringResult.Equal Then
                        summaryRowItem.Add(item1)
                        gv3.Columns(i).FormatString = "{0:n2}"
                    End If
                Next
                gv3.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv3.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
            For i As Integer = item To gv3.Columns.Count - 5
                Dim grandTotal As Decimal = 0
                For j As Integer = 0 To gv3.Rows.Count - 1
                    Dim columnValue As Object = String.Empty
                    columnValue = gv3.Rows(j).Cells(i).Value
                    If (Not IsDBNull(gv3.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                        grandTotal = grandTotal + clsCommon.myCdbl(gv3.Rows(j).Cells(i).Value)
                    End If
                Next
                If (clsCommon.myCdbl(grandTotal) > 0) Then
                    gv3.Columns(i).IsVisible = True
                Else
                    gv3.Columns(i).IsVisible = False
                End If
            Next
            'Try
            '    Dim strItemFatch() As String = strItem2.Split(",")
            '    For count As Integer = 0 To strItemFatch.Length - 1
            '        Dim strCode As String = strItemFatch(count)
            '        Dim strCode2 As String = Replace(strItemFatch(count), "[", "")
            '        strCode2 = Replace(strCode2, "]", "")
            '        Dim sum As Integer = clsCommon.myCdbl(dtgv.Compute("SUM(" + strCode + ")", String.Empty))
            '        If gv3.Columns.Contains(strCode2) Then
            '            If sum > 0 Then
            '                gv3.Columns(strCode2).IsVisible = True
            '            Else
            '                gv3.Columns(strCode2).IsVisible = False
            '            End If
            '        End If
            '    Next
            'Catch ex As Exception
            'End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

End Class