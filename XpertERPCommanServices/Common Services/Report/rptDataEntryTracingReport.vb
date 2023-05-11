Imports common
Imports System.ComponentModel
Imports System.IO

' Date- 30-Nov-2020  by Sanjay - Create new report 
Public Class rptDataEntryTracingReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
    Dim IsFormLoad As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
        IsFormLoad = True
    End Sub
    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        btnAll.IsChecked = True
        txtLocation.arrValueMember = Nothing
        'TxtMultiCustomerCategory.arrValueMember = Nothing
        LoadTypes()
        LoadCustomerCategory()
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Select")
        dt.Rows.Add("Sales Data")
        dt.Rows.Add("Purchase/Stores Data")
        dt.Rows.Add("SHED/BMCU/MCC")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub
    Sub LoadCustomerCategory()
        Dim qry As String = "SELECT 'Select' as [Code] UNION ALL select cust_category_code as [Code] from TSPL_CUSTOMER_CATEGORY_MASTER "
        dt = New DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        ddlCustomerCategory.DataSource = dt
        ddlCustomerCategory.ValueMember = "Code"
        ddlCustomerCategory.DisplayMember = "Code"
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If chkMarketing.Checked = True Then
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + chkMarketing.Text
        Else
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Select") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Select Report Type")
                ddlReportType.Focus()
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "Select") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Select Customer Category")
                ddlCustomerCategory.Focus()
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "SHED/BMCU/MCC") = CompairStringResult.Equal Then
                If fromDate.Value <> ToDate.Value Then
                    clsCommon.MyMessageBoxShow("Show only one day data at a time,So From and To date must be same.")
                    ToDate.Focus()
                    Exit Sub
                End If
            End If
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + clsCommon.myCstr(ddlReportType.SelectedValue)
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal Then
                PageSetupReport_ID += clsCommon.myCstr(ddlCustomerCategory.SelectedValue)
            End If
        End If

        TemplateGridview = Gv1
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim dt1 As New DataTable
            Dim qry As String = ""
            If chkMarketing.Checked = True Then
                qry = "select isnull(TSPL_ZONE_MASTER.Description,'') as Zone"
                qry += ",isnull(tt.Cash,0) as Cash,isnull(tt.Credit,0) as Credit"
                qry += ",isnull(tt.[Qty In Ltr],0) as [Qty In Ltr],isnull(tt.[Invoice Amount],0) as [Invoice Amount],isnull(tt.[Payment],0) as [Payment]" &
                       ",isnull(RecCount.[ReceiptNo],0) as [Payment Receipts] ,isnull([DispatchNo],0) as [Stock issued by Dispatch Section] "

                qry += " from ("
                qry += "select Final.Zone"

                qry += ",sum(isnull(Credit,0)) As Credit, sum(isnull(Cash,0)) As Cash"

                qry += ",sum(isnull(Final.QtyLtr,0)) as [Qty In Ltr],sum(isnull(Final.[Invoice Amount],0)) as [Invoice Amount],SUM(isnull(Final.[Receipt Amount],0)) as [Payment]"

                qry += " from(SELECT  isnull(TSPL_ZONE_MASTER.Zone_code,'') as [Zone] "
                qry += ",case when TSPL_BOOKING_MATSER.Booking_Type='CR' then 1 ELSE 0 END AS 'Credit' " &
               ",case when TSPL_BOOKING_MATSER.Booking_Type='CASH' then 1 ELSE 0 END AS 'Cash' "

                qry += ",ZZ.QtyLtr,ZZ.[Invoice Amount],ZZ.[Receipt Amount] " &
                " FROM (select YY.Cust_Code,YY.[Booking No],sum(YY.[Invoice Amount]) as [Invoice Amount],sum(YY.QtyLtr) as QtyLtr,SUM(isnull(Receipt.[Receipt Amount],0)) as [Receipt Amount] " & '"  ,count(DISTINCT Receipt.Against_Sale_No) as [Payment Receipts] ,count(DISTINCT YY.[Invoice No]) as [Stock issued by Dispatch Section] " &
                " from(select Cust_Code,[Booking No],[Invoice No], max([Invoice Amount]) [Invoice Amount], sum(QtyLtr) as QtyLtr from  " &
                " (select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_BOOKING_MATSER.Document_No as [Booking No] " &
                ",TSPL_SD_SALE_INVOICE_head.DOCUMENT_CODE as [Invoice No],TSPL_SD_SALE_INVOICE_head.total_amt as [Invoice Amount] " &
                ",(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast(TSPL_SD_SALE_INVOICE_DETAIL.qty* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) QtyLtr  " &
                " From TSPL_BOOKING_MATSER left outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No  " &
                " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No  " &
                " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_head.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code  " &
                " left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=TSPL_SD_SALE_INVOICE_head.document_code " &
                " Left Join(select Item_Code, UOM_Code, Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code And TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " &
                " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code  " &
                " Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code  " &
                " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code  " &
                "   where 2 = 2 "
                qry += " and  CONVERT(date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                qry += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY='Others' "
                qry += " and TSPL_BOOKING_MATSER.Booking_Type in ('CASH','CR')"

                If btnPosted.IsChecked Then
                    qry += " and TSPL_BOOKING_MATSER.Posted=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_BOOKING_MATSER.Posted=0  "
                End If
                qry += " )XX group by Cust_Code,[Booking No],[Invoice No] " &
                " )YY left outer join  " &
                " (select TSPL_Customer_Invoice_Head.Against_Sale_No,sum(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0)) as [Receipt Amount] from  " &
                " TSPL_Customer_Invoice_Head " &
                "  Left outer join  TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No " &
                " group by TSPL_Customer_Invoice_Head.Against_Sale_No " &
                "  )as Receipt  on Receipt.Against_Sale_No=YY.[Invoice No] " &
                " group by YY.Cust_Code,YY.[Booking No] " &
                " )ZZ " &
                "    LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=ZZ.[Booking No] " &
                "    Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=ZZ.Cust_Code  " &
                "    left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code  " &
                " )Final GROUP BY Final.Zone )tt  " &
                " left outer join (select count(distinct TSPL_RECEIPT_HEADER.Receipt_No) as [ReceiptNo],isnull(TSPL_CUSTOMER_MASTER.Zone_Code,'') as Zone_Code  " &
                " from TSPL_RECEIPT_HEADER  " &
                " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  where TSPL_RECEIPT_HEADER.receipt_type<>'A' AND TSPL_RECEIPT_HEADER.Cust_code IS NOT NULL AND TSPL_RECEIPT_HEADER.Cust_code <> '' and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_RECEIPT_HEADER.Posted='Y'  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_RECEIPT_HEADER.Posted='N'  "
                End If
                qry += " group by isnull(TSPL_CUSTOMER_MASTER.Zone_Code,''))as RecCount on RecCount.Zone_Code = tt.Zone " &
                " left outer join (select count(distinct TSPL_SD_SHIPMENT_HEAD.document_code) as [DispatchNo],isnull(TSPL_CUSTOMER_MASTER.Zone_Code,'') as Zone_Code  " &
                " from TSPL_SD_SHIPMENT_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SHIPMENT_HEAD.Customer_code " &
                "  where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.status=0  "
                End If
                qry += " group by isnull(TSPL_CUSTOMER_MASTER.Zone_Code,''))as Dispatch on Dispatch.Zone_Code = tt.Zone  " &
                    " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=tt.Zone order by  TSPL_ZONE_MASTER.Description"
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal Then

                qry = "select TSPL_ZONE_MASTER.Description as Zone"
                If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "MKT") = CompairStringResult.Equal Then
                    qry += ",isnull(tt.Cash,0) as Cash"
                Else
                    qry += ",isnull(tt.Credit,0) as Credit,isnull(tt.Card,0) as Card,isnull(tt.SO,0) as SO,isnull(tt.Cash,0) as Cash,isnull(tt.[Festive Order],0) as [Festive Order]"
                End If

                qry += ",isnull(tt.[Qty In Ltr],0) as [Qty In Ltr],isnull(tt.[Invoice Amount],0) as [Invoice Amount],isnull(tt.[Payment],0) as [Payment]" &
                       ",isnull(RecCount.[ReceiptNo],0) as [Payment Receipts] ,isnull([DispatchNo],0) as [Stock issued by Dispatch Section] "
                If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "LMS") = CompairStringResult.Equal Then
                    qry += ",tt.[Truck Sheet Generated]"
                End If

                qry += " from TSPL_ZONE_MASTER left outer join ("
                qry += "select Final.Zone"
                'If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "MKT") = CompairStringResult.Equal Then
                '    qry += ", sum(isnull(Cash,0)) As Cash"
                'Else
                qry += ",sum(isnull(Credit,0)) As Credit, sum(isnull(Card,0)) As Card, sum(isnull(SO,0)) As SO, sum(isnull(Cash,0)) As Cash, sum(isnull([Festive Order],0)) As [Festive Order]"
                'End If

                qry += ",sum(isnull(Final.QtyLtr,0)) as [Qty In Ltr],sum(isnull(Final.[Invoice Amount],0)) as [Invoice Amount],SUM(isnull(Final.[Receipt Amount],0)) as [Payment]"


                'If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "LMS") = CompairStringResult.Equal Then
                qry += ",Case when max(TruckSheetGenerate)=1 then 'Yes' else 'No' end as [Truck Sheet Generated]"
                'End If

                qry += " from(SELECT  TSPL_ZONE_MASTER.Zone_code as [Zone] "
                ' If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "MKT") = CompairStringResult.Equal Then
                '     qry += ",case when TSPL_BOOKING_MATSER.Booking_Type='CASH' then 1 ELSE 0 END AS 'Cash' "
                ' Else
                qry += ",case when TSPL_BOOKING_MATSER.Booking_Type='CR' then 1 ELSE 0 END AS 'Credit' " &
               ",case when TSPL_BOOKING_MATSER.Booking_Type='CD' then 1 ELSE 0 END AS 'Card' " &
               ",case when TSPL_BOOKING_MATSER.Booking_Type='SO' then 1 ELSE 0 END AS 'SO' " &
               ",case when TSPL_BOOKING_MATSER.Booking_Type='CASH' then 1 ELSE 0 END AS 'Cash' " &
               ",case when TSPL_BOOKING_MATSER.Booking_Type='FESTIVE ORDER' then 1 ELSE 0 END AS 'Festive Order' "
                ' End If

                qry += ",ZZ.QtyLtr,ZZ.[Invoice Amount],ZZ.[Receipt Amount] " & ',[Payment Receipts],[Stock issued by Dispatch Section]
                ", TruckSheetGenerate " &
                " FROM (select YY.Cust_Code,YY.[Booking No],sum(YY.[Invoice Amount]) as [Invoice Amount],sum(YY.QtyLtr) as QtyLtr,SUM(isnull(Receipt.[Receipt Amount],0)) as [Receipt Amount] " & '"  ,count(DISTINCT Receipt.Against_Sale_No) as [Payment Receipts] ,count(DISTINCT YY.[Invoice No]) as [Stock issued by Dispatch Section] " &
                " from(select Cust_Code,[Booking No],[Invoice No], max([Invoice Amount]) [Invoice Amount], sum(QtyLtr) as QtyLtr from  " &
                " (select TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_BOOKING_MATSER.Document_No as [Booking No] " &
                ",TSPL_SD_SALE_INVOICE_head.DOCUMENT_CODE as [Invoice No],TSPL_SD_SALE_INVOICE_head.total_amt as [Invoice Amount] " &
                ",(case when coalesce(stockLtr.Conversion_Factor,0)=0 then 0 else cast(TSPL_SD_SALE_INVOICE_DETAIL.qty* (Stock_SU.Conversion_Factor)/(coalesce(stockLtr.Conversion_Factor,1)) as numeric(18,3)) end) QtyLtr  " &
                " From TSPL_BOOKING_MATSER left outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No=TSPL_BOOKING_MATSER.Document_No  " &
                " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No  " &
                " Left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_head.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code  " &
                " left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.document_code=TSPL_SD_SALE_INVOICE_head.document_code " &
                " Left Join(select Item_Code, UOM_Code, Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =Stock_SU.Item_Code And TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =Stock_SU.UOM_Code " &
                " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='Ltr') as StockLtr on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =StockLtr.Item_Code  " &
                " Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code  " &
                " left outer join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.cust_category_code  " &
                "   where 2 = 2 "
                qry += " and  CONVERT(date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_BOOKING_MATSER.location_code in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
                'If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                '    qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code in(" + clsCommon.GetMulcallString(TxtMultiCustomerCategory.arrValueMember) + ")" + Environment.NewLine
                'End If
                qry += " and TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code='" + ddlCustomerCategory.SelectedValue + "'"

                If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "MKT") = CompairStringResult.Equal Then
                    qry += " and TSPL_BOOKING_MATSER.Booking_Type='CASH'"
                End If

                If btnPosted.IsChecked Then
                    qry += " and TSPL_BOOKING_MATSER.Posted=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_BOOKING_MATSER.Posted=0  "
                End If
                qry += " )XX group by Cust_Code,[Booking No],[Invoice No] " &
                " )YY left outer join  " &
                " (select TSPL_Customer_Invoice_Head.Against_Sale_No,sum(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0)) as [Receipt Amount] from  " &
                " TSPL_Customer_Invoice_Head " &
                "  Left outer join  TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No " &
                " group by TSPL_Customer_Invoice_Head.Against_Sale_No " &
                "  )as Receipt  on Receipt.Against_Sale_No=YY.[Invoice No] " &
                " group by YY.Cust_Code,YY.[Booking No] " &
                " )ZZ " &
                "    LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=ZZ.[Booking No] " &
                "    Left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=ZZ.Cust_Code  " &
                "    left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code  " &
                " )Final GROUP BY Final.Zone )tt on TSPL_ZONE_MASTER.Zone_Code=tt.Zone " &
                " left outer join (select count(distinct TSPL_RECEIPT_HEADER.Receipt_No) as [ReceiptNo],TSPL_CUSTOMER_MASTER.Zone_Code  " &
                " from TSPL_RECEIPT_HEADER  " &
                " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  where TSPL_RECEIPT_HEADER.receipt_type<>'A' and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_RECEIPT_HEADER.Posted='Y'  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_RECEIPT_HEADER.Posted='N'  "
                End If
                qry += " group by TSPL_CUSTOMER_MASTER.Zone_Code)as RecCount on RecCount.Zone_Code = TSPL_ZONE_MASTER.Zone_Code " &
                " left outer join (select count(distinct TSPL_SD_SHIPMENT_HEAD.document_code) as [DispatchNo],TSPL_CUSTOMER_MASTER.Zone_Code  " &
                " from TSPL_SD_SHIPMENT_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SHIPMENT_HEAD.Customer_code " &
                "  where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.status=0  "
                End If
                qry += " group by TSPL_CUSTOMER_MASTER.Zone_Code)as Dispatch on Dispatch.Zone_Code = TSPL_ZONE_MASTER.Zone_Code  " &
                    " order by  TSPL_ZONE_MASTER.Description"

            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Purchase/Stores Data") = CompairStringResult.Equal Then
                qry = "select max(INDENT_COUNT) as [Indent Placed],max(PO_COUNT) as [Purchase Order],max(SRN_COUNT) AS SRN,max(ISSUE_COUNT) AS [Stock Issued to Department] FROM  " &
                 " (select count(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No) AS PO_COUNT,0 AS INDENT_COUNT,0 AS ISSUE_COUNT,0 AS SRN_COUNT from TSPL_PURCHASE_ORDER_HEAD where 1=1 "

                qry += " and  CONVERT(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If

                If btnPosted.IsChecked Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_PURCHASE_ORDER_HEAD.Status=0  "
                End If

                qry += " UNION ALL Select 0 As PO_COUNT, count(TSPL_REQUISITION_HEAD.Requisition_Id) As INDENT_COUNT,0 As ISSUE_COUNT,0 As SRN_COUNT from TSPL_REQUISITION_HEAD where 1=1 "
                'Is_Internal='Y'
                qry += " and  CONVERT(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_REQUISITION_HEAD.Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If

                If btnPosted.IsChecked Then
                    qry += " and TSPL_REQUISITION_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_REQUISITION_HEAD.Status=0  "
                End If

                qry += " UNION ALL SELECT 0 AS PO_COUNT,0 AS INDENT_COUNT,COUNT(Doc_No) AS ISSUE_COUNT,0 AS SRN_COUNT FROM TSPL_IssueReturn_HEAD where TSPL_IssueReturn_HEAD.Doc_Type='Issue' "

                qry += " and  CONVERT(date,TSPL_IssueReturn_HEAD.Doc_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                    "CONVERT(date,TSPL_IssueReturn_HEAD.Doc_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_IssueReturn_HEAD.From_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If

                If btnPosted.IsChecked Then
                    qry += " and TSPL_IssueReturn_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_IssueReturn_HEAD.Status=0  "
                End If

                qry += " UNION ALL SELECT 0 AS PO_COUNT,0 AS INDENT_COUNT,0 AS ISSUE_COUNT,COUNT(SRN_No) AS SRN_COUNT from TSPL_SRN_HEAD WHERE 1=1 "

                qry += " and  CONVERT(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                   "CONVERT(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If

                If btnPosted.IsChecked Then
                    qry += " and TSPL_SRN_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_SRN_HEAD.Status=0  "
                End If

                qry += " )XX "

            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "SHED/BMCU/MCC") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER () over (order by TSPL_MCC_MASTER.MCC_NAME ) As SNo,TSPL_LOCATION_MASTER.Location_Desc as [Plant] " &
                       ",TSPL_MCC_MASTER.MCC_NAME as [SHED/MCC],CASE WHEN isnull(DWC.DWC_Counts,0)>0 THEN 'Yes' else 'No' end as [DayWise Collection] " &
                       ",isnull(CAN_SALE.CAN_Counts,0) as [Loose Milk Sales],isnull(TD.TD_Counts,0) as [IUT Transfer],isnull(CFS.CFS_Counts,0) as [Cattle Feed/Medicine] " &
                       " from TSPL_MCC_MASTER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.Plant_Code"

                qry += " LEFT OUTER JOIN (select COUNT(distinct Document_No) as DWC_Counts,MCC_Code from TSPL_MILK_SHIFT_UPLOADER_HEAD where 1=1 "
                qry += " and  CONVERT(date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                       "CONVERT(date,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status=0  "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " group by MCC_Code)AS DWC ON DWC.MCC_Code=TSPL_MCC_MASTER.MCC_Code "

                qry += " LEFT OUTER JOIN (select COUNT(distinct Document_No) as CAN_Counts,Location_Code from TSPL_CAN_SALE_HEAD where 1=1 "
                qry += " and  CONVERT(date,TSPL_CAN_SALE_HEAD.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                       "CONVERT(date,TSPL_CAN_SALE_HEAD.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_CAN_SALE_HEAD.Posted=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_CAN_SALE_HEAD.Posted=0  "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_CAN_SALE_HEAD.Location_Code in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " group by Location_Code)AS CAN_SALE ON CAN_SALE.Location_Code=TSPL_MCC_MASTER.MCC_Code"

                qry += " left outer join (select count(distinct Chalan_NO) as TD_Counts ,MCC_Code from tspl_mcc_dispatch_challan where 1=1 "
                qry += " and  CONVERT(date,tspl_mcc_dispatch_challan.Dispatch_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                       "CONVERT(date,tspl_mcc_dispatch_challan.Dispatch_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and tspl_mcc_dispatch_challan.isPosted=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and tspl_mcc_dispatch_challan.isPosted=0  "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and tspl_mcc_dispatch_challan.MCC_Code in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " group by MCC_Code)as TD on TD.MCC_Code=TSPL_MCC_MASTER.MCC_Code"

                qry += " left outer join (select count(distinct document_code) as CFS_Counts,Bill_To_Location from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='MCC' "
                qry += " and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND " + Environment.NewLine &
                       "CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
                If btnPosted.IsChecked Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Status=1  "
                ElseIf btnUnposted.IsChecked Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Status=0  "
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
                qry += " group by Bill_To_Location)as CFS on CFS.Bill_To_Location=TSPL_MCC_MASTER.MCC_Code "
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " where TSPL_MCC_MASTER.MCC_Code in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
                End If
            End If

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                btnGo.Enabled = False
                SetGridFormat()
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormat()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.AutoSizeRows = True
        Gv1.EnableFiltering = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If chkMarketing.Checked = True Then
            Dim Qty4 As New GridViewSummaryItem("Cash", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty4)
            Dim Qty1 As New GridViewSummaryItem("Credit", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty1)
            Dim Qty6 As New GridViewSummaryItem("Qty In Ltr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty6)
            Dim Qty7 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty7)
            Dim Qty8 As New GridViewSummaryItem("Payment", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty8)
            Dim Qty9 As New GridViewSummaryItem("Payment Receipts", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty9)
            Dim Qty10 As New GridViewSummaryItem("Stock issued by Dispatch Section", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty10)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal Then
            If clsCommon.CompairString(ddlCustomerCategory.SelectedValue, "MKT") = CompairStringResult.Equal Then
                Dim Qty4 As New GridViewSummaryItem("Cash", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty4)
            Else
                Dim Qty1 As New GridViewSummaryItem("Credit", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty1)
                Dim Qty2 As New GridViewSummaryItem("Card", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty2)
                Dim Qty3 As New GridViewSummaryItem("SO", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty3)
                Dim Qty4 As New GridViewSummaryItem("Cash", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty4)
                Dim Qty5 As New GridViewSummaryItem("Festive Order", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty5)
            End If

            Dim Qty6 As New GridViewSummaryItem("Qty In Ltr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty6)
            Dim Qty7 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty7)
            Dim Qty8 As New GridViewSummaryItem("Payment", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty8)
            Dim Qty9 As New GridViewSummaryItem("Payment Receipts", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty9)
            Dim Qty10 As New GridViewSummaryItem("Stock issued by Dispatch Section", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty10)

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "SHED/BMCU/MCC") = CompairStringResult.Equal Then
            Dim Qty1 As New GridViewSummaryItem("Loose Milk Sales", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty1)
            Dim Qty2 As New GridViewSummaryItem("IUT Transfer", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty2)
            Dim Qty3 As New GridViewSummaryItem("Cattle Feed/Medicine", "", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty3)

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.BestFitColumns()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub



    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If TxtMultiCustomerCategory.arrDispalyMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrDispalyMember))
            'End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal Then
                arrHeader.Add("Customer Category : " + clsCommon.myCstr(ddlCustomerCategory.SelectedValue))
            End If

            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Select") <> CompairStringResult.Equal Then
                arrHeader.Add("Name : " & clsCommon.myCstr(ddlReportType.SelectedValue))
            End If
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDataEntryTracingReport & "'"))
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Data Entry Tracing Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Data Entry Tracing Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CuCaMulSel1", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub TxtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Tr1TypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub DdlReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            If IsFormLoad = False Then
                Exit Sub
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Sales Data") = CompairStringResult.Equal Then
                ddlCustomerCategory.Enabled = True
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Purchase/Stores Data") = CompairStringResult.Equal Then
                ddlCustomerCategory.Enabled = False
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "SHED/BMCU/MCC") = CompairStringResult.Equal Then
                ddlCustomerCategory.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ChkMarketing_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMarketing.ToggleStateChanged
        If chkMarketing.Checked = True Then
            Reset()
            txtLocation.Enabled = False
            ddlReportType.Enabled = False
            ddlCustomerCategory.Enabled = False
        Else
            Reset()
            txtLocation.Enabled = True
            ddlReportType.Enabled = True
            ddlCustomerCategory.Enabled = True
        End If
    End Sub
End Class
