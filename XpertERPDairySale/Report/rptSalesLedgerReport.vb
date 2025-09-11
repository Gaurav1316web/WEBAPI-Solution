
Imports common
Imports System.IO


Public Class rptSalesLedgerReport
    Inherits FrmMainTranScreen

#Region "Variables"
#End Region
    Private Sub rptSalesLedgerReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ROUTE_MASTER.ROUTE_NO as [ROUTE NO] ,TSPL_ROUTE_MASTER.Route_Desc as [ROUTE NAME] from TSPL_ROUTE_MASTER
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No "
            If txtZone.arrValueMember IsNot Nothing Then
                qry += "where TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerRoute", qry, "ROUTE NO", "ROUTE NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                If rbtnDetail.IsChecked Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Document_Date").Name)
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Shift").Name)
                Else
                    If rbtnRoute.IsChecked Then
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_No").Name)
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route_Desc").Name)
                    ElseIf rbtnCustomer.IsChecked Then
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Cust_Code").Name)
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Customer_Name").Name)
                    ElseIf rbtnZone.IsChecked Then
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone_Code").Name)
                        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone Name").Name)
                    End If
                End If


                view.ColumnGroups.Add(New GridViewColumnGroup("Quantity"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Qy.Tot."))
                view.ColumnGroups.Add(New GridViewColumnGroup("Rate Amount"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))

                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                For col As Integer = 6 To gv1.Columns("Total Qty").Index - 1
                    view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Total Qty").Name)

                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())

                For col As Integer = gv1.Columns("Total Qty").Index + 1 To gv1.Columns("Total Amt").Index - 1
                    view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next

                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Total Amt").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Deposit Amt").Name)
                If rbtnDetail.IsChecked Then
                    view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Due").Name)
                    view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Balance Amount").Name)
                End If

                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "

            If txtRoute.arrValueMember IsNot Nothing Then
                qry += "where Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerCustomer", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ZONE_MASTER.Zone_Code AS Code,TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Zone_Code = TSPL_ZONE_MASTER.Zone_Code"

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerZone", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        If rbtnCustomer.IsChecked AndAlso rbtnDispatch.IsChecked AndAlso rbtnSummary.IsChecked AndAlso clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
            LoadDataUDP()
        Else
            LoadData()
        End If
        'LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""

        If rbtnDemand.IsChecked Then
            VarID += "_DE"
        ElseIf rbtnDispatch.IsChecked Then
            VarID += "_DI"
        End If
        If rbtnDetail.IsChecked Then
            VarID += "_D"
        ElseIf rbtnSummary.IsChecked Then
            VarID += "_S"
        End If
        If rbtnRoute.IsChecked Then
            VarID += "_R"
        ElseIf rbtnCustomer.IsChecked Then
            VarID += "_C"
        ElseIf rbtnZone.IsChecked Then
            VarID += "_Z"
        End If

        If rbtnMilkType.IsChecked Then
            VarID += "_MT"
        ElseIf rbtnProductType.IsChecked Then
            VarID += "_PT"
        ElseIf rbtnBothType.IsChecked Then
            VarID += "_BT"
        End If

        If rbtnMorning.IsChecked Then
            VarID += "_MS"
        ElseIf rbtnEvening.IsChecked Then
            VarID += "_PS"
        ElseIf rbtnBothShift.IsChecked Then
            VarID += "_BS"
        End If

        gv1.VarID = VarID

    End Sub

    Sub funreset()
        EnableDisableControls(True)
        chkDCSSale.Checked = False
        gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnDetail.IsChecked = True
        rbtnRoute.IsChecked = True
        rbtnMorning.IsChecked = True
        rbtnMilkType.IsChecked = True
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadDataUDP()
        Try
            'If rbtnDetail.IsChecked Then
            '    If rbtnZone.IsChecked Then
            '        If txtZone.arrValueMember Is Nothing Then
            '            clsCommon.MyMessageBoxShow(Me, "You must select at least one Zone with Detail option", Me.Text)
            '            Exit Sub
            '        Else
            '            If txtZone.arrValueMember.Count = 1 Then
            '                txtRoute.arrValueMember = Nothing
            '                txtCustomer.arrValueMember = Nothing
            '            ElseIf txtZone.arrValueMember.Count > 1 Then
            '                clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Zone at a time with Detail option", Me.Text)
            '                Exit Sub
            '            End If
            '        End If

            '    ElseIf rbtnRoute.IsChecked Then
            '        If txtRoute.arrValueMember Is Nothing Then
            '            clsCommon.MyMessageBoxShow(Me, "You must select at least one Route with Detail option", Me.Text)
            '            Exit Sub
            '        Else
            '            If txtRoute.arrValueMember.Count = 1 Then
            '                txtZone.arrValueMember = Nothing
            '                txtCustomer.arrValueMember = Nothing
            '            ElseIf txtRoute.arrValueMember.Count > 1 Then
            '                clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Route at a time with Detail option", Me.Text)
            '                Exit Sub
            '            End If
            '        End If

            '    ElseIf rbtnCustomer.IsChecked Then
            '        If txtCustomer.arrValueMember Is Nothing Then
            '            clsCommon.MyMessageBoxShow(Me, "You must select at least one Customer with Detail option", Me.Text)
            '            Exit Sub
            '        Else
            '            If txtCustomer.arrValueMember.Count = 1 Then
            '                txtZone.arrValueMember = Nothing
            '                txtRoute.arrValueMember = Nothing
            '            ElseIf txtCustomer.arrValueMember.Count > 1 Then
            '                clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Customer at a time with Detail option", Me.Text)
            '                Exit Sub
            '            End If
            '        End If
            '    End If
            'End If

            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim strShift As String = ""
            Dim whrclsShift As String = ""
            If rbtnMorning.IsChecked Then
                If rbtnDemand.IsChecked Then
                    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Morning' "
                ElseIf rbtnDispatch.IsChecked Then
                    whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'AM' "
                End If
                strShift = " 'M' "
            ElseIf rbtnEvening.IsChecked Then
                If rbtnDemand.IsChecked Then
                    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "
                ElseIf rbtnDispatch.IsChecked Then
                    whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'PM' "
                End If
                strShift = " 'E' "
            ElseIf rbtnBothShift.IsChecked Then
                strShift = "'' "
            End If

            If rbtnMilkType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
            ElseIf rbtnProductType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If

            If txtRoute.arrValueMember IsNot Nothing Then
                If rbtnDemand.IsChecked Then
                    whrcls += "  And TSPL_DEMAND_BOOKING_MASTER.Route_No In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                ElseIf rbtnDispatch.IsChecked Then
                    If chkDCSSale.Checked Then
                        whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ",'')"
                    Else
                        whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                    End If
                End If
            End If

            If txtCustomer.arrValueMember IsNot Nothing Then
                If rbtnDemand.IsChecked Then
                    whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                ElseIf rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
            End If
            If txtZone.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")"
            End If
            If rbtnDispatch.IsChecked Then
                If chkDCSSale.Checked = False Then
                    whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <>'MCC'"
                End If
            End If

            If rbtnDispatch.IsChecked Then
                qry = "SELECT max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where  TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & " " & whrclsShift & ""
            ElseIf rbtnDemand.IsChecked Then
                qry = " SELECT  max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_DEMAND_BOOKING_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
            where  TSPL_DEMAND_BOOKING_MASTER.Posted = 1 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= Convert(date,'" & txtFromDate.Value & "',103)   and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103)  " & whrcls & " " & whrclsShift & ""
            End If
            qry += " group by TSPL_ITEM_MASTER.Item_Code ORDER BY Sku_Seq "

            Dim itemName2 As String = Nothing
            Dim itemName1 As String = Nothing
            Dim itemNames1 As String = Nothing
            Dim itemNames2 As String = Nothing
            Dim itemNamesQty As String = Nothing
            Dim itemNamesAmt As String = Nothing
            Dim itemNameNULL As String = Nothing
            Dim itemNameNULLAmt As String = Nothing
            Dim itemName7 As String = Nothing
            Dim itemName8 As String = Nothing
            Dim itemName89 As String = Nothing
            Dim itemName899 As String = Nothing
            Dim FinalItemNamesQty As String = Nothing
            Dim FinalItemNamesAmt As String = Nothing
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    'itemName1 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    'itemName2 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","
                    'FinalItemNamesQty += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    'FinalItemNamesAmt += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","

                    If i = 0 Then
                        itemNamesQty += "SUM([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        itemNamesAmt += "SUM([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]"
                        itemName7 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        itemName8 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]"
                        'itemNamesAmt += " Sum(ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemName89 += " Sum(ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemName899 += " Sum(ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "
                        itemNameNULL += " NULL AS [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNameNULLAmt += " NULL AS [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1] "
                    Else
                        itemNamesQty += ", SUM([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        itemNamesAmt += ", SUM([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]"
                        itemName7 += ", Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        itemName8 += ", Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]"
                        itemName89 += "+" + " ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemName899 += "+" + " ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        'itemNamesAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "
                        itemNameNULL += ", NULL AS [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNameNULLAmt += ", NULL AS [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1] "

                    End If
                Next
            End If

            qry = " WITH ItemDetails AS (
    SELECT TSPL_CUSTOMER_MASTER.Credit_Customer,TSPL_ITEM_MASTER.Item_Code,TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description AS [Zone Name],TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
        TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt AS Amount,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
        TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty AS CRATE,
        ISNULL((TSPL_SD_SALE_INVOICE_DETAIL.Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / ISNULL(report_uom.Conversion_Factor, 1), 0) AS Report_UOM_Qty
    FROM TSPL_SD_SALE_INVOICE_DETAIL 
    LEFT JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
    LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD  ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
    LEFT JOIN TSPL_SD_SHIPMENT_HEAD  ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
    LEFT JOIN TSPL_CUSTOMER_MASTER  ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
    LEFT JOIN TSPL_ZONE_MASTER  ON TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code
    LEFT JOIN TSPL_ROUTE_MASTER  ON TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No
    LEFT JOIN TSPL_ITEM_UOM_DETAIL  ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
    LEFT JOIN ( SELECT item_code, uom_code, conversion_factor, UOM_Description FROM TSPL_ITEM_UOM_DETAIL
        WHERE Report_UOM = 1 ) report_uom ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = report_uom.item_code
    WHERE TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & whrcls & " " & whrclsShift & "
    And  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  
    and   convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103)),
		ReceiptSummary AS (
			SELECT TSPL_RECEIPT_HEADER.Cust_Code,SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) AS Receipt_Amount
    FROM TSPL_RECEIPT_HEADER 
    LEFT JOIN TSPL_CUSTOMER_MASTER  ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code
    WHERE TSPL_RECEIPT_HEADER.Posted = 'Y' AND TSPL_CUSTOMER_MASTER.IsDistributor = 'Y' GROUP BY TSPL_RECEIPT_HEADER.Cust_Code),

AggregatedData AS (
    SELECT MAX(ItemDetails.Credit_Customer)Credit_Customer,ItemDetails.Item_Code,ItemDetails.Cust_Code,max(ItemDetails.Zone_Code)Zone_Code,max(ItemDetails.[Zone Name])[Zone Name],max(ItemDetails.Customer_Name)Customer_Name, max(ItemDetails.Route_No)Route_No,max(ItemDetails.Route_Desc)Route_Desc,SUM(ItemDetails.CRATE) AS Crate,
        SUM(ItemDetails.Report_UOM_Qty) AS Report_UOM_Qty,MAX(ItemDetails.Short_Description) AS Short_Description,
        MAX(ItemDetails.Item_Description) AS Item_Description,MAX(ReceiptSummary.Receipt_Amount) AS Receipt_Amount,
        SUM(ItemDetails.Amount) AS Amount
    FROM ItemDetails 
    LEFT JOIN ReceiptSummary  ON ReceiptSummary.Cust_Code = ItemDetails.Cust_Code
    GROUP BY ItemDetails.Cust_Code, ItemDetails.Item_Code
),

PivotReportUOM AS (
    SELECT Credit_Customer,Cust_Code,Item_Code,Zone_Code,[Zone Name],Customer_Name,Route_No,Route_Desc,Crate,Report_UOM_Qty,Short_Description,Item_Description,
        Amount,Receipt_Amount
    FROM AggregatedData
),
PivotedData AS (
    SELECT MAX(Credit_Customer)Credit_Customer,Cust_Code,MAX(Zone_Code) AS Zone_Code,MAX([Zone Name]) AS [Zone Name],MAX(Customer_Name) AS Customer_Name,MAX(Route_No) AS Route_No,
        MAX(Route_Desc) AS Route_Desc," & itemName7 & "," & itemName89 & ") AS [Total Qty],
        " & itemName8 & "," & itemName899 & " )AS [Total Amt],
        MAX(Receipt_Amount) AS [Deposit Amt]
    FROM (
        SELECT * FROM PivotReportUOM
    ) src
    PIVOT (
        SUM(Report_UOM_Qty) FOR Short_Description IN (" & itemNames1 & ")
    ) AS p1
    PIVOT (
        SUM(Amount) FOR Item_Description IN (" & itemNames2 & ")
    ) AS p2
    GROUP BY Cust_Code
)

Select * from (Select  SortOrder,MAX(Credit_Customer)Credit_Customer,Cust_Code,MAX(Zone_Code) AS Zone_Code,MAX([Zone Name]) AS [Zone Name],MAX(Customer_Name) AS Customer_Name,MAX(Route_No) AS Route_No,
       MAX(Route_Desc) AS Route_Desc," & itemNamesQty & ",sum(ISNULL([Total Qty],0)) as [Total Qty],
	   " & itemNamesAmt & ",sum(ISNULL([Total Amt],0)) as [Total Amt],
       MAX([Deposit Amt]) AS [Deposit Amt] 
from (
SELECT *,1 AS SortOrder 
FROM PivotedData where Credit_Customer='Y'

union all

Select  NULL AS Credit_Customer,'Department' AS Cust_Code,NULL AS Zone_Code,NULL AS [Zone Name],NULL AS Customer_Name,NULL AS Route_No,NULL AS Route_Desc,
       " & itemNameNULL & " ,NULL AS [Total Qty]," & itemNameNULLAmt & " ,NULL AS [Total Amt],NULL AS [Deposit Amt],0 AS SortOrder
		FROM PivotedData 

		UNION ALL

		
Select  NULL AS Credit_Customer,'Total' AS Cust_Code,NULL AS Zone_Code,NULL AS [Zone Name],NULL AS Customer_Name,NULL AS Route_No,NULL AS Route_Desc,
        " & itemNamesQty & ",Sum([Total Qty]) AS [Total Qty]," & itemNamesAmt & ",Sum([Total Amt]) AS [Total Amt],Sum([Deposit Amt]) AS [Deposit Amt],2 AS SortOrder
		FROM PivotedData  where Credit_Customer='Y'
UNION ALL 

		Select  NULL AS Credit_Customer,'Agent' AS Cust_Code,NULL AS Zone_Code,NULL AS [Zone Name],NULL AS Customer_Name,NULL AS Route_No,NULL AS Route_Desc,
        " & itemNameNULL & " ,NULL AS [Total Qty]," & itemNameNULLAmt & " ,NULL AS [Total Amt],NULL AS [Deposit Amt],3 AS SortOrder
		FROM PivotedData 
		UNION ALL 
		Select  *,4 as Sortorder from 
		 PivotedData where Credit_Customer='N'
		 union all
		 Select  NULL AS Credit_Customer,'Agent Total' AS Cust_Code,NULL AS Zone_Code,NULL AS [Zone Name],NULL AS Customer_Name,NULL AS Route_No,NULL AS Route_Desc,
         " & itemNamesQty & ",Sum([Total Qty]) AS [Total Qty]," & itemNamesAmt & ",Sum([Total Amt]) AS [Total Amt],Sum([Deposit Amt]) AS [Deposit Amt],5 AS SortOrder
		FROM PivotedData  where Credit_Customer='N'

		)yy	group by yy.Cust_Code,yy.SortOrder)yy
--ORDER BY Cust_Code; "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim Credit_qry As String = "Select Credit_Customer from TSPL_CUSTOMER_MASTER WHERE Cust_Code In (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "

            Dim creditdt As DataTable = clsDBFuncationality.GetDataTable(Credit_qry)

            ' Now check if the result contains both "Y" and "N"
            Dim hasY As Boolean = creditdt.AsEnumerable().Any(Function(row) row.Field(Of String)("Credit_Customer").ToUpper() = "Y")
            Dim hasN As Boolean = creditdt.AsEnumerable().Any(Function(row) row.Field(Of String)("Credit_Customer").ToUpper() = "N")

            If hasY AndAlso hasN Then
                ' Contains both Y and N → no change
                ' You can just keep the original dt
            ElseIf hasY AndAlso Not hasN Then
                ' Contains only Y → remove agent rows from dt
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    Dim custCode As String = dt.Rows(i)("cust_code").ToString().ToLower()
                    If custCode.Contains("agent total") OrElse custCode.Contains("agent") Then
                        dt.Rows.RemoveAt(i)
                    End If
                Next
                dt.AcceptChanges()
            End If

            ' Now dt is modified if needed, proceed with further processing

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
                    'If rbtnDetail.IsChecked Then
                    '    If rbtnDispatch.IsChecked Then
                    '        Dim InvoiceBtn As New GridViewCommandColumn()
                    '        InvoiceBtn.FormatString = ""
                    '        InvoiceBtn.UseDefaultText = True
                    '        InvoiceBtn.DefaultText = "Click to Show Invoice No"
                    '        InvoiceBtn.HeaderText = "InvoiceNo"
                    '        InvoiceBtn.Name = "InvoiceNo"
                    '        InvoiceBtn.FieldName = "InvoiceNo"
                    '        InvoiceBtn.Width = 80
                    '        InvoiceBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    '        gv1.MasterTemplate.Columns.Insert(9, InvoiceBtn)
                    '    End If
                    'End If

                    gv1.BestFitColumns()
                'View()
                SetGridFormationUDP()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub

                End If
            'gv1.BestFitColumns()
            'View()
            'SetGridFormation()
            'ReStoreGridLayout()
            'gv1.MasterTemplate.AutoExpandGroups = True
            'RadPageView1.SelectedPage = RadPageViewPage2
            'gv1.BestFitColumns()
            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            '    Exit Sub

            'End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData()
        Try
            If rbtnDetail.IsChecked Then
                If rbtnZone.IsChecked Then
                    If txtZone.arrValueMember Is Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "You must select at least one Zone with Detail option", Me.Text)
                        Exit Sub
                    Else
                        If txtZone.arrValueMember.Count = 1 Then
                            txtRoute.arrValueMember = Nothing
                            txtCustomer.arrValueMember = Nothing
                        ElseIf txtZone.arrValueMember.Count > 1 Then
                            clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Zone at a time with Detail option", Me.Text)
                            Exit Sub
                        End If
                    End If

                ElseIf rbtnRoute.IsChecked Then
                    If txtRoute.arrValueMember Is Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "You must select at least one Route with Detail option", Me.Text)
                        Exit Sub
                    Else
                        If txtRoute.arrValueMember.Count = 1 Then
                            txtZone.arrValueMember = Nothing
                            txtCustomer.arrValueMember = Nothing
                        ElseIf txtRoute.arrValueMember.Count > 1 Then
                            clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Route at a time with Detail option", Me.Text)
                            Exit Sub
                        End If
                    End If

                ElseIf rbtnCustomer.IsChecked Then
                    If txtCustomer.arrValueMember Is Nothing Then
                        clsCommon.MyMessageBoxShow(Me, "You must select at least one Customer with Detail option", Me.Text)
                        Exit Sub
                    Else
                        If txtCustomer.arrValueMember.Count = 1 Then
                            txtZone.arrValueMember = Nothing
                            txtRoute.arrValueMember = Nothing
                        ElseIf txtCustomer.arrValueMember.Count > 1 Then
                            clsCommon.MyMessageBoxShow(Me, "You cannot select more than one Customer at a time with Detail option", Me.Text)
                            Exit Sub
                        End If
                    End If
                End If
            End If


            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim strShift As String = ""
            Dim whrclsShift As String = ""
            If rbtnMorning.IsChecked Then
                If rbtnDemand.IsChecked Then
                    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Morning' "
                ElseIf rbtnDispatch.IsChecked Then
                    whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'AM' "
                End If
                strShift = " 'M' "
            ElseIf rbtnEvening.IsChecked Then
                If rbtnDemand.IsChecked Then
                    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "
                ElseIf rbtnDispatch.IsChecked Then
                    whrclsShift = " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'PM' "
                End If
                strShift = " 'E' "
            ElseIf rbtnBothShift.IsChecked Then
                strShift = "'' "
            End If

            If rbtnMilkType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
            ElseIf rbtnProductType.IsChecked Then
                whrcls += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If

            If txtRoute.arrValueMember IsNot Nothing Then
                If rbtnDemand.IsChecked Then
                    whrcls += "  And TSPL_DEMAND_BOOKING_MASTER.Route_No In (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                ElseIf rbtnDispatch.IsChecked Then
                    If chkDCSSale.Checked Then
                        whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ",'')"
                    Else
                        whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                    End If
                End If
            End If

            If txtCustomer.arrValueMember IsNot Nothing Then
                If rbtnDemand.IsChecked Then
                    whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                ElseIf rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
            End If
            If txtZone.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")"
            End If
            If rbtnDispatch.IsChecked Then
                If chkDCSSale.Checked = False Then
                    whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type <>'MCC'"
                End If
            End If

            If rbtnDispatch.IsChecked Then
                qry = "SELECT max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where  TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & " " & whrclsShift & ""
            ElseIf rbtnDemand.IsChecked Then
                qry = " SELECT  max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + 'Amt' as Item_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_DEMAND_BOOKING_DETAIL  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
            where  TSPL_DEMAND_BOOKING_MASTER.Posted = 1 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= Convert(date,'" & txtFromDate.Value & "',103)   and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103)  " & whrcls & " " & whrclsShift & ""
            End If
            qry += " group by TSPL_ITEM_MASTER.Item_Code ORDER BY Sku_Seq "

            Dim itemName2 As String = Nothing
            Dim itemName1 As String = Nothing
            Dim itemNames1 As String = Nothing
            Dim itemNames2 As String = Nothing
            Dim itemNamesQty As String = Nothing
            Dim itemNamesAmt As String = Nothing
            Dim FinalItemNamesQty As String = Nothing
            Dim FinalItemNamesAmt As String = Nothing
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    itemName1 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    itemName2 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","
                    FinalItemNamesQty += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    FinalItemNamesAmt += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "1]" + ","

                    If i = 0 Then
                        itemNamesQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNamesAmt += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "
                    Else
                        itemNamesQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNamesAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "],0)"
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                        itemNames2 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Description")) + "] "

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

            Dim BaseQry As String = ""
            Dim FinalQuery As String = ""
            Dim groupBy As String = ""
            If rbtnSummary.IsChecked Then
                If rbtnRoute.IsChecked Then
                    groupBy = "Route_No"
                ElseIf rbtnCustomer.IsChecked Then
                    groupBy = "Cust_Code "
                ElseIf rbtnZone.IsChecked Then
                    groupBy = "Zone_Code "
                End If
            End If

            If rbtnSummary.IsChecked Then
                If rbtnCustomer.IsChecked Then
                    BaseQry += "SELECT max(Zone_Code)Zone_Code,max([Zone Name])[Zone Name], (Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
                ElseIf rbtnRoute.IsChecked Then
                    BaseQry += "SELECT max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,(Route_No)Route_No,max(Route_Desc)Route_Desc  , "
                ElseIf rbtnZone.IsChecked Then
                    BaseQry += "SELECT (Zone_Code)Zone_Code,  max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
                End If
            Else
                BaseQry += " SELECT max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,convert(date, Document_Date, 103) Document_Date, Shift_Type ,"
                If rbtnDispatch.IsChecked Then
                    If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
                        BaseQry += " max(Sale_Invoice_No)Sale_Invoice_No ,"
                    End If
                End If
            End If
            BaseQry += " " & itemName1 & " SUM(" & itemNamesQty & ") As [Total Qty], " & itemName2 & "SUM(" & itemNamesAmt & ") As [Total Amt],max( Receipt_Amount) As [Deposit Amt] FROM ( "
            If rbtnDetail.IsChecked Then
                BaseQry += " select max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,convert(date, Document_Date, 103) Document_Date, Shift_Type,sum(CRATE)CRATE,sum(Amount)Amount ,max( Receipt_Amount) As [Deposit Amt],max(Short_Description)Short_Description,max(Item_Description)Item_Description,sum(Receipt_Amount)Receipt_Amount,sum(Report_UOM_Qty)Report_UOM_Qty  "
                If rbtnDispatch.IsChecked Then
                    If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
                        BaseQry += " ,max(Sale_Invoice_No)Sale_Invoice_No "
                    End If
                End If
                BaseQry += " from ( "
            End If
            If rbtnSummary.IsChecked Then
                If rbtnCustomer.IsChecked Then
                    BaseQry += "SELECT max(Zone_Code)Zone_Code,max([Zone Name])[Zone Name], (Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
                ElseIf rbtnRoute.IsChecked Then
                    BaseQry += "SELECT max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,(Route_No)Route_No,max(Route_Desc)Route_Desc  , "
                ElseIf rbtnZone.IsChecked Then
                    BaseQry += "SELECT (Zone_Code)Zone_Code,  max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
                End If
                BaseQry += " sum(Crate)Crate,sum(Report_UOM_Qty)Report_UOM_Qty,max(Short_Description)Short_Description,max(Item_Description)Item_Description,max(Receipt_Amount)Receipt_Amount,sum(Amount)Amount from ( "
            End If

            BaseQry += " SELECT Item_Code, Receipt.Cust_Code as Cust_Code1  ,Zone_Code,[Zone Name],XX.Cust_Code,Customer_Name,Route_No,Route_Desc,"
            If rbtnDetail.IsChecked Then
                BaseQry += " Document_Date,Shift_Type,"
                If rbtnDispatch.IsChecked Then
                    BaseQry += "Sale_Invoice_No,"
                End If
            End If
            BaseQry += " Report_UOM_Qty,CRATE,Receipt.Receipt_Amount,Short_Description,Item_Description,Amount FROM ( Select TSPL_ITEM_MASTER.Item_Code, TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description As [Zone Name], TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,"

            If rbtnDispatch.IsChecked Then
                BaseQry += " TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_ROUTE_MASTER.Route_Desc,"
                If rbtnDetail.IsChecked Then
                    BaseQry += " Case When isnull(TSPL_SD_SHIPMENT_HEAD.Shift_Type,'') = 'AM' THEN 'AM' else 'PM' END AS Shift_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,"
                End If
                BaseQry += "  TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt As Amount, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty as CRATE,isnull((TSPL_SD_SALE_INVOICE_DETAIL.Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(Report_UOM.Conversion_Factor),0) As Report_UOM_Qty
         From TSPL_SD_SALE_INVOICE_DETAIL Left OUTER Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left OUTER Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No Left OUTER Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
         Left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No
left outer join TSPL_ITEM_UOM_DETAIL ON tspl_item_uom_detail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and tspl_item_uom_detail.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code  LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  Report_UOM ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = Report_UOM.item_code 
where 2 = 2  And TSPL_SD_SALE_INVOICE_HEAD.Status = 1 " & whrcls & " " & whrclsShift & " "
            ElseIf rbtnDemand.IsChecked Then
                BaseQry += "  TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,"
                If rbtnDetail.IsChecked Then
                    BaseQry += " Case WHEN isnull(TSPL_DEMAND_BOOKING_MASTER.ShiftType,'') = 'Morning' THEN 'AM' else 'PM'   END AS Shift_Type,TSPL_DEMAND_BOOKING_MASTER.Document_Date,"
                End If
                BaseQry += "  TSPL_ITEM_MASTER.Item_Desc, TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount + (case when TSPL_DEMAND_BOOKING_DETAIL.TAX1 = 'TCS' then TAX1_Amt  when TSPL_DEMAND_BOOKING_DETAIL.TAX2 = 'TCS' then TAX2_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX3 = 'TCS' then TAX3_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX4 = 'TCS' then TAX4_Amt
         when TSPL_DEMAND_BOOKING_DETAIL.TAX5 = 'TCS' then TAX5_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX6 = 'TCS' then TAX6_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX7 = 'TCS' then TAX7_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX8 = 'TCS' then TAX8_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX9 = 'TCS' then TAX9_Amt when TSPL_DEMAND_BOOKING_DETAIL.TAX10 = 'TCS' then TAX10_Amt else 0 END ) Amount, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,
         TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.Qty As CRATE,isnull((TSPL_DEMAND_BOOKING_DETAIL.Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(Report_UOM.Conversion_Factor),0) As Report_UOM_Qty FROM TSPL_DEMAND_BOOKING_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code LEFT OUTER JOIN TSPL_DEMAND_BOOKING_MASTER ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
         Left OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
left outer join TSPL_ITEM_UOM_DETAIL ON tspl_item_uom_detail.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code and tspl_item_uom_detail.UOM_Code= TSPL_DEMAND_BOOKING_DETAIL.Unit_code  LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  Report_UOM ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = Report_UOM.item_code 
         where 2 = 2   and TSPL_DEMAND_BOOKING_MASTER.Posted = 1 " & whrcls & " " & whrclsShift & "  "
            End If

            If rbtnSummary.IsChecked Then
                If rbtnDispatch.IsChecked Then
                    BaseQry += "And  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
                Else
                    BaseQry += "And  convert(date,Document_Date,103) >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   convert(date,Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "
                End If
                If rbtnMorning.IsChecked Then
                    FinalQuery += " and Shift_Type = 'AM' "
                ElseIf rbtnEvening.IsChecked Then
                    FinalQuery += " and Shift_Type = 'PM'"
                End If
            End If

            BaseQry += " )xx left join ( select TSPL_RECEIPT_HEADER.Cust_Code ,SUM(Receipt_Amount)Receipt_Amount  from TSPL_RECEIPT_HEADER   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code  
	        WHERE TSPL_RECEIPT_HEADER.Posted = 'Y' and TSPL_CUSTOMER_MASTER.IsDistributor = 'Y'  GROUP BY TSPL_RECEIPT_HEADER.Cust_Code ) Receipt on Receipt.Cust_Code = XX.Cust_Code )XXX "

            If rbtnSummary.IsChecked Then
                BaseQry += " group by " & groupBy & ",Item_Code ) xxxx"
            Else
                BaseQry += "  group by Document_Date,Shift_Type,Item_Code )final  "
            End If
            BaseQry += " PIVOT (SUM(Report_UOM_Qty)  FOR Short_Description IN (" & itemNames1 & ") ) AS pivot_Report_UOM PIVOT (SUM(Amount)  FOR Item_Description IN (" & itemNames2 & ") ) AS pivot_net_amt  "

            If rbtnDetail.IsChecked Then
                FinalQuery = "With CTE as (SELECT XXFINAL.Document_Date, XXFINAL.Shift_Type, case when max(Shift_Type) = 'AM' THEN 'M' ELSE 'E' END AS Shift,max(Zone_Code)Zone_Code, max([Zone Name])[Zone Name], max(Cust_Code)Cust_Code,max(Customer_Name)Customer_Name,max(Route_No)Route_No,max(Route_Desc)Route_Desc ,"
                If rbtnDispatch.IsChecked Then
                    If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
                        Dim Invqry As String = ShowInvoiceNo()
                        FinalQuery += " ( SELECT STUFF(( SELECT ',' + Sale_Invoice_No FROM ( " & Invqry & " ) sub FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'), 1, 1, '')) AS [Sale Invoice No],"
                    End If
                End If
                FinalQuery += "" & FinalItemNamesQty & "SUM(XXFINAL.[Total Qty])[Total Qty]," & FinalItemNamesAmt & "
               SUM(XXFINAL.[Total Amt])[Total Amt],max(XXFINAL.[Deposit Amt])[Deposit Amt] FROM (  " & BaseQry & " GROUP BY Document_Date,Shift_Type  ) XXFINAL GROUP BY Document_Date,Shift_Type )
               select xxx.*,(op + [Total Amt]) as Due,(OP+[Total Amt]-[Deposit Amt]) as [Balance Amount] from (
               select CTE.* ,isnull((select sum(InnerCTE.[Total Amt])-max(InnerCTE.[Deposit Amt]) from CTE as InnerCTE where 2= (case when CTE.Shift_Type='AM' then  (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else 3 end )
               else (case when InnerCTE.Document_Date<CTE.Document_Date then 2 else (case when InnerCTE.Document_Date=CTE.Document_Date and InnerCTE.Shift_Type='AM' then 2 else 3 end) end) end) ),0) as OP
	           from CTE  )xxx  where xxx.Document_Date >= CONVERT(DATE, '" & txtFromDate.Value & "', 103)  and   xxx.Document_Date <= CONVERT(DATE, '" & txtToDate.Value & "', 103) "

                If rbtnMorning.IsChecked Then
                    FinalQuery += " and XXX.Shift_Type = 'AM' "
                ElseIf rbtnEvening.IsChecked Then
                    FinalQuery += " and XXX.Shift_Type = 'PM'"
                End If
                FinalQuery += "order by xxx.Document_Date,xxx.Shift_Type desc"
            Else
                FinalQuery = "" & BaseQry & ""

                FinalQuery += "Group BY " & groupBy & " order by " & groupBy & ""
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
                If rbtnDetail.IsChecked Then
                    If rbtnDispatch.IsChecked Then
                        Dim InvoiceBtn As New GridViewCommandColumn()
                        InvoiceBtn.FormatString = ""
                        InvoiceBtn.UseDefaultText = True
                        InvoiceBtn.DefaultText = "Click to Show Invoice No"
                        InvoiceBtn.HeaderText = "InvoiceNo"
                        InvoiceBtn.Name = "InvoiceNo"
                        InvoiceBtn.FieldName = "InvoiceNo"
                        InvoiceBtn.Width = 80
                        InvoiceBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                        gv1.MasterTemplate.Columns.Insert(9, InvoiceBtn)
                    End If
                End If

                gv1.BestFitColumns()
                View()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormationUDP()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        gv1.Columns("SortOrder").IsVisible = False
        gv1.Columns("Credit_Customer").IsVisible = False

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

        For ii As Integer = gv1.Columns("Total Qty").Index + 1 To gv1.Columns("Total Amt").Index - 1
            Dim colName As Integer = gv1.Columns(ii).Name.Length - 1
            gv1.Columns(ii).HeaderText = gv1.Columns(ii).Name.Remove(colName, 1)
        Next
        gv1.Columns("Cust_Code").HeaderText = "Customer Code"
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        gv1.Columns("Route_No").HeaderText = "Route Code"
        gv1.Columns("Route_Desc").HeaderText = "Route Name"
        gv1.Columns("Zone_Code").HeaderText = "Zone Code"

        If rbtnSummary.IsChecked Then

            If rbtnCustomer.IsChecked Then
                gv1.Columns("Route_No").IsVisible = False
                gv1.Columns("Route_Desc").IsVisible = False
                gv1.Columns("Zone_Code").IsVisible = False
                gv1.Columns("Zone Name").IsVisible = False
                gv1.Columns("Cust_Code").HeaderText = "Customer Code"
                gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            ElseIf rbtnRoute.IsChecked Then
                gv1.Columns("Cust_Code").IsVisible = False
                gv1.Columns("Customer_Name").IsVisible = False
                gv1.Columns("Zone_Code").IsVisible = False
                gv1.Columns("Zone Name").IsVisible = False
                gv1.Columns("Route_No").HeaderText = "Route Code"
                gv1.Columns("Route_Desc").HeaderText = "Route Name"
            ElseIf rbtnZone.IsChecked Then
                gv1.Columns("Cust_Code").IsVisible = False
                gv1.Columns("Customer_Name").IsVisible = False
                gv1.Columns("Route_No").IsVisible = False
                gv1.Columns("Route_Desc").IsVisible = False
                gv1.Columns("Zone_Code").HeaderText = "Zone Code"

            End If

        Else
            gv1.Columns("OP").IsVisible = False
            gv1.Columns("Document_Date").HeaderText = "Gate Pass Date"
            gv1.Columns("Document_Date").FormatString = "{0: dd/MM/yyyy}"
            gv1.Columns("Document_Date").ExcelExportFormatString = "{0:dd/MM/yyyy}"
            gv1.Columns("Due").HeaderText = "Due Amt Int.Paid"
            gv1.Columns("Route_No").IsVisible = False
            gv1.Columns("Route_Desc").IsVisible = False
            gv1.Columns("Cust_Code").IsVisible = False
            gv1.Columns("Customer_Name").IsVisible = False
            gv1.Columns("Zone_Code").IsVisible = False
            gv1.Columns("Zone Name").IsVisible = False
            If clsCommon.myLen(gv1.Columns("Sale Invoice No")) > 0 Then
                gv1.Columns("Sale Invoice No").HeaderText = "Sale Invoice No"
                gv1.Columns("Sale Invoice No").IsVisible = False
            End If
            If rbtnBothShift.IsChecked Then
                gv1.Columns("Shift").IsVisible = False
                gv1.Columns("Shift_Type").IsVisible = False
            End If
        End If

        Dim index As Integer = 0
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rbtnSummary.IsChecked Then
            index = 6
        Else
            If rbtnDemand.IsChecked Then
                index = 9
            ElseIf rbtnDispatch.IsChecked Then
                If rbtnMilkType.IsChecked OrElse rbtnProductType.IsChecked Then
                    index = 10
                ElseIf rbtnBothType.IsChecked Then
                    index = 10
                End If
            End If
        End If

        For ii As Integer = index To gv1.Columns.Count - 1
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
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSalesLedgerReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                If rbtnSummary.IsChecked = True Then
                    arrHeader.Add("Report Type : " & "Summary")
                ElseIf rbtnDetail.IsChecked = True Then
                    arrHeader.Add("Report Type : " & "Details")
                    If rbtnRoute.IsChecked Then
                        arrHeader.Add("Route Code : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "   Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
                    ElseIf rbtnCustomer.IsChecked Then
                        arrHeader.Add("Customer Code: " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "   Customer Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                    ElseIf rbtnZone.IsChecked Then
                        arrHeader.Add("Zone Code : " & clsCommon.GetMulcallString(txtZone.arrValueMember) & "   Zone Name :" & clsCommon.GetMulcallString(txtZone.arrDispalyMember) & "")
                    End If
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
                If rbtnSummary.IsChecked = True Then
                    arrHeader.Add("Report Type : " & "Summary")
                ElseIf rbtnDetail.IsChecked = True Then
                    arrHeader.Add("Report Type : " & "Details")
                    If rbtnRoute.IsChecked Then
                        arrHeader.Add("Route Code : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "   Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
                    ElseIf rbtnCustomer.IsChecked Then
                        arrHeader.Add("Customer Code: " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "   Customer Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                    ElseIf rbtnZone.IsChecked Then
                        arrHeader.Add("Zone Code : " & clsCommon.GetMulcallString(txtZone.arrValueMember) & "   Zone Name :" & clsCommon.GetMulcallString(txtZone.arrDispalyMember) & "")
                    End If

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

    Private Sub gv1_CommandCellClick(sender As Object, e As EventArgs) Handles gv1.CommandCellClick
        Try
            If gv1.CurrentColumn Is gv1.Columns("InvoiceNo") Then
                Dim frm As New frmShowInvoiceNo()
                If rbtnRoute.IsChecked Then
                    frm.FilterColumn = "Route_No"
                ElseIf rbtnCustomer.IsChecked Then
                    frm.FilterColumn = "Cust_Code"
                ElseIf rbtnZone.IsChecked Then
                    frm.FilterColumn = "Zone_Code"
                End If
                frm.Query = ShowInvoiceNo()
                frm.WindowState = FormWindowState.Normal
                frm.ShowDialog()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ShowInvoiceNo()
        Dim query As String = ""
        Try
            Dim whrcls As String = "  "
            Dim GroupByCol As String = ""
            If rbtnMilkType.IsChecked Then
                whrcls = " and TSPL_ITEM_MASTER.Is_FreshItem = 1 "
            ElseIf rbtnProductType.IsChecked Then
                whrcls = " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If
            If rbtnMorning.IsChecked Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'AM' "
            ElseIf rbtnEvening.IsChecked Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type  = 'PM' "
            End If
            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If
            If txtZone.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")"
            End If
            If rbtnRoute.IsChecked Then
                GroupByCol = "Route_No"
                query = " select Route_No,max(Route_Desc)Route_Desc , "
            ElseIf rbtnCustomer.IsChecked Then
                GroupByCol = "Cust_Code"
                query = " select Cust_Code , max(Customer_Name)Customer_Name, "
            ElseIf rbtnZone.IsChecked Then
                GroupByCol = "Zone_Code"
                query = " select Zone_Code , max([Zone Name])[Zone Name],"
            End If
            query += " Sale_Invoice_No from ( Select  TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description As [Zone Name], TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_ROUTE_MASTER.Route_Desc, Case When isnull(TSPL_SD_SHIPMENT_HEAD.Shift_Type,'') = 'AM' THEN 'AM' else 'PM' END AS Shift_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No
         From TSPL_SD_SALE_INVOICE_DETAIL Left OUTER Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left OUTER Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No Left OUTER Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
         Left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No where 2 = 2  And TSPL_SD_SALE_INVOICE_HEAD.Status = 1 and
		   CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103)  and   CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103)  
		" & whrcls & " )xx group by " & GroupByCol & ",Sale_Invoice_No "

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return query
    End Function
    Private Sub rbtnDemand_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnDemand.ToggleStateChanged
        If rbtnDemand.IsChecked Then
            chkDCSSale.Visible = False
        ElseIf rbtnDispatch.IsChecked Then
            chkDCSSale.Visible = True
        End If
    End Sub
End Class

