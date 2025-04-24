Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptBoothWiseBillReport
    Dim isPrint As Boolean = False
    Dim SettShowMultipleLegers As Boolean = False
    Dim dt As DataTable = Nothing
    Dim dt1 As DataTable = Nothing

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

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

    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        ToDate.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtRoute.Enabled = isEnable
        TxtCustomer.Enabled = isEnable
        rbtnDetail.Enabled = isEnable
        rbtnSummary.Enabled = isEnable
        btnShiftEvening.Enabled = isEnable
        btnShiftMorning.Enabled = isEnable
        btnShiftBoth.Enabled = isEnable
        rdbBoth.Enabled = isEnable
        rdbMilk.Enabled = isEnable
        rdbProduct.Enabled = isEnable
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptBoothWiseBillReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadGroupBox1.Enabled = True
        RadGroupBox4.Enabled = True
        RadGroupBox2.Enabled = True
        txtRoute.Enabled = True
        TxtCustomer.Enabled = True
        GV1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        GV1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            Dim Whr As String = ""
            Dim itemdesc As String = ""
            Dim Sumitemdesc As String = ""
            Dim strtxtfDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            dt = Nothing
            Dim dt1 As DataTable = Nothing
            Dim Qry As String = ""
            Dim strqry As String = ""

            strqry += " SELECT distinct Short_Description + ' ' + '(' + case when isnull(it.Report_UOM,0) = 1 then  it.UOM_Code + ')' end AS Short_Description, Sku_Seq
                         FROM TSPL_DEMAND_BOOKING_DETAIL 
                        Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                        Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
						left join ( select Item_Code,Report_UOM,UOM_Code from TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) It on It.Item_Code =TSPL_ITEM_MASTER.Item_Code 
                        Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
                        left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  where  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " order by Sku_Seq"
            dt1 = clsDBFuncationality.GetDataTable(strqry)

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

            If rdbMilk.IsChecked Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Fresh' "
            ElseIf rdbProduct.IsChecked Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Product' "
            ElseIf rdbBoth.IsChecked Then
                Whr += " "
            End If

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.route_no In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If

            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code In (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")"
            End If

            If btnShiftMorning.IsChecked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' "
            ElseIf btnShiftEvening.IsChecked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' "
            End If

            If rbtnDetail.IsChecked Then
                Qry += "Select MainQry.* from ( SELECT [Shift], (Date) as Date, max([Booth Code]) as Booth_Code, MAX([Customer Name]) AS [Booth Name], MAX([Mobile No]) AS [Mobile No], MAX([Route]) AS [Route], MAX([Route Name]) AS [Route Name],  
                    " + Sumitemdesc + " FROM (SELECT TSPL_CUSTOMER_MASTER.City_Code, TSPL_CUSTOMER_MASTER.GSTNo, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_CUSTOMER_MASTER.Add1, TSPL_CUSTOMER_MASTER.Add2, TSPL_CUSTOMER_MASTER.Add3, TSPL_CUSTOMER_MASTER.Phone1, TSPL_CUSTOMER_MASTER.Phone2,TSPL_CUSTOMER_MASTER.Fax, TSPL_CUSTOMER_MASTER.State, TSPL_CUSTOMER_MASTER.Email, TSPL_CUSTOMER_MASTER.PAN,  CASE WHEN ITEMDETAIL1.Report_UOM = 1 THEN ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) END AS [Quantity],CONVERT(VARCHAR, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) AS [Date], 
                    TSPL_DEMAND_BOOKING_MASTER.ShiftType  as [Shift], TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No], TSPL_ROUTE_MASTER.Route_No AS [Route], TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code 
                    AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                    left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE 
                    left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code  
                    left outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code =TSPL_DEMAND_BOOKING_MASTER.Comp_Code  						
                    LEFT JOIN (SELECT Conversion_Factor, Item_Code, Report_UOM, UOM_Code FROM TSPL_ITEM_UOM_DETAIL WHERE Report_UOM = 1) AS ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) 
                    AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " 
                    ) AS SourceData PIVOT (SUM([Quantity])FOR Short_Description IN (" + itemdesc + ")) AS PivotTable GROUP BY  Date, Route, Cust_Code, [Shift])MainQry 
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'  
                    order by MainQry.Date "

            ElseIf rbtnSummary.IsChecked Then
                Qry += "SELECT '" + fromDate.Value + "' As 'From Date',Convert(Varchar(10),'" + ToDate.Value + "') As 'To Date', [Booth Code], MAX([Customer Name]) AS [Booth Name], MAX([Mobile No]) AS [Mobile No], MAX([Route]) AS [Route], MAX([Route Name]) AS [Route Name],
                    " + Sumitemdesc + " FROM (SELECT CASE WHEN ITEMDETAIL1.Report_UOM = 1 THEN ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor) END AS [Quantity],
                    TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS [Booth Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No],
                    TSPL_ROUTE_MASTER.Route_No AS [Route], TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty,
                    TSPL_DEMAND_BOOKING_DETAIL.Unit_code FROM TSPL_DEMAND_BOOKING_MASTER
                    LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
                    LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                    LEFT JOIN (SELECT Conversion_Factor, Item_Code, Report_UOM, UOM_Code FROM TSPL_ITEM_UOM_DETAIL WHERE Report_UOM = 1) AS ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                    where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " 
                    ) AS SourceData PIVOT (SUM([Quantity])FOR Short_Description IN (" + itemdesc + ")) AS PivotTable GROUP BY [Booth Code] 
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
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function GetReportID() As String
        Dim VarID As String = MyBase.Form_ID
        If rdbMilk.IsChecked Then
            VarID += "_M"
        ElseIf rdbProduct.IsChecked Then
            VarID += "_P"
        ElseIf rdbBoth.IsChecked Then
            VarID += "_B"
        End If
        Return VarID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= GV1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To GV1.Columns.Count - 1 Step ii + 1
                        GV1.Columns(ii).IsVisible = False
                    Next
                    GV1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            If GV1 IsNot Nothing AndAlso GV1.Rows.Count > 0 Then
                GV1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim chkIndex As Integer = 0
                For i As Integer = 6 To GV1.Columns.Count - 1
                    If (GV1.Columns(i).Name).Contains("Route") Then
                        chkIndex = i
                    ElseIf (GV1.Columns(i).Name).Contains("Route Name") Then
                        chkIndex = i
                    End If
                    If chkIndex > 0 AndAlso i > chkIndex Then
                        Dim item As New GridViewSummaryItem(GV1.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    End If
                Next
                GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Public Function returnQry() As String
        Dim Whr As String = ""
        Dim itemdesc As String = ""
        Dim Sumitemdesc As String = ""
        Dim strtxtfDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
        Dim qry As String = ""
        Dim strqry As String = ""
        Dim whrh As String = ""
        If rdbProduct.IsChecked Then
            whrh = "and Is_Ambient=1"
        ElseIf rdbMilk.IsChecked Then
            whrh = " and Is_FreshItem=1"
        End If

        If rbtnDetail.IsChecked Then
            strqry = "SELECT distinct Short_Description + ' ' + '(' + case when isnull(it.Report_UOM,0) = 1 then  it.UOM_Code + ')' end AS Short_Description, Sku_Seq
                         FROM TSPL_DEMAND_BOOKING_DETAIL 
                        Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                        Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
						left join ( select Item_Code,Report_UOM,UOM_Code from TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) It on It.Item_Code =TSPL_ITEM_MASTER.Item_Code 
                        Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
                        left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  where  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " order by Sku_Seq"

        ElseIf rbtnSummary.IsChecked Then
            strqry = " SELECT distinct Short_Description, Sku_Seq
                         FROM TSPL_DEMAND_BOOKING_DETAIL 
                        Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
                        Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
						left join ( select Item_Code,Report_UOM,UOM_Code from TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) It on It.Item_Code =TSPL_ITEM_MASTER.Item_Code 
                        Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
                        left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  where  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + whrh + " order by Sku_Seq"
        End If

        '  strqry += " SELECT distinct Short_Description + ' ' + '(' + case when isnull(it.Report_UOM,0) = 1 then  it.UOM_Code + ')' end AS Short_Description
        '                   FROM TSPL_DEMAND_BOOKING_DETAIL 
        '                  Left Outer Join TSPL_DEMAND_BOOKING_MASTER On TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
        '                  Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
        '                  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
        'left join ( select Item_Code,Report_UOM,UOM_Code from TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) It on It.Item_Code =TSPL_ITEM_MASTER.Item_Code 
        '                  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.location_code 
        '                  left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  where  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " order by Short_Description desc"

        dt1 = clsDBFuncationality.GetDataTable(strqry)

        If dt1 Is Nothing OrElse dt1.Rows.Count > 0 Then
            For kk As Integer = 0 To dt1.Rows.Count - 1
                If clsCommon.myLen(itemdesc) > 0 Then
                    If rbtnDetail.IsChecked Then
                        itemdesc += ",[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        Sumitemdesc += ", ISNULL(CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)), 0) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                    ElseIf rbtnSummary.IsChecked Then
                        itemdesc += ",[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        Sumitemdesc += ", CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                    End If
                Else
                    If rbtnDetail.IsChecked Then
                        itemdesc = "[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        Sumitemdesc += " ISNULL(CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)), 0) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                    ElseIf rbtnSummary.IsChecked Then
                        itemdesc = "[" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                        Sumitemdesc += " CAST(SUM([" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]) AS DECIMAL(10, 2)) As [" + clsCommon.myCstr(dt1.Rows(kk)("Short_Description")) + "]"
                    End If
                End If
            Next
        Else
            Throw New Exception("No data found to display")
        End If

        If rdbMilk.IsChecked Then
            Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Fresh' "
        ElseIf rdbProduct.IsChecked Then
            Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Product' "
        ElseIf rdbBoth.IsChecked Then
            Whr += " "
        End If

        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Whr += " and TSPL_DEMAND_BOOKING_MASTER.route_no In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
        End If

        If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
            Whr += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code In (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")"
        End If

        If btnShiftMorning.IsChecked Then
            Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' "
        ElseIf btnShiftEvening.IsChecked Then
            Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' "
        End If

        If rbtnDetail.IsChecked Then
            Dim dayDifference As Integer = DateDiff(DateInterval.Day, clsCommon.myCDate(fromDate.Value, "yyyy/MM/dd"), clsCommon.myCDate(ToDate.Value, "yyyy/MM/dd"))
            If dayDifference > 31 Then
                Throw New Exception("Date range must not exceed one month.")
            End If
            qry = "SELECT '" + fromDate.Value + "' As 'From_Date',Convert(Varchar(10),'" + ToDate.Value + "') As 'To_Date', TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.GSTReg_No As SellerGST,TSPL_COMPANY_MASTER.Pan_No, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.City_Code, TSPL_COMPANY_MASTER.comp_code, TSPL_COMPANY_MASTER.add1, TSPL_COMPANY_MASTER.add2, TSPL_COMPANY_MASTER.add3, 
            TSPL_COMPANY_MASTER.Fax, TSPL_COMPANY_MASTER.Email, TSPL_COMPANY_MASTER.Phone1, TSPL_COMPANY_MASTER.Phone2,TSPL_CUSTOMER_MASTER.add1 as CusAdd1 ,TSPL_CUSTOMER_MASTER.Add2 as CusAdd2, TSPL_CUSTOMER_MASTER.Add3 as CusAdd3,TSPL_CUSTOMER_MASTER.State,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_CUSTOMER_MASTER.PAN, TSPL_CUSTOMER_MASTER.GSTNO as Cust_GST_NO,CASE WHEN ITEMDETAIL1.Report_UOM = 1 THEN ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)
            END AS [Quantity], CONVERT(VARCHAR, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) AS [Date], TSPL_DEMAND_BOOKING_DETAIL.Cust_Code As[Booth Code],CAST(TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS INT) As[Booth], CASE WHEN  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' then 1 WHEN  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' then 2 end as shiftType ,
            TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_CUSTOMER_MASTER.Phone1 AS [Mobile No], TSPL_ROUTE_MASTER.Route_No AS [Route], TSPL_CUSTOMER_MASTER.Route_Desc AS [Route Name],
            CASE WHEN  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' then 'M' WHEN  TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' then 'E' end  AS [Shift], TSPL_ITEM_MASTER.Short_Description + ' ' + '(' + case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code + ')' end AS Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty FROM TSPL_DEMAND_BOOKING_MASTER
            LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
            LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
            LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
            LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
            left outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code =TSPL_DEMAND_BOOKING_MASTER.Comp_Code 
            LEFT JOIN (SELECT Conversion_Factor, Item_Code, Report_UOM, UOM_Code FROM TSPL_ITEM_UOM_DETAIL WHERE Report_UOM = 1) AS ITEMDETAIL1 
            ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) 
            AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)" + Whr + " ORDER BY CAST(TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS INT) "

        ElseIf rbtnSummary.IsChecked Then
            qry = "select *,(ItemNetAmount/Quantity_Pouch) as Rate_Item from ( SELECT ROW_NUMBER() OVER (PARTITION BY TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ORDER BY MAX(TSPL_DEMAND_BOOKING_MASTER.Document_Date)) AS Sl_No, 
'" + fromDate.Value + "' As 'From_Date',Convert(Varchar(10),'" + ToDate.Value + "') As 'To_Date', MAX(CAST(Logo_Img AS VARBINARY(MAX))) AS Logo_Img, max(TSPL_COMPANY_MASTER.GSTReg_No) As SellerGST, max (TSPL_COMPANY_MASTER.Pan_No) as [Comp Pan], max (TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name, max (TSPL_COMPANY_MASTER.City_Code) as City_Code, max (TSPL_COMPANY_MASTER.comp_code) as comp_code, max (TSPL_COMPANY_MASTER.add1) as Add1, max (TSPL_COMPANY_MASTER.add2) as add2, max (TSPL_COMPANY_MASTER.add3)as add3, max (TSPL_COMPANY_MASTER.Fax) as Fax, max(TSPL_COMPANY_MASTER.Email) as Email, max (TSPL_COMPANY_MASTER.Phone1) as Phone1, max (TSPL_COMPANY_MASTER.Phone2) as Phone2, max(TSPL_CUSTOMER_MASTER.add1) as CusAdd1, max(TSPL_CUSTOMER_MASTER.Add2) as CusAdd2, max(TSPL_CUSTOMER_MASTER.Add3) as CusAdd3, max (TSPL_CUSTOMER_MASTER.State) as State, max (TSPL_CUSTOMER_MASTER.Zone_Code) as [Zone_Code], max (TSPL_CUSTOMER_MASTER.PAN) as [Cust Pan], max (TSPL_CUSTOMER_MASTER.GSTNO) as Cust_GST_NO, max (TSPL_CUSTOMER_MASTER.Customer_Name) AS [Customer Name], max (TSPL_CUSTOMER_MASTER.Phone1) AS [Mobile No], max(TSPL_LOCATION_MASTER.Location_Desc) as Location,  max(TSPL_ROUTE_MASTER.Route_No) AS [Route], max (TSPL_CUSTOMER_MASTER.Route_Desc) AS [Route Name],  max(CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103)) AS [Date], MAX(Sku_Seq) AS Sku_Seq,
            (CAST(TSPL_DEMAND_BOOKING_DETAIL.Cust_Code AS INT)) As [Booth Code], 	
			MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' THEN 1 WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' THEN 2 END) AS ShiftType,
			MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' THEN 'M' WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' THEN 'E' END) AS [Shift],
			max (TSPL_ITEM_MASTER.Short_Description) AS Short_Description, max (HSN_Code)HSN_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Code, sum (TSPL_DEMAND_BOOKING_DETAIL.Qty) as Qty, 
			sum(CASE WHEN ITEMDETAIL1.Report_UOM = 1 THEN ((Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ITEMDETAIL1.Conversion_Factor)END) AS [Quantity_Pouch],
			sum(ItemNetAmount)as ItemNetAmount, max(TSPL_DEMAND_BOOKING_DETAIL.Item_Rate)Item_Rate,
			max (case when isnull(ITEMDETAIL1.Report_UOM,0) = 1 then  ITEMDETAIL1.UOM_Code end) as [Report_UOM] FROM TSPL_DEMAND_BOOKING_MASTER
            LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
            LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
            LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
            LEFT JOIN tspl_item_master ON TSPL_DEMAND_BOOKING_DETAIL.item_code = tspl_item_master.item_code
			LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_DEMAND_BOOKING_MASTER.Location_Code
            LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = tspl_item_master.item_code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code 
            left outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code =TSPL_DEMAND_BOOKING_MASTER.Comp_Code          
            iNNER  JOIN (SELECT Conversion_Factor, Item_Code, Report_UOM, UOM_Code FROM TSPL_ITEM_UOM_DETAIL WHERE Report_UOM = 1 ) AS ITEMDETAIL1 
            ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code	 where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) 
            AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)" + Whr + "  Group by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Code, Item_Rate)xx order by Sku_Seq"
        End If
        Return qry
    End Function

    Private Sub PrintBoothBill_Click(sender As Object, e As EventArgs) Handles PrintBoothBill.Click
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim dt As DataTable = Nothing
            Dim qry As String = returnQry()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim Whr As String = ""
            If rdbMilk.IsChecked Then
                Whr += " And TSPL_DEMAND_BOOKING_MASTER.ItemType ='Fresh' "
            ElseIf rdbProduct.IsChecked Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Product' "
            ElseIf rdbBoth.IsChecked Then
                Whr += " "
            End If

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_MASTER.route_no In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If

            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                Whr += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code In (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")"
            End If

            If btnShiftMorning.IsChecked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' "
            ElseIf btnShiftEvening.IsChecked Then
                Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' "
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If rbtnDetail.IsChecked Then
                    Dim query As String = "SELECT (Amount / NULLIF(QtyLtr, 0)) AS Rate_Item, (Sku_Seq) AS Sku_Seq, (Item_Desc) AS Item_Desc,  (Amount) AS Amount, (QtyLtr) AS QtyLtr, 
                    (ConversionFactor) AS ConversionFactor,  (CF) AS CF,  (Report_UOM) AS Report_UOM,(Route_No)Route_No,(Cust_Code)as [Booth Code] from (SELECT MAX(Sku_Seq) AS Sku_Seq, MAX(Short_Description) AS Item_Desc,  SUM(ItemNetAmount) AS Amount, SUM(TotalLtr_ItemWise) AS QtyLtr, 
                    MAX(ConversionFactor) AS ConversionFactor,  MAX(CF) AS CF,  MAX(Report_UOM) AS Report_UOM,max(Route_No)Route_No,(Cust_Code)Cust_Code FROM (SELECT TSPL_DEMAND_BOOKING_MASTER.Route_No,
                    CONVERT(DATE, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) AS Document_Date,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_ITEM_MASTER.Item_Code,
                    TSPL_ITEM_MASTER.Sku_Seq, TSPL_ITEM_MASTER.Short_Description, TSPL_DEMAND_BOOKING_DETAIL.Qty, TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount, TSPL_ITEM_MASTER.Rate, 
                    TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise, TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS ConversionFactor, ITEMDETAIL1.Conversion_Factor AS CF, ITEMDETAIL1.UOM_Code AS Report_UOM FROM TSPL_DEMAND_BOOKING_DETAIL 
                    LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                    LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code  
                    LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                    AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                    LEFT JOIN (SELECT Conversion_Factor, Item_Code, Report_UOM, UOM_Code FROM TSPL_ITEM_UOM_DETAIL WHERE Report_UOM = 1) AS ITEMDETAIL1 ON ITEMDETAIL1.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code WHERE TSPL_ITEM_MASTER.Item_Type = 'F' 
                    AND TSPL_ITEM_MASTER.Is_FreshItem = 1 AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1  And convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103)
                    AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Whr + " )xx group by Cust_Code, Item_Code )xxx order by Sku_Seq"

                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                    Throw New Exception("The Print Button does not function for detailed printing.")
                    ' frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "crptBoothWiseBillDetail", "Booth Wise Bill Detail", "SubReportInvoiceDetail")
                Else
                    frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptBoothWiseBillSummary", "Booth Wise Bill Summary Report", Nothing, "crptBoothWiseBillSummary.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                End If
                frmCRV = Nothing
            Else
                Throw New Exception("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If GV1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            End If

            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(TxtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Booth Wise Bill Report", GV1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If GV1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            End If

            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(TxtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Booth Wise Bill Report", GV1, arrHeader, "Booth Wise Bill Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

End Class