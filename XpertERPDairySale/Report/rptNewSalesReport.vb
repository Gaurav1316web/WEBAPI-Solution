
'Imports common
'Imports System.IO


'Public Class rptNewSalesReport
'    Inherits FrmMainTranScreen

'#Region "Variables"
'    Const ReportID As String = "NewSalesReport"
'#End Region
'    Private Sub rptNewSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        funreset()
'        txtToDate.Value = clsCommon.GETSERVERDATE()
'        txtFromDate.Value = clsCommon.GETSERVERDATE()
'    End Sub

'    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
'        Try
'            Dim qry As String = "select distinct TSPL_ROUTE_MASTER.ROUTE_NO as [ROUTE NO] ,TSPL_ROUTE_MASTER.Route_Desc as [ROUTE NAME] from TSPL_ROUTE_MASTER
'            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No "

'            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerRoute", qry, "ROUTE NO", "ROUTE NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try
'    End Sub

'    Sub View()
'        Try
'            'If gv1.Rows.Count > 0 Then
'            '    Dim view As New ColumnGroupsViewDefinition()
'            '    view.ColumnGroups.Add(New GridViewColumnGroup(""))
'            '    view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
'            '    If rbtnDetail.IsChecked Then
'            '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Document_Date").Name)
'            '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Shift").Name)
'            '    Else
'            '        If rbtnRoute.IsChecked Then
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_No").Name)
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_Desc").Name)
'            '        ElseIf rbtnRouteWise.IsChecked Then
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Cust_Code").Name)
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Customer_Name").Name)
'            '        ElseIf rbtnPartyWise.IsChecked Then
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone_Code").Name)
'            '            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone Name").Name)
'            '        End If
'            '    End If


'            '    view.ColumnGroups.Add(New GridViewColumnGroup("Quantity"))
'            '    view.ColumnGroups.Add(New GridViewColumnGroup("Qy.Tot."))
'            '    view.ColumnGroups.Add(New GridViewColumnGroup("Rate Amount"))
'            '    view.ColumnGroups.Add(New GridViewColumnGroup("Total"))

'            '    view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
'            '    For col As Integer = 9 To gv1.Columns("Total Qty").Index - 1
'            '        view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
'            '    Next
'            '    view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
'            '    view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Total Qty").Name)

'            '    view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())

'            '    For col As Integer = gv1.Columns("Total Qty").Index + 1 To gv1.Columns("Total Amt").Index - 1
'            '        view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
'            '    Next

'            '    view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
'            '    view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Total Amt").Name)
'            '    view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Deposit Amt").Name)
'            '    If rbtnDetail.IsChecked Then
'            '        view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Due").Name)
'            '        view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Balance Amount").Name)
'            '    End If

'            '    gv1.ViewDefinition = view

'            'End If
'        Catch ex As Exception
'            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
'        End Try
'    End Sub

'    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
'        funreset()
'    End Sub

'    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

'        LoadData()
'    End Sub

'    Sub funreset()
'        EnableDisableControls(True)
'        gv1.DataSource = Nothing
'        txtRoute.arrValueMember = Nothing
'        txtCustomer.Value = ""
'        RadPageView1.SelectedPage = RadPageViewPage1
'        rbtnDemand.IsChecked = True
'        rbtnPartyWise.IsChecked = True
'        chkExcludeGhee.Checked = False
'    End Sub

'    Private Sub EnableDisableControls(ByVal val As Boolean)
'        RadGroupBox1.Enabled = val
'    End Sub

'    Private Sub LoadData()
'        Try
'            Dim dtitemName As DataTable = New DataTable()
'            Dim qry As String = ""

'            If rbtnDemand.IsChecked Then

'            Else
'                qry = "SELECT distinct TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Short_Description + 'Amt' as Item_Description,TSPL_ITEM_MASTER.Sku_Seq
'            FROM TSPL_SD_SHIPMENT_DETAIL 
'            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
'            left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
'            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
'            where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
'            and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) "
'                If txtRoute.arrValueMember IsNot Nothing Then
'                    qry += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
'                End If

'                If clsCommon.myLen(txtCustomer.Value) > 0 Then
'                    qry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code = " + clsCommon.myCstr(txtCustomer.Value) + " "
'                End If

'            End If



'            qry += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "

'            qry += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "

'            qry += " and TSPL_ITEM_MASTER.Is_FreshItem = 1 or  TSPL_ITEM_MASTER.Is_Ambient = 1 "

'            qry += " AND TSPL_SD_SHIPMENT_HEAD.Status = 1  ORDER BY Structure_Code,Sku_Seq"

'            Dim itemName2 As String = Nothing
'            Dim itemName1 As String = Nothing
'            Dim itemNames1 As String = Nothing
'            Dim itemNames2 As String = Nothing
'            Dim itemNamesQty As String = Nothing
'            Dim itemNamesAmt As String = Nothing
'            Dim FinalItemNamesQty As String = Nothing
'            Dim FinalItemNamesAmt As String = Nothing
'            If dtitemName.Rows.Count > 0 Then
'                For i As Integer = 0 To dtitemName.Rows.Count - 1
'                    itemName1 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
'                    itemName2 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","
'                    FinalItemNamesQty += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
'                    FinalItemNamesAmt += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","

'                    If i = 0 Then
'                        itemNamesQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
'                        itemNamesAmt += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
'                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
'                        itemNames2 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "
'                    Else
'                        itemNamesQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
'                        itemNamesAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
'                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
'                        itemNames2 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "

'                    End If
'                Next
'            Else
'                gv1.DataSource = Nothing
'                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
'                Exit Sub
'            End If

'            Dim BaseQry As String = ""
'            Dim FinalQuery As String = ""
'            Dim groupBy As String = ""
'            If rbtnDispatch.IsChecked Then
'                If rbtnRoute.IsChecked Then
'                    groupBy = "Route_No"
'                ElseIf rbtnRouteWise.IsChecked Then
'                    groupBy = "Cust_Code "
'                ElseIf rbtnPartyWise.IsChecked Then
'                    groupBy = "Zone_Code "
'                End If
'            End If

'            If rbtnDispatch.IsChecked Then
'                If rbtnRouteWise.IsChecked Then
'                    BaseQry += "SELECT max(Zone_Code)Zone_Code,max([Zone Name])[Zone Name], (Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc,max(Document_Date)Document_Date,max(Shift_Type)Shift_Type "
'                ElseIf rbtnRoute.IsChecked Then
'                    BaseQry += "SELECT max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,(Route_No)Route_No,max(Route_Desc)Route_Desc ,max(Document_Date)Document_Date,max(Shift_Type)Shift_Type  "
'                ElseIf rbtnPartyWise.IsChecked Then
'                    BaseQry += "SELECT (Zone_Code)Zone_Code,  max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc,max(Document_Date)Document_Date,max(Shift_Type)Shift_Type "
'                End If
'            Else
'                BaseQry += " SELECT max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,convert(date, Document_Date, 103) Document_Date, Shift_Type"
'            End If

'            BaseQry += " ," & itemName1 & " SUM(" & itemNamesQty & ") AS [Total Qty], " & itemName2 & "SUM(" & itemNamesAmt & ") AS [Total Amt],SUM( Receipt_Amount) AS [Deposit Amt]
'         FROM (
'         SELECT  TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as [Zone Name], TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,CASE WHEN isnull(TSPL_SD_SHIPMENT_HEAD.Shift_Type,'') = 'AM' THEN 'AM' else 'PM' 
'       END AS Shift_Type,TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_DETAIL.Structure_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_DETAIL.Qty as CRATE,0 AS Receipt_Amount
'         FROM TSPL_SD_SHIPMENT_DETAIL
'         LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
'         LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
'         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
'         left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code
'		 left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SHIPMENT_HEAD.Route_No
'         where 2 = 2 "

'            If txtZone.arrValueMember IsNot Nothing Then
'                BaseQry += "AND TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")  "
'            End If

'            If txtRoute.arrValueMember IsNot Nothing Then
'                BaseQry += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
'            End If
'            If txtCustomer.arrValueMember IsNot Nothing Then
'                BaseQry += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
'            End If

'            If rbtnMilkType.IsChecked Then
'                BaseQry += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
'            ElseIf rbtnProductType.IsChecked Then
'                BaseQry += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
'            ElseIf rbtnBothType.IsChecked Then
'                BaseQry += " and TSPL_ITEM_MASTER.Is_FreshItem = 1 or  TSPL_ITEM_MASTER.Is_Ambient = 1 "

'            End If

'            BaseQry += " and TSPL_SD_SHIPMENT_HEAD.Status = 1 "

'            If rbtnDispatch.IsChecked Then
'                BaseQry += "and  convert(date,Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
'                If rbtnMorning.IsChecked Then
'                    FinalQuery += " and Shift_Type = 'AM' "
'                ElseIf rbtnEvening.IsChecked Then
'                    FinalQuery += " and Shift_Type = 'PM'"
'                End If
'            End If
'            BaseQry += "  union all 
'    select  max(TSPL_ZONE_MASTER.Zone_Code)Zone_Code,max(TSPL_ZONE_MASTER.Description) as [Zone Name] , max(TSPL_RECEIPT_HEADER.Cust_Code) as Cust_Code
'	,max(TSPL_CUSTOMER_MASTER.Customer_Name)Customer_Name ,max(TSPL_CUSTOMER_MASTER.Route_No) as Route_No , max(TSPL_ROUTE_MASTER.Route_Desc)Route_Desc, " & strShift & "  Shift_Type, TSPL_RECEIPT_HEADER.Receipt_Date as Document_Date,'' AS Structure_Code, '' AS Item_Desc,0 AS Item_Net_Amt,'' AS Short_Description,
'    '' AS Item_Description, '' AS Unit_code, 0 AS CRATE,SUM(Receipt_Amount)Receipt_Amount  from TSPL_RECEIPT_HEADER 
'   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code  left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code  left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No
'	 WHERE TSPL_RECEIPT_HEADER.Posted = 'Y'"

'            If txtZone.arrValueMember IsNot Nothing Then
'                BaseQry += " and TSPL_ZONE_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")  "
'            End If

'            If txtRoute.arrValueMember IsNot Nothing Then
'                BaseQry += " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "

'            End If

'            If txtCustomer.arrValueMember IsNot Nothing Then
'                BaseQry += " and  TSPL_RECEIPT_HEADER.Cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
'            End If
'            BaseQry += "GROUP BY Receipt_Date ) AS xx PIVOT (SUM(CRATE)  FOR Short_Description IN (" & itemNames1 & ") ) AS pivot_crate PIVOT (SUM(Item_Net_Amt)  FOR Item_Description IN (" & itemNames2 & ") ) AS pivot_net_amt  "

'            If rbtnDemand.IsChecked Then
'                FinalQuery = "With CTE as (SELECT XXFINAL.Document_Date, XXFINAL.Shift_Type, case when max(Shift_Type) = 'AM' THEN 'M' ELSE 'E' END AS Shift,max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
'                FinalQuery += "" & FinalItemNamesQty & "SUM(XXFINAL.[Total Qty])[Total Qty]," & FinalItemNamesAmt & "
'               SUM(XXFINAL.[Total Amt])[Total Amt],SUM(XXFINAL.[Deposit Amt])[Deposit Amt] FROM (  " & BaseQry & " GROUP BY Document_Date,Shift_Type  ) XXFINAL GROUP BY Document_Date,Shift_Type )
'               select xxx.*,(op + [Total Amt]) as Due,(OP+[Total Amt]-[Deposit Amt]) as [Balance Amount] from (
'               select CTE.* ,isnull((select sum(InnerCTE.[Total Amt])-sum(InnerCTE.[Deposit Amt]) from CTE as InnerCTE where 2= (case when CTE.Shift_Type='AM' then  (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else 3 end )
'               else (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else (case when InnerCTE.Document_Date=CTE.Document_Date and InnerCTE.Shift_Type='AM' then 2 else 3 end) end) end) ),0) as OP
'	           from CTE  )xxx  where xxx.Document_Date >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   xxx.Document_Date <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "

'                If rbtnMorning.IsChecked Then
'                    FinalQuery += " and XXX.Shift_Type = 'AM' "
'                ElseIf rbtnEvening.IsChecked Then
'                    FinalQuery += " and XXX.Shift_Type = 'PM'"
'                End If
'                FinalQuery += "order by xxx.Document_Date,xxx.Shift_Type desc"
'            Else
'                FinalQuery = "" & BaseQry & ""

'                FinalQuery += "Group BY " & groupBy & " order by " & groupBy & ""
'            End If

'            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)

'            gv1.DataSource = Nothing
'            gv1.Rows.Clear()
'            gv1.Columns.Clear()
'            gv1.GroupDescriptors.Clear()
'            gv1.MasterView.Refresh()
'            gv1.GroupDescriptors.Clear()
'            gv1.EnableFiltering = True
'            gv1.MasterTemplate.SummaryRowsBottom.Clear()
'            If dt.Rows.Count > 0 Then
'                gv1.DataSource = dt
'                gv1.BestFitColumns()
'                View()
'                SetGridFormation()
'                ReStoreGridLayout()
'                gv1.MasterTemplate.AutoExpandGroups = True
'                RadPageView1.SelectedPage = RadPageViewPage2
'                gv1.BestFitColumns()
'            Else
'                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
'                Exit Sub

'            End If
'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

'        End Try
'    End Sub

'    Private Function LoadDemandData() As DataTable
'        Dim dt As DataTable = New DataTable()
'        Try
'        Catch ex As Exception

'        End Try
'        Return dt
'    End Function

'    Private Function LoadDispatchData() As DataTable
'        Dim dt As DataTable = New DataTable()
'        Try
'        Catch ex As Exception

'        End Try
'        Return dt
'    End Function
'    Sub SetGridFormation()
'        gv1.TableElement.TableHeaderHeight = 40
'        gv1.MasterTemplate.ShowRowHeaderColumn = True
'        For ii As Integer = 0 To gv1.Columns.Count - 1
'            gv1.Columns(ii).ReadOnly = True
'            gv1.Columns(ii).IsVisible = True
'        Next
'        gv1.ShowGroupPanel = False

'        For ii As Integer = gv1.Columns("Total Qty").Index + 1 To gv1.Columns("Total Amt").Index - 1
'            Dim colName As Integer = gv1.Columns(ii).Name.Length - 1
'            gv1.Columns(ii).HeaderText = gv1.Columns(ii).Name.Remove(colName, 1)
'        Next
'        gv1.Columns("Shift_Type").IsVisible = False

'        If rbtnDispatch.IsChecked Then
'            gv1.Columns("Document_Date").IsVisible = False

'            If rbtnRouteWise.IsChecked Then
'                gv1.Columns("Route_No").IsVisible = False
'                gv1.Columns("Route_Desc").IsVisible = False
'                gv1.Columns("Zone_Code").IsVisible = False
'                gv1.Columns("Zone Name").IsVisible = False
'                gv1.Columns("Cust_Code").HeaderText = "Customer Code"
'                gv1.Columns("Customer_Name").HeaderText = "Customer Name"

'            ElseIf rbtnRoute.IsChecked Then
'                gv1.Columns("Cust_Code").IsVisible = False
'                gv1.Columns("Customer_Name").IsVisible = False
'                gv1.Columns("Zone_Code").IsVisible = False
'                gv1.Columns("Zone Name").IsVisible = False
'                gv1.Columns("Route_No").HeaderText = "Route Code"
'                gv1.Columns("Route_Desc").HeaderText = "Route Name"
'            ElseIf rbtnPartyWise.IsChecked Then
'                gv1.Columns("Cust_Code").IsVisible = False
'                gv1.Columns("Customer_Name").IsVisible = False
'                gv1.Columns("Route_No").IsVisible = False
'                gv1.Columns("Route_Desc").IsVisible = False
'                gv1.Columns("Zone_Code").HeaderText = "Zone Code"

'            End If

'        Else
'            gv1.Columns("OP").IsVisible = False
'            gv1.Columns("Document_Date").HeaderText = "Gate Pass Date"
'            gv1.Columns("Document_Date").FormatString = "{0: dd/MM/yyyy}"
'            gv1.Columns("Document_Date").ExcelExportFormatString = "{0:dd/MM/yyyy}"
'            gv1.Columns("Due").HeaderText = "Due Amt Int.Paid"
'            gv1.Columns("Route_No").IsVisible = False
'            gv1.Columns("Route_Desc").IsVisible = False
'            gv1.Columns("Cust_Code").IsVisible = False
'            gv1.Columns("Customer_Name").IsVisible = False
'            gv1.Columns("Zone_Code").IsVisible = False
'            gv1.Columns("Zone Name").IsVisible = False

'            If rbtnBothShift.IsChecked Then
'                gv1.Columns("Shift").IsVisible = False
'            End If
'        End If

'        Dim summaryRowItem As New GridViewSummaryRowItem()
'        For ii As Integer = 9 To gv1.Columns.Count - 1
'            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
'        Next

'        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
'    End Sub

'    Private Sub ReStoreGridLayout()
'        Try
'            If clsCommon.myLen(ReportID) > 0 Then
'                Dim obj As clsGridLayout = New clsGridLayout()
'                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
'                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
'                    Dim ii As Integer = 0
'                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
'                        gv1.Columns(ii).IsVisible = False
'                        gv1.Columns(ii).VisibleInColumnChooser = True
'                    Next
'                    gv1.LoadLayout(obj.GridLayout)
'                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
'                End If
'                obj = Nothing
'            End If
'        Catch err As Exception
'            MessageBox.Show(err.Message)
'        End Try
'    End Sub

'    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
'        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
'            gv1.MasterTemplate.FilterDescriptors.Clear()
'            Dim obj As New clsGridLayout()
'            obj.ReportID = MyBase.Form_ID
'            obj.UserID = objCommonVar.CurrentUserCode
'            obj.GridLayout = New MemoryStream()
'            gv1.SaveLayout(obj.GridLayout)
'            obj.GridColumns = gv1.ColumnCount
'            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
'            If obj.SaveData() Then
'                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
'            End If
'            obj.GridLayout.Close()
'            obj.GridLayout.Dispose()
'        End If
'    End Sub

'    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
'        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
'        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
'    End Sub

'    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
'        Try
'            If gv1.Rows.Count > 0 Then
'                Dim arrHeader As List(Of String) = New List(Of String)()
'                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
'                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptNewSalesReport & "'"))
'                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
'                If rbtnDispatch.IsChecked = True Then
'                    arrHeader.Add("Report Type : " & "Summary")
'                End If
'                If rbtnDemand.IsChecked = True Then
'                    arrHeader.Add("Report Type : " & "Details")
'                End If
'                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, False, True)
'            Else
'                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
'            End If
'        Catch ex As Exception
'            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try

'    End Sub

'    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
'        Try
'            If gv1.Rows.Count > 0 Then
'                Dim style As New GridPrintStyle()
'                style.PrintGrouping = True
'                style.HeaderCellBackColor = Color.White
'                style.GroupRowBackColor = Color.White
'                style.SummaryCellBackColor = Color.White
'                style.PrintSummaries = True
'                gv1.PrintStyle = style

'                Dim doc As New clsMyPrintDocument()

'                doc.Margins.Top = 50
'                doc.Margins.Bottom = 50
'                doc.Margins.Left = 50
'                doc.Margins.Right = 50
'                doc.HeaderHeight = 90
'                doc.Landscape = True
'                doc.AssociatedObject = gv1

'                doc.DocumentName = objCommonVar.CurrentCompanyName

'                doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine & clsCommon.myCDate(txtFromDate.Value) + "-" + clsCommon.myCDate(txtToDate.Value)
'                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

'                doc.AssociatedObject = gv1

'                doc.RightFooter = "Page [Page #] Of [Total Pages]"

'                Dim dialog As New RadPrintPreviewDialog
'                dialog.Document = doc
'                dialog.ToolMenu.Visible = True
'                dialog.Show()

'                'doc.Print()
'            Else
'                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
'            End If
'        Catch ex As Exception
'            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try
'    End Sub

'    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
'        CancelPressed()
'    End Sub
'    Sub CancelPressed()
'        Me.Close()
'    End Sub

'    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating

'    End Sub
'End Class

