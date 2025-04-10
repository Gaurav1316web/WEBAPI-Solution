
Imports common
Imports System.IO
Imports Telerik.Pivot.Core
Imports Telerik.WinControls.Export
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.UI.Export
Imports Telerik.Windows.Controls

Public Class rptItemAndShiftWiseSaleSummaryReport
    Inherits FrmMainTranScreen
    Private Sub rptItemAndShiftWiseSaleSummaryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select distinct TSPL_SD_SALE_INVOICE_HEAD.Route_No as  [ROUTE NO],TSPL_ROUTE_MASTER.Route_Desc as [ROUTE NAME]  FROM TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
           left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No
            where  TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND convert(date,TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) >=Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103) 
            and convert(date,TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103)  and TSPL_ITEM_MASTER.Is_FreshItem = 1 "

            If rbtnMorning.IsChecked Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'AM' "
            ElseIf rbtnEvening.IsChecked Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'PM' "
            End If
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("MilkSupplyRoute", qry, "ROUTE NO", "ROUTE NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        'LoadData()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDemand.IsChecked = True
        rbtnMorning.IsChecked = True
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim BaseQry As String = ""
            BaseQry = ReturnQry()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt


                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    'Private Sub LoadData()
    '    Try
    '        Dim qry As String = ReturnQry()
    '        Dim whrcls As String = ""
    '        Dim strShift As String = ""
    '        Dim whrclsShift As String = ""
    '        If rbtnMorning.IsChecked Then
    '            If rbtnDemand.IsChecked Then
    '                whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Morning' "
    '            ElseIf rbtnDispatch.IsChecked Then
    '                whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'AM' "
    '            End If
    '            strShift = " 'M' "
    '        ElseIf rbtnEvening.IsChecked Then
    '            If rbtnDemand.IsChecked Then
    '                whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "
    '            ElseIf rbtnDispatch.IsChecked Then
    '                whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'PM' "
    '            End If
    '            strShift = " 'E' "
    '        ElseIf rbtnBothShift.IsChecked Then
    '            strShift = "'' "
    '        End If

    '        If txtRoute.arrValueMember IsNot Nothing Then
    '            If rbtnDemand.IsChecked Then
    '                whrcls += "  And TSPL_DEMAND_BOOKING_MASTER.Route_No In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
    '            ElseIf rbtnDispatch.IsChecked Then
    '                whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
    '            End If
    '        End If


    '        If rbtnDispatch.IsChecked Then
    '            qry = "SELECT max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
    '        FROM TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
    '        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
    '        where  TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
    '        and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & " " & whrclsShift & ""
    '        ElseIf rbtnDemand.IsChecked Then
    '            qry = " SELECT  max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
    '        FROM TSPL_DEMAND_BOOKING_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
    '        where  TSPL_DEMAND_BOOKING_MASTER.Posted = 1 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= Convert(date,'" & txtFromDate.Value & "',103)   and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103)  " & whrcls & " " & whrclsShift & ""
    '        End If
    '        qry += " group by TSPL_ITEM_MASTER.Item_Code ORDER BY Sku_Seq "

    '        qry += " SELECT Item_Code, Receipt.Cust_Code as Cust_Code1  ,Zone_Code,[Zone Name],XX.Cust_Code,Customer_Name,Route_No,Route_Desc,"

    '        qry += " CRATE,Receipt.Receipt_Amount,Short_Description,Item_Description,Amount FROM ( Select TSPL_ITEM_MASTER.Item_Code, TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description As [Zone Name], TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,"

    '        If rbtnDispatch.IsChecked Then
    '            BaseQry += " TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_ROUTE_MASTER.Route_Desc,"
    '            If rbtnDetail.IsChecked Then
    '                BaseQry += " Case When isnull(TSPL_SD_SHIPMENT_HEAD.Shift_Type,'') = 'AM' THEN 'AM' else 'PM' END AS Shift_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,"
    '            End If
    '            BaseQry += "  TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt As Amount, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty as CRATE
    '     From TSPL_SD_SALE_INVOICE_DETAIL Left OUTER Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left OUTER Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No Left OUTER Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
    '     Left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No where 2 = 2  And TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & whrcls & " " & whrclsShift & " "
    '        ElseIf rbtnDemand.IsChecked Then
    '            BaseQry += "  TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,"
    '            If rbtnDetail.IsChecked Then
    '                BaseQry += " Case WHEN isnull(TSPL_DEMAND_BOOKING_MASTER.ShiftType,'') = 'Morning' THEN 'AM' else 'PM'   END AS Shift_Type,TSPL_DEMAND_BOOKING_MASTER.Document_Date,"
    '            End If
    '            BaseQry += "  TSPL_ITEM_MASTER.Item_Desc, TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount + (case when TSPL_DEMAND_BOOKING_DETAIL.TAX1 = 'TCS' then TAX1_Amt  when TSPL_DEMAND_BOOKING_DETAIL.TAX2 = 'TCS' then TAX2_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX3 = 'TCS' then TAX3_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX4 = 'TCS' then TAX4_Amt
    '     when TSPL_DEMAND_BOOKING_DETAIL.TAX5 = 'TCS' then TAX5_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX6 = 'TCS' then TAX6_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX7 = 'TCS' then TAX7_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX8 = 'TCS' then TAX8_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX9 = 'TCS' then TAX9_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX10 = 'TCS' then TAX10_Amt else 0 END ) Amount, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
    '     TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.Qty As CRATE FROM TSPL_DEMAND_BOOKING_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
    '     Left OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
    '     where 2 = 2   and TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " & whrcls & " " & whrclsShift & "  "
    '        End If

    '        If rbtnSummary.IsChecked Then
    '            If rbtnDispatch.IsChecked Then
    '                BaseQry += "And  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
    '            Else
    '                BaseQry += "And  convert(date,Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
    '            End If
    '            If rbtnMorning.IsChecked Then
    '                FinalQuery += " and Shift_Type = 'AM' "
    '            ElseIf rbtnEvening.IsChecked Then
    '                FinalQuery += " and Shift_Type = 'PM'"
    '            End If
    '        End If

    '        '  qry += " PIVOT (SUM(CRATE)  FOR Short_Description IN (" & itemNames1 & ") ) AS pivot_crate PIVOT (SUM(Amount)  FOR Item_Description IN (" & itemNames2 & ") ) AS pivot_net_amt  "

    '        If rbtnDetail.IsChecked Then
    '            FinalQuery = "With CTE as (SELECT XXFINAL.Document_Date, XXFINAL.Shift_Type, case when max(Shift_Type) = 'AM' THEN 'M' ELSE 'E' END AS Shift,max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
    '            If rbtnDispatch.IsChecked Then
    '                If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
    '                    Dim Invqry As String = ShowInvoiceNo()
    '                    FinalQuery += " ( SELECT STUFF(( SELECT ',' + Sale_Invoice_No FROM ( " & Invqry & " ) sub FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) AS [Sale Invoice No],"
    '                End If
    '            End If
    '            FinalQuery += "" & FinalItemNamesQty & "SUM(XXFINAL.[Total Qty])[Total Qty]," & FinalItemNamesAmt & "
    '           SUM(XXFINAL.[Total Amt])[Total Amt],max(XXFINAL.[Deposit Amt])[Deposit Amt] FROM (  " & BaseQry & " GROUP BY Document_Date,Shift_Type  ) XXFINAL GROUP BY Document_Date,Shift_Type )
    '           select xxx.*,(op + [Total Amt]) as Due,(OP+[Total Amt]-[Deposit Amt]) as [Balance Amount] from (
    '           select CTE.* ,isnull((select sum(InnerCTE.[Total Amt])-max(InnerCTE.[Deposit Amt]) from CTE as InnerCTE where 2= (case when CTE.Shift_Type='AM' then  (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else 3 end )
    '           else (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else (case when InnerCTE.Document_Date=CTE.Document_Date and InnerCTE.Shift_Type='AM' then 2 else 3 end) end) end) ),0) as OP
    '        from CTE  )xxx  where xxx.Document_Date >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   xxx.Document_Date <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "

    '            If rbtnMorning.IsChecked Then
    '                FinalQuery += " and XXX.Shift_Type = 'AM' "
    '            ElseIf rbtnEvening.IsChecked Then
    '                FinalQuery += " and XXX.Shift_Type = 'PM'"
    '            End If
    '            FinalQuery += "order by xxx.Document_Date,xxx.Shift_Type desc"
    '        Else
    '            FinalQuery = "" & BaseQry & ""

    '            FinalQuery += "Group BY " & groupBy & " order by " & groupBy & ""
    '        End If

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        gv1.DataSource = Nothing
    '        gv1.Rows.Clear()
    '        gv1.Columns.Clear()
    '        gv1.GroupDescriptors.Clear()
    '        gv1.MasterView.Refresh()
    '        gv1.GroupDescriptors.Clear()
    '        gv1.EnableFiltering = True
    '        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        If dt.Rows.Count > 0 Then
    '            gv1.DataSource = dt
    '            gv1.BestFitColumns()
    '            '  SetGridFormation()
    '            ReStoreGridLayout()
    '            gv1.MasterTemplate.AutoExpandGroups = True
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            gv1.BestFitColumns()
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
    '            Exit Sub

    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Sub SetGridFormation()
    '    gv1.TableElement.TableHeaderHeight = 40
    '    gv1.MasterTemplate.ShowRowHeaderColumn = True
    '    For ii As Integer = 0 To gv1.Columns.Count - 1
    '        gv1.Columns(ii).ReadOnly = True
    '        gv1.Columns(ii).IsVisible = True
    '    Next
    '    gv1.ShowGroupPanel = False

    '    For ii As Integer = gv1.Columns("Total Qty").Index + 1 To gv1.Columns("Total Amt").Index - 1
    '        Dim colName As Integer = gv1.Columns(ii).Name.Length - 1
    '        gv1.Columns(ii).HeaderText = gv1.Columns(ii).Name.Remove(colName, 1)
    '    Next
    '    gv1.Columns("Cust_Code").HeaderText = "Customer Code"
    '    gv1.Columns("Customer_Name").HeaderText = "Customer Name"
    '    gv1.Columns("Route_No").HeaderText = "Route Code"
    '    gv1.Columns("Route_Desc").HeaderText = "Route Name"
    '    gv1.Columns("Zone_Code").HeaderText = "Zone Code"

    '    If rbtnSummary.IsChecked Then

    '        If rbtnCustomer.IsChecked Then
    '            gv1.Columns("Route_No").IsVisible = False
    '            gv1.Columns("Route_Desc").IsVisible = False
    '            gv1.Columns("Zone_Code").IsVisible = False
    '            gv1.Columns("Zone Name").IsVisible = False
    '            gv1.Columns("Cust_Code").HeaderText = "Customer Code"
    '            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

    '        ElseIf rbtnRoute.IsChecked Then
    '            gv1.Columns("Cust_Code").IsVisible = False
    '            gv1.Columns("Customer_Name").IsVisible = False
    '            gv1.Columns("Zone_Code").IsVisible = False
    '            gv1.Columns("Zone Name").IsVisible = False
    '            gv1.Columns("Route_No").HeaderText = "Route Code"
    '            gv1.Columns("Route_Desc").HeaderText = "Route Name"
    '        ElseIf rbtnZone.IsChecked Then
    '            gv1.Columns("Cust_Code").IsVisible = False
    '            gv1.Columns("Customer_Name").IsVisible = False
    '            gv1.Columns("Route_No").IsVisible = False
    '            gv1.Columns("Route_Desc").IsVisible = False
    '            gv1.Columns("Zone_Code").HeaderText = "Zone Code"

    '        End If

    '    Else
    '        gv1.Columns("OP").IsVisible = False
    '        gv1.Columns("Document_Date").HeaderText = "Gate Pass Date"
    '        gv1.Columns("Document_Date").FormatString = "{0: dd/MM/yyyy}"
    '        gv1.Columns("Document_Date").ExcelExportFormatString = "{0:dd/MM/yyyy}"
    '        gv1.Columns("Due").HeaderText = "Due Amt Int.Paid"
    '        gv1.Columns("Route_No").IsVisible = False
    '        gv1.Columns("Route_Desc").IsVisible = False
    '        gv1.Columns("Cust_Code").IsVisible = False
    '        gv1.Columns("Customer_Name").IsVisible = False
    '        gv1.Columns("Zone_Code").IsVisible = False
    '        gv1.Columns("Zone Name").IsVisible = False
    '        If clsCommon.myLen(gv1.Columns("Sale Invoice No")) > 0 Then
    '            gv1.Columns("Sale Invoice No").HeaderText = "Sale Invoice No"
    '            gv1.Columns("Sale Invoice No").IsVisible = False
    '        End If
    '        If rbtnBothShift.IsChecked Then
    '            gv1.Columns("Shift").IsVisible = False
    '            gv1.Columns("Shift_Type").IsVisible = False
    '        End If
    '    End If

    '    Dim index As Integer = 0
    '    Dim summaryRowItem As New GridViewSummaryRowItem()
    '    If rbtnSummary.IsChecked Then
    '        index = 6
    '    Else
    '        If rbtnDemand.IsChecked Then
    '            index = 9
    '        ElseIf rbtnDispatch.IsChecked Then
    '            If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
    '                index = 10
    '            ElseIf rbtnBothType.IsChecked Then
    '                index = 9
    '            End If
    '        End If
    '    End If

    '    For ii As Integer = index To gv1.Columns.Count - 1
    '        summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
    '    Next

    '    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    '    gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    'End Sub
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
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemAndShiftWiseSaleSummaryReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                If rbtnDemand.IsChecked Then
                    arrHeader.Add("Transaction Type : " & "Demand")
                ElseIf rbtnDispatch.IsChecked Then
                    arrHeader.Add("Transaction Type : " & "Dispatch")
                End If
                If txtRoute.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route Code : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "   Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
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
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                Dim ReportHeading As String = ""
                If rbtnDemand.IsChecked Then
                    arrHeader.Add("Transaction Type : " & "Demand")
                ElseIf rbtnDispatch.IsChecked Then
                    arrHeader.Add("Transaction Type : " & "Dispatch")
                End If
                If txtRoute.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route Code : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "   Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
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

    'Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    Dim BaseQry As String = ""
    '    BaseQry = ReturnQry()
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        If rbtnDemand.IsChecked Then
    '            If rbtnCustRoute.IsChecked Then
    '                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptMilkSupplySaleCustRouteWiseDetail", "")
    '            ElseIf rbtnCustomer.IsChecked Then
    '                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptMilkSupplySaleCustomerWiseDetail", "")
    '            End If
    '        ElseIf rbtnDispatch.IsChecked Then
    '            If rbtnCustRoute.IsChecked Then
    '                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptMilkSupplySaleCustRouteWiseSummary", "")
    '            ElseIf rbtnCustomer.IsChecked Then
    '                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptMilkSupplySaleCustomerWiseSummary", "")
    '            End If
    '        End If
    '    End If
    'End Sub
    Function ReturnQry()
        Try
            Dim qry As String = ""
            If rbtnDemand.IsChecked Then
            ElseIf rbtnDispatch.IsChecked Then

            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function
End Class
