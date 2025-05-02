Imports common
Public Class rptDayWiseBoothDemand
    Inherits FrmMainTranScreen
    Private Sub TxtCustomer__My_Click(sender As Object, e As EventArgs) Handles TxtCustomer._My_Click
        Try
            Dim qry As String = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " where Route_No In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If
            TxtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", qry, "Code", "Name", TxtCustomer.arrValueMember, TxtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Reset()
        txtRoute.arrValueMember = Nothing
        TxtCustomer.arrValueMember = Nothing
        fromDate.Value = clsCommon.GETSERVERDATE()
        RadGroupBox1.Visible = True
        RadGroupBox3.Visible = True
        RadGroupBox4.Visible = True
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbnItemType.IsChecked = True
        rbnItemWise.IsChecked = False
        rdbBooth.IsChecked = True
        rdbRoute.IsChecked = False
        rdbRouteBoothGrp.IsChecked = False
        GV1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub rptDayWiseBoothDemand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            Dim Whr As String = ""
            Dim itemdesc As String = ""
            Dim Qry As String = ""
            Dim Sumitemdesc As String = ""
            Dim itemDetail As String = ""
            Dim Groupby As String = ""
            Dim Booth As String = ""
            Dim Dates As String = ""
            Dim order As String = ""
            Dim dt As DataTable = Nothing
            Dim dt1 As DataTable = Nothing
            Dim Route As String = ""
            Dim startDate As Date = fromDate.Value
            Dim endDate As Date = ToDate.Value
            Dim daysCount As Integer = DateDiff(DateInterval.Day, startDate, endDate) + 1
            If rbnItemWise.IsChecked Then
                itemDetail = " SELECT distinct Short_Description + ' ' + '(' +'LTR'+ ')'  AS Short_Description, Sku_Seq
                         FROM TSPL_DEMAND_BOOKING_DETAIL 
                        Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                        Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                        Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
                        left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  where  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  order by Sku_Seq"
                dt1 = clsDBFuncationality.GetDataTable(itemDetail)
                If dt1 Is Nothing OrElse dt1.Rows.Count > 0 Then
                    For kk As Integer = 0 To dt1.Rows.Count - 1
                        If clsCommon.myLen(itemdesc) > 0 Then
                            itemdesc += ",[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            Sumitemdesc += ", ISNULL(CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)), 0) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        Else
                            itemdesc = "[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                            Sumitemdesc += " ISNULL(CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)), 0) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        End If
                    Next
                Else
                    Throw New Exception("No data found to display")
                End If
            End If
            If rdbBooth.IsChecked And (rbnItemWise.IsChecked) And rdbDetail.IsChecked Then
                Groupby = " GROUP BY [Booth Code] ,Route,Document_Date "
                Booth = " [Booth Code],"
                Dates = "Document_Date as Date,"
            ElseIf rdbBooth.IsChecked And (rbnItemWise.IsChecked) And rdbSummary.IsChecked Then
                Groupby = " GROUP BY [Booth Code] ,Route "
                Booth = " [Booth Code],"
                order = "Order By [Booth Code] "
            ElseIf rdbRoute.IsChecked And rbnItemWise.IsChecked And rdbDetail.IsChecked Then
                Groupby = " Group by Document_Date ,Route "
                Dates = " Cast(document_date as Date) as [Date],"
            ElseIf rdbRoute.IsChecked And rbnItemWise.IsChecked And rdbSummary.IsChecked Then
                Groupby = " Group by Route "
            ElseIf rdbRoute.IsChecked And rbnItemType.IsChecked And rdbDetail.IsChecked Then
                Groupby = " Group by Document_Date ,Route "
                Route = "Route"
                Dates = "Document_Date as Date,"
            ElseIf rdbRoute.IsChecked And rbnItemType.IsChecked And rdbSummary.IsChecked Then
                Groupby = " Group by Route "
                Route = "Route"
            ElseIf rdbBooth.IsChecked And rbnItemType.IsChecked And rdbDetail.IsChecked Then
                Groupby = " Group by Document_Date , [Booth Code] "
                Booth = " [Booth Code],"
                Route = "max(Route) as Route"
                Dates = "Document_Date as Date,"
            ElseIf rdbBooth.IsChecked And rbnItemType.IsChecked And rdbSummary.IsChecked Then
                Groupby = " Group by  [Booth Code] "
                Booth = " [Booth Code],"
                Route = "max(Route) as Route"
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemWise.IsChecked And rdbDetail.IsChecked Then
                Groupby = "GROUP BY Document_Date,Route, Cust_Group_Code order by Date,Route"
                Dates = "Cast(document_date as Date) as [Date], "
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemWise.IsChecked And rdbSummary.IsChecked Then
                Groupby = "GROUP BY Route, Cust_Group_Code order by Route"
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemType.IsChecked And rdbDetail.IsChecked Then
                Groupby = "Group by Document_Date ,Route, Cust_Group_Code  order by Document_Date ,Route"
                Dates = "Document_Date as Date,"
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemType.IsChecked And rdbSummary.IsChecked Then
                Groupby = "Group by Route, Cust_Group_Code  order by Route"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                Whr += "  and Route In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If
            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                Whr += " and [Booth Code] In (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")"
            End If
            If (rdbBooth.IsChecked OrElse rdbRoute.IsChecked) And rbnItemWise.IsChecked Then
                Qry += "SELECT " + Dates + "'LTR' as [QTY IN]," + Booth + "([Route]) AS [Route], max(Zone_Code) as Zone,max(Cust_Group_Code) as [Booth Type], MAX([Mobile No]) AS [Mobile No],  
                     " + Sumitemdesc + ",Sum(ItemNetAmount) as [Total Amount] FROM ((
select xx.Item_Code,  xx.Quantity,xx.[Booth Code],dateadd(day,1,convert(date,Document_Date,103)) as Document_Date,xx.[Customer Name],xx.[Mobile No],xx.Route,xx.Zone_Code,xx.[Route Name],xx.Short_Description,xx.Qty,xx.Cust_Group_Code,xx.Unit_code,ItemNetAmount  from (
                    (SELECT TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
					 ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)  AS [Quantity],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=dateadd(day,-1,convert(date,'" + fromDate.Value + "',103)) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=dateadd(day,-1,convert(date,'" + ToDate.Value + "',103))  
                    ))xx )
					union all
                    (SELECT TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
					 ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)  AS [Quantity],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) 
                    ))AS SourceData PIVOT (SUM([Quantity])FOR Short_Description IN (" + itemdesc + ")) AS PivotTable where 2=2 " + Whr + " " + Groupby + " " + order + "
                     "
                If (rdbBooth.IsChecked OrElse rdbRoute.IsChecked) And rbnItemWise.IsChecked And rdbDetail.IsChecked Then
                    Qry += " order by Date "
                End If
            ElseIf (rdbBooth.IsChecked OrElse rdbRoute.IsChecked) And rbnItemType.IsChecked Then
                Qry = "Select " + Dates + " " + Booth + " " + Route + ",max(Zone_Code) as Zone,max(Cust_Group_Code) as [Booth Type],max([Mobile No]) as [Mobile No],Sum([Milk(LTR)]) as [Milk(LTR)],Sum([Product(LTR)]) as [Product(LTR)],Sum([Milk(LTR)])+Sum([Product(LTR)]) as [Total (LTR)],sum(ItemNetAmount) as [Total Amount],Sum([Milk(LTR)])/  " + clsCommon.myCstr(daysCount) + "  as [AVG Milk],Sum([Product(LTR)])/ " + clsCommon.myCstr(daysCount) + " as [AVG Product] from (((select xx.[Milk(LTR)],xx.[Product(LTR)],xx.[Booth Code],dateadd(day,1,convert(date,xx.Document_Date,103)) as Document_Date,xx.[Customer Name],xx.[Mobile No],xx.Route,xx.Zone_Code,xx.[Route Name],xx.Short_Description,xx.Qty,xx.Cust_Group_Code,xx.Unit_code,ItemNetAmount,Is_FreshItem,Is_FreshAmbient from (
                    (SELECT 
					 Case when tspl_item_master.IsTaxable=0 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Milk(LTR)],Case when tspl_item_master.IsTaxable=1 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Product(LTR)],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],tspl_item_master.Is_FreshItem,Is_FreshAmbient,
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=dateadd(day,-1,convert(date,'" + fromDate.Value + "',103)) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=dateadd(day,-1,convert(date,'" + ToDate.Value + "',103))  
                    ))xx )
					union all
                    (SELECT 
					 case when tspl_item_master.IsTaxable=0 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) end   AS [Milk(LTR)],Case when tspl_item_master.IsTaxable=1 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Product(LTR)],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date , TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,Is_FreshItem,Is_FreshAmbient  FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) 
                    )))YYY  where  2=2 " + Whr + " " + Groupby + " "
                If (rdbBooth.IsChecked OrElse rdbRoute.IsChecked) And rbnItemType.IsChecked And rdbDetail.IsChecked Then
                    Qry += " order by Document_Date "
                End If
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemType.IsChecked Then
                Qry = "Select " + Dates + " (Route) as Route,max(Zone_Code) as Zone,(Cust_Group_Code) as [Booth Type],max([Mobile No]) as [Mobile No],Sum([Milk(LTR)]) as [Milk(LTR)],Sum([Product(LTR)]) as [Product(LTR)],Sum([Milk(LTR)])+Sum([Product(LTR)]) as [Total (LTR)],sum(ItemNetAmount) as [Total Amount] ,Sum([Milk(LTR)])/  " + clsCommon.myCstr(daysCount) + "  as [AVG Milk],Sum([Product(LTR)])/ " + clsCommon.myCstr(daysCount) + " as [AVG Product] from (((select xx.[Milk(LTR)],xx.[Product(LTR)],xx.[Booth Code],dateadd(day,1,convert(date,xx.Document_Date,103)) as Document_Date,xx.[Customer Name],xx.[Mobile No],xx.Route,xx.Zone_Code,xx.[Route Name],xx.Short_Description,xx.Qty,xx.Cust_Group_Code,xx.Unit_code,ItemNetAmount,Is_FreshItem,Is_FreshAmbient from (
                    (SELECT 
					 Case when tspl_item_master.IsTaxable=0 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Milk(LTR)],Case when tspl_item_master.IsTaxable=1 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Product(LTR)],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],tspl_item_master.Is_FreshItem,Is_FreshAmbient,
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=dateadd(day,-1,convert(date,'" + fromDate.Value + "',103)) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=dateadd(day,-1,convert(date,'" + ToDate.Value + "',103))  
                    ))xx )
					union all
                    (SELECT 
					 case when tspl_item_master.IsTaxable=0 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) end   AS [Milk(LTR)],Case when tspl_item_master.IsTaxable=1 then ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) else 0 end  AS [Product(LTR)],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date , TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount,Is_FreshItem,Is_FreshAmbient  FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) 
                    )))YYY  where  2=2 " + Whr + "  " + Groupby + " "
            ElseIf rdbRouteBoothGrp.IsChecked And rbnItemWise.IsChecked Then
                Qry = "SELECT " + Dates + " ([Route]) AS [Route], max(Zone_Code) as Zone,(Cust_Group_Code) as [Booth Type], MAX([Mobile No]) AS [Mobile No],  
                      " + Sumitemdesc + ",Sum(ItemNetAmount) as [Total Amount] FROM ((
                      select xx.Item_Code, xx.Quantity,xx.[Booth Code],dateadd(day,1,convert(date,Document_Date,103)) as Document_Date,xx.[Customer Name],xx.[Mobile No],xx.Route,xx.Zone_Code,xx.[Route Name],xx.Short_Description,xx.Qty,xx.Cust_Group_Code,xx.Unit_code,ItemNetAmount  from (
                    (SELECT TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
					 ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)  AS [Quantity],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],TSPL_DEMAND_BOOKING_MASTER.Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=dateadd(day,-1,convert(date,'" + fromDate.Value + "',103)) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=dateadd(day,-1,convert(date,'" + ToDate.Value + "',103))  
                    ))xx )
					union all
                    (SELECT TSPL_DEMAND_BOOKING_DETAIL.Item_Code,
					 ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)  AS [Quantity],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code],convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) as Document_Date, TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route],TSPL_ROUTE_MASTER.Zone_Code, TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_CUSTOMER_MASTER.Cust_Group_Code,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                 left outer join TSPL_ITEM_UOM_DETAIL ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and ITEMDETAIL1.UOM_Code='LTR'
                    where  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' and
					convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) 
                    ))AS SourceData PIVOT (SUM([Quantity])FOR Short_Description IN (" + itemdesc + ")) AS PivotTable where 2=2 " + Whr + "  " + Groupby + "
                     "
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            GV1.DataSource = Nothing
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()
                GV1.DataSource = dt
                GV1.AutoExpandGroups = True
                GV1.ShowGroupPanel = True
                GV1.ShowRowHeaderColumn = False
                GV1.AllowAddNewRow = False
                GV1.AllowDeleteRow = False
                GV1.EnableFiltering = True
                GV1.ShowFilteringRow = True
                GV1.BestFitColumns()
                If (rdbBooth.IsChecked OrElse rdbRoute.IsChecked) And rbnItemType.IsChecked Then
                    FormatGrid()
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
                If rdbBooth.IsChecked And rbnItemWise.IsChecked Then
                    If dt.Columns.Contains("Date") Then
                        GV1.Columns("Date").FormatString = "{0: dd/MM/yyyy}"
                    End If
                    If dt.Columns.Contains("Zone") Then
                        GV1.Columns("Zone").IsVisible = True
                    End If
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 7 To GV1.Columns.Count - 1
                        Dim items = GV1.Columns(i).HeaderText()
                        Dim item2 As New GridViewSummaryItem(items, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                        GV1.Columns(i).FormatString = "{0:n2}"
                    Next
                    GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    ReStoreGridLayout()
                ElseIf rdbRoute.IsChecked And rbnItemWise.IsChecked Then
                    If dt.Columns.Contains("Date") Then
                        GV1.Columns("Date").FormatString = "{0: dd/MM/yyyy}"
                    End If
                    If dt.Columns.Contains("Zone") Then
                        GV1.Columns("Zone").IsVisible = True
                    End If
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 6 To GV1.Columns.Count - 1
                        Dim items = GV1.Columns(i).HeaderText()
                        Dim item2 As New GridViewSummaryItem(items, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                        GV1.Columns(i).FormatString = "{0:n2}"
                    Next
                    GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    ReStoreGridLayout()
                ElseIf rdbRouteBoothGrp.IsChecked And rbnItemType.IsChecked Then
                    FormatGridRouteGrp()
                ElseIf rdbRouteBoothGrp.IsChecked And rbnItemWise.IsChecked Then
                    If dt.Columns.Contains("Date") Then
                        GV1.Columns("Date").FormatString = "{0: dd/MM/yyyy}"
                    End If
                    If dt.Columns.Contains("Zone") Then
                        GV1.Columns("Zone").IsVisible = True
                    End If
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 5 To GV1.Columns.Count - 1
                        Dim items = GV1.Columns(i).HeaderText()
                        Dim item2 As New GridViewSummaryItem(items, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                        GV1.Columns(i).FormatString = "{0:n2}"
                    Next
                    GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    ReStoreGridLayout()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()

        GV1.AutoExpandGroups = True
        GV1.ShowGroupPanel = True
        GV1.ShowRowHeaderColumn = False
        GV1.AllowAddNewRow = False
        GV1.AllowDeleteRow = False
        GV1.EnableFiltering = True
        GV1.ShowFilteringRow = True


        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).BestFit()
        Next
        If rdbDetail.IsChecked Then
            GV1.Columns("Date").Name = "Date"
            GV1.Columns("Date").FormatString = "{0:dd/MM/yyyy}"
        End If
        If rdbRoute.IsChecked And rbnItemType.IsChecked = False Then
            GV1.Columns("Booth Code").HeaderText = "Booth Code"
            GV1.Columns("Booth Code").Width = 100
            GV1.Columns("Booth Code").IsVisible = True
        End If

        GV1.Columns("Route").HeaderText = "Route Code"
        GV1.Columns("Route").Width = 100
        GV1.Columns("Route").IsVisible = True

        GV1.Columns("Zone").HeaderText = "Zone"
        GV1.Columns("Zone").Width = 100
        GV1.Columns("Zone").IsVisible = False


        GV1.Columns("Booth Type").HeaderText = "Booth Type"
        GV1.Columns("Booth Type").Width = 100
        GV1.Columns("Booth Type").FormatString = ""

        GV1.Columns("Mobile No").HeaderText = "Mobile No"
        GV1.Columns("Mobile No").Width = 100
        GV1.Columns("Mobile No").FormatString = ""

        GV1.Columns("Milk(LTR)").HeaderText = "Milk(LTR)"
        GV1.Columns("Milk(LTR)").IsVisible = True
        GV1.Columns("Milk(LTR)").FormatString = "{0:n2}"
        If objCommonVar.CurrComp_Code1 = "JPR" Then
            GV1.Columns("Product(LTR)").HeaderText = "CHHACH(LTR)"
        Else
            GV1.Columns("Product(LTR)").HeaderText = "Product(LTR)"
        End If
        GV1.Columns("Product(LTR)").Width = 100
        GV1.Columns("Product(LTR)").FormatString = "{0:n2}"

        GV1.Columns("Total (LTR)").HeaderText = "Total (LTR)"
        GV1.Columns("Total (LTR)").IsVisible = True
        GV1.Columns("Total (LTR)").FormatString = "{0:n2}"

        GV1.Columns("Total Amount").HeaderText = "Total Amount"
        GV1.Columns("Total Amount").Width = 100
        GV1.Columns("Total Amount").FormatString = "{0:n2}"

        GV1.Columns("AVG Milk").HeaderText = "AVG Milk"
        GV1.Columns("AVG Milk").IsVisible = True
        GV1.Columns("AVG Milk").FormatString = "{0:n2}"
        If objCommonVar.CurrComp_Code1 = "JPR" Then
            GV1.Columns("AVG Product").HeaderText = "AVG CHHACH"
        Else
            GV1.Columns("AVG Product").HeaderText = "AVG Product"
        End If
        GV1.Columns("AVG Product").Width = 100
        GV1.Columns("AVG Product").FormatString = "{0:n2}"

        GV1.ShowGroupPanel = True
        GV1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbRoute.IsChecked And rbnItemType.IsChecked And rdbDetail.IsChecked Then

            For ii As Integer = 5 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbBooth.IsChecked And rbnItemType.IsChecked And rdbDetail.IsChecked Then

            For ii As Integer = 6 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbBooth.IsChecked And rbnItemType.IsChecked And rdbSummary.IsChecked Then
            For ii As Integer = 5 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbRoute.IsChecked And rbnItemType.IsChecked And rdbSummary.IsChecked Then
            For ii As Integer = 4 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        End If
        ' 


        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Sub FormatGridRouteGrp()

        GV1.AutoExpandGroups = True
        GV1.ShowGroupPanel = True
        GV1.ShowRowHeaderColumn = False
        GV1.AllowAddNewRow = False
        GV1.AllowDeleteRow = False
        GV1.EnableFiltering = True
        GV1.ShowFilteringRow = True


        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).BestFit()
        Next
        If rdbDetail.IsChecked Then
            GV1.Columns("Date").Name = "Date"
            GV1.Columns("Date").FormatString = "{0:dd/MM/yyyy}"
        End If

        GV1.Columns("Route").HeaderText = "Route Code"
        GV1.Columns("Route").Width = 100
        GV1.Columns("Route").IsVisible = True

        GV1.Columns("Zone").HeaderText = "Zone"
        GV1.Columns("Zone").Width = 100
        GV1.Columns("Zone").IsVisible = False


        GV1.Columns("Booth Type").HeaderText = "Booth Type"
        GV1.Columns("Booth Type").Width = 100
        GV1.Columns("Booth Type").FormatString = ""

        GV1.Columns("Mobile No").HeaderText = "Mobile No"
        GV1.Columns("Mobile No").Width = 100
        GV1.Columns("Mobile No").FormatString = ""

        GV1.Columns("Milk(LTR)").HeaderText = "Milk(LTR)"
        GV1.Columns("Milk(LTR)").IsVisible = True
        GV1.Columns("Milk(LTR)").FormatString = "{0:n2}"
        If objCommonVar.CurrComp_Code1 = "JPR" Then
            GV1.Columns("Product(LTR)").HeaderText = "CHHACH(LTR)"
        Else
            GV1.Columns("Product(LTR)").HeaderText = "Product(LTR)"
        End If
        GV1.Columns("Product(LTR)").Width = 100
        GV1.Columns("Product(LTR)").FormatString = "{0:n2}"

        GV1.Columns("Total (LTR)").HeaderText = "Total (LTR)"
        GV1.Columns("Total (LTR)").IsVisible = True
        GV1.Columns("Total (LTR)").FormatString = "{0:n2}"

        GV1.Columns("Total Amount").HeaderText = "Total Amount"
        GV1.Columns("Total Amount").Width = 100
        GV1.Columns("Total Amount").FormatString = "{0:n2}"

        GV1.Columns("AVG Milk").HeaderText = "AVG Milk"
        GV1.Columns("AVG Milk").IsVisible = True
        GV1.Columns("AVG Milk").FormatString = "{0:n2}"

        If objCommonVar.CurrComp_Code1 = "JPR" Then
            GV1.Columns("AVG Product").HeaderText = "AVG CHHACH"
        Else
            GV1.Columns("AVG Product").HeaderText = "AVG Product"
        End If
        GV1.Columns("AVG Product").Width = 100
        GV1.Columns("AVG Product").FormatString = "{0:n2}"


        GV1.ShowGroupPanel = True
        GV1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbDetail.IsChecked Then
            For ii As Integer = 5 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        ElseIf rdbSummary.IsChecked Then
            For ii As Integer = 4 To GV1.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(GV1.Columns(ii).Name, "{0:n2}", GridAggregateFunction.Sum))
            Next
        End If
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= GV1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To GV1.Columns.Count - 1 Step ii + 1
                        GV1.Columns(ii).IsVisible = False
                        GV1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    GV1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If GV1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDayWiseBoothDemand & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                If rdbBooth.IsChecked And rbnItemWise.IsChecked Then
                    arrHeader.Add("Report Type: Booth Item-Wise ")
                ElseIf rdbBooth.IsChecked And rbnItemType.IsChecked Then
                    arrHeader.Add("Report Type: Booth Item-Type ")
                ElseIf rdbRoute.IsChecked And rbnItemType.IsChecked Then
                    arrHeader.Add("Report Type: Route Item-Type ")
                ElseIf rdbRoute.IsChecked And rbnItemWise.IsChecked Then
                    arrHeader.Add("Report Type: Route Item-Wise ")
                ElseIf rdbRouteBoothGrp.IsChecked And rbnItemWise.IsChecked Then
                    arrHeader.Add("Report Type: Route Booth Group Item-Wise ")
                ElseIf rdbRoute.IsChecked And rbnItemType.IsChecked Then
                    arrHeader.Add("Report Type: Route Booth Group Item-Type ")
                End If
                If exporter = EnumExportTo.Excel Then
                        transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                        transportSql.QuickExportToExcel(GV1, "", Me.Text, , arrHeader)
                    Else
                        transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                        clsCommon.MyExportToPDF("Day Wise Booth Demand", GV1, arrHeader, "Day Wise Booth Demand", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    'Sub GetReportGridID()
    '    Dim VarID As String = ""
    '    If rbtnDetail.IsChecked Then
    '        VarID += "_DE"
    '    ElseIf rbtnSummary.IsChecked Then
    '        VarID += "_Su"
    '    End If
    '    If rdbMilk.Checked Then
    '        VarID += "_MK"
    '    ElseIf rdbProduct.Checked Then
    '        VarID += "_PR"
    '    ElseIf rdbDemandBoth.Checked Then
    '        VarID += "_BT"
    '    End If
    '    GV1.VarID = VarID
    'End Sub
End Class