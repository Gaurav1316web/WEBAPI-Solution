Imports common
Public Class SalesMarketingDashboard
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Addnew()
    End Sub
    Sub Addnew()
        ' txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        'txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        'txtUnion.arrValueMember = Nothing
        'cmbReportType.SelectedIndex = 0
        EnableDisableCtrl(True)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGridgv(EnumExportTo.Excel)
    End Sub

    Private Sub SalesMarketingDashboard_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        RadPageView1.SelectedPage = RadPageViewPage1
        Addnew()
        'RadPageViewPage4.Visible = False
        'RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If cmbReportType.SelectedIndex = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Report Type.")
        ElseIf cmbReportType.SelectedIndex = 1 Then
            UnionWiseDemand()
            'EnableDisableCtrl(False)
        ElseIf cmbReportType.SelectedIndex = 2 Then
            RouteAndBoothWiseDemand()
            'EnableDisableCtrl(False)
        Else
            ItemWiseDemand()
            'EnableDisableCtrl(False)
        End If
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
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

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    'Divya

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,max(Union_Contact_Person) as Union_Contact_Person,max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
                     ISNULL(SUM(TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,
                    ISNULL(SUM(FATKGDemand), 0) AS Dis_FATKG,
                    ISNULL(SUM(SNFKGDemand), 0) AS Dis_SNFKG,
                    ISNULL(SUM(DEMANDQTYKG), 0) AS DEMANDQTYKG,
                    ISNULL(SUM(FATKGPRODUCT), 0) AS FATKGPRODUCT,
                    ISNULL(SUM(SNFKGPRODUCT), 0) AS SNFKGPRODUCT
					FROM
					( 
----1st
SELECT   SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand,max(tspl_company_master.Union_Contact_Person) as Union_Contact_Person,
						max(tspl_company_master.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,0 AS DEMANDQTYKG,0 AS FATKGPRODUCT,0 AS SNFKGPRODUCT
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                        	left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_company_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_company_master.Comp_Code=im.Comp_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                          AND dbm.Posted=1

                        ------2nd

                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand,
                        	'' as Union_Contact_Person,
						'' as Union_Contact_PhoneNo,0 AS DEMANDQTYKG,0 AS FATKGPRODUCT,0 AS SNFKGPRODUCT
                from (
                SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,0 as Route_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                             AND TSPL_Dispatch_BulkSale.Posted=1  
                )xxxx 

				---3rd
				UNION ALL
				SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,
               0 AS SNFKGDemand, max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person, max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
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
							
		                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
						  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
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
							---4th

                        union all
                        SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,
               0 AS SNFKGDemand, max(Union_Contact_Person) as Union_Contact_Person, max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS DEMANDQTYKG,
                        SUM(xxxx.Fat_KG ) AS FATKGPRODUCT,
                        SUM(xxxx.SNF_KG) AS SNFKGPRODUCT
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_COMPANY_MASTER.Union_Contact_Person
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='KG'  
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code
					  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
                    WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                              and Is_Ambient=1 and IsTaxable=1  AND TSPL_Dispatch_BulkSale.Posted=1      
                    )xxxx 
					
					-----5TH 

	union all

	 SELECT 0 as TotalLtr_ItemWiseDemand, 0 AS FATKGDemand,
              0 AS SNFKGDemand,
                        max(Union_Contact_Person) as Union_Contact_Person,
			     max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              SUM(xxxx.Qty) AS DEMANDQTYKG,
               SUM(xxxx.Fat_KG) AS FATKGPRODUCT,
               SUM(xxxx.SNF_KG) AS SNFKGPRODUCT			    
        FROM 
        (
            SELECT TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_ITEM_MASTER.STD_FatPer,TSPL_ITEM_MASTER.STD_SNFPer, CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   (Qty * STD_FatPer / 100) AS Fat_KG,
                   (Qty * STD_SNFPer / 100) as SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertDiv.UOM_Code = 'KG'
			        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=1
        ) xxxx )Disp_BUlksale"
                    'end
                Next
            End If
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
                EnableDisableCtrl(False)
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
        gv1.Columns("Union Name").Width = 200
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Union_Contact_Person").HeaderText = "Union Contact Person"
        gv1.Columns("Union_Contact_Person").Width = 200
        gv1.Columns("Union_Contact_Person").IsVisible = True

        gv1.Columns("Union_Contact_PhoneNo").HeaderText = "Union Contact PhoneNo"
        gv1.Columns("Union_Contact_PhoneNo").Width = 200
        gv1.Columns("Union_Contact_PhoneNo").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 200
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 200
        gv1.Columns("Todate").IsVisible = False



        gv1.Columns("username").HeaderText = "User Name"
        gv1.Columns("username").Width = 200
        gv1.Columns("username").IsVisible = False

        gv1.Columns("DEMAND_INLTR").HeaderText = "Milk Demand Qty"
        gv1.Columns("DEMAND_INLTR").FormatString = "{0:n2}"


        gv1.Columns("Dis_FATKG").HeaderText = " Milk FAT KG"
        gv1.Columns("Dis_FATKG").IsVisible = True
        gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"


        gv1.Columns("Dis_SNFKG").HeaderText = "Milk SNF KG"
        gv1.Columns("Dis_SNFKG").IsVisible = True
        gv1.Columns("Dis_SNFKG").FormatString = "{0:n3}"


        gv1.Columns("DEMANDQTYKG").HeaderText = "Product Demand QTY"
        gv1.Columns("DEMANDQTYKG").IsVisible = True
        gv1.Columns("DEMANDQTYKG").FormatString = "{0:n2}"


        gv1.Columns("FATKGPRODUCT").HeaderText = "Product FAT KG"
        gv1.Columns("FATKGPRODUCT").IsVisible = True
        gv1.Columns("FATKGPRODUCT").FormatString = "{0:n3}"



        gv1.Columns("SNFKGPRODUCT").HeaderText = "Product SNF KG"
        gv1.Columns("SNFKGPRODUCT").IsVisible = True
        gv1.Columns("SNFKGPRODUCT").FormatString = "{0:n3}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item3 As New GridViewSummaryItem("DEMAND_INLTR", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Dis_FATKG", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Dis_SNFKG", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("DEMANDQTYKG", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("FATKGPRODUCT", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("SNFKGPRODUCT", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    'Divya---

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,max(Union_Contact_Person) as Union_Contact_Person,
						max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,count(distinct route_no) AS route_no,count(distinct Booth)Booth,ISNULL(SUM(TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,ISNULL(SUM(FATKGDemand), 0) AS Dis_FATKG,
                        ISNULL(SUM(SNFKGDemand), 0) AS Dis_SNFKG,ISNULL(SUM(DEMANDQTYKG), 0) AS DEMANDQTYKG,ISNULL(SUM(FATKGPRODUCT), 0) AS FATKGPRODUCT,ISNULL(SUM(SNFKGPRODUCT), 0) AS SNFKGPRODUCT FROM ( SELECT SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand,max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person,max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              dbm.route_no,dbd.Cust_Code AS Booth,0 as DEMANDQTYKG, 0 as FATKGPRODUCT,0 as SNFKGPRODUCT
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' AND im.IsTaxable = 0 AND im.Is_FreshItem = 1 and dbm.Posted=1
        GROUP BY dbm.route_no,dbd.Cust_Code
		---2ND
		UNION ALL
		SELECT SUM(xxxx.Qty) AS TotalLtr_ItemWiseDemand,SUM(xxxx.Fat_KG) AS FATKGDemand,SUM(xxxx.SNF_KG) AS SNFKGDemand,'' as Union_Contact_Person,'' as Union_Contact_PhoneNo,NULL AS route_no,
		 null as Booth,0 as DEMANDQTYKG, 0 as FATKGPRODUCT,0 as SNFKGPRODUCT FROM  (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,NULL AS route_no,null as Booth
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'LTR'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx

		-----3RD
		UNION ALL 
		 SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,0 AS SNFKGDemand,max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person,max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              dbm.route_no,dbd.Cust_Code AS Booth,SUM(CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                       WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'KG' THEN dbd.qty  ELSE 0  END) AS DEMANDQTYKG,
               SUM(ISNULL((CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                               ELSE 0  END) * im.STD_FatPer / 100, 0)) AS FATKGPRODUCT,
               SUM(ISNULL((CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  ELSE 0 END) * im.STD_SNFPer / 100, 0)) AS SNFKGPRODUCT
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='POUCH') AS POUCHCONVER ON POUCHCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='BOTTLE') AS BOTTLECONVER ON BOTTLECONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='CUP') AS CUPCONVER ON CUPCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='PACK') AS PACKCONVER ON PACKCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='KG') AS KGCONVER ON KGCONVER.Item_code = dbd.Item_Code
		        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
          AND im.IsTaxable = 1 AND im.Is_Ambient = 1 and dbm.Posted=1 GROUP BY dbm.route_no,dbd.Cust_Code
	   ---------4TH

	   union all 
	    SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand, 0 AS SNFKGDemand,'' as Union_Contact_Person,'' as Union_Contact_PhoneNo,NULL AS route_no,null as Booth, SUM(xxxx.Qty) AS DEMANDQTYKG,SUM(xxxx.Fat_KG) AS FATKGPRODUCT,
        SUM(xxxx.SNF_KG) AS SNFKGPRODUCT FROM (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor  END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'KG'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx
	-----5TH 

	union all
	 SELECT 0 as TotalLtr_ItemWiseDemand, 0 AS FATKGDemand,0 AS SNFKGDemand,max(Union_Contact_Person) as Union_Contact_Person,max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,Route_No,Cust_Code AS Booth,	 
	 SUM(xxxx.Qty) AS DEMANDQTYKG,SUM(xxxx.Fat_KG) AS FATKGPRODUCT,SUM(xxxx.SNF_KG) AS SNFKGPRODUCT FROM 
        (
            SELECT TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Route_No,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_ITEM_MASTER.STD_FatPer,TSPL_ITEM_MASTER.STD_SNFPer, CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,(Qty * STD_FatPer / 100) AS Fat_KG,(Qty * STD_SNFPer / 100) as SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertDiv.UOM_Code = 'KG'
			        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=1
        ) xxxx GROUP BY Route_No,Cust_Code
		 ) Final "
                    'end---
                Next
            End If
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
                SetGridFormat2()
                gv1.BestFitColumns()
                EnableDisableCtrl(False)
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
        gv1.Columns("Union Name").Width = 200
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 200
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 200
        gv1.Columns("Todate").IsVisible = False



        gv1.Columns("username").HeaderText = "User Name"
        gv1.Columns("username").Width = 200
        gv1.Columns("username").IsVisible = False

        gv1.Columns("route_no").HeaderText = "No of Routes"
        gv1.Columns("route_no").Width = 200


        gv1.Columns("Booth").HeaderText = " No of Booths"
        gv1.Columns("Booth").IsVisible = True



        gv1.Columns("DEMAND_INLTR").HeaderText = "Milk Demand Qty"
        gv1.Columns("DEMAND_INLTR").FormatString = "{0:n2}"


        gv1.Columns("Dis_FATKG").HeaderText = " Milk FAT KG"
        gv1.Columns("Dis_FATKG").IsVisible = True
        gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"



        gv1.Columns("Dis_SNFKG").HeaderText = "Milk SNF KG"
        gv1.Columns("Dis_SNFKG").IsVisible = True
        gv1.Columns("Dis_SNFKG").FormatString = "{0:n3}"


        gv1.Columns("DEMANDQTYKG").HeaderText = "Product Demand QTY"
        gv1.Columns("DEMANDQTYKG").IsVisible = True
        gv1.Columns("DEMANDQTYKG").FormatString = "{0:n2}"


        gv1.Columns("FATKGPRODUCT").HeaderText = "Product FAT KG"
        gv1.Columns("FATKGPRODUCT").IsVisible = True
        gv1.Columns("FATKGPRODUCT").FormatString = "{0:n3}"


        gv1.Columns("SNFKGPRODUCT").HeaderText = "Product SNF KG"
        gv1.Columns("SNFKGPRODUCT").IsVisible = True
        gv1.Columns("SNFKGPRODUCT").FormatString = "{0:n3}"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("route_no", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Booth", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("DEMAND_INLTR", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Dis_FATKG", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Dis_SNFKG", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("DEMANDQTYKG", "{0:f2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("FATKGPRODUCT", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("SNFKGPRODUCT", "{0:f3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True
        ' View()
    End Sub
    Sub ExportGrid()
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
        ExportGrid()
    End Sub
    Private Sub ExportGridgv(ByVal exporter As EnumExportTo)
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
    Sub ItemWiseDemand()
        Try
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strCreateConv As String = ""
            Dim strIsMilkPouch As String = ""
            Dim strGrandTotalWithoutScheme As String = ""
            'Dim arrItem As String()
            Dim strItem2WithSum As String = ""
            Dim strItem2 As String = ""
            Dim itemdesc As String = ""
            Dim sumitemdesc As String = ""
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            Dim query As String
            Dim FinalQry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            Dim arrUnion As ArrayList = New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            Else
                dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            End If
            query = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim dt1 As DataTable = Nothing
                Dim strqry As String = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        strqry += " UNION  "
                    End If
                    strqry += "SELECT  max(report_name)Short_Description,MAX(Item_SNO)Item_SNO,max(Report_UOM)Report_UOM FROM (
                                    SELECT    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code,Item_Code.report_name FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL 
                                    Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                                    inner join (Select Item_Code,Report_Name from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER
                                    where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) 
                                    and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 ) As Item_Code on Item_Code.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                    where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
" & Environment.NewLine & " UNION ALL " & Environment.NewLine & " 
									SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.report_name FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code where  ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0  and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
            )XX inner join (SELECT Alias_Name,SNO AS Item_SNO,code,Report_UOM FROM [TSPL_MASTER].[dbo].TSPL_Item_Alias_Master) As Item_Alias_Master on Item_Alias_Master.Alias_Name = XX.Report_Name
                   WHERE xx.Report_Name<>'' group by Item_Code "
                Next
                dt1 = clsDBFuncationality.GetDataTable(" SELECT * FROM  (" & strqry & "  )XXX order by Item_SNO")
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For kk As Integer = 0 To dt1.Rows.Count - 1
                        If clsCommon.myLen(itemdesc) > 0 Then
                            itemdesc += ",[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc += ",Sum(IsNull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc += ",(IsNull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            sumitemdesc += ",(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "(" + clsCommon.myCstr(dt1.Rows(kk)("Report_UOM")) + ")] "
                        Else
                            itemdesc = "[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc = "Sum(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "] "
                            'sumitemdesc = "(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "] "
                            sumitemdesc = "(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "(" + clsCommon.myCstr(dt1.Rows(kk)("Report_UOM")) + ")] "
                        End If
                    Next
                Else

                End If

            End If

            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                Else
                    query += " ( "
                End If

                query += " select xxxx.Item_Code,Short_Description,Item_SNO,Qty,Unit_code,xxxx.Report_UOM,(QTY * ConFromUom.Conversion_Factor)/ConToUom.Conversion_Factor as Report_Uom_Qty,[Union Name] from (
                             SELECT  ITEM_CODE, (report_name)Short_Description,MAX(Item_SNO)Item_SNO,sum(qty)Qty,MAX(Unit_code)Unit_code,max(Report_UOM)Report_UOM,max([Union Name]) [Union Name]
                            FROM  ( SELECT * FROM ( SELECT '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code,Item_Code.report_name,
                                  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL 
									Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No 
									inner join (Select Item_Code,Report_Name from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER 
                                    where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) 
                                    and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 ) As Item_Code on Item_Code.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                    where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                    and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) 

									UNION ALL 

									SELECT '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_name ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code
									FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
					where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null)   and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0  
                    and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                    and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)  
                        ) XX 
						inner join ( SELECT Alias_Name,SNO AS Item_SNO,Report_UOM FROM [TSPL_MASTER].[dbo].TSPL_Item_Alias_Master) As Item_Alias_Master on Item_Alias_Master.Alias_Name = XX.Report_Name
						 WHERE ISNULL(xx.Report_Name,'') <>''   ) XXX 
									  group by xxx.Report_Name, XXX.Item_Code 
									  ) xxxx 
									  LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConFromUom ON ConFromUom.Item_Code=xxxx.Item_Code AND ConFromUom.UOM_Code=xxxx.Unit_code
								      LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConToUom  ON ConToUom.Item_Code=xxxx.Item_Code and ConToUom.UOM_Code=xxxx.Report_UOM  "


                'query += " select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],max(Union_Contact_Person)Union_Contact_Person,max(Union_Contact_PhoneNo)Union_Contact_PhoneNo,
                '            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username, " + sumitemdesc + " from (select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                '                                  ,  XXXFinal.[Customer Name] asc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], " + sumitemdesc + "  ,max( XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By],max(Union_Contact_Person)Union_Contact_Person,max(Union_Contact_PhoneNo)Union_Contact_PhoneNo  from ( " + MainQuery + " ) XXXFinal left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]	 group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name] )ttt "
                'query += " "
            Next

            FinalQry = " SELECT CAST(ROW_NUMBER() OVER(ORDER BY ([Union Name])) AS INT) AS SNo,([Union Name]) as [Union Name],TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo," + sumitemdesc + " FROM
                        (select [Union Name],Short_Description,Sum(Report_Uom_Qty)Report_Uom_Qty 
                        From  " + query + " )  AS SourceData group by [Union Name],Short_Description )Final
                            PIVOT (SUM(Report_UOM_Qty) FOR Short_Description IN (" + itemdesc + ") ) AS PVT 
                            Left join TSPL_COMPANY_MASTER on 2=2"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(FinalQry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dtgv
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.BestFitColumns()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            EnableDisableCtrl(False)
            Dim item As Integer = 2
            If gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = item To gv1.Columns.Count - 1
                    Dim aa = gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    If clsCommon.CompairString(aa, "Modified By") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(aa, "Created By") <> CompairStringResult.Equal Then
                        summaryRowItem.Add(item1)
                        gv1.Columns(i).FormatString = "{0:n2}"
                    End If
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
            For i As Integer = item To gv1.Columns.Count - 5
                Dim grandTotal As Decimal = 0
                For j As Integer = 0 To gv1.Rows.Count - 1
                    Dim columnValue As Object = String.Empty
                    columnValue = gv1.Rows(j).Cells(i).Value
                    If (Not IsDBNull(gv1.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                        grandTotal = grandTotal + clsCommon.myCdbl(gv1.Rows(j).Cells(i).Value)
                    End If
                Next
                If (clsCommon.myCdbl(grandTotal) > 0) Then
                    gv1.Columns(i).IsVisible = True
                Else
                    gv1.Columns(i).IsVisible = False
                End If
            Next
            'Try
            '    Dim strItemFatch() As String = strItem2.Split(",")
            '    For count As Integer = 0 To strItemFatch.Length - 1
            '        Dim strCode As String = strItemFatch(count)
            '        Dim strCode2 As String = Replace(strItemFatch(count), "[", "")
            '        strCode2 = Replace(strCode2, "]", "")
            '        Dim sum As Integer = clsCommon.myCdbl(dtgv.Compute("SUM(" + strCode + ")", String.Empty))
            '        If gv1.Columns.Contains(strCode2) Then
            '            If sum > 0 Then
            '                gv1.Columns(strCode2).IsVisible = True
            '            Else
            '                gv1.Columns(strCode2).IsVisible = False
            '            End If
            '        End If
            '    Next
            'Catch ex As Exception
            'End Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Sub UnionWiseDemandPrint()
        Try
            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    'Divya

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,max(Union_Contact_Person) as Union_Contact_Person,max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
                     ISNULL(SUM(TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,
                    ISNULL(SUM(FATKGDemand), 0) AS Dis_FATKG,
                    ISNULL(SUM(SNFKGDemand), 0) AS Dis_SNFKG,
                    ISNULL(SUM(DEMANDQTYKG), 0) AS DEMANDQTYKG,
                    ISNULL(SUM(FATKGPRODUCT), 0) AS FATKGPRODUCT,
                    ISNULL(SUM(SNFKGPRODUCT), 0) AS SNFKGPRODUCT
					FROM
					( 
----1st
SELECT   SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand,max(tspl_company_master.Union_Contact_Person) as Union_Contact_Person,
						max(tspl_company_master.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,0 AS DEMANDQTYKG,0 AS FATKGPRODUCT,0 AS SNFKGPRODUCT
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                        	left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_company_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_company_master.Comp_Code=im.Comp_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                          AND dbm.Posted=1

                        ------2nd

                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand,
                        	'' as Union_Contact_Person,
						'' as Union_Contact_PhoneNo,0 AS DEMANDQTYKG,0 AS FATKGPRODUCT,0 AS SNFKGPRODUCT
                from (
                SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,0 as Route_No,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                             AND TSPL_Dispatch_BulkSale.Posted=1  
                )xxxx 

				---3rd
				UNION ALL
				SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,
               0 AS SNFKGDemand, max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person, max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
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
							
		                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
						  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
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
							---4th

                        union all
                        SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,
               0 AS SNFKGDemand, max(Union_Contact_Person) as Union_Contact_Person, max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS DEMANDQTYKG,
                        SUM(xxxx.Fat_KG ) AS FATKGPRODUCT,
                        SUM(xxxx.SNF_KG) AS SNFKGPRODUCT
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_COMPANY_MASTER.Union_Contact_Person
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='KG'  
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER on  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code
					  left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
                    WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                              and Is_Ambient=1 and IsTaxable=1  AND TSPL_Dispatch_BulkSale.Posted=1      
                    )xxxx 
					
					-----5TH 

	union all

	 SELECT 0 as TotalLtr_ItemWiseDemand, 0 AS FATKGDemand,
              0 AS SNFKGDemand,
                        max(Union_Contact_Person) as Union_Contact_Person,
			     max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              SUM(xxxx.Qty) AS DEMANDQTYKG,
               SUM(xxxx.Fat_KG) AS FATKGPRODUCT,
               SUM(xxxx.SNF_KG) AS SNFKGPRODUCT			    
        FROM 
        (
            SELECT TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_ITEM_MASTER.STD_FatPer,TSPL_ITEM_MASTER.STD_SNFPer, CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   (Qty * STD_FatPer / 100) AS Fat_KG,
                   (Qty * STD_SNFPer / 100) as SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertDiv.UOM_Code = 'KG'
			        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=1
        ) xxxx )Disp_BUlksale"
                    'end
                Next
            End If
            'query = "select * from (" + query + ")xx "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.UnionReports, dt2, "rptUnionWiseDemand", "Demand Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If cmbReportType.SelectedIndex = 1 Then
            UnionWiseDemandPrint()
        ElseIf cmbReportType.SelectedIndex = 2 Then
            RouteAndBoothWiseDemandPrint()
        ElseIf cmbReportType.SelectedIndex = 3 Then
            ItemWiseDemandPrint()
        End If
    End Sub

    Sub ItemWiseDemandPrint()
        Try
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strCreateConv As String = ""
            Dim strIsMilkPouch As String = ""
            Dim strGrandTotalWithoutScheme As String = ""
            'Dim arrItem As String()
            Dim strItem2WithSum As String = ""
            Dim strItem2 As String = ""
            Dim itemdesc As String = ""
            Dim sumitemdesc As String = ""
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            Dim strWhrRoutSummaryPrint As String = String.Empty
            Dim query As String
            Dim FinalQry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            Dim arrUnion As ArrayList = New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            Else
                dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            End If
            query = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim dt1 As DataTable = Nothing
                Dim strqry As String = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        strqry += " UNION  "
                    End If
                    strqry += "SELECT  max(report_name)Short_Description,MAX(Item_SNO)Item_SNO,max(Report_UOM)Report_UOM FROM (
                                    SELECT    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code,Item_Code.report_name FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL 
                                    Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                                    inner join (Select Item_Code,Report_Name from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER
                                    where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) 
                                    and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 ) As Item_Code on Item_Code.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                    where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
" & Environment.NewLine & " UNION ALL " & Environment.NewLine & " 
									SELECT [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.report_name FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code where  ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0  and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'
            )XX inner join (SELECT Alias_Name,SNO AS Item_SNO,code,Report_UOM FROM [TSPL_MASTER].[dbo].TSPL_Item_Alias_Master) As Item_Alias_Master on Item_Alias_Master.Alias_Name = XX.Report_Name
                   WHERE xx.Report_Name<>'' group by Item_Code "
                Next
                dt1 = clsDBFuncationality.GetDataTable(" SELECT * FROM  (" & strqry & "  )XXX order by Item_SNO")
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For kk As Integer = 0 To dt1.Rows.Count - 1
                        If clsCommon.myLen(itemdesc) > 0 Then
                            itemdesc += ",[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc += ",Sum(IsNull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc += ",(IsNull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            sumitemdesc += ",(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "(" + clsCommon.myCstr(dt1.Rows(kk)("Report_UOM")) + ")] "
                        Else
                            itemdesc = "[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            'sumitemdesc = "Sum(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "] "
                            'sumitemdesc = "(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "] "
                            sumitemdesc = "(isnull([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "(" + clsCommon.myCstr(dt1.Rows(kk)("Report_UOM")) + ")] "
                        End If
                    Next
                Else

                End If

            End If

            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                Else
                    query += " ( "
                End If

                query += " select xxxx.Item_Code,Short_Description,Item_SNO,Qty,Unit_code,xxxx.Report_UOM,(QTY * ConFromUom.Conversion_Factor)/ConToUom.Conversion_Factor as Report_Uom_Qty,[Union Name],Union_Contact_Person,Union_Contact_PhoneNo,Comp_Name from (
                             SELECT  ITEM_CODE, (report_name)Short_Description,MAX(Item_SNO)Item_SNO,sum(qty)Qty,MAX(Unit_code)Unit_code,max(Report_UOM)Report_UOM,max([Union Name]) [Union Name]
                            FROM  ( SELECT * FROM ( SELECT '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code,Item_Code.report_name,
                                  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Qty,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Unit_code FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL 
									Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER On [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No 
									inner join (Select Item_Code,Report_Name from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER
                                    where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null) 
                                    and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0 ) As Item_Code on Item_Code.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                                    where convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                                    and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'

									UNION ALL 

									SELECT '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_name ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code
									FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
					where ([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name !='' or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Report_Name is not null)   and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =1   or [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER.Is_Milk_Pouch =0  
                    and convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                    and  convert(date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)  
                        ) XX 
						inner join ( SELECT Alias_Name,SNO AS Item_SNO,Report_UOM FROM [TSPL_MASTER].[dbo].TSPL_Item_Alias_Master) As Item_Alias_Master on Item_Alias_Master.Alias_Name = XX.Report_Name
						 WHERE ISNULL(xx.Report_Name,'') <>''   ) XXX 
									  group by xxx.Report_Name, XXX.Item_Code 
									  ) xxxx 
									  LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConFromUom ON ConFromUom.Item_Code=xxxx.Item_Code AND ConFromUom.UOM_Code=xxxx.Unit_code
								      LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConToUom  ON ConToUom.Item_Code=xxxx.Item_Code and ConToUom.UOM_Code=xxxx.Report_UOM 
                                      Left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on 2=2"


                'query += " select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],max(Union_Contact_Person)Union_Contact_Person,max(Union_Contact_PhoneNo)Union_Contact_PhoneNo,
                '            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username, " + sumitemdesc + " from (select ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY  TSPL_ROUTE_MASTER.Route_Seq_No asc ,XXXFinal.[Route No] asc 
                '                                  ,  XXXFinal.[Customer Name] asc) as Sno,XXXFinal.[Route No],max(XXXFinal.[Customer Code]) as [Customer Code],  XXXFinal.[Customer Name], " + sumitemdesc + "  ,max( XXXFinal.[Modified By]) as [Modified By], max(XXXFinal.[Created By]) as  [Created By],max(Union_Contact_Person)Union_Contact_Person,max(Union_Contact_PhoneNo)Union_Contact_PhoneNo  from ( " + MainQuery + " ) XXXFinal left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ROUTE_MASTER.Route_No = XXXFinal.[Route No]	 group by route_seq_no, XXXFinal.[Route No],  XXXFinal.[Customer Name] )ttt "
                'query += " "
            Next

            FinalQry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,Union_Contact_Person,Union_Contact_PhoneNo,Comp_Name,CAST(ROW_NUMBER() OVER(ORDER BY ([Union Name])) AS INT) AS SNo,([Union Name]) as [Union Name],Report_Uom_Qty,Short_Description FROM
                        (select [Union Name],Short_Description + '(' + max(Report_UOM) + ')' AS Short_Description,Sum(Report_Uom_Qty)Report_Uom_Qty ,max(Union_Contact_Person)Union_Contact_Person,max(Union_Contact_PhoneNo)Union_Contact_PhoneNo,max(Comp_Name)Comp_Name 
                        From  " + query + " )  AS SourceData group by [Union Name],Short_Description )Final "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(FinalQry)
            If dtgv IsNot Nothing OrElse dtgv.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.UnionReports, dtgv, "rptItemWiseDetail", "Item Wise Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
            'clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
        End Try
    End Sub
    Sub RouteAndBoothWiseDemandPrint()
        Try

            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""

            '          If chkRJSBNS.Checked Then
            '              query += "union all
            'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            'union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '          End If
            dt = clsMilkUnion.UnionDBName1(txtUnion.arrValueMember)
            query = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    'Divya---

                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,max(Union_Contact_Person) as Union_Contact_Person,
						max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,count(distinct route_no) AS route_no,count(distinct Booth)Booth,ISNULL(SUM(TotalLtr_ItemWiseDemand), 0) AS DEMAND_INLTR,ISNULL(SUM(FATKGDemand), 0) AS Dis_FATKG,
                        ISNULL(SUM(SNFKGDemand), 0) AS Dis_SNFKG,ISNULL(SUM(DEMANDQTYKG), 0) AS DEMANDQTYKG,ISNULL(SUM(FATKGPRODUCT), 0) AS FATKGPRODUCT,ISNULL(SUM(SNFKGPRODUCT), 0) AS SNFKGPRODUCT FROM ( SELECT SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand,max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person,max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              dbm.route_no,dbd.Cust_Code AS Booth,0 as DEMANDQTYKG, 0 as FATKGPRODUCT,0 as SNFKGPRODUCT
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' AND im.IsTaxable = 0 AND im.Is_FreshItem = 1 and dbm.Posted=1
        GROUP BY dbm.route_no,dbd.Cust_Code
		---2ND
		UNION ALL
		SELECT SUM(xxxx.Qty) AS TotalLtr_ItemWiseDemand,SUM(xxxx.Fat_KG) AS FATKGDemand,SUM(xxxx.SNF_KG) AS SNFKGDemand,'' as Union_Contact_Person,'' as Union_Contact_PhoneNo,NULL AS route_no,
		 null as Booth,0 as DEMANDQTYKG, 0 as FATKGPRODUCT,0 as SNFKGPRODUCT FROM  (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,NULL AS route_no,null as Booth
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'LTR'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx

		-----3RD
		UNION ALL 
		 SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand,0 AS SNFKGDemand,max(TSPL_COMPANY_MASTER.Union_Contact_Person) as Union_Contact_Person,max(TSPL_COMPANY_MASTER.Union_Contact_PhoneNo) as Union_Contact_PhoneNo,
              dbm.route_no,dbd.Cust_Code AS Booth,SUM(CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                       WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                       WHEN dbd.Unit_code = 'KG' THEN dbd.qty  ELSE 0  END) AS DEMANDQTYKG,
               SUM(ISNULL((CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  
                               ELSE 0  END) * im.STD_FatPer / 100, 0)) AS FATKGPRODUCT,
               SUM(ISNULL((CASE WHEN dbd.Unit_code = 'POUCH' THEN dbd.qty * POUCHCONVER.Conversion_Factor / KGCONVER.Conversion_Factor 
                               WHEN dbd.Unit_code = 'BOTTLE' THEN dbd.qty * BOTTLECONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'CUP' THEN dbd.qty * CUPCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'PACK' THEN dbd.qty * PACKCONVER.Conversion_Factor / KGCONVER.Conversion_Factor
                               WHEN dbd.Unit_code = 'KG' THEN dbd.qty  ELSE 0 END) * im.STD_SNFPer / 100, 0)) AS SNFKGPRODUCT
        FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='POUCH') AS POUCHCONVER ON POUCHCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='BOTTLE') AS BOTTLECONVER ON BOTTLECONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='CUP') AS CUPCONVER ON CUPCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='PACK') AS PACKCONVER ON PACKCONVER.Item_code = dbd.Item_Code
        LEFT JOIN (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_Code='KG') AS KGCONVER ON KGCONVER.Item_code = dbd.Item_Code
		        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=im.Comp_Code
        WHERE CONVERT(DATE, dbm.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
          AND im.IsTaxable = 1 AND im.Is_Ambient = 1 and dbm.Posted=1 GROUP BY dbm.route_no,dbd.Cust_Code
	   ---------4TH

	   union all 
	    SELECT 0 as TotalLtr_ItemWiseDemand,0 AS FATKGDemand, 0 AS SNFKGDemand,'' as Union_Contact_Person,'' as Union_Contact_PhoneNo,NULL AS route_no,null as Booth, SUM(xxxx.Qty) AS DEMANDQTYKG,SUM(xxxx.Fat_KG) AS FATKGPRODUCT,
        SUM(xxxx.SNF_KG) AS SNFKGPRODUCT FROM (
            SELECT CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor  END AS Qty,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG,
                   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code AND ConvertDiv.UOM_Code = 'KG'
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_Dispatch_BulkSale.Posted=1
        ) xxxx
	-----5TH 

	union all
	 SELECT 0 as TotalLtr_ItemWiseDemand, 0 AS FATKGDemand,0 AS SNFKGDemand,max(Union_Contact_Person) as Union_Contact_Person,max(Union_Contact_PhoneNo) as Union_Contact_PhoneNo,Route_No,Cust_Code AS Booth,	 
	 SUM(xxxx.Qty) AS DEMANDQTYKG,SUM(xxxx.Fat_KG) AS FATKGPRODUCT,SUM(xxxx.SNF_KG) AS SNFKGPRODUCT FROM 
        (
            SELECT TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Route_No,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_COMPANY_MASTER.Union_Contact_Person,TSPL_COMPANY_MASTER.Union_Contact_PhoneNo,TSPL_ITEM_MASTER.STD_FatPer,TSPL_ITEM_MASTER.STD_SNFPer, CASE WHEN ISNULL(ConvertDiv.Conversion_Factor, 0) = 0 THEN 0 
                        ELSE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty * ConvertMul.Conversion_Factor / ConvertDiv.Conversion_Factor 
                   END AS Qty,(Qty * STD_FatPer / 100) AS Fat_KG,(Qty * STD_SNFPer / 100) as SNF_KG
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
            LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No = 
			[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
			        LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertMul ON ConvertMul.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertMul.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code 
            INNER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code AND ConvertDiv.UOM_Code = 'KG'
			        left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Code=TSPL_ITEM_MASTER.Comp_Code
            WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=1
        ) xxxx GROUP BY Route_No,Cust_Code
		 ) Final "
                    'end---
                Next
            End If
            'query = "select * from (" + query + ")xx "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.UnionReports, dt2, "rptRoute&BoothWiseDemand", "Demand Report")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim qry As String = ""
            qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleUnionDs", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class