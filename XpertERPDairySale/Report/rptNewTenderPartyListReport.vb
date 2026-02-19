Imports common
Imports System.IO
Public Class rptNewTenderPartyListReport
    Inherits FrmMainTranScreen
    Private Sub rptNewTenderPartyListReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Sub LoadShiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "AM"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "PM"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)
        cmbShift.ValueMember = "Code"
        cmbShift.DisplayMember = "Name"
        cmbShift.DataSource = dt
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())

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
        LoadData(False)
    End Sub
    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadShiftType()
        cmbShift.SelectedIndex = 0
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        gbStatus.Enabled = val
    End Sub

    Private Sub LoadData(isprint As Boolean)
        Try
            Dim strPrintqry As String = ""
            If clsCommon.CompairString(cmbShift.SelectedValue, "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Please select shift type", Me.Text)
                Exit Sub
            End If
            If isprint Then
                strPrintqry = " TSPL_COMPANY_MASTER.Comp_Name,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "' as Date, '" & txtDate.Value.AddDays(-1).Day & "' as Previous_Day,'" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2), "dd/MM/yyyy") & "' as Previous_Date,'" & objCommonVar.CurrentUser & "' as User_Code, "
            End If
            Dim qry As String = " Select *,isnull((op+Sale_Amt+Debit_Amt+Credit_Amt-RTGS_Amt-Return_Amt),0) as Closing_Bal from 
(Select Customer_Code,
Isnull(Sum(Case when (RI)=2 THEN (Total_Amt) END),0) AS Gurantee_Amt,
--Case when max(RI)=2 THEN Sum(Total_Amt) END AS Gurantee_Amt,
Sum(Total_Amt * case when RI IN(1,10,4) then 1 else case when RI In(7) then -1 else 0 end end 
* case when convert(date,Supply_Date,103) < '11/Jan/2026 ' then 1 else 0 end) as OP,
sum(case when (RI)=6  and convert(date,Supply_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'  then Total_Amt else 0 end )as Security_Amt,
sum(case when (RI)=3  and convert(date,Supply_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'  then Total_Amt else 0 end )as Refund_Amt,
sum(case when (RI)=7  and convert(date,Supply_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'  then Total_Amt else 0 end )as RTGS_Amt,
sum(case when (RI)=8  and convert(date,Supply_Date,103) = '" & txtDate.Value.AddDays(-1).Day & "'  then Total_Amt else 0 end )as Closing_Rec_Amt,

Sum ( case when (RI)=1 and  --((Convert(Date, Supply_Date,103) = Convert(Date, '10/Jan/2026',103))
--OR 
(Convert(Date, Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "',103) )
--)
then Total_Amt else  0 end  ) as Sale_Amt, 

Sum ( case when (RI)=11 and (Convert(Date, Supply_Date,103) = Convert(Date, '" & txtDate.Value.AddDays(-1).Day & "',103) )
--OR (Convert(Date, Supply_Date,103) = Convert(Date, '10/Jan/2026 ',103) 
 then Total_Amt else 0 end  ) as Closing_Sale_Amt,
Sum(case when (RI)=12 and Convert(Date, Supply_Date,103) = Convert(Date, '" & txtDate.Value.AddDays(-1).Day & "',103)   then Total_Amt else 0  end )as Estimated_Avg_Sale,
Sum(case when (RI)=5 and Convert(Date, Supply_Date,103) = Convert(Date, '" & txtDate.Value.AddDays(-1).Day & "',103)   then Total_Amt else 0  end )as PrevDay_Return_Amt,
Sum(case when (RI)=9 and Convert(Date, Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & ",103)   then Total_Amt else 0  end )as Return_Amt,
--Sum(Case when (RI)=5 THEN (Total_Amt) END) AS Return_Amt,
sum(case when (RI)=10 and convert(date,Supply_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "  then Total_Amt else 0 end )as Credit_Amt,
sum(case when (RI)=4 and convert(date,Supply_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "  then Total_Amt else 0 end )as Debit_Amt

from (
Select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,case when  Shift_Type='PM' then DATEADD(DAY, 1, Supply_Date) else Supply_Date end Supply_Date,
Shift_Type,TSPL_CUSTOMER_MASTER.Zone_Code as Zone_Code1,Route.Zone_Code,
TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,NULL AS AgainstScrapReturn,1 as RI 
from TSPL_SD_SALE_INVOICE_HEAD
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No 
LEFT JOIN TSPL_ROUTE_MASTER Route ON Route.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No 
where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='D208' and
CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "' 
			and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'

			union all

			Select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,case when  Shift_Type='PM' then DATEADD(DAY, 1, Supply_Date) else Supply_Date end Supply_Date,
Shift_Type,TSPL_CUSTOMER_MASTER.Zone_Code as Zone_Code1,Route.Zone_Code,
TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,NULL AS AgainstScrapReturn,11 as RI 
from TSPL_SD_SALE_INVOICE_HEAD
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No 
LEFT JOIN TSPL_ROUTE_MASTER Route ON Route.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No 
where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='D208' and
CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "' 
			and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'

			union all

			Select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,case when  Shift_Type='PM' then DATEADD(DAY, 1, Supply_Date) else Supply_Date end Supply_Date,
Shift_Type,TSPL_CUSTOMER_MASTER.Zone_Code as Zone_Code1,Route.Zone_Code,
TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,NULL AS AgainstScrapReturn,12 as RI 
from TSPL_SD_SALE_INVOICE_HEAD
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No 
LEFT JOIN TSPL_ROUTE_MASTER Route ON Route.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No 
where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='D208' and
CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "' 
			and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "'
            union all

select TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code as Customer_Code,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & " as Supply_Date,NULL AS Shift_Type,null as Zone_Code1,null as Zone_Code,
sum(Type * amount) Total_Amt,NULL AS AgainstScrapReturn,2 as RI
 from ( 
 Select 1 AS Type,vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & ", 103) BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103) and status='Y'
	        
  union all 
 Select -1 as Type, vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RT' AND Receiving_code 
 IN ( Select DocNo from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & ", 103) 
 BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103)  and status='Y' ) 
 AND  CONVERT(date, Date, 103)  < '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & " and status='Y' 
 ) xx 
 left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code=xx.vendor_code
 where TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code='D208'
 group by TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code

 union all
  select cust_code as Customer_Code,Receipt_Date as Supply_Date,Receipt_Type as  Shift_Type, SecurityDeposit AS Zone_Code1,
  NULL AS Zone_Code,Receipt_Amount AS Total_Amt,NULL AS AgainstScrapReturn,6 AS RI from TSPL_RECEIPT_HEADER
  where Cust_Code='D208' and SecurityDeposit='Y' and Receipt_Type='P'

   union all
  select cust_code as Customer_Code,Receipt_Date as Supply_Date,Receipt_Type as  Shift_Type, SecurityDeposit AS Zone_Code1,
  NULL AS Zone_Code,Receipt_Amount AS Total_Amt,NULL AS AgainstScrapReturn,3 AS RI from TSPL_RECEIPT_HEADER
  where Cust_Code='D208' and SecurityDeposit='Y' and Receipt_Type='F' 

   union all
  select cust_code as Customer_Code,Receipt_Date as Supply_Date,Receipt_Type as  Shift_Type, SecurityDeposit AS Zone_Code1,
  NULL AS Zone_Code,Receipt_Amount AS Total_Amt,NULL AS AgainstScrapReturn,7 AS RI from TSPL_RECEIPT_HEADER
  where Cust_Code='D208' and SecurityDeposit='N'   

     union all
  select cust_code as Customer_Code,Receipt_Date as Supply_Date,Receipt_Type as  Shift_Type, SecurityDeposit AS Zone_Code1,
  NULL AS Zone_Code,Receipt_Amount AS Total_Amt,NULL AS AgainstScrapReturn,8 AS RI from TSPL_RECEIPT_HEADER
  where Cust_Code='D208'   and SecurityDeposit='N'

  union all

 select Customer_Code,Posting_Date as Supply_Date,Document_Type as  Shift_Type, Against_Sale_Return_No AS Zone_Code1, 
 Against_MCC_Material_Sale_Return AS Zone_Code,Document_Total AS Total_Amt,AgainstScrapReturn AS AgainstScrapReturn,4 AS RI 
 --AgainstScrapReturn 
 from TSPL_Customer_Invoice_Head where  TSPL_Customer_Invoice_Head.Customer_Code='D208' and Document_Type='D' 
 and (Against_Sale_Return_No is null or Against_Sale_Return_No='') and (Against_MCC_Material_Sale_Return is null or Against_MCC_Material_Sale_Return='')
and (AgainstScrapReturn is null or AgainstScrapReturn='')
 
  union all

 select Customer_Code,Posting_Date as Supply_Date,Document_Type as  Shift_Type, Against_Sale_Return_No AS Zone_Code1, 
 Against_MCC_Material_Sale_Return AS Zone_Code,Document_Total AS Total_Amt,AgainstScrapReturn AS AgainstScrapReturn,10 AS RI 
 --AgainstScrapReturn 
 from TSPL_Customer_Invoice_Head where  TSPL_Customer_Invoice_Head.Customer_Code='D208' and Document_Type='C' 
 and (Against_Sale_Return_No is null or Against_Sale_Return_No='') and (Against_MCC_Material_Sale_Return is null or Against_MCC_Material_Sale_Return='')
and (AgainstScrapReturn is null or AgainstScrapReturn='')

 union all

 (Select Customer_Code as Customer_Code,Document_Date as Supply_Date,NULL AS Shift_Type,null as Zone_Code1,null as Zone_Code,
 Cast (Sum(Return_Amt) as decimal(18,2)) Total_Amt,NULL AS AgainstScrapReturn,5 as RI
from (Select Customer_Code ,Document_Date,(Total_Amt)Return_Amt from TSPL_SD_SALE_RETURN_HEAD --where convert(date,Document_Date,103) = '12/Jan/2026 ' group by Customer_Code
union all
Select cust_Code ,Return_ship_Date as Document_Date,(Doc_Amt)Return_Amt from TSPL_SCRAPSALE_HEAD_RETURN --where convert(date,Return_ship_Date,103) = '12/Jan/2026 ' group by cust_Code
)XX where  Customer_Code='D208' Group by Customer_Code,Document_Date ) 

 union all

 (Select Customer_Code as Customer_Code,Document_Date as Supply_Date,NULL AS Shift_Type,null as Zone_Code1,null as Zone_Code,
 Cast (Sum(Return_Amt) as decimal(18,2)) Total_Amt,NULL AS AgainstScrapReturn,9 as RI
from (Select Customer_Code ,Document_Date,(Total_Amt)Return_Amt from TSPL_SD_SALE_RETURN_HEAD 
union all
Select cust_Code ,Return_ship_Date as Document_Date,(Doc_Amt)Return_Amt from TSPL_SCRAPSALE_HEAD_RETURN 
)XX where  Customer_Code='D208' Group by Customer_Code,Document_Date ) 
 
 )XX GROUP BY Customer_Code) XY

 "
            If rbtnCreditCustomer.IsChecked Then
                qry += " and Credit_Customer='Y' "
            ElseIf rbtnDistributor.IsChecked Then
                qry += " and IsDistributor='Y' "
            End If
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                qry += " and xxfinal.Cust_Type_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and xxfinal.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If

            ''''" & Environment.NewLine & "LEFT JOIN (Select Cust_Code,(Security_Amt-Refund_Amt)Security_Amt,RTGS_Amt,Closing_Rec_Amt from  (select Cust_Code, sum(case when SecurityDeposit='Y' and Receipt_Type='P' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  then Receipt_Amount else 0 end )as Security_Amt,sum(case when SecurityDeposit='Y' and Receipt_Type='F' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  then Receipt_Amount else 0 end )as Refund_Amt,  sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' then Receipt_Amount else 0 end )as RTGS_Amt, sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) = '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") & "' then Receipt_Amount else 0 end )as Closing_Rec_Amt from TSPL_RECEIPT_HEADER WHERE  convert(date,Receipt_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") & "' and  convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' and Posted='Y' group by Cust_Code ) XX ) Receipt ON Receipt.Cust_Code=xxx.Customer_Code " & Environment.NewLine & " )xxfinal  left join TSPL_COMPANY_MASTER on 1=1"
            '''''left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No where  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy hh:mm tt") & "' and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  ) xx group by xx.Customer_Code ) xxx " & Environment.NewLine & " left join ( select vendor_code,sum(Type * amount)Guarantee_Amount from ( " & Environment.NewLine & " Select 1 AS Type,vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "', 103) BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103) and status='Y'

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
                SetGridFormation(isprint)
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                If isprint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dt, "rptNewTenderPartyList", "New Tender Party List ")
                    frmCRV = Nothing
                End If
            Else
                If isprint Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub LoadData1(isPrint As Boolean)
        Try
            Dim strPrintqry As String = ""
            If clsCommon.CompairString(cmbShift.SelectedValue, "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Please select shift type", Me.Text)
                Exit Sub
            End If
            If isPrint Then
                strPrintqry = " TSPL_COMPANY_MASTER.Comp_Name,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "' as Date, '" & txtDate.Value.AddDays(-1).Day & "' as Previous_Day,'" & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2), "dd/MM/yyyy") & "' as Previous_Date,'" & objCommonVar.CurrentUser & "' as User_Code, "
            End If
            Dim qry As String = "  select " & strPrintqry & " ROW_NUMBER() over (order by Customer_Code) as SNO,IsDistributor,Route_No,case when Credit_Customer='Y' then Zone_Code1 else Zone_Code end as Zone_Code1,Credit_Customer,Customer_Code,Customer_Name,Cust_Type_Code as [Customer Type],xxfinal.Phone1,Guarantee_Amount,Security_Amt,Zone_Code,Closing_Bal,Return_Amt as [Sale Return Amt],Debit_Total as [Debit Amt],Credit_Total as [Credit Amt],Sale_Amt,RTGS_Amt,(Closing_Bal+ Sale_Amt+Debit_Total -RTGS_Amt-Credit_Total-Return_Amt) as Outstanding_Amt,Estimated_Avg_Sale,(Closing_Bal+ Sale_Amt -RTGS_Amt) - (Estimated_Avg_Sale) as Estimated_OS_Amt  from ( " & Environment.NewLine & "select IsDistributor,Route_No,Zone_Code1,Credit_Customer,Customer_Code,Customer_Name,Cust_Type_Code,Phone1,isnull(Guarantee_Amount,0)Guarantee_Amount,isnull(Security_Amt,0)Security_Amt,Zone_Code,isnull((Closing_Sale_Amt-Closing_Rec_Amt),0) as Closing_Bal,isnull(Sale_Amt,0) Sale_Amt,isnull(RTGS_Amt,0) as RTGS_Amt,isnull(Estimated_Avg_Sale,0)Estimated_Avg_Sale,isnull((Return_Amt),0) as Return_Amt,isnull((Credit_Total),0) as Credit_Total,isnull((Debit_Total),0) as Debit_Total from ( " & Environment.NewLine & "
            select max(IsDistributor)IsDistributor,max(Route_No)Route_No,max(Zone_Code1)Zone_Code1,max(Credit_Customer)Credit_Customer,Customer_Code,max(Customer_Name)Customer_Name,max(Cust_Type_Code)Cust_Type_Code,max(Phone1)Phone1,  MAX(Zone_Code)Zone_Code,sum(Sale_Amt)Sale_Amt,sum(Closing_Sale_Amt)Closing_Sale_Amt,sum(Estimated_Avg_Sale)Estimated_Avg_Sale from ( " & Environment.NewLine & " select TSPL_CUSTOMER_MASTER.Cust_Type_Code,TSPL_CUSTOMER_MASTER.IsDistributor,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Zone_Code as Zone_Code1,TSPL_CUSTOMER_MASTER.Credit_Customer, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,replace(TSPL_CUSTOMER_MASTER.Phone1,'(+91)','') as Phone1,ROUTE.Zone_Code, ( case when (Convert(Date, TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy ") & "',103) AND  TSPL_SD_SHIPMENT_HEAD.Shift_Type='PM')
            OR (Convert(Date, TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "',103) and TSPL_SD_SHIPMENT_HEAD.Shift_Type='AM') then TSPL_SD_SALE_INVOICE_HEAD.Total_Amt else 0 end  ) as Sale_Amt, ( case when (Convert(Date, TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "',103) AND  TSPL_SD_SHIPMENT_HEAD.Shift_Type='PM') OR (Convert(Date, TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy ") & "',103) and TSPL_SD_SHIPMENT_HEAD.Shift_Type='AM') then TSPL_SD_SALE_INVOICE_HEAD.Total_Amt else 0 end  ) as Closing_Sale_Amt,
            case when Convert(Date, TSPL_SD_SHIPMENT_HEAD.Supply_Date,103) = Convert(Date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "',103) " & IIf(cmbShift.SelectedValue <> "Both", " AND   TSPL_SD_SHIPMENT_HEAD.Shift_Type='" & cmbShift.SelectedValue & "'", "") & "  then TSPL_SD_SALE_INVOICE_HEAD.Total_Amt else 0  end as Estimated_Avg_Sale, TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Shift_Type,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt  from TSPL_SD_SALE_INVOICE_HEAD " & Environment.NewLine & "  LEFT JOIN ( SELECT string_agg (Zone_Code,',') Zone_Code,Route_No FROM ( select ISNULL( Zone_Code,'')Zone_Code,Route_No from  TSPL_ROUTE_MASTER) XX GROUP BY Route_No, Zone_Code ) Route ON Route.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No where CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy ") & "' and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "'  ) xx group by xx.Customer_Code ) xxx " & Environment.NewLine & " left join ( select vendor_code,sum(Type * amount)Guarantee_Amount from ( " & Environment.NewLine & " Select 1 AS Type,vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "', 103) BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103) and status='Y'
	        " & Environment.NewLine & "  union all " & Environment.NewLine & " Select -1 as Type, vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RT' AND Receiving_code IN ( Select DocNo from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "', 103) BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103)  and status='Y' ) AND  CONVERT(date, Date, 103)  < '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' and status='Y' ) xx group by vendor_code )Bank on Bank.vendor_code=xxx.Customer_Code
            " & Environment.NewLine & "LEFT JOIN (Select Cust_Code,(Security_Amt-Refund_Amt)Security_Amt,RTGS_Amt,Closing_Rec_Amt from  (select Cust_Code, sum(case when SecurityDeposit='Y' and Receipt_Type='P' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "'  then Receipt_Amount else 0 end )as Security_Amt,sum(case when SecurityDeposit='Y' and Receipt_Type='F' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "'  then Receipt_Amount else 0 end )as Refund_Amt,  sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' then Receipt_Amount else 0 end )as RTGS_Amt, sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy ") & "' then Receipt_Amount else 0 end )as Closing_Rec_Amt from TSPL_RECEIPT_HEADER WHERE convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' and Posted='Y' group by Cust_Code ) XX ) Receipt ON Receipt.Cust_Code=xxx.Customer_Code LEFT JOIN (Select Customer_Code as CustCode, Cast (Sum(Return_Amt) as decimal(18,2)) Return_Amt 
from (Select Customer_Code ,Sum(Total_Amt)Return_Amt from TSPL_SD_SALE_RETURN_HEAD where convert(date,Document_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' group by Customer_Code
union all
Select cust_Code ,Sum(Doc_Amt)Return_Amt from TSPL_SCRAPSALE_HEAD_RETURN where convert(date,Return_ship_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' group by cust_Code
)XX Group by Customer_Code ) Return_Data on Return_Data.CustCode=xxx.Customer_Code

left join (Select Customer_Code as Credit_Cust,Sum(Document_Total)Credit_Total from TSPL_Customer_Invoice_Head 
where convert(date,Posting_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' and  Document_Type ='C' and (Against_Sale_Return_No is null or Against_Sale_Return_No='')
and (Against_MCC_Material_Sale_Return is null or Against_MCC_Material_Sale_Return='') and (AgainstScrapReturn is null or AgainstScrapReturn='')   group by Customer_Code)Credit_Amt on Credit_Amt.Credit_Cust=xxx.Customer_Code

left join (Select Customer_Code as Debit_Cust,Sum(Document_Total)Debit_Total from TSPL_Customer_Invoice_Head 
where convert(date,Posting_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy ") & "' and  Document_Type ='D' and (Against_Sale_Return_No is null or Against_Sale_Return_No='')
and (Against_MCC_Material_Sale_Return is null or Against_MCC_Material_Sale_Return='') and (AgainstScrapReturn is null or AgainstScrapReturn='')   group by Customer_Code)Debit_Amt on Debit_Amt.Debit_Cust=xxx.Customer_Code " & Environment.NewLine & " )xxfinal  left join TSPL_COMPANY_MASTER on 1=1"
            qry += " where 2=2 "
            If rbtnCreditCustomer.IsChecked Then
                qry += " and Credit_Customer='Y' "
            ElseIf rbtnDistributor.IsChecked Then
                qry += " and IsDistributor='Y' "
            End If
            If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
                qry += " and xxfinal.Cust_Type_Code in(" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and xxfinal.Customer_Code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")" + Environment.NewLine
            End If

            ''''" & Environment.NewLine & "LEFT JOIN (Select Cust_Code,(Security_Amt-Refund_Amt)Security_Amt,RTGS_Amt,Closing_Rec_Amt from  (select Cust_Code, sum(case when SecurityDeposit='Y' and Receipt_Type='P' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  then Receipt_Amount else 0 end )as Security_Amt,sum(case when SecurityDeposit='Y' and Receipt_Type='F' and convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  then Receipt_Amount else 0 end )as Refund_Amt,  sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' then Receipt_Amount else 0 end )as RTGS_Amt, sum(case when SecurityDeposit='N' and convert(date,Receipt_Date,103) = '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") & "' then Receipt_Amount else 0 end )as Closing_Rec_Amt from TSPL_RECEIPT_HEADER WHERE  convert(date,Receipt_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-1)), "dd/MMM/yyyy hh:mm tt") & "' and  convert(date,Receipt_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' and Posted='Y' group by Cust_Code ) XX ) Receipt ON Receipt.Cust_Code=xxx.Customer_Code " & Environment.NewLine & " )xxfinal  left join TSPL_COMPANY_MASTER on 1=1"
            '''''left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No where  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value.AddDays(-2)), "dd/MMM/yyyy hh:mm tt") & "' and  CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "'  ) xx group by xx.Customer_Code ) xxx " & Environment.NewLine & " left join ( select vendor_code,sum(Type * amount)Guarantee_Amount from ( " & Environment.NewLine & " Select 1 AS Type,vendor_code,Amount from TSPL_BANK_GUARANTEE_MASTER where Bank_Guarantee_Type = 'RC' AND CONVERT(date, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "', 103) BETWEEN  CONVERT(date, start_date, 103) AND CONVERT(date, extended_date, 103) and status='Y'

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
                SetGridFormation(isPrint)
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dt, "rptNewTenderPartyList", "New Tender Party List ")
                    frmCRV = Nothing
                End If
            Else
                If isPrint Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation(ByVal isPrint As Boolean)
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            If clsCommon.CompairString(gv1.Columns(ii).Name, "SNO") <> CompairStringResult.Equal Then
                gv1.Columns(ii).FormatString = "{0:n2}"
            End If
        Next
        gv1.ShowGroupPanel = False

        gv1.Columns("IsDistributor").IsVisible = False
        gv1.Columns("Route_No").IsVisible = False
        gv1.Columns("Zone_Code1").IsVisible = False
        gv1.Columns("Credit_Customer").IsVisible = False
        gv1.Columns("SNO").HeaderText = "S.No"
        gv1.Columns("Customer_Code").HeaderText = "Customer Code"
        gv1.Columns("Customer_Name").HeaderText = "Name of Distributor"
        gv1.Columns("Phone1").HeaderText = "Distributors " & Environment.NewLine & " Mobile Number"
        gv1.Columns("Guarantee_Amount").HeaderText = "Bank " & Environment.NewLine & " Guarantee"
        gv1.Columns("Security_Amt").HeaderText = "Security"
        gv1.Columns("Closing_Bal").HeaderText = "Closing " & Environment.NewLine & " Balance " & Environment.NewLine & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2), "dd/MM/yyyy") & ""
        gv1.Columns("Sale_Amt").HeaderText = "Sale " & txtDate.Value.AddDays(-1).Day & "," & " " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & " " & Environment.NewLine & "(Evening + Morning Supply)"
        gv1.Columns("RTGS_Amt").HeaderText = "RTGS_Amt " & Environment.NewLine & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & ""
        gv1.Columns("Outstanding_Amt").HeaderText = "Outstanding " & Environment.NewLine & " Amount (in Rs.)"
        gv1.Columns("Estimated_Avg_Sale").HeaderText = "Estimated One Day Average Sale " & Environment.NewLine & clsCommon.GetPrintDate(txtDate.Value.AddDays(-2), "dd/MM/yyyy") & ""
        gv1.Columns("Estimated_OS_Amt").HeaderText = "Estimated " & Environment.NewLine & " Outstanding " & Environment.NewLine & "Balance"
        gv1.Columns("Zone_Code").HeaderText = "Zone.No/Area.No"

        Dim index As Integer = 0
        If isPrint Then
            index = 9
            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("Previous_Day").IsVisible = False
            gv1.Columns("Previous_Date").IsVisible = False
            gv1.Columns("Date").IsVisible = False
            gv1.Columns("User_Code").IsVisible = False
        Else
            'index = 4
            index = 8
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()

        For ii As Integer = index To gv1.Columns.Count - 1
            If clsCommon.CompairString(gv1.Columns(ii).Name, "Zone_Code") <> CompairStringResult.Equal Then
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptNewTenderPartyListReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtDate.Value) + "  To " + clsCommon.myCDate(txtDate.Value))
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
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                Dim ReportHeading As String = ""
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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub

    Private Sub TxtCustomerType__My_Click(sender As Object, e As EventArgs) Handles TxtCustomerType._My_Click
        Dim qry As String = " SELECT  [Cust_Type_Code] as Code,[Cust_Type_Desc] as Name FROM [TSPL_CUSTOMER_TYPE_MASTER] Where 2=2  "
        TxtCustomerType.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)

        txtMultiCustomer.arrValueMember = Nothing
    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master  "
        If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
            qry += " Where Cust_Type_Code In (" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")"
        End If
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)

    End Sub
End Class

