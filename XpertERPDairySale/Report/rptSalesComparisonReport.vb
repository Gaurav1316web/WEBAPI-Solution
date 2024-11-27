Imports common
Imports System.IO

Public Class rptSalesComparisonReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dtitemName As DataTable
    Dim dt As DataTable
#End Region
    Private Sub rptSalesComparisonReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtFromDate2.Value = clsCommon.GETSERVERDATE()
        txtFromDate1.Value = clsCommon.GETSERVERDATE()
        txtToDate1.Value = clsCommon.GETSERVERDATE()
        txtToDate2.Value = clsCommon.GETSERVERDATE()
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                If rbtnCustomer.IsChecked Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Cust_Code").Name)
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Customer_Name").Name)
                ElseIf rbtnRoute.IsChecked Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_No").Name)
                End If

                For Each dr As DataRow In dtitemName.Rows
                    view.ColumnGroups.Add(New GridViewColumnGroup(dr("Short_Description")))
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        If gv1.Columns(ii).Name.Contains(dr("Short_Description")) Then
                            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns(ii).Name)
                        End If
                    Next
                Next
                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""

        If rbtnDemand.IsChecked Then
            VarID += "_DE"
        ElseIf rbtnDispatch.IsChecked Then
            VarID += "_DI"
        End If

        If rbtnRoute.IsChecked Then
            VarID += "_R"
        ElseIf rbtnCustomer.IsChecked Then
            VarID += "_C"
        End If

        If rbtnMilkType.IsChecked Then
            VarID += "_MT"
        ElseIf rbtnProductType.IsChecked Then
            VarID += "_PT"
        ElseIf rbtnBothType.IsChecked Then
            VarID += "_BT"
        End If

        gv1.VarID = VarID

    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnRoute.IsChecked = True
        rbtnMilkType.IsChecked = True
        If rbtnCustomer.IsChecked Then
        ElseIf rbtnRoute.IsChecked Then
            lblItem.Location = New System.Drawing.Point(5, 89)
            txtItem.Location = New System.Drawing.Point(95, 89)
        End If
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try

            Dim qry1 As String = ""
            Dim qry2 As String = ""
            Dim qry3 As String = ""
            Dim whrcls As String = ""
            Dim whrclsDate1 As String = " and convert(date,Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MMM/yyyy") & "',103)   and convert(date,Document_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate2.Value, "dd/MMM/yyyy") & "',103)  "
            Dim whrclsDate2 As String = " and convert(date,Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtToDate1.Value, "dd/MMM/yyyy") & "',103)   and convert(date,Document_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate2.Value, "dd/MMM/yyyy") & "',103)  "
            Dim Date1 As String = "'" + clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MM/yyyy") + "-" + clsCommon.GetPrintDate(txtFromDate2.Value, "dd/MM/yyyy") + "'"
            Dim Date2 As String = "'" + clsCommon.GetPrintDate(txtToDate1.Value, "dd/MM/yyyy") + "-" + clsCommon.GetPrintDate(txtToDate2.Value, "dd/MM/yyyy") + "'"
            Dim BaseQry As String = ""
            Dim groupBy As String = ""
            If rbtnMilkType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 "
            ElseIf rbtnProductType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_Ambient = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 "
            End If
            If rbtnDemand.IsChecked Then
                If rbtnCustomer.IsChecked Then
                    groupBy += "  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code"
                ElseIf rbtnRoute.IsChecked Then
                    groupBy += " TSPL_DEMAND_BOOKING_MASTER.Route_No "
                End If
            ElseIf rbtnDispatch.IsChecked Then
                If rbtnCustomer.IsChecked Then
                    groupBy += " TSPL_SD_SHIPMENT_HEAD.Customer_Code "
                ElseIf rbtnRoute.IsChecked Then
                    groupBy += " TSPL_SD_SHIPMENT_HEAD.Route_No "
                End If
            End If
            If clsCommon.myLen(txtRoute.Value) > 0 Then
                If rbtnDemand.IsChecked Then
                    whrcls += "  And TSPL_DEMAND_BOOKING_MASTER.Route_No = '" + txtRoute.Value + "'"
                ElseIf rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SHIPMENT_HEAD.Route_No = '" + txtRoute.Value + "'"
                End If
            End If

            If clsCommon.myLen(txtCustomer.Value) > 0 Then
                If rbtnDemand.IsChecked Then
                    whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code = '" + txtCustomer.Value + "' "
                ElseIf rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code = '" + txtCustomer.Value + "'"
                End If
            End If
            If txtItem.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            If rbtnDemand.IsChecked Then
                qry1 = " SELECT TSPL_ITEM_MASTER.Item_Code ,  " + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Prev_Item," + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Prev_Item_Amt,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq,max(TSPL_ITEM_MASTER.Short_Description)Short_Description "
                BaseQry = " FROM TSPL_DEMAND_BOOKING_DETAIL left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
			left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
            where  TSPL_DEMAND_BOOKING_MASTER.Posted = 1 AND TSPL_ITEM_MASTER.Is_DisplayDemand = 1 " & whrcls & "  "
                qry2 = " SELECT TSPL_ITEM_MASTER.Item_Code ,  " + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Current_Item," + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Current_Item_Amt,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq " & BaseQry & "  "
                BaseQry += "" & whrclsDate1 & ""

                BaseQry += " group by TSPL_ITEM_MASTER.Item_Code )xx " + Environment.NewLine + " left join (" & qry2 & " " & whrclsDate2 & " group by TSPL_ITEM_MASTER.Item_Code )  as xxx on xx.Item_Code = xxx.Item_Code)xxxx group by  Item_Code ORDER BY Sku_Seq"

            ElseIf rbtnDispatch.IsChecked Then
                qry1 = " SELECT TSPL_ITEM_MASTER.Item_Code ,  " + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Prev_Item," + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Prev_Item_Amt,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq,max(TSPL_ITEM_MASTER.Short_Description)Short_Description "
                BaseQry = " FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
			left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
            where  TSPL_SD_SHIPMENT_HEAD.Status = 1 AND TSPL_ITEM_MASTER.Is_DisplayDemand = 1 " & whrcls & "  "
                qry2 = " SELECT TSPL_ITEM_MASTER.Item_Code ,  " + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Current_Item," + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Current_Item_Amt,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq " & BaseQry & " "
                BaseQry += "" & whrclsDate1 & ""

                BaseQry += " group by TSPL_ITEM_MASTER.Item_Code )xx " + Environment.NewLine + " left join (" & qry2 & " " & whrclsDate2 & " group by TSPL_ITEM_MASTER.Item_Code )  as xxx on xx.Item_Code = xxx.Item_Code)xxxx group by  Item_Code ORDER BY Sku_Seq "

            End If
            qry3 = "  select Item_Code,max(Sku_Seq)Sku_Seq,max(Short_Description)Short_Description,max(Prev_Item)Prev_Item,max(Prev_Item_Amt)Prev_Item_Amt,max(Current_Item)Current_Item,max(Current_Item_Amt)Current_Item_Amt from ( select xx.Sku_Seq, xx.Short_Description, xx.Item_Code,Prev_Item,Prev_Item_Amt, case when isnull(Current_Item,'') = '' then  " + Date2 + " + xx.Short_Description else Current_Item end as Current_Item , case when isnull(Current_Item,'') = '' then  " + Date2 + " + xx.Short_Description else Current_Item_Amt end as  Current_Item_Amt from ( " & qry1 & " " & BaseQry & ""

            Dim itemNameCurr As String = Nothing
            Dim itemNamePrev As String = Nothing
            Dim itemNamesPrev As String = Nothing
            Dim itemNamesCurr As String = Nothing
            Dim itemNamesPrevQty As String = Nothing
            Dim itemNamesCurrQty As String = Nothing
            dtitemName = clsDBFuncationality.GetDataTable(qry3)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    itemNamePrev += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "]" + ", (Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "],0)) - Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "],0))) as 'Diff[" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "]',"
                    If i = 0 Then
                        itemNamesPrevQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "],0)"
                        itemNamesCurrQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "],0)"
                        itemNamesPrev += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "] "
                        itemNamesCurr += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "] "
                    Else
                        itemNamesPrevQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "],0)"
                        itemNamesCurrQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "],0)"
                        itemNamesPrev += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Prev_Item")) + "] "
                        itemNamesCurr += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Current_Item")) + "] "

                    End If
                Next
            Else
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = Nothing
                Exit Sub
            End If

            If rbtnRoute.IsChecked Then
                groupBy = " TSPL_ROUTE_MASTER.Route_No "
            ElseIf rbtnCustomer.IsChecked Then
                groupBy = " TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name "
            End If

            BaseQry = "SELECT "
            If rbtnCustomer.IsChecked Then
                BaseQry += " Cust_Code,Customer_Name "
            ElseIf rbtnRoute.IsChecked Then
                BaseQry += " Route_No  "
            End If

            BaseQry += ", " & itemNamePrev & " 0 as Total FROM ( SELECT  xx.Item_Code,xx.Cust_Code,xx.Customer_Name,xx.Route_No,Prev_Item,Prev_Item_Amt,xx.Prev_Qty as Prev_Qty,xx.Amount as Prev_Amt,Current_Item,Current_Item_Amt,xxx.Current_Qty as Current_Qty,xxx.Amount as Current_Amount  from ( "

            If rbtnDispatch.IsChecked Then
                qry1 = " Select TSPL_ITEM_MASTER.Item_Code,  "
                If rbtnCustomer.IsChecked Then
                    qry1 += " TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, max(TSPL_ROUTE_MASTER.Route_No)Route_No,"
                ElseIf rbtnRoute.IsChecked Then
                    qry1 += " max(TSPL_CUSTOMER_MASTER.Cust_Code)Cust_Code ,max(TSPL_CUSTOMER_MASTER.Customer_Name)Customer_Name, (TSPL_ROUTE_MASTER.Route_No)Route_No,"
                End If
                qry1 += "  max(TSPL_ROUTE_MASTER.Route_Desc)Route_Desc, sum(TSPL_SD_SHIPMENT_DETAIL.Amount)Amount, "
                BaseQry += "" & qry1 & "" + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Prev_Item," + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Prev_Item_Amt,max(TSPL_SD_SHIPMENT_DETAIL.Unit_code)UOM,convert(Decimal(18,2),( SUM(TSPL_SD_SHIPMENT_DETAIL.Qty) * isnull(max(TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /max(I.Conversion_Factor)) As Prev_Qty"
                qry2 = " " + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Current_Item," + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Current_Item_Amt,( SUM(TSPL_SD_SHIPMENT_DETAIL.Qty) * isnull(max(TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /max(I.Conversion_Factor) As Current_Qty "
                qry3 = " FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code Left OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SHIPMENT_HEAD.Route_No 
            LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_code LEFT JOIN  ( select item_code,uom_code,conversion_factor from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = I.item_code  where 2 = 2   and TSPL_SD_SHIPMENT_HEAD.Status = 1 AND TSPL_ITEM_MASTER.Is_DisplayDemand = 1 " & whrcls & "  "

                BaseQry += "" & qry3 & ""
                BaseQry += "" & whrclsDate1 & " group by " & groupBy & ",TSPL_ITEM_MASTER.Item_Code "
                BaseQry += " ) xx left join ( " & qry1 & " " & qry2 & " " & qry3 & " " & whrclsDate2 & "  group by " & groupBy & ",TSPL_ITEM_MASTER.Item_Code) as xxx on xx.Item_Code = xxx.Item_Code "

            ElseIf rbtnDemand.IsChecked Then
                qry1 = "  Select TSPL_ITEM_MASTER.Item_Code,"
                If rbtnCustomer.IsChecked Then
                    qry1 += " TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, max(TSPL_ROUTE_MASTER.Route_No)Route_No,"
                ElseIf rbtnRoute.IsChecked Then
                    qry1 += " max(TSPL_CUSTOMER_MASTER.Cust_Code)Cust_Code ,max(TSPL_CUSTOMER_MASTER.Customer_Name)Customer_Name, (TSPL_ROUTE_MASTER.Route_No)Route_No,"
                End If
                qry1 += " max(TSPL_ROUTE_MASTER.Route_Desc)Route_Desc, sum(TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount)Amount, max(TSPL_DEMAND_BOOKING_DETAIL.Unit_code)UOM, "
                BaseQry += "" & qry1 & "" + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Prev_Item," + Date1 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Prev_Item_Amt,convert(Decimal(18,2),( SUM(TSPL_DEMAND_BOOKING_DETAIL.Qty) * isnull(max(TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /max(I.Conversion_Factor)) As Prev_Qty "
                qry2 = " " + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description)  as Current_Item," + Date2 + " + max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Current_Item_Amt,( SUM(TSPL_DEMAND_BOOKING_DETAIL.Qty) * isnull(max(TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /max(I.Conversion_Factor) As Current_Qty "
                qry3 = " FROM TSPL_DEMAND_BOOKING_DETAIL  LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No Left OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
            LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_DEMAND_BOOKING_DETAIL.Unit_code LEFT JOIN  ( select item_code,uom_code,conversion_factor from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code  where 2 = 2   and TSPL_DEMAND_BOOKING_MASTER.Posted = 1 AND TSPL_ITEM_MASTER.Is_DisplayDemand = 1 " & whrcls & "  "

                BaseQry += "" & qry3 & ""
                BaseQry += "" & whrclsDate1 & " group by " & groupBy & ",TSPL_ITEM_MASTER.Item_Code  "
                BaseQry += " ) xx left join ( " & qry1 & " " & qry2 & " " & qry3 & " " & whrclsDate2 & "  group by " & groupBy & ",TSPL_ITEM_MASTER.Item_Code) as xxx on xx.Item_Code = xxx.Item_Code  "
            End If
            BaseQry += ") final PIVOT (SUM(Prev_Qty)  FOR Prev_Item IN (" & itemNamesPrev & ") ) AS prev_Qty PIVOT (SUM(Current_Qty)  FOR Current_Item IN (" & itemNamesCurr & ") ) AS pivot_Current_Qty "

            If rbtnCustomer.IsChecked Then

                BaseQry += " Group by Cust_Code, Customer_Name  order by Cust_Code "
            ElseIf rbtnRoute.IsChecked Then
                BaseQry += " Group by Route_No  order by Route_No "
            End If
            dt = clsDBFuncationality.GetDataTable(BaseQry)


            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                View()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControls(False)
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.ShowGroupPanel = False

        For i As Integer = IIf(rbtnCustomer.IsChecked, 2, 1) To gv1.Columns("Total").Index - 1
            gv1.Columns(i).HeaderText = gv1.Columns(i).Name.Substring(0, 21)

            If gv1.Columns(i).Name.Contains("Diff") Then
                gv1.Columns(i).HeaderText = "Difference(To-From)"
            End If
        Next
        If rbtnRoute.IsChecked Then
            gv1.Columns("Route_No").HeaderText = "Route Code"
        ElseIf rbtnCustomer.IsChecked Then
            gv1.Columns("Cust_Code").HeaderText = "Customer Code"
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()

        For ii As Integer = IIf(rbtnCustomer.IsChecked, 2, 1) To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company  " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name  " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSalesComparisonReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate1.Value) + "  To " + clsCommon.myCDate(txtFromDate2.Value))

                If rbtnRoute.IsChecked Then
                    arrHeader.Add("Route Code : " & txtRoute.Value & "")
                ElseIf rbtnCustomer.IsChecked Then
                    arrHeader.Add("Customer Code: " & txtCustomer.Value & "")
                End If
                transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate1.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtFromDate2.Value, "dd/MMM/yyyy"))
                Dim ReportHeading As String = ""

                If rbtnRoute.IsChecked Then
                    arrHeader.Add("Route Code : " & txtRoute.Value & "")
                ElseIf rbtnCustomer.IsChecked Then
                    arrHeader.Add("Customer Code: " & txtCustomer.Value & "")
                End If

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Try
            Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "
            txtCustomer.Value = clsCommon.ShowSelectForm("SalesComprCustomer", qry, "Code", "", txtCustomer.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "Select Route_No as Code , Route_Desc as Name from tspl_route_master"
            txtRoute.Value = clsCommon.ShowSelectForm("D1D2RouteFinder", qry, "Code", "", txtRoute.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCustomer.ToggleStateChanged, rbtnRoute.ToggleStateChanged
        If rbtnCustomer.IsChecked Then
            lblRoute.Visible = False
            txtRoute.Visible = False
            lblCustomer.Location = New System.Drawing.Point(5, 64)
            txtCustomer.Location = New System.Drawing.Point(95, 64)
            lblItem.Location = New System.Drawing.Point(5, 89)
            txtItem.Location = New System.Drawing.Point(95, 89)
            lblCustomer.Visible = True
            txtCustomer.Visible = True
        ElseIf rbtnRoute.IsChecked Then
            lblCustomer.Visible = False
            txtCustomer.Visible = False
            lblRoute.Visible = True
            txtRoute.Visible = True
            lblItem.Location = New System.Drawing.Point(5, 89)
            txtItem.Location = New System.Drawing.Point(95, 89)
        End If
    End Sub
End Class

