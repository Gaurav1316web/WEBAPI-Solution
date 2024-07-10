
Imports common
Imports System.IO


Public Class rptNewSalesReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Const ReportID As String = "NewSalesReport"
#End Region
    Private Sub rptNewSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ROUTE_MASTER.ROUTE_NO as [ROUTE NO] ,TSPL_ROUTE_MASTER.Route_Desc as [ROUTE NAME] from TSPL_ROUTE_MASTER
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No "

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerRoute", qry, "ROUTE NO", "ROUTE NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        LoadData()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        txtCustomer.Value = ""
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDemand.IsChecked = True
        rbtnPartyWise.IsChecked = True
        chkExcludeGhee.Checked = False
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim dtFreshItem As DataTable = New DataTable()
            Dim dtProductItem As DataTable = New DataTable()

            Dim qry As String = ""
            If rbtnPartyWise.IsChecked Then
                If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Distributor", Me.Text)
                    txtCustomer.Focus()
                    Exit Sub
                End If
            End If
            Dim whrcls As String = ""
            If rbtnDemand.IsChecked Then
                qry = " ,TSPL_ITEM_MASTER.Sku_Seq FROM TSPL_BOOKING_DETAIL 
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code  left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No
            left join ( SELECT  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No, TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code, TSPL_DISTRIBUTOR_ROUTE.Start_Date FROM  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code 
            JOIN ( SELECT Route_No,MAX(Start_Date) AS Max_Start_Date FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER  LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE  ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code GROUP BY Route_No) AS LatestDates ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No = LatestDates.Route_No AND TSPL_DISTRIBUTOR_ROUTE.Start_Date = LatestDates.Max_Start_Date 
			  ) as  dist on dist.Route_No = TSPL_BOOKING_DETAIL.route_no left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = dist.Cust_Code
            where 2 = 2 "
                whrcls = " AND TSPL_BOOKING_MATSER.Posted = 1 and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) 
            <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "

                If txtRoute.arrValueMember IsNot Nothing Then
                    whrcls += " and TSPL_BOOKING_DETAIL.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If

                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    whrcls += " and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtCustomer.Value) + "' "
                End If
                If chkExcludeGhee.Checked Then
                    whrcls += " and TSPL_ITEM_MASTER.TypeOfItm  <> 'G'"
                End If
            Else
                qry = " ,TSPL_ITEM_MASTER.Sku_Seq
            FROM TSPL_SD_SHIPMENT_DETAIL 
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
            left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
             left join ( SELECT  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No, TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code, TSPL_DISTRIBUTOR_ROUTE.Start_Date FROM  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
           JOIN ( SELECT Route_No,MAX(Start_Date) AS Max_Start_Date FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER  LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE  ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
              GROUP BY Route_No) AS LatestDates ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No = LatestDates.Route_No AND TSPL_DISTRIBUTOR_ROUTE.Start_Date = LatestDates.Max_Start_Date 
			  ) as  dist on dist.Route_No = TSPL_SD_SHIPMENT_HEAD.route_no
             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = dist.Cust_Code where 2 = 2"

                whrcls = " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) 
            and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "
                If txtRoute.arrValueMember IsNot Nothing Then
                    whrcls += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If

                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    whrcls += " and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtCustomer.Value) + " '"
                End If
                If chkExcludeGhee.Checked Then
                    whrcls += " and TSPL_ITEM_MASTER.TypeOfItm  <> 'G'"
                End If
            End If
            dtFreshItem = clsDBFuncationality.GetDataTable("SELECT max(TSPL_ITEM_MASTER.Short_Description) as  Fresh_Item " & qry & " " & whrcls & " and  (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or ( 2 = 2  " & whrcls & " and (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1)) group by TSPL_ITEM_MASTER.Item_Code,Sku_Seq  ORDER BY Sku_Seq")
            dtProductItem = clsDBFuncationality.GetDataTable("SELECT max(TSPL_ITEM_MASTER.Short_Description) as  Product_Item " & qry & " " & whrcls & "  and TSPL_ITEM_MASTER.Is_Ambient = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 group by TSPL_ITEM_MASTER.Item_Code,Sku_Seq ORDER BY Sku_Seq")

            Dim ProductIemName As String = Nothing
            Dim FreshItemName As String = Nothing
            Dim FreshItemsName As String = Nothing
            Dim ProductIemsName As String = Nothing
            Dim ProductItemsAmt As String = Nothing
            Dim ItemSubGroup As String = Nothing
            Dim ItemSubGroupAvg As String = Nothing
            Dim ItemsSubGroup As String = Nothing
            Dim itemNamesFresh As String = Nothing
            Dim itemNamesProduct As String = Nothing
            Dim FreshItemNameMax As String = Nothing
            Dim ProductItemNameMax As String = Nothing
            If dtFreshItem.Rows.Count <= 0 AndAlso dtProductItem.Rows.Count <= 0 Then

                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            If dtFreshItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtFreshItem.Rows.Count - 1
                    FreshItemName += " Sum(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "]" + ","
                    FreshItemNameMax += "max(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "]" + ","
                    If i = 0 Then
                        itemNamesFresh += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "],0)"
                        FreshItemsName += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "] "
                    Else
                        itemNamesFresh += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "],0)"
                        FreshItemsName += ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Fresh_Item")) + "] "
                    End If
                Next

            End If

            If dtProductItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtProductItem.Rows.Count - 1
                    ProductIemName += "Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "]" + ","
                    ProductItemNameMax += " max(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "]" + ","
                    If i = 0 Then
                        itemNamesProduct += "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "],0)"
                        ProductIemsName += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "] "
                    Else
                        itemNamesProduct += "+" + "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "],0)"

                        ProductIemsName += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item")) + "] "
                    End If
                Next
            End If

            Dim dtItemSubGroup As DataTable = New DataTable()
            Dim ItemSubQry As String = ""
            If rbtnDemand.IsChecked Then
                ItemSubQry = " SELECT TSPL_ITEM_MASTER.Item_Sub_Group_Type FROM TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code  left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
            where 2 = 2  AND TSPL_BOOKING_MATSER.Posted = 1 and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) 
            <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  and isnull(Item_Sub_Group_Type,'') <> '' group by Item_Sub_Group_Type "
            Else
                ItemSubQry = " SELECT TSPL_ITEM_MASTER.Item_Sub_Group_Type FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
            where 2 = 2  AND TSPL_SD_SHIPMENT_HEAD.Status = 1 and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) 
            <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  and isnull(Item_Sub_Group_Type,'') <> '' group by Item_Sub_Group_Type "

            End If
            dtItemSubGroup = clsDBFuncationality.GetDataTable(ItemSubQry)

            If dtProductItem.Rows.Count > 0 Then
                If dtItemSubGroup.Rows.Count > 0 Then
                    For i As Integer = 0 To dtItemSubGroup.Rows.Count - 1
                        ItemSubGroupAvg += " case when  cast(sum(" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + ") as int ) = 0 or max(Days) = 0 then 0 else (sum(" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + ")/ max(Days)) end as [" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + " Avg] , "
                        ItemSubGroup += "Sum(IsNull([" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + "],0)) As [" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + "]" + ","
                        If i = 0 Then
                            ItemsSubGroup += "[" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + "] "
                        Else
                            ItemsSubGroup += ", [" + clsCommon.myCstr(dtItemSubGroup.Rows(i)("Item_Sub_Group_Type")) + "] "
                        End If
                    Next
                End If
            End If

            Dim MaxQry As String = ""
            Dim qry1 As String = ""
            Dim qry2 As String = ""
            Dim qry3 As String = ""
            qry = ""
            Dim TotalFreshQty As String = ""
            Dim TotalProdQty As String = ""
            Dim FinalQuery As String = ""
            If dtFreshItem.Rows.Count > 0 Then
                TotalFreshQty = " SUM(" & itemNamesFresh & ")  "
            Else
                TotalFreshQty += " 0  "
            End If

            If dtProductItem.Rows.Count > 0 Then
                TotalProdQty = " SUM(" & itemNamesProduct & ")  "
            Else
                TotalProdQty += " 0  "
            End If
            MaxQry = " " & FreshItemNameMax & " 0 as Total, " & ProductItemNameMax & " 0 as Amount, 0 as Receipt_Amount, (SUM(Amount) - SUM(Receipt_Amount)) as Bal, " & TotalFreshQty & " AS [Total Milk Qty] from ( " & Environment.NewLine & ""
            qry1 = "" & FreshItemName & " 0 as Total, " & ProductIemName & " SUM(Amount) Amount, SUM(Receipt_Amount)Receipt_Amount, 0 as Bal ,  " & TotalFreshQty & "  AS [Total Milk Qty] "
            qry2 = "" & FreshItemName & " 0 as Total , " & ProductIemName & " SUM(Amount) Amount, 0 as Receipt_Amount, 0 as Bal ,  " & TotalFreshQty & " AS [Total Milk Qty]  from ( " & Environment.NewLine & ""
            qry3 = "" & FreshItemName & " 0 as Total , " & ProductIemName & " max(Amount) Amount, 0 as Receipt_Amount, 0 as Bal, " & TotalFreshQty & " AS [Total Milk Qty]  from ( " & Environment.NewLine & ""

            If rbtnDemand.IsChecked Then

                qry += " SELECT TSPL_ITEM_MASTER.Item_Code,TSPL_BOOKING_DETAIL.Cust_Code, TSPL_ITEM_MASTER.Item_Sub_Group_Type,TSPL_ITEM_MASTER.Item_Sub_Group_Type AS Item_Sub_Group_Type1, DATEDIFF(day, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') as Days, TSPL_BOOKING_DETAIL.Route_No, CONVERT(DATE,TSPL_BOOKING_MATSER.Document_Date,103)Document_Date,CASE WHEN isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') = 'AM' THEN 'AM' else 'PM'  END AS Shift_Type , case when  (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then  TSPL_ITEM_MASTER.Short_Description end as Fresh_Item ,
                case when  (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then  TSPL_ITEM_MASTER.Short_Description + 'Amt' end as Fresh_Item_Amt,TSPL_BOOKING_DETAIL.Unit_Code AS UOM ,isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) AS Qty,
                case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) end as Fresh_Qty, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) end as Product_Qty,round((isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) as KG_QTY ,round((isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) as KG_QTY1
                ,round((isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) as LTR_QTY ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_BOOKING_DETAIL.Amount_with_Tax,0)end as Product_Amount
		        ,case when  (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then isnull(TSPL_BOOKING_DETAIL.Amount_with_Tax,0)end as Fresh_Amount,TSPL_BOOKING_DETAIL.Amount_with_Tax AS Amount, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description  end AS Product_Item
		        ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + 'Amt' end AS Product_Item_Amt, 0 AS Receipt_Amount
                FROM TSPL_BOOKING_DETAIL
                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BOOKING_DETAIL.Unit_Code 
		        left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  left join ( SELECT  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No, TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code, TSPL_DISTRIBUTOR_ROUTE.Start_Date FROM  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
              JOIN ( SELECT Route_No,MAX(Start_Date) AS Max_Start_Date FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER  LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE  ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
              GROUP BY Route_No) AS LatestDates ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No = LatestDates.Route_No AND TSPL_DISTRIBUTOR_ROUTE.Start_Date = LatestDates.Max_Start_Date 
			  ) as  dist on dist.Route_No = TSPL_BOOKING_DETAIL.route_no left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = dist.Cust_Code
                left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_BOOKING_DETAIL.Item_Code = I.item_code 
                where 2 = 2 and TSPL_BOOKING_MATSER.Posted = 1   and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103) "

                If txtRoute.arrValueMember IsNot Nothing Then
                    qry += " and TSPL_BOOKING_DETAIL.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                End If
                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtCustomer.Value) + "'"
                End If

            Else
                qry += "SELECT TSPL_ITEM_MASTER.Item_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code, TSPL_ITEM_MASTER.Item_Sub_Group_Type,TSPL_ITEM_MASTER.Item_Sub_Group_Type AS Item_Sub_Group_Type1, DATEDIFF(day, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') as Days, TSPL_SD_SHIPMENT_HEAD.Route_No, CONVERT(DATE,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)Document_Date,CASE WHEN isnull(TSPL_SD_SHIPMENT_HEAD.Shift_Type,'') = 'AM' THEN 'AM' else 'PM'  END AS Shift_Type , case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then  TSPL_ITEM_MASTER.Short_Description end as Fresh_Item ,
                case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then  TSPL_ITEM_MASTER.Short_Description + 'Amt' end as Fresh_Item_Amt,TSPL_SD_SHIPMENT_DETAIL.Unit_Code AS UOM ,isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) AS Qty,
                case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) end as Fresh_Qty, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) end as Product_Qty,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) as KG_QTY ,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) as KG_QTY1
                ,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) as LTR_QTY ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.Amount,0)end as Product_Amount
		        ,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) or (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 and Is_CrateType = 1) then isnull(TSPL_SD_SHIPMENT_DETAIL.Amount,0)end as Fresh_Amount,TSPL_SD_SHIPMENT_DETAIL.Amount, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description  end AS Product_Item
		        ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + 'Amt' end AS Product_Item_Amt, 0 AS Receipt_Amount
                FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_Code 
		        left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE left join ( SELECT  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No, TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code, TSPL_DISTRIBUTOR_ROUTE.Start_Date FROM  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
           JOIN ( SELECT Route_No,MAX(Start_Date) AS Max_Start_Date FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER  LEFT JOIN  TSPL_DISTRIBUTOR_ROUTE  ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.code = TSPL_DISTRIBUTOR_ROUTE.code
              GROUP BY Route_No) AS LatestDates ON TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No = LatestDates.Route_No AND TSPL_DISTRIBUTOR_ROUTE.Start_Date = LatestDates.Max_Start_Date 
			  ) as  dist on dist.Route_No = TSPL_SD_SHIPMENT_HEAD.route_no left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=dist.Cust_Code 
                left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = I.item_code  where 2 = 2 and TSPL_SD_SHIPMENT_HEAD.Status = 1
                and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103) "

                If txtRoute.arrValueMember IsNot Nothing Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
                End If
                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code = '" + clsCommon.myCstr(txtCustomer.Value) + "'"
                End If
            End If
            If rbtnPartyWise.IsChecked Then
                qry += "  union all 
            select '' as Item_Code,'' as Cust_Code  , '' as Item_Sub_Group_Type, '' AS Item_Sub_Group_Type1,  DATEDIFF(day, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "') as Days,  TSPL_CUSTOMER_MASTER.Route_No as Route_No, TSPL_RECEIPT_HEADER.Receipt_Date as Document_Date,'' AS Shift_Type, '' as UOM,'' as Fresh_Item_Amt,'' AS Fresh_Item,0 AS Qty,0 as Fresh_Qty,0 as Product_Qty,0 as KG_QTY,0 as KG_QTY1 , 0 as LTR_QTY, 0 as Fresh_Amount,0 as Product_Amount,0 as Amount,'' as Product_Item,'' as Product_Item_Amt,(Receipt_Amount)Receipt_Amount
	        FROM TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code  
	        WHERE TSPL_RECEIPT_HEADER.Posted = 'Y' AND  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103)"

                If txtRoute.arrValueMember IsNot Nothing Then
                    qry += " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "

                End If

                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    qry += " and  TSPL_RECEIPT_HEADER.Cust_Code  = '" + clsCommon.myCstr(txtCustomer.Value) + "'"
                End If
            End If

            qry += " ) AS xx  "


            If rbtnPartyWise.IsChecked Then
                Dim AmountavgQry As String = "Select isnull(SUM(" & itemNamesFresh & ""

                If dtFreshItem.Rows.Count > 0 AndAlso dtProductItem.Rows.Count > 0 Then
                    AmountavgQry += " + " & itemNamesProduct & "),0) AS [Total_Qty] "
                Else
                    AmountavgQry += " " & itemNamesProduct & "),0) AS [Total_Qty] "
                End If
                AmountavgQry += "from ( " & qry & " " & Environment.NewLine & ""
                If dtFreshItem.Rows.Count > 0 Then
                    AmountavgQry += "PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    AmountavgQry += "PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If
                Dim AmountAvg As Decimal = clsDBFuncationality.getSingleValue(AmountavgQry)
                FinalQuery = "Select  1 As SNO, max(Route_No)Route_No,Shift_Type, convert(varchar,Document_Date,103)Document_Date, " & qry1 & " from ( " & Environment.NewLine & " " & qry & " " & Environment.NewLine & " "
                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += "PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If
                FinalQuery += " Group by Document_Date, Shift_Type"
                FinalQuery += " " & Environment.NewLine & " ---QTY " & Environment.NewLine & " union all " & Environment.NewLine & " Select  2 As SNO, max(Route_No) Route_No, max(Shift_Type)Shift_Type,'QTY' as Document_Date, " & qry1 & " from ( " & Environment.NewLine & " " & qry & " " & Environment.NewLine & ""

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If

                FinalQuery += "  " & Environment.NewLine & " --RATE " & Environment.NewLine & " union all " & Environment.NewLine & " Select 3 as SNO,	max(route_no) as Route_No,max(Shift_Type)Shift_Type,'Rate' as Document_Date, " & MaxQry & "select * , convert( decimal(18,2),(Fresh_Amount/ LTR_QTY)) as Fresh_Rate ,  convert( decimal(18,2),(Product_Amount /  KG_QTY)) as Product_Rate from (" & qry & " " & Environment.NewLine & " )xxx "

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (avg(Fresh_Rate)  FOR Fresh_Item IN (" & FreshItemsName & ") ) AS pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (avg(Product_Rate)   FOR Product_Item IN (" & ProductIemsName & ") ) AS  pivot_Product"
                End If

                FinalQuery += " " & Environment.NewLine & " --AMT " & Environment.NewLine & " union all " & Environment.NewLine & " Select 4 as SNO,max(route_no) as Route_No,max(Shift_Type)Shift_Type,'AMT' as Document_Date, " & qry2 & "" & qry & " " & Environment.NewLine & ""
                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " pivot (sum(Fresh_Amount) For  Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT(SUM(Product_Amount)   For Product_Item In(" & ProductIemsName & ") ) As  pivot_Product"
                End If


                FinalQuery += " " & Environment.NewLine & " --AVG " & Environment.NewLine & " union all " & Environment.NewLine & " Select 5 As SNO,max(route_no) As Route_No,max(Shift_Type)Shift_Type,'AVG' as Document_Date, " & qry3 & " select Days,route_no,Document_Date,Shift_Type,Fresh_Item,Fresh_Item_Amt,Uom,Qty,Fresh_Qty,Product_Qty,KG_QTY,LTR_QTY,Product_Amount,Fresh_Amount,
                (" & AmountAvg & ") AS Amount,Product_Item,Product_Item_Amt,Receipt_Amount , case when cast(LTR_QTY as int) = 0 or Days = 0 then 0 else  convert( decimal(18,2),(LTR_QTY/Days )) end as Fresh_Avg ,  case when cast(KG_QTY as int) = 0 or Days = 0 then 0 else convert( decimal(18,2),(KG_QTY /  Days))end as Product_Avg  from ( " & qry & " " & Environment.NewLine & " )xxx"

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " pivot (sum(Fresh_Avg) FOR  Fresh_Item IN (" & FreshItemsName & ") ) AS pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(Product_Avg)   FOR Product_Item IN(" & ProductIemsName & ") ) AS  pivot_Product "
                End If

                FinalQuery += " order by SNO, Document_Date,Shift_Type "
            ElseIf rbtnRouteWise.IsChecked Then
                FinalQuery = "Select  (Route_No)Route_No,(Shift_Type)Shift_Type, (convert(varchar,Document_Date,103)) Document_Date, " & qry1 & "  from ( " & Environment.NewLine & " " & qry & " " & Environment.NewLine & ""

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If
                FinalQuery += "group by Route_No, Document_Date, Shift_Type "
                FinalQuery += " order by  Document_Date, Shift_Type "
            ElseIf rbtnRouteSummary.IsChecked Then

                FinalQuery = " Select  (Route_No)Route_No,max(Shift_Type)Shift_Type, max(convert(varchar,Document_Date,103)) Document_Date," & qry1 & ", " & ItemSubGroup & " " & TotalFreshQty + " + " & TotalProdQty & " AS [Total Qty] , case when cast(sum([Total Milk Qty])as int) = 0 or max(Days) = 0 then 0 else (sum([Total Milk Qty])/max(Days)) end as [Milk Avg] , " & ItemSubGroupAvg & " 0 as OTH " & "   from ( " & Environment.NewLine & " Select  max(Days)Days,  (Route_No)Route_No,max(Shift_Type)Shift_Type, max(convert(varchar,Document_Date,103)) Document_Date, " & qry1 & ", " & ItemSubGroup & "  0 as Total_Qty from ( " & Environment.NewLine & "" & qry & " " & Environment.NewLine & ""

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                    If dtItemSubGroup.Rows.Count > 0 Then
                        FinalQuery += "  pivot(sum(KG_QTY1) For Item_Sub_Group_Type In (" & ItemsSubGroup & ") )As pivot_sub "
                    End If
                End If
                FinalQuery += " Group by Route_No ,Item_Sub_Group_Type1  )XXFINAL GROUP BY Route_No "
                FinalQuery += " order by Route_No"
            ElseIf rbtProductSale.IsChecked Then
                FinalQuery = "Select  (Shift_Type)Shift_Type, (convert(varchar,Document_Date,103)) Document_Date, " & qry1 & "  from ( " & Environment.NewLine & " " & qry & " " & Environment.NewLine & ""

                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If
                FinalQuery += "group by Route_No, Document_Date, Shift_Type "
                FinalQuery += " " & Environment.NewLine & " ---TTL " & Environment.NewLine & " union all " & Environment.NewLine & " Select   max(Shift_Type)Shift_Type,'TTL' as Document_Date, " & qry1 & " from ( " & Environment.NewLine & " " & qry & " " & Environment.NewLine & ""
                If dtFreshItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemsName & ") ) As pivot_fresh "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    FinalQuery += " PIVOT (SUM(KG_QTY)   For Product_Item In (" & ProductIemsName & ") ) As  pivot_Product "
                End If
                FinalQuery += "order by  Document_Date, Shift_Type "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)

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
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found To Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                
        End Try
    End Sub

    Private Function LoadDemandData() As DataTable
        Dim dt As DataTable = New DataTable()
        Try
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Private Function LoadDispatchData() As DataTable
        Dim dt As DataTable = New DataTable()
        Try
        Catch ex As Exception

        End Try
        Return dt
    End Function
    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.ShowGroupPanel = False


        gv1.Columns("Shift_Type").IsVisible = False
        gv1.Columns("Total").IsVisible = False

        gv1.Columns("Document_Date").HeaderText = "Date"
        If rbtnRouteSummary.IsChecked Then
            gv1.Columns("OTH").IsVisible = False
            gv1.Columns("Document_Date").IsVisible = False

        Else
            gv1.Columns("Total Milk Qty").IsVisible = False
        End If

        gv1.Columns("Amount").HeaderText = "SHIFT WISE AMOUNT RS."
        gv1.Columns("Receipt_Amount").HeaderText = "CHQ/CASH AMOUNT"
        gv1.Columns("Bal").HeaderText = "BAL. RS."

        If rbtnRouteWise.IsChecked OrElse rbtnRouteSummary.IsChecked OrElse rbtProductSale.IsChecked Then
            gv1.Columns("Amount").IsVisible = False
            gv1.Columns("Receipt_Amount").IsVisible = False
            gv1.Columns("Bal").IsVisible = False
            If rbtnRouteWise.IsChecked OrElse rbtnRouteSummary.IsChecked Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For ii As Integer = 3 To gv1.Columns.Count - 1
                    summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
                Next
                gv1.Columns("Route_No").HeaderText = "Route No"
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        ElseIf rbtnPartyWise.IsChecked Then
            gv1.Columns("Route_No").IsVisible = False
            gv1.Columns("SNO").IsVisible = False

        End If

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptNewSalesReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                If rbtnPartyWise.IsChecked = True Then
                    arrHeader.Add("Report Type : " & rbtnPartyWise.Text)
                    arrHeader.Add("Distributor : " & txtCustomer.Value)
                End If
                If rbtnRouteWise.IsChecked = True Then
                    arrHeader.Add("Report Type : " & rbtnRouteWise.Text)
                    arrHeader.Add("Route No : " & clsCommon.GetMulcallString(txtRoute.arrDispalyMember))
                End If
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
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
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                If rbtnPartyWise.IsChecked = True Then
                    arrHeader.Add("Report Type : " & rbtnPartyWise.Text)
                    arrHeader.Add("Distributor : " & txtCustomer.Value)
                End If
                If rbtnRouteWise.IsChecked = True Then
                    arrHeader.Add("Report Type : " & rbtnRouteWise.Text)
                    arrHeader.Add("Route No : " & clsCommon.GetMulcallString(txtRoute.arrDispalyMember))
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
            Dim sQuery As String = " Select Cust_Code as Code , Customer_Name as Name  from TSPL_CUSTOMER_MASTER  "
            Dim whrcls As String = " IsDistributor = 'Y'"
            txtCustomer.Value = clsCommon.ShowSelectForm("SalesNewReportCust", sQuery, "Code", whrcls, txtCustomer.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub
End Class

