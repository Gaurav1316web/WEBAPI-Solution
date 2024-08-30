''Last Modify by priti 23/10/2013 02:00 pm  for BM00000000877,BM00000000948
'--UPdation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001230]
Imports common
Imports XpertERPEngine

Public Class FrmDiscountAnalysis
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim refreshGrid As String = "F"
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDiscountAnalysis)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Sub LoadLocation()
        cbgLoc.DataSource = clsLocation.GetLocationSegments()
        cbgLoc.ValueMember = "Code"
        cbgLoc.DisplayMember = "Name"
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master WHERE 2=2 "
        cbgCust.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCust.ValueMember = "Customer Code"
        cbgCust.DisplayMember = "Customer Name"
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Private Sub FrmDiscountAnalysis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        chkLocationAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkRouteAll.IsChecked = True
        LoadCustomer()
        LoadLocation()
        LoadRoute()
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        SetUserMgmtNew()

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocAll.IsChecked
    End Sub

    'Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
    '    cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    'End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        GV1.DataSource = Nothing
        gv1.Columns.Clear()
        GV1.Rows.Clear()
        Reset()
    End Sub
    Private Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        chkLocationAll.IsChecked = True
        chkCustAll.IsChecked = True
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbSku.IsChecked = True Then
            VarID += "_SK"
        Else rdbpack.IsChecked = True
            VarID += "_PA"
        End If
        If rdbSummary.IsChecked = True Then
            VarID += "_SU"
        ElseIf rdbDiscSummary.IsChecked = True Then
            VarID += "_DS"
        ElseIf rdbDetail.IsChecked = True Then
            VarID += "_DE"
        End If
        GV1.VarID = VarID
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GetReportGridID()
        Print()
    End Sub

    Private Sub Print()
        Try
            Dim strInvItem, strRetItem, strSeq As String
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkselectcustomer.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return

            End If
            GV1.EnableFiltering = True
            Dim dt As DataTable

            If rdbSku.IsChecked Then
                strInvItem = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strRetItem = "TSPL_SALE_return_DETAIL.Item_Code"
                strSeq = "Sku_Seq"
            Else
                strInvItem = "TSPL_ITEM_DETAILS.Class_Desc"
                strRetItem = "TSPL_ITEM_DETAILS.Class_Desc"
                strSeq = "Pack_Seq"
            End If

            Dim str1 As String = "select Sale_Invoice_No,convert(date,Sale_Invoice_Date,103) as Sale_Invoice_Date,Item,seq, " & _
            "Route_No,Route_Desc,Cust_Code,Customer_Name,Location_Code,Location_Desc, convert(decimal(18,2),sum(GrossQty)) as GrossQty," & _
            "convert(decimal(18,2),sum(FOCQty)) as FOCQty, " & _
            "convert(decimal(18,2),sum(NetQty)) as NetQty, " & _
            "convert(decimal(18,2),sum(TradeQty)) as TradeQty, " & _
            "convert(decimal(18,2),sum(TradeAmt)) as TradeAmt, " & _
            "convert(decimal(18,2),sum(TargetQty)) as TargetQty, " & _
            "convert(decimal(18,2),sum(TargetAmt)) as TargetAmt, " & _
            "convert(decimal(18,2),sum(CustDiscQty)) as CustDiscQty, " & _
           "convert(decimal(18,2),sum(CustDiscAmt)) as CustDiscAmt, " & _
          "convert(decimal(18,2),sum(TradeMargin)) as TradeMargin, " & _
                "convert(decimal(18,2),sum(PartyDiscount)) as PartyDiscount, " & _
                "convert(decimal(18,2),sum(ExtraDiscount)) as ExtraDiscount, " & _
                "convert(decimal(18,2),sum(DistMargin)) as DistMargin, " & _
                "convert(decimal(18,2),sum(AgencyMargin)) as AgencyMargin, " & _
                "convert(decimal(18,2),sum(TPTandOthers)) as TPTandOthers, " & _
                "convert(decimal(18,2),sum(SchemeDisc)) as SchemeDisc,  " & _
                "convert(decimal(18,2),sum(PC1)) as PC1, " & _
                "convert(decimal(18,2),sum(pc2)) as pc2, " & _
           "convert(decimal(18,2),sum(TradeQty + TargetQty + CustDiscQty)) as TotQty, " & _
            "convert(decimal(18,2),sum(TradeAmt + TargetAmt + CustDiscAmt)) as TotAmt  from   " & _
            "(select Item,seq,Sale_Invoice_No,Sale_Invoice_Date,Route_No,Route_Desc,Cust_Code,Customer_Name,Location_Code,Location_Desc,  " & _
            "InvQty  AS GrossQty, " & _
            "case when Scheme_Item='Y'  then  InvQty else 0 end AS FOCQty, " & _
            "case when Scheme_Item='N'  then  InvQty else 0 end AS NetQty, " & _
            "case when Scheme_Item='Y' and Discount_Code = '' then  InvQty else 0 end AS TradeQty,  " & _
            "case when Scheme_Item='Y' and Discount_Code = '' then  FOCAMt else 0 end AS TradeAmt,  " & _
            "case when Scheme_Item='Y' and Discount_Code <> '' then  InvQty else 0 end AS TargetQty,  " & _
            "case when Scheme_Item='Y' and Discount_Code <> '' then  FOCAMt else 0 end AS TargetAmt,  " & _
            "case when Total_Disc_Amt > 0 then InvQty else 0 end as CustDiscQty,case when Total_Disc_Amt > 0 then  " & _
            "Total_Disc_Amt else 0 end as CustDiscAmt , " & _
            "TradeMargin * InvQty as TradeMargin,PartyDiscount* InvQty as PartyDiscount,ExtraDiscount* InvQty as ExtraDiscount, " & _
            "DistMargin* InvQty as DistMargin,AgencyMargin* InvQty as AgencyMargin, " & _
            "TPTandOthers* InvQty as TPTandOthers,SchemeDisc* InvQty as SchemeDisc, " & _
            "PC1* InvQty as PC1,PC2* InvQty as PC2 from ("

            str1 += "select " & strInvItem & " as Item," & strSeq & " as seq,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Sale_Invoice_Date,TSPL_ROUTE_MASTER.Route_No, " & _
            "TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_INVOICE_HEAD.Cust_Code,Customer_Name,Location_Code, " & _
            "Location_Desc,Scheme_Item,Discount_Code,Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor as InvQty, " & _
            "isnull(((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((TSPL_SALE_INVOICE_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL.Price_Amount4 + TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL.Price_Amount9))),0) AS FOCAMt, " & _
            "Total_Disc_Amt, " & _
            " TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS [TradeMargin], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 AS [PartyDiscount], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 AS [ExtraDiscount], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 AS [DistMargin], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 AS [AgencyMargin], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 AS [TPTandOthers], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 AS [SchemeDisc], " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 AS PC1, " & _
           "TSPL_SALE_INVOICE_DETAIL.Price_Amount9 AS PC2, " & _
            "TSPL_SALE_INVOICE_DETAIL.Price_Amount10 AS TPT " & _
            "from TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on  " & _
            "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
            "TSPL_ROUTE_MASTER on TSPL_SALE_INVOICE_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
            "TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "left outer join TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "where TSPL_ITEM_DETAILS.Class_Name='size' and " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and is_Post='Y' "
            str1 += "union all "
            str1 += "select " & strRetItem & " as Item," & strSeq & "  as seq,TSPL_SALE_return_HEAD.Sale_Return_No as Sale_Invoice_No,Sale_Return_Date as Sale_Invoice_Date, " & _
            "TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_return_HEAD.Cust_Code,Customer_Name,Location_Code, " & _
            "Location_Desc,Scheme_Item,Discount_Code, - (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as InvQty, " & _
            "- (isnull(((TSPL_SALE_return_DETAIL.Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((TSPL_SALE_return_DETAIL.MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (TSPL_SALE_return_DETAIL.Price_Amount1 + TSPL_SALE_return_DETAIL.Price_Amount2 + TSPL_SALE_return_DETAIL.Price_Amount3 + TSPL_SALE_return_DETAIL.Price_Amount4 + TSPL_SALE_return_DETAIL.Price_Amount5 + TSPL_SALE_return_DETAIL.Price_Amount6 + TSPL_SALE_return_DETAIL.Price_Amount7 + TSPL_SALE_return_DETAIL.Price_Amount8 + TSPL_SALE_return_DETAIL.Price_Amount9))),0)) AS FOCAMt, " & _
            "-Total_Disc_Amt, " & _
            "TSPL_SALE_return_DETAIL.Price_Amount1 AS [TradeMargin], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount2 AS [PartyDiscount], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount3 AS [ExtraDiscount], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount4 AS [DistMargin], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount5 AS [AgencyMargin], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount6 AS [TPTandOthers], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount7 AS [SchemeDisc], " & _
            "TSPL_SALE_return_DETAIL.Price_Amount8 AS PC1, " & _
            "TSPL_SALE_return_DETAIL.Price_Amount9 AS PC2, " & _
             "TSPL_SALE_return_DETAIL.Price_Amount10 AS TPT " & _
            "from TSPL_SALE_return_HEAD left outer join TSPL_SALE_return_DETAIL on " & _
            "TSPL_SALE_return_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join " & _
            "TSPL_ROUTE_MASTER on TSPL_SALE_return_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
            "TSPL_CUSTOMER_MASTER on TSPL_SALE_return_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join " & _
            "TSPL_LOCATION_MASTER on TSPL_SALE_return_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
            "TSPL_SALE_return_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "left outer join TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_DETAILS.Item_Code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_ITEM_DETAILS.Class_Name='size' and  " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103)   and is_Post='Y' "

            str1 += ") aa where 2=2   "
            If chkLocationSelect.IsChecked Then
                str1 += " and Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")) "
            End If
            If chkCustSelect.IsChecked Then
                str1 += " and Cust_Code in (" + clsCommon.GetMulcallString(cbgCust.CheckedValue) + ") "
            End If
            If chkRouteSelect.IsChecked Then
                str1 += " and Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If
            str1 += ") final   group by Sale_Invoice_No,Sale_Invoice_Date,Route_No,Route_Desc,Location_Code,Location_Desc,Cust_Code,Customer_Name,Item,seq  "


            If rdbSummary.IsChecked Then
                str1 = "select Route_No,Route_Desc,Cust_Code,Customer_Name,Location_Code,Location_Desc,convert(decimal(18,2),sum(GrossQty)) as GrossQty, " & _
                "convert(decimal(18,2),sum(FOCQty)) as FOCQty, convert(decimal(18,2),sum(NetQty)) as NetQty, " & _
                "convert(decimal(18,2),sum(TradeQty)) as TradeQty, convert(decimal(18,2),sum(TradeAmt)) as TradeAmt, " & _
                "convert(decimal(18,2),sum(TargetQty)) as TargetQty, convert(decimal(18,2),sum(TargetAmt)) as TargetAmt, " & _
                "convert(decimal(18,2),sum(CustDiscQty)) as CustDiscQty, convert(decimal(18,2),sum(CustDiscAmt)) as CustDiscAmt," & _
                "convert(decimal(18,2),sum(TradeMargin)) as TradeMargin, " & _
                "convert(decimal(18,2),sum(PartyDiscount)) as PartyDiscount, " & _
                "convert(decimal(18,2),sum(ExtraDiscount)) as ExtraDiscount, " & _
                "convert(decimal(18,2),sum(DistMargin)) as DistMargin, " & _
                "convert(decimal(18,2),sum(AgencyMargin)) as AgencyMargin, " & _
                "convert(decimal(18,2),sum(TPTandOthers)) as TPTandOthers, " & _
                "convert(decimal(18,2),sum(SchemeDisc)) as SchemeDisc,  " & _
                "convert(decimal(18,2),sum(PC1)) as PC1, " & _
                "convert(decimal(18,2),sum(pc2)) as pc2, " & _
                "convert(decimal(18,2),sum(TradeQty + TargetQty + CustDiscQty)) as TotQty, " & _
                "convert(decimal(18,2),sum(TradeAmt + TargetAmt + CustDiscAmt)) as TotAmt from ( " & str1 & " )  xx group by Route_No,Route_Desc,Location_Code,Location_Desc,Cust_Code,Customer_Name  "

            ElseIf rdbDetail.IsChecked Then
                str1 += " ORDER BY final.Sale_Invoice_Date "

            ElseIf rdbDiscSummary.IsChecked Then
                str1 = "select Route_No,Route_Desc,Cust_Code,Customer_Name,Location_Code,Location_Desc,convert(decimal(18,2),sum(GrossQty)) as GrossQty, " & _
               "convert(decimal(18,2),sum(FOCQty)) as FOCQty, convert(decimal(18,2),sum(NetQty)) as NetQty, " & _
               "convert(decimal(18,2),sum(TradeQty)) as TradeQty, convert(decimal(18,2),sum(TradeAmt)) as TradeAmt, " & _
               "convert(decimal(18,2),sum(TargetQty)) as TargetQty, convert(decimal(18,2),sum(TargetAmt)) as TargetAmt, " & _
               "convert(decimal(18,2),sum(CustDiscQty)) as CustDiscQty, convert(decimal(18,2),sum(CustDiscAmt)) as CustDiscAmt," & _
               "convert(decimal(18,2),sum(PartyDiscount)) as PartyDiscount, " & _
               "convert(decimal(18,2),sum(ExtraDiscount)) as ExtraDiscount, " & _
               "convert(decimal(18,2),sum(AgencyMargin)) as AgencyMargin, " & _
               "convert(decimal(18,2),sum(TPTandOthers)) as TPTandOthers, " & _
               "convert(decimal(18,2),sum(SchemeDisc)) as SchemeDisc,  " & _
               "convert(decimal(18,2),sum(PC1)) as PC1, " & _
               "convert(decimal(18,2),sum(pc2)) as pc2, " & _
               "convert(decimal(18,2),sum(TradeQty + TargetQty + CustDiscQty)) as TotQty, " & _
               "convert(decimal(18,2),sum(TradeAmt + TargetAmt + CustDiscAmt + PartyDiscount)) as TotAmt, " & _
               "case when  sum(GrossQty) > 0 then convert(decimal(18,2),sum(TradeAmt + TargetAmt + CustDiscAmt + PartyDiscount)/sum(GrossQty) ) else 0 end as CPC, " & _
               "convert(decimal(18,2),sum(TradeMargin)) as TradeMargin, " & _
               "convert(decimal(18,2),sum(DistMargin)) as DistMargin " & _
               "from ( " & str1 & " )  xx group by Route_No,Route_Desc,Location_Code,Location_Desc,Cust_Code,Customer_Name  "
            End If
            dt = clsDBFuncationality.GetDataTable(str1)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                SetGridFormationOFGV1()

            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked Then

            GV1.Columns("Sale_Invoice_No").IsVisible = True
            GV1.Columns("Sale_Invoice_No").Width = 100
            GV1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            GV1.Columns("Sale_Invoice_Date").IsVisible = True
            GV1.Columns("Sale_Invoice_Date").Width = 100
            GV1.Columns("Sale_Invoice_Date").FormatString = "{0:d}"
            GV1.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"

            GV1.Columns("Item").IsVisible = True
            GV1.Columns("Item").Width = 100
            GV1.Columns("Item").HeaderText = "Item"

            GV1.Columns("Seq").IsVisible = True
            GV1.Columns("Seq").Width = 100
            GV1.Columns("Seq").HeaderText = "Seq No"
        End If

        GV1.Columns("Route_No").IsVisible = True
        GV1.Columns("Route_No").Width = 100
        GV1.Columns("Route_No").HeaderText = "Route No"

        GV1.Columns("Route_Desc").IsVisible = True
        GV1.Columns("Route_Desc").Width = 100
        GV1.Columns("Route_Desc").HeaderText = "Route Desc"

        GV1.Columns("Cust_Code").IsVisible = True
        GV1.Columns("Cust_Code").Width = 100
        GV1.Columns("Cust_Code").HeaderText = "Distributor Code"

        GV1.Columns("Customer_Name").IsVisible = True
        GV1.Columns("Customer_Name").Width = 100
        GV1.Columns("Customer_Name").HeaderText = "Distributor Name"

        GV1.Columns("Location_Code").IsVisible = True
        GV1.Columns("Location_Code").Width = 100
        GV1.Columns("Location_Code").HeaderText = "Location"

        GV1.Columns("Location_Desc").IsVisible = True
        GV1.Columns("Location_Desc").Width = 100
        GV1.Columns("Location_Desc").HeaderText = "Location Desc"

        GV1.Columns("TradeQty").IsVisible = True
        GV1.Columns("TradeQty").Width = 120
        GV1.Columns("TradeQty").HeaderText = "Trade Scheme Settled Qty"

        GV1.Columns("TradeAmt").IsVisible = True
        GV1.Columns("TradeAmt").Width = 120
        GV1.Columns("TradeAmt").HeaderText = "Trade Scheme Settled Amt"

        GV1.Columns("TargetQty").IsVisible = True
        GV1.Columns("TargetQty").Width = 120
        GV1.Columns("TargetQty").HeaderText = "Target Inc. Qty"

        GV1.Columns("TargetAmt").IsVisible = True
        GV1.Columns("TargetAmt").Width = 120
        GV1.Columns("TargetAmt").HeaderText = "Target Inc. Amt"

        GV1.Columns("CustDiscQty").IsVisible = True
        GV1.Columns("CustDiscQty").Width = 120
        GV1.Columns("CustDiscQty").HeaderText = "% Disc Qty"

        GV1.Columns("CustDiscAmt").IsVisible = True
        GV1.Columns("CustDiscAmt").Width = 120
        GV1.Columns("CustDiscAmt").HeaderText = "% Disc Amt"

        GV1.Columns("TotQty").IsVisible = True
        GV1.Columns("TotQty").Width = 120
        GV1.Columns("TotQty").HeaderText = "Grand Tot Qty"

        GV1.Columns("TotAmt").IsVisible = True
        GV1.Columns("TotAmt").Width = 120
        GV1.Columns("TotAmt").HeaderText = "Grand Tot Amt"

        GV1.Columns("GrossQty").IsVisible = True
        GV1.Columns("GrossQty").Width = 120
        GV1.Columns("GrossQty").HeaderText = "Gross Qty"

        GV1.Columns("FOCQty").IsVisible = True
        GV1.Columns("FOCQty").Width = 120
        GV1.Columns("FOCQty").HeaderText = "FOC"

        GV1.Columns("NetQty").IsVisible = True
        GV1.Columns("NetQty").Width = 120
        GV1.Columns("NetQty").HeaderText = "Net Qty"



        GV1.Columns("TradeMargin").IsVisible = True
        GV1.Columns("TradeMargin").Width = 120
        GV1.Columns("TradeMargin").HeaderText = "Trade Margin"

        GV1.Columns("PartyDiscount").IsVisible = True
        GV1.Columns("PartyDiscount").Width = 120
        GV1.Columns("PartyDiscount").HeaderText = "Party Discount"

        GV1.Columns("DistMargin").IsVisible = True
        GV1.Columns("DistMargin").Width = 120
        GV1.Columns("DistMargin").HeaderText = "Dist Margin"

        GV1.Columns("PC1").IsVisible = True
        GV1.Columns("PC1").Width = 120
        GV1.Columns("PC1").HeaderText = "PC1"

        If rdbDiscSummary.IsChecked Then
            GV1.Columns("PC1").IsVisible = False

            GV1.Columns("CPC").IsVisible = True
            GV1.Columns("CPC").Width = 120
            GV1.Columns("CPC").HeaderText = "CPC"
        End If

        'GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Route format ""{0}: {1}"" Group By Route_No"))
        'GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_Desc as Route format ""{0}: {1}"" Group By Route_Desc"))
        'GV1.ShowGroupPanel = False
        GV1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0


        Dim item8 As New GridViewSummaryItem("TradeQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("TradeAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TargetQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("TargetAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("CustDiscQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("CustDiscAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("TotQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("TotAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("GrossQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("FOCQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("NetQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        Dim TradeMargin As New GridViewSummaryItem("TradeMargin", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TradeMargin)
        Dim PartyDiscount As New GridViewSummaryItem("PartyDiscount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(PartyDiscount)
        Dim ExtraDiscount As New GridViewSummaryItem("ExtraDiscount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ExtraDiscount)
        Dim DistMargin As New GridViewSummaryItem("DistMargin", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(DistMargin)
        Dim AgencyMargin As New GridViewSummaryItem("AgencyMargin", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(AgencyMargin)
        Dim TPTandOthers As New GridViewSummaryItem("TPTandOthers", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TPTandOthers)
        Dim SchemeDisc As New GridViewSummaryItem("SchemeDisc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeDisc)
        Dim PC1 As New GridViewSummaryItem("PC1", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(PC1)
        Dim pc2 As New GridViewSummaryItem("pc2", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(pc2)

        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkCustSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCust.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLoc.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Discount Analysis Report", GV1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Discount Analysis Report", GV1, arrHeader, "Discount Analysis Report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Excel.Click
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Pdf.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCust.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLoc.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
End Class
